using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005D4 RID: 1492
	[NullableContext(1)]
	[Nullable(0)]
	public readonly struct CustomAttributeNamedArgument
	{
		// Token: 0x06004C2A RID: 19498 RVA: 0x0018B82B File Offset: 0x0018AA2B
		public static bool operator ==(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return left.Equals(right);
		}

		// Token: 0x06004C2B RID: 19499 RVA: 0x0018B840 File Offset: 0x0018AA40
		public static bool operator !=(CustomAttributeNamedArgument left, CustomAttributeNamedArgument right)
		{
			return !left.Equals(right);
		}

		// Token: 0x06004C2C RID: 19500 RVA: 0x0018B858 File Offset: 0x0018AA58
		public CustomAttributeNamedArgument(MemberInfo memberInfo, [Nullable(2)] object value)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			FieldInfo fieldInfo = memberInfo as FieldInfo;
			Type argumentType;
			if (fieldInfo != null)
			{
				argumentType = fieldInfo.FieldType;
			}
			else
			{
				PropertyInfo propertyInfo = memberInfo as PropertyInfo;
				if (propertyInfo == null)
				{
					throw new ArgumentException(SR.Argument_InvalidMemberForNamedArgument);
				}
				argumentType = propertyInfo.PropertyType;
			}
			this.m_memberInfo = memberInfo;
			this.m_value = new CustomAttributeTypedArgument(argumentType, value);
		}

		// Token: 0x06004C2D RID: 19501 RVA: 0x0018B8BE File Offset: 0x0018AABE
		public CustomAttributeNamedArgument(MemberInfo memberInfo, CustomAttributeTypedArgument typedArgument)
		{
			if (memberInfo == null)
			{
				throw new ArgumentNullException("memberInfo");
			}
			this.m_memberInfo = memberInfo;
			this.m_value = typedArgument;
		}

		// Token: 0x06004C2E RID: 19502 RVA: 0x0018B8E0 File Offset: 0x0018AAE0
		public override string ToString()
		{
			if (this.m_memberInfo == null)
			{
				return base.ToString();
			}
			return string.Format("{0} = {1}", this.MemberInfo.Name, this.TypedValue.ToString(this.ArgumentType != typeof(object)));
		}

		// Token: 0x06004C2F RID: 19503 RVA: 0x0018B944 File Offset: 0x0018AB44
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004C30 RID: 19504 RVA: 0x0018B956 File Offset: 0x0018AB56
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj == this;
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x06004C31 RID: 19505 RVA: 0x0018B966 File Offset: 0x0018AB66
		internal Type ArgumentType
		{
			get
			{
				if (!(this.m_memberInfo is FieldInfo))
				{
					return ((PropertyInfo)this.m_memberInfo).PropertyType;
				}
				return ((FieldInfo)this.m_memberInfo).FieldType;
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x06004C32 RID: 19506 RVA: 0x0018B996 File Offset: 0x0018AB96
		public MemberInfo MemberInfo
		{
			get
			{
				return this.m_memberInfo;
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x06004C33 RID: 19507 RVA: 0x0018B99E File Offset: 0x0018AB9E
		public CustomAttributeTypedArgument TypedValue
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x17000BFD RID: 3069
		// (get) Token: 0x06004C34 RID: 19508 RVA: 0x0018B9A6 File Offset: 0x0018ABA6
		public string MemberName
		{
			get
			{
				return this.MemberInfo.Name;
			}
		}

		// Token: 0x17000BFE RID: 3070
		// (get) Token: 0x06004C35 RID: 19509 RVA: 0x0018B9B3 File Offset: 0x0018ABB3
		public bool IsField
		{
			get
			{
				return this.MemberInfo is FieldInfo;
			}
		}

		// Token: 0x04001331 RID: 4913
		private readonly MemberInfo m_memberInfo;

		// Token: 0x04001332 RID: 4914
		private readonly CustomAttributeTypedArgument m_value;
	}
}
