using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System
{
	// Token: 0x02000110 RID: 272
	internal static class DateTimeFormat
	{
		// Token: 0x06000E34 RID: 3636 RVA: 0x000D0324 File Offset: 0x000CF524
		internal static void FormatDigits(StringBuilder outputBuffer, int value, int len)
		{
			DateTimeFormat.FormatDigits(outputBuffer, value, len, false);
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x000D0330 File Offset: 0x000CF530
		internal unsafe static void FormatDigits(StringBuilder outputBuffer, int value, int len, bool overrideLengthLimit)
		{
			if (!overrideLengthLimit && len > 2)
			{
				len = 2;
			}
			char* ptr = stackalloc char[(UIntPtr)32];
			char* ptr2 = ptr + 16;
			int num = value;
			do
			{
				*(--ptr2) = (char)(num % 10 + 48);
				num /= 10;
			}
			while (num != 0 && ptr2 != ptr);
			int num2 = (int)((long)(ptr + 16 - ptr2));
			while (num2 < len && ptr2 != ptr)
			{
				*(--ptr2) = '0';
				num2++;
			}
			outputBuffer.Append(ptr2, num2);
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x000D039E File Offset: 0x000CF59E
		private static void HebrewFormatDigits(StringBuilder outputBuffer, int digits)
		{
			HebrewNumber.Append(outputBuffer, digits);
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x000D03A8 File Offset: 0x000CF5A8
		internal unsafe static int ParseRepeatPattern(ReadOnlySpan<char> format, int pos, char patternChar)
		{
			int length = format.Length;
			int num = pos + 1;
			while (num < length && *format[num] == (ushort)patternChar)
			{
				num++;
			}
			return num - pos;
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x000D03DA File Offset: 0x000CF5DA
		private static string FormatDayOfWeek(int dayOfWeek, int repeat, DateTimeFormatInfo dtfi)
		{
			if (repeat == 3)
			{
				return dtfi.GetAbbreviatedDayName((DayOfWeek)dayOfWeek);
			}
			return dtfi.GetDayName((DayOfWeek)dayOfWeek);
		}

		// Token: 0x06000E39 RID: 3641 RVA: 0x000D03EF File Offset: 0x000CF5EF
		private static string FormatMonth(int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x000D0404 File Offset: 0x000CF604
		private static string FormatHebrewMonthName(DateTime time, int month, int repeatCount, DateTimeFormatInfo dtfi)
		{
			if (dtfi.Calendar.IsLeapYear(dtfi.Calendar.GetYear(time)))
			{
				return dtfi.InternalGetMonthName(month, MonthNameStyles.LeapYear, repeatCount == 3);
			}
			if (month >= 7)
			{
				month++;
			}
			if (repeatCount == 3)
			{
				return dtfi.GetAbbreviatedMonthName(month);
			}
			return dtfi.GetMonthName(month);
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x000D0454 File Offset: 0x000CF654
		internal unsafe static int ParseQuoteString(ReadOnlySpan<char> format, int pos, StringBuilder result)
		{
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
						throw new FormatException(SR.Format_InvalidString);
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
				throw new FormatException(SR.Format(SR.Format_BadQuote, c));
			}
			return pos - num;
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000D04EC File Offset: 0x000CF6EC
		internal unsafe static int ParseNextChar(ReadOnlySpan<char> format, int pos)
		{
			if (pos >= format.Length - 1)
			{
				return -1;
			}
			return (int)(*format[pos + 1]);
		}

		// Token: 0x06000E3D RID: 3645 RVA: 0x000D0508 File Offset: 0x000CF708
		private unsafe static bool IsUseGenitiveForm(ReadOnlySpan<char> format, int index, int tokenLen, char patternToMatch)
		{
			int num = 0;
			int num2 = index - 1;
			while (num2 >= 0 && *format[num2] != (ushort)patternToMatch)
			{
				num2--;
			}
			if (num2 >= 0)
			{
				while (--num2 >= 0 && *format[num2] == (ushort)patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			num2 = index + tokenLen;
			while (num2 < format.Length && *format[num2] != (ushort)patternToMatch)
			{
				num2++;
			}
			if (num2 < format.Length)
			{
				num = 0;
				while (++num2 < format.Length && *format[num2] == (ushort)patternToMatch)
				{
					num++;
				}
				if (num <= 1)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06000E3E RID: 3646 RVA: 0x000D05A8 File Offset: 0x000CF7A8
		private unsafe static StringBuilder FormatCustomized(DateTime dateTime, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, TimeSpan offset, StringBuilder result)
		{
			Calendar calendar = dtfi.Calendar;
			bool flag = false;
			if (result == null)
			{
				flag = true;
				result = StringBuilderCache.Acquire(16);
			}
			bool flag2 = calendar.ID == CalendarId.HEBREW;
			bool flag3 = calendar.ID == CalendarId.JAPAN;
			bool timeOnly = true;
			int i = 0;
			while (i < format.Length)
			{
				char c = (char)(*format[i]);
				int num2;
				if (c <= 'K')
				{
					if (c <= '/')
					{
						if (c <= '%')
						{
							if (c != '"')
							{
								if (c != '%')
								{
									goto IL_673;
								}
								int num = DateTimeFormat.ParseNextChar(format, i);
								if (num >= 0 && num != 37)
								{
									char c2 = (char)num;
									StringBuilder stringBuilder = DateTimeFormat.FormatCustomized(dateTime, MemoryMarshal.CreateReadOnlySpan<char>(ref c2, 1), dtfi, offset, result);
									num2 = 2;
									goto IL_680;
								}
								if (flag)
								{
									StringBuilderCache.Release(result);
								}
								throw new FormatException(SR.Format_InvalidString);
							}
						}
						else if (c != '\'')
						{
							if (c != '/')
							{
								goto IL_673;
							}
							result.Append(dtfi.DateSeparator);
							num2 = 1;
							goto IL_680;
						}
						num2 = DateTimeFormat.ParseQuoteString(format, i, result);
					}
					else if (c <= 'F')
					{
						if (c != ':')
						{
							if (c != 'F')
							{
								goto IL_673;
							}
							goto IL_1F3;
						}
						else
						{
							result.Append(dtfi.TimeSeparator);
							num2 = 1;
						}
					}
					else if (c != 'H')
					{
						if (c != 'K')
						{
							goto IL_673;
						}
						num2 = 1;
						DateTimeFormat.FormatCustomizedRoundripTimeZone(dateTime, offset, result);
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(result, dateTime.Hour, num2);
					}
				}
				else if (c <= 'm')
				{
					if (c <= '\\')
					{
						if (c != 'M')
						{
							if (c != '\\')
							{
								goto IL_673;
							}
							int num = DateTimeFormat.ParseNextChar(format, i);
							if (num < 0)
							{
								if (flag)
								{
									StringBuilderCache.Release(result);
								}
								throw new FormatException(SR.Format_InvalidString);
							}
							result.Append((char)num);
							num2 = 2;
						}
						else
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int month = calendar.GetMonth(dateTime);
							if (num2 <= 2)
							{
								if (flag2)
								{
									DateTimeFormat.HebrewFormatDigits(result, month);
								}
								else
								{
									DateTimeFormat.FormatDigits(result, month, num2);
								}
							}
							else if (flag2)
							{
								result.Append(DateTimeFormat.FormatHebrewMonthName(dateTime, month, num2, dtfi));
							}
							else if ((dtfi.FormatFlags & DateTimeFormatFlags.UseGenitiveMonth) != DateTimeFormatFlags.None)
							{
								result.Append(dtfi.InternalGetMonthName(month, DateTimeFormat.IsUseGenitiveForm(format, i, num2, 'd') ? MonthNameStyles.Genitive : MonthNameStyles.Regular, num2 == 3));
							}
							else
							{
								result.Append(DateTimeFormat.FormatMonth(month, num2, dtfi));
							}
							timeOnly = false;
						}
					}
					else
					{
						switch (c)
						{
						case 'd':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num2 <= 2)
							{
								int dayOfMonth = calendar.GetDayOfMonth(dateTime);
								if (flag2)
								{
									DateTimeFormat.HebrewFormatDigits(result, dayOfMonth);
								}
								else
								{
									DateTimeFormat.FormatDigits(result, dayOfMonth, num2);
								}
							}
							else
							{
								int dayOfWeek = (int)calendar.GetDayOfWeek(dateTime);
								result.Append(DateTimeFormat.FormatDayOfWeek(dayOfWeek, num2, dtfi));
							}
							timeOnly = false;
							break;
						case 'e':
							goto IL_673;
						case 'f':
							goto IL_1F3;
						case 'g':
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							result.Append(dtfi.GetEraName(calendar.GetEra(dateTime)));
							break;
						case 'h':
						{
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							int num3 = dateTime.Hour % 12;
							if (num3 == 0)
							{
								num3 = 12;
							}
							DateTimeFormat.FormatDigits(result, num3, num2);
							break;
						}
						default:
							if (c != 'm')
							{
								goto IL_673;
							}
							num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							DateTimeFormat.FormatDigits(result, dateTime.Minute, num2);
							break;
						}
					}
				}
				else if (c <= 't')
				{
					if (c != 's')
					{
						if (c != 't')
						{
							goto IL_673;
						}
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num2 == 1)
						{
							if (dateTime.Hour < 12)
							{
								if (dtfi.AMDesignator.Length >= 1)
								{
									result.Append(dtfi.AMDesignator[0]);
								}
							}
							else if (dtfi.PMDesignator.Length >= 1)
							{
								result.Append(dtfi.PMDesignator[0]);
							}
						}
						else
						{
							result.Append((dateTime.Hour < 12) ? dtfi.AMDesignator : dtfi.PMDesignator);
						}
					}
					else
					{
						num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						DateTimeFormat.FormatDigits(result, dateTime.Second, num2);
					}
				}
				else if (c != 'y')
				{
					if (c != 'z')
					{
						goto IL_673;
					}
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					DateTimeFormat.FormatCustomizedTimeZone(dateTime, offset, num2, timeOnly, result);
				}
				else
				{
					int year = calendar.GetYear(dateTime);
					num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (flag3 && !LocalAppContextSwitches.FormatJapaneseFirstYearAsANumber && year == 1 && ((i + num2 < format.Length && *format[i + num2] == 24180) || (i + num2 < format.Length - 1 && *format[i + num2] == 39 && *format[i + num2 + 1] == 24180)))
					{
						result.Append("元"[0]);
					}
					else if (dtfi.HasForceTwoDigitYears)
					{
						DateTimeFormat.FormatDigits(result, year, (num2 <= 2) ? num2 : 2);
					}
					else if (calendar.ID == CalendarId.HEBREW)
					{
						DateTimeFormat.HebrewFormatDigits(result, year);
					}
					else if (num2 <= 2)
					{
						DateTimeFormat.FormatDigits(result, year % 100, num2);
					}
					else if (num2 <= 16)
					{
						DateTimeFormat.FormatDigits(result, year, num2, true);
					}
					else
					{
						result.Append(year.ToString("D" + num2.ToString(), CultureInfo.InvariantCulture));
					}
					timeOnly = false;
				}
				IL_680:
				i += num2;
				continue;
				IL_1F3:
				num2 = DateTimeFormat.ParseRepeatPattern(format, i, c);
				if (num2 > 7)
				{
					if (flag)
					{
						StringBuilderCache.Release(result);
					}
					throw new FormatException(SR.Format_InvalidString);
				}
				long num4 = dateTime.Ticks % 10000000L;
				num4 /= (long)Math.Pow(10.0, (double)(7 - num2));
				if (c == 'f')
				{
					result.AppendSpanFormattable<int>((int)num4, DateTimeFormat.fixedNumberFormats[num2 - 1], CultureInfo.InvariantCulture);
					goto IL_680;
				}
				int num5 = num2;
				while (num5 > 0 && num4 % 10L == 0L)
				{
					num4 /= 10L;
					num5--;
				}
				if (num5 > 0)
				{
					result.AppendSpanFormattable<int>((int)num4, DateTimeFormat.fixedNumberFormats[num5 - 1], CultureInfo.InvariantCulture);
					goto IL_680;
				}
				if (result.Length > 0 && result[result.Length - 1] == '.')
				{
					result.Remove(result.Length - 1, 1);
					goto IL_680;
				}
				goto IL_680;
				IL_673:
				result.Append(c);
				num2 = 1;
				goto IL_680;
			}
			return result;
		}

		// Token: 0x06000E3F RID: 3647 RVA: 0x000D0C4C File Offset: 0x000CFE4C
		private static void FormatCustomizedTimeZone(DateTime dateTime, TimeSpan offset, int tokenLen, bool timeOnly, StringBuilder result)
		{
			bool flag = offset.Ticks == long.MinValue;
			if (flag)
			{
				if (timeOnly && dateTime.Ticks < 864000000000L)
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(DateTime.Now, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
				else if (dateTime.Kind == DateTimeKind.Utc)
				{
					offset = default(TimeSpan);
				}
				else
				{
					offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
				}
			}
			if (offset.Ticks >= 0L)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			if (tokenLen <= 1)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, "{0:0}", offset.Hours);
				return;
			}
			result.AppendFormat(CultureInfo.InvariantCulture, "{0:00}", offset.Hours);
			if (tokenLen >= 3)
			{
				result.AppendFormat(CultureInfo.InvariantCulture, ":{0:00}", offset.Minutes);
			}
		}

		// Token: 0x06000E40 RID: 3648 RVA: 0x000D0D40 File Offset: 0x000CFF40
		private static void FormatCustomizedRoundripTimeZone(DateTime dateTime, TimeSpan offset, StringBuilder result)
		{
			if (offset.Ticks == -9223372036854775808L)
			{
				DateTimeKind kind = dateTime.Kind;
				if (kind == DateTimeKind.Utc)
				{
					result.Append('Z');
					return;
				}
				if (kind != DateTimeKind.Local)
				{
					return;
				}
				offset = TimeZoneInfo.GetLocalUtcOffset(dateTime, TimeZoneInfoOptions.NoThrowOnInvalidTime);
			}
			if (offset.Ticks >= 0L)
			{
				result.Append('+');
			}
			else
			{
				result.Append('-');
				offset = offset.Negate();
			}
			DateTimeFormat.Append2DigitNumber(result, offset.Hours);
			result.Append(':');
			DateTimeFormat.Append2DigitNumber(result, offset.Minutes);
		}

		// Token: 0x06000E41 RID: 3649 RVA: 0x000D0DD0 File Offset: 0x000CFFD0
		private static void Append2DigitNumber(StringBuilder result, int val)
		{
			result.Append((char)(48 + val / 10));
			result.Append((char)(48 + val % 10));
		}

		// Token: 0x06000E42 RID: 3650 RVA: 0x000D0DF0 File Offset: 0x000CFFF0
		internal unsafe static string GetRealFormat(ReadOnlySpan<char> format, DateTimeFormatInfo dtfi)
		{
			char c = (char)(*format[0]);
			if (c > 'U')
			{
				if (c != 'Y')
				{
					switch (c)
					{
					case 'd':
						return dtfi.ShortDatePattern;
					case 'e':
						goto IL_159;
					case 'f':
						return dtfi.LongDatePattern + " " + dtfi.ShortTimePattern;
					case 'g':
						return dtfi.GeneralShortTimePattern;
					default:
						switch (c)
						{
						case 'm':
							goto IL_109;
						case 'n':
						case 'p':
						case 'q':
						case 'v':
						case 'w':
						case 'x':
							goto IL_159;
						case 'o':
							goto IL_112;
						case 'r':
							goto IL_11A;
						case 's':
							return dtfi.SortableDateTimePattern;
						case 't':
							return dtfi.ShortTimePattern;
						case 'u':
							return dtfi.UniversalSortableDateTimePattern;
						case 'y':
							break;
						default:
							goto IL_159;
						}
						break;
					}
				}
				return dtfi.YearMonthPattern;
			}
			switch (c)
			{
			case 'D':
				return dtfi.LongDatePattern;
			case 'E':
				goto IL_159;
			case 'F':
				return dtfi.FullDateTimePattern;
			case 'G':
				return dtfi.GeneralLongTimePattern;
			default:
				switch (c)
				{
				case 'M':
					break;
				case 'N':
				case 'P':
				case 'Q':
				case 'S':
					goto IL_159;
				case 'O':
					goto IL_112;
				case 'R':
					goto IL_11A;
				case 'T':
					return dtfi.LongTimePattern;
				case 'U':
					return dtfi.FullDateTimePattern;
				default:
					goto IL_159;
				}
				break;
			}
			IL_109:
			return dtfi.MonthDayPattern;
			IL_112:
			return "yyyy'-'MM'-'dd'T'HH':'mm':'ss.fffffffK";
			IL_11A:
			return dtfi.RFC1123Pattern;
			IL_159:
			throw new FormatException(SR.Format_InvalidString);
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x000D0F64 File Offset: 0x000D0164
		private unsafe static string ExpandPredefinedFormat(ReadOnlySpan<char> format, ref DateTime dateTime, ref DateTimeFormatInfo dtfi, TimeSpan offset)
		{
			char c = (char)(*format[0]);
			if (c <= 'R')
			{
				if (c != 'O')
				{
					if (c != 'R')
					{
						goto IL_EF;
					}
					goto IL_59;
				}
			}
			else if (c != 'U')
			{
				switch (c)
				{
				case 'o':
					break;
				case 'p':
				case 'q':
				case 't':
					goto IL_EF;
				case 'r':
				case 'u':
					goto IL_59;
				case 's':
					dtfi = DateTimeFormatInfo.InvariantInfo;
					goto IL_EF;
				default:
					goto IL_EF;
				}
			}
			else
			{
				if (offset.Ticks != -9223372036854775808L)
				{
					throw new FormatException(SR.Format_InvalidString);
				}
				dtfi = (DateTimeFormatInfo)dtfi.Clone();
				if (dtfi.Calendar.GetType() != typeof(GregorianCalendar))
				{
					dtfi.Calendar = GregorianCalendar.GetDefaultInstance();
				}
				dateTime = dateTime.ToUniversalTime();
				goto IL_EF;
			}
			dtfi = DateTimeFormatInfo.InvariantInfo;
			goto IL_EF;
			IL_59:
			if (offset.Ticks != -9223372036854775808L)
			{
				dateTime -= offset;
			}
			dtfi = DateTimeFormatInfo.InvariantInfo;
			IL_EF:
			return DateTimeFormat.GetRealFormat(format, dtfi);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x000D1068 File Offset: 0x000D0268
		internal static string Format(DateTime dateTime, string format, IFormatProvider provider)
		{
			return DateTimeFormat.Format(dateTime, format, provider, new TimeSpan(long.MinValue));
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000D1080 File Offset: 0x000D0280
		internal unsafe static string Format(DateTime dateTime, string format, IFormatProvider provider, TimeSpan offset)
		{
			if (format != null && format.Length == 1)
			{
				char c = format[0];
				if (c <= 'R')
				{
					if (c != 'O')
					{
						if (c != 'R')
						{
							goto IL_97;
						}
						goto IL_72;
					}
				}
				else if (c != 'o')
				{
					if (c != 'r')
					{
						goto IL_97;
					}
					goto IL_72;
				}
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)66], 33);
				Span<char> destination = span;
				int length;
				DateTimeFormat.TryFormatO(dateTime, offset, destination, out length);
				return destination.Slice(0, length).ToString();
				IL_72:
				string text = string.FastAllocateString(29);
				int num;
				DateTimeFormat.TryFormatR(dateTime, offset, new Span<char>(text.GetRawStringData(), text.Length), out num);
				return text;
			}
			IL_97:
			DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(provider);
			return StringBuilderCache.GetStringAndRelease(DateTimeFormat.FormatStringBuilder(dateTime, format, instance, offset));
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x000D113E File Offset: 0x000D033E
		internal static bool TryFormat(DateTime dateTime, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
		{
			return DateTimeFormat.TryFormat(dateTime, destination, out charsWritten, format, provider, new TimeSpan(long.MinValue));
		}

		// Token: 0x06000E47 RID: 3655 RVA: 0x000D115C File Offset: 0x000D035C
		internal unsafe static bool TryFormat(DateTime dateTime, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider, TimeSpan offset)
		{
			if (format.Length == 1)
			{
				char c = (char)(*format[0]);
				if (c <= 'R')
				{
					if (c != 'O')
					{
						if (c != 'R')
						{
							goto IL_47;
						}
						goto IL_3C;
					}
				}
				else if (c != 'o')
				{
					if (c != 'r')
					{
						goto IL_47;
					}
					goto IL_3C;
				}
				return DateTimeFormat.TryFormatO(dateTime, offset, destination, out charsWritten);
				IL_3C:
				return DateTimeFormat.TryFormatR(dateTime, offset, destination, out charsWritten);
			}
			IL_47:
			DateTimeFormatInfo instance = DateTimeFormatInfo.GetInstance(provider);
			StringBuilder stringBuilder = DateTimeFormat.FormatStringBuilder(dateTime, format, instance, offset);
			bool flag = stringBuilder.Length <= destination.Length;
			if (flag)
			{
				stringBuilder.CopyTo(0, destination, stringBuilder.Length);
				charsWritten = stringBuilder.Length;
			}
			else
			{
				charsWritten = 0;
			}
			StringBuilderCache.Release(stringBuilder);
			return flag;
		}

		// Token: 0x06000E48 RID: 3656 RVA: 0x000D11FC File Offset: 0x000D03FC
		private static StringBuilder FormatStringBuilder(DateTime dateTime, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, TimeSpan offset)
		{
			if (format.Length == 0)
			{
				bool flag = false;
				if (dateTime.Ticks < 864000000000L)
				{
					CalendarId id = dtfi.Calendar.ID;
					switch (id)
					{
					case CalendarId.JAPAN:
					case CalendarId.TAIWAN:
					case CalendarId.HIJRI:
					case CalendarId.HEBREW:
						break;
					case CalendarId.KOREA:
					case CalendarId.THAI:
						goto IL_61;
					default:
						if (id != CalendarId.JULIAN && id - CalendarId.PERSIAN > 1)
						{
							goto IL_61;
						}
						break;
					}
					flag = true;
					dtfi = DateTimeFormatInfo.InvariantInfo;
				}
				IL_61:
				if (offset.Ticks == -9223372036854775808L)
				{
					format = (flag ? "s" : "G");
				}
				else
				{
					format = (flag ? "yyyy'-'MM'-'ddTHH':'mm':'ss zzz" : dtfi.DateTimeOffsetPattern);
				}
			}
			if (format.Length == 1)
			{
				format = DateTimeFormat.ExpandPredefinedFormat(format, ref dateTime, ref dtfi, offset);
			}
			return DateTimeFormat.FormatCustomized(dateTime, format, dtfi, offset, null);
		}

		// Token: 0x06000E49 RID: 3657 RVA: 0x000D12D4 File Offset: 0x000D04D4
		private unsafe static bool TryFormatO(DateTime dateTime, TimeSpan offset, Span<char> destination, out int charsWritten)
		{
			int num = 27;
			DateTimeKind dateTimeKind = DateTimeKind.Local;
			if (offset.Ticks == -9223372036854775808L)
			{
				dateTimeKind = dateTime.Kind;
				if (dateTimeKind == DateTimeKind.Local)
				{
					offset = TimeZoneInfo.Local.GetUtcOffset(dateTime);
					num += 6;
				}
				else if (dateTimeKind == DateTimeKind.Utc)
				{
					num++;
				}
			}
			else
			{
				num += 6;
			}
			if (destination.Length < num)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = num;
			ref char ptr = ref destination[26];
			int value;
			int value2;
			int value3;
			dateTime.GetDate(out value, out value2, out value3);
			int value4;
			int value5;
			int value6;
			int num2;
			dateTime.GetTimePrecise(out value4, out value5, out value6, out num2);
			DateTimeFormat.WriteFourDecimalDigits((uint)value, destination, 0);
			*destination[4] = '-';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value2, destination, 5);
			*destination[7] = '-';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value3, destination, 8);
			*destination[10] = 'T';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value4, destination, 11);
			*destination[13] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value5, destination, 14);
			*destination[16] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value6, destination, 17);
			*destination[19] = '.';
			DateTimeFormat.WriteDigits((ulong)num2, destination.Slice(20, 7));
			if (dateTimeKind == DateTimeKind.Local)
			{
				int num3 = (int)(offset.Ticks / 600000000L);
				char c;
				if (num3 < 0)
				{
					c = '-';
					num3 = -num3;
				}
				else
				{
					c = '+';
				}
				int value8;
				int value7 = Math.DivRem(num3, 60, out value8);
				DateTimeFormat.WriteTwoDecimalDigits((uint)value8, destination, 31);
				*destination[30] = ':';
				DateTimeFormat.WriteTwoDecimalDigits((uint)value7, destination, 28);
				*destination[27] = c;
			}
			else if (dateTimeKind == DateTimeKind.Utc)
			{
				*destination[27] = 'Z';
			}
			return true;
		}

		// Token: 0x06000E4A RID: 3658 RVA: 0x000D1464 File Offset: 0x000D0664
		private unsafe static bool TryFormatR(DateTime dateTime, TimeSpan offset, Span<char> destination, out int charsWritten)
		{
			if (28 >= destination.Length)
			{
				charsWritten = 0;
				return false;
			}
			if (offset.Ticks != -9223372036854775808L)
			{
				dateTime -= offset;
			}
			int value;
			int num;
			int value2;
			dateTime.GetDate(out value, out num, out value2);
			int value3;
			int value4;
			int value5;
			dateTime.GetTime(out value3, out value4, out value5);
			string text = DateTimeFormat.InvariantAbbreviatedDayNames[(int)dateTime.DayOfWeek];
			string text2 = DateTimeFormat.InvariantAbbreviatedMonthNames[num - 1];
			*destination[0] = text[0];
			*destination[1] = text[1];
			*destination[2] = text[2];
			*destination[3] = ',';
			*destination[4] = ' ';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value2, destination, 5);
			*destination[7] = ' ';
			*destination[8] = text2[0];
			*destination[9] = text2[1];
			*destination[10] = text2[2];
			*destination[11] = ' ';
			DateTimeFormat.WriteFourDecimalDigits((uint)value, destination, 12);
			*destination[16] = ' ';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value3, destination, 17);
			*destination[19] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value4, destination, 20);
			*destination[22] = ':';
			DateTimeFormat.WriteTwoDecimalDigits((uint)value5, destination, 23);
			*destination[25] = ' ';
			*destination[26] = 'G';
			*destination[27] = 'M';
			*destination[28] = 'T';
			charsWritten = 29;
			return true;
		}

		// Token: 0x06000E4B RID: 3659 RVA: 0x000D15EC File Offset: 0x000D07EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteTwoDecimalDigits(uint value, Span<char> destination, int offset)
		{
			uint num = 48U + value;
			value /= 10U;
			*destination[offset + 1] = (char)(num - value * 10U);
			*destination[offset] = (char)(48U + value);
		}

		// Token: 0x06000E4C RID: 3660 RVA: 0x000D1624 File Offset: 0x000D0824
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteFourDecimalDigits(uint value, Span<char> buffer, int startingIndex = 0)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 3] = (char)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 2] = (char)(num - value * 10U);
			num = 48U + value;
			value /= 10U;
			*buffer[startingIndex + 1] = (char)(num - value * 10U);
			*buffer[startingIndex] = (char)(48U + value);
		}

		// Token: 0x06000E4D RID: 3661 RVA: 0x000D1698 File Offset: 0x000D0898
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteDigits(ulong value, Span<char> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				ulong num = 48UL + value;
				value /= 10UL;
				*buffer[i] = (char)(num - value * 10UL);
			}
			*buffer[0] = (char)(48UL + value);
		}

		// Token: 0x06000E4E RID: 3662 RVA: 0x000D16E8 File Offset: 0x000D08E8
		internal static string[] GetAllDateTimes(DateTime dateTime, char format, DateTimeFormatInfo dtfi)
		{
			string[] allDateTimePatterns;
			string[] array;
			if (format <= 'U')
			{
				switch (format)
				{
				case 'D':
				case 'F':
				case 'G':
					break;
				case 'E':
					goto IL_133;
				default:
					switch (format)
					{
					case 'M':
					case 'T':
						break;
					case 'N':
					case 'P':
					case 'Q':
					case 'S':
						goto IL_133;
					case 'O':
					case 'R':
						goto IL_11A;
					case 'U':
					{
						DateTime dateTime2 = dateTime.ToUniversalTime();
						allDateTimePatterns = dtfi.GetAllDateTimePatterns(format);
						array = new string[allDateTimePatterns.Length];
						for (int i = 0; i < allDateTimePatterns.Length; i++)
						{
							array[i] = DateTimeFormat.Format(dateTime2, allDateTimePatterns[i], dtfi);
						}
						return array;
					}
					default:
						goto IL_133;
					}
					break;
				}
			}
			else if (format != 'Y')
			{
				switch (format)
				{
				case 'd':
				case 'f':
				case 'g':
					break;
				case 'e':
					goto IL_133;
				default:
					switch (format)
					{
					case 'm':
					case 't':
					case 'y':
						break;
					case 'n':
					case 'p':
					case 'q':
					case 'v':
					case 'w':
					case 'x':
						goto IL_133;
					case 'o':
					case 'r':
					case 's':
					case 'u':
						goto IL_11A;
					default:
						goto IL_133;
					}
					break;
				}
			}
			allDateTimePatterns = dtfi.GetAllDateTimePatterns(format);
			array = new string[allDateTimePatterns.Length];
			for (int j = 0; j < allDateTimePatterns.Length; j++)
			{
				array[j] = DateTimeFormat.Format(dateTime, allDateTimePatterns[j], dtfi);
			}
			return array;
			IL_11A:
			return new string[]
			{
				DateTimeFormat.Format(dateTime, char.ToString(format), dtfi)
			};
			IL_133:
			throw new FormatException(SR.Format_InvalidString);
		}

		// Token: 0x06000E4F RID: 3663 RVA: 0x000D1834 File Offset: 0x000D0A34
		internal static string[] GetAllDateTimes(DateTime dateTime, DateTimeFormatInfo dtfi)
		{
			List<string> list = new List<string>(132);
			for (int i = 0; i < DateTimeFormat.allStandardFormats.Length; i++)
			{
				string[] allDateTimes = DateTimeFormat.GetAllDateTimes(dateTime, DateTimeFormat.allStandardFormats[i], dtfi);
				for (int j = 0; j < allDateTimes.Length; j++)
				{
					list.Add(allDateTimes[j]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x04000310 RID: 784
		internal static char[] allStandardFormats = new char[]
		{
			'd',
			'D',
			'f',
			'F',
			'g',
			'G',
			'm',
			'M',
			'o',
			'O',
			'r',
			'R',
			's',
			't',
			'T',
			'u',
			'U',
			'y',
			'Y'
		};

		// Token: 0x04000311 RID: 785
		internal static readonly DateTimeFormatInfo InvariantFormatInfo = CultureInfo.InvariantCulture.DateTimeFormat;

		// Token: 0x04000312 RID: 786
		internal static readonly string[] InvariantAbbreviatedMonthNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedMonthNames;

		// Token: 0x04000313 RID: 787
		internal static readonly string[] InvariantAbbreviatedDayNames = DateTimeFormat.InvariantFormatInfo.AbbreviatedDayNames;

		// Token: 0x04000314 RID: 788
		internal static string[] fixedNumberFormats = new string[]
		{
			"0",
			"00",
			"000",
			"0000",
			"00000",
			"000000",
			"0000000"
		};
	}
}
