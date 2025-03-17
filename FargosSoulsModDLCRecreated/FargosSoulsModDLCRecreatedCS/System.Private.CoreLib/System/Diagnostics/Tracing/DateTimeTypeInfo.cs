using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200078B RID: 1931
	internal sealed class DateTimeTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005D58 RID: 23896 RVA: 0x001C38F2 File Offset: 0x001C2AF2
		public DateTimeTypeInfo() : base(typeof(DateTime))
		{
		}

		// Token: 0x06005D59 RID: 23897 RVA: 0x001C3904 File Offset: 0x001C2B04
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.FileTime, format));
		}

		// Token: 0x06005D5A RID: 23898 RVA: 0x001C3918 File Offset: 0x001C2B18
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			DateTime asDateTime = value.ScalarValue.AsDateTime;
			long value2 = 0L;
			if (asDateTime.Ticks > 504911232000000000L)
			{
				value2 = asDateTime.ToFileTimeUtc();
			}
			collector.AddScalar(value2);
		}
	}
}
