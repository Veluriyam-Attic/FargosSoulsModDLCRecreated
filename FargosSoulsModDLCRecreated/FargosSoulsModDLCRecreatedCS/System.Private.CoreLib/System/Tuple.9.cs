using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020001AA RID: 426
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(1)]
	[Serializable]
	public class Tuple<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, TRest> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x060019DA RID: 6618 RVA: 0x000F9A46 File Offset: 0x000F8C46
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x060019DB RID: 6619 RVA: 0x000F9A4E File Offset: 0x000F8C4E
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x060019DC RID: 6620 RVA: 0x000F9A56 File Offset: 0x000F8C56
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x060019DD RID: 6621 RVA: 0x000F9A5E File Offset: 0x000F8C5E
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x000F9A66 File Offset: 0x000F8C66
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x060019DF RID: 6623 RVA: 0x000F9A6E File Offset: 0x000F8C6E
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		// Token: 0x17000638 RID: 1592
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x000F9A76 File Offset: 0x000F8C76
		public T7 Item7
		{
			get
			{
				return this.m_Item7;
			}
		}

		// Token: 0x17000639 RID: 1593
		// (get) Token: 0x060019E1 RID: 6625 RVA: 0x000F9A7E File Offset: 0x000F8C7E
		public TRest Rest
		{
			get
			{
				return this.m_Rest;
			}
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x000F9A88 File Offset: 0x000F8C88
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, TRest rest)
		{
			if (!(rest is ITupleInternal))
			{
				throw new ArgumentException(SR.ArgumentException_TupleLastArgumentNotATuple);
			}
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
			this.m_Item7 = item7;
			this.m_Rest = rest;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x000F8646 File Offset: 0x000F7846
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x000F9AF4 File Offset: 0x000F8CF4
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5) && comparer.Equals(this.m_Item6, tuple.m_Item6) && comparer.Equals(this.m_Item7, tuple.m_Item7)) && comparer.Equals(this.m_Rest, tuple.m_Rest);
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x000F868E File Offset: 0x000F788E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x000F9C0C File Offset: 0x000F8E0C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7, TRest> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7, TRest>;
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
			num = comparer.Compare(this.m_Item6, tuple.m_Item6);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.m_Item7, tuple.m_Item7);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.m_Rest, tuple.m_Rest);
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x000F86EF File Offset: 0x000F78EF
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x000F9D50 File Offset: 0x000F8F50
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			ITupleInternal tupleInternal = (ITupleInternal)((object)this.m_Rest);
			if (tupleInternal.Length >= 8)
			{
				return tupleInternal.GetHashCode(comparer);
			}
			switch (8 - tupleInternal.Length)
			{
			case 1:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 2:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 3:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 4:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 5:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 6:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			case 7:
				return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7), tupleInternal.GetHashCode(comparer));
			default:
				return -1;
			}
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x000F870F File Offset: 0x000F790F
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x000F8718 File Offset: 0x000F7918
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x000F9FEC File Offset: 0x000F91EC
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
			sb.Append(", ");
			sb.Append(this.m_Item7);
			sb.Append(", ");
			return ((ITupleInternal)((object)this.m_Rest)).ToString(sb);
		}

		// Token: 0x1700063A RID: 1594
		// (get) Token: 0x060019EC RID: 6636 RVA: 0x000FA0E1 File Offset: 0x000F92E1
		int ITuple.Length
		{
			get
			{
				return 7 + ((ITupleInternal)((object)this.Rest)).Length;
			}
		}

		// Token: 0x1700063B RID: 1595
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
				case 6:
					result = this.Item7;
					break;
				default:
					result = ((ITupleInternal)((object)this.Rest))[index - 7];
					break;
				}
				return result;
			}
		}

		// Token: 0x040005AF RID: 1455
		private readonly T1 m_Item1;

		// Token: 0x040005B0 RID: 1456
		private readonly T2 m_Item2;

		// Token: 0x040005B1 RID: 1457
		private readonly T3 m_Item3;

		// Token: 0x040005B2 RID: 1458
		private readonly T4 m_Item4;

		// Token: 0x040005B3 RID: 1459
		private readonly T5 m_Item5;

		// Token: 0x040005B4 RID: 1460
		private readonly T6 m_Item6;

		// Token: 0x040005B5 RID: 1461
		private readonly T7 m_Item7;

		// Token: 0x040005B6 RID: 1462
		private readonly TRest m_Rest;
	}
}
