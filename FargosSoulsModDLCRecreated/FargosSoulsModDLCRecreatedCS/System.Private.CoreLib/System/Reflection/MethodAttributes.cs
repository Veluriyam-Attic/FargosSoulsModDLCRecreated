using System;

namespace System.Reflection
{
	// Token: 0x020005E7 RID: 1511
	[Flags]
	public enum MethodAttributes
	{
		// Token: 0x04001372 RID: 4978
		MemberAccessMask = 7,
		// Token: 0x04001373 RID: 4979
		PrivateScope = 0,
		// Token: 0x04001374 RID: 4980
		Private = 1,
		// Token: 0x04001375 RID: 4981
		FamANDAssem = 2,
		// Token: 0x04001376 RID: 4982
		Assembly = 3,
		// Token: 0x04001377 RID: 4983
		Family = 4,
		// Token: 0x04001378 RID: 4984
		FamORAssem = 5,
		// Token: 0x04001379 RID: 4985
		Public = 6,
		// Token: 0x0400137A RID: 4986
		Static = 16,
		// Token: 0x0400137B RID: 4987
		Final = 32,
		// Token: 0x0400137C RID: 4988
		Virtual = 64,
		// Token: 0x0400137D RID: 4989
		HideBySig = 128,
		// Token: 0x0400137E RID: 4990
		CheckAccessOnOverride = 512,
		// Token: 0x0400137F RID: 4991
		VtableLayoutMask = 256,
		// Token: 0x04001380 RID: 4992
		ReuseSlot = 0,
		// Token: 0x04001381 RID: 4993
		NewSlot = 256,
		// Token: 0x04001382 RID: 4994
		Abstract = 1024,
		// Token: 0x04001383 RID: 4995
		SpecialName = 2048,
		// Token: 0x04001384 RID: 4996
		PinvokeImpl = 8192,
		// Token: 0x04001385 RID: 4997
		UnmanagedExport = 8,
		// Token: 0x04001386 RID: 4998
		RTSpecialName = 4096,
		// Token: 0x04001387 RID: 4999
		HasSecurity = 16384,
		// Token: 0x04001388 RID: 5000
		RequireSecObject = 32768,
		// Token: 0x04001389 RID: 5001
		ReservedMask = 53248
	}
}
