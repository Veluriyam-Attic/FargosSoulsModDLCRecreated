using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020001A7 RID: 423
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(1)]
	[Serializable]
	public class Tuple<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060019A4 RID: 6564 RVA: 0x000F8E3C File Offset: 0x000F803C
		public T1 Item1
		{
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060019A5 RID: 6565 RVA: 0x000F8E44 File Offset: 0x000F8044
		public T2 Item2
		{
			get
			{
				return this.m_Item2;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060019A6 RID: 6566 RVA: 0x000F8E4C File Offset: 0x000F804C
		public T3 Item3
		{
			get
			{
				return this.m_Item3;
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x060019A7 RID: 6567 RVA: 0x000F8E54 File Offset: 0x000F8054
		public T4 Item4
		{
			get
			{
				return this.m_Item4;
			}
		}

		// Token: 0x1700061E RID: 1566
		// (get) Token: 0x060019A8 RID: 6568 RVA: 0x000F8E5C File Offset: 0x000F805C
		public T5 Item5
		{
			get
			{
				return this.m_Item5;
			}
		}

		// Token: 0x060019A9 RID: 6569 RVA: 0x000F8E64 File Offset: 0x000F8064
		public Tuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			this.m_Item1 = item1;
			this.m_Item2 = item2;
			this.m_Item3 = item3;
			this.m_Item4 = item4;
			this.m_Item5 = item5;
		}

		// Token: 0x060019AA RID: 6570 RVA: 0x000F8646 File Offset: 0x000F7846
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x060019AB RID: 6571 RVA: 0x000F8E94 File Offset: 0x000F8094
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
			return tuple != null && (comparer.Equals(this.m_Item1, tuple.m_Item1) && comparer.Equals(this.m_Item2, tuple.m_Item2) && comparer.Equals(this.m_Item3, tuple.m_Item3) && comparer.Equals(this.m_Item4, tuple.m_Item4)) && comparer.Equals(this.m_Item5, tuple.m_Item5);
		}

		// Token: 0x060019AC RID: 6572 RVA: 0x000F868E File Offset: 0x000F788E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x060019AD RID: 6573 RVA: 0x000F8F48 File Offset: 0x000F8148
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1, T2, T3, T4, T5> tuple = other as Tuple<T1, T2, T3, T4, T5>;
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
			return comparer.Compare(this.m_Item5, tuple.m_Item5);
		}

		// Token: 0x060019AE RID: 6574 RVA: 0x000F86EF File Offset: 0x000F78EF
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x060019AF RID: 6575 RVA: 0x000F9024 File Offset: 0x000F8224
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return Tuple.CombineHashCodes(comparer.GetHashCode(this.m_Item1), comparer.GetHashCode(this.m_Item2), comparer.GetHashCode(this.m_Item3), comparer.GetHashCode(this.m_Item4), comparer.GetHashCode(this.m_Item5));
		}

		// Token: 0x060019B0 RID: 6576 RVA: 0x000F870F File Offset: 0x000F790F
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x060019B1 RID: 6577 RVA: 0x000F8718 File Offset: 0x000F7918
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x060019B2 RID: 6578 RVA: 0x000F908C File Offset: 0x000F828C
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
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x1700061F RID: 1567
		// (get) Token: 0x060019B3 RID: 6579 RVA: 0x000E8AA0 File Offset: 0x000E7CA0
		int ITuple.Length
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17000620 RID: 1568
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
				default:
					throw new IndexOutOfRangeException();
				}
				return result;
			}
		}

		// Token: 0x0400059D RID: 1437
		private readonly T1 m_Item1;

		// Token: 0x0400059E RID: 1438
		private readonly T2 m_Item2;

		// Token: 0x0400059F RID: 1439
		private readonly T3 m_Item3;

		// Token: 0x040005A0 RID: 1440
		private readonly T4 m_Item4;

		// Token: 0x040005A1 RID: 1441
		private readonly T5 m_Item5;
	}
}
