using System;

namespace System.Text
{
	// Token: 0x02000392 RID: 914
	internal static class StringBuilderCache
	{
		// Token: 0x06003056 RID: 12374 RVA: 0x001650F4 File Offset: 0x001642F4
		public static StringBuilder Acquire(int capacity = 16)
		{
			if (capacity <= 360)
			{
				StringBuilder stringBuilder = StringBuilderCache.t_cachedInstance;
				if (stringBuilder != null && capacity <= stringBuilder.Capacity)
				{
					StringBuilderCache.t_cachedInstance = null;
					stringBuilder.Clear();
					return stringBuilder;
				}
			}
			return new StringBuilder(capacity);
		}

		// Token: 0x06003057 RID: 12375 RVA: 0x00165130 File Offset: 0x00164330
		public static void Release(StringBuilder sb)
		{
			if (sb.Capacity <= 360)
			{
				StringBuilderCache.t_cachedInstance = sb;
			}
		}

		// Token: 0x06003058 RID: 12376 RVA: 0x00165148 File Offset: 0x00164348
		public static string GetStringAndRelease(StringBuilder sb)
		{
			string result = sb.ToString();
			StringBuilderCache.Release(sb);
			return result;
		}

		// Token: 0x04000D44 RID: 3396
		[ThreadStatic]
		private static StringBuilder t_cachedInstance;
	}
}
