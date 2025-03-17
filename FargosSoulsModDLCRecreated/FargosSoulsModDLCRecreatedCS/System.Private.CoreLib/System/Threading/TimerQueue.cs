using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;

namespace System.Threading
{
	// Token: 0x02000277 RID: 631
	internal class TimerQueue
	{
		// Token: 0x060026A0 RID: 9888 RVA: 0x001427D9 File Offset: 0x001419D9
		private TimerQueue(int id)
		{
			this._id = id;
		}

		// Token: 0x060026A1 RID: 9889 RVA: 0x001427FC File Offset: 0x001419FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private bool SetTimer(uint actualDuration)
		{
			if (this.m_appDomainTimer == null || this.m_appDomainTimer.IsInvalid)
			{
				this.m_appDomainTimer = TimerQueue.CreateAppDomainTimer(actualDuration, this._id);
				return !this.m_appDomainTimer.IsInvalid;
			}
			return TimerQueue.ChangeAppDomainTimer(this.m_appDomainTimer, actualDuration);
		}

		// Token: 0x060026A2 RID: 9890 RVA: 0x0014284B File Offset: 0x00141A4B
		internal static void AppDomainTimerCallback(int id)
		{
			TimerQueue.Instances[id].FireNextTimers();
		}

		// Token: 0x060026A3 RID: 9891
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern TimerQueue.AppDomainTimerSafeHandle CreateAppDomainTimer(uint dueTime, int id);

		// Token: 0x060026A4 RID: 9892
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool ChangeAppDomainTimer(TimerQueue.AppDomainTimerSafeHandle handle, uint dueTime);

		// Token: 0x060026A5 RID: 9893
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool DeleteAppDomainTimer(IntPtr handle);

		// Token: 0x17000836 RID: 2102
		// (get) Token: 0x060026A6 RID: 9894 RVA: 0x00142859 File Offset: 0x00141A59
		public static TimerQueue[] Instances { get; } = TimerQueue.CreateTimerQueues();

		// Token: 0x060026A7 RID: 9895 RVA: 0x00142860 File Offset: 0x00141A60
		private static TimerQueue[] CreateTimerQueues()
		{
			TimerQueue[] array = new TimerQueue[Environment.ProcessorCount];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new TimerQueue(i);
			}
			return array;
		}

		// Token: 0x060026A8 RID: 9896 RVA: 0x00142890 File Offset: 0x00141A90
		private bool EnsureTimerFiresBy(uint requestedDuration)
		{
			uint num = Math.Min(requestedDuration, 268435455U);
			if (this._isTimerScheduled)
			{
				long num2 = TimerQueue.TickCount64 - this._currentTimerStartTicks;
				if (num2 >= (long)((ulong)this._currentTimerDuration))
				{
					return true;
				}
				uint num3 = this._currentTimerDuration - (uint)num2;
				if (num >= num3)
				{
					return true;
				}
			}
			if (this.SetTimer(num))
			{
				this._isTimerScheduled = true;
				this._currentTimerStartTicks = TimerQueue.TickCount64;
				this._currentTimerDuration = num;
				return true;
			}
			return false;
		}

		// Token: 0x060026A9 RID: 9897 RVA: 0x00142900 File Offset: 0x00141B00
		private void FireNextTimers()
		{
			TimerQueueTimer timerQueueTimer = null;
			lock (this)
			{
				this._isTimerScheduled = false;
				bool flag2 = false;
				uint num = uint.MaxValue;
				long tickCount = TimerQueue.TickCount64;
				TimerQueueTimer timerQueueTimer2 = this._shortTimers;
				for (int i = 0; i < 2; i++)
				{
					while (timerQueueTimer2 != null)
					{
						TimerQueueTimer next = timerQueueTimer2._next;
						long num2 = tickCount - timerQueueTimer2._startTicks;
						long num3 = (long)((ulong)timerQueueTimer2._dueTime - (ulong)num2);
						if (num3 <= 0L)
						{
							if (timerQueueTimer2._period != 4294967295U)
							{
								timerQueueTimer2._startTicks = tickCount;
								long num4 = num2 - (long)((ulong)timerQueueTimer2._dueTime);
								timerQueueTimer2._dueTime = ((num4 < (long)((ulong)timerQueueTimer2._period)) ? (timerQueueTimer2._period - (uint)num4) : 1U);
								if (timerQueueTimer2._dueTime < num)
								{
									flag2 = true;
									num = timerQueueTimer2._dueTime;
								}
								bool flag3 = tickCount + (long)((ulong)timerQueueTimer2._dueTime) - this._currentAbsoluteThreshold <= 0L;
								if (timerQueueTimer2._short != flag3)
								{
									this.MoveTimerToCorrectList(timerQueueTimer2, flag3);
								}
							}
							else
							{
								this.DeleteTimer(timerQueueTimer2);
							}
							if (timerQueueTimer == null)
							{
								timerQueueTimer = timerQueueTimer2;
							}
							else
							{
								ThreadPool.UnsafeQueueUserWorkItemInternal(timerQueueTimer2, false);
							}
						}
						else
						{
							if (num3 < (long)((ulong)num))
							{
								flag2 = true;
								num = (uint)num3;
							}
							if (!timerQueueTimer2._short && num3 <= 333L)
							{
								this.MoveTimerToCorrectList(timerQueueTimer2, true);
							}
						}
						timerQueueTimer2 = next;
					}
					if (i == 0)
					{
						long num5 = this._currentAbsoluteThreshold - tickCount;
						if (num5 > 0L)
						{
							if (this._shortTimers == null && this._longTimers != null)
							{
								num = (uint)num5 + 1U;
								flag2 = true;
								break;
							}
							break;
						}
						else
						{
							timerQueueTimer2 = this._longTimers;
							this._currentAbsoluteThreshold = tickCount + 333L;
						}
					}
				}
				if (flag2)
				{
					this.EnsureTimerFiresBy(num);
				}
			}
			if (timerQueueTimer != null)
			{
				timerQueueTimer.Fire(false);
			}
		}

