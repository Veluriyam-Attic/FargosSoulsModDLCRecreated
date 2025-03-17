using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x0200058A RID: 1418
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class ConstructorInfo : MethodBase
	{
		// Token: 0x06004905 RID: 18693 RVA: 0x00177FCF File Offset: 0x001771CF
		internal virtual Type GetReturnType()
		{
			throw new NotImplementedException();
		}

		// Token: 0x17000B08 RID: 2824
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override MemberTypes MemberType
		{
			get
			{
				return MemberTypes.Constructor;
			}
		}

		// Token: 0x06004908 RID: 18696 RVA: 0x001830D2 File Offset: 0x001822D2
		[NullableContext(1)]
		[DebuggerHidden]
		[DebuggerStepThrough]
		public object Invoke([Nullable(2)] object[] parameters)
		{
			return this.Invoke(BindingFlags.Default, null, parameters, null);
		}

		// Token: 0x06004909 RID: 18697
		[return: Nullable(1)]
		public abstract object Invoke(BindingFlags invokeAttr, Binder binder, object[] parameters, CultureInfo culture);

		// Token: 0x0600490A RID: 18698 RVA: 0x001830DE File Offset: 0x001822DE
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x0600490B RID: 18699 RVA: 0x001830E7 File Offset: 0x001822E7
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0600490C RID: 18700 RVA: 0x0018222B File Offset: 0x0018142B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(ConstructorInfo left, ConstructorInfo right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x0600490D RID: 18701 RVA: 0x001830EF File Offset: 0x001822EF
		public static bool operator !=(ConstructorInfo left, ConstructorInfo right)
		{
			return !(left == right);
		}

		// Token: 0x040011D2 RID: 4562
		[Nullable(1)]
		public static readonly string ConstructorName = ".ctor";

		// Token: 0x040011D3 RID: 4563
		[Nullable(1)]
		public static readonly string TypeConstructorName = ".cctor";
	}
}
