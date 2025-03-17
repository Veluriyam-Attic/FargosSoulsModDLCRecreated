using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000263 RID: 611
	[NullableContext(1)]
	[Nullable(0)]
	public static class Interlocked
	{
		// Token: 0x0600253B RID: 9531 RVA: 0x00140BBE File Offset: 0x0013FDBE
		public static int Increment(ref int location)
		{
			return Interlocked.Add(ref location, 1);
		}

		// Token: 0x0600253C RID: 9532 RVA: 0x00140BC7 File Offset: 0x0013FDC7
		public static long Increment(ref long location)
		{
			return Interlocked.Add(ref location, 1L);
		}

		// Token: 0x0600253D RID: 9533 RVA: 0x00140BD1 File Offset: 0x0013FDD1
		public static int Decrement(ref int location)
		{
			return Interlocked.Add(ref location, -1);
		}

		// Token: 0x0600253E RID: 9534 RVA: 0x00140BDA File Offset: 0x0013FDDA
		public static long Decrement(ref long location)
		{
			return Interlocked.Add(ref location, -1L);
		}

		// Token: 0x0600253F RID: 9535
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int Exchange(ref int location1, int value);

		// Token: 0x06002540 RID: 9536
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long Exchange(ref long location1, long value);

		// Token: 0x06002541 RID: 9537
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Exchange(ref float location1, float value);

		// Token: 0x06002542 RID: 9538
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exchange(ref double location1, double value);

		// Token: 0x06002543 RID: 9539
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: NotNullIfNotNull("location1")]
		public static extern object Exchange([NotNullIfNotNull("value")] ref object location1, object value);

		// Token: 0x06002544 RID: 9540
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr Exchange(ref IntPtr location1, IntPtr value);

		// Token: 0x06002545 RID: 9541 RVA: 0x00140BE4 File Offset: 0x0013FDE4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		[return: NotNullIfNotNull("location1")]
		public static T Exchange<[Nullable(2)] T>([NotNullIfNotNull("value")] ref T location1, T value) where T : class
		{
			return Unsafe.As<T>(Interlocked.Exchange(Unsafe.As<T, object>(ref location1), value));
		}

		// Token: 0x06002546 RID: 9542
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int CompareExchange(ref int location1, int value, int comparand);

		// Token: 0x06002547 RID: 9543
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern long CompareExchange(ref long location1, long value, long comparand);

		// Token: 0x06002548 RID: 9544
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float CompareExchange(ref float location1, float value, float comparand);

		// Token: 0x06002549 RID: 9545
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double CompareExchange(ref double location1, double value, double comparand);

		// Token: 0x0600254A RID: 9546
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		[return: NotNullIfNotNull("location1")]
		public static extern object CompareExchange(ref object location1, object value, object comparand);

		// Token: 0x0600254B RID: 9547
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern IntPtr CompareExchange(ref IntPtr location1, IntPtr value, IntPtr comparand);

		// Token: 0x0600254C RID: 9548 RVA: 0x00140BFC File Offset: 0x0013FDFC
		[Intrinsic]
		[return: NotNullIfNotNull("location1")]
		public static T CompareExchange<[Nullable(2)] T>(ref T location1, T value, T comparand) where T : class
		{
			return Unsafe.As<T>(Interlocked.CompareExchange(Unsafe.As<T, object>(ref location1), value, comparand));
		}

		// Token: 0x0600254D RID: 9549 RVA: 0x00140C1A File Offset: 0x0013FE1A
		public static int Add(ref int location1, int value)
		{
			return Interlocked.ExchangeAdd(ref location1, value) + value;
		}

		// Token: 0x0600254E RID: 9550 RVA: 0x00140C25 File Offset: 0x0013FE25
		public static long Add(ref long location1, long value)
		{
			return Interlocked.ExchangeAdd(ref location1, value) + value;
		}

		// Token: 0x0600254F RID: 9551
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern int ExchangeAdd(ref int location1, int value);

		// Token: 0x06002550 RID: 9552
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern long ExchangeAdd(ref long location1, long value);

		// Token: 0x06002551 RID: 9553 RVA: 0x00140C30 File Offset: 0x0013FE30
		public static long Read(ref long location)
		{
			return Interlocked.CompareExchange(ref location, 0L, 0L);
		}

		// Token: 0x06002552 RID: 9554
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void MemoryBarrier();

		// Token: 0x06002553 RID: 9555
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReadMemoryBarrier();

		// Token: 0x06002554 RID: 9556
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _MemoryBarrierProcessWide();

		// Token: 0x06002555 RID: 9557 RVA: 0x00140C3C File Offset: 0x0013FE3C
		public static void MemoryBarrierProcessWide()
		{
			Interlocked._MemoryBarrierProcessWide();
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x00140C43 File Offset: 0x0013FE43
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Increment(ref uint location)
		{
			return Interlocked.Add(ref location, 1U);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x00140C4C File Offset: 0x0013FE4C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Increment(ref ulong location)
		{
			return Interlocked.Add(ref location, 1UL);
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x00140C56 File Offset: 0x0013FE56
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Decrement(ref uint location)
		{
			return (uint)Interlocked.Add(Unsafe.As<uint, int>(ref location), -1);
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x00140C64 File Offset: 0x0013FE64
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Decrement(ref ulong location)
		{
			return (ulong)Interlocked.Add(Unsafe.As<ulong, long>(ref location), -1L);
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x00140C73 File Offset: 0x0013FE73
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Exchange(ref uint location1, uint value)
		{
			return (uint)Interlocked.Exchange(Unsafe.As<uint, int>(ref location1), (int)value);
		}

		// Token: 0x0600255B RID: 9563 RVA: 0x00140C81 File Offset: 0x0013FE81
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Exchange(ref ulong location1, ulong value)
		{
			return (ulong)Interlocked.Exchange(Unsafe.As<ulong, long>(ref location1), (long)value);
		}

		// Token: 0x0600255C RID: 9564 RVA: 0x00140C8F File Offset: 0x0013FE8F
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint CompareExchange(ref uint location1, uint value, uint comparand)
		{
			return (uint)Interlocked.CompareExchange(Unsafe.As<uint, int>(ref location1), (int)value, (int)comparand);
		}

		// Token: 0x0600255D RID: 9565 RVA: 0x00140C9E File Offset: 0x0013FE9E
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong CompareExchange(ref ulong location1, ulong value, ulong comparand)
		{
			return (ulong)Interlocked.CompareExchange(Unsafe.As<ulong, long>(ref location1), (long)value, (long)comparand);
		}

		// Token: 0x0600255E RID: 9566 RVA: 0x00140CAD File Offset: 0x0013FEAD
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Add(ref uint location1, uint value)
		{
			return (uint)Interlocked.Add(Unsafe.As<uint, int>(ref location1), (int)value);
		}

		// Token: 0x0600255F RID: 9567 RVA: 0x00140CBB File Offset: 0x0013FEBB
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Add(ref ulong location1, ulong value)
		{
			return (ulong)Interlocked.Add(Unsafe.As<ulong, long>(ref location1), (long)value);
		}

		// Token: 0x06002560 RID: 9568 RVA: 0x00140CC9 File Offset: 0x0013FEC9
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Read(ref ulong location)
		{
			return Interlocked.CompareExchange(ref location, 0UL, 0UL);
		}

		// Token: 0x06002561 RID: 9569 RVA: 0x00140CD8 File Offset: 0x0013FED8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int And(ref int location1, int value)
		{
			int num = location1;
			int num2;
			for (;;)
			{
				int value2 = num & value;
				num2 = Interlocked.CompareExchange(ref location1, value2, num);
				if (num2 == num)
				{
					break;
				}
				num = num2;
			}
			return num2;
		}

		// Token: 0x06002562 RID: 9570 RVA: 0x00140CFE File Offset: 0x0013FEFE
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint And(ref uint location1, uint value)
		{
			return (uint)Interlocked.And(Unsafe.As<uint, int>(ref location1), (int)value);
		}

		// Token: 0x06002563 RID: 9571 RVA: 0x00140D0C File Offset: 0x0013FF0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long And(ref long location1, long value)
		{
			long num = location1;
			long num2;
			for (;;)
			{
				long value2 = num & value;
				num2 = Interlocked.CompareExchange(ref location1, value2, num);
				if (num2 == num)
				{
					break;
				}
				num = num2;
			}
			return num2;
		}

		// Token: 0x06002564 RID: 9572 RVA: 0x00140D32 File Offset: 0x0013FF32
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong And(ref ulong location1, ulong value)
		{
			return (ulong)Interlocked.And(Unsafe.As<ulong, long>(ref location1), (long)value);
		}

		// Token: 0x06002565 RID: 9573 RVA: 0x00140D40 File Offset: 0x0013FF40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Or(ref int location1, int value)
		{
			int num = location1;
			int num2;
			for (;;)
			{
				int value2 = num | value;
				num2 = Interlocked.CompareExchange(ref location1, value2, num);
				if (num2 == num)
				{
					break;
				}
				num = num2;
			}
			return num2;
		}

		// Token: 0x06002566 RID: 9574 RVA: 0x00140D66 File Offset: 0x0013FF66
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Or(ref uint location1, uint value)
		{
			return (uint)Interlocked.Or(Unsafe.As<uint, int>(ref location1), (int)value);
		}

		// Token: 0x06002567 RID: 9575 RVA: 0x00140D74 File Offset: 0x0013FF74
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long Or(ref long location1, long value)
		{
			long num = location1;
			long num2;
			for (;;)
			{
				long value2 = num | value;
				num2 = Interlocked.CompareExchange(ref location1, value2, num);
				if (num2 == num)
				{
					break;
				}
				num = num2;
			}
			return num2;
		}

		// Token: 0x06002568 RID: 9576 RVA: 0x00140D9A File Offset: 0x0013FF9A
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Or(ref ulong location1, ulong value)
		{
			return (ulong)Interlocked.Or(Unsafe.As<ulong, long>(ref location1), (long)value);
		}
	}
}
