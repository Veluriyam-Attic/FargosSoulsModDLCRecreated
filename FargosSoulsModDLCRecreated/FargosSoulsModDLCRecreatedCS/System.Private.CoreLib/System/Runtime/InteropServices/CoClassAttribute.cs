using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200046C RID: 1132
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class CoClassAttribute : Attribute
	{
		// Token: 0x06004444 RID: 17476 RVA: 0x00178D69 File Offset: 0x00177F69
		public CoClassAttribute(Type coClass)
		{
			this.CoClass = coClass;
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06004445 RID: 17477 RVA: 0x00178D78 File Offset: 0x00177F78
		public Type CoClass { get; }
	}
}
