using System;

namespace System.Threading
{
	// Token: 0x020002A5 RID: 677
	public struct NativeOverlapped
	{
		// Token: 0x04000A76 RID: 2678
		public IntPtr InternalLow;

		// Token: 0x04000A77 RID: 2679
		public IntPtr InternalHigh;

		// Token: 0x04000A78 RID: 2680
		public int OffsetLow;

		// Token: 0x04000A79 RID: 2681
		public int OffsetHigh;

		// Token: 0x04000A7A RID: 2682
		public IntPtr EventHandle;
	}
}
