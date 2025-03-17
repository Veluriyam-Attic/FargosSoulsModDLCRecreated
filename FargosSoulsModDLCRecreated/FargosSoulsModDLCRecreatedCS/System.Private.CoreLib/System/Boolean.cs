using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x020000D6 RID: 214
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct Boolean : IComparable, IConvertible, IComparable<bool>, IEquatable<bool>
	{
		// Token: 0x06000AD2 RID: 2770 RVA: 0x000C99D9 File Offset: 0x000C8BD9
		public override int GetHashCode()
		{
			if (!this)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x000C99E2 File Offset: 0x000C8BE2
		[NullableContext(1)]
		public override string ToString()
		{
			if (!this)
			{
				return "False";
			}
			return "True";
		}

		// Token: 0x06000AD4 RID: 2772 RVA: 0x000C99F3 File Offset: 0x000C8BF3
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return this.ToString();
		}

		// Token: 0x06000AD5 RID: 2773 RVA: 0x000C99FC File Offset: 0x000C8BFC
		public unsafe bool TryFormat(Span<char> destination, out int charsWritten)
		{
			if (this)
			{
				if (destination.Length > 3)
				{
					*destination[0] = 'T';
					*destination[1] = 'r';
					*destination[2] = 'u';
					*destination[3] = 'e';
					charsWritten = 4;
					return true;
				}
			}
			else if (destination.Length > 4)
			{
				*destination[0] = 'F';
				*destination[1] = 'a';
				*destination[2] = 'l';
				*destination[3] = 's';
				*destination[4] = 'e';
				charsWritten = 5;
				return true;
			}
			charsWritten = 0;
			return false;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x000C9A92 File Offset: 0x000C8C92
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is bool && this == (bool)obj;
		}

		// Token: 0x06000AD7 RID: 2775 RVA: 0x000C9AA8 File Offset: 0x000C8CA8
		[NonVersionable]
		public bool Equals(bool obj)
		{
			return this == obj;
		}

		// Token: 0x06000AD8 RID: 2776 RVA: 0x000C9AAF File Offset: 0x000C8CAF
		[NullableContext(2)]
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is bool))
			{
				throw new ArgumentException(SR.Arg_MustBeBoolean);
			}
			if (this == (bool)obj)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000AD9 RID: 2777 RVA: 0x000C9ADC File Offset: 0x000C8CDC
		public int CompareTo(bool value)
		{
			if (this == value)
			{
				return 0;
			}
			if (!this)
			{
				return -1;
			}
			return 1;
		}

		// Token: 0x06000ADA RID: 2778 RVA: 0x000C9AEC File Offset: 0x000C8CEC
		internal unsafe static bool IsTrueStringIgnoreCase(ReadOnlySpan<char> value)
		{
			return value.Length == 4 && (*value[0] == 116 || *value[0] == 84) && (*value[1] == 114 || *value[1] == 82) && (*value[2] == 117 || *value[2] == 85) && (*value[3] == 101 || *value[3] == 69);
		}

		// Token: 0x06000ADB RID: 2779 RVA: 0x000C9B70 File Offset: 0x000C8D70
		internal unsafe static bool IsFalseStringIgnoreCase(ReadOnlySpan<char> value)
		{
			return value.Length == 5 && (*value[0] == 102 || *value[0] == 70) && (*value[1] == 97 || *value[1] == 65) && (*value[2] == 108 || *value[2] == 76) && (*value[3] == 115 || *value[3] == 83) && (*value[4] == 101 || *value[4] == 69);
		}

		// Token: 0x06000ADC RID: 2780 RVA: 0x000C9C10 File Offset: 0x000C8E10
		[NullableContext(1)]
		public static bool Parse(string value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			return bool.Parse(value.AsSpan());
		}

		// Token: 0x06000ADD RID: 2781 RVA: 0x000C9C2C File Offset: 0x000C8E2C
		public static bool Parse(ReadOnlySpan<char> value)
		{
			bool result;
			if (!bool.TryParse(value, out result))
			{
				throw new FormatException(SR.Format(SR.Format_BadBoolean, new string(value)));
			}
			return result;
		}

		// Token: 0x06000ADE RID: 2782 RVA: 0x000C9C5A File Offset: 0x000C8E5A
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string value, out bool result)
		{
			if (value == null)
			{
				result = false;
				return false;
			}
			return bool.TryParse(value.AsSpan(), out result);
		}

		// Token: 0x06000ADF RID: 2783 RVA: 0x000C9C70 File Offset: 0x000C8E70
		public static bool TryParse(ReadOnlySpan<char> value, out bool result)
		{
			if (bool.IsTrueStringIgnoreCase(value))
			{
				result = true;
				return true;
			}
			if (bool.IsFalseStringIgnoreCase(value))
			{
				result = false;
				return true;
			}
			value = bool.TrimWhiteSpaceAndNull(value);
			if (bool.IsTrueStringIgnoreCase(value))
			{
				result = true;
				return true;
			}
			if (bool.IsFalseStringIgnoreCase(value))
			{
				result = false;
				return true;
			}
			result = false;
			return false;
		}

		// Token: 0x06000AE0 RID: 2784 RVA: 0x000C9CC0 File Offset: 0x000C8EC0
		private unsafe static ReadOnlySpan<char> TrimWhiteSpaceAndNull(ReadOnlySpan<char> value)
		{
			int num = 0;
			while (num < value.Length && (char.IsWhiteSpace((char)(*value[num])) || *value[num] == 0))
			{
				num++;
			}
			int num2 = value.Length - 1;
			while (num2 >= num && (char.IsWhiteSpace((char)(*value[num2])) || *value[num2] == 0))
			{
				num2--;
			}
			return value.Slice(num, num2 - num + 1);
		}

		// Token: 0x06000AE1 RID: 2785 RVA: 0x000C9D36 File Offset: 0x000C8F36
		public TypeCode GetTypeCode()
		{
			return TypeCode.Boolean;
		}

		// Token: 0x06000AE2 RID: 2786 RVA: 0x000C9D39 File Offset: 0x000C8F39
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000AE3 RID: 2787 RVA: 0x000C9D3D File Offset: 0x000C8F3D
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Boolean", "Char"));
		}

		// Token: 0x06000AE4 RID: 2788 RVA: 0x000C9D58 File Offset: 0x000C8F58
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x06000AE5 RID: 2789 RVA: 0x000C9D61 File Offset: 0x000C8F61
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x06000AE6 RID: 2790 RVA: 0x000C9D6A File Offset: 0x000C8F6A
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x000C9D73 File Offset: 0x000C8F73
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x000C9D7C File Offset: 0x000C8F7C
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x000C9D85 File Offset: 0x000C8F85
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x000C9D8E File Offset: 0x000C8F8E
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000C9D97 File Offset: 0x000C8F97
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000C9DA0 File Offset: 0x000C8FA0
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000C9DA9 File Offset: 0x000C8FA9
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000C9DB2 File Offset: 0x000C8FB2
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000C9DBB File Offset: 0x000C8FBB
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Boolean", "DateTime"));
		}

		// Token: 0x06000AF0 RID: 2800 RVA: 0x000C9DD6 File Offset: 0x000C8FD6
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x040002A2 RID: 674
		private readonly bool m_value;

		// Token: 0x040002A3 RID: 675
		[Nullable(1)]
		public static readonly string TrueString = "True";

		// Token: 0x040002A4 RID: 676
		[Nullable(1)]
		public static readonly string FalseString = "False";
	}
}
