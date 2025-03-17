using System;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x0200034E RID: 846
	[Flags]
	public enum ValueTaskSourceOnCompletedFlags
	{
		// Token: 0x04000C65 RID: 3173
		None = 0,
		// Token: 0x04000C66 RID: 3174
		UseSchedulingContext = 1,
		// Token: 0x04000C67 RID: 3175
		FlowExecutionContext = 2
	}
}
