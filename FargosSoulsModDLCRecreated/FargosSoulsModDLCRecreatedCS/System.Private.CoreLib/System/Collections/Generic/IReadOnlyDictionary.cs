using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007FE RID: 2046
	[NullableContext(1)]
	public interface IReadOnlyDictionary<[Nullable(2)] TKey, [Nullable(2)] TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>, IEnumerable<KeyValuePair<!0, !1>>, IEnumerable
	{
		// Token: 0x060061A3 RID: 24995
		bool ContainsKey(TKey key);

		// Token: 0x060061A4 RID: 24996
		bool TryGetValue(TKey key, [MaybeNullWhen(false)] out TValue value);

		// Token: 0x1700101E RID: 4126
		TValue this[TKey key]
		{
			get;
		}

		// Token: 0x1700101F RID: 4127
		// (get) Token: 0x060061A6 RID: 24998
		IEnumerable<TKey> Keys { get; }

		// Token: 0x17001020 RID: 4128
		// (get) Token: 0x060061A7 RID: 24999
		IEnumerable<TValue> Values { get; }
	}
}
