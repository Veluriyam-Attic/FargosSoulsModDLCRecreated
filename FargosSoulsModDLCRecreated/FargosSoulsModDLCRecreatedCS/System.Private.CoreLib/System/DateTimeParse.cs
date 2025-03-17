using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000111 RID: 273
	internal static class DateTimeParse
	{
		// Token: 0x06000E51 RID: 3665 RVA: 0x000D1920 File Offset: 0x000D0B20
		internal static DateTime ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref dateTimeResult))
			{
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000E52 RID: 3666 RVA: 0x000D1958 File Offset: 0x000D0B58
		internal static DateTime ParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style, out TimeSpan offset)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref dateTimeResult))
			{
				offset = dateTimeResult.timeZoneOffset;
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x000D19B0 File Offset: 0x000D0BB0
		internal static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				return true;
			}
			result = DateTime.MinValue;
			return false;
		}

		// Token: 0x06000E54 RID: 3668 RVA: 0x000D19F8 File Offset: 0x000D0BF8
		internal static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result, out TimeSpan offset)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParseExact(s, format, dtfi, style, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				offset = dateTimeResult.timeZoneOffset;
				return true;
			}
			result = DateTime.MinValue;
			offset = TimeSpan.Zero;
			return false;
		}

		// Token: 0x06000E55 RID: 3669 RVA: 0x000D1A67 File Offset: 0x000D0C67
		internal static bool TryParseExact(ReadOnlySpan<char> s, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, DateTimeStyles style, ref DateTimeResult result)
		{
			if (s.Length == 0)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDateTime");
				return false;
			}
			if (format.Length == 0)
			{
				result.SetBadFormatSpecifierFailure();
				return false;
			}
			return DateTimeParse.DoStrictParse(s, format, style, dtfi, ref result);
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x000D1AA0 File Offset: 0x000D0CA0
		internal static DateTime ParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref dateTimeResult))
			{
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x000D1AD8 File Offset: 0x000D0CD8
		internal static DateTime ParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out TimeSpan offset)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref dateTimeResult))
			{
				offset = dateTimeResult.timeZoneOffset;
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x000D1B30 File Offset: 0x000D0D30
		internal static bool TryParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result, out TimeSpan offset)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				offset = dateTimeResult.timeZoneOffset;
				return true;
			}
			result = DateTime.MinValue;
			offset = TimeSpan.Zero;
			return false;
		}

		// Token: 0x06000E59 RID: 3673 RVA: 0x000D1BA0 File Offset: 0x000D0DA0
		internal static bool TryParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, out DateTime result)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParseExactMultiple(s, formats, dtfi, style, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				return true;
			}
			result = DateTime.MinValue;
			return false;
		}

		// Token: 0x06000E5A RID: 3674 RVA: 0x000D1BE8 File Offset: 0x000D0DE8
		internal static bool TryParseExactMultiple(ReadOnlySpan<char> s, string[] formats, DateTimeFormatInfo dtfi, DateTimeStyles style, ref DateTimeResult result)
		{
			if (formats == null)
			{
				result.SetFailure(ParseFailureKind.ArgumentNull, "ArgumentNull_String", null, "formats");
				return false;
			}
			if (s.Length == 0)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDateTime");
				return false;
			}
			if (formats.Length == 0)
			{
				result.SetFailure(ParseFailureKind.Format, "Format_NoFormatSpecifier");
				return false;
			}
			for (int i = 0; i < formats.Length; i++)
			{
				if (formats[i] == null || formats[i].Length == 0)
				{
					result.SetBadFormatSpecifierFailure();
					return false;
				}
				DateTimeResult dateTimeResult = default(DateTimeResult);
				dateTimeResult.Init(s);
				dateTimeResult.flags = result.flags;
				if (DateTimeParse.TryParseExact(s, formats[i], dtfi, style, ref dateTimeResult))
				{
					result.parsedDate = dateTimeResult.parsedDate;
					result.timeZoneOffset = dateTimeResult.timeZoneOffset;
					return true;
				}
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E5B RID: 3675 RVA: 0x000D1CB4 File Offset: 0x000D0EB4
		private unsafe static bool MatchWord(ref __DTString str, string target)
		{
			if (target.Length > str.Value.Length - str.Index)
			{
				return false;
			}
			if (str.CompareInfo.Compare(str.Value.Slice(str.Index, target.Length), target, CompareOptions.IgnoreCase) != 0)
			{
				return false;
			}
			int num = str.Index + target.Length;
			if (num < str.Value.Length)
			{
				char c = (char)(*str.Value[num]);
				if (char.IsLetter(c))
				{
					return false;
				}
			}
			str.Index = num;
			if (str.Index < str.Length)
			{
				str.m_current = (char)(*str.Value[str.Index]);
			}
			return true;
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x000D1D6C File Offset: 0x000D0F6C
		private static bool GetTimeZoneName(ref __DTString str)
		{
			return DateTimeParse.MatchWord(ref str, "GMT") || DateTimeParse.MatchWord(ref str, "Z");
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x000D1D8D File Offset: 0x000D0F8D
		internal static bool IsDigit(char ch)
		{
			return ch - '0' <= '\t';
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x000D1D9C File Offset: 0x000D0F9C
		private static bool ParseFraction(ref __DTString str, out double result)
		{
			result = 0.0;
			double num = 0.1;
			int num2 = 0;
			char current;
			while (str.GetNext() && DateTimeParse.IsDigit(current = str.m_current))
			{
				result += (double)(current - '0') * num;
				num *= 0.1;
				num2++;
			}
			return num2 > 0;
		}

		// Token: 0x06000E5F RID: 3679 RVA: 0x000D1DFC File Offset: 0x000D0FFC
		private static bool ParseTimeZone(ref __DTString str, ref TimeSpan result)
		{
			int num = 0;
			DTSubString subString = str.GetSubString();
			if (subString.length != 1)
			{
				return false;
			}
			char c = subString[0];
			if (c != '+' && c != '-')
			{
				return false;
			}
			str.ConsumeSubString(subString);
			subString = str.GetSubString();
			if (subString.type != DTSubStringType.Number)
			{
				return false;
			}
			int value = subString.value;
			int length = subString.length;
			int hours;
			if (length == 1 || length == 2)
			{
				hours = value;
				str.ConsumeSubString(subString);
				subString = str.GetSubString();
				if (subString.length == 1 && subString[0] == ':')
				{
					str.ConsumeSubString(subString);
					subString = str.GetSubString();
					if (subString.type != DTSubStringType.Number || subString.length < 1 || subString.length > 2)
					{
						return false;
					}
					num = subString.value;
					str.ConsumeSubString(subString);
				}
			}
			else
			{
				if (length != 3 && length != 4)
				{
					return false;
				}
				hours = value / 100;
				num = value % 100;
				str.ConsumeSubString(subString);
			}
			if (num < 0 || num >= 60)
			{
				return false;
			}
			result = new TimeSpan(hours, num, 0);
			if (c == '-')
			{
				result = result.Negate();
			}
			return true;
		}

		// Token: 0x06000E60 RID: 3680 RVA: 0x000D1F14 File Offset: 0x000D1114
		private unsafe static bool HandleTimeZone(ref __DTString str, ref DateTimeResult result)
		{
			if (str.Index < str.Length - 1)
			{
				char c = (char)(*str.Value[str.Index]);
				int num = 0;
				while (char.IsWhiteSpace(c) && str.Index + num < str.Length - 1)
				{
					num++;
					c = (char)(*str.Value[str.Index + num]);
				}
				if (c == '+' || c == '-')
				{
					str.Index += num;
					if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0)
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					result.flags |= ParseFlags.TimeZoneUsed;
					if (!DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06000E61 RID: 3681 RVA: 0x000D1FD0 File Offset: 0x000D11D0
		private unsafe static bool Lex(DateTimeParse.DS dps, ref __DTString str, ref DateTimeToken dtok, ref DateTimeRawInfo raw, ref DateTimeResult result, ref DateTimeFormatInfo dtfi, DateTimeStyles styles)
		{
			dtok.dtt = DateTimeParse.DTT.Unk;
			TokenType tokenType;
			int num;
			str.GetRegularToken(out tokenType, out num, dtfi);
			switch (tokenType)
			{
			case TokenType.NumberToken:
			case TokenType.YearNumberToken:
				if (raw.numCount == 3 || num == -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (dps == DateTimeParse.DS.T_NNt && str.Index < str.Length - 1)
				{
					char c = (char)(*str.Value[str.Index]);
					if (c == '.')
					{
						DateTimeParse.ParseFraction(ref str, out raw.fraction);
					}
				}
				if ((dps == DateTimeParse.DS.T_NNt || dps == DateTimeParse.DS.T_Nt) && str.Index < str.Length - 1 && !DateTimeParse.HandleTimeZone(ref str, ref result))
				{
					return false;
				}
				dtok.num = num;
				if (tokenType != TokenType.YearNumberToken)
				{
					int index;
					char current;
					TokenType separatorToken;
					TokenType tokenType2 = separatorToken = str.GetSeparatorToken(dtfi, out index, out current);
					if (separatorToken > TokenType.SEP_YearSuff)
					{
						if (separatorToken <= TokenType.SEP_HourSuff)
						{
							if (separatorToken == TokenType.SEP_MonthSuff || separatorToken == TokenType.SEP_DaySuff)
							{
								dtok.dtt = DateTimeParse.DTT.NumDatesuff;
								dtok.suffix = tokenType2;
								break;
							}
							if (separatorToken != TokenType.SEP_HourSuff)
							{
								goto IL_59D;
							}
						}
						else if (separatorToken <= TokenType.SEP_SecondSuff)
						{
							if (separatorToken != TokenType.SEP_MinuteSuff && separatorToken != TokenType.SEP_SecondSuff)
							{
								goto IL_59D;
							}
						}
						else
						{
							if (separatorToken == TokenType.SEP_LocalTimeMark)
							{
								dtok.dtt = DateTimeParse.DTT.NumLocalTimeMark;
								raw.AddNumber(dtok.num);
								break;
							}
							if (separatorToken != TokenType.SEP_DateOrOffset)
							{
								goto IL_59D;
							}
							if (DateTimeParse.s_dateParsingStates[(int)dps][4] == DateTimeParse.DS.ERROR && DateTimeParse.s_dateParsingStates[(int)dps][3] > DateTimeParse.DS.ERROR)
							{
								str.Index = index;
								str.m_current = current;
								dtok.dtt = DateTimeParse.DTT.NumSpace;
							}
							else
							{
								dtok.dtt = DateTimeParse.DTT.NumDatesep;
							}
							raw.AddNumber(dtok.num);
							break;
						}
						dtok.dtt = DateTimeParse.DTT.NumTimesuff;
						dtok.suffix = tokenType2;
						break;
					}
					if (separatorToken <= TokenType.SEP_Am)
					{
						if (separatorToken == TokenType.SEP_End)
						{
							dtok.dtt = DateTimeParse.DTT.NumEnd;
							raw.AddNumber(dtok.num);
							break;
						}
						if (separatorToken == TokenType.SEP_Space)
						{
							dtok.dtt = DateTimeParse.DTT.NumSpace;
							raw.AddNumber(dtok.num);
							break;
						}
						if (separatorToken != TokenType.SEP_Am)
						{
							goto IL_59D;
						}
					}
					else if (separatorToken <= TokenType.SEP_Date)
					{
						if (separatorToken != TokenType.SEP_Pm)
						{
							if (separatorToken != TokenType.SEP_Date)
							{
								goto IL_59D;
							}
							dtok.dtt = DateTimeParse.DTT.NumDatesep;
							raw.AddNumber(dtok.num);
							break;
						}
					}
					else if (separatorToken != TokenType.SEP_Time)
					{
						if (separatorToken != TokenType.SEP_YearSuff)
						{
							goto IL_59D;
						}
						try
						{
							dtok.num = dtfi.Calendar.ToFourDigitYear(num);
						}
						catch (ArgumentOutOfRangeException)
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						dtok.dtt = DateTimeParse.DTT.NumDatesuff;
						dtok.suffix = tokenType2;
						break;
					}
					else
					{
						if (raw.hasSameDateAndTimeSeparators && (dps == DateTimeParse.DS.D_Y || dps == DateTimeParse.DS.D_YN || dps == DateTimeParse.DS.D_YNd || dps == DateTimeParse.DS.D_YM || dps == DateTimeParse.DS.D_YMd))
						{
							dtok.dtt = DateTimeParse.DTT.NumDatesep;
							raw.AddNumber(dtok.num);
							break;
						}
						dtok.dtt = DateTimeParse.DTT.NumTimesep;
						raw.AddNumber(dtok.num);
						break;
					}
					if (raw.timeMark != DateTimeParse.TM.NotSet)
					{
						result.SetBadDateTimeFailure();
						break;
					}
					raw.timeMark = ((tokenType2 == TokenType.SEP_Am) ? DateTimeParse.TM.AM : DateTimeParse.TM.PM);
					dtok.dtt = DateTimeParse.DTT.NumAmpm;
					if (dps == DateTimeParse.DS.D_NN && !DateTimeParse.ProcessTerminalState(DateTimeParse.DS.DX_NN, ref result, ref styles, ref raw, dtfi))
					{
						return false;
					}
					raw.AddNumber(dtok.num);
					if ((dps == DateTimeParse.DS.T_NNt || dps == DateTimeParse.DS.T_Nt) && !DateTimeParse.HandleTimeZone(ref str, ref result))
					{
						return false;
					}
					break;
					IL_59D:
					result.SetBadDateTimeFailure();
					return false;
				}
				if (raw.year == -1)
				{
					raw.year = num;
					int index;
					char current;
					TokenType separatorToken2;
					TokenType tokenType2 = separatorToken2 = str.GetSeparatorToken(dtfi, out index, out current);
					if (separatorToken2 <= TokenType.SEP_Time)
					{
						if (separatorToken2 <= TokenType.SEP_Am)
						{
							if (separatorToken2 == TokenType.SEP_End)
							{
								dtok.dtt = DateTimeParse.DTT.YearEnd;
								return true;
							}
							if (separatorToken2 == TokenType.SEP_Space)
							{
								dtok.dtt = DateTimeParse.DTT.YearSpace;
								return true;
							}
							if (separatorToken2 != TokenType.SEP_Am)
							{
								goto IL_2B7;
							}
						}
						else if (separatorToken2 != TokenType.SEP_Pm)
						{
							if (separatorToken2 == TokenType.SEP_Date)
							{
								dtok.dtt = DateTimeParse.DTT.YearDateSep;
								return true;
							}
							if (separatorToken2 != TokenType.SEP_Time)
							{
								goto IL_2B7;
							}
							if (!raw.hasSameDateAndTimeSeparators)
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							dtok.dtt = DateTimeParse.DTT.YearDateSep;
							return true;
						}
						if (raw.timeMark == DateTimeParse.TM.NotSet)
						{
							raw.timeMark = ((tokenType2 == TokenType.SEP_Am) ? DateTimeParse.TM.AM : DateTimeParse.TM.PM);
							dtok.dtt = DateTimeParse.DTT.YearSpace;
							return true;
						}
						result.SetBadDateTimeFailure();
						return true;
					}
					else
					{
						if (separatorToken2 > TokenType.SEP_DaySuff)
						{
							if (separatorToken2 <= TokenType.SEP_MinuteSuff)
							{
								if (separatorToken2 != TokenType.SEP_HourSuff && separatorToken2 != TokenType.SEP_MinuteSuff)
								{
									goto IL_2B7;
								}
							}
							else if (separatorToken2 != TokenType.SEP_SecondSuff)
							{
								if (separatorToken2 != TokenType.SEP_DateOrOffset)
								{
									goto IL_2B7;
								}
								if (DateTimeParse.s_dateParsingStates[(int)dps][13] == DateTimeParse.DS.ERROR && DateTimeParse.s_dateParsingStates[(int)dps][12] > DateTimeParse.DS.ERROR)
								{
									str.Index = index;
									str.m_current = current;
									dtok.dtt = DateTimeParse.DTT.YearSpace;
									return true;
								}
								dtok.dtt = DateTimeParse.DTT.YearDateSep;
								return true;
							}
							dtok.dtt = DateTimeParse.DTT.NumTimesuff;
							dtok.suffix = tokenType2;
							return true;
						}
						if (separatorToken2 == TokenType.SEP_YearSuff || separatorToken2 == TokenType.SEP_MonthSuff || separatorToken2 == TokenType.SEP_DaySuff)
						{
							dtok.dtt = DateTimeParse.DTT.NumDatesuff;
							dtok.suffix = tokenType2;
							return true;
						}
					}
					IL_2B7:
					result.SetBadDateTimeFailure();
					return false;
				}
				result.SetBadDateTimeFailure();
				return false;
			case TokenType.Am:
			case TokenType.Pm:
				if (raw.timeMark != DateTimeParse.TM.NotSet)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				raw.timeMark = (DateTimeParse.TM)num;
				break;
			case TokenType.MonthToken:
			{
				if (raw.month != -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				int index;
				char current;
				TokenType separatorToken3;
				TokenType tokenType2 = separatorToken3 = str.GetSeparatorToken(dtfi, out index, out current);
				if (separatorToken3 <= TokenType.SEP_Space)
				{
					if (separatorToken3 == TokenType.SEP_End)
					{
						dtok.dtt = DateTimeParse.DTT.MonthEnd;
						goto IL_7F6;
					}
					if (separatorToken3 == TokenType.SEP_Space)
					{
						dtok.dtt = DateTimeParse.DTT.MonthSpace;
						goto IL_7F6;
					}
				}
				else
				{
					if (separatorToken3 == TokenType.SEP_Date)
					{
						dtok.dtt = DateTimeParse.DTT.MonthDatesep;
						goto IL_7F6;
					}
					if (separatorToken3 != TokenType.SEP_Time)
					{
						if (separatorToken3 == TokenType.SEP_DateOrOffset)
						{
							if (DateTimeParse.s_dateParsingStates[(int)dps][8] == DateTimeParse.DS.ERROR && DateTimeParse.s_dateParsingStates[(int)dps][7] > DateTimeParse.DS.ERROR)
							{
								str.Index = index;
								str.m_current = current;
								dtok.dtt = DateTimeParse.DTT.MonthSpace;
								goto IL_7F6;
							}
							dtok.dtt = DateTimeParse.DTT.MonthDatesep;
							goto IL_7F6;
						}
					}
					else
					{
						if (!raw.hasSameDateAndTimeSeparators)
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						dtok.dtt = DateTimeParse.DTT.MonthDatesep;
						goto IL_7F6;
					}
				}
				result.SetBadDateTimeFailure();
				return false;
				IL_7F6:
				raw.month = num;
				break;
			}
			case TokenType.EndOfString:
				dtok.dtt = DateTimeParse.DTT.End;
				break;
			case TokenType.DayOfWeekToken:
				if (raw.dayOfWeek != -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				raw.dayOfWeek = num;
				dtok.dtt = DateTimeParse.DTT.DayOfWeek;
				break;
			case TokenType.TimeZoneToken:
				if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				dtok.dtt = DateTimeParse.DTT.TimeZone;
				result.flags |= ParseFlags.TimeZoneUsed;
				result.timeZoneOffset = new TimeSpan(0L);
				result.flags |= ParseFlags.TimeZoneUtc;
				break;
			case TokenType.EraToken:
				if (result.era == -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				result.era = num;
				dtok.dtt = DateTimeParse.DTT.Era;
				break;
			case TokenType.UnknownToken:
				if (char.IsLetter(str.m_current))
				{
					result.SetFailure(ParseFailureKind.FormatWithOriginalDateTimeAndParameter, "Format_UnknownDateTimeWord", str.Index);
					return false;
				}
				if ((str.m_current == '-' || str.m_current == '+') && (result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags)0)
				{
					int index2 = str.Index;
					if (DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
					{
						result.flags |= ParseFlags.TimeZoneUsed;
						return true;
					}
					str.Index = index2;
				}
				if (DateTimeParse.VerifyValidPunctuation(ref str))
				{
					return true;
				}
				result.SetBadDateTimeFailure();
				return false;
			case TokenType.HebrewNumber:
			{
				int index;
				char current;
				TokenType tokenType2;
				if (num < 100)
				{
					dtok.num = num;
					raw.AddNumber(dtok.num);
					TokenType separatorToken4;
					tokenType2 = (separatorToken4 = str.GetSeparatorToken(dtfi, out index, out current));
					if (separatorToken4 <= TokenType.SEP_Space)
					{
						if (separatorToken4 == TokenType.SEP_End)
						{
							dtok.dtt = DateTimeParse.DTT.NumEnd;
							break;
						}
						if (separatorToken4 != TokenType.SEP_Space)
						{
							goto IL_6F4;
						}
					}
					else if (separatorToken4 != TokenType.SEP_Date)
					{
						if (separatorToken4 != TokenType.SEP_DateOrOffset)
						{
							goto IL_6F4;
						}
						if (DateTimeParse.s_dateParsingStates[(int)dps][4] == DateTimeParse.DS.ERROR && DateTimeParse.s_dateParsingStates[(int)dps][3] > DateTimeParse.DS.ERROR)
						{
							str.Index = index;
							str.m_current = current;
							dtok.dtt = DateTimeParse.DTT.NumSpace;
							break;
						}
						dtok.dtt = DateTimeParse.DTT.NumDatesep;
						break;
					}
					dtok.dtt = DateTimeParse.DTT.NumDatesep;
					break;
					IL_6F4:
					result.SetBadDateTimeFailure();
					return false;
				}
				if (raw.year != -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				raw.year = num;
				TokenType separatorToken5;
				tokenType2 = (separatorToken5 = str.GetSeparatorToken(dtfi, out index, out current));
				if (separatorToken5 != TokenType.SEP_End)
				{
					if (separatorToken5 != TokenType.SEP_Space)
					{
						if (separatorToken5 == TokenType.SEP_DateOrOffset)
						{
							if (DateTimeParse.s_dateParsingStates[(int)dps][12] > DateTimeParse.DS.ERROR)
							{
								str.Index = index;
								str.m_current = current;
								dtok.dtt = DateTimeParse.DTT.YearSpace;
								break;
							}
						}
						result.SetBadDateTimeFailure();
						return false;
					}
					dtok.dtt = DateTimeParse.DTT.YearSpace;
				}
				else
				{
					dtok.dtt = DateTimeParse.DTT.YearEnd;
				}
				break;
			}
			case TokenType.JapaneseEraToken:
				result.calendar = JapaneseCalendar.GetDefaultInstance();
				dtfi = DateTimeFormatInfo.GetJapaneseCalendarDTFI();
				if (result.era == -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				result.era = num;
				dtok.dtt = DateTimeParse.DTT.Era;
				break;
			case TokenType.TEraToken:
				result.calendar = TaiwanCalendar.GetDefaultInstance();
				dtfi = DateTimeFormatInfo.GetTaiwanCalendarDTFI();
				if (result.era == -1)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				result.era = num;
				dtok.dtt = DateTimeParse.DTT.Era;
				break;
			}
			return true;
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x000D29AC File Offset: 0x000D1BAC
		private unsafe static bool VerifyValidPunctuation(ref __DTString str)
		{
			char c = (char)(*str.Value[str.Index]);
			if (c == '#')
			{
				bool flag = false;
				bool flag2 = false;
				for (int i = 0; i < str.Length; i++)
				{
					c = (char)(*str.Value[i]);
					if (c == '#')
					{
						if (flag)
						{
							if (flag2)
							{
								return false;
							}
							flag2 = true;
						}
						else
						{
							flag = true;
						}
					}
					else if (c == '\0')
					{
						if (!flag2)
						{
							return false;
						}
					}
					else if (!char.IsWhiteSpace(c) && (!flag || flag2))
					{
						return false;
					}
				}
				if (!flag2)
				{
					return false;
				}
				str.GetNext();
				return true;
			}
			else
			{
				if (c == '\0')
				{
					for (int j = str.Index; j < str.Length; j++)
					{
						if (*str.Value[j] != 0)
						{
							return false;
						}
					}
					str.Index = str.Length;
					return true;
				}
				return false;
			}
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x000D2A70 File Offset: 0x000D1C70
		private static bool GetYearMonthDayOrder(string datePattern, out int order)
		{
			int num = -1;
			int num2 = -1;
			int num3 = -1;
			int num4 = 0;
			bool flag = false;
			int num5 = 0;
			while (num5 < datePattern.Length && num4 < 3)
			{
				char c = datePattern[num5];
				if (c == '\\' || c == '%')
				{
					num5++;
				}
				else
				{
					if (c == '\'' || c == '"')
					{
						flag = !flag;
					}
					if (!flag)
					{
						if (c == 'y')
						{
							num = num4++;
							while (num5 + 1 < datePattern.Length)
							{
								if (datePattern[num5 + 1] != 'y')
								{
									break;
								}
								num5++;
							}
						}
						else if (c == 'M')
						{
							num2 = num4++;
							while (num5 + 1 < datePattern.Length)
							{
								if (datePattern[num5 + 1] != 'M')
								{
									break;
								}
								num5++;
							}
						}
						else if (c == 'd')
						{
							int num6 = 1;
							while (num5 + 1 < datePattern.Length && datePattern[num5 + 1] == 'd')
							{
								num6++;
								num5++;
							}
							if (num6 <= 2)
							{
								num3 = num4++;
							}
						}
					}
				}
				num5++;
			}
			if (num == 0 && num2 == 1 && num3 == 2)
			{
				order = 0;
				return true;
			}
			if (num2 == 0 && num3 == 1 && num == 2)
			{
				order = 1;
				return true;
			}
			if (num3 == 0 && num2 == 1 && num == 2)
			{
				order = 2;
				return true;
			}
			if (num == 0 && num3 == 1 && num2 == 2)
			{
				order = 3;
				return true;
			}
			order = -1;
			return false;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000D2BC4 File Offset: 0x000D1DC4
		private static bool GetYearMonthOrder(string pattern, out int order)
		{
			int num = -1;
			int num2 = -1;
			int num3 = 0;
			bool flag = false;
			int num4 = 0;
			while (num4 < pattern.Length && num3 < 2)
			{
				char c = pattern[num4];
				if (c == '\\' || c == '%')
				{
					num4++;
				}
				else
				{
					if (c == '\'' || c == '"')
					{
						flag = !flag;
					}
					if (!flag)
					{
						if (c == 'y')
						{
							num = num3++;
							while (num4 + 1 < pattern.Length)
							{
								if (pattern[num4 + 1] != 'y')
								{
									break;
								}
								num4++;
							}
						}
						else if (c == 'M')
						{
							num2 = num3++;
							while (num4 + 1 < pattern.Length && pattern[num4 + 1] == 'M')
							{
								num4++;
							}
						}
					}
				}
				num4++;
			}
			if (num == 0 && num2 == 1)
			{
				order = 4;
				return true;
			}
			if (num2 == 0 && num == 1)
			{
				order = 5;
				return true;
			}
			order = -1;
			return false;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x000D2CA4 File Offset: 0x000D1EA4
		private static bool GetMonthDayOrder(string pattern, out int order)
		{
			int num = -1;
			int num2 = -1;
			int num3 = 0;
			bool flag = false;
			int num4 = 0;
			while (num4 < pattern.Length && num3 < 2)
			{
				char c = pattern[num4];
				if (c == '\\' || c == '%')
				{
					num4++;
				}
				else
				{
					if (c == '\'' || c == '"')
					{
						flag = !flag;
					}
					if (!flag)
					{
						if (c == 'd')
						{
							int num5 = 1;
							while (num4 + 1 < pattern.Length && pattern[num4 + 1] == 'd')
							{
								num5++;
								num4++;
							}
							if (num5 <= 2)
							{
								num2 = num3++;
							}
						}
						else if (c == 'M')
						{
							num = num3++;
							while (num4 + 1 < pattern.Length && pattern[num4 + 1] == 'M')
							{
								num4++;
							}
						}
					}
				}
				num4++;
			}
			if (num == 0 && num2 == 1)
			{
				order = 6;
				return true;
			}
			if (num2 == 0 && num == 1)
			{
				order = 7;
				return true;
			}
			order = -1;
			return false;
		}

		// Token: 0x06000E66 RID: 3686 RVA: 0x000D2D98 File Offset: 0x000D1F98
		private static bool TryAdjustYear(ref DateTimeResult result, int year, out int adjustedYear)
		{
			if (year < 100)
			{
				try
				{
					year = result.calendar.ToFourDigitYear(year);
				}
				catch (ArgumentOutOfRangeException)
				{
					adjustedYear = -1;
					return false;
				}
			}
			adjustedYear = year;
			return true;
		}

		// Token: 0x06000E67 RID: 3687 RVA: 0x000D2DD8 File Offset: 0x000D1FD8
		private static bool SetDateYMD(ref DateTimeResult result, int year, int month, int day)
		{
			if (result.calendar.IsValidDay(year, month, day, result.era))
			{
				result.SetDate(year, month, day);
				return true;
			}
			return false;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x000D2DFC File Offset: 0x000D1FFC
		private static bool SetDateMDY(ref DateTimeResult result, int month, int day, int year)
		{
			return DateTimeParse.SetDateYMD(ref result, year, month, day);
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x000D2E07 File Offset: 0x000D2007
		private static bool SetDateDMY(ref DateTimeResult result, int day, int month, int year)
		{
			return DateTimeParse.SetDateYMD(ref result, year, month, day);
		}

		// Token: 0x06000E6A RID: 3690 RVA: 0x000D2E12 File Offset: 0x000D2012
		private static bool SetDateYDM(ref DateTimeResult result, int year, int day, int month)
		{
			return DateTimeParse.SetDateYMD(ref result, year, month, day);
		}

		// Token: 0x06000E6B RID: 3691 RVA: 0x000D2E1D File Offset: 0x000D201D
		private static void GetDefaultYear(ref DateTimeResult result, ref DateTimeStyles styles)
		{
			result.Year = result.calendar.GetYear(DateTimeParse.GetDateTimeNow(ref result, ref styles));
			result.flags |= ParseFlags.YearDefault;
		}

		// Token: 0x06000E6C RID: 3692 RVA: 0x000D2E48 File Offset: 0x000D2048
		private static bool GetDayOfNN(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			DateTimeParse.GetDefaultYear(ref result, ref styles);
			int num;
			if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.MonthDayPattern);
				return false;
			}
			if (num == 6)
			{
				if (DateTimeParse.SetDateYMD(ref result, result.Year, number, number2))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (DateTimeParse.SetDateYMD(ref result, result.Year, number2, number))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E6D RID: 3693 RVA: 0x000D2EF4 File Offset: 0x000D20F4
		private static bool GetDayOfNNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			int number3 = raw.GetNumber(2);
			int num;
			if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.ShortDatePattern);
				return false;
			}
			int year;
			if (num == 0)
			{
				if (DateTimeParse.TryAdjustYear(ref result, number, out year) && DateTimeParse.SetDateYMD(ref result, year, number2, number3))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 1)
			{
				if (DateTimeParse.TryAdjustYear(ref result, number3, out year) && DateTimeParse.SetDateMDY(ref result, number, number2, year))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 2)
			{
				if (DateTimeParse.TryAdjustYear(ref result, number3, out year) && DateTimeParse.SetDateDMY(ref result, number, number2, year))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 3 && DateTimeParse.TryAdjustYear(ref result, number, out year) && DateTimeParse.SetDateYDM(ref result, year, number2, number3))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E6E RID: 3694 RVA: 0x000D3010 File Offset: 0x000D2210
		private static bool GetDayOfMN(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int num;
			if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.MonthDayPattern);
				return false;
			}
			if (num == 7)
			{
				int num2;
				if (!DateTimeParse.GetYearMonthOrder(dtfi.YearMonthPattern, out num2))
				{
					result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.YearMonthPattern);
					return false;
				}
				if (num2 == 5)
				{
					int year;
					if (!DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out year) || !DateTimeParse.SetDateYMD(ref result, year, raw.month, 1))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					return true;
				}
			}
			DateTimeParse.GetDefaultYear(ref result, ref styles);
			if (!DateTimeParse.SetDateYMD(ref result, result.Year, raw.month, raw.GetNumber(0)))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			return true;
		}

		// Token: 0x06000E6F RID: 3695 RVA: 0x000D30D8 File Offset: 0x000D22D8
		private static bool GetHebrewDayOfNM(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			int num;
			if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.MonthDayPattern);
				return false;
			}
			result.Month = raw.month;
			if ((num == 7 || num == 6) && result.calendar.IsValidDay(result.Year, result.Month, raw.GetNumber(0), result.era))
			{
				result.Day = raw.GetNumber(0);
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E70 RID: 3696 RVA: 0x000D3158 File Offset: 0x000D2358
		private static bool GetDayOfNM(ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int num;
			if (!DateTimeParse.GetMonthDayOrder(dtfi.MonthDayPattern, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.MonthDayPattern);
				return false;
			}
			if (num == 6)
			{
				int num2;
				if (!DateTimeParse.GetYearMonthOrder(dtfi.YearMonthPattern, out num2))
				{
					result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.YearMonthPattern);
					return false;
				}
				if (num2 == 4)
				{
					int year;
					if (!DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out year) || !DateTimeParse.SetDateYMD(ref result, year, raw.month, 1))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					return true;
				}
			}
			DateTimeParse.GetDefaultYear(ref result, ref styles);
			if (!DateTimeParse.SetDateYMD(ref result, result.Year, raw.month, raw.GetNumber(0)))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			return true;
		}

		// Token: 0x06000E71 RID: 3697 RVA: 0x000D3220 File Offset: 0x000D2420
		private static bool GetDayOfMNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			int num;
			if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.ShortDatePattern);
				return false;
			}
			if (num == 1)
			{
				int year;
				if (DateTimeParse.TryAdjustYear(ref result, number2, out year) && result.calendar.IsValidDay(year, raw.month, number, result.era))
				{
					result.SetDate(year, raw.month, number);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
				if (DateTimeParse.TryAdjustYear(ref result, number, out year) && result.calendar.IsValidDay(year, raw.month, number2, result.era))
				{
					result.SetDate(year, raw.month, number2);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 0)
			{
				int year;
				if (DateTimeParse.TryAdjustYear(ref result, number, out year) && result.calendar.IsValidDay(year, raw.month, number2, result.era))
				{
					result.SetDate(year, raw.month, number2);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
				if (DateTimeParse.TryAdjustYear(ref result, number2, out year) && result.calendar.IsValidDay(year, raw.month, number, result.era))
				{
					result.SetDate(year, raw.month, number);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (num == 2)
			{
				int year;
				if (DateTimeParse.TryAdjustYear(ref result, number2, out year) && result.calendar.IsValidDay(year, raw.month, number, result.era))
				{
					result.SetDate(year, raw.month, number);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
				if (DateTimeParse.TryAdjustYear(ref result, number, out year) && result.calendar.IsValidDay(year, raw.month, number2, result.era))
				{
					result.SetDate(year, raw.month, number2);
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E72 RID: 3698 RVA: 0x000D343C File Offset: 0x000D263C
		private static bool GetDayOfYNN(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			string shortDatePattern = dtfi.ShortDatePattern;
			int num;
			if (DateTimeParse.GetYearMonthDayOrder(shortDatePattern, out num) && num == 3)
			{
				if (DateTimeParse.SetDateYMD(ref result, raw.year, number2, number))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (DateTimeParse.SetDateYMD(ref result, raw.year, number, number2))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E73 RID: 3699 RVA: 0x000D34D0 File Offset: 0x000D26D0
		private static bool GetDayOfNNY(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int number = raw.GetNumber(0);
			int number2 = raw.GetNumber(1);
			int num;
			if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, out num))
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.ShortDatePattern);
				return false;
			}
			if (num == 1 || num == 0)
			{
				if (DateTimeParse.SetDateYMD(ref result, raw.year, number, number2))
				{
					result.flags |= ParseFlags.HaveDate;
					return true;
				}
			}
			else if (DateTimeParse.SetDateYMD(ref result, raw.year, number2, number))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E74 RID: 3700 RVA: 0x000D3578 File Offset: 0x000D2778
		private static bool GetDayOfYMN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.month, raw.GetNumber(0)))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E75 RID: 3701 RVA: 0x000D35D0 File Offset: 0x000D27D0
		private static bool GetDayOfYN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.GetNumber(0), 1))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E76 RID: 3702 RVA: 0x000D3624 File Offset: 0x000D2824
		private static bool GetDayOfYM(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveDate) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (DateTimeParse.SetDateYMD(ref result, raw.year, raw.month, 1))
			{
				result.flags |= ParseFlags.HaveDate;
				return true;
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E77 RID: 3703 RVA: 0x000D3674 File Offset: 0x000D2874
		private static void AdjustTimeMark(DateTimeFormatInfo dtfi, ref DateTimeRawInfo raw)
		{
			if (raw.timeMark == DateTimeParse.TM.NotSet && dtfi.AMDesignator != null && dtfi.PMDesignator != null)
			{
				if (dtfi.AMDesignator.Length == 0 && dtfi.PMDesignator.Length != 0)
				{
					raw.timeMark = DateTimeParse.TM.AM;
				}
				if (dtfi.PMDesignator.Length == 0 && dtfi.AMDesignator.Length != 0)
				{
					raw.timeMark = DateTimeParse.TM.PM;
				}
			}
		}

		// Token: 0x06000E78 RID: 3704 RVA: 0x000D36DC File Offset: 0x000D28DC
		private static bool AdjustHour(ref int hour, DateTimeParse.TM timeMark)
		{
			if (timeMark != DateTimeParse.TM.NotSet)
			{
				if (timeMark == DateTimeParse.TM.AM)
				{
					if (hour < 0 || hour > 12)
					{
						return false;
					}
					hour = ((hour == 12) ? 0 : hour);
				}
				else
				{
					if (hour < 0 || hour > 23)
					{
						return false;
					}
					if (hour < 12)
					{
						hour += 12;
					}
				}
			}
			return true;
		}

		// Token: 0x06000E79 RID: 3705 RVA: 0x000D371C File Offset: 0x000D291C
		private static bool GetTimeOfN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveTime) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (raw.timeMark == DateTimeParse.TM.NotSet)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Hour = raw.GetNumber(0);
			result.flags |= ParseFlags.HaveTime;
			return true;
		}

		// Token: 0x06000E7A RID: 3706 RVA: 0x000D375C File Offset: 0x000D295C
		private static bool GetTimeOfNN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveTime) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Hour = raw.GetNumber(0);
			result.Minute = raw.GetNumber(1);
			result.flags |= ParseFlags.HaveTime;
			return true;
		}

		// Token: 0x06000E7B RID: 3707 RVA: 0x000D3798 File Offset: 0x000D2998
		private static bool GetTimeOfNNN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if ((result.flags & ParseFlags.HaveTime) != (ParseFlags)0)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Hour = raw.GetNumber(0);
			result.Minute = raw.GetNumber(1);
			result.Second = raw.GetNumber(2);
			result.flags |= ParseFlags.HaveTime;
			return true;
		}

		// Token: 0x06000E7C RID: 3708 RVA: 0x000D37EC File Offset: 0x000D29EC
		private static bool GetDateOfDSN(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if (raw.numCount != 1 || result.Day != -1)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Day = raw.GetNumber(0);
			return true;
		}

		// Token: 0x06000E7D RID: 3709 RVA: 0x000D3818 File Offset: 0x000D2A18
		private static bool GetDateOfNDS(ref DateTimeResult result, ref DateTimeRawInfo raw)
		{
			if (result.Month == -1)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (result.Year != -1)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (!DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out result.Year))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.Day = 1;
			return true;
		}

		// Token: 0x06000E7E RID: 3710 RVA: 0x000D386C File Offset: 0x000D2A6C
		private static bool GetDateOfNNDS(ref DateTimeResult result, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			if ((result.flags & ParseFlags.HaveYear) != (ParseFlags)0)
			{
				if ((result.flags & ParseFlags.HaveMonth) == (ParseFlags)0 && (result.flags & ParseFlags.HaveDay) == (ParseFlags)0 && DateTimeParse.TryAdjustYear(ref result, raw.year, out result.Year) && DateTimeParse.SetDateYMD(ref result, result.Year, raw.GetNumber(0), raw.GetNumber(1)))
				{
					return true;
				}
			}
			else if ((result.flags & ParseFlags.HaveMonth) != (ParseFlags)0 && (result.flags & ParseFlags.HaveYear) == (ParseFlags)0 && (result.flags & ParseFlags.HaveDay) == (ParseFlags)0)
			{
				int num;
				if (!DateTimeParse.GetYearMonthDayOrder(dtfi.ShortDatePattern, out num))
				{
					result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDatePattern", dtfi.ShortDatePattern);
					return false;
				}
				int year;
				if (num == 0)
				{
					if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(0), out year) && DateTimeParse.SetDateYMD(ref result, year, result.Month, raw.GetNumber(1)))
					{
						return true;
					}
				}
				else if (DateTimeParse.TryAdjustYear(ref result, raw.GetNumber(1), out year) && DateTimeParse.SetDateYMD(ref result, year, result.Month, raw.GetNumber(0)))
				{
					return true;
				}
			}
			result.SetBadDateTimeFailure();
			return false;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x000D3978 File Offset: 0x000D2B78
		private static bool ProcessDateTimeSuffix(ref DateTimeResult result, ref DateTimeRawInfo raw, ref DateTimeToken dtok)
		{
			TokenType suffix = dtok.suffix;
			if (suffix <= TokenType.SEP_DaySuff)
			{
				if (suffix != TokenType.SEP_YearSuff)
				{
					if (suffix != TokenType.SEP_MonthSuff)
					{
						if (suffix == TokenType.SEP_DaySuff)
						{
							if ((result.flags & ParseFlags.HaveDay) != (ParseFlags)0)
							{
								return false;
							}
							result.flags |= ParseFlags.HaveDay;
							result.Day = dtok.num;
						}
					}
					else
					{
						if ((result.flags & ParseFlags.HaveMonth) != (ParseFlags)0)
						{
							return false;
						}
						result.flags |= ParseFlags.HaveMonth;
						result.Month = (raw.month = dtok.num);
					}
				}
				else
				{
					if ((result.flags & ParseFlags.HaveYear) != (ParseFlags)0)
					{
						return false;
					}
					result.flags |= ParseFlags.HaveYear;
					result.Year = (raw.year = dtok.num);
				}
			}
			else if (suffix != TokenType.SEP_HourSuff)
			{
				if (suffix != TokenType.SEP_MinuteSuff)
				{
					if (suffix == TokenType.SEP_SecondSuff)
					{
						if ((result.flags & ParseFlags.HaveSecond) != (ParseFlags)0)
						{
							return false;
						}
						result.flags |= ParseFlags.HaveSecond;
						result.Second = dtok.num;
					}
				}
				else
				{
					if ((result.flags & ParseFlags.HaveMinute) != (ParseFlags)0)
					{
						return false;
					}
					result.flags |= ParseFlags.HaveMinute;
					result.Minute = dtok.num;
				}
			}
			else
			{
				if ((result.flags & ParseFlags.HaveHour) != (ParseFlags)0)
				{
					return false;
				}
				result.flags |= ParseFlags.HaveHour;
				result.Hour = dtok.num;
			}
			return true;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x000D3AD4 File Offset: 0x000D2CD4
		internal static bool ProcessHebrewTerminalState(DateTimeParse.DS dps, ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			switch (dps)
			{
			case DateTimeParse.DS.DX_MN:
			case DateTimeParse.DS.DX_NM:
				DateTimeParse.GetDefaultYear(ref result, ref styles);
				if (!dtfi.YearMonthAdjustment(ref result.Year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
					return false;
				}
				if (!DateTimeParse.GetHebrewDayOfNM(ref result, ref raw, dtfi))
				{
					return false;
				}
				goto IL_1A1;
			case DateTimeParse.DS.DX_MNN:
				raw.year = raw.GetNumber(1);
				if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
					return false;
				}
				if (!DateTimeParse.GetDayOfMNN(ref result, ref raw, dtfi))
				{
					return false;
				}
				goto IL_1A1;
			case DateTimeParse.DS.DX_YMN:
				if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
					return false;
				}
				if (!DateTimeParse.GetDayOfYMN(ref result, ref raw))
				{
					return false;
				}
				goto IL_1A1;
			case DateTimeParse.DS.DX_YM:
				if (!dtfi.YearMonthAdjustment(ref raw.year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
					return false;
				}
				if (!DateTimeParse.GetDayOfYM(ref result, ref raw))
				{
					return false;
				}
				goto IL_1A1;
			case DateTimeParse.DS.TX_N:
				if (!DateTimeParse.GetTimeOfN(ref result, ref raw))
				{
					return false;
				}
				goto IL_1A1;
			case DateTimeParse.DS.TX_NN:
				if (!DateTimeParse.GetTimeOfNN(ref result, ref raw))
				{
					return false;
				}
				goto IL_1A1;
			case DateTimeParse.DS.TX_NNN:
				if (!DateTimeParse.GetTimeOfNNN(ref result, ref raw))
				{
					return false;
				}
				goto IL_1A1;
			case DateTimeParse.DS.DX_NNY:
				if (raw.year < 1000)
				{
					raw.year += 5000;
				}
				if (!DateTimeParse.GetDayOfNNY(ref result, ref raw, dtfi))
				{
					return false;
				}
				if (!dtfi.YearMonthAdjustment(ref result.Year, ref raw.month, true))
				{
					result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
					return false;
				}
				goto IL_1A1;
			}
			result.SetBadDateTimeFailure();
			return false;
			IL_1A1:
			if (dps > DateTimeParse.DS.ERROR)
			{
				raw.numCount = 0;
			}
			return true;
		}

		// Token: 0x06000E81 RID: 3713 RVA: 0x000D3C90 File Offset: 0x000D2E90
		internal static bool ProcessTerminalState(DateTimeParse.DS dps, ref DateTimeResult result, ref DateTimeStyles styles, ref DateTimeRawInfo raw, DateTimeFormatInfo dtfi)
		{
			bool flag = true;
			switch (dps)
			{
			case DateTimeParse.DS.DX_NN:
				flag = DateTimeParse.GetDayOfNN(ref result, ref styles, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_NNN:
				flag = DateTimeParse.GetDayOfNNN(ref result, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_MN:
				flag = DateTimeParse.GetDayOfMN(ref result, ref styles, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_NM:
				flag = DateTimeParse.GetDayOfNM(ref result, ref styles, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_MNN:
				flag = DateTimeParse.GetDayOfMNN(ref result, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_DS:
				flag = true;
				break;
			case DateTimeParse.DS.DX_DSN:
				flag = DateTimeParse.GetDateOfDSN(ref result, ref raw);
				break;
			case DateTimeParse.DS.DX_NDS:
				flag = DateTimeParse.GetDateOfNDS(ref result, ref raw);
				break;
			case DateTimeParse.DS.DX_NNDS:
				flag = DateTimeParse.GetDateOfNNDS(ref result, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_YNN:
				flag = DateTimeParse.GetDayOfYNN(ref result, ref raw, dtfi);
				break;
			case DateTimeParse.DS.DX_YMN:
				flag = DateTimeParse.GetDayOfYMN(ref result, ref raw);
				break;
			case DateTimeParse.DS.DX_YN:
				flag = DateTimeParse.GetDayOfYN(ref result, ref raw);
				break;
			case DateTimeParse.DS.DX_YM:
				flag = DateTimeParse.GetDayOfYM(ref result, ref raw);
				break;
			case DateTimeParse.DS.TX_N:
				flag = DateTimeParse.GetTimeOfN(ref result, ref raw);
				break;
			case DateTimeParse.DS.TX_NN:
				flag = DateTimeParse.GetTimeOfNN(ref result, ref raw);
				break;
			case DateTimeParse.DS.TX_NNN:
				flag = DateTimeParse.GetTimeOfNNN(ref result, ref raw);
				break;
			case DateTimeParse.DS.TX_TS:
				flag = true;
				break;
			case DateTimeParse.DS.DX_NNY:
				flag = DateTimeParse.GetDayOfNNY(ref result, ref raw, dtfi);
				break;
			}
			if (!flag)
			{
				return false;
			}
			if (dps > DateTimeParse.DS.ERROR)
			{
				raw.numCount = 0;
			}
			return true;
		}

		// Token: 0x06000E82 RID: 3714 RVA: 0x000D3DCC File Offset: 0x000D2FCC
		internal static DateTime Parse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParse(s, dtfi, styles, ref dateTimeResult))
			{
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000E83 RID: 3715 RVA: 0x000D3E04 File Offset: 0x000D3004
		internal static DateTime Parse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out TimeSpan offset)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParse(s, dtfi, styles, ref dateTimeResult))
			{
				offset = dateTimeResult.timeZoneOffset;
				return dateTimeResult.parsedDate;
			}
			throw DateTimeParse.GetDateTimeParseException(ref dateTimeResult);
		}

		// Token: 0x06000E84 RID: 3716 RVA: 0x000D3E58 File Offset: 0x000D3058
		internal static bool TryParse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out DateTime result)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			if (DateTimeParse.TryParse(s, dtfi, styles, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				return true;
			}
			result = DateTime.MinValue;
			return false;
		}

		// Token: 0x06000E85 RID: 3717 RVA: 0x000D3E9C File Offset: 0x000D309C
		internal static bool TryParse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles, out DateTime result, out TimeSpan offset)
		{
			DateTimeResult dateTimeResult = default(DateTimeResult);
			dateTimeResult.Init(s);
			dateTimeResult.flags |= ParseFlags.CaptureOffset;
			if (DateTimeParse.TryParse(s, dtfi, styles, ref dateTimeResult))
			{
				result = dateTimeResult.parsedDate;
				offset = dateTimeResult.timeZoneOffset;
				return true;
			}
			result = DateTime.MinValue;
			offset = TimeSpan.Zero;
			return false;
		}

		// Token: 0x06000E86 RID: 3718 RVA: 0x000D3F08 File Offset: 0x000D3108
		internal unsafe static bool TryParse(ReadOnlySpan<char> s, DateTimeFormatInfo dtfi, DateTimeStyles styles, ref DateTimeResult result)
		{
			if (s.Length == 0)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadDateTime");
				return false;
			}
			DateTimeParse.DS ds = DateTimeParse.DS.BEGIN;
			bool flag = false;
			DateTimeToken dateTimeToken = default(DateTimeToken);
			dateTimeToken.suffix = TokenType.SEP_Unk;
			DateTimeRawInfo dateTimeRawInfo = default(DateTimeRawInfo);
			int* numberBuffer = stackalloc int[(UIntPtr)12];
			dateTimeRawInfo.Init(numberBuffer);
			dateTimeRawInfo.hasSameDateAndTimeSeparators = dtfi.DateSeparator.Equals(dtfi.TimeSeparator, StringComparison.Ordinal);
			result.calendar = dtfi.Calendar;
			result.era = 0;
			__DTString _DTString = new __DTString(s, dtfi);
			_DTString.GetNext();
			while (DateTimeParse.Lex(ds, ref _DTString, ref dateTimeToken, ref dateTimeRawInfo, ref result, ref dtfi, styles))
			{
				if (dateTimeToken.dtt != DateTimeParse.DTT.Unk)
				{
					if (dateTimeToken.suffix != TokenType.SEP_Unk)
					{
						if (!DateTimeParse.ProcessDateTimeSuffix(ref result, ref dateTimeRawInfo, ref dateTimeToken))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						dateTimeToken.suffix = TokenType.SEP_Unk;
					}
					if (dateTimeToken.dtt == DateTimeParse.DTT.NumLocalTimeMark)
					{
						if (ds == DateTimeParse.DS.D_YNd || ds == DateTimeParse.DS.D_YN)
						{
							return DateTimeParse.ParseISO8601(ref dateTimeRawInfo, ref _DTString, styles, ref result);
						}
						result.SetBadDateTimeFailure();
						return false;
					}
					else
					{
						if (dateTimeRawInfo.hasSameDateAndTimeSeparators)
						{
							if (dateTimeToken.dtt == DateTimeParse.DTT.YearEnd || dateTimeToken.dtt == DateTimeParse.DTT.YearSpace || dateTimeToken.dtt == DateTimeParse.DTT.YearDateSep)
							{
								if (ds == DateTimeParse.DS.T_Nt)
								{
									ds = DateTimeParse.DS.D_Nd;
								}
								if (ds == DateTimeParse.DS.T_NNt)
								{
									ds = DateTimeParse.DS.D_NNd;
								}
							}
							bool flag2 = _DTString.AtEnd();
							if (DateTimeParse.s_dateParsingStates[(int)ds][(int)dateTimeToken.dtt] == DateTimeParse.DS.ERROR || flag2)
							{
								DateTimeParse.DTT dtt = dateTimeToken.dtt;
								switch (dtt)
								{
								case DateTimeParse.DTT.NumDatesep:
									dateTimeToken.dtt = (flag2 ? DateTimeParse.DTT.NumEnd : DateTimeParse.DTT.NumSpace);
									break;
								case DateTimeParse.DTT.NumTimesep:
									dateTimeToken.dtt = (flag2 ? DateTimeParse.DTT.NumEnd : DateTimeParse.DTT.NumSpace);
									break;
								case DateTimeParse.DTT.MonthEnd:
								case DateTimeParse.DTT.MonthSpace:
									break;
								case DateTimeParse.DTT.MonthDatesep:
									dateTimeToken.dtt = (flag2 ? DateTimeParse.DTT.MonthEnd : DateTimeParse.DTT.MonthSpace);
									break;
								default:
									if (dtt == DateTimeParse.DTT.YearDateSep)
									{
										dateTimeToken.dtt = (flag2 ? DateTimeParse.DTT.YearEnd : DateTimeParse.DTT.YearSpace);
									}
									break;
								}
							}
						}
						ds = DateTimeParse.s_dateParsingStates[(int)ds][(int)dateTimeToken.dtt];
						if (ds == DateTimeParse.DS.ERROR)
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						if (ds > DateTimeParse.DS.ERROR)
						{
							if ((dtfi.FormatFlags & DateTimeFormatFlags.UseHebrewRule) != DateTimeFormatFlags.None)
							{
								if (!DateTimeParse.ProcessHebrewTerminalState(ds, ref result, ref styles, ref dateTimeRawInfo, dtfi))
								{
									return false;
								}
							}
							else if (!DateTimeParse.ProcessTerminalState(ds, ref result, ref styles, ref dateTimeRawInfo, dtfi))
							{
								return false;
							}
							flag = true;
							ds = DateTimeParse.DS.BEGIN;
						}
					}
				}
				if (dateTimeToken.dtt == DateTimeParse.DTT.End || dateTimeToken.dtt == DateTimeParse.DTT.NumEnd || dateTimeToken.dtt == DateTimeParse.DTT.MonthEnd)
				{
					if (!flag)
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					DateTimeParse.AdjustTimeMark(dtfi, ref dateTimeRawInfo);
					if (!DateTimeParse.AdjustHour(ref result.Hour, dateTimeRawInfo.timeMark))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					bool bTimeOnly = result.Year == -1 && result.Month == -1 && result.Day == -1;
					if (!DateTimeParse.CheckDefaultDateTime(ref result, ref result.calendar, styles))
					{
						return false;
					}
					DateTime dateTime;
					if (!result.calendar.TryToDateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0, result.era, out dateTime))
					{
						result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
						return false;
					}
					if (dateTimeRawInfo.fraction > 0.0 && !dateTime.TryAddTicks((long)Math.Round(dateTimeRawInfo.fraction * 10000000.0), out dateTime))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if (dateTimeRawInfo.dayOfWeek != -1 && dateTimeRawInfo.dayOfWeek != (int)result.calendar.GetDayOfWeek(dateTime))
					{
						result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "Format_BadDayOfWeek");
						return false;
					}
					result.parsedDate = dateTime;
					return DateTimeParse.DetermineTimeZoneAdjustments(ref result, styles, bTimeOnly);
				}
			}
			return false;
		}

		// Token: 0x06000E87 RID: 3719 RVA: 0x000D4264 File Offset: 0x000D3464
		private static bool DetermineTimeZoneAdjustments(ref DateTimeResult result, DateTimeStyles styles, bool bTimeOnly)
		{
			if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0)
			{
				return DateTimeParse.DateTimeOffsetTimeZonePostProcessing(ref result, styles);
			}
			long ticks = result.timeZoneOffset.Ticks;
			if (ticks < -504000000000L || ticks > 504000000000L)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "Format_OffsetOutOfRange");
				return false;
			}
			if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags)0)
			{
				if ((styles & DateTimeStyles.AssumeLocal) != DateTimeStyles.None)
				{
					if ((styles & DateTimeStyles.AdjustToUniversal) == DateTimeStyles.None)
					{
						result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Local);
						return true;
					}
					result.flags |= ParseFlags.TimeZoneUsed;
					result.timeZoneOffset = TimeZoneInfo.GetLocalUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
				else
				{
					if ((styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None)
					{
						return true;
					}
					if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
					{
						result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Utc);
						return true;
					}
					result.flags |= ParseFlags.TimeZoneUsed;
					result.timeZoneOffset = TimeSpan.Zero;
				}
			}
			if ((styles & DateTimeStyles.RoundtripKind) != DateTimeStyles.None && (result.flags & ParseFlags.TimeZoneUtc) != (ParseFlags)0)
			{
				result.parsedDate = DateTime.SpecifyKind(result.parsedDate, DateTimeKind.Utc);
				return true;
			}
			if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
			{
				return DateTimeParse.AdjustTimeZoneToUniversal(ref result);
			}
			return DateTimeParse.AdjustTimeZoneToLocal(ref result, bTimeOnly);
		}

		// Token: 0x06000E88 RID: 3720 RVA: 0x000D438C File Offset: 0x000D358C
		private static bool DateTimeOffsetTimeZonePostProcessing(ref DateTimeResult result, DateTimeStyles styles)
		{
			if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags)0)
			{
				if ((styles & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
				{
					result.timeZoneOffset = TimeSpan.Zero;
				}
				else
				{
					result.timeZoneOffset = TimeZoneInfo.GetLocalUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
			}
			long ticks = result.timeZoneOffset.Ticks;
			long num = result.parsedDate.Ticks - ticks;
			if (num < 0L || num > 3155378975999999999L)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "Format_UTCOutOfRange");
				return false;
			}
			if (ticks < -504000000000L || ticks > 504000000000L)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "Format_OffsetOutOfRange");
				return false;
			}
			if ((styles & DateTimeStyles.AdjustToUniversal) != DateTimeStyles.None)
			{
				if ((result.flags & ParseFlags.TimeZoneUsed) == (ParseFlags)0 && (styles & DateTimeStyles.AssumeUniversal) == DateTimeStyles.None)
				{
					bool result2 = DateTimeParse.AdjustTimeZoneToUniversal(ref result);
					result.timeZoneOffset = TimeSpan.Zero;
					return result2;
				}
				result.parsedDate = new DateTime(num, DateTimeKind.Utc);
				result.timeZoneOffset = TimeSpan.Zero;
			}
			return true;
		}

		// Token: 0x06000E89 RID: 3721 RVA: 0x000D4474 File Offset: 0x000D3674
		private static bool AdjustTimeZoneToUniversal(ref DateTimeResult result)
		{
			long num = result.parsedDate.Ticks;
			num -= result.timeZoneOffset.Ticks;
			if (num < 0L)
			{
				num += 864000000000L;
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "Format_DateOutOfRange");
				return false;
			}
			result.parsedDate = new DateTime(num, DateTimeKind.Utc);
			return true;
		}

		// Token: 0x06000E8A RID: 3722 RVA: 0x000D44DC File Offset: 0x000D36DC
		private static bool AdjustTimeZoneToLocal(ref DateTimeResult result, bool bTimeOnly)
		{
			long num = result.parsedDate.Ticks;
			TimeZoneInfo local = TimeZoneInfo.Local;
			bool isAmbiguousDst = false;
			if (num < 864000000000L)
			{
				num -= result.timeZoneOffset.Ticks;
				num += local.GetUtcOffset(bTimeOnly ? DateTime.Now : result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
				if (num < 0L)
				{
					num += 864000000000L;
				}
			}
			else
			{
				num -= result.timeZoneOffset.Ticks;
				if (num < 0L || num > 3155378975999999999L)
				{
					num += local.GetUtcOffset(result.parsedDate, TimeZoneInfoOptions.NoThrowOnInvalidTime).Ticks;
				}
				else
				{
					DateTime time = new DateTime(num, DateTimeKind.Utc);
					bool flag;
					num += TimeZoneInfo.GetUtcOffsetFromUtc(time, TimeZoneInfo.Local, out flag, out isAmbiguousDst).Ticks;
				}
			}
			if (num < 0L || num > 3155378975999999999L)
			{
				result.parsedDate = DateTime.MinValue;
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "Format_DateOutOfRange");
				return false;
			}
			result.parsedDate = new DateTime(num, DateTimeKind.Local, isAmbiguousDst);
			return true;
		}

		// Token: 0x06000E8B RID: 3723 RVA: 0x000D45E4 File Offset: 0x000D37E4
		private static bool ParseISO8601(ref DateTimeRawInfo raw, ref __DTString str, DateTimeStyles styles, ref DateTimeResult result)
		{
			if (raw.year >= 0 && raw.GetNumber(0) >= 0)
			{
				raw.GetNumber(1);
			}
			str.Index--;
			int second = 0;
			double num = 0.0;
			str.SkipWhiteSpaces();
			int hour;
			if (!DateTimeParse.ParseDigits(ref str, 2, out hour))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			str.SkipWhiteSpaces();
			if (!str.Match(':'))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			str.SkipWhiteSpaces();
			int minute;
			if (!DateTimeParse.ParseDigits(ref str, 2, out minute))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			str.SkipWhiteSpaces();
			if (str.Match(':'))
			{
				str.SkipWhiteSpaces();
				if (!DateTimeParse.ParseDigits(ref str, 2, out second))
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (str.Match('.'))
				{
					if (!DateTimeParse.ParseFraction(ref str, out num))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					str.Index--;
				}
				str.SkipWhiteSpaces();
			}
			if (str.GetNext())
			{
				char @char = str.GetChar();
				if (@char == '+' || @char == '-')
				{
					result.flags |= ParseFlags.TimeZoneUsed;
					if (!DateTimeParse.ParseTimeZone(ref str, ref result.timeZoneOffset))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
				}
				else if (@char == 'Z' || @char == 'z')
				{
					result.flags |= ParseFlags.TimeZoneUsed;
					result.timeZoneOffset = TimeSpan.Zero;
					result.flags |= ParseFlags.TimeZoneUtc;
				}
				else
				{
					str.Index--;
				}
				str.SkipWhiteSpaces();
				if (str.Match('#'))
				{
					if (!DateTimeParse.VerifyValidPunctuation(ref str))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					str.SkipWhiteSpaces();
				}
				if (str.Match('\0') && !DateTimeParse.VerifyValidPunctuation(ref str))
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (str.GetNext())
				{
					result.SetBadDateTimeFailure();
					return false;
				}
			}
			Calendar defaultInstance = GregorianCalendar.GetDefaultInstance();
			DateTime parsedDate;
			if (!defaultInstance.TryToDateTime(raw.year, raw.GetNumber(0), raw.GetNumber(1), hour, minute, second, 0, result.era, out parsedDate))
			{
				result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
				return false;
			}
			if (!parsedDate.TryAddTicks((long)Math.Round(num * 10000000.0), out parsedDate))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			result.parsedDate = parsedDate;
			return DateTimeParse.DetermineTimeZoneAdjustments(ref result, styles, false);
		}

		// Token: 0x06000E8C RID: 3724 RVA: 0x000D480C File Offset: 0x000D3A0C
		internal static bool MatchHebrewDigits(ref __DTString str, int digitLen, out int number)
		{
			number = 0;
			HebrewNumberParsingContext hebrewNumberParsingContext = new HebrewNumberParsingContext(0);
			HebrewNumberParsingState hebrewNumberParsingState = HebrewNumberParsingState.ContinueParsing;
			while (hebrewNumberParsingState == HebrewNumberParsingState.ContinueParsing && str.GetNext())
			{
				hebrewNumberParsingState = HebrewNumber.ParseByChar(str.GetChar(), ref hebrewNumberParsingContext);
			}
			if (hebrewNumberParsingState == HebrewNumberParsingState.FoundEndOfHebrewNumber)
			{
				number = hebrewNumberParsingContext.result;
				return true;
			}
			return false;
		}

		// Token: 0x06000E8D RID: 3725 RVA: 0x000D4851 File Offset: 0x000D3A51
		internal static bool ParseDigits(ref __DTString str, int digitLen, out int result)
		{
			if (digitLen == 1)
			{
				return DateTimeParse.ParseDigits(ref str, 1, 2, out result);
			}
			return DateTimeParse.ParseDigits(ref str, digitLen, digitLen, out result);
		}

		// Token: 0x06000E8E RID: 3726 RVA: 0x000D486C File Offset: 0x000D3A6C
		internal static bool ParseDigits(ref __DTString str, int minDigitLen, int maxDigitLen, out int result)
		{
			int num = 0;
			int index = str.Index;
			int i;
			for (i = 0; i < maxDigitLen; i++)
			{
				if (!str.GetNextDigit())
				{
					str.Index--;
					break;
				}
				num = num * 10 + str.GetDigit();
			}
			result = num;
			if (i < minDigitLen)
			{
				str.Index = index;
				return false;
			}
			return true;
		}

		// Token: 0x06000E8F RID: 3727 RVA: 0x000D48C0 File Offset: 0x000D3AC0
		private static bool ParseFractionExact(ref __DTString str, int maxDigitLen, ref double result)
		{
			if (!str.GetNextDigit())
			{
				str.Index--;
				return false;
			}
			result = (double)str.GetDigit();
			int i;
			for (i = 1; i < maxDigitLen; i++)
			{
				if (!str.GetNextDigit())
				{
					str.Index--;
					break;
				}
				result = result * 10.0 + (double)str.GetDigit();
			}
			result /= (double)TimeSpanParse.Pow10(i);
			return i == maxDigitLen;
		}

		// Token: 0x06000E90 RID: 3728 RVA: 0x000D4934 File Offset: 0x000D3B34
		private static bool ParseSign(ref __DTString str, ref bool result)
		{
			if (!str.GetNext())
			{
				return false;
			}
			char @char = str.GetChar();
			if (@char == '+')
			{
				result = true;
				return true;
			}
			if (@char == '-')
			{
				result = false;
				return true;
			}
			return false;
		}

		// Token: 0x06000E91 RID: 3729 RVA: 0x000D4968 File Offset: 0x000D3B68
		private static bool ParseTimeZoneOffset(ref __DTString str, int len, ref TimeSpan result)
		{
			bool flag = true;
			int num = 0;
			int hours;
			if (len - 1 <= 1)
			{
				if (!DateTimeParse.ParseSign(ref str, ref flag))
				{
					return false;
				}
				if (!DateTimeParse.ParseDigits(ref str, len, out hours))
				{
					return false;
				}
			}
			else
			{
				if (!DateTimeParse.ParseSign(ref str, ref flag))
				{
					return false;
				}
				if (!DateTimeParse.ParseDigits(ref str, 1, out hours))
				{
					return false;
				}
				if (str.Match(":"))
				{
					if (!DateTimeParse.ParseDigits(ref str, 2, out num))
					{
						return false;
					}
				}
				else
				{
					str.Index--;
					if (!DateTimeParse.ParseDigits(ref str, 2, out num))
					{
						return false;
					}
				}
			}
			if (num < 0 || num >= 60)
			{
				return false;
			}
			result = new TimeSpan(hours, num, 0);
			if (!flag)
			{
				result = result.Negate();
			}
			return true;
		}

		// Token: 0x06000E92 RID: 3730 RVA: 0x000D4A0C File Offset: 0x000D3C0C
		private static bool MatchAbbreviatedMonthName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			int num = 0;
			result = -1;
			if (str.GetNext())
			{
				int num2 = (dtfi.GetMonthName(13).Length == 0) ? 12 : 13;
				for (int i = 1; i <= num2; i++)
				{
					string abbreviatedMonthName = dtfi.GetAbbreviatedMonthName(i);
					int length = abbreviatedMonthName.Length;
					if ((dtfi.HasSpacesInMonthNames ? str.MatchSpecifiedWords(abbreviatedMonthName, false, ref length) : str.MatchSpecifiedWord(abbreviatedMonthName)) && length > num)
					{
						num = length;
						result = i;
					}
				}
				if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
				{
					int num3 = str.MatchLongestWords(dtfi.AbbreviatedMonthGenitiveNames, ref num);
					if (num3 >= 0)
					{
						result = num3 + 1;
					}
				}
				if ((dtfi.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
				{
					int num4 = str.MatchLongestWords(dtfi.InternalGetLeapYearMonthNames(), ref num);
					if (num4 >= 0)
					{
						result = num4 + 1;
					}
				}
			}
			if (result > 0)
			{
				str.Index += num - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000E93 RID: 3731 RVA: 0x000D4AE4 File Offset: 0x000D3CE4
		private static bool MatchMonthName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			int num = 0;
			result = -1;
			if (str.GetNext())
			{
				int num2 = (dtfi.GetMonthName(13).Length == 0) ? 12 : 13;
				for (int i = 1; i <= num2; i++)
				{
					string monthName = dtfi.GetMonthName(i);
					int length = monthName.Length;
					if ((dtfi.HasSpacesInMonthNames ? str.MatchSpecifiedWords(monthName, false, ref length) : str.MatchSpecifiedWord(monthName)) && length > num)
					{
						num = length;
						result = i;
					}
				}
				if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
				{
					int num3 = str.MatchLongestWords(dtfi.MonthGenitiveNames, ref num);
					if (num3 >= 0)
					{
						result = num3 + 1;
					}
				}
				if ((dtfi.FormatFlags & DateTimeFormatFlags.UseLeapYearMonth) != DateTimeFormatFlags.None)
				{
					int num4 = str.MatchLongestWords(dtfi.InternalGetLeapYearMonthNames(), ref num);
					if (num4 >= 0)
					{
						result = num4 + 1;
					}
				}
			}
			if (result > 0)
			{
				str.Index += num - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000E94 RID: 3732 RVA: 0x000D4BBC File Offset: 0x000D3DBC
		private static bool MatchAbbreviatedDayName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			int num = 0;
			result = -1;
			if (str.GetNext())
			{
				for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
				{
					string abbreviatedDayName = dtfi.GetAbbreviatedDayName(dayOfWeek);
					int length = abbreviatedDayName.Length;
					if ((dtfi.HasSpacesInDayNames ? str.MatchSpecifiedWords(abbreviatedDayName, false, ref length) : str.MatchSpecifiedWord(abbreviatedDayName)) && length > num)
					{
						num = length;
						result = (int)dayOfWeek;
					}
				}
			}
			if (result >= 0)
			{
				str.Index += num - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000E95 RID: 3733 RVA: 0x000D4C2C File Offset: 0x000D3E2C
		private static bool MatchDayName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			int num = 0;
			result = -1;
			if (str.GetNext())
			{
				for (DayOfWeek dayOfWeek = DayOfWeek.Sunday; dayOfWeek <= DayOfWeek.Saturday; dayOfWeek++)
				{
					string dayName = dtfi.GetDayName(dayOfWeek);
					int length = dayName.Length;
					if ((dtfi.HasSpacesInDayNames ? str.MatchSpecifiedWords(dayName, false, ref length) : str.MatchSpecifiedWord(dayName)) && length > num)
					{
						num = length;
						result = (int)dayOfWeek;
					}
				}
			}
			if (result >= 0)
			{
				str.Index += num - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000E96 RID: 3734 RVA: 0x000D4C9C File Offset: 0x000D3E9C
		private static bool MatchEraName(ref __DTString str, DateTimeFormatInfo dtfi, ref int result)
		{
			if (str.GetNext())
			{
				int[] eras = dtfi.Calendar.Eras;
				if (eras != null)
				{
					for (int i = 0; i < eras.Length; i++)
					{
						string text = dtfi.GetEraName(eras[i]);
						if (str.MatchSpecifiedWord(text))
						{
							str.Index += text.Length - 1;
							result = eras[i];
							return true;
						}
						text = dtfi.GetAbbreviatedEraName(eras[i]);
						if (str.MatchSpecifiedWord(text))
						{
							str.Index += text.Length - 1;
							result = eras[i];
							return true;
						}
					}
				}
			}
			return false;
		}

		// Token: 0x06000E97 RID: 3735 RVA: 0x000D4D28 File Offset: 0x000D3F28
		private static bool MatchTimeMark(ref __DTString str, DateTimeFormatInfo dtfi, ref DateTimeParse.TM result)
		{
			result = DateTimeParse.TM.NotSet;
			if (dtfi.AMDesignator.Length == 0)
			{
				result = DateTimeParse.TM.AM;
			}
			if (dtfi.PMDesignator.Length == 0)
			{
				result = DateTimeParse.TM.PM;
			}
			if (str.GetNext())
			{
				string text = dtfi.AMDesignator;
				if (text.Length > 0 && str.MatchSpecifiedWord(text))
				{
					str.Index += text.Length - 1;
					result = DateTimeParse.TM.AM;
					return true;
				}
				text = dtfi.PMDesignator;
				if (text.Length > 0 && str.MatchSpecifiedWord(text))
				{
					str.Index += text.Length - 1;
					result = DateTimeParse.TM.PM;
					return true;
				}
				str.Index--;
			}
			return result != DateTimeParse.TM.NotSet;
		}

		// Token: 0x06000E98 RID: 3736 RVA: 0x000D4DD4 File Offset: 0x000D3FD4
		private static bool MatchAbbreviatedTimeMark(ref __DTString str, DateTimeFormatInfo dtfi, ref DateTimeParse.TM result)
		{
			if (str.GetNext())
			{
				string amdesignator = dtfi.AMDesignator;
				if (amdesignator.Length > 0 && str.GetChar() == amdesignator[0])
				{
					result = DateTimeParse.TM.AM;
					return true;
				}
				string pmdesignator = dtfi.PMDesignator;
				if (pmdesignator.Length > 0 && str.GetChar() == pmdesignator[0])
				{
					result = DateTimeParse.TM.PM;
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E99 RID: 3737 RVA: 0x000D4E32 File Offset: 0x000D4032
		private static bool CheckNewValue(ref int currentValue, int newValue, char patternChar, ref DateTimeResult result)
		{
			if (currentValue == -1)
			{
				currentValue = newValue;
				return true;
			}
			if (newValue != currentValue)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", patternChar);
				return false;
			}
			return true;
		}

		// Token: 0x06000E9A RID: 3738 RVA: 0x000D4E58 File Offset: 0x000D4058
		private static DateTime GetDateTimeNow(ref DateTimeResult result, ref DateTimeStyles styles)
		{
			if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0)
			{
				if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0)
				{
					return new DateTime(DateTime.UtcNow.Ticks + result.timeZoneOffset.Ticks, DateTimeKind.Unspecified);
				}
				if ((styles & DateTimeStyles.AssumeUniversal) != DateTimeStyles.None)
				{
					return DateTime.UtcNow;
				}
			}
			return DateTime.Now;
		}

		// Token: 0x06000E9B RID: 3739 RVA: 0x000D4EB4 File Offset: 0x000D40B4
		private static bool CheckDefaultDateTime(ref DateTimeResult result, ref Calendar cal, DateTimeStyles styles)
		{
			if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0 && (result.Month != -1 || result.Day != -1) && (result.Year == -1 || (result.flags & ParseFlags.YearDefault) != (ParseFlags)0) && (result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "Format_MissingIncompleteDate");
				return false;
			}
			if (result.Year == -1 || result.Month == -1 || result.Day == -1)
			{
				DateTime dateTimeNow = DateTimeParse.GetDateTimeNow(ref result, ref styles);
				if (result.Month == -1 && result.Day == -1)
				{
					if (result.Year == -1)
					{
						if ((styles & DateTimeStyles.NoCurrentDateDefault) != DateTimeStyles.None)
						{
							cal = GregorianCalendar.GetDefaultInstance();
							result.Year = (result.Month = (result.Day = 1));
						}
						else
						{
							result.Year = cal.GetYear(dateTimeNow);
							result.Month = cal.GetMonth(dateTimeNow);
							result.Day = cal.GetDayOfMonth(dateTimeNow);
						}
					}
					else
					{
						result.Month = 1;
						result.Day = 1;
					}
				}
				else
				{
					if (result.Year == -1)
					{
						result.Year = cal.GetYear(dateTimeNow);
					}
					if (result.Month == -1)
					{
						result.Month = 1;
					}
					if (result.Day == -1)
					{
						result.Day = 1;
					}
				}
			}
			if (result.Hour == -1)
			{
				result.Hour = 0;
			}
			if (result.Minute == -1)
			{
				result.Minute = 0;
			}
			if (result.Second == -1)
			{
				result.Second = 0;
			}
			if (result.era == -1)
			{
				result.era = 0;
			}
			return true;
		}

		// Token: 0x06000E9C RID: 3740 RVA: 0x000D5034 File Offset: 0x000D4234
		private unsafe static string ExpandPredefinedFormat(ReadOnlySpan<char> format, ref DateTimeFormatInfo dtfi, ref ParsingInfo parseInfo, ref DateTimeResult result)
		{
			char c = (char)(*format[0]);
			if (c <= 'R')
			{
				if (c != 'O')
				{
					if (c != 'R')
					{
						goto IL_104;
					}
					goto IL_59;
				}
			}
			else if (c != 'U')
			{
				switch (c)
				{
				case 'o':
				case 's':
					break;
				case 'p':
				case 'q':
				case 't':
					goto IL_104;
				case 'r':
					goto IL_59;
				case 'u':
					parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
					dtfi = DateTimeFormatInfo.InvariantInfo;
					if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0)
					{
						result.flags |= ParseFlags.UtcSortPattern;
						goto IL_104;
					}
					goto IL_104;
				default:
					goto IL_104;
				}
			}
			else
			{
				parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
				result.flags |= ParseFlags.TimeZoneUsed;
				result.timeZoneOffset = new TimeSpan(0L);
				result.flags |= ParseFlags.TimeZoneUtc;
				if (dtfi.Calendar.GetType() != typeof(GregorianCalendar))
				{
					dtfi = (DateTimeFormatInfo)dtfi.Clone();
					dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
					goto IL_104;
				}
				goto IL_104;
			}
			DateTimeParse.ConfigureFormatOS(ref dtfi, ref parseInfo);
			goto IL_104;
			IL_59:
			DateTimeParse.ConfigureFormatR(ref dtfi, ref parseInfo, ref result);
			IL_104:
			return DateTimeFormat.GetRealFormat(format, dtfi);
		}

		// Token: 0x06000E9D RID: 3741 RVA: 0x000D5150 File Offset: 0x000D4350
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ParseJapaneseEraStart(ref __DTString str, DateTimeFormatInfo dtfi)
		{
			if (LocalAppContextSwitches.EnforceLegacyJapaneseDateParsing || dtfi.Calendar.ID != CalendarId.JAPAN || !str.GetNext())
			{
				return false;
			}
			if (str.m_current != "元"[0])
			{
				str.Index--;
				return false;
			}
			return true;
		}

		// Token: 0x06000E9E RID: 3742 RVA: 0x000D519D File Offset: 0x000D439D
		private static void ConfigureFormatR(ref DateTimeFormatInfo dtfi, ref ParsingInfo parseInfo, ref DateTimeResult result)
		{
			parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
			dtfi = DateTimeFormatInfo.InvariantInfo;
			if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0)
			{
				result.flags |= ParseFlags.Rfc1123Pattern;
			}
		}

		// Token: 0x06000E9F RID: 3743 RVA: 0x000D51CE File Offset: 0x000D43CE
		private static void ConfigureFormatOS(ref DateTimeFormatInfo dtfi, ref ParsingInfo parseInfo)
		{
			parseInfo.calendar = GregorianCalendar.GetDefaultInstance();
			dtfi = DateTimeFormatInfo.InvariantInfo;
		}

		// Token: 0x06000EA0 RID: 3744 RVA: 0x000D51E4 File Offset: 0x000D43E4
		private unsafe static bool ParseByFormat(ref __DTString str, ref __DTString format, ref ParsingInfo parseInfo, DateTimeFormatInfo dtfi, ref DateTimeResult result)
		{
			int newValue = 0;
			int newValue2 = 0;
			int newValue3 = 0;
			int newValue4 = 0;
			int newValue5 = 0;
			int newValue6 = 0;
			int newValue7 = 0;
			double num = 0.0;
			DateTimeParse.TM tm = DateTimeParse.TM.AM;
			char @char = format.GetChar();
			int repeatCount;
			if (@char <= 'K')
			{
				if (@char <= '.')
				{
					if (@char <= '%')
					{
						if (@char != '"')
						{
							if (@char != '%')
							{
								goto IL_918;
							}
							if (format.Index >= format.Value.Length - 1 || *format.Value[format.Index + 1] == 37)
							{
								result.SetBadFormatSpecifierFailure(format.Value);
								return false;
							}
							return true;
						}
					}
					else if (@char != '\'')
					{
						if (@char != '.')
						{
							goto IL_918;
						}
						if (str.Match(@char))
						{
							return true;
						}
						if (format.GetNext() && format.Match('F'))
						{
							format.GetRepeatCount();
							return true;
						}
						result.SetBadDateTimeFailure();
						return false;
					}
					StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
					if (!DateTimeParse.TryParseQuoteString(format.Value, format.Index, stringBuilder, out repeatCount))
					{
						result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_BadQuote", @char);
						StringBuilderCache.Release(stringBuilder);
						return false;
					}
					format.Index += repeatCount - 1;
					string stringAndRelease = StringBuilderCache.GetStringAndRelease(stringBuilder);
					for (int i = 0; i < stringAndRelease.Length; i++)
					{
						if (stringAndRelease[i] == ' ' && parseInfo.fAllowInnerWhite)
						{
							str.SkipWhiteSpaces();
						}
						else if (!str.Match(stringAndRelease[i]))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
					}
					if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0 && (((result.flags & ParseFlags.Rfc1123Pattern) != (ParseFlags)0 && stringAndRelease == "GMT") || ((result.flags & ParseFlags.UtcSortPattern) != (ParseFlags)0 && stringAndRelease == "Z")))
					{
						result.flags |= ParseFlags.TimeZoneUsed;
						result.timeZoneOffset = TimeSpan.Zero;
						return true;
					}
					return true;
				}
				else if (@char <= ':')
				{
					if (@char != '/')
					{
						if (@char != ':')
						{
							goto IL_918;
						}
						if (((dtfi.TimeSeparator.Length > 1 && dtfi.TimeSeparator[0] == ':') || !str.Match(':')) && !str.Match(dtfi.TimeSeparator))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						return true;
					}
					else
					{
						if (((dtfi.DateSeparator.Length > 1 && dtfi.DateSeparator[0] == '/') || !str.Match('/')) && !str.Match(dtfi.DateSeparator))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						return true;
					}
				}
				else if (@char != 'F')
				{
					if (@char != 'H')
					{
						if (@char != 'K')
						{
							goto IL_918;
						}
						if (str.Match('Z'))
						{
							if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0 && result.timeZoneOffset != TimeSpan.Zero)
							{
								result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", 'K');
								return false;
							}
							result.flags |= ParseFlags.TimeZoneUsed;
							result.timeZoneOffset = new TimeSpan(0L);
							result.flags |= ParseFlags.TimeZoneUtc;
							return true;
						}
						else
						{
							if (!str.Match('+') && !str.Match('-'))
							{
								return true;
							}
							str.Index--;
							TimeSpan timeSpan = new TimeSpan(0L);
							if (!DateTimeParse.ParseTimeZoneOffset(ref str, 3, ref timeSpan))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0 && timeSpan != result.timeZoneOffset)
							{
								result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", 'K');
								return false;
							}
							result.timeZoneOffset = timeSpan;
							result.flags |= ParseFlags.TimeZoneUsed;
							return true;
						}
					}
					else
					{
						repeatCount = format.GetRepeatCount();
						if (!DateTimeParse.ParseDigits(ref str, (repeatCount < 2) ? 1 : 2, out newValue5))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						if (!DateTimeParse.CheckNewValue(ref result.Hour, newValue5, @char, ref result))
						{
							return false;
						}
						return true;
					}
				}
			}
			else if (@char <= 'h')
			{
				if (@char <= 'Z')
				{
					if (@char != 'M')
					{
						if (@char != 'Z')
						{
							goto IL_918;
						}
						if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0 && result.timeZoneOffset != TimeSpan.Zero)
						{
							result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", 'Z');
							return false;
						}
						result.flags |= ParseFlags.TimeZoneUsed;
						result.timeZoneOffset = new TimeSpan(0L);
						result.flags |= ParseFlags.TimeZoneUtc;
						str.Index++;
						if (!DateTimeParse.GetTimeZoneName(ref str))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						str.Index--;
						return true;
					}
					else
					{
						repeatCount = format.GetRepeatCount();
						if (repeatCount <= 2)
						{
							if (!DateTimeParse.ParseDigits(ref str, repeatCount, out newValue2) && (!parseInfo.fCustomNumberParser || !parseInfo.parseNumberDelegate(ref str, repeatCount, out newValue2)))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
						}
						else
						{
							if (repeatCount == 3)
							{
								if (!DateTimeParse.MatchAbbreviatedMonthName(ref str, dtfi, ref newValue2))
								{
									result.SetBadDateTimeFailure();
									return false;
								}
							}
							else if (!DateTimeParse.MatchMonthName(ref str, dtfi, ref newValue2))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							result.flags |= ParseFlags.ParsedMonthName;
						}
						if (!DateTimeParse.CheckNewValue(ref result.Month, newValue2, @char, ref result))
						{
							return false;
						}
						return true;
					}
				}
				else if (@char != '\\')
				{
					switch (@char)
					{
					case 'd':
						repeatCount = format.GetRepeatCount();
						if (repeatCount <= 2)
						{
							if (!DateTimeParse.ParseDigits(ref str, repeatCount, out newValue3) && (!parseInfo.fCustomNumberParser || !parseInfo.parseNumberDelegate(ref str, repeatCount, out newValue3)))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							if (!DateTimeParse.CheckNewValue(ref result.Day, newValue3, @char, ref result))
							{
								return false;
							}
							return true;
						}
						else
						{
							if (repeatCount == 3)
							{
								if (!DateTimeParse.MatchAbbreviatedDayName(ref str, dtfi, ref newValue4))
								{
									result.SetBadDateTimeFailure();
									return false;
								}
							}
							else if (!DateTimeParse.MatchDayName(ref str, dtfi, ref newValue4))
							{
								result.SetBadDateTimeFailure();
								return false;
							}
							if (!DateTimeParse.CheckNewValue(ref parseInfo.dayOfWeek, newValue4, @char, ref result))
							{
								return false;
							}
							return true;
						}
						break;
					case 'e':
						goto IL_918;
					case 'f':
						break;
					case 'g':
						format.GetRepeatCount();
						if (!DateTimeParse.MatchEraName(ref str, dtfi, ref result.era))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						return true;
					case 'h':
						parseInfo.fUseHour12 = true;
						repeatCount = format.GetRepeatCount();
						if (!DateTimeParse.ParseDigits(ref str, (repeatCount < 2) ? 1 : 2, out newValue5))
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						if (!DateTimeParse.CheckNewValue(ref result.Hour, newValue5, @char, ref result))
						{
							return false;
						}
						return true;
					default:
						goto IL_918;
					}
				}
				else
				{
					if (!format.GetNext())
					{
						result.SetBadFormatSpecifierFailure(format.Value);
						return false;
					}
					if (!str.Match(format.GetChar()))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					return true;
				}
			}
			else if (@char <= 's')
			{
				if (@char != 'm')
				{
					if (@char != 's')
					{
						goto IL_918;
					}
					repeatCount = format.GetRepeatCount();
					if (!DateTimeParse.ParseDigits(ref str, (repeatCount < 2) ? 1 : 2, out newValue7))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if (!DateTimeParse.CheckNewValue(ref result.Second, newValue7, @char, ref result))
					{
						return false;
					}
					return true;
				}
				else
				{
					repeatCount = format.GetRepeatCount();
					if (!DateTimeParse.ParseDigits(ref str, (repeatCount < 2) ? 1 : 2, out newValue6))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if (!DateTimeParse.CheckNewValue(ref result.Minute, newValue6, @char, ref result))
					{
						return false;
					}
					return true;
				}
			}
			else if (@char != 't')
			{
				if (@char != 'y')
				{
					if (@char != 'z')
					{
						goto IL_918;
					}
					repeatCount = format.GetRepeatCount();
					TimeSpan timeSpan2 = new TimeSpan(0L);
					if (!DateTimeParse.ParseTimeZoneOffset(ref str, repeatCount, ref timeSpan2))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if ((result.flags & ParseFlags.TimeZoneUsed) != (ParseFlags)0 && timeSpan2 != result.timeZoneOffset)
					{
						result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", 'z');
						return false;
					}
					result.timeZoneOffset = timeSpan2;
					result.flags |= ParseFlags.TimeZoneUsed;
					return true;
				}
				else
				{
					repeatCount = format.GetRepeatCount();
					bool flag;
					if (DateTimeParse.ParseJapaneseEraStart(ref str, dtfi))
					{
						newValue = 1;
						flag = true;
					}
					else if (dtfi.HasForceTwoDigitYears)
					{
						flag = DateTimeParse.ParseDigits(ref str, 1, 4, out newValue);
					}
					else
					{
						if (repeatCount <= 2)
						{
							parseInfo.fUseTwoDigitYear = true;
						}
						flag = DateTimeParse.ParseDigits(ref str, repeatCount, out newValue);
					}
					if (!flag && parseInfo.fCustomNumberParser)
					{
						flag = parseInfo.parseNumberDelegate(ref str, repeatCount, out newValue);
					}
					if (!flag)
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if (!DateTimeParse.CheckNewValue(ref result.Year, newValue, @char, ref result))
					{
						return false;
					}
					return true;
				}
			}
			else
			{
				repeatCount = format.GetRepeatCount();
				if (repeatCount == 1)
				{
					if (!DateTimeParse.MatchAbbreviatedTimeMark(ref str, dtfi, ref tm))
					{
						result.SetBadDateTimeFailure();
						return false;
					}
				}
				else if (!DateTimeParse.MatchTimeMark(ref str, dtfi, ref tm))
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (parseInfo.timeMark == DateTimeParse.TM.NotSet)
				{
					parseInfo.timeMark = tm;
					return true;
				}
				if (parseInfo.timeMark != tm)
				{
					result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", @char);
					return false;
				}
				return true;
			}
			repeatCount = format.GetRepeatCount();
			if (repeatCount > 7)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (!DateTimeParse.ParseFractionExact(ref str, repeatCount, ref num) && @char == 'f')
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (result.fraction < 0.0)
			{
				result.fraction = num;
				return true;
			}
			if (num != result.fraction)
			{
				result.SetFailure(ParseFailureKind.FormatWithParameter, "Format_RepeatDateTimePattern", @char);
				return false;
			}
			return true;
			IL_918:
			if (@char == ' ')
			{
				if (!parseInfo.fAllowInnerWhite && !str.Match(@char))
				{
					if (parseInfo.fAllowTrailingWhite && format.GetNext() && DateTimeParse.ParseByFormat(ref str, ref format, ref parseInfo, dtfi, ref result))
					{
						return true;
					}
					result.SetBadDateTimeFailure();
					return false;
				}
			}
			else if (format.MatchSpecifiedWord("GMT"))
			{
				format.Index += "GMT".Length - 1;
				result.flags |= ParseFlags.TimeZoneUsed;
				result.timeZoneOffset = TimeSpan.Zero;
				if (!str.Match("GMT"))
				{
					result.SetBadDateTimeFailure();
					return false;
				}
			}
			else if (!str.Match(@char))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			return true;
		}

		// Token: 0x06000EA1 RID: 3745 RVA: 0x000D5BB8 File Offset: 0x000D4DB8
		internal unsafe static bool TryParseQuoteString(ReadOnlySpan<char> format, int pos, StringBuilder result, out int returnValue)
		{
			returnValue = 0;
			int length = format.Length;
			int num = pos;
			char c = (char)(*format[pos++]);
			bool flag = false;
			while (pos < length)
			{
				char c2 = (char)(*format[pos++]);
				if (c2 == c)
				{
					flag = true;
					break;
				}
				if (c2 == '\\')
				{
					if (pos >= length)
					{
						return false;
					}
					result.Append((char)(*format[pos++]));
				}
				else
				{
					result.Append(c2);
				}
			}
			if (!flag)
			{
				return false;
			}
			returnValue = pos - num;
			return true;
		}

		// Token: 0x06000EA2 RID: 3746 RVA: 0x000D5C3C File Offset: 0x000D4E3C
		private unsafe static bool DoStrictParse(ReadOnlySpan<char> s, ReadOnlySpan<char> formatParam, DateTimeStyles styles, DateTimeFormatInfo dtfi, ref DateTimeResult result)
		{
			ParsingInfo parsingInfo = default(ParsingInfo);
			parsingInfo.Init();
			parsingInfo.calendar = dtfi.Calendar;
			parsingInfo.fAllowInnerWhite = ((styles & DateTimeStyles.AllowInnerWhite) > DateTimeStyles.None);
			parsingInfo.fAllowTrailingWhite = ((styles & DateTimeStyles.AllowTrailingWhite) > DateTimeStyles.None);
			if (formatParam.Length == 1)
			{
				char c = (char)(*formatParam[0]);
				if (styles == DateTimeStyles.None)
				{
					if (c <= 'R')
					{
						if (c == 'O')
						{
							goto IL_87;
						}
						if (c != 'R')
						{
							goto IL_99;
						}
					}
					else
					{
						if (c == 'o')
						{
							goto IL_87;
						}
						if (c != 'r')
						{
							goto IL_99;
						}
					}
					DateTimeParse.ConfigureFormatR(ref dtfi, ref parsingInfo, ref result);
					return DateTimeParse.ParseFormatR(s, ref parsingInfo, ref result);
					IL_87:
					DateTimeParse.ConfigureFormatOS(ref dtfi, ref parsingInfo);
					return DateTimeParse.ParseFormatO(s, ref result);
				}
				IL_99:
				if ((result.flags & ParseFlags.CaptureOffset) != (ParseFlags)0 && c == 'U')
				{
					result.SetBadFormatSpecifierFailure(formatParam);
					return false;
				}
				formatParam = DateTimeParse.ExpandPredefinedFormat(formatParam, ref dtfi, ref parsingInfo, ref result);
			}
			result.calendar = parsingInfo.calendar;
			if (parsingInfo.calendar.ID == CalendarId.HEBREW)
			{
				parsingInfo.parseNumberDelegate = DateTimeParse.s_hebrewNumberParser;
				parsingInfo.fCustomNumberParser = true;
			}
			result.Hour = (result.Minute = (result.Second = -1));
			__DTString _DTString = new __DTString(formatParam, dtfi, false);
			__DTString _DTString2 = new __DTString(s, dtfi, false);
			if (parsingInfo.fAllowTrailingWhite)
			{
				_DTString.TrimTail();
				_DTString.RemoveTrailingInQuoteSpaces();
				_DTString2.TrimTail();
			}
			if ((styles & DateTimeStyles.AllowLeadingWhite) != DateTimeStyles.None)
			{
				_DTString.SkipWhiteSpaces();
				_DTString.RemoveLeadingInQuoteSpaces();
				_DTString2.SkipWhiteSpaces();
			}
			while (_DTString.GetNext())
			{
				if (parsingInfo.fAllowInnerWhite)
				{
					_DTString2.SkipWhiteSpaces();
				}
				if (!DateTimeParse.ParseByFormat(ref _DTString2, ref _DTString, ref parsingInfo, dtfi, ref result))
				{
					return false;
				}
			}
			if (_DTString2.Index < _DTString2.Value.Length - 1)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (parsingInfo.fUseTwoDigitYear && (dtfi.FormatFlags & DateTimeFormatFlags.UseHebrewRule) == DateTimeFormatFlags.None)
			{
				if (result.Year >= 100)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				try
				{
					result.Year = parsingInfo.calendar.ToFourDigitYear(result.Year);
				}
				catch (ArgumentOutOfRangeException)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
			}
			if (parsingInfo.fUseHour12)
			{
				if (parsingInfo.timeMark == DateTimeParse.TM.NotSet)
				{
					parsingInfo.timeMark = DateTimeParse.TM.AM;
				}
				if (result.Hour > 12)
				{
					result.SetBadDateTimeFailure();
					return false;
				}
				if (parsingInfo.timeMark == DateTimeParse.TM.AM)
				{
					if (result.Hour == 12)
					{
						result.Hour = 0;
					}
				}
				else
				{
					result.Hour = ((result.Hour == 12) ? 12 : (result.Hour + 12));
				}
			}
			else if ((parsingInfo.timeMark == DateTimeParse.TM.AM && result.Hour >= 12) || (parsingInfo.timeMark == DateTimeParse.TM.PM && result.Hour < 12))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			bool flag = result.Year == -1 && result.Month == -1 && result.Day == -1;
			if (!DateTimeParse.CheckDefaultDateTime(ref result, ref parsingInfo.calendar, styles))
			{
				return false;
			}
			if (!flag && dtfi.HasYearMonthAdjustment && !dtfi.YearMonthAdjustment(ref result.Year, ref result.Month, (result.flags & ParseFlags.ParsedMonthName) > (ParseFlags)0))
			{
				result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
				return false;
			}
			if (!parsingInfo.calendar.TryToDateTime(result.Year, result.Month, result.Day, result.Hour, result.Minute, result.Second, 0, result.era, out result.parsedDate))
			{
				result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
				return false;
			}
			if (result.fraction > 0.0 && !result.parsedDate.TryAddTicks((long)Math.Round(result.fraction * 10000000.0), out result.parsedDate))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (parsingInfo.dayOfWeek != -1 && parsingInfo.dayOfWeek != (int)parsingInfo.calendar.GetDayOfWeek(result.parsedDate))
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "Format_BadDayOfWeek");
				return false;
			}
			return DateTimeParse.DetermineTimeZoneAdjustments(ref result, styles, flag);
		}

		// Token: 0x06000EA3 RID: 3747 RVA: 0x000D6048 File Offset: 0x000D5248
		private unsafe static bool ParseFormatR(ReadOnlySpan<char> source, ref ParsingInfo parseInfo, ref DateTimeResult result)
		{
			if (source.Length != 29)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			uint num = (uint)(*source[0]);
			uint num2 = (uint)(*source[1]);
			uint num3 = (uint)(*source[2]);
			uint num4 = (uint)(*source[3]);
			if ((num | num2 | num3 | num4) > 127U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			uint num5 = num << 24 | num2 << 16 | num3 << 8 | num4 | 538976256U;
			DayOfWeek dayOfWeek;
			if (num5 <= 1935766572U)
			{
				if (num5 == 1718774060U)
				{
					dayOfWeek = DayOfWeek.Friday;
					goto IL_EC;
				}
				if (num5 == 1836019244U)
				{
					dayOfWeek = DayOfWeek.Monday;
					goto IL_EC;
				}
				if (num5 == 1935766572U)
				{
					dayOfWeek = DayOfWeek.Saturday;
					goto IL_EC;
				}
			}
			else if (num5 <= 1953002796U)
			{
				if (num5 == 1937075756U)
				{
					dayOfWeek = DayOfWeek.Sunday;
					goto IL_EC;
				}
				if (num5 == 1953002796U)
				{
					dayOfWeek = DayOfWeek.Thursday;
					goto IL_EC;
				}
			}
			else
			{
				if (num5 == 1953850668U)
				{
					dayOfWeek = DayOfWeek.Tuesday;
					goto IL_EC;
				}
				if (num5 == 2003133484U)
				{
					dayOfWeek = DayOfWeek.Wednesday;
					goto IL_EC;
				}
			}
			result.SetBadDateTimeFailure();
			return false;
			IL_EC:
			if (*source[4] != 32)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			uint num6 = (uint)(*source[5] - 48);
			uint num7 = (uint)(*source[6] - 48);
			if (num6 > 9U || num7 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int day = (int)(num6 * 10U + num7);
			if (*source[7] != 32)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			uint num8 = (uint)(*source[8]);
			uint num9 = (uint)(*source[9]);
			uint num10 = (uint)(*source[10]);
			uint num11 = (uint)(*source[11]);
			if ((num8 | num9 | num10 | num11) > 127U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			uint num12 = num8 << 24 | num9 << 16 | num10 << 8 | num11 | 538976256U;
			int month;
			if (num12 <= 1786080288U)
			{
				if (num12 <= 1684366112U)
				{
					if (num12 == 1634759200U)
					{
						month = 4;
						goto IL_287;
					}
					if (num12 == 1635084064U)
					{
						month = 8;
						goto IL_287;
					}
					if (num12 == 1684366112U)
					{
						month = 12;
						goto IL_287;
					}
				}
				else
				{
					if (num12 == 1717920288U)
					{
						month = 2;
						goto IL_287;
					}
					if (num12 == 1784770080U)
					{
						month = 1;
						goto IL_287;
					}
					if (num12 == 1786080288U)
					{
						month = 7;
						goto IL_287;
					}
				}
			}
			else if (num12 <= 1835104544U)
			{
				if (num12 == 1786080800U)
				{
					month = 6;
					goto IL_287;
				}
				if (num12 == 1835102752U)
				{
					month = 3;
					goto IL_287;
				}
				if (num12 == 1835104544U)
				{
					month = 5;
					goto IL_287;
				}
			}
			else
			{
				if (num12 == 1852798496U)
				{
					month = 11;
					goto IL_287;
				}
				if (num12 == 1868788768U)
				{
					month = 10;
					goto IL_287;
				}
				if (num12 == 1936027680U)
				{
					month = 9;
					goto IL_287;
				}
			}
			result.SetBadDateTimeFailure();
			return false;
			IL_287:
			uint num13 = (uint)(*source[12] - 48);
			uint num14 = (uint)(*source[13] - 48);
			uint num15 = (uint)(*source[14] - 48);
			uint num16 = (uint)(*source[15] - 48);
			if (num13 > 9U || num14 > 9U || num15 > 9U || num16 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int year = (int)(num13 * 1000U + num14 * 100U + num15 * 10U + num16);
			if (*source[16] != 32)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			uint num17 = (uint)(*source[17] - 48);
			uint num18 = (uint)(*source[18] - 48);
			if (num17 > 9U || num18 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int hour = (int)(num17 * 10U + num18);
			if (*source[19] != 58)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			uint num19 = (uint)(*source[20] - 48);
			uint num20 = (uint)(*source[21] - 48);
			if (num19 > 9U || num20 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int minute = (int)(num19 * 10U + num20);
			if (*source[22] != 58)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			uint num21 = (uint)(*source[23] - 48);
			uint num22 = (uint)(*source[24] - 48);
			if (num21 > 9U || num22 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int second = (int)(num21 * 10U + num22);
			if (*source[25] != 32 || *source[26] != 71 || *source[27] != 77 || *source[28] != 84)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (!parseInfo.calendar.TryToDateTime(year, month, day, hour, minute, second, 0, 0, out result.parsedDate))
			{
				result.SetFailure(ParseFailureKind.FormatBadDateTimeCalendar, "Format_BadDateTimeCalendar");
				return false;
			}
			if (dayOfWeek != result.parsedDate.DayOfWeek)
			{
				result.SetFailure(ParseFailureKind.FormatWithOriginalDateTime, "Format_BadDayOfWeek");
				return false;
			}
			return true;
		}

		// Token: 0x06000EA4 RID: 3748 RVA: 0x000D64D0 File Offset: 0x000D56D0
		private unsafe static bool ParseFormatO(ReadOnlySpan<char> source, ref DateTimeResult result)
		{
			if (source.Length < 27 || *source[4] != 45 || *source[7] != 45 || *source[10] != 84 || *source[13] != 58 || *source[16] != 58 || *source[19] != 46)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			uint num = (uint)(*source[0] - 48);
			uint num2 = (uint)(*source[1] - 48);
			uint num3 = (uint)(*source[2] - 48);
			uint num4 = (uint)(*source[3] - 48);
			if (num > 9U || num2 > 9U || num3 > 9U || num4 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int year = (int)(num * 1000U + num2 * 100U + num3 * 10U + num4);
			uint num5 = (uint)(*source[5] - 48);
			uint num6 = (uint)(*source[6] - 48);
			if (num5 > 9U || num6 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int month = (int)(num5 * 10U + num6);
			uint num7 = (uint)(*source[8] - 48);
			uint num8 = (uint)(*source[9] - 48);
			if (num7 > 9U || num8 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int day = (int)(num7 * 10U + num8);
			uint num9 = (uint)(*source[11] - 48);
			uint num10 = (uint)(*source[12] - 48);
			if (num9 > 9U || num10 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int hour = (int)(num9 * 10U + num10);
			uint num11 = (uint)(*source[14] - 48);
			uint num12 = (uint)(*source[15] - 48);
			if (num11 > 9U || num12 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int minute = (int)(num11 * 10U + num12);
			uint num13 = (uint)(*source[17] - 48);
			uint num14 = (uint)(*source[18] - 48);
			if (num13 > 9U || num14 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			int second = (int)(num13 * 10U + num14);
			uint num15 = (uint)(*source[20] - 48);
			uint num16 = (uint)(*source[21] - 48);
			uint num17 = (uint)(*source[22] - 48);
			uint num18 = (uint)(*source[23] - 48);
			uint num19 = (uint)(*source[24] - 48);
			uint num20 = (uint)(*source[25] - 48);
			uint num21 = (uint)(*source[26] - 48);
			if (num15 > 9U || num16 > 9U || num17 > 9U || num18 > 9U || num19 > 9U || num20 > 9U || num21 > 9U)
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			double num22 = (num15 * 1000000U + num16 * 100000U + num17 * 10000U + num18 * 1000U + num19 * 100U + num20 * 10U + num21) / 10000000.0;
			DateTime dateTime;
			if (!DateTime.TryCreate(year, month, day, hour, minute, second, 0, out dateTime))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (!dateTime.TryAddTicks((long)Math.Round(num22 * 10000000.0), out result.parsedDate))
			{
				result.SetBadDateTimeFailure();
				return false;
			}
			if (source.Length > 27)
			{
				char c = (char)(*source[27]);
				if (c != '+' && c != '-')
				{
					if (c != 'Z')
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					if (source.Length != 28)
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					result.flags |= (ParseFlags.TimeZoneUsed | ParseFlags.TimeZoneUtc);
				}
				else
				{
					int num25;
					int num26;
					if (source.Length == 33)
					{
						uint num23 = (uint)(*source[28] - 48);
						uint num24 = (uint)(*source[29] - 48);
						if (num23 > 9U || num24 > 9U)
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						num25 = (int)(num23 * 10U + num24);
						num26 = 30;
					}
					else
					{
						if (source.Length != 32)
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						num25 = (int)(*source[28] - 48);
						if (num25 > 9)
						{
							result.SetBadDateTimeFailure();
							return false;
						}
						num26 = 29;
					}
					if (*source[num26] != 58)
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					uint num27 = (uint)(*source[num26 + 1] - 48);
					uint num28 = (uint)(*source[num26 + 2] - 48);
					if (num27 > 9U || num28 > 9U)
					{
						result.SetBadDateTimeFailure();
						return false;
					}
					int minutes = (int)(num27 * 10U + num28);
					result.flags |= ParseFlags.TimeZoneUsed;
					result.timeZoneOffset = new TimeSpan(num25, minutes, 0);
					if (c == '-')
					{
						result.timeZoneOffset = result.timeZoneOffset.Negate();
					}
				}
			}
			return DateTimeParse.DetermineTimeZoneAdjustments(ref result, DateTimeStyles.None, false);
		}

		// Token: 0x06000EA5 RID: 3749 RVA: 0x000D6970 File Offset: 0x000D5B70
		private static Exception GetDateTimeParseException(ref DateTimeResult result)
		{
			switch (result.failure)
			{
			case ParseFailureKind.ArgumentNull:
				return new ArgumentNullException(result.failureArgumentName, SR.GetResourceString(result.failureMessageID));
			case ParseFailureKind.Format:
				return new FormatException(SR.GetResourceString(result.failureMessageID));
			case ParseFailureKind.FormatWithParameter:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), result.failureMessageFormatArgument));
			case ParseFailureKind.FormatWithOriginalDateTime:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), new string(result.originalDateTimeString)));
			case ParseFailureKind.FormatWithFormatSpecifier:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), new string(result.failedFormatSpecifier)));
			case ParseFailureKind.FormatWithOriginalDateTimeAndParameter:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), new string(result.originalDateTimeString), result.failureMessageFormatArgument));
			case ParseFailureKind.FormatBadDateTimeCalendar:
				return new FormatException(SR.Format(SR.GetResourceString(result.failureMessageID), new string(result.originalDateTimeString), result.calendar));
			default:
				return null;
			}
		}

		// Token: 0x04000315 RID: 789
		private static readonly DateTimeParse.MatchNumberDelegate s_hebrewNumberParser = new DateTimeParse.MatchNumberDelegate(DateTimeParse.MatchHebrewDigits);

		// Token: 0x04000316 RID: 790
		private static readonly DateTimeParse.DS[][] s_dateParsingStates = new DateTimeParse.DS[][]
		{
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.BEGIN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.TX_N,
				DateTimeParse.DS.N,
				DateTimeParse.DS.D_Nd,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_M,
				DateTimeParse.DS.D_M,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.BEGIN,
				DateTimeParse.DS.D_Y,
				DateTimeParse.DS.D_Y,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.BEGIN,
				DateTimeParse.DS.BEGIN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.NN,
				DateTimeParse.DS.D_NNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NM,
				DateTimeParse.DS.D_NM,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.D_NDS,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.N,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.D_YNd,
				DateTimeParse.DS.DX_YN,
				DateTimeParse.DS.N,
				DateTimeParse.DS.N,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_NN,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.TX_N,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.NN,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.NN,
				DateTimeParse.DS.NN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NN,
				DateTimeParse.DS.D_NNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NM,
				DateTimeParse.DS.D_MN,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_Nd,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.D_YNd,
				DateTimeParse.DS.DX_YN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_Nd,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_NN,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.TX_N,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_NN,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.DX_NNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NNd,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_NNY,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NNd,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_MN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_MN,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_M,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.D_YMd,
				DateTimeParse.DS.DX_YM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_M,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_MN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_MN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_MN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_NM,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_NM,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NM,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_MNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_MNd,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_NDS,
				DateTimeParse.DS.DX_NNDS,
				DateTimeParse.DS.DX_NNDS,
				DateTimeParse.DS.DX_NNDS,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NDS,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_NDS,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_NDS,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.D_YNd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YM,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.D_YMd,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_Y,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_Y,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_YN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.DX_YNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YN,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_YM,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.DX_YMN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_YM,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.DX_DS,
				DateTimeParse.DS.DX_DSN,
				DateTimeParse.DS.TX_N,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.TX_TS,
				DateTimeParse.DS.TX_TS,
				DateTimeParse.DS.TX_TS,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.D_Nd,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.D_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.ERROR
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.TX_NN,
				DateTimeParse.DS.TX_NN,
				DateTimeParse.DS.TX_NN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_NNt,
				DateTimeParse.DS.DX_NM,
				DateTimeParse.DS.D_NM,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.T_Nt,
				DateTimeParse.DS.TX_NN
			},
			new DateTimeParse.DS[]
			{
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.TX_NNN,
				DateTimeParse.DS.TX_NNN,
				DateTimeParse.DS.TX_NNN,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_S,
				DateTimeParse.DS.T_NNt,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.ERROR,
				DateTimeParse.DS.T_NNt,
				DateTimeParse.DS.T_NNt,
				DateTimeParse.DS.TX_NNN
			}
		};

		// Token: 0x02000112 RID: 274
		// (Invoke) Token: 0x06000EA8 RID: 3752
		internal delegate bool MatchNumberDelegate(ref __DTString str, int digitLen, out int result);

		// Token: 0x02000113 RID: 275
		internal enum DTT
		{
			// Token: 0x04000318 RID: 792
			End,
			// Token: 0x04000319 RID: 793
			NumEnd,
			// Token: 0x0400031A RID: 794
			NumAmpm,
			// Token: 0x0400031B RID: 795
			NumSpace,
			// Token: 0x0400031C RID: 796
			NumDatesep,
			// Token: 0x0400031D RID: 797
			NumTimesep,
			// Token: 0x0400031E RID: 798
			MonthEnd,
			// Token: 0x0400031F RID: 799
			MonthSpace,
			// Token: 0x04000320 RID: 800
			MonthDatesep,
			// Token: 0x04000321 RID: 801
			NumDatesuff,
			// Token: 0x04000322 RID: 802
			NumTimesuff,
			// Token: 0x04000323 RID: 803
			DayOfWeek,
			// Token: 0x04000324 RID: 804
			YearSpace,
			// Token: 0x04000325 RID: 805
			YearDateSep,
			// Token: 0x04000326 RID: 806
			YearEnd,
			// Token: 0x04000327 RID: 807
			TimeZone,
			// Token: 0x04000328 RID: 808
			Era,
			// Token: 0x04000329 RID: 809
			NumUTCTimeMark,
			// Token: 0x0400032A RID: 810
			Unk,
			// Token: 0x0400032B RID: 811
			NumLocalTimeMark,
			// Token: 0x0400032C RID: 812
			Max
		}

		// Token: 0x02000114 RID: 276
		internal enum TM
		{
			// Token: 0x0400032E RID: 814
			NotSet = -1,
			// Token: 0x0400032F RID: 815
			AM,
			// Token: 0x04000330 RID: 816
			PM
		}

		// Token: 0x02000115 RID: 277
		internal enum DS
		{
			// Token: 0x04000332 RID: 818
			BEGIN,
			// Token: 0x04000333 RID: 819
			N,
			// Token: 0x04000334 RID: 820
			NN,
			// Token: 0x04000335 RID: 821
			D_Nd,
			// Token: 0x04000336 RID: 822
			D_NN,
			// Token: 0x04000337 RID: 823
			D_NNd,
			// Token: 0x04000338 RID: 824
			D_M,
			// Token: 0x04000339 RID: 825
			D_MN,
			// Token: 0x0400033A RID: 826
			D_NM,
			// Token: 0x0400033B RID: 827
			D_MNd,
			// Token: 0x0400033C RID: 828
			D_NDS,
			// Token: 0x0400033D RID: 829
			D_Y,
			// Token: 0x0400033E RID: 830
			D_YN,
			// Token: 0x0400033F RID: 831
			D_YNd,
			// Token: 0x04000340 RID: 832
			D_YM,
			// Token: 0x04000341 RID: 833
			D_YMd,
			// Token: 0x04000342 RID: 834
			D_S,
			// Token: 0x04000343 RID: 835
			T_S,
			// Token: 0x04000344 RID: 836
			T_Nt,
			// Token: 0x04000345 RID: 837
			T_NNt,
			// Token: 0x04000346 RID: 838
			ERROR,
			// Token: 0x04000347 RID: 839
			DX_NN,
			// Token: 0x04000348 RID: 840
			DX_NNN,
			// Token: 0x04000349 RID: 841
			DX_MN,
			// Token: 0x0400034A RID: 842
			DX_NM,
			// Token: 0x0400034B RID: 843
			DX_MNN,
			// Token: 0x0400034C RID: 844
			DX_DS,
			// Token: 0x0400034D RID: 845
			DX_DSN,
			// Token: 0x0400034E RID: 846
			DX_NDS,
			// Token: 0x0400034F RID: 847
			DX_NNDS,
			// Token: 0x04000350 RID: 848
			DX_YNN,
			// Token: 0x04000351 RID: 849
			DX_YMN,
			// Token: 0x04000352 RID: 850
			DX_YN,
			// Token: 0x04000353 RID: 851
			DX_YM,
			// Token: 0x04000354 RID: 852
			TX_N,
			// Token: 0x04000355 RID: 853
			TX_NN,
			// Token: 0x04000356 RID: 854
			TX_NNN,
			// Token: 0x04000357 RID: 855
			TX_TS,
			// Token: 0x04000358 RID: 856
			DX_NNY
		}
	}
}
