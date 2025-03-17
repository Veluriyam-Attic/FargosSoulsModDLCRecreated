using System;
using System.Buffers.Binary;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000120 RID: 288
	[NonVersionable]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public struct Guid : IFormattable, IComparable, IComparable<Guid>, IEquatable<Guid>, ISpanFormattable
	{
		// Token: 0x06000ED0 RID: 3792 RVA: 0x000D77FB File Offset: 0x000D69FB
		[NullableContext(1)]
		public Guid(byte[] b)
		{
			if (b == null)
			{
				throw new ArgumentNullException("b");
			}
			this = new Guid(new ReadOnlySpan<byte>(b));
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x000D7818 File Offset: 0x000D6A18
		public unsafe Guid(ReadOnlySpan<byte> b)
		{
			if (b.Length != 16)
			{
				throw new ArgumentException(SR.Format(SR.Arg_GuidArrayCtor, "16"), "b");
			}
			if (BitConverter.IsLittleEndian)
			{
				this = MemoryMarshal.Read<Guid>(b);
				return;
			}
			this._k = *b[15];
			this._a = BinaryPrimitives.ReadInt32LittleEndian(b);
			this._b = BinaryPrimitives.ReadInt16LittleEndian(b.Slice(4));
			this._c = BinaryPrimitives.ReadInt16LittleEndian(b.Slice(8));
			this._d = *b[8];
			this._e = *b[9];
			this._f = *b[10];
			this._g = *b[11];
			this._h = *b[12];
			this._i = *b[13];
			this._j = *b[14];
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x000D7910 File Offset: 0x000D6B10
		[CLSCompliant(false)]
		public Guid(uint a, ushort b, ushort c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = (int)a;
			this._b = (short)b;
			this._c = (short)c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x000D7974 File Offset: 0x000D6B74
		[NullableContext(1)]
		public Guid(int a, short b, short c, byte[] d)
		{
			if (d == null)
			{
				throw new ArgumentNullException("d");
			}
			if (d.Length != 8)
			{
				throw new ArgumentException(SR.Format(SR.Arg_GuidArrayCtor, "8"), "d");
			}
			this._a = a;
			this._b = b;
			this._c = c;
			this._k = d[7];
			this._d = d[0];
			this._e = d[1];
			this._f = d[2];
			this._g = d[3];
			this._h = d[4];
			this._i = d[5];
			this._j = d[6];
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x000D7A18 File Offset: 0x000D6C18
		public Guid(int a, short b, short c, byte d, byte e, byte f, byte g, byte h, byte i, byte j, byte k)
		{
			this._a = a;
			this._b = b;
			this._c = c;
			this._d = d;
			this._e = e;
			this._f = f;
			this._g = g;
			this._h = h;
			this._i = i;
			this._j = j;
			this._k = k;
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x000D7A7C File Offset: 0x000D6C7C
		[NullableContext(1)]
		public Guid(string g)
		{
			if (g == null)
			{
				throw new ArgumentNullException("g");
			}
			Guid.GuidResult guidResult = new Guid.GuidResult(Guid.GuidParseThrowStyle.All);
			bool flag = Guid.TryParseGuid(g, ref guidResult);
			this = guidResult._parsedGuid;
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000D7ABC File Offset: 0x000D6CBC
		[NullableContext(1)]
		public static Guid Parse(string input)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Guid.Parse(input);
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000D7AE4 File Offset: 0x000D6CE4
		public static Guid Parse(ReadOnlySpan<char> input)
		{
			Guid.GuidResult guidResult = new Guid.GuidResult(Guid.GuidParseThrowStyle.AllButOverflow);
			bool flag = Guid.TryParseGuid(input, ref guidResult);
			return guidResult._parsedGuid;
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000D7B08 File Offset: 0x000D6D08
		[NullableContext(2)]
		public static bool TryParse([NotNullWhen(true)] string input, out Guid result)
		{
			if (input == null)
			{
				result = default(Guid);
				return false;
			}
			return Guid.TryParse(input, out result);
		}

		// Token: 0x06000ED9 RID: 3801 RVA: 0x000D7B24 File Offset: 0x000D6D24
		public static bool TryParse(ReadOnlySpan<char> input, out Guid result)
		{
			Guid.GuidResult guidResult = new Guid.GuidResult(Guid.GuidParseThrowStyle.None);
			if (Guid.TryParseGuid(input, ref guidResult))
			{
				result = guidResult._parsedGuid;
				return true;
			}
			result = default(Guid);
			return false;
		}

		// Token: 0x06000EDA RID: 3802 RVA: 0x000D7B5C File Offset: 0x000D6D5C
		[NullableContext(1)]
		public static Guid ParseExact(string input, string format)
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			ReadOnlySpan<char> input2 = input;
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			return Guid.ParseExact(input2, format);
		}

		// Token: 0x06000EDB RID: 3803 RVA: 0x000D7B98 File Offset: 0x000D6D98
		public unsafe static Guid ParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format)
		{
			if (format.Length != 1)
			{
				throw new FormatException(SR.Format_InvalidGuidFormatSpecification);
			}
			input = input.Trim();
			Guid.GuidResult guidResult = new Guid.GuidResult(Guid.GuidParseThrowStyle.AllButOverflow);
			char c = (char)(*format[0] | 32);
			if (c <= 'd')
			{
				if (c == 'b')
				{
					bool flag = Guid.TryParseExactB(input, ref guidResult);
					goto IL_97;
				}
				if (c == 'd')
				{
					bool flag = Guid.TryParseExactD(input, ref guidResult);
					goto IL_97;
				}
			}
			else
			{
				if (c == 'n')
				{
					bool flag = Guid.TryParseExactN(input, ref guidResult);
					goto IL_97;
				}
				if (c == 'p')
				{
					bool flag = Guid.TryParseExactP(input, ref guidResult);
					goto IL_97;
				}
				if (c == 'x')
				{
					bool flag = Guid.TryParseExactX(input, ref guidResult);
					goto IL_97;
				}
			}
			throw new FormatException(SR.Format_InvalidGuidFormatSpecification);
			IL_97:
			return guidResult._parsedGuid;
		}

		// Token: 0x06000EDC RID: 3804 RVA: 0x000D7C44 File Offset: 0x000D6E44
		[NullableContext(2)]
		public static bool TryParseExact([NotNullWhen(true)] string input, [NotNullWhen(true)] string format, out Guid result)
		{
			if (input == null)
			{
				result = default(Guid);
				return false;
			}
			return Guid.TryParseExact(input, format, out result);
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x000D7C64 File Offset: 0x000D6E64
		public unsafe static bool TryParseExact(ReadOnlySpan<char> input, ReadOnlySpan<char> format, out Guid result)
		{
			if (format.Length != 1)
			{
				result = default(Guid);
				return false;
			}
			input = input.Trim();
			Guid.GuidResult guidResult = new Guid.GuidResult(Guid.GuidParseThrowStyle.None);
			bool flag = false;
			char c = (char)(*format[0] | 32);
			if (c <= 'd')
			{
				if (c != 'b')
				{
					if (c == 'd')
					{
						flag = Guid.TryParseExactD(input, ref guidResult);
					}
				}
				else
				{
					flag = Guid.TryParseExactB(input, ref guidResult);
				}
			}
			else if (c != 'n')
			{
				if (c != 'p')
				{
					if (c == 'x')
					{
						flag = Guid.TryParseExactX(input, ref guidResult);
					}
				}
				else
				{
					flag = Guid.TryParseExactP(input, ref guidResult);
				}
			}
			else
			{
				flag = Guid.TryParseExactN(input, ref guidResult);
			}
			if (flag)
			{
				result = guidResult._parsedGuid;
				return true;
			}
			result = default(Guid);
			return false;
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000D7D14 File Offset: 0x000D6F14
		private unsafe static bool TryParseGuid(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			guidString = guidString.Trim();
			if (guidString.Length == 0)
			{
				result.SetFailure(false, "Format_GuidUnrecognized");
				return false;
			}
			char c = (char)(*guidString[0]);
			bool result2;
			if (c != '(')
			{
				if (c != '{')
				{
					result2 = (guidString.Contains('-') ? Guid.TryParseExactD(guidString, ref result) : Guid.TryParseExactN(guidString, ref result));
				}
				else
				{
					result2 = (guidString.Contains('-') ? Guid.TryParseExactB(guidString, ref result) : Guid.TryParseExactX(guidString, ref result));
				}
			}
			else
			{
				result2 = Guid.TryParseExactP(guidString, ref result);
			}
			return result2;
		}

		// Token: 0x06000EDF RID: 3807 RVA: 0x000D7D9C File Offset: 0x000D6F9C
		private unsafe static bool TryParseExactB(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			if (guidString.Length != 38 || *guidString[0] != 123 || *guidString[37] != 125)
			{
				result.SetFailure(false, "Format_GuidInvLen");
				return false;
			}
			return Guid.TryParseExactD(guidString.Slice(1, 36), ref result);
		}

		// Token: 0x06000EE0 RID: 3808 RVA: 0x000D7DF0 File Offset: 0x000D6FF0
		private unsafe static bool TryParseExactD(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			if (guidString.Length != 36)
			{
				result.SetFailure(false, "Format_GuidInvLen");
				return false;
			}
			if (*guidString[8] != 45 || *guidString[13] != 45 || *guidString[18] != 45 || *guidString[23] != 45)
			{
				result.SetFailure(false, "Format_GuidDashes");
				return false;
			}
			ref Guid ptr = ref result._parsedGuid;
			uint num;
			if (Guid.TryParseHex(guidString.Slice(0, 8), Unsafe.As<int, uint>(ref ptr._a)) && Guid.TryParseHex(guidString.Slice(9, 4), out num))
			{
				ptr._b = (short)num;
				if (Guid.TryParseHex(guidString.Slice(14, 4), out num))
				{
					ptr._c = (short)num;
					if (Guid.TryParseHex(guidString.Slice(19, 4), out num))
					{
						ptr._d = (byte)(num >> 8);
						ptr._e = (byte)num;
						if (Guid.TryParseHex(guidString.Slice(24, 4), out num))
						{
							ptr._f = (byte)(num >> 8);
							ptr._g = (byte)num;
							if (uint.TryParse(guidString.Slice(28, 8), NumberStyles.AllowHexSpecifier, null, out num))
							{
								ptr._h = (byte)(num >> 24);
								ptr._i = (byte)(num >> 16);
								ptr._j = (byte)(num >> 8);
								ptr._k = (byte)num;
								return true;
							}
						}
					}
				}
			}
			result.SetFailure(false, "Format_GuidInvalidChar");
			return false;
		}

		// Token: 0x06000EE1 RID: 3809 RVA: 0x000D7F58 File Offset: 0x000D7158
		private static bool TryParseExactN(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			if (guidString.Length != 32)
			{
				result.SetFailure(false, "Format_GuidInvLen");
				return false;
			}
			ref Guid ptr = ref result._parsedGuid;
			uint num;
			if (uint.TryParse(guidString.Slice(0, 8), NumberStyles.AllowHexSpecifier, null, Unsafe.As<int, uint>(ref ptr._a)) && uint.TryParse(guidString.Slice(8, 8), NumberStyles.AllowHexSpecifier, null, out num))
			{
				ptr._b = (short)(num >> 16);
				ptr._c = (short)num;
				if (uint.TryParse(guidString.Slice(16, 8), NumberStyles.AllowHexSpecifier, null, out num))
				{
					ptr._d = (byte)(num >> 24);
					ptr._e = (byte)(num >> 16);
					ptr._f = (byte)(num >> 8);
					ptr._g = (byte)num;
					if (uint.TryParse(guidString.Slice(24, 8), NumberStyles.AllowHexSpecifier, null, out num))
					{
						ptr._h = (byte)(num >> 24);
						ptr._i = (byte)(num >> 16);
						ptr._j = (byte)(num >> 8);
						ptr._k = (byte)num;
						return true;
					}
				}
			}
			result.SetFailure(false, "Format_GuidInvalidChar");
			return false;
		}

		// Token: 0x06000EE2 RID: 3810 RVA: 0x000D8068 File Offset: 0x000D7268
		private unsafe static bool TryParseExactP(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			if (guidString.Length != 38 || *guidString[0] != 40 || *guidString[37] != 41)
			{
				result.SetFailure(false, "Format_GuidInvLen");
				return false;
			}
			return Guid.TryParseExactD(guidString.Slice(1, 36), ref result);
		}

		// Token: 0x06000EE3 RID: 3811 RVA: 0x000D80BC File Offset: 0x000D72BC
		private unsafe static bool TryParseExactX(ReadOnlySpan<char> guidString, ref Guid.GuidResult result)
		{
			guidString = Guid.EatAllWhitespace(guidString);
			if (guidString.Length == 0 || *guidString[0] != 123)
			{
				result.SetFailure(false, "Format_GuidBrace");
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, 1))
			{
				result.SetFailure(false, "Format_GuidHexPrefix");
				return false;
			}
			int num = 3;
			int num2 = guidString.Slice(num).IndexOf(',');
			if (num2 <= 0)
			{
				result.SetFailure(false, "Format_GuidComma");
				return false;
			}
			bool flag = false;
			if (!Guid.TryParseHex(guidString.Slice(num, num2), Unsafe.As<int, uint>(ref result._parsedGuid._a), ref flag) || flag)
			{
				result.SetFailure(flag, flag ? "Overflow_UInt32" : "Format_GuidInvalidChar");
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(false, "Format_GuidHexPrefix");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.Slice(num).IndexOf(',');
			if (num2 <= 0)
			{
				result.SetFailure(false, "Format_GuidComma");
				return false;
			}
			if (!Guid.TryParseHex(guidString.Slice(num, num2), out result._parsedGuid._b, ref flag) || flag)
			{
				result.SetFailure(flag, flag ? "Overflow_UInt32" : "Format_GuidInvalidChar");
				return false;
			}
			if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
			{
				result.SetFailure(false, "Format_GuidHexPrefix");
				return false;
			}
			num = num + num2 + 3;
			num2 = guidString.Slice(num).IndexOf(',');
			if (num2 <= 0)
			{
				result.SetFailure(false, "Format_GuidComma");
				return false;
			}
			if (!Guid.TryParseHex(guidString.Slice(num, num2), out result._parsedGuid._c, ref flag) || flag)
			{
				result.SetFailure(flag, flag ? "Overflow_UInt32" : "Format_GuidInvalidChar");
				return false;
			}
			if (guidString.Length <= num + num2 + 1 || *guidString[num + num2 + 1] != 123)
			{
				result.SetFailure(false, "Format_GuidBrace");
				return false;
			}
			num2++;
			for (int i = 0; i < 8; i++)
			{
				if (!Guid.IsHexPrefix(guidString, num + num2 + 1))
				{
					result.SetFailure(false, "Format_GuidHexPrefix");
					return false;
				}
				num = num + num2 + 3;
				if (i < 7)
				{
					num2 = guidString.Slice(num).IndexOf(',');
					if (num2 <= 0)
					{
						result.SetFailure(false, "Format_GuidComma");
						return false;
					}
				}
				else
				{
					num2 = guidString.Slice(num).IndexOf('}');
					if (num2 <= 0)
					{
						result.SetFailure(false, "Format_GuidBraceAfterLastNumber");
						return false;
					}
				}
				uint num3;
				if (!Guid.TryParseHex(guidString.Slice(num, num2), out num3, ref flag) || flag || num3 > 255U)
				{
					result.SetFailure(flag, flag ? "Overflow_UInt32" : ((num3 > 255U) ? "Overflow_Byte" : "Format_GuidInvalidChar"));
					return false;
				}
				*Unsafe.Add<byte>(ref result._parsedGuid._d, i) = (byte)num3;
			}
			if (num + num2 + 1 >= guidString.Length || *guidString[num + num2 + 1] != 125)
			{
				result.SetFailure(false, "Format_GuidEndBrace");
				return false;
			}
			if (num + num2 + 1 != guidString.Length - 1)
			{
				result.SetFailure(false, "Format_ExtraJunkAtEnd");
				return false;
			}
			return true;
		}

		// Token: 0x06000EE4 RID: 3812 RVA: 0x000D83C4 File Offset: 0x000D75C4
		private static bool TryParseHex(ReadOnlySpan<char> guidString, out short result, ref bool overflow)
		{
			uint num;
			bool result2 = Guid.TryParseHex(guidString, out num, ref overflow);
			result = (short)num;
			return result2;
		}

		// Token: 0x06000EE5 RID: 3813 RVA: 0x000D83E0 File Offset: 0x000D75E0
		private static bool TryParseHex(ReadOnlySpan<char> guidString, out uint result)
		{
			bool flag = false;
			return Guid.TryParseHex(guidString, out result, ref flag);
		}

		// Token: 0x06000EE6 RID: 3814 RVA: 0x000D83F8 File Offset: 0x000D75F8
		private unsafe static bool TryParseHex(ReadOnlySpan<char> guidString, out uint result, ref bool overflow)
		{
			if (guidString.Length != 0)
			{
				if (*guidString[0] == 43)
				{
					guidString = guidString.Slice(1);
				}
				if (guidString.Length > 1 && *guidString[0] == 48 && (*guidString[1] | 32) == 120)
				{
					guidString = guidString.Slice(2);
				}
			}
			int i = 0;
			while (i < guidString.Length && *guidString[i] == 48)
			{
				i++;
			}
			int num = 0;
			uint num2 = 0U;
			while (i < guidString.Length)
			{
				char c = (char)(*guidString[i]);
				int num3 = HexConverter.FromChar((int)c);
				if (num3 == 255)
				{
					if (num > 8)
					{
						overflow = true;
					}
					result = 0U;
					return false;
				}
				num2 = num2 * 16U + (uint)num3;
				num++;
				i++;
			}
			if (num > 8)
			{
				overflow = true;
			}
			result = num2;
			return true;
		}

		// Token: 0x06000EE7 RID: 3815 RVA: 0x000D84C8 File Offset: 0x000D76C8
		private unsafe static ReadOnlySpan<char> EatAllWhitespace(ReadOnlySpan<char> str)
		{
			int i = 0;
			while (i < str.Length && !char.IsWhiteSpace((char)(*str[i])))
			{
				i++;
			}
			if (i == str.Length)
			{
				return str;
			}
			char[] array = new char[str.Length];
			int length = 0;
			if (i > 0)
			{
				length = i;
				str.Slice(0, i).CopyTo(array);
			}
			while (i < str.Length)
			{
				char c = (char)(*str[i]);
				if (!char.IsWhiteSpace(c))
				{
					array[length++] = c;
				}
				i++;
			}
			return new ReadOnlySpan<char>(array, 0, length);
		}

		// Token: 0x06000EE8 RID: 3816 RVA: 0x000D8564 File Offset: 0x000D7764
		private unsafe static bool IsHexPrefix(ReadOnlySpan<char> str, int i)
		{
			return i + 1 < str.Length && *str[i] == 48 && (*str[i + 1] | 32) == 120;
		}

		// Token: 0x06000EE9 RID: 3817 RVA: 0x000D8594 File Offset: 0x000D7794
		[NullableContext(1)]
		public byte[] ToByteArray()
		{
			byte[] array = new byte[16];
			if (BitConverter.IsLittleEndian)
			{
				MemoryMarshal.TryWrite<Guid>(array, ref this);
			}
			else
			{
				this.TryWriteBytes(array);
			}
			return array;
		}

		// Token: 0x06000EEA RID: 3818 RVA: 0x000D85D0 File Offset: 0x000D77D0
		public unsafe bool TryWriteBytes(Span<byte> destination)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.TryWrite<Guid>(destination, ref this);
			}
			if (destination.Length < 16)
			{
				return false;
			}
			*destination[15] = this._k;
			BinaryPrimitives.WriteInt32LittleEndian(destination, this._a);
			BinaryPrimitives.WriteInt16LittleEndian(destination.Slice(4), this._b);
			BinaryPrimitives.WriteInt16LittleEndian(destination.Slice(8), this._c);
			*destination[8] = this._d;
			*destination[9] = this._e;
			*destination[10] = this._f;
			*destination[11] = this._g;
			*destination[12] = this._h;
			*destination[13] = this._i;
			*destination[14] = this._j;
			return true;
		}

		// Token: 0x06000EEB RID: 3819 RVA: 0x000D86AB File Offset: 0x000D78AB
		[NullableContext(1)]
		public override string ToString()
		{
			return this.ToString("D", null);
		}

		// Token: 0x06000EEC RID: 3820 RVA: 0x000D86B9 File Offset: 0x000D78B9
		public unsafe override int GetHashCode()
		{
			return this._a ^ *Unsafe.Add<int>(ref this._a, 1) ^ *Unsafe.Add<int>(ref this._a, 2) ^ *Unsafe.Add<int>(ref this._a, 3);
		}

		// Token: 0x06000EED RID: 3821 RVA: 0x000D86EC File Offset: 0x000D78EC
		[NullableContext(2)]
		public unsafe override bool Equals(object o)
		{
			if (o == null || !(o is Guid))
			{
				return false;
			}
			Guid guid = (Guid)o;
			return guid._a == this._a && *Unsafe.Add<int>(ref guid._a, 1) == *Unsafe.Add<int>(ref this._a, 1) && *Unsafe.Add<int>(ref guid._a, 2) == *Unsafe.Add<int>(ref this._a, 2) && *Unsafe.Add<int>(ref guid._a, 3) == *Unsafe.Add<int>(ref this._a, 3);
		}

		// Token: 0x06000EEE RID: 3822 RVA: 0x000D8774 File Offset: 0x000D7974
		public unsafe bool Equals(Guid g)
		{
			return g._a == this._a && *Unsafe.Add<int>(ref g._a, 1) == *Unsafe.Add<int>(ref this._a, 1) && *Unsafe.Add<int>(ref g._a, 2) == *Unsafe.Add<int>(ref this._a, 2) && *Unsafe.Add<int>(ref g._a, 3) == *Unsafe.Add<int>(ref this._a, 3);
		}

		// Token: 0x06000EEF RID: 3823 RVA: 0x000D87E8 File Offset: 0x000D79E8
		private int GetResult(uint me, uint them)
		{
			if (me >= them)
			{
				return 1;
			}
			return -1;
		}

		// Token: 0x06000EF0 RID: 3824 RVA: 0x000D87F4 File Offset: 0x000D79F4
		[NullableContext(2)]
		public int CompareTo(object value)
		{
			if (value == null)
			{
				return 1;
			}
			if (!(value is Guid))
			{
				throw new ArgumentException(SR.Arg_MustBeGuid, "value");
			}
			Guid guid = (Guid)value;
			if (guid._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)guid._a);
			}
			if (guid._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)guid._b);
			}
			if (guid._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)guid._c);
			}
			if (guid._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)guid._d);
			}
			if (guid._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)guid._e);
			}
			if (guid._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)guid._f);
			}
			if (guid._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)guid._g);
			}
			if (guid._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)guid._h);
			}
			if (guid._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)guid._i);
			}
			if (guid._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)guid._j);
			}
			if (guid._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)guid._k);
			}
			return 0;
		}

		// Token: 0x06000EF1 RID: 3825 RVA: 0x000D8994 File Offset: 0x000D7B94
		public int CompareTo(Guid value)
		{
			if (value._a != this._a)
			{
				return this.GetResult((uint)this._a, (uint)value._a);
			}
			if (value._b != this._b)
			{
				return this.GetResult((uint)this._b, (uint)value._b);
			}
			if (value._c != this._c)
			{
				return this.GetResult((uint)this._c, (uint)value._c);
			}
			if (value._d != this._d)
			{
				return this.GetResult((uint)this._d, (uint)value._d);
			}
			if (value._e != this._e)
			{
				return this.GetResult((uint)this._e, (uint)value._e);
			}
			if (value._f != this._f)
			{
				return this.GetResult((uint)this._f, (uint)value._f);
			}
			if (value._g != this._g)
			{
				return this.GetResult((uint)this._g, (uint)value._g);
			}
			if (value._h != this._h)
			{
				return this.GetResult((uint)this._h, (uint)value._h);
			}
			if (value._i != this._i)
			{
				return this.GetResult((uint)this._i, (uint)value._i);
			}
			if (value._j != this._j)
			{
				return this.GetResult((uint)this._j, (uint)value._j);
			}
			if (value._k != this._k)
			{
				return this.GetResult((uint)this._k, (uint)value._k);
			}
			return 0;
		}

		// Token: 0x06000EF2 RID: 3826 RVA: 0x000D8B10 File Offset: 0x000D7D10
		public unsafe static bool operator ==(Guid a, Guid b)
		{
			return a._a == b._a && *Unsafe.Add<int>(ref a._a, 1) == *Unsafe.Add<int>(ref b._a, 1) && *Unsafe.Add<int>(ref a._a, 2) == *Unsafe.Add<int>(ref b._a, 2) && *Unsafe.Add<int>(ref a._a, 3) == *Unsafe.Add<int>(ref b._a, 3);
		}

		// Token: 0x06000EF3 RID: 3827 RVA: 0x000D8B88 File Offset: 0x000D7D88
		public unsafe static bool operator !=(Guid a, Guid b)
		{
			return a._a != b._a || *Unsafe.Add<int>(ref a._a, 1) != *Unsafe.Add<int>(ref b._a, 1) || *Unsafe.Add<int>(ref a._a, 2) != *Unsafe.Add<int>(ref b._a, 2) || *Unsafe.Add<int>(ref a._a, 3) != *Unsafe.Add<int>(ref b._a, 3);
		}

		// Token: 0x06000EF4 RID: 3828 RVA: 0x000D8C02 File Offset: 0x000D7E02
		[NullableContext(1)]
		public string ToString([Nullable(2)] string format)
		{
			return this.ToString(format, null);
		}

		// Token: 0x06000EF5 RID: 3829 RVA: 0x000D8C0C File Offset: 0x000D7E0C
		private unsafe static int HexsToChars(char* guidChars, int a, int b)
		{
			*guidChars = HexConverter.ToCharLower(a >> 4);
			guidChars[1] = HexConverter.ToCharLower(a);
			guidChars[2] = HexConverter.ToCharLower(b >> 4);
			guidChars[3] = HexConverter.ToCharLower(b);
			return 4;
		}

		// Token: 0x06000EF6 RID: 3830 RVA: 0x000D8C40 File Offset: 0x000D7E40
		private unsafe static int HexsToCharsHexOutput(char* guidChars, int a, int b)
		{
			*guidChars = '0';
			guidChars[1] = 'x';
			guidChars[2] = HexConverter.ToCharLower(a >> 4);
			guidChars[3] = HexConverter.ToCharLower(a);
			guidChars[4] = ',';
			guidChars[5] = '0';
			guidChars[6] = 'x';
			guidChars[7] = HexConverter.ToCharLower(b >> 4);
			guidChars[8] = HexConverter.ToCharLower(b);
			return 9;
		}

		// Token: 0x06000EF7 RID: 3831 RVA: 0x000D8CAC File Offset: 0x000D7EAC
		[NullableContext(2)]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			if (string.IsNullOrEmpty(format))
			{
				format = "D";
			}
			if (format.Length != 1)
			{
				throw new FormatException(SR.Format_InvalidGuidFormatSpecification);
			}
			char c = format[0];
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_8C;
					}
					if (c != 'D')
					{
						goto IL_96;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_87;
					}
					if (c == 'P')
					{
						goto IL_8C;
					}
					if (c != 'X')
					{
						goto IL_96;
					}
					goto IL_91;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_8C;
				}
				if (c != 'd')
				{
					goto IL_96;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_87;
				}
				if (c == 'p')
				{
					goto IL_8C;
				}
				if (c != 'x')
				{
					goto IL_96;
				}
				goto IL_91;
			}
			int length = 36;
			goto IL_A1;
			IL_87:
			length = 32;
			goto IL_A1;
			IL_8C:
			length = 38;
			goto IL_A1;
			IL_91:
			length = 68;
			goto IL_A1;
			IL_96:
			throw new FormatException(SR.Format_InvalidGuidFormatSpecification);
			IL_A1:
			string text = string.FastAllocateString(length);
			int num;
			bool flag = this.TryFormat(new Span<char>(text.GetRawStringData(), text.Length), out num, format);
			return text;
		}

		// Token: 0x06000EF8 RID: 3832 RVA: 0x000D8D84 File Offset: 0x000D7F84
		public unsafe bool TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format = default(ReadOnlySpan<char>))
		{
			if (format.Length == 0)
			{
				format = "D";
			}
			if (format.Length != 1)
			{
				throw new FormatException(SR.Format_InvalidGuidFormatSpecification);
			}
			bool flag = true;
			bool flag2 = false;
			int num = 0;
			char c = (char)(*format[0]);
			if (c <= 'X')
			{
				if (c <= 'D')
				{
					if (c == 'B')
					{
						goto IL_9D;
					}
					if (c != 'D')
					{
						goto IL_C2;
					}
				}
				else
				{
					if (c == 'N')
					{
						goto IL_96;
					}
					if (c == 'P')
					{
						goto IL_A8;
					}
					if (c != 'X')
					{
						goto IL_C2;
					}
					goto IL_B3;
				}
			}
			else if (c <= 'd')
			{
				if (c == 'b')
				{
					goto IL_9D;
				}
				if (c != 'd')
				{
					goto IL_C2;
				}
			}
			else
			{
				if (c == 'n')
				{
					goto IL_96;
				}
				if (c == 'p')
				{
					goto IL_A8;
				}
				if (c != 'x')
				{
					goto IL_C2;
				}
				goto IL_B3;
			}
			int num2 = 36;
			goto IL_CD;
			IL_96:
			flag = false;
			num2 = 32;
			goto IL_CD;
			IL_9D:
			num = 8192123;
			num2 = 38;
			goto IL_CD;
			IL_A8:
			num = 2687016;
			num2 = 38;
			goto IL_CD;
			IL_B3:
			num = 8192123;
			flag = false;
			flag2 = true;
			num2 = 68;
			goto IL_CD;
			IL_C2:
			throw new FormatException(SR.Format_InvalidGuidFormatSpecification);
			IL_CD:
			if (destination.Length < num2)
			{
				charsWritten = 0;
				return false;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(destination))
			{
				char* ptr = reference;
				char* ptr2 = ptr;
				if (num != 0)
				{
					*(ptr2++) = (char)num;
				}
				if (flag2)
				{
					*(ptr2++) = '0';
					*(ptr2++) = 'x';
					ptr2 += Guid.HexsToChars(ptr2, this._a >> 24, this._a >> 16);
					ptr2 += Guid.HexsToChars(ptr2, this._a >> 8, this._a);
					*(ptr2++) = ',';
					*(ptr2++) = '0';
					*(ptr2++) = 'x';
					ptr2 += Guid.HexsToChars(ptr2, this._b >> 8, (int)this._b);
					*(ptr2++) = ',';
					*(ptr2++) = '0';
					*(ptr2++) = 'x';
					ptr2 += Guid.HexsToChars(ptr2, this._c >> 8, (int)this._c);
					*(ptr2++) = ',';
					*(ptr2++) = '{';
					ptr2 += Guid.HexsToCharsHexOutput(ptr2, (int)this._d, (int)this._e);
					*(ptr2++) = ',';
					ptr2 += Guid.HexsToCharsHexOutput(ptr2, (int)this._f, (int)this._g);
					*(ptr2++) = ',';
					ptr2 += Guid.HexsToCharsHexOutput(ptr2, (int)this._h, (int)this._i);
					*(ptr2++) = ',';
					ptr2 += Guid.HexsToCharsHexOutput(ptr2, (int)this._j, (int)this._k);
					*(ptr2++) = '}';
				}
				else
				{
					ptr2 += Guid.HexsToChars(ptr2, this._a >> 24, this._a >> 16);
					ptr2 += Guid.HexsToChars(ptr2, this._a >> 8, this._a);
					if (flag)
					{
						*(ptr2++) = '-';
					}
					ptr2 += Guid.HexsToChars(ptr2, this._b >> 8, (int)this._b);
					if (flag)
					{
						*(ptr2++) = '-';
					}
					ptr2 += Guid.HexsToChars(ptr2, this._c >> 8, (int)this._c);
					if (flag)
					{
						*(ptr2++) = '-';
					}
					ptr2 += Guid.HexsToChars(ptr2, (int)this._d, (int)this._e);
					if (flag)
					{
						*(ptr2++) = '-';
					}
					ptr2 += Guid.HexsToChars(ptr2, (int)this._f, (int)this._g);
					ptr2 += Guid.HexsToChars(ptr2, (int)this._h, (int)this._i);
					ptr2 += Guid.HexsToChars(ptr2, (int)this._j, (int)this._k);
				}
				if (num != 0)
				{
					*(ptr2++) = (char)(num >> 16);
				}
			}
			charsWritten = num2;
			return true;
		}

		// Token: 0x06000EF9 RID: 3833 RVA: 0x000D9136 File Offset: 0x000D8336
		bool ISpanFormattable.TryFormat(Span<char> destination, out int charsWritten, ReadOnlySpan<char> format, IFormatProvider provider)
		{
			return this.TryFormat(destination, out charsWritten, format);
		}

		// Token: 0x06000EFA RID: 3834 RVA: 0x000D9144 File Offset: 0x000D8344
		public static Guid NewGuid()
		{
			Guid result;
			int num = Interop.Ole32.CoCreateGuid(out result);
			if (num != 0)
			{
				throw new Exception
				{
					HResult = num
				};
			}
			return result;
		}

		// Token: 0x040003CB RID: 971
		public static readonly Guid Empty;

		// Token: 0x040003CC RID: 972
		private int _a;

		// Token: 0x040003CD RID: 973
		private short _b;

		// Token: 0x040003CE RID: 974
		private short _c;

		// Token: 0x040003CF RID: 975
		private byte _d;

		// Token: 0x040003D0 RID: 976
		private byte _e;

		// Token: 0x040003D1 RID: 977
		private byte _f;

		// Token: 0x040003D2 RID: 978
		private byte _g;

		// Token: 0x040003D3 RID: 979
		private byte _h;

		// Token: 0x040003D4 RID: 980
		private byte _i;

		// Token: 0x040003D5 RID: 981
		private byte _j;

		// Token: 0x040003D6 RID: 982
		private byte _k;

		// Token: 0x02000121 RID: 289
		private enum GuidParseThrowStyle : byte
		{
			// Token: 0x040003D8 RID: 984
			None,
			// Token: 0x040003D9 RID: 985
			All,
			// Token: 0x040003DA RID: 986
			AllButOverflow
		}

		// Token: 0x02000122 RID: 290
		private struct GuidResult
		{
			// Token: 0x06000EFB RID: 3835 RVA: 0x000D916C File Offset: 0x000D836C
			internal GuidResult(Guid.GuidParseThrowStyle canThrow)
			{
				this = default(Guid.GuidResult);
				this._throwStyle = canThrow;
			}

			// Token: 0x06000EFC RID: 3836 RVA: 0x000D917C File Offset: 0x000D837C
			internal void SetFailure(bool overflow, string failureMessageID)
			{
				if (this._throwStyle == Guid.GuidParseThrowStyle.None)
				{
					return;
				}
				if (!overflow)
				{
					throw new FormatException(SR.GetResourceString(failureMessageID));
				}
				if (this._throwStyle == Guid.GuidParseThrowStyle.All)
				{
					throw new OverflowException(SR.GetResourceString(failureMessageID));
				}
				throw new FormatException(SR.Format_GuidUnrecognized);
			}

			// Token: 0x040003DB RID: 987
			private readonly Guid.GuidParseThrowStyle _throwStyle;

			// Token: 0x040003DC RID: 988
			internal Guid _parsedGuid;
		}
	}
}
