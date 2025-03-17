using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020002A3 RID: 675
	[DebuggerDisplay("Set = {IsSet}")]
	public class ManualResetEventSlim : IDisposable
	{
		// Token: 0x1700084F RID: 2127
		// (get) Token: 0x060027B0 RID: 10160 RVA: 0x001459B0 File Offset: 0x00144BB0
		[Nullable(1)]
		public WaitHandle WaitHandle
		{
			[NullableContext(1)]
			get
			{
				this.ThrowIfDisposed();
				if (this.m_eventObj == null)
				{
					this.LazyInitializeEvent();
				}
				return this.m_eventObj;
			}
		}

		// Token: 0x17000850 RID: 2128
		// (get) Token: 0x060027B1 RID: 10161 RVA: 0x001459D0 File Offset: 0x00144BD0
		// (set) Token: 0x060027B2 RID: 10162 RVA: 0x001459E7 File Offset: 0x00144BE7
		public bool IsSet
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortion(this.m_combinedState, int.MinValue) != 0;
			}
			private set
			{
				this.UpdateStateAtomically((value ? 1 : 0) << 31, int.MinValue);
			}
		}

		// Token: 0x17000851 RID: 2129
		// (get) Token: 0x060027B3 RID: 10163 RVA: 0x001459FE File Offset: 0x00144BFE
		// (set) Token: 0x060027B4 RID: 10164 RVA: 0x00145A14 File Offset: 0x00144C14
		public int SpinCount
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 1073217536, 19);
			}
			private set
			{
				this.m_combinedState = ((this.m_combinedState & -1073217537) | value << 19);
			}
		}

		// Token: 0x17000852 RID: 2130
		// (get) Token: 0x060027B5 RID: 10165 RVA: 0x00145A31 File Offset: 0x00144C31
		// (set) Token: 0x060027B6 RID: 10166 RVA: 0x00145A46 File Offset: 0x00144C46
		private int Waiters
		{
			get
			{
				return ManualResetEventSlim.ExtractStatePortionAndShiftRight(this.m_combinedState, 524287, 0);
			}
			set
			{
				if (value >= 524287)
				{
					throw new InvalidOperationException(SR.Format(SR.ManualResetEventSlim_ctor_TooManyWaiters, 524287));
				}
				this.UpdateStateAtomically(value, 524287);
			}
		}

		// Token: 0x060027B7 RID: 10167 RVA: 0x00145A76 File Offset: 0x00144C76
		public ManualResetEventSlim() : this(false)
		{
		}

		// Token: 0x060027B8 RID: 10168 RVA: 0x00145A7F File Offset: 0x00144C7F
		public ManualResetEventSlim(bool initialState)
		{
			this.Initialize(initialState, SpinWait.SpinCountforSpinBeforeWait);
		}

		// Token: 0x060027B9 RID: 10169 RVA: 0x00145A94 File Offset: 0x00144C94
		public ManualResetEventSlim(bool initialState, int spinCount)
		{
			if (spinCount < 0)
			{
				throw new ArgumentOutOfRangeException("spinCount");
			}
			if (spinCount > 2047)
			{
				throw new ArgumentOutOfRangeException("spinCount", SR.Format(SR.ManualResetEventSlim_ctor_SpinCountOutOfRange, 2047));
			}
			this.Initialize(initialState, spinCount);
		}

		// Token: 0x060027BA RID: 10170 RVA: 0x00145AE5 File Offset: 0x00144CE5
		private void Initialize(bool initialState, int spinCount)
		{
			this.m_combinedState = (initialState ? int.MinValue : 0);
			this.SpinCount = (Environment.IsSingleProcessor ? 1 : spinCount);
		}

		// Token: 0x060027BB RID: 10171 RVA: 0x00145B0C File Offset: 0x00144D0C
		private void EnsureLockObjectCreated()
		{
			if (this.m_lock != null)
			{
				return;
			}
			object value = new object();
			Interlocked.CompareExchange(ref this.m_lock, value, null);
		}

		// Token: 0x060027BC RID: 10172 RVA: 0x00145B38 File Offset: 0x00144D38
		private void LazyInitializeEvent()
		{
			bool isSet = this.IsSet;
			ManualResetEvent manualResetEvent = new ManualResetEvent(isSet);
			if (Interlocked.CompareExchange<ManualResetEvent>(ref this.m_eventObj, manualResetEvent, null) != null)
			{
				manualResetEvent.Dispose();
				return;
			}
			bool isSet2 = this.IsSet;
			if (isSet2 != isSet)
			{
				ManualResetEvent obj = manualResetEvent;
				lock (obj)
				{
					if (this.m_eventObj == manualResetEvent)
					{
						manualResetEvent.Set();
					}
				}
			}
		}

		// Token: 0x060027BD RID: 10173 RVA: 0x00145BB0 File Offset: 0x00144DB0
		public void Set()
		{
			this.Set(false);
		}

		// Token: 0x060027BE RID: 10174 RVA: 0x00145BBC File Offset: 0x00144DBC
		private void Set(bool duringCancellation)
		{
			this.IsSet = true;
			if (this.Waiters > 0)
			{
				object @lock = this.m_lock;
				lock (@lock)
				{
					Monitor.PulseAll(this.m_lock);
				}
			}
			ManualResetEvent eventObj = this.m_eventObj;
			if (eventObj != null && !duringCancellation)
			{
				ManualResetEvent obj = eventObj;
				lock (obj)
				{
					if (this.m_eventObj != null)
					{
						this.m_eventObj.Set();
					}
				}
			}
		}

		// Token: 0x060027BF RID: 10175 RVA: 0x00145C64 File Offset: 0x00144E64
		public void Reset()
		{
			this.ThrowIfDisposed();
			if (this.m_eventObj != null)
			{
				this.m_eventObj.Reset();
			}
			this.IsSet = false;
		}

		// Token: 0x060027C0 RID: 10176 RVA: 0x00145C8B File Offset: 0x00144E8B
		public void Wait()
		{
			this.Wait(-1, CancellationToken.None);
		}

		// Token: 0x060027C1 RID: 10177 RVA: 0x00145C9A File Offset: 0x00144E9A
		public void Wait(CancellationToken cancellationToken)
		{
			this.Wait(-1, cancellationToken);
		}

		// Token: 0x060027C2 RID: 10178 RVA: 0x00145CA8 File Offset: 0x00144EA8
		public bool Wait(TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, CancellationToken.None);
		}

		// Token: 0x060027C3 RID: 10179 RVA: 0x00145CE4 File Offset: 0x00144EE4
		public bool Wait(TimeSpan timeout, CancellationToken cancellationToken)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout");
			}
			return this.Wait((int)num, cancellationToken);
		}

		// Token: 0x060027C4 RID: 10180 RVA: 0x00145D1C File Offset: 0x00144F1C
		public bool Wait(int millisecondsTimeout)
		{
			return this.Wait(millisecondsTimeout, CancellationToken.None);
		}

		// Token: 0x060027C5 RID: 10181 RVA: 0x00145D2C File Offset: 0x00144F2C
		public bool Wait(int millisecondsTimeout, CancellationToken cancellationToken)
		{
			this.ThrowIfDisposed();
			cancellationToken.ThrowIfCancellationRequested();
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout");
			}
			if (!this.IsSet)
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				uint startTime = 0U;
				bool flag = false;
				int num = millisecondsTimeout;
				if (millisecondsTimeout != -1)
				{
					startTime = TimeoutHelper.GetTime();
					flag = true;
				}
				int spinCount = this.SpinCount;
				SpinWait spinWait = default(SpinWait);
				while (spinWait.Count < spinCount)
				{
					spinWait.SpinOnce(-1);
					if (this.IsSet)
					{
						return true;
					}
					if (spinWait.Count >= 100 && spinWait.Count % 10 == 0)
					{
						cancellationToken.ThrowIfCancellationRequested();
					}
				}
				this.EnsureLockObjectCreated();
				using (cancellationToken.UnsafeRegister(ManualResetEventSlim.s_cancellationTokenCallback, this))
				{
					object @lock = this.m_lock;
					lock (@lock)
					{
						while (!this.IsSet)
						{
							cancellationToken.ThrowIfCancellationRequested();
							if (flag)
							{
								num = TimeoutHelper.UpdateTimeOut(startTime, millisecondsTimeout);
								if (num <= 0)
								{
									return false;
								}
							}
							int waiters = this.Waiters;
							this.Waiters = waiters + 1;
							if (this.IsSet)
							{
								waiters = this.Waiters;
								this.Waiters = waiters - 1;
								return true;
							}
							try
							{
								if (!Monitor.Wait(this.m_lock, num))
								{
									return false;
								}
							}
							finally
							{
								waiters = this.Waiters;
								this.Waiters = waiters - 1;
							}
						}
					}
				}
				return true;
			}
			return true;
		}

		// Token: 0x060027C6 RID: 10182 RVA: 0x00145EBC File Offset: 0x001450BC
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060027C7 RID: 10183 RVA: 0x00145ECC File Offset: 0x001450CC
		protected virtual void Dispose(bool disposing)
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				return;
			}
			this.m_combinedState |= 1073741824;
			if (disposing)
			{
				ManualResetEvent eventObj = this.m_eventObj;
				if (eventObj != null)
				{
					ManualResetEvent obj = eventObj;
					lock (obj)
					{
						eventObj.Dispose();
						this.m_eventObj = null;
					}
				}
			}
		}

		// Token: 0x060027C8 RID: 10184 RVA: 0x00145F48 File Offset: 0x00145148
		private void ThrowIfDisposed()
		{
			if ((this.m_combinedState & 1073741824) != 0)
			{
				throw new ObjectDisposedException(SR.ManualResetEventSlim_Disposed);
			}
		}

		// Token: 0x060027C9 RID: 10185 RVA: 0x00145F68 File Offset: 0x00145168
		private static void CancellationTokenCallback(object obj)
		{
			ManualResetEventSlim manualResetEventSlim = (ManualResetEventSlim)obj;
			object @lock = manualResetEventSlim.m_lock;
			lock (@lock)
			{
				Monitor.PulseAll(manualResetEventSlim.m_lock);
			}
		}

		// Token: 0x060027CA RID: 10186 RVA: 0x00145FB8 File Offset: 0x001451B8
		private void UpdateStateAtomically(int newBits, int updateBitsMask)
		{
			SpinWait spinWait = default(SpinWait);
			for (;;)
			{
				int combinedState = this.m_combinedState;
				int value = (combinedState & ~updateBitsMask) | newBits;
				if (Interlocked.CompareExchange(ref this.m_combinedState, value, combinedState) == combinedState)
				{
					break;
				}
				spinWait.SpinOnce(-1);
			}
		}

		// Token: 0x060027CB RID: 10187 RVA: 0x00145FF7 File Offset: 0x001451F7
		private static int ExtractStatePortionAndShiftRight(int state, int mask, int rightBitShiftCount)
		{
			return (int)((uint)(state & mask) >> rightBitShiftCount);
		}

		// Token: 0x060027CC RID: 10188 RVA: 0x00146001 File Offset: 0x00145201
		private static int ExtractStatePortion(int state, int mask)
		{
			return state & mask;
		}

		// Token: 0x04000A72 RID: 2674
		private volatile object m_lock;

		// Token: 0x04000A73 RID: 2675
		private volatile ManualResetEvent m_eventObj;

		// Token: 0x04000A74 RID: 2676
		private volatile int m_combinedState;

		// Token: 0x04000A75 RID: 2677
		private static readonly Action<object> s_cancellationTokenCallback = new Action<object>(ManualResetEventSlim.CancellationTokenCallback);
	}
}
