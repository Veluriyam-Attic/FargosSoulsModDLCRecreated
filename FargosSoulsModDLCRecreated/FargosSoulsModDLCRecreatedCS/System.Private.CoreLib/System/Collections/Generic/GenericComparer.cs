using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007E0 RID: 2016
	[NullableContext(2)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class GenericComparer<[Nullable(0)] T> : Comparer<T> where T : IComparable<T>
	{
		// Token: 0x060060C2 RID: 24770 RVA: 0x001CEA7E File Offset: 0x001CDC7E
		public override int Compare(T x, T y)
		{
			if (x != null)
			{
				if (y != null)
				{
					return x.CompareTo(y);
				}
				return 1;
			}
			else
			{
				if (y != null)
				{
					return -1;
				}
				return 0;
			}
		}

		// Token: 0x060060C3 RID: 24771 RVA: 0x001CE115 File Offset: 0x001CD315
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType();
		}

		// Token: 0x060060C4 RID: 24772 RVA: 0x001CE12D File Offset: 0x001CD32D
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}
	}
}
