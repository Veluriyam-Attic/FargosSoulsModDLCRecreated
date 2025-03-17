using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200016C RID: 364
	internal static class ParseNumbers
	{
		// Token: 0x0600126C RID: 4716 RVA: 0x000E6BE4 File Offset: 0x000E5DE4
		public static long StringToLong(ReadOnlySpan<char> s, int radix, int flags)
		{
			int num = 0;
			return ParseNumbers.StringToLong(s, radix, flags, ref num);
		}

		// Token: 0x0600126D RID: 4717 RVA: 0x000E6C00 File Offset: 0x000E5E00
		public unsafe static long StringToLong(ReadOnlySpan<char> s, int radix, int flags, ref int currPos)
		{
			int num = currPos;
			int num2 = (-1 == radix) ? 10 : radix;
			if (num2 != 2 && num2 != 10 && num2 != 8 && num2 != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase, "radix");
			}
			int length = s.Length;
			if (num < 0 || num >= length)
			{
				throw new ArgumentOutOfRangeException(SR.ArgumentOutOfRange_Index);
			}
			if ((flags & 4096) == 0 && (flags & 8192) == 0)
			{
				ParseNumbers.EatWhiteSpace(s, ref num);
				if (num == length)
				{
					throw new FormatException(SR.Format_EmptyInputString);
				}
			}
			int num3 = 1;
			if (*s[num] == 45)
			{
				if (num2 != 10)
				{
					throw new ArgumentException(SR.Arg_CannotHaveNegativeValue);
				}
				if ((flags & 512) != 0)
				{
					throw new OverflowException(SR.Overflow_NegativeUnsigned);
				}
				num3 = -1;
				num++;
			}
			else if (*s[num] == 43)
			{
				num++;
			}
			if ((radix == -1 || radix == 16) && num + 1 < length && *s[num] == 48 && (*s[num + 1] == 120 || *s[num + 1] == 88))
			{
				num2 = 16;
				num += 2;
			}
			int num4 = num;
			long num5 = ParseNumbers.GrabLongs(num2, s, ref num, (flags & 512) != 0);
			if (num == num4)
			{
				throw new FormatException(SR.Format_NoParsibleDigits);
			}
			if ((flags & 4096) != 0 && num < length)
			{
				throw new FormatException(SR.Format_ExtraJunkAtEnd);
			}
			currPos = num;
			if (num5 == -9223372036854775808L && num3 == 1 && num2 == 10 && (flags & 512) == 0)
			{
				Number.ThrowOverflowException(TypeCode.Int64);
			}
			if (num2 == 10)
			{
				num5 *= (long)num3;
			}
			return num5;
		}

		// Token: 0x0600126E RID: 4718 RVA: 0x000E6D84 File Offset: 0x000E5F84
		public static int StringToInt(ReadOnlySpan<char> s, int radix, int flags)
		{
			int num = 0;
			return ParseNumbers.StringToInt(s, radix, flags, ref num);
		}

		// Token: 0x0600126F RID: 4719 RVA: 0x000E6DA0 File Offset: 0x000E5FA0
		public unsafe static int StringToInt(ReadOnlySpan<char> s, int radix, int flags, ref int currPos)
		{
			int num = currPos;
			int num2 = (-1 == radix) ? 10 : radix;
			if (num2 != 2 && num2 != 10 && num2 != 8 && num2 != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase, "radix");
			}
			int length = s.Length;
			if (num < 0 || num >= length)
			{
				throw new ArgumentOutOfRangeException(SR.ArgumentOutOfRange_Index);
			}
			if ((flags & 4096) == 0 && (flags & 8192) == 0)
			{
				ParseNumbers.EatWhiteSpace(s, ref num);
				if (num == length)
				{
					throw new FormatException(SR.Format_EmptyInputString);
				}
			}
			int num3 = 1;
			if (*s[num] == 45)
			{
				if (num2 != 10)
				{
					throw new ArgumentException(SR.Arg_CannotHaveNegativeValue);
				}
				if ((flags & 512) != 0)
				{
					throw new OverflowException(SR.Overflow_NegativeUnsigned);
				}
				num3 = -1;
				num++;
			}
			else if (*s[num] == 43)
			{
				num++;
			}
			if ((radix == -1 || radix == 16) && num + 1 < length && *s[num] == 48 && (*s[num + 1] == 120 || *s[num + 1] == 88))
			{
				num2 = 16;
				num += 2;
			}
			int num4 = num;
			int num5 = ParseNumbers.GrabInts(num2, s, ref num, (flags & 512) != 0);
			if (num == num4)
			{
				throw new FormatException(SR.Format_NoParsibleDigits);
			}
			if ((flags & 4096) != 0 && num < length)
			{
				throw new FormatException(SR.Format_ExtraJunkAtEnd);
			}
			currPos = num;
			if ((flags & 1024) != 0)
			{
				if (num5 > 255)
				{
					Number.ThrowOverflowException(TypeCode.SByte);
				}
			}
			else if ((flags & 2048) != 0)
			{
				if (num5 > 65535)
				{
					Number.ThrowOverflowException(TypeCode.Int16);
				}
			}
			else if (num5 == -2147483648 && num3 == 1 && num2 == 10 && (flags & 512) == 0)
			{
				Number.ThrowOverflowException(TypeCode.Int32);
			}
			if (num2 == 10)
			{
				num5 *= num3;
			}
			return num5;
		}

		// Token: 0x06001270 RID: 4720 RVA: 0x000E6F54 File Offset: 0x000E6154
		public unsafe static string IntToString(int n, int radix, int width, char paddingChar, int flags)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)132], 66);
			Span<char> span2 = span;
			if (radix < 2 || radix > 36)
			{
				throw new ArgumentException(SR.Arg_InvalidBase, "radix");
			}
			bool flag = false;
			uint num;
			if (n < 0)
			{
				flag = true;
				num = (uint)((10 == radix) ? (-(uint)n) : n);
			}
			else
			{
				num = (uint)n;
			}
			if ((flags & 64) != 0)
			{
				num &= 255U;
			}
			else if ((flags & 128) != 0)
			{
				num &= 65535U;
			}
			int num2;
			if (num == 0U)
			{
				*span2[0] = '0';
				num2 = 1;
			}
			else
			{
				num2 = 0;
				for (int i = 0; i < span2.Length; i++)
				{
					uint num3 = num / (uint)radix;
					uint num4 = num - num3 * (uint)radix;
					num = num3;
					*span2[i] = ((num4 < 10U) ? ((char)(num4 + 48U)) : ((char)(num4 + 97U - 10U)));
					if (num == 0U)
					{
						num2 = i + 1;
						break;
					}
				}
			}
			if (radix != 10 && (flags & 32) != 0)
			{
				if (16 == radix)
				{
					*span2[num2++] = 'x';
					*span2[num2++] = '0';
				}
				else if (8 == radix)
				{
					*span2[num2++] = '0';
				}
			}
			if (10 == radix)
			{
				if (flag)
				{
					*span2[num2++] = '-';
				}
				else if ((flags & 16) != 0)
				{
					*span2[num2++] = '+';
				}
				else if ((flags & 8) != 0)
				{
					*span2[num2++] = ' ';
				}
			}
			string text = string.FastAllocateString(Math.Max(width, num2));
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
			char* ptr4 = ptr3;
			int num5 = text.Length - num2;
			if ((flags & 1) != 0)
			{
				for (int j = 0; j < num5; j++)
				{
					*(ptr4++) = paddingChar;
				}
				for (int k = 0; k < num2; k++)
				{
					*(ptr4++) = *span2[num2 - k - 1];
				}
			}
			else
			{
				for (int l = 0; l < num2; l++)
				{
					*(ptr4++) = *span2[num2 - l - 1];
				}
				for (int m = 0; m < num5; m++)
				{
					*(ptr4++) = paddingChar;
				}
			}
			char* ptr2 = null;
			return text;
		}

		// Token: 0x06001271 RID: 4721 RVA: 0x000E7178 File Offset: 0x000E6378
		public unsafe static string LongToString(long n, int radix, int width, char paddingChar, int flags)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)134], 67);
			Span<char> span2 = span;
			if (radix < 2 || radix > 36)
			{
				throw new ArgumentException(SR.Arg_InvalidBase, "radix");
			}
			bool flag = false;
			ulong num;
			if (n < 0L)
			{
				flag = true;
				num = (ulong)((10 == radix) ? (-(ulong)n) : n);
			}
			else
			{
				num = (ulong)n;
			}
			if ((flags & 64) != 0)
			{
				num &= 255UL;
			}
			else if ((flags & 128) != 0)
			{
				num &= 65535UL;
			}
			else if ((flags & 256) != 0)
			{
				num &= (ulong)-1;
			}
			int num2;
			if (num == 0UL)
			{
				*span2[0] = '0';
				num2 = 1;
			}
			else
			{
				num2 = 0;
				for (int i = 0; i < span2.Length; i++)
				{
					ulong num3 = num / (ulong)((long)radix);
					int num4 = (int)(num - num3 * (ulong)((long)radix));
					num = num3;
					*span2[i] = ((num4 < 10) ? ((char)(num4 + 48)) : ((char)(num4 + 97 - 10)));
					if (num == 0UL)
					{
						num2 = i + 1;
						break;
					}
				}
			}
			if (radix != 10 && (flags & 32) != 0)
			{
				if (16 == radix)
				{
					*span2[num2++] = 'x';
					*span2[num2++] = '0';
				}
				else if (8 == radix)
				{
					*span2[num2++] = '0';
				}
				else if ((flags & 16384) != 0)
				{
					*span2[num2++] = '#';
					*span2[num2++] = (char)(radix % 10 + 48);
					*span2[num2++] = (char)(radix / 10 + 48);
				}
			}
			if (10 == radix)
			{
				if (flag)
				{
					*span2[num2++] = '-';
				}
				else if ((flags & 16) != 0)
				{
					*span2[num2++] = '+';
				}
				else if ((flags & 8) != 0)
				{
					*span2[num2++] = ' ';
				}
			}
			string text = string.FastAllocateString(Math.Max(width, num2));
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
			char* ptr4 = ptr3;
			int num5 = text.Length - num2;
			if ((flags & 1) != 0)
			{
				for (int j = 0; j < num5; j++)
				{
					*(ptr4++) = paddingChar;
				}
				for (int k = 0; k < num2; k++)
				{
					*(ptr4++) = *span2[num2 - k - 1];
				}
			}
			else
			{
				for (int l = 0; l < num2; l++)
				{
					*(ptr4++) = *span2[num2 - l - 1];
				}
				for (int m = 0; m < num5; m++)
				{
					*(ptr4++) = paddingChar;
				}
			}
			char* ptr2 = null;
			return text;
		}

		// Token: 0x06001272 RID: 4722 RVA: 0x000E73FC File Offset: 0x000E65FC
		private unsafe static void EatWhiteSpace(ReadOnlySpan<char> s, ref int i)
		{
			int num = i;
			while (num < s.Length && char.IsWhiteSpace((char)(*s[num])))
			{
				num++;
			}
			i = num;
		}

		// Token: 0x06001273 RID: 4723 RVA: 0x000E7430 File Offset: 0x000E6630
		private unsafe static long GrabLongs(int radix, ReadOnlySpan<char> s, ref int i, bool isUnsigned)
		{
			ulong num = 0UL;
			if (radix == 10 && !isUnsigned)
			{
				ulong num2 = 922337203685477580UL;
				int num3;
				while (i < s.Length && ParseNumbers.IsDigit((char)(*s[i]), radix, out num3))
				{
					if (num > num2 || num < 0UL)
					{
						Number.ThrowOverflowException(TypeCode.Int64);
					}
					num = num * (ulong)((long)radix) + (ulong)((long)num3);
					i++;
				}
				if (num < 0UL && num != 9223372036854775808UL)
				{
					Number.ThrowOverflowException(TypeCode.Int64);
				}
			}
			else
			{
				ulong num2 = (radix == 10) ? 1844674407370955161UL : ((radix == 16) ? 1152921504606846975UL : ((radix == 8) ? 2305843009213693951UL : 9223372036854775807UL));
				int num4;
				while (i < s.Length && ParseNumbers.IsDigit((char)(*s[i]), radix, out num4))
				{
					if (num > num2)
					{
						Number.ThrowOverflowException(TypeCode.UInt64);
					}
					ulong num5 = num * (ulong)((long)radix) + (ulong)((long)num4);
					if (num5 < num)
					{
						Number.ThrowOverflowException(TypeCode.UInt64);
					}
					num = num5;
					i++;
				}
			}
			return (long)num;
		}

		// Token: 0x06001274 RID: 4724 RVA: 0x000E7538 File Offset: 0x000E6738
		private unsafe static int GrabInts(int radix, ReadOnlySpan<char> s, ref int i, bool isUnsigned)
		{
			uint num = 0U;
			if (radix == 10 && !isUnsigned)
			{
				uint num2 = 214748364U;
				int num3;
				while (i < s.Length && ParseNumbers.IsDigit((char)(*s[i]), radix, out num3))
				{
					if (num > num2 || num < 0U)
					{
						Number.ThrowOverflowException(TypeCode.Int32);
					}
					num = num * (uint)radix + (uint)num3;
					i++;
				}
				if (num < 0U && num != 2147483648U)
				{
					Number.ThrowOverflowException(TypeCode.Int32);
				}
			}
			else
			{
				uint num2 = (radix == 10) ? 429496729U : ((radix == 16) ? 268435455U : ((radix == 8) ? 536870911U : 2147483647U));
				int num4;
				while (i < s.Length && ParseNumbers.IsDigit((char)(*s[i]), radix, out num4))
				{
					if (num > num2)
					{
						Number.ThrowOverflowException(TypeCode.UInt32);
					}
					uint num5 = num * (uint)radix + (uint)num4;
					if (num5 < num)
					{
						Number.ThrowOverflowException(TypeCode.UInt32);
					}
					num = num5;
					i++;
				}
			}
			return (int)num;
		}

		// Token: 0x06001275 RID: 4725 RVA: 0x000E761C File Offset: 0x000E681C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsDigit(char c, int radix, out int result)
		{
			int num;
			if (c - '0' <= '\t')
			{
				num = (result = (int)(c - '0'));
			}
			else if (c - 'A' <= '\u0019')
			{
				num = (result = (int)(c - 'A' + '\n'));
			}
			else
			{
				if (c - 'a' > '\u0019')
				{
					result = -1;
					return false;
				}
				num = (result = (int)(c - 'a' + '\n'));
			}
			return num < radix;
		}
	}
}
