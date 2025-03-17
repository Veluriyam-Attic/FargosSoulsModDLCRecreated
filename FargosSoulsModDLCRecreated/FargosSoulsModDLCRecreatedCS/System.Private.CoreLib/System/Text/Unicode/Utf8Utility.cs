using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System.Text.Unicode
{
	// Token: 0x02000399 RID: 921
	internal static class Utf8Utility
	{
		// Token: 0x06003070 RID: 12400 RVA: 0x00165D00 File Offset: 0x00164F00
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ExtractCharFromFirstThreeByteSequence(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (value & 4128768U) >> 16 | (value & 16128U) >> 2 | (value & 15U) << 12;
			}
			return (value & 251658240U) >> 12 | (value & 4128768U) >> 10 | (value & 16128U) >> 8;
		}

		// Token: 0x06003071 RID: 12401 RVA: 0x00165D50 File Offset: 0x00164F50
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ExtractCharFromFirstTwoByteSequence(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				uint num = (uint)((uint)((byte)value) << 6);
				return (uint)((byte)(value >> 8)) + num - 12288U - 128U;
			}
			return (uint)((ushort)((value & 520093696U) >> 18 | (value & 4128768U) >> 16));
		}

		// Token: 0x06003072 RID: 12402 RVA: 0x00165D94 File Offset: 0x00164F94
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ExtractCharsFromFourByteSequence(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				uint num = (uint)((uint)((byte)value) << 8);
				num |= (value & 16128U) >> 6;
				num |= (value & 3145728U) >> 20;
				num |= (value & 1056964608U) >> 8;
				num |= (value & 983040U) << 6;
				num -= 64U;
				num -= 8192U;
				num += 2048U;
				return num + 3690987520U;
			}
			uint num2 = value & 4278190080U;
			num2 |= (value & 4128768U) << 2;
			num2 |= (value & 12288U) << 4;
			num2 |= (value & 3840U) >> 2;
			num2 |= (value & 63U);
			num2 -= 536870912U;
			num2 -= 4194304U;
			num2 += 56320U;
			return num2 + 134217728U;
		}

		// Token: 0x06003073 RID: 12403 RVA: 0x00165E54 File Offset: 0x00165054
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ExtractFourUtf8BytesFromSurrogatePair(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value += 64U;
				uint num = BinaryPrimitives.ReverseEndianness(value & 4130560U);
				num = BitOperations.RotateLeft(num, 16);
				uint num2 = (value & 252U) << 6;
				uint num3 = value >> 6 & 983040U;
				num3 |= num2;
				uint num4 = (value & 3U) << 20;
				num4 |= 2155905264U;
				return num4 | num | num3;
			}
			value -= 3623934976U;
			value += 4194304U;
			uint num5 = value & 117440512U;
			uint num6 = value >> 2 & 4128768U;
			num6 |= num5;
			uint num7 = value << 2 & 3840U;
			uint num8 = value >> 6 & 196608U;
			num8 |= num7;
			uint num9 = (value & 63U) + 4034953344U;
			return num9 | num6 | num8;
		}

		// Token: 0x06003074 RID: 12404 RVA: 0x00165F0F File Offset: 0x0016510F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ExtractTwoCharsPackedFromTwoAdjacentTwoByteSequences(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (value & 1056980736U) >> 8 | (value & 2031647U) << 6;
			}
			return (value & 520101632U) >> 2 | (value & 4128831U);
		}

		// Token: 0x06003075 RID: 12405 RVA: 0x00165F3D File Offset: 0x0016513D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ExtractTwoUtf8TwoByteSequencesFromTwoPackedUtf16Chars(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return (value >> 6 & 2031647U) + (value << 8 & 1056980736U) + 2160099520U;
			}
			return (value << 2 & 520101632U) + (value & 4128831U) + 3229663360U;
		}

		// Token: 0x06003076 RID: 12406 RVA: 0x00165F78 File Offset: 0x00165178
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ExtractUtf8TwoByteSequenceFromFirstUtf16Char(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				uint num = value << 2 & 7936U;
				value &= 63U;
				return (uint)BinaryPrimitives.ReverseEndianness((ushort)(num + value + 49280U));
			}
			uint num2 = value >> 16 & 63U;
			value = (value >> 22 & 7936U);
			return value + num2 + 49280U;
		}

		// Token: 0x06003077 RID: 12407 RVA: 0x00165FC9 File Offset: 0x001651C9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsFirstCharAscii(uint value)
		{
			return (BitConverter.IsLittleEndian && (value & 65408U) == 0U) || (!BitConverter.IsLittleEndian && value < 8388608U);
		}

		// Token: 0x06003078 RID: 12408 RVA: 0x00165FEE File Offset: 0x001651EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsFirstCharAtLeastThreeUtf8Bytes(uint value)
		{
			return (BitConverter.IsLittleEndian && (value & 63488U) != 0U) || (!BitConverter.IsLittleEndian && value >= 134217728U);
		}

		// Token: 0x06003079 RID: 12409 RVA: 0x00166016 File Offset: 0x00165216
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsFirstCharSurrogate(uint value)
		{
			return (BitConverter.IsLittleEndian && (value - 55296U & 63488U) == 0U) || (!BitConverter.IsLittleEndian && value - 3623878656U < 134217728U);
		}

		// Token: 0x0600307A RID: 12410 RVA: 0x00166047 File Offset: 0x00165247
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsFirstCharTwoUtf8Bytes(uint value)
		{
			return (BitConverter.IsLittleEndian && (value - 128U & 65535U) < 1920U) || (!BitConverter.IsLittleEndian && UnicodeUtility.IsInRangeInclusive(value, 8388608U, 134217727U));
		}

		// Token: 0x0600307B RID: 12411 RVA: 0x0016607F File Offset: 0x0016527F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsLowByteUtf8ContinuationByte(uint value)
		{
			return (byte)(value - 128U) <= 63;
		}

		// Token: 0x0600307C RID: 12412 RVA: 0x00166090 File Offset: 0x00165290
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsSecondCharAscii(uint value)
		{
			return (BitConverter.IsLittleEndian && value < 8388608U) || (!BitConverter.IsLittleEndian && (value & 65408U) == 0U);
		}

		// Token: 0x0600307D RID: 12413 RVA: 0x001660B6 File Offset: 0x001652B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsSecondCharAtLeastThreeUtf8Bytes(uint value)
		{
			return (BitConverter.IsLittleEndian && (value & 4160749568U) != 0U) || (!BitConverter.IsLittleEndian && (value & 63488U) > 0U);
		}

		// Token: 0x0600307E RID: 12414 RVA: 0x001660DD File Offset: 0x001652DD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsSecondCharSurrogate(uint value)
		{
			return (BitConverter.IsLittleEndian && value - 3623878656U < 134217728U) || (!BitConverter.IsLittleEndian && (value - 55296U & 63488U) == 0U);
		}

		// Token: 0x0600307F RID: 12415 RVA: 0x0016610F File Offset: 0x0016530F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsSecondCharTwoUtf8Bytes(uint value)
		{
			return (BitConverter.IsLittleEndian && UnicodeUtility.IsInRangeInclusive(value, 8388608U, 134217727U)) || (!BitConverter.IsLittleEndian && (value - 128U & 65535U) < 1920U);
		}

		// Token: 0x06003080 RID: 12416 RVA: 0x00166149 File Offset: 0x00165349
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsUtf8ContinuationByte(in byte value)
		{
			return (sbyte)value < -64;
		}

		// Token: 0x06003081 RID: 12417 RVA: 0x00166152 File Offset: 0x00165352
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsWellFormedUtf16SurrogatePair(uint value)
		{
			return (BitConverter.IsLittleEndian && (value - 3691042816U & 4227922944U) == 0U) || (!BitConverter.IsLittleEndian && (value - 3623934976U & 4227922944U) == 0U);
		}

		// Token: 0x06003082 RID: 12418 RVA: 0x00166185 File Offset: 0x00165385
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint ToLittleEndian(uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return value;
			}
			return BinaryPrimitives.ReverseEndianness(value);
		}

		// Token: 0x06003083 RID: 12419 RVA: 0x00166196 File Offset: 0x00165396
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32BeginsWithOverlongUtf8TwoByteSequence(uint value)
		{
			return (BitConverter.IsLittleEndian && (byte)value < 194) || (!BitConverter.IsLittleEndian && value < 3254779904U);
		}

		// Token: 0x06003084 RID: 12420 RVA: 0x001661BB File Offset: 0x001653BB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32BeginsWithUtf8FourByteMask(uint value)
		{
			return (BitConverter.IsLittleEndian && (value - 2155905264U & 3233857784U) == 0U) || (!BitConverter.IsLittleEndian && (value - 4034953216U & 4173381824U) == 0U);
		}

		// Token: 0x06003085 RID: 12421 RVA: 0x001661EE File Offset: 0x001653EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32BeginsWithUtf8ThreeByteMask(uint value)
		{
			return (BitConverter.IsLittleEndian && (value - 8421600U & 12632304U) == 0U) || (!BitConverter.IsLittleEndian && (value - 3766517760U & 4039163904U) == 0U);
		}

		// Token: 0x06003086 RID: 12422 RVA: 0x00166221 File Offset: 0x00165421
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32BeginsWithUtf8TwoByteMask(uint value)
		{
			return (BitConverter.IsLittleEndian && (value - 32960U & 49376U) == 0U) || (!BitConverter.IsLittleEndian && (value - 3229614080U & 3770679296U) == 0U);
		}

		// Token: 0x06003087 RID: 12423 RVA: 0x00166254 File Offset: 0x00165454
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32EndsWithOverlongUtf8TwoByteSequence(uint value)
		{
			return (BitConverter.IsLittleEndian && (value & 1966080U) == 0U) || (!BitConverter.IsLittleEndian && (value & 7680U) == 0U);
		}

		// Token: 0x06003088 RID: 12424 RVA: 0x0016627B File Offset: 0x0016547B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32EndsWithUtf8TwoByteMask(uint value)
		{
			return (BitConverter.IsLittleEndian && (value - 2160066560U & 3235905536U) == 0U) || (!BitConverter.IsLittleEndian && (value - 49280U & 57536U) == 0U);
		}

		// Token: 0x06003089 RID: 12425 RVA: 0x001662AE File Offset: 0x001654AE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32BeginsWithValidUtf8TwoByteSequenceLittleEndian(uint value)
		{
			return (BitConverter.IsLittleEndian && UnicodeUtility.IsInRangeInclusive(value & 49407U, 32962U, 32991U)) || (!BitConverter.IsLittleEndian && false);
		}

		// Token: 0x0600308A RID: 12426 RVA: 0x001662DB File Offset: 0x001654DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32EndsWithValidUtf8TwoByteSequenceLittleEndian(uint value)
		{
			return (BitConverter.IsLittleEndian && UnicodeUtility.IsInRangeInclusive(value & 3237937152U, 2160197632U, 2162098176U)) || (!BitConverter.IsLittleEndian && false);
		}

		// Token: 0x0600308B RID: 12427 RVA: 0x00166308 File Offset: 0x00165508
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32FirstByteIsAscii(uint value)
		{
			return (BitConverter.IsLittleEndian && (value & 128U) == 0U) || (!BitConverter.IsLittleEndian && value >= 0U);
		}

		// Token: 0x0600308C RID: 12428 RVA: 0x0016632C File Offset: 0x0016552C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32FourthByteIsAscii(uint value)
		{
			return (BitConverter.IsLittleEndian && value >= 0U) || (!BitConverter.IsLittleEndian && (value & 128U) == 0U);
		}

		// Token: 0x0600308D RID: 12429 RVA: 0x0016634E File Offset: 0x0016554E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32SecondByteIsAscii(uint value)
		{
			return (BitConverter.IsLittleEndian && (value & 32768U) == 0U) || (!BitConverter.IsLittleEndian && (value & 8388608U) == 0U);
		}

		// Token: 0x0600308E RID: 12430 RVA: 0x00166375 File Offset: 0x00165575
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool UInt32ThirdByteIsAscii(uint value)
		{
			return (BitConverter.IsLittleEndian && (value & 8388608U) == 0U) || (!BitConverter.IsLittleEndian && (value & 32768U) == 0U);
		}

		// Token: 0x0600308F RID: 12431 RVA: 0x0016639C File Offset: 0x0016559C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteTwoUtf16CharsAsTwoUtf8ThreeByteSequences(ref byte outputBuffer, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				uint num = (value << 2 & 16128U) | (value & 63U) << 16;
				uint num2 = (value >> 4 & 251658240U) | (value >> 12 & 15U);
				Unsafe.WriteUnaligned<uint>(ref outputBuffer, num + num2 + 3766517984U);
				Unsafe.WriteUnaligned<ushort>(Unsafe.Add<byte>(ref outputBuffer, 4), (ushort)((value >> 22 & 63U) + (value >> 8 & 16128U) + 32896U));
				return;
			}
			*Unsafe.Add<byte>(ref outputBuffer, 5) = (byte)((value & 63U) | 128U);
			*Unsafe.Add<byte>(ref outputBuffer, 4) = (byte)(((value >>= 6) & 63U) | 128U);
			*Unsafe.Add<byte>(ref outputBuffer, 3) = (byte)(((value >>= 6) & 15U) | 224U);
			*Unsafe.Add<byte>(ref outputBuffer, 2) = (byte)(((value >>= 4) & 63U) | 128U);
			*Unsafe.Add<byte>(ref outputBuffer, 1) = (byte)(((value >>= 6) & 63U) | 128U);
			outputBuffer = (byte)((value >>= 6) | 224U);
		}

		// Token: 0x06003090 RID: 12432 RVA: 0x0016648C File Offset: 0x0016568C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static void WriteFirstUtf16CharAsUtf8ThreeByteSequence(ref byte outputBuffer, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				uint num = value << 2 & 16128U;
				uint num2 = (uint)((ushort)value) >> 12;
				Unsafe.WriteUnaligned<ushort>(ref outputBuffer, (ushort)(num + num2 + 32992U));
				*Unsafe.Add<byte>(ref outputBuffer, 2) = (byte)((value & 63U) | 4294967168U);
				return;
			}
			*Unsafe.Add<byte>(ref outputBuffer, 2) = (byte)(((value >>= 16) & 63U) | 128U);
			*Unsafe.Add<byte>(ref outputBuffer, 1) = (byte)(((value >>= 6) & 63U) | 128U);
			outputBuffer = (byte)((value >>= 6) | 224U);
		}

		// Token: 0x06003091 RID: 12433 RVA: 0x00166514 File Offset: 0x00165714
		public unsafe static OperationStatus TranscodeToUtf16(byte* pInputBuffer, int inputLength, char* pOutputBuffer, int outputCharsRemaining, out byte* pInputBufferRemaining, out char* pOutputBufferRemaining)
		{
			UIntPtr uintPtr = ASCIIUtility.WidenAsciiToUtf16(pInputBuffer, pOutputBuffer, (UIntPtr)Math.Min(inputLength, outputCharsRemaining));
			pInputBuffer += (ulong)uintPtr;
			pOutputBuffer += (ulong)uintPtr * 2UL / 2UL;
			if ((int)uintPtr == inputLength)
			{
				pInputBufferRemaining = pInputBuffer;
				pOutputBufferRemaining = pOutputBuffer;
				return OperationStatus.Done;
			}
			inputLength -= (int)uintPtr;
			outputCharsRemaining -= (int)uintPtr;
			if (inputLength >= 4)
			{
				byte* ptr = pInputBuffer + inputLength - 4;
				for (;;)
				{
					uint num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
					int i;
					for (;;)
					{
						IL_51:
						if (ASCIIUtility.AllBytesInUInt32AreAscii(num))
						{
							if (outputCharsRemaining >= 4)
							{
								ASCIIUtility.WidenFourAsciiBytesToUtf16AndWriteToBuffer(ref *pOutputBuffer, num);
								pInputBuffer += 4;
								pOutputBuffer += 4;
								outputCharsRemaining -= 4;
								uint val = (void*)Unsafe.ByteOffset<byte>(ref *pInputBuffer, ref *ptr) + 4U;
								uint num2 = Math.Min(val, (uint)outputCharsRemaining) / 8U;
								for (i = 0; i < (int)num2; i++)
								{
									num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
									uint num3 = Unsafe.ReadUnaligned<uint>((void*)(pInputBuffer + 4));
									if (!ASCIIUtility.AllBytesInUInt32AreAscii(num | num3))
									{
										if (ASCIIUtility.AllBytesInUInt32AreAscii(num))
										{
											ASCIIUtility.WidenFourAsciiBytesToUtf16AndWriteToBuffer(ref *pOutputBuffer, num);
											num = num3;
											pInputBuffer += 4;
											pOutputBuffer += 4;
											outputCharsRemaining -= 4;
										}
										outputCharsRemaining -= 8 * i;
										goto IL_120;
									}
									pInputBuffer += 8;
									ASCIIUtility.WidenFourAsciiBytesToUtf16AndWriteToBuffer(ref *pOutputBuffer, num);
									ASCIIUtility.WidenFourAsciiBytesToUtf16AndWriteToBuffer(ref pOutputBuffer[4], num3);
									pOutputBuffer += 8;
								}
								goto Block_6;
							}
							goto IL_52B;
						}
						IL_120:
						if (Utf8Utility.UInt32FirstByteIsAscii(num))
						{
							if (outputCharsRemaining >= 3)
							{
								uint num4 = Utf8Utility.ToLittleEndian(num);
								UIntPtr uintPtr2 = (UIntPtr)((IntPtr)1);
								*pOutputBuffer = (char)((byte)num4);
								if (Utf8Utility.UInt32SecondByteIsAscii(num))
								{
									uintPtr2++;
									num4 >>= 8;
									pOutputBuffer[1] = (char)((byte)num4);
									if (Utf8Utility.UInt32ThirdByteIsAscii(num))
									{
										uintPtr2++;
										num4 >>= 8;
										pOutputBuffer[2] = (char)((byte)num4);
									}
								}
								pInputBuffer += (ulong)uintPtr2;
								pOutputBuffer += (ulong)uintPtr2 * 2UL / 2UL;
								outputCharsRemaining -= (int)uintPtr2;
							}
							else
							{
								if (outputCharsRemaining == 0)
								{
									goto IL_719;
								}
								uint num5 = Utf8Utility.ToLittleEndian(num);
								pInputBuffer++;
								*(pOutputBuffer++) = (char)((byte)num5);
								outputCharsRemaining--;
								if (Utf8Utility.UInt32SecondByteIsAscii(num))
								{
									if (outputCharsRemaining == 0)
									{
										goto IL_719;
									}
									pInputBuffer++;
									num5 >>= 8;
									*(pOutputBuffer++) = (char)((byte)num5);
									if (Utf8Utility.UInt32ThirdByteIsAscii(num))
									{
										goto IL_719;
									}
									outputCharsRemaining = 0;
								}
							}
							if (pInputBuffer != ptr)
							{
								goto IL_52B;
							}
							num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
						}
						while (Utf8Utility.UInt32BeginsWithUtf8TwoByteMask(num))
						{
							if (Utf8Utility.UInt32BeginsWithOverlongUtf8TwoByteSequence(num))
							{
								goto IL_71D;
							}
							while ((BitConverter.IsLittleEndian && Utf8Utility.UInt32EndsWithValidUtf8TwoByteSequenceLittleEndian(num)) || (!BitConverter.IsLittleEndian && Utf8Utility.UInt32EndsWithUtf8TwoByteMask(num) && !Utf8Utility.UInt32EndsWithOverlongUtf8TwoByteSequence(num)))
							{
								if (outputCharsRemaining < 2)
								{
									goto IL_52B;
								}
								Unsafe.WriteUnaligned<uint>((void*)pOutputBuffer, Utf8Utility.ExtractTwoCharsPackedFromTwoAdjacentTwoByteSequences(num));
								pInputBuffer += 4;
								pOutputBuffer += 2;
								outputCharsRemaining -= 2;
								if (pInputBuffer != ptr)
								{
									goto IL_52B;
								}
								num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
								if (BitConverter.IsLittleEndian)
								{
									if (!Utf8Utility.UInt32BeginsWithValidUtf8TwoByteSequenceLittleEndian(num))
									{
										goto IL_51;
									}
								}
								else
								{
									if (!Utf8Utility.UInt32BeginsWithUtf8TwoByteMask(num))
									{
										goto IL_51;
									}
									if (Utf8Utility.UInt32BeginsWithOverlongUtf8TwoByteSequence(num))
									{
										goto Block_26;
									}
								}
							}
							uint num6 = Utf8Utility.ExtractCharFromFirstTwoByteSequence(num);
							if (Utf8Utility.UInt32ThirdByteIsAscii(num))
							{
								if (Utf8Utility.UInt32FourthByteIsAscii(num))
								{
									goto Block_28;
								}
								if (outputCharsRemaining < 2)
								{
									goto IL_52B;
								}
								*pOutputBuffer = (char)num6;
								pOutputBuffer[1] = (char)((byte)(num >> ((BitConverter.IsLittleEndian ? 16 : 8) & 31)));
								pInputBuffer += 3;
								pOutputBuffer += 2;
								outputCharsRemaining -= 2;
								if (ptr < pInputBuffer)
								{
									goto IL_52B;
								}
								num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
							}
							else
							{
								if (outputCharsRemaining == 0)
								{
									goto IL_52B;
								}
								*pOutputBuffer = (char)num6;
								pInputBuffer += 2;
								pOutputBuffer++;
								outputCharsRemaining--;
								if (ptr >= pInputBuffer)
								{
									num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
									break;
								}
								goto IL_52B;
							}
						}
						if (!Utf8Utility.UInt32BeginsWithUtf8ThreeByteMask(num))
						{
							goto IL_4AD;
						}
						for (;;)
						{
							if (BitConverter.IsLittleEndian)
							{
								if ((num & 8207U) == 0U)
								{
									goto IL_71D;
								}
								if ((num - 8205U & 8207U) == 0U)
								{
									goto Block_38;
								}
							}
							else if ((num & 253755392U) == 0U || (num - 220200960U & 253755392U) == 0U)
							{
								goto IL_71D;
							}
							if (outputCharsRemaining != 0)
							{
								if (!BitConverter.IsLittleEndian || (num - 3758096384U & 4026531840U) != 0U || outputCharsRemaining <= 1 || (void*)Unsafe.ByteOffset<byte>(ref *pInputBuffer, ref *ptr) < 3)
								{
									goto IL_446;
								}
								uint num7 = Unsafe.ReadUnaligned<uint>((void*)(pInputBuffer + 3));
								if (!Utf8Utility.UInt32BeginsWithUtf8ThreeByteMask(num7) || (num7 & 8207U) == 0U || (num7 - 8205U & 8207U) == 0U)
								{
									goto IL_446;
								}
								*pOutputBuffer = (char)Utf8Utility.ExtractCharFromFirstThreeByteSequence(num);
								pOutputBuffer[1] = (char)Utf8Utility.ExtractCharFromFirstThreeByteSequence(num7);
								pInputBuffer += 6;
								pOutputBuffer += 2;
								outputCharsRemaining -= 2;
								IL_45E:
								if (Utf8Utility.UInt32FourthByteIsAscii(num))
								{
									if (outputCharsRemaining == 0)
									{
										goto IL_719;
									}
									if (BitConverter.IsLittleEndian)
									{
										*pOutputBuffer = (char)(num >> 24);
									}
									else
									{
										*pOutputBuffer = (char)((byte)num);
									}
									pInputBuffer++;
									pOutputBuffer++;
									outputCharsRemaining--;
								}
								if (pInputBuffer != ptr)
								{
									goto IL_52B;
								}
								num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
								if (Utf8Utility.UInt32BeginsWithUtf8ThreeByteMask(num))
								{
									continue;
								}
								break;
								IL_446:
								*pOutputBuffer = (char)Utf8Utility.ExtractCharFromFirstThreeByteSequence(num);
								pInputBuffer += 3;
								pOutputBuffer++;
								outputCharsRemaining--;
								goto IL_45E;
							}
							goto IL_719;
						}
					}
					IL_524:
					if (pInputBuffer != ptr)
					{
						goto IL_52B;
					}
					continue;
					Block_6:
					outputCharsRemaining -= 8 * i;
					goto IL_524;
					Block_28:
					if (outputCharsRemaining >= 3)
					{
						uint num6;
						*pOutputBuffer = (char)num6;
						if (BitConverter.IsLittleEndian)
						{
							num >>= 16;
							pOutputBuffer[1] = (char)((byte)num);
							num >>= 8;
							pOutputBuffer[2] = (char)num;
						}
						else
						{
							pOutputBuffer[2] = (char)((byte)num);
							pOutputBuffer[1] = (char)((byte)(num >> 8));
						}
						pInputBuffer += 4;
						pOutputBuffer += 3;
						outputCharsRemaining -= 3;
						goto IL_524;
					}
					goto IL_52B;
					IL_4AD:
					if (!Utf8Utility.UInt32BeginsWithUtf8FourByteMask(num))
					{
						break;
					}
					if (BitConverter.IsLittleEndian)
					{
						uint value = num & 65535U;
						value = BitOperations.RotateRight(value, 8);
						if (!UnicodeUtility.IsInRangeInclusive(value, 4026531984U, 4093640847U))
						{
							break;
						}
					}
					else if (!UnicodeUtility.IsInRangeInclusive(num, 4035969024U, 4103077887U))
					{
						break;
					}
					if (outputCharsRemaining >= 2)
					{
						Unsafe.WriteUnaligned<uint>((void*)pOutputBuffer, Utf8Utility.ExtractCharsFromFourByteSequence(num));
						pInputBuffer += 4;
						pOutputBuffer += 2;
						outputCharsRemaining -= 2;
						goto IL_524;
					}
					goto IL_719;
				}
				Block_26:
				Block_38:
				goto IL_71D;
				IL_52B:
				inputLength = (void*)Unsafe.ByteOffset<byte>(ref *pInputBuffer, ref *ptr) + 4;
			}
			OperationStatus result;
			while (inputLength > 0)
			{
				uint num8 = (uint)(*pInputBuffer);
				if (num8 > 127U)
				{
					num8 -= 194U;
					if ((byte)num8 <= 29)
					{
						if (inputLength >= 2)
						{
							uint num9 = (uint)pInputBuffer[1];
							if (!Utf8Utility.IsLowByteUtf8ContinuationByte(num9))
							{
								goto IL_71D;
							}
							if (outputCharsRemaining != 0)
							{
								uint num10 = (num8 << 6) + num9 + 128U - 128U;
								*pOutputBuffer = (char)num10;
								pInputBuffer += 2;
								pOutputBuffer++;
								inputLength -= 2;
								outputCharsRemaining--;
								continue;
							}
							goto IL_719;
						}
					}
					else if ((byte)num8 <= 45)
					{
						if (inputLength >= 3)
						{
							uint num11 = (uint)pInputBuffer[1];
							uint num12 = (uint)pInputBuffer[2];
							if (!Utf8Utility.IsLowByteUtf8ContinuationByte(num11) || !Utf8Utility.IsLowByteUtf8ContinuationByte(num12))
							{
								goto IL_71D;
							}
							uint num13 = (num8 << 12) + (num11 << 6);
							if (num13 < 133120U)
							{
								goto IL_71D;
							}
							num13 -= 186368U;
							if (num13 < 2048U)
							{
								goto IL_71D;
							}
							if (outputCharsRemaining != 0)
							{
								num13 += num12;
								num13 += 55296U;
								num13 -= 128U;
								*pOutputBuffer = (char)num13;
								pInputBuffer += 3;
								pOutputBuffer++;
								inputLength -= 3;
								outputCharsRemaining--;
								continue;
							}
							goto IL_719;
						}
						else if (inputLength >= 2)
						{
							uint num14 = (uint)pInputBuffer[1];
							if (!Utf8Utility.IsLowByteUtf8ContinuationByte(num14))
							{
								goto IL_71D;
							}
							uint num15 = (num8 << 6) + num14;
							if (num15 < 2080U)
							{
								goto IL_71D;
							}
							if (UnicodeUtility.IsInRangeInclusive(num15, 2912U, 2943U))
							{
								goto IL_71D;
							}
						}
					}
					else
					{
						if ((byte)num8 > 50)
						{
							goto IL_71D;
						}
						if (inputLength >= 2)
						{
							uint num16 = (uint)pInputBuffer[1];
							if (!Utf8Utility.IsLowByteUtf8ContinuationByte(num16))
							{
								goto IL_71D;
							}
							uint value2 = (num8 << 6) + num16;
							if (!UnicodeUtility.IsInRangeInclusive(value2, 3088U, 3343U))
							{
								goto IL_71D;
							}
							if (inputLength >= 3)
							{
								if (!Utf8Utility.IsLowByteUtf8ContinuationByte((uint)pInputBuffer[2]))
								{
									goto IL_71D;
								}
								if (inputLength >= 4)
								{
									if (!Utf8Utility.IsLowByteUtf8ContinuationByte((uint)pInputBuffer[3]))
									{
										goto IL_71D;
									}
									goto IL_719;
								}
							}
						}
					}
					result = OperationStatus.NeedMoreData;
					goto IL_71F;
				}
				if (outputCharsRemaining == 0)
				{
					goto IL_719;
				}
				*pOutputBuffer = (char)num8;
				pInputBuffer++;
				pOutputBuffer++;
				inputLength--;
				outputCharsRemaining--;
			}
			result = OperationStatus.Done;
			goto IL_71F;
			IL_719:
			result = OperationStatus.DestinationTooSmall;
			goto IL_71F;
			IL_71D:
			result = OperationStatus.InvalidData;
			IL_71F:
			pInputBufferRemaining = pInputBuffer;
			pOutputBufferRemaining = pOutputBuffer;
			return result;
		}

		// Token: 0x06003092 RID: 12434 RVA: 0x00166C4C File Offset: 0x00165E4C
		public unsafe static OperationStatus TranscodeToUtf8(char* pInputBuffer, int inputLength, byte* pOutputBuffer, int outputBytesRemaining, out char* pInputBufferRemaining, out byte* pOutputBufferRemaining)
		{
			UIntPtr uintPtr = ASCIIUtility.NarrowUtf16ToAscii(pInputBuffer, pOutputBuffer, (UIntPtr)Math.Min(inputLength, outputBytesRemaining));
			pInputBuffer += (ulong)uintPtr * 2UL / 2UL;
			pOutputBuffer += (ulong)uintPtr;
			if ((int)uintPtr == inputLength)
			{
				pInputBufferRemaining = pInputBuffer;
				pOutputBufferRemaining = pOutputBuffer;
				return OperationStatus.Done;
			}
			inputLength -= (int)uintPtr;
			outputBytesRemaining -= (int)uintPtr;
			uint num9;
			if (inputLength >= 2)
			{
				char* ptr = pInputBuffer + (ulong)inputLength * 2UL / 2UL - 2;
				Vector128<short> right;
				Unsafe.SkipInit<Vector128<short>>(out right);
				if (Sse41.X64.IsSupported || (AdvSimd.Arm64.IsSupported && BitConverter.IsLittleEndian))
				{
					right = Vector128.Create(-128);
				}
				uint num;
				for (;;)
				{
					num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
					int i;
					Vector128<short> vector;
					ulong num8;
					for (;;)
					{
						IL_82:
						if (Utf16Utility.AllCharsInUInt32AreAscii(num))
						{
							if (outputBytesRemaining < 2)
							{
								goto IL_589;
							}
							uint num2 = num | num >> 8;
							Unsafe.WriteUnaligned<ushort>((void*)pOutputBuffer, (ushort)num2);
							pInputBuffer += 2;
							pOutputBuffer += 2;
							outputBytesRemaining -= 2;
							uint num3 = (uint)((long)(ptr - pInputBuffer)) + 2U;
							uint num4 = (uint)Math.Min((long)((ulong)num3), (long)outputBytesRemaining);
							if (!Sse41.X64.IsSupported && (!AdvSimd.Arm64.IsSupported || !BitConverter.IsLittleEndian))
							{
								uint num5 = num4 / 4U;
								i = 0;
								while (i < (int)num5)
								{
									num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
									uint num6 = Unsafe.ReadUnaligned<uint>((void*)(pInputBuffer + 2));
									if (Utf16Utility.AllCharsInUInt32AreAscii(num | num6))
									{
										Unsafe.WriteUnaligned<ushort>((void*)pOutputBuffer, (ushort)(num | num >> 8));
										Unsafe.WriteUnaligned<ushort>((void*)(pOutputBuffer + 2), (ushort)(num6 | num6 >> 8));
										pInputBuffer += 4;
										pOutputBuffer += 4;
										i++;
									}
									else
									{
										outputBytesRemaining -= 4 * i;
										if (Utf16Utility.AllCharsInUInt32AreAscii(num))
										{
											Unsafe.WriteUnaligned<ushort>((void*)pOutputBuffer, (ushort)(num | num >> 8));
											pInputBuffer += 2;
											pOutputBuffer += 2;
											outputBytesRemaining -= 2;
											num = num6;
											goto IL_369;
										}
										goto IL_369;
									}
								}
								goto Block_21;
							}
							uint num7 = num4 / 8U;
							int j = 0;
							while (j < (int)num7)
							{
								vector = Unsafe.ReadUnaligned<Vector128<short>>((void*)pInputBuffer);
								if (AdvSimd.IsSupported)
								{
									Vector128<short> vector2 = AdvSimd.CompareTest(vector, right);
									if (AdvSimd.Arm64.MinPairwise(vector2, vector2).AsUInt64<short>().ToScalar<ulong>() > 0UL)
									{
										goto IL_203;
									}
									Vector64<byte> source = AdvSimd.ExtractNarrowingSaturateUnsignedLower(vector);
									AdvSimd.Store(pOutputBuffer, source);
								}
								else
								{
									if (!Sse41.TestZ(vector, right))
									{
										goto IL_203;
									}
									Sse2.StoreScalar((ulong*)pOutputBuffer, Sse2.PackUnsignedSaturate(vector, vector).AsUInt64<byte>());
								}
								pInputBuffer += 8;
								pOutputBuffer += 8;
								j++;
								continue;
								IL_203:
								outputBytesRemaining -= 8 * j;
								if (Sse2.X64.IsSupported)
								{
									num8 = Sse2.X64.ConvertToUInt64(vector.AsUInt64<short>());
								}
								else
								{
									num8 = vector.AsUInt64<short>().ToScalar<ulong>();
								}
								if (Utf16Utility.AllCharsInUInt64AreAscii(num8))
								{
									if (AdvSimd.IsSupported)
									{
										Vector64<byte> vector3 = AdvSimd.ExtractNarrowingSaturateUnsignedLower(vector);
										AdvSimd.StoreSelectedScalar((uint*)pOutputBuffer, vector3.AsUInt32<byte>(), 0);
									}
									else
									{
										Unsafe.WriteUnaligned<uint>((void*)pOutputBuffer, Sse2.ConvertToUInt32(Sse2.PackUnsignedSaturate(vector, vector).AsUInt32<byte>()));
									}
									pInputBuffer += 4;
									pOutputBuffer += 4;
									outputBytesRemaining -= 4;
									num8 = vector.AsUInt64<short>().GetElement(1);
								}
								IL_293:
								num = (uint)num8;
								if (Utf16Utility.AllCharsInUInt32AreAscii(num))
								{
									Unsafe.WriteUnaligned<ushort>((void*)pOutputBuffer, (ushort)(num | num >> 8));
									pInputBuffer += 2;
									pOutputBuffer += 2;
									outputBytesRemaining -= 2;
									num = (uint)(num8 >> 32);
									goto IL_369;
								}
								goto IL_369;
							}
							outputBytesRemaining -= 8 * j;
							if ((num4 & 4U) == 0U)
							{
								break;
							}
							num8 = Unsafe.ReadUnaligned<ulong>((void*)pInputBuffer);
							if (Utf16Utility.AllCharsInUInt64AreAscii(num8))
							{
								goto Block_14;
							}
							goto IL_293;
						}
						for (;;)
						{
							IL_369:
							if (Utf8Utility.IsFirstCharAscii(num))
							{
								if (outputBytesRemaining == 0)
								{
									goto IL_63B;
								}
								if (BitConverter.IsLittleEndian)
								{
									*pOutputBuffer = (byte)num;
								}
								else
								{
									*pOutputBuffer = (byte)(num >> 24);
								}
								pInputBuffer++;
								pOutputBuffer++;
								outputBytesRemaining--;
								if (pInputBuffer != ptr)
								{
									goto IL_573;
								}
								num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
							}
							if (!Utf8Utility.IsFirstCharAtLeastThreeUtf8Bytes(num))
							{
								while (Utf8Utility.IsSecondCharTwoUtf8Bytes(num))
								{
									if (outputBytesRemaining < 4)
									{
										goto IL_589;
									}
									Unsafe.WriteUnaligned<uint>((void*)pOutputBuffer, Utf8Utility.ExtractTwoUtf8TwoByteSequencesFromTwoPackedUtf16Chars(num));
									pInputBuffer += 2;
									pOutputBuffer += 4;
									outputBytesRemaining -= 4;
									if (pInputBuffer != ptr)
									{
										goto IL_573;
									}
									num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
									if (!Utf8Utility.IsFirstCharTwoUtf8Bytes(num))
									{
										goto IL_82;
									}
								}
								if (outputBytesRemaining < 2)
								{
									goto IL_63B;
								}
								Unsafe.WriteUnaligned<ushort>((void*)pOutputBuffer, (ushort)Utf8Utility.ExtractUtf8TwoByteSequenceFromFirstUtf16Char(num));
								if (Utf8Utility.IsSecondCharAscii(num))
								{
									goto Block_32;
								}
								pInputBuffer++;
								pOutputBuffer += 2;
								outputBytesRemaining -= 2;
								if (pInputBuffer != ptr)
								{
									goto IL_573;
								}
								num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
							}
							while (!Utf8Utility.IsFirstCharSurrogate(num))
							{
								if (Utf8Utility.IsSecondCharAtLeastThreeUtf8Bytes(num) && !Utf8Utility.IsSecondCharSurrogate(num) && outputBytesRemaining >= 6)
								{
									Utf8Utility.WriteTwoUtf16CharsAsTwoUtf8ThreeByteSequences(ref *pOutputBuffer, num);
									pInputBuffer += 2;
									pOutputBuffer += 6;
									outputBytesRemaining -= 6;
									if (pInputBuffer != ptr)
									{
										goto IL_573;
									}
									num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
									if (!Utf8Utility.IsFirstCharAtLeastThreeUtf8Bytes(num))
									{
										goto IL_82;
									}
								}
								else
								{
									if (outputBytesRemaining < 3)
									{
										goto IL_63B;
									}
									Utf8Utility.WriteFirstUtf16CharAsUtf8ThreeByteSequence(ref *pOutputBuffer, num);
									pInputBuffer++;
									pOutputBuffer += 3;
									outputBytesRemaining -= 3;
									if (Utf8Utility.IsSecondCharAscii(num))
									{
										if (outputBytesRemaining == 0)
										{
											goto IL_63B;
										}
										if (BitConverter.IsLittleEndian)
										{
											*pOutputBuffer = (byte)(num >> 16);
										}
										else
										{
											*pOutputBuffer = (byte)num;
										}
										pInputBuffer++;
										pOutputBuffer++;
										outputBytesRemaining--;
										if (pInputBuffer != ptr)
										{
											goto IL_573;
										}
										num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
										if (!Utf8Utility.IsFirstCharAtLeastThreeUtf8Bytes(num))
										{
											goto IL_82;
										}
									}
									else
									{
										if (pInputBuffer == ptr)
										{
											num = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
											goto IL_369;
										}
										goto IL_573;
									}
								}
							}
							goto IL_53C;
						}
					}
					IL_56C:
					if (pInputBuffer != ptr)
					{
						goto IL_573;
					}
					continue;
					Block_14:
					vector = Vector128.CreateScalarUnsafe(num8).AsInt16<ulong>();
					if (AdvSimd.IsSupported)
					{
						Vector64<byte> vector4 = AdvSimd.ExtractNarrowingSaturateUnsignedLower(vector);
						AdvSimd.StoreSelectedScalar((uint*)pOutputBuffer, vector4.AsUInt32<byte>(), 0);
					}
					else
					{
						Unsafe.WriteUnaligned<uint>((void*)pOutputBuffer, Sse2.ConvertToUInt32(Sse2.PackUnsignedSaturate(vector, vector).AsUInt32<byte>()));
					}
					pInputBuffer += 4;
					pOutputBuffer += 4;
					outputBytesRemaining -= 4;
					goto IL_56C;
					Block_21:
					outputBytesRemaining -= 4 * i;
					goto IL_56C;
					Block_32:
					if (outputBytesRemaining >= 3)
					{
						if (BitConverter.IsLittleEndian)
						{
							num >>= 16;
						}
						pOutputBuffer[2] = (byte)num;
						pInputBuffer += 2;
						pOutputBuffer += 3;
						outputBytesRemaining -= 3;
						goto IL_56C;
					}
					break;
					IL_53C:
					if (!Utf8Utility.IsWellFormedUtf16SurrogatePair(num))
					{
						goto IL_640;
					}
					if (outputBytesRemaining >= 4)
					{
						Unsafe.WriteUnaligned<uint>((void*)pOutputBuffer, Utf8Utility.ExtractFourUtf8BytesFromSurrogatePair(num));
						pInputBuffer += 2;
						pOutputBuffer += 4;
						outputBytesRemaining -= 4;
						goto IL_56C;
					}
					goto IL_63B;
				}
				pInputBuffer++;
				pOutputBuffer += 2;
				goto IL_63B;
				IL_573:
				inputLength = (int)((long)(ptr - pInputBuffer)) + 2;
				goto IL_57E;
				IL_589:
				if (BitConverter.IsLittleEndian)
				{
					num9 = (num & 65535U);
					goto IL_59F;
				}
				num9 = num >> 16;
				goto IL_59F;
			}
			IL_57E:
			if (inputLength == 0)
			{
				goto IL_631;
			}
			num9 = (uint)(*pInputBuffer);
			IL_59F:
			OperationStatus result;
			if (num9 <= 127U)
			{
				if (outputBytesRemaining == 0)
				{
					goto IL_63B;
				}
				*pOutputBuffer = (byte)num9;
				pInputBuffer++;
				pOutputBuffer++;
			}
			else if (num9 < 2048U)
			{
				if (outputBytesRemaining < 2)
				{
					goto IL_63B;
				}
				pOutputBuffer[1] = (byte)((num9 & 63U) | 4294967168U);
				*pOutputBuffer = (byte)(num9 >> 6 | 4294967232U);
				pInputBuffer++;
				pOutputBuffer += 2;
			}
			else if (!UnicodeUtility.IsSurrogateCodePoint(num9))
			{
				if (outputBytesRemaining < 3)
				{
					goto IL_63B;
				}
				pOutputBuffer[2] = (byte)((num9 & 63U) | 4294967168U);
				pOutputBuffer[1] = (byte)((num9 >> 6 & 63U) | 4294967168U);
				*pOutputBuffer = (byte)(num9 >> 12 | 4294967264U);
				pInputBuffer++;
				pOutputBuffer += 3;
			}
			else
			{
				if (num9 <= 56319U)
				{
					result = OperationStatus.NeedMoreData;
					goto IL_643;
				}
				goto IL_640;
			}
			if (inputLength > 1)
			{
				goto IL_63B;
			}
			IL_631:
			result = OperationStatus.Done;
			goto IL_643;
			IL_63B:
			result = OperationStatus.DestinationTooSmall;
			goto IL_643;
			IL_640:
			result = OperationStatus.InvalidData;
			IL_643:
			pInputBufferRemaining = pInputBuffer;
			pOutputBufferRemaining = pOutputBuffer;
			return result;
		}

		// Token: 0x06003093 RID: 12435 RVA: 0x001672A8 File Offset: 0x001664A8
		public unsafe static byte* GetPointerToFirstInvalidByte(byte* pInputBuffer, int inputLength, out int utf16CodeUnitCountAdjustment, out int scalarCountAdjustment)
		{
			UIntPtr indexOfFirstNonAsciiByte = ASCIIUtility.GetIndexOfFirstNonAsciiByte(pInputBuffer, (UIntPtr)inputLength);
			pInputBuffer += (ulong)indexOfFirstNonAsciiByte;
			inputLength -= (int)indexOfFirstNonAsciiByte;
			if (inputLength == 0)
			{
				utf16CodeUnitCountAdjustment = 0;
				scalarCountAdjustment = 0;
				return pInputBuffer;
			}
			int num = 0;
			int num2 = 0;
			UIntPtr uintPtr2;
			if (inputLength >= 4)
			{
				byte* ptr = pInputBuffer + inputLength - 4;
				IL_52F:
				while (pInputBuffer == ptr)
				{
					uint num3 = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
					for (;;)
					{
						IL_43:
						if (!ASCIIUtility.AllBytesInUInt32AreAscii(num3))
						{
							goto IL_172;
						}
						pInputBuffer += 4;
						if ((void*)Unsafe.ByteOffset<byte>(ref *pInputBuffer, ref *ptr) < 16)
						{
							goto IL_52F;
						}
						num3 = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
						if (ASCIIUtility.AllBytesInUInt32AreAscii(num3))
						{
							pInputBuffer = (pInputBuffer + 4 & ~3);
							byte* ptr2 = ptr - 12;
							Vector128<byte> bitMask = BitConverter.IsLittleEndian ? Vector128.Create(4097).AsByte<ushort>() : Vector128.Create(272).AsByte<ushort>();
							ulong nonAsciiBytes;
							uint num4;
							for (;;)
							{
								if (AdvSimd.Arm64.IsSupported && BitConverter.IsLittleEndian)
								{
									nonAsciiBytes = Utf8Utility.GetNonAsciiBytes(AdvSimd.LoadVector128(pInputBuffer), bitMask);
									if (nonAsciiBytes != 0UL)
									{
										goto Block_9;
									}
								}
								else if (Sse2.IsSupported)
								{
									num4 = (uint)Sse2.MoveMask(Sse2.LoadVector128(pInputBuffer));
									if (num4 != 0U)
									{
										goto Block_11;
									}
								}
								else
								{
									if (!ASCIIUtility.AllBytesInUInt32AreAscii(*(uint*)pInputBuffer | *(uint*)(pInputBuffer + 4)))
									{
										goto IL_15C;
									}
									if (!ASCIIUtility.AllBytesInUInt32AreAscii(*(uint*)(pInputBuffer + (IntPtr)2 * 4) | *(uint*)(pInputBuffer + (IntPtr)3 * 4)))
									{
										goto IL_157;
									}
								}
								pInputBuffer += 16;
								if (pInputBuffer != ptr2)
								{
									goto Block_13;
								}
							}
							IL_13E:
							UIntPtr uintPtr;
							pInputBuffer += (ulong)uintPtr;
							if (pInputBuffer == ptr)
							{
								num3 = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
								goto IL_191;
							}
							goto IL_540;
							Block_9:
							uintPtr = (UIntPtr)((IntPtr)BitOperations.TrailingZeroCount(nonAsciiBytes) >> 2);
							goto IL_13E;
							IL_15C:
							num3 = *(uint*)pInputBuffer;
							if (ASCIIUtility.AllBytesInUInt32AreAscii(num3))
							{
								pInputBuffer += 4;
								num3 = *(uint*)pInputBuffer;
								goto IL_172;
							}
							goto IL_172;
							IL_157:
							pInputBuffer += 8;
							goto IL_15C;
							Block_11:
							uintPtr = (UIntPtr)((IntPtr)BitOperations.TrailingZeroCount(num4));
							goto IL_13E;
						}
						goto IL_172;
						for (;;)
						{
							IL_191:
							num3 -= (BitConverter.IsLittleEndian ? 32960U : 3229614080U);
							if ((num3 & (BitConverter.IsLittleEndian ? 49376U : 3770679296U)) != 0U)
							{
								break;
							}
							if ((BitConverter.IsLittleEndian && (byte)num3 < 2) || (!BitConverter.IsLittleEndian && num3 < 33554432U))
							{
								goto IL_628;
							}
							while ((BitConverter.IsLittleEndian && Utf8Utility.UInt32EndsWithValidUtf8TwoByteSequenceLittleEndian(num3)) || (!BitConverter.IsLittleEndian && Utf8Utility.UInt32EndsWithUtf8TwoByteMask(num3) && !Utf8Utility.UInt32EndsWithOverlongUtf8TwoByteSequence(num3)))
							{
								pInputBuffer += 4;
								num -= 2;
								if (pInputBuffer != ptr)
								{
									goto IL_540;
								}
								num3 = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
								if (BitConverter.IsLittleEndian)
								{
									if (!Utf8Utility.UInt32BeginsWithValidUtf8TwoByteSequenceLittleEndian(num3))
									{
										goto IL_43;
									}
								}
								else
								{
									if (!Utf8Utility.UInt32BeginsWithUtf8TwoByteMask(num3))
									{
										goto IL_43;
									}
									if (Utf8Utility.UInt32BeginsWithOverlongUtf8TwoByteSequence(num3))
									{
										goto Block_29;
									}
								}
							}
							num--;
							if (!Utf8Utility.UInt32ThirdByteIsAscii(num3))
							{
								goto IL_290;
							}
							if (Utf8Utility.UInt32FourthByteIsAscii(num3))
							{
								goto Block_31;
							}
							pInputBuffer += 3;
							if (pInputBuffer != ptr)
							{
								goto IL_52F;
							}
							num3 = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
						}
						num3 -= (BitConverter.IsLittleEndian ? 8388640U : 536903680U);
						if ((num3 & (BitConverter.IsLittleEndian ? 12632304U : 4039163904U)) != 0U)
						{
							goto IL_4B6;
						}
						for (;;)
						{
							if (BitConverter.IsLittleEndian)
							{
								if ((num3 & 8207U) == 0U)
								{
									goto IL_628;
								}
								if ((num3 - 8205U & 8207U) == 0U)
								{
									goto Block_37;
								}
							}
							else if ((num3 & 253755392U) == 0U || (num3 - 220200960U & 253755392U) == 0U)
							{
								goto IL_628;
							}
							for (;;)
							{
								IL_316:
								IntPtr intPtr;
								if (BitConverter.IsLittleEndian)
								{
									intPtr = (IntPtr)((int)num3 >> 31);
								}
								else
								{
									intPtr = (IntPtr)((sbyte)num3) >> 7;
								}
								pInputBuffer += 4;
								pInputBuffer += (long)intPtr;
								num -= 2;
								ulong num5;
								for (;;)
								{
									int size = IntPtr.Size;
									if (!BitConverter.IsLittleEndian || (IntPtr)((long)(ptr - pInputBuffer)) < (IntPtr)5)
									{
										goto IL_496;
									}
									num5 = Unsafe.ReadUnaligned<ulong>((void*)pInputBuffer);
									num3 = (uint)num5;
									if ((num5 & 13902823984598139120UL) != 9286563722648649952UL || !Utf8Utility.IsUtf8ContinuationByte(pInputBuffer[8]))
									{
										break;
									}
									if (((uint)num5 & 8207U) == 0U || ((uint)num5 - 8205U & 8207U) == 0U)
									{
										goto IL_628;
									}
									num5 >>= 24;
									if (((uint)num5 & 8207U) == 0U || ((uint)num5 - 8205U & 8207U) == 0U)
									{
										goto IL_316;
									}
									num5 >>= 24;
									if (((uint)num5 & 8207U) == 0U || ((uint)num5 - 8205U & 8207U) == 0U)
									{
										goto IL_316;
									}
									pInputBuffer += 9;
									num -= 6;
								}
								if ((num5 & 211934905417968UL) != 141291010687200UL)
								{
									break;
								}
								if (((uint)num5 & 8207U) == 0U || ((uint)num5 - 8205U & 8207U) == 0U)
								{
									goto IL_628;
								}
								num5 >>= 24;
								if (((uint)num5 & 8207U) != 0U && ((uint)num5 - 8205U & 8207U) != 0U)
								{
									goto Block_54;
								}
							}
							if (Utf8Utility.UInt32BeginsWithUtf8ThreeByteMask(num3))
							{
								continue;
							}
							goto IL_43;
							IL_496:
							if (pInputBuffer != ptr)
							{
								goto IL_540;
							}
							num3 = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
							if (!Utf8Utility.UInt32BeginsWithUtf8ThreeByteMask(num3))
							{
								goto IL_43;
							}
						}
						IL_172:
						uint num6 = ASCIIUtility.CountNumberOfLeadingAsciiBytesFromUInt32WithSomeNonAsciiData(num3);
						pInputBuffer += num6;
						if (ptr >= pInputBuffer)
						{
							num3 = Unsafe.ReadUnaligned<uint>((void*)pInputBuffer);
							goto IL_191;
						}
						goto IL_540;
					}
					Block_29:
					goto IL_628;
					Block_13:
					continue;
					Block_31:
					pInputBuffer += 4;
					continue;
					IL_290:
					pInputBuffer += 2;
					continue;
					Block_54:
					pInputBuffer += 6;
					num -= 4;
					continue;
					IL_4B6:
					if (BitConverter.IsLittleEndian)
					{
						num3 &= 3233873919U;
						if (num3 > 2147500031U)
						{
							goto IL_628;
						}
						num3 = BitOperations.RotateRight(num3, 8);
						if (!UnicodeUtility.IsInRangeInclusive(num3, 276824080U, 343932943U))
						{
							goto IL_628;
						}
					}
					else
					{
						num3 -= 128U;
						if ((num3 & 12632256U) != 0U || !UnicodeUtility.IsInRangeInclusive(num3, 269484032U, 336592895U))
						{
							goto IL_628;
						}
					}
					pInputBuffer += 4;
					num -= 2;
					num2--;
					continue;
					Block_37:
					goto IL_628;
					IL_540:
					uintPtr2 = (byte*)((void*)Unsafe.ByteOffset<byte>(ref *pInputBuffer, ref *ptr)) + 4;
					goto IL_620;
				}
				goto IL_540;
			}
			uintPtr2 = (UIntPtr)inputLength;
			IL_620:
			while (uintPtr2 > (UIntPtr)((IntPtr)0))
			{
				uint num7 = (uint)(*pInputBuffer);
				if ((byte)num7 < 128)
				{
					pInputBuffer++;
					uintPtr2--;
				}
				else
				{
					if (uintPtr2 < (UIntPtr)((IntPtr)2))
					{
						break;
					}
					uint value = (uint)pInputBuffer[1];
					if ((byte)num7 < 224)
					{
						if ((byte)num7 < 194 || !Utf8Utility.IsLowByteUtf8ContinuationByte(value))
						{
							break;
						}
						pInputBuffer += 2;
						num--;
						uintPtr2 -= (UIntPtr)((IntPtr)2);
					}
					else
					{
						if (uintPtr2 < (UIntPtr)((IntPtr)3) || (byte)num7 >= 240)
						{
							break;
						}
						if ((byte)num7 == 224)
						{
							if (!UnicodeUtility.IsInRangeInclusive(value, 160U, 191U))
							{
								break;
							}
						}
						else if ((byte)num7 == 237)
						{
							if (!UnicodeUtility.IsInRangeInclusive(value, 128U, 159U))
							{
								break;
							}
						}
						else if (!Utf8Utility.IsLowByteUtf8ContinuationByte(value))
						{
							break;
						}
						if (!Utf8Utility.IsUtf8ContinuationByte(pInputBuffer[2]))
						{
							break;
						}
						pInputBuffer += 3;
						num -= 2;
						uintPtr2 -= (UIntPtr)((IntPtr)3);
					}
				}
			}
			IL_628:
			utf16CodeUnitCountAdjustment = num;
			scalarCountAdjustment = num2;
			return pInputBuffer;
		}

		// Token: 0x06003094 RID: 12436 RVA: 0x001678E4 File Offset: 0x00166AE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static ulong GetNonAsciiBytes(Vector128<byte> value, Vector128<byte> bitMask128)
		{
			if (!AdvSimd.Arm64.IsSupported || !BitConverter.IsLittleEndian)
			{
				throw new PlatformNotSupportedException();
			}
			Vector128<byte> left = AdvSimd.ShiftRightArithmetic(value.AsSByte<byte>(), 7).AsByte<sbyte>();
			Vector128<byte> vector = AdvSimd.And(left, bitMask128);
			vector = AdvSimd.Arm64.AddPairwise(vector, vector);
			return vector.AsUInt64<byte>().ToScalar<ulong>();
		}
	}
}
