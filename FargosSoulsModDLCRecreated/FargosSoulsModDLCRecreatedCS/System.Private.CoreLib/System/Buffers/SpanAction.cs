using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000243 RID: 579
	// (Invoke) Token: 0x06002419 RID: 9241
	public delegate void SpanAction<[Nullable(2)] T, [Nullable(2)] in TArg>([Nullable(new byte[]
	{
		0,
		1
	})] Span<T> span, TArg arg);
}
