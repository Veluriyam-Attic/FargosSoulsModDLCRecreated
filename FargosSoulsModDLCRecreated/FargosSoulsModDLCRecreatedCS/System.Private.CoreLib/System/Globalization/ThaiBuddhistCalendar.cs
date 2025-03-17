using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x0200022C RID: 556
	[NullableContext(1)]
	[Nullable(0)]
	public class ThaiBuddhistCalendar : Calendar
	{
		// Token: 0x170007E8 RID: 2024
		// (get) Token: 0x06002343 RID: 9027 RVA: 0x0011CCDB File Offset: 0x0011BEDB
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x170007E9 RID: 2025
		// (get) Token: 0x06002344 RID: 9028 RVA: 0x0011CCE2 File Offset: 0x0011BEE2
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x170007EA RID: 2026
		// (get) Token: 0x06002345 RID: 9029 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x06002346 RID: 9030 RVA: 0x001352DD File Offset: 0x001344DD
		public ThaiBuddhistCalendar()
		{
			this._helper = new GregorianCalendarHelper(this, ThaiBuddhistCalendar.s_thaiBuddhistEraInfo);
		}

		// Token: 0x170007EB RID: 2027
		// (get) Token: 0x06002347 RID: 9031 RVA: 0x000DA80B File Offset: 0x000D9A0B
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.THAI;
			}
		}

		// Token: 0x06002348 RID: 9032 RVA: 0x001352F6 File Offset: 0x001344F6
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this._helper.AddMonths(time, months);
		}

		// Token: 0x06002349 RID: 9033 RVA: 0x00135305 File Offset: 0x00134505
		public override DateTime AddYears(DateTime time, int years)
		{
			return this._helper.AddYears(time, years);
		}

		// Token: 0x0600234A RID: 9034 RVA: 0x00135314 File Offset: 0x00134514
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this._helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x0600234B RID: 9035 RVA: 0x00135324 File Offset: 0x00134524
		public override int GetDaysInYear(int year, int era)
		{
			return this._helper.GetDaysInYear(year, era);
		}

		// Token: 0x0600234C RID: 9036 RVA: 0x00135333 File Offset: 0x00134533
		public override int GetDayOfMonth(DateTime time)
		{
			return this._helper.GetDayOfMonth(time);
		}

		// Token: 0x0600234D RID: 9037 RVA: 0x00135341 File Offset: 0x00134541
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this._helper.GetDayOfWeek(time);
		}

		// Token: 0x0600234E RID: 9038 RVA: 0x0013534F File Offset: 0x0013454F
		public override int GetDayOfYear(DateTime time)
		{
			return this._helper.GetDayOfYear(time);
		}

		// Token: 0x0600234F RID: 9039 RVA: 0x0013535D File Offset: 0x0013455D
		public override int GetMonthsInYear(int year, int era)
		{
			return this._helper.GetMonthsInYear(year, era);
		}

		// Token: 0x06002350 RID: 9040 RVA: 0x0013536C File Offset: 0x0013456C
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this._helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06002351 RID: 9041 RVA: 0x0013537C File Offset: 0x0013457C
		public override int GetEra(DateTime time)
		{
			return this._helper.GetEra(time);
		}

		// Token: 0x06002352 RID: 9042 RVA: 0x0013538A File Offset: 0x0013458A
		public override int GetMonth(DateTime time)
		{
			return this._helper.GetMonth(time);
		}

		// Token: 0x06002353 RID: 9043 RVA: 0x00135398 File Offset: 0x00134598
		public override int GetYear(DateTime time)
		{
			return this._helper.GetYear(time);
		}

		// Token: 0x06002354 RID: 9044 RVA: 0x001353A6 File Offset: 0x001345A6
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this._helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x001353B8 File Offset: 0x001345B8
		public override bool IsLeapYear(int year, int era)
		{
			return this._helper.IsLeapYear(year, era);
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x001353C7 File Offset: 0x001345C7
		public override int GetLeapMonth(int year, int era)
		{
			return this._helper.GetLeapMonth(year, era);
		}

		// Token: 0x06002357 RID: 9047 RVA: 0x001353D6 File Offset: 0x001345D6
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this._helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x001353E8 File Offset: 0x001345E8
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this._helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x170007EC RID: 2028
		// (get) Token: 0x06002359 RID: 9049 RVA: 0x0013540D File Offset: 0x0013460D
		public override int[] Eras
		{
			get
			{
				return this._helper.Eras;
			}
		}

		// Token: 0x170007ED RID: 2029
		// (get) Token: 0x0600235A RID: 9050 RVA: 0x0013541A File Offset: 0x0013461A
		// (set) Token: 0x0600235B RID: 9051 RVA: 0x00135444 File Offset: 0x00134644
		public override int TwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == -1)
				{
					this._twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 2572);
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

		// Token: 0x0600235C RID: 9052 RVA: 0x001354A3 File Offset: 0x001346A3
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this._helper.ToFourDigitYear(year, this.TwoDigitYearMax);
		}

		// Token: 0x040008F2 RID: 2290
		private static readonly EraInfo[] s_thaiBuddhistEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1, 1, 1, -543, 544, 10542)
		};

		// Token: 0x040008F3 RID: 2291
		public const int ThaiBuddhistEra = 1;

		// Token: 0x040008F4 RID: 2292
		private readonly GregorianCalendarHelper _helper;
	}
}
