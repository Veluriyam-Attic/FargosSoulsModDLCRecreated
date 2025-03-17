using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007B5 RID: 1973
	[NullableContext(1)]
	public interface ICollection : IEnumerable
	{
		// Token: 0x06005F8E RID: 24462
		void CopyTo(Array array, int index);

		// Token: 0x17000FAB RID: 4011
		// (get) Token: 0x06005F8F RID: 24463
		int Count { get; }

		// Token: 0x17000FAC RID: 4012
		// (get) Token: 0x06005F90 RID: 24464
		object SyncRoot { get; }

		// Token: 0x17000FAD RID: 4013
		// (get) Token: 0x06005F91 RID: 24465
		bool IsSynchronized { get; }
	}
}
