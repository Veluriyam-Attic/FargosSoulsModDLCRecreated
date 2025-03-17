using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000123 RID: 291
	public readonly struct Half : IComparable, IFormattable, IComparable<Half>, IEquatable<Half>, ISpanFormattable
	{
		// Token: 0x17000154 RID: 340
		// (get) Token: 0x06000EFD RID: 3837 RVA: 0x000D91B5 File Offset: 0x000D83B5
		public static Half Epsilon
		{
			get
			{
				return new Half(1);
			}
		}

		// Token: 0x17000155 RID: 341
		// (get) Token: 0x06000EFE RID: 3838 RVA: 0x000D91BD File Offset: 0x000D83BD
		public static Half PositiveInfinity
		{
			get
			{
				return new Half(31744);
			}
		}

		// Token: 0x17000156 RID: 342
		// (get) Token: 0x06000EFF RID: 3839 RVA: 0x000D91C9 File Offset: 0x000D83C9
		public static Half NegativeInfinity
		{
			get
			{
				return new Half(64512);
			}
		}

		// Token: 0x17000157 RID: 343
		// (get) Token: 0x06000F00 RID: 3840 RVA: 0x000D91D5 File Offset: 0x000D83D5
		public static Half NaN
		{
			get
			{
				return new Half(65024);
			}
		}

		// Token: 0x17000158 RID: 344
		// (get) Token: 0x06000F01 RID: 3841 RVA: 0x000D91E1 File Offset: 0x000D83E1
		public static Half MinValue
		{
			get
			{
				return new Half(64511);
			}
		}

		// Token: 0x17000159 RID: 345
		// (get) Token: 0x06000F02 RID: 3842 RVA: 0x000D91ED File Offset: 0x000D83ED
		public static Half MaxValue
		{
			get
			{
				return new Half(31743);
			}
		}

		// Token: 0x06000F03 RID: 3843 RVA: 0x000D91F9 File Offset: 0x000D83F9
		internal Half(ushort value)
		{
			this._value = value;
		}

		// Token: 0x06000F04 RID: 3844 RVA: 0x000D9202 File Offset: 0x000D8402
		private Half(bool sign, ushort exp, ushort sig)
		{
			this._value = (ushort)(((sign ? 1 : 0) << 15) + ((int)exp << 10) + (int)sig);
		}

		// Token: 0x1700015A RID: 346
		// (get) Token: 0x06000F05 RID: 3845 RVA: 0x000D921C File Offset: 0x000D841C
		private sbyte Exponent
		{
			get
			{
				return (sbyte)((this._value & 31744) >> 10);
			}
		}

		// Token: 0x1700015B RID: 347
		// (get) Token: 0x06000F06 RID: 3846 RVA: 0x000D922E File Offset: 0x000D842E
		private ushort Significand
		{
			get
			{
				return this._value & 1023;
			}
		}

		// Token: 0x06000F07 RID: 3847 RVA: 0x000D9240 File Offset: 0x000D8440
		public static bool operator <(Half left, Half right)
		{
			if (Half.IsNaN(left) || Half.IsNaN(right))
			{
				return false;
			}
			bool flag = Half.IsNegative(left);
			if (flag != Half.IsNegative(right))
			{
				return flag && !Half.AreZero(left, right);
			}
			return left._value < right._value ^ flag;
		}

		// Token: 0x06000F08 RID: 3848 RVA: 0x000D928F File Offset: 0x000D848F
		public static bool operator >(Half left, Half right)
		{
			return right < left;
		}

		// Token: 0x06000F09 RID: 3849 RVA: 0x000D9298 File Offset: 0x000D8498
		public static bool operator <=(Half left, Half right)
		{
			if (Half.IsNaN(left) || Half.IsNaN(right))
			{
				return false;
			}
			bool flag = Half.IsNegative(left);
			if (flag != Half.IsNegative(right))
			{
				return flag || Half.AreZero(left, right);
			}
			return left._value <= right._value ^ flag;
		}

		// Token: 0x06000F0A RID: 3850 RVA: 0x000D92E7 File Offset: 0x000D84E7
		public static bool operator >=(Half left, Half right)
		{
			return right <= left;
		}

		// Token: 0x06000F0B RID: 3851 RVA: 0x000D92F0 File Offset: 0x000D84F0
		public static bool operator ==(Half left, Half right)
		{
			return !Half.IsNaN(left) && !Half.IsNaN(right) && (left._value == right._value || Half.AreZero(left, right));
		}

		// Token: 0x06000F0C RID: 3852 RVA: 0x000D931B File Offset: 0x000D851B
		public static bool operator !=(Half left, Half right)
		{
			return !(left == right);
		}

		// Token: 0x06000F0D RID: 3853 RVA: 0x000D9327 File Offset: 0x000D8527
		public static bool IsFinite(Half value)
		{
			return Half.StripSign(value) < 31744U;
		}

		// Token: 0x06000F0E RID: 3854 RVA: 0x000D9336 File Offset: 0x000D8536
		public static bool IsInfinity(Half value)
		{
			return Half.StripSign(value) == 31744U;
		}

		// Token: 0x06000F0F RID: 3855 RVA: 0x000D9345 File Offset: 0x000D8545
		public static bool IsNaN(Half value)
		{
			return Half.StripSign(value) > 31744U;
		}

		// Token: 0x06000F10 RID: 3856 RVA: 0x000D9354 File Offset: 0x000D8554
		public static bool IsNegative(Half value)
		{
			return (short)value._value < 0;
		}

		// Token: 0x06000F11 RID: 3857 RVA: 0x000D9360 File Offset: 0x000D8560
		public static bool IsNegativeInfinity(Half value)
		{
			return value._value == 64512;
		}

		// Token: 0x06000F12 RID: 3858 RVA: 0x000D9370 File Offset: 0x000D8570
		public static bool IsNormal(Half value)
		{
			uint num = Half.StripSign(value);
			return num < 31744U && num != 0U && (num & 31744U) > 0U;
		}

		// Token: 0x06000F13 RID: 3859 RVA: 0x000D939B File Offset: 0x000D859B
		public static bool IsPositiveInfinity(Half value)
		{
			return value._value == 31744;
		}

		// Token: 0x06000F14 RID: 3860 RVA: 0x000D93AC File Offset: 0x000D85AC
		public static bool IsSubnormal(Half value)
		{
			uint num = Half.StripSign(value);
			return num < 31744U && num != 0U && (num & 31744U) == 0U;
		}

		// Token: 0x06000F15 RID: 3861 RVA: 0x000D93D7 File Offset: 0x000D85D7
		[NullableContext(1)]
		public static Half Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseHalf(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F16 RID: 3862 RVA: 0x000D93F8 File Offset: 0x000D85F8
		[NullableContext(1)]
		public static Half Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseHalf(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F17 RID: 3863 RVA: 0x000D941B File Offset: 0x000D861B
		[NullableContext(1)]
		public static Half Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseHalf(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F18 RID: 3864 RVA: 0x000D943D File Offset: 0x000D863D
		[NullableContext(1)]
		public static Half Parse(string s, NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseHalf(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F19 RID: 3865 RVA: 0x000D9461 File Offset: 0x000D8661
		public static Half Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseHalf(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F1A RID: 3866 RVA: 0x000D9476 File Offset: 0x000D8676
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out Half result)
		{
			if (s == null)
			{
				result = default(Half);
				return false;
			}
			return Half.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, null, out result);
		}

		// Token: 0x06000F1B RID: 3867 RVA: 0x000D9491 File Offset: 0x000D8691
		public static bool TryParse(ReadOnlySpan<char> s, out Half result)
		{
			return Half.TryParse(s, NumberStyles.AllowLeadingWhite | NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign | NumberStyles.AllowDecimalPoint | NumberStyles.AllowThousands | NumberStyles.AllowExponent, null, out result);
		}

		// Token: 0x06000F1C RID: 3868 RVA: 0x000D94A0 File Offset: 0x000D86A0
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out Half result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				result = default(Half);
				return false;
			}
			return Half.TryParse(s.AsSpan(), style, provider, out result);
		}

		// Token: 0x06000F1D RID: 3869 RVA: 0x000D94C2 File Offset: 0x000D86C2
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out Half result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.TryParseHalf(s, style, NumberFormatInfo.GetInstance(provider), out result);
		}

		// Token: 0x06000F1E RID: 3870 RVA: 0x000D94D8 File Offset: 0x000D86D8
		private static bool AreZero(Half left, Half right)
		{
			return (ushort)((int)(left._value | right._value) & -32769) == 0;
		}

		// Token: 0x06000F1F RID: 3871 RVA: 0x000D94F1 File Offset: 0x000D86F1
		private static bool IsNaNOrZero(Half value)
		{
			return ((int)(value._value - 1) & -32769) >= 31744;
		}

		// Token: 0x06000F20 RID: 3872 RVA: 0x000D950B File Offset: 0x000D870B
		private static uint StripSign(Half value)
		{
			return (uint)((ushort)((int)value._value & -32769));
		}

		// Token: 0x06000F21 RID: 3873 RVA: 0x000D951A File Offset: 0x000D871A
		[NullableContext(2)]
		public int CompareTo(object obj)
		{
			if (obj is Half)
			{
				return this.CompareTo((Half)obj);
			}
			if (obj != null)
			{
				throw new ArgumentException(SR.Arg_MustBeHalf);
			}
			return 1;
		}

		// Token: 0x06000F22 RID: 3874 RVA: 0x000D9540 File Offset: 0x000D8740
		public int CompareTo(Half other)
		{
			if (this < other)
			{
				return -1;
			}
			if (this > other)
			{
				return 1;
			}
			if (this == other)
			{
				return 0;
			}
			if (!Half.IsNaN(this))
			{
				return 1;
			}
			if (!Half.IsNaN(other))
			{
				return -1;
			}
			return 0;
		}

		// Token: 0x06000F23 RID: 3875 RVA: 0x000D9598 File Offset: 0x000D8798
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is Half)
			{
				Half other = (Half)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06000F24 RID: 3876 RVA: 0x000D95BD File Offset: 0x000D87BD
		public bool Equals(Half other)
		{
			return this == other || (Half.IsNaN(this) && Half.IsNaN(other));
		}

		// Token: 0x06000F25 RID: 3877 RVA: 0x000D95E4 File Offset: 0x000D87E4
		public override int GetHashCode()
		{
			if (Half.IsNaNOrZero(this))
			{
				return (int)(this._value & 31744);
			}
			return (int)this._value;
		}

		// Token: 0x06000F26 RID: 3878 RVA: 0x000D9606 File Offset: 0x000D8806
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.FormatHalf(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F27 RID: 3879 RVA: 0x000D9619 File Offset: 0x000D8819
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return Number.FormatHalf(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000F28 RID: 3880 RVA: 0x000D962C File Offset: 0x000D882C
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.FormatHalf(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F29 RID: 3881 RVA: 0x000D9640 File Offset: 0x000D8840
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatHalf(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000F2A RID: 3882 RVA: 0x000D9654 File Offset: 0x000D8854
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatHalf(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		// Token: 0x06000F2B RID: 3883 RVA: 0x000D966C File Offset: 0x000D886C
		public static explicit operator Half(float value)
		{
			uint num = (uint)BitConverter.SingleToInt32Bits(value);
			bool flag = (num & 2147483648U) >> 31 > 0U;
			int num2 = (int)(num & 2139095040U) >> 23;
			uint num3 = num & 8388607U;
			if (num2 == 255)
			{
				if (num3 != 0U)
				{
					return Half.CreateHalfNaN(flag, (ulong)num3 << 41);
				}
				if (!flag)
				{
					return Half.PositiveInfinity;
				}
				return Half.NegativeInfinity;
			}
			else
			{
				uint num4 = num3 >> 9 | (((num3 & 511U) != 0U) ? 1U : 0U);
				if ((num2 | (int)num4) == 0)
				{
					return new Half(flag, 0, 0);
				}
				return new Half(Half.RoundPackToHalf(flag, (short)(num2 - 113), (ushort)(num4 | 16384U)));
			}
		}

		// Token: 0x06000F2C RID: 3884 RVA: 0x000D9704 File Offset: 0x000D8904
		public static explicit operator Half(double value)
		{
			ulong num = (ulong)BitConverter.DoubleToInt64Bits(value);
			bool flag = (num & 9223372036854775808UL) >> 63 > 0UL;
			int num2 = (int)((num & 9218868437227405312UL) >> 52);
			ulong num3 = num & 4503599627370495UL;
			if (num2 == 2047)
			{
				if (num3 != 0UL)
				{
					return Half.CreateHalfNaN(flag, num3 << 12);
				}
				if (!flag)
				{
					return Half.PositiveInfinity;
				}
				return Half.NegativeInfinity;
			}
			else
			{
				uint num4 = (uint)Half.ShiftRightJam(num3, 38);
				if ((num2 | (int)num4) == 0)
				{
					return new Half(flag, 0, 0);
				}
				return new Half(Half.RoundPackToHalf(flag, (short)(num2 - 1009), (ushort)(num4 | 16384U)));
			}
		}

		// Token: 0x06000F2D RID: 3885 RVA: 0x000D97A4 File Offset: 0x000D89A4
		public static explicit operator float(Half value)
		{
			bool flag = Half.IsNegative(value);
			int num = (int)value.Exponent;
			uint num2 = (uint)value.Significand;
			if (num != 31)
			{
				if (num == 0)
				{
					if (num2 == 0U)
					{
						return BitConverter.Int32BitsToSingle(flag ? int.MinValue : 0);
					}
					ValueTuple<int, uint> valueTuple = Half.NormSubnormalF16Sig(num2);
					num = valueTuple.Item1;
					num2 = valueTuple.Item2;
					num--;
				}
				return Half.CreateSingle(flag, (byte)(num + 112), num2 << 13);
			}
			if (num2 != 0U)
			{
				return Half.CreateSingleNaN(flag, (ulong)num2 << 54);
			}
			if (!flag)
			{
				return float.PositiveInfinity;
			}
			return float.NegativeInfinity;
		}

		// Token: 0x06000F2E RID: 3886 RVA: 0x000D9828 File Offset: 0x000D8A28
		public static explicit operator double(Half value)
		{
			bool flag = Half.IsNegative(value);
			int num = (int)value.Exponent;
			uint num2 = (uint)value.Significand;
			if (num != 31)
			{
				if (num == 0)
				{
					if (num2 == 0U)
					{
						return BitConverter.Int64BitsToDouble(flag ? long.MinValue : 0L);
					}
					ValueTuple<int, uint> valueTuple = Half.NormSubnormalF16Sig(num2);
					num = valueTuple.Item1;
					num2 = valueTuple.Item2;
					num--;
				}
				return Half.CreateDouble(flag, (ushort)(num + 1008), (ulong)num2 << 42);
			}
			if (num2 != 0U)
			{
				return Half.CreateDoubleNaN(flag, (ulong)num2 << 54);
			}
			if (!flag)
			{
				return double.PositiveInfinity;
			}
			return double.NegativeInfinity;
		}

		// Token: 0x06000F2F RID: 3887 RVA: 0x000D98BD File Offset: 0x000D8ABD
		internal static Half Negate(Half value)
		{
			if (!Half.IsNaN(value))
			{
				return new Half(value._value ^ 32768);
			}
			return value;
		}

		// Token: 0x06000F30 RID: 3888 RVA: 0x000D98DC File Offset: 0x000D8ADC
		[return: TupleElementNames(new string[]
		{
			"Exp",
			"Sig"
		})]
		private static ValueTuple<int, uint> NormSubnormalF16Sig(uint sig)
		{
			int num = BitOperations.LeadingZeroCount(sig) - 16 - 5;
			return new ValueTuple<int, uint>(1 - num, sig << num);
		}

		// Token: 0x06000F31 RID: 3889 RVA: 0x000D9904 File Offset: 0x000D8B04
		private static Half CreateHalfNaN(bool sign, ulong significand)
		{
			uint num = (sign ? 1U : 0U) << 15;
			uint num2 = (uint)(significand >> 54);
			return BitConverter.Int16BitsToHalf((short)(num | 32256U | num2));
		}

		// Token: 0x06000F32 RID: 3890 RVA: 0x000D9934 File Offset: 0x000D8B34
		private static ushort RoundPackToHalf(bool sign, short exp, ushort sig)
		{
			int num = (int)(sig & 15);
			if (exp >= 29)
			{
				if (exp < 0)
				{
					sig = (ushort)Half.ShiftRightJam((uint)sig, (int)(-(int)exp));
					exp = 0;
				}
				else if (exp > 29 || sig + 8 >= 32768)
				{
					if (!sign)
					{
						return 31744;
					}
					return 64512;
				}
			}
			sig = (ushort)(sig + 8 >> 4);
			sig &= ~((((num ^ 8) != 0) ? 0 : 1) & 1);
			if (sig == 0)
			{
				exp = 0;
			}
			return new Half(sign, (ushort)exp, sig)._value;
		}

		// Token: 0x06000F33 RID: 3891 RVA: 0x000D99AB File Offset: 0x000D8BAB
		private static uint ShiftRightJam(uint i, int dist)
		{
			if (dist < 31)
			{
				return i >> dist | ((i << -dist != 0U) ? 1U : 0U);
			}
			if (i == 0U)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x06000F34 RID: 3892 RVA: 0x000D99D0 File Offset: 0x000D8BD0
		private static ulong ShiftRightJam(ulong l, int dist)
		{
			if (dist < 63)
			{
				return l >> dist | ((l << -dist != 0UL) ? 1UL : 0UL);
			}
			if (l == 0UL)
			{
				return 0UL;
			}
			return 1UL;
		}

		// Token: 0x06000F35 RID: 3893 RVA: 0x000D99FC File Offset: 0x000D8BFC
		private static float CreateSingleNaN(bool sign, ulong significand)
		{
			uint num = (sign ? 1U : 0U) << 31;
			uint num2 = (uint)(significand >> 41);
			return BitConverter.Int32BitsToSingle((int)(num | 2143289344U | num2));
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x000D9A28 File Offset: 0x000D8C28
		private static double CreateDoubleNaN(bool sign, ulong significand)
		{
			ulong num = (sign ? 1UL : 0UL) << 63;
			ulong num2 = significand >> 12;
			return BitConverter.Int64BitsToDouble((long)(num | 9221120237041090560UL | num2));
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x000D9A59 File Offset: 0x000D8C59
		private static float CreateSingle(bool sign, byte exp, uint sig)
		{
			return BitConverter.Int32BitsToSingle((sign ? 1 : 0) << 31 | (int)exp << 23 | (int)sig);
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x000D9A71 File Offset: 0x000D8C71
		private static double CreateDouble(bool sign, ushort exp, ulong sig)
		{
			return BitConverter.Int64BitsToDouble((long)((sign ? 1UL : 0UL) << 63 | (ulong)exp << 52 | sig));
		}

		// Token: 0x040003DD RID: 989
		private static readonly Half PositiveZero = new Half(0);

		// Token: 0x040003DE RID: 990
		private static readonly Half NegativeZero = new Half(32768);

		// Token: 0x040003DF RID: 991
		private readonly ushort _value;
	}
}
