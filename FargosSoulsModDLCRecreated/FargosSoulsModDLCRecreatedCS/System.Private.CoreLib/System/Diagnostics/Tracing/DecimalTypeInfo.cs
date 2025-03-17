using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200078E RID: 1934
	internal sealed class DecimalTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005D61 RID: 23905 RVA: 0x001C3A43 File Offset: 0x001C2C43
		public DecimalTypeInfo() : base(typeof(decimal))
		{
		}

		// Token: 0x06005D62 RID: 23906 RVA: 0x001C3A55 File Offset: 0x001C2C55
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.AddScalar(name, Statics.MakeDataType(TraceLoggingDataType.Double, format));
		}

		// Token: 0x06005D63 RID: 23907 RVA: 0x001C3A66 File Offset: 0x001C2C66
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			collector.AddScalar((double)value.ScalarValue.AsDecimal);
		}
	}
}
