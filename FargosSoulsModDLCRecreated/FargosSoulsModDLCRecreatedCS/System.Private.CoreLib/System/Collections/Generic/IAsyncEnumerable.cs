using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Collections.Generic
{
	// Token: 0x020007EE RID: 2030
	[NullableContext(1)]
	public interface IAsyncEnumerable<[Nullable(2)] out T>
	{
		// Token: 0x0600617C RID: 24956
		IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken cancellationToken = default(CancellationToken));
	}
}
