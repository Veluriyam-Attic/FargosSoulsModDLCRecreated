using System;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000766 RID: 1894
	internal sealed class InvokeTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005CB5 RID: 23733 RVA: 0x001C1EC8 File Offset: 0x001C10C8
		public InvokeTypeInfo(Type type, TypeAnalysis typeAnalysis) : base(type, typeAnalysis.name, typeAnalysis.level, typeAnalysis.opcode, typeAnalysis.keywords, typeAnalysis.tags)
		{
			if (typeAnalysis.properties.Length != 0)
			{
				this.properties = typeAnalysis.properties;
			}
		}

		// Token: 0x06005CB6 RID: 23734 RVA: 0x001C1F04 File Offset: 0x001C1104
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			TraceLoggingMetadataCollector traceLoggingMetadataCollector = collector.AddGroup(name);
			if (this.properties != null)
			{
				foreach (PropertyAnalysis propertyAnalysis in this.properties)
				{
					EventFieldFormat format2 = EventFieldFormat.Default;
					EventFieldAttribute fieldAttribute = propertyAnalysis.fieldAttribute;
					if (fieldAttribute != null)
					{
						traceLoggingMetadataCollector.Tags = fieldAttribute.Tags;
						format2 = fieldAttribute.Format;
					}
					propertyAnalysis.typeInfo.WriteMetadata(traceLoggingMetadataCollector, propertyAnalysis.name, format2);
				}
			}
		}

		// Token: 0x06005CB7 RID: 23735 RVA: 0x001C1F74 File Offset: 0x001C1174
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			if (this.properties != null)
			{
				foreach (PropertyAnalysis propertyAnalysis in this.properties)
				{
					propertyAnalysis.typeInfo.WriteData(collector, propertyAnalysis.getter(value));
				}
			}
		}

		// Token: 0x06005CB8 RID: 23736 RVA: 0x001C1FBC File Offset: 0x001C11BC
		public override object GetData(object value)
		{
			if (this.properties != null)
			{
				List<string> list = new List<string>();
				List<object> list2 = new List<object>();
				for (int i = 0; i < this.properties.Length; i++)
				{
					object value2 = this.properties[i].propertyInfo.GetValue(value);
					list.Add(this.properties[i].name);
					list2.Add(this.properties[i].typeInfo.GetData(value2));
				}
				return new EventPayload(list, list2);
			}
			return null;
		}

		// Token: 0x04001BE2 RID: 7138
		internal readonly PropertyAnalysis[] properties;
	}
}
