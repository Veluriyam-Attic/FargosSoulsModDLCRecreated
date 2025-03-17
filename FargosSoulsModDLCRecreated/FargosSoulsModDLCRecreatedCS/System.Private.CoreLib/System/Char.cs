using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using System.Text;

namespace System
{
	// Token: 0x020000DA RID: 218
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct Char : IComparable, IComparable<char>, IEquatable<char>, IConvertible
	{
		// Token: 0x1700011E RID: 286
		// (get) Token: 0x06000B1D RID: 2845 RVA: 0x000CA0AF File Offset: 0x000C92AF
		[Nullable(0)]
		private unsafe static ReadOnlySpan<byte> Latin1CharInfo
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.1D715D2A2ED1CDD8C368F519DF4B8B9748F65E031AEA80652432FBBA5C35DFE6), 256);
			}
		}

		// Token: 0x06000B1E RID: 2846 RVA: 0x000CA0C0 File Offset: 0x000C92C0
		private static bool IsLatin1(char ch)
		{
			return (int)ch < char.Latin1CharInfo.Length;
		}

		// Token: 0x06000B1F RID: 2847 RVA: 0x000CA0DD File Offset: 0x000C92DD
		private static bool IsAscii(char ch)
		{
			return ch <= '\u007f';
		}

		// Token: 0x06000B20 RID: 2848 RVA: 0x000CA0E8 File Offset: 0x000C92E8
		private unsafe static UnicodeCategory GetLatin1UnicodeCategory(char ch)
		{
			return (UnicodeCategory)(*char.Latin1CharInfo[(int)ch] & 31);
		}

		// Token: 0x06000B21 RID: 2849 RVA: 0x000CA107 File Offset: 0x000C9307
		public override int GetHashCode()
		{
			return (int)(this | (int)this << 16);
		}

		// Token: 0x06000B22 RID: 2850 RVA: 0x000CA111 File Offset: 0x000C9311
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is char && this == (char)obj;
		}

		// Token: 0x06000B23 RID: 2851 RVA: 0x000CA127 File Offset: 0x000C9327
		[NonVersionable]
		public bool Equals(char obj)
		{
			return this == obj;
		}

		// Token: 0x06000B24 RID: 2852 RVA: 0x000CA12E File Offset: 0x000C932E
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is char))
			{
				throw new ArgumentException(SR.Arg_MustBeChar);
			}
			return (int)(this - (char)value);
		}

		// Token: 0x06000B25 RID: 2853 RVA: 0x000CA151 File Offset: 0x000C9351
		public int CompareTo(char value)
		{
			return (int)(this - value);
		}

		// Token: 0x06000B26 RID: 2854 RVA: 0x000CA157 File Offset: 0x000C9357
		public override string ToString()
		{
			return char.ToString(this);
		}

		// Token: 0x06000B27 RID: 2855 RVA: 0x000CA157 File Offset: 0x000C9357
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return char.ToString(this);
		}

		// Token: 0x06000B28 RID: 2856 RVA: 0x000CA160 File Offset: 0x000C9360
		public static string ToString(char c)
		{
			return string.CreateFromChar(c);
		}

		// Token: 0x06000B29 RID: 2857 RVA: 0x000CA168 File Offset: 0x000C9368
		public static char Parse(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (s.Length != 1)
			{
				throw new FormatException(SR.Format_NeedSingleChar);
			}
			return s[0];
		}

		// Token: 0x06000B2A RID: 2858 RVA: 0x000CA193 File Offset: 0x000C9393
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out char result)
		{
			result = '\0';
			if (s == null)
			{
				return false;
			}
			if (s.Length != 1)
			{
				return false;
			}
			result = s[0];
			return true;
		}

		// Token: 0x06000B2B RID: 2859 RVA: 0x000CA1B2 File Offset: 0x000C93B2
		public static bool IsDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsInRange(c, '0', '9');
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000B2C RID: 2860 RVA: 0x000CA1D0 File Offset: 0x000C93D0
		internal static bool IsInRange(char c, char min, char max)
		{
			return c - min <= max - min;
		}

		// Token: 0x06000B2D RID: 2861 RVA: 0x000CA1D0 File Offset: 0x000C93D0
		private static bool IsInRange(UnicodeCategory c, UnicodeCategory min, UnicodeCategory max)
		{
			return c - min <= max - min;
		}

		// Token: 0x06000B2E RID: 2862 RVA: 0x000CA1DD File Offset: 0x000C93DD
		internal static bool CheckLetter(UnicodeCategory uc)
		{
			return char.IsInRange(uc, UnicodeCategory.UppercaseLetter, UnicodeCategory.OtherLetter);
		}

		// Token: 0x06000B2F RID: 2863 RVA: 0x000CA1E8 File Offset: 0x000C93E8
		public unsafe static bool IsLetter(char c)
		{
			if (char.IsAscii(c))
			{
				return (*char.Latin1CharInfo[(int)c] & 96) > 0;
			}
			return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000B30 RID: 2864 RVA: 0x000CA220 File Offset: 0x000C9420
		private unsafe static bool IsWhiteSpaceLatin1(char c)
		{
			return (*char.Latin1CharInfo[(int)c] & 128) > 0;
		}

		// Token: 0x06000B31 RID: 2865 RVA: 0x000CA245 File Offset: 0x000C9445
		public static bool IsWhiteSpace(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsWhiteSpaceLatin1(c);
			}
			return CharUnicodeInfo.GetIsWhiteSpace(c);
		}

		// Token: 0x06000B32 RID: 2866 RVA: 0x000CA25C File Offset: 0x000C945C
		public unsafe static bool IsUpper(char c)
		{
			if (char.IsLatin1(c))
			{
				return (*char.Latin1CharInfo[(int)c] & 64) > 0;
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.UppercaseLetter;
		}

		// Token: 0x06000B33 RID: 2867 RVA: 0x000CA290 File Offset: 0x000C9490
		public unsafe static bool IsLower(char c)
		{
			if (char.IsLatin1(c))
			{
				return (*char.Latin1CharInfo[(int)c] & 32) > 0;
			}
			return CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000B34 RID: 2868 RVA: 0x000CA2C4 File Offset: 0x000C94C4
		internal static bool CheckPunctuation(UnicodeCategory uc)
		{
			return char.IsInRange(uc, UnicodeCategory.ConnectorPunctuation, UnicodeCategory.OtherPunctuation);
		}

		// Token: 0x06000B35 RID: 2869 RVA: 0x000CA2D0 File Offset: 0x000C94D0
		public static bool IsPunctuation(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000B36 RID: 2870 RVA: 0x000CA2F1 File Offset: 0x000C94F1
		internal static bool CheckLetterOrDigit(UnicodeCategory uc)
		{
			return char.CheckLetter(uc) || uc == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000B37 RID: 2871 RVA: 0x000CA301 File Offset: 0x000C9501
		public static bool IsLetterOrDigit(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000B38 RID: 2872 RVA: 0x000CA322 File Offset: 0x000C9522
		public static char ToUpper(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToUpper(c);
		}

		// Token: 0x06000B39 RID: 2873 RVA: 0x000CA33E File Offset: 0x000C953E
		public static char ToUpper(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToUpper(c);
		}

		// Token: 0x06000B3A RID: 2874 RVA: 0x000CA350 File Offset: 0x000C9550
		public static char ToUpperInvariant(char c)
		{
			return TextInfo.ToUpperInvariant(c);
		}

		// Token: 0x06000B3B RID: 2875 RVA: 0x000CA358 File Offset: 0x000C9558
		public static char ToLower(char c, CultureInfo culture)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return culture.TextInfo.ToLower(c);
		}

		// Token: 0x06000B3C RID: 2876 RVA: 0x000CA374 File Offset: 0x000C9574
		public static char ToLower(char c)
		{
			return CultureInfo.CurrentCulture.TextInfo.ToLower(c);
		}

		// Token: 0x06000B3D RID: 2877 RVA: 0x000CA386 File Offset: 0x000C9586
		public static char ToLowerInvariant(char c)
		{
			return TextInfo.ToLowerInvariant(c);
		}

		// Token: 0x06000B3E RID: 2878 RVA: 0x000CA38E File Offset: 0x000C958E
		public TypeCode GetTypeCode()
		{
			return TypeCode.Char;
		}

		// Token: 0x06000B3F RID: 2879 RVA: 0x000CA391 File Offset: 0x000C9591
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Char", "Boolean"));
		}

		// Token: 0x06000B40 RID: 2880 RVA: 0x000CA3AC File Offset: 0x000C95AC
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000B41 RID: 2881 RVA: 0x000CA3B0 File Offset: 0x000C95B0
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000B42 RID: 2882 RVA: 0x000CA3B9 File Offset: 0x000C95B9
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000B43 RID: 2883 RVA: 0x000CA3C2 File Offset: 0x000C95C2
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000B44 RID: 2884 RVA: 0x000CA3CB File Offset: 0x000C95CB
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000B45 RID: 2885 RVA: 0x000CA3D4 File Offset: 0x000C95D4
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000B46 RID: 2886 RVA: 0x000CA3DD File Offset: 0x000C95DD
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000B47 RID: 2887 RVA: 0x000CA3E6 File Offset: 0x000C95E6
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000B48 RID: 2888 RVA: 0x000CA3EF File Offset: 0x000C95EF
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000B49 RID: 2889 RVA: 0x000CA3F8 File Offset: 0x000C95F8
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Char", "Single"));
		}

		// Token: 0x06000B4A RID: 2890 RVA: 0x000CA413 File Offset: 0x000C9613
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Char", "Double"));
		}

		// Token: 0x06000B4B RID: 2891 RVA: 0x000CA42E File Offset: 0x000C962E
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Char", "Decimal"));
		}

		// Token: 0x06000B4C RID: 2892 RVA: 0x000CA449 File Offset: 0x000C9649
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Char", "DateTime"));
		}

		// Token: 0x06000B4D RID: 2893 RVA: 0x000CA464 File Offset: 0x000C9664
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x06000B4E RID: 2894 RVA: 0x000CA474 File Offset: 0x000C9674
		public static bool IsControl(char c)
		{
			return ((int)(c + '\u0001') & -129) <= 32;
		}

		// Token: 0x06000B4F RID: 2895 RVA: 0x000CA486 File Offset: 0x000C9686
		public static bool IsControl(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsControl(s[index]);
		}

		// Token: 0x06000B50 RID: 2896 RVA: 0x000CA4B8 File Offset: 0x000C96B8
		public static bool IsDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.IsInRange(c, '0', '9');
			}
			return CharUnicodeInfo.GetUnicodeCategoryInternal(s, index) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06000B51 RID: 2897 RVA: 0x000CA50C File Offset: 0x000C970C
		public unsafe static bool IsLetter(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsAscii(c))
			{
				return (*char.Latin1CharInfo[(int)c] & 96) > 0;
			}
			return char.CheckLetter(CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
		}

		// Token: 0x06000B52 RID: 2898 RVA: 0x000CA570 File Offset: 0x000C9770
		public static bool IsLetterOrDigit(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.CheckLetterOrDigit(char.GetLatin1UnicodeCategory(ch));
			}
			return char.CheckLetterOrDigit(CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
		}

		// Token: 0x06000B53 RID: 2899 RVA: 0x000CA5C8 File Offset: 0x000C97C8
		public unsafe static bool IsLower(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return (*char.Latin1CharInfo[(int)c] & 32) > 0;
			}
			return CharUnicodeInfo.GetUnicodeCategoryInternal(s, index) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06000B54 RID: 2900 RVA: 0x000CA627 File Offset: 0x000C9827
		internal static bool CheckNumber(UnicodeCategory uc)
		{
			return char.IsInRange(uc, UnicodeCategory.DecimalDigitNumber, UnicodeCategory.OtherNumber);
		}

		// Token: 0x06000B55 RID: 2901 RVA: 0x000CA632 File Offset: 0x000C9832
		public static bool IsNumber(char c)
		{
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategory(c));
			}
			if (char.IsAscii(c))
			{
				return char.IsInRange(c, '0', '9');
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000B56 RID: 2902 RVA: 0x000CA668 File Offset: 0x000C9868
		public static bool IsNumber(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (!char.IsLatin1(c))
			{
				return char.CheckNumber(CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
			}
			if (char.IsAscii(c))
			{
				return char.IsInRange(c, '0', '9');
			}
			return char.CheckNumber(char.GetLatin1UnicodeCategory(c));
		}

		// Token: 0x06000B57 RID: 2903 RVA: 0x000CA6D4 File Offset: 0x000C98D4
		public static bool IsPunctuation(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.CheckPunctuation(char.GetLatin1UnicodeCategory(ch));
			}
			return char.CheckPunctuation(CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
		}

		// Token: 0x06000B58 RID: 2904 RVA: 0x000CA72B File Offset: 0x000C992B
		internal static bool CheckSeparator(UnicodeCategory uc)
		{
			return char.IsInRange(uc, UnicodeCategory.SpaceSeparator, UnicodeCategory.ParagraphSeparator);
		}

		// Token: 0x06000B59 RID: 2905 RVA: 0x000CA737 File Offset: 0x000C9937
		private static bool IsSeparatorLatin1(char c)
		{
			return c == ' ' || c == '\u00a0';
		}

		// Token: 0x06000B5A RID: 2906 RVA: 0x000CA748 File Offset: 0x000C9948
		public static bool IsSeparator(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000B5B RID: 2907 RVA: 0x000CA764 File Offset: 0x000C9964
		public static bool IsSeparator(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return char.IsSeparatorLatin1(c);
			}
			return char.CheckSeparator(CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
		}

		// Token: 0x06000B5C RID: 2908 RVA: 0x000CA7B6 File Offset: 0x000C99B6
		public static bool IsSurrogate(char c)
		{
			return char.IsInRange(c, '\ud800', '\udfff');
		}

		// Token: 0x06000B5D RID: 2909 RVA: 0x000CA7C8 File Offset: 0x000C99C8
		public static bool IsSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsSurrogate(s[index]);
		}

		// Token: 0x06000B5E RID: 2910 RVA: 0x000CA7F8 File Offset: 0x000C99F8
		internal static bool CheckSymbol(UnicodeCategory uc)
		{
			return char.IsInRange(uc, UnicodeCategory.MathSymbol, UnicodeCategory.OtherSymbol);
		}

		// Token: 0x06000B5F RID: 2911 RVA: 0x000CA804 File Offset: 0x000C9A04
		public static bool IsSymbol(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(c));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategory(c));
		}

		// Token: 0x06000B60 RID: 2912 RVA: 0x000CA828 File Offset: 0x000C9A28
		public static bool IsSymbol(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char ch = s[index];
			if (char.IsLatin1(ch))
			{
				return char.CheckSymbol(char.GetLatin1UnicodeCategory(ch));
			}
			return char.CheckSymbol(CharUnicodeInfo.GetUnicodeCategoryInternal(s, index));
		}

		// Token: 0x06000B61 RID: 2913 RVA: 0x000CA880 File Offset: 0x000C9A80
		public unsafe static bool IsUpper(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			char c = s[index];
			if (char.IsLatin1(c))
			{
				return (*char.Latin1CharInfo[(int)c] & 64) > 0;
			}
			return CharUnicodeInfo.GetUnicodeCategoryInternal(s, index) == UnicodeCategory.UppercaseLetter;
		}

		// Token: 0x06000B62 RID: 2914 RVA: 0x000CA8DF File Offset: 0x000C9ADF
		public static bool IsWhiteSpace(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsWhiteSpace(s[index]);
		}

		// Token: 0x06000B63 RID: 2915 RVA: 0x000CA90F File Offset: 0x000C9B0F
		public static UnicodeCategory GetUnicodeCategory(char c)
		{
			if (char.IsLatin1(c))
			{
				return char.GetLatin1UnicodeCategory(c);
			}
			return CharUnicodeInfo.GetUnicodeCategory((int)c);
		}

		// Token: 0x06000B64 RID: 2916 RVA: 0x000CA928 File Offset: 0x000C9B28
		public static UnicodeCategory GetUnicodeCategory(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (char.IsLatin1(s[index]))
			{
				return char.GetLatin1UnicodeCategory(s[index]);
			}
			return CharUnicodeInfo.GetUnicodeCategoryInternal(s, index);
		}

		// Token: 0x06000B65 RID: 2917 RVA: 0x000CA979 File Offset: 0x000C9B79
		public static double GetNumericValue(char c)
		{
			return CharUnicodeInfo.GetNumericValue(c);
		}

		// Token: 0x06000B66 RID: 2918 RVA: 0x000CA981 File Offset: 0x000C9B81
		public static double GetNumericValue(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return CharUnicodeInfo.GetNumericValueInternal(s, index);
		}

		// Token: 0x06000B67 RID: 2919 RVA: 0x000CA9AC File Offset: 0x000C9BAC
		public static bool IsHighSurrogate(char c)
		{
			return char.IsInRange(c, '\ud800', '\udbff');
		}

		// Token: 0x06000B68 RID: 2920 RVA: 0x000CA9BE File Offset: 0x000C9BBE
		public static bool IsHighSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsHighSurrogate(s[index]);
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x000CA9F2 File Offset: 0x000C9BF2
		public static bool IsLowSurrogate(char c)
		{
			return char.IsInRange(c, '\udc00', '\udfff');
		}

		// Token: 0x06000B6A RID: 2922 RVA: 0x000CAA04 File Offset: 0x000C9C04
		public static bool IsLowSurrogate(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return char.IsLowSurrogate(s[index]);
		}

		// Token: 0x06000B6B RID: 2923 RVA: 0x000CAA38 File Offset: 0x000C9C38
		public static bool IsSurrogatePair(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			return index + 1 < s.Length && char.IsSurrogatePair(s[index], s[index + 1]);
		}

		// Token: 0x06000B6C RID: 2924 RVA: 0x000CAA90 File Offset: 0x000C9C90
		public static bool IsSurrogatePair(char highSurrogate, char lowSurrogate)
		{
			uint num = (uint)(highSurrogate - '\ud800');
			uint num2 = (uint)(lowSurrogate - '\udc00');
			return (num | num2) <= 1023U;
		}

		// Token: 0x06000B6D RID: 2925 RVA: 0x000CAABC File Offset: 0x000C9CBC
		public static string ConvertFromUtf32(int utf32)
		{
			if (!UnicodeUtility.IsValidUnicodeScalar((uint)utf32))
			{
				throw new ArgumentOutOfRangeException("utf32", SR.ArgumentOutOfRange_InvalidUTF32);
			}
			return Rune.UnsafeCreate((uint)utf32).ToString();
		}

		// Token: 0x06000B6E RID: 2926 RVA: 0x000CAAF8 File Offset: 0x000C9CF8
		public static int ConvertToUtf32(char highSurrogate, char lowSurrogate)
		{
			uint num = (uint)(highSurrogate - '\ud800');
			uint num2 = (uint)(lowSurrogate - '\udc00');
			if ((num | num2) > 1023U)
			{
				char.ConvertToUtf32_ThrowInvalidArgs(num);
			}
			return (int)((num << 10) + (uint)(lowSurrogate - '\udc00') + 65536U);
		}

		// Token: 0x06000B6F RID: 2927 RVA: 0x000CAB37 File Offset: 0x000C9D37
		[StackTraceHidden]
		private static void ConvertToUtf32_ThrowInvalidArgs(uint highSurrogateOffset)
		{
			if (highSurrogateOffset > 1023U)
			{
				throw new ArgumentOutOfRangeException("highSurrogate", SR.ArgumentOutOfRange_InvalidHighSurrogate);
			}
			throw new ArgumentOutOfRangeException("lowSurrogate", SR.ArgumentOutOfRange_InvalidLowSurrogate);
		}

		// Token: 0x06000B70 RID: 2928 RVA: 0x000CAB60 File Offset: 0x000C9D60
		public static int ConvertToUtf32(string s, int index)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			if (index < 0 || index >= s.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			int num = (int)(s[index] - '\ud800');
			if (num < 0 || num > 2047)
			{
				return (int)s[index];
			}
			if (num > 1023)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidLowSurrogate, index), "s");
			}
			if (index >= s.Length - 1)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidHighSurrogate, index), "s");
			}
			int num2 = (int)(s[index + 1] - '\udc00');
			if (num2 >= 0 && num2 <= 1023)
			{
				return num * 1024 + num2 + 65536;
			}
			throw new ArgumentException(SR.Format(SR.Argument_InvalidHighSurrogate, index), "s");
		}

		// Token: 0x040002A9 RID: 681
		private readonly char m_value;

		// Token: 0x040002AA RID: 682
		public const char MaxValue = '￿';

		// Token: 0x040002AB RID: 683
		public const char MinValue = '\0';
	}
}
