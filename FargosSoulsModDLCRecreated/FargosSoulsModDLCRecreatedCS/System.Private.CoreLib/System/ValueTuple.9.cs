using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x020001C1 RID: 449
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Nullable(0)]
	[NullableContext(1)]
	[Serializable]
	[StructLayout(LayoutKind.Auto)]
	public struct ValueTuple<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(0)] TRest> : IEquatable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IStructuralEquatable, IStructuralComparable, IComparable, IComparable<ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>>, IValueTupleInternal, ITuple where TRest : struct
	{
		// Token: 0x06001B5E RID: 7006 RVA: 0x000FFF44 File Offset: 0x000FF144
		public ValueTuple(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, [Nullable(0)] TRest rest)
		{
			if (!(rest is IValueTupleInternal))
			{
				throw new ArgumentException(SR.ArgumentException_ValueTupleLastArgumentNotAValueTuple);
			}
			this.Item1 = item1;
			this.Item2 = item2;
			this.Item3 = item3;
			this.Item4 = item4;
			this.Item5 = item5;
			this.Item6 = item6;
			this.Item7 = item7;
			this.Rest = rest;
		}

		// Token: 0x06001B5F RID: 7007 RVA: 0x000FFFA7 File Offset: 0x000FF1A7
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return obj is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> && this.Equals((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)obj);
		}

		// Token: 0x06001B60 RID: 7008 RVA: 0x000FFFC0 File Offset: 0x000FF1C0
		public bool Equals([Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0
		})] ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
		{
			return EqualityComparer<T1>.Default.Equals(this.Item1, other.Item1) && EqualityComparer<T2>.Default.Equals(this.Item2, other.Item2) && EqualityComparer<T3>.Default.Equals(this.Item3, other.Item3) && EqualityComparer<T4>.Default.Equals(this.Item4, other.Item4) && EqualityComparer<T5>.Default.Equals(this.Item5, other.Item5) && EqualityComparer<T6>.Default.Equals(this.Item6, other.Item6) && EqualityComparer<T7>.Default.Equals(this.Item7, other.Item7) && EqualityComparer<TRest>.Default.Equals(this.Rest, other.Rest);
		}

		// Token: 0x06001B61 RID: 7009 RVA: 0x00100094 File Offset: 0x000FF294
		bool IStructuralEquatable.Equals(object other, IEqualityComparer comparer)
		{
			if (other == null || !(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
			{
				return false;
			}
			ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other;
			return comparer.Equals(this.Item1, valueTuple.Item1) && comparer.Equals(this.Item2, valueTuple.Item2) && comparer.Equals(this.Item3, valueTuple.Item3) && comparer.Equals(this.Item4, valueTuple.Item4) && comparer.Equals(this.Item5, valueTuple.Item5) && comparer.Equals(this.Item6, valueTuple.Item6) && comparer.Equals(this.Item7, valueTuple.Item7) && comparer.Equals(this.Rest, valueTuple.Rest);
		}

		// Token: 0x06001B62 RID: 7010 RVA: 0x001001AE File Offset: 0x000FF3AE
		int IComparable.CompareTo(object other)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			return this.CompareTo((ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other);
		}

		// Token: 0x06001B63 RID: 7011 RVA: 0x001001F0 File Offset: 0x000FF3F0
		public int CompareTo([Nullable(new byte[]
		{
			0,
			1,
			1,
			1,
			1,
			1,
			1,
			1,
			0
		})] ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> other)
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
			num = Comparer<T7>.Default.Compare(this.Item7, other.Item7);
			if (num != 0)
			{
				return num;
			}
			return Comparer<TRest>.Default.Compare(this.Rest, other.Rest);
		}

		// Token: 0x06001B64 RID: 7012 RVA: 0x001002D8 File Offset: 0x000FF4D8
		int IStructuralComparable.CompareTo(object other, IComparer comparer)
		{
			if (other == null)
			{
				return 1;
			}
			if (!(other is ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>))
			{
				throw new ArgumentException(SR.Format(SR.ArgumentException_ValueTupleIncorrectType, base.GetType()), "other");
			}
			ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest> valueTuple = (ValueTuple<T1, T2, T3, T4, T5, T6, T7, TRest>)other;
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
			num = comparer.Compare(this.Item7, valueTuple.Item7);
			if (num != 0)
			{
				return num;
			}
			return comparer.Compare(this.Rest, valueTuple.Rest);
		}

		// Token: 0x06001B65 RID: 7013 RVA: 0x00100428 File Offset: 0x000FF628
		public override int GetHashCode()
		{
			if (!(this.Rest is IValueTupleInternal))
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
						goto IL_4A;
					}
				}
				value = ptr.GetHashCode();
				IL_4A:
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
						goto IL_82;
					}
				}
				value2 = ptr2.GetHashCode();
				IL_82:
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
						goto IL_BA;
					}
				}
				value3 = ptr3.GetHashCode();
				IL_BA:
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
						goto IL_F2;
					}
				}
				value4 = ptr4.GetHashCode();
				IL_F2:
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
						goto IL_12A;
					}
				}
				value5 = ptr5.GetHashCode();
				IL_12A:
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
						goto IL_162;
					}
				}
				value6 = ptr6.GetHashCode();
				IL_162:
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
						goto IL_19A;
					}
				}
				value7 = ptr7.GetHashCode();
				IL_19A:
				return HashCode.Combine<int, int, int, int, int, int, int>(value, value2, value3, value4, value5, value6, value7);
			}
			int length = ((IValueTupleInternal)((object)this.Rest)).Length;
			int hashCode = this.Rest.GetHashCode();
			if (length >= 8)
			{
				return hashCode;
			}
			switch (8 - length)
			{
			case 1:
			{
				ref T7 ptr8 = ref this.Item7;
				T7 t7 = default(T7);
				int value8;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr8 = ref t7;
					if (t7 == null)
					{
						value8 = 0;
						goto IL_237;
					}
				}
				value8 = ptr8.GetHashCode();
				IL_237:
				return HashCode.Combine<int, int>(value8, hashCode);
			}
			case 2:
			{
				ref T6 ptr9 = ref this.Item6;
				T6 t6 = default(T6);
				int value9;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr9 = ref t6;
					if (t6 == null)
					{
						value9 = 0;
						goto IL_276;
					}
				}
				value9 = ptr9.GetHashCode();
				IL_276:
				ref T7 ptr10 = ref this.Item7;
				T7 t7 = default(T7);
				int value10;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr10 = ref t7;
					if (t7 == null)
					{
						value10 = 0;
						goto IL_2AE;
					}
				}
				value10 = ptr10.GetHashCode();
				IL_2AE:
				return HashCode.Combine<int, int, int>(value9, value10, hashCode);
			}
			case 3:
			{
				ref T5 ptr11 = ref this.Item5;
				T5 t5 = default(T5);
				int value11;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr11 = ref t5;
					if (t5 == null)
					{
						value11 = 0;
						goto IL_2ED;
					}
				}
				value11 = ptr11.GetHashCode();
				IL_2ED:
				ref T6 ptr12 = ref this.Item6;
				T6 t6 = default(T6);
				int value12;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr12 = ref t6;
					if (t6 == null)
					{
						value12 = 0;
						goto IL_325;
					}
				}
				value12 = ptr12.GetHashCode();
				IL_325:
				ref T7 ptr13 = ref this.Item7;
				T7 t7 = default(T7);
				int value13;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr13 = ref t7;
					if (t7 == null)
					{
						value13 = 0;
						goto IL_35D;
					}
				}
				value13 = ptr13.GetHashCode();
				IL_35D:
				return HashCode.Combine<int, int, int, int>(value11, value12, value13, hashCode);
			}
			case 4:
			{
				ref T4 ptr14 = ref this.Item4;
				T4 t4 = default(T4);
				int value14;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr14 = ref t4;
					if (t4 == null)
					{
						value14 = 0;
						goto IL_39C;
					}
				}
				value14 = ptr14.GetHashCode();
				IL_39C:
				ref T5 ptr15 = ref this.Item5;
				T5 t5 = default(T5);
				int value15;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr15 = ref t5;
					if (t5 == null)
					{
						value15 = 0;
						goto IL_3D4;
					}
				}
				value15 = ptr15.GetHashCode();
				IL_3D4:
				ref T6 ptr16 = ref this.Item6;
				T6 t6 = default(T6);
				int value16;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr16 = ref t6;
					if (t6 == null)
					{
						value16 = 0;
						goto IL_40C;
					}
				}
				value16 = ptr16.GetHashCode();
				IL_40C:
				ref T7 ptr17 = ref this.Item7;
				T7 t7 = default(T7);
				int value17;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr17 = ref t7;
					if (t7 == null)
					{
						value17 = 0;
						goto IL_444;
					}
				}
				value17 = ptr17.GetHashCode();
				IL_444:
				return HashCode.Combine<int, int, int, int, int>(value14, value15, value16, value17, hashCode);
			}
			case 5:
			{
				ref T3 ptr18 = ref this.Item3;
				T3 t3 = default(T3);
				int value18;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr18 = ref t3;
					if (t3 == null)
					{
						value18 = 0;
						goto IL_483;
					}
				}
				value18 = ptr18.GetHashCode();
				IL_483:
				ref T4 ptr19 = ref this.Item4;
				T4 t4 = default(T4);
				int value19;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr19 = ref t4;
					if (t4 == null)
					{
						value19 = 0;
						goto IL_4BB;
					}
				}
				value19 = ptr19.GetHashCode();
				IL_4BB:
				ref T5 ptr20 = ref this.Item5;
				T5 t5 = default(T5);
				int value20;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr20 = ref t5;
					if (t5 == null)
					{
						value20 = 0;
						goto IL_4F3;
					}
				}
				value20 = ptr20.GetHashCode();
				IL_4F3:
				ref T6 ptr21 = ref this.Item6;
				T6 t6 = default(T6);
				int value21;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr21 = ref t6;
					if (t6 == null)
					{
						value21 = 0;
						goto IL_52B;
					}
				}
				value21 = ptr21.GetHashCode();
				IL_52B:
				ref T7 ptr22 = ref this.Item7;
				T7 t7 = default(T7);
				int value22;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr22 = ref t7;
					if (t7 == null)
					{
						value22 = 0;
						goto IL_563;
					}
				}
				value22 = ptr22.GetHashCode();
				IL_563:
				return HashCode.Combine<int, int, int, int, int, int>(value18, value19, value20, value21, value22, hashCode);
			}
			case 6:
			{
				ref T2 ptr23 = ref this.Item2;
				T2 t2 = default(T2);
				int value23;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr23 = ref t2;
					if (t2 == null)
					{
						value23 = 0;
						goto IL_5A2;
					}
				}
				value23 = ptr23.GetHashCode();
				IL_5A2:
				ref T3 ptr24 = ref this.Item3;
				T3 t3 = default(T3);
				int value24;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr24 = ref t3;
					if (t3 == null)
					{
						value24 = 0;
						goto IL_5DA;
					}
				}
				value24 = ptr24.GetHashCode();
				IL_5DA:
				ref T4 ptr25 = ref this.Item4;
				T4 t4 = default(T4);
				int value25;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr25 = ref t4;
					if (t4 == null)
					{
						value25 = 0;
						goto IL_612;
					}
				}
				value25 = ptr25.GetHashCode();
				IL_612:
				ref T5 ptr26 = ref this.Item5;
				T5 t5 = default(T5);
				int value26;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr26 = ref t5;
					if (t5 == null)
					{
						value26 = 0;
						goto IL_64A;
					}
				}
				value26 = ptr26.GetHashCode();
				IL_64A:
				ref T6 ptr27 = ref this.Item6;
				T6 t6 = default(T6);
				int value27;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr27 = ref t6;
					if (t6 == null)
					{
						value27 = 0;
						goto IL_682;
					}
				}
				value27 = ptr27.GetHashCode();
				IL_682:
				ref T7 ptr28 = ref this.Item7;
				T7 t7 = default(T7);
				int value28;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr28 = ref t7;
					if (t7 == null)
					{
						value28 = 0;
						goto IL_6BA;
					}
				}
				value28 = ptr28.GetHashCode();
				IL_6BA:
				return HashCode.Combine<int, int, int, int, int, int, int>(value23, value24, value25, value26, value27, value28, hashCode);
			}
			case 7:
			case 8:
			{
				ref T1 ptr29 = ref this.Item1;
				T1 t = default(T1);
				int value29;
				if (t == null)
				{
					t = this.Item1;
					ptr29 = ref t;
					if (t == null)
					{
						value29 = 0;
						goto IL_6F6;
					}
				}
				value29 = ptr29.GetHashCode();
				IL_6F6:
				ref T2 ptr30 = ref this.Item2;
				T2 t2 = default(T2);
				int value30;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr30 = ref t2;
					if (t2 == null)
					{
						value30 = 0;
						goto IL_72E;
					}
				}
				value30 = ptr30.GetHashCode();
				IL_72E:
				ref T3 ptr31 = ref this.Item3;
				T3 t3 = default(T3);
				int value31;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr31 = ref t3;
					if (t3 == null)
					{
						value31 = 0;
						goto IL_766;
					}
				}
				value31 = ptr31.GetHashCode();
				IL_766:
				ref T4 ptr32 = ref this.Item4;
				T4 t4 = default(T4);
				int value32;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr32 = ref t4;
					if (t4 == null)
					{
						value32 = 0;
						goto IL_79E;
					}
				}
				value32 = ptr32.GetHashCode();
				IL_79E:
				ref T5 ptr33 = ref this.Item5;
				T5 t5 = default(T5);
				int value33;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr33 = ref t5;
					if (t5 == null)
					{
						value33 = 0;
						goto IL_7D6;
					}
				}
				value33 = ptr33.GetHashCode();
				IL_7D6:
				ref T6 ptr34 = ref this.Item6;
				T6 t6 = default(T6);
				int value34;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr34 = ref t6;
					if (t6 == null)
					{
						value34 = 0;
						goto IL_80E;
					}
				}
				value34 = ptr34.GetHashCode();
				IL_80E:
				ref T7 ptr35 = ref this.Item7;
				T7 t7 = default(T7);
				int value35;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr35 = ref t7;
					if (t7 == null)
					{
						value35 = 0;
						goto IL_846;
					}
				}
				value35 = ptr35.GetHashCode();
				IL_846:
				return HashCode.Combine<int, int, int, int, int, int, int, int>(value29, value30, value31, value32, value33, value34, value35, hashCode);
			}
			default:
				return -1;
			}
		}

		// Token: 0x06001B66 RID: 7014 RVA: 0x00100C83 File Offset: 0x000FFE83
		int IStructuralEquatable.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B67 RID: 7015 RVA: 0x00100C8C File Offset: 0x000FFE8C
		private int GetHashCodeCore(IEqualityComparer comparer)
		{
			IValueTupleInternal valueTupleInternal = this.Rest as IValueTupleInternal;
			if (valueTupleInternal == null)
			{
				return HashCode.Combine<int, int, int, int, int, int, int>(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7));
			}
			int length = valueTupleInternal.Length;
			int hashCode = valueTupleInternal.GetHashCode(comparer);
			if (length >= 8)
			{
				return hashCode;
			}
			switch (8 - length)
			{
			case 1:
				return HashCode.Combine<int, int>(comparer.GetHashCode(this.Item7), hashCode);
			case 2:
				return HashCode.Combine<int, int, int>(comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), hashCode);
			case 3:
				return HashCode.Combine<int, int, int, int>(comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), hashCode);
			case 4:
				return HashCode.Combine<int, int, int, int, int>(comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), hashCode);
			case 5:
				return HashCode.Combine<int, int, int, int, int, int>(comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), hashCode);
			case 6:
				return HashCode.Combine<int, int, int, int, int, int, int>(comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), hashCode);
			case 7:
			case 8:
				return HashCode.Combine<int, int, int, int, int, int, int, int>(comparer.GetHashCode(this.Item1), comparer.GetHashCode(this.Item2), comparer.GetHashCode(this.Item3), comparer.GetHashCode(this.Item4), comparer.GetHashCode(this.Item5), comparer.GetHashCode(this.Item6), comparer.GetHashCode(this.Item7), hashCode);
			default:
				return -1;
			}
		}

		// Token: 0x06001B68 RID: 7016 RVA: 0x00100C83 File Offset: 0x000FFE83
		int IValueTupleInternal.GetHashCode(IEqualityComparer comparer)
		{
			return this.GetHashCodeCore(comparer);
		}

		// Token: 0x06001B69 RID: 7017 RVA: 0x00100F80 File Offset: 0x00100180
		public override string ToString()
		{
			T1 t;
			T2 t2;
			T3 t3;
			T4 t4;
			T5 t5;
			T6 t6;
			T7 t7;
			if (this.Rest is IValueTupleInternal)
			{
				string[] array = new string[16];
				array[0] = "(";
				int num = 1;
				ref T1 ptr = ref this.Item1;
				t = default(T1);
				string text;
				if (t == null)
				{
					t = this.Item1;
					ptr = ref t;
					if (t == null)
					{
						text = null;
						goto IL_5B;
					}
				}
				text = ptr.ToString();
				IL_5B:
				array[num] = text;
				array[2] = ", ";
				int num2 = 3;
				ref T2 ptr2 = ref this.Item2;
				t2 = default(T2);
				string text2;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr2 = ref t2;
					if (t2 == null)
					{
						text2 = null;
						goto IL_9B;
					}
				}
				text2 = ptr2.ToString();
				IL_9B:
				array[num2] = text2;
				array[4] = ", ";
				int num3 = 5;
				ref T3 ptr3 = ref this.Item3;
				t3 = default(T3);
				string text3;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr3 = ref t3;
					if (t3 == null)
					{
						text3 = null;
						goto IL_DB;
					}
				}
				text3 = ptr3.ToString();
				IL_DB:
				array[num3] = text3;
				array[6] = ", ";
				int num4 = 7;
				ref T4 ptr4 = ref this.Item4;
				t4 = default(T4);
				string text4;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr4 = ref t4;
					if (t4 == null)
					{
						text4 = null;
						goto IL_11B;
					}
				}
				text4 = ptr4.ToString();
				IL_11B:
				array[num4] = text4;
				array[8] = ", ";
				int num5 = 9;
				ref T5 ptr5 = ref this.Item5;
				t5 = default(T5);
				string text5;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr5 = ref t5;
					if (t5 == null)
					{
						text5 = null;
						goto IL_15F;
					}
				}
				text5 = ptr5.ToString();
				IL_15F:
				array[num5] = text5;
				array[10] = ", ";
				int num6 = 11;
				ref T6 ptr6 = ref this.Item6;
				t6 = default(T6);
				string text6;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr6 = ref t6;
					if (t6 == null)
					{
						text6 = null;
						goto IL_1A4;
					}
				}
				text6 = ptr6.ToString();
				IL_1A4:
				array[num6] = text6;
				array[12] = ", ";
				int num7 = 13;
				ref T7 ptr7 = ref this.Item7;
				t7 = default(T7);
				string text7;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr7 = ref t7;
					if (t7 == null)
					{
						text7 = null;
						goto IL_1E9;
					}
				}
				text7 = ptr7.ToString();
				IL_1E9:
				array[num7] = text7;
				array[14] = ", ";
				array[15] = ((IValueTupleInternal)((object)this.Rest)).ToStringEnd();
				return string.Concat(array);
			}
			string[] array2 = new string[17];
			array2[0] = "(";
			int num8 = 1;
			ref T1 ptr8 = ref this.Item1;
			t = default(T1);
			string text8;
			if (t == null)
			{
				t = this.Item1;
				ptr8 = ref t;
				if (t == null)
				{
					text8 = null;
					goto IL_258;
				}
			}
			text8 = ptr8.ToString();
			IL_258:
			array2[num8] = text8;
			array2[2] = ", ";
			int num9 = 3;
			ref T2 ptr9 = ref this.Item2;
			t2 = default(T2);
			string text9;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr9 = ref t2;
				if (t2 == null)
				{
					text9 = null;
					goto IL_298;
				}
			}
			text9 = ptr9.ToString();
			IL_298:
			array2[num9] = text9;
			array2[4] = ", ";
			int num10 = 5;
			ref T3 ptr10 = ref this.Item3;
			t3 = default(T3);
			string text10;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr10 = ref t3;
				if (t3 == null)
				{
					text10 = null;
					goto IL_2D8;
				}
			}
			text10 = ptr10.ToString();
			IL_2D8:
			array2[num10] = text10;
			array2[6] = ", ";
			int num11 = 7;
			ref T4 ptr11 = ref this.Item4;
			t4 = default(T4);
			string text11;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr11 = ref t4;
				if (t4 == null)
				{
					text11 = null;
					goto IL_318;
				}
			}
			text11 = ptr11.ToString();
			IL_318:
			array2[num11] = text11;
			array2[8] = ", ";
			int num12 = 9;
			ref T5 ptr12 = ref this.Item5;
			t5 = default(T5);
			string text12;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr12 = ref t5;
				if (t5 == null)
				{
					text12 = null;
					goto IL_35C;
				}
			}
			text12 = ptr12.ToString();
			IL_35C:
			array2[num12] = text12;
			array2[10] = ", ";
			int num13 = 11;
			ref T6 ptr13 = ref this.Item6;
			t6 = default(T6);
			string text13;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr13 = ref t6;
				if (t6 == null)
				{
					text13 = null;
					goto IL_3A1;
				}
			}
			text13 = ptr13.ToString();
			IL_3A1:
			array2[num13] = text13;
			array2[12] = ", ";
			int num14 = 13;
			ref T7 ptr14 = ref this.Item7;
			t7 = default(T7);
			string text14;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr14 = ref t7;
				if (t7 == null)
				{
					text14 = null;
					goto IL_3E6;
				}
			}
			text14 = ptr14.ToString();
			IL_3E6:
			array2[num14] = text14;
			array2[14] = ", ";
			array2[15] = this.Rest.ToString();
			array2[16] = ")";
			return string.Concat(array2);
		}

		// Token: 0x06001B6A RID: 7018 RVA: 0x001013A0 File Offset: 0x001005A0
		string IValueTupleInternal.ToStringEnd()
		{
			T1 t;
			T2 t2;
			T3 t3;
			T4 t4;
			T5 t5;
			T6 t6;
			T7 t7;
			if (this.Rest is IValueTupleInternal)
			{
				string[] array = new string[15];
				int num = 0;
				ref T1 ptr = ref this.Item1;
				t = default(T1);
				string text;
				if (t == null)
				{
					t = this.Item1;
					ptr = ref t;
					if (t == null)
					{
						text = null;
						goto IL_53;
					}
				}
				text = ptr.ToString();
				IL_53:
				array[num] = text;
				array[1] = ", ";
				int num2 = 2;
				ref T2 ptr2 = ref this.Item2;
				t2 = default(T2);
				string text2;
				if (t2 == null)
				{
					t2 = this.Item2;
					ptr2 = ref t2;
					if (t2 == null)
					{
						text2 = null;
						goto IL_93;
					}
				}
				text2 = ptr2.ToString();
				IL_93:
				array[num2] = text2;
				array[3] = ", ";
				int num3 = 4;
				ref T3 ptr3 = ref this.Item3;
				t3 = default(T3);
				string text3;
				if (t3 == null)
				{
					t3 = this.Item3;
					ptr3 = ref t3;
					if (t3 == null)
					{
						text3 = null;
						goto IL_D3;
					}
				}
				text3 = ptr3.ToString();
				IL_D3:
				array[num3] = text3;
				array[5] = ", ";
				int num4 = 6;
				ref T4 ptr4 = ref this.Item4;
				t4 = default(T4);
				string text4;
				if (t4 == null)
				{
					t4 = this.Item4;
					ptr4 = ref t4;
					if (t4 == null)
					{
						text4 = null;
						goto IL_113;
					}
				}
				text4 = ptr4.ToString();
				IL_113:
				array[num4] = text4;
				array[7] = ", ";
				int num5 = 8;
				ref T5 ptr5 = ref this.Item5;
				t5 = default(T5);
				string text5;
				if (t5 == null)
				{
					t5 = this.Item5;
					ptr5 = ref t5;
					if (t5 == null)
					{
						text5 = null;
						goto IL_156;
					}
				}
				text5 = ptr5.ToString();
				IL_156:
				array[num5] = text5;
				array[9] = ", ";
				int num6 = 10;
				ref T6 ptr6 = ref this.Item6;
				t6 = default(T6);
				string text6;
				if (t6 == null)
				{
					t6 = this.Item6;
					ptr6 = ref t6;
					if (t6 == null)
					{
						text6 = null;
						goto IL_19B;
					}
				}
				text6 = ptr6.ToString();
				IL_19B:
				array[num6] = text6;
				array[11] = ", ";
				int num7 = 12;
				ref T7 ptr7 = ref this.Item7;
				t7 = default(T7);
				string text7;
				if (t7 == null)
				{
					t7 = this.Item7;
					ptr7 = ref t7;
					if (t7 == null)
					{
						text7 = null;
						goto IL_1E0;
					}
				}
				text7 = ptr7.ToString();
				IL_1E0:
				array[num7] = text7;
				array[13] = ", ";
				array[14] = ((IValueTupleInternal)((object)this.Rest)).ToStringEnd();
				return string.Concat(array);
			}
			string[] array2 = new string[16];
			int num8 = 0;
			ref T1 ptr8 = ref this.Item1;
			t = default(T1);
			string text8;
			if (t == null)
			{
				t = this.Item1;
				ptr8 = ref t;
				if (t == null)
				{
					text8 = null;
					goto IL_247;
				}
			}
			text8 = ptr8.ToString();
			IL_247:
			array2[num8] = text8;
			array2[1] = ", ";
			int num9 = 2;
			ref T2 ptr9 = ref this.Item2;
			t2 = default(T2);
			string text9;
			if (t2 == null)
			{
				t2 = this.Item2;
				ptr9 = ref t2;
				if (t2 == null)
				{
					text9 = null;
					goto IL_287;
				}
			}
			text9 = ptr9.ToString();
			IL_287:
			array2[num9] = text9;
			array2[3] = ", ";
			int num10 = 4;
			ref T3 ptr10 = ref this.Item3;
			t3 = default(T3);
			string text10;
			if (t3 == null)
			{
				t3 = this.Item3;
				ptr10 = ref t3;
				if (t3 == null)
				{
					text10 = null;
					goto IL_2C7;
				}
			}
			text10 = ptr10.ToString();
			IL_2C7:
			array2[num10] = text10;
			array2[5] = ", ";
			int num11 = 6;
			ref T4 ptr11 = ref this.Item4;
			t4 = default(T4);
			string text11;
			if (t4 == null)
			{
				t4 = this.Item4;
				ptr11 = ref t4;
				if (t4 == null)
				{
					text11 = null;
					goto IL_307;
				}
			}
			text11 = ptr11.ToString();
			IL_307:
			array2[num11] = text11;
			array2[7] = ", ";
			int num12 = 8;
			ref T5 ptr12 = ref this.Item5;
			t5 = default(T5);
			string text12;
			if (t5 == null)
			{
				t5 = this.Item5;
				ptr12 = ref t5;
				if (t5 == null)
				{
					text12 = null;
					goto IL_34A;
				}
			}
			text12 = ptr12.ToString();
			IL_34A:
			array2[num12] = text12;
			array2[9] = ", ";
			int num13 = 10;
			ref T6 ptr13 = ref this.Item6;
			t6 = default(T6);
			string text13;
			if (t6 == null)
			{
				t6 = this.Item6;
				ptr13 = ref t6;
				if (t6 == null)
				{
					text13 = null;
					goto IL_38F;
				}
			}
			text13 = ptr13.ToString();
			IL_38F:
			array2[num13] = text13;
			array2[11] = ", ";
			int num14 = 12;
			ref T7 ptr14 = ref this.Item7;
			t7 = default(T7);
			string text14;
			if (t7 == null)
			{
				t7 = this.Item7;
				ptr14 = ref t7;
				if (t7 == null)
				{
					text14 = null;
					goto IL_3D4;
				}
			}
			text14 = ptr14.ToString();
			IL_3D4:
			array2[num14] = text14;
			array2[13] = ", ";
			array2[14] = this.Rest.ToString();
			array2[15] = ")";
			return string.Concat(array2);
		}

		// Token: 0x17000652 RID: 1618
		// (get) Token: 0x06001B6B RID: 7019 RVA: 0x001017AE File Offset: 0x001009AE
		int ITuple.Length
		{
			get
			{
				if (!(this.Rest is IValueTupleInternal))
				{
					return 8;
				}
				return 7 + ((IValueTupleInternal)((object)this.Rest)).Length;
			}
		}

		// Token: 0x17000653 RID: 1619
		[Nullable(2)]
		object ITuple.this[int index]
		{
			get
			{
				switch (index)
				{
				case 0:
					return this.Item1;
				case 1:
					return this.Item2;
				case 2:
					return this.Item3;
				case 3:
					return this.Item4;
				case 4:
					return this.Item5;
				case 5:
					return this.Item6;
				case 6:
					return this.Item7;
				default:
					if (this.Rest is IValueTupleInternal)
					{
						return ((IValueTupleInternal)((object)this.Rest))[index - 7];
					}
					if (index == 7)
					{
						return this.Rest;
					}
					throw new IndexOutOfRangeException();
				}
			}
		}

		// Token: 0x040005F6 RID: 1526
		public T1 Item1;

		// Token: 0x040005F7 RID: 1527
		public T2 Item2;

		// Token: 0x040005F8 RID: 1528
		public T3 Item3;

		// Token: 0x040005F9 RID: 1529
		public T4 Item4;

		// Token: 0x040005FA RID: 1530
		public T5 Item5;

		// Token: 0x040005FB RID: 1531
		public T6 Item6;

		// Token: 0x040005FC RID: 1532
		public T7 Item7;

		// Token: 0x040005FD RID: 1533
		[Nullable(0)]
		public TRest Rest;
	}
}
