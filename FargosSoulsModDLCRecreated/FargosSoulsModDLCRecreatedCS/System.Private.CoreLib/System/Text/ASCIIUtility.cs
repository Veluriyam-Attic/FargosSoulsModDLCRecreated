using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200035A RID: 858
	internal static class ASCIIUtility
	{
		// Token: 0x06002D42 RID: 11586 RVA: 0x0015835A File Offset: 0x0015755A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool AllBytesInUInt64AreAscii(ulong value)
		{
			return (value & 9259542123273814144UL) == 0UL;
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x0015836B File Offset: 0x0015756B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool AllCharsInUInt32AreAscii(uint value)
		{
			return (value & 4286644096U) == 0U;
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x00158377 File Offset: 0x00157577
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool AllCharsInUInt64AreAscii(ulong value)
		{
			return (value & 18410996206198128512UL) == 0UL;
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x00158388 File Offset: 0x00157588
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static int GetIndexOfFirstNonAsciiByteInLane_AdvSimd(Vector128<byte> value, Vector128<byte> bitmask)
		{
			if (!AdvSimd.Arm64.IsSupported || !BitConverter.IsLittleEndian)
			{
				throw new PlatformNotSupportedException();
			}
			Vector128<byte> left = AdvSimd.ShiftRightArithmetic(value.AsSByte<byte>(), 7).AsByte<sbyte>();
			Vector128<byte> vector = AdvSimd.And(left, bitmask);
			vector = AdvSimd.Arm64.AddPairwise(vector, vector);
			ulong value2 = vector.AsUInt64<byte>().ToScalar<ulong>();
			return BitOperations.TrailingZeroCount(value2) >> 2;
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x001583E1 File Offset: 0x001575E1
		private static bool FirstCharInUInt32IsAscii(uint value)
		{
			return (BitConverter.IsLittleEndian && (value & 65408U) == 0U) || (!BitConverter.IsLittleEndian && (value & 4286578688U) == 0U);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x00158408 File Offset: 0x00157608
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		public unsafe static UIntPtr GetIndexOfFirstNonAsciiByte(byte* pBuffer, [NativeInteger] UIntPtr bufferLength)
		{
			if (!Sse2.IsSupported && (!AdvSimd.Arm64.IsSupported || !BitConverter.IsLittleEndian))
			{
				return ASCIIUtility.GetIndexOfFirstNonAsciiByte_Default(pBuffer, bufferLength);
			}
			return ASCIIUtility.GetIndexOfFirstNonAsciiByte_Intrinsified(pBuffer, bufferLength);
		}

		// Token: 0x06002D48 RID: 11592 RVA: 0x00158430 File Offset: 0x00157630
		[return: NativeInteger]
		private unsafe static UIntPtr GetIndexOfFirstNonAsciiByte_Default(byte* pBuffer, [NativeInteger] UIntPtr bufferLength)
		{
			byte* ptr = pBuffer;
			if (Vector.IsHardwareAccelerated && bufferLength >= (UIntPtr)(2 * Vector<sbyte>.Count))
			{
				uint count = (uint)Vector<sbyte>.Count;
				if (Vector.GreaterThanOrEqualAll<sbyte>(Unsafe.ReadUnaligned<Vector<sbyte>>((void*)pBuffer), Vector<sbyte>.Zero))
				{
					byte* ptr2 = pBuffer + (ulong)bufferLength - count;
					pBuffer = (pBuffer + count & ~(count - 1U));
					while (!Vector.LessThanAny<sbyte>(Unsafe.Read<Vector<sbyte>>((void*)pBuffer), Vector<sbyte>.Zero))
					{
						pBuffer += count;
						if (pBuffer != ptr2)
						{
							break;
						}
					}
					bufferLength -= pBuffer;
					bufferLength += ptr;
				}
			}
			while (bufferLength >= (UIntPtr)((IntPtr)8))
			{
				uint num = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
				uint num2 = Unsafe.ReadUnaligned<uint>((void*)(pBuffer + 4));
				if (!ASCIIUtility.AllBytesInUInt32AreAscii(num | num2))
				{
					if (ASCIIUtility.AllBytesInUInt32AreAscii(num))
					{
						num = num2;
						pBuffer += 4;
					}
					IL_F9:
					pBuffer += ASCIIUtility.CountNumberOfLeadingAsciiBytesFromUInt32WithSomeNonAsciiData(num);
					IL_F3:
					return (UIntPtr)(pBuffer - ptr);
				}
				pBuffer += 8;
				bufferLength -= (UIntPtr)((IntPtr)8);
			}
			if ((bufferLength & (UIntPtr)((IntPtr)4)) != 0)
			{
				uint num = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
				if (!ASCIIUtility.AllBytesInUInt32AreAscii(num))
				{
					goto IL_F9;
				}
				pBuffer += 4;
			}
			if ((bufferLength & (UIntPtr)((IntPtr)2)) != 0)
			{
				uint num = (uint)Unsafe.ReadUnaligned<ushort>((void*)pBuffer);
				if (!ASCIIUtility.AllBytesInUInt32AreAscii(num))
				{
					goto IL_F9;
				}
				pBuffer += 2;
			}
			if ((bufferLength & (UIntPtr)((IntPtr)1)) != 0 && *(sbyte*)pBuffer >= 0)
			{
				pBuffer++;
				goto IL_F3;
			}
			goto IL_F3;
		}

		// Token: 0x06002D49 RID: 11593 RVA: 0x000CB6A3 File Offset: 0x000CA8A3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ContainsNonAsciiByte_Sse2(uint sseMask)
		{
			return sseMask > 0U;
		}

		// Token: 0x06002D4A RID: 11594 RVA: 0x00158542 File Offset: 0x00157742
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool ContainsNonAsciiByte_AdvSimd(uint advSimdIndex)
		{
			return advSimdIndex < 16U;
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x0015854C File Offset: 0x0015774C
		[return: NativeInteger]
		private unsafe static UIntPtr GetIndexOfFirstNonAsciiByte_Intrinsified(byte* pBuffer, [NativeInteger] UIntPtr bufferLength)
		{
			uint num = (uint)Unsafe.SizeOf<Vector128<byte>>();
			UIntPtr uintPtr = (UIntPtr)(num - 1U);
			Vector128<byte> bitmask = BitConverter.IsLittleEndian ? Vector128.Create(4097).AsByte<ushort>() : Vector128.Create(272).AsByte<ushort>();
			uint num2 = uint.MaxValue;
			uint num3 = uint.MaxValue;
			uint num4 = uint.MaxValue;
			uint num5 = uint.MaxValue;
			byte* ptr = pBuffer;
			if (bufferLength >= (UIntPtr)num)
			{
				if (Sse2.IsSupported)
				{
					num2 = (uint)Sse2.MoveMask(Sse2.LoadVector128(pBuffer));
					if (ASCIIUtility.ContainsNonAsciiByte_Sse2(num2))
					{
						goto IL_24F;
					}
				}
				else
				{
					if (!AdvSimd.Arm64.IsSupported)
					{
						throw new PlatformNotSupportedException();
					}
					num4 = (uint)ASCIIUtility.GetIndexOfFirstNonAsciiByteInLane_AdvSimd(AdvSimd.LoadVector128(pBuffer), bitmask);
					if (ASCIIUtility.ContainsNonAsciiByte_AdvSimd(num4))
					{
						goto IL_24F;
					}
				}
				if (bufferLength >= (UIntPtr)(2U * num))
				{
					pBuffer = (pBuffer + num & ~uintPtr);
					bufferLength += ptr;
					bufferLength -= pBuffer;
					if (bufferLength >= (UIntPtr)(2U * num))
					{
						byte* ptr2 = pBuffer + bufferLength - 2U * num;
						for (;;)
						{
							if (Sse2.IsSupported)
							{
								Vector128<byte> value = Sse2.LoadAlignedVector128(pBuffer);
								Vector128<byte> value2 = Sse2.LoadAlignedVector128(pBuffer + num);
								num2 = (uint)Sse2.MoveMask(value);
								num3 = (uint)Sse2.MoveMask(value2);
								if (ASCIIUtility.ContainsNonAsciiByte_Sse2(num2 | num3))
								{
									break;
								}
							}
							else
							{
								if (!AdvSimd.Arm64.IsSupported)
								{
									goto IL_147;
								}
								Vector128<byte> value3 = AdvSimd.LoadVector128(pBuffer);
								Vector128<byte> value4 = AdvSimd.LoadVector128(pBuffer + num);
								num4 = (uint)ASCIIUtility.GetIndexOfFirstNonAsciiByteInLane_AdvSimd(value3, bitmask);
								num5 = (uint)ASCIIUtility.GetIndexOfFirstNonAsciiByteInLane_AdvSimd(value4, bitmask);
								if (ASCIIUtility.ContainsNonAsciiByte_AdvSimd(num4))
								{
									break;
								}
								if (ASCIIUtility.ContainsNonAsciiByte_AdvSimd(num5))
								{
									break;
								}
							}
							pBuffer += 2U * num;
							if (pBuffer != ptr2)
							{
								goto IL_15D;
							}
						}
						goto IL_213;
						IL_147:
						throw new PlatformNotSupportedException();
						IL_213:
						if (Sse2.IsSupported)
						{
							if (!ASCIIUtility.ContainsNonAsciiByte_Sse2(num2))
							{
								pBuffer += num;
								num2 = num3;
								goto IL_24F;
							}
							goto IL_24F;
						}
						else
						{
							if (!AdvSimd.IsSupported)
							{
								throw new PlatformNotSupportedException();
							}
							if (!ASCIIUtility.ContainsNonAsciiByte_AdvSimd(num4))
							{
								pBuffer += num;
								num4 = num5;
								goto IL_24F;
							}
							goto IL_24F;
						}
					}
					IL_15D:
					if ((bufferLength & (UIntPtr)num) == 0)
					{
						goto IL_1B2;
					}
					if (Sse2.IsSupported)
					{
						num2 = (uint)Sse2.MoveMask(Sse2.LoadAlignedVector128(pBuffer));
						if (ASCIIUtility.ContainsNonAsciiByte_Sse2(num2))
						{
							goto IL_24F;
						}
					}
					else
					{
						if (!AdvSimd.Arm64.IsSupported)
						{
							throw new PlatformNotSupportedException();
						}
						num4 = (uint)ASCIIUtility.GetIndexOfFirstNonAsciiByteInLane_AdvSimd(AdvSimd.LoadVector128(pBuffer), bitmask);
						if (ASCIIUtility.ContainsNonAsciiByte_AdvSimd(num4))
						{
							goto IL_24F;
						}
					}
				}
				pBuffer += num;
				IL_1B2:
				if (((UIntPtr)((byte)bufferLength) & uintPtr) != 0)
				{
					pBuffer += (ulong)((bufferLength & uintPtr) - (UIntPtr)num);
					if (Sse2.IsSupported)
					{
						num2 = (uint)Sse2.MoveMask(Sse2.LoadVector128(pBuffer));
						if (ASCIIUtility.ContainsNonAsciiByte_Sse2(num2))
						{
							goto IL_24F;
						}
					}
					else
					{
						if (!AdvSimd.Arm64.IsSupported)
						{
							throw new PlatformNotSupportedException();
						}
						num4 = (uint)ASCIIUtility.GetIndexOfFirstNonAsciiByteInLane_AdvSimd(AdvSimd.LoadVector128(pBuffer), bitmask);
						if (ASCIIUtility.ContainsNonAsciiByte_AdvSimd(num4))
						{
							goto IL_24F;
						}
					}
					pBuffer += num;
					goto IL_20E;
				}
				goto IL_20E;
				IL_24F:
				if (Sse2.IsSupported)
				{
					pBuffer += BitOperations.TrailingZeroCount(num2);
				}
				else
				{
					if (!AdvSimd.Arm64.IsSupported)
					{
						throw new PlatformNotSupportedException();
					}
					pBuffer += num4;
				}
			}
			else
			{
				if ((bufferLength & (UIntPtr)((IntPtr)8)) != 0)
				{
					int size = UIntPtr.Size;
					ulong num6 = Unsafe.ReadUnaligned<ulong>((void*)pBuffer);
					if (!ASCIIUtility.AllBytesInUInt64AreAscii(num6))
					{
						num6 &= 9259542123273814144UL;
						pBuffer += (ulong)((IntPtr)(BitOperations.TrailingZeroCount(num6) >> 3));
						goto IL_20E;
					}
					pBuffer += 8;
				}
				if ((bufferLength & (UIntPtr)((IntPtr)4)) != 0)
				{
					uint num7 = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
					if (!ASCIIUtility.AllBytesInUInt32AreAscii(num7))
					{
						pBuffer += ASCIIUtility.CountNumberOfLeadingAsciiBytesFromUInt32WithSomeNonAsciiData(num7);
						goto IL_20E;
					}
					pBuffer += 4;
				}
				if ((bufferLength & (UIntPtr)((IntPtr)2)) != 0)
				{
					uint num7 = (uint)Unsafe.ReadUnaligned<ushort>((void*)pBuffer);
					if (!ASCIIUtility.AllBytesInUInt32AreAscii(num7))
					{
						pBuffer += (ulong)((UIntPtr)((IntPtr)((sbyte)num7) >> 7) + (UIntPtr)((IntPtr)1));
						goto IL_20E;
					}
					pBuffer += 2;
				}
				if ((bufferLength & (UIntPtr)((IntPtr)1)) != 0 && *(sbyte*)pBuffer >= 0)
				{
					pBuffer++;
				}
			}
			IL_20E:
			return (UIntPtr)(pBuffer - ptr);
		}

		// Token: 0x06002D4C RID: 11596 RVA: 0x0015888F File Offset: 0x00157A8F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NativeInteger]
		public unsafe static UIntPtr GetIndexOfFirstNonAsciiChar(char* pBuffer, [NativeInteger] UIntPtr bufferLength)
		{
			if (!Sse2.IsSupported)
			{
				return ASCIIUtility.GetIndexOfFirstNonAsciiChar_Default(pBuffer, bufferLength);
			}
			return ASCIIUtility.GetIndexOfFirstNonAsciiChar_Sse2(pBuffer, bufferLength);
		}

		// Token: 0x06002D4D RID: 11597 RVA: 0x001588A8 File Offset: 0x00157AA8
		[return: NativeInteger]
		private unsafe static UIntPtr GetIndexOfFirstNonAsciiChar_Default(char* pBuffer, [NativeInteger] UIntPtr bufferLength)
		{
			char* ptr = pBuffer;
			if (Vector.IsHardwareAccelerated && bufferLength >= (UIntPtr)(2 * Vector<ushort>.Count))
			{
				uint count = (uint)Vector<ushort>.Count;
				uint count2 = (uint)Vector<byte>.Count;
				Vector<ushort> right = new Vector<ushort>(127);
				if (Vector.LessThanOrEqualAll<ushort>(Unsafe.ReadUnaligned<Vector<ushort>>((void*)pBuffer), right))
				{
					char* ptr2 = pBuffer + (ulong)bufferLength * 2UL / 2UL - (ulong)count * 2UL / 2UL;
					pBuffer = (pBuffer + count2 / 2U & ~(count2 - 1U));
					while (!Vector.GreaterThanAny<ushort>(Unsafe.Read<Vector<ushort>>((void*)pBuffer), right))
					{
						pBuffer += (ulong)count * 2UL / 2UL;
						if (pBuffer != ptr2)
						{
							break;
						}
					}
					bufferLength -= (UIntPtr)((pBuffer - ptr) / (IntPtr)2);
				}
			}
			while (bufferLength >= (UIntPtr)((IntPtr)4))
			{
				uint num = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
				uint num2 = Unsafe.ReadUnaligned<uint>((void*)(pBuffer + 2));
				if (!ASCIIUtility.AllCharsInUInt32AreAscii(num | num2))
				{
					if (ASCIIUtility.AllCharsInUInt32AreAscii(num))
					{
						num = num2;
						pBuffer += 2;
					}
					IL_109:
					if (ASCIIUtility.FirstCharInUInt32IsAscii(num))
					{
						pBuffer++;
					}
					IL_100:
					UIntPtr uintPtr = (UIntPtr)(pBuffer - ptr);
					return uintPtr / (UIntPtr)((IntPtr)2);
				}
				pBuffer += 4;
				bufferLength -= (UIntPtr)((IntPtr)4);
			}
			if ((bufferLength & (UIntPtr)((IntPtr)2)) != 0)
			{
				uint num = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
				if (!ASCIIUtility.AllCharsInUInt32AreAscii(num))
				{
					goto IL_109;
				}
				pBuffer += 2;
			}
			if ((bufferLength & (UIntPtr)((IntPtr)1)) != 0 && *pBuffer <= '\u007f')
			{
				pBuffer++;
				goto IL_100;
			}
			goto IL_100;
		}

		// Token: 0x06002D4E RID: 11598 RVA: 0x001589CC File Offset: 0x00157BCC
		[return: NativeInteger]
		private unsafe static UIntPtr GetIndexOfFirstNonAsciiChar_Sse2(char* pBuffer, [NativeInteger] UIntPtr bufferLength)
		{
			if (bufferLength == 0)
			{
				return (UIntPtr)((IntPtr)0);
			}
			uint num = (uint)Unsafe.SizeOf<Vector128<byte>>();
			uint num2 = num / 2U;
			char* ptr = pBuffer;
			if (bufferLength >= (UIntPtr)num2)
			{
				Vector128<ushort> right = Vector128.Create(65408);
				Vector128<ushort> right2 = Vector128.Create(32640);
				Vector128<ushort> left = Sse2.LoadVector128((ushort*)pBuffer);
				uint num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
				if ((num3 & 43690U) == 0U)
				{
					bufferLength <<= 1;
					if (bufferLength >= (UIntPtr)(2U * num))
					{
						pBuffer = (pBuffer + num / 2U & ~(num - 1U));
						bufferLength = bufferLength / 2 + ptr;
						bufferLength -= pBuffer;
						if (bufferLength >= (UIntPtr)(2U * num))
						{
							char* ptr2 = pBuffer + bufferLength / 2 - 2U * num / 2U;
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
									goto IL_FA;
								}
							}
							if (Sse41.IsSupported)
							{
								if (!Sse41.TestZ(left, right))
								{
									goto IL_1E6;
								}
							}
							else
							{
								num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
								if ((num3 & 43690U) != 0U)
								{
									goto IL_1FA;
								}
							}
							pBuffer += (ulong)num2 * 2UL / 2UL;
							left = vector;
							goto IL_1E6;
						}
						IL_FA:
						if ((bufferLength & (UIntPtr)num) == 0)
						{
							goto IL_148;
						}
						left = Sse2.LoadAlignedVector128((ushort*)pBuffer);
						if (Sse41.IsSupported)
						{
							if (!Sse41.TestZ(left, right))
							{
								goto IL_1E6;
							}
						}
						else
						{
							num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
							if ((num3 & 43690U) != 0U)
							{
								goto IL_1FA;
							}
						}
					}
					pBuffer += (ulong)num2 * 2UL / 2UL;
					IL_148:
					if (((uint)((byte)bufferLength) & num - 1U) != 0U)
					{
						pBuffer = pBuffer + (ulong)(bufferLength & (UIntPtr)(num - 1U)) / 2UL - num / 2U;
						left = Sse2.LoadVector128((ushort*)pBuffer);
						if (Sse41.IsSupported)
						{
							if (!Sse41.TestZ(left, right))
							{
								goto IL_1E6;
							}
						}
						else
						{
							num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
							if ((num3 & 43690U) != 0U)
							{
								goto IL_1FA;
							}
						}
						pBuffer += (ulong)num2 * 2UL / 2UL;
						goto IL_1A1;
					}
					goto IL_1A1;
					IL_1E6:
					num3 = (uint)Sse2.MoveMask(Sse2.AddSaturate(left, right2).AsByte<ushort>());
				}
				IL_1FA:
				num3 &= 43690U;
				pBuffer = (char*)((byte*)(pBuffer + BitOperations.TrailingZeroCount(num3) / 2) - 1);
			}
			else
			{
				if ((bufferLength & (UIntPtr)((IntPtr)4)) != 0)
				{
					int size = UIntPtr.Size;
					ulong num4 = Unsafe.ReadUnaligned<ulong>((void*)pBuffer);
					if (!ASCIIUtility.AllCharsInUInt64AreAscii(num4))
					{
						num4 &= 18410996206198128512UL;
						pBuffer += (ulong)((IntPtr)(BitOperations.TrailingZeroCount(num4) >> 3) & ~(IntPtr)1) / 2UL;
						goto IL_1A1;
					}
					pBuffer += 4;
				}
				if ((bufferLength & (UIntPtr)((IntPtr)2)) != 0)
				{
					uint value = Unsafe.ReadUnaligned<uint>((void*)pBuffer);
					if (ASCIIUtility.AllCharsInUInt32AreAscii(value))
					{
						pBuffer += 2;
					}
					else
					{
						if (ASCIIUtility.FirstCharInUInt32IsAscii(value))
						{
							pBuffer++;
							goto IL_1A1;
						}
						goto IL_1A1;
					}
				}
				if ((bufferLength & (UIntPtr)((IntPtr)1)) != 0 && *pBuffer <= '\u007f')
				{
					pBuffer++;
				}
			}
			IL_1A1:
			return (UIntPtr)((pBuffer - ptr) / (IntPtr)2);
		}

		// Token: 0x06002D4F RID: 11599 RVA: 0x00158C8C File Offset: 0x00157E8C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void NarrowFourUtf16CharsToAsciiAndWriteToBuffer(ref byte outputBuffer, ulong value)
		{
			if (Sse2.X64.IsSupported)
			{
				Vector128<short> vector = Sse2.X64.ConvertScalarToVector128UInt64(value).AsInt16<ulong>();
				Vector128<uint> value2 = Sse2.PackUnsignedSaturate(vector, vector).AsUInt32<byte>();
				Unsafe.WriteUnaligned<uint>(ref outputBuffer, Sse2.ConvertToUInt32(value2));
				return;
			}
			if (AdvSimd.IsSupported)
			{
				Vector128<short> value3 = Vector128.CreateScalarUnsafe(value).AsInt16<ulong>();
				Vector64<byte> vector2 = AdvSimd.ExtractNarrowingSaturateUnsignedLower(value3);
				Unsafe.WriteUnaligned<uint>(ref outputBuffer, vector2.AsUInt32<byte>().ToScalar<uint>());
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

		// Token: 0x06002D50 RID: 11600 RVA: 0x00158D62 File Offset: 0x00157F62
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void NarrowTwoUtf16CharsToAsciiAndWriteToBuffer(ref byte outputBuffer, uint value)
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

		// Token: 0x06002D51 RID: 11601 RVA: 0x00158D90 File Offset: 0x00157F90
		[return: NativeInteger]
		public unsafe static UIntPtr NarrowUtf16ToAscii(char* pUtf16Buffer, byte* pAsciiBuffer, [NativeInteger] UIntPtr elementCount)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			ulong num;
			if (Sse2.IsSupported)
			{
				if (elementCount >= (UIntPtr)(2 * Unsafe.SizeOf<Vector128<byte>>()))
				{
					int size = IntPtr.Size;
					num = Unsafe.ReadUnaligned<ulong>((void*)pUtf16Buffer);
					if (!ASCIIUtility.AllCharsInUInt64AreAscii(num))
					{
						goto IL_18B;
					}
					uintPtr = ASCIIUtility.NarrowUtf16ToAscii_Sse2(pUtf16Buffer, pAsciiBuffer, elementCount);
				}
			}
			else if (Vector.IsHardwareAccelerated)
			{
				uint num2 = (uint)Unsafe.SizeOf<Vector<byte>>();
				if (elementCount >= (UIntPtr)(2U * num2))
				{
					int size2 = IntPtr.Size;
					num = Unsafe.ReadUnaligned<ulong>((void*)pUtf16Buffer);
					if (!ASCIIUtility.AllCharsInUInt64AreAscii(num))
					{
						goto IL_18B;
					}
					Vector<ushort> right = new Vector<ushort>(127);
					UIntPtr uintPtr2 = elementCount - (UIntPtr)(2U * num2);
					do
					{
						Vector<ushort> vector = Unsafe.ReadUnaligned<Vector<ushort>>((void*)(pUtf16Buffer + (ulong)uintPtr * 2UL / 2UL));
						Vector<ushort> vector2 = Unsafe.ReadUnaligned<Vector<ushort>>((void*)(pUtf16Buffer + (ulong)uintPtr * 2UL / 2UL + Vector<ushort>.Count));
						if (Vector.GreaterThanAny<ushort>(Vector.BitwiseOr<ushort>(vector, vector2), right))
						{
							break;
						}
						Vector<byte> value = Vector.Narrow(vector, vector2);
						Unsafe.WriteUnaligned<Vector<byte>>((void*)(pAsciiBuffer + (ulong)uintPtr), value);
						uintPtr += (UIntPtr)num2;
					}
					while (uintPtr <= uintPtr2);
				}
			}
			UIntPtr uintPtr3 = elementCount - uintPtr;
			if (uintPtr3 >= (UIntPtr)((IntPtr)4))
			{
				UIntPtr uintPtr4 = uintPtr + uintPtr3 - (UIntPtr)((IntPtr)4);
				do
				{
					int size3 = IntPtr.Size;
					num = Unsafe.ReadUnaligned<ulong>((void*)(pUtf16Buffer + (ulong)uintPtr * 2UL / 2UL));
					if (!ASCIIUtility.AllCharsInUInt64AreAscii(num))
					{
						goto IL_18B;
					}
					ASCIIUtility.NarrowFourUtf16CharsToAsciiAndWriteToBuffer(ref pAsciiBuffer[(ulong)uintPtr], num);
					uintPtr += (UIntPtr)((IntPtr)4);
				}
				while (uintPtr <= uintPtr4);
			}
			uint num3;
			if (((uint)uintPtr3 & 2U) != 0U)
			{
				num3 = Unsafe.ReadUnaligned<uint>((void*)(pUtf16Buffer + (ulong)uintPtr * 2UL / 2UL));
				if (!ASCIIUtility.AllCharsInUInt32AreAscii(num3))
				{
					goto IL_1CF;
				}
				ASCIIUtility.NarrowTwoUtf16CharsToAsciiAndWriteToBuffer(ref pAsciiBuffer[(ulong)uintPtr], num3);
				uintPtr += (UIntPtr)((IntPtr)2);
			}
			if (((uint)uintPtr3 & 1U) != 0U)
			{
				num3 = (uint)pUtf16Buffer[(ulong)uintPtr * 2UL / 2UL];
				if (num3 <= 127U)
				{
					pAsciiBuffer[(ulong)uintPtr] = (byte)num3;
					uintPtr++;
				}
			}
			return uintPtr;
			IL_18B:
			int size4 = IntPtr.Size;
			if (BitConverter.IsLittleEndian)
			{
				num3 = (uint)num;
			}
			else
			{
				num3 = (uint)(num >> 32);
			}
			if (ASCIIUtility.AllCharsInUInt32AreAscii(num3))
			{
				ASCIIUtility.NarrowTwoUtf16CharsToAsciiAndWriteToBuffer(ref pAsciiBuffer[(ulong)uintPtr], num3);
				if (BitConverter.IsLittleEndian)
				{
					num3 = (uint)(num >> 32);
				}
				else
				{
					num3 = (uint)num;
				}
				uintPtr += (UIntPtr)((IntPtr)2);
			}
			IL_1CF:
			if (ASCIIUtility.FirstCharInUInt32IsAscii(num3))
			{
				if (!BitConverter.IsLittleEndian)
				{
					num3 >>= 16;
				}
				pAsciiBuffer[(ulong)uintPtr] = (byte)num3;
				return uintPtr + 1;
			}
			return uintPtr;
		}

		// Token: 0x06002D52 RID: 11602 RVA: 0x00158F90 File Offset: 0x00158190
		[return: NativeInteger]
		private unsafe static UIntPtr NarrowUtf16ToAscii_Sse2(char* pUtf16Buffer, byte* pAsciiBuffer, [NativeInteger] UIntPtr elementCount)
		{
			uint num = (uint)Unsafe.SizeOf<Vector128<byte>>();
			UIntPtr uintPtr = (UIntPtr)(num - 1U);
			Vector128<short> right = Vector128.Create(-128);
			Vector128<ushort> right2 = Vector128.Create(32640);
			Vector128<short> vector = Sse2.LoadVector128((short*)pUtf16Buffer);
			if (Sse41.IsSupported)
			{
				if (!Sse41.TestZ(vector, right))
				{
					return (UIntPtr)((IntPtr)0);
				}
			}
			else if ((Sse2.MoveMask(Sse2.AddSaturate(vector.AsUInt16<short>(), right2).AsByte<ushort>()) & 43690) != 0)
			{
				return (UIntPtr)((IntPtr)0);
			}
			Vector128<byte> vector2 = Sse2.PackUnsignedSaturate(vector, vector);
			Sse2.StoreScalar((ulong*)pAsciiBuffer, vector2.AsUInt64<byte>());
			UIntPtr uintPtr2 = (UIntPtr)(num / 2U);
			if ((pAsciiBuffer & num / 2U) == 0U)
			{
				vector = Sse2.LoadVector128((short*)(pUtf16Buffer + (ulong)uintPtr2 * 2UL / 2UL));
				if (Sse41.IsSupported)
				{
					if (!Sse41.TestZ(vector, right))
					{
						return uintPtr2;
					}
				}
				else if ((Sse2.MoveMask(Sse2.AddSaturate(vector.AsUInt16<short>(), right2).AsByte<ushort>()) & 43690) != 0)
				{
					return uintPtr2;
				}
				vector2 = Sse2.PackUnsignedSaturate(vector, vector);
				Sse2.StoreScalar((ulong*)(pAsciiBuffer + (ulong)uintPtr2), vector2.AsUInt64<byte>());
			}
			uintPtr2 = num - (pAsciiBuffer & uintPtr);
			UIntPtr uintPtr3 = elementCount - (UIntPtr)num;
			for (;;)
			{
				vector = Sse2.LoadVector128((short*)(pUtf16Buffer + (ulong)uintPtr2 * 2UL / 2UL));
				Vector128<short> right3 = Sse2.LoadVector128((short*)(pUtf16Buffer + (ulong)uintPtr2 * 2UL / 2UL + (ulong)(num / 2U) * 2UL / 2UL));
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
				Sse2.StoreAligned(pAsciiBuffer + (ulong)uintPtr2, vector2);
				uintPtr2 += (UIntPtr)num;
				if (uintPtr2 > uintPtr3)
				{
					return uintPtr2;
				}
			}
			if (Sse41.IsSupported)
			{
				if (!Sse41.TestZ(vector, right))
				{
					return uintPtr2;
				}
			}
			else if ((Sse2.MoveMask(Sse2.AddSaturate(vector.AsUInt16<short>(), right2).AsByte<ushort>()) & 43690) != 0)
			{
				return uintPtr2;
			}
			vector2 = Sse2.PackUnsignedSaturate(vector, vector);
			Sse2.StoreScalar((ulong*)(pAsciiBuffer + (ulong)uintPtr2), vector2.AsUInt64<byte>());
			uintPtr2 += (UIntPtr)(num / 2U);
			return uintPtr2;
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x0015917C File Offset: 0x0015837C
		[return: NativeInteger]
		public unsafe static UIntPtr WidenAsciiToUtf16(byte* pAsciiBuffer, char* pUtf16Buffer, [NativeInteger] UIntPtr elementCount)
		{
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			if (BitConverter.IsLittleEndian && (Sse2.IsSupported || AdvSimd.Arm64.IsSupported))
			{
				if (elementCount >= (UIntPtr)(2 * Unsafe.SizeOf<Vector128<byte>>()))
				{
					uintPtr = ASCIIUtility.WidenAsciiToUtf16_Intrinsified(pAsciiBuffer, pUtf16Buffer, elementCount);
				}
			}
			else if (Vector.IsHardwareAccelerated)
			{
				uint num = (uint)Unsafe.SizeOf<Vector<byte>>();
				if (elementCount >= (UIntPtr)num)
				{
					UIntPtr uintPtr2 = elementCount - (UIntPtr)num;
					do
					{
						Vector<sbyte> vector = Unsafe.ReadUnaligned<Vector<sbyte>>((void*)(pAsciiBuffer + (ulong)uintPtr));
						if (Vector.LessThanAny<sbyte>(vector, Vector<sbyte>.Zero))
						{
							break;
						}
						Vector<ushort> value;
						Vector<ushort> value2;
						Vector.Widen(Vector.AsVectorByte<sbyte>(vector), out value, out value2);
						Unsafe.WriteUnaligned<Vector<ushort>>((void*)(pUtf16Buffer + (ulong)uintPtr * 2UL / 2UL), value);
						Unsafe.WriteUnaligned<Vector<ushort>>((void*)(pUtf16Buffer + (ulong)uintPtr * 2UL / 2UL + Vector<ushort>.Count), value2);
						uintPtr += (UIntPtr)num;
					}
					while (uintPtr <= uintPtr2);
				}
			}
			UIntPtr uintPtr3 = elementCount - uintPtr;
			if (uintPtr3 < (UIntPtr)((IntPtr)4))
			{
				goto IL_E0;
			}
			UIntPtr uintPtr4 = uintPtr + uintPtr3 - (UIntPtr)((IntPtr)4);
			uint num2;
			for (;;)
			{
				num2 = Unsafe.ReadUnaligned<uint>((void*)(pAsciiBuffer + (ulong)uintPtr));
				if (!ASCIIUtility.AllBytesInUInt32AreAscii(num2))
				{
					break;
				}
				ASCIIUtility.WidenFourAsciiBytesToUtf16AndWriteToBuffer(ref pUtf16Buffer[(ulong)uintPtr * 2UL / 2UL], num2);
				uintPtr += (UIntPtr)((IntPtr)4);
				if (uintPtr > uintPtr4)
				{
					goto IL_E0;
				}
			}
			IL_178:
			while (((byte)num2 & 128) == 0)
			{
				pUtf16Buffer[(ulong)uintPtr * 2UL / 2UL] = (char)((byte)num2);
				uintPtr++;
				num2 >>= 8;
			}
			return uintPtr;
			IL_E0:
			if (((uint)uintPtr3 & 2U) != 0U)
			{
				num2 = (uint)Unsafe.ReadUnaligned<ushort>((void*)(pAsciiBuffer + (ulong)uintPtr));
				if (!ASCIIUtility.AllBytesInUInt32AreAscii(num2))
				{
					goto IL_178;
				}
				if (BitConverter.IsLittleEndian)
				{
					pUtf16Buffer[(ulong)uintPtr * 2UL / 2UL] = (char)((byte)num2);
					pUtf16Buffer[(ulong)(uintPtr + (UIntPtr)((IntPtr)1)) * 2UL / 2UL] = (char)(num2 >> 8);
				}
				else
				{
					pUtf16Buffer[(ulong)(uintPtr + (UIntPtr)((IntPtr)1)) * 2UL / 2UL] = (char)((byte)num2);
					pUtf16Buffer[(ulong)uintPtr * 2UL / 2UL] = (char)(num2 >> 8);
				}
				uintPtr += (UIntPtr)((IntPtr)2);
			}
			if (((uint)uintPtr3 & 1U) != 0U)
			{
				num2 = (uint)pAsciiBuffer[(ulong)uintPtr];
				if (((byte)num2 & 128) == 0)
				{
					pUtf16Buffer[(ulong)uintPtr * 2UL / 2UL] = (char)num2;
					uintPtr++;
				}
			}
			return uintPtr;
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x0015930C File Offset: 0x0015850C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool ContainsNonAsciiByte(Vector128<byte> value)
		{
			if (!AdvSimd.Arm64.IsSupported)
			{
				throw new PlatformNotSupportedException();
			}
			value = AdvSimd.Arm64.MaxPairwise(value, value);
			return (value.AsUInt64<byte>().ToScalar<ulong>() & 9259542123273814144UL) > 0UL;
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x00159340 File Offset: 0x00158540
		[return: NativeInteger]
		private unsafe static UIntPtr WidenAsciiToUtf16_Intrinsified(byte* pAsciiBuffer, char* pUtf16Buffer, [NativeInteger] UIntPtr elementCount)
		{
			uint num = (uint)Unsafe.SizeOf<Vector128<byte>>();
			UIntPtr uintPtr = (UIntPtr)(num - 1U);
			Vector128<byte> vector;
			bool flag;
			if (Sse2.IsSupported)
			{
				vector = Sse2.LoadVector128(pAsciiBuffer);
				flag = (Sse2.MoveMask(vector) != 0);
			}
			else
			{
				if (!AdvSimd.Arm64.IsSupported)
				{
					throw new PlatformNotSupportedException();
				}
				vector = AdvSimd.LoadVector128(pAsciiBuffer);
				flag = ASCIIUtility.ContainsNonAsciiByte(vector);
			}
			if (flag)
			{
				return (UIntPtr)((IntPtr)0);
			}
			Vector128<byte> zero = Vector128<byte>.Zero;
			if (Sse2.IsSupported)
			{
				Vector128<byte> source = Sse2.UnpackLow(vector, zero);
				Sse2.Store((byte*)pUtf16Buffer, source);
			}
			else
			{
				if (!AdvSimd.IsSupported)
				{
					throw new PlatformNotSupportedException();
				}
				Vector128<byte> source = AdvSimd.ZeroExtendWideningLower(vector.GetLower<byte>()).AsByte<ushort>();
				AdvSimd.Store((byte*)pUtf16Buffer, source);
			}
			UIntPtr uintPtr2 = (num >> 1) - (pUtf16Buffer >> 1 & uintPtr >> 1);
			UIntPtr uintPtr3 = elementCount - (UIntPtr)num;
			char* ptr = pUtf16Buffer + (ulong)uintPtr2 * 2UL / 2UL;
			for (;;)
			{
				if (Sse2.IsSupported)
				{
					vector = Sse2.LoadVector128(pAsciiBuffer + (ulong)uintPtr2);
					flag = (Sse2.MoveMask(vector) != 0);
				}
				else
				{
					if (!AdvSimd.Arm64.IsSupported)
					{
						break;
					}
					vector = AdvSimd.LoadVector128(pAsciiBuffer + (ulong)uintPtr2);
					flag = ASCIIUtility.ContainsNonAsciiByte(vector);
				}
				if (flag)
				{
					goto IL_177;
				}
				if (Sse2.IsSupported)
				{
					Vector128<byte> source2 = Sse2.UnpackLow(vector, zero);
					Sse2.StoreAligned((byte*)ptr, source2);
					Vector128<byte> source3 = Sse2.UnpackHigh(vector, zero);
					Sse2.StoreAligned((byte*)(ptr + num / 2U), source3);
				}
				else
				{
					if (!AdvSimd.Arm64.IsSupported)
					{
						goto IL_153;
					}
					Vector128<ushort> value = AdvSimd.ZeroExtendWideningLower(vector.GetLower<byte>());
					Vector128<ushort> value2 = AdvSimd.ZeroExtendWideningUpper(vector);
					AdvSimd.Arm64.StorePair((ushort*)ptr, value, value2);
				}
				uintPtr2 += (UIntPtr)num;
				ptr += (ulong)num * 2UL / 2UL;
				if (uintPtr2 > uintPtr3)
				{
					return uintPtr2;
				}
			}
			throw new PlatformNotSupportedException();
			IL_153:
			throw new PlatformNotSupportedException();
			IL_177:
			if (!flag)
			{
				if (Sse2.IsSupported)
				{
					Vector128<byte> source = Sse2.UnpackLow(vector, zero);
					Sse2.StoreAligned((byte*)(pUtf16Buffer + (ulong)uintPtr2 * 2UL / 2UL), source);
				}
				else
				{
					if (!AdvSimd.Arm64.IsSupported)
					{
						throw new PlatformNotSupportedException();
					}
					Vector128<ushort> source4 = AdvSimd.ZeroExtendWideningLower(vector.GetLower<byte>());
					AdvSimd.Store((ushort*)(pUtf16Buffer + (ulong)uintPtr2 * 2UL / 2UL), source4);
				}
				return uintPtr2 + (UIntPtr)(num / 2U);
			}
			return uintPtr2;
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x00159520 File Offset: 0x00158720
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static void WidenFourAsciiBytesToUtf16AndWriteToBuffer(ref char outputBuffer, uint value)
		{
			if (Sse2.X64.IsSupported)
			{
				Vector128<byte> left = Sse2.ConvertScalarToVector128UInt32(value).AsByte<uint>();
				Vector128<ulong> value2 = Sse2.UnpackLow(left, Vector128<byte>.Zero).AsUInt64<byte>();
				Unsafe.WriteUnaligned<ulong>(Unsafe.As<char, byte>(ref outputBuffer), Sse2.X64.ConvertToUInt64(value2));
				return;
			}
			if (AdvSimd.Arm64.IsSupported)
			{
				Vector128<byte> left2 = AdvSimd.DuplicateToVector128(value).AsByte<uint>();
				Vector128<ulong> vector = AdvSimd.Arm64.ZipLow(left2, Vector128<byte>.Zero).AsUInt64<byte>();
				Unsafe.WriteUnaligned<ulong>(Unsafe.As<char, byte>(ref outputBuffer), vector.ToScalar<ulong>());
				return;
			}
			if (BitConverter.IsLittleEndian)
			{
				outputBuffer = (char)((byte)value);
				value >>= 8;
				*Unsafe.Add<char>(ref outputBuffer, 1) = (char)((byte)value);
				value >>= 8;
				*Unsafe.Add<char>(ref outputBuffer, 2) = (char)((byte)value);
				value >>= 8;
				*Unsafe.Add<char>(ref outputBuffer, 3) = (char)value;
				return;
			}
			*Unsafe.Add<char>(ref outputBuffer, 3) = (char)((byte)value);
			value >>= 8;
			*Unsafe.Add<char>(ref outputBuffer, 2) = (char)((byte)value);
			value >>= 8;
			*Unsafe.Add<char>(ref outputBuffer, 1) = (char)((byte)value);
			value >>= 8;
			outputBuffer = (char)value;
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x00159603 File Offset: 0x00158803
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool AllBytesInUInt32AreAscii(uint value)
		{
			return (value & 2155905152U) == 0U;
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x00159610 File Offset: 0x00158810
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static uint CountNumberOfLeadingAsciiBytesFromUInt32WithSomeNonAsciiData(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (uint)BitOperations.TrailingZeroCount(value & 2155905152U) >> 3;
			}
			value = ~value;
			value = BitOperations.RotateLeft(value, 1);
			uint num = value & 1U;
			uint num2 = num;
			value = BitOperations.RotateLeft(value, 8);
			num &= value;
			num2 += num;
			value = BitOperations.RotateLeft(value, 8);
			num &= value;
			return num2 + num;
		}
	}
}
