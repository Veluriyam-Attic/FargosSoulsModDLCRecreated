using System;

namespace System.Threading
{
	// Token: 0x02000296 RID: 662
	internal interface IDeferredDisposable
	{
		// Token: 0x06002766 RID: 10086
		void OnFinalRelease(bool disposed);
	}
}
