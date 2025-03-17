using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004DC RID: 1244
	[Nullable(0)]
	[NullableContext(1)]
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct EXCEPINFO
	{
		// Token: 0x0400105D RID: 4189
		public short wCode;

		// Token: 0x0400105E RID: 4190
		public short wReserved;

		// Token: 0x0400105F RID: 4191
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrSource;

		// Token: 0x04001060 RID: 4192
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrDescription;

		// Token: 0x04001061 RID: 4193
		[MarshalAs(UnmanagedType.BStr)]
		public string bstrHelpFile;

		// Token: 0x04001062 RID: 4194
		public int dwHelpContext;

		// Token: 0x04001063 RID: 4195
		public IntPtr pvReserved;

		// Token: 0x04001064 RID: 4196
		public IntPtr pfnDeferredFillIn;

		// Token: 0x04001065 RID: 4197
		public int scode;
	}
}
