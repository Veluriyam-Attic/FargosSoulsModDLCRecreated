using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace Microsoft.Win32.SafeHandles
{
	// Token: 0x0200004C RID: 76
	public sealed class SafeFileHandle : SafeHandleZeroOrMinusOneIsInvalid
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x000AB212 File Offset: 0x000AA412
		private SafeFileHandle() : base(true)
		{
			this._isAsync = null;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x000AB227 File Offset: 0x000AA427
		public SafeFileHandle(IntPtr preexistingHandle, bool ownsHandle) : base(ownsHandle)
		{
			base.SetHandle(preexistingHandle);
			this._isAsync = null;
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000AB243 File Offset: 0x000AA443
		// (set) Token: 0x060000D9 RID: 217 RVA: 0x000AB24B File Offset: 0x000AA44B
		internal bool? IsAsync
		{
			get
			{
				return this._isAsync;
			}
			set
			{
				this._isAsync = value;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060000DA RID: 218 RVA: 0x000AB254 File Offset: 0x000AA454
		// (set) Token: 0x060000DB RID: 219 RVA: 0x000AB25C File Offset: 0x000AA45C
		[Nullable(2)]
		internal ThreadPoolBoundHandle ThreadPoolBinding { get; set; }

		// Token: 0x060000DC RID: 220 RVA: 0x000AB205 File Offset: 0x000AA405
		protected override bool ReleaseHandle()
		{
			return Interop.Kernel32.CloseHandle(this.handle);
		}

		// Token: 0x040000CB RID: 203
		private bool? _isAsync;
	}
}
