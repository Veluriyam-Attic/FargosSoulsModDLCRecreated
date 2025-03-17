using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System.Text.Unicode
{
	// Token: 0x02000397 RID: 919
	internal static class Utf16Utility
	{
		// Token: 0x06003064 RID: 12388 RVA: 0x0015836B File Offset: 0x0015756B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool AllCharsInUInt32AreAscii(uint value)
		{
			return (value & 4286644096U) == 0U;
		}

		// Token: 0x06003065 RID: 12389 RVA: 0x00158377 File Offset: 0x00157577
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool AllCharsInUInt64AreAscii(ulong value)
		{
			return (value & 18410996206198128512UL) == 0UL;
		}

		// Token: 0x06003066 RID: 12390 RVA: 0x001653F0 File Offset: 0x001645F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint ConvertAllAsciiCharsInUInt32ToLowercase(uint value)
		{
			uint num = value + 8388736U - 4259905U;
			uint num2 = value + 8388736U - 5963867U;
			uint num3 = num ^ num2;
			uint num4 = (num3 & 8388736U) >> 2;
			return value ^ num4;
		}

		// Token: 0x06003067 RID: 12391 RVA: 0x0016542C File Offset: 0x0016462C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint ConvertAllAsciiCharsInUInt32ToUppercase(uint value)
		{
			uint num = value + 8388736U - 6357089U;
			uint num2 = value + 8388736U - 8061051U;
			uint num3 = num ^ num2;
			uint num4 = (num3 & 8388736U) >> 2;
			return value ^ num4;
		}

		// Token: 0x06003068 RID: 12392 RVA: 0x00165468 File Offset: 0x00164668
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool UInt32ContainsAnyLowercaseAsciiChar(uint value)
		{
			uint num = value + 8388736U - 6357089U;
			uint num2 = value + 8388736U - 8061051U;
			uint num3 = num ^ num2;
			return (num3 & 8388736U) > 0U;
		}

		// Token: 0x06003069 RID: 12393 RVA: 0x001654A0 File Offset: 0x001646A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool UInt32ContainsAnyUppercaseAsciiChar(uint value)
		{
			uint num = value + 8388736U - 4259905U;
			uint num2 = value + 8388736U - 5963867U;
			uint num3 = num ^ num2;
			return (num3 & 8388736U) > 0U;
		}

		// Token: 0x0600306A RID: 12394 RVA: 0x001654D8 File Offset: 0x001646D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool UInt32OrdinalIgnoreCaseAscii(uint valueA, uint valueB)
		{
			uint num = valueA ^ valueB;
			uint num2 = valueA + 16777472U - 4259905U;
			uint num3 = (valueA | 2097184U) + 8388736U - 8061051U;
			uint num4 = num2 | num3;
			return ((num4 >> 2 | 4292870111U) & num) == 0U;
		}

		// Token: 0x0600306B RID: 12395 RVA: 0x00165520 File Offset: 0x00164720
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool UInt64OrdinalIgnoreCaseAscii(ulong valueA, ulong valueB)
		{
			ulong num = valueA + 36029346783166592UL - 18296152663326785UL;
			ulong num2 = (valueA | 9007336695791648UL) + 72058693566333184UL - 34621950424449147UL;
			ulong num3 = (36029346783166592UL & num & num2) >> 2;
			return (valueA | num3) == (valueB | num3);
		}

		// Token: 0x0600306C RID: 12396 RVA: 0x0016557C File Offset: 0x0016477C
		public unsafe static char* GetPointerToFirstInvalidChar(char* pInputBuffer, int inputLength, out long utf8CodeUnitCountAdjustment, out int scalarCountAdjustment)
		{
			int num = (int)ASCIIUtility.GetIndexOfFirstNonAsciiChar(pInputBuffer, (UIntPtr)inputLength);
			pInputBuffer += (ulong)num * 2UL / 2UL;
			inputLength -= num;
			if (inputLength == 0)
			{
				utf8CodeUnitCountAdjustment = 0L;
				scalarCountAdjustment = 0;
				return pInputBuffer;
			}
			long num2 = 0L;
			int num3 = 0;
			if (Sse2.IsSupported)
			{
				if (inputLength >= Vector128<ushort>.Count)
				{
					Vector128<ushort> vector = Vector128.Create(128);
					Vector128<ushort> right = Vector128.Create(43008);
					Vector128<short> right2 = Vector128.Create(-30720);
					Vector128<ushort> zero = Vector128<ushort>.Zero;
					Vector128<byte> bitMask = BitConverter.IsLittleEndian ? Vector128.Create(9241421688590303745UL).AsByte<ulong>() : Vector128.Create(72624976668147840L).AsByte<long>();
					do
					{
						Vector128<ushort> vector2;
						if (AdvSimd.Arm64.IsSupported)
						{
							vector2 = AdvSimd.LoadVector128((ushort*)pInputBuffer);
						}
						else
						{
							vector2 = Sse2.LoadVector128((ushort*)pInputBuffer);
						}
						Vector128<ushort> left;
						if (AdvSimd.Arm64.IsSupported)
						{
							left = AdvSimd.Min(vector2, vector);
						}
						else if (Sse41.IsSupported)
						{
							left = Sse41.Min(vector2, vector);
						}
						else
						{
							left = Sse2.AndNot(Sse2.CompareGreaterThan(vector.AsInt16<ushort>(), vector2.AsInt16<ushort>()).AsUInt16<short>(), vector);
						}
						uint num4;
						if (AdvSimd.IsSupported)
						{
							Vector128<ushort> right3 = AdvSimd.Subtract(zero, AdvSimd.ShiftRightLogical(vector2, 11));
							num4 = Utf16Utility.GetNonAsciiBytes(AdvSimd.Or(left, right3).AsByte<ushort>(), bitMask);
						}
						else
						{
							Vector128<ushort> right3 = Sse2.Subtract(zero, Sse2.ShiftRightLogical(vector2, 11));
							num4 = (uint)Sse2.MoveMask(Sse2.Or(left, right3).AsByte<ushort>());
						}
						uint num5 = (uint)BitOperations.PopCount(num4);
						if (AdvSimd.Arm64.IsSupported)
						{
							vector2 = AdvSimd.Add(vector2, right);
							num4 = Utf16Utility.GetNonAsciiBytes(AdvSimd.CompareLessThan(vector2.AsInt16<ushort>(), right2).AsByte<short>(), bitMask);
						}
						else
						{
							vector2 = Sse2.Add(vector2, right);
							num4 = (uint)Sse2.MoveMask(Sse2.CompareLessThan(vector2.AsInt16<ushort>(), right2).AsByte<short>());
						}
						if (num4 != 0U)
						{
							uint num6;
							if (AdvSimd.Arm64.IsSupported)
							{
								num6 = Utf16Utility.GetNonAsciiBytes(AdvSimd.ShiftRightLogical(vector2, 3).AsByte<ushort>(), bitMask);
							}
							else
							{
								num6 = (uint)Sse2.MoveMask(Sse2.ShiftRightLogical(vector2, 3).AsByte<ushort>());
							}
							uint num7 = num6 & num4;
							uint num8 = (num6 ^ 21845U) & num4;
							num8 <<= 2;
							if ((uint)((ushort)num8) != num7)
							{
								break;
							}
							if (num8 > 65535U)
							{
								num8 = (uint)((ushort)num8);
								num5 -= 2U;
								pInputBuffer--;
								inputLength++;
							}
							UIntPtr uintPtr = (UIntPtr)BitOperations.PopCount(num8);
							num3 -= (int)uintPtr;
							int size = IntPtr.Size;
							num2 -= (long)((ulong)uintPtr);
							num2 -= (long)((ulong)uintPtr);
						}
						num2 += (long)((ulong)num5);
						pInputBuffer += Vector128<ushort>.Count;
						inputLength -= Vector128<ushort>.Count;
					}
					while (inputLength >= Vector128<ushort>.Count);
				}
			}
			else if (Vector.IsHardwareAccelerated && inputLength >= Vector<ushort>.Count)
			{
				Vector<ushort> right4 = new Vector<ushort>(128);
				Vector<ushort> right5 = new Vector<ushort>(1024);
				Vector<ushort> right6 = new Vector<ushort>(2048);
				Vector<ushort> right7 = new Vector<ushort>(55296);
				do
				{
					Vector<ushort> left2 = Unsafe.ReadUnaligned<Vector<ushort>>((void*)pInputBuffer);
					Vector<ushort> right8 = Vector.GreaterThanOrEqual<ushort>(left2, right4);
					Vector<ushort> right9 = Vector.GreaterThanOrEqual<ushort>(left2, right6);
					Vector<ulong> vector3 = (Vector<ulong>)(Vector<ushort>.Zero - right8 - right9);
					UIntPtr uintPtr2 = (UIntPtr)((IntPtr)0);
					for (int i = 0; i < Vector<ulong>.Count; i++)
					{
						uintPtr2 += (UIntPtr)vector3[i];
					}
					uint num9 = (uint)uintPtr2;
					int size2 = IntPtr.Size;
					num9 += (uint)(uintPtr2 >> 32);
					num9 = (uint)((ushort)num9) + (num9 >> 16);
					left2 -= right7;
					Vector<ushort> left3 = Vector.LessThan<ushort>(left2, right6);
					if (left3 != Vector<ushort>.Zero)
					{
						Vector<ushort> right10 = Vector.LessThan<ushort>(left2, right5);
						Vector<ushort> vector4 = Vector.AndNot<ushort>(left3, right10);
						if (vector4[0] != 0)
						{
							goto IL_49B;
						}
						ushort num10 = 0;
						for (int j = 0; j < Vector<ushort>.Count - 1; j++)
						{
							num10 -= right10[j];
							if (right10[j] != vector4[j + 1])
							{
								goto IL_497;
							}
						}
						if (right10[Vector<ushort>.Count - 1] != 0)
						{
							pInputBuffer--;
							inputLength++;
							num9 -= 2U;
						}
						IntPtr intPtr = (IntPtr)((UIntPtr)num10);
						num3 -= (int)intPtr;
						num2 -= (long)intPtr;
						num2 -= (long)intPtr;
					}
					num2 += (long)((ulong)num9);
					pInputBuffer += Vector<ushort>.Count;
					inputLength -= Vector<ushort>.Count;
				}
				while (inputLength >= Vector<ushort>.Count);
			}
			IL_497:
			while (inputLength > 0)
			{
				uint num11 = (uint)(*pInputBuffer);
				if (num11 > 127U)
				{
					num2 += (long)((ulong)(num11 + 129024U >> 16));
					if (UnicodeUtility.IsSurrogateCodePoint(num11))
					{
						num2 -= 2L;
						if (inputLength == 1)
						{
							break;
						}
						num11 = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
						if ((num11 - (BitConverter.IsLittleEndian ? 3691042816U : 3623934976U) & 4227922944U) != 0U)
						{
							break;
						}
						num3--;
						num2 += 2L;
						pInputBuffer++;
						inputLength--;
					}
				}
				pInputBuffer++;
				inputLength--;
			}
			IL_49B:
			utf8CodeUnitCountAdjustment = num2;
			scalarCountAdjustment = num3;
			return pInputBuffer;
		}

		// Token: 0x0600306D RID: 12397 RVA: 0x00165A2C File Offset: 0x00164C2C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint GetNonAsciiBytes(Vector128<byte> value, Vector128<byte> bitMask128)
		{
			Vector128<byte> left = AdvSimd.ShiftRightArithmetic(value.AsSByte<byte>(), 7).AsByte<sbyte>();
			Vector128<byte> vector = AdvSimd.And(left, bitMask128);
			vector = AdvSimd.Arm64.AddPairwise(vector, vector);
			vector = AdvSimd.Arm64.AddPairwise(vector, vector);
			vector = AdvSimd.Arm64.AddPairwise(vector, vector);
			return (uint)vector.AsUInt16<byte>().ToScalar<ushort>();
		}
	}
}
