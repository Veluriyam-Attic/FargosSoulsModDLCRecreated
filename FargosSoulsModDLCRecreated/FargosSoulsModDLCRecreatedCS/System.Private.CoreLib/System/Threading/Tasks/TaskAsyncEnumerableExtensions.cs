using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000322 RID: 802
	[Nullable(0)]
	[NullableContext(1)]
	public static class TaskAsyncEnumerableExtensions
	{
		// Token: 0x06002B26 RID: 11046 RVA: 0x0015147D File Offset: 0x0015067D
		public static ConfiguredAsyncDisposable ConfigureAwait(this IAsyncDisposable source, bool continueOnCapturedContext)
		{
			return new ConfiguredAsyncDisposable(source, continueOnCapturedContext);
		}

		// Token: 0x06002B27 RID: 11047 RVA: 0x00151488 File Offset: 0x00150688
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait<[Nullable(2)] T>(this IAsyncEnumerable<T> source, bool continueOnCapturedContext)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(source, continueOnCapturedContext, default(CancellationToken));
		}

		// Token: 0x06002B28 RID: 11048 RVA: 0x001514A5 File Offset: 0x001506A5
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ConfiguredCancelableAsyncEnumerable<T> WithCancellation<[Nullable(2)] T>(this IAsyncEnumerable<T> source, CancellationToken cancellationToken)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(source, true, cancellationToken);
		}
	}
}
