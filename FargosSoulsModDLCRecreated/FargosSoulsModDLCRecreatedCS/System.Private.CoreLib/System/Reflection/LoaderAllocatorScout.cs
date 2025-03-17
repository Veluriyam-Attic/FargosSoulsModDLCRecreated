using System;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000597 RID: 1431
	internal sealed class LoaderAllocatorScout
	{
		// Token: 0x060049A2 RID: 18850
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool Destroy(IntPtr nativeLoaderAllocator);

		// Token: 0x060049A3 RID: 18851 RVA: 0x00185F2C File Offset: 0x0018512C
		~LoaderAllocatorScout()
		{
			if (!(this.m_nativeLoaderAllocator == IntPtr.Zero))
			{
				if (!LoaderAllocatorScout.Destroy(this.m_nativeLoaderAllocator))
				{
					GC.ReRegisterForFinalize(this);
				}
			}
		}

		// Token: 0x04001213 RID: 4627
		internal IntPtr m_nativeLoaderAllocator;
	}
}
