using System;

namespace System.Threading
{
	// Token: 0x0200026C RID: 620
	internal readonly struct ThreadHandle
	{
		// Token: 0x060025BC RID: 9660 RVA: 0x00141429 File Offset: 0x00140629
		internal ThreadHandle(IntPtr pThread)
		{
			this._ptr = pThread;
		}

		// Token: 0x040009E5 RID: 2533
		private readonly IntPtr _ptr;
	}
}
