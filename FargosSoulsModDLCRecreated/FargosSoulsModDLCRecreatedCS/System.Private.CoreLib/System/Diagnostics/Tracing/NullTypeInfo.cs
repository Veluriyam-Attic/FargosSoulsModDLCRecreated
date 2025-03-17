using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000785 RID: 1925
	internal sealed class NullTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005D26 RID: 23846 RVA: 0x001C33F4 File Offset: 0x001C25F4
		public NullTypeInfo() : base(typeof(EmptyStruct))
		{
		}

		// Token: 0x06005D27 RID: 23847 RVA: 0x001C3406 File Offset: 0x001C2606
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddGroup(name);
		}

		// Token: 0x06005D28 RID: 23848 RVA: 0x000AB30B File Offset: 0x000AA50B
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
		}

		// Token: 0x06005D29 RID: 23849 RVA: 0x000C26FD File Offset: 0x000C18FD
		public override object GetData(object value)
		{
			return null;
		}
	}
}
