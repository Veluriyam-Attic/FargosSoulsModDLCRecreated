using System;
using System.Collections;
using System.Globalization;

namespace System
{
	// Token: 0x020000E2 RID: 226
	[Obsolete("System.CurrentSystemTimeZone has been deprecated.  Please investigate the use of System.TimeZoneInfo.Local instead.")]
	internal class CurrentSystemTimeZone : TimeZone
	{
		// Token: 0x06000CD4 RID: 3284 RVA: 0x000CD094 File Offset: 0x000CC294
		internal CurrentSystemTimeZone()
		{
			TimeZoneInfo local = TimeZoneInfo.Local;
			this.m_ticksOffset = local.BaseUtcOffset.Ticks;
			this.m_standardName = local.StandardName;
			this.m_daylightName = local.DaylightName;
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x06000CD5 RID: 3285 RVA: 0x000CD0E4 File Offset: 0x000CC2E4
		public override string StandardName
		{
			get
			{
				return this.m_standardName;
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x06000CD6 RID: 3286 RVA: 0x000CD0EC File Offset: 0x000CC2EC
		public override string DaylightName
		{
			get
			{
				return this.m_daylightName;
			}
		}

		// Token: 0x06000CD7 RID: 3287 RVA: 0x000CD0F4 File Offset: 0x000CC2F4
		internal long GetUtcOffsetFromUniversalTime(DateTime time, ref bool isAmbiguousLocalDst)
		{
			TimeSpan timeSpan = new TimeSpan(this.m_ticksOffset);
			DaylightTime daylightChanges = this.GetDaylightChanges(time.Year);
			isAmbiguousLocalDst = false;
			if (daylightChanges == null || daylightChanges.Delta.Ticks == 0L)
			{
				return timeSpan.Ticks;
			}
			DateTime dateTime = daylightChanges.Start - timeSpan;
			DateTime dateTime2 = daylightChanges.End - timeSpan - daylightChanges.Delta;
			DateTime t;
			DateTime t2;
			if (daylightChanges.Delta.Ticks > 0L)
			{
				t = dateTime2 - daylightChanges.Delta;
				t2 = dateTime2;
			}
			else
			{
				t = dateTime;
				t2 = dateTime - daylightChanges.Delta;
			}
			bool flag;
			if (dateTime > dateTime2)
			{
				flag = (time < dateTime2 || time >= dateTime);
			}
			else
			{
				flag = (time >= dateTime && time < dateTime2);
			}
			if (flag)
			{
				timeSpan += daylightChanges.Delta;
				if (time >= t && time < t2)
				{
					isAmbiguousLocalDst = true;
				}
			}
			return timeSpan.Ticks;
		}

		// Token: 0x06000CD8 RID: 3288 RVA: 0x000CD1FC File Offset: 0x000CC3FC
		public override DateTime ToLocalTime(DateTime time)
		{
			if (time.Kind == DateTimeKind.Local)
			{
				return time;
			}
			bool isAmbiguousDst = false;
			long utcOffsetFromUniversalTime = this.GetUtcOffsetFromUniversalTime(time, ref isAmbiguousDst);
			long num = time.Ticks + utcOffsetFromUniversalTime;
			if (num > 3155378975999999999L)
			{
				return new DateTime(3155378975999999999L, DateTimeKind.Local);
			}
			if (num < 0L)
			{
				return new DateTime(0L, DateTimeKind.Local);
			}
			return new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
		}

		// Token: 0x06000CD9 RID: 3289 RVA: 0x000CD25D File Offset: 0x000CC45D
		public override DaylightTime GetDaylightChanges(int year)
		{
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", SR.Format(SR.ArgumentOutOfRange_Range, 1, 9999));
			}
			return this.GetCachedDaylightChanges(year);
		}

		// Token: 0x06000CDA RID: 3290 RVA: 0x000CD298 File Offset: 0x000CC498
		private static DaylightTime CreateDaylightChanges(int year)
		{
			DateTime start = DateTime.MinValue;
			DateTime end = DateTime.MinValue;
			TimeSpan delta = TimeSpan.Zero;
			if (TimeZoneInfo.Local.SupportsDaylightSavingTime)
			{
				foreach (TimeZoneInfo.AdjustmentRule adjustmentRule in TimeZoneInfo.Local.GetAdjustmentRules())
				{
					if (adjustmentRule.DateStart.Year <= year && adjustmentRule.DateEnd.Year >= year && adjustmentRule.DaylightDelta != TimeSpan.Zero)
					{
						start = TimeZoneInfo.TransitionTimeToDateTime(year, adjustmentRule.DaylightTransitionStart);
						end = TimeZoneInfo.TransitionTimeToDateTime(year, adjustmentRule.DaylightTransitionEnd);
						delta = adjustmentRule.DaylightDelta;
						break;
					}
				}
			}
			return new DaylightTime(start, end, delta);
		}

		// Token: 0x06000CDB RID: 3291 RVA: 0x000CD350 File Offset: 0x000CC550
		public override TimeSpan GetUtcOffset(DateTime time)
		{
			if (time.Kind == DateTimeKind.Utc)
			{
				return TimeSpan.Zero;
			}
			return new TimeSpan(TimeZone.CalculateUtcOffset(time, this.GetDaylightChanges(time.Year)).Ticks + this.m_ticksOffset);
		}

		// Token: 0x06000CDC RID: 3292 RVA: 0x000CD394 File Offset: 0x000CC594
		private DaylightTime GetCachedDaylightChanges(int year)
		{
			object key = year;
			if (!this.m_CachedDaylightChanges.Contains(key))
			{
				DaylightTime value = CurrentSystemTimeZone.CreateDaylightChanges(year);
				Hashtable cachedDaylightChanges = this.m_CachedDaylightChanges;
				lock (cachedDaylightChanges)
				{
					if (!this.m_CachedDaylightChanges.Contains(key))
					{
						this.m_CachedDaylightChanges.Add(key, value);
					}
				}
			}
			return (DaylightTime)this.m_CachedDaylightChanges[key];
		}

		// Token: 0x040002B7 RID: 695
		private readonly long m_ticksOffset;

		// Token: 0x040002B8 RID: 696
		private readonly string m_standardName;

		// Token: 0x040002B9 RID: 697
		private readonly string m_daylightName;

		// Token: 0x040002BA RID: 698
		private readonly Hashtable m_CachedDaylightChanges = new Hashtable();
	}
}
