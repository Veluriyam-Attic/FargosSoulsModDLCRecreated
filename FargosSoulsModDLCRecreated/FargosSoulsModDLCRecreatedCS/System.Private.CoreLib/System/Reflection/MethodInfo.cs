using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005EA RID: 1514
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class MethodInfo : MethodBase
	{
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06004C82 RID: 19586 RVA: 0x000DAEBB File Offset: 0x000DA0BB
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Method;
			}
		}

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06004C83 RID: 19587 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual ParameterInfo ReturnParameter
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06004C84 RID: 19588 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual Type ReturnType
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x06004C85 RID: 19589 RVA: 0x000C2761 File Offset: 0x000C1961
		public override Type[] GetGenericArguments()
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x06004C86 RID: 19590 RVA: 0x000C2761 File Offset: 0x000C1961
		public virtual MethodInfo GetGenericMethodDefinition()
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x06004C87 RID: 19591 RVA: 0x000C2761 File Offset: 0x000C1961
		public virtual MethodInfo MakeGenericMethod(params Type[] typeArguments)
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x06004C88 RID: 19592
		public abstract MethodInfo GetBaseDefinition();

		// Token: 0x17000C1E RID: 3102
		// (get) Token: 0x06004C89 RID: 19593
		public abstract ICustomAttributeProvider ReturnTypeCustomAttributes { get; }

		// Token: 0x06004C8A RID: 19594 RVA: 0x000C2761 File Offset: 0x000C1961
		public virtual Delegate CreateDelegate(Type delegateType)
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x06004C8B RID: 19595 RVA: 0x000C2761 File Offset: 0x000C1961
		public virtual Delegate CreateDelegate(Type delegateType, [Nullable(2)] object target)
		{
			throw new NotSupportedException(SR.NotSupported_SubclassOverride);
		}

		// Token: 0x06004C8C RID: 19596 RVA: 0x0018BC98 File Offset: 0x0018AE98
		public T CreateDelegate<[Nullable(0)] T>() where T : Delegate
		{
			return (T)((object)this.CreateDelegate(typeof(T)));
		}

		// Token: 0x06004C8D RID: 19597 RVA: 0x0018BCAF File Offset: 0x0018AEAF
		public T CreateDelegate<[Nullable(0)] T>([Nullable(2)] object target) where T : Delegate
		{
			return (T)((object)this.CreateDelegate(typeof(T), target));
		}

		// Token: 0x06004C8E RID: 19598 RVA: 0x001830DE File Offset: 0x001822DE
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004C8F RID: 19599 RVA: 0x001830E7 File Offset: 0x001822E7
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004C90 RID: 19600 RVA: 0x0018222B File Offset: 0x0018142B
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(MethodInfo left, MethodInfo right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x06004C91 RID: 19601 RVA: 0x0018BCC7 File Offset: 0x0018AEC7
		[NullableContext(2)]
		public static bool operator !=(MethodInfo left, MethodInfo right)
		{
			return !(left == right);
		}

		// Token: 0x17000C1F RID: 3103
		// (get) Token: 0x06004C92 RID: 19602 RVA: 0x0018BCD3 File Offset: 0x0018AED3
		internal virtual int GenericParameterCount
		{
			get
			{
				return this.GetGenericArguments().Length;
			}
		}
	}
}