		// Token: 0x17000837 RID: 2103
		// (get) Token: 0x060026AA RID: 9898 RVA: 0x00142AD4 File Offset: 0x00141CD4
		// (set) Token: 0x060026AB RID: 9899 RVA: 0x00142ADC File Offset: 0x00141CDC
		public long ActiveCount { get; private set; }

		// Token: 0x060026AC RID: 9900 RVA: 0x00142AE8 File Offset: 0x00141CE8
		public bool UpdateTimer(TimerQueueTimer timer, uint dueTime, uint period)
		{
			long tickCount = TimerQueue.TickCount64;
			long num = tickCount + (long)((ulong)dueTime);
			bool flag = this._currentAbsoluteThreshold - num >= 0L;
			if (timer._dueTime == 4294967295U)
			{
				timer._short = flag;
				this.LinkTimer(timer);
				long activeCount = this.ActiveCount + 1L;
				this.ActiveCount = activeCount;
			}
			else if (timer._short != flag)
			{
				this.UnlinkTimer(timer);
				timer._short = flag;
				this.LinkTimer(timer);
			}
			timer._dueTime = dueTime;
			timer._period = ((period == 0U) ? uint.MaxValue : period);
			timer._startTicks = tickCount;
			return this.EnsureTimerFiresBy(dueTime);
		}

		// Token: 0x060026AD RID: 9901 RVA: 0x00142B7A File Offset: 0x00141D7A
		public void MoveTimerToCorrectList(TimerQueueTimer timer, bool shortList)
		{
			this.UnlinkTimer(timer);
			timer._short = shortList;
			this.LinkTimer(timer);
		}

		// Token: 0x060026AE RID: 9902 RVA: 0x00142B94 File Offset: 0x00141D94
		private void LinkTimer(TimerQueueTimer timer)
		{
			ref TimerQueueTimer ptr = ref timer._short ? ref this._shortTimers : ref this._longTimers;
			timer._next = ptr;
			if (timer._next != null)
			{
				timer._next._prev = timer;
			}
			timer._prev = null;
			ptr = timer;
		}

		// Token: 0x060026AF RID: 9903 RVA: 0x00142BE0 File Offset: 0x00141DE0
		private void UnlinkTimer(TimerQueueTimer timer)
		{
			TimerQueueTimer timerQueueTimer = timer._next;
			if (timerQueueTimer != null)
			{
				timerQueueTimer._prev = timer._prev;
			}
			if (this._shortTimers == timer)
			{
				this._shortTimers = timerQueueTimer;
			}
			else if (this._longTimers == timer)
			{
				this._longTimers = timerQueueTimer;
			}
			timerQueueTimer = timer._prev;
			if (timerQueueTimer != null)
			{
				timerQueueTimer._next = timer._next;
			}
		}

		// Token: 0x060026B0 RID: 9904 RVA: 0x00142C3C File Offset: 0x00141E3C
		public void DeleteTimer(TimerQueueTimer timer)
		{
			if (timer._dueTime != 4294967295U)
			{
				long activeCount = this.ActiveCount - 1L;
				this.ActiveCount = activeCount;
				this.UnlinkTimer(timer);
				timer._prev = null;
				timer._next = null;
				timer._dueTime = uint.MaxValue;
				timer._period = uint.MaxValue;
				timer._startTicks = 0L;
				timer._short = false;
			}
		}

		// Token: 0x17000838 RID: 2104
		// (get) Token: 0x060026B1 RID: 9905 RVA: 0x00142C98 File Offset: 0x00141E98
		private static long TickCount64
		{
			get
			{
				if (Environment.IsWindows8OrAbove)
				{
					ulong num;
					bool flag = Interop.Kernel32.QueryUnbiasedInterruptTime(out num);
					return (long)(num / 10000UL);
				}
				return Environment.TickCount64;
			}
		}

		// Token: 0x04000A0C RID: 2572
		private readonly int _id;

		// Token: 0x04000A0D RID: 2573
		private TimerQueue.AppDomainTimerSafeHandle m_appDomainTimer;

		// Token: 0x04000A0F RID: 2575
		private bool _isTimerScheduled;

		// Token: 0x04000A10 RID: 2576
		private long _currentTimerStartTicks;

		// Token: 0x04000A11 RID: 2577
		private uint _currentTimerDuration;

		// Token: 0x04000A12 RID: 2578
		private TimerQueueTimer _shortTimers;

		// Token: 0x04000A13 RID: 2579
		private TimerQueueTimer _longTimers;

		// Token: 0x04000A14 RID: 2580
		private long _currentAbsoluteThreshold = TimerQueue.TickCount64 + 333L;

		// Token: 0x02000278 RID: 632
		private sealed class AppDomainTimerSafeHandle : SafeHandleZeroOrMinusOneIsInvalid
		{
			// Token: 0x060026B3 RID: 9907 RVA: 0x000AB1EC File Offset: 0x000AA3EC
			public AppDomainTimerSafeHandle() : base(true)
			{
			}

			// Token: 0x060026B4 RID: 9908 RVA: 0x00142CCE File Offset: 0x00141ECE
			protected override bool ReleaseHandle()
			{
				return TimerQueue.DeleteAppDomainTimer(this.handle);
			}
		}
	}
}
