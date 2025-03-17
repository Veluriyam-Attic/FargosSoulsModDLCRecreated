using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020007F6 RID: 2038
	internal sealed class DictionaryValueCollectionDebugView<TKey, TValue>
	{
		// Token: 0x06006195 RID: 24981 RVA: 0x001D2224 File Offset: 0x001D1424
		public DictionaryValueCollectionDebugView(ICollection<TValue> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x1700101A RID: 4122
		// (get) Token: 0x06006196 RID: 24982 RVA: 0x001D2244 File Offset: 0x001D1444
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public TValue[] Items
		{
			get
			{
				TValue[] array = new TValue[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001D35 RID: 7477
		private readonly ICollection<TValue> _collection;
	}
}
