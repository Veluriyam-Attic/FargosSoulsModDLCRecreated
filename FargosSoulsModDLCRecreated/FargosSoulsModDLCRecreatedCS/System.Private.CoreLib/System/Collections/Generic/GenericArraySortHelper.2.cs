using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007D4 RID: 2004
	internal class GenericArraySortHelper<TKey, TValue> : IArraySortHelper<TKey, TValue> where TKey : IComparable<TKey>
	{
		// Token: 0x06006076 RID: 24694 RVA: 0x001CDAF8 File Offset: 0x001CCCF8
		public void Sort(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
		{
			try
			{
				if (comparer == null || comparer == Comparer<TKey>.Default)
				{
					if (keys.Length > 1)
					{
						if (typeof(TKey) == typeof(double) || typeof(TKey) == typeof(float) || typeof(TKey) == typeof(Half))
						{
							int num = SortUtils.MoveNansToFront<TKey, TValue>(keys, values);
							if (num == keys.Length)
							{
								return;
							}
							keys = keys.Slice(num);
							values = values.Slice(num);
						}
						GenericArraySortHelper<TKey, TValue>.IntroSort(keys, values, 2 * (BitOperations.Log2((uint)keys.Length) + 1));
					}
				}
				else
				{
					ArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, comparer);
				}
			}
			catch (IndexOutOfRangeException)
			{
				ThrowHelper.ThrowArgumentException_BadComparer(comparer);
			}
			catch (Exception e)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, e);
			}
		}

		// Token: 0x06006077 RID: 24695 RVA: 0x001CDBEC File Offset: 0x001CCDEC
		private unsafe static void SwapIfGreaterWithValues(Span<TKey> keys, Span<TValue> values, int i, int j)
		{
			ref TKey ptr = ref keys[i];
			if (ptr != null && GenericArraySortHelper<TKey, TValue>.GreaterThan(ref ptr, keys[j]))
			{
				TKey tkey = ptr;
				*keys[i] = *keys[j];
				*keys[j] = tkey;
				TValue tvalue = *values[i];
				*values[i] = *values[j];
				*values[j] = tvalue;
			}
		}

		// Token: 0x06006078 RID: 24696 RVA: 0x001CD690 File Offset: 0x001CC890
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void Swap(Span<TKey> keys, Span<TValue> values, int i, int j)
		{
			TKey tkey = *keys[i];
			*keys[i] = *keys[j];
			*keys[j] = tkey;
			TValue tvalue = *values[i];
			*values[i] = *values[j];
			*values[j] = tvalue;
		}

		// Token: 0x06006079 RID: 24697 RVA: 0x001CDC84 File Offset: 0x001CCE84
		private static void IntroSort(Span<TKey> keys, Span<TValue> values, int depthLimit)
		{
			int i = keys.Length;
			while (i > 1)
			{
				if (i <= 16)
				{
					if (i == 2)
					{
						GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, 1);
						return;
					}
					if (i == 3)
					{
						GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, 1);
						GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, 2);
						GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 1, 2);
						return;
					}
					GenericArraySortHelper<TKey, TValue>.InsertionSort(keys.Slice(0, i), values.Slice(0, i));
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						GenericArraySortHelper<TKey, TValue>.HeapSort(keys.Slice(0, i), values.Slice(0, i));
						return;
					}
					depthLimit--;
					int num = GenericArraySortHelper<TKey, TValue>.PickPivotAndPartition(keys.Slice(0, i), values.Slice(0, i));
					Span<TKey> span = keys;
					int num2 = num + 1;
					int num3 = i - num2;
					Span<TKey> keys2 = span.Slice(num2, num3);
					Span<TValue> span2 = values;
					num3 = num + 1;
					num2 = i - num3;
					GenericArraySortHelper<TKey, TValue>.IntroSort(keys2, span2.Slice(num3, num2), depthLimit);
					i = num;
				}
			}
		}

		// Token: 0x0600607A RID: 24698 RVA: 0x001CDD5C File Offset: 0x001CCF5C
		private unsafe static int PickPivotAndPartition(Span<TKey> keys, Span<TValue> values)
		{
			int num = keys.Length - 1;
			int num2 = num >> 1;
			GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, num2);
			GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, 0, num);
			GenericArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, num2, num);
			TKey tkey = *keys[num2];
			GenericArraySortHelper<TKey, TValue>.Swap(keys, values, num2, num - 1);
			int i = 0;
			int j = num - 1;
			while (i < j)
			{
				if (tkey == null)
				{
					while (i < num - 1 && *keys[++i] == null)
					{
					}
					while (j > 0)
					{
						if (*keys[--j] == null)
						{
							break;
						}
					}
				}
				else
				{
					while (GenericArraySortHelper<TKey, TValue>.GreaterThan(ref tkey, keys[++i]))
					{
					}
					while (GenericArraySortHelper<TKey, TValue>.LessThan(ref tkey, keys[--j]))
					{
					}
				}
				if (i >= j)
				{
					break;
				}
				GenericArraySortHelper<TKey, TValue>.Swap(keys, values, i, j);
			}
			if (i != num - 1)
			{
				GenericArraySortHelper<TKey, TValue>.Swap(keys, values, i, num - 1);
			}
			return i;
		}

		// Token: 0x0600607B RID: 24699 RVA: 0x001CDE54 File Offset: 0x001CD054
		private static void HeapSort(Span<TKey> keys, Span<TValue> values)
		{
			int length = keys.Length;
			for (int i = length >> 1; i >= 1; i--)
			{
				GenericArraySortHelper<TKey, TValue>.DownHeap(keys, values, i, length, 0);
			}
			for (int j = length; j > 1; j--)
			{
				GenericArraySortHelper<TKey, TValue>.Swap(keys, values, 0, j - 1);
				GenericArraySortHelper<TKey, TValue>.DownHeap(keys, values, 1, j - 1, 0);
			}
		}

		// Token: 0x0600607C RID: 24700 RVA: 0x001CDEA4 File Offset: 0x001CD0A4
		private unsafe static void DownHeap(Span<TKey> keys, Span<TValue> values, int i, int n, int lo)
		{
			TKey tkey = *keys[lo + i - 1];
			TValue tvalue = *values[lo + i - 1];
			while (i <= n >> 1)
			{
				int num = 2 * i;
				if (num < n && (*keys[lo + num - 1] == null || GenericArraySortHelper<TKey, TValue>.LessThan(keys[lo + num - 1], keys[lo + num])))
				{
					num++;
				}
				if (*keys[lo + num - 1] == null || !GenericArraySortHelper<TKey, TValue>.LessThan(ref tkey, keys[lo + num - 1]))
				{
					break;
				}
				*keys[lo + i - 1] = *keys[lo + num - 1];
				*values[lo + i - 1] = *values[lo + num - 1];
				i = num;
			}
			*keys[lo + i - 1] = tkey;
			*values[lo + i - 1] = tvalue;
		}

		// Token: 0x0600607D RID: 24701 RVA: 0x001CDFCC File Offset: 0x001CD1CC
		private unsafe static void InsertionSort(Span<TKey> keys, Span<TValue> values)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
				TKey tkey = *keys[i + 1];
				TValue tvalue = *values[i + 1];
				int num = i;
				while (num >= 0 && (tkey == null || GenericArraySortHelper<TKey, TValue>.LessThan(ref tkey, keys[num])))
				{
					*keys[num + 1] = *keys[num];
					*values[num + 1] = *values[num];
					num--;
				}
				*keys[num + 1] = tkey;
				*values[num + 1] = tvalue;
			}
		}

		// Token: 0x0600607E RID: 24702 RVA: 0x001CCE30 File Offset: 0x001CC030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool LessThan(ref TKey left, ref TKey right)
		{
			if (typeof(TKey) == typeof(byte))
			{
				return (byte)((object)left) < (byte)((object)right);
			}
			if (typeof(TKey) == typeof(sbyte))
			{
				return (sbyte)((object)left) < (sbyte)((object)right);
			}
			if (typeof(TKey) == typeof(ushort))
			{
				return (ushort)((object)left) < (ushort)((object)right);
			}
			if (typeof(TKey) == typeof(short))
			{
				return (short)((object)left) < (short)((object)right);
			}
			if (typeof(TKey) == typeof(uint))
			{
				return (uint)((object)left) < (uint)((object)right);
			}
			if (typeof(TKey) == typeof(int))
			{
				return (int)((object)left) < (int)((object)right);
			}
			if (typeof(TKey) == typeof(ulong))
			{
				return (ulong)((object)left) < (ulong)((object)right);
			}
			if (typeof(TKey) == typeof(long))
			{
				return (long)((object)left) < (long)((object)right);
			}
			if (typeof(TKey) == typeof(UIntPtr))
			{
				return (UIntPtr)((object)left) < (UIntPtr)((object)right);
			}
			if (typeof(TKey) == typeof(IntPtr))
			{
				return (IntPtr)((object)left) < (IntPtr)((object)right);
			}
			if (typeof(TKey) == typeof(float))
			{
				return (float)((object)left) < (float)((object)right);
			}
			if (typeof(TKey) == typeof(double))
			{
				return (double)((object)left) < (double)((object)right);
			}
			if (typeof(TKey) == typeof(Half))
			{
				return (Half)((object)left) < (Half)((object)right);
			}
			return left.CompareTo(right) < 0;
		}

		// Token: 0x0600607F RID: 24703 RVA: 0x001CD1A8 File Offset: 0x001CC3A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool GreaterThan(ref TKey left, ref TKey right)
		{
			if (typeof(TKey) == typeof(byte))
			{
				return (byte)((object)left) > (byte)((object)right);
			}
			if (typeof(TKey) == typeof(sbyte))
			{
				return (sbyte)((object)left) > (sbyte)((object)right);
			}
			if (typeof(TKey) == typeof(ushort))
			{
				return (ushort)((object)left) > (ushort)((object)right);
			}
			if (typeof(TKey) == typeof(short))
			{
				return (short)((object)left) > (short)((object)right);
			}
			if (typeof(TKey) == typeof(uint))
			{
				return (uint)((object)left) > (uint)((object)right);
			}
			if (typeof(TKey) == typeof(int))
			{
				return (int)((object)left) > (int)((object)right);
			}
			if (typeof(TKey) == typeof(ulong))
			{
				return (ulong)((object)left) > (ulong)((object)right);
			}
			if (typeof(TKey) == typeof(long))
			{
				return (long)((object)left) > (long)((object)right);
			}
			if (typeof(TKey) == typeof(UIntPtr))
			{
				return (UIntPtr)((object)left) > (UIntPtr)((object)right);
			}
			if (typeof(TKey) == typeof(IntPtr))
			{
				return (IntPtr)((object)left) > (IntPtr)((object)right);
			}
			if (typeof(TKey) == typeof(float))
			{
				return (float)((object)left) > (float)((object)right);
			}
			if (typeof(TKey) == typeof(double))
			{
				return (double)((object)left) > (double)((object)right);
			}
			if (typeof(TKey) == typeof(Half))
			{
				return (Half)((object)left) > (Half)((object)right);
			}
			return left.CompareTo(right) > 0;
		}
	}
}
