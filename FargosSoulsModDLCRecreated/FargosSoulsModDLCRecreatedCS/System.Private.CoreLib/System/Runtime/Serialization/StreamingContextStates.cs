using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020003EE RID: 1006
	[Flags]
	public enum StreamingContextStates
	{
		// Token: 0x04000E0D RID: 3597
		CrossProcess = 1,
		// Token: 0x04000E0E RID: 3598
		CrossMachine = 2,
		// Token: 0x04000E0F RID: 3599
		File = 4,
		// Token: 0x04000E10 RID: 3600
		Persistence = 8,
		// Token: 0x04000E11 RID: 3601
		Remoting = 16,
		// Token: 0x04000E12 RID: 3602
		Other = 32,
		// Token: 0x04000E13 RID: 3603
		Clone = 64,
		// Token: 0x04000E14 RID: 3604
		CrossAppDomain = 128,
		// Token: 0x04000E15 RID: 3605
		All = 255
	}
}
