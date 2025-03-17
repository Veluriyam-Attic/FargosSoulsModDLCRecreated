using System;

namespace System.Threading
{
	// Token: 0x020002D7 RID: 727
	public static class Timeout
	{
		// Token: 0x04000B14 RID: 2836
		public static readonly TimeSpan InfiniteTimeSpan = new TimeSpan(0, 0, 0, 0, -1);

		// Token: 0x04000B15 RID: 2837
		public const int Infinite = -1;
	}
}
