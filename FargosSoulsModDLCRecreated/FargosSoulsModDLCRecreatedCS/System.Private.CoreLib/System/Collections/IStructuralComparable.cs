using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007BE RID: 1982
	[NullableContext(1)]
	public interface IStructuralComparable
	{
		// Token: 0x06005FB3 RID: 24499
		int CompareTo([Nullable(2)] object other, IComparer comparer);
	}
}
