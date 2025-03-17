using System;
using System.Runtime.InteropServices;

namespace System.StubHelpers
{
	// Token: 0x020003B1 RID: 945
	internal sealed class SafeHandleCleanupWorkListElement : CleanupWorkListElement
	{
		// Token: 0x060030E2 RID: 12514 RVA: 0x00168585 File Offset: 0x00167785
		public SafeHandleCleanupWorkListElement(SafeHandle handle)
		{
			this.m_handle = handle;
		}

		// Token: 0x060030E3 RID: 12515 RVA: 0x00168594 File Offset: 0x00167794
		protected override void DestroyCore()
		{
			if (this.m_owned)
			{
				StubHelpers.SafeHandleRelease(this.m_handle);
			}
		}

		// Token: 0x060030E4 RID: 12516 RVA: 0x001685A9 File Offset: 0x001677A9
		public IntPtr AddRef()
		{
			return StubHelpers.SafeHandleAddRef(this.m_handle, ref this.m_owned);
		}

		// Token: 0x04000D78 RID: 3448
		private SafeHandle m_handle;

		// Token: 0x04000D79 RID: 3449
		private bool m_owned;
	}
}
