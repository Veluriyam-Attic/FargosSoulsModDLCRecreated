using System;
using System.Buffers;
using System.Globalization;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Unicode;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200014A RID: 330
	internal static class Marvin
	{
		// Token: 0x06001073 RID: 4211 RVA: 0x000DB84A File Offset: 0x000DAA4A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ComputeHash32(ReadOnlySpan<byte> data, ulong seed)
		{
			return Marvin.ComputeHash32(MemoryMarshal.GetReference<byte>(data), (uint)data.Length, (uint)seed, (uint)(seed >> 32));
		}

		// Token: 0x06001074 RID: 4212 RVA: 0x000DB868 File Offset: 0x000DAA68
		public unsafe static int ComputeHash32(ref byte data, uint count, uint p0, uint p1)
		{
			uint num;
			if (count < 8U)
			{
				if (count < 4U)
				{
					if (BitConverter.IsLittleEndian)
					{
						num = 128U;
					}
					else
					{
						num = 2147483648U;
					}
					if ((count & 1U) != 0U)
					{
						num = (uint)(*Unsafe.AddByteOffset<byte>(ref data, (UIntPtr)count & (UIntPtr)((IntPtr)2)));
						if (BitConverter.IsLittleEndian)
						{
							num |= 32768U;
						}
						else
						{
							num <<= 24;
							num |= 8388608U;
						}
					}
					if ((count & 2U) == 0U)
					{
						goto IL_BD;
					}
					if (BitConverter.IsLittleEndian)
					{
						num <<= 16;
						num |= (uint)Unsafe.ReadUnaligned<ushort>(ref data);
						goto IL_BD;
					}
					num |= (uint)Unsafe.ReadUnaligned<ushort>(ref data);
					num = BitOperations.RotateLeft(num, 16);
					goto IL_BD;
				}
			}
			else
			{
				uint num2 = count / 8U;
				do
				{
					p0 += Unsafe.ReadUnaligned<uint>(ref data);
					uint num3 = Unsafe.ReadUnaligned<uint>(Unsafe.AddByteOffset<byte>(ref data, (UIntPtr)((IntPtr)4)));
					Marvin.Block(ref p0, ref p1);
					p0 += num3;
					Marvin.Block(ref p0, ref p1);
					data = Unsafe.AddByteOffset<byte>(ref data, (UIntPtr)((IntPtr)8));
				}
				while ((num2 -= 1U) > 0U);
				if ((count & 4U) == 0U)
				{
					goto IL_6A;
				}
			}
			p0 += Unsafe.ReadUnaligned<uint>(ref data);
			Marvin.Block(ref p0, ref p1);
			IL_6A:
			num = Unsafe.ReadUnaligned<uint>(Unsafe.Add<byte>(Unsafe.AddByteOffset<byte>(ref data, (UIntPtr)count & (UIntPtr)((IntPtr)7)), -4));
			count = ~count << 3;
			if (BitConverter.IsLittleEndian)
			{
				num >>= 8;
				num |= 2147483648U;
				num >>= (int)count;
			}
			else
			{
				num <<= 8;
				num |= 128U;
				num <<= (int)count;
			}
			IL_BD:
			p0 += num;
			Marvin.Block(ref p0, ref p1);
			Marvin.Block(ref p0, ref p1);
			return (int)(p1 ^ p0);
		}

		// Token: 0x06001075 RID: 4213 RVA: 0x000DB9C4 File Offset: 0x000DABC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Block(ref uint rp0, ref uint rp1)
		{
			uint num = rp0;
			uint num2 = rp1;
			num2 ^= num;
			num = BitOperations.RotateLeft(num, 20);
			num += num2;
			num2 = BitOperations.RotateLeft(num2, 9);
			num2 ^= num;
			num = BitOperations.RotateLeft(num, 27);
			num += num2;
			num2 = BitOperations.RotateLeft(num2, 19);
			rp0 = num;
			rp1 = num2;
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06001076 RID: 4214 RVA: 0x000DBA11 File Offset: 0x000DAC11
		public static ulong DefaultSeed { get; } = Marvin.GenerateSeed();

		// Token: 0x06001077 RID: 4215 RVA: 0x000DBA18 File Offset: 0x000DAC18
		private unsafe static ulong GenerateSeed()
		{
			ulong result;
			Interop.GetRandomBytes((byte*)(&result), 8);
			return result;
		}

		// Token: 0x06001078 RID: 4216 RVA: 0x000DBA30 File Offset: 0x000DAC30
		public unsafe static int ComputeHash32OrdinalIgnoreCase(ref char data, int count, uint p0, uint p1)
		{
			uint num = (uint)count;
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			while (num >= 2U)
			{
				uint num2 = Unsafe.ReadUnaligned<uint>(Unsafe.As<char, byte>(Unsafe.AddByteOffset<char>(ref data, uintPtr)));
				if (!Utf16Utility.AllCharsInUInt32AreAscii(num2))
				{
					IL_82:
					return Marvin.ComputeHash32OrdinalIgnoreCaseSlow(Unsafe.AddByteOffset<char>(ref data, uintPtr), (int)num, p0, p1);
				}
				p0 += Utf16Utility.ConvertAllAsciiCharsInUInt32ToUppercase(num2);
				Marvin.Block(ref p0, ref p1);
				uintPtr += (UIntPtr)((IntPtr)4);
				num -= 2U;
			}
			if (num > 0U)
			{
				uint num2 = (uint)(*Unsafe.AddByteOffset<char>(ref data, uintPtr));
				if (num2 > 127U)
				{
					goto IL_82;
				}
				p0 += Utf16Utility.ConvertAllAsciiCharsInUInt32ToUppercase(num2) + 8388480U;
			}
			p0 += 128U;
			Marvin.Block(ref p0, ref p1);
			Marvin.Block(ref p0, ref p1);
			return (int)(p1 ^ p0);
		}

		// Token: 0x06001079 RID: 4217 RVA: 0x000DBAD0 File Offset: 0x000DACD0
		private unsafe static int ComputeHash32OrdinalIgnoreCaseSlow(ref char data, int count, uint p0, uint p1)
		{
			char[] array = null;
			Span<char> span2;
			if (count <= 64)
			{
				Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)128], 64);
				span2 = span;
			}
			else
			{
				span2 = (array = ArrayPool<char>.Shared.Rent(count));
			}
			Span<char> span3 = span2;
			int num = Ordinal.ToUpperOrdinal(new ReadOnlySpan<char>(ref data, count), span3);
			int result = Marvin.ComputeHash32(Unsafe.As<char, byte>(MemoryMarshal.GetReference<char>(span3)), (uint)(num * 2), p0, p1);
			if (array != null)
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return result;
		}
	}
}
