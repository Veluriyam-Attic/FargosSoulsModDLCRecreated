using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004E6 RID: 1254
	[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
	public struct TYPELIBATTR
	{
		// Token: 0x040010A2 RID: 4258
		public Guid guid;

		// Token: 0x040010A3 RID: 4259
		public int lcid;

		// Token: 0x040010A4 RID: 4260
		public SYSKIND syskind;

		// Token: 0x040010A5 RID: 4261
		public short wMajorVerNum;

		// Token: 0x040010A6 RID: 4262
		public short wMinorVerNum;

		// Token: 0x040010A7 RID: 4263
		public LIBFLAGS wLibFlags;
	}
}
