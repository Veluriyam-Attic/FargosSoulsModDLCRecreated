using System;
using System.Runtime.CompilerServices;
using Internal.Win32;

namespace System.Globalization
{
	// Token: 0x0200020F RID: 527
	[NullableContext(1)]
	[Nullable(0)]
	public class HijriCalendar : Calendar
	{
		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x0600213A RID: 8506 RVA: 0x0012DB07 File Offset: 0x0012CD07
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return HijriCalendar.s_calendarMinValue;
			}
		}

		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x0600213B RID: 8507 RVA: 0x0012DB0E File Offset: 0x0012CD0E
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return HijriCalendar.s_calendarMaxValue;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x0600213C RID: 8508 RVA: 0x000CE630 File Offset: 0x000CD830
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunarCalendar;
			}
		}

		// Token: 0x1700075E RID: 1886
		// (get) Token: 0x0600213E RID: 8510 RVA: 0x000C9FD4 File Offset: 0x000C91D4
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.HIJRI;
			}
		}

		// Token: 0x1700075F RID: 1887
		// (get) Token: 0x0600213F RID: 8511 RVA: 0x0012DB28 File Offset: 0x0012CD28
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 354;
			}
		}

		// Token: 0x06002140 RID: 8512 RVA: 0x0012DB2F File Offset: 0x0012CD2F
		private long GetAbsoluteDateHijri(int y, int m, int d)
		{
			return this.DaysUpToHijriYear(y) + (long)HijriCalendar.s_hijriMonthDays[m - 1] + (long)d - 1L - (long)this.HijriAdjustment;
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x0012DB54 File Offset: 0x0012CD54
		private long DaysUpToHijriYear(int HijriYear)
		{
			int num = (HijriYear - 1) / 30 * 30;
			int i = HijriYear - num - 1;
			long num2 = (long)num * 10631L / 30L + 227013L;
			while (i > 0)
			{
				num2 += (long)(354 + (this.IsLeapYear(i, 0) ? 1 : 0));
				i--;
			}
			return num2;
		}

		// Token: 0x17000760 RID: 1888
		// (get) Token: 0x06002142 RID: 8514 RVA: 0x0012DBA9 File Offset: 0x0012CDA9
		// (set) Token: 0x06002143 RID: 8515 RVA: 0x0012DBCA File Offset: 0x0012CDCA
		public int HijriAdjustment
		{
			get
			{
				if (this._hijriAdvance == -2147483648)
				{
					this._hijriAdvance = this.GetHijriDateAdjustment();
				}
				return this._hijriAdvance;
			}
			set
			{
				if (value < -2 || value > 2)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Bounds_Lower_Upper, -2, 2));
				}
				base.VerifyWritable();
				this._hijriAdvance = value;
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x0012DC0C File Offset: 0x0012CE0C
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < HijriCalendar.s_calendarMinValue.Ticks || ticks > HijriCalendar.s_calendarMaxValue.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", ticks, SR.Format(CultureInfo.InvariantCulture, SR.ArgumentOutOfRange_CalendarRange, HijriCalendar.s_calendarMinValue, HijriCalendar.s_calendarMaxValue));
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x0012DC67 File Offset: 0x0012CE67
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != HijriCalendar.HijriEra)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x0012DC8A File Offset: 0x0012CE8A
		internal static void CheckYearRange(int year, int era)
		{
			HijriCalendar.CheckEraRange(era);
			if (year < 1 || year > 9666)
			{
				throw new ArgumentOutOfRangeException("year", SR.Format(SR.ArgumentOutOfRange_Range, 1, 9666));
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x0012DCC4 File Offset: 0x0012CEC4
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			if (year == 9666 && month > 4)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.Format(SR.ArgumentOutOfRange_Range, 1, 4));
			}
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.ArgumentOutOfRange_Month);
			}
		}

		// Token: 0x06002148 RID: 8520 RVA: 0x0012DD2C File Offset: 0x0012CF2C
		internal virtual int GetDatePart(long ticks, int part)
		{
			HijriCalendar.CheckTicksRange(ticks);
			long num = ticks / 864000000000L + 1L;
			num += (long)this.HijriAdjustment;
			int num2 = (int)((num - 227013L) * 30L / 10631L) + 1;
			long num3 = this.DaysUpToHijriYear(num2);
			long num4 = (long)this.GetDaysInYear(num2, 0);
			if (num < num3)
			{
				num3 -= num4;
				num2--;
			}
			else if (num == num3)
			{
				num2--;
				num3 -= (long)this.GetDaysInYear(num2, 0);
			}
			else if (num > num3 + num4)
			{
				num3 += num4;
				num2++;
			}
			if (part == 0)
			{
				return num2;
			}
			int num5 = 1;
			num -= num3;
			if (part == 1)
			{
				return (int)num;
			}
			while (num5 <= 12 && num > (long)HijriCalendar.s_hijriMonthDays[num5 - 1])
			{
				num5++;
			}
			num5--;
			if (part == 2)
			{
				return num5;
			}
			int result = (int)(num - (long)HijriCalendar.s_hijriMonthDays[num5 - 1]);
			if (part == 3)
			{
				return result;
			}
			throw new InvalidOperationException(SR.InvalidOperation_DateTimeParsing);
		}

		// Token: 0x06002149 RID: 8521 RVA: 0x0012DE10 File Offset: 0x0012D010
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
			long ticks = this.GetAbsoluteDateHijri(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L;
			Calendar.CheckAddResult(ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return new DateTime(ticks);
		}

		// Token: 0x0600214A RID: 8522 RVA: 0x0012BB85 File Offset: 0x0012AD85
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x0600214B RID: 8523 RVA: 0x0012DF0A File Offset: 0x0012D10A
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 3);
		}

		// Token: 0x0600214C RID: 8524 RVA: 0x0012BB9B File Offset: 0x0012AD9B
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x0600214D RID: 8525 RVA: 0x0012DF1A File Offset: 0x0012D11A
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 1);
		}

		// Token: 0x0600214E RID: 8526 RVA: 0x0012DF2A File Offset: 0x0012D12A
		public override int GetDaysInMonth(int year, int month, int era)
		{
			HijriCalendar.CheckYearMonthRange(year, month, era);
			if (month == 12)
			{
				if (!this.IsLeapYear(year, 0))
				{
					return 29;
				}
				return 30;
			}
			else
			{
				if (month % 2 != 1)
				{
					return 29;
				}
				return 30;
			}
		}

		// Token: 0x0600214F RID: 8527 RVA: 0x0012DF54 File Offset: 0x0012D154
		public override int GetDaysInYear(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			if (!this.IsLeapYear(year, 0))
			{
				return 354;
			}
			return 355;
		}

		// Token: 0x06002150 RID: 8528 RVA: 0x0012DF72 File Offset: 0x0012D172
		public override int GetEra(DateTime time)
		{
			HijriCalendar.CheckTicksRange(time.Ticks);
			return HijriCalendar.HijriEra;
		}

		// Token: 0x17000761 RID: 1889
		// (get) Token: 0x06002151 RID: 8529 RVA: 0x0012DF85 File Offset: 0x0012D185
		public override int[] Eras
		{
			get
			{
				return new int[]
				{
					HijriCalendar.HijriEra
				};
			}
		}

		// Token: 0x06002152 RID: 8530 RVA: 0x0012DF95 File Offset: 0x0012D195
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 2);
		}

		// Token: 0x06002153 RID: 8531 RVA: 0x0012DFA5 File Offset: 0x0012D1A5
		public override int GetMonthsInYear(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			return 12;
		}

		// Token: 0x06002154 RID: 8532 RVA: 0x0012DFB0 File Offset: 0x0012D1B0
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time.Ticks, 0);
		}

		// Token: 0x06002155 RID: 8533 RVA: 0x0012DFC0 File Offset: 0x0012D1C0
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Day, daysInMonth, month));
			}
			return this.IsLeapYear(year, era) && month == 12 && day == 30;
		}

		// Token: 0x06002156 RID: 8534 RVA: 0x0012E01E File Offset: 0x0012D21E
		public override int GetLeapMonth(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			return 0;
		}

		// Token: 0x06002157 RID: 8535 RVA: 0x0012E028 File Offset: 0x0012D228
		public override bool IsLeapMonth(int year, int month, int era)
		{
			HijriCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		// Token: 0x06002158 RID: 8536 RVA: 0x0012E033 File Offset: 0x0012D233
		public override bool IsLeapYear(int year, int era)
		{
			HijriCalendar.CheckYearRange(year, era);
			return (year * 11 + 14) % 30 < 11;
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x0012E04C File Offset: 0x0012D24C
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Day, daysInMonth, month));
			}
			long absoluteDateHijri = this.GetAbsoluteDateHijri(year, month, day);
			if (absoluteDateHijri < 0L)
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadYearMonthDay);
			}
			return new DateTime(absoluteDateHijri * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
		}

		// Token: 0x17000762 RID: 1890
		// (get) Token: 0x0600215A RID: 8538 RVA: 0x0012E0CC File Offset: 0x0012D2CC
		// (set) Token: 0x0600215B RID: 8539 RVA: 0x0012E0F4 File Offset: 0x0012D2F4
		public override int TwoDigitYearMax
		{
			get
			{
				if (this._twoDigitYearMax == -1)
				{
					this._twoDigitYearMax = Calendar.GetSystemTwoDigitYearSetting(this.ID, 1451);
				}
				return this._twoDigitYearMax;
			}
			set
			{
				base.VerifyWritable();
				if (value < 99 || value > 9666)
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 99, 9666));
				}
				this._twoDigitYearMax = value;
			}
		}

		// Token: 0x0600215C RID: 8540 RVA: 0x0012E148 File Offset: 0x0012D348
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
			if (year > 9666)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1, 9666));
			}
			return year;
		}

		// Token: 0x0600215D RID: 8541 RVA: 0x0012E1B0 File Offset: 0x0012D3B0
		private int GetHijriDateAdjustment()
		{
			if (this._hijriAdvance == -2147483648)
			{
				this._hijriAdvance = HijriCalendar.GetAdvanceHijriDate();
			}
			return this._hijriAdvance;
		}

		// Token: 0x0600215E RID: 8542 RVA: 0x0012E1D0 File Offset: 0x0012D3D0
		private static int GetAdvanceHijriDate()
		{
			int result;
			using (RegistryKey registryKey = Registry.CurrentUser.OpenSubKey("Control Panel\\International"))
			{
				if (registryKey == null)
				{
					result = 0;
				}
				else
				{
					object value = registryKey.GetValue("AddHijriDate");
					if (value == null)
					{
						result = 0;
					}
					else
					{
						int num = 0;
						string text = value.ToString();
						if (string.Compare(text, 0, "AddHijriDate", 0, "AddHijriDate".Length, StringComparison.OrdinalIgnoreCase) == 0)
						{
							if (text.Length == "AddHijriDate".Length)
							{
								num = -1;
							}
							else
							{
								try
								{
									int num2 = int.Parse(text.AsSpan("AddHijriDate".Length), NumberStyles.Integer, CultureInfo.InvariantCulture);
									if (num2 >= -2 && num2 <= 2)
									{
										num = num2;
									}
								}
								catch (ArgumentException)
								{
								}
								catch (FormatException)
								{
								}
								catch (OverflowException)
								{
								}
							}
						}
						result = num;
					}
				}
			}
			return result;
		}

		// Token: 0x04000865 RID: 2149
		public static readonly int HijriEra = 1;

		// Token: 0x04000866 RID: 2150
		private static readonly int[] s_hijriMonthDays = new int[]
		{
			0,
			30,
			59,
			89,
			118,
			148,
			177,
			207,
			236,
			266,
			295,
			325,
			355
		};

		// Token: 0x04000867 RID: 2151
		private int _hijriAdvance = int.MinValue;

		// Token: 0x04000868 RID: 2152
		private static readonly DateTime s_calendarMinValue = new DateTime(622, 7, 18);

		// Token: 0x04000869 RID: 2153
		private static readonly DateTime s_calendarMaxValue = DateTime.MaxValue;
	}
}
