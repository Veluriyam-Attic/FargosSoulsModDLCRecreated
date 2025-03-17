using System;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000250 RID: 592
	public readonly struct StandardFormat : IEquatable<StandardFormat>
	{
		// Token: 0x1700080E RID: 2062
		// (get) Token: 0x06002449 RID: 9289 RVA: 0x00139625 File Offset: 0x00138825
		public char Symbol
		{
			get
			{
				return (char)this._format;
			}
		}

		// Token: 0x1700080F RID: 2063
		// (get) Token: 0x0600244A RID: 9290 RVA: 0x0013962D File Offset: 0x0013882D
		public byte Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x17000810 RID: 2064
		// (get) Token: 0x0600244B RID: 9291 RVA: 0x00139635 File Offset: 0x00138835
		public bool HasPrecision
		{
			get
			{
				return this._precision != byte.MaxValue;
			}
		}

		// Token: 0x17000811 RID: 2065
		// (get) Token: 0x0600244C RID: 9292 RVA: 0x00139647 File Offset: 0x00138847
		public bool IsDefault
		{
			get
			{
				return this._format == 0 && this._precision == 0;
			}
		}

		// Token: 0x0600244D RID: 9293 RVA: 0x0013965C File Offset: 0x0013885C
		public StandardFormat(char symbol, byte precision = 255)
		{
			if (precision != 255 && precision > 99)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_PrecisionTooLarge();
			}
			if (symbol != (char)((byte)symbol))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException_SymbolDoesNotFit();
			}
			this._format = (byte)symbol;
			this._precision = precision;
		}

		// Token: 0x0600244E RID: 9294 RVA: 0x00139689 File Offset: 0x00138889
		public static implicit operator StandardFormat(char symbol)
		{
			return new StandardFormat(symbol, byte.MaxValue);
		}

		// Token: 0x0600244F RID: 9295 RVA: 0x00139698 File Offset: 0x00138898
		public static StandardFormat Parse(ReadOnlySpan<char> format)
		{
			StandardFormat result;
			StandardFormat.ParseHelper(format, out result, true);
			return result;
		}

		// Token: 0x06002450 RID: 9296 RVA: 0x001396B0 File Offset: 0x001388B0
		[NullableContext(2)]
		public static StandardFormat Parse(string format)
		{
			if (format != null)
			{
				return StandardFormat.Parse(format.AsSpan());
			}
			return default(StandardFormat);
		}

		// Token: 0x06002451 RID: 9297 RVA: 0x001396D5 File Offset: 0x001388D5
		public static bool TryParse(ReadOnlySpan<char> format, out StandardFormat result)
		{
			return StandardFormat.ParseHelper(format, out result, false);
		}

		// Token: 0x06002452 RID: 9298 RVA: 0x001396E0 File Offset: 0x001388E0
		private unsafe static bool ParseHelper(ReadOnlySpan<char> format, out StandardFormat standardFormat, bool throws = false)
		{
			standardFormat = default(StandardFormat);
			if (format.Length == 0)
			{
				return true;
			}
			char symbol = (char)(*format[0]);
			byte precision;
			if (format.Length == 1)
			{
				precision = byte.MaxValue;
			}
			else
			{
				uint num = 0U;
				int i = 1;
				while (i < format.Length)
				{
					uint num2 = (uint)(*format[i] - 48);
					if (num2 > 9U)
					{
						if (!throws)
						{
							return false;
						}
						throw new FormatException(SR.Format(SR.Argument_CannotParsePrecision, 99));
					}
					else
					{
						num = num * 10U + num2;
						if (num > 99U)
						{
							if (!throws)
							{
								return false;
							}
							throw new FormatException(SR.Format(SR.Argument_PrecisionTooLarge, 99));
						}
						else
						{
							i++;
						}
					}
				}
				precision = (byte)num;
			}
			standardFormat = new StandardFormat(symbol, precision);
			return true;
		}

		// Token: 0x06002453 RID: 9299 RVA: 0x0013979C File Offset: 0x0013899C
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is StandardFormat)
			{
				StandardFormat other = (StandardFormat)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06002454 RID: 9300 RVA: 0x001397C1 File Offset: 0x001389C1
		public override int GetHashCode()
		{
			return this._format.GetHashCode() ^ this._precision.GetHashCode();
		}

		// Token: 0x06002455 RID: 9301 RVA: 0x001397DA File Offset: 0x001389DA
		public bool Equals(StandardFormat other)
		{
			return this._format == other._format && this._precision == other._precision;
		}

		// Token: 0x06002456 RID: 9302 RVA: 0x001397FC File Offset: 0x001389FC
		[NullableContext(1)]
		public unsafe override string ToString()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)6], 3);
			Span<char> destination = span;
			int length = this.Format(destination);
			return new string(destination.Slice(0, length));
		}

		// Token: 0x06002457 RID: 9303 RVA: 0x00139834 File Offset: 0x00138A34
		internal unsafe int Format(Span<char> destination)
		{
			int num = 0;
			char symbol = this.Symbol;
			if (symbol != '\0' && destination.Length == 3)
			{
				*destination[0] = symbol;
				num = 1;
				uint precision = (uint)this.Precision;
				if (precision != 255U)
				{
					if (precision >= 10U)
					{
						uint num2 = Math.DivRem(precision, 10U, out precision);
						*destination[1] = (char)(48U + num2 % 10U);
						num = 2;
					}
					*destination[num] = (char)(48U + precision);
					num++;
				}
			}
			return num;
		}

		// Token: 0x06002458 RID: 9304 RVA: 0x001398A8 File Offset: 0x00138AA8
		public static bool operator ==(StandardFormat left, StandardFormat right)
		{
			return left.Equals(right);
		}

		// Token: 0x06002459 RID: 9305 RVA: 0x001398B2 File Offset: 0x00138AB2
		public static bool operator !=(StandardFormat left, StandardFormat right)
		{
			return !left.Equals(right);
		}

		// Token: 0x04000985 RID: 2437
		public const byte NoPrecision = 255;

		// Token: 0x04000986 RID: 2438
		public const byte MaxPrecision = 99;

		// Token: 0x04000987 RID: 2439
		private readonly byte _format;

		// Token: 0x04000988 RID: 2440
		private readonly byte _precision;
	}
}
