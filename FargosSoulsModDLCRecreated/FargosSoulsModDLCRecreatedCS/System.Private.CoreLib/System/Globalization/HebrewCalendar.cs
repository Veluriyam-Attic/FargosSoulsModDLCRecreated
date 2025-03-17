using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000207 RID: 519
	public class HebrewCalendar : Calendar
	{
		// Token: 0x17000753 RID: 1875
		// (get) Token: 0x0600210C RID: 8460 RVA: 0x0012CBD3 File Offset: 0x0012BDD3
		private unsafe static ReadOnlySpan<byte> HebrewTable
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.A72EB4166B1B422391E0F6E483BEF87AE75881E655BCB152E37F3D9688B2AA71), 1316);
			}
		}

		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x0600210D RID: 8461 RVA: 0x0012CBE4 File Offset: 0x0012BDE4
		private unsafe static ReadOnlySpan<byte> LunarMonthLen
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.582395A131FD1F6A949789B4B29B6A7E75B48DA700E8EF0842000BD9280CB880), 98);
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x0600210E RID: 8462 RVA: 0x0012CBF2 File Offset: 0x0012BDF2
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HebrewCalendar.s_calendarMinValue;
			}
		}

		// Token: 0x17000756 RID: 1878
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x0012CBF9 File Offset: 0x0012BDF9
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HebrewCalendar.s_calendarMaxValue;
			}
		}

		// Token: 0x17000757 RID: 1879
		// (get) Token: 0x06002110 RID: 8464 RVA: 0x000C9D36 File Offset: 0x000C8F36
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunisolarCalendar;
			}
		}

		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x06002112 RID: 8466 RVA: 0x000DAEBB File Offset: 0x000DA0BB
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.HEBREW;
			}
		}

		// Token: 0x06002113 RID: 8467 RVA: 0x0012CC00 File Offset: 0x0012BE00
		private static void CheckHebrewYearValue(int y, int era, string varName)
		{
			HebrewCalendar.CheckEraRange(era);
			if (y > 5999 || y < 5343)
			{
				throw new ArgumentOutOfRangeException(varName, y, SR.Format(SR.ArgumentOutOfRange_Range, 5343, 5999));
			}
		}

		// Token: 0x06002114 RID: 8468 RVA: 0x0012CC50 File Offset: 0x0012BE50
		private void CheckHebrewMonthValue(int year, int month, int era)
		{
			int monthsInYear = this.GetMonthsInYear(year, era);
			if (month < 1 || month > monthsInYear)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.Format(SR.ArgumentOutOfRange_Range, 1, monthsInYear));
			}
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x0012CC98 File Offset: 0x0012BE98
		private void CheckHebrewDayValue(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Range, 1, daysInMonth));
			}
		}

		// Token: 0x06002116 RID: 8470 RVA: 0x0012CCDF File Offset: 0x0012BEDF
		private static void CheckEraRange(int era)
		{
			if (era != 0 && era != HebrewCalendar.HebrewEra)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
		}

		// Token: 0x06002117 RID: 8471 RVA: 0x0012CD04 File Offset: 0x0012BF04
		private static void CheckTicksRange(long ticks)
		{
			if (ticks < HebrewCalendar.s_calendarMinValue.Ticks || ticks > HebrewCalendar.s_calendarMaxValue.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", ticks, SR.Format(CultureInfo.InvariantCulture, SR.ArgumentOutOfRange_CalendarRange, HebrewCalendar.s_calendarMinValue, HebrewCalendar.s_calendarMaxValue));
			}
		}

		// Token: 0x06002118 RID: 8472 RVA: 0x0012CD60 File Offset: 0x0012BF60
		private static int GetResult(HebrewCalendar.DateBuffer result, int part)
		{
			switch (part)
			{
			case 0:
				return result.year;
			case 2:
				return result.month;
			case 3:
				return result.day;
			}
			throw new InvalidOperationException(SR.InvalidOperation_DateTimeParsing);
		}

		// Token: 0x06002119 RID: 8473 RVA: 0x0012CDAC File Offset: 0x0012BFAC
		internal unsafe static int GetLunarMonthDay(int gregorianYear, HebrewCalendar.DateBuffer lunarDate)
		{
			int num = gregorianYear - 1583;
			if (num < 0 || num > 656)
			{
				throw new ArgumentOutOfRangeException("gregorianYear");
			}
			num *= 2;
			lunarDate.day = (int)(*HebrewCalendar.HebrewTable[num]);
			int result = (int)(*HebrewCalendar.HebrewTable[num + 1]);
			int day = lunarDate.day;
			if (day != 0)
			{
				switch (day)
				{
				case 30:
					lunarDate.month = 3;
					break;
				case 31:
					lunarDate.month = 5;
					lunarDate.day = 2;
					break;
				case 32:
					lunarDate.month = 5;
					lunarDate.day = 3;
					break;
				case 33:
					lunarDate.month = 3;
					lunarDate.day = 29;
					break;
				default:
					lunarDate.month = 4;
					break;
				}
			}
			else
			{
				lunarDate.month = 5;
				lunarDate.day = 1;
			}
			return result;
		}

		// Token: 0x0600211A RID: 8474 RVA: 0x0012CE7C File Offset: 0x0012C07C
		internal unsafe virtual int GetDatePart(long ticks, int part)
		{
			HebrewCalendar.CheckTicksRange(ticks);
			DateTime dateTime = new DateTime(ticks);
			int num;
			int num2;
			int num3;
			dateTime.GetDate(out num, out num2, out num3);
			HebrewCalendar.DateBuffer dateBuffer = new HebrewCalendar.DateBuffer();
			dateBuffer.year = num + 3760;
			int num4 = HebrewCalendar.GetLunarMonthDay(num, dateBuffer);
			HebrewCalendar.DateBuffer dateBuffer2 = new HebrewCalendar.DateBuffer();
			dateBuffer2.year = dateBuffer.year;
			dateBuffer2.month = dateBuffer.month;
			dateBuffer2.day = dateBuffer.day;
			long absoluteDate = GregorianCalendar.GetAbsoluteDate(num, num2, num3);
			if (num2 == 1 && num3 == 1)
			{
				return HebrewCalendar.GetResult(dateBuffer2, part);
			}
			long num5 = absoluteDate - GregorianCalendar.GetAbsoluteDate(num, 1, 1);
			if (num5 + (long)dateBuffer.day <= (long)((ulong)(*HebrewCalendar.LunarMonthLen[num4 * 14 + dateBuffer.month])))
			{
				dateBuffer2.day += (int)num5;
				return HebrewCalendar.GetResult(dateBuffer2, part);
			}
			dateBuffer2.month++;
			dateBuffer2.day = 1;
			num5 -= (long)((int)(*HebrewCalendar.LunarMonthLen[num4 * 14 + dateBuffer.month]) - dateBuffer.day);
			if (num5 > 1L)
			{
				while (num5 > (long)((ulong)(*HebrewCalendar.LunarMonthLen[num4 * 14 + dateBuffer2.month])))
				{
					long num6 = num5;
					ReadOnlySpan<byte> lunarMonthLen = HebrewCalendar.LunarMonthLen;
					int num7 = num4 * 14;
					HebrewCalendar.DateBuffer dateBuffer3 = dateBuffer2;
					int month = dateBuffer3.month;
					dateBuffer3.month = month + 1;
					num5 = num6 - (long)((ulong)(*lunarMonthLen[num7 + month]));
					if (dateBuffer2.month > 13 || *HebrewCalendar.LunarMonthLen[num4 * 14 + dateBuffer2.month] == 0)
					{
						dateBuffer2.year++;
						num4 = (int)(*HebrewCalendar.HebrewTable[(num + 1 - 1583) * 2 + 1]);
						dateBuffer2.month = 1;
					}
				}
				dateBuffer2.day += (int)(num5 - 1L);
			}
			return HebrewCalendar.GetResult(dateBuffer2, part);
		}

		// Token: 0x0600211B RID: 8475 RVA: 0x0012D078 File Offset: 0x0012C278
		public override DateTime AddMonths(DateTime time, int months)
		{
			DateTime result;
			try
			{
				int num = this.GetDatePart(time.Ticks, 0);
				int datePart = this.GetDatePart(time.Ticks, 2);
				int num2 = this.GetDatePart(time.Ticks, 3);
				int i;
				if (months >= 0)
				{
					int monthsInYear;
					for (i = datePart + months; i > (monthsInYear = this.GetMonthsInYear(num, 0)); i -= monthsInYear)
					{
						num++;
					}
				}
				else if ((i = datePart + months) <= 0)
				{
					months = -months;
					months -= datePart;
					num--;
					int monthsInYear;
					while (months > (monthsInYear = this.GetMonthsInYear(num, 0)))
					{
						num--;
						months -= monthsInYear;
					}
					monthsInYear = this.GetMonthsInYear(num, 0);
					i = monthsInYear - months;
				}
				int daysInMonth = this.GetDaysInMonth(num, i);
				if (num2 > daysInMonth)
				{
					num2 = daysInMonth;
				}
				result = this.ToDateTime(num, i, num2, 0, 0, 0, 0);
				result = new DateTime(result.Ticks + time.Ticks % 864000000000L);
			}
			catch (ArgumentException)
			{
				throw new ArgumentOutOfRangeException("months", months, SR.ArgumentOutOfRange_AddValue);
			}
			return result;
		}

		// Token: 0x0600211C RID: 8476 RVA: 0x0012D184 File Offset: 0x0012C384
		public override DateTime AddYears(DateTime time, int years)
		{
			int num = this.GetDatePart(time.Ticks, 0);
			int num2 = this.GetDatePart(time.Ticks, 2);
			int num3 = this.GetDatePart(time.Ticks, 3);
			num += years;
			HebrewCalendar.CheckHebrewYearValue(num, 0, "years");
			int monthsInYear = this.GetMonthsInYear(num, 0);
			if (num2 > monthsInYear)
			{
				num2 = monthsInYear;
			}
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long ticks = this.ToDateTime(num, num2, num3, 0, 0, 0, 0).Ticks + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		// Token: 0x0600211D RID: 8477 RVA: 0x0012D233 File Offset: 0x0012C433
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x0600211E RID: 8478 RVA: 0x0012BB9B File Offset: 0x0012AD9B
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x0600211F RID: 8479 RVA: 0x0012D244 File Offset: 0x0012C444
		internal unsafe static int GetHebrewYearType(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return (int)(*HebrewCalendar.HebrewTable[(year - 3760 - 1583) * 2 + 1]);
		}

		// Token: 0x06002120 RID: 8480 RVA: 0x0012D27C File Offset: 0x0012C47C
		public override int GetDayOfYear(DateTime time)
		{
			int year = this.GetYear(time);
			DateTime dateTime;
			if (year == 5343)
			{
				dateTime = new DateTime(1582, 9, 27);
			}
			else
			{
				dateTime = this.ToDateTime(year, 1, 1, 0, 0, 0, 0, 0);
			}
			return (int)((time.Ticks - dateTime.Ticks) / 864000000000L) + 1;
		}

		// Token: 0x06002121 RID: 8481 RVA: 0x0012D2D8 File Offset: 0x0012C4D8
		public unsafe override int GetDaysInMonth(int year, int month, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			int num = (int)(*HebrewCalendar.LunarMonthLen[hebrewYearType * 14 + month]);
			if (num == 0)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.ArgumentOutOfRange_Month);
			}
			return num;
		}

		// Token: 0x06002122 RID: 8482 RVA: 0x0012D32C File Offset: 0x0012C52C
		public override int GetDaysInYear(int year, int era)
		{
			HebrewCalendar.CheckEraRange(era);
			int hebrewYearType = HebrewCalendar.GetHebrewYearType(year, era);
			if (hebrewYearType < 4)
			{
				return 352 + hebrewYearType;
			}
			return 382 + (hebrewYearType - 3);
		}

		// Token: 0x06002123 RID: 8483 RVA: 0x0012D35C File Offset: 0x0012C55C
		public override int GetEra(DateTime time)
		{
			return HebrewCalendar.HebrewEra;
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x06002124 RID: 8484 RVA: 0x0012D363 File Offset: 0x0012C563
		[Nullable(1)]
		public override int[] Eras
		{
			[NullableContext(1)]
			get
			{
				return new int[]
				{
					HebrewCalendar.HebrewEra
				};
			}
		}

		// Token: 0x06002125 RID: 8485 RVA: 0x0012D373 File Offset: 0x0012C573
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x06002126 RID: 8486 RVA: 0x0012D383 File Offset: 0x0012C583
		public override int GetMonthsInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 12;
			}
			return 13;
		}

		// Token: 0x06002127 RID: 8487 RVA: 0x0012D394 File Offset: 0x0012C594
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x06002128 RID: 8488 RVA: 0x0012D3A4 File Offset: 0x0012C5A4
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			if (this.IsLeapMonth(year, month, era))
			{
				this.CheckHebrewDayValue(year, month, day, era);
				return true;
			}
			if (this.IsLeapYear(year, 0) && month == 6 && day == 30)
			{
				return true;
			}
			this.CheckHebrewDayValue(year, month, day, era);
			return false;
		}

		// Token: 0x06002129 RID: 8489 RVA: 0x0012D3E0 File Offset: 0x0012C5E0
		public override int GetLeapMonth(int year, int era)
		{
			if (this.IsLeapYear(year, era))
			{
				return 7;
			}
			return 0;
		}

		// Token: 0x0600212A RID: 8490 RVA: 0x0012D3F0 File Offset: 0x0012C5F0
		public override bool IsLeapMonth(int year, int month, int era)
		{
			bool flag = this.IsLeapYear(year, era);
			this.CheckHebrewMonthValue(year, month, era);
			return flag && month == 7;
		}

		// Token: 0x0600212B RID: 8491 RVA: 0x0012D418 File Offset: 0x0012C618
		public override bool IsLeapYear(int year, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			return (7L * (long)year + 1L) % 19L < 7L;
		}

		// Token: 0x0600212C RID: 8492 RVA: 0x0012D438 File Offset: 0x0012C638
		private unsafe static int GetDayDifference(int lunarYearType, int month1, int day1, int month2, int day2)
		{
			if (month1 == month2)
			{
				return day1 - day2;
			}
			bool flag = month1 > month2;
			if (flag)
			{
				int num = month1;
				int num2 = day1;
				month1 = month2;
				day1 = day2;
				month2 = num;
				day2 = num2;
			}
			int num3 = (int)(*HebrewCalendar.LunarMonthLen[lunarYearType * 14 + month1]) - day1;
			month1++;
			while (month1 < month2)
			{
				num3 += (int)(*HebrewCalendar.LunarMonthLen[lunarYearType * 14 + month1++]);
			}
			num3 += day2;
			if (!flag)
			{
				return -num3;
			}
			return num3;
		}

		// Token: 0x0600212D RID: 8493 RVA: 0x0012D4B4 File Offset: 0x0012C6B4
		private static DateTime HebrewToGregorian(int hebrewYear, int hebrewMonth, int hebrewDay, int hour, int minute, int second, int millisecond)
		{
			int num = hebrewYear - 3760;
			HebrewCalendar.DateBuffer dateBuffer = new HebrewCalendar.DateBuffer();
			int lunarMonthDay = HebrewCalendar.GetLunarMonthDay(num, dateBuffer);
			if (hebrewMonth == dateBuffer.month && hebrewDay == dateBuffer.day)
			{
				return new DateTime(num, 1, 1, hour, minute, second, millisecond);
			}
			int dayDifference = HebrewCalendar.GetDayDifference(lunarMonthDay, hebrewMonth, hebrewDay, dateBuffer.month, dateBuffer.day);
			DateTime dateTime = new DateTime(num, 1, 1);
			return new DateTime(dateTime.Ticks + (long)dayDifference * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
		}

		// Token: 0x0600212E RID: 8494 RVA: 0x0012D540 File Offset: 0x0012C740
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			HebrewCalendar.CheckHebrewYearValue(year, era, "year");
			this.CheckHebrewMonthValue(year, month, era);
			this.CheckHebrewDayValue(year, month, day, era);
			DateTime result = HebrewCalendar.HebrewToGregorian(year, month, day, hour, minute, second, millisecond);
			HebrewCalendar.CheckTicksRange(result.Ticks);
			return result;
		}

		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0012D58D File Offset: 0x0012C78D
		// (set) Token: 0x06002130 RID: 8496 RVA: 0x0012D5B4 File Offset: 0x0012C7B4
		public override int TwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == -1)
				{
					this._twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 5790);
				}
				return this._twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value != 99)
				{
					HebrewCalendar.CheckHebrewYearValue(value, HebrewCalendar.HebrewEra, "value");
				}
				this._twoDigitYearMax = value;
			}
		}

		// Token: 0x06002131 RID: 8497 RVA: 0x0012D5D8 File Offset: 0x0012C7D8
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (year < 100)
			{
				return base.ToFourDigitYear(year);
			}
			if (year > 5999 || year < 5343)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 5343, 5999));
			}
			return year;
		}

		// Token: 0x04000833 RID: 2099
		public static readonly int HebrewEra = 1;

		// Token: 0x04000834 RID: 2100
		private static readonly DateTime s_calendarMinValue = new DateTime(1583, 1, 1);

		// Token: 0x04000835 RID: 2101
		private static readonly DateTime s_calendarMaxValue = new DateTime(new DateTime(2239, 9, 29, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x02000208 RID: 520
		internal class DateBuffer
		{
			// Token: 0x04000836 RID: 2102
			internal int year;

			// Token: 0x04000837 RID: 2103
			internal int month;

			// Token: 0x04000838 RID: 2104
			internal int day;
		}
	}
}
