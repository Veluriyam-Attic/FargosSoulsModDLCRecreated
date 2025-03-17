using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200075A RID: 1882
	internal struct DataCollector
	{
		// Token: 0x06005C6B RID: 23659 RVA: 0x001C1330 File Offset: 0x001C0530
		internal unsafe void Enable(byte* scratch, int scratchSize, EventSource.EventData* datas, int dataCount, GCHandle* pins, int pinCount)
		{
			this.datasStart = datas;
			this.scratchEnd = scratch + scratchSize;
			this.datasEnd = datas + dataCount;
			this.pinsEnd = pins + pinCount;
			this.scratch = scratch;
			this.datas = datas;
			this.pins = pins;
			this.writingScalars = false;
		}

		// Token: 0x06005C6C RID: 23660 RVA: 0x001C138F File Offset: 0x001C058F
		internal void Disable()
		{
			this = default(DataCollector);
		}

		// Token: 0x06005C6D RID: 23661 RVA: 0x001C1398 File Offset: 0x001C0598
		internal unsafe EventSource.EventData* Finish()
		{
			this.ScalarsEnd();
			return this.datas;
		}

		// Token: 0x06005C6E RID: 23662 RVA: 0x001C13A8 File Offset: 0x001C05A8
		internal unsafe void AddScalar(void* value, int size)
		{
			if (this.bufferNesting != 0)
			{
				int num = this.bufferPos;
				int num2;
				checked
				{
					this.bufferPos += size;
					this.EnsureBuffer();
					num2 = 0;
				}
				while (num2 != size)
				{
					this.buffer[num] = ((byte*)value)[num2];
					num2++;
					num++;
				}
				return;
			}
			byte* ptr = this.scratch;
			byte* ptr2 = ptr + size;
			if (this.scratchEnd < ptr2)
			{
				throw new IndexOutOfRangeException(SR.EventSource_AddScalarOutOfRange);
			}
			this.ScalarsBegin();
			this.scratch = ptr2;
			for (int num3 = 0; num3 != size; num3++)
			{
				ptr[num3] = ((byte*)value)[num3];
			}
		}

		// Token: 0x06005C6F RID: 23663 RVA: 0x001C1440 File Offset: 0x001C0640
		internal unsafe void AddNullTerminatedString(string value)
		{
			if (value == null)
			{
				value = string.Empty;
			}
			int num = value.IndexOf('\0');
			if (num < 0)
			{
				num = value.Length;
			}
			int num2 = (num + 1) * 2;
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(num2);
			}
			if (this.bufferNesting == 0)
			{
				this.ScalarsEnd();
				this.PinArray(value, num2);
				return;
			}
			int startIndex = this.bufferPos;
			checked
			{
				this.bufferPos += num2;
				this.EnsureBuffer();
			}
			void* ptr;
			if (value == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = value.GetPinnableReference())
				{
					ptr = (void*)ptr2;
				}
			}
			void* value2 = ptr;
			Marshal.Copy((IntPtr)value2, this.buffer, startIndex, num2);
			char* ptr2 = null;
		}

		// Token: 0x06005C70 RID: 23664 RVA: 0x001C14DC File Offset: 0x001C06DC
		internal unsafe void AddArray(Array value, int length, int itemSize)
		{
			if (length > 65535)
			{
				length = 65535;
			}
			int num = length * itemSize;
			if (this.bufferNesting != 0)
			{
				this.EnsureBuffer(num + 2);
			}
			this.AddScalar((void*)(&length), 2);
			checked
			{
				if (length != 0)
				{
					if (this.bufferNesting == 0)
					{
						this.ScalarsEnd();
						this.PinArray(value, num);
						return;
					}
					int dstOffset = this.bufferPos;
					this.bufferPos += num;
					this.EnsureBuffer();
					Buffer.BlockCopy(value, 0, this.buffer, dstOffset, num);
				}
			}
		}

		// Token: 0x06005C71 RID: 23665 RVA: 0x001C155B File Offset: 0x001C075B
		internal int BeginBufferedArray()
		{
			this.BeginBuffered();
			this.bufferPos += 2;
			return this.bufferPos;
		}

		// Token: 0x06005C72 RID: 23666 RVA: 0x001C1577 File Offset: 0x001C0777
		internal void EndBufferedArray(int bookmark, int count)
		{
			this.EnsureBuffer();
			this.buffer[bookmark - 2] = (byte)count;
			this.buffer[bookmark - 1] = (byte)(count >> 8);
			this.EndBuffered();
		}

		// Token: 0x06005C73 RID: 23667 RVA: 0x001C159F File Offset: 0x001C079F
		internal void BeginBuffered()
		{
			this.ScalarsEnd();
			this.bufferNesting++;
		}

		// Token: 0x06005C74 RID: 23668 RVA: 0x001C15B5 File Offset: 0x001C07B5
		internal void EndBuffered()
		{
			this.bufferNesting--;
			if (this.bufferNesting == 0)
			{
				this.EnsureBuffer();
				this.PinArray(this.buffer, this.bufferPos);
				this.buffer = null;
				this.bufferPos = 0;
			}
		}

		// Token: 0x06005C75 RID: 23669 RVA: 0x001C15F4 File Offset: 0x001C07F4
		private void EnsureBuffer()
		{
			int num = this.bufferPos;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x06005C76 RID: 23670 RVA: 0x001C1624 File Offset: 0x001C0824
		private void EnsureBuffer(int additionalSize)
		{
			int num = this.bufferPos + additionalSize;
			if (this.buffer == null || this.buffer.Length < num)
			{
				this.GrowBuffer(num);
			}
		}

		// Token: 0x06005C77 RID: 23671 RVA: 0x001C1654 File Offset: 0x001C0854
		private void GrowBuffer(int required)
		{
			int num = (this.buffer == null) ? 64 : this.buffer.Length;
			do
			{
				num *= 2;
			}
			while (num < required);
			Array.Resize<byte>(ref this.buffer, num);
		}

		// Token: 0x06005C78 RID: 23672 RVA: 0x001C168C File Offset: 0x001C088C
		private unsafe void PinArray(object value, int size)
		{
			GCHandle* ptr = this.pins;
			if (this.pinsEnd == ptr)
			{
				throw new IndexOutOfRangeException(SR.EventSource_PinArrayOutOfRange);
			}
			EventSource.EventData* ptr2 = this.datas;
			if (this.datasEnd == ptr2)
			{
				throw new IndexOutOfRangeException(SR.EventSource_DataDescriptorsOutOfRange);
			}
			this.pins = ptr + 1;
			this.datas = ptr2 + 1;
			*ptr = GCHandle.Alloc(value, GCHandleType.Pinned);
			ptr2->DataPointer = ptr->AddrOfPinnedObject();
			ptr2->m_Size = size;
		}

		// Token: 0x06005C79 RID: 23673 RVA: 0x001C170C File Offset: 0x001C090C
		private unsafe void ScalarsBegin()
		{
			if (!this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				if (this.datasEnd == ptr)
				{
					throw new IndexOutOfRangeException(SR.EventSource_DataDescriptorsOutOfRange);
				}
				ptr->DataPointer = (IntPtr)((void*)this.scratch);
				this.writingScalars = true;
			}
		}

		// Token: 0x06005C7A RID: 23674 RVA: 0x001C1754 File Offset: 0x001C0954
		private unsafe void ScalarsEnd()
		{
			if (this.writingScalars)
			{
				EventSource.EventData* ptr = this.datas;
				ptr->m_Size = (this.scratch - checked((UIntPtr)ptr->m_Ptr)) / 1;
				this.datas = ptr + 1;
				this.writingScalars = false;
			}
		}

		// Token: 0x04001BB0 RID: 7088
		[ThreadStatic]
		internal static DataCollector ThreadInstance;

		// Token: 0x04001BB1 RID: 7089
		private unsafe byte* scratchEnd;

		// Token: 0x04001BB2 RID: 7090
		private unsafe EventSource.EventData* datasEnd;

		// Token: 0x04001BB3 RID: 7091
		private unsafe GCHandle* pinsEnd;

		// Token: 0x04001BB4 RID: 7092
		private unsafe EventSource.EventData* datasStart;

		// Token: 0x04001BB5 RID: 7093
		private unsafe byte* scratch;

		// Token: 0x04001BB6 RID: 7094
		private unsafe EventSource.EventData* datas;

		// Token: 0x04001BB7 RID: 7095
		private unsafe GCHandle* pins;

		// Token: 0x04001BB8 RID: 7096
		private byte[] buffer;

		// Token: 0x04001BB9 RID: 7097
		private int bufferPos;

		// Token: 0x04001BBA RID: 7098
		private int bufferNesting;

		// Token: 0x04001BBB RID: 7099
		private bool writingScalars;
	}
}
