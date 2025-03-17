using System;

namespace System.Reflection
{
	// Token: 0x020005C6 RID: 1478
	[Flags]
	public enum AssemblyNameFlags
	{
		// Token: 0x040012E4 RID: 4836
		None = 0,
		// Token: 0x040012E5 RID: 4837
		PublicKey = 1,
		// Token: 0x040012E6 RID: 4838
		EnableJITcompileOptimizer = 16384,
		// Token: 0x040012E7 RID: 4839
		EnableJITcompileTracking = 32768,
		// Token: 0x040012E8 RID: 4840
		Retargetable = 256
	}
}
