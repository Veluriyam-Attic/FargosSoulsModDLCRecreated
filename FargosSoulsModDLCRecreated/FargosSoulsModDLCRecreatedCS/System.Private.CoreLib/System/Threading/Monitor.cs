using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Threading
{
	// Token: 0x02000264 RID: 612
	[NullableContext(1)]
	[Nullable(0)]
	public static class Monitor
	{
		// Token: 0x06002569 RID: 9577
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Enter(object obj);

		// Token: 0x0600256A RID: 9578 RVA: 0x00140DA8 File Offset: 0x0013FFA8
		public static void Enter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnter(obj, ref lockTaken);
		}

		// Token: 0x0600256B RID: 9579 RVA: 0x00140DBA File Offset: 0x0013FFBA
		[DoesNotReturn]
		private static void ThrowLockTakenException()
		{
			throw new ArgumentException(SR.Argument_MustBeFalse, "lockTaken");
		}

		// Token: 0x0600256C RID: 9580
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReliableEnter(object obj, ref bool lockTaken);

		// Token: 0x0600256D RID: 9581
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void Exit(object obj);

		// Token: 0x0600256E RID: 9582 RVA: 0x00140DCC File Offset: 0x0013FFCC
		public static bool TryEnter(object obj)
		{
			bool result = false;
			Monitor.TryEnter(obj, 0, ref result);
			return result;
		}

		// Token: 0x0600256F RID: 9583 RVA: 0x00140DE5 File Offset: 0x0013FFE5
		public static void TryEnter(object obj, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, 0, ref lockTaken);
		}

		// Token: 0x06002570 RID: 9584 RVA: 0x00140DF8 File Offset: 0x0013FFF8
		public static bool TryEnter(object obj, int millisecondsTimeout)
		{
			bool result = false;
			Monitor.TryEnter(obj, millisecondsTimeout, ref result);
			return result;
		}

		// Token: 0x06002571 RID: 9585 RVA: 0x00140E14 File Offset: 0x00140014
		private static int MillisecondsTimeoutFromTimeSpan(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			return (int)num;
		}

		// Token: 0x06002572 RID: 9586 RVA: 0x00140E4A File Offset: 0x0014004A
		public static bool TryEnter(object obj, TimeSpan timeout)
		{
			return Monitor.TryEnter(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout));
		}

		// Token: 0x06002573 RID: 9587 RVA: 0x00140E58 File Offset: 0x00140058
		public static void TryEnter(object obj, int millisecondsTimeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, millisecondsTimeout, ref lockTaken);
		}

		// Token: 0x06002574 RID: 9588 RVA: 0x00140E6B File Offset: 0x0014006B
		public static void TryEnter(object obj, TimeSpan timeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				Monitor.ThrowLockTakenException();
			}
			Monitor.ReliableEnterTimeout(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), ref lockTaken);
		}

		// Token: 0x06002575 RID: 9589
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ReliableEnterTimeout(object obj, int timeout, ref bool lockTaken);

		// Token: 0x06002576 RID: 9590 RVA: 0x00140E83 File Offset: 0x00140083
		public static bool IsEntered(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.IsEnteredNative(obj);
		}

		// Token: 0x06002577 RID: 9591
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool IsEnteredNative(object obj);

		// Token: 0x06002578 RID: 9592
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool ObjWait(bool exitContext, int millisecondsTimeout, object obj);

		// Token: 0x06002579 RID: 9593 RVA: 0x00140E99 File Offset: 0x00140099
		public static bool Wait(object obj, int millisecondsTimeout, bool exitContext)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			return Monitor.ObjWait(exitContext, millisecondsTimeout, obj);
		}

		// Token: 0x0600257A RID: 9594 RVA: 0x00140EB1 File Offset: 0x001400B1
		public static bool Wait(object obj, TimeSpan timeout, bool exitContext)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), exitContext);
		}

		// Token: 0x0600257B RID: 9595 RVA: 0x00140EC0 File Offset: 0x001400C0
		public static bool Wait(object obj, int millisecondsTimeout)
		{
			return Monitor.Wait(obj, millisecondsTimeout, false);
		}

		// Token: 0x0600257C RID: 9596 RVA: 0x00140ECA File Offset: 0x001400CA
		public static bool Wait(object obj, TimeSpan timeout)
		{
			return Monitor.Wait(obj, Monitor.MillisecondsTimeoutFromTimeSpan(timeout), false);
		}

		// Token: 0x0600257D RID: 9597 RVA: 0x00140ED9 File Offset: 0x001400D9
		public static bool Wait(object obj)
		{
			return Monitor.Wait(obj, -1, false);
		}

		// Token: 0x0600257E RID: 9598
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ObjPulse(object obj);

		// Token: 0x0600257F RID: 9599 RVA: 0x00140EE3 File Offset: 0x001400E3
		public static void Pulse(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulse(obj);
		}

		// Token: 0x06002580 RID: 9600
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void ObjPulseAll(object obj);

		// Token: 0x06002581 RID: 9601 RVA: 0x00140EF9 File Offset: 0x001400F9
		public static void PulseAll(object obj)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			Monitor.ObjPulseAll(obj);
		}

		// Token: 0x17000814 RID: 2068
		// (get) Token: 0x06002582 RID: 9602 RVA: 0x00140F0F File Offset: 0x0014010F
		public static long LockContentionCount
		{
			get
			{
				return Monitor.GetLockContentionCount();
			}
		}

		// Token: 0x06002583 RID: 9603
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern long GetLockContentionCount();
	}
}
