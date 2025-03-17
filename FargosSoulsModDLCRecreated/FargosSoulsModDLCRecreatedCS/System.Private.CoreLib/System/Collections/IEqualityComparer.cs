using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007BB RID: 1979
	[NullableContext(1)]
	public interface IEqualityComparer
	{
		// Token: 0x06005FA5 RID: 24485
		[NullableContext(2)]
		bool Equals(object x, object y);

		// Token: 0x06005FA6 RID: 24486
		int GetHashCode(object obj);
	}
}
