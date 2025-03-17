using System;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000765 RID: 1893
	internal class FieldMetadata
	{
		// Token: 0x06005CB1 RID: 23729 RVA: 0x001C1C96 File Offset: 0x001C0E96
		public FieldMetadata(string name, TraceLoggingDataType type, EventFieldTags tags, bool variableCount) : this(name, type, tags, variableCount ? 64 : 0, 0, null)
		{
		}

		// Token: 0x06005CB2 RID: 23730 RVA: 0x001C1CAC File Offset: 0x001C0EAC
		private FieldMetadata(string name, TraceLoggingDataType dataType, EventFieldTags tags, byte countFlags, ushort fixedCount = 0, byte[] custom = null)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name", "This usually means that the object passed to Write is of a type that does not support being used as the top-level object in an event, e.g. a primitive or built-in type.");
			}
			Statics.CheckName(name);
			int num = (int)(dataType & (TraceLoggingDataType)31);
			this.name = name;
			this.nameSize = Encoding.UTF8.GetByteCount(this.name) + 1;
			this.inType = (byte)(num | (int)countFlags);
			this.outType = (byte)(dataType >> 8 & (TraceLoggingDataType)127);
			this.tags = tags;
			this.fixedCount = fixedCount;
			this.custom = custom;
			if (countFlags != 0)
			{
				if (num == 0)
				{
					throw new NotSupportedException(SR.EventSource_NotSupportedArrayOfNil);
				}
				if (num == 14)
				{
					throw new NotSupportedException(SR.EventSource_NotSupportedArrayOfBinary);
				}
				if (num == 1 || num == 2)
				{
					throw new NotSupportedException(SR.EventSource_NotSupportedArrayOfNullTerminatedString);
				}
			}
			if ((this.tags & (EventFieldTags)268435455) != EventFieldTags.None)
			{
				this.outType |= 128;
			}
			if (this.outType != 0)
			{
				this.inType |= 128;
			}
		}

		// Token: 0x06005CB3 RID: 23731 RVA: 0x001C1D9C File Offset: 0x001C0F9C
		public void IncrementStructFieldCount()
		{
			this.inType |= 128;
			this.outType += 1;
			if ((this.outType & 127) == 0)
			{
				throw new NotSupportedException(SR.EventSource_TooManyFields);
			}
		}

		// Token: 0x06005CB4 RID: 23732 RVA: 0x001C1DD8 File Offset: 0x001C0FD8
		public void Encode(ref int pos, byte[] metadata)
		{
			if (metadata != null)
			{
				Encoding.UTF8.GetBytes(this.name, 0, this.name.Length, metadata, pos);
			}
			pos += this.nameSize;
			if (metadata != null)
			{
				metadata[pos] = this.inType;
			}
			pos++;
			if ((this.inType & 128) != 0)
			{
				if (metadata != null)
				{
					metadata[pos] = this.outType;
				}
				pos++;
				if ((this.outType & 128) != 0)
				{
					Statics.EncodeTags((int)this.tags, ref pos, metadata);
				}
			}
			if ((this.inType & 32) != 0)
			{
				if (metadata != null)
				{
					metadata[pos] = (byte)this.fixedCount;
					metadata[pos + 1] = (byte)(this.fixedCount >> 8);
				}
				pos += 2;
				if (96 == (this.inType & 96) && this.fixedCount != 0)
				{
					if (metadata != null)
					{
						Buffer.BlockCopy(this.custom, 0, metadata, pos, (int)this.fixedCount);
					}
					pos += (int)this.fixedCount;
				}
			}
		}

		// Token: 0x04001BDB RID: 7131
		private readonly string name;

		// Token: 0x04001BDC RID: 7132
		private readonly int nameSize;

		// Token: 0x04001BDD RID: 7133
		private readonly EventFieldTags tags;

		// Token: 0x04001BDE RID: 7134
		private readonly byte[] custom;

		// Token: 0x04001BDF RID: 7135
		private readonly ushort fixedCount;

		// Token: 0x04001BE0 RID: 7136
		private byte inType;

		// Token: 0x04001BE1 RID: 7137
		private byte outType;
	}
}
