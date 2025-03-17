using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004CA RID: 1226
	[StructLayout(LayoutKind.Explicit, CharSet = CharSet.Unicode)]
	public struct BINDPTR
	{
		// Token: 0x04000FF2 RID: 4082
		[FieldOffset(0)]
		public IntPtr lpfuncdesc;

		// Token: 0x04000FF3 RID: 4083
		[FieldOffset(0)]
		public IntPtr lpvardesc;

		// Token: 0x04000FF4 RID: 4084
		[FieldOffset(0)]
		public IntPtr lptcomp;
	}
}
