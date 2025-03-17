using System;
using System.Runtime.ConstrainedExecution;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000476 RID: 1142
	public abstract class CriticalHandle : CriticalFinalizerObject, IDisposable
	{
		// Token: 0x0600445B RID: 17499 RVA: 0x00179014 File Offset: 0x00178214
		protected CriticalHandle(IntPtr invalidHandleValue)
		{
			this.handle = invalidHandleValue;
		}

		// Token: 0x0600445C RID: 17500 RVA: 0x00179024 File Offset: 0x00178224
		~CriticalHandle()
		{
			this.Dispose(false);
		}

		// Token: 0x0600445D RID: 17501 RVA: 0x00179054 File Offset: 0x00178254
		private void Cleanup()
		{
			if (this.IsClosed)
			{
				return;
			}
			this._isClosed = true;
			if (this.IsInvalid)
			{
				return;
			}
			int lastWin32Error = Marshal.GetLastWin32Error();
			this.ReleaseHandle();
			Marshal.SetLastWin32Error(lastWin32Error);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600445E RID: 17502 RVA: 0x00179093 File Offset: 0x00178293
		protected void SetHandle(IntPtr handle)
		{
			this.handle = handle;
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x0600445F RID: 17503 RVA: 0x0017909C File Offset: 0x0017829C
		public bool IsClosed
		{
			get
			{
				return this._isClosed;
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06004460 RID: 17504
		public abstract bool IsInvalid { get; }

		// Token: 0x06004461 RID: 17505 RVA: 0x001790A4 File Offset: 0x001782A4
		public void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06004462 RID: 17506 RVA: 0x001790A4 File Offset: 0x001782A4
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06004463 RID: 17507 RVA: 0x001790AD File Offset: 0x001782AD
		protected virtual void Dispose(bool disposing)
		{
			this.Cleanup();
		}

		// Token: 0x06004464 RID: 17508 RVA: 0x001790B5 File Offset: 0x001782B5
		public void SetHandleAsInvalid()
		{
			this._isClosed = true;
			GC.SuppressFinalize(this);
		}

		// Token: 0x06004465 RID: 17509
		protected abstract bool ReleaseHandle();

		// Token: 0x04000F24 RID: 3876
		protected IntPtr handle;

		// Token: 0x04000F25 RID: 3877
		private bool _isClosed;
	}
}
