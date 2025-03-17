﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001BD RID: 445
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4> : IEquatable<ValueTuple<T1, T2, T3, T4>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4>>, IValueTupleInternal, ITuple
	{
		// Token: 0x06001B22 RID: 6946 RVA: 0x000FDC42 File Offset: 0x000FCE42
		[NullableContext(1)]
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
		}

		// Token: 0x06001B23 RID: 6947 RVA: 0x000FDC61 File Offset: 0x000FCE61
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2, T3, T4> && this.Equals((ValueTuple<T1, T2, T3, T4>)obj);
		}

		// Token: 0x06001B24 RID: 6948 RVA: 0x000FDC7C File Offset: 0x000FCE7C
		public bool Equals([Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1
		})] ValueTuple<T1, T2, T3, T4> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(this.Item4, other.Item4);
		}

		// Token: 0x06001B25 RID: 6949 RVA: 0x000FDCEC File Offset: 0x000FCEEC
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2, T3, T4>))
			{
				return false;
			}
			ValueTuple<T1, T2, T3, T4> valueTuple = (ValueTuple<T1, T2, T3, T4>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4);
		}

		// Token: 0x06001B26 RID: 6950 RVA: 0x000FDD85 File Offset: 0x000FCF85
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2, T3, T4>)other);
		}

		// Token: 0x06001B27 RID: 6951 RVA: 0x000FDDC8 File Offset: 0x000FCFC8
		public int CompareTo([Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1
		})] ValueTuple<T1, T2, T3, T4> other)
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
			num = Comparer<T3>.Default.Compare(this.Item3, other.Item3);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T4>.Default.Compare(this.Item4, other.Item4);
		}

		// Token: 0x06001B28 RID: 6952 RVA: 0x000FDE40 File Offset: 0x000FD040
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			ValueTuple<T1, T2, T3, T4> valueTuple = (ValueTuple<T1, T2, T3, T4>)other;
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
			num = comparer.Compare(this.Item3, valueTuple.Item3);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item4, valueTuple.Item4);
		}

		// Token: 0x06001B29 RID: 6953 RVA: 0x000FDF08 File Offset: 0x000FD108
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
			ref T4 ptr4 = ref this.Item4;
			T4 t4 = default(T4);
			int value4;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr4 = ref t4;
				if (t4 == null)
				{
					value4 = 0;
					goto IL_D4;
				}
			}
			value4 = ptr4.GetHashCode();
			IL_D4:
			return HashCode.Combine<int, int, int, int>(value, value2, value3, value4);
		}

		// Token: 0x06001B2A RID: 6954 RVA: 0x000FDFEE File Offset: 0x000FD1EE
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B2B RID: 6955 RVA: 0x000FDFF8 File Offset: 0x000FD1F8
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return HashCode.Combine<int, int, int, int>(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4));
		}

		// Token: 0x06001B2C RID: 6956 RVA: 0x000FDFEE File Offset: 0x000FD1EE
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B2D RID: 6957 RVA: 0x000FE050 File Offset: 0x000FD250
		[NullableContext(1)]
		public override string ToString()
		{
			string[] array = new string[9];
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
					goto IL_46;
				}
			}
			text = ptr.ToString();
			IL_46:
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
					goto IL_86;
				}
			}
			text2 = ptr2.ToString();
			IL_86:
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
					goto IL_C6;
				}
			}
			text3 = ptr3.ToString();
			IL_C6:
			array[num3] = text3;
			array[6] = ", ";
			int num4 = 7;
			ref T4 ptr4 = ref this.Item4;
			T4 t4 = default(T4);
			string text4;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr4 = ref t4;
				if (t4 == null)
				{
					text4 = null;
					goto IL_106;
				}
			}
			text4 = ptr4.ToString();
			IL_106:
			array[num4] = text4;
			array[8] = ")";
			return string.Concat(array);
		}

		// Token: 0x06001B2E RID: 6958 RVA: 0x000FE174 File Offset: 0x000FD374
		string IValueTupleInternal.ToStringEnd()
		{
			string[] array = new string[8];
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
			array[5] = ", ";
			int num4 = 6;
			ref T4 ptr4 = ref this.Item4;
			T4 t4 = default(T4);
			string text4;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr4 = ref t4;
				if (t4 == null)
				{
					text4 = null;
					goto IL_FD;
				}
			}
			text4 = ptr4.ToString();
			IL_FD:
			array[num4] = text4;
			array[7] = ")";
			return string.Concat(array);
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06001B2F RID: 6959 RVA: 0x000CA38E File Offset: 0x000C958E
		int ITuple.Length
		{
			get
			{
				return 4;
			}
		}

		// Token: 0x1700064B RID: 1611
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
				default:
					throw new IndexOutOfRangeException();
				}
				return result;
			}
		}

		// Token: 0x040005E0 RID: 1504
		[Nullable(1)]
		public T1 Item1;

		// Token: 0x040005E1 RID: 1505
		[Nullable(1)]
		public T2 Item2;

		// Token: 0x040005E2 RID: 1506
		[Nullable(1)]
		public T3 Item3;

		// Token: 0x040005E3 RID: 1507
		[Nullable(1)]
		public T4 Item4;
	}
}
