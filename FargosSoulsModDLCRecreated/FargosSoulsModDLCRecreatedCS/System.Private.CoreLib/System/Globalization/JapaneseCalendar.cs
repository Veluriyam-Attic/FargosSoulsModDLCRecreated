using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security;
using Internal.Win32;

namespace System.Globalization
{
	// Token: 0x02000215 RID: 533
	[NullableContext(1)]
	[Nullable(0)]
	public class JapaneseCalendar : Calendar
	{
		// Token: 0x17000767 RID: 1895
		// (get) Token: 0x06002199 RID: 8601 RVA: 0x0012FA0D File Offset: 0x0012EC0D
		public override DateTime MinSupportedDateTime
		{
			get
			{
				return JapaneseCalendar.s_calendarMinValue;
			}
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x0600219A RID: 8602 RVA: 0x0011CCE2 File Offset: 0x0011BEE2
		public override DateTime MaxSupportedDateTime
		{
			get
			{
				return DateTime.MaxValue;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x0600219B RID: 8603 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override CalendarAlgorithmType AlgorithmType
		{
			get
			{
				return CalendarAlgorithmType.SolarCalendar;
			}
		}

		// Token: 0x0600219C RID: 8604 RVA: 0x0012FA14 File Offset: 0x0012EC14
		internal static EraInfo[] GetEraInfo()
		{
			EraInfo[] result;
			if ((result = JapaneseCalendar.s_japaneseEraInfo) == null)
			{
				result = ((JapaneseCalendar.s_japaneseEraInfo = (GlobalizationMode.UseNls ? JapaneseCalendar.NlsGetJapaneseEras() : JapaneseCalendar.IcuGetJapaneseEras())) ?? (JapaneseCalendar.s_japaneseEraInfo = new EraInfo[]
				{
					new EraInfo(5, 2019, 5, 1, 2018, 1, 7981, "令和", "令", "R"),
					new EraInfo(4, 1989, 1, 8, 1988, 1, 31, "平成", "平", "H"),
					new EraInfo(3, 1926, 12, 25, 1925, 1, 64, "昭和", "昭", "S"),
					new EraInfo(2, 1912, 7, 30, 1911, 1, 15, "大正", "大", "T"),
					new EraInfo(1, 1868, 1, 1, 1867, 1, 45, "明治", "明", "M")
				}));
			}
			return result;
		}

		// Token: 0x0600219D RID: 8605 RVA: 0x0012FB28 File Offset: 0x0012ED28
		internal static Calendar GetDefaultInstance()
		{
			Calendar result;
			if ((result = JapaneseCalendar.s_defaultInstance) == null)
			{
				result = (JapaneseCalendar.s_defaultInstance = new JapaneseCalendar());
			}
			return result;
		}

		// Token: 0x0600219E RID: 8606 RVA: 0x0012FB44 File Offset: 0x0012ED44
		public JapaneseCalendar()
		{
			try
			{
				new CultureInfo("ja-JP");
			}
			catch (ArgumentException innerException)
			{
				throw new TypeInitializationException(base.GetType().ToString(), innerException);
			}
			this._helper = new GregorianCalendarHelper(this, JapaneseCalendar.GetEraInfo());
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x0600219F RID: 8607 RVA: 0x000C9D36 File Offset: 0x000C8F36
		internal override CalendarId ID
		{
			get
			{
				return CalendarId.JAPAN;
			}
		}

		// Token: 0x060021A0 RID: 8608 RVA: 0x0012FB98 File Offset: 0x0012ED98
		public override DateTime AddMonths(DateTime time, int months)
		{
			return this._helper.AddMonths(time, months);
		}

		// Token: 0x060021A1 RID: 8609 RVA: 0x0012FBA7 File Offset: 0x0012EDA7
		public override DateTime AddYears(DateTime time, int years)
		{
			return this._helper.AddYears(time, years);
		}

		// Token: 0x060021A2 RID: 8610 RVA: 0x0012FBB6 File Offset: 0x0012EDB6
		public override int GetDaysInMonth(int year, int month, int era)
		{
			return this._helper.GetDaysInMonth(year, month, era);
		}

		// Token: 0x060021A3 RID: 8611 RVA: 0x0012FBC6 File Offset: 0x0012EDC6
		public override int GetDaysInYear(int year, int era)
		{
			return this._helper.GetDaysInYear(year, era);
		}

		// Token: 0x060021A4 RID: 8612 RVA: 0x0012FBD5 File Offset: 0x0012EDD5
		public override int GetDayOfMonth(DateTime time)
		{
			return this._helper.GetDayOfMonth(time);
		}

		// Token: 0x060021A5 RID: 8613 RVA: 0x0012FBE3 File Offset: 0x0012EDE3
		public override DayOfWeek GetDayOfWeek(DateTime time)
		{
			return this._helper.GetDayOfWeek(time);
		}

		// Token: 0x060021A6 RID: 8614 RVA: 0x0012FBF1 File Offset: 0x0012EDF1
		public override int GetDayOfYear(DateTime time)
		{
			return this._helper.GetDayOfYear(time);
		}

		// Token: 0x060021A7 RID: 8615 RVA: 0x0012FBFF File Offset: 0x0012EDFF
		public override int GetMonthsInYear(int year, int era)
		{
			return this._helper.GetMonthsInYear(year, era);
		}

		// Token: 0x060021A8 RID: 8616 RVA: 0x0012FC0E File Offset: 0x0012EE0E
		public override int GetWeekOfYear(DateTime time, CalendarWeekRule rule, DayOfWeek firstDayOfWeek)
		{
			return this._helper.GetWeekOfYear(time, rule, firstDayOfWeek);
		}

		// Token: 0x060021A9 RID: 8617 RVA: 0x0012FC1E File Offset: 0x0012EE1E
		public override int GetEra(DateTime time)
		{
			return this._helper.GetEra(time);
		}

		// Token: 0x060021AA RID: 8618 RVA: 0x0012FC2C File Offset: 0x0012EE2C
		public override int GetMonth(DateTime time)
		{
			return this._helper.GetMonth(time);
		}

		// Token: 0x060021AB RID: 8619 RVA: 0x0012FC3A File Offset: 0x0012EE3A
		public override int GetYear(DateTime time)
		{
			return this._helper.GetYear(time);
		}

		// Token: 0x060021AC RID: 8620 RVA: 0x0012FC48 File Offset: 0x0012EE48
		public override bool IsLeapDay(int year, int month, int day, int era)
		{
			return this._helper.IsLeapDay(year, month, day, era);
		}

		// Token: 0x060021AD RID: 8621 RVA: 0x0012FC5A File Offset: 0x0012EE5A
		public override bool IsLeapYear(int year, int era)
		{
			return this._helper.IsLeapYear(year, era);
		}

		// Token: 0x060021AE RID: 8622 RVA: 0x0012FC69 File Offset: 0x0012EE69
		public override int GetLeapMonth(int year, int era)
		{
			return this._helper.GetLeapMonth(year, era);
		}

		// Token: 0x060021AF RID: 8623 RVA: 0x0012FC78 File Offset: 0x0012EE78
		public override bool IsLeapMonth(int year, int month, int era)
		{
			return this._helper.IsLeapMonth(year, month, era);
		}

		// Token: 0x060021B0 RID: 8624 RVA: 0x0012FC88 File Offset: 0x0012EE88
		public override DateTime ToDateTime(int year, int month, int day, int hour, int minute, int second, int millisecond, int era)
		{
			return this._helper.ToDateTime(year, month, day, hour, minute, second, millisecond, era);
		}

		// Token: 0x060021B1 RID: 8625 RVA: 0x0012FCB0 File Offset: 0x0012EEB0
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

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x060021B2 RID: 8626 RVA: 0x0012FD17 File Offset: 0x0012EF17
		public override int[] Eras
		{
			get
			{
				return this._helper.Eras;
			}
		}

		// Token: 0x060021B3 RID: 8627 RVA: 0x0012FD24 File Offset: 0x0012EF24
		internal static string[] EraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].eraName;
			}
			return array;
		}

		// Token: 0x060021B4 RID: 8628 RVA: 0x0012FD60 File Offset: 0x0012EF60
		internal static string[] AbbrevEraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].abbrevEraName;
			}
			return array;
		}

		// Token: 0x060021B5 RID: 8629 RVA: 0x0012FD9C File Offset: 0x0012EF9C
		internal static string[] EnglishEraNames()
		{
			EraInfo[] eraInfo = JapaneseCalendar.GetEraInfo();
			string[] array = new string[eraInfo.Length];
			for (int i = 0; i < eraInfo.Length; i++)
			{
				array[i] = eraInfo[eraInfo.Length - i - 1].englishEraName;
			}
			return array;
		}

		// Token: 0x060021B6 RID: 8630 RVA: 0x0012FDD8 File Offset: 0x0012EFD8
		internal override bool IsValidYear(int year, int era)
		{
			return this._helper.IsValidYear(year, era);
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x060021B7 RID: 8631 RVA: 0x0012FDE7 File Offset: 0x0012EFE7
		// (set) Token: 0x060021B8 RID: 8632 RVA: 0x0012FE0C File Offset: 0x0012F00C
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

		// Token: 0x060021B9 RID: 8633 RVA: 0x0012FE6C File Offset: 0x0012F06C
		private static EraInfo[] IcuGetJapaneseEras()
		{
			if (GlobalizationMode.Invariant)
			{
				return null;
			}
			string[] array;
			if (!CalendarData.EnumCalendarInfo("ja-JP", CalendarId.JAPAN, CalendarDataType.EraNames, out array))
			{
				return null;
			}
			List<EraInfo> list = new List<EraInfo>();
			int num = 9999;
			int latestJapaneseEra = Interop.Globalization.GetLatestJapaneseEra();
			for (int i = latestJapaneseEra; i >= 0; i--)
			{
				DateTime t;
				if (!JapaneseCalendar.GetJapaneseEraStartDate(i, out t))
				{
					return null;
				}
				if (t < JapaneseCalendar.s_calendarMinValue)
				{
					break;
				}
				list.Add(new EraInfo(i, t.Year, t.Month, t.Day, t.Year - 1, 1, num - t.Year + 1, array[i], JapaneseCalendar.GetAbbreviatedEraName(array, i), ""));
				num = t.Year;
			}
			string[] array2;
			if (!CalendarData.EnumCalendarInfo("ja", CalendarId.JAPAN, CalendarDataType.AbbrevEraNames, out array2))
			{
				array2 = JapaneseCalendar.s_abbreviatedEnglishEraNames;
			}
			if (array2[array2.Length - 1].Length == 0 || array2[array2.Length - 1][0] > '\u007f')
			{
				array2 = JapaneseCalendar.s_abbreviatedEnglishEraNames;
			}
			int num2 = (array2 == JapaneseCalendar.s_abbreviatedEnglishEraNames) ? (list.Count - 1) : (array2.Length - 1);
			for (int j = 0; j < list.Count; j++)
			{
				list[j].era = list.Count - j;
				if (num2 < array2.Length)
				{
					list[j].englishEraName = array2[num2];
				}
				num2--;
			}
			return list.ToArray();
		}

		// Token: 0x060021BA RID: 8634 RVA: 0x0012FFD0 File Offset: 0x0012F1D0
		private static string GetAbbreviatedEraName(string[] eraNames, int eraIndex)
		{
			return eraNames[eraIndex].Substring(0, 1);
		}

		// Token: 0x060021BB RID: 8635 RVA: 0x0012FFDC File Offset: 0x0012F1DC
		private static bool GetJapaneseEraStartDate(int era, out DateTime dateTime)
		{
			dateTime = default(DateTime);
			int year;
			int month;
			int day;
			bool japaneseEraStartDate = Interop.Globalization.GetJapaneseEraStartDate(era, out year, out month, out day);
			if (japaneseEraStartDate)
			{
				dateTime = new DateTime(year, month, day);
			}
			return japaneseEraStartDate;
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x00130010 File Offset: 0x0012F210
		private static EraInfo[] NlsGetJapaneseEras()
		{
			int num = 0;
			EraInfo[] array = null;
			try
			{
				using (RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("System\\CurrentControlSet\\Control\\Nls\\Calendars\\Japanese\\Eras"))
				{
					if (registryKey == null)
					{
						return null;
					}
					string[] valueNames = registryKey.GetValueNames();
					if (valueNames != null && valueNames.Length != 0)
					{
						array = new EraInfo[valueNames.Length];
						for (int i = 0; i < valueNames.Length; i++)
						{
							string value = valueNames[i];
							object value2 = registryKey.GetValue(valueNames[i]);
							EraInfo eraFromValue = JapaneseCalendar.GetEraFromValue(value, (value2 != null) ? value2.ToString() : null);
							if (eraFromValue != null)
							{
								array[num] = eraFromValue;
								num++;
							}
						}
					}
				}
			}
			catch (SecurityException)
			{
				return null;
			}
			catch (IOException)
			{
				return null;
			}
			catch (UnauthorizedAccessException)
			{
				return null;
			}
			if (num < 4)
			{
				return null;
			}
			Array.Resize<EraInfo>(ref array, num);
			Array.Sort<EraInfo>(array, new Comparison<EraInfo>(JapaneseCalendar.CompareEraRanges));
			for (int j = 0; j < array.Length; j++)
			{
				array[j].era = array.Length - j;
				if (j == 0)
				{
					array[0].maxEraYear = 9999 - array[0].yearOffset;
				}
				else
				{
					array[j].maxEraYear = array[j - 1].yearOffset + 1 - array[j].yearOffset;
				}
			}
			return array;
		}

		// Token: 0x060021BD RID: 8637 RVA: 0x00130170 File Offset: 0x0012F370
		private static int CompareEraRanges(EraInfo a, EraInfo b)
		{
			return b.ticks.CompareTo(a.ticks);
		}

		// Token: 0x060021BE RID: 8638 RVA: 0x00130184 File Offset: 0x0012F384
		private static EraInfo GetEraFromValue(string value, string data)
		{
			if (value == null || data == null)
			{
				return null;
			}
			if (value.Length != 10)
			{
				return null;
			}
			ReadOnlySpan<char> readOnlySpan = value.AsSpan();
			int num;
			int startMonth;
			int startDay;
			if (!int.TryParse(readOnlySpan.Slice(0, 4), NumberStyles.None, NumberFormatInfo.InvariantInfo, out num) || !int.TryParse(readOnlySpan.Slice(5, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out startMonth) || !int.TryParse(readOnlySpan.Slice(8, 2), NumberStyles.None, NumberFormatInfo.InvariantInfo, out startDay))
			{
				return null;
			}
			string[] array = data.Split('_', StringSplitOptions.None);
			if (array.Length != 4)
			{
				return null;
			}
			if (array[0].Length == 0 || array[1].Length == 0 || array[2].Length == 0 || array[3].Length == 0)
			{
				return null;
			}
			return new EraInfo(0, num, startMonth, startDay, num - 1, 1, 0, array[0], array[1], array[3]);
		}

		// Token: 0x0400087B RID: 2171
		private static readonly DateTime s_calendarMinValue = new DateTime(1868, 9, 8);

		// Token: 0x0400087C RID: 2172
		private static volatile EraInfo[] s_japaneseEraInfo;

		// Token: 0x0400087D RID: 2173
		internal static volatile Calendar s_defaultInstance;

		// Token: 0x0400087E RID: 2174
		internal GregorianCalendarHelper _helper;

		// Token: 0x0400087F RID: 2175
		private static readonly string[] s_abbreviatedEnglishEraNames = new string[]
		{
			"M",
			"T",
			"S",
			"H",
			"R"
		};
	}
}
