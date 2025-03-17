using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200066B RID: 1643
	public enum StackBehaviour
	{
		// Token: 0x0400174A RID: 5962
		Pop0,
		// Token: 0x0400174B RID: 5963
		Pop1,
		// Token: 0x0400174C RID: 5964
		Pop1_pop1,
		// Token: 0x0400174D RID: 5965
		Popi,
		// Token: 0x0400174E RID: 5966
		Popi_pop1,
		// Token: 0x0400174F RID: 5967
		Popi_popi,
		// Token: 0x04001750 RID: 5968
		Popi_popi8,
		// Token: 0x04001751 RID: 5969
		Popi_popi_popi,
		// Token: 0x04001752 RID: 5970
		Popi_popr4,
		// Token: 0x04001753 RID: 5971
		Popi_popr8,
		// Token: 0x04001754 RID: 5972
		Popref,
		// Token: 0x04001755 RID: 5973
		Popref_pop1,
		// Token: 0x04001756 RID: 5974
		Popref_popi,
		// Token: 0x04001757 RID: 5975
		Popref_popi_popi,
		// Token: 0x04001758 RID: 5976
		Popref_popi_popi8,
		// Token: 0x04001759 RID: 5977
		Popref_popi_popr4,
		// Token: 0x0400175A RID: 5978
		Popref_popi_popr8,
		// Token: 0x0400175B RID: 5979
		Popref_popi_popref,
		// Token: 0x0400175C RID: 5980
		Push0,
		// Token: 0x0400175D RID: 5981
		Push1,
		// Token: 0x0400175E RID: 5982
		Push1_push1,
		// Token: 0x0400175F RID: 5983
		Pushi,
		// Token: 0x04001760 RID: 5984
		Pushi8,
		// Token: 0x04001761 RID: 5985
		Pushr4,
		// Token: 0x04001762 RID: 5986
		Pushr8,
		// Token: 0x04001763 RID: 5987
		Pushref,
		// Token: 0x04001764 RID: 5988
		Varpop,
		// Token: 0x04001765 RID: 5989
		Varpush,
		// Token: 0x04001766 RID: 5990
		Popref_popi_pop1
	}
}
