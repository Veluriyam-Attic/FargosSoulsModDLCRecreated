using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007BF RID: 1983
	[NullableContext(1)]
	public interface IStructuralEquatable
	{
		// Token: 0x06005FB4 RID: 24500
		bool Equals([Nullable(2)] object other, IEqualityComparer comparer);

		// Token: 0x06005FB5 RID: 24501
		int GetHashCode(IEqualityComparer comparer);
	}
}
