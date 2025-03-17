using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004A2 RID: 1186
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class TypeIdentifierAttribute : Attribute
	{
		// Token: 0x060044E3 RID: 17635 RVA: 0x000AA9FC File Offset: 0x000A9BFC
		public TypeIdentifierAttribute()
		{
		}

		// Token: 0x060044E4 RID: 17636 RVA: 0x00179B4D File Offset: 0x00178D4D
		public TypeIdentifierAttribute(string scope, string identifier)
		{
			this.Scope = scope;
			this.Identifier = identifier;
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x00179B63 File Offset: 0x00178D63
		public string Scope { get; }

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x060044E6 RID: 17638 RVA: 0x00179B6B File Offset: 0x00178D6B
		public string Identifier { get; }
	}
}
