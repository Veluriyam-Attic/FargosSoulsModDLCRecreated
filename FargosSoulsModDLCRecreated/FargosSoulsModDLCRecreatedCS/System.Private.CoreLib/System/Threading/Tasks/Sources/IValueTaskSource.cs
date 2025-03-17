using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks.Sources
{
	// Token: 0x02000350 RID: 848
	[NullableContext(2)]
	public interface IValueTaskSource
	{
		// Token: 0x06002C88 RID: 11400
		ValueTaskSourceStatus GetStatus(short token);

		// Token: 0x06002C89 RID: 11401
		void OnCompleted([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> continuation, object state, short token, ValueTaskSourceOnCompletedFlags flags);

		// Token: 0x06002C8A RID: 11402
		void GetResult(short token);
	}
}
