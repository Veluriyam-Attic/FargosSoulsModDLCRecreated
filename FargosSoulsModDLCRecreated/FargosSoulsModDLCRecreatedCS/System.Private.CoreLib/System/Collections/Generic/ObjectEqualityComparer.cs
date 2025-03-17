using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007DB RID: 2011
	[NullableContext(2)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class ObjectEqualityComparer<T> : EqualityComparer<T>
	{
		// Token: 0x060060AA RID: 24746 RVA: 0x001CE738 File Offset: 0x001CD938
		internal override int IndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex + count;
			if (value == null)
			{
				for (int i = startIndex; i < num; i++)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j < num; j++)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060060AB RID: 24747 RVA: 0x001CE7AC File Offset: 0x001CD9AC
		internal override int LastIndexOf(T[] array, T value, int startIndex, int count)
		{
			int num = startIndex - count + 1;
			if (value == null)
			{
				for (int i = startIndex; i >= num; i--)
				{
					if (array[i] == null)
					{
						return i;
					}
				}
			}
			else
			{
				for (int j = startIndex; j >= num; j--)
				{
					if (array[j] != null && array[j].Equals(value))
					{
						return j;
					}
				}
			}
			return -1;
		}

		// Token: 0x060060AC RID: 24748 RVA: 0x001CE81F File Offset: 0x001CDA1F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x060060AD RID: 24749 RVA: 0x001CE5B4 File Offset: 0x001CD7B4
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode([DisallowNull] T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060060AE RID: 24750 RVA: 0x001CE115 File Offset: 0x001CD315
		public override bool Equals(object obj)
		{
			return obj != null && base.GetType() == obj.GetType();
		}

		// Token: 0x060060AF RID: 24751 RVA: 0x001CE12D File Offset: 0x001CD32D
		public override int GetHashCode()
		{
			return base.GetType().GetHashCode();
		}
	}
}
