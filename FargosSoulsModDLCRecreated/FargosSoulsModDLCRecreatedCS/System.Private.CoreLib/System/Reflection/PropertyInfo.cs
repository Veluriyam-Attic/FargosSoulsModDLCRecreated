using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005F8 RID: 1528
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class PropertyInfo : MemberInfo
	{
		// Token: 0x17000C40 RID: 3136
		// (get) Token: 0x06004D00 RID: 19712 RVA: 0x000C6BB9 File Offset: 0x000C5DB9
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Property;
			}
		}

		// Token: 0x17000C41 RID: 3137
		// (get) Token: 0x06004D01 RID: 19713
		[Nullable(1)]
		public abstract Type PropertyType { [NullableContext(1)] get; }

		// Token: 0x06004D02 RID: 19714
		[NullableContext(1)]
		public abstract ParameterInfo[] GetIndexParameters();

		// Token: 0x17000C42 RID: 3138
		// (get) Token: 0x06004D03 RID: 19715
		public abstract PropertyAttributes Attributes { get; }

		// Token: 0x17000C43 RID: 3139
		// (get) Token: 0x06004D04 RID: 19716 RVA: 0x0018C28A File Offset: 0x0018B48A
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & PropertyAttributes.SpecialName) > PropertyAttributes.None;
			}
		}

		// Token: 0x17000C44 RID: 3140
		// (get) Token: 0x06004D05 RID: 19717
		public abstract bool CanRead { get; }

		// Token: 0x17000C45 RID: 3141
		// (get) Token: 0x06004D06 RID: 19718
		public abstract bool CanWrite { get; }

		// Token: 0x06004D07 RID: 19719 RVA: 0x0018C29B File Offset: 0x0018B49B
		[NullableContext(1)]
		public MethodInfo[] GetAccessors()
		{
			return this.GetAccessors(false);
		}

		// Token: 0x06004D08 RID: 19720
		[NullableContext(1)]
		public abstract MethodInfo[] GetAccessors(bool nonPublic);

		// Token: 0x17000C46 RID: 3142
		// (get) Token: 0x06004D09 RID: 19721 RVA: 0x0018C2A4 File Offset: 0x0018B4A4
		public virtual MethodInfo GetMethod
		{
			get
			{
				return this.GetGetMethod(true);
			}
		}

		// Token: 0x06004D0A RID: 19722 RVA: 0x0018C2AD File Offset: 0x0018B4AD
		public MethodInfo GetGetMethod()
		{
			return this.GetGetMethod(false);
		}

		// Token: 0x06004D0B RID: 19723
		public abstract MethodInfo GetGetMethod(bool nonPublic);

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06004D0C RID: 19724 RVA: 0x0018C2B6 File Offset: 0x0018B4B6
		public virtual MethodInfo SetMethod
		{
			get
			{
				return this.GetSetMethod(true);
			}
		}

		// Token: 0x06004D0D RID: 19725 RVA: 0x0018C2BF File Offset: 0x0018B4BF
		public MethodInfo GetSetMethod()
		{
			return this.GetSetMethod(false);
		}

		// Token: 0x06004D0E RID: 19726
		public abstract MethodInfo GetSetMethod(bool nonPublic);

		// Token: 0x06004D0F RID: 19727 RVA: 0x00186403 File Offset: 0x00185603
		[NullableContext(1)]
		public virtual Type[] GetOptionalCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x06004D10 RID: 19728 RVA: 0x00186403 File Offset: 0x00185603
		[NullableContext(1)]
		public virtual Type[] GetRequiredCustomModifiers()
		{
			return Array.Empty<Type>();
		}

		// Token: 0x06004D11 RID: 19729 RVA: 0x0018C2C8 File Offset: 0x0018B4C8
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object GetValue(object obj)
		{
			return this.GetValue(obj, null);
		}

		// Token: 0x06004D12 RID: 19730 RVA: 0x0018C2D2 File Offset: 0x0018B4D2
		[DebuggerStepThrough]
		[DebuggerHidden]
		public virtual object GetValue(object obj, object[] index)
		{
			return this.GetValue(obj, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06004D13 RID: 19731
		public abstract object GetValue(object obj, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06004D14 RID: 19732 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual object GetConstantValue()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004D15 RID: 19733 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual object GetRawConstantValue()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004D16 RID: 19734 RVA: 0x0018C2DF File Offset: 0x0018B4DF
		[DebuggerStepThrough]
		[DebuggerHidden]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, null);
		}

		// Token: 0x06004D17 RID: 19735 RVA: 0x0018C2EA File Offset: 0x0018B4EA
		[DebuggerHidden]
		[DebuggerStepThrough]
		public virtual void SetValue(object obj, object value, object[] index)
		{
			this.SetValue(obj, value, BindingFlags.Default, null, index, null);
		}

		// Token: 0x06004D18 RID: 19736
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, object[] index, CultureInfo culture);

		// Token: 0x06004D19 RID: 19737 RVA: 0x00185EF0 File Offset: 0x001850F0
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004D1A RID: 19738 RVA: 0x00185EF9 File Offset: 0x001850F9
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004D1B RID: 19739 RVA: 0x0018222B File Offset: 0x0018142B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(PropertyInfo left, PropertyInfo right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06004D1C RID: 19740 RVA: 0x0018C2F8 File Offset: 0x0018B4F8
		public static bool operator !=(PropertyInfo left, PropertyInfo right)
		{
			return !(left == right);
		}
	}
}
