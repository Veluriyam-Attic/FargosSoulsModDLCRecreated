using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200053A RID: 1338
	public interface ICriticalNotifyCompletion : INotifyCompletion
	{
		// Token: 0x0600473F RID: 18239
		[NullableContext(1)]
		void UnsafeOnCompleted(Action continuation);
	}
}
