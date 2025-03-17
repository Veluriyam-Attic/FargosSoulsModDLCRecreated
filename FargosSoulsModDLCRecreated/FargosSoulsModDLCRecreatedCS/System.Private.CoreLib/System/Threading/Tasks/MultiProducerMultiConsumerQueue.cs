using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;

namespace System.Threading.Tasks
{
	// Token: 0x02000306 RID: 774
	[DebuggerDisplay("Count = {Count}")]
	internal sealed class MultiProducerMultiConsumerQueue<T> : ConcurrentQueue<T>, IProducerConsumerQueue<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x06002A00 RID: 10752 RVA: 0x0014D0A4 File Offset: 0x0014C2A4
		void IProducerConsumerQueue<!0>.Enqueue(T item)
		{
			base.Enqueue(item);
		}

		// Token: 0x06002A01 RID: 10753 RVA: 0x0014D0AD File Offset: 0x0014C2AD
		bool IProducerConsumerQueue<!0>.TryDequeue([MaybeNullWhen(false)] out T result)
		{
			return base.TryDequeue(out result);
		}

		// Token: 0x170008A5 RID: 2213
		// (get) Token: 0x06002A02 RID: 10754 RVA: 0x0014D0B6 File Offset: 0x0014C2B6
		bool IProducerConsumerQueue<!0>.IsEmpty
		{
			get
			{
				return base.IsEmpty;
			}
		}

		// Token: 0x170008A6 RID: 2214
		// (get) Token: 0x06002A03 RID: 10755 RVA: 0x0014D0BE File Offset: 0x0014C2BE
		int IProducerConsumerQueue<!0>.Count
		{
			get
			{
				return base.Count;
			}
		}
	}
}
