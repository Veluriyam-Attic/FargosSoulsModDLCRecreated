using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x020002DD RID: 733
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class Timer : MarshalByRefObject, IDisposable, IAsyncDisposable
	{
		// Token: 0x060028FF RID: 10495 RVA: 0x0014A6EC File Offset: 0x001498EC
		public Timer(TimerCallback callback, [Nullable(2)] object state, int dueTime, int period) : this(callback, state, dueTime, period, true)
		{
		}

		// Token: 0x06002900 RID: 10496 RVA: 0x0014A6FA File Offset: 0x001498FA
		internal Timer(TimerCallback callback, object state, int dueTime, int period, bool flowExecutionContext)
		{
			if (dueTime < -1)
			{
				throw new ArgumentOutOfRangeException("dueTime", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (period < -1)
			{
				throw new ArgumentOutOfRangeException("period", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			this.TimerSetup(callback, state, (uint)dueTime, (uint)period, flowExecutionContext);
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x0014A738 File Offset: 0x00149938
		public Timer(TimerCallback callback, [Nullable(2)] object state, TimeSpan dueTime, TimeSpan period)
		{
			long num = (long)dueTime.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (num > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("dueTime", SR.ArgumentOutOfRange_TimeoutTooLarge);
			}
			long num2 = (long)period.TotalMilliseconds;
			if (num2 < -1L)
			{
				throw new ArgumentOutOfRangeException("period", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (num2 > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("period", SR.ArgumentOutOfRange_PeriodTooLarge);
			}
			this.TimerSetup(callback, state, (uint)num, (uint)num2, true);
		}

		// Token: 0x06002902 RID: 10498 RVA: 0x0014A7C0 File Offset: 0x001499C0
		[CLSCompliant(false)]
		public Timer(TimerCallback callback, [Nullable(2)] object state, uint dueTime, uint period)
		{
			this.TimerSetup(callback, state, dueTime, period, true);
		}

		// Token: 0x06002903 RID: 10499 RVA: 0x0014A7D4 File Offset: 0x001499D4
		public Timer(TimerCallback callback, [Nullable(2)] object state, long dueTime, long period)
		{
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (dueTime > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("dueTime", SR.ArgumentOutOfRange_TimeoutTooLarge);
			}
			if (period > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("period", SR.ArgumentOutOfRange_PeriodTooLarge);
			}
			this.TimerSetup(callback, state, (uint)dueTime, (uint)period, true);
		}

		// Token: 0x06002904 RID: 10500 RVA: 0x0014A84D File Offset: 0x00149A4D
		public Timer(TimerCallback callback)
		{
			this.TimerSetup(callback, this, uint.MaxValue, uint.MaxValue, true);
		}

		// Token: 0x06002905 RID: 10501 RVA: 0x0014A860 File Offset: 0x00149A60
		[MemberNotNull("_timer")]
		private void TimerSetup(TimerCallback callback, object state, uint dueTime, uint period, bool flowExecutionContext = true)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._timer = new TimerHolder(new TimerQueueTimer(callback, state, dueTime, period, flowExecutionContext));
		}

		// Token: 0x06002906 RID: 10502 RVA: 0x0014A887 File Offset: 0x00149A87
		public bool Change(int dueTime, int period)
		{
			if (dueTime < -1)
			{
				throw new ArgumentOutOfRangeException("dueTime", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (period < -1)
			{
				throw new ArgumentOutOfRangeException("period", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			return this._timer._timer.Change((uint)dueTime, (uint)period);
		}

		// Token: 0x06002907 RID: 10503 RVA: 0x0014A8C3 File Offset: 0x00149AC3
		public bool Change(TimeSpan dueTime, TimeSpan period)
		{
			return this.Change((long)dueTime.TotalMilliseconds, (long)period.TotalMilliseconds);
		}

		// Token: 0x06002908 RID: 10504 RVA: 0x0014A8DB File Offset: 0x00149ADB
		[CLSCompliant(false)]
		public bool Change(uint dueTime, uint period)
		{
			return this._timer._timer.Change(dueTime, period);
		}

		// Token: 0x06002909 RID: 10505 RVA: 0x0014A8F0 File Offset: 0x00149AF0
		public bool Change(long dueTime, long period)
		{
			if (dueTime < -1L)
			{
				throw new ArgumentOutOfRangeException("dueTime", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (period < -1L)
			{
				throw new ArgumentOutOfRangeException("period", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (dueTime > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("dueTime", SR.ArgumentOutOfRange_TimeoutTooLarge);
			}
			if (period > (long)((ulong)-2))
			{
				throw new ArgumentOutOfRangeException("period", SR.ArgumentOutOfRange_PeriodTooLarge);
			}
			return this._timer._timer.Change((uint)dueTime, (uint)period);
		}

		// Token: 0x1700087D RID: 2173
		// (get) Token: 0x0600290A RID: 10506 RVA: 0x0014A968 File Offset: 0x00149B68
		public static long ActiveCount
		{
			get
			{
				long num = 0L;
				foreach (TimerQueue timerQueue in TimerQueue.Instances)
				{
					TimerQueue obj = timerQueue;
					lock (obj)
					{
						num += timerQueue.ActiveCount;
					}
				}
				return num;
			}
		}

		// Token: 0x0600290B RID: 10507 RVA: 0x0014A9C8 File Offset: 0x00149BC8
		public bool Dispose(WaitHandle notifyObject)
		{
			if (notifyObject == null)
			{
				throw new ArgumentNullException("notifyObject");
			}
			return this._timer.Close(notifyObject);
		}

		// Token: 0x0600290C RID: 10508 RVA: 0x0014A9E4 File Offset: 0x00149BE4
		public void Dispose()
		{
			this._timer.Close();
		}

		// Token: 0x0600290D RID: 10509 RVA: 0x0014A9F1 File Offset: 0x00149BF1
		public ValueTask DisposeAsync()
		{
			return this._timer.CloseAsync();
		}

		// Token: 0x04000B26 RID: 2854
		private TimerHolder _timer;
	}
}
