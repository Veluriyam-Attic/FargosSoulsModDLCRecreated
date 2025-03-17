using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200004D RID: 77
	internal sealed class SafeFindHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000DD RID: 221 RVA: 0x000AB1EC File Offset: 0x000AA3EC
		internal SafeFindHandle() : base(true)
		{
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000AB265 File Offset: 0x000AA465
		protected override bool ReleaseHandle()
		{
			return Interop.Kernel32.FindClose(this.handle);
		}
	}
}
