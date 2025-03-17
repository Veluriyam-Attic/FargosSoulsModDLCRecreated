using System;
using System.Collections.Generic;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000767 RID: 1895
	internal sealed class NameInfo : ConcurrentSetItem<KeyValuePair<string, EventTags>, NameInfo>
	{
		// Token: 0x06005CB9 RID: 23737 RVA: 0x001C203C File Offset: 0x001C123C
		internal static void ReserveEventIDsBelow(int eventId)
		{
			int num;
			int num2;
			do
			{
				num = NameInfo.lastIdentity;
				num2 = (NameInfo.lastIdentity & -16777216) + eventId;
				num2 = Math.Max(num2, num);
			}
			while (Interlocked.CompareExchange(ref NameInfo.lastIdentity, num2, num) != num);
		}

		// Token: 0x06005CBA RID: 23738 RVA: 0x001C2074 File Offset: 0x001C1274
		public NameInfo(string name, EventTags tags, int typeMetadataSize)
		{
			this.name = name;
			this.tags = (tags & (EventTags)268435455);
			this.identity = Interlocked.Increment(ref NameInfo.lastIdentity);
			int prefixSize = 0;
			Statics.EncodeTags((int)this.tags, ref prefixSize, null);
			this.nameMetadata = Statics.MetadataForString(name, prefixSize, 0, typeMetadataSize);
			prefixSize = 2;
			Statics.EncodeTags((int)this.tags, ref prefixSize, this.nameMetadata);
		}

		// Token: 0x06005CBB RID: 23739 RVA: 0x001C20DF File Offset: 0x001C12DF
		public override int Compare(NameInfo other)
		{
			return this.Compare(other.name, other.tags);
		}

		// Token: 0x06005CBC RID: 23740 RVA: 0x001C20F3 File Offset: 0x001C12F3
		public override int Compare(KeyValuePair<string, EventTags> key)
		{
			return this.Compare(key.Key, key.Value & (EventTags)268435455);
		}

		// Token: 0x06005CBD RID: 23741 RVA: 0x001C2110 File Offset: 0x001C1310
		private int Compare(string otherName, EventTags otherTags)
		{
			int num = StringComparer.Ordinal.Compare(this.name, otherName);
			if (num == 0 && this.tags != otherTags)
			{
				num = ((this.tags < otherTags) ? -1 : 1);
			}
			return num;
		}

		// Token: 0x06005CBE RID: 23742 RVA: 0x001C214C File Offset: 0x001C134C
		public unsafe IntPtr GetOrCreateEventHandle(EventProvider provider, TraceLoggingEventHandleTable eventHandleTable, EventDescriptor descriptor, TraceLoggingEventTypes eventTypes)
		{
			IntPtr intPtr;
			if ((intPtr = eventHandleTable[descriptor.EventId]) == IntPtr.Zero)
			{
				lock (eventHandleTable)
				{
					if ((intPtr = eventHandleTable[descriptor.EventId]) == IntPtr.Zero)
					{
						byte[] array = EventPipeMetadataGenerator.Instance.GenerateEventMetadata(descriptor.EventId, this.name, (EventKeywords)descriptor.Keywords, (EventLevel)descriptor.Level, (uint)descriptor.Version, (EventOpcode)descriptor.Opcode, eventTypes);
						uint metadataLength = (uint)((array != null) ? array.Length : 0);
						byte[] array2;
						byte* pMetadata;
						if ((array2 = array) == null || array2.Length == 0)
						{
							pMetadata = null;
						}
						else
						{
							pMetadata = &array2[0];
						}
						intPtr = provider.m_eventProvider.DefineEventHandle((uint)descriptor.EventId, this.name, descriptor.Keywords, (uint)descriptor.Version, (uint)descriptor.Level, pMetadata, metadataLength);
						array2 = null;
						eventHandleTable.SetEventHandle(descriptor.EventId, intPtr);
					}
				}
			}
			return intPtr;
		}

		// Token: 0x04001BE3 RID: 7139
		private static int lastIdentity = 184549376;

		// Token: 0x04001BE4 RID: 7140
		internal readonly string name;

		// Token: 0x04001BE5 RID: 7141
		internal readonly EventTags tags;

		// Token: 0x04001BE6 RID: 7142
		internal readonly int identity;

		// Token: 0x04001BE7 RID: 7143
		internal readonly byte[] nameMetadata;
	}
}
