using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007E2 RID: 2018
	[NullableContext(2)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class ObjectComparer<T> : Comparer<T>
	{
		// Token: 0x060060CA RID: 24778 RVA: 0x001CEAEF File Offset: 0x001CDCEF
		public override int Compare(T x, T y)
		{
			return Comparer.Default.Compare(x, y);
		}

		// Token: 0x060060CB RID: 24779 RVA: 0x001CE115 File Offset: 0x001CD315
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType();
		}

		// Token: 0x060060CC RID: 24780 RVA: 0x001CE12D File Offset: 0x001CD32D
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}
	}
}
