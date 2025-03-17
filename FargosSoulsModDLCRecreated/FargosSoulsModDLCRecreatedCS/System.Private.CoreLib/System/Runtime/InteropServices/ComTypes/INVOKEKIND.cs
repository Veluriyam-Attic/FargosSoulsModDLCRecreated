using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004DE RID: 1246
	[Flags]
	public enum INVOKEKIND
	{
		// Token: 0x0400106D RID: 4205
		INVOKE_FUNC = 1,
		// Token: 0x0400106E RID: 4206
		INVOKE_PROPERTYGET = 2,
		// Token: 0x0400106F RID: 4207
		INVOKE_PROPERTYPUT = 4,
		// Token: 0x04001070 RID: 4208
		INVOKE_PROPERTYPUTREF = 8
	}
}
