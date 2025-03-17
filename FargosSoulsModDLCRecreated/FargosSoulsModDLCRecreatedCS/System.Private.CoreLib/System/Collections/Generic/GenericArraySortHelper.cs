using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System.Collections.Generic
{
	// Token: 0x020007D1 RID: 2001
	internal class GenericArraySortHelper<T> : IArraySortHelper<T> where T : IComparable<T>
	{
		// Token: 0x0600605B RID: 24667 RVA: 0x001CC84C File Offset: 0x001CBA4C
		public void Sort(Span<T> keys, IComparer<T> comparer)
		{
			try
			{
				if (comparer == null || comparer == Comparer<T>.Default)
				{
					if (keys.Length > 1)
					{
						if (typeof(T) == typeof(double) || typeof(T) == typeof(float) || typeof(T) == typeof(Half))
						{
							int num = SortUtils.MoveNansToFront<T, byte>(keys, default(Span<byte>));
							if (num == keys.Length)
							{
								return;
							}
							keys = keys.Slice(num);
						}
						GenericArraySortHelper<T>.IntroSort(keys, 2 * (BitOperations.Log2((uint)keys.Length) + 1));
					}
				}
				else
				{
					ArraySortHelper<T>.IntrospectiveSort(keys, new Comparison<T>(comparer.Compare));
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

		// Token: 0x0600605C RID: 24668 RVA: 0x001CC948 File Offset: 0x001CBB48
		public int BinarySearch(T[] array, int index, int length, T value, IComparer<T> comparer)
		{
			int result;
			try
			{
				if (comparer == null || comparer == Comparer<T>.Default)
				{
					result = GenericArraySortHelper<T>.BinarySearch(array, index, length, value);
				}
				else
				{
					result = ArraySortHelper<T>.InternalBinarySearch(array, index, length, value, comparer);
				}
			}
			catch (Exception e)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.InvalidOperation_IComparerFailed, e);
				result = 0;
			}
			return result;
		}

		// Token: 0x0600605D RID: 24669 RVA: 0x001CC99C File Offset: 0x001CBB9C
		private static int BinarySearch(T[] array, int index, int length, T value)
		{
			int i = index;
			int num = index + length - 1;
			while (i <= num)
			{
				int num2 = i + (num - i >> 1);
				int num3;
				if (array[num2] == null)
				{
					num3 = ((value == null) ? 0 : -1);
				}
				else
				{
					num3 = array[num2].CompareTo(value);
				}
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

		// Token: 0x0600605E RID: 24670 RVA: 0x001CCA07 File Offset: 0x001CBC07
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void SwapIfGreater(ref T i, ref T j)
		{
			if (i != null && GenericArraySortHelper<T>.GreaterThan(ref i, ref j))
			{
				GenericArraySortHelper<T>.Swap(ref i, ref j);
			}
		}

		// Token: 0x0600605F RID: 24671 RVA: 0x001CCA28 File Offset: 0x001CBC28
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Swap(ref T i, ref T j)
		{
			T t = i;
			i = j;
			j = t;
		}

		// Token: 0x06006060 RID: 24672 RVA: 0x001CCA50 File Offset: 0x001CBC50
		private static void IntroSort(Span<T> keys, int depthLimit)
		{
			int i = keys.Length;
			while (i > 1)
			{
				if (i <= 16)
				{
					if (i == 2)
					{
						GenericArraySortHelper<T>.SwapIfGreater(keys[0], keys[1]);
						return;
					}
					if (i == 3)
					{
						ref T j = ref keys[2];
						ref T ptr = ref keys[1];
						ref T i2 = ref keys[0];
						GenericArraySortHelper<T>.SwapIfGreater(ref i2, ref ptr);
						GenericArraySortHelper<T>.SwapIfGreater(ref i2, ref j);
						GenericArraySortHelper<T>.SwapIfGreater(ref ptr, ref j);
						return;
					}
					GenericArraySortHelper<T>.InsertionSort(keys.Slice(0, i));
					return;
				}
				else
				{
					if (depthLimit == 0)
					{
						GenericArraySortHelper<T>.HeapSort(keys.Slice(0, i));
						return;
					}
					depthLimit--;
					int num = GenericArraySortHelper<T>.PickPivotAndPartition(keys.Slice(0, i));
					Span<T> span = keys;
					int num2 = num + 1;
					int length = i - num2;
					GenericArraySortHelper<T>.IntroSort(span.Slice(num2, length), depthLimit);
					i = num;
				}
			}
		}

		// Token: 0x06006061 RID: 24673 RVA: 0x001CCB20 File Offset: 0x001CBD20
		private unsafe static int PickPivotAndPartition(Span<T> keys)
		{
			ref T reference = ref MemoryMarshal.GetReference<T>(keys);
			ref T j = ref Unsafe.Add<T>(ref reference, keys.Length - 1);
			ref T ptr = ref Unsafe.Add<T>(ref reference, keys.Length - 1 >> 1);
			GenericArraySortHelper<T>.SwapIfGreater(ref reference, ref ptr);
			GenericArraySortHelper<T>.SwapIfGreater(ref reference, ref j);
			GenericArraySortHelper<T>.SwapIfGreater(ref ptr, ref j);
			ref T ptr2 = ref Unsafe.Add<T>(ref reference, keys.Length - 2);
			T t = ptr;
			GenericArraySortHelper<T>.Swap(ref ptr, ref ptr2);
			ref T ptr3 = ref reference;
			ref T ptr4 = ref ptr2;
			while (Unsafe.IsAddressLessThan<T>(ref ptr3, ref ptr4))
			{
				if (t == null)
				{
					while (Unsafe.IsAddressLessThan<T>(ref ptr3, ref ptr2) && *(ptr3 = Unsafe.Add<T>(ref ptr3, 1)) == null)
					{
					}
					while (Unsafe.IsAddressGreaterThan<T>(ref ptr4, ref reference))
					{
						if (*(ptr4 = Unsafe.Add<T>(ref ptr4, -1)) == null)
						{
							break;
						}
					}
				}
				else
				{
					while (Unsafe.IsAddressLessThan<T>(ref ptr3, ref ptr2) && GenericArraySortHelper<T>.GreaterThan(ref t, ptr3 = Unsafe.Add<T>(ref ptr3, 1)))
					{
					}
					while (Unsafe.IsAddressGreaterThan<T>(ref ptr4, ref reference) && GenericArraySortHelper<T>.LessThan(ref t, ptr4 = Unsafe.Add<T>(ref ptr4, -1)))
					{
					}
				}
				if (!Unsafe.IsAddressLessThan<T>(ref ptr3, ref ptr4))
				{
					break;
				}
				GenericArraySortHelper<T>.Swap(ref ptr3, ref ptr4);
			}
			if (!Unsafe.AreSame<T>(ref ptr3, ref ptr2))
			{
				GenericArraySortHelper<T>.Swap(ref ptr3, ref ptr2);
			}
			return (int)(Unsafe.ByteOffset<T>(ref reference, ref ptr3) / (IntPtr)Unsafe.SizeOf<T>());
		}

		// Token: 0x06006062 RID: 24674 RVA: 0x001CCC64 File Offset: 0x001CBE64
		private static void HeapSort(Span<T> keys)
		{
			int length = keys.Length;
			for (int i = length >> 1; i >= 1; i--)
			{
				GenericArraySortHelper<T>.DownHeap(keys, i, length, 0);
			}
			for (int j = length; j > 1; j--)
			{
				GenericArraySortHelper<T>.Swap(keys[0], keys[j - 1]);
				GenericArraySortHelper<T>.DownHeap(keys, 1, j - 1, 0);
			}
		}

		// Token: 0x06006063 RID: 24675 RVA: 0x001CCCC0 File Offset: 0x001CBEC0
		private unsafe static void DownHeap(Span<T> keys, int i, int n, int lo)
		{
			T t = *keys[lo + i - 1];
			while (i <= n >> 1)
			{
				int num = 2 * i;
				if (num < n && (*keys[lo + num - 1] == null || GenericArraySortHelper<T>.LessThan(keys[lo + num - 1], keys[lo + num])))
				{
					num++;
				}
				if (*keys[lo + num - 1] == null || !GenericArraySortHelper<T>.LessThan(ref t, keys[lo + num - 1]))
				{
					break;
				}
				*keys[lo + i - 1] = *keys[lo + num - 1];
				i = num;
			}
			*keys[lo + i - 1] = t;
		}

		// Token: 0x06006064 RID: 24676 RVA: 0x001CCD94 File Offset: 0x001CBF94
		private unsafe static void InsertionSort(Span<T> keys)
		{
			for (int i = 0; i < keys.Length - 1; i++)
			{
				T t = *Unsafe.Add<T>(MemoryMarshal.GetReference<T>(keys), i + 1);
				int num = i;
				while (num >= 0 && (t == null || GenericArraySortHelper<T>.LessThan(ref t, Unsafe.Add<T>(MemoryMarshal.GetReference<T>(keys), num))))
				{
					*Unsafe.Add<T>(MemoryMarshal.GetReference<T>(keys), num + 1) = *Unsafe.Add<T>(MemoryMarshal.GetReference<T>(keys), num);
					num--;
				}
				*Unsafe.Add<T>(MemoryMarshal.GetReference<T>(keys), num + 1) = t;
			}
		}

		// Token: 0x06006065 RID: 24677 RVA: 0x001CCE30 File Offset: 0x001CC030
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool LessThan(ref T left, ref T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (byte)((object)left) < (byte)((object)right);
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (sbyte)((object)left) < (sbyte)((object)right);
			}
			if (typeof(T) == typeof(ushort))
			{
				return (ushort)((object)left) < (ushort)((object)right);
			}
			if (typeof(T) == typeof(short))
			{
				return (short)((object)left) < (short)((object)right);
			}
			if (typeof(T) == typeof(uint))
			{
				return (uint)((object)left) < (uint)((object)right);
			}
			if (typeof(T) == typeof(int))
			{
				return (int)((object)left) < (int)((object)right);
			}
			if (typeof(T) == typeof(ulong))
			{
				return (ulong)((object)left) < (ulong)((object)right);
			}
			if (typeof(T) == typeof(long))
			{
				return (long)((object)left) < (long)((object)right);
			}
			if (typeof(T) == typeof(UIntPtr))
			{
				return (UIntPtr)((object)left) < (UIntPtr)((object)right);
			}
			if (typeof(T) == typeof(IntPtr))
			{
				return (IntPtr)((object)left) < (IntPtr)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) < (float)((object)right);
			}
			if (typeof(T) == typeof(double))
			{
				return (double)((object)left) < (double)((object)right);
			}
			if (typeof(T) == typeof(Half))
			{
				return (Half)((object)left) < (Half)((object)right);
			}
			return left.CompareTo(right) < 0;
		}

		// Token: 0x06006066 RID: 24678 RVA: 0x001CD1A8 File Offset: 0x001CC3A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool GreaterThan(ref T left, ref T right)
		{
			if (typeof(T) == typeof(byte))
			{
				return (byte)((object)left) > (byte)((object)right);
			}
			if (typeof(T) == typeof(sbyte))
			{
				return (sbyte)((object)left) > (sbyte)((object)right);
			}
			if (typeof(T) == typeof(ushort))
			{
				return (ushort)((object)left) > (ushort)((object)right);
			}
			if (typeof(T) == typeof(short))
			{
				return (short)((object)left) > (short)((object)right);
			}
			if (typeof(T) == typeof(uint))
			{
				return (uint)((object)left) > (uint)((object)right);
			}
			if (typeof(T) == typeof(int))
			{
				return (int)((object)left) > (int)((object)right);
			}
			if (typeof(T) == typeof(ulong))
			{
				return (ulong)((object)left) > (ulong)((object)right);
			}
			if (typeof(T) == typeof(long))
			{
				return (long)((object)left) > (long)((object)right);
			}
			if (typeof(T) == typeof(UIntPtr))
			{
				return (UIntPtr)((object)left) > (UIntPtr)((object)right);
			}
			if (typeof(T) == typeof(IntPtr))
			{
				return (IntPtr)((object)left) > (IntPtr)((object)right);
			}
			if (typeof(T) == typeof(float))
			{
				return (float)((object)left) > (float)((object)right);
			}
			if (typeof(T) == typeof(double))
			{
				return (double)((object)left) > (double)((object)right);
			}
			if (typeof(T) == typeof(Half))
			{
				return (Half)((object)left) > (Half)((object)right);
			}
			return left.CompareTo(right) > 0;
		}
	}
}
