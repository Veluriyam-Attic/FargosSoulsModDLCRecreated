using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200017C RID: 380
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct Single : IComparable, IConvertible, IFormattable, IComparable<float>, IEquatable<float>, ISpanFormattable
	{
		// Token: 0x060012FD RID: 4861 RVA: 0x000E8B34 File Offset: 0x000E7D34
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFinite(float f)
		{
			int num = BitConverter.SingleToInt32Bits(f);
			return (num & int.MaxValue) < 2139095040;
		}

		// Token: 0x060012FE RID: 4862 RVA: 0x000E8B58 File Offset: 0x000E7D58
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInfinity(float f)
		{
			int num = BitConverter.SingleToInt32Bits(f);
			return (num & int.MaxValue) == 2139095040;
		}

		// Token: 0x060012FF RID: 4863 RVA: 0x000CFB36 File Offset: 0x000CED36
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(float f)
		{
			return f != f;
		}

		// Token: 0x06001300 RID: 4864 RVA: 0x000E8B7A File Offset: 0x000E7D7A
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegative(float f)
		{
			return BitConverter.SingleToInt32Bits(f) < 0;
		}

		// Token: 0x06001301 RID: 4865 RVA: 0x000E8B85 File Offset: 0x000E7D85
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegativeInfinity(float f)
		{
			return f == float.NegativeInfinity;
		}

		// Token: 0x06001302 RID: 4866 RVA: 0x000E8B90 File Offset: 0x000E7D90
		[NonVersionable]
		public static bool IsNormal(float f)
		{
			int num = BitConverter.SingleToInt32Bits(f);
			num &= int.MaxValue;
			return num < 2139095040 && num != 0 && (num & 2139095040) != 0;
		}

		// Token: 0x06001303 RID: 4867 RVA: 0x000E8BC3 File Offset: 0x000E7DC3
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPositiveInfinity(float f)
		{
			return f == float.PositiveInfinity;
		}

		// Token: 0x06001304 RID: 4868 RVA: 0x000E8BD0 File Offset: 0x000E7DD0
		[NonVersionable]
		public static bool IsSubnormal(float f)
		{
			int num = BitConverter.SingleToInt32Bits(f);
			num &= int.MaxValue;
			return num < 2139095040 && num != 0 && (num & 2139095040) == 0;
		}

		// Token: 0x06001305 RID: 4869 RVA: 0x000E8C03 File Offset: 0x000E7E03
		internal static int ExtractExponentFromBits(uint bits)
		{
			return (int)(bits >> 23 & 255U);
		}

		// Token: 0x06001306 RID: 4870 RVA: 0x000E8C0F File Offset: 0x000E7E0F
		internal static uint ExtractSignificandFromBits(uint bits)
		{
			return bits & 8388607U;
		}

		// Token: 0x06001307 RID: 4871 RVA: 0x000E8C18 File Offset: 0x000E7E18
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is float))
			{
				throw new ArgumentException(SR.Arg_MustBeSingle);
			}
			float num = (float)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			if (this == num)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06001308 RID: 4872 RVA: 0x000E8C6F File Offset: 0x000E7E6F
		public int CompareTo(float value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			if (this == value)
			{
				return 0;
			}
			if (!float.IsNaN(this))
			{
				return 1;
			}
			if (!float.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06001309 RID: 4873 RVA: 0x000B8662 File Offset: 0x000B7862
		[NonVersionable]
		public static bool operator ==(float left, float right)
		{
			return left == right;
		}

		// Token: 0x0600130A RID: 4874 RVA: 0x000CFCC2 File Offset: 0x000CEEC2
		[NonVersionable]
		public static bool operator !=(float left, float right)
		{
			return left != right;
		}

		// Token: 0x0600130B RID: 4875 RVA: 0x000CFCCB File Offset: 0x000CEECB
		[NonVersionable]
		public static bool operator <(float left, float right)
		{
			return left < right;
		}

		// Token: 0x0600130C RID: 4876 RVA: 0x000CFCD1 File Offset: 0x000CEED1
		[NonVersionable]
		public static bool operator >(float left, float right)
		{
			return left > right;
		}

		// Token: 0x0600130D RID: 4877 RVA: 0x000CFCD7 File Offset: 0x000CEED7
		[NonVersionable]
		public static bool operator <=(float left, float right)
		{
			return left <= right;
		}

		// Token: 0x0600130E RID: 4878 RVA: 0x000CFCE0 File Offset: 0x000CEEE0
		[NonVersionable]
		public static bool operator >=(float left, float right)
		{
			return left >= right;
		}

		// Token: 0x0600130F RID: 4879 RVA: 0x000E8C9C File Offset: 0x000E7E9C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (!(obj is float))
			{
				return false;
			}
			float num = (float)obj;
			return num == this || (float.IsNaN(num) && float.IsNaN(this));
		}

		// Token: 0x06001310 RID: 4880 RVA: 0x000E8CD2 File Offset: 0x000E7ED2
		public bool Equals(float obj)
		{
			return obj == this || (float.IsNaN(obj) && float.IsNaN(this));
		}

		// Token: 0x06001311 RID: 4881 RVA: 0x000E8CEC File Offset: 0x000E7EEC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe override int GetHashCode()
		{
			int num = *Unsafe.As<float, int>(Unsafe.AsRef<float>(this.m_value));
			if ((num - 1 & 2147483647) >= 2139095040)
			{
				num &= 2139095040;
			}
			return num;
		}

		// Token: 0x06001312 RID: 4882 RVA: 0x000E8D24 File Offset: 0x000E7F24
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001313 RID: 4883 RVA: 0x000E8D33 File Offset: 0x000E7F33
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.FormatSingle(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001314 RID: 4884 RVA: 0x000E8D43 File Offset: 0x000E7F43
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001315 RID: 4885 RVA: 0x000E8D52 File Offset: 0x000E7F52
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatSingle(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001316 RID: 4886 RVA: 0x000E8D62 File Offset: 0x000E7F62
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatSingle(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		// Token: 0x06001317 RID: 4887 RVA: 0x000E8D75 File Offset: 0x000E7F75
		[NullableContext(1)]
		public static float Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseSingle(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001318 RID: 4888 RVA: 0x000E8D96 File Offset: 0x000E7F96
		[NullableContext(1)]
		public static float Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseSingle(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001319 RID: 4889 RVA: 0x000E8DB9 File Offset: 0x000E7FB9
		[NullableContext(1)]
		public static float Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseSingle(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600131A RID: 4890 RVA: 0x000E8DDB File Offset: 0x000E7FDB
		[NullableContext(1)]
		public static float Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseSingle(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600131B RID: 4891 RVA: 0x000E8DFF File Offset: 0x000E7FFF
		public static float Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseSingle(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600131C RID: 4892 RVA: 0x000E8E14 File Offset: 0x000E8014
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out float result)
		{
			if (s == null)
			{
				result = 0f;
				return false;
			}
			return float.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x0600131D RID: 4893 RVA: 0x000E8E38 File Offset: 0x000E8038
		public static bool TryParse(ReadOnlySpan<char> s, out float result)
		{
			return float.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x0600131E RID: 4894 RVA: 0x000E8E4B File Offset: 0x000E804B
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out float result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				result = 0f;
				return false;
			}
			return float.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x0600131F RID: 4895 RVA: 0x000E8E72 File Offset: 0x000E8072
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out float result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return float.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001320 RID: 4896 RVA: 0x000E8E88 File Offset: 0x000E8088
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out float result)
		{
			return Number.TryParseSingle(s, style, info, out result);
		}

		// Token: 0x06001321 RID: 4897 RVA: 0x000E8E93 File Offset: 0x000E8093
		public TypeCode GetTypeCode()
		{
			return TypeCode.Single;
		}

		// Token: 0x06001322 RID: 4898 RVA: 0x000E8E97 File Offset: 0x000E8097
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001323 RID: 4899 RVA: 0x000E8EA0 File Offset: 0x000E80A0
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Single", "Char"));
		}

		// Token: 0x06001324 RID: 4900 RVA: 0x000E8EBB File Offset: 0x000E80BB
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001325 RID: 4901 RVA: 0x000E8EC4 File Offset: 0x000E80C4
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001326 RID: 4902 RVA: 0x000E8ECD File Offset: 0x000E80CD
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001327 RID: 4903 RVA: 0x000E8ED6 File Offset: 0x000E80D6
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001328 RID: 4904 RVA: 0x000E8EDF File Offset: 0x000E80DF
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x000E8EE8 File Offset: 0x000E80E8
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x000E8EF1 File Offset: 0x000E80F1
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x0600132B RID: 4907 RVA: 0x000E8EFA File Offset: 0x000E80FA
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x0600132C RID: 4908 RVA: 0x000E8F03 File Offset: 0x000E8103
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x0600132D RID: 4909 RVA: 0x000E8F07 File Offset: 0x000E8107
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x0600132E RID: 4910 RVA: 0x000E8F10 File Offset: 0x000E8110
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x0600132F RID: 4911 RVA: 0x000E8F19 File Offset: 0x000E8119
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Single", "DateTime"));
		}

		// Token: 0x06001330 RID: 4912 RVA: 0x000E8F34 File Offset: 0x000E8134
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0400048D RID: 1165
		private readonly float m_value;

		// Token: 0x0400048E RID: 1166
		public const float MinValue = -3.4028235E+38f;

		// Token: 0x0400048F RID: 1167
		public const float Epsilon = 1E-45f;

		// Token: 0x04000490 RID: 1168
		public const float MaxValue = 3.4028235E+38f;

		// Token: 0x04000491 RID: 1169
		public const float PositiveInfinity = float.PositiveInfinity;

		// Token: 0x04000492 RID: 1170
		public const float NegativeInfinity = float.NegativeInfinity;

		// Token: 0x04000493 RID: 1171
		public const float NaN = float.NaN;
	}
}
