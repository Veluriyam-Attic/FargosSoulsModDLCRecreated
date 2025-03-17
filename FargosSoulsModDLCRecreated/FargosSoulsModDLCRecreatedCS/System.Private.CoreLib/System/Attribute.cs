using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000055 RID: 85
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public abstract class Attribute
	{
		// Token: 0x06000192 RID: 402 RVA: 0x000AE214 File Offset: 0x000AD414
		private static Attribute[] InternalGetCustomAttributes(PropertyInfo element, Type type, bool inherit)
		{
			Attribute[] array = (Attribute[])element.GetCustomAttributes(type, inherit);
			if (!inherit)
			{
				return array;
			}
			Dictionary<Type, AttributeUsageAttribute> types = new Dictionary<Type, AttributeUsageAttribute>(11);
			List<Attribute> list = new List<Attribute>();
			Attribute.CopyToArrayList(list, array, types);
			Type[] indexParameterTypes = Attribute.GetIndexParameterTypes(element);
			PropertyInfo parentDefinition = Attribute.GetParentDefinition(element, indexParameterTypes);
			while (parentDefinition != null)
			{
				array = Attribute.GetCustomAttributes(parentDefinition, type, false);
				Attribute.AddAttributesToList(list, array, types);
				parentDefinition = Attribute.GetParentDefinition(parentDefinition, indexParameterTypes);
			}
			Attribute[] array2 = Attribute.CreateAttributeArrayHelper(type, list.Count);
			list.CopyTo(array2, 0);
			return array2;
		}

		// Token: 0x06000193 RID: 403 RVA: 0x000AE29C File Offset: 0x000AD49C
		private static bool InternalIsDefined(PropertyInfo element, Type attributeType, bool inherit)
		{
			if (element.IsDefined(attributeType, inherit))
			{
				return true;
			}
			if (inherit)
			{
				AttributeUsageAttribute attributeUsageAttribute = Attribute.InternalGetAttributeUsage(attributeType);
				if (!attributeUsageAttribute.Inherited)
				{
					return false;
				}
				Type[] indexParameterTypes = Attribute.GetIndexParameterTypes(element);
				PropertyInfo parentDefinition = Attribute.GetParentDefinition(element, indexParameterTypes);
				while (parentDefinition != null)
				{
					if (parentDefinition.IsDefined(attributeType, false))
					{
						return true;
					}
					parentDefinition = Attribute.GetParentDefinition(parentDefinition, indexParameterTypes);
				}
			}
			return false;
		}

		// Token: 0x06000194 RID: 404 RVA: 0x000AE2F8 File Offset: 0x000AD4F8
		private static PropertyInfo GetParentDefinition(PropertyInfo property, Type[] propertyParameters)
		{
			MethodInfo methodInfo = property.GetGetMethod(true) ?? property.GetSetMethod(true);
			RuntimeMethodInfo runtimeMethodInfo = methodInfo as RuntimeMethodInfo;
			if (runtimeMethodInfo != null)
			{
				runtimeMethodInfo = runtimeMethodInfo.GetParentDefinition();
				if (runtimeMethodInfo != null)
				{
					return runtimeMethodInfo.DeclaringType.GetProperty(property.Name, BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic, null, property.PropertyType, propertyParameters, null);
				}
			}
			return null;
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000AE358 File Offset: 0x000AD558
		private static Attribute[] InternalGetCustomAttributes(EventInfo element, Type type, bool inherit)
		{
			Attribute[] array = (Attribute[])element.GetCustomAttributes(type, inherit);
			if (inherit)
			{
				Dictionary<Type, AttributeUsageAttribute> types = new Dictionary<Type, AttributeUsageAttribute>(11);
				List<Attribute> list = new List<Attribute>();
				Attribute.CopyToArrayList(list, array, types);
				EventInfo parentDefinition = Attribute.GetParentDefinition(element);
				while (parentDefinition != null)
				{
					array = Attribute.GetCustomAttributes(parentDefinition, type, false);
					Attribute.AddAttributesToList(list, array, types);
					parentDefinition = Attribute.GetParentDefinition(parentDefinition);
				}
				Attribute[] array2 = Attribute.CreateAttributeArrayHelper(type, list.Count);
				list.CopyTo(array2, 0);
				return array2;
			}
			return array;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000AE3D4 File Offset: 0x000AD5D4
		private static EventInfo GetParentDefinition(EventInfo ev)
		{
			MethodInfo addMethod = ev.GetAddMethod(true);
			RuntimeMethodInfo runtimeMethodInfo = addMethod as RuntimeMethodInfo;
			if (runtimeMethodInfo != null)
			{
				runtimeMethodInfo = runtimeMethodInfo.GetParentDefinition();
				if (runtimeMethodInfo != null)
				{
					return runtimeMethodInfo.DeclaringType.GetEvent(ev.Name);
				}
			}
			return null;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000AE41C File Offset: 0x000AD61C
		private static bool InternalIsDefined(EventInfo element, Type attributeType, bool inherit)
		{
			if (element.IsDefined(attributeType, inherit))
			{
				return true;
			}
			if (inherit)
			{
				AttributeUsageAttribute attributeUsageAttribute = Attribute.InternalGetAttributeUsage(attributeType);
				if (!attributeUsageAttribute.Inherited)
				{
					return false;
				}
				EventInfo parentDefinition = Attribute.GetParentDefinition(element);
				while (parentDefinition != null)
				{
					if (parentDefinition.IsDefined(attributeType, false))
					{
						return true;
					}
					parentDefinition = Attribute.GetParentDefinition(parentDefinition);
				}
			}
			return false;
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000AE470 File Offset: 0x000AD670
		private static ParameterInfo GetParentDefinition(ParameterInfo param)
		{
			RuntimeMethodInfo runtimeMethodInfo = param.Member as RuntimeMethodInfo;
			if (runtimeMethodInfo != null)
			{
				runtimeMethodInfo = runtimeMethodInfo.GetParentDefinition();
				if (runtimeMethodInfo != null)
				{
					int position = param.Position;
					if (position == -1)
					{
						return runtimeMethodInfo.ReturnParameter;
					}
					ParameterInfo[] parameters = runtimeMethodInfo.GetParameters();
					return parameters[position];
				}
			}
			return null;
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000AE4C0 File Offset: 0x000AD6C0
		private static Attribute[] InternalParamGetCustomAttributes(ParameterInfo param, Type type, bool inherit)
		{
			List<Type> list = new List<Type>();
			if (type == null)
			{
				type = typeof(Attribute);
			}
			object[] customAttributes = param.GetCustomAttributes(type, false);
			for (int i = 0; i < customAttributes.Length; i++)
			{
				Type type2 = customAttributes[i].GetType();
				AttributeUsageAttribute attributeUsageAttribute = Attribute.InternalGetAttributeUsage(type2);
				if (!attributeUsageAttribute.AllowMultiple)
				{
					list.Add(type2);
				}
			}
			Attribute[] array;
			if (customAttributes.Length == 0)
			{
				array = Attribute.CreateAttributeArrayHelper(type, 0);
			}
			else
			{
				array = (Attribute[])customAttributes;
			}
			if (param.Member.DeclaringType == null)
			{
				return array;
			}
			if (!inherit)
			{
				return array;
			}
			for (ParameterInfo parentDefinition = Attribute.GetParentDefinition(param); parentDefinition != null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
			{
				customAttributes = parentDefinition.GetCustomAttributes(type, false);
				int num = 0;
				for (int j = 0; j < customAttributes.Length; j++)
				{
					Type type3 = customAttributes[j].GetType();
					AttributeUsageAttribute attributeUsageAttribute2 = Attribute.InternalGetAttributeUsage(type3);
					if (attributeUsageAttribute2.Inherited && !list.Contains(type3))
					{
						if (!attributeUsageAttribute2.AllowMultiple)
						{
							list.Add(type3);
						}
						num++;
					}
					else
					{
						customAttributes[j] = null;
					}
				}
				Attribute[] array2 = Attribute.CreateAttributeArrayHelper(type, num);
				num = 0;
				for (int k = 0; k < customAttributes.Length; k++)
				{
					if (customAttributes[k] != null)
					{
						array2[num] = (Attribute)customAttributes[k];
						num++;
					}
				}
				Attribute[] array3 = array;
				array = Attribute.CreateAttributeArrayHelper(type, array3.Length + num);
				Array.Copy(array3, array, array3.Length);
				int num2 = array3.Length;
				for (int l = 0; l < array2.Length; l++)
				{
					array[num2 + l] = array2[l];
				}
			}
			return array;
		}

		// Token: 0x0600019A RID: 410 RVA: 0x000AE644 File Offset: 0x000AD844
		private static bool InternalParamIsDefined(ParameterInfo param, Type type, bool inherit)
		{
			if (param.IsDefined(type, false))
			{
				return true;
			}
			if (param.Member.DeclaringType == null || !inherit)
			{
				return false;
			}
			for (ParameterInfo parentDefinition = Attribute.GetParentDefinition(param); parentDefinition != null; parentDefinition = Attribute.GetParentDefinition(parentDefinition))
			{
				object[] customAttributes = parentDefinition.GetCustomAttributes(type, false);
				for (int i = 0; i < customAttributes.Length; i++)
				{
					Type type2 = customAttributes[i].GetType();
					AttributeUsageAttribute attributeUsageAttribute = Attribute.InternalGetAttributeUsage(type2);
					if (customAttributes[i] is Attribute && attributeUsageAttribute.Inherited)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x000AE6C0 File Offset: 0x000AD8C0
		private static void CopyToArrayList(List<Attribute> attributeList, Attribute[] attributes, Dictionary<Type, AttributeUsageAttribute> types)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				attributeList.Add(attributes[i]);
				Type type = attributes[i].GetType();
				if (!types.ContainsKey(type))
				{
					types[type] = Attribute.InternalGetAttributeUsage(type);
				}
			}
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000AE704 File Offset: 0x000AD904
		private static Type[] GetIndexParameterTypes(PropertyInfo element)
		{
			ParameterInfo[] indexParameters = element.GetIndexParameters();
			if (indexParameters.Length != 0)
			{
				Type[] array = new Type[indexParameters.Length];
				for (int i = 0; i < indexParameters.Length; i++)
				{
					array[i] = indexParameters[i].ParameterType;
				}
				return array;
			}
			return Array.Empty<Type>();
		}

		// Token: 0x0600019D RID: 413 RVA: 0x000AE748 File Offset: 0x000AD948
		private static void AddAttributesToList(List<Attribute> attributeList, Attribute[] attributes, Dictionary<Type, AttributeUsageAttribute> types)
		{
			for (int i = 0; i < attributes.Length; i++)
			{
				Type type = attributes[i].GetType();
				AttributeUsageAttribute attributeUsageAttribute;
				types.TryGetValue(type, out attributeUsageAttribute);
				if (attributeUsageAttribute == null)
				{
					attributeUsageAttribute = Attribute.InternalGetAttributeUsage(type);
					types[type] = attributeUsageAttribute;
					if (attributeUsageAttribute.Inherited)
					{
						attributeList.Add(attributes[i]);
					}
				}
				else if (attributeUsageAttribute.Inherited && attributeUsageAttribute.AllowMultiple)
				{
					attributeList.Add(attributes[i]);
				}
			}
		}

		// Token: 0x0600019E RID: 414 RVA: 0x000AE7B4 File Offset: 0x000AD9B4
		private static AttributeUsageAttribute InternalGetAttributeUsage(Type type)
		{
			object[] customAttributes = type.GetCustomAttributes(typeof(AttributeUsageAttribute), false);
			if (customAttributes.Length == 1)
			{
				return (AttributeUsageAttribute)customAttributes[0];
			}
			if (customAttributes.Length == 0)
			{
				return AttributeUsageAttribute.Default;
			}
			throw new FormatException(SR.Format(SR.Format_AttributeUsage, type));
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000AE7FC File Offset: 0x000AD9FC
		private static Attribute[] CreateAttributeArrayHelper(Type elementType, int elementCount)
		{
			return (Attribute[])Array.CreateInstance(elementType, elementCount);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000AE80A File Offset: 0x000ADA0A
		public static Attribute[] GetCustomAttributes(MemberInfo element, Type type)
		{
			return Attribute.GetCustomAttributes(element, type, true);
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x000AE814 File Offset: 0x000ADA14
		public static Attribute[] GetCustomAttributes(MemberInfo element, Type type, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			if (!type.IsSubclassOf(typeof(Attribute)) && type != typeof(Attribute))
			{
				throw new ArgumentException(SR.Argument_MustHaveAttributeBaseClass);
			}
			MemberTypes memberType = element.MemberType;
			Attribute[] result;
			if (memberType != MemberTypes.Event)
			{
				if (memberType == MemberTypes.Property)
				{
					result = Attribute.InternalGetCustomAttributes((PropertyInfo)element, type, inherit);
				}
				else
				{
					result = (element.GetCustomAttributes(type, inherit) as Attribute[]);
				}
			}
			else
			{
				result = Attribute.InternalGetCustomAttributes((EventInfo)element, type, inherit);
			}
			return result;
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x000AE8B7 File Offset: 0x000ADAB7
		public static Attribute[] GetCustomAttributes(MemberInfo element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x000AE8C0 File Offset: 0x000ADAC0
		public static Attribute[] GetCustomAttributes(MemberInfo element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			MemberTypes memberType = element.MemberType;
			Attribute[] result;
			if (memberType != MemberTypes.Event)
			{
				if (memberType == MemberTypes.Property)
				{
					result = Attribute.InternalGetCustomAttributes((PropertyInfo)element, typeof(Attribute), inherit);
				}
				else
				{
					result = (element.GetCustomAttributes(typeof(Attribute), inherit) as Attribute[]);
				}
			}
			else
			{
				result = Attribute.InternalGetCustomAttributes((EventInfo)element, typeof(Attribute), inherit);
			}
			return result;
		}

		// Token: 0x060001A4 RID: 420 RVA: 0x000AE93B File Offset: 0x000ADB3B
		public static bool IsDefined(MemberInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000AE948 File Offset: 0x000ADB48
		public static bool IsDefined(MemberInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(SR.Argument_MustHaveAttributeBaseClass);
			}
			MemberTypes memberType = element.MemberType;
			bool result;
			if (memberType != MemberTypes.Event)
			{
				if (memberType == MemberTypes.Property)
				{
					result = Attribute.InternalIsDefined((PropertyInfo)element, attributeType, inherit);
				}
				else
				{
					result = element.IsDefined(attributeType, inherit);
				}
			}
			else
			{
				result = Attribute.InternalIsDefined((EventInfo)element, attributeType, inherit);
			}
			return result;
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x000AE9E6 File Offset: 0x000ADBE6
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x000AE9F0 File Offset: 0x000ADBF0
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(MemberInfo element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(SR.RFLCT_AmbigCust);
		}

		// Token: 0x060001A8 RID: 424 RVA: 0x000AEA23 File Offset: 0x000ADC23
		public static Attribute[] GetCustomAttributes(ParameterInfo element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		// Token: 0x060001A9 RID: 425 RVA: 0x000AEA2C File Offset: 0x000ADC2C
		public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		// Token: 0x060001AA RID: 426 RVA: 0x000AEA38 File Offset: 0x000ADC38
		public static Attribute[] GetCustomAttributes(ParameterInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(SR.Argument_MustHaveAttributeBaseClass);
			}
			if (element.Member == null)
			{
				throw new ArgumentException(SR.Argument_InvalidParameterInfo, "element");
			}
			MemberInfo member = element.Member;
			if (member.MemberType == MemberTypes.Method && inherit)
			{
				return Attribute.InternalParamGetCustomAttributes(element, attributeType, inherit);
			}
			return element.GetCustomAttributes(attributeType, inherit) as Attribute[];
		}

		// Token: 0x060001AB RID: 427 RVA: 0x000AEAE0 File Offset: 0x000ADCE0
		public static Attribute[] GetCustomAttributes(ParameterInfo element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (element.Member == null)
			{
				throw new ArgumentException(SR.Argument_InvalidParameterInfo, "element");
			}
			MemberInfo member = element.Member;
			if (member.MemberType == MemberTypes.Method && inherit)
			{
				return Attribute.InternalParamGetCustomAttributes(element, null, inherit);
			}
			return element.GetCustomAttributes(typeof(Attribute), inherit) as Attribute[];
		}

		// Token: 0x060001AC RID: 428 RVA: 0x000AEB4C File Offset: 0x000ADD4C
		public static bool IsDefined(ParameterInfo element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		// Token: 0x060001AD RID: 429 RVA: 0x000AEB58 File Offset: 0x000ADD58
		public static bool IsDefined(ParameterInfo element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(SR.Argument_MustHaveAttributeBaseClass);
			}
			MemberInfo member = element.Member;
			MemberTypes memberType = member.MemberType;
			if (memberType == MemberTypes.Constructor)
			{
				return element.IsDefined(attributeType, false);
			}
			if (memberType == MemberTypes.Method)
			{
				return Attribute.InternalParamIsDefined(element, attributeType, inherit);
			}
			if (memberType != MemberTypes.Property)
			{
				throw new ArgumentException(SR.Argument_InvalidParamInfo);
			}
			return element.IsDefined(attributeType, false);
		}

		// Token: 0x060001AE RID: 430 RVA: 0x000AEBF8 File Offset: 0x000ADDF8
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		// Token: 0x060001AF RID: 431 RVA: 0x000AEC04 File Offset: 0x000ADE04
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(ParameterInfo element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(SR.RFLCT_AmbigCust);
		}

		// Token: 0x060001B0 RID: 432 RVA: 0x000AEC3D File Offset: 0x000ADE3D
		public static Attribute[] GetCustomAttributes(Module element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		// Token: 0x060001B1 RID: 433 RVA: 0x000AEC47 File Offset: 0x000ADE47
		public static Attribute[] GetCustomAttributes(Module element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		// Token: 0x060001B2 RID: 434 RVA: 0x000AEC50 File Offset: 0x000ADE50
		public static Attribute[] GetCustomAttributes(Module element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (Attribute[])element.GetCustomAttributes(typeof(Attribute), inherit);
		}

		// Token: 0x060001B3 RID: 435 RVA: 0x000AEC7C File Offset: 0x000ADE7C
		public static Attribute[] GetCustomAttributes(Module element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(SR.Argument_MustHaveAttributeBaseClass);
			}
			return (Attribute[])element.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060001B4 RID: 436 RVA: 0x000AECED File Offset: 0x000ADEED
		public static bool IsDefined(Module element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, false);
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x000AECF8 File Offset: 0x000ADEF8
		public static bool IsDefined(Module element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(SR.Argument_MustHaveAttributeBaseClass);
			}
			return element.IsDefined(attributeType, false);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x000AED64 File Offset: 0x000ADF64
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(Module element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000AED70 File Offset: 0x000ADF70
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(Module element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(SR.RFLCT_AmbigCust);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000AEDA3 File Offset: 0x000ADFA3
		public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttributes(element, attributeType, true);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x000AEDB0 File Offset: 0x000ADFB0
		public static Attribute[] GetCustomAttributes(Assembly element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(SR.Argument_MustHaveAttributeBaseClass);
			}
			return (Attribute[])element.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060001BA RID: 442 RVA: 0x000AEE21 File Offset: 0x000AE021
		public static Attribute[] GetCustomAttributes(Assembly element)
		{
			return Attribute.GetCustomAttributes(element, true);
		}

		// Token: 0x060001BB RID: 443 RVA: 0x000AEE2A File Offset: 0x000AE02A
		public static Attribute[] GetCustomAttributes(Assembly element, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			return (Attribute[])element.GetCustomAttributes(typeof(Attribute), inherit);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x000AEE56 File Offset: 0x000AE056
		public static bool IsDefined(Assembly element, Type attributeType)
		{
			return Attribute.IsDefined(element, attributeType, true);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000AEE60 File Offset: 0x000AE060
		public static bool IsDefined(Assembly element, Type attributeType, bool inherit)
		{
			if (element == null)
			{
				throw new ArgumentNullException("element");
			}
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (!attributeType.IsSubclassOf(typeof(Attribute)) && attributeType != typeof(Attribute))
			{
				throw new ArgumentException(SR.Argument_MustHaveAttributeBaseClass);
			}
			return element.IsDefined(attributeType, false);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000AEECC File Offset: 0x000AE0CC
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(Assembly element, Type attributeType)
		{
			return Attribute.GetCustomAttribute(element, attributeType, true);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x000AEED8 File Offset: 0x000AE0D8
		[return: Nullable(2)]
		public static Attribute GetCustomAttribute(Assembly element, Type attributeType, bool inherit)
		{
			Attribute[] customAttributes = Attribute.GetCustomAttributes(element, attributeType, inherit);
			if (customAttributes == null || customAttributes.Length == 0)
			{
				return null;
			}
			if (customAttributes.Length == 1)
			{
				return customAttributes[0];
			}
			throw new AmbiguousMatchException(SR.RFLCT_AmbigCust);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000AEF0C File Offset: 0x000AE10C
		[NullableContext(2)]
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2075:UnrecognizedReflectionPattern", Justification = "Unused fields don't make a difference for equality")]
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			if (base.GetType() != obj.GetType())
			{
				return false;
			}
			Type type = base.GetType();
			while (type != typeof(Attribute))
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				for (int i = 0; i < fields.Length; i++)
				{
					object value = fields[i].GetValue(this);
					object value2 = fields[i].GetValue(obj);
					if (!Attribute.AreFieldValuesEqual(value, value2))
					{
						return false;
					}
				}
				type = type.BaseType;
			}
			return true;
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000AEF98 File Offset: 0x000AE198
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2075:UnrecognizedReflectionPattern", Justification = "Unused fields don't make a difference for hashcode quality")]
		public override int GetHashCode()
		{
			Type type = base.GetType();
			while (type != typeof(Attribute))
			{
				FieldInfo[] fields = type.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				object obj = null;
				for (int i = 0; i < fields.Length; i++)
				{
					object value = fields[i].GetValue(this);
					if (value != null && !value.GetType().IsArray)
					{
						obj = value;
					}
					if (obj != null)
					{
						break;
					}
				}
				if (obj != null)
				{
					return obj.GetHashCode();
				}
				type = type.BaseType;
			}
			return type.GetHashCode();
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x000AF014 File Offset: 0x000AE214
		private static bool AreFieldValuesEqual(object thisValue, object thatValue)
		{
			if (thisValue == null && thatValue == null)
			{
				return true;
			}
			if (thisValue == null || thatValue == null)
			{
				return false;
			}
			Type type = thisValue.GetType();
			if (type.IsArray)
			{
				if (!type.Equals(thatValue.GetType()))
				{
					return false;
				}
				Array array = (Array)thisValue;
				Array array2 = (Array)thatValue;
				if (array.Length != array2.Length)
				{
					return false;
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (!Attribute.AreFieldValuesEqual(array.GetValue(i), array2.GetValue(i)))
					{
						return false;
					}
				}
			}
			else if (!thisValue.Equals(thatValue))
			{
				return false;
			}
			return true;
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060001C4 RID: 452 RVA: 0x000AF0A4 File Offset: 0x000AE2A4
		public virtual object TypeId
		{
			get
			{
				return base.GetType();
			}
		}

		// Token: 0x060001C5 RID: 453 RVA: 0x000AF0AC File Offset: 0x000AE2AC
		[NullableContext(2)]
		public virtual bool Match(object obj)
		{
			return this.Equals(obj);
		}

		// Token: 0x060001C6 RID: 454 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsDefaultAttribute()
		{
			return false;
		}
	}
}
