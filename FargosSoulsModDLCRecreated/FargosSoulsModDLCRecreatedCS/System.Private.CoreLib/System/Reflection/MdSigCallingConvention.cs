using System;

namespace System.Reflection
{
	// Token: 0x0200059B RID: 1435
	[Flags]
	internal enum MdSigCallingConvention : byte
	{
		// Token: 0x0400121D RID: 4637
		CallConvMask = 15,
		// Token: 0x0400121E RID: 4638
		Default = 0,
		// Token: 0x0400121F RID: 4639
		C = 1,
		// Token: 0x04001220 RID: 4640
		StdCall = 2,
		// Token: 0x04001221 RID: 4641
		ThisCall = 3,
		// Token: 0x04001222 RID: 4642
		FastCall = 4,
		// Token: 0x04001223 RID: 4643
		Vararg = 5,
		// Token: 0x04001224 RID: 4644
		Field = 6,
		// Token: 0x04001225 RID: 4645
		LocalSig = 7,
		// Token: 0x04001226 RID: 4646
		Property = 8,
		// Token: 0x04001227 RID: 4647
		Unmanaged = 9,
		// Token: 0x04001228 RID: 4648
		GenericInst = 10,
		// Token: 0x04001229 RID: 4649
		Generic = 16,
		// Token: 0x0400122A RID: 4650
		HasThis = 32,
		// Token: 0x0400122B RID: 4651
		ExplicitThis = 64
	}
}
