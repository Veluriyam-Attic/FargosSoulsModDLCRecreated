using System;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Collections.Generic
{
	// Token: 0x02000803 RID: 2051
	public static class KeyValuePair
	{
		// Token: 0x060061BF RID: 25023 RVA: 0x001D22D9 File Offset: 0x001D14D9
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1,
			1
		})]
		public static KeyValuePair<TKey, TValue> Create<[Nullable(2)] TKey, [Nullable(2)] TValue>(TKey key, TValue value)
		{
			return new KeyValuePair<TKey, TValue>(key, value);
		}

		// Token: 0x060061C0 RID: 25024 RVA: 0x001D22E4 File Offset: 0x001D14E4
		internal unsafe static string PairToString(object key, object value)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)128], 64);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			valueStringBuilder.Append('[');
			if (key != null)
			{
				valueStringBuilder.Append(key.ToString());
			}
			valueStringBuilder.Append(", ");
			if (value != null)
			{
				valueStringBuilder.Append(value.ToString());
			}
			valueStringBuilder.Append(']');
			return valueStringBuilder.ToString();
		}
	}
}
