using System;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000784 RID: 1924
	internal static class SimpleEventTypes<T>
	{
		// Token: 0x17000F2C RID: 3884
		// (get) Token: 0x06005D24 RID: 23844 RVA: 0x001C3395 File Offset: 0x001C2595
		public static TraceLoggingEventTypes Instance
		{
			get
			{
				return SimpleEventTypes<T>.instance ?? SimpleEventTypes<T>.InitInstance();
			}
		}

		// Token: 0x06005D25 RID: 23845 RVA: 0x001C33A8 File Offset: 0x001C25A8
		private static TraceLoggingEventTypes InitInstance()
		{
			TraceLoggingTypeInfo traceLoggingTypeInfo = TraceLoggingTypeInfo.GetInstance(typeof(T), null);
			TraceLoggingEventTypes value = new TraceLoggingEventTypes(traceLoggingTypeInfo.Name, traceLoggingTypeInfo.Tags, new TraceLoggingTypeInfo[]
			{
				traceLoggingTypeInfo
			});
			Interlocked.CompareExchange<TraceLoggingEventTypes>(ref SimpleEventTypes<T>.instance, value, null);
			return SimpleEventTypes<T>.instance;
		}

		// Token: 0x04001C2F RID: 7215
		private static TraceLoggingEventTypes instance;
	}
}
