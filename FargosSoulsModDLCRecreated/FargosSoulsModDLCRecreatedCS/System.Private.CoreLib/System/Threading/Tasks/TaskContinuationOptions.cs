using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200031D RID: 797
	[Flags]
	public enum TaskContinuationOptions
	{
		// Token: 0x04000BE6 RID: 3046
		None = 0,
		// Token: 0x04000BE7 RID: 3047
		PreferFairness = 1,
		// Token: 0x04000BE8 RID: 3048
		LongRunning = 2,
		// Token: 0x04000BE9 RID: 3049
		AttachedToParent = 4,
		// Token: 0x04000BEA RID: 3050
		DenyChildAttach = 8,
		// Token: 0x04000BEB RID: 3051
		HideScheduler = 16,
		// Token: 0x04000BEC RID: 3052
		LazyCancellation = 32,
		// Token: 0x04000BED RID: 3053
		RunContinuationsAsynchronously = 64,
		// Token: 0x04000BEE RID: 3054
		NotOnRanToCompletion = 65536,
		// Token: 0x04000BEF RID: 3055
		NotOnFaulted = 131072,
		// Token: 0x04000BF0 RID: 3056
		NotOnCanceled = 262144,
		// Token: 0x04000BF1 RID: 3057
		OnlyOnRanToCompletion = 393216,
		// Token: 0x04000BF2 RID: 3058
		OnlyOnFaulted = 327680,
		// Token: 0x04000BF3 RID: 3059
		OnlyOnCanceled = 196608,
		// Token: 0x04000BF4 RID: 3060
		ExecuteSynchronously = 524288
	}
}
