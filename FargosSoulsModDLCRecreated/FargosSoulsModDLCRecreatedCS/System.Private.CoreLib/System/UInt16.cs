using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020001B0 RID: 432
	[CLSCompliant(false)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct UInt16 : IComparable, IConvertible, IFormattable, IComparable<ushort>, IEquatable<ushort>, ISpanFormattable
	{
		// Token: 0x06001A3E RID: 6718 RVA: 0x000FC561 File Offset: 0x000FB761
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (value is ushort)
			{
				return (int)(this - (ushort)value);
			}
			throw new ArgumentException(SR.Arg_MustBeUInt16);
		}

		// Token: 0x06001A3F RID: 6719 RVA: 0x000CA151 File Offset: 0x000C9351
		public int CompareTo(ushort value)
		{
			return (int)(this - value);
		}

		// Token: 0x06001A40 RID: 6720 RVA: 0x000FC584 File Offset: 0x000FB784
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ushort && this == (ushort)obj;
		}

		// Token: 0x06001A41 RID: 6721 RVA: 0x000CA127 File Offset: 0x000C9327
		[NonVersionable]
		public bool Equals(ushort obj)
		{
			return this == obj;
		}

		// Token: 0x06001A42 RID: 6722 RVA: 0x000CA3AC File Offset: 0x000C95AC
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x06001A43 RID: 6723 RVA: 0x000FC59A File Offset: 0x000FB79A
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.UInt32ToDecStr((uint)this);
		}

		// Token: 0x06001A44 RID: 6724 RVA: 0x000FC59A File Offset: 0x000FB79A
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.UInt32ToDecStr((uint)this);
		}

		// Token: 0x06001A45 RID: 6725 RVA: 0x000FC5A3 File Offset: 0x000FB7A3
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return Number.FormatUInt32((uint)this, format, null);
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x000FC5AE File Offset: 0x000FB7AE
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatUInt32((uint)this, format, provider);
		}

		// Token: 0x06001A47 RID: 6727 RVA: 0x000FC5B9 File Offset: 0x000FB7B9
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatUInt32((uint)this, format, provider, destination, out charsWritten);
		}

		// Token: 0x06001A48 RID: 6728 RVA: 0x000FC5C7 File Offset: 0x000FB7C7
		[NullableContext(1)]
		public static ushort Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001A49 RID: 6729 RVA: 0x000FC5E4 File Offset: 0x000FB7E4
		[NullableContext(1)]
		public static ushort Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return ushort.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06001A4A RID: 6730 RVA: 0x000FC607 File Offset: 0x000FB807
		[NullableContext(1)]
		public static ushort Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return ushort.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001A4B RID: 6731 RVA: 0x000FC625 File Offset: 0x000FB825
		[NullableContext(1)]
		public static ushort Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return ushort.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001A4C RID: 6732 RVA: 0x000FC649 File Offset: 0x000FB849
		public static ushort Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x000FC660 File Offset: 0x000FB860
		private static ushort Parse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info)
		{
			uint num;
			Number.ParsingStatus parsingStatus = Number.TryParseUInt32(s, style, info, out num);
			if (parsingStatus != Number.ParsingStatus.OK)
			{
				Number.ThrowOverflowOrFormatException(parsingStatus, TypeCode.UInt16);
			}
			if (num > 65535U)
			{
				Number.ThrowOverflowException(TypeCode.UInt16);
			}
			return (ushort)num;
		}

		// Token: 0x06001A4E RID: 6734 RVA: 0x000FC692 File Offset: 0x000FB892
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out ushort result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return ushort.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001A4F RID: 6735 RVA: 0x000FC6AE File Offset: 0x000FB8AE
		public static bool TryParse(ReadOnlySpan<char> s, out ushort result)
		{
			return ushort.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x06001A50 RID: 6736 RVA: 0x000FC6BD File Offset: 0x000FB8BD
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out ushort result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return ushort.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001A51 RID: 6737 RVA: 0x000FC6E0 File Offset: 0x000FB8E0
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out ushort result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return ushort.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06001A52 RID: 6738 RVA: 0x000FC6F8 File Offset: 0x000FB8F8
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out ushort result)
		{
			uint num;
			if (Number.TryParseUInt32(s, style, info, out num) != Number.ParsingStatus.OK || num > 65535U)
			{
				result = 0;
				return false;
			}
			result = (ushort)num;
			return true;
		}

		// Token: 0x06001A53 RID: 6739 RVA: 0x000DAEBB File Offset: 0x000DA0BB
		public TypeCode GetTypeCode()
		{
			return TypeCode.UInt16;
		}

		// Token: 0x06001A54 RID: 6740 RVA: 0x000FC723 File Offset: 0x000FB923
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x06001A55 RID: 6741 RVA: 0x000FC72C File Offset: 0x000FB92C
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x06001A56 RID: 6742 RVA: 0x000FC735 File Offset: 0x000FB935
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06001A57 RID: 6743 RVA: 0x000FC73E File Offset: 0x000FB93E
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06001A58 RID: 6744 RVA: 0x000FC747 File Offset: 0x000FB947
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06001A59 RID: 6745 RVA: 0x000CA3AC File Offset: 0x000C95AC
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x000FC750 File Offset: 0x000FB950
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x000FC759 File Offset: 0x000FB959
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x000FC762 File Offset: 0x000FB962
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06001A5D RID: 6749 RVA: 0x000FC76B File Offset: 0x000FB96B
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06001A5E RID: 6750 RVA: 0x000FC774 File Offset: 0x000FB974
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06001A5F RID: 6751 RVA: 0x000FC77D File Offset: 0x000FB97D
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06001A60 RID: 6752 RVA: 0x000FC786 File Offset: 0x000FB986
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06001A61 RID: 6753 RVA: 0x000FC78F File Offset: 0x000FB98F
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "UInt16", "DateTime"));
		}

		// Token: 0x06001A62 RID: 6754 RVA: 0x000FC7AA File Offset: 0x000FB9AA
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040005CB RID: 1483
		private readonly ushort m_value;

		// Token: 0x040005CC RID: 1484
		public const ushort MaxValue = 65535;

		// Token: 0x040005CD RID: 1485
		public const ushort MinValue = 0;
	}
}
