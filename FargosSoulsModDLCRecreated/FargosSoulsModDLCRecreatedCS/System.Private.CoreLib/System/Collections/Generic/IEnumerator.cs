using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007F8 RID: 2040
	public interface IEnumerator<[Nullable(2)] out T> : IDisposable, IEnumerator
	{
		// Token: 0x1700101B RID: 4123
		// (get) Token: 0x06006198 RID: 24984
		[Nullable(1)]
		T Current { [NullableContext(1)] get; }
	}
}
