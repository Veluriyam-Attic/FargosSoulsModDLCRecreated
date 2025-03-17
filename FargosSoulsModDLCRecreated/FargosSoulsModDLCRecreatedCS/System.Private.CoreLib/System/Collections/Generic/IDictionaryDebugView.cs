using System;
using System.Diagnostics;

namespace System.Collections.Generic
{
	// Token: 0x020007F4 RID: 2036
	internal sealed class IDictionaryDebugView<K, V>
	{
		// Token: 0x06006191 RID: 24977 RVA: 0x001D218C File Offset: 0x001D138C
		public IDictionaryDebugView(IDictionary<K, V> dictionary)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			this._dict = dictionary;
		}

		// Token: 0x17001018 RID: 4120
		// (get) Token: 0x06006192 RID: 24978 RVA: 0x001D21AC File Offset: 0x001D13AC
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public KeyValuePair<K, V>[] Items
		{
			get
			{
				KeyValuePair<K, V>[] array = new KeyValuePair<K, V>[this._dict.Count];
				this._dict.CopyTo(array, 0);
				return array;
			}
		}

		// Token: 0x04001D33 RID: 7475
		private readonly IDictionary<K, V> _dict;
	}
}
