using System;

namespace System.Threading.Tasks
{
	// Token: 0x020002ED RID: 749
	internal enum CausalityRelation
	{
		// Token: 0x04000B38 RID: 2872
		AssignDelegate,
		// Token: 0x04000B39 RID: 2873
		Join,
		// Token: 0x04000B3A RID: 2874
		Choice,
		// Token: 0x04000B3B RID: 2875
		Cancel,
		// Token: 0x04000B3C RID: 2876
		Error
	}
}
