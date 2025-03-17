using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Concurrent
{
	// Token: 0x020007CB RID: 1995
	[NullableContext(1)]
	public interface IProducerConsumerCollection<[Nullable(2)] T> : IEnumerable<!0>, IEnumerable, ICollection
	{
		// Token: 0x06006001 RID: 24577
		void CopyTo(T[] array, int index);

		// Token: 0x06006002 RID: 24578
		bool TryAdd(T item);

		// Token: 0x06006003 RID: 24579
		bool TryTake([MaybeNullWhen(false)] out T item);

		// Token: 0x06006004 RID: 24580
		T[] ToArray();
	}
}
