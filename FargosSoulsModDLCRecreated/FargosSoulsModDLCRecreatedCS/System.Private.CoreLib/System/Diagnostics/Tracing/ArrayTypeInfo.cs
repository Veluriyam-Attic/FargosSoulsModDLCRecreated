using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000757 RID: 1879
	internal sealed class ArrayTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005C62 RID: 23650 RVA: 0x001C113E File Offset: 0x001C033E
		public ArrayTypeInfo(Type type, TraceLoggingTypeInfo elementInfo) : base(type)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x06005C63 RID: 23651 RVA: 0x001C114E File Offset: 0x001C034E
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x06005C64 RID: 23652 RVA: 0x001C116C File Offset: 0x001C036C
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			int bookmark = collector.BeginBufferedArray();
			int count = 0;
			Array array = (Array)value.ReferenceValue;
			if (array != null)
			{
				count = array.Length;
				for (int i = 0; i < array.Length; i++)
				{
					this.elementInfo.WriteData(collector, this.elementInfo.PropertyValueFactory(array.GetValue(i)));
				}
			}
			collector.EndBufferedArray(bookmark, count);
		}

		// Token: 0x06005C65 RID: 23653 RVA: 0x001C11D8 File Offset: 0x001C03D8
		public override object GetData(object value)
		{
			Array array = (Array)value;
			object[] array2 = new object[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.elementInfo.GetData(array.GetValue(i));
			}
			return array2;
		}

		// Token: 0x04001BAE RID: 7086
		private readonly TraceLoggingTypeInfo elementInfo;
	}
}
