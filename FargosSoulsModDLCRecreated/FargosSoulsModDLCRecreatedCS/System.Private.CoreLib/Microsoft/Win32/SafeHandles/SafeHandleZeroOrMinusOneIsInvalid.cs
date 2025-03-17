using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200004A RID: 74
	public abstract class SafeHandleZeroOrMinusOneIsInvalid : SafeHandle
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x000AB1B7 File Offset: 0x000AA3B7
		protected SafeHandleZeroOrMinusOneIsInvalid(bool ownsHandle) : base(IntPtr.Zero, ownsHandle)
		{
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x000AB1C5 File Offset: 0x000AA3C5
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero || this.handle == new IntPtr(-1);
			}
		}
	}
}
