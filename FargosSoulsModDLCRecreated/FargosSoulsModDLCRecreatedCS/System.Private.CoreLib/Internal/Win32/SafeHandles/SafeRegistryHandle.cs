using System;
using Microsoft.Win32.SafeHandles;

namespace Internal.Win32.SafeHandles
{
	// Token: 0x02000815 RID: 2069
	internal sealed class SafeRegistryHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x0600624E RID: 25166 RVA: 0x000AB1EC File Offset: 0x000AA3EC
		internal SafeRegistryHandle() : base(true)
		{
		}

		// Token: 0x0600624F RID: 25167 RVA: 0x000AB1F5 File Offset: 0x000AA3F5
		public SafeRegistryHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
		}

		// Token: 0x06006250 RID: 25168 RVA: 0x001D3D31 File Offset: 0x001D2F31
		protected override bool ReleaseHandle()
		{
			return Interop.Advapi32.RegCloseKey(this.handle) == 0;
		}
	}
}
