using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005D2 RID: 1490
	[Nullable(0)]
	[NullableContext(1)]
	public static class CustomAttributeExtensions
	{
		// Token: 0x06004C02 RID: 19458 RVA: 0x0018B603 File Offset: 0x0018A803
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x06004C03 RID: 19459 RVA: 0x0018B60C File Offset: 0x0018A80C
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x06004C04 RID: 19460 RVA: 0x0018B615 File Offset: 0x0018A815
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x06004C05 RID: 19461 RVA: 0x0018B61E File Offset: 0x0018A81E
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType);
		}

		// Token: 0x06004C06 RID: 19462 RVA: 0x0018B627 File Offset: 0x0018A827
		[return: Nullable(2)]
		public static T GetCustomAttribute<[Nullable(0)] T>(this Assembly element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x06004C07 RID: 19463 RVA: 0x0018B63E File Offset: 0x0018A83E
		[return: Nullable(2)]
		public static T GetCustomAttribute<[Nullable(0)] T>(this Module element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x06004C08 RID: 19464 RVA: 0x0018B655 File Offset: 0x0018A855
		[return: Nullable(2)]
		public static T GetCustomAttribute<[Nullable(0)] T>(this MemberInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x06004C09 RID: 19465 RVA: 0x0018B66C File Offset: 0x0018A86C
		[return: Nullable(2)]
		public static T GetCustomAttribute<[Nullable(0)] T>(this ParameterInfo element) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T)));
		}

		// Token: 0x06004C0A RID: 19466 RVA: 0x0018B683 File Offset: 0x0018A883
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		// Token: 0x06004C0B RID: 19467 RVA: 0x0018B68D File Offset: 0x0018A88D
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttribute(element, attributeType, inherit);
		}

		// Token: 0x06004C0C RID: 19468 RVA: 0x0018B697 File Offset: 0x0018A897
		[return: Nullable(2)]
		public static T GetCustomAttribute<[Nullable(0)] T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		// Token: 0x06004C0D RID: 19469 RVA: 0x0018B6AF File Offset: 0x0018A8AF
		[return: Nullable(2)]
		public static T GetCustomAttribute<[Nullable(0)] T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (T)((object)element.GetCustomAttribute(typeof(T), inherit));
		}

		// Token: 0x06004C0E RID: 19470 RVA: 0x0018B6C7 File Offset: 0x0018A8C7
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x06004C0F RID: 19471 RVA: 0x0018B6CF File Offset: 0x0018A8CF
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x06004C10 RID: 19472 RVA: 0x0018B6D7 File Offset: 0x0018A8D7
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x06004C11 RID: 19473 RVA: 0x0018B6DF File Offset: 0x0018A8DF
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element)
		{
			return Attribute.GetCustomAttributes(element);
		}

		// Token: 0x06004C12 RID: 19474 RVA: 0x0018B6E7 File Offset: 0x0018A8E7
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		// Token: 0x06004C13 RID: 19475 RVA: 0x0018B6F0 File Offset: 0x0018A8F0
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, inherit);
		}

		// Token: 0x06004C14 RID: 19476 RVA: 0x0018B6F9 File Offset: 0x0018A8F9
		public static IEnumerable<Attribute> GetCustomAttributes(this Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x06004C15 RID: 19477 RVA: 0x0018B702 File Offset: 0x0018A902
		public static IEnumerable<Attribute> GetCustomAttributes(this Module element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x06004C16 RID: 19478 RVA: 0x0018B70B File Offset: 0x0018A90B
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x06004C17 RID: 19479 RVA: 0x0018B714 File Offset: 0x0018A914
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType);
		}

		// Token: 0x06004C18 RID: 19480 RVA: 0x0018B71D File Offset: 0x0018A91D
		public static IEnumerable<T> GetCustomAttributes<[Nullable(0)] T>(this Assembly element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x06004C19 RID: 19481 RVA: 0x0018B734 File Offset: 0x0018A934
		public static IEnumerable<T> GetCustomAttributes<[Nullable(0)] T>(this Module element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x06004C1A RID: 19482 RVA: 0x0018B74B File Offset: 0x0018A94B
		public static IEnumerable<T> GetCustomAttributes<[Nullable(0)] T>(this MemberInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x06004C1B RID: 19483 RVA: 0x0018B762 File Offset: 0x0018A962
		public static IEnumerable<T> GetCustomAttributes<[Nullable(0)] T>(this ParameterInfo element) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T));
		}

		// Token: 0x06004C1C RID: 19484 RVA: 0x0018B779 File Offset: 0x0018A979
		public static IEnumerable<Attribute> GetCustomAttributes(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		// Token: 0x06004C1D RID: 19485 RVA: 0x0018B783 File Offset: 0x0018A983
		public static IEnumerable<Attribute> GetCustomAttributes(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.GetCustomAttributes(element, attributeType, inherit);
		}

		// Token: 0x06004C1E RID: 19486 RVA: 0x0018B78D File Offset: 0x0018A98D
		public static IEnumerable<T> GetCustomAttributes<[Nullable(0)] T>(this MemberInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x06004C1F RID: 19487 RVA: 0x0018B7A5 File Offset: 0x0018A9A5
		public static IEnumerable<T> GetCustomAttributes<[Nullable(0)] T>(this ParameterInfo element, bool inherit) where T : Attribute
		{
			return (IEnumerable<T>)element.GetCustomAttributes(typeof(T), inherit);
		}

		// Token: 0x06004C20 RID: 19488 RVA: 0x0018B7BD File Offset: 0x0018A9BD
		public static bool IsDefined(this Assembly element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x06004C21 RID: 19489 RVA: 0x0018B7C6 File Offset: 0x0018A9C6
		public static bool IsDefined(this Module element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x06004C22 RID: 19490 RVA: 0x0018B7CF File Offset: 0x0018A9CF
		public static bool IsDefined(this MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x06004C23 RID: 19491 RVA: 0x0018B7D8 File Offset: 0x0018A9D8
		public static bool IsDefined(this ParameterInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType);
		}

		// Token: 0x06004C24 RID: 19492 RVA: 0x0018B7E1 File Offset: 0x0018A9E1
		public static bool IsDefined(this MemberInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}

		// Token: 0x06004C25 RID: 19493 RVA: 0x0018B7EB File Offset: 0x0018A9EB
		public static bool IsDefined(this ParameterInfo element, Type attributeType, bool inherit)
		{
			return Attribute.IsDefined(element, attributeType, inherit);
		}
	}
}
