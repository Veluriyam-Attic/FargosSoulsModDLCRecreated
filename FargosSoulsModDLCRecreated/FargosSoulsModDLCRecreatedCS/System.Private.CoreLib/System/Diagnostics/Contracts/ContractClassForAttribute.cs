using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006E2 RID: 1762
	[Nullable(0)]
	[NullableContext(1)]
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
	public sealed class ContractClassForAttribute : Attribute
	{
		// Token: 0x06005905 RID: 22789 RVA: 0x001B16BF File Offset: 0x001B08BF
		public ContractClassForAttribute(Type typeContractsAreFor)
		{
			this._typeIAmAContractFor = typeContractsAreFor;
		}

		// Token: 0x17000E82 RID: 3714
		// (get) Token: 0x06005906 RID: 22790 RVA: 0x001B16CE File Offset: 0x001B08CE
		public Type TypeContractsAreFor
		{
			get
			{
				return this._typeIAmAContractFor;
			}
		}

		// Token: 0x04001997 RID: 6551
		private readonly Type _typeIAmAContractFor;
	}
}
