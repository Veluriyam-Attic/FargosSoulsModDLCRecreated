using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000539 RID: 1337
	[NullableContext(1)]
	public interface INotifyCompletion
	{
		// Token: 0x0600473E RID: 18238
		void OnCompleted(Action continuation);
	}
}
