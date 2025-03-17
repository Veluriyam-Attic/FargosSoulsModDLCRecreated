using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x020001DC RID: 476
	internal class CalendarData
	{
		// Token: 0x06001DED RID: 7661 RVA: 0x0011D356 File Offset: 0x0011C556
		private CalendarData()
		{
		}

		// Token: 0x06001DEE RID: 7662 RVA: 0x0011D36C File Offset: 0x0011C56C
		private static CalendarData CreateInvariant()
		{
			CalendarData calendarData = new CalendarData();
			calendarData.sNativeName = "Gregorian Calendar";
			calendarData.iTwoDigitYearMax = 2029;
			calendarData.iCurrentEra = 1;
			calendarData.saShortDates = new string[]
			{
				"MM/dd/yyyy",
				"yyyy-MM-dd"
			};
			calendarData.saLongDates = new string[]
			{
				"dddd, dd MMMM yyyy"
			};
			calendarData.saYearMonths = new string[]
			{
				"yyyy MMMM"
			};
			calendarData.sMonthDay = "MMMM dd";
			calendarData.saEraNames = new string[]
			{
				"A.D."
			};
			calendarData.saAbbrevEraNames = new string[]
			{
				"AD"
			};
			calendarData.saAbbrevEnglishEraNames = new string[]
			{
				"AD"
			};
			calendarData.saDayNames = new string[]
			{
				"Sunday",
				"Monday",
				"Tuesday",
				"Wednesday",
				"Thursday",
				"Friday",
				"Saturday"
			};
			calendarData.saAbbrevDayNames = new string[]
			{
				"Sun",
				"Mon",
				"Tue",
				"Wed",
				"Thu",
				"Fri",
				"Sat"
			};
			calendarData.saSuperShortDayNames = new string[]
			{
				"Su",
				"Mo",
				"Tu",
				"We",
				"Th",
				"Fr",
				"Sa"
			};
			calendarData.saMonthNames = new string[]
			{
				"January",
				"February",
				"March",
				"April",
				"May",
				"June",
				"July",
				"August",
				"September",
				"October",
				"November",
				"December",
				string.Empty
			};
			calendarData.saAbbrevMonthNames = new string[]
			{
				"Jan",
				"Feb",
				"Mar",
				"Apr",
				"May",
				"Jun",
				"Jul",
				"Aug",
				"Sep",
				"Oct",
				"Nov",
				"Dec",
				string.Empty
			};
			calendarData.saMonthGenitiveNames = calendarData.saMonthNames;
			calendarData.saAbbrevMonthGenitiveNames = calendarData.saAbbrevMonthNames;
			calendarData.saLeapYearMonthNames = calendarData.saMonthNames;
			calendarData.bUseUserOverrides = false;
			return calendarData;
		}

		// Token: 0x06001DEF RID: 7663 RVA: 0x0011D614 File Offset: 0x0011C814
		internal CalendarData(string localeName, CalendarId calendarId, bool bUseUserOverrides)
		{
			this.bUseUserOverrides = bUseUserOverrides;
			if (!this.LoadCalendarDataFromSystemCore(localeName, calendarId))
			{
				if (this.sNativeName == null)
				{
					this.sNativeName = string.Empty;
				}
				if (this.saShortDates == null)
				{
					this.saShortDates = CalendarData.Invariant.saShortDates;
				}
				if (this.saYearMonths == null)
				{
					this.saYearMonths = CalendarData.Invariant.saYearMonths;
				}
				if (this.saLongDates == null)
				{
					this.saLongDates = CalendarData.Invariant.saLongDates;
				}
				if (this.sMonthDay == null)
				{
					this.sMonthDay = CalendarData.Invariant.sMonthDay;
				}
				if (this.saEraNames == null)
				{
					this.saEraNames = CalendarData.Invariant.saEraNames;
				}
				if (this.saAbbrevEraNames == null)
				{
					this.saAbbrevEraNames = CalendarData.Invariant.saAbbrevEraNames;
				}
				if (this.saAbbrevEnglishEraNames == null)
				{
					this.saAbbrevEnglishEraNames = CalendarData.Invariant.saAbbrevEnglishEraNames;
				}
				if (this.saDayNames == null)
				{
					this.saDayNames = CalendarData.Invariant.saDayNames;
				}
				if (this.saAbbrevDayNames == null)
				{
					this.saAbbrevDayNames = CalendarData.Invariant.saAbbrevDayNames;
				}
				if (this.saSuperShortDayNames == null)
				{
					this.saSuperShortDayNames = CalendarData.Invariant.saSuperShortDayNames;
				}
				if (this.saMonthNames == null)
				{
					this.saMonthNames = CalendarData.Invariant.saMonthNames;
				}
				if (this.saAbbrevMonthNames == null)
				{
					this.saAbbrevMonthNames = CalendarData.Invariant.saAbbrevMonthNames;
				}
			}
			if (calendarId == CalendarId.TAIWAN)
			{
				if (this.SystemSupportsTaiwaneseCalendar())
				{
					this.sNativeName = "中華民國曆";
				}
				else
				{
					this.sNativeName = string.Empty;
				}
			}
			if (this.saMonthGenitiveNames == null || this.saMonthGenitiveNames.Length == 0 || string.IsNullOrEmpty(this.saMonthGenitiveNames[0]))
			{
				this.saMonthGenitiveNames = this.saMonthNames;
			}
			if (this.saAbbrevMonthGenitiveNames == null || this.saAbbrevMonthGenitiveNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevMonthGenitiveNames[0]))
			{
				this.saAbbrevMonthGenitiveNames = this.saAbbrevMonthNames;
			}
			if (this.saLeapYearMonthNames == null || this.saLeapYearMonthNames.Length == 0 || string.IsNullOrEmpty(this.saLeapYearMonthNames[0]))
			{
				this.saLeapYearMonthNames = this.saMonthNames;
			}
			this.InitializeEraNames(localeName, calendarId);
			this.InitializeAbbreviatedEraNames(localeName, calendarId);
			if (calendarId == CalendarId.JAPAN)
			{
				this.saAbbrevEnglishEraNames = JapaneseCalendar.EnglishEraNames();
			}
			else
			{
				this.saAbbrevEnglishEraNames = new string[]
				{
					""
				};
			}
			this.iCurrentEra = this.saEraNames.Length;
		}

		// Token: 0x06001DF0 RID: 7664 RVA: 0x0011D868 File Offset: 0x0011CA68
		private void InitializeEraNames(string localeName, CalendarId calendarId)
		{
			switch (calendarId)
			{
			case CalendarId.GREGORIAN:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[]
					{
						"A.D."
					};
					return;
				}
				return;
			case CalendarId.GREGORIAN_US:
			case CalendarId.JULIAN:
				this.saEraNames = new string[]
				{
					"A.D."
				};
				return;
			case CalendarId.JAPAN:
			case CalendarId.JAPANESELUNISOLAR:
				this.saEraNames = JapaneseCalendar.EraNames();
				return;
			case CalendarId.TAIWAN:
				if (this.SystemSupportsTaiwaneseCalendar())
				{
					this.saEraNames = new string[]
					{
						"中華民國"
					};
					return;
				}
				this.saEraNames = new string[]
				{
					string.Empty
				};
				return;
			case CalendarId.KOREA:
				this.saEraNames = new string[]
				{
					"단기"
				};
				return;
			case CalendarId.HIJRI:
			case CalendarId.UMALQURA:
				if (localeName == "dv-MV")
				{
					this.saEraNames = new string[]
					{
						"ހިޖްރީ"
					};
					return;
				}
				this.saEraNames = new string[]
				{
					"بعد الهجرة"
				};
				return;
			case CalendarId.THAI:
				this.saEraNames = new string[]
				{
					"พ.ศ."
				};
				return;
			case CalendarId.HEBREW:
				this.saEraNames = new string[]
				{
					"C.E."
				};
				return;
			case CalendarId.GREGORIAN_ME_FRENCH:
				this.saEraNames = new string[]
				{
					"ap. J.-C."
				};
				return;
			case CalendarId.GREGORIAN_ARABIC:
			case CalendarId.GREGORIAN_XLIT_ENGLISH:
			case CalendarId.GREGORIAN_XLIT_FRENCH:
				this.saEraNames = new string[]
				{
					"م"
				};
				return;
			case CalendarId.PERSIAN:
				if (this.saEraNames == null || this.saEraNames.Length == 0 || string.IsNullOrEmpty(this.saEraNames[0]))
				{
					this.saEraNames = new string[]
					{
						"ه.ش"
					};
					return;
				}
				return;
			}
			this.saEraNames = CalendarData.Invariant.saEraNames;
		}

		// Token: 0x06001DF1 RID: 7665 RVA: 0x0011DA50 File Offset: 0x0011CC50
		private void InitializeAbbreviatedEraNames(string localeName, CalendarId calendarId)
		{
			if (calendarId <= CalendarId.JULIAN)
			{
				switch (calendarId)
				{
				case CalendarId.GREGORIAN:
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = new string[]
						{
							"AD"
						};
						return;
					}
					return;
				case CalendarId.GREGORIAN_US:
					break;
				case CalendarId.JAPAN:
					goto IL_93;
				case CalendarId.TAIWAN:
					this.saAbbrevEraNames = new string[1];
					if (this.saEraNames[0].Length == 4)
					{
						this.saAbbrevEraNames[0] = this.saEraNames[0].Substring(2, 2);
						return;
					}
					this.saAbbrevEraNames[0] = this.saEraNames[0];
					return;
				case CalendarId.KOREA:
					goto IL_148;
				case CalendarId.HIJRI:
					goto IL_9F;
				default:
					if (calendarId != CalendarId.JULIAN)
					{
						goto IL_148;
					}
					break;
				}
				this.saAbbrevEraNames = new string[]
				{
					"AD"
				};
				return;
			}
			if (calendarId != CalendarId.JAPANESELUNISOLAR)
			{
				if (calendarId != CalendarId.PERSIAN)
				{
					if (calendarId != CalendarId.UMALQURA)
					{
						goto IL_148;
					}
					goto IL_9F;
				}
				else
				{
					if (this.saAbbrevEraNames == null || this.saAbbrevEraNames.Length == 0 || string.IsNullOrEmpty(this.saAbbrevEraNames[0]))
					{
						this.saAbbrevEraNames = this.saEraNames;
						return;
					}
					return;
				}
			}
			IL_93:
			this.saAbbrevEraNames = JapaneseCalendar.AbbrevEraNames();
			return;
			IL_9F:
			if (localeName == "dv-MV")
			{
				this.saAbbrevEraNames = new string[]
				{
					"ހ."
				};
				return;
			}
			this.saAbbrevEraNames = new string[]
			{
				"هـ"
			};
			return;
			IL_148:
			this.saAbbrevEraNames = this.saEraNames;
		}

		// Token: 0x06001DF2 RID: 7666 RVA: 0x0011DBB4 File Offset: 0x0011CDB4
		internal static int GetCalendarCurrentEra(Calendar calendar)
		{
			if (GlobalizationMode.Invariant)
			{
				return CalendarData.Invariant.iCurrentEra;
			}
			CalendarId baseCalendarID = calendar.BaseCalendarID;
			string name = CalendarData.CalendarIdToCultureName(baseCalendarID);
			return CultureInfo.GetCultureInfo(name)._cultureData.GetCalendar(baseCalendarID).iCurrentEra;
		}

		// Token: 0x06001DF3 RID: 7667 RVA: 0x0011DBF8 File Offset: 0x0011CDF8
		private static string CalendarIdToCultureName(CalendarId calendarId)
		{
			switch (calendarId)
			{
			case CalendarId.GREGORIAN_US:
				return "fa-IR";
			case CalendarId.JAPAN:
				return "ja-JP";
			case CalendarId.TAIWAN:
				return "zh-TW";
			case CalendarId.KOREA:
				return "ko-KR";
			case CalendarId.HIJRI:
			case CalendarId.GREGORIAN_ARABIC:
			case CalendarId.UMALQURA:
				return "ar-SA";
			case CalendarId.THAI:
				return "th-TH";
			case CalendarId.HEBREW:
				return "he-IL";
			case CalendarId.GREGORIAN_ME_FRENCH:
				return "ar-DZ";
			case CalendarId.GREGORIAN_XLIT_ENGLISH:
			case CalendarId.GREGORIAN_XLIT_FRENCH:
				return "ar-IQ";
			}
			return "en-US";
		}

		// Token: 0x06001DF4 RID: 7668 RVA: 0x0011DCA2 File Offset: 0x0011CEA2
		private bool SystemSupportsTaiwaneseCalendar()
		{
			if (!GlobalizationMode.UseNls)
			{
				return CalendarData.IcuSystemSupportsTaiwaneseCalendar();
			}
			return CalendarData.NlsSystemSupportsTaiwaneseCalendar();
		}

		// Token: 0x06001DF5 RID: 7669 RVA: 0x0011DCB8 File Offset: 0x0011CEB8
		private bool IcuLoadCalendarDataFromSystem(string localeName, CalendarId calendarId)
		{
			bool flag = true;
			flag &= CalendarData.GetCalendarInfo(localeName, calendarId, CalendarDataType.NativeName, out this.sNativeName);
			flag &= CalendarData.GetCalendarInfo(localeName, calendarId, CalendarDataType.MonthDay, out this.sMonthDay);
			if (this.sMonthDay != null)
			{
				this.sMonthDay = CalendarData.NormalizeDatePattern(this.sMonthDay);
			}
			flag &= CalendarData.EnumDatePatterns(localeName, calendarId, CalendarDataType.ShortDates, out this.saShortDates);
			flag &= CalendarData.EnumDatePatterns(localeName, calendarId, CalendarDataType.LongDates, out this.saLongDates);
			flag &= CalendarData.EnumDatePatterns(localeName, calendarId, CalendarDataType.YearMonths, out this.saYearMonths);
			flag &= CalendarData.EnumCalendarInfo(localeName, calendarId, CalendarDataType.DayNames, out this.saDayNames);
			flag &= CalendarData.EnumCalendarInfo(localeName, calendarId, CalendarDataType.AbbrevDayNames, out this.saAbbrevDayNames);
			flag &= CalendarData.EnumCalendarInfo(localeName, calendarId, CalendarDataType.SuperShortDayNames, out this.saSuperShortDayNames);
			string text = null;
			flag &= CalendarData.EnumMonthNames(localeName, calendarId, CalendarDataType.MonthNames, out this.saMonthNames, ref text);
			if (text != null)
			{
				this.saLeapYearMonthNames = (string[])this.saMonthNames.Clone();
				this.saLeapYearMonthNames[6] = text;
				this.saMonthNames[5] = this.saMonthNames[6];
				this.saMonthNames[6] = text;
			}
			flag &= CalendarData.EnumMonthNames(localeName, calendarId, CalendarDataType.AbbrevMonthNames, out this.saAbbrevMonthNames, ref text);
			flag &= CalendarData.EnumMonthNames(localeName, calendarId, CalendarDataType.MonthGenitiveNames, out this.saMonthGenitiveNames, ref text);
			flag &= CalendarData.EnumMonthNames(localeName, calendarId, CalendarDataType.AbbrevMonthGenitiveNames, out this.saAbbrevMonthGenitiveNames, ref text);
			flag &= CalendarData.EnumEraNames(localeName, calendarId, CalendarDataType.EraNames, out this.saEraNames);
			return flag & CalendarData.EnumEraNames(localeName, calendarId, CalendarDataType.AbbrevEraNames, out this.saAbbrevEraNames);
		}

		// Token: 0x06001DF6 RID: 7670 RVA: 0x0011DE1A File Offset: 0x0011D01A
		internal static int IcuGetTwoDigitYearMax(CalendarId calendarId)
		{
			return -1;
		}

		// Token: 0x06001DF7 RID: 7671 RVA: 0x0011DE20 File Offset: 0x0011D020
		internal static int IcuGetCalendars(string localeName, CalendarId[] calendars)
		{
			int num = Interop.Globalization.GetCalendars(localeName, calendars, calendars.Length);
			if (num == 0 && calendars.Length != 0)
			{
				calendars[0] = CalendarId.GREGORIAN;
				num = 1;
			}
			return num;
		}

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000AC09E File Offset: 0x000AB29E
		private static bool IcuSystemSupportsTaiwaneseCalendar()
		{
			return true;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x0011DE46 File Offset: 0x0011D046
		private unsafe static bool GetCalendarInfo(string localeName, CalendarId calendarId, CalendarDataType dataType, out string calendarString)
		{
			return Interop.CallStringMethod<string, CalendarId, CalendarDataType>(delegate(Span<char> buffer, string locale, CalendarId id, CalendarDataType type)
			{
				fixed (char* pinnableReference = buffer.GetPinnableReference())
				{
					char* result = pinnableReference;
					return Interop.Globalization.GetCalendarInfo(locale, id, type, result, buffer.Length);
				}
			}, localeName, calendarId, dataType, out calendarString);
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x0011DE70 File Offset: 0x0011D070
		private static bool EnumDatePatterns(string localeName, CalendarId calendarId, CalendarDataType dataType, out string[] datePatterns)
		{
			datePatterns = null;
			CalendarData.IcuEnumCalendarsData icuEnumCalendarsData = default(CalendarData.IcuEnumCalendarsData);
			icuEnumCalendarsData.Results = new List<string>();
			icuEnumCalendarsData.DisallowDuplicates = true;
			bool flag = CalendarData.EnumCalendarInfo(localeName, calendarId, dataType, ref icuEnumCalendarsData);
			if (flag)
			{
				List<string> results = icuEnumCalendarsData.Results;
				for (int i = 0; i < results.Count; i++)
				{
					results[i] = CalendarData.NormalizeDatePattern(results[i]);
				}
				if (dataType == CalendarDataType.ShortDates)
				{
					CalendarData.FixDefaultShortDatePattern(results);
				}
				datePatterns = results.ToArray();
			}
			return flag;
		}

		// Token: 0x06001DFB RID: 7675 RVA: 0x0011DEE8 File Offset: 0x0011D0E8
		private unsafe static void FixDefaultShortDatePattern(List<string> shortDatePatterns)
		{
			if (shortDatePatterns.Count == 0)
			{
				return;
			}
			string text = shortDatePatterns[0];
			if (text.Length > 100)
			{
				return;
			}
			int num = text.Length + 2;
			Span<char> span = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)num) * 2)], num);
			Span<char> span2 = span;
			int i;
			for (i = 0; i < text.Length; i++)
			{
				if (text[i] == '\'')
				{
					do
					{
						*span2[i] = text[i];
						i++;
					}
					while (i < text.Length && text[i] != '\'');
					if (i >= text.Length)
					{
						return;
					}
				}
				else if (text[i] == 'y')
				{
					*span2[i] = 'y';
					break;
				}
				*span2[i] = text[i];
			}
			if (i >= text.Length - 1 || text[i + 1] != 'y')
			{
				return;
			}
			if (i + 2 < text.Length && text[i + 2] == 'y')
			{
				return;
			}
			*span2[i + 1] = 'y';
			*span2[i + 2] = 'y';
			*span2[i + 3] = 'y';
			for (i += 2; i < text.Length; i++)
			{
				*span2[i + 2] = text[i];
			}
			shortDatePatterns[0] = span2.ToString();
			for (int j = 1; j < shortDatePatterns.Count; j++)
			{
				if (shortDatePatterns[j] == shortDatePatterns[0])
				{
					shortDatePatterns[j] = text;
					return;
				}
			}
			shortDatePatterns.Add(text);
		}

		// Token: 0x06001DFC RID: 7676 RVA: 0x0011E070 File Offset: 0x0011D270
		private static string NormalizeDatePattern(string input)
		{
			StringBuilder stringBuilder = StringBuilderCache.Acquire(input.Length);
			int i = 0;
			while (i < input.Length)
			{
				char c = input[i];
				int num;
				if (c <= 'L')
				{
					if (c <= 'E')
					{
						if (c == '\'')
						{
							stringBuilder.Append(input[i++]);
							while (i < input.Length)
							{
								char c2 = input[i++];
								stringBuilder.Append(c2);
								if (c2 == '\'')
								{
									break;
								}
							}
							continue;
						}
						if (c != 'E')
						{
							goto IL_107;
						}
					}
					else
					{
						if (c == 'G')
						{
							num = CalendarData.CountOccurrences(input, 'G', ref i);
							stringBuilder.Append('g');
							continue;
						}
						if (c != 'L')
						{
							goto IL_107;
						}
						goto IL_B2;
					}
				}
				else if (c <= 'c')
				{
					if (c == 'M')
					{
						goto IL_B2;
					}
					if (c != 'c')
					{
						goto IL_107;
					}
				}
				else if (c != 'e')
				{
					if (c != 'y')
					{
						goto IL_107;
					}
					num = CalendarData.CountOccurrences(input, 'y', ref i);
					if (num == 1)
					{
						num = 4;
					}
					stringBuilder.Append('y', num);
					continue;
				}
				CalendarData.NormalizeDayOfWeek(input, stringBuilder, ref i);
				continue;
				IL_B2:
				num = CalendarData.CountOccurrences(input, input[i], ref i);
				if (num > 4)
				{
					num = 3;
				}
				stringBuilder.Append('M', num);
				continue;
				IL_107:
				stringBuilder.Append(input[i++]);
			}
			return StringBuilderCache.GetStringAndRelease(stringBuilder);
		}

		// Token: 0x06001DFD RID: 7677 RVA: 0x0011E1A8 File Offset: 0x0011D3A8
		private static void NormalizeDayOfWeek(string input, StringBuilder destination, ref int index)
		{
			char value = input[index];
			int num = CalendarData.CountOccurrences(input, value, ref index);
			num = Math.Max(num, 3);
			if (num > 4)
			{
				num = 3;
			}
			destination.Append('d', num);
		}

		// Token: 0x06001DFE RID: 7678 RVA: 0x0011E1E0 File Offset: 0x0011D3E0
		private static int CountOccurrences(string input, char value, ref int index)
		{
			int num = index;
			while (index < input.Length && input[index] == value)
			{
				index++;
			}
			return index - num;
		}

		// Token: 0x06001DFF RID: 7679 RVA: 0x0011E214 File Offset: 0x0011D414
		private static bool EnumMonthNames(string localeName, CalendarId calendarId, CalendarDataType dataType, out string[] monthNames, ref string leapHebrewMonthName)
		{
			monthNames = null;
			CalendarData.IcuEnumCalendarsData icuEnumCalendarsData = default(CalendarData.IcuEnumCalendarsData);
			icuEnumCalendarsData.Results = new List<string>();
			bool flag = CalendarData.EnumCalendarInfo(localeName, calendarId, dataType, ref icuEnumCalendarsData);
			if (flag)
			{
				if (icuEnumCalendarsData.Results.Count == 12)
				{
					icuEnumCalendarsData.Results.Add(string.Empty);
				}
				if (icuEnumCalendarsData.Results.Count > 13)
				{
					if (calendarId == CalendarId.HEBREW)
					{
						leapHebrewMonthName = icuEnumCalendarsData.Results[13];
					}
					icuEnumCalendarsData.Results.RemoveRange(13, icuEnumCalendarsData.Results.Count - 13);
				}
				monthNames = icuEnumCalendarsData.Results.ToArray();
			}
			return flag;
		}

		// Token: 0x06001E00 RID: 7680 RVA: 0x0011E2B4 File Offset: 0x0011D4B4
		private static bool EnumEraNames(string localeName, CalendarId calendarId, CalendarDataType dataType, out string[] eraNames)
		{
			bool result = CalendarData.EnumCalendarInfo(localeName, calendarId, dataType, out eraNames);
			if (calendarId != CalendarId.JAPAN && calendarId != CalendarId.JAPANESELUNISOLAR)
			{
				string[] array = eraNames;
				if (array != null && array.Length != 0)
				{
					string[] array2 = new string[]
					{
						eraNames[eraNames.Length - 1]
					};
					eraNames = array2;
				}
			}
			return result;
		}

		// Token: 0x06001E01 RID: 7681 RVA: 0x0011E2FC File Offset: 0x0011D4FC
		internal static bool EnumCalendarInfo(string localeName, CalendarId calendarId, CalendarDataType dataType, out string[] calendarData)
		{
			calendarData = null;
			CalendarData.IcuEnumCalendarsData icuEnumCalendarsData = default(CalendarData.IcuEnumCalendarsData);
			icuEnumCalendarsData.Results = new List<string>();
			bool flag = CalendarData.EnumCalendarInfo(localeName, calendarId, dataType, ref icuEnumCalendarsData);
			if (flag)
			{
				calendarData = icuEnumCalendarsData.Results.ToArray();
			}
			return flag;
		}

		// Token: 0x06001E02 RID: 7682 RVA: 0x0011E33C File Offset: 0x0011D53C
		private static bool EnumCalendarInfo(string localeName, CalendarId calendarId, CalendarDataType dataType, ref CalendarData.IcuEnumCalendarsData callbackContext)
		{
			return Interop.Globalization.EnumCalendarInfo(ldftn(EnumCalendarInfoCallback), localeName != null, calendarId, dataType, (IntPtr)Unsafe.AsPointer<CalendarData.IcuEnumCalendarsData>(ref callbackContext));
		}

		// Token: 0x06001E03 RID: 7683 RVA: 0x0011E358 File Offset: 0x0011D558
		[UnmanagedCallersOnly]
		private unsafe static void EnumCalendarInfoCallback(char* calendarStringPtr, IntPtr context)
		{
			try
			{
				ReadOnlySpan<char> strA = new ReadOnlySpan<char>((void*)calendarStringPtr, string.wcslen(calendarStringPtr));
				ref CalendarData.IcuEnumCalendarsData ptr = ref Unsafe.As<byte, CalendarData.IcuEnumCalendarsData>(ref *(byte*)((void*)context));
				if (ptr.DisallowDuplicates)
				{
					foreach (string value in ptr.Results)
					{
						if (string.CompareOrdinal(strA, value) == 0)
						{
							return;
						}
					}
				}
				ptr.Results.Add(strA.ToString());
			}
			catch (Exception ex)
			{
			}
		}

		// Token: 0x06001E04 RID: 7684 RVA: 0x0011E400 File Offset: 0x0011D600
		internal static int NlsGetTwoDigitYearMax(CalendarId calendarId)
		{
			if (GlobalizationMode.Invariant)
			{
				return CalendarData.Invariant.iTwoDigitYearMax;
			}
			int result;
			if (!CalendarData.CallGetCalendarInfoEx(null, calendarId, 48U, out result))
			{
				return -1;
			}
			return result;
		}

		// Token: 0x06001E05 RID: 7685 RVA: 0x0011E430 File Offset: 0x0011D630
		private static bool NlsSystemSupportsTaiwaneseCalendar()
		{
			string text;
			return CalendarData.CallGetCalendarInfoEx("zh-TW", CalendarId.TAIWAN, 2U, out text);
		}

		// Token: 0x06001E06 RID: 7686 RVA: 0x0011E44B File Offset: 0x0011D64B
		private static bool CallGetCalendarInfoEx(string localeName, CalendarId calendar, uint calType, out int data)
		{
			return Interop.Kernel32.GetCalendarInfoEx(localeName, (uint)calendar, IntPtr.Zero, calType | 536870912U, IntPtr.Zero, 0, out data) != 0;
		}

		// Token: 0x06001E07 RID: 7687 RVA: 0x0011E46C File Offset: 0x0011D66C
		private unsafe static bool CallGetCalendarInfoEx(string localeName, CalendarId calendar, uint calType, out string data)
		{
			char* ptr = stackalloc char[(UIntPtr)160];
			int num = Interop.Kernel32.GetCalendarInfoEx(localeName, (uint)calendar, IntPtr.Zero, calType, (IntPtr)((void*)ptr), 80, IntPtr.Zero);
			if (num > 0)
			{
				if (ptr[num - 1] == '\0')
				{
					num--;
				}
				data = new string(ptr, 0, num);
				return true;
			}
			data = "";
			return false;
		}

		// Token: 0x06001E08 RID: 7688 RVA: 0x0011E4C4 File Offset: 0x0011D6C4
		[UnmanagedCallersOnly]
		private unsafe static Interop.BOOL EnumCalendarInfoCallback(char* lpCalendarInfoString, uint calendar, IntPtr pReserved, void* lParam)
		{
			ref CalendarData.EnumData ptr = ref Unsafe.As<byte, CalendarData.EnumData>(ref *(byte*)lParam);
			Interop.BOOL result;
			try
			{
				string text = new string(lpCalendarInfoString);
				if (ptr.userOverride != text)
				{
					ptr.strings.Add(text);
				}
				result = Interop.BOOL.TRUE;
			}
			catch (Exception)
			{
				result = Interop.BOOL.FALSE;
			}
			return result;
		}

		// Token: 0x06001E09 RID: 7689 RVA: 0x0011E514 File Offset: 0x0011D714
		[UnmanagedCallersOnly]
		private unsafe static Interop.BOOL EnumCalendarsCallback(char* lpCalendarInfoString, uint calendar, IntPtr reserved, void* lParam)
		{
			ref CalendarData.NlsEnumCalendarsData ptr = ref Unsafe.As<byte, CalendarData.NlsEnumCalendarsData>(ref *(byte*)lParam);
			Interop.BOOL result;
			try
			{
				if ((long)ptr.userOverride != (long)((ulong)calendar))
				{
					ptr.calendars.Add((int)calendar);
				}
				result = Interop.BOOL.TRUE;
			}
			catch (Exception)
			{
				result = Interop.BOOL.FALSE;
			}
			return result;
		}

		// Token: 0x06001E0A RID: 7690 RVA: 0x0011E55C File Offset: 0x0011D75C
		private bool LoadCalendarDataFromSystemCore(string localeName, CalendarId calendarId)
		{
			if (GlobalizationMode.UseNls)
			{
				return this.NlsLoadCalendarDataFromSystem(localeName, calendarId);
			}
			bool flag = this.IcuLoadCalendarDataFromSystem(localeName, calendarId);
			if (flag && this.bUseUserOverrides)
			{
				CalendarData.NormalizeCalendarId(ref calendarId, ref localeName);
				flag &= CalendarData.CallGetCalendarInfoEx(localeName, calendarId, 48U, out this.iTwoDigitYearMax);
				CalendarId calendarId2 = (CalendarId)CultureData.GetLocaleInfoExInt(localeName, 4105U);
				if (calendarId2 == calendarId)
				{
					string value = CultureData.ReescapeWin32String(CultureData.GetLocaleInfoEx(localeName, 31U));
					string value2 = CultureData.ReescapeWin32String(CultureData.GetLocaleInfoEx(localeName, 32U));
					this.InsertOrSwapOverride(value, ref this.saShortDates);
					this.InsertOrSwapOverride(value2, ref this.saLongDates);
				}
			}
			return flag;
		}

		// Token: 0x06001E0B RID: 7691 RVA: 0x0011E5F0 File Offset: 0x0011D7F0
		private void InsertOrSwapOverride(string value, ref string[] destination)
		{
			if (value == null)
			{
				return;
			}
			for (int i = 0; i < destination.Length; i++)
			{
				if (destination[i] == value)
				{
					if (i > 0)
					{
						string text = destination[0];
						destination[0] = value;
						destination[i] = text;
					}
					return;
				}
			}
			string[] array = new string[destination.Length + 1];
			array[0] = value;
			Array.Copy(destination, 0, array, 1, destination.Length);
			destination = array;
		}

		// Token: 0x06001E0C RID: 7692 RVA: 0x0011E654 File Offset: 0x0011D854
		private bool NlsLoadCalendarDataFromSystem(string localeName, CalendarId calendarId)
		{
			bool flag = true;
			uint num = this.bUseUserOverrides ? 0U : 2147483648U;
			CalendarData.NormalizeCalendarId(ref calendarId, ref localeName);
			flag &= CalendarData.CallGetCalendarInfoEx(localeName, calendarId, 48U | num, out this.iTwoDigitYearMax);
			flag &= CalendarData.CallGetCalendarInfoEx(localeName, calendarId, 2U, out this.sNativeName);
			flag &= CalendarData.CallGetCalendarInfoEx(localeName, calendarId, 56U, out this.sMonthDay);
			flag &= CalendarData.CallEnumCalendarInfo(localeName, calendarId, 5U, 31U | num, out this.saShortDates);
			flag &= CalendarData.CallEnumCalendarInfo(localeName, calendarId, 6U, 32U | num, out this.saLongDates);
			flag &= CalendarData.CallEnumCalendarInfo(localeName, calendarId, 47U, 4102U, out this.saYearMonths);
			flag &= CalendarData.GetCalendarDayInfo(localeName, calendarId, 13U, out this.saDayNames);
			flag &= CalendarData.GetCalendarDayInfo(localeName, calendarId, 20U, out this.saAbbrevDayNames);
			flag &= CalendarData.GetCalendarMonthInfo(localeName, calendarId, 21U, out this.saMonthNames);
			flag &= CalendarData.GetCalendarMonthInfo(localeName, calendarId, 34U, out this.saAbbrevMonthNames);
			CalendarData.GetCalendarDayInfo(localeName, calendarId, 55U, out this.saSuperShortDayNames);
			if (calendarId == CalendarId.GREGORIAN)
			{
				CalendarData.GetCalendarMonthInfo(localeName, calendarId, 268435477U, out this.saMonthGenitiveNames);
				CalendarData.GetCalendarMonthInfo(localeName, calendarId, 268435490U, out this.saAbbrevMonthGenitiveNames);
			}
			CalendarData.CallEnumCalendarInfo(localeName, calendarId, 4U, 0U, out this.saEraNames);
			CalendarData.CallEnumCalendarInfo(localeName, calendarId, 57U, 0U, out this.saAbbrevEraNames);
			this.saShortDates = CultureData.ReescapeWin32Strings(this.saShortDates);
			this.saLongDates = CultureData.ReescapeWin32Strings(this.saLongDates);
			this.saYearMonths = CultureData.ReescapeWin32Strings(this.saYearMonths);
			this.sMonthDay = CultureData.ReescapeWin32String(this.sMonthDay);
			return flag;
		}

		// Token: 0x06001E0D RID: 7693 RVA: 0x0011E7E0 File Offset: 0x0011D9E0
		private static void NormalizeCalendarId(ref CalendarId calendarId, ref string localeName)
		{
			switch (calendarId)
			{
			case CalendarId.JULIAN:
			case CalendarId.CHINESELUNISOLAR:
			case CalendarId.SAKA:
			case CalendarId.LUNAR_ETO_CHN:
			case CalendarId.LUNAR_ETO_KOR:
			case CalendarId.LUNAR_ETO_ROKUYOU:
			case CalendarId.KOREANLUNISOLAR:
			case CalendarId.TAIWANLUNISOLAR:
				calendarId = CalendarId.GREGORIAN_US;
				break;
			case CalendarId.JAPANESELUNISOLAR:
				calendarId = CalendarId.JAPAN;
				break;
			}
			CalendarData.CheckSpecialCalendar(ref calendarId, ref localeName);
		}

		// Token: 0x06001E0E RID: 7694 RVA: 0x0011E830 File Offset: 0x0011DA30
		private static void CheckSpecialCalendar(ref CalendarId calendar, ref string localeName)
		{
			CalendarId calendarId = calendar;
			string text;
			if (calendarId != CalendarId.GREGORIAN_US)
			{
				if (calendarId != CalendarId.TAIWAN)
				{
					return;
				}
				if (!CalendarData.NlsSystemSupportsTaiwaneseCalendar())
				{
					calendar = CalendarId.GREGORIAN;
				}
			}
			else if (!CalendarData.CallGetCalendarInfoEx(localeName, calendar, 2U, out text))
			{
				localeName = "fa-IR";
				if (!CalendarData.CallGetCalendarInfoEx(localeName, calendar, 2U, out text))
				{
					localeName = "en-US";
					calendar = CalendarId.GREGORIAN;
					return;
				}
			}
		}

		// Token: 0x06001E0F RID: 7695 RVA: 0x0011E884 File Offset: 0x0011DA84
		private static bool CallEnumCalendarInfo(string localeName, CalendarId calendar, uint calType, uint lcType, out string[] data)
		{
			CalendarData.EnumData enumData = default(CalendarData.EnumData);
			enumData.userOverride = null;
			enumData.strings = new List<string>();
			if (lcType != 0U && (lcType & 2147483648U) == 0U)
			{
				CalendarId calendarId = (CalendarId)CultureData.GetLocaleInfoExInt(localeName, 4105U);
				if (calendarId == calendar)
				{
					string localeInfoEx = CultureData.GetLocaleInfoEx(localeName, lcType);
					if (localeInfoEx != null)
					{
						enumData.userOverride = localeInfoEx;
						enumData.strings.Add(localeInfoEx);
					}
				}
			}
			Interop.Kernel32.EnumCalendarInfoExEx(ldftn(EnumCalendarInfoCallback), localeName, (Interop.BOOL)calendar, null, calType, Unsafe.AsPointer<CalendarData.EnumData>(ref enumData));
			if (enumData.strings.Count == 0)
			{
				data = null;
				return false;
			}
			string[] array = enumData.strings.ToArray();
			if (calType == 57U || calType == 4U)
			{
				Array.Reverse<string>(array, 0, array.Length);
			}
			data = array;
			return true;
		}

		// Token: 0x06001E10 RID: 7696 RVA: 0x0011E938 File Offset: 0x0011DB38
		private static bool GetCalendarDayInfo(string localeName, CalendarId calendar, uint calType, out string[] outputStrings)
		{
			bool flag = true;
			string[] array = new string[7];
			int i = 0;
			while (i < 7)
			{
				flag &= CalendarData.CallGetCalendarInfoEx(localeName, calendar, calType, out array[i]);
				if (i == 0)
				{
					calType -= 7U;
				}
				i++;
				calType += 1U;
			}
			outputStrings = array;
			return flag;
		}

		// Token: 0x06001E11 RID: 7697 RVA: 0x0011E980 File Offset: 0x0011DB80
		private static bool GetCalendarMonthInfo(string localeName, CalendarId calendar, uint calType, out string[] outputStrings)
		{
			string[] array = new string[13];
			int i = 0;
			while (i < 13)
			{
				if (!CalendarData.CallGetCalendarInfoEx(localeName, calendar, calType, out array[i]))
				{
					array[i] = "";
				}
				i++;
				calType += 1U;
			}
			outputStrings = array;
			return true;
		}

		// Token: 0x06001E12 RID: 7698 RVA: 0x0011E9C4 File Offset: 0x0011DBC4
		internal unsafe static int GetCalendarsCore(string localeName, bool useUserOverride, CalendarId[] calendars)
		{
			if (GlobalizationMode.UseNls)
			{
				return CalendarData.NlsGetCalendars(localeName, useUserOverride, calendars);
			}
			int num = CalendarData.IcuGetCalendars(localeName, calendars);
			if (useUserOverride)
			{
				int localeInfoExInt = CultureData.GetLocaleInfoExInt(localeName, 4105U);
				if (localeInfoExInt != 0 && (CalendarId)localeInfoExInt != calendars[0])
				{
					CalendarId calendarId = (CalendarId)localeInfoExInt;
					for (int i = 1; i < calendars.Length; i++)
					{
						if (calendars[i] == calendarId)
						{
							CalendarId calendarId2 = calendars[0];
							calendars[0] = calendarId;
							calendars[i] = calendarId2;
							return num;
						}
					}
					num = ((num < calendars.Length) ? (num + 1) : num);
					int num2 = num;
					Span<CalendarId> span = new Span<CalendarId>(stackalloc byte[checked(unchecked((UIntPtr)num2) * 2)], num2);
					Span<CalendarId> span2 = span;
					*span2[0] = calendarId;
					calendars.AsSpan<CalendarId>().Slice(0, num - 1).CopyTo(span2.Slice(1));
					span2.CopyTo(calendars);
				}
			}
			return num;
		}

		// Token: 0x06001E13 RID: 7699 RVA: 0x0011EA98 File Offset: 0x0011DC98
		private static int NlsGetCalendars(string localeName, bool useUserOverride, CalendarId[] calendars)
		{
			CalendarData.NlsEnumCalendarsData nlsEnumCalendarsData = default(CalendarData.NlsEnumCalendarsData);
			nlsEnumCalendarsData.userOverride = 0;
			nlsEnumCalendarsData.calendars = new List<int>();
			if (useUserOverride)
			{
				int localeInfoExInt = CultureData.GetLocaleInfoExInt(localeName, 4105U);
				if (localeInfoExInt != 0)
				{
					nlsEnumCalendarsData.userOverride = localeInfoExInt;
					nlsEnumCalendarsData.calendars.Add(localeInfoExInt);
				}
			}
			Interop.Kernel32.EnumCalendarInfoExEx(ldftn(EnumCalendarsCallback), localeName, (Interop.BOOL)(-1), null, 1U, Unsafe.AsPointer<CalendarData.NlsEnumCalendarsData>(ref nlsEnumCalendarsData));
			for (int i = 0; i < Math.Min(calendars.Length, nlsEnumCalendarsData.calendars.Count); i++)
			{
				calendars[i] = (CalendarId)nlsEnumCalendarsData.calendars[i];
			}
			return nlsEnumCalendarsData.calendars.Count;
		}

		// Token: 0x04000687 RID: 1671
		internal string sNativeName;

		// Token: 0x04000688 RID: 1672
		internal string[] saShortDates;

		// Token: 0x04000689 RID: 1673
		internal string[] saYearMonths;

		// Token: 0x0400068A RID: 1674
		internal string[] saLongDates;

		// Token: 0x0400068B RID: 1675
		internal string sMonthDay;

		// Token: 0x0400068C RID: 1676
		internal string[] saEraNames;

		// Token: 0x0400068D RID: 1677
		internal string[] saAbbrevEraNames;

		// Token: 0x0400068E RID: 1678
		internal string[] saAbbrevEnglishEraNames;

		// Token: 0x0400068F RID: 1679
		internal string[] saDayNames;

		// Token: 0x04000690 RID: 1680
		internal string[] saAbbrevDayNames;

		// Token: 0x04000691 RID: 1681
		internal string[] saSuperShortDayNames;

		// Token: 0x04000692 RID: 1682
		internal string[] saMonthNames;

		// Token: 0x04000693 RID: 1683
		internal string[] saAbbrevMonthNames;

		// Token: 0x04000694 RID: 1684
		internal string[] saMonthGenitiveNames;

		// Token: 0x04000695 RID: 1685
		internal string[] saAbbrevMonthGenitiveNames;

		// Token: 0x04000696 RID: 1686
		internal string[] saLeapYearMonthNames;

		// Token: 0x04000697 RID: 1687
		internal int iTwoDigitYearMax = 2029;

		// Token: 0x04000698 RID: 1688
		private int iCurrentEra;

		// Token: 0x04000699 RID: 1689
		internal bool bUseUserOverrides;

		// Token: 0x0400069A RID: 1690
		internal static readonly CalendarData Invariant = CalendarData.CreateInvariant();

		// Token: 0x020001DD RID: 477
		private struct IcuEnumCalendarsData
		{
			// Token: 0x0400069B RID: 1691
			public List<string> Results;

			// Token: 0x0400069C RID: 1692
			public bool DisallowDuplicates;
		}

		// Token: 0x020001DE RID: 478
		private struct EnumData
		{
			// Token: 0x0400069D RID: 1693
			public string userOverride;

			// Token: 0x0400069E RID: 1694
			public List<string> strings;
		}

		// Token: 0x020001DF RID: 479
		public struct NlsEnumCalendarsData
		{
			// Token: 0x0400069F RID: 1695
			public int userOverride;

			// Token: 0x040006A0 RID: 1696
			public List<int> calendars;
		}
	}
}
