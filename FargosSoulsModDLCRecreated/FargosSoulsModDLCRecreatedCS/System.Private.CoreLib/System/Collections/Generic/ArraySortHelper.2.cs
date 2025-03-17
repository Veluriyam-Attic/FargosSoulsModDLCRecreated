using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007D3 RID: 2003
	[TypeDependency("System.Collections.Generic.GenericArraySortHelper`2")]
	internal class ArraySortHelper<TKey, TValue> : IArraySortHelper<TKey, TValue>
	{
		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06006069 RID: 24681 RVA: 0x001CD51F File Offset: 0x001CC71F
		public static IArraySortHelper<TKey, TValue> Default
		{
			get
			{
				return ArraySortHelper<TKey, TValue>.s_defaultArraySortHelper;
			}
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x001CD528 File Offset: 0x001CC728
		[DynamicDependency("#ctor", typeof(GenericArraySortHelper<, >))]
		private static IArraySortHelper<TKey, TValue> CreateArraySortHelper()
		{
			IArraySortHelper<TKey, TValue> result;
			if (typeof(IComparable<TKey>).IsAssignableFrom(typeof(TKey)))
			{
				result = (IArraySortHelper<TKey, TValue>)RuntimeTypeHandle.Allocate(typeof(GenericArraySortHelper<string, string>).TypeHandle.Instantiate(new Type[]
				{
					typeof(TKey),
					typeof(TValue)
				}));
			}
			else
			{
				result = new ArraySortHelper<TKey, TValue>();
			}
			return result;
		}

		// Token: 0x0600606B RID: 24683 RVA: 0x001CD59C File Offset: 0x001CC79C
		public void Sort(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
		{
			try
			{
				ArraySortHelper<TKey, TValue>.IntrospectiveSort(keys, values, comparer ?? Comparer<TKey>.Default);
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

		// Token: 0x0600606C RID: 24684 RVA: 0x001CD5EC File Offset: 0x001CC7EC
		private unsafe static void SwapIfGreaterWithValues(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer, int i, int j)
		{
			if (comparer.Compare(*keys[i], *keys[j]) > 0)
			{
				TKey tkey = *keys[i];
				*keys[i] = *keys[j];
				*keys[j] = tkey;
				TValue tvalue = *values[i];
				*values[i] = *values[j];
				*values[j] = tvalue;
			}
		}

		// Token: 0x0600606D RID: 24685 RVA: 0x001CD690 File Offset: 0x001CC890
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

		// Token: 0x0600606E RID: 24686 RVA: 0x001CD709 File Offset: 0x001CC909
		internal static void IntrospectiveSort(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
		{
			if (keys.Length > 1)
			{
				ArraySortHelper<TKey, TValue>.IntroSort(keys, values, 2 * (BitOperations.Log2((uint)keys.Length) + 1), comparer);
			}
		}

		// Token: 0x0600606F RID: 24687 RVA: 0x001CD730 File Offset: 0x001CC930
		private static void IntroSort(Span<TKey> keys, Span<TValue> values, int depthLimit, IComparer<TKey> comparer)
		{
			int i = keys.Length;
			while (i > 1)
			{
				if (i <= 16)
				{
					if (i == 2)
					{
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, 1);
						return;
					}
					if (i == 3)
					{
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, 1);
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, 2);
						ArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 1, 2);
						return;
					}
					ArraySortHelper<TKey, TValue>.InsertionSort(keys.Slice(0, i), values.Slice(0, i), comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						ArraySortHelper<TKey, TValue>.HeapSort(keys.Slice(0, i), values.Slice(0, i), comparer);
						return;
					}
					depthLimit--;
					int num = ArraySortHelper<TKey, TValue>.PickPivotAndPartition(keys.Slice(0, i), values.Slice(0, i), comparer);
					Span<TKey> span = keys;
					int num2 = num + 1;
					int num3 = i - num2;
					Span<TKey> keys2 = span.Slice(num2, num3);
					Span<TValue> span2 = values;
					num3 = num + 1;
					num2 = i - num3;
					ArraySortHelper<TKey, TValue>.IntroSort(keys2, span2.Slice(num3, num2), depthLimit, comparer);
					i = num;
				}
			}
		}

		// Token: 0x06006070 RID: 24688 RVA: 0x001CD810 File Offset: 0x001CCA10
		private unsafe static int PickPivotAndPartition(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
		{
			int num = keys.Length - 1;
			int num2 = num >> 1;
			ArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, num2);
			ArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, 0, num);
			ArraySortHelper<TKey, TValue>.SwapIfGreaterWithValues(keys, values, comparer, num2, num);
			TKey tkey = *keys[num2];
			ArraySortHelper<TKey, TValue>.Swap(keys, values, num2, num - 1);
			int i = 0;
			int num3 = num - 1;
			while (i < num3)
			{
				while (comparer.Compare(*keys[++i], tkey) < 0)
				{
				}
				while (comparer.Compare(tkey, *keys[--num3]) < 0)
				{
				}
				if (i >= num3)
				{
					break;
				}
				ArraySortHelper<TKey, TValue>.Swap(keys, values, i, num3);
			}
			if (i != num - 1)
			{
				ArraySortHelper<TKey, TValue>.Swap(keys, values, i, num - 1);
			}
			return i;
		}

		// Token: 0x06006071 RID: 24689 RVA: 0x001CD8CC File Offset: 0x001CCACC
		private static void HeapSort(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
		{
			int length = keys.Length;
			for (int i = length >> 1; i >= 1; i--)
			{
				ArraySortHelper<TKey, TValue>.DownHeap(keys, values, i, length, 0, comparer);
			}
			for (int j = length; j > 1; j--)
			{
				ArraySortHelper<TKey, TValue>.Swap(keys, values, 0, j - 1);
				ArraySortHelper<TKey, TValue>.DownHeap(keys, values, 1, j - 1, 0, comparer);
			}
		}

		// Token: 0x06006072 RID: 24690 RVA: 0x001CD920 File Offset: 0x001CCB20
		private unsafe static void DownHeap(Span<TKey> keys, Span<TValue> values, int i, int n, int lo, IComparer<TKey> comparer)
		{
			TKey tkey = *keys[lo + i - 1];
			TValue tvalue = *values[lo + i - 1];
			while (i <= n >> 1)
			{
				int num = 2 * i;
				if (num < n && comparer.Compare(*keys[lo + num - 1], *keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer.Compare(tkey, *keys[lo + num - 1]) >= 0)
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

		// Token: 0x06006073 RID: 24691 RVA: 0x001CDA28 File Offset: 0x001CCC28
		private unsafe static void InsertionSort(Span<TKey> keys, Span<TValue> values, IComparer<TKey> comparer)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
				TKey tkey = *keys[i + 1];
				TValue tvalue = *values[i + 1];
				int num = i;
				while (num >= 0 && comparer.Compare(tkey, *keys[num]) < 0)
				{
					*keys[num + 1] = *keys[num];
					*values[num + 1] = *values[num];
					num--;
				}
				*keys[num + 1] = tkey;
				*values[num + 1] = tvalue;
			}
		}

		// Token: 0x04001D01 RID: 7425
		private static readonly IArraySortHelper<TKey, TValue> s_defaultArraySortHelper = ArraySortHelper<TKey, TValue>.CreateArraySortHelper();
	}
}
