using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001C0 RID: 448
	[NullableContext(1)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7> : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7>>, IValueTupleInternal, ITuple
	{
		// Token: 0x06001B4F RID: 6991 RVA: 0x000FF46C File Offset: 0x000FE66C
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
			this.Item7 = item7;
		}

		// Token: 0x06001B50 RID: 6992 RVA: 0x000FF4A3 File Offset: 0x000FE6A3
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7> && this.Equals((ValueTuple<T1, T2, T3, T4, T5, T6, T7>)obj);
		}

		// Token: 0x06001B51 RID: 6993 RVA: 0x000FF4BC File Offset: 0x000FE6BC
		public bool Equals([Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		})] ValueTuple<T1, T2, T3, T4, T5, T6, T7> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(this.Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(this.Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(this.Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(this.Item7, other.Item7);
		}

		// Token: 0x06001B52 RID: 6994 RVA: 0x000FF574 File Offset: 0x000FE774
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>))
			{
				return false;
			}
			ValueTuple<T1, T2, T3, T4, T5, T6, T7> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4) && comparer.Equals(this.Item5, valueTuple.Item5) && comparer.Equals(this.Item6, valueTuple.Item6) && comparer.Equals(this.Item7, valueTuple.Item7);
		}

		// Token: 0x06001B53 RID: 6995 RVA: 0x000FF66D File Offset: 0x000FE86D
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6, T7>)other);
		}

		// Token: 0x06001B54 RID: 6996 RVA: 0x000FF6B0 File Offset: 0x000FE8B0
		public int CompareTo([Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1
		})] ValueTuple<T1, T2, T3, T4, T5, T6, T7> other)
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
			num = Comparer<T4>.Default.Compare(this.Item4, other.Item4);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T5>.Default.Compare(this.Item5, other.Item5);
			if (num != 0)
			{
				return num;
			}
			num = Comparer<T6>.Default.Compare(this.Item6, other.Item6);
			if (num != 0)
			{
				return num;
			}
			return Comparer<T7>.Default.Compare(this.Item7, other.Item7);
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x000FF77C File Offset: 0x000FE97C
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			ValueTuple<T1, T2, T3, T4, T5, T6, T7> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7>)other;
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
			num = comparer.Compare(this.Item4, valueTuple.Item4);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item5, valueTuple.Item5);
			if (num != 0)
			{
				return num;
			}
			num = comparer.Compare(this.Item6, valueTuple.Item6);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Item7, valueTuple.Item7);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x000FF8AC File Offset: 0x000FEAAC
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
			ref T5 ptr5 = ref this.Item5;
			T5 t5 = default(T5);
			int value5;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr5 = ref t5;
				if (t5 == null)
				{
					value5 = 0;
					goto IL_10C;
				}
			}
			value5 = ptr5.GetHashCode();
			IL_10C:
			ref T6 ptr6 = ref this.Item6;
			T6 t6 = default(T6);
			int value6;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr6 = ref t6;
				if (t6 == null)
				{
					value6 = 0;
					goto IL_144;
				}
			}
			value6 = ptr6.GetHashCode();
			IL_144:
			ref T7 ptr7 = ref this.Item7;
			T7 t7 = default(T7);
			int value7;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr7 = ref t7;
				if (t7 == null)
				{
					value7 = 0;
					goto IL_17C;
				}
			}
			value7 = ptr7.GetHashCode();
			IL_17C:
			return HashCode.Combine<int, int, int, int, int, int, int>(value, value2, value3, value4, value5, value6, value7);
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x000FFA3A File Offset: 0x000FEC3A
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B58 RID: 7000 RVA: 0x000FFA44 File Offset: 0x000FEC44
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			return HashCode.Combine<int, int, int, int, int, int, int>(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7));
		}

		// Token: 0x06001B59 RID: 7001 RVA: 0x000FFA3A File Offset: 0x000FEC3A
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B5A RID: 7002 RVA: 0x000FFAD0 File Offset: 0x000FECD0
		public override string ToString()
		{
			string[] array = new string[15];
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
			array[8] = ", ";
			int num5 = 9;
			ref T5 ptr5 = ref this.Item5;
			T5 t5 = default(T5);
			string text5;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr5 = ref t5;
				if (t5 == null)
				{
					text5 = null;
					goto IL_14A;
				}
			}
			text5 = ptr5.ToString();
			IL_14A:
			array[num5] = text5;
			array[10] = ", ";
			int num6 = 11;
			ref T6 ptr6 = ref this.Item6;
			T6 t6 = default(T6);
			string text6;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr6 = ref t6;
				if (t6 == null)
				{
					text6 = null;
					goto IL_18F;
				}
			}
			text6 = ptr6.ToString();
			IL_18F:
			array[num6] = text6;
			array[12] = ", ";
			int num7 = 13;
			ref T7 ptr7 = ref this.Item7;
			T7 t7 = default(T7);
			string text7;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr7 = ref t7;
				if (t7 == null)
				{
					text7 = null;
					goto IL_1D4;
				}
			}
			text7 = ptr7.ToString();
			IL_1D4:
			array[num7] = text7;
			array[14] = ")";
			return string.Concat(array);
		}

		// Token: 0x06001B5B RID: 7003 RVA: 0x000FFCC0 File Offset: 0x000FEEC0
		string IValueTupleInternal.ToStringEnd()
		{
			string[] array = new string[14];
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
					goto IL_3E;
				}
			}
			text = ptr.ToString();
			IL_3E:
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
					goto IL_7E;
				}
			}
			text2 = ptr2.ToString();
			IL_7E:
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
					goto IL_BE;
				}
			}
			text3 = ptr3.ToString();
			IL_BE:
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
					goto IL_FE;
				}
			}
			text4 = ptr4.ToString();
			IL_FE:
			array[num4] = text4;
			array[7] = ", ";
			int num5 = 8;
			ref T5 ptr5 = ref this.Item5;
			T5 t5 = default(T5);
			string text5;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr5 = ref t5;
				if (t5 == null)
				{
					text5 = null;
					goto IL_141;
				}
			}
			text5 = ptr5.ToString();
			IL_141:
			array[num5] = text5;
			array[9] = ", ";
			int num6 = 10;
			ref T6 ptr6 = ref this.Item6;
			T6 t6 = default(T6);
			string text6;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr6 = ref t6;
				if (t6 == null)
				{
					text6 = null;
					goto IL_186;
				}
			}
			text6 = ptr6.ToString();
			IL_186:
			array[num6] = text6;
			array[11] = ", ";
			int num7 = 12;
			ref T7 ptr7 = ref this.Item7;
			T7 t7 = default(T7);
			string text7;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr7 = ref t7;
				if (t7 == null)
				{
					text7 = null;
					goto IL_1CB;
				}
			}
			text7 = ptr7.ToString();
			IL_1CB:
			array[num7] = text7;
			array[13] = ")";
			return string.Concat(array);
		}

		// Token: 0x17000650 RID: 1616
		// (get) Token: 0x06001B5C RID: 7004 RVA: 0x000DA80B File Offset: 0x000D9A0B
		int ITuple.Length
		{
			get
			{
				return 7;
			}
		}

		// Token: 0x17000651 RID: 1617
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

		// Token: 0x040005EF RID: 1519
		public T1 Item1;

		// Token: 0x040005F0 RID: 1520
		public T2 Item2;

		// Token: 0x040005F1 RID: 1521
		public T3 Item3;

		// Token: 0x040005F2 RID: 1522
		public T4 Item4;

		// Token: 0x040005F3 RID: 1523
		public T5 Item5;

		// Token: 0x040005F4 RID: 1524
		public T6 Item6;

		// Token: 0x040005F5 RID: 1525
		public T7 Item7;
	}
}
