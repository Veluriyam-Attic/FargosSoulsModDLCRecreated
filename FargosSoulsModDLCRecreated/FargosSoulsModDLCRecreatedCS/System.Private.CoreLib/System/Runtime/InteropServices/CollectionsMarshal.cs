using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200046D RID: 1133
	public static class CollectionsMarshal
	{
		// Token: 0x06004446 RID: 17478 RVA: 0x00178D80 File Offset: 0x00177F80
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			2,
			1
		})] List<T> list)
		{
			if (list != null)
			{
				return new Span<T>(list._items, 0, list._size);
			}
			return default(Span<T>);
		}
	}
}
