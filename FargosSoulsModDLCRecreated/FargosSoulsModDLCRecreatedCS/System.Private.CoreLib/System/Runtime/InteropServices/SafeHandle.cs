using System;
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200049E RID: 1182
	public abstract class SafeHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x060044CC RID: 17612 RVA: 0x00179987 File Offset: 0x00178B87
		protected SafeHandle(IntPtr invalidHandleValue, bool ownsHandle)
		{
			this.handle = invalidHandleValue;
			this._state = 4;
			this._ownsHandle = ownsHandle;
			if (!ownsHandle)
			{
				GC.SuppressFinalize(this);
			}
			this._fullyInitialized = true;
		}

		// Token: 0x060044CD RID: 17613 RVA: 0x001799B8 File Offset: 0x00178BB8
		~SafeHandle()
		{
			if (this._fullyInitialized)
			{
				this.Dispose(false);
			}
		}

		// Token: 0x060044CE RID: 17614 RVA: 0x001799F0 File Offset: 0x00178BF0
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x060044CF RID: 17615 RVA: 0x001799F9 File Offset: 0x00178BF9
		public IntPtr DangerousGetHandle()
		{
			return this.handle;
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x060044D0 RID: 17616 RVA: 0x00179A01 File Offset: 0x00178C01
		public bool IsClosed
		{
			get
			{
				return (this._state & 1) == 1;
			}
		}

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x060044D1 RID: 17617
		public abstract bool IsInvalid { get; }

		// Token: 0x060044D2 RID: 17618 RVA: 0x00179A10 File Offset: 0x00178C10
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x060044D3 RID: 17619 RVA: 0x00179A18 File Offset: 0x00178C18
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060044D4 RID: 17620 RVA: 0x00179A27 File Offset: 0x00178C27
		protected virtual void Dispose(bool disposing)
		{
			this.InternalRelease(true);
		}

		// Token: 0x060044D5 RID: 17621 RVA: 0x00179A30 File Offset: 0x00178C30
		public void SetHandleAsInvalid()
		{
			Interlocked.Or(ref this._state, 1);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060044D6 RID: 17622
		protected abstract bool ReleaseHandle();

		// Token: 0x060044D7 RID: 17623 RVA: 0x00179A48 File Offset: 0x00178C48
		public void DangerousAddRef(ref bool success)
		{
			for (;;)
			{
				int state = this._state;
				if ((state & 1) != 0)
				{
					break;
				}
				int value = state + 4;
				if (Interlocked.CompareExchange(ref this._state, value, state) == state)
				{
					goto Block_1;
				}
			}
			throw new ObjectDisposedException("SafeHandle", SR.ObjectDisposed_SafeHandleClosed);
			Block_1:
			success = true;
		}

		// Token: 0x060044D8 RID: 17624 RVA: 0x00179A8A File Offset: 0x00178C8A
		public void DangerousRelease()
		{
			this.InternalRelease(false);
		}

		// Token: 0x060044D9 RID: 17625 RVA: 0x00179A94 File Offset: 0x00178C94
		private void InternalRelease(bool disposeOrFinalizeOperation)
		{
			bool flag;
			for (;;)
			{
				int state = this._state;
				if (disposeOrFinalizeOperation && (state & 2) != 0)
				{
					break;
				}
				if ((state & -4) == 0)
				{
					goto Block_2;
				}
				flag = ((state & -3) == 4 && this._ownsHandle && !this.IsInvalid);
				int num = state - 4;
				if ((state & -4) == 4)
				{
					num |= 1;
				}
				if (disposeOrFinalizeOperation)
				{
					num |= 2;
				}
				if (Interlocked.CompareExchange(ref this._state, num, state) == state)
				{
					goto Block_7;
				}
			}
			return;
			Block_2:
			throw new ObjectDisposedException("SafeHandle", SR.ObjectDisposed_SafeHandleClosed);
			Block_7:
			if (flag)
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				this.ReleaseHandle();
				Marshal.SetLastWin32Error(lastWin32Error);
			}
		}

		// Token: 0x04000F62 RID: 3938
		protected IntPtr handle;

		// Token: 0x04000F63 RID: 3939
		private volatile int _state;

		// Token: 0x04000F64 RID: 3940
		private readonly bool _ownsHandle;

		// Token: 0x04000F65 RID: 3941
		private volatile bool _fullyInitialized;
	}
}
