using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020001A4 RID: 420
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Tuple<[Nullable(2)] T1, [Nullable(2)] T2> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x1700060B RID: 1547
		// (get) Token: 0x06001977 RID: 6519 RVA: 0x000F8774 File Offset: 0x000F7974
		[Nullable(1)]
		public T1 Item1
		{
			[NullableContext(1)]
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700060C RID: 1548
		// (get) Token: 0x06001978 RID: 6520 RVA: 0x000F877C File Offset: 0x000F797C
		[Nullable(1)]
		public T2 Item2
		{
			[NullableContext(1)]
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x06001979 RID: 6521 RVA: 0x000F8784 File Offset: 0x000F7984
		[NullableContext(1)]
		public Tuple(T1 item1, T2 item2)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
		}

		// Token: 0x0600197A RID: 6522 RVA: 0x000F8646 File Offset: 0x000F7846
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x0600197B RID: 6523 RVA: 0x000F879C File Offset: 0x000F799C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2);
		}

		// Token: 0x0600197C RID: 6524 RVA: 0x000F868E File Offset: 0x000F788E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x0600197D RID: 6525 RVA: 0x000F87F8 File Offset: 0x000F79F8
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2> tuple = other as Tuple<T1, T2>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_TupleIncorrectType, base.GetType()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item2, tuple.m_Item2);
		}

		// Token: 0x0600197E RID: 6526 RVA: 0x000F86EF File Offset: 0x000F78EF
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x0600197F RID: 6527 RVA: 0x000F886D File Offset: 0x000F7A6D
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2));
		}

		// Token: 0x06001980 RID: 6528 RVA: 0x000F870F File Offset: 0x000F790F
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06001981 RID: 6529 RVA: 0x000F8718 File Offset: 0x000F7918
		[NullableContext(1)]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06001982 RID: 6530 RVA: 0x000F8898 File Offset: 0x000F7A98
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x1700060D RID: 1549
		// (get) Token: 0x06001983 RID: 6531 RVA: 0x000CE630 File Offset: 0x000CD830
		int ITuple.Length
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x1700060E RID: 1550
		[Nullable(2)]
		object ITuple.this[int index]
		{
			get
			{
				object result;
				if (index != 0)
				{
					if (index != 1)
					{
						throw new IndexOutOfRangeException();
					}
					result = this.Item2;
				}
				else
				{
					result = this.Item1;
				}
				return result;
			}
		}

		// Token: 0x04000594 RID: 1428
		private readonly T1 m_Item1;

		// Token: 0x04000595 RID: 1429
		private readonly T2 m_Item2;
	}
}
