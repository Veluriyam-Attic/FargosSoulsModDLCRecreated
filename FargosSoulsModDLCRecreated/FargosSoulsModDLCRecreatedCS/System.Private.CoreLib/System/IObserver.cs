using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200013D RID: 317
	[NullableContext(1)]
	public interface IObserver<[Nullable(2)] in T>
	{
		// Token: 0x0600102C RID: 4140
		void OnNext(T value);

		// Token: 0x0600102D RID: 4141
		void OnError(Exception error);

		// Token: 0x0600102E RID: 4142
		void OnCompleted();
	}
}
