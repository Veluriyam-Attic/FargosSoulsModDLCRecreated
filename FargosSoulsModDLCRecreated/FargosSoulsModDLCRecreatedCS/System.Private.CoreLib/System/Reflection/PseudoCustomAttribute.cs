using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x02000594 RID: 1428
	internal static class PseudoCustomAttribute
	{
		// Token: 0x06004970 RID: 18800 RVA: 0x001853D0 File Offset: 0x001845D0
		private static Dictionary<RuntimeType, RuntimeType> CreatePseudoCustomAttributeDictionary()
		{
			Type[] array = new Type[]
			{
				typeof(FieldOffsetAttribute),
				typeof(SerializableAttribute),
				typeof(MarshalAsAttribute),
				typeof(ComImportAttribute),
				typeof(NonSerializedAttribute),
				typeof(InAttribute),
				typeof(OutAttribute),
				typeof(OptionalAttribute),
				typeof(DllImportAttribute),
				typeof(PreserveSigAttribute),
				typeof(TypeForwardedToAttribute)
			};
			Dictionary<RuntimeType, RuntimeType> dictionary = new Dictionary<RuntimeType, RuntimeType>(array.Length);
			foreach (RuntimeType runtimeType in array)
			{
				dictionary[runtimeType] = runtimeType;
			}
			return dictionary;
		}

		// Token: 0x06004971 RID: 18801 RVA: 0x001854A4 File Offset: 0x001846A4
		internal static void GetCustomAttributes(RuntimeType type, RuntimeType caType, out RuntimeType.ListBuilder<Attribute> pcas)
		{
			pcas = default(RuntimeType.ListBuilder<Attribute>);
			bool flag = caType == typeof(object) || caType == typeof(Attribute);
			if (!flag && !PseudoCustomAttribute.s_pca.ContainsKey(caType))
			{
				return;
			}
			if ((flag || caType == typeof(SerializableAttribute)) && (type.Attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic)
			{
				pcas.Add(new SerializableAttribute());
			}
			if ((flag || caType == typeof(ComImportAttribute)) && (type.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic)
			{
				pcas.Add(new ComImportAttribute());
			}
		}

		// Token: 0x06004972 RID: 18802 RVA: 0x0018554C File Offset: 0x0018474C
		internal static bool IsDefined(RuntimeType type, RuntimeType caType)
		{
			bool flag = caType == typeof(object) || caType == typeof(Attribute);
			return (flag || PseudoCustomAttribute.s_pca.ContainsKey(caType)) && (((flag || caType == typeof(SerializableAttribute)) && (type.Attributes & TypeAttributes.Serializable) != TypeAttributes.NotPublic) || ((flag || caType == typeof(ComImportAttribute)) && (type.Attributes & TypeAttributes.Import) != TypeAttributes.NotPublic));
		}

		// Token: 0x06004973 RID: 18803 RVA: 0x001855DC File Offset: 0x001847DC
		internal static void GetCustomAttributes(RuntimeMethodInfo method, RuntimeType caType, out RuntimeType.ListBuilder<Attribute> pcas)
		{
			pcas = default(RuntimeType.ListBuilder<Attribute>);
			bool flag = caType == typeof(object) || caType == typeof(Attribute);
			if (!flag && !PseudoCustomAttribute.s_pca.ContainsKey(caType))
			{
				return;
			}
			if (flag || caType == typeof(DllImportAttribute))
			{
				Attribute dllImportCustomAttribute = PseudoCustomAttribute.GetDllImportCustomAttribute(method);
				if (dllImportCustomAttribute != null)
				{
					pcas.Add(dllImportCustomAttribute);
				}
			}
			if ((flag || caType == typeof(PreserveSigAttribute)) && (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL)
			{
				pcas.Add(new PreserveSigAttribute());
			}
		}

		// Token: 0x06004974 RID: 18804 RVA: 0x0018567C File Offset: 0x0018487C
		internal static bool IsDefined(RuntimeMethodInfo method, RuntimeType caType)
		{
			bool flag = caType == typeof(object) || caType == typeof(Attribute);
			return (flag || PseudoCustomAttribute.s_pca.ContainsKey(caType)) && (((flag || caType == typeof(DllImportAttribute)) && (method.Attributes & MethodAttributes.PinvokeImpl) != MethodAttributes.PrivateScope) || ((flag || caType == typeof(PreserveSigAttribute)) && (method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) != MethodImplAttributes.IL));
		}

		// Token: 0x06004975 RID: 18805 RVA: 0x0018570C File Offset: 0x0018490C
		internal static void GetCustomAttributes(RuntimeParameterInfo parameter, RuntimeType caType, out RuntimeType.ListBuilder<Attribute> pcas)
		{
			pcas = default(RuntimeType.ListBuilder<Attribute>);
			bool flag = caType == typeof(object) || caType == typeof(Attribute);
			if (!flag && !PseudoCustomAttribute.s_pca.ContainsKey(caType))
			{
				return;
			}
			if ((flag || caType == typeof(InAttribute)) && parameter.IsIn)
			{
				pcas.Add(new InAttribute());
			}
			if ((flag || caType == typeof(OutAttribute)) && parameter.IsOut)
			{
				pcas.Add(new OutAttribute());
			}
			if ((flag || caType == typeof(OptionalAttribute)) && parameter.IsOptional)
			{
				pcas.Add(new OptionalAttribute());
			}
			if (flag || caType == typeof(MarshalAsAttribute))
			{
				Attribute marshalAsCustomAttribute = PseudoCustomAttribute.GetMarshalAsCustomAttribute(parameter);
				if (marshalAsCustomAttribute != null)
				{
					pcas.Add(marshalAsCustomAttribute);
				}
			}
		}

		// Token: 0x06004976 RID: 18806 RVA: 0x001857F8 File Offset: 0x001849F8
		internal static bool IsDefined(RuntimeParameterInfo parameter, RuntimeType caType)
		{
			bool flag = caType == typeof(object) || caType == typeof(Attribute);
			return (flag || PseudoCustomAttribute.s_pca.ContainsKey(caType)) && (((flag || caType == typeof(InAttribute)) && parameter.IsIn) || ((flag || caType == typeof(OutAttribute)) && parameter.IsOut) || ((flag || caType == typeof(OptionalAttribute)) && parameter.IsOptional) || ((flag || caType == typeof(MarshalAsAttribute)) && PseudoCustomAttribute.GetMarshalAsCustomAttribute(parameter) != null));
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x001858BC File Offset: 0x00184ABC
		internal static void GetCustomAttributes(RuntimeFieldInfo field, RuntimeType caType, out RuntimeType.ListBuilder<Attribute> pcas)
		{
			pcas = default(RuntimeType.ListBuilder<Attribute>);
			bool flag = caType == typeof(object) || caType == typeof(Attribute);
			if (!flag && !PseudoCustomAttribute.s_pca.ContainsKey(caType))
			{
				return;
			}
			if (flag || caType == typeof(MarshalAsAttribute))
			{
				Attribute attribute = PseudoCustomAttribute.GetMarshalAsCustomAttribute(field);
				if (attribute != null)
				{
					pcas.Add(attribute);
				}
			}
			if (flag || caType == typeof(FieldOffsetAttribute))
			{
				Attribute attribute = PseudoCustomAttribute.GetFieldOffsetCustomAttribute(field);
				if (attribute != null)
				{
					pcas.Add(attribute);
				}
			}
			if ((flag || caType == typeof(NonSerializedAttribute)) && (field.Attributes & FieldAttributes.NotSerialized) != FieldAttributes.PrivateScope)
			{
				pcas.Add(new NonSerializedAttribute());
			}
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x00185984 File Offset: 0x00184B84
		internal static bool IsDefined(RuntimeFieldInfo field, RuntimeType caType)
		{
			bool flag = caType == typeof(object) || caType == typeof(Attribute);
			return (flag || PseudoCustomAttribute.s_pca.ContainsKey(caType)) && (((flag || caType == typeof(MarshalAsAttribute)) && PseudoCustomAttribute.GetMarshalAsCustomAttribute(field) != null) || ((flag || caType == typeof(FieldOffsetAttribute)) && PseudoCustomAttribute.GetFieldOffsetCustomAttribute(field) != null) || ((flag || caType == typeof(NonSerializedAttribute)) && (field.Attributes & FieldAttributes.NotSerialized) != FieldAttributes.PrivateScope));
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x00185A30 File Offset: 0x00184C30
		private static DllImportAttribute GetDllImportCustomAttribute(RuntimeMethodInfo method)
		{
			if ((method.Attributes & MethodAttributes.PinvokeImpl) == MethodAttributes.PrivateScope)
			{
				return null;
			}
			MetadataImport metadataImport = ModuleHandle.GetMetadataImport(method.Module.ModuleHandle.GetRuntimeModule());
			int metadataToken = method.MetadataToken;
			PInvokeAttributes pinvokeAttributes;
			string entryPoint;
			string dllName;
			metadataImport.GetPInvokeMap(metadataToken, out pinvokeAttributes, out entryPoint, out dllName);
			CharSet charSet = CharSet.None;
			switch (pinvokeAttributes & PInvokeAttributes.CharSetMask)
			{
			case PInvokeAttributes.CharSetNotSpec:
				charSet = CharSet.None;
				break;
			case PInvokeAttributes.CharSetAnsi:
				charSet = CharSet.Ansi;
				break;
			case PInvokeAttributes.CharSetUnicode:
				charSet = CharSet.Unicode;
				break;
			case PInvokeAttributes.CharSetMask:
				charSet = CharSet.Auto;
				break;
			}
			CallingConvention callingConvention = CallingConvention.Cdecl;
			PInvokeAttributes pinvokeAttributes2 = pinvokeAttributes & PInvokeAttributes.CallConvMask;
			if (pinvokeAttributes2 <= PInvokeAttributes.CallConvCdecl)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvWinapi)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvCdecl)
					{
						callingConvention = CallingConvention.Cdecl;
					}
				}
				else
				{
					callingConvention = CallingConvention.Winapi;
				}
			}
			else if (pinvokeAttributes2 != PInvokeAttributes.CallConvStdcall)
			{
				if (pinvokeAttributes2 != PInvokeAttributes.CallConvThiscall)
				{
					if (pinvokeAttributes2 == PInvokeAttributes.CallConvFastcall)
					{
						callingConvention = CallingConvention.FastCall;
					}
				}
				else
				{
					callingConvention = CallingConvention.ThisCall;
				}
			}
			else
			{
				callingConvention = CallingConvention.StdCall;
			}
			return new DllImportAttribute(dllName)
			{
				EntryPoint = entryPoint,
				CharSet = charSet,
				SetLastError = ((pinvokeAttributes & PInvokeAttributes.SupportsLastError) > PInvokeAttributes.CharSetNotSpec),
				ExactSpelling = ((pinvokeAttributes & PInvokeAttributes.NoMangle) > PInvokeAttributes.CharSetNotSpec),
				PreserveSig = ((method.GetMethodImplementationFlags() & MethodImplAttributes.PreserveSig) > MethodImplAttributes.IL),
				CallingConvention = callingConvention,
				BestFitMapping = ((pinvokeAttributes & PInvokeAttributes.BestFitMask) == PInvokeAttributes.BestFitEnabled),
				ThrowOnUnmappableChar = ((pinvokeAttributes & PInvokeAttributes.ThrowOnUnmappableCharMask) == PInvokeAttributes.ThrowOnUnmappableCharEnabled)
			};
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x00185B92 File Offset: 0x00184D92
		private static MarshalAsAttribute GetMarshalAsCustomAttribute(RuntimeParameterInfo parameter)
		{
			return PseudoCustomAttribute.GetMarshalAsCustomAttribute(parameter.MetadataToken, parameter.GetRuntimeModule());
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x00185BA5 File Offset: 0x00184DA5
		private static MarshalAsAttribute GetMarshalAsCustomAttribute(RuntimeFieldInfo field)
		{
			return PseudoCustomAttribute.GetMarshalAsCustomAttribute(field.MetadataToken, field.GetRuntimeModule());
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x00185BB8 File Offset: 0x00184DB8
		private static MarshalAsAttribute GetMarshalAsCustomAttribute(int token, RuntimeModule scope)
		{
			ConstArray fieldMarshal = ModuleHandle.GetMetadataImport(scope.GetNativeHandle()).GetFieldMarshal(token);
			if (fieldMarshal.Length == 0)
			{
				return null;
			}
			UnmanagedType unmanagedType;
			VarEnum safeArraySubType;
			string text;
			UnmanagedType arraySubType;
			int num;
			int sizeConst;
			string text2;
			string marshalCookie;
			int iidParameterIndex;
			MetadataImport.GetMarshalAs(fieldMarshal, out unmanagedType, out safeArraySubType, out text, out arraySubType, out num, out sizeConst, out text2, out marshalCookie, out iidParameterIndex);
			RuntimeType safeArrayUserDefinedSubType = string.IsNullOrEmpty(text) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text, scope);
			RuntimeType marshalTypeRef = null;
			try
			{
				marshalTypeRef = ((text2 == null) ? null : RuntimeTypeHandle.GetTypeByNameUsingCARules(text2, scope));
			}
			catch (TypeLoadException)
			{
			}
			return new MarshalAsAttribute(unmanagedType)
			{
				SafeArraySubType = safeArraySubType,
				SafeArrayUserDefinedSubType = safeArrayUserDefinedSubType,
				IidParameterIndex = iidParameterIndex,
				ArraySubType = arraySubType,
				SizeParamIndex = (short)num,
				SizeConst = sizeConst,
				MarshalType = text2,
				MarshalTypeRef = marshalTypeRef,
				MarshalCookie = marshalCookie
			};
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x00185C98 File Offset: 0x00184E98
		private static FieldOffsetAttribute GetFieldOffsetCustomAttribute(RuntimeFieldInfo field)
		{
			int offset;
			if (field.DeclaringType != null && field.GetRuntimeModule().MetadataImport.GetFieldOffset(field.DeclaringType.MetadataToken, field.MetadataToken, out offset))
			{
				return new FieldOffsetAttribute(offset);
			}
			return null;
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00185CE4 File Offset: 0x00184EE4
		internal static StructLayoutAttribute GetStructLayoutCustomAttribute(RuntimeType type)
		{
			if (type.IsInterface || type.HasElementType || type.IsGenericParameter)
			{
				return null;
			}
			LayoutKind layoutKind = LayoutKind.Auto;
			TypeAttributes typeAttributes = type.Attributes & TypeAttributes.LayoutMask;
			if (typeAttributes != TypeAttributes.NotPublic)
			{
				if (typeAttributes != TypeAttributes.SequentialLayout)
				{
					if (typeAttributes == TypeAttributes.ExplicitLayout)
					{
						layoutKind = LayoutKind.Explicit;
					}
				}
				else
				{
					layoutKind = LayoutKind.Sequential;
				}
			}
			else
			{
				layoutKind = LayoutKind.Auto;
			}
			CharSet charSet = CharSet.None;
			TypeAttributes typeAttributes2 = type.Attributes & TypeAttributes.StringFormatMask;
			if (typeAttributes2 != TypeAttributes.NotPublic)
			{
				if (typeAttributes2 != TypeAttributes.UnicodeClass)
				{
					if (typeAttributes2 == TypeAttributes.AutoClass)
					{
						charSet = CharSet.Auto;
					}
				}
				else
				{
					charSet = CharSet.Unicode;
				}
			}
			else
			{
				charSet = CharSet.Ansi;
			}
			int num;
			int size;
			type.GetRuntimeModule().MetadataImport.GetClassLayout(type.MetadataToken, out num, out size);
			if (num == 0)
			{
				num = 8;
			}
			return new StructLayoutAttribute(layoutKind)
			{
				Pack = num,
				Size = size,
				CharSet = charSet
			};
		}

		// Token: 0x04001206 RID: 4614
		private static readonly Dictionary<RuntimeType, RuntimeType> s_pca = PseudoCustomAttribute.CreatePseudoCustomAttributeDictionary();
	}
}
