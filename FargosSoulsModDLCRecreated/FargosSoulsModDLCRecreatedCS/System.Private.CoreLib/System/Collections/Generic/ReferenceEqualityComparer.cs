using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x0200080F RID: 2063
	public sealed class ReferenceEqualityComparer : IEqualityComparer<object>, IEqualityComparer
	{
		// Token: 0x06006234 RID: 25140 RVA: 0x000ABD27 File Offset: 0x000AAF27
		private ReferenceEqualityComparer()
		{
		}

		// Token: 0x17001030 RID: 4144
		// (get) Token: 0x06006235 RID: 25141 RVA: 0x001D360E File Offset: 0x001D280E
		[Nullable(1)]
		public static ReferenceEqualityComparer Instance { [NullableContext(1)] get; } = new ReferenceEqualityComparer();

		// Token: 0x06006236 RID: 25142 RVA: 0x001CE852 File Offset: 0x001CDA52
		[NullableContext(2)]
		public bool Equals(object x, object y)
		{
			return x == y;
		}

		// Token: 0x06006237 RID: 25143 RVA: 0x001D3615 File Offset: 0x001D2815
		[NullableContext(2)]
		public int GetHashCode(object obj)
		{
			return RuntimeHelpers.GetHashCode(obj);
		}
	}
}
