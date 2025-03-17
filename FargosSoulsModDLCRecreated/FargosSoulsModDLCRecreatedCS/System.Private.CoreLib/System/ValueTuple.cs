using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001B9 RID: 441
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public struct ValueTuple : IEquatable<ValueTuple>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple>, IValueTupleInternal, ITuple
	{
		// Token: 0x06001AE0 RID: 6880 RVA: 0x000FD002 File Offset: 0x000FC202
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ValueTuple;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool Equals(ValueTuple other)
		{
			return true;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000FD002 File Offset: 0x000FC202
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			return other is ValueTuple;
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000FD00D File Offset: 0x000FC20D
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			return 0;
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000AC09B File Offset: 0x000AB29B
		public int CompareTo(ValueTuple other)
		{
			return 0;
		}

		// Token: 0x06001AE5 RID: 6885 RVA: 0x000FD00D File Offset: 0x000FC20D
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			return 0;
		}

		// Token: 0x06001AE6 RID: 6886 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override int GetHashCode()
		{
			return 0;
		}

		// Token: 0x06001AE7 RID: 6887 RVA: 0x000AC09B File Offset: 0x000AB29B
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000AC09B File Offset: 0x000AB29B
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return 0;
		}

		// Token: 0x06001AE9 RID: 6889 RVA: 0x000FD042 File Offset: 0x000FC242
		public override string ToString()
		{
			return "()";
		}

		// Token: 0x06001AEA RID: 6890 RVA: 0x000FD049 File Offset: 0x000FC249
		string IValueTupleInternal.ToStringEnd()
		{
			return ")";
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06001AEB RID: 6891 RVA: 0x000AC09B File Offset: 0x000AB29B
		int ITuple.Length
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000643 RID: 1603
		[Nullable(2)]
		object ITuple.this[int index]
		{
			get
			{
				throw new IndexOutOfRangeException();
			}
		}

		// Token: 0x06001AED RID: 6893 RVA: 0x000FD050 File Offset: 0x000FC250
		public static ValueTuple Create()
		{
			return default(ValueTuple);
		}

		// Token: 0x06001AEE RID: 6894 RVA: 0x000FD066 File Offset: 0x000FC266
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ValueTuple<T1> Create<[Nullable(2)] T1>(T1 item1)
		{
			return new ValueTuple<T1>(item1);
		}

		// Token: 0x06001AEF RID: 6895 RVA: 0x000FD06E File Offset: 0x000FC26E
		[return: Nullable(new byte[]
		{
			0,
			1,
			1
		})]
		public static ValueTuple<T1, T2> Create<[Nullable(2)] T1, [Nullable(2)] T2>(T1 item1, T2 item2)
		{
			return new ValueTuple<T1, T2>(item1, item2);
		}

		// Token: 0x06001AF0 RID: 6896 RVA: 0x000FD077 File Offset: 0x000FC277
		[return: Nullable(new byte[]
		{
			0,
			1,
			1,
			1
		})]
		public static ValueTuple<T1, T2, T3> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(T1 item1, T2 item2, T3 item3)
		{
			return new ValueTuple<T1, T2, T3>(item1, item2, item3);
		}

		// Token: 0x06001AF1 RID: 6897 RVA: 0x000FD081 File Offset: 0x000FC281
		[return: Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1
		})]
		public static ValueTuple<T1, T2, T3, T4> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new ValueTuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		// Token: 0x06001AF2 RID: 6898 RVA: 0x000FD08C File Offset: 0x000FC28C
		[return: Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1,
			1
		})]
		public static ValueTuple<T1, T2, T3, T4, T5> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new ValueTuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		// Token: 0x06001AF3 RID: 6899 RVA: 0x000FD099 File Offset: 0x000FC299
		[return: Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1,
			1,
			1
		})]
		public static ValueTuple<T1, T2, T3, T4, T5, T6> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		// Token: 0x06001AF4 RID: 6900 RVA: 0x000FD0A8 File Offset: 0x000FC2A8
		[return: Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		})]
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		// Token: 0x06001AF5 RID: 6901 RVA: 0x000FD0B9 File Offset: 0x000FC2B9
		[return: Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0,
			1
		})]
		public static ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new ValueTuple<T1, T2, T3, T4, T5, T6, T7, ValueTuple<T8>>(item1, item2, item3, item4, item5, item6, item7, ValueTuple.Create<T8>(item8));
		}
	}
}
