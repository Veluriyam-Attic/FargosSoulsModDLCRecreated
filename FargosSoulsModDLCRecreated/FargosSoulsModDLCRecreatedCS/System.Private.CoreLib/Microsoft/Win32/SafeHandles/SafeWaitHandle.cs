using System;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200004B RID: 75
	public sealed class SafeWaitHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x000AB1EC File Offset: 0x000AA3EC
		private SafeWaitHandle() : base(true)
		{
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x000AB1F5 File Offset: 0x000AA3F5
		public SafeWaitHandle(IntPtr existingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(existingHandle);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000AB205 File Offset: 0x000AA405
		protected override bool ReleaseHandle()
		{
			return Interop.Kernel32.CloseHandle(this.handle);
		}
	}
}
