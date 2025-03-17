using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x0200021F RID: 543
	[Nullable(0)]
	[NullableContext(1)]
	public class PersianCalendar : Calendar
	{
		// Token: 0x170007AE RID: 1966
		// (get) Token: 0x06002285 RID: 8837 RVA: 0x001331E4 File Offset: 0x001323E4
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return PersianCalendar.s_minDate;
			}
		}

		// Token: 0x170007AF RID: 1967
		// (get) Token: 0x06002286 RID: 8838 RVA: 0x001331EB File Offset: 0x001323EB
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return PersianCalendar.s_maxDate;
			}
		}

		// Token: 0x170007B0 RID: 1968
		// (get) Token: 0x06002287 RID: 8839 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x170007B1 RID: 1969
		// (get) Token: 0x06002289 RID: 8841 RVA: 0x000AC09E File Offset: 0x000AB29E
		internal override CalendarId BaseCalendarID
		{
			get
			{
				return CalendarId.GREGORIAN;
			}
		}

		// Token: 0x170007B2 RID: 1970
		// (get) Token: 0x0600228A RID: 8842 RVA: 0x001331F2 File Offset: 0x001323F2
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.PERSIAN;
			}
		}

		// Token: 0x0600228B RID: 8843 RVA: 0x001331F8 File Offset: 0x001323F8
		private long GetAbsoluteDatePersian(int year, int month, int day)
		{
			if (year < 1 || year > 9378 || month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadYearMonthDay);
			}
			int num = PersianCalendar.DaysInPreviousMonths(month) + day - 1;
			int num2 = (int)(365.242189 * (double)(year - 1));
			long num3 = CalendricalCalculationsHelper.PersianNewYearOnOrBefore(PersianCalendar.s_persianEpoch + (long)num2 + 180L);
			return num3 + (long)num;
		}

		// Token: 0x0600228C RID: 8844 RVA: 0x0013325C File Offset: 0x0013245C
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < PersianCalendar.s_minDate.Ticks || ticks > PersianCalendar.s_maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", ticks, SR.Format(SR.ArgumentOutOfRange_CalendarRange, PersianCalendar.s_minDate, PersianCalendar.s_maxDate));
			}
		}

		// Token: 0x0600228D RID: 8845 RVA: 0x001332B2 File Offset: 0x001324B2
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != PersianCalendar.PersianEra)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
		}

		// Token: 0x0600228E RID: 8846 RVA: 0x001332D5 File Offset: 0x001324D5
		internal static void CheckYearRange(int year, int era)
		{
			PersianCalendar.CheckEraRange(era);
			if (year < 1 || year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9378));
			}
		}

		// Token: 0x0600228F RID: 8847 RVA: 0x00133314 File Offset: 0x00132514
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378 && month > 10)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.Format(SR.ArgumentOutOfRange_Range, 1, 10));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.ArgumentOutOfRange_Month);
			}
		}

		// Token: 0x06002290 RID: 8848 RVA: 0x0013337C File Offset: 0x0013257C
		private static int MonthFromOrdinalDay(int ordinalDay)
		{
			int num = 0;
			while (ordinalDay > PersianCalendar.s_daysToMonth[num])
			{
				num++;
			}
			return num;
		}

		// Token: 0x06002291 RID: 8849 RVA: 0x0013339C File Offset: 0x0013259C
		private static int DaysInPreviousMonths(int month)
		{
			month--;
			return PersianCalendar.s_daysToMonth[month];
		}

		// Token: 0x06002292 RID: 8850 RVA: 0x001333AC File Offset: 0x001325AC
		internal int GetDatePart(long ticks, int part)
		{
			PersianCalendar.CheckTicksRange(ticks);
			long num = ticks / 864000000000L + 1L;
			long num2 = CalendricalCalculationsHelper.PersianNewYearOnOrBefore(num);
			int num3 = (int)Math.Floor((double)(num2 - PersianCalendar.s_persianEpoch) / 365.242189 + 0.5) + 1;
			if (part == 0)
			{
				return num3;
			}
			int num4 = (int)(num - CalendricalCalculationsHelper.GetNumberOfDays(this.ToDateTime(num3, 1, 1, 0, 0, 0, 0, 1)));
			if (part == 1)
			{
				return num4;
			}
			int num5 = PersianCalendar.MonthFromOrdinalDay(num4);
			if (part == 2)
			{
				return num5;
			}
			int result = num4 - PersianCalendar.DaysInPreviousMonths(num5);
			if (part == 3)
			{
				return result;
			}
			throw new InvalidOperationException(SR.InvalidOperation_DateTimeParsing);
		}

		// Token: 0x06002293 RID: 8851 RVA: 0x00133448 File Offset: 0x00132648
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", months, SR.Format(SR.ArgumentOutOfRange_Range, -120000, 120000));
			}
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
			int daysInMonth = this.GetDaysInMonth(num, num2);
			if (num3 > daysInMonth)
			{
				num3 = daysInMonth;
			}
			long ticks = this.GetAbsoluteDatePersian(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		// Token: 0x06002294 RID: 8852 RVA: 0x0012BB85 File Offset: 0x0012AD85
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x06002295 RID: 8853 RVA: 0x00133542 File Offset: 0x00132742
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x06002296 RID: 8854 RVA: 0x0012BB9B File Offset: 0x0012AD9B
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x06002297 RID: 8855 RVA: 0x00133552 File Offset: 0x00132752
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x06002298 RID: 8856 RVA: 0x00133564 File Offset: 0x00132764
		public override int GetDaysInMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			if (month == 10 && year == 9378)
			{
				return 13;
			}
			int num = PersianCalendar.s_daysToMonth[month] - PersianCalendar.s_daysToMonth[month - 1];
			if (month == 12 && !this.IsLeapYear(year))
			{
				num--;
			}
			return num;
		}

		// Token: 0x06002299 RID: 8857 RVA: 0x001335AE File Offset: 0x001327AE
		public override int GetDaysInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return PersianCalendar.s_daysToMonth[9] + 13;
			}
			if (!this.IsLeapYear(year, 0))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x0600229A RID: 8858 RVA: 0x001335E0 File Offset: 0x001327E0
		public override int GetEra(DateTime time)
		{
			PersianCalendar.CheckTicksRange(time.Ticks);
			return PersianCalendar.PersianEra;
		}

		// Token: 0x170007B3 RID: 1971
		// (get) Token: 0x0600229B RID: 8859 RVA: 0x001335F3 File Offset: 0x001327F3
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					PersianCalendar.PersianEra
				};
			}
		}

		// Token: 0x0600229C RID: 8860 RVA: 0x00133603 File Offset: 0x00132803
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x0600229D RID: 8861 RVA: 0x00133613 File Offset: 0x00132813
		public override int GetMonthsInYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			if (year == 9378)
			{
				return 10;
			}
			return 12;
		}

		// Token: 0x0600229E RID: 8862 RVA: 0x00133629 File Offset: 0x00132829
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x0600229F RID: 8863 RVA: 0x0012DFC0 File Offset: 0x0012D1C0
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Day, daysInMonth, month));
			}
			return this.IsLeapYear(year, era) && month == 12 && day == 30;
		}

		// Token: 0x060022A0 RID: 8864 RVA: 0x00133639 File Offset: 0x00132839
		public override int GetLeapMonth(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return 0;
		}

		// Token: 0x060022A1 RID: 8865 RVA: 0x00133643 File Offset: 0x00132843
		public override bool IsLeapMonth(int year, int month, int era)
		{
			PersianCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		// Token: 0x060022A2 RID: 8866 RVA: 0x0013364E File Offset: 0x0013284E
		public override bool IsLeapYear(int year, int era)
		{
			PersianCalendar.CheckYearRange(year, era);
			return year != 9378 && this.GetAbsoluteDatePersian(year + 1, 1, 1) - this.GetAbsoluteDatePersian(year, 1, 1) == 366L;
		}

		// Token: 0x060022A3 RID: 8867 RVA: 0x00133680 File Offset: 0x00132880
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Day, daysInMonth, month));
			}
			long absoluteDatePersian = this.GetAbsoluteDatePersian(year, month, day);
			if (absoluteDatePersian < 0L)
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadYearMonthDay);
			}
			return new DateTime(absoluteDatePersian * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
		}

		// Token: 0x170007B4 RID: 1972
		// (get) Token: 0x060022A4 RID: 8868 RVA: 0x00133700 File Offset: 0x00132900
		// (set) Token: 0x060022A5 RID: 8869 RVA: 0x00133728 File Offset: 0x00132928
		public override int TwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == -1)
				{
					this._twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1410);
				}
				return this._twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9378)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 99, 9378));
				}
				this._twoDigitYearMax = value;
			}
		}

		// Token: 0x060022A6 RID: 8870 RVA: 0x0013377C File Offset: 0x0013297C
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
			if (year > 9378)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9378));
			}
			return year;
		}

		// Token: 0x040008C3 RID: 2243
		public static readonly int PersianEra = 1;

		// Token: 0x040008C4 RID: 2244
		private static readonly long s_persianEpoch = new DateTime(622, 3, 22).Ticks / 864000000000L;

		// Token: 0x040008C5 RID: 2245
		private static readonly int[] s_daysToMonth = new int[]
		{
			0,
			31,
			62,
			93,
			124,
			155,
			186,
			216,
			246,
			276,
			306,
			336,
			366
		};

		// Token: 0x040008C6 RID: 2246
		private static readonly DateTime s_minDate = new DateTime(622, 3, 22);

		// Token: 0x040008C7 RID: 2247
		private static readonly DateTime s_maxDate = DateTime.MaxValue;
	}
}
