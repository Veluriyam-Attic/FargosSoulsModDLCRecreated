using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004BE RID: 1214
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct CONNECTDATA
	{
		// Token: 0x04000FDC RID: 4060
		[Nullable(1)]
		[MarshalAs(UnmanagedType.Interface)]
		public object pUnk;

		// Token: 0x04000FDD RID: 4061
		public int dwCookie;
	}
}
