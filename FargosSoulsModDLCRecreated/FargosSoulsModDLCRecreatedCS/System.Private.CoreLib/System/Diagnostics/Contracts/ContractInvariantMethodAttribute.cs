using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006E3 RID: 1763
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = false)]
	public sealed class ContractInvariantMethodAttribute : Attribute
	{
	}
}
