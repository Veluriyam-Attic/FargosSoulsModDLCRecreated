using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x020001C8 RID: 456
	public static class BitOperations
	{
		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001BA0 RID: 7072 RVA: 0x0010239A File Offset: 0x0010159A
		private unsafe static ReadOnlySpan<byte> TrailingZeroCountDeBruijn
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.3BF63951626584EB1653F9B8DBB590A5EE1EAE1135A904B9317C3773896DF076), 32);
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001BA1 RID: 7073 RVA: 0x001023A8 File Offset: 0x001015A8
		private unsafe static ReadOnlySpan<byte> Log2DeBruijn
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.4BCD43D478B9229AB7A13406353712C7944B60348C36B4D0E6B789D10F697652), 32);
			}
		}

		// Token: 0x06001BA2 RID: 7074 RVA: 0x001023B6 File Offset: 0x001015B6
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(uint value)
		{
			if (Lzcnt.IsSupported)
			{
				return (int)Lzcnt.LeadingZeroCount(value);
			}
			if (ArmBase.IsSupported)
			{
				return ArmBase.LeadingZeroCount(value);
			}
			if (value == 0U)
			{
				return 32;
			}
			if (X86Base.IsSupported)
			{
				return (int)(31U ^ X86Base.BitScanReverse(value));
			}
			return 31 ^ BitOperations.Log2SoftwareFallback(value);
		}

		// Token: 0x06001BA3 RID: 7075 RVA: 0x001023F4 File Offset: 0x001015F4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LeadingZeroCount(ulong value)
		{
			if (Lzcnt.X64.IsSupported)
			{
				return (int)Lzcnt.X64.LeadingZeroCount(value);
			}
			if (ArmBase.Arm64.IsSupported)
			{
				return ArmBase.Arm64.LeadingZeroCount(value);
			}
			if (X86Base.X64.IsSupported)
			{
				if (value != 0UL)
				{
					return 63 ^ (int)X86Base.X64.BitScanReverse(value);
				}
				return 64;
			}
			else
			{
				uint num = (uint)(value >> 32);
				if (num == 0U)
				{
					return 32 + BitOperations.LeadingZeroCount((uint)value);
				}
				return BitOperations.LeadingZeroCount(num);
			}
		}

		// Token: 0x06001BA4 RID: 7076 RVA: 0x00102450 File Offset: 0x00101650
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Log2(uint value)
		{
			value |= 1U;
			if (Lzcnt.IsSupported)
			{
				return (int)(31U ^ Lzcnt.LeadingZeroCount(value));
			}
			if (ArmBase.IsSupported)
			{
				return 31 ^ ArmBase.LeadingZeroCount(value);
			}
			if (X86Base.IsSupported)
			{
				return (int)X86Base.BitScanReverse(value);
			}
			return BitOperations.Log2SoftwareFallback(value);
		}

		// Token: 0x06001BA5 RID: 7077 RVA: 0x00102490 File Offset: 0x00101690
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Log2(ulong value)
		{
			value |= 1UL;
			if (Lzcnt.X64.IsSupported)
			{
				return 63 ^ (int)Lzcnt.X64.LeadingZeroCount(value);
			}
			if (ArmBase.Arm64.IsSupported)
			{
				return 63 ^ ArmBase.Arm64.LeadingZeroCount(value);
			}
			if (X86Base.X64.IsSupported)
			{
				return (int)X86Base.X64.BitScanReverse(value);
			}
			uint num = (uint)(value >> 32);
			if (num == 0U)
			{
				return BitOperations.Log2((uint)value);
			}
			return 32 + BitOperations.Log2(num);
		}

		// Token: 0x06001BA6 RID: 7078 RVA: 0x001024F0 File Offset: 0x001016F0
		private unsafe static int Log2SoftwareFallback(uint value)
		{
			value |= value >> 1;
			value |= value >> 2;
			value |= value >> 4;
			value |= value >> 8;
			value |= value >> 16;
			return (int)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(BitOperations.Log2DeBruijn), (IntPtr)((int)(value * 130329821U >> 27))));
		}

		// Token: 0x06001BA7 RID: 7079 RVA: 0x00102540 File Offset: 0x00101740
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PopCount(uint value)
		{
			if (Popcnt.IsSupported)
			{
				return (int)Popcnt.PopCount(value);
			}
			if (AdvSimd.Arm64.IsSupported)
			{
				Vector64<uint> vector = Vector64.CreateScalar(value);
				Vector64<byte> vector2 = AdvSimd.Arm64.AddAcross(AdvSimd.PopCount(vector.AsByte<uint>()));
				return (int)vector2.ToScalar<byte>();
			}
			return BitOperations.<PopCount>g__SoftwareFallback|9_0(value);
		}

		// Token: 0x06001BA8 RID: 7080 RVA: 0x00102588 File Offset: 0x00101788
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int PopCount(ulong value)
		{
			if (Popcnt.X64.IsSupported)
			{
				return (int)Popcnt.X64.PopCount(value);
			}
			if (AdvSimd.Arm64.IsSupported)
			{
				Vector64<ulong> vector = Vector64.Create(value);
				Vector64<byte> vector2 = AdvSimd.Arm64.AddAcross(AdvSimd.PopCount(vector.AsByte<ulong>()));
				return (int)vector2.ToScalar<byte>();
			}
			return BitOperations.<PopCount>g__SoftwareFallback|10_0(value);
		}

		// Token: 0x06001BA9 RID: 7081 RVA: 0x001025D0 File Offset: 0x001017D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(int value)
		{
			return BitOperations.TrailingZeroCount((uint)value);
		}

		// Token: 0x06001BAA RID: 7082 RVA: 0x001025D8 File Offset: 0x001017D8
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int TrailingZeroCount(uint value)
		{
			if (Bmi1.IsSupported)
			{
				return (int)Bmi1.TrailingZeroCount(value);
			}
			if (ArmBase.IsSupported)
			{
				return ArmBase.LeadingZeroCount(ArmBase.ReverseElementBits(value));
			}
			if (value == 0U)
			{
				return 32;
			}
			if (X86Base.IsSupported)
			{
				return (int)X86Base.BitScanForward(value);
			}
			return (int)(*Unsafe.AddByteOffset<byte>(MemoryMarshal.GetReference<byte>(BitOperations.TrailingZeroCountDeBruijn), (IntPtr)((int)((value & -value) * 125613361U >> 27))));
		}

		// Token: 0x06001BAB RID: 7083 RVA: 0x0010263C File Offset: 0x0010183C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(long value)
		{
			return BitOperations.TrailingZeroCount((ulong)value);
		}

		// Token: 0x06001BAC RID: 7084 RVA: 0x00102644 File Offset: 0x00101844
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int TrailingZeroCount(ulong value)
		{
			if (Bmi1.X64.IsSupported)
			{
				return (int)Bmi1.X64.TrailingZeroCount(value);
			}
			if (ArmBase.Arm64.IsSupported)
			{
				return ArmBase.Arm64.LeadingZeroCount(ArmBase.Arm64.ReverseElementBits(value));
			}
			if (X86Base.X64.IsSupported)
			{
				if (value != 0UL)
				{
					return (int)X86Base.X64.BitScanForward(value);
				}
				return 64;
			}
			else
			{
				uint num = (uint)value;
				if (num == 0U)
				{
					return 32 + BitOperations.TrailingZeroCount((uint)(value >> 32));
				}
				return BitOperations.TrailingZeroCount(num);
			}
		}

		// Token: 0x06001BAD RID: 7085 RVA: 0x001026A2 File Offset: 0x001018A2
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RotateLeft(uint value, int offset)
		{
			return value << offset | value >> 32 - offset;
		}

		// Token: 0x06001BAE RID: 7086 RVA: 0x001026B4 File Offset: 0x001018B4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong RotateLeft(ulong value, int offset)
		{
			return value << offset | value >> 64 - offset;
		}

		// Token: 0x06001BAF RID: 7087 RVA: 0x001026C6 File Offset: 0x001018C6
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint RotateRight(uint value, int offset)
		{
			return value >> offset | value << 32 - offset;
		}

		// Token: 0x06001BB0 RID: 7088 RVA: 0x001026D8 File Offset: 0x001018D8
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong RotateRight(ulong value, int offset)
		{
			return value >> offset | value << 64 - offset;
		}

		// Token: 0x06001BB1 RID: 7089 RVA: 0x001026EA File Offset: 0x001018EA
		[CompilerGenerated]
		internal static int <PopCount>g__SoftwareFallback|9_0(uint value)
		{
			value -= (value >> 1 & 1431655765U);
			value = (value & 858993459U) + (value >> 2 & 858993459U);
			value = (value + (value >> 4) & 252645135U) * 16843009U >> 24;
			return (int)value;
		}

		// Token: 0x06001BB2 RID: 7090 RVA: 0x00102724 File Offset: 0x00101924
		[CompilerGenerated]
		internal static int <PopCount>g__SoftwareFallback|10_0(ulong value)
		{
			value -= (value >> 1 & 6148914691236517205UL);
			value = (value & 3689348814741910323UL) + (value >> 2 & 3689348814741910323UL);
			value = (value + (value >> 4) & 1085102592571150095UL) * 72340172838076673UL >> 56;
			return (int)value;
		}
	}
}
