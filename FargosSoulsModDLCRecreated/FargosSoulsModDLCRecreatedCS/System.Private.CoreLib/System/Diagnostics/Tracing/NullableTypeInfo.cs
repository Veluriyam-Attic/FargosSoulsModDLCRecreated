using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200078F RID: 1935
	internal sealed class NullableTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005D64 RID: 23908 RVA: 0x001C3A80 File Offset: 0x001C2C80
		public NullableTypeInfo(Type type, List<Type> recursionCheck) : base(type)
		{
			Type[] genericTypeArguments = type.GenericTypeArguments;
			this.valueInfo = TraceLoggingTypeInfo.GetInstance(genericTypeArguments[0], recursionCheck);
			this.valueGetter = PropertyValue.GetPropertyGetter(type.GetTypeInfo().GetDeclaredProperty("Value"));
		}

		// Token: 0x06005D65 RID: 23909 RVA: 0x001C3AC8 File Offset: 0x001C2CC8
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			traceLoggingMetadataCollector.AddScalar("HasValue", TraceLoggingDataType.Boolean8);
			this.valueInfo.WriteMetadata(traceLoggingMetadataCollector, "Value", format);
		}

		// Token: 0x06005D66 RID: 23910 RVA: 0x001C3B00 File Offset: 0x001C2D00
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			bool flag = value.ReferenceValue != null;
			collector.AddScalar(flag);
			PropertyValue value2 = flag ? this.valueGetter(value) : this.valueInfo.PropertyValueFactory(Activator.CreateInstance(this.valueInfo.DataType));
			this.valueInfo.WriteData(collector, value2);
		}

		// Token: 0x04001C39 RID: 7225
		private readonly TraceLoggingTypeInfo valueInfo;

		// Token: 0x04001C3A RID: 7226
		private readonly Func<PropertyValue, PropertyValue> valueGetter;
	}
}
