using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000267 RID: 615
	[Nullable(0)]
	[NullableContext(2)]
	public class Overlapped
	{
		// Token: 0x06002592 RID: 9618 RVA: 0x001410D3 File Offset: 0x001402D3
		public Overlapped()
		{
			this._overlappedData = new OverlappedData(this);
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x001410E7 File Offset: 0x001402E7
		public unsafe Overlapped(int offsetLo, int offsetHi, IntPtr hEvent, IAsyncResult ar) : this()
		{
			*this._overlappedData.OffsetLow = offsetLo;
			*this._overlappedData.OffsetHigh = offsetHi;
			*this._overlappedData.EventHandle = hEvent;
			this._overlappedData._asyncResult = ar;
		}

		// Token: 0x06002594 RID: 9620 RVA: 0x00141123 File Offset: 0x00140323
		[Obsolete("This constructor is not 64-bit compatible.  Use the constructor that takes an IntPtr for the event handle.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public Overlapped(int offsetLo, int offsetHi, int hEvent, IAsyncResult ar) : this(offsetLo, offsetHi, new IntPtr(hEvent), ar)
		{
		}

		// Token: 0x17000818 RID: 2072
		// (get) Token: 0x06002595 RID: 9621 RVA: 0x00141135 File Offset: 0x00140335
		// (set) Token: 0x06002596 RID: 9622 RVA: 0x00141142 File Offset: 0x00140342
		public IAsyncResult AsyncResult
		{
			get
			{
				return this._overlappedData._asyncResult;
			}
			set
			{
				this._overlappedData._asyncResult = value;
			}
		}

		// Token: 0x17000819 RID: 2073
		// (get) Token: 0x06002597 RID: 9623 RVA: 0x00141150 File Offset: 0x00140350
		// (set) Token: 0x06002598 RID: 9624 RVA: 0x0014115E File Offset: 0x0014035E
		public unsafe int OffsetLow
		{
			get
			{
				return *this._overlappedData.OffsetLow;
			}
			set
			{
				*this._overlappedData.OffsetLow = value;
			}
		}

		// Token: 0x1700081A RID: 2074
		// (get) Token: 0x06002599 RID: 9625 RVA: 0x0014116D File Offset: 0x0014036D
		// (set) Token: 0x0600259A RID: 9626 RVA: 0x0014117B File Offset: 0x0014037B
		public unsafe int OffsetHigh
		{
			get
			{
				return *this._overlappedData.OffsetHigh;
			}
			set
			{
				*this._overlappedData.OffsetHigh = value;
			}
		}

		// Token: 0x1700081B RID: 2075
		// (get) Token: 0x0600259B RID: 9627 RVA: 0x0014118C File Offset: 0x0014038C
		// (set) Token: 0x0600259C RID: 9628 RVA: 0x001411A7 File Offset: 0x001403A7
		[Obsolete("This property is not 64-bit compatible.  Use EventHandleIntPtr instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		public int EventHandle
		{
			get
			{
				return this.EventHandleIntPtr.ToInt32();
			}
			set
			{
				this.EventHandleIntPtr = new IntPtr(value);
			}
		}

		// Token: 0x1700081C RID: 2076
		// (get) Token: 0x0600259D RID: 9629 RVA: 0x001411B5 File Offset: 0x001403B5
		// (set) Token: 0x0600259E RID: 9630 RVA: 0x001411C3 File Offset: 0x001403C3
		public unsafe IntPtr EventHandleIntPtr
		{
			get
			{
				return *this._overlappedData.EventHandle;
			}
			set
			{
				*this._overlappedData.EventHandle = value;
			}
		}

		// Token: 0x0600259F RID: 9631 RVA: 0x001411D2 File Offset: 0x001403D2
		[NullableContext(0)]
		[Obsolete("This method is not safe.  Use Pack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[CLSCompliant(false)]
		public unsafe NativeOverlapped* Pack([Nullable(2)] IOCompletionCallback iocb)
		{
			return this.Pack(iocb, null);
		}

		// Token: 0x060025A0 RID: 9632 RVA: 0x001411DC File Offset: 0x001403DC
		[CLSCompliant(false)]
		[return: Nullable(0)]
		public unsafe NativeOverlapped* Pack(IOCompletionCallback iocb, object userData)
		{
			return this._overlappedData.Pack(iocb, userData);
		}

		// Token: 0x060025A1 RID: 9633 RVA: 0x001411EB File Offset: 0x001403EB
		[CLSCompliant(false)]
		[Obsolete("This method is not safe.  Use UnsafePack (iocb, userData) instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[NullableContext(0)]
		public unsafe NativeOverlapped* UnsafePack([Nullable(2)] IOCompletionCallback iocb)
		{
			return this.UnsafePack(iocb, null);
		}

		// Token: 0x060025A2 RID: 9634 RVA: 0x001411F5 File Offset: 0x001403F5
		[CLSCompliant(false)]
		[return: Nullable(0)]
		public unsafe NativeOverlapped* UnsafePack(IOCompletionCallback iocb, object userData)
		{
			return this._overlappedData.UnsafePack(iocb, userData);
		}

		// Token: 0x060025A3 RID: 9635 RVA: 0x00141204 File Offset: 0x00140404
		[NullableContext(0)]
		[CLSCompliant(false)]
		[return: Nullable(1)]
		public unsafe static Overlapped Unpack(NativeOverlapped* nativeOverlappedPtr)
		{
			if (nativeOverlappedPtr == null)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			return OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr)._overlapped;
		}

		// Token: 0x060025A4 RID: 9636 RVA: 0x00141221 File Offset: 0x00140421
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe static void Free(NativeOverlapped* nativeOverlappedPtr)
		{
			if (nativeOverlappedPtr == null)
			{
				throw new ArgumentNullException("nativeOverlappedPtr");
			}
			OverlappedData.GetOverlappedFromNative(nativeOverlappedPtr)._overlapped._overlappedData = null;
			OverlappedData.FreeNativeOverlapped(nativeOverlappedPtr);
		}

		// Token: 0x040009D6 RID: 2518
		private OverlappedData _overlappedData;
	}
}
