using System;

namespace System.Threading.Tasks
{
	// Token: 0x0200030C RID: 780
	public enum TaskStatus
	{
		// Token: 0x04000B9B RID: 2971
		Created,
		// Token: 0x04000B9C RID: 2972
		WaitingForActivation,
		// Token: 0x04000B9D RID: 2973
		WaitingToRun,
		// Token: 0x04000B9E RID: 2974
		Running,
		// Token: 0x04000B9F RID: 2975
		WaitingForChildrenToComplete,
		// Token: 0x04000BA0 RID: 2976
		RanToCompletion,
		// Token: 0x04000BA1 RID: 2977
		Canceled,
		// Token: 0x04000BA2 RID: 2978
		Faulted
	}
}
