using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006E7 RID: 1767
	[NullableContext(1)]
	[Nullable(0)]
	[Conditional("CONTRACTS_FULL")]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class ContractPublicPropertyNameAttribute : Attribute
	{
		// Token: 0x0600590C RID: 22796 RVA: 0x001B16ED File Offset: 0x001B08ED
		public ContractPublicPropertyNameAttribute(string name)
		{
			this._publicName = name;
		}

		// Token: 0x17000E84 RID: 3716
		// (get) Token: 0x0600590D RID: 22797 RVA: 0x001B16FC File Offset: 0x001B08FC
		public string Name
		{
			get
			{
				return this._publicName;
			}
		}

		// Token: 0x04001999 RID: 6553
		private readonly string _publicName;
	}
}
