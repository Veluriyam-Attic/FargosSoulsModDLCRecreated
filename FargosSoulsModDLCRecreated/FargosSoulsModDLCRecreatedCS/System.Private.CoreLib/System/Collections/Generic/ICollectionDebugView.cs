using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020007F1 RID: 2033
	internal sealed class ICollectionDebugView<T>
	{
		// Token: 0x06006186 RID: 24966 RVA: 0x001D2143 File Offset: 0x001D1343
		public ICollectionDebugView(ICollection<T> collection)
		{
			if (collection == null)
			{
				throw new ArgumentNullException("collection");
			}
			this._collection = collection;
		}

		// Token: 0x17001014 RID: 4116
		// (get) Token: 0x06006187 RID: 24967 RVA: 0x001D2160 File Offset: 0x001D1360
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				T[] array = new T[this._collection.Count];
				this._collection.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001D32 RID: 7474
		private readonly ICollection<T> _collection;
	}
}
