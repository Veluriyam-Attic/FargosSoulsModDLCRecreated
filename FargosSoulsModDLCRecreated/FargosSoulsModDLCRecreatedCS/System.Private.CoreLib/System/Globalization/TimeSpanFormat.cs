using System;
using System.Buffers.Text;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Globalization
{
	// Token: 0x0200022D RID: 557
	internal static class TimeSpanFormat
	{
		// Token: 0x0600235E RID: 9054 RVA: 0x00135508 File Offset: 0x00134708
		internal static string Format(TimeSpan value, string format, IFormatProvider formatProvider)
		{
			if (string.IsNullOrEmpty(format))
			{
				return TimeSpanFormat.FormatC(value);
			}
			if (format.Length != 1)
			{
				return StringBuilderCache.GetStringAndRelease(TimeSpanFormat.FormatCustomized(value, format, DateTimeFormatInfo.GetInstance(formatProvider), null));
			}
			char c = format[0];
			if (c == 'c' || (c | ' ') == 't')
			{
				return TimeSpanFormat.FormatC(value);
			}
			if ((c | ' ') == 'g')
			{
				return TimeSpanFormat.FormatG(value, DateTimeFormatInfo.GetInstance(formatProvider), (c == 'G') ? TimeSpanFormat.StandardFormat.G : TimeSpanFormat.StandardFormat.g);
			}
			throw new FormatException(SR.Format_InvalidString);
		}

		// Token: 0x0600235F RID: 9055 RVA: 0x0013558C File Offset: 0x0013478C
		internal unsafe static bool TryFormat(TimeSpan value, Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider formatProvider)
		{
			if (format.Length == 0)
			{
				return TimeSpanFormat.TryFormatStandard(value, TimeSpanFormat.StandardFormat.C, null, destination, out charsWritten);
			}
			if (format.Length == 1)
			{
				char c = (char)(*format[0]);
				if (c == 'c' || (c | ' ') == 't')
				{
					return TimeSpanFormat.TryFormatStandard(value, TimeSpanFormat.StandardFormat.C, null, destination, out charsWritten);
				}
				TimeSpanFormat.StandardFormat standardFormat;
				if (c != 'g')
				{
					if (c != 'G')
					{
						throw new FormatException(SR.Format_InvalidString);
					}
					standardFormat = TimeSpanFormat.StandardFormat.G;
				}
				else
				{
					standardFormat = TimeSpanFormat.StandardFormat.g;
				}
				TimeSpanFormat.StandardFormat format2 = standardFormat;
				return TimeSpanFormat.TryFormatStandard(value, format2, DateTimeFormatInfo.GetInstance(formatProvider).DecimalSeparator, destination, out charsWritten);
			}
			else
			{
				StringBuilder stringBuilder = TimeSpanFormat.FormatCustomized(value, format, DateTimeFormatInfo.GetInstance(formatProvider), null);
				if (stringBuilder.Length <= destination.Length)
				{
					stringBuilder.CopyTo(0, destination, stringBuilder.Length);
					charsWritten = stringBuilder.Length;
					StringBuilderCache.Release(stringBuilder);
					return true;
				}
				charsWritten = 0;
				StringBuilderCache.Release(stringBuilder);
				return false;
			}
		}

		// Token: 0x06002360 RID: 9056 RVA: 0x00135650 File Offset: 0x00134850
		internal unsafe static string FormatC(TimeSpan value)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)52], 26);
			Span<char> destination = span;
			int length;
			TimeSpanFormat.TryFormatStandard(value, TimeSpanFormat.StandardFormat.C, null, destination, out length);
			return new string(destination.Slice(0, length));
		}

		// Token: 0x06002361 RID: 9057 RVA: 0x0013568C File Offset: 0x0013488C
		private unsafe static string FormatG(TimeSpan value, DateTimeFormatInfo dtfi, TimeSpanFormat.StandardFormat format)
		{
			string decimalSeparator = dtfi.DecimalSeparator;
			int num = 25 + decimalSeparator.Length;
			Span<char> span2;
			if (num < 128)
			{
				int num2 = num;
				Span<char> span = new Span<char>(stackalloc byte[checked(unchecked((UIntPtr)num2) * 2)], num2);
				span2 = span;
			}
			else
			{
				span2 = new char[num];
			}
			Span<char> destination = span2;
			int length;
			TimeSpanFormat.TryFormatStandard(value, format, decimalSeparator, destination, out length);
			return new string(destination.Slice(0, length));
		}

		// Token: 0x06002362 RID: 9058 RVA: 0x001356FC File Offset: 0x001348FC
		private unsafe static bool TryFormatStandard(TimeSpan value, TimeSpanFormat.StandardFormat format, string decimalSeparator, Span<char> destination, out int charsWritten)
		{
			int num = 8;
			long num2 = value.Ticks;
			uint num3;
			ulong num4;
			if (num2 < 0L)
			{
				num = 9;
				num2 = -num2;
				if (num2 < 0L)
				{
					num3 = 4775808U;
					num4 = 922337203685UL;
					goto IL_45;
				}
			}
			ulong num5;
			num4 = Math.DivRem((ulong)num2, 10000000UL, out num5);
			num3 = (uint)num5;
			IL_45:
			int num6 = 0;
			if (format != TimeSpanFormat.StandardFormat.C)
			{
				if (format != TimeSpanFormat.StandardFormat.G)
				{
					if (num3 != 0U)
					{
						num6 = 7 - FormattingHelpers.CountDecimalTrailingZeros(num3, out num3);
						num += num6 + 1;
					}
				}
				else
				{
					num6 = 7;
					num += num6 + 1;
				}
			}
			else if (num3 != 0U)
			{
				num6 = 7;
				num += num6 + 1;
			}
			ulong num7 = 0UL;
			ulong num8 = 0UL;
			if (num4 > 0UL)
			{
				num7 = Math.DivRem(num4, 60UL, out num8);
			}
			ulong num9 = 0UL;
			ulong num10 = 0UL;
			if (num7 > 0UL)
			{
				num9 = Math.DivRem(num7, 60UL, out num10);
			}
			uint num11 = 0U;
			uint num12 = 0U;
			if (num9 > 0UL)
			{
				num11 = Math.DivRem((uint)num9, 24U, out num12);
			}
			int num13 = 2;
			if (format == TimeSpanFormat.StandardFormat.g && num12 < 10U)
			{
				num13 = 1;
				num--;
			}
			int num14 = 0;
			if (num11 > 0U)
			{
				num14 = FormattingHelpers.CountDigits(num11);
				num += num14 + 1;
			}
			else if (format == TimeSpanFormat.StandardFormat.G)
			{
				num += 2;
				num14 = 1;
			}
			if (destination.Length < num)
			{
				charsWritten = 0;
				return false;
			}
			int num15 = 0;
			if (value.Ticks < 0L)
			{
				*destination[num15++] = '-';
			}
			if (num14 != 0)
			{
				TimeSpanFormat.WriteDigits(num11, destination.Slice(num15, num14));
				num15 += num14;
				*destination[num15++] = ((format == TimeSpanFormat.StandardFormat.C) ? '.' : ':');
			}
			if (num13 == 2)
			{
				TimeSpanFormat.WriteTwoDigits(num12, destination.Slice(num15));
				num15 += 2;
			}
			else
			{
				*destination[num15++] = (char)(48U + num12);
			}
			*destination[num15++] = ':';
			TimeSpanFormat.WriteTwoDigits((uint)num10, destination.Slice(num15));
			num15 += 2;
			*destination[num15++] = ':';
			TimeSpanFormat.WriteTwoDigits((uint)num8, destination.Slice(num15));
			num15 += 2;
			if (num6 != 0)
			{
				if (format == TimeSpanFormat.StandardFormat.C)
				{
					*destination[num15++] = '.';
				}
				else if (decimalSeparator.Length == 1)
				{
					*destination[num15++] = decimalSeparator[0];
				}
				else
				{
					decimalSeparator.AsSpan().CopyTo(destination);
					num15 += decimalSeparator.Length;
				}
				TimeSpanFormat.WriteDigits(num3, destination.Slice(num15, num6));
				num15 += num6;
			}
			charsWritten = num;
			return true;
		}

		// Token: 0x06002363 RID: 9059 RVA: 0x00135964 File Offset: 0x00134B64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteTwoDigits(uint value, Span<char> buffer)
		{
			uint num = 48U + value;
			value /= 10U;
			*buffer[1] = (char)(num - value * 10U);
			*buffer[0] = (char)(48U + value);
		}

		// Token: 0x06002364 RID: 9060 RVA: 0x0013599C File Offset: 0x00134B9C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteDigits(uint value, Span<char> buffer)
		{
			for (int i = buffer.Length - 1; i >= 1; i--)
			{
				uint num = 48U + value;
				value /= 10U;
				*buffer[i] = (char)(num - value * 10U);
			}
			*buffer[0] = (char)(48U + value);
		}

		// Token: 0x06002365 RID: 9061 RVA: 0x001359E8 File Offset: 0x00134BE8
		private unsafe static StringBuilder FormatCustomized(TimeSpan value, ReadOnlySpan<char> format, DateTimeFormatInfo dtfi, StringBuilder result = null)
		{
			bool flag = false;
			if (result == null)
			{
				result = StringBuilderCache.Acquire(16);
				flag = true;
			}
			int num = (int)(value.Ticks / 864000000000L);
			long num2 = value.Ticks % 864000000000L;
			if (value.Ticks < 0L)
			{
				num = -num;
				num2 = -num2;
			}
			int value2 = (int)(num2 / 36000000000L % 24L);
			int value3 = (int)(num2 / 600000000L % 60L);
			int value4 = (int)(num2 / 10000000L % 60L);
			int num3 = (int)(num2 % 10000000L);
			int i = 0;
			while (i < format.Length)
			{
				char c = (char)(*format[i]);
				int num5;
				if (c <= 'F')
				{
					if (c <= '%')
					{
						if (c != '"')
						{
							if (c != '%')
							{
								goto IL_2A7;
							}
							int num4 = DateTimeFormat.ParseNextChar(format, i);
							if (num4 >= 0 && num4 != 37)
							{
								char c2 = (char)num4;
								StringBuilder stringBuilder = TimeSpanFormat.FormatCustomized(value, MemoryMarshal.CreateReadOnlySpan<char>(ref c2, 1), dtfi, result);
								num5 = 2;
								goto IL_2BB;
							}
							goto IL_2A7;
						}
					}
					else if (c != '\'')
					{
						if (c != 'F')
						{
							goto IL_2A7;
						}
						num5 = DateTimeFormat.ParseRepeatPattern(format, i, c);
						if (num5 > 7)
						{
							goto IL_2A7;
						}
						long num6 = (long)num3;
						num6 /= TimeSpanParse.Pow10(7 - num5);
						int num7 = num5;
						while (num7 > 0 && num6 % 10L == 0L)
						{
							num6 /= 10L;
							num7--;
						}
						if (num7 > 0)
						{
							result.AppendSpanFormattable<long>(num6, DateTimeFormat.fixedNumberFormats[num7 - 1], CultureInfo.InvariantCulture);
							goto IL_2BB;
						}
						goto IL_2BB;
					}
					num5 = DateTimeFormat.ParseQuoteString(format, i, result);
				}
				else if (c <= 'h')
				{
					if (c != '\\')
					{
						switch (c)
						{
						case 'd':
							num5 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num5 > 8)
							{
								goto IL_2A7;
							}
							DateTimeFormat.FormatDigits(result, num, num5, true);
							break;
						case 'e':
						case 'g':
							goto IL_2A7;
						case 'f':
						{
							num5 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num5 > 7)
							{
								goto IL_2A7;
							}
							long num6 = (long)num3;
							num6 /= TimeSpanParse.Pow10(7 - num5);
							result.AppendSpanFormattable<long>(num6, DateTimeFormat.fixedNumberFormats[num5 - 1], CultureInfo.InvariantCulture);
							break;
						}
						case 'h':
							num5 = DateTimeFormat.ParseRepeatPattern(format, i, c);
							if (num5 > 2)
							{
								goto IL_2A7;
							}
							DateTimeFormat.FormatDigits(result, value2, num5);
							break;
						default:
							goto IL_2A7;
						}
					}
					else
					{
						int num4 = DateTimeFormat.ParseNextChar(format, i);
						if (num4 < 0)
						{
							goto IL_2A7;
						}
						result.Append((char)num4);
						num5 = 2;
					}
				}
				else if (c != 'm')
				{
					if (c != 's')
					{
						goto IL_2A7;
					}
					num5 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num5 > 2)
					{
						goto IL_2A7;
					}
					DateTimeFormat.FormatDigits(result, value4, num5);
				}
				else
				{
					num5 = DateTimeFormat.ParseRepeatPattern(format, i, c);
					if (num5 > 2)
					{
						goto IL_2A7;
					}
					DateTimeFormat.FormatDigits(result, value3, num5);
				}
				IL_2BB:
				i += num5;
				continue;
				IL_2A7:
				if (flag)
				{
					StringBuilderCache.Release(result);
				}
				throw new FormatException(SR.Format_InvalidString);
			}
			return result;
		}

		// Token: 0x040008F5 RID: 2293
		internal static readonly TimeSpanFormat.FormatLiterals PositiveInvariantFormatLiterals = TimeSpanFormat.FormatLiterals.InitInvariant(false);

		// Token: 0x040008F6 RID: 2294
		internal static readonly TimeSpanFormat.FormatLiterals NegativeInvariantFormatLiterals = TimeSpanFormat.FormatLiterals.InitInvariant(true);

		// Token: 0x0200022E RID: 558
		private enum StandardFormat
		{
			// Token: 0x040008F8 RID: 2296
			C,
			// Token: 0x040008F9 RID: 2297
			G,
			// Token: 0x040008FA RID: 2298
			g
		}

		// Token: 0x0200022F RID: 559
		internal struct FormatLiterals
		{
			// Token: 0x170007EE RID: 2030
			// (get) Token: 0x06002367 RID: 9063 RVA: 0x00135CDE File Offset: 0x00134EDE
			internal string Start
			{
				get
				{
					return this._literals[0];
				}
			}

			// Token: 0x170007EF RID: 2031
			// (get) Token: 0x06002368 RID: 9064 RVA: 0x00135CE8 File Offset: 0x00134EE8
			internal string DayHourSep
			{
				get
				{
					return this._literals[1];
				}
			}

			// Token: 0x170007F0 RID: 2032
			// (get) Token: 0x06002369 RID: 9065 RVA: 0x00135CF2 File Offset: 0x00134EF2
			internal string HourMinuteSep
			{
				get
				{
					return this._literals[2];
				}
			}

			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x0600236A RID: 9066 RVA: 0x00135CFC File Offset: 0x00134EFC
			internal string MinuteSecondSep
			{
				get
				{
					return this._literals[3];
				}
			}

			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x0600236B RID: 9067 RVA: 0x00135D06 File Offset: 0x00134F06
			internal string SecondFractionSep
			{
				get
				{
					return this._literals[4];
				}
			}

			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x0600236C RID: 9068 RVA: 0x00135D10 File Offset: 0x00134F10
			internal string End
			{
				get
				{
					return this._literals[5];
				}
			}

			// Token: 0x0600236D RID: 9069 RVA: 0x00135D1C File Offset: 0x00134F1C
			internal static TimeSpanFormat.FormatLiterals InitInvariant(bool isNegative)
			{
				TimeSpanFormat.FormatLiterals formatLiterals = new TimeSpanFormat.FormatLiterals
				{
					_literals = new string[6]
				};
				formatLiterals._literals[0] = (isNegative ? "-" : string.Empty);
				formatLiterals._literals[1] = ".";
				formatLiterals._literals[2] = ":";
				formatLiterals._literals[3] = ":";
				formatLiterals._literals[4] = ".";
				formatLiterals._literals[5] = string.Empty;
				formatLiterals.AppCompatLiteral = ":.";
				formatLiterals.dd = 2;
				formatLiterals.hh = 2;
				formatLiterals.mm = 2;
				formatLiterals.ss = 2;
				formatLiterals.ff = 7;
				return formatLiterals;
			}

			// Token: 0x0600236E RID: 9070 RVA: 0x00135DCC File Offset: 0x00134FCC
			internal unsafe void Init(ReadOnlySpan<char> format, bool useInvariantFieldLengths)
			{
				this.dd = (this.hh = (this.mm = (this.ss = (this.ff = 0))));
				this._literals = new string[6];
				for (int i = 0; i < this._literals.Length; i++)
				{
					this._literals[i] = string.Empty;
				}
				StringBuilder stringBuilder = StringBuilderCache.Acquire(16);
				bool flag = false;
				char c = '\'';
				int num = 0;
				int j = 0;
				while (j < format.Length)
				{
					char c2 = (char)(*format[j]);
					if (c2 <= 'F')
					{
						if (c2 <= '%')
						{
							if (c2 != '"')
							{
								if (c2 != '%')
								{
									goto IL_1C5;
								}
								goto IL_1C5;
							}
						}
						else if (c2 != '\'')
						{
							if (c2 != 'F')
							{
								goto IL_1C5;
							}
							goto IL_1B0;
						}
						if (flag && c == (char)(*format[j]))
						{
							if (num < 0 || num > 5)
							{
								return;
							}
							this._literals[num] = stringBuilder.ToString();
							stringBuilder.Length = 0;
							flag = false;
						}
						else if (!flag)
						{
							c = (char)(*format[j]);
							flag = true;
						}
					}
					else if (c2 <= 'h')
					{
						if (c2 != '\\')
						{
							switch (c2)
							{
							case 'd':
								if (!flag)
								{
									num = 1;
									this.dd++;
								}
								break;
							case 'e':
							case 'g':
								goto IL_1C5;
							case 'f':
								goto IL_1B0;
							case 'h':
								if (!flag)
								{
									num = 2;
									this.hh++;
								}
								break;
							default:
								goto IL_1C5;
							}
						}
						else
						{
							if (flag)
							{
								goto IL_1C5;
							}
							j++;
						}
					}
					else if (c2 != 'm')
					{
						if (c2 != 's')
						{
							goto IL_1C5;
						}
						if (!flag)
						{
							num = 4;
							this.ss++;
						}
					}
					else if (!flag)
					{
						num = 3;
						this.mm++;
					}
					IL_1D6:
					j++;
					continue;
					IL_1B0:
					if (!flag)
					{
						num = 5;
						this.ff++;
						goto IL_1D6;
					}
					goto IL_1D6;
					IL_1C5:
					stringBuilder.Append((char)(*format[j]));
					goto IL_1D6;
				}
				this.AppCompatLiteral = this.MinuteSecondSep + this.SecondFractionSep;
				if (useInvariantFieldLengths)
				{
					this.dd = 2;
					this.hh = 2;
					this.mm = 2;
					this.ss = 2;
					this.ff = 7;
				}
				else
				{
					if (this.dd < 1 || this.dd > 2)
					{
						this.dd = 2;
					}
					if (this.hh < 1 || this.hh > 2)
					{
						this.hh = 2;
					}
					if (this.mm < 1 || this.mm > 2)
					{
						this.mm = 2;
					}
					if (this.ss < 1 || this.ss > 2)
					{
						this.ss = 2;
					}
					if (this.ff < 1 || this.ff > 7)
					{
						this.ff = 7;
					}
				}
				StringBuilderCache.Release(stringBuilder);
			}

			// Token: 0x040008FB RID: 2299
			internal string AppCompatLiteral;

			// Token: 0x040008FC RID: 2300
			internal int dd;

			// Token: 0x040008FD RID: 2301
			internal int hh;

			// Token: 0x040008FE RID: 2302
			internal int mm;

			// Token: 0x040008FF RID: 2303
			internal int ss;

			// Token: 0x04000900 RID: 2304
			internal int ff;

			// Token: 0x04000901 RID: 2305
			private string[] _literals;
		}
	}
}
