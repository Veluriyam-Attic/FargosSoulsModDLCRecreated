using System;
using System.Buffers.Binary;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.Unicode;
using Internal.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x020001E6 RID: 486
	public static class CharUnicodeInfo
	{
		// Token: 0x06001E3E RID: 7742 RVA: 0x0011F9FA File Offset: 0x0011EBFA
		internal static StrongBidiCategory GetBidiCategory(string s, int index)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (index >= s.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			return CharUnicodeInfo.GetBidiCategoryNoBoundsChecks((uint)CharUnicodeInfo.GetCodePointFromString(s, index));
		}

		// Token: 0x06001E3F RID: 7743 RVA: 0x0011FA24 File Offset: 0x0011EC24
		internal static StrongBidiCategory GetBidiCategory(StringBuilder s, int index)
		{
			int num = (int)s[index];
			if (index < s.Length - 1)
			{
				int num2 = num - 55296;
				if (num2 <= 1023)
				{
					int num3 = (int)(s[index + 1] - '\udc00');
					if (num3 <= 1023)
					{
						num = (num2 << 10) + num3 + 65536;
					}
				}
			}
			return CharUnicodeInfo.GetBidiCategoryNoBoundsChecks((uint)num);
		}

		// Token: 0x06001E40 RID: 7744 RVA: 0x0011FA80 File Offset: 0x0011EC80
		private unsafe static StrongBidiCategory GetBidiCategoryNoBoundsChecks(uint codePoint)
		{
			UIntPtr categoryCasingTableOffsetNoBoundsChecks = CharUnicodeInfo.GetCategoryCasingTableOffsetNoBoundsChecks(codePoint);
			return (StrongBidiCategory)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.CategoriesValues), categoryCasingTableOffsetNoBoundsChecks) & 96);
		}

		// Token: 0x06001E41 RID: 7745 RVA: 0x0011FAAA File Offset: 0x0011ECAA
		public static int GetDecimalDigitValue(char ch)
		{
			return CharUnicodeInfo.GetDecimalDigitValueInternalNoBoundsCheck((uint)ch);
		}

		// Token: 0x06001E42 RID: 7746 RVA: 0x0011FAB2 File Offset: 0x0011ECB2
		[NullableContext(1)]
		public static int GetDecimalDigitValue(string s, int index)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (index >= s.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			return CharUnicodeInfo.GetDecimalDigitValueInternalNoBoundsCheck((uint)CharUnicodeInfo.GetCodePointFromString(s, index));
		}

		// Token: 0x06001E43 RID: 7747 RVA: 0x0011FADC File Offset: 0x0011ECDC
		private unsafe static int GetDecimalDigitValueInternalNoBoundsCheck(uint codePoint)
		{
			UIntPtr numericGraphemeTableOffsetNoBoundsChecks = CharUnicodeInfo.GetNumericGraphemeTableOffsetNoBoundsChecks(codePoint);
			uint num = (uint)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.DigitValues), numericGraphemeTableOffsetNoBoundsChecks));
			return (int)((num >> 4) - 1U);
		}

		// Token: 0x06001E44 RID: 7748 RVA: 0x0011FB07 File Offset: 0x0011ED07
		public static int GetDigitValue(char ch)
		{
			return CharUnicodeInfo.GetDigitValueInternalNoBoundsCheck((uint)ch);
		}

		// Token: 0x06001E45 RID: 7749 RVA: 0x0011FB0F File Offset: 0x0011ED0F
		[NullableContext(1)]
		public static int GetDigitValue(string s, int index)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (index >= s.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			return CharUnicodeInfo.GetDigitValueInternalNoBoundsCheck((uint)CharUnicodeInfo.GetCodePointFromString(s, index));
		}

		// Token: 0x06001E46 RID: 7750 RVA: 0x0011FB38 File Offset: 0x0011ED38
		private unsafe static int GetDigitValueInternalNoBoundsCheck(uint codePoint)
		{
			UIntPtr numericGraphemeTableOffsetNoBoundsChecks = CharUnicodeInfo.GetNumericGraphemeTableOffsetNoBoundsChecks(codePoint);
			int num = (int)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.DigitValues), numericGraphemeTableOffsetNoBoundsChecks));
			return (num & 15) - 1;
		}

		// Token: 0x06001E47 RID: 7751 RVA: 0x0011FB64 File Offset: 0x0011ED64
		internal unsafe static GraphemeClusterBreakType GetGraphemeClusterBreakType(Rune rune)
		{
			UIntPtr numericGraphemeTableOffsetNoBoundsChecks = CharUnicodeInfo.GetNumericGraphemeTableOffsetNoBoundsChecks((uint)rune.Value);
			return (GraphemeClusterBreakType)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.GraphemeSegmentationValues), numericGraphemeTableOffsetNoBoundsChecks));
		}

		// Token: 0x06001E48 RID: 7752 RVA: 0x0011FB90 File Offset: 0x0011ED90
		internal unsafe static bool GetIsWhiteSpace(char ch)
		{
			UIntPtr categoryCasingTableOffsetNoBoundsChecks = CharUnicodeInfo.GetCategoryCasingTableOffsetNoBoundsChecks((uint)ch);
			return (sbyte)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.CategoriesValues), categoryCasingTableOffsetNoBoundsChecks)) < 0;
		}

		// Token: 0x06001E49 RID: 7753 RVA: 0x0011FBB9 File Offset: 0x0011EDB9
		public static double GetNumericValue(char ch)
		{
			return CharUnicodeInfo.GetNumericValueNoBoundsCheck((uint)ch);
		}

		// Token: 0x06001E4A RID: 7754 RVA: 0x0011FBC1 File Offset: 0x0011EDC1
		internal static double GetNumericValue(int codePoint)
		{
			if (!UnicodeUtility.IsValidCodePoint((uint)codePoint))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.codePoint);
			}
			return CharUnicodeInfo.GetNumericValueNoBoundsCheck((uint)codePoint);
		}

		// Token: 0x06001E4B RID: 7755 RVA: 0x0011FBD8 File Offset: 0x0011EDD8
		[NullableContext(1)]
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (index >= s.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			return CharUnicodeInfo.GetNumericValueInternal(s, index);
		}

		// Token: 0x06001E4C RID: 7756 RVA: 0x0011FBFB File Offset: 0x0011EDFB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static double GetNumericValueInternal(string s, int index)
		{
			return CharUnicodeInfo.GetNumericValueNoBoundsCheck((uint)CharUnicodeInfo.GetCodePointFromString(s, index));
		}

		// Token: 0x06001E4D RID: 7757 RVA: 0x0011FC0C File Offset: 0x0011EE0C
		private unsafe static double GetNumericValueNoBoundsCheck(uint codePoint)
		{
			UIntPtr numericGraphemeTableOffsetNoBoundsChecks = CharUnicodeInfo.GetNumericGraphemeTableOffsetNoBoundsChecks(codePoint);
			ref byte source = ref Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.NumericValues), numericGraphemeTableOffsetNoBoundsChecks * (UIntPtr)((IntPtr)8));
			if (BitConverter.IsLittleEndian)
			{
				return Unsafe.ReadUnaligned<double>(ref source);
			}
			ulong value = Unsafe.ReadUnaligned<ulong>(ref source);
			value = BinaryPrimitives.ReverseEndianness(value);
			return *Unsafe.As<ulong, double>(ref value);
		}

		// Token: 0x06001E4E RID: 7758 RVA: 0x0011FC58 File Offset: 0x0011EE58
		public static UnicodeCategory GetUnicodeCategory(char ch)
		{
			return CharUnicodeInfo.GetUnicodeCategoryNoBoundsChecks((uint)ch);
		}

		// Token: 0x06001E4F RID: 7759 RVA: 0x0011FC60 File Offset: 0x0011EE60
		public static UnicodeCategory GetUnicodeCategory(int codePoint)
		{
			if (!UnicodeUtility.IsValidCodePoint((uint)codePoint))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.codePoint);
			}
			return CharUnicodeInfo.GetUnicodeCategoryNoBoundsChecks((uint)codePoint);
		}

		// Token: 0x06001E50 RID: 7760 RVA: 0x0011FC77 File Offset: 0x0011EE77
		[NullableContext(1)]
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			if (index >= s.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			return CharUnicodeInfo.GetUnicodeCategoryInternal(s, index);
		}

		// Token: 0x06001E51 RID: 7761 RVA: 0x0011FC9A File Offset: 0x0011EE9A
		internal static UnicodeCategory GetUnicodeCategoryInternal(string value, int index)
		{
			return CharUnicodeInfo.GetUnicodeCategoryNoBoundsChecks((uint)CharUnicodeInfo.GetCodePointFromString(value, index));
		}

		// Token: 0x06001E52 RID: 7762 RVA: 0x0011FCA8 File Offset: 0x0011EEA8
		internal static UnicodeCategory GetUnicodeCategoryInternal(string str, int index, out int charLength)
		{
			uint codePointFromString = (uint)CharUnicodeInfo.GetCodePointFromString(str, index);
			charLength = ((codePointFromString >= 65536U) ? 2 : 1);
			return CharUnicodeInfo.GetUnicodeCategoryNoBoundsChecks(codePointFromString);
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x0011FCD4 File Offset: 0x0011EED4
		private unsafe static UnicodeCategory GetUnicodeCategoryNoBoundsChecks(uint codePoint)
		{
			UIntPtr categoryCasingTableOffsetNoBoundsChecks = CharUnicodeInfo.GetCategoryCasingTableOffsetNoBoundsChecks(codePoint);
			return (UnicodeCategory)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.CategoriesValues), categoryCasingTableOffsetNoBoundsChecks) & 31);
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x0011FCFC File Offset: 0x0011EEFC
		private static int GetCodePointFromString(string s, int index)
		{
			int num = 0;
			if (index < s.Length)
			{
				num = (int)s[index];
				int num2 = num - 55296;
				if (num2 <= 1023)
				{
					index++;
					if (index < s.Length)
					{
						int num3 = (int)(s[index] - '\udc00');
						if (num3 <= 1023)
						{
							num = (num2 << 10) + num3 + 65536;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x0011FD60 File Offset: 0x0011EF60
		[return: NativeInteger]
		private unsafe static UIntPtr GetCategoryCasingTableOffsetNoBoundsChecks(uint codePoint)
		{
			uint num = (uint)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.CategoryCasingLevel1Index), (UIntPtr)(codePoint >> 9)));
			ref byte source = ref Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.CategoryCasingLevel2Index), (UIntPtr)((num << 6) + (codePoint >> 3 & 62U)));
			if (BitConverter.IsLittleEndian)
			{
				num = (uint)Unsafe.ReadUnaligned<ushort>(ref source);
			}
			else
			{
				num = (uint)BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<ushort>(ref source));
			}
			return (UIntPtr)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.CategoryCasingLevel3Index), (UIntPtr)((num << 4) + (codePoint & 15U))));
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x0011FDD4 File Offset: 0x0011EFD4
		[return: NativeInteger]
		private unsafe static UIntPtr GetNumericGraphemeTableOffsetNoBoundsChecks(uint codePoint)
		{
			uint num = (uint)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.NumericGraphemeLevel1Index), (UIntPtr)(codePoint >> 9)));
			ref byte source = ref Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.NumericGraphemeLevel2Index), (UIntPtr)((num << 6) + (codePoint >> 3 & 62U)));
			if (BitConverter.IsLittleEndian)
			{
				num = (uint)Unsafe.ReadUnaligned<ushort>(ref source);
			}
			else
			{
				num = (uint)BinaryPrimitives.ReverseEndianness(Unsafe.ReadUnaligned<ushort>(ref source));
			}
			return (UIntPtr)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(CharUnicodeInfo.NumericGraphemeLevel3Index), (UIntPtr)((num << 4) + (codePoint & 15U))));
		}

		// Token: 0x17000685 RID: 1669
		// (get) Token: 0x06001E57 RID: 7767 RVA: 0x0011FE48 File Offset: 0x0011F048
		private unsafe static ReadOnlySpan<byte> CategoryCasingLevel1Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.2543C65A207717323ADAAF85A5AFA466FF3E161CEBA23FD930D8C07B4E1F10C5), 2176);
			}
		}

		// Token: 0x17000686 RID: 1670
		// (get) Token: 0x06001E58 RID: 7768 RVA: 0x0011FE59 File Offset: 0x0011F059
		private unsafe static ReadOnlySpan<byte> CategoryCasingLevel2Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.E0885FD045CE86EE57B48DCF6083657177BCB84C61E593E71C0E25F4BE2A988D), 6272);
			}
		}

		// Token: 0x17000687 RID: 1671
		// (get) Token: 0x06001E59 RID: 7769 RVA: 0x0011FE6A File Offset: 0x0011F06A
		private unsafe static ReadOnlySpan<byte> CategoryCasingLevel3Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.9231080865B594C6C56D86692323787BE98F8C6A0B84B7E5D4911A9E48A12183), 11184);
			}
		}

		// Token: 0x17000688 RID: 1672
		// (get) Token: 0x06001E5A RID: 7770 RVA: 0x0011FE7B File Offset: 0x0011F07B
		private unsafe static ReadOnlySpan<byte> CategoriesValues
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.86820D359F56E64EC4DBA72775510DA9DCB3FEE9AA89CC68AB73AD04A8C286CD), 56);
			}
		}

		// Token: 0x17000689 RID: 1673
		// (get) Token: 0x06001E5B RID: 7771 RVA: 0x0011FE89 File Offset: 0x0011F089
		private unsafe static ReadOnlySpan<byte> NumericGraphemeLevel1Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.A804C0FCBEB1F95E8B0833D950BAC70B2B79AF6DD22A969E1D0372CBB7A1E7E9), 2176);
			}
		}

		// Token: 0x1700068A RID: 1674
		// (get) Token: 0x06001E5C RID: 7772 RVA: 0x0011FE9A File Offset: 0x0011F09A
		private unsafe static ReadOnlySpan<byte> NumericGraphemeLevel2Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.2682DCA9BFBD11689C71C18593548106F649D40ACB1152538869CDD92AE55085), 4928);
			}
		}

		// Token: 0x1700068B RID: 1675
		// (get) Token: 0x06001E5D RID: 7773 RVA: 0x0011FEAB File Offset: 0x0011F0AB
		private unsafe static ReadOnlySpan<byte> NumericGraphemeLevel3Index
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.EC7551C312A9124B2A76A3FCFC4DD8A7C9ED206B9EC240A2D55937CAE5DC0EDE), 6096);
			}
		}

		// Token: 0x1700068C RID: 1676
		// (get) Token: 0x06001E5E RID: 7774 RVA: 0x0011FEBC File Offset: 0x0011F0BC
		private unsafe static ReadOnlySpan<byte> DigitValues
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.2FF31353E83C9BDB78A790B8A02664AB21F1E89964732C21F2C164D01CDD39DC), 177);
			}
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001E5F RID: 7775 RVA: 0x0011FECD File Offset: 0x0011F0CD
		private unsafe static ReadOnlySpan<byte> NumericValues
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.0107A6D71D367FA901A73B2343D07B198CBB249E11715E27F2A077A666D82476), 1416);
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001E60 RID: 7776 RVA: 0x0011FEDE File Offset: 0x0011F0DE
		private unsafe static ReadOnlySpan<byte> GraphemeSegmentationValues
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F20934FF721BC3C50DEFC87EDD892E3335DC8C209D9D64A7C81E696777EF00B7), 177);
			}
		}
	}
}
