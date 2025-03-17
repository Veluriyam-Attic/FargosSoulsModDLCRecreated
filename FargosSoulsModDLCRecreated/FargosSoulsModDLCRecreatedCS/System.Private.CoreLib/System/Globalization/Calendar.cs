using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x020001DA RID: 474
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Calendar : ICloneable
	{
		// Token: 0x1700067B RID: 1659
		// (get) Token: 0x06001DAE RID: 7598 RVA: 0x0011CCDB File Offset: 0x0011BEDB
		public virtual DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x1700067C RID: 1660
		// (get) Token: 0x06001DAF RID: 7599 RVA: 0x0011CCE2 File Offset: 0x0011BEE2
		public virtual DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x1700067D RID: 1661
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.Unknown;
			}
		}

		// Token: 0x1700067E RID: 1662
		// (get) Token: 0x06001DB2 RID: 7602 RVA: 0x000AC09B File Offset: 0x000AB29B
		internal virtual CalendarId ID
		{
			get
			{
				return CalendarId.UNINITIALIZED_VALUE;
			}
		}

		// Token: 0x1700067F RID: 1663
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x0011CCFF File Offset: 0x0011BEFF
		internal virtual CalendarId BaseCalendarID
		{
			get
			{
				return this.ID;
			}
		}

		// Token: 0x17000680 RID: 1664
		// (get) Token: 0x06001DB4 RID: 7604 RVA: 0x0011CD07 File Offset: 0x0011BF07
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x06001DB5 RID: 7605 RVA: 0x0011CD10 File Offset: 0x0011BF10
		public virtual object Clone()
		{
			object obj = base.MemberwiseClone();
			((Calendar)obj).SetReadOnlyState(false);
			return obj;
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x0011CD34 File Offset: 0x0011BF34
		public static Calendar ReadOnly(Calendar calendar)
		{
			if (calendar == null)
			{
				throw new ArgumentNullException("calendar");
			}
			if (calendar.IsReadOnly)
			{
				return calendar;
			}
			Calendar calendar2 = (Calendar)calendar.MemberwiseClone();
			calendar2.SetReadOnlyState(true);
			return calendar2;
		}

		// Token: 0x06001DB7 RID: 7607 RVA: 0x0011CD6D File Offset: 0x0011BF6D
		internal void VerifyWritable()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
			}
		}

		// Token: 0x06001DB8 RID: 7608 RVA: 0x0011CD82 File Offset: 0x0011BF82
		internal void SetReadOnlyState(bool readOnly)
		{
			this._isReadOnly = readOnly;
		}

		// Token: 0x17000681 RID: 1665
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x0011CD8B File Offset: 0x0011BF8B
		internal virtual int CurrentEraValue
		{
			get
			{
				if (this._currentEraValue == -1)
				{
					this._currentEraValue = CalendarData.GetCalendarCurrentEra(this);
				}
				return this._currentEraValue;
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x0011CDA8 File Offset: 0x0011BFA8
		internal static void CheckAddResult(long ticks, DateTime minValue, DateTime maxValue)
		{
			if (ticks < minValue.Ticks || ticks > maxValue.Ticks)
			{
				throw new ArgumentException(SR.Format(SR.Argument_ResultCalendarRange, minValue, maxValue));
			}
		}

		// Token: 0x06001DBB RID: 7611 RVA: 0x0011CDDC File Offset: 0x0011BFDC
		internal DateTime Add(DateTime time, double value, int scale)
		{
			double num = value * (double)scale + ((value >= 0.0) ? 0.5 : -0.5);
			if (num <= -315537897600000.0 || num >= 315537897600000.0)
			{
				throw new ArgumentOutOfRangeException("value", value, SR.ArgumentOutOfRange_AddValue);
			}
			long num2 = (long)num;
			long ticks = time.Ticks + num2 * 10000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x0011CE69 File Offset: 0x0011C069
		public virtual DateTime AddMilliseconds(DateTime time, double milliseconds)
		{
			return this.Add(time, milliseconds, 1);
		}

		// Token: 0x06001DBD RID: 7613 RVA: 0x0011CE74 File Offset: 0x0011C074
		public virtual DateTime AddDays(DateTime time, int days)
		{
			return this.Add(time, (double)days, 86400000);
		}

		// Token: 0x06001DBE RID: 7614 RVA: 0x0011CE84 File Offset: 0x0011C084
		public virtual DateTime AddHours(DateTime time, int hours)
		{
			return this.Add(time, (double)hours, 3600000);
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x0011CE94 File Offset: 0x0011C094
		public virtual DateTime AddMinutes(DateTime time, int minutes)
		{
			return this.Add(time, (double)minutes, 60000);
		}

		// Token: 0x06001DC0 RID: 7616
		public abstract DateTime AddMonths(DateTime time, int months);

		// Token: 0x06001DC1 RID: 7617 RVA: 0x0011CEA4 File Offset: 0x0011C0A4
		public virtual DateTime AddSeconds(DateTime time, int seconds)
		{
			return this.Add(time, (double)seconds, 1000);
		}

		// Token: 0x06001DC2 RID: 7618 RVA: 0x0011CEB4 File Offset: 0x0011C0B4
		public virtual DateTime AddWeeks(DateTime time, int weeks)
		{
			return this.AddDays(time, weeks * 7);
		}

		// Token: 0x06001DC3 RID: 7619
		public abstract DateTime AddYears(DateTime time, int years);

		// Token: 0x06001DC4 RID: 7620
		public abstract int GetDayOfMonth(DateTime time);

		// Token: 0x06001DC5 RID: 7621
		public abstract DayOfWeek GetDayOfWeek(DateTime time);

		// Token: 0x06001DC6 RID: 7622
		public abstract int GetDayOfYear(DateTime time);

		// Token: 0x06001DC7 RID: 7623 RVA: 0x0011CEC0 File Offset: 0x0011C0C0
		public virtual int GetDaysInMonth(int year, int month)
		{
			return this.GetDaysInMonth(year, month, 0);
		}

		// Token: 0x06001DC8 RID: 7624
		public abstract int GetDaysInMonth(int year, int month, int era);

		// Token: 0x06001DC9 RID: 7625 RVA: 0x0011CECB File Offset: 0x0011C0CB
		public virtual int GetDaysInYear(int year)
		{
			return this.GetDaysInYear(year, 0);
		}

		// Token: 0x06001DCA RID: 7626
		public abstract int GetDaysInYear(int year, int era);

		// Token: 0x06001DCB RID: 7627
		public abstract int GetEra(DateTime time);

		// Token: 0x17000682 RID: 1666
		// (get) Token: 0x06001DCC RID: 7628
		public abstract int[] Eras { get; }

		// Token: 0x06001DCD RID: 7629 RVA: 0x0011CED5 File Offset: 0x0011C0D5
		public virtual int GetHour(DateTime time)
		{
			return (int)(time.Ticks / 36000000000L % 24L);
		}

		// Token: 0x06001DCE RID: 7630 RVA: 0x0011CEED File Offset: 0x0011C0ED
		public virtual double GetMilliseconds(DateTime time)
		{
			return (double)(time.Ticks / 10000L % 1000L);
		}

		// Token: 0x06001DCF RID: 7631 RVA: 0x0011CF05 File Offset: 0x0011C105
		public virtual int GetMinute(DateTime time)
		{
			return (int)(time.Ticks / 600000000L % 60L);
		}

		// Token: 0x06001DD0 RID: 7632
		public abstract int GetMonth(DateTime time);

		// Token: 0x06001DD1 RID: 7633 RVA: 0x0011CF1A File Offset: 0x0011C11A
		public virtual int GetMonthsInYear(int year)
		{
			return this.GetMonthsInYear(year, 0);
		}

		// Token: 0x06001DD2 RID: 7634
		public abstract int GetMonthsInYear(int year, int era);

		// Token: 0x06001DD3 RID: 7635 RVA: 0x0011CF24 File Offset: 0x0011C124
		public virtual int GetSecond(DateTime time)
		{
			return (int)(time.Ticks / 10000000L % 60L);
		}

		// Token: 0x06001DD4 RID: 7636 RVA: 0x0011CF3C File Offset: 0x0011C13C
		internal int GetFirstDayWeekOfYear(DateTime time, int firstDayOfWeek)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = this.GetDayOfWeek(time) - (DayOfWeek)(num % 7);
			int num3 = (num2 - firstDayOfWeek + 14) % 7;
			return (num + num3) / 7 + 1;
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0011CF70 File Offset: 0x0011C170
		private int GetWeekOfYearFullDays(DateTime time, int firstDayOfWeek, int fullDays)
		{
			int num = this.GetDayOfYear(time) - 1;
			int num2 = this.GetDayOfWeek(time) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek - num2 + 14) % 7;
			if (num3 != 0 && num3 >= fullDays)
			{
				num3 -= 7;
			}
			int num4 = num - num3;
			if (num4 >= 0)
			{
				return num4 / 7 + 1;
			}
			if (time <= this.MinSupportedDateTime.AddDays((double)num))
			{
				return this.GetWeekOfYearOfMinSupportedDateTime(firstDayOfWeek, fullDays);
			}
			return this.GetWeekOfYearFullDays(time.AddDays((double)(-(double)(num + 1))), firstDayOfWeek, fullDays);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x0011CFEC File Offset: 0x0011C1EC
		private int GetWeekOfYearOfMinSupportedDateTime(int firstDayOfWeek, int minimumDaysInFirstWeek)
		{
			int num = this.GetDayOfYear(this.MinSupportedDateTime) - 1;
			int num2 = this.GetDayOfWeek(this.MinSupportedDateTime) - (DayOfWeek)(num % 7);
			int num3 = (firstDayOfWeek + 7 - num2) % 7;
			if (num3 == 0 || num3 >= minimumDaysInFirstWeek)
			{
				return 1;
			}
			int num4 = this.DaysInYearBeforeMinSupportedYear - 1;
			int num5 = num2 - 1 - num4 % 7;
			int num6 = (firstDayOfWeek - num5 + 14) % 7;
			int num7 = num4 - num6;
			if (num6 >= minimumDaysInFirstWeek)
			{
				num7 += 7;
			}
			return num7 / 7 + 1;
		}

		// Token: 0x17000683 RID: 1667
		// (get) Token: 0x06001DD7 RID: 7639 RVA: 0x0011D05E File Offset: 0x0011C25E
		protected virtual int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 365;
			}
		}

		// Token: 0x06001DD8 RID: 7640 RVA: 0x0011D068 File Offset: 0x0011C268
		public virtual int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			if (firstDayOfWeek < DayOfWeek.Sunday || firstDayOfWeek > DayOfWeek.Saturday)
			{
				throw new ArgumentOutOfRangeException("firstDayOfWeek", firstDayOfWeek, SR.Format(SR.ArgumentOutOfRange_Range, DayOfWeek.Sunday, DayOfWeek.Saturday));
			}
			int result;
			switch (rule)
			{
			case CalendarWeekRule.FirstDay:
				result = this.GetFirstDayWeekOfYear(time, (int)firstDayOfWeek);
				break;
			case CalendarWeekRule.FirstFullWeek:
				result = this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 7);
				break;
			case CalendarWeekRule.FirstFourDayWeek:
				result = this.GetWeekOfYearFullDays(time, (int)firstDayOfWeek, 4);
				break;
			default:
				throw new ArgumentOutOfRangeException("rule", rule, SR.Format(SR.ArgumentOutOfRange_Range, CalendarWeekRule.FirstDay, CalendarWeekRule.FirstFourDayWeek));
			}
			return result;
		}

		// Token: 0x06001DD9 RID: 7641
		public abstract int GetYear(DateTime time);

		// Token: 0x06001DDA RID: 7642 RVA: 0x0011D103 File Offset: 0x0011C303
		public virtual bool IsLeapDay(int year, int month, int day)
		{
			return this.IsLeapDay(year, month, day, 0);
		}

		// Token: 0x06001DDB RID: 7643
		public abstract bool IsLeapDay(int year, int month, int day, int era);

		// Token: 0x06001DDC RID: 7644 RVA: 0x0011D10F File Offset: 0x0011C30F
		public virtual bool IsLeapMonth(int year, int month)
		{
			return this.IsLeapMonth(year, month, 0);
		}

		// Token: 0x06001DDD RID: 7645
		public abstract bool IsLeapMonth(int year, int month, int era);

		// Token: 0x06001DDE RID: 7646 RVA: 0x0011D11A File Offset: 0x0011C31A
		public virtual int GetLeapMonth(int year)
		{
			return this.GetLeapMonth(year, 0);
		}

		// Token: 0x06001DDF RID: 7647 RVA: 0x0011D124 File Offset: 0x0011C324
		public virtual int GetLeapMonth(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 0;
			}
			int monthsInYear = this.GetMonthsInYear(year, era);
			for (int i = 1; i <= monthsInYear; i++)
			{
				if (this.IsLeapMonth(year, i, era))
				{
					return i;
				}
			}
			return 0;
		}

		// Token: 0x06001DE0 RID: 7648 RVA: 0x0011D160 File Offset: 0x0011C360
		public virtual bool IsLeapYear(int year)
		{
			return this.IsLeapYear(year, 0);
		}

		// Token: 0x06001DE1 RID: 7649
		public abstract bool IsLeapYear(int year, int era);

		// Token: 0x06001DE2 RID: 7650 RVA: 0x0011D16C File Offset: 0x0011C36C
		public virtual DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond)
		{
			return this.ToDateTime(year, month, day, hour, minute, second, millisecond, 0);
		}

		// Token: 0x06001DE3 RID: 7651
		public abstract DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era);

		// Token: 0x06001DE4 RID: 7652 RVA: 0x0011D18C File Offset: 0x0011C38C
		internal virtual bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
		{
			result = DateTime.MinValue;
			bool result2;
			try
			{
				result = this.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
				result2 = true;
			}
			catch (ArgumentException)
			{
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06001DE5 RID: 7653 RVA: 0x0011D1DC File Offset: 0x0011C3DC
		internal virtual bool IsValidYear(int year, int era)
		{
			return year >= this.GetYear(this.MinSupportedDateTime) && year <= this.GetYear(this.MaxSupportedDateTime);
		}

		// Token: 0x06001DE6 RID: 7654 RVA: 0x0011D201 File Offset: 0x0011C401
		internal virtual bool IsValidMonth(int year, int month, int era)
		{
			return this.IsValidYear(year, era) && month >= 1 && month <= this.GetMonthsInYear(year, era);
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0011D221 File Offset: 0x0011C421
		internal virtual bool IsValidDay(int year, int month, int day, int era)
		{
			return this.IsValidMonth(year, month, era) && day >= 1 && day <= this.GetDaysInMonth(year, month, era);
		}

		// Token: 0x17000684 RID: 1668
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x0011D245 File Offset: 0x0011C445
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x0011D24D File Offset: 0x0011C44D
		public virtual int TwoDigitYearMax
		{
			get
			{
				return this._twoDigitYearMax;
			}
			set
			{
				this.VerifyWritable();
				this._twoDigitYearMax = value;
			}
		}

		// Token: 0x06001DEA RID: 7658 RVA: 0x0011D25C File Offset: 0x0011C45C
		public virtual int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (year < 100)
			{
				return (this.TwoDigitYearMax / 100 - ((year > this.TwoDigitYearMax % 100) ? 1 : 0)) * 100 + year;
			}
			return year;
		}

		// Token: 0x06001DEB RID: 7659 RVA: 0x0011D2AC File Offset: 0x0011C4AC
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", millisecond, SR.Format(SR.ArgumentOutOfRange_Range, 0, 999));
			}
			return InternalGlobalizationHelper.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x06001DEC RID: 7660 RVA: 0x0011D32C File Offset: 0x0011C52C
		internal static int GetSystemTwoDigitYearSetting(CalendarId CalID, int defaultYearValue)
		{
			int num = GlobalizationMode.UseNls ? CalendarData.NlsGetTwoDigitYearMax(CalID) : CalendarData.IcuGetTwoDigitYearMax(CalID);
			if (num < 0)
			{
				return defaultYearValue;
			}
			return num;
		}

		// Token: 0x0400067E RID: 1662
		private int _currentEraValue = -1;

		// Token: 0x0400067F RID: 1663
		private bool _isReadOnly;

		// Token: 0x04000680 RID: 1664
		public const int CurrentEra = 0;

		// Token: 0x04000681 RID: 1665
		internal int _twoDigitYearMax = -1;
	}
}
