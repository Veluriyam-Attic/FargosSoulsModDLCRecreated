using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000754 RID: 1876
	public enum EventOpcode
	{
		// Token: 0x04001B92 RID: 7058
		Info,
		// Token: 0x04001B93 RID: 7059
		Start,
		// Token: 0x04001B94 RID: 7060
		Stop,
		// Token: 0x04001B95 RID: 7061
		DataCollectionStart,
		// Token: 0x04001B96 RID: 7062
		DataCollectionStop,
		// Token: 0x04001B97 RID: 7063
		Extension,
		// Token: 0x04001B98 RID: 7064
		Reply,
		// Token: 0x04001B99 RID: 7065
		Resume,
		// Token: 0x04001B9A RID: 7066
		Suspend,
		// Token: 0x04001B9B RID: 7067
		Send,
		// Token: 0x04001B9C RID: 7068
		Receive = 240
	}
}
