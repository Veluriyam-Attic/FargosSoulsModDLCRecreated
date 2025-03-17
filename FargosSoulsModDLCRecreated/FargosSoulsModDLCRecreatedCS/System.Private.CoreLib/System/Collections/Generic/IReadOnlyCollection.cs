using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007FD RID: 2045
	public interface IReadOnlyCollection<[Nullable(2)] out T> : IEnumerable<!0>, IEnumerable
	{
		// Token: 0x1700101D RID: 4125
		// (get) Token: 0x060061A2 RID: 24994
		int Count { get; }
	}
}
