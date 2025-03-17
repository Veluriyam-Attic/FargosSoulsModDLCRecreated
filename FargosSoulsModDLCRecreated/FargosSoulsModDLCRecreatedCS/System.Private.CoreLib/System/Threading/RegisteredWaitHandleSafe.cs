using System;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x02000271 RID: 625
	internal sealed class RegisteredWaitHandleSafe : CriticalFinalizerObject
	{
		// Token: 0x1700082D RID: 2093
		// (get) Token: 0x06002649 RID: 9801 RVA: 0x00141D17 File Offset: 0x00140F17
		private static IntPtr InvalidHandle
		{
			get
			{
				return new IntPtr(-1);
			}
		}

		// Token: 0x0600264A RID: 9802 RVA: 0x00141D1F File Offset: 0x00140F1F
		internal IntPtr GetHandle()
		{
			return this.registeredWaitHandle;
		}

		// Token: 0x0600264B RID: 9803 RVA: 0x00141D27 File Offset: 0x00140F27
		internal void SetHandle(IntPtr handle)
		{
			this.registeredWaitHandle = handle;
		}

		// Token: 0x0600264C RID: 9804 RVA: 0x00141D30 File Offset: 0x00140F30
		internal void SetWaitObject(WaitHandle waitObject)
		{
			this.m_internalWaitObject = waitObject;
			if (waitObject != null)
			{
				this.m_internalWaitObject.SafeWaitHandle.DangerousAddRef(ref this.bReleaseNeeded);
			}
		}

		// Token: 0x0600264D RID: 9805 RVA: 0x00141D54 File Offset: 0x00140F54
		internal bool Unregister(WaitHandle waitObject)
		{
			bool flag = false;
			bool flag2 = false;
			do
			{
				if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) == 0)
				{
					flag2 = true;
					try
					{
						if (this.ValidHandle())
						{
							flag = RegisteredWaitHandleSafe.UnregisterWaitNative(this.GetHandle(), (waitObject != null) ? waitObject.SafeWaitHandle : null);
							if (flag)
							{
								if (this.bReleaseNeeded)
								{
									this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
									this.bReleaseNeeded = false;
								}
								this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
								this.m_internalWaitObject = null;
								GC.SuppressFinalize(this);
							}
						}
					}
					finally
					{
						this.m_lock = 0;
					}
				}
				Thread.SpinWait(1);
			}
			while (!flag2);
			return flag;
		}

		// Token: 0x0600264E RID: 9806 RVA: 0x00141DF8 File Offset: 0x00140FF8
		private bool ValidHandle()
		{
			return this.registeredWaitHandle != RegisteredWaitHandleSafe.InvalidHandle && this.registeredWaitHandle != IntPtr.Zero;
		}

		// Token: 0x0600264F RID: 9807 RVA: 0x00141E20 File Offset: 0x00141020
		~RegisteredWaitHandleSafe()
		{
			if (Interlocked.CompareExchange(ref this.m_lock, 1, 0) == 0)
			{
				try
				{
					if (this.ValidHandle())
					{
						RegisteredWaitHandleSafe.WaitHandleCleanupNative(this.registeredWaitHandle);
						if (this.bReleaseNeeded)
						{
							this.m_internalWaitObject.SafeWaitHandle.DangerousRelease();
							this.bReleaseNeeded = false;
						}
						this.SetHandle(RegisteredWaitHandleSafe.InvalidHandle);
						this.m_internalWaitObject = null;
					}
				}
				finally
				{
					this.m_lock = 0;
				}
			}
		}

		// Token: 0x06002650 RID: 9808
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void WaitHandleCleanupNative(IntPtr handle);

		// Token: 0x06002651 RID: 9809
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool UnregisterWaitNative(IntPtr handle, SafeHandle waitObject);

		// Token: 0x040009F6 RID: 2550
		private IntPtr registeredWaitHandle = RegisteredWaitHandleSafe.InvalidHandle;

		// Token: 0x040009F7 RID: 2551
		private WaitHandle m_internalWaitObject;

		// Token: 0x040009F8 RID: 2552
		private bool bReleaseNeeded;

		// Token: 0x040009F9 RID: 2553
		private volatile int m_lock;
	}
}
