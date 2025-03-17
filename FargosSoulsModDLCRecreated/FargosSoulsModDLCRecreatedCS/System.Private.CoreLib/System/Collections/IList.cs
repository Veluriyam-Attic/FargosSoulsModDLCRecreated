using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007BD RID: 1981
	[NullableContext(2)]
	public interface IList : ICollection, IEnumerable
	{
		// Token: 0x17000FB7 RID: 4023
		object this[int index]
		{
			get;
			set;
		}

		// Token: 0x06005FAA RID: 24490
		int Add(object value);

		// Token: 0x06005FAB RID: 24491
		bool Contains(object value);

		// Token: 0x06005FAC RID: 24492
		void Clear();

		// Token: 0x17000FB8 RID: 4024
		// (get) Token: 0x06005FAD RID: 24493
		bool IsReadOnly { get; }

		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06005FAE RID: 24494
		bool IsFixedSize { get; }

		// Token: 0x06005FAF RID: 24495
		int IndexOf(object value);

		// Token: 0x06005FB0 RID: 24496
		void Insert(int index, object value);

		// Token: 0x06005FB1 RID: 24497
		void Remove(object value);

		// Token: 0x06005FB2 RID: 24498
		void RemoveAt(int index);
	}
}
