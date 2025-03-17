using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200078D RID: 1933
	internal sealed class TimeSpanTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005D5E RID: 23902 RVA: 0x001C39F8 File Offset: 0x001C2BF8
		public TimeSpanTypeInfo() : base(typeof(TimeSpan))
		{
		}

		// Token: 0x06005D5F RID: 23903 RVA: 0x001C3A0A File Offset: 0x001C2C0A
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Int64, format));
		}

		// Token: 0x06005D60 RID: 23904 RVA: 0x001C3A1C File Offset: 0x001C2C1C
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			collector.AddScalar(value.ScalarValue.AsTimeSpan.Ticks);
		}
	}
}
