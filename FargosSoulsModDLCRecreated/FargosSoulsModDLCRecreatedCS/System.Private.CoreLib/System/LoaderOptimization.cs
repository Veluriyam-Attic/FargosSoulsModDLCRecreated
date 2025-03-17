using System;

namespace System
{
	// Token: 0x02000145 RID: 325
	public enum LoaderOptimization
	{
		// Token: 0x0400040D RID: 1037
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		DisallowBindings = 4,
		// Token: 0x0400040E RID: 1038
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		DomainMask = 3,
		// Token: 0x0400040F RID: 1039
		MultiDomain = 2,
		// Token: 0x04000410 RID: 1040
		MultiDomainHost,
		// Token: 0x04000411 RID: 1041
		NotSpecified = 0,
		// Token: 0x04000412 RID: 1042
		SingleDomain
	}
}
