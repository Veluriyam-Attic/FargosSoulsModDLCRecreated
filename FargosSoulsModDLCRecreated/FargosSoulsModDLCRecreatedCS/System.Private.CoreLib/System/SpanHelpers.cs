using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000180 RID: 384
	internal static class SpanHelpers
	{
		// Token: 0x06001350 RID: 4944 RVA: 0x000E957F File Offset: 0x000E877F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<T, TComparable>(this ReadOnlySpan<T> span, TComparable comparable) where TComparable : IComparable<T>
		{
			if (comparable == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparable);
			}
			return SpanHelpers.BinarySearch<T, TComparable>(MemoryMarshal.GetReference<T>(span), span.Length, comparable);
		}

		// Token: 0x06001351 RID: 4945 RVA: 0x000E95A4 File Offset: 0x000E87A4
		public unsafe static int BinarySearch<T, TComparable>(ref T spanStart, int length, TComparable comparable) where TComparable : IComparable<T>
		{
			int i = 0;
			int num = length - 1;
			while (i <= num)
			{
				int num2 = (int)((uint)(num + i) >> 1);
				int num3 = comparable.CompareTo(*Unsafe.Add<T>(ref spanStart, num2));
				if (num3 == 0)
				{
					return num2;
				}
				if (num3 > 0)
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

		// Token: 0x06001352 RID: 4946 RVA: 0x000E95F4 File Offset: 0x000E87F4
		public static int IndexOf(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			byte value2 = value;
			ref byte second = ref Unsafe.Add<byte>(ref value, 1);
			int num = valueLength - 1;
			int i = searchSpaceLength - num;
			int num2 = 0;
			while (i > 0)
			{
				int num3 = SpanHelpers.IndexOf(Unsafe.Add<byte>(ref searchSpace, num2), value2, i);
				if (num3 == -1)
				{
					break;
				}
				i -= num3;
				num2 += num3;
				if (i <= 0)
				{
					break;
				}
				if (SpanHelpers.SequenceEqual(Unsafe.Add<byte>(ref searchSpace, num2 + 1), ref second, (UIntPtr)((IntPtr)num)))
				{
					return num2;
				}
				i--;
				num2++;
			}
			return -1;
		}

		// Token: 0x06001353 RID: 4947 RVA: 0x000E966C File Offset: 0x000E886C
		public unsafe static int IndexOfAny(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf(ref searchSpace, *Unsafe.Add<byte>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06001354 RID: 4948 RVA: 0x000E96AC File Offset: 0x000E88AC
		public unsafe static int LastIndexOfAny(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.LastIndexOf(ref searchSpace, *Unsafe.Add<byte>(ref value, i), searchSpaceLength);
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06001355 RID: 4949 RVA: 0x000E96E4 File Offset: 0x000E88E4
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static bool Contains(ref byte searchSpace, byte value, int length)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				uintPtr2 = SpanHelpers.UnalignedCountVector(ref searchSpace);
			}
			for (;;)
			{
				if (uintPtr2 < (UIntPtr)((IntPtr)8))
				{
					if (uintPtr2 >= (UIntPtr)((IntPtr)4))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)4);
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)0)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)))
						{
							return true;
						}
						uintPtr += (UIntPtr)((IntPtr)4);
					}
					while (uintPtr2 > (UIntPtr)((IntPtr)0))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)1);
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr))
						{
							return true;
						}
						uintPtr += (UIntPtr)((IntPtr)1);
					}
					if (!Vector.IsHardwareAccelerated || uintPtr >= (UIntPtr)length)
					{
						break;
					}
					uintPtr2 = ((UIntPtr)length - uintPtr & (UIntPtr)((IntPtr)(~(IntPtr)(Vector<byte>.Count - 1))));
					Vector<byte> left = new Vector<byte>(value);
					while (uintPtr2 > uintPtr)
					{
						Vector<byte> other = Vector.Equals<byte>(left, SpanHelpers.LoadVector(ref searchSpace, uintPtr));
						if (!Vector<byte>.Zero.Equals(other))
						{
							return true;
						}
						uintPtr += (UIntPtr)((IntPtr)Vector<byte>.Count);
					}
					if (uintPtr >= (UIntPtr)length)
					{
						break;
					}
					uintPtr2 = (UIntPtr)length - uintPtr;
				}
				else
				{
					uintPtr2 -= (UIntPtr)((IntPtr)8);
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)0)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)4)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)5)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)6)) || value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)7)))
					{
						return true;
					}
					uintPtr += (UIntPtr)((IntPtr)8);
				}
			}
			return false;
		}

		// Token: 0x06001356 RID: 4950 RVA: 0x000E988C File Offset: 0x000E8A8C
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int IndexOf(ref byte searchSpace, byte value, int length)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Sse2.IsSupported || AdvSimd.Arm64.IsSupported)
			{
				if (length >= Vector128<byte>.Count * 2)
				{
					uintPtr2 = SpanHelpers.UnalignedCountVector128(ref searchSpace);
				}
			}
			else if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				uintPtr2 = SpanHelpers.UnalignedCountVector(ref searchSpace);
			}
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			Vector<byte> vector;
			for (;;)
			{
				if (uintPtr2 < (UIntPtr)((IntPtr)8))
				{
					if (uintPtr2 >= (UIntPtr)((IntPtr)4))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)4);
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr))
						{
							goto IL_3B0;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)))
						{
							goto IL_3B3;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)))
						{
							goto IL_3B9;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)))
						{
							goto IL_3BF;
						}
						uintPtr += (UIntPtr)((IntPtr)4);
					}
					while (uintPtr2 > (UIntPtr)((IntPtr)0))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)1);
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr))
						{
							goto IL_3B0;
						}
						uintPtr += (UIntPtr)((IntPtr)1);
					}
					if (Avx2.IsSupported)
					{
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						if (((UIntPtr)Unsafe.AsPointer<byte>(ref searchSpace) + uintPtr & (UIntPtr)((IntPtr)(Vector256<byte>.Count - 1))) != 0)
						{
							Vector128<byte> left = Vector128.Create(value);
							Vector128<byte> right = SpanHelpers.LoadVector128(ref searchSpace, uintPtr);
							num = Sse2.MoveMask(Sse2.CompareEqual(left, right));
							if (num != 0)
							{
								break;
							}
							uintPtr += (UIntPtr)((IntPtr)Vector128<byte>.Count);
						}
						uintPtr2 = SpanHelpers.GetByteVector256SpanLength(uintPtr, length);
						if (uintPtr2 > uintPtr)
						{
							Vector256<byte> left2 = Vector256.Create(value);
							do
							{
								Vector256<byte> right2 = SpanHelpers.LoadVector256(ref searchSpace, uintPtr);
								num2 = Avx2.MoveMask(Avx2.CompareEqual(left2, right2));
								if (num2 != 0)
								{
									goto IL_1F5;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector256<byte>.Count);
							}
							while (uintPtr2 > uintPtr);
						}
						uintPtr2 = SpanHelpers.GetByteVector128SpanLength(uintPtr, length);
						if (uintPtr2 > uintPtr)
						{
							Vector128<byte> left3 = Vector128.Create(value);
							Vector128<byte> right3 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr);
							num3 = Sse2.MoveMask(Sse2.CompareEqual(left3, right3));
							if (num3 != 0)
							{
								goto IL_241;
							}
							uintPtr += (UIntPtr)((IntPtr)Vector128<byte>.Count);
						}
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = (UIntPtr)length - uintPtr;
					}
					else if (Sse2.IsSupported)
					{
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = SpanHelpers.GetByteVector128SpanLength(uintPtr, length);
						Vector128<byte> left4 = Vector128.Create(value);
						while (uintPtr2 > uintPtr)
						{
							Vector128<byte> right4 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr);
							num4 = Sse2.MoveMask(Sse2.CompareEqual(left4, right4));
							if (num4 != 0)
							{
								goto IL_2A8;
							}
							uintPtr += (UIntPtr)((IntPtr)Vector128<byte>.Count);
						}
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = (UIntPtr)length - uintPtr;
					}
					else if (AdvSimd.Arm64.IsSupported)
					{
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = SpanHelpers.GetByteVector128SpanLength(uintPtr, length);
						Vector128<byte> mask = Vector128.Create(4097).AsByte<ushort>();
						num5 = 0;
						Vector128<byte> left5 = Vector128.Create(value);
						while (uintPtr2 > uintPtr)
						{
							Vector128<byte> right5 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr);
							Vector128<byte> compareResult = AdvSimd.CompareEqual(left5, right5);
							if (SpanHelpers.TryFindFirstMatchedLane(mask, compareResult, ref num5))
							{
								goto IL_32B;
							}
							uintPtr += (UIntPtr)((IntPtr)Vector128<byte>.Count);
						}
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = (UIntPtr)length - uintPtr;
					}
					else
					{
						if (!Vector.IsHardwareAccelerated || uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = SpanHelpers.GetByteVectorSpanLength(uintPtr, length);
						Vector<byte> left6 = new Vector<byte>(value);
						while (uintPtr2 > uintPtr)
						{
							vector = Vector.Equals<byte>(left6, SpanHelpers.LoadVector(ref searchSpace, uintPtr));
							if (!Vector<byte>.Zero.Equals(vector))
							{
								goto IL_390;
							}
							uintPtr += (UIntPtr)((IntPtr)Vector<byte>.Count);
						}
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = (UIntPtr)length - uintPtr;
					}
				}
				else
				{
					uintPtr2 -= (UIntPtr)((IntPtr)8);
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr))
					{
						goto IL_3B0;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)))
					{
						goto IL_3B3;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)))
					{
						goto IL_3B9;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)))
					{
						goto IL_3BF;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)4)))
					{
						goto IL_3C5;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)5)))
					{
						goto IL_3CB;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)6)))
					{
						goto IL_3D1;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)7)))
					{
						goto IL_3D7;
					}
					uintPtr += (UIntPtr)((IntPtr)8);
				}
			}
			return (int)(uintPtr + (UIntPtr)BitOperations.TrailingZeroCount(num));
			IL_1F5:
			return (int)(uintPtr + (UIntPtr)BitOperations.TrailingZeroCount(num2));
			IL_241:
			return (int)(uintPtr + (UIntPtr)BitOperations.TrailingZeroCount(num3));
			IL_2A8:
			return (int)(uintPtr + (UIntPtr)BitOperations.TrailingZeroCount(num4));
			IL_32B:
			return (int)(uintPtr + (UIntPtr)num5);
			IL_390:
			return (int)uintPtr + SpanHelpers.LocateFirstFoundByte(vector);
			IL_3B0:
			return (int)uintPtr;
			IL_3B3:
			return (int)(uintPtr + (UIntPtr)((IntPtr)1));
			IL_3B9:
			return (int)(uintPtr + (UIntPtr)((IntPtr)2));
			IL_3BF:
			return (int)(uintPtr + (UIntPtr)((IntPtr)3));
			IL_3C5:
			return (int)(uintPtr + (UIntPtr)((IntPtr)4));
			IL_3CB:
			return (int)(uintPtr + (UIntPtr)((IntPtr)5));
			IL_3D1:
			return (int)(uintPtr + (UIntPtr)((IntPtr)6));
			IL_3D7:
			return (int)(uintPtr + (UIntPtr)((IntPtr)7));
		}

		// Token: 0x06001357 RID: 4951 RVA: 0x000E9C78 File Offset: 0x000E8E78
		public static int LastIndexOf(ref byte searchSpace, int searchSpaceLength, ref byte value, int valueLength)
		{
			if (valueLength == 0)
			{
				return searchSpaceLength;
			}
			byte value2 = value;
			ref byte second = ref Unsafe.Add<byte>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				num4 = SpanHelpers.LastIndexOf(ref searchSpace, value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				if (SpanHelpers.SequenceEqual(Unsafe.Add<byte>(ref searchSpace, num4 + 1), ref second, (UIntPtr)num))
				{
					break;
				}
				num2 += num3 - num4;
			}
			return num4;
		}

		// Token: 0x06001358 RID: 4952 RVA: 0x000E9CDC File Offset: 0x000E8EDC
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int LastIndexOf(ref byte searchSpace, byte value, int length)
		{
			UIntPtr uintPtr = (UIntPtr)length;
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				uintPtr2 = SpanHelpers.UnalignedCountVectorFromEnd(ref searchSpace, length);
			}
			Vector<byte> vector;
			for (;;)
			{
				if (uintPtr2 < (UIntPtr)((IntPtr)8))
				{
					if (uintPtr2 >= (UIntPtr)((IntPtr)4))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)4);
						uintPtr -= (UIntPtr)((IntPtr)4);
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)))
						{
							goto IL_1C9;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)))
						{
							goto IL_1C3;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)))
						{
							goto IL_1BD;
						}
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr))
						{
							break;
						}
					}
					while (uintPtr2 > (UIntPtr)((IntPtr)0))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)1);
						uintPtr -= (UIntPtr)((IntPtr)1);
						if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr))
						{
							goto IL_1BA;
						}
					}
					if (!Vector.IsHardwareAccelerated || uintPtr <= (UIntPtr)((IntPtr)0))
					{
						return -1;
					}
					uintPtr2 = (uintPtr & (UIntPtr)((IntPtr)(~(IntPtr)(Vector<byte>.Count - 1))));
					Vector<byte> left = new Vector<byte>(value);
					while (uintPtr2 > (UIntPtr)((IntPtr)(Vector<byte>.Count - 1)))
					{
						vector = Vector.Equals<byte>(left, SpanHelpers.LoadVector(ref searchSpace, uintPtr - (UIntPtr)((IntPtr)Vector<byte>.Count)));
						if (!Vector<byte>.Zero.Equals(vector))
						{
							goto IL_190;
						}
						uintPtr -= (UIntPtr)((IntPtr)Vector<byte>.Count);
						uintPtr2 -= (UIntPtr)((IntPtr)Vector<byte>.Count);
					}
					if (uintPtr <= (UIntPtr)((IntPtr)0))
					{
						return -1;
					}
					uintPtr2 = uintPtr;
				}
				else
				{
					uintPtr2 -= (UIntPtr)((IntPtr)8);
					uintPtr -= (UIntPtr)((IntPtr)8);
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)7)))
					{
						goto IL_1E1;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)6)))
					{
						goto IL_1DB;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)5)))
					{
						goto IL_1D5;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)4)))
					{
						goto IL_1CF;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)))
					{
						goto IL_1C9;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)))
					{
						goto IL_1C3;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)))
					{
						goto IL_1BD;
					}
					if (value == *Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr))
					{
						break;
					}
				}
			}
			goto IL_1BA;
			IL_190:
			return (int)uintPtr - Vector<byte>.Count + SpanHelpers.LocateLastFoundByte(vector);
			IL_1BA:
			return (int)uintPtr;
			IL_1BD:
			return (int)(uintPtr + (UIntPtr)((IntPtr)1));
			IL_1C3:
			return (int)(uintPtr + (UIntPtr)((IntPtr)2));
			IL_1C9:
			return (int)(uintPtr + (UIntPtr)((IntPtr)3));
			IL_1CF:
			return (int)(uintPtr + (UIntPtr)((IntPtr)4));
			IL_1D5:
			return (int)(uintPtr + (UIntPtr)((IntPtr)5));
			IL_1DB:
			return (int)(uintPtr + (UIntPtr)((IntPtr)6));
			IL_1E1:
			return (int)(uintPtr + (UIntPtr)((IntPtr)7));
		}

		// Token: 0x06001359 RID: 4953 RVA: 0x000E9ED0 File Offset: 0x000E90D0
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int IndexOfAny(ref byte searchSpace, byte value0, byte value1, int length)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Sse2.IsSupported || AdvSimd.Arm64.IsSupported)
			{
				IntPtr intPtr = (IntPtr)length - (IntPtr)Vector128<byte>.Count;
				if (intPtr >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr;
					goto IL_212;
				}
			}
			else if (Vector.IsHardwareAccelerated)
			{
				IntPtr intPtr2 = (IntPtr)length - (IntPtr)Vector<byte>.Count;
				if (intPtr2 >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr2;
					goto IL_212;
				}
			}
			while (uintPtr2 >= (UIntPtr)((IntPtr)8))
			{
				uintPtr2 -= (UIntPtr)((IntPtr)8);
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1E5;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_1E8:
					return (int)(uintPtr + (UIntPtr)((IntPtr)1));
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_1EE:
					return (int)(uintPtr + (UIntPtr)((IntPtr)2));
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					IL_1F4:
					return (int)(uintPtr + (UIntPtr)((IntPtr)3));
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)4)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)4));
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)5)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)5));
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)6)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)6));
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)7)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)7));
				}
				uintPtr += (UIntPtr)((IntPtr)8);
			}
			if (uintPtr2 >= (UIntPtr)((IntPtr)4))
			{
				uintPtr2 -= (UIntPtr)((IntPtr)4);
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1E5;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1E8;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1EE;
				}
				num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1F4;
				}
				uintPtr += (UIntPtr)((IntPtr)4);
			}
			while (uintPtr2 > (UIntPtr)((IntPtr)0))
			{
				uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
				if ((uint)value0 == num || (uint)value1 == num)
				{
					goto IL_1E5;
				}
				uintPtr += (UIntPtr)((IntPtr)1);
				uintPtr2 -= (UIntPtr)((IntPtr)1);
			}
			return -1;
			IL_1E5:
			return (int)uintPtr;
			IL_212:
			if (Sse2.IsSupported)
			{
				int num2;
				if (Avx2.IsSupported && uintPtr2 >= (UIntPtr)((IntPtr)Vector128<byte>.Count))
				{
					Vector256<byte> left = Vector256.Create(value0);
					Vector256<byte> left2 = Vector256.Create(value1);
					uintPtr2 -= (UIntPtr)((IntPtr)Vector128<byte>.Count);
					Vector256<byte> right;
					while (uintPtr2 > uintPtr)
					{
						right = SpanHelpers.LoadVector256(ref searchSpace, uintPtr);
						num2 = Avx2.MoveMask(Avx2.Or(Avx2.CompareEqual(left, right), Avx2.CompareEqual(left2, right)));
						if (num2 != 0)
						{
							goto IL_33C;
						}
						uintPtr += (UIntPtr)((IntPtr)Vector256<byte>.Count);
					}
					right = SpanHelpers.LoadVector256(ref searchSpace, uintPtr2);
					uintPtr = uintPtr2;
					num2 = Avx2.MoveMask(Avx2.Or(Avx2.CompareEqual(left, right), Avx2.CompareEqual(left2, right)));
					if (num2 == 0)
					{
						return -1;
					}
				}
				else
				{
					Vector128<byte> left3 = Vector128.Create(value0);
					Vector128<byte> left4 = Vector128.Create(value1);
					Vector128<byte> right2;
					while (uintPtr2 > uintPtr)
					{
						right2 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr);
						num2 = Sse2.MoveMask(Sse2.Or(Sse2.CompareEqual(left3, right2), Sse2.CompareEqual(left4, right2)).AsByte<byte>());
						if (num2 != 0)
						{
							goto IL_33C;
						}
						uintPtr += (UIntPtr)((IntPtr)Vector128<byte>.Count);
					}
					right2 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr2);
					uintPtr = uintPtr2;
					num2 = Sse2.MoveMask(Sse2.Or(Sse2.CompareEqual(left3, right2), Sse2.CompareEqual(left4, right2)));
					if (num2 == 0)
					{
						return -1;
					}
				}
				IL_33C:
				uintPtr += (UIntPtr)((IntPtr)BitOperations.TrailingZeroCount(num2));
				goto IL_1E5;
			}
			if (AdvSimd.Arm64.IsSupported)
			{
				Vector128<byte> mask = Vector128.Create(4097).AsByte<ushort>();
				int num3 = 0;
				Vector128<byte> left5 = Vector128.Create(value0);
				Vector128<byte> left6 = Vector128.Create(value1);
				Vector128<byte> right3;
				Vector128<byte> compareResult;
				while (uintPtr2 > uintPtr)
				{
					right3 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr);
					compareResult = AdvSimd.Or(AdvSimd.CompareEqual(left5, right3), AdvSimd.CompareEqual(left6, right3));
					if (SpanHelpers.TryFindFirstMatchedLane(mask, compareResult, ref num3))
					{
						uintPtr += (UIntPtr)num3;
						goto IL_1E5;
					}
					uintPtr += (UIntPtr)((IntPtr)Vector128<byte>.Count);
				}
				right3 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr2);
				uintPtr = uintPtr2;
				compareResult = AdvSimd.Or(AdvSimd.CompareEqual(left5, right3), AdvSimd.CompareEqual(left6, right3));
				if (SpanHelpers.TryFindFirstMatchedLane(mask, compareResult, ref num3))
				{
					uintPtr += (UIntPtr)num3;
					goto IL_1E5;
				}
				return -1;
			}
			else
			{
				if (!Vector.IsHardwareAccelerated)
				{
					goto IL_1E5;
				}
				Vector<byte> right4 = new Vector<byte>(value0);
				Vector<byte> right5 = new Vector<byte>(value1);
				Vector<byte> vector;
				while (uintPtr2 > uintPtr)
				{
					vector = SpanHelpers.LoadVector(ref searchSpace, uintPtr);
					vector = Vector.BitwiseOr<byte>(Vector.Equals<byte>(vector, right4), Vector.Equals<byte>(vector, right5));
					if (!Vector<byte>.Zero.Equals(vector))
					{
						IL_49A:
						uintPtr += (UIntPtr)((IntPtr)SpanHelpers.LocateFirstFoundByte(vector));
						goto IL_1E5;
					}
					uintPtr += (UIntPtr)((IntPtr)Vector<byte>.Count);
				}
				vector = SpanHelpers.LoadVector(ref searchSpace, uintPtr2);
				uintPtr = uintPtr2;
				vector = Vector.BitwiseOr<byte>(Vector.Equals<byte>(vector, right4), Vector.Equals<byte>(vector, right5));
				if (!Vector<byte>.Zero.Equals(vector))
				{
					goto IL_49A;
				}
				return -1;
			}
		}

		// Token: 0x0600135A RID: 4954 RVA: 0x000EA388 File Offset: 0x000E9588
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int IndexOfAny(ref byte searchSpace, byte value0, byte value1, byte value2, int length)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Sse2.IsSupported || AdvSimd.Arm64.IsSupported)
			{
				if (length >= Vector128<byte>.Count * 2)
				{
					uintPtr2 = SpanHelpers.UnalignedCountVector128(ref searchSpace);
				}
			}
			else if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				uintPtr2 = SpanHelpers.UnalignedCountVector(ref searchSpace);
			}
			int num2;
			int num3;
			int num4;
			int num5;
			Vector<byte> vector;
			for (;;)
			{
				if (uintPtr2 < (UIntPtr)((IntPtr)8))
				{
					if (uintPtr2 >= (UIntPtr)((IntPtr)4))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)4);
						uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
						if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
						{
							goto IL_5BA;
						}
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)));
						if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
						{
							goto IL_5BD;
						}
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)));
						if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
						{
							goto IL_5C3;
						}
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)));
						if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
						{
							goto IL_5C9;
						}
						uintPtr += (UIntPtr)((IntPtr)4);
					}
					while (uintPtr2 > (UIntPtr)((IntPtr)0))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)1);
						uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
						if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
						{
							goto IL_5BA;
						}
						uintPtr += (UIntPtr)((IntPtr)1);
					}
					if (Avx2.IsSupported)
					{
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = SpanHelpers.GetByteVector256SpanLength(uintPtr, length);
						if (uintPtr2 > uintPtr)
						{
							Vector256<byte> left = Vector256.Create(value0);
							Vector256<byte> left2 = Vector256.Create(value1);
							Vector256<byte> left3 = Vector256.Create(value2);
							do
							{
								Vector256<byte> right = SpanHelpers.LoadVector256(ref searchSpace, uintPtr);
								Vector256<byte> left4 = Avx2.CompareEqual(left, right);
								Vector256<byte> right2 = Avx2.CompareEqual(left2, right);
								Vector256<byte> right3 = Avx2.CompareEqual(left3, right);
								num2 = Avx2.MoveMask(Avx2.Or(Avx2.Or(left4, right2), right3));
								if (num2 != 0)
								{
									goto IL_2FC;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector256<byte>.Count);
							}
							while (uintPtr2 > uintPtr);
						}
						uintPtr2 = SpanHelpers.GetByteVector128SpanLength(uintPtr, length);
						if (uintPtr2 > uintPtr)
						{
							Vector128<byte> left5 = Vector128.Create(value0);
							Vector128<byte> left6 = Vector128.Create(value1);
							Vector128<byte> left7 = Vector128.Create(value2);
							Vector128<byte> right4 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr);
							Vector128<byte> left8 = Sse2.CompareEqual(left5, right4);
							Vector128<byte> right5 = Sse2.CompareEqual(left6, right4);
							Vector128<byte> right6 = Sse2.CompareEqual(left7, right4);
							num3 = Sse2.MoveMask(Sse2.Or(Sse2.Or(left8, right5), right6));
							if (num3 != 0)
							{
								goto IL_384;
							}
							uintPtr += (UIntPtr)((IntPtr)Vector128<byte>.Count);
						}
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = (UIntPtr)length - uintPtr;
					}
					else if (Sse2.IsSupported)
					{
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = SpanHelpers.GetByteVector128SpanLength(uintPtr, length);
						Vector128<byte> left9 = Vector128.Create(value0);
						Vector128<byte> left10 = Vector128.Create(value1);
						Vector128<byte> left11 = Vector128.Create(value2);
						while (uintPtr2 > uintPtr)
						{
							Vector128<byte> right7 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr);
							Vector128<byte> left12 = Sse2.CompareEqual(left9, right7);
							Vector128<byte> right8 = Sse2.CompareEqual(left10, right7);
							Vector128<byte> right9 = Sse2.CompareEqual(left11, right7);
							num4 = Sse2.MoveMask(Sse2.Or(Sse2.Or(left12, right8), right9));
							if (num4 != 0)
							{
								goto IL_42C;
							}
							uintPtr += (UIntPtr)((IntPtr)Vector128<byte>.Count);
						}
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = (UIntPtr)length - uintPtr;
					}
					else if (AdvSimd.Arm64.IsSupported)
					{
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = SpanHelpers.GetByteVector128SpanLength(uintPtr, length);
						Vector128<byte> mask = Vector128.Create(4097).AsByte<ushort>();
						num5 = 0;
						Vector128<byte> left13 = Vector128.Create(value0);
						Vector128<byte> left14 = Vector128.Create(value1);
						Vector128<byte> left15 = Vector128.Create(value2);
						while (uintPtr2 > uintPtr)
						{
							Vector128<byte> right10 = SpanHelpers.LoadVector128(ref searchSpace, uintPtr);
							Vector128<byte> left16 = AdvSimd.CompareEqual(left13, right10);
							Vector128<byte> right11 = AdvSimd.CompareEqual(left14, right10);
							Vector128<byte> right12 = AdvSimd.CompareEqual(left15, right10);
							Vector128<byte> compareResult = AdvSimd.Or(AdvSimd.Or(left16, right11), right12);
							if (SpanHelpers.TryFindFirstMatchedLane(mask, compareResult, ref num5))
							{
								goto IL_4F1;
							}
							uintPtr += (UIntPtr)((IntPtr)Vector128<byte>.Count);
						}
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = (UIntPtr)length - uintPtr;
					}
					else
					{
						if (!Vector.IsHardwareAccelerated || uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = SpanHelpers.GetByteVectorSpanLength(uintPtr, length);
						Vector<byte> right13 = new Vector<byte>(value0);
						Vector<byte> right14 = new Vector<byte>(value1);
						Vector<byte> right15 = new Vector<byte>(value2);
						while (uintPtr2 > uintPtr)
						{
							Vector<byte> left17 = SpanHelpers.LoadVector(ref searchSpace, uintPtr);
							vector = Vector.BitwiseOr<byte>(Vector.BitwiseOr<byte>(Vector.Equals<byte>(left17, right13), Vector.Equals<byte>(left17, right14)), Vector.Equals<byte>(left17, right15));
							if (!Vector<byte>.Zero.Equals(vector))
							{
								goto IL_596;
							}
							uintPtr += (UIntPtr)((IntPtr)Vector<byte>.Count);
						}
						if (uintPtr >= (UIntPtr)length)
						{
							return -1;
						}
						uintPtr2 = (UIntPtr)length - uintPtr;
					}
				}
				else
				{
					uintPtr2 -= (UIntPtr)((IntPtr)8);
					uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_5BA;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_5BD;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_5C3;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_5C9;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)4)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_5CF;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)5)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_5D5;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)6)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_5DB;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)7)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_5E1;
					}
					uintPtr += (UIntPtr)((IntPtr)8);
				}
			}
			IL_2FC:
			return (int)(uintPtr + (UIntPtr)BitOperations.TrailingZeroCount(num2));
			IL_384:
			return (int)(uintPtr + (UIntPtr)BitOperations.TrailingZeroCount(num3));
			IL_42C:
			return (int)(uintPtr + (UIntPtr)BitOperations.TrailingZeroCount(num4));
			IL_4F1:
			return (int)(uintPtr + (UIntPtr)num5);
			IL_596:
			return (int)uintPtr + SpanHelpers.LocateFirstFoundByte(vector);
			IL_5BA:
			return (int)uintPtr;
			IL_5BD:
			return (int)(uintPtr + (UIntPtr)((IntPtr)1));
			IL_5C3:
			return (int)(uintPtr + (UIntPtr)((IntPtr)2));
			IL_5C9:
			return (int)(uintPtr + (UIntPtr)((IntPtr)3));
			IL_5CF:
			return (int)(uintPtr + (UIntPtr)((IntPtr)4));
			IL_5D5:
			return (int)(uintPtr + (UIntPtr)((IntPtr)5));
			IL_5DB:
			return (int)(uintPtr + (UIntPtr)((IntPtr)6));
			IL_5E1:
			return (int)(uintPtr + (UIntPtr)((IntPtr)7));
		}

		// Token: 0x0600135B RID: 4955 RVA: 0x000EA97C File Offset: 0x000E9B7C
		public unsafe static int LastIndexOfAny(ref byte searchSpace, byte value0, byte value1, int length)
		{
			UIntPtr uintPtr = (UIntPtr)length;
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				uintPtr2 = SpanHelpers.UnalignedCountVectorFromEnd(ref searchSpace, length);
			}
			Vector<byte> vector;
			for (;;)
			{
				if (uintPtr2 < (UIntPtr)((IntPtr)8))
				{
					if (uintPtr2 >= (UIntPtr)((IntPtr)4))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)4);
						uintPtr -= (UIntPtr)((IntPtr)4);
						uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)));
						if ((uint)value0 == num || (uint)value1 == num)
						{
							goto IL_28B;
						}
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)));
						if ((uint)value0 == num || (uint)value1 == num)
						{
							goto IL_285;
						}
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)));
						if ((uint)value0 == num || (uint)value1 == num)
						{
							goto IL_27F;
						}
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
						if ((uint)value0 == num)
						{
							break;
						}
						if ((uint)value1 == num)
						{
							break;
						}
					}
					while (uintPtr2 > (UIntPtr)((IntPtr)0))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)1);
						uintPtr -= (UIntPtr)((IntPtr)1);
						uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
						if ((uint)value0 == num || (uint)value1 == num)
						{
							goto IL_27C;
						}
					}
					if (!Vector.IsHardwareAccelerated || uintPtr <= (UIntPtr)((IntPtr)0))
					{
						return -1;
					}
					uintPtr2 = (uintPtr & (UIntPtr)((IntPtr)(~(IntPtr)(Vector<byte>.Count - 1))));
					Vector<byte> right = new Vector<byte>(value0);
					Vector<byte> right2 = new Vector<byte>(value1);
					while (uintPtr2 > (UIntPtr)((IntPtr)(Vector<byte>.Count - 1)))
					{
						Vector<byte> left = SpanHelpers.LoadVector(ref searchSpace, uintPtr - (UIntPtr)((IntPtr)Vector<byte>.Count));
						vector = Vector.BitwiseOr<byte>(Vector.Equals<byte>(left, right), Vector.Equals<byte>(left, right2));
						if (!Vector<byte>.Zero.Equals(vector))
						{
							goto IL_252;
						}
						uintPtr -= (UIntPtr)((IntPtr)Vector<byte>.Count);
						uintPtr2 -= (UIntPtr)((IntPtr)Vector<byte>.Count);
					}
					if (uintPtr <= (UIntPtr)((IntPtr)0))
					{
						return -1;
					}
					uintPtr2 = uintPtr;
				}
				else
				{
					uintPtr2 -= (UIntPtr)((IntPtr)8);
					uintPtr -= (UIntPtr)((IntPtr)8);
					uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)7)));
					if ((uint)value0 == num || (uint)value1 == num)
					{
						goto IL_2A3;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)6)));
					if ((uint)value0 == num || (uint)value1 == num)
					{
						goto IL_29D;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)5)));
					if ((uint)value0 == num || (uint)value1 == num)
					{
						goto IL_297;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)4)));
					if ((uint)value0 == num || (uint)value1 == num)
					{
						goto IL_291;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)));
					if ((uint)value0 == num || (uint)value1 == num)
					{
						goto IL_28B;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)));
					if ((uint)value0 == num || (uint)value1 == num)
					{
						goto IL_285;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)));
					if ((uint)value0 == num || (uint)value1 == num)
					{
						goto IL_27F;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
					if ((uint)value0 == num || (uint)value1 == num)
					{
						break;
					}
				}
			}
			goto IL_27C;
			IL_252:
			return (int)uintPtr - Vector<byte>.Count + SpanHelpers.LocateLastFoundByte(vector);
			IL_27C:
			return (int)uintPtr;
			IL_27F:
			return (int)(uintPtr + (UIntPtr)((IntPtr)1));
			IL_285:
			return (int)(uintPtr + (UIntPtr)((IntPtr)2));
			IL_28B:
			return (int)(uintPtr + (UIntPtr)((IntPtr)3));
			IL_291:
			return (int)(uintPtr + (UIntPtr)((IntPtr)4));
			IL_297:
			return (int)(uintPtr + (UIntPtr)((IntPtr)5));
			IL_29D:
			return (int)(uintPtr + (UIntPtr)((IntPtr)6));
			IL_2A3:
			return (int)(uintPtr + (UIntPtr)((IntPtr)7));
		}

		// Token: 0x0600135C RID: 4956 RVA: 0x000EAC34 File Offset: 0x000E9E34
		public unsafe static int LastIndexOfAny(ref byte searchSpace, byte value0, byte value1, byte value2, int length)
		{
			UIntPtr uintPtr = (UIntPtr)length;
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Vector.IsHardwareAccelerated && length >= Vector<byte>.Count * 2)
			{
				uintPtr2 = SpanHelpers.UnalignedCountVectorFromEnd(ref searchSpace, length);
			}
			Vector<byte> vector;
			for (;;)
			{
				if (uintPtr2 < (UIntPtr)((IntPtr)8))
				{
					if (uintPtr2 >= (UIntPtr)((IntPtr)4))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)4);
						uintPtr -= (UIntPtr)((IntPtr)4);
						uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)));
						if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
						{
							goto IL_31F;
						}
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)));
						if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
						{
							goto IL_319;
						}
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)));
						if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
						{
							goto IL_313;
						}
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
						if ((uint)value0 == num || (uint)value1 == num)
						{
							break;
						}
						if ((uint)value2 == num)
						{
							break;
						}
					}
					while (uintPtr2 > (UIntPtr)((IntPtr)0))
					{
						uintPtr2 -= (UIntPtr)((IntPtr)1);
						uintPtr -= (UIntPtr)((IntPtr)1);
						uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
						if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
						{
							goto IL_310;
						}
					}
					if (!Vector.IsHardwareAccelerated || uintPtr <= (UIntPtr)((IntPtr)0))
					{
						return -1;
					}
					uintPtr2 = (uintPtr & (UIntPtr)((IntPtr)(~(IntPtr)(Vector<byte>.Count - 1))));
					Vector<byte> right = new Vector<byte>(value0);
					Vector<byte> right2 = new Vector<byte>(value1);
					Vector<byte> right3 = new Vector<byte>(value2);
					while (uintPtr2 > (UIntPtr)((IntPtr)(Vector<byte>.Count - 1)))
					{
						Vector<byte> left = SpanHelpers.LoadVector(ref searchSpace, uintPtr - (UIntPtr)((IntPtr)Vector<byte>.Count));
						vector = Vector.BitwiseOr<byte>(Vector.BitwiseOr<byte>(Vector.Equals<byte>(left, right), Vector.Equals<byte>(left, right2)), Vector.Equals<byte>(left, right3));
						if (!Vector<byte>.Zero.Equals(vector))
						{
							goto IL_2E4;
						}
						uintPtr -= (UIntPtr)((IntPtr)Vector<byte>.Count);
						uintPtr2 -= (UIntPtr)((IntPtr)Vector<byte>.Count);
					}
					if (uintPtr <= (UIntPtr)((IntPtr)0))
					{
						return -1;
					}
					uintPtr2 = uintPtr;
				}
				else
				{
					uintPtr2 -= (UIntPtr)((IntPtr)8);
					uintPtr -= (UIntPtr)((IntPtr)8);
					uint num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)7)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_337;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)6)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_331;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)5)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_32B;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)4)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_325;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)3)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_31F;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)2)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_319;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr + (UIntPtr)((IntPtr)1)));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						goto IL_313;
					}
					num = (uint)(*Unsafe.AddByteOffset<byte>(ref searchSpace, uintPtr));
					if ((uint)value0 == num || (uint)value1 == num || (uint)value2 == num)
					{
						break;
					}
				}
			}
			goto IL_310;
			IL_2E4:
			return (int)uintPtr - Vector<byte>.Count + SpanHelpers.LocateLastFoundByte(vector);
			IL_310:
			return (int)uintPtr;
			IL_313:
			return (int)(uintPtr + (UIntPtr)((IntPtr)1));
			IL_319:
			return (int)(uintPtr + (UIntPtr)((IntPtr)2));
			IL_31F:
			return (int)(uintPtr + (UIntPtr)((IntPtr)3));
			IL_325:
			return (int)(uintPtr + (UIntPtr)((IntPtr)4));
			IL_32B:
			return (int)(uintPtr + (UIntPtr)((IntPtr)5));
			IL_331:
			return (int)(uintPtr + (UIntPtr)((IntPtr)6));
			IL_337:
			return (int)(uintPtr + (UIntPtr)((IntPtr)7));
		}

		// Token: 0x0600135D RID: 4957 RVA: 0x000EAF80 File Offset: 0x000EA180
		public unsafe static bool SequenceEqual(ref byte first, ref byte second, [NativeInteger] UIntPtr length)
		{
			if (length >= (UIntPtr)((IntPtr)sizeof(UIntPtr)))
			{
				if (!Unsafe.AreSame<byte>(ref first, ref second))
				{
					if (Sse2.IsSupported)
					{
						if (Avx2.IsSupported && length >= (UIntPtr)((IntPtr)Vector256<byte>.Count))
						{
							UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
							UIntPtr uintPtr2 = length - (UIntPtr)((IntPtr)Vector256<byte>.Count);
							Vector256<byte> value;
							if (uintPtr2 != 0)
							{
								do
								{
									value = Avx2.CompareEqual(SpanHelpers.LoadVector256(ref first, uintPtr), SpanHelpers.LoadVector256(ref second, uintPtr));
									if (Avx2.MoveMask(value) != -1)
									{
										return false;
									}
									uintPtr += (UIntPtr)((IntPtr)Vector256<byte>.Count);
								}
								while (uintPtr2 > uintPtr);
							}
							value = Avx2.CompareEqual(SpanHelpers.LoadVector256(ref first, uintPtr2), SpanHelpers.LoadVector256(ref second, uintPtr2));
							if (Avx2.MoveMask(value) == -1)
							{
								return true;
							}
							return false;
						}
						else if (length >= (UIntPtr)((IntPtr)16))
						{
							UIntPtr uintPtr3 = (UIntPtr)((IntPtr)0);
							UIntPtr uintPtr4 = length - (UIntPtr)((IntPtr)16);
							Vector128<byte> value2;
							if (uintPtr4 != 0)
							{
								do
								{
									value2 = Sse2.CompareEqual(SpanHelpers.LoadVector128(ref first, uintPtr3), SpanHelpers.LoadVector128(ref second, uintPtr3));
									if (Sse2.MoveMask(value2) != 65535)
									{
										return false;
									}
									uintPtr3 += (UIntPtr)((IntPtr)16);
								}
								while (uintPtr4 > uintPtr3);
							}
							value2 = Sse2.CompareEqual(SpanHelpers.LoadVector128(ref first, uintPtr4), SpanHelpers.LoadVector128(ref second, uintPtr4));
							if (Sse2.MoveMask(value2) == 65535)
							{
								return true;
							}
							return false;
						}
					}
					else if (Vector.IsHardwareAccelerated && length >= (UIntPtr)((IntPtr)Vector<byte>.Count))
					{
						UIntPtr uintPtr5 = (UIntPtr)((IntPtr)0);
						UIntPtr uintPtr6 = length - (UIntPtr)((IntPtr)Vector<byte>.Count);
						if (uintPtr6 > (UIntPtr)((IntPtr)0))
						{
							while (!(SpanHelpers.LoadVector(ref first, uintPtr5) != SpanHelpers.LoadVector(ref second, uintPtr5)))
							{
								uintPtr5 += (UIntPtr)((IntPtr)Vector<byte>.Count);
								if (uintPtr6 <= uintPtr5)
								{
									goto IL_1DD;
								}
							}
							return false;
						}
						IL_1DD:
						if (SpanHelpers.LoadVector(ref first, uintPtr6) == SpanHelpers.LoadVector(ref second, uintPtr6))
						{
							return true;
						}
						return false;
					}
					if (Sse2.IsSupported)
					{
						UIntPtr offset = length - (UIntPtr)((IntPtr)sizeof(UIntPtr));
						UIntPtr uintPtr7 = SpanHelpers.LoadNUInt(ref first) - SpanHelpers.LoadNUInt(ref second);
						uintPtr7 |= SpanHelpers.LoadNUInt(ref first, offset) - SpanHelpers.LoadNUInt(ref second, offset);
						return uintPtr7 == (UIntPtr)((IntPtr)0);
					}
					UIntPtr uintPtr8 = (UIntPtr)((IntPtr)0);
					UIntPtr uintPtr9 = length - (UIntPtr)((IntPtr)sizeof(UIntPtr));
					if (uintPtr9 > (UIntPtr)((IntPtr)0))
					{
						while (SpanHelpers.LoadNUInt(ref first, uintPtr8) == SpanHelpers.LoadNUInt(ref second, uintPtr8))
						{
							uintPtr8 += (UIntPtr)((IntPtr)sizeof(UIntPtr));
							if (uintPtr9 <= uintPtr8)
							{
								goto IL_278;
							}
						}
						return false;
					}
					IL_278:
					return SpanHelpers.LoadNUInt(ref first, uintPtr9) == SpanHelpers.LoadNUInt(ref second, uintPtr9);
				}
				return true;
			}
			bool result;
			if (length < (UIntPtr)((IntPtr)4))
			{
				uint num = 0U;
				UIntPtr uintPtr10 = length & (UIntPtr)((IntPtr)2);
				if (uintPtr10 != 0)
				{
					num = (uint)SpanHelpers.LoadUShort(ref first);
					num -= (uint)SpanHelpers.LoadUShort(ref second);
				}
				if ((length & (UIntPtr)((IntPtr)1)) != 0)
				{
					num |= (uint)(*Unsafe.AddByteOffset<byte>(ref first, uintPtr10) - *Unsafe.AddByteOffset<byte>(ref second, uintPtr10));
				}
				result = (num == 0U);
			}
			else
			{
				UIntPtr offset2 = length - (UIntPtr)((IntPtr)4);
				uint num2 = SpanHelpers.LoadUInt(ref first) - SpanHelpers.LoadUInt(ref second);
				num2 |= SpanHelpers.LoadUInt(ref first, offset2) - SpanHelpers.LoadUInt(ref second, offset2);
				result = (num2 == 0U);
			}
			return result;
		}

		// Token: 0x0600135E RID: 4958 RVA: 0x000EB220 File Offset: 0x000EA420
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundByte(Vector<byte> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<byte>(match);
			ulong num = 0UL;
			int i;
			for (i = 0; i < Vector<ulong>.Count; i++)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 8 + SpanHelpers.LocateFirstFoundByte(num);
		}

		// Token: 0x0600135F RID: 4959 RVA: 0x000EB260 File Offset: 0x000EA460
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int SequenceCompareTo(ref byte first, int firstLength, ref byte second, int secondLength)
		{
			if (!Unsafe.AreSame<byte>(ref first, ref second))
			{
				UIntPtr uintPtr = (UIntPtr)((firstLength < secondLength) ? firstLength : secondLength);
				UIntPtr uintPtr2 = (UIntPtr)((IntPtr)0);
				UIntPtr uintPtr3 = uintPtr;
				if (Avx2.IsSupported)
				{
					if (uintPtr3 >= (UIntPtr)((IntPtr)Vector256<byte>.Count))
					{
						uintPtr3 -= (UIntPtr)((IntPtr)Vector256<byte>.Count);
						uint num;
						while (uintPtr3 > uintPtr2)
						{
							num = (uint)Avx2.MoveMask(Avx2.CompareEqual(SpanHelpers.LoadVector256(ref first, uintPtr2), SpanHelpers.LoadVector256(ref second, uintPtr2)));
							if (num != 4294967295U)
							{
								IL_85:
								uint value = ~num;
								uintPtr2 += (UIntPtr)BitOperations.TrailingZeroCount(value);
								return Unsafe.AddByteOffset<byte>(ref first, uintPtr2).CompareTo(*Unsafe.AddByteOffset<byte>(ref second, uintPtr2));
							}
							uintPtr2 += (UIntPtr)((IntPtr)Vector256<byte>.Count);
						}
						uintPtr2 = uintPtr3;
						num = (uint)Avx2.MoveMask(Avx2.CompareEqual(SpanHelpers.LoadVector256(ref first, uintPtr2), SpanHelpers.LoadVector256(ref second, uintPtr2)));
						if (num != 4294967295U)
						{
							goto IL_85;
						}
						goto IL_277;
					}
					else
					{
						if (uintPtr3 >= (UIntPtr)((IntPtr)Vector128<byte>.Count))
						{
							uintPtr3 -= (UIntPtr)((IntPtr)Vector128<byte>.Count);
							uint num2;
							if (uintPtr3 > uintPtr2)
							{
								num2 = (uint)Sse2.MoveMask(Sse2.CompareEqual(SpanHelpers.LoadVector128(ref first, uintPtr2), SpanHelpers.LoadVector128(ref second, uintPtr2)));
								if (num2 != 65535U)
								{
									goto IL_111;
								}
							}
							uintPtr2 = uintPtr3;
							num2 = (uint)Sse2.MoveMask(Sse2.CompareEqual(SpanHelpers.LoadVector128(ref first, uintPtr2), SpanHelpers.LoadVector128(ref second, uintPtr2)));
							if (num2 == 65535U)
							{
								goto IL_277;
							}
							IL_111:
							uint value2 = ~num2;
							uintPtr2 += (UIntPtr)BitOperations.TrailingZeroCount(value2);
							return Unsafe.AddByteOffset<byte>(ref first, uintPtr2).CompareTo(*Unsafe.AddByteOffset<byte>(ref second, uintPtr2));
						}
						goto IL_21B;
					}
				}
				else if (Sse2.IsSupported)
				{
					if (uintPtr3 < (UIntPtr)((IntPtr)Vector128<byte>.Count))
					{
						goto IL_21B;
					}
					uintPtr3 -= (UIntPtr)((IntPtr)Vector128<byte>.Count);
					uint num3;
					while (uintPtr3 > uintPtr2)
					{
						num3 = (uint)Sse2.MoveMask(Sse2.CompareEqual(SpanHelpers.LoadVector128(ref first, uintPtr2), SpanHelpers.LoadVector128(ref second, uintPtr2)));
						if (num3 != 65535U)
						{
							IL_1B3:
							uint value3 = ~num3;
							uintPtr2 += (UIntPtr)BitOperations.TrailingZeroCount(value3);
							return Unsafe.AddByteOffset<byte>(ref first, uintPtr2).CompareTo(*Unsafe.AddByteOffset<byte>(ref second, uintPtr2));
						}
						uintPtr2 += (UIntPtr)((IntPtr)Vector128<byte>.Count);
					}
					uintPtr2 = uintPtr3;
					num3 = (uint)Sse2.MoveMask(Sse2.CompareEqual(SpanHelpers.LoadVector128(ref first, uintPtr2), SpanHelpers.LoadVector128(ref second, uintPtr2)));
					if (num3 != 65535U)
					{
						goto IL_1B3;
					}
					goto IL_277;
				}
				else
				{
					if (!Vector.IsHardwareAccelerated || uintPtr3 <= (UIntPtr)((IntPtr)Vector<byte>.Count))
					{
						goto IL_21B;
					}
					uintPtr3 -= (UIntPtr)((IntPtr)Vector<byte>.Count);
					while (uintPtr3 > uintPtr2)
					{
						if (SpanHelpers.LoadVector(ref first, uintPtr2) != SpanHelpers.LoadVector(ref second, uintPtr2))
						{
							break;
						}
						uintPtr2 += (UIntPtr)((IntPtr)Vector<byte>.Count);
					}
				}
				IL_273:
				while (uintPtr > uintPtr2)
				{
					int num4 = Unsafe.AddByteOffset<byte>(ref first, uintPtr2).CompareTo(*Unsafe.AddByteOffset<byte>(ref second, uintPtr2));
					if (num4 != 0)
					{
						return num4;
					}
					uintPtr2 += (UIntPtr)((IntPtr)1);
				}
				goto IL_277;
				IL_21B:
				if (uintPtr3 > (UIntPtr)((IntPtr)sizeof(UIntPtr)))
				{
					uintPtr3 -= (UIntPtr)((IntPtr)sizeof(UIntPtr));
					while (uintPtr3 > uintPtr2)
					{
						if (SpanHelpers.LoadNUInt(ref first, uintPtr2) != SpanHelpers.LoadNUInt(ref second, uintPtr2))
						{
							break;
						}
						uintPtr2 += (UIntPtr)((IntPtr)sizeof(UIntPtr));
					}
					goto IL_273;
				}
				goto IL_273;
			}
			IL_277:
			return firstLength - secondLength;
		}

		// Token: 0x06001360 RID: 4960 RVA: 0x000EB4E8 File Offset: 0x000EA6E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundByte(Vector<byte> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<byte>(match);
			ulong num = 0UL;
			int i;
			for (i = Vector<ulong>.Count - 1; i >= 0; i--)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 8 + SpanHelpers.LocateLastFoundByte(num);
		}

		// Token: 0x06001361 RID: 4961 RVA: 0x000EB527 File Offset: 0x000EA727
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundByte(ulong match)
		{
			return BitOperations.TrailingZeroCount(match) >> 3;
		}

		// Token: 0x06001362 RID: 4962 RVA: 0x000EB531 File Offset: 0x000EA731
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundByte(ulong match)
		{
			return BitOperations.Log2(match) >> 3;
		}

		// Token: 0x06001363 RID: 4963 RVA: 0x000EB53B File Offset: 0x000EA73B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ushort LoadUShort(ref byte start)
		{
			return Unsafe.ReadUnaligned<ushort>(ref start);
		}

		// Token: 0x06001364 RID: 4964 RVA: 0x000EB543 File Offset: 0x000EA743
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint LoadUInt(ref byte start)
		{
			return Unsafe.ReadUnaligned<uint>(ref start);
		}

		// Token: 0x06001365 RID: 4965 RVA: 0x000EB54B File Offset: 0x000EA74B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint LoadUInt(ref byte start, [NativeInteger] UIntPtr offset)
		{
			return Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref start, offset));
		}

		// Token: 0x06001366 RID: 4966 RVA: 0x000EB559 File Offset: 0x000EA759
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static UIntPtr LoadNUInt(ref byte start)
		{
			return Unsafe.ReadUnaligned<UIntPtr>(ref start);
		}

		// Token: 0x06001367 RID: 4967 RVA: 0x000EB561 File Offset: 0x000EA761
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static UIntPtr LoadNUInt(ref byte start, [NativeInteger] UIntPtr offset)
		{
			return Unsafe.ReadUnaligned<UIntPtr>(Unsafe.AddByteOffset<byte>(ref start, offset));
		}

		// Token: 0x06001368 RID: 4968 RVA: 0x000EB56F File Offset: 0x000EA76F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector<byte> LoadVector(ref byte start, [NativeInteger] UIntPtr offset)
		{
			return Unsafe.ReadUnaligned<Vector<byte>>(Unsafe.AddByteOffset<byte>(ref start, offset));
		}

		// Token: 0x06001369 RID: 4969 RVA: 0x000EB57D File Offset: 0x000EA77D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector128<byte> LoadVector128(ref byte start, [NativeInteger] UIntPtr offset)
		{
			return Unsafe.ReadUnaligned<Vector128<byte>>(Unsafe.AddByteOffset<byte>(ref start, offset));
		}

		// Token: 0x0600136A RID: 4970 RVA: 0x000EB58B File Offset: 0x000EA78B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector256<byte> LoadVector256(ref byte start, [NativeInteger] UIntPtr offset)
		{
			return Unsafe.ReadUnaligned<Vector256<byte>>(Unsafe.AddByteOffset<byte>(ref start, offset));
		}

		// Token: 0x0600136B RID: 4971 RVA: 0x000EB599 File Offset: 0x000EA799
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static UIntPtr GetByteVectorSpanLength([NativeInteger] UIntPtr offset, int length)
		{
			return (UIntPtr)(length - (int)offset & ~(Vector<byte>.Count - 1));
		}

		// Token: 0x0600136C RID: 4972 RVA: 0x000EB5A9 File Offset: 0x000EA7A9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static UIntPtr GetByteVector128SpanLength([NativeInteger] UIntPtr offset, int length)
		{
			return (UIntPtr)(length - (int)offset & ~(Vector128<byte>.Count - 1));
		}

		// Token: 0x0600136D RID: 4973 RVA: 0x000EB5B9 File Offset: 0x000EA7B9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static UIntPtr GetByteVector256SpanLength([NativeInteger] UIntPtr offset, int length)
		{
			return (UIntPtr)(length - (int)offset & ~(Vector256<byte>.Count - 1));
		}

		// Token: 0x0600136E RID: 4974 RVA: 0x000EB5CC File Offset: 0x000EA7CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static UIntPtr UnalignedCountVector(ref byte searchSpace)
		{
			IntPtr intPtr = Unsafe.AsPointer<byte>(ref searchSpace) & Vector<byte>.Count - 1;
			return (UIntPtr)((IntPtr)Vector<byte>.Count - intPtr & (IntPtr)(Vector<byte>.Count - 1));
		}

		// Token: 0x0600136F RID: 4975 RVA: 0x000EB5FC File Offset: 0x000EA7FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static UIntPtr UnalignedCountVector128(ref byte searchSpace)
		{
			IntPtr intPtr = Unsafe.AsPointer<byte>(ref searchSpace) & Vector128<byte>.Count - 1;
			return (UIntPtr)((uint)((IntPtr)Vector128<byte>.Count - intPtr & (IntPtr)(Vector128<byte>.Count - 1)));
		}

		// Token: 0x06001370 RID: 4976 RVA: 0x000EB62C File Offset: 0x000EA82C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static UIntPtr UnalignedCountVectorFromEnd(ref byte searchSpace, int length)
		{
			IntPtr intPtr = Unsafe.AsPointer<byte>(ref searchSpace) & Vector<byte>.Count - 1;
			return (UIntPtr)((uint)((IntPtr)(length & Vector<byte>.Count - 1) + intPtr & (IntPtr)(Vector<byte>.Count - 1)));
		}

		// Token: 0x06001371 RID: 4977 RVA: 0x000EB660 File Offset: 0x000EA860
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryFindFirstMatchedLane(Vector128<byte> mask, Vector128<byte> compareResult, ref int matchedLane)
		{
			Vector128<byte> vector = AdvSimd.And(compareResult, mask);
			Vector128<byte> vector2 = AdvSimd.Arm64.AddPairwise(vector, vector);
			ulong num = vector2.AsUInt64<byte>().ToScalar<ulong>();
			if (num == 0UL)
			{
				return false;
			}
			matchedLane = BitOperations.TrailingZeroCount(num) >> 2;
			return true;
		}

		// Token: 0x06001372 RID: 4978 RVA: 0x000EB69C File Offset: 0x000EA89C
		public static int IndexOf(ref char searchSpace, int searchSpaceLength, ref char value, int valueLength)
		{
			if (valueLength == 0)
			{
				return 0;
			}
			char value2 = value;
			ref char source = ref Unsafe.Add<char>(ref value, 1);
			int num = valueLength - 1;
			int i = searchSpaceLength - num;
			int num2 = 0;
			while (i > 0)
			{
				int num3 = SpanHelpers.IndexOf(Unsafe.Add<char>(ref searchSpace, num2), value2, i);
				if (num3 == -1)
				{
					break;
				}
				i -= num3;
				num2 += num3;
				if (i <= 0)
				{
					break;
				}
				if (SpanHelpers.SequenceEqual(Unsafe.As<char, byte>(Unsafe.Add<char>(ref searchSpace, num2 + 1)), Unsafe.As<char, byte>(ref source), (UIntPtr)num * (UIntPtr)((IntPtr)2)))
				{
					return num2;
				}
				i--;
				num2++;
			}
			return -1;
		}

		// Token: 0x06001373 RID: 4979 RVA: 0x000EB720 File Offset: 0x000EA920
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int SequenceCompareTo(ref char first, int firstLength, ref char second, int secondLength)
		{
			int result = firstLength - secondLength;
			if (!Unsafe.AreSame<char>(ref first, ref second))
			{
				UIntPtr uintPtr = (UIntPtr)((firstLength < secondLength) ? firstLength : secondLength);
				UIntPtr uintPtr2 = (UIntPtr)((IntPtr)0);
				if (uintPtr >= (UIntPtr)((IntPtr)(sizeof(UIntPtr) / 2)))
				{
					if (Vector.IsHardwareAccelerated && uintPtr >= (UIntPtr)((IntPtr)Vector<ushort>.Count))
					{
						UIntPtr uintPtr3 = uintPtr - (UIntPtr)((IntPtr)Vector<ushort>.Count);
						while (!(Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, (IntPtr)uintPtr2))) != Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, (IntPtr)uintPtr2)))))
						{
							uintPtr2 += (UIntPtr)((IntPtr)Vector<ushort>.Count);
							if (uintPtr3 < uintPtr2)
							{
								break;
							}
						}
					}
					while (uintPtr >= uintPtr2 + (UIntPtr)((IntPtr)(sizeof(UIntPtr) / 2)) && Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, (IntPtr)uintPtr2))) == Unsafe.ReadUnaligned<UIntPtr>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, (IntPtr)uintPtr2))))
					{
						uintPtr2 += (UIntPtr)((IntPtr)(sizeof(UIntPtr) / 2));
					}
				}
				if (uintPtr >= uintPtr2 + (UIntPtr)((IntPtr)2) && Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref first, (IntPtr)uintPtr2))) == Unsafe.ReadUnaligned<int>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref second, (IntPtr)uintPtr2))))
				{
					uintPtr2 += (UIntPtr)((IntPtr)2);
				}
				while (uintPtr2 < uintPtr)
				{
					int num = Unsafe.Add<char>(ref first, (IntPtr)uintPtr2).CompareTo(*Unsafe.Add<char>(ref second, (IntPtr)uintPtr2));
					if (num != 0)
					{
						return num;
					}
					uintPtr2 += (UIntPtr)((IntPtr)1);
				}
			}
			return result;
		}

		// Token: 0x06001374 RID: 4980 RVA: 0x000EB84C File Offset: 0x000EAA4C
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static bool Contains(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2;
				char* ptr4 = ptr3 + length;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					int num = (ptr3 & Unsafe.SizeOf<Vector<ushort>>() - 1) / 2;
					length = (Vector<ushort>.Count - num & Vector<ushort>.Count - 1);
				}
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							if (value == *ptr3)
							{
								return true;
							}
							ptr3++;
						}
						if (!Vector.IsHardwareAccelerated || ptr3 >= ptr4)
						{
							break;
						}
						length = (int)((long)(ptr4 - ptr3) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> left = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							Vector<ushort> other = Vector.Equals<ushort>(left, Unsafe.Read<Vector<ushort>>((void*)ptr3));
							if (!Vector<ushort>.Zero.Equals(other))
							{
								return true;
							}
							ptr3 += Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 >= ptr4)
						{
							break;
						}
						length = (int)((long)(ptr4 - ptr3));
					}
					else
					{
						length -= 4;
						if (value == *ptr3 || value == ptr3[1] || value == ptr3[2] || value == ptr3[3])
						{
							return true;
						}
						ptr3 += 4;
					}
				}
				return false;
			}
		}

		// Token: 0x06001375 RID: 4981 RVA: 0x000EB960 File Offset: 0x000EAB60
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int IndexOf(ref char searchSpace, char value, int length)
		{
			IntPtr intPtr = (IntPtr)0;
			IntPtr intPtr2 = (IntPtr)length;
			if ((Unsafe.AsPointer<char>(ref searchSpace) & 1) == 0)
			{
				if (Sse2.IsSupported || AdvSimd.Arm64.IsSupported)
				{
					if (length >= Vector128<ushort>.Count * 2)
					{
						intPtr2 = SpanHelpers.UnalignedCountVector128(ref searchSpace);
					}
				}
				else if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					intPtr2 = SpanHelpers.UnalignedCountVector(ref searchSpace);
				}
			}
			int num;
			int num2;
			int num3;
			int num4;
			int num5;
			Vector<ushort> vector;
			for (;;)
			{
				if (intPtr2 < (IntPtr)4)
				{
					while (intPtr2 > (IntPtr)0)
					{
						if (value == *Unsafe.Add<char>(ref searchSpace, intPtr))
						{
							goto IL_367;
						}
						intPtr++;
						intPtr2--;
					}
					if (Avx2.IsSupported)
					{
						if (intPtr >= (IntPtr)length)
						{
							return -1;
						}
						if ((Unsafe.AsPointer<char>(Unsafe.Add<char>(ref searchSpace, intPtr)) & Vector256<byte>.Count - 1) != null)
						{
							Vector128<ushort> left = Vector128.Create((ushort)value);
							Vector128<ushort> right = SpanHelpers.LoadVector128(ref searchSpace, intPtr);
							num = Sse2.MoveMask(Sse2.CompareEqual(left, right).AsByte<ushort>());
							if (num != 0)
							{
								break;
							}
							intPtr += (IntPtr)Vector128<ushort>.Count;
						}
						intPtr2 = SpanHelpers.GetCharVector256SpanLength(intPtr, (IntPtr)length);
						if (intPtr2 > (IntPtr)0)
						{
							Vector256<ushort> left2 = Vector256.Create((ushort)value);
							do
							{
								Vector256<ushort> right2 = SpanHelpers.LoadVector256(ref searchSpace, intPtr);
								num2 = Avx2.MoveMask(Avx2.CompareEqual(left2, right2).AsByte<ushort>());
								if (num2 != 0)
								{
									goto IL_169;
								}
								intPtr += (IntPtr)Vector256<ushort>.Count;
								intPtr2 -= (IntPtr)Vector256<ushort>.Count;
							}
							while (intPtr2 > (IntPtr)0);
						}
						intPtr2 = SpanHelpers.GetCharVector128SpanLength(intPtr, (IntPtr)length);
						if (intPtr2 > (IntPtr)0)
						{
							Vector128<ushort> left3 = Vector128.Create((ushort)value);
							Vector128<ushort> right3 = SpanHelpers.LoadVector128(ref searchSpace, intPtr);
							num3 = Sse2.MoveMask(Sse2.CompareEqual(left3, right3).AsByte<ushort>());
							if (num3 != 0)
							{
								goto IL_1C0;
							}
							intPtr += (IntPtr)Vector128<ushort>.Count;
						}
						if (intPtr >= (IntPtr)length)
						{
							return -1;
						}
						intPtr2 = (IntPtr)length - intPtr;
					}
					else if (Sse2.IsSupported)
					{
						if (intPtr >= (IntPtr)length)
						{
							return -1;
						}
						intPtr2 = SpanHelpers.GetCharVector128SpanLength(intPtr, (IntPtr)length);
						if (intPtr2 > (IntPtr)0)
						{
							Vector128<ushort> left4 = Vector128.Create((ushort)value);
							do
							{
								Vector128<ushort> right4 = SpanHelpers.LoadVector128(ref searchSpace, intPtr);
								num4 = Sse2.MoveMask(Sse2.CompareEqual(left4, right4).AsByte<ushort>());
								if (num4 != 0)
								{
									goto IL_23C;
								}
								intPtr += (IntPtr)Vector128<ushort>.Count;
								intPtr2 -= (IntPtr)Vector128<ushort>.Count;
							}
							while (intPtr2 > (IntPtr)0);
						}
						if (intPtr >= (IntPtr)length)
						{
							return -1;
						}
						intPtr2 = (IntPtr)length - intPtr;
					}
					else if (AdvSimd.Arm64.IsSupported)
					{
						if (intPtr >= (IntPtr)length)
						{
							return -1;
						}
						intPtr2 = SpanHelpers.GetCharVector128SpanLength(intPtr, (IntPtr)length);
						if (intPtr2 > (IntPtr)0)
						{
							Vector128<ushort> left5 = Vector128.Create((ushort)value);
							num5 = 0;
							do
							{
								Vector128<ushort> right5 = SpanHelpers.LoadVector128(ref searchSpace, intPtr);
								Vector128<ushort> compareResult = AdvSimd.CompareEqual(left5, right5);
								if (SpanHelpers.TryFindFirstMatchedLane(compareResult, ref num5))
								{
									goto IL_2BD;
								}
								intPtr += (IntPtr)Vector128<ushort>.Count;
								intPtr2 -= (IntPtr)Vector128<ushort>.Count;
							}
							while (intPtr2 > (IntPtr)0);
						}
						if (intPtr >= (IntPtr)length)
						{
							return -1;
						}
						intPtr2 = (IntPtr)length - intPtr;
					}
					else
					{
						if (!Vector.IsHardwareAccelerated || intPtr >= (IntPtr)length)
						{
							return -1;
						}
						intPtr2 = SpanHelpers.GetCharVectorSpanLength(intPtr, (IntPtr)length);
						if (intPtr2 > (IntPtr)0)
						{
							Vector<ushort> left6 = new Vector<ushort>((ushort)value);
							do
							{
								vector = Vector.Equals<ushort>(left6, SpanHelpers.LoadVector(ref searchSpace, intPtr));
								if (!Vector<ushort>.Zero.Equals(vector))
								{
									goto IL_333;
								}
								intPtr += (IntPtr)Vector<ushort>.Count;
								intPtr2 -= (IntPtr)Vector<ushort>.Count;
							}
							while (intPtr2 > (IntPtr)0);
						}
						if (intPtr >= (IntPtr)length)
						{
							return -1;
						}
						intPtr2 = (IntPtr)length - intPtr;
					}
				}
				else
				{
					ref char ptr = ref Unsafe.Add<char>(ref searchSpace, intPtr);
					if (value == ptr)
					{
						goto IL_367;
					}
					if (value == *Unsafe.Add<char>(ref ptr, 1))
					{
						goto IL_361;
					}
					if (value == *Unsafe.Add<char>(ref ptr, 2))
					{
						goto IL_35B;
					}
					if (value == *Unsafe.Add<char>(ref ptr, 3))
					{
						goto IL_355;
					}
					intPtr += (IntPtr)4;
					intPtr2 -= (IntPtr)4;
				}
			}
			return (int)((long)intPtr + (long)((ulong)(BitOperations.TrailingZeroCount(num) / 2)));
			IL_169:
			return (int)((long)intPtr + (long)((ulong)(BitOperations.TrailingZeroCount(num2) / 2)));
			IL_1C0:
			return (int)((long)intPtr + (long)((ulong)(BitOperations.TrailingZeroCount(num3) / 2)));
			IL_23C:
			return (int)((long)intPtr + (long)((ulong)(BitOperations.TrailingZeroCount(num4) / 2)));
			IL_2BD:
			return (int)(intPtr + (IntPtr)num5);
			IL_333:
			return (int)(intPtr + (IntPtr)SpanHelpers.LocateFirstFoundChar(vector));
			IL_355:
			return (int)(intPtr + (IntPtr)3);
			IL_35B:
			return (int)(intPtr + (IntPtr)2);
			IL_361:
			return (int)(intPtr + (IntPtr)1);
			IL_367:
			return (int)intPtr;
		}

		// Token: 0x06001376 RID: 4982 RVA: 0x000EBCD8 File Offset: 0x000EAED8
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int IndexOfAny(ref char searchStart, char value0, char value1, int length)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Sse2.IsSupported)
			{
				IntPtr intPtr = (IntPtr)length - (IntPtr)Vector128<ushort>.Count;
				if (intPtr >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr;
					if (Sse2.IsSupported)
					{
						int num;
						if (Avx2.IsSupported && uintPtr2 >= (UIntPtr)((IntPtr)Vector128<ushort>.Count))
						{
							Vector256<ushort> left = Vector256.Create((ushort)value0);
							Vector256<ushort> left2 = Vector256.Create((ushort)value1);
							uintPtr2 -= (UIntPtr)((IntPtr)Vector128<ushort>.Count);
							Vector256<ushort> right;
							while (uintPtr2 > uintPtr)
							{
								right = SpanHelpers.LoadVector256(ref searchStart, uintPtr);
								num = Avx2.MoveMask(Avx2.Or(Avx2.CompareEqual(left, right), Avx2.CompareEqual(left2, right)).AsByte<ushort>());
								if (num != 0)
								{
									goto IL_211;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector256<ushort>.Count);
							}
							right = SpanHelpers.LoadVector256(ref searchStart, uintPtr2);
							uintPtr = uintPtr2;
							num = Avx2.MoveMask(Avx2.Or(Avx2.CompareEqual(left, right), Avx2.CompareEqual(left2, right)).AsByte<ushort>());
							if (num == 0)
							{
								return -1;
							}
						}
						else
						{
							Vector128<ushort> left3 = Vector128.Create((ushort)value0);
							Vector128<ushort> left4 = Vector128.Create((ushort)value1);
							Vector128<ushort> right2;
							while (uintPtr2 > uintPtr)
							{
								right2 = SpanHelpers.LoadVector128(ref searchStart, uintPtr);
								num = Sse2.MoveMask(Sse2.Or(Sse2.CompareEqual(left3, right2), Sse2.CompareEqual(left4, right2)).AsByte<ushort>());
								if (num != 0)
								{
									goto IL_211;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector128<ushort>.Count);
							}
							right2 = SpanHelpers.LoadVector128(ref searchStart, uintPtr2);
							uintPtr = uintPtr2;
							num = Sse2.MoveMask(Sse2.Or(Sse2.CompareEqual(left3, right2), Sse2.CompareEqual(left4, right2)).AsByte<ushort>());
							if (num == 0)
							{
								return -1;
							}
						}
						IL_211:
						uintPtr += (UIntPtr)BitOperations.TrailingZeroCount(num) >> 1;
						goto IL_D5;
					}
					goto IL_223;
				}
			}
			else if (Vector.IsHardwareAccelerated)
			{
				IntPtr intPtr2 = (IntPtr)length - (IntPtr)Vector<ushort>.Count;
				if (intPtr2 >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr2;
					goto IL_223;
				}
			}
			while (uintPtr2 >= (UIntPtr)((IntPtr)4))
			{
				ref char ptr = ref SpanHelpers.Add(ref searchStart, uintPtr);
				int num2 = (int)ptr;
				if ((int)value0 == num2 || (int)value1 == num2)
				{
					goto IL_D5;
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 1));
				if ((int)value0 == num2 || (int)value1 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)1));
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 2));
				if ((int)value0 == num2 || (int)value1 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)2));
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 3));
				if ((int)value0 == num2 || (int)value1 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)3));
				}
				uintPtr += (UIntPtr)((IntPtr)4);
				uintPtr2 -= (UIntPtr)((IntPtr)4);
			}
			while (uintPtr2 > (UIntPtr)((IntPtr)0))
			{
				int num2 = (int)(*SpanHelpers.Add(ref searchStart, uintPtr));
				if ((int)value0 == num2 || (int)value1 == num2)
				{
					goto IL_D5;
				}
				uintPtr += (UIntPtr)((IntPtr)1);
				uintPtr2 -= (UIntPtr)((IntPtr)1);
			}
			return -1;
			IL_D5:
			return (int)uintPtr;
			IL_223:
			if (Sse2.IsSupported || !Vector.IsHardwareAccelerated)
			{
				return -1;
			}
			Vector<ushort> right3 = new Vector<ushort>((ushort)value0);
			Vector<ushort> right4 = new Vector<ushort>((ushort)value1);
			Vector<ushort> vector;
			while (uintPtr2 > uintPtr)
			{
				vector = SpanHelpers.LoadVector(ref searchStart, uintPtr);
				vector = Vector.BitwiseOr<ushort>(Vector.Equals<ushort>(vector, right3), Vector.Equals<ushort>(vector, right4));
				if (!Vector<ushort>.Zero.Equals(vector))
				{
					IL_2C3:
					uintPtr += (UIntPtr)SpanHelpers.LocateFirstFoundChar(vector);
					goto IL_D5;
				}
				uintPtr += (UIntPtr)((IntPtr)Vector<ushort>.Count);
			}
			vector = SpanHelpers.LoadVector(ref searchStart, uintPtr2);
			uintPtr = uintPtr2;
			vector = Vector.BitwiseOr<ushort>(Vector.Equals<ushort>(vector, right3), Vector.Equals<ushort>(vector, right4));
			if (!Vector<ushort>.Zero.Equals(vector))
			{
				goto IL_2C3;
			}
			return -1;
		}

		// Token: 0x06001377 RID: 4983 RVA: 0x000EBFB8 File Offset: 0x000EB1B8
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int IndexOfAny(ref char searchStart, char value0, char value1, char value2, int length)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Sse2.IsSupported)
			{
				IntPtr intPtr = (IntPtr)length - (IntPtr)Vector128<ushort>.Count;
				if (intPtr >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr;
					if (Sse2.IsSupported)
					{
						int num;
						if (Avx2.IsSupported && uintPtr2 >= (UIntPtr)((IntPtr)Vector128<ushort>.Count))
						{
							Vector256<ushort> left = Vector256.Create((ushort)value0);
							Vector256<ushort> left2 = Vector256.Create((ushort)value1);
							Vector256<ushort> left3 = Vector256.Create((ushort)value2);
							uintPtr2 -= (UIntPtr)((IntPtr)Vector128<ushort>.Count);
							Vector256<ushort> right;
							while (uintPtr2 > uintPtr)
							{
								right = SpanHelpers.LoadVector256(ref searchStart, uintPtr);
								num = Avx2.MoveMask(Avx2.Or(Avx2.Or(Avx2.CompareEqual(left, right), Avx2.CompareEqual(left2, right)), Avx2.CompareEqual(left3, right)).AsByte<ushort>());
								if (num != 0)
								{
									goto IL_27F;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector256<ushort>.Count);
							}
							right = SpanHelpers.LoadVector256(ref searchStart, uintPtr2);
							uintPtr = uintPtr2;
							num = Avx2.MoveMask(Avx2.Or(Avx2.Or(Avx2.CompareEqual(left, right), Avx2.CompareEqual(left2, right)), Avx2.CompareEqual(left3, right)).AsByte<ushort>());
							if (num == 0)
							{
								return -1;
							}
						}
						else
						{
							Vector128<ushort> left4 = Vector128.Create((ushort)value0);
							Vector128<ushort> left5 = Vector128.Create((ushort)value1);
							Vector128<ushort> left6 = Vector128.Create((ushort)value2);
							Vector128<ushort> right2;
							while (uintPtr2 > uintPtr)
							{
								right2 = SpanHelpers.LoadVector128(ref searchStart, uintPtr);
								num = Sse2.MoveMask(Sse2.Or(Sse2.Or(Sse2.CompareEqual(left4, right2), Sse2.CompareEqual(left5, right2)), Sse2.CompareEqual(left6, right2)).AsByte<ushort>());
								if (num != 0)
								{
									goto IL_27F;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector128<ushort>.Count);
							}
							right2 = SpanHelpers.LoadVector128(ref searchStart, uintPtr2);
							uintPtr = uintPtr2;
							num = Sse2.MoveMask(Sse2.Or(Sse2.Or(Sse2.CompareEqual(left4, right2), Sse2.CompareEqual(left5, right2)), Sse2.CompareEqual(left6, right2)).AsByte<ushort>());
							if (num == 0)
							{
								return -1;
							}
						}
						IL_27F:
						uintPtr += (UIntPtr)BitOperations.TrailingZeroCount(num) >> 1;
						goto IL_FB;
					}
					goto IL_291;
				}
			}
			else if (Vector.IsHardwareAccelerated)
			{
				IntPtr intPtr2 = (IntPtr)length - (IntPtr)Vector<ushort>.Count;
				if (intPtr2 >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr2;
					goto IL_291;
				}
			}
			while (uintPtr2 >= (UIntPtr)((IntPtr)4))
			{
				ref char ptr = ref SpanHelpers.Add(ref searchStart, uintPtr);
				int num2 = (int)ptr;
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2)
				{
					goto IL_FB;
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 1));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)1));
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 2));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)2));
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 3));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)3));
				}
				uintPtr += (UIntPtr)((IntPtr)4);
				uintPtr2 -= (UIntPtr)((IntPtr)4);
			}
			while (uintPtr2 > (UIntPtr)((IntPtr)0))
			{
				int num2 = (int)(*SpanHelpers.Add(ref searchStart, uintPtr));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2)
				{
					goto IL_FB;
				}
				uintPtr += (UIntPtr)((IntPtr)1);
				uintPtr2 -= (UIntPtr)((IntPtr)1);
			}
			return -1;
			IL_FB:
			return (int)uintPtr;
			IL_291:
			if (Sse2.IsSupported || !Vector.IsHardwareAccelerated)
			{
				return -1;
			}
			Vector<ushort> right3 = new Vector<ushort>((ushort)value0);
			Vector<ushort> right4 = new Vector<ushort>((ushort)value1);
			Vector<ushort> right5 = new Vector<ushort>((ushort)value2);
			Vector<ushort> vector;
			while (uintPtr2 > uintPtr)
			{
				vector = SpanHelpers.LoadVector(ref searchStart, uintPtr);
				vector = Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.Equals<ushort>(vector, right3), Vector.Equals<ushort>(vector, right4)), Vector.Equals<ushort>(vector, right5));
				if (!Vector<ushort>.Zero.Equals(vector))
				{
					IL_355:
					uintPtr += (UIntPtr)SpanHelpers.LocateFirstFoundChar(vector);
					goto IL_FB;
				}
				uintPtr += (UIntPtr)((IntPtr)Vector<ushort>.Count);
			}
			vector = SpanHelpers.LoadVector(ref searchStart, uintPtr2);
			uintPtr = uintPtr2;
			vector = Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.Equals<ushort>(vector, right3), Vector.Equals<ushort>(vector, right4)), Vector.Equals<ushort>(vector, right5));
			if (!Vector<ushort>.Zero.Equals(vector))
			{
				goto IL_355;
			}
			return -1;
		}

		// Token: 0x06001378 RID: 4984 RVA: 0x000EC32C File Offset: 0x000EB52C
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int IndexOfAny(ref char searchStart, char value0, char value1, char value2, char value3, int length)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Sse2.IsSupported)
			{
				IntPtr intPtr = (IntPtr)length - (IntPtr)Vector128<ushort>.Count;
				if (intPtr >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr;
					if (Sse2.IsSupported)
					{
						int num;
						if (Avx2.IsSupported && uintPtr2 >= (UIntPtr)((IntPtr)Vector128<ushort>.Count))
						{
							Vector256<ushort> left = Vector256.Create((ushort)value0);
							Vector256<ushort> left2 = Vector256.Create((ushort)value1);
							Vector256<ushort> left3 = Vector256.Create((ushort)value2);
							Vector256<ushort> left4 = Vector256.Create((ushort)value3);
							uintPtr2 -= (UIntPtr)((IntPtr)Vector128<ushort>.Count);
							Vector256<ushort> right;
							while (uintPtr2 > uintPtr)
							{
								right = SpanHelpers.LoadVector256(ref searchStart, uintPtr);
								num = Avx2.MoveMask(Avx2.CompareEqual(left, right).AsByte<ushort>());
								num |= Avx2.MoveMask(Avx2.CompareEqual(left2, right).AsByte<ushort>());
								num |= Avx2.MoveMask(Avx2.CompareEqual(left3, right).AsByte<ushort>());
								num |= Avx2.MoveMask(Avx2.CompareEqual(left4, right).AsByte<ushort>());
								if (num != 0)
								{
									goto IL_36F;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector256<ushort>.Count);
							}
							right = SpanHelpers.LoadVector256(ref searchStart, uintPtr2);
							uintPtr = uintPtr2;
							num = Avx2.MoveMask(Avx2.CompareEqual(left, right).AsByte<ushort>());
							num |= Avx2.MoveMask(Avx2.CompareEqual(left2, right).AsByte<ushort>());
							num |= Avx2.MoveMask(Avx2.CompareEqual(left3, right).AsByte<ushort>());
							num |= Avx2.MoveMask(Avx2.CompareEqual(left4, right).AsByte<ushort>());
							if (num == 0)
							{
								return -1;
							}
						}
						else
						{
							Vector128<ushort> left5 = Vector128.Create((ushort)value0);
							Vector128<ushort> left6 = Vector128.Create((ushort)value1);
							Vector128<ushort> left7 = Vector128.Create((ushort)value2);
							Vector128<ushort> left8 = Vector128.Create((ushort)value3);
							Vector128<ushort> right2;
							while (uintPtr2 > uintPtr)
							{
								right2 = SpanHelpers.LoadVector128(ref searchStart, uintPtr);
								num = Sse2.MoveMask(Sse2.CompareEqual(left5, right2).AsByte<ushort>());
								num |= Sse2.MoveMask(Sse2.CompareEqual(left6, right2).AsByte<ushort>());
								num |= Sse2.MoveMask(Sse2.CompareEqual(left7, right2).AsByte<ushort>());
								num |= Sse2.MoveMask(Sse2.CompareEqual(left8, right2).AsByte<ushort>());
								if (num != 0)
								{
									goto IL_36F;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector128<ushort>.Count);
							}
							right2 = SpanHelpers.LoadVector128(ref searchStart, uintPtr2);
							uintPtr = uintPtr2;
							num = Sse2.MoveMask(Sse2.CompareEqual(left5, right2).AsByte<ushort>());
							num |= Sse2.MoveMask(Sse2.CompareEqual(left6, right2).AsByte<ushort>());
							num |= Sse2.MoveMask(Sse2.CompareEqual(left7, right2).AsByte<ushort>());
							num |= Sse2.MoveMask(Sse2.CompareEqual(left8, right2).AsByte<ushort>());
							if (num == 0)
							{
								return -1;
							}
						}
						IL_36F:
						uintPtr += (UIntPtr)BitOperations.TrailingZeroCount(num) >> 1;
						goto IL_129;
					}
					goto IL_381;
				}
			}
			else if (Vector.IsHardwareAccelerated)
			{
				IntPtr intPtr2 = (IntPtr)length - (IntPtr)Vector<ushort>.Count;
				if (intPtr2 >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr2;
					goto IL_381;
				}
			}
			while (uintPtr2 >= (UIntPtr)((IntPtr)4))
			{
				ref char ptr = ref SpanHelpers.Add(ref searchStart, uintPtr);
				int num2 = (int)ptr;
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2)
				{
					goto IL_129;
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 1));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)1));
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 2));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)2));
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 3));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)3));
				}
				uintPtr += (UIntPtr)((IntPtr)4);
				uintPtr2 -= (UIntPtr)((IntPtr)4);
			}
			while (uintPtr2 > (UIntPtr)((IntPtr)0))
			{
				int num2 = (int)(*SpanHelpers.Add(ref searchStart, uintPtr));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2)
				{
					goto IL_129;
				}
				uintPtr += (UIntPtr)((IntPtr)1);
				uintPtr2 -= (UIntPtr)((IntPtr)1);
			}
			return -1;
			IL_129:
			return (int)uintPtr;
			IL_381:
			if (Sse2.IsSupported || !Vector.IsHardwareAccelerated)
			{
				return -1;
			}
			Vector<ushort> right3 = new Vector<ushort>((ushort)value0);
			Vector<ushort> right4 = new Vector<ushort>((ushort)value1);
			Vector<ushort> right5 = new Vector<ushort>((ushort)value2);
			Vector<ushort> right6 = new Vector<ushort>((ushort)value3);
			Vector<ushort> vector;
			while (uintPtr2 > uintPtr)
			{
				vector = SpanHelpers.LoadVector(ref searchStart, uintPtr);
				vector = Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.Equals<ushort>(vector, right3), Vector.Equals<ushort>(vector, right4)), Vector.Equals<ushort>(vector, right5)), Vector.Equals<ushort>(vector, right6));
				if (!Vector<ushort>.Zero.Equals(vector))
				{
					IL_46A:
					uintPtr += (UIntPtr)SpanHelpers.LocateFirstFoundChar(vector);
					goto IL_129;
				}
				uintPtr += (UIntPtr)((IntPtr)Vector<ushort>.Count);
			}
			vector = SpanHelpers.LoadVector(ref searchStart, uintPtr2);
			uintPtr = uintPtr2;
			vector = Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.Equals<ushort>(vector, right3), Vector.Equals<ushort>(vector, right4)), Vector.Equals<ushort>(vector, right5)), Vector.Equals<ushort>(vector, right6));
			if (!Vector<ushort>.Zero.Equals(vector))
			{
				goto IL_46A;
			}
			return -1;
		}

		// Token: 0x06001379 RID: 4985 RVA: 0x000EC7B4 File Offset: 0x000EB9B4
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int IndexOfAny(ref char searchStart, char value0, char value1, char value2, char value3, char value4, int length)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			UIntPtr uintPtr2 = (UIntPtr)length;
			if (Sse2.IsSupported)
			{
				IntPtr intPtr = (IntPtr)length - (IntPtr)Vector128<ushort>.Count;
				if (intPtr >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr;
					if (Sse2.IsSupported)
					{
						int num;
						if (Avx2.IsSupported && uintPtr2 >= (UIntPtr)((IntPtr)Vector128<ushort>.Count))
						{
							Vector256<ushort> left = Vector256.Create((ushort)value0);
							Vector256<ushort> left2 = Vector256.Create((ushort)value1);
							Vector256<ushort> left3 = Vector256.Create((ushort)value2);
							Vector256<ushort> left4 = Vector256.Create((ushort)value3);
							Vector256<ushort> left5 = Vector256.Create((ushort)value4);
							uintPtr2 -= (UIntPtr)((IntPtr)Vector128<ushort>.Count);
							Vector256<ushort> right;
							while (uintPtr2 > uintPtr)
							{
								right = SpanHelpers.LoadVector256(ref searchStart, uintPtr);
								num = Avx2.MoveMask(Avx2.CompareEqual(left, right).AsByte<ushort>());
								num |= Avx2.MoveMask(Avx2.CompareEqual(left2, right).AsByte<ushort>());
								num |= Avx2.MoveMask(Avx2.CompareEqual(left3, right).AsByte<ushort>());
								num |= Avx2.MoveMask(Avx2.CompareEqual(left4, right).AsByte<ushort>());
								num |= Avx2.MoveMask(Avx2.CompareEqual(left5, right).AsByte<ushort>());
								if (num != 0)
								{
									goto IL_40F;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector256<ushort>.Count);
							}
							right = SpanHelpers.LoadVector256(ref searchStart, uintPtr2);
							uintPtr = uintPtr2;
							num = Avx2.MoveMask(Avx2.CompareEqual(left, right).AsByte<ushort>());
							num |= Avx2.MoveMask(Avx2.CompareEqual(left2, right).AsByte<ushort>());
							num |= Avx2.MoveMask(Avx2.CompareEqual(left3, right).AsByte<ushort>());
							num |= Avx2.MoveMask(Avx2.CompareEqual(left4, right).AsByte<ushort>());
							num |= Avx2.MoveMask(Avx2.CompareEqual(left5, right).AsByte<ushort>());
							if (num == 0)
							{
								return -1;
							}
						}
						else
						{
							Vector128<ushort> left6 = Vector128.Create((ushort)value0);
							Vector128<ushort> left7 = Vector128.Create((ushort)value1);
							Vector128<ushort> left8 = Vector128.Create((ushort)value2);
							Vector128<ushort> left9 = Vector128.Create((ushort)value3);
							Vector128<ushort> left10 = Vector128.Create((ushort)value4);
							Vector128<ushort> right2;
							while (uintPtr2 > uintPtr)
							{
								right2 = SpanHelpers.LoadVector128(ref searchStart, uintPtr);
								num = Sse2.MoveMask(Sse2.CompareEqual(left6, right2).AsByte<ushort>());
								num |= Sse2.MoveMask(Sse2.CompareEqual(left7, right2).AsByte<ushort>());
								num |= Sse2.MoveMask(Sse2.CompareEqual(left8, right2).AsByte<ushort>());
								num |= Sse2.MoveMask(Sse2.CompareEqual(left9, right2).AsByte<ushort>());
								num |= Sse2.MoveMask(Sse2.CompareEqual(left10, right2).AsByte<ushort>());
								if (num != 0)
								{
									goto IL_40F;
								}
								uintPtr += (UIntPtr)((IntPtr)Vector128<ushort>.Count);
							}
							right2 = SpanHelpers.LoadVector128(ref searchStart, uintPtr2);
							uintPtr = uintPtr2;
							num = Sse2.MoveMask(Sse2.CompareEqual(left6, right2).AsByte<ushort>());
							num |= Sse2.MoveMask(Sse2.CompareEqual(left7, right2).AsByte<ushort>());
							num |= Sse2.MoveMask(Sse2.CompareEqual(left8, right2).AsByte<ushort>());
							num |= Sse2.MoveMask(Sse2.CompareEqual(left9, right2).AsByte<ushort>());
							num |= Sse2.MoveMask(Sse2.CompareEqual(left10, right2).AsByte<ushort>());
							if (num == 0)
							{
								return -1;
							}
						}
						IL_40F:
						uintPtr += (UIntPtr)BitOperations.TrailingZeroCount(num) >> 1;
						goto IL_148;
					}
					goto IL_421;
				}
			}
			else if (Vector.IsHardwareAccelerated)
			{
				IntPtr intPtr2 = (IntPtr)length - (IntPtr)Vector<ushort>.Count;
				if (intPtr2 >= (IntPtr)0)
				{
					uintPtr2 = (UIntPtr)intPtr2;
					goto IL_421;
				}
			}
			while (uintPtr2 >= (UIntPtr)((IntPtr)4))
			{
				ref char ptr = ref SpanHelpers.Add(ref searchStart, uintPtr);
				int num2 = (int)ptr;
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2 || (int)value4 == num2)
				{
					goto IL_148;
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 1));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2 || (int)value4 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)1));
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 2));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2 || (int)value4 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)2));
				}
				num2 = (int)(*Unsafe.Add<char>(ref ptr, 3));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2 || (int)value4 == num2)
				{
					return (int)(uintPtr + (UIntPtr)((IntPtr)3));
				}
				uintPtr += (UIntPtr)((IntPtr)4);
				uintPtr2 -= (UIntPtr)((IntPtr)4);
			}
			while (uintPtr2 > (UIntPtr)((IntPtr)0))
			{
				int num2 = (int)(*SpanHelpers.Add(ref searchStart, uintPtr));
				if ((int)value0 == num2 || (int)value1 == num2 || (int)value2 == num2 || (int)value3 == num2 || (int)value4 == num2)
				{
					goto IL_148;
				}
				uintPtr += (UIntPtr)((IntPtr)1);
				uintPtr2 -= (UIntPtr)((IntPtr)1);
			}
			return -1;
			IL_148:
			return (int)uintPtr;
			IL_421:
			if (Sse2.IsSupported || !Vector.IsHardwareAccelerated)
			{
				return -1;
			}
			Vector<ushort> right3 = new Vector<ushort>((ushort)value0);
			Vector<ushort> right4 = new Vector<ushort>((ushort)value1);
			Vector<ushort> right5 = new Vector<ushort>((ushort)value2);
			Vector<ushort> right6 = new Vector<ushort>((ushort)value3);
			Vector<ushort> right7 = new Vector<ushort>((ushort)value4);
			Vector<ushort> vector;
			while (uintPtr2 > uintPtr)
			{
				vector = SpanHelpers.LoadVector(ref searchStart, uintPtr);
				vector = Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.Equals<ushort>(vector, right3), Vector.Equals<ushort>(vector, right4)), Vector.Equals<ushort>(vector, right5)), Vector.Equals<ushort>(vector, right6)), Vector.Equals<ushort>(vector, right7));
				if (!Vector<ushort>.Zero.Equals(vector))
				{
					IL_52F:
					uintPtr += (UIntPtr)SpanHelpers.LocateFirstFoundChar(vector);
					goto IL_148;
				}
				uintPtr += (UIntPtr)((IntPtr)Vector<ushort>.Count);
			}
			vector = SpanHelpers.LoadVector(ref searchStart, uintPtr2);
			uintPtr = uintPtr2;
			vector = Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.BitwiseOr<ushort>(Vector.Equals<ushort>(vector, right3), Vector.Equals<ushort>(vector, right4)), Vector.Equals<ushort>(vector, right5)), Vector.Equals<ushort>(vector, right6)), Vector.Equals<ushort>(vector, right7));
			if (!Vector<ushort>.Zero.Equals(vector))
			{
				goto IL_52F;
			}
			return -1;
		}

		// Token: 0x0600137A RID: 4986 RVA: 0x000ECD00 File Offset: 0x000EBF00
		[MethodImpl(MethodImplOptions.AggressiveOptimization)]
		public unsafe static int LastIndexOf(ref char searchSpace, char value, int length)
		{
			fixed (char* ptr = &searchSpace)
			{
				char* ptr2 = ptr;
				char* ptr3 = ptr2 + length;
				char* ptr4 = ptr2;
				if (Vector.IsHardwareAccelerated && length >= Vector<ushort>.Count * 2)
				{
					length = (ptr3 & Unsafe.SizeOf<Vector<ushort>>() - 1) / 2;
				}
				char* ptr5;
				Vector<ushort> vector;
				for (;;)
				{
					if (length < 4)
					{
						while (length > 0)
						{
							length--;
							ptr3--;
							if (*ptr3 == value)
							{
								goto IL_11D;
							}
						}
						if (!Vector.IsHardwareAccelerated || ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4) & (long)(~(long)(Vector<ushort>.Count - 1)));
						Vector<ushort> left = new Vector<ushort>((ushort)value);
						while (length > 0)
						{
							ptr5 = ptr3 - Vector<ushort>.Count;
							vector = Vector.Equals<ushort>(left, Unsafe.Read<Vector<ushort>>((void*)ptr5));
							if (!Vector<ushort>.Zero.Equals(vector))
							{
								goto IL_F4;
							}
							ptr3 -= Vector<ushort>.Count;
							length -= Vector<ushort>.Count;
						}
						if (ptr3 == ptr4)
						{
							return -1;
						}
						length = (int)((long)(ptr3 - ptr4));
					}
					else
					{
						length -= 4;
						ptr3 -= 4;
						if (ptr3[3] == value)
						{
							goto IL_139;
						}
						if (ptr3[2] == value)
						{
							goto IL_12F;
						}
						if (ptr3[1] == value)
						{
							goto IL_125;
						}
						if (*ptr3 == value)
						{
							goto IL_11D;
						}
					}
				}
				IL_F4:
				return (int)((long)(ptr5 - ptr4)) + SpanHelpers.LocateLastFoundChar(vector);
				IL_11D:
				return (int)((long)(ptr3 - ptr4));
				IL_125:
				return (int)((long)(ptr3 - ptr4)) + 1;
				IL_12F:
				return (int)((long)(ptr3 - ptr4)) + 2;
				IL_139:
				return (int)((long)(ptr3 - ptr4)) + 3;
			}
		}

		// Token: 0x0600137B RID: 4987 RVA: 0x000ECE50 File Offset: 0x000EC050
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = 0; i < Vector<ulong>.Count; i++)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateFirstFoundChar(num);
		}

		// Token: 0x0600137C RID: 4988 RVA: 0x000ECE8D File Offset: 0x000EC08D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateFirstFoundChar(ulong match)
		{
			return BitOperations.TrailingZeroCount(match) >> 4;
		}

		// Token: 0x0600137D RID: 4989 RVA: 0x000ECE98 File Offset: 0x000EC098
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(Vector<ushort> match)
		{
			Vector<ulong> vector = Vector.AsVectorUInt64<ushort>(match);
			ulong num = 0UL;
			int i;
			for (i = Vector<ulong>.Count - 1; i >= 0; i--)
			{
				num = vector[i];
				if (num != 0UL)
				{
					break;
				}
			}
			return i * 4 + SpanHelpers.LocateLastFoundChar(num);
		}

		// Token: 0x0600137E RID: 4990 RVA: 0x000ECED7 File Offset: 0x000EC0D7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int LocateLastFoundChar(ulong match)
		{
			return BitOperations.Log2(match) >> 4;
		}

		// Token: 0x0600137F RID: 4991 RVA: 0x000ECEE1 File Offset: 0x000EC0E1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector<ushort> LoadVector(ref char start, [NativeInteger] IntPtr offset)
		{
			return Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref start, offset)));
		}

		// Token: 0x06001380 RID: 4992 RVA: 0x000ECEF4 File Offset: 0x000EC0F4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector<ushort> LoadVector(ref char start, [NativeInteger] UIntPtr offset)
		{
			return Unsafe.ReadUnaligned<Vector<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref start, (IntPtr)offset)));
		}

		// Token: 0x06001381 RID: 4993 RVA: 0x000ECF08 File Offset: 0x000EC108
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector128<ushort> LoadVector128(ref char start, [NativeInteger] IntPtr offset)
		{
			return Unsafe.ReadUnaligned<Vector128<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref start, offset)));
		}

		// Token: 0x06001382 RID: 4994 RVA: 0x000ECF1B File Offset: 0x000EC11B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector128<ushort> LoadVector128(ref char start, [NativeInteger] UIntPtr offset)
		{
			return Unsafe.ReadUnaligned<Vector128<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref start, (IntPtr)offset)));
		}

		// Token: 0x06001383 RID: 4995 RVA: 0x000ECF2F File Offset: 0x000EC12F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector256<ushort> LoadVector256(ref char start, [NativeInteger] IntPtr offset)
		{
			return Unsafe.ReadUnaligned<Vector256<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref start, offset)));
		}

		// Token: 0x06001384 RID: 4996 RVA: 0x000ECF42 File Offset: 0x000EC142
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static Vector256<ushort> LoadVector256(ref char start, [NativeInteger] UIntPtr offset)
		{
			return Unsafe.ReadUnaligned<Vector256<ushort>>(Unsafe.As<char, byte>(Unsafe.Add<char>(ref start, (IntPtr)offset)));
		}

		// Token: 0x06001385 RID: 4997 RVA: 0x000ECF56 File Offset: 0x000EC156
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ref char Add(ref char start, [NativeInteger] UIntPtr offset)
		{
			return Unsafe.Add<char>(ref start, (IntPtr)offset);
		}

		// Token: 0x06001386 RID: 4998 RVA: 0x000ECF60 File Offset: 0x000EC160
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static IntPtr GetCharVectorSpanLength([NativeInteger] IntPtr offset, [NativeInteger] IntPtr length)
		{
			return length - offset & (IntPtr)(~(IntPtr)(Vector<ushort>.Count - 1));
		}

		// Token: 0x06001387 RID: 4999 RVA: 0x000ECF6F File Offset: 0x000EC16F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static IntPtr GetCharVector128SpanLength([NativeInteger] IntPtr offset, [NativeInteger] IntPtr length)
		{
			return length - offset & (IntPtr)(~(IntPtr)(Vector128<ushort>.Count - 1));
		}

		// Token: 0x06001388 RID: 5000 RVA: 0x000ECF7E File Offset: 0x000EC17E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static IntPtr GetCharVector256SpanLength([NativeInteger] IntPtr offset, [NativeInteger] IntPtr length)
		{
			return length - offset & (IntPtr)(~(IntPtr)(Vector256<ushort>.Count - 1));
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x000ECF8D File Offset: 0x000EC18D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static IntPtr UnalignedCountVector(ref char searchSpace)
		{
			return (IntPtr)((UIntPtr)(-Unsafe.AsPointer<char>(ref searchSpace) / 2) & (UIntPtr)((IntPtr)(Vector<ushort>.Count - 1)));
		}

		// Token: 0x0600138A RID: 5002 RVA: 0x000ECFA3 File Offset: 0x000EC1A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		private static IntPtr UnalignedCountVector128(ref char searchSpace)
		{
			return (IntPtr)((UIntPtr)(-Unsafe.AsPointer<char>(ref searchSpace) / 2) & (UIntPtr)((IntPtr)(Vector128<ushort>.Count - 1)));
		}

		// Token: 0x0600138B RID: 5003 RVA: 0x000ECFBC File Offset: 0x000EC1BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool TryFindFirstMatchedLane(Vector128<ushort> compareResult, ref int matchedLane)
		{
			Vector128<byte> vector = AdvSimd.Arm64.AddPairwise(compareResult.AsByte<ushort>(), compareResult.AsByte<ushort>());
			ulong num = vector.AsUInt64<byte>().ToScalar<ulong>();
			if (num == 0UL)
			{
				return false;
			}
			matchedLane = BitOperations.TrailingZeroCount(num) >> 3;
			return true;
		}

		// Token: 0x0600138C RID: 5004 RVA: 0x000ECFF7 File Offset: 0x000EC1F7
		public static void ClearWithoutReferences(ref byte b, [NativeInteger] UIntPtr byteLength)
		{
			if (byteLength == 0)
			{
				return;
			}
			if (byteLength <= (UIntPtr)((IntPtr)768))
			{
				Unsafe.InitBlockUnaligned(ref b, 0, (uint)byteLength);
				return;
			}
			Buffer._ZeroMemory(ref b, byteLength);
		}

		// Token: 0x0600138D RID: 5005 RVA: 0x000ED018 File Offset: 0x000EC218
		public unsafe static void ClearWithReferences(ref IntPtr ip, [NativeInteger] UIntPtr pointerSizeLength)
		{
			while (pointerSizeLength >= (UIntPtr)((IntPtr)8))
			{
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -1) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -2) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -3) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -4) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -5) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -6) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -7) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -8) = 0;
				pointerSizeLength -= (UIntPtr)((IntPtr)8);
			}
			if (pointerSizeLength < (UIntPtr)((IntPtr)4))
			{
				if (pointerSizeLength < (UIntPtr)((IntPtr)2))
				{
					if (pointerSizeLength <= (UIntPtr)((IntPtr)0))
					{
						return;
					}
					goto IL_12F;
				}
			}
			else
			{
				*Unsafe.Add<IntPtr>(ref ip, 2) = 0;
				*Unsafe.Add<IntPtr>(ref ip, 3) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -3) = 0;
				*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -2) = 0;
			}
			*Unsafe.Add<IntPtr>(ref ip, 1) = 0;
			*Unsafe.Add<IntPtr>(Unsafe.Add<IntPtr>(ref ip, (IntPtr)pointerSizeLength), -1) = 0;
			IL_12F:
			ip = 0;
		}

		// Token: 0x0600138E RID: 5006 RVA: 0x000ED15C File Offset: 0x000EC35C
		public static int IndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return 0;
			}
			T value2 = value;
			ref T second = ref Unsafe.Add<T>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				int num4 = SpanHelpers.IndexOf<T>(Unsafe.Add<T>(ref searchSpace, num2), value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				num2 += num4;
				if (SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(ref searchSpace, num2 + 1), ref second, num))
				{
					break;
				}
				num2++;
			}
			return num2;
		}

		// Token: 0x0600138F RID: 5007 RVA: 0x000ED1C8 File Offset: 0x000EC3C8
		public unsafe static bool Contains<T>(ref T searchSpace, T value, int length) where T : IEquatable<T>
		{
			IntPtr intPtr = (IntPtr)0;
			if (default(T) == null)
			{
				if (value == null)
				{
					byte* ptr = length;
					intPtr = (IntPtr)0;
					while (intPtr.ToPointer() < (void*)ptr)
					{
						if (*Unsafe.Add<T>(ref searchSpace, intPtr) == null)
						{
							return true;
						}
						intPtr += 1;
					}
					return false;
				}
			}
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 0)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 4)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 5)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 6)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 7)))
				{
					return true;
				}
				intPtr += 8;
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 0)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)) || value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					return true;
				}
				intPtr += 4;
			}
			while (length > 0)
			{
				length--;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					return true;
				}
				intPtr += 1;
			}
			return false;
		}

		// Token: 0x06001390 RID: 5008 RVA: 0x000ED438 File Offset: 0x000EC638
		public unsafe static int IndexOf<T>(ref T searchSpace, T value, int length) where T : IEquatable<T>
		{
			IntPtr intPtr = (IntPtr)0;
			if (default(T) == null)
			{
				if (value == null)
				{
					byte* ptr = length;
					intPtr = (IntPtr)0;
					while (intPtr.ToPointer() < (void*)ptr)
					{
						if (*Unsafe.Add<T>(ref searchSpace, intPtr) == null)
						{
							goto IL_259;
						}
						intPtr += 1;
					}
					return -1;
				}
			}
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_259;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					IL_261:
					return (void*)(intPtr + 1);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					IL_26F:
					return (void*)(intPtr + 2);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					IL_27D:
					return (void*)(intPtr + 3);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 4)))
				{
					return (void*)(intPtr + 4);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 5)))
				{
					return (void*)(intPtr + 5);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 6)))
				{
					return (void*)(intPtr + 6);
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 7)))
				{
					return (void*)(intPtr + 7);
				}
				intPtr += 8;
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_259;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 1)))
				{
					goto IL_261;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 2)))
				{
					goto IL_26F;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr + 3)))
				{
					goto IL_27D;
				}
				intPtr += 4;
			}
			while (length > 0)
			{
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, intPtr)))
				{
					goto IL_259;
				}
				intPtr += 1;
				length--;
			}
			return -1;
			IL_259:
			return (void*)intPtr;
		}

		// Token: 0x06001391 RID: 5009 RVA: 0x000ED708 File Offset: 0x000EC908
		public unsafe static int IndexOfAny<T>(ref T searchSpace, T value0, T value1, int length) where T : IEquatable<T>
		{
			int i = 0;
			if (default(T) == null)
			{
				if (value0 == null || value1 == null)
				{
					for (i = 0; i < length; i++)
					{
						T t = *Unsafe.Add<T>(ref searchSpace, i);
						if (t == null)
						{
							if (value0 == null)
							{
								return i;
							}
							if (value1 == null)
							{
								return i;
							}
						}
						else if (t.Equals(value0) || t.Equals(value1))
						{
							return i;
						}
					}
					return -1;
				}
			}
			while (length - i >= 8)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_352:
					return i + 1;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_356:
					return i + 2;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_35A:
					return i + 3;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 4);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i + 4;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 5);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i + 5;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 6);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i + 6;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 7);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i + 7;
				}
				i += 8;
			}
			if (length - i >= 4)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_352;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_356;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_35A;
				}
				i += 4;
			}
			while (i < length)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001392 RID: 5010 RVA: 0x000EDA84 File Offset: 0x000ECC84
		public unsafe static int IndexOfAny<T>(ref T searchSpace, T value0, T value1, T value2, int length) where T : IEquatable<T>
		{
			int i = 0;
			if (default(T) == null)
			{
				if (value0 == null || value1 == null || value2 == null)
				{
					for (i = 0; i < length; i++)
					{
						T t = *Unsafe.Add<T>(ref searchSpace, i);
						if (t == null)
						{
							if (value0 == null || value1 == null)
							{
								return i;
							}
							if (value2 == null)
							{
								return i;
							}
						}
						else if (t.Equals(value0) || t.Equals(value1) || t.Equals(value2))
						{
							return i;
						}
					}
					return -1;
				}
			}
			while (length - i >= 8)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_473:
					return i + 1;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_477:
					return i + 2;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_47B:
					return i + 3;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 4);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i + 4;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 5);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i + 5;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 6);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i + 6;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 7);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i + 7;
				}
				i += 8;
			}
			if (length - i >= 4)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 1);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_473;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 2);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_477;
				}
				t = *Unsafe.Add<T>(ref searchSpace, i + 3);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_47B;
				}
				i += 4;
			}
			while (i < length)
			{
				T t = *Unsafe.Add<T>(ref searchSpace, i);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return i;
				}
				i++;
			}
			return -1;
		}

		// Token: 0x06001393 RID: 5011 RVA: 0x000EDF20 File Offset: 0x000ED120
		public unsafe static int IndexOfAny<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.IndexOf<T>(ref searchSpace, *Unsafe.Add<T>(ref value, i), searchSpaceLength);
				if (num2 < num)
				{
					num = num2;
					searchSpaceLength = num2;
					if (num == 0)
					{
						break;
					}
				}
			}
			return num;
		}

		// Token: 0x06001394 RID: 5012 RVA: 0x000EDF64 File Offset: 0x000ED164
		public static int LastIndexOf<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return searchSpaceLength;
			}
			T value2 = value;
			ref T second = ref Unsafe.Add<T>(ref value, 1);
			int num = valueLength - 1;
			int num2 = 0;
			int num4;
			for (;;)
			{
				int num3 = searchSpaceLength - num2 - num;
				if (num3 <= 0)
				{
					return -1;
				}
				num4 = SpanHelpers.LastIndexOf<T>(ref searchSpace, value2, num3);
				if (num4 == -1)
				{
					return -1;
				}
				if (SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(ref searchSpace, num4 + 1), ref second, num))
				{
					break;
				}
				num2 += num3 - num4;
			}
			return num4;
		}

		// Token: 0x06001395 RID: 5013 RVA: 0x000EDFC8 File Offset: 0x000ED1C8
		public unsafe static int LastIndexOf<T>(ref T searchSpace, T value, int length) where T : IEquatable<T>
		{
			if (default(T) == null)
			{
				if (value == null)
				{
					for (length--; length >= 0; length--)
					{
						if (*Unsafe.Add<T>(ref searchSpace, length) == null)
						{
							return length;
						}
					}
					return -1;
				}
			}
			while (length >= 8)
			{
				length -= 8;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 7)))
				{
					return length + 7;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 6)))
				{
					return length + 6;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 5)))
				{
					return length + 5;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 4)))
				{
					return length + 4;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 3)))
				{
					IL_208:
					return length + 3;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 2)))
				{
					IL_204:
					return length + 2;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 1)))
				{
					IL_200:
					return length + 1;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 3)))
				{
					goto IL_208;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 2)))
				{
					goto IL_204;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length + 1)))
				{
					goto IL_200;
				}
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				if (value.Equals(*Unsafe.Add<T>(ref searchSpace, length)))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x06001396 RID: 5014 RVA: 0x000EE1F0 File Offset: 0x000ED3F0
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, T value0, T value1, int length) where T : IEquatable<T>
		{
			if (default(T) == null)
			{
				if (value0 == null || value1 == null)
				{
					for (length--; length >= 0; length--)
					{
						T t = *Unsafe.Add<T>(ref searchSpace, length);
						if (t == null)
						{
							if (value0 == null)
							{
								return length;
							}
							if (value1 == null)
							{
								return length;
							}
						}
						else if (t.Equals(value0) || t.Equals(value1))
						{
							return length;
						}
					}
					return -1;
				}
			}
			while (length >= 8)
			{
				length -= 8;
				T t = *Unsafe.Add<T>(ref searchSpace, length + 7);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length + 7;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 6);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length + 6;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 5);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length + 5;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 4);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length + 4;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_35B:
					return length + 3;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_357:
					return length + 2;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(t) || value1.Equals(t))
				{
					IL_353:
					return length + 1;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				T t = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_35B;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_357;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(t) || value1.Equals(t))
				{
					goto IL_353;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t))
				{
					return length;
				}
				if (value1.Equals(t))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				T t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x06001397 RID: 5015 RVA: 0x000EE56C File Offset: 0x000ED76C
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, T value0, T value1, T value2, int length) where T : IEquatable<T>
		{
			if (default(T) == null)
			{
				if (value0 == null || value1 == null)
				{
					for (length--; length >= 0; length--)
					{
						T t = *Unsafe.Add<T>(ref searchSpace, length);
						if (t == null)
						{
							if (value0 == null || value1 == null)
							{
								return length;
							}
							if (value2 == null)
							{
								return length;
							}
						}
						else if (t.Equals(value0) || t.Equals(value1) || t.Equals(value2))
						{
							return length;
						}
					}
					return -1;
				}
			}
			while (length >= 8)
			{
				length -= 8;
				T t = *Unsafe.Add<T>(ref searchSpace, length + 7);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length + 7;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 6);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length + 6;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 5);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length + 5;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 4);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length + 4;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_487:
					return length + 3;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_482:
					return length + 2;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					IL_47D:
					return length + 1;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length;
				}
			}
			if (length >= 4)
			{
				length -= 4;
				T t = *Unsafe.Add<T>(ref searchSpace, length + 3);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_487;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 2);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_482;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length + 1);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					goto IL_47D;
				}
				t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t))
				{
					return length;
				}
				if (value2.Equals(t))
				{
					return length;
				}
			}
			while (length > 0)
			{
				length--;
				T t = *Unsafe.Add<T>(ref searchSpace, length);
				if (value0.Equals(t) || value1.Equals(t) || value2.Equals(t))
				{
					return length;
				}
			}
			return -1;
		}

		// Token: 0x06001398 RID: 5016 RVA: 0x000EEA18 File Offset: 0x000EDC18
		public unsafe static int LastIndexOfAny<T>(ref T searchSpace, int searchSpaceLength, ref T value, int valueLength) where T : IEquatable<T>
		{
			if (valueLength == 0)
			{
				return -1;
			}
			int num = -1;
			for (int i = 0; i < valueLength; i++)
			{
				int num2 = SpanHelpers.LastIndexOf<T>(ref searchSpace, *Unsafe.Add<T>(ref value, i), searchSpaceLength);
				if (num2 > num)
				{
					num = num2;
				}
			}
			return num;
		}

		// Token: 0x06001399 RID: 5017 RVA: 0x000EEA54 File Offset: 0x000EDC54
		public unsafe static bool SequenceEqual<T>(ref T first, ref T second, int length) where T : IEquatable<T>
		{
			if (!Unsafe.AreSame<T>(ref first, ref second))
			{
				IntPtr intPtr = (IntPtr)0;
				while (length >= 8)
				{
					length -= 8;
					T t = *Unsafe.Add<T>(ref first, intPtr);
					T t2 = *Unsafe.Add<T>(ref second, intPtr);
					if ((t != null) ? t.Equals(t2) : (t2 == null))
					{
						t = *Unsafe.Add<T>(ref first, intPtr + 1);
						t2 = *Unsafe.Add<T>(ref second, intPtr + 1);
						if ((t != null) ? t.Equals(t2) : (t2 == null))
						{
							t = *Unsafe.Add<T>(ref first, intPtr + 2);
							t2 = *Unsafe.Add<T>(ref second, intPtr + 2);
							if ((t != null) ? t.Equals(t2) : (t2 == null))
							{
								t = *Unsafe.Add<T>(ref first, intPtr + 3);
								t2 = *Unsafe.Add<T>(ref second, intPtr + 3);
								if ((t != null) ? t.Equals(t2) : (t2 == null))
								{
									t = *Unsafe.Add<T>(ref first, intPtr + 4);
									t2 = *Unsafe.Add<T>(ref second, intPtr + 4);
									if ((t != null) ? t.Equals(t2) : (t2 == null))
									{
										t = *Unsafe.Add<T>(ref first, intPtr + 5);
										t2 = *Unsafe.Add<T>(ref second, intPtr + 5);
										if ((t != null) ? t.Equals(t2) : (t2 == null))
										{
											t = *Unsafe.Add<T>(ref first, intPtr + 6);
											t2 = *Unsafe.Add<T>(ref second, intPtr + 6);
											if ((t != null) ? t.Equals(t2) : (t2 == null))
											{
												t = *Unsafe.Add<T>(ref first, intPtr + 7);
												t2 = *Unsafe.Add<T>(ref second, intPtr + 7);
												if ((t != null) ? t.Equals(t2) : (t2 == null))
												{
													intPtr += 8;
													continue;
												}
											}
										}
									}
								}
							}
						}
					}
					return false;
				}
				if (length >= 4)
				{
					length -= 4;
					T t = *Unsafe.Add<T>(ref first, intPtr);
					T t2 = *Unsafe.Add<T>(ref second, intPtr);
					if (!((t != null) ? t.Equals(t2) : (t2 == null)))
					{
						return false;
					}
					t = *Unsafe.Add<T>(ref first, intPtr + 1);
					t2 = *Unsafe.Add<T>(ref second, intPtr + 1);
					if (!((t != null) ? t.Equals(t2) : (t2 == null)))
					{
						return false;
					}
					t = *Unsafe.Add<T>(ref first, intPtr + 2);
					t2 = *Unsafe.Add<T>(ref second, intPtr + 2);
					if (!((t != null) ? t.Equals(t2) : (t2 == null)))
					{
						return false;
					}
					t = *Unsafe.Add<T>(ref first, intPtr + 3);
					t2 = *Unsafe.Add<T>(ref second, intPtr + 3);
					if (!((t != null) ? t.Equals(t2) : (t2 == null)))
					{
						return false;
					}
					intPtr += 4;
				}
				while (length > 0)
				{
					T t = *Unsafe.Add<T>(ref first, intPtr);
					T t2 = *Unsafe.Add<T>(ref second, intPtr);
					if (!((t != null) ? t.Equals(t2) : (t2 == null)))
					{
						return false;
					}
					intPtr += 1;
					length--;
				}
			}
			return true;
		}

		// Token: 0x0600139A RID: 5018 RVA: 0x000EEE6C File Offset: 0x000EE06C
		public unsafe static int SequenceCompareTo<T>(ref T first, int firstLength, ref T second, int secondLength) where T : IComparable<T>
		{
			int num = firstLength;
			if (num > secondLength)
			{
				num = secondLength;
			}
			int i = 0;
			while (i < num)
			{
				T t = *Unsafe.Add<T>(ref second, i);
				T ptr2;
				ref T ptr = ptr2 = Unsafe.Add<T>(ref first, i);
				T t2 = default(T);
				if (t2 != null)
				{
					goto IL_52;
				}
				t2 = ptr;
				ptr2 = ref t2;
				if (t2 != null)
				{
					goto IL_52;
				}
				int num2 = (t == null) ? 0 : -1;
				IL_5E:
				int num3 = num2;
				if (num3 != 0)
				{
					return num3;
				}
				i++;
				continue;
				IL_52:
				num2 = ptr2.CompareTo(t);
				goto IL_5E;
			}
			return firstLength.CompareTo(secondLength);
		}

		// Token: 0x02000181 RID: 385
		internal readonly struct ComparerComparable<T, TComparer> : IComparable<T> where TComparer : IComparer<T>
		{
			// Token: 0x0600139B RID: 5019 RVA: 0x000EEEED File Offset: 0x000EE0ED
			public ComparerComparable(T value, TComparer comparer)
			{
				this._value = value;
				this._comparer = comparer;
			}

			// Token: 0x0600139C RID: 5020 RVA: 0x000EEF00 File Offset: 0x000EE100
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			public int CompareTo(T other)
			{
				TComparer comparer = this._comparer;
				return comparer.Compare(this._value, other);
			}

			// Token: 0x04000499 RID: 1177
			private readonly T _value;

			// Token: 0x0400049A RID: 1178
			private readonly TComparer _comparer;
		}
	}
}
