using System;
using System.Buffers.Text;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200015B RID: 347
	internal static class Number
	{
		// Token: 0x0600115D RID: 4445 RVA: 0x000DF11C File Offset: 0x000DE31C
		public unsafe static void Dragon4Double(double value, int cutoffNumber, bool isSignificantDigits, ref Number.NumberBuffer number)
		{
			double num = double.IsNegative(value) ? (-value) : value;
			int exponent;
			ulong num2 = Number.ExtractFractionAndBiasedExponent(value, out exponent);
			bool hasUnequalMargins = false;
			uint mantissaHighBitIdx;
			if (num2 >> 52 != 0UL)
			{
				mantissaHighBitIdx = 52U;
				hasUnequalMargins = (num2 == 4503599627370496UL);
			}
			else
			{
				mantissaHighBitIdx = (uint)BitOperations.Log2(num2);
			}
			int num4;
			int num3 = (int)Number.Dragon4(num2, exponent, mantissaHighBitIdx, hasUnequalMargins, cutoffNumber, isSignificantDigits, number.Digits, out num4);
			number.Scale = num4 + 1;
			*number.Digits[num3] = 0;
			number.DigitsCount = num3;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x000DF19C File Offset: 0x000DE39C
		public unsafe static void Dragon4Half(Half value, int cutoffNumber, bool isSignificantDigits, ref Number.NumberBuffer number)
		{
			Half half = Half.IsNegative(value) ? Half.Negate(value) : value;
			int exponent;
			ushort num = Number.ExtractFractionAndBiasedExponent(value, out exponent);
			bool hasUnequalMargins = false;
			uint mantissaHighBitIdx;
			if (num >> 10 != 0)
			{
				mantissaHighBitIdx = 10U;
				hasUnequalMargins = (num == 1024);
			}
			else
			{
				mantissaHighBitIdx = (uint)BitOperations.Log2((uint)num);
			}
			int num3;
			int num2 = (int)Number.Dragon4((ulong)num, exponent, mantissaHighBitIdx, hasUnequalMargins, cutoffNumber, isSignificantDigits, number.Digits, out num3);
			number.Scale = num3 + 1;
			*number.Digits[num2] = 0;
			number.DigitsCount = num2;
		}

		// Token: 0x0600115F RID: 4447 RVA: 0x000DF21C File Offset: 0x000DE41C
		public unsafe static void Dragon4Single(float value, int cutoffNumber, bool isSignificantDigits, ref Number.NumberBuffer number)
		{
			float num = float.IsNegative(value) ? (-value) : value;
			int exponent;
			uint num2 = Number.ExtractFractionAndBiasedExponent(value, out exponent);
			bool hasUnequalMargins = false;
			uint mantissaHighBitIdx;
			if (num2 >> 23 != 0U)
			{
				mantissaHighBitIdx = 23U;
				hasUnequalMargins = (num2 == 8388608U);
			}
			else
			{
				mantissaHighBitIdx = (uint)BitOperations.Log2(num2);
			}
			int num4;
			int num3 = (int)Number.Dragon4((ulong)num2, exponent, mantissaHighBitIdx, hasUnequalMargins, cutoffNumber, isSignificantDigits, number.Digits, out num4);
			number.Scale = num4 + 1;
			*number.Digits[num3] = 0;
			number.DigitsCount = num3;
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x000DF298 File Offset: 0x000DE498
		private unsafe static uint Dragon4(ulong mantissa, int exponent, uint mantissaHighBitIdx, bool hasUnequalMargins, int cutoffNumber, bool isSignificantDigits, Span<byte> buffer, out int decimalExponent)
		{
			int num = 0;
			Number.BigInteger bigInteger;
			Number.BigInteger bigInteger2;
			Number.BigInteger bigInteger3;
			Number.BigInteger* ptr;
			if (hasUnequalMargins)
			{
				Number.BigInteger bigInteger4;
				if (exponent > 0)
				{
					Number.BigInteger.SetUInt64(out bigInteger, 4UL * mantissa);
					bigInteger.ShiftLeft((uint)exponent);
					Number.BigInteger.SetUInt32(out bigInteger2, 4U);
					Number.BigInteger.Pow2((uint)exponent, out bigInteger3);
					Number.BigInteger.Pow2((uint)(exponent + 1), out bigInteger4);
				}
				else
				{
					Number.BigInteger.SetUInt64(out bigInteger, 4UL * mantissa);
					Number.BigInteger.Pow2((uint)(-exponent + 2), out bigInteger2);
					Number.BigInteger.SetUInt32(out bigInteger3, 1U);
					Number.BigInteger.SetUInt32(out bigInteger4, 2U);
				}
				ptr = &bigInteger4;
			}
			else
			{
				if (exponent > 0)
				{
					Number.BigInteger.SetUInt64(out bigInteger, 2UL * mantissa);
					bigInteger.ShiftLeft((uint)exponent);
					Number.BigInteger.SetUInt32(out bigInteger2, 2U);
					Number.BigInteger.Pow2((uint)exponent, out bigInteger3);
				}
				else
				{
					Number.BigInteger.SetUInt64(out bigInteger, 2UL * mantissa);
					Number.BigInteger.Pow2((uint)(-exponent + 1), out bigInteger2);
					Number.BigInteger.SetUInt32(out bigInteger3, 1U);
				}
				ptr = &bigInteger3;
			}
			int num2 = (int)Math.Ceiling((double)(mantissaHighBitIdx + (uint)exponent) * 0.3010299956639812 - 0.69);
			if (num2 > 0)
			{
				bigInteger2.MultiplyPow10((uint)num2);
			}
			else if (num2 < 0)
			{
				Number.BigInteger bigInteger5;
				Number.BigInteger.Pow10((uint)(-(uint)num2), out bigInteger5);
				bigInteger.Multiply(ref bigInteger5);
				bigInteger3.Multiply(ref bigInteger5);
				if (ptr != &bigInteger3)
				{
					Number.BigInteger.Multiply(ref bigInteger3, 2U, out *ptr);
				}
			}
			bool flag = mantissa % 2UL == 0UL;
			bool flag2;
			if (cutoffNumber == -1)
			{
				Number.BigInteger bigInteger6;
				Number.BigInteger.Add(ref bigInteger, ref *ptr, out bigInteger6);
				int num3 = Number.BigInteger.Compare(ref bigInteger6, ref bigInteger2);
				flag2 = (flag ? (num3 >= 0) : (num3 > 0));
			}
			else
			{
				flag2 = (Number.BigInteger.Compare(ref bigInteger, ref bigInteger2) >= 0);
			}
			if (flag2)
			{
				num2++;
			}
			else
			{
				bigInteger.Multiply10();
				bigInteger3.Multiply10();
				if (ptr != &bigInteger3)
				{
					Number.BigInteger.Multiply(ref bigInteger3, 2U, out *ptr);
				}
			}
			int num4 = num2 - buffer.Length;
			if (cutoffNumber != -1)
			{
				int num5;
				if (isSignificantDigits)
				{
					num5 = num2 - cutoffNumber;
				}
				else
				{
					num5 = -cutoffNumber;
				}
				if (num5 > num4)
				{
					num4 = num5;
				}
			}
			num2 = (decimalExponent = num2 - 1);
			uint block = bigInteger2.GetBlock((uint)(bigInteger2.GetLength() - 1));
			if (block < 8U || block > 429496729U)
			{
				uint num6 = (uint)BitOperations.Log2(block);
				uint shift = (59U - num6) % 32U;
				bigInteger2.ShiftLeft(shift);
				bigInteger.ShiftLeft(shift);
				bigInteger3.ShiftLeft(shift);
				if (ptr != &bigInteger3)
				{
					Number.BigInteger.Multiply(ref bigInteger3, 2U, out *ptr);
				}
			}
			uint num7;
			bool flag3;
			bool flag4;
			if (cutoffNumber == -1)
			{
				for (;;)
				{
					num7 = Number.BigInteger.HeuristicDivide(ref bigInteger, ref bigInteger2);
					Number.BigInteger bigInteger7;
					Number.BigInteger.Add(ref bigInteger, ref *ptr, out bigInteger7);
					int num8 = Number.BigInteger.Compare(ref bigInteger, ref bigInteger3);
					int num9 = Number.BigInteger.Compare(ref bigInteger7, ref bigInteger2);
					if (flag)
					{
						flag3 = (num8 <= 0);
						flag4 = (num9 >= 0);
					}
					else
					{
						flag3 = (num8 < 0);
						flag4 = (num9 > 0);
					}
					if (flag3 || flag4 || num2 == num4)
					{
						break;
					}
					*buffer[num] = (byte)(48U + num7);
					num++;
					bigInteger.Multiply10();
					bigInteger3.Multiply10();
					if (ptr != &bigInteger3)
					{
						Number.BigInteger.Multiply(ref bigInteger3, 2U, out *ptr);
					}
					num2--;
				}
			}
			else
			{
				if (num2 < num4)
				{
					num7 = Number.BigInteger.HeuristicDivide(ref bigInteger, ref bigInteger2);
					if (num7 > 5U || (num7 == 5U && !bigInteger.IsZero()))
					{
						decimalExponent++;
						num7 = 1U;
					}
					*buffer[num] = (byte)(48U + num7);
					return (uint)(num + 1);
				}
				flag3 = false;
				flag4 = false;
				for (;;)
				{
					num7 = Number.BigInteger.HeuristicDivide(ref bigInteger, ref bigInteger2);
					if (bigInteger.IsZero() || num2 <= num4)
					{
						break;
					}
					*buffer[num] = (byte)(48U + num7);
					num++;
					bigInteger.Multiply10();
					num2--;
				}
			}
			bool flag5 = flag3;
			if (flag3 == flag4)
			{
				bigInteger.ShiftLeft(1U);
				int num10 = Number.BigInteger.Compare(ref bigInteger, ref bigInteger2);
				flag5 = (num10 < 0);
				if (num10 == 0)
				{
					flag5 = ((num7 & 1U) == 0U);
				}
			}
			if (flag5)
			{
				*buffer[num] = (byte)(48U + num7);
				num++;
			}
			else if (num7 == 9U)
			{
				while (num != 0)
				{
					num--;
					if (*buffer[num] != 57)
					{
						ref byte ptr2 = ref buffer[num];
						ptr2 += 1;
						return (uint)(num + 1);
					}
				}
				*buffer[num] = 49;
				num++;
				decimalExponent++;
			}
			else
			{
				*buffer[num] = (byte)(48U + num7 + 1U);
				num++;
			}
			return (uint)num;
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x000DF6AC File Offset: 0x000DE8AC
		public unsafe static string FormatDecimal(decimal value, ReadOnlySpan<char> format, NumberFormatInfo info)
		{
			int nMaxDigits;
			char c = Number.ParseFormatSpecifier(format, out nMaxDigits);
			byte* digits = stackalloc byte[(UIntPtr)31];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Decimal, digits, 31);
			Number.DecimalToNumber(ref value, ref numberBuffer);
			char* pointer = stackalloc char[(UIntPtr)64];
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
			if (c != '\0')
			{
				Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, nMaxDigits, info);
			}
			else
			{
				Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format, info);
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x06001162 RID: 4450 RVA: 0x000DF71C File Offset: 0x000DE91C
		public unsafe static bool TryFormatDecimal(decimal value, ReadOnlySpan<char> format, NumberFormatInfo info, Span<char> destination, out int charsWritten)
		{
			int nMaxDigits;
			char c = Number.ParseFormatSpecifier(format, out nMaxDigits);
			byte* digits = stackalloc byte[(UIntPtr)31];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Decimal, digits, 31);
			Number.DecimalToNumber(ref value, ref numberBuffer);
			char* pointer = stackalloc char[(UIntPtr)64];
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
			if (c != '\0')
			{
				Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, nMaxDigits, info);
			}
			else
			{
				Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format, info);
			}
			return valueStringBuilder.TryCopyTo(destination, out charsWritten);
		}

		// Token: 0x06001163 RID: 4451 RVA: 0x000DF78C File Offset: 0x000DE98C
		internal unsafe static void DecimalToNumber(ref decimal d, ref Number.NumberBuffer number)
		{
			byte* digitsPointer = number.GetDigitsPointer();
			number.DigitsCount = 29;
			number.IsNegative = d.IsNegative;
			byte* ptr = digitsPointer + 29;
			while ((d.Mid | d.High) != 0U)
			{
				ptr = Number.UInt32ToDecChars(ptr, decimal.DecDivMod1E9(ref d), 9);
			}
			ptr = Number.UInt32ToDecChars(ptr, d.Low, 0);
			int num = (int)((long)(digitsPointer + 29 - ptr));
			number.DigitsCount = num;
			number.Scale = num - d.Scale;
			byte* digitsPointer2 = number.GetDigitsPointer();
			while (--num >= 0)
			{
				*(digitsPointer2++) = *(ptr++);
			}
			*digitsPointer2 = 0;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000DF828 File Offset: 0x000DEA28
		public unsafe static string FormatDouble(double value, string format, NumberFormatInfo info)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)64], 32);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			return Number.FormatDouble(ref valueStringBuilder, value, format, info) ?? valueStringBuilder.ToString();
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000DF86C File Offset: 0x000DEA6C
		public unsafe static bool TryFormatDouble(double value, ReadOnlySpan<char> format, NumberFormatInfo info, Span<char> destination, out int charsWritten)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)64], 32);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			string text = Number.FormatDouble(ref valueStringBuilder, value, format, info);
			if (text == null)
			{
				return valueStringBuilder.TryCopyTo(destination, out charsWritten);
			}
			return Number.TryCopyTo(text, destination, out charsWritten);
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x000DF8B0 File Offset: 0x000DEAB0
		private static int GetFloatingPointMaxDigitsAndPrecision(char fmt, ref int precision, NumberFormatInfo info, out bool isSignificantDigits)
		{
			if (fmt == '\0')
			{
				isSignificantDigits = true;
				return precision;
			}
			int result = precision;
			if (fmt <= 'R')
			{
				switch (fmt)
				{
				case 'C':
					break;
				case 'D':
					goto IL_EF;
				case 'E':
					goto IL_9E;
				case 'F':
					goto IL_B1;
				case 'G':
					goto IL_C3;
				default:
					switch (fmt)
					{
					case 'N':
						goto IL_B1;
					case 'O':
					case 'Q':
						goto IL_EF;
					case 'P':
						goto IL_CF;
					case 'R':
						goto IL_E7;
					default:
						goto IL_EF;
					}
					break;
				}
			}
			else
			{
				switch (fmt)
				{
				case 'c':
					break;
				case 'd':
					goto IL_EF;
				case 'e':
					goto IL_9E;
				case 'f':
					goto IL_B1;
				case 'g':
					goto IL_C3;
				default:
					switch (fmt)
					{
					case 'n':
						goto IL_B1;
					case 'o':
					case 'q':
						goto IL_EF;
					case 'p':
						goto IL_CF;
					case 'r':
						goto IL_E7;
					default:
						goto IL_EF;
					}
					break;
				}
			}
			if (precision == -1)
			{
				precision = info.CurrencyDecimalDigits;
			}
			isSignificantDigits = false;
			return result;
			IL_9E:
			if (precision == -1)
			{
				precision = 6;
			}
			precision++;
			isSignificantDigits = true;
			return result;
			IL_B1:
			if (precision == -1)
			{
				precision = info.NumberDecimalDigits;
			}
			isSignificantDigits = false;
			return result;
			IL_C3:
			if (precision == 0)
			{
				precision = -1;
			}
			isSignificantDigits = true;
			return result;
			IL_CF:
			if (precision == -1)
			{
				precision = info.PercentDecimalDigits;
			}
			precision += 2;
			isSignificantDigits = false;
			return result;
			IL_E7:
			precision = -1;
			isSignificantDigits = true;
			return result;
			IL_EF:
			throw new FormatException(SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x06001167 RID: 4455 RVA: 0x000DF9B8 File Offset: 0x000DEBB8
		private unsafe static string FormatDouble(ref ValueStringBuilder sb, double value, ReadOnlySpan<char> format, NumberFormatInfo info)
		{
			if (double.IsFinite(value))
			{
				int num;
				char c = Number.ParseFormatSpecifier(format, out num);
				byte* digits = stackalloc byte[(UIntPtr)769];
				if (c == '\0')
				{
					num = 15;
				}
				Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.FloatingPoint, digits, 769);
				numberBuffer.IsNegative = double.IsNegative(value);
				bool flag;
				int nMaxDigits = Number.GetFloatingPointMaxDigitsAndPrecision(c, ref num, info, out flag);
				if (value != 0.0 && (!flag || !Number.Grisu3.TryRunDouble(value, num, ref numberBuffer)))
				{
					Number.Dragon4Double(value, num, flag, ref numberBuffer);
				}
				if (c != '\0')
				{
					if (num == -1)
					{
						nMaxDigits = Math.Max(numberBuffer.DigitsCount, 17);
					}
					Number.NumberToString(ref sb, ref numberBuffer, c, nMaxDigits, info);
				}
				else
				{
					Number.NumberToStringFormat(ref sb, ref numberBuffer, format, info);
				}
				return null;
			}
			if (double.IsNaN(value))
			{
				return info.NaNSymbol;
			}
			if (!double.IsNegative(value))
			{
				return info.PositiveInfinitySymbol;
			}
			return info.NegativeInfinitySymbol;
		}

		// Token: 0x06001168 RID: 4456 RVA: 0x000DFA88 File Offset: 0x000DEC88
		public unsafe static string FormatSingle(float value, string format, NumberFormatInfo info)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)64], 32);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			return Number.FormatSingle(ref valueStringBuilder, value, format, info) ?? valueStringBuilder.ToString();
		}

		// Token: 0x06001169 RID: 4457 RVA: 0x000DFACC File Offset: 0x000DECCC
		public unsafe static bool TryFormatSingle(float value, ReadOnlySpan<char> format, NumberFormatInfo info, Span<char> destination, out int charsWritten)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)64], 32);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			string text = Number.FormatSingle(ref valueStringBuilder, value, format, info);
			if (text == null)
			{
				return valueStringBuilder.TryCopyTo(destination, out charsWritten);
			}
			return Number.TryCopyTo(text, destination, out charsWritten);
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x000DFB10 File Offset: 0x000DED10
		private unsafe static string FormatSingle(ref ValueStringBuilder sb, float value, ReadOnlySpan<char> format, NumberFormatInfo info)
		{
			if (float.IsFinite(value))
			{
				int num;
				char c = Number.ParseFormatSpecifier(format, out num);
				byte* digits = stackalloc byte[(UIntPtr)114];
				if (c == '\0')
				{
					num = 7;
				}
				Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.FloatingPoint, digits, 114);
				numberBuffer.IsNegative = float.IsNegative(value);
				bool flag;
				int nMaxDigits = Number.GetFloatingPointMaxDigitsAndPrecision(c, ref num, info, out flag);
				if (value != 0f && (!flag || !Number.Grisu3.TryRunSingle(value, num, ref numberBuffer)))
				{
					Number.Dragon4Single(value, num, flag, ref numberBuffer);
				}
				if (c != '\0')
				{
					if (num == -1)
					{
						nMaxDigits = Math.Max(numberBuffer.DigitsCount, 9);
					}
					Number.NumberToString(ref sb, ref numberBuffer, c, nMaxDigits, info);
				}
				else
				{
					Number.NumberToStringFormat(ref sb, ref numberBuffer, format, info);
				}
				return null;
			}
			if (float.IsNaN(value))
			{
				return info.NaNSymbol;
			}
			if (!float.IsNegative(value))
			{
				return info.PositiveInfinitySymbol;
			}
			return info.NegativeInfinitySymbol;
		}

		// Token: 0x0600116B RID: 4459 RVA: 0x000DFBD4 File Offset: 0x000DEDD4
		public unsafe static string FormatHalf(Half value, string format, NumberFormatInfo info)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)64], 32);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			return Number.FormatHalf(ref valueStringBuilder, value, format, info) ?? valueStringBuilder.ToString();
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000DFC18 File Offset: 0x000DEE18
		private unsafe static string FormatHalf(ref ValueStringBuilder sb, Half value, ReadOnlySpan<char> format, NumberFormatInfo info)
		{
			if (Half.IsFinite(value))
			{
				int num;
				char c = Number.ParseFormatSpecifier(format, out num);
				byte* digits = stackalloc byte[(UIntPtr)21];
				if (c == '\0')
				{
					num = 5;
				}
				Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.FloatingPoint, digits, 21);
				numberBuffer.IsNegative = Half.IsNegative(value);
				bool flag;
				int nMaxDigits = Number.GetFloatingPointMaxDigitsAndPrecision(c, ref num, info, out flag);
				if (value != default(Half) && (!flag || !Number.Grisu3.TryRunHalf(value, num, ref numberBuffer)))
				{
					Number.Dragon4Half(value, num, flag, ref numberBuffer);
				}
				if (c != '\0')
				{
					if (num == -1)
					{
						nMaxDigits = Math.Max(numberBuffer.DigitsCount, 5);
					}
					Number.NumberToString(ref sb, ref numberBuffer, c, nMaxDigits, info);
				}
				else
				{
					Number.NumberToStringFormat(ref sb, ref numberBuffer, format, info);
				}
				return null;
			}
			if (Half.IsNaN(value))
			{
				return info.NaNSymbol;
			}
			if (!Half.IsNegative(value))
			{
				return info.PositiveInfinitySymbol;
			}
			return info.NegativeInfinitySymbol;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000DFCE8 File Offset: 0x000DEEE8
		public unsafe static bool TryFormatHalf(Half value, ReadOnlySpan<char> format, NumberFormatInfo info, Span<char> destination, out int charsWritten)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)64], 32);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			string text = Number.FormatHalf(ref valueStringBuilder, value, format, info);
			if (text == null)
			{
				return valueStringBuilder.TryCopyTo(destination, out charsWritten);
			}
			return Number.TryCopyTo(text, destination, out charsWritten);
		}

		// Token: 0x0600116E RID: 4462 RVA: 0x000DFD2C File Offset: 0x000DEF2C
		private static bool TryCopyTo(string source, Span<char> destination, out int charsWritten)
		{
			if (source.AsSpan().TryCopyTo(destination))
			{
				charsWritten = source.Length;
				return true;
			}
			charsWritten = 0;
			return false;
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x000DFD58 File Offset: 0x000DEF58
		private static char GetHexBase(char fmt)
		{
			return fmt - '!';
		}

		// Token: 0x06001170 RID: 4464 RVA: 0x000DFD5F File Offset: 0x000DEF5F
		public static string FormatInt32(int value, int hexMask, string format, IFormatProvider provider)
		{
			if (!string.IsNullOrEmpty(format))
			{
				return Number.<FormatInt32>g__FormatInt32Slow|38_0(value, hexMask, format, provider);
			}
			if (value < 0)
			{
				return Number.NegativeInt32ToDecStr(value, -1, NumberFormatInfo.GetInstance(provider).NegativeSign);
			}
			return Number.UInt32ToDecStr((uint)value);
		}

		// Token: 0x06001171 RID: 4465 RVA: 0x000DFD90 File Offset: 0x000DEF90
		public static bool TryFormatInt32(int value, int hexMask, ReadOnlySpan<char> format, IFormatProvider provider, Span<char> destination, out int charsWritten)
		{
			if (format.Length != 0)
			{
				return Number.<TryFormatInt32>g__TryFormatInt32Slow|39_0(value, hexMask, format, provider, destination, out charsWritten);
			}
			if (value < 0)
			{
				return Number.TryNegativeInt32ToDecStr(value, -1, NumberFormatInfo.GetInstance(provider).NegativeSign, destination, out charsWritten);
			}
			return Number.TryUInt32ToDecStr((uint)value, -1, destination, out charsWritten);
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000DFDCF File Offset: 0x000DEFCF
		public static string FormatUInt32(uint value, string format, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(format))
			{
				return Number.UInt32ToDecStr(value);
			}
			return Number.<FormatUInt32>g__FormatUInt32Slow|40_0(value, format, provider);
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x000DFDE8 File Offset: 0x000DEFE8
		public static bool TryFormatUInt32(uint value, ReadOnlySpan<char> format, IFormatProvider provider, Span<char> destination, out int charsWritten)
		{
			if (format.Length == 0)
			{
				return Number.TryUInt32ToDecStr(value, -1, destination, out charsWritten);
			}
			return Number.<TryFormatUInt32>g__TryFormatUInt32Slow|41_0(value, format, provider, destination, out charsWritten);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x000DFE09 File Offset: 0x000DF009
		public static string FormatInt64(long value, string format, IFormatProvider provider)
		{
			if (!string.IsNullOrEmpty(format))
			{
				return Number.<FormatInt64>g__FormatInt64Slow|42_0(value, format, provider);
			}
			if (value < 0L)
			{
				return Number.NegativeInt64ToDecStr(value, -1, NumberFormatInfo.GetInstance(provider).NegativeSign);
			}
			return Number.UInt64ToDecStr((ulong)value, -1);
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x000DFE3B File Offset: 0x000DF03B
		public static bool TryFormatInt64(long value, ReadOnlySpan<char> format, IFormatProvider provider, Span<char> destination, out int charsWritten)
		{
			if (format.Length != 0)
			{
				return Number.<TryFormatInt64>g__TryFormatInt64Slow|43_0(value, format, provider, destination, out charsWritten);
			}
			if (value < 0L)
			{
				return Number.TryNegativeInt64ToDecStr(value, -1, NumberFormatInfo.GetInstance(provider).NegativeSign, destination, out charsWritten);
			}
			return Number.TryUInt64ToDecStr((ulong)value, -1, destination, out charsWritten);
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x000DFE77 File Offset: 0x000DF077
		public static string FormatUInt64(ulong value, string format, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(format))
			{
				return Number.UInt64ToDecStr(value, -1);
			}
			return Number.<FormatUInt64>g__FormatUInt64Slow|44_0(value, format, provider);
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x000DFE91 File Offset: 0x000DF091
		public static bool TryFormatUInt64(ulong value, ReadOnlySpan<char> format, IFormatProvider provider, Span<char> destination, out int charsWritten)
		{
			if (format.Length == 0)
			{
				return Number.TryUInt64ToDecStr(value, -1, destination, out charsWritten);
			}
			return Number.<TryFormatUInt64>g__TryFormatUInt64Slow|45_0(value, format, provider, destination, out charsWritten);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x000DFEB4 File Offset: 0x000DF0B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void Int32ToNumber(int value, ref Number.NumberBuffer number)
		{
			number.DigitsCount = 10;
			if (value >= 0)
			{
				number.IsNegative = false;
			}
			else
			{
				number.IsNegative = true;
				value = -value;
			}
			byte* digitsPointer = number.GetDigitsPointer();
			byte* ptr = Number.UInt32ToDecChars(digitsPointer + 10, (uint)value, 0);
			int num = (int)((long)(digitsPointer + 10 - ptr));
			number.DigitsCount = num;
			number.Scale = num;
			byte* digitsPointer2 = number.GetDigitsPointer();
			while (--num >= 0)
			{
				*(digitsPointer2++) = *(ptr++);
			}
			*digitsPointer2 = 0;
		}

		// Token: 0x06001179 RID: 4473 RVA: 0x000DFF2D File Offset: 0x000DF12D
		public static string Int32ToDecStr(int value)
		{
			if (value < 0)
			{
				return Number.NegativeInt32ToDecStr(value, -1, NumberFormatInfo.CurrentInfo.NegativeSign);
			}
			return Number.UInt32ToDecStr((uint)value);
		}

		// Token: 0x0600117A RID: 4474 RVA: 0x000DFF4C File Offset: 0x000DF14C
		private unsafe static string NegativeInt32ToDecStr(int value, int digits, string sNegative)
		{
			if (digits < 1)
			{
				digits = 1;
			}
			int num = Math.Max(digits, FormattingHelpers.CountDigits((uint)(-(uint)value))) + sNegative.Length;
			string text = string.FastAllocateString(num);
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = text.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			char* ptr4 = Number.UInt32ToDecChars(ptr3 + num, (uint)(-(uint)value), digits);
			for (int i = sNegative.Length - 1; i >= 0; i--)
			{
				*(--ptr4) = sNegative[i];
			}
			char* ptr2 = null;
			return text;
		}

		// Token: 0x0600117B RID: 4475 RVA: 0x000DFFC8 File Offset: 0x000DF1C8
		private unsafe static bool TryNegativeInt32ToDecStr(int value, int digits, string sNegative, Span<char> destination, out int charsWritten)
		{
			if (digits < 1)
			{
				digits = 1;
			}
			int num = Math.Max(digits, FormattingHelpers.CountDigits((uint)(-(uint)value))) + sNegative.Length;
			if (num > destination.Length)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				char* ptr2 = Number.UInt32ToDecChars(ptr + num, (uint)(-(uint)value), digits);
				for (int i = sNegative.Length - 1; i >= 0; i--)
				{
					*(--ptr2) = sNegative[i];
				}
			}
			return true;
		}

		// Token: 0x0600117C RID: 4476 RVA: 0x000E0048 File Offset: 0x000DF248
		private unsafe static string Int32ToHexStr(int value, char hexBase, int digits)
		{
			if (digits < 1)
			{
				digits = 1;
			}
			int num = Math.Max(digits, FormattingHelpers.CountHexDigits((ulong)value));
			string text = string.FastAllocateString(num);
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = text.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			char* ptr4 = Number.Int32ToHexChars(ptr3 + num, (uint)value, (int)hexBase, digits);
			char* ptr2 = null;
			return text;
		}

		// Token: 0x0600117D RID: 4477 RVA: 0x000E0098 File Offset: 0x000DF298
		private unsafe static bool TryInt32ToHexStr(int value, char hexBase, int digits, Span<char> destination, out int charsWritten)
		{
			if (digits < 1)
			{
				digits = 1;
			}
			int num = Math.Max(digits, FormattingHelpers.CountHexDigits((ulong)value));
			if (num > destination.Length)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				char* ptr2 = Number.Int32ToHexChars(ptr + num, (uint)value, (int)hexBase, digits);
			}
			return true;
		}

		// Token: 0x0600117E RID: 4478 RVA: 0x000E00EC File Offset: 0x000DF2EC
		private unsafe static char* Int32ToHexChars(char* buffer, uint value, int hexBase, int digits)
		{
			while (--digits >= 0 || value != 0U)
			{
				byte b = (byte)(value & 15U);
				*(--buffer) = (char)((int)b + ((b < 10) ? 48 : hexBase));
				value >>= 4;
			}
			return buffer;
		}

		// Token: 0x0600117F RID: 4479 RVA: 0x000E0128 File Offset: 0x000DF328
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void UInt32ToNumber(uint value, ref Number.NumberBuffer number)
		{
			number.DigitsCount = 10;
			number.IsNegative = false;
			byte* digitsPointer = number.GetDigitsPointer();
			byte* ptr = Number.UInt32ToDecChars(digitsPointer + 10, value, 0);
			int num = (int)((long)(digitsPointer + 10 - ptr));
			number.DigitsCount = num;
			number.Scale = num;
			byte* digitsPointer2 = number.GetDigitsPointer();
			while (--num >= 0)
			{
				*(digitsPointer2++) = *(ptr++);
			}
			*digitsPointer2 = 0;
		}

		// Token: 0x06001180 RID: 4480 RVA: 0x000E0190 File Offset: 0x000DF390
		internal unsafe static byte* UInt32ToDecChars(byte* bufferEnd, uint value, int digits)
		{
			while (--digits >= 0 || value != 0U)
			{
				uint num;
				value = Math.DivRem(value, 10U, out num);
				*(--bufferEnd) = (byte)(num + 48U);
			}
			return bufferEnd;
		}

		// Token: 0x06001181 RID: 4481 RVA: 0x000E01C4 File Offset: 0x000DF3C4
		internal unsafe static char* UInt32ToDecChars(char* bufferEnd, uint value, int digits)
		{
			while (--digits >= 0 || value != 0U)
			{
				uint num;
				value = Math.DivRem(value, 10U, out num);
				*(--bufferEnd) = (char)(num + 48U);
			}
			return bufferEnd;
		}

		// Token: 0x06001182 RID: 4482 RVA: 0x000E01F8 File Offset: 0x000DF3F8
		internal unsafe static string UInt32ToDecStr(uint value)
		{
			int num = FormattingHelpers.CountDigits(value);
			if (num == 1)
			{
				return Number.s_singleDigitStringCache[(int)value];
			}
			string text = string.FastAllocateString(num);
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = text.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			char* ptr4 = ptr3 + num;
			do
			{
				uint num2;
				value = Math.DivRem(value, 10U, out num2);
				*(--ptr4) = (char)(num2 + 48U);
			}
			while (value != 0U);
			char* ptr2 = null;
			return text;
		}

		// Token: 0x06001183 RID: 4483 RVA: 0x000E025C File Offset: 0x000DF45C
		private unsafe static string UInt32ToDecStr(uint value, int digits)
		{
			if (digits <= 1)
			{
				return Number.UInt32ToDecStr(value);
			}
			int num = Math.Max(digits, FormattingHelpers.CountDigits(value));
			string text = string.FastAllocateString(num);
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = text.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			char* bufferEnd = ptr3 + num;
			bufferEnd = Number.UInt32ToDecChars(bufferEnd, value, digits);
			char* ptr2 = null;
			return text;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x000E02B0 File Offset: 0x000DF4B0
		private unsafe static bool TryUInt32ToDecStr(uint value, int digits, Span<char> destination, out int charsWritten)
		{
			int num = Math.Max(digits, FormattingHelpers.CountDigits(value));
			if (num > destination.Length)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				char* ptr2 = ptr + num;
				if (digits <= 1)
				{
					do
					{
						uint num2;
						value = Math.DivRem(value, 10U, out num2);
						*(--ptr2) = (char)(num2 + 48U);
					}
					while (value != 0U);
				}
				else
				{
					ptr2 = Number.UInt32ToDecChars(ptr2, value, digits);
				}
			}
			return true;
		}

		// Token: 0x06001185 RID: 4485 RVA: 0x000E031C File Offset: 0x000DF51C
		private unsafe static void Int64ToNumber(long input, ref Number.NumberBuffer number)
		{
			ulong value = (ulong)input;
			number.IsNegative = (input < 0L);
			number.DigitsCount = 19;
			if (number.IsNegative)
			{
				value = (ulong)(-(ulong)input);
			}
			byte* digitsPointer = number.GetDigitsPointer();
			byte* ptr = digitsPointer + 19;
			while (Number.High32(value) != 0U)
			{
				ptr = Number.UInt32ToDecChars(ptr, Number.Int64DivMod1E9(ref value), 9);
			}
			ptr = Number.UInt32ToDecChars(ptr, Number.Low32(value), 0);
			int num = (int)((long)(digitsPointer + 19 - ptr));
			number.DigitsCount = num;
			number.Scale = num;
			byte* digitsPointer2 = number.GetDigitsPointer();
			while (--num >= 0)
			{
				*(digitsPointer2++) = *(ptr++);
			}
			*digitsPointer2 = 0;
		}

		// Token: 0x06001186 RID: 4486 RVA: 0x000E03BA File Offset: 0x000DF5BA
		public static string Int64ToDecStr(long value)
		{
			if (value < 0L)
			{
				return Number.NegativeInt64ToDecStr(value, -1, NumberFormatInfo.CurrentInfo.NegativeSign);
			}
			return Number.UInt64ToDecStr((ulong)value, -1);
		}

		// Token: 0x06001187 RID: 4487 RVA: 0x000E03DC File Offset: 0x000DF5DC
		private unsafe static string NegativeInt64ToDecStr(long input, int digits, string sNegative)
		{
			if (digits < 1)
			{
				digits = 1;
			}
			ulong value = (ulong)(-(ulong)input);
			int num = Math.Max(digits, FormattingHelpers.CountDigits(value)) + sNegative.Length;
			string text = string.FastAllocateString(num);
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = text.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			char* ptr4 = ptr3 + num;
			while (Number.High32(value) != 0U)
			{
				ptr4 = Number.UInt32ToDecChars(ptr4, Number.Int64DivMod1E9(ref value), 9);
				digits -= 9;
			}
			ptr4 = Number.UInt32ToDecChars(ptr4, Number.Low32(value), digits);
			for (int i = sNegative.Length - 1; i >= 0; i--)
			{
				*(--ptr4) = sNegative[i];
			}
			char* ptr2 = null;
			return text;
		}

		// Token: 0x06001188 RID: 4488 RVA: 0x000E0488 File Offset: 0x000DF688
		private unsafe static bool TryNegativeInt64ToDecStr(long input, int digits, string sNegative, Span<char> destination, out int charsWritten)
		{
			if (digits < 1)
			{
				digits = 1;
			}
			ulong value = (ulong)(-(ulong)input);
			int num = Math.Max(digits, FormattingHelpers.CountDigits((ulong)(-(ulong)input))) + sNegative.Length;
			if (num > destination.Length)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				char* ptr2 = ptr + num;
				while (Number.High32(value) != 0U)
				{
					ptr2 = Number.UInt32ToDecChars(ptr2, Number.Int64DivMod1E9(ref value), 9);
					digits -= 9;
				}
				ptr2 = Number.UInt32ToDecChars(ptr2, Number.Low32(value), digits);
				for (int i = sNegative.Length - 1; i >= 0; i--)
				{
					*(--ptr2) = sNegative[i];
				}
			}
			return true;
		}

		// Token: 0x06001189 RID: 4489 RVA: 0x000E0538 File Offset: 0x000DF738
		private unsafe static string Int64ToHexStr(long value, char hexBase, int digits)
		{
			int num = Math.Max(digits, FormattingHelpers.CountHexDigits((ulong)value));
			string text = string.FastAllocateString(num);
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = text.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			char* buffer = ptr3 + num;
			if (Number.High32((ulong)value) != 0U)
			{
				buffer = Number.Int32ToHexChars(buffer, Number.Low32((ulong)value), (int)hexBase, 8);
				buffer = Number.Int32ToHexChars(buffer, Number.High32((ulong)value), (int)hexBase, digits - 8);
			}
			else
			{
				buffer = Number.Int32ToHexChars(buffer, Number.Low32((ulong)value), (int)hexBase, Math.Max(digits, 1));
			}
			char* ptr2 = null;
			return text;
		}

		// Token: 0x0600118A RID: 4490 RVA: 0x000E05BC File Offset: 0x000DF7BC
		private unsafe static bool TryInt64ToHexStr(long value, char hexBase, int digits, Span<char> destination, out int charsWritten)
		{
			int num = Math.Max(digits, FormattingHelpers.CountHexDigits((ulong)value));
			if (num > destination.Length)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				char* buffer = ptr + num;
				if (Number.High32((ulong)value) != 0U)
				{
					buffer = Number.Int32ToHexChars(buffer, Number.Low32((ulong)value), (int)hexBase, 8);
					buffer = Number.Int32ToHexChars(buffer, Number.High32((ulong)value), (int)hexBase, digits - 8);
				}
				else
				{
					buffer = Number.Int32ToHexChars(buffer, Number.Low32((ulong)value), (int)hexBase, Math.Max(digits, 1));
				}
			}
			return true;
		}

		// Token: 0x0600118B RID: 4491 RVA: 0x000E0640 File Offset: 0x000DF840
		private unsafe static void UInt64ToNumber(ulong value, ref Number.NumberBuffer number)
		{
			number.DigitsCount = 20;
			number.IsNegative = false;
			byte* digitsPointer = number.GetDigitsPointer();
			byte* ptr = digitsPointer + 20;
			while (Number.High32(value) != 0U)
			{
				ptr = Number.UInt32ToDecChars(ptr, Number.Int64DivMod1E9(ref value), 9);
			}
			ptr = Number.UInt32ToDecChars(ptr, Number.Low32(value), 0);
			int num = (int)((long)(digitsPointer + 20 - ptr));
			number.DigitsCount = num;
			number.Scale = num;
			byte* digitsPointer2 = number.GetDigitsPointer();
			while (--num >= 0)
			{
				*(digitsPointer2++) = *(ptr++);
			}
			*digitsPointer2 = 0;
		}

		// Token: 0x0600118C RID: 4492 RVA: 0x000E06CC File Offset: 0x000DF8CC
		internal unsafe static string UInt64ToDecStr(ulong value, int digits)
		{
			if (digits < 1)
			{
				digits = 1;
			}
			int num = Math.Max(digits, FormattingHelpers.CountDigits(value));
			if (num == 1)
			{
				return Number.s_singleDigitStringCache[(int)(checked((IntPtr)value))];
			}
			string text = string.FastAllocateString(num);
			char* ptr;
			if (text == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = text.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			char* bufferEnd = ptr3 + num;
			while (Number.High32(value) != 0U)
			{
				bufferEnd = Number.UInt32ToDecChars(bufferEnd, Number.Int64DivMod1E9(ref value), 9);
				digits -= 9;
			}
			bufferEnd = Number.UInt32ToDecChars(bufferEnd, Number.Low32(value), digits);
			char* ptr2 = null;
			return text;
		}

		// Token: 0x0600118D RID: 4493 RVA: 0x000E0750 File Offset: 0x000DF950
		private unsafe static bool TryUInt64ToDecStr(ulong value, int digits, Span<char> destination, out int charsWritten)
		{
			if (digits < 1)
			{
				digits = 1;
			}
			int num = Math.Max(digits, FormattingHelpers.CountDigits(value));
			if (num > destination.Length)
			{
				charsWritten = 0;
				return false;
			}
			charsWritten = num;
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				char* bufferEnd = ptr + num;
				while (Number.High32(value) != 0U)
				{
					bufferEnd = Number.UInt32ToDecChars(bufferEnd, Number.Int64DivMod1E9(ref value), 9);
					digits -= 9;
				}
				bufferEnd = Number.UInt32ToDecChars(bufferEnd, Number.Low32(value), digits);
			}
			return true;
		}

		// Token: 0x0600118E RID: 4494 RVA: 0x000E07C8 File Offset: 0x000DF9C8
		internal unsafe static char ParseFormatSpecifier(ReadOnlySpan<char> format, out int digits)
		{
			char c = '\0';
			if (format.Length > 0)
			{
				c = (char)(*format[0]);
				if (c - 'A' <= '\u0019' || c - 'a' <= '\u0019')
				{
					if (format.Length == 1)
					{
						digits = -1;
						return c;
					}
					if (format.Length == 2)
					{
						int num = (int)(*format[1] - 48);
						if (num < 10)
						{
							digits = num;
							return c;
						}
					}
					else if (format.Length == 3)
					{
						int num2 = (int)(*format[1] - 48);
						int num3 = (int)(*format[2] - 48);
						if (num2 < 10 && num3 < 10)
						{
							digits = num2 * 10 + num3;
							return c;
						}
					}
					int num4 = 0;
					int num5 = 1;
					while (num5 < format.Length && *format[num5] - 48 < 10 && num4 < 10)
					{
						num4 = num4 * 10 + (int)(*format[num5++]) - 48;
					}
					if (num5 == format.Length || *format[num5] == 0)
					{
						digits = num4;
						return c;
					}
				}
			}
			digits = -1;
			if (format.Length != 0 && c != '\0')
			{
				return '\0';
			}
			return 'G';
		}

		// Token: 0x0600118F RID: 4495 RVA: 0x000E08D8 File Offset: 0x000DFAD8
		internal unsafe static void NumberToString(ref ValueStringBuilder sb, ref Number.NumberBuffer number, char format, int nMaxDigits, NumberFormatInfo info)
		{
			bool isCorrectlyRounded = number.Kind == Number.NumberBufferKind.FloatingPoint;
			if (format <= 'R')
			{
				switch (format)
				{
				case 'C':
					break;
				case 'D':
					goto IL_1F5;
				case 'E':
					goto IL_11E;
				case 'F':
					goto IL_B4;
				case 'G':
					goto IL_153;
				default:
					switch (format)
					{
					case 'N':
						goto IL_F7;
					case 'O':
					case 'Q':
						goto IL_1F5;
					case 'P':
						goto IL_1AE;
					case 'R':
						goto IL_1E0;
					default:
						goto IL_1F5;
					}
					break;
				}
			}
			else
			{
				switch (format)
				{
				case 'c':
					break;
				case 'd':
					goto IL_1F5;
				case 'e':
					goto IL_11E;
				case 'f':
					goto IL_B4;
				case 'g':
					goto IL_153;
				default:
					switch (format)
					{
					case 'n':
						goto IL_F7;
					case 'o':
					case 'q':
						goto IL_1F5;
					case 'p':
						goto IL_1AE;
					case 'r':
						goto IL_1E0;
					default:
						goto IL_1F5;
					}
					break;
				}
			}
			if (nMaxDigits < 0)
			{
				nMaxDigits = info.CurrencyDecimalDigits;
			}
			Number.RoundNumber(ref number, number.Scale + nMaxDigits, isCorrectlyRounded);
			Number.FormatCurrency(ref sb, ref number, nMaxDigits, info);
			return;
			IL_B4:
			if (nMaxDigits < 0)
			{
				nMaxDigits = info.NumberDecimalDigits;
			}
			Number.RoundNumber(ref number, number.Scale + nMaxDigits, isCorrectlyRounded);
			if (number.IsNegative)
			{
				sb.Append(info.NegativeSign);
			}
			Number.FormatFixed(ref sb, ref number, nMaxDigits, null, info.NumberDecimalSeparator, null);
			return;
			IL_F7:
			if (nMaxDigits < 0)
			{
				nMaxDigits = info.NumberDecimalDigits;
			}
			Number.RoundNumber(ref number, number.Scale + nMaxDigits, isCorrectlyRounded);
			Number.FormatNumber(ref sb, ref number, nMaxDigits, info);
			return;
			IL_11E:
			if (nMaxDigits < 0)
			{
				nMaxDigits = 6;
			}
			nMaxDigits++;
			Number.RoundNumber(ref number, nMaxDigits, isCorrectlyRounded);
			if (number.IsNegative)
			{
				sb.Append(info.NegativeSign);
			}
			Number.FormatScientific(ref sb, ref number, nMaxDigits, info, format);
			return;
			IL_153:
			bool bSuppressScientific = false;
			if (nMaxDigits < 1)
			{
				if (number.Kind == Number.NumberBufferKind.Decimal && nMaxDigits == -1)
				{
					bSuppressScientific = true;
					if (*number.Digits[0] == 0)
					{
						goto IL_19E;
					}
					goto IL_189;
				}
				else
				{
					nMaxDigits = number.DigitsCount;
				}
			}
			Number.RoundNumber(ref number, nMaxDigits, isCorrectlyRounded);
			IL_189:
			if (number.IsNegative)
			{
				sb.Append(info.NegativeSign);
			}
			IL_19E:
			Number.FormatGeneral(ref sb, ref number, nMaxDigits, info, format - '\u0002', bSuppressScientific);
			return;
			IL_1AE:
			if (nMaxDigits < 0)
			{
				nMaxDigits = info.PercentDecimalDigits;
			}
			number.Scale += 2;
			Number.RoundNumber(ref number, number.Scale + nMaxDigits, isCorrectlyRounded);
			Number.FormatPercent(ref sb, ref number, nMaxDigits, info);
			return;
			IL_1E0:
			if (number.Kind == Number.NumberBufferKind.FloatingPoint)
			{
				format -= '\v';
				goto IL_153;
			}
			IL_1F5:
			throw new FormatException(SR.Argument_BadFormatSpecifier);
		}

		// Token: 0x06001190 RID: 4496 RVA: 0x000E0AE4 File Offset: 0x000DFCE4
		internal unsafe static void NumberToStringFormat(ref ValueStringBuilder sb, ref Number.NumberBuffer number, ReadOnlySpan<char> format, NumberFormatInfo info)
		{
			int num = 0;
			byte* digitsPointer = number.GetDigitsPointer();
			int num2 = Number.FindSection(format, (*digitsPointer == 0) ? 2 : (number.IsNegative ? 1 : 0));
			int num3;
			int num4;
			int num5;
			int num6;
			bool flag;
			bool flag2;
			int i;
			for (;;)
			{
				num3 = 0;
				num4 = -1;
				num5 = int.MaxValue;
				num6 = 0;
				flag = false;
				int num7 = -1;
				flag2 = false;
				int num8 = 0;
				i = num2;
				fixed (char* reference = MemoryMarshal.GetReference<char>(format))
				{
					char* ptr = reference;
					char c;
					while (i < format.Length && (c = ptr[(IntPtr)(i++) * 2]) != '\0' && c != ';')
					{
						if (c <= 'E')
						{
							switch (c)
							{
							case '"':
							case '\'':
								while (i < format.Length && ptr[i] != '\0')
								{
									if (ptr[(IntPtr)(i++) * 2] == c)
									{
										break;
									}
								}
								continue;
							case '#':
								num3++;
								continue;
							case '$':
							case '&':
								continue;
							case '%':
								num8 += 2;
								continue;
							default:
								switch (c)
								{
								case ',':
									if (num3 > 0 && num4 < 0)
									{
										if (num7 >= 0)
										{
											if (num7 == num3)
											{
												num++;
												continue;
											}
											flag2 = true;
										}
										num7 = num3;
										num = 1;
										continue;
									}
									continue;
								case '-':
								case '/':
									continue;
								case '.':
									if (num4 < 0)
									{
										num4 = num3;
										continue;
									}
									continue;
								case '0':
									if (num5 == 2147483647)
									{
										num5 = num3;
									}
									num3++;
									num6 = num3;
									continue;
								default:
									if (c != 'E')
									{
										continue;
									}
									break;
								}
								break;
							}
						}
						else if (c != '\\')
						{
							if (c != 'e')
							{
								if (c != '‰')
								{
									continue;
								}
								num8 += 3;
								continue;
							}
						}
						else
						{
							if (i < format.Length && ptr[i] != '\0')
							{
								i++;
								continue;
							}
							continue;
						}
						if ((i < format.Length && ptr[i] == '0') || (i + 1 < format.Length && (ptr[i] == '+' || ptr[i] == '-') && ptr[i + 1] == '0'))
						{
							while (++i < format.Length && ptr[i] == '0')
							{
							}
							flag = true;
						}
					}
				}
				if (num4 < 0)
				{
					num4 = num3;
				}
				if (num7 >= 0)
				{
					if (num7 == num4)
					{
						num8 -= num * 3;
					}
					else
					{
						flag2 = true;
					}
				}
				if (*digitsPointer == 0)
				{
					break;
				}
				number.Scale += num8;
				int pos = flag ? num3 : (number.Scale + num3 - num4);
				Number.RoundNumber(ref number, pos, false);
				if (*digitsPointer != 0)
				{
					goto IL_2A8;
				}
				i = Number.FindSection(format, 2);
				if (i == num2)
				{
					goto IL_2A8;
				}
				num2 = i;
			}
			if (number.Kind != Number.NumberBufferKind.FloatingPoint)
			{
				number.IsNegative = false;
			}
			number.Scale = 0;
			IL_2A8:
			num5 = ((num5 < num4) ? (num4 - num5) : 0);
			num6 = ((num6 > num4) ? (num4 - num6) : 0);
			int num9;
			int j;
			if (flag)
			{
				num9 = num4;
				j = 0;
			}
			else
			{
				num9 = ((number.Scale > num4) ? number.Scale : num4);
				j = number.Scale - num4;
			}
			i = num2;
			Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)16], 4);
			Span<int> span2 = span;
			int num10 = -1;
			if (flag2 && info.NumberGroupSeparator.Length > 0)
			{
				int[] numberGroupSizes = info._numberGroupSizes;
				int num11 = 0;
				int num12 = 0;
				int num13 = numberGroupSizes.Length;
				if (num13 != 0)
				{
					num12 = numberGroupSizes[num11];
				}
				int num14 = num12;
				int num15 = num9 + ((j < 0) ? j : 0);
				int num16 = (num5 > num15) ? num5 : num15;
				while (num16 > num12 && num14 != 0)
				{
					num10++;
					if (num10 >= span2.Length)
					{
						int[] array = new int[span2.Length * 2];
						span2.CopyTo(array);
						span2 = array;
					}
					*span2[num10] = num12;
					if (num11 < num13 - 1)
					{
						num11++;
						num14 = numberGroupSizes[num11];
					}
					num12 += num14;
				}
			}
			if (number.IsNegative && num2 == 0 && number.Scale != 0)
			{
				sb.Append(info.NegativeSign);
			}
			bool flag3 = false;
			fixed (char* reference2 = MemoryMarshal.GetReference<char>(format))
			{
				char* ptr2 = reference2;
				byte* ptr3 = digitsPointer;
				char c;
				while (i < format.Length && (c = ptr2[(IntPtr)(i++) * 2]) != '\0' && c != ';')
				{
					if (j > 0)
					{
						if (c == '#' || c == '.' || c == '0')
						{
							while (j > 0)
							{
								sb.Append((char)((*ptr3 != 0) ? (*(ptr3++)) : 48));
								if (flag2 && num9 > 1 && num10 >= 0 && num9 == *span2[num10] + 1)
								{
									sb.Append(info.NumberGroupSeparator);
									num10--;
								}
								num9--;
								j--;
							}
						}
					}
					if (c <= 'E')
					{
						switch (c)
						{
						case '"':
						case '\'':
							while (i < format.Length && ptr2[i] != '\0' && ptr2[i] != c)
							{
								sb.Append(ptr2[(IntPtr)(i++) * 2]);
							}
							if (i < format.Length && ptr2[i] != '\0')
							{
								i++;
								continue;
							}
							continue;
						case '#':
							break;
						case '$':
						case '&':
							goto IL_79C;
						case '%':
							sb.Append(info.PercentSymbol);
							continue;
						default:
							switch (c)
							{
							case ',':
								continue;
							case '-':
							case '/':
								goto IL_79C;
							case '.':
								if (num9 == 0 && !flag3 && (num6 < 0 || (num4 < num3 && *ptr3 != 0)))
								{
									sb.Append(info.NumberDecimalSeparator);
									flag3 = true;
									continue;
								}
								continue;
							case '0':
								break;
							default:
								if (c != 'E')
								{
									goto IL_79C;
								}
								goto IL_647;
							}
							break;
						}
						if (j < 0)
						{
							j++;
							c = ((num9 <= num5) ? '0' : '\0');
						}
						else
						{
							c = (char)((*ptr3 != 0) ? (*(ptr3++)) : ((num9 > num6) ? 48 : 0));
						}
						if (c != '\0')
						{
							sb.Append(c);
							if (flag2 && num9 > 1 && num10 >= 0 && num9 == *span2[num10] + 1)
							{
								sb.Append(info.NumberGroupSeparator);
								num10--;
							}
						}
						num9--;
						continue;
					}
					if (c != '\\')
					{
						if (c != 'e')
						{
							if (c != '‰')
							{
								goto IL_79C;
							}
							sb.Append(info.PerMilleSymbol);
							continue;
						}
					}
					else
					{
						if (i < format.Length && ptr2[i] != '\0')
						{
							sb.Append(ptr2[(IntPtr)(i++) * 2]);
							continue;
						}
						continue;
					}
					IL_647:
					bool positiveSign = false;
					int num17 = 0;
					if (flag)
					{
						if (i < format.Length && ptr2[i] == '0')
						{
							num17++;
						}
						else if (i + 1 < format.Length && ptr2[i] == '+' && ptr2[i + 1] == '0')
						{
							positiveSign = true;
						}
						else if (i + 1 >= format.Length || ptr2[i] != '-' || ptr2[i + 1] != '0')
						{
							sb.Append(c);
							continue;
						}
						while (++i < format.Length && ptr2[i] == '0')
						{
							num17++;
						}
						if (num17 > 10)
						{
							num17 = 10;
						}
						int value = (*digitsPointer == 0) ? 0 : (number.Scale - num4);
						Number.FormatExponent(ref sb, info, value, c, num17, positiveSign);
						flag = false;
						continue;
					}
					sb.Append(c);
					if (i < format.Length)
					{
						if (ptr2[i] == '+' || ptr2[i] == '-')
						{
							sb.Append(ptr2[(IntPtr)(i++) * 2]);
						}
						while (i < format.Length)
						{
							if (ptr2[i] != '0')
							{
								break;
							}
							sb.Append(ptr2[(IntPtr)(i++) * 2]);
						}
						continue;
					}
					continue;
					IL_79C:
					sb.Append(c);
				}
			}
			if (number.IsNegative && num2 == 0 && number.Scale == 0 && sb.Length > 0)
			{
				sb.Insert(0, info.NegativeSign);
			}
		}

		// Token: 0x06001191 RID: 4497 RVA: 0x000E12EC File Offset: 0x000E04EC
		private static void FormatCurrency(ref ValueStringBuilder sb, ref Number.NumberBuffer number, int nMaxDigits, NumberFormatInfo info)
		{
			string text = number.IsNegative ? Number.s_negCurrencyFormats[info.CurrencyNegativePattern] : Number.s_posCurrencyFormats[info.CurrencyPositivePattern];
			foreach (char c in text)
			{
				if (c != '#')
				{
					if (c != '$')
					{
						if (c != '-')
						{
							sb.Append(c);
						}
						else
						{
							sb.Append(info.NegativeSign);
						}
					}
					else
					{
						sb.Append(info.CurrencySymbol);
					}
				}
				else
				{
					Number.FormatFixed(ref sb, ref number, nMaxDigits, info._currencyGroupSizes, info.CurrencyDecimalSeparator, info.CurrencyGroupSeparator);
				}
			}
		}

		// Token: 0x06001192 RID: 4498 RVA: 0x000E1388 File Offset: 0x000E0588
		private unsafe static void FormatFixed(ref ValueStringBuilder sb, ref Number.NumberBuffer number, int nMaxDigits, int[] groupDigits, string sDecimal, string sGroup)
		{
			int i = number.Scale;
			byte* ptr = number.GetDigitsPointer();
			if (i > 0)
			{
				if (groupDigits != null)
				{
					int num = 0;
					int num2 = i;
					int num3 = 0;
					if (groupDigits.Length != 0)
					{
						int num4 = groupDigits[num];
						while (i > num4)
						{
							num3 = groupDigits[num];
							if (num3 == 0)
							{
								break;
							}
							num2 += sGroup.Length;
							if (num < groupDigits.Length - 1)
							{
								num++;
							}
							num4 += groupDigits[num];
							if (num4 < 0 || num2 < 0)
							{
								throw new ArgumentOutOfRangeException();
							}
						}
						num3 = ((num4 == 0) ? 0 : groupDigits[0]);
					}
					num = 0;
					int num5 = 0;
					int digitsCount = number.DigitsCount;
					int num6 = (i < digitsCount) ? i : digitsCount;
					fixed (char* reference = MemoryMarshal.GetReference<char>(sb.AppendSpan(num2)))
					{
						char* ptr2 = reference;
						char* ptr3 = ptr2 + num2 - 1;
						for (int j = i - 1; j >= 0; j--)
						{
							*(ptr3--) = (char)((j < num6) ? ptr[j] : 48);
							if (num3 > 0)
							{
								num5++;
								if (num5 == num3 && j != 0)
								{
									for (int k = sGroup.Length - 1; k >= 0; k--)
									{
										*(ptr3--) = sGroup[k];
									}
									if (num < groupDigits.Length - 1)
									{
										num++;
										num3 = groupDigits[num];
									}
									num5 = 0;
								}
							}
						}
						ptr += num6;
					}
				}
				else
				{
					do
					{
						sb.Append((char)((*ptr != 0) ? (*(ptr++)) : 48));
					}
					while (--i > 0);
				}
			}
			else
			{
				sb.Append('0');
			}
			if (nMaxDigits > 0)
			{
				sb.Append(sDecimal);
				if (i < 0 && nMaxDigits > 0)
				{
					int num7 = Math.Min(-i, nMaxDigits);
					sb.Append('0', num7);
					i += num7;
					nMaxDigits -= num7;
				}
				while (nMaxDigits > 0)
				{
					sb.Append((char)((*ptr != 0) ? (*(ptr++)) : 48));
					nMaxDigits--;
				}
			}
		}

		// Token: 0x06001193 RID: 4499 RVA: 0x000E1540 File Offset: 0x000E0740
		private static void FormatNumber(ref ValueStringBuilder sb, ref Number.NumberBuffer number, int nMaxDigits, NumberFormatInfo info)
		{
			string text = number.IsNegative ? Number.s_negNumberFormats[info.NumberNegativePattern] : "#";
			foreach (char c in text)
			{
				if (c != '#')
				{
					if (c != '-')
					{
						sb.Append(c);
					}
					else
					{
						sb.Append(info.NegativeSign);
					}
				}
				else
				{
					Number.FormatFixed(ref sb, ref number, nMaxDigits, info._numberGroupSizes, info.NumberDecimalSeparator, info.NumberGroupSeparator);
				}
			}
		}

		// Token: 0x06001194 RID: 4500 RVA: 0x000E15C4 File Offset: 0x000E07C4
		private unsafe static void FormatScientific(ref ValueStringBuilder sb, ref Number.NumberBuffer number, int nMaxDigits, NumberFormatInfo info, char expChar)
		{
			byte* digitsPointer = number.GetDigitsPointer();
			sb.Append((char)((*digitsPointer != 0) ? (*(digitsPointer++)) : 48));
			if (nMaxDigits != 1)
			{
				sb.Append(info.NumberDecimalSeparator);
			}
			while (--nMaxDigits > 0)
			{
				sb.Append((char)((*digitsPointer != 0) ? (*(digitsPointer++)) : 48));
			}
			int value = (*number.Digits[0] == 0) ? 0 : (number.Scale - 1);
			Number.FormatExponent(ref sb, info, value, expChar, 3, true);
		}

		// Token: 0x06001195 RID: 4501 RVA: 0x000E1644 File Offset: 0x000E0844
		private unsafe static void FormatExponent(ref ValueStringBuilder sb, NumberFormatInfo info, int value, char expChar, int minDigits, bool positiveSign)
		{
			sb.Append(expChar);
			if (value < 0)
			{
				sb.Append(info.NegativeSign);
				value = -value;
			}
			else if (positiveSign)
			{
				sb.Append(info.PositiveSign);
			}
			char* ptr = stackalloc char[(UIntPtr)20];
			char* ptr2 = Number.UInt32ToDecChars(ptr + 10, (uint)value, minDigits);
			sb.Append(ptr2, (int)((long)(ptr + 10 - ptr2)));
		}

		// Token: 0x06001196 RID: 4502 RVA: 0x000E16A8 File Offset: 0x000E08A8
		private unsafe static void FormatGeneral(ref ValueStringBuilder sb, ref Number.NumberBuffer number, int nMaxDigits, NumberFormatInfo info, char expChar, bool bSuppressScientific)
		{
			int i = number.Scale;
			bool flag = false;
			if (!bSuppressScientific && (i > nMaxDigits || i < -3))
			{
				i = 1;
				flag = true;
			}
			byte* digitsPointer = number.GetDigitsPointer();
			if (i > 0)
			{
				do
				{
					sb.Append((char)((*digitsPointer != 0) ? (*(digitsPointer++)) : 48));
				}
				while (--i > 0);
			}
			else
			{
				sb.Append('0');
			}
			if (*digitsPointer != 0 || i < 0)
			{
				sb.Append(info.NumberDecimalSeparator);
				while (i < 0)
				{
					sb.Append('0');
					i++;
				}
				while (*digitsPointer != 0)
				{
					sb.Append((char)(*(digitsPointer++)));
				}
			}
			if (flag)
			{
				Number.FormatExponent(ref sb, info, number.Scale - 1, expChar, 2, true);
			}
		}

		// Token: 0x06001197 RID: 4503 RVA: 0x000E1750 File Offset: 0x000E0950
		private static void FormatPercent(ref ValueStringBuilder sb, ref Number.NumberBuffer number, int nMaxDigits, NumberFormatInfo info)
		{
			string text = number.IsNegative ? Number.s_negPercentFormats[info.PercentNegativePattern] : Number.s_posPercentFormats[info.PercentPositivePattern];
			foreach (char c in text)
			{
				if (c != '#')
				{
					if (c != '%')
					{
						if (c != '-')
						{
							sb.Append(c);
						}
						else
						{
							sb.Append(info.NegativeSign);
						}
					}
					else
					{
						sb.Append(info.PercentSymbol);
					}
				}
				else
				{
					Number.FormatFixed(ref sb, ref number, nMaxDigits, info._percentGroupSizes, info.PercentDecimalSeparator, info.PercentGroupSeparator);
				}
			}
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x000E17EC File Offset: 0x000E09EC
		internal unsafe static void RoundNumber(ref Number.NumberBuffer number, int pos, bool isCorrectlyRounded)
		{
			byte* digitsPointer = number.GetDigitsPointer();
			int num = 0;
			while (num < pos && digitsPointer[num] != 0)
			{
				num++;
			}
			if (num == pos && Number.<RoundNumber>g__ShouldRoundUp|78_0(digitsPointer, num, number.Kind, isCorrectlyRounded))
			{
				while (num > 0 && digitsPointer[num - 1] == 57)
				{
					num--;
				}
				if (num > 0)
				{
					byte* ptr = digitsPointer + (num - 1);
					*ptr += 1;
				}
				else
				{
					number.Scale++;
					*digitsPointer = 49;
					num = 1;
				}
			}
			else
			{
				while (num > 0 && digitsPointer[num - 1] == 48)
				{
					num--;
				}
			}
			if (num == 0)
			{
				if (number.Kind != Number.NumberBufferKind.FloatingPoint)
				{
					number.IsNegative = false;
				}
				number.Scale = 0;
			}
			digitsPointer[num] = 0;
			number.DigitsCount = num;
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x000E1898 File Offset: 0x000E0A98
		private unsafe static int FindSection(ReadOnlySpan<char> format, int section)
		{
			if (section == 0)
			{
				return 0;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(format))
			{
				char* ptr = reference;
				int i = 0;
				while (i < format.Length)
				{
					char c2;
					char c = c2 = ptr[(IntPtr)(i++) * 2];
					if (c2 <= '"')
					{
						if (c2 == '\0')
						{
							return 0;
						}
						if (c2 != '"')
						{
							continue;
						}
					}
					else if (c2 != '\'')
					{
						if (c2 != ';')
						{
							if (c2 != '\\')
							{
								continue;
							}
							if (i < format.Length && ptr[i] != '\0')
							{
								i++;
								continue;
							}
							continue;
						}
						else
						{
							if (--section != 0)
							{
								continue;
							}
							if (i < format.Length && ptr[i] != '\0' && ptr[i] != ';')
							{
								return i;
							}
							return 0;
						}
					}
					while (i < format.Length && ptr[i] != '\0')
					{
						if (ptr[(IntPtr)(i++) * 2] == c)
						{
							break;
						}
					}
				}
				return 0;
			}
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x000E1962 File Offset: 0x000E0B62
		private static uint Low32(ulong value)
		{
			return (uint)value;
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000E1966 File Offset: 0x000E0B66
		private static uint High32(ulong value)
		{
			return (uint)((value & 18446744069414584320UL) >> 32);
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000E1978 File Offset: 0x000E0B78
		private static uint Int64DivMod1E9(ref ulong value)
		{
			uint result = (uint)(value % 1000000000UL);
			value /= 1000000000UL;
			return result;
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x000E199C File Offset: 0x000E0B9C
		private static ulong ExtractFractionAndBiasedExponent(double value, out int exponent)
		{
			ulong num = (ulong)BitConverter.DoubleToInt64Bits(value);
			ulong num2 = num & 4503599627370495UL;
			exponent = ((int)(num >> 52) & 2047);
			if (exponent != 0)
			{
				num2 |= 4503599627370496UL;
				exponent -= 1075;
			}
			else
			{
				exponent = -1074;
			}
			return num2;
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x000E19F0 File Offset: 0x000E0BF0
		private static ushort ExtractFractionAndBiasedExponent(Half value, out int exponent)
		{
			ushort num = (ushort)BitConverter.HalfToInt16Bits(value);
			ushort num2 = num & 1023;
			exponent = (num >> 10 & 31);
			if (exponent != 0)
			{
				num2 |= 1024;
				exponent -= 25;
			}
			else
			{
				exponent = -24;
			}
			return num2;
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x000E1A34 File Offset: 0x000E0C34
		private static uint ExtractFractionAndBiasedExponent(float value, out int exponent)
		{
			uint num = (uint)BitConverter.SingleToInt32Bits(value);
			uint num2 = num & 8388607U;
			exponent = (int)(num >> 23 & 255U);
			if (exponent != 0)
			{
				num2 |= 8388608U;
				exponent -= 150;
			}
			else
			{
				exponent = -149;
			}
			return num2;
		}

		// Token: 0x060011A0 RID: 4512 RVA: 0x000E1A7C File Offset: 0x000E0C7C
		private unsafe static void AccumulateDecimalDigitsIntoBigInteger(ref Number.NumberBuffer number, uint firstIndex, uint lastIndex, out Number.BigInteger result)
		{
			Number.BigInteger.SetZero(out result);
			byte* ptr = number.GetDigitsPointer() + firstIndex;
			uint num2;
			for (uint num = lastIndex - firstIndex; num != 0U; num -= num2)
			{
				num2 = Math.Min(num, 9U);
				uint value = Number.DigitsToUInt32(ptr, (int)num2);
				result.MultiplyPow10(num2);
				result.Add(value);
				ptr += num2;
			}
		}

		// Token: 0x060011A1 RID: 4513 RVA: 0x000E1ACC File Offset: 0x000E0CCC
		private static ulong AssembleFloatingPointBits(in Number.FloatingPointInfo info, ulong initialMantissa, int initialExponent, bool hasZeroTail)
		{
			uint num = Number.BigInteger.CountSignificantBits(initialMantissa);
			int num2 = (int)((uint)info.NormalMantissaBits - num);
			int num3 = initialExponent - num2;
			ulong num4 = initialMantissa;
			int num5 = num3;
			if (num3 > info.MaxBinaryExponent)
			{
				return info.InfinityBits;
			}
			if (num3 < info.MinBinaryExponent)
			{
				int num6 = num2 + num3 + info.ExponentBias - 1;
				num5 = -info.ExponentBias;
				if (num6 < 0)
				{
					num4 = Number.RightShiftWithRounding(num4, -num6, hasZeroTail);
					if (num4 == 0UL)
					{
						return info.ZeroBits;
					}
					if (num4 > info.DenormalMantissaMask)
					{
						num5 = initialExponent - (num6 + 1) - num2;
					}
				}
				else
				{
					num4 <<= num6;
				}
			}
			else if (num2 < 0)
			{
				num4 = Number.RightShiftWithRounding(num4, -num2, hasZeroTail);
				if (num4 > info.NormalMantissaMask)
				{
					num4 >>= 1;
					num5++;
					if (num5 > info.MaxBinaryExponent)
					{
						return info.InfinityBits;
					}
				}
			}
			else if (num2 > 0)
			{
				num4 <<= num2;
			}
			num4 &= info.DenormalMantissaMask;
			ulong num7 = (ulong)((ulong)((long)(num5 + info.ExponentBias)) << (int)info.DenormalMantissaBits);
			return num7 | num4;
		}

		// Token: 0x060011A2 RID: 4514 RVA: 0x000E1BBC File Offset: 0x000E0DBC
		private static ulong ConvertBigIntegerToFloatingPointBits(ref Number.BigInteger value, in Number.FloatingPointInfo info, uint integerBitsOfPrecision, bool hasNonZeroFractionalPart)
		{
			int denormalMantissaBits = (int)info.DenormalMantissaBits;
			if (integerBitsOfPrecision <= 64U)
			{
				return Number.AssembleFloatingPointBits(info, value.ToUInt64(), denormalMantissaBits, !hasNonZeroFractionalPart);
			}
			uint num2;
			uint num = Math.DivRem(integerBitsOfPrecision, 32U, out num2);
			uint num3 = num - 1U;
			uint num4 = num3 - 1U;
			int num5 = denormalMantissaBits + (int)(num4 * 32U);
			bool flag = !hasNonZeroFractionalPart;
			ulong initialMantissa;
			if (num2 == 0U)
			{
				initialMantissa = ((ulong)value.GetBlock(num3) << 32) + (ulong)value.GetBlock(num4);
			}
			else
			{
				int num6 = (int)num2;
				int num7 = 64 - num6;
				int num8 = num7 - 32;
				num5 += (int)num2;
				uint block = value.GetBlock(num4);
				uint num9 = block >> num6;
				ulong num10 = (ulong)value.GetBlock(num3) << num8;
				ulong num11 = (ulong)value.GetBlock(num) << num7;
				initialMantissa = num11 + num10 + (ulong)num9;
				uint num12 = (1U << (int)num2) - 1U;
				flag &= ((block & num12) == 0U);
			}
			for (uint num13 = 0U; num13 != num4; num13 += 1U)
			{
				flag &= (value.GetBlock(num13) == 0U);
			}
			return Number.AssembleFloatingPointBits(info, initialMantissa, num5, flag);
		}

		// Token: 0x060011A3 RID: 4515 RVA: 0x000E1CC0 File Offset: 0x000E0EC0
		private unsafe static uint DigitsToUInt32(byte* p, int count)
		{
			byte* ptr = p + count;
			uint num = (uint)(*p - 48);
			for (p++; p < ptr; p++)
			{
				num = 10U * num + (uint)(*p) - 48U;
			}
			return num;
		}

		// Token: 0x060011A4 RID: 4516 RVA: 0x000E1CF4 File Offset: 0x000E0EF4
		private unsafe static ulong DigitsToUInt64(byte* p, int count)
		{
			byte* ptr = p + count;
			ulong num = (ulong)((long)(*p - 48));
			for (p++; p < ptr; p++)
			{
				num = 10UL * num + (ulong)(*p) - 48UL;
			}
			return num;
		}

		// Token: 0x060011A5 RID: 4517 RVA: 0x000E1D2C File Offset: 0x000E0F2C
		private unsafe static ulong NumberToFloatingPointBits(ref Number.NumberBuffer number, in Number.FloatingPointInfo info)
		{
			uint digitsCount = (uint)number.DigitsCount;
			uint num = (uint)Math.Max(0, number.Scale);
			uint num2 = Math.Min(num, digitsCount);
			uint num3 = digitsCount - num2;
			uint num4 = (uint)Math.Abs((long)number.Scale - (long)((ulong)num2) - (long)((ulong)num3));
			byte* digitsPointer = number.GetDigitsPointer();
			if (info.DenormalMantissaBits <= 23 && digitsCount <= 7U && num4 <= 10U)
			{
				float num5 = Number.DigitsToUInt32(digitsPointer, (int)digitsCount);
				float num6 = Number.s_Pow10SingleTable[(int)num4];
				if (num3 != 0U)
				{
					num5 /= num6;
				}
				else
				{
					num5 *= num6;
				}
				if (info.DenormalMantissaBits == 10)
				{
					return (ulong)((ushort)BitConverter.HalfToInt16Bits((Half)num5));
				}
				return (ulong)BitConverter.SingleToInt32Bits(num5);
			}
			else
			{
				if (digitsCount > 15U || num4 > 22U)
				{
					return Number.NumberToFloatingPointBitsSlow(ref number, info, num, num2, num3);
				}
				double num7 = Number.DigitsToUInt64(digitsPointer, (int)digitsCount);
				double num8 = Number.s_Pow10DoubleTable[(int)num4];
				if (num3 != 0U)
				{
					num7 /= num8;
				}
				else
				{
					num7 *= num8;
				}
				if (info.DenormalMantissaBits == 52)
				{
					return (ulong)BitConverter.DoubleToInt64Bits(num7);
				}
				if (info.DenormalMantissaBits == 23)
				{
					return (ulong)BitConverter.SingleToInt32Bits((float)num7);
				}
				return (ulong)BitConverter.HalfToInt16Bits((Half)num7);
			}
		}

		// Token: 0x060011A6 RID: 4518 RVA: 0x000E1E48 File Offset: 0x000E1048
		private static ulong NumberToFloatingPointBitsSlow(ref Number.NumberBuffer number, in Number.FloatingPointInfo info, uint positiveExponent, uint integerDigitsPresent, uint fractionalDigitsPresent)
		{
			uint num = (uint)(info.NormalMantissaBits + 1);
			uint digitsCount = (uint)number.DigitsCount;
			uint num2 = positiveExponent - integerDigitsPresent;
			uint lastIndex = digitsCount;
			Number.BigInteger bigInteger;
			Number.AccumulateDecimalDigitsIntoBigInteger(ref number, 0U, integerDigitsPresent, out bigInteger);
			if (num2 > 0U)
			{
				if ((ulong)num2 > (ulong)((long)info.OverflowDecimalExponent))
				{
					return info.InfinityBits;
				}
				bigInteger.MultiplyPow10(num2);
			}
			uint num3 = Number.BigInteger.CountSignificantBits(ref bigInteger);
			if (num3 >= num || fractionalDigitsPresent == 0U)
			{
				return Number.ConvertBigIntegerToFloatingPointBits(ref bigInteger, info, num3, fractionalDigitsPresent > 0U);
			}
			uint num4 = fractionalDigitsPresent;
			if (number.Scale < 0)
			{
				num4 += (uint)(-(uint)number.Scale);
			}
			if (num3 == 0U && (ulong)num4 - (ulong)((long)digitsCount) > (ulong)((long)info.OverflowDecimalExponent))
			{
				return info.ZeroBits;
			}
			Number.BigInteger bigInteger2;
			Number.AccumulateDecimalDigitsIntoBigInteger(ref number, integerDigitsPresent, lastIndex, out bigInteger2);
			if (bigInteger2.IsZero())
			{
				return Number.ConvertBigIntegerToFloatingPointBits(ref bigInteger, info, num3, fractionalDigitsPresent > 0U);
			}
			Number.BigInteger bigInteger3;
			Number.BigInteger.Pow10(num4, out bigInteger3);
			uint num5 = Number.BigInteger.CountSignificantBits(ref bigInteger2);
			uint num6 = Number.BigInteger.CountSignificantBits(ref bigInteger3);
			uint num7 = 0U;
			if (num6 > num5)
			{
				num7 = num6 - num5;
			}
			if (num7 > 0U)
			{
				bigInteger2.ShiftLeft(num7);
			}
			uint num8 = num - num3;
			uint num9 = num8;
			if (num3 > 0U)
			{
				if (num7 > num9)
				{
					return Number.ConvertBigIntegerToFloatingPointBits(ref bigInteger, info, num3, fractionalDigitsPresent > 0U);
				}
				num9 -= num7;
			}
			uint num10 = num7;
			if (Number.BigInteger.Compare(ref bigInteger2, ref bigInteger3) < 0)
			{
				num10 += 1U;
			}
			bigInteger2.ShiftLeft(num9);
			Number.BigInteger bigInteger4;
			Number.BigInteger bigInteger5;
			Number.BigInteger.DivRem(ref bigInteger2, ref bigInteger3, out bigInteger4, out bigInteger5);
			ulong num11 = bigInteger4.ToUInt64();
			bool flag = !number.HasNonZeroTail && bigInteger5.IsZero();
			uint num12 = Number.BigInteger.CountSignificantBits(num11);
			if (num12 > num8)
			{
				int num13 = (int)(num12 - num8);
				flag = (flag && (num11 & (1UL << num13) - 1UL) == 0UL);
				num11 >>= num13;
			}
			ulong num14 = bigInteger.ToUInt64();
			ulong initialMantissa = (num14 << (int)num8) + num11;
			int initialExponent = (int)((num3 > 0U) ? (num3 - 2U) : (-num10 - 1U));
			return Number.AssembleFloatingPointBits(info, initialMantissa, initialExponent, flag);
		}

		// Token: 0x060011A7 RID: 4519 RVA: 0x000E2030 File Offset: 0x000E1230
		private static ulong RightShiftWithRounding(ulong value, int shift, bool hasZeroTail)
		{
			if (shift >= 64)
			{
				return 0UL;
			}
			ulong num = (1UL << shift - 1) - 1UL;
			ulong num2 = 1UL << shift - 1;
			ulong num3 = 1UL << shift;
			bool lsbBit = (value & num3) > 0UL;
			bool roundBit = (value & num2) > 0UL;
			bool hasTailBits = !hasZeroTail || (value & num) > 0UL;
			return (value >> shift) + (Number.ShouldRoundUp(lsbBit, roundBit, hasTailBits) ? 1UL : 0UL);
		}

		// Token: 0x060011A8 RID: 4520 RVA: 0x000E209D File Offset: 0x000E129D
		private static bool ShouldRoundUp(bool lsbBit, bool roundBit, bool hasTailBits)
		{
			return roundBit && (hasTailBits || lsbBit);
		}

		// Token: 0x060011A9 RID: 4521 RVA: 0x000E20A8 File Offset: 0x000E12A8
		private unsafe static bool TryNumberToInt32(ref Number.NumberBuffer number, ref int value)
		{
			int num = number.Scale;
			if (num > 10 || num < number.DigitsCount)
			{
				return false;
			}
			byte* digitsPointer = number.GetDigitsPointer();
			int num2 = 0;
			while (--num >= 0)
			{
				if (num2 > 214748364)
				{
					return false;
				}
				num2 *= 10;
				if (*digitsPointer != 0)
				{
					num2 += (int)(*(digitsPointer++) - 48);
				}
			}
			if (number.IsNegative)
			{
				num2 = -num2;
				if (num2 > 0)
				{
					return false;
				}
			}
			else if (num2 < 0)
			{
				return false;
			}
			value = num2;
			return true;
		}

		// Token: 0x060011AA RID: 4522 RVA: 0x000E211C File Offset: 0x000E131C
		private unsafe static bool TryNumberToInt64(ref Number.NumberBuffer number, ref long value)
		{
			int num = number.Scale;
			if (num > 19 || num < number.DigitsCount)
			{
				return false;
			}
			byte* digitsPointer = number.GetDigitsPointer();
			long num2 = 0L;
			while (--num >= 0)
			{
				if (num2 > 922337203685477580L)
				{
					return false;
				}
				num2 *= 10L;
				if (*digitsPointer != 0)
				{
					num2 += (long)(*(digitsPointer++) - 48);
				}
			}
			if (number.IsNegative)
			{
				num2 = -num2;
				if (num2 > 0L)
				{
					return false;
				}
			}
			else if (num2 < 0L)
			{
				return false;
			}
			value = num2;
			return true;
		}

		// Token: 0x060011AB RID: 4523 RVA: 0x000E2198 File Offset: 0x000E1398
		private unsafe static bool TryNumberToUInt32(ref Number.NumberBuffer number, ref uint value)
		{
			int num = number.Scale;
			if (num > 10 || num < number.DigitsCount || number.IsNegative)
			{
				return false;
			}
			byte* digitsPointer = number.GetDigitsPointer();
			uint num2 = 0U;
			while (--num >= 0)
			{
				if (num2 > 429496729U)
				{
					return false;
				}
				num2 *= 10U;
				if (*digitsPointer != 0)
				{
					uint num3 = num2 + (uint)(*(digitsPointer++) - 48);
					if (num3 < num2)
					{
						return false;
					}
					num2 = num3;
				}
			}
			value = num2;
			return true;
		}

		// Token: 0x060011AC RID: 4524 RVA: 0x000E2204 File Offset: 0x000E1404
		private unsafe static bool TryNumberToUInt64(ref Number.NumberBuffer number, ref ulong value)
		{
			int num = number.Scale;
			if (num > 20 || num < number.DigitsCount || number.IsNegative)
			{
				return false;
			}
			byte* digitsPointer = number.GetDigitsPointer();
			ulong num2 = 0UL;
			while (--num >= 0)
			{
				if (num2 > 1844674407370955161UL)
				{
					return false;
				}
				num2 *= 10UL;
				if (*digitsPointer != 0)
				{
					ulong num3 = num2 + (ulong)((long)(*(digitsPointer++) - 48));
					if (num3 < num2)
					{
						return false;
					}
					num2 = num3;
				}
			}
			value = num2;
			return true;
		}

		// Token: 0x060011AD RID: 4525 RVA: 0x000E2278 File Offset: 0x000E1478
		internal static int ParseInt32(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info)
		{
			int result;
			Number.ParsingStatus parsingStatus = Number.TryParseInt32(value, styles, info, out result);
			if (parsingStatus != Number.ParsingStatus.OK)
			{
				Number.ThrowOverflowOrFormatException(parsingStatus, TypeCode.Int32);
			}
			return result;
		}

		// Token: 0x060011AE RID: 4526 RVA: 0x000E229C File Offset: 0x000E149C
		internal static long ParseInt64(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info)
		{
			long result;
			Number.ParsingStatus parsingStatus = Number.TryParseInt64(value, styles, info, out result);
			if (parsingStatus != Number.ParsingStatus.OK)
			{
				Number.ThrowOverflowOrFormatException(parsingStatus, TypeCode.Int64);
			}
			return result;
		}

		// Token: 0x060011AF RID: 4527 RVA: 0x000E22C0 File Offset: 0x000E14C0
		internal static uint ParseUInt32(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info)
		{
			uint result;
			Number.ParsingStatus parsingStatus = Number.TryParseUInt32(value, styles, info, out result);
			if (parsingStatus != Number.ParsingStatus.OK)
			{
				Number.ThrowOverflowOrFormatException(parsingStatus, TypeCode.UInt32);
			}
			return result;
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x000E22E4 File Offset: 0x000E14E4
		internal static ulong ParseUInt64(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info)
		{
			ulong result;
			Number.ParsingStatus parsingStatus = Number.TryParseUInt64(value, styles, info, out result);
			if (parsingStatus != Number.ParsingStatus.OK)
			{
				Number.ThrowOverflowOrFormatException(parsingStatus, TypeCode.UInt64);
			}
			return result;
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x000E2308 File Offset: 0x000E1508
		private unsafe static bool TryParseNumber(ref char* str, char* strEnd, NumberStyles styles, ref Number.NumberBuffer number, NumberFormatInfo info)
		{
			string text = null;
			bool flag = false;
			string value;
			string value2;
			if ((styles & NumberStyles.AllowCurrencySymbol) != NumberStyles.None)
			{
				text = info.CurrencySymbol;
				value = info.CurrencyDecimalSeparator;
				value2 = info.CurrencyGroupSeparator;
				flag = true;
			}
			else
			{
				value = info.NumberDecimalSeparator;
				value2 = info.NumberGroupSeparator;
			}
			int num = 0;
			char* ptr = str;
			char c = (ptr < strEnd) ? (*ptr) : '\0';
			for (;;)
			{
				if (!Number.IsWhite((int)c) || (styles & NumberStyles.AllowLeadingWhite) == NumberStyles.None || ((num & 1) != 0 && (num & 32) == 0 && info.NumberNegativePattern != 2))
				{
					char* ptr2;
					if ((styles & NumberStyles.AllowLeadingSign) != NumberStyles.None && (num & 1) == 0 && ((ptr2 = Number.MatchChars(ptr, strEnd, info.PositiveSign)) != null || ((ptr2 = Number.MatchChars(ptr, strEnd, info.NegativeSign)) != null && (number.IsNegative = true))))
					{
						num |= 1;
						ptr = ptr2 - 1;
					}
					else if (c == '(' && (styles & NumberStyles.AllowParentheses) != NumberStyles.None && (num & 1) == 0)
					{
						num |= 3;
						number.IsNegative = true;
					}
					else
					{
						if (text == null || (ptr2 = Number.MatchChars(ptr, strEnd, text)) == null)
						{
							break;
						}
						num |= 32;
						text = null;
						ptr = ptr2 - 1;
					}
				}
				c = ((++ptr < strEnd) ? (*ptr) : '\0');
			}
			int num2 = 0;
			int num3 = 0;
			int num4 = number.Digits.Length - 1;
			for (;;)
			{
				char* ptr2;
				if (Number.IsDigit((int)c))
				{
					num |= 4;
					if (c != '0' || (num & 8) != 0)
					{
						if (num2 < num4)
						{
							*number.Digits[num2++] = (byte)c;
							if (c != '0' || number.Kind != Number.NumberBufferKind.Integer)
							{
								num3 = num2;
							}
						}
						else if (c != '0')
						{
							number.HasNonZeroTail = true;
						}
						if ((num & 16) == 0)
						{
							number.Scale++;
						}
						num |= 8;
					}
					else if ((num & 16) != 0)
					{
						number.Scale--;
					}
				}
				else if ((styles & NumberStyles.AllowDecimalPoint) != NumberStyles.None && (num & 16) == 0 && ((ptr2 = Number.MatchChars(ptr, strEnd, value)) != null || (flag && (num & 32) == 0 && (ptr2 = Number.MatchChars(ptr, strEnd, info.NumberDecimalSeparator)) != null)))
				{
					num |= 16;
					ptr = ptr2 - 1;
				}
				else
				{
					if ((styles & NumberStyles.AllowThousands) == NumberStyles.None || (num & 4) == 0 || (num & 16) != 0 || ((ptr2 = Number.MatchChars(ptr, strEnd, value2)) == null && (!flag || (num & 32) != 0 || (ptr2 = Number.MatchChars(ptr, strEnd, info.NumberGroupSeparator)) == null)))
					{
						break;
					}
					ptr = ptr2 - 1;
				}
				c = ((++ptr < strEnd) ? (*ptr) : '\0');
			}
			bool flag2 = false;
			number.DigitsCount = num3;
			*number.Digits[num3] = 0;
			if ((num & 4) != 0)
			{
				if ((c == 'E' || c == 'e') && (styles & NumberStyles.AllowExponent) != NumberStyles.None)
				{
					char* ptr3 = ptr;
					c = ((++ptr < strEnd) ? (*ptr) : '\0');
					char* ptr2;
					if ((ptr2 = Number.MatchChars(ptr, strEnd, info._positiveSign)) != null)
					{
						c = (((ptr = ptr2) < strEnd) ? (*ptr) : '\0');
					}
					else if ((ptr2 = Number.MatchChars(ptr, strEnd, info._negativeSign)) != null)
					{
						c = (((ptr = ptr2) < strEnd) ? (*ptr) : '\0');
						flag2 = true;
					}
					if (Number.IsDigit((int)c))
					{
						int num5 = 0;
						do
						{
							num5 = num5 * 10 + (int)(c - '0');
							c = ((++ptr < strEnd) ? (*ptr) : '\0');
							if (num5 > 1000)
							{
								num5 = 9999;
								while (Number.IsDigit((int)c))
								{
									c = ((++ptr < strEnd) ? (*ptr) : '\0');
								}
							}
						}
						while (Number.IsDigit((int)c));
						if (flag2)
						{
							num5 = -num5;
						}
						number.Scale += num5;
					}
					else
					{
						ptr = ptr3;
						c = ((ptr < strEnd) ? (*ptr) : '\0');
					}
				}
				for (;;)
				{
					if (!Number.IsWhite((int)c) || (styles & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
					{
						char* ptr2;
						if ((styles & NumberStyles.AllowTrailingSign) != NumberStyles.None && (num & 1) == 0 && ((ptr2 = Number.MatchChars(ptr, strEnd, info.PositiveSign)) != null || ((ptr2 = Number.MatchChars(ptr, strEnd, info.NegativeSign)) != null && (number.IsNegative = true))))
						{
							num |= 1;
							ptr = ptr2 - 1;
						}
						else if (c == ')' && (num & 2) != 0)
						{
							num &= -3;
						}
						else
						{
							if (text == null || (ptr2 = Number.MatchChars(ptr, strEnd, text)) == null)
							{
								break;
							}
							text = null;
							ptr = ptr2 - 1;
						}
					}
					c = ((++ptr < strEnd) ? (*ptr) : '\0');
				}
				if ((num & 2) == 0)
				{
					if ((num & 8) == 0)
					{
						if (number.Kind != Number.NumberBufferKind.Decimal)
						{
							number.Scale = 0;
						}
						if (number.Kind == Number.NumberBufferKind.Integer && (num & 16) == 0)
						{
							number.IsNegative = false;
						}
					}
					str = ptr;
					return true;
				}
			}
			str = ptr;
			return false;
		}

		// Token: 0x060011B2 RID: 4530 RVA: 0x000E27A8 File Offset: 0x000E19A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Number.ParsingStatus TryParseInt32(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out int result)
		{
			if ((styles & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign)) == NumberStyles.None)
			{
				return Number.TryParseInt32IntegerStyle(value, styles, info, out result);
			}
			if ((styles & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				result = 0;
				return Number.TryParseUInt32HexNumberStyle(value, styles, Unsafe.As<int, uint>(ref result));
			}
			return Number.TryParseInt32Number(value, styles, info, out result);
		}

		// Token: 0x060011B3 RID: 4531 RVA: 0x000E27E0 File Offset: 0x000E19E0
		private unsafe static Number.ParsingStatus TryParseInt32Number(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out int result)
		{
			result = 0;
			byte* digits = stackalloc byte[(UIntPtr)11];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 11);
			if (!Number.TryStringToNumber(value, styles, ref numberBuffer, info))
			{
				return Number.ParsingStatus.Failed;
			}
			if (!Number.TryNumberToInt32(ref numberBuffer, ref result))
			{
				return Number.ParsingStatus.Overflow;
			}
			return Number.ParsingStatus.OK;
		}

		// Token: 0x060011B4 RID: 4532 RVA: 0x000E281C File Offset: 0x000E1A1C
		internal unsafe static Number.ParsingStatus TryParseInt32IntegerStyle(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out int result)
		{
			if (value.IsEmpty)
			{
				goto IL_25A;
			}
			int num = 0;
			int num2 = (int)(*value[0]);
			if ((styles & NumberStyles.AllowLeadingWhite) != NumberStyles.None && Number.IsWhite(num2))
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_25A;
					}
					num2 = (int)(*value[num]);
				}
				while (Number.IsWhite(num2));
			}
			int num3 = 1;
			if ((styles & NumberStyles.AllowLeadingSign) != NumberStyles.None)
			{
				if (info.HasInvariantNumberSigns)
				{
					if (num2 == 45)
					{
						num3 = -1;
						num++;
						if (num >= value.Length)
						{
							goto IL_25A;
						}
						num2 = (int)(*value[num]);
					}
					else if (num2 == 43)
					{
						num++;
						if (num >= value.Length)
						{
							goto IL_25A;
						}
						num2 = (int)(*value[num]);
					}
				}
				else
				{
					value = value.Slice(num);
					num = 0;
					string positiveSign = info.PositiveSign;
					string negativeSign = info.NegativeSign;
					if (!string.IsNullOrEmpty(positiveSign) && value.StartsWith(positiveSign))
					{
						num += positiveSign.Length;
						if (num >= value.Length)
						{
							goto IL_25A;
						}
						num2 = (int)(*value[num]);
					}
					else if (!string.IsNullOrEmpty(negativeSign) && value.StartsWith(negativeSign))
					{
						num3 = -1;
						num += negativeSign.Length;
						if (num >= value.Length)
						{
							goto IL_25A;
						}
						num2 = (int)(*value[num]);
					}
				}
			}
			bool flag = false;
			int num4 = 0;
			if (!Number.IsDigit(num2))
			{
				goto IL_25A;
			}
			if (num2 == 48)
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_24E;
					}
					num2 = (int)(*value[num]);
				}
				while (num2 == 48);
				if (!Number.IsDigit(num2))
				{
					goto IL_26A;
				}
			}
			num4 = num2 - 48;
			num++;
			for (int i = 0; i < 8; i++)
			{
				if (num >= value.Length)
				{
					goto IL_24E;
				}
				num2 = (int)(*value[num]);
				if (!Number.IsDigit(num2))
				{
					goto IL_26A;
				}
				num++;
				num4 = 10 * num4 + num2 - 48;
			}
			if (num >= value.Length)
			{
				goto IL_24E;
			}
			num2 = (int)(*value[num]);
			if (!Number.IsDigit(num2))
			{
				goto IL_26A;
			}
			num++;
			flag = (num4 > 214748364);
			num4 = num4 * 10 + num2 - 48;
			flag |= (num4 > (int)(2147483647U + ((uint)num3 >> 31)));
			if (num < value.Length)
			{
				num2 = (int)(*value[num]);
				while (Number.IsDigit(num2))
				{
					flag = true;
					num++;
					if (num >= value.Length)
					{
						goto IL_262;
					}
					num2 = (int)(*value[num]);
				}
				goto IL_26A;
			}
			IL_24B:
			if (flag)
			{
				goto IL_262;
			}
			IL_24E:
			result = num4 * num3;
			return Number.ParsingStatus.OK;
			IL_262:
			result = 0;
			return Number.ParsingStatus.Overflow;
			IL_26A:
			if (Number.IsWhite(num2))
			{
				if ((styles & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
				{
					goto IL_25A;
				}
				num++;
				while (num < value.Length && Number.IsWhite((int)(*value[num])))
				{
					num++;
				}
				if (num >= value.Length)
				{
					goto IL_24B;
				}
			}
			if (!Number.TrailingZeros(value, num))
			{
				goto IL_25A;
			}
			goto IL_24B;
			IL_25A:
			result = 0;
			return Number.ParsingStatus.Failed;
		}

		// Token: 0x060011B5 RID: 4533 RVA: 0x000E2AD8 File Offset: 0x000E1CD8
		internal unsafe static Number.ParsingStatus TryParseInt64IntegerStyle(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out long result)
		{
			if (value.IsEmpty)
			{
				goto IL_270;
			}
			int num = 0;
			int num2 = (int)(*value[0]);
			if ((styles & NumberStyles.AllowLeadingWhite) != NumberStyles.None && Number.IsWhite(num2))
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_270;
					}
					num2 = (int)(*value[num]);
				}
				while (Number.IsWhite(num2));
			}
			int num3 = 1;
			if ((styles & NumberStyles.AllowLeadingSign) != NumberStyles.None)
			{
				if (info.HasInvariantNumberSigns)
				{
					if (num2 == 45)
					{
						num3 = -1;
						num++;
						if (num >= value.Length)
						{
							goto IL_270;
						}
						num2 = (int)(*value[num]);
					}
					else if (num2 == 43)
					{
						num++;
						if (num >= value.Length)
						{
							goto IL_270;
						}
						num2 = (int)(*value[num]);
					}
				}
				else
				{
					value = value.Slice(num);
					num = 0;
					string positiveSign = info.PositiveSign;
					string negativeSign = info.NegativeSign;
					if (!string.IsNullOrEmpty(positiveSign) && value.StartsWith(positiveSign))
					{
						num += positiveSign.Length;
						if (num >= value.Length)
						{
							goto IL_270;
						}
						num2 = (int)(*value[num]);
					}
					else if (!string.IsNullOrEmpty(negativeSign) && value.StartsWith(negativeSign))
					{
						num3 = -1;
						num += negativeSign.Length;
						if (num >= value.Length)
						{
							goto IL_270;
						}
						num2 = (int)(*value[num]);
					}
				}
			}
			bool flag = false;
			long num4 = 0L;
			if (!Number.IsDigit(num2))
			{
				goto IL_270;
			}
			if (num2 == 48)
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_263;
					}
					num2 = (int)(*value[num]);
				}
				while (num2 == 48);
				if (!Number.IsDigit(num2))
				{
					goto IL_282;
				}
			}
			num4 = (long)(num2 - 48);
			num++;
			for (int i = 0; i < 17; i++)
			{
				if (num >= value.Length)
				{
					goto IL_263;
				}
				num2 = (int)(*value[num]);
				if (!Number.IsDigit(num2))
				{
					goto IL_282;
				}
				num++;
				num4 = 10L * num4 + (long)num2 - 48L;
			}
			if (num >= value.Length)
			{
				goto IL_263;
			}
			num2 = (int)(*value[num]);
			if (!Number.IsDigit(num2))
			{
				goto IL_282;
			}
			num++;
			flag = (num4 > 922337203685477580L);
			num4 = num4 * 10L + (long)num2 - 48L;
			flag |= (num4 > (long)(9223372036854775807UL + (ulong)((uint)num3 >> 31)));
			if (num < value.Length)
			{
				num2 = (int)(*value[num]);
				while (Number.IsDigit(num2))
				{
					flag = true;
					num++;
					if (num >= value.Length)
					{
						goto IL_279;
					}
					num2 = (int)(*value[num]);
				}
				goto IL_282;
			}
			IL_260:
			if (flag)
			{
				goto IL_279;
			}
			IL_263:
			result = num4 * (long)num3;
			return Number.ParsingStatus.OK;
			IL_279:
			result = 0L;
			return Number.ParsingStatus.Overflow;
			IL_282:
			if (Number.IsWhite(num2))
			{
				if ((styles & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
				{
					goto IL_270;
				}
				num++;
				while (num < value.Length && Number.IsWhite((int)(*value[num])))
				{
					num++;
				}
				if (num >= value.Length)
				{
					goto IL_260;
				}
			}
			if (!Number.TrailingZeros(value, num))
			{
				goto IL_270;
			}
			goto IL_260;
			IL_270:
			result = 0L;
			return Number.ParsingStatus.Failed;
		}

		// Token: 0x060011B6 RID: 4534 RVA: 0x000E2DAC File Offset: 0x000E1FAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Number.ParsingStatus TryParseInt64(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out long result)
		{
			if ((styles & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign)) == NumberStyles.None)
			{
				return Number.TryParseInt64IntegerStyle(value, styles, info, out result);
			}
			if ((styles & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				result = 0L;
				return Number.TryParseUInt64HexNumberStyle(value, styles, Unsafe.As<long, ulong>(ref result));
			}
			return Number.TryParseInt64Number(value, styles, info, out result);
		}

		// Token: 0x060011B7 RID: 4535 RVA: 0x000E2DE4 File Offset: 0x000E1FE4
		private unsafe static Number.ParsingStatus TryParseInt64Number(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out long result)
		{
			result = 0L;
			byte* digits = stackalloc byte[(UIntPtr)20];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 20);
			if (!Number.TryStringToNumber(value, styles, ref numberBuffer, info))
			{
				return Number.ParsingStatus.Failed;
			}
			if (!Number.TryNumberToInt64(ref numberBuffer, ref result))
			{
				return Number.ParsingStatus.Overflow;
			}
			return Number.ParsingStatus.OK;
		}

		// Token: 0x060011B8 RID: 4536 RVA: 0x000E2E21 File Offset: 0x000E2021
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Number.ParsingStatus TryParseUInt32(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out uint result)
		{
			if ((styles & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign)) == NumberStyles.None)
			{
				return Number.TryParseUInt32IntegerStyle(value, styles, info, out result);
			}
			if ((styles & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				return Number.TryParseUInt32HexNumberStyle(value, styles, out result);
			}
			return Number.TryParseUInt32Number(value, styles, info, out result);
		}

		// Token: 0x060011B9 RID: 4537 RVA: 0x000E2E50 File Offset: 0x000E2050
		private unsafe static Number.ParsingStatus TryParseUInt32Number(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out uint result)
		{
			result = 0U;
			byte* digits = stackalloc byte[(UIntPtr)11];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 11);
			if (!Number.TryStringToNumber(value, styles, ref numberBuffer, info))
			{
				return Number.ParsingStatus.Failed;
			}
			if (!Number.TryNumberToUInt32(ref numberBuffer, ref result))
			{
				return Number.ParsingStatus.Overflow;
			}
			return Number.ParsingStatus.OK;
		}

		// Token: 0x060011BA RID: 4538 RVA: 0x000E2E8C File Offset: 0x000E208C
		internal unsafe static Number.ParsingStatus TryParseUInt32IntegerStyle(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out uint result)
		{
			if (value.IsEmpty)
			{
				goto IL_252;
			}
			int num = 0;
			int num2 = (int)(*value[0]);
			if ((styles & NumberStyles.AllowLeadingWhite) != NumberStyles.None && Number.IsWhite(num2))
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_252;
					}
					num2 = (int)(*value[num]);
				}
				while (Number.IsWhite(num2));
			}
			bool flag = false;
			if ((styles & NumberStyles.AllowLeadingSign) != NumberStyles.None)
			{
				if (info.HasInvariantNumberSigns)
				{
					if (num2 == 43)
					{
						num++;
						if (num >= value.Length)
						{
							goto IL_252;
						}
						num2 = (int)(*value[num]);
					}
					else if (num2 == 45)
					{
						flag = true;
						num++;
						if (num >= value.Length)
						{
							goto IL_252;
						}
						num2 = (int)(*value[num]);
					}
				}
				else
				{
					value = value.Slice(num);
					num = 0;
					string positiveSign = info.PositiveSign;
					string negativeSign = info.NegativeSign;
					if (!string.IsNullOrEmpty(positiveSign) && value.StartsWith(positiveSign))
					{
						num += positiveSign.Length;
						if (num >= value.Length)
						{
							goto IL_252;
						}
						num2 = (int)(*value[num]);
					}
					else if (!string.IsNullOrEmpty(negativeSign) && value.StartsWith(negativeSign))
					{
						flag = true;
						num += negativeSign.Length;
						if (num >= value.Length)
						{
							goto IL_252;
						}
						num2 = (int)(*value[num]);
					}
				}
			}
			int num3 = 0;
			if (!Number.IsDigit(num2))
			{
				goto IL_252;
			}
			if (num2 == 48)
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_249;
					}
					num2 = (int)(*value[num]);
				}
				while (num2 == 48);
				if (!Number.IsDigit(num2))
				{
					flag = false;
					goto IL_264;
				}
			}
			num3 = num2 - 48;
			num++;
			for (int i = 0; i < 8; i++)
			{
				if (num >= value.Length)
				{
					goto IL_246;
				}
				num2 = (int)(*value[num]);
				if (!Number.IsDigit(num2))
				{
					goto IL_264;
				}
				num++;
				num3 = 10 * num3 + num2 - 48;
			}
			if (num < value.Length)
			{
				num2 = (int)(*value[num]);
				if (!Number.IsDigit(num2))
				{
					goto IL_264;
				}
				num++;
				flag |= (num3 > 429496729 || (num3 == 429496729 && num2 > 53));
				num3 = num3 * 10 + num2 - 48;
				if (num < value.Length)
				{
					num2 = (int)(*value[num]);
					while (Number.IsDigit(num2))
					{
						flag = true;
						num++;
						if (num >= value.Length)
						{
							goto IL_25A;
						}
						num2 = (int)(*value[num]);
					}
					goto IL_264;
				}
			}
			IL_246:
			if (flag)
			{
				goto IL_25A;
			}
			IL_249:
			result = (uint)num3;
			return Number.ParsingStatus.OK;
			IL_25A:
			result = 0U;
			return Number.ParsingStatus.Overflow;
			IL_264:
			if (Number.IsWhite(num2))
			{
				if ((styles & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
				{
					goto IL_252;
				}
				num++;
				while (num < value.Length && Number.IsWhite((int)(*value[num])))
				{
					num++;
				}
				if (num >= value.Length)
				{
					goto IL_246;
				}
			}
			if (!Number.TrailingZeros(value, num))
			{
				goto IL_252;
			}
			goto IL_246;
			IL_252:
			result = 0U;
			return Number.ParsingStatus.Failed;
		}

		// Token: 0x060011BB RID: 4539 RVA: 0x000E3144 File Offset: 0x000E2344
		private unsafe static Number.ParsingStatus TryParseUInt32HexNumberStyle(ReadOnlySpan<char> value, NumberStyles styles, out uint result)
		{
			if (value.IsEmpty)
			{
				goto IL_11F;
			}
			int num = 0;
			int num2 = (int)(*value[0]);
			if ((styles & NumberStyles.AllowLeadingWhite) != NumberStyles.None && Number.IsWhite(num2))
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_11F;
					}
					num2 = (int)(*value[num]);
				}
				while (Number.IsWhite(num2));
			}
			bool flag = false;
			uint num3 = 0U;
			if (!HexConverter.IsHexChar(num2))
			{
				goto IL_11F;
			}
			if (num2 == 48)
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_116;
					}
					num2 = (int)(*value[num]);
				}
				while (num2 == 48);
				if (!HexConverter.IsHexChar(num2))
				{
					goto IL_12F;
				}
			}
			num3 = (uint)HexConverter.FromChar(num2);
			num++;
			for (int i = 0; i < 7; i++)
			{
				if (num >= value.Length)
				{
					goto IL_116;
				}
				num2 = (int)(*value[num]);
				uint num4 = (uint)HexConverter.FromChar(num2);
				if (num4 == 255U)
				{
					goto IL_12F;
				}
				num++;
				num3 = 16U * num3 + num4;
			}
			if (num < value.Length)
			{
				num2 = (int)(*value[num]);
				if (HexConverter.IsHexChar(num2))
				{
					do
					{
						num++;
						if (num >= value.Length)
						{
							goto IL_127;
						}
						num2 = (int)(*value[num]);
					}
					while (HexConverter.IsHexChar(num2));
					flag = true;
				}
				goto IL_12F;
			}
			IL_116:
			result = num3;
			return Number.ParsingStatus.OK;
			IL_127:
			result = 0U;
			return Number.ParsingStatus.Overflow;
			IL_12F:
			if (!Number.IsWhite(num2))
			{
				goto IL_16A;
			}
			if ((styles & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
			{
				goto IL_11F;
			}
			num++;
			while (num < value.Length && Number.IsWhite((int)(*value[num])))
			{
				num++;
			}
			if (num < value.Length)
			{
				goto IL_16A;
			}
			IL_113:
			if (!flag)
			{
				goto IL_116;
			}
			goto IL_127;
			IL_16A:
			if (!Number.TrailingZeros(value, num))
			{
				goto IL_11F;
			}
			goto IL_113;
			IL_11F:
			result = 0U;
			return Number.ParsingStatus.Failed;
		}

		// Token: 0x060011BC RID: 4540 RVA: 0x000E32C5 File Offset: 0x000E24C5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Number.ParsingStatus TryParseUInt64(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out ulong result)
		{
			if ((styles & ~(NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign)) == NumberStyles.None)
			{
				return Number.TryParseUInt64IntegerStyle(value, styles, info, out result);
			}
			if ((styles & NumberStyles.AllowHexSpecifier) != NumberStyles.None)
			{
				return Number.TryParseUInt64HexNumberStyle(value, styles, out result);
			}
			return Number.TryParseUInt64Number(value, styles, info, out result);
		}

		// Token: 0x060011BD RID: 4541 RVA: 0x000E32F4 File Offset: 0x000E24F4
		private unsafe static Number.ParsingStatus TryParseUInt64Number(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out ulong result)
		{
			result = 0UL;
			byte* digits = stackalloc byte[(UIntPtr)21];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 21);
			if (!Number.TryStringToNumber(value, styles, ref numberBuffer, info))
			{
				return Number.ParsingStatus.Failed;
			}
			if (!Number.TryNumberToUInt64(ref numberBuffer, ref result))
			{
				return Number.ParsingStatus.Overflow;
			}
			return Number.ParsingStatus.OK;
		}

		// Token: 0x060011BE RID: 4542 RVA: 0x000E3334 File Offset: 0x000E2534
		internal unsafe static Number.ParsingStatus TryParseUInt64IntegerStyle(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out ulong result)
		{
			if (value.IsEmpty)
			{
				goto IL_266;
			}
			int num = 0;
			int num2 = (int)(*value[0]);
			if ((styles & NumberStyles.AllowLeadingWhite) != NumberStyles.None && Number.IsWhite(num2))
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_266;
					}
					num2 = (int)(*value[num]);
				}
				while (Number.IsWhite(num2));
			}
			bool flag = false;
			if ((styles & NumberStyles.AllowLeadingSign) != NumberStyles.None)
			{
				if (info.HasInvariantNumberSigns)
				{
					if (num2 == 43)
					{
						num++;
						if (num >= value.Length)
						{
							goto IL_266;
						}
						num2 = (int)(*value[num]);
					}
					else if (num2 == 45)
					{
						flag = true;
						num++;
						if (num >= value.Length)
						{
							goto IL_266;
						}
						num2 = (int)(*value[num]);
					}
				}
				else
				{
					value = value.Slice(num);
					num = 0;
					string positiveSign = info.PositiveSign;
					string negativeSign = info.NegativeSign;
					if (!string.IsNullOrEmpty(positiveSign) && value.StartsWith(positiveSign))
					{
						num += positiveSign.Length;
						if (num >= value.Length)
						{
							goto IL_266;
						}
						num2 = (int)(*value[num]);
					}
					else if (!string.IsNullOrEmpty(negativeSign) && value.StartsWith(negativeSign))
					{
						flag = true;
						num += negativeSign.Length;
						if (num >= value.Length)
						{
							goto IL_266;
						}
						num2 = (int)(*value[num]);
					}
				}
			}
			long num3 = 0L;
			if (!Number.IsDigit(num2))
			{
				goto IL_266;
			}
			if (num2 == 48)
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_25D;
					}
					num2 = (int)(*value[num]);
				}
				while (num2 == 48);
				if (!Number.IsDigit(num2))
				{
					flag = false;
					goto IL_27A;
				}
			}
			num3 = (long)(num2 - 48);
			num++;
			for (int i = 0; i < 18; i++)
			{
				if (num >= value.Length)
				{
					goto IL_25A;
				}
				num2 = (int)(*value[num]);
				if (!Number.IsDigit(num2))
				{
					goto IL_27A;
				}
				num++;
				num3 = 10L * num3 + (long)num2 - 48L;
			}
			if (num < value.Length)
			{
				num2 = (int)(*value[num]);
				if (!Number.IsDigit(num2))
				{
					goto IL_27A;
				}
				num++;
				flag |= (num3 > 1844674407370955161L || (num3 == 1844674407370955161L && num2 > 53));
				num3 = num3 * 10L + (long)num2 - 48L;
				if (num < value.Length)
				{
					num2 = (int)(*value[num]);
					while (Number.IsDigit(num2))
					{
						flag = true;
						num++;
						if (num >= value.Length)
						{
							goto IL_26F;
						}
						num2 = (int)(*value[num]);
					}
					goto IL_27A;
				}
			}
			IL_25A:
			if (flag)
			{
				goto IL_26F;
			}
			IL_25D:
			result = (ulong)num3;
			return Number.ParsingStatus.OK;
			IL_26F:
			result = 0UL;
			return Number.ParsingStatus.Overflow;
			IL_27A:
			if (Number.IsWhite(num2))
			{
				if ((styles & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
				{
					goto IL_266;
				}
				num++;
				while (num < value.Length && Number.IsWhite((int)(*value[num])))
				{
					num++;
				}
				if (num >= value.Length)
				{
					goto IL_25A;
				}
			}
			if (!Number.TrailingZeros(value, num))
			{
				goto IL_266;
			}
			goto IL_25A;
			IL_266:
			result = 0UL;
			return Number.ParsingStatus.Failed;
		}

		// Token: 0x060011BF RID: 4543 RVA: 0x000E3600 File Offset: 0x000E2800
		private unsafe static Number.ParsingStatus TryParseUInt64HexNumberStyle(ReadOnlySpan<char> value, NumberStyles styles, out ulong result)
		{
			if (value.IsEmpty)
			{
				goto IL_124;
			}
			int num = 0;
			int num2 = (int)(*value[0]);
			if ((styles & NumberStyles.AllowLeadingWhite) != NumberStyles.None && Number.IsWhite(num2))
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_124;
					}
					num2 = (int)(*value[num]);
				}
				while (Number.IsWhite(num2));
			}
			bool flag = false;
			ulong num3 = 0UL;
			if (!HexConverter.IsHexChar(num2))
			{
				goto IL_124;
			}
			if (num2 == 48)
			{
				do
				{
					num++;
					if (num >= value.Length)
					{
						goto IL_11B;
					}
					num2 = (int)(*value[num]);
				}
				while (num2 == 48);
				if (!HexConverter.IsHexChar(num2))
				{
					goto IL_136;
				}
			}
			num3 = (ulong)HexConverter.FromChar(num2);
			num++;
			for (int i = 0; i < 15; i++)
			{
				if (num >= value.Length)
				{
					goto IL_11B;
				}
				num2 = (int)(*value[num]);
				uint num4 = (uint)HexConverter.FromChar(num2);
				if (num4 == 255U)
				{
					goto IL_136;
				}
				num++;
				num3 = 16UL * num3 + (ulong)num4;
			}
			if (num < value.Length)
			{
				num2 = (int)(*value[num]);
				if (HexConverter.IsHexChar(num2))
				{
					do
					{
						num++;
						if (num >= value.Length)
						{
							goto IL_12D;
						}
						num2 = (int)(*value[num]);
					}
					while (HexConverter.IsHexChar(num2));
					flag = true;
				}
				goto IL_136;
			}
			IL_11B:
			result = num3;
			return Number.ParsingStatus.OK;
			IL_12D:
			result = 0UL;
			return Number.ParsingStatus.Overflow;
			IL_136:
			if (!Number.IsWhite(num2))
			{
				goto IL_171;
			}
			if ((styles & NumberStyles.AllowTrailingWhite) == NumberStyles.None)
			{
				goto IL_124;
			}
			num++;
			while (num < value.Length && Number.IsWhite((int)(*value[num])))
			{
				num++;
			}
			if (num < value.Length)
			{
				goto IL_171;
			}
			IL_118:
			if (!flag)
			{
				goto IL_11B;
			}
			goto IL_12D;
			IL_171:
			if (!Number.TrailingZeros(value, num))
			{
				goto IL_124;
			}
			goto IL_118;
			IL_124:
			result = 0UL;
			return Number.ParsingStatus.Failed;
		}

		// Token: 0x060011C0 RID: 4544 RVA: 0x000E3788 File Offset: 0x000E2988
		internal static decimal ParseDecimal(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info)
		{
			decimal result;
			Number.ParsingStatus parsingStatus = Number.TryParseDecimal(value, styles, info, out result);
			if (parsingStatus != Number.ParsingStatus.OK)
			{
				Number.ThrowOverflowOrFormatException(parsingStatus, TypeCode.Decimal);
			}
			return result;
		}

		// Token: 0x060011C1 RID: 4545 RVA: 0x000E37AC File Offset: 0x000E29AC
		internal unsafe static bool TryNumberToDecimal(ref Number.NumberBuffer number, ref decimal value)
		{
			byte* ptr = number.GetDigitsPointer();
			int i = number.Scale;
			bool isNegative = number.IsNegative;
			uint num = (uint)(*ptr);
			if (num == 0U)
			{
				value = new decimal(0, 0, 0, isNegative, (byte)Math.Clamp(-i, 0, 28));
				return true;
			}
			if (i > 29)
			{
				return false;
			}
			ulong num2 = 0UL;
			while (i > -28)
			{
				i--;
				num2 *= 10UL;
				num2 += (ulong)(num - 48U);
				num = (uint)(*(++ptr));
				if (num2 >= 1844674407370955161UL)
				{
					break;
				}
				if (num == 0U)
				{
					while (i > 0)
					{
						i--;
						num2 *= 10UL;
						if (num2 >= 1844674407370955161UL)
						{
							break;
						}
					}
					break;
				}
			}
			uint num3 = 0U;
			while ((i > 0 || (num != 0U && i > -28)) && (num3 < 429496729U || (num3 == 429496729U && (num2 < 11068046444225730969UL || (num2 == 11068046444225730969UL && num <= 53U)))))
			{
				ulong num4 = (ulong)((uint)num2) * 10UL;
				ulong num5 = (ulong)((uint)(num2 >> 32)) * 10UL + (num4 >> 32);
				num2 = (ulong)((uint)num4) + (num5 << 32);
				num3 = (uint)(num5 >> 32) + num3 * 10U;
				if (num != 0U)
				{
					num -= 48U;
					num2 += (ulong)num;
					if (num2 < (ulong)num)
					{
						num3 += 1U;
					}
					num = (uint)(*(++ptr));
				}
				i--;
			}
			if (num >= 53U)
			{
				if (num == 53U && (num2 & 1UL) == 0UL)
				{
					num = (uint)(*(++ptr));
					bool flag = !number.HasNonZeroTail;
					while (num > 0U && flag)
					{
						flag &= (num == 48U);
						num = (uint)(*(++ptr));
					}
					if (flag)
					{
						goto IL_1A8;
					}
				}
				if ((num2 += 1UL) == 0UL && (num3 += 1U) == 0U)
				{
					num2 = 11068046444225730970UL;
					num3 = 429496729U;
					i++;
				}
			}
			IL_1A8:
			if (i > 0)
			{
				return false;
			}
			if (i <= -29)
			{
				value = new decimal(0, 0, 0, isNegative, 28);
			}
			else
			{
				value = new decimal((int)num2, (int)(num2 >> 32), (int)num3, isNegative, (byte)(-(byte)i));
			}
			return true;
		}

		// Token: 0x060011C2 RID: 4546 RVA: 0x000E399C File Offset: 0x000E2B9C
		internal static double ParseDouble(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info)
		{
			double result;
			if (!Number.TryParseDouble(value, styles, info, out result))
			{
				Number.ThrowOverflowOrFormatException(Number.ParsingStatus.Failed, TypeCode.Empty);
			}
			return result;
		}

		// Token: 0x060011C3 RID: 4547 RVA: 0x000E39C0 File Offset: 0x000E2BC0
		internal static float ParseSingle(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info)
		{
			float result;
			if (!Number.TryParseSingle(value, styles, info, out result))
			{
				Number.ThrowOverflowOrFormatException(Number.ParsingStatus.Failed, TypeCode.Empty);
			}
			return result;
		}

		// Token: 0x060011C4 RID: 4548 RVA: 0x000E39E4 File Offset: 0x000E2BE4
		internal static Half ParseHalf(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info)
		{
			Half result;
			if (!Number.TryParseHalf(value, styles, info, out result))
			{
				Number.ThrowOverflowOrFormatException(Number.ParsingStatus.Failed, TypeCode.Empty);
			}
			return result;
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x000E3A08 File Offset: 0x000E2C08
		internal unsafe static Number.ParsingStatus TryParseDecimal(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out decimal result)
		{
			byte* digits = stackalloc byte[(UIntPtr)31];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Decimal, digits, 31);
			result = 0m;
			if (!Number.TryStringToNumber(value, styles, ref numberBuffer, info))
			{
				return Number.ParsingStatus.Failed;
			}
			if (!Number.TryNumberToDecimal(ref numberBuffer, ref result))
			{
				return Number.ParsingStatus.Overflow;
			}
			return Number.ParsingStatus.OK;
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x000E3A48 File Offset: 0x000E2C48
		internal unsafe static bool TryParseDouble(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out double result)
		{
			byte* digits = stackalloc byte[(UIntPtr)769];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.FloatingPoint, digits, 769);
			if (!Number.TryStringToNumber(value, styles, ref numberBuffer, info))
			{
				ReadOnlySpan<char> span = value.Trim();
				if (span.EqualsOrdinalIgnoreCase(info.PositiveInfinitySymbol))
				{
					result = double.PositiveInfinity;
				}
				else if (span.EqualsOrdinalIgnoreCase(info.NegativeInfinitySymbol))
				{
					result = double.NegativeInfinity;
				}
				else if (span.EqualsOrdinalIgnoreCase(info.NaNSymbol))
				{
					result = double.NaN;
				}
				else if (span.StartsWith(info.PositiveSign, StringComparison.OrdinalIgnoreCase))
				{
					span = span.Slice(info.PositiveSign.Length);
					if (span.EqualsOrdinalIgnoreCase(info.PositiveInfinitySymbol))
					{
						result = double.PositiveInfinity;
					}
					else
					{
						if (!span.EqualsOrdinalIgnoreCase(info.NaNSymbol))
						{
							result = 0.0;
							return false;
						}
						result = double.NaN;
					}
				}
				else
				{
					if (!span.StartsWith(info.NegativeSign, StringComparison.OrdinalIgnoreCase) || !span.Slice(info.NegativeSign.Length).EqualsOrdinalIgnoreCase(info.NaNSymbol))
					{
						result = 0.0;
						return false;
					}
					result = double.NaN;
				}
			}
			else
			{
				result = Number.NumberToDouble(ref numberBuffer);
			}
			return true;
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x000E3BC0 File Offset: 0x000E2DC0
		internal unsafe static bool TryParseHalf(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out Half result)
		{
			byte* digits = stackalloc byte[(UIntPtr)21];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.FloatingPoint, digits, 21);
			if (!Number.TryStringToNumber(value, styles, ref numberBuffer, info))
			{
				ReadOnlySpan<char> span = value.Trim();
				if (span.EqualsOrdinalIgnoreCase(info.PositiveInfinitySymbol))
				{
					result = Half.PositiveInfinity;
				}
				else if (span.EqualsOrdinalIgnoreCase(info.NegativeInfinitySymbol))
				{
					result = Half.NegativeInfinity;
				}
				else if (span.EqualsOrdinalIgnoreCase(info.NaNSymbol))
				{
					result = Half.NaN;
				}
				else if (span.StartsWith(info.PositiveSign, StringComparison.OrdinalIgnoreCase))
				{
					span = span.Slice(info.PositiveSign.Length);
					if (!info.PositiveInfinitySymbol.StartsWith(info.PositiveSign, StringComparison.OrdinalIgnoreCase) && span.EqualsOrdinalIgnoreCase(info.PositiveInfinitySymbol))
					{
						result = Half.PositiveInfinity;
					}
					else
					{
						if (info.NaNSymbol.StartsWith(info.PositiveSign, StringComparison.OrdinalIgnoreCase) || !span.EqualsOrdinalIgnoreCase(info.NaNSymbol))
						{
							result = (Half)0f;
							return false;
						}
						result = Half.NaN;
					}
				}
				else
				{
					if (!span.StartsWith(info.NegativeSign, StringComparison.OrdinalIgnoreCase) || info.NaNSymbol.StartsWith(info.NegativeSign, StringComparison.OrdinalIgnoreCase) || !span.Slice(info.NegativeSign.Length).EqualsOrdinalIgnoreCase(info.NaNSymbol))
					{
						result = (Half)0f;
						return false;
					}
					result = Half.NaN;
				}
			}
			else
			{
				result = Number.NumberToHalf(ref numberBuffer);
			}
			return true;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x000E3D80 File Offset: 0x000E2F80
		internal unsafe static bool TryParseSingle(ReadOnlySpan<char> value, NumberStyles styles, NumberFormatInfo info, out float result)
		{
			byte* digits = stackalloc byte[(UIntPtr)114];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.FloatingPoint, digits, 114);
			if (!Number.TryStringToNumber(value, styles, ref numberBuffer, info))
			{
				ReadOnlySpan<char> span = value.Trim();
				if (span.EqualsOrdinalIgnoreCase(info.PositiveInfinitySymbol))
				{
					result = float.PositiveInfinity;
				}
				else if (span.EqualsOrdinalIgnoreCase(info.NegativeInfinitySymbol))
				{
					result = float.NegativeInfinity;
				}
				else if (span.EqualsOrdinalIgnoreCase(info.NaNSymbol))
				{
					result = float.NaN;
				}
				else if (span.StartsWith(info.PositiveSign, StringComparison.OrdinalIgnoreCase))
				{
					span = span.Slice(info.PositiveSign.Length);
					if (!info.PositiveInfinitySymbol.StartsWith(info.PositiveSign, StringComparison.OrdinalIgnoreCase) && span.EqualsOrdinalIgnoreCase(info.PositiveInfinitySymbol))
					{
						result = float.PositiveInfinity;
					}
					else
					{
						if (info.NaNSymbol.StartsWith(info.PositiveSign, StringComparison.OrdinalIgnoreCase) || !span.EqualsOrdinalIgnoreCase(info.NaNSymbol))
						{
							result = 0f;
							return false;
						}
						result = float.NaN;
					}
				}
				else
				{
					if (!span.StartsWith(info.NegativeSign, StringComparison.OrdinalIgnoreCase) || info.NaNSymbol.StartsWith(info.NegativeSign, StringComparison.OrdinalIgnoreCase) || !span.Slice(info.NegativeSign.Length).EqualsOrdinalIgnoreCase(info.NaNSymbol))
					{
						result = 0f;
						return false;
					}
					result = float.NaN;
				}
			}
			else
			{
				result = Number.NumberToSingle(ref numberBuffer);
			}
			return true;
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x000E3F0C File Offset: 0x000E310C
		internal unsafe static bool TryStringToNumber(ReadOnlySpan<char> value, NumberStyles styles, ref Number.NumberBuffer number, NumberFormatInfo info)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(value))
			{
				char* ptr = reference;
				char* ptr2 = ptr;
				if (!Number.TryParseNumber(ref ptr2, ptr2 + value.Length, styles, ref number, info) || ((int)((long)(ptr2 - ptr)) < value.Length && !Number.TrailingZeros(value, (int)((long)(ptr2 - ptr)))))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x000E3F64 File Offset: 0x000E3164
		private unsafe static bool TrailingZeros(ReadOnlySpan<char> value, int index)
		{
			for (int i = index; i < value.Length; i++)
			{
				if (*value[i] != 0)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x000E3F91 File Offset: 0x000E3191
		private static bool IsSpaceReplacingChar(char c)
		{
			return c == '\u00a0' || c == '\u202f';
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x000E3FA8 File Offset: 0x000E31A8
		private unsafe static char* MatchChars(char* p, char* pEnd, string value)
		{
			char* ptr;
			if (value == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = value.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			char* ptr4 = ptr3;
			if (*ptr4 != '\0')
			{
				do
				{
					char c = (p < pEnd) ? (*p) : '\0';
					if (c != *ptr4 && (!Number.IsSpaceReplacingChar(*ptr4) || c != ' '))
					{
						goto IL_43;
					}
					p++;
					ptr4++;
				}
				while (*ptr4 != '\0');
				return p;
			}
			IL_43:
			char* ptr2 = null;
			return null;
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x000E3FFD File Offset: 0x000E31FD
		private static bool IsWhite(int ch)
		{
			return ch == 32 || ch - 9 <= 4;
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x000D1D8D File Offset: 0x000D0F8D
		private static bool IsDigit(int ch)
		{
			return ch - 48 <= 9;
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000E400E File Offset: 0x000E320E
		[DoesNotReturn]
		internal static void ThrowOverflowOrFormatException(Number.ParsingStatus status, TypeCode type = TypeCode.Empty)
		{
			throw Number.GetException(status, type);
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000E4017 File Offset: 0x000E3217
		[DoesNotReturn]
		internal static void ThrowOverflowException(TypeCode type)
		{
			throw Number.GetException(Number.ParsingStatus.Overflow, type);
		}

		// Token: 0x060011D1 RID: 4561 RVA: 0x000E4020 File Offset: 0x000E3220
		private static Exception GetException(Number.ParsingStatus status, TypeCode type)
		{
			if (status == Number.ParsingStatus.Failed)
			{
				return new FormatException(SR.Format_InvalidString);
			}
			string message;
			switch (type)
			{
			case TypeCode.SByte:
				message = SR.Overflow_SByte;
				break;
			case TypeCode.Byte:
				message = SR.Overflow_Byte;
				break;
			case TypeCode.Int16:
				message = SR.Overflow_Int16;
				break;
			case TypeCode.UInt16:
				message = SR.Overflow_UInt16;
				break;
			case TypeCode.Int32:
				message = SR.Overflow_Int32;
				break;
			case TypeCode.UInt32:
				message = SR.Overflow_UInt32;
				break;
			case TypeCode.Int64:
				message = SR.Overflow_Int64;
				break;
			case TypeCode.UInt64:
				message = SR.Overflow_UInt64;
				break;
			default:
				message = SR.Overflow_Decimal;
				break;
			}
			return new OverflowException(message);
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000E40B4 File Offset: 0x000E32B4
		internal static double NumberToDouble(ref Number.NumberBuffer number)
		{
			double num;
			if (number.DigitsCount == 0 || number.Scale < -324)
			{
				num = 0.0;
			}
			else if (number.Scale > 309)
			{
				num = double.PositiveInfinity;
			}
			else
			{
				ulong value = Number.NumberToFloatingPointBits(ref number, Number.FloatingPointInfo.Double);
				num = BitConverter.Int64BitsToDouble((long)value);
			}
			if (!number.IsNegative)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000E411C File Offset: 0x000E331C
		internal static Half NumberToHalf(ref Number.NumberBuffer number)
		{
			Half half;
			if (number.DigitsCount == 0 || number.Scale < -8)
			{
				half = default(Half);
			}
			else if (number.Scale > 5)
			{
				half = Half.PositiveInfinity;
			}
			else
			{
				ushort value = (ushort)Number.NumberToFloatingPointBits(ref number, Number.FloatingPointInfo.Half);
				half = new Half(value);
			}
			if (!number.IsNegative)
			{
				return half;
			}
			return Half.Negate(half);
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000E417C File Offset: 0x000E337C
		internal static float NumberToSingle(ref Number.NumberBuffer number)
		{
			float num;
			if (number.DigitsCount == 0 || number.Scale < -45)
			{
				num = 0f;
			}
			else if (number.Scale > 39)
			{
				num = float.PositiveInfinity;
			}
			else
			{
				uint value = (uint)Number.NumberToFloatingPointBits(ref number, Number.FloatingPointInfo.Single);
				num = BitConverter.Int32BitsToSingle((int)value);
			}
			if (!number.IsNegative)
			{
				return num;
			}
			return -num;
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000E4404 File Offset: 0x000E3604
		[CompilerGenerated]
		internal unsafe static string <FormatInt32>g__FormatInt32Slow|38_0(int value, int hexMask, string format, IFormatProvider provider)
		{
			ReadOnlySpan<char> format2 = format;
			int num;
			char c = Number.ParseFormatSpecifier(format2, out num);
			char c2 = c & '￟';
			if ((c2 == 'G') ? (num < 1) : (c2 == 'D'))
			{
				if (value < 0)
				{
					return Number.NegativeInt32ToDecStr(value, num, NumberFormatInfo.GetInstance(provider).NegativeSign);
				}
				return Number.UInt32ToDecStr((uint)value, num);
			}
			else
			{
				if (c2 == 'X')
				{
					return Number.Int32ToHexStr(value & hexMask, Number.GetHexBase(c), num);
				}
				NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
				byte* digits = stackalloc byte[(UIntPtr)11];
				Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 11);
				Number.Int32ToNumber(value, ref numberBuffer);
				char* pointer = stackalloc char[(UIntPtr)64];
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
				if (c != '\0')
				{
					Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, num, instance);
				}
				else
				{
					Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format2, instance);
				}
				return valueStringBuilder.ToString();
			}
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x000E44D8 File Offset: 0x000E36D8
		[CompilerGenerated]
		internal unsafe static bool <TryFormatInt32>g__TryFormatInt32Slow|39_0(int value, int hexMask, ReadOnlySpan<char> format, IFormatProvider provider, Span<char> destination, out int charsWritten)
		{
			int num;
			char c = Number.ParseFormatSpecifier(format, out num);
			char c2 = c & '￟';
			if ((c2 == 'G') ? (num < 1) : (c2 == 'D'))
			{
				if (value < 0)
				{
					return Number.TryNegativeInt32ToDecStr(value, num, NumberFormatInfo.GetInstance(provider).NegativeSign, destination, out charsWritten);
				}
				return Number.TryUInt32ToDecStr((uint)value, num, destination, out charsWritten);
			}
			else
			{
				if (c2 == 'X')
				{
					return Number.TryInt32ToHexStr(value & hexMask, Number.GetHexBase(c), num, destination, out charsWritten);
				}
				NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
				byte* digits = stackalloc byte[(UIntPtr)11];
				Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 11);
				Number.Int32ToNumber(value, ref numberBuffer);
				char* pointer = stackalloc char[(UIntPtr)64];
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
				if (c != '\0')
				{
					Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, num, instance);
				}
				else
				{
					Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format, instance);
				}
				return valueStringBuilder.TryCopyTo(destination, out charsWritten);
			}
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000E45AC File Offset: 0x000E37AC
		[CompilerGenerated]
		internal unsafe static string <FormatUInt32>g__FormatUInt32Slow|40_0(uint value, string format, IFormatProvider provider)
		{
			ReadOnlySpan<char> format2 = format;
			int num;
			char c = Number.ParseFormatSpecifier(format2, out num);
			char c2 = c & '￟';
			if ((c2 == 'G') ? (num < 1) : (c2 == 'D'))
			{
				return Number.UInt32ToDecStr(value, num);
			}
			if (c2 == 'X')
			{
				return Number.Int32ToHexStr((int)value, Number.GetHexBase(c), num);
			}
			NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
			byte* digits = stackalloc byte[(UIntPtr)11];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 11);
			Number.UInt32ToNumber(value, ref numberBuffer);
			char* pointer = stackalloc char[(UIntPtr)64];
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
			if (c != '\0')
			{
				Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, num, instance);
			}
			else
			{
				Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format2, instance);
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x000E4664 File Offset: 0x000E3864
		[CompilerGenerated]
		internal unsafe static bool <TryFormatUInt32>g__TryFormatUInt32Slow|41_0(uint value, ReadOnlySpan<char> format, IFormatProvider provider, Span<char> destination, out int charsWritten)
		{
			int num;
			char c = Number.ParseFormatSpecifier(format, out num);
			char c2 = c & '￟';
			if ((c2 == 'G') ? (num < 1) : (c2 == 'D'))
			{
				return Number.TryUInt32ToDecStr(value, num, destination, out charsWritten);
			}
			if (c2 == 'X')
			{
				return Number.TryInt32ToHexStr((int)value, Number.GetHexBase(c), num, destination, out charsWritten);
			}
			NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
			byte* digits = stackalloc byte[(UIntPtr)11];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 11);
			Number.UInt32ToNumber(value, ref numberBuffer);
			char* pointer = stackalloc char[(UIntPtr)64];
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
			if (c != '\0')
			{
				Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, num, instance);
			}
			else
			{
				Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format, instance);
			}
			return valueStringBuilder.TryCopyTo(destination, out charsWritten);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000E4718 File Offset: 0x000E3918
		[CompilerGenerated]
		internal unsafe static string <FormatInt64>g__FormatInt64Slow|42_0(long value, string format, IFormatProvider provider)
		{
			ReadOnlySpan<char> format2 = format;
			int num;
			char c = Number.ParseFormatSpecifier(format2, out num);
			char c2 = c & '￟';
			if ((c2 == 'G') ? (num < 1) : (c2 == 'D'))
			{
				if (value < 0L)
				{
					return Number.NegativeInt64ToDecStr(value, num, NumberFormatInfo.GetInstance(provider).NegativeSign);
				}
				return Number.UInt64ToDecStr((ulong)value, num);
			}
			else
			{
				if (c2 == 'X')
				{
					return Number.Int64ToHexStr(value, Number.GetHexBase(c), num);
				}
				NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
				byte* digits = stackalloc byte[(UIntPtr)20];
				Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 20);
				Number.Int64ToNumber(value, ref numberBuffer);
				char* pointer = stackalloc char[(UIntPtr)64];
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
				if (c != '\0')
				{
					Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, num, instance);
				}
				else
				{
					Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format2, instance);
				}
				return valueStringBuilder.ToString();
			}
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x000E47E8 File Offset: 0x000E39E8
		[CompilerGenerated]
		internal unsafe static bool <TryFormatInt64>g__TryFormatInt64Slow|43_0(long value, ReadOnlySpan<char> format, IFormatProvider provider, Span<char> destination, out int charsWritten)
		{
			int num;
			char c = Number.ParseFormatSpecifier(format, out num);
			char c2 = c & '￟';
			if ((c2 == 'G') ? (num < 1) : (c2 == 'D'))
			{
				if (value < 0L)
				{
					return Number.TryNegativeInt64ToDecStr(value, num, NumberFormatInfo.GetInstance(provider).NegativeSign, destination, out charsWritten);
				}
				return Number.TryUInt64ToDecStr((ulong)value, num, destination, out charsWritten);
			}
			else
			{
				if (c2 == 'X')
				{
					return Number.TryInt64ToHexStr(value, Number.GetHexBase(c), num, destination, out charsWritten);
				}
				NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
				byte* digits = stackalloc byte[(UIntPtr)20];
				Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 20);
				Number.Int64ToNumber(value, ref numberBuffer);
				char* pointer = stackalloc char[(UIntPtr)64];
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
				if (c != '\0')
				{
					Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, num, instance);
				}
				else
				{
					Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format, instance);
				}
				return valueStringBuilder.TryCopyTo(destination, out charsWritten);
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000E48B4 File Offset: 0x000E3AB4
		[CompilerGenerated]
		internal unsafe static string <FormatUInt64>g__FormatUInt64Slow|44_0(ulong value, string format, IFormatProvider provider)
		{
			ReadOnlySpan<char> format2 = format;
			int num;
			char c = Number.ParseFormatSpecifier(format2, out num);
			char c2 = c & '￟';
			if ((c2 == 'G') ? (num < 1) : (c2 == 'D'))
			{
				return Number.UInt64ToDecStr(value, num);
			}
			if (c2 == 'X')
			{
				return Number.Int64ToHexStr((long)value, Number.GetHexBase(c), num);
			}
			NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
			byte* digits = stackalloc byte[(UIntPtr)21];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 21);
			Number.UInt64ToNumber(value, ref numberBuffer);
			char* pointer = stackalloc char[(UIntPtr)64];
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
			if (c != '\0')
			{
				Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, num, instance);
			}
			else
			{
				Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format2, instance);
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000E496C File Offset: 0x000E3B6C
		[CompilerGenerated]
		internal unsafe static bool <TryFormatUInt64>g__TryFormatUInt64Slow|45_0(ulong value, ReadOnlySpan<char> format, IFormatProvider provider, Span<char> destination, out int charsWritten)
		{
			int num;
			char c = Number.ParseFormatSpecifier(format, out num);
			char c2 = c & '￟';
			if ((c2 == 'G') ? (num < 1) : (c2 == 'D'))
			{
				return Number.TryUInt64ToDecStr(value, num, destination, out charsWritten);
			}
			if (c2 == 'X')
			{
				return Number.TryInt64ToHexStr((long)value, Number.GetHexBase(c), num, destination, out charsWritten);
			}
			NumberFormatInfo instance = NumberFormatInfo.GetInstance(provider);
			byte* digits = stackalloc byte[(UIntPtr)21];
			Number.NumberBuffer numberBuffer = new Number.NumberBuffer(Number.NumberBufferKind.Integer, digits, 21);
			Number.UInt64ToNumber(value, ref numberBuffer);
			char* pointer = stackalloc char[(UIntPtr)64];
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(new Span<char>((void*)pointer, 32));
			if (c != '\0')
			{
				Number.NumberToString(ref valueStringBuilder, ref numberBuffer, c, num, instance);
			}
			else
			{
				Number.NumberToStringFormat(ref valueStringBuilder, ref numberBuffer, format, instance);
			}
			return valueStringBuilder.TryCopyTo(destination, out charsWritten);
		}

		// Token: 0x060011DE RID: 4574 RVA: 0x000E4A20 File Offset: 0x000E3C20
		[CompilerGenerated]
		internal unsafe static bool <RoundNumber>g__ShouldRoundUp|78_0(byte* dig, int i, Number.NumberBufferKind numberKind, bool isCorrectlyRounded)
		{
			byte b = dig[i];
			return b != 0 && !isCorrectlyRounded && b >= 53;
		}

		// Token: 0x04000429 RID: 1065
		private static readonly string[] s_singleDigitStringCache = new string[]
		{
			"0",
			"1",
			"2",
			"3",
			"4",
			"5",
			"6",
			"7",
			"8",
			"9"
		};

		// Token: 0x0400042A RID: 1066
		private static readonly string[] s_posCurrencyFormats = new string[]
		{
			"$#",
			"#$",
			"$ #",
			"# $"
		};

		// Token: 0x0400042B RID: 1067
		private static readonly string[] s_negCurrencyFormats = new string[]
		{
			"($#)",
			"-$#",
			"$-#",
			"$#-",
			"(#$)",
			"-#$",
			"#-$",
			"#$-",
			"-# $",
			"-$ #",
			"# $-",
			"$ #-",
			"$ -#",
			"#- $",
			"($ #)",
			"(# $)",
			"$- #"
		};

		// Token: 0x0400042C RID: 1068
		private static readonly string[] s_posPercentFormats = new string[]
		{
			"# %",
			"#%",
			"%#",
			"% #"
		};

		// Token: 0x0400042D RID: 1069
		private static readonly string[] s_negPercentFormats = new string[]
		{
			"-# %",
			"-#%",
			"-%#",
			"%-#",
			"%#-",
			"#-%",
			"#%-",
			"-% #",
			"# %-",
			"% #-",
			"% -#",
			"#- %"
		};

		// Token: 0x0400042E RID: 1070
		private static readonly string[] s_negNumberFormats = new string[]
		{
			"(#)",
			"-#",
			"- #",
			"#-",
			"# -"
		};

		// Token: 0x0400042F RID: 1071
		private static readonly float[] s_Pow10SingleTable = new float[]
		{
			1f,
			10f,
			100f,
			1000f,
			10000f,
			100000f,
			1000000f,
			10000000f,
			100000000f,
			1E+09f,
			1E+10f
		};

		// Token: 0x04000430 RID: 1072
		private static readonly double[] s_Pow10DoubleTable = new double[]
		{
			1.0,
			10.0,
			100.0,
			1000.0,
			10000.0,
			100000.0,
			1000000.0,
			10000000.0,
			100000000.0,
			1000000000.0,
			10000000000.0,
			100000000000.0,
			1000000000000.0,
			10000000000000.0,
			100000000000000.0,
			1000000000000000.0,
			10000000000000000.0,
			1E+17,
			1E+18,
			1E+19,
			1E+20,
			1E+21,
			1E+22
		};

		// Token: 0x0200015C RID: 348
		[StructLayout(LayoutKind.Sequential, Pack = 1)]
		internal ref struct BigInteger
		{
			// Token: 0x060011DF RID: 4575 RVA: 0x000E4A44 File Offset: 0x000E3C44
			public unsafe static void Add(ref Number.BigInteger lhs, ref Number.BigInteger rhs, out Number.BigInteger result)
			{
				ref Number.BigInteger ptr = ref (lhs._length < rhs._length) ? ref rhs : ref lhs;
				ref Number.BigInteger ptr2 = ref (lhs._length < rhs._length) ? ref lhs : ref rhs;
				int length = ptr._length;
				int length2 = ptr2._length;
				result._length = length;
				ulong num = 0UL;
				int i = 0;
				int j = 0;
				int num2 = 0;
				while (j < length2)
				{
					ulong num3 = num + (ulong)(*(ref ptr._blocks.FixedElementField + (IntPtr)i * 4)) + (ulong)(*(ref ptr2._blocks.FixedElementField + (IntPtr)j * 4));
					num = num3 >> 32;
					*(ref result._blocks.FixedElementField + (IntPtr)num2 * 4) = (uint)num3;
					i++;
					j++;
					num2++;
				}
				while (i < length)
				{
					ulong num4 = num + (ulong)(*(ref ptr._blocks.FixedElementField + (IntPtr)i * 4));
					num = num4 >> 32;
					*(ref result._blocks.FixedElementField + (IntPtr)num2 * 4) = (uint)num4;
					i++;
					num2++;
				}
				if (num != 0UL)
				{
					*(ref result._blocks.FixedElementField + (IntPtr)num2 * 4) = 1U;
					result._length++;
				}
			}

			// Token: 0x060011E0 RID: 4576 RVA: 0x000E4B64 File Offset: 0x000E3D64
			public unsafe static int Compare(ref Number.BigInteger lhs, ref Number.BigInteger rhs)
			{
				int length = lhs._length;
				int length2 = rhs._length;
				int num = length - length2;
				if (num != 0)
				{
					return num;
				}
				if (length == 0)
				{
					return 0;
				}
				int i = length - 1;
				while (i >= 0)
				{
					long num2 = (long)((ulong)(*(ref lhs._blocks.FixedElementField + (IntPtr)i * 4)) - (ulong)(*(ref rhs._blocks.FixedElementField + (IntPtr)i * 4)));
					if (num2 != 0L)
					{
						if (num2 <= 0L)
						{
							return -1;
						}
						return 1;
					}
					else
					{
						i--;
					}
				}
				return 0;
			}

			// Token: 0x060011E1 RID: 4577 RVA: 0x000E4BD1 File Offset: 0x000E3DD1
			public static uint CountSignificantBits(uint value)
			{
				return (uint)(32 - BitOperations.LeadingZeroCount(value));
			}

			// Token: 0x060011E2 RID: 4578 RVA: 0x000E4BDC File Offset: 0x000E3DDC
			public static uint CountSignificantBits(ulong value)
			{
				return (uint)(64 - BitOperations.LeadingZeroCount(value));
			}

			// Token: 0x060011E3 RID: 4579 RVA: 0x000E4BE8 File Offset: 0x000E3DE8
			public unsafe static uint CountSignificantBits(ref Number.BigInteger value)
			{
				if (value.IsZero())
				{
					return 0U;
				}
				uint num = (uint)(value._length - 1);
				return num * 32U + Number.BigInteger.CountSignificantBits(*(ref value._blocks.FixedElementField + (IntPtr)((ulong)num * 4UL)));
			}

			// Token: 0x060011E4 RID: 4580 RVA: 0x000E4C28 File Offset: 0x000E3E28
			public unsafe static void DivRem(ref Number.BigInteger lhs, ref Number.BigInteger rhs, out Number.BigInteger quo, out Number.BigInteger rem)
			{
				if (lhs.IsZero())
				{
					Number.BigInteger.SetZero(out quo);
					Number.BigInteger.SetZero(out rem);
					return;
				}
				int length = lhs._length;
				int length2 = rhs._length;
				if (length == 1 && length2 == 1)
				{
					uint value2;
					uint value = Math.DivRem(lhs._blocks.FixedElementField, rhs._blocks.FixedElementField, out value2);
					Number.BigInteger.SetUInt32(out quo, value);
					Number.BigInteger.SetUInt32(out rem, value2);
					return;
				}
				if (length2 == 1)
				{
					int num = length;
					ulong b = (ulong)rhs._blocks.FixedElementField;
					ulong num2 = 0UL;
					for (int i = num - 1; i >= 0; i--)
					{
						ulong a = num2 << 32 | (ulong)(*(ref lhs._blocks.FixedElementField + (IntPtr)i * 4));
						ulong num3 = Math.DivRem(a, b, out num2);
						if (num3 == 0UL && i == num - 1)
						{
							num--;
						}
						else
						{
							*(ref quo._blocks.FixedElementField + (IntPtr)i * 4) = (uint)num3;
						}
					}
					quo._length = num;
					Number.BigInteger.SetUInt32(out rem, (uint)num2);
					return;
				}
				if (length2 > length)
				{
					Number.BigInteger.SetZero(out quo);
					Number.BigInteger.SetValue(out rem, ref lhs);
					return;
				}
				int num4 = length - length2 + 1;
				Number.BigInteger.SetValue(out rem, ref lhs);
				int num5 = length;
				uint num6 = *(ref rhs._blocks.FixedElementField + (IntPtr)(length2 - 1) * 4);
				uint num7 = *(ref rhs._blocks.FixedElementField + (IntPtr)(length2 - 2) * 4);
				int num8 = BitOperations.LeadingZeroCount(num6);
				int num9 = 32 - num8;
				if (num8 > 0)
				{
					num6 = (num6 << num8 | num7 >> num9);
					num7 <<= num8;
					if (length2 > 2)
					{
						num7 |= *(ref rhs._blocks.FixedElementField + (IntPtr)(length2 - 3) * 4) >> num9;
					}
				}
				for (int j = length; j >= length2; j--)
				{
					int num10 = j - length2;
					uint num11 = (j < length) ? (*(ref rem._blocks.FixedElementField + (IntPtr)j * 4)) : 0U;
					ulong num12 = (ulong)num11 << 32 | (ulong)(*(ref rem._blocks.FixedElementField + (IntPtr)(j - 1) * 4));
					uint num13 = (j > 1) ? (*(ref rem._blocks.FixedElementField + (IntPtr)(j - 2) * 4)) : 0U;
					if (num8 > 0)
					{
						num12 = (num12 << num8 | (ulong)(num13 >> num9));
						num13 <<= num8;
						if (j > 2)
						{
							num13 |= *(ref rem._blocks.FixedElementField + (IntPtr)(j - 3) * 4) >> num9;
						}
					}
					ulong num14 = num12 / (ulong)num6;
					if (num14 > (ulong)-1)
					{
						num14 = (ulong)-1;
					}
					while (Number.BigInteger.DivideGuessTooBig(num14, num12, num13, num6, num7))
					{
						num14 -= 1UL;
					}
					if (num14 > 0UL)
					{
						uint num15 = Number.BigInteger.SubtractDivisor(ref rem, num10, ref rhs, num14);
						if (num15 != num11)
						{
							num15 = Number.BigInteger.AddDivisor(ref rem, num10, ref rhs);
							num14 -= 1UL;
						}
					}
					if (num4 != 0)
					{
						if (num14 == 0UL && num10 == num4 - 1)
						{
							num4--;
						}
						else
						{
							*(ref quo._blocks.FixedElementField + (IntPtr)num10 * 4) = (uint)num14;
						}
					}
					if (j < num5)
					{
						num5--;
					}
				}
				quo._length = num4;
				for (int k = num5 - 1; k >= 0; k--)
				{
					if (*(ref rem._blocks.FixedElementField + (IntPtr)k * 4) == 0U)
					{
						num5--;
					}
				}
				rem._length = num5;
			}

			// Token: 0x060011E5 RID: 4581 RVA: 0x000E4F4C File Offset: 0x000E414C
			public unsafe static uint HeuristicDivide(ref Number.BigInteger dividend, ref Number.BigInteger divisor)
			{
				int num = divisor._length;
				if (dividend._length < num)
				{
					return 0U;
				}
				int num2 = num - 1;
				uint num3 = *(ref dividend._blocks.FixedElementField + (IntPtr)num2 * 4) / (*(ref divisor._blocks.FixedElementField + (IntPtr)num2 * 4) + 1U);
				if (num3 != 0U)
				{
					int num4 = 0;
					ulong num5 = 0UL;
					ulong num6 = 0UL;
					do
					{
						ulong num7 = (ulong)(*(ref divisor._blocks.FixedElementField + (IntPtr)num4 * 4)) * (ulong)num3 + num6;
						num6 = num7 >> 32;
						ulong num8 = (ulong)(*(ref dividend._blocks.FixedElementField + (IntPtr)num4 * 4)) - (ulong)((uint)num7) - num5;
						num5 = (num8 >> 32 & 1UL);
						*(ref dividend._blocks.FixedElementField + (IntPtr)num4 * 4) = (uint)num8;
						num4++;
					}
					while (num4 < num);
					while (num > 0 && *(ref dividend._blocks.FixedElementField + (IntPtr)(num - 1) * 4) == 0U)
					{
						num--;
					}
					dividend._length = num;
				}
				if (Number.BigInteger.Compare(ref dividend, ref divisor) >= 0)
				{
					num3 += 1U;
					int num9 = 0;
					ulong num10 = 0UL;
					do
					{
						ulong num11 = (ulong)(*(ref dividend._blocks.FixedElementField + (IntPtr)num9 * 4)) - (ulong)(*(ref divisor._blocks.FixedElementField + (IntPtr)num9 * 4)) - num10;
						num10 = (num11 >> 32 & 1UL);
						*(ref dividend._blocks.FixedElementField + (IntPtr)num9 * 4) = (uint)num11;
						num9++;
					}
					while (num9 < num);
					while (num > 0 && *(ref dividend._blocks.FixedElementField + (IntPtr)(num - 1) * 4) == 0U)
					{
						num--;
					}
					dividend._length = num;
				}
				return num3;
			}

			// Token: 0x060011E6 RID: 4582 RVA: 0x000E50C4 File Offset: 0x000E42C4
			public unsafe static void Multiply(ref Number.BigInteger lhs, uint value, out Number.BigInteger result)
			{
				if (lhs._length <= 1)
				{
					Number.BigInteger.SetUInt64(out result, (ulong)lhs.ToUInt32() * (ulong)value);
					return;
				}
				if (value <= 1U)
				{
					if (value == 0U)
					{
						Number.BigInteger.SetZero(out result);
						return;
					}
					Number.BigInteger.SetValue(out result, ref lhs);
					return;
				}
				else
				{
					int length = lhs._length;
					int i = 0;
					uint num = 0U;
					while (i < length)
					{
						ulong num2 = (ulong)(*(ref lhs._blocks.FixedElementField + (IntPtr)i * 4)) * (ulong)value + (ulong)num;
						*(ref result._blocks.FixedElementField + (IntPtr)i * 4) = (uint)num2;
						num = (uint)(num2 >> 32);
						i++;
					}
					if (num != 0U)
					{
						*(ref result._blocks.FixedElementField + (IntPtr)i * 4) = num;
						result._length = length + 1;
						return;
					}
					result._length = length;
					return;
				}
			}

			// Token: 0x060011E7 RID: 4583 RVA: 0x000E5170 File Offset: 0x000E4370
			public unsafe static void Multiply(ref Number.BigInteger lhs, ref Number.BigInteger rhs, out Number.BigInteger result)
			{
				if (lhs._length <= 1)
				{
					Number.BigInteger.Multiply(ref rhs, lhs.ToUInt32(), out result);
					return;
				}
				if (rhs._length <= 1)
				{
					Number.BigInteger.Multiply(ref lhs, rhs.ToUInt32(), out result);
					return;
				}
				ref Number.BigInteger ptr = ref lhs;
				int length = lhs._length;
				ref Number.BigInteger ptr2 = ref rhs;
				int length2 = rhs._length;
				if (length < length2)
				{
					ptr = ref rhs;
					length = rhs._length;
					ptr2 = ref lhs;
					length2 = lhs._length;
				}
				int num = length2 + length;
				result._length = num;
				Buffer.ZeroMemory((byte*)result.GetBlocksPointer(), (UIntPtr)(num * 4));
				int i = 0;
				int num2 = 0;
				while (i < length2)
				{
					if (*(ref ptr2._blocks.FixedElementField + (IntPtr)i * 4) != 0U)
					{
						int num3 = 0;
						int num4 = num2;
						ulong num5 = 0UL;
						do
						{
							ulong num6 = (ulong)(*(ref result._blocks.FixedElementField + (IntPtr)num4 * 4)) + (ulong)(*(ref ptr2._blocks.FixedElementField + (IntPtr)i * 4)) * (ulong)(*(ref ptr._blocks.FixedElementField + (IntPtr)num3 * 4)) + num5;
							num5 = num6 >> 32;
							*(ref result._blocks.FixedElementField + (IntPtr)num4 * 4) = (uint)num6;
							num4++;
							num3++;
						}
						while (num3 < length);
						*(ref result._blocks.FixedElementField + (IntPtr)num4 * 4) = (uint)num5;
					}
					i++;
					num2++;
				}
				if (num > 0 && *(ref result._blocks.FixedElementField + (IntPtr)(num - 1) * 4) == 0U)
				{
					result._length--;
				}
			}

			// Token: 0x060011E8 RID: 4584 RVA: 0x000E52DC File Offset: 0x000E44DC
			public unsafe static void Pow2(uint exponent, out Number.BigInteger result)
			{
				uint num2;
				uint num = Number.BigInteger.DivRem32(exponent, out num2);
				result._length = (int)(num + 1U);
				if (num > 0U)
				{
					Buffer.ZeroMemory((byte*)result.GetBlocksPointer(), (UIntPtr)(num * 4U));
				}
				*(ref result._blocks.FixedElementField + (IntPtr)((ulong)num * 4UL)) = 1U << (int)num2;
			}

			// Token: 0x060011E9 RID: 4585 RVA: 0x000E5328 File Offset: 0x000E4528
			public unsafe static void Pow10(uint exponent, out Number.BigInteger result)
			{
				Number.BigInteger bigInteger;
				Number.BigInteger.SetUInt32(out bigInteger, Number.BigInteger.s_Pow10UInt32Table[(int)(exponent & 7U)]);
				ref Number.BigInteger ptr = ref bigInteger;
				Number.BigInteger bigInteger2;
				Number.BigInteger.SetZero(out bigInteger2);
				ref Number.BigInteger ptr2 = ref bigInteger2;
				exponent >>= 3;
				uint num = 0U;
				while (exponent != 0U)
				{
					if ((exponent & 1U) != 0U)
					{
						fixed (uint* ptr3 = &Number.BigInteger.s_Pow10BigNumTable[Number.BigInteger.s_Pow10BigNumTableIndices[(int)num]])
						{
							uint* ptr4 = ptr3;
							ref Number.BigInteger rhs = ref *(Number.BigInteger*)ptr4;
							Number.BigInteger.Multiply(ref ptr, ref rhs, out ptr2);
						}
						ref Number.BigInteger ptr5 = ref ptr2;
						ptr2 = ref ptr;
						ptr = ref ptr5;
					}
					num += 1U;
					exponent >>= 1;
				}
				Number.BigInteger.SetValue(out result, ref ptr);
			}

			// Token: 0x060011EA RID: 4586 RVA: 0x000E53A8 File Offset: 0x000E45A8
			private unsafe static uint AddDivisor(ref Number.BigInteger lhs, int lhsStartIndex, ref Number.BigInteger rhs)
			{
				int length = lhs._length;
				int length2 = rhs._length;
				ulong num = 0UL;
				for (int i = 0; i < length2; i++)
				{
					ref uint ptr = ref lhs._blocks.FixedElementField + (IntPtr)(lhsStartIndex + i) * 4;
					ulong num2 = (ulong)ptr + num + (ulong)(*(ref rhs._blocks.FixedElementField + (IntPtr)i * 4));
					ptr = (uint)num2;
					num = num2 >> 32;
				}
				return (uint)num;
			}

			// Token: 0x060011EB RID: 4587 RVA: 0x000E5410 File Offset: 0x000E4610
			private static bool DivideGuessTooBig(ulong q, ulong valHi, uint valLo, uint divHi, uint divLo)
			{
				ulong num = (ulong)divHi * q;
				ulong num2 = (ulong)divLo * q;
				num += num2 >> 32;
				num2 &= (ulong)-1;
				return num >= valHi && (num > valHi || (num2 >= (ulong)valLo && num2 > (ulong)valLo));
			}

			// Token: 0x060011EC RID: 4588 RVA: 0x000E5450 File Offset: 0x000E4650
			private unsafe static uint SubtractDivisor(ref Number.BigInteger lhs, int lhsStartIndex, ref Number.BigInteger rhs, ulong q)
			{
				int num = lhs._length - lhsStartIndex;
				int length = rhs._length;
				ulong num2 = 0UL;
				for (int i = 0; i < length; i++)
				{
					num2 += (ulong)(*(ref rhs._blocks.FixedElementField + (IntPtr)i * 4)) * q;
					uint num3 = (uint)num2;
					num2 >>= 32;
					ref uint ptr = ref lhs._blocks.FixedElementField + (IntPtr)(lhsStartIndex + i) * 4;
					if (ptr < num3)
					{
						num2 += 1UL;
					}
					ptr -= num3;
				}
				return (uint)num2;
			}

			// Token: 0x060011ED RID: 4589 RVA: 0x000E54C8 File Offset: 0x000E46C8
			public unsafe void Add(uint value)
			{
				int length = this._length;
				if (length == 0)
				{
					Number.BigInteger.SetUInt32(out this, value);
					return;
				}
				this._blocks.FixedElementField = this._blocks.FixedElementField + value;
				if (this._blocks.FixedElementField >= value)
				{
					return;
				}
				for (int i = 1; i < length; i++)
				{
					*(ref this._blocks.FixedElementField + (IntPtr)i * 4) += 1U;
					if (*(ref this._blocks.FixedElementField + (IntPtr)i * 4) > 0U)
					{
						return;
					}
				}
				*(ref this._blocks.FixedElementField + (IntPtr)length * 4) = 1U;
				this._length = length + 1;
			}

			// Token: 0x060011EE RID: 4590 RVA: 0x000E5558 File Offset: 0x000E4758
			public unsafe uint GetBlock(uint index)
			{
				return *(ref this._blocks.FixedElementField + (IntPtr)((ulong)index * 4UL));
			}

			// Token: 0x060011EF RID: 4591 RVA: 0x000E556D File Offset: 0x000E476D
			public int GetLength()
			{
				return this._length;
			}

			// Token: 0x060011F0 RID: 4592 RVA: 0x000E5575 File Offset: 0x000E4775
			public bool IsZero()
			{
				return this._length == 0;
			}

			// Token: 0x060011F1 RID: 4593 RVA: 0x000E5580 File Offset: 0x000E4780
			public void Multiply(uint value)
			{
				Number.BigInteger.Multiply(ref this, value, out this);
			}

			// Token: 0x060011F2 RID: 4594 RVA: 0x000E558C File Offset: 0x000E478C
			public void Multiply(ref Number.BigInteger value)
			{
				if (value._length <= 1)
				{
					Number.BigInteger.Multiply(ref this, value.ToUInt32(), out this);
					return;
				}
				Number.BigInteger bigInteger;
				Number.BigInteger.SetValue(out bigInteger, ref this);
				Number.BigInteger.Multiply(ref bigInteger, ref value, out this);
			}

			// Token: 0x060011F3 RID: 4595 RVA: 0x000E55C4 File Offset: 0x000E47C4
			public unsafe void Multiply10()
			{
				if (this.IsZero())
				{
					return;
				}
				int num = 0;
				int length = this._length;
				ulong num2 = 0UL;
				do
				{
					ulong num3 = (ulong)(*(ref this._blocks.FixedElementField + (IntPtr)num * 4));
					ulong num4 = (num3 << 3) + (num3 << 1) + num2;
					num2 = num4 >> 32;
					*(ref this._blocks.FixedElementField + (IntPtr)num * 4) = (uint)num4;
					num++;
				}
				while (num < length);
				if (num2 != 0UL)
				{
					*(ref this._blocks.FixedElementField + (IntPtr)num * 4) = (uint)num2;
					this._length++;
				}
			}

			// Token: 0x060011F4 RID: 4596 RVA: 0x000E564C File Offset: 0x000E484C
			public void MultiplyPow10(uint exponent)
			{
				if (exponent <= 9U)
				{
					this.Multiply(Number.BigInteger.s_Pow10UInt32Table[(int)exponent]);
					return;
				}
				if (!this.IsZero())
				{
					Number.BigInteger bigInteger;
					Number.BigInteger.Pow10(exponent, out bigInteger);
					this.Multiply(ref bigInteger);
				}
			}

			// Token: 0x060011F5 RID: 4597 RVA: 0x000E5684 File Offset: 0x000E4884
			public static void SetUInt32(out Number.BigInteger result, uint value)
			{
				if (value == 0U)
				{
					Number.BigInteger.SetZero(out result);
					return;
				}
				result._blocks.FixedElementField = value;
				result._length = 1;
			}

			// Token: 0x060011F6 RID: 4598 RVA: 0x000E56A4 File Offset: 0x000E48A4
			public unsafe static void SetUInt64(out Number.BigInteger result, ulong value)
			{
				if (value <= (ulong)-1)
				{
					Number.BigInteger.SetUInt32(out result, (uint)value);
					return;
				}
				result._blocks.FixedElementField = (uint)value;
				*(ref result._blocks.FixedElementField + 4) = (uint)(value >> 32);
				result._length = 2;
			}

			// Token: 0x060011F7 RID: 4599 RVA: 0x000E56DC File Offset: 0x000E48DC
			public unsafe static void SetValue(out Number.BigInteger result, ref Number.BigInteger value)
			{
				int length = value._length;
				result._length = length;
				Buffer.Memcpy((byte*)result.GetBlocksPointer(), (byte*)value.GetBlocksPointer(), length * 4);
			}

			// Token: 0x060011F8 RID: 4600 RVA: 0x000E570B File Offset: 0x000E490B
			public static void SetZero(out Number.BigInteger result)
			{
				result._length = 0;
			}

			// Token: 0x060011F9 RID: 4601 RVA: 0x000E5714 File Offset: 0x000E4914
			public unsafe void ShiftLeft(uint shift)
			{
				int length = this._length;
				if (length == 0 || shift == 0U)
				{
					return;
				}
				uint num2;
				uint num = Number.BigInteger.DivRem32(shift, out num2);
				int i = length - 1;
				int num3 = i + (int)num;
				if (num2 == 0U)
				{
					while (i >= 0)
					{
						*(ref this._blocks.FixedElementField + (IntPtr)num3 * 4) = *(ref this._blocks.FixedElementField + (IntPtr)i * 4);
						i--;
						num3--;
					}
					this._length += (int)num;
					Buffer.ZeroMemory((byte*)this.GetBlocksPointer(), (UIntPtr)(num * 4U));
					return;
				}
				num3++;
				this._length = num3 + 1;
				uint num4 = 32U - num2;
				uint num5 = 0U;
				uint num6 = *(ref this._blocks.FixedElementField + (IntPtr)i * 4);
				uint num7 = num6 >> (int)num4;
				while (i > 0)
				{
					*(ref this._blocks.FixedElementField + (IntPtr)num3 * 4) = (num5 | num7);
					num5 = num6 << (int)num2;
					i--;
					num3--;
					num6 = *(ref this._blocks.FixedElementField + (IntPtr)i * 4);
					num7 = num6 >> (int)num4;
				}
				*(ref this._blocks.FixedElementField + (IntPtr)num3 * 4) = (num5 | num7);
				*(ref this._blocks.FixedElementField + (IntPtr)(num3 - 1) * 4) = num6 << (int)num2;
				Buffer.ZeroMemory((byte*)this.GetBlocksPointer(), (UIntPtr)(num * 4U));
				if (*(ref this._blocks.FixedElementField + (IntPtr)(this._length - 1) * 4) == 0U)
				{
					this._length--;
				}
			}

			// Token: 0x060011FA RID: 4602 RVA: 0x000E5881 File Offset: 0x000E4A81
			public uint ToUInt32()
			{
				if (this._length > 0)
				{
					return this._blocks.FixedElementField;
				}
				return 0U;
			}

			// Token: 0x060011FB RID: 4603 RVA: 0x000E589C File Offset: 0x000E4A9C
			public unsafe ulong ToUInt64()
			{
				if (this._length > 1)
				{
					return ((ulong)(*(ref this._blocks.FixedElementField + 4)) << 32) + (ulong)this._blocks.FixedElementField;
				}
				if (this._length > 0)
				{
					return (ulong)this._blocks.FixedElementField;
				}
				return 0UL;
			}

			// Token: 0x060011FC RID: 4604 RVA: 0x000E58EC File Offset: 0x000E4AEC
			private unsafe uint* GetBlocksPointer()
			{
				return (uint*)Unsafe.AsPointer<uint>(ref this._blocks.FixedElementField);
			}

			// Token: 0x060011FD RID: 4605 RVA: 0x000E58FE File Offset: 0x000E4AFE
			private static uint DivRem32(uint value, out uint remainder)
			{
				remainder = (value & 31U);
				return value >> 5;
			}

			// Token: 0x04000431 RID: 1073
			private static readonly uint[] s_Pow10UInt32Table = new uint[]
			{
				1U,
				10U,
				100U,
				1000U,
				10000U,
				100000U,
				1000000U,
				10000000U,
				100000000U,
				1000000000U
			};

			// Token: 0x04000432 RID: 1074
			private static readonly int[] s_Pow10BigNumTableIndices = new int[]
			{
				0,
				2,
				5,
				10,
				18,
				33,
				61,
				116
			};

			// Token: 0x04000433 RID: 1075
			private static readonly uint[] s_Pow10BigNumTable = new uint[]
			{
				1U,
				100000000U,
				2U,
				1874919424U,
				2328306U,
				4U,
				0U,
				2242703233U,
				762134875U,
				1262U,
				7U,
				0U,
				0U,
				3211403009U,
				1849224548U,
				3668416493U,
				3913284084U,
				1593091U,
				14U,
				0U,
				0U,
				0U,
				0U,
				781532673U,
				64985353U,
				253049085U,
				594863151U,
				3553621484U,
				3288652808U,
				3167596762U,
				2788392729U,
				3911132675U,
				590U,
				27U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				2553183233U,
				3201533787U,
				3638140786U,
				303378311U,
				1809731782U,
				3477761648U,
				3583367183U,
				649228654U,
				2915460784U,
				487929380U,
				1011012442U,
				1677677582U,
				3428152256U,
				1710878487U,
				1438394610U,
				2161952759U,
				4100910556U,
				1608314830U,
				349175U,
				54U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				4234999809U,
				2012377703U,
				2408924892U,
				1570150255U,
				3090844311U,
				3273530073U,
				1187251475U,
				2498123591U,
				3364452033U,
				1148564857U,
				687371067U,
				2854068671U,
				1883165473U,
				505794538U,
				2988060450U,
				3159489326U,
				2531348317U,
				3215191468U,
				849106862U,
				3892080979U,
				3288073877U,
				2242451748U,
				4183778142U,
				2995818208U,
				2477501924U,
				325481258U,
				2487842652U,
				1774082830U,
				1933815724U,
				2962865281U,
				1168579910U,
				2724829000U,
				2360374019U,
				2315984659U,
				2360052375U,
				3251779801U,
				1664357844U,
				28U,
				107U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				689565697U,
				4116392818U,
				1853628763U,
				516071302U,
				2568769159U,
				365238920U,
				336250165U,
				1283268122U,
				3425490969U,
				248595470U,
				2305176814U,
				2111925499U,
				507770399U,
				2681111421U,
				589114268U,
				591287751U,
				1708941527U,
				4098957707U,
				475844916U,
				3378731398U,
				2452339615U,
				2817037361U,
				2678008327U,
				1656645978U,
				2383430340U,
				73103988U,
				448667107U,
				2329420453U,
				3124020241U,
				3625235717U,
				3208634035U,
				2412059158U,
				2981664444U,
				4117622508U,
				838560765U,
				3069470027U,
				270153238U,
				1802868219U,
				3692709886U,
				2161737865U,
				2159912357U,
				2585798786U,
				837488486U,
				4237238160U,
				2540319504U,
				3798629246U,
				3748148874U,
				1021550776U,
				2386715342U,
				1973637538U,
				1823520457U,
				1146713475U,
				833971519U,
				3277251466U,
				905620390U,
				26278816U,
				2680483154U,
				2294040859U,
				373297482U,
				5996609U,
				4109575006U,
				512575049U,
				917036550U,
				1942311753U,
				2816916778U,
				3248920332U,
				1192784020U,
				3537586671U,
				2456567643U,
				2925660628U,
				759380297U,
				888447942U,
				3559939476U,
				3654687237U,
				805U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U,
				0U
			};

			// Token: 0x04000434 RID: 1076
			private int _length;

			// Token: 0x04000435 RID: 1077
			[FixedBuffer(typeof(uint), 115)]
			private Number.BigInteger.<_blocks>e__FixedBuffer _blocks;

			// Token: 0x0200015D RID: 349
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 460)]
			public struct <_blocks>e__FixedBuffer
			{
				// Token: 0x04000436 RID: 1078
				public uint FixedElementField;
			}
		}

		// Token: 0x0200015E RID: 350
		[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
		internal readonly ref struct DiyFp
		{
			// Token: 0x060011FF RID: 4607 RVA: 0x000E5960 File Offset: 0x000E4B60
			public static Number.DiyFp CreateAndGetBoundaries(double value, out Number.DiyFp mMinus, out Number.DiyFp mPlus)
			{
				Number.DiyFp result = new Number.DiyFp(value);
				result.GetBoundaries(52, out mMinus, out mPlus);
				return result;
			}

			// Token: 0x06001200 RID: 4608 RVA: 0x000E5984 File Offset: 0x000E4B84
			public static Number.DiyFp CreateAndGetBoundaries(float value, out Number.DiyFp mMinus, out Number.DiyFp mPlus)
			{
				Number.DiyFp result = new Number.DiyFp(value);
				result.GetBoundaries(23, out mMinus, out mPlus);
				return result;
			}

			// Token: 0x06001201 RID: 4609 RVA: 0x000E59A8 File Offset: 0x000E4BA8
			public static Number.DiyFp CreateAndGetBoundaries(Half value, out Number.DiyFp mMinus, out Number.DiyFp mPlus)
			{
				Number.DiyFp result = new Number.DiyFp(value);
				result.GetBoundaries(10, out mMinus, out mPlus);
				return result;
			}

			// Token: 0x06001202 RID: 4610 RVA: 0x000E59C9 File Offset: 0x000E4BC9
			public DiyFp(double value)
			{
				this.f = Number.ExtractFractionAndBiasedExponent(value, out this.e);
			}

			// Token: 0x06001203 RID: 4611 RVA: 0x000E59DD File Offset: 0x000E4BDD
			public DiyFp(float value)
			{
				this.f = (ulong)Number.ExtractFractionAndBiasedExponent(value, out this.e);
			}

			// Token: 0x06001204 RID: 4612 RVA: 0x000E59F2 File Offset: 0x000E4BF2
			public DiyFp(Half value)
			{
				this.f = (ulong)Number.ExtractFractionAndBiasedExponent(value, out this.e);
			}

			// Token: 0x06001205 RID: 4613 RVA: 0x000E5A07 File Offset: 0x000E4C07
			public DiyFp(ulong f, int e)
			{
				this.f = f;
				this.e = e;
			}

			// Token: 0x06001206 RID: 4614 RVA: 0x000E5A18 File Offset: 0x000E4C18
			public Number.DiyFp Multiply(in Number.DiyFp other)
			{
				uint num = (uint)(this.f >> 32);
				uint num2 = (uint)this.f;
				uint num3 = (uint)(other.f >> 32);
				uint num4 = (uint)other.f;
				ulong num5 = (ulong)num * (ulong)num3;
				ulong num6 = (ulong)num2 * (ulong)num3;
				ulong num7 = (ulong)num * (ulong)num4;
				ulong num8 = (ulong)num2 * (ulong)num4;
				ulong num9 = (num8 >> 32) + (ulong)((uint)num7) + (ulong)((uint)num6);
				num9 += (ulong)int.MinValue;
				return new Number.DiyFp(num5 + (num7 >> 32) + (num6 >> 32) + (num9 >> 32), this.e + other.e + 64);
			}

			// Token: 0x06001207 RID: 4615 RVA: 0x000E5AAC File Offset: 0x000E4CAC
			public Number.DiyFp Normalize()
			{
				int num = BitOperations.LeadingZeroCount(this.f);
				return new Number.DiyFp(this.f << num, this.e - num);
			}

			// Token: 0x06001208 RID: 4616 RVA: 0x000E5ADD File Offset: 0x000E4CDD
			public Number.DiyFp Subtract(in Number.DiyFp other)
			{
				return new Number.DiyFp(this.f - other.f, this.e);
			}

			// Token: 0x06001209 RID: 4617 RVA: 0x000E5AF8 File Offset: 0x000E4CF8
			private void GetBoundaries(int implicitBitIndex, out Number.DiyFp mMinus, out Number.DiyFp mPlus)
			{
				mPlus = new Number.DiyFp((this.f << 1) + 1UL, this.e - 1).Normalize();
				if (this.f == 1UL << implicitBitIndex)
				{
					mMinus = new Number.DiyFp((this.f << 2) - 1UL, this.e - 2);
				}
				else
				{
					mMinus = new Number.DiyFp((this.f << 1) - 1UL, this.e - 1);
				}
				mMinus = new Number.DiyFp(mMinus.f << mMinus.e - mPlus.e, mPlus.e);
			}

			// Token: 0x04000437 RID: 1079
			public readonly ulong f;

			// Token: 0x04000438 RID: 1080
			public readonly int e;
		}

		// Token: 0x0200015F RID: 351
		internal static class Grisu3
		{
			// Token: 0x0600120A RID: 4618 RVA: 0x000E5BA0 File Offset: 0x000E4DA0
			public unsafe static bool TryRunDouble(double value, int requestedDigits, ref Number.NumberBuffer number)
			{
				double value2 = double.IsNegative(value) ? (-value) : value;
				int num;
				int num2;
				bool flag;
				if (requestedDigits == -1)
				{
					Number.DiyFp diyFp2;
					Number.DiyFp diyFp3;
					Number.DiyFp diyFp = Number.DiyFp.CreateAndGetBoundaries(value2, out diyFp2, out diyFp3).Normalize();
					flag = Number.Grisu3.TryRunShortest(diyFp2, diyFp, diyFp3, number.Digits, out num, out num2);
				}
				else
				{
					Number.DiyFp diyFp4 = new Number.DiyFp(value2).Normalize();
					flag = Number.Grisu3.TryRunCounted(diyFp4, requestedDigits, number.Digits, out num, out num2);
				}
				if (flag)
				{
					number.Scale = num + num2;
					*number.Digits[num] = 0;
					number.DigitsCount = num;
				}
				return flag;
			}

			// Token: 0x0600120B RID: 4619 RVA: 0x000E5C34 File Offset: 0x000E4E34
			public unsafe static bool TryRunHalf(Half value, int requestedDigits, ref Number.NumberBuffer number)
			{
				Half value2 = Half.IsNegative(value) ? Half.Negate(value) : value;
				int num;
				int num2;
				bool flag;
				if (requestedDigits == -1)
				{
					Number.DiyFp diyFp2;
					Number.DiyFp diyFp3;
					Number.DiyFp diyFp = Number.DiyFp.CreateAndGetBoundaries(value2, out diyFp2, out diyFp3).Normalize();
					flag = Number.Grisu3.TryRunShortest(diyFp2, diyFp, diyFp3, number.Digits, out num, out num2);
				}
				else
				{
					Number.DiyFp diyFp4 = new Number.DiyFp(value2).Normalize();
					flag = Number.Grisu3.TryRunCounted(diyFp4, requestedDigits, number.Digits, out num, out num2);
				}
				if (flag)
				{
					number.Scale = num + num2;
					*number.Digits[num] = 0;
					number.DigitsCount = num;
				}
				return flag;
			}

			// Token: 0x0600120C RID: 4620 RVA: 0x000E5CCC File Offset: 0x000E4ECC
			public unsafe static bool TryRunSingle(float value, int requestedDigits, ref Number.NumberBuffer number)
			{
				float value2 = float.IsNegative(value) ? (-value) : value;
				int num;
				int num2;
				bool flag;
				if (requestedDigits == -1)
				{
					Number.DiyFp diyFp2;
					Number.DiyFp diyFp3;
					Number.DiyFp diyFp = Number.DiyFp.CreateAndGetBoundaries(value2, out diyFp2, out diyFp3).Normalize();
					flag = Number.Grisu3.TryRunShortest(diyFp2, diyFp, diyFp3, number.Digits, out num, out num2);
				}
				else
				{
					Number.DiyFp diyFp4 = new Number.DiyFp(value2).Normalize();
					flag = Number.Grisu3.TryRunCounted(diyFp4, requestedDigits, number.Digits, out num, out num2);
				}
				if (flag)
				{
					number.Scale = num + num2;
					*number.Digits[num] = 0;
					number.DigitsCount = num;
				}
				return flag;
			}

			// Token: 0x0600120D RID: 4621 RVA: 0x000E5D60 File Offset: 0x000E4F60
			private static bool TryRunCounted(in Number.DiyFp w, int requestedDigits, Span<byte> buffer, out int length, out int decimalExponent)
			{
				int minExponent = -60 - (w.e + 64);
				int maxExponent = -32 - (w.e + 64);
				int num;
				Number.DiyFp cachedPowerForBinaryExponentRange = Number.Grisu3.GetCachedPowerForBinaryExponentRange(minExponent, maxExponent, out num);
				Number.DiyFp diyFp = w.Multiply(cachedPowerForBinaryExponentRange);
				int num2;
				bool result = Number.Grisu3.TryDigitGenCounted(diyFp, requestedDigits, buffer, out length, out num2);
				decimalExponent = -num + num2;
				return result;
			}

			// Token: 0x0600120E RID: 4622 RVA: 0x000E5DB4 File Offset: 0x000E4FB4
			private static bool TryRunShortest(in Number.DiyFp boundaryMinus, in Number.DiyFp w, in Number.DiyFp boundaryPlus, Span<byte> buffer, out int length, out int decimalExponent)
			{
				int minExponent = -60 - (w.e + 64);
				int maxExponent = -32 - (w.e + 64);
				int num;
				Number.DiyFp cachedPowerForBinaryExponentRange = Number.Grisu3.GetCachedPowerForBinaryExponentRange(minExponent, maxExponent, out num);
				Number.DiyFp diyFp = w.Multiply(cachedPowerForBinaryExponentRange);
				Number.DiyFp diyFp2 = boundaryMinus.Multiply(cachedPowerForBinaryExponentRange);
				Number.DiyFp diyFp3 = boundaryPlus.Multiply(cachedPowerForBinaryExponentRange);
				int num2;
				bool result = Number.Grisu3.TryDigitGenShortest(diyFp2, diyFp, diyFp3, buffer, out length, out num2);
				decimalExponent = -num + num2;
				return result;
			}

			// Token: 0x0600120F RID: 4623 RVA: 0x000E5E20 File Offset: 0x000E5020
			private static uint BiggestPowerTen(uint number, int numberBits, out int exponentPlusOne)
			{
				int num = (numberBits + 1) * 1233 >> 12;
				uint num2 = Number.Grisu3.s_SmallPowersOfTen[num];
				if (number < num2)
				{
					num--;
					num2 = Number.Grisu3.s_SmallPowersOfTen[num];
				}
				exponentPlusOne = num + 1;
				return num2;
			}

			// Token: 0x06001210 RID: 4624 RVA: 0x000E5E58 File Offset: 0x000E5058
			private unsafe static bool TryDigitGenCounted(in Number.DiyFp w, int requestedDigits, Span<byte> buffer, out int length, out int kappa)
			{
				ulong num = 1UL;
				Number.DiyFp diyFp = new Number.DiyFp(1UL << -w.e, w.e);
				uint num2 = (uint)(w.f >> -diyFp.e);
				ulong num3 = w.f & diyFp.f - 1UL;
				if (num3 == 0UL && (requestedDigits >= 11 || num2 < Number.Grisu3.s_SmallPowersOfTen[requestedDigits - 1]))
				{
					length = 0;
					kappa = 0;
					return false;
				}
				uint num4 = Number.Grisu3.BiggestPowerTen(num2, 64 - -diyFp.e, out kappa);
				length = 0;
				while (kappa > 0)
				{
					uint num5 = Math.DivRem(num2, num4, out num2);
					*buffer[length] = (byte)(48U + num5);
					length++;
					requestedDigits--;
					kappa--;
					if (requestedDigits == 0)
					{
						break;
					}
					num4 /= 10U;
				}
				if (requestedDigits == 0)
				{
					ulong rest = ((ulong)num2 << -diyFp.e) + num3;
					return Number.Grisu3.TryRoundWeedCounted(buffer, length, rest, (ulong)num4 << -diyFp.e, num, ref kappa);
				}
				while (requestedDigits > 0 && num3 > num)
				{
					num3 *= 10UL;
					num *= 10UL;
					uint num6 = (uint)(num3 >> -diyFp.e);
					*buffer[length] = (byte)(48U + num6);
					length++;
					requestedDigits--;
					kappa--;
					num3 &= diyFp.f - 1UL;
				}
				if (requestedDigits != 0)
				{
					*buffer[0] = 0;
					length = 0;
					kappa = 0;
					return false;
				}
				return Number.Grisu3.TryRoundWeedCounted(buffer, length, num3, diyFp.f, num, ref kappa);
			}

			// Token: 0x06001211 RID: 4625 RVA: 0x000E5FC8 File Offset: 0x000E51C8
			private unsafe static bool TryDigitGenShortest(in Number.DiyFp low, in Number.DiyFp w, in Number.DiyFp high, Span<byte> buffer, out int length, out int kappa)
			{
				ulong num = 1UL;
				Number.DiyFp diyFp = new Number.DiyFp(low.f - num, low.e);
				Number.DiyFp diyFp2 = new Number.DiyFp(high.f + num, high.e);
				Number.DiyFp diyFp3 = diyFp2.Subtract(diyFp);
				Number.DiyFp diyFp4 = new Number.DiyFp(1UL << -w.e, w.e);
				uint num2 = (uint)(diyFp2.f >> -diyFp4.e);
				ulong num3 = diyFp2.f & diyFp4.f - 1UL;
				uint num4 = Number.Grisu3.BiggestPowerTen(num2, 64 - -diyFp4.e, out kappa);
				length = 0;
				while (kappa > 0)
				{
					uint num5 = Math.DivRem(num2, num4, out num2);
					*buffer[length] = (byte)(48U + num5);
					length++;
					kappa--;
					ulong num6 = ((ulong)num2 << -diyFp4.e) + num3;
					if (num6 < diyFp3.f)
					{
						return Number.Grisu3.TryRoundWeedShortest(buffer, length, diyFp2.Subtract(w).f, diyFp3.f, num6, (ulong)num4 << -diyFp4.e, num);
					}
					num4 /= 10U;
				}
				do
				{
					num3 *= 10UL;
					num *= 10UL;
					diyFp3 = new Number.DiyFp(diyFp3.f * 10UL, diyFp3.e);
					uint num7 = (uint)(num3 >> -diyFp4.e);
					*buffer[length] = (byte)(48U + num7);
					length++;
					kappa--;
					num3 &= diyFp4.f - 1UL;
				}
				while (num3 >= diyFp3.f);
				return Number.Grisu3.TryRoundWeedShortest(buffer, length, diyFp2.Subtract(w).f * num, diyFp3.f, num3, diyFp4.f, num);
			}

			// Token: 0x06001212 RID: 4626 RVA: 0x000E6190 File Offset: 0x000E5390
			private static Number.DiyFp GetCachedPowerForBinaryExponentRange(int minExponent, int maxExponent, out int decimalExponent)
			{
				double num = Math.Ceiling((double)(minExponent + 64 - 1) * 0.3010299956639812);
				int num2 = (348 + (int)num - 1) / 8 + 1;
				decimalExponent = (int)Number.Grisu3.s_CachedPowersDecimalExponent[num2];
				return new Number.DiyFp(Number.Grisu3.s_CachedPowersSignificand[num2], (int)Number.Grisu3.s_CachedPowersBinaryExponent[num2]);
			}

			// Token: 0x06001213 RID: 4627 RVA: 0x000E61E0 File Offset: 0x000E53E0
			private unsafe static bool TryRoundWeedCounted(Span<byte> buffer, int length, ulong rest, ulong tenKappa, ulong unit, ref int kappa)
			{
				if (unit >= tenKappa || tenKappa - unit <= unit)
				{
					return false;
				}
				if (tenKappa - rest > rest && tenKappa - 2UL * rest >= 2UL * unit)
				{
					return true;
				}
				if (rest > unit && (tenKappa <= rest - unit || tenKappa - (rest - unit) <= rest - unit))
				{
					ref byte ptr = ref buffer[length - 1];
					ptr += 1;
					int num = length - 1;
					while (num > 0 && *buffer[num] == 58)
					{
						*buffer[num] = 48;
						ref byte ptr2 = ref buffer[num - 1];
						ptr2 += 1;
						num--;
					}
					if (*buffer[0] == 58)
					{
						*buffer[0] = 49;
						kappa++;
					}
					return true;
				}
				return false;
			}

			// Token: 0x06001214 RID: 4628 RVA: 0x000E6294 File Offset: 0x000E5494
			private static bool TryRoundWeedShortest(Span<byte> buffer, int length, ulong distanceTooHighW, ulong unsafeInterval, ulong rest, ulong tenKappa, ulong unit)
			{
				ulong num = distanceTooHighW - unit;
				ulong num2 = distanceTooHighW + unit;
				while (rest < num && unsafeInterval - rest >= tenKappa && (rest + tenKappa < num || num - rest >= rest + tenKappa - num))
				{
					ref byte ptr = ref buffer[length - 1];
					ptr -= 1;
					rest += tenKappa;
				}
				return (rest >= num2 || unsafeInterval - rest < tenKappa || (rest + tenKappa >= num2 && num2 - rest <= rest + tenKappa - num2)) && 2UL * unit <= rest && rest <= unsafeInterval - 4UL * unit;
			}

			// Token: 0x04000439 RID: 1081
			private static readonly short[] s_CachedPowersBinaryExponent = new short[]
			{
				-1220,
				-1193,
				-1166,
				-1140,
				-1113,
				-1087,
				-1060,
				-1034,
				-1007,
				-980,
				-954,
				-927,
				-901,
				-874,
				-847,
				-821,
				-794,
				-768,
				-741,
				-715,
				-688,
				-661,
				-635,
				-608,
				-582,
				-555,
				-529,
				-502,
				-475,
				-449,
				-422,
				-396,
				-369,
				-343,
				-316,
				-289,
				-263,
				-236,
				-210,
				-183,
				-157,
				-130,
				-103,
				-77,
				-50,
				-24,
				3,
				30,
				56,
				83,
				109,
				136,
				162,
				189,
				216,
				242,
				269,
				295,
				322,
				348,
				375,
				402,
				428,
				455,
				481,
				508,
				534,
				561,
				588,
				614,
				641,
				667,
				694,
				720,
				747,
				774,
				800,
				827,
				853,
				880,
				907,
				933,
				960,
				986,
				1013,
				1039,
				1066
			};

			// Token: 0x0400043A RID: 1082
			private static readonly short[] s_CachedPowersDecimalExponent = new short[]
			{
				-348,
				-340,
				-332,
				-324,
				-316,
				-308,
				-300,
				-292,
				-284,
				-276,
				-268,
				-260,
				-252,
				-244,
				-236,
				-228,
				-220,
				-212,
				-204,
				-196,
				-188,
				-180,
				-172,
				-164,
				-156,
				-148,
				-140,
				-132,
				-124,
				-116,
				-108,
				-100,
				-92,
				-84,
				-76,
				-68,
				-60,
				-52,
				-44,
				-36,
				-28,
				-20,
				-12,
				-4,
				4,
				12,
				20,
				28,
				36,
				44,
				52,
				60,
				68,
				76,
				84,
				92,
				100,
				108,
				116,
				124,
				132,
				140,
				148,
				156,
				164,
				172,
				180,
				188,
				196,
				204,
				212,
				220,
				228,
				236,
				244,
				252,
				260,
				268,
				276,
				284,
				292,
				300,
				308,
				316,
				324,
				332,
				340
			};

			// Token: 0x0400043B RID: 1083
			private static readonly ulong[] s_CachedPowersSignificand = new ulong[]
			{
				18054884314459144840UL,
				13451937075301367670UL,
				10022474136428063862UL,
				14934650266808366570UL,
				11127181549972568877UL,
				16580792590934885855UL,
				12353653155963782858UL,
				18408377700990114895UL,
				13715310171984221708UL,
				10218702384817765436UL,
				15227053142812498563UL,
				11345038669416679861UL,
				16905424996341287883UL,
				12595523146049147757UL,
				9384396036005875287UL,
				13983839803942852151UL,
				10418772551374772303UL,
				15525180923007089351UL,
				11567161174868858868UL,
				17236413322193710309UL,
				12842128665889583758UL,
				9568131466127621947UL,
				14257626930069360058UL,
				10622759856335341974UL,
				15829145694278690180UL,
				11793632577567316726UL,
				17573882009934360870UL,
				13093562431584567480UL,
				9755464219737475723UL,
				14536774485912137811UL,
				10830740992659433045UL,
				16139061738043178685UL,
				12024538023802026127UL,
				17917957937422433684UL,
				13349918974505688015UL,
				9946464728195732843UL,
				14821387422376473014UL,
				11042794154864902060UL,
				16455045573212060422UL,
				12259964326927110867UL,
				18268770466636286478UL,
				13611294676837538539UL,
				10141204801825835212UL,
				15111572745182864684UL,
				11258999068426240000UL,
				16777216000000000000UL,
				12500000000000000000UL,
				9313225746154785156UL,
				13877787807814456755UL,
				10339757656912845936UL,
				15407439555097886824UL,
				11479437019748901445UL,
				17105694144590052135UL,
				12744735289059618216UL,
				9495567745759798747UL,
				14149498560666738074UL,
				10542197943230523224UL,
				15709099088952724970UL,
				11704190886730495818UL,
				17440603504673385349UL,
				12994262207056124023UL,
				9681479787123295682UL,
				14426529090290212157UL,
				10748601772107342003UL,
				16016664761464807395UL,
				11933345169920330789UL,
				17782069995880619868UL,
				13248674568444952270UL,
				9871031767461413346UL,
				14708983551653345445UL,
				10959046745042015199UL,
				16330252207878254650UL,
				12166986024289022870UL,
				18130221999122236476UL,
				13508068024458167312UL,
				10064294952495520794UL,
				14996968138956309548UL,
				11173611982879273257UL,
				16649979327439178909UL,
				12405201291620119593UL,
				9242595204427927429UL,
				13772540099066387757UL,
				10261342003245940623UL,
				15290591125556738113UL,
				11392378155556871081UL,
				16975966327722178521UL,
				12648080533535911531UL
			};

			// Token: 0x0400043C RID: 1084
			private static readonly uint[] s_SmallPowersOfTen = new uint[]
			{
				1U,
				10U,
				100U,
				1000U,
				10000U,
				100000U,
				1000000U,
				10000000U,
				100000000U,
				1000000000U
			};
		}

		// Token: 0x02000160 RID: 352
		[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
		internal ref struct NumberBuffer
		{
			// Token: 0x06001216 RID: 4630 RVA: 0x000E638D File Offset: 0x000E558D
			public unsafe NumberBuffer(Number.NumberBufferKind kind, byte* digits, int digitsLength)
			{
				this.DigitsCount = 0;
				this.Scale = 0;
				this.IsNegative = false;
				this.HasNonZeroTail = false;
				this.Kind = kind;
				this.Digits = new Span<byte>((void*)digits, digitsLength);
				*this.Digits[0] = 0;
			}

			// Token: 0x06001217 RID: 4631 RVA: 0x000E63CD File Offset: 0x000E55CD
			public unsafe byte* GetDigitsPointer()
			{
				return (byte*)Unsafe.AsPointer<byte>(this.Digits[0]);
			}

			// Token: 0x06001218 RID: 4632 RVA: 0x000E63E0 File Offset: 0x000E55E0
			public unsafe override string ToString()
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append('[');
				stringBuilder.Append('"');
				for (int i = 0; i < this.Digits.Length; i++)
				{
					byte b = *this.Digits[i];
					if (b == 0)
					{
						break;
					}
					stringBuilder.Append((char)b);
				}
				stringBuilder.Append('"');
				stringBuilder.Append(", Length = ").Append(this.DigitsCount);
				stringBuilder.Append(", Scale = ").Append(this.Scale);
				stringBuilder.Append(", IsNegative = ").Append(this.IsNegative);
				stringBuilder.Append(", HasNonZeroTail = ").Append(this.HasNonZeroTail);
				stringBuilder.Append(", Kind = ").Append(this.Kind);
				stringBuilder.Append(']');
				return stringBuilder.ToString();
			}

			// Token: 0x0400043D RID: 1085
			public int DigitsCount;

			// Token: 0x0400043E RID: 1086
			public int Scale;

			// Token: 0x0400043F RID: 1087
			public bool IsNegative;

			// Token: 0x04000440 RID: 1088
			public bool HasNonZeroTail;

			// Token: 0x04000441 RID: 1089
			public Number.NumberBufferKind Kind;

			// Token: 0x04000442 RID: 1090
			public Span<byte> Digits;
		}

		// Token: 0x02000161 RID: 353
		internal enum NumberBufferKind : byte
		{
			// Token: 0x04000444 RID: 1092
			Unknown,
			// Token: 0x04000445 RID: 1093
			Integer,
			// Token: 0x04000446 RID: 1094
			Decimal,
			// Token: 0x04000447 RID: 1095
			FloatingPoint
		}

		// Token: 0x02000162 RID: 354
		public readonly struct FloatingPointInfo
		{
			// Token: 0x17000185 RID: 389
			// (get) Token: 0x06001219 RID: 4633 RVA: 0x000E64C4 File Offset: 0x000E56C4
			public ulong ZeroBits { get; }

			// Token: 0x17000186 RID: 390
			// (get) Token: 0x0600121A RID: 4634 RVA: 0x000E64CC File Offset: 0x000E56CC
			public ulong InfinityBits { get; }

			// Token: 0x17000187 RID: 391
			// (get) Token: 0x0600121B RID: 4635 RVA: 0x000E64D4 File Offset: 0x000E56D4
			public ulong NormalMantissaMask { get; }

			// Token: 0x17000188 RID: 392
			// (get) Token: 0x0600121C RID: 4636 RVA: 0x000E64DC File Offset: 0x000E56DC
			public ulong DenormalMantissaMask { get; }

			// Token: 0x17000189 RID: 393
			// (get) Token: 0x0600121D RID: 4637 RVA: 0x000E64E4 File Offset: 0x000E56E4
			public int MinBinaryExponent { get; }

			// Token: 0x1700018A RID: 394
			// (get) Token: 0x0600121E RID: 4638 RVA: 0x000E64EC File Offset: 0x000E56EC
			public int MaxBinaryExponent { get; }

			// Token: 0x1700018B RID: 395
			// (get) Token: 0x0600121F RID: 4639 RVA: 0x000E64F4 File Offset: 0x000E56F4
			public int ExponentBias { get; }

			// Token: 0x1700018C RID: 396
			// (get) Token: 0x06001220 RID: 4640 RVA: 0x000E64FC File Offset: 0x000E56FC
			public int OverflowDecimalExponent { get; }

			// Token: 0x1700018D RID: 397
			// (get) Token: 0x06001221 RID: 4641 RVA: 0x000E6504 File Offset: 0x000E5704
			public ushort NormalMantissaBits { get; }

			// Token: 0x1700018E RID: 398
			// (get) Token: 0x06001222 RID: 4642 RVA: 0x000E650C File Offset: 0x000E570C
			public ushort DenormalMantissaBits { get; }

			// Token: 0x06001223 RID: 4643 RVA: 0x000E6514 File Offset: 0x000E5714
			public FloatingPointInfo(ushort denormalMantissaBits, ushort exponentBits, int maxBinaryExponent, int exponentBias, ulong infinityBits)
			{
				this.<ExponentBits>k__BackingField = exponentBits;
				this.DenormalMantissaBits = denormalMantissaBits;
				this.NormalMantissaBits = denormalMantissaBits + 1;
				this.OverflowDecimalExponent = (maxBinaryExponent + (int)(2 * this.NormalMantissaBits)) / 3;
				this.ExponentBias = exponentBias;
				this.MaxBinaryExponent = maxBinaryExponent;
				this.MinBinaryExponent = 1 - maxBinaryExponent;
				this.DenormalMantissaMask = (1L << (int)denormalMantissaBits) - 1L;
				this.NormalMantissaMask = (1L << (int)this.NormalMantissaBits) - 1L;
				this.InfinityBits = infinityBits;
				this.ZeroBits = 0L;
			}

			// Token: 0x04000448 RID: 1096
			public static readonly Number.FloatingPointInfo Double = new Number.FloatingPointInfo(52, 11, 1023, 1023, 9218868437227405312UL);

			// Token: 0x04000449 RID: 1097
			public static readonly Number.FloatingPointInfo Single = new Number.FloatingPointInfo(23, 8, 127, 127, 2139095040UL);

			// Token: 0x0400044A RID: 1098
			public static readonly Number.FloatingPointInfo Half = new Number.FloatingPointInfo(10, 5, 15, 15, 31744UL);
		}

		// Token: 0x02000163 RID: 355
		internal enum ParsingStatus
		{
			// Token: 0x04000457 RID: 1111
			OK,
			// Token: 0x04000458 RID: 1112
			Failed,
			// Token: 0x04000459 RID: 1113
			Overflow
		}
	}
}
