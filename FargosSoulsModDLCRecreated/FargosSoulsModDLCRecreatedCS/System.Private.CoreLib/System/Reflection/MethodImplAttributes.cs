using System;

namespace System.Reflection
{
	// Token: 0x020005E9 RID: 1513
	public enum MethodImplAttributes
	{
		// Token: 0x0400138B RID: 5003
		CodeTypeMask = 3,
		// Token: 0x0400138C RID: 5004
		IL = 0,
		// Token: 0x0400138D RID: 5005
		Native,
		// Token: 0x0400138E RID: 5006
		OPTIL,
		// Token: 0x0400138F RID: 5007
		Runtime,
		// Token: 0x04001390 RID: 5008
		ManagedMask,
		// Token: 0x04001391 RID: 5009
		Unmanaged = 4,
		// Token: 0x04001392 RID: 5010
		Managed = 0,
		// Token: 0x04001393 RID: 5011
		ForwardRef = 16,
		// Token: 0x04001394 RID: 5012
		PreserveSig = 128,
		// Token: 0x04001395 RID: 5013
		InternalCall = 4096,
		// Token: 0x04001396 RID: 5014
		Synchronized = 32,
		// Token: 0x04001397 RID: 5015
		NoInlining = 8,
		// Token: 0x04001398 RID: 5016
		AggressiveInlining = 256,
		// Token: 0x04001399 RID: 5017
		NoOptimization = 64,
		// Token: 0x0400139A RID: 5018
		AggressiveOptimization = 512,
		// Token: 0x0400139B RID: 5019
		MaxMethodImplVal = 65535
	}
}
