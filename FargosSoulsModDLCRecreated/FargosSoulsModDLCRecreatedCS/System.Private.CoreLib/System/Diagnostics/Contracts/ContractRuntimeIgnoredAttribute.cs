using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006E5 RID: 1765
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
	public sealed class ContractRuntimeIgnoredAttribute : Attribute
	{
	}
}
