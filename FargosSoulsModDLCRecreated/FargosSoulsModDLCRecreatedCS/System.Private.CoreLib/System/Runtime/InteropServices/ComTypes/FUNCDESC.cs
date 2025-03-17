using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004D0 RID: 1232
	public struct FUNCDESC
	{
		// Token: 0x04001027 RID: 4135
		public int memid;

		// Token: 0x04001028 RID: 4136
		public IntPtr lprgscode;

		// Token: 0x04001029 RID: 4137
		public IntPtr lprgelemdescParam;

		// Token: 0x0400102A RID: 4138
		public FUNCKIND funckind;

		// Token: 0x0400102B RID: 4139
		public INVOKEKIND invkind;

		// Token: 0x0400102C RID: 4140
		public CALLCONV callconv;

		// Token: 0x0400102D RID: 4141
		public short cParams;

		// Token: 0x0400102E RID: 4142
		public short cParamsOpt;

		// Token: 0x0400102F RID: 4143
		public short oVft;

		// Token: 0x04001030 RID: 4144
		public short cScodes;

		// Token: 0x04001031 RID: 4145
		public ELEMDESC elemdescFunc;

		// Token: 0x04001032 RID: 4146
		public short wFuncFlags;
	}
}
