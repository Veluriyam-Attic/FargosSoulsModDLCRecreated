using System;

namespace System.Reflection
{
	// Token: 0x020005DA RID: 1498
	[Flags]
	public enum FieldAttributes
	{
		// Token: 0x0400133F RID: 4927
		FieldAccessMask = 7,
		// Token: 0x04001340 RID: 4928
		PrivateScope = 0,
		// Token: 0x04001341 RID: 4929
		Private = 1,
		// Token: 0x04001342 RID: 4930
		FamANDAssem = 2,
		// Token: 0x04001343 RID: 4931
		Assembly = 3,
		// Token: 0x04001344 RID: 4932
		Family = 4,
		// Token: 0x04001345 RID: 4933
		FamORAssem = 5,
		// Token: 0x04001346 RID: 4934
		Public = 6,
		// Token: 0x04001347 RID: 4935
		Static = 16,
		// Token: 0x04001348 RID: 4936
		InitOnly = 32,
		// Token: 0x04001349 RID: 4937
		Literal = 64,
		// Token: 0x0400134A RID: 4938
		NotSerialized = 128,
		// Token: 0x0400134B RID: 4939
		SpecialName = 512,
		// Token: 0x0400134C RID: 4940
		PinvokeImpl = 8192,
		// Token: 0x0400134D RID: 4941
		RTSpecialName = 1024,
		// Token: 0x0400134E RID: 4942
		HasFieldMarshal = 4096,
		// Token: 0x0400134F RID: 4943
		HasDefault = 32768,
		// Token: 0x04001350 RID: 4944
		HasFieldRVA = 256,
		// Token: 0x04001351 RID: 4945
		ReservedMask = 38144
	}
}
