using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000200 RID: 512
	public abstract class EastAsianLunisolarCalendar : Calendar
	{
		// Token: 0x17000741 RID: 1857
		// (get) Token: 0x0600208D RID: 8333 RVA: 0x000C9D36 File Offset: 0x000C8F36
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		// Token: 0x0600208E RID: 8334 RVA: 0x0012AB24 File Offset: 0x00129D24
		public virtual int GetSexagenaryYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num;
			int num2;
			int num3;
			this.TimeToLunar(time, out num, out num2, out num3);
			return (num - 4) % 60 + 1;
		}

		// Token: 0x0600208F RID: 8335 RVA: 0x0012AB53 File Offset: 0x00129D53
		public int GetCelestialStem(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || sexagenaryYear > 60)
			{
				throw new ArgumentOutOfRangeException("sexagenaryYear", sexagenaryYear, SR.Format(SR.ArgumentOutOfRange_Range, 1, 60));
			}
			return (sexagenaryYear - 1) % 10 + 1;
		}

		// Token: 0x06002090 RID: 8336 RVA: 0x0012AB8E File Offset: 0x00129D8E
		public int GetTerrestrialBranch(int sexagenaryYear)
		{
			if (sexagenaryYear < 1 || sexagenaryYear > 60)
			{
				throw new ArgumentOutOfRangeException("sexagenaryYear", sexagenaryYear, SR.Format(SR.ArgumentOutOfRange_Range, 1, 60));
			}
			return (sexagenaryYear - 1) % 12 + 1;
		}

		// Token: 0x06002091 RID: 8337
		internal abstract int GetYearInfo(int LunarYear, int Index);

		// Token: 0x06002092 RID: 8338
		internal abstract int GetYear(int year, DateTime time);

		// Token: 0x06002093 RID: 8339
		internal abstract int GetGregorianYear(int year, int era);

		// Token: 0x17000742 RID: 1858
		// (get) Token: 0x06002094 RID: 8340
		internal abstract int MinCalendarYear { get; }

		// Token: 0x17000743 RID: 1859
		// (get) Token: 0x06002095 RID: 8341
		internal abstract int MaxCalendarYear { get; }

		// Token: 0x17000744 RID: 1860
		// (get) Token: 0x06002096 RID: 8342
		[Nullable(new byte[]
		{
			2,
			1
		})]
		internal abstract EraInfo[] CalEraInfo { get; }

		// Token: 0x17000745 RID: 1861
		// (get) Token: 0x06002097 RID: 8343
		internal abstract DateTime MinDate { get; }

		// Token: 0x17000746 RID: 1862
		// (get) Token: 0x06002098 RID: 8344
		internal abstract DateTime MaxDate { get; }

		// Token: 0x06002099 RID: 8345 RVA: 0x0012ABCC File Offset: 0x00129DCC
		internal int MinEraCalendarYear(int era)
		{
			EraInfo[] calEraInfo = this.CalEraInfo;
			if (calEraInfo == null)
			{
				return this.MinCalendarYear;
			}
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era == this.GetEra(this.MinDate))
			{
				return this.GetYear(this.MinCalendarYear, this.MinDate);
			}
			for (int i = 0; i < calEraInfo.Length; i++)
			{
				if (era == calEraInfo[i].era)
				{
					return calEraInfo[i].minEraYear;
				}
			}
			throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
		}

		// Token: 0x0600209A RID: 8346 RVA: 0x0012AC50 File Offset: 0x00129E50
		internal int MaxEraCalendarYear(int era)
		{
			EraInfo[] calEraInfo = this.CalEraInfo;
			if (calEraInfo == null)
			{
				return this.MaxCalendarYear;
			}
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era == this.GetEra(this.MaxDate))
			{
				return this.GetYear(this.MaxCalendarYear, this.MaxDate);
			}
			for (int i = 0; i < calEraInfo.Length; i++)
			{
				if (era == calEraInfo[i].era)
				{
					return calEraInfo[i].maxEraYear;
				}
			}
			throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
		}

		// Token: 0x0600209B RID: 8347 RVA: 0x0012ACD2 File Offset: 0x00129ED2
		internal EastAsianLunisolarCalendar()
		{
		}

		// Token: 0x0600209C RID: 8348 RVA: 0x0012ACDC File Offset: 0x00129EDC
		internal void CheckTicksRange(long ticks)
		{
			if (ticks < this.MinSupportedDateTime.Ticks || ticks > this.MaxSupportedDateTime.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", ticks, SR.Format(CultureInfo.InvariantCulture, SR.ArgumentOutOfRange_CalendarRange, this.MinSupportedDateTime, this.MaxSupportedDateTime));
			}
		}

		// Token: 0x0600209D RID: 8349 RVA: 0x0012AD44 File Offset: 0x00129F44
		internal void CheckEraRange(int era)
		{
			if (era == 0)
			{
				era = this.CurrentEraValue;
			}
			if (era < this.GetEra(this.MinDate) || era > this.GetEra(this.MaxDate))
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
		}

		// Token: 0x0600209E RID: 8350 RVA: 0x0012AD90 File Offset: 0x00129F90
		internal int CheckYearRange(int year, int era)
		{
			this.CheckEraRange(era);
			year = this.GetGregorianYear(year, era);
			if (year < this.MinCalendarYear || year > this.MaxCalendarYear)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, this.MinEraCalendarYear(era), this.MaxEraCalendarYear(era)));
			}
			return year;
		}

		// Token: 0x0600209F RID: 8351 RVA: 0x0012ADF4 File Offset: 0x00129FF4
		internal int CheckYearMonthRange(int year, int month, int era)
		{
			year = this.CheckYearRange(year, era);
			if (month == 13 && this.GetYearInfo(year, 0) == 0)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.ArgumentOutOfRange_Month);
			}
			if (month < 1 || month > 13)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.ArgumentOutOfRange_Month);
			}
			return year;
		}

		// Token: 0x060020A0 RID: 8352 RVA: 0x0012AE50 File Offset: 0x0012A050
		internal int InternalGetDaysInMonth(int year, int month)
		{
			int num = 32768;
			num >>= month - 1;
			if ((this.GetYearInfo(year, 3) & num) == 0)
			{
				return 29;
			}
			return 30;
		}

		// Token: 0x060020A1 RID: 8353 RVA: 0x0012AE7D File Offset: 0x0012A07D
		public override int GetDaysInMonth(int year, int month, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			return this.InternalGetDaysInMonth(year, month);
		}

		// Token: 0x060020A2 RID: 8354 RVA: 0x0012AE92 File Offset: 0x0012A092
		private static bool GregorianIsLeapYear(int y)
		{
			return y % 4 == 0 && (y % 100 != 0 || y % 400 == 0);
		}

		// Token: 0x060020A3 RID: 8355 RVA: 0x0012AEB0 File Offset: 0x0012A0B0
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int num = this.InternalGetDaysInMonth(year, month);
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Day, num, month));
			}
			int year2;
			int month2;
			int day2;
			if (!this.LunarToGregorian(year, month, day, out year2, out month2, out day2))
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadYearMonthDay);
			}
			return new DateTime(year2, month2, day2, hour, minute, second, millisecond);
		}

		// Token: 0x060020A4 RID: 8356 RVA: 0x0012AF30 File Offset: 0x0012A130
		private void GregorianToLunar(int solarYear, int solarMonth, int solarDate, out int lunarYear, out int lunarMonth, out int lunarDate)
		{
			int num = EastAsianLunisolarCalendar.GregorianIsLeapYear(solarYear) ? EastAsianLunisolarCalendar.s_daysToMonth366[solarMonth - 1] : EastAsianLunisolarCalendar.s_daysToMonth365[solarMonth - 1];
			num += solarDate;
			int i = num;
			lunarYear = solarYear;
			int yearInfo;
			int yearInfo2;
			if (lunarYear == this.MaxCalendarYear + 1)
			{
				lunarYear--;
				i += (EastAsianLunisolarCalendar.GregorianIsLeapYear(lunarYear) ? 366 : 365);
				yearInfo = this.GetYearInfo(lunarYear, 1);
				yearInfo2 = this.GetYearInfo(lunarYear, 2);
			}
			else
			{
				yearInfo = this.GetYearInfo(lunarYear, 1);
				yearInfo2 = this.GetYearInfo(lunarYear, 2);
				if (solarMonth < yearInfo || (solarMonth == yearInfo && solarDate < yearInfo2))
				{
					lunarYear--;
					i += (EastAsianLunisolarCalendar.GregorianIsLeapYear(lunarYear) ? 366 : 365);
					yearInfo = this.GetYearInfo(lunarYear, 1);
					yearInfo2 = this.GetYearInfo(lunarYear, 2);
				}
			}
			i -= EastAsianLunisolarCalendar.s_daysToMonth365[yearInfo - 1];
			i -= yearInfo2 - 1;
			int num2 = 32768;
			int yearInfo3 = this.GetYearInfo(lunarYear, 3);
			int num3 = ((yearInfo3 & num2) != 0) ? 30 : 29;
			lunarMonth = 1;
			while (i > num3)
			{
				i -= num3;
				lunarMonth++;
				num2 >>= 1;
				num3 = (((yearInfo3 & num2) != 0) ? 30 : 29);
			}
			lunarDate = i;
		}

		// Token: 0x060020A5 RID: 8357 RVA: 0x0012B078 File Offset: 0x0012A278
		private bool LunarToGregorian(int lunarYear, int lunarMonth, int lunarDate, out int solarYear, out int solarMonth, out int solarDay)
		{
			if (lunarDate < 1 || lunarDate > 30)
			{
				solarYear = 0;
				solarMonth = 0;
				solarDay = 0;
				return false;
			}
			int num = lunarDate - 1;
			for (int i = 1; i < lunarMonth; i++)
			{
				num += this.InternalGetDaysInMonth(lunarYear, i);
			}
			int yearInfo = this.GetYearInfo(lunarYear, 1);
			int yearInfo2 = this.GetYearInfo(lunarYear, 2);
			bool flag = EastAsianLunisolarCalendar.GregorianIsLeapYear(lunarYear);
			int[] array = flag ? EastAsianLunisolarCalendar.s_daysToMonth366 : EastAsianLunisolarCalendar.s_daysToMonth365;
			solarDay = yearInfo2;
			if (yearInfo > 1)
			{
				solarDay += array[yearInfo - 1];
			}
			solarDay += num;
			if (solarDay > 365 + (flag ? 1 : 0))
			{
				solarYear = lunarYear + 1;
				solarDay -= 365 + (flag ? 1 : 0);
			}
			else
			{
				solarYear = lunarYear;
			}
			solarMonth = 1;
			while (solarMonth < 12 && array[solarMonth] < solarDay)
			{
				solarMonth++;
			}
			solarDay -= array[solarMonth - 1];
			return true;
		}

		// Token: 0x060020A6 RID: 8358 RVA: 0x0012B168 File Offset: 0x0012A368
		private DateTime LunarToTime(DateTime time, int year, int month, int day)
		{
			int year2;
			int month2;
			int day2;
			this.LunarToGregorian(year, month, day, out year2, out month2, out day2);
			int hour;
			int minute;
			int second;
			int millisecond;
			time.GetTime(out hour, out minute, out second, out millisecond);
			return GregorianCalendar.GetDefaultInstance().ToDateTime(year2, month2, day2, hour, minute, second, millisecond);
		}

		// Token: 0x060020A7 RID: 8359 RVA: 0x0012B1AC File Offset: 0x0012A3AC
		private void TimeToLunar(DateTime time, out int year, out int month, out int day)
		{
			Calendar defaultInstance = GregorianCalendar.GetDefaultInstance();
			int year2 = defaultInstance.GetYear(time);
			int month2 = defaultInstance.GetMonth(time);
			int dayOfMonth = defaultInstance.GetDayOfMonth(time);
			this.GregorianToLunar(year2, month2, dayOfMonth, out year, out month, out day);
		}

		// Token: 0x060020A8 RID: 8360 RVA: 0x0012B1E4 File Offset: 0x0012A3E4
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", months, SR.Format(SR.ArgumentOutOfRange_Range, -120000, 120000));
			}
			this.CheckTicksRange(time.Ticks);
			int num;
			int num2;
			int num3;
			this.TimeToLunar(time, out num, out num2, out num3);
			int i = num2 + months;
			if (i > 0)
			{
				int num4 = this.InternalIsLeapYear(num) ? 13 : 12;
				while (i - num4 > 0)
				{
					i -= num4;
					num++;
					num4 = (this.InternalIsLeapYear(num) ? 13 : 12);
				}
				num2 = i;
			}
			else
			{
				while (i <= 0)
				{
					int num5 = this.InternalIsLeapYear(num - 1) ? 13 : 12;
					i += num5;
					num--;
				}
				num2 = i;
			}
			int num6 = this.InternalGetDaysInMonth(num, num2);
			if (num3 > num6)
			{
				num3 = num6;
			}
			DateTime result = this.LunarToTime(time, num, num2, num3);
			Calendar.CheckAddResult(result.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return result;
		}

		// Token: 0x060020A9 RID: 8361 RVA: 0x0012B2E4 File Offset: 0x0012A4E4
		public override DateTime AddYears(DateTime time, int years)
		{
			this.CheckTicksRange(time.Ticks);
			int num;
			int num2;
			int num3;
			this.TimeToLunar(time, out num, out num2, out num3);
			num += years;
			if (num2 == 13 && !this.InternalIsLeapYear(num))
			{
				num2 = 12;
				num3 = this.InternalGetDaysInMonth(num, num2);
			}
			int num4 = this.InternalGetDaysInMonth(num, num2);
			if (num3 > num4)
			{
				num3 = num4;
			}
			DateTime result = this.LunarToTime(time, num, num2, num3);
			Calendar.CheckAddResult(result.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return result;
		}

		// Token: 0x060020AA RID: 8362 RVA: 0x0012B360 File Offset: 0x0012A560
		public override int GetDayOfYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int year;
			int num;
			int num2;
			this.TimeToLunar(time, out year, out num, out num2);
			for (int i = 1; i < num; i++)
			{
				num2 += this.InternalGetDaysInMonth(year, i);
			}
			return num2;
		}

		// Token: 0x060020AB RID: 8363 RVA: 0x0012B3A0 File Offset: 0x0012A5A0
		public override int GetDayOfMonth(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num;
			int num2;
			int result;
			this.TimeToLunar(time, out num, out num2, out result);
			return result;
		}

		// Token: 0x060020AC RID: 8364 RVA: 0x0012B3C8 File Offset: 0x0012A5C8
		public override int GetDaysInYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			int num = 0;
			int num2 = this.InternalIsLeapYear(year) ? 13 : 12;
			while (num2 != 0)
			{
				num += this.InternalGetDaysInMonth(year, num2--);
			}
			return num;
		}

		// Token: 0x060020AD RID: 8365 RVA: 0x0012B408 File Offset: 0x0012A608
		public override int GetMonth(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int num;
			int result;
			int num2;
			this.TimeToLunar(time, out num, out result, out num2);
			return result;
		}

		// Token: 0x060020AE RID: 8366 RVA: 0x0012B430 File Offset: 0x0012A630
		public override int GetYear(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			int year;
			int num;
			int num2;
			this.TimeToLunar(time, out year, out num, out num2);
			return this.GetYear(year, time);
		}

		// Token: 0x060020AF RID: 8367 RVA: 0x0012B45F File Offset: 0x0012A65F
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x060020B0 RID: 8368 RVA: 0x0012B485 File Offset: 0x0012A685
		public override int GetMonthsInYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			if (!this.InternalIsLeapYear(year))
			{
				return 12;
			}
			return 13;
		}

		// Token: 0x060020B1 RID: 8369 RVA: 0x0012B4A0 File Offset: 0x0012A6A0
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int num = this.InternalGetDaysInMonth(year, month);
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Day, num, month));
			}
			int yearInfo = this.GetYearInfo(year, 0);
			return yearInfo != 0 && month == yearInfo + 1;
		}

		// Token: 0x060020B2 RID: 8370 RVA: 0x0012B508 File Offset: 0x0012A708
		public override bool IsLeapMonth(int year, int month, int era)
		{
			year = this.CheckYearMonthRange(year, month, era);
			int yearInfo = this.GetYearInfo(year, 0);
			return yearInfo != 0 && month == yearInfo + 1;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x0012B534 File Offset: 0x0012A734
		public override int GetLeapMonth(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			int yearInfo = this.GetYearInfo(year, 0);
			if (yearInfo <= 0)
			{
				return 0;
			}
			return yearInfo + 1;
		}

		// Token: 0x060020B4 RID: 8372 RVA: 0x0012B55D File Offset: 0x0012A75D
		internal bool InternalIsLeapYear(int year)
		{
			return this.GetYearInfo(year, 0) != 0;
		}

		// Token: 0x060020B5 RID: 8373 RVA: 0x0012B56A File Offset: 0x0012A76A
		public override bool IsLeapYear(int year, int era)
		{
			year = this.CheckYearRange(year, era);
			return this.InternalIsLeapYear(year);
		}

		// Token: 0x17000747 RID: 1863
		// (get) Token: 0x060020B6 RID: 8374 RVA: 0x0012B57D File Offset: 0x0012A77D
		// (set) Token: 0x060020B7 RID: 8375 RVA: 0x0012B5B4 File Offset: 0x0012A7B4
		public override int TwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == -1)
				{
					this._twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.BaseCalendarID, this.GetYear(new DateTime(2029, 1, 1)));
				}
				return this._twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.MaxCalendarYear)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 99, this.MaxCalendarYear));
				}
				this._twoDigitYearMax = value;
			}
		}

		// Token: 0x060020B8 RID: 8376 RVA: 0x0012B609 File Offset: 0x0012A809
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			year = base.ToFourDigitYear(year);
			this.CheckYearRange(year, 0);
			return year;
		}

		// Token: 0x04000814 RID: 2068
		private static readonly int[] s_daysToMonth365 = new int[]
		{
			0,
			31,
			59,
			90,
			120,
			151,
			181,
			212,
			243,
			273,
			304,
			334
		};

		// Token: 0x04000815 RID: 2069
		private static readonly int[] s_daysToMonth366 = new int[]
		{
			0,
			31,
			60,
			91,
			121,
			152,
			182,
			213,
			244,
			274,
			305,
			335
		};
	}
}
