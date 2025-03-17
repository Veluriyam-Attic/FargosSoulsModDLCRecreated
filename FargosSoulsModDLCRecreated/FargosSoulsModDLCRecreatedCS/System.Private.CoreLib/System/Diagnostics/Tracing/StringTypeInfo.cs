using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200078A RID: 1930
	internal sealed class StringTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005D54 RID: 23892 RVA: 0x001C38A6 File Offset: 0x001C2AA6
		public StringTypeInfo() : base(typeof(string))
		{
		}

		// Token: 0x06005D55 RID: 23893 RVA: 0x001C38B8 File Offset: 0x001C2AB8
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			if (name == null)
			{
				name = "message";
			}
			collector.AddNullTerminatedString(name, Statics.MakeDataType(TraceLoggingDataType.Utf16String, format));
		}

		// Token: 0x06005D56 RID: 23894 RVA: 0x001C38D2 File Offset: 0x001C2AD2
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			collector.AddNullTerminatedString((string)value.ReferenceValue);
		}

		// Token: 0x06005D57 RID: 23895 RVA: 0x001C38E6 File Offset: 0x001C2AE6
		public override object GetData(object value)
		{
			if (value == null)
			{
				return "";
			}
			return value;
		}
	}
}
