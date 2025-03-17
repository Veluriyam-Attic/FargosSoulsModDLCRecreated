using System;
using System.Collections;
using System.Collections.Generic;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200075C RID: 1884
	internal sealed class EnumerableTypeInfo : TraceLoggingTypeInfo
	{
		// Token: 0x06005C7B RID: 23675 RVA: 0x001C179D File Offset: 0x001C099D
		public EnumerableTypeInfo(Type type, TraceLoggingTypeInfo elementInfo) : base(type)
		{
			this.elementInfo = elementInfo;
		}

		// Token: 0x17000F13 RID: 3859
		// (get) Token: 0x06005C7C RID: 23676 RVA: 0x001C17AD File Offset: 0x001C09AD
		internal TraceLoggingTypeInfo ElementInfo
		{
			get
			{
				return this.elementInfo;
			}
		}

		// Token: 0x06005C7D RID: 23677 RVA: 0x001C17B5 File Offset: 0x001C09B5
		public override void WriteMetadata(TraceLoggingMetadataCollector collector, string name, EventFieldFormat format)
		{
			collector.BeginBufferedArray();
			this.elementInfo.WriteMetadata(collector, name, format);
			collector.EndBufferedArray();
		}

		// Token: 0x06005C7E RID: 23678 RVA: 0x001C17D4 File Offset: 0x001C09D4
		public override void WriteData(TraceLoggingDataCollector collector, PropertyValue value)
		{
			int bookmark = collector.BeginBufferedArray();
			int num = 0;
			IEnumerable enumerable = (IEnumerable)value.ReferenceValue;
			if (enumerable != null)
			{
				foreach (object arg in enumerable)
				{
					this.elementInfo.WriteData(collector, this.elementInfo.PropertyValueFactory(arg));
					num++;
				}
			}
			collector.EndBufferedArray(bookmark, num);
		}

		// Token: 0x06005C7F RID: 23679 RVA: 0x001C1864 File Offset: 0x001C0A64
		public override object GetData(object value)
		{
			IEnumerable enumerable = (IEnumerable)value;
			List<object> list = new List<object>();
			foreach (object value2 in enumerable)
			{
				list.Add(this.elementInfo.GetData(value2));
			}
			return list.ToArray();
		}

		// Token: 0x04001BBC RID: 7100
		private readonly TraceLoggingTypeInfo elementInfo;
	}
}
