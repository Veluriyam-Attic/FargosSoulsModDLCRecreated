using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000759 RID: 1881
	internal abstract class ConcurrentSetItem<KeyType, ItemType> where ItemType : ConcurrentSetItem<KeyType, ItemType>
	{
		// Token: 0x06005C68 RID: 23656
		public abstract int Compare(ItemType other);

		// Token: 0x06005C69 RID: 23657
		public abstract int Compare(KeyType key);
	}
}
