using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020001A1 RID: 417
	internal interface ITupleInternal : ITuple
	{
		// Token: 0x06001959 RID: 6489
		string ToString(StringBuilder sb);

		// Token: 0x0600195A RID: 6490
		int GetHashCode(IEqualityComparer comparer);
	}
}
