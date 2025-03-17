using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020005B3 RID: 1459
	internal sealed class RuntimePropertyInfo : PropertyInfo
	{
		// Token: 0x06004B99 RID: 19353 RVA: 0x0018A870 File Offset: 0x00189A70
		internal RuntimePropertyInfo(int tkProperty, RuntimeType declaredType, RuntimeType.RuntimeTypeCache reflectedTypeCache, out bool isPrivate)
		{
			MetadataImport metadataImport = declaredType.GetRuntimeModule().MetadataImport;
			this.m_token = tkProperty;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaredType;
			ConstArray constArray;
			metadataImport.GetPropertyProps(tkProperty, out this.m_utf8name, out this.m_flags, out constArray);
			RuntimeMethodInfo runtimeMethodInfo;
			RuntimeMethodInfo runtimeMethodInfo2;
			RuntimeMethodInfo runtimeMethodInfo3;
			Associates.AssignAssociates(metadataImport, tkProperty, declaredType, reflectedTypeCache.GetRuntimeType(), out runtimeMethodInfo, out runtimeMethodInfo2, out runtimeMethodInfo3, out this.m_getterMethod, out this.m_setterMethod, out this.m_otherMethod, out isPrivate, out this.m_bindingFlags);
		}

		// Token: 0x06004B9A RID: 19354 RVA: 0x0018A8E8 File Offset: 0x00189AE8
		internal override bool CacheEquals(object o)
		{
			RuntimePropertyInfo runtimePropertyInfo = o as RuntimePropertyInfo;
			return runtimePropertyInfo != null && runtimePropertyInfo.m_token == this.m_token && RuntimeTypeHandle.GetModule(this.m_declaringType).Equals(RuntimeTypeHandle.GetModule(runtimePropertyInfo.m_declaringType));
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06004B9B RID: 19355 RVA: 0x0018A92C File Offset: 0x00189B2C
		internal unsafe Signature Signature
		{
			get
			{
				if (this.m_signature == null)
				{
					void* ptr;
					PropertyAttributes propertyAttributes;
					ConstArray constArray;
					this.GetRuntimeModule().MetadataImport.GetPropertyProps(this.m_token, out ptr, out propertyAttributes, out constArray);
					this.m_signature = new Signature(constArray.Signature.ToPointer(), constArray.Length, this.m_declaringType);
				}
				return this.m_signature;
			}
		}

		// Token: 0x06004B9C RID: 19356 RVA: 0x0018A98E File Offset: 0x00189B8E
		internal bool EqualsSig(RuntimePropertyInfo target)
		{
			return Signature.CompareSig(this.Signature, target.Signature);
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06004B9D RID: 19357 RVA: 0x0018A9A1 File Offset: 0x00189BA1
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x06004B9E RID: 19358 RVA: 0x0018A9AC File Offset: 0x00189BAC
		public override string ToString()
		{
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(100);
			valueStringBuilder.Append(this.PropertyType.FormatTypeName());
			valueStringBuilder.Append(' ');
			valueStringBuilder.Append(this.Name);
			RuntimeType[] arguments = this.Signature.Arguments;
			if (arguments.Length != 0)
			{
				valueStringBuilder.Append(" [");
				Type[] parameterTypes = arguments;
				MethodBase.AppendParameters(ref valueStringBuilder, parameterTypes, this.Signature.CallingConvention);
				valueStringBuilder.Append(']');
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x06004B9F RID: 19359 RVA: 0x0018AA31 File Offset: 0x00189C31
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004BA0 RID: 19360 RVA: 0x0018AA48 File Offset: 0x00189C48
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x06004BA1 RID: 19361 RVA: 0x0018AA98 File Offset: 0x00189C98
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x06004BA2 RID: 19362 RVA: 0x0018AAE5 File Offset: 0x00189CE5
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x06004BA3 RID: 19363 RVA: 0x000C6BB9 File Offset: 0x000C5DB9
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06004BA4 RID: 19364 RVA: 0x0018AAF0 File Offset: 0x00189CF0
		public override string Name
		{
			get
			{
				string result;
				if ((result = this.m_name) == null)
				{
					result = (this.m_name = new MdUtf8String(this.m_utf8name).ToString());
				}
				return result;
			}
		}

		// Token: 0x17000BD9 RID: 3033
		// (get) Token: 0x06004BA5 RID: 19365 RVA: 0x0018AB29 File Offset: 0x00189D29
		public override Type DeclaringType
		{
			get
			{
				return this.m_declaringType;
			}
		}

		// Token: 0x06004BA6 RID: 19366 RVA: 0x0018AB31 File Offset: 0x00189D31
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimePropertyInfo>(other);
		}

		// Token: 0x17000BDA RID: 3034
		// (get) Token: 0x06004BA7 RID: 19367 RVA: 0x0018AB3A File Offset: 0x00189D3A
		public override Type ReflectedType
		{
			get
			{
				return this.ReflectedTypeInternal;
			}
		}

		// Token: 0x17000BDB RID: 3035
		// (get) Token: 0x06004BA8 RID: 19368 RVA: 0x0018AB42 File Offset: 0x00189D42
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000BDC RID: 3036
		// (get) Token: 0x06004BA9 RID: 19369 RVA: 0x0018AB4F File Offset: 0x00189D4F
		public override int MetadataToken
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x17000BDD RID: 3037
		// (get) Token: 0x06004BAA RID: 19370 RVA: 0x0018AB57 File Offset: 0x00189D57
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004BAB RID: 19371 RVA: 0x0018AB5F File Offset: 0x00189D5F
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x17000BDE RID: 3038
		// (get) Token: 0x06004BAC RID: 19372 RVA: 0x0018AB6C File Offset: 0x00189D6C
		public override bool IsCollectible
		{
			get
			{
				return this.m_declaringType.IsCollectible;
			}
		}

		// Token: 0x06004BAD RID: 19373 RVA: 0x0018AB79 File Offset: 0x00189D79
		public override Type[] GetRequiredCustomModifiers()
		{
			return this.Signature.GetCustomModifiers(0, true);
		}

		// Token: 0x06004BAE RID: 19374 RVA: 0x0018AB88 File Offset: 0x00189D88
		public override Type[] GetOptionalCustomModifiers()
		{
			return this.Signature.GetCustomModifiers(0, false);
		}

		// Token: 0x06004BAF RID: 19375 RVA: 0x0018AB98 File Offset: 0x00189D98
		internal object GetConstantValue(bool raw)
		{
			object value = MdConstant.GetValue(this.GetRuntimeModule().MetadataImport, this.m_token, this.PropertyType.GetTypeHandleInternal(), raw);
			if (value == DBNull.Value)
			{
				throw new InvalidOperationException(SR.Arg_EnumLitValueNotFound);
			}
			return value;
		}

		// Token: 0x06004BB0 RID: 19376 RVA: 0x0018ABDC File Offset: 0x00189DDC
		public override object GetConstantValue()
		{
			return this.GetConstantValue(false);
		}

		// Token: 0x06004BB1 RID: 19377 RVA: 0x0018ABE5 File Offset: 0x00189DE5
		public override object GetRawConstantValue()
		{
			return this.GetConstantValue(true);
		}

		// Token: 0x06004BB2 RID: 19378 RVA: 0x0018ABF0 File Offset: 0x00189DF0
		public override MethodInfo[] GetAccessors(bool nonPublic)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			if (Associates.IncludeAccessor(this.m_getterMethod, nonPublic))
			{
				list.Add(this.m_getterMethod);
			}
			if (Associates.IncludeAccessor(this.m_setterMethod, nonPublic))
			{
				list.Add(this.m_setterMethod);
			}
			if (this.m_otherMethod != null)
			{
				for (int i = 0; i < this.m_otherMethod.Length; i++)
				{
					if (Associates.IncludeAccessor(this.m_otherMethod[i], nonPublic))
					{
						list.Add(this.m_otherMethod[i]);
					}
				}
			}
			return list.ToArray();
		}

		// Token: 0x17000BDF RID: 3039
		// (get) Token: 0x06004BB3 RID: 19379 RVA: 0x0018AC76 File Offset: 0x00189E76
		public override Type PropertyType
		{
			get
			{
				return this.Signature.ReturnType;
			}
		}

		// Token: 0x06004BB4 RID: 19380 RVA: 0x0018AC83 File Offset: 0x00189E83
		public override MethodInfo GetGetMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_getterMethod, nonPublic))
			{
				return null;
			}
			return this.m_getterMethod;
		}

		// Token: 0x06004BB5 RID: 19381 RVA: 0x0018AC9B File Offset: 0x00189E9B
		public override MethodInfo GetSetMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_setterMethod, nonPublic))
			{
				return null;
			}
			return this.m_setterMethod;
		}

		// Token: 0x06004BB6 RID: 19382 RVA: 0x0018ACB4 File Offset: 0x00189EB4
		public override ParameterInfo[] GetIndexParameters()
		{
			ParameterInfo[] indexParametersNoCopy = this.GetIndexParametersNoCopy();
			int num = indexParametersNoCopy.Length;
			if (num == 0)
			{
				return indexParametersNoCopy;
			}
			ParameterInfo[] array = new ParameterInfo[num];
			Array.Copy(indexParametersNoCopy, array, num);
			return array;
		}

		// Token: 0x06004BB7 RID: 19383 RVA: 0x0018ACE4 File Offset: 0x00189EE4
		internal ParameterInfo[] GetIndexParametersNoCopy()
		{
			if (this.m_parameters == null)
			{
				int num = 0;
				ParameterInfo[] array = null;
				MethodInfo methodInfo = this.GetGetMethod(true);
				if (methodInfo != null)
				{
					array = methodInfo.GetParametersNoCopy();
					num = array.Length;
				}
				else
				{
					methodInfo = this.GetSetMethod(true);
					if (methodInfo != null)
					{
						array = methodInfo.GetParametersNoCopy();
						num = array.Length - 1;
					}
				}
				ParameterInfo[] array2 = new ParameterInfo[num];
				for (int i = 0; i < num; i++)
				{
					array2[i] = new RuntimeParameterInfo((RuntimeParameterInfo)array[i], this);
				}
				this.m_parameters = array2;
			}
			return this.m_parameters;
		}

		// Token: 0x17000BE0 RID: 3040
		// (get) Token: 0x06004BB8 RID: 19384 RVA: 0x0018AD70 File Offset: 0x00189F70
		public override PropertyAttributes Attributes
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x17000BE1 RID: 3041
		// (get) Token: 0x06004BB9 RID: 19385 RVA: 0x0018AD78 File Offset: 0x00189F78
		public override bool CanRead
		{
			get
			{
				return this.m_getterMethod != null;
			}
		}

		// Token: 0x17000BE2 RID: 3042
		// (get) Token: 0x06004BBA RID: 19386 RVA: 0x0018AD86 File Offset: 0x00189F86
		public override bool CanWrite
		{
			get
			{
				return this.m_setterMethod != null;
			}
		}

		// Token: 0x06004BBB RID: 19387 RVA: 0x0018AD94 File Offset: 0x00189F94
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, index, null);
		}

		// Token: 0x06004BBC RID: 19388 RVA: 0x0018ADA4 File Offset: 0x00189FA4
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo getMethod = this.GetGetMethod(true);
			if (getMethod == null)
			{
				throw new ArgumentException(SR.Arg_GetMethNotFnd);
			}
			return getMethod.Invoke(obj, invokeAttr, binder, index, null);
		}

		// Token: 0x06004BBD RID: 19389 RVA: 0x0018ADD9 File Offset: 0x00189FD9
		[DebuggerHidden]
		[DebuggerStepThrough]
		public override void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, index, null);
		}

		// Token: 0x06004BBE RID: 19390 RVA: 0x0018ADE8 File Offset: 0x00189FE8
		[DebuggerStepThrough]
		[DebuggerHidden]
		public override void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture)
		{
			MethodInfo setMethod = this.GetSetMethod(true);
			if (setMethod == null)
			{
				throw new ArgumentException(SR.Arg_SetMethNotFnd);
			}
			object[] array;
			if (index != null)
			{
				array = new object[index.Length + 1];
				for (int i = 0; i < index.Length; i++)
				{
					array[i] = index[i];
				}
				array[index.Length] = value;
			}
			else
			{
				array = new object[]
				{
					value
				};
			}
			setMethod.Invoke(obj, invokeAttr, binder, array, culture);
		}

		// Token: 0x040012C1 RID: 4801
		private int m_token;

		// Token: 0x040012C2 RID: 4802
		private string m_name;

		// Token: 0x040012C3 RID: 4803
		private unsafe void* m_utf8name;

		// Token: 0x040012C4 RID: 4804
		private PropertyAttributes m_flags;

		// Token: 0x040012C5 RID: 4805
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x040012C6 RID: 4806
		private RuntimeMethodInfo m_getterMethod;

		// Token: 0x040012C7 RID: 4807
		private RuntimeMethodInfo m_setterMethod;

		// Token: 0x040012C8 RID: 4808
		private MethodInfo[] m_otherMethod;

		// Token: 0x040012C9 RID: 4809
		private RuntimeType m_declaringType;

		// Token: 0x040012CA RID: 4810
		private BindingFlags m_bindingFlags;

		// Token: 0x040012CB RID: 4811
		private Signature m_signature;

		// Token: 0x040012CC RID: 4812
		private ParameterInfo[] m_parameters;
	}
}
