using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001BC RID: 444
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3> : IEquatable<ValueTuple<T1, T2, T3>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3>>, IValueTupleInternal, ITuple
	{
		// Token: 0x06001B13 RID: 6931 RVA: 0x000FD6FD File Offset: 0x000FC8FD
		[NullableContext(1)]
		public ValueTuple(T1 item1, T2 item2, T3 item3)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x000FD714 File Offset: 0x000FC914
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2, T3> && this.Equals((ValueTuple<T1, T2, T3>)obj);
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x000FD72C File Offset: 0x000FC92C
		public bool Equals([Nullable(new byte[]
		{
			0,
			1,
			1,
			1
		})] ValueTuple<T1, T2, T3> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3);
		}

		// Token: 0x06001B16 RID: 6934 RVA: 0x000FD784 File Offset: 0x000FC984
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2, T3>))
			{
				return false;
			}
			ValueTuple<T1, T2, T3> valueTuple = (ValueTuple<T1, T2, T3>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3);
		}

		// Token: 0x06001B17 RID: 6935 RVA: 0x000FD7FF File Offset: 0x000FC9FF
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2, T3>)other);
		}

		// Token: 0x06001B18 RID: 6936 RVA: 0x000FD840 File Offset: 0x000FCA40
		public int CompareTo([Nullable(new byte[]
		{
			0,
			1,
			1,
			1
		})] ValueTuple<T1, T2, T3> other)
		{
			int num = Comparer<T1>.Default.Compare(this.Item1, other.Item1);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T2>.Default.Compare(this.Item2, other.Item2);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T3>.Default.Compare(this.Item3, other.Item3);
		}

		// Token: 0x06001B19 RID: 6937 RVA: 0x000FD89C File Offset: 0x000FCA9C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			ValueTuple<T1, T2, T3> valueTuple = (ValueTuple<T1, T2, T3>)other;
			int num = comparer.Compare(this.Item1, valueTuple.Item1);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item2, valueTuple.Item2);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item3, valueTuple.Item3);
		}

		// Token: 0x06001B1A RID: 6938 RVA: 0x000FD944 File Offset: 0x000FCB44
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
			ref T3 ptr3 = ref this.Item3;
			T3 t3 = default(T3);
			int value3;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr3 = ref t3;
				if (t3 == null)
				{
					value3 = 0;
					goto IL_9F;
				}
			}
			value3 = ptr3.GetHashCode();
			IL_9F:
			return HashCode.Combine<int, int, int>(value, value2, value3);
		}

		// Token: 0x06001B1B RID: 6939 RVA: 0x000FD9F5 File Offset: 0x000FCBF5
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B1C RID: 6940 RVA: 0x000FD9FE File Offset: 0x000FCBFE
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return HashCode.Combine<int, int, int>(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3));
		}

		// Token: 0x06001B1D RID: 6941 RVA: 0x000FD9F5 File Offset: 0x000FCBF5
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B1E RID: 6942 RVA: 0x000FDA38 File Offset: 0x000FCC38
		[NullableContext(1)]
		public override string ToString()
		{
			string[] array = new string[7];
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
			array[4] = ", ";
			int num3 = 5;
			ref T3 ptr3 = ref this.Item3;
			T3 t3 = default(T3);
			string text3;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr3 = ref t3;
				if (t3 == null)
				{
					text3 = null;
					goto IL_C5;
				}
			}
			text3 = ptr3.ToString();
			IL_C5:
			array[num3] = text3;
			array[6] = ")";
			return string.Concat(array);
		}

		// Token: 0x06001B1F RID: 6943 RVA: 0x000FDB18 File Offset: 0x000FCD18
		string IValueTupleInternal.ToStringEnd()
		{
			string[] array = new string[6];
			int num = 0;
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
					goto IL_3D;
				}
			}
			text = ptr.ToString();
			IL_3D:
			array[num] = text;
			array[1] = ", ";
			int num2 = 2;
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
					goto IL_7D;
				}
			}
			text2 = ptr2.ToString();
			IL_7D:
			array[num2] = text2;
			array[3] = ", ";
			int num3 = 4;
			ref T3 ptr3 = ref this.Item3;
			T3 t3 = default(T3);
			string text3;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr3 = ref t3;
				if (t3 == null)
				{
					text3 = null;
					goto IL_BD;
				}
			}
			text3 = ptr3.ToString();
			IL_BD:
			array[num3] = text3;
			array[5] = ")";
			return string.Concat(array);
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06001B20 RID: 6944 RVA: 0x000C9D36 File Offset: 0x000C8F36
		int ITuple.Length
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17000649 RID: 1609
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

		// Token: 0x040005DD RID: 1501
		[Nullable(1)]
		public T1 Item1;

		// Token: 0x040005DE RID: 1502
		[Nullable(1)]
		public T2 Item2;

		// Token: 0x040005DF RID: 1503
		[Nullable(1)]
		public T3 Item3;
	}
}
