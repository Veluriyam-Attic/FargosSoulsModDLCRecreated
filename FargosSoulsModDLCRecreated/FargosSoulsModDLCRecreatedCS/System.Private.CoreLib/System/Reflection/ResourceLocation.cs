using System;

namespace System.Reflection
{
	// Token: 0x020005FC RID: 1532
	[Flags]
	public enum ResourceLocation
	{
		// Token: 0x040013D8 RID: 5080
		ContainedInAnotherAssembly = 2,
		// Token: 0x040013D9 RID: 5081
		ContainedInManifestFile = 4,
		// Token: 0x040013DA RID: 5082
		Embedded = 1
	}
}
