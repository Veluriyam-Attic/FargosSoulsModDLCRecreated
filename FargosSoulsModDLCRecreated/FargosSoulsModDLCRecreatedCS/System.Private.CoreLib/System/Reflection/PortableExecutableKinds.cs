using System;

namespace System.Reflection
{
	// Token: 0x020005F5 RID: 1525
	[Flags]
	public enum PortableExecutableKinds
	{
		// Token: 0x040013BC RID: 5052
		NotAPortableExecutableImage = 0,
		// Token: 0x040013BD RID: 5053
		ILOnly = 1,
		// Token: 0x040013BE RID: 5054
		Required32Bit = 2,
		// Token: 0x040013BF RID: 5055
		PE32Plus = 4,
		// Token: 0x040013C0 RID: 5056
		Unmanaged32Bit = 8,
		// Token: 0x040013C1 RID: 5057
		Preferred32Bit = 16
	}
}
