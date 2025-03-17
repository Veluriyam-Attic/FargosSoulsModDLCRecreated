using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000E0 RID: 224
	[NullableContext(2)]
	[Nullable(0)]
	public static class Convert
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x000CAD80 File Offset: 0x000C9F80
		private unsafe static bool TryDecodeFromUtf16(ReadOnlySpan<char> utf16, Span<byte> bytes, out int consumed, out int written)
		{
			ref char reference = ref MemoryMarshal.GetReference<char>(utf16);
			ref byte reference2 = ref MemoryMarshal.GetReference<byte>(bytes);
			int num = utf16.Length & -4;
			int length = bytes.Length;
			int i = 0;
			int num2 = 0;
			if (utf16.Length != 0)
			{
				ref sbyte reference3 = ref MemoryMarshal.GetReference<sbyte>(Convert.DecodingMap);
				int num3;
				if (length >= (num >> 2) * 3)
				{
					num3 = num - 4;
				}
				else
				{
					num3 = length / 3 * 4;
				}
				while (i < num3)
				{
					int num4 = Convert.Decode(Unsafe.Add<char>(ref reference, i), ref reference3);
					if (num4 < 0)
					{
						IL_200:
						consumed = i;
						written = num2;
						return false;
					}
					Convert.WriteThreeLowOrderBytes(Unsafe.Add<byte>(ref reference2, num2), num4);
					num2 += 3;
					i += 4;
				}
				if (num3 != num - 4 || i == num)
				{
					goto IL_200;
				}
				int num5 = (int)(*Unsafe.Add<char>(ref reference, num - 4));
				int num6 = (int)(*Unsafe.Add<char>(ref reference, num - 3));
				int num7 = (int)(*Unsafe.Add<char>(ref reference, num - 2));
				int num8 = (int)(*Unsafe.Add<char>(ref reference, num - 1));
				if (((long)(num5 | num6 | num7 | num8) & (long)((ulong)-256)) != 0L)
				{
					goto IL_200;
				}
				num5 = (int)(*Unsafe.Add<sbyte>(ref reference3, num5));
				num6 = (int)(*Unsafe.Add<sbyte>(ref reference3, num6));
				num5 <<= 18;
				num6 <<= 12;
				num5 |= num6;
				if (num8 != 61)
				{
					num7 = (int)(*Unsafe.Add<sbyte>(ref reference3, num7));
					num8 = (int)(*Unsafe.Add<sbyte>(ref reference3, num8));
					num7 <<= 6;
					num5 |= num8;
					num5 |= num7;
					if (num5 < 0 || num2 > length - 3)
					{
						goto IL_200;
					}
					Convert.WriteThreeLowOrderBytes(Unsafe.Add<byte>(ref reference2, num2), num5);
					num2 += 3;
				}
				else if (num7 != 61)
				{
					num7 = (int)(*Unsafe.Add<sbyte>(ref reference3, num7));
					num7 <<= 6;
					num5 |= num7;
					if (num5 < 0 || num2 > length - 2)
					{
						goto IL_200;
					}
					*Unsafe.Add<byte>(ref reference2, num2) = (byte)(num5 >> 16);
					*Unsafe.Add<byte>(ref reference2, num2 + 1) = (byte)(num5 >> 8);
					num2 += 2;
				}
				else
				{
					if (num5 < 0 || num2 > length - 1)
					{
						goto IL_200;
					}
					*Unsafe.Add<byte>(ref reference2, num2) = (byte)(num5 >> 16);
					num2++;
				}
				i += 4;
				if (num != utf16.Length)
				{
					goto IL_200;
				}
			}
			consumed = i;
			written = num2;
			return true;
		}

		// Token: 0x06000B81 RID: 2945 RVA: 0x000CAF98 File Offset: 0x000CA198
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static int Decode(ref char encodedChars, ref sbyte decodingMap)
		{
			int num = (int)encodedChars;
			int num2 = (int)(*Unsafe.Add<char>(ref encodedChars, 1));
			int num3 = (int)(*Unsafe.Add<char>(ref encodedChars, 2));
			int num4 = (int)(*Unsafe.Add<char>(ref encodedChars, 3));
			if (((long)(num | num2 | num3 | num4) & (long)((ulong)-256)) != 0L)
			{
				return -1;
			}
			num = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num));
			num2 = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num2));
			num3 = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num3));
			num4 = (int)(*Unsafe.Add<sbyte>(ref decodingMap, num4));
			num <<= 18;
			num2 <<= 12;
			num3 <<= 6;
			num |= num4;
			num2 |= num3;
			return num | num2;
		}

		// Token: 0x06000B82 RID: 2946 RVA: 0x000CB015 File Offset: 0x000CA215
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteThreeLowOrderBytes(ref byte destination, int value)
		{
			destination = (byte)(value >> 16);
			*Unsafe.Add<byte>(ref destination, 1) = (byte)(value >> 8);
			*Unsafe.Add<byte>(ref destination, 2) = (byte)value;
		}

		// Token: 0x17000122 RID: 290
		// (get) Token: 0x06000B83 RID: 2947 RVA: 0x000CB034 File Offset: 0x000CA234
		[Nullable(0)]
		private unsafe static ReadOnlySpan<sbyte> DecodingMap
		{
			get
			{
				return new ReadOnlySpan<sbyte>((void*)(&<PrivateImplementationDetails>.F2830F044682E33B39018B5912634835B641562914E192CA66C654F5E4492FA8), 256);
			}
		}

		// Token: 0x06000B84 RID: 2948 RVA: 0x000CB048 File Offset: 0x000CA248
		public static TypeCode GetTypeCode(object value)
		{
			if (value == null)
			{
				return TypeCode.Empty;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.GetTypeCode();
			}
			return TypeCode.Object;
		}

		// Token: 0x06000B85 RID: 2949 RVA: 0x000CB06C File Offset: 0x000CA26C
		public static bool IsDBNull([NotNullWhen(true)] object value)
		{
			if (value == System.DBNull.Value)
			{
				return true;
			}
			IConvertible convertible = value as IConvertible;
			return convertible != null && convertible.GetTypeCode() == TypeCode.DBNull;
		}

		// Token: 0x06000B86 RID: 2950 RVA: 0x000CB098 File Offset: 0x000CA298
		[return: NotNullIfNotNull("value")]
		public static object ChangeType(object value, TypeCode typeCode)
		{
			return Convert.ChangeType(value, typeCode, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000B87 RID: 2951 RVA: 0x000CB0A8 File Offset: 0x000CA2A8
		[return: NotNullIfNotNull("value")]
		public static object ChangeType(object value, TypeCode typeCode, IFormatProvider provider)
		{
			if (value == null && (typeCode == TypeCode.Empty || typeCode == TypeCode.String || typeCode == TypeCode.Object))
			{
				return null;
			}
			IConvertible convertible = value as IConvertible;
			if (convertible == null)
			{
				throw new InvalidCastException(SR.InvalidCast_IConvertible);
			}
			switch (typeCode)
			{
			case TypeCode.Empty:
				throw new InvalidCastException(SR.InvalidCast_Empty);
			case TypeCode.Object:
				return value;
			case TypeCode.DBNull:
				throw new InvalidCastException(SR.InvalidCast_DBNull);
			case TypeCode.Boolean:
				return convertible.ToBoolean(provider);
			case TypeCode.Char:
				return convertible.ToChar(provider);
			case TypeCode.SByte:
				return convertible.ToSByte(provider);
			case TypeCode.Byte:
				return convertible.ToByte(provider);
			case TypeCode.Int16:
				return convertible.ToInt16(provider);
			case TypeCode.UInt16:
				return convertible.ToUInt16(provider);
			case TypeCode.Int32:
				return convertible.ToInt32(provider);
			case TypeCode.UInt32:
				return convertible.ToUInt32(provider);
			case TypeCode.Int64:
				return convertible.ToInt64(provider);
			case TypeCode.UInt64:
				return convertible.ToUInt64(provider);
			case TypeCode.Single:
				return convertible.ToSingle(provider);
			case TypeCode.Double:
				return convertible.ToDouble(provider);
			case TypeCode.Decimal:
				return convertible.ToDecimal(provider);
			case TypeCode.DateTime:
				return convertible.ToDateTime(provider);
			case TypeCode.String:
				return convertible.ToString(provider);
			}
			throw new ArgumentException(SR.Arg_UnknownTypeCode);
		}

		// Token: 0x06000B88 RID: 2952 RVA: 0x000CB24C File Offset: 0x000CA44C
		internal static object DefaultToType(IConvertible value, Type targetType, IFormatProvider provider)
		{
			if (targetType == null)
			{
				throw new ArgumentNullException("targetType");
			}
			if (value.GetType() == targetType)
			{
				return value;
			}
			if (targetType == Convert.ConvertTypes[3])
			{
				return value.ToBoolean(provider);
			}
			if (targetType == Convert.ConvertTypes[4])
			{
				return value.ToChar(provider);
			}
			if (targetType == Convert.ConvertTypes[5])
			{
				return value.ToSByte(provider);
			}
			if (targetType == Convert.ConvertTypes[6])
			{
				return value.ToByte(provider);
			}
			if (targetType == Convert.ConvertTypes[7])
			{
				return value.ToInt16(provider);
			}
			if (targetType == Convert.ConvertTypes[8])
			{
				return value.ToUInt16(provider);
			}
			if (targetType == Convert.ConvertTypes[9])
			{
				return value.ToInt32(provider);
			}
			if (targetType == Convert.ConvertTypes[10])
			{
				return value.ToUInt32(provider);
			}
			if (targetType == Convert.ConvertTypes[11])
			{
				return value.ToInt64(provider);
			}
			if (targetType == Convert.ConvertTypes[12])
			{
				return value.ToUInt64(provider);
			}
			if (targetType == Convert.ConvertTypes[13])
			{
				return value.ToSingle(provider);
			}
			if (targetType == Convert.ConvertTypes[14])
			{
				return value.ToDouble(provider);
			}
			if (targetType == Convert.ConvertTypes[15])
			{
				return value.ToDecimal(provider);
			}
			if (targetType == Convert.ConvertTypes[16])
			{
				return value.ToDateTime(provider);
			}
			if (targetType == Convert.ConvertTypes[18])
			{
				return value.ToString(provider);
			}
			if (targetType == Convert.ConvertTypes[1])
			{
				return value;
			}
			if (targetType == Convert.EnumType)
			{
				return (Enum)value;
			}
			if (targetType == Convert.ConvertTypes[2])
			{
				throw new InvalidCastException(SR.InvalidCast_DBNull);
			}
			if (targetType == Convert.ConvertTypes[0])
			{
				throw new InvalidCastException(SR.InvalidCast_Empty);
			}
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, value.GetType().FullName, targetType.FullName));
		}

		// Token: 0x06000B89 RID: 2953 RVA: 0x000CB43A File Offset: 0x000CA63A
		[return: NotNullIfNotNull("value")]
		public static object ChangeType(object value, [Nullable(1)] Type conversionType)
		{
			return Convert.ChangeType(value, conversionType, CultureInfo.CurrentCulture);
		}

		// Token: 0x06000B8A RID: 2954 RVA: 0x000CB448 File Offset: 0x000CA648
		[return: NotNullIfNotNull("value")]
		public static object ChangeType(object value, [Nullable(1)] Type conversionType, IFormatProvider provider)
		{
			if (conversionType == null)
			{
				throw new ArgumentNullException("conversionType");
			}
			if (value == null)
			{
				if (conversionType.IsValueType)
				{
					throw new InvalidCastException(SR.InvalidCast_CannotCastNullToValueType);
				}
				return null;
			}
			else
			{
				IConvertible convertible = value as IConvertible;
				if (convertible == null)
				{
					if (value.GetType() == conversionType)
					{
						return value;
					}
					throw new InvalidCastException(SR.InvalidCast_IConvertible);
				}
				else
				{
					if (conversionType == Convert.ConvertTypes[3])
					{
						return convertible.ToBoolean(provider);
					}
					if (conversionType == Convert.ConvertTypes[4])
					{
						return convertible.ToChar(provider);
					}
					if (conversionType == Convert.ConvertTypes[5])
					{
						return convertible.ToSByte(provider);
					}
					if (conversionType == Convert.ConvertTypes[6])
					{
						return convertible.ToByte(provider);
					}
					if (conversionType == Convert.ConvertTypes[7])
					{
						return convertible.ToInt16(provider);
					}
					if (conversionType == Convert.ConvertTypes[8])
					{
						return convertible.ToUInt16(provider);
					}
					if (conversionType == Convert.ConvertTypes[9])
					{
						return convertible.ToInt32(provider);
					}
					if (conversionType == Convert.ConvertTypes[10])
					{
						return convertible.ToUInt32(provider);
					}
					if (conversionType == Convert.ConvertTypes[11])
					{
						return convertible.ToInt64(provider);
					}
					if (conversionType == Convert.ConvertTypes[12])
					{
						return convertible.ToUInt64(provider);
					}
					if (conversionType == Convert.ConvertTypes[13])
					{
						return convertible.ToSingle(provider);
					}
					if (conversionType == Convert.ConvertTypes[14])
					{
						return convertible.ToDouble(provider);
					}
					if (conversionType == Convert.ConvertTypes[15])
					{
						return convertible.ToDecimal(provider);
					}
					if (conversionType == Convert.ConvertTypes[16])
					{
						return convertible.ToDateTime(provider);
					}
					if (conversionType == Convert.ConvertTypes[18])
					{
						return convertible.ToString(provider);
					}
					if (conversionType == Convert.ConvertTypes[1])
					{
						return value;
					}
					return convertible.ToType(conversionType, provider);
				}
			}
		}

		// Token: 0x06000B8B RID: 2955 RVA: 0x000CB611 File Offset: 0x000CA811
		[DoesNotReturn]
		private static void ThrowCharOverflowException()
		{
			throw new OverflowException(SR.Overflow_Char);
		}

		// Token: 0x06000B8C RID: 2956 RVA: 0x000CB61D File Offset: 0x000CA81D
		[DoesNotReturn]
		private static void ThrowByteOverflowException()
		{
			throw new OverflowException(SR.Overflow_Byte);
		}

		// Token: 0x06000B8D RID: 2957 RVA: 0x000CB629 File Offset: 0x000CA829
		[DoesNotReturn]
		private static void ThrowSByteOverflowException()
		{
			throw new OverflowException(SR.Overflow_SByte);
		}

		// Token: 0x06000B8E RID: 2958 RVA: 0x000CB635 File Offset: 0x000CA835
		[DoesNotReturn]
		private static void ThrowInt16OverflowException()
		{
			throw new OverflowException(SR.Overflow_Int16);
		}

		// Token: 0x06000B8F RID: 2959 RVA: 0x000CB641 File Offset: 0x000CA841
		[DoesNotReturn]
		private static void ThrowUInt16OverflowException()
		{
			throw new OverflowException(SR.Overflow_UInt16);
		}

		// Token: 0x06000B90 RID: 2960 RVA: 0x000CB64D File Offset: 0x000CA84D
		[DoesNotReturn]
		private static void ThrowInt32OverflowException()
		{
			throw new OverflowException(SR.Overflow_Int32);
		}

		// Token: 0x06000B91 RID: 2961 RVA: 0x000CB659 File Offset: 0x000CA859
		[DoesNotReturn]
		private static void ThrowUInt32OverflowException()
		{
			throw new OverflowException(SR.Overflow_UInt32);
		}

		// Token: 0x06000B92 RID: 2962 RVA: 0x000CB665 File Offset: 0x000CA865
		[DoesNotReturn]
		private static void ThrowInt64OverflowException()
		{
			throw new OverflowException(SR.Overflow_Int64);
		}

		// Token: 0x06000B93 RID: 2963 RVA: 0x000CB671 File Offset: 0x000CA871
		[DoesNotReturn]
		private static void ThrowUInt64OverflowException()
		{
			throw new OverflowException(SR.Overflow_UInt64);
		}

		// Token: 0x06000B94 RID: 2964 RVA: 0x000CB67D File Offset: 0x000CA87D
		public static bool ToBoolean(object value)
		{
			return value != null && ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000B95 RID: 2965 RVA: 0x000CB690 File Offset: 0x000CA890
		public static bool ToBoolean(object value, IFormatProvider provider)
		{
			return value != null && ((IConvertible)value).ToBoolean(provider);
		}

		// Token: 0x06000B96 RID: 2966 RVA: 0x000AC098 File Offset: 0x000AB298
		public static bool ToBoolean(bool value)
		{
			return value;
		}

		// Token: 0x06000B97 RID: 2967 RVA: 0x000CB6A3 File Offset: 0x000CA8A3
		[CLSCompliant(false)]
		public static bool ToBoolean(sbyte value)
		{
			return value != 0;
		}

		// Token: 0x06000B98 RID: 2968 RVA: 0x000CB6A9 File Offset: 0x000CA8A9
		public static bool ToBoolean(char value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000B99 RID: 2969 RVA: 0x000CB6A3 File Offset: 0x000CA8A3
		public static bool ToBoolean(byte value)
		{
			return value > 0;
		}

		// Token: 0x06000B9A RID: 2970 RVA: 0x000CB6A3 File Offset: 0x000CA8A3
		public static bool ToBoolean(short value)
		{
			return value != 0;
		}

		// Token: 0x06000B9B RID: 2971 RVA: 0x000CB6A3 File Offset: 0x000CA8A3
		[CLSCompliant(false)]
		public static bool ToBoolean(ushort value)
		{
			return value > 0;
		}

		// Token: 0x06000B9C RID: 2972 RVA: 0x000CB6A3 File Offset: 0x000CA8A3
		public static bool ToBoolean(int value)
		{
			return value != 0;
		}

		// Token: 0x06000B9D RID: 2973 RVA: 0x000CB6A3 File Offset: 0x000CA8A3
		[CLSCompliant(false)]
		public static bool ToBoolean(uint value)
		{
			return value > 0U;
		}

		// Token: 0x06000B9E RID: 2974 RVA: 0x000CB6B7 File Offset: 0x000CA8B7
		public static bool ToBoolean(long value)
		{
			return value != 0L;
		}

		// Token: 0x06000B9F RID: 2975 RVA: 0x000CB6B7 File Offset: 0x000CA8B7
		[CLSCompliant(false)]
		public static bool ToBoolean(ulong value)
		{
			return value > 0UL;
		}

		// Token: 0x06000BA0 RID: 2976 RVA: 0x000CB6BE File Offset: 0x000CA8BE
		public static bool ToBoolean(string value)
		{
			return value != null && bool.Parse(value);
		}

		// Token: 0x06000BA1 RID: 2977 RVA: 0x000CB6BE File Offset: 0x000CA8BE
		public static bool ToBoolean(string value, IFormatProvider provider)
		{
			return value != null && bool.Parse(value);
		}

		// Token: 0x06000BA2 RID: 2978 RVA: 0x000CB6CB File Offset: 0x000CA8CB
		public static bool ToBoolean(float value)
		{
			return value != 0f;
		}

		// Token: 0x06000BA3 RID: 2979 RVA: 0x000CB6D8 File Offset: 0x000CA8D8
		public static bool ToBoolean(double value)
		{
			return value != 0.0;
		}

		// Token: 0x06000BA4 RID: 2980 RVA: 0x000CB6E9 File Offset: 0x000CA8E9
		public static bool ToBoolean(decimal value)
		{
			return value != 0m;
		}

		// Token: 0x06000BA5 RID: 2981 RVA: 0x000CB6F6 File Offset: 0x000CA8F6
		public static bool ToBoolean(DateTime value)
		{
			return ((IConvertible)value).ToBoolean(null);
		}

		// Token: 0x06000BA6 RID: 2982 RVA: 0x000CB704 File Offset: 0x000CA904
		public static char ToChar(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(null);
			}
			return '\0';
		}

		// Token: 0x06000BA7 RID: 2983 RVA: 0x000CB717 File Offset: 0x000CA917
		public static char ToChar(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToChar(provider);
			}
			return '\0';
		}

		// Token: 0x06000BA8 RID: 2984 RVA: 0x000CB72A File Offset: 0x000CA92A
		public static char ToChar(bool value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BA9 RID: 2985 RVA: 0x000AC098 File Offset: 0x000AB298
		public static char ToChar(char value)
		{
			return value;
		}

		// Token: 0x06000BAA RID: 2986 RVA: 0x000CB738 File Offset: 0x000CA938
		[CLSCompliant(false)]
		public static char ToChar(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000BAB RID: 2987 RVA: 0x000AC098 File Offset: 0x000AB298
		public static char ToChar(byte value)
		{
			return (char)value;
		}

		// Token: 0x06000BAC RID: 2988 RVA: 0x000CB738 File Offset: 0x000CA938
		public static char ToChar(short value)
		{
			if (value < 0)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000BAD RID: 2989 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static char ToChar(ushort value)
		{
			return (char)value;
		}

		// Token: 0x06000BAE RID: 2990 RVA: 0x000CB745 File Offset: 0x000CA945
		public static char ToChar(int value)
		{
			return Convert.ToChar((uint)value);
		}

		// Token: 0x06000BAF RID: 2991 RVA: 0x000CB74D File Offset: 0x000CA94D
		[CLSCompliant(false)]
		public static char ToChar(uint value)
		{
			if (value > 65535U)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000BB0 RID: 2992 RVA: 0x000CB75E File Offset: 0x000CA95E
		public static char ToChar(long value)
		{
			return Convert.ToChar((ulong)value);
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x000CB766 File Offset: 0x000CA966
		[CLSCompliant(false)]
		public static char ToChar(ulong value)
		{
			if (value > 65535UL)
			{
				Convert.ThrowCharOverflowException();
			}
			return (char)value;
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000CB778 File Offset: 0x000CA978
		[NullableContext(1)]
		public static char ToChar(string value)
		{
			return Convert.ToChar(value, null);
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000CB781 File Offset: 0x000CA981
		[NullableContext(1)]
		public static char ToChar(string value, [Nullable(2)] IFormatProvider provider)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (value.Length != 1)
			{
				throw new FormatException(SR.Format_NeedSingleChar);
			}
			return value[0];
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000CB7AC File Offset: 0x000CA9AC
		public static char ToChar(float value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x000CB7BA File Offset: 0x000CA9BA
		public static char ToChar(double value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x000CB7C8 File Offset: 0x000CA9C8
		public static char ToChar(decimal value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x000CB7D6 File Offset: 0x000CA9D6
		public static char ToChar(DateTime value)
		{
			return ((IConvertible)value).ToChar(null);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000CB7E4 File Offset: 0x000CA9E4
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(null);
			}
			return 0;
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000CB7F7 File Offset: 0x000CA9F7
		[CLSCompliant(false)]
		public static sbyte ToSByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSByte(provider);
			}
			return 0;
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000CB80A File Offset: 0x000CAA0A
		[CLSCompliant(false)]
		public static sbyte ToSByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static sbyte ToSByte(sbyte value)
		{
			return value;
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000CB812 File Offset: 0x000CAA12
		[CLSCompliant(false)]
		public static sbyte ToSByte(char value)
		{
			if (value > '\u007f')
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000CB812 File Offset: 0x000CAA12
		[CLSCompliant(false)]
		public static sbyte ToSByte(byte value)
		{
			if (value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x06000BBE RID: 3006 RVA: 0x000CB820 File Offset: 0x000CAA20
		[CLSCompliant(false)]
		public static sbyte ToSByte(short value)
		{
			if (value < -128 || value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x06000BBF RID: 3007 RVA: 0x000CB812 File Offset: 0x000CAA12
		[CLSCompliant(false)]
		public static sbyte ToSByte(ushort value)
		{
			if (value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x06000BC0 RID: 3008 RVA: 0x000CB820 File Offset: 0x000CAA20
		[CLSCompliant(false)]
		public static sbyte ToSByte(int value)
		{
			if (value < -128 || value > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x06000BC1 RID: 3009 RVA: 0x000CB833 File Offset: 0x000CAA33
		[CLSCompliant(false)]
		public static sbyte ToSByte(uint value)
		{
			if (value > 127U)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x06000BC2 RID: 3010 RVA: 0x000CB841 File Offset: 0x000CAA41
		[CLSCompliant(false)]
		public static sbyte ToSByte(long value)
		{
			if (value < -128L || value > 127L)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x06000BC3 RID: 3011 RVA: 0x000CB856 File Offset: 0x000CAA56
		[CLSCompliant(false)]
		public static sbyte ToSByte(ulong value)
		{
			if (value > 127UL)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)value;
		}

		// Token: 0x06000BC4 RID: 3012 RVA: 0x000CB865 File Offset: 0x000CAA65
		[CLSCompliant(false)]
		public static sbyte ToSByte(float value)
		{
			return Convert.ToSByte((double)value);
		}

		// Token: 0x06000BC5 RID: 3013 RVA: 0x000CB86E File Offset: 0x000CAA6E
		[CLSCompliant(false)]
		public static sbyte ToSByte(double value)
		{
			return Convert.ToSByte(Convert.ToInt32(value));
		}

		// Token: 0x06000BC6 RID: 3014 RVA: 0x000CB87B File Offset: 0x000CAA7B
		[CLSCompliant(false)]
		public static sbyte ToSByte(decimal value)
		{
			return decimal.ToSByte(decimal.Round(value, 0));
		}

		// Token: 0x06000BC7 RID: 3015 RVA: 0x000CB889 File Offset: 0x000CAA89
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return sbyte.Parse(value);
		}

		// Token: 0x06000BC8 RID: 3016 RVA: 0x000CB896 File Offset: 0x000CAA96
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, [Nullable(2)] IFormatProvider provider)
		{
			return sbyte.Parse(value, provider);
		}

		// Token: 0x06000BC9 RID: 3017 RVA: 0x000CB89F File Offset: 0x000CAA9F
		[CLSCompliant(false)]
		public static sbyte ToSByte(DateTime value)
		{
			return ((IConvertible)value).ToSByte(null);
		}

		// Token: 0x06000BCA RID: 3018 RVA: 0x000CB8AD File Offset: 0x000CAAAD
		public static byte ToByte(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(null);
			}
			return 0;
		}

		// Token: 0x06000BCB RID: 3019 RVA: 0x000CB8C0 File Offset: 0x000CAAC0
		public static byte ToByte(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToByte(provider);
			}
			return 0;
		}

		// Token: 0x06000BCC RID: 3020 RVA: 0x000CB80A File Offset: 0x000CAA0A
		public static byte ToByte(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x000AC098 File Offset: 0x000AB298
		public static byte ToByte(byte value)
		{
			return value;
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x000CB8D3 File Offset: 0x000CAAD3
		public static byte ToByte(char value)
		{
			if (value > 'ÿ')
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x000CB8E4 File Offset: 0x000CAAE4
		[CLSCompliant(false)]
		public static byte ToByte(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x000CB8F1 File Offset: 0x000CAAF1
		public static byte ToByte(short value)
		{
			if (value > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x000CB8D3 File Offset: 0x000CAAD3
		[CLSCompliant(false)]
		public static byte ToByte(ushort value)
		{
			if (value > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x000CB902 File Offset: 0x000CAB02
		public static byte ToByte(int value)
		{
			return Convert.ToByte((uint)value);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x000CB8F1 File Offset: 0x000CAAF1
		[CLSCompliant(false)]
		public static byte ToByte(uint value)
		{
			if (value > 255U)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x000CB90A File Offset: 0x000CAB0A
		public static byte ToByte(long value)
		{
			return Convert.ToByte((ulong)value);
		}

		// Token: 0x06000BD5 RID: 3029 RVA: 0x000CB912 File Offset: 0x000CAB12
		[CLSCompliant(false)]
		public static byte ToByte(ulong value)
		{
			if (value > 255UL)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)value;
		}

		// Token: 0x06000BD6 RID: 3030 RVA: 0x000CB924 File Offset: 0x000CAB24
		public static byte ToByte(float value)
		{
			return Convert.ToByte((double)value);
		}

		// Token: 0x06000BD7 RID: 3031 RVA: 0x000CB92D File Offset: 0x000CAB2D
		public static byte ToByte(double value)
		{
			return Convert.ToByte(Convert.ToInt32(value));
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000CB93A File Offset: 0x000CAB3A
		public static byte ToByte(decimal value)
		{
			return decimal.ToByte(decimal.Round(value, 0));
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000CB948 File Offset: 0x000CAB48
		public static byte ToByte(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value);
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000CB955 File Offset: 0x000CAB55
		public static byte ToByte(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return byte.Parse(value, provider);
		}

		// Token: 0x06000BDB RID: 3035 RVA: 0x000CB963 File Offset: 0x000CAB63
		public static byte ToByte(DateTime value)
		{
			return ((IConvertible)value).ToByte(null);
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x000CB971 File Offset: 0x000CAB71
		public static short ToInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(null);
			}
			return 0;
		}

		// Token: 0x06000BDD RID: 3037 RVA: 0x000CB984 File Offset: 0x000CAB84
		public static short ToInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt16(provider);
			}
			return 0;
		}

		// Token: 0x06000BDE RID: 3038 RVA: 0x000CB80A File Offset: 0x000CAA0A
		public static short ToInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BDF RID: 3039 RVA: 0x000CB997 File Offset: 0x000CAB97
		public static short ToInt16(char value)
		{
			if (value > '翿')
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x06000BE0 RID: 3040 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static short ToInt16(sbyte value)
		{
			return (short)value;
		}

		// Token: 0x06000BE1 RID: 3041 RVA: 0x000AC098 File Offset: 0x000AB298
		public static short ToInt16(byte value)
		{
			return (short)value;
		}

		// Token: 0x06000BE2 RID: 3042 RVA: 0x000CB997 File Offset: 0x000CAB97
		[CLSCompliant(false)]
		public static short ToInt16(ushort value)
		{
			if (value > 32767)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x000CB9A8 File Offset: 0x000CABA8
		public static short ToInt16(int value)
		{
			if (value < -32768 || value > 32767)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x000CB9C1 File Offset: 0x000CABC1
		[CLSCompliant(false)]
		public static short ToInt16(uint value)
		{
			if (value > 32767U)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x000AC098 File Offset: 0x000AB298
		public static short ToInt16(short value)
		{
			return value;
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x000CB9D2 File Offset: 0x000CABD2
		public static short ToInt16(long value)
		{
			if (value < -32768L || value > 32767L)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x000CB9ED File Offset: 0x000CABED
		[CLSCompliant(false)]
		public static short ToInt16(ulong value)
		{
			if (value > 32767UL)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)value;
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x000CB9FF File Offset: 0x000CABFF
		public static short ToInt16(float value)
		{
			return Convert.ToInt16((double)value);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x000CBA08 File Offset: 0x000CAC08
		public static short ToInt16(double value)
		{
			return Convert.ToInt16(Convert.ToInt32(value));
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x000CBA15 File Offset: 0x000CAC15
		public static short ToInt16(decimal value)
		{
			return decimal.ToInt16(decimal.Round(value, 0));
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x000CBA23 File Offset: 0x000CAC23
		public static short ToInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value);
		}

		// Token: 0x06000BEC RID: 3052 RVA: 0x000CBA30 File Offset: 0x000CAC30
		public static short ToInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return short.Parse(value, provider);
		}

		// Token: 0x06000BED RID: 3053 RVA: 0x000CBA3E File Offset: 0x000CAC3E
		public static short ToInt16(DateTime value)
		{
			return ((IConvertible)value).ToInt16(null);
		}

		// Token: 0x06000BEE RID: 3054 RVA: 0x000CBA4C File Offset: 0x000CAC4C
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(null);
			}
			return 0;
		}

		// Token: 0x06000BEF RID: 3055 RVA: 0x000CBA5F File Offset: 0x000CAC5F
		[CLSCompliant(false)]
		public static ushort ToUInt16(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt16(provider);
			}
			return 0;
		}

		// Token: 0x06000BF0 RID: 3056 RVA: 0x000CB80A File Offset: 0x000CAA0A
		[CLSCompliant(false)]
		public static ushort ToUInt16(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000BF1 RID: 3057 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static ushort ToUInt16(char value)
		{
			return (ushort)value;
		}

		// Token: 0x06000BF2 RID: 3058 RVA: 0x000CBA72 File Offset: 0x000CAC72
		[CLSCompliant(false)]
		public static ushort ToUInt16(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x06000BF3 RID: 3059 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static ushort ToUInt16(byte value)
		{
			return (ushort)value;
		}

		// Token: 0x06000BF4 RID: 3060 RVA: 0x000CBA72 File Offset: 0x000CAC72
		[CLSCompliant(false)]
		public static ushort ToUInt16(short value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x06000BF5 RID: 3061 RVA: 0x000CBA7F File Offset: 0x000CAC7F
		[CLSCompliant(false)]
		public static ushort ToUInt16(int value)
		{
			return Convert.ToUInt16((uint)value);
		}

		// Token: 0x06000BF6 RID: 3062 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static ushort ToUInt16(ushort value)
		{
			return value;
		}

		// Token: 0x06000BF7 RID: 3063 RVA: 0x000CBA87 File Offset: 0x000CAC87
		[CLSCompliant(false)]
		public static ushort ToUInt16(uint value)
		{
			if (value > 65535U)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x06000BF8 RID: 3064 RVA: 0x000CBA98 File Offset: 0x000CAC98
		[CLSCompliant(false)]
		public static ushort ToUInt16(long value)
		{
			return Convert.ToUInt16((ulong)value);
		}

		// Token: 0x06000BF9 RID: 3065 RVA: 0x000CBAA0 File Offset: 0x000CACA0
		[CLSCompliant(false)]
		public static ushort ToUInt16(ulong value)
		{
			if (value > 65535UL)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)value;
		}

		// Token: 0x06000BFA RID: 3066 RVA: 0x000CBAB2 File Offset: 0x000CACB2
		[CLSCompliant(false)]
		public static ushort ToUInt16(float value)
		{
			return Convert.ToUInt16((double)value);
		}

		// Token: 0x06000BFB RID: 3067 RVA: 0x000CBABB File Offset: 0x000CACBB
		[CLSCompliant(false)]
		public static ushort ToUInt16(double value)
		{
			return Convert.ToUInt16(Convert.ToInt32(value));
		}

		// Token: 0x06000BFC RID: 3068 RVA: 0x000CBAC8 File Offset: 0x000CACC8
		[CLSCompliant(false)]
		public static ushort ToUInt16(decimal value)
		{
			return decimal.ToUInt16(decimal.Round(value, 0));
		}

		// Token: 0x06000BFD RID: 3069 RVA: 0x000CBAD6 File Offset: 0x000CACD6
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value);
		}

		// Token: 0x06000BFE RID: 3070 RVA: 0x000CBAE3 File Offset: 0x000CACE3
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return ushort.Parse(value, provider);
		}

		// Token: 0x06000BFF RID: 3071 RVA: 0x000CBAF1 File Offset: 0x000CACF1
		[CLSCompliant(false)]
		public static ushort ToUInt16(DateTime value)
		{
			return ((IConvertible)value).ToUInt16(null);
		}

		// Token: 0x06000C00 RID: 3072 RVA: 0x000CBAFF File Offset: 0x000CACFF
		public static int ToInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(null);
			}
			return 0;
		}

		// Token: 0x06000C01 RID: 3073 RVA: 0x000CBB12 File Offset: 0x000CAD12
		public static int ToInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt32(provider);
			}
			return 0;
		}

		// Token: 0x06000C02 RID: 3074 RVA: 0x000CB80A File Offset: 0x000CAA0A
		public static int ToInt32(bool value)
		{
			if (!value)
			{
				return 0;
			}
			return 1;
		}

		// Token: 0x06000C03 RID: 3075 RVA: 0x000AC098 File Offset: 0x000AB298
		public static int ToInt32(char value)
		{
			return (int)value;
		}

		// Token: 0x06000C04 RID: 3076 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static int ToInt32(sbyte value)
		{
			return (int)value;
		}

		// Token: 0x06000C05 RID: 3077 RVA: 0x000AC098 File Offset: 0x000AB298
		public static int ToInt32(byte value)
		{
			return (int)value;
		}

		// Token: 0x06000C06 RID: 3078 RVA: 0x000AC098 File Offset: 0x000AB298
		public static int ToInt32(short value)
		{
			return (int)value;
		}

		// Token: 0x06000C07 RID: 3079 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static int ToInt32(ushort value)
		{
			return (int)value;
		}

		// Token: 0x06000C08 RID: 3080 RVA: 0x000CBB25 File Offset: 0x000CAD25
		[CLSCompliant(false)]
		public static int ToInt32(uint value)
		{
			if (value < 0U)
			{
				Convert.ThrowInt32OverflowException();
			}
			return (int)value;
		}

		// Token: 0x06000C09 RID: 3081 RVA: 0x000AC098 File Offset: 0x000AB298
		public static int ToInt32(int value)
		{
			return value;
		}

		// Token: 0x06000C0A RID: 3082 RVA: 0x000CBB31 File Offset: 0x000CAD31
		public static int ToInt32(long value)
		{
			if (value < -2147483648L || value > 2147483647L)
			{
				Convert.ThrowInt32OverflowException();
			}
			return (int)value;
		}

		// Token: 0x06000C0B RID: 3083 RVA: 0x000CBB4C File Offset: 0x000CAD4C
		[CLSCompliant(false)]
		public static int ToInt32(ulong value)
		{
			if (value > 2147483647UL)
			{
				Convert.ThrowInt32OverflowException();
			}
			return (int)value;
		}

		// Token: 0x06000C0C RID: 3084 RVA: 0x000CBB5E File Offset: 0x000CAD5E
		public static int ToInt32(float value)
		{
			return Convert.ToInt32((double)value);
		}

		// Token: 0x06000C0D RID: 3085 RVA: 0x000CBB68 File Offset: 0x000CAD68
		public static int ToInt32(double value)
		{
			if (value >= 0.0)
			{
				if (value < 2147483647.5)
				{
					int num = (int)value;
					double num2 = value - (double)num;
					if (num2 > 0.5 || (num2 == 0.5 && (num & 1) != 0))
					{
						num++;
					}
					return num;
				}
			}
			else if (value >= -2147483648.5)
			{
				int num3 = (int)value;
				double num4 = value - (double)num3;
				if (num4 < -0.5 || (num4 == -0.5 && (num3 & 1) != 0))
				{
					num3--;
				}
				return num3;
			}
			throw new OverflowException(SR.Overflow_Int32);
		}

		// Token: 0x06000C0E RID: 3086 RVA: 0x000CBBF9 File Offset: 0x000CADF9
		public static int ToInt32(decimal value)
		{
			return decimal.ToInt32(decimal.Round(value, 0));
		}

		// Token: 0x06000C0F RID: 3087 RVA: 0x000CBC07 File Offset: 0x000CAE07
		public static int ToInt32(string value)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value);
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x000CBC14 File Offset: 0x000CAE14
		public static int ToInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0;
			}
			return int.Parse(value, provider);
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x000CBC22 File Offset: 0x000CAE22
		public static int ToInt32(DateTime value)
		{
			return ((IConvertible)value).ToInt32(null);
		}

		// Token: 0x06000C12 RID: 3090 RVA: 0x000CBC30 File Offset: 0x000CAE30
		[CLSCompliant(false)]
		public static uint ToUInt32(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(null);
			}
			return 0U;
		}

		// Token: 0x06000C13 RID: 3091 RVA: 0x000CBC43 File Offset: 0x000CAE43
		[CLSCompliant(false)]
		public static uint ToUInt32(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt32(provider);
			}
			return 0U;
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x000CB80A File Offset: 0x000CAA0A
		[CLSCompliant(false)]
		public static uint ToUInt32(bool value)
		{
			if (!value)
			{
				return 0U;
			}
			return 1U;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static uint ToUInt32(char value)
		{
			return (uint)value;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x000CBC56 File Offset: 0x000CAE56
		[CLSCompliant(false)]
		public static uint ToUInt32(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		// Token: 0x06000C17 RID: 3095 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static uint ToUInt32(byte value)
		{
			return (uint)value;
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x000CBC56 File Offset: 0x000CAE56
		[CLSCompliant(false)]
		public static uint ToUInt32(short value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static uint ToUInt32(ushort value)
		{
			return (uint)value;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x000CBC56 File Offset: 0x000CAE56
		[CLSCompliant(false)]
		public static uint ToUInt32(int value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static uint ToUInt32(uint value)
		{
			return value;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x000CBC62 File Offset: 0x000CAE62
		[CLSCompliant(false)]
		public static uint ToUInt32(long value)
		{
			return Convert.ToUInt32((ulong)value);
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x000CBC6A File Offset: 0x000CAE6A
		[CLSCompliant(false)]
		public static uint ToUInt32(ulong value)
		{
			if (value > (ulong)-1)
			{
				Convert.ThrowUInt32OverflowException();
			}
			return (uint)value;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x000CBC78 File Offset: 0x000CAE78
		[CLSCompliant(false)]
		public static uint ToUInt32(float value)
		{
			return Convert.ToUInt32((double)value);
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000CBC84 File Offset: 0x000CAE84
		[CLSCompliant(false)]
		public static uint ToUInt32(double value)
		{
			if (value >= -0.5 && value < 4294967295.5)
			{
				uint num = (uint)value;
				double num2 = value - num;
				if (num2 > 0.5 || (num2 == 0.5 && (num & 1U) != 0U))
				{
					num += 1U;
				}
				return num;
			}
			throw new OverflowException(SR.Overflow_UInt32);
		}

		// Token: 0x06000C20 RID: 3104 RVA: 0x000CBCDF File Offset: 0x000CAEDF
		[CLSCompliant(false)]
		public static uint ToUInt32(decimal value)
		{
			return decimal.ToUInt32(decimal.Round(value, 0));
		}

		// Token: 0x06000C21 RID: 3105 RVA: 0x000CBCED File Offset: 0x000CAEED
		[CLSCompliant(false)]
		public static uint ToUInt32(string value)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value);
		}

		// Token: 0x06000C22 RID: 3106 RVA: 0x000CBCFA File Offset: 0x000CAEFA
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0U;
			}
			return uint.Parse(value, provider);
		}

		// Token: 0x06000C23 RID: 3107 RVA: 0x000CBD08 File Offset: 0x000CAF08
		[CLSCompliant(false)]
		public static uint ToUInt32(DateTime value)
		{
			return ((IConvertible)value).ToUInt32(null);
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x000CBD16 File Offset: 0x000CAF16
		public static long ToInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(null);
			}
			return 0L;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x000CBD2A File Offset: 0x000CAF2A
		public static long ToInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToInt64(provider);
			}
			return 0L;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x000CBD3E File Offset: 0x000CAF3E
		public static long ToInt64(bool value)
		{
			return value ? 1L : 0L;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x000CBD48 File Offset: 0x000CAF48
		public static long ToInt64(char value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x000CBD4C File Offset: 0x000CAF4C
		[CLSCompliant(false)]
		public static long ToInt64(sbyte value)
		{
			return (long)value;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x000CBD48 File Offset: 0x000CAF48
		public static long ToInt64(byte value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000CBD4C File Offset: 0x000CAF4C
		public static long ToInt64(short value)
		{
			return (long)value;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x000CBD48 File Offset: 0x000CAF48
		[CLSCompliant(false)]
		public static long ToInt64(ushort value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x000CBD4C File Offset: 0x000CAF4C
		public static long ToInt64(int value)
		{
			return (long)value;
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000CBD48 File Offset: 0x000CAF48
		[CLSCompliant(false)]
		public static long ToInt64(uint value)
		{
			return (long)((ulong)value);
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x000CBD50 File Offset: 0x000CAF50
		[CLSCompliant(false)]
		public static long ToInt64(ulong value)
		{
			if (value < 0UL)
			{
				Convert.ThrowInt64OverflowException();
			}
			return (long)value;
		}

		// Token: 0x06000C2F RID: 3119 RVA: 0x000AC098 File Offset: 0x000AB298
		public static long ToInt64(long value)
		{
			return value;
		}

		// Token: 0x06000C30 RID: 3120 RVA: 0x000CBD5D File Offset: 0x000CAF5D
		public static long ToInt64(float value)
		{
			return Convert.ToInt64((double)value);
		}

		// Token: 0x06000C31 RID: 3121 RVA: 0x000CBD66 File Offset: 0x000CAF66
		public static long ToInt64(double value)
		{
			return checked((long)Math.Round(value));
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x000CBD6F File Offset: 0x000CAF6F
		public static long ToInt64(decimal value)
		{
			return decimal.ToInt64(decimal.Round(value, 0));
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000CBD7D File Offset: 0x000CAF7D
		public static long ToInt64(string value)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value);
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x000CBD8B File Offset: 0x000CAF8B
		public static long ToInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0L;
			}
			return long.Parse(value, provider);
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000CBD9A File Offset: 0x000CAF9A
		public static long ToInt64(DateTime value)
		{
			return ((IConvertible)value).ToInt64(null);
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x000CBDA8 File Offset: 0x000CAFA8
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(null);
			}
			return 0UL;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x000CBDBC File Offset: 0x000CAFBC
		[CLSCompliant(false)]
		public static ulong ToUInt64(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToUInt64(provider);
			}
			return 0UL;
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000CBDD0 File Offset: 0x000CAFD0
		[CLSCompliant(false)]
		public static ulong ToUInt64(bool value)
		{
			if (!value)
			{
				return 0UL;
			}
			return 1UL;
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x000CBD48 File Offset: 0x000CAF48
		[CLSCompliant(false)]
		public static ulong ToUInt64(char value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000CBDDA File Offset: 0x000CAFDA
		[CLSCompliant(false)]
		public static ulong ToUInt64(sbyte value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x000CBD48 File Offset: 0x000CAF48
		[CLSCompliant(false)]
		public static ulong ToUInt64(byte value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x000CBDDA File Offset: 0x000CAFDA
		[CLSCompliant(false)]
		public static ulong ToUInt64(short value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000C3D RID: 3133 RVA: 0x000CBD48 File Offset: 0x000CAF48
		[CLSCompliant(false)]
		public static ulong ToUInt64(ushort value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C3E RID: 3134 RVA: 0x000CBDDA File Offset: 0x000CAFDA
		[CLSCompliant(false)]
		public static ulong ToUInt64(int value)
		{
			if (value < 0)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)((long)value);
		}

		// Token: 0x06000C3F RID: 3135 RVA: 0x000CBD48 File Offset: 0x000CAF48
		[CLSCompliant(false)]
		public static ulong ToUInt64(uint value)
		{
			return (ulong)value;
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000CBDE7 File Offset: 0x000CAFE7
		[CLSCompliant(false)]
		public static ulong ToUInt64(long value)
		{
			if (value < 0L)
			{
				Convert.ThrowUInt64OverflowException();
			}
			return (ulong)value;
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		public static ulong ToUInt64(ulong value)
		{
			return value;
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000CBDF4 File Offset: 0x000CAFF4
		[CLSCompliant(false)]
		public static ulong ToUInt64(float value)
		{
			return Convert.ToUInt64((double)value);
		}

		// Token: 0x06000C43 RID: 3139 RVA: 0x000CBDFD File Offset: 0x000CAFFD
		[CLSCompliant(false)]
		public static ulong ToUInt64(double value)
		{
			return checked((ulong)Math.Round(value));
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x000CBE06 File Offset: 0x000CB006
		[CLSCompliant(false)]
		public static ulong ToUInt64(decimal value)
		{
			return decimal.ToUInt64(decimal.Round(value, 0));
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x000CBE14 File Offset: 0x000CB014
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value);
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x000CBE22 File Offset: 0x000CB022
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0UL;
			}
			return ulong.Parse(value, provider);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x000CBE31 File Offset: 0x000CB031
		[CLSCompliant(false)]
		public static ulong ToUInt64(DateTime value)
		{
			return ((IConvertible)value).ToUInt64(null);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x000CBE3F File Offset: 0x000CB03F
		public static float ToSingle(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(null);
			}
			return 0f;
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x000CBE56 File Offset: 0x000CB056
		public static float ToSingle(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToSingle(provider);
			}
			return 0f;
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x000CBE6D File Offset: 0x000CB06D
		[CLSCompliant(false)]
		public static float ToSingle(sbyte value)
		{
			return (float)value;
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x000CBE6D File Offset: 0x000CB06D
		public static float ToSingle(byte value)
		{
			return (float)value;
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x000CBE71 File Offset: 0x000CB071
		public static float ToSingle(char value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		// Token: 0x06000C4D RID: 3149 RVA: 0x000CBE6D File Offset: 0x000CB06D
		public static float ToSingle(short value)
		{
			return (float)value;
		}

		// Token: 0x06000C4E RID: 3150 RVA: 0x000CBE6D File Offset: 0x000CB06D
		[CLSCompliant(false)]
		public static float ToSingle(ushort value)
		{
			return (float)value;
		}

		// Token: 0x06000C4F RID: 3151 RVA: 0x000CBE6D File Offset: 0x000CB06D
		public static float ToSingle(int value)
		{
			return (float)value;
		}

		// Token: 0x06000C50 RID: 3152 RVA: 0x000CBE7F File Offset: 0x000CB07F
		[CLSCompliant(false)]
		public static float ToSingle(uint value)
		{
			return value;
		}

		// Token: 0x06000C51 RID: 3153 RVA: 0x000CBE6D File Offset: 0x000CB06D
		public static float ToSingle(long value)
		{
			return (float)value;
		}

		// Token: 0x06000C52 RID: 3154 RVA: 0x000CBE7F File Offset: 0x000CB07F
		[CLSCompliant(false)]
		public static float ToSingle(ulong value)
		{
			return value;
		}

		// Token: 0x06000C53 RID: 3155 RVA: 0x000AC098 File Offset: 0x000AB298
		public static float ToSingle(float value)
		{
			return value;
		}

		// Token: 0x06000C54 RID: 3156 RVA: 0x000CBE6D File Offset: 0x000CB06D
		public static float ToSingle(double value)
		{
			return (float)value;
		}

		// Token: 0x06000C55 RID: 3157 RVA: 0x000CBE84 File Offset: 0x000CB084
		public static float ToSingle(decimal value)
		{
			return (float)value;
		}

		// Token: 0x06000C56 RID: 3158 RVA: 0x000CBE8D File Offset: 0x000CB08D
		public static float ToSingle(string value)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value);
		}

		// Token: 0x06000C57 RID: 3159 RVA: 0x000CBE9E File Offset: 0x000CB09E
		public static float ToSingle(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0f;
			}
			return float.Parse(value, provider);
		}

		// Token: 0x06000C58 RID: 3160 RVA: 0x000CBEB0 File Offset: 0x000CB0B0
		public static float ToSingle(bool value)
		{
			return (float)(value ? 1 : 0);
		}

		// Token: 0x06000C59 RID: 3161 RVA: 0x000CBEBA File Offset: 0x000CB0BA
		public static float ToSingle(DateTime value)
		{
			return ((IConvertible)value).ToSingle(null);
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x000CBEC8 File Offset: 0x000CB0C8
		public static double ToDouble(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(null);
			}
			return 0.0;
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x000CBEE3 File Offset: 0x000CB0E3
		public static double ToDouble(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDouble(provider);
			}
			return 0.0;
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x000CBEFE File Offset: 0x000CB0FE
		[CLSCompliant(false)]
		public static double ToDouble(sbyte value)
		{
			return (double)value;
		}

		// Token: 0x06000C5D RID: 3165 RVA: 0x000CBEFE File Offset: 0x000CB0FE
		public static double ToDouble(byte value)
		{
			return (double)value;
		}

		// Token: 0x06000C5E RID: 3166 RVA: 0x000CBEFE File Offset: 0x000CB0FE
		public static double ToDouble(short value)
		{
			return (double)value;
		}

		// Token: 0x06000C5F RID: 3167 RVA: 0x000CBF02 File Offset: 0x000CB102
		public static double ToDouble(char value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		// Token: 0x06000C60 RID: 3168 RVA: 0x000CBEFE File Offset: 0x000CB0FE
		[CLSCompliant(false)]
		public static double ToDouble(ushort value)
		{
			return (double)value;
		}

		// Token: 0x06000C61 RID: 3169 RVA: 0x000CBEFE File Offset: 0x000CB0FE
		public static double ToDouble(int value)
		{
			return (double)value;
		}

		// Token: 0x06000C62 RID: 3170 RVA: 0x000CBF10 File Offset: 0x000CB110
		[CLSCompliant(false)]
		public static double ToDouble(uint value)
		{
			return value;
		}

		// Token: 0x06000C63 RID: 3171 RVA: 0x000CBEFE File Offset: 0x000CB0FE
		public static double ToDouble(long value)
		{
			return (double)value;
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x000CBF10 File Offset: 0x000CB110
		[CLSCompliant(false)]
		public static double ToDouble(ulong value)
		{
			return value;
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x000CBEFE File Offset: 0x000CB0FE
		public static double ToDouble(float value)
		{
			return (double)value;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x000AC098 File Offset: 0x000AB298
		public static double ToDouble(double value)
		{
			return value;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x000CBF15 File Offset: 0x000CB115
		public static double ToDouble(decimal value)
		{
			return (double)value;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x000CBF1E File Offset: 0x000CB11E
		public static double ToDouble(string value)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x000CBF33 File Offset: 0x000CB133
		public static double ToDouble(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0.0;
			}
			return double.Parse(value, provider);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x000CBF49 File Offset: 0x000CB149
		public static double ToDouble(bool value)
		{
			return (double)(value ? 1 : 0);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x000CBF53 File Offset: 0x000CB153
		public static double ToDouble(DateTime value)
		{
			return ((IConvertible)value).ToDouble(null);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x000CBF61 File Offset: 0x000CB161
		public static decimal ToDecimal(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(null);
			}
			return 0m;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x000CBF78 File Offset: 0x000CB178
		public static decimal ToDecimal(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDecimal(provider);
			}
			return 0m;
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x000CBF8F File Offset: 0x000CB18F
		[CLSCompliant(false)]
		public static decimal ToDecimal(sbyte value)
		{
			return value;
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x000CBF97 File Offset: 0x000CB197
		public static decimal ToDecimal(byte value)
		{
			return value;
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x000CBF9F File Offset: 0x000CB19F
		public static decimal ToDecimal(char value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x000CBFAD File Offset: 0x000CB1AD
		public static decimal ToDecimal(short value)
		{
			return value;
		}

		// Token: 0x06000C72 RID: 3186 RVA: 0x000CBFB5 File Offset: 0x000CB1B5
		[CLSCompliant(false)]
		public static decimal ToDecimal(ushort value)
		{
			return value;
		}

		// Token: 0x06000C73 RID: 3187 RVA: 0x000CBFBD File Offset: 0x000CB1BD
		public static decimal ToDecimal(int value)
		{
			return value;
		}

		// Token: 0x06000C74 RID: 3188 RVA: 0x000CBFC5 File Offset: 0x000CB1C5
		[CLSCompliant(false)]
		public static decimal ToDecimal(uint value)
		{
			return value;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x000CBFCD File Offset: 0x000CB1CD
		public static decimal ToDecimal(long value)
		{
			return value;
		}

		// Token: 0x06000C76 RID: 3190 RVA: 0x000CBFD5 File Offset: 0x000CB1D5
		[CLSCompliant(false)]
		public static decimal ToDecimal(ulong value)
		{
			return value;
		}

		// Token: 0x06000C77 RID: 3191 RVA: 0x000CBFDD File Offset: 0x000CB1DD
		public static decimal ToDecimal(float value)
		{
			return (decimal)value;
		}

		// Token: 0x06000C78 RID: 3192 RVA: 0x000CBFE5 File Offset: 0x000CB1E5
		public static decimal ToDecimal(double value)
		{
			return (decimal)value;
		}

		// Token: 0x06000C79 RID: 3193 RVA: 0x000CBFED File Offset: 0x000CB1ED
		public static decimal ToDecimal(string value)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value);
		}

		// Token: 0x06000C7A RID: 3194 RVA: 0x000CBFFE File Offset: 0x000CB1FE
		public static decimal ToDecimal(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return 0m;
			}
			return decimal.Parse(value, provider);
		}

		// Token: 0x06000C7B RID: 3195 RVA: 0x000AC098 File Offset: 0x000AB298
		public static decimal ToDecimal(decimal value)
		{
			return value;
		}

		// Token: 0x06000C7C RID: 3196 RVA: 0x000CC010 File Offset: 0x000CB210
		public static decimal ToDecimal(bool value)
		{
			return value ? 1 : 0;
		}

		// Token: 0x06000C7D RID: 3197 RVA: 0x000CC01E File Offset: 0x000CB21E
		public static decimal ToDecimal(DateTime value)
		{
			return ((IConvertible)value).ToDecimal(null);
		}

		// Token: 0x06000C7E RID: 3198 RVA: 0x000AC098 File Offset: 0x000AB298
		public static DateTime ToDateTime(DateTime value)
		{
			return value;
		}

		// Token: 0x06000C7F RID: 3199 RVA: 0x000CC02C File Offset: 0x000CB22C
		public static DateTime ToDateTime(object value)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(null);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000C80 RID: 3200 RVA: 0x000CC043 File Offset: 0x000CB243
		public static DateTime ToDateTime(object value, IFormatProvider provider)
		{
			if (value != null)
			{
				return ((IConvertible)value).ToDateTime(provider);
			}
			return DateTime.MinValue;
		}

		// Token: 0x06000C81 RID: 3201 RVA: 0x000CC05A File Offset: 0x000CB25A
		public static DateTime ToDateTime(string value)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value);
		}

		// Token: 0x06000C82 RID: 3202 RVA: 0x000CC06D File Offset: 0x000CB26D
		public static DateTime ToDateTime(string value, IFormatProvider provider)
		{
			if (value == null)
			{
				return new DateTime(0L);
			}
			return DateTime.Parse(value, provider);
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x000CC081 File Offset: 0x000CB281
		[CLSCompliant(false)]
		public static DateTime ToDateTime(sbyte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C84 RID: 3204 RVA: 0x000CC08F File Offset: 0x000CB28F
		public static DateTime ToDateTime(byte value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C85 RID: 3205 RVA: 0x000CC09D File Offset: 0x000CB29D
		public static DateTime ToDateTime(short value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C86 RID: 3206 RVA: 0x000CC0AB File Offset: 0x000CB2AB
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ushort value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C87 RID: 3207 RVA: 0x000CC0B9 File Offset: 0x000CB2B9
		public static DateTime ToDateTime(int value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C88 RID: 3208 RVA: 0x000CC0C7 File Offset: 0x000CB2C7
		[CLSCompliant(false)]
		public static DateTime ToDateTime(uint value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x000CC0D5 File Offset: 0x000CB2D5
		public static DateTime ToDateTime(long value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x000CC0E3 File Offset: 0x000CB2E3
		[CLSCompliant(false)]
		public static DateTime ToDateTime(ulong value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C8B RID: 3211 RVA: 0x000CC0F1 File Offset: 0x000CB2F1
		public static DateTime ToDateTime(bool value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C8C RID: 3212 RVA: 0x000CC0FF File Offset: 0x000CB2FF
		public static DateTime ToDateTime(char value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x000CC10D File Offset: 0x000CB30D
		public static DateTime ToDateTime(float value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C8E RID: 3214 RVA: 0x000CC11B File Offset: 0x000CB31B
		public static DateTime ToDateTime(double value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x000CC129 File Offset: 0x000CB329
		public static DateTime ToDateTime(decimal value)
		{
			return ((IConvertible)value).ToDateTime(null);
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x000CC137 File Offset: 0x000CB337
		public static string ToString(object value)
		{
			return Convert.ToString(value, null);
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x000CC140 File Offset: 0x000CB340
		public static string ToString(object value, IFormatProvider provider)
		{
			IConvertible convertible = value as IConvertible;
			if (convertible != null)
			{
				return convertible.ToString(provider);
			}
			IFormattable formattable = value as IFormattable;
			if (formattable != null)
			{
				return formattable.ToString(null, provider);
			}
			if (value != null)
			{
				return value.ToString();
			}
			return string.Empty;
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x000CC181 File Offset: 0x000CB381
		[NullableContext(1)]
		public static string ToString(bool value)
		{
			return value.ToString();
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x000CC181 File Offset: 0x000CB381
		[NullableContext(1)]
		public static string ToString(bool value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString();
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x000CC18A File Offset: 0x000CB38A
		[NullableContext(1)]
		public static string ToString(char value)
		{
			return char.ToString(value);
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x000CC192 File Offset: 0x000CB392
		[NullableContext(1)]
		public static string ToString(char value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString();
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x000CC19B File Offset: 0x000CB39B
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static string ToString(sbyte value)
		{
			return value.ToString();
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x000CC1A4 File Offset: 0x000CB3A4
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static string ToString(sbyte value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x000CC1AE File Offset: 0x000CB3AE
		[NullableContext(1)]
		public static string ToString(byte value)
		{
			return value.ToString();
		}

		// Token: 0x06000C99 RID: 3225 RVA: 0x000CC1B7 File Offset: 0x000CB3B7
		[NullableContext(1)]
		public static string ToString(byte value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x000CC1C1 File Offset: 0x000CB3C1
		[NullableContext(1)]
		public static string ToString(short value)
		{
			return value.ToString();
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000CC1CA File Offset: 0x000CB3CA
		[NullableContext(1)]
		public static string ToString(short value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000CC1D4 File Offset: 0x000CB3D4
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static string ToString(ushort value)
		{
			return value.ToString();
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x000CC1DD File Offset: 0x000CB3DD
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static string ToString(ushort value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x000CC1E7 File Offset: 0x000CB3E7
		[NullableContext(1)]
		public static string ToString(int value)
		{
			return value.ToString();
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x000CC1F0 File Offset: 0x000CB3F0
		[NullableContext(1)]
		public static string ToString(int value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000CC1FA File Offset: 0x000CB3FA
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static string ToString(uint value)
		{
			return value.ToString();
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000CC203 File Offset: 0x000CB403
		[CLSCompliant(false)]
		[NullableContext(1)]
		public static string ToString(uint value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000CC20D File Offset: 0x000CB40D
		[NullableContext(1)]
		public static string ToString(long value)
		{
			return value.ToString();
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000CC216 File Offset: 0x000CB416
		[NullableContext(1)]
		public static string ToString(long value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000CC220 File Offset: 0x000CB420
		[NullableContext(1)]
		[CLSCompliant(false)]
		public static string ToString(ulong value)
		{
			return value.ToString();
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x000CC229 File Offset: 0x000CB429
		[CLSCompliant(false)]
		[NullableContext(1)]
		public static string ToString(ulong value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000CC233 File Offset: 0x000CB433
		[NullableContext(1)]
		public static string ToString(float value)
		{
			return value.ToString();
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x000CC23C File Offset: 0x000CB43C
		[NullableContext(1)]
		public static string ToString(float value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x000CC246 File Offset: 0x000CB446
		[NullableContext(1)]
		public static string ToString(double value)
		{
			return value.ToString();
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000CC24F File Offset: 0x000CB44F
		[NullableContext(1)]
		public static string ToString(double value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000CC259 File Offset: 0x000CB459
		[NullableContext(1)]
		public static string ToString(decimal value)
		{
			return value.ToString();
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000CC262 File Offset: 0x000CB462
		[NullableContext(1)]
		public static string ToString(decimal value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000CC26C File Offset: 0x000CB46C
		[NullableContext(1)]
		public static string ToString(DateTime value)
		{
			return value.ToString();
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x000CC275 File Offset: 0x000CB475
		[NullableContext(1)]
		public static string ToString(DateTime value, [Nullable(2)] IFormatProvider provider)
		{
			return value.ToString(provider);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000AC098 File Offset: 0x000AB298
		[return: NotNullIfNotNull("value")]
		public static string ToString(string value)
		{
			return value;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000AC098 File Offset: 0x000AB298
		[return: NotNullIfNotNull("value")]
		public static string ToString(string value, IFormatProvider provider)
		{
			return value;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000CC280 File Offset: 0x000CB480
		public static byte ToByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4608);
			if (num > 255)
			{
				Convert.ThrowByteOverflowException();
			}
			return (byte)num;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000CC2D0 File Offset: 0x000CB4D0
		[CLSCompliant(false)]
		public static sbyte ToSByte(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 5120);
			if (fromBase != 10 && num <= 255)
			{
				return (sbyte)num;
			}
			if (num < -128 || num > 127)
			{
				Convert.ThrowSByteOverflowException();
			}
			return (sbyte)num;
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x000CC334 File Offset: 0x000CB534
		public static short ToInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 6144);
			if (fromBase != 10 && num <= 65535)
			{
				return (short)num;
			}
			if (num < -32768 || num > 32767)
			{
				Convert.ThrowInt16OverflowException();
			}
			return (short)num;
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x000CC39C File Offset: 0x000CB59C
		[CLSCompliant(false)]
		public static ushort ToUInt16(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			if (value == null)
			{
				return 0;
			}
			int num = ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4608);
			if (num > 65535)
			{
				Convert.ThrowUInt16OverflowException();
			}
			return (ushort)num;
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x000CC3EC File Offset: 0x000CB5EC
		public static int ToInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			if (value == null)
			{
				return 0;
			}
			return ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4096);
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x000CC421 File Offset: 0x000CB621
		[CLSCompliant(false)]
		public static uint ToUInt32(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			if (value == null)
			{
				return 0U;
			}
			return (uint)ParseNumbers.StringToInt(value.AsSpan(), fromBase, 4608);
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x000CC456 File Offset: 0x000CB656
		public static long ToInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			if (value == null)
			{
				return 0L;
			}
			return ParseNumbers.StringToLong(value.AsSpan(), fromBase, 4096);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000CC48C File Offset: 0x000CB68C
		[CLSCompliant(false)]
		public static ulong ToUInt64(string value, int fromBase)
		{
			if (fromBase != 2 && fromBase != 8 && fromBase != 10 && fromBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			if (value == null)
			{
				return 0UL;
			}
			return (ulong)ParseNumbers.StringToLong(value.AsSpan(), fromBase, 4608);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x000CC4C2 File Offset: 0x000CB6C2
		[NullableContext(1)]
		public static string ToString(byte value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 64);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000CC4ED File Offset: 0x000CB6ED
		[NullableContext(1)]
		public static string ToString(short value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			return ParseNumbers.IntToString((int)value, toBase, -1, ' ', 128);
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000CC51B File Offset: 0x000CB71B
		[NullableContext(1)]
		public static string ToString(int value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			return ParseNumbers.IntToString(value, toBase, -1, ' ', 0);
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x000CC545 File Offset: 0x000CB745
		[NullableContext(1)]
		public static string ToString(long value, int toBase)
		{
			if (toBase != 2 && toBase != 8 && toBase != 10 && toBase != 16)
			{
				throw new ArgumentException(SR.Arg_InvalidBase);
			}
			return ParseNumbers.LongToString(value, toBase, -1, ' ', 0);
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x000CC56F File Offset: 0x000CB76F
		[NullableContext(1)]
		public static string ToBase64String(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(new ReadOnlySpan<byte>(inArray), Base64FormattingOptions.None);
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x000CC58B File Offset: 0x000CB78B
		[NullableContext(1)]
		public static string ToBase64String(byte[] inArray, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToBase64String(new ReadOnlySpan<byte>(inArray), options);
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x000CC5A7 File Offset: 0x000CB7A7
		[NullableContext(1)]
		public static string ToBase64String(byte[] inArray, int offset, int length)
		{
			return Convert.ToBase64String(inArray, offset, length, Base64FormattingOptions.None);
		}

		// Token: 0x06000CBF RID: 3263 RVA: 0x000CC5B4 File Offset: 0x000CB7B4
		[NullableContext(1)]
		public static string ToBase64String(byte[] inArray, int offset, int length, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_Index);
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_OffsetLength);
			}
			return Convert.ToBase64String(new ReadOnlySpan<byte>(inArray, offset, length), options);
		}

		// Token: 0x06000CC0 RID: 3264 RVA: 0x000CC620 File Offset: 0x000CB820
		[NullableContext(0)]
		[return: Nullable(1)]
		public unsafe static string ToBase64String(ReadOnlySpan<byte> bytes, Base64FormattingOptions options = Base64FormattingOptions.None)
		{
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(SR.Format(SR.Arg_EnumIllegalVal, (int)options), "options");
			}
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
			string text = string.FastAllocateString(Convert.ToBase64_CalculateAndValidateOutputLength(bytes.Length, insertLineBreaks));
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* inData = reference;
				char* ptr;
				if (text == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = text.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* outChars = ptr;
				int num = Convert.ConvertToBase64Array(outChars, inData, 0, bytes.Length, insertLineBreaks);
				char* ptr2 = null;
			}
			return text;
		}

		// Token: 0x06000CC1 RID: 3265 RVA: 0x000CC6B0 File Offset: 0x000CB8B0
		[NullableContext(1)]
		public static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut)
		{
			return Convert.ToBase64CharArray(inArray, offsetIn, length, outArray, offsetOut, Base64FormattingOptions.None);
		}

		// Token: 0x06000CC2 RID: 3266 RVA: 0x000CC6C0 File Offset: 0x000CB8C0
		[NullableContext(1)]
		public unsafe static int ToBase64CharArray(byte[] inArray, int offsetIn, int length, char[] outArray, int offsetOut, Base64FormattingOptions options)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (outArray == null)
			{
				throw new ArgumentNullException("outArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_Index);
			}
			if (offsetIn < 0)
			{
				throw new ArgumentOutOfRangeException("offsetIn", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (offsetOut < 0)
			{
				throw new ArgumentOutOfRangeException("offsetOut", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(SR.Format(SR.Arg_EnumIllegalVal, (int)options), "options");
			}
			int num = inArray.Length;
			if (offsetIn > num - length)
			{
				throw new ArgumentOutOfRangeException("offsetIn", SR.ArgumentOutOfRange_OffsetLength);
			}
			if (num == 0)
			{
				return 0;
			}
			bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
			int num2 = outArray.Length;
			int num3 = Convert.ToBase64_CalculateAndValidateOutputLength(length, insertLineBreaks);
			if (offsetOut > num2 - num3)
			{
				throw new ArgumentOutOfRangeException("offsetOut", SR.ArgumentOutOfRange_OffsetOut);
			}
			int result;
			fixed (char* ptr = &outArray[offsetOut])
			{
				char* outChars = ptr;
				fixed (byte* ptr2 = &inArray[0])
				{
					byte* inData = ptr2;
					result = Convert.ConvertToBase64Array(outChars, inData, offsetIn, length, insertLineBreaks);
				}
			}
			return result;
		}

		// Token: 0x06000CC3 RID: 3267 RVA: 0x000CC7CC File Offset: 0x000CB9CC
		[NullableContext(0)]
		public unsafe static bool TryToBase64Chars(ReadOnlySpan<byte> bytes, Span<char> chars, out int charsWritten, Base64FormattingOptions options = Base64FormattingOptions.None)
		{
			if (options < Base64FormattingOptions.None || options > Base64FormattingOptions.InsertLineBreaks)
			{
				throw new ArgumentException(SR.Format(SR.Arg_EnumIllegalVal, (int)options), "options");
			}
			if (bytes.Length == 0)
			{
				charsWritten = 0;
				return true;
			}
			bool insertLineBreaks = options == Base64FormattingOptions.InsertLineBreaks;
			int num = Convert.ToBase64_CalculateAndValidateOutputLength(bytes.Length, insertLineBreaks);
			if (num > chars.Length)
			{
				charsWritten = 0;
				return false;
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* outChars = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* inData = reference2;
					charsWritten = Convert.ConvertToBase64Array(outChars, inData, 0, bytes.Length, insertLineBreaks);
					return true;
				}
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x000CC858 File Offset: 0x000CBA58
		private unsafe static int ConvertToBase64Array(char* outChars, byte* inData, int offset, int length, bool insertLineBreaks)
		{
			int num = length % 3;
			int num2 = offset + (length - num);
			int num3 = 0;
			int num4 = 0;
			fixed (char* ptr = &Convert.base64Table[0])
			{
				char* ptr2 = ptr;
				int i;
				for (i = offset; i < num2; i += 3)
				{
					if (insertLineBreaks)
					{
						if (num4 == 76)
						{
							outChars[num3++] = '\r';
							outChars[num3++] = '\n';
							num4 = 0;
						}
						num4 += 4;
					}
					outChars[num3] = ptr2[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr2[(int)(inData[i] & 3) << 4 | (inData[i + 1] & 240) >> 4];
					outChars[num3 + 2] = ptr2[(int)(inData[i + 1] & 15) << 2 | (inData[i + 2] & 192) >> 6];
					outChars[num3 + 3] = ptr2[inData[i + 2] & 63];
					num3 += 4;
				}
				i = num2;
				if (insertLineBreaks && num != 0 && num4 == 76)
				{
					outChars[num3++] = '\r';
					outChars[num3++] = '\n';
				}
				if (num != 1)
				{
					if (num == 2)
					{
						outChars[num3] = ptr2[(inData[i] & 252) >> 2];
						outChars[num3 + 1] = ptr2[(int)(inData[i] & 3) << 4 | (inData[i + 1] & 240) >> 4];
						outChars[num3 + 2] = ptr2[(inData[i + 1] & 15) << 2];
						outChars[num3 + 3] = ptr2[64];
						num3 += 4;
					}
				}
				else
				{
					outChars[num3] = ptr2[(inData[i] & 252) >> 2];
					outChars[num3 + 1] = ptr2[(inData[i] & 3) << 4];
					outChars[num3 + 2] = ptr2[64];
					outChars[num3 + 3] = ptr2[64];
					num3 += 4;
				}
			}
			return num3;
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x000CCA60 File Offset: 0x000CBC60
		private static int ToBase64_CalculateAndValidateOutputLength(int inputLength, bool insertLineBreaks)
		{
			long num = (long)inputLength / 3L * 4L;
			num += ((inputLength % 3 != 0) ? 4L : 0L);
			if (num == 0L)
			{
				return 0;
			}
			if (insertLineBreaks)
			{
				long num2 = num / 76L;
				if (num % 76L == 0L)
				{
					num2 -= 1L;
				}
				num += num2 * 2L;
			}
			if (num > 2147483647L)
			{
				throw new OutOfMemoryException();
			}
			return (int)num;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x000CCAB8 File Offset: 0x000CBCB8
		[NullableContext(1)]
		public unsafe static byte[] FromBase64String(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char* ptr;
			if (s == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = s.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* inputPtr = ptr;
			return Convert.FromBase64CharPtr(inputPtr, s.Length);
		}

		// Token: 0x06000CC7 RID: 3271 RVA: 0x000CCAF0 File Offset: 0x000CBCF0
		[NullableContext(0)]
		public static bool TryFromBase64String([Nullable(1)] string s, Span<byte> bytes, out int bytesWritten)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Convert.TryFromBase64Chars(s.AsSpan(), bytes, out bytesWritten);
		}

		// Token: 0x06000CC8 RID: 3272 RVA: 0x000CCB10 File Offset: 0x000CBD10
		[NullableContext(0)]
		public unsafe static bool TryFromBase64Chars(ReadOnlySpan<char> chars, Span<byte> bytes, out int bytesWritten)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)8], 4);
			Span<char> span2 = span;
			bytesWritten = 0;
			while (chars.Length != 0)
			{
				int start;
				int num;
				bool flag = Convert.TryDecodeFromUtf16(chars, bytes, out start, out num);
				bytesWritten += num;
				if (flag)
				{
					return true;
				}
				chars = chars.Slice(start);
				bytes = bytes.Slice(num);
				if (((char)(*chars[0])).IsSpace())
				{
					int num2 = 1;
					while (num2 != chars.Length && ((char)(*chars[num2])).IsSpace())
					{
						num2++;
					}
					chars = chars.Slice(num2);
					if (num % 3 != 0 && chars.Length != 0)
					{
						bytesWritten = 0;
						return false;
					}
				}
				else
				{
					int start2;
					int num3;
					Convert.CopyToTempBufferWithoutWhiteSpace(chars, span2, out start2, out num3);
					if ((num3 & 3) != 0)
					{
						bytesWritten = 0;
						return false;
					}
					span2 = span2.Slice(0, num3);
					int num4;
					int num5;
					if (!Convert.TryDecodeFromUtf16(span2, bytes, out num4, out num5))
					{
						bytesWritten = 0;
						return false;
					}
					bytesWritten += num5;
					chars = chars.Slice(start2);
					bytes = bytes.Slice(num5);
					if (num5 % 3 != 0)
					{
						for (int i = 0; i < chars.Length; i++)
						{
							if (!((char)(*chars[i])).IsSpace())
							{
								bytesWritten = 0;
								return false;
							}
						}
						return true;
					}
				}
			}
			return true;
		}

		// Token: 0x06000CC9 RID: 3273 RVA: 0x000CCC54 File Offset: 0x000CBE54
		private unsafe static void CopyToTempBufferWithoutWhiteSpace(ReadOnlySpan<char> chars, Span<char> tempBuffer, out int consumed, out int charsWritten)
		{
			charsWritten = 0;
			for (int i = 0; i < chars.Length; i++)
			{
				char c = (char)(*chars[i]);
				if (!c.IsSpace())
				{
					int num = charsWritten;
					charsWritten = num + 1;
					*tempBuffer[num] = c;
					if (charsWritten == tempBuffer.Length)
					{
						consumed = i + 1;
						return;
					}
				}
			}
			consumed = chars.Length;
		}

		// Token: 0x06000CCA RID: 3274 RVA: 0x000CCCB4 File Offset: 0x000CBEB4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsSpace(this char c)
		{
			return c == ' ' || c == '\t' || c == '\r' || c == '\n';
		}

		// Token: 0x06000CCB RID: 3275 RVA: 0x000CCCCC File Offset: 0x000CBECC
		[NullableContext(1)]
		public unsafe static byte[] FromBase64CharArray(char[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_Index);
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_OffsetLength);
			}
			if (inArray.Length == 0)
			{
				return Array.Empty<byte>();
			}
			fixed (char* ptr = &inArray[0])
			{
				char* ptr2 = ptr;
				return Convert.FromBase64CharPtr(ptr2 + offset, length);
			}
		}

		// Token: 0x06000CCC RID: 3276 RVA: 0x000CCD48 File Offset: 0x000CBF48
		private unsafe static byte[] FromBase64CharPtr(char* inputPtr, int inputLength)
		{
			while (inputLength > 0)
			{
				int num = (int)inputPtr[inputLength - 1];
				if (num != 32 && num != 10 && num != 13 && num != 9)
				{
					break;
				}
				inputLength--;
			}
			int num2 = Convert.FromBase64_ComputeResultLength(inputPtr, inputLength);
			byte[] array = new byte[num2];
			int num3;
			if (!Convert.TryFromBase64Chars(new ReadOnlySpan<char>((void*)inputPtr, inputLength), array, out num3))
			{
				throw new FormatException(SR.Format_BadBase64Char);
			}
			return array;
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000CCDB0 File Offset: 0x000CBFB0
		private unsafe static int FromBase64_ComputeResultLength(char* inputPtr, int inputLength)
		{
			char* ptr = inputPtr + inputLength;
			int num = inputLength;
			int num2 = 0;
			while (inputPtr < ptr)
			{
				uint num3 = (uint)(*inputPtr);
				inputPtr++;
				if (num3 <= 32U)
				{
					num--;
				}
				else if (num3 == 61U)
				{
					num--;
					num2++;
				}
			}
			if (num2 != 0)
			{
				if (num2 == 1)
				{
					num2 = 2;
				}
				else
				{
					if (num2 != 2)
					{
						throw new FormatException(SR.Format_BadBase64Char);
					}
					num2 = 1;
				}
			}
			return num / 4 * 3 + num2;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000CCE13 File Offset: 0x000CC013
		[NullableContext(1)]
		public static byte[] FromHexString(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return Convert.FromHexString(s.AsSpan());
		}

		// Token: 0x06000CCF RID: 3279 RVA: 0x000CCE30 File Offset: 0x000CC030
		[NullableContext(0)]
		[return: Nullable(1)]
		public static byte[] FromHexString(ReadOnlySpan<char> chars)
		{
			if (chars.Length == 0)
			{
				return Array.Empty<byte>();
			}
			if (chars.Length % 2 != 0)
			{
				throw new FormatException(SR.Format_BadHexLength);
			}
			byte[] array = GC.AllocateUninitializedArray<byte>(chars.Length >> 1, false);
			if (!HexConverter.TryDecodeFromUtf16(chars, array))
			{
				throw new FormatException(SR.Format_BadHexChar);
			}
			return array;
		}

		// Token: 0x06000CD0 RID: 3280 RVA: 0x000CCE8C File Offset: 0x000CC08C
		[NullableContext(1)]
		public static string ToHexString(byte[] inArray)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			return Convert.ToHexString(new ReadOnlySpan<byte>(inArray));
		}

		// Token: 0x06000CD1 RID: 3281 RVA: 0x000CCEA8 File Offset: 0x000CC0A8
		[NullableContext(1)]
		public static string ToHexString(byte[] inArray, int offset, int length)
		{
			if (inArray == null)
			{
				throw new ArgumentNullException("inArray");
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_Index);
			}
			if (offset < 0)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (offset > inArray.Length - length)
			{
				throw new ArgumentOutOfRangeException("offset", SR.ArgumentOutOfRange_OffsetLength);
			}
			return Convert.ToHexString(new ReadOnlySpan<byte>(inArray, offset, length));
		}

		// Token: 0x06000CD2 RID: 3282 RVA: 0x000CCF10 File Offset: 0x000CC110
		[NullableContext(0)]
		[return: Nullable(1)]
		public static string ToHexString(ReadOnlySpan<byte> bytes)
		{
			if (bytes.Length == 0)
			{
				return string.Empty;
			}
			if (bytes.Length > 1073741823)
			{
				throw new ArgumentOutOfRangeException("bytes", SR.ArgumentOutOfRange_InputTooLarge);
			}
			return HexConverter.ToString(bytes, HexConverter.Casing.Upper);
		}

		// Token: 0x040002B0 RID: 688
		internal static readonly Type[] ConvertTypes = new Type[]
		{
			typeof(Empty),
			typeof(object),
			typeof(DBNull),
			typeof(bool),
			typeof(char),
			typeof(sbyte),
			typeof(byte),
			typeof(short),
			typeof(ushort),
			typeof(int),
			typeof(uint),
			typeof(long),
			typeof(ulong),
			typeof(float),
			typeof(double),
			typeof(decimal),
			typeof(DateTime),
			typeof(object),
			typeof(string)
		};

		// Token: 0x040002B1 RID: 689
		private static readonly Type EnumType = typeof(Enum);

		// Token: 0x040002B2 RID: 690
		internal static readonly char[] base64Table = new char[]
		{
			'A',
			'B',
			'C',
			'D',
			'E',
			'F',
			'G',
			'H',
			'I',
			'J',
			'K',
			'L',
			'M',
			'N',
			'O',
			'P',
			'Q',
			'R',
			'S',
			'T',
			'U',
			'V',
			'W',
			'X',
			'Y',
			'Z',
			'a',
			'b',
			'c',
			'd',
			'e',
			'f',
			'g',
			'h',
			'i',
			'j',
			'k',
			'l',
			'm',
			'n',
			'o',
			'p',
			'q',
			'r',
			's',
			't',
			'u',
			'v',
			'w',
			'x',
			'y',
			'z',
			'0',
			'1',
			'2',
			'3',
			'4',
			'5',
			'6',
			'7',
			'8',
			'9',
			'+',
			'/',
			'='
		};

		// Token: 0x040002B3 RID: 691
		[Nullable(1)]
		public static readonly object DBNull = System.DBNull.Value;
	}
}
