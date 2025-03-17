using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004D4 RID: 1236
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct PARAMDESC
	{
		// Token: 0x04001044 RID: 4164
		public IntPtr lpVarValue;

		// Token: 0x04001045 RID: 4165
		public PARAMFLAG wParamFlags;
	}
}
