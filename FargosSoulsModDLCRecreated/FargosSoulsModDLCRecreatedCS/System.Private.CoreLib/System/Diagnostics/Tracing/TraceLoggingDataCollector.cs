using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000791 RID: 1937
	internal class TraceLoggingDataCollector
	{
		// Token: 0x06005D7A RID: 23930 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private TraceLoggingDataCollector()
		{
		}

		// Token: 0x06005D7B RID: 23931 RVA: 0x001C4396 File Offset: 0x001C3596
		public int BeginBufferedArray()
		{
			return DataCollector.ThreadInstance.BeginBufferedArray();
		}

		// Token: 0x06005D7C RID: 23932 RVA: 0x001C43A2 File Offset: 0x001C35A2
		public void EndBufferedArray(int bookmark, int count)
		{
			DataCollector.ThreadInstance.EndBufferedArray(bookmark, count);
		}

		// Token: 0x06005D7D RID: 23933 RVA: 0x001C43B0 File Offset: 0x001C35B0
		public unsafe void AddScalar(PropertyValue value)
		{
			PropertyValue.Scalar scalarValue = value.ScalarValue;
			DataCollector.ThreadInstance.AddScalar((void*)(&scalarValue), value.ScalarLength);
		}

		// Token: 0x06005D7E RID: 23934 RVA: 0x001C43D9 File Offset: 0x001C35D9
		public unsafe void AddScalar(long value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06005D7F RID: 23935 RVA: 0x001C43D9 File Offset: 0x001C35D9
		public unsafe void AddScalar(double value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 8);
		}

		// Token: 0x06005D80 RID: 23936 RVA: 0x001C43E9 File Offset: 0x001C35E9
		public unsafe void AddScalar(bool value)
		{
			DataCollector.ThreadInstance.AddScalar((void*)(&value), 1);
		}

		// Token: 0x06005D81 RID: 23937 RVA: 0x001C43F9 File Offset: 0x001C35F9
		public void AddNullTerminatedString(string value)
		{
			DataCollector.ThreadInstance.AddNullTerminatedString(value);
		}

		// Token: 0x06005D82 RID: 23938 RVA: 0x001C4408 File Offset: 0x001C3608
		public void AddArray(PropertyValue value, int elementSize)
		{
			Array array = (Array)value.ReferenceValue;
			DataCollector.ThreadInstance.AddArray(array, (array == null) ? 0 : array.Length, elementSize);
		}

		// Token: 0x04001C3E RID: 7230
		internal static readonly TraceLoggingDataCollector Instance = new TraceLoggingDataCollector();
	}
}
