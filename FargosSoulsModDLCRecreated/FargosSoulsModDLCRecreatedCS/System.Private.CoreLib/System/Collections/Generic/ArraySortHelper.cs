using System;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007D0 RID: 2000
	[TypeDependency("System.Collections.Generic.GenericArraySortHelper`1")]
	internal class ArraySortHelper<T> : IArraySortHelper<T>
	{
		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x0600604B RID: 24651 RVA: 0x001CC2EC File Offset: 0x001CB4EC
		public static IArraySortHelper<T> Default
		{
			get
			{
				return ArraySortHelper<T>.s_defaultArraySortHelper;
			}
		}

		// Token: 0x0600604C RID: 24652 RVA: 0x001CC2F4 File Offset: 0x001CB4F4
		[DynamicDependency("#ctor", typeof(GenericArraySortHelper<>))]
		private static IArraySortHelper<T> CreateArraySortHelper()
		{
			IArraySortHelper<T> result;
			if (typeof(IComparable<T>).IsAssignableFrom(typeof(T)))
			{
				result = (IArraySortHelper<T>)RuntimeTypeHandle.Allocate(typeof(GenericArraySortHelper<string>).TypeHandle.Instantiate(new Type[]
				{
					typeof(T)
				}));
			}
			else
			{
				result = new ArraySortHelper<T>();
			}
			return result;
		}

		// Token: 0x0600604D RID: 24653 RVA: 0x001CC35C File Offset: 0x001CB55C
		public void Sort(Span<T> keys, IComparer<T> comparer)
		{
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				ArraySortHelper<T>.IntrospectiveSort(keys, new Comparison<T>(comparer.Compare));
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

		// Token: 0x0600604E RID: 24654 RVA: 0x001CC3B8 File Offset: 0x001CB5B8
		public int BinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int result;
			try
			{
				if (comparer == null)
				{
					comparer = Comparer<T>.Default;
				}
				result = ArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
			}
			catch (Exception e)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, e);
				result = 0;
			}
			return result;
		}

		// Token: 0x0600604F RID: 24655 RVA: 0x001CC400 File Offset: 0x001CB600
		internal static void Sort(Span<T> keys, Comparison<T> comparer)
		{
			try
			{
				ArraySortHelper<T>.IntrospectiveSort(keys, comparer);
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

		// Token: 0x06006050 RID: 24656 RVA: 0x001CC448 File Offset: 0x001CB648
		internal static int InternalBinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int i = index;
			int num = index + length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3 = comparer.Compare(array[num2], value);
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 < 0)
				{
					i = num2 + 1;
				}
				else
				{
					num = num2 - 1;
				}
			}
			return ~i;
		}

		// Token: 0x06006051 RID: 24657 RVA: 0x001CC490 File Offset: 0x001CB690
		private unsafe static void SwapIfGreater(Span<T> keys, Comparison<T> comparer, int i, int j)
		{
			if (comparer(*keys[i], *keys[j]) > 0)
			{
				T t = *keys[i];
				*keys[i] = *keys[j];
				*keys[j] = t;
			}
		}

		// Token: 0x06006052 RID: 24658 RVA: 0x001CC4F8 File Offset: 0x001CB6F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void Swap(Span<T> a, int i, int j)
		{
			T t = *a[i];
			*a[i] = *a[j];
			*a[j] = t;
		}

		// Token: 0x06006053 RID: 24659 RVA: 0x001CC53B File Offset: 0x001CB73B
		internal static void IntrospectiveSort(Span<T> keys, Comparison<T> comparer)
		{
			if (keys.Length > 1)
			{
				ArraySortHelper<T>.IntroSort(keys, 2 * (BitOperations.Log2((uint)keys.Length) + 1), comparer);
			}
		}

		// Token: 0x06006054 RID: 24660 RVA: 0x001CC560 File Offset: 0x001CB760
		private static void IntroSort(Span<T> keys, int depthLimit, Comparison<T> comparer)
		{
			int i = keys.Length;
			while (i > 1)
			{
				if (i <= 16)
				{
					if (i == 2)
					{
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, 1);
						return;
					}
					if (i == 3)
					{
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, 1);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, 2);
						ArraySortHelper<T>.SwapIfGreater(keys, comparer, 1, 2);
						return;
					}
					ArraySortHelper<T>.InsertionSort(keys.Slice(0, i), comparer);
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						ArraySortHelper<T>.HeapSort(keys.Slice(0, i), comparer);
						return;
					}
					depthLimit--;
					int num = ArraySortHelper<T>.PickPivotAndPartition(keys.Slice(0, i), comparer);
					Span<T> span = keys;
					int num2 = num + 1;
					int length = i - num2;
					ArraySortHelper<T>.IntroSort(span.Slice(num2, length), depthLimit, comparer);
					i = num;
				}
			}
		}

		// Token: 0x06006055 RID: 24661 RVA: 0x001CC60C File Offset: 0x001CB80C
		private unsafe static int PickPivotAndPartition(Span<T> keys, Comparison<T> comparer)
		{
			int num = keys.Length - 1;
			int num2 = num >> 1;
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, num2);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, 0, num);
			ArraySortHelper<T>.SwapIfGreater(keys, comparer, num2, num);
			T t = *keys[num2];
			ArraySortHelper<T>.Swap(keys, num2, num - 1);
			int i = 0;
			int num3 = num - 1;
			while (i < num3)
			{
				while (comparer(*keys[++i], t) < 0)
				{
				}
				while (comparer(t, *keys[--num3]) < 0)
				{
				}
				if (i >= num3)
				{
					break;
				}
				ArraySortHelper<T>.Swap(keys, i, num3);
			}
			if (i != num - 1)
			{
				ArraySortHelper<T>.Swap(keys, i, num - 1);
			}
			return i;
		}

		// Token: 0x06006056 RID: 24662 RVA: 0x001CC6C0 File Offset: 0x001CB8C0
		private static void HeapSort(Span<T> keys, Comparison<T> comparer)
		{
			int length = keys.Length;
			for (int i = length >> 1; i >= 1; i--)
			{
				ArraySortHelper<T>.DownHeap(keys, i, length, 0, comparer);
			}
			for (int j = length; j > 1; j--)
			{
				ArraySortHelper<T>.Swap(keys, 0, j - 1);
				ArraySortHelper<T>.DownHeap(keys, 1, j - 1, 0, comparer);
			}
		}

		// Token: 0x06006057 RID: 24663 RVA: 0x001CC710 File Offset: 0x001CB910
		private unsafe static void DownHeap(Span<T> keys, int i, int n, int lo, Comparison<T> comparer)
		{
			T t = *keys[lo + i - 1];
			while (i <= n >> 1)
			{
				int num = 2 * i;
				if (num < n && comparer(*keys[lo + num - 1], *keys[lo + num]) < 0)
				{
					num++;
				}
				if (comparer(t, *keys[lo + num - 1]) >= 0)
				{
					break;
				}
				*keys[lo + i - 1] = *keys[lo + num - 1];
				i = num;
			}
			*keys[lo + i - 1] = t;
		}

		// Token: 0x06006058 RID: 24664 RVA: 0x001CC7C0 File Offset: 0x001CB9C0
		private unsafe static void InsertionSort(Span<T> keys, Comparison<T> comparer)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
				T t = *keys[i + 1];
				int num = i;
				while (num >= 0 && comparer(t, *keys[num]) < 0)
				{
					*keys[num + 1] = *keys[num];
					num--;
				}
				*keys[num + 1] = t;
			}
		}

		// Token: 0x04001D00 RID: 7424
		private static readonly IArraySortHelper<T> s_defaultArraySortHelper = ArraySortHelper<T>.CreateArraySortHelper();
	}
}
