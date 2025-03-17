using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x020006DD RID: 1757
	public class Stopwatch
	{
		// Token: 0x060058E1 RID: 22753 RVA: 0x001B13C6 File Offset: 0x001B05C6
		public Stopwatch()
		{
			this.Reset();
		}

		// Token: 0x060058E2 RID: 22754 RVA: 0x001B13D4 File Offset: 0x001B05D4
		public void Start()
		{
			if (!this._isRunning)
			{
				this._startTimeStamp = Stopwatch.GetTimestamp();
				this._isRunning = true;
			}
		}

		// Token: 0x060058E3 RID: 22755 RVA: 0x001B13F0 File Offset: 0x001B05F0
		[NullableContext(1)]
		public static Stopwatch StartNew()
		{
			Stopwatch stopwatch = new Stopwatch();
			stopwatch.Start();
			return stopwatch;
		}

		// Token: 0x060058E4 RID: 22756 RVA: 0x001B140C File Offset: 0x001B060C
		public void Stop()
		{
			if (this._isRunning)
			{
				long timestamp = Stopwatch.GetTimestamp();
				long num = timestamp - this._startTimeStamp;
				this._elapsed += num;
				this._isRunning = false;
				if (this._elapsed < 0L)
				{
					this._elapsed = 0L;
				}
			}
		}

		// Token: 0x060058E5 RID: 22757 RVA: 0x001B1457 File Offset: 0x001B0657
		public void Reset()
		{
			this._elapsed = 0L;
			this._isRunning = false;
			this._startTimeStamp = 0L;
		}

		// Token: 0x060058E6 RID: 22758 RVA: 0x001B1470 File Offset: 0x001B0670
		public void Restart()
		{
			this._elapsed = 0L;
			this._startTimeStamp = Stopwatch.GetTimestamp();
			this._isRunning = true;
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x060058E7 RID: 22759 RVA: 0x001B148C File Offset: 0x001B068C
		public bool IsRunning
		{
			get
			{
				return this._isRunning;
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x060058E8 RID: 22760 RVA: 0x001B1494 File Offset: 0x001B0694
		public TimeSpan Elapsed
		{
			get
			{
				return new TimeSpan(this.GetElapsedDateTimeTicks());
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x060058E9 RID: 22761 RVA: 0x001B14A1 File Offset: 0x001B06A1
		public long ElapsedMilliseconds
		{
			get
			{
				return this.GetElapsedDateTimeTicks() / 10000L;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x060058EA RID: 22762 RVA: 0x001B14B0 File Offset: 0x001B06B0
		public long ElapsedTicks
		{
			get
			{
				return this.GetRawElapsedTicks();
			}
		}

		// Token: 0x060058EB RID: 22763 RVA: 0x001B14B8 File Offset: 0x001B06B8
		public static long GetTimestamp()
		{
			return Stopwatch.QueryPerformanceCounter();
		}

		// Token: 0x060058EC RID: 22764 RVA: 0x001B14C0 File Offset: 0x001B06C0
		private long GetRawElapsedTicks()
		{
			long num = this._elapsed;
			if (this._isRunning)
			{
				long timestamp = Stopwatch.GetTimestamp();
				long num2 = timestamp - this._startTimeStamp;
				num += num2;
			}
			return num;
		}

		// Token: 0x060058ED RID: 22765 RVA: 0x001B14F0 File Offset: 0x001B06F0
		private long GetElapsedDateTimeTicks()
		{
			return (long)((double)this.GetRawElapsedTicks() * Stopwatch.s_tickFrequency);
		}

		// Token: 0x060058EE RID: 22766 RVA: 0x001B1500 File Offset: 0x001B0700
		private unsafe static long QueryPerformanceFrequency()
		{
			long result;
			Interop.BOOL @bool = Interop.Kernel32.QueryPerformanceFrequency(&result);
			return result;
		}

		// Token: 0x060058EF RID: 22767 RVA: 0x001B1518 File Offset: 0x001B0718
		private unsafe static long QueryPerformanceCounter()
		{
			long result;
			Interop.BOOL @bool = Interop.Kernel32.QueryPerformanceCounter(&result);
			return result;
		}

		// Token: 0x04001986 RID: 6534
		private long _elapsed;

		// Token: 0x04001987 RID: 6535
		private long _startTimeStamp;

		// Token: 0x04001988 RID: 6536
		private bool _isRunning;

		// Token: 0x04001989 RID: 6537
		public static readonly long Frequency = Stopwatch.QueryPerformanceFrequency();

		// Token: 0x0400198A RID: 6538
		public static readonly bool IsHighResolution = true;

		// Token: 0x0400198B RID: 6539
		private static readonly double s_tickFrequency = 10000000.0 / (double)Stopwatch.Frequency;
	}
}
