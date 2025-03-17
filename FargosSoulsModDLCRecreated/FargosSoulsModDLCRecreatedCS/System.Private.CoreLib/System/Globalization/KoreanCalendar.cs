using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000218 RID: 536
	[NullableContext(1)]
	[Nullable(0)]
	public class KoreanCalendar : Calendar
	{
		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x060021F2 RID: 8690 RVA: 0x0011CCDB File Offset: 0x0011BEDB
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return DateTime.MinValue;
			}
		}

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x060021F3 RID: 8691 RVA: 0x0011CCE2 File Offset: 0x0011BEE2
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000780 RID: 1920
		// (get) Token: 0x060021F4 RID: 8692 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x060021F5 RID: 8693 RVA: 0x00130A30 File Offset: 0x0012FC30
		public KoreanCalendar()
		{
			try
			{
				new CultureInfo("ko-KR");
			}
			catch (ArgumentException innerException)
			{
				throw new TypeInitializationException(base.GetType().ToString(), innerException);
			}
			this._helper = new GregorianCalendarHelper(this, KoreanCalendar.s_koreanEraInfo);
		}

		// Token: 0x17000781 RID: 1921
		// (get) Token: 0x060021F6 RID: 8694 RVA: 0x000E8AA0 File Offset: 0x000E7CA0
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.KOREA;
			}
		}

		// Token: 0x060021F7 RID: 8695 RVA: 0x00130A84 File Offset: 0x0012FC84
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this._helper.AddMonths(time, months);
		}

		// Token: 0x060021F8 RID: 8696 RVA: 0x00130A93 File Offset: 0x0012FC93
		public override DateTime AddYears(DateTime time, int years)
		{
			return this._helper.AddYears(time, years);
		}

		// Token: 0x060021F9 RID: 8697 RVA: 0x00130AA2 File Offset: 0x0012FCA2
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this._helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x060021FA RID: 8698 RVA: 0x00130AB2 File Offset: 0x0012FCB2
		public override int GetDaysInYear(int year, int era)
		{
			return this._helper.GetDaysInYear(year, era);
		}

		// Token: 0x060021FB RID: 8699 RVA: 0x00130AC1 File Offset: 0x0012FCC1
		public override int GetDayOfMonth(DateTime time)
		{
			return this._helper.GetDayOfMonth(time);
		}

		// Token: 0x060021FC RID: 8700 RVA: 0x00130ACF File Offset: 0x0012FCCF
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this._helper.GetDayOfWeek(time);
		}

		// Token: 0x060021FD RID: 8701 RVA: 0x00130ADD File Offset: 0x0012FCDD
		public override int GetDayOfYear(DateTime time)
		{
			return this._helper.GetDayOfYear(time);
		}

		// Token: 0x060021FE RID: 8702 RVA: 0x00130AEB File Offset: 0x0012FCEB
		public override int GetMonthsInYear(int year, int era)
		{
			return this._helper.GetMonthsInYear(year, era);
		}

		// Token: 0x060021FF RID: 8703 RVA: 0x00130AFA File Offset: 0x0012FCFA
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this._helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x06002200 RID: 8704 RVA: 0x00130B0A File Offset: 0x0012FD0A
		public override int GetEra(DateTime time)
		{
			return this._helper.GetEra(time);
		}

		// Token: 0x06002201 RID: 8705 RVA: 0x00130B18 File Offset: 0x0012FD18
		public override int GetMonth(DateTime time)
		{
			return this._helper.GetMonth(time);
		}

		// Token: 0x06002202 RID: 8706 RVA: 0x00130B26 File Offset: 0x0012FD26
		public override int GetYear(DateTime time)
		{
			return this._helper.GetYear(time);
		}

		// Token: 0x06002203 RID: 8707 RVA: 0x00130B34 File Offset: 0x0012FD34
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this._helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x06002204 RID: 8708 RVA: 0x00130B46 File Offset: 0x0012FD46
		public override bool IsLeapYear(int year, int era)
		{
			return this._helper.IsLeapYear(year, era);
		}

		// Token: 0x06002205 RID: 8709 RVA: 0x00130B55 File Offset: 0x0012FD55
		public override int GetLeapMonth(int year, int era)
		{
			return this._helper.GetLeapMonth(year, era);
		}

		// Token: 0x06002206 RID: 8710 RVA: 0x00130B64 File Offset: 0x0012FD64
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this._helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x06002207 RID: 8711 RVA: 0x00130B74 File Offset: 0x0012FD74
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this._helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x17000782 RID: 1922
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x00130B99 File Offset: 0x0012FD99
		public override int[] Eras
		{
			get
			{
				return this._helper.Eras;
			}
		}

		// Token: 0x17000783 RID: 1923
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x00130BA6 File Offset: 0x0012FDA6
		// (set) Token: 0x0600220A RID: 8714 RVA: 0x00130BD0 File Offset: 0x0012FDD0
		public override int TwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == -1)
				{
					this._twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 4362);
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

		// Token: 0x0600220B RID: 8715 RVA: 0x00130C2F File Offset: 0x0012FE2F
		public override int ToFourDigitYear(int year)
		{
			if (year < 0)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this._helper.ToFourDigitYear(year, this.TwoDigitYearMax);
		}

		// Token: 0x04000889 RID: 2185
		public const int KoreanEra = 1;

		// Token: 0x0400088A RID: 2186
		private static readonly EraInfo[] s_koreanEraInfo = new EraInfo[]
		{
			new EraInfo(1, 1, 1, 1, -2333, 2334, 12332)
		};

		// Token: 0x0400088B RID: 2187
		private readonly GregorianCalendarHelper _helper;
	}
}
