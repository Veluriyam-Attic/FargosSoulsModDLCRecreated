using System;
using System.Buffers.Binary;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Buffers.Text
{
	// Token: 0x02000258 RID: 600
	public static class Utf8Formatter
	{
		// Token: 0x0600247D RID: 9341 RVA: 0x0013A37C File Offset: 0x0013957C
		public unsafe static bool TryFormat(bool value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			char symbolOrDefault = FormattingHelpers.GetSymbolOrDefault(format, 'G');
			if (value)
			{
				if (symbolOrDefault == 'G')
				{
					if (!BinaryPrimitives.TryWriteUInt32BigEndian(destination, 1416787301U))
					{
						goto IL_7E;
					}
				}
				else
				{
					if (symbolOrDefault != 'l')
					{
						goto IL_83;
					}
					if (!BinaryPrimitives.TryWriteUInt32BigEndian(destination, 1953658213U))
					{
						goto IL_7E;
					}
				}
				bytesWritten = 4;
				return true;
			}
			if (symbolOrDefault == 'G')
			{
				if (4 >= destination.Length)
				{
					goto IL_7E;
				}
				BinaryPrimitives.WriteUInt32BigEndian(destination, 1180789875U);
			}
			else
			{
				if (symbolOrDefault != 'l')
				{
					goto IL_83;
				}
				if (4 >= destination.Length)
				{
					goto IL_7E;
				}
				BinaryPrimitives.WriteUInt32BigEndian(destination, 1717660787U);
			}
			*destination[4] = 101;
			bytesWritten = 5;
			return true;
			IL_7E:
			bytesWritten = 0;
			return false;
			IL_83:
			return FormattingHelpers.TryFormatThrowFormatException(out bytesWritten);
		}

		// Token: 0x0600247E RID: 9342 RVA: 0x0013A414 File Offset: 0x00139614
		public static bool TryFormat(DateTimeOffset value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			TimeSpan offset = Utf8Constants.NullUtcOffset;
			char c = format.Symbol;
			if (format.IsDefault)
			{
				c = 'G';
				offset = value.Offset;
			}
			if (c <= 'O')
			{
				if (c == 'G')
				{
					return Utf8Formatter.TryFormatDateTimeG(value.DateTime, offset, destination, out bytesWritten);
				}
				if (c == 'O')
				{
					return Utf8Formatter.TryFormatDateTimeO(value.DateTime, value.Offset, destination, out bytesWritten);
				}
			}
			else
			{
				if (c == 'R')
				{
					return Utf8Formatter.TryFormatDateTimeR(value.UtcDateTime, destination, out bytesWritten);
				}
				if (c == 'l')
				{
					return Utf8Formatter.TryFormatDateTimeL(value.UtcDateTime, destination, out bytesWritten);
				}
			}
			return FormattingHelpers.TryFormatThrowFormatException(out bytesWritten);
		}

		// Token: 0x0600247F RID: 9343 RVA: 0x0013A4B4 File Offset: 0x001396B4
		public static bool TryFormat(DateTime value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			char symbolOrDefault = FormattingHelpers.GetSymbolOrDefault(format, 'G');
			if (symbolOrDefault <= 'O')
			{
				if (symbolOrDefault == 'G')
				{
					return Utf8Formatter.TryFormatDateTimeG(value, Utf8Constants.NullUtcOffset, destination, out bytesWritten);
				}
				if (symbolOrDefault == 'O')
				{
					return Utf8Formatter.TryFormatDateTimeO(value, Utf8Constants.NullUtcOffset, destination, out bytesWritten);
				}
			}
			else
			{
				if (symbolOrDefault == 'R')
				{
					return Utf8Formatter.TryFormatDateTimeR(value, destination, out bytesWritten);
				}
				if (symbolOrDefault == 'l')
				{
					return Utf8Formatter.TryFormatDateTimeL(value, destination, out bytesWritten);
				}
			}
			return FormattingHelpers.TryFormatThrowFormatException(out bytesWritten);
		}

		// Token: 0x06002480 RID: 9344 RVA: 0x0013A528 File Offset: 0x00139728
		private unsafe static bool TryFormatDateTimeG(DateTime value, TimeSpan offset, Span<byte> destination, out int bytesWritten)
		{
			int num = 19;
			if (offset != Utf8Constants.NullUtcOffset)
			{
				num += 7;
			}
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = num;
			byte b = *destination[18];
			int value2;
			int value3;
			int value4;
			value.GetDate(out value2, out value3, out value4);
			int value5;
			int value6;
			int value7;
			value.GetTime(out value5, out value6, out value7);
			FormattingHelpers.WriteTwoDecimalDigits((uint)value3, destination, 0);
			*destination[2] = 47;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value4, destination, 3);
			*destination[5] = 47;
			FormattingHelpers.WriteFourDecimalDigits((uint)value2, destination, 6);
			*destination[10] = 32;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value5, destination, 11);
			*destination[13] = 58;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value6, destination, 14);
			*destination[16] = 58;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value7, destination, 17);
			if (offset != Utf8Constants.NullUtcOffset)
			{
				int num2 = (int)(offset.Ticks / 600000000L);
				byte b2;
				if (num2 < 0)
				{
					b2 = 45;
					num2 = -num2;
				}
				else
				{
					b2 = 43;
				}
				int value9;
				int value8 = Math.DivRem(num2, 60, out value9);
				FormattingHelpers.WriteTwoDecimalDigits((uint)value9, destination, 24);
				*destination[23] = 58;
				FormattingHelpers.WriteTwoDecimalDigits((uint)value8, destination, 21);
				*destination[20] = b2;
				*destination[19] = 32;
			}
			return true;
		}

		// Token: 0x06002481 RID: 9345 RVA: 0x0013A66C File Offset: 0x0013986C
		private unsafe static bool TryFormatDateTimeL(DateTime value, Span<byte> destination, out int bytesWritten)
		{
			if (28 >= destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			int value2;
			int num;
			int value3;
			value.GetDate(out value2, out num, out value3);
			int value4;
			int value5;
			int value6;
			value.GetTime(out value4, out value5, out value6);
			uint num2 = Utf8Formatter.s_dayAbbreviationsLowercase[(int)value.DayOfWeek];
			*destination[0] = (byte)num2;
			num2 >>= 8;
			*destination[1] = (byte)num2;
			num2 >>= 8;
			*destination[2] = (byte)num2;
			*destination[3] = 44;
			*destination[4] = 32;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value3, destination, 5);
			*destination[7] = 32;
			uint num3 = Utf8Formatter.s_monthAbbreviationsLowercase[num - 1];
			*destination[8] = (byte)num3;
			num3 >>= 8;
			*destination[9] = (byte)num3;
			num3 >>= 8;
			*destination[10] = (byte)num3;
			*destination[11] = 32;
			FormattingHelpers.WriteFourDecimalDigits((uint)value2, destination, 12);
			*destination[16] = 32;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value4, destination, 17);
			*destination[19] = 58;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value5, destination, 20);
			*destination[22] = 58;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value6, destination, 23);
			*destination[25] = 32;
			*destination[26] = 103;
			*destination[27] = 109;
			*destination[28] = 116;
			bytesWritten = 29;
			return true;
		}

		// Token: 0x06002482 RID: 9346 RVA: 0x0013A7D4 File Offset: 0x001399D4
		private unsafe static bool TryFormatDateTimeO(DateTime value, TimeSpan offset, Span<byte> destination, out int bytesWritten)
		{
			int num = 27;
			DateTimeKind dateTimeKind = DateTimeKind.Local;
			if (offset == Utf8Constants.NullUtcOffset)
			{
				dateTimeKind = value.Kind;
				if (dateTimeKind == DateTimeKind.Local)
				{
					offset = TimeZoneInfo.Local.GetUtcOffset(value);
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
				bytesWritten = 0;
				return false;
			}
			bytesWritten = num;
			ref byte ptr = ref destination[26];
			int value2;
			int value3;
			int value4;
			value.GetDate(out value2, out value3, out value4);
			int value5;
			int value6;
			int value7;
			int value8;
			value.GetTimePrecise(out value5, out value6, out value7, out value8);
			FormattingHelpers.WriteFourDecimalDigits((uint)value2, destination, 0);
			*destination[4] = 45;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value3, destination, 5);
			*destination[7] = 45;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value4, destination, 8);
			*destination[10] = 84;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value5, destination, 11);
			*destination[13] = 58;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value6, destination, 14);
			*destination[16] = 58;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value7, destination, 17);
			*destination[19] = 46;
			FormattingHelpers.WriteDigits((uint)value8, destination.Slice(20, 7));
			if (dateTimeKind == DateTimeKind.Local)
			{
				int num2 = (int)(offset.Ticks / 600000000L);
				byte b;
				if (num2 < 0)
				{
					b = 45;
					num2 = -num2;
				}
				else
				{
					b = 43;
				}
				int value10;
				int value9 = Math.DivRem(num2, 60, out value10);
				FormattingHelpers.WriteTwoDecimalDigits((uint)value10, destination, 31);
				*destination[30] = 58;
				FormattingHelpers.WriteTwoDecimalDigits((uint)value9, destination, 28);
				*destination[27] = b;
			}
			else if (dateTimeKind == DateTimeKind.Utc)
			{
				*destination[27] = 90;
			}
			return true;
		}

		// Token: 0x06002483 RID: 9347 RVA: 0x0013A95C File Offset: 0x00139B5C
		private unsafe static bool TryFormatDateTimeR(DateTime value, Span<byte> destination, out int bytesWritten)
		{
			if (28 >= destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			int value2;
			int num;
			int value3;
			value.GetDate(out value2, out num, out value3);
			int value4;
			int value5;
			int value6;
			value.GetTime(out value4, out value5, out value6);
			uint num2 = Utf8Formatter.s_dayAbbreviations[(int)value.DayOfWeek];
			*destination[0] = (byte)num2;
			num2 >>= 8;
			*destination[1] = (byte)num2;
			num2 >>= 8;
			*destination[2] = (byte)num2;
			*destination[3] = 44;
			*destination[4] = 32;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value3, destination, 5);
			*destination[7] = 32;
			uint num3 = Utf8Formatter.s_monthAbbreviations[num - 1];
			*destination[8] = (byte)num3;
			num3 >>= 8;
			*destination[9] = (byte)num3;
			num3 >>= 8;
			*destination[10] = (byte)num3;
			*destination[11] = 32;
			FormattingHelpers.WriteFourDecimalDigits((uint)value2, destination, 12);
			*destination[16] = 32;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value4, destination, 17);
			*destination[19] = 58;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value5, destination, 20);
			*destination[22] = 58;
			FormattingHelpers.WriteTwoDecimalDigits((uint)value6, destination, 23);
			*destination[25] = 32;
			*destination[26] = 71;
			*destination[27] = 77;
			*destination[28] = 84;
			bytesWritten = 29;
			return true;
		}

		// Token: 0x06002484 RID: 9348 RVA: 0x0013AAC4 File Offset: 0x00139CC4
		public unsafe static bool TryFormat(decimal value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			if (format.IsDefault)
			{
				format = 'G';
			}
			char symbol = format.Symbol;
			switch (symbol)
			{
			case 'E':
				goto IL_F4;
			case 'F':
				goto IL_A0;
			case 'G':
				break;
			default:
				switch (symbol)
				{
				case 'e':
					goto IL_F4;
				case 'f':
					goto IL_A0;
				case 'g':
					break;
				default:
					return FormattingHelpers.TryFormatThrowFormatException(out bytesWritten);
				}
				break;
			}
			if (format.Precision != 255)
			{
				throw new NotSupportedException(SR.Argument_GWithPrecisionNotSupported);
			}
			byte* digits = stackalloc byte[(UIntPtr)31];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Decimal, digits, 31);
			Number.DecimalToNumber(ref value, ref numberBuffer);
			if (*numberBuffer.Digits[0] == 0)
			{
				numberBuffer.IsNegative = false;
			}
			return Utf8Formatter.TryFormatDecimalG(ref numberBuffer, destination, out bytesWritten);
			IL_A0:
			byte* digits2 = stackalloc byte[(UIntPtr)31];
			Number.NumberBuffer numberBuffer2 = new Number.NumberBuffer(Number.NumberBufferKind.Decimal, digits2, 31);
			Number.DecimalToNumber(ref value, ref numberBuffer2);
			byte b = (format.Precision == byte.MaxValue) ? 2 : format.Precision;
			Number.RoundNumber(ref numberBuffer2, numberBuffer2.Scale + (int)b, false);
			return Utf8Formatter.TryFormatDecimalF(ref numberBuffer2, destination, out bytesWritten, b);
			IL_F4:
			byte* digits3 = stackalloc byte[(UIntPtr)31];
			Number.NumberBuffer numberBuffer3 = new Number.NumberBuffer(Number.NumberBufferKind.Decimal, digits3, 31);
			Number.DecimalToNumber(ref value, ref numberBuffer3);
			byte b2 = (format.Precision == byte.MaxValue) ? 6 : format.Precision;
			Number.RoundNumber(ref numberBuffer3, (int)(b2 + 1), false);
			return Utf8Formatter.TryFormatDecimalE(ref numberBuffer3, destination, out bytesWritten, b2, (byte)format.Symbol);
		}

		// Token: 0x06002485 RID: 9349 RVA: 0x0013AC24 File Offset: 0x00139E24
		private unsafe static bool TryFormatDecimalE(ref Number.NumberBuffer number, Span<byte> destination, out int bytesWritten, byte precision, byte exponentSymbol)
		{
			int scale = number.Scale;
			ReadOnlySpan<byte> readOnlySpan = number.Digits;
			int num = (int)((number.IsNegative ? 1 : 0) + 1 + ((precision == 0) ? 0 : (precision + 1)) + 2 + 3);
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			int num2 = 0;
			int num3 = 0;
			if (number.IsNegative)
			{
				*destination[num2++] = 45;
			}
			byte b = *readOnlySpan[num3];
			int num4;
			if (b == 0)
			{
				*destination[num2++] = 48;
				num4 = 0;
			}
			else
			{
				*destination[num2++] = b;
				num3++;
				num4 = scale - 1;
			}
			if (precision > 0)
			{
				*destination[num2++] = 46;
				for (int i = 0; i < (int)precision; i++)
				{
					byte b2 = *readOnlySpan[num3];
					if (b2 == 0)
					{
						while (i++ < (int)precision)
						{
							*destination[num2++] = 48;
						}
						break;
					}
					*destination[num2++] = b2;
					num3++;
				}
			}
			*destination[num2++] = exponentSymbol;
			if (num4 >= 0)
			{
				*destination[num2++] = 43;
			}
			else
			{
				*destination[num2++] = 45;
				num4 = -num4;
			}
			*destination[num2++] = 48;
			*destination[num2++] = (byte)(num4 / 10 + 48);
			*destination[num2++] = (byte)(num4 % 10 + 48);
			bytesWritten = num;
			return true;
		}

		// Token: 0x06002486 RID: 9350 RVA: 0x0013ADA8 File Offset: 0x00139FA8
		private unsafe static bool TryFormatDecimalF(ref Number.NumberBuffer number, Span<byte> destination, out int bytesWritten, byte precision)
		{
			int scale = number.Scale;
			ReadOnlySpan<byte> readOnlySpan = number.Digits;
			int num = (number.IsNegative ? 1 : 0) + ((scale <= 0) ? 1 : scale) + (int)((precision == 0) ? 0 : (precision + 1));
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			int i = 0;
			int num2 = 0;
			if (number.IsNegative)
			{
				*destination[num2++] = 45;
			}
			if (scale <= 0)
			{
				*destination[num2++] = 48;
			}
			else
			{
				while (i < scale)
				{
					byte b = *readOnlySpan[i];
					if (b == 0)
					{
						int num3 = scale - i;
						for (int j = 0; j < num3; j++)
						{
							*destination[num2++] = 48;
						}
						break;
					}
					*destination[num2++] = b;
					i++;
				}
			}
			if (precision > 0)
			{
				*destination[num2++] = 46;
				int k = 0;
				if (scale < 0)
				{
					int num4 = Math.Min((int)precision, -scale);
					for (int l = 0; l < num4; l++)
					{
						*destination[num2++] = 48;
					}
					k += num4;
				}
				while (k < (int)precision)
				{
					byte b2 = *readOnlySpan[i];
					if (b2 == 0)
					{
						while (k++ < (int)precision)
						{
							*destination[num2++] = 48;
						}
						break;
					}
					*destination[num2++] = b2;
					i++;
					k++;
				}
			}
			bytesWritten = num;
			return true;
		}

		// Token: 0x06002487 RID: 9351 RVA: 0x0013AF24 File Offset: 0x0013A124
		private unsafe static bool TryFormatDecimalG(ref Number.NumberBuffer number, Span<byte> destination, out int bytesWritten)
		{
			int scale = number.Scale;
			ReadOnlySpan<byte> readOnlySpan = number.Digits;
			int digitsCount = number.DigitsCount;
			bool flag = scale < digitsCount;
			int num;
			if (flag)
			{
				num = digitsCount + 1;
				if (scale <= 0)
				{
					num += 1 + -scale;
				}
			}
			else
			{
				num = ((scale <= 0) ? 1 : scale);
			}
			if (number.IsNegative)
			{
				num++;
			}
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			int i = 0;
			int num2 = 0;
			if (number.IsNegative)
			{
				*destination[num2++] = 45;
			}
			if (scale <= 0)
			{
				*destination[num2++] = 48;
			}
			else
			{
				while (i < scale)
				{
					byte b = *readOnlySpan[i];
					if (b == 0)
					{
						int num3 = scale - i;
						for (int j = 0; j < num3; j++)
						{
							*destination[num2++] = 48;
						}
						break;
					}
					*destination[num2++] = b;
					i++;
				}
			}
			if (flag)
			{
				*destination[num2++] = 46;
				if (scale < 0)
				{
					int num4 = -scale;
					for (int k = 0; k < num4; k++)
					{
						*destination[num2++] = 48;
					}
				}
				byte b2;
				while ((b2 = *readOnlySpan[i++]) != 0)
				{
					*destination[num2++] = b2;
				}
			}
			bytesWritten = num;
			return true;
		}

		// Token: 0x06002488 RID: 9352 RVA: 0x0013B085 File Offset: 0x0013A285
		public static bool TryFormat(double value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatFloatingPoint<double>(value, destination, out bytesWritten, format);
		}

		// Token: 0x06002489 RID: 9353 RVA: 0x0013B090 File Offset: 0x0013A290
		public static bool TryFormat(float value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatFloatingPoint<float>(value, destination, out bytesWritten, format);
		}

		// Token: 0x0600248A RID: 9354 RVA: 0x0013B09C File Offset: 0x0013A29C
		private unsafe static bool TryFormatFloatingPoint<T>(T value, Span<byte> destination, out int bytesWritten, StandardFormat format) where T : IFormattable, ISpanFormattable
		{
			Span<char> span = default(Span<char>);
			Span<char> span2;
			if (!format.IsDefault)
			{
				span2 = new Span<char>(stackalloc byte[(UIntPtr)6], 3);
				span = span2;
				int length = format.Format(span);
				span = span.Slice(0, length);
			}
			span2 = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
			Span<char> destination2 = span2;
			ReadOnlySpan<char> chars = default(Span<char>);
			int length2;
			if (value.TryFormat(destination2, out length2, span, CultureInfo.InvariantCulture))
			{
				chars = destination2.Slice(0, length2);
			}
			else
			{
				if (destination.Length <= 128)
				{
					bytesWritten = 0;
					return false;
				}
				chars = value.ToString(new string(span), CultureInfo.InvariantCulture);
			}
			if (chars.Length > destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			bool result;
			try
			{
				bytesWritten = Encoding.UTF8.GetBytes(chars, destination);
				result = true;
			}
			catch
			{
				bytesWritten = 0;
				result = false;
			}
			return result;
		}

		// Token: 0x0600248B RID: 9355 RVA: 0x0013B1B0 File Offset: 0x0013A3B0
		public unsafe static bool TryFormat(Guid value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			char symbolOrDefault = FormattingHelpers.GetSymbolOrDefault(format, 'D');
			int num;
			if (symbolOrDefault <= 'D')
			{
				if (symbolOrDefault == 'B')
				{
					num = -2139260122;
					goto IL_4B;
				}
				if (symbolOrDefault == 'D')
				{
					num = -2147483612;
					goto IL_4B;
				}
			}
			else
			{
				if (symbolOrDefault == 'N')
				{
					num = 32;
					goto IL_4B;
				}
				if (symbolOrDefault == 'P')
				{
					num = -2144786394;
					goto IL_4B;
				}
			}
			return FormattingHelpers.TryFormatThrowFormatException(out bytesWritten);
			IL_4B:
			if ((int)((byte)num) > destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = (int)((byte)num);
			num >>= 8;
			if ((byte)num != 0)
			{
				*destination[0] = (byte)num;
				destination = destination.Slice(1);
			}
			num >>= 8;
			Utf8Formatter.DecomposedGuid decomposedGuid = default(Utf8Formatter.DecomposedGuid);
			decomposedGuid.Guid = value;
			ref byte ptr = ref destination[8];
			HexConverter.ToBytesBuffer(decomposedGuid.Byte03, destination, 0, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte02, destination, 2, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte01, destination, 4, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte00, destination, 6, HexConverter.Casing.Lower);
			if (num < 0)
			{
				*destination[8] = 45;
				destination = destination.Slice(9);
			}
			else
			{
				destination = destination.Slice(8);
			}
			ref byte ptr2 = ref destination[4];
			HexConverter.ToBytesBuffer(decomposedGuid.Byte05, destination, 0, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte04, destination, 2, HexConverter.Casing.Lower);
			if (num < 0)
			{
				*destination[4] = 45;
				destination = destination.Slice(5);
			}
			else
			{
				destination = destination.Slice(4);
			}
			ref byte ptr3 = ref destination[4];
			HexConverter.ToBytesBuffer(decomposedGuid.Byte07, destination, 0, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte06, destination, 2, HexConverter.Casing.Lower);
			if (num < 0)
			{
				*destination[4] = 45;
				destination = destination.Slice(5);
			}
			else
			{
				destination = destination.Slice(4);
			}
			ref byte ptr4 = ref destination[4];
			HexConverter.ToBytesBuffer(decomposedGuid.Byte08, destination, 0, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte09, destination, 2, HexConverter.Casing.Lower);
			if (num < 0)
			{
				*destination[4] = 45;
				destination = destination.Slice(5);
			}
			else
			{
				destination = destination.Slice(4);
			}
			ref byte ptr5 = ref destination[11];
			HexConverter.ToBytesBuffer(decomposedGuid.Byte10, destination, 0, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte11, destination, 2, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte12, destination, 4, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte13, destination, 6, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte14, destination, 8, HexConverter.Casing.Lower);
			HexConverter.ToBytesBuffer(decomposedGuid.Byte15, destination, 10, HexConverter.Casing.Lower);
			if ((byte)num != 0)
			{
				*destination[12] = (byte)num;
			}
			return true;
		}

		// Token: 0x0600248C RID: 9356 RVA: 0x0013B442 File Offset: 0x0013A642
		public static bool TryFormat(byte value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatUInt64((ulong)value, destination, out bytesWritten, format);
		}

		// Token: 0x0600248D RID: 9357 RVA: 0x0013B44E File Offset: 0x0013A64E
		[CLSCompliant(false)]
		public static bool TryFormat(sbyte value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatInt64((long)value, 255UL, destination, out bytesWritten, format);
		}

		// Token: 0x0600248E RID: 9358 RVA: 0x0013B442 File Offset: 0x0013A642
		[CLSCompliant(false)]
		public static bool TryFormat(ushort value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatUInt64((ulong)value, destination, out bytesWritten, format);
		}

		// Token: 0x0600248F RID: 9359 RVA: 0x0013B460 File Offset: 0x0013A660
		public static bool TryFormat(short value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatInt64((long)value, 65535UL, destination, out bytesWritten, format);
		}

		// Token: 0x06002490 RID: 9360 RVA: 0x0013B442 File Offset: 0x0013A642
		[CLSCompliant(false)]
		public static bool TryFormat(uint value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatUInt64((ulong)value, destination, out bytesWritten, format);
		}

		// Token: 0x06002491 RID: 9361 RVA: 0x0013B472 File Offset: 0x0013A672
		public static bool TryFormat(int value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatInt64((long)value, (ulong)-1, destination, out bytesWritten, format);
		}

		// Token: 0x06002492 RID: 9362 RVA: 0x0013B480 File Offset: 0x0013A680
		[CLSCompliant(false)]
		public static bool TryFormat(ulong value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatUInt64(value, destination, out bytesWritten, format);
		}

		// Token: 0x06002493 RID: 9363 RVA: 0x0013B48B File Offset: 0x0013A68B
		public static bool TryFormat(long value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			return Utf8Formatter.TryFormatInt64(value, ulong.MaxValue, destination, out bytesWritten, format);
		}

		// Token: 0x06002494 RID: 9364 RVA: 0x0013B498 File Offset: 0x0013A698
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryFormatInt64(long value, ulong mask, Span<byte> destination, out int bytesWritten, StandardFormat format)
		{
			if (format.IsDefault)
			{
				return Utf8Formatter.TryFormatInt64Default(value, destination, out bytesWritten);
			}
			char symbol = format.Symbol;
			if (symbol <= 'X')
			{
				if (symbol <= 'G')
				{
					if (symbol == 'D')
					{
						goto IL_83;
					}
					if (symbol != 'G')
					{
						goto IL_C9;
					}
				}
				else
				{
					if (symbol == 'N')
					{
						goto IL_93;
					}
					if (symbol != 'X')
					{
						goto IL_C9;
					}
					return Utf8Formatter.TryFormatUInt64X((ulong)(value & (long)mask), format.Precision, false, destination, out bytesWritten);
				}
			}
			else if (symbol <= 'g')
			{
				if (symbol == 'd')
				{
					goto IL_83;
				}
				if (symbol != 'g')
				{
					goto IL_C9;
				}
			}
			else
			{
				if (symbol == 'n')
				{
					goto IL_93;
				}
				if (symbol != 'x')
				{
					goto IL_C9;
				}
				return Utf8Formatter.TryFormatUInt64X((ulong)(value & (long)mask), format.Precision, true, destination, out bytesWritten);
			}
			if (format.HasPrecision)
			{
				throw new NotSupportedException(SR.Argument_GWithPrecisionNotSupported);
			}
			return Utf8Formatter.TryFormatInt64D(value, format.Precision, destination, out bytesWritten);
			IL_83:
			return Utf8Formatter.TryFormatInt64D(value, format.Precision, destination, out bytesWritten);
			IL_93:
			return Utf8Formatter.TryFormatInt64N(value, format.Precision, destination, out bytesWritten);
			IL_C9:
			return FormattingHelpers.TryFormatThrowFormatException(out bytesWritten);
		}

		// Token: 0x06002495 RID: 9365 RVA: 0x0013B574 File Offset: 0x0013A774
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryFormatInt64D(long value, byte precision, Span<byte> destination, out int bytesWritten)
		{
			bool insertNegationSign = false;
			if (value < 0L)
			{
				insertNegationSign = true;
				value = -value;
			}
			return Utf8Formatter.TryFormatUInt64D((ulong)value, precision, destination, insertNegationSign, out bytesWritten);
		}

		// Token: 0x06002496 RID: 9366 RVA: 0x0013B598 File Offset: 0x0013A798
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryFormatInt64Default(long value, Span<byte> destination, out int bytesWritten)
		{
			if (value < 10L)
			{
				return Utf8Formatter.TryFormatUInt32SingleDigit((uint)value, destination, out bytesWritten);
			}
			int size = IntPtr.Size;
			return Utf8Formatter.TryFormatInt64MultipleDigits(value, destination, out bytesWritten);
		}

		// Token: 0x06002497 RID: 9367 RVA: 0x0013B5B8 File Offset: 0x0013A7B8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool TryFormatInt64MultipleDigits(long value, Span<byte> destination, out int bytesWritten)
		{
			if (value >= 0L)
			{
				return Utf8Formatter.TryFormatUInt64MultipleDigits((ulong)value, destination, out bytesWritten);
			}
			value = -value;
			int num = FormattingHelpers.CountDigits((ulong)value);
			if (num >= destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			*destination[0] = 45;
			bytesWritten = num + 1;
			FormattingHelpers.WriteDigits((ulong)value, destination.Slice(1, num));
			return true;
		}

		// Token: 0x06002498 RID: 9368 RVA: 0x0013B610 File Offset: 0x0013A810
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryFormatInt64N(long value, byte precision, Span<byte> destination, out int bytesWritten)
		{
			bool insertNegationSign = false;
			if (value < 0L)
			{
				insertNegationSign = true;
				value = -value;
			}
			return Utf8Formatter.TryFormatUInt64N((ulong)value, precision, destination, insertNegationSign, out bytesWritten);
		}

		// Token: 0x06002499 RID: 9369 RVA: 0x0013B634 File Offset: 0x0013A834
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryFormatUInt64(ulong value, Span<byte> destination, out int bytesWritten, StandardFormat format)
		{
			if (format.IsDefault)
			{
				return Utf8Formatter.TryFormatUInt64Default(value, destination, out bytesWritten);
			}
			char symbol = format.Symbol;
			if (symbol <= 'X')
			{
				if (symbol <= 'G')
				{
					if (symbol == 'D')
					{
						goto IL_84;
					}
					if (symbol != 'G')
					{
						goto IL_C8;
					}
				}
				else
				{
					if (symbol == 'N')
					{
						goto IL_95;
					}
					if (symbol != 'X')
					{
						goto IL_C8;
					}
					return Utf8Formatter.TryFormatUInt64X(value, format.Precision, false, destination, out bytesWritten);
				}
			}
			else if (symbol <= 'g')
			{
				if (symbol == 'd')
				{
					goto IL_84;
				}
				if (symbol != 'g')
				{
					goto IL_C8;
				}
			}
			else
			{
				if (symbol == 'n')
				{
					goto IL_95;
				}
				if (symbol != 'x')
				{
					goto IL_C8;
				}
				return Utf8Formatter.TryFormatUInt64X(value, format.Precision, true, destination, out bytesWritten);
			}
			if (format.HasPrecision)
			{
				throw new NotSupportedException(SR.Argument_GWithPrecisionNotSupported);
			}
			return Utf8Formatter.TryFormatUInt64D(value, format.Precision, destination, false, out bytesWritten);
			IL_84:
			return Utf8Formatter.TryFormatUInt64D(value, format.Precision, destination, false, out bytesWritten);
			IL_95:
			return Utf8Formatter.TryFormatUInt64N(value, format.Precision, destination, false, out bytesWritten);
			IL_C8:
			return FormattingHelpers.TryFormatThrowFormatException(out bytesWritten);
		}

		// Token: 0x0600249A RID: 9370 RVA: 0x0013B710 File Offset: 0x0013A910
		private unsafe static bool TryFormatUInt64D(ulong value, byte precision, Span<byte> destination, bool insertNegationSign, out int bytesWritten)
		{
			int num = FormattingHelpers.CountDigits(value);
			int num2 = (int)((precision == byte.MaxValue) ? 0 : precision) - num;
			if (num2 < 0)
			{
				num2 = 0;
			}
			int num3 = num + num2;
			if (insertNegationSign)
			{
				num3++;
			}
			if (num3 > destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = num3;
			if (insertNegationSign)
			{
				*destination[0] = 45;
				destination = destination.Slice(1);
			}
			if (num2 > 0)
			{
				FormattingHelpers.FillWithAsciiZeros(destination.Slice(0, num2));
			}
			FormattingHelpers.WriteDigits(value, destination.Slice(num2, num));
			return true;
		}

		// Token: 0x0600249B RID: 9371 RVA: 0x0013B792 File Offset: 0x0013A992
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryFormatUInt64Default(ulong value, Span<byte> destination, out int bytesWritten)
		{
			if (value < 10UL)
			{
				return Utf8Formatter.TryFormatUInt32SingleDigit((uint)value, destination, out bytesWritten);
			}
			int size = IntPtr.Size;
			return Utf8Formatter.TryFormatUInt64MultipleDigits(value, destination, out bytesWritten);
		}

		// Token: 0x0600249C RID: 9372 RVA: 0x0013B7B2 File Offset: 0x0013A9B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static bool TryFormatUInt32SingleDigit(uint value, Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length == 0)
			{
				bytesWritten = 0;
				return false;
			}
			*destination[0] = (byte)(48U + value);
			bytesWritten = 1;
			return true;
		}

		// Token: 0x0600249D RID: 9373 RVA: 0x0013B7D4 File Offset: 0x0013A9D4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryFormatUInt64MultipleDigits(ulong value, Span<byte> destination, out int bytesWritten)
		{
			int num = FormattingHelpers.CountDigits(value);
			if (num > destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = num;
			FormattingHelpers.WriteDigits(value, destination.Slice(0, num));
			return true;
		}

		// Token: 0x0600249E RID: 9374 RVA: 0x0013B80C File Offset: 0x0013AA0C
		private unsafe static bool TryFormatUInt64N(ulong value, byte precision, Span<byte> destination, bool insertNegationSign, out int bytesWritten)
		{
			int num = FormattingHelpers.CountDigits(value);
			int num2 = (num - 1) / 3;
			int num3 = (int)((precision == byte.MaxValue) ? 2 : precision);
			int num4 = num + num2;
			if (num3 > 0)
			{
				num4 += num3 + 1;
			}
			if (insertNegationSign)
			{
				num4++;
			}
			if (num4 > destination.Length)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = num4;
			if (insertNegationSign)
			{
				*destination[0] = 45;
				destination = destination.Slice(1);
			}
			FormattingHelpers.WriteDigitsWithGroupSeparator(value, destination.Slice(0, num + num2));
			if (num3 > 0)
			{
				*destination[num + num2] = 46;
				FormattingHelpers.FillWithAsciiZeros(destination.Slice(num + num2 + 1, num3));
			}
			return true;
		}

		// Token: 0x0600249F RID: 9375 RVA: 0x0013B8AC File Offset: 0x0013AAAC
		private unsafe static bool TryFormatUInt64X(ulong value, byte precision, bool useLower, Span<byte> destination, out int bytesWritten)
		{
			int num = FormattingHelpers.CountHexDigits(value);
			int num2 = (precision == byte.MaxValue) ? num : Math.Max((int)precision, num);
			if (destination.Length < num2)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = num2;
			if (useLower)
			{
				while (--num2 < destination.Length)
				{
					*destination[num2] = (byte)HexConverter.ToCharLower((int)value);
					value >>= 4;
				}
			}
			else
			{
				while (--num2 < destination.Length)
				{
					*destination[num2] = (byte)HexConverter.ToCharUpper((int)value);
					value >>= 4;
				}
			}
			return true;
		}

		// Token: 0x060024A0 RID: 9376 RVA: 0x0013B938 File Offset: 0x0013AB38
		public unsafe static bool TryFormat(TimeSpan value, Span<byte> destination, out int bytesWritten, StandardFormat format = default(StandardFormat))
		{
			char c = FormattingHelpers.GetSymbolOrDefault(format, 'c');
			if (c <= 'T')
			{
				if (c == 'G')
				{
					goto IL_36;
				}
				if (c != 'T')
				{
					goto IL_2F;
				}
			}
			else
			{
				if (c == 'c' || c == 'g')
				{
					goto IL_36;
				}
				if (c != 't')
				{
					goto IL_2F;
				}
			}
			c = 'c';
			goto IL_36;
			IL_2F:
			return FormattingHelpers.TryFormatThrowFormatException(out bytesWritten);
			IL_36:
			int num = 8;
			long num2 = value.Ticks;
			uint num3;
			ulong num4;
			if (num2 < 0L)
			{
				num2 = -num2;
				if (num2 < 0L)
				{
					num3 = 4775808U;
					num4 = 922337203685UL;
					goto IL_82;
				}
			}
			ulong num5;
			num4 = FormattingHelpers.DivMod((ulong)Math.Abs(value.Ticks), 10000000UL, out num5);
			num3 = (uint)num5;
			IL_82:
			int num6 = 0;
			if (c == 'c')
			{
				if (num3 != 0U)
				{
					num6 = 7;
				}
			}
			else if (c == 'G')
			{
				num6 = 7;
			}
			else if (num3 != 0U)
			{
				num6 = 7 - FormattingHelpers.CountDecimalTrailingZeros(num3, out num3);
			}
			if (num6 != 0)
			{
				num += num6 + 1;
			}
			ulong num7 = 0UL;
			ulong num8 = 0UL;
			if (num4 > 0UL)
			{
				num7 = FormattingHelpers.DivMod(num4, 60UL, out num8);
			}
			ulong num9 = 0UL;
			ulong num10 = 0UL;
			if (num7 > 0UL)
			{
				num9 = FormattingHelpers.DivMod(num7, 60UL, out num10);
			}
			uint num11 = 0U;
			uint num12 = 0U;
			if (num9 > 0UL)
			{
				num11 = FormattingHelpers.DivMod((uint)num9, 24U, out num12);
			}
			int num13 = 2;
			if (num12 < 10U && c == 'g')
			{
				num13--;
				num--;
			}
			int num14 = 0;
			if (num11 == 0U)
			{
				if (c == 'G')
				{
					num += 2;
					num14 = 1;
				}
			}
			else
			{
				num14 = FormattingHelpers.CountDigits(num11);
				num += num14 + 1;
			}
			if (value.Ticks < 0L)
			{
				num++;
			}
			if (destination.Length < num)
			{
				bytesWritten = 0;
				return false;
			}
			bytesWritten = num;
			int num15 = 0;
			if (value.Ticks < 0L)
			{
				*destination[num15++] = 45;
			}
			if (num14 > 0)
			{
				FormattingHelpers.WriteDigits(num11, destination.Slice(num15, num14));
				num15 += num14;
				*destination[num15++] = ((c == 'c') ? 46 : 58);
			}
			FormattingHelpers.WriteDigits(num12, destination.Slice(num15, num13));
			num15 += num13;
			*destination[num15++] = 58;
			FormattingHelpers.WriteDigits((uint)num10, destination.Slice(num15, 2));
			num15 += 2;
			*destination[num15++] = 58;
			FormattingHelpers.WriteDigits((uint)num8, destination.Slice(num15, 2));
			num15 += 2;
			if (num6 > 0)
			{
				*destination[num15++] = 46;
				FormattingHelpers.WriteDigits(num3, destination.Slice(num15, num6));
				num15 += num6;
			}
			return true;
		}

		// Token: 0x04000998 RID: 2456
		private static readonly uint[] s_dayAbbreviations = new uint[]
		{
			7238995U,
			7237453U,
			6649172U,
			6579543U,
			7694420U,
			6910534U,
			7627091U
		};

		// Token: 0x04000999 RID: 2457
		private static readonly uint[] s_dayAbbreviationsLowercase = new uint[]
		{
			7239027U,
			7237485U,
			6649204U,
			6579575U,
			7694452U,
			6910566U,
			7627123U
		};

		// Token: 0x0400099A RID: 2458
		private static readonly uint[] s_monthAbbreviations = new uint[]
		{
			7233866U,
			6448454U,
			7496013U,
			7499841U,
			7954765U,
			7238986U,
			7107914U,
			6780225U,
			7365971U,
			7627599U,
			7761742U,
			6513988U
		};

		// Token: 0x0400099B RID: 2459
		private static readonly uint[] s_monthAbbreviationsLowercase = new uint[]
		{
			7233898U,
			6448486U,
			7496045U,
			7499873U,
			7954797U,
			7239018U,
			7107946U,
			6780257U,
			7366003U,
			7627631U,
			7761774U,
			6514020U
		};

		// Token: 0x02000259 RID: 601
		[StructLayout(LayoutKind.Explicit)]
		private struct DecomposedGuid
		{
			// Token: 0x0400099C RID: 2460
			[FieldOffset(0)]
			public Guid Guid;

			// Token: 0x0400099D RID: 2461
			[FieldOffset(0)]
			public byte Byte00;

			// Token: 0x0400099E RID: 2462
			[FieldOffset(1)]
			public byte Byte01;

			// Token: 0x0400099F RID: 2463
			[FieldOffset(2)]
			public byte Byte02;

			// Token: 0x040009A0 RID: 2464
			[FieldOffset(3)]
			public byte Byte03;

			// Token: 0x040009A1 RID: 2465
			[FieldOffset(4)]
			public byte Byte04;

			// Token: 0x040009A2 RID: 2466
			[FieldOffset(5)]
			public byte Byte05;

			// Token: 0x040009A3 RID: 2467
			[FieldOffset(6)]
			public byte Byte06;

			// Token: 0x040009A4 RID: 2468
			[FieldOffset(7)]
			public byte Byte07;

			// Token: 0x040009A5 RID: 2469
			[FieldOffset(8)]
			public byte Byte08;

			// Token: 0x040009A6 RID: 2470
			[FieldOffset(9)]
			public byte Byte09;

			// Token: 0x040009A7 RID: 2471
			[FieldOffset(10)]
			public byte Byte10;

			// Token: 0x040009A8 RID: 2472
			[FieldOffset(11)]
			public byte Byte11;

			// Token: 0x040009A9 RID: 2473
			[FieldOffset(12)]
			public byte Byte12;

			// Token: 0x040009AA RID: 2474
			[FieldOffset(13)]
			public byte Byte13;

			// Token: 0x040009AB RID: 2475
			[FieldOffset(14)]
			public byte Byte14;

			// Token: 0x040009AC RID: 2476
			[FieldOffset(15)]
			public byte Byte15;
		}
	}
}
