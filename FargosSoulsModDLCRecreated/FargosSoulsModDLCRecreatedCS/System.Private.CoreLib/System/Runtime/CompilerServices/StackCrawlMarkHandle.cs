using System;
using System.Threading;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200056B RID: 1387
	internal ref struct StackCrawlMarkHandle
	{
		// Token: 0x060047BA RID: 18362 RVA: 0x0017E1FC File Offset: 0x0017D3FC
		internal StackCrawlMarkHandle(ref StackCrawlMark stackMark)
		{
			this._ptr = Unsafe.AsPointer<StackCrawlMark>(ref stackMark);
		}

		// Token: 0x04001152 RID: 4434
		private unsafe void* _ptr;
	}
}
