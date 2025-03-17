using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004C7 RID: 1223
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct STATSTG
	{
		// Token: 0x04000FE0 RID: 4064
		[Nullable(1)]
		public string pwcsName;

		// Token: 0x04000FE1 RID: 4065
		public int type;

		// Token: 0x04000FE2 RID: 4066
		public long cbSize;

		// Token: 0x04000FE3 RID: 4067
		public FILETIME mtime;

		// Token: 0x04000FE4 RID: 4068
		public FILETIME ctime;

		// Token: 0x04000FE5 RID: 4069
		public FILETIME atime;

		// Token: 0x04000FE6 RID: 4070
		public int grfMode;

		// Token: 0x04000FE7 RID: 4071
		public int grfLocksSupported;

		// Token: 0x04000FE8 RID: 4072
		public Guid clsid;

		// Token: 0x04000FE9 RID: 4073
		public int grfStateBits;

		// Token: 0x04000FEA RID: 4074
		public int reserved;
	}
}
