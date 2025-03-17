using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020001A8 RID: 424
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Tuple<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000621 RID: 1569
		// (get) Token: 0x060019B5 RID: 6581 RVA: 0x000F91AA File Offset: 0x000F83AA
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000622 RID: 1570
		// (get) Token: 0x060019B6 RID: 6582 RVA: 0x000F91B2 File Offset: 0x000F83B2
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x17000623 RID: 1571
		// (get) Token: 0x060019B7 RID: 6583 RVA: 0x000F91BA File Offset: 0x000F83BA
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x17000624 RID: 1572
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x000F91C2 File Offset: 0x000F83C2
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x17000625 RID: 1573
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x000F91CA File Offset: 0x000F83CA
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x17000626 RID: 1574
		// (get) Token: 0x060019BA RID: 6586 RVA: 0x000F91D2 File Offset: 0x000F83D2
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		// Token: 0x060019BB RID: 6587 RVA: 0x000F91DA File Offset: 0x000F83DA
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
		}

		// Token: 0x060019BC RID: 6588 RVA: 0x000F8646 File Offset: 0x000F7846
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060019BD RID: 6589 RVA: 0x000F9210 File Offset: 0x000F8410
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5)) && comparer.Equals(this.m_Item6, tuple.m_Item6);
		}

		// Token: 0x060019BE RID: 6590 RVA: 0x000F868E File Offset: 0x000F788E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060019BF RID: 6591 RVA: 0x000F92E8 File Offset: 0x000F84E8
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6> tuple = other as Tuple<T1, T2, T3, T4, T5, T6>;
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
			num = comparer.Compare(this.m_Item3, tuple.m_Item3);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item4, tuple.m_Item4);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item5, tuple.m_Item5);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Item6, tuple.m_Item6);
		}

		// Token: 0x060019C0 RID: 6592 RVA: 0x000F86EF File Offset: 0x000F78EF
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060019C1 RID: 6593 RVA: 0x000F93E8 File Offset: 0x000F85E8
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6));
		}

		// Token: 0x060019C2 RID: 6594 RVA: 0x000F870F File Offset: 0x000F790F
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060019C3 RID: 6595 RVA: 0x000F8718 File Offset: 0x000F7918
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060019C4 RID: 6596 RVA: 0x000F9460 File Offset: 0x000F8660
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(", ");
			sb.Append(this.m_Item2);
			sb.Append(", ");
			sb.Append(this.m_Item3);
			sb.Append(", ");
			sb.Append(this.m_Item4);
			sb.Append(", ");
			sb.Append(this.m_Item5);
			sb.Append(", ");
			sb.Append(this.m_Item6);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000627 RID: 1575
		// (get) Token: 0x060019C5 RID: 6597 RVA: 0x000C9FD4 File Offset: 0x000C91D4
		int ITuple.Length
		{
			get
			{
				return 6;
			}
		}

		// Token: 0x17000628 RID: 1576
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
				case 3:
					result = this.Item4;
					break;
				case 4:
					result = this.Item5;
					break;
				case 5:
					result = this.Item6;
					break;
				default:
					throw new IndexOutOfRangeException();
				}
				return result;
			}
		}

		// Token: 0x040005A2 RID: 1442
		private readonly T1 m_Item1;

		// Token: 0x040005A3 RID: 1443
		private readonly T2 m_Item2;

		// Token: 0x040005A4 RID: 1444
		private readonly T3 m_Item3;

		// Token: 0x040005A5 RID: 1445
		private readonly T4 m_Item4;

		// Token: 0x040005A6 RID: 1446
		private readonly T5 m_Item5;

		// Token: 0x040005A7 RID: 1447
		private readonly T6 m_Item6;
	}
}
