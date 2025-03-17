using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x0200017A RID: 378
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[CLSCompliant(false)]
	[Serializable]
	public readonly struct SByte : IComparable, IConvertible, IFormattable, IComparable<sbyte>, IEquatable<sbyte>, ISpanFormattable
	{
		// Token: 0x060012D7 RID: 4823 RVA: 0x000E889E File Offset: 0x000E7A9E
		[NullableContext(2)]
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (!(obj is sbyte))
			{
				throw new ArgumentException(SR.Arg_MustBeSByte);
			}
			return (int)(this - (sbyte)obj);
		}

		// Token: 0x060012D8 RID: 4824 RVA: 0x000E88C1 File Offset: 0x000E7AC1
		public int CompareTo(sbyte value)
		{
			return (int)(this - value);
		}

		// Token: 0x060012D9 RID: 4825 RVA: 0x000E88C7 File Offset: 0x000E7AC7
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is sbyte && this == (sbyte)obj;
		}

		// Token: 0x060012DA RID: 4826 RVA: 0x000E88DD File Offset: 0x000E7ADD
		[NonVersionable]
		public bool Equals(sbyte obj)
		{
			return this == obj;
		}

		// Token: 0x060012DB RID: 4827 RVA: 0x000E88E4 File Offset: 0x000E7AE4
		public override int GetHashCode()
		{
			return (int)this;
		}

		// Token: 0x060012DC RID: 4828 RVA: 0x000E88E8 File Offset: 0x000E7AE8
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.Int32ToDecStr((int)this);
		}

		// Token: 0x060012DD RID: 4829 RVA: 0x000E88F1 File Offset: 0x000E7AF1
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x060012DE RID: 4830 RVA: 0x000E88FB File Offset: 0x000E7AFB
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, 0, null, provider);
		}

		// Token: 0x060012DF RID: 4831 RVA: 0x000E8907 File Offset: 0x000E7B07
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatInt32((int)this, 255, format, provider);
		}

		// Token: 0x060012E0 RID: 4832 RVA: 0x000E8917 File Offset: 0x000E7B17
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatInt32((int)this, 255, format, provider, destination, out charsWritten);
		}

		// Token: 0x060012E1 RID: 4833 RVA: 0x000E892A File Offset: 0x000E7B2A
		[NullableContext(1)]
		public static sbyte Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060012E2 RID: 4834 RVA: 0x000E8947 File Offset: 0x000E7B47
		[NullableContext(1)]
		public static sbyte Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return sbyte.Parse(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x060012E3 RID: 4835 RVA: 0x000E896A File Offset: 0x000E7B6A
		[NullableContext(1)]
		public static sbyte Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return sbyte.Parse(s, NumberStyles.Integer, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012E4 RID: 4836 RVA: 0x000E8988 File Offset: 0x000E7B88
		[NullableContext(1)]
		public static sbyte Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return sbyte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012E5 RID: 4837 RVA: 0x000E89AC File Offset: 0x000E7BAC
		public static sbyte Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Integer, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.Parse(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x060012E6 RID: 4838 RVA: 0x000E89C4 File Offset: 0x000E7BC4
		private static sbyte Parse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info)
		{
			int num;
			Number.ParsingStatus parsingStatus = Number.TryParseInt32(s, style, info, out num);
			if (parsingStatus != Number.ParsingStatus.OK)
			{
				Number.ThrowOverflowOrFormatException(parsingStatus, TypeCode.SByte);
			}
			if (num - -128 - (int)((style & NumberStyles.AllowHexSpecifier) >> 2) > 255)
			{
				Number.ThrowOverflowException(TypeCode.SByte);
			}
			return (sbyte)num;
		}

		// Token: 0x060012E7 RID: 4839 RVA: 0x000E8A03 File Offset: 0x000E7C03
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out sbyte result)
		{
			if (s == null)
			{
				result = 0;
				return false;
			}
			return sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060012E8 RID: 4840 RVA: 0x000E8A1F File Offset: 0x000E7C1F
		public static bool TryParse(ReadOnlySpan<char> s, out sbyte result)
		{
			return sbyte.TryParse(s, NumberStyles.Integer, NumberFormatInfo.CurrentInfo, out result);
		}

		// Token: 0x060012E9 RID: 4841 RVA: 0x000E8A2E File Offset: 0x000E7C2E
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out sbyte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			if (s == null)
			{
				result = 0;
				return false;
			}
			return sbyte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060012EA RID: 4842 RVA: 0x000E8A51 File Offset: 0x000E7C51
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out sbyte result)
		{
			NumberFormatInfo.ValidateParseStyleInteger(style);
			return sbyte.TryParse(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x060012EB RID: 4843 RVA: 0x000E8A68 File Offset: 0x000E7C68
		private static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, NumberFormatInfo info, out sbyte result)
		{
			int num;
			if (Number.TryParseInt32(s, style, info, out num) != Number.ParsingStatus.OK || num - -128 - (int)((style & NumberStyles.AllowHexSpecifier) >> 2) > 255)
			{
				result = 0;
				return false;
			}
			result = (sbyte)num;
			return true;
		}

		// Token: 0x060012EC RID: 4844 RVA: 0x000E8AA0 File Offset: 0x000E7CA0
		public TypeCode GetTypeCode()
		{
			return TypeCode.SByte;
		}

		// Token: 0x060012ED RID: 4845 RVA: 0x000E8AA3 File Offset: 0x000E7CA3
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x060012EE RID: 4846 RVA: 0x000E8AAC File Offset: 0x000E7CAC
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this);
		}

		// Token: 0x060012EF RID: 4847 RVA: 0x000E88E4 File Offset: 0x000E7AE4
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x060012F0 RID: 4848 RVA: 0x000E8AB5 File Offset: 0x000E7CB5
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x060012F1 RID: 4849 RVA: 0x000E8ABE File Offset: 0x000E7CBE
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x060012F2 RID: 4850 RVA: 0x000E8AC7 File Offset: 0x000E7CC7
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x060012F3 RID: 4851 RVA: 0x000E88E4 File Offset: 0x000E7AE4
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return (int)this;
		}

		// Token: 0x060012F4 RID: 4852 RVA: 0x000E8AD0 File Offset: 0x000E7CD0
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x060012F5 RID: 4853 RVA: 0x000E8AD9 File Offset: 0x000E7CD9
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x060012F6 RID: 4854 RVA: 0x000E8AE2 File Offset: 0x000E7CE2
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x060012F7 RID: 4855 RVA: 0x000E8AEB File Offset: 0x000E7CEB
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this);
		}

		// Token: 0x060012F8 RID: 4856 RVA: 0x000E8AF4 File Offset: 0x000E7CF4
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this);
		}

		// Token: 0x060012F9 RID: 4857 RVA: 0x000E8AFD File Offset: 0x000E7CFD
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this);
		}

		// Token: 0x060012FA RID: 4858 RVA: 0x000E8B06 File Offset: 0x000E7D06
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "SByte", "DateTime"));
		}

		// Token: 0x060012FB RID: 4859 RVA: 0x000E8B21 File Offset: 0x000E7D21
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0400048A RID: 1162
		private readonly sbyte m_value;

		// Token: 0x0400048B RID: 1163
		public const sbyte MaxValue = 127;

		// Token: 0x0400048C RID: 1164
		public const sbyte MinValue = -128;
	}
}
