using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000134 RID: 308
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct Int16 : IComparable, IConvertible, IFormattable, IComparable<short>, IEquatable<short>, ISpanFormattable
	{
		// Token: 0x06000F89 RID: 3977 RVA: 0x000DA603 File Offset: 0x000D9803
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is short)
			{
				return (int)(this - (short)value);
			}
			throw new ArgumentException(SR.Arg_MustBeInt16);
		}

		// Token: 0x06000F8A RID: 3978 RVA: 0x000DA626 File Offset: 0x000D9826
		public int CompareTo(short value)
		{
			return (int)(this - value);
		}

		// Token: 0x06000F8B RID: 3979 RVA: 0x000DA62C File Offset: 0x000D982C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is short && this == (short)obj;
		}

		// Token: 0x06000F8C RID: 3980 RVA: 0x000DA642 File Offset: 0x000D9842
		[NonVersionable]
		public bool Equals(short obj)
		{
			return this == obj;
		}

		// Token: 0x06000F8D RID: 3981 RVA: 0x000DA649 File Offset: 0x000D9849
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06000F8E RID: 3982 RVA: 0x000DA64D File Offset: 0x000D984D
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.Int32ToDecStr((int)this);
		}

		// Token: 0x06000F8F RID: 3983 RVA: 0x000DA656 File Offset: 0x000D9856
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, 0, null, provider);
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x000DA662 File Offset: 0x000D9862
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x000DA66C File Offset: 0x000D986C
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, 65535, format, provider);
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000DA67C File Offset: 0x000D987C
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatInt32((int)this, 65535, format, provider, destination, out charsWritten);
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x000DA68F File Offset: 0x000D988F
		[NullableContext(1)]
		public static short Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000DA6AC File Offset: 0x000D98AC
		[NullableContext(1)]
		public static short Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return short.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F95 RID: 3989 RVA: 0x000DA6CF File Offset: 0x000D98CF
		[NullableContext(1)]
		public static short Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return short.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F96 RID: 3990 RVA: 0x000DA6ED File Offset: 0x000D98ED
		[NullableContext(1)]
		public static short Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return short.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x000DA711 File Offset: 0x000D9911
		public static short Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return short.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x000DA728 File Offset: 0x000D9928
		private static short Parse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info)
		{
			int num;
			Number.ParsingStatus parsingStatus = Number.TryParseInt32(s, style, info, out num);
			if (parsingStatus != Number.ParsingStatus.OK)
			{
				Number.ThrowOverflowOrFormatException(parsingStatus, TypeCode.Int16);
			}
			if (num - -32768 - (int)((int)(style & NumberStyles.AllowHexSpecifier) << 6) > 65535)
			{
				Number.ThrowOverflowException(TypeCode.Int16);
			}
			return (short)num;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x000DA76A File Offset: 0x000D996A
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out short result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x000DA786 File Offset: 0x000D9986
		public static bool TryParse(ReadOnlySpan<char> s, out short result)
		{
			return short.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000F9B RID: 3995 RVA: 0x000DA795 File Offset: 0x000D9995
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out short result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return short.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000F9C RID: 3996 RVA: 0x000DA7B8 File Offset: 0x000D99B8
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out short result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return short.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000F9D RID: 3997 RVA: 0x000DA7D0 File Offset: 0x000D99D0
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out short result)
		{
			int num;
			if (Number.TryParseInt32(s, style, info, out num) != Number.ParsingStatus.OK || num - -32768 - (int)((int)(style & NumberStyles.AllowHexSpecifier) << 6) > 65535)
			{
				result = 0;
				return false;
			}
			result = (short)num;
			return true;
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x000DA80B File Offset: 0x000D9A0B
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int16;
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x000DA80E File Offset: 0x000D9A0E
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x000DA817 File Offset: 0x000D9A17
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000FA1 RID: 4001 RVA: 0x000DA820 File Offset: 0x000D9A20
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x000DA829 File Offset: 0x000D9A29
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x000DA649 File Offset: 0x000D9849
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x000DA832 File Offset: 0x000D9A32
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000FA5 RID: 4005 RVA: 0x000DA83B File Offset: 0x000D9A3B
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x000DA844 File Offset: 0x000D9A44
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x000DA84D File Offset: 0x000D9A4D
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x000DA856 File Offset: 0x000D9A56
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000FA9 RID: 4009 RVA: 0x000DA85F File Offset: 0x000D9A5F
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000FAA RID: 4010 RVA: 0x000DA868 File Offset: 0x000D9A68
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000FAB RID: 4011 RVA: 0x000DA871 File Offset: 0x000D9A71
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000FAC RID: 4012 RVA: 0x000DA87A File Offset: 0x000D9A7A
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Int16", "DateTime"));
		}

		// Token: 0x06000FAD RID: 4013 RVA: 0x000DA895 File Offset: 0x000D9A95
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040003EA RID: 1002
		private readonly short m_value;

		// Token: 0x040003EB RID: 1003
		public const short MaxValue = 32767;

		// Token: 0x040003EC RID: 1004
		public const short MinValue = -32768;
	}
}
