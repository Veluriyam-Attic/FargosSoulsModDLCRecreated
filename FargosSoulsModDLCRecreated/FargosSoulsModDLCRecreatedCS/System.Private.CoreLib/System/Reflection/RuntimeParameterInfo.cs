using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005B2 RID: 1458
	internal sealed class RuntimeParameterInfo : ParameterInfo
	{
		// Token: 0x06004B7D RID: 19325 RVA: 0x00189DF8 File Offset: 0x00188FF8
		internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
		{
			ParameterInfo parameterInfo;
			return RuntimeParameterInfo.GetParameters(method, member, sig, out parameterInfo, false);
		}

		// Token: 0x06004B7E RID: 19326 RVA: 0x00189E10 File Offset: 0x00189010
		internal static ParameterInfo GetReturnParameter(IRuntimeMethodInfo method, MemberInfo member, Signature sig)
		{
			ParameterInfo result;
			RuntimeParameterInfo.GetParameters(method, member, sig, out result, true);
			return result;
		}

		// Token: 0x06004B7F RID: 19327 RVA: 0x00189E2C File Offset: 0x0018902C
		internal static ParameterInfo[] GetParameters(IRuntimeMethodInfo methodHandle, MemberInfo member, Signature sig, out ParameterInfo returnParameter, bool fetchReturnParameter)
		{
			returnParameter = null;
			int num = sig.Arguments.Length;
			ParameterInfo[] array = fetchReturnParameter ? null : new ParameterInfo[num];
			int methodDef = RuntimeMethodHandle.GetMethodDef(methodHandle);
			int num2 = 0;
			if (!System.Reflection.MetadataToken.IsNullToken(methodDef))
			{
				MetadataImport metadataImport = RuntimeTypeHandle.GetMetadataImport(RuntimeMethodHandle.GetDeclaringType(methodHandle));
				MetadataEnumResult metadataEnumResult;
				metadataImport.EnumParams(methodDef, out metadataEnumResult);
				num2 = metadataEnumResult.Length;
				if (num2 > num + 1)
				{
					throw new BadImageFormatException(SR.BadImageFormat_ParameterSignatureMismatch);
				}
				for (int i = 0; i < num2; i++)
				{
					int num3 = metadataEnumResult[i];
					int num4;
					ParameterAttributes attributes;
					metadataImport.GetParamDefProps(num3, out num4, out attributes);
					num4--;
					if (fetchReturnParameter && num4 == -1)
					{
						if (returnParameter != null)
						{
							throw new BadImageFormatException(SR.BadImageFormat_ParameterSignatureMismatch);
						}
						returnParameter = new RuntimeParameterInfo(sig, metadataImport, num3, num4, attributes, member);
					}
					else if (!fetchReturnParameter && num4 >= 0)
					{
						if (num4 >= num)
						{
							throw new BadImageFormatException(SR.BadImageFormat_ParameterSignatureMismatch);
						}
						array[num4] = new RuntimeParameterInfo(sig, metadataImport, num3, num4, attributes, member);
					}
				}
			}
			if (fetchReturnParameter)
			{
				if (returnParameter == null)
				{
					returnParameter = new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, -1, ParameterAttributes.None, member);
				}
			}
			else if (num2 < array.Length + 1)
			{
				for (int j = 0; j < array.Length; j++)
				{
					if (array[j] == null)
					{
						array[j] = new RuntimeParameterInfo(sig, MetadataImport.EmptyImport, 0, j, ParameterAttributes.None, member);
					}
				}
			}
			return array;
		}

		// Token: 0x06004B80 RID: 19328 RVA: 0x00189F6C File Offset: 0x0018916C
		internal void SetName(string name)
		{
			this.NameImpl = name;
		}

		// Token: 0x06004B81 RID: 19329 RVA: 0x00189F75 File Offset: 0x00189175
		internal void SetAttributes(ParameterAttributes attributes)
		{
			this.AttrsImpl = attributes;
		}

		// Token: 0x06004B82 RID: 19330 RVA: 0x00189F7E File Offset: 0x0018917E
		internal RuntimeParameterInfo(RuntimeParameterInfo accessor, RuntimePropertyInfo property) : this(accessor, property)
		{
			this.m_signature = property.Signature;
		}

		// Token: 0x06004B83 RID: 19331 RVA: 0x00189F94 File Offset: 0x00189194
		private RuntimeParameterInfo(RuntimeParameterInfo accessor, MemberInfo member)
		{
			this.MemberImpl = member;
			this.m_originalMember = (accessor.MemberImpl as MethodBase);
			this.NameImpl = accessor.Name;
			this.m_nameIsCached = true;
			this.ClassImpl = accessor.ParameterType;
			this.PositionImpl = accessor.Position;
			this.AttrsImpl = accessor.Attributes;
			this.m_tkParamDef = (System.Reflection.MetadataToken.IsNullToken(accessor.MetadataToken) ? 134217728 : accessor.MetadataToken);
			this.m_scope = accessor.m_scope;
		}

		// Token: 0x06004B84 RID: 19332 RVA: 0x0018A024 File Offset: 0x00189224
		private RuntimeParameterInfo(Signature signature, MetadataImport scope, int tkParamDef, int position, ParameterAttributes attributes, MemberInfo member)
		{
			this.PositionImpl = position;
			this.MemberImpl = member;
			this.m_signature = signature;
			this.m_tkParamDef = (System.Reflection.MetadataToken.IsNullToken(tkParamDef) ? 134217728 : tkParamDef);
			this.m_scope = scope;
			this.AttrsImpl = attributes;
			this.ClassImpl = null;
			this.NameImpl = null;
		}

		// Token: 0x06004B85 RID: 19333 RVA: 0x0018A084 File Offset: 0x00189284
		internal RuntimeParameterInfo(MethodInfo owner, string name, Type parameterType, int position)
		{
			this.MemberImpl = owner;
			this.NameImpl = name;
			this.m_nameIsCached = true;
			this.m_noMetadata = true;
			this.ClassImpl = parameterType;
			this.PositionImpl = position;
			this.AttrsImpl = ParameterAttributes.None;
			this.m_tkParamDef = 134217728;
			this.m_scope = MetadataImport.EmptyImport;
		}

		// Token: 0x17000BCF RID: 3023
		// (get) Token: 0x06004B86 RID: 19334 RVA: 0x0018A0E4 File Offset: 0x001892E4
		public override Type ParameterType
		{
			get
			{
				if (this.ClassImpl == null)
				{
					RuntimeType classImpl;
					if (this.PositionImpl == -1)
					{
						classImpl = this.m_signature.ReturnType;
					}
					else
					{
						classImpl = this.m_signature.Arguments[this.PositionImpl];
					}
					this.ClassImpl = classImpl;
				}
				return this.ClassImpl;
			}
		}

		// Token: 0x17000BD0 RID: 3024
		// (get) Token: 0x06004B87 RID: 19335 RVA: 0x0018A138 File Offset: 0x00189338
		public override string Name
		{
			get
			{
				if (!this.m_nameIsCached)
				{
					if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
					{
						string nameImpl = this.m_scope.GetName(this.m_tkParamDef).ToString();
						this.NameImpl = nameImpl;
					}
					this.m_nameIsCached = true;
				}
				return this.NameImpl;
			}
		}

		// Token: 0x17000BD1 RID: 3025
		// (get) Token: 0x06004B88 RID: 19336 RVA: 0x0018A194 File Offset: 0x00189394
		public override bool HasDefaultValue
		{
			get
			{
				if (this.m_noMetadata || this.m_noDefaultValue)
				{
					return false;
				}
				object defaultValueInternal = this.GetDefaultValueInternal(false);
				return defaultValueInternal != DBNull.Value;
			}
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x06004B89 RID: 19337 RVA: 0x0018A1C6 File Offset: 0x001893C6
		public override object DefaultValue
		{
			get
			{
				return this.GetDefaultValue(false);
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06004B8A RID: 19338 RVA: 0x0018A1CF File Offset: 0x001893CF
		public override object RawDefaultValue
		{
			get
			{
				return this.GetDefaultValue(true);
			}
		}

		// Token: 0x06004B8B RID: 19339 RVA: 0x0018A1D8 File Offset: 0x001893D8
		private object GetDefaultValue(bool raw)
		{
			if (this.m_noMetadata)
			{
				return null;
			}
			object obj = this.GetDefaultValueInternal(raw);
			if (obj == DBNull.Value && base.IsOptional)
			{
				obj = Type.Missing;
			}
			return obj;
		}

		// Token: 0x06004B8C RID: 19340 RVA: 0x0018A210 File Offset: 0x00189410
		private object GetDefaultValueInternal(bool raw)
		{
			if (this.m_noDefaultValue)
			{
				return DBNull.Value;
			}
			object obj = null;
			if (this.ParameterType == typeof(DateTime))
			{
				if (raw)
				{
					CustomAttributeTypedArgument customAttributeTypedArgument = CustomAttributeData.Filter(CustomAttributeData.GetCustomAttributes(this), typeof(DateTimeConstantAttribute), 0);
					if (customAttributeTypedArgument.ArgumentType != null)
					{
						return new DateTime((long)customAttributeTypedArgument.Value);
					}
				}
				else
				{
					object[] customAttributes = this.GetCustomAttributes(typeof(DateTimeConstantAttribute), false);
					if (customAttributes != null && customAttributes.Length != 0)
					{
						return ((DateTimeConstantAttribute)customAttributes[0]).Value;
					}
				}
			}
			if (!System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				obj = MdConstant.GetValue(this.m_scope, this.m_tkParamDef, this.ParameterType.GetTypeHandleInternal(), raw);
			}
			if (obj == DBNull.Value)
			{
				if (raw)
				{
					using (IEnumerator<CustomAttributeData> enumerator = CustomAttributeData.GetCustomAttributes(this).GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							CustomAttributeData customAttributeData = enumerator.Current;
							Type declaringType = customAttributeData.Constructor.DeclaringType;
							if (declaringType == typeof(DateTimeConstantAttribute))
							{
								obj = RuntimeParameterInfo.GetRawDateTimeConstant(customAttributeData);
							}
							else if (declaringType == typeof(DecimalConstantAttribute))
							{
								obj = RuntimeParameterInfo.GetRawDecimalConstant(customAttributeData);
							}
							else if (declaringType.IsSubclassOf(RuntimeParameterInfo.s_CustomConstantAttributeType))
							{
								obj = RuntimeParameterInfo.GetRawConstant(customAttributeData);
							}
						}
						goto IL_1A7;
					}
				}
				object[] customAttributes2 = this.GetCustomAttributes(RuntimeParameterInfo.s_CustomConstantAttributeType, false);
				if (customAttributes2.Length != 0)
				{
					obj = ((CustomConstantAttribute)customAttributes2[0]).Value;
				}
				else
				{
					customAttributes2 = this.GetCustomAttributes(RuntimeParameterInfo.s_DecimalConstantAttributeType, false);
					if (customAttributes2.Length != 0)
					{
						obj = ((DecimalConstantAttribute)customAttributes2[0]).Value;
					}
				}
			}
			IL_1A7:
			if (obj == DBNull.Value)
			{
				this.m_noDefaultValue = true;
			}
			return obj;
		}

		// Token: 0x06004B8D RID: 19341 RVA: 0x0018A3E4 File Offset: 0x001895E4
		private static decimal GetRawDecimalConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return (decimal)customAttributeNamedArgument.TypedValue.Value;
				}
			}
			ParameterInfo[] parameters = attr.Constructor.GetParameters();
			IList<CustomAttributeTypedArgument> constructorArguments = attr.ConstructorArguments;
			if (parameters[2].ParameterType == typeof(uint))
			{
				int lo = (int)((uint)constructorArguments[4].Value);
				int mid = (int)((uint)constructorArguments[3].Value);
				int hi = (int)((uint)constructorArguments[2].Value);
				byte b = (byte)constructorArguments[1].Value;
				byte scale = (byte)constructorArguments[0].Value;
				return new decimal(lo, mid, hi, b > 0, scale);
			}
			int lo2 = (int)constructorArguments[4].Value;
			int mid2 = (int)constructorArguments[3].Value;
			int hi2 = (int)constructorArguments[2].Value;
			byte b2 = (byte)constructorArguments[1].Value;
			byte scale2 = (byte)constructorArguments[0].Value;
			return new decimal(lo2, mid2, hi2, b2 > 0, scale2);
		}

		// Token: 0x06004B8E RID: 19342 RVA: 0x0018A59C File Offset: 0x0018979C
		private static DateTime GetRawDateTimeConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return new DateTime((long)customAttributeNamedArgument.TypedValue.Value);
				}
			}
			return new DateTime((long)attr.ConstructorArguments[0].Value);
		}

		// Token: 0x06004B8F RID: 19343 RVA: 0x0018A638 File Offset: 0x00189838
		private static object GetRawConstant(CustomAttributeData attr)
		{
			foreach (CustomAttributeNamedArgument customAttributeNamedArgument in attr.NamedArguments)
			{
				if (customAttributeNamedArgument.MemberInfo.Name.Equals("Value"))
				{
					return customAttributeNamedArgument.TypedValue.Value;
				}
			}
			return DBNull.Value;
		}

		// Token: 0x06004B90 RID: 19344 RVA: 0x0018A6B0 File Offset: 0x001898B0
		internal RuntimeModule GetRuntimeModule()
		{
			RuntimeMethodInfo runtimeMethodInfo = this.Member as RuntimeMethodInfo;
			RuntimeConstructorInfo runtimeConstructorInfo = this.Member as RuntimeConstructorInfo;
			RuntimePropertyInfo runtimePropertyInfo = this.Member as RuntimePropertyInfo;
			if (runtimeMethodInfo != null)
			{
				return runtimeMethodInfo.GetRuntimeModule();
			}
			if (runtimeConstructorInfo != null)
			{
				return runtimeConstructorInfo.GetRuntimeModule();
			}
			if (runtimePropertyInfo != null)
			{
				return runtimePropertyInfo.GetRuntimeModule();
			}
			return null;
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06004B91 RID: 19345 RVA: 0x0018A712 File Offset: 0x00189912
		public override int MetadataToken
		{
			get
			{
				return this.m_tkParamDef;
			}
		}

		// Token: 0x06004B92 RID: 19346 RVA: 0x0018A71A File Offset: 0x0018991A
		public override Type[] GetRequiredCustomModifiers()
		{
			if (this.m_signature != null)
			{
				return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, true);
			}
			return Type.EmptyTypes;
		}

		// Token: 0x06004B93 RID: 19347 RVA: 0x0018A73E File Offset: 0x0018993E
		public override Type[] GetOptionalCustomModifiers()
		{
			if (this.m_signature != null)
			{
				return this.m_signature.GetCustomModifiers(this.PositionImpl + 1, false);
			}
			return Type.EmptyTypes;
		}

		// Token: 0x06004B94 RID: 19348 RVA: 0x0018A762 File Offset: 0x00189962
		public override object[] GetCustomAttributes(bool inherit)
		{
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return Array.Empty<object>();
			}
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004B95 RID: 19349 RVA: 0x0018A78C File Offset: 0x0018998C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return Array.Empty<object>();
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x06004B96 RID: 19350 RVA: 0x0018A7EC File Offset: 0x001899EC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			if (System.Reflection.MetadataToken.IsNullToken(this.m_tkParamDef))
			{
				return false;
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x06004B97 RID: 19351 RVA: 0x0018A848 File Offset: 0x00189A48
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x040012B8 RID: 4792
		private static readonly Type s_DecimalConstantAttributeType = typeof(DecimalConstantAttribute);

		// Token: 0x040012B9 RID: 4793
		private static readonly Type s_CustomConstantAttributeType = typeof(CustomConstantAttribute);

		// Token: 0x040012BA RID: 4794
		private int m_tkParamDef;

		// Token: 0x040012BB RID: 4795
		private MetadataImport m_scope;

		// Token: 0x040012BC RID: 4796
		private Signature m_signature;

		// Token: 0x040012BD RID: 4797
		private volatile bool m_nameIsCached;

		// Token: 0x040012BE RID: 4798
		private readonly bool m_noMetadata;

		// Token: 0x040012BF RID: 4799
		private bool m_noDefaultValue;

		// Token: 0x040012C0 RID: 4800
		private MethodBase m_originalMember;
	}
}
