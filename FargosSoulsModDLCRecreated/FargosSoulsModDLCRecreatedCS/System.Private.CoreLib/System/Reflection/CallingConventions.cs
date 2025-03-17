using System;

namespace System.Reflection
{
	// Token: 0x020005D0 RID: 1488
	[Flags]
	public enum CallingConventions
	{
		// Token: 0x04001307 RID: 4871
		Standard = 1,
		// Token: 0x04001308 RID: 4872
		VarArgs = 2,
		// Token: 0x04001309 RID: 4873
		Any = 3,
		// Token: 0x0400130A RID: 4874
		HasThis = 32,
		// Token: 0x0400130B RID: 4875
		ExplicitThis = 64
	}
}
