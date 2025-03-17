using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006E1 RID: 1761
	[Conditional("CONTRACTS_FULL")]
	[Nullable(0)]
	[NullableContext(1)]
	[Conditional("DEBUG")]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class ContractClassAttribute : Attribute
	{
		// Token: 0x06005903 RID: 22787 RVA: 0x001B16A8 File Offset: 0x001B08A8
		public ContractClassAttribute(Type typeContainingContracts)
		{
			this._typeWithContracts = typeContainingContracts;
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06005904 RID: 22788 RVA: 0x001B16B7 File Offset: 0x001B08B7
		public Type TypeContainingContracts
		{
			get
			{
				return this._typeWithContracts;
			}
		}

		// Token: 0x04001996 RID: 6550
		private readonly Type _typeWithContracts;
	}
}
