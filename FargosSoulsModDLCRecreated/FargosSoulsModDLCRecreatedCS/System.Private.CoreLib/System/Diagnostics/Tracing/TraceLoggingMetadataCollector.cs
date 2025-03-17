using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000796 RID: 1942
	internal class TraceLoggingMetadataCollector
	{
		// Token: 0x06005D92 RID: 23954 RVA: 0x001C4804 File Offset: 0x001C3A04
		internal TraceLoggingMetadataCollector()
		{
			this.impl = new TraceLoggingMetadataCollector.Impl();
		}

		// Token: 0x06005D93 RID: 23955 RVA: 0x001C4822 File Offset: 0x001C3A22
		private TraceLoggingMetadataCollector(TraceLoggingMetadataCollector other, FieldMetadata group)
		{
			this.impl = other.impl;
			this.currentGroup = group;
		}

		// Token: 0x17000F30 RID: 3888
		// (get) Token: 0x06005D94 RID: 23956 RVA: 0x001C4848 File Offset: 0x001C3A48
		// (set) Token: 0x06005D95 RID: 23957 RVA: 0x001C4850 File Offset: 0x001C3A50
		internal EventFieldTags Tags { get; set; }

		// Token: 0x17000F31 RID: 3889
		// (get) Token: 0x06005D96 RID: 23958 RVA: 0x001C4859 File Offset: 0x001C3A59
		internal int ScratchSize
		{
			get
			{
				return (int)this.impl.scratchSize;
			}
		}

		// Token: 0x17000F32 RID: 3890
		// (get) Token: 0x06005D97 RID: 23959 RVA: 0x001C4866 File Offset: 0x001C3A66
		internal int DataCount
		{
			get
			{
				return (int)this.impl.dataCount;
			}
		}

		// Token: 0x17000F33 RID: 3891
		// (get) Token: 0x06005D98 RID: 23960 RVA: 0x001C4873 File Offset: 0x001C3A73
		internal int PinCount
		{
			get
			{
				return (int)this.impl.pinCount;
			}
		}

		// Token: 0x17000F34 RID: 3892
		// (get) Token: 0x06005D99 RID: 23961 RVA: 0x001C4880 File Offset: 0x001C3A80
		private bool BeginningBufferedArray
		{
			get
			{
				return this.bufferedArrayFieldCount == 0;
			}
		}

		// Token: 0x06005D9A RID: 23962 RVA: 0x001C488C File Offset: 0x001C3A8C
		public TraceLoggingMetadataCollector AddGroup(string name)
		{
			TraceLoggingMetadataCollector result = this;
			if (name != null || this.BeginningBufferedArray)
			{
				FieldMetadata fieldMetadata = new FieldMetadata(name, TraceLoggingDataType.Struct, this.Tags, this.BeginningBufferedArray);
				this.AddField(fieldMetadata);
				result = new TraceLoggingMetadataCollector(this, fieldMetadata);
			}
			return result;
		}

		// Token: 0x06005D9B RID: 23963 RVA: 0x001C48CC File Offset: 0x001C3ACC
		public void AddScalar(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			int size;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
				break;
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
				goto IL_6F;
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.HexInt32:
				size = 4;
				goto IL_8B;
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt64:
				size = 8;
				goto IL_8B;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case (TraceLoggingDataType)19:
				goto IL_80;
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.SystemTime:
				size = 16;
				goto IL_8B;
			default:
				if (traceLoggingDataType != TraceLoggingDataType.Char8)
				{
					if (traceLoggingDataType != TraceLoggingDataType.Char16)
					{
						goto IL_80;
					}
					goto IL_6F;
				}
				break;
			}
			size = 1;
			goto IL_8B;
			IL_6F:
			size = 2;
			goto IL_8B;
			IL_80:
			throw new ArgumentOutOfRangeException("type");
			IL_8B:
			this.impl.AddScalar(size);
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x06005D9C RID: 23964 RVA: 0x001C498C File Offset: 0x001C3B8C
		public void AddNullTerminatedString(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			if (traceLoggingDataType != TraceLoggingDataType.Utf16String)
			{
				throw new ArgumentOutOfRangeException("type");
			}
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, this.BeginningBufferedArray));
		}

		// Token: 0x06005D9D RID: 23965 RVA: 0x001C49D4 File Offset: 0x001C3BD4
		public void AddArray(string name, TraceLoggingDataType type)
		{
			TraceLoggingDataType traceLoggingDataType = type & (TraceLoggingDataType)31;
			switch (traceLoggingDataType)
			{
			case TraceLoggingDataType.Int8:
			case TraceLoggingDataType.UInt8:
			case TraceLoggingDataType.Int16:
			case TraceLoggingDataType.UInt16:
			case TraceLoggingDataType.Int32:
			case TraceLoggingDataType.UInt32:
			case TraceLoggingDataType.Int64:
			case TraceLoggingDataType.UInt64:
			case TraceLoggingDataType.Float:
			case TraceLoggingDataType.Double:
			case TraceLoggingDataType.Boolean32:
			case TraceLoggingDataType.Guid:
			case TraceLoggingDataType.FileTime:
			case TraceLoggingDataType.HexInt32:
			case TraceLoggingDataType.HexInt64:
				goto IL_74;
			case TraceLoggingDataType.Binary:
			case (TraceLoggingDataType)16:
			case TraceLoggingDataType.SystemTime:
			case (TraceLoggingDataType)19:
				break;
			default:
				if (traceLoggingDataType == TraceLoggingDataType.Char8 || traceLoggingDataType == TraceLoggingDataType.Char16)
				{
					goto IL_74;
				}
				break;
			}
			throw new ArgumentOutOfRangeException("type");
			IL_74:
			if (this.BeginningBufferedArray)
			{
				throw new NotSupportedException(SR.EventSource_NotSupportedNestedArraysEnums);
			}
			this.impl.AddScalar(2);
			this.impl.AddNonscalar();
			this.AddField(new FieldMetadata(name, type, this.Tags, true));
		}

		// Token: 0x06005D9E RID: 23966 RVA: 0x001C4A93 File Offset: 0x001C3C93
		public void BeginBufferedArray()
		{
			if (this.bufferedArrayFieldCount >= 0)
			{
				throw new NotSupportedException(SR.EventSource_NotSupportedNestedArraysEnums);
			}
			this.bufferedArrayFieldCount = 0;
			this.impl.BeginBuffered();
		}

		// Token: 0x06005D9F RID: 23967 RVA: 0x001C4ABB File Offset: 0x001C3CBB
		public void EndBufferedArray()
		{
			if (this.bufferedArrayFieldCount != 1)
			{
				throw new InvalidOperationException(SR.EventSource_IncorrentlyAuthoredTypeInfo);
			}
			this.bufferedArrayFieldCount = int.MinValue;
			this.impl.EndBuffered();
		}

		// Token: 0x06005DA0 RID: 23968 RVA: 0x001C4AE8 File Offset: 0x001C3CE8
		internal byte[] GetMetadata()
		{
			int num = this.impl.Encode(null);
			byte[] array = new byte[num];
			this.impl.Encode(array);
			return array;
		}

		// Token: 0x06005DA1 RID: 23969 RVA: 0x001C4B17 File Offset: 0x001C3D17
		private void AddField(FieldMetadata fieldMetadata)
		{
			this.Tags = EventFieldTags.None;
			this.bufferedArrayFieldCount++;
			this.impl.fields.Add(fieldMetadata);
			if (this.currentGroup != null)
			{
				this.currentGroup.IncrementStructFieldCount();
			}
		}

		// Token: 0x04001C74 RID: 7284
		private readonly TraceLoggingMetadataCollector.Impl impl;

		// Token: 0x04001C75 RID: 7285
		private readonly FieldMetadata currentGroup;

		// Token: 0x04001C76 RID: 7286
		private int bufferedArrayFieldCount = int.MinValue;

		// Token: 0x02000797 RID: 1943
		private class Impl
		{
			// Token: 0x06005DA2 RID: 23970 RVA: 0x001C4B52 File Offset: 0x001C3D52
			public void AddScalar(int size)
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						if (!this.scalar)
						{
							this.dataCount += 1;
						}
						this.scalar = true;
						this.scratchSize = (short)((int)this.scratchSize + size);
					}
				}
			}

			// Token: 0x06005DA3 RID: 23971 RVA: 0x001C4B89 File Offset: 0x001C3D89
			public void AddNonscalar()
			{
				checked
				{
					if (this.bufferNesting == 0)
					{
						this.scalar = false;
						this.pinCount += 1;
						this.dataCount += 1;
					}
				}
			}

			// Token: 0x06005DA4 RID: 23972 RVA: 0x001C4BB8 File Offset: 0x001C3DB8
			public void BeginBuffered()
			{
				if (this.bufferNesting == 0)
				{
					this.AddNonscalar();
				}
				this.bufferNesting++;
			}

			// Token: 0x06005DA5 RID: 23973 RVA: 0x001C4BD6 File Offset: 0x001C3DD6
			public void EndBuffered()
			{
				this.bufferNesting--;
			}

			// Token: 0x06005DA6 RID: 23974 RVA: 0x001C4BE8 File Offset: 0x001C3DE8
			public int Encode(byte[] metadata)
			{
				int result = 0;
				foreach (FieldMetadata fieldMetadata in this.fields)
				{
					fieldMetadata.Encode(ref result, metadata);
				}
				return result;
			}

			// Token: 0x04001C78 RID: 7288
			internal readonly List<FieldMetadata> fields = new List<FieldMetadata>();

			// Token: 0x04001C79 RID: 7289
			internal short scratchSize;

			// Token: 0x04001C7A RID: 7290
			internal sbyte dataCount;

			// Token: 0x04001C7B RID: 7291
			internal sbyte pinCount;

			// Token: 0x04001C7C RID: 7292
			private int bufferNesting;

			// Token: 0x04001C7D RID: 7293
			private bool scalar;
		}
	}
}
