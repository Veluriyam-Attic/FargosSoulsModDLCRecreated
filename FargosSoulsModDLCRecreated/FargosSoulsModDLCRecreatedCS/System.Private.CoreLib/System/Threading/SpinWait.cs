using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020002B8 RID: 696
	[NullableContext(1)]
	[Nullable(0)]
	public struct SpinWait
	{
		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06002867 RID: 10343 RVA: 0x00148883 File Offset: 0x00147A83
		// (set) Token: 0x06002868 RID: 10344 RVA: 0x0014888B File Offset: 0x00147A8B
		public int Count
		{
			get
			{
				return this._count;
			}
			internal set
			{
				this._count = value;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06002869 RID: 10345 RVA: 0x00148894 File Offset: 0x00147A94
		public bool NextSpinWillYield
		{
			get
			{
				return this._count >= 10 || Environment.IsSingleProcessor;
			}
		}

		// Token: 0x0600286A RID: 10346 RVA: 0x001488A7 File Offset: 0x00147AA7
		public void SpinOnce()
		{
			this.SpinOnceCore(20);
		}

		// Token: 0x0600286B RID: 10347 RVA: 0x001488B1 File Offset: 0x00147AB1
		public void SpinOnce(int sleep1Threshold)
		{
			if (sleep1Threshold < -1)
			{
				throw new ArgumentOutOfRangeException("sleep1Threshold", sleep1Threshold, SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (sleep1Threshold >= 0 && sleep1Threshold < 10)
			{
				sleep1Threshold = 10;
			}
			this.SpinOnceCore(sleep1Threshold);
		}

		// Token: 0x0600286C RID: 10348 RVA: 0x001488E4 File Offset: 0x00147AE4
		private void SpinOnceCore(int sleep1Threshold)
		{
			if ((this._count >= 10 && ((this._count >= sleep1Threshold && sleep1Threshold >= 0) || (this._count - 10) % 2 == 0)) || Environment.IsSingleProcessor)
			{
				if (this._count >= sleep1Threshold && sleep1Threshold >= 0)
				{
					Thread.Sleep(1);
				}
				else
				{
					int num = (this._count >= 10) ? ((this._count - 10) / 2) : this._count;
					if (num % 5 == 4)
					{
						Thread.Sleep(0);
					}
					else
					{
						Thread.Yield();
					}
				}
			}
			else
			{
				int num2 = Thread.OptimalMaxSpinWaitsPerSpinIteration;
				if (this._count <= 30 && 1 << this._count < num2)
				{
					num2 = 1 << this._count;
				}
				Thread.SpinWait(num2);
			}
			this._count = ((this._count == int.MaxValue) ? 10 : (this._count + 1));
		}

		// Token: 0x0600286D RID: 10349 RVA: 0x001489B4 File Offset: 0x00147BB4
		public void Reset()
		{
			this._count = 0;
		}

		// Token: 0x0600286E RID: 10350 RVA: 0x001489BD File Offset: 0x00147BBD
		public static void SpinUntil(Func<bool> condition)
		{
			SpinWait.SpinUntil(condition, -1);
		}

		// Token: 0x0600286F RID: 10351 RVA: 0x001489C8 File Offset: 0x00147BC8
		public static bool SpinUntil(Func<bool> condition, TimeSpan timeout)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", timeout, SR.SpinWait_SpinUntil_TimeoutWrong);
			}
			return SpinWait.SpinUntil(condition, (int)num);
		}

		// Token: 0x06002870 RID: 10352 RVA: 0x00148A0C File Offset: 0x00147C0C
		public static bool SpinUntil(Func<bool> condition, int millisecondsTimeout)
		{
			if (millisecondsTimeout < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeout", millisecondsTimeout, SR.SpinWait_SpinUntil_TimeoutWrong);
			}
			if (condition == null)
			{
				throw new ArgumentNullException("condition", SR.SpinWait_SpinUntil_ArgumentNull);
			}
			uint num = 0U;
			if (millisecondsTimeout != 0 && millisecondsTimeout != -1)
			{
				num = TimeoutHelper.GetTime();
			}
			SpinWait spinWait = default(SpinWait);
			while (!condition())
			{
				if (millisecondsTimeout == 0)
				{
					return false;
				}
				spinWait.SpinOnce();
				if (millisecondsTimeout != -1 && spinWait.NextSpinWillYield && (long)millisecondsTimeout <= (long)((ulong)(TimeoutHelper.GetTime() - num)))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04000AC9 RID: 2761
		internal static readonly int SpinCountforSpinBeforeWait = Environment.IsSingleProcessor ? 1 : 35;

		// Token: 0x04000ACA RID: 2762
		private int _count;
	}
}
