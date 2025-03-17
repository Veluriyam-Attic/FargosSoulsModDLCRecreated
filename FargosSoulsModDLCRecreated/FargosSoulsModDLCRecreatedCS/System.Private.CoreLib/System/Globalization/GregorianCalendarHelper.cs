using System;

namespace System.Globalization
{
	// Token: 0x02000205 RID: 517
	internal class GregorianCalendarHelper
	{
		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x060020EC RID: 8428 RVA: 0x0012C251 File Offset: 0x0012B451
		internal int MaxYear
		{
			get
			{
				return this.m_maxYear;
			}
		}

		// Token: 0x060020ED RID: 8429 RVA: 0x0012C259 File Offset: 0x0012B459
		internal GregorianCalendarHelper(Calendar cal, EraInfo[] eraInfo)
		{
			this.m_Cal = cal;
			this.m_EraInfo = eraInfo;
			this.m_maxYear = this.m_EraInfo[0].maxEraYear;
			this.m_minYear = this.m_EraInfo[0].minEraYear;
		}

		// Token: 0x060020EE RID: 8430 RVA: 0x0012C298 File Offset: 0x0012B498
		private int GetYearOffset(int year, int era, bool throwOnError)
		{
			if (year < 0)
			{
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("year", SR.ArgumentOutOfRange_NeedNonNegNum);
				}
				return -1;
			}
			else
			{
				if (era == 0)
				{
					era = this.m_Cal.CurrentEraValue;
				}
				int i = 0;
				while (i < this.m_EraInfo.Length)
				{
					if (era == this.m_EraInfo[i].era)
					{
						if (year >= this.m_EraInfo[i].minEraYear)
						{
							if (year <= this.m_EraInfo[i].maxEraYear)
							{
								return this.m_EraInfo[i].yearOffset;
							}
							if (!LocalAppContextSwitches.EnforceJapaneseEraYearRanges)
							{
								int num = year - this.m_EraInfo[i].maxEraYear;
								for (int j = i - 1; j >= 0; j--)
								{
									if (num <= this.m_EraInfo[j].maxEraYear)
									{
										return this.m_EraInfo[i].yearOffset;
									}
									num -= this.m_EraInfo[j].maxEraYear;
								}
							}
						}
						if (throwOnError)
						{
							throw new ArgumentOutOfRangeException("year", SR.Format(SR.ArgumentOutOfRange_Range, this.m_EraInfo[i].minEraYear, this.m_EraInfo[i].maxEraYear));
						}
						break;
					}
					else
					{
						i++;
					}
				}
				if (throwOnError)
				{
					throw new ArgumentOutOfRangeException("era", SR.ArgumentOutOfRange_InvalidEraValue);
				}
				return -1;
			}
		}

		// Token: 0x060020EF RID: 8431 RVA: 0x0012C3CB File Offset: 0x0012B5CB
		internal int GetGregorianYear(int year, int era)
		{
			return this.GetYearOffset(year, era, true) + year;
		}

		// Token: 0x060020F0 RID: 8432 RVA: 0x0012C3D8 File Offset: 0x0012B5D8
		internal bool IsValidYear(int year, int era)
		{
			return this.GetYearOffset(year, era, false) >= 0;
		}

