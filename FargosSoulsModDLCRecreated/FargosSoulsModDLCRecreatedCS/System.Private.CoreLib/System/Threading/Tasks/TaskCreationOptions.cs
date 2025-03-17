using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200031B RID: 795
	[Flags]
	public enum TaskCreationOptions
	{
		// Token: 0x04000BD6 RID: 3030
		None = 0,
		// Token: 0x04000BD7 RID: 3031
		PreferFairness = 1,
		// Token: 0x04000BD8 RID: 3032
		LongRunning = 2,
		// Token: 0x04000BD9 RID: 3033
		AttachedToParent = 4,
		// Token: 0x04000BDA RID: 3034
		DenyChildAttach = 8,
		// Token: 0x04000BDB RID: 3035
		HideScheduler = 16,
		// Token: 0x04000BDC RID: 3036
		RunContinuationsAsynchronously = 64
	}
}
