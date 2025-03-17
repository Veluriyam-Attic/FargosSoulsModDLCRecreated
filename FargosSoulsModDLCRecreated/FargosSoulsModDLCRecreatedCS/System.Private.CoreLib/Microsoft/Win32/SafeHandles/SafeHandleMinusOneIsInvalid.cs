using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000049 RID: 73
	public abstract class SafeHandleMinusOneIsInvalid : SafeHandle
	{
		// Token: 0x060000CF RID: 207 RVA: 0x000AB195 File Offset: 0x000AA395
		protected SafeHandleMinusOneIsInvalid(bool ownsHandle) : base(new IntPtr(-1), ownsHandle)
		{
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x000AB1A4 File Offset: 0x000AA3A4
		public override bool IsInvalid
		{
			get
			{
				return this.handle == new IntPtr(-1);
			}
		}
	}
}
