using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001B8 RID: 440
	internal interface IValueTupleInternal : ITuple
	{
		// Token: 0x06001ADE RID: 6878
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x06001ADF RID: 6879
		string ToStringEnd();
	}
}
