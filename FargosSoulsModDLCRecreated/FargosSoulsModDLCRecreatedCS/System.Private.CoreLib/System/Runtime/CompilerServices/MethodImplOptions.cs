using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000547 RID: 1351
	[Flags]
	public enum MethodImplOptions
	{
		// Token: 0x0400112A RID: 4394
		Unmanaged = 4,
		// Token: 0x0400112B RID: 4395
		NoInlining = 8,
		// Token: 0x0400112C RID: 4396
		ForwardRef = 16,
		// Token: 0x0400112D RID: 4397
		Synchronized = 32,
		// Token: 0x0400112E RID: 4398
		NoOptimization = 64,
		// Token: 0x0400112F RID: 4399
		PreserveSig = 128,
		// Token: 0x04001130 RID: 4400
		AggressiveInlining = 256,
		// Token: 0x04001131 RID: 4401
		AggressiveOptimization = 512,
		// Token: 0x04001132 RID: 4402
		InternalCall = 4096
	}
}
