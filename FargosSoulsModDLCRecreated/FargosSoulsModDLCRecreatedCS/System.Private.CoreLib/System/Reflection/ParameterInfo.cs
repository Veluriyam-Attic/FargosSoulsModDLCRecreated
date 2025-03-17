using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace System.Reflection
{
	// Token: 0x020005F2 RID: 1522
	[NullableContext(1)]
	[Nullable(0)]
	public class ParameterInfo : ICustomAttributeProvider, IObjectReference
	{
		// Token: 0x06004CDD RID: 19677 RVA: 0x000ABD27 File Offset: 0x000AAF27
		protected ParameterInfo()
		{
		}

		// Token: 0x17000C2F RID: 3119
		// (get) Token: 0x06004CDE RID: 19678 RVA: 0x0018BFE6 File Offset: 0x0018B1E6
		public virtual ParameterAttributes Attributes
		{
			get
			{
				return this.AttrsImpl;
			}
		}

		// Token: 0x17000C30 RID: 3120
		// (get) Token: 0x06004CDF RID: 19679 RVA: 0x0018BFEE File Offset: 0x0018B1EE
		public virtual MemberInfo Member
		{
			get
			{
				return this.MemberImpl;
			}
		}

		// Token: 0x17000C31 RID: 3121
		// (get) Token: 0x06004CE0 RID: 19680 RVA: 0x0018BFF6 File Offset: 0x0018B1F6
		[Nullable(2)]
		public virtual string Name
		{
			[NullableContext(2)]
			get
			{
				return this.NameImpl;
			}
		}

		// Token: 0x17000C32 RID: 3122
		// (get) Token: 0x06004CE1 RID: 19681 RVA: 0x0018BFFE File Offset: 0x0018B1FE
		public virtual Type ParameterType
		{
			get
			{
				return this.ClassImpl;
			}
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x06004CE2 RID: 19682 RVA: 0x0018C006 File Offset: 0x0018B206
		public virtual int Position
		{
			get
			{
				return this.PositionImpl;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x06004CE3 RID: 19683 RVA: 0x0018C00E File Offset: 0x0018B20E
		public bool IsIn
		{
			get
			{
				return (this.Attributes & ParameterAttributes.In) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x06004CE4 RID: 19684 RVA: 0x0018C01B File Offset: 0x0018B21B
		public bool IsLcid
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Lcid) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x06004CE5 RID: 19685 RVA: 0x0018C028 File Offset: 0x0018B228
		public bool IsOptional
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Optional) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x06004CE6 RID: 19686 RVA: 0x0018C036 File Offset: 0x0018B236
		public bool IsOut
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Out) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x06004CE7 RID: 19687 RVA: 0x0018C043 File Offset: 0x0018B243
		public bool IsRetval
		{
			get
			{
				return (this.Attributes & ParameterAttributes.Retval) > ParameterAttributes.None;
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x06004CE8 RID: 19688 RVA: 0x000C2700 File Offset: 0x000C1900
		[Nullable(2)]
		public virtual object DefaultValue
		{
			[NullableContext(2)]
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x06004CE9 RID: 19689 RVA: 0x000C2700 File Offset: 0x000C1900
		[Nullable(2)]
		public virtual object RawDefaultValue
		{
			[NullableContext(2)]
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x06004CEA RID: 19690 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool HasDefaultValue
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x06004CEB RID: 19691 RVA: 0x0018C050 File Offset: 0x0018B250
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x06004CEC RID: 19692 RVA: 0x0018C067 File Offset: 0x0018B267
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x06004CED RID: 19693 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004CEE RID: 19694 RVA: 0x0018C06F File Offset: 0x0018B26F
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return Array.Empty<object>();
		}

		// Token: 0x06004CEF RID: 19695 RVA: 0x0018C076 File Offset: 0x0018B276
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return Array.Empty<object>();
		}

		// Token: 0x06004CF0 RID: 19696 RVA: 0x00186403 File Offset: 0x00185603
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x06004CF1 RID: 19697 RVA: 0x00186403 File Offset: 0x00185603
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x17000C3D RID: 3133
		// (get) Token: 0x06004CF2 RID: 19698 RVA: 0x0018C091 File Offset: 0x0018B291
		public virtual int MetadataToken
		{
			get
			{
				return 134217728;
			}
		}

		// Token: 0x06004CF3 RID: 19699 RVA: 0x0018C098 File Offset: 0x0018B298
		public object GetRealObject(StreamingContext context)
		{
			if (this.MemberImpl == null)
			{
				throw new SerializationException(SR.Serialization_InsufficientState);
			}
			MemberTypes memberType = this.MemberImpl.MemberType;
			if (memberType != MemberTypes.Constructor && memberType != MemberTypes.Method)
			{
				if (memberType != MemberTypes.Property)
				{
					throw new SerializationException(SR.Serialization_NoParameterInfo);
				}
				ParameterInfo[] array = ((PropertyInfo)this.MemberImpl).GetIndexParameters();
				if (array != null && this.PositionImpl > -1 && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException(SR.Serialization_BadParameterInfo);
			}
			else if (this.PositionImpl == -1)
			{
				if (this.MemberImpl.MemberType == MemberTypes.Method)
				{
					return ((MethodInfo)this.MemberImpl).ReturnParameter;
				}
				throw new SerializationException(SR.Serialization_BadParameterInfo);
			}
			else
			{
				ParameterInfo[] array = ((MethodBase)this.MemberImpl).GetParametersNoCopy();
				if (array != null && this.PositionImpl < array.Length)
				{
					return array[this.PositionImpl];
				}
				throw new SerializationException(SR.Serialization_BadParameterInfo);
			}
		}

		// Token: 0x06004CF4 RID: 19700 RVA: 0x0018C188 File Offset: 0x0018B388
		public override string ToString()
		{
			return this.ParameterType.FormatTypeName() + " " + this.Name;
		}

		// Token: 0x040013B2 RID: 5042
		protected ParameterAttributes AttrsImpl;

		// Token: 0x040013B3 RID: 5043
		[Nullable(2)]
		protected Type ClassImpl;

		// Token: 0x040013B4 RID: 5044
		[Nullable(2)]
		protected object DefaultValueImpl;

		// Token: 0x040013B5 RID: 5045
		protected MemberInfo MemberImpl;

		// Token: 0x040013B6 RID: 5046
		[Nullable(2)]
		protected string NameImpl;

		// Token: 0x040013B7 RID: 5047
		protected int PositionImpl;
	}
}
