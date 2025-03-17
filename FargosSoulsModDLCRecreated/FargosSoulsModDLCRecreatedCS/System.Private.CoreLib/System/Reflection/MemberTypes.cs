using System;

namespace System.Reflection
{
	// Token: 0x020005E6 RID: 1510
	[Flags]
	public enum MemberTypes
	{
		// Token: 0x04001368 RID: 4968
		Constructor = 1,
		// Token: 0x04001369 RID: 4969
		Event = 2,
		// Token: 0x0400136A RID: 4970
		Field = 4,
		// Token: 0x0400136B RID: 4971
		Method = 8,
		// Token: 0x0400136C RID: 4972
		Property = 16,
		// Token: 0x0400136D RID: 4973
		TypeInfo = 32,
		// Token: 0x0400136E RID: 4974
		Custom = 64,
		// Token: 0x0400136F RID: 4975
		NestedType = 128,
		// Token: 0x04001370 RID: 4976
		All = 191
	}
}
