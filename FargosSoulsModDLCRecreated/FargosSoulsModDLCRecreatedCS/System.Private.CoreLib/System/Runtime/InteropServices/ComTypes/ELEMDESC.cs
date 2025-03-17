using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004D6 RID: 1238
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct ELEMDESC
	{
		// Token: 0x04001048 RID: 4168
		public TYPEDESC tdesc;

		// Token: 0x04001049 RID: 4169
		public ELEMDESC.DESCUNION desc;

		// Token: 0x020004D7 RID: 1239
		[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
		public struct DESCUNION
		{
			// Token: 0x0400104A RID: 4170
			[FieldOffset(0)]
			public IDLDESC idldesc;

			// Token: 0x0400104B RID: 4171
			[FieldOffset(0)]
			public PARAMDESC paramdesc;
		}
	}
}
