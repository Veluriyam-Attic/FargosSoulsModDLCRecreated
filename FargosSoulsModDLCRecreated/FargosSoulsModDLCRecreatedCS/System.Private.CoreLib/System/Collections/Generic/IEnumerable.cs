using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007F7 RID: 2039
	public interface IEnumerable<[Nullable(2)] out T> : IEnumerable
	{
		// Token: 0x06006197 RID: 24983
		[NullableContext(1)]
		IEnumerator<T> GetEnumerator();
	}
}
