using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007FF RID: 2047
	[NullableContext(1)]
	public interface IReadOnlyList<[Nullable(2)] out T> : IReadOnlyCollection<T>, IEnumerable<!0>, IEnumerable
	{
		// Token: 0x17001021 RID: 4129
		T this[int index]
		{
			get;
		}
	}
}
