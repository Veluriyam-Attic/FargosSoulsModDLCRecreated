using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200044F RID: 1103
	public static class MemoryMarshal
	{
		// Token: 0x060043A7 RID: 17319 RVA: 0x00176F6F File Offset: 0x0017616F
		[NullableContext(1)]
		[Intrinsic]
		[NonVersionable]
		public static ref T GetArrayDataReference<[Nullable(2)] T>(T[] array)
		{
			return Unsafe.As<byte, T>(ref Unsafe.As<RawArrayData>(array).Data);
		}

		// Token: 0x060043A8 RID: 17320 RVA: 0x00176F81 File Offset: 0x00176181
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<byte> AsBytes<T>(Span<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new Span<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x060043A9 RID: 17321 RVA: 0x00176FB6 File Offset: 0x001761B6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<byte> AsBytes<T>(ReadOnlySpan<T> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			return new ReadOnlySpan<byte>(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), checked(span.Length * Unsafe.SizeOf<T>()));
		}

		// Token: 0x060043AA RID: 17322 RVA: 0x00176FEB File Offset: 0x001761EB
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static Memory<T> AsMemory<T>([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlyMemory<T> memory)
		{
			return *Unsafe.As<ReadOnlyMemory<T>, Memory<T>>(ref memory);
		}

		// Token: 0x060043AB RID: 17323 RVA: 0x00176FF9 File Offset: 0x001761F9
		[NullableContext(1)]
		public static ref T GetReference<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] Span<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x060043AC RID: 17324 RVA: 0x00177007 File Offset: 0x00176207
		[NullableContext(1)]
		public static ref T GetReference<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> span)
		{
			return span._pointer.Value;
		}

		// Token: 0x060043AD RID: 17325 RVA: 0x00177015 File Offset: 0x00176215
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(Span<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x060043AE RID: 17326 RVA: 0x00177034 File Offset: 0x00176234
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static ref T GetNonNullPinnableReference<T>(ReadOnlySpan<T> span)
		{
			if (span.Length == 0)
			{
				return Unsafe.AsRef<T>(1);
			}
			return span._pointer.Value;
		}

		// Token: 0x060043AF RID: 17327 RVA: 0x00177054 File Offset: 0x00176254
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Span<TTo> Cast<TFrom, TTo>(Span<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			uint num = (uint)Unsafe.SizeOf<TFrom>();
			uint num2 = (uint)Unsafe.SizeOf<TTo>();
			uint length = (uint)span.Length;
			int length2;
			if (num == num2)
			{
				length2 = (int)length;
			}
			else if (num == 1U)
			{
				length2 = (int)(length / num2);
			}
			else
			{
				ulong num3 = (ulong)length * (ulong)num / (ulong)num2;
				length2 = checked((int)num3);
			}
			return new Span<TTo>(Unsafe.As<TFrom, TTo>(span._pointer.Value), length2);
		}

		// Token: 0x060043B0 RID: 17328 RVA: 0x001770D8 File Offset: 0x001762D8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<TTo> Cast<TFrom, TTo>(ReadOnlySpan<TFrom> span) where TFrom : struct where TTo : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TFrom>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TFrom));
			}
			if (RuntimeHelpers.IsReferenceOrContainsReferences<TTo>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(TTo));
			}
			uint num = (uint)Unsafe.SizeOf<TFrom>();
			uint num2 = (uint)Unsafe.SizeOf<TTo>();
			uint length = (uint)span.Length;
			int length2;
			if (num == num2)
			{
				length2 = (int)length;
			}
			else if (num == 1U)
			{
				length2 = (int)(length / num2);
			}
			else
			{
				ulong num3 = (ulong)length * (ulong)num / (ulong)num2;
				length2 = checked((int)num3);
			}
			return new ReadOnlySpan<TTo>(Unsafe.As<TFrom, TTo>(MemoryMarshal.GetReference<TFrom>(span)), length2);
		}

		// Token: 0x060043B1 RID: 17329 RVA: 0x00177156 File Offset: 0x00176356
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> CreateSpan<[Nullable(2)] T>(ref T reference, int length)
		{
			return new Span<T>(ref reference, length);
		}

		// Token: 0x060043B2 RID: 17330 RVA: 0x0017715F File Offset: 0x0017635F
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ReadOnlySpan<T> CreateReadOnlySpan<[Nullable(2)] T>(ref T reference, int length)
		{
			return new ReadOnlySpan<T>(ref reference, length);
		}

		// Token: 0x060043B3 RID: 17331 RVA: 0x00177168 File Offset: 0x00176368
		[NullableContext(2)]
		public static bool TryGetArray<T>([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlyMemory<T> memory, [Nullable(new byte[]
		{
			0,
			1
		})] out ArraySegment<T> segment)
		{
			int num;
			int num2;
			object objectStartLength = memory.GetObjectStartLength(out num, out num2);
			if (objectStartLength != null && (!(typeof(T) == typeof(char)) || !(objectStartLength.GetType() == typeof(string))))
			{
				if (RuntimeHelpers.ObjectHasComponentSize(objectStartLength))
				{
					segment = new ArraySegment<T>(Unsafe.As<T[]>(objectStartLength), num & int.MaxValue, num2);
					return true;
				}
				ArraySegment<T> arraySegment;
				if (Unsafe.As<MemoryManager<T>>(objectStartLength).TryGetArray(out arraySegment))
				{
					segment = new ArraySegment<T>(arraySegment.Array, arraySegment.Offset + num, num2);
					return true;
				}
			}
			if (num2 == 0)
			{
				segment = ArraySegment<T>.Empty;
				return true;
			}
			segment = default(ArraySegment<T>);
			return false;
		}

		// Token: 0x060043B4 RID: 17332 RVA: 0x00177224 File Offset: 0x00176424
		[NullableContext(2)]
		public static bool TryGetMemoryManager<T, [Nullable(0)] TManager>([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlyMemory<T> memory, [NotNullWhen(true)] out TManager manager) where TManager : MemoryManager<T>
		{
			int num;
			int num2;
			return (manager = (memory.GetObjectStartLength(out num, out num2) as TManager)) != null;
		}

		// Token: 0x060043B5 RID: 17333 RVA: 0x00177258 File Offset: 0x00176458
		[NullableContext(2)]
		public static bool TryGetMemoryManager<T, [Nullable(0)] TManager>([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlyMemory<T> memory, [NotNullWhen(true)] out TManager manager, out int start, out int length) where TManager : MemoryManager<T>
		{
			if ((manager = (memory.GetObjectStartLength(out start, out length) as TManager)) == null)
			{
				start = 0;
				length = 0;
				return false;
			}
			return true;
		}

		// Token: 0x060043B6 RID: 17334 RVA: 0x00177291 File Offset: 0x00176491
		[NullableContext(1)]
		public unsafe static IEnumerable<T> ToEnumerable<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlyMemory<T> memory)
		{
			int num;
			for (int i = 0; i < memory.Length; i = num + 1)
			{
				yield return *memory.Span[i];
				num = i;
			}
			yield break;
		}

		// Token: 0x060043B7 RID: 17335 RVA: 0x001772A4 File Offset: 0x001764A4
		public static bool TryGetString(ReadOnlyMemory<char> memory, [Nullable(2)] [NotNullWhen(true)] out string text, out int start, out int length)
		{
			int num;
			int num2;
			string text2 = memory.GetObjectStartLength(out num, out num2) as string;
			if (text2 != null)
			{
				text = text2;
				start = num;
				length = num2;
				return true;
			}
			text = null;
			start = 0;
			length = 0;
			return false;
		}

		// Token: 0x060043B8 RID: 17336 RVA: 0x001772DA File Offset: 0x001764DA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Read<T>(ReadOnlySpan<byte> source) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > source.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
		}

		// Token: 0x060043B9 RID: 17337 RVA: 0x00177314 File Offset: 0x00176514
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryRead<T>(ReadOnlySpan<byte> source, out T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)source.Length))
			{
				value = default(T);
				return false;
			}
			value = Unsafe.ReadUnaligned<T>(MemoryMarshal.GetReference<byte>(source));
			return true;
		}

		// Token: 0x060043BA RID: 17338 RVA: 0x00177362 File Offset: 0x00176562
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void Write<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if (Unsafe.SizeOf<T>() > destination.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
		}

		// Token: 0x060043BB RID: 17339 RVA: 0x001773A0 File Offset: 0x001765A0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryWrite<T>(Span<byte> destination, ref T value) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)destination.Length))
			{
				return false;
			}
			Unsafe.WriteUnaligned<T>(MemoryMarshal.GetReference<byte>(destination), value);
			return true;
		}

		// Token: 0x060043BC RID: 17340 RVA: 0x001773DC File Offset: 0x001765DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref T AsRef<T>(Span<byte> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)span.Length))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return Unsafe.As<byte, T>(MemoryMarshal.GetReference<byte>(span));
		}

		// Token: 0x060043BD RID: 17341 RVA: 0x00177416 File Offset: 0x00176616
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ref readonly T AsRef<T>(ReadOnlySpan<byte> span) where T : struct
		{
			if (RuntimeHelpers.IsReferenceOrContainsReferences<T>())
			{
				ThrowHelper.ThrowInvalidTypeWithPointersNotSupported(typeof(T));
			}
			if ((long)Unsafe.SizeOf<T>() > (long)((ulong)span.Length))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return Unsafe.As<byte, T>(MemoryMarshal.GetReference<byte>(span));
		}

		// Token: 0x060043BE RID: 17342 RVA: 0x00177450 File Offset: 0x00176650
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> CreateFromPinnedArray<T>([Nullable(new byte[]
		{
			2,
			1
		})] T[] array, int start, int length)
		{
			if (array == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return default(Memory<T>);
			}
			if (!typeof(T).IsValueType && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length || length > array.Length - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Memory<T>(array, start | int.MinValue, length);
		}
	}
}
