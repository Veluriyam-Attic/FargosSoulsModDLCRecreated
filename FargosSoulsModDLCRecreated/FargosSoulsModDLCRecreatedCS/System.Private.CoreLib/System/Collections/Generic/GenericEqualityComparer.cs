using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007D9 RID: 2009
	[NullableContext(1)]
	[Nullable(new byte[]
	{
		0,
		1
	})]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public sealed class GenericEqualityComparer<[Nullable(0)] T> : EqualityComparer<T> where T : IEquatable<T>
	{
		// Token: 0x0600609C RID: 24732 RVA: 0x001CE4AC File Offset: 0x001CD6AC
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

		// Token: 0x0600609D RID: 24733 RVA: 0x001CE518 File Offset: 0x001CD718
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

		// Token: 0x0600609E RID: 24734 RVA: 0x001CE586 File Offset: 0x001CD786
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override bool Equals(T x, T y)
		{
			if (x != null)
			{
				return y != null && x.Equals(y);
			}
			return y == null;
		}

		// Token: 0x0600609F RID: 24735 RVA: 0x001CE5B4 File Offset: 0x001CD7B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public override int GetHashCode([DisallowNull] T obj)
		{
			if (obj == null)
			{
				return 0;
			}
			return obj.GetHashCode();
		}

		// Token: 0x060060A0 RID: 24736 RVA: 0x001CE5CD File Offset: 0x001CD7CD
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is GenericEqualityComparer<T>;
		}

		// Token: 0x060060A1 RID: 24737 RVA: 0x001CE5D8 File Offset: 0x001CD7D8
		public override int GetHashCode()
		{
			return typeof(GenericEqualityComparer<T>).GetHashCode();
		}
	}
}
