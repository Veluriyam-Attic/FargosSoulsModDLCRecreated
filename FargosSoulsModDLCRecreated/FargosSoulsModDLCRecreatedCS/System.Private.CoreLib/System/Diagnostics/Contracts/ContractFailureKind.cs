using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006EC RID: 1772
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	public enum ContractFailureKind
	{
		// Token: 0x0400199F RID: 6559
		Precondition,
		// Token: 0x040019A0 RID: 6560
		Postcondition,
		// Token: 0x040019A1 RID: 6561
		PostconditionOnException,
		// Token: 0x040019A2 RID: 6562
		Invariant,
		// Token: 0x040019A3 RID: 6563
		Assert,
		// Token: 0x040019A4 RID: 6564
		Assume
	}
}
