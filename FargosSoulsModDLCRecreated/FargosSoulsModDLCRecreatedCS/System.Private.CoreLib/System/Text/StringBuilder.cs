using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Text
{
	// Token: 0x02000355 RID: 853
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class StringBuilder : ISerializable
	{
		// Token: 0x06002CA1 RID: 11425 RVA: 0x001553B4 File Offset: 0x001545B4
		private int GetReplaceBufferCapacity(int requiredCapacity)
		{
			int num = this.Capacity;
			if (num < requiredCapacity)
			{
				num = (requiredCapacity + 1 & -2);
			}
			return num;
		}

		// Token: 0x06002CA2 RID: 11426 RVA: 0x001553D4 File Offset: 0x001545D4
		internal unsafe void ReplaceBufferInternal(char* newBuffer, int newLength)
		{
			if (newLength > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_Capacity);
			}
			if (newLength > this.m_ChunkChars.Length)
			{
				this.m_ChunkChars = new char[this.GetReplaceBufferCapacity(newLength)];
			}
			new Span<char>((void*)newBuffer, newLength).CopyTo(this.m_ChunkChars);
			this.m_ChunkLength = newLength;
			this.m_ChunkPrevious = null;
			this.m_ChunkOffset = 0;
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x00155448 File Offset: 0x00154648
		internal void ReplaceBufferUtf8Internal(ReadOnlySpan<byte> source)
		{
			if (source.Length > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_Capacity);
			}
			int charCount = Encoding.UTF8.GetCharCount(source);
			if (charCount > this.m_ChunkChars.Length)
			{
				this.m_ChunkChars = new char[this.GetReplaceBufferCapacity(charCount)];
			}
			this.m_ChunkLength = Encoding.UTF8.GetChars(source, this.m_ChunkChars);
			this.m_ChunkPrevious = null;
			this.m_ChunkOffset = 0;
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x001554C8 File Offset: 0x001546C8
		internal unsafe void ReplaceBufferAnsiInternal(sbyte* newBuffer, int newLength)
		{
			if (newLength > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_Capacity);
			}
			if (newLength > this.m_ChunkChars.Length)
			{
				this.m_ChunkChars = new char[this.GetReplaceBufferCapacity(newLength)];
			}
			char[] array;
			char* lpWideCharStr;
			if ((array = this.m_ChunkChars) == null || array.Length == 0)
			{
				lpWideCharStr = null;
			}
			else
			{
				lpWideCharStr = &array[0];
			}
			int chunkLength = Interop.Kernel32.MultiByteToWideChar(0U, 1U, (byte*)newBuffer, newLength, lpWideCharStr, newLength);
			array = null;
			this.m_ChunkOffset = 0;
			this.m_ChunkLength = chunkLength;
			this.m_ChunkPrevious = null;
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x0015554C File Offset: 0x0015474C
		internal unsafe void InternalCopy(IntPtr dest, int len)
		{
			if (len == 0)
			{
				return;
			}
			bool flag = true;
			byte* ptr = (byte*)dest.ToPointer();
			StringBuilder stringBuilder = this.FindChunkForByte(len);
			do
			{
				int num = stringBuilder.m_ChunkOffset * 2;
				int len2 = stringBuilder.m_ChunkLength * 2;
				fixed (char* ptr2 = &stringBuilder.m_ChunkChars[0])
				{
					char* ptr3 = ptr2;
					byte* src = (byte*)ptr3;
					if (flag)
					{
						flag = false;
						Buffer.Memcpy(ptr + num, src, len - num);
					}
					else
					{
						Buffer.Memcpy(ptr + num, src, len2);
					}
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			while (stringBuilder != null);
		}

		// Token: 0x06002CA6 RID: 11430 RVA: 0x001555C8 File Offset: 0x001547C8
		private StringBuilder FindChunkForByte(int byteIndex)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset * 2 > byteIndex)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x06002CA7 RID: 11431 RVA: 0x001555EC File Offset: 0x001547EC
		public StringBuilder()
		{
			this.m_MaxCapacity = int.MaxValue;
			this.m_ChunkChars = new char[16];
		}

		// Token: 0x06002CA8 RID: 11432 RVA: 0x0015560C File Offset: 0x0015480C
		public StringBuilder(int capacity) : this(capacity, int.MaxValue)
		{
		}

		// Token: 0x06002CA9 RID: 11433 RVA: 0x0015561A File Offset: 0x0015481A
		[NullableContext(2)]
		public StringBuilder(string value) : this(value, 16)
		{
		}

		// Token: 0x06002CAA RID: 11434 RVA: 0x00155625 File Offset: 0x00154825
		[NullableContext(2)]
		public StringBuilder(string value, int capacity) : this(value, 0, (value != null) ? value.Length : 0, capacity)
		{
		}

		// Token: 0x06002CAB RID: 11435 RVA: 0x0015563C File Offset: 0x0015483C
		[NullableContext(2)]
		public unsafe StringBuilder(string value, int startIndex, int length, int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format(SR.ArgumentOutOfRange_MustBePositive, "capacity"));
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.Format(SR.ArgumentOutOfRange_MustBeNonNegNum, "length"));
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (value == null)
			{
				value = string.Empty;
			}
			if (startIndex > value.Length - length)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_IndexLength);
			}
			this.m_MaxCapacity = int.MaxValue;
			if (capacity == 0)
			{
				capacity = 16;
			}
			capacity = Math.Max(capacity, length);
			this.m_ChunkChars = GC.AllocateUninitializedArray<char>(capacity, false);
			this.m_ChunkLength = length;
			char* ptr;
			if (value == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = value.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			StringBuilder.ThreadSafeCopy(ptr3 + startIndex, this.m_ChunkChars, 0, length);
			char* ptr2 = null;
		}

		// Token: 0x06002CAC RID: 11436 RVA: 0x00155720 File Offset: 0x00154920
		public StringBuilder(int capacity, int maxCapacity)
		{
			if (capacity > maxCapacity)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_Capacity);
			}
			if (maxCapacity < 1)
			{
				throw new ArgumentOutOfRangeException("maxCapacity", SR.ArgumentOutOfRange_SmallMaxCapacity);
			}
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.Format(SR.ArgumentOutOfRange_MustBePositive, "capacity"));
			}
			if (capacity == 0)
			{
				capacity = Math.Min(16, maxCapacity);
			}
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkChars = GC.AllocateUninitializedArray<char>(capacity, false);
		}

		// Token: 0x06002CAD RID: 11437 RVA: 0x0015579C File Offset: 0x0015499C
		private StringBuilder(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			int num = 0;
			string text = null;
			int num2 = int.MaxValue;
			bool flag = false;
			SerializationInfoEnumerator enumerator = info.GetEnumerator();
			while (enumerator.MoveNext())
			{
				string name = enumerator.Name;
				if (!(name == "m_MaxCapacity"))
				{
					if (!(name == "m_StringValue"))
					{
						if (name == "Capacity")
						{
							num = info.GetInt32("Capacity");
							flag = true;
						}
					}
					else
					{
						text = info.GetString("m_StringValue");
					}
				}
				else
				{
					num2 = info.GetInt32("m_MaxCapacity");
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			if (num2 < 1 || text.Length > num2)
			{
				throw new SerializationException(SR.Serialization_StringBuilderMaxCapacity);
			}
			if (!flag)
			{
				num = Math.Min(Math.Max(16, text.Length), num2);
			}
			if (num < 0 || num < text.Length || num > num2)
			{
				throw new SerializationException(SR.Serialization_StringBuilderCapacity);
			}
			this.m_MaxCapacity = num2;
			this.m_ChunkChars = GC.AllocateUninitializedArray<char>(num, false);
			text.CopyTo(0, this.m_ChunkChars, 0, text.Length);
			this.m_ChunkLength = text.Length;
		}

		// Token: 0x06002CAE RID: 11438 RVA: 0x001558C4 File Offset: 0x00154AC4
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("m_MaxCapacity", this.m_MaxCapacity);
			info.AddValue("Capacity", this.Capacity);
			info.AddValue("m_StringValue", this.ToString());
			info.AddValue("m_currentThread", 0);
		}

		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x0015591E File Offset: 0x00154B1E
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x00155930 File Offset: 0x00154B30
		public int Capacity
		{
			get
			{
				return this.m_ChunkChars.Length + this.m_ChunkOffset;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_NegativeCapacity);
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_Capacity);
				}
				if (value < this.Length)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_SmallCapacity);
				}
				if (this.Capacity != value)
				{
					int length = value - this.m_ChunkOffset;
					char[] array = GC.AllocateUninitializedArray<char>(length, false);
					Array.Copy(this.m_ChunkChars, array, this.m_ChunkLength);
					this.m_ChunkChars = array;
				}
			}
		}

		// Token: 0x17000905 RID: 2309
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x001559B6 File Offset: 0x00154BB6
		public int MaxCapacity
		{
			get
			{
				return this.m_MaxCapacity;
			}
		}

		// Token: 0x06002CB2 RID: 11442 RVA: 0x001559BE File Offset: 0x00154BBE
		public int EnsureCapacity(int capacity)
		{
			if (capacity < 0)
			{
				throw new ArgumentOutOfRangeException("capacity", SR.ArgumentOutOfRange_NegativeCapacity);
			}
			if (this.Capacity < capacity)
			{
				this.Capacity = capacity;
			}
			return this.Capacity;
		}

		// Token: 0x06002CB3 RID: 11443 RVA: 0x001559EC File Offset: 0x00154BEC
		public unsafe override string ToString()
		{
			if (this.Length == 0)
			{
				return string.Empty;
			}
			string text = string.FastAllocateString(this.Length);
			StringBuilder stringBuilder = this;
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = text.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* ptr2 = ptr;
			for (;;)
			{
				if (stringBuilder.m_ChunkLength > 0)
				{
					char[] chunkChars = stringBuilder.m_ChunkChars;
					int chunkOffset = stringBuilder.m_ChunkOffset;
					int chunkLength = stringBuilder.m_ChunkLength;
					if (chunkLength + chunkOffset > text.Length || chunkLength > chunkChars.Length)
					{
						break;
					}
					fixed (char* ptr3 = &chunkChars[0])
					{
						char* smem = ptr3;
						string.wstrcpy(ptr2 + chunkOffset, smem, chunkLength);
					}
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
				if (stringBuilder == null)
				{
					return text;
				}
			}
			throw new ArgumentOutOfRangeException("chunkLength", SR.ArgumentOutOfRange_Index);
		}

		// Token: 0x06002CB4 RID: 11444 RVA: 0x00155A9C File Offset: 0x00154C9C
		public unsafe string ToString(int startIndex, int length)
		{
			int length2 = this.Length;
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (startIndex > length2)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndexLargerThanLength);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NegativeLength);
			}
			if (startIndex > length2 - length)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_IndexLength);
			}
			string text = string.FastAllocateString(length);
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = text.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* pointer = ptr;
			this.CopyTo(startIndex, new Span<char>((void*)pointer, length), length);
			return text;
		}

		// Token: 0x06002CB5 RID: 11445 RVA: 0x00155B2A File Offset: 0x00154D2A
		public StringBuilder Clear()
		{
			this.Length = 0;
			return this;
		}

		// Token: 0x17000906 RID: 2310
		// (get) Token: 0x06002CB6 RID: 11446 RVA: 0x00155B34 File Offset: 0x00154D34
		// (set) Token: 0x06002CB7 RID: 11447 RVA: 0x00155B44 File Offset: 0x00154D44
		public int Length
		{
			get
			{
				return this.m_ChunkOffset + this.m_ChunkLength;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_NegativeLength);
				}
				if (value > this.MaxCapacity)
				{
					throw new ArgumentOutOfRangeException("value", SR.ArgumentOutOfRange_SmallCapacity);
				}
				if (value == 0 && this.m_ChunkPrevious == null)
				{
					this.m_ChunkLength = 0;
					this.m_ChunkOffset = 0;
					return;
				}
				int num = value - this.Length;
				if (num > 0)
				{
					this.Append('\0', num);
					return;
				}
				StringBuilder stringBuilder = this.FindChunkForIndex(value);
				if (stringBuilder != this)
				{
					int num2 = Math.Min(this.Capacity, Math.Max(this.Length * 6 / 5, this.m_ChunkChars.Length));
					int num3 = num2 - stringBuilder.m_ChunkOffset;
					if (num3 > stringBuilder.m_ChunkChars.Length)
					{
						char[] array = GC.AllocateUninitializedArray<char>(num3, false);
						Array.Copy(stringBuilder.m_ChunkChars, array, stringBuilder.m_ChunkLength);
						this.m_ChunkChars = array;
					}
					else
					{
						this.m_ChunkChars = stringBuilder.m_ChunkChars;
					}
					this.m_ChunkPrevious = stringBuilder.m_ChunkPrevious;
					this.m_ChunkOffset = stringBuilder.m_ChunkOffset;
				}
				this.m_ChunkLength = value - stringBuilder.m_ChunkOffset;
			}
		}

		// Token: 0x17000907 RID: 2311
		[IndexerName("Chars")]
		public char this[int index]
		{
			get
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new IndexOutOfRangeException();
				}
				return stringBuilder.m_ChunkChars[num];
				Block_3:
				throw new IndexOutOfRangeException();
			}
			set
			{
				StringBuilder stringBuilder = this;
				int num;
				for (;;)
				{
					num = index - stringBuilder.m_ChunkOffset;
					if (num >= 0)
					{
						break;
					}
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder == null)
					{
						goto Block_3;
					}
				}
				if (num >= stringBuilder.m_ChunkLength)
				{
					throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
				}
				stringBuilder.m_ChunkChars[num] = value;
				return;
				Block_3:
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
		}

		// Token: 0x06002CBA RID: 11450 RVA: 0x00155CEC File Offset: 0x00154EEC
		public StringBuilder.ChunkEnumerator GetChunks()
		{
			return new StringBuilder.ChunkEnumerator(this);
		}

		// Token: 0x06002CBB RID: 11451 RVA: 0x00155CF4 File Offset: 0x00154EF4
		public StringBuilder Append(char value, int repeatCount)
		{
			if (repeatCount < 0)
			{
				throw new ArgumentOutOfRangeException("repeatCount", SR.ArgumentOutOfRange_NegativeCount);
			}
			if (repeatCount == 0)
			{
				return this;
			}
			int num = this.Length + repeatCount;
			if (num > this.m_MaxCapacity || num < repeatCount)
			{
				throw new ArgumentOutOfRangeException("repeatCount", SR.ArgumentOutOfRange_LengthGreaterThanCapacity);
			}
			int num2 = this.m_ChunkLength;
			while (repeatCount > 0)
			{
				if (num2 < this.m_ChunkChars.Length)
				{
					this.m_ChunkChars[num2++] = value;
					repeatCount--;
				}
				else
				{
					this.m_ChunkLength = num2;
					this.ExpandByABlock(repeatCount);
					num2 = 0;
				}
			}
			this.m_ChunkLength = num2;
			return this;
		}

		// Token: 0x06002CBC RID: 11452 RVA: 0x00155D84 File Offset: 0x00154F84
		public unsafe StringBuilder Append([Nullable(2)] char[] value, int startIndex, int charCount)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (charCount > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_Index);
				}
				if (charCount == 0)
				{
					return this;
				}
				fixed (char* ptr = &value[startIndex])
				{
					char* value2 = ptr;
					this.Append(value2, charCount);
					return this;
				}
			}
		}

		// Token: 0x06002CBD RID: 11453 RVA: 0x00155E04 File Offset: 0x00155004
		public unsafe StringBuilder Append([Nullable(2)] string value)
		{
			if (value != null)
			{
				char[] chunkChars = this.m_ChunkChars;
				int chunkLength = this.m_ChunkLength;
				int length = value.Length;
				if (chunkLength + length < chunkChars.Length)
				{
					if (length <= 2)
					{
						if (length > 0)
						{
							chunkChars[chunkLength] = value[0];
						}
						if (length > 1)
						{
							chunkChars[chunkLength + 1] = value[1];
						}
					}
					else
					{
						char* ptr;
						if (value == null)
						{
							ptr = null;
						}
						else
						{
							fixed (char* ptr2 = value.GetPinnableReference())
							{
								ptr = ptr2;
							}
						}
						char* smem = ptr;
						fixed (char* ptr3 = &chunkChars[chunkLength])
						{
							char* dmem = ptr3;
							string.wstrcpy(dmem, smem, length);
						}
						char* ptr2 = null;
					}
					this.m_ChunkLength = chunkLength + length;
				}
				else
				{
					this.AppendHelper(value);
				}
			}
			return this;
		}

		// Token: 0x06002CBE RID: 11454 RVA: 0x00155EA0 File Offset: 0x001550A0
		private unsafe void AppendHelper(string value)
		{
			char* ptr;
			if (value == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = value.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* value2 = ptr;
			this.Append(value2, value.Length);
			char* ptr2 = null;
		}

		// Token: 0x06002CBF RID: 11455 RVA: 0x00155ED0 File Offset: 0x001550D0
		public unsafe StringBuilder Append([Nullable(2)] string value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (startIndex > value.Length - count)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
				}
				char* ptr;
				if (value == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* pinnableReference = value.GetPinnableReference())
					{
						ptr = pinnableReference;
					}
				}
				char* ptr2 = ptr;
				this.Append(ptr2 + startIndex, count);
				return this;
			}
		}

		// Token: 0x06002CC0 RID: 11456 RVA: 0x00155F5B File Offset: 0x0015515B
		public StringBuilder Append([Nullable(2)] StringBuilder value)
		{
			if (value != null && value.Length != 0)
			{
				return this.AppendCore(value, 0, value.Length);
			}
			return this;
		}

		// Token: 0x06002CC1 RID: 11457 RVA: 0x00155F78 File Offset: 0x00155178
		public StringBuilder Append([Nullable(2)] StringBuilder value, int startIndex, int count)
		{
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (value == null)
			{
				if (startIndex == 0 && count == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value");
			}
			else
			{
				if (count == 0)
				{
					return this;
				}
				if (count > value.Length - startIndex)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
				}
				return this.AppendCore(value, startIndex, count);
			}
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x00155FEC File Offset: 0x001551EC
		private StringBuilder AppendCore(StringBuilder value, int startIndex, int count)
		{
			if (value == this)
			{
				return this.Append(value.ToString(startIndex, count));
			}
			int num = this.Length + count;
			if (num > this.m_MaxCapacity)
			{
				throw new ArgumentOutOfRangeException("Capacity", SR.ArgumentOutOfRange_Capacity);
			}
			while (count > 0)
			{
				int num2 = Math.Min(this.m_ChunkChars.Length - this.m_ChunkLength, count);
				if (num2 == 0)
				{
					this.ExpandByABlock(count);
					num2 = Math.Min(this.m_ChunkChars.Length - this.m_ChunkLength, count);
				}
				value.CopyTo(startIndex, new Span<char>(this.m_ChunkChars, this.m_ChunkLength, num2), num2);
				this.m_ChunkLength += num2;
				startIndex += num2;
				count -= num2;
			}
			return this;
		}

		// Token: 0x06002CC3 RID: 11459 RVA: 0x0015609B File Offset: 0x0015529B
		public StringBuilder AppendLine()
		{
			return this.Append(Environment.NewLine);
		}

		// Token: 0x06002CC4 RID: 11460 RVA: 0x001560A8 File Offset: 0x001552A8
		public StringBuilder AppendLine([Nullable(2)] string value)
		{
			this.Append(value);
			return this.Append(Environment.NewLine);
		}

		// Token: 0x06002CC5 RID: 11461 RVA: 0x001560C0 File Offset: 0x001552C0
		public void CopyTo(int sourceIndex, char[] destination, int destinationIndex, int count)
		{
			if (destination == null)
			{
				throw new ArgumentNullException("destination");
			}
			if (destinationIndex < 0)
			{
				throw new ArgumentOutOfRangeException("destinationIndex", SR.Format(SR.ArgumentOutOfRange_MustBeNonNegNum, "destinationIndex"));
			}
			if (destinationIndex > destination.Length - count)
			{
				throw new ArgumentException(SR.ArgumentOutOfRange_OffsetOut);
			}
			this.CopyTo(sourceIndex, new Span<char>(destination).Slice(destinationIndex), count);
		}

		// Token: 0x06002CC6 RID: 11462 RVA: 0x00156128 File Offset: 0x00155328
		[NullableContext(0)]
		public void CopyTo(int sourceIndex, Span<char> destination, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.Arg_NegativeArgCount);
			}
			if (sourceIndex > this.Length)
			{
				throw new ArgumentOutOfRangeException("sourceIndex", SR.ArgumentOutOfRange_Index);
			}
			if (sourceIndex > this.Length - count)
			{
				throw new ArgumentException(SR.Arg_LongerThanSrcString);
			}
			StringBuilder stringBuilder = this;
			int num = sourceIndex + count;
			int num2 = count;
			while (count > 0)
			{
				int num3 = num - stringBuilder.m_ChunkOffset;
				if (num3 >= 0)
				{
					num3 = Math.Min(num3, stringBuilder.m_ChunkLength);
					int num4 = count;
					int num5 = num3 - count;
					if (num5 < 0)
					{
						num4 += num5;
						num5 = 0;
					}
					num2 -= num4;
					count -= num4;
					StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num5, destination, num2, num4);
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x001561DC File Offset: 0x001553DC
		public unsafe StringBuilder Insert(int index, [Nullable(2)] string value, int count)
		{
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (string.IsNullOrEmpty(value) || count == 0)
			{
				return this;
			}
			long num = (long)value.Length * (long)count;
			if (num > (long)(this.MaxCapacity - this.Length))
			{
				throw new OutOfMemoryException();
			}
			StringBuilder stringBuilder;
			int num2;
			this.MakeRoom(index, (int)num, out stringBuilder, out num2, false);
			char* ptr;
			if (value == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = value.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* value2 = ptr;
			while (count > 0)
			{
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num2, value2, value.Length);
				count--;
			}
			return this;
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x00156288 File Offset: 0x00155488
		public StringBuilder Remove(int startIndex, int length)
		{
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_NegativeLength);
			}
			if (startIndex < 0)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
			}
			if (length > this.Length - startIndex)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_Index);
			}
			if (this.Length == length && startIndex == 0)
			{
				this.Length = 0;
				return this;
			}
			if (length > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.Remove(startIndex, length, out stringBuilder, out num);
			}
			return this;
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x001562FE File Offset: 0x001554FE
		public StringBuilder Append(bool value)
		{
			return this.Append(value.ToString());
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x00156310 File Offset: 0x00155510
		public StringBuilder Append(char value)
		{
			int chunkLength = this.m_ChunkLength;
			char[] chunkChars = this.m_ChunkChars;
			if (chunkChars.Length > chunkLength)
			{
				chunkChars[chunkLength] = value;
				this.m_ChunkLength++;
			}
			else
			{
				this.Append(value, 1);
			}
			return this;
		}

		// Token: 0x06002CCB RID: 11467 RVA: 0x0015634F File Offset: 0x0015554F
		[CLSCompliant(false)]
		public StringBuilder Append(sbyte value)
		{
			return this.AppendSpanFormattable<sbyte>(value);
		}

		// Token: 0x06002CCC RID: 11468 RVA: 0x00156358 File Offset: 0x00155558
		public StringBuilder Append(byte value)
		{
			return this.AppendSpanFormattable<byte>(value);
		}

		// Token: 0x06002CCD RID: 11469 RVA: 0x00156361 File Offset: 0x00155561
		public StringBuilder Append(short value)
		{
			return this.AppendSpanFormattable<short>(value);
		}

		// Token: 0x06002CCE RID: 11470 RVA: 0x0015636A File Offset: 0x0015556A
		public StringBuilder Append(int value)
		{
			return this.AppendSpanFormattable<int>(value);
		}

		// Token: 0x06002CCF RID: 11471 RVA: 0x00156373 File Offset: 0x00155573
		public StringBuilder Append(long value)
		{
			return this.AppendSpanFormattable<long>(value);
		}

		// Token: 0x06002CD0 RID: 11472 RVA: 0x0015637C File Offset: 0x0015557C
		public StringBuilder Append(float value)
		{
			return this.AppendSpanFormattable<float>(value);
		}

		// Token: 0x06002CD1 RID: 11473 RVA: 0x00156385 File Offset: 0x00155585
		public StringBuilder Append(double value)
		{
			return this.AppendSpanFormattable<double>(value);
		}

		// Token: 0x06002CD2 RID: 11474 RVA: 0x0015638E File Offset: 0x0015558E
		public StringBuilder Append(decimal value)
		{
			return this.AppendSpanFormattable<decimal>(value);
		}

		// Token: 0x06002CD3 RID: 11475 RVA: 0x00156397 File Offset: 0x00155597
		[CLSCompliant(false)]
		public StringBuilder Append(ushort value)
		{
			return this.AppendSpanFormattable<ushort>(value);
		}

		// Token: 0x06002CD4 RID: 11476 RVA: 0x001563A0 File Offset: 0x001555A0
		[CLSCompliant(false)]
		public StringBuilder Append(uint value)
		{
			return this.AppendSpanFormattable<uint>(value);
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x001563A9 File Offset: 0x001555A9
		[CLSCompliant(false)]
		public StringBuilder Append(ulong value)
		{
			return this.AppendSpanFormattable<ulong>(value);
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x001563B4 File Offset: 0x001555B4
		private StringBuilder AppendSpanFormattable<T>(T value) where T : ISpanFormattable
		{
			int num;
			if (value.TryFormat(this.RemainingCurrentChunk, out num, default(ReadOnlySpan<char>), null))
			{
				this.m_ChunkLength += num;
				return this;
			}
			return this.Append(value.ToString());
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x00156408 File Offset: 0x00155608
		internal StringBuilder AppendSpanFormattable<T>(T value, string format, IFormatProvider provider) where T : ISpanFormattable, IFormattable
		{
			int num;
			if (value.TryFormat(this.RemainingCurrentChunk, out num, format, provider))
			{
				this.m_ChunkLength += num;
				return this;
			}
			return this.Append(value.ToString(format, provider));
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x00156458 File Offset: 0x00155658
		public StringBuilder Append([Nullable(2)] object value)
		{
			if (value != null)
			{
				return this.Append(value.ToString());
			}
			return this;
		}

		// Token: 0x06002CD9 RID: 11481 RVA: 0x0015646C File Offset: 0x0015566C
		public unsafe StringBuilder Append([Nullable(2)] char[] value)
		{
			if (value != null && value.Length != 0)
			{
				fixed (char* ptr = &value[0])
				{
					char* value2 = ptr;
					this.Append(value2, value.Length);
				}
			}
			return this;
		}

		// Token: 0x06002CDA RID: 11482 RVA: 0x0015649C File Offset: 0x0015569C
		[NullableContext(0)]
		[return: Nullable(1)]
		public unsafe StringBuilder Append(ReadOnlySpan<char> value)
		{
			if (value.Length > 0)
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(value))
				{
					char* value2 = reference;
					this.Append(value2, value.Length);
				}
			}
			return this;
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x001564D0 File Offset: 0x001556D0
		[NullableContext(0)]
		[return: Nullable(1)]
		public StringBuilder Append(ReadOnlyMemory<char> value)
		{
			return this.Append(value.Span);
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x001564E0 File Offset: 0x001556E0
		public unsafe StringBuilder AppendJoin([Nullable(2)] string separator, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] values)
		{
			if (separator == null)
			{
				separator = string.Empty;
			}
			char* ptr;
			if (separator == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = separator.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* separator2 = ptr;
			return this.AppendJoinCore<object>(separator2, separator.Length, values);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x00156518 File Offset: 0x00155718
		public unsafe StringBuilder AppendJoin<[Nullable(2)] T>([Nullable(2)] string separator, IEnumerable<T> values)
		{
			if (separator == null)
			{
				separator = string.Empty;
			}
			char* ptr;
			if (separator == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = separator.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* separator2 = ptr;
			return this.AppendJoinCore<T>(separator2, separator.Length, values);
		}

		// Token: 0x06002CDE RID: 11486 RVA: 0x00156550 File Offset: 0x00155750
		public unsafe StringBuilder AppendJoin([Nullable(2)] string separator, [Nullable(new byte[]
		{
			1,
			2
		})] params string[] values)
		{
			if (separator == null)
			{
				separator = string.Empty;
			}
			char* ptr;
			if (separator == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = separator.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* separator2 = ptr;
			return this.AppendJoinCore<string>(separator2, separator.Length, values);
		}

		// Token: 0x06002CDF RID: 11487 RVA: 0x00156586 File Offset: 0x00155786
		public unsafe StringBuilder AppendJoin(char separator, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] values)
		{
			return this.AppendJoinCore<object>(&separator, 1, values);
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x00156593 File Offset: 0x00155793
		public unsafe StringBuilder AppendJoin<[Nullable(2)] T>(char separator, IEnumerable<T> values)
		{
			return this.AppendJoinCore<T>(&separator, 1, values);
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x001565A0 File Offset: 0x001557A0
		public unsafe StringBuilder AppendJoin(char separator, [Nullable(new byte[]
		{
			1,
			2
		})] params string[] values)
		{
			return this.AppendJoinCore<string>(&separator, 1, values);
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x001565B0 File Offset: 0x001557B0
		private unsafe StringBuilder AppendJoinCore<T>(char* separator, int separatorLength, IEnumerable<T> values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.values);
			}
			using (IEnumerator<T> enumerator = values.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					return this;
				}
				T t = enumerator.Current;
				if (t != null)
				{
					this.Append(t.ToString());
				}
				while (enumerator.MoveNext())
				{
					this.Append(separator, separatorLength);
					t = enumerator.Current;
					if (t != null)
					{
						this.Append(t.ToString());
					}
				}
			}
			return this;
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x00156654 File Offset: 0x00155854
		private unsafe StringBuilder AppendJoinCore<T>(char* separator, int separatorLength, T[] values)
		{
			if (values == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.values);
			}
			if (values.Length == 0)
			{
				return this;
			}
			if (values[0] != null)
			{
				this.Append(values[0].ToString());
			}
			for (int i = 1; i < values.Length; i++)
			{
				this.Append(separator, separatorLength);
				if (values[i] != null)
				{
					this.Append(values[i].ToString());
				}
			}
			return this;
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x001566DC File Offset: 0x001558DC
		public unsafe StringBuilder Insert(int index, [Nullable(2)] string value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (value != null)
			{
				char* ptr;
				if (value == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = value.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value2 = ptr;
				this.Insert(index, value2, value.Length);
				char* ptr2 = null;
			}
			return this;
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x00156728 File Offset: 0x00155928
		public StringBuilder Insert(int index, bool value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x00156739 File Offset: 0x00155939
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, sbyte value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x0015674A File Offset: 0x0015594A
		public StringBuilder Insert(int index, byte value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x0015675B File Offset: 0x0015595B
		public StringBuilder Insert(int index, short value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x0015676C File Offset: 0x0015596C
		public unsafe StringBuilder Insert(int index, char value)
		{
			this.Insert(index, &value, 1);
			return this;
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x0015677A File Offset: 0x0015597A
		public StringBuilder Insert(int index, [Nullable(2)] char[] value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (value != null)
			{
				this.Insert(index, value, 0, value.Length);
			}
			return this;
		}

		// Token: 0x06002CEB RID: 11499 RVA: 0x001567A8 File Offset: 0x001559A8
		public unsafe StringBuilder Insert(int index, [Nullable(2)] char[] value, int startIndex, int charCount)
		{
			int length = this.Length;
			if (index > length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (value == null)
			{
				if (startIndex == 0 && charCount == 0)
				{
					return this;
				}
				throw new ArgumentNullException("value", SR.ArgumentNull_String);
			}
			else
			{
				if (startIndex < 0)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_StartIndex);
				}
				if (charCount < 0)
				{
					throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_GenericPositive);
				}
				if (startIndex > value.Length - charCount)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
				}
				if (charCount > 0)
				{
					fixed (char* ptr = &value[startIndex])
					{
						char* value2 = ptr;
						this.Insert(index, value2, charCount);
					}
				}
				return this;
			}
		}

		// Token: 0x06002CEC RID: 11500 RVA: 0x0015684C File Offset: 0x00155A4C
		public StringBuilder Insert(int index, int value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CED RID: 11501 RVA: 0x0015685D File Offset: 0x00155A5D
		public StringBuilder Insert(int index, long value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CEE RID: 11502 RVA: 0x0015686E File Offset: 0x00155A6E
		public StringBuilder Insert(int index, float value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CEF RID: 11503 RVA: 0x0015687F File Offset: 0x00155A7F
		public StringBuilder Insert(int index, double value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CF0 RID: 11504 RVA: 0x00156890 File Offset: 0x00155A90
		public StringBuilder Insert(int index, decimal value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CF1 RID: 11505 RVA: 0x001568A1 File Offset: 0x00155AA1
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ushort value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CF2 RID: 11506 RVA: 0x001568B2 File Offset: 0x00155AB2
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, uint value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CF3 RID: 11507 RVA: 0x001568C3 File Offset: 0x00155AC3
		[CLSCompliant(false)]
		public StringBuilder Insert(int index, ulong value)
		{
			return this.Insert(index, value.ToString(), 1);
		}

		// Token: 0x06002CF4 RID: 11508 RVA: 0x001568D4 File Offset: 0x00155AD4
		public StringBuilder Insert(int index, [Nullable(2)] object value)
		{
			if (value != null)
			{
				return this.Insert(index, value.ToString(), 1);
			}
			return this;
		}

		// Token: 0x06002CF5 RID: 11509 RVA: 0x001568EC File Offset: 0x00155AEC
		[NullableContext(0)]
		[return: Nullable(1)]
		public unsafe StringBuilder Insert(int index, ReadOnlySpan<char> value)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (value.Length > 0)
			{
				fixed (char* reference = MemoryMarshal.GetReference<char>(value))
				{
					char* value2 = reference;
					this.Insert(index, value2, value.Length);
				}
			}
			return this;
		}

		// Token: 0x06002CF6 RID: 11510 RVA: 0x00156939 File Offset: 0x00155B39
		public StringBuilder AppendFormat(string format, [Nullable(2)] object arg0)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0));
		}

		// Token: 0x06002CF7 RID: 11511 RVA: 0x00156949 File Offset: 0x00155B49
		public StringBuilder AppendFormat(string format, [Nullable(2)] object arg0, [Nullable(2)] object arg1)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x06002CF8 RID: 11512 RVA: 0x0015695A File Offset: 0x00155B5A
		[NullableContext(2)]
		[return: Nullable(1)]
		public StringBuilder AppendFormat([Nullable(1)] string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(null, format, new ParamsArray(arg0, arg1, arg2));
		}

		// Token: 0x06002CF9 RID: 11513 RVA: 0x00156970 File Offset: 0x00155B70
		public StringBuilder AppendFormat(string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			if (args == null)
			{
				string paramName = (format == null) ? "format" : "args";
				throw new ArgumentNullException(paramName);
			}
			return this.AppendFormatHelper(null, format, new ParamsArray(args));
		}

		// Token: 0x06002CFA RID: 11514 RVA: 0x001569A5 File Offset: 0x00155BA5
		public StringBuilder AppendFormat([Nullable(2)] IFormatProvider provider, string format, [Nullable(2)] object arg0)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0));
		}

		// Token: 0x06002CFB RID: 11515 RVA: 0x001569B5 File Offset: 0x00155BB5
		[NullableContext(2)]
		[return: Nullable(1)]
		public StringBuilder AppendFormat(IFormatProvider provider, [Nullable(1)] string format, object arg0, object arg1)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1));
		}

		// Token: 0x06002CFC RID: 11516 RVA: 0x001569C7 File Offset: 0x00155BC7
		[NullableContext(2)]
		[return: Nullable(1)]
		public StringBuilder AppendFormat(IFormatProvider provider, [Nullable(1)] string format, object arg0, object arg1, object arg2)
		{
			return this.AppendFormatHelper(provider, format, new ParamsArray(arg0, arg1, arg2));
		}

		// Token: 0x06002CFD RID: 11517 RVA: 0x001569DC File Offset: 0x00155BDC
		public StringBuilder AppendFormat([Nullable(2)] IFormatProvider provider, string format, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			if (args == null)
			{
				string paramName = (format == null) ? "format" : "args";
				throw new ArgumentNullException(paramName);
			}
			return this.AppendFormatHelper(provider, format, new ParamsArray(args));
		}

		// Token: 0x06002CFE RID: 11518 RVA: 0x00156A11 File Offset: 0x00155C11
		private static void FormatError()
		{
			throw new FormatException(SR.Format_InvalidString);
		}

		// Token: 0x06002CFF RID: 11519 RVA: 0x00156A20 File Offset: 0x00155C20
		internal StringBuilder AppendFormatHelper(IFormatProvider provider, string format, ParamsArray args)
		{
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			int num = 0;
			int length = format.Length;
			char c = '\0';
			ICustomFormatter customFormatter = null;
			if (provider != null)
			{
				customFormatter = (ICustomFormatter)provider.GetFormat(typeof(ICustomFormatter));
			}
			for (;;)
			{
				if (num < length)
				{
					c = format[num];
					num++;
					if (c == '}')
					{
						if (num < length && format[num] == '}')
						{
							num++;
						}
						else
						{
							StringBuilder.FormatError();
						}
					}
					else if (c == '{')
					{
						if (num >= length || format[num] != '{')
						{
							num--;
							goto IL_8F;
						}
						num++;
					}
					this.Append(c);
					continue;
				}
				IL_8F:
				if (num == length)
				{
					return this;
				}
				num++;
				if (num == length || (c = format[num]) < '0' || c > '9')
				{
					StringBuilder.FormatError();
				}
				int num2 = 0;
				do
				{
					num2 = num2 * 10 + (int)c - 48;
					num++;
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
				}
				while (c >= '0' && c <= '9' && num2 < 1000000);
				if (num2 >= args.Length)
				{
					break;
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				bool flag = false;
				int num3 = 0;
				if (c == ',')
				{
					num++;
					while (num < length && format[num] == ' ')
					{
						num++;
					}
					if (num == length)
					{
						StringBuilder.FormatError();
					}
					c = format[num];
					if (c == '-')
					{
						flag = true;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
					}
					if (c < '0' || c > '9')
					{
						StringBuilder.FormatError();
					}
					do
					{
						num3 = num3 * 10 + (int)c - 48;
						num++;
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						if (c < '0' || c > '9')
						{
							break;
						}
					}
					while (num3 < 1000000);
				}
				while (num < length && (c = format[num]) == ' ')
				{
					num++;
				}
				object obj = args[num2];
				ReadOnlySpan<char> readOnlySpan = default(ReadOnlySpan<char>);
				if (c == ':')
				{
					num++;
					int num4 = num;
					for (;;)
					{
						if (num == length)
						{
							StringBuilder.FormatError();
						}
						c = format[num];
						if (c == '}')
						{
							break;
						}
						if (c == '{')
						{
							StringBuilder.FormatError();
						}
						num++;
					}
					if (num > num4)
					{
						readOnlySpan = format.AsSpan(num4, num - num4);
					}
				}
				else if (c != '}')
				{
					StringBuilder.FormatError();
				}
				num++;
				string text = null;
				string text2 = null;
				if (customFormatter != null)
				{
					if (readOnlySpan.Length != 0)
					{
						text2 = new string(readOnlySpan);
					}
					text = customFormatter.Format(text2, obj, provider);
				}
				if (text == null)
				{
					ISpanFormattable spanFormattable = obj as ISpanFormattable;
					int num5;
					if (spanFormattable != null && (flag || num3 == 0) && spanFormattable.TryFormat(this.RemainingCurrentChunk, out num5, readOnlySpan, provider))
					{
						this.m_ChunkLength += num5;
						int num6 = num3 - num5;
						if (flag && num6 > 0)
						{
							this.Append(' ', num6);
							continue;
						}
						continue;
					}
					else
					{
						IFormattable formattable = obj as IFormattable;
						if (formattable != null)
						{
							if (readOnlySpan.Length != 0 && text2 == null)
							{
								text2 = new string(readOnlySpan);
							}
							text = formattable.ToString(text2, provider);
						}
						else if (obj != null)
						{
							text = obj.ToString();
						}
					}
				}
				if (text == null)
				{
					text = string.Empty;
				}
				int num7 = num3 - text.Length;
				if (!flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
				this.Append(text);
				if (flag && num7 > 0)
				{
					this.Append(' ', num7);
				}
			}
			throw new FormatException(SR.Format_IndexOutOfRange);
		}

		// Token: 0x06002D00 RID: 11520 RVA: 0x00156D7A File Offset: 0x00155F7A
		public StringBuilder Replace(string oldValue, [Nullable(2)] string newValue)
		{
			return this.Replace(oldValue, newValue, 0, this.Length);
		}

		// Token: 0x06002D01 RID: 11521 RVA: 0x00156D8C File Offset: 0x00155F8C
		[NullableContext(2)]
		public bool Equals(StringBuilder sb)
		{
			if (sb == null)
			{
				return false;
			}
			if (this.Length != sb.Length)
			{
				return false;
			}
			if (sb == this)
			{
				return true;
			}
			StringBuilder stringBuilder = this;
			int i = stringBuilder.m_ChunkLength;
			StringBuilder stringBuilder2 = sb;
			int j = stringBuilder2.m_ChunkLength;
			for (;;)
			{
				IL_2D:
				i--;
				j--;
				while (i < 0)
				{
					stringBuilder = stringBuilder.m_ChunkPrevious;
					if (stringBuilder != null)
					{
						i = stringBuilder.m_ChunkLength + i;
					}
					else
					{
						IL_63:
						while (j < 0)
						{
							stringBuilder2 = stringBuilder2.m_ChunkPrevious;
							if (stringBuilder2 == null)
							{
								break;
							}
							j = stringBuilder2.m_ChunkLength + j;
						}
						if (i < 0)
						{
							goto Block_7;
						}
						if (j < 0)
						{
							return false;
						}
						if (stringBuilder.m_ChunkChars[i] != stringBuilder2.m_ChunkChars[j])
						{
							return false;
						}
						goto IL_2D;
					}
				}
				goto IL_63;
			}
			Block_7:
			return j < 0;
		}

		// Token: 0x06002D02 RID: 11522 RVA: 0x00156E24 File Offset: 0x00156024
		[NullableContext(0)]
		public bool Equals(ReadOnlySpan<char> span)
		{
			if (span.Length != this.Length)
			{
				return false;
			}
			StringBuilder stringBuilder = this;
			int num = 0;
			for (;;)
			{
				int chunkLength = stringBuilder.m_ChunkLength;
				num += chunkLength;
				ReadOnlySpan<char> span2 = new ReadOnlySpan<char>(stringBuilder.m_ChunkChars, 0, chunkLength);
				if (!span2.EqualsOrdinal(span.Slice(span.Length - num, chunkLength)))
				{
					break;
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
				if (stringBuilder == null)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D03 RID: 11523 RVA: 0x00156E88 File Offset: 0x00156088
		public StringBuilder Replace(string oldValue, [Nullable(2)] string newValue, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Index);
			}
			if (oldValue == null)
			{
				throw new ArgumentNullException("oldValue");
			}
			if (oldValue.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "oldValue");
			}
			if (newValue == null)
			{
				newValue = string.Empty;
			}
			int[] array = null;
			int num = 0;
			StringBuilder stringBuilder = this.FindChunkForIndex(startIndex);
			int num2 = startIndex - stringBuilder.m_ChunkOffset;
			while (count > 0)
			{
				if (this.StartsWith(stringBuilder, num2, count, oldValue))
				{
					if (array == null)
					{
						array = new int[5];
					}
					else if (num >= array.Length)
					{
						Array.Resize<int>(ref array, array.Length * 3 / 2 + 4);
					}
					array[num++] = num2;
					num2 += oldValue.Length;
					count -= oldValue.Length;
				}
				else
				{
					num2++;
					count--;
				}
				if (num2 >= stringBuilder.m_ChunkLength || count == 0)
				{
					int num3 = num2 + stringBuilder.m_ChunkOffset;
					this.ReplaceAllInChunk(array, num, stringBuilder, oldValue.Length, newValue);
					num3 += (newValue.Length - oldValue.Length) * num;
					num = 0;
					stringBuilder = this.FindChunkForIndex(num3);
					num2 = num3 - stringBuilder.m_ChunkOffset;
				}
			}
			return this;
		}

		// Token: 0x06002D04 RID: 11524 RVA: 0x00156FD0 File Offset: 0x001561D0
		public StringBuilder Replace(char oldChar, char newChar)
		{
			return this.Replace(oldChar, newChar, 0, this.Length);
		}

		// Token: 0x06002D05 RID: 11525 RVA: 0x00156FE4 File Offset: 0x001561E4
		public StringBuilder Replace(char oldChar, char newChar, int startIndex, int count)
		{
			int length = this.Length;
			if (startIndex > length)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.ArgumentOutOfRange_Index);
			}
			if (count < 0 || startIndex > length - count)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_Index);
			}
			int num = startIndex + count;
			StringBuilder stringBuilder = this;
			for (;;)
			{
				int num2 = num - stringBuilder.m_ChunkOffset;
				int num3 = startIndex - stringBuilder.m_ChunkOffset;
				if (num2 >= 0)
				{
					int i = Math.Max(num3, 0);
					int num4 = Math.Min(stringBuilder.m_ChunkLength, num2);
					while (i < num4)
					{
						if (stringBuilder.m_ChunkChars[i] == oldChar)
						{
							stringBuilder.m_ChunkChars[i] = newChar;
						}
						i++;
					}
				}
				if (num3 >= 0)
				{
					break;
				}
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return this;
		}

		// Token: 0x06002D06 RID: 11526 RVA: 0x00157094 File Offset: 0x00156294
		[NullableContext(0)]
		[CLSCompliant(false)]
		[return: Nullable(1)]
		public unsafe StringBuilder Append(char* value, int valueCount)
		{
			if (valueCount < 0)
			{
				throw new ArgumentOutOfRangeException("valueCount", SR.ArgumentOutOfRange_NegativeCount);
			}
			int num = this.Length + valueCount;
			if (num > this.m_MaxCapacity || num < valueCount)
			{
				throw new ArgumentOutOfRangeException("valueCount", SR.ArgumentOutOfRange_LengthGreaterThanCapacity);
			}
			int num2 = valueCount + this.m_ChunkLength;
			if (num2 <= this.m_ChunkChars.Length)
			{
				StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, valueCount);
				this.m_ChunkLength = num2;
			}
			else
			{
				int num3 = this.m_ChunkChars.Length - this.m_ChunkLength;
				if (num3 > 0)
				{
					StringBuilder.ThreadSafeCopy(value, this.m_ChunkChars, this.m_ChunkLength, num3);
					this.m_ChunkLength = this.m_ChunkChars.Length;
				}
				int num4 = valueCount - num3;
				this.ExpandByABlock(num4);
				StringBuilder.ThreadSafeCopy(value + num3, this.m_ChunkChars, 0, num4);
				this.m_ChunkLength = num4;
			}
			return this;
		}

		// Token: 0x06002D07 RID: 11527 RVA: 0x00157168 File Offset: 0x00156368
		private unsafe void Insert(int index, char* value, int valueCount)
		{
			if (index > this.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (valueCount > 0)
			{
				StringBuilder stringBuilder;
				int num;
				this.MakeRoom(index, valueCount, out stringBuilder, out num, false);
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num, value, valueCount);
			}
		}

		// Token: 0x06002D08 RID: 11528 RVA: 0x001571AC File Offset: 0x001563AC
		private unsafe void ReplaceAllInChunk(int[] replacements, int replacementsCount, StringBuilder sourceChunk, int removeCount, string value)
		{
			if (replacementsCount <= 0)
			{
				return;
			}
			char* ptr;
			if (value == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = value.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* value2 = ptr;
			long num = (long)(value.Length - removeCount) * (long)replacementsCount;
			int num2 = (int)num;
			if ((long)num2 != num)
			{
				throw new OutOfMemoryException();
			}
			StringBuilder stringBuilder = sourceChunk;
			int num3 = replacements[0];
			if (num2 > 0)
			{
				this.MakeRoom(stringBuilder.m_ChunkOffset + num3, num2, out stringBuilder, out num3, true);
			}
			int num4 = 0;
			for (;;)
			{
				this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num3, value2, value.Length);
				int num5 = replacements[num4] + removeCount;
				num4++;
				if (num4 >= replacementsCount)
				{
					break;
				}
				int num6 = replacements[num4];
				if (num2 != 0)
				{
					fixed (char* ptr3 = &sourceChunk.m_ChunkChars[num5])
					{
						char* value3 = ptr3;
						this.ReplaceInPlaceAtChunk(ref stringBuilder, ref num3, value3, num6 - num5);
					}
				}
				else
				{
					num3 += num6 - num5;
				}
			}
			if (num2 < 0)
			{
				this.Remove(stringBuilder.m_ChunkOffset + num3, -num2, out stringBuilder, out num3);
			}
			char* ptr2 = null;
		}

		// Token: 0x06002D09 RID: 11529 RVA: 0x00157298 File Offset: 0x00156498
		private bool StartsWith(StringBuilder chunk, int indexInChunk, int count, string value)
		{
			for (int i = 0; i < value.Length; i++)
			{
				if (count == 0)
				{
					return false;
				}
				if (indexInChunk >= chunk.m_ChunkLength)
				{
					chunk = this.Next(chunk);
					if (chunk == null)
					{
						return false;
					}
					indexInChunk = 0;
				}
				if (value[i] != chunk.m_ChunkChars[indexInChunk])
				{
					return false;
				}
				indexInChunk++;
				count--;
			}
			return true;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x001572F8 File Offset: 0x001564F8
		private unsafe void ReplaceInPlaceAtChunk(ref StringBuilder chunk, ref int indexInChunk, char* value, int count)
		{
			if (count != 0)
			{
				for (;;)
				{
					int val = chunk.m_ChunkLength - indexInChunk;
					int num = Math.Min(val, count);
					StringBuilder.ThreadSafeCopy(value, chunk.m_ChunkChars, indexInChunk, num);
					indexInChunk += num;
					if (indexInChunk >= chunk.m_ChunkLength)
					{
						chunk = this.Next(chunk);
						indexInChunk = 0;
					}
					count -= num;
					if (count == 0)
					{
						break;
					}
					value += num;
				}
			}
		}

		// Token: 0x06002D0B RID: 11531 RVA: 0x00157360 File Offset: 0x00156560
		private unsafe static void ThreadSafeCopy(char* sourcePtr, char[] destination, int destinationIndex, int count)
		{
			if (count <= 0)
			{
				return;
			}
			if (destinationIndex <= destination.Length && destinationIndex + count <= destination.Length)
			{
				fixed (char* ptr = &destination[destinationIndex])
				{
					char* dmem = ptr;
					string.wstrcpy(dmem, sourcePtr, count);
				}
				return;
			}
			throw new ArgumentOutOfRangeException("destinationIndex", SR.ArgumentOutOfRange_Index);
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x001573A8 File Offset: 0x001565A8
		private unsafe static void ThreadSafeCopy(char[] source, int sourceIndex, Span<char> destination, int destinationIndex, int count)
		{
			if (count > 0)
			{
				if (sourceIndex > source.Length || count > source.Length - sourceIndex)
				{
					throw new ArgumentOutOfRangeException("sourceIndex", SR.ArgumentOutOfRange_Index);
				}
				if (destinationIndex > destination.Length || count > destination.Length - destinationIndex)
				{
					throw new ArgumentOutOfRangeException("destinationIndex", SR.ArgumentOutOfRange_Index);
				}
				fixed (char* ptr = &source[sourceIndex])
				{
					char* smem = ptr;
					fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
					{
						char* ptr2 = reference;
						string.wstrcpy(ptr2 + destinationIndex, smem, count);
					}
				}
			}
		}

		// Token: 0x06002D0D RID: 11533 RVA: 0x0015742C File Offset: 0x0015662C
		private StringBuilder FindChunkForIndex(int index)
		{
			StringBuilder stringBuilder = this;
			while (stringBuilder.m_ChunkOffset > index)
			{
				stringBuilder = stringBuilder.m_ChunkPrevious;
			}
			return stringBuilder;
		}

		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x0015744E File Offset: 0x0015664E
		[Nullable(0)]
		private Span<char> RemainingCurrentChunk
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return new Span<char>(this.m_ChunkChars, this.m_ChunkLength, this.m_ChunkChars.Length - this.m_ChunkLength);
			}
		}

		// Token: 0x06002D0F RID: 11535 RVA: 0x00157470 File Offset: 0x00156670
		private StringBuilder Next(StringBuilder chunk)
		{
			if (chunk != this)
			{
				return this.FindChunkForIndex(chunk.m_ChunkOffset + chunk.m_ChunkLength);
			}
			return null;
		}

		// Token: 0x06002D10 RID: 11536 RVA: 0x0015748C File Offset: 0x0015668C
		private void ExpandByABlock(int minBlockCharCount)
		{
			if (minBlockCharCount + this.Length > this.m_MaxCapacity || minBlockCharCount + this.Length < minBlockCharCount)
			{
				throw new ArgumentOutOfRangeException("requiredLength", SR.ArgumentOutOfRange_SmallCapacity);
			}
			int num = Math.Max(minBlockCharCount, Math.Min(this.Length, 8000));
			if (this.m_ChunkOffset + this.m_ChunkLength + num < num)
			{
				throw new OutOfMemoryException();
			}
			char[] chunkChars = GC.AllocateUninitializedArray<char>(num, false);
			this.m_ChunkPrevious = new StringBuilder(this);
			this.m_ChunkOffset += this.m_ChunkLength;
			this.m_ChunkLength = 0;
			this.m_ChunkChars = chunkChars;
		}

		// Token: 0x06002D11 RID: 11537 RVA: 0x00157528 File Offset: 0x00156728
		private StringBuilder(StringBuilder from)
		{
			this.m_ChunkLength = from.m_ChunkLength;
			this.m_ChunkOffset = from.m_ChunkOffset;
			this.m_ChunkChars = from.m_ChunkChars;
			this.m_ChunkPrevious = from.m_ChunkPrevious;
			this.m_MaxCapacity = from.m_MaxCapacity;
		}

		// Token: 0x06002D12 RID: 11538 RVA: 0x00157578 File Offset: 0x00156778
		private unsafe void MakeRoom(int index, int count, out StringBuilder chunk, out int indexInChunk, bool doNotMoveFollowingChars)
		{
			if (count + this.Length > this.m_MaxCapacity || count + this.Length < count)
			{
				throw new ArgumentOutOfRangeException("requiredLength", SR.ArgumentOutOfRange_SmallCapacity);
			}
			chunk = this;
			while (chunk.m_ChunkOffset > index)
			{
				chunk.m_ChunkOffset += count;
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = index - chunk.m_ChunkOffset;
			if (!doNotMoveFollowingChars && chunk.m_ChunkLength <= 32 && chunk.m_ChunkChars.Length - chunk.m_ChunkLength >= count)
			{
				int i = chunk.m_ChunkLength;
				while (i > indexInChunk)
				{
					i--;
					chunk.m_ChunkChars[i + count] = chunk.m_ChunkChars[i];
				}
				chunk.m_ChunkLength += count;
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(Math.Max(count, 16), chunk.m_MaxCapacity, chunk.m_ChunkPrevious);
			stringBuilder.m_ChunkLength = count;
			int num = Math.Min(count, indexInChunk);
			if (num > 0)
			{
				fixed (char* ptr = &chunk.m_ChunkChars[0])
				{
					char* ptr2 = ptr;
					StringBuilder.ThreadSafeCopy(ptr2, stringBuilder.m_ChunkChars, 0, num);
					int num2 = indexInChunk - num;
					if (num2 >= 0)
					{
						StringBuilder.ThreadSafeCopy(ptr2 + num, chunk.m_ChunkChars, 0, num2);
						indexInChunk = num2;
					}
				}
			}
			chunk.m_ChunkPrevious = stringBuilder;
			chunk.m_ChunkOffset += count;
			if (num < count)
			{
				chunk = stringBuilder;
				indexInChunk = num;
			}
		}

		// Token: 0x06002D13 RID: 11539 RVA: 0x001576DC File Offset: 0x001568DC
		private StringBuilder(int size, int maxCapacity, StringBuilder previousBlock)
		{
			this.m_ChunkChars = GC.AllocateUninitializedArray<char>(size, false);
			this.m_MaxCapacity = maxCapacity;
			this.m_ChunkPrevious = previousBlock;
			if (previousBlock != null)
			{
				this.m_ChunkOffset = previousBlock.m_ChunkOffset + previousBlock.m_ChunkLength;
			}
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x00157718 File Offset: 0x00156918
		private void Remove(int startIndex, int count, out StringBuilder chunk, out int indexInChunk)
		{
			int num = startIndex + count;
			chunk = this;
			StringBuilder stringBuilder = null;
			int num2 = 0;
			for (;;)
			{
				if (num - chunk.m_ChunkOffset >= 0)
				{
					if (stringBuilder == null)
					{
						stringBuilder = chunk;
						num2 = num - stringBuilder.m_ChunkOffset;
					}
					if (startIndex - chunk.m_ChunkOffset >= 0)
					{
						break;
					}
				}
				else
				{
					chunk.m_ChunkOffset -= count;
				}
				chunk = chunk.m_ChunkPrevious;
			}
			indexInChunk = startIndex - chunk.m_ChunkOffset;
			int num3 = indexInChunk;
			int count2 = stringBuilder.m_ChunkLength - num2;
			if (stringBuilder != chunk)
			{
				num3 = 0;
				chunk.m_ChunkLength = indexInChunk;
				stringBuilder.m_ChunkPrevious = chunk;
				stringBuilder.m_ChunkOffset = chunk.m_ChunkOffset + chunk.m_ChunkLength;
				if (indexInChunk == 0)
				{
					stringBuilder.m_ChunkPrevious = chunk.m_ChunkPrevious;
					chunk = stringBuilder;
				}
			}
			stringBuilder.m_ChunkLength -= num2 - num3;
			if (num3 != num2)
			{
				StringBuilder.ThreadSafeCopy(stringBuilder.m_ChunkChars, num2, stringBuilder.m_ChunkChars, num3, count2);
			}
		}

		// Token: 0x04000C7A RID: 3194
		internal char[] m_ChunkChars;

		// Token: 0x04000C7B RID: 3195
		internal StringBuilder m_ChunkPrevious;

		// Token: 0x04000C7C RID: 3196
		internal int m_ChunkLength;

		// Token: 0x04000C7D RID: 3197
		internal int m_ChunkOffset;

		// Token: 0x04000C7E RID: 3198
		internal int m_MaxCapacity;

		// Token: 0x02000356 RID: 854
		[NullableContext(0)]
		public struct ChunkEnumerator
		{
			// Token: 0x06002D15 RID: 11541 RVA: 0x001577FE File Offset: 0x001569FE
			[EditorBrowsable(EditorBrowsableState.Never)]
			public StringBuilder.ChunkEnumerator GetEnumerator()
			{
				return this;
			}

			// Token: 0x06002D16 RID: 11542 RVA: 0x00157808 File Offset: 0x00156A08
			public bool MoveNext()
			{
				if (this._currentChunk == this._firstChunk)
				{
					return false;
				}
				if (this._manyChunks != null)
				{
					return this._manyChunks.MoveNext(ref this._currentChunk);
				}
				StringBuilder stringBuilder = this._firstChunk;
				while (stringBuilder.m_ChunkPrevious != this._currentChunk)
				{
					stringBuilder = stringBuilder.m_ChunkPrevious;
				}
				this._currentChunk = stringBuilder;
				return true;
			}

			// Token: 0x17000909 RID: 2313
			// (get) Token: 0x06002D17 RID: 11543 RVA: 0x00157865 File Offset: 0x00156A65
			public ReadOnlyMemory<char> Current
			{
				get
				{
					if (this._currentChunk == null)
					{
						ThrowHelper.ThrowInvalidOperationException_InvalidOperation_EnumOpCantHappen();
					}
					return new ReadOnlyMemory<char>(this._currentChunk.m_ChunkChars, 0, this._currentChunk.m_ChunkLength);
				}
			}

			// Token: 0x06002D18 RID: 11544 RVA: 0x00157890 File Offset: 0x00156A90
			internal ChunkEnumerator(StringBuilder stringBuilder)
			{
				this._firstChunk = stringBuilder;
				this._currentChunk = null;
				this._manyChunks = null;
				int num = StringBuilder.ChunkEnumerator.ChunkCount(stringBuilder);
				if (8 < num)
				{
					this._manyChunks = new StringBuilder.ChunkEnumerator.ManyChunkInfo(stringBuilder, num);
				}
			}

			// Token: 0x06002D19 RID: 11545 RVA: 0x001578CC File Offset: 0x00156ACC
			private static int ChunkCount(StringBuilder stringBuilder)
			{
				int num = 0;
				while (stringBuilder != null)
				{
					num++;
					stringBuilder = stringBuilder.m_ChunkPrevious;
				}
				return num;
			}

			// Token: 0x04000C7F RID: 3199
			private readonly StringBuilder _firstChunk;

			// Token: 0x04000C80 RID: 3200
			private StringBuilder _currentChunk;

			// Token: 0x04000C81 RID: 3201
			private readonly StringBuilder.ChunkEnumerator.ManyChunkInfo _manyChunks;

			// Token: 0x02000357 RID: 855
			private class ManyChunkInfo
			{
				// Token: 0x06002D1A RID: 11546 RVA: 0x001578F0 File Offset: 0x00156AF0
				public bool MoveNext(ref StringBuilder current)
				{
					int num = this._chunkPos + 1;
					this._chunkPos = num;
					int num2 = num;
					if (this._chunks.Length <= num2)
					{
						return false;
					}
					current = this._chunks[num2];
					return true;
				}

				// Token: 0x06002D1B RID: 11547 RVA: 0x00157927 File Offset: 0x00156B27
				public ManyChunkInfo(StringBuilder stringBuilder, int chunkCount)
				{
					this._chunks = new StringBuilder[chunkCount];
					while (0 <= --chunkCount)
					{
						this._chunks[chunkCount] = stringBuilder;
						stringBuilder = stringBuilder.m_ChunkPrevious;
					}
					this._chunkPos = -1;
				}

				// Token: 0x04000C82 RID: 3202
				private readonly StringBuilder[] _chunks;

				// Token: 0x04000C83 RID: 3203
				private int _chunkPos;
			}
		}
	}
}
