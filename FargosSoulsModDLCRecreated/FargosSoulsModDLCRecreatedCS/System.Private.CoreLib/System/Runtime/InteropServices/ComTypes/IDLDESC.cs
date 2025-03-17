using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004D2 RID: 1234
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct IDLDESC
	{
		// Token: 0x04001039 RID: 4153
		public IntPtr dwReserved;

		// Token: 0x0400103A RID: 4154
		public IDLFLAG wIDLFlags;
	}
}
