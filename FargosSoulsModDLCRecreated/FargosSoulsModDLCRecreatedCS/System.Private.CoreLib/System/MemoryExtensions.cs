using System;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Internal.Runtime.CompilerServices;

namespace System
{
	// Token: 0x0200014E RID: 334
	public static class MemoryExtensions
	{
		// Token: 0x06001099 RID: 4249 RVA: 0x000DC18C File Offset: 0x000DB38C
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array, int start)
		{
			if (array == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException();
				}
				return default(Span<T>);
			}
			if (!typeof(T).IsValueType && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			if (start > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(MemoryMarshal.GetArrayDataReference<T>(array), start), array.Length - start);
		}

		// Token: 0x0600109A RID: 4250 RVA: 0x000DC1FC File Offset: 0x000DB3FC
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array, Index startIndex)
		{
			if (array == null)
			{
				if (!startIndex.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Span<T>);
			}
			if (!typeof(T).IsValueType && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			int offset = startIndex.GetOffset(array.Length);
			if (offset > array.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new Span<T>(Unsafe.Add<T>(MemoryMarshal.GetArrayDataReference<T>(array), offset), array.Length - offset);
		}

		// Token: 0x0600109B RID: 4251 RVA: 0x000DC284 File Offset: 0x000DB484
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array, Range range)
		{
			if (array == null)
			{
				Index start = range.Start;
				Index end = range.End;
				if (!start.Equals(Index.Start) || !end.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Span<T>);
			}
			if (!typeof(T).IsValueType && array.GetType() != typeof(T[]))
			{
				ThrowHelper.ThrowArrayTypeMismatchException();
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(array.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new Span<T>(Unsafe.Add<T>(MemoryMarshal.GetArrayDataReference<T>(array), item), item2);
		}

		// Token: 0x0600109C RID: 4252 RVA: 0x000BFA38 File Offset: 0x000BEC38
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan([Nullable(2)] this string text)
		{
			if (text == null)
			{
				return default(ReadOnlySpan<char>);
			}
			return new ReadOnlySpan<char>(text.GetRawStringData(), text.Length);
		}

		// Token: 0x0600109D RID: 4253 RVA: 0x000DC328 File Offset: 0x000DB528
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan([Nullable(2)] this string text, int start)
		{
			if (text == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if (start > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.Add<char>(text.GetRawStringData(), start), text.Length - start);
		}

		// Token: 0x0600109E RID: 4254 RVA: 0x000DC378 File Offset: 0x000DB578
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ReadOnlySpan<char> AsSpan([Nullable(2)] this string text, int start, int length)
		{
			if (text == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlySpan<char>);
			}
			if ((ulong)start + (ulong)length > (ulong)text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlySpan<char>(Unsafe.Add<char>(text.GetRawStringData(), start), length);
		}

		// Token: 0x0600109F RID: 4255 RVA: 0x000DC3C8 File Offset: 0x000DB5C8
		public static ReadOnlyMemory<char> AsMemory([Nullable(2)] this string text)
		{
			if (text == null)
			{
				return default(ReadOnlyMemory<char>);
			}
			return new ReadOnlyMemory<char>(text, 0, text.Length);
		}

		// Token: 0x060010A0 RID: 4256 RVA: 0x000DC3F0 File Offset: 0x000DB5F0
		public static ReadOnlyMemory<char> AsMemory([Nullable(2)] this string text, int start)
		{
			if (text == null)
			{
				if (start != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlyMemory<char>);
			}
			if (start > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<char>(text, start, text.Length - start);
		}

		// Token: 0x060010A1 RID: 4257 RVA: 0x000DC434 File Offset: 0x000DB634
		public static ReadOnlyMemory<char> AsMemory([Nullable(2)] this string text, Index startIndex)
		{
			if (text == null)
			{
				if (!startIndex.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);
				}
				return default(ReadOnlyMemory<char>);
			}
			int offset = startIndex.GetOffset(text.Length);
			if (offset > text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException();
			}
			return new ReadOnlyMemory<char>(text, offset, text.Length - offset);
		}

		// Token: 0x060010A2 RID: 4258 RVA: 0x000DC490 File Offset: 0x000DB690
		public static ReadOnlyMemory<char> AsMemory([Nullable(2)] this string text, int start, int length)
		{
			if (text == null)
			{
				if (start != 0 || length != 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
				}
				return default(ReadOnlyMemory<char>);
			}
			if ((ulong)start + (ulong)length > (ulong)text.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new ReadOnlyMemory<char>(text, start, length);
		}

		// Token: 0x060010A3 RID: 4259 RVA: 0x000DC4D4 File Offset: 0x000DB6D4
		public static ReadOnlyMemory<char> AsMemory([Nullable(2)] this string text, Range range)
		{
			if (text == null)
			{
				Index start = range.Start;
				Index end = range.End;
				if (!start.Equals(Index.Start) || !end.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.text);
				}
				return default(ReadOnlyMemory<char>);
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(text.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new ReadOnlyMemory<char>(text, item, item2);
		}

		// Token: 0x060010A4 RID: 4260 RVA: 0x000DC544 File Offset: 0x000DB744
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool Contains<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T value) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.Contains(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.Contains(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
				}
			}
			return SpanHelpers.Contains<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060010A5 RID: 4261 RVA: 0x000DC5BC File Offset: 0x000DB7BC
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static bool Contains<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.Contains(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.Contains(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
				}
			}
			return SpanHelpers.Contains<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060010A6 RID: 4262 RVA: 0x000DC634 File Offset: 0x000DB834
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T value) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
				}
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060010A7 RID: 4263 RVA: 0x000DC6AC File Offset: 0x000DB8AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(value)), value.Length);
				}
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x060010A8 RID: 4264 RVA: 0x000DC744 File Offset: 0x000DB944
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOf<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T value) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.LastIndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
				}
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060010A9 RID: 4265 RVA: 0x000DC7BC File Offset: 0x000DB9BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOf<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (Unsafe.SizeOf<T>() == 1 && RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x060010AA RID: 4266 RVA: 0x000DC824 File Offset: 0x000DBA24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other) where T : IEquatable<T>
		{
			int length = span.Length;
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				UIntPtr uintPtr = (UIntPtr)((IntPtr)Unsafe.SizeOf<T>());
				return length == other.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), (UIntPtr)((IntPtr)length * (IntPtr)uintPtr));
			}
			return length == other.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other), length);
		}

		// Token: 0x060010AB RID: 4267 RVA: 0x000DC894 File Offset: 0x000DBA94
		public static int SequenceCompareTo<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other) where T : IComparable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			return SpanHelpers.SequenceCompareTo<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(other), other.Length);
		}

		// Token: 0x060010AC RID: 4268 RVA: 0x000DC94C File Offset: 0x000DBB4C
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOf<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
				}
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000DC9C4 File Offset: 0x000DBBC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int IndexOf<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.IndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(value)), value.Length);
				}
			}
			return SpanHelpers.IndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000DCA5C File Offset: 0x000DBC5C
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOf<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T value) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.LastIndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value), span.Length);
				}
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), value, span.Length);
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000DCAD4 File Offset: 0x000DBCD4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOf<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			if (Unsafe.SizeOf<T>() == 1 && RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				return SpanHelpers.LastIndexOf(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), value.Length);
			}
			return SpanHelpers.LastIndexOf<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(value), value.Length);
		}

		// Token: 0x060010B0 RID: 4272 RVA: 0x000DCB3C File Offset: 0x000DBD3C
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value0), *Unsafe.As<T, char>(ref value1), span.Length);
				}
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x060010B1 RID: 4273 RVA: 0x000DCBC4 File Offset: 0x000DBDC4
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value0), *Unsafe.As<T, char>(ref value1), *Unsafe.As<T, char>(ref value2), span.Length);
				}
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x060010B2 RID: 4274 RVA: 0x000DCC60 File Offset: 0x000DBE60
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					ref byte ptr = ref Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values));
					if (values.Length == 2)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), ptr, *Unsafe.Add<byte>(ref ptr, 1), span.Length);
					}
					if (values.Length == 3)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), ptr, *Unsafe.Add<byte>(ref ptr, 1), *Unsafe.Add<byte>(ref ptr, 2), span.Length);
					}
					return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, ref ptr, values.Length);
				}
				else if (Unsafe.SizeOf<T>() == 2)
				{
					ref char ptr2 = ref Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(values));
					if (values.Length == 5)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, *Unsafe.Add<char>(ref ptr2, 1), *Unsafe.Add<char>(ref ptr2, 2), *Unsafe.Add<char>(ref ptr2, 3), *Unsafe.Add<char>(ref ptr2, 4), span.Length);
					}
					if (values.Length == 2)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, *Unsafe.Add<char>(ref ptr2, 1), span.Length);
					}
					if (values.Length == 4)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, *Unsafe.Add<char>(ref ptr2, 1), *Unsafe.Add<char>(ref ptr2, 2), *Unsafe.Add<char>(ref ptr2, 3), span.Length);
					}
					if (values.Length == 3)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, *Unsafe.Add<char>(ref ptr2, 1), *Unsafe.Add<char>(ref ptr2, 2), span.Length);
					}
					if (values.Length == 1)
					{
						return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, span.Length);
					}
				}
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x060010B3 RID: 4275 RVA: 0x000DCE48 File Offset: 0x000DC048
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value0), *Unsafe.As<T, char>(ref value1), span.Length);
				}
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x060010B4 RID: 4276 RVA: 0x000DCED0 File Offset: 0x000DC0D0
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
				}
				if (Unsafe.SizeOf<T>() == 2)
				{
					return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, char>(ref value0), *Unsafe.As<T, char>(ref value1), *Unsafe.As<T, char>(ref value2), span.Length);
				}
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x060010B5 RID: 4277 RVA: 0x000DCF6C File Offset: 0x000DC16C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int IndexOfAny<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				if (Unsafe.SizeOf<T>() == 1)
				{
					ref byte ptr = ref Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values));
					if (values.Length == 2)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), ptr, *Unsafe.Add<byte>(ref ptr, 1), span.Length);
					}
					if (values.Length == 3)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), ptr, *Unsafe.Add<byte>(ref ptr, 1), *Unsafe.Add<byte>(ref ptr, 2), span.Length);
					}
					return SpanHelpers.IndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, ref ptr, values.Length);
				}
				else if (Unsafe.SizeOf<T>() == 2)
				{
					ref char ptr2 = ref Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(values));
					if (values.Length == 5)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, *Unsafe.Add<char>(ref ptr2, 1), *Unsafe.Add<char>(ref ptr2, 2), *Unsafe.Add<char>(ref ptr2, 3), *Unsafe.Add<char>(ref ptr2, 4), span.Length);
					}
					if (values.Length == 2)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, *Unsafe.Add<char>(ref ptr2, 1), span.Length);
					}
					if (values.Length == 4)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, *Unsafe.Add<char>(ref ptr2, 1), *Unsafe.Add<char>(ref ptr2, 2), *Unsafe.Add<char>(ref ptr2, 3), span.Length);
					}
					if (values.Length == 3)
					{
						return SpanHelpers.IndexOfAny(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, *Unsafe.Add<char>(ref ptr2, 1), *Unsafe.Add<char>(ref ptr2, 2), span.Length);
					}
					if (values.Length == 1)
					{
						return SpanHelpers.IndexOf(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), ptr2, span.Length);
					}
				}
			}
			return SpanHelpers.IndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x060010B6 RID: 4278 RVA: 0x000DD154 File Offset: 0x000DC354
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (Unsafe.SizeOf<T>() == 1 && RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x060010B7 RID: 4279 RVA: 0x000DD1AC File Offset: 0x000DC3AC
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (Unsafe.SizeOf<T>() == 1 && RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x060010B8 RID: 4280 RVA: 0x000DD210 File Offset: 0x000DC410
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfAny<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (Unsafe.SizeOf<T>() == 1 && RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x060010B9 RID: 4281 RVA: 0x000DD278 File Offset: 0x000DC478
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T value0, T value1) where T : IEquatable<T>
		{
			if (Unsafe.SizeOf<T>() == 1 && RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, span.Length);
		}

		// Token: 0x060010BA RID: 4282 RVA: 0x000DD2D0 File Offset: 0x000DC4D0
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int LastIndexOfAny<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T value0, T value1, T value2) where T : IEquatable<T>
		{
			if (Unsafe.SizeOf<T>() == 1 && RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), *Unsafe.As<T, byte>(ref value0), *Unsafe.As<T, byte>(ref value1), *Unsafe.As<T, byte>(ref value2), span.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), value0, value1, value2, span.Length);
		}

		// Token: 0x060010BB RID: 4283 RVA: 0x000DD334 File Offset: 0x000DC534
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int LastIndexOfAny<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> values) where T : IEquatable<T>
		{
			if (Unsafe.SizeOf<T>() == 1 && RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				return SpanHelpers.LastIndexOfAny(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(values)), values.Length);
			}
			return SpanHelpers.LastIndexOfAny<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(values), values.Length);
		}

		// Token: 0x060010BC RID: 4284 RVA: 0x000DD39C File Offset: 0x000DC59C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool SequenceEqual<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other) where T : IEquatable<T>
		{
			int length = span.Length;
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				UIntPtr uintPtr = (UIntPtr)((IntPtr)Unsafe.SizeOf<T>());
				return length == other.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), (UIntPtr)((IntPtr)length * (IntPtr)uintPtr));
			}
			return length == other.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other), length);
		}

		// Token: 0x060010BD RID: 4285 RVA: 0x000DD40C File Offset: 0x000DC60C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int SequenceCompareTo<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other) where T : IComparable<T>
		{
			if (typeof(T) == typeof(byte))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			if (typeof(T) == typeof(char))
			{
				return SpanHelpers.SequenceCompareTo(Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(span)), span.Length, Unsafe.As<T, char>(MemoryMarshal.GetReference<T>(other)), other.Length);
			}
			return SpanHelpers.SequenceCompareTo<T>(MemoryMarshal.GetReference<T>(span), span.Length, MemoryMarshal.GetReference<T>(other), other.Length);
		}

		// Token: 0x060010BE RID: 4286 RVA: 0x000DD4C4 File Offset: 0x000DC6C4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = value.Length;
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				UIntPtr uintPtr = (UIntPtr)((IntPtr)Unsafe.SizeOf<T>());
				return length <= span.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (UIntPtr)((IntPtr)length * (IntPtr)uintPtr));
			}
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(value), length);
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x000DD534 File Offset: 0x000DC734
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool StartsWith<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = value.Length;
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				UIntPtr uintPtr = (UIntPtr)((IntPtr)Unsafe.SizeOf<T>());
				return length <= span.Length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(span)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (UIntPtr)((IntPtr)length * (IntPtr)uintPtr));
			}
			return length <= span.Length && SpanHelpers.SequenceEqual<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(value), length);
		}

		// Token: 0x060010C0 RID: 4288 RVA: 0x000DD5A4 File Offset: 0x000DC7A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWith<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = span.Length;
			int length2 = value.Length;
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				UIntPtr uintPtr = (UIntPtr)((IntPtr)Unsafe.SizeOf<T>());
				return length2 <= length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (UIntPtr)((IntPtr)length2 * (IntPtr)uintPtr));
			}
			return length2 <= length && SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2), MemoryMarshal.GetReference<T>(value), length2);
		}

		// Token: 0x060010C1 RID: 4289 RVA: 0x000DD620 File Offset: 0x000DC820
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EndsWith<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> value) where T : IEquatable<T>
		{
			int length = span.Length;
			int length2 = value.Length;
			if (RuntimeHelpers.IsBitwiseEquatable<T>())
			{
				UIntPtr uintPtr = (UIntPtr)((IntPtr)Unsafe.SizeOf<T>());
				return length2 <= length && SpanHelpers.SequenceEqual(Unsafe.As<T, byte>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2)), Unsafe.As<T, byte>(MemoryMarshal.GetReference<T>(value)), (UIntPtr)((IntPtr)length2 * (IntPtr)uintPtr));
			}
			return length2 <= length && SpanHelpers.SequenceEqual<T>(Unsafe.Add<T>(MemoryMarshal.GetReference<T>(span), length - length2), MemoryMarshal.GetReference<T>(value), length2);
		}

		// Token: 0x060010C2 RID: 4290 RVA: 0x000DD69C File Offset: 0x000DC89C
		[NullableContext(2)]
		public static void Reverse<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span)
		{
			if (span.Length <= 1)
			{
				return;
			}
			ref T ptr = ref MemoryMarshal.GetReference<T>(span);
			ref T ptr2 = ref Unsafe.Add<T>(Unsafe.Add<T>(ref ptr, span.Length), -1);
			do
			{
				T t = ptr;
				ptr = ptr2;
				ptr2 = t;
				ptr = Unsafe.Add<T>(ref ptr, 1);
				ptr2 = Unsafe.Add<T>(ref ptr2, -1);
			}
			while (Unsafe.IsAddressLessThan<T>(ref ptr, ref ptr2));
		}

		// Token: 0x060010C3 RID: 4291 RVA: 0x000DD702 File Offset: 0x000DC902
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array)
		{
			return new Span<T>(array);
		}

		// Token: 0x060010C4 RID: 4292 RVA: 0x000DD70A File Offset: 0x000DC90A
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array, int start, int length)
		{
			return new Span<T>(array, start, length);
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x000DD714 File Offset: 0x000DC914
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ArraySegment<T> segment)
		{
			return new Span<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x000DD730 File Offset: 0x000DC930
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ArraySegment<T> segment, int start)
		{
			if (start > segment.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Span<T>(segment.Array, segment.Offset + start, segment.Count - start);
		}

		// Token: 0x060010C7 RID: 4295 RVA: 0x000DD764 File Offset: 0x000DC964
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ArraySegment<T> segment, Index startIndex)
		{
			int offset = startIndex.GetOffset(segment.Count);
			return segment.AsSpan(offset);
		}

		// Token: 0x060010C8 RID: 4296 RVA: 0x000DD787 File Offset: 0x000DC987
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ArraySegment<T> segment, int start, int length)
		{
			if (start > segment.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if (length > segment.Count - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new Span<T>(segment.Array, segment.Offset + start, length);
		}

		// Token: 0x060010C9 RID: 4297 RVA: 0x000DD7C4 File Offset: 0x000DC9C4
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> AsSpan<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ArraySegment<T> segment, Range range)
		{
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(segment.Count);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new Span<T>(segment.Array, segment.Offset + item, item2);
		}

		// Token: 0x060010CA RID: 4298 RVA: 0x000DD802 File Offset: 0x000DCA02
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> AsMemory<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array)
		{
			return new Memory<T>(array);
		}

		// Token: 0x060010CB RID: 4299 RVA: 0x000DD80A File Offset: 0x000DCA0A
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> AsMemory<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array, int start)
		{
			return new Memory<T>(array, start);
		}

		// Token: 0x060010CC RID: 4300 RVA: 0x000DD814 File Offset: 0x000DCA14
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> AsMemory<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array, Index startIndex)
		{
			if (array == null)
			{
				if (!startIndex.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Memory<T>);
			}
			int offset = startIndex.GetOffset(array.Length);
			return new Memory<T>(array, offset);
		}

		// Token: 0x060010CD RID: 4301 RVA: 0x000DD854 File Offset: 0x000DCA54
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> AsMemory<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array, int start, int length)
		{
			return new Memory<T>(array, start, length);
		}

		// Token: 0x060010CE RID: 4302 RVA: 0x000DD860 File Offset: 0x000DCA60
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> AsMemory<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] array, Range range)
		{
			if (array == null)
			{
				Index start = range.Start;
				Index end = range.End;
				if (!start.Equals(Index.Start) || !end.Equals(Index.Start))
				{
					ThrowHelper.ThrowArgumentNullException(ExceptionArgument.array);
				}
				return default(Memory<T>);
			}
			ValueTuple<int, int> offsetAndLength = range.GetOffsetAndLength(array.Length);
			int item = offsetAndLength.Item1;
			int item2 = offsetAndLength.Item2;
			return new Memory<T>(array, item, item2);
		}

		// Token: 0x060010CF RID: 4303 RVA: 0x000DD8CC File Offset: 0x000DCACC
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> AsMemory<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ArraySegment<T> segment)
		{
			return new Memory<T>(segment.Array, segment.Offset, segment.Count);
		}

		// Token: 0x060010D0 RID: 4304 RVA: 0x000DD8E8 File Offset: 0x000DCAE8
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> AsMemory<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ArraySegment<T> segment, int start)
		{
			if (start > segment.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			return new Memory<T>(segment.Array, segment.Offset + start, segment.Count - start);
		}

		// Token: 0x060010D1 RID: 4305 RVA: 0x000DD919 File Offset: 0x000DCB19
		[NullableContext(2)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> AsMemory<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ArraySegment<T> segment, int start, int length)
		{
			if (start > segment.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.start);
			}
			if (length > segment.Count - start)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.length);
			}
			return new Memory<T>(segment.Array, segment.Offset + start, length);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x000DD958 File Offset: 0x000DCB58
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] source, [Nullable(new byte[]
		{
			0,
			1
		})] Span<T> destination)
		{
			new ReadOnlySpan<T>(source).CopyTo(destination);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x000DD974 File Offset: 0x000DCB74
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void CopyTo<T>([Nullable(new byte[]
		{
			2,
			1
		})] this T[] source, [Nullable(new byte[]
		{
			0,
			1
		})] Memory<T> destination)
		{
			source.CopyTo(destination.Span);
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x000DD983 File Offset: 0x000DCB83
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Overlaps<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other)
		{
			return span.Overlaps(other);
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x000DD991 File Offset: 0x000DCB91
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Overlaps<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other, out int elementOffset)
		{
			return span.Overlaps(other, out elementOffset);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x000DD9A0 File Offset: 0x000DCBA0
		[NullableContext(2)]
		public static bool Overlaps<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other)
		{
			if (span.IsEmpty || other.IsEmpty)
			{
				return false;
			}
			IntPtr value = Unsafe.ByteOffset<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other));
			if (Unsafe.SizeOf<IntPtr>() == 4)
			{
				return (int)value < span.Length * Unsafe.SizeOf<T>() || (int)value > -(other.Length * Unsafe.SizeOf<T>());
			}
			return (long)value < (long)span.Length * (long)Unsafe.SizeOf<T>() || (long)value > -((long)other.Length * (long)Unsafe.SizeOf<T>());
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x000DDA3C File Offset: 0x000DCC3C
		[NullableContext(2)]
		public static bool Overlaps<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> other, out int elementOffset)
		{
			if (span.IsEmpty || other.IsEmpty)
			{
				elementOffset = 0;
				return false;
			}
			IntPtr value = Unsafe.ByteOffset<T>(MemoryMarshal.GetReference<T>(span), MemoryMarshal.GetReference<T>(other));
			if (Unsafe.SizeOf<IntPtr>() == 4)
			{
				if ((int)value < span.Length * Unsafe.SizeOf<T>() || (int)value > -(other.Length * Unsafe.SizeOf<T>()))
				{
					if ((int)value % Unsafe.SizeOf<T>() != 0)
					{
						ThrowHelper.ThrowArgumentException_OverlapAlignmentMismatch();
					}
					elementOffset = (int)value / Unsafe.SizeOf<T>();
					return true;
				}
				elementOffset = 0;
				return false;
			}
			else
			{
				if ((long)value < (long)span.Length * (long)Unsafe.SizeOf<T>() || (long)value > -((long)other.Length * (long)Unsafe.SizeOf<T>()))
				{
					if ((long)value % (long)Unsafe.SizeOf<T>() != 0L)
					{
						ThrowHelper.ThrowArgumentException_OverlapAlignmentMismatch();
					}
					elementOffset = (int)((long)value / (long)Unsafe.SizeOf<T>());
					return true;
				}
				elementOffset = 0;
				return false;
			}
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x000DDB26 File Offset: 0x000DCD26
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, IComparable<T> comparable)
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x060010D9 RID: 4313 RVA: 0x000DDB2F File Offset: 0x000DCD2F
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<[Nullable(2)] T, [Nullable(0)] TComparable>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, TComparable comparable) where TComparable : IComparable<T>
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x060010DA RID: 4314 RVA: 0x000DDB3D File Offset: 0x000DCD3D
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<[Nullable(2)] T, [Nullable(0)] TComparer>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T value, TComparer comparer) where TComparer : IComparer<T>
		{
			return span.BinarySearch(value, comparer);
		}

		// Token: 0x060010DB RID: 4315 RVA: 0x000DDB4C File Offset: 0x000DCD4C
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, IComparable<T> comparable)
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x060010DC RID: 4316 RVA: 0x000DDB55 File Offset: 0x000DCD55
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<[Nullable(2)] T, [Nullable(0)] TComparable>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, TComparable comparable) where TComparable : IComparable<T>
		{
			return span.BinarySearch(comparable);
		}

		// Token: 0x060010DD RID: 4317 RVA: 0x000DDB60 File Offset: 0x000DCD60
		[NullableContext(1)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int BinarySearch<[Nullable(2)] T, [Nullable(0)] TComparer>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T value, TComparer comparer) where TComparer : IComparer<T>
		{
			if (comparer == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparer);
			}
			SpanHelpers.ComparerComparable<T, TComparer> comparable = new SpanHelpers.ComparerComparable<T, TComparer>(value, comparer);
			return span.BinarySearch(comparable);
		}

		// Token: 0x060010DE RID: 4318 RVA: 0x000DDB8C File Offset: 0x000DCD8C
		[NullableContext(2)]
		public static void Sort<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span)
		{
			span.Sort(null);
		}

		// Token: 0x060010DF RID: 4319 RVA: 0x000DDB95 File Offset: 0x000DCD95
		public static void Sort<[Nullable(2)] T, TComparer>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(1)] TComparer comparer) where TComparer : IComparer<T>
		{
			if (span.Length > 1)
			{
				ArraySortHelper<T>.Default.Sort(span, comparer);
			}
		}

		// Token: 0x060010E0 RID: 4320 RVA: 0x000DDBB2 File Offset: 0x000DCDB2
		[NullableContext(1)]
		public static void Sort<[Nullable(2)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, Comparison<T> comparison)
		{
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);
			}
			if (span.Length > 1)
			{
				ArraySortHelper<T>.Sort(span, comparison);
			}
		}

		// Token: 0x060010E1 RID: 4321 RVA: 0x000DDBCF File Offset: 0x000DCDCF
		[NullableContext(2)]
		public static void Sort<TKey, TValue>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<TKey> keys, [Nullable(new byte[]
		{
			0,
			1
		})] Span<TValue> items)
		{
			keys.Sort(items, null);
		}

		// Token: 0x060010E2 RID: 4322 RVA: 0x000DDBD9 File Offset: 0x000DCDD9
		[NullableContext(2)]
		public static void Sort<TKey, TValue, [Nullable(0)] TComparer>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<TKey> keys, [Nullable(new byte[]
		{
			0,
			1
		})] Span<TValue> items, [Nullable(1)] TComparer comparer) where TComparer : IComparer<TKey>
		{
			if (keys.Length != items.Length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_SpansMustHaveSameLength);
			}
			if (keys.Length > 1)
			{
				ArraySortHelper<TKey, TValue>.Default.Sort(keys, items, comparer);
			}
		}

		// Token: 0x060010E3 RID: 4323 RVA: 0x000DDC0E File Offset: 0x000DCE0E
		[NullableContext(2)]
		public static void Sort<TKey, TValue>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<TKey> keys, [Nullable(new byte[]
		{
			0,
			1
		})] Span<TValue> items, [Nullable(1)] Comparison<TKey> comparison)
		{
			if (comparison == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.comparison);
			}
			if (keys.Length != items.Length)
			{
				ThrowHelper.ThrowArgumentException(ExceptionResource.Argument_SpansMustHaveSameLength);
			}
			if (keys.Length > 1)
			{
				ArraySortHelper<TKey, TValue>.Default.Sort(keys, items, new ComparisonComparer<TKey>(comparison));
			}
		}

		// Token: 0x060010E4 RID: 4324 RVA: 0x000DDC50 File Offset: 0x000DCE50
		public unsafe static bool IsWhiteSpace(this ReadOnlySpan<char> span)
		{
			for (int i = 0; i < span.Length; i++)
			{
				if (!char.IsWhiteSpace((char)(*span[i])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060010E5 RID: 4325 RVA: 0x000DDC82 File Offset: 0x000DCE82
		public static bool Contains(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			return span.IndexOf(value, comparisonType) >= 0;
		}

		// Token: 0x060010E6 RID: 4326 RVA: 0x000DDC94 File Offset: 0x000DCE94
		public static bool Equals(this ReadOnlySpan<char> span, ReadOnlySpan<char> other, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(span, other, string.GetCaseCompareOfComparisonCulture(comparisonType)) == 0;
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.Compare(span, other, string.GetCaseCompareOfComparisonCulture(comparisonType)) == 0;
			case StringComparison.Ordinal:
				return span.EqualsOrdinal(other);
			default:
				return span.EqualsOrdinalIgnoreCase(other);
			}
		}

		// Token: 0x060010E7 RID: 4327 RVA: 0x000DDD03 File Offset: 0x000DCF03
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool EqualsOrdinal(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length == value.Length && (value.Length == 0 || span.SequenceEqual(value));
		}

		// Token: 0x060010E8 RID: 4328 RVA: 0x000DDD29 File Offset: 0x000DCF29
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool EqualsOrdinalIgnoreCase(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return span.Length == value.Length && (value.Length == 0 || Ordinal.EqualsIgnoreCase(MemoryMarshal.GetReference<char>(span), MemoryMarshal.GetReference<char>(value), span.Length));
		}

		// Token: 0x060010E9 RID: 4329 RVA: 0x000DDD60 File Offset: 0x000DCF60
		public static int CompareTo(this ReadOnlySpan<char> span, ReadOnlySpan<char> other, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.Compare(span, other, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.Compare(span, other, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
				if (span.Length == 0 || other.Length == 0)
				{
					return span.Length - other.Length;
				}
				return string.CompareOrdinal(span, other);
			default:
				return Ordinal.CompareStringIgnoreCase(MemoryMarshal.GetReference<char>(span), span.Length, MemoryMarshal.GetReference<char>(other), other.Length);
			}
		}

		// Token: 0x060010EA RID: 4330 RVA: 0x000DDE04 File Offset: 0x000DD004
		public static int IndexOf(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			if (comparisonType == StringComparison.Ordinal)
			{
				return SpanHelpers.IndexOf(MemoryMarshal.GetReference<char>(span), span.Length, MemoryMarshal.GetReference<char>(value), value.Length);
			}
			if (comparisonType <= StringComparison.CurrentCultureIgnoreCase)
			{
				return CultureInfo.CurrentCulture.CompareInfo.IndexOf(span, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			}
			if (comparisonType - StringComparison.InvariantCulture > 1)
			{
				return Ordinal.IndexOfOrdinalIgnoreCase(span, value);
			}
			return CompareInfo.Invariant.IndexOf(span, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
		}

		// Token: 0x060010EB RID: 4331 RVA: 0x000DDE7C File Offset: 0x000DD07C
		public static int LastIndexOf(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			if (comparisonType == StringComparison.Ordinal)
			{
				return SpanHelpers.LastIndexOf<char>(MemoryMarshal.GetReference<char>(span), span.Length, MemoryMarshal.GetReference<char>(value), value.Length);
			}
			if (comparisonType <= StringComparison.CurrentCultureIgnoreCase)
			{
				return CultureInfo.CurrentCulture.CompareInfo.LastIndexOf(span, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			}
			if (comparisonType - StringComparison.InvariantCulture > 1)
			{
				return Ordinal.LastIndexOfOrdinalIgnoreCase(span, value);
			}
			return CompareInfo.Invariant.LastIndexOf(span, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
		}

		// Token: 0x060010EC RID: 4332 RVA: 0x000DDEF4 File Offset: 0x000DD0F4
		public static int ToLower(this ReadOnlySpan<char> source, Span<char> destination, [Nullable(2)] CultureInfo culture)
		{
			if (source.Overlaps(destination))
			{
				throw new InvalidOperationException(SR.InvalidOperation_SpanOverlappedOperation);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				TextInfo.ToLowerAsciiInvariant(source, destination);
			}
			else
			{
				culture.TextInfo.ChangeCaseToLower(source, destination);
			}
			return source.Length;
		}

		// Token: 0x060010ED RID: 4333 RVA: 0x000DDF5C File Offset: 0x000DD15C
		public static int ToLowerInvariant(this ReadOnlySpan<char> source, Span<char> destination)
		{
			if (source.Overlaps(destination))
			{
				throw new InvalidOperationException(SR.InvalidOperation_SpanOverlappedOperation);
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				TextInfo.ToLowerAsciiInvariant(source, destination);
			}
			else
			{
				TextInfo.Invariant.ChangeCaseToLower(source, destination);
			}
			return source.Length;
		}

		// Token: 0x060010EE RID: 4334 RVA: 0x000DDFB8 File Offset: 0x000DD1B8
		public static int ToUpper(this ReadOnlySpan<char> source, Span<char> destination, [Nullable(2)] CultureInfo culture)
		{
			if (source.Overlaps(destination))
			{
				throw new InvalidOperationException(SR.InvalidOperation_SpanOverlappedOperation);
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentCulture;
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				TextInfo.ToUpperAsciiInvariant(source, destination);
			}
			else
			{
				culture.TextInfo.ChangeCaseToUpper(source, destination);
			}
			return source.Length;
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x000DE020 File Offset: 0x000DD220
		public static int ToUpperInvariant(this ReadOnlySpan<char> source, Span<char> destination)
		{
			if (source.Overlaps(destination))
			{
				throw new InvalidOperationException(SR.InvalidOperation_SpanOverlappedOperation);
			}
			if (destination.Length < source.Length)
			{
				return -1;
			}
			if (GlobalizationMode.Invariant)
			{
				TextInfo.ToUpperAsciiInvariant(source, destination);
			}
			else
			{
				TextInfo.Invariant.ChangeCaseToUpper(source, destination);
			}
			return source.Length;
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x000DE07C File Offset: 0x000DD27C
		public static bool EndsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsSuffix(span, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IsSuffix(span, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
				return span.EndsWith(value);
			default:
				return span.EndsWithOrdinalIgnoreCase(value);
			}
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x000DE0E5 File Offset: 0x000DD2E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool EndsWithOrdinalIgnoreCase(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return value.Length <= span.Length && Ordinal.EqualsIgnoreCase(Unsafe.Add<char>(MemoryMarshal.GetReference<char>(span), span.Length - value.Length), MemoryMarshal.GetReference<char>(value), value.Length);
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x000DE128 File Offset: 0x000DD328
		public static bool StartsWith(this ReadOnlySpan<char> span, ReadOnlySpan<char> value, StringComparison comparisonType)
		{
			string.CheckStringComparison(comparisonType);
			switch (comparisonType)
			{
			case StringComparison.CurrentCulture:
			case StringComparison.CurrentCultureIgnoreCase:
				return CultureInfo.CurrentCulture.CompareInfo.IsPrefix(span, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.InvariantCulture:
			case StringComparison.InvariantCultureIgnoreCase:
				return CompareInfo.Invariant.IsPrefix(span, value, string.GetCaseCompareOfComparisonCulture(comparisonType));
			case StringComparison.Ordinal:
				return span.StartsWith(value);
			default:
				return span.StartsWithOrdinalIgnoreCase(value);
			}
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x000DE191 File Offset: 0x000DD391
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool StartsWithOrdinalIgnoreCase(this ReadOnlySpan<char> span, ReadOnlySpan<char> value)
		{
			return value.Length <= span.Length && Ordinal.EqualsIgnoreCase(MemoryMarshal.GetReference<char>(span), MemoryMarshal.GetReference<char>(value), value.Length);
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x000DE1BD File Offset: 0x000DD3BD
		public static SpanRuneEnumerator EnumerateRunes(this ReadOnlySpan<char> span)
		{
			return new SpanRuneEnumerator(span);
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x000DE1C5 File Offset: 0x000DD3C5
		public static SpanRuneEnumerator EnumerateRunes(this Span<char> span)
		{
			return new SpanRuneEnumerator(span);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x000DE1D4 File Offset: 0x000DD3D4
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> Trim<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Memory<T> memory, T trimElement) where T : IEquatable<T>
		{
			ReadOnlySpan<T> span = memory.Span;
			int start = MemoryExtensions.ClampStart<T>(span, trimElement);
			int length = MemoryExtensions.ClampEnd<T>(span, start, trimElement);
			return memory.Slice(start, length);
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x000DE208 File Offset: 0x000DD408
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> TrimStart<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Memory<T> memory, T trimElement) where T : IEquatable<T>
		{
			return memory.Slice(MemoryExtensions.ClampStart<T>(memory.Span, trimElement));
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x000DE223 File Offset: 0x000DD423
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Memory<T> TrimEnd<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Memory<T> memory, T trimElement) where T : IEquatable<T>
		{
			return memory.Slice(0, MemoryExtensions.ClampEnd<T>(memory.Span, 0, trimElement));
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x000DE240 File Offset: 0x000DD440
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ReadOnlyMemory<T> Trim<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlyMemory<T> memory, T trimElement) where T : IEquatable<T>
		{
			ReadOnlySpan<T> span = memory.Span;
			int start = MemoryExtensions.ClampStart<T>(span, trimElement);
			int length = MemoryExtensions.ClampEnd<T>(span, start, trimElement);
			return memory.Slice(start, length);
		}

		// Token: 0x060010FA RID: 4346 RVA: 0x000DE26F File Offset: 0x000DD46F
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ReadOnlyMemory<T> TrimStart<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlyMemory<T> memory, T trimElement) where T : IEquatable<T>
		{
			return memory.Slice(MemoryExtensions.ClampStart<T>(memory.Span, trimElement));
		}

		// Token: 0x060010FB RID: 4347 RVA: 0x000DE285 File Offset: 0x000DD485
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ReadOnlyMemory<T> TrimEnd<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlyMemory<T> memory, T trimElement) where T : IEquatable<T>
		{
			return memory.Slice(0, MemoryExtensions.ClampEnd<T>(memory.Span, 0, trimElement));
		}

		// Token: 0x060010FC RID: 4348 RVA: 0x000DE2A0 File Offset: 0x000DD4A0
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> Trim<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T trimElement) where T : IEquatable<T>
		{
			int start = MemoryExtensions.ClampStart<T>(span, trimElement);
			int length = MemoryExtensions.ClampEnd<T>(span, start, trimElement);
			return span.Slice(start, length);
		}

		// Token: 0x060010FD RID: 4349 RVA: 0x000DE2D1 File Offset: 0x000DD4D1
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> TrimStart<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T trimElement) where T : IEquatable<T>
		{
			return span.Slice(MemoryExtensions.ClampStart<T>(span, trimElement));
		}

		// Token: 0x060010FE RID: 4350 RVA: 0x000DE2E6 File Offset: 0x000DD4E6
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static Span<T> TrimEnd<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, T trimElement) where T : IEquatable<T>
		{
			return span.Slice(0, MemoryExtensions.ClampEnd<T>(span, 0, trimElement));
		}

		// Token: 0x060010FF RID: 4351 RVA: 0x000DE300 File Offset: 0x000DD500
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ReadOnlySpan<T> Trim<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T trimElement) where T : IEquatable<T>
		{
			int start = MemoryExtensions.ClampStart<T>(span, trimElement);
			int length = MemoryExtensions.ClampEnd<T>(span, start, trimElement);
			return span.Slice(start, length);
		}

		// Token: 0x06001100 RID: 4352 RVA: 0x000DE327 File Offset: 0x000DD527
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ReadOnlySpan<T> TrimStart<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T trimElement) where T : IEquatable<T>
		{
			return span.Slice(MemoryExtensions.ClampStart<T>(span, trimElement));
		}

		// Token: 0x06001101 RID: 4353 RVA: 0x000DE337 File Offset: 0x000DD537
		[NullableContext(1)]
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public static ReadOnlySpan<T> TrimEnd<[Nullable(0)] T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, T trimElement) where T : IEquatable<T>
		{
			return span.Slice(0, MemoryExtensions.ClampEnd<T>(span, 0, trimElement));
		}

		// Token: 0x06001102 RID: 4354 RVA: 0x000DE34C File Offset: 0x000DD54C
		private unsafe static int ClampStart<T>(ReadOnlySpan<T> span, T trimElement) where T : IEquatable<T>
		{
			int i = 0;
			if (trimElement != null)
			{
				while (i < span.Length)
				{
					if (!trimElement.Equals(*span[i]))
					{
						break;
					}
					i++;
				}
			}
			else
			{
				while (i < span.Length && *span[i] == null)
				{
					i++;
				}
			}
			return i;
		}

		// Token: 0x06001103 RID: 4355 RVA: 0x000DE3B4 File Offset: 0x000DD5B4
		private unsafe static int ClampEnd<T>(ReadOnlySpan<T> span, int start, T trimElement) where T : IEquatable<T>
		{
			int i = span.Length - 1;
			if (trimElement != null)
			{
				while (i >= start)
				{
					if (!trimElement.Equals(*span[i]))
					{
						break;
					}
					i--;
				}
			}
			else
			{
				while (i >= start && *span[i] == null)
				{
					i--;
				}
			}
			return i - start + 1;
		}

		// Token: 0x06001104 RID: 4356 RVA: 0x000DE41C File Offset: 0x000DD61C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static Memory<T> Trim<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Memory<T> memory, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				ReadOnlySpan<T> span = memory.Span;
				int start = MemoryExtensions.ClampStart<T>(span, trimElements);
				int length = MemoryExtensions.ClampEnd<T>(span, start, trimElements);
				return memory.Slice(start, length);
			}
			if (trimElements.Length == 1)
			{
				return memory.Trim(*trimElements[0]);
			}
			return memory;
		}

		// Token: 0x06001105 RID: 4357 RVA: 0x000DE47C File Offset: 0x000DD67C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static Memory<T> TrimStart<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Memory<T> memory, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				return memory.Slice(MemoryExtensions.ClampStart<T>(memory.Span, trimElements));
			}
			if (trimElements.Length == 1)
			{
				return memory.TrimStart(*trimElements[0]);
			}
			return memory;
		}

		// Token: 0x06001106 RID: 4358 RVA: 0x000DE4CC File Offset: 0x000DD6CC
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static Memory<T> TrimEnd<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Memory<T> memory, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				return memory.Slice(0, MemoryExtensions.ClampEnd<T>(memory.Span, 0, trimElements));
			}
			if (trimElements.Length == 1)
			{
				return memory.TrimEnd(*trimElements[0]);
			}
			return memory;
		}

		// Token: 0x06001107 RID: 4359 RVA: 0x000DE520 File Offset: 0x000DD720
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static ReadOnlyMemory<T> Trim<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlyMemory<T> memory, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				ReadOnlySpan<T> span = memory.Span;
				int start = MemoryExtensions.ClampStart<T>(span, trimElements);
				int length = MemoryExtensions.ClampEnd<T>(span, start, trimElements);
				return memory.Slice(start, length);
			}
			if (trimElements.Length == 1)
			{
				return memory.Trim(*trimElements[0]);
			}
			return memory;
		}

		// Token: 0x06001108 RID: 4360 RVA: 0x000DE579 File Offset: 0x000DD779
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static ReadOnlyMemory<T> TrimStart<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlyMemory<T> memory, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				return memory.Slice(MemoryExtensions.ClampStart<T>(memory.Span, trimElements));
			}
			if (trimElements.Length == 1)
			{
				return memory.TrimStart(*trimElements[0]);
			}
			return memory;
		}

		// Token: 0x06001109 RID: 4361 RVA: 0x000DE5BC File Offset: 0x000DD7BC
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static ReadOnlyMemory<T> TrimEnd<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlyMemory<T> memory, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				return memory.Slice(0, MemoryExtensions.ClampEnd<T>(memory.Span, 0, trimElements));
			}
			if (trimElements.Length == 1)
			{
				return memory.TrimEnd(*trimElements[0]);
			}
			return memory;
		}

		// Token: 0x0600110A RID: 4362 RVA: 0x000DE60C File Offset: 0x000DD80C
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static Span<T> Trim<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				int start = MemoryExtensions.ClampStart<T>(span, trimElements);
				int length = MemoryExtensions.ClampEnd<T>(span, start, trimElements);
				return span.Slice(start, length);
			}
			if (trimElements.Length == 1)
			{
				return span.Trim(*trimElements[0]);
			}
			return span;
		}

		// Token: 0x0600110B RID: 4363 RVA: 0x000DE667 File Offset: 0x000DD867
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static Span<T> TrimStart<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				return span.Slice(MemoryExtensions.ClampStart<T>(span, trimElements));
			}
			if (trimElements.Length == 1)
			{
				return span.TrimStart(*trimElements[0]);
			}
			return span;
		}

		// Token: 0x0600110C RID: 4364 RVA: 0x000DE6A8 File Offset: 0x000DD8A8
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static Span<T> TrimEnd<T>([Nullable(new byte[]
		{
			0,
			1
		})] this Span<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				return span.Slice(0, MemoryExtensions.ClampEnd<T>(span, 0, trimElements));
			}
			if (trimElements.Length == 1)
			{
				return span.TrimEnd(*trimElements[0]);
			}
			return span;
		}

		// Token: 0x0600110D RID: 4365 RVA: 0x000DE6F4 File Offset: 0x000DD8F4
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static ReadOnlySpan<T> Trim<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				int start = MemoryExtensions.ClampStart<T>(span, trimElements);
				int length = MemoryExtensions.ClampEnd<T>(span, start, trimElements);
				return span.Slice(start, length);
			}
			if (trimElements.Length == 1)
			{
				return span.Trim(*trimElements[0]);
			}
			return span;
		}

		// Token: 0x0600110E RID: 4366 RVA: 0x000DE745 File Offset: 0x000DD945
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static ReadOnlySpan<T> TrimStart<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				return span.Slice(MemoryExtensions.ClampStart<T>(span, trimElements));
			}
			if (trimElements.Length == 1)
			{
				return span.TrimStart(*trimElements[0]);
			}
			return span;
		}

		// Token: 0x0600110F RID: 4367 RVA: 0x000DE77F File Offset: 0x000DD97F
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public unsafe static ReadOnlySpan<T> TrimEnd<T>([Nullable(new byte[]
		{
			0,
			1
		})] this ReadOnlySpan<T> span, [Nullable(new byte[]
		{
			0,
			1
		})] ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			if (trimElements.Length > 1)
			{
				return span.Slice(0, MemoryExtensions.ClampEnd<T>(span, 0, trimElements));
			}
			if (trimElements.Length == 1)
			{
				return span.TrimEnd(*trimElements[0]);
			}
			return span;
		}

		// Token: 0x06001110 RID: 4368 RVA: 0x000DE7BC File Offset: 0x000DD9BC
		private unsafe static int ClampStart<T>(ReadOnlySpan<T> span, ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			int num = 0;
			while (num < span.Length && trimElements.Contains(*span[num]))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001111 RID: 4369 RVA: 0x000DE7F4 File Offset: 0x000DD9F4
		private unsafe static int ClampEnd<T>(ReadOnlySpan<T> span, int start, ReadOnlySpan<T> trimElements) where T : IEquatable<T>
		{
			int num = span.Length - 1;
			while (num >= start && trimElements.Contains(*span[num]))
			{
				num--;
			}
			return num - start + 1;
		}

		// Token: 0x06001112 RID: 4370 RVA: 0x000DE830 File Offset: 0x000DDA30
		public static Memory<char> Trim(this Memory<char> memory)
		{
			ReadOnlySpan<char> span = memory.Span;
			int start = MemoryExtensions.ClampStart(span);
			int length = MemoryExtensions.ClampEnd(span, start);
			return memory.Slice(start, length);
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x000DE862 File Offset: 0x000DDA62
		public static Memory<char> TrimStart(this Memory<char> memory)
		{
			return memory.Slice(MemoryExtensions.ClampStart(memory.Span));
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x000DE87C File Offset: 0x000DDA7C
		public static Memory<char> TrimEnd(this Memory<char> memory)
		{
			return memory.Slice(0, MemoryExtensions.ClampEnd(memory.Span, 0));
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x000DE898 File Offset: 0x000DDA98
		public static ReadOnlyMemory<char> Trim(this ReadOnlyMemory<char> memory)
		{
			ReadOnlySpan<char> span = memory.Span;
			int start = MemoryExtensions.ClampStart(span);
			int length = MemoryExtensions.ClampEnd(span, start);
			return memory.Slice(start, length);
		}

		// Token: 0x06001116 RID: 4374 RVA: 0x000DE8C5 File Offset: 0x000DDAC5
		public static ReadOnlyMemory<char> TrimStart(this ReadOnlyMemory<char> memory)
		{
			return memory.Slice(MemoryExtensions.ClampStart(memory.Span));
		}

		// Token: 0x06001117 RID: 4375 RVA: 0x000DE8DA File Offset: 0x000DDADA
		public static ReadOnlyMemory<char> TrimEnd(this ReadOnlyMemory<char> memory)
		{
			return memory.Slice(0, MemoryExtensions.ClampEnd(memory.Span, 0));
		}

		// Token: 0x06001118 RID: 4376 RVA: 0x000DE8F4 File Offset: 0x000DDAF4
		public unsafe static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span)
		{
			int num = 0;
			while (num < span.Length && char.IsWhiteSpace((char)(*span[num])))
			{
				num++;
			}
			int num2 = span.Length - 1;
			while (num2 > num && char.IsWhiteSpace((char)(*span[num2])))
			{
				num2--;
			}
			return span.Slice(num, num2 - num + 1);
		}

		// Token: 0x06001119 RID: 4377 RVA: 0x000DE954 File Offset: 0x000DDB54
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span)
		{
			int num = 0;
			while (num < span.Length && char.IsWhiteSpace((char)(*span[num])))
			{
				num++;
			}
			return span.Slice(num);
		}

		// Token: 0x0600111A RID: 4378 RVA: 0x000DE98C File Offset: 0x000DDB8C
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span)
		{
			int num = span.Length - 1;
			while (num >= 0 && char.IsWhiteSpace((char)(*span[num])))
			{
				num--;
			}
			return span.Slice(0, num + 1);
		}

		// Token: 0x0600111B RID: 4379 RVA: 0x000DE9C8 File Offset: 0x000DDBC8
		public unsafe static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span, char trimChar)
		{
			int num = 0;
			while (num < span.Length && *span[num] == (ushort)trimChar)
			{
				num++;
			}
			int num2 = span.Length - 1;
			while (num2 > num && *span[num2] == (ushort)trimChar)
			{
				num2--;
			}
			return span.Slice(num, num2 - num + 1);
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x000DEA20 File Offset: 0x000DDC20
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span, char trimChar)
		{
			int num = 0;
			while (num < span.Length && *span[num] == (ushort)trimChar)
			{
				num++;
			}
			return span.Slice(num);
		}

		// Token: 0x0600111D RID: 4381 RVA: 0x000DEA54 File Offset: 0x000DDC54
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span, char trimChar)
		{
			int num = span.Length - 1;
			while (num >= 0 && *span[num] == (ushort)trimChar)
			{
				num--;
			}
			return span.Slice(0, num + 1);
		}

		// Token: 0x0600111E RID: 4382 RVA: 0x000DEA8C File Offset: 0x000DDC8C
		public static ReadOnlySpan<char> Trim(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			return span.TrimStart(trimChars).TrimEnd(trimChars);
		}

		// Token: 0x0600111F RID: 4383 RVA: 0x000DEA9C File Offset: 0x000DDC9C
		public unsafe static ReadOnlySpan<char> TrimStart(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			if (trimChars.IsEmpty)
			{
				return span.TrimStart();
			}
			int i = 0;
			IL_40:
			while (i < span.Length)
			{
				for (int j = 0; j < trimChars.Length; j++)
				{
					if (*span[i] == *trimChars[j])
					{
						i++;
						goto IL_40;
					}
				}
				break;
			}
			return span.Slice(i);
		}

		// Token: 0x06001120 RID: 4384 RVA: 0x000DEAFC File Offset: 0x000DDCFC
		public unsafe static ReadOnlySpan<char> TrimEnd(this ReadOnlySpan<char> span, ReadOnlySpan<char> trimChars)
		{
			if (trimChars.IsEmpty)
			{
				return span.TrimEnd();
			}
			int i = span.Length - 1;
			IL_48:
			while (i >= 0)
			{
				for (int j = 0; j < trimChars.Length; j++)
				{
					if (*span[i] == *trimChars[j])
					{
						i--;
						goto IL_48;
					}
				}
				break;
			}
			return span.Slice(0, i + 1);
		}

		// Token: 0x06001121 RID: 4385 RVA: 0x000DEB60 File Offset: 0x000DDD60
		public static Span<char> Trim(this Span<char> span)
		{
			int start = MemoryExtensions.ClampStart(span);
			int length = MemoryExtensions.ClampEnd(span, start);
			return span.Slice(start, length);
		}

		// Token: 0x06001122 RID: 4386 RVA: 0x000DEB8F File Offset: 0x000DDD8F
		public static Span<char> TrimStart(this Span<char> span)
		{
			return span.Slice(MemoryExtensions.ClampStart(span));
		}

		// Token: 0x06001123 RID: 4387 RVA: 0x000DEBA3 File Offset: 0x000DDDA3
		public static Span<char> TrimEnd(this Span<char> span)
		{
			return span.Slice(0, MemoryExtensions.ClampEnd(span, 0));
		}

		// Token: 0x06001124 RID: 4388 RVA: 0x000DEBBC File Offset: 0x000DDDBC
		private unsafe static int ClampStart(ReadOnlySpan<char> span)
		{
			int num = 0;
			while (num < span.Length && char.IsWhiteSpace((char)(*span[num])))
			{
				num++;
			}
			return num;
		}

		// Token: 0x06001125 RID: 4389 RVA: 0x000DEBEC File Offset: 0x000DDDEC
		private unsafe static int ClampEnd(ReadOnlySpan<char> span, int start)
		{
			int num = span.Length - 1;
			while (num >= start && char.IsWhiteSpace((char)(*span[num])))
			{
				num--;
			}
			return num - start + 1;
		}
	}
}
