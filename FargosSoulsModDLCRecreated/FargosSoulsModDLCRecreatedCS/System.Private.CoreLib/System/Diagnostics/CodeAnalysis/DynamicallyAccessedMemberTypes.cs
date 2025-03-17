using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006EE RID: 1774
	[Flags]
	public enum DynamicallyAccessedMemberTypes
	{
		// Token: 0x040019A8 RID: 6568
		None = 0,
		// Token: 0x040019A9 RID: 6569
		PublicParameterlessConstructor = 1,
		// Token: 0x040019AA RID: 6570
		PublicConstructors = 3,
		// Token: 0x040019AB RID: 6571
		NonPublicConstructors = 4,
		// Token: 0x040019AC RID: 6572
		PublicMethods = 8,
		// Token: 0x040019AD RID: 6573
		NonPublicMethods = 16,
		// Token: 0x040019AE RID: 6574
		PublicFields = 32,
		// Token: 0x040019AF RID: 6575
		NonPublicFields = 64,
		// Token: 0x040019B0 RID: 6576
		PublicNestedTypes = 128,
		// Token: 0x040019B1 RID: 6577
		NonPublicNestedTypes = 256,
		// Token: 0x040019B2 RID: 6578
		PublicProperties = 512,
		// Token: 0x040019B3 RID: 6579
		NonPublicProperties = 1024,
		// Token: 0x040019B4 RID: 6580
		PublicEvents = 2048,
		// Token: 0x040019B5 RID: 6581
		NonPublicEvents = 4096,
		// Token: 0x040019B6 RID: 6582
		All = -1
	}
}
