﻿using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004E0 RID: 1248
	[Flags]
	public enum FUNCFLAGS : short
	{
		// Token: 0x0400107D RID: 4221
		FUNCFLAG_FRESTRICTED = 1,
		// Token: 0x0400107E RID: 4222
		FUNCFLAG_FSOURCE = 2,
		// Token: 0x0400107F RID: 4223
		FUNCFLAG_FBINDABLE = 4,
		// Token: 0x04001080 RID: 4224
		FUNCFLAG_FREQUESTEDIT = 8,
		// Token: 0x04001081 RID: 4225
		FUNCFLAG_FDISPLAYBIND = 16,
		// Token: 0x04001082 RID: 4226
		FUNCFLAG_FDEFAULTBIND = 32,
		// Token: 0x04001083 RID: 4227
		FUNCFLAG_FHIDDEN = 64,
		// Token: 0x04001084 RID: 4228
		FUNCFLAG_FUSESGETLASTERROR = 128,
		// Token: 0x04001085 RID: 4229
		FUNCFLAG_FDEFAULTCOLLELEM = 256,
		// Token: 0x04001086 RID: 4230
		FUNCFLAG_FUIDEFAULT = 512,
		// Token: 0x04001087 RID: 4231
		FUNCFLAG_FNONBROWSABLE = 1024,
		// Token: 0x04001088 RID: 4232
		FUNCFLAG_FREPLACEABLE = 2048,
		// Token: 0x04001089 RID: 4233
		FUNCFLAG_FIMMEDIATEBIND = 4096
	}
}
