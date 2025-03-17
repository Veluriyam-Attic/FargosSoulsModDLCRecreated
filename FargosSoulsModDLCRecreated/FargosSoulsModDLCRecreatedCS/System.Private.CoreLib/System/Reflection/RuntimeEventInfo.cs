using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x020005AB RID: 1451
	internal sealed class RuntimeEventInfo : EventInfo
	{
		// Token: 0x06004ADA RID: 19162 RVA: 0x00188118 File Offset: 0x00187318
		internal RuntimeEventInfo(int tkEvent, RuntimeType declaredType, RuntimeType.RuntimeTypeCache reflectedTypeCache, out bool isPrivate)
		{
			MetadataImport metadataImport = declaredType.GetRuntimeModule().MetadataImport;
			this.m_token = tkEvent;
			this.m_reflectedTypeCache = reflectedTypeCache;
			this.m_declaringType = declaredType;
			RuntimeType runtimeType = reflectedTypeCache.GetRuntimeType();
			metadataImport.GetEventProps(tkEvent, out this.m_utf8name, out this.m_flags);
			RuntimeMethodInfo runtimeMethodInfo;
			RuntimeMethodInfo runtimeMethodInfo2;
			Associates.AssignAssociates(metadataImport, tkEvent, declaredType, runtimeType, out this.m_addMethod, out this.m_removeMethod, out this.m_raiseMethod, out runtimeMethodInfo, out runtimeMethodInfo2, out this.m_otherMethod, out isPrivate, out this.m_bindingFlags);
		}

		// Token: 0x06004ADB RID: 19163 RVA: 0x00188194 File Offset: 0x00187394
		internal override bool CacheEquals(object o)
		{
			RuntimeEventInfo runtimeEventInfo = o as RuntimeEventInfo;
			return runtimeEventInfo != null && runtimeEventInfo.m_token == this.m_token && RuntimeTypeHandle.GetModule(this.m_declaringType).Equals(RuntimeTypeHandle.GetModule(runtimeEventInfo.m_declaringType));
		}

		// Token: 0x17000B8E RID: 2958
		// (get) Token: 0x06004ADC RID: 19164 RVA: 0x001881D6 File Offset: 0x001873D6
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x06004ADD RID: 19165 RVA: 0x001881E0 File Offset: 0x001873E0
		public override string ToString()
		{
			if (this.m_addMethod == null || this.m_addMethod.GetParametersNoCopy().Length == 0)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NoPublicAddMethod);
			}
			return this.m_addMethod.GetParametersNoCopy()[0].ParameterType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x06004ADE RID: 19166 RVA: 0x0018823B File Offset: 0x0018743B
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004ADF RID: 19167 RVA: 0x00188254 File Offset: 0x00187454
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

		// Token: 0x06004AE0 RID: 19168 RVA: 0x001882A4 File Offset: 0x001874A4
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

		// Token: 0x06004AE1 RID: 19169 RVA: 0x001882F1 File Offset: 0x001874F1
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x06004AE2 RID: 19170 RVA: 0x000CE630 File Offset: 0x000CD830
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Event;
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x06004AE3 RID: 19171 RVA: 0x001882FC File Offset: 0x001874FC
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

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06004AE4 RID: 19172 RVA: 0x00188335 File Offset: 0x00187535
		public override Type DeclaringType
		{
			get
			{
				return this.m_declaringType;
			}
		}

		// Token: 0x06004AE5 RID: 19173 RVA: 0x0018833D File Offset: 0x0018753D
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeEventInfo>(other);
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06004AE6 RID: 19174 RVA: 0x00188346 File Offset: 0x00187546
		public override Type ReflectedType
		{
			get
			{
				return this.ReflectedTypeInternal;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06004AE7 RID: 19175 RVA: 0x0018834E File Offset: 0x0018754E
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06004AE8 RID: 19176 RVA: 0x0018835B File Offset: 0x0018755B
		public override int MetadataToken
		{
			get
			{
				return this.m_token;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06004AE9 RID: 19177 RVA: 0x00188363 File Offset: 0x00187563
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x06004AEA RID: 19178 RVA: 0x0018836B File Offset: 0x0018756B
		internal RuntimeModule GetRuntimeModule()
		{
			return this.m_declaringType.GetRuntimeModule();
		}

		// Token: 0x06004AEB RID: 19179 RVA: 0x00188378 File Offset: 0x00187578
		public override MethodInfo[] GetOtherMethods(bool nonPublic)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			if (this.m_otherMethod == null)
			{
				return Array.Empty<MethodInfo>();
			}
			for (int i = 0; i < this.m_otherMethod.Length; i++)
			{
				if (Associates.IncludeAccessor(this.m_otherMethod[i], nonPublic))
				{
					list.Add(this.m_otherMethod[i]);
				}
			}
			return list.ToArray();
		}

		// Token: 0x06004AEC RID: 19180 RVA: 0x001883D0 File Offset: 0x001875D0
		public override MethodInfo GetAddMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_addMethod, nonPublic))
			{
				return null;
			}
			return this.m_addMethod;
		}

		// Token: 0x06004AED RID: 19181 RVA: 0x001883E8 File Offset: 0x001875E8
		public override MethodInfo GetRemoveMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_removeMethod, nonPublic))
			{
				return null;
			}
			return this.m_removeMethod;
		}

		// Token: 0x06004AEE RID: 19182 RVA: 0x00188400 File Offset: 0x00187600
		public override MethodInfo GetRaiseMethod(bool nonPublic)
		{
			if (!Associates.IncludeAccessor(this.m_raiseMethod, nonPublic))
			{
				return null;
			}
			return this.m_raiseMethod;
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06004AEF RID: 19183 RVA: 0x00188418 File Offset: 0x00187618
		public override EventAttributes Attributes
		{
			get
			{
				return this.m_flags;
			}
		}

		// Token: 0x04001286 RID: 4742
		private int m_token;

		// Token: 0x04001287 RID: 4743
		private EventAttributes m_flags;

		// Token: 0x04001288 RID: 4744
		private string m_name;

		// Token: 0x04001289 RID: 4745
		private unsafe void* m_utf8name;

		// Token: 0x0400128A RID: 4746
		private RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x0400128B RID: 4747
		private RuntimeMethodInfo m_addMethod;

		// Token: 0x0400128C RID: 4748
		private RuntimeMethodInfo m_removeMethod;

		// Token: 0x0400128D RID: 4749
		private RuntimeMethodInfo m_raiseMethod;

		// Token: 0x0400128E RID: 4750
		private MethodInfo[] m_otherMethod;

		// Token: 0x0400128F RID: 4751
		private RuntimeType m_declaringType;

		// Token: 0x04001290 RID: 4752
		private BindingFlags m_bindingFlags;
	}
}
