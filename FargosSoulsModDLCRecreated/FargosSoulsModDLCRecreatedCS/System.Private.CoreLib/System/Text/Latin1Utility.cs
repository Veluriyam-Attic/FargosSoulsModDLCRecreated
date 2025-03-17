using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200037B RID: 891
	internal static class Latin1Utility
	{
		// Token: 0x06002F00 RID: 12032 RVA: 0x0015E96D File Offset: 0x0015DB6D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong GetIndexOfFirstNonLatin1Char(char* pBuffer, ulong bufferLength)
		{
			if (!Sse2.IsSupported)
			{
				return Latin1Utility.GetIndexOfFirstNonLatin1Char_Default(pBuffer, bufferLength);
			}
			return Latin1Utility.GetIndexOfFirstNonLatin1Char_Sse2(pBuffer, bufferLength);
		}

		// Token: 0x06002F01 RID: 12033 RVA: 0x0015E988 File Offset: 0x0015DB88
		private unsafe static ulong GetIndexOfFirstNonLatin1Char_Default(char* pBuffer, ulong bufferLength)
		{
			char* ptr = pBuffer;
			if (Vector.IsHardwareAccelerated && bufferLength >= (ulong)(2 * Vector<ushort>.Count))
			{
				uint count = (uint)Vector<ushort>.Count;
				uint count2 = (uint)Vector<byte>.Count;
				Vector<ushort> right = new Vector<ushort>(255);
				if (Vector.LessThanOrEqualAll<ushort>(Unsafe.ReadUnaligned<Vector<ushort>>((void*)pBuffer), right))
				{
					char* ptr2 = pBuffer + bufferLength * 2UL / 2UL - (ulong)count * 2UL / 2UL;
					pBuffer = (pBuffer + (ulong)count2 / 2UL & ~(count2 - 1U));
					while (!Vector.GreaterThanAny<ushort>(Unsafe.Read<Vector<ushort>>((void*)pBuffer), right))
					{
						pBuffer += (ulong)count * 2UL / 2UL;
						if (pBuffer != ptr2)
						{
							break;
						}
					}
					bufferLength -= (ulong)((pBuffer - ptr) / 2);
				}
			}
			while (bufferLength >= 4UL)
			{
				uint num = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
				uint num2 = Unsafe.ReadUnaligned<uint>((void*)(pBuffer + 2));
				if (!Latin1Utility.AllCharsInUInt32AreLatin1(num | num2))
				{
					if (Latin1Utility.AllCharsInUInt32AreLatin1(num))
					{
						num = num2;
						pBuffer += 2;
					}
					IL_114:
					if (Latin1Utility.FirstCharInUInt32IsLatin1(num))
					{
						pBuffer++;
					}
					IL_109:
					ulong num3 = (ulong)(pBuffer - ptr);
					return num3 / 2UL;
				}
				pBuffer += 4;
				bufferLength -= 4UL;
			}
			if ((bufferLength & 2UL) != 0UL)
			{
				uint num = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
				if (!Latin1Utility.AllCharsInUInt32AreLatin1(num))
				{
					goto IL_114;
				}
				pBuffer += 2;
			}
			if ((bufferLength & 1UL) != 0UL && *pBuffer <= 'ÿ')
			{
				pBuffer++;
				goto IL_109;
			}
			goto IL_109;
		}

		// Token: 0x06002F02 RID: 12034 RVA: 0x0015EAB8 File Offset: 0x0015DCB8
		private unsafe static ulong GetIndexOfFirstNonLatin1Char_Sse2(char* pBuffer, ulong bufferLength)
		{
			if (bufferLength == 0UL)
			{
				return 0UL;
			}
			uint num = (uint)Unsafe.SizeOf<Vector128<byte>>();
			uint num2 = num / 2U;
			char* ptr = pBuffer;
			if (bufferLength >= (ulong)num2)
			{
				Vector128<ushort> right = Vector128.Create(65280);
				Vector128<ushort> right2 = Vector128.Create(32512);
				Vector128<ushort> left = Sse2.LoadVector128((ushort*)pBuffer);
				uint num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
				if ((num3 & 43690U) == 0U)
				{
					bufferLength <<= 1;
					if (bufferLength >= (ulong)(2U * num))
					{
						pBuffer = (pBuffer + (ulong)num / 2UL & ~(num - 1U));
						bufferLength = bufferLength / 2UL + ptr;
						bufferLength -= pBuffer;
						if (bufferLength >= (ulong)(2U * num))
						{
							char* ptr2 = pBuffer + bufferLength / 2UL - (ulong)(2U * num) / 2UL;
							Vector128<ushort> vector;
							for (;;)
							{
								left = Sse2.LoadAlignedVector128((ushort*)pBuffer);
								vector = Sse2.LoadAlignedVector128((ushort*)(pBuffer + (ulong)num2 * 2UL / 2UL));
								Vector128<ushort> left2 = Sse2.Or(left, vector);
								if (Sse41.IsSupported)
								{
									if (!Sse41.TestZ(left2, right))
									{
										break;
									}
								}
								else
								{
									num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left2, right2).AsByte<ushort>());
									if ((num3 & 43690U) != 0U)
									{
										break;
									}
								}
								pBuffer += (ulong)(2U * num2) * 2UL / 2UL;
								if (pBuffer != ptr2)
								{
									goto IL_100;
								}
							}
							if (Sse41.IsSupported)
							{
								if (!Sse41.TestZ(left, right))
								{
									goto IL_1ED;
								}
							}
							else
							{
								num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
								if ((num3 & 43690U) != 0U)
								{
									goto IL_201;
								}
							}
							pBuffer += (ulong)num2 * 2UL / 2UL;
							left = vector;
							goto IL_1ED;
						}
						IL_100:
						if ((bufferLength & (ulong)num) == 0UL)
						{
							goto IL_14E;
						}
						left = Sse2.LoadAlignedVector128((ushort*)pBuffer);
						if (Sse41.IsSupported)
						{
							if (!Sse41.TestZ(left, right))
							{
								goto IL_1ED;
							}
						}
						else
						{
							num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
							if ((num3 & 43690U) != 0U)
							{
								goto IL_201;
							}
						}
					}
					pBuffer += (ulong)num2 * 2UL / 2UL;
					IL_14E:
					if (((uint)((byte)bufferLength) & num - 1U) != 0U)
					{
						pBuffer = pBuffer + (bufferLength & (ulong)(num - 1U)) / 2UL - num / 2U;
						left = Sse2.LoadVector128((ushort*)pBuffer);
						if (Sse41.IsSupported)
						{
							if (!Sse41.TestZ(left, right))
							{
								goto IL_1ED;
							}
						}
						else
						{
							num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
							if ((num3 & 43690U) != 0U)
							{
								goto IL_201;
							}
						}
						pBuffer += (ulong)num2 * 2UL / 2UL;
						goto IL_1A6;
					}
					goto IL_1A6;
					IL_1ED:
					num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
				}
				IL_201:
				num3 &= 43690U;
				pBuffer = (char*)((byte*)(pBuffer + BitOperations.TrailingZeroCount(num3) / 2) - 1);
			}
			else
			{
				uint num5;
				if ((bufferLength & 4UL) != 0UL)
				{
					if (Bmi1.X64.IsSupported)
					{
						ulong num4 = Unsafe.ReadUnaligned<ulong>((void*)pBuffer);
						if (!Latin1Utility.AllCharsInUInt64AreLatin1(num4))
						{
							num4 &= 18374966859414961920UL;
							pBuffer += (Bmi1.X64.TrailingZeroCount(num4) / 8UL & 18446744073709551614UL) / 2UL;
							goto IL_1A6;
						}
					}
					else
					{
						num5 = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
						uint num6 = Unsafe.ReadUnaligned<uint>((void*)(pBuffer + 2));
						if (!Latin1Utility.AllCharsInUInt32AreLatin1(num5 | num6))
						{
							if (Latin1Utility.AllCharsInUInt32AreLatin1(num5))
							{
								num5 = num6;
								pBuffer += 2;
								goto IL_21B;
							}
							goto IL_21B;
						}
					}
					pBuffer += 4;
					goto IL_2BB;
				}
				goto IL_2BB;
				IL_21B:
				if (Latin1Utility.FirstCharInUInt32IsLatin1(num5))
				{
					pBuffer++;
					goto IL_1A6;
				}
				goto IL_1A6;
				IL_2BB:
				if ((bufferLength & 2UL) != 0UL)
				{
					num5 = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
					if (!Latin1Utility.AllCharsInUInt32AreLatin1(num5))
					{
						goto IL_21B;
					}
					pBuffer += 2;
				}
				if ((bufferLength & 1UL) != 0UL && *pBuffer <= 'ÿ')
				{
					pBuffer++;
				}
			}
			IL_1A6:
			return (ulong)((pBuffer - ptr) / 2);
		}

		// Token: 0x06002F03 RID: 12035 RVA: 0x0015EDC0 File Offset: 0x0015DFC0
		public unsafe static ulong NarrowUtf16ToLatin1(char* pUtf16Buffer, byte* pLatin1Buffer, ulong elementCount)
		{
			ulong num = 0UL;
			ulong num2;
			if (Sse2.IsSupported)
			{
				if (elementCount >= (ulong)(2 * Unsafe.SizeOf<Vector128<byte>>()))
				{
					int size = IntPtr.Size;
					num2 = Unsafe.ReadUnaligned<ulong>((void*)pUtf16Buffer);
					if (!Latin1Utility.AllCharsInUInt64AreLatin1(num2))
					{
						goto IL_186;
					}
					num = Latin1Utility.NarrowUtf16ToLatin1_Sse2(pUtf16Buffer, pLatin1Buffer, elementCount);
				}
			}
			else if (Vector.IsHardwareAccelerated)
			{
				uint num3 = (uint)Unsafe.SizeOf<Vector<byte>>();
				if (elementCount >= (ulong)(2U * num3))
				{
					int size2 = IntPtr.Size;
					num2 = Unsafe.ReadUnaligned<ulong>((void*)pUtf16Buffer);
					if (!Latin1Utility.AllCharsInUInt64AreLatin1(num2))
					{
						goto IL_186;
					}
					Vector<ushort> right = new Vector<ushort>(255);
					ulong num4 = elementCount - (ulong)(2U * num3);
					do
					{
						Vector<ushort> vector = Unsafe.ReadUnaligned<Vector<ushort>>((void*)(pUtf16Buffer + num * 2UL / 2UL));
						Vector<ushort> vector2 = Unsafe.ReadUnaligned<Vector<ushort>>((void*)(pUtf16Buffer + num * 2UL / 2UL + Vector<ushort>.Count));
						if (Vector.GreaterThanAny<ushort>(Vector.BitwiseOr<ushort>(vector, vector2), right))
						{
							break;
						}
						Vector<byte> value = Vector.Narrow(vector, vector2);
						Unsafe.WriteUnaligned<Vector<byte>>((void*)(pLatin1Buffer + num), value);
						num += (ulong)num3;
					}
					while (num <= num4);
				}
			}
			ulong num5 = elementCount - num;
			if (num5 >= 4UL)
			{
				ulong num6 = num + num5 - 4UL;
				do
				{
					int size3 = IntPtr.Size;
					num2 = Unsafe.ReadUnaligned<ulong>((void*)(pUtf16Buffer + num * 2UL / 2UL));
					if (!Latin1Utility.AllCharsInUInt64AreLatin1(num2))
					{
						goto IL_186;
					}
					Latin1Utility.NarrowFourUtf16CharsToLatin1AndWriteToBuffer(ref pLatin1Buffer[num], num2);
					num += 4UL;
				}
				while (num <= num6);
			}
			uint num7;
			if (((uint)num5 & 2U) != 0U)
			{
				num7 = Unsafe.ReadUnaligned<uint>((void*)(pUtf16Buffer + num * 2UL / 2UL));
				if (!Latin1Utility.AllCharsInUInt32AreLatin1(num7))
				{
					goto IL_1C9;
				}
				Latin1Utility.NarrowTwoUtf16CharsToLatin1AndWriteToBuffer(ref pLatin1Buffer[num], num7);
				num += 2UL;
			}
			if (((uint)num5 & 1U) != 0U)
			{
				num7 = (uint)pUtf16Buffer[num * 2UL / 2UL];
				if (num7 <= 255U)
				{
					pLatin1Buffer[num] = (byte)num7;
					num += 1UL;
				}
			}
			return num;
			IL_186:
			int size4 = IntPtr.Size;
			if (BitConverter.IsLittleEndian)
			{
				num7 = (uint)num2;
			}
			else
			{
				num7 = (uint)(num2 >> 32);
			}
			if (Latin1Utility.AllCharsInUInt32AreLatin1(num7))
			{
				Latin1Utility.NarrowTwoUtf16CharsToLatin1AndWriteToBuffer(ref pLatin1Buffer[num], num7);
				if (BitConverter.IsLittleEndian)
				{
					num7 = (uint)(num2 >> 32);
				}
				else
				{
					num7 = (uint)num2;
				}
				num += 2UL;
			}
			IL_1C9:
			if (Latin1Utility.FirstCharInUInt32IsLatin1(num7))
			{
				if (!BitConverter.IsLittleEndian)
				{
					num7 >>= 16;
				}
				pLatin1Buffer[num] = (byte)num7;
				return num + 1UL;
			}
			return num;
		}

		// Token: 0x06002F04 RID: 12036 RVA: 0x0015EFBC File Offset: 0x0015E1BC
		private unsafe static ulong NarrowUtf16ToLatin1_Sse2(char* pUtf16Buffer, byte* pLatin1Buffer, ulong elementCount)
		{
			uint num = (uint)Unsafe.SizeOf<Vector128<byte>>();
			ulong num2 = (ulong)(num - 1U);
			Vector128<short> right = Vector128.Create(-256);
			Vector128<ushort> right2 = Vector128.Create(32512);
			Vector128<short> vector = Sse2.LoadVector128((short*)pUtf16Buffer);
			if (Sse41.IsSupported)
			{
				if (!Sse41.TestZ(vector, right))
				{
					return 0UL;
				}
			}
			else if ((Sse2.MoveMask(Sse2.AddSaturate(vector.AsUInt16<short>(), right2).AsByte<ushort>()) & 43690) != 0)
			{
				return 0UL;
			}
			Vector128<byte> vector2 = Sse2.PackUnsignedSaturate(vector, vector);
			Sse2.StoreScalar((ulong*)pLatin1Buffer, vector2.AsUInt64<byte>());
			ulong num3 = (ulong)(num / 2U);
			if ((pLatin1Buffer & num / 2U) == 0U)
			{
				vector = Sse2.LoadVector128((short*)(pUtf16Buffer + num3 * 2UL / 2UL));
				if (Sse41.IsSupported)
				{
					if (!Sse41.TestZ(vector, right))
					{
						return num3;
					}
				}
				else if ((Sse2.MoveMask(Sse2.AddSaturate(vector.AsUInt16<short>(), right2).AsByte<ushort>()) & 43690) != 0)
				{
					return num3;
				}
				vector2 = Sse2.PackUnsignedSaturate(vector, vector);
				Sse2.StoreScalar((ulong*)(pLatin1Buffer + num3), vector2.AsUInt64<byte>());
			}
			num3 = num - (pLatin1Buffer & num2);
			ulong num4 = elementCount - (ulong)num;
			for (;;)
			{
				vector = Sse2.LoadVector128((short*)(pUtf16Buffer + num3 * 2UL / 2UL));
				Vector128<short> right3 = Sse2.LoadVector128((short*)(pUtf16Buffer + num3 * 2UL / 2UL + (ulong)(num / 2U) * 2UL / 2UL));
				Vector128<short> vector3 = Sse2.Or(vector, right3);
				if (Sse41.IsSupported)
				{
					if (!Sse41.TestZ(vector3, right))
					{
						break;
					}
				}
				else if ((Sse2.MoveMask(Sse2.AddSaturate(vector3.AsUInt16<short>(), right2).AsByte<ushort>()) & 43690) != 0)
				{
					break;
				}
				vector2 = Sse2.PackUnsignedSaturate(vector, right3);
				Sse2.StoreAligned(pLatin1Buffer + num3, vector2);
				num3 += (ulong)num;
				if (num3 > num4)
				{
					return num3;
				}
			}
			if (Sse41.IsSupported)
			{
				if (!Sse41.TestZ(vector, right))
				{
					return num3;
				}
			}
			else if ((Sse2.MoveMask(Sse2.AddSaturate(vector.AsUInt16<short>(), right2).AsByte<ushort>()) & 43690) != 0)
			{
				return num3;
			}
			vector2 = Sse2.PackUnsignedSaturate(vector, vector);
			Sse2.StoreScalar((ulong*)(pLatin1Buffer + num3), vector2.AsUInt64<byte>());
			num3 += (ulong)(num / 2U);
			return num3;
		}

		// Token: 0x06002F05 RID: 12037 RVA: 0x0015F1A6 File Offset: 0x0015E3A6
		public unsafe static void WidenLatin1ToUtf16(byte* pLatin1Buffer, char* pUtf16Buffer, ulong elementCount)
		{
			if (Sse2.IsSupported)
			{
				Latin1Utility.WidenLatin1ToUtf16_Sse2(pLatin1Buffer, pUtf16Buffer, elementCount);
				return;
			}
			Latin1Utility.WidenLatin1ToUtf16_Fallback(pLatin1Buffer, pUtf16Buffer, elementCount);
		}

		// Token: 0x06002F06 RID: 12038 RVA: 0x0015F1C0 File Offset: 0x0015E3C0
		private unsafe static void WidenLatin1ToUtf16_Sse2(byte* pLatin1Buffer, char* pUtf16Buffer, ulong elementCount)
		{
			uint num = (uint)Unsafe.SizeOf<Vector128<byte>>();
			ulong num2 = (ulong)(num - 1U);
			ulong num3 = 0UL;
			Vector128<byte> zero = Vector128<byte>.Zero;
			if (elementCount >= (ulong)num)
			{
				Vector128<byte> left = Sse2.LoadScalarVector128((ulong*)pLatin1Buffer).AsByte<ulong>();
				Sse2.Store((byte*)pUtf16Buffer, Sse2.UnpackLow(left, zero));
				num3 = (num >> 1) - (pUtf16Buffer >> 1 & num2 >> 1);
				char* ptr = pUtf16Buffer + num3 * 2UL / 2UL;
				ulong num4 = elementCount - (ulong)num;
				while (num3 <= num4)
				{
					left = Sse2.LoadVector128(pLatin1Buffer + num3);
					Vector128<byte> source = Sse2.UnpackLow(left, zero);
					Sse2.StoreAligned((byte*)ptr, source);
					Vector128<byte> source2 = Sse2.UnpackHigh(left, zero);
					Sse2.StoreAligned((byte*)(ptr + num / 2U), source2);
					num3 += (ulong)num;
					ptr += (ulong)num * 2UL / 2UL;
				}
			}
			uint num5 = (uint)elementCount - (uint)num3;
			if ((num5 & 8U) != 0U)
			{
				Vector128<byte> left = Sse2.LoadScalarVector128((ulong*)(pLatin1Buffer + num3)).AsByte<ulong>();
				Sse2.Store((byte*)(pUtf16Buffer + num3 * 2UL / 2UL), Sse2.UnpackLow(left, zero));
				num3 += 8UL;
			}
			if ((num5 & 4U) != 0U)
			{
				Vector128<byte> left = Sse2.LoadScalarVector128((uint*)(pLatin1Buffer + num3)).AsByte<uint>();
				Sse2.StoreScalar((ulong*)(pUtf16Buffer + num3 * 2UL / 2UL), Sse2.UnpackLow(left, zero).AsUInt64<byte>());
				num3 += 4UL;
			}
			if ((num5 & 3U) != 0U)
			{
				pUtf16Buffer[num3 * 2UL / 2UL] = (char)pLatin1Buffer[num3];
				if ((num5 & 2U) != 0U)
				{
					pUtf16Buffer[(num3 + 1UL) * 2UL / 2UL] = (char)pLatin1Buffer[num3 + 1UL];
					if ((num5 & 1U) != 0U)
					{
						pUtf16Buffer[(num3 + 2UL) * 2UL / 2UL] = (char)pLatin1Buffer[num3 + 2UL];
					}
				}
			}
		}

		// Token: 0x06002F07 RID: 12039 RVA: 0x0015F31C File Offset: 0x0015E51C
		private unsafe static void WidenLatin1ToUtf16_Fallback(byte* pLatin1Buffer, char* pUtf16Buffer, ulong elementCount)
		{
			ulong num = 0UL;
			if (Vector.IsHardwareAccelerated)
			{
				uint count = (uint)Vector<byte>.Count;
				if (elementCount >= (ulong)count)
				{
					ulong num2 = elementCount - (ulong)count;
					do
					{
						Vector<byte> value = Unsafe.ReadUnaligned<Vector<byte>>((void*)(pLatin1Buffer + num));
						Vector<ushort> value2;
						Vector<ushort> value3;
						Vector.Widen(Vector.AsVectorByte<byte>(value), out value2, out value3);
						Unsafe.WriteUnaligned<Vector<ushort>>((void*)(pUtf16Buffer + num * 2UL / 2UL), value2);
						Unsafe.WriteUnaligned<Vector<ushort>>((void*)(pUtf16Buffer + num * 2UL / 2UL + Vector<ushort>.Count), value3);
						num += (ulong)count;
					}
					while (num <= num2);
				}
			}
			while (num < elementCount)
			{
				pUtf16Buffer[num * 2UL / 2UL] = (char)pLatin1Buffer[num];
				num += 1UL;
			}
		}

		// Token: 0x06002F08 RID: 12040 RVA: 0x0015F3A2 File Offset: 0x0015E5A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool AllCharsInUInt32AreLatin1(uint value)
		{
			return (value & 4278255360U) == 0U;
		}

		// Token: 0x06002F09 RID: 12041 RVA: 0x0015F3AE File Offset: 0x0015E5AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool AllCharsInUInt64AreLatin1(ulong value)
		{
			return (value & 18374966859414961920UL) == 0UL;
		}

		// Token: 0x06002F0A RID: 12042 RVA: 0x0015F3BF File Offset: 0x0015E5BF
		private static bool FirstCharInUInt32IsLatin1(uint value)
		{
			return (BitConverter.IsLittleEndian && (value & 65280U) == 0U) || (!BitConverter.IsLittleEndian && (value & 4278190080U) == 0U);
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x0015F3E8 File Offset: 0x0015E5E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void NarrowFourUtf16CharsToLatin1AndWriteToBuffer(ref byte outputBuffer, ulong value)
		{
			if (Sse2.X64.IsSupported)
			{
				Vector128<short> vector = Sse2.X64.ConvertScalarToVector128UInt64(value).AsInt16<ulong>();
				Vector128<uint> value2 = Sse2.PackUnsignedSaturate(vector, vector).AsUInt32<byte>();
				Unsafe.WriteUnaligned<uint>(ref outputBuffer, Sse2.ConvertToUInt32(value2));
				return;
			}
			if (BitConverter.IsLittleEndian)
			{
				outputBuffer = (byte)value;
				value >>= 16;
				*Unsafe.Add<byte>(ref outputBuffer, 1) = (byte)value;
				value >>= 16;
				*Unsafe.Add<byte>(ref outputBuffer, 2) = (byte)value;
				value >>= 16;
				*Unsafe.Add<byte>(ref outputBuffer, 3) = (byte)value;
				return;
			}
			*Unsafe.Add<byte>(ref outputBuffer, 3) = (byte)value;
			value >>= 16;
			*Unsafe.Add<byte>(ref outputBuffer, 2) = (byte)value;
			value >>= 16;
			*Unsafe.Add<byte>(ref outputBuffer, 1) = (byte)value;
			value >>= 16;
			outputBuffer = (byte)value;
		}

		// Token: 0x06002F0C RID: 12044 RVA: 0x00158D62 File Offset: 0x00157F62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void NarrowTwoUtf16CharsToLatin1AndWriteToBuffer(ref byte outputBuffer, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				outputBuffer = (byte)value;
				*Unsafe.Add<byte>(ref outputBuffer, 1) = (byte)(value >> 16);
				return;
			}
			*Unsafe.Add<byte>(ref outputBuffer, 1) = (byte)value;
			outputBuffer = (byte)(value >> 16);
		}
	}
}
