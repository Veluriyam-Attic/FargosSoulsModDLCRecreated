using System;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006E6 RID: 1766
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property)]
	public sealed class ContractVerificationAttribute : Attribute
	{
		// Token: 0x0600590A RID: 22794 RVA: 0x001B16D6 File Offset: 0x001B08D6
		public ContractVerificationAttribute(bool value)
		{
			this._value = value;
		}

		// Token: 0x17000E83 RID: 3715
		// (get) Token: 0x0600590B RID: 22795 RVA: 0x001B16E5 File Offset: 0x001B08E5
		public bool Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x04001998 RID: 6552
		private readonly bool _value;
	}
}
