using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200070E RID: 1806
	[Flags]
	public enum EventActivityOptions
	{
		// Token: 0x04001A1F RID: 6687
		None = 0,
		// Token: 0x04001A20 RID: 6688
		Disable = 2,
		// Token: 0x04001A21 RID: 6689
		Recursive = 4,
		// Token: 0x04001A22 RID: 6690
		Detachable = 8
	}
}
