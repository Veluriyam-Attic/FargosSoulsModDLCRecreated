using System;

namespace System.Collections
{
	// Token: 0x020007BA RID: 1978
	public interface IEnumerator
	{
		// Token: 0x06005FA2 RID: 24482
		bool MoveNext();

		// Token: 0x17000FB6 RID: 4022
		// (get) Token: 0x06005FA3 RID: 24483
		object Current { get; }

		// Token: 0x06005FA4 RID: 24484
		void Reset();
	}
}
