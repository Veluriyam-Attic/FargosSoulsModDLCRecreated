using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004D5 RID: 1237
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPEDESC
	{
		// Token: 0x04001046 RID: 4166
		public IntPtr lpValue;

		// Token: 0x04001047 RID: 4167
		public short vt;
	}
}
