using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007FB RID: 2043
	[NullableContext(1)]
	public interface IList<[Nullable(2)] T> : ICollection<!0>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x1700101C RID: 4124
		T this[int index]
		{
			get;
			set;
		}

		// Token: 0x0600619F RID: 24991
		int IndexOf(T item);

		// Token: 0x060061A0 RID: 24992
		void Insert(int index, T item);

		// Token: 0x060061A1 RID: 24993
		void RemoveAt(int index);
	}
}
