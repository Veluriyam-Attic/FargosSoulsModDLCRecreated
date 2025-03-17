using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020001A9 RID: 425
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Tuple<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000629 RID: 1577
		// (get) Token: 0x060019C7 RID: 6599 RVA: 0x000F95AC File Offset: 0x000F87AC
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700062A RID: 1578
		// (get) Token: 0x060019C8 RID: 6600 RVA: 0x000F95B4 File Offset: 0x000F87B4
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700062B RID: 1579
		// (get) Token: 0x060019C9 RID: 6601 RVA: 0x000F95BC File Offset: 0x000F87BC
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x1700062C RID: 1580
		// (get) Token: 0x060019CA RID: 6602 RVA: 0x000F95C4 File Offset: 0x000F87C4
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x1700062D RID: 1581
		// (get) Token: 0x060019CB RID: 6603 RVA: 0x000F95CC File Offset: 0x000F87CC
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x1700062E RID: 1582
		// (get) Token: 0x060019CC RID: 6604 RVA: 0x000F95D4 File Offset: 0x000F87D4
		public T6 Item6
		{
			get
			{
				return this.m_Item6;
			}
		}

		// Token: 0x1700062F RID: 1583
		// (get) Token: 0x060019CD RID: 6605 RVA: 0x000F95DC File Offset: 0x000F87DC
		public T7 Item7
		{
			get
			{
				return this.m_Item7;
			}
		}

		// Token: 0x060019CE RID: 6606 RVA: 0x000F95E4 File Offset: 0x000F87E4
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
			this.m_Item6 = item6;
			this.m_Item7 = item7;
		}

		// Token: 0x060019CF RID: 6607 RVA: 0x000F8646 File Offset: 0x000F7846
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060019D0 RID: 6608 RVA: 0x000F9624 File Offset: 0x000F8824
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4) && comparer.Equals(this.m_Item5, tuple.m_Item5) && comparer.Equals(this.m_Item6, tuple.m_Item6)) && comparer.Equals(this.m_Item7, tuple.m_Item7);
		}

		// Token: 0x060019D1 RID: 6609 RVA: 0x000F868E File Offset: 0x000F788E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060019D2 RID: 6610 RVA: 0x000F971C File Offset: 0x000F891C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5, T6, T7> tuple = other as Tuple<T1, T2, T3, T4, T5, T6, T7>;
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
			return comparer.Compare(this.m_Item7, tuple.m_Item7);
		}

		// Token: 0x060019D3 RID: 6611 RVA: 0x000F86EF File Offset: 0x000F78EF
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060019D4 RID: 6612 RVA: 0x000F983C File Offset: 0x000F8A3C
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5), comparer.GetHashCode(this.m_Item6), comparer.GetHashCode(this.m_Item7));
		}

		// Token: 0x060019D5 RID: 6613 RVA: 0x000F870F File Offset: 0x000F790F
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060019D6 RID: 6614 RVA: 0x000F8718 File Offset: 0x000F7918
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060019D7 RID: 6615 RVA: 0x000F98C8 File Offset: 0x000F8AC8
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
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x060019D8 RID: 6616 RVA: 0x000DA80B File Offset: 0x000D9A0B
		int ITuple.Length
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17000631 RID: 1585
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
					throw new IndexOutOfRangeException();
				}
				return result;
			}
		}

		// Token: 0x040005A8 RID: 1448
		private readonly T1 m_Item1;

		// Token: 0x040005A9 RID: 1449
		private readonly T2 m_Item2;

		// Token: 0x040005AA RID: 1450
		private readonly T3 m_Item3;

		// Token: 0x040005AB RID: 1451
		private readonly T4 m_Item4;

		// Token: 0x040005AC RID: 1452
		private readonly T5 m_Item5;

		// Token: 0x040005AD RID: 1453
		private readonly T6 m_Item6;

		// Token: 0x040005AE RID: 1454
		private readonly T7 m_Item7;
	}
}
