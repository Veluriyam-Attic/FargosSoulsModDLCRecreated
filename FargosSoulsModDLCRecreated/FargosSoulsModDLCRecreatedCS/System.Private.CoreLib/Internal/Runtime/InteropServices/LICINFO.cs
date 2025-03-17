using System;
using System.Runtime.InteropServices;

namespace Internal.Runtime.InteropServices
{
	// Token: 0x0200081D RID: 2077
	internal struct LICINFO
	{
		// Token: 0x04001D59 RID: 7513
		public int cbLicInfo;

		// Token: 0x04001D5A RID: 7514
		[MarshalAs(UnmanagedType.Bool)]
		public bool fRuntimeKeyAvail;

		// Token: 0x04001D5B RID: 7515
		[MarshalAs(UnmanagedType.Bool)]
		public bool fLicVerified;
	}
}
