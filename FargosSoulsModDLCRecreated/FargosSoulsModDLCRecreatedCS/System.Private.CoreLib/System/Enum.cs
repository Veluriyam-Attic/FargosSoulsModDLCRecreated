using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000065 RID: 101
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(1)]
	[Serializable]
	public abstract class Enum : ValueType, IComparable, IFormattable, IConvertible
	{
		// Token: 0x060002D8 RID: 728
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetEnumValuesAndNames(QCallTypeHandle enumType, ObjectHandleOnStack values, ObjectHandleOnStack names, Interop.BOOL getNames);

		// Token: 0x060002D9 RID: 729
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public override extern bool Equals(object obj);

		// Token: 0x060002DA RID: 730
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern object InternalBoxEnum(RuntimeType enumType, long value);

		// Token: 0x060002DB RID: 731
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern CorElementType InternalGetCorElementType();

		// Token: 0x060002DC RID: 732
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeType InternalGetUnderlyingType(RuntimeType enumType);

		// Token: 0x060002DD RID: 733
		[MethodImpl(MethodImplOptions.InternalCall)]
		private extern bool InternalHasFlag(Enum flags);

		// Token: 0x060002DE RID: 734 RVA: 0x000B36A8 File Offset: 0x000B28A8
		private static Enum.EnumInfo GetEnumInfo(RuntimeType enumType, bool getNames = true)
		{
			Enum.EnumInfo enumInfo = enumType.GenericCache as Enum.EnumInfo;
			if (enumInfo == null || (getNames && enumInfo.Names == null))
			{
				ulong[] values = null;
				string[] names = null;
				RuntimeTypeHandle typeHandleInternal = enumType.GetTypeHandleInternal();
				Enum.GetEnumValuesAndNames(new QCallTypeHandle(ref typeHandleInternal), ObjectHandleOnStack.Create<ulong[]>(ref values), ObjectHandleOnStack.Create<string[]>(ref names), getNames ? Interop.BOOL.TRUE : Interop.BOOL.FALSE);
				bool hasFlagsAttribute = enumType.IsDefined(typeof(FlagsAttribute), false);
				enumInfo = new Enum.EnumInfo(hasFlagsAttribute, values, names);
				enumType.GenericCache = enumInfo;
			}
			return enumInfo;
		}

		// Token: 0x060002DF RID: 735 RVA: 0x000B3720 File Offset: 0x000B2920
		private string ValueToString()
		{
			ref byte rawData = ref this.GetRawData();
			switch (this.InternalGetCorElementType())
			{
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
				return Unsafe.As<byte, bool>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_CHAR:
				return Unsafe.As<byte, char>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_I1:
				return Unsafe.As<byte, sbyte>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_U1:
				return rawData.ToString();
			case CorElementType.ELEMENT_TYPE_I2:
				return Unsafe.As<byte, short>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_U2:
				return Unsafe.As<byte, ushort>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_I4:
				return Unsafe.As<byte, int>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_U4:
				return Unsafe.As<byte, uint>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_I8:
				return Unsafe.As<byte, long>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_U8:
				return Unsafe.As<byte, ulong>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_R4:
				return Unsafe.As<byte, float>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_R8:
				return Unsafe.As<byte, double>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_I:
				return Unsafe.As<byte, IntPtr>(ref rawData).ToString();
			case CorElementType.ELEMENT_TYPE_U:
				return Unsafe.As<byte, UIntPtr>(ref rawData).ToString();
			}
			throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
		}

		// Token: 0x060002E0 RID: 736 RVA: 0x000B3884 File Offset: 0x000B2A84
		private unsafe string ValueToHexString()
		{
			ref byte rawData = ref this.GetRawData();
			switch (this.InternalGetCorElementType())
			{
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
				return Convert.ToByte(*Unsafe.As<byte, bool>(ref rawData)).ToString("X2", null);
			case CorElementType.ELEMENT_TYPE_CHAR:
			case CorElementType.ELEMENT_TYPE_I2:
			case CorElementType.ELEMENT_TYPE_U2:
				return Unsafe.As<byte, ushort>(ref rawData).ToString("X4", null);
			case CorElementType.ELEMENT_TYPE_I1:
			case CorElementType.ELEMENT_TYPE_U1:
				return rawData.ToString("X2", null);
			case CorElementType.ELEMENT_TYPE_I4:
			case CorElementType.ELEMENT_TYPE_U4:
				return Unsafe.As<byte, uint>(ref rawData).ToString("X8", null);
			case CorElementType.ELEMENT_TYPE_I8:
			case CorElementType.ELEMENT_TYPE_U8:
				return Unsafe.As<byte, ulong>(ref rawData).ToString("X16", null);
			default:
				throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
			}
		}

		// Token: 0x060002E1 RID: 737 RVA: 0x000B393C File Offset: 0x000B2B3C
		private static string ValueToHexString(object value)
		{
			string result;
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				result = Convert.ToByte((bool)value).ToString("X2", null);
				break;
			case TypeCode.Char:
				result = ((ushort)((char)value)).ToString("X4", null);
				break;
			case TypeCode.SByte:
				result = ((byte)((sbyte)value)).ToString("X2", null);
				break;
			case TypeCode.Byte:
				result = ((byte)value).ToString("X2", null);
				break;
			case TypeCode.Int16:
				result = ((ushort)((short)value)).ToString("X4", null);
				break;
			case TypeCode.UInt16:
				result = ((ushort)value).ToString("X4", null);
				break;
			case TypeCode.Int32:
				result = ((uint)((int)value)).ToString("X8", null);
				break;
			case TypeCode.UInt32:
				result = ((uint)value).ToString("X8", null);
				break;
			case TypeCode.Int64:
				result = ((ulong)((long)value)).ToString("X16", null);
				break;
			case TypeCode.UInt64:
				result = ((ulong)value).ToString("X16", null);
				break;
			default:
				throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
			}
			return result;
		}

		// Token: 0x060002E2 RID: 738 RVA: 0x000B3A91 File Offset: 0x000B2C91
		internal static string GetEnumName(RuntimeType enumType, ulong ulValue)
		{
			return Enum.GetEnumName(Enum.GetEnumInfo(enumType, true), ulValue);
		}

		// Token: 0x060002E3 RID: 739 RVA: 0x000B3AA0 File Offset: 0x000B2CA0
		private static string GetEnumName(Enum.EnumInfo enumInfo, ulong ulValue)
		{
			int num = Array.BinarySearch<ulong>(enumInfo.Values, ulValue);
			if (num >= 0)
			{
				return enumInfo.Names[num];
			}
			return null;
		}

		// Token: 0x060002E4 RID: 740 RVA: 0x000B3AC8 File Offset: 0x000B2CC8
		private static string InternalFormat(RuntimeType enumType, ulong value)
		{
			Enum.EnumInfo enumInfo = Enum.GetEnumInfo(enumType, true);
			if (!enumInfo.HasFlagsAttribute)
			{
				return Enum.GetEnumName(enumInfo, value);
			}
			return Enum.InternalFlagsFormat(enumType, enumInfo, value);
		}

		// Token: 0x060002E5 RID: 741 RVA: 0x000B3AF5 File Offset: 0x000B2CF5
		private static string InternalFlagsFormat(RuntimeType enumType, ulong result)
		{
			return Enum.InternalFlagsFormat(enumType, Enum.GetEnumInfo(enumType, true), result);
		}

		// Token: 0x060002E6 RID: 742 RVA: 0x000B3B08 File Offset: 0x000B2D08
		private unsafe static string InternalFlagsFormat(RuntimeType enumType, Enum.EnumInfo enumInfo, ulong resultValue)
		{
			string[] names = enumInfo.Names;
			ulong[] values = enumInfo.Values;
			if (resultValue == 0UL)
			{
				if (values.Length == 0 || values[0] != 0UL)
				{
					return "0";
				}
				return names[0];
			}
			else
			{
				Span<int> span = new Span<int>(stackalloc byte[(UIntPtr)256], 64);
				Span<int> span2 = span;
				int i;
				for (i = values.Length - 1; i >= 0; i--)
				{
					if (values[i] == resultValue)
					{
						return names[i];
					}
					if (values[i] < resultValue)
					{
						break;
					}
				}
				int num = 0;
				int num2 = 0;
				while (i >= 0)
				{
					ulong num3 = values[i];
					if (i == 0 && num3 == 0UL)
					{
						break;
					}
					if ((resultValue & num3) == num3)
					{
						resultValue -= num3;
						*span2[num2++] = i;
						checked
						{
							num += names[i].Length;
						}
					}
					i--;
				}
				if (resultValue != 0UL)
				{
					return null;
				}
				string text = string.FastAllocateString(checked(num + 2 * (num2 - 1)));
				Span<char> destination = new Span<char>(text.GetRawStringData(), text.Length);
				string text2 = names[*span2[--num2]];
				text2.AsSpan().CopyTo(destination);
				destination = destination.Slice(text2.Length);
				while (--num2 >= 0)
				{
					*destination[0] = ',';
					*destination[1] = ' ';
					destination = destination.Slice(2);
					text2 = names[*span2[num2]];
					text2.AsSpan().CopyTo(destination);
					destination = destination.Slice(text2.Length);
				}
				return text;
			}
		}

		// Token: 0x060002E7 RID: 743 RVA: 0x000B3C74 File Offset: 0x000B2E74
		internal static ulong ToUInt64(object value)
		{
			ulong result;
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				result = (ulong)Convert.ToByte((bool)value);
				break;
			case TypeCode.Char:
				result = (ulong)((char)value);
				break;
			case TypeCode.SByte:
				result = (ulong)((long)((sbyte)value));
				break;
			case TypeCode.Byte:
				result = (ulong)((byte)value);
				break;
			case TypeCode.Int16:
				result = (ulong)((long)((short)value));
				break;
			case TypeCode.UInt16:
				result = (ulong)((ushort)value);
				break;
			case TypeCode.Int32:
				result = (ulong)((long)((int)value));
				break;
			case TypeCode.UInt32:
				result = (ulong)((uint)value);
				break;
			case TypeCode.Int64:
				result = (ulong)((long)value);
				break;
			case TypeCode.UInt64:
				result = (ulong)value;
				break;
			default:
				throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
			}
			return result;
		}

		// Token: 0x060002E8 RID: 744 RVA: 0x000B3D2F File Offset: 0x000B2F2F
		[NullableContext(0)]
		[return: Nullable(2)]
		public static string GetName<TEnum>(TEnum value) where TEnum : struct, Enum
		{
			return Enum.GetName(typeof(TEnum), value);
		}

		// Token: 0x060002E9 RID: 745 RVA: 0x000B3D46 File Offset: 0x000B2F46
		[return: Nullable(2)]
		public static string GetName(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumName(value);
		}

		// Token: 0x060002EA RID: 746 RVA: 0x000B3D5D File Offset: 0x000B2F5D
		public static string[] GetNames<[Nullable(0)] TEnum>() where TEnum : struct, Enum
		{
			return Enum.GetNames(typeof(TEnum));
		}

		// Token: 0x060002EB RID: 747 RVA: 0x000B3D6E File Offset: 0x000B2F6E
		public static string[] GetNames(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumNames();
		}

		// Token: 0x060002EC RID: 748 RVA: 0x000B3D84 File Offset: 0x000B2F84
		internal static string[] InternalGetNames(RuntimeType enumType)
		{
			return Enum.GetEnumInfo(enumType, true).Names;
		}

		// Token: 0x060002ED RID: 749 RVA: 0x000B3D92 File Offset: 0x000B2F92
		public static Type GetUnderlyingType(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumUnderlyingType();
		}

		// Token: 0x060002EE RID: 750 RVA: 0x000B3DAE File Offset: 0x000B2FAE
		[NullableContext(0)]
		[return: Nullable(new byte[]
		{
			1,
			0
		})]
		public static TEnum[] GetValues<TEnum>() where TEnum : struct, Enum
		{
			return (TEnum[])Enum.GetValues(typeof(TEnum));
		}

		// Token: 0x060002EF RID: 751 RVA: 0x000B3DC4 File Offset: 0x000B2FC4
		public static Array GetValues(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.GetEnumValues();
		}

		// Token: 0x060002F0 RID: 752 RVA: 0x000B3DDC File Offset: 0x000B2FDC
		[Intrinsic]
		public bool HasFlag(Enum flag)
		{
			if (flag == null)
			{
				throw new ArgumentNullException("flag");
			}
			if (!base.GetType().IsEquivalentTo(flag.GetType()))
			{
				throw new ArgumentException(SR.Format(SR.Argument_EnumTypeDoesNotMatch, flag.GetType(), base.GetType()));
			}
			return this.InternalHasFlag(flag);
		}

		// Token: 0x060002F1 RID: 753 RVA: 0x000B3E2D File Offset: 0x000B302D
		internal static ulong[] InternalGetValues(RuntimeType enumType)
		{
			return Enum.GetEnumInfo(enumType, false).Values;
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x000B3E3C File Offset: 0x000B303C
		[NullableContext(0)]
		public static bool IsDefined<TEnum>(TEnum value) where TEnum : struct, Enum
		{
			RuntimeType enumType = (RuntimeType)typeof(TEnum);
			ulong[] array = Enum.InternalGetValues(enumType);
			ulong value2 = Enum.ToUInt64(value);
			return Array.BinarySearch<ulong>(array, value2) >= 0;
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x000B3E79 File Offset: 0x000B3079
		public static bool IsDefined(Type enumType, object value)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			return enumType.IsEnumDefined(value);
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x000B3E90 File Offset: 0x000B3090
		public static object Parse(Type enumType, string value)
		{
			return Enum.Parse(enumType, value, false);
		}

		// Token: 0x060002F5 RID: 757 RVA: 0x000B3E9C File Offset: 0x000B309C
		public static object Parse(Type enumType, string value, bool ignoreCase)
		{
			object result;
			bool flag = Enum.TryParse(enumType, value, ignoreCase, true, out result);
			return result;
		}

		// Token: 0x060002F6 RID: 758 RVA: 0x000B3EB6 File Offset: 0x000B30B6
		[NullableContext(0)]
		public static TEnum Parse<TEnum>([Nullable(1)] string value) where TEnum : struct
		{
			return Enum.Parse<TEnum>(value, false);
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x000B3EC0 File Offset: 0x000B30C0
		[NullableContext(0)]
		public static TEnum Parse<TEnum>([Nullable(1)] string value, bool ignoreCase) where TEnum : struct
		{
			TEnum result;
			bool flag = Enum.TryParse<TEnum>(value, ignoreCase, true, out result);
			return result;
		}

		// Token: 0x060002F8 RID: 760 RVA: 0x000B3ED9 File Offset: 0x000B30D9
		[NullableContext(2)]
		public static bool TryParse([Nullable(1)] Type enumType, string value, out object result)
		{
			return Enum.TryParse(enumType, value, false, out result);
		}

		// Token: 0x060002F9 RID: 761 RVA: 0x000B3EE4 File Offset: 0x000B30E4
		[NullableContext(2)]
		public static bool TryParse([Nullable(1)] Type enumType, string value, bool ignoreCase, out object result)
		{
			return Enum.TryParse(enumType, value, ignoreCase, false, out result);
		}

		// Token: 0x060002FA RID: 762 RVA: 0x000B3EF0 File Offset: 0x000B30F0
		private static bool TryParse(Type enumType, string value, bool ignoreCase, bool throwOnFailure, out object result)
		{
			RuntimeType runtimeType = Enum.ValidateRuntimeType(enumType);
			ReadOnlySpan<char> value2 = value.AsSpan().TrimStart();
			if (value2.Length == 0)
			{
				if (throwOnFailure)
				{
					throw (value == null) ? new ArgumentNullException("value") : new ArgumentException(SR.Arg_MustContainEnumInfo, "value");
				}
				result = null;
				return false;
			}
			else
			{
				switch (Type.GetTypeCode(runtimeType))
				{
				case TypeCode.SByte:
				{
					int num;
					bool flag = Enum.TryParseInt32Enum(runtimeType, value, value2, -128, 127, ignoreCase, throwOnFailure, TypeCode.SByte, out num);
					result = (flag ? Enum.InternalBoxEnum(runtimeType, (long)num) : null);
					return flag;
				}
				case TypeCode.Byte:
				{
					uint num2;
					bool flag = Enum.TryParseUInt32Enum(runtimeType, value, value2, 255U, ignoreCase, throwOnFailure, TypeCode.Byte, out num2);
					result = (flag ? Enum.InternalBoxEnum(runtimeType, (long)((ulong)num2)) : null);
					return flag;
				}
				case TypeCode.Int16:
				{
					int num;
					bool flag = Enum.TryParseInt32Enum(runtimeType, value, value2, -32768, 32767, ignoreCase, throwOnFailure, TypeCode.Int16, out num);
					result = (flag ? Enum.InternalBoxEnum(runtimeType, (long)num) : null);
					return flag;
				}
				case TypeCode.UInt16:
				{
					uint num2;
					bool flag = Enum.TryParseUInt32Enum(runtimeType, value, value2, 65535U, ignoreCase, throwOnFailure, TypeCode.UInt16, out num2);
					result = (flag ? Enum.InternalBoxEnum(runtimeType, (long)((ulong)num2)) : null);
					return flag;
				}
				case TypeCode.Int32:
				{
					int num;
					bool flag = Enum.TryParseInt32Enum(runtimeType, value, value2, int.MinValue, int.MaxValue, ignoreCase, throwOnFailure, TypeCode.Int32, out num);
					result = (flag ? Enum.InternalBoxEnum(runtimeType, (long)num) : null);
					return flag;
				}
				case TypeCode.UInt32:
				{
					uint num2;
					bool flag = Enum.TryParseUInt32Enum(runtimeType, value, value2, uint.MaxValue, ignoreCase, throwOnFailure, TypeCode.UInt32, out num2);
					result = (flag ? Enum.InternalBoxEnum(runtimeType, (long)((ulong)num2)) : null);
					return flag;
				}
				case TypeCode.Int64:
				{
					long value3;
					bool flag = Enum.TryParseInt64Enum(runtimeType, value, value2, ignoreCase, throwOnFailure, out value3);
					result = (flag ? Enum.InternalBoxEnum(runtimeType, value3) : null);
					return flag;
				}
				case TypeCode.UInt64:
				{
					ulong value4;
					bool flag = Enum.TryParseUInt64Enum(runtimeType, value, value2, ignoreCase, throwOnFailure, out value4);
					result = (flag ? Enum.InternalBoxEnum(runtimeType, (long)value4) : null);
					return flag;
				}
				default:
					return Enum.TryParseRareEnum(runtimeType, value, value2, ignoreCase, throwOnFailure, out result);
				}
			}
		}

		// Token: 0x060002FB RID: 763 RVA: 0x000B40C6 File Offset: 0x000B32C6
		[NullableContext(0)]
		public static bool TryParse<TEnum>([Nullable(2)] string value, out TEnum result) where TEnum : struct
		{
			return Enum.TryParse<TEnum>(value, false, out result);
		}

		// Token: 0x060002FC RID: 764 RVA: 0x000B40D0 File Offset: 0x000B32D0
		[NullableContext(0)]
		public static bool TryParse<TEnum>([Nullable(2)] string value, bool ignoreCase, out TEnum result) where TEnum : struct
		{
			return Enum.TryParse<TEnum>(value, ignoreCase, false, out result);
		}

		// Token: 0x060002FD RID: 765 RVA: 0x000B40DC File Offset: 0x000B32DC
		private unsafe static bool TryParse<TEnum>(string value, bool ignoreCase, bool throwOnFailure, out TEnum result) where TEnum : struct
		{
			if (!typeof(TEnum).IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "TEnum");
			}
			ReadOnlySpan<char> value2 = value.AsSpan().TrimStart();
			if (value2.Length == 0)
			{
				if (throwOnFailure)
				{
					throw (value == null) ? new ArgumentNullException("value") : new ArgumentException(SR.Arg_MustContainEnumInfo, "value");
				}
				result = default(TEnum);
				return false;
			}
			else
			{
				RuntimeType enumType = (RuntimeType)typeof(TEnum);
				switch (Type.GetTypeCode(typeof(TEnum)))
				{
				case TypeCode.SByte:
				{
					int num;
					bool flag = Enum.TryParseInt32Enum(enumType, value, value2, -128, 127, ignoreCase, throwOnFailure, TypeCode.SByte, out num);
					sbyte b = (sbyte)num;
					result = *Unsafe.As<sbyte, TEnum>(ref b);
					return flag;
				}
				case TypeCode.Byte:
				{
					uint num2;
					bool flag = Enum.TryParseUInt32Enum(enumType, value, value2, 255U, ignoreCase, throwOnFailure, TypeCode.Byte, out num2);
					byte b2 = (byte)num2;
					result = *Unsafe.As<byte, TEnum>(ref b2);
					return flag;
				}
				case TypeCode.Int16:
				{
					int num;
					bool flag = Enum.TryParseInt32Enum(enumType, value, value2, -32768, 32767, ignoreCase, throwOnFailure, TypeCode.Int16, out num);
					short num3 = (short)num;
					result = *Unsafe.As<short, TEnum>(ref num3);
					return flag;
				}
				case TypeCode.UInt16:
				{
					uint num2;
					bool flag = Enum.TryParseUInt32Enum(enumType, value, value2, 65535U, ignoreCase, throwOnFailure, TypeCode.UInt16, out num2);
					ushort num4 = (ushort)num2;
					result = *Unsafe.As<ushort, TEnum>(ref num4);
					return flag;
				}
				case TypeCode.Int32:
				{
					int num;
					bool flag = Enum.TryParseInt32Enum(enumType, value, value2, int.MinValue, int.MaxValue, ignoreCase, throwOnFailure, TypeCode.Int32, out num);
					result = *Unsafe.As<int, TEnum>(ref num);
					return flag;
				}
				case TypeCode.UInt32:
				{
					uint num2;
					bool flag = Enum.TryParseUInt32Enum(enumType, value, value2, uint.MaxValue, ignoreCase, throwOnFailure, TypeCode.UInt32, out num2);
					result = *Unsafe.As<uint, TEnum>(ref num2);
					return flag;
				}
				case TypeCode.Int64:
				{
					long num5;
					bool flag = Enum.TryParseInt64Enum(enumType, value, value2, ignoreCase, throwOnFailure, out num5);
					result = *Unsafe.As<long, TEnum>(ref num5);
					return flag;
				}
				case TypeCode.UInt64:
				{
					ulong num6;
					bool flag = Enum.TryParseUInt64Enum(enumType, value, value2, ignoreCase, throwOnFailure, out num6);
					result = *Unsafe.As<ulong, TEnum>(ref num6);
					return flag;
				}
				default:
				{
					object obj;
					bool flag = Enum.TryParseRareEnum(enumType, value, value2, ignoreCase, throwOnFailure, out obj);
					result = (flag ? ((TEnum)((object)obj)) : default(TEnum));
					return flag;
				}
				}
			}
		}

		// Token: 0x060002FE RID: 766 RVA: 0x000B4310 File Offset: 0x000B3510
		private unsafe static bool TryParseInt32Enum(RuntimeType enumType, string originalValueString, ReadOnlySpan<char> value, int minInclusive, int maxInclusive, bool ignoreCase, bool throwOnFailure, TypeCode type, out int result)
		{
			Number.ParsingStatus parsingStatus = Number.ParsingStatus.OK;
			if (Enum.StartsNumber((char)(*value[0])))
			{
				parsingStatus = Number.TryParseInt32IntegerStyle(value, NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat, out result);
				if (parsingStatus == Number.ParsingStatus.OK)
				{
					if (result - minInclusive <= maxInclusive - minInclusive)
					{
						return true;
					}
					parsingStatus = Number.ParsingStatus.Overflow;
				}
			}
			ulong num;
			if (parsingStatus == Number.ParsingStatus.Overflow)
			{
				if (throwOnFailure)
				{
					Number.ThrowOverflowException(type);
				}
			}
			else if (Enum.TryParseByName(enumType, originalValueString, value, ignoreCase, throwOnFailure, out num))
			{
				result = (int)num;
				return true;
			}
			result = 0;
			return false;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000B4384 File Offset: 0x000B3584
		private unsafe static bool TryParseUInt32Enum(RuntimeType enumType, string originalValueString, ReadOnlySpan<char> value, uint maxInclusive, bool ignoreCase, bool throwOnFailure, TypeCode type, out uint result)
		{
			Number.ParsingStatus parsingStatus = Number.ParsingStatus.OK;
			if (Enum.StartsNumber((char)(*value[0])))
			{
				parsingStatus = Number.TryParseUInt32IntegerStyle(value, NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat, out result);
				if (parsingStatus == Number.ParsingStatus.OK)
				{
					if (result <= maxInclusive)
					{
						return true;
					}
					parsingStatus = Number.ParsingStatus.Overflow;
				}
			}
			ulong num;
			if (parsingStatus == Number.ParsingStatus.Overflow)
			{
				if (throwOnFailure)
				{
					Number.ThrowOverflowException(type);
				}
			}
			else if (Enum.TryParseByName(enumType, originalValueString, value, ignoreCase, throwOnFailure, out num))
			{
				result = (uint)num;
				return true;
			}
			result = 0U;
			return false;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000B43F4 File Offset: 0x000B35F4
		private unsafe static bool TryParseInt64Enum(RuntimeType enumType, string originalValueString, ReadOnlySpan<char> value, bool ignoreCase, bool throwOnFailure, out long result)
		{
			Number.ParsingStatus parsingStatus = Number.ParsingStatus.OK;
			if (Enum.StartsNumber((char)(*value[0])))
			{
				parsingStatus = Number.TryParseInt64IntegerStyle(value, NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat, out result);
				if (parsingStatus == Number.ParsingStatus.OK)
				{
					return true;
				}
			}
			ulong num;
			if (parsingStatus == Number.ParsingStatus.Overflow)
			{
				if (throwOnFailure)
				{
					Number.ThrowOverflowException(TypeCode.Int64);
				}
			}
			else if (Enum.TryParseByName(enumType, originalValueString, value, ignoreCase, throwOnFailure, out num))
			{
				result = (long)num;
				return true;
			}
			result = 0L;
			return false;
		}

		// Token: 0x06000301 RID: 769 RVA: 0x000B4458 File Offset: 0x000B3658
		private unsafe static bool TryParseUInt64Enum(RuntimeType enumType, string originalValueString, ReadOnlySpan<char> value, bool ignoreCase, bool throwOnFailure, out ulong result)
		{
			Number.ParsingStatus parsingStatus = Number.ParsingStatus.OK;
			if (Enum.StartsNumber((char)(*value[0])))
			{
				parsingStatus = Number.TryParseUInt64IntegerStyle(value, NumberStyles.AllowTrailingWhite | NumberStyles.AllowLeadingSign, CultureInfo.InvariantCulture.NumberFormat, out result);
				if (parsingStatus == Number.ParsingStatus.OK)
				{
					return true;
				}
			}
			if (parsingStatus == Number.ParsingStatus.Overflow)
			{
				if (throwOnFailure)
				{
					Number.ThrowOverflowException(TypeCode.UInt64);
				}
			}
			else if (Enum.TryParseByName(enumType, originalValueString, value, ignoreCase, throwOnFailure, out result))
			{
				return true;
			}
			result = 0UL;
			return false;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000B44B8 File Offset: 0x000B36B8
		private unsafe static bool TryParseRareEnum(RuntimeType enumType, string originalValueString, ReadOnlySpan<char> value, bool ignoreCase, bool throwOnFailure, [NotNullWhen(true)] out object result)
		{
			if (Enum.StartsNumber((char)(*value[0])))
			{
				Type underlyingType = Enum.GetUnderlyingType(enumType);
				try
				{
					result = Enum.ToObject(enumType, Convert.ChangeType(value.ToString(), underlyingType, CultureInfo.InvariantCulture));
					return true;
				}
				catch (FormatException)
				{
				}
				catch when (endfilter(!throwOnFailure > false))
				{
					result = null;
					return false;
				}
			}
			ulong value2;
			if (Enum.TryParseByName(enumType, originalValueString, value, ignoreCase, throwOnFailure, out value2))
			{
				try
				{
					result = Enum.ToObject(enumType, value2);
					return true;
				}
				catch when (endfilter(!throwOnFailure > false))
				{
				}
			}
			result = null;
			return false;
		}

		// Token: 0x06000303 RID: 771 RVA: 0x000B4574 File Offset: 0x000B3774
		private static bool TryParseByName(RuntimeType enumType, string originalValueString, ReadOnlySpan<char> value, bool ignoreCase, bool throwOnFailure, out ulong result)
		{
			Enum.EnumInfo enumInfo = Enum.GetEnumInfo(enumType, true);
			string[] names = enumInfo.Names;
			ulong[] values = enumInfo.Values;
			bool flag = true;
			ulong num = 0UL;
			while (value.Length > 0)
			{
				int num2 = value.IndexOf(',');
				ReadOnlySpan<char> span;
				if (num2 == -1)
				{
					span = value.Trim();
					value = default(ReadOnlySpan<char>);
				}
				else
				{
					if (num2 == value.Length - 1)
					{
						flag = false;
						break;
					}
					span = value.Slice(0, num2).Trim();
					value = value.Slice(num2 + 1);
				}
				bool flag2 = false;
				if (ignoreCase)
				{
					for (int i = 0; i < names.Length; i++)
					{
						if (span.EqualsOrdinalIgnoreCase(names[i]))
						{
							num |= values[i];
							flag2 = true;
							break;
						}
					}
				}
				else
				{
					for (int j = 0; j < names.Length; j++)
					{
						if (span.EqualsOrdinal(names[j]))
						{
							num |= values[j];
							flag2 = true;
							break;
						}
					}
				}
				if (!flag2)
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				result = num;
				return true;
			}
			if (throwOnFailure)
			{
				throw new ArgumentException(SR.Format(SR.Arg_EnumValueNotFound, originalValueString));
			}
			result = 0UL;
			return false;
		}

		// Token: 0x06000304 RID: 772 RVA: 0x000B469D File Offset: 0x000B389D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool StartsNumber(char c)
		{
			return char.IsInRange(c, '0', '9') || c == '-' || c == '+';
		}

		// Token: 0x06000305 RID: 773 RVA: 0x000B46B8 File Offset: 0x000B38B8
		public static object ToObject(Type enumType, object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			object result;
			switch (Convert.GetTypeCode(value))
			{
			case TypeCode.Boolean:
				result = Enum.ToObject(enumType, (bool)value);
				break;
			case TypeCode.Char:
				result = Enum.ToObject(enumType, (char)value);
				break;
			case TypeCode.SByte:
				result = Enum.ToObject(enumType, (sbyte)value);
				break;
			case TypeCode.Byte:
				result = Enum.ToObject(enumType, (byte)value);
				break;
			case TypeCode.Int16:
				result = Enum.ToObject(enumType, (short)value);
				break;
			case TypeCode.UInt16:
				result = Enum.ToObject(enumType, (ushort)value);
				break;
			case TypeCode.Int32:
				result = Enum.ToObject(enumType, (int)value);
				break;
			case TypeCode.UInt32:
				result = Enum.ToObject(enumType, (uint)value);
				break;
			case TypeCode.Int64:
				result = Enum.ToObject(enumType, (long)value);
				break;
			case TypeCode.UInt64:
				result = Enum.ToObject(enumType, (ulong)value);
				break;
			default:
				throw new ArgumentException(SR.Arg_MustBeEnumBaseTypeOrEnum, "value");
			}
			return result;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000B47BC File Offset: 0x000B39BC
		public static string Format(Type enumType, object value, string format)
		{
			RuntimeType enumType2 = Enum.ValidateRuntimeType(enumType);
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (format == null)
			{
				throw new ArgumentNullException("format");
			}
			Type type = value.GetType();
			if (type.IsEnum)
			{
				if (!type.IsEquivalentTo(enumType))
				{
					throw new ArgumentException(SR.Format(SR.Arg_EnumAndObjectMustBeSameType, type, enumType));
				}
				if (format.Length != 1)
				{
					throw new FormatException(SR.Format_InvalidEnumFormatSpecification);
				}
				return ((Enum)value).ToString(format);
			}
			else
			{
				Type underlyingType = Enum.GetUnderlyingType(enumType);
				if (type != underlyingType)
				{
					throw new ArgumentException(SR.Format(SR.Arg_EnumFormatUnderlyingTypeAndObjectMustBeSameType, type, underlyingType));
				}
				if (format.Length == 1)
				{
					char c = format[0];
					if (c <= 'X')
					{
						switch (c)
						{
						case 'D':
							goto IL_100;
						case 'E':
							goto IL_125;
						case 'F':
							goto IL_10E;
						case 'G':
							break;
						default:
							if (c != 'X')
							{
								goto IL_125;
							}
							goto IL_107;
						}
					}
					else
					{
						switch (c)
						{
						case 'd':
							goto IL_100;
						case 'e':
							goto IL_125;
						case 'f':
							goto IL_10E;
						case 'g':
							break;
						default:
							if (c != 'x')
							{
								goto IL_125;
							}
							goto IL_107;
						}
					}
					return Enum.GetEnumName(enumType2, Enum.ToUInt64(value)) ?? value.ToString();
					IL_100:
					return value.ToString();
					IL_107:
					return Enum.ValueToHexString(value);
					IL_10E:
					return Enum.InternalFlagsFormat(enumType2, Enum.ToUInt64(value)) ?? value.ToString();
				}
				IL_125:
				throw new FormatException(SR.Format_InvalidEnumFormatSpecification);
			}
		}

		// Token: 0x06000307 RID: 775 RVA: 0x000B48F8 File Offset: 0x000B3AF8
		internal unsafe object GetValue()
		{
			ref byte rawData = ref this.GetRawData();
			switch (this.InternalGetCorElementType())
			{
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
				return *Unsafe.As<byte, bool>(ref rawData);
			case CorElementType.ELEMENT_TYPE_CHAR:
				return *Unsafe.As<byte, char>(ref rawData);
			case CorElementType.ELEMENT_TYPE_I1:
				return *Unsafe.As<byte, sbyte>(ref rawData);
			case CorElementType.ELEMENT_TYPE_U1:
				return rawData;
			case CorElementType.ELEMENT_TYPE_I2:
				return *Unsafe.As<byte, short>(ref rawData);
			case CorElementType.ELEMENT_TYPE_U2:
				return *Unsafe.As<byte, ushort>(ref rawData);
			case CorElementType.ELEMENT_TYPE_I4:
				return *Unsafe.As<byte, int>(ref rawData);
			case CorElementType.ELEMENT_TYPE_U4:
				return *Unsafe.As<byte, uint>(ref rawData);
			case CorElementType.ELEMENT_TYPE_I8:
				return *Unsafe.As<byte, long>(ref rawData);
			case CorElementType.ELEMENT_TYPE_U8:
				return *Unsafe.As<byte, ulong>(ref rawData);
			case CorElementType.ELEMENT_TYPE_R4:
				return *Unsafe.As<byte, float>(ref rawData);
			case CorElementType.ELEMENT_TYPE_R8:
				return *Unsafe.As<byte, double>(ref rawData);
			case CorElementType.ELEMENT_TYPE_I:
				return *Unsafe.As<byte, IntPtr>(ref rawData);
			case CorElementType.ELEMENT_TYPE_U:
				return *Unsafe.As<byte, UIntPtr>(ref rawData);
			}
			throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
		}

		// Token: 0x06000308 RID: 776 RVA: 0x000B4A6C File Offset: 0x000B3C6C
		private unsafe ulong ToUInt64()
		{
			ref byte rawData = ref this.GetRawData();
			switch (this.InternalGetCorElementType())
			{
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
				return Convert.ToUInt64(*Unsafe.As<byte, bool>(ref rawData), CultureInfo.InvariantCulture);
			case CorElementType.ELEMENT_TYPE_CHAR:
			case CorElementType.ELEMENT_TYPE_U2:
				return (ulong)(*Unsafe.As<byte, ushort>(ref rawData));
			case CorElementType.ELEMENT_TYPE_I1:
				return (ulong)((long)(*Unsafe.As<byte, sbyte>(ref rawData)));
			case CorElementType.ELEMENT_TYPE_U1:
				return (ulong)rawData;
			case CorElementType.ELEMENT_TYPE_I2:
				return (ulong)((long)(*Unsafe.As<byte, short>(ref rawData)));
			case CorElementType.ELEMENT_TYPE_I4:
				return (ulong)((long)(*Unsafe.As<byte, int>(ref rawData)));
			case CorElementType.ELEMENT_TYPE_U4:
			case CorElementType.ELEMENT_TYPE_R4:
				return (ulong)(*Unsafe.As<byte, uint>(ref rawData));
			case CorElementType.ELEMENT_TYPE_I8:
				return (ulong)(*Unsafe.As<byte, long>(ref rawData));
			case CorElementType.ELEMENT_TYPE_U8:
			case CorElementType.ELEMENT_TYPE_R8:
				return *Unsafe.As<byte, ulong>(ref rawData);
			case CorElementType.ELEMENT_TYPE_I:
				return (ulong)((long)(*Unsafe.As<byte, IntPtr>(ref rawData)));
			case CorElementType.ELEMENT_TYPE_U:
				return (ulong)(*Unsafe.As<byte, UIntPtr>(ref rawData));
			}
			throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
		}

		// Token: 0x06000309 RID: 777 RVA: 0x000B4B70 File Offset: 0x000B3D70
		public override int GetHashCode()
		{
			ref byte rawData = ref this.GetRawData();
			switch (this.InternalGetCorElementType())
			{
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
				return Unsafe.As<byte, bool>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_CHAR:
				return Unsafe.As<byte, char>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_I1:
				return Unsafe.As<byte, sbyte>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_U1:
				return rawData.GetHashCode();
			case CorElementType.ELEMENT_TYPE_I2:
				return Unsafe.As<byte, short>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_U2:
				return Unsafe.As<byte, ushort>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_I4:
				return Unsafe.As<byte, int>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_U4:
				return Unsafe.As<byte, uint>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_I8:
				return Unsafe.As<byte, long>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_U8:
				return Unsafe.As<byte, ulong>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_R4:
				return Unsafe.As<byte, float>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_R8:
				return Unsafe.As<byte, double>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_I:
				return Unsafe.As<byte, IntPtr>(ref rawData).GetHashCode();
			case CorElementType.ELEMENT_TYPE_U:
				return Unsafe.As<byte, UIntPtr>(ref rawData).GetHashCode();
			}
			throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
		}

		// Token: 0x0600030A RID: 778 RVA: 0x000B4CD2 File Offset: 0x000B3ED2
		public override string ToString()
		{
			return Enum.InternalFormat((RuntimeType)base.GetType(), this.ToUInt64()) ?? this.ValueToString();
		}

		// Token: 0x0600030B RID: 779 RVA: 0x000B4CF4 File Offset: 0x000B3EF4
		[NullableContext(2)]
		public unsafe int CompareTo(object target)
		{
			if (target == this)
			{
				return 0;
			}
			if (target == null)
			{
				return 1;
			}
			if (base.GetType() != target.GetType())
			{
				throw new ArgumentException(SR.Format(SR.Arg_EnumAndObjectMustBeSameType, target.GetType(), base.GetType()));
			}
			ref byte rawData = ref this.GetRawData();
			ref byte rawData2 = ref target.GetRawData();
			switch (this.InternalGetCorElementType())
			{
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
			case CorElementType.ELEMENT_TYPE_U1:
				return rawData.CompareTo(rawData2);
			case CorElementType.ELEMENT_TYPE_CHAR:
			case CorElementType.ELEMENT_TYPE_U2:
				return Unsafe.As<byte, ushort>(ref rawData).CompareTo(*Unsafe.As<byte, ushort>(ref rawData2));
			case CorElementType.ELEMENT_TYPE_I1:
				return Unsafe.As<byte, sbyte>(ref rawData).CompareTo(*Unsafe.As<byte, sbyte>(ref rawData2));
			case CorElementType.ELEMENT_TYPE_I2:
				return Unsafe.As<byte, short>(ref rawData).CompareTo(*Unsafe.As<byte, short>(ref rawData2));
			case CorElementType.ELEMENT_TYPE_I4:
				return Unsafe.As<byte, int>(ref rawData).CompareTo(*Unsafe.As<byte, int>(ref rawData2));
			case CorElementType.ELEMENT_TYPE_U4:
				return Unsafe.As<byte, uint>(ref rawData).CompareTo(*Unsafe.As<byte, uint>(ref rawData2));
			case CorElementType.ELEMENT_TYPE_I8:
			case CorElementType.ELEMENT_TYPE_I:
				return Unsafe.As<byte, long>(ref rawData).CompareTo(*Unsafe.As<byte, long>(ref rawData2));
			case CorElementType.ELEMENT_TYPE_U8:
			case CorElementType.ELEMENT_TYPE_U:
				return Unsafe.As<byte, ulong>(ref rawData).CompareTo(*Unsafe.As<byte, ulong>(ref rawData2));
			case CorElementType.ELEMENT_TYPE_R4:
				return Unsafe.As<byte, float>(ref rawData).CompareTo(*Unsafe.As<byte, float>(ref rawData2));
			case CorElementType.ELEMENT_TYPE_R8:
				return Unsafe.As<byte, double>(ref rawData).CompareTo(*Unsafe.As<byte, double>(ref rawData2));
			}
			throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
		}

		// Token: 0x0600030C RID: 780 RVA: 0x000B4E7B File Offset: 0x000B407B
		[NullableContext(2)]
		[Obsolete("The provider argument is not used. Please use ToString(String).")]
		[return: Nullable(1)]
		public string ToString(string format, IFormatProvider provider)
		{
			return this.ToString(format);
		}

		// Token: 0x0600030D RID: 781 RVA: 0x000B4E84 File Offset: 0x000B4084
		public string ToString([Nullable(2)] string format)
		{
			if (string.IsNullOrEmpty(format))
			{
				return this.ToString();
			}
			if (format.Length == 1)
			{
				char c = format[0];
				if (c <= 'X')
				{
					switch (c)
					{
					case 'D':
						goto IL_6F;
					case 'E':
						goto IL_9E;
					case 'F':
						goto IL_7D;
					case 'G':
						break;
					default:
						if (c != 'X')
						{
							goto IL_9E;
						}
						goto IL_76;
					}
				}
				else
				{
					switch (c)
					{
					case 'd':
						goto IL_6F;
					case 'e':
						goto IL_9E;
					case 'f':
						goto IL_7D;
					case 'g':
						break;
					default:
						if (c != 'x')
						{
							goto IL_9E;
						}
						goto IL_76;
					}
				}
				return this.ToString();
				IL_6F:
				return this.ValueToString();
				IL_76:
				return this.ValueToHexString();
				IL_7D:
				return Enum.InternalFlagsFormat((RuntimeType)base.GetType(), this.ToUInt64()) ?? this.ValueToString();
			}
			IL_9E:
			throw new FormatException(SR.Format_InvalidEnumFormatSpecification);
		}

		// Token: 0x0600030E RID: 782 RVA: 0x000B4F39 File Offset: 0x000B4139
		[Obsolete("The provider argument is not used. Please use ToString().")]
		public string ToString([Nullable(2)] IFormatProvider provider)
		{
			return this.ToString();
		}

		// Token: 0x0600030F RID: 783 RVA: 0x000B4F44 File Offset: 0x000B4144
		public TypeCode GetTypeCode()
		{
			TypeCode result;
			switch (this.InternalGetCorElementType())
			{
			case CorElementType.ELEMENT_TYPE_BOOLEAN:
				result = TypeCode.Boolean;
				break;
			case CorElementType.ELEMENT_TYPE_CHAR:
				result = TypeCode.Char;
				break;
			case CorElementType.ELEMENT_TYPE_I1:
				result = TypeCode.SByte;
				break;
			case CorElementType.ELEMENT_TYPE_U1:
				result = TypeCode.Byte;
				break;
			case CorElementType.ELEMENT_TYPE_I2:
				result = TypeCode.Int16;
				break;
			case CorElementType.ELEMENT_TYPE_U2:
				result = TypeCode.UInt16;
				break;
			case CorElementType.ELEMENT_TYPE_I4:
				result = TypeCode.Int32;
				break;
			case CorElementType.ELEMENT_TYPE_U4:
				result = TypeCode.UInt32;
				break;
			case CorElementType.ELEMENT_TYPE_I8:
				result = TypeCode.Int64;
				break;
			case CorElementType.ELEMENT_TYPE_U8:
				result = TypeCode.UInt64;
				break;
			default:
				throw new InvalidOperationException(SR.InvalidOperation_UnknownEnumType);
			}
			return result;
		}

		// Token: 0x06000310 RID: 784 RVA: 0x000B4FC2 File Offset: 0x000B41C2
		bool IConvertible.ToBoolean(IFormatProvider provider)
		{
			return Convert.ToBoolean(this.GetValue());
		}

		// Token: 0x06000311 RID: 785 RVA: 0x000B4FCF File Offset: 0x000B41CF
		char IConvertible.ToChar(IFormatProvider provider)
		{
			return Convert.ToChar(this.GetValue());
		}

		// Token: 0x06000312 RID: 786 RVA: 0x000B4FDC File Offset: 0x000B41DC
		sbyte IConvertible.ToSByte(IFormatProvider provider)
		{
			return Convert.ToSByte(this.GetValue());
		}

		// Token: 0x06000313 RID: 787 RVA: 0x000B4FE9 File Offset: 0x000B41E9
		byte IConvertible.ToByte(IFormatProvider provider)
		{
			return Convert.ToByte(this.GetValue());
		}

		// Token: 0x06000314 RID: 788 RVA: 0x000B4FF6 File Offset: 0x000B41F6
		short IConvertible.ToInt16(IFormatProvider provider)
		{
			return Convert.ToInt16(this.GetValue());
		}

		// Token: 0x06000315 RID: 789 RVA: 0x000B5003 File Offset: 0x000B4203
		ushort IConvertible.ToUInt16(IFormatProvider provider)
		{
			return Convert.ToUInt16(this.GetValue());
		}

		// Token: 0x06000316 RID: 790 RVA: 0x000B5010 File Offset: 0x000B4210
		int IConvertible.ToInt32(IFormatProvider provider)
		{
			return Convert.ToInt32(this.GetValue());
		}

		// Token: 0x06000317 RID: 791 RVA: 0x000B501D File Offset: 0x000B421D
		uint IConvertible.ToUInt32(IFormatProvider provider)
		{
			return Convert.ToUInt32(this.GetValue());
		}

		// Token: 0x06000318 RID: 792 RVA: 0x000B502A File Offset: 0x000B422A
		long IConvertible.ToInt64(IFormatProvider provider)
		{
			return Convert.ToInt64(this.GetValue());
		}

		// Token: 0x06000319 RID: 793 RVA: 0x000B5037 File Offset: 0x000B4237
		ulong IConvertible.ToUInt64(IFormatProvider provider)
		{
			return Convert.ToUInt64(this.GetValue());
		}

		// Token: 0x0600031A RID: 794 RVA: 0x000B5044 File Offset: 0x000B4244
		float IConvertible.ToSingle(IFormatProvider provider)
		{
			return Convert.ToSingle(this.GetValue());
		}

		// Token: 0x0600031B RID: 795 RVA: 0x000B5051 File Offset: 0x000B4251
		double IConvertible.ToDouble(IFormatProvider provider)
		{
			return Convert.ToDouble(this.GetValue());
		}

		// Token: 0x0600031C RID: 796 RVA: 0x000B505E File Offset: 0x000B425E
		decimal IConvertible.ToDecimal(IFormatProvider provider)
		{
			return Convert.ToDecimal(this.GetValue());
		}

		// Token: 0x0600031D RID: 797 RVA: 0x000B506B File Offset: 0x000B426B
		DateTime IConvertible.ToDateTime(IFormatProvider provider)
		{
			throw new InvalidCastException(SR.Format(SR.InvalidCast_FromTo, "Enum", "DateTime"));
		}

		// Token: 0x0600031E RID: 798 RVA: 0x000B5086 File Offset: 0x000B4286
		object IConvertible.ToType(Type type, IFormatProvider provider)
		{
			return Convert.DefaultToType(this, type, provider);
		}

		// Token: 0x0600031F RID: 799 RVA: 0x000B5090 File Offset: 0x000B4290
		[CLSCompliant(false)]
		public static object ToObject(Type enumType, sbyte value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), (long)value);
		}

		// Token: 0x06000320 RID: 800 RVA: 0x000B5090 File Offset: 0x000B4290
		public static object ToObject(Type enumType, short value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), (long)value);
		}

		// Token: 0x06000321 RID: 801 RVA: 0x000B5090 File Offset: 0x000B4290
		public static object ToObject(Type enumType, int value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), (long)value);
		}

		// Token: 0x06000322 RID: 802 RVA: 0x000B509F File Offset: 0x000B429F
		public static object ToObject(Type enumType, byte value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), (long)((ulong)value));
		}

		// Token: 0x06000323 RID: 803 RVA: 0x000B509F File Offset: 0x000B429F
		[CLSCompliant(false)]
		public static object ToObject(Type enumType, ushort value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), (long)((ulong)value));
		}

		// Token: 0x06000324 RID: 804 RVA: 0x000B509F File Offset: 0x000B429F
		[CLSCompliant(false)]
		public static object ToObject(Type enumType, uint value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), (long)((ulong)value));
		}

		// Token: 0x06000325 RID: 805 RVA: 0x000B50AE File Offset: 0x000B42AE
		public static object ToObject(Type enumType, long value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), value);
		}

		// Token: 0x06000326 RID: 806 RVA: 0x000B50AE File Offset: 0x000B42AE
		[CLSCompliant(false)]
		public static object ToObject(Type enumType, ulong value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), (long)value);
		}

		// Token: 0x06000327 RID: 807 RVA: 0x000B509F File Offset: 0x000B429F
		private static object ToObject(Type enumType, char value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), (long)((ulong)value));
		}

		// Token: 0x06000328 RID: 808 RVA: 0x000B50BC File Offset: 0x000B42BC
		private static object ToObject(Type enumType, bool value)
		{
			return Enum.InternalBoxEnum(Enum.ValidateRuntimeType(enumType), value ? 1L : 0L);
		}

		// Token: 0x06000329 RID: 809 RVA: 0x000B50D4 File Offset: 0x000B42D4
		private static RuntimeType ValidateRuntimeType(Type enumType)
		{
			if (enumType == null)
			{
				throw new ArgumentNullException("enumType");
			}
			if (!enumType.IsEnum)
			{
				throw new ArgumentException(SR.Arg_MustBeEnum, "enumType");
			}
			RuntimeType runtimeType = enumType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "enumType");
			}
			return runtimeType;
		}

		// Token: 0x04000111 RID: 273
		private const char EnumSeparatorChar = ',';

		// Token: 0x02000066 RID: 102
		internal sealed class EnumInfo
		{
			// Token: 0x0600032B RID: 811 RVA: 0x000B5130 File Offset: 0x000B4330
			public EnumInfo(bool hasFlagsAttribute, ulong[] values, string[] names)
			{
				this.HasFlagsAttribute = hasFlagsAttribute;
				this.Values = values;
				this.Names = names;
			}

			// Token: 0x04000112 RID: 274
			public readonly bool HasFlagsAttribute;

			// Token: 0x04000113 RID: 275
			public readonly ulong[] Values;

			// Token: 0x04000114 RID: 276
			public readonly string[] Names;
		}
	}
}
