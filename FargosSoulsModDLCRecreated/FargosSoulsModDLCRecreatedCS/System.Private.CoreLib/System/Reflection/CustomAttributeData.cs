using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Reflection
{
	// Token: 0x0200058B RID: 1419
	[NullableContext(1)]
	[Nullable(0)]
	public class CustomAttributeData
	{
		// Token: 0x0600490F RID: 18703 RVA: 0x00183111 File Offset: 0x00182311
		public static IList<CustomAttributeData> GetCustomAttributes(MemberInfo target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x06004910 RID: 18704 RVA: 0x0018312D File Offset: 0x0018232D
		public static IList<CustomAttributeData> GetCustomAttributes(Module target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x06004911 RID: 18705 RVA: 0x00183149 File Offset: 0x00182349
		public static IList<CustomAttributeData> GetCustomAttributes(Assembly target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x06004912 RID: 18706 RVA: 0x00183165 File Offset: 0x00182365
		public static IList<CustomAttributeData> GetCustomAttributes(ParameterInfo target)
		{
			if (target == null)
			{
				throw new ArgumentNullException("target");
			}
			return target.GetCustomAttributesData();
		}

		// Token: 0x06004913 RID: 18707 RVA: 0x0018317C File Offset: 0x0018237C
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeType target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			RuntimeType.ListBuilder<Attribute> listBuilder;
			PseudoCustomAttribute.GetCustomAttributes(target, (RuntimeType)typeof(object), out listBuilder);
			return CustomAttributeData.GetCombinedList(customAttributes, ref listBuilder);
		}

		// Token: 0x06004914 RID: 18708 RVA: 0x001831BC File Offset: 0x001823BC
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeFieldInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			RuntimeType.ListBuilder<Attribute> listBuilder;
			PseudoCustomAttribute.GetCustomAttributes(target, (RuntimeType)typeof(object), out listBuilder);
			return CustomAttributeData.GetCombinedList(customAttributes, ref listBuilder);
		}

		// Token: 0x06004915 RID: 18709 RVA: 0x001831FC File Offset: 0x001823FC
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeMethodInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			RuntimeType.ListBuilder<Attribute> listBuilder;
			PseudoCustomAttribute.GetCustomAttributes(target, (RuntimeType)typeof(object), out listBuilder);
			return CustomAttributeData.GetCombinedList(customAttributes, ref listBuilder);
		}

		// Token: 0x06004916 RID: 18710 RVA: 0x0018323A File Offset: 0x0018243A
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeConstructorInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004917 RID: 18711 RVA: 0x0018324D File Offset: 0x0018244D
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeEventInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004918 RID: 18712 RVA: 0x00183260 File Offset: 0x00182460
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimePropertyInfo target)
		{
			return CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
		}

		// Token: 0x06004919 RID: 18713 RVA: 0x00183273 File Offset: 0x00182473
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeModule target)
		{
			if (target.IsResource())
			{
				return new List<CustomAttributeData>();
			}
			return CustomAttributeData.GetCustomAttributes(target, target.MetadataToken);
		}

		// Token: 0x0600491A RID: 18714 RVA: 0x0018328F File Offset: 0x0018248F
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeAssembly target)
		{
			return CustomAttributeData.GetCustomAttributes((RuntimeModule)target.ManifestModule, RuntimeAssembly.GetToken(target.GetNativeHandle()));
		}

		// Token: 0x0600491B RID: 18715 RVA: 0x001832AC File Offset: 0x001824AC
		internal static IList<CustomAttributeData> GetCustomAttributesInternal(RuntimeParameterInfo target)
		{
			IList<CustomAttributeData> customAttributes = CustomAttributeData.GetCustomAttributes(target.GetRuntimeModule(), target.MetadataToken);
			RuntimeType.ListBuilder<Attribute> listBuilder;
			PseudoCustomAttribute.GetCustomAttributes(target, (RuntimeType)typeof(object), out listBuilder);
			return CustomAttributeData.GetCombinedList(customAttributes, ref listBuilder);
		}

		// Token: 0x0600491C RID: 18716 RVA: 0x001832EC File Offset: 0x001824EC
		private static IList<CustomAttributeData> GetCombinedList(IList<CustomAttributeData> customAttributes, ref RuntimeType.ListBuilder<Attribute> pseudoAttributes)
		{
			if (pseudoAttributes.Count == 0)
			{
				return customAttributes;
			}
			CustomAttributeData[] array = new CustomAttributeData[customAttributes.Count + pseudoAttributes.Count];
			customAttributes.CopyTo(array, pseudoAttributes.Count);
			for (int i = 0; i < pseudoAttributes.Count; i++)
			{
				array[i] = new CustomAttributeData(pseudoAttributes[i]);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x0600491D RID: 18717 RVA: 0x0018334C File Offset: 0x0018254C
		private static CustomAttributeEncoding TypeToCustomAttributeEncoding(RuntimeType type)
		{
			if (type == typeof(int))
			{
				return CustomAttributeEncoding.Int32;
			}
			if (type.IsEnum)
			{
				return CustomAttributeEncoding.Enum;
			}
			if (type == typeof(string))
			{
				return CustomAttributeEncoding.String;
			}
			if (type == typeof(Type))
			{
				return CustomAttributeEncoding.Type;
			}
			if (type == typeof(object))
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsArray)
			{
				return CustomAttributeEncoding.Array;
			}
			if (type == typeof(char))
			{
				return CustomAttributeEncoding.Char;
			}
			if (type == typeof(bool))
			{
				return CustomAttributeEncoding.Boolean;
			}
			if (type == typeof(byte))
			{
				return CustomAttributeEncoding.Byte;
			}
			if (type == typeof(sbyte))
			{
				return CustomAttributeEncoding.SByte;
			}
			if (type == typeof(short))
			{
				return CustomAttributeEncoding.Int16;
			}
			if (type == typeof(ushort))
			{
				return CustomAttributeEncoding.UInt16;
			}
			if (type == typeof(uint))
			{
				return CustomAttributeEncoding.UInt32;
			}
			if (type == typeof(long))
			{
				return CustomAttributeEncoding.Int64;
			}
			if (type == typeof(ulong))
			{
				return CustomAttributeEncoding.UInt64;
			}
			if (type == typeof(float))
			{
				return CustomAttributeEncoding.Float;
			}
			if (type == typeof(double))
			{
				return CustomAttributeEncoding.Double;
			}
			if (type == typeof(Enum))
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsClass)
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsInterface)
			{
				return CustomAttributeEncoding.Object;
			}
			if (type.IsValueType)
			{
				return CustomAttributeEncoding.Undefined;
			}
			throw new ArgumentException(SR.Argument_InvalidKindOfTypeForCA, "type");
		}

		// Token: 0x0600491E RID: 18718 RVA: 0x001834E8 File Offset: 0x001826E8
		private static CustomAttributeType InitCustomAttributeType(RuntimeType parameterType)
		{
			CustomAttributeEncoding customAttributeEncoding = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
			CustomAttributeEncoding customAttributeEncoding2 = CustomAttributeEncoding.Undefined;
			CustomAttributeEncoding encodedEnumType = CustomAttributeEncoding.Undefined;
			string enumName = null;
			if (customAttributeEncoding == CustomAttributeEncoding.Array)
			{
				parameterType = (RuntimeType)parameterType.GetElementType();
				customAttributeEncoding2 = CustomAttributeData.TypeToCustomAttributeEncoding(parameterType);
			}
			if (customAttributeEncoding == CustomAttributeEncoding.Enum || customAttributeEncoding2 == CustomAttributeEncoding.Enum)
			{
				encodedEnumType = CustomAttributeData.TypeToCustomAttributeEncoding((RuntimeType)Enum.GetUnderlyingType(parameterType));
				enumName = parameterType.AssemblyQualifiedName;
			}
			return new CustomAttributeType(customAttributeEncoding, customAttributeEncoding2, encodedEnumType, enumName);
		}

		// Token: 0x0600491F RID: 18719 RVA: 0x00183548 File Offset: 0x00182748
		private static IList<CustomAttributeData> GetCustomAttributes(RuntimeModule module, int tkTarget)
		{
			CustomAttributeRecord[] customAttributeRecords = CustomAttributeData.GetCustomAttributeRecords(module, tkTarget);
			CustomAttributeData[] array = new CustomAttributeData[customAttributeRecords.Length];
			for (int i = 0; i < customAttributeRecords.Length; i++)
			{
				array[i] = new CustomAttributeData(module, customAttributeRecords[i].tkCtor, ref customAttributeRecords[i].blob);
			}
			return Array.AsReadOnly<CustomAttributeData>(array);
		}

		// Token: 0x06004920 RID: 18720 RVA: 0x0018359C File Offset: 0x0018279C
		internal static CustomAttributeRecord[] GetCustomAttributeRecords(RuntimeModule module, int targetToken)
		{
			MetadataImport metadataImport = module.MetadataImport;
			MetadataEnumResult metadataEnumResult;
			metadataImport.EnumCustomAttributes(targetToken, out metadataEnumResult);
			if (metadataEnumResult.Length == 0)
			{
				return Array.Empty<CustomAttributeRecord>();
			}
			CustomAttributeRecord[] array = new CustomAttributeRecord[metadataEnumResult.Length];
			for (int i = 0; i < array.Length; i++)
			{
				metadataImport.GetCustomAttributeProps(metadataEnumResult[i], out array[i].tkCtor.Value, out array[i].blob);
			}
			return array;
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x00183614 File Offset: 0x00182814
		internal static CustomAttributeTypedArgument Filter(IList<CustomAttributeData> attrs, Type caType, int parameter)
		{
			for (int i = 0; i < attrs.Count; i++)
			{
				if (attrs[i].Constructor.DeclaringType == caType)
				{
					return attrs[i].ConstructorArguments[parameter];
				}
			}
			return default(CustomAttributeTypedArgument);
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x000ABD27 File Offset: 0x000AAF27
		protected CustomAttributeData()
		{
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x00183668 File Offset: 0x00182868
		private CustomAttributeData(RuntimeModule scope, MetadataToken caCtorToken, in ConstArray blob)
		{
			this.m_scope = scope;
			this.m_ctor = (RuntimeConstructorInfo)RuntimeType.GetMethodBase(scope, caCtorToken);
			ParameterInfo[] parametersNoCopy = this.m_ctor.GetParametersNoCopy();
			this.m_ctorParams = new CustomAttributeCtorParameter[parametersNoCopy.Length];
			for (int i = 0; i < parametersNoCopy.Length; i++)
			{
				this.m_ctorParams[i] = new CustomAttributeCtorParameter(CustomAttributeData.InitCustomAttributeType((RuntimeType)parametersNoCopy[i].ParameterType));
			}
			FieldInfo[] fields = this.m_ctor.DeclaringType.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			PropertyInfo[] properties = this.m_ctor.DeclaringType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			this.m_namedParams = new CustomAttributeNamedParameter[properties.Length + fields.Length];
			for (int j = 0; j < fields.Length; j++)
			{
				this.m_namedParams[j] = new CustomAttributeNamedParameter(fields[j].Name, CustomAttributeEncoding.Field, CustomAttributeData.InitCustomAttributeType((RuntimeType)fields[j].FieldType));
			}
			for (int k = 0; k < properties.Length; k++)
			{
				this.m_namedParams[k + fields.Length] = new CustomAttributeNamedParameter(properties[k].Name, CustomAttributeEncoding.Property, CustomAttributeData.InitCustomAttributeType((RuntimeType)properties[k].PropertyType));
			}
			this.m_members = new MemberInfo[fields.Length + properties.Length];
			fields.CopyTo(this.m_members, 0);
			properties.CopyTo(this.m_members, fields.Length);
			CustomAttributeEncodedArgument.ParseAttributeArguments(blob, ref this.m_ctorParams, ref this.m_namedParams, this.m_scope);
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x001837F0 File Offset: 0x001829F0
		internal CustomAttributeData(Attribute attribute)
		{
			DllImportAttribute dllImportAttribute = attribute as DllImportAttribute;
			if (dllImportAttribute != null)
			{
				this.Init(dllImportAttribute);
				return;
			}
			FieldOffsetAttribute fieldOffsetAttribute = attribute as FieldOffsetAttribute;
			if (fieldOffsetAttribute != null)
			{
				this.Init(fieldOffsetAttribute);
				return;
			}
			MarshalAsAttribute marshalAsAttribute = attribute as MarshalAsAttribute;
			if (marshalAsAttribute != null)
			{
				this.Init(marshalAsAttribute);
				return;
			}
			TypeForwardedToAttribute typeForwardedToAttribute = attribute as TypeForwardedToAttribute;
			if (typeForwardedToAttribute != null)
			{
				this.Init(typeForwardedToAttribute);
				return;
			}
			this.Init(attribute);
		}

		// Token: 0x06004925 RID: 18725 RVA: 0x00183854 File Offset: 0x00182A54
		private void Init(DllImportAttribute dllImport)
		{
			Type typeFromHandle = typeof(DllImportAttribute);
			this.m_ctor = typeFromHandle.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(dllImport.Value)
			});
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(new CustomAttributeNamedArgument[]
			{
				new CustomAttributeNamedArgument(typeFromHandle.GetField("EntryPoint"), dllImport.EntryPoint),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CharSet"), dllImport.CharSet),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ExactSpelling"), dllImport.ExactSpelling),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("SetLastError"), dllImport.SetLastError),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("PreserveSig"), dllImport.PreserveSig),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("CallingConvention"), dllImport.CallingConvention),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("BestFitMapping"), dllImport.BestFitMapping),
				new CustomAttributeNamedArgument(typeFromHandle.GetField("ThrowOnUnmappableChar"), dllImport.ThrowOnUnmappableChar)
			});
		}

		// Token: 0x06004926 RID: 18726 RVA: 0x001839BC File Offset: 0x00182BBC
		private void Init(FieldOffsetAttribute fieldOffset)
		{
			this.m_ctor = typeof(FieldOffsetAttribute).GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(fieldOffset.Value)
			});
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(Array.Empty<CustomAttributeNamedArgument>());
		}

		// Token: 0x06004927 RID: 18727 RVA: 0x00183A1C File Offset: 0x00182C1C
		private void Init(MarshalAsAttribute marshalAs)
		{
			Type typeFromHandle = typeof(MarshalAsAttribute);
			this.m_ctor = typeFromHandle.GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(marshalAs.Value)
			});
			int num = 3;
			if (marshalAs.MarshalType != null)
			{
				num++;
			}
			if (marshalAs.MarshalTypeRef != null)
			{
				num++;
			}
			if (marshalAs.MarshalCookie != null)
			{
				num++;
			}
			num++;
			num++;
			if (marshalAs.SafeArrayUserDefinedSubType != null)
			{
				num++;
			}
			CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[num];
			num = 0;
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("ArraySubType"), marshalAs.ArraySubType);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SizeParamIndex"), marshalAs.SizeParamIndex);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SizeConst"), marshalAs.SizeConst);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("IidParameterIndex"), marshalAs.IidParameterIndex);
			array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SafeArraySubType"), marshalAs.SafeArraySubType);
			if (marshalAs.MarshalType != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalType"), marshalAs.MarshalType);
			}
			if (marshalAs.MarshalTypeRef != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalTypeRef"), marshalAs.MarshalTypeRef);
			}
			if (marshalAs.MarshalCookie != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("MarshalCookie"), marshalAs.MarshalCookie);
			}
			if (marshalAs.SafeArrayUserDefinedSubType != null)
			{
				array[num++] = new CustomAttributeNamedArgument(typeFromHandle.GetField("SafeArrayUserDefinedSubType"), marshalAs.SafeArrayUserDefinedSubType);
			}
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
		}

		// Token: 0x06004928 RID: 18728 RVA: 0x00183C38 File Offset: 0x00182E38
		private void Init(TypeForwardedToAttribute forwardedTo)
		{
			Type typeFromHandle = typeof(TypeForwardedToAttribute);
			Type[] types = new Type[]
			{
				typeof(Type)
			};
			this.m_ctor = typeFromHandle.GetConstructor(BindingFlags.Instance | BindingFlags.Public, null, types, null);
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(new CustomAttributeTypedArgument[]
			{
				new CustomAttributeTypedArgument(typeof(Type), forwardedTo.Destination)
			});
			CustomAttributeNamedArgument[] array = Array.Empty<CustomAttributeNamedArgument>();
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
		}

		// Token: 0x06004929 RID: 18729 RVA: 0x00183CB6 File Offset: 0x00182EB6
		private void Init(object pca)
		{
			this.m_ctor = pca.GetType().GetConstructors(BindingFlags.Instance | BindingFlags.Public)[0];
			this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(Array.Empty<CustomAttributeTypedArgument>());
			this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(Array.Empty<CustomAttributeNamedArgument>());
		}

		// Token: 0x0600492A RID: 18730 RVA: 0x00183CF0 File Offset: 0x00182EF0
		public override string ToString()
		{
			string text = "";
			for (int i = 0; i < this.ConstructorArguments.Count; i++)
			{
				text += string.Format((i == 0) ? "{0}" : ", {0}", this.ConstructorArguments[i]);
			}
			string text2 = "";
			for (int j = 0; j < this.NamedArguments.Count; j++)
			{
				text2 += string.Format((j == 0 && text.Length == 0) ? "{0}" : ", {0}", this.NamedArguments[j]);
			}
			return string.Format("[{0}({1}{2})]", this.Constructor.DeclaringType.FullName, text, text2);
		}

		// Token: 0x0600492B RID: 18731 RVA: 0x001686D0 File Offset: 0x001678D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600492C RID: 18732 RVA: 0x000BC2A8 File Offset: 0x000BB4A8
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x17000B09 RID: 2825
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x00183DB1 File Offset: 0x00182FB1
		public virtual Type AttributeType
		{
			get
			{
				return this.Constructor.DeclaringType;
			}
		}

		// Token: 0x17000B0A RID: 2826
		// (get) Token: 0x0600492E RID: 18734 RVA: 0x00183DBE File Offset: 0x00182FBE
		public virtual ConstructorInfo Constructor
		{
			get
			{
				return this.m_ctor;
			}
		}

		// Token: 0x17000B0B RID: 2827
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x00183DC8 File Offset: 0x00182FC8
		public virtual IList<CustomAttributeTypedArgument> ConstructorArguments
		{
			get
			{
				if (this.m_typedCtorArgs == null)
				{
					CustomAttributeTypedArgument[] array = new CustomAttributeTypedArgument[this.m_ctorParams.Length];
					for (int i = 0; i < array.Length; i++)
					{
						CustomAttributeEncodedArgument customAttributeEncodedArgument = this.m_ctorParams[i].CustomAttributeEncodedArgument;
						array[i] = new CustomAttributeTypedArgument(this.m_scope, customAttributeEncodedArgument);
					}
					this.m_typedCtorArgs = Array.AsReadOnly<CustomAttributeTypedArgument>(array);
				}
				return this.m_typedCtorArgs;
			}
		}

		// Token: 0x17000B0C RID: 2828
		// (get) Token: 0x06004930 RID: 18736 RVA: 0x00183E30 File Offset: 0x00183030
		public virtual IList<CustomAttributeNamedArgument> NamedArguments
		{
			get
			{
				if (this.m_namedArgs == null)
				{
					if (this.m_namedParams == null)
					{
						return null;
					}
					int num = 0;
					for (int i = 0; i < this.m_namedParams.Length; i++)
					{
						if (this.m_namedParams[i].EncodedArgument.CustomAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
						{
							num++;
						}
					}
					CustomAttributeNamedArgument[] array = new CustomAttributeNamedArgument[num];
					int j = 0;
					int num2 = 0;
					while (j < this.m_namedParams.Length)
					{
						if (this.m_namedParams[j].EncodedArgument.CustomAttributeType.EncodedType != CustomAttributeEncoding.Undefined)
						{
							array[num2++] = new CustomAttributeNamedArgument(this.m_members[j], new CustomAttributeTypedArgument(this.m_scope, this.m_namedParams[j].EncodedArgument));
						}
						j++;
					}
					this.m_namedArgs = Array.AsReadOnly<CustomAttributeNamedArgument>(array);
				}
				return this.m_namedArgs;
			}
		}

		// Token: 0x040011D4 RID: 4564
		private ConstructorInfo m_ctor;

		// Token: 0x040011D5 RID: 4565
		private readonly RuntimeModule m_scope;

		// Token: 0x040011D6 RID: 4566
		private readonly MemberInfo[] m_members;

		// Token: 0x040011D7 RID: 4567
		private readonly CustomAttributeCtorParameter[] m_ctorParams;

		// Token: 0x040011D8 RID: 4568
		private readonly CustomAttributeNamedParameter[] m_namedParams;

		// Token: 0x040011D9 RID: 4569
		private IList<CustomAttributeTypedArgument> m_typedCtorArgs;

		// Token: 0x040011DA RID: 4570
		private IList<CustomAttributeNamedArgument> m_namedArgs;
	}
}
