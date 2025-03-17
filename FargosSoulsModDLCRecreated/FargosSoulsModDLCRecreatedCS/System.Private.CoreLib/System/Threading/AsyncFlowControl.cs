using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x0200029C RID: 668
	public struct AsyncFlowControl : IDisposable
	{
		// Token: 0x06002793 RID: 10131 RVA: 0x00145685 File Offset: 0x00144885
		internal void Initialize(Thread currentThread)
		{
			this._thread = currentThread;
		}

		// Token: 0x06002794 RID: 10132 RVA: 0x00145690 File Offset: 0x00144890
		public void Undo()
		{
			if (this._thread == null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_CannotUseAFCMultiple);
			}
			if (Thread.CurrentThread != this._thread)
			{
				throw new InvalidOperationException(SR.InvalidOperation_CannotUseAFCOtherThread);
			}
			if (!ExecutionContext.IsFlowSuppressed())
			{
				throw new InvalidOperationException(SR.InvalidOperation_AsyncFlowCtrlCtxMismatch);
			}
			this._thread = null;
			ExecutionContext.RestoreFlow();
		}

		// Token: 0x06002795 RID: 10133 RVA: 0x001456E6 File Offset: 0x001448E6
		public void Dispose()
		{
			this.Undo();
		}

		// Token: 0x06002796 RID: 10134 RVA: 0x001456EE File Offset: 0x001448EE
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is AsyncFlowControl && this.Equals((AsyncFlowControl)obj);
		}

		// Token: 0x06002797 RID: 10135 RVA: 0x00145706 File Offset: 0x00144906
		public bool Equals(AsyncFlowControl obj)
		{
			return this._thread == obj._thread;
		}

		// Token: 0x06002798 RID: 10136 RVA: 0x00145716 File Offset: 0x00144916
		public override int GetHashCode()
		{
			Thread thread = this._thread;
			if (thread == null)
			{
				return 0;
			}
			return thread.GetHashCode();
		}

		// Token: 0x06002799 RID: 10137 RVA: 0x00145729 File Offset: 0x00144929
		public static bool operator ==(AsyncFlowControl a, AsyncFlowControl b)
		{
			return a.Equals(b);
		}

		// Token: 0x0600279A RID: 10138 RVA: 0x00145733 File Offset: 0x00144933
		public static bool operator !=(AsyncFlowControl a, AsyncFlowControl b)
		{
			return !(a == b);
		}

		// Token: 0x04000A6D RID: 2669
		private Thread _thread;
	}
}
