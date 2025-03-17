using System;

namespace System.Threading
{
	// Token: 0x020002D5 RID: 725
	[Flags]
	public enum ThreadState
	{
		// Token: 0x04000B0A RID: 2826
		Running = 0,
		// Token: 0x04000B0B RID: 2827
		StopRequested = 1,
		// Token: 0x04000B0C RID: 2828
		SuspendRequested = 2,
		// Token: 0x04000B0D RID: 2829
		Background = 4,
		// Token: 0x04000B0E RID: 2830
		Unstarted = 8,
		// Token: 0x04000B0F RID: 2831
		Stopped = 16,
		// Token: 0x04000B10 RID: 2832
		WaitSleepJoin = 32,
		// Token: 0x04000B11 RID: 2833
		Suspended = 64,
		// Token: 0x04000B12 RID: 2834
		AbortRequested = 128,
		// Token: 0x04000B13 RID: 2835
		Aborted = 256
	}
}
