using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020002B6 RID: 694
	[DebuggerDisplay("IsHeld = {IsHeld}")]
	[DebuggerTypeProxy(typeof(SpinLock.SystemThreading_SpinLockDebugView))]
	public struct SpinLock
	{
		// Token: 0x06002854 RID: 10324 RVA: 0x00148378 File Offset: 0x00147578
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int CompareExchange(ref int location, int value, int comparand, ref bool success)
		{
			int num = Interlocked.CompareExchange(ref location, value, comparand);
			success = (num == comparand);
			return num;
		}

		// Token: 0x06002855 RID: 10325 RVA: 0x00148395 File Offset: 0x00147595
		public SpinLock(bool enableThreadOwnerTracking)
		{
			this._owner = 0;
			if (!enableThreadOwnerTracking)
			{
				this._owner |= int.MinValue;
			}
		}

		// Token: 0x06002856 RID: 10326 RVA: 0x001483BC File Offset: 0x001475BC
		public void Enter(ref bool lockTaken)
		{
			int owner = this._owner;
			if (lockTaken || (owner & -2147483647) != -2147483648 || SpinLock.CompareExchange(ref this._owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(-1, ref lockTaken);
			}
		}

		// Token: 0x06002857 RID: 10327 RVA: 0x00148400 File Offset: 0x00147600
		public void TryEnter(ref bool lockTaken)
		{
			int owner = this._owner;
			if ((owner & -2147483648) == 0 | lockTaken)
			{
				this.ContinueTryEnter(0, ref lockTaken);
				return;
			}
			if ((owner & 1) != 0)
			{
				lockTaken = false;
				return;
			}
			SpinLock.CompareExchange(ref this._owner, owner | 1, owner, ref lockTaken);
		}

		// Token: 0x06002858 RID: 10328 RVA: 0x00148448 File Offset: 0x00147648
		public void TryEnter(TimeSpan timeout, ref bool lockTaken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SR.SpinLock_TryEnter_ArgumentOutOfRange);
			}
			this.TryEnter((int)timeout.TotalMilliseconds, ref lockTaken);
		}

		// Token: 0x06002859 RID: 10329 RVA: 0x00148494 File Offset: 0x00147694
		public void TryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			int owner = this._owner;
			if ((millisecondsTimeout < -1 | lockTaken) || (owner & -2147483647) != -2147483648 || SpinLock.CompareExchange(ref this._owner, owner | 1, owner, ref lockTaken) != owner)
			{
				this.ContinueTryEnter(millisecondsTimeout, ref lockTaken);
			}
		}

		// Token: 0x0600285A RID: 10330 RVA: 0x001484DC File Offset: 0x001476DC
		private void ContinueTryEnter(int millisecondsTimeout, ref bool lockTaken)
		{
			if (lockTaken)
			{
				lockTaken = false;
				throw new ArgumentException(SR.SpinLock_TryReliableEnter_ArgumentException);
			}
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, SR.SpinLock_TryEnter_ArgumentOutOfRange);
			}
			uint startTime = 0U;
			if (millisecondsTimeout != -1 && millisecondsTimeout != 0)
			{
				startTime = TimeoutHelper.GetTime();
			}
			if (this.IsThreadOwnerTrackingEnabled)
			{
				this.ContinueTryEnterWithThreadTracking(millisecondsTimeout, startTime, ref lockTaken);
				return;
			}
			int num = int.MaxValue;
			int owner = this._owner;
			if ((owner & 1) == 0)
			{
				if (SpinLock.CompareExchange(ref this._owner, owner | 1, owner, ref lockTaken) == owner)
				{
					return;
				}
				if (millisecondsTimeout == 0)
				{
					return;
				}
			}
			else
			{
				if (millisecondsTimeout == 0)
				{
					return;
				}
				if ((owner & 2147483646) != 2147483646)
				{
					num = (Interlocked.Add(ref this._owner, 2) & 2147483646) >> 1;
				}
			}
			SpinWait spinWait = default(SpinWait);
			if (num > Environment.ProcessorCount)
			{
				spinWait.Count = 10;
			}
			for (;;)
			{
				spinWait.SpinOnce(40);
				owner = this._owner;
				if ((owner & 1) == 0)
				{
					int value = ((owner & 2147483646) == 0) ? (owner | 1) : (owner - 2 | 1);
					if (SpinLock.CompareExchange(ref this._owner, value, owner, ref lockTaken) == owner)
					{
						break;
					}
				}
				if (spinWait.Count % 10 == 0 && millisecondsTimeout != -1 && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0)
				{
					goto Block_17;
				}
			}
			return;
			Block_17:
			this.DecrementWaiters();
		}

		// Token: 0x0600285B RID: 10331 RVA: 0x00148604 File Offset: 0x00147804
		private void DecrementWaiters()
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int owner = this._owner;
				if ((owner & 2147483646) == 0)
				{
					break;
				}
				if (Interlocked.CompareExchange(ref this._owner, owner - 2, owner) == owner)
				{
					return;
				}
				spinWait.SpinOnce();
			}
		}

		// Token: 0x0600285C RID: 10332 RVA: 0x00148648 File Offset: 0x00147848
		private void ContinueTryEnterWithThreadTracking(int millisecondsTimeout, uint startTime, ref bool lockTaken)
		{
			int currentManagedThreadId = Environment.CurrentManagedThreadId;
			if (this._owner == currentManagedThreadId)
			{
				throw new LockRecursionException(SR.SpinLock_TryEnter_LockRecursionException);
			}
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				spinWait.SpinOnce();
				if (this._owner == 0 && SpinLock.CompareExchange(ref this._owner, currentManagedThreadId, 0, ref lockTaken) == 0)
				{
					break;
				}
				if (millisecondsTimeout == 0 || (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout) <= 0))
				{
					return;
				}
			}
		}

		// Token: 0x0600285D RID: 10333 RVA: 0x001486B5 File Offset: 0x001478B5
		public void Exit()
		{
			if ((this._owner & -2147483648) == 0)
			{
				this.ExitSlowPath(true);
				return;
			}
			Interlocked.Decrement(ref this._owner);
		}

		// Token: 0x0600285E RID: 10334 RVA: 0x001486DC File Offset: 0x001478DC
		public void Exit(bool useMemoryBarrier)
		{
			int owner = this._owner;
			if ((owner & -2147483648) != 0 & !useMemoryBarrier)
			{
				this._owner = (owner & -2);
				return;
			}
			this.ExitSlowPath(useMemoryBarrier);
		}

		// Token: 0x0600285F RID: 10335 RVA: 0x00148718 File Offset: 0x00147918
		private void ExitSlowPath(bool useMemoryBarrier)
		{
			bool flag = (this._owner & int.MinValue) == 0;
			if (flag && !this.IsHeldByCurrentThread)
			{
				throw new SynchronizationLockException(SR.SpinLock_Exit_SynchronizationLockException);
			}
			if (useMemoryBarrier)
			{
				if (flag)
				{
					Interlocked.Exchange(ref this._owner, 0);
					return;
				}
				Interlocked.Decrement(ref this._owner);
				return;
			}
			else
			{
				if (flag)
				{
					this._owner = 0;
					return;
				}
				int owner = this._owner;
				this._owner = (owner & -2);
				return;
			}
		}

		// Token: 0x17000865 RID: 2149
		// (get) Token: 0x06002860 RID: 10336 RVA: 0x00148790 File Offset: 0x00147990
		public bool IsHeld
		{
			get
			{
				if (this.IsThreadOwnerTrackingEnabled)
				{
					return this._owner != 0;
				}
				return (this._owner & 1) != 0;
			}
		}

		// Token: 0x17000866 RID: 2150
		// (get) Token: 0x06002861 RID: 10337 RVA: 0x001487B3 File Offset: 0x001479B3
		public bool IsHeldByCurrentThread
		{
			get
			{
				if (!this.IsThreadOwnerTrackingEnabled)
				{
					throw new InvalidOperationException(SR.SpinLock_IsHeldByCurrentThread);
				}
				return (this._owner & int.MaxValue) == Environment.CurrentManagedThreadId;
			}
		}

		// Token: 0x17000867 RID: 2151
		// (get) Token: 0x06002862 RID: 10338 RVA: 0x001487DD File Offset: 0x001479DD
		public bool IsThreadOwnerTrackingEnabled
		{
			get
			{
				return (this._owner & int.MinValue) == 0;
			}
		}

		// Token: 0x04000AC7 RID: 2759
		private volatile int _owner;

		// Token: 0x020002B7 RID: 695
		internal class SystemThreading_SpinLockDebugView
		{
			// Token: 0x06002863 RID: 10339 RVA: 0x001487F0 File Offset: 0x001479F0
			public SystemThreading_SpinLockDebugView(SpinLock spinLock)
			{
				this._spinLock = spinLock;
			}

			// Token: 0x17000868 RID: 2152
			// (get) Token: 0x06002864 RID: 10340 RVA: 0x00148800 File Offset: 0x00147A00
			public bool? IsHeldByCurrentThread
			{
				get
				{
					bool? result;
					try
					{
						result = new bool?(this._spinLock.IsHeldByCurrentThread);
					}
					catch (InvalidOperationException)
					{
						result = null;
					}
					return result;
				}
			}

			// Token: 0x17000869 RID: 2153
			// (get) Token: 0x06002865 RID: 10341 RVA: 0x00148840 File Offset: 0x00147A40
			public int? OwnerThreadID
			{
				get
				{
					if (this._spinLock.IsThreadOwnerTrackingEnabled)
					{
						return new int?(this._spinLock._owner);
					}
					return null;
				}
			}

			// Token: 0x1700086A RID: 2154
			// (get) Token: 0x06002866 RID: 10342 RVA: 0x00148876 File Offset: 0x00147A76
			public bool IsHeld
			{
				get
				{
					return this._spinLock.IsHeld;
				}
			}

			// Token: 0x04000AC8 RID: 2760
			private SpinLock _spinLock;
		}
	}
}
