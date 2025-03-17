using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000135 RID: 309
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct Int32 : IComparable, IConvertible, IFormattable, IComparable<int>, IEquatable<int>, ISpanFormattable
	{
		// Token: 0x06000FAE RID: 4014 RVA: 0x000DA8A8 File Offset: 0x000D9AA8
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is int))
			{
				throw new ArgumentException(SR.Arg_MustBeInt32);
			}
			int num = (int)value;
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

		// Token: 0x06000FAF RID: 4015 RVA: 0x000DA8E3 File Offset: 0x000D9AE3
		public int CompareTo(int value)
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

		// Token: 0x06000FB0 RID: 4016 RVA: 0x000DA8F4 File Offset: 0x000D9AF4
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is int && this == (int)obj;
		}

		// Token: 0x06000FB1 RID: 4017 RVA: 0x000DA90A File Offset: 0x000D9B0A
		[NonVersionable]
		public bool Equals(int obj)
		{
			return this == obj;
		}

		// Token: 0x06000FB2 RID: 4018 RVA: 0x000DA911 File Offset: 0x000D9B11
		public override int GetHashCode()
		{
			return this;
		}

		// Token: 0x06000FB3 RID: 4019 RVA: 0x000DA915 File Offset: 0x000D9B15
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.Int32ToDecStr(this);
		}

		// Token: 0x06000FB4 RID: 4020 RVA: 0x000DA91E File Offset: 0x000D9B1E
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000FB5 RID: 4021 RVA: 0x000DA928 File Offset: 0x000D9B28
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.FormatInt32(this, 0, null, provider);
		}

		// Token: 0x06000FB6 RID: 4022 RVA: 0x000DA934 File Offset: 0x000D9B34
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32(this, -1, format, provider);
		}

		// Token: 0x06000FB7 RID: 4023 RVA: 0x000DA940 File Offset: 0x000D9B40
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatInt32(this, -1, format, provider, destination, out charsWritten);
		}

		// Token: 0x06000FB8 RID: 4024 RVA: 0x000DA94F File Offset: 0x000D9B4F
		[NullableContext(1)]
		public static int Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000FB9 RID: 4025 RVA: 0x000DA96C File Offset: 0x000D9B6C
		[NullableContext(1)]
		public static int Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000FBA RID: 4026 RVA: 0x000DA98F File Offset: 0x000D9B8F
		[NullableContext(1)]
		public static int Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000FBB RID: 4027 RVA: 0x000DA9AD File Offset: 0x000D9BAD
		[NullableContext(1)]
		public static int Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000FBC RID: 4028 RVA: 0x000DA9D1 File Offset: 0x000D9BD1
		public static int Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000FBD RID: 4029 RVA: 0x000DA9E6 File Offset: 0x000D9BE6
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out int result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return Number.TryParseInt32IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000FBE RID: 4030 RVA: 0x000DAA05 File Offset: 0x000D9C05
		public static bool TryParse(ReadOnlySpan<char> s, out int result)
		{
			return Number.TryParseInt32IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000FBF RID: 4031 RVA: 0x000DAA17 File Offset: 0x000D9C17
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out int result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000FC0 RID: 4032 RVA: 0x000DAA3D File Offset: 0x000D9C3D
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out int result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseInt32(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000FC1 RID: 4033 RVA: 0x000DAA56 File Offset: 0x000D9C56
		public TypeCode GetTypeCode()
		{
			return TypeCode.Int32;
		}

		// Token: 0x06000FC2 RID: 4034 RVA: 0x000DAA5A File Offset: 0x000D9C5A
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000FC3 RID: 4035 RVA: 0x000DAA63 File Offset: 0x000D9C63
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000FC4 RID: 4036 RVA: 0x000DAA6C File Offset: 0x000D9C6C
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000FC5 RID: 4037 RVA: 0x000DAA75 File Offset: 0x000D9C75
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000FC6 RID: 4038 RVA: 0x000DAA7E File Offset: 0x000D9C7E
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000FC7 RID: 4039 RVA: 0x000DAA87 File Offset: 0x000D9C87
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x000DA911 File Offset: 0x000D9B11
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000FC9 RID: 4041 RVA: 0x000DAA90 File Offset: 0x000D9C90
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000FCA RID: 4042 RVA: 0x000DAA99 File Offset: 0x000D9C99
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000FCB RID: 4043 RVA: 0x000DAAA2 File Offset: 0x000D9CA2
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000FCC RID: 4044 RVA: 0x000DAAAB File Offset: 0x000D9CAB
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000FCD RID: 4045 RVA: 0x000DAAB4 File Offset: 0x000D9CB4
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000FCE RID: 4046 RVA: 0x000DAABD File Offset: 0x000D9CBD
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000FCF RID: 4047 RVA: 0x000DAAC6 File Offset: 0x000D9CC6
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Int32", "DateTime"));
		}

		// Token: 0x06000FD0 RID: 4048 RVA: 0x000DAAE1 File Offset: 0x000D9CE1
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040003ED RID: 1005
		private readonly int m_value;

		// Token: 0x040003EE RID: 1006
		public const int MaxValue = 2147483647;

		// Token: 0x040003EF RID: 1007
		public const int MinValue = -2147483648;
	}
}
