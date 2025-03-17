using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200074B RID: 1867
	[EventData]
	internal class IncrementingPollingCounterPayloadType
	{
		// Token: 0x06005BA9 RID: 23465 RVA: 0x001BEA48 File Offset: 0x001BDC48
		public IncrementingPollingCounterPayloadType(IncrementingCounterPayload payload)
		{
			this.Payload = payload;
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x06005BAA RID: 23466 RVA: 0x001BEA57 File Offset: 0x001BDC57
		// (set) Token: 0x06005BAB RID: 23467 RVA: 0x001BEA5F File Offset: 0x001BDC5F
		public IncrementingCounterPayload Payload { get; set; }
	}
}
