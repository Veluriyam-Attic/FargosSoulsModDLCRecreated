using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000790 RID: 1936
	internal static class Statics
	{
		// Token: 0x06005D67 RID: 23911 RVA: 0x001C3B60 File Offset: 0x001C2D60
		public static byte[] MetadataForString(string name, int prefixSize, int suffixSize, int additionalSize)
		{
			Statics.CheckName(name);
			int num = Encoding.UTF8.GetByteCount(name) + 3 + prefixSize + suffixSize;
			byte[] array = new byte[num];
			ushort num2 = checked((ushort)(num + additionalSize));
			array[0] = (byte)num2;
			array[1] = (byte)(num2 >> 8);
			Encoding.UTF8.GetBytes(name, 0, name.Length, array, 2 + prefixSize);
			return array;
		}

		// Token: 0x06005D68 RID: 23912 RVA: 0x001C3BB8 File Offset: 0x001C2DB8
		public static void EncodeTags(int tags, ref int pos, byte[] metadata)
		{
			int num = tags & 268435455;
			bool flag;
			do
			{
				byte b = (byte)(num >> 21 & 127);
				flag = ((num & 2097151) != 0);
				b |= (flag ? 128 : 0);
				num <<= 7;
				if (metadata != null)
				{
					metadata[pos] = b;
				}
				pos++;
			}
			while (flag);
		}

		// Token: 0x06005D69 RID: 23913 RVA: 0x001C3C06 File Offset: 0x001C2E06
		public static byte Combine(int settingValue, byte defaultValue)
		{
			if ((int)((byte)settingValue) != settingValue)
			{
				return defaultValue;
			}
			return (byte)settingValue;
		}

		// Token: 0x06005D6A RID: 23914 RVA: 0x001C3C11 File Offset: 0x001C2E11
		public static int Combine(int settingValue1, int settingValue2)
		{
			if ((int)((byte)settingValue1) != settingValue1)
			{
				return settingValue2;
			}
			return settingValue1;
		}

		// Token: 0x06005D6B RID: 23915 RVA: 0x001C3C1B File Offset: 0x001C2E1B
		public static void CheckName(string name)
		{
			if (name != null && 0 <= name.IndexOf('\0'))
			{
				throw new ArgumentOutOfRangeException("name");
			}
		}

		// Token: 0x06005D6C RID: 23916 RVA: 0x001C3C35 File Offset: 0x001C2E35
		public static bool ShouldOverrideFieldName(string fieldName)
		{
			return fieldName.Length <= 2 && fieldName[0] == '_';
		}

		// Token: 0x06005D6D RID: 23917 RVA: 0x001C3C4D File Offset: 0x001C2E4D
		public static TraceLoggingDataType MakeDataType(TraceLoggingDataType baseType, EventFieldFormat format)
		{
			return (baseType & (TraceLoggingDataType)31) | (TraceLoggingDataType)(format << 8);
		}

		// Token: 0x06005D6E RID: 23918 RVA: 0x001C3C58 File Offset: 0x001C2E58
		public static TraceLoggingDataType Format8(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case EventFieldFormat.String:
				return TraceLoggingDataType.Char8;
			case EventFieldFormat.Boolean:
				return TraceLoggingDataType.Boolean8;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt8;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x06005D6F RID: 23919 RVA: 0x001C3CA8 File Offset: 0x001C2EA8
		public static TraceLoggingDataType Format16(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case EventFieldFormat.String:
				return TraceLoggingDataType.Char16;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt16;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x06005D70 RID: 23920 RVA: 0x001C3CF0 File Offset: 0x001C2EF0
		public static TraceLoggingDataType Format32(EventFieldFormat format, TraceLoggingDataType native)
		{
			switch (format)
			{
			case EventFieldFormat.Default:
				return native;
			case (EventFieldFormat)1:
			case EventFieldFormat.String:
				break;
			case EventFieldFormat.Boolean:
				return TraceLoggingDataType.Boolean32;
			case EventFieldFormat.Hexadecimal:
				return TraceLoggingDataType.HexInt32;
			default:
				if (format == EventFieldFormat.HResult)
				{
					return TraceLoggingDataType.HResult;
				}
				break;
			}
			return Statics.MakeDataType(native, format);
		}

		// Token: 0x06005D71 RID: 23921 RVA: 0x001C3D40 File Offset: 0x001C2F40
		public static TraceLoggingDataType Format64(EventFieldFormat format, TraceLoggingDataType native)
		{
			TraceLoggingDataType result;
			if (format != EventFieldFormat.Default)
			{
				if (format != EventFieldFormat.Hexadecimal)
				{
					result = Statics.MakeDataType(native, format);
				}
				else
				{
					result = TraceLoggingDataType.HexInt64;
				}
			}
			else
			{
				result = native;
			}
			return result;
		}

		// Token: 0x06005D72 RID: 23922 RVA: 0x001C3D68 File Offset: 0x001C2F68
		public static TraceLoggingDataType FormatPtr(EventFieldFormat format, TraceLoggingDataType native)
		{
			TraceLoggingDataType result;
			if (format != EventFieldFormat.Default)
			{
				if (format != EventFieldFormat.Hexadecimal)
				{
					result = Statics.MakeDataType(native, format);
				}
				else
				{
					result = Statics.HexIntPtrType;
				}
			}
			else
			{
				result = native;
			}
			return result;
		}

		// Token: 0x06005D73 RID: 23923 RVA: 0x001C3D93 File Offset: 0x001C2F93
		public static bool HasCustomAttribute(PropertyInfo propInfo, Type attributeType)
		{
			return propInfo.IsDefined(attributeType, false);
		}

		// Token: 0x06005D74 RID: 23924 RVA: 0x001C3DA0 File Offset: 0x001C2FA0
		public static AttributeType GetCustomAttribute<AttributeType>(PropertyInfo propInfo) where AttributeType : Attribute
		{
			AttributeType result = default(AttributeType);
			object[] customAttributes = propInfo.GetCustomAttributes(typeof(AttributeType), false);
			if (customAttributes.Length != 0)
			{
				result = (AttributeType)((object)customAttributes[0]);
			}
			return result;
		}

		// Token: 0x06005D75 RID: 23925 RVA: 0x001C3DA0 File Offset: 0x001C2FA0
		public static AttributeType GetCustomAttribute<AttributeType>(Type type) where AttributeType : Attribute
		{
			AttributeType result = default(AttributeType);
			object[] customAttributes = type.GetCustomAttributes(typeof(AttributeType), false);
			if (customAttributes.Length != 0)
			{
				result = (AttributeType)((object)customAttributes[0]);
			}
			return result;
		}

		// Token: 0x06005D76 RID: 23926 RVA: 0x001C3DD8 File Offset: 0x001C2FD8
		public static Type FindEnumerableElementType(Type type)
		{
			Type type2 = null;
			if (Statics.IsGenericMatch(type, typeof(IEnumerable<>)))
			{
				type2 = type.GetGenericArguments()[0];
			}
			else
			{
				Type[] array = type.FindInterfaces(new TypeFilter(Statics.IsGenericMatch), typeof(IEnumerable<>));
				foreach (Type type3 in array)
				{
					if (type2 != null)
					{
						type2 = null;
						break;
					}
					type2 = type3.GetGenericArguments()[0];
				}
			}
			return type2;
		}

		// Token: 0x06005D77 RID: 23927 RVA: 0x001C3E4E File Offset: 0x001C304E
		public static bool IsGenericMatch(Type type, object openType)
		{
			return type.IsGenericType && type.GetGenericTypeDefinition() == (Type)openType;
		}

		// Token: 0x06005D78 RID: 23928 RVA: 0x001C3E6C File Offset: 0x001C306C
		public static TraceLoggingTypeInfo CreateDefaultTypeInfo(Type dataType, List<Type> recursionCheck)
		{
			if (recursionCheck.Contains(dataType))
			{
				throw new NotSupportedException(SR.EventSource_RecursiveTypeDefinition);
			}
			recursionCheck.Add(dataType);
			EventDataAttribute customAttribute = Statics.GetCustomAttribute<EventDataAttribute>(dataType);
			TraceLoggingTypeInfo result;
			if (customAttribute != null || Statics.GetCustomAttribute<CompilerGeneratedAttribute>(dataType) != null || Statics.IsGenericMatch(dataType, typeof(KeyValuePair<, >)))
			{
				TypeAnalysis typeAnalysis = new TypeAnalysis(dataType, customAttribute, recursionCheck);
				result = new InvokeTypeInfo(dataType, typeAnalysis);
			}
			else if (dataType.IsArray)
			{
				Type elementType = dataType.GetElementType();
				if (elementType == typeof(bool))
				{
					result = ScalarArrayTypeInfo.Boolean();
				}
				else if (elementType == typeof(byte))
				{
					result = ScalarArrayTypeInfo.Byte();
				}
				else if (elementType == typeof(sbyte))
				{
					result = ScalarArrayTypeInfo.SByte();
				}
				else if (elementType == typeof(short))
				{
					result = ScalarArrayTypeInfo.Int16();
				}
				else if (elementType == typeof(ushort))
				{
					result = ScalarArrayTypeInfo.UInt16();
				}
				else if (elementType == typeof(int))
				{
					result = ScalarArrayTypeInfo.Int32();
				}
				else if (elementType == typeof(uint))
				{
					result = ScalarArrayTypeInfo.UInt32();
				}
				else if (elementType == typeof(long))
				{
					result = ScalarArrayTypeInfo.Int64();
				}
				else if (elementType == typeof(ulong))
				{
					result = ScalarArrayTypeInfo.UInt64();
				}
				else if (elementType == typeof(char))
				{
					result = ScalarArrayTypeInfo.Char();
				}
				else if (elementType == typeof(double))
				{
					result = ScalarArrayTypeInfo.Double();
				}
				else if (elementType == typeof(float))
				{
					result = ScalarArrayTypeInfo.Single();
				}
				else if (elementType == typeof(IntPtr))
				{
					result = ScalarArrayTypeInfo.IntPtr();
				}
				else if (elementType == typeof(UIntPtr))
				{
					result = ScalarArrayTypeInfo.UIntPtr();
				}
				else if (elementType == typeof(Guid))
				{
					result = ScalarArrayTypeInfo.Guid();
				}
				else
				{
					result = new ArrayTypeInfo(dataType, TraceLoggingTypeInfo.GetInstance(elementType, recursionCheck));
				}
			}
			else
			{
				if (dataType.IsEnum)
				{
					dataType = Enum.GetUnderlyingType(dataType);
				}
				if (dataType == typeof(string))
				{
					result = new StringTypeInfo();
				}
				else if (dataType == typeof(bool))
				{
					result = ScalarTypeInfo.Boolean();
				}
				else if (dataType == typeof(byte))
				{
					result = ScalarTypeInfo.Byte();
				}
				else if (dataType == typeof(sbyte))
				{
					result = ScalarTypeInfo.SByte();
				}
				else if (dataType == typeof(short))
				{
					result = ScalarTypeInfo.Int16();
				}
				else if (dataType == typeof(ushort))
				{
					result = ScalarTypeInfo.UInt16();
				}
				else if (dataType == typeof(int))
				{
					result = ScalarTypeInfo.Int32();
				}
				else if (dataType == typeof(uint))
				{
					result = ScalarTypeInfo.UInt32();
				}
				else if (dataType == typeof(long))
				{
					result = ScalarTypeInfo.Int64();
				}
				else if (dataType == typeof(ulong))
				{
					result = ScalarTypeInfo.UInt64();
				}
				else if (dataType == typeof(char))
				{
					result = ScalarTypeInfo.Char();
				}
				else if (dataType == typeof(double))
				{
					result = ScalarTypeInfo.Double();
				}
				else if (dataType == typeof(float))
				{
					result = ScalarTypeInfo.Single();
				}
				else if (dataType == typeof(DateTime))
				{
					result = new DateTimeTypeInfo();
				}
				else if (dataType == typeof(decimal))
				{
					result = new DecimalTypeInfo();
				}
				else if (dataType == typeof(IntPtr))
				{
					result = ScalarTypeInfo.IntPtr();
				}
				else if (dataType == typeof(UIntPtr))
				{
					result = ScalarTypeInfo.UIntPtr();
				}
				else if (dataType == typeof(Guid))
				{
					result = ScalarTypeInfo.Guid();
				}
				else if (dataType == typeof(TimeSpan))
				{
					result = new TimeSpanTypeInfo();
				}
				else if (dataType == typeof(DateTimeOffset))
				{
					result = new DateTimeOffsetTypeInfo();
				}
				else if (dataType == typeof(EmptyStruct))
				{
					result = new NullTypeInfo();
				}
				else if (Statics.IsGenericMatch(dataType, typeof(Nullable<>)))
				{
					result = new NullableTypeInfo(dataType, recursionCheck);
				}
				else
				{
					Type type = Statics.FindEnumerableElementType(dataType);
					if (!(type != null))
					{
						throw new ArgumentException(SR.Format(SR.EventSource_NonCompliantTypeError, dataType.Name));
					}
					result = new EnumerableTypeInfo(dataType, TraceLoggingTypeInfo.GetInstance(type, recursionCheck));
				}
			}
			return result;
		}

		// Token: 0x06005D79 RID: 23929 RVA: 0x001C4367 File Offset: 0x001C3567
		// Note: this type is marked as 'beforefieldinit'.
		static Statics()
		{
			if (IntPtr.Size != 8)
			{
			}
			Statics.IntPtrType = TraceLoggingDataType.Int64;
			if (IntPtr.Size != 8)
			{
			}
			Statics.UIntPtrType = TraceLoggingDataType.UInt64;
			if (IntPtr.Size != 8)
			{
			}
			Statics.HexIntPtrType = TraceLoggingDataType.HexInt64;
		}

		// Token: 0x04001C3B RID: 7227
		public static readonly TraceLoggingDataType IntPtrType;

		// Token: 0x04001C3C RID: 7228
		public static readonly TraceLoggingDataType UIntPtrType;

		// Token: 0x04001C3D RID: 7229
		public static readonly TraceLoggingDataType HexIntPtrType;
	}
}
