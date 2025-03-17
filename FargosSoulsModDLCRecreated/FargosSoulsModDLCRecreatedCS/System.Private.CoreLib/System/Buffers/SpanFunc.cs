using System;

namespace System.Buffers
{
	// Token: 0x02000245 RID: 581
	// (Invoke) Token: 0x06002421 RID: 9249
	internal delegate TResult SpanFunc<TSpan, in T1, in T2, in T3, out TResult>(Span<TSpan> span, T1 arg1, T2 arg2, T3 arg3);
}
