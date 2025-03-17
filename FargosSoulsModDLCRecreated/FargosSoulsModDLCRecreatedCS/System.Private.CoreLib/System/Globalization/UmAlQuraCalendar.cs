using System;
using System.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000239 RID: 569
	[NullableContext(1)]
	[Nullable(0)]
	public class UmAlQuraCalendar : Calendar
	{
		// Token: 0x060023AD RID: 9133 RVA: 0x00138168 File Offset: 0x00137368
		private static UmAlQuraCalendar.DateMapping[] InitDateMapping()
		{
			short[] array = new short[]
			{
				746,
				1900,
				4,
				30,
				1769,
				1901,
				4,
				19,
				3794,
				1902,
				4,
				9,
				3748,
				1903,
				3,
				30,
				3402,
				1904,
				3,
				18,
				2710,
				1905,
				3,
				7,
				1334,
				1906,
				2,
				24,
				2741,
				1907,
				2,
				13,
				3498,
				1908,
				2,
				3,
				2980,
				1909,
				1,
				23,
				2889,
				1910,
				1,
				12,
				2707,
				1911,
				1,
				1,
				1323,
				1911,
				12,
				21,
				2647,
				1912,
				12,
				9,
				1206,
				1913,
				11,
				29,
				2741,
				1914,
				11,
				18,
				1450,
				1915,
				11,
				8,
				3413,
				1916,
				10,
				27,
				3370,
				1917,
				10,
				17,
				2646,
				1918,
				10,
				6,
				1198,
				1919,
				9,
				25,
				2397,
				1920,
				9,
				13,
				748,
				1921,
				9,
				3,
				1749,
				1922,
				8,
				23,
				1706,
				1923,
				8,
				13,
				1365,
				1924,
				8,
				1,
				1195,
				1925,
				7,
				21,
				2395,
				1926,
				7,
				10,
				698,
				1927,
				6,
				30,
				1397,
				1928,
				6,
				18,
				2994,
				1929,
				6,
				8,
				1892,
				1930,
				5,
				29,
				1865,
				1931,
				5,
				18,
				1621,
				1932,
				5,
				6,
				683,
				1933,
				4,
				25,
				1371,
				1934,
				4,
				14,
				2778,
				1935,
				4,
				4,
				1748,
				1936,
				3,
				24,
				3785,
				1937,
				3,
				13,
				3474,
				1938,
				3,
				3,
				3365,
				1939,
				2,
				20,
				2637,
				1940,
				2,
				9,
				685,
				1941,
				1,
				28,
				1389,
				1942,
				1,
				17,
				2922,
				1943,
				1,
				7,
				2898,
				1943,
				12,
				28,
				2725,
				1944,
				12,
				16,
				2635,
				1945,
				12,
				5,
				1175,
				1946,
				11,
				24,
				2359,
				1947,
				11,
				13,
				694,
				1948,
				11,
				2,
				1397,
				1949,
				10,
				22,
				3434,
				1950,
				10,
				12,
				3410,
				1951,
				10,
				2,
				2710,
				1952,
				9,
				20,
				2349,
				1953,
				9,
				9,
				605,
				1954,
				8,
				29,
				1245,
				1955,
				8,
				18,
				2778,
				1956,
				8,
				7,
				1492,
				1957,
				7,
				28,
				3497,
				1958,
				7,
				17,
				3410,
				1959,
				7,
				7,
				2730,
				1960,
				6,
				25,
				1238,
				1961,
				6,
				14,
				2486,
				1962,
				6,
				3,
				884,
				1963,
				5,
				24,
				1897,
				1964,
				5,
				12,
				1874,
				1965,
				5,
				2,
				1701,
				1966,
				4,
				21,
				1355,
				1967,
				4,
				10,
				2731,
				1968,
				3,
				29,
				1370,
				1969,
				3,
				19,
				2773,
				1970,
				3,
				8,
				3538,
				1971,
				2,
				26,
				3492,
				1972,
				2,
				16,
				3401,
				1973,
				2,
				4,
				2709,
				1974,
				1,
				24,
				1325,
				1975,
				1,
				13,
				2653,
				1976,
				1,
				2,
				1370,
				1976,
				12,
				22,
				2773,
				1977,
				12,
				11,
				1706,
				1978,
				12,
				1,
				1685,
				1979,
				11,
				20,
				1323,
				1980,
				11,
				8,
				2647,
				1981,
				10,
				28,
				1198,
				1982,
				10,
				18,
				2422,
				1983,
				10,
				7,
				1388,
				1984,
				9,
				26,
				2901,
				1985,
				9,
				15,
				2730,
				1986,
				9,
				5,
				2645,
				1987,
				8,
				25,
				1197,
				1988,
				8,
				13,
				2397,
				1989,
				8,
				2,
				730,
				1990,
				7,
				23,
				1497,
				1991,
				7,
				12,
				3506,
				1992,
				7,
				1,
				2980,
				1993,
				6,
				21,
				2890,
				1994,
				6,
				10,
				2645,
				1995,
				5,
				30,
				693,
				1996,
				5,
				18,
				1397,
				1997,
				5,
				7,
				2922,
				1998,
				4,
				27,
				3026,
				1999,
				4,
				17,
				3012,
				2000,
				4,
				6,
				2953,
				2001,
				3,
				26,
				2709,
				2002,
				3,
				15,
				1325,
				2003,
				3,
				4,
				1453,
				2004,
				2,
				21,
				2922,
				2005,
				2,
				10,
				1748,
				2006,
				1,
				31,
				3529,
				2007,
				1,
				20,
				3474,
				2008,
				1,
				10,
				2726,
				2008,
				12,
				29,
				2390,
				2009,
				12,
				18,
				686,
				2010,
				12,
				7,
				1389,
				2011,
				11,
				26,
				874,
				2012,
				11,
				15,
				2901,
				2013,
				11,
				4,
				2730,
				2014,
				10,
				25,
				2381,
				2015,
				10,
				14,
				1181,
				2016,
				10,
				2,
				2397,
				2017,
				9,
				21,
				698,
				2018,
				9,
				11,
				1461,
				2019,
				8,
				31,
				1450,
				2020,
				8,
				20,
				3413,
				2021,
				8,
				9,
				2714,
				2022,
				7,
				30,
				2350,
				2023,
				7,
				19,
				622,
				2024,
				7,
				7,
				1373,
				2025,
				6,
				26,
				2778,
				2026,
				6,
				16,
				1748,
				2027,
				6,
				6,
				1701,
				2028,
				5,
				25,
				1355,
				2029,
				5,
				14,
				2711,
				2030,
				5,
				3,
				1358,
				2031,
				4,
				23,
				2734,
				2032,
				4,
				11,
				1452,
				2033,
				4,
				1,
				2985,
				2034,
				3,
				21,
				3474,
				2035,
				3,
				11,
				2853,
				2036,
				2,
				28,
				1611,
				2037,
				2,
				16,
				3243,
				2038,
				2,
				5,
				1370,
				2039,
				1,
				26,
				2901,
				2040,
				1,
				15,
				1746,
				2041,
				1,
				4,
				3749,
				2041,
				12,
				24,
				3658,
				2042,
				12,
				14,
				2709,
				2043,
				12,
				3,
				1325,
				2044,
				11,
				21,
				2733,
				2045,
				11,
				10,
				876,
				2046,
				10,
				31,
				1881,
				2047,
				10,
				20,
				1746,
				2048,
				10,
				9,
				1685,
				2049,
				9,
				28,
				1325,
				2050,
				9,
				17,
				2651,
				2051,
				9,
				6,
				1210,
				2052,
				8,
				26,
				2490,
				2053,
				8,
				15,
				948,
				2054,
				8,
				5,
				2921,
				2055,
				7,
				25,
				2898,
				2056,
				7,
				14,
				2726,
				2057,
				7,
				3,
				1206,
				2058,
				6,
				22,
				2413,
				2059,
				6,
				11,
				748,
				2060,
				5,
				31,
				1753,
				2061,
				5,
				20,
				3762,
				2062,
				5,
				10,
				3412,
				2063,
				4,
				30,
				3370,
				2064,
				4,
				18,
				2646,
				2065,
				4,
				7,
				1198,
				2066,
				3,
				27,
				2413,
				2067,
				3,
				16,
				3434,
				2068,
				3,
				5,
				2900,
				2069,
				2,
				23,
				2857,
				2070,
				2,
				12,
				2707,
				2071,
				2,
				1,
				1323,
				2072,
				1,
				21,
				2647,
				2073,
				1,
				9,
				1334,
				2073,
				12,
				30,
				2741,
				2074,
				12,
				19,
				1706,
				2075,
				12,
				9,
				3731,
				2076,
				11,
				27,
				0,
				2077,
				11,
				17
			};
			UmAlQuraCalendar.DateMapping[] array2 = new UmAlQuraCalendar.DateMapping[array.Length / 4];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = new UmAlQuraCalendar.DateMapping((int)array[i * 4], (int)array[i * 4 + 1], (int)array[i * 4 + 2], (int)array[i * 4 + 3]);
			}
			return array2;
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x060023AE RID: 9134 RVA: 0x001381CB File Offset: 0x001373CB
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return UmAlQuraCalendar.s_minDate;
			}
		}

		// Token: 0x170007F9 RID: 2041
		// (get) Token: 0x060023AF RID: 9135 RVA: 0x001381D2 File Offset: 0x001373D2
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return UmAlQuraCalendar.s_maxDate;
			}
		}

		// Token: 0x170007FA RID: 2042
		// (get) Token: 0x060023B0 RID: 9136 RVA: 0x000CE630 File Offset: 0x000CD830
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.LunarCalendar;
			}
		}

		// Token: 0x170007FB RID: 2043
		// (get) Token: 0x060023B2 RID: 9138 RVA: 0x000C9FD4 File Offset: 0x000C91D4
		internal override CalendarId BaseCalendarID
		{
			get
			{
				return CalendarId.HIJRI;
			}
		}

		// Token: 0x170007FC RID: 2044
		// (get) Token: 0x060023B3 RID: 9139 RVA: 0x001381D9 File Offset: 0x001373D9
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.UMALQURA;
			}
		}

		// Token: 0x170007FD RID: 2045
		// (get) Token: 0x060023B4 RID: 9140 RVA: 0x001381DD File Offset: 0x001373DD
		protected override int DaysInYearBeforeMinSupportedYear
		{
			get
			{
				return 355;
			}
		}

		// Token: 0x060023B5 RID: 9141 RVA: 0x001381E4 File Offset: 0x001373E4
		private static void ConvertHijriToGregorian(int HijriYear, int HijriMonth, int HijriDay, out int yg, out int mg, out int dg)
		{
			int num = HijriDay - 1;
			int num2 = HijriYear - 1318;
			DateTime gregorianDate = UmAlQuraCalendar.s_hijriYearInfo[num2].GregorianDate;
			int num3 = UmAlQuraCalendar.s_hijriYearInfo[num2].HijriMonthsLengthFlags;
			for (int i = 1; i < HijriMonth; i++)
			{
				num = num + 29 + (num3 & 1);
				num3 >>= 1;
			}
			gregorianDate.AddDays((double)num).GetDate(out yg, out mg, out dg);
		}

		// Token: 0x060023B6 RID: 9142 RVA: 0x00138254 File Offset: 0x00137454
		private static long GetAbsoluteDateUmAlQura(int year, int month, int day)
		{
			int year2;
			int month2;
			int day2;
			UmAlQuraCalendar.ConvertHijriToGregorian(year, month, day, out year2, out month2, out day2);
			return GregorianCalendar.GetAbsoluteDate(year2, month2, day2);
		}

		// Token: 0x060023B7 RID: 9143 RVA: 0x00138278 File Offset: 0x00137478
		internal static void CheckTicksRange(long ticks)
		{
			if (ticks < UmAlQuraCalendar.s_minDate.Ticks || ticks > UmAlQuraCalendar.s_maxDate.Ticks)
			{
				throw new ArgumentOutOfRangeException("time", ticks, SR.Format(CultureInfo.InvariantCulture, SR.ArgumentOutOfRange_CalendarRange, UmAlQuraCalendar.s_minDate, UmAlQuraCalendar.s_maxDate));
			}
		}

		// Token: 0x060023B8 RID: 9144 RVA: 0x001382D3 File Offset: 0x001374D3
		internal static void CheckEraRange(int era)
		{
			if (era != 0 && era != 1)
			{
				throw new ArgumentOutOfRangeException("era", era, SR.ArgumentOutOfRange_InvalidEraValue);
			}
		}

		// Token: 0x060023B9 RID: 9145 RVA: 0x001382F4 File Offset: 0x001374F4
		internal static void CheckYearRange(int year, int era)
		{
			UmAlQuraCalendar.CheckEraRange(era);
			if (year < 1318 || year > 1500)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1318, 1500));
			}
		}

		// Token: 0x060023BA RID: 9146 RVA: 0x00138346 File Offset: 0x00137546
		internal static void CheckYearMonthRange(int year, int month, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			if (month < 1 || month > 12)
			{
				throw new ArgumentOutOfRangeException("month", month, SR.ArgumentOutOfRange_Month);
			}
		}

		// Token: 0x060023BB RID: 9147 RVA: 0x00138370 File Offset: 0x00137570
		private static void ConvertGregorianToHijri(DateTime time, out int HijriYear, out int HijriMonth, out int HijriDay)
		{
			int num = (int)((time.Ticks - UmAlQuraCalendar.s_minDate.Ticks) / 864000000000L) / 355;
			while (time.CompareTo(UmAlQuraCalendar.s_hijriYearInfo[++num].GregorianDate) > 0)
			{
			}
			if (time.CompareTo(UmAlQuraCalendar.s_hijriYearInfo[num].GregorianDate) != 0)
			{
				num--;
			}
			TimeSpan timeSpan = time.Subtract(UmAlQuraCalendar.s_hijriYearInfo[num].GregorianDate);
			int num2 = num + 1318;
			int num3 = 1;
			int num4 = 1;
			double num5 = timeSpan.TotalDays;
			int num6 = UmAlQuraCalendar.s_hijriYearInfo[num].HijriMonthsLengthFlags;
			int num7 = 29 + (num6 & 1);
			while (num5 >= (double)num7)
			{
				num5 -= (double)num7;
				num6 >>= 1;
				num7 = 29 + (num6 & 1);
				num3++;
			}
			num4 += (int)num5;
			HijriDay = num4;
			HijriMonth = num3;
			HijriYear = num2;
		}

		// Token: 0x060023BC RID: 9148 RVA: 0x0013845C File Offset: 0x0013765C
		private int GetDatePart(DateTime time, int part)
		{
			long ticks = time.Ticks;
			UmAlQuraCalendar.CheckTicksRange(ticks);
			int num;
			int num2;
			int num3;
			UmAlQuraCalendar.ConvertGregorianToHijri(time, out num, out num2, out num3);
			if (part == 0)
			{
				return num;
			}
			if (part == 2)
			{
				return num2;
			}
			if (part == 3)
			{
				return num3;
			}
			if (part == 1)
			{
				return (int)(UmAlQuraCalendar.GetAbsoluteDateUmAlQura(num, num2, num3) - UmAlQuraCalendar.GetAbsoluteDateUmAlQura(num, 1, 1) + 1L);
			}
			throw new InvalidOperationException(SR.InvalidOperation_DateTimeParsing);
		}

		// Token: 0x060023BD RID: 9149 RVA: 0x001384B8 File Offset: 0x001376B8
		public override DateTime AddMonths(DateTime time, int months)
		{
			if (months < -120000 || months > 120000)
			{
				throw new ArgumentOutOfRangeException("months", months, SR.Format(SR.ArgumentOutOfRange_Range, -120000, 120000));
			}
			int num = this.GetDatePart(time, 0);
			int num2 = this.GetDatePart(time, 2);
			int num3 = this.GetDatePart(time, 3);
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
			if (num3 > 29)
			{
				int daysInMonth = this.GetDaysInMonth(num, num2);
				if (num3 > daysInMonth)
				{
					num3 = daysInMonth;
				}
			}
			UmAlQuraCalendar.CheckYearRange(num, 1);
			DateTime result = new DateTime(UmAlQuraCalendar.GetAbsoluteDateUmAlQura(num, num2, num3) * 864000000000L + time.Ticks % 864000000000L);
			Calendar.CheckAddResult(result.Ticks, this.MinSupportedDateTime, this.MaxSupportedDateTime);
			return result;
		}

		// Token: 0x060023BE RID: 9150 RVA: 0x0012BB85 File Offset: 0x0012AD85
		public override DateTime AddYears(DateTime time, int years)
		{
			return this.AddMonths(time, years * 12);
		}

		// Token: 0x060023BF RID: 9151 RVA: 0x001385B0 File Offset: 0x001377B0
		public override int GetDayOfMonth(DateTime time)
		{
			return this.GetDatePart(time, 3);
		}

		// Token: 0x060023C0 RID: 9152 RVA: 0x0012BB9B File Offset: 0x0012AD9B
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return (DayOfWeek)(time.Ticks / 864000000000L + 1L) % (DayOfWeek)7;
		}

		// Token: 0x060023C1 RID: 9153 RVA: 0x001385BA File Offset: 0x001377BA
		public override int GetDayOfYear(DateTime time)
		{
			return this.GetDatePart(time, 1);
		}

		// Token: 0x060023C2 RID: 9154 RVA: 0x001385C4 File Offset: 0x001377C4
		public override int GetDaysInMonth(int year, int month, int era)
		{
			UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
			if ((UmAlQuraCalendar.s_hijriYearInfo[year - 1318].HijriMonthsLengthFlags & 1 << month - 1) == 0)
			{
				return 29;
			}
			return 30;
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x001385F4 File Offset: 0x001377F4
		internal static int RealGetDaysInYear(int year)
		{
			int num = 0;
			int num2 = UmAlQuraCalendar.s_hijriYearInfo[year - 1318].HijriMonthsLengthFlags;
			for (int i = 1; i <= 12; i++)
			{
				num = num + 29 + (num2 & 1);
				num2 >>= 1;
			}
			return num;
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x00138635 File Offset: 0x00137835
		public override int GetDaysInYear(int year, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			return UmAlQuraCalendar.RealGetDaysInYear(year);
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x00138644 File Offset: 0x00137844
		public override int GetEra(DateTime time)
		{
			UmAlQuraCalendar.CheckTicksRange(time.Ticks);
			return 1;
		}

		// Token: 0x170007FE RID: 2046
		// (get) Token: 0x060023C6 RID: 9158 RVA: 0x0011FFFA File Offset: 0x0011F1FA
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

		// Token: 0x060023C7 RID: 9159 RVA: 0x00138653 File Offset: 0x00137853
		public override int GetMonth(DateTime time)
		{
			return this.GetDatePart(time, 2);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x0013865D File Offset: 0x0013785D
		public override int GetMonthsInYear(int year, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			return 12;
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x00138668 File Offset: 0x00137868
		public override int GetYear(DateTime time)
		{
			return this.GetDatePart(time, 0);
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x00138674 File Offset: 0x00137874
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			if (day >= 1 && day <= 29)
			{
				UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
				return false;
			}
			int daysInMonth = this.GetDaysInMonth(year, month, era);
			if (day < 1 || day > daysInMonth)
			{
				throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Day, daysInMonth, month));
			}
			return false;
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x001386D0 File Offset: 0x001378D0
		public override int GetLeapMonth(int year, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			return 0;
		}

		// Token: 0x060023CC RID: 9164 RVA: 0x001386DA File Offset: 0x001378DA
		public override bool IsLeapMonth(int year, int month, int era)
		{
			UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
			return false;
		}

		// Token: 0x060023CD RID: 9165 RVA: 0x001386E5 File Offset: 0x001378E5
		public override bool IsLeapYear(int year, int era)
		{
			UmAlQuraCalendar.CheckYearRange(year, era);
			return UmAlQuraCalendar.RealGetDaysInYear(year) == 355;
		}

		// Token: 0x060023CE RID: 9166 RVA: 0x001386FC File Offset: 0x001378FC
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			if (day >= 1 && day <= 29)
			{
				UmAlQuraCalendar.CheckYearMonthRange(year, month, era);
			}
			else
			{
				int daysInMonth = this.GetDaysInMonth(year, month, era);
				if (day < 1 || day > daysInMonth)
				{
					throw new ArgumentOutOfRangeException("day", day, SR.Format(SR.ArgumentOutOfRange_Day, daysInMonth, month));
				}
			}
			long absoluteDateUmAlQura = UmAlQuraCalendar.GetAbsoluteDateUmAlQura(year, month, day);
			if (absoluteDateUmAlQura < 0L)
			{
				throw new ArgumentOutOfRangeException(null, SR.ArgumentOutOfRange_BadYearMonthDay);
			}
			return new DateTime(absoluteDateUmAlQura * 864000000000L + Calendar.TimeToTicks(hour, minute, second, millisecond));
		}

		// Token: 0x170007FF RID: 2047
		// (get) Token: 0x060023CF RID: 9167 RVA: 0x0012E0CC File Offset: 0x0012D2CC
		// (set) Token: 0x060023D0 RID: 9168 RVA: 0x00138790 File Offset: 0x00137990
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
				if (value != 99 && (value < 1318 || value > 1500))
				{
					throw new ArgumentOutOfRangeException("value", value, SR.Format(SR.ArgumentOutOfRange_Range, 1318, 1500));
				}
				base.VerifyWritable();
				this._twoDigitYearMax = value;
			}
		}

		// Token: 0x060023D1 RID: 9169 RVA: 0x001387F0 File Offset: 0x001379F0
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
			if (year < 1318 || year > 1500)
			{
				throw new ArgumentOutOfRangeException("year", year, SR.Format(SR.ArgumentOutOfRange_Range, 1318, 1500));
			}
			return year;
		}

		// Token: 0x04000933 RID: 2355
		private static readonly UmAlQuraCalendar.DateMapping[] s_hijriYearInfo = UmAlQuraCalendar.InitDateMapping();

		// Token: 0x04000934 RID: 2356
		public const int UmAlQuraEra = 1;

		// Token: 0x04000935 RID: 2357
		private static readonly DateTime s_minDate = new DateTime(1900, 4, 30);

		// Token: 0x04000936 RID: 2358
		private static readonly DateTime s_maxDate = new DateTime(new DateTime(2077, 11, 16, 23, 59, 59, 999).Ticks + 9999L);

		// Token: 0x0200023A RID: 570
		private struct DateMapping
		{
			// Token: 0x060023D3 RID: 9171 RVA: 0x001388BF File Offset: 0x00137ABF
			internal DateMapping(int MonthsLengthFlags, int GYear, int GMonth, int GDay)
			{
				this.HijriMonthsLengthFlags = MonthsLengthFlags;
				this.GregorianDate = new DateTime(GYear, GMonth, GDay);
			}

			// Token: 0x04000937 RID: 2359
			internal int HijriMonthsLengthFlags;

			// Token: 0x04000938 RID: 2360
			internal DateTime GregorianDate;
		}
	}
}
