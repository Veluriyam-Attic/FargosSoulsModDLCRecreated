using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020007F5 RID: 2037
	internal sealed class DictionaryKeyCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06006193 RID: 24979 RVA: 0x001D21D8 File Offset: 0x001D13D8
		public DictionaryKeyCollectionDebugView(ICollection<TKey> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001019 RID: 4121
		// (get) Token: 0x06006194 RID: 24980 RVA: 0x001D21F8 File Offset: 0x001D13F8
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TKey[] Items
		{
			get
			{
				TKey[] array = new TKey[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001D34 RID: 7476
		private readonly ICollection<TKey> _collection;
	}
}
