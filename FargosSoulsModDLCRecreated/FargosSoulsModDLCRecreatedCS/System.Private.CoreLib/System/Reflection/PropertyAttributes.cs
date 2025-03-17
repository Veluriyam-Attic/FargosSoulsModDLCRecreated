using System;

namespace System.Reflection
{
	// Token: 0x020005F7 RID: 1527
	[Flags]
	public enum PropertyAttributes
	{
		// Token: 0x040013CA RID: 5066
		None = 0,
		// Token: 0x040013CB RID: 5067
		SpecialName = 512,
		// Token: 0x040013CC RID: 5068
		RTSpecialName = 1024,
		// Token: 0x040013CD RID: 5069
		HasDefault = 4096,
		// Token: 0x040013CE RID: 5070
		Reserved2 = 8192,
		// Token: 0x040013CF RID: 5071
		Reserved3 = 16384,
		// Token: 0x040013D0 RID: 5072
		Reserved4 = 32768,
		// Token: 0x040013D1 RID: 5073
		ReservedMask = 62464
	}
}
