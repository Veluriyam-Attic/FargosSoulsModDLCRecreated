using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200031C RID: 796
	[Flags]
	internal enum InternalTaskOptions
	{
		// Token: 0x04000BDE RID: 3038
		None = 0,
		// Token: 0x04000BDF RID: 3039
		InternalOptionsMask = 65280,
		// Token: 0x04000BE0 RID: 3040
		ContinuationTask = 512,
		// Token: 0x04000BE1 RID: 3041
		PromiseTask = 1024,
		// Token: 0x04000BE2 RID: 3042
		LazyCancellation = 4096,
		// Token: 0x04000BE3 RID: 3043
		QueuedByRuntime = 8192,
		// Token: 0x04000BE4 RID: 3044
		DoNotDispose = 16384
	}
}