		// Token: 0x060020F1 RID: 8433 RVA: 0x0012C3EC File Offset: 0x0012B5EC
		internal int GetDatePart(long ticks, int part)
		{
			this.CheckTicksRange(ticks);
			int i = (int)(ticks / 864000000000L);
			int num = i / 146097;
			i -= num * 146097;
			int num2 = i / 36524;
			if (num2 == 4)
			{
				num2 = 3;
			}
			i -= num2 * 36524;
			int num3 = i / 1461;
			i -= num3 * 1461;
			int num4 = i / 365;
			if (num4 == 4)
			{
				num4 = 3;
			}
			if (part == 0)
			{
				return num * 400 + num2 * 100 + num3 * 4 + num4 + 1;
			}
			i -= num4 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = (num4 == 3 && (num3 != 24 || num2 == 3)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365;
			int num5 = (i >> 5) + 1;
			while (i >= array[num5])
			{
				num5++;
			}
			if (part == 2)
			{
				return num5;
			}
			return i - array[num5 - 1] + 1;
		}

		// Token: 0x060020F2 RID: 8434 RVA: 0x0012C4D8 File Offset: 0x0012B6D8
		internal static long GetAbsoluteDate(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365;
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					int num2 = num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1;
					return (long)num2;
				}
			}
			throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadYearMonthDay);
		}

		// Token: 0x060020F3 RID: 8435 RVA: 0x0012C560 File Offset: 0x0012B760
		internal static long DateToTicks(int year, int month, int day)
		{
			return GregorianCalendarHelper.GetAbsoluteDate(year, month, day) * 864000000000L;
		}

		// Token: 0x060020F4 RID: 8436 RVA: 0x0012C574 File Offset: 0x0012B774
		internal static long TimeToTicks(int hour, int minute, int second, int millisecond)
		{
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
			}
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", SR.Format(SR.ArgumentOutOfRange_Range, 0, 999));
			}
			return InternalGlobalizationHelper.TimeToTicks(hour, minute, second) + (long)millisecond * 10000L;
		}

		// Token: 0x060020F5 RID: 8437 RVA: 0x0012C5EC File Offset: 0x0012B7EC
		internal void CheckTicksRange(long ticks)
		{
			if (ticks < this.m_Cal.MinSupportedDateTime.Ticks || ticks > this.m_Cal.MaxSupportedDateTime.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", SR.Format(CultureInfo.InvariantCulture, SR.ArgumentOutOfRange_CalendarRange, this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime));
			}
		}

		// Token: 0x060020F6 RID: 8438 RVA: 0x0012C660 File Offset: 0x0012B860
		public DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", SR.Format(SR.ArgumentOutOfRange_Range, -120000, 120000));
			}
			this.CheckTicksRange(time.Ticks);
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			int num4 = num2 - 1 + months;
			if (num4 >= 0)
			{
				num2 = num4 % 12 + 1;
				num += num4 / 12;
			}
			else
			{
				num2 = 12 + (num4 + 1) % 12;
				num += (num4 - 11) / 12;
			}
			int[] array = (num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365;
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long ticks = GregorianCalendarHelper.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(ticks, this.m_Cal.MinSupportedDateTime, this.m_Cal.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		// Token: 0x060020F7 RID: 8439 RVA: 0x0012C785 File Offset: 0x0012B985
		public DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x060020F8 RID: 8440 RVA: 0x0012C792 File Offset: 0x0012B992
		public int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x060020F9 RID: 8441 RVA: 0x0012C7A2 File Offset: 0x0012B9A2
		public DayOfWeek GetDayOfWeek(DateTime time)
		{
			this.CheckTicksRange(time.Ticks);
			return (DayOfWeek)((time.Ticks / 864000000000L + 1L) % 7L);
		}

		// Token: 0x060020FA RID: 8442 RVA: 0x0012C7C9 File Offset: 0x0012B9C9
		public int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x060020FB RID: 8443 RVA: 0x0012C7DC File Offset: 0x0012B9DC
		public int GetDaysInMonth(int year, int month, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", SR.ArgumentOutOfRange_Month);
			}
			int[] array = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendarHelper.DaysToMonth366 : GregorianCalendarHelper.DaysToMonth365;
			return array[month] - array[month - 1];
		}

		// Token: 0x060020FC RID: 8444 RVA: 0x0012C836 File Offset: 0x0012BA36
		public int GetDaysInYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			if (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x060020FD RID: 8445 RVA: 0x0012C864 File Offset: 0x0012BA64
		public int GetEra(DateTime time)
		{
			long ticks = time.Ticks;
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return this.m_EraInfo[i].era;
				}
			}
			throw new ArgumentOutOfRangeException("time", SR.ArgumentOutOfRange_Era);
		}

		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x060020FE RID: 8446 RVA: 0x0012C8BC File Offset: 0x0012BABC
		public int[] Eras
		{
			get
			{
				if (this.m_eras == null)
				{
					this.m_eras = new int[this.m_EraInfo.Length];
					for (int i = 0; i < this.m_EraInfo.Length; i++)
					{
						this.m_eras[i] = this.m_EraInfo[i].era;
					}
				}
				return (int[])this.m_eras.Clone();
			}
		}

		// Token: 0x060020FF RID: 8447 RVA: 0x0012C91C File Offset: 0x0012BB1C
		public int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x06002100 RID: 8448 RVA: 0x0012C92C File Offset: 0x0012BB2C
		public int GetMonthsInYear(int year, int era)
		{
			this.ValidateYearInEra(year, era);
			return 12;
		}

		// Token: 0x06002101 RID: 8449 RVA: 0x0012C938 File Offset: 0x0012BB38
		public int GetYear(DateTime time)
		{
			long ticks = time.Ticks;
			int datePart = this.GetDatePart(ticks, 0);
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks)
				{
					return datePart - this.m_EraInfo[i].yearOffset;
				}
			}
			throw new ArgumentException(SR.Argument_NoEra);
		}

		// Token: 0x06002102 RID: 8450 RVA: 0x0012C994 File Offset: 0x0012BB94
		public int GetYear(int year, DateTime time)
		{
			long ticks = time.Ticks;
			for (int i = 0; i < this.m_EraInfo.Length; i++)
			{
				if (ticks >= this.m_EraInfo[i].ticks && year > this.m_EraInfo[i].yearOffset)
				{
					return year - this.m_EraInfo[i].yearOffset;
				}
			}
			throw new ArgumentException(SR.Argument_NoEra);
		}

		// Token: 0x06002103 RID: 8451 RVA: 0x0012C9F8 File Offset: 0x0012BBF8
		public bool IsLeapDay(int year, int month, int day, int era)
		{
			if (day < 1 || day > this.GetDaysInMonth(year, month, era))
			{
				throw new ArgumentOutOfRangeException("day", SR.Format(SR.ArgumentOutOfRange_Range, 1, this.GetDaysInMonth(year, month, era)));
			}
			return this.IsLeapYear(year, era) && (month == 2 && day == 29);
		}

		// Token: 0x06002104 RID: 8452 RVA: 0x0012CA59 File Offset: 0x0012BC59
		public void ValidateYearInEra(int year, int era)
		{
			this.GetYearOffset(year, era, true);
		}

		// Token: 0x06002105 RID: 8453 RVA: 0x0012CA65 File Offset: 0x0012BC65
		public int GetLeapMonth(int year, int era)
		{
			this.ValidateYearInEra(year, era);
			return 0;
		}

		// Token: 0x06002106 RID: 8454 RVA: 0x0012CA70 File Offset: 0x0012BC70
		public bool IsLeapMonth(int year, int month, int era)
		{
			this.ValidateYearInEra(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", SR.Format(SR.ArgumentOutOfRange_Range, 1, 12));
			}
			return false;
		}

		// Token: 0x06002107 RID: 8455 RVA: 0x0012CAA6 File Offset: 0x0012BCA6
		public bool IsLeapYear(int year, int era)
		{
			year = this.GetGregorianYear(year, era);
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x06002108 RID: 8456 RVA: 0x0012CACC File Offset: 0x0012BCCC
		public DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			year = this.GetGregorianYear(year, era);
			long ticks = GregorianCalendarHelper.DateToTicks(year, month, day) + GregorianCalendarHelper.TimeToTicks(hour, minute, second, millisecond);
			this.CheckTicksRange(ticks);
			return new DateTime(ticks);
		}

		// Token: 0x06002109 RID: 8457 RVA: 0x0012CB08 File Offset: 0x0012BD08
		public int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			this.CheckTicksRange(time.Ticks);
			return GregorianCalendar.GetDefaultInstance().GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x0600210A RID: 8458 RVA: 0x0012CB24 File Offset: 0x0012BD24
		public int ToFourDigitYear(int year, int twoDigitYearMax)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (year < 100)
			{
				int num = year % 100;
				return (twoDigitYearMax / 100 - ((num > twoDigitYearMax % 100) ? 1 : 0)) * 100 + num;
			}
			if (year < this.m_minYear || year > this.m_maxYear)
			{
				throw new ArgumentOutOfRangeException("year", SR.Format(SR.ArgumentOutOfRange_Range, this.m_minYear, this.m_maxYear));
			}
			return year;
		}

		// Token: 0x04000825 RID: 2085
		internal static readonly int[] DaysToMonth365 = new int[]
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
			334,
			365
		};

		// Token: 0x04000826 RID: 2086
		internal static readonly int[] DaysToMonth366 = new int[]
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
			335,
			366
		};

		// Token: 0x04000827 RID: 2087
		internal int m_maxYear;

		// Token: 0x04000828 RID: 2088
		internal int m_minYear;

		// Token: 0x04000829 RID: 2089
		internal Calendar m_Cal;

		// Token: 0x0400082A RID: 2090
		internal EraInfo[] m_EraInfo;

		// Token: 0x0400082B RID: 2091
		internal int[] m_eras;
	}
}
