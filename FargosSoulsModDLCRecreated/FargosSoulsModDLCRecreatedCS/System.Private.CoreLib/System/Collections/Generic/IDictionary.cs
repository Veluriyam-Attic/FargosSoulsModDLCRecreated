using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007F3 RID: 2035
	[NullableContext(1)]
	public interface IDictionary<[Nullable(2)] TKey, [Nullable(2)] TValue> : ICollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x17001015 RID: 4117
		TValue this[TKey key]
		{
			get;
			set;
		}

		// Token: 0x17001016 RID: 4118
		// (get) Token: 0x0600618B RID: 24971
		ICollection<TKey> Keys { get; }

		// Token: 0x17001017 RID: 4119
		// (get) Token: 0x0600618C RID: 24972
		ICollection<TValue> Values { get; }

		// Token: 0x0600618D RID: 24973
		bool ContainsKey(TKey key);

		// Token: 0x0600618E RID: 24974
		void Add(TKey key, TValue value);

		// Token: 0x0600618F RID: 24975
		bool Remove(TKey key);

		// Token: 0x06006190 RID: 24976
		bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);
	}
}
