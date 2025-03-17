using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004E5 RID: 1253
	[Flags]
	public enum LIBFLAGS : short
	{
		// Token: 0x0400109E RID: 4254
		LIBFLAG_FRESTRICTED = 1,
		// Token: 0x0400109F RID: 4255
		LIBFLAG_FCONTROL = 2,
		// Token: 0x040010A0 RID: 4256
		LIBFLAG_FHIDDEN = 4,
		// Token: 0x040010A1 RID: 4257
		LIBFLAG_FHASDISKIMAGE = 8
	}
}
