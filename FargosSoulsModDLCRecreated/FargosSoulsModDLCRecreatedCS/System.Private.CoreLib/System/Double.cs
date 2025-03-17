using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000ED RID: 237
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct Double : IComparable, IConvertible, IFormattable, IComparable<double>, IEquatable<double>, ISpanFormattable
	{
		// Token: 0x06000D74 RID: 3444 RVA: 0x000CFAE0 File Offset: 0x000CECE0
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsFinite(double d)
		{
			long num = BitConverter.DoubleToInt64Bits(d);
			return (num & long.MaxValue) < 9218868437227405312L;
		}

		// Token: 0x06000D75 RID: 3445 RVA: 0x000CFB0C File Offset: 0x000CED0C
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsInfinity(double d)
		{
			long num = BitConverter.DoubleToInt64Bits(d);
			return (num & long.MaxValue) == 9218868437227405312L;
		}

		// Token: 0x06000D76 RID: 3446 RVA: 0x000CFB36 File Offset: 0x000CED36
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNaN(double d)
		{
			return d != d;
		}

		// Token: 0x06000D77 RID: 3447 RVA: 0x000CFB3F File Offset: 0x000CED3F
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegative(double d)
		{
			return BitConverter.DoubleToInt64Bits(d) < 0L;
		}

		// Token: 0x06000D78 RID: 3448 RVA: 0x000CFB4B File Offset: 0x000CED4B
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsNegativeInfinity(double d)
		{
			return d == double.NegativeInfinity;
		}

		// Token: 0x06000D79 RID: 3449 RVA: 0x000CFB5C File Offset: 0x000CED5C
		[NonVersionable]
		public static bool IsNormal(double d)
		{
			long num = BitConverter.DoubleToInt64Bits(d);
			num &= long.MaxValue;
			return num < 9218868437227405312L && num != 0L && (num & 9218868437227405312L) != 0L;
		}

		// Token: 0x06000D7A RID: 3450 RVA: 0x000CFB9C File Offset: 0x000CED9C
		[NonVersionable]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool IsPositiveInfinity(double d)
		{
			return d == double.PositiveInfinity;
		}

		// Token: 0x06000D7B RID: 3451 RVA: 0x000CFBAC File Offset: 0x000CEDAC
		[NonVersionable]
		public static bool IsSubnormal(double d)
		{
			long num = BitConverter.DoubleToInt64Bits(d);
			num &= long.MaxValue;
			return num < 9218868437227405312L && num != 0L && (num & 9218868437227405312L) == 0L;
		}

		// Token: 0x06000D7C RID: 3452 RVA: 0x000CFBEC File Offset: 0x000CEDEC
		internal static int ExtractExponentFromBits(ulong bits)
		{
			return (int)(bits >> 52) & 2047;
		}

		// Token: 0x06000D7D RID: 3453 RVA: 0x000CFBF9 File Offset: 0x000CEDF9
		internal static ulong ExtractSignificandFromBits(ulong bits)
		{
			return bits & 4503599627370495UL;
		}

		// Token: 0x06000D7E RID: 3454 RVA: 0x000CFC08 File Offset: 0x000CEE08
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is double))
			{
				throw new ArgumentException(SR.Arg_MustBeDouble);
			}
			double num = (double)value;
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
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(num))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000D7F RID: 3455 RVA: 0x000CFC5F File Offset: 0x000CEE5F
		public int CompareTo(double value)
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
			if (!double.IsNaN(this))
			{
				return 1;
			}
			if (!double.IsNaN(value))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x000CFC8C File Offset: 0x000CEE8C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (!(obj is double))
			{
				return false;
			}
			double num = (double)obj;
			return num == this || (double.IsNaN(num) && double.IsNaN(this));
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x000B8662 File Offset: 0x000B7862
		[NonVersionable]
		public static bool operator ==(double left, double right)
		{
			return left == right;
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x000CFCC2 File Offset: 0x000CEEC2
		[NonVersionable]
		public static bool operator !=(double left, double right)
		{
			return left != right;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x000CFCCB File Offset: 0x000CEECB
		[NonVersionable]
		public static bool operator <(double left, double right)
		{
			return left < right;
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x000CFCD1 File Offset: 0x000CEED1
		[NonVersionable]
		public static bool operator >(double left, double right)
		{
			return left > right;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x000CFCD7 File Offset: 0x000CEED7
		[NonVersionable]
		public static bool operator <=(double left, double right)
		{
			return left <= right;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x000CFCE0 File Offset: 0x000CEEE0
		[NonVersionable]
		public static bool operator >=(double left, double right)
		{
			return left >= right;
		}

		// Token: 0x06000D87 RID: 3463 RVA: 0x000CFCE9 File Offset: 0x000CEEE9
		public bool Equals(double obj)
		{
			return obj == this || (double.IsNaN(obj) && double.IsNaN(this));
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x000CFD04 File Offset: 0x000CEF04
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe override int GetHashCode()
		{
			long num = *Unsafe.As<double, long>(Unsafe.AsRef<double>(this.m_value));
			if ((num - 1L & 9223372036854775807L) >= 9218868437227405312L)
			{
				num &= 9218868437227405312L;
			}
			return (int)num ^ (int)(num >> 32);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x000CFD50 File Offset: 0x000CEF50
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x000CFD5F File Offset: 0x000CEF5F
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x000CFD6E File Offset: 0x000CEF6E
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.FormatDouble(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x000CFD7E File Offset: 0x000CEF7E
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDouble(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x000CFD8E File Offset: 0x000CEF8E
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatDouble(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x000CFDA1 File Offset: 0x000CEFA1
		[NullableContext(1)]
		public static double Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDouble(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x000CFDC2 File Offset: 0x000CEFC2
		[NullableContext(1)]
		public static double Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDouble(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000D90 RID: 3472 RVA: 0x000CFDE5 File Offset: 0x000CEFE5
		[NullableContext(1)]
		public static double Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDouble(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D91 RID: 3473 RVA: 0x000CFE07 File Offset: 0x000CF007
		[NullableContext(1)]
		public static double Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDouble(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D92 RID: 3474 RVA: 0x000CFE2B File Offset: 0x000CF02B
		public static double Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseDouble(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000D93 RID: 3475 RVA: 0x000CFE40 File Offset: 0x000CF040
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out double result)
		{
			if (s == null)
			{
				result = 0.0;
				return false;
			}
			return double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000D94 RID: 3476 RVA: 0x000CFE68 File Offset: 0x000CF068
		public static bool TryParse(ReadOnlySpan<char> s, out double result)
		{
			return double.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000D95 RID: 3477 RVA: 0x000CFE7B File Offset: 0x000CF07B
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out double result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				result = 0.0;
				return false;
			}
			return double.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000D96 RID: 3478 RVA: 0x000CFEA6 File Offset: 0x000CF0A6
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out double result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return double.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000D97 RID: 3479 RVA: 0x000CFEBC File Offset: 0x000CF0BC
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out double result)
		{
			return Number.TryParseDouble(s, style, info, out result);
		}

		// Token: 0x06000D98 RID: 3480 RVA: 0x000CFEC7 File Offset: 0x000CF0C7
		public TypeCode GetTypeCode()
		{
			return TypeCode.Double;
		}

		// Token: 0x06000D99 RID: 3481 RVA: 0x000CFECB File Offset: 0x000CF0CB
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000D9A RID: 3482 RVA: 0x000CFED4 File Offset: 0x000CF0D4
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Double", "Char"));
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x000CFEEF File Offset: 0x000CF0EF
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000D9C RID: 3484 RVA: 0x000CFEF8 File Offset: 0x000CF0F8
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000D9D RID: 3485 RVA: 0x000CFF01 File Offset: 0x000CF101
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000D9E RID: 3486 RVA: 0x000CFF0A File Offset: 0x000CF10A
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000D9F RID: 3487 RVA: 0x000CFF13 File Offset: 0x000CF113
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000DA0 RID: 3488 RVA: 0x000CFF1C File Offset: 0x000CF11C
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000DA1 RID: 3489 RVA: 0x000CFF25 File Offset: 0x000CF125
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000DA2 RID: 3490 RVA: 0x000CFF2E File Offset: 0x000CF12E
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000DA3 RID: 3491 RVA: 0x000CFF37 File Offset: 0x000CF137
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000DA4 RID: 3492 RVA: 0x000CFF40 File Offset: 0x000CF140
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000DA5 RID: 3493 RVA: 0x000CFF44 File Offset: 0x000CF144
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000DA6 RID: 3494 RVA: 0x000CFF4D File Offset: 0x000CF14D
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Double", "DateTime"));
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x000CFF68 File Offset: 0x000CF168
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040002E1 RID: 737
		private readonly double m_value;

		// Token: 0x040002E2 RID: 738
		public const double MinValue = -1.7976931348623157E+308;

		// Token: 0x040002E3 RID: 739
		public const double MaxValue = 1.7976931348623157E+308;

		// Token: 0x040002E4 RID: 740
		public const double Epsilon = 5E-324;

		// Token: 0x040002E5 RID: 741
		public const double NegativeInfinity = double.NegativeInfinity;

		// Token: 0x040002E6 RID: 742
		public const double PositiveInfinity = double.PositiveInfinity;

		// Token: 0x040002E7 RID: 743
		public const double NaN = double.NaN;
	}
}
