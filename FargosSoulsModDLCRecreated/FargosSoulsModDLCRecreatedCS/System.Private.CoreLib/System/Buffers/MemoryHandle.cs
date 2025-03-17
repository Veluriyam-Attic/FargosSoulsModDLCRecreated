using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers
{
	// Token: 0x0200024D RID: 589
	public struct MemoryHandle : IDisposable
	{
		// Token: 0x0600243C RID: 9276 RVA: 0x0013957C File Offset: 0x0013877C
		[CLSCompliant(false)]
		public unsafe MemoryHandle(void* pointer, GCHandle handle = default(GCHandle), [Nullable(2)] IPinnable pinnable = null)
		{
			this._pointer = pointer;
			this._handle = handle;
			this._pinnable = pinnable;
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600243D RID: 9277 RVA: 0x00139593 File Offset: 0x00138793
		[CLSCompliant(false)]
		public unsafe void* Pointer
		{
			get
			{
				return this._pointer;
			}
		}

		// Token: 0x0600243E RID: 9278 RVA: 0x0013959B File Offset: 0x0013879B
		public void Dispose()
		{
			if (this._handle.IsAllocated)
			{
				this._handle.Free();
			}
			if (this._pinnable != null)
			{
				this._pinnable.Unpin();
				this._pinnable = null;
			}
			this._pointer = null;
		}

		// Token: 0x0400097D RID: 2429
		private unsafe void* _pointer;

		// Token: 0x0400097E RID: 2430
		private GCHandle _handle;

		// Token: 0x0400097F RID: 2431
		private IPinnable _pinnable;
	}
}
