using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x02000801 RID: 2049
	[NullableContext(1)]
	public interface IReadOnlySet<[Nullable(2)] T> : IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060061B4 RID: 25012
		bool Contains(T item);

		// Token: 0x060061B5 RID: 25013
		bool IsProperSubsetOf(IEnumerable<T> other);

		// Token: 0x060061B6 RID: 25014
		bool IsProperSupersetOf(IEnumerable<T> other);

		// Token: 0x060061B7 RID: 25015
		bool IsSubsetOf(IEnumerable<T> other);

		// Token: 0x060061B8 RID: 25016
		bool IsSupersetOf(IEnumerable<T> other);

		// Token: 0x060061B9 RID: 25017
		bool Overlaps(IEnumerable<T> other);

		// Token: 0x060061BA RID: 25018
		bool SetEquals(IEnumerable<T> other);
	}
}
