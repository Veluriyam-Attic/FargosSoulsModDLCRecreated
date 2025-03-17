using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000217 RID: 535
	[Nullable(0)]
	[NullableContext(1)]
	public class JulianCalendar : Calendar
	{
		// Token: 0x17000778 RID: 1912
		// (get) Token: 0x060021D2 RID: 8658 RVA: 0x0011CCDB File Offset: 0x0011BEDB
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x17000779 RID: 1913
		// (get) Token: 0x060021D3 RID: 8659 RVA: 0x0011CCE2 File Offset: 0x0011BEE2
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x060021D4 RID: 8660 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x060021D5 RID: 8661 RVA: 0x00130458 File Offset: 0x0012F658
		public JulianCalendar()
		{
			this._twoDigitYearMax = 2029;
		}

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x000E8E93 File Offset: 0x000E8093
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.JULIAN;
			}
		}

		// Token: 0x060021D7 RID: 8663 RVA: 0x00130476 File Offset: 0x0012F676
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != JulianCalendar.JulianEra)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
		}

		// Token: 0x060021D8 RID: 8664 RVA: 0x0013049C File Offset: 0x0012F69C
		internal void CheckYearEraRange(int year, int era)
		{
			JulianCalendar.CheckEraRange(era);
			if (year <= 0 || year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, this.MaxYear));
			}
		}

		// Token: 0x060021D9 RID: 8665 RVA: 0x001304E8 File Offset: 0x0012F6E8
		internal static void CheckMonthRange(int month)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.ArgumentOutOfRange_Month);
			}
		}

		// Token: 0x060021DA RID: 8666 RVA: 0x0013050C File Offset: 0x0012F70C
		internal static void CheckDayRange(int year, int month, int day)
		{
			if (year == 1 && month == 1 && day < 3)
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadYearMonthDay);
			}
			int[] array = (year % 4 == 0) ? JulianCalendar.s_daysToMonth366 : JulianCalendar.s_daysToMonth365;
			int num = array[month] - array[month - 1];
			if (day < 1 || day > num)
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Range, 1, num));
			}
		}

		// Token: 0x060021DB RID: 8667 RVA: 0x00130584 File Offset: 0x0012F784
		internal static int GetDatePart(long ticks, int part)
		{
			long num = ticks + 1728000000000L;
			int i = (int)(num / 864000000000L);
			int num2 = i / 1461;
			i -= num2 * 1461;
			int num3 = i / 365;
			if (num3 == 4)
			{
				num3 = 3;
			}
			if (part == 0)
			{
				return num2 * 4 + num3 + 1;
			}
			i -= num3 * 365;
			if (part == 1)
			{
				return i + 1;
			}
			int[] array = (num3 == 3) ? JulianCalendar.s_daysToMonth366 : JulianCalendar.s_daysToMonth365;
			int num4 = (i >> 5) + 1;
			while (i >= array[num4])
			{
				num4++;
			}
			if (part == 2)
			{
				return num4;
			}
			return i - array[num4 - 1] + 1;
		}

		// Token: 0x060021DC RID: 8668 RVA: 0x00130628 File Offset: 0x0012F828
		internal static long DateToTicks(int year, int month, int day)
		{
			int[] array = (year % 4 == 0) ? JulianCalendar.s_daysToMonth366 : JulianCalendar.s_daysToMonth365;
			int num = year - 1;
			int num2 = num * 365 + num / 4 + array[month - 1] + day - 1;
			return (long)(num2 - 2) * 864000000000L;
		}

		// Token: 0x060021DD RID: 8669 RVA: 0x00130670 File Offset: 0x0012F870
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", months, SR.Format(SR.ArgumentOutOfRange_Range, -120000, 120000));
			}
			int num = JulianCalendar.GetDatePart(time.Ticks, 0);
			int num2 = JulianCalendar.GetDatePart(time.Ticks, 2);
			int num3 = JulianCalendar.GetDatePart(time.Ticks, 3);
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
			int[] array = (num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? JulianCalendar.s_daysToMonth366 : JulianCalendar.s_daysToMonth365;
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long ticks = JulianCalendar.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		// Token: 0x060021DE RID: 8670 RVA: 0x0012BB85 File Offset: 0x0012AD85
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x060021DF RID: 8671 RVA: 0x00130781 File Offset: 0x0012F981
		public override int GetDayOfMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x060021E0 RID: 8672 RVA: 0x0012BB9B File Offset: 0x0012AD9B
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x060021E1 RID: 8673 RVA: 0x00130790 File Offset: 0x0012F990
		public override int GetDayOfYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x060021E2 RID: 8674 RVA: 0x001307A0 File Offset: 0x0012F9A0
		public override int GetDaysInMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			int[] array = (year % 4 == 0) ? JulianCalendar.s_daysToMonth366 : JulianCalendar.s_daysToMonth365;
			return array[month] - array[month - 1];
		}

		// Token: 0x060021E3 RID: 8675 RVA: 0x001307D6 File Offset: 0x0012F9D6
		public override int GetDaysInYear(int year, int era)
		{
			if (!this.IsLeapYear(year, era))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x060021E4 RID: 8676 RVA: 0x001307ED File Offset: 0x0012F9ED
		public override int GetEra(DateTime time)
		{
			return JulianCalendar.JulianEra;
		}

		// Token: 0x060021E5 RID: 8677 RVA: 0x001307F4 File Offset: 0x0012F9F4
		public override int GetMonth(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x060021E6 RID: 8678 RVA: 0x00130803 File Offset: 0x0012FA03
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					JulianCalendar.JulianEra
				};
			}
		}

		// Token: 0x060021E7 RID: 8679 RVA: 0x00130813 File Offset: 0x0012FA13
		public override int GetMonthsInYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 12;
		}

		// Token: 0x060021E8 RID: 8680 RVA: 0x0013081F File Offset: 0x0012FA1F
		public override int GetYear(DateTime time)
		{
			return JulianCalendar.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x060021E9 RID: 8681 RVA: 0x0013082E File Offset: 0x0012FA2E
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			JulianCalendar.CheckMonthRange(month);
			if (this.IsLeapYear(year, era))
			{
				JulianCalendar.CheckDayRange(year, month, day);
				return month == 2 && day == 29;
			}
			JulianCalendar.CheckDayRange(year, month, day);
			return false;
		}

		// Token: 0x060021EA RID: 8682 RVA: 0x0013085E File Offset: 0x0012FA5E
		public override int GetLeapMonth(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return 0;
		}

		// Token: 0x060021EB RID: 8683 RVA: 0x00130869 File Offset: 0x0012FA69
		public override bool IsLeapMonth(int year, int month, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			return false;
		}

		// Token: 0x060021EC RID: 8684 RVA: 0x0013087A File Offset: 0x0012FA7A
		public override bool IsLeapYear(int year, int era)
		{
			this.CheckYearEraRange(year, era);
			return year % 4 == 0;
		}

		// Token: 0x060021ED RID: 8685 RVA: 0x0013088C File Offset: 0x0012FA8C
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			this.CheckYearEraRange(year, era);
			JulianCalendar.CheckMonthRange(month);
			JulianCalendar.CheckDayRange(year, month, day);
			if (millisecond < 0 || millisecond >= 1000)
			{
				throw new ArgumentOutOfRangeException("millisecond", millisecond, SR.Format(SR.ArgumentOutOfRange_Range, 0, 999));
			}
			if (hour < 0 || hour >= 24 || minute < 0 || minute >= 60 || second < 0 || second >= 60)
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadHourMinuteSecond);
			}
			return new DateTime(JulianCalendar.DateToTicks(year, month, day) + new TimeSpan(0, hour, minute, second, millisecond).Ticks);
		}

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x060021EE RID: 8686 RVA: 0x0011D245 File Offset: 0x0011C445
		// (set) Token: 0x060021EF RID: 8687 RVA: 0x0013093C File Offset: 0x0012FB3C
		public override int TwoDigitYearMax
		{
			get
			{
				return this._twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this.MaxYear)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 99, this.MaxYear));
				}
				this._twoDigitYearMax = value;
			}
		}

		// Token: 0x060021F0 RID: 8688 RVA: 0x00130994 File Offset: 0x0012FB94
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (year > this.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Bounds_Lower_Upper, 1, this.MaxYear));
			}
			return base.ToFourDigitYear(year);
		}

		// Token: 0x04000885 RID: 2181
		public static readonly int JulianEra = 1;

		// Token: 0x04000886 RID: 2182
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
			334,
			365
		};

		// Token: 0x04000887 RID: 2183
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
			335,
			366
		};

		// Token: 0x04000888 RID: 2184
		internal int MaxYear = 9999;
	}
}
