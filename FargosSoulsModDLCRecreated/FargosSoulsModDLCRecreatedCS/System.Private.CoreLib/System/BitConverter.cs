using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000D4 RID: 212
	[NullableContext(1)]
	[Nullable(0)]
	public static class BitConverter
	{
		// Token: 0x06000A9D RID: 2717 RVA: 0x000C92D8 File Offset: 0x000C84D8
		public static byte[] GetBytes(bool value)
		{
			return new byte[]
			{
				value ? 1 : 0
			};
		}

		// Token: 0x06000A9E RID: 2718 RVA: 0x000C92F7 File Offset: 0x000C84F7
		[NullableContext(0)]
		public static bool TryWriteBytes(Span<byte> destination, bool value)
		{
			if (destination.Length < 1)
			{
				return false;
			}
			Unsafe.WriteUnaligned<byte>(MemoryMarshal.GetReference<byte>(destination), value ? 1 : 0);
			return true;
		}

		// Token: 0x06000A9F RID: 2719 RVA: 0x000C9318 File Offset: 0x000C8518
		public unsafe static byte[] GetBytes(char value)
		{
			byte[] array = new byte[2];
			*Unsafe.As<byte, char>(ref array[0]) = value;
			return array;
		}

		// Token: 0x06000AA0 RID: 2720 RVA: 0x000C933B File Offset: 0x000C853B
		[NullableContext(0)]
		public static bool TryWriteBytes(Span<byte> destination, char value)
		{
			if (destination.Length < 2)
			{
				return false;
			}
			Unsafe.WriteUnaligned<char>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06000AA1 RID: 2721 RVA: 0x000C9358 File Offset: 0x000C8558
		public unsafe static byte[] GetBytes(short value)
		{
			byte[] array = new byte[2];
			*Unsafe.As<byte, short>(ref array[0]) = value;
			return array;
		}

		// Token: 0x06000AA2 RID: 2722 RVA: 0x000C937B File Offset: 0x000C857B
		[NullableContext(0)]
		public static bool TryWriteBytes(Span<byte> destination, short value)
		{
			if (destination.Length < 2)
			{
				return false;
			}
			Unsafe.WriteUnaligned<short>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06000AA3 RID: 2723 RVA: 0x000C9398 File Offset: 0x000C8598
		public unsafe static byte[] GetBytes(int value)
		{
			byte[] array = new byte[4];
			*Unsafe.As<byte, int>(ref array[0]) = value;
			return array;
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000C93BB File Offset: 0x000C85BB
		[NullableContext(0)]
		public static bool TryWriteBytes(Span<byte> destination, int value)
		{
			if (destination.Length < 4)
			{
				return false;
			}
			Unsafe.WriteUnaligned<int>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000C93D8 File Offset: 0x000C85D8
		public unsafe static byte[] GetBytes(long value)
		{
			byte[] array = new byte[8];
			*Unsafe.As<byte, long>(ref array[0]) = value;
			return array;
		}

		// Token: 0x06000AA6 RID: 2726 RVA: 0x000C93FB File Offset: 0x000C85FB
		[NullableContext(0)]
		public static bool TryWriteBytes(Span<byte> destination, long value)
		{
			if (destination.Length < 8)
			{
				return false;
			}
			Unsafe.WriteUnaligned<long>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06000AA7 RID: 2727 RVA: 0x000C9418 File Offset: 0x000C8618
		[CLSCompliant(false)]
		public unsafe static byte[] GetBytes(ushort value)
		{
			byte[] array = new byte[2];
			*Unsafe.As<byte, ushort>(ref array[0]) = value;
			return array;
		}

		// Token: 0x06000AA8 RID: 2728 RVA: 0x000C943B File Offset: 0x000C863B
		[NullableContext(0)]
		[CLSCompliant(false)]
		public static bool TryWriteBytes(Span<byte> destination, ushort value)
		{
			if (destination.Length < 2)
			{
				return false;
			}
			Unsafe.WriteUnaligned<ushort>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06000AA9 RID: 2729 RVA: 0x000C9458 File Offset: 0x000C8658
		[CLSCompliant(false)]
		public unsafe static byte[] GetBytes(uint value)
		{
			byte[] array = new byte[4];
			*Unsafe.As<byte, uint>(ref array[0]) = value;
			return array;
		}

		// Token: 0x06000AAA RID: 2730 RVA: 0x000C947B File Offset: 0x000C867B
		[NullableContext(0)]
		[CLSCompliant(false)]
		public static bool TryWriteBytes(Span<byte> destination, uint value)
		{
			if (destination.Length < 4)
			{
				return false;
			}
			Unsafe.WriteUnaligned<uint>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x000C9498 File Offset: 0x000C8698
		[CLSCompliant(false)]
		public unsafe static byte[] GetBytes(ulong value)
		{
			byte[] array = new byte[8];
			*Unsafe.As<byte, ulong>(ref array[0]) = value;
			return array;
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x000C94BB File Offset: 0x000C86BB
		[NullableContext(0)]
		[CLSCompliant(false)]
		public static bool TryWriteBytes(Span<byte> destination, ulong value)
		{
			if (destination.Length < 8)
			{
				return false;
			}
			Unsafe.WriteUnaligned<ulong>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000C94D8 File Offset: 0x000C86D8
		public unsafe static byte[] GetBytes(float value)
		{
			byte[] array = new byte[4];
			*Unsafe.As<byte, float>(ref array[0]) = value;
			return array;
		}

		// Token: 0x06000AAE RID: 2734 RVA: 0x000C94FB File Offset: 0x000C86FB
		[NullableContext(0)]
		public static bool TryWriteBytes(Span<byte> destination, float value)
		{
			if (destination.Length < 4)
			{
				return false;
			}
			Unsafe.WriteUnaligned<float>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06000AAF RID: 2735 RVA: 0x000C9518 File Offset: 0x000C8718
		public unsafe static byte[] GetBytes(double value)
		{
			byte[] array = new byte[8];
			*Unsafe.As<byte, double>(ref array[0]) = value;
			return array;
		}

		// Token: 0x06000AB0 RID: 2736 RVA: 0x000C953B File Offset: 0x000C873B
		[NullableContext(0)]
		public static bool TryWriteBytes(Span<byte> destination, double value)
		{
			if (destination.Length < 8)
			{
				return false;
			}
			Unsafe.WriteUnaligned<double>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x06000AB1 RID: 2737 RVA: 0x000C9556 File Offset: 0x000C8756
		public static char ToChar(byte[] value, int startIndex)
		{
			return (char)BitConverter.ToInt16(value, startIndex);
		}

		// Token: 0x06000AB2 RID: 2738 RVA: 0x000C9560 File Offset: 0x000C8760
		[NullableContext(0)]
		public static char ToChar(ReadOnlySpan<byte> value)
		{
			if (value.Length < 2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<char>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x06000AB3 RID: 2739 RVA: 0x000C957D File Offset: 0x000C877D
		public static short ToInt16(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex >= value.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 2)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<short>(ref value[startIndex]);
		}

		// Token: 0x06000AB4 RID: 2740 RVA: 0x000C95B0 File Offset: 0x000C87B0
		[NullableContext(0)]
		public static short ToInt16(ReadOnlySpan<byte> value)
		{
			if (value.Length < 2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<short>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x06000AB5 RID: 2741 RVA: 0x000C95CD File Offset: 0x000C87CD
		public static int ToInt32(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex >= value.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 4)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<int>(ref value[startIndex]);
		}

		// Token: 0x06000AB6 RID: 2742 RVA: 0x000C9600 File Offset: 0x000C8800
		[NullableContext(0)]
		public static int ToInt32(ReadOnlySpan<byte> value)
		{
			if (value.Length < 4)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<int>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x06000AB7 RID: 2743 RVA: 0x000C961D File Offset: 0x000C881D
		public static long ToInt64(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex >= value.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 8)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<long>(ref value[startIndex]);
		}

		// Token: 0x06000AB8 RID: 2744 RVA: 0x000C9650 File Offset: 0x000C8850
		[NullableContext(0)]
		public static long ToInt64(ReadOnlySpan<byte> value)
		{
			if (value.Length < 8)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<long>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x06000AB9 RID: 2745 RVA: 0x000C9556 File Offset: 0x000C8756
		[CLSCompliant(false)]
		public static ushort ToUInt16(byte[] value, int startIndex)
		{
			return (ushort)BitConverter.ToInt16(value, startIndex);
		}

		// Token: 0x06000ABA RID: 2746 RVA: 0x000C966D File Offset: 0x000C886D
		[NullableContext(0)]
		[CLSCompliant(false)]
		public static ushort ToUInt16(ReadOnlySpan<byte> value)
		{
			if (value.Length < 2)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<ushort>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x06000ABB RID: 2747 RVA: 0x000C968A File Offset: 0x000C888A
		[CLSCompliant(false)]
		public static uint ToUInt32(byte[] value, int startIndex)
		{
			return (uint)BitConverter.ToInt32(value, startIndex);
		}

		// Token: 0x06000ABC RID: 2748 RVA: 0x000C9693 File Offset: 0x000C8893
		[NullableContext(0)]
		[CLSCompliant(false)]
		public static uint ToUInt32(ReadOnlySpan<byte> value)
		{
			if (value.Length < 4)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<uint>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x000C96B0 File Offset: 0x000C88B0
		[CLSCompliant(false)]
		public static ulong ToUInt64(byte[] value, int startIndex)
		{
			return (ulong)BitConverter.ToInt64(value, startIndex);
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x000C96B9 File Offset: 0x000C88B9
		[CLSCompliant(false)]
		[NullableContext(0)]
		public static ulong ToUInt64(ReadOnlySpan<byte> value)
		{
			if (value.Length < 8)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<ulong>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x000C96D6 File Offset: 0x000C88D6
		public static float ToSingle(byte[] value, int startIndex)
		{
			return BitConverter.Int32BitsToSingle(BitConverter.ToInt32(value, startIndex));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x000C96E4 File Offset: 0x000C88E4
		[NullableContext(0)]
		public static float ToSingle(ReadOnlySpan<byte> value)
		{
			if (value.Length < 4)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<float>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x000C9701 File Offset: 0x000C8901
		public static double ToDouble(byte[] value, int startIndex)
		{
			return BitConverter.Int64BitsToDouble(BitConverter.ToInt64(value, startIndex));
		}

		// Token: 0x06000AC2 RID: 2754 RVA: 0x000C970F File Offset: 0x000C890F
		[NullableContext(0)]
		public static double ToDouble(ReadOnlySpan<byte> value)
		{
			if (value.Length < 8)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<double>(MemoryMarshal.GetReference<byte>(value));
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x000C972C File Offset: 0x000C892C
		public unsafe static string ToString(byte[] value, int startIndex, int length)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex < 0 || (startIndex >= value.Length && startIndex > 0))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (length < 0)
			{
				throw new ArgumentOutOfRangeException("length", SR.ArgumentOutOfRange_GenericPositive);
			}
			if (startIndex > value.Length - length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Arg_ArrayPlusOffTooSmall, ExceptionArgument.value);
			}
			if (length == 0)
			{
				return string.Empty;
			}
			if (length > 715827882)
			{
				throw new ArgumentOutOfRangeException("length", SR.Format(SR.ArgumentOutOfRange_LengthTooLarge, 715827882));
			}
			return string.Create<ValueTuple<byte[], int, int>>(length * 3 - 1, new ValueTuple<byte[], int, int>(value, startIndex, length), delegate(Span<char> dst, [TupleElementNames(new string[]
			{
				"value",
				"startIndex",
				"length"
			})] ValueTuple<byte[], int, int> state)
			{
				ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>(state.Item1, state.Item2, state.Item3);
				int i = 0;
				int num = 0;
				byte b = *readOnlySpan[i++];
				*dst[num++] = HexConverter.ToCharUpper(b >> 4);
				*dst[num++] = HexConverter.ToCharUpper((int)b);
				while (i < readOnlySpan.Length)
				{
					b = *readOnlySpan[i++];
					*dst[num++] = '-';
					*dst[num++] = HexConverter.ToCharUpper(b >> 4);
					*dst[num++] = HexConverter.ToCharUpper((int)b);
				}
			});
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x000C97DB File Offset: 0x000C89DB
		public static string ToString(byte[] value)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			return BitConverter.ToString(value, 0, value.Length);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x000C97F0 File Offset: 0x000C89F0
		public static string ToString(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			return BitConverter.ToString(value, startIndex, value.Length - startIndex);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x000C9807 File Offset: 0x000C8A07
		public static bool ToBoolean(byte[] value, int startIndex)
		{
			if (value == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.value);
			}
			if (startIndex < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			if (startIndex > value.Length - 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.startIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			return value[startIndex] > 0;
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x000C9832 File Offset: 0x000C8A32
		[NullableContext(0)]
		public static bool ToBoolean(ReadOnlySpan<byte> value)
		{
			if (value.Length < 1)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.value);
			}
			return Unsafe.ReadUnaligned<byte>(MemoryMarshal.GetReference<byte>(value)) > 0;
		}

		// Token: 0x06000AC8 RID: 2760 RVA: 0x000C9854 File Offset: 0x000C8A54
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long DoubleToInt64Bits(double value)
		{
			if (Sse2.X64.IsSupported)
			{
				Vector128<long> value2 = Vector128.CreateScalarUnsafe(value).AsInt64<double>();
				return Sse2.X64.ConvertToInt64(value2);
			}
			return *(long*)(&value);
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x000C9880 File Offset: 0x000C8A80
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double Int64BitsToDouble(long value)
		{
			if (Sse2.X64.IsSupported)
			{
				Vector128<double> vector = Vector128.CreateScalarUnsafe(value).AsDouble<long>();
				return vector.ToScalar<double>();
			}
			return *(double*)(&value);
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x000C98AC File Offset: 0x000C8AAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int SingleToInt32Bits(float value)
		{
			if (Sse2.IsSupported)
			{
				Vector128<int> value2 = Vector128.CreateScalarUnsafe(value).AsInt32<float>();
				return Sse2.ConvertToInt32(value2);
			}
			return *(int*)(&value);
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x000C98D8 File Offset: 0x000C8AD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float Int32BitsToSingle(int value)
		{
			if (Sse2.IsSupported)
			{
				Vector128<float> vector = Vector128.CreateScalarUnsafe(value).AsSingle<int>();
				return vector.ToScalar<float>();
			}
			return *(float*)(&value);
		}

		// Token: 0x06000ACC RID: 2764 RVA: 0x000C9903 File Offset: 0x000C8B03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static short HalfToInt16Bits(Half value)
		{
			return *(short*)(&value);
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x000C9909 File Offset: 0x000C8B09
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe static Half Int16BitsToHalf(short value)
		{
			return *(Half*)(&value);
		}

		// Token: 0x0400029F RID: 671
		[Intrinsic]
		public static readonly bool IsLittleEndian = true;
	}
}
