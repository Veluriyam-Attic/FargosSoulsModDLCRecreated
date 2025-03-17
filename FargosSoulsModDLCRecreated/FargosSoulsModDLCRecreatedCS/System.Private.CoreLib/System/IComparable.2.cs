using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000129 RID: 297
	[NullableContext(2)]
	public interface IComparable<in T>
	{
		// Token: 0x06000F57 RID: 3927
		int CompareTo(T other);
	}
}
