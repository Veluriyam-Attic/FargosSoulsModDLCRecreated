using System;

namespace System.Collections.Generic
{
	// Token: 0x020007ED RID: 2029
	internal sealed class HashSetEqualityComparer<T> : IEqualityComparer<HashSet<T>>
	{
		// Token: 0x06006177 RID: 24951 RVA: 0x001D1FF0 File Offset: 0x001D11F0
		public bool Equals(HashSet<T> x, HashSet<T> y)
		{
			if (x == y)
			{
				return true;
			}
			if (x == null || y == null)
			{
				return false;
			}
			EqualityComparer<T> @default = EqualityComparer<T>.Default;
			if (HashSet<T>.EqualityComparersAreEqual(x, y))
			{
				return x.Count == y.Count && y.IsSubsetOfHashSetWithSameComparer(x);
			}
			foreach (T x2 in y)
			{
				bool flag = false;
				foreach (T y2 in x)
				{
					if (@default.Equals(x2, y2))
					{
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06006178 RID: 24952 RVA: 0x001D20C4 File Offset: 0x001D12C4
		public int GetHashCode(HashSet<T> obj)
		{
			int num = 0;
			if (obj != null)
			{
				foreach (T t in obj)
				{
					if (t != null)
					{
						num ^= t.GetHashCode();
					}
				}
			}
			return num;
		}

		// Token: 0x06006179 RID: 24953 RVA: 0x001D212C File Offset: 0x001D132C
		public override bool Equals(object obj)
		{
			return obj is HashSetEqualityComparer<T>;
		}

		// Token: 0x0600617A RID: 24954 RVA: 0x001D2137 File Offset: 0x001D1337
		public override int GetHashCode()
		{
			return EqualityComparer<T>.Default.GetHashCode();
		}
	}
}
