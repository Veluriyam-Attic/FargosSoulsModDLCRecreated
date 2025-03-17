using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000126 RID: 294
	[NullableContext(1)]
	public interface IAsyncResult
	{
		// Token: 0x1700015C RID: 348
		// (get) Token: 0x06000F51 RID: 3921
		bool IsCompleted { get; }

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x06000F52 RID: 3922
		WaitHandle AsyncWaitHandle { get; }

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x06000F53 RID: 3923
		[Nullable(2)]
		object AsyncState { [NullableContext(2)] get; }

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x06000F54 RID: 3924
		bool CompletedSynchronously { get; }
	}
}
