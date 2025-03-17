using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;
using Internal.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020002DE RID: 734
	[NullableContext(1)]
	[Nullable(0)]
	public static class Volatile
	{
		// Token: 0x0600290E RID: 10510 RVA: 0x0014A9FE File Offset: 0x00149BFE
		[NonVersionable]
		[Intrinsic]
		public static bool Read(ref bool location)
		{
			return Unsafe.As<bool, Volatile.VolatileBoolean>(ref location).Value;
		}

		// Token: 0x0600290F RID: 10511 RVA: 0x0014AA0D File Offset: 0x00149C0D
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref bool location, bool value)
		{
			Unsafe.As<bool, Volatile.VolatileBoolean>(ref location).Value = value;
		}

		// Token: 0x06002910 RID: 10512 RVA: 0x0014AA1D File Offset: 0x00149C1D
		[NonVersionable]
		[Intrinsic]
		public static byte Read(ref byte location)
		{
			return Unsafe.As<byte, Volatile.VolatileByte>(ref location).Value;
		}

		// Token: 0x06002911 RID: 10513 RVA: 0x0014AA2C File Offset: 0x00149C2C
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref byte location, byte value)
		{
			Unsafe.As<byte, Volatile.VolatileByte>(ref location).Value = value;
		}

		// Token: 0x06002912 RID: 10514 RVA: 0x0014AA3C File Offset: 0x00149C3C
		[NonVersionable]
		[Intrinsic]
		public unsafe static double Read(ref double location)
		{
			long num = Volatile.Read(Unsafe.As<double, long>(ref location));
			return *(double*)(&num);
		}

		// Token: 0x06002913 RID: 10515 RVA: 0x0014AA59 File Offset: 0x00149C59
		[NonVersionable]
		[Intrinsic]
		public unsafe static void Write(ref double location, double value)
		{
			Volatile.Write(Unsafe.As<double, long>(ref location), *(long*)(&value));
		}

		// Token: 0x06002914 RID: 10516 RVA: 0x0014AA6A File Offset: 0x00149C6A
		[Intrinsic]
		[NonVersionable]
		public static short Read(ref short location)
		{
			return Unsafe.As<short, Volatile.VolatileInt16>(ref location).Value;
		}

		// Token: 0x06002915 RID: 10517 RVA: 0x0014AA79 File Offset: 0x00149C79
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref short location, short value)
		{
			Unsafe.As<short, Volatile.VolatileInt16>(ref location).Value = value;
		}

		// Token: 0x06002916 RID: 10518 RVA: 0x0014AA89 File Offset: 0x00149C89
		[Intrinsic]
		[NonVersionable]
		public static int Read(ref int location)
		{
			return Unsafe.As<int, Volatile.VolatileInt32>(ref location).Value;
		}

		// Token: 0x06002917 RID: 10519 RVA: 0x0014AA98 File Offset: 0x00149C98
		[NonVersionable]
		[Intrinsic]
		public static void Write(ref int location, int value)
		{
			Unsafe.As<int, Volatile.VolatileInt32>(ref location).Value = value;
		}

		// Token: 0x06002918 RID: 10520 RVA: 0x0014AAA8 File Offset: 0x00149CA8
		[NonVersionable]
		[Intrinsic]
		public static long Read(ref long location)
		{
			return (long)Unsafe.As<long, Volatile.VolatileIntPtr>(ref location).Value;
		}

		// Token: 0x06002919 RID: 10521 RVA: 0x0014AABC File Offset: 0x00149CBC
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref long location, long value)
		{
			Unsafe.As<long, Volatile.VolatileIntPtr>(ref location).Value = (IntPtr)value;
		}

		// Token: 0x0600291A RID: 10522 RVA: 0x0014AAD1 File Offset: 0x00149CD1
		[Intrinsic]
		[NonVersionable]
		public static IntPtr Read(ref IntPtr location)
		{
			return Unsafe.As<IntPtr, Volatile.VolatileIntPtr>(ref location).Value;
		}

		// Token: 0x0600291B RID: 10523 RVA: 0x0014AAE0 File Offset: 0x00149CE0
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref IntPtr location, IntPtr value)
		{
			Unsafe.As<IntPtr, Volatile.VolatileIntPtr>(ref location).Value = value;
		}

		// Token: 0x0600291C RID: 10524 RVA: 0x0014AAF0 File Offset: 0x00149CF0
		[Intrinsic]
		[CLSCompliant(false)]
		[NonVersionable]
		public static sbyte Read(ref sbyte location)
		{
			return Unsafe.As<sbyte, Volatile.VolatileSByte>(ref location).Value;
		}

		// Token: 0x0600291D RID: 10525 RVA: 0x0014AAFF File Offset: 0x00149CFF
		[CLSCompliant(false)]
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref sbyte location, sbyte value)
		{
			Unsafe.As<sbyte, Volatile.VolatileSByte>(ref location).Value = value;
		}

		// Token: 0x0600291E RID: 10526 RVA: 0x0014AB0F File Offset: 0x00149D0F
		[NonVersionable]
		[Intrinsic]
		public static float Read(ref float location)
		{
			return Unsafe.As<float, Volatile.VolatileSingle>(ref location).Value;
		}

		// Token: 0x0600291F RID: 10527 RVA: 0x0014AB1E File Offset: 0x00149D1E
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref float location, float value)
		{
			Unsafe.As<float, Volatile.VolatileSingle>(ref location).Value = value;
		}

		// Token: 0x06002920 RID: 10528 RVA: 0x0014AB2E File Offset: 0x00149D2E
		[NonVersionable]
		[Intrinsic]
		[CLSCompliant(false)]
		public static ushort Read(ref ushort location)
		{
			return Unsafe.As<ushort, Volatile.VolatileUInt16>(ref location).Value;
		}

		// Token: 0x06002921 RID: 10529 RVA: 0x0014AB3D File Offset: 0x00149D3D
		[Intrinsic]
		[CLSCompliant(false)]
		[NonVersionable]
		public static void Write(ref ushort location, ushort value)
		{
			Unsafe.As<ushort, Volatile.VolatileUInt16>(ref location).Value = value;
		}

		// Token: 0x06002922 RID: 10530 RVA: 0x0014AB4D File Offset: 0x00149D4D
		[CLSCompliant(false)]
		[Intrinsic]
		[NonVersionable]
		public static uint Read(ref uint location)
		{
			return Unsafe.As<uint, Volatile.VolatileUInt32>(ref location).Value;
		}

		// Token: 0x06002923 RID: 10531 RVA: 0x0014AB5C File Offset: 0x00149D5C
		[CLSCompliant(false)]
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref uint location, uint value)
		{
			Unsafe.As<uint, Volatile.VolatileUInt32>(ref location).Value = value;
		}

		// Token: 0x06002924 RID: 10532 RVA: 0x0014AB6C File Offset: 0x00149D6C
		[Intrinsic]
		[CLSCompliant(false)]
		[NonVersionable]
		public static ulong Read(ref ulong location)
		{
			return (ulong)Volatile.Read(Unsafe.As<ulong, long>(ref location));
		}

		// Token: 0x06002925 RID: 10533 RVA: 0x0014AB79 File Offset: 0x00149D79
		[CLSCompliant(false)]
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref ulong location, ulong value)
		{
			Volatile.Write(Unsafe.As<ulong, long>(ref location), (long)value);
		}

		// Token: 0x06002926 RID: 10534 RVA: 0x0014AB87 File Offset: 0x00149D87
		[NonVersionable]
		[Intrinsic]
		[CLSCompliant(false)]
		public static UIntPtr Read(ref UIntPtr location)
		{
			return Unsafe.As<UIntPtr, Volatile.VolatileUIntPtr>(ref location).Value;
		}

		// Token: 0x06002927 RID: 10535 RVA: 0x0014AB96 File Offset: 0x00149D96
		[CLSCompliant(false)]
		[Intrinsic]
		[NonVersionable]
		public static void Write(ref UIntPtr location, UIntPtr value)
		{
			Unsafe.As<UIntPtr, Volatile.VolatileUIntPtr>(ref location).Value = value;
		}

		// Token: 0x06002928 RID: 10536 RVA: 0x0014ABA6 File Offset: 0x00149DA6
		[Intrinsic]
		[NonVersionable]
		[return: NotNullIfNotNull("location")]
		public static T Read<[Nullable(2)] T>(ref T location) where T : class
		{
			return Unsafe.As<T>(Unsafe.As<T, Volatile.VolatileObject>(ref location).Value);
		}

		// Token: 0x06002929 RID: 10537 RVA: 0x0014ABBA File Offset: 0x00149DBA
		[Intrinsic]
		[NonVersionable]
		public static void Write<[Nullable(2)] T>([NotNullIfNotNull("value")] ref T location, T value) where T : class
		{
			Unsafe.As<T, Volatile.VolatileObject>(ref location).Value = value;
		}

		// Token: 0x020002DF RID: 735
		private struct VolatileBoolean
		{
			// Token: 0x04000B27 RID: 2855
			public volatile bool Value;
		}

		// Token: 0x020002E0 RID: 736
		private struct VolatileByte
		{
			// Token: 0x04000B28 RID: 2856
			public volatile byte Value;
		}

		// Token: 0x020002E1 RID: 737
		private struct VolatileInt16
		{
			// Token: 0x04000B29 RID: 2857
			public volatile short Value;
		}

		// Token: 0x020002E2 RID: 738
		private struct VolatileInt32
		{
			// Token: 0x04000B2A RID: 2858
			public volatile int Value;
		}

		// Token: 0x020002E3 RID: 739
		private struct VolatileIntPtr
		{
			// Token: 0x04000B2B RID: 2859
			public volatile IntPtr Value;
		}

		// Token: 0x020002E4 RID: 740
		private struct VolatileSByte
		{
			// Token: 0x04000B2C RID: 2860
			public volatile sbyte Value;
		}

		// Token: 0x020002E5 RID: 741
		private struct VolatileSingle
		{
			// Token: 0x04000B2D RID: 2861
			public volatile float Value;
		}

		// Token: 0x020002E6 RID: 742
		private struct VolatileUInt16
		{
			// Token: 0x04000B2E RID: 2862
			public volatile ushort Value;
		}

		// Token: 0x020002E7 RID: 743
		private struct VolatileUInt32
		{
			// Token: 0x04000B2F RID: 2863
			public volatile uint Value;
		}

		// Token: 0x020002E8 RID: 744
		private struct VolatileUIntPtr
		{
			// Token: 0x04000B30 RID: 2864
			public volatile UIntPtr Value;
		}

		// Token: 0x020002E9 RID: 745
		private struct VolatileObject
		{
			// Token: 0x04000B31 RID: 2865
			public volatile object Value;
		}
	}
}
