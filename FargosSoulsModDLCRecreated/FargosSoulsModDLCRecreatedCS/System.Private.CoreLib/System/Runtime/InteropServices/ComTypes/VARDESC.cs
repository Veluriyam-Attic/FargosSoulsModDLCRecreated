using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004D9 RID: 1241
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct VARDESC
	{
		// Token: 0x04001051 RID: 4177
		public int memid;

		// Token: 0x04001052 RID: 4178
		[Nullable(1)]
		public string lpstrSchema;

		// Token: 0x04001053 RID: 4179
		public VARDESC.DESCUNION desc;

		// Token: 0x04001054 RID: 4180
		public ELEMDESC elemdescVar;

		// Token: 0x04001055 RID: 4181
		public short wVarFlags;

		// Token: 0x04001056 RID: 4182
		public VARKIND varkind;

		// Token: 0x020004DA RID: 1242
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x04001057 RID: 4183
			[FieldOffset(0)]
			public int oInst;

			// Token: 0x04001058 RID: 4184
			[FieldOffset(0)]
			public IntPtr lpvarValue;
		}
	}
}
