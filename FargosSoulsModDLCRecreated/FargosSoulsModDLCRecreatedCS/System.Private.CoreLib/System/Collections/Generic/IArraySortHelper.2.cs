using System;

namespace System.Collections.Generic
{
	// Token: 0x020007D2 RID: 2002
	internal interface IArraySortHelper<TKey, TValue>
	{
		// Token: 0x06006068 RID: 24680
		void Sort(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer);
	}
}
