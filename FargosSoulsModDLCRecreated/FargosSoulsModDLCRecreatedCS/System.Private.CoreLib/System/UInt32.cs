using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020001B1 RID: 433
	[CLSCompliant(false)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct UInt32 : IComparable, IConvertible, IFormattable, IComparable<uint>, IEquatable<uint>, ISpanFormattable
	{
		// Token: 0x06001A63 RID: 6755 RVA: 0x000FC7BC File Offset: 0x000FB9BC
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is uint))
			{
				throw new ArgumentException(SR.Arg_MustBeUInt32);
			}
			uint num = (uint)value;
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

		// Token: 0x06001A64 RID: 6756 RVA: 0x000FC7F7 File Offset: 0x000FB9F7
		public int CompareTo(uint value)
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

		// Token: 0x06001A65 RID: 6757 RVA: 0x000FC808 File Offset: 0x000FBA08
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is uint && this == (uint)obj;
		}

		// Token: 0x06001A66 RID: 6758 RVA: 0x000FC81E File Offset: 0x000FBA1E
		[NonVersionable]
		public bool Equals(uint obj)
		{
			return this == obj;
		}

		// Token: 0x06001A67 RID: 6759 RVA: 0x000FC825 File Offset: 0x000FBA25
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06001A68 RID: 6760 RVA: 0x000FC829 File Offset: 0x000FBA29
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.UInt32ToDecStr(this);
		}

		// Token: 0x06001A69 RID: 6761 RVA: 0x000FC829 File Offset: 0x000FBA29
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.UInt32ToDecStr(this);
		}

		// Token: 0x06001A6A RID: 6762 RVA: 0x000FC832 File Offset: 0x000FBA32
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return Number.FormatUInt32(this, format, null);
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x000FC83D File Offset: 0x000FBA3D
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt32(this, format, provider);
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x000FC848 File Offset: 0x000FBA48
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatUInt32(this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06001A6D RID: 6765 RVA: 0x000FC856 File Offset: 0x000FBA56
		[NullableContext(1)]
		public static uint Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt32(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001A6E RID: 6766 RVA: 0x000FC873 File Offset: 0x000FBA73
		[NullableContext(1)]
		public static uint Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt32(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001A6F RID: 6767 RVA: 0x000FC896 File Offset: 0x000FBA96
		[NullableContext(1)]
		public static uint Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt32(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001A70 RID: 6768 RVA: 0x000FC8B4 File Offset: 0x000FBAB4
		[NullableContext(1)]
		public static uint Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseUInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001A71 RID: 6769 RVA: 0x000FC8D8 File Offset: 0x000FBAD8
		public static uint Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.ParseUInt32(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001A72 RID: 6770 RVA: 0x000FC8ED File Offset: 0x000FBAED
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out uint result)
		{
			if (s == null)
			{
				result = 0U;
				return false;
			}
			return Number.TryParseUInt32IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06001A73 RID: 6771 RVA: 0x000FC90C File Offset: 0x000FBB0C
		public static bool TryParse(ReadOnlySpan<char> s, out uint result)
		{
			return Number.TryParseUInt32IntegerStyle(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06001A74 RID: 6772 RVA: 0x000FC91E File Offset: 0x000FBB1E
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out uint result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0U;
				return false;
			}
			return Number.TryParseUInt32(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06001A75 RID: 6773 RVA: 0x000FC944 File Offset: 0x000FBB44
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out uint result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return Number.TryParseUInt32(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06001A76 RID: 6774 RVA: 0x000FC95D File Offset: 0x000FBB5D
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt32;
		}

		// Token: 0x06001A77 RID: 6775 RVA: 0x000FC961 File Offset: 0x000FBB61
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001A78 RID: 6776 RVA: 0x000FC96A File Offset: 0x000FBB6A
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001A79 RID: 6777 RVA: 0x000FC973 File Offset: 0x000FBB73
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x000FC97C File Offset: 0x000FBB7C
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x000FC985 File Offset: 0x000FBB85
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x000FC98E File Offset: 0x000FBB8E
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x000FC997 File Offset: 0x000FBB97
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x000FC825 File Offset: 0x000FBA25
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x000FC9A0 File Offset: 0x000FBBA0
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x000FC9A9 File Offset: 0x000FBBA9
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x000FC9B2 File Offset: 0x000FBBB2
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x000FC9BB File Offset: 0x000FBBBB
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001A83 RID: 6787 RVA: 0x000FC9C4 File Offset: 0x000FBBC4
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001A84 RID: 6788 RVA: 0x000FC9CD File Offset: 0x000FBBCD
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "UInt32", "DateTime"));
		}

		// Token: 0x06001A85 RID: 6789 RVA: 0x000FC9E8 File Offset: 0x000FBBE8
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040005CE RID: 1486
		private readonly uint m_value;

		// Token: 0x040005CF RID: 1487
		public const uint MaxValue = 4294967295U;

		// Token: 0x040005D0 RID: 1488
		public const uint MinValue = 0U;
	}
}
