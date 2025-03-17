using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000710 RID: 1808
	[EventData]
	internal class CounterPayloadType
	{
		// Token: 0x06005A06 RID: 23046 RVA: 0x001B398B File Offset: 0x001B2B8B
		public CounterPayloadType(CounterPayload payload)
		{
			this.Payload = payload;
		}

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06005A07 RID: 23047 RVA: 0x001B399A File Offset: 0x001B2B9A
		// (set) Token: 0x06005A08 RID: 23048 RVA: 0x001B39A2 File Offset: 0x001B2BA2
		public CounterPayload Payload { get; set; }
	}
}
