using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x02000351 RID: 849
	[NullableContext(2)]
	public interface IValueTaskSource<out TResult>
	{
		// Token: 0x06002C8B RID: 11403
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x06002C8C RID: 11404
		void OnCompleted([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x06002C8D RID: 11405
		[NullableContext(1)]
		TResult GetResult(short token);
	}
}
