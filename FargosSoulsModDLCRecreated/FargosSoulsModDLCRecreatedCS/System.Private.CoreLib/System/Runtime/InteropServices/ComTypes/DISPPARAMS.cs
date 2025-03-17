using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004DB RID: 1243
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct DISPPARAMS
	{
		// Token: 0x04001059 RID: 4185
		public IntPtr rgvarg;

		// Token: 0x0400105A RID: 4186
		public IntPtr rgdispidNamedArgs;

		// Token: 0x0400105B RID: 4187
		public int cArgs;

		// Token: 0x0400105C RID: 4188
		public int cNamedArgs;
	}
}
