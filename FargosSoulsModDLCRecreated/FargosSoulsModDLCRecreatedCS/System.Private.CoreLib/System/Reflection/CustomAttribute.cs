using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000593 RID: 1427
	internal static class CustomAttribute
	{
		// Token: 0x0600494F RID: 18767 RVA: 0x001846EC File Offset: 0x001838EC
		internal static bool IsDefined(RuntimeType type, RuntimeType caType, bool inherit)
		{
			if (type.GetElementType() != null)
			{
				return false;
			}
			if (PseudoCustomAttribute.IsDefined(type, caType))
			{
				return true;
			}
			if (CustomAttribute.IsCustomAttributeDefined(type.GetRuntimeModule(), type.MetadataToken, caType))
			{
				return true;
			}
			if (!inherit)
			{
				return false;
			}
			type = (type.BaseType as RuntimeType);
			while (type != null)
			{
				if (CustomAttribute.IsCustomAttributeDefined(type.GetRuntimeModule(), type.MetadataToken, caType, 0, inherit))
				{
					return true;
				}
				type = (type.BaseType as RuntimeType);
			}
			return false;
		}

		// Token: 0x06004950 RID: 18768 RVA: 0x00184770 File Offset: 0x00183970
		internal static bool IsDefined(RuntimeMethodInfo method, RuntimeType caType, bool inherit)
		{
			if (PseudoCustomAttribute.IsDefined(method, caType))
			{
				return true;
			}
			if (CustomAttribute.IsCustomAttributeDefined(method.GetRuntimeModule(), method.MetadataToken, caType))
			{
				return true;
			}
			if (!inherit)
			{
				return false;
			}
			method = method.GetParentDefinition();
			while (method != null)
			{
				if (CustomAttribute.IsCustomAttributeDefined(method.GetRuntimeModule(), method.MetadataToken, caType, 0, inherit))
				{
					return true;
				}
				method = method.GetParentDefinition();
			}
			return false;
		}

		// Token: 0x06004951 RID: 18769 RVA: 0x001847D7 File Offset: 0x001839D7
		internal static bool IsDefined(RuntimeConstructorInfo ctor, RuntimeType caType)
		{
			return CustomAttribute.IsCustomAttributeDefined(ctor.GetRuntimeModule(), ctor.MetadataToken, caType);
		}

		// Token: 0x06004952 RID: 18770 RVA: 0x001847EB File Offset: 0x001839EB
		internal static bool IsDefined(RuntimePropertyInfo property, RuntimeType caType)
		{
			return CustomAttribute.IsCustomAttributeDefined(property.GetRuntimeModule(), property.MetadataToken, caType);
		}

		// Token: 0x06004953 RID: 18771 RVA: 0x001847FF File Offset: 0x001839FF
		internal static bool IsDefined(RuntimeEventInfo e, RuntimeType caType)
		{
			return CustomAttribute.IsCustomAttributeDefined(e.GetRuntimeModule(), e.MetadataToken, caType);
		}

		// Token: 0x06004954 RID: 18772 RVA: 0x00184813 File Offset: 0x00183A13
		internal static bool IsDefined(RuntimeFieldInfo field, RuntimeType caType)
		{
			return PseudoCustomAttribute.IsDefined(field, caType) || CustomAttribute.IsCustomAttributeDefined(field.GetRuntimeModule(), field.MetadataToken, caType);
		}

		// Token: 0x06004955 RID: 18773 RVA: 0x00184832 File Offset: 0x00183A32
		internal static bool IsDefined(RuntimeParameterInfo parameter, RuntimeType caType)
		{
			return PseudoCustomAttribute.IsDefined(parameter, caType) || CustomAttribute.IsCustomAttributeDefined(parameter.GetRuntimeModule(), parameter.MetadataToken, caType);
		}

		// Token: 0x06004956 RID: 18774 RVA: 0x00184851 File Offset: 0x00183A51
		internal static bool IsDefined(RuntimeAssembly assembly, RuntimeType caType)
		{
			return CustomAttribute.IsCustomAttributeDefined(assembly.ManifestModule as RuntimeModule, RuntimeAssembly.GetToken(assembly.GetNativeHandle()), caType);
		}

		// Token: 0x06004957 RID: 18775 RVA: 0x0018486F File Offset: 0x00183A6F
		internal static bool IsDefined(RuntimeModule module, RuntimeType caType)
		{
			return CustomAttribute.IsCustomAttributeDefined(module, module.MetadataToken, caType);
		}

		// Token: 0x06004958 RID: 18776 RVA: 0x00184880 File Offset: 0x00183A80
		internal static object[] GetCustomAttributes(RuntimeType type, RuntimeType caType, bool inherit)
		{
			if (type.GetElementType() != null)
			{
				if (!caType.IsValueType)
				{
					return CustomAttribute.CreateAttributeArrayHelper(caType, 0);
				}
				return Array.Empty<object>();
			}
			else
			{
				if (type.IsGenericType && !type.IsGenericTypeDefinition)
				{
					type = (type.GetGenericTypeDefinition() as RuntimeType);
				}
				RuntimeType.ListBuilder<Attribute> listBuilder;
				PseudoCustomAttribute.GetCustomAttributes(type, caType, out listBuilder);
				if (!inherit || (caType.IsSealed && !CustomAttribute.GetAttributeUsage(caType).Inherited))
				{
					object[] customAttributes = CustomAttribute.GetCustomAttributes(type.GetRuntimeModule(), type.MetadataToken, listBuilder.Count, caType);
					if (listBuilder.Count > 0)
					{
						listBuilder.CopyTo(customAttributes, customAttributes.Length - listBuilder.Count);
					}
					return customAttributes;
				}
				RuntimeType.ListBuilder<object> derivedAttributes = default(RuntimeType.ListBuilder<object>);
				bool mustBeInheritable = false;
				RuntimeType elementType = (caType.IsValueType || caType.ContainsGenericParameters) ? ((RuntimeType)typeof(object)) : caType;
				for (int i = 0; i < listBuilder.Count; i++)
				{
					derivedAttributes.Add(listBuilder[i]);
				}
				while (type != (RuntimeType)typeof(object) && type != null)
				{
					CustomAttribute.AddCustomAttributes(ref derivedAttributes, type.GetRuntimeModule(), type.MetadataToken, caType, mustBeInheritable, derivedAttributes);
					mustBeInheritable = true;
					type = (type.BaseType as RuntimeType);
				}
				object[] array = CustomAttribute.CreateAttributeArrayHelper(elementType, derivedAttributes.Count);
				for (int j = 0; j < derivedAttributes.Count; j++)
				{
					array[j] = derivedAttributes[j];
				}
				return array;
			}
		}

		// Token: 0x06004959 RID: 18777 RVA: 0x00184A04 File Offset: 0x00183C04
		internal static object[] GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, bool inherit)
		{
			if (method.IsGenericMethod && !method.IsGenericMethodDefinition)
			{
				method = (method.GetGenericMethodDefinition() as RuntimeMethodInfo);
			}
			RuntimeType.ListBuilder<Attribute> listBuilder;
			PseudoCustomAttribute.GetCustomAttributes(method, caType, out listBuilder);
			if (!inherit || (caType.IsSealed && !CustomAttribute.GetAttributeUsage(caType).Inherited))
			{
				object[] customAttributes = CustomAttribute.GetCustomAttributes(method.GetRuntimeModule(), method.MetadataToken, listBuilder.Count, caType);
				if (listBuilder.Count > 0)
				{
					listBuilder.CopyTo(customAttributes, customAttributes.Length - listBuilder.Count);
				}
				return customAttributes;
			}
			RuntimeType.ListBuilder<object> derivedAttributes = default(RuntimeType.ListBuilder<object>);
			bool mustBeInheritable = false;
			RuntimeType elementType = (caType.IsValueType || caType.ContainsGenericParameters) ? ((RuntimeType)typeof(object)) : caType;
			for (int i = 0; i < listBuilder.Count; i++)
			{
				derivedAttributes.Add(listBuilder[i]);
			}
			while (method != null)
			{
				CustomAttribute.AddCustomAttributes(ref derivedAttributes, method.GetRuntimeModule(), method.MetadataToken, caType, mustBeInheritable, derivedAttributes);
				mustBeInheritable = true;
				method = method.GetParentDefinition();
			}
			object[] array = CustomAttribute.CreateAttributeArrayHelper(elementType, derivedAttributes.Count);
			for (int j = 0; j < derivedAttributes.Count; j++)
			{
				array[j] = derivedAttributes[j];
			}
			return array;
		}

		// Token: 0x0600495A RID: 18778 RVA: 0x00184B46 File Offset: 0x00183D46
		internal static object[] GetCustomAttributes(RuntimeConstructorInfo ctor, RuntimeType caType)
		{
			return CustomAttribute.GetCustomAttributes(ctor.GetRuntimeModule(), ctor.MetadataToken, 0, caType);
		}

		// Token: 0x0600495B RID: 18779 RVA: 0x00184B5B File Offset: 0x00183D5B
		internal static object[] GetCustomAttributes(RuntimePropertyInfo property, RuntimeType caType)
		{
			return CustomAttribute.GetCustomAttributes(property.GetRuntimeModule(), property.MetadataToken, 0, caType);
		}

		// Token: 0x0600495C RID: 18780 RVA: 0x00184B70 File Offset: 0x00183D70
		internal static object[] GetCustomAttributes(RuntimeEventInfo e, RuntimeType caType)
		{
			return CustomAttribute.GetCustomAttributes(e.GetRuntimeModule(), e.MetadataToken, 0, caType);
		}

		// Token: 0x0600495D RID: 18781 RVA: 0x00184B88 File Offset: 0x00183D88
		internal static object[] GetCustomAttributes(RuntimeFieldInfo field, RuntimeType caType)
		{
			RuntimeType.ListBuilder<Attribute> listBuilder;
			PseudoCustomAttribute.GetCustomAttributes(field, caType, out listBuilder);
			object[] customAttributes = CustomAttribute.GetCustomAttributes(field.GetRuntimeModule(), field.MetadataToken, listBuilder.Count, caType);
			if (listBuilder.Count > 0)
			{
				listBuilder.CopyTo(customAttributes, customAttributes.Length - listBuilder.Count);
			}
			return customAttributes;
		}

		// Token: 0x0600495E RID: 18782 RVA: 0x00184BD8 File Offset: 0x00183DD8
		internal static object[] GetCustomAttributes(RuntimeParameterInfo parameter, RuntimeType caType)
		{
			RuntimeType.ListBuilder<Attribute> listBuilder;
			PseudoCustomAttribute.GetCustomAttributes(parameter, caType, out listBuilder);
			object[] customAttributes = CustomAttribute.GetCustomAttributes(parameter.GetRuntimeModule(), parameter.MetadataToken, listBuilder.Count, caType);
			if (listBuilder.Count > 0)
			{
				listBuilder.CopyTo(customAttributes, customAttributes.Length - listBuilder.Count);
			}
			return customAttributes;
		}

		// Token: 0x0600495F RID: 18783 RVA: 0x00184C28 File Offset: 0x00183E28
		internal static object[] GetCustomAttributes(RuntimeAssembly assembly, RuntimeType caType)
		{
			int token = RuntimeAssembly.GetToken(assembly.GetNativeHandle());
			return CustomAttribute.GetCustomAttributes(assembly.ManifestModule as RuntimeModule, token, 0, caType);
		}

		// Token: 0x06004960 RID: 18784 RVA: 0x00184C54 File Offset: 0x00183E54
		internal static object[] GetCustomAttributes(RuntimeModule module, RuntimeType caType)
		{
			return CustomAttribute.GetCustomAttributes(module, module.MetadataToken, 0, caType);
		}

		// Token: 0x06004961 RID: 18785 RVA: 0x00184C64 File Offset: 0x00183E64
		private static bool IsCustomAttributeDefined(RuntimeModule decoratedModule, int decoratedMetadataToken, RuntimeType attributeFilterType)
		{
			return CustomAttribute.IsCustomAttributeDefined(decoratedModule, decoratedMetadataToken, attributeFilterType, 0, false);
		}

		// Token: 0x06004962 RID: 18786 RVA: 0x00184C70 File Offset: 0x00183E70
		private static bool IsCustomAttributeDefined(RuntimeModule decoratedModule, int decoratedMetadataToken, RuntimeType attributeFilterType, int attributeCtorToken, bool mustBeInheritable)
		{
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(decoratedModule, decoratedMetadataToken);
			if (attributeFilterType != null)
			{
				MetadataImport metadataImport = decoratedModule.MetadataImport;
				RuntimeType.ListBuilder<object> listBuilder = default(RuntimeType.ListBuilder<object>);
				for (int i = 0; i < customAttributeRecords.Length; i++)
				{
					RuntimeType runtimeType;
					IRuntimeMethodInfo runtimeMethodInfo;
					bool flag;
					if (CustomAttribute.FilterCustomAttributeRecord(customAttributeRecords[i].tkCtor, metadataImport, decoratedModule, decoratedMetadataToken, attributeFilterType, mustBeInheritable, ref listBuilder, out runtimeType, out runtimeMethodInfo, out flag))
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < customAttributeRecords.Length; j++)
				{
					if (customAttributeRecords[j].tkCtor == attributeCtorToken)
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06004963 RID: 18787 RVA: 0x00184D00 File Offset: 0x00183F00
		private static object[] GetCustomAttributes(RuntimeModule decoratedModule, int decoratedMetadataToken, int pcaCount, RuntimeType attributeFilterType)
		{
			RuntimeType.ListBuilder<object> listBuilder = default(RuntimeType.ListBuilder<object>);
			CustomAttribute.AddCustomAttributes(ref listBuilder, decoratedModule, decoratedMetadataToken, attributeFilterType, false, default(RuntimeType.ListBuilder<object>));
			RuntimeType elementType = (attributeFilterType == null || attributeFilterType.IsValueType || attributeFilterType.ContainsGenericParameters) ? ((RuntimeType)typeof(object)) : attributeFilterType;
			object[] array = CustomAttribute.CreateAttributeArrayHelper(elementType, listBuilder.Count + pcaCount);
			for (int i = 0; i < listBuilder.Count; i++)
			{
				array[i] = listBuilder[i];
			}
			return array;
		}

		// Token: 0x06004964 RID: 18788 RVA: 0x00184D90 File Offset: 0x00183F90
		private unsafe static void AddCustomAttributes(ref RuntimeType.ListBuilder<object> attributes, RuntimeModule decoratedModule, int decoratedMetadataToken, RuntimeType attributeFilterType, bool mustBeInheritable, RuntimeType.ListBuilder<object> derivedAttributes)
		{
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(decoratedModule, decoratedMetadataToken);
			if (attributeFilterType == null && customAttributeRecords.Length == 0)
			{
				return;
			}
			MetadataImport metadataImport = decoratedModule.MetadataImport;
			for (int i = 0; i < customAttributeRecords.Length; i++)
			{
				ref CustomAttributeRecord ptr = ref customAttributeRecords[i];
				IntPtr intPtr = ptr.blob.Signature;
				IntPtr intPtr2 = (IntPtr)((void*)((byte*)((void*)intPtr) + ptr.blob.Length));
				RuntimeType runtimeType;
				IRuntimeMethodInfo runtimeMethodInfo;
				bool isVarArg;
				if (CustomAttribute.FilterCustomAttributeRecord(ptr.tkCtor, metadataImport, decoratedModule, decoratedMetadataToken, attributeFilterType, mustBeInheritable, ref derivedAttributes, out runtimeType, out runtimeMethodInfo, out isVarArg))
				{
					RuntimeConstructorInfo.CheckCanCreateInstance(runtimeType, isVarArg);
					int num;
					object obj;
					if (runtimeMethodInfo != null)
					{
						obj = CustomAttribute.CreateCaObject(decoratedModule, runtimeType, runtimeMethodInfo, ref intPtr, intPtr2, out num);
					}
					else
					{
						obj = runtimeType.CreateInstanceDefaultCtor(false, true, true, false);
						if ((int)((long)((byte*)((void*)intPtr2) - (byte*)((void*)intPtr))) == 0)
						{
							num = 0;
						}
						else
						{
							if (Marshal.ReadInt16(intPtr) != 1)
							{
								throw new CustomAttributeFormatException();
							}
							intPtr = (IntPtr)((void*)((byte*)((void*)intPtr) + 2));
							num = (int)Marshal.ReadInt16(intPtr);
							intPtr = (IntPtr)((void*)((byte*)((void*)intPtr) + 2));
						}
					}
					for (int j = 0; j < num; j++)
					{
						string text;
						bool flag;
						RuntimeType runtimeType2;
						object obj2;
						CustomAttribute.GetPropertyOrFieldData(decoratedModule, ref intPtr, intPtr2, out text, out flag, out runtimeType2, out obj2);
						try
						{
							if (flag)
							{
								if (runtimeType2 == null && obj2 != null)
								{
									runtimeType2 = (RuntimeType)obj2.GetType();
									if (runtimeType2 == CustomAttribute.Type_RuntimeType)
									{
										runtimeType2 = CustomAttribute.Type_Type;
									}
								}
								PropertyInfo propertyInfo = (runtimeType2 == null) ? runtimeType.GetProperty(text) : runtimeType.GetProperty(text, runtimeType2, Type.EmptyTypes);
								if (propertyInfo == null)
								{
									throw new CustomAttributeFormatException(SR.Format(SR.RFLCT_InvalidPropFail, text));
								}
								MethodInfo setMethod = propertyInfo.GetSetMethod(true);
								if (setMethod.IsPublic)
								{
									setMethod.Invoke(obj, BindingFlags.Default, null, new object[]
									{
										obj2
									}, null);
								}
							}
							else
							{
								FieldInfo field = runtimeType.GetField(text);
								field.SetValue(obj, obj2, BindingFlags.Default, Type.DefaultBinder, null);
							}
						}
						catch (Exception inner)
						{
							throw new CustomAttributeFormatException(SR.Format(flag ? SR.RFLCT_InvalidPropFail : SR.RFLCT_InvalidFieldFail, text), inner);
						}
					}
					if (intPtr != intPtr2)
					{
						throw new CustomAttributeFormatException();
					}
					attributes.Add(obj);
				}
			}
		}

		// Token: 0x06004965 RID: 18789 RVA: 0x00184FD0 File Offset: 0x001841D0
		private static bool FilterCustomAttributeRecord(MetadataToken caCtorToken, in MetadataImport scope, RuntimeModule decoratedModule, MetadataToken decoratedToken, RuntimeType attributeFilterType, bool mustBeInheritable, ref RuntimeType.ListBuilder<object> derivedAttributes, out RuntimeType attributeType, out IRuntimeMethodInfo ctorWithParameters, out bool isVarArg)
		{
			ctorWithParameters = null;
			isVarArg = false;
			attributeType = (decoratedModule.ResolveType(scope.GetParentToken(caCtorToken), null, null) as RuntimeType);
			if (!attributeFilterType.IsAssignableFrom(attributeType))
			{
				return false;
			}
			if (!CustomAttribute.AttributeUsageCheck(attributeType, mustBeInheritable, ref derivedAttributes))
			{
				return false;
			}
			if ((attributeType.Attributes & TypeAttributes.WindowsRuntime) == TypeAttributes.WindowsRuntime)
			{
				return false;
			}
			ConstArray methodSignature = scope.GetMethodSignature(caCtorToken);
			isVarArg = ((methodSignature[0] & 5) > 0);
			bool flag = methodSignature[1] > 0;
			if (flag)
			{
				if (attributeType.IsGenericType)
				{
					ctorWithParameters = decoratedModule.ResolveMethod(caCtorToken, attributeType.GenericTypeArguments, null).MethodHandle.GetMethodInfo();
				}
				else
				{
					ctorWithParameters = ModuleHandle.ResolveMethodHandleInternal(decoratedModule.GetNativeHandle(), caCtorToken);
				}
			}
			MetadataToken token = default(MetadataToken);
			if (decoratedToken.IsParamDef)
			{
				token = new MetadataToken(scope.GetParentToken(decoratedToken));
				token = new MetadataToken(scope.GetParentToken(token));
			}
			else if (decoratedToken.IsMethodDef || decoratedToken.IsProperty || decoratedToken.IsEvent || decoratedToken.IsFieldDef)
			{
				token = new MetadataToken(scope.GetParentToken(decoratedToken));
			}
			else if (decoratedToken.IsTypeDef)
			{
				token = decoratedToken;
			}
			else if (decoratedToken.IsGenericPar)
			{
				token = new MetadataToken(scope.GetParentToken(decoratedToken));
				if (token.IsMethodDef)
				{
					token = new MetadataToken(scope.GetParentToken(token));
				}
			}
			RuntimeTypeHandle runtimeTypeHandle = token.IsTypeDef ? decoratedModule.ModuleHandle.ResolveTypeHandle(token) : default(RuntimeTypeHandle);
			RuntimeTypeHandle typeHandle = attributeType.TypeHandle;
			bool result = RuntimeMethodHandle.IsCAVisibleFromDecoratedType(new QCallTypeHandle(ref typeHandle), (ctorWithParameters != null) ? ctorWithParameters.Value : RuntimeMethodHandleInternal.EmptyHandle, new QCallTypeHandle(ref runtimeTypeHandle), new QCallModule(ref decoratedModule)) > Interop.BOOL.FALSE;
			GC.KeepAlive(ctorWithParameters);
			return result;
		}

		// Token: 0x06004966 RID: 18790 RVA: 0x001851CC File Offset: 0x001843CC
		private static bool AttributeUsageCheck(RuntimeType attributeType, bool mustBeInheritable, ref RuntimeType.ListBuilder<object> derivedAttributes)
		{
			AttributeUsageAttribute attributeUsageAttribute = null;
			if (mustBeInheritable)
			{
				attributeUsageAttribute = CustomAttribute.GetAttributeUsage(attributeType);
				if (!attributeUsageAttribute.Inherited)
				{
					return false;
				}
			}
			if (derivedAttributes.Count == 0)
			{
				return true;
			}
			for (int i = 0; i < derivedAttributes.Count; i++)
			{
				if (derivedAttributes[i].GetType() == attributeType)
				{
					if (attributeUsageAttribute == null)
					{
						attributeUsageAttribute = CustomAttribute.GetAttributeUsage(attributeType);
					}
					return attributeUsageAttribute.AllowMultiple;
				}
			}
			return true;
		}

		// Token: 0x06004967 RID: 18791 RVA: 0x00185230 File Offset: 0x00184430
		internal static AttributeUsageAttribute GetAttributeUsage(RuntimeType decoratedAttribute)
		{
			RuntimeModule runtimeModule = decoratedAttribute.GetRuntimeModule();
			MetadataImport metadataImport = runtimeModule.MetadataImport;
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(runtimeModule, decoratedAttribute.MetadataToken);
			AttributeUsageAttribute attributeUsageAttribute = null;
			for (int i = 0; i < customAttributeRecords.Length; i++)
			{
				ref CustomAttributeRecord ptr = ref customAttributeRecords[i];
				RuntimeType runtimeType = runtimeModule.ResolveType(metadataImport.GetParentToken(ptr.tkCtor), null, null) as RuntimeType;
				if (!(runtimeType != (RuntimeType)typeof(AttributeUsageAttribute)))
				{
					if (attributeUsageAttribute != null)
					{
						throw new FormatException(SR.Format(SR.Format_AttributeUsage, runtimeType));
					}
					AttributeTargets validOn;
					bool inherited;
					bool allowMultiple;
					CustomAttribute.ParseAttributeUsageAttribute(ptr.blob, out validOn, out inherited, out allowMultiple);
					attributeUsageAttribute = new AttributeUsageAttribute(validOn, allowMultiple, inherited);
				}
			}
			return attributeUsageAttribute ?? AttributeUsageAttribute.Default;
		}

		// Token: 0x06004968 RID: 18792
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void _ParseAttributeUsageAttribute(IntPtr pCa, int cCa, out int targets, out bool inherited, out bool allowMultiple);

		// Token: 0x06004969 RID: 18793 RVA: 0x001852F0 File Offset: 0x001844F0
		private static void ParseAttributeUsageAttribute(ConstArray ca, out AttributeTargets targets, out bool inherited, out bool allowMultiple)
		{
			int num;
			CustomAttribute._ParseAttributeUsageAttribute(ca.Signature, ca.Length, out num, out inherited, out allowMultiple);
			targets = (AttributeTargets)num;
		}

		// Token: 0x0600496A RID: 18794
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern object _CreateCaObject(RuntimeModule pModule, RuntimeType type, IRuntimeMethodInfo pCtor, byte** ppBlob, byte* pEndBlob, int* pcNamedArgs);

		// Token: 0x0600496B RID: 18795 RVA: 0x00185318 File Offset: 0x00184518
		private unsafe static object CreateCaObject(RuntimeModule module, RuntimeType type, IRuntimeMethodInfo ctor, ref IntPtr blob, IntPtr blobEnd, out int namedArgs)
		{
			byte* value = (byte*)((void*)blob);
			byte* pEndBlob = (byte*)((void*)blobEnd);
			int num;
			object result = CustomAttribute._CreateCaObject(module, type, ctor, &value, pEndBlob, &num);
			blob = (IntPtr)((void*)value);
			namedArgs = num;
			return result;
		}

		// Token: 0x0600496C RID: 18796
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern void _GetPropertyOrFieldData(RuntimeModule pModule, byte** ppBlobStart, byte* pBlobEnd, out string name, out bool bIsProperty, out RuntimeType type, out object value);

		// Token: 0x0600496D RID: 18797 RVA: 0x00185354 File Offset: 0x00184554
		private unsafe static void GetPropertyOrFieldData(RuntimeModule module, ref IntPtr blobStart, IntPtr blobEnd, out string name, out bool isProperty, out RuntimeType type, out object value)
		{
			byte* value2 = (byte*)((void*)blobStart);
			CustomAttribute._GetPropertyOrFieldData(module.GetNativeHandle(), &value2, (byte*)((void*)blobEnd), out name, out isProperty, out type, out value);
			blobStart = (IntPtr)((void*)value2);
		}

		// Token: 0x0600496E RID: 18798 RVA: 0x0018538C File Offset: 0x0018458C
		private static object[] CreateAttributeArrayHelper(RuntimeType elementType, int elementCount)
		{
			if (elementCount == 0)
			{
				return elementType.GetEmptyArray();
			}
			return (object[])Array.CreateInstance(elementType, elementCount);
		}

		// Token: 0x04001204 RID: 4612
		private static readonly RuntimeType Type_RuntimeType = (RuntimeType)typeof(RuntimeType);

		// Token: 0x04001205 RID: 4613
		private static readonly RuntimeType Type_Type = (RuntimeType)typeof(Type);
	}
}
