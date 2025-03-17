using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000D8 RID: 216
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct Byte : IComparable, IConvertible, IFormattable, IComparable<byte>, IEquatable<byte>, ISpanFormattable
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x000C9E0E File Offset: 0x000C900E
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is byte))
			{
				throw new ArgumentException(SR.Arg_MustBeByte);
			}
			return (int)(this - (byte)value);
		}

		// Token: 0x06000AF5 RID: 2805 RVA: 0x000C9E31 File Offset: 0x000C9031
		public int CompareTo(byte value)
		{
			return (int)(this - value);
		}

		// Token: 0x06000AF6 RID: 2806 RVA: 0x000C9E37 File Offset: 0x000C9037
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is byte && this == (byte)obj;
		}

		// Token: 0x06000AF7 RID: 2807 RVA: 0x000C9AA8 File Offset: 0x000C8CA8
		[NonVersionable]
		public bool Equals(byte obj)
		{
			return this == obj;
		}

		// Token: 0x06000AF8 RID: 2808 RVA: 0x000C9D39 File Offset: 0x000C8F39
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06000AF9 RID: 2809 RVA: 0x000C9E4D File Offset: 0x000C904D
		[NullableContext(1)]
		public static byte Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x000C9E6A File Offset: 0x000C906A
		[NullableContext(1)]
		public static byte Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x000C9E8D File Offset: 0x000C908D
		[NullableContext(1)]
		public static byte Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x000C9EAB File Offset: 0x000C90AB
		[NullableContext(1)]
		public static byte Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return byte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x000C9ECF File Offset: 0x000C90CF
		public static byte Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000AFE RID: 2814 RVA: 0x000C9EE4 File Offset: 0x000C90E4
		private static byte Parse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info)
		{
			uint num;
			Number.ParsingStatus parsingStatus = Number.TryParseUInt32(s, style, info, out num);
			if (parsingStatus != Number.ParsingStatus.OK)
			{
				Number.ThrowOverflowOrFormatException(parsingStatus, TypeCode.Byte);
			}
			if (num > 255U)
			{
				Number.ThrowOverflowException(TypeCode.Byte);
			}
			return (byte)num;
		}

		// Token: 0x06000AFF RID: 2815 RVA: 0x000C9F16 File Offset: 0x000C9116
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out byte result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return byte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000B00 RID: 2816 RVA: 0x000C9F32 File Offset: 0x000C9132
		public static bool TryParse(ReadOnlySpan<char> s, out byte result)
		{
			return byte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06000B01 RID: 2817 RVA: 0x000C9F41 File Offset: 0x000C9141
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out byte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return byte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000B02 RID: 2818 RVA: 0x000C9F64 File Offset: 0x000C9164
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out byte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return byte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000B03 RID: 2819 RVA: 0x000C9F7C File Offset: 0x000C917C
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out byte result)
		{
			uint num;
			if (Number.TryParseUInt32(s, style, info, out num) != Number.ParsingStatus.OK || num > 255U)
			{
				result = 0;
				return false;
			}
			result = (byte)num;
			return true;
		}

		// Token: 0x06000B04 RID: 2820 RVA: 0x000C9FA7 File Offset: 0x000C91A7
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.UInt32ToDecStr((uint)this);
		}

		// Token: 0x06000B05 RID: 2821 RVA: 0x000C9FB0 File Offset: 0x000C91B0
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return Number.FormatUInt32((uint)this, format, null);
		}

		// Token: 0x06000B06 RID: 2822 RVA: 0x000C9FA7 File Offset: 0x000C91A7
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.UInt32ToDecStr((uint)this);
		}

		// Token: 0x06000B07 RID: 2823 RVA: 0x000C9FBB File Offset: 0x000C91BB
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, format, provider);
		}

		// Token: 0x06000B08 RID: 2824 RVA: 0x000C9FC6 File Offset: 0x000C91C6
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatUInt32((uint)this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06000B09 RID: 2825 RVA: 0x000C9FD4 File Offset: 0x000C91D4
		public TypeCode GetTypeCode()
		{
			return TypeCode.Byte;
		}

		// Token: 0x06000B0A RID: 2826 RVA: 0x000C9FD7 File Offset: 0x000C91D7
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x000C9FE0 File Offset: 0x000C91E0
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06000B0C RID: 2828 RVA: 0x000C9FE9 File Offset: 0x000C91E9
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000B0D RID: 2829 RVA: 0x000C9D39 File Offset: 0x000C8F39
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000B0E RID: 2830 RVA: 0x000C9FF2 File Offset: 0x000C91F2
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000B0F RID: 2831 RVA: 0x000C9FFB File Offset: 0x000C91FB
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000B10 RID: 2832 RVA: 0x000CA004 File Offset: 0x000C9204
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000B11 RID: 2833 RVA: 0x000CA00D File Offset: 0x000C920D
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000B12 RID: 2834 RVA: 0x000CA016 File Offset: 0x000C9216
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000B13 RID: 2835 RVA: 0x000CA01F File Offset: 0x000C921F
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000B14 RID: 2836 RVA: 0x000CA028 File Offset: 0x000C9228
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000B15 RID: 2837 RVA: 0x000CA031 File Offset: 0x000C9231
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000B16 RID: 2838 RVA: 0x000CA03A File Offset: 0x000C923A
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000B17 RID: 2839 RVA: 0x000CA043 File Offset: 0x000C9243
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Byte", "DateTime"));
		}

		// Token: 0x06000B18 RID: 2840 RVA: 0x000CA05E File Offset: 0x000C925E
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040002A6 RID: 678
		private readonly byte m_value;

		// Token: 0x040002A7 RID: 679
		public const byte MaxValue = 255;

		// Token: 0x040002A8 RID: 680
		public const byte MinValue = 0;
	}
}
