using System;

namespace System.Runtime.Versioning
{
	// Token: 0x02000401 RID: 1025
	[Flags]
	public enum ResourceScope
	{
		// Token: 0x04000E34 RID: 3636
		None = 0,
		// Token: 0x04000E35 RID: 3637
		Machine = 1,
		// Token: 0x04000E36 RID: 3638
		Process = 2,
		// Token: 0x04000E37 RID: 3639
		AppDomain = 4,
		// Token: 0x04000E38 RID: 3640
		Library = 8,
		// Token: 0x04000E39 RID: 3641
		Private = 16,
		// Token: 0x04000E3A RID: 3642
		Assembly = 32
	}
}
