using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000225 RID: 549
	[NullableContext(1)]
	[Nullable(0)]
	public class TaiwanCalendar : Calendar
	{
		// Token: 0x060022DC RID: 8924 RVA: 0x00133FD5 File Offset: 0x001331D5
		internal static Calendar GetDefaultInstance()
		{
			Calendar result;
			if ((result = TaiwanCalendar.s_defaultInstance) == null)
			{
				result = (TaiwanCalendar.s_defaultInstance = new TaiwanCalendar());
			}
			return result;
		}

		// Token: 0x170007CA RID: 1994
		// (get) Token: 0x060022DD RID: 8925 RVA: 0x00133FEF File Offset: 0x001331EF
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return TaiwanCalendar.s_calendarMinValue;
			}
		}

		// Token: 0x170007CB RID: 1995
		// (get) Token: 0x060022DE RID: 8926 RVA: 0x0011CCE2 File Offset: 0x0011BEE2
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170007CC RID: 1996
		// (get) Token: 0x060022DF RID: 8927 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x00133FF8 File Offset: 0x001331F8
		public TaiwanCalendar()
		{
			try
			{
				new CultureInfo("zh-TW");
			}
			catch (ArgumentException innerException)
			{
				throw new TypeInitializationException(base.GetType().ToString(), innerException);
			}
			this._helper = new GregorianCalendarHelper(this, TaiwanCalendar.s_taiwanEraInfo);
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x060022E1 RID: 8929 RVA: 0x000CA38E File Offset: 0x000C958E
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.TAIWAN;
			}
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x0013404C File Offset: 0x0013324C
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this._helper.AddMonths(time, months);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x0013405B File Offset: 0x0013325B
		public override DateTime AddYears(DateTime time, int years)
		{
			return this._helper.AddYears(time, years);
		}

		// Token: 0x060022E4 RID: 8932 RVA: 0x0013406A File Offset: 0x0013326A
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this._helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x060022E5 RID: 8933 RVA: 0x0013407A File Offset: 0x0013327A
		public override int GetDaysInYear(int year, int era)
		{
			return this._helper.GetDaysInYear(year, era);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x00134089 File Offset: 0x00133289
		public override int GetDayOfMonth(DateTime time)
		{
			return this._helper.GetDayOfMonth(time);
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x00134097 File Offset: 0x00133297
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this._helper.GetDayOfWeek(time);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x001340A5 File Offset: 0x001332A5
		public override int GetDayOfYear(DateTime time)
		{
			return this._helper.GetDayOfYear(time);
		}

		// Token: 0x060022E9 RID: 8937 RVA: 0x001340B3 File Offset: 0x001332B3
		public override int GetMonthsInYear(int year, int era)
		{
			return this._helper.GetMonthsInYear(year, era);
		}

		// Token: 0x060022EA RID: 8938 RVA: 0x001340C2 File Offset: 0x001332C2
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this._helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x060022EB RID: 8939 RVA: 0x001340D2 File Offset: 0x001332D2
		public override int GetEra(DateTime time)
		{
			return this._helper.GetEra(time);
		}

		// Token: 0x060022EC RID: 8940 RVA: 0x001340E0 File Offset: 0x001332E0
		public override int GetMonth(DateTime time)
		{
			return this._helper.GetMonth(time);
		}

		// Token: 0x060022ED RID: 8941 RVA: 0x001340EE File Offset: 0x001332EE
		public override int GetYear(DateTime time)
		{
			return this._helper.GetYear(time);
		}

		// Token: 0x060022EE RID: 8942 RVA: 0x001340FC File Offset: 0x001332FC
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this._helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x060022EF RID: 8943 RVA: 0x0013410E File Offset: 0x0013330E
		public override bool IsLeapYear(int year, int era)
		{
			return this._helper.IsLeapYear(year, era);
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x0013411D File Offset: 0x0013331D
		public override int GetLeapMonth(int year, int era)
		{
			return this._helper.GetLeapMonth(year, era);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x0013412C File Offset: 0x0013332C
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this._helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x0013413C File Offset: 0x0013333C
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this._helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x060022F3 RID: 8947 RVA: 0x00134161 File Offset: 0x00133361
		public override int[] Eras
		{
			get
			{
				return this._helper.Eras;
			}
		}

		// Token: 0x170007CF RID: 1999
		// (get) Token: 0x060022F4 RID: 8948 RVA: 0x0012FDE7 File Offset: 0x0012EFE7
		// (set) Token: 0x060022F5 RID: 8949 RVA: 0x00134170 File Offset: 0x00133370
		public override int TwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == -1)
				{
					this._twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 99);
				}
				return this._twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > this._helper.MaxYear)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 99, this._helper.MaxYear));
				}
				this._twoDigitYearMax = value;
			}
		}

		// Token: 0x060022F6 RID: 8950 RVA: 0x001341D0 File Offset: 0x001333D0
		public override int ToFourDigitYear(int year)
		{
			if (year <= 0)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.ArgumentOutOfRange_NeedPosNum);
			}
			if (year > this._helper.MaxYear)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, this._helper.MaxYear));
			}
			return year;
		}

		// Token: 0x040008D7 RID: 2263
		private static readonly EraInfo[] s_taiwanEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1912, 1, 1, 1911, 1, 8088)
		};

		// Token: 0x040008D8 RID: 2264
		private static volatile Calendar s_defaultInstance;

		// Token: 0x040008D9 RID: 2265
		private readonly GregorianCalendarHelper _helper;

		// Token: 0x040008DA RID: 2266
		private static readonly DateTime s_calendarMinValue = new DateTime(1912, 1, 1);
	}
}
