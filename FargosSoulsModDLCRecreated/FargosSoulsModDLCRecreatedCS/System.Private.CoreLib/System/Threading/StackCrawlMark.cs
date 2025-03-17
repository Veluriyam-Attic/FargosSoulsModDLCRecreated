using System;

namespace System.Threading
{
	// Token: 0x02000268 RID: 616
	internal enum StackCrawlMark
	{
		// Token: 0x040009D8 RID: 2520
		LookForMe,
		// Token: 0x040009D9 RID: 2521
		LookForMyCaller,
		// Token: 0x040009DA RID: 2522
		LookForMyCallersCaller,
		// Token: 0x040009DB RID: 2523
		LookForThread
	}
}
