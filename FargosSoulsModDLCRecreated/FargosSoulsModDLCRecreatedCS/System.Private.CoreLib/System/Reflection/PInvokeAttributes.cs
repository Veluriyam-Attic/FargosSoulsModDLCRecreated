using System;

namespace System.Reflection
{
	// Token: 0x0200059C RID: 1436
	[Flags]
	internal enum PInvokeAttributes
	{
		// Token: 0x0400122D RID: 4653
		NoMangle = 1,
		// Token: 0x0400122E RID: 4654
		CharSetMask = 6,
		// Token: 0x0400122F RID: 4655
		CharSetNotSpec = 0,
		// Token: 0x04001230 RID: 4656
		CharSetAnsi = 2,
		// Token: 0x04001231 RID: 4657
		CharSetUnicode = 4,
		// Token: 0x04001232 RID: 4658
		CharSetAuto = 6,
		// Token: 0x04001233 RID: 4659
		BestFitUseAssem = 0,
		// Token: 0x04001234 RID: 4660
		BestFitEnabled = 16,
		// Token: 0x04001235 RID: 4661
		BestFitDisabled = 32,
		// Token: 0x04001236 RID: 4662
		BestFitMask = 48,
		// Token: 0x04001237 RID: 4663
		ThrowOnUnmappableCharUseAssem = 0,
		// Token: 0x04001238 RID: 4664
		ThrowOnUnmappableCharEnabled = 4096,
		// Token: 0x04001239 RID: 4665
		ThrowOnUnmappableCharDisabled = 8192,
		// Token: 0x0400123A RID: 4666
		ThrowOnUnmappableCharMask = 12288,
		// Token: 0x0400123B RID: 4667
		SupportsLastError = 64,
		// Token: 0x0400123C RID: 4668
		CallConvMask = 1792,
		// Token: 0x0400123D RID: 4669
		CallConvWinapi = 256,
		// Token: 0x0400123E RID: 4670
		CallConvCdecl = 512,
		// Token: 0x0400123F RID: 4671
		CallConvStdcall = 768,
		// Token: 0x04001240 RID: 4672
		CallConvThiscall = 1024,
		// Token: 0x04001241 RID: 4673
		CallConvFastcall = 1280,
		// Token: 0x04001242 RID: 4674
		MaxValue = 65535
	}
}
