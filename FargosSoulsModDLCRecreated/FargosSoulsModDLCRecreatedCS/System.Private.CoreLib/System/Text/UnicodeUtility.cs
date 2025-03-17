using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000387 RID: 903
	internal static class UnicodeUtility
	{
		// Token: 0x06002FAD RID: 12205 RVA: 0x00162A3E File Offset: 0x00161C3E
		public static int GetPlane(uint codePoint)
		{
			return (int)(codePoint >> 16);
		}

		// Token: 0x06002FAE RID: 12206 RVA: 0x00162A44 File Offset: 0x00161C44
		public static uint GetScalarFromUtf16SurrogatePair(uint highSurrogateCodePoint, uint lowSurrogateCodePoint)
		{
			return (highSurrogateCodePoint << 10) + lowSurrogateCodePoint - 56613888U;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x00162A52 File Offset: 0x00161C52
		public static int GetUtf16SequenceLength(uint value)
		{
			value -= 65536U;
			value += 33554432U;
			value >>= 24;
			return (int)value;
		}

		// Token: 0x06002FB0 RID: 12208 RVA: 0x00162A6D File Offset: 0x00161C6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void GetUtf16SurrogatesFromSupplementaryPlaneScalar(uint value, out char highSurrogateCodePoint, out char lowSurrogateCodePoint)
		{
			highSurrogateCodePoint = (char)(value + 56557568U >> 10);
			lowSurrogateCodePoint = (char)((value & 1023U) + 56320U);
		}

		// Token: 0x06002FB1 RID: 12209 RVA: 0x00162A8C File Offset: 0x00161C8C
		public static int GetUtf8SequenceLength(uint value)
		{
			int num = (int)(value - 2048U) >> 31;
			value ^= 63488U;
			value -= 63616U;
			value += 67108864U;
			value >>= 24;
			return (int)(value + (uint)(num * 2));
		}

		// Token: 0x06002FB2 RID: 12210 RVA: 0x000CA0DD File Offset: 0x000C92DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsAsciiCodePoint(uint value)
		{
			return value <= 127U;
		}

		// Token: 0x06002FB3 RID: 12211 RVA: 0x00162ACA File Offset: 0x00161CCA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsBmpCodePoint(uint value)
		{
			return value <= 65535U;
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x00162AD7 File Offset: 0x00161CD7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsHighSurrogateCodePoint(uint value)
		{
			return UnicodeUtility.IsInRangeInclusive(value, 55296U, 56319U);
		}

		// Token: 0x06002FB5 RID: 12213 RVA: 0x000CA1D0 File Offset: 0x000C93D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInRangeInclusive(uint value, uint lowerBound, uint upperBound)
		{
			return value - lowerBound <= upperBound - lowerBound;
		}

		// Token: 0x06002FB6 RID: 12214 RVA: 0x00162AE9 File Offset: 0x00161CE9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsLowSurrogateCodePoint(uint value)
		{
			return UnicodeUtility.IsInRangeInclusive(value, 56320U, 57343U);
		}

		// Token: 0x06002FB7 RID: 12215 RVA: 0x00162AFB File Offset: 0x00161CFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsSurrogateCodePoint(uint value)
		{
			return UnicodeUtility.IsInRangeInclusive(value, 55296U, 57343U);
		}

		// Token: 0x06002FB8 RID: 12216 RVA: 0x00162B0D File Offset: 0x00161D0D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidCodePoint(uint codePoint)
		{
			return codePoint <= 1114111U;
		}

		// Token: 0x06002FB9 RID: 12217 RVA: 0x00162B1A File Offset: 0x00161D1A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsValidUnicodeScalar(uint value)
		{
			return (value - 1114112U ^ 55296U) >= 4293855232U;
		}
	}
}
