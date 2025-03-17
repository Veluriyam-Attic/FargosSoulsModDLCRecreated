using System;
using System.Diagnostics;

namespace System.Collections.Concurrent
{
	// Token: 0x020007CC RID: 1996
	internal sealed class IProducerConsumerCollectionDebugView<T>
	{
		// Token: 0x06006005 RID: 24581 RVA: 0x001CBBC4 File Offset: 0x001CADC4
		public IProducerConsumerCollectionDebugView(IProducerConsumerCollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17000FD2 RID: 4050
		// (get) Token: 0x06006006 RID: 24582 RVA: 0x001CBBE2 File Offset: 0x001CADE2
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._collection.ToArray();
			}
		}

		// Token: 0x04001CFD RID: 7421
		private readonly IProducerConsumerCollection<T> _collection;
	}
}
