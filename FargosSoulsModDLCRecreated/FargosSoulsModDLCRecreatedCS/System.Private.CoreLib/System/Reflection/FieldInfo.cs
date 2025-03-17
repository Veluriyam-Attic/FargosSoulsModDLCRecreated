using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x02000595 RID: 1429
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class FieldInfo : MemberInfo
	{
		// Token: 0x06004980 RID: 18816 RVA: 0x00185DB4 File Offset: 0x00184FB4
		[NullableContext(1)]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(SR.Argument_InvalidHandle, "handle");
			}
			FieldInfo fieldInfo = RuntimeType.GetFieldInfo(handle.GetRuntimeFieldInfo());
			Type declaringType = fieldInfo.DeclaringType;
			if (declaringType != null && declaringType.IsGenericType)
			{
				throw new ArgumentException(SR.Format(SR.Argument_FieldDeclaringTypeGeneric, fieldInfo.Name, declaringType.GetGenericTypeDefinition()));
			}
			return fieldInfo;
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x00185E1C File Offset: 0x0018501C
		[NullableContext(1)]
		public static FieldInfo GetFieldFromHandle(RuntimeFieldHandle handle, RuntimeTypeHandle declaringType)
		{
			if (handle.IsNullHandle())
			{
				throw new ArgumentException(SR.Argument_InvalidHandle);
			}
			return RuntimeType.GetFieldInfo(declaringType.GetRuntimeType(), handle.GetRuntimeFieldInfo());
		}

		// Token: 0x17000B19 RID: 2841
		// (get) Token: 0x06004983 RID: 18819 RVA: 0x000CA38E File Offset: 0x000C958E
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Field;
			}
		}

		// Token: 0x17000B1A RID: 2842
		// (get) Token: 0x06004984 RID: 18820
		public abstract FieldAttributes Attributes { get; }

		// Token: 0x17000B1B RID: 2843
		// (get) Token: 0x06004985 RID: 18821
		[Nullable(1)]
		public abstract Type FieldType { [NullableContext(1)] get; }

		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x06004986 RID: 18822 RVA: 0x00185E45 File Offset: 0x00185045
		public bool IsInitOnly
		{
			get
			{
				return (this.Attributes & FieldAttributes.InitOnly) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06004987 RID: 18823 RVA: 0x00185E53 File Offset: 0x00185053
		public bool IsLiteral
		{
			get
			{
				return (this.Attributes & FieldAttributes.Literal) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06004988 RID: 18824 RVA: 0x00185E61 File Offset: 0x00185061
		public bool IsNotSerialized
		{
			get
			{
				return (this.Attributes & FieldAttributes.NotSerialized) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06004989 RID: 18825 RVA: 0x00185E72 File Offset: 0x00185072
		public bool IsPinvokeImpl
		{
			get
			{
				return (this.Attributes & FieldAttributes.PinvokeImpl) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x0600498A RID: 18826 RVA: 0x00185E83 File Offset: 0x00185083
		public bool IsSpecialName
		{
			get
			{
				return (this.Attributes & FieldAttributes.SpecialName) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600498B RID: 18827 RVA: 0x00185E94 File Offset: 0x00185094
		public bool IsStatic
		{
			get
			{
				return (this.Attributes & FieldAttributes.Static) > FieldAttributes.PrivateScope;
			}
		}

		// Token: 0x17000B22 RID: 2850
		// (get) Token: 0x0600498C RID: 18828 RVA: 0x00185EA2 File Offset: 0x001850A2
		public bool IsAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Assembly;
			}
		}

		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x0600498D RID: 18829 RVA: 0x00185EAF File Offset: 0x001850AF
		public bool IsFamily
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Family;
			}
		}

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x00185EBC File Offset: 0x001850BC
		public bool IsFamilyAndAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamANDAssem;
			}
		}

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x00185EC9 File Offset: 0x001850C9
		public bool IsFamilyOrAssembly
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.FamORAssem;
			}
		}

		// Token: 0x17000B26 RID: 2854
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x00185ED6 File Offset: 0x001850D6
		public bool IsPrivate
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Private;
			}
		}

		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x06004991 RID: 18833 RVA: 0x00185EE3 File Offset: 0x001850E3
		public bool IsPublic
		{
			get
			{
				return (this.Attributes & FieldAttributes.FieldAccessMask) == FieldAttributes.Public;
			}
		}

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x06004992 RID: 18834 RVA: 0x000AC09E File Offset: 0x000AB29E
		public virtual bool IsSecurityCritical
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x06004993 RID: 18835 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsSecuritySafeCritical
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06004994 RID: 18836 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsSecurityTransparent
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B2B RID: 2859
		// (get) Token: 0x06004995 RID: 18837
		public abstract RuntimeFieldHandle FieldHandle { get; }

		// Token: 0x06004996 RID: 18838 RVA: 0x00185EF0 File Offset: 0x001850F0
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x00185EF9 File Offset: 0x001850F9
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x0018222B File Offset: 0x0018142B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(FieldInfo left, FieldInfo right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x00185F01 File Offset: 0x00185101
		public static bool operator !=(FieldInfo left, FieldInfo right)
		{
			return !(left == right);
		}

		// Token: 0x0600499A RID: 18842
		public abstract object GetValue(object obj);

		// Token: 0x0600499B RID: 18843 RVA: 0x00185F0D File Offset: 0x0018510D
		[DebuggerHidden]
		[DebuggerStepThrough]
		public void SetValue(object obj, object value)
		{
			this.SetValue(obj, value, BindingFlags.Default, Type.DefaultBinder, null);
		}

		// Token: 0x0600499C RID: 18844
		public abstract void SetValue(object obj, object value, BindingFlags invokeAttr, Binder binder, CultureInfo culture);

		// Token: 0x0600499D RID: 18845 RVA: 0x00185F1E File Offset: 0x0018511E
		[CLSCompliant(false)]
		[NullableContext(1)]
		public virtual void SetValueDirect(TypedReference obj, object value)
		{
			throw new NotSupportedException(SR.NotSupported_AbstractNonCLS);
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x00185F1E File Offset: 0x0018511E
		[CLSCompliant(false)]
		public virtual object GetValueDirect(TypedReference obj)
		{
			throw new NotSupportedException(SR.NotSupported_AbstractNonCLS);
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x00185F1E File Offset: 0x0018511E
		public virtual object GetRawConstantValue()
		{
			throw new NotSupportedException(SR.NotSupported_AbstractNonCLS);
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x000C2700 File Offset: 0x000C1900
		[NullableContext(1)]
		public virtual Type[] GetOptionalCustomModifiers()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x000C2700 File Offset: 0x000C1900
		[NullableContext(1)]
		public virtual Type[] GetRequiredCustomModifiers()
		{
			throw NotImplemented.ByDesign;
		}
	}
}
