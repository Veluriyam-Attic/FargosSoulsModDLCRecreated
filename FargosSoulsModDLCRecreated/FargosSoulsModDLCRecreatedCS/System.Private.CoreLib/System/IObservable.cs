using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200013C RID: 316
	[NullableContext(1)]
	public interface IObservable<[Nullable(2)] out T>
	{
		// Token: 0x0600102B RID: 4139
		IDisposable Subscribe(IObserver<T> observer);
	}
}
