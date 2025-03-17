using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007F9 RID: 2041
	[NullableContext(2)]
	public interface IEqualityComparer<in T>
	{
		// Token: 0x06006199 RID: 24985
		bool Equals(T x, T y);

		// Token: 0x0600619A RID: 24986
		[NullableContext(1)]
		int GetHashCode([DisallowNull] T obj);
	}
}
