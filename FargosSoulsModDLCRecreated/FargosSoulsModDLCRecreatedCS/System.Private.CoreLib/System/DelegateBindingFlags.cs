using System;

namespace System
{
	// Token: 0x02000064 RID: 100
	internal enum DelegateBindingFlags
	{
		// Token: 0x0400010A RID: 266
		StaticMethodOnly = 1,
		// Token: 0x0400010B RID: 267
		InstanceMethodOnly,
		// Token: 0x0400010C RID: 268
		OpenDelegateOnly = 4,
		// Token: 0x0400010D RID: 269
		ClosedDelegateOnly = 8,
		// Token: 0x0400010E RID: 270
		NeverCloseOverNull = 16,
		// Token: 0x0400010F RID: 271
		CaselessMatching = 32,
		// Token: 0x04000110 RID: 272
		RelaxedSignature = 64
	}
}
