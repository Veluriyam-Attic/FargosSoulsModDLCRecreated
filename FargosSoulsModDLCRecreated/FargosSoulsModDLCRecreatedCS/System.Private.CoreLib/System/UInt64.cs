using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020001B2 RID: 434
	[CLSCompliant(false)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct UInt64 : IComparable, IConvertible, IFormattable, IComparable<ulong>, IEquatable<ulong>, ISpanFormattable
	{
		// Token: 0x06001A86 RID: 6790 RVA: 0x000FC9F8 File Offset: 0x000FBBF8
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is ulong))
			{
				throw new ArgumentException(SR.Arg_MustBeUInt64);
			}
			ulong num = (ulong)value;
			if (this < num)
			{
				return -1;
			}
			if (this > num)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001A87 RID: 6791 RVA: 0x000FCA33 File Offset: 0x000FBC33
		public int CompareTo(ulong value)
		{
			if (this < value)
			{
				return -1;
			}
			if (this > value)
			{
				return 1;
			}
			return 0;
		}

		// Token: 0x06001A88 RID: 6792 RVA: 0x000FCA44 File Offset: 0x000FBC44
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ulong && this == (ulong)obj;
		}

		// Token: 0x06001A89 RID: 6793 RVA: 0x000DAB56 File Offset: 0x000D9D56
		[NonVersionable]
		public bool Equals(ulong obj)
		{
			return this == obj;
		}

		// Token: 0x06001A8A RID: 6794 RVA: 0x000FCA5A File Offset: 0x000FBC5A
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		// Token: 0x06001A8B RID: 6795 RVA: 0x000FCA66 File Offset: 0x000FBC66
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.UInt64ToDecStr(this, -1);
		}

		// Token: 0x06001A8C RID: 6796 RVA: 0x000FCA66 File Offset: 0x000FBC66
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.UInt64ToDecStr(this, -1);
		}

		// Token: 0x06001A8D RID: 6797 RVA: 0x000FCA70 File Offset: 0x000FBC70
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return Number.FormatUInt64(this, format, null);
		}

		// Token: 0x06001A8E RID: 6798 RVA: 0x000FCA7B File Offset: 0x000FBC7B
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt64(this, format, provider);
		}

		// Token: 0x06001A8F RID: 6799 RVA: 0x000FCA86 File Offset: 0x000FBC86
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatUInt64(this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06001A90 RID: 6800 RVA: 0x000FCA94 File Offset: 0x000FBC94
		[NullableContext(1)]
		public static ulong Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001A91 RID: 6801 RVA: 0x000FCAB1 File Offset: 0x000FBCB1
		[NullableContext(1)]
		public static ulong Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001A92 RID: 6802 RVA: 0x000FCAD4 File Offset: 0x000FBCD4
		[NullableContext(1)]
		public static ulong Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001A93 RID: 6803 RVA: 0x000FCAF2 File Offset: 0x000FBCF2
		[NullableContext(1)]
		public static ulong Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001A94 RID: 6804 RVA: 0x000FCB16 File Offset: 0x000FBD16
		public static ulong Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001A95 RID: 6805 RVA: 0x000FCB2B File Offset: 0x000FBD2B
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out ulong result)
		{
			if (s == null)
			{
				result = 0UL;
				return false;
			}
			return Number.TryParseUInt64IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06001A96 RID: 6806 RVA: 0x000FCB4B File Offset: 0x000FBD4B
		public static bool TryParse(ReadOnlySpan<char> s, out ulong result)
		{
			return Number.TryParseUInt64IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06001A97 RID: 6807 RVA: 0x000FCB5D File Offset: 0x000FBD5D
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out ulong result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0UL;
				return false;
			}
			return Number.TryParseUInt64(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06001A98 RID: 6808 RVA: 0x000FCB84 File Offset: 0x000FBD84
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out ulong result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseUInt64(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06001A99 RID: 6809 RVA: 0x000FCB9D File Offset: 0x000FBD9D
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt64;
		}

		// Token: 0x06001A9A RID: 6810 RVA: 0x000FCBA1 File Offset: 0x000FBDA1
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001A9B RID: 6811 RVA: 0x000FCBAA File Offset: 0x000FBDAA
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001A9C RID: 6812 RVA: 0x000FCBB3 File Offset: 0x000FBDB3
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001A9D RID: 6813 RVA: 0x000FCBBC File Offset: 0x000FBDBC
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001A9E RID: 6814 RVA: 0x000FCBC5 File Offset: 0x000FBDC5
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001A9F RID: 6815 RVA: 0x000FCBCE File Offset: 0x000FBDCE
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001AA0 RID: 6816 RVA: 0x000FCBD7 File Offset: 0x000FBDD7
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001AA1 RID: 6817 RVA: 0x000FCBE0 File Offset: 0x000FBDE0
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001AA2 RID: 6818 RVA: 0x000FCBE9 File Offset: 0x000FBDE9
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001AA3 RID: 6819 RVA: 0x000DACF6 File Offset: 0x000D9EF6
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001AA4 RID: 6820 RVA: 0x000FCBF2 File Offset: 0x000FBDF2
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001AA5 RID: 6821 RVA: 0x000FCBFB File Offset: 0x000FBDFB
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001AA6 RID: 6822 RVA: 0x000FCC04 File Offset: 0x000FBE04
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001AA7 RID: 6823 RVA: 0x000FCC0D File Offset: 0x000FBE0D
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "UInt64", "DateTime"));
		}

		// Token: 0x06001AA8 RID: 6824 RVA: 0x000FCC28 File Offset: 0x000FBE28
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040005D1 RID: 1489
		private readonly ulong m_value;

		// Token: 0x040005D2 RID: 1490
		public const ulong MaxValue = 18446744073709551615UL;

		// Token: 0x040005D3 RID: 1491
		public const ulong MinValue = 0UL;
	}
}
