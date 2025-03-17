using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000749 RID: 1865
	[EventData]
	internal class IncrementingEventCounterPayloadType
	{
		// Token: 0x06005BA0 RID: 23456 RVA: 0x001BE838 File Offset: 0x001BDA38
		public IncrementingEventCounterPayloadType(IncrementingCounterPayload payload)
		{
			this.Payload = payload;
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x06005BA1 RID: 23457 RVA: 0x001BE847 File Offset: 0x001BDA47
		// (set) Token: 0x06005BA2 RID: 23458 RVA: 0x001BE84F File Offset: 0x001BDA4F
		public IncrementingCounterPayload Payload { get; set; }
	}
}
