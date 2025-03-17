using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007F2 RID: 2034
	[NullableContext(2)]
	public interface IComparer<in T>
	{
		// Token: 0x06006188 RID: 24968
		int Compare(T x, T y);
	}
}
