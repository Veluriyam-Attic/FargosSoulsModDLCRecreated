using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200078C RID: 1932
	internal sealed class DateTimeOffsetTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005D5B RID: 23899 RVA: 0x001C3956 File Offset: 0x001C2B56
		public DateTimeOffsetTypeInfo() : base(typeof(DateTimeOffset))
		{
		}

		// Token: 0x06005D5C RID: 23900 RVA: 0x001C3968 File Offset: 0x001C2B68
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("Ticks", Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
			traceLoggingMetadataCollector.AddScalar("Offset", TraceLoggingDataType.Int64);
		}

		// Token: 0x06005D5D RID: 23901 RVA: 0x001C39A0 File Offset: 0x001C2BA0
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			DateTimeOffset asDateTimeOffset = value.ScalarValue.AsDateTimeOffset;
			long ticks = asDateTimeOffset.Ticks;
			collector.AddScalar((ticks < 504911232000000000L) ? 0L : (ticks - 504911232000000000L));
			collector.AddScalar(asDateTimeOffset.Offset.Ticks);
		}
	}
}
