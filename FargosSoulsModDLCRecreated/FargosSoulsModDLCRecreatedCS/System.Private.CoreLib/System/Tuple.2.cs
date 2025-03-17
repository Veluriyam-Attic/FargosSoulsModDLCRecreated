using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x020001A3 RID: 419
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class Tuple<[Nullable(2)] T1> : IStructuralEquatable, IStructuralComparable, IComparable, ITupleInternal, ITuple
	{
		// Token: 0x17000608 RID: 1544
		// (get) Token: 0x0600196A RID: 6506 RVA: 0x000F862F File Offset: 0x000F782F
		[Nullable(1)]
		public T1 Item1
		{
			[NullableContext(1)]
			get
			{
				return this.m_Item1;
			}
		}

		// Token: 0x0600196B RID: 6507 RVA: 0x000F8637 File Offset: 0x000F7837
		[NullableContext(1)]
		public Tuple(T1 item1)
		{
			this.m_Item1 = item1;
		}

		// Token: 0x0600196C RID: 6508 RVA: 0x000F8646 File Offset: 0x000F7846
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return ((IStructuralEquatable)this).Equals(obj, EqualityComparer<object>.Default);
		}

		// Token: 0x0600196D RID: 6509 RVA: 0x000F8654 File Offset: 0x000F7854
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null)
			{
				return false;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			return tuple != null && comparer.Equals(this.m_Item1, tuple.m_Item1);
		}

		// Token: 0x0600196E RID: 6510 RVA: 0x000F868E File Offset: 0x000F788E
		int IComparable.CompareTo(object obj)
		{
			return ((IStructuralComparable)this).CompareTo(obj, Comparer<object>.Default);
		}

		// Token: 0x0600196F RID: 6511 RVA: 0x000F869C File Offset: 0x000F789C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			Tuple<T1> tuple = other as Tuple<T1>;
			if (tuple == null)
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_TupleIncorrectType, base.GetType()), "other");
			}
			return comparer.Compare(this.m_Item1, tuple.m_Item1);
		}

		// Token: 0x06001970 RID: 6512 RVA: 0x000F86EF File Offset: 0x000F78EF
		public override int GetHashCode()
		{
			return ((IStructuralEquatable)this).GetHashCode(EqualityComparer<object>.Default);
		}

		// Token: 0x06001971 RID: 6513 RVA: 0x000F86FC File Offset: 0x000F78FC
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.m_Item1);
		}

		// Token: 0x06001972 RID: 6514 RVA: 0x000F870F File Offset: 0x000F790F
		int ITupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return ((IStructuralEquatable)this).GetHashCode(comparer);
		}

		// Token: 0x06001973 RID: 6515 RVA: 0x000F8718 File Offset: 0x000F7918
		[NullableContext(1)]
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append('(');
			return ((ITupleInternal)this).ToString(stringBuilder);
		}

		// Token: 0x06001974 RID: 6516 RVA: 0x000F873B File Offset: 0x000F793B
		string ITupleInternal.ToString(StringBuilder sb)
		{
			sb.Append(this.m_Item1);
			sb.Append(')');
			return sb.ToString();
		}

		// Token: 0x17000609 RID: 1545
		// (get) Token: 0x06001975 RID: 6517 RVA: 0x000AC09E File Offset: 0x000AB29E
		int ITuple.Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x1700060A RID: 1546
		[Nullable(2)]
		object ITuple.this[int index]
		{
			get
			{
				if (index != 0)
				{
					throw new IndexOutOfRangeException();
				}
				return this.Item1;
			}
		}

		// Token: 0x04000593 RID: 1427
		private readonly T1 m_Item1;
	}
}
