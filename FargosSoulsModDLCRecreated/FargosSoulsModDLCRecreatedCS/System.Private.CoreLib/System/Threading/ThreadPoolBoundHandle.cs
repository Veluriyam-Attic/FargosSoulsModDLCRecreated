using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x02000260 RID: 608
	public sealed class ThreadPoolBoundHandle : IDisposable
	{
		// Token: 0x06002527 RID: 9511 RVA: 0x001407E0 File Offset: 0x0013F9E0
		private ThreadPoolBoundHandle(SafeHandle handle)
		{
			this._handle = handle;
		}

		// Token: 0x17000813 RID: 2067
		// (get) Token: 0x06002528 RID: 9512 RVA: 0x001407EF File Offset: 0x0013F9EF
		[Nullable(1)]
		public SafeHandle Handle
		{
			[NullableContext(1)]
			get
			{
				return this._handle;
			}
		}

		// Token: 0x06002529 RID: 9513 RVA: 0x001407F7 File Offset: 0x0013F9F7
		[NullableContext(1)]
		public static ThreadPoolBoundHandle BindHandle(SafeHandle handle)
		{
			if (handle == null)
			{
				throw new ArgumentNullException("handle");
			}
			if (handle.IsClosed || handle.IsInvalid)
			{
				throw new ArgumentException(SR.Argument_InvalidHandle, "handle");
			}
			return ThreadPoolBoundHandle.BindHandleCore(handle);
		}

		// Token: 0x0600252A RID: 9514 RVA: 0x00140830 File Offset: 0x0013FA30
		[CLSCompliant(false)]
		[NullableContext(2)]
		[return: Nullable(0)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped([Nullable(1)] IOCompletionCallback callback, object state, object pinData)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this.EnsureNotDisposed();
			return new ThreadPoolBoundHandleOverlapped(callback, state, pinData, null)
			{
				_boundHandle = this
			}._nativeOverlapped;
		}

		// Token: 0x0600252B RID: 9515 RVA: 0x00140868 File Offset: 0x0013FA68
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* AllocateNativeOverlapped([Nullable(1)] PreAllocatedOverlapped preAllocated)
		{
			if (preAllocated == null)
			{
				throw new ArgumentNullException("preAllocated");
			}
			this.EnsureNotDisposed();
			preAllocated.AddRef();
			NativeOverlapped* nativeOverlapped;
			try
			{
				ThreadPoolBoundHandleOverlapped overlapped = preAllocated._overlapped;
				if (overlapped._boundHandle != null)
				{
					throw new ArgumentException(SR.Argument_PreAllocatedAlreadyAllocated, "preAllocated");
				}
				overlapped._boundHandle = this;
				nativeOverlapped = overlapped._nativeOverlapped;
			}
			catch
			{
				preAllocated.Release();
				throw;
			}
			return nativeOverlapped;
		}

		// Token: 0x0600252C RID: 9516 RVA: 0x001408DC File Offset: 0x0013FADC
		[CLSCompliant(false)]
		public unsafe void FreeNativeOverlapped(NativeOverlapped* overlapped)
		{
			if (overlapped == null)
			{
				throw new ArgumentNullException("overlapped");
			}
			ThreadPoolBoundHandleOverlapped overlappedWrapper = ThreadPoolBoundHandle.GetOverlappedWrapper(overlapped);
			if (overlappedWrapper._boundHandle != this)
			{
				throw new ArgumentException(SR.Argument_NativeOverlappedWrongBoundHandle, "overlapped");
			}
			if (overlappedWrapper._preAllocated != null)
			{
				overlappedWrapper._preAllocated.Release();
				return;
			}
			Overlapped.Free(overlapped);
		}

		// Token: 0x0600252D RID: 9517 RVA: 0x00140934 File Offset: 0x0013FB34
		[CLSCompliant(false)]
		[return: Nullable(2)]
		public unsafe static object GetNativeOverlappedState(NativeOverlapped* overlapped)
		{
			if (overlapped == null)
			{
				throw new ArgumentNullException("overlapped");
			}
			ThreadPoolBoundHandleOverlapped overlappedWrapper = ThreadPoolBoundHandle.GetOverlappedWrapper(overlapped);
			return overlappedWrapper._userState;
		}

		// Token: 0x0600252E RID: 9518 RVA: 0x00140960 File Offset: 0x0013FB60
		private unsafe static ThreadPoolBoundHandleOverlapped GetOverlappedWrapper(NativeOverlapped* overlapped)
		{
			ThreadPoolBoundHandleOverlapped result;
			try
			{
				result = (ThreadPoolBoundHandleOverlapped)Overlapped.Unpack(overlapped);
			}
			catch (NullReferenceException innerException)
			{
				throw new ArgumentException(SR.Argument_NativeOverlappedAlreadyFree, "overlapped", innerException);
			}
			return result;
		}

		// Token: 0x0600252F RID: 9519 RVA: 0x001409A0 File Offset: 0x0013FBA0
		public void Dispose()
		{
			this._isDisposed = true;
		}

		// Token: 0x06002530 RID: 9520 RVA: 0x001409A9 File Offset: 0x0013FBA9
		private void EnsureNotDisposed()
		{
			if (this._isDisposed)
			{
				throw new ObjectDisposedException(base.GetType().ToString());
			}
		}

		// Token: 0x06002531 RID: 9521 RVA: 0x001409C4 File Offset: 0x0013FBC4
		private static ThreadPoolBoundHandle BindHandleCore(SafeHandle handle)
		{
			try
			{
				bool flag = ThreadPool.BindHandle(handle);
			}
			catch (Exception ex)
			{
				if (ex.HResult == -2147024890)
				{
					throw new ArgumentException(SR.Argument_InvalidHandle, "handle");
				}
				if (ex.HResult == -2147024809)
				{
					throw new ArgumentException(SR.Argument_AlreadyBoundOrSyncHandle, "handle");
				}
				throw;
			}
			return new ThreadPoolBoundHandle(handle);
		}

		// Token: 0x040009BD RID: 2493
		private readonly SafeHandle _handle;

		// Token: 0x040009BE RID: 2494
		private bool _isDisposed;
	}
}
