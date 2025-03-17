using System;

namespace System.Threading
{
	// Token: 0x02000261 RID: 609
	internal sealed class ThreadPoolBoundHandleOverlapped : Overlapped
	{
		// Token: 0x06002532 RID: 9522 RVA: 0x00140A30 File Offset: 0x0013FC30
		public unsafe ThreadPoolBoundHandleOverlapped(IOCompletionCallback callback, object state, object pinData, PreAllocatedOverlapped preAllocated)
		{
			this._userCallback = callback;
			this._userState = state;
			this._preAllocated = preAllocated;
			this._nativeOverlapped = base.Pack(ThreadPoolBoundHandleOverlapped.s_completionCallback, pinData);
			this._nativeOverlapped->OffsetLow = 0;
			this._nativeOverlapped->OffsetHigh = 0;
		}

		// Token: 0x06002533 RID: 9523 RVA: 0x00140A84 File Offset: 0x0013FC84
		private unsafe static void CompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* nativeOverlapped)
		{
			ThreadPoolBoundHandleOverlapped threadPoolBoundHandleOverlapped = (ThreadPoolBoundHandleOverlapped)Overlapped.Unpack(nativeOverlapped);
			if (threadPoolBoundHandleOverlapped._completed)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NativeOverlappedReused);
			}
			threadPoolBoundHandleOverlapped._completed = true;
			if (threadPoolBoundHandleOverlapped._boundHandle == null)
			{
				throw new InvalidOperationException(SR.Argument_NativeOverlappedAlreadyFree);
			}
			threadPoolBoundHandleOverlapped._userCallback(errorCode, numBytes, nativeOverlapped);
		}

		// Token: 0x040009BF RID: 2495
		private static readonly IOCompletionCallback s_completionCallback = new IOCompletionCallback(ThreadPoolBoundHandleOverlapped.CompletionCallback);

		// Token: 0x040009C0 RID: 2496
		private readonly IOCompletionCallback _userCallback;

		// Token: 0x040009C1 RID: 2497
		internal readonly object _userState;

		// Token: 0x040009C2 RID: 2498
		internal PreAllocatedOverlapped _preAllocated;

		// Token: 0x040009C3 RID: 2499
		internal unsafe NativeOverlapped* _nativeOverlapped;

		// Token: 0x040009C4 RID: 2500
		internal ThreadPoolBoundHandle _boundHandle;

		// Token: 0x040009C5 RID: 2501
		internal bool _completed;
	}
}
