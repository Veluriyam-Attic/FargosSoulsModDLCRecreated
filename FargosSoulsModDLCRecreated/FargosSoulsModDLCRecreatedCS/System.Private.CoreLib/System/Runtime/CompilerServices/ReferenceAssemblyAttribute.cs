using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000549 RID: 1353
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class ReferenceAssemblyAttribute : Attribute
	{
		// Token: 0x0600474F RID: 18255 RVA: 0x000AA9FC File Offset: 0x000A9BFC
		public ReferenceAssemblyAttribute()
		{
		}

		// Token: 0x06004750 RID: 18256 RVA: 0x0017D649 File Offset: 0x0017C849
		public ReferenceAssemblyAttribute(string description)
		{
			this.Description = description;
		}

		// Token: 0x17000ABC RID: 2748
		// (get) Token: 0x06004751 RID: 18257 RVA: 0x0017D658 File Offset: 0x0017C858
		public string Description { get; }
	}
}
