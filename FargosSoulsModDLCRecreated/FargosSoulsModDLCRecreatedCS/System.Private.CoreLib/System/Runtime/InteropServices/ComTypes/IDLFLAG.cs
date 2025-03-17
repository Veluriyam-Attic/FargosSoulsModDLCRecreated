using System;

namespace System.Runtime.InteropServices.ComTypes
{
	// Token: 0x020004D1 RID: 1233
	[Flags]
	public enum IDLFLAG : short
	{
		// Token: 0x04001034 RID: 4148
		IDLFLAG_NONE = 0,
		// Token: 0x04001035 RID: 4149
		IDLFLAG_FIN = 1,
		// Token: 0x04001036 RID: 4150
		IDLFLAG_FOUT = 2,
		// Token: 0x04001037 RID: 4151
		IDLFLAG_FLCID = 4,
		// Token: 0x04001038 RID: 4152
		IDLFLAG_FRETVAL = 8
	}
}
