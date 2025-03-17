using System;

namespace System.Collections.Generic
{
	// Token: 0x020007D7 RID: 2007
	internal static class ComparerHelpers
	{
		// Token: 0x0600608D RID: 24717 RVA: 0x001CE14C File Offset: 0x001CD34C
		internal static object CreateDefaultComparer(Type type)
		{
			object obj = null;
			RuntimeType runtimeType = (RuntimeType)type;
			if (typeof(IComparable<>).MakeGenericType(new Type[]
			{
				type
			}).IsAssignableFrom(type))
			{
				obj = RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericComparer<int>), runtimeType);
			}
			else if (type.IsGenericType)
			{
				if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					obj = ComparerHelpers.TryCreateNullableComparer(runtimeType);
				}
			}
			else if (type.IsEnum)
			{
				obj = ComparerHelpers.TryCreateEnumComparer(runtimeType);
			}
			return obj ?? RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(ObjectComparer<object>), runtimeType);
		}

		// Token: 0x0600608E RID: 24718 RVA: 0x001CE1EC File Offset: 0x001CD3EC
		private static object TryCreateNullableComparer(RuntimeType nullableType)
		{
			RuntimeType runtimeType = (RuntimeType)nullableType.GetGenericArguments()[0];
			if (typeof(IComparable<>).MakeGenericType(new Type[]
			{
				runtimeType
			}).IsAssignableFrom(runtimeType))
			{
				return RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(NullableComparer<int>), runtimeType);
			}
			return null;
		}

		// Token: 0x0600608F RID: 24719 RVA: 0x001CE240 File Offset: 0x001CD440
		private static object TryCreateEnumComparer(RuntimeType enumType)
		{
			TypeCode typeCode = Type.GetTypeCode(Enum.GetUnderlyingType(enumType));
			if (typeCode - TypeCode.SByte <= 7)
			{
				return RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(EnumComparer<>), enumType);
			}
			return null;
		}

		// Token: 0x06006090 RID: 24720 RVA: 0x001CE278 File Offset: 0x001CD478
		internal static object CreateDefaultEqualityComparer(Type type)
		{
			object obj = null;
			RuntimeType runtimeType = (RuntimeType)type;
			if (type == typeof(byte))
			{
				obj = new ByteEqualityComparer();
			}
			else if (type.IsAssignableTo(typeof(IEquatable<>).MakeGenericType(new Type[]
			{
				type
			})))
			{
				obj = RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(GenericEqualityComparer<int>), runtimeType);
			}
			else if (type.IsGenericType)
			{
				if (type.GetGenericTypeDefinition() == typeof(Nullable<>))
				{
					obj = ComparerHelpers.TryCreateNullableEqualityComparer(runtimeType);
				}
			}
			else if (type.IsEnum)
			{
				obj = ComparerHelpers.TryCreateEnumEqualityComparer(runtimeType);
			}
			return obj ?? RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(ObjectEqualityComparer<object>), runtimeType);
		}

		// Token: 0x06006091 RID: 24721 RVA: 0x001CE334 File Offset: 0x001CD534
		private static object TryCreateNullableEqualityComparer(RuntimeType nullableType)
		{
			RuntimeType runtimeType = (RuntimeType)nullableType.GetGenericArguments()[0];
			if (typeof(IEquatable<>).MakeGenericType(new Type[]
			{
				runtimeType
			}).IsAssignableFrom(runtimeType))
			{
				return RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(NullableEqualityComparer<int>), runtimeType);
			}
			return null;
		}

		// Token: 0x06006092 RID: 24722 RVA: 0x001CE388 File Offset: 0x001CD588
		private static object TryCreateEnumEqualityComparer(RuntimeType enumType)
		{
			TypeCode typeCode = Type.GetTypeCode(Enum.GetUnderlyingType(enumType));
			if (typeCode - TypeCode.SByte <= 7)
			{
				return RuntimeTypeHandle.CreateInstanceForAnotherGenericParameter((RuntimeType)typeof(EnumEqualityComparer<>), enumType);
			}
			return null;
		}
	}
}
