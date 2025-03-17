using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Collections.Generic
{
	// Token: 0x020007EF RID: 2031
	public interface IAsyncEnumerator<[Nullable(2)] out T> : IAsyncDisposable
	{
		// Token: 0x0600617D RID: 24957
		ValueTask<bool> MoveNextAsync();

		// Token: 0x17001011 RID: 4113
		// (get) Token: 0x0600617E RID: 24958
		[Nullable(1)]
		T Current { [NullableContext(1)] get; }
	}
}
