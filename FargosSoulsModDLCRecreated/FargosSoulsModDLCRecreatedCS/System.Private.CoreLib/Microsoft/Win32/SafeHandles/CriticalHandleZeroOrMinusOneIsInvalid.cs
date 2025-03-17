using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000048 RID: 72
	public abstract class CriticalHandleZeroOrMinusOneIsInvalid : CriticalHandle
	{
		// Token: 0x060000CD RID: 205 RVA: 0x000AB161 File Offset: 0x000AA361
		protected CriticalHandleZeroOrMinusOneIsInvalid() : base(IntPtr.Zero)
		{
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x060000CE RID: 206 RVA: 0x000AB16E File Offset: 0x000AA36E
		public override bool IsInvalid
		{
			get
			{
				return this.handle == IntPtr.Zero || this.handle == new IntPtr(-1);
			}
		}
	}
}
