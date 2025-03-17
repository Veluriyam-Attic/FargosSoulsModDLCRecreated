using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000266 RID: 614
	internal sealed class OverlappedData
	{
		// Token: 0x06002588 RID: 9608 RVA: 0x00140FE3 File Offset: 0x001401E3
		internal OverlappedData(Overlapped overlapped)
		{
			this._overlapped = overlapped;
		}

		// Token: 0x17000815 RID: 2069
		// (get) Token: 0x06002589 RID: 9609 RVA: 0x00140FF2 File Offset: 0x001401F2
		internal unsafe ref int OffsetLow
		{
			get
			{
				if (this._pNativeOverlapped == null)
				{
					return ref this._offsetLow;
				}
				return ref this._pNativeOverlapped->OffsetLow;
			}
		}

		// Token: 0x17000816 RID: 2070
		// (get) Token: 0x0600258A RID: 9610 RVA: 0x00141010 File Offset: 0x00140210
		internal unsafe ref int OffsetHigh
		{
			get
			{
				if (this._pNativeOverlapped == null)
				{
					return ref this._offsetHigh;
				}
				return ref this._pNativeOverlapped->OffsetHigh;
			}
		}

		// Token: 0x17000817 RID: 2071
		// (get) Token: 0x0600258B RID: 9611 RVA: 0x0014102E File Offset: 0x0014022E
		internal unsafe ref IntPtr EventHandle
		{
			get
			{
				if (this._pNativeOverlapped == null)
				{
					return ref this._eventHandle;
				}
				return ref this._pNativeOverlapped->EventHandle;
			}
		}

		// Token: 0x0600258C RID: 9612 RVA: 0x0014104C File Offset: 0x0014024C
		internal unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			if (this._pNativeOverlapped != null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_Overlapped_Pack);
			}
			if (iocb != null)
			{
				ExecutionContext executionContext = ExecutionContext.Capture();
				this._callback = ((executionContext != null && !executionContext.IsDefault) ? new _IOCompletionCallback(iocb, executionContext) : iocb);
			}
			else
			{
				this._callback = null;
			}
			this._userObject = userData;
			return this.AllocateNativeOverlapped();
		}

		// Token: 0x0600258D RID: 9613 RVA: 0x001410A8 File Offset: 0x001402A8
		internal unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			if (this._pNativeOverlapped != null)
			{
				throw new InvalidOperationException(SR.InvalidOperation_Overlapped_Pack);
			}
			this._userObject = userData;
			this._callback = iocb;
			return this.AllocateNativeOverlapped();
		}

		// Token: 0x0600258E RID: 9614
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe extern NativeOverlapped* AllocateNativeOverlapped();

		// Token: 0x0600258F RID: 9615
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void FreeNativeOverlapped(NativeOverlapped* nativeOverlappedPtr);

		// Token: 0x06002590 RID: 9616
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern OverlappedData GetOverlappedFromNative(NativeOverlapped* nativeOverlappedPtr);

		// Token: 0x06002591 RID: 9617
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal unsafe static extern void CheckVMForIOPacket(out NativeOverlapped* pNativeOverlapped, out uint errorCode, out uint numBytes);

		// Token: 0x040009CE RID: 2510
		internal IAsyncResult _asyncResult;

		// Token: 0x040009CF RID: 2511
		internal object _callback;

		// Token: 0x040009D0 RID: 2512
		internal readonly Overlapped _overlapped;

		// Token: 0x040009D1 RID: 2513
		private object _userObject;

		// Token: 0x040009D2 RID: 2514
		private unsafe readonly NativeOverlapped* _pNativeOverlapped;

		// Token: 0x040009D3 RID: 2515
		private IntPtr _eventHandle;

		// Token: 0x040009D4 RID: 2516
		private int _offsetLow;

		// Token: 0x040009D5 RID: 2517
		private int _offsetHigh;
	}
}
