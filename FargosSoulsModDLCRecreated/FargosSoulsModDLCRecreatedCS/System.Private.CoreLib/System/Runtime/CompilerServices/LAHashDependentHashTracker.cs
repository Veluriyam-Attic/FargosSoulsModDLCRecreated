using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004E9 RID: 1257
	[StructLayout(LayoutKind.Sequential)]
	internal class LAHashDependentHashTracker
	{
		// Token: 0x060045DE RID: 17886 RVA: 0x0017A294 File Offset: 0x00179494
		~LAHashDependentHashTracker()
		{
			if (this._dependentHandle.IsAllocated)
			{
				this._dependentHandle.Free();
			}
		}

		// Token: 0x040010A8 RID: 4264
		private GCHandle _dependentHandle;

		// Token: 0x040010A9 RID: 4265
		private IntPtr _loaderAllocator;
	}
}
