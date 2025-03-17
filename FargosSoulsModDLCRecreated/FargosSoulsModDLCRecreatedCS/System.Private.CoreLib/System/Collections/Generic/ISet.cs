using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000800 RID: 2048
	[NullableContext(1)]
	public interface ISet<[Nullable(2)] T> : ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060061A9 RID: 25001
		bool Add(T item);

		// Token: 0x060061AA RID: 25002
		void UnionWith(IEnumerable<T> other);

		// Token: 0x060061AB RID: 25003
		void IntersectWith(IEnumerable<T> other);

		// Token: 0x060061AC RID: 25004
		void ExceptWith(IEnumerable<T> other);

		// Token: 0x060061AD RID: 25005
		void SymmetricExceptWith(IEnumerable<T> other);

		// Token: 0x060061AE RID: 25006
		bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x060061AF RID: 25007
		bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x060061B0 RID: 25008
		bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x060061B1 RID: 25009
		bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x060061B2 RID: 25010
		bool Overlaps(IEnumerable<T> other);

		// Token: 0x060061B3 RID: 25011
		bool SetEquals(IEnumerable<T> other);
	}
}
