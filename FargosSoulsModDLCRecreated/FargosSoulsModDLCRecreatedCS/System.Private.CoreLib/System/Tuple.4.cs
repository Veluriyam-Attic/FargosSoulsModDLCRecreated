using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020001A5 RID: 421
	[Nullable(0)]
	[NullableContext(1)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Tuple<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x1700060F RID: 1551
		// (get) Token: 0x06001985 RID: 6533 RVA: 0x000F891D File Offset: 0x000F7B1D
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000610 RID: 1552
		// (get) Token: 0x06001986 RID: 6534 RVA: 0x000F8925 File Offset: 0x000F7B25
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x17000611 RID: 1553
		// (get) Token: 0x06001987 RID: 6535 RVA: 0x000F892D File Offset: 0x000F7B2D
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x06001988 RID: 6536 RVA: 0x000F8935 File Offset: 0x000F7B35
		public Tuple(T1 item1, T2 item2, T3 item3)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
		}

		// Token: 0x06001989 RID: 6537 RVA: 0x000F8646 File Offset: 0x000F7846
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x0600198A RID: 6538 RVA: 0x000F8954 File Offset: 0x000F7B54
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2)) && comparer.Equals(this.m_Item3, tuple.m_Item3);
		}

		// Token: 0x0600198B RID: 6539 RVA: 0x000F868E File Offset: 0x000F788E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x0600198C RID: 6540 RVA: 0x000F89CC File Offset: 0x000F7BCC
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3> tuple = other as Tuple<T1, T2, T3>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_TupleIncorrectType, base.GetType()), "other");
			}
			int num = comparer.Compare(this.m_Item1, tuple.m_Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item2, tuple.m_Item2);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item3, tuple.m_Item3);
		}

		// Token: 0x0600198D RID: 6541 RVA: 0x000F86EF File Offset: 0x000F78EF
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x0600198E RID: 6542 RVA: 0x000F8A63 File Offset: 0x000F7C63
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3));
		}

		// Token: 0x0600198F RID: 6543 RVA: 0x000F870F File Offset: 0x000F790F
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06001990 RID: 6544 RVA: 0x000F8718 File Offset: 0x000F7918
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06001991 RID: 6545 RVA: 0x000F8AA0 File Offset: 0x000F7CA0
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001992 RID: 6546 RVA: 0x000C9D36 File Offset: 0x000C8F36
		int ITuple.Length
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000613 RID: 1555
		[Nullable(2)]
		object ITuple.this[int index]
		{
			get
			{
				object result;
				switch (index)
				{
				case 0:
					result = this.Item1;
					break;
				case 1:
					result = this.Item2;
					break;
				case 2:
					result = this.Item3;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
				return result;
			}
		}

		// Token: 0x04000596 RID: 1430
		private readonly T1 m_Item1;

		// Token: 0x04000597 RID: 1431
		private readonly T2 m_Item2;

		// Token: 0x04000598 RID: 1432
		private readonly T3 m_Item3;
	}
}
