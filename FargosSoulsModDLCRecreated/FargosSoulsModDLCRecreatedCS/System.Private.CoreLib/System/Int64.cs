using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000136 RID: 310
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct Int64 : IComparable, IConvertible, IFormattable, IComparable<long>, IEquatable<long>, ISpanFormattable
	{
		// Token: 0x06000FD1 RID: 4049 RVA: 0x000DAAF4 File Offset: 0x000D9CF4
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is long))
			{
				throw new ArgumentException(SR.Arg_MustBeInt64);
			}
			long num = (long)value;
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

		// Token: 0x06000FD2 RID: 4050 RVA: 0x000DAB2F File Offset: 0x000D9D2F
		public int CompareTo(long value)
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

		// Token: 0x06000FD3 RID: 4051 RVA: 0x000DAB40 File Offset: 0x000D9D40
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is long && this == (long)obj;
		}

		// Token: 0x06000FD4 RID: 4052 RVA: 0x000DAB56 File Offset: 0x000D9D56
		[NonVersionable]
		public bool Equals(long obj)
		{
			return this == obj;
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x000DAB5D File Offset: 0x000D9D5D
		public override int GetHashCode()
		{
			return (int)this ^ (int)(this >> 32);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x000DAB69 File Offset: 0x000D9D69
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.Int64ToDecStr(this);
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x000DAB72 File Offset: 0x000D9D72
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.FormatInt64(this, null, provider);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x000DAB7D File Offset: 0x000D9D7D
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return Number.FormatInt64(this, format, null);
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x000DAB88 File Offset: 0x000D9D88
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt64(this, format, provider);
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x000DAB93 File Offset: 0x000D9D93
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatInt64(this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x000DABA1 File Offset: 0x000D9DA1
		[NullableContext(1)]
		public static long Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x000DABBE File Offset: 0x000D9DBE
		[NullableContext(1)]
		public static long Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt64(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x000DABE1 File Offset: 0x000D9DE1
		[NullableContext(1)]
		public static long Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt64(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000FDE RID: 4062 RVA: 0x000DABFF File Offset: 0x000D9DFF
		[NullableContext(1)]
		public static long Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000FDF RID: 4063 RVA: 0x000DAC23 File Offset: 0x000D9E23
		public static long Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt64(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000FE0 RID: 4064 RVA: 0x000DAC38 File Offset: 0x000D9E38
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out long result)
		{
			if (s == null)
			{
				result = 0L;
				return false;
			}
			return Number.TryParseInt64IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000FE1 RID: 4065 RVA: 0x000DAC58 File Offset: 0x000D9E58
		public static bool TryParse(ReadOnlySpan<char> s, out long result)
		{
			return Number.TryParseInt64IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000DAC6A File Offset: 0x000D9E6A
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out long result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0L;
				return false;
			}
			return Number.TryParseInt64(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000FE3 RID: 4067 RVA: 0x000DAC91 File Offset: 0x000D9E91
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out long result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt64(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000FE4 RID: 4068 RVA: 0x000DACAA File Offset: 0x000D9EAA
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int64;
		}

		// Token: 0x06000FE5 RID: 4069 RVA: 0x000DACAE File Offset: 0x000D9EAE
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000FE6 RID: 4070 RVA: 0x000DACB7 File Offset: 0x000D9EB7
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000FE7 RID: 4071 RVA: 0x000DACC0 File Offset: 0x000D9EC0
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000FE8 RID: 4072 RVA: 0x000DACC9 File Offset: 0x000D9EC9
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000FE9 RID: 4073 RVA: 0x000DACD2 File Offset: 0x000D9ED2
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000FEA RID: 4074 RVA: 0x000DACDB File Offset: 0x000D9EDB
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000FEB RID: 4075 RVA: 0x000DACE4 File Offset: 0x000D9EE4
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000FEC RID: 4076 RVA: 0x000DACED File Offset: 0x000D9EED
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000FED RID: 4077 RVA: 0x000DACF6 File Offset: 0x000D9EF6
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000FEE RID: 4078 RVA: 0x000DACFA File Offset: 0x000D9EFA
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000FEF RID: 4079 RVA: 0x000DAD03 File Offset: 0x000D9F03
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000FF0 RID: 4080 RVA: 0x000DAD0C File Offset: 0x000D9F0C
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000FF1 RID: 4081 RVA: 0x000DAD15 File Offset: 0x000D9F15
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000FF2 RID: 4082 RVA: 0x000DAD1E File Offset: 0x000D9F1E
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Int64", "DateTime"));
		}

		// Token: 0x06000FF3 RID: 4083 RVA: 0x000DAD39 File Offset: 0x000D9F39
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040003F0 RID: 1008
		private readonly long m_value;

		// Token: 0x040003F1 RID: 1009
		public const long MaxValue = 9223372036854775807L;

		// Token: 0x040003F2 RID: 1010
		public const long MinValue = -9223372036854775808L;
	}
}
