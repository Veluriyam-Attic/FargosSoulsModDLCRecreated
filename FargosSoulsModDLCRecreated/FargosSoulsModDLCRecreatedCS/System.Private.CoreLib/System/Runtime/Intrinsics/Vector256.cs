using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics
{
	// Token: 0x02000414 RID: 1044
	public static class Vector256
	{
		// Token: 0x060033AF RID: 13231 RVA: 0x0016EAA4 File Offset: 0x0016DCA4
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static Vector256<U> As<T, U>(this Vector256<T> vector) where T : struct where U : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			ThrowHelper.ThrowForUnsupportedVectorBaseType<U>();
			return *Unsafe.As<Vector256<T>, Vector256<U>>(ref vector);
		}

		// Token: 0x060033B0 RID: 13232 RVA: 0x0016EABC File Offset: 0x0016DCBC
		[Intrinsic]
		public static Vector256<byte> AsByte<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, byte>();
		}

		// Token: 0x060033B1 RID: 13233 RVA: 0x0016EAC4 File Offset: 0x0016DCC4
		[Intrinsic]
		public static Vector256<double> AsDouble<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, double>();
		}

		// Token: 0x060033B2 RID: 13234 RVA: 0x0016EACC File Offset: 0x0016DCCC
		[Intrinsic]
		public static Vector256<short> AsInt16<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, short>();
		}

		// Token: 0x060033B3 RID: 13235 RVA: 0x0016EAD4 File Offset: 0x0016DCD4
		[Intrinsic]
		public static Vector256<int> AsInt32<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, int>();
		}

		// Token: 0x060033B4 RID: 13236 RVA: 0x0016EADC File Offset: 0x0016DCDC
		[Intrinsic]
		public static Vector256<long> AsInt64<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, long>();
		}

		// Token: 0x060033B5 RID: 13237 RVA: 0x0016EAE4 File Offset: 0x0016DCE4
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector256<sbyte> AsSByte<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, sbyte>();
		}

		// Token: 0x060033B6 RID: 13238 RVA: 0x0016EAEC File Offset: 0x0016DCEC
		[Intrinsic]
		public static Vector256<float> AsSingle<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, float>();
		}

		// Token: 0x060033B7 RID: 13239 RVA: 0x0016EAF4 File Offset: 0x0016DCF4
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector256<ushort> AsUInt16<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, ushort>();
		}

		// Token: 0x060033B8 RID: 13240 RVA: 0x0016EAFC File Offset: 0x0016DCFC
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector256<uint> AsUInt32<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, uint>();
		}

		// Token: 0x060033B9 RID: 13241 RVA: 0x0016EB04 File Offset: 0x0016DD04
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector256<ulong> AsUInt64<T>(this Vector256<T> vector) where T : struct
		{
			return vector.As<T, ulong>();
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x0016EB0C File Offset: 0x0016DD0C
		[Intrinsic]
		public static Vector256<T> AsVector256<T>(this Vector<T> value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			Vector256<T> result = default(Vector256<T>);
			Unsafe.WriteUnaligned<Vector<T>>(Unsafe.As<Vector256<T>, byte>(ref result), value);
			return result;
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x0016EB34 File Offset: 0x0016DD34
		[Intrinsic]
		public unsafe static Vector<T> AsVector<T>(this Vector256<T> value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return *Unsafe.As<Vector256<T>, Vector<T>>(ref value);
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x0016EB47 File Offset: 0x0016DD47
		[Intrinsic]
		public static Vector256<byte> Create(byte value)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|14_0(value);
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x0016EB5D File Offset: 0x0016DD5D
		[Intrinsic]
		public static Vector256<double> Create(double value)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|15_0(value);
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x0016EB73 File Offset: 0x0016DD73
		[Intrinsic]
		public static Vector256<short> Create(short value)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|16_0(value);
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x0016EB89 File Offset: 0x0016DD89
		[Intrinsic]
		public static Vector256<int> Create(int value)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|17_0(value);
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x0016EB9F File Offset: 0x0016DD9F
		[Intrinsic]
		public static Vector256<long> Create(long value)
		{
			if (Sse2.X64.IsSupported && Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|18_0(value);
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x0016EBBC File Offset: 0x0016DDBC
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector256<sbyte> Create(sbyte value)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|19_0(value);
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x0016EBD2 File Offset: 0x0016DDD2
		[Intrinsic]
		public static Vector256<float> Create(float value)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|20_0(value);
		}

		// Token: 0x060033C3 RID: 13251 RVA: 0x0016EBE8 File Offset: 0x0016DDE8
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector256<ushort> Create(ushort value)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|21_0(value);
		}

		// Token: 0x060033C4 RID: 13252 RVA: 0x0016EBFE File Offset: 0x0016DDFE
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector256<uint> Create(uint value)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|22_0(value);
		}

		// Token: 0x060033C5 RID: 13253 RVA: 0x0016EC14 File Offset: 0x0016DE14
		[CLSCompliant(false)]
		[Intrinsic]
		public static Vector256<ulong> Create(ulong value)
		{
			if (Sse2.X64.IsSupported && Avx.IsSupported)
			{
				return Vector256.Create(value);
			}
			return Vector256.<Create>g__SoftwareFallback|23_0(value);
		}

		// Token: 0x060033C6 RID: 13254 RVA: 0x0016EC34 File Offset: 0x0016DE34
		[Intrinsic]
		public static Vector256<byte> Create(byte e0, byte e1, byte e2, byte e3, byte e4, byte e5, byte e6, byte e7, byte e8, byte e9, byte e10, byte e11, byte e12, byte e13, byte e14, byte e15, byte e16, byte e17, byte e18, byte e19, byte e20, byte e21, byte e22, byte e23, byte e24, byte e25, byte e26, byte e27, byte e28, byte e29, byte e30, byte e31)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15, e16, e17, e18, e19, e20, e21, e22, e23, e24, e25, e26, e27, e28, e29, e30, e31);
			}
			return Vector256.<Create>g__SoftwareFallback|24_0(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15, e16, e17, e18, e19, e20, e21, e22, e23, e24, e25, e26, e27, e28, e29, e30, e31);
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x0016ECCB File Offset: 0x0016DECB
		[Intrinsic]
		public static Vector256<double> Create(double e0, double e1, double e2, double e3)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3);
			}
			return Vector256.<Create>g__SoftwareFallback|25_0(e0, e1, e2, e3);
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x0016ECE8 File Offset: 0x0016DEE8
		[Intrinsic]
		public static Vector256<short> Create(short e0, short e1, short e2, short e3, short e4, short e5, short e6, short e7, short e8, short e9, short e10, short e11, short e12, short e13, short e14, short e15)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15);
			}
			return Vector256.<Create>g__SoftwareFallback|26_0(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15);
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x0016ED3F File Offset: 0x0016DF3F
		[Intrinsic]
		public static Vector256<int> Create(int e0, int e1, int e2, int e3, int e4, int e5, int e6, int e7)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3, e4, e5, e6, e7);
			}
			return Vector256.<Create>g__SoftwareFallback|27_0(e0, e1, e2, e3, e4, e5, e6, e7);
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x0016ED6B File Offset: 0x0016DF6B
		[Intrinsic]
		public static Vector256<long> Create(long e0, long e1, long e2, long e3)
		{
			if (Sse2.X64.IsSupported && Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3);
			}
			return Vector256.<Create>g__SoftwareFallback|28_0(e0, e1, e2, e3);
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x0016ED90 File Offset: 0x0016DF90
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector256<sbyte> Create(sbyte e0, sbyte e1, sbyte e2, sbyte e3, sbyte e4, sbyte e5, sbyte e6, sbyte e7, sbyte e8, sbyte e9, sbyte e10, sbyte e11, sbyte e12, sbyte e13, sbyte e14, sbyte e15, sbyte e16, sbyte e17, sbyte e18, sbyte e19, sbyte e20, sbyte e21, sbyte e22, sbyte e23, sbyte e24, sbyte e25, sbyte e26, sbyte e27, sbyte e28, sbyte e29, sbyte e30, sbyte e31)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15, e16, e17, e18, e19, e20, e21, e22, e23, e24, e25, e26, e27, e28, e29, e30, e31);
			}
			return Vector256.<Create>g__SoftwareFallback|29_0(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15, e16, e17, e18, e19, e20, e21, e22, e23, e24, e25, e26, e27, e28, e29, e30, e31);
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x0016EE27 File Offset: 0x0016E027
		[Intrinsic]
		public static Vector256<float> Create(float e0, float e1, float e2, float e3, float e4, float e5, float e6, float e7)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3, e4, e5, e6, e7);
			}
			return Vector256.<Create>g__SoftwareFallback|30_0(e0, e1, e2, e3, e4, e5, e6, e7);
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x0016EE54 File Offset: 0x0016E054
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector256<ushort> Create(ushort e0, ushort e1, ushort e2, ushort e3, ushort e4, ushort e5, ushort e6, ushort e7, ushort e8, ushort e9, ushort e10, ushort e11, ushort e12, ushort e13, ushort e14, ushort e15)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15);
			}
			return Vector256.<Create>g__SoftwareFallback|31_0(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15);
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x0016EEAB File Offset: 0x0016E0AB
		[CLSCompliant(false)]
		[Intrinsic]
		public static Vector256<uint> Create(uint e0, uint e1, uint e2, uint e3, uint e4, uint e5, uint e6, uint e7)
		{
			if (Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3, e4, e5, e6, e7);
			}
			return Vector256.<Create>g__SoftwareFallback|32_0(e0, e1, e2, e3, e4, e5, e6, e7);
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x0016EED7 File Offset: 0x0016E0D7
		[CLSCompliant(false)]
		[Intrinsic]
		public static Vector256<ulong> Create(ulong e0, ulong e1, ulong e2, ulong e3)
		{
			if (Sse2.X64.IsSupported && Avx.IsSupported)
			{
				return Vector256.Create(e0, e1, e2, e3);
			}
			return Vector256.<Create>g__SoftwareFallback|33_0(e0, e1, e2, e3);
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x0016EEFC File Offset: 0x0016E0FC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<byte> Create(Vector128<byte> lower, Vector128<byte> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<byte> vector = lower.ToVector256Unsafe<byte>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|34_0(lower, upper);
		}

		// Token: 0x060033D1 RID: 13265 RVA: 0x0016EF28 File Offset: 0x0016E128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<double> Create(Vector128<double> lower, Vector128<double> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<double> vector = lower.ToVector256Unsafe<double>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|35_0(lower, upper);
		}

		// Token: 0x060033D2 RID: 13266 RVA: 0x0016EF54 File Offset: 0x0016E154
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<short> Create(Vector128<short> lower, Vector128<short> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<short> vector = lower.ToVector256Unsafe<short>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|36_0(lower, upper);
		}

		// Token: 0x060033D3 RID: 13267 RVA: 0x0016EF80 File Offset: 0x0016E180
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<int> Create(Vector128<int> lower, Vector128<int> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<int> vector = lower.ToVector256Unsafe<int>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|37_0(lower, upper);
		}

		// Token: 0x060033D4 RID: 13268 RVA: 0x0016EFAC File Offset: 0x0016E1AC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<long> Create(Vector128<long> lower, Vector128<long> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<long> vector = lower.ToVector256Unsafe<long>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|38_0(lower, upper);
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x0016EFD8 File Offset: 0x0016E1D8
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<sbyte> Create(Vector128<sbyte> lower, Vector128<sbyte> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<sbyte> vector = lower.ToVector256Unsafe<sbyte>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|39_0(lower, upper);
		}

		// Token: 0x060033D6 RID: 13270 RVA: 0x0016F004 File Offset: 0x0016E204
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<float> Create(Vector128<float> lower, Vector128<float> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<float> vector = lower.ToVector256Unsafe<float>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|40_0(lower, upper);
		}

		// Token: 0x060033D7 RID: 13271 RVA: 0x0016F030 File Offset: 0x0016E230
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<ushort> Create(Vector128<ushort> lower, Vector128<ushort> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<ushort> vector = lower.ToVector256Unsafe<ushort>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|41_0(lower, upper);
		}

		// Token: 0x060033D8 RID: 13272 RVA: 0x0016F05C File Offset: 0x0016E25C
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<uint> Create(Vector128<uint> lower, Vector128<uint> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<uint> vector = lower.ToVector256Unsafe<uint>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|42_0(lower, upper);
		}

		// Token: 0x060033D9 RID: 13273 RVA: 0x0016F088 File Offset: 0x0016E288
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<ulong> Create(Vector128<ulong> lower, Vector128<ulong> upper)
		{
			if (Avx.IsSupported)
			{
				Vector256<ulong> vector = lower.ToVector256Unsafe<ulong>();
				return vector.WithUpper(upper);
			}
			return Vector256.<Create>g__SoftwareFallback|43_0(lower, upper);
		}

		// Token: 0x060033DA RID: 13274 RVA: 0x0016F0B2 File Offset: 0x0016E2B2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<byte> CreateScalar(byte value)
		{
			if (Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<byte>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|44_0(value);
		}

		// Token: 0x060033DB RID: 13275 RVA: 0x0016F0CD File Offset: 0x0016E2CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<double> CreateScalar(double value)
		{
			if (Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<double>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|45_0(value);
		}

		// Token: 0x060033DC RID: 13276 RVA: 0x0016F0E8 File Offset: 0x0016E2E8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<short> CreateScalar(short value)
		{
			if (Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<short>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|46_0(value);
		}

		// Token: 0x060033DD RID: 13277 RVA: 0x0016F103 File Offset: 0x0016E303
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<int> CreateScalar(int value)
		{
			if (Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<int>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|47_0(value);
		}

		// Token: 0x060033DE RID: 13278 RVA: 0x0016F11E File Offset: 0x0016E31E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<long> CreateScalar(long value)
		{
			if (Sse2.X64.IsSupported && Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<long>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|48_0(value);
		}

		// Token: 0x060033DF RID: 13279 RVA: 0x0016F140 File Offset: 0x0016E340
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<sbyte> CreateScalar(sbyte value)
		{
			if (Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<sbyte>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|49_0(value);
		}

		// Token: 0x060033E0 RID: 13280 RVA: 0x0016F15B File Offset: 0x0016E35B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<float> CreateScalar(float value)
		{
			if (Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<float>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|50_0(value);
		}

		// Token: 0x060033E1 RID: 13281 RVA: 0x0016F176 File Offset: 0x0016E376
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<ushort> CreateScalar(ushort value)
		{
			if (Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<ushort>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|51_0(value);
		}

		// Token: 0x060033E2 RID: 13282 RVA: 0x0016F191 File Offset: 0x0016E391
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<uint> CreateScalar(uint value)
		{
			if (Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<uint>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|52_0(value);
		}

		// Token: 0x060033E3 RID: 13283 RVA: 0x0016F1AC File Offset: 0x0016E3AC
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<ulong> CreateScalar(ulong value)
		{
			if (Sse2.X64.IsSupported && Avx.IsSupported)
			{
				return Vector128.CreateScalar(value).ToVector256<ulong>();
			}
			return Vector256.<CreateScalar>g__SoftwareFallback|53_0(value);
		}

		// Token: 0x060033E4 RID: 13284 RVA: 0x0016F1D0 File Offset: 0x0016E3D0
		[Intrinsic]
		public unsafe static Vector256<byte> CreateScalarUnsafe(byte value)
		{
			byte* ptr = stackalloc byte[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<byte>>((void*)ptr);
		}

		// Token: 0x060033E5 RID: 13285 RVA: 0x0016F1F4 File Offset: 0x0016E3F4
		[Intrinsic]
		public unsafe static Vector256<double> CreateScalarUnsafe(double value)
		{
			double* ptr = stackalloc double[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<double>>((void*)ptr);
		}

		// Token: 0x060033E6 RID: 13286 RVA: 0x0016F218 File Offset: 0x0016E418
		[Intrinsic]
		public unsafe static Vector256<short> CreateScalarUnsafe(short value)
		{
			short* ptr = stackalloc short[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<short>>((void*)ptr);
		}

		// Token: 0x060033E7 RID: 13287 RVA: 0x0016F23C File Offset: 0x0016E43C
		[Intrinsic]
		public unsafe static Vector256<int> CreateScalarUnsafe(int value)
		{
			int* ptr = stackalloc int[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<int>>((void*)ptr);
		}

		// Token: 0x060033E8 RID: 13288 RVA: 0x0016F260 File Offset: 0x0016E460
		[Intrinsic]
		public unsafe static Vector256<long> CreateScalarUnsafe(long value)
		{
			long* ptr = stackalloc long[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<long>>((void*)ptr);
		}

		// Token: 0x060033E9 RID: 13289 RVA: 0x0016F284 File Offset: 0x0016E484
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector256<sbyte> CreateScalarUnsafe(sbyte value)
		{
			sbyte* ptr = stackalloc sbyte[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<sbyte>>((void*)ptr);
		}

		// Token: 0x060033EA RID: 13290 RVA: 0x0016F2A8 File Offset: 0x0016E4A8
		[Intrinsic]
		public unsafe static Vector256<float> CreateScalarUnsafe(float value)
		{
			float* ptr = stackalloc float[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<float>>((void*)ptr);
		}

		// Token: 0x060033EB RID: 13291 RVA: 0x0016F2CC File Offset: 0x0016E4CC
		[Intrinsic]
		[CLSCompliant(false)]
		public unsafe static Vector256<ushort> CreateScalarUnsafe(ushort value)
		{
			ushort* ptr = stackalloc ushort[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<ushort>>((void*)ptr);
		}

		// Token: 0x060033EC RID: 13292 RVA: 0x0016F2F0 File Offset: 0x0016E4F0
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector256<uint> CreateScalarUnsafe(uint value)
		{
			uint* ptr = stackalloc uint[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<uint>>((void*)ptr);
		}

		// Token: 0x060033ED RID: 13293 RVA: 0x0016F314 File Offset: 0x0016E514
		[Intrinsic]
		[CLSCompliant(false)]
		public unsafe static Vector256<ulong> CreateScalarUnsafe(ulong value)
		{
			ulong* ptr = stackalloc ulong[(UIntPtr)32];
			*ptr = value;
			return *Unsafe.AsRef<Vector256<ulong>>((void*)ptr);
		}

		// Token: 0x060033EE RID: 13294 RVA: 0x0016F338 File Offset: 0x0016E538
		[Intrinsic]
		public unsafe static T GetElement<T>(this Vector256<T> vector, int index) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (index >= Vector256<T>.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			ref T source = ref Unsafe.As<Vector256<T>, T>(ref vector);
			return *Unsafe.Add<T>(ref source, index);
		}

		// Token: 0x060033EF RID: 13295 RVA: 0x0016F370 File Offset: 0x0016E570
		[Intrinsic]
		public unsafe static Vector256<T> WithElement<T>(this Vector256<T> vector, int index, T value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (index >= Vector256<T>.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			Vector256<T> result = vector;
			ref T source = ref Unsafe.As<Vector256<T>, T>(ref result);
			*Unsafe.Add<T>(ref source, index) = value;
			return result;
		}

		// Token: 0x060033F0 RID: 13296 RVA: 0x0016F3A9 File Offset: 0x0016E5A9
		[Intrinsic]
		public unsafe static Vector128<T> GetLower<T>(this Vector256<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return *Unsafe.As<Vector256<T>, Vector128<T>>(ref vector);
		}

		// Token: 0x060033F1 RID: 13297 RVA: 0x0016F3BC File Offset: 0x0016E5BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<T> WithLower<T>(this Vector256<T> vector, Vector128<T> value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (Avx2.IsSupported && typeof(T) != typeof(float) && typeof(T) != typeof(double))
			{
				return Avx2.InsertVector128(vector.AsByte<T>(), value.AsByte<T>(), 0).As<byte, T>();
			}
			if (Avx.IsSupported)
			{
				return Avx.InsertVector128(vector.AsSingle<T>(), value.AsSingle<T>(), 0).As<float, T>();
			}
			return Vector256.<WithLower>g__SoftwareFallback|67_0<T>(vector, value);
		}

		// Token: 0x060033F2 RID: 13298 RVA: 0x0016F44C File Offset: 0x0016E64C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<T> GetUpper<T>(this Vector256<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (Avx2.IsSupported && typeof(T) != typeof(float) && typeof(T) != typeof(double))
			{
				return Avx2.ExtractVector128(vector.AsByte<T>(), 1).As<byte, T>();
			}
			if (Avx.IsSupported)
			{
				return Avx.ExtractVector128(vector.AsSingle<T>(), 1).As<float, T>();
			}
			return Vector256.<GetUpper>g__SoftwareFallback|68_0<T>(vector);
		}

		// Token: 0x060033F3 RID: 13299 RVA: 0x0016F4CC File Offset: 0x0016E6CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector256<T> WithUpper<T>(this Vector256<T> vector, Vector128<T> value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (Avx2.IsSupported && typeof(T) != typeof(float) && typeof(T) != typeof(double))
			{
				return Avx2.InsertVector128(vector.AsByte<T>(), value.AsByte<T>(), 1).As<byte, T>();
			}
			if (Avx.IsSupported)
			{
				return Avx.InsertVector128(vector.AsSingle<T>(), value.AsSingle<T>(), 1).As<float, T>();
			}
			return Vector256.<WithUpper>g__SoftwareFallback|69_0<T>(vector, value);
		}

		// Token: 0x060033F4 RID: 13300 RVA: 0x0016F559 File Offset: 0x0016E759
		[Intrinsic]
		public unsafe static T ToScalar<T>(this Vector256<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return *Unsafe.As<Vector256<T>, T>(ref vector);
		}

		// Token: 0x060033F5 RID: 13301 RVA: 0x0016F56C File Offset: 0x0016E76C
		[CompilerGenerated]
		internal unsafe static Vector256<byte> <Create>g__SoftwareFallback|14_0(byte value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = value;
			*(intPtr + 1) = value;
			*(intPtr + 2) = value;
			*(intPtr + 3) = value;
			*(intPtr + 4) = value;
			*(intPtr + 5) = value;
			*(intPtr + 6) = value;
			*(intPtr + 7) = value;
			*(intPtr + 8) = value;
			*(intPtr + 9) = value;
			*(intPtr + 10) = value;
			*(intPtr + 11) = value;
			*(intPtr + 12) = value;
			*(intPtr + 13) = value;
			*(intPtr + 14) = value;
			*(intPtr + 15) = value;
			*(intPtr + 16) = value;
			*(intPtr + 17) = value;
			*(intPtr + 18) = value;
			*(intPtr + 19) = value;
			*(intPtr + 20) = value;
			*(intPtr + 21) = value;
			*(intPtr + 22) = value;
			*(intPtr + 23) = value;
			*(intPtr + 24) = value;
			*(intPtr + 25) = value;
			*(intPtr + 26) = value;
			*(intPtr + 27) = value;
			*(intPtr + 28) = value;
			*(intPtr + 29) = value;
			*(intPtr + 30) = value;
			*(intPtr + 31) = value;
			byte* source = intPtr;
			return *Unsafe.AsRef<Vector256<byte>>((void*)source);
		}

		// Token: 0x060033F6 RID: 13302 RVA: 0x0016F640 File Offset: 0x0016E840
		[CompilerGenerated]
		internal unsafe static Vector256<double> <Create>g__SoftwareFallback|15_0(double value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = value;
			*(intPtr + 8) = value;
			*(intPtr + (IntPtr)2 * 8) = value;
			*(intPtr + (IntPtr)3 * 8) = value;
			double* source = intPtr;
			return *Unsafe.AsRef<Vector256<double>>((void*)source);
		}

		// Token: 0x060033F7 RID: 13303 RVA: 0x0016F678 File Offset: 0x0016E878
		[CompilerGenerated]
		internal unsafe static Vector256<short> <Create>g__SoftwareFallback|16_0(short value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = value;
			*(intPtr + 2) = value;
			*(intPtr + (IntPtr)2 * 2) = value;
			*(intPtr + (IntPtr)3 * 2) = value;
			*(intPtr + (IntPtr)4 * 2) = value;
			*(intPtr + (IntPtr)5 * 2) = value;
			*(intPtr + (IntPtr)6 * 2) = value;
			*(intPtr + (IntPtr)7 * 2) = value;
			*(intPtr + (IntPtr)8 * 2) = value;
			*(intPtr + (IntPtr)9 * 2) = value;
			*(intPtr + (IntPtr)10 * 2) = value;
			*(intPtr + (IntPtr)11 * 2) = value;
			*(intPtr + (IntPtr)12 * 2) = value;
			*(intPtr + (IntPtr)13 * 2) = value;
			*(intPtr + (IntPtr)14 * 2) = value;
			*(intPtr + (IntPtr)15 * 2) = value;
			short* source = intPtr;
			return *Unsafe.AsRef<Vector256<short>>((void*)source);
		}

		// Token: 0x060033F8 RID: 13304 RVA: 0x0016F718 File Offset: 0x0016E918
		[CompilerGenerated]
		internal unsafe static Vector256<int> <Create>g__SoftwareFallback|17_0(int value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = value;
			*(intPtr + 4) = value;
			*(intPtr + (IntPtr)2 * 4) = value;
			*(intPtr + (IntPtr)3 * 4) = value;
			*(intPtr + (IntPtr)4 * 4) = value;
			*(intPtr + (IntPtr)5 * 4) = value;
			*(intPtr + (IntPtr)6 * 4) = value;
			*(intPtr + (IntPtr)7 * 4) = value;
			int* source = intPtr;
			return *Unsafe.AsRef<Vector256<int>>((void*)source);
		}

		// Token: 0x060033F9 RID: 13305 RVA: 0x0016F770 File Offset: 0x0016E970
		[CompilerGenerated]
		internal unsafe static Vector256<long> <Create>g__SoftwareFallback|18_0(long value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = value;
			*(intPtr + 8) = value;
			*(intPtr + (IntPtr)2 * 8) = value;
			*(intPtr + (IntPtr)3 * 8) = value;
			long* source = intPtr;
			return *Unsafe.AsRef<Vector256<long>>((void*)source);
		}

		// Token: 0x060033FA RID: 13306 RVA: 0x0016F7A8 File Offset: 0x0016E9A8
		[CompilerGenerated]
		internal unsafe static Vector256<sbyte> <Create>g__SoftwareFallback|19_0(sbyte value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = (byte)value;
			*(intPtr + 1) = (byte)value;
			*(intPtr + 2) = (byte)value;
			*(intPtr + 3) = (byte)value;
			*(intPtr + 4) = (byte)value;
			*(intPtr + 5) = (byte)value;
			*(intPtr + 6) = (byte)value;
			*(intPtr + 7) = (byte)value;
			*(intPtr + 8) = (byte)value;
			*(intPtr + 9) = (byte)value;
			*(intPtr + 10) = (byte)value;
			*(intPtr + 11) = (byte)value;
			*(intPtr + 12) = (byte)value;
			*(intPtr + 13) = (byte)value;
			*(intPtr + 14) = (byte)value;
			*(intPtr + 15) = (byte)value;
			*(intPtr + 16) = (byte)value;
			*(intPtr + 17) = (byte)value;
			*(intPtr + 18) = (byte)value;
			*(intPtr + 19) = (byte)value;
			*(intPtr + 20) = (byte)value;
			*(intPtr + 21) = (byte)value;
			*(intPtr + 22) = (byte)value;
			*(intPtr + 23) = (byte)value;
			*(intPtr + 24) = (byte)value;
			*(intPtr + 25) = (byte)value;
			*(intPtr + 26) = (byte)value;
			*(intPtr + 27) = (byte)value;
			*(intPtr + 28) = (byte)value;
			*(intPtr + 29) = (byte)value;
			*(intPtr + 30) = (byte)value;
			*(intPtr + 31) = (byte)value;
			sbyte* source = intPtr;
			return *Unsafe.AsRef<Vector256<sbyte>>((void*)source);
		}

		// Token: 0x060033FB RID: 13307 RVA: 0x0016F87C File Offset: 0x0016EA7C
		[CompilerGenerated]
		internal unsafe static Vector256<float> <Create>g__SoftwareFallback|20_0(float value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = value;
			*(intPtr + 4) = value;
			*(intPtr + (IntPtr)2 * 4) = value;
			*(intPtr + (IntPtr)3 * 4) = value;
			*(intPtr + (IntPtr)4 * 4) = value;
			*(intPtr + (IntPtr)5 * 4) = value;
			*(intPtr + (IntPtr)6 * 4) = value;
			*(intPtr + (IntPtr)7 * 4) = value;
			float* source = intPtr;
			return *Unsafe.AsRef<Vector256<float>>((void*)source);
		}

		// Token: 0x060033FC RID: 13308 RVA: 0x0016F8D4 File Offset: 0x0016EAD4
		[CompilerGenerated]
		internal unsafe static Vector256<ushort> <Create>g__SoftwareFallback|21_0(ushort value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = (short)value;
			*(intPtr + 2) = (short)value;
			*(intPtr + (IntPtr)2 * 2) = (short)value;
			*(intPtr + (IntPtr)3 * 2) = (short)value;
			*(intPtr + (IntPtr)4 * 2) = (short)value;
			*(intPtr + (IntPtr)5 * 2) = (short)value;
			*(intPtr + (IntPtr)6 * 2) = (short)value;
			*(intPtr + (IntPtr)7 * 2) = (short)value;
			*(intPtr + (IntPtr)8 * 2) = (short)value;
			*(intPtr + (IntPtr)9 * 2) = (short)value;
			*(intPtr + (IntPtr)10 * 2) = (short)value;
			*(intPtr + (IntPtr)11 * 2) = (short)value;
			*(intPtr + (IntPtr)12 * 2) = (short)value;
			*(intPtr + (IntPtr)13 * 2) = (short)value;
			*(intPtr + (IntPtr)14 * 2) = (short)value;
			*(intPtr + (IntPtr)15 * 2) = (short)value;
			ushort* source = intPtr;
			return *Unsafe.AsRef<Vector256<ushort>>((void*)source);
		}

		// Token: 0x060033FD RID: 13309 RVA: 0x0016F974 File Offset: 0x0016EB74
		[CompilerGenerated]
		internal unsafe static Vector256<uint> <Create>g__SoftwareFallback|22_0(uint value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = (int)value;
			*(intPtr + 4) = (int)value;
			*(intPtr + (IntPtr)2 * 4) = (int)value;
			*(intPtr + (IntPtr)3 * 4) = (int)value;
			*(intPtr + (IntPtr)4 * 4) = (int)value;
			*(intPtr + (IntPtr)5 * 4) = (int)value;
			*(intPtr + (IntPtr)6 * 4) = (int)value;
			*(intPtr + (IntPtr)7 * 4) = (int)value;
			uint* source = intPtr;
			return *Unsafe.AsRef<Vector256<uint>>((void*)source);
		}

		// Token: 0x060033FE RID: 13310 RVA: 0x0016F9CC File Offset: 0x0016EBCC
		[CompilerGenerated]
		internal unsafe static Vector256<ulong> <Create>g__SoftwareFallback|23_0(ulong value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = (long)value;
			*(intPtr + 8) = (long)value;
			*(intPtr + (IntPtr)2 * 8) = (long)value;
			*(intPtr + (IntPtr)3 * 8) = (long)value;
			ulong* source = intPtr;
			return *Unsafe.AsRef<Vector256<ulong>>((void*)source);
		}

		// Token: 0x060033FF RID: 13311 RVA: 0x0016FA04 File Offset: 0x0016EC04
		[CompilerGenerated]
		internal unsafe static Vector256<byte> <Create>g__SoftwareFallback|24_0(byte e0, byte e1, byte e2, byte e3, byte e4, byte e5, byte e6, byte e7, byte e8, byte e9, byte e10, byte e11, byte e12, byte e13, byte e14, byte e15, byte e16, byte e17, byte e18, byte e19, byte e20, byte e21, byte e22, byte e23, byte e24, byte e25, byte e26, byte e27, byte e28, byte e29, byte e30, byte e31)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = e0;
			*(intPtr + 1) = e1;
			*(intPtr + 2) = e2;
			*(intPtr + 3) = e3;
			*(intPtr + 4) = e4;
			*(intPtr + 5) = e5;
			*(intPtr + 6) = e6;
			*(intPtr + 7) = e7;
			*(intPtr + 8) = e8;
			*(intPtr + 9) = e9;
			*(intPtr + 10) = e10;
			*(intPtr + 11) = e11;
			*(intPtr + 12) = e12;
			*(intPtr + 13) = e13;
			*(intPtr + 14) = e14;
			*(intPtr + 15) = e15;
			*(intPtr + 16) = e16;
			*(intPtr + 17) = e17;
			*(intPtr + 18) = e18;
			*(intPtr + 19) = e19;
			*(intPtr + 20) = e20;
			*(intPtr + 21) = e21;
			*(intPtr + 22) = e22;
			*(intPtr + 23) = e23;
			*(intPtr + 24) = e24;
			*(intPtr + 25) = e25;
			*(intPtr + 26) = e26;
			*(intPtr + 27) = e27;
			*(intPtr + 28) = e28;
			*(intPtr + 29) = e29;
			*(intPtr + 30) = e30;
			*(intPtr + 31) = e31;
			byte* source = intPtr;
			return *Unsafe.AsRef<Vector256<byte>>((void*)source);
		}

		// Token: 0x06003400 RID: 13312 RVA: 0x0016FAF4 File Offset: 0x0016ECF4
		[CompilerGenerated]
		internal unsafe static Vector256<double> <Create>g__SoftwareFallback|25_0(double e0, double e1, double e2, double e3)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = e0;
			*(intPtr + 8) = e1;
			*(intPtr + (IntPtr)2 * 8) = e2;
			*(intPtr + (IntPtr)3 * 8) = e3;
			double* source = intPtr;
			return *Unsafe.AsRef<Vector256<double>>((void*)source);
		}

		// Token: 0x06003401 RID: 13313 RVA: 0x0016FB2C File Offset: 0x0016ED2C
		[CompilerGenerated]
		internal unsafe static Vector256<short> <Create>g__SoftwareFallback|26_0(short e0, short e1, short e2, short e3, short e4, short e5, short e6, short e7, short e8, short e9, short e10, short e11, short e12, short e13, short e14, short e15)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = e0;
			*(intPtr + 2) = e1;
			*(intPtr + (IntPtr)2 * 2) = e2;
			*(intPtr + (IntPtr)3 * 2) = e3;
			*(intPtr + (IntPtr)4 * 2) = e4;
			*(intPtr + (IntPtr)5 * 2) = e5;
			*(intPtr + (IntPtr)6 * 2) = e6;
			*(intPtr + (IntPtr)7 * 2) = e7;
			*(intPtr + (IntPtr)8 * 2) = e8;
			*(intPtr + (IntPtr)9 * 2) = e9;
			*(intPtr + (IntPtr)10 * 2) = e10;
			*(intPtr + (IntPtr)11 * 2) = e11;
			*(intPtr + (IntPtr)12 * 2) = e12;
			*(intPtr + (IntPtr)13 * 2) = e13;
			*(intPtr + (IntPtr)14 * 2) = e14;
			*(intPtr + (IntPtr)15 * 2) = e15;
			short* source = intPtr;
			return *Unsafe.AsRef<Vector256<short>>((void*)source);
		}

		// Token: 0x06003402 RID: 13314 RVA: 0x0016FBD8 File Offset: 0x0016EDD8
		[CompilerGenerated]
		internal unsafe static Vector256<int> <Create>g__SoftwareFallback|27_0(int e0, int e1, int e2, int e3, int e4, int e5, int e6, int e7)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = e0;
			*(intPtr + 4) = e1;
			*(intPtr + (IntPtr)2 * 4) = e2;
			*(intPtr + (IntPtr)3 * 4) = e3;
			*(intPtr + (IntPtr)4 * 4) = e4;
			*(intPtr + (IntPtr)5 * 4) = e5;
			*(intPtr + (IntPtr)6 * 4) = e6;
			*(intPtr + (IntPtr)7 * 4) = e7;
			int* source = intPtr;
			return *Unsafe.AsRef<Vector256<int>>((void*)source);
		}

		// Token: 0x06003403 RID: 13315 RVA: 0x0016FC34 File Offset: 0x0016EE34
		[CompilerGenerated]
		internal unsafe static Vector256<long> <Create>g__SoftwareFallback|28_0(long e0, long e1, long e2, long e3)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = e0;
			*(intPtr + 8) = e1;
			*(intPtr + (IntPtr)2 * 8) = e2;
			*(intPtr + (IntPtr)3 * 8) = e3;
			long* source = intPtr;
			return *Unsafe.AsRef<Vector256<long>>((void*)source);
		}

		// Token: 0x06003404 RID: 13316 RVA: 0x0016FC6C File Offset: 0x0016EE6C
		[CompilerGenerated]
		internal unsafe static Vector256<sbyte> <Create>g__SoftwareFallback|29_0(sbyte e0, sbyte e1, sbyte e2, sbyte e3, sbyte e4, sbyte e5, sbyte e6, sbyte e7, sbyte e8, sbyte e9, sbyte e10, sbyte e11, sbyte e12, sbyte e13, sbyte e14, sbyte e15, sbyte e16, sbyte e17, sbyte e18, sbyte e19, sbyte e20, sbyte e21, sbyte e22, sbyte e23, sbyte e24, sbyte e25, sbyte e26, sbyte e27, sbyte e28, sbyte e29, sbyte e30, sbyte e31)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = (byte)e0;
			*(intPtr + 1) = (byte)e1;
			*(intPtr + 2) = (byte)e2;
			*(intPtr + 3) = (byte)e3;
			*(intPtr + 4) = (byte)e4;
			*(intPtr + 5) = (byte)e5;
			*(intPtr + 6) = (byte)e6;
			*(intPtr + 7) = (byte)e7;
			*(intPtr + 8) = (byte)e8;
			*(intPtr + 9) = (byte)e9;
			*(intPtr + 10) = (byte)e10;
			*(intPtr + 11) = (byte)e11;
			*(intPtr + 12) = (byte)e12;
			*(intPtr + 13) = (byte)e13;
			*(intPtr + 14) = (byte)e14;
			*(intPtr + 15) = (byte)e15;
			*(intPtr + 16) = (byte)e16;
			*(intPtr + 17) = (byte)e17;
			*(intPtr + 18) = (byte)e18;
			*(intPtr + 19) = (byte)e19;
			*(intPtr + 20) = (byte)e20;
			*(intPtr + 21) = (byte)e21;
			*(intPtr + 22) = (byte)e22;
			*(intPtr + 23) = (byte)e23;
			*(intPtr + 24) = (byte)e24;
			*(intPtr + 25) = (byte)e25;
			*(intPtr + 26) = (byte)e26;
			*(intPtr + 27) = (byte)e27;
			*(intPtr + 28) = (byte)e28;
			*(intPtr + 29) = (byte)e29;
			*(intPtr + 30) = (byte)e30;
			*(intPtr + 31) = (byte)e31;
			sbyte* source = intPtr;
			return *Unsafe.AsRef<Vector256<sbyte>>((void*)source);
		}

		// Token: 0x06003405 RID: 13317 RVA: 0x0016FD5C File Offset: 0x0016EF5C
		[CompilerGenerated]
		internal unsafe static Vector256<float> <Create>g__SoftwareFallback|30_0(float e0, float e1, float e2, float e3, float e4, float e5, float e6, float e7)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = e0;
			*(intPtr + 4) = e1;
			*(intPtr + (IntPtr)2 * 4) = e2;
			*(intPtr + (IntPtr)3 * 4) = e3;
			*(intPtr + (IntPtr)4 * 4) = e4;
			*(intPtr + (IntPtr)5 * 4) = e5;
			*(intPtr + (IntPtr)6 * 4) = e6;
			*(intPtr + (IntPtr)7 * 4) = e7;
			float* source = intPtr;
			return *Unsafe.AsRef<Vector256<float>>((void*)source);
		}

		// Token: 0x06003406 RID: 13318 RVA: 0x0016FDB8 File Offset: 0x0016EFB8
		[CompilerGenerated]
		internal unsafe static Vector256<ushort> <Create>g__SoftwareFallback|31_0(ushort e0, ushort e1, ushort e2, ushort e3, ushort e4, ushort e5, ushort e6, ushort e7, ushort e8, ushort e9, ushort e10, ushort e11, ushort e12, ushort e13, ushort e14, ushort e15)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = (short)e0;
			*(intPtr + 2) = (short)e1;
			*(intPtr + (IntPtr)2 * 2) = (short)e2;
			*(intPtr + (IntPtr)3 * 2) = (short)e3;
			*(intPtr + (IntPtr)4 * 2) = (short)e4;
			*(intPtr + (IntPtr)5 * 2) = (short)e5;
			*(intPtr + (IntPtr)6 * 2) = (short)e6;
			*(intPtr + (IntPtr)7 * 2) = (short)e7;
			*(intPtr + (IntPtr)8 * 2) = (short)e8;
			*(intPtr + (IntPtr)9 * 2) = (short)e9;
			*(intPtr + (IntPtr)10 * 2) = (short)e10;
			*(intPtr + (IntPtr)11 * 2) = (short)e11;
			*(intPtr + (IntPtr)12 * 2) = (short)e12;
			*(intPtr + (IntPtr)13 * 2) = (short)e13;
			*(intPtr + (IntPtr)14 * 2) = (short)e14;
			*(intPtr + (IntPtr)15 * 2) = (short)e15;
			ushort* source = intPtr;
			return *Unsafe.AsRef<Vector256<ushort>>((void*)source);
		}

		// Token: 0x06003407 RID: 13319 RVA: 0x0016FE64 File Offset: 0x0016F064
		[CompilerGenerated]
		internal unsafe static Vector256<uint> <Create>g__SoftwareFallback|32_0(uint e0, uint e1, uint e2, uint e3, uint e4, uint e5, uint e6, uint e7)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = (int)e0;
			*(intPtr + 4) = (int)e1;
			*(intPtr + (IntPtr)2 * 4) = (int)e2;
			*(intPtr + (IntPtr)3 * 4) = (int)e3;
			*(intPtr + (IntPtr)4 * 4) = (int)e4;
			*(intPtr + (IntPtr)5 * 4) = (int)e5;
			*(intPtr + (IntPtr)6 * 4) = (int)e6;
			*(intPtr + (IntPtr)7 * 4) = (int)e7;
			uint* source = intPtr;
			return *Unsafe.AsRef<Vector256<uint>>((void*)source);
		}

		// Token: 0x06003408 RID: 13320 RVA: 0x0016FEC0 File Offset: 0x0016F0C0
		[CompilerGenerated]
		internal unsafe static Vector256<ulong> <Create>g__SoftwareFallback|33_0(ulong e0, ulong e1, ulong e2, ulong e3)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)32];
			*intPtr = (long)e0;
			*(intPtr + 8) = (long)e1;
			*(intPtr + (IntPtr)2 * 8) = (long)e2;
			*(intPtr + (IntPtr)3 * 8) = (long)e3;
			ulong* source = intPtr;
			return *Unsafe.AsRef<Vector256<ulong>>((void*)source);
		}

		// Token: 0x06003409 RID: 13321 RVA: 0x0016FEF8 File Offset: 0x0016F0F8
		[CompilerGenerated]
		internal unsafe static Vector256<byte> <Create>g__SoftwareFallback|34_0(Vector128<byte> lower, Vector128<byte> upper)
		{
			Vector256<byte> zero = Vector256<byte>.Zero;
			ref Vector128<byte> ptr = ref Unsafe.As<Vector256<byte>, Vector128<byte>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<byte>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600340A RID: 13322 RVA: 0x0016FF28 File Offset: 0x0016F128
		[CompilerGenerated]
		internal unsafe static Vector256<double> <Create>g__SoftwareFallback|35_0(Vector128<double> lower, Vector128<double> upper)
		{
			Vector256<double> zero = Vector256<double>.Zero;
			ref Vector128<double> ptr = ref Unsafe.As<Vector256<double>, Vector128<double>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<double>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600340B RID: 13323 RVA: 0x0016FF58 File Offset: 0x0016F158
		[CompilerGenerated]
		internal unsafe static Vector256<short> <Create>g__SoftwareFallback|36_0(Vector128<short> lower, Vector128<short> upper)
		{
			Vector256<short> zero = Vector256<short>.Zero;
			ref Vector128<short> ptr = ref Unsafe.As<Vector256<short>, Vector128<short>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<short>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600340C RID: 13324 RVA: 0x0016FF88 File Offset: 0x0016F188
		[CompilerGenerated]
		internal unsafe static Vector256<int> <Create>g__SoftwareFallback|37_0(Vector128<int> lower, Vector128<int> upper)
		{
			Vector256<int> zero = Vector256<int>.Zero;
			ref Vector128<int> ptr = ref Unsafe.As<Vector256<int>, Vector128<int>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<int>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600340D RID: 13325 RVA: 0x0016FFB8 File Offset: 0x0016F1B8
		[CompilerGenerated]
		internal unsafe static Vector256<long> <Create>g__SoftwareFallback|38_0(Vector128<long> lower, Vector128<long> upper)
		{
			Vector256<long> zero = Vector256<long>.Zero;
			ref Vector128<long> ptr = ref Unsafe.As<Vector256<long>, Vector128<long>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<long>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600340E RID: 13326 RVA: 0x0016FFE8 File Offset: 0x0016F1E8
		[CompilerGenerated]
		internal unsafe static Vector256<sbyte> <Create>g__SoftwareFallback|39_0(Vector128<sbyte> lower, Vector128<sbyte> upper)
		{
			Vector256<sbyte> zero = Vector256<sbyte>.Zero;
			ref Vector128<sbyte> ptr = ref Unsafe.As<Vector256<sbyte>, Vector128<sbyte>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<sbyte>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600340F RID: 13327 RVA: 0x00170018 File Offset: 0x0016F218
		[CompilerGenerated]
		internal unsafe static Vector256<float> <Create>g__SoftwareFallback|40_0(Vector128<float> lower, Vector128<float> upper)
		{
			Vector256<float> zero = Vector256<float>.Zero;
			ref Vector128<float> ptr = ref Unsafe.As<Vector256<float>, Vector128<float>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<float>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x06003410 RID: 13328 RVA: 0x00170048 File Offset: 0x0016F248
		[CompilerGenerated]
		internal unsafe static Vector256<ushort> <Create>g__SoftwareFallback|41_0(Vector128<ushort> lower, Vector128<ushort> upper)
		{
			Vector256<ushort> zero = Vector256<ushort>.Zero;
			ref Vector128<ushort> ptr = ref Unsafe.As<Vector256<ushort>, Vector128<ushort>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<ushort>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x06003411 RID: 13329 RVA: 0x00170078 File Offset: 0x0016F278
		[CompilerGenerated]
		internal unsafe static Vector256<uint> <Create>g__SoftwareFallback|42_0(Vector128<uint> lower, Vector128<uint> upper)
		{
			Vector256<uint> zero = Vector256<uint>.Zero;
			ref Vector128<uint> ptr = ref Unsafe.As<Vector256<uint>, Vector128<uint>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<uint>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x06003412 RID: 13330 RVA: 0x001700A8 File Offset: 0x0016F2A8
		[CompilerGenerated]
		internal unsafe static Vector256<ulong> <Create>g__SoftwareFallback|43_0(Vector128<ulong> lower, Vector128<ulong> upper)
		{
			Vector256<ulong> zero = Vector256<ulong>.Zero;
			ref Vector128<ulong> ptr = ref Unsafe.As<Vector256<ulong>, Vector128<ulong>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector128<ulong>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x06003413 RID: 13331 RVA: 0x001700D8 File Offset: 0x0016F2D8
		[CompilerGenerated]
		internal static Vector256<byte> <CreateScalar>g__SoftwareFallback|44_0(byte value)
		{
			Vector256<byte> zero = Vector256<byte>.Zero;
			Unsafe.WriteUnaligned<byte>(Unsafe.As<Vector256<byte>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003414 RID: 13332 RVA: 0x001700FC File Offset: 0x0016F2FC
		[CompilerGenerated]
		internal static Vector256<double> <CreateScalar>g__SoftwareFallback|45_0(double value)
		{
			Vector256<double> zero = Vector256<double>.Zero;
			Unsafe.WriteUnaligned<double>(Unsafe.As<Vector256<double>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003415 RID: 13333 RVA: 0x00170120 File Offset: 0x0016F320
		[CompilerGenerated]
		internal static Vector256<short> <CreateScalar>g__SoftwareFallback|46_0(short value)
		{
			Vector256<short> zero = Vector256<short>.Zero;
			Unsafe.WriteUnaligned<short>(Unsafe.As<Vector256<short>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003416 RID: 13334 RVA: 0x00170144 File Offset: 0x0016F344
		[CompilerGenerated]
		internal static Vector256<int> <CreateScalar>g__SoftwareFallback|47_0(int value)
		{
			Vector256<int> zero = Vector256<int>.Zero;
			Unsafe.WriteUnaligned<int>(Unsafe.As<Vector256<int>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003417 RID: 13335 RVA: 0x00170168 File Offset: 0x0016F368
		[CompilerGenerated]
		internal static Vector256<long> <CreateScalar>g__SoftwareFallback|48_0(long value)
		{
			Vector256<long> zero = Vector256<long>.Zero;
			Unsafe.WriteUnaligned<long>(Unsafe.As<Vector256<long>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003418 RID: 13336 RVA: 0x0017018C File Offset: 0x0016F38C
		[CompilerGenerated]
		internal static Vector256<sbyte> <CreateScalar>g__SoftwareFallback|49_0(sbyte value)
		{
			Vector256<sbyte> zero = Vector256<sbyte>.Zero;
			Unsafe.WriteUnaligned<sbyte>(Unsafe.As<Vector256<sbyte>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003419 RID: 13337 RVA: 0x001701B0 File Offset: 0x0016F3B0
		[CompilerGenerated]
		internal static Vector256<float> <CreateScalar>g__SoftwareFallback|50_0(float value)
		{
			Vector256<float> zero = Vector256<float>.Zero;
			Unsafe.WriteUnaligned<float>(Unsafe.As<Vector256<float>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x0600341A RID: 13338 RVA: 0x001701D4 File Offset: 0x0016F3D4
		[CompilerGenerated]
		internal static Vector256<ushort> <CreateScalar>g__SoftwareFallback|51_0(ushort value)
		{
			Vector256<ushort> zero = Vector256<ushort>.Zero;
			Unsafe.WriteUnaligned<ushort>(Unsafe.As<Vector256<ushort>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x0600341B RID: 13339 RVA: 0x001701F8 File Offset: 0x0016F3F8
		[CompilerGenerated]
		internal static Vector256<uint> <CreateScalar>g__SoftwareFallback|52_0(uint value)
		{
			Vector256<uint> zero = Vector256<uint>.Zero;
			Unsafe.WriteUnaligned<uint>(Unsafe.As<Vector256<uint>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x0600341C RID: 13340 RVA: 0x0017021C File Offset: 0x0016F41C
		[CompilerGenerated]
		internal static Vector256<ulong> <CreateScalar>g__SoftwareFallback|53_0(ulong value)
		{
			Vector256<ulong> zero = Vector256<ulong>.Zero;
			Unsafe.WriteUnaligned<ulong>(Unsafe.As<Vector256<ulong>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x0600341D RID: 13341 RVA: 0x00170240 File Offset: 0x0016F440
		[CompilerGenerated]
		internal unsafe static Vector256<T> <WithLower>g__SoftwareFallback|67_0<T>(Vector256<T> vector, Vector128<T> value) where T : struct
		{
			Vector256<T> result = vector;
			*Unsafe.As<Vector256<T>, Vector128<T>>(ref result) = value;
			return result;
		}

		// Token: 0x0600341E RID: 13342 RVA: 0x00170260 File Offset: 0x0016F460
		[CompilerGenerated]
		internal unsafe static Vector128<T> <GetUpper>g__SoftwareFallback|68_0<T>(Vector256<T> vector) where T : struct
		{
			ref Vector128<T> source = ref Unsafe.As<Vector256<T>, Vector128<T>>(ref vector);
			return *Unsafe.Add<Vector128<T>>(ref source, 1);
		}

		// Token: 0x0600341F RID: 13343 RVA: 0x00170284 File Offset: 0x0016F484
		[CompilerGenerated]
		internal unsafe static Vector256<T> <WithUpper>g__SoftwareFallback|69_0<T>(Vector256<T> vector, Vector128<T> value) where T : struct
		{
			Vector256<T> result = vector;
			ref Vector128<T> source = ref Unsafe.As<Vector256<T>, Vector128<T>>(ref result);
			*Unsafe.Add<Vector128<T>>(ref source, 1) = value;
			return result;
		}
	}
}
