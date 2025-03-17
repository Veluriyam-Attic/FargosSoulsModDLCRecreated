using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace System.Threading.Tasks
{
	// Token: 0x02000305 RID: 773
	internal interface IProducerConsumerQueue<T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x060029FC RID: 10748
		void Enqueue(T item);

		// Token: 0x060029FD RID: 10749
		bool TryDequeue([MaybeNullWhen(false)] out T result);

		// Token: 0x170008A3 RID: 2211
		// (get) Token: 0x060029FE RID: 10750
		bool IsEmpty { get; }

		// Token: 0x170008A4 RID: 2212
		// (get) Token: 0x060029FF RID: 10751
		int Count { get; }
	}
}
