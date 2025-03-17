using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Serialization;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200005C RID: 92
	[NonVersionable]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public readonly struct Decimal : IFormattable, IComparable, IConvertible, IComparable<decimal>, IEquatable<decimal>, ISpanFormattable, ISerializable, IDeserializationCallback
	{
		// Token: 0x060001EC RID: 492 RVA: 0x000AFA21 File Offset: 0x000AEC21
		internal Decimal(Currency value)
		{
			this = decimal.FromOACurrency(value.m_value);
		}

		// Token: 0x060001ED RID: 493 RVA: 0x000AFA34 File Offset: 0x000AEC34
		public Decimal(int value)
		{
			if (value >= 0)
			{
				this._flags = 0;
			}
			else
			{
				this._flags = int.MinValue;
				value = -value;
			}
			this._lo64 = (ulong)value;
			this._hi32 = 0U;
		}

		// Token: 0x060001EE RID: 494 RVA: 0x000AFA61 File Offset: 0x000AEC61
		[CLSCompliant(false)]
		public Decimal(uint value)
		{
			this._flags = 0;
			this._lo64 = (ulong)value;
			this._hi32 = 0U;
		}

		// Token: 0x060001EF RID: 495 RVA: 0x000AFA79 File Offset: 0x000AEC79
		public Decimal(long value)
		{
			if (value >= 0L)
			{
				this._flags = 0;
			}
			else
			{
				this._flags = int.MinValue;
				value = -value;
			}
			this._lo64 = (ulong)value;
			this._hi32 = 0U;
		}

		// Token: 0x060001F0 RID: 496 RVA: 0x000AFAA6 File Offset: 0x000AECA6
		[CLSCompliant(false)]
		public Decimal(ulong value)
		{
			this._flags = 0;
			this._lo64 = value;
			this._hi32 = 0U;
		}

		// Token: 0x060001F1 RID: 497 RVA: 0x000AFABD File Offset: 0x000AECBD
		public Decimal(float value)
		{
			decimal.DecCalc.VarDecFromR4(value, decimal.AsMutable(ref this));
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x000AFACB File Offset: 0x000AECCB
		public Decimal(double value)
		{
			decimal.DecCalc.VarDecFromR8(value, decimal.AsMutable(ref this));
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x000AFADC File Offset: 0x000AECDC
		private Decimal(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._flags = info.GetInt32("flags");
			this._hi32 = (uint)info.GetInt32("hi");
			this._lo64 = (ulong)info.GetInt32("lo") + (ulong)((ulong)((long)info.GetInt32("mid")) << 32);
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000AFB3C File Offset: 0x000AED3C
		void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("flags", this._flags);
			info.AddValue("hi", (int)this.High);
			info.AddValue("lo", (int)this.Low);
			info.AddValue("mid", (int)this.Mid);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000AFB9C File Offset: 0x000AED9C
		public static decimal FromOACurrency(long cy)
		{
			bool isNegative = false;
			ulong num;
			if (cy < 0L)
			{
				isNegative = true;
				num = (ulong)(-(ulong)cy);
			}
			else
			{
				num = (ulong)cy;
			}
			int num2 = 4;
			if (num != 0UL)
			{
				while (num2 != 0 && num % 10UL == 0UL)
				{
					num2--;
					num /= 10UL;
				}
			}
			return new decimal((int)num, (int)(num >> 32), 0, isNegative, (byte)num2);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x000AFBE4 File Offset: 0x000AEDE4
		public static long ToOACurrency(decimal value)
		{
			return decimal.DecCalc.VarCyFromDec(decimal.AsMutable(ref value));
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x000AFBF2 File Offset: 0x000AEDF2
		private static bool IsValid(int flags)
		{
			return (flags & 2130771967) == 0 && (flags & 16711680) <= 1835008;
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000AFC10 File Offset: 0x000AEE10
		[NullableContext(1)]
		public Decimal(int[] bits)
		{
			if (bits == null)
			{
				throw new ArgumentNullException("bits");
			}
			this = new decimal(bits);
		}

		// Token: 0x060001F9 RID: 505 RVA: 0x000AFC30 File Offset: 0x000AEE30
		public unsafe Decimal(ReadOnlySpan<int> bits)
		{
			if (bits.Length == 4)
			{
				int flags = *bits[3];
				if (decimal.IsValid(flags))
				{
					this._lo64 = (ulong)(*bits[0]) + ((ulong)(*bits[1]) << 32);
					this._hi32 = (uint)(*bits[2]);
					this._flags = flags;
					return;
				}
			}
			throw new ArgumentException(SR.Arg_DecBitCtor);
		}

		// Token: 0x060001FA RID: 506 RVA: 0x000AFC98 File Offset: 0x000AEE98
		public Decimal(int lo, int mid, int hi, bool isNegative, byte scale)
		{
			if (scale > 28)
			{
				throw new ArgumentOutOfRangeException("scale", SR.ArgumentOutOfRange_DecimalScale);
			}
			this._lo64 = (ulong)lo + ((ulong)mid << 32);
			this._hi32 = (uint)hi;
			this._flags = (int)scale << 16;
			if (isNegative)
			{
				this._flags |= int.MinValue;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x000AFCF1 File Offset: 0x000AEEF1
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			if (!decimal.IsValid(this._flags))
			{
				throw new SerializationException(SR.Overflow_Decimal);
			}
		}

		// Token: 0x060001FC RID: 508 RVA: 0x000AFD0B File Offset: 0x000AEF0B
		private Decimal(int lo, int mid, int hi, int flags)
		{
			if (decimal.IsValid(flags))
			{
				this._lo64 = (ulong)lo + ((ulong)mid << 32);
				this._hi32 = (uint)hi;
				this._flags = flags;
				return;
			}
			throw new ArgumentException(SR.Arg_DecBitCtor);
		}

		// Token: 0x060001FD RID: 509 RVA: 0x000AFD3E File Offset: 0x000AEF3E
		private Decimal(in decimal d, int flags)
		{
			this = d;
			this._flags = flags;
		}

		// Token: 0x060001FE RID: 510 RVA: 0x000AFD53 File Offset: 0x000AEF53
		internal static decimal Abs(in decimal d)
		{
			return new decimal(ref d, d._flags & int.MaxValue);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000AFD67 File Offset: 0x000AEF67
		public static decimal Add(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), false);
			return d1;
		}

		// Token: 0x06000200 RID: 512 RVA: 0x000AFD80 File Offset: 0x000AEF80
		public static decimal Ceiling(decimal d)
		{
			int flags = d._flags;
			if ((flags & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(flags >> 16)), MidpointRounding.ToPositiveInfinity);
			}
			return d;
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000AFDB0 File Offset: 0x000AEFB0
		public static int Compare(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2);
		}

		// Token: 0x06000202 RID: 514 RVA: 0x000AFDBC File Offset: 0x000AEFBC
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is decimal))
			{
				throw new ArgumentException(SR.Arg_MustBeDecimal);
			}
			decimal num = (decimal)value;
			return decimal.DecCalc.VarDecCmp(this, num);
		}

		// Token: 0x06000203 RID: 515 RVA: 0x000AFDF0 File Offset: 0x000AEFF0
		public int CompareTo(decimal value)
		{
			return decimal.DecCalc.VarDecCmp(this, value);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x000AFDFA File Offset: 0x000AEFFA
		public static decimal Divide(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecDiv(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06000205 RID: 517 RVA: 0x000AFE10 File Offset: 0x000AF010
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			if (value is decimal)
			{
				decimal num = (decimal)value;
				return decimal.DecCalc.VarDecCmp(this, num) == 0;
			}
			return false;
		}

		// Token: 0x06000206 RID: 518 RVA: 0x000AFE39 File Offset: 0x000AF039
		public bool Equals(decimal value)
		{
			return decimal.DecCalc.VarDecCmp(this, value) == 0;
		}

		// Token: 0x06000207 RID: 519 RVA: 0x000AFE46 File Offset: 0x000AF046
		public override int GetHashCode()
		{
			return decimal.DecCalc.GetHashCode(this);
		}

		// Token: 0x06000208 RID: 520 RVA: 0x000AFE4E File Offset: 0x000AF04E
		public static bool Equals(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) == 0;
		}

		// Token: 0x06000209 RID: 521 RVA: 0x000AFE5C File Offset: 0x000AF05C
		public static decimal Floor(decimal d)
		{
			int flags = d._flags;
			if ((flags & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(flags >> 16)), MidpointRounding.ToNegativeInfinity);
			}
			return d;
		}

		// Token: 0x0600020A RID: 522 RVA: 0x000AFE8C File Offset: 0x000AF08C
		[NullableContext(1)]
		public override string ToString()
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600020B RID: 523 RVA: 0x000AFEA4 File Offset: 0x000AF0A4
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x0600020C RID: 524 RVA: 0x000AFEBC File Offset: 0x000AF0BC
		[NullableContext(1)]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return Number.FormatDecimal(this, null, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600020D RID: 525 RVA: 0x000AFED5 File Offset: 0x000AF0D5
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return Number.FormatDecimal(this, format, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x0600020E RID: 526 RVA: 0x000AFEEE File Offset: 0x000AF0EE
		public bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>), [Nullable(2)] IFormatProvider provider = null)
		{
			return Number.TryFormatDecimal(this, format, NumberFormatInfo.GetInstance(provider), destination, out charsWritten);
		}

		// Token: 0x0600020F RID: 527 RVA: 0x000AFF05 File Offset: 0x000AF105
		[NullableContext(1)]
		public static decimal Parse(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000210 RID: 528 RVA: 0x000AFF23 File Offset: 0x000AF123
		[NullableContext(1)]
		public static decimal Parse(string s, NumberStyles style)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, style, NumberFormatInfo.CurrentInfo);
		}

		// Token: 0x06000211 RID: 529 RVA: 0x000AFF46 File Offset: 0x000AF146
		[NullableContext(1)]
		public static decimal Parse(string s, [Nullable(2)] IFormatProvider provider)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, NumberStyles.Number, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000212 RID: 530 RVA: 0x000AFF65 File Offset: 0x000AF165
		[NullableContext(1)]
		public static decimal Parse(string s, NumberStyles style, [Nullable(2)] IFormatProvider provider)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
			}
			return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000213 RID: 531 RVA: 0x000AFF89 File Offset: 0x000AF189
		public static decimal Parse(ReadOnlySpan<char> s, NumberStyles style = NumberStyles.Number, [Nullable(2)] IFormatProvider provider = null)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.ParseDecimal(s, style, NumberFormatInfo.GetInstance(provider));
		}

		// Token: 0x06000214 RID: 532 RVA: 0x000AFF9E File Offset: 0x000AF19E
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, out decimal result)
		{
			if (s == null)
			{
				result = 0m;
				return false;
			}
			return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000215 RID: 533 RVA: 0x000AFFC2 File Offset: 0x000AF1C2
		public static bool TryParse(ReadOnlySpan<char> s, out decimal result)
		{
			return Number.TryParseDecimal(s, NumberStyles.Number, NumberFormatInfo.CurrentInfo, out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000216 RID: 534 RVA: 0x000AFFD5 File Offset: 0x000AF1D5
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string s, NumberStyles style, IFormatProvider provider, out decimal result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			if (s == null)
			{
				result = 0m;
				return false;
			}
			return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000217 RID: 535 RVA: 0x000AFFFF File Offset: 0x000AF1FF
		public static bool TryParse(ReadOnlySpan<char> s, NumberStyles style, [Nullable(2)] IFormatProvider provider, out decimal result)
		{
			NumberFormatInfo.ValidateParseStyleFloatingPoint(style);
			return Number.TryParseDecimal(s, style, NumberFormatInfo.GetInstance(provider), out result) == Number.ParsingStatus.OK;
		}

		// Token: 0x06000218 RID: 536 RVA: 0x000B0018 File Offset: 0x000AF218
		[NullableContext(1)]
		public static int[] GetBits(decimal d)
		{
			return new int[]
			{
				(int)d.Low,
				(int)d.Mid,
				(int)d.High,
				d._flags
			};
		}

		// Token: 0x06000219 RID: 537 RVA: 0x000B0048 File Offset: 0x000AF248
		public unsafe static int GetBits(decimal d, Span<int> destination)
		{
			if (destination.Length <= 3)
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			*destination[0] = (int)d.Low;
			*destination[1] = (int)d.Mid;
			*destination[2] = (int)d.High;
			*destination[3] = d._flags;
			return 4;
		}

		// Token: 0x0600021A RID: 538 RVA: 0x000B00A4 File Offset: 0x000AF2A4
		public unsafe static bool TryGetBits(decimal d, Span<int> destination, out int valuesWritten)
		{
			if (destination.Length <= 3)
			{
				valuesWritten = 0;
				return false;
			}
			*destination[0] = (int)d.Low;
			*destination[1] = (int)d.Mid;
			*destination[2] = (int)d.High;
			*destination[3] = d._flags;
			valuesWritten = 4;
			return true;
		}

		// Token: 0x0600021B RID: 539 RVA: 0x000B0104 File Offset: 0x000AF304
		internal static void GetBytes(in decimal d, byte[] buffer)
		{
			Span<byte> destination = buffer;
			BinaryPrimitives.WriteInt32LittleEndian(destination, (int)d.Low);
			BinaryPrimitives.WriteInt32LittleEndian(destination.Slice(4), (int)d.Mid);
			BinaryPrimitives.WriteInt32LittleEndian(destination.Slice(8), (int)d.High);
			BinaryPrimitives.WriteInt32LittleEndian(destination.Slice(12), d._flags);
		}

		// Token: 0x0600021C RID: 540 RVA: 0x000B0160 File Offset: 0x000AF360
		internal static decimal ToDecimal(ReadOnlySpan<byte> span)
		{
			int lo = BinaryPrimitives.ReadInt32LittleEndian(span);
			int mid = BinaryPrimitives.ReadInt32LittleEndian(span.Slice(4));
			int hi = BinaryPrimitives.ReadInt32LittleEndian(span.Slice(8));
			int flags = BinaryPrimitives.ReadInt32LittleEndian(span.Slice(12));
			return new decimal(lo, mid, hi, flags);
		}

		// Token: 0x0600021D RID: 541 RVA: 0x000B01A8 File Offset: 0x000AF3A8
		internal static ref readonly decimal Max(in decimal d1, in decimal d2)
		{
			if (decimal.DecCalc.VarDecCmp(d1, d2) < 0)
			{
				return ref d2;
			}
			return ref d1;
		}

		// Token: 0x0600021E RID: 542 RVA: 0x000B01B7 File Offset: 0x000AF3B7
		internal static ref readonly decimal Min(in decimal d1, in decimal d2)
		{
			if (decimal.DecCalc.VarDecCmp(d1, d2) >= 0)
			{
				return ref d2;
			}
			return ref d1;
		}

		// Token: 0x0600021F RID: 543 RVA: 0x000B01C6 File Offset: 0x000AF3C6
		public static decimal Remainder(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMod(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06000220 RID: 544 RVA: 0x000B01DC File Offset: 0x000AF3DC
		public static decimal Multiply(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMul(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06000221 RID: 545 RVA: 0x000B01F2 File Offset: 0x000AF3F2
		public static decimal Negate(decimal d)
		{
			return new decimal(ref d, d._flags ^ int.MinValue);
		}

		// Token: 0x06000222 RID: 546 RVA: 0x000B0207 File Offset: 0x000AF407
		public static decimal Round(decimal d)
		{
			return decimal.Round(ref d, 0, MidpointRounding.ToEven);
		}

		// Token: 0x06000223 RID: 547 RVA: 0x000B0212 File Offset: 0x000AF412
		public static decimal Round(decimal d, int decimals)
		{
			return decimal.Round(ref d, decimals, MidpointRounding.ToEven);
		}

		// Token: 0x06000224 RID: 548 RVA: 0x000B021D File Offset: 0x000AF41D
		public static decimal Round(decimal d, MidpointRounding mode)
		{
			return decimal.Round(ref d, 0, mode);
		}

		// Token: 0x06000225 RID: 549 RVA: 0x000B0228 File Offset: 0x000AF428
		public static decimal Round(decimal d, int decimals, MidpointRounding mode)
		{
			return decimal.Round(ref d, decimals, mode);
		}

		// Token: 0x06000226 RID: 550 RVA: 0x000B0234 File Offset: 0x000AF434
		private static decimal Round(ref decimal d, int decimals, MidpointRounding mode)
		{
			if (decimals > 28)
			{
				throw new ArgumentOutOfRangeException("decimals", SR.ArgumentOutOfRange_DecimalRound);
			}
			if (mode > MidpointRounding.ToPositiveInfinity)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidEnumValue, mode, "MidpointRounding"), "mode");
			}
			int num = d.Scale - decimals;
			if (num > 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)num, mode);
			}
			return d;
		}

		// Token: 0x06000227 RID: 551 RVA: 0x000B029A File Offset: 0x000AF49A
		internal static int Sign(in decimal d)
		{
			if ((d.Low64 | (ulong)d.High) != 0UL)
			{
				return d._flags >> 31 | 1;
			}
			return 0;
		}

		// Token: 0x06000228 RID: 552 RVA: 0x000B02B9 File Offset: 0x000AF4B9
		public static decimal Subtract(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), true);
			return d1;
		}

		// Token: 0x06000229 RID: 553 RVA: 0x000B02D0 File Offset: 0x000AF4D0
		public static byte ToByte(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException)
			{
				Number.ThrowOverflowException(TypeCode.Byte);
				throw;
			}
			if (num != (uint)((byte)num))
			{
				Number.ThrowOverflowException(TypeCode.Byte);
			}
			return (byte)num;
		}

		// Token: 0x0600022A RID: 554 RVA: 0x000B030C File Offset: 0x000AF50C
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException)
			{
				Number.ThrowOverflowException(TypeCode.SByte);
				throw;
			}
			if (num != (int)((sbyte)num))
			{
				Number.ThrowOverflowException(TypeCode.SByte);
			}
			return (sbyte)num;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000B0348 File Offset: 0x000AF548
		public static short ToInt16(decimal value)
		{
			int num;
			try
			{
				num = decimal.ToInt32(value);
			}
			catch (OverflowException)
			{
				Number.ThrowOverflowException(TypeCode.Int16);
				throw;
			}
			if (num != (int)((short)num))
			{
				Number.ThrowOverflowException(TypeCode.Int16);
			}
			return (short)num;
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000B0384 File Offset: 0x000AF584
		public static double ToDouble(decimal d)
		{
			return decimal.DecCalc.VarR8FromDec(d);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x000B0390 File Offset: 0x000AF590
		public static int ToInt32(decimal d)
		{
			decimal.Truncate(ref d);
			if ((d.High | d.Mid) == 0U)
			{
				int num = (int)d.Low;
				if (!d.IsNegative)
				{
					if (num >= 0)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0)
					{
						return num;
					}
				}
			}
			throw new OverflowException(SR.Overflow_Int32);
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000B03E0 File Offset: 0x000AF5E0
		public static long ToInt64(decimal d)
		{
			decimal.Truncate(ref d);
			if (d.High == 0U)
			{
				long num = (long)d.Low64;
				if (!d.IsNegative)
				{
					if (num >= 0L)
					{
						return num;
					}
				}
				else
				{
					num = -num;
					if (num <= 0L)
					{
						return num;
					}
				}
			}
			throw new OverflowException(SR.Overflow_Int64);
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000B042C File Offset: 0x000AF62C
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			uint num;
			try
			{
				num = decimal.ToUInt32(value);
			}
			catch (OverflowException)
			{
				Number.ThrowOverflowException(TypeCode.UInt16);
				throw;
			}
			if (num != (uint)((ushort)num))
			{
				Number.ThrowOverflowException(TypeCode.UInt16);
			}
			return (ushort)num;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000B0468 File Offset: 0x000AF668
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal d)
		{
			decimal.Truncate(ref d);
			if ((d.High | d.Mid) == 0U)
			{
				uint low = d.Low;
				if (!d.IsNegative || low == 0U)
				{
					return low;
				}
			}
			throw new OverflowException(SR.Overflow_UInt32);
		}

		// Token: 0x06000231 RID: 561 RVA: 0x000B04B0 File Offset: 0x000AF6B0
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal d)
		{
			decimal.Truncate(ref d);
			if (d.High == 0U)
			{
				ulong low = d.Low64;
				if (!d.IsNegative || low == 0UL)
				{
					return low;
				}
			}
			throw new OverflowException(SR.Overflow_UInt64);
		}

		// Token: 0x06000232 RID: 562 RVA: 0x000B04ED File Offset: 0x000AF6ED
		public static float ToSingle(decimal d)
		{
			return decimal.DecCalc.VarR4FromDec(d);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x000B04F6 File Offset: 0x000AF6F6
		public static decimal Truncate(decimal d)
		{
			decimal.Truncate(ref d);
			return d;
		}

		// Token: 0x06000234 RID: 564 RVA: 0x000B0500 File Offset: 0x000AF700
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Truncate(ref decimal d)
		{
			int flags = d._flags;
			if ((flags & 16711680) != 0)
			{
				decimal.DecCalc.InternalRound(decimal.AsMutable(ref d), (uint)((byte)(flags >> 16)), MidpointRounding.ToZero);
			}
		}

		// Token: 0x06000235 RID: 565 RVA: 0x000B052E File Offset: 0x000AF72E
		public static implicit operator decimal(byte value)
		{
			return new decimal((uint)value);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x000B0536 File Offset: 0x000AF736
		[CLSCompliant(false)]
		public static implicit operator decimal(sbyte value)
		{
			return new decimal((int)value);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x000B0536 File Offset: 0x000AF736
		public static implicit operator decimal(short value)
		{
			return new decimal((int)value);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x000B052E File Offset: 0x000AF72E
		[CLSCompliant(false)]
		public static implicit operator decimal(ushort value)
		{
			return new decimal((uint)value);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x000B052E File Offset: 0x000AF72E
		public static implicit operator decimal(char value)
		{
			return new decimal((uint)value);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x000B0536 File Offset: 0x000AF736
		public static implicit operator decimal(int value)
		{
			return new decimal(value);
		}

		// Token: 0x0600023B RID: 571 RVA: 0x000B052E File Offset: 0x000AF72E
		[CLSCompliant(false)]
		public static implicit operator decimal(uint value)
		{
			return new decimal(value);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000B053E File Offset: 0x000AF73E
		public static implicit operator decimal(long value)
		{
			return new decimal(value);
		}

		// Token: 0x0600023D RID: 573 RVA: 0x000B0546 File Offset: 0x000AF746
		[CLSCompliant(false)]
		public static implicit operator decimal(ulong value)
		{
			return new decimal(value);
		}

		// Token: 0x0600023E RID: 574 RVA: 0x000B054E File Offset: 0x000AF74E
		public static explicit operator decimal(float value)
		{
			return new decimal(value);
		}

		// Token: 0x0600023F RID: 575 RVA: 0x000B0556 File Offset: 0x000AF756
		public static explicit operator decimal(double value)
		{
			return new decimal(value);
		}

		// Token: 0x06000240 RID: 576 RVA: 0x000B055E File Offset: 0x000AF75E
		public static explicit operator byte(decimal value)
		{
			return decimal.ToByte(value);
		}

		// Token: 0x06000241 RID: 577 RVA: 0x000B0566 File Offset: 0x000AF766
		[CLSCompliant(false)]
		public static explicit operator sbyte(decimal value)
		{
			return decimal.ToSByte(value);
		}

		// Token: 0x06000242 RID: 578 RVA: 0x000B0570 File Offset: 0x000AF770
		public static explicit operator char(decimal value)
		{
			ushort result;
			try
			{
				result = decimal.ToUInt16(value);
			}
			catch (OverflowException innerException)
			{
				throw new OverflowException(SR.Overflow_Char, innerException);
			}
			return (char)result;
		}

		// Token: 0x06000243 RID: 579 RVA: 0x000B05A4 File Offset: 0x000AF7A4
		public static explicit operator short(decimal value)
		{
			return decimal.ToInt16(value);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x000B05AC File Offset: 0x000AF7AC
		[CLSCompliant(false)]
		public static explicit operator ushort(decimal value)
		{
			return decimal.ToUInt16(value);
		}

		// Token: 0x06000245 RID: 581 RVA: 0x000B05B4 File Offset: 0x000AF7B4
		public static explicit operator int(decimal value)
		{
			return decimal.ToInt32(value);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x000B05BC File Offset: 0x000AF7BC
		[CLSCompliant(false)]
		public static explicit operator uint(decimal value)
		{
			return decimal.ToUInt32(value);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x000B05C4 File Offset: 0x000AF7C4
		public static explicit operator long(decimal value)
		{
			return decimal.ToInt64(value);
		}

		// Token: 0x06000248 RID: 584 RVA: 0x000B05CC File Offset: 0x000AF7CC
		[CLSCompliant(false)]
		public static explicit operator ulong(decimal value)
		{
			return decimal.ToUInt64(value);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x000B04ED File Offset: 0x000AF6ED
		public static explicit operator float(decimal value)
		{
			return decimal.DecCalc.VarR4FromDec(value);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x000B0384 File Offset: 0x000AF584
		public static explicit operator double(decimal value)
		{
			return decimal.DecCalc.VarR8FromDec(value);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x000AC098 File Offset: 0x000AB298
		public static decimal operator +(decimal d)
		{
			return d;
		}

		// Token: 0x0600024C RID: 588 RVA: 0x000B01F2 File Offset: 0x000AF3F2
		public static decimal operator -(decimal d)
		{
			return new decimal(ref d, d._flags ^ int.MinValue);
		}

		// Token: 0x0600024D RID: 589 RVA: 0x000B05D4 File Offset: 0x000AF7D4
		public static decimal operator ++(decimal d)
		{
			return decimal.Add(d, 1m);
		}

		// Token: 0x0600024E RID: 590 RVA: 0x000B05E1 File Offset: 0x000AF7E1
		public static decimal operator --(decimal d)
		{
			return decimal.Subtract(d, 1m);
		}

		// Token: 0x0600024F RID: 591 RVA: 0x000AFD67 File Offset: 0x000AEF67
		public static decimal operator +(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), false);
			return d1;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x000B02B9 File Offset: 0x000AF4B9
		public static decimal operator -(decimal d1, decimal d2)
		{
			decimal.DecCalc.DecAddSub(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2), true);
			return d1;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x000B01DC File Offset: 0x000AF3DC
		public static decimal operator *(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMul(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000AFDFA File Offset: 0x000AEFFA
		public static decimal operator /(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecDiv(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x000B01C6 File Offset: 0x000AF3C6
		public static decimal operator %(decimal d1, decimal d2)
		{
			decimal.DecCalc.VarDecMod(decimal.AsMutable(ref d1), decimal.AsMutable(ref d2));
			return d1;
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000AFE4E File Offset: 0x000AF04E
		public static bool operator ==(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) == 0;
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000B05EE File Offset: 0x000AF7EE
		public static bool operator !=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) != 0;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x000B05FC File Offset: 0x000AF7FC
		public static bool operator <(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) < 0;
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000B060A File Offset: 0x000AF80A
		public static bool operator <=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) <= 0;
		}

		// Token: 0x06000258 RID: 600 RVA: 0x000B061B File Offset: 0x000AF81B
		public static bool operator >(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) > 0;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x000B0629 File Offset: 0x000AF829
		public static bool operator >=(decimal d1, decimal d2)
		{
			return decimal.DecCalc.VarDecCmp(d1, d2) >= 0;
		}

		// Token: 0x0600025A RID: 602 RVA: 0x000B063A File Offset: 0x000AF83A
		public TypeCode GetTypeCode()
		{
			return TypeCode.Decimal;
		}

		// Token: 0x0600025B RID: 603 RVA: 0x000B063E File Offset: 0x000AF83E
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this);
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000B064B File Offset: 0x000AF84B
		char IConvertible.ToChar(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Decimal", "Char"));
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000B0666 File Offset: 0x000AF866
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this);
		}

		// Token: 0x0600025E RID: 606 RVA: 0x000B0673 File Offset: 0x000AF873
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x000B0680 File Offset: 0x000AF880
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x000B068D File Offset: 0x000AF88D
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x000B069A File Offset: 0x000AF89A
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x000B06A7 File Offset: 0x000AF8A7
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x000B06B4 File Offset: 0x000AF8B4
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x000B06C1 File Offset: 0x000AF8C1
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x000B06CE File Offset: 0x000AF8CE
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return decimal.DecCalc.VarR4FromDec(this);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x000B06D6 File Offset: 0x000AF8D6
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return decimal.DecCalc.VarR8FromDec(this);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x000B06DE File Offset: 0x000AF8DE
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return this;
		}

		// Token: 0x06000268 RID: 616 RVA: 0x000B06E6 File Offset: 0x000AF8E6
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Decimal", "DateTime"));
		}

		// Token: 0x06000269 RID: 617 RVA: 0x000B0701 File Offset: 0x000AF901
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600026A RID: 618 RVA: 0x000B0715 File Offset: 0x000AF915
		internal uint High
		{
			get
			{
				return this._hi32;
			}
		}

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x0600026B RID: 619 RVA: 0x000B071D File Offset: 0x000AF91D
		internal uint Low
		{
			get
			{
				return (uint)this._lo64;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x0600026C RID: 620 RVA: 0x000B0726 File Offset: 0x000AF926
		internal uint Mid
		{
			get
			{
				return (uint)(this._lo64 >> 32);
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600026D RID: 621 RVA: 0x000B0732 File Offset: 0x000AF932
		internal bool IsNegative
		{
			get
			{
				return this._flags < 0;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x0600026E RID: 622 RVA: 0x000B073D File Offset: 0x000AF93D
		internal int Scale
		{
			get
			{
				return (int)((byte)(this._flags >> 16));
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600026F RID: 623 RVA: 0x000B0749 File Offset: 0x000AF949
		private ulong Low64
		{
			get
			{
				return this._lo64;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x000B0751 File Offset: 0x000AF951
		private static ref decimal.DecCalc AsMutable(ref decimal d)
		{
			return Unsafe.As<decimal, decimal.DecCalc>(ref d);
		}

		// Token: 0x06000271 RID: 625 RVA: 0x000B0759 File Offset: 0x000AF959
		internal static uint DecDivMod1E9(ref decimal value)
		{
			return decimal.DecCalc.DecDivMod1E9(decimal.AsMutable(ref value));
		}

		// Token: 0x040000DC RID: 220
		public const decimal Zero = 0m;

		// Token: 0x040000DD RID: 221
		public const decimal One = 1m;

		// Token: 0x040000DE RID: 222
		public const decimal MinusOne = -1m;

		// Token: 0x040000DF RID: 223
		public const decimal MaxValue = 79228162514264337593543950335m;

		// Token: 0x040000E0 RID: 224
		public const decimal MinValue = -79228162514264337593543950335m;

		// Token: 0x040000E1 RID: 225
		private readonly int _flags;

		// Token: 0x040000E2 RID: 226
		private readonly uint _hi32;

		// Token: 0x040000E3 RID: 227
		private readonly ulong _lo64;

		// Token: 0x0200005D RID: 93
		[StructLayout(LayoutKind.Explicit)]
		private struct DecCalc
		{
			// Token: 0x1700001A RID: 26
			// (get) Token: 0x06000273 RID: 627 RVA: 0x000B07B4 File Offset: 0x000AF9B4
			// (set) Token: 0x06000274 RID: 628 RVA: 0x000B07BC File Offset: 0x000AF9BC
			private uint High
			{
				get
				{
					return this.uhi;
				}
				set
				{
					this.uhi = value;
				}
			}

			// Token: 0x1700001B RID: 27
			// (get) Token: 0x06000275 RID: 629 RVA: 0x000B07C5 File Offset: 0x000AF9C5
			// (set) Token: 0x06000276 RID: 630 RVA: 0x000B07CD File Offset: 0x000AF9CD
			private uint Low
			{
				get
				{
					return this.ulo;
				}
				set
				{
					this.ulo = value;
				}
			}

			// Token: 0x1700001C RID: 28
			// (get) Token: 0x06000277 RID: 631 RVA: 0x000B07D6 File Offset: 0x000AF9D6
			// (set) Token: 0x06000278 RID: 632 RVA: 0x000B07DE File Offset: 0x000AF9DE
			private uint Mid
			{
				get
				{
					return this.umid;
				}
				set
				{
					this.umid = value;
				}
			}

			// Token: 0x1700001D RID: 29
			// (get) Token: 0x06000279 RID: 633 RVA: 0x000B07E7 File Offset: 0x000AF9E7
			private bool IsNegative
			{
				get
				{
					return this.uflags < 0U;
				}
			}

			// Token: 0x1700001E RID: 30
			// (get) Token: 0x0600027A RID: 634 RVA: 0x000B07F2 File Offset: 0x000AF9F2
			private int Scale
			{
				get
				{
					return (int)((byte)(this.uflags >> 16));
				}
			}

			// Token: 0x1700001F RID: 31
			// (get) Token: 0x0600027B RID: 635 RVA: 0x000B07FE File Offset: 0x000AF9FE
			// (set) Token: 0x0600027C RID: 636 RVA: 0x000B0806 File Offset: 0x000AFA06
			private ulong Low64
			{
				get
				{
					return this.ulomid;
				}
				set
				{
					this.ulomid = value;
				}
			}

			// Token: 0x0600027D RID: 637 RVA: 0x000B080F File Offset: 0x000AFA0F
			private unsafe static uint GetExponent(float f)
			{
				return (uint)((byte)(*(uint*)(&f) >> 23));
			}

			// Token: 0x0600027E RID: 638 RVA: 0x000B0819 File Offset: 0x000AFA19
			private unsafe static uint GetExponent(double d)
			{
				return (uint)((ulong)(*(long*)(&d)) >> 52) & 2047U;
			}

			// Token: 0x0600027F RID: 639 RVA: 0x000B0829 File Offset: 0x000AFA29
			private static ulong UInt32x32To64(uint a, uint b)
			{
				return (ulong)a * (ulong)b;
			}

			// Token: 0x06000280 RID: 640 RVA: 0x000B0830 File Offset: 0x000AFA30
			private static void UInt64x64To128(ulong a, ulong b, ref decimal.DecCalc result)
			{
				ulong num = decimal.DecCalc.UInt32x32To64((uint)a, (uint)b);
				ulong num2 = decimal.DecCalc.UInt32x32To64((uint)a, (uint)(b >> 32));
				ulong num3 = decimal.DecCalc.UInt32x32To64((uint)(a >> 32), (uint)(b >> 32));
				num3 += num2 >> 32;
				num += (num2 <<= 32);
				if (num < num2)
				{
					num3 += 1UL;
				}
				num2 = decimal.DecCalc.UInt32x32To64((uint)(a >> 32), (uint)b);
				num3 += num2 >> 32;
				num += (num2 <<= 32);
				if (num < num2)
				{
					num3 += 1UL;
				}
				if (num3 > (ulong)-1)
				{
					Number.ThrowOverflowException(TypeCode.Decimal);
				}
				result.Low64 = num;
				result.High = (uint)num3;
			}

			// Token: 0x06000281 RID: 641 RVA: 0x000B08C0 File Offset: 0x000AFAC0
			private static uint Div96By32(ref decimal.DecCalc.Buf12 bufNum, uint den)
			{
				if (bufNum.U2 != 0U)
				{
					ulong num = bufNum.High64;
					ulong num2 = num / (ulong)den;
					bufNum.High64 = num2;
					num = (num - (ulong)((uint)num2 * den) << 32 | (ulong)bufNum.U0);
					if (num == 0UL)
					{
						return 0U;
					}
					uint num3 = (uint)(num / (ulong)den);
					bufNum.U0 = num3;
					return (uint)num - num3 * den;
				}
				else
				{
					ulong num = bufNum.Low64;
					if (num == 0UL)
					{
						return 0U;
					}
					ulong num2 = num / (ulong)den;
					bufNum.Low64 = num2;
					return (uint)(num - num2 * (ulong)den);
				}
			}

			// Token: 0x06000282 RID: 642 RVA: 0x000B0934 File Offset: 0x000AFB34
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static bool Div96ByConst(ref ulong high64, ref uint low, uint pow)
			{
				ulong num = high64 / (ulong)pow;
				uint num2 = (uint)(((high64 - num * (ulong)pow << 32) + (ulong)low) / (ulong)pow);
				if (low == num2 * pow)
				{
					high64 = num;
					low = num2;
					return true;
				}
				return false;
			}

			// Token: 0x06000283 RID: 643 RVA: 0x000B096C File Offset: 0x000AFB6C
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private static void Unscale(ref uint low, ref ulong high64, ref int scale)
			{
				while ((byte)low == 0 && scale >= 8 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 100000000U))
				{
					scale -= 8;
				}
				if ((low & 15U) == 0U && scale >= 4 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 10000U))
				{
					scale -= 4;
				}
				if ((low & 3U) == 0U && scale >= 2 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 100U))
				{
					scale -= 2;
				}
				if ((low & 1U) == 0U && scale >= 1 && decimal.DecCalc.Div96ByConst(ref high64, ref low, 10U))
				{
					scale--;
				}
			}

			// Token: 0x06000284 RID: 644 RVA: 0x000B09F4 File Offset: 0x000AFBF4
			private static uint Div96By64(ref decimal.DecCalc.Buf12 bufNum, ulong den)
			{
				uint u = bufNum.U2;
				if (u == 0U)
				{
					ulong num = bufNum.Low64;
					if (num < den)
					{
						return 0U;
					}
					uint num2 = (uint)(num / den);
					num -= (ulong)num2 * den;
					bufNum.Low64 = num;
					return num2;
				}
				else
				{
					uint num3 = (uint)(den >> 32);
					ulong num;
					uint num2;
					if (u >= num3)
					{
						num = bufNum.Low64;
						num -= den << 32;
						num2 = 0U;
						do
						{
							num2 -= 1U;
							num += den;
						}
						while (num >= den);
						bufNum.Low64 = num;
						return num2;
					}
					ulong high = bufNum.High64;
					if (high < (ulong)num3)
					{
						return 0U;
					}
					num2 = (uint)(high / (ulong)num3);
					num = ((ulong)bufNum.U0 | high - (ulong)(num2 * num3) << 32);
					ulong num4 = decimal.DecCalc.UInt32x32To64(num2, (uint)den);
					num -= num4;
					if (num > ~num4)
					{
						do
						{
							num2 -= 1U;
							num += den;
						}
						while (num >= den);
					}
					bufNum.Low64 = num;
					return num2;
				}
			}

			// Token: 0x06000285 RID: 645 RVA: 0x000B0AB0 File Offset: 0x000AFCB0
			private static uint Div128By96(ref decimal.DecCalc.Buf16 bufNum, ref decimal.DecCalc.Buf12 bufDen)
			{
				ulong high = bufNum.High64;
				uint u = bufDen.U2;
				if (high < (ulong)u)
				{
					return 0U;
				}
				uint num = (uint)(high / (ulong)u);
				uint num2 = (uint)high - num * u;
				ulong num3 = decimal.DecCalc.UInt32x32To64(num, bufDen.U0);
				ulong num4 = decimal.DecCalc.UInt32x32To64(num, bufDen.U1);
				num4 += num3 >> 32;
				num3 = ((ulong)((uint)num3) | num4 << 32);
				num4 >>= 32;
				ulong num5 = bufNum.Low64;
				num5 -= num3;
				num2 -= (uint)num4;
				if (num5 > ~num3)
				{
					num2 -= 1U;
					if (num2 < ~(uint)num4)
					{
						goto IL_B4;
					}
				}
				else if (num2 <= ~(uint)num4)
				{
					goto IL_B4;
				}
				num3 = bufDen.Low64;
				do
				{
					num -= 1U;
					num5 += num3;
					num2 += u;
				}
				while ((num5 >= num3 || num2++ >= u) && num2 >= u);
				IL_B4:
				bufNum.Low64 = num5;
				bufNum.U2 = num2;
				return num;
			}

			// Token: 0x06000286 RID: 646 RVA: 0x000B0B84 File Offset: 0x000AFD84
			private static uint IncreaseScale(ref decimal.DecCalc.Buf12 bufNum, uint power)
			{
				ulong num = decimal.DecCalc.UInt32x32To64(bufNum.U0, power);
				bufNum.U0 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U1, power);
				bufNum.U1 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U2, power);
				bufNum.U2 = (uint)num;
				return (uint)(num >> 32);
			}

			// Token: 0x06000287 RID: 647 RVA: 0x000B0BE4 File Offset: 0x000AFDE4
			private static void IncreaseScale64(ref decimal.DecCalc.Buf12 bufNum, uint power)
			{
				ulong num = decimal.DecCalc.UInt32x32To64(bufNum.U0, power);
				bufNum.U0 = (uint)num;
				num >>= 32;
				num += decimal.DecCalc.UInt32x32To64(bufNum.U1, power);
				bufNum.High64 = num;
			}

			// Token: 0x06000288 RID: 648 RVA: 0x000B0C24 File Offset: 0x000AFE24
			private unsafe static int ScaleResult(decimal.DecCalc.Buf24* bufRes, uint hiRes, int scale)
			{
				int num = 0;
				if (hiRes > 2U)
				{
					num = (int)(hiRes * 32U - 64U - 1U);
					num -= BitOperations.LeadingZeroCount(*(uint*)(bufRes + (ulong)hiRes * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24)));
					num = (num * 77 >> 8) + 1;
					if (num > scale)
					{
						goto IL_1CC;
					}
				}
				if (num < scale - 28)
				{
					num = scale - 28;
				}
				if (num != 0)
				{
					scale -= num;
					uint num2 = 0U;
					uint num3 = 0U;
					for (;;)
					{
						num2 |= num3;
						uint num5;
						uint num4;
						switch (num)
						{
						case 1:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10U);
							break;
						case 2:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100U);
							break;
						case 3:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000U);
							break;
						case 4:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10000U);
							break;
						case 5:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100000U);
							break;
						case 6:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000000U);
							break;
						case 7:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 10000000U);
							break;
						case 8:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 100000000U);
							break;
						default:
							num4 = decimal.DecCalc.DivByConst((uint*)bufRes, hiRes, out num5, out num3, 1000000000U);
							break;
						}
						*(int*)(bufRes + (ulong)hiRes * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24)) = (int)num5;
						if (num5 == 0U && hiRes != 0U)
						{
							hiRes -= 1U;
						}
						num -= 9;
						if (num <= 0)
						{
							if (hiRes > 2U)
							{
								if (scale == 0)
								{
									goto IL_1CC;
								}
								num = 1;
								scale--;
							}
							else
							{
								num4 >>= 1;
								if (num4 > num3 || (num4 >= num3 && ((*(uint*)bufRes & 1U) | num2) == 0U))
								{
									break;
								}
								uint num6 = *(uint*)bufRes + 1U;
								*(int*)bufRes = (int)num6;
								if (num6 != 0U)
								{
									break;
								}
								uint num7 = 0U;
								do
								{
									decimal.DecCalc.Buf24* ptr = bufRes + (ulong)(num7 += 1U) * 4UL / (ulong)sizeof(decimal.DecCalc.Buf24);
									num6 = *(uint*)ptr + 1U;
									*(int*)ptr = (int)num6;
								}
								while (num6 == 0U);
								if (num7 <= 2U)
								{
									break;
								}
								if (scale == 0)
								{
									goto IL_1CC;
								}
								hiRes = num7;
								num2 = 0U;
								num3 = 0U;
								num = 1;
								scale--;
							}
						}
					}
				}
				return scale;
				IL_1CC:
				Number.ThrowOverflowException(TypeCode.Decimal);
				return 0;
			}

			// Token: 0x06000289 RID: 649 RVA: 0x000B0E08 File Offset: 0x000B0008
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			private unsafe static uint DivByConst(uint* result, uint hiRes, out uint quotient, out uint remainder, uint power)
			{
				uint num = result[(ulong)hiRes * 4UL / 4UL];
				remainder = num - (quotient = num / power) * power;
				for (uint num2 = hiRes - 1U; num2 >= 0U; num2 -= 1U)
				{
					ulong num3 = (ulong)result[(ulong)num2 * 4UL / 4UL] + ((ulong)remainder << 32);
					remainder = (uint)num3 - (result[(ulong)num2 * 4UL / 4UL] = (uint)(num3 / (ulong)power)) * power;
				}
				return power;
			}

			// Token: 0x0600028A RID: 650 RVA: 0x000B0E6C File Offset: 0x000B006C
			private static int OverflowUnscale(ref decimal.DecCalc.Buf12 bufQuo, int scale, bool sticky)
			{
				if (--scale < 0)
				{
					Number.ThrowOverflowException(TypeCode.Decimal);
				}
				bufQuo.U2 = 429496729U;
				ulong num = 25769803776UL + (ulong)bufQuo.U1;
				uint num2 = (uint)(num / 10UL);
				bufQuo.U1 = num2;
				num = (num - (ulong)(num2 * 10U) << 32) + (ulong)bufQuo.U0;
				num2 = (uint)(num / 10UL);
				bufQuo.U0 = num2;
				uint num3 = (uint)(num - (ulong)(num2 * 10U));
				if (num3 > 5U || (num3 == 5U && (sticky || (bufQuo.U0 & 1U) != 0U)))
				{
					decimal.DecCalc.Add32To96(ref bufQuo, 1U);
				}
				return scale;
			}

			// Token: 0x0600028B RID: 651 RVA: 0x000B0EFC File Offset: 0x000B00FC
			private static int SearchScale(ref decimal.DecCalc.Buf12 bufQuo, int scale)
			{
				uint u = bufQuo.U2;
				ulong low = bufQuo.Low64;
				int num = 0;
				if (u <= 429496729U)
				{
					decimal.DecCalc.PowerOvfl[] powerOvflValues = decimal.DecCalc.PowerOvflValues;
					if (scale > 19)
					{
						num = 28 - scale;
						if (u < powerOvflValues[num - 1].Hi)
						{
							goto IL_D1;
						}
					}
					else if (u < 4U || (u == 4U && low <= 5441186219426131129UL))
					{
						return 9;
					}
					if (u > 42949U)
					{
						if (u > 4294967U)
						{
							num = 2;
							if (u > 42949672U)
							{
								num--;
							}
						}
						else
						{
							num = 4;
							if (u > 429496U)
							{
								num--;
							}
						}
					}
					else if (u > 429U)
					{
						num = 6;
						if (u > 4294U)
						{
							num--;
						}
					}
					else
					{
						num = 8;
						if (u > 42U)
						{
							num--;
						}
					}
					if (u == powerOvflValues[num - 1].Hi && low > powerOvflValues[num - 1].MidLo)
					{
						num--;
					}
				}
				IL_D1:
				if (num + scale < 0)
				{
					Number.ThrowOverflowException(TypeCode.Decimal);
				}
				return num;
			}

			// Token: 0x0600028C RID: 652 RVA: 0x000B0FE8 File Offset: 0x000B01E8
			private static bool Add32To96(ref decimal.DecCalc.Buf12 bufNum, uint value)
			{
				if ((bufNum.Low64 += (ulong)value) < (ulong)value)
				{
					uint num = bufNum.U2 + 1U;
					bufNum.U2 = num;
					if (num == 0U)
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600028D RID: 653 RVA: 0x000B1020 File Offset: 0x000B0220
			internal unsafe static void DecAddSub(ref decimal.DecCalc d1, ref decimal.DecCalc d2, bool sign)
			{
				ulong num = d1.Low64;
				uint num2 = d1.High;
				uint num3 = d1.uflags;
				uint num4 = d2.uflags;
				uint num5 = num4 ^ num3;
				sign ^= ((num5 & 2147483648U) > 0U);
				if ((num5 & 16711680U) != 0U)
				{
					uint num6 = num3;
					num3 = ((num4 & 16711680U) | (num3 & 2147483648U));
					int i = (int)(num3 - num6) >> 16;
					if (i < 0)
					{
						i = -i;
						num3 = num6;
						if (sign)
						{
							num3 ^= 2147483648U;
						}
						num = d2.Low64;
						num2 = d2.High;
						d2 = d1;
					}
					ulong num9;
					if (num2 == 0U)
					{
						if (num <= (ulong)-1)
						{
							if ((uint)num == 0U)
							{
								uint num7 = num3 & 2147483648U;
								if (sign)
								{
									num7 ^= 2147483648U;
								}
								d1 = d2;
								d1.uflags = ((d2.uflags & 16711680U) | num7);
								return;
							}
							while (i > 9)
							{
								i -= 9;
								num = decimal.DecCalc.UInt32x32To64((uint)num, 1000000000U);
								if (num > (ulong)-1)
								{
									goto IL_106;
								}
							}
							num = decimal.DecCalc.UInt32x32To64((uint)num, decimal.DecCalc.s_powers10[i]);
							goto IL_450;
						}
						do
						{
							IL_106:
							uint b = 1000000000U;
							if (i < 9)
							{
								b = decimal.DecCalc.s_powers10[i];
							}
							ulong num8 = decimal.DecCalc.UInt32x32To64((uint)num, b);
							num9 = decimal.DecCalc.UInt32x32To64((uint)(num >> 32), b) + (num8 >> 32);
							num = (ulong)((uint)num8) + (num9 << 32);
							num2 = (uint)(num9 >> 32);
							if ((i -= 9) <= 0)
							{
								goto IL_450;
							}
						}
						while (num2 == 0U);
					}
					do
					{
						uint b = 1000000000U;
						if (i < 9)
						{
							b = decimal.DecCalc.s_powers10[i];
						}
						ulong num8 = decimal.DecCalc.UInt32x32To64((uint)num, b);
						num9 = decimal.DecCalc.UInt32x32To64((uint)(num >> 32), b) + (num8 >> 32);
						num = (ulong)((uint)num8) + (num9 << 32);
						num9 >>= 32;
						num9 += decimal.DecCalc.UInt32x32To64(num2, b);
						i -= 9;
						if (num9 > (ulong)-1)
						{
							goto IL_1CF;
						}
						num2 = (uint)num9;
					}
					while (i > 0);
					goto IL_450;
					IL_1CF:
					decimal.DecCalc.Buf24 buf;
					Unsafe.SkipInit<decimal.DecCalc.Buf24>(out buf);
					buf.Low64 = num;
					buf.Mid64 = num9;
					uint num10 = 3U;
					while (i > 0)
					{
						uint b = 1000000000U;
						if (i < 9)
						{
							b = decimal.DecCalc.s_powers10[i];
						}
						num9 = 0UL;
						uint* ptr = (uint*)(&buf);
						uint num11 = 0U;
						do
						{
							num9 += decimal.DecCalc.UInt32x32To64(ptr[(ulong)num11 * 4UL / 4UL], b);
							ptr[(ulong)num11 * 4UL / 4UL] = (uint)num9;
							num11 += 1U;
							num9 >>= 32;
						}
						while (num11 <= num10);
						if ((uint)num9 != 0U)
						{
							ptr[(IntPtr)((ulong)(num10 += 1U) * 4UL)] = (uint)num9;
						}
						i -= 9;
					}
					num9 = buf.Low64;
					num = d2.Low64;
					uint u = buf.U2;
					num2 = d2.High;
					if (sign)
					{
						num = num9 - num;
						num2 = u - num2;
						if (num > num9)
						{
							num2 -= 1U;
							if (num2 < u)
							{
								goto IL_350;
							}
						}
						else if (num2 <= u)
						{
							goto IL_350;
						}
						uint* ptr2 = (uint*)(&buf);
						uint num12 = 3U;
						uint num13;
						do
						{
							uint* ptr3 = ptr2 + (IntPtr)((ulong)num12++ * 4UL);
							num13 = *ptr3;
							*ptr3 = num13 - 1U;
						}
						while (num13 == 0U);
						if (ptr2[(ulong)num10 * 4UL / 4UL] == 0U && (num10 -= 1U) <= 2U)
						{
							goto IL_4B9;
						}
					}
					else
					{
						num += num9;
						num2 += u;
						if (num < num9)
						{
							num2 += 1U;
							if (num2 > u)
							{
								goto IL_350;
							}
						}
						else if (num2 >= u)
						{
							goto IL_350;
						}
						uint* ptr4 = (uint*)(&buf);
						uint num14 = 3U;
						do
						{
							uint* ptr5 = ptr4 + (IntPtr)((ulong)num14++ * 4UL);
							uint num13 = *ptr5 + 1U;
							*ptr5 = num13;
							if (num13 != 0U)
							{
								goto IL_350;
							}
						}
						while (num10 >= num14);
						ptr4[(ulong)num14 * 4UL / 4UL] = 1U;
						num10 = num14;
					}
					IL_350:
					buf.Low64 = num;
					buf.U2 = num2;
					i = decimal.DecCalc.ScaleResult(&buf, num10, (int)((byte)(num3 >> 16)));
					num3 = ((num3 & 4278255615U) | (uint)((uint)i << 16));
					num = buf.Low64;
					num2 = buf.U2;
					goto IL_4B9;
				}
				IL_450:
				ulong num15 = num;
				uint num16 = num2;
				if (sign)
				{
					num = num15 - d2.Low64;
					num2 = num16 - d2.High;
					if (num > num15)
					{
						num2 -= 1U;
						if (num2 < num16)
						{
							goto IL_4B9;
						}
					}
					else if (num2 <= num16)
					{
						goto IL_4B9;
					}
					num3 ^= 2147483648U;
					num2 = ~num2;
					num = -num;
					if (num == 0UL)
					{
						num2 += 1U;
					}
				}
				else
				{
					num = num15 + d2.Low64;
					num2 = num16 + d2.High;
					if (num < num15)
					{
						num2 += 1U;
						if (num2 > num16)
						{
							goto IL_4B9;
						}
					}
					else if (num2 >= num16)
					{
						goto IL_4B9;
					}
					if ((num3 & 16711680U) == 0U)
					{
						Number.ThrowOverflowException(TypeCode.Decimal);
					}
					num3 -= 65536U;
					ulong num17 = (ulong)num2 + 4294967296UL;
					num2 = (uint)(num17 / 10UL);
					num17 = (num17 - (ulong)(num2 * 10U) << 32) + (num >> 32);
					uint num18 = (uint)(num17 / 10UL);
					num17 = (num17 - (ulong)(num18 * 10U) << 32) + (ulong)((uint)num);
					num = (ulong)num18;
					num <<= 32;
					num18 = (uint)(num17 / 10UL);
					num += (ulong)num18;
					num18 = (uint)num17 - num18 * 10U;
					if (num18 >= 5U && (num18 > 5U || (num & 1UL) != 0UL) && (num += 1UL) == 0UL)
					{
						num2 += 1U;
					}
				}
				IL_4B9:
				d1.uflags = num3;
				d1.High = num2;
				d1.Low64 = num;
			}

			// Token: 0x0600028E RID: 654 RVA: 0x000B14FC File Offset: 0x000B06FC
			internal static long VarCyFromDec(ref decimal.DecCalc pdecIn)
			{
				int num = pdecIn.Scale - 4;
				long num4;
				if (num < 0)
				{
					if (pdecIn.High != 0U)
					{
						goto IL_93;
					}
					uint a = decimal.DecCalc.s_powers10[-num];
					ulong num2 = decimal.DecCalc.UInt32x32To64(a, pdecIn.Mid);
					if (num2 > (ulong)-1)
					{
						goto IL_93;
					}
					ulong num3 = decimal.DecCalc.UInt32x32To64(a, pdecIn.Low);
					num3 += (num2 <<= 32);
					if (num3 < num2)
					{
						goto IL_93;
					}
					num4 = (long)num3;
				}
				else
				{
					if (num != 0)
					{
						decimal.DecCalc.InternalRound(ref pdecIn, (uint)num, MidpointRounding.ToEven);
					}
					if (pdecIn.High != 0U)
					{
						goto IL_93;
					}
					num4 = (long)pdecIn.Low64;
				}
				if (num4 >= 0L || (num4 == -9223372036854775808L && pdecIn.IsNegative))
				{
					if (pdecIn.IsNegative)
					{
						num4 = -num4;
					}
					return num4;
				}
				IL_93:
				throw new OverflowException(SR.Overflow_Currency);
			}

			// Token: 0x0600028F RID: 655 RVA: 0x000B15A8 File Offset: 0x000B07A8
			internal static int VarDecCmp(in decimal d1, in decimal d2)
			{
				if ((d2.Low64 | (ulong)d2.High) == 0UL)
				{
					if ((d1.Low64 | (ulong)d1.High) == 0UL)
					{
						return 0;
					}
					return d1._flags >> 31 | 1;
				}
				else
				{
					if ((d1.Low64 | (ulong)d1.High) == 0UL)
					{
						return -(d2._flags >> 31 | 1);
					}
					int num = (d1._flags >> 31) - (d2._flags >> 31);
					if (num != 0)
					{
						return num;
					}
					return decimal.DecCalc.VarDecCmpSub(d1, d2);
				}
			}

			// Token: 0x06000290 RID: 656 RVA: 0x000B1620 File Offset: 0x000B0820
			private static int VarDecCmpSub(in decimal d1, in decimal d2)
			{
				int flags = d2._flags;
				int num = flags >> 31 | 1;
				int num2 = flags - d1._flags;
				ulong num3 = d1.Low64;
				uint num4 = d1.High;
				ulong num5 = d2.Low64;
				uint num6 = d2.High;
				if (num2 != 0)
				{
					num2 >>= 16;
					if (num2 < 0)
					{
						num2 = -num2;
						num = -num;
						ulong num7 = num3;
						num3 = num5;
						num5 = num7;
						uint num8 = num4;
						num4 = num6;
						num6 = num8;
					}
					for (;;)
					{
						uint b = (num2 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num2];
						ulong num9 = decimal.DecCalc.UInt32x32To64((uint)num3, b);
						ulong num10 = decimal.DecCalc.UInt32x32To64((uint)(num3 >> 32), b) + (num9 >> 32);
						num3 = (ulong)((uint)num9) + (num10 << 32);
						num10 >>= 32;
						num10 += decimal.DecCalc.UInt32x32To64(num4, b);
						if (num10 > (ulong)-1)
						{
							break;
						}
						num4 = (uint)num10;
						if ((num2 -= 9) <= 0)
						{
							goto IL_CB;
						}
					}
					return num;
				}
				IL_CB:
				uint num11 = num4 - num6;
				if (num11 != 0U)
				{
					if (num11 > num4)
					{
						num = -num;
					}
					return num;
				}
				ulong num12 = num3 - num5;
				if (num12 == 0UL)
				{
					num = 0;
				}
				else if (num12 > num3)
				{
					num = -num;
				}
				return num;
			}

			// Token: 0x06000291 RID: 657 RVA: 0x000B1728 File Offset: 0x000B0928
			internal unsafe static void VarDecMul(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				int num = (int)((byte)(d1.uflags + d2.uflags >> 16));
				decimal.DecCalc.Buf24 buf;
				Unsafe.SkipInit<decimal.DecCalc.Buf24>(out buf);
				uint num6;
				if ((d1.High | d1.Mid) == 0U)
				{
					ulong num4;
					if ((d2.High | d2.Mid) == 0U)
					{
						ulong num2 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
						if (num > 28)
						{
							if (num > 47)
							{
								goto IL_3B4;
							}
							num -= 29;
							ulong num3 = decimal.DecCalc.s_ulongPowers10[num];
							num4 = num2 / num3;
							ulong num5 = num2 - num4 * num3;
							num2 = num4;
							num3 >>= 1;
							if (num5 >= num3 && (num5 > num3 || ((uint)num2 & 1U) > 0U))
							{
								num2 += 1UL;
							}
							num = 28;
						}
						d1.Low64 = num2;
						d1.uflags = (((d2.uflags ^ d1.uflags) & 2147483648U) | (uint)((uint)num << 16));
						return;
					}
					num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
					buf.U0 = (uint)num4;
					num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Mid) + (num4 >> 32);
					buf.U1 = (uint)num4;
					num4 >>= 32;
					if (d2.High != 0U)
					{
						num4 += decimal.DecCalc.UInt32x32To64(d1.Low, d2.High);
						if (num4 > (ulong)-1)
						{
							buf.Mid64 = num4;
							num6 = 3U;
							goto IL_368;
						}
					}
					buf.U2 = (uint)num4;
					num6 = 2U;
				}
				else if ((d2.High | d2.Mid) == 0U)
				{
					ulong num4 = decimal.DecCalc.UInt32x32To64(d2.Low, d1.Low);
					buf.U0 = (uint)num4;
					num4 = decimal.DecCalc.UInt32x32To64(d2.Low, d1.Mid) + (num4 >> 32);
					buf.U1 = (uint)num4;
					num4 >>= 32;
					if (d1.High != 0U)
					{
						num4 += decimal.DecCalc.UInt32x32To64(d2.Low, d1.High);
						if (num4 > (ulong)-1)
						{
							buf.Mid64 = num4;
							num6 = 3U;
							goto IL_368;
						}
					}
					buf.U2 = (uint)num4;
					num6 = 2U;
				}
				else
				{
					ulong num4 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Low);
					buf.U0 = (uint)num4;
					ulong num7 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.Mid) + (num4 >> 32);
					num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.Low);
					num4 += num7;
					buf.U1 = (uint)num4;
					if (num4 < num7)
					{
						num7 = (num4 >> 32 | 4294967296UL);
					}
					else
					{
						num7 = num4 >> 32;
					}
					num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.Mid) + num7;
					if ((d1.High | d2.High) > 0U)
					{
						num7 = decimal.DecCalc.UInt32x32To64(d1.Low, d2.High);
						num4 += num7;
						uint num8 = 0U;
						if (num4 < num7)
						{
							num8 = 1U;
						}
						num7 = decimal.DecCalc.UInt32x32To64(d1.High, d2.Low);
						num4 += num7;
						buf.U2 = (uint)num4;
						if (num4 < num7)
						{
							num8 += 1U;
						}
						num7 = ((ulong)num8 << 32 | num4 >> 32);
						num4 = decimal.DecCalc.UInt32x32To64(d1.Mid, d2.High);
						num4 += num7;
						num8 = 0U;
						if (num4 < num7)
						{
							num8 = 1U;
						}
						num7 = decimal.DecCalc.UInt32x32To64(d1.High, d2.Mid);
						num4 += num7;
						buf.U3 = (uint)num4;
						if (num4 < num7)
						{
							num8 += 1U;
						}
						num4 = ((ulong)num8 << 32 | num4 >> 32);
						buf.High64 = decimal.DecCalc.UInt32x32To64(d1.High, d2.High) + num4;
						num6 = 5U;
					}
					else
					{
						buf.Mid64 = num4;
						num6 = 3U;
					}
				}
				uint* ptr = (uint*)(&buf);
				while (ptr[num6] == 0U)
				{
					if (num6 == 0U)
					{
						goto IL_3B4;
					}
					num6 -= 1U;
				}
				IL_368:
				if (num6 > 2U || num > 28)
				{
					num = decimal.DecCalc.ScaleResult(&buf, num6, num);
				}
				d1.Low64 = buf.Low64;
				d1.High = buf.U2;
				d1.uflags = (((d2.uflags ^ d1.uflags) & 2147483648U) | (uint)((uint)num << 16));
				return;
				IL_3B4:
				d1 = default(decimal.DecCalc);
			}

			// Token: 0x06000292 RID: 658 RVA: 0x000B1AF0 File Offset: 0x000B0CF0
			internal static void VarDecFromR4(float input, out decimal.DecCalc result)
			{
				result = default(decimal.DecCalc);
				int num = (int)(decimal.DecCalc.GetExponent(input) - 126U);
				if (num < -94)
				{
					return;
				}
				if (num > 96)
				{
					Number.ThrowOverflowException(TypeCode.Decimal);
				}
				uint num2 = 0U;
				if (input < 0f)
				{
					input = -input;
					num2 = 2147483648U;
				}
				double num3 = (double)input;
				int num4 = 6 - (num * 19728 >> 16);
				if (num4 >= 0)
				{
					if (num4 > 28)
					{
						num4 = 28;
					}
					num3 *= decimal.DecCalc.s_doublePowers10[num4];
				}
				else if (num4 != -1 || num3 >= 10000000.0)
				{
					num3 /= decimal.DecCalc.s_doublePowers10[-num4];
				}
				else
				{
					num4 = 0;
				}
				if (num3 < 1000000.0 && num4 < 28)
				{
					num3 *= 10.0;
					num4++;
				}
				uint num5;
				if (Sse41.IsSupported)
				{
					num5 = (uint)((int)Math.Round(num3));
				}
				else
				{
					num5 = (uint)((int)num3);
					num3 -= (double)num5;
					if (num3 > 0.5 || (num3 == 0.5 && (num5 & 1U) != 0U))
					{
						num5 += 1U;
					}
				}
				if (num5 == 0U)
				{
					return;
				}
				if (num4 < 0)
				{
					num4 = -num4;
					if (num4 < 10)
					{
						result.Low64 = decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4]);
					}
					else if (num4 > 18)
					{
						ulong a = decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4 - 18]);
						decimal.DecCalc.UInt64x64To128(a, 1000000000000000000UL, ref result);
					}
					else
					{
						ulong num6 = decimal.DecCalc.UInt32x32To64(num5, decimal.DecCalc.s_powers10[num4 - 9]);
						ulong num7 = decimal.DecCalc.UInt32x32To64(1000000000U, (uint)(num6 >> 32));
						num6 = decimal.DecCalc.UInt32x32To64(1000000000U, (uint)num6);
						result.Low = (uint)num6;
						num7 += num6 >> 32;
						result.Mid = (uint)num7;
						num7 >>= 32;
						result.High = (uint)num7;
					}
				}
				else
				{
					int num8 = num4;
					if (num8 > 6)
					{
						num8 = 6;
					}
					if ((num5 & 15U) == 0U && num8 >= 4)
					{
						uint num9 = num5 / 10000U;
						if (num5 == num9 * 10000U)
						{
							num5 = num9;
							num4 -= 4;
							num8 -= 4;
						}
					}
					if ((num5 & 3U) == 0U && num8 >= 2)
					{
						uint num10 = num5 / 100U;
						if (num5 == num10 * 100U)
						{
							num5 = num10;
							num4 -= 2;
							num8 -= 2;
						}
					}
					if ((num5 & 1U) == 0U && num8 >= 1)
					{
						uint num11 = num5 / 10U;
						if (num5 == num11 * 10U)
						{
							num5 = num11;
							num4--;
						}
					}
					num2 |= (uint)((uint)num4 << 16);
					result.Low = num5;
				}
				result.uflags = num2;
			}

			// Token: 0x06000293 RID: 659 RVA: 0x000B1D3C File Offset: 0x000B0F3C
			internal static void VarDecFromR8(double input, out decimal.DecCalc result)
			{
				result = default(decimal.DecCalc);
				int num = (int)(decimal.DecCalc.GetExponent(input) - 1022U);
				if (num < -94)
				{
					return;
				}
				if (num > 96)
				{
					Number.ThrowOverflowException(TypeCode.Decimal);
				}
				uint num2 = 0U;
				if (input < 0.0)
				{
					input = -input;
					num2 = 2147483648U;
				}
				double num3 = input;
				int num4 = 14 - (num * 19728 >> 16);
				if (num4 >= 0)
				{
					if (num4 > 28)
					{
						num4 = 28;
					}
					num3 *= decimal.DecCalc.s_doublePowers10[num4];
				}
				else if (num4 != -1 || num3 >= 1000000000000000.0)
				{
					num3 /= decimal.DecCalc.s_doublePowers10[-num4];
				}
				else
				{
					num4 = 0;
				}
				if (num3 < 100000000000000.0 && num4 < 28)
				{
					num3 *= 10.0;
					num4++;
				}
				ulong num5;
				if (Sse41.IsSupported)
				{
					num5 = (ulong)((long)Math.Round(num3));
				}
				else
				{
					num5 = (ulong)((long)num3);
					num3 -= (double)num5;
					if (num3 > 0.5 || (num3 == 0.5 && (num5 & 1UL) != 0UL))
					{
						num5 += 1UL;
					}
				}
				if (num5 == 0UL)
				{
					return;
				}
				if (num4 < 0)
				{
					num4 = -num4;
					if (num4 < 10)
					{
						uint b = decimal.DecCalc.s_powers10[num4];
						ulong num6 = decimal.DecCalc.UInt32x32To64((uint)num5, b);
						ulong num7 = decimal.DecCalc.UInt32x32To64((uint)(num5 >> 32), b);
						result.Low = (uint)num6;
						num7 += num6 >> 32;
						result.Mid = (uint)num7;
						num7 >>= 32;
						result.High = (uint)num7;
					}
					else
					{
						decimal.DecCalc.UInt64x64To128(num5, decimal.DecCalc.s_ulongPowers10[num4 - 1], ref result);
					}
				}
				else
				{
					int num8 = num4;
					if (num8 > 14)
					{
						num8 = 14;
					}
					if ((byte)num5 == 0 && num8 >= 8)
					{
						ulong num9 = num5 / 100000000UL;
						if ((uint)num5 == (uint)(num9 * 100000000UL))
						{
							num5 = num9;
							num4 -= 8;
							num8 -= 8;
						}
					}
					if (((uint)num5 & 15U) == 0U && num8 >= 4)
					{
						ulong num10 = num5 / 10000UL;
						if ((uint)num5 == (uint)(num10 * 10000UL))
						{
							num5 = num10;
							num4 -= 4;
							num8 -= 4;
						}
					}
					if (((uint)num5 & 3U) == 0U && num8 >= 2)
					{
						ulong num11 = num5 / 100UL;
						if ((uint)num5 == (uint)(num11 * 100UL))
						{
							num5 = num11;
							num4 -= 2;
							num8 -= 2;
						}
					}
					if (((uint)num5 & 1U) == 0U && num8 >= 1)
					{
						ulong num12 = num5 / 10UL;
						if ((uint)num5 == (uint)(num12 * 10UL))
						{
							num5 = num12;
							num4--;
						}
					}
					num2 |= (uint)((uint)num4 << 16);
					result.Low64 = num5;
				}
				result.uflags = num2;
			}

			// Token: 0x06000294 RID: 660 RVA: 0x000B1F8D File Offset: 0x000B118D
			internal static float VarR4FromDec(in decimal value)
			{
				return (float)decimal.DecCalc.VarR8FromDec(value);
			}

			// Token: 0x06000295 RID: 661 RVA: 0x000B1F98 File Offset: 0x000B1198
			internal static double VarR8FromDec(in decimal value)
			{
				double num = (value.Low64 + value.High * 1.8446744073709552E+19) / decimal.DecCalc.s_doublePowers10[value.Scale];
				if (value.IsNegative)
				{
					num = -num;
				}
				return num;
			}

			// Token: 0x06000296 RID: 662 RVA: 0x000B1FDC File Offset: 0x000B11DC
			internal static int GetHashCode(in decimal d)
			{
				if ((d.Low64 | (ulong)d.High) == 0UL)
				{
					return 0;
				}
				uint num = (uint)d._flags;
				if ((num & 16711680U) == 0U || (d.Low & 1U) != 0U)
				{
					return (int)(num ^ d.High ^ d.Mid ^ d.Low);
				}
				int num2 = (int)((byte)(num >> 16));
				uint low = d.Low;
				ulong num3 = (ulong)d.High << 32 | (ulong)d.Mid;
				decimal.DecCalc.Unscale(ref low, ref num3, ref num2);
				num = ((num & 4278255615U) | (uint)((uint)num2 << 16));
				return (int)(num ^ (uint)(num3 >> 32) ^ (uint)num3 ^ low);
			}

			// Token: 0x06000297 RID: 663 RVA: 0x000B2070 File Offset: 0x000B1270
			internal unsafe static void VarDecDiv(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				decimal.DecCalc.Buf12 buf;
				Unsafe.SkipInit<decimal.DecCalc.Buf12>(out buf);
				int num = (int)((sbyte)(d1.uflags - d2.uflags >> 16));
				bool flag = false;
				if ((d2.High | d2.Mid) == 0U)
				{
					uint low = d2.Low;
					if (low == 0U)
					{
						throw new DivideByZeroException();
					}
					buf.Low64 = d1.Low64;
					buf.U2 = d1.High;
					uint num2 = decimal.DecCalc.Div96By32(ref buf, low);
					for (;;)
					{
						int num3;
						if (num2 == 0U)
						{
							if (num >= 0)
							{
								goto IL_3F3;
							}
							num3 = Math.Min(9, -num);
						}
						else
						{
							flag = true;
							if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
							{
								break;
							}
						}
						uint num4 = decimal.DecCalc.s_powers10[num3];
						num += num3;
						if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
						{
							goto IL_4AB;
						}
						ulong num5 = decimal.DecCalc.UInt32x32To64(num2, num4);
						uint num6 = (uint)(num5 / (ulong)low);
						num2 = (uint)num5 - num6 * low;
						if (!decimal.DecCalc.Add32To96(ref buf, num6))
						{
							goto Block_11;
						}
					}
					uint num7 = num2 << 1;
					if (num7 < num2)
					{
						goto IL_46A;
					}
					if (num7 < low)
					{
						goto IL_3F3;
					}
					if (num7 > low)
					{
						goto IL_46A;
					}
					if ((buf.U0 & 1U) != 0U)
					{
						goto IL_46A;
					}
					goto IL_3F3;
					Block_11:
					num = decimal.DecCalc.OverflowUnscale(ref buf, num, num2 > 0U);
				}
				else
				{
					uint num7 = d2.High;
					if (num7 == 0U)
					{
						num7 = d2.Mid;
					}
					int num3 = BitOperations.LeadingZeroCount(num7);
					decimal.DecCalc.Buf16 buf2;
					Unsafe.SkipInit<decimal.DecCalc.Buf16>(out buf2);
					buf2.Low64 = d1.Low64 << num3;
					buf2.High64 = (ulong)d1.Mid + ((ulong)d1.High << 32) >> 32 - num3;
					ulong num8 = d2.Low64 << num3;
					if (d2.High == 0U)
					{
						buf.U2 = 0U;
						buf.U1 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2.U1), num8);
						buf.U0 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2), num8);
						for (;;)
						{
							if (buf2.Low64 == 0UL)
							{
								if (num >= 0)
								{
									goto IL_3F3;
								}
								num3 = Math.Min(9, -num);
							}
							else
							{
								flag = true;
								if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
								{
									break;
								}
							}
							uint num4 = decimal.DecCalc.s_powers10[num3];
							num += num3;
							if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
							{
								goto IL_4AB;
							}
							decimal.DecCalc.IncreaseScale64(ref *(decimal.DecCalc.Buf12*)(&buf2), num4);
							num7 = decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf2), num8);
							if (!decimal.DecCalc.Add32To96(ref buf, num7))
							{
								goto Block_22;
							}
						}
						ulong num9 = buf2.Low64;
						if (num9 < 0UL || (num9 <<= 1) > num8)
						{
							goto IL_46A;
						}
						if (num9 == num8 && (buf.U0 & 1U) != 0U)
						{
							goto IL_46A;
						}
						goto IL_3F3;
						Block_22:
						num = decimal.DecCalc.OverflowUnscale(ref buf, num, buf2.Low64 > 0UL);
					}
					else
					{
						decimal.DecCalc.Buf12 buf3;
						Unsafe.SkipInit<decimal.DecCalc.Buf12>(out buf3);
						buf3.Low64 = num8;
						buf3.U2 = (uint)((ulong)d2.Mid + ((ulong)d2.High << 32) >> 32 - num3);
						buf.Low64 = (ulong)decimal.DecCalc.Div128By96(ref buf2, ref buf3);
						buf.U2 = 0U;
						for (;;)
						{
							if ((buf2.Low64 | (ulong)buf2.U2) == 0UL)
							{
								if (num >= 0)
								{
									goto IL_3F3;
								}
								num3 = Math.Min(9, -num);
							}
							else
							{
								flag = true;
								if (num == 28 || (num3 = decimal.DecCalc.SearchScale(ref buf, num)) == 0)
								{
									break;
								}
							}
							uint num4 = decimal.DecCalc.s_powers10[num3];
							num += num3;
							if (decimal.DecCalc.IncreaseScale(ref buf, num4) != 0U)
							{
								goto IL_4AB;
							}
							buf2.U3 = decimal.DecCalc.IncreaseScale(ref *(decimal.DecCalc.Buf12*)(&buf2), num4);
							num7 = decimal.DecCalc.Div128By96(ref buf2, ref buf3);
							if (!decimal.DecCalc.Add32To96(ref buf, num7))
							{
								goto Block_33;
							}
						}
						if (buf2.U2 < 0U)
						{
							goto IL_46A;
						}
						num7 = buf2.U1 >> 31;
						buf2.Low64 <<= 1;
						buf2.U2 = (buf2.U2 << 1) + num7;
						if (buf2.U2 > buf3.U2)
						{
							goto IL_46A;
						}
						if (buf2.U2 != buf3.U2)
						{
							goto IL_3F3;
						}
						if (buf2.Low64 > buf3.Low64)
						{
							goto IL_46A;
						}
						if (buf2.Low64 == buf3.Low64 && (buf.U0 & 1U) != 0U)
						{
							goto IL_46A;
						}
						goto IL_3F3;
						Block_33:
						num = decimal.DecCalc.OverflowUnscale(ref buf, num, (buf2.Low64 | buf2.High64) > 0UL);
					}
				}
				IL_3F3:
				if (flag)
				{
					uint u = buf.U0;
					ulong high = buf.High64;
					decimal.DecCalc.Unscale(ref u, ref high, ref num);
					d1.Low = u;
					d1.Mid = (uint)high;
					d1.High = (uint)(high >> 32);
				}
				else
				{
					d1.Low64 = buf.Low64;
					d1.High = buf.U2;
				}
				d1.uflags = (((d1.uflags ^ d2.uflags) & 2147483648U) | (uint)((uint)num << 16));
				return;
				IL_46A:
				ulong num10 = buf.Low64 + 1UL;
				buf.Low64 = num10;
				if (num10 != 0UL)
				{
					goto IL_3F3;
				}
				uint num11 = buf.U2 + 1U;
				buf.U2 = num11;
				if (num11 == 0U)
				{
					num = decimal.DecCalc.OverflowUnscale(ref buf, num, true);
					goto IL_3F3;
				}
				goto IL_3F3;
				IL_4AB:
				Number.ThrowOverflowException(TypeCode.Decimal);
			}

			// Token: 0x06000298 RID: 664 RVA: 0x000B2530 File Offset: 0x000B1730
			internal static void VarDecMod(ref decimal.DecCalc d1, ref decimal.DecCalc d2)
			{
				if ((d2.ulo | d2.umid | d2.uhi) == 0U)
				{
					throw new DivideByZeroException();
				}
				if ((d1.ulo | d1.umid | d1.uhi) == 0U)
				{
					return;
				}
				d2.uflags = ((d2.uflags & 2147483647U) | (d1.uflags & 2147483648U));
				int num = decimal.DecCalc.VarDecCmpSub(Unsafe.As<decimal.DecCalc, decimal>(ref d1), Unsafe.As<decimal.DecCalc, decimal>(ref d2));
				if (num == 0)
				{
					d1.ulo = 0U;
					d1.umid = 0U;
					d1.uhi = 0U;
					if (d2.uflags > d1.uflags)
					{
						d1.uflags = d2.uflags;
					}
					return;
				}
				if ((num ^ (int)(d1.uflags & 2147483648U)) < 0)
				{
					return;
				}
				int num2 = (int)((sbyte)(d1.uflags - d2.uflags >> 16));
				if (num2 > 0)
				{
					do
					{
						uint num3 = (num2 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num2];
						ulong num4 = decimal.DecCalc.UInt32x32To64(d2.Low, num3);
						d2.Low = (uint)num4;
						num4 >>= 32;
						num4 += ((ulong)d2.Mid + ((ulong)d2.High << 32)) * (ulong)num3;
						d2.Mid = (uint)num4;
						d2.High = (uint)(num4 >> 32);
					}
					while ((num2 -= 9) > 0);
					num2 = 0;
				}
				for (;;)
				{
					if (num2 < 0)
					{
						d1.uflags = d2.uflags;
						decimal.DecCalc.Buf12 buf;
						Unsafe.SkipInit<decimal.DecCalc.Buf12>(out buf);
						buf.Low64 = d1.Low64;
						buf.U2 = d1.High;
						uint num6;
						do
						{
							int num5 = decimal.DecCalc.SearchScale(ref buf, 28 + num2);
							if (num5 == 0)
							{
								break;
							}
							num6 = ((num5 >= 9) ? 1000000000U : decimal.DecCalc.s_powers10[num5]);
							num2 += num5;
							ulong num7 = decimal.DecCalc.UInt32x32To64(buf.U0, num6);
							buf.U0 = (uint)num7;
							num7 >>= 32;
							buf.High64 = num7 + buf.High64 * (ulong)num6;
						}
						while (num6 == 1000000000U && num2 < 0);
						d1.Low64 = buf.Low64;
						d1.High = buf.U2;
					}
					if (d1.High == 0U)
					{
						break;
					}
					if ((d2.High | d2.Mid) != 0U)
					{
						goto IL_250;
					}
					uint low = d2.Low;
					ulong num8 = (ulong)d1.High << 32 | (ulong)d1.Mid;
					num8 = (num8 % (ulong)low << 32 | (ulong)d1.Low);
					d1.Low64 = num8 % (ulong)low;
					d1.High = 0U;
					if (num2 >= 0)
					{
						return;
					}
				}
				d1.Low64 %= d2.Low64;
				return;
				IL_250:
				decimal.DecCalc.VarDecModFull(ref d1, ref d2, num2);
			}

			// Token: 0x06000299 RID: 665 RVA: 0x000B27A0 File Offset: 0x000B19A0
			private unsafe static void VarDecModFull(ref decimal.DecCalc d1, ref decimal.DecCalc d2, int scale)
			{
				uint num = d2.High;
				if (num == 0U)
				{
					num = d2.Mid;
				}
				int num2 = BitOperations.LeadingZeroCount(num);
				decimal.DecCalc.Buf28 buf;
				Unsafe.SkipInit<decimal.DecCalc.Buf28>(out buf);
				buf.Buf24.Low64 = d1.Low64 << num2;
				buf.Buf24.Mid64 = (ulong)d1.Mid + ((ulong)d1.High << 32) >> 32 - num2;
				uint num3 = 3U;
				while (scale < 0)
				{
					uint b = (scale <= -9) ? 1000000000U : decimal.DecCalc.s_powers10[-scale];
					uint* ptr = (uint*)(&buf);
					ulong num4 = decimal.DecCalc.UInt32x32To64(buf.Buf24.U0, b);
					buf.Buf24.U0 = (uint)num4;
					int num5 = 1;
					while ((long)num5 <= (long)((ulong)num3))
					{
						num4 >>= 32;
						num4 += decimal.DecCalc.UInt32x32To64(ptr[num5], b);
						ptr[num5] = (uint)num4;
						num5++;
					}
					if (num4 > 2147483647UL)
					{
						ptr[(IntPtr)((ulong)(num3 += 1U) * 4UL)] = (uint)(num4 >> 32);
					}
					scale += 9;
				}
				if (d2.High == 0U)
				{
					ulong den = d2.Low64 << num2;
					switch (num3)
					{
					case 4U:
						goto IL_15E;
					case 5U:
						break;
					case 6U:
						decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U4), den);
						break;
					default:
						goto IL_173;
					}
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U3), den);
					IL_15E:
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U2), den);
					IL_173:
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf.Buf24.U1), den);
					decimal.DecCalc.Div96By64(ref *(decimal.DecCalc.Buf12*)(&buf), den);
					d1.Low64 = buf.Buf24.Low64 >> num2;
					d1.High = 0U;
					return;
				}
				decimal.DecCalc.Buf12 buf2;
				Unsafe.SkipInit<decimal.DecCalc.Buf12>(out buf2);
				buf2.Low64 = d2.Low64 << num2;
				buf2.U2 = (uint)((ulong)d2.Mid + ((ulong)d2.High << 32) >> 32 - num2);
				switch (num3)
				{
				case 4U:
					goto IL_22D;
				case 5U:
					break;
				case 6U:
					decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf.Buf24.U3), ref buf2);
					break;
				default:
					goto IL_242;
				}
				decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf.Buf24.U2), ref buf2);
				IL_22D:
				decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf.Buf24.U1), ref buf2);
				IL_242:
				decimal.DecCalc.Div128By96(ref *(decimal.DecCalc.Buf16*)(&buf), ref buf2);
				d1.Low64 = (buf.Buf24.Low64 >> num2) + ((ulong)buf.Buf24.U2 << 32 - num2 << 32);
				d1.High = buf.Buf24.U2 >> num2;
			}

			// Token: 0x0600029A RID: 666 RVA: 0x000B2A40 File Offset: 0x000B1C40
			internal static void InternalRound(ref decimal.DecCalc d, uint scale, MidpointRounding mode)
			{
				d.uflags -= scale << 16;
				uint num = 0U;
				uint num5;
				while (scale >= 9U)
				{
					scale -= 9U;
					uint num2 = d.uhi;
					uint num4;
					if (num2 == 0U)
					{
						ulong low = d.Low64;
						ulong num3 = low / 1000000000UL;
						d.Low64 = num3;
						num4 = (uint)(low - num3 * 1000000000UL);
					}
					else
					{
						num4 = num2 - (d.uhi = num2 / 1000000000U) * 1000000000U;
						num2 = d.umid;
						if ((num2 | num4) != 0U)
						{
							num4 = num2 - (d.umid = (uint)(((ulong)num4 << 32 | (ulong)num2) / 1000000000UL)) * 1000000000U;
						}
						num2 = d.ulo;
						if ((num2 | num4) != 0U)
						{
							num4 = num2 - (d.ulo = (uint)(((ulong)num4 << 32 | (ulong)num2) / 1000000000UL)) * 1000000000U;
						}
					}
					num5 = 1000000000U;
					if (scale == 0U)
					{
						IL_199:
						if (mode != MidpointRounding.ToZero)
						{
							if (mode == MidpointRounding.ToEven)
							{
								num4 <<= 1;
								if ((num | (d.ulo & 1U)) != 0U)
								{
									num4 += 1U;
								}
								if (num5 >= num4)
								{
									return;
								}
							}
							else if (mode == MidpointRounding.AwayFromZero)
							{
								num4 <<= 1;
								if (num5 > num4)
								{
									return;
								}
							}
							else if (mode == MidpointRounding.ToNegativeInfinity)
							{
								if ((num4 | num) == 0U)
								{
									return;
								}
								if (!d.IsNegative)
								{
									return;
								}
							}
							else if ((num4 | num) == 0U || d.IsNegative)
							{
								return;
							}
							ulong num6 = d.Low64 + 1UL;
							d.Low64 = num6;
							if (num6 == 0UL)
							{
								d.uhi += 1U;
							}
						}
						return;
					}
					num |= num4;
				}
				num5 = decimal.DecCalc.s_powers10[(int)scale];
				uint num7 = d.uhi;
				if (num7 == 0U)
				{
					ulong low2 = d.Low64;
					if (low2 != 0UL)
					{
						ulong num8 = low2 / (ulong)num5;
						d.Low64 = num8;
						uint num4 = (uint)(low2 - num8 * (ulong)num5);
						goto IL_199;
					}
					if (mode > MidpointRounding.ToZero)
					{
						uint num4 = 0U;
						goto IL_199;
					}
					return;
				}
				else
				{
					uint num4 = num7 - (d.uhi = num7 / num5) * num5;
					num7 = d.umid;
					if ((num7 | num4) != 0U)
					{
						num4 = num7 - (d.umid = (uint)(((ulong)num4 << 32 | (ulong)num7) / (ulong)num5)) * num5;
					}
					num7 = d.ulo;
					if ((num7 | num4) != 0U)
					{
						num4 = num7 - (d.ulo = (uint)(((ulong)num4 << 32 | (ulong)num7) / (ulong)num5)) * num5;
						goto IL_199;
					}
					goto IL_199;
				}
			}

			// Token: 0x0600029B RID: 667 RVA: 0x000B2C54 File Offset: 0x000B1E54
			internal static uint DecDivMod1E9(ref decimal.DecCalc value)
			{
				ulong num = ((ulong)value.uhi << 32) + (ulong)value.umid;
				ulong num2 = num / 1000000000UL;
				value.uhi = (uint)(num2 >> 32);
				value.umid = (uint)num2;
				ulong num3 = (num - (ulong)((uint)num2 * 1000000000U) << 32) + (ulong)value.ulo;
				uint num4 = (uint)(num3 / 1000000000UL);
				value.ulo = num4;
				return (uint)num3 - num4 * 1000000000U;
			}

			// Token: 0x040000E4 RID: 228
			[FieldOffset(0)]
			private uint uflags;

			// Token: 0x040000E5 RID: 229
			[FieldOffset(4)]
			private uint uhi;

			// Token: 0x040000E6 RID: 230
			[FieldOffset(8)]
			private uint ulo;

			// Token: 0x040000E7 RID: 231
			[FieldOffset(12)]
			private uint umid;

			// Token: 0x040000E8 RID: 232
			[FieldOffset(8)]
			private ulong ulomid;

			// Token: 0x040000E9 RID: 233
			private static readonly uint[] s_powers10 = new uint[]
			{
				1U,
				10U,
				100U,
				1000U,
				10000U,
				100000U,
				1000000U,
				10000000U,
				100000000U,
				1000000000U
			};

			// Token: 0x040000EA RID: 234
			private static readonly ulong[] s_ulongPowers10 = new ulong[]
			{
				10UL,
				100UL,
				1000UL,
				10000UL,
				100000UL,
				1000000UL,
				10000000UL,
				100000000UL,
				1000000000UL,
				10000000000UL,
				100000000000UL,
				1000000000000UL,
				10000000000000UL,
				100000000000000UL,
				1000000000000000UL,
				10000000000000000UL,
				100000000000000000UL,
				1000000000000000000UL,
				10000000000000000000UL
			};

			// Token: 0x040000EB RID: 235
			private static readonly double[] s_doublePowers10 = new double[]
			{
				1.0,
				10.0,
				100.0,
				1000.0,
				10000.0,
				100000.0,
				1000000.0,
				10000000.0,
				100000000.0,
				1000000000.0,
				10000000000.0,
				100000000000.0,
				1000000000000.0,
				10000000000000.0,
				100000000000000.0,
				1000000000000000.0,
				10000000000000000.0,
				1E+17,
				1E+18,
				1E+19,
				1E+20,
				1E+21,
				1E+22,
				1E+23,
				1E+24,
				1E+25,
				1E+26,
				1E+27,
				1E+28,
				1E+29,
				1E+30,
				1E+31,
				1E+32,
				1E+33,
				1E+34,
				1E+35,
				1E+36,
				1E+37,
				1E+38,
				1E+39,
				1E+40,
				1E+41,
				1E+42,
				1E+43,
				1E+44,
				1E+45,
				1E+46,
				1E+47,
				1E+48,
				1E+49,
				1E+50,
				1E+51,
				1E+52,
				1E+53,
				1E+54,
				1E+55,
				1E+56,
				1E+57,
				1E+58,
				1E+59,
				1E+60,
				1E+61,
				1E+62,
				1E+63,
				1E+64,
				1E+65,
				1E+66,
				1E+67,
				1E+68,
				1E+69,
				1E+70,
				1E+71,
				1E+72,
				1E+73,
				1E+74,
				1E+75,
				1E+76,
				1E+77,
				1E+78,
				1E+79,
				1E+80
			};

			// Token: 0x040000EC RID: 236
			private static readonly decimal.DecCalc.PowerOvfl[] PowerOvflValues = new decimal.DecCalc.PowerOvfl[]
			{
				new decimal.DecCalc.PowerOvfl(429496729U, 2576980377U, 2576980377U),
				new decimal.DecCalc.PowerOvfl(42949672U, 4123168604U, 687194767U),
				new decimal.DecCalc.PowerOvfl(4294967U, 1271310319U, 2645699854U),
				new decimal.DecCalc.PowerOvfl(429496U, 3133608139U, 694066715U),
				new decimal.DecCalc.PowerOvfl(42949U, 2890341191U, 2216890319U),
				new decimal.DecCalc.PowerOvfl(4294U, 4154504685U, 2369172679U),
				new decimal.DecCalc.PowerOvfl(429U, 2133437386U, 4102387834U),
				new decimal.DecCalc.PowerOvfl(42U, 4078814305U, 410238783U)
			};

			// Token: 0x0200005E RID: 94
			private struct PowerOvfl
			{
				// Token: 0x0600029D RID: 669 RVA: 0x000B2DF6 File Offset: 0x000B1FF6
				public PowerOvfl(uint hi, uint mid, uint lo)
				{
					this.Hi = hi;
					this.MidLo = ((ulong)mid << 32) + (ulong)lo;
				}

				// Token: 0x040000ED RID: 237
				public readonly uint Hi;

				// Token: 0x040000EE RID: 238
				public readonly ulong MidLo;
			}

			// Token: 0x0200005F RID: 95
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf12
			{
				// Token: 0x17000020 RID: 32
				// (get) Token: 0x0600029E RID: 670 RVA: 0x000B2E0D File Offset: 0x000B200D
				// (set) Token: 0x0600029F RID: 671 RVA: 0x000B2E15 File Offset: 0x000B2015
				public ulong Low64
				{
					get
					{
						return this.ulo64LE;
					}
					set
					{
						this.ulo64LE = value;
					}
				}

				// Token: 0x17000021 RID: 33
				// (get) Token: 0x060002A0 RID: 672 RVA: 0x000B2E1E File Offset: 0x000B201E
				// (set) Token: 0x060002A1 RID: 673 RVA: 0x000B2E26 File Offset: 0x000B2026
				public ulong High64
				{
					get
					{
						return this.uhigh64LE;
					}
					set
					{
						this.uhigh64LE = value;
					}
				}

				// Token: 0x040000EF RID: 239
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x040000F0 RID: 240
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x040000F1 RID: 241
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x040000F2 RID: 242
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x040000F3 RID: 243
				[FieldOffset(4)]
				private ulong uhigh64LE;
			}

			// Token: 0x02000060 RID: 96
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf16
			{
				// Token: 0x17000022 RID: 34
				// (get) Token: 0x060002A2 RID: 674 RVA: 0x000B2E2F File Offset: 0x000B202F
				// (set) Token: 0x060002A3 RID: 675 RVA: 0x000B2E37 File Offset: 0x000B2037
				public ulong Low64
				{
					get
					{
						return this.ulo64LE;
					}
					set
					{
						this.ulo64LE = value;
					}
				}

				// Token: 0x17000023 RID: 35
				// (get) Token: 0x060002A4 RID: 676 RVA: 0x000B2E40 File Offset: 0x000B2040
				// (set) Token: 0x060002A5 RID: 677 RVA: 0x000B2E48 File Offset: 0x000B2048
				public ulong High64
				{
					get
					{
						return this.uhigh64LE;
					}
					set
					{
						this.uhigh64LE = value;
					}
				}

				// Token: 0x040000F4 RID: 244
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x040000F5 RID: 245
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x040000F6 RID: 246
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x040000F7 RID: 247
				[FieldOffset(12)]
				public uint U3;

				// Token: 0x040000F8 RID: 248
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x040000F9 RID: 249
				[FieldOffset(8)]
				private ulong uhigh64LE;
			}

			// Token: 0x02000061 RID: 97
			[StructLayout(LayoutKind.Explicit)]
			private struct Buf24
			{
				// Token: 0x17000024 RID: 36
				// (get) Token: 0x060002A6 RID: 678 RVA: 0x000B2E51 File Offset: 0x000B2051
				// (set) Token: 0x060002A7 RID: 679 RVA: 0x000B2E59 File Offset: 0x000B2059
				public ulong Low64
				{
					get
					{
						return this.ulo64LE;
					}
					set
					{
						this.ulo64LE = value;
					}
				}

				// Token: 0x17000025 RID: 37
				// (set) Token: 0x060002A8 RID: 680 RVA: 0x000B2E62 File Offset: 0x000B2062
				public ulong Mid64
				{
					set
					{
						this.umid64LE = value;
					}
				}

				// Token: 0x17000026 RID: 38
				// (set) Token: 0x060002A9 RID: 681 RVA: 0x000B2E6B File Offset: 0x000B206B
				public ulong High64
				{
					set
					{
						this.uhigh64LE = value;
					}
				}

				// Token: 0x040000FA RID: 250
				[FieldOffset(0)]
				public uint U0;

				// Token: 0x040000FB RID: 251
				[FieldOffset(4)]
				public uint U1;

				// Token: 0x040000FC RID: 252
				[FieldOffset(8)]
				public uint U2;

				// Token: 0x040000FD RID: 253
				[FieldOffset(12)]
				public uint U3;

				// Token: 0x040000FE RID: 254
				[FieldOffset(16)]
				public uint U4;

				// Token: 0x040000FF RID: 255
				[FieldOffset(20)]
				public uint U5;

				// Token: 0x04000100 RID: 256
				[FieldOffset(0)]
				private ulong ulo64LE;

				// Token: 0x04000101 RID: 257
				[FieldOffset(8)]
				private ulong umid64LE;

				// Token: 0x04000102 RID: 258
				[FieldOffset(16)]
				private ulong uhigh64LE;
			}

			// Token: 0x02000062 RID: 98
			private struct Buf28
			{
				// Token: 0x04000103 RID: 259
				public decimal.DecCalc.Buf24 Buf24;

				// Token: 0x04000104 RID: 260
				public uint U6;
			}
		}
	}
}
