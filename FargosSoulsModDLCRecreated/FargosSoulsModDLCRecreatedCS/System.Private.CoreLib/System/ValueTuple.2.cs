using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001BA RID: 442
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public struct ValueTuple<[Nullable(2)] T1> : IEquatable<ValueTuple<T1>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1>>, IValueTupleInternal, ITuple
	{
		// Token: 0x06001AF6 RID: 6902 RVA: 0x000FD0D1 File Offset: 0x000FC2D1
		[NullableContext(1)]
		public ValueTuple(T1 item1)
		{
			this.Item1 = item1;
		}

		// Token: 0x06001AF7 RID: 6903 RVA: 0x000FD0DA File Offset: 0x000FC2DA
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1> && this.Equals((ValueTuple<T1>)obj);
		}

		// Token: 0x06001AF8 RID: 6904 RVA: 0x000FD0F2 File Offset: 0x000FC2F2
		public bool Equals([Nullable(new byte[]
		{
			0,
			1
		})] ValueTuple<T1> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1);
		}

		// Token: 0x06001AF9 RID: 6905 RVA: 0x000FD10C File Offset: 0x000FC30C
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1>))
			{
				return false;
			}
			ValueTuple<T1> valueTuple = (ValueTuple<T1>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06001AFA RID: 6906 RVA: 0x000FD14C File Offset: 0x000FC34C
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			ValueTuple<T1> valueTuple = (ValueTuple<T1>)other;
			return Comparer<T1>.Default.Compare(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06001AFB RID: 6907 RVA: 0x000FD1A8 File Offset: 0x000FC3A8
		public int CompareTo([Nullable(new byte[]
		{
			0,
			1
		})] ValueTuple<T1> other)
		{
			return Comparer<T1>.Default.Compare(this.Item1, other.Item1);
		}

		// Token: 0x06001AFC RID: 6908 RVA: 0x000FD1C0 File Offset: 0x000FC3C0
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			ValueTuple<T1> valueTuple = (ValueTuple<T1>)other;
			return comparer.Compare(this.Item1, valueTuple.Item1);
		}

		// Token: 0x06001AFD RID: 6909 RVA: 0x000FD224 File Offset: 0x000FC424
		public override int GetHashCode()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					return 0;
				}
			}
			return ptr.GetHashCode();
		}

		// Token: 0x06001AFE RID: 6910 RVA: 0x000FD265 File Offset: 0x000FC465
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06001AFF RID: 6911 RVA: 0x000FD265 File Offset: 0x000FC465
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return comparer.GetHashCode(this.Item1);
		}

		// Token: 0x06001B00 RID: 6912 RVA: 0x000FD278 File Offset: 0x000FC478
		[NullableContext(1)]
		public override string ToString()
		{
			string str = "(";
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string str2;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					str2 = null;
					goto IL_3A;
				}
			}
			str2 = ptr.ToString();
			IL_3A:
			return str + str2 + ")";
		}

		// Token: 0x06001B01 RID: 6913 RVA: 0x000FD2CC File Offset: 0x000FC4CC
		string IValueTupleInternal.ToStringEnd()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string str;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					str = null;
					goto IL_35;
				}
			}
			str = ptr.ToString();
			IL_35:
			return str + ")";
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06001B02 RID: 6914 RVA: 0x000AC09E File Offset: 0x000AB29E
		int ITuple.Length
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x17000645 RID: 1605
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

		// Token: 0x040005DA RID: 1498
		[Nullable(1)]
		public T1 Item1;
	}
}
