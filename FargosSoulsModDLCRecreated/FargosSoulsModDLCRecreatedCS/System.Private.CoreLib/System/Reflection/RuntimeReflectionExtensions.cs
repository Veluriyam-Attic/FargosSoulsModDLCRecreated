using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005FD RID: 1533
	[NullableContext(1)]
	[Nullable(0)]
	public static class RuntimeReflectionExtensions
	{
		// Token: 0x06004D2A RID: 19754 RVA: 0x0018C476 File Offset: 0x0018B676
		public static IEnumerable<FieldInfo> GetRuntimeFields([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields | DynamicallyAccessedMemberTypes.NonPublicFields)] this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetFields(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004D2B RID: 19755 RVA: 0x0018C494 File Offset: 0x0018B694
		public static IEnumerable<MethodInfo> GetRuntimeMethods([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods)] this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004D2C RID: 19756 RVA: 0x0018C4B2 File Offset: 0x0018B6B2
		public static IEnumerable<PropertyInfo> GetRuntimeProperties([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties | DynamicallyAccessedMemberTypes.NonPublicProperties)] this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetProperties(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004D2D RID: 19757 RVA: 0x0018C4D0 File Offset: 0x0018B6D0
		public static IEnumerable<EventInfo> GetRuntimeEvents([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents | DynamicallyAccessedMemberTypes.NonPublicEvents)] this Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetEvents(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x06004D2E RID: 19758 RVA: 0x0018C4EE File Offset: 0x0018B6EE
		[return: Nullable(2)]
		public static FieldInfo GetRuntimeField([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicFields)] this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetField(name);
		}

		// Token: 0x06004D2F RID: 19759 RVA: 0x0018C50B File Offset: 0x0018B70B
		[return: Nullable(2)]
		public static MethodInfo GetRuntimeMethod([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods)] this Type type, string name, Type[] parameters)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetMethod(name, parameters);
		}

		// Token: 0x06004D30 RID: 19760 RVA: 0x0018C529 File Offset: 0x0018B729
		[return: Nullable(2)]
		public static PropertyInfo GetRuntimeProperty([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicProperties)] this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetProperty(name);
		}

		// Token: 0x06004D31 RID: 19761 RVA: 0x0018C546 File Offset: 0x0018B746
		[return: Nullable(2)]
		public static EventInfo GetRuntimeEvent([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicEvents)] this Type type, string name)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			return type.GetEvent(name);
		}

		// Token: 0x06004D32 RID: 19762 RVA: 0x0018C563 File Offset: 0x0018B763
		[return: Nullable(2)]
		public static MethodInfo GetRuntimeBaseDefinition(this MethodInfo method)
		{
			if (method == null)
			{
				throw new ArgumentNullException("method");
			}
			return method.GetBaseDefinition();
		}

		// Token: 0x06004D33 RID: 19763 RVA: 0x0018C57F File Offset: 0x0018B77F
		public static InterfaceMapping GetRuntimeInterfaceMap(this TypeInfo typeInfo, Type interfaceType)
		{
			if (typeInfo == null)
			{
				throw new ArgumentNullException("typeInfo");
			}
			return typeInfo.GetInterfaceMap(interfaceType);
		}

		// Token: 0x06004D34 RID: 19764 RVA: 0x0018C59C File Offset: 0x0018B79C
		public static MethodInfo GetMethodInfo(this Delegate del)
		{
			if (del == null)
			{
				throw new ArgumentNullException("del");
			}
			return del.Method;
		}
	}
}
