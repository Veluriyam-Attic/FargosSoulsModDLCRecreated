using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007B7 RID: 1975
	[NullableContext(1)]
	public interface IDictionary : ICollection, IEnumerable
	{
		// Token: 0x17000FAE RID: 4014
		[Nullable(2)]
		object this[object key]
		{
			[return: Nullable(2)]
			get;
			[param: Nullable(2)]
			set;
		}

		// Token: 0x17000FAF RID: 4015
		// (get) Token: 0x06005F95 RID: 24469
		ICollection Keys { get; }

		// Token: 0x17000FB0 RID: 4016
		// (get) Token: 0x06005F96 RID: 24470
		ICollection Values { get; }

		// Token: 0x06005F97 RID: 24471
		bool Contains(object key);

		// Token: 0x06005F98 RID: 24472
		void Add(object key, [Nullable(2)] object value);

		// Token: 0x06005F99 RID: 24473
		void Clear();

		// Token: 0x17000FB1 RID: 4017
		// (get) Token: 0x06005F9A RID: 24474
		bool IsReadOnly { get; }

		// Token: 0x17000FB2 RID: 4018
		// (get) Token: 0x06005F9B RID: 24475
		bool IsFixedSize { get; }

		// Token: 0x06005F9C RID: 24476
		IDictionaryEnumerator GetEnumerator();

		// Token: 0x06005F9D RID: 24477
		void Remove(object key);
	}
}
