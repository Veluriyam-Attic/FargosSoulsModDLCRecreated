using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000480 RID: 1152
	[Flags]
	public enum DllImportSearchPath
	{
		// Token: 0x04000F3D RID: 3901
		UseDllDirectoryForDependencies = 256,
		// Token: 0x04000F3E RID: 3902
		ApplicationDirectory = 512,
		// Token: 0x04000F3F RID: 3903
		UserDirectories = 1024,
		// Token: 0x04000F40 RID: 3904
		System32 = 2048,
		// Token: 0x04000F41 RID: 3905
		SafeDirectories = 4096,
		// Token: 0x04000F42 RID: 3906
		AssemblyDirectory = 2,
		// Token: 0x04000F43 RID: 3907
		LegacyBehavior = 0
	}
}
