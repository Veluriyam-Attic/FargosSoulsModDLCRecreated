using System;

namespace System.Threading
{
	// Token: 0x02000265 RID: 613
	internal class _IOCompletionCallback
	{
		// Token: 0x06002584 RID: 9604 RVA: 0x00140F16 File Offset: 0x00140116
		internal _IOCompletionCallback(IOCompletionCallback ioCompletionCallback, ExecutionContext executionContext)
		{
			this._ioCompletionCallback = ioCompletionCallback;
			this._executionContext = executionContext;
		}

		// Token: 0x06002585 RID: 9605 RVA: 0x00140F2C File Offset: 0x0014012C
		internal static void IOCompletionCallback_Context(object state)
		{
			_IOCompletionCallback iocompletionCallback = (_IOCompletionCallback)state;
			iocompletionCallback._ioCompletionCallback(iocompletionCallback._errorCode, iocompletionCallback._numBytes, iocompletionCallback._pNativeOverlapped);
		}

		// Token: 0x06002586 RID: 9606 RVA: 0x00140F60 File Offset: 0x00140160
		internal unsafe static void PerformIOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pNativeOverlapped)
		{
			do
			{
				OverlappedData overlappedFromNative = OverlappedData.GetOverlappedFromNative(pNativeOverlapped);
				IOCompletionCallback iocompletionCallback = overlappedFromNative._callback as IOCompletionCallback;
				if (iocompletionCallback != null)
				{
					iocompletionCallback(errorCode, numBytes, pNativeOverlapped);
				}
				else
				{
					_IOCompletionCallback iocompletionCallback2 = (_IOCompletionCallback)overlappedFromNative._callback;
					iocompletionCallback2._errorCode = errorCode;
					iocompletionCallback2._numBytes = numBytes;
					iocompletionCallback2._pNativeOverlapped = pNativeOverlapped;
					ExecutionContext.RunInternal(iocompletionCallback2._executionContext, _IOCompletionCallback._ccb, iocompletionCallback2);
				}
				OverlappedData.CheckVMForIOPacket(out pNativeOverlapped, out errorCode, out numBytes);
			}
			while (pNativeOverlapped != null);
		}

		// Token: 0x040009C8 RID: 2504
		private readonly IOCompletionCallback _ioCompletionCallback;

		// Token: 0x040009C9 RID: 2505
		private readonly ExecutionContext _executionContext;

		// Token: 0x040009CA RID: 2506
		private uint _errorCode;

		// Token: 0x040009CB RID: 2507
		private uint _numBytes;

		// Token: 0x040009CC RID: 2508
		private unsafe NativeOverlapped* _pNativeOverlapped;

		// Token: 0x040009CD RID: 2509
		internal static ContextCallback _ccb = new ContextCallback(_IOCompletionCallback.IOCompletionCallback_Context);
	}
}
