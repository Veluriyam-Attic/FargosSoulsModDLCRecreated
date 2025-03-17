using System;
using System.Buffers;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.Unicode;

namespace System.Text
{
	// Token: 0x0200037D RID: 893
	[DebuggerDisplay("{DebuggerDisplay,nq}")]
	public readonly struct Rune : IComparable, IComparable<Rune>, IEquatable<Rune>
	{
		// Token: 0x1700095C RID: 2396
		// (get) Token: 0x06002F0D RID: 12045 RVA: 0x0015F492 File Offset: 0x0015E692
		private unsafe static ReadOnlySpan<byte> AsciiCharInfo
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.2F3EFC9595514E83DED03093C4F3E3C781A650E1AAB8CA350537CD1A47E1EE8E), 128);
			}
		}

		// Token: 0x06002F0E RID: 12046 RVA: 0x0015F4A4 File Offset: 0x0015E6A4
		public Rune(char ch)
		{
			if (UnicodeUtility.IsSurrogateCodePoint((uint)ch))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.ch);
			}
			this._value = (uint)ch;
		}

		// Token: 0x06002F0F RID: 12047 RVA: 0x0015F4C9 File Offset: 0x0015E6C9
		public Rune(char highSurrogate, char lowSurrogate)
		{
			this = new Rune((uint)char.ConvertToUtf32(highSurrogate, lowSurrogate), false);
		}

		// Token: 0x06002F10 RID: 12048 RVA: 0x0015F4D9 File Offset: 0x0015E6D9
		public Rune(int value)
		{
			this = new Rune((uint)value);
		}

		// Token: 0x06002F11 RID: 12049 RVA: 0x0015F4E2 File Offset: 0x0015E6E2
		[CLSCompliant(false)]
		public Rune(uint value)
		{
			if (!UnicodeUtility.IsValidUnicodeScalar(value))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			this._value = value;
		}

		// Token: 0x06002F12 RID: 12050 RVA: 0x0015F4F9 File Offset: 0x0015E6F9
		private Rune(uint scalarValue, bool unused)
		{
			this._value = scalarValue;
		}

		// Token: 0x06002F13 RID: 12051 RVA: 0x0015F502 File Offset: 0x0015E702
		public static bool operator ==(Rune left, Rune right)
		{
			return left._value == right._value;
		}

		// Token: 0x06002F14 RID: 12052 RVA: 0x0015F512 File Offset: 0x0015E712
		public static bool operator !=(Rune left, Rune right)
		{
			return left._value != right._value;
		}

		// Token: 0x06002F15 RID: 12053 RVA: 0x0015F525 File Offset: 0x0015E725
		public static bool operator <(Rune left, Rune right)
		{
			return left._value < right._value;
		}

		// Token: 0x06002F16 RID: 12054 RVA: 0x0015F535 File Offset: 0x0015E735
		public static bool operator <=(Rune left, Rune right)
		{
			return left._value <= right._value;
		}

		// Token: 0x06002F17 RID: 12055 RVA: 0x0015F548 File Offset: 0x0015E748
		public static bool operator >(Rune left, Rune right)
		{
			return left._value > right._value;
		}

		// Token: 0x06002F18 RID: 12056 RVA: 0x0015F558 File Offset: 0x0015E758
		public static bool operator >=(Rune left, Rune right)
		{
			return left._value >= right._value;
		}

		// Token: 0x06002F19 RID: 12057 RVA: 0x0015F56B File Offset: 0x0015E76B
		public static explicit operator Rune(char ch)
		{
			return new Rune(ch);
		}

		// Token: 0x06002F1A RID: 12058 RVA: 0x0015F573 File Offset: 0x0015E773
		[CLSCompliant(false)]
		public static explicit operator Rune(uint value)
		{
			return new Rune(value);
		}

		// Token: 0x06002F1B RID: 12059 RVA: 0x0015F57B File Offset: 0x0015E77B
		public static explicit operator Rune(int value)
		{
			return new Rune(value);
		}

		// Token: 0x1700095D RID: 2397
		// (get) Token: 0x06002F1C RID: 12060 RVA: 0x0015F584 File Offset: 0x0015E784
		[Nullable(1)]
		private string DebuggerDisplay
		{
			get
			{
				return FormattableString.Invariant(FormattableStringFactory.Create("U+{0:X4} '{1}'", new object[]
				{
					this._value,
					Rune.IsValid(this._value) ? this.ToString() : "�"
				}));
			}
		}

		// Token: 0x1700095E RID: 2398
		// (get) Token: 0x06002F1D RID: 12061 RVA: 0x0015F5D7 File Offset: 0x0015E7D7
		public bool IsAscii
		{
			get
			{
				return UnicodeUtility.IsAsciiCodePoint(this._value);
			}
		}

		// Token: 0x1700095F RID: 2399
		// (get) Token: 0x06002F1E RID: 12062 RVA: 0x0015F5E4 File Offset: 0x0015E7E4
		public bool IsBmp
		{
			get
			{
				return UnicodeUtility.IsBmpCodePoint(this._value);
			}
		}

		// Token: 0x17000960 RID: 2400
		// (get) Token: 0x06002F1F RID: 12063 RVA: 0x0015F5F1 File Offset: 0x0015E7F1
		public int Plane
		{
			get
			{
				return UnicodeUtility.GetPlane(this._value);
			}
		}

		// Token: 0x17000961 RID: 2401
		// (get) Token: 0x06002F20 RID: 12064 RVA: 0x0015F5FE File Offset: 0x0015E7FE
		public static Rune ReplacementChar
		{
			get
			{
				return Rune.UnsafeCreate(65533U);
			}
		}

		// Token: 0x17000962 RID: 2402
		// (get) Token: 0x06002F21 RID: 12065 RVA: 0x0015F60C File Offset: 0x0015E80C
		public int Utf16SequenceLength
		{
			get
			{
				return UnicodeUtility.GetUtf16SequenceLength(this._value);
			}
		}

		// Token: 0x17000963 RID: 2403
		// (get) Token: 0x06002F22 RID: 12066 RVA: 0x0015F628 File Offset: 0x0015E828
		public int Utf8SequenceLength
		{
			get
			{
				return UnicodeUtility.GetUtf8SequenceLength(this._value);
			}
		}

		// Token: 0x17000964 RID: 2404
		// (get) Token: 0x06002F23 RID: 12067 RVA: 0x0015F642 File Offset: 0x0015E842
		public int Value
		{
			get
			{
				return (int)this._value;
			}
		}

		// Token: 0x06002F24 RID: 12068 RVA: 0x0015F64C File Offset: 0x0015E84C
		private unsafe static Rune ChangeCaseCultureAware(Rune rune, TextInfo textInfo, bool toUpper)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)4], 2);
			Span<char> span2 = span;
			span = new Span<char>(stackalloc byte[(UIntPtr)4], 2);
			Span<char> destination = span;
			int length = rune.EncodeToUtf16(span2);
			span2 = span2.Slice(0, length);
			destination = destination.Slice(0, length);
			if (toUpper)
			{
				textInfo.ChangeCaseToUpper(span2, destination);
			}
			else
			{
				textInfo.ChangeCaseToLower(span2, destination);
			}
			if (rune.IsBmp)
			{
				return Rune.UnsafeCreate((uint)(*destination[0]));
			}
			return Rune.UnsafeCreate(UnicodeUtility.GetScalarFromUtf16SurrogatePair((uint)(*destination[0]), (uint)(*destination[1])));
		}

		// Token: 0x06002F25 RID: 12069 RVA: 0x0015F6E3 File Offset: 0x0015E8E3
		public int CompareTo(Rune other)
		{
			return this.Value - other.Value;
		}

		// Token: 0x06002F26 RID: 12070 RVA: 0x0015F6F4 File Offset: 0x0015E8F4
		public unsafe static OperationStatus DecodeFromUtf16(ReadOnlySpan<char> source, out Rune result, out int charsConsumed)
		{
			if (!source.IsEmpty)
			{
				char c = (char)(*source[0]);
				if (Rune.TryCreate(c, out result))
				{
					charsConsumed = 1;
					return OperationStatus.Done;
				}
				if (1 < source.Length)
				{
					char lowSurrogate = (char)(*source[1]);
					if (Rune.TryCreate(c, lowSurrogate, out result))
					{
						charsConsumed = 2;
						return OperationStatus.Done;
					}
				}
				else if (char.IsHighSurrogate(c))
				{
					goto IL_4C;
				}
				charsConsumed = 1;
				result = Rune.ReplacementChar;
				return OperationStatus.InvalidData;
			}
			IL_4C:
			charsConsumed = source.Length;
			result = Rune.ReplacementChar;
			return OperationStatus.NeedMoreData;
		}

		// Token: 0x06002F27 RID: 12071 RVA: 0x0015F774 File Offset: 0x0015E974
		public unsafe static OperationStatus DecodeFromUtf8(ReadOnlySpan<byte> source, out Rune result, out int bytesConsumed)
		{
			int num = 0;
			if (num < source.Length)
			{
				uint num2 = (uint)(*source[num]);
				if (!UnicodeUtility.IsAsciiCodePoint(num2))
				{
					if (UnicodeUtility.IsInRangeInclusive(num2, 194U, 244U))
					{
						num2 = num2 - 194U << 6;
						num++;
						if (num >= source.Length)
						{
							goto IL_163;
						}
						int num3 = (int)((sbyte)(*source[num]));
						if (num3 < -64)
						{
							num2 += (uint)num3;
							num2 += 128U;
							num2 += 128U;
							if (num2 < 2048U)
							{
								goto IL_21;
							}
							if (UnicodeUtility.IsInRangeInclusive(num2, 2080U, 3343U) && !UnicodeUtility.IsInRangeInclusive(num2, 2912U, 2943U) && !UnicodeUtility.IsInRangeInclusive(num2, 3072U, 3087U))
							{
								num++;
								if (num >= source.Length)
								{
									goto IL_163;
								}
								num3 = (int)((sbyte)(*source[num]));
								if (num3 < -64)
								{
									num2 <<= 6;
									num2 += (uint)num3;
									num2 += 128U;
									num2 -= 131072U;
									if (num2 <= 65535U)
									{
										goto IL_21;
									}
									num++;
									if (num >= source.Length)
									{
										goto IL_163;
									}
									num3 = (int)((sbyte)(*source[num]));
									if (num3 < -64)
									{
										num2 <<= 6;
										num2 += (uint)num3;
										num2 += 128U;
										num2 -= 4194304U;
										goto IL_21;
									}
								}
							}
						}
					}
					else
					{
						num = 1;
					}
					bytesConsumed = num;
					result = Rune.ReplacementChar;
					return OperationStatus.InvalidData;
				}
				IL_21:
				bytesConsumed = num + 1;
				result = Rune.UnsafeCreate(num2);
				return OperationStatus.Done;
			}
			IL_163:
			bytesConsumed = num;
			result = Rune.ReplacementChar;
			return OperationStatus.NeedMoreData;
		}

		// Token: 0x06002F28 RID: 12072 RVA: 0x0015F8F4 File Offset: 0x0015EAF4
		public unsafe static OperationStatus DecodeLastFromUtf16(ReadOnlySpan<char> source, out Rune result, out int charsConsumed)
		{
			int num = source.Length - 1;
			if (num < source.Length)
			{
				char c = (char)(*source[num]);
				if (Rune.TryCreate(c, out result))
				{
					charsConsumed = 1;
					return OperationStatus.Done;
				}
				if (char.IsLowSurrogate(c))
				{
					num--;
					if (num < source.Length)
					{
						char highSurrogate = (char)(*source[num]);
						if (Rune.TryCreate(highSurrogate, c, out result))
						{
							charsConsumed = 2;
							return OperationStatus.Done;
						}
					}
					charsConsumed = 1;
					result = Rune.ReplacementChar;
					return OperationStatus.InvalidData;
				}
			}
			charsConsumed = (int)((uint)(-(uint)source.Length) >> 31);
			result = Rune.ReplacementChar;
			return OperationStatus.NeedMoreData;
		}

		// Token: 0x06002F29 RID: 12073 RVA: 0x0015F988 File Offset: 0x0015EB88
		public unsafe static OperationStatus DecodeLastFromUtf8(ReadOnlySpan<byte> source, out Rune value, out int bytesConsumed)
		{
			int num = source.Length - 1;
			if (num >= source.Length)
			{
				value = Rune.ReplacementChar;
				bytesConsumed = 0;
				return OperationStatus.NeedMoreData;
			}
			uint num2 = (uint)(*source[num]);
			if (UnicodeUtility.IsAsciiCodePoint(num2))
			{
				bytesConsumed = 1;
				value = Rune.UnsafeCreate(num2);
				return OperationStatus.Done;
			}
			if (((byte)num2 & 64) != 0)
			{
				return Rune.DecodeFromUtf8(source.Slice(num), out value, out bytesConsumed);
			}
			int i = 3;
			while (i > 0)
			{
				num--;
				if (num >= source.Length)
				{
					break;
				}
				if ((sbyte)(*source[num]) < -64)
				{
					i--;
				}
				else
				{
					source = source.Slice(num);
					Rune rune;
					int num3;
					OperationStatus result = Rune.DecodeFromUtf8(source, out rune, out num3);
					if (num3 == source.Length)
					{
						bytesConsumed = num3;
						value = rune;
						return result;
					}
					break;
				}
			}
			value = Rune.ReplacementChar;
			bytesConsumed = 1;
			return OperationStatus.InvalidData;
		}

		// Token: 0x06002F2A RID: 12074 RVA: 0x0015FA60 File Offset: 0x0015EC60
		public int EncodeToUtf16(Span<char> destination)
		{
			int result;
			if (!this.TryEncodeToUtf16(destination, out result))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			return result;
		}

		// Token: 0x06002F2B RID: 12075 RVA: 0x0015FA80 File Offset: 0x0015EC80
		public int EncodeToUtf8(Span<byte> destination)
		{
			int result;
			if (!this.TryEncodeToUtf8(destination, out result))
			{
				ThrowHelper.ThrowArgumentException_DestinationTooShort();
			}
			return result;
		}

		// Token: 0x06002F2C RID: 12076 RVA: 0x0015FAA0 File Offset: 0x0015ECA0
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			if (obj is Rune)
			{
				Rune other = (Rune)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06002F2D RID: 12077 RVA: 0x0015FAC5 File Offset: 0x0015ECC5
		public bool Equals(Rune other)
		{
			return this == other;
		}

		// Token: 0x06002F2E RID: 12078 RVA: 0x0015FAD3 File Offset: 0x0015ECD3
		public override int GetHashCode()
		{
			return this.Value;
		}

		// Token: 0x06002F2F RID: 12079 RVA: 0x0015FADC File Offset: 0x0015ECDC
		[NullableContext(1)]
		public static Rune GetRuneAt(string input, int index)
		{
			int num = Rune.ReadRuneFromString(input, index);
			if (num < 0)
			{
				ThrowHelper.ThrowArgumentException_CannotExtractScalar(ExceptionArgument.index);
			}
			return Rune.UnsafeCreate((uint)num);
		}

		// Token: 0x06002F30 RID: 12080 RVA: 0x0015FB02 File Offset: 0x0015ED02
		public static bool IsValid(int value)
		{
			return Rune.IsValid((uint)value);
		}

		// Token: 0x06002F31 RID: 12081 RVA: 0x0015FB0A File Offset: 0x0015ED0A
		[CLSCompliant(false)]
		public static bool IsValid(uint value)
		{
			return UnicodeUtility.IsValidUnicodeScalar(value);
		}

		// Token: 0x06002F32 RID: 12082 RVA: 0x0015FB14 File Offset: 0x0015ED14
		internal unsafe static int ReadFirstRuneFromUtf16Buffer(ReadOnlySpan<char> input)
		{
			if (input.IsEmpty)
			{
				return -1;
			}
			uint num = (uint)(*input[0]);
			if (UnicodeUtility.IsSurrogateCodePoint(num))
			{
				if (!UnicodeUtility.IsHighSurrogateCodePoint(num))
				{
					return -1;
				}
				if (1 >= input.Length)
				{
					return -1;
				}
				uint num2 = (uint)(*input[1]);
				if (!UnicodeUtility.IsLowSurrogateCodePoint(num2))
				{
					return -1;
				}
				num = UnicodeUtility.GetScalarFromUtf16SurrogatePair(num, num2);
			}
			return (int)num;
		}

		// Token: 0x06002F33 RID: 12083 RVA: 0x0015FB74 File Offset: 0x0015ED74
		private static int ReadRuneFromString(string input, int index)
		{
			if (input == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.input);
			}
			if (index >= input.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRange_IndexException();
			}
			uint num = (uint)input[index];
			if (UnicodeUtility.IsSurrogateCodePoint(num))
			{
				if (!UnicodeUtility.IsHighSurrogateCodePoint(num))
				{
					return -1;
				}
				index++;
				if (index >= input.Length)
				{
					return -1;
				}
				uint num2 = (uint)input[index];
				if (!UnicodeUtility.IsLowSurrogateCodePoint(num2))
				{
					return -1;
				}
				num = UnicodeUtility.GetScalarFromUtf16SurrogatePair(num, num2);
			}
			return (int)num;
		}

		// Token: 0x06002F34 RID: 12084 RVA: 0x0015FBE0 File Offset: 0x0015EDE0
		[NullableContext(1)]
		public override string ToString()
		{
			if (this.IsBmp)
			{
				return string.CreateFromChar((char)this._value);
			}
			char c;
			char c2;
			UnicodeUtility.GetUtf16SurrogatesFromSupplementaryPlaneScalar(this._value, out c, out c2);
			return string.CreateFromChar(c, c2);
		}

		// Token: 0x06002F35 RID: 12085 RVA: 0x0015FC18 File Offset: 0x0015EE18
		public static bool TryCreate(char ch, out Rune result)
		{
			if (!UnicodeUtility.IsSurrogateCodePoint((uint)ch))
			{
				result = Rune.UnsafeCreate((uint)ch);
				return true;
			}
			result = default(Rune);
			return false;
		}

		// Token: 0x06002F36 RID: 12086 RVA: 0x0015FC48 File Offset: 0x0015EE48
		public static bool TryCreate(char highSurrogate, char lowSurrogate, out Rune result)
		{
			uint num = (uint)(highSurrogate - '\ud800');
			uint num2 = (uint)(lowSurrogate - '\udc00');
			if ((num | num2) <= 1023U)
			{
				result = Rune.UnsafeCreate((num << 10) + (uint)(lowSurrogate - '\udc00') + 65536U);
				return true;
			}
			result = default(Rune);
			return false;
		}

		// Token: 0x06002F37 RID: 12087 RVA: 0x0015FC96 File Offset: 0x0015EE96
		public static bool TryCreate(int value, out Rune result)
		{
			return Rune.TryCreate((uint)value, out result);
		}

		// Token: 0x06002F38 RID: 12088 RVA: 0x0015FC9F File Offset: 0x0015EE9F
		[CLSCompliant(false)]
		public static bool TryCreate(uint value, out Rune result)
		{
			if (UnicodeUtility.IsValidUnicodeScalar(value))
			{
				result = Rune.UnsafeCreate(value);
				return true;
			}
			result = default(Rune);
			return false;
		}

		// Token: 0x06002F39 RID: 12089 RVA: 0x0015FCC0 File Offset: 0x0015EEC0
		public unsafe bool TryEncodeToUtf16(Span<char> destination, out int charsWritten)
		{
			if (destination.Length >= 1)
			{
				if (this.IsBmp)
				{
					*destination[0] = (char)this._value;
					charsWritten = 1;
					return true;
				}
				if (destination.Length >= 2)
				{
					UnicodeUtility.GetUtf16SurrogatesFromSupplementaryPlaneScalar(this._value, destination[0], destination[1]);
					charsWritten = 2;
					return true;
				}
			}
			charsWritten = 0;
			return false;
		}

		// Token: 0x06002F3A RID: 12090 RVA: 0x0015FD24 File Offset: 0x0015EF24
		public unsafe bool TryEncodeToUtf8(Span<byte> destination, out int bytesWritten)
		{
			if (destination.Length >= 1)
			{
				if (this.IsAscii)
				{
					*destination[0] = (byte)this._value;
					bytesWritten = 1;
					return true;
				}
				if (destination.Length >= 2)
				{
					if (this._value <= 2047U)
					{
						*destination[0] = (byte)(this._value + 12288U >> 6);
						*destination[1] = (byte)((this._value & 63U) + 128U);
						bytesWritten = 2;
						return true;
					}
					if (destination.Length >= 3)
					{
						if (this._value <= 65535U)
						{
							*destination[0] = (byte)(this._value + 917504U >> 12);
							*destination[1] = (byte)(((this._value & 4032U) >> 6) + 128U);
							*destination[2] = (byte)((this._value & 63U) + 128U);
							bytesWritten = 3;
							return true;
						}
						if (destination.Length >= 4)
						{
							*destination[0] = (byte)(this._value + 62914560U >> 18);
							*destination[1] = (byte)(((this._value & 258048U) >> 12) + 128U);
							*destination[2] = (byte)(((this._value & 4032U) >> 6) + 128U);
							*destination[3] = (byte)((this._value & 63U) + 128U);
							bytesWritten = 4;
							return true;
						}
					}
				}
			}
			bytesWritten = 0;
			return false;
		}

		// Token: 0x06002F3B RID: 12091 RVA: 0x0015FE9C File Offset: 0x0015F09C
		[NullableContext(1)]
		public static bool TryGetRuneAt(string input, int index, out Rune value)
		{
			int num = Rune.ReadRuneFromString(input, index);
			if (num >= 0)
			{
				value = Rune.UnsafeCreate((uint)num);
				return true;
			}
			value = default(Rune);
			return false;
		}

		// Token: 0x06002F3C RID: 12092 RVA: 0x0015FECB File Offset: 0x0015F0CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static Rune UnsafeCreate(uint scalarValue)
		{
			return new Rune(scalarValue, false);
		}

		// Token: 0x06002F3D RID: 12093 RVA: 0x0015FED4 File Offset: 0x0015F0D4
		public static double GetNumericValue(Rune value)
		{
			if (!value.IsAscii)
			{
				return CharUnicodeInfo.GetNumericValue(value.Value);
			}
			uint num = value._value - 48U;
			if (num > 9U)
			{
				return -1.0;
			}
			return num;
		}

		// Token: 0x06002F3E RID: 12094 RVA: 0x0015FF14 File Offset: 0x0015F114
		public unsafe static UnicodeCategory GetUnicodeCategory(Rune value)
		{
			if (value.IsAscii)
			{
				return (UnicodeCategory)(*Rune.AsciiCharInfo[value.Value] & 31);
			}
			return Rune.GetUnicodeCategoryNonAscii(value);
		}

		// Token: 0x06002F3F RID: 12095 RVA: 0x0015FF49 File Offset: 0x0015F149
		private static UnicodeCategory GetUnicodeCategoryNonAscii(Rune value)
		{
			return CharUnicodeInfo.GetUnicodeCategory(value.Value);
		}

		// Token: 0x06002F40 RID: 12096 RVA: 0x0015FF57 File Offset: 0x0015F157
		private static bool IsCategoryLetter(UnicodeCategory category)
		{
			return UnicodeUtility.IsInRangeInclusive((uint)category, 0U, 4U);
		}

		// Token: 0x06002F41 RID: 12097 RVA: 0x0015FF61 File Offset: 0x0015F161
		private static bool IsCategoryLetterOrDecimalDigit(UnicodeCategory category)
		{
			return UnicodeUtility.IsInRangeInclusive((uint)category, 0U, 4U) || category == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06002F42 RID: 12098 RVA: 0x0015FF73 File Offset: 0x0015F173
		private static bool IsCategoryNumber(UnicodeCategory category)
		{
			return UnicodeUtility.IsInRangeInclusive((uint)category, 8U, 10U);
		}

		// Token: 0x06002F43 RID: 12099 RVA: 0x0015FF7E File Offset: 0x0015F17E
		private static bool IsCategoryPunctuation(UnicodeCategory category)
		{
			return UnicodeUtility.IsInRangeInclusive((uint)category, 18U, 24U);
		}

		// Token: 0x06002F44 RID: 12100 RVA: 0x0015FF8A File Offset: 0x0015F18A
		private static bool IsCategorySeparator(UnicodeCategory category)
		{
			return UnicodeUtility.IsInRangeInclusive((uint)category, 11U, 13U);
		}

		// Token: 0x06002F45 RID: 12101 RVA: 0x0015FF96 File Offset: 0x0015F196
		private static bool IsCategorySymbol(UnicodeCategory category)
		{
			return UnicodeUtility.IsInRangeInclusive((uint)category, 25U, 28U);
		}

		// Token: 0x06002F46 RID: 12102 RVA: 0x0015FFA2 File Offset: 0x0015F1A2
		public static bool IsControl(Rune value)
		{
			return (value._value + 1U & 4294967167U) <= 32U;
		}

		// Token: 0x06002F47 RID: 12103 RVA: 0x0015FFB9 File Offset: 0x0015F1B9
		public static bool IsDigit(Rune value)
		{
			if (value.IsAscii)
			{
				return UnicodeUtility.IsInRangeInclusive(value._value, 48U, 57U);
			}
			return Rune.GetUnicodeCategoryNonAscii(value) == UnicodeCategory.DecimalDigitNumber;
		}

		// Token: 0x06002F48 RID: 12104 RVA: 0x0015FFDD File Offset: 0x0015F1DD
		public static bool IsLetter(Rune value)
		{
			if (value.IsAscii)
			{
				return (value._value - 65U & 4294967263U) <= 25U;
			}
			return Rune.IsCategoryLetter(Rune.GetUnicodeCategoryNonAscii(value));
		}

		// Token: 0x06002F49 RID: 12105 RVA: 0x00160008 File Offset: 0x0015F208
		public unsafe static bool IsLetterOrDigit(Rune value)
		{
			if (value.IsAscii)
			{
				return (*Rune.AsciiCharInfo[value.Value] & 64) > 0;
			}
			return Rune.IsCategoryLetterOrDecimalDigit(Rune.GetUnicodeCategoryNonAscii(value));
		}

		// Token: 0x06002F4A RID: 12106 RVA: 0x00160045 File Offset: 0x0015F245
		public static bool IsLower(Rune value)
		{
			if (value.IsAscii)
			{
				return UnicodeUtility.IsInRangeInclusive(value._value, 97U, 122U);
			}
			return Rune.GetUnicodeCategoryNonAscii(value) == UnicodeCategory.LowercaseLetter;
		}

		// Token: 0x06002F4B RID: 12107 RVA: 0x00160069 File Offset: 0x0015F269
		public static bool IsNumber(Rune value)
		{
			if (value.IsAscii)
			{
				return UnicodeUtility.IsInRangeInclusive(value._value, 48U, 57U);
			}
			return Rune.IsCategoryNumber(Rune.GetUnicodeCategoryNonAscii(value));
		}

		// Token: 0x06002F4C RID: 12108 RVA: 0x0016008F File Offset: 0x0015F28F
		public static bool IsPunctuation(Rune value)
		{
			return Rune.IsCategoryPunctuation(Rune.GetUnicodeCategory(value));
		}

		// Token: 0x06002F4D RID: 12109 RVA: 0x0016009C File Offset: 0x0015F29C
		public static bool IsSeparator(Rune value)
		{
			return Rune.IsCategorySeparator(Rune.GetUnicodeCategory(value));
		}

		// Token: 0x06002F4E RID: 12110 RVA: 0x001600A9 File Offset: 0x0015F2A9
		public static bool IsSymbol(Rune value)
		{
			return Rune.IsCategorySymbol(Rune.GetUnicodeCategory(value));
		}

		// Token: 0x06002F4F RID: 12111 RVA: 0x001600B6 File Offset: 0x0015F2B6
		public static bool IsUpper(Rune value)
		{
			if (value.IsAscii)
			{
				return UnicodeUtility.IsInRangeInclusive(value._value, 65U, 90U);
			}
			return Rune.GetUnicodeCategoryNonAscii(value) == UnicodeCategory.UppercaseLetter;
		}

		// Token: 0x06002F50 RID: 12112 RVA: 0x001600DC File Offset: 0x0015F2DC
		public unsafe static bool IsWhiteSpace(Rune value)
		{
			if (value.IsAscii)
			{
				return (*Rune.AsciiCharInfo[value.Value] & 128) > 0;
			}
			return value.IsBmp && CharUnicodeInfo.GetIsWhiteSpace((char)value._value);
		}

		// Token: 0x06002F51 RID: 12113 RVA: 0x00160128 File Offset: 0x0015F328
		[NullableContext(1)]
		public static Rune ToLower(Rune value, CultureInfo culture)
		{
			if (culture == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
			}
			if (GlobalizationMode.Invariant)
			{
				return Rune.ToLowerInvariant(value);
			}
			return Rune.ChangeCaseCultureAware(value, culture.TextInfo, false);
		}

		// Token: 0x06002F52 RID: 12114 RVA: 0x0016014F File Offset: 0x0015F34F
		public static Rune ToLowerInvariant(Rune value)
		{
			if (value.IsAscii)
			{
				return Rune.UnsafeCreate(Utf16Utility.ConvertAllAsciiCharsInUInt32ToLowercase(value._value));
			}
			if (GlobalizationMode.Invariant)
			{
				return value;
			}
			return Rune.ChangeCaseCultureAware(value, TextInfo.Invariant, false);
		}

		// Token: 0x06002F53 RID: 12115 RVA: 0x00160180 File Offset: 0x0015F380
		[NullableContext(1)]
		public static Rune ToUpper(Rune value, CultureInfo culture)
		{
			if (culture == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.culture);
			}
			if (GlobalizationMode.Invariant)
			{
				return Rune.ToUpperInvariant(value);
			}
			return Rune.ChangeCaseCultureAware(value, culture.TextInfo, true);
		}

		// Token: 0x06002F54 RID: 12116 RVA: 0x001601A7 File Offset: 0x0015F3A7
		public static Rune ToUpperInvariant(Rune value)
		{
			if (value.IsAscii)
			{
				return Rune.UnsafeCreate(Utf16Utility.ConvertAllAsciiCharsInUInt32ToUppercase(value._value));
			}
			if (GlobalizationMode.Invariant)
			{
				return value;
			}
			return Rune.ChangeCaseCultureAware(value, TextInfo.Invariant, true);
		}

		// Token: 0x06002F55 RID: 12117 RVA: 0x001601D8 File Offset: 0x0015F3D8
		int IComparable.CompareTo(object obj)
		{
			if (obj == null)
			{
				return 1;
			}
			if (obj is Rune)
			{
				Rune other = (Rune)obj;
				return this.CompareTo(other);
			}
			throw new ArgumentException(SR.Arg_MustBeRune);
		}

		// Token: 0x04000CF3 RID: 3315
		private readonly uint _value;
	}
}
