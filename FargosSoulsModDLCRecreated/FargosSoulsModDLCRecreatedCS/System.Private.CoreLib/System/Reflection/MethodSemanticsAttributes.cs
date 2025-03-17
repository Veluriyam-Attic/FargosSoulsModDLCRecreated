using System;

namespace System.Reflection
{
	// Token: 0x0200059D RID: 1437
	[Flags]
	internal enum MethodSemanticsAttributes
	{
		// Token: 0x04001244 RID: 4676
		Setter = 1,
		// Token: 0x04001245 RID: 4677
		Getter = 2,
		// Token: 0x04001246 RID: 4678
		Other = 4,
		// Token: 0x04001247 RID: 4679
		AddOn = 8,
		// Token: 0x04001248 RID: 4680
		RemoveOn = 16,
		// Token: 0x04001249 RID: 4681
		Fire = 32
	}
}
