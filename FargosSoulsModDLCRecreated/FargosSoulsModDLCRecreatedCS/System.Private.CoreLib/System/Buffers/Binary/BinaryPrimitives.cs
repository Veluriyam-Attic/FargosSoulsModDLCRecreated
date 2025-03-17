using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Buffers.Binary
{
	// Token: 0x0200025F RID: 607
	public static class BinaryPrimitives
	{
		// Token: 0x060024DF RID: 9439 RVA: 0x000AC098 File Offset: 0x000AB298
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte ReverseEndianness(sbyte value)
		{
			return value;
		}

		// Token: 0x060024E0 RID: 9440 RVA: 0x0013FE66 File Offset: 0x0013F066
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReverseEndianness(short value)
		{
			return (short)BinaryPrimitives.ReverseEndianness((ushort)value);
		}

		// Token: 0x060024E1 RID: 9441 RVA: 0x0013FE70 File Offset: 0x0013F070
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReverseEndianness(int value)
		{
			return (int)BinaryPrimitives.ReverseEndianness((uint)value);
		}

		// Token: 0x060024E2 RID: 9442 RVA: 0x0013FE78 File Offset: 0x0013F078
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReverseEndianness(long value)
		{
			return (long)BinaryPrimitives.ReverseEndianness((ulong)value);
		}

		// Token: 0x060024E3 RID: 9443 RVA: 0x000AC098 File Offset: 0x000AB298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte ReverseEndianness(byte value)
		{
			return value;
		}

		// Token: 0x060024E4 RID: 9444 RVA: 0x0013FE80 File Offset: 0x0013F080
		[CLSCompliant(false)]
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReverseEndianness(ushort value)
		{
			return (ushort)((value >> 8) + ((int)value << 8));
		}

		// Token: 0x060024E5 RID: 9445 RVA: 0x0013FE8A File Offset: 0x0013F08A
		[Intrinsic]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReverseEndianness(uint value)
		{
			return BitOperations.RotateRight(value & 16711935U, 8) + BitOperations.RotateLeft(value & 4278255360U, 8);
		}

		// Token: 0x060024E6 RID: 9446 RVA: 0x0013FEA7 File Offset: 0x0013F0A7
		[Intrinsic]
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReverseEndianness(ulong value)
		{
			return ((ulong)BinaryPrimitives.ReverseEndianness((uint)value) << 32) + (ulong)BinaryPrimitives.ReverseEndianness((uint)(value >> 32));
		}

		// Token: 0x060024E7 RID: 9447 RVA: 0x0013FEC0 File Offset: 0x0013F0C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double ReadDoubleBigEndian(ReadOnlySpan<byte> source)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.Read<double>(source);
			}
			return BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(MemoryMarshal.Read<long>(source)));
		}

		// Token: 0x060024E8 RID: 9448 RVA: 0x0013FEE0 File Offset: 0x0013F0E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReadInt16BigEndian(ReadOnlySpan<byte> source)
		{
			short num = MemoryMarshal.Read<short>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024E9 RID: 9449 RVA: 0x0013FF04 File Offset: 0x0013F104
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadInt32BigEndian(ReadOnlySpan<byte> source)
		{
			int num = MemoryMarshal.Read<int>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024EA RID: 9450 RVA: 0x0013FF28 File Offset: 0x0013F128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReadInt64BigEndian(ReadOnlySpan<byte> source)
		{
			long num = MemoryMarshal.Read<long>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024EB RID: 9451 RVA: 0x0013FF4B File Offset: 0x0013F14B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ReadSingleBigEndian(ReadOnlySpan<byte> source)
		{
			if (!BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.Read<float>(source);
			}
			return BitConverter.Int32BitsToSingle(BinaryPrimitives.ReverseEndianness(MemoryMarshal.Read<int>(source)));
		}

		// Token: 0x060024EC RID: 9452 RVA: 0x0013FF6C File Offset: 0x0013F16C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReadUInt16BigEndian(ReadOnlySpan<byte> source)
		{
			ushort num = MemoryMarshal.Read<ushort>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024ED RID: 9453 RVA: 0x0013FF90 File Offset: 0x0013F190
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReadUInt32BigEndian(ReadOnlySpan<byte> source)
		{
			uint num = MemoryMarshal.Read<uint>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024EE RID: 9454 RVA: 0x0013FFB4 File Offset: 0x0013F1B4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReadUInt64BigEndian(ReadOnlySpan<byte> source)
		{
			ulong num = MemoryMarshal.Read<ulong>(source);
			if (BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024EF RID: 9455 RVA: 0x0013FFD8 File Offset: 0x0013F1D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadDoubleBigEndian(ReadOnlySpan<byte> source, out double value)
		{
			if (BitConverter.IsLittleEndian)
			{
				long value2;
				bool result = MemoryMarshal.TryRead<long>(source, out value2);
				value = BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(value2));
				return result;
			}
			return MemoryMarshal.TryRead<double>(source, out value);
		}

		// Token: 0x060024F0 RID: 9456 RVA: 0x0014000C File Offset: 0x0013F20C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt16BigEndian(ReadOnlySpan<byte> source, out short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				short value2;
				bool result = MemoryMarshal.TryRead<short>(source, out value2);
				value = BinaryPrimitives.ReverseEndianness(value2);
				return result;
			}
			return MemoryMarshal.TryRead<short>(source, out value);
		}

		// Token: 0x060024F1 RID: 9457 RVA: 0x0014003C File Offset: 0x0013F23C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt32BigEndian(ReadOnlySpan<byte> source, out int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				int value2;
				bool result = MemoryMarshal.TryRead<int>(source, out value2);
				value = BinaryPrimitives.ReverseEndianness(value2);
				return result;
			}
			return MemoryMarshal.TryRead<int>(source, out value);
		}

		// Token: 0x060024F2 RID: 9458 RVA: 0x0014006C File Offset: 0x0013F26C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt64BigEndian(ReadOnlySpan<byte> source, out long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				long value2;
				bool result = MemoryMarshal.TryRead<long>(source, out value2);
				value = BinaryPrimitives.ReverseEndianness(value2);
				return result;
			}
			return MemoryMarshal.TryRead<long>(source, out value);
		}

		// Token: 0x060024F3 RID: 9459 RVA: 0x0014009C File Offset: 0x0013F29C
		public static bool TryReadSingleBigEndian(ReadOnlySpan<byte> source, out float value)
		{
			if (BitConverter.IsLittleEndian)
			{
				int value2;
				bool result = MemoryMarshal.TryRead<int>(source, out value2);
				value = BitConverter.Int32BitsToSingle(BinaryPrimitives.ReverseEndianness(value2));
				return result;
			}
			return MemoryMarshal.TryRead<float>(source, out value);
		}

		// Token: 0x060024F4 RID: 9460 RVA: 0x001400D0 File Offset: 0x0013F2D0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt16BigEndian(ReadOnlySpan<byte> source, out ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				ushort value2;
				bool result = MemoryMarshal.TryRead<ushort>(source, out value2);
				value = BinaryPrimitives.ReverseEndianness(value2);
				return result;
			}
			return MemoryMarshal.TryRead<ushort>(source, out value);
		}

		// Token: 0x060024F5 RID: 9461 RVA: 0x00140100 File Offset: 0x0013F300
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt32BigEndian(ReadOnlySpan<byte> source, out uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				uint value2;
				bool result = MemoryMarshal.TryRead<uint>(source, out value2);
				value = BinaryPrimitives.ReverseEndianness(value2);
				return result;
			}
			return MemoryMarshal.TryRead<uint>(source, out value);
		}

		// Token: 0x060024F6 RID: 9462 RVA: 0x00140130 File Offset: 0x0013F330
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt64BigEndian(ReadOnlySpan<byte> source, out ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				ulong value2;
				bool result = MemoryMarshal.TryRead<ulong>(source, out value2);
				value = BinaryPrimitives.ReverseEndianness(value2);
				return result;
			}
			return MemoryMarshal.TryRead<ulong>(source, out value);
		}

		// Token: 0x060024F7 RID: 9463 RVA: 0x0014015E File Offset: 0x0013F35E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double ReadDoubleLittleEndian(ReadOnlySpan<byte> source)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.Read<double>(source);
			}
			return BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(MemoryMarshal.Read<long>(source)));
		}

		// Token: 0x060024F8 RID: 9464 RVA: 0x00140180 File Offset: 0x0013F380
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short ReadInt16LittleEndian(ReadOnlySpan<byte> source)
		{
			short num = MemoryMarshal.Read<short>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024F9 RID: 9465 RVA: 0x001401A4 File Offset: 0x0013F3A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int ReadInt32LittleEndian(ReadOnlySpan<byte> source)
		{
			int num = MemoryMarshal.Read<int>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024FA RID: 9466 RVA: 0x001401C8 File Offset: 0x0013F3C8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long ReadInt64LittleEndian(ReadOnlySpan<byte> source)
		{
			long num = MemoryMarshal.Read<long>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024FB RID: 9467 RVA: 0x001401EB File Offset: 0x0013F3EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float ReadSingleLittleEndian(ReadOnlySpan<byte> source)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.Read<float>(source);
			}
			return BitConverter.Int32BitsToSingle(BinaryPrimitives.ReverseEndianness(MemoryMarshal.Read<int>(source)));
		}

		// Token: 0x060024FC RID: 9468 RVA: 0x0014020C File Offset: 0x0013F40C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort ReadUInt16LittleEndian(ReadOnlySpan<byte> source)
		{
			ushort num = MemoryMarshal.Read<ushort>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024FD RID: 9469 RVA: 0x00140230 File Offset: 0x0013F430
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint ReadUInt32LittleEndian(ReadOnlySpan<byte> source)
		{
			uint num = MemoryMarshal.Read<uint>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024FE RID: 9470 RVA: 0x00140254 File Offset: 0x0013F454
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong ReadUInt64LittleEndian(ReadOnlySpan<byte> source)
		{
			ulong num = MemoryMarshal.Read<ulong>(source);
			if (!BitConverter.IsLittleEndian)
			{
				num = BinaryPrimitives.ReverseEndianness(num);
			}
			return num;
		}

		// Token: 0x060024FF RID: 9471 RVA: 0x00140278 File Offset: 0x0013F478
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadDoubleLittleEndian(ReadOnlySpan<byte> source, out double value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				long value2;
				bool result = MemoryMarshal.TryRead<long>(source, out value2);
				value = BitConverter.Int64BitsToDouble(BinaryPrimitives.ReverseEndianness(value2));
				return result;
			}
			return MemoryMarshal.TryRead<double>(source, out value);
		}

		// Token: 0x06002500 RID: 9472 RVA: 0x001402AC File Offset: 0x0013F4AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt16LittleEndian(ReadOnlySpan<byte> source, out short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.TryRead<short>(source, out value);
			}
			short value2;
			bool result = MemoryMarshal.TryRead<short>(source, out value2);
			value = BinaryPrimitives.ReverseEndianness(value2);
			return result;
		}

		// Token: 0x06002501 RID: 9473 RVA: 0x001402DC File Offset: 0x0013F4DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt32LittleEndian(ReadOnlySpan<byte> source, out int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.TryRead<int>(source, out value);
			}
			int value2;
			bool result = MemoryMarshal.TryRead<int>(source, out value2);
			value = BinaryPrimitives.ReverseEndianness(value2);
			return result;
		}

		// Token: 0x06002502 RID: 9474 RVA: 0x0014030C File Offset: 0x0013F50C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadInt64LittleEndian(ReadOnlySpan<byte> source, out long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.TryRead<long>(source, out value);
			}
			long value2;
			bool result = MemoryMarshal.TryRead<long>(source, out value2);
			value = BinaryPrimitives.ReverseEndianness(value2);
			return result;
		}

		// Token: 0x06002503 RID: 9475 RVA: 0x0014033C File Offset: 0x0013F53C
		public static bool TryReadSingleLittleEndian(ReadOnlySpan<byte> source, out float value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				int value2;
				bool result = MemoryMarshal.TryRead<int>(source, out value2);
				value = BitConverter.Int32BitsToSingle(BinaryPrimitives.ReverseEndianness(value2));
				return result;
			}
			return MemoryMarshal.TryRead<float>(source, out value);
		}

		// Token: 0x06002504 RID: 9476 RVA: 0x00140370 File Offset: 0x0013F570
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt16LittleEndian(ReadOnlySpan<byte> source, out ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.TryRead<ushort>(source, out value);
			}
			ushort value2;
			bool result = MemoryMarshal.TryRead<ushort>(source, out value2);
			value = BinaryPrimitives.ReverseEndianness(value2);
			return result;
		}

		// Token: 0x06002505 RID: 9477 RVA: 0x001403A0 File Offset: 0x0013F5A0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt32LittleEndian(ReadOnlySpan<byte> source, out uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.TryRead<uint>(source, out value);
			}
			uint value2;
			bool result = MemoryMarshal.TryRead<uint>(source, out value2);
			value = BinaryPrimitives.ReverseEndianness(value2);
			return result;
		}

		// Token: 0x06002506 RID: 9478 RVA: 0x001403D0 File Offset: 0x0013F5D0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryReadUInt64LittleEndian(ReadOnlySpan<byte> source, out ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				return MemoryMarshal.TryRead<ulong>(source, out value);
			}
			ulong value2;
			bool result = MemoryMarshal.TryRead<ulong>(source, out value2);
			value = BinaryPrimitives.ReverseEndianness(value2);
			return result;
		}

		// Token: 0x06002507 RID: 9479 RVA: 0x00140400 File Offset: 0x0013F600
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteDoubleBigEndian(Span<byte> destination, double value)
		{
			if (BitConverter.IsLittleEndian)
			{
				long num = BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(value));
				MemoryMarshal.Write<long>(destination, ref num);
				return;
			}
			MemoryMarshal.Write<double>(destination, ref value);
		}

		// Token: 0x06002508 RID: 9480 RVA: 0x00140431 File Offset: 0x0013F631
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt16BigEndian(Span<byte> destination, short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<short>(destination, ref value);
		}

		// Token: 0x06002509 RID: 9481 RVA: 0x0014044A File Offset: 0x0013F64A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt32BigEndian(Span<byte> destination, int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<int>(destination, ref value);
		}

		// Token: 0x0600250A RID: 9482 RVA: 0x00140463 File Offset: 0x0013F663
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt64BigEndian(Span<byte> destination, long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<long>(destination, ref value);
		}

		// Token: 0x0600250B RID: 9483 RVA: 0x0014047C File Offset: 0x0013F67C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteSingleBigEndian(Span<byte> destination, float value)
		{
			if (BitConverter.IsLittleEndian)
			{
				int num = BinaryPrimitives.ReverseEndianness(BitConverter.SingleToInt32Bits(value));
				MemoryMarshal.Write<int>(destination, ref num);
				return;
			}
			MemoryMarshal.Write<float>(destination, ref value);
		}

		// Token: 0x0600250C RID: 9484 RVA: 0x001404AD File Offset: 0x0013F6AD
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt16BigEndian(Span<byte> destination, ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ushort>(destination, ref value);
		}

		// Token: 0x0600250D RID: 9485 RVA: 0x001404C6 File Offset: 0x0013F6C6
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt32BigEndian(Span<byte> destination, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<uint>(destination, ref value);
		}

		// Token: 0x0600250E RID: 9486 RVA: 0x001404DF File Offset: 0x0013F6DF
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt64BigEndian(Span<byte> destination, ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ulong>(destination, ref value);
		}

		// Token: 0x0600250F RID: 9487 RVA: 0x001404F8 File Offset: 0x0013F6F8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteDoubleBigEndian(Span<byte> destination, double value)
		{
			if (BitConverter.IsLittleEndian)
			{
				long num = BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(value));
				return MemoryMarshal.TryWrite<long>(destination, ref num);
			}
			return MemoryMarshal.TryWrite<double>(destination, ref value);
		}

		// Token: 0x06002510 RID: 9488 RVA: 0x00140529 File Offset: 0x0013F729
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt16BigEndian(Span<byte> destination, short value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<short>(destination, ref value);
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x00140542 File Offset: 0x0013F742
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt32BigEndian(Span<byte> destination, int value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<int>(destination, ref value);
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x0014055B File Offset: 0x0013F75B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt64BigEndian(Span<byte> destination, long value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<long>(destination, ref value);
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x00140574 File Offset: 0x0013F774
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteSingleBigEndian(Span<byte> destination, float value)
		{
			if (BitConverter.IsLittleEndian)
			{
				int num = BinaryPrimitives.ReverseEndianness(BitConverter.SingleToInt32Bits(value));
				return MemoryMarshal.TryWrite<int>(destination, ref num);
			}
			return MemoryMarshal.TryWrite<float>(destination, ref value);
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x001405A5 File Offset: 0x0013F7A5
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt16BigEndian(Span<byte> destination, ushort value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ushort>(destination, ref value);
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x001405BE File Offset: 0x0013F7BE
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt32BigEndian(Span<byte> destination, uint value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<uint>(destination, ref value);
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x001405D7 File Offset: 0x0013F7D7
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt64BigEndian(Span<byte> destination, ulong value)
		{
			if (BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ulong>(destination, ref value);
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x001405F0 File Offset: 0x0013F7F0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteDoubleLittleEndian(Span<byte> destination, double value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				long num = BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(value));
				MemoryMarshal.Write<long>(destination, ref num);
				return;
			}
			MemoryMarshal.Write<double>(destination, ref value);
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x00140621 File Offset: 0x0013F821
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt16LittleEndian(Span<byte> destination, short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<short>(destination, ref value);
		}

		// Token: 0x06002519 RID: 9497 RVA: 0x0014063A File Offset: 0x0013F83A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt32LittleEndian(Span<byte> destination, int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<int>(destination, ref value);
		}

		// Token: 0x0600251A RID: 9498 RVA: 0x00140653 File Offset: 0x0013F853
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteInt64LittleEndian(Span<byte> destination, long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<long>(destination, ref value);
		}

		// Token: 0x0600251B RID: 9499 RVA: 0x0014066C File Offset: 0x0013F86C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteSingleLittleEndian(Span<byte> destination, float value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				int num = BinaryPrimitives.ReverseEndianness(BitConverter.SingleToInt32Bits(value));
				MemoryMarshal.Write<int>(destination, ref num);
				return;
			}
			MemoryMarshal.Write<float>(destination, ref value);
		}

		// Token: 0x0600251C RID: 9500 RVA: 0x0014069D File Offset: 0x0013F89D
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt16LittleEndian(Span<byte> destination, ushort value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ushort>(destination, ref value);
		}

		// Token: 0x0600251D RID: 9501 RVA: 0x001406B6 File Offset: 0x0013F8B6
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt32LittleEndian(Span<byte> destination, uint value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<uint>(destination, ref value);
		}

		// Token: 0x0600251E RID: 9502 RVA: 0x001406CF File Offset: 0x0013F8CF
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void WriteUInt64LittleEndian(Span<byte> destination, ulong value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			MemoryMarshal.Write<ulong>(destination, ref value);
		}

		// Token: 0x0600251F RID: 9503 RVA: 0x001406E8 File Offset: 0x0013F8E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteDoubleLittleEndian(Span<byte> destination, double value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				long num = BinaryPrimitives.ReverseEndianness(BitConverter.DoubleToInt64Bits(value));
				return MemoryMarshal.TryWrite<long>(destination, ref num);
			}
			return MemoryMarshal.TryWrite<double>(destination, ref value);
		}

		// Token: 0x06002520 RID: 9504 RVA: 0x00140719 File Offset: 0x0013F919
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt16LittleEndian(Span<byte> destination, short value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<short>(destination, ref value);
		}

		// Token: 0x06002521 RID: 9505 RVA: 0x00140732 File Offset: 0x0013F932
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt32LittleEndian(Span<byte> destination, int value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<int>(destination, ref value);
		}

		// Token: 0x06002522 RID: 9506 RVA: 0x0014074B File Offset: 0x0013F94B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteInt64LittleEndian(Span<byte> destination, long value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<long>(destination, ref value);
		}

		// Token: 0x06002523 RID: 9507 RVA: 0x00140764 File Offset: 0x0013F964
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteSingleLittleEndian(Span<byte> destination, float value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				int num = BinaryPrimitives.ReverseEndianness(BitConverter.SingleToInt32Bits(value));
				return MemoryMarshal.TryWrite<int>(destination, ref num);
			}
			return MemoryMarshal.TryWrite<float>(destination, ref value);
		}

		// Token: 0x06002524 RID: 9508 RVA: 0x00140795 File Offset: 0x0013F995
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt16LittleEndian(Span<byte> destination, ushort value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ushort>(destination, ref value);
		}

		// Token: 0x06002525 RID: 9509 RVA: 0x001407AE File Offset: 0x0013F9AE
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt32LittleEndian(Span<byte> destination, uint value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<uint>(destination, ref value);
		}

		// Token: 0x06002526 RID: 9510 RVA: 0x001407C7 File Offset: 0x0013F9C7
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWriteUInt64LittleEndian(Span<byte> destination, ulong value)
		{
			if (!BitConverter.IsLittleEndian)
			{
				value = BinaryPrimitives.ReverseEndianness(value);
			}
			return MemoryMarshal.TryWrite<ulong>(destination, ref value);
		}
	}
}
