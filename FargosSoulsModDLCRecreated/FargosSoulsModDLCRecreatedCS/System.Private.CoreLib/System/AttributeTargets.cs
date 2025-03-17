using System;

namespace System
{
	// Token: 0x020000D2 RID: 210
	[Flags]
	public enum AttributeTargets
	{
		// Token: 0x0400028B RID: 651
		Assembly = 1,
		// Token: 0x0400028C RID: 652
		Module = 2,
		// Token: 0x0400028D RID: 653
		Class = 4,
		// Token: 0x0400028E RID: 654
		Struct = 8,
		// Token: 0x0400028F RID: 655
		Enum = 16,
		// Token: 0x04000290 RID: 656
		Constructor = 32,
		// Token: 0x04000291 RID: 657
		Method = 64,
		// Token: 0x04000292 RID: 658
		Property = 128,
		// Token: 0x04000293 RID: 659
		Field = 256,
		// Token: 0x04000294 RID: 660
		Event = 512,
		// Token: 0x04000295 RID: 661
		Interface = 1024,
		// Token: 0x04000296 RID: 662
		Parameter = 2048,
		// Token: 0x04000297 RID: 663
		Delegate = 4096,
		// Token: 0x04000298 RID: 664
		ReturnValue = 8192,
		// Token: 0x04000299 RID: 665
		GenericParameter = 16384,
		// Token: 0x0400029A RID: 666
		All = 32767
	}
}
