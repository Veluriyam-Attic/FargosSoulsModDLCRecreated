using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics
{
	// Token: 0x02000411 RID: 1041
	public static class Vector128
	{
		// Token: 0x06003322 RID: 13090 RVA: 0x0016D21B File Offset: 0x0016C41B
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static Vector128<U> As<T, U>(this Vector128<T> vector) where T : struct where U : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			ThrowHelper.ThrowForUnsupportedVectorBaseType<U>();
			return *Unsafe.As<Vector128<T>, Vector128<U>>(ref vector);
		}

		// Token: 0x06003323 RID: 13091 RVA: 0x0016D233 File Offset: 0x0016C433
		[Intrinsic]
		public static Vector128<byte> AsByte<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, byte>();
		}

		// Token: 0x06003324 RID: 13092 RVA: 0x0016D23B File Offset: 0x0016C43B
		[Intrinsic]
		public static Vector128<double> AsDouble<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, double>();
		}

		// Token: 0x06003325 RID: 13093 RVA: 0x0016D243 File Offset: 0x0016C443
		[Intrinsic]
		public static Vector128<short> AsInt16<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, short>();
		}

		// Token: 0x06003326 RID: 13094 RVA: 0x0016D24B File Offset: 0x0016C44B
		[Intrinsic]
		public static Vector128<int> AsInt32<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, int>();
		}

		// Token: 0x06003327 RID: 13095 RVA: 0x0016D253 File Offset: 0x0016C453
		[Intrinsic]
		public static Vector128<long> AsInt64<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, long>();
		}

		// Token: 0x06003328 RID: 13096 RVA: 0x0016D25B File Offset: 0x0016C45B
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector128<sbyte> AsSByte<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, sbyte>();
		}

		// Token: 0x06003329 RID: 13097 RVA: 0x0016D263 File Offset: 0x0016C463
		[Intrinsic]
		public static Vector128<float> AsSingle<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, float>();
		}

		// Token: 0x0600332A RID: 13098 RVA: 0x0016D26B File Offset: 0x0016C46B
		[CLSCompliant(false)]
		[Intrinsic]
		public static Vector128<ushort> AsUInt16<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, ushort>();
		}

		// Token: 0x0600332B RID: 13099 RVA: 0x0016D273 File Offset: 0x0016C473
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector128<uint> AsUInt32<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, uint>();
		}

		// Token: 0x0600332C RID: 13100 RVA: 0x0016D27B File Offset: 0x0016C47B
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector128<ulong> AsUInt64<T>(this Vector128<T> vector) where T : struct
		{
			return vector.As<T, ulong>();
		}

		// Token: 0x0600332D RID: 13101 RVA: 0x0016D283 File Offset: 0x0016C483
		public static Vector128<float> AsVector128(this Vector2 value)
		{
			return new Vector4(value, 0f, 0f).AsVector128();
		}

		// Token: 0x0600332E RID: 13102 RVA: 0x0016D29A File Offset: 0x0016C49A
		public static Vector128<float> AsVector128(this Vector3 value)
		{
			return new Vector4(value, 0f).AsVector128();
		}

		// Token: 0x0600332F RID: 13103 RVA: 0x0016D2AC File Offset: 0x0016C4AC
		[Intrinsic]
		public unsafe static Vector128<float> AsVector128(this Vector4 value)
		{
			return *Unsafe.As<Vector4, Vector128<float>>(ref value);
		}

		// Token: 0x06003330 RID: 13104 RVA: 0x0016D2BA File Offset: 0x0016C4BA
		[Intrinsic]
		public unsafe static Vector128<T> AsVector128<T>(this Vector<T> value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return *Unsafe.As<Vector<T>, Vector128<T>>(ref value);
		}

		// Token: 0x06003331 RID: 13105 RVA: 0x0016D2CD File Offset: 0x0016C4CD
		public unsafe static Vector2 AsVector2(this Vector128<float> value)
		{
			return *Unsafe.As<Vector128<float>, Vector2>(ref value);
		}

		// Token: 0x06003332 RID: 13106 RVA: 0x0016D2DB File Offset: 0x0016C4DB
		public unsafe static Vector3 AsVector3(this Vector128<float> value)
		{
			return *Unsafe.As<Vector128<float>, Vector3>(ref value);
		}

		// Token: 0x06003333 RID: 13107 RVA: 0x0016D2E9 File Offset: 0x0016C4E9
		[Intrinsic]
		public unsafe static Vector4 AsVector4(this Vector128<float> value)
		{
			return *Unsafe.As<Vector128<float>, Vector4>(ref value);
		}

		// Token: 0x06003334 RID: 13108 RVA: 0x0016D2F8 File Offset: 0x0016C4F8
		[Intrinsic]
		public static Vector<T> AsVector<T>(this Vector128<T> value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			Vector<T> result = default(Vector<T>);
			Unsafe.WriteUnaligned<Vector128<T>>(Unsafe.As<Vector<T>, byte>(ref result), value);
			return result;
		}

		// Token: 0x06003335 RID: 13109 RVA: 0x0016D320 File Offset: 0x0016C520
		[Intrinsic]
		public static Vector128<byte> Create(byte value)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|20_0(value);
		}

		// Token: 0x06003336 RID: 13110 RVA: 0x0016D33D File Offset: 0x0016C53D
		[Intrinsic]
		public static Vector128<double> Create(double value)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|21_0(value);
		}

		// Token: 0x06003337 RID: 13111 RVA: 0x0016D35A File Offset: 0x0016C55A
		[Intrinsic]
		public static Vector128<short> Create(short value)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|22_0(value);
		}

		// Token: 0x06003338 RID: 13112 RVA: 0x0016D377 File Offset: 0x0016C577
		[Intrinsic]
		public static Vector128<int> Create(int value)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|23_0(value);
		}

		// Token: 0x06003339 RID: 13113 RVA: 0x0016D394 File Offset: 0x0016C594
		[Intrinsic]
		public static Vector128<long> Create(long value)
		{
			if (Sse2.X64.IsSupported || AdvSimd.Arm64.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|24_0(value);
		}

		// Token: 0x0600333A RID: 13114 RVA: 0x0016D3B1 File Offset: 0x0016C5B1
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector128<sbyte> Create(sbyte value)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|25_0(value);
		}

		// Token: 0x0600333B RID: 13115 RVA: 0x0016D3CE File Offset: 0x0016C5CE
		[Intrinsic]
		public static Vector128<float> Create(float value)
		{
			if (Sse.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|26_0(value);
		}

		// Token: 0x0600333C RID: 13116 RVA: 0x0016D3EB File Offset: 0x0016C5EB
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector128<ushort> Create(ushort value)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|27_0(value);
		}

		// Token: 0x0600333D RID: 13117 RVA: 0x0016D408 File Offset: 0x0016C608
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector128<uint> Create(uint value)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|28_0(value);
		}

		// Token: 0x0600333E RID: 13118 RVA: 0x0016D425 File Offset: 0x0016C625
		[CLSCompliant(false)]
		[Intrinsic]
		public static Vector128<ulong> Create(ulong value)
		{
			if (Sse2.X64.IsSupported || AdvSimd.Arm64.IsSupported)
			{
				return Vector128.Create(value);
			}
			return Vector128.<Create>g__SoftwareFallback|29_0(value);
		}

		// Token: 0x0600333F RID: 13119 RVA: 0x0016D444 File Offset: 0x0016C644
		[Intrinsic]
		public static Vector128<byte> Create(byte e0, byte e1, byte e2, byte e3, byte e4, byte e5, byte e6, byte e7, byte e8, byte e9, byte e10, byte e11, byte e12, byte e13, byte e14, byte e15)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15);
			}
			return Vector128.<Create>g__SoftwareFallback|30_0(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15);
		}

		// Token: 0x06003340 RID: 13120 RVA: 0x0016D4A2 File Offset: 0x0016C6A2
		[Intrinsic]
		public static Vector128<double> Create(double e0, double e1)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(e0, e1);
			}
			return Vector128.<Create>g__SoftwareFallback|31_0(e0, e1);
		}

		// Token: 0x06003341 RID: 13121 RVA: 0x0016D4C1 File Offset: 0x0016C6C1
		[Intrinsic]
		public static Vector128<short> Create(short e0, short e1, short e2, short e3, short e4, short e5, short e6, short e7)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(e0, e1, e2, e3, e4, e5, e6, e7);
			}
			return Vector128.<Create>g__SoftwareFallback|32_0(e0, e1, e2, e3, e4, e5, e6, e7);
		}

		// Token: 0x06003342 RID: 13122 RVA: 0x0016D4F4 File Offset: 0x0016C6F4
		[Intrinsic]
		public static Vector128<int> Create(int e0, int e1, int e2, int e3)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(e0, e1, e2, e3);
			}
			return Vector128.<Create>g__SoftwareFallback|33_0(e0, e1, e2, e3);
		}

		// Token: 0x06003343 RID: 13123 RVA: 0x0016D517 File Offset: 0x0016C717
		[Intrinsic]
		public static Vector128<long> Create(long e0, long e1)
		{
			if (Sse2.X64.IsSupported || AdvSimd.Arm64.IsSupported)
			{
				return Vector128.Create(e0, e1);
			}
			return Vector128.<Create>g__SoftwareFallback|34_0(e0, e1);
		}

		// Token: 0x06003344 RID: 13124 RVA: 0x0016D538 File Offset: 0x0016C738
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector128<sbyte> Create(sbyte e0, sbyte e1, sbyte e2, sbyte e3, sbyte e4, sbyte e5, sbyte e6, sbyte e7, sbyte e8, sbyte e9, sbyte e10, sbyte e11, sbyte e12, sbyte e13, sbyte e14, sbyte e15)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15);
			}
			return Vector128.<Create>g__SoftwareFallback|35_0(e0, e1, e2, e3, e4, e5, e6, e7, e8, e9, e10, e11, e12, e13, e14, e15);
		}

		// Token: 0x06003345 RID: 13125 RVA: 0x0016D596 File Offset: 0x0016C796
		[Intrinsic]
		public static Vector128<float> Create(float e0, float e1, float e2, float e3)
		{
			if (Sse.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(e0, e1, e2, e3);
			}
			return Vector128.<Create>g__SoftwareFallback|36_0(e0, e1, e2, e3);
		}

		// Token: 0x06003346 RID: 13126 RVA: 0x0016D5B9 File Offset: 0x0016C7B9
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector128<ushort> Create(ushort e0, ushort e1, ushort e2, ushort e3, ushort e4, ushort e5, ushort e6, ushort e7)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(e0, e1, e2, e3, e4, e5, e6, e7);
			}
			return Vector128.<Create>g__SoftwareFallback|37_0(e0, e1, e2, e3, e4, e5, e6, e7);
		}

		// Token: 0x06003347 RID: 13127 RVA: 0x0016D5EC File Offset: 0x0016C7EC
		[CLSCompliant(false)]
		[Intrinsic]
		public static Vector128<uint> Create(uint e0, uint e1, uint e2, uint e3)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return Vector128.Create(e0, e1, e2, e3);
			}
			return Vector128.<Create>g__SoftwareFallback|38_0(e0, e1, e2, e3);
		}

		// Token: 0x06003348 RID: 13128 RVA: 0x0016D60F File Offset: 0x0016C80F
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector128<ulong> Create(ulong e0, ulong e1)
		{
			if (Sse2.X64.IsSupported || AdvSimd.Arm64.IsSupported)
			{
				return Vector128.Create(e0, e1);
			}
			return Vector128.<Create>g__SoftwareFallback|39_0(e0, e1);
		}

		// Token: 0x06003349 RID: 13129 RVA: 0x0016D62E File Offset: 0x0016C82E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<byte> Create(Vector64<byte> lower, Vector64<byte> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<byte>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|40_0(lower, upper);
		}

		// Token: 0x0600334A RID: 13130 RVA: 0x0016D64B File Offset: 0x0016C84B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<double> Create(Vector64<double> lower, Vector64<double> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<double>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|41_0(lower, upper);
		}

		// Token: 0x0600334B RID: 13131 RVA: 0x0016D668 File Offset: 0x0016C868
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<short> Create(Vector64<short> lower, Vector64<short> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<short>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|42_0(lower, upper);
		}

		// Token: 0x0600334C RID: 13132 RVA: 0x0016D685 File Offset: 0x0016C885
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<int> Create(Vector64<int> lower, Vector64<int> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<int>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|43_0(lower, upper);
		}

		// Token: 0x0600334D RID: 13133 RVA: 0x0016D6A2 File Offset: 0x0016C8A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<long> Create(Vector64<long> lower, Vector64<long> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<long>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|44_0(lower, upper);
		}

		// Token: 0x0600334E RID: 13134 RVA: 0x0016D6BF File Offset: 0x0016C8BF
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<sbyte> Create(Vector64<sbyte> lower, Vector64<sbyte> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<sbyte>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|45_0(lower, upper);
		}

		// Token: 0x0600334F RID: 13135 RVA: 0x0016D6DC File Offset: 0x0016C8DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<float> Create(Vector64<float> lower, Vector64<float> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<float>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|46_0(lower, upper);
		}

		// Token: 0x06003350 RID: 13136 RVA: 0x0016D6F9 File Offset: 0x0016C8F9
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<ushort> Create(Vector64<ushort> lower, Vector64<ushort> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<ushort>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|47_0(lower, upper);
		}

		// Token: 0x06003351 RID: 13137 RVA: 0x0016D716 File Offset: 0x0016C916
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<uint> Create(Vector64<uint> lower, Vector64<uint> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<uint>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|48_0(lower, upper);
		}

		// Token: 0x06003352 RID: 13138 RVA: 0x0016D733 File Offset: 0x0016C933
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<ulong> Create(Vector64<ulong> lower, Vector64<ulong> upper)
		{
			if (AdvSimd.IsSupported)
			{
				return lower.ToVector128Unsafe<ulong>().WithUpper(upper);
			}
			return Vector128.<Create>g__SoftwareFallback|49_0(lower, upper);
		}

		// Token: 0x06003353 RID: 13139 RVA: 0x0016D750 File Offset: 0x0016C950
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<byte> CreateScalar(byte value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<byte>.Zero, 0, value);
			}
			if (Sse2.IsSupported)
			{
				return Sse2.ConvertScalarToVector128UInt32((uint)value).AsByte<uint>();
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|50_0(value);
		}

		// Token: 0x06003354 RID: 13140 RVA: 0x0016D77F File Offset: 0x0016C97F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<double> CreateScalar(double value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<double>.Zero, 0, value);
			}
			if (Sse2.IsSupported)
			{
				return Sse2.MoveScalar(Vector128<double>.Zero, Vector128.CreateScalarUnsafe(value));
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|51_0(value);
		}

		// Token: 0x06003355 RID: 13141 RVA: 0x0016D7B3 File Offset: 0x0016C9B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<short> CreateScalar(short value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<short>.Zero, 0, value);
			}
			if (Sse2.IsSupported)
			{
				return Sse2.ConvertScalarToVector128UInt32((uint)((ushort)value)).AsInt16<uint>();
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|52_0(value);
		}

		// Token: 0x06003356 RID: 13142 RVA: 0x0016D7E3 File Offset: 0x0016C9E3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<int> CreateScalar(int value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<int>.Zero, 0, value);
			}
			if (Sse2.IsSupported)
			{
				return Sse2.ConvertScalarToVector128Int32(value);
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|53_0(value);
		}

		// Token: 0x06003357 RID: 13143 RVA: 0x0016D80D File Offset: 0x0016CA0D
		public static Vector128<long> CreateScalar(long value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<long>.Zero, 0, value);
			}
			if (Sse2.X64.IsSupported)
			{
				return Sse2.X64.ConvertScalarToVector128Int64(value);
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|54_0(value);
		}

		// Token: 0x06003358 RID: 13144 RVA: 0x0016D837 File Offset: 0x0016CA37
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<sbyte> CreateScalar(sbyte value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<sbyte>.Zero, 0, value);
			}
			if (Sse2.IsSupported)
			{
				return Sse2.ConvertScalarToVector128UInt32((uint)((byte)value)).AsSByte<uint>();
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|55_0(value);
		}

		// Token: 0x06003359 RID: 13145 RVA: 0x0016D867 File Offset: 0x0016CA67
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<float> CreateScalar(float value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<float>.Zero, 0, value);
			}
			if (Sse.IsSupported)
			{
				return Sse.MoveScalar(Vector128<float>.Zero, Vector128.CreateScalarUnsafe(value));
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|56_0(value);
		}

		// Token: 0x0600335A RID: 13146 RVA: 0x0016D89B File Offset: 0x0016CA9B
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<ushort> CreateScalar(ushort value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<ushort>.Zero, 0, value);
			}
			if (Sse2.IsSupported)
			{
				return Sse2.ConvertScalarToVector128UInt32((uint)value).AsUInt16<uint>();
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|57_0(value);
		}

		// Token: 0x0600335B RID: 13147 RVA: 0x0016D8CA File Offset: 0x0016CACA
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<uint> CreateScalar(uint value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<uint>.Zero, 0, value);
			}
			if (Sse2.IsSupported)
			{
				return Sse2.ConvertScalarToVector128UInt32(value);
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|58_0(value);
		}

		// Token: 0x0600335C RID: 13148 RVA: 0x0016D8F4 File Offset: 0x0016CAF4
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<ulong> CreateScalar(ulong value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector128<ulong>.Zero, 0, value);
			}
			if (Sse2.X64.IsSupported)
			{
				return Sse2.X64.ConvertScalarToVector128UInt64(value);
			}
			return Vector128.<CreateScalar>g__SoftwareFallback|59_0(value);
		}

		// Token: 0x0600335D RID: 13149 RVA: 0x0016D920 File Offset: 0x0016CB20
		[Intrinsic]
		public unsafe static Vector128<byte> CreateScalarUnsafe(byte value)
		{
			byte* ptr = stackalloc byte[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<byte>>((void*)ptr);
		}

		// Token: 0x0600335E RID: 13150 RVA: 0x0016D944 File Offset: 0x0016CB44
		[Intrinsic]
		public unsafe static Vector128<double> CreateScalarUnsafe(double value)
		{
			double* ptr = stackalloc double[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<double>>((void*)ptr);
		}

		// Token: 0x0600335F RID: 13151 RVA: 0x0016D968 File Offset: 0x0016CB68
		[Intrinsic]
		public unsafe static Vector128<short> CreateScalarUnsafe(short value)
		{
			short* ptr = stackalloc short[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<short>>((void*)ptr);
		}

		// Token: 0x06003360 RID: 13152 RVA: 0x0016D98C File Offset: 0x0016CB8C
		[Intrinsic]
		public unsafe static Vector128<int> CreateScalarUnsafe(int value)
		{
			int* ptr = stackalloc int[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<int>>((void*)ptr);
		}

		// Token: 0x06003361 RID: 13153 RVA: 0x0016D9B0 File Offset: 0x0016CBB0
		[Intrinsic]
		public unsafe static Vector128<long> CreateScalarUnsafe(long value)
		{
			long* ptr = stackalloc long[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<long>>((void*)ptr);
		}

		// Token: 0x06003362 RID: 13154 RVA: 0x0016D9D4 File Offset: 0x0016CBD4
		[Intrinsic]
		[CLSCompliant(false)]
		public unsafe static Vector128<sbyte> CreateScalarUnsafe(sbyte value)
		{
			sbyte* ptr = stackalloc sbyte[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<sbyte>>((void*)ptr);
		}

		// Token: 0x06003363 RID: 13155 RVA: 0x0016D9F8 File Offset: 0x0016CBF8
		[Intrinsic]
		public unsafe static Vector128<float> CreateScalarUnsafe(float value)
		{
			float* ptr = stackalloc float[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<float>>((void*)ptr);
		}

		// Token: 0x06003364 RID: 13156 RVA: 0x0016DA1C File Offset: 0x0016CC1C
		[Intrinsic]
		[CLSCompliant(false)]
		public unsafe static Vector128<ushort> CreateScalarUnsafe(ushort value)
		{
			ushort* ptr = stackalloc ushort[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<ushort>>((void*)ptr);
		}

		// Token: 0x06003365 RID: 13157 RVA: 0x0016DA40 File Offset: 0x0016CC40
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector128<uint> CreateScalarUnsafe(uint value)
		{
			uint* ptr = stackalloc uint[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<uint>>((void*)ptr);
		}

		// Token: 0x06003366 RID: 13158 RVA: 0x0016DA64 File Offset: 0x0016CC64
		[Intrinsic]
		[CLSCompliant(false)]
		public unsafe static Vector128<ulong> CreateScalarUnsafe(ulong value)
		{
			ulong* ptr = stackalloc ulong[(UIntPtr)16];
			*ptr = value;
			return *Unsafe.AsRef<Vector128<ulong>>((void*)ptr);
		}

		// Token: 0x06003367 RID: 13159 RVA: 0x0016DA88 File Offset: 0x0016CC88
		[Intrinsic]
		public unsafe static T GetElement<T>(this Vector128<T> vector, int index) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (index >= Vector128<T>.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			ref T source = ref Unsafe.As<Vector128<T>, T>(ref vector);
			return *Unsafe.Add<T>(ref source, index);
		}

		// Token: 0x06003368 RID: 13160 RVA: 0x0016DAC0 File Offset: 0x0016CCC0
		[Intrinsic]
		public unsafe static Vector128<T> WithElement<T>(this Vector128<T> vector, int index, T value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (index >= Vector128<T>.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			Vector128<T> result = vector;
			ref T source = ref Unsafe.As<Vector128<T>, T>(ref result);
			*Unsafe.Add<T>(ref source, index) = value;
			return result;
		}

		// Token: 0x06003369 RID: 13161 RVA: 0x0016DAF9 File Offset: 0x0016CCF9
		[Intrinsic]
		public unsafe static Vector64<T> GetLower<T>(this Vector128<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return *Unsafe.As<Vector128<T>, Vector64<T>>(ref vector);
		}

		// Token: 0x0600336A RID: 13162 RVA: 0x0016DB0C File Offset: 0x0016CD0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<T> WithLower<T>(this Vector128<T> vector, Vector64<T> value) where T : struct
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.InsertScalar(vector.AsUInt64<T>(), 0, value.AsUInt64<T>()).As<ulong, T>();
			}
			return Vector128.<WithLower>g__SoftwareFallback|73_0<T>(vector, value);
		}

		// Token: 0x0600336B RID: 13163 RVA: 0x0016DB34 File Offset: 0x0016CD34
		[Intrinsic]
		public unsafe static Vector64<T> GetUpper<T>(this Vector128<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			ref Vector64<T> source = ref Unsafe.As<Vector128<T>, Vector64<T>>(ref vector);
			return *Unsafe.Add<Vector64<T>>(ref source, 1);
		}

		// Token: 0x0600336C RID: 13164 RVA: 0x0016DB5A File Offset: 0x0016CD5A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<T> WithUpper<T>(this Vector128<T> vector, Vector64<T> value) where T : struct
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.InsertScalar(vector.AsUInt64<T>(), 1, value.AsUInt64<T>()).As<ulong, T>();
			}
			return Vector128.<WithUpper>g__SoftwareFallback|75_0<T>(vector, value);
		}

		// Token: 0x0600336D RID: 13165 RVA: 0x0016DB82 File Offset: 0x0016CD82
		[Intrinsic]
		public unsafe static T ToScalar<T>(this Vector128<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return *Unsafe.As<Vector128<T>, T>(ref vector);
		}

		// Token: 0x0600336E RID: 13166 RVA: 0x0016DB98 File Offset: 0x0016CD98
		[Intrinsic]
		public unsafe static Vector256<T> ToVector256<T>(this Vector128<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			Vector256<T> zero = Vector256<T>.Zero;
			*Unsafe.As<Vector256<T>, Vector128<T>>(ref zero) = vector;
			return zero;
		}

		// Token: 0x0600336F RID: 13167 RVA: 0x0016DBC0 File Offset: 0x0016CDC0
		[Intrinsic]
		public unsafe static Vector256<T> ToVector256Unsafe<T>(this Vector128<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			byte* source = stackalloc byte[(UIntPtr)32];
			*Unsafe.AsRef<Vector128<T>>((void*)source) = vector;
			return *Unsafe.AsRef<Vector256<T>>((void*)source);
		}

		// Token: 0x06003370 RID: 13168 RVA: 0x0016DBF0 File Offset: 0x0016CDF0
		[CompilerGenerated]
		internal unsafe static Vector128<byte> <Create>g__SoftwareFallback|20_0(byte value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
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
			byte* source = intPtr;
			return *Unsafe.AsRef<Vector128<byte>>((void*)source);
		}

		// Token: 0x06003371 RID: 13169 RVA: 0x0016DC64 File Offset: 0x0016CE64
		[CompilerGenerated]
		internal unsafe static Vector128<double> <Create>g__SoftwareFallback|21_0(double value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = value;
			*(intPtr + 8) = value;
			double* source = intPtr;
			return *Unsafe.AsRef<Vector128<double>>((void*)source);
		}

		// Token: 0x06003372 RID: 13170 RVA: 0x0016DC8C File Offset: 0x0016CE8C
		[CompilerGenerated]
		internal unsafe static Vector128<short> <Create>g__SoftwareFallback|22_0(short value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = value;
			*(intPtr + 2) = value;
			*(intPtr + (IntPtr)2 * 2) = value;
			*(intPtr + (IntPtr)3 * 2) = value;
			*(intPtr + (IntPtr)4 * 2) = value;
			*(intPtr + (IntPtr)5 * 2) = value;
			*(intPtr + (IntPtr)6 * 2) = value;
			*(intPtr + (IntPtr)7 * 2) = value;
			short* source = intPtr;
			return *Unsafe.AsRef<Vector128<short>>((void*)source);
		}

		// Token: 0x06003373 RID: 13171 RVA: 0x0016DCE4 File Offset: 0x0016CEE4
		[CompilerGenerated]
		internal unsafe static Vector128<int> <Create>g__SoftwareFallback|23_0(int value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = value;
			*(intPtr + 4) = value;
			*(intPtr + (IntPtr)2 * 4) = value;
			*(intPtr + (IntPtr)3 * 4) = value;
			int* source = intPtr;
			return *Unsafe.AsRef<Vector128<int>>((void*)source);
		}

		// Token: 0x06003374 RID: 13172 RVA: 0x0016DD1C File Offset: 0x0016CF1C
		[CompilerGenerated]
		internal unsafe static Vector128<long> <Create>g__SoftwareFallback|24_0(long value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = value;
			*(intPtr + 8) = value;
			long* source = intPtr;
			return *Unsafe.AsRef<Vector128<long>>((void*)source);
		}

		// Token: 0x06003375 RID: 13173 RVA: 0x0016DD44 File Offset: 0x0016CF44
		[CompilerGenerated]
		internal unsafe static Vector128<sbyte> <Create>g__SoftwareFallback|25_0(sbyte value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
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
			sbyte* source = intPtr;
			return *Unsafe.AsRef<Vector128<sbyte>>((void*)source);
		}

		// Token: 0x06003376 RID: 13174 RVA: 0x0016DDB8 File Offset: 0x0016CFB8
		[CompilerGenerated]
		internal unsafe static Vector128<float> <Create>g__SoftwareFallback|26_0(float value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = value;
			*(intPtr + 4) = value;
			*(intPtr + (IntPtr)2 * 4) = value;
			*(intPtr + (IntPtr)3 * 4) = value;
			float* source = intPtr;
			return *Unsafe.AsRef<Vector128<float>>((void*)source);
		}

		// Token: 0x06003377 RID: 13175 RVA: 0x0016DDF0 File Offset: 0x0016CFF0
		[CompilerGenerated]
		internal unsafe static Vector128<ushort> <Create>g__SoftwareFallback|27_0(ushort value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = (short)value;
			*(intPtr + 2) = (short)value;
			*(intPtr + (IntPtr)2 * 2) = (short)value;
			*(intPtr + (IntPtr)3 * 2) = (short)value;
			*(intPtr + (IntPtr)4 * 2) = (short)value;
			*(intPtr + (IntPtr)5 * 2) = (short)value;
			*(intPtr + (IntPtr)6 * 2) = (short)value;
			*(intPtr + (IntPtr)7 * 2) = (short)value;
			ushort* source = intPtr;
			return *Unsafe.AsRef<Vector128<ushort>>((void*)source);
		}

		// Token: 0x06003378 RID: 13176 RVA: 0x0016DE48 File Offset: 0x0016D048
		[CompilerGenerated]
		internal unsafe static Vector128<uint> <Create>g__SoftwareFallback|28_0(uint value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = (int)value;
			*(intPtr + 4) = (int)value;
			*(intPtr + (IntPtr)2 * 4) = (int)value;
			*(intPtr + (IntPtr)3 * 4) = (int)value;
			uint* source = intPtr;
			return *Unsafe.AsRef<Vector128<uint>>((void*)source);
		}

		// Token: 0x06003379 RID: 13177 RVA: 0x0016DE80 File Offset: 0x0016D080
		[CompilerGenerated]
		internal unsafe static Vector128<ulong> <Create>g__SoftwareFallback|29_0(ulong value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = (long)value;
			*(intPtr + 8) = (long)value;
			ulong* source = intPtr;
			return *Unsafe.AsRef<Vector128<ulong>>((void*)source);
		}

		// Token: 0x0600337A RID: 13178 RVA: 0x0016DEA8 File Offset: 0x0016D0A8
		[CompilerGenerated]
		internal unsafe static Vector128<byte> <Create>g__SoftwareFallback|30_0(byte e0, byte e1, byte e2, byte e3, byte e4, byte e5, byte e6, byte e7, byte e8, byte e9, byte e10, byte e11, byte e12, byte e13, byte e14, byte e15)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
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
			byte* source = intPtr;
			return *Unsafe.AsRef<Vector128<byte>>((void*)source);
		}

		// Token: 0x0600337B RID: 13179 RVA: 0x0016DF28 File Offset: 0x0016D128
		[CompilerGenerated]
		internal unsafe static Vector128<double> <Create>g__SoftwareFallback|31_0(double e0, double e1)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = e0;
			*(intPtr + 8) = e1;
			double* source = intPtr;
			return *Unsafe.AsRef<Vector128<double>>((void*)source);
		}

		// Token: 0x0600337C RID: 13180 RVA: 0x0016DF50 File Offset: 0x0016D150
		[CompilerGenerated]
		internal unsafe static Vector128<short> <Create>g__SoftwareFallback|32_0(short e0, short e1, short e2, short e3, short e4, short e5, short e6, short e7)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = e0;
			*(intPtr + 2) = e1;
			*(intPtr + (IntPtr)2 * 2) = e2;
			*(intPtr + (IntPtr)3 * 2) = e3;
			*(intPtr + (IntPtr)4 * 2) = e4;
			*(intPtr + (IntPtr)5 * 2) = e5;
			*(intPtr + (IntPtr)6 * 2) = e6;
			*(intPtr + (IntPtr)7 * 2) = e7;
			short* source = intPtr;
			return *Unsafe.AsRef<Vector128<short>>((void*)source);
		}

		// Token: 0x0600337D RID: 13181 RVA: 0x0016DFAC File Offset: 0x0016D1AC
		[CompilerGenerated]
		internal unsafe static Vector128<int> <Create>g__SoftwareFallback|33_0(int e0, int e1, int e2, int e3)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = e0;
			*(intPtr + 4) = e1;
			*(intPtr + (IntPtr)2 * 4) = e2;
			*(intPtr + (IntPtr)3 * 4) = e3;
			int* source = intPtr;
			return *Unsafe.AsRef<Vector128<int>>((void*)source);
		}

		// Token: 0x0600337E RID: 13182 RVA: 0x0016DFE4 File Offset: 0x0016D1E4
		[CompilerGenerated]
		internal unsafe static Vector128<long> <Create>g__SoftwareFallback|34_0(long e0, long e1)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = e0;
			*(intPtr + 8) = e1;
			long* source = intPtr;
			return *Unsafe.AsRef<Vector128<long>>((void*)source);
		}

		// Token: 0x0600337F RID: 13183 RVA: 0x0016E00C File Offset: 0x0016D20C
		[CompilerGenerated]
		internal unsafe static Vector128<sbyte> <Create>g__SoftwareFallback|35_0(sbyte e0, sbyte e1, sbyte e2, sbyte e3, sbyte e4, sbyte e5, sbyte e6, sbyte e7, sbyte e8, sbyte e9, sbyte e10, sbyte e11, sbyte e12, sbyte e13, sbyte e14, sbyte e15)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
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
			sbyte* source = intPtr;
			return *Unsafe.AsRef<Vector128<sbyte>>((void*)source);
		}

		// Token: 0x06003380 RID: 13184 RVA: 0x0016E08C File Offset: 0x0016D28C
		[CompilerGenerated]
		internal unsafe static Vector128<float> <Create>g__SoftwareFallback|36_0(float e0, float e1, float e2, float e3)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = e0;
			*(intPtr + 4) = e1;
			*(intPtr + (IntPtr)2 * 4) = e2;
			*(intPtr + (IntPtr)3 * 4) = e3;
			float* source = intPtr;
			return *Unsafe.AsRef<Vector128<float>>((void*)source);
		}

		// Token: 0x06003381 RID: 13185 RVA: 0x0016E0C4 File Offset: 0x0016D2C4
		[CompilerGenerated]
		internal unsafe static Vector128<ushort> <Create>g__SoftwareFallback|37_0(ushort e0, ushort e1, ushort e2, ushort e3, ushort e4, ushort e5, ushort e6, ushort e7)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = (short)e0;
			*(intPtr + 2) = (short)e1;
			*(intPtr + (IntPtr)2 * 2) = (short)e2;
			*(intPtr + (IntPtr)3 * 2) = (short)e3;
			*(intPtr + (IntPtr)4 * 2) = (short)e4;
			*(intPtr + (IntPtr)5 * 2) = (short)e5;
			*(intPtr + (IntPtr)6 * 2) = (short)e6;
			*(intPtr + (IntPtr)7 * 2) = (short)e7;
			ushort* source = intPtr;
			return *Unsafe.AsRef<Vector128<ushort>>((void*)source);
		}

		// Token: 0x06003382 RID: 13186 RVA: 0x0016E120 File Offset: 0x0016D320
		[CompilerGenerated]
		internal unsafe static Vector128<uint> <Create>g__SoftwareFallback|38_0(uint e0, uint e1, uint e2, uint e3)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = (int)e0;
			*(intPtr + 4) = (int)e1;
			*(intPtr + (IntPtr)2 * 4) = (int)e2;
			*(intPtr + (IntPtr)3 * 4) = (int)e3;
			uint* source = intPtr;
			return *Unsafe.AsRef<Vector128<uint>>((void*)source);
		}

		// Token: 0x06003383 RID: 13187 RVA: 0x0016E158 File Offset: 0x0016D358
		[CompilerGenerated]
		internal unsafe static Vector128<ulong> <Create>g__SoftwareFallback|39_0(ulong e0, ulong e1)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)16];
			*intPtr = (long)e0;
			*(intPtr + 8) = (long)e1;
			ulong* source = intPtr;
			return *Unsafe.AsRef<Vector128<ulong>>((void*)source);
		}

		// Token: 0x06003384 RID: 13188 RVA: 0x0016E180 File Offset: 0x0016D380
		[CompilerGenerated]
		internal unsafe static Vector128<byte> <Create>g__SoftwareFallback|40_0(Vector64<byte> lower, Vector64<byte> upper)
		{
			Vector128<byte> zero = Vector128<byte>.Zero;
			ref Vector64<byte> ptr = ref Unsafe.As<Vector128<byte>, Vector64<byte>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<byte>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x06003385 RID: 13189 RVA: 0x0016E1B0 File Offset: 0x0016D3B0
		[CompilerGenerated]
		internal unsafe static Vector128<double> <Create>g__SoftwareFallback|41_0(Vector64<double> lower, Vector64<double> upper)
		{
			Vector128<double> zero = Vector128<double>.Zero;
			ref Vector64<double> ptr = ref Unsafe.As<Vector128<double>, Vector64<double>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<double>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x06003386 RID: 13190 RVA: 0x0016E1E0 File Offset: 0x0016D3E0
		[CompilerGenerated]
		internal unsafe static Vector128<short> <Create>g__SoftwareFallback|42_0(Vector64<short> lower, Vector64<short> upper)
		{
			Vector128<short> zero = Vector128<short>.Zero;
			ref Vector64<short> ptr = ref Unsafe.As<Vector128<short>, Vector64<short>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<short>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x06003387 RID: 13191 RVA: 0x0016E210 File Offset: 0x0016D410
		[CompilerGenerated]
		internal unsafe static Vector128<int> <Create>g__SoftwareFallback|43_0(Vector64<int> lower, Vector64<int> upper)
		{
			Vector128<int> zero = Vector128<int>.Zero;
			ref Vector64<int> ptr = ref Unsafe.As<Vector128<int>, Vector64<int>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<int>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x06003388 RID: 13192 RVA: 0x0016E240 File Offset: 0x0016D440
		[CompilerGenerated]
		internal unsafe static Vector128<long> <Create>g__SoftwareFallback|44_0(Vector64<long> lower, Vector64<long> upper)
		{
			Vector128<long> zero = Vector128<long>.Zero;
			ref Vector64<long> ptr = ref Unsafe.As<Vector128<long>, Vector64<long>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<long>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x06003389 RID: 13193 RVA: 0x0016E270 File Offset: 0x0016D470
		[CompilerGenerated]
		internal unsafe static Vector128<sbyte> <Create>g__SoftwareFallback|45_0(Vector64<sbyte> lower, Vector64<sbyte> upper)
		{
			Vector128<sbyte> zero = Vector128<sbyte>.Zero;
			ref Vector64<sbyte> ptr = ref Unsafe.As<Vector128<sbyte>, Vector64<sbyte>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<sbyte>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600338A RID: 13194 RVA: 0x0016E2A0 File Offset: 0x0016D4A0
		[CompilerGenerated]
		internal unsafe static Vector128<float> <Create>g__SoftwareFallback|46_0(Vector64<float> lower, Vector64<float> upper)
		{
			Vector128<float> zero = Vector128<float>.Zero;
			ref Vector64<float> ptr = ref Unsafe.As<Vector128<float>, Vector64<float>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<float>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600338B RID: 13195 RVA: 0x0016E2D0 File Offset: 0x0016D4D0
		[CompilerGenerated]
		internal unsafe static Vector128<ushort> <Create>g__SoftwareFallback|47_0(Vector64<ushort> lower, Vector64<ushort> upper)
		{
			Vector128<ushort> zero = Vector128<ushort>.Zero;
			ref Vector64<ushort> ptr = ref Unsafe.As<Vector128<ushort>, Vector64<ushort>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<ushort>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600338C RID: 13196 RVA: 0x0016E300 File Offset: 0x0016D500
		[CompilerGenerated]
		internal unsafe static Vector128<uint> <Create>g__SoftwareFallback|48_0(Vector64<uint> lower, Vector64<uint> upper)
		{
			Vector128<uint> zero = Vector128<uint>.Zero;
			ref Vector64<uint> ptr = ref Unsafe.As<Vector128<uint>, Vector64<uint>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<uint>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600338D RID: 13197 RVA: 0x0016E330 File Offset: 0x0016D530
		[CompilerGenerated]
		internal unsafe static Vector128<ulong> <Create>g__SoftwareFallback|49_0(Vector64<ulong> lower, Vector64<ulong> upper)
		{
			Vector128<ulong> zero = Vector128<ulong>.Zero;
			ref Vector64<ulong> ptr = ref Unsafe.As<Vector128<ulong>, Vector64<ulong>>(ref zero);
			ptr = lower;
			*Unsafe.Add<Vector64<ulong>>(ref ptr, 1) = upper;
			return zero;
		}

		// Token: 0x0600338E RID: 13198 RVA: 0x0016E360 File Offset: 0x0016D560
		[CompilerGenerated]
		internal static Vector128<byte> <CreateScalar>g__SoftwareFallback|50_0(byte value)
		{
			Vector128<byte> zero = Vector128<byte>.Zero;
			Unsafe.WriteUnaligned<byte>(Unsafe.As<Vector128<byte>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x0600338F RID: 13199 RVA: 0x0016E384 File Offset: 0x0016D584
		[CompilerGenerated]
		internal static Vector128<double> <CreateScalar>g__SoftwareFallback|51_0(double value)
		{
			Vector128<double> zero = Vector128<double>.Zero;
			Unsafe.WriteUnaligned<double>(Unsafe.As<Vector128<double>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003390 RID: 13200 RVA: 0x0016E3A8 File Offset: 0x0016D5A8
		[CompilerGenerated]
		internal static Vector128<short> <CreateScalar>g__SoftwareFallback|52_0(short value)
		{
			Vector128<short> zero = Vector128<short>.Zero;
			Unsafe.WriteUnaligned<short>(Unsafe.As<Vector128<short>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003391 RID: 13201 RVA: 0x0016E3CC File Offset: 0x0016D5CC
		[CompilerGenerated]
		internal static Vector128<int> <CreateScalar>g__SoftwareFallback|53_0(int value)
		{
			Vector128<int> zero = Vector128<int>.Zero;
			Unsafe.WriteUnaligned<int>(Unsafe.As<Vector128<int>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003392 RID: 13202 RVA: 0x0016E3F0 File Offset: 0x0016D5F0
		[CompilerGenerated]
		internal static Vector128<long> <CreateScalar>g__SoftwareFallback|54_0(long value)
		{
			Vector128<long> zero = Vector128<long>.Zero;
			Unsafe.WriteUnaligned<long>(Unsafe.As<Vector128<long>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003393 RID: 13203 RVA: 0x0016E414 File Offset: 0x0016D614
		[CompilerGenerated]
		internal static Vector128<sbyte> <CreateScalar>g__SoftwareFallback|55_0(sbyte value)
		{
			Vector128<sbyte> zero = Vector128<sbyte>.Zero;
			Unsafe.WriteUnaligned<sbyte>(Unsafe.As<Vector128<sbyte>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003394 RID: 13204 RVA: 0x0016E438 File Offset: 0x0016D638
		[CompilerGenerated]
		internal static Vector128<float> <CreateScalar>g__SoftwareFallback|56_0(float value)
		{
			Vector128<float> zero = Vector128<float>.Zero;
			Unsafe.WriteUnaligned<float>(Unsafe.As<Vector128<float>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003395 RID: 13205 RVA: 0x0016E45C File Offset: 0x0016D65C
		[CompilerGenerated]
		internal static Vector128<ushort> <CreateScalar>g__SoftwareFallback|57_0(ushort value)
		{
			Vector128<ushort> zero = Vector128<ushort>.Zero;
			Unsafe.WriteUnaligned<ushort>(Unsafe.As<Vector128<ushort>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003396 RID: 13206 RVA: 0x0016E480 File Offset: 0x0016D680
		[CompilerGenerated]
		internal static Vector128<uint> <CreateScalar>g__SoftwareFallback|58_0(uint value)
		{
			Vector128<uint> zero = Vector128<uint>.Zero;
			Unsafe.WriteUnaligned<uint>(Unsafe.As<Vector128<uint>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003397 RID: 13207 RVA: 0x0016E4A4 File Offset: 0x0016D6A4
		[CompilerGenerated]
		internal static Vector128<ulong> <CreateScalar>g__SoftwareFallback|59_0(ulong value)
		{
			Vector128<ulong> zero = Vector128<ulong>.Zero;
			Unsafe.WriteUnaligned<ulong>(Unsafe.As<Vector128<ulong>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003398 RID: 13208 RVA: 0x0016E4C8 File Offset: 0x0016D6C8
		[CompilerGenerated]
		internal unsafe static Vector128<T> <WithLower>g__SoftwareFallback|73_0<T>(Vector128<T> vector, Vector64<T> value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			Vector128<T> result = vector;
			*Unsafe.As<Vector128<T>, Vector64<T>>(ref result) = value;
			return result;
		}

		// Token: 0x06003399 RID: 13209 RVA: 0x0016E4EC File Offset: 0x0016D6EC
		[CompilerGenerated]
		internal unsafe static Vector128<T> <WithUpper>g__SoftwareFallback|75_0<T>(Vector128<T> vector, Vector64<T> value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			Vector128<T> result = vector;
			ref Vector64<T> source = ref Unsafe.As<Vector128<T>, Vector64<T>>(ref result);
			*Unsafe.Add<Vector64<T>>(ref source, 1) = value;
			return result;
		}
	}
}
