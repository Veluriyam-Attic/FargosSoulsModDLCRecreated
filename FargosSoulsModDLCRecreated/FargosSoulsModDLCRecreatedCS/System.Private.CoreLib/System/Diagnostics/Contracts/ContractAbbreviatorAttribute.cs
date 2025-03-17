using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006E9 RID: 1769
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractAbbreviatorAttribute : Attribute
	{
	}
}
