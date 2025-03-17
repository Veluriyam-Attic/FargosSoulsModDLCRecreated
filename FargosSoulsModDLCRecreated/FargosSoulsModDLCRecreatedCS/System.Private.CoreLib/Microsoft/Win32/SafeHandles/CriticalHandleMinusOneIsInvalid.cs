using System;
using System.Runtime.InteropServices;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x02000047 RID: 71
	public abstract class CriticalHandleMinusOneIsInvalid : CriticalHandle
	{
		// Token: 0x060000CB RID: 203 RVA: 0x000AB140 File Offset: 0x000AA340
		protected CriticalHandleMinusOneIsInvalid() : base(new IntPtr(-1))
		{
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x060000CC RID: 204 RVA: 0x000AB14E File Offset: 0x000AA34E
		public override bool IsInvalid
		{
			get
			{
				return this.handle == new IntPtr(-1);
			}
		}
	}
}
