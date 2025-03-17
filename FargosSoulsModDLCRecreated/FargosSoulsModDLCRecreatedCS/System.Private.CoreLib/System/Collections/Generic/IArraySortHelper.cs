using System;

namespace System.Collections.Generic
{
	// Token: 0x020007CF RID: 1999
	internal interface IArraySortHelper<TKey>
	{
		// Token: 0x06006049 RID: 24649
		void Sort(Span<TKey> keys, IComparer<TKey> comparer);

		// Token: 0x0600604A RID: 24650
		int BinarySearch(TKey[] keys, int index, int length, TKey value, IComparer<TKey> comparer);
	}
}
