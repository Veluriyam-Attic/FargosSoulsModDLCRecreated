using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200065E RID: 1630
	public enum FlowControl
	{
		// Token: 0x04001547 RID: 5447
		Branch,
		// Token: 0x04001548 RID: 5448
		Break,
		// Token: 0x04001549 RID: 5449
		Call,
		// Token: 0x0400154A RID: 5450
		Cond_Branch,
		// Token: 0x0400154B RID: 5451
		Meta,
		// Token: 0x0400154C RID: 5452
		Next,
		// Token: 0x0400154D RID: 5453
		[Obsolete("This API has been deprecated. https://go.microsoft.com/fwlink/?linkid=14202")]
		Phi,
		// Token: 0x0400154E RID: 5454
		Return,
		// Token: 0x0400154F RID: 5455
		Throw
	}
}
