using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007F0 RID: 2032
	[NullableContext(1)]
	public interface ICollection<[Nullable(2)] T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x17001012 RID: 4114
		// (get) Token: 0x0600617F RID: 24959
		int Count { get; }

		// Token: 0x17001013 RID: 4115
		// (get) Token: 0x06006180 RID: 24960
		bool IsReadOnly { get; }

		// Token: 0x06006181 RID: 24961
		void Add(T item);

		// Token: 0x06006182 RID: 24962
		void Clear();

		// Token: 0x06006183 RID: 24963
		bool Contains(T item);

		// Token: 0x06006184 RID: 24964
		void CopyTo(T[] array, int arrayIndex);

		// Token: 0x06006185 RID: 24965
		bool Remove(T item);
	}
}
