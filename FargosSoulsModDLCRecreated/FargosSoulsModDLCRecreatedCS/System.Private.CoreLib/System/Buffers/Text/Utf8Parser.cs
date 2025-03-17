using System;
using System.Buffers.Binary;
using Internal.Runtime.CompilerServices;

namespace System.Buffers.Text
{
	// Token: 0x0200025B RID: 603
	public static class Utf8Parser
	{
		// Token: 0x060024A6 RID: 9382 RVA: 0x0013BC28 File Offset: 0x0013AE28
		public unsafe static bool TryParse(ReadOnlySpan<byte> source, out bool value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat != '\0' && standardFormat != 'G' && standardFormat != 'l')
			{
				ThrowHelper.ThrowFormatException_BadFormatSpecifier();
			}
			if (source.Length >= 4)
			{
				int num = BinaryPrimitives.ReadInt32LittleEndian(source) & -538976289;
				if (num == 1163219540)
				{
					bytesConsumed = 4;
					value = true;
					return true;
				}
				if (4 < source.Length && num == 1397506374 && ((int)(*source[4]) & -33) == 69)
				{
					bytesConsumed = 5;
					value = false;
					return true;
				}
			}
			bytesConsumed = 0;
			value = false;
			return false;
		}

		// Token: 0x060024A7 RID: 9383 RVA: 0x0013BCA0 File Offset: 0x0013AEA0
		public static bool TryParse(ReadOnlySpan<byte> source, out DateTime value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'G')
			{
				if (standardFormat == '\0' || standardFormat == 'G')
				{
					DateTimeOffset dateTimeOffset;
					return Utf8Parser.TryParseDateTimeG(source, out value, out dateTimeOffset, out bytesConsumed);
				}
			}
			else if (standardFormat != 'O')
			{
				if (standardFormat != 'R')
				{
					if (standardFormat == 'l')
					{
						DateTimeOffset dateTimeOffset2;
						if (!Utf8Parser.TryParseDateTimeOffsetR(source, 32U, out dateTimeOffset2, out bytesConsumed))
						{
							value = default(DateTime);
							return false;
						}
						value = dateTimeOffset2.DateTime;
						return true;
					}
				}
				else
				{
					DateTimeOffset dateTimeOffset3;
					if (!Utf8Parser.TryParseDateTimeOffsetR(source, 0U, out dateTimeOffset3, out bytesConsumed))
					{
						value = default(DateTime);
						return false;
					}
					value = dateTimeOffset3.DateTime;
					return true;
				}
			}
			else
			{
				DateTimeOffset dateTimeOffset4;
				DateTimeKind dateTimeKind;
				if (!Utf8Parser.TryParseDateTimeOffsetO(source, out dateTimeOffset4, out bytesConsumed, out dateTimeKind))
				{
					value = default(DateTime);
					bytesConsumed = 0;
					return false;
				}
				if (dateTimeKind != DateTimeKind.Utc)
				{
					if (dateTimeKind == DateTimeKind.Local)
					{
						value = dateTimeOffset4.LocalDateTime;
					}
					else
					{
						value = dateTimeOffset4.DateTime;
					}
				}
				else
				{
					value = dateTimeOffset4.UtcDateTime;
				}
				return true;
			}
			return ParserHelpers.TryParseThrowFormatException<DateTime>(out value, out bytesConsumed);
		}

		// Token: 0x060024A8 RID: 9384 RVA: 0x0013BD84 File Offset: 0x0013AF84
		public static bool TryParse(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'G')
			{
				if (standardFormat == '\0')
				{
					return Utf8Parser.TryParseDateTimeOffsetDefault(source, out value, out bytesConsumed);
				}
				if (standardFormat == 'G')
				{
					DateTime dateTime;
					return Utf8Parser.TryParseDateTimeG(source, out dateTime, out value, out bytesConsumed);
				}
			}
			else
			{
				if (standardFormat == 'O')
				{
					DateTimeKind dateTimeKind;
					return Utf8Parser.TryParseDateTimeOffsetO(source, out value, out bytesConsumed, out dateTimeKind);
				}
				if (standardFormat == 'R')
				{
					return Utf8Parser.TryParseDateTimeOffsetR(source, 0U, out value, out bytesConsumed);
				}
				if (standardFormat == 'l')
				{
					return Utf8Parser.TryParseDateTimeOffsetR(source, 32U, out value, out bytesConsumed);
				}
			}
			return ParserHelpers.TryParseThrowFormatException<DateTimeOffset>(out value, out bytesConsumed);
		}

		// Token: 0x060024A9 RID: 9385 RVA: 0x0013BDF8 File Offset: 0x0013AFF8
		private unsafe static bool TryParseDateTimeOffsetDefault(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed)
		{
			if (source.Length < 26)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			DateTime dateTime;
			DateTimeOffset dateTimeOffset;
			int num;
			if (!Utf8Parser.TryParseDateTimeG(source, out dateTime, out dateTimeOffset, out num))
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			if (*source[19] != 32)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			byte b = *source[20];
			if (b != 43 && b != 45)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			uint num2 = (uint)(*source[21] - 48);
			uint num3 = (uint)(*source[22] - 48);
			if (num2 > 9U || num3 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			int offsetHours = (int)(num2 * 10U + num3);
			if (*source[23] != 58)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			uint num4 = (uint)(*source[24] - 48);
			uint num5 = (uint)(*source[25] - 48);
			if (num4 > 9U || num5 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			int offsetMinutes = (int)(num4 * 10U + num5);
			if (!Utf8Parser.TryCreateDateTimeOffset(dateTime, b == 45, offsetHours, offsetMinutes, out value))
			{
				bytesConsumed = 0;
				value = default(DateTimeOffset);
				return false;
			}
			bytesConsumed = 26;
			return true;
		}

		// Token: 0x060024AA RID: 9386 RVA: 0x0013BF2C File Offset: 0x0013B12C
		private unsafe static bool TryParseDateTimeG(ReadOnlySpan<byte> source, out DateTime value, out DateTimeOffset valueAsOffset, out int bytesConsumed)
		{
			if (source.Length < 19)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num = (uint)(*source[0] - 48);
			uint num2 = (uint)(*source[1] - 48);
			if (num > 9U || num2 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int month = (int)(num * 10U + num2);
			if (*source[2] != 47)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num3 = (uint)(*source[3] - 48);
			uint num4 = (uint)(*source[4] - 48);
			if (num3 > 9U || num4 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int day = (int)(num3 * 10U + num4);
			if (*source[5] != 47)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num5 = (uint)(*source[6] - 48);
			uint num6 = (uint)(*source[7] - 48);
			uint num7 = (uint)(*source[8] - 48);
			uint num8 = (uint)(*source[9] - 48);
			if (num5 > 9U || num6 > 9U || num7 > 9U || num8 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int year = (int)(num5 * 1000U + num6 * 100U + num7 * 10U + num8);
			if (*source[10] != 32)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num9 = (uint)(*source[11] - 48);
			uint num10 = (uint)(*source[12] - 48);
			if (num9 > 9U || num10 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int hour = (int)(num9 * 10U + num10);
			if (*source[13] != 58)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num11 = (uint)(*source[14] - 48);
			uint num12 = (uint)(*source[15] - 48);
			if (num11 > 9U || num12 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int minute = (int)(num11 * 10U + num12);
			if (*source[16] != 58)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			uint num13 = (uint)(*source[17] - 48);
			uint num14 = (uint)(*source[18] - 48);
			if (num13 > 9U || num14 > 9U)
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			int second = (int)(num13 * 10U + num14);
			if (!Utf8Parser.TryCreateDateTimeOffsetInterpretingDataAsLocalTime(year, month, day, hour, minute, second, 0, out valueAsOffset))
			{
				bytesConsumed = 0;
				value = default(DateTime);
				valueAsOffset = default(DateTimeOffset);
				return false;
			}
			bytesConsumed = 19;
			value = valueAsOffset.DateTime;
			return true;
		}

		// Token: 0x060024AB RID: 9387 RVA: 0x0013C208 File Offset: 0x0013B408
		private static bool TryCreateDateTimeOffset(DateTime dateTime, bool offsetNegative, int offsetHours, int offsetMinutes, out DateTimeOffset value)
		{
			if (offsetHours > 14)
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (offsetMinutes > 59)
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (offsetHours == 14 && offsetMinutes != 0)
			{
				value = default(DateTimeOffset);
				return false;
			}
			long num = ((long)offsetHours * 3600L + (long)offsetMinutes * 60L) * 10000000L;
			if (offsetNegative)
			{
				num = -num;
			}
			try
			{
				value = new DateTimeOffset(dateTime.Ticks, new TimeSpan(num));
			}
			catch (ArgumentOutOfRangeException)
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		// Token: 0x060024AC RID: 9388 RVA: 0x0013C2A0 File Offset: 0x0013B4A0
		private static bool TryCreateDateTimeOffset(int year, int month, int day, int hour, int minute, int second, int fraction, bool offsetNegative, int offsetHours, int offsetMinutes, out DateTimeOffset value)
		{
			DateTime dateTime;
			if (!Utf8Parser.TryCreateDateTime(year, month, day, hour, minute, second, fraction, DateTimeKind.Unspecified, out dateTime))
			{
				value = default(DateTimeOffset);
				return false;
			}
			if (!Utf8Parser.TryCreateDateTimeOffset(dateTime, offsetNegative, offsetHours, offsetMinutes, out value))
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		// Token: 0x060024AD RID: 9389 RVA: 0x0013C2E8 File Offset: 0x0013B4E8
		private static bool TryCreateDateTimeOffsetInterpretingDataAsLocalTime(int year, int month, int day, int hour, int minute, int second, int fraction, out DateTimeOffset value)
		{
			DateTime dateTime;
			if (!Utf8Parser.TryCreateDateTime(year, month, day, hour, minute, second, fraction, DateTimeKind.Local, out dateTime))
			{
				value = default(DateTimeOffset);
				return false;
			}
			try
			{
				value = new DateTimeOffset(dateTime);
			}
			catch (ArgumentOutOfRangeException)
			{
				value = default(DateTimeOffset);
				return false;
			}
			return true;
		}

		// Token: 0x060024AE RID: 9390 RVA: 0x0013C344 File Offset: 0x0013B544
		private static bool TryCreateDateTime(int year, int month, int day, int hour, int minute, int second, int fraction, DateTimeKind kind, out DateTime value)
		{
			if (year == 0)
			{
				value = default(DateTime);
				return false;
			}
			if (month - 1 >= 12)
			{
				value = default(DateTime);
				return false;
			}
			uint num = (uint)(day - 1);
			if (num >= 28U && (ulong)num >= (ulong)((long)DateTime.DaysInMonth(year, month)))
			{
				value = default(DateTime);
				return false;
			}
			if (hour > 23)
			{
				value = default(DateTime);
				return false;
			}
			if (minute > 59)
			{
				value = default(DateTime);
				return false;
			}
			if (second > 59)
			{
				value = default(DateTime);
				return false;
			}
			int[] array = DateTime.IsLeapYear(year) ? Utf8Parser.s_daysToMonth366 : Utf8Parser.s_daysToMonth365;
			int num2 = year - 1;
			int num3 = num2 * 365 + num2 / 4 - num2 / 100 + num2 / 400 + array[month - 1] + day - 1;
			long num4 = (long)num3 * 864000000000L;
			int num5 = hour * 3600 + minute * 60 + second;
			num4 += (long)num5 * 10000000L;
			num4 += (long)fraction;
			value = new DateTime(num4, kind);
			return true;
		}

		// Token: 0x060024AF RID: 9391 RVA: 0x0013C444 File Offset: 0x0013B644
		private unsafe static bool TryParseDateTimeOffsetO(ReadOnlySpan<byte> source, out DateTimeOffset value, out int bytesConsumed, out DateTimeKind kind)
		{
			if (source.Length < 27)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num = (uint)(*source[0] - 48);
			uint num2 = (uint)(*source[1] - 48);
			uint num3 = (uint)(*source[2] - 48);
			uint num4 = (uint)(*source[3] - 48);
			if (num > 9U || num2 > 9U || num3 > 9U || num4 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int year = (int)(num * 1000U + num2 * 100U + num3 * 10U + num4);
			if (*source[4] != 45)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num5 = (uint)(*source[5] - 48);
			uint num6 = (uint)(*source[6] - 48);
			if (num5 > 9U || num6 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int month = (int)(num5 * 10U + num6);
			if (*source[7] != 45)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num7 = (uint)(*source[8] - 48);
			uint num8 = (uint)(*source[9] - 48);
			if (num7 > 9U || num8 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int day = (int)(num7 * 10U + num8);
			if (*source[10] != 84)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num9 = (uint)(*source[11] - 48);
			uint num10 = (uint)(*source[12] - 48);
			if (num9 > 9U || num10 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int hour = (int)(num9 * 10U + num10);
			if (*source[13] != 58)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num11 = (uint)(*source[14] - 48);
			uint num12 = (uint)(*source[15] - 48);
			if (num11 > 9U || num12 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int minute = (int)(num11 * 10U + num12);
			if (*source[16] != 58)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num13 = (uint)(*source[17] - 48);
			uint num14 = (uint)(*source[18] - 48);
			if (num13 > 9U || num14 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int second = (int)(num13 * 10U + num14);
			if (*source[19] != 46)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			uint num15 = (uint)(*source[20] - 48);
			uint num16 = (uint)(*source[21] - 48);
			uint num17 = (uint)(*source[22] - 48);
			uint num18 = (uint)(*source[23] - 48);
			uint num19 = (uint)(*source[24] - 48);
			uint num20 = (uint)(*source[25] - 48);
			uint num21 = (uint)(*source[26] - 48);
			if (num15 > 9U || num16 > 9U || num17 > 9U || num18 > 9U || num19 > 9U || num20 > 9U || num21 > 9U)
			{
				value = default(DateTimeOffset);
				bytesConsumed = 0;
				kind = DateTimeKind.Unspecified;
				return false;
			}
			int fraction = (int)(num15 * 1000000U + num16 * 100000U + num17 * 10000U + num18 * 1000U + num19 * 100U + num20 * 10U + num21);
			byte b = (source.Length <= 27) ? 0 : (*source[27]);
			if (b != 90 && b != 43 && b != 45)
			{
				if (!Utf8Parser.TryCreateDateTimeOffsetInterpretingDataAsLocalTime(year, month, day, hour, minute, second, fraction, out value))
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				bytesConsumed = 27;
				kind = DateTimeKind.Unspecified;
				return true;
			}
			else if (b == 90)
			{
				if (!Utf8Parser.TryCreateDateTimeOffset(year, month, day, hour, minute, second, fraction, false, 0, 0, out value))
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				bytesConsumed = 28;
				kind = DateTimeKind.Utc;
				return true;
			}
			else
			{
				if (source.Length < 33)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				uint num22 = (uint)(*source[28] - 48);
				uint num23 = (uint)(*source[29] - 48);
				if (num22 > 9U || num23 > 9U)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				int offsetHours = (int)(num22 * 10U + num23);
				if (*source[30] != 58)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				uint num24 = (uint)(*source[31] - 48);
				uint num25 = (uint)(*source[32] - 48);
				if (num24 > 9U || num25 > 9U)
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				int offsetMinutes = (int)(num24 * 10U + num25);
				if (!Utf8Parser.TryCreateDateTimeOffset(year, month, day, hour, minute, second, fraction, b == 45, offsetHours, offsetMinutes, out value))
				{
					value = default(DateTimeOffset);
					bytesConsumed = 0;
					kind = DateTimeKind.Unspecified;
					return false;
				}
				bytesConsumed = 33;
				kind = DateTimeKind.Local;
				return true;
			}
		}

		// Token: 0x060024B0 RID: 9392 RVA: 0x0013C928 File Offset: 0x0013BB28
		private unsafe static bool TryParseDateTimeOffsetR(ReadOnlySpan<byte> source, uint caseFlipXorMask, out DateTimeOffset dateTimeOffset, out int bytesConsumed)
		{
			if (source.Length < 29)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num = (uint)(*source[0]) ^ caseFlipXorMask;
			uint num2 = (uint)(*source[1]);
			uint num3 = (uint)(*source[2]);
			uint num4 = (uint)(*source[3]);
			uint num5 = num << 24 | num2 << 16 | num3 << 8 | num4;
			DayOfWeek dayOfWeek;
			if (num5 <= 1398895660U)
			{
				if (num5 == 1181903148U)
				{
					dayOfWeek = DayOfWeek.Friday;
					goto IL_D9;
				}
				if (num5 == 1299148332U)
				{
					dayOfWeek = DayOfWeek.Monday;
					goto IL_D9;
				}
				if (num5 == 1398895660U)
				{
					dayOfWeek = DayOfWeek.Saturday;
					goto IL_D9;
				}
			}
			else if (num5 <= 1416131884U)
			{
				if (num5 == 1400204844U)
				{
					dayOfWeek = DayOfWeek.Sunday;
					goto IL_D9;
				}
				if (num5 == 1416131884U)
				{
					dayOfWeek = DayOfWeek.Thursday;
					goto IL_D9;
				}
			}
			else
			{
				if (num5 == 1416979756U)
				{
					dayOfWeek = DayOfWeek.Tuesday;
					goto IL_D9;
				}
				if (num5 == 1466262572U)
				{
					dayOfWeek = DayOfWeek.Wednesday;
					goto IL_D9;
				}
			}
			bytesConsumed = 0;
			dateTimeOffset = default(DateTimeOffset);
			return false;
			IL_D9:
			if (*source[4] != 32)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num6 = (uint)(*source[5] - 48);
			uint num7 = (uint)(*source[6] - 48);
			if (num6 > 9U || num7 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int day = (int)(num6 * 10U + num7);
			if (*source[7] != 32)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num8 = (uint)(*source[8]) ^ caseFlipXorMask;
			uint num9 = (uint)(*source[9]);
			uint num10 = (uint)(*source[10]);
			uint num11 = (uint)(*source[11]);
			uint num12 = num8 << 24 | num9 << 16 | num10 << 8 | num11;
			int month;
			if (num12 <= 1249209376U)
			{
				if (num12 <= 1147495200U)
				{
					if (num12 == 1097888288U)
					{
						month = 4;
						goto IL_269;
					}
					if (num12 == 1098213152U)
					{
						month = 8;
						goto IL_269;
					}
					if (num12 == 1147495200U)
					{
						month = 12;
						goto IL_269;
					}
				}
				else
				{
					if (num12 == 1181049376U)
					{
						month = 2;
						goto IL_269;
					}
					if (num12 == 1247899168U)
					{
						month = 1;
						goto IL_269;
					}
					if (num12 == 1249209376U)
					{
						month = 7;
						goto IL_269;
					}
				}
			}
			else if (num12 <= 1298233632U)
			{
				if (num12 == 1249209888U)
				{
					month = 6;
					goto IL_269;
				}
				if (num12 == 1298231840U)
				{
					month = 3;
					goto IL_269;
				}
				if (num12 == 1298233632U)
				{
					month = 5;
					goto IL_269;
				}
			}
			else
			{
				if (num12 == 1315927584U)
				{
					month = 11;
					goto IL_269;
				}
				if (num12 == 1331917856U)
				{
					month = 10;
					goto IL_269;
				}
				if (num12 == 1399156768U)
				{
					month = 9;
					goto IL_269;
				}
			}
			bytesConsumed = 0;
			dateTimeOffset = default(DateTimeOffset);
			return false;
			IL_269:
			uint num13 = (uint)(*source[12] - 48);
			uint num14 = (uint)(*source[13] - 48);
			uint num15 = (uint)(*source[14] - 48);
			uint num16 = (uint)(*source[15] - 48);
			if (num13 > 9U || num14 > 9U || num15 > 9U || num16 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int year = (int)(num13 * 1000U + num14 * 100U + num15 * 10U + num16);
			if (*source[16] != 32)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num17 = (uint)(*source[17] - 48);
			uint num18 = (uint)(*source[18] - 48);
			if (num17 > 9U || num18 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int hour = (int)(num17 * 10U + num18);
			if (*source[19] != 58)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num19 = (uint)(*source[20] - 48);
			uint num20 = (uint)(*source[21] - 48);
			if (num19 > 9U || num20 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int minute = (int)(num19 * 10U + num20);
			if (*source[22] != 58)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			uint num21 = (uint)(*source[23] - 48);
			uint num22 = (uint)(*source[24] - 48);
			if (num21 > 9U || num22 > 9U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			int second = (int)(num21 * 10U + num22);
			uint num23 = (uint)(*source[25]);
			uint num24 = (uint)(*source[26]) ^ caseFlipXorMask;
			uint num25 = (uint)(*source[27]) ^ caseFlipXorMask;
			uint num26 = (uint)(*source[28]) ^ caseFlipXorMask;
			uint num27 = num23 << 24 | num24 << 16 | num25 << 8 | num26;
			if (num27 != 541543764U)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			if (!Utf8Parser.TryCreateDateTimeOffset(year, month, day, hour, minute, second, 0, false, 0, 0, out dateTimeOffset))
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			if (dayOfWeek != dateTimeOffset.DayOfWeek)
			{
				bytesConsumed = 0;
				dateTimeOffset = default(DateTimeOffset);
				return false;
			}
			bytesConsumed = 29;
			return true;
		}

		// Token: 0x060024B1 RID: 9393 RVA: 0x0013CDC0 File Offset: 0x0013BFC0
		public unsafe static bool TryParse(ReadOnlySpan<byte> source, out decimal value, out int bytesConsumed, char standardFormat = '\0')
		{
			Utf8Parser.ParseNumberOptions options;
			if (standardFormat != '\0')
			{
				switch (standardFormat)
				{
				case 'E':
				case 'G':
					goto IL_2F;
				case 'F':
					break;
				default:
					switch (standardFormat)
					{
					case 'e':
					case 'g':
						goto IL_2F;
					case 'f':
						break;
					default:
						return ParserHelpers.TryParseThrowFormatException<decimal>(out value, out bytesConsumed);
					}
					break;
				}
				options = (Utf8Parser.ParseNumberOptions)0;
				goto IL_3F;
			}
			IL_2F:
			options = Utf8Parser.ParseNumberOptions.AllowExponent;
			IL_3F:
			byte* digits = stackalloc byte[(UIntPtr)31];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Decimal, digits, 31);
			bool flag;
			if (!Utf8Parser.TryParseNumber(source, ref numberBuffer, out bytesConsumed, options, out flag))
			{
				value = 0m;
				return false;
			}
			if (!flag && (standardFormat == 'E' || standardFormat == 'e'))
			{
				value = 0m;
				bytesConsumed = 0;
				return false;
			}
			value = 0m;
			if (!Number.TryNumberToDecimal(ref numberBuffer, ref value))
			{
				value = 0m;
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060024B2 RID: 9394 RVA: 0x0013CE6C File Offset: 0x0013C06C
		public unsafe static bool TryParse(ReadOnlySpan<byte> source, out float value, out int bytesConsumed, char standardFormat = '\0')
		{
			byte* digits = stackalloc byte[(UIntPtr)114];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.FloatingPoint, digits, 114);
			if (Utf8Parser.TryParseNormalAsFloatingPoint(source, ref numberBuffer, out bytesConsumed, standardFormat))
			{
				value = Number.NumberToSingle(ref numberBuffer);
				return true;
			}
			return Utf8Parser.TryParseAsSpecialFloatingPoint<float>(source, float.PositiveInfinity, float.NegativeInfinity, float.NaN, out value, out bytesConsumed);
		}

		// Token: 0x060024B3 RID: 9395 RVA: 0x0013CEB8 File Offset: 0x0013C0B8
		public unsafe static bool TryParse(ReadOnlySpan<byte> source, out double value, out int bytesConsumed, char standardFormat = '\0')
		{
			byte* digits = stackalloc byte[(UIntPtr)769];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.FloatingPoint, digits, 769);
			if (Utf8Parser.TryParseNormalAsFloatingPoint(source, ref numberBuffer, out bytesConsumed, standardFormat))
			{
				value = Number.NumberToDouble(ref numberBuffer);
				return true;
			}
			return Utf8Parser.TryParseAsSpecialFloatingPoint<double>(source, double.PositiveInfinity, double.NegativeInfinity, double.NaN, out value, out bytesConsumed);
		}

		// Token: 0x060024B4 RID: 9396 RVA: 0x0013CF18 File Offset: 0x0013C118
		private static bool TryParseNormalAsFloatingPoint(ReadOnlySpan<byte> source, ref Number.NumberBuffer number, out int bytesConsumed, char standardFormat)
		{
			Utf8Parser.ParseNumberOptions options;
			if (standardFormat != '\0')
			{
				switch (standardFormat)
				{
				case 'E':
				case 'G':
					goto IL_2F;
				case 'F':
					break;
				default:
					switch (standardFormat)
					{
					case 'e':
					case 'g':
						goto IL_2F;
					case 'f':
						break;
					default:
						return ParserHelpers.TryParseThrowFormatException(out bytesConsumed);
					}
					break;
				}
				options = (Utf8Parser.ParseNumberOptions)0;
				goto IL_3E;
			}
			IL_2F:
			options = Utf8Parser.ParseNumberOptions.AllowExponent;
			IL_3E:
			bool flag;
			if (!Utf8Parser.TryParseNumber(source, ref number, out bytesConsumed, options, out flag))
			{
				return false;
			}
			if (!flag && (standardFormat == 'E' || standardFormat == 'e'))
			{
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060024B5 RID: 9397 RVA: 0x0013CF88 File Offset: 0x0013C188
		private unsafe static bool TryParseAsSpecialFloatingPoint<T>(ReadOnlySpan<byte> source, T positiveInfinity, T negativeInfinity, T nan, out T value, out int bytesConsumed) where T : struct
		{
			int num = 0;
			int num2 = source.Length;
			bool flag = false;
			if (num2 >= 4)
			{
				byte b = *source[num];
				if (b != 43)
				{
					if (b != 45)
					{
						goto IL_2E;
					}
					flag = true;
				}
				num++;
				num2--;
			}
			IL_2E:
			if (num2 >= 3)
			{
				if (((int)(*source[num] ^ 110) & -33) == 0 && ((int)(*source[num + 1] ^ 97) & -33) == 0 && ((int)(*source[num + 2] ^ 110) & -33) == 0)
				{
					value = nan;
					bytesConsumed = 3 + num;
					return true;
				}
				if (num2 >= 8)
				{
					int num3 = BinaryPrimitives.ReadInt32LittleEndian(source.Slice(num)) ^ 1768320617;
					if ((num3 & -538976289) == 0)
					{
						num3 = (BinaryPrimitives.ReadInt32LittleEndian(source.Slice(num + 4)) ^ 2037672302);
						if ((num3 & -538976289) == 0)
						{
							value = (flag ? negativeInfinity : positiveInfinity);
							bytesConsumed = 8 + num;
							return true;
						}
					}
				}
			}
			value = default(T);
			bytesConsumed = 0;
			return false;
		}

		// Token: 0x060024B6 RID: 9398 RVA: 0x0013D078 File Offset: 0x0013C278
		public static bool TryParse(ReadOnlySpan<byte> source, out Guid value, out int bytesConsumed, char standardFormat = '\0')
		{
			while (standardFormat != '\0')
			{
				if (standardFormat <= 'D')
				{
					if (standardFormat == 'B')
					{
						return Utf8Parser.TryParseGuidCore(source, out value, out bytesConsumed, 32123);
					}
					if (standardFormat == 'D')
					{
						standardFormat = '\0';
						continue;
					}
				}
				else
				{
					if (standardFormat == 'N')
					{
						return Utf8Parser.TryParseGuidN(source, out value, out bytesConsumed);
					}
					if (standardFormat == 'P')
					{
						return Utf8Parser.TryParseGuidCore(source, out value, out bytesConsumed, 10536);
					}
				}
				return ParserHelpers.TryParseThrowFormatException<Guid>(source, out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseGuidCore(source, out value, out bytesConsumed, 0);
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x0013D0E4 File Offset: 0x0013C2E4
		private static bool TryParseGuidN(ReadOnlySpan<byte> text, out Guid value, out int bytesConsumed)
		{
			if (text.Length < 32)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			uint a;
			int num;
			if (!Utf8Parser.TryParseUInt32X(text.Slice(0, 8), out a, out num) || num != 8)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			ushort num2;
			if (!Utf8Parser.TryParseUInt16X(text.Slice(8, 4), out num2, out num) || num != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			ushort num3;
			if (!Utf8Parser.TryParseUInt16X(text.Slice(12, 4), out num3, out num) || num != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			ushort num4;
			if (!Utf8Parser.TryParseUInt16X(text.Slice(16, 4), out num4, out num) || num != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			ulong num5;
			if (!Utf8Parser.TryParseUInt64X(text.Slice(20), out num5, out num) || num != 12)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			bytesConsumed = 32;
			value = new Guid((int)a, (short)num2, (short)num3, (byte)(num4 >> 8), (byte)num4, (byte)(num5 >> 40), (byte)(num5 >> 32), (byte)(num5 >> 24), (byte)(num5 >> 16), (byte)(num5 >> 8), (byte)num5);
			return true;
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x0013D1FC File Offset: 0x0013C3FC
		private unsafe static bool TryParseGuidCore(ReadOnlySpan<byte> source, out Guid value, out int bytesConsumed, int ends)
		{
			int num = 36 + ((ends != 0) ? 2 : 0);
			if (source.Length < num)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (ends != 0)
			{
				if (*source[0] != (byte)ends)
				{
					value = default(Guid);
					bytesConsumed = 0;
					return false;
				}
				source = source.Slice(1);
				ends >>= 8;
			}
			uint a;
			int num2;
			if (!Utf8Parser.TryParseUInt32X(source, out a, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 8)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num2] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(9);
			ushort num3;
			if (!Utf8Parser.TryParseUInt16X(source, out num3, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num2] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(5);
			ushort num4;
			if (!Utf8Parser.TryParseUInt16X(source, out num4, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num2] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(5);
			ushort num5;
			if (!Utf8Parser.TryParseUInt16X(source, out num5, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 4)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (*source[num2] != 45)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			source = source.Slice(5);
			ulong num6;
			if (!Utf8Parser.TryParseUInt64X(source, out num6, out num2))
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (num2 != 12)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			if (ends != 0 && *source[num2] != (byte)ends)
			{
				value = default(Guid);
				bytesConsumed = 0;
				return false;
			}
			bytesConsumed = num;
			value = new Guid((int)a, (short)num3, (short)num4, (byte)(num5 >> 8), (byte)num5, (byte)(num6 >> 40), (byte)(num6 >> 32), (byte)(num6 >> 24), (byte)(num6 >> 16), (byte)(num6 >> 8), (byte)num6);
			return true;
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x0013D404 File Offset: 0x0013C604
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out sbyte value, out int bytesConsumed, char standardFormat = '\0')
		{
			while (standardFormat != '\0')
			{
				int num = (int)(standardFormat | ' ');
				if (num <= 103)
				{
					if (num == 100 || num == 103)
					{
						standardFormat = '\0';
						continue;
					}
				}
				else
				{
					if (num == 110)
					{
						return Utf8Parser.TryParseSByteN(source, out value, out bytesConsumed);
					}
					if (num == 120)
					{
						Unsafe.SkipInit<sbyte>(out value);
						return Utf8Parser.TryParseByteX(source, Unsafe.As<sbyte, byte>(ref value), out bytesConsumed);
					}
				}
				return ParserHelpers.TryParseThrowFormatException<sbyte>(source, out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseSByteD(source, out value, out bytesConsumed);
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x0013D46C File Offset: 0x0013C66C
		public static bool TryParse(ReadOnlySpan<byte> source, out short value, out int bytesConsumed, char standardFormat = '\0')
		{
			while (standardFormat != '\0')
			{
				int num = (int)(standardFormat | ' ');
				if (num <= 103)
				{
					if (num == 100 || num == 103)
					{
						standardFormat = '\0';
						continue;
					}
				}
				else
				{
					if (num == 110)
					{
						return Utf8Parser.TryParseInt16N(source, out value, out bytesConsumed);
					}
					if (num == 120)
					{
						Unsafe.SkipInit<short>(out value);
						return Utf8Parser.TryParseUInt16X(source, Unsafe.As<short, ushort>(ref value), out bytesConsumed);
					}
				}
				return ParserHelpers.TryParseThrowFormatException<short>(source, out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseInt16D(source, out value, out bytesConsumed);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x0013D4D4 File Offset: 0x0013C6D4
		public static bool TryParse(ReadOnlySpan<byte> source, out int value, out int bytesConsumed, char standardFormat = '\0')
		{
			while (standardFormat != '\0')
			{
				int num = (int)(standardFormat | ' ');
				if (num <= 103)
				{
					if (num == 100 || num == 103)
					{
						standardFormat = '\0';
						continue;
					}
				}
				else
				{
					if (num == 110)
					{
						return Utf8Parser.TryParseInt32N(source, out value, out bytesConsumed);
					}
					if (num == 120)
					{
						Unsafe.SkipInit<int>(out value);
						return Utf8Parser.TryParseUInt32X(source, Unsafe.As<int, uint>(ref value), out bytesConsumed);
					}
				}
				return ParserHelpers.TryParseThrowFormatException<int>(source, out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseInt32D(source, out value, out bytesConsumed);
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x0013D53C File Offset: 0x0013C73C
		public static bool TryParse(ReadOnlySpan<byte> source, out long value, out int bytesConsumed, char standardFormat = '\0')
		{
			while (standardFormat != '\0')
			{
				int num = (int)(standardFormat | ' ');
				if (num <= 103)
				{
					if (num == 100 || num == 103)
					{
						standardFormat = '\0';
						continue;
					}
				}
				else
				{
					if (num == 110)
					{
						return Utf8Parser.TryParseInt64N(source, out value, out bytesConsumed);
					}
					if (num == 120)
					{
						Unsafe.SkipInit<long>(out value);
						return Utf8Parser.TryParseUInt64X(source, Unsafe.As<long, ulong>(ref value), out bytesConsumed);
					}
				}
				return ParserHelpers.TryParseThrowFormatException<long>(source, out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseInt64D(source, out value, out bytesConsumed);
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x0013D5A4 File Offset: 0x0013C7A4
		private unsafe static bool TryParseSByteD(ReadOnlySpan<byte> source, out sbyte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_123;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_123;
					}
					num3 = (int)(*source[num2]);
				}
				int num4 = 0;
				if (ParserHelpers.IsDigit(num3))
				{
					if (num3 == 48)
					{
						do
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_12B;
							}
							num3 = (int)(*source[num2]);
						}
						while (num3 == 48);
						if (!ParserHelpers.IsDigit(num3))
						{
							goto IL_12B;
						}
					}
					num4 = num3 - 48;
					num2++;
					if (num2 < source.Length)
					{
						num3 = (int)(*source[num2]);
						if (ParserHelpers.IsDigit(num3))
						{
							num2++;
							num4 = 10 * num4 + num3 - 48;
							if (num2 < source.Length)
							{
								num3 = (int)(*source[num2]);
								if (ParserHelpers.IsDigit(num3))
								{
									num2++;
									num4 = num4 * 10 + num3 - 48;
									if ((ulong)num4 > (ulong)(127L + (long)((-1 * num + 1) / 2)) || (num2 < source.Length && ParserHelpers.IsDigit((int)(*source[num2]))))
									{
										goto IL_123;
									}
								}
							}
						}
					}
					IL_12B:
					bytesConsumed = num2;
					value = (sbyte)(num4 * num);
					return true;
				}
			}
			IL_123:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x0013D6E8 File Offset: 0x0013C8E8
		private unsafe static bool TryParseInt16D(ReadOnlySpan<byte> source, out short value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_186;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_186;
					}
					num3 = (int)(*source[num2]);
				}
				int num4 = 0;
				if (ParserHelpers.IsDigit(num3))
				{
					if (num3 == 48)
					{
						do
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_18E;
							}
							num3 = (int)(*source[num2]);
						}
						while (num3 == 48);
						if (!ParserHelpers.IsDigit(num3))
						{
							goto IL_18E;
						}
					}
					num4 = num3 - 48;
					num2++;
					if (num2 < source.Length)
					{
						num3 = (int)(*source[num2]);
						if (ParserHelpers.IsDigit(num3))
						{
							num2++;
							num4 = 10 * num4 + num3 - 48;
							if (num2 < source.Length)
							{
								num3 = (int)(*source[num2]);
								if (ParserHelpers.IsDigit(num3))
								{
									num2++;
									num4 = 10 * num4 + num3 - 48;
									if (num2 < source.Length)
									{
										num3 = (int)(*source[num2]);
										if (ParserHelpers.IsDigit(num3))
										{
											num2++;
											num4 = 10 * num4 + num3 - 48;
											if (num2 < source.Length)
											{
												num3 = (int)(*source[num2]);
												if (ParserHelpers.IsDigit(num3))
												{
													num2++;
													num4 = num4 * 10 + num3 - 48;
													if ((ulong)num4 > (ulong)(32767L + (long)((-1 * num + 1) / 2)) || (num2 < source.Length && ParserHelpers.IsDigit((int)(*source[num2]))))
													{
														goto IL_186;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_18E:
					bytesConsumed = num2;
					value = (short)(num4 * num);
					return true;
				}
			}
			IL_186:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x0013D890 File Offset: 0x0013CA90
		private unsafe static bool TryParseInt32D(ReadOnlySpan<byte> source, out int value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_281;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_281;
					}
					num3 = (int)(*source[num2]);
				}
				int num4 = 0;
				if (ParserHelpers.IsDigit(num3))
				{
					if (num3 == 48)
					{
						do
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_289;
							}
							num3 = (int)(*source[num2]);
						}
						while (num3 == 48);
						if (!ParserHelpers.IsDigit(num3))
						{
							goto IL_289;
						}
					}
					num4 = num3 - 48;
					num2++;
					if (num2 < source.Length)
					{
						num3 = (int)(*source[num2]);
						if (ParserHelpers.IsDigit(num3))
						{
							num2++;
							num4 = 10 * num4 + num3 - 48;
							if (num2 < source.Length)
							{
								num3 = (int)(*source[num2]);
								if (ParserHelpers.IsDigit(num3))
								{
									num2++;
									num4 = 10 * num4 + num3 - 48;
									if (num2 < source.Length)
									{
										num3 = (int)(*source[num2]);
										if (ParserHelpers.IsDigit(num3))
										{
											num2++;
											num4 = 10 * num4 + num3 - 48;
											if (num2 < source.Length)
											{
												num3 = (int)(*source[num2]);
												if (ParserHelpers.IsDigit(num3))
												{
													num2++;
													num4 = 10 * num4 + num3 - 48;
													if (num2 < source.Length)
													{
														num3 = (int)(*source[num2]);
														if (ParserHelpers.IsDigit(num3))
														{
															num2++;
															num4 = 10 * num4 + num3 - 48;
															if (num2 < source.Length)
															{
																num3 = (int)(*source[num2]);
																if (ParserHelpers.IsDigit(num3))
																{
																	num2++;
																	num4 = 10 * num4 + num3 - 48;
																	if (num2 < source.Length)
																	{
																		num3 = (int)(*source[num2]);
																		if (ParserHelpers.IsDigit(num3))
																		{
																			num2++;
																			num4 = 10 * num4 + num3 - 48;
																			if (num2 < source.Length)
																			{
																				num3 = (int)(*source[num2]);
																				if (ParserHelpers.IsDigit(num3))
																				{
																					num2++;
																					num4 = 10 * num4 + num3 - 48;
																					if (num2 < source.Length)
																					{
																						num3 = (int)(*source[num2]);
																						if (ParserHelpers.IsDigit(num3))
																						{
																							num2++;
																							if (num4 > 214748364)
																							{
																								goto IL_281;
																							}
																							num4 = num4 * 10 + num3 - 48;
																							if ((ulong)num4 > (ulong)(2147483647L + (long)((-1 * num + 1) / 2)) || (num2 < source.Length && ParserHelpers.IsDigit((int)(*source[num2]))))
																							{
																								goto IL_281;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_289:
					bytesConsumed = num2;
					value = num4 * num;
					return true;
				}
			}
			IL_281:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x0013DB30 File Offset: 0x0013CD30
		private unsafe static bool TryParseInt64D(ReadOnlySpan<byte> source, out long value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0L;
				return false;
			}
			int num = 0;
			int num2 = 1;
			if (*source[0] == 45)
			{
				num = 1;
				num2 = -1;
				if (source.Length <= num)
				{
					bytesConsumed = 0;
					value = 0L;
					return false;
				}
			}
			else if (*source[0] == 43)
			{
				num = 1;
				if (source.Length <= num)
				{
					bytesConsumed = 0;
					value = 0L;
					return false;
				}
			}
			int num3 = 19 + num;
			long num4 = (long)(*source[num] - 48);
			if (num4 < 0L || num4 > 9L)
			{
				bytesConsumed = 0;
				value = 0L;
				return false;
			}
			ulong num5 = (ulong)num4;
			if (source.Length < num3)
			{
				for (int i = num + 1; i < source.Length; i++)
				{
					long num6 = (long)(*source[i] - 48);
					if (num6 < 0L || num6 > 9L)
					{
						bytesConsumed = i;
						value = (long)(num5 * (ulong)((long)num2));
						return true;
					}
					num5 = num5 * 10UL + (ulong)num6;
				}
			}
			else
			{
				for (int j = num + 1; j < num3 - 1; j++)
				{
					long num7 = (long)(*source[j] - 48);
					if (num7 < 0L || num7 > 9L)
					{
						bytesConsumed = j;
						value = (long)(num5 * (ulong)((long)num2));
						return true;
					}
					num5 = num5 * 10UL + (ulong)num7;
				}
				for (int k = num3 - 1; k < source.Length; k++)
				{
					long num8 = (long)(*source[k] - 48);
					if (num8 < 0L || num8 > 9L)
					{
						bytesConsumed = k;
						value = (long)(num5 * (ulong)((long)num2));
						return true;
					}
					bool flag = num2 > 0;
					bool flag2 = num8 > 8L || (flag && num8 > 7L);
					if (num5 > 922337203685477580UL || (num5 == 922337203685477580UL && flag2))
					{
						bytesConsumed = 0;
						value = 0L;
						return false;
					}
					num5 = num5 * 10UL + (ulong)num8;
				}
			}
			bytesConsumed = source.Length;
			value = (long)(num5 * (ulong)((long)num2));
			return true;
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x0013DD14 File Offset: 0x0013CF14
		private unsafe static bool TryParseSByteN(ReadOnlySpan<byte> source, out sbyte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_F9;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_F9;
					}
					num3 = (int)(*source[num2]);
				}
				int num4;
				if (num3 != 46)
				{
					if (ParserHelpers.IsDigit(num3))
					{
						num4 = num3 - 48;
						for (;;)
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_101;
							}
							num3 = (int)(*source[num2]);
							if (num3 != 44)
							{
								if (num3 == 46)
								{
									goto IL_D4;
								}
								if (!ParserHelpers.IsDigit(num3))
								{
									goto IL_101;
								}
								num4 = num4 * 10 + num3 - 48;
								if (num4 > 127 + (-1 * num + 1) / 2)
								{
									break;
								}
							}
						}
						goto IL_F9;
					}
					goto IL_F9;
				}
				else
				{
					num4 = 0;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_F9;
					}
				}
				do
				{
					IL_D4:
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_101;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_F9;
				}
				IL_101:
				bytesConsumed = num2;
				value = (sbyte)(num4 * num);
				return true;
			}
			IL_F9:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x0013DE2C File Offset: 0x0013D02C
		private unsafe static bool TryParseInt16N(ReadOnlySpan<byte> source, out short value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_FF;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_FF;
					}
					num3 = (int)(*source[num2]);
				}
				int num4;
				if (num3 != 46)
				{
					if (ParserHelpers.IsDigit(num3))
					{
						num4 = num3 - 48;
						for (;;)
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_107;
							}
							num3 = (int)(*source[num2]);
							if (num3 != 44)
							{
								if (num3 == 46)
								{
									goto IL_DA;
								}
								if (!ParserHelpers.IsDigit(num3))
								{
									goto IL_107;
								}
								num4 = num4 * 10 + num3 - 48;
								if (num4 > 32767 + (-1 * num + 1) / 2)
								{
									break;
								}
							}
						}
						goto IL_FF;
					}
					goto IL_FF;
				}
				else
				{
					num4 = 0;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_FF;
					}
				}
				do
				{
					IL_DA:
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_107;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_FF;
				}
				IL_107:
				bytesConsumed = num2;
				value = (short)(num4 * num);
				return true;
			}
			IL_FF:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x0013DF4C File Offset: 0x0013D14C
		private unsafe static bool TryParseInt32N(ReadOnlySpan<byte> source, out int value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_10A;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_10A;
					}
					num3 = (int)(*source[num2]);
				}
				int num4;
				if (num3 != 46)
				{
					if (ParserHelpers.IsDigit(num3))
					{
						num4 = num3 - 48;
						for (;;)
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_112;
							}
							num3 = (int)(*source[num2]);
							if (num3 != 44)
							{
								if (num3 == 46)
								{
									goto IL_E5;
								}
								if (!ParserHelpers.IsDigit(num3))
								{
									goto IL_112;
								}
								if (num4 > 214748364)
								{
									break;
								}
								num4 = num4 * 10 + num3 - 48;
								if ((ulong)num4 > (ulong)(2147483647L + (long)((-1 * num + 1) / 2)))
								{
									break;
								}
							}
						}
						goto IL_10A;
					}
					goto IL_10A;
				}
				else
				{
					num4 = 0;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_10A;
					}
				}
				do
				{
					IL_E5:
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_112;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_10A;
				}
				IL_112:
				bytesConsumed = num2;
				value = num4 * num;
				return true;
			}
			IL_10A:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x0013E074 File Offset: 0x0013D274
		private unsafe static bool TryParseInt64N(ReadOnlySpan<byte> source, out long value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 1;
				int num2 = 0;
				int num3 = (int)(*source[num2]);
				if (num3 == 45)
				{
					num = -1;
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_115;
					}
					num3 = (int)(*source[num2]);
				}
				else if (num3 == 43)
				{
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_115;
					}
					num3 = (int)(*source[num2]);
				}
				long num4;
				if (num3 != 46)
				{
					if (ParserHelpers.IsDigit(num3))
					{
						num4 = (long)(num3 - 48);
						for (;;)
						{
							num2++;
							if (num2 >= source.Length)
							{
								goto IL_11E;
							}
							num3 = (int)(*source[num2]);
							if (num3 != 44)
							{
								if (num3 == 46)
								{
									goto IL_F0;
								}
								if (!ParserHelpers.IsDigit(num3))
								{
									goto IL_11E;
								}
								if (num4 > 922337203685477580L)
								{
									break;
								}
								num4 = num4 * 10L + (long)num3 - 48L;
								if (num4 > 9223372036854775807L + (long)((-1 * num + 1) / 2))
								{
									break;
								}
							}
						}
						goto IL_115;
					}
					goto IL_115;
				}
				else
				{
					num4 = 0L;
					num2++;
					if (num2 >= source.Length || *source[num2] != 48)
					{
						goto IL_115;
					}
				}
				do
				{
					IL_F0:
					num2++;
					if (num2 >= source.Length)
					{
						goto IL_11E;
					}
					num3 = (int)(*source[num2]);
				}
				while (num3 == 48);
				if (ParserHelpers.IsDigit(num3))
				{
					goto IL_115;
				}
				IL_11E:
				bytesConsumed = num2;
				value = num4 * (long)num;
				return true;
			}
			IL_115:
			bytesConsumed = 0;
			value = 0L;
			return false;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x0013E1AC File Offset: 0x0013D3AC
		public static bool TryParse(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed, char standardFormat = '\0')
		{
			while (standardFormat != '\0')
			{
				int num = (int)(standardFormat | ' ');
				if (num <= 103)
				{
					if (num == 100 || num == 103)
					{
						standardFormat = '\0';
						continue;
					}
				}
				else
				{
					if (num == 110)
					{
						return Utf8Parser.TryParseByteN(source, out value, out bytesConsumed);
					}
					if (num == 120)
					{
						return Utf8Parser.TryParseByteX(source, out value, out bytesConsumed);
					}
				}
				return ParserHelpers.TryParseThrowFormatException<byte>(source, out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseByteD(source, out value, out bytesConsumed);
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x0013E208 File Offset: 0x0013D408
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed, char standardFormat = '\0')
		{
			while (standardFormat != '\0')
			{
				int num = (int)(standardFormat | ' ');
				if (num <= 103)
				{
					if (num == 100 || num == 103)
					{
						standardFormat = '\0';
						continue;
					}
				}
				else
				{
					if (num == 110)
					{
						return Utf8Parser.TryParseUInt16N(source, out value, out bytesConsumed);
					}
					if (num == 120)
					{
						return Utf8Parser.TryParseUInt16X(source, out value, out bytesConsumed);
					}
				}
				return ParserHelpers.TryParseThrowFormatException<ushort>(source, out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseUInt16D(source, out value, out bytesConsumed);
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x0013E264 File Offset: 0x0013D464
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed, char standardFormat = '\0')
		{
			while (standardFormat != '\0')
			{
				int num = (int)(standardFormat | ' ');
				if (num <= 103)
				{
					if (num == 100 || num == 103)
					{
						standardFormat = '\0';
						continue;
					}
				}
				else
				{
					if (num == 110)
					{
						return Utf8Parser.TryParseUInt32N(source, out value, out bytesConsumed);
					}
					if (num == 120)
					{
						return Utf8Parser.TryParseUInt32X(source, out value, out bytesConsumed);
					}
				}
				return ParserHelpers.TryParseThrowFormatException<uint>(source, out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseUInt32D(source, out value, out bytesConsumed);
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x0013E2C0 File Offset: 0x0013D4C0
		[CLSCompliant(false)]
		public static bool TryParse(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed, char standardFormat = '\0')
		{
			while (standardFormat != '\0')
			{
				int num = (int)(standardFormat | ' ');
				if (num <= 103)
				{
					if (num == 100 || num == 103)
					{
						standardFormat = '\0';
						continue;
					}
				}
				else
				{
					if (num == 110)
					{
						return Utf8Parser.TryParseUInt64N(source, out value, out bytesConsumed);
					}
					if (num == 120)
					{
						return Utf8Parser.TryParseUInt64X(source, out value, out bytesConsumed);
					}
				}
				return ParserHelpers.TryParseThrowFormatException<ulong>(source, out value, out bytesConsumed);
			}
			return Utf8Parser.TryParseUInt64D(source, out value, out bytesConsumed);
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x0013E31C File Offset: 0x0013D51C
		private unsafe static bool TryParseByteD(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				int num3 = 0;
				if (ParserHelpers.IsDigit(num2))
				{
					if (num2 == 48)
					{
						do
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_DD;
							}
							num2 = (int)(*source[num]);
						}
						while (num2 == 48);
						if (!ParserHelpers.IsDigit(num2))
						{
							goto IL_DD;
						}
					}
					num3 = num2 - 48;
					num++;
					if (num < source.Length)
					{
						num2 = (int)(*source[num]);
						if (ParserHelpers.IsDigit(num2))
						{
							num++;
							num3 = 10 * num3 + num2 - 48;
							if (num < source.Length)
							{
								num2 = (int)(*source[num]);
								if (ParserHelpers.IsDigit(num2))
								{
									num++;
									num3 = num3 * 10 + num2 - 48;
									if (num3 > 255 || (num < source.Length && ParserHelpers.IsDigit((int)(*source[num]))))
									{
										goto IL_D5;
									}
								}
							}
						}
					}
					IL_DD:
					bytesConsumed = num;
					value = (byte)num3;
					return true;
				}
			}
			IL_D5:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x0013E410 File Offset: 0x0013D610
		private unsafe static bool TryParseUInt16D(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				int num3 = 0;
				if (ParserHelpers.IsDigit(num2))
				{
					if (num2 == 48)
					{
						do
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_13D;
							}
							num2 = (int)(*source[num]);
						}
						while (num2 == 48);
						if (!ParserHelpers.IsDigit(num2))
						{
							goto IL_13D;
						}
					}
					num3 = num2 - 48;
					num++;
					if (num < source.Length)
					{
						num2 = (int)(*source[num]);
						if (ParserHelpers.IsDigit(num2))
						{
							num++;
							num3 = 10 * num3 + num2 - 48;
							if (num < source.Length)
							{
								num2 = (int)(*source[num]);
								if (ParserHelpers.IsDigit(num2))
								{
									num++;
									num3 = 10 * num3 + num2 - 48;
									if (num < source.Length)
									{
										num2 = (int)(*source[num]);
										if (ParserHelpers.IsDigit(num2))
										{
											num++;
											num3 = 10 * num3 + num2 - 48;
											if (num < source.Length)
											{
												num2 = (int)(*source[num]);
												if (ParserHelpers.IsDigit(num2))
												{
													num++;
													num3 = num3 * 10 + num2 - 48;
													if (num3 > 65535 || (num < source.Length && ParserHelpers.IsDigit((int)(*source[num]))))
													{
														goto IL_135;
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_13D:
					bytesConsumed = num;
					value = (ushort)num3;
					return true;
				}
			}
			IL_135:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x0013E564 File Offset: 0x0013D764
		private unsafe static bool TryParseUInt32D(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				int num3 = 0;
				if (ParserHelpers.IsDigit(num2))
				{
					if (num2 == 48)
					{
						do
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_23D;
							}
							num2 = (int)(*source[num]);
						}
						while (num2 == 48);
						if (!ParserHelpers.IsDigit(num2))
						{
							goto IL_23D;
						}
					}
					num3 = num2 - 48;
					num++;
					if (num < source.Length)
					{
						num2 = (int)(*source[num]);
						if (ParserHelpers.IsDigit(num2))
						{
							num++;
							num3 = 10 * num3 + num2 - 48;
							if (num < source.Length)
							{
								num2 = (int)(*source[num]);
								if (ParserHelpers.IsDigit(num2))
								{
									num++;
									num3 = 10 * num3 + num2 - 48;
									if (num < source.Length)
									{
										num2 = (int)(*source[num]);
										if (ParserHelpers.IsDigit(num2))
										{
											num++;
											num3 = 10 * num3 + num2 - 48;
											if (num < source.Length)
											{
												num2 = (int)(*source[num]);
												if (ParserHelpers.IsDigit(num2))
												{
													num++;
													num3 = 10 * num3 + num2 - 48;
													if (num < source.Length)
													{
														num2 = (int)(*source[num]);
														if (ParserHelpers.IsDigit(num2))
														{
															num++;
															num3 = 10 * num3 + num2 - 48;
															if (num < source.Length)
															{
																num2 = (int)(*source[num]);
																if (ParserHelpers.IsDigit(num2))
																{
																	num++;
																	num3 = 10 * num3 + num2 - 48;
																	if (num < source.Length)
																	{
																		num2 = (int)(*source[num]);
																		if (ParserHelpers.IsDigit(num2))
																		{
																			num++;
																			num3 = 10 * num3 + num2 - 48;
																			if (num < source.Length)
																			{
																				num2 = (int)(*source[num]);
																				if (ParserHelpers.IsDigit(num2))
																				{
																					num++;
																					num3 = 10 * num3 + num2 - 48;
																					if (num < source.Length)
																					{
																						num2 = (int)(*source[num]);
																						if (ParserHelpers.IsDigit(num2))
																						{
																							num++;
																							if (num3 > 429496729 || (num3 == 429496729 && num2 > 53))
																							{
																								goto IL_235;
																							}
																							num3 = num3 * 10 + num2 - 48;
																							if (num < source.Length && ParserHelpers.IsDigit((int)(*source[num])))
																							{
																								goto IL_235;
																							}
																						}
																					}
																				}
																			}
																		}
																	}
																}
															}
														}
													}
												}
											}
										}
									}
								}
							}
						}
					}
					IL_23D:
					bytesConsumed = num;
					value = (uint)num3;
					return true;
				}
			}
			IL_235:
			bytesConsumed = 0;
			value = 0U;
			return false;
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x0013E7B8 File Offset: 0x0013D9B8
		private unsafe static bool TryParseUInt64D(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num = (ulong)(*source[0] - 48);
			if (num > 9UL)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num2 = num;
			if (source.Length < 19)
			{
				for (int i = 1; i < source.Length; i++)
				{
					ulong num3 = (ulong)(*source[i] - 48);
					if (num3 > 9UL)
					{
						bytesConsumed = i;
						value = num2;
						return true;
					}
					num2 = num2 * 10UL + num3;
				}
			}
			else
			{
				for (int j = 1; j < 18; j++)
				{
					ulong num4 = (ulong)(*source[j] - 48);
					if (num4 > 9UL)
					{
						bytesConsumed = j;
						value = num2;
						return true;
					}
					num2 = num2 * 10UL + num4;
				}
				for (int k = 18; k < source.Length; k++)
				{
					ulong num5 = (ulong)(*source[k] - 48);
					if (num5 > 9UL)
					{
						bytesConsumed = k;
						value = num2;
						return true;
					}
					if (num2 > 1844674407370955161UL || (num2 == 1844674407370955161UL && num5 > 5UL))
					{
						bytesConsumed = 0;
						value = 0UL;
						return false;
					}
					num2 = num2 * 10UL + num5;
				}
			}
			bytesConsumed = source.Length;
			value = num2;
			return true;
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x0013E8EC File Offset: 0x0013DAEC
		private unsafe static bool TryParseByteN(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_CE;
					}
					num2 = (int)(*source[num]);
				}
				int num3;
				if (num2 != 46)
				{
					if (ParserHelpers.IsDigit(num2))
					{
						num3 = num2 - 48;
						for (;;)
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_D6;
							}
							num2 = (int)(*source[num]);
							if (num2 != 44)
							{
								if (num2 == 46)
								{
									goto IL_A9;
								}
								if (!ParserHelpers.IsDigit(num2))
								{
									goto IL_D6;
								}
								num3 = num3 * 10 + num2 - 48;
								if (num3 > 255)
								{
									break;
								}
							}
						}
						goto IL_CE;
					}
					goto IL_CE;
				}
				else
				{
					num3 = 0;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_CE;
					}
				}
				do
				{
					IL_A9:
					num++;
					if (num >= source.Length)
					{
						goto IL_D6;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_CE;
				}
				IL_D6:
				bytesConsumed = num;
				value = (byte)num3;
				return true;
			}
			IL_CE:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x0013E9D8 File Offset: 0x0013DBD8
		private unsafe static bool TryParseUInt16N(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_CE;
					}
					num2 = (int)(*source[num]);
				}
				int num3;
				if (num2 != 46)
				{
					if (ParserHelpers.IsDigit(num2))
					{
						num3 = num2 - 48;
						for (;;)
						{
							num++;
							if (num >= source.Length)
							{
								goto IL_D6;
							}
							num2 = (int)(*source[num]);
							if (num2 != 44)
							{
								if (num2 == 46)
								{
									goto IL_A9;
								}
								if (!ParserHelpers.IsDigit(num2))
								{
									goto IL_D6;
								}
								num3 = num3 * 10 + num2 - 48;
								if (num3 > 65535)
								{
									break;
								}
							}
						}
						goto IL_CE;
					}
					goto IL_CE;
				}
				else
				{
					num3 = 0;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_CE;
					}
				}
				do
				{
					IL_A9:
					num++;
					if (num >= source.Length)
					{
						goto IL_D6;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_CE;
				}
				IL_D6:
				bytesConsumed = num;
				value = (ushort)num3;
				return true;
			}
			IL_CE:
			bytesConsumed = 0;
			value = 0;
			return false;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x0013EAC4 File Offset: 0x0013DCC4
		private unsafe static bool TryParseUInt32N(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_DE;
					}
					num2 = (int)(*source[num]);
				}
				int num3;
				if (num2 != 46)
				{
					if (!ParserHelpers.IsDigit(num2))
					{
						goto IL_DE;
					}
					num3 = num2 - 48;
					for (;;)
					{
						num++;
						if (num >= source.Length)
						{
							goto IL_E6;
						}
						num2 = (int)(*source[num]);
						if (num2 != 44)
						{
							if (num2 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num2))
							{
								goto IL_E6;
							}
							if (num3 > 429496729 || (num3 == 429496729 && num2 > 53))
							{
								goto IL_DE;
							}
							num3 = num3 * 10 + num2 - 48;
						}
					}
				}
				else
				{
					num3 = 0;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_DE;
					}
				}
				do
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_E6;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_DE;
				}
				IL_E6:
				bytesConsumed = num;
				value = (uint)num3;
				return true;
			}
			IL_DE:
			bytesConsumed = 0;
			value = 0U;
			return false;
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x0013EBC0 File Offset: 0x0013DDC0
		private unsafe static bool TryParseUInt64N(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed)
		{
			if (source.Length >= 1)
			{
				int num = 0;
				int num2 = (int)(*source[num]);
				if (num2 == 43)
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_EB;
					}
					num2 = (int)(*source[num]);
				}
				long num3;
				if (num2 != 46)
				{
					if (!ParserHelpers.IsDigit(num2))
					{
						goto IL_EB;
					}
					num3 = (long)(num2 - 48);
					for (;;)
					{
						num++;
						if (num >= source.Length)
						{
							goto IL_F4;
						}
						num2 = (int)(*source[num]);
						if (num2 != 44)
						{
							if (num2 == 46)
							{
								break;
							}
							if (!ParserHelpers.IsDigit(num2))
							{
								goto IL_F4;
							}
							if (num3 > 1844674407370955161L || (num3 == 1844674407370955161L && num2 > 53))
							{
								goto IL_EB;
							}
							num3 = num3 * 10L + (long)num2 - 48L;
						}
					}
				}
				else
				{
					num3 = 0L;
					num++;
					if (num >= source.Length || *source[num] != 48)
					{
						goto IL_EB;
					}
				}
				do
				{
					num++;
					if (num >= source.Length)
					{
						goto IL_F4;
					}
					num2 = (int)(*source[num]);
				}
				while (num2 == 48);
				if (ParserHelpers.IsDigit(num2))
				{
					goto IL_EB;
				}
				IL_F4:
				bytesConsumed = num;
				value = (ulong)num3;
				return true;
			}
			IL_EB:
			bytesConsumed = 0;
			value = 0UL;
			return false;
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x0013ECC8 File Offset: 0x0013DEC8
		private unsafe static bool TryParseByteX(ReadOnlySpan<byte> source, out byte value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			ReadOnlySpan<byte> charToHexLookup = HexConverter.CharToHexLookup;
			byte index = *source[0];
			byte b = *charToHexLookup[(int)index];
			if (b == 255)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			uint num = (uint)b;
			if (source.Length <= 2)
			{
				for (int i = 1; i < source.Length; i++)
				{
					index = *source[i];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = i;
						value = (byte)num;
						return true;
					}
					num = (num << 4) + (uint)b;
				}
			}
			else
			{
				for (int j = 1; j < 2; j++)
				{
					index = *source[j];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = j;
						value = (byte)num;
						return true;
					}
					num = (num << 4) + (uint)b;
				}
				for (int k = 2; k < source.Length; k++)
				{
					index = *source[k];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = k;
						value = (byte)num;
						return true;
					}
					if (num > 15U)
					{
						bytesConsumed = 0;
						value = 0;
						return false;
					}
					num = (num << 4) + (uint)b;
				}
			}
			bytesConsumed = source.Length;
			value = (byte)num;
			return true;
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x0013EE00 File Offset: 0x0013E000
		private unsafe static bool TryParseUInt16X(ReadOnlySpan<byte> source, out ushort value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			ReadOnlySpan<byte> charToHexLookup = HexConverter.CharToHexLookup;
			byte index = *source[0];
			byte b = *charToHexLookup[(int)index];
			if (b == 255)
			{
				bytesConsumed = 0;
				value = 0;
				return false;
			}
			uint num = (uint)b;
			if (source.Length <= 4)
			{
				for (int i = 1; i < source.Length; i++)
				{
					index = *source[i];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = i;
						value = (ushort)num;
						return true;
					}
					num = (num << 4) + (uint)b;
				}
			}
			else
			{
				for (int j = 1; j < 4; j++)
				{
					index = *source[j];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = j;
						value = (ushort)num;
						return true;
					}
					num = (num << 4) + (uint)b;
				}
				for (int k = 4; k < source.Length; k++)
				{
					index = *source[k];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = k;
						value = (ushort)num;
						return true;
					}
					if (num > 4095U)
					{
						bytesConsumed = 0;
						value = 0;
						return false;
					}
					num = (num << 4) + (uint)b;
				}
			}
			bytesConsumed = source.Length;
			value = (ushort)num;
			return true;
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x0013EF3C File Offset: 0x0013E13C
		private unsafe static bool TryParseUInt32X(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0U;
				return false;
			}
			ReadOnlySpan<byte> charToHexLookup = HexConverter.CharToHexLookup;
			byte index = *source[0];
			byte b = *charToHexLookup[(int)index];
			if (b == 255)
			{
				bytesConsumed = 0;
				value = 0U;
				return false;
			}
			uint num = (uint)b;
			if (source.Length <= 8)
			{
				for (int i = 1; i < source.Length; i++)
				{
					index = *source[i];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = i;
						value = num;
						return true;
					}
					num = (num << 4) + (uint)b;
				}
			}
			else
			{
				for (int j = 1; j < 8; j++)
				{
					index = *source[j];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = j;
						value = num;
						return true;
					}
					num = (num << 4) + (uint)b;
				}
				for (int k = 8; k < source.Length; k++)
				{
					index = *source[k];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = k;
						value = num;
						return true;
					}
					if (num > 268435455U)
					{
						bytesConsumed = 0;
						value = 0U;
						return false;
					}
					num = (num << 4) + (uint)b;
				}
			}
			bytesConsumed = source.Length;
			value = num;
			return true;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x0013F074 File Offset: 0x0013E274
		private unsafe static bool TryParseUInt64X(ReadOnlySpan<byte> source, out ulong value, out int bytesConsumed)
		{
			if (source.Length < 1)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ReadOnlySpan<byte> charToHexLookup = HexConverter.CharToHexLookup;
			byte index = *source[0];
			byte b = *charToHexLookup[(int)index];
			if (b == 255)
			{
				bytesConsumed = 0;
				value = 0UL;
				return false;
			}
			ulong num = (ulong)b;
			if (source.Length <= 16)
			{
				for (int i = 1; i < source.Length; i++)
				{
					index = *source[i];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = i;
						value = num;
						return true;
					}
					num = (num << 4) + (ulong)b;
				}
			}
			else
			{
				for (int j = 1; j < 16; j++)
				{
					index = *source[j];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = j;
						value = num;
						return true;
					}
					num = (num << 4) + (ulong)b;
				}
				for (int k = 16; k < source.Length; k++)
				{
					index = *source[k];
					b = *charToHexLookup[(int)index];
					if (b == 255)
					{
						bytesConsumed = k;
						value = num;
						return true;
					}
					if (num > 1152921504606846975UL)
					{
						bytesConsumed = 0;
						value = 0UL;
						return false;
					}
					num = (num << 4) + (ulong)b;
				}
			}
			bytesConsumed = source.Length;
			value = num;
			return true;
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x0013F1BC File Offset: 0x0013E3BC
		private unsafe static bool TryParseNumber(ReadOnlySpan<byte> source, ref Number.NumberBuffer number, out int bytesConsumed, Utf8Parser.ParseNumberOptions options, out bool textUsedExponentNotation)
		{
			textUsedExponentNotation = false;
			if (source.Length == 0)
			{
				bytesConsumed = 0;
				return false;
			}
			Span<byte> digits = number.Digits;
			int num = 0;
			byte b = *source[num];
			if (b != 43)
			{
				if (b != 45)
				{
					goto IL_55;
				}
				number.IsNegative = true;
			}
			num++;
			if (num == source.Length)
			{
				bytesConsumed = 0;
				return false;
			}
			b = *source[num];
			IL_55:
			int num2 = num;
			int num3 = 0;
			int num4 = digits.Length - 1;
			while (num != source.Length)
			{
				b = *source[num];
				if (b != 48)
				{
					break;
				}
				num++;
			}
			if (num == source.Length)
			{
				bytesConsumed = num;
				return true;
			}
			int num5 = num;
			int num6 = 0;
			while (num != source.Length)
			{
				b = *source[num];
				int num7 = (int)(b - 48);
				if (num7 > 9)
				{
					break;
				}
				num++;
				num3++;
				if (num3 >= num4)
				{
					num6 |= num7;
				}
			}
			number.HasNonZeroTail = (num6 != 0);
			int num8 = num - num2;
			int num9 = num - num5;
			int num10 = Math.Min(num9, num4);
			source.Slice(num5, num10).CopyTo(digits);
			int num11 = num10;
			number.Scale = num9;
			if (num == source.Length)
			{
				*digits[num11] = 0;
				number.DigitsCount = num11;
				bytesConsumed = num;
				return true;
			}
			int num12 = 0;
			if (b == 46)
			{
				num++;
				int num13 = num;
				while (num != source.Length)
				{
					b = *source[num];
					int num14 = (int)(b - 48);
					if (num14 > 9)
					{
						break;
					}
					num++;
					num3++;
					if (num3 >= num4)
					{
						num6 |= num14;
					}
				}
				number.HasNonZeroTail = (num6 != 0);
				num12 = num - num13;
				int num15 = num13;
				if (num11 == 0)
				{
					while (num15 < num && *source[num15] == 48)
					{
						number.Scale--;
						num15++;
					}
				}
				int num16 = Math.Min(num - num15, num4 - num11);
				source.Slice(num15, num16).CopyTo(digits.Slice(num11));
				num11 += num16;
				if (num == source.Length)
				{
					if (num8 == 0 && num12 == 0)
					{
						bytesConsumed = 0;
						return false;
					}
					*digits[num11] = 0;
					number.DigitsCount = num11;
					bytesConsumed = num;
					return true;
				}
			}
			if (num8 == 0 && num12 == 0)
			{
				bytesConsumed = 0;
				return false;
			}
			if (((int)b & -33) != 69)
			{
				*digits[num11] = 0;
				number.DigitsCount = num11;
				bytesConsumed = num;
				return true;
			}
			textUsedExponentNotation = true;
			num++;
			if ((options & Utf8Parser.ParseNumberOptions.AllowExponent) == (Utf8Parser.ParseNumberOptions)0)
			{
				bytesConsumed = 0;
				return false;
			}
			if (num == source.Length)
			{
				bytesConsumed = 0;
				return false;
			}
			bool flag = false;
			b = *source[num];
			if (b != 43)
			{
				if (b != 45)
				{
					goto IL_2A0;
				}
				flag = true;
			}
			num++;
			if (num == source.Length)
			{
				bytesConsumed = 0;
				return false;
			}
			b = *source[num];
			IL_2A0:
			if (b - 48 > 9)
			{
				bytesConsumed = 0;
				return false;
			}
			uint maxValue;
			int num17;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out maxValue, out num17))
			{
				maxValue = uint.MaxValue;
				for (num += 10; num != source.Length; num++)
				{
					b = *source[num];
					int num18 = (int)(b - 48);
					if (num18 > 9)
					{
						break;
					}
				}
			}
			num += num17;
			if (flag)
			{
				if ((long)number.Scale < (long)(18446744071562067968UL + (ulong)maxValue))
				{
					number.Scale = int.MinValue;
				}
				else
				{
					number.Scale -= (int)maxValue;
				}
			}
			else if ((long)number.Scale > (long)(2147483647UL - (ulong)maxValue))
			{
				number.Scale = int.MaxValue;
			}
			else
			{
				number.Scale += (int)maxValue;
			}
			*digits[num11] = 0;
			number.DigitsCount = num11;
			bytesConsumed = num;
			return true;
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x0013F534 File Offset: 0x0013E734
		private unsafe static bool TryParseTimeSpanBigG(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed)
		{
			int num = 0;
			byte b = 0;
			while (num != source.Length)
			{
				b = *source[num];
				if (b != 32 && b != 9)
				{
					break;
				}
				num++;
			}
			if (num == source.Length)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			bool isNegative = false;
			if (b == 45)
			{
				isNegative = true;
				num++;
				if (num == source.Length)
				{
					value = default(TimeSpan);
					bytesConsumed = 0;
					return false;
				}
			}
			uint days;
			int num2;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out days, out num2))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num2;
			if (num == source.Length || *source[num++] != 58)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			uint hours;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out hours, out num2))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num2;
			if (num == source.Length || *source[num++] != 58)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			uint minutes;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out minutes, out num2))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num2;
			if (num == source.Length || *source[num++] != 58)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			uint seconds;
			if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out seconds, out num2))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num2;
			if (num == source.Length || *source[num++] != 46)
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			uint fraction;
			if (!Utf8Parser.TryParseTimeSpanFraction(source.Slice(num), out fraction, out num2))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			num += num2;
			if (!Utf8Parser.TryCreateTimeSpan(isNegative, days, hours, minutes, seconds, fraction, out value))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			if (num != source.Length && (*source[num] == 46 || *source[num] == 58))
			{
				value = default(TimeSpan);
				bytesConsumed = 0;
				return false;
			}
			bytesConsumed = num;
			return true;
		}

		// Token: 0x060024D7 RID: 9431 RVA: 0x0013F744 File Offset: 0x0013E944
		private static bool TryParseTimeSpanC(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed)
		{
			Utf8Parser.TimeSpanSplitter timeSpanSplitter = default(Utf8Parser.TimeSpanSplitter);
			if (!timeSpanSplitter.TrySplitTimeSpan(source, true, out bytesConsumed))
			{
				value = default(TimeSpan);
				return false;
			}
			bool isNegative = timeSpanSplitter.IsNegative;
			uint separators = timeSpanSplitter.Separators;
			bool flag;
			if (separators <= 16842752U)
			{
				if (separators == 0U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, 0U, 0U, 0U, 0U, out value);
					goto IL_172;
				}
				if (separators == 16777216U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, 0U, 0U, out value);
					goto IL_172;
				}
				if (separators == 16842752U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, 0U, out value);
					goto IL_172;
				}
			}
			else if (separators <= 33619968U)
			{
				if (separators == 16843264U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, out value);
					goto IL_172;
				}
				if (separators == 33619968U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, 0U, 0U, out value);
					goto IL_172;
				}
			}
			else
			{
				if (separators == 33620224U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, 0U, out value);
					goto IL_172;
				}
				if (separators == 33620226U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, timeSpanSplitter.V5, out value);
					goto IL_172;
				}
			}
			value = default(TimeSpan);
			flag = false;
			IL_172:
			if (!flag)
			{
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060024D8 RID: 9432 RVA: 0x0013F8CC File Offset: 0x0013EACC
		public static bool TryParse(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed, char standardFormat = '\0')
		{
			if (standardFormat <= 'T')
			{
				if (standardFormat != '\0')
				{
					if (standardFormat == 'G')
					{
						return Utf8Parser.TryParseTimeSpanBigG(source, out value, out bytesConsumed);
					}
					if (standardFormat != 'T')
					{
						goto IL_3E;
					}
				}
			}
			else if (standardFormat != 'c')
			{
				if (standardFormat == 'g')
				{
					return Utf8Parser.TryParseTimeSpanLittleG(source, out value, out bytesConsumed);
				}
				if (standardFormat != 't')
				{
					goto IL_3E;
				}
			}
			return Utf8Parser.TryParseTimeSpanC(source, out value, out bytesConsumed);
			IL_3E:
			return ParserHelpers.TryParseThrowFormatException<TimeSpan>(out value, out bytesConsumed);
		}

		// Token: 0x060024D9 RID: 9433 RVA: 0x0013F920 File Offset: 0x0013EB20
		private unsafe static bool TryParseTimeSpanFraction(ReadOnlySpan<byte> source, out uint value, out int bytesConsumed)
		{
			int num = 0;
			if (num == source.Length)
			{
				value = 0U;
				bytesConsumed = 0;
				return false;
			}
			uint num2 = (uint)(*source[num] - 48);
			if (num2 > 9U)
			{
				value = 0U;
				bytesConsumed = 0;
				return false;
			}
			num++;
			uint num3 = num2;
			int num4 = 1;
			while (num != source.Length)
			{
				num2 = (uint)(*source[num] - 48);
				if (num2 > 9U)
				{
					break;
				}
				num++;
				num4++;
				if (num4 > 7)
				{
					value = 0U;
					bytesConsumed = 0;
					return false;
				}
				num3 = 10U * num3 + num2;
			}
			switch (num4)
			{
			case 2:
				num3 *= 100000U;
				break;
			case 3:
				num3 *= 10000U;
				break;
			case 4:
				num3 *= 1000U;
				break;
			case 5:
				num3 *= 100U;
				break;
			case 6:
				num3 *= 10U;
				break;
			case 7:
				break;
			default:
				num3 *= 1000000U;
				break;
			}
			value = num3;
			bytesConsumed = num;
			return true;
		}

		// Token: 0x060024DA RID: 9434 RVA: 0x0013F9FC File Offset: 0x0013EBFC
		private static bool TryCreateTimeSpan(bool isNegative, uint days, uint hours, uint minutes, uint seconds, uint fraction, out TimeSpan timeSpan)
		{
			if (hours > 23U || minutes > 59U || seconds > 59U)
			{
				timeSpan = default(TimeSpan);
				return false;
			}
			long num = (long)(((ulong)days * 3600UL * 24UL + (ulong)hours * 3600UL + (ulong)minutes * 60UL + (ulong)seconds) * 1000UL);
			long ticks;
			if (isNegative)
			{
				num = -num;
				if (num < -922337203685477L)
				{
					timeSpan = default(TimeSpan);
					return false;
				}
				long num2 = num * 10000L;
				if (num2 < (long)(9223372036854775808UL + (ulong)fraction))
				{
					timeSpan = default(TimeSpan);
					return false;
				}
				ticks = num2 - (long)((ulong)fraction);
			}
			else
			{
				if (num > 922337203685477L)
				{
					timeSpan = default(TimeSpan);
					return false;
				}
				long num3 = num * 10000L;
				if (num3 > (long)(9223372036854775807UL - (ulong)fraction))
				{
					timeSpan = default(TimeSpan);
					return false;
				}
				ticks = num3 + (long)((ulong)fraction);
			}
			timeSpan = new TimeSpan(ticks);
			return true;
		}

		// Token: 0x060024DB RID: 9435 RVA: 0x0013FAE4 File Offset: 0x0013ECE4
		private static bool TryParseTimeSpanLittleG(ReadOnlySpan<byte> source, out TimeSpan value, out int bytesConsumed)
		{
			Utf8Parser.TimeSpanSplitter timeSpanSplitter = default(Utf8Parser.TimeSpanSplitter);
			if (!timeSpanSplitter.TrySplitTimeSpan(source, false, out bytesConsumed))
			{
				value = default(TimeSpan);
				return false;
			}
			bool isNegative = timeSpanSplitter.IsNegative;
			uint separators = timeSpanSplitter.Separators;
			bool flag;
			if (separators <= 16842752U)
			{
				if (separators == 0U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, 0U, 0U, 0U, 0U, out value);
					goto IL_133;
				}
				if (separators == 16777216U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, 0U, 0U, out value);
					goto IL_133;
				}
				if (separators == 16842752U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, 0U, out value);
					goto IL_133;
				}
			}
			else
			{
				if (separators == 16843008U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, 0U, out value);
					goto IL_133;
				}
				if (separators == 16843010U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, timeSpanSplitter.V5, out value);
					goto IL_133;
				}
				if (separators == 16843264U)
				{
					flag = Utf8Parser.TryCreateTimeSpan(isNegative, 0U, timeSpanSplitter.V1, timeSpanSplitter.V2, timeSpanSplitter.V3, timeSpanSplitter.V4, out value);
					goto IL_133;
				}
			}
			value = default(TimeSpan);
			flag = false;
			IL_133:
			if (!flag)
			{
				bytesConsumed = 0;
				return false;
			}
			return true;
		}

		// Token: 0x040009AD RID: 2477
		private static readonly int[] s_daysToMonth365 = new int[]
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

		// Token: 0x040009AE RID: 2478
		private static readonly int[] s_daysToMonth366 = new int[]
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

		// Token: 0x0200025C RID: 604
		[Flags]
		private enum ParseNumberOptions
		{
			// Token: 0x040009B0 RID: 2480
			AllowExponent = 1
		}

		// Token: 0x0200025D RID: 605
		private enum ComponentParseResult : byte
		{
			// Token: 0x040009B2 RID: 2482
			NoMoreData,
			// Token: 0x040009B3 RID: 2483
			Colon,
			// Token: 0x040009B4 RID: 2484
			Period,
			// Token: 0x040009B5 RID: 2485
			ParseFailure
		}

		// Token: 0x0200025E RID: 606
		private struct TimeSpanSplitter
		{
			// Token: 0x060024DD RID: 9437 RVA: 0x0013FC60 File Offset: 0x0013EE60
			public unsafe bool TrySplitTimeSpan(ReadOnlySpan<byte> source, bool periodUsedToSeparateDay, out int bytesConsumed)
			{
				int num = 0;
				byte b = 0;
				while (num != source.Length)
				{
					b = *source[num];
					if (b != 32 && b != 9)
					{
						break;
					}
					num++;
				}
				if (num == source.Length)
				{
					bytesConsumed = 0;
					return false;
				}
				if (b == 45)
				{
					this.IsNegative = true;
					num++;
					if (num == source.Length)
					{
						bytesConsumed = 0;
						return false;
					}
				}
				int num2;
				if (!Utf8Parser.TryParseUInt32D(source.Slice(num), out this.V1, out num2))
				{
					bytesConsumed = 0;
					return false;
				}
				num += num2;
				Utf8Parser.ComponentParseResult componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, periodUsedToSeparateDay, ref num, out this.V2);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)((uint)componentParseResult << 24);
				componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, false, ref num, out this.V3);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)((uint)componentParseResult << 16);
				componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, false, ref num, out this.V4);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)((uint)componentParseResult << 8);
				componentParseResult = Utf8Parser.TimeSpanSplitter.ParseComponent(source, false, ref num, out this.V5);
				if (componentParseResult == Utf8Parser.ComponentParseResult.ParseFailure)
				{
					bytesConsumed = 0;
					return false;
				}
				if (componentParseResult == Utf8Parser.ComponentParseResult.NoMoreData)
				{
					bytesConsumed = num;
					return true;
				}
				this.Separators |= (uint)componentParseResult;
				if (num != source.Length && (*source[num] == 46 || *source[num] == 58))
				{
					bytesConsumed = 0;
					return false;
				}
				bytesConsumed = num;
				return true;
			}

			// Token: 0x060024DE RID: 9438 RVA: 0x0013FDD4 File Offset: 0x0013EFD4
			private unsafe static Utf8Parser.ComponentParseResult ParseComponent(ReadOnlySpan<byte> source, bool neverParseAsFraction, ref int srcIndex, out uint value)
			{
				if (srcIndex == source.Length)
				{
					value = 0U;
					return Utf8Parser.ComponentParseResult.NoMoreData;
				}
				byte b = *source[srcIndex];
				if (b == 58 || (b == 46 && neverParseAsFraction))
				{
					srcIndex++;
					int num;
					if (!Utf8Parser.TryParseUInt32D(source.Slice(srcIndex), out value, out num))
					{
						value = 0U;
						return Utf8Parser.ComponentParseResult.ParseFailure;
					}
					srcIndex += num;
					if (b != 58)
					{
						return Utf8Parser.ComponentParseResult.Period;
					}
					return Utf8Parser.ComponentParseResult.Colon;
				}
				else
				{
					if (b != 46)
					{
						value = 0U;
						return Utf8Parser.ComponentParseResult.NoMoreData;
					}
					srcIndex++;
					int num2;
					if (!Utf8Parser.TryParseTimeSpanFraction(source.Slice(srcIndex), out value, out num2))
					{
						value = 0U;
						return Utf8Parser.ComponentParseResult.ParseFailure;
					}
					srcIndex += num2;
					return Utf8Parser.ComponentParseResult.Period;
				}
			}

			// Token: 0x040009B6 RID: 2486
			public uint V1;

			// Token: 0x040009B7 RID: 2487
			public uint V2;

			// Token: 0x040009B8 RID: 2488
			public uint V3;

			// Token: 0x040009B9 RID: 2489
			public uint V4;

			// Token: 0x040009BA RID: 2490
			public uint V5;

			// Token: 0x040009BB RID: 2491
			public bool IsNegative;

			// Token: 0x040009BC RID: 2492
			public uint Separators;
		}
	}
}
