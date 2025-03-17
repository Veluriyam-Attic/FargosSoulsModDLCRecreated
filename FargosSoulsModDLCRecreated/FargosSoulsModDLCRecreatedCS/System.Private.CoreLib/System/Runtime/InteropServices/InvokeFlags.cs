using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200045A RID: 1114
	[Flags]
	internal enum InvokeFlags : short
	{
		// Token: 0x04000ECD RID: 3789
		DISPATCH_METHOD = 1,
		// Token: 0x04000ECE RID: 3790
		DISPATCH_PROPERTYGET = 2,
		// Token: 0x04000ECF RID: 3791
		DISPATCH_PROPERTYPUT = 4,
		// Token: 0x04000ED0 RID: 3792
		DISPATCH_PROPERTYPUTREF = 8
	}
}
