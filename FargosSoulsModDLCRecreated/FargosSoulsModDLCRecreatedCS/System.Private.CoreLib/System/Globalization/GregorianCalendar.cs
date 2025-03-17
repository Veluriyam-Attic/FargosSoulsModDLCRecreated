using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000203 RID: 515
	[NullableContext(1)]
	[Nullable(0)]
	public class GregorianCalendar : Calendar
	{
		// Token: 0x1700074A RID: 1866
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0011CCDB File Offset: 0x0011BEDB
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x060020C8 RID: 8392 RVA: 0x0011CCE2 File Offset: 0x0011BEE2
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x060020CA RID: 8394 RVA: 0x0012B93F File Offset: 0x0012AB3F
		internal static Calendar GetDefaultInstance()
		{
			Calendar result;
			if ((result = GregorianCalendar.s_defaultInstance) == null)
			{
				result = (GregorianCalendar.s_defaultInstance = new GregorianCalendar());
			}
			return result;
		}

		// Token: 0x060020CB RID: 8395 RVA: 0x0012B959 File Offset: 0x0012AB59
		public GregorianCalendar() : this(GregorianCalendarTypes.Localized)
		{
		}

		// Token: 0x060020CC RID: 8396 RVA: 0x0012B962 File Offset: 0x0012AB62
		public GregorianCalendar(GregorianCalendarTypes type)
		{
			if (type < GregorianCalendarTypes.Localized || type > GregorianCalendarTypes.TransliteratedFrench)
			{
				throw new ArgumentOutOfRangeException("type", type, SR.Format(SR.ArgumentOutOfRange_Range, GregorianCalendarTypes.Localized, GregorianCalendarTypes.TransliteratedFrench));
			}
			this._type = type;
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x060020CD RID: 8397 RVA: 0x0012B9A2 File Offset: 0x0012ABA2
		// (set) Token: 0x060020CE RID: 8398 RVA: 0x0012B9AA File Offset: 0x0012ABAA
		public virtual GregorianCalendarTypes CalendarType
		{
			get
			{
				return this._type;
			}
			set
			{
				base.VerifyWritable();
				if (value < GregorianCalendarTypes.Localized || value > GregorianCalendarTypes.TransliteratedFrench)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, GregorianCalendarTypes.Localized, GregorianCalendarTypes.TransliteratedFrench));
				}
				this._type = value;
			}
		}

		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x0012B9EA File Offset: 0x0012ABEA
		internal override CalendarId ID
		{
			get
			{
				return (CalendarId)this._type;
			}
		}

		// Token: 0x060020D0 RID: 8400 RVA: 0x0012B9F4 File Offset: 0x0012ABF4
		internal static long GetAbsoluteDate(int year, int month, int day)
		{
			if (year >= 1 && year <= 9999 && month >= 1 && month <= 12)
			{
				int[] array = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365;
				if (day >= 1 && day <= array[month] - array[month - 1])
				{
					int num = year - 1;
					return (long)(num * 365 + num / 4 - num / 100 + num / 400 + array[month - 1] + day - 1);
				}
			}
			throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadYearMonthDay);
		}

		// Token: 0x060020D1 RID: 8401 RVA: 0x0012BA7A File Offset: 0x0012AC7A
		internal virtual long DateToTicks(int year, int month, int day)
		{
			return GregorianCalendar.GetAbsoluteDate(year, month, day) * 864000000000L;
		}

		// Token: 0x060020D2 RID: 8402 RVA: 0x0012BA90 File Offset: 0x0012AC90
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", months, SR.Format(SR.ArgumentOutOfRange_Range, -120000, 120000));
			}
			int num;
			int num2;
			int num3;
			time.GetDate(out num, out num2, out num3);
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
			int[] array = (num % 4 == 0 && (num % 100 != 0 || num % 400 == 0)) ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365;
			int num5 = array[num2] - array[num2 - 1];
			if (num3 > num5)
			{
				num3 = num5;
			}
			long ticks = this.DateToTicks(num, num2, num3) + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		// Token: 0x060020D3 RID: 8403 RVA: 0x0012BB85 File Offset: 0x0012AD85
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x060020D4 RID: 8404 RVA: 0x0012BB92 File Offset: 0x0012AD92
		public override int GetDayOfMonth(DateTime time)
		{
			return time.Day;
		}

		// Token: 0x060020D5 RID: 8405 RVA: 0x0012BB9B File Offset: 0x0012AD9B
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x060020D6 RID: 8406 RVA: 0x0012BBB4 File Offset: 0x0012ADB4
		public override int GetDayOfYear(DateTime time)
		{
			return time.DayOfYear;
		}

		// Token: 0x060020D7 RID: 8407 RVA: 0x0012BBC0 File Offset: 0x0012ADC0
		public override int GetDaysInMonth(int year, int month, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9999));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.ArgumentOutOfRange_Month);
			}
			int[] array = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365;
			return array[month] - array[month - 1];
		}

		// Token: 0x060020D8 RID: 8408 RVA: 0x0012BC6C File Offset: 0x0012AE6C
		public override int GetDaysInYear(int year, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9999));
			}
			if (year % 4 != 0 || (year % 100 == 0 && year % 400 != 0))
			{
				return 365;
			}
			return 366;
		}

		// Token: 0x060020D9 RID: 8409 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override int GetEra(DateTime time)
		{
			return 1;
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x060020DA RID: 8410 RVA: 0x0011FFFA File Offset: 0x0011F1FA
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					1
				};
			}
		}

		// Token: 0x060020DB RID: 8411 RVA: 0x0012BCEC File Offset: 0x0012AEEC
		public override int GetMonth(DateTime time)
		{
			return time.Month;
		}

		// Token: 0x060020DC RID: 8412 RVA: 0x0012BCF8 File Offset: 0x0012AEF8
		public override int GetMonthsInYear(int year, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9999));
			}
			return 12;
		}

		// Token: 0x060020DD RID: 8413 RVA: 0x0012BD5B File Offset: 0x0012AF5B
		public override int GetYear(DateTime time)
		{
			return time.Year;
		}

		// Token: 0x060020DE RID: 8414 RVA: 0x0012BD64 File Offset: 0x0012AF64
		internal override bool IsValidYear(int year, int era)
		{
			return year >= 1 && year <= 9999;
		}

		// Token: 0x060020DF RID: 8415 RVA: 0x0012BD78 File Offset: 0x0012AF78
		internal override bool IsValidDay(int year, int month, int day, int era)
		{
			if ((era != 0 && era != 1) || year < 1 || year > 9999 || month < 1 || month > 12 || day < 1)
			{
				return false;
			}
			int[] array = (year % 4 == 0 && (year % 100 != 0 || year % 400 == 0)) ? GregorianCalendar.DaysToMonth366 : GregorianCalendar.DaysToMonth365;
			return day <= array[month] - array[month - 1];
		}

		// Token: 0x060020E0 RID: 8416 RVA: 0x0012BDDC File Offset: 0x0012AFDC
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.Format(SR.ArgumentOutOfRange_Range, 1, 12));
			}
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9999));
			}
			if (day < 1 || day > this.GetDaysInMonth(year, month))
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Range, 1, this.GetDaysInMonth(year, month)));
			}
			return this.IsLeapYear(year) && month == 2 && day == 29;
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0012BEC4 File Offset: 0x0012B0C4
		public override int GetLeapMonth(int year, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9999));
			}
			return 0;
		}

		// Token: 0x060020E2 RID: 8418 RVA: 0x0012BF28 File Offset: 0x0012B128
		public override bool IsLeapMonth(int year, int month, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9999));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.Format(SR.ArgumentOutOfRange_Range, 1, 12));
			}
			return false;
		}

		// Token: 0x060020E3 RID: 8419 RVA: 0x0012BFBC File Offset: 0x0012B1BC
		public override bool IsLeapYear(int year, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
			if (year < 1 || year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9999));
			}
			return year % 4 == 0 && (year % 100 != 0 || year % 400 == 0);
		}

		// Token: 0x060020E4 RID: 8420 RVA: 0x0012C036 File Offset: 0x0012B236
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
			return new DateTime(year, month, day, hour, minute, second, millisecond);
		}

		// Token: 0x060020E5 RID: 8421 RVA: 0x0012C068 File Offset: 0x0012B268
		internal override bool TryToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era, out DateTime result)
		{
			if (era != 0 && era != 1)
			{
				result = DateTime.MinValue;
				return false;
			}
			return DateTime.TryCreate(year, month, day, hour, minute, second, millisecond, out result);
		}

		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x060020E6 RID: 8422 RVA: 0x0012C093 File Offset: 0x0012B293
		// (set) Token: 0x060020E7 RID: 8423 RVA: 0x0012C0BC File Offset: 0x0012B2BC
		public override int TwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == -1)
				{
					this._twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 2029);
				}
				return this._twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9999)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 99, 9999));
				}
				this._twoDigitYearMax = value;
			}
		}

		// Token: 0x060020E8 RID: 8424 RVA: 0x0012C110 File Offset: 0x0012B310
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (year > 9999)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9999));
			}
			return base.ToFourDigitYear(year);
		}

		// Token: 0x04000818 RID: 2072
		public const int ADEra = 1;

		// Token: 0x04000819 RID: 2073
		private GregorianCalendarTypes _type;

		// Token: 0x0400081A RID: 2074
		private static readonly int[] DaysToMonth365 = new int[]
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

		// Token: 0x0400081B RID: 2075
		private static readonly int[] DaysToMonth366 = new int[]
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

		// Token: 0x0400081C RID: 2076
		private static volatile Calendar s_defaultInstance;
	}
}
