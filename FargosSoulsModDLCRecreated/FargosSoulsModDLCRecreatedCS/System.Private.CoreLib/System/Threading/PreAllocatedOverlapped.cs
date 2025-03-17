using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000262 RID: 610
	public sealed class PreAllocatedOverlapped : IDisposable, IDeferredDisposable
	{
		// Token: 0x06002535 RID: 9525 RVA: 0x00140AEB File Offset: 0x0013FCEB
		[NullableContext(2)]
		[CLSCompliant(false)]
		public PreAllocatedOverlapped([Nullable(1)] IOCompletionCallback callback, object state, object pinData)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._overlapped = new ThreadPoolBoundHandleOverlapped(callback, state, pinData, this);
		}

		// Token: 0x06002536 RID: 9526 RVA: 0x00140B10 File Offset: 0x0013FD10
		internal bool AddRef()
		{
			return this._lifetime.AddRef();
		}

		// Token: 0x06002537 RID: 9527 RVA: 0x00140B1D File Offset: 0x0013FD1D
		internal void Release()
		{
			this._lifetime.Release(this);
		}

		// Token: 0x06002538 RID: 9528 RVA: 0x00140B2B File Offset: 0x0013FD2B
		public void Dispose()
		{
			this._lifetime.Dispose(this);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002539 RID: 9529 RVA: 0x00140B40 File Offset: 0x0013FD40
		~PreAllocatedOverlapped()
		{
			this.Dispose();
		}

		// Token: 0x0600253A RID: 9530 RVA: 0x00140B6C File Offset: 0x0013FD6C
		unsafe void IDeferredDisposable.OnFinalRelease(bool disposed)
		{
			if (this._overlapped != null)
			{
				if (disposed)
				{
					Overlapped.Free(this._overlapped._nativeOverlapped);
					return;
				}
				this._overlapped._boundHandle = null;
				this._overlapped._completed = false;
				*this._overlapped._nativeOverlapped = default(NativeOverlapped);
			}
		}

		// Token: 0x040009C6 RID: 2502
		internal readonly ThreadPoolBoundHandleOverlapped _overlapped;

		// Token: 0x040009C7 RID: 2503
		private DeferredDisposableLifetime<PreAllocatedOverlapped> _lifetime;
	}
}
