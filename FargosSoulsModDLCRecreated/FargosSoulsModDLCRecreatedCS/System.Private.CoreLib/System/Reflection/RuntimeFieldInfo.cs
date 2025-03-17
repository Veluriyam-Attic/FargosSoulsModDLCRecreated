using System;
using System.Collections.Generic;

namespace System.Reflection
{
	// Token: 0x020005AD RID: 1453
	internal abstract class RuntimeFieldInfo : FieldInfo
	{
		// Token: 0x06004AF9 RID: 19193 RVA: 0x00188658 File Offset: 0x00187858
		protected RuntimeFieldInfo(RuntimeType.RuntimeTypeCache reflectedTypeCache, RuntimeType declaringType, BindingFlags bindingFlags)
		{
			this.m_bindingFlags = bindingFlags;
			this.m_declaringType = declaringType;
			this.m_reflectedTypeCache = reflectedTypeCache;
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06004AFA RID: 19194 RVA: 0x00188675 File Offset: 0x00187875
		internal BindingFlags BindingFlags
		{
			get
			{
				return this.m_bindingFlags;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06004AFB RID: 19195 RVA: 0x0018867D File Offset: 0x0018787D
		private RuntimeType ReflectedTypeInternal
		{
			get
			{
				return this.m_reflectedTypeCache.GetRuntimeType();
			}
		}

		// Token: 0x06004AFC RID: 19196 RVA: 0x0018868A File Offset: 0x0018788A
		internal RuntimeType GetDeclaringTypeInternal()
		{
			return this.m_declaringType;
		}

		// Token: 0x06004AFD RID: 19197 RVA: 0x0018868A File Offset: 0x0018788A
		internal RuntimeType GetRuntimeType()
		{
			return this.m_declaringType;
		}

		// Token: 0x06004AFE RID: 19198
		internal abstract RuntimeModule GetRuntimeModule();

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06004AFF RID: 19199 RVA: 0x000CA38E File Offset: 0x000C958E
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06004B00 RID: 19200 RVA: 0x00188692 File Offset: 0x00187892
		public override Type ReflectedType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.ReflectedTypeInternal;
				}
				return null;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06004B01 RID: 19201 RVA: 0x001886A9 File Offset: 0x001878A9
		public override Type DeclaringType
		{
			get
			{
				if (!this.m_reflectedTypeCache.IsGlobal)
				{
					return this.m_declaringType;
				}
				return null;
			}
		}

		// Token: 0x06004B02 RID: 19202 RVA: 0x001886C0 File Offset: 0x001878C0
		public sealed override bool HasSameMetadataDefinitionAs(MemberInfo other)
		{
			return base.HasSameMetadataDefinitionAsCore<RuntimeFieldInfo>(other);
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06004B03 RID: 19203 RVA: 0x001886C9 File Offset: 0x001878C9
		public override Module Module
		{
			get
			{
				return this.GetRuntimeModule();
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06004B04 RID: 19204 RVA: 0x001886D1 File Offset: 0x001878D1
		public override bool IsCollectible
		{
			get
			{
				return this.m_declaringType.IsCollectible;
			}
		}

		// Token: 0x06004B05 RID: 19205 RVA: 0x001886DE File Offset: 0x001878DE
		public override string ToString()
		{
			return this.FieldType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x06004B06 RID: 19206 RVA: 0x001886FB File Offset: 0x001878FB
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004B07 RID: 19207 RVA: 0x00188714 File Offset: 0x00187914
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

		// Token: 0x06004B08 RID: 19208 RVA: 0x00188764 File Offset: 0x00187964
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

		// Token: 0x06004B09 RID: 19209 RVA: 0x001887B1 File Offset: 0x001879B1
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x04001299 RID: 4761
		private BindingFlags m_bindingFlags;

		// Token: 0x0400129A RID: 4762
		protected RuntimeType.RuntimeTypeCache m_reflectedTypeCache;

		// Token: 0x0400129B RID: 4763
		protected RuntimeType m_declaringType;
	}
}
