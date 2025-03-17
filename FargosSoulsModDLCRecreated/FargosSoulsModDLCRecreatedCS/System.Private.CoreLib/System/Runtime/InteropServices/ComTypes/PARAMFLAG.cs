using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004D3 RID: 1235
	[Flags]
	public enum PARAMFLAG : short
	{
		// Token: 0x0400103C RID: 4156
		PARAMFLAG_NONE = 0,
		// Token: 0x0400103D RID: 4157
		PARAMFLAG_FIN = 1,
		// Token: 0x0400103E RID: 4158
		PARAMFLAG_FOUT = 2,
		// Token: 0x0400103F RID: 4159
		PARAMFLAG_FLCID = 4,
		// Token: 0x04001040 RID: 4160
		PARAMFLAG_FRETVAL = 8,
		// Token: 0x04001041 RID: 4161
		PARAMFLAG_FOPT = 16,
		// Token: 0x04001042 RID: 4162
		PARAMFLAG_FHASDEFAULT = 32,
		// Token: 0x04001043 RID: 4163
		PARAMFLAG_FHASCUSTDATA = 64
	}
}
