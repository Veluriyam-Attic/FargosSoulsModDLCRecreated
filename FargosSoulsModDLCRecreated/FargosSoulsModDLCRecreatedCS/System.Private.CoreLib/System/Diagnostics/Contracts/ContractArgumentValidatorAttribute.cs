using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006E8 RID: 1768
	[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
	[Conditional("CONTRACTS_FULL")]
	public sealed class ContractArgumentValidatorAttribute : Attribute
	{
	}
}
