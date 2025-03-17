using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001BB RID: 443
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<[Nullable(2)] T1, [Nullable(2)] T2> : IEquatable<ValueTuple<T1, T2>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2>>, IValueTupleInternal, ITuple
	{
		// Token: 0x06001B04 RID: 6916 RVA: 0x000FD32E File Offset: 0x000FC52E
		[NullableContext(1)]
		public ValueTuple(T1 item1, T2 item2)
		{
			this.Item1 = item1;
			this.Item2 = item2;
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x000FD33E File Offset: 0x000FC53E
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2> && this.Equals((ValueTuple<T1, T2>)obj);
		}

		// Token: 0x06001B06 RID: 6918 RVA: 0x000FD356 File Offset: 0x000FC556
		public bool Equals([Nullable(new byte[]
		{
			0,
			1,
			1
		})] ValueTuple<T1, T2> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2);
		}

		// Token: 0x06001B07 RID: 6919 RVA: 0x000FD388 File Offset: 0x000FC588
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2>))
			{
				return false;
			}
			ValueTuple<T1, T2> valueTuple = (ValueTuple<T1, T2>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2);
		}

		// Token: 0x06001B08 RID: 6920 RVA: 0x000FD3E5 File Offset: 0x000FC5E5
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2>)other);
		}

		// Token: 0x06001B09 RID: 6921 RVA: 0x000FD428 File Offset: 0x000FC628
		public int CompareTo([Nullable(new byte[]
		{
			0,
			1,
			1
		})] ValueTuple<T1, T2> other)
		{
			int num = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T2>.Default.Compare(this.Item2, other.Item2);
		}

		// Token: 0x06001B0A RID: 6922 RVA: 0x000FD468 File Offset: 0x000FC668
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			ValueTuple<T1, T2> valueTuple = (ValueTuple<T1, T2>)other;
			int num = comparer.Compare(this.Item1, valueTuple.Item1);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item2, valueTuple.Item2);
		}

		// Token: 0x06001B0B RID: 6923 RVA: 0x000FD4EC File Offset: 0x000FC6EC
		public override int GetHashCode()
		{
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			int value;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					value = 0;
					goto IL_35;
				}
			}
			value = ptr.GetHashCode();
			IL_35:
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			int value2;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					value2 = 0;
					goto IL_6A;
				}
			}
			value2 = ptr2.GetHashCode();
			IL_6A:
			return HashCode.Combine<int, int>(value, value2);
		}

		// Token: 0x06001B0C RID: 6924 RVA: 0x000FD568 File Offset: 0x000FC768
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B0D RID: 6925 RVA: 0x000FD571 File Offset: 0x000FC771
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return HashCode.Combine<int, int>(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2));
		}

		// Token: 0x06001B0E RID: 6926 RVA: 0x000FD568 File Offset: 0x000FC768
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B0F RID: 6927 RVA: 0x000FD59C File Offset: 0x000FC79C
		[NullableContext(1)]
		public override string ToString()
		{
			string[] array = new string[5];
			array[0] = "(";
			int num = 1;
			ref T1 ptr = ref this.Item1;
			T1 t = default(T1);
			string text;
			if (t == null)
			{
				t = this.Item1;
				ptr = ref t;
				if (t == null)
				{
					text = null;
					goto IL_45;
				}
			}
			text = ptr.ToString();
			IL_45:
			array[num] = text;
			array[2] = ", ";
			int num2 = 3;
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			string text2;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					text2 = null;
					goto IL_85;
				}
			}
			text2 = ptr2.ToString();
			IL_85:
			array[num2] = text2;
			array[4] = ")";
			return string.Concat(array);
		}

		// Token: 0x06001B10 RID: 6928 RVA: 0x000FD63C File Offset: 0x000FC83C
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
			string str2 = ", ";
			ref T2 ptr2 = ref this.Item2;
			T2 t2 = default(T2);
			string str3;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr2 = ref t2;
				if (t2 == null)
				{
					str3 = null;
					goto IL_6F;
				}
			}
			str3 = ptr2.ToString();
			IL_6F:
			return str + str2 + str3 + ")";
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06001B11 RID: 6929 RVA: 0x000CE630 File Offset: 0x000CD830
		int ITuple.Length
		{
			get
			{
				return 2;
			}
		}

		// Token: 0x17000647 RID: 1607
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

		// Token: 0x040005DB RID: 1499
		[Nullable(1)]
		public T1 Item1;

		// Token: 0x040005DC RID: 1500
		[Nullable(1)]
		public T2 Item2;
	}
}
