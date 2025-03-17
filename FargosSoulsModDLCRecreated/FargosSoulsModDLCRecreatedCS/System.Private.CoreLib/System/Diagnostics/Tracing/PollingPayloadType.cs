using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200074F RID: 1871
	[EventData]
	internal class PollingPayloadType
	{
		// Token: 0x06005C43 RID: 23619 RVA: 0x001C08BC File Offset: 0x001BFABC
		public PollingPayloadType(CounterPayload payload)
		{
			this.Payload = payload;
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x06005C44 RID: 23620 RVA: 0x001C08CB File Offset: 0x001BFACB
		// (set) Token: 0x06005C45 RID: 23621 RVA: 0x001C08D3 File Offset: 0x001BFAD3
		public CounterPayload Payload { get; set; }
	}
}
