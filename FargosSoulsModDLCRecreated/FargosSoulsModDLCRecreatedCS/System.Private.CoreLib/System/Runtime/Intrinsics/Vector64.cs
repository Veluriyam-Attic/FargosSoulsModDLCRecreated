using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics.Arm;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics
{
	// Token: 0x02000417 RID: 1047
	public static class Vector64
	{
		// Token: 0x06003435 RID: 13365 RVA: 0x0017070C File Offset: 0x0016F90C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static Vector64<U> As<T, U>(this Vector64<T> vector) where T : struct where U : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			ThrowHelper.ThrowForUnsupportedVectorBaseType<U>();
			return *Unsafe.As<Vector64<T>, Vector64<U>>(ref vector);
		}

		// Token: 0x06003436 RID: 13366 RVA: 0x00170724 File Offset: 0x0016F924
		[Intrinsic]
		public static Vector64<byte> AsByte<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, byte>();
		}

		// Token: 0x06003437 RID: 13367 RVA: 0x0017072C File Offset: 0x0016F92C
		[Intrinsic]
		public static Vector64<double> AsDouble<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, double>();
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x00170734 File Offset: 0x0016F934
		[Intrinsic]
		public static Vector64<short> AsInt16<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, short>();
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x0017073C File Offset: 0x0016F93C
		[Intrinsic]
		public static Vector64<int> AsInt32<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, int>();
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x00170744 File Offset: 0x0016F944
		[Intrinsic]
		public static Vector64<long> AsInt64<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, long>();
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x0017074C File Offset: 0x0016F94C
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector64<sbyte> AsSByte<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, sbyte>();
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x00170754 File Offset: 0x0016F954
		[Intrinsic]
		public static Vector64<float> AsSingle<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, float>();
		}

		// Token: 0x0600343D RID: 13373 RVA: 0x0017075C File Offset: 0x0016F95C
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector64<ushort> AsUInt16<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, ushort>();
		}

		// Token: 0x0600343E RID: 13374 RVA: 0x00170764 File Offset: 0x0016F964
		[CLSCompliant(false)]
		[Intrinsic]
		public static Vector64<uint> AsUInt32<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, uint>();
		}

		// Token: 0x0600343F RID: 13375 RVA: 0x0017076C File Offset: 0x0016F96C
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector64<ulong> AsUInt64<T>(this Vector64<T> vector) where T : struct
		{
			return vector.As<T, ulong>();
		}

		// Token: 0x06003440 RID: 13376 RVA: 0x00170774 File Offset: 0x0016F974
		[Intrinsic]
		public static Vector64<byte> Create(byte value)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|12_0(value);
		}

		// Token: 0x06003441 RID: 13377 RVA: 0x0017078A File Offset: 0x0016F98A
		[Intrinsic]
		public static Vector64<double> Create(double value)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|13_0(value);
		}

		// Token: 0x06003442 RID: 13378 RVA: 0x001707A0 File Offset: 0x0016F9A0
		[Intrinsic]
		public static Vector64<short> Create(short value)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|14_0(value);
		}

		// Token: 0x06003443 RID: 13379 RVA: 0x001707B6 File Offset: 0x0016F9B6
		[Intrinsic]
		public static Vector64<int> Create(int value)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|15_0(value);
		}

		// Token: 0x06003444 RID: 13380 RVA: 0x001707CC File Offset: 0x0016F9CC
		[Intrinsic]
		public static Vector64<long> Create(long value)
		{
			if (AdvSimd.Arm64.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|16_0(value);
		}

		// Token: 0x06003445 RID: 13381 RVA: 0x001707E2 File Offset: 0x0016F9E2
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector64<sbyte> Create(sbyte value)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|17_0(value);
		}

		// Token: 0x06003446 RID: 13382 RVA: 0x001707F8 File Offset: 0x0016F9F8
		[Intrinsic]
		public static Vector64<float> Create(float value)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|18_0(value);
		}

		// Token: 0x06003447 RID: 13383 RVA: 0x0017080E File Offset: 0x0016FA0E
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector64<ushort> Create(ushort value)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|19_0(value);
		}

		// Token: 0x06003448 RID: 13384 RVA: 0x00170824 File Offset: 0x0016FA24
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector64<uint> Create(uint value)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|20_0(value);
		}

		// Token: 0x06003449 RID: 13385 RVA: 0x0017083A File Offset: 0x0016FA3A
		[CLSCompliant(false)]
		[Intrinsic]
		public static Vector64<ulong> Create(ulong value)
		{
			if (AdvSimd.Arm64.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<Create>g__SoftwareFallback|21_0(value);
		}

		// Token: 0x0600344A RID: 13386 RVA: 0x00170850 File Offset: 0x0016FA50
		[Intrinsic]
		public static Vector64<byte> Create(byte e0, byte e1, byte e2, byte e3, byte e4, byte e5, byte e6, byte e7)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(e0, e1, e2, e3, e4, e5, e6, e7);
			}
			return Vector64.<Create>g__SoftwareFallback|22_0(e0, e1, e2, e3, e4, e5, e6, e7);
		}

		// Token: 0x0600344B RID: 13387 RVA: 0x0017087C File Offset: 0x0016FA7C
		[Intrinsic]
		public static Vector64<short> Create(short e0, short e1, short e2, short e3)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(e0, e1, e2, e3);
			}
			return Vector64.<Create>g__SoftwareFallback|23_0(e0, e1, e2, e3);
		}

		// Token: 0x0600344C RID: 13388 RVA: 0x00170898 File Offset: 0x0016FA98
		[Intrinsic]
		public static Vector64<int> Create(int e0, int e1)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(e0, e1);
			}
			return Vector64.<Create>g__SoftwareFallback|24_0(e0, e1);
		}

		// Token: 0x0600344D RID: 13389 RVA: 0x001708B0 File Offset: 0x0016FAB0
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector64<sbyte> Create(sbyte e0, sbyte e1, sbyte e2, sbyte e3, sbyte e4, sbyte e5, sbyte e6, sbyte e7)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(e0, e1, e2, e3, e4, e5, e6, e7);
			}
			return Vector64.<Create>g__SoftwareFallback|25_0(e0, e1, e2, e3, e4, e5, e6, e7);
		}

		// Token: 0x0600344E RID: 13390 RVA: 0x001708DC File Offset: 0x0016FADC
		[Intrinsic]
		public static Vector64<float> Create(float e0, float e1)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(e0, e1);
			}
			return Vector64.<Create>g__SoftwareFallback|26_0(e0, e1);
		}

		// Token: 0x0600344F RID: 13391 RVA: 0x001708F4 File Offset: 0x0016FAF4
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector64<ushort> Create(ushort e0, ushort e1, ushort e2, ushort e3)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(e0, e1, e2, e3);
			}
			return Vector64.<Create>g__SoftwareFallback|27_0(e0, e1, e2, e3);
		}

		// Token: 0x06003450 RID: 13392 RVA: 0x00170910 File Offset: 0x0016FB10
		[Intrinsic]
		[CLSCompliant(false)]
		public static Vector64<uint> Create(uint e0, uint e1)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(e0, e1);
			}
			return Vector64.<Create>g__SoftwareFallback|28_0(e0, e1);
		}

		// Token: 0x06003451 RID: 13393 RVA: 0x00170928 File Offset: 0x0016FB28
		public static Vector64<byte> CreateScalar(byte value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector64<byte>.Zero, 0, value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|29_0(value);
		}

		// Token: 0x06003452 RID: 13394 RVA: 0x00170944 File Offset: 0x0016FB44
		public static Vector64<double> CreateScalar(double value)
		{
			if (AdvSimd.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|30_0(value);
		}

		// Token: 0x06003453 RID: 13395 RVA: 0x0017095A File Offset: 0x0016FB5A
		public static Vector64<short> CreateScalar(short value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector64<short>.Zero, 0, value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|31_0(value);
		}

		// Token: 0x06003454 RID: 13396 RVA: 0x00170976 File Offset: 0x0016FB76
		public static Vector64<int> CreateScalar(int value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector64<int>.Zero, 0, value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|32_0(value);
		}

		// Token: 0x06003455 RID: 13397 RVA: 0x00170992 File Offset: 0x0016FB92
		public static Vector64<long> CreateScalar(long value)
		{
			if (AdvSimd.Arm64.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|33_0(value);
		}

		// Token: 0x06003456 RID: 13398 RVA: 0x001709A8 File Offset: 0x0016FBA8
		[CLSCompliant(false)]
		public static Vector64<sbyte> CreateScalar(sbyte value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector64<sbyte>.Zero, 0, value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|34_0(value);
		}

		// Token: 0x06003457 RID: 13399 RVA: 0x001709C4 File Offset: 0x0016FBC4
		public static Vector64<float> CreateScalar(float value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector64<float>.Zero, 0, value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|35_0(value);
		}

		// Token: 0x06003458 RID: 13400 RVA: 0x001709E0 File Offset: 0x0016FBE0
		[CLSCompliant(false)]
		public static Vector64<ushort> CreateScalar(ushort value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector64<ushort>.Zero, 0, value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|36_0(value);
		}

		// Token: 0x06003459 RID: 13401 RVA: 0x001709FC File Offset: 0x0016FBFC
		[CLSCompliant(false)]
		public static Vector64<uint> CreateScalar(uint value)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.Insert(Vector64<uint>.Zero, 0, value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|37_0(value);
		}

		// Token: 0x0600345A RID: 13402 RVA: 0x00170A18 File Offset: 0x0016FC18
		[CLSCompliant(false)]
		public static Vector64<ulong> CreateScalar(ulong value)
		{
			if (AdvSimd.Arm64.IsSupported)
			{
				return Vector64.Create(value);
			}
			return Vector64.<CreateScalar>g__SoftwareFallback|38_0(value);
		}

		// Token: 0x0600345B RID: 13403 RVA: 0x00170A30 File Offset: 0x0016FC30
		[Intrinsic]
		public unsafe static Vector64<byte> CreateScalarUnsafe(byte value)
		{
			byte* ptr = stackalloc byte[(UIntPtr)8];
			*ptr = value;
			return *Unsafe.AsRef<Vector64<byte>>((void*)ptr);
		}

		// Token: 0x0600345C RID: 13404 RVA: 0x00170A50 File Offset: 0x0016FC50
		[Intrinsic]
		public unsafe static Vector64<short> CreateScalarUnsafe(short value)
		{
			short* ptr = stackalloc short[(UIntPtr)8];
			*ptr = value;
			return *Unsafe.AsRef<Vector64<short>>((void*)ptr);
		}

		// Token: 0x0600345D RID: 13405 RVA: 0x00170A70 File Offset: 0x0016FC70
		[Intrinsic]
		public unsafe static Vector64<int> CreateScalarUnsafe(int value)
		{
			int* ptr = stackalloc int[(UIntPtr)8];
			*ptr = value;
			return *Unsafe.AsRef<Vector64<int>>((void*)ptr);
		}

		// Token: 0x0600345E RID: 13406 RVA: 0x00170A90 File Offset: 0x0016FC90
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector64<sbyte> CreateScalarUnsafe(sbyte value)
		{
			sbyte* ptr = stackalloc sbyte[(UIntPtr)8];
			*ptr = value;
			return *Unsafe.AsRef<Vector64<sbyte>>((void*)ptr);
		}

		// Token: 0x0600345F RID: 13407 RVA: 0x00170AB0 File Offset: 0x0016FCB0
		[Intrinsic]
		public unsafe static Vector64<float> CreateScalarUnsafe(float value)
		{
			float* ptr = stackalloc float[(UIntPtr)8];
			*ptr = value;
			return *Unsafe.AsRef<Vector64<float>>((void*)ptr);
		}

		// Token: 0x06003460 RID: 13408 RVA: 0x00170AD0 File Offset: 0x0016FCD0
		[Intrinsic]
		[CLSCompliant(false)]
		public unsafe static Vector64<ushort> CreateScalarUnsafe(ushort value)
		{
			ushort* ptr = stackalloc ushort[(UIntPtr)8];
			*ptr = value;
			return *Unsafe.AsRef<Vector64<ushort>>((void*)ptr);
		}

		// Token: 0x06003461 RID: 13409 RVA: 0x00170AF0 File Offset: 0x0016FCF0
		[Intrinsic]
		[CLSCompliant(false)]
		public unsafe static Vector64<uint> CreateScalarUnsafe(uint value)
		{
			uint* ptr = stackalloc uint[(UIntPtr)8];
			*ptr = value;
			return *Unsafe.AsRef<Vector64<uint>>((void*)ptr);
		}

		// Token: 0x06003462 RID: 13410 RVA: 0x00170B10 File Offset: 0x0016FD10
		[Intrinsic]
		public unsafe static T GetElement<T>(this Vector64<T> vector, int index) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (index >= Vector64<T>.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			ref T source = ref Unsafe.As<Vector64<T>, T>(ref vector);
			return *Unsafe.Add<T>(ref source, index);
		}

		// Token: 0x06003463 RID: 13411 RVA: 0x00170B48 File Offset: 0x0016FD48
		[Intrinsic]
		public unsafe static Vector64<T> WithElement<T>(this Vector64<T> vector, int index, T value) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			if (index >= Vector64<T>.Count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.index);
			}
			Vector64<T> result = vector;
			ref T source = ref Unsafe.As<Vector64<T>, T>(ref result);
			*Unsafe.Add<T>(ref source, index) = value;
			return result;
		}

		// Token: 0x06003464 RID: 13412 RVA: 0x00170B81 File Offset: 0x0016FD81
		[Intrinsic]
		public unsafe static T ToScalar<T>(this Vector64<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			return *Unsafe.As<Vector64<T>, T>(ref vector);
		}

		// Token: 0x06003465 RID: 13413 RVA: 0x00170B94 File Offset: 0x0016FD94
		[Intrinsic]
		public unsafe static Vector128<T> ToVector128<T>(this Vector64<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			Vector128<T> zero = Vector128<T>.Zero;
			*Unsafe.As<Vector128<T>, Vector64<T>>(ref zero) = vector;
			return zero;
		}

		// Token: 0x06003466 RID: 13414 RVA: 0x00170BBC File Offset: 0x0016FDBC
		[Intrinsic]
		public unsafe static Vector128<T> ToVector128Unsafe<T>(this Vector64<T> vector) where T : struct
		{
			ThrowHelper.ThrowForUnsupportedVectorBaseType<T>();
			byte* source = stackalloc byte[(UIntPtr)16];
			*Unsafe.AsRef<Vector64<T>>((void*)source) = vector;
			return *Unsafe.AsRef<Vector128<T>>((void*)source);
		}

		// Token: 0x06003467 RID: 13415 RVA: 0x00170BEC File Offset: 0x0016FDEC
		[CompilerGenerated]
		internal unsafe static Vector64<byte> <Create>g__SoftwareFallback|12_0(byte value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = value;
			*(intPtr + 1) = value;
			*(intPtr + 2) = value;
			*(intPtr + 3) = value;
			*(intPtr + 4) = value;
			*(intPtr + 5) = value;
			*(intPtr + 6) = value;
			*(intPtr + 7) = value;
			byte* source = intPtr;
			return *Unsafe.AsRef<Vector64<byte>>((void*)source);
		}

		// Token: 0x06003468 RID: 13416 RVA: 0x00170C2F File Offset: 0x0016FE2F
		[CompilerGenerated]
		internal unsafe static Vector64<double> <Create>g__SoftwareFallback|13_0(double value)
		{
			return *Unsafe.As<double, Vector64<double>>(ref value);
		}

		// Token: 0x06003469 RID: 13417 RVA: 0x00170C40 File Offset: 0x0016FE40
		[CompilerGenerated]
		internal unsafe static Vector64<short> <Create>g__SoftwareFallback|14_0(short value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = value;
			*(intPtr + 2) = value;
			*(intPtr + (IntPtr)2 * 2) = value;
			*(intPtr + (IntPtr)3 * 2) = value;
			short* source = intPtr;
			return *Unsafe.AsRef<Vector64<short>>((void*)source);
		}

		// Token: 0x0600346A RID: 13418 RVA: 0x00170C78 File Offset: 0x0016FE78
		[CompilerGenerated]
		internal unsafe static Vector64<int> <Create>g__SoftwareFallback|15_0(int value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = value;
			*(intPtr + 4) = value;
			int* source = intPtr;
			return *Unsafe.AsRef<Vector64<int>>((void*)source);
		}

		// Token: 0x0600346B RID: 13419 RVA: 0x00170C9D File Offset: 0x0016FE9D
		[CompilerGenerated]
		internal unsafe static Vector64<long> <Create>g__SoftwareFallback|16_0(long value)
		{
			return *Unsafe.As<long, Vector64<long>>(ref value);
		}

		// Token: 0x0600346C RID: 13420 RVA: 0x00170CAC File Offset: 0x0016FEAC
		[CompilerGenerated]
		internal unsafe static Vector64<sbyte> <Create>g__SoftwareFallback|17_0(sbyte value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = (byte)value;
			*(intPtr + 1) = (byte)value;
			*(intPtr + 2) = (byte)value;
			*(intPtr + 3) = (byte)value;
			*(intPtr + 4) = (byte)value;
			*(intPtr + 5) = (byte)value;
			*(intPtr + 6) = (byte)value;
			*(intPtr + 7) = (byte)value;
			sbyte* source = intPtr;
			return *Unsafe.AsRef<Vector64<sbyte>>((void*)source);
		}

		// Token: 0x0600346D RID: 13421 RVA: 0x00170CF0 File Offset: 0x0016FEF0
		[CompilerGenerated]
		internal unsafe static Vector64<float> <Create>g__SoftwareFallback|18_0(float value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = value;
			*(intPtr + 4) = value;
			float* source = intPtr;
			return *Unsafe.AsRef<Vector64<float>>((void*)source);
		}

		// Token: 0x0600346E RID: 13422 RVA: 0x00170D18 File Offset: 0x0016FF18
		[CompilerGenerated]
		internal unsafe static Vector64<ushort> <Create>g__SoftwareFallback|19_0(ushort value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = (short)value;
			*(intPtr + 2) = (short)value;
			*(intPtr + (IntPtr)2 * 2) = (short)value;
			*(intPtr + (IntPtr)3 * 2) = (short)value;
			ushort* source = intPtr;
			return *Unsafe.AsRef<Vector64<ushort>>((void*)source);
		}

		// Token: 0x0600346F RID: 13423 RVA: 0x00170D50 File Offset: 0x0016FF50
		[CompilerGenerated]
		internal unsafe static Vector64<uint> <Create>g__SoftwareFallback|20_0(uint value)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = (int)value;
			*(intPtr + 4) = (int)value;
			uint* source = intPtr;
			return *Unsafe.AsRef<Vector64<uint>>((void*)source);
		}

		// Token: 0x06003470 RID: 13424 RVA: 0x00170D75 File Offset: 0x0016FF75
		[CompilerGenerated]
		internal unsafe static Vector64<ulong> <Create>g__SoftwareFallback|21_0(ulong value)
		{
			return *Unsafe.As<ulong, Vector64<ulong>>(ref value);
		}

		// Token: 0x06003471 RID: 13425 RVA: 0x00170D84 File Offset: 0x0016FF84
		[CompilerGenerated]
		internal unsafe static Vector64<byte> <Create>g__SoftwareFallback|22_0(byte e0, byte e1, byte e2, byte e3, byte e4, byte e5, byte e6, byte e7)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = e0;
			*(intPtr + 1) = e1;
			*(intPtr + 2) = e2;
			*(intPtr + 3) = e3;
			*(intPtr + 4) = e4;
			*(intPtr + 5) = e5;
			*(intPtr + 6) = e6;
			*(intPtr + 7) = e7;
			byte* source = intPtr;
			return *Unsafe.AsRef<Vector64<byte>>((void*)source);
		}

		// Token: 0x06003472 RID: 13426 RVA: 0x00170DCC File Offset: 0x0016FFCC
		[CompilerGenerated]
		internal unsafe static Vector64<short> <Create>g__SoftwareFallback|23_0(short e0, short e1, short e2, short e3)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = e0;
			*(intPtr + 2) = e1;
			*(intPtr + (IntPtr)2 * 2) = e2;
			*(intPtr + (IntPtr)3 * 2) = e3;
			short* source = intPtr;
			return *Unsafe.AsRef<Vector64<short>>((void*)source);
		}

		// Token: 0x06003473 RID: 13427 RVA: 0x00170E04 File Offset: 0x00170004
		[CompilerGenerated]
		internal unsafe static Vector64<int> <Create>g__SoftwareFallback|24_0(int e0, int e1)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = e0;
			*(intPtr + 4) = e1;
			int* source = intPtr;
			return *Unsafe.AsRef<Vector64<int>>((void*)source);
		}

		// Token: 0x06003474 RID: 13428 RVA: 0x00170E2C File Offset: 0x0017002C
		[CompilerGenerated]
		internal unsafe static Vector64<sbyte> <Create>g__SoftwareFallback|25_0(sbyte e0, sbyte e1, sbyte e2, sbyte e3, sbyte e4, sbyte e5, sbyte e6, sbyte e7)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = (byte)e0;
			*(intPtr + 1) = (byte)e1;
			*(intPtr + 2) = (byte)e2;
			*(intPtr + 3) = (byte)e3;
			*(intPtr + 4) = (byte)e4;
			*(intPtr + 5) = (byte)e5;
			*(intPtr + 6) = (byte)e6;
			*(intPtr + 7) = (byte)e7;
			sbyte* source = intPtr;
			return *Unsafe.AsRef<Vector64<sbyte>>((void*)source);
		}

		// Token: 0x06003475 RID: 13429 RVA: 0x00170E74 File Offset: 0x00170074
		[CompilerGenerated]
		internal unsafe static Vector64<float> <Create>g__SoftwareFallback|26_0(float e0, float e1)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = e0;
			*(intPtr + 4) = e1;
			float* source = intPtr;
			return *Unsafe.AsRef<Vector64<float>>((void*)source);
		}

		// Token: 0x06003476 RID: 13430 RVA: 0x00170E9C File Offset: 0x0017009C
		[CompilerGenerated]
		internal unsafe static Vector64<ushort> <Create>g__SoftwareFallback|27_0(ushort e0, ushort e1, ushort e2, ushort e3)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = (short)e0;
			*(intPtr + 2) = (short)e1;
			*(intPtr + (IntPtr)2 * 2) = (short)e2;
			*(intPtr + (IntPtr)3 * 2) = (short)e3;
			ushort* source = intPtr;
			return *Unsafe.AsRef<Vector64<ushort>>((void*)source);
		}

		// Token: 0x06003477 RID: 13431 RVA: 0x00170ED4 File Offset: 0x001700D4
		[CompilerGenerated]
		internal unsafe static Vector64<uint> <Create>g__SoftwareFallback|28_0(uint e0, uint e1)
		{
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = (int)e0;
			*(intPtr + 4) = (int)e1;
			uint* source = intPtr;
			return *Unsafe.AsRef<Vector64<uint>>((void*)source);
		}

		// Token: 0x06003478 RID: 13432 RVA: 0x00170EFC File Offset: 0x001700FC
		[CompilerGenerated]
		internal static Vector64<byte> <CreateScalar>g__SoftwareFallback|29_0(byte value)
		{
			Vector64<byte> zero = Vector64<byte>.Zero;
			Unsafe.WriteUnaligned<byte>(Unsafe.As<Vector64<byte>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003479 RID: 13433 RVA: 0x00170C2F File Offset: 0x0016FE2F
		[CompilerGenerated]
		internal unsafe static Vector64<double> <CreateScalar>g__SoftwareFallback|30_0(double value)
		{
			return *Unsafe.As<double, Vector64<double>>(ref value);
		}

		// Token: 0x0600347A RID: 13434 RVA: 0x00170F20 File Offset: 0x00170120
		[CompilerGenerated]
		internal static Vector64<short> <CreateScalar>g__SoftwareFallback|31_0(short value)
		{
			Vector64<short> zero = Vector64<short>.Zero;
			Unsafe.WriteUnaligned<short>(Unsafe.As<Vector64<short>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x0600347B RID: 13435 RVA: 0x00170F44 File Offset: 0x00170144
		[CompilerGenerated]
		internal static Vector64<int> <CreateScalar>g__SoftwareFallback|32_0(int value)
		{
			Vector64<int> zero = Vector64<int>.Zero;
			Unsafe.WriteUnaligned<int>(Unsafe.As<Vector64<int>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x0600347C RID: 13436 RVA: 0x00170C9D File Offset: 0x0016FE9D
		[CompilerGenerated]
		internal unsafe static Vector64<long> <CreateScalar>g__SoftwareFallback|33_0(long value)
		{
			return *Unsafe.As<long, Vector64<long>>(ref value);
		}

		// Token: 0x0600347D RID: 13437 RVA: 0x00170F68 File Offset: 0x00170168
		[CompilerGenerated]
		internal static Vector64<sbyte> <CreateScalar>g__SoftwareFallback|34_0(sbyte value)
		{
			Vector64<sbyte> zero = Vector64<sbyte>.Zero;
			Unsafe.WriteUnaligned<sbyte>(Unsafe.As<Vector64<sbyte>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x0600347E RID: 13438 RVA: 0x00170F8C File Offset: 0x0017018C
		[CompilerGenerated]
		internal static Vector64<float> <CreateScalar>g__SoftwareFallback|35_0(float value)
		{
			Vector64<float> zero = Vector64<float>.Zero;
			Unsafe.WriteUnaligned<float>(Unsafe.As<Vector64<float>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x0600347F RID: 13439 RVA: 0x00170FB0 File Offset: 0x001701B0
		[CompilerGenerated]
		internal static Vector64<ushort> <CreateScalar>g__SoftwareFallback|36_0(ushort value)
		{
			Vector64<ushort> zero = Vector64<ushort>.Zero;
			Unsafe.WriteUnaligned<ushort>(Unsafe.As<Vector64<ushort>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003480 RID: 13440 RVA: 0x00170FD4 File Offset: 0x001701D4
		[CompilerGenerated]
		internal static Vector64<uint> <CreateScalar>g__SoftwareFallback|37_0(uint value)
		{
			Vector64<uint> zero = Vector64<uint>.Zero;
			Unsafe.WriteUnaligned<uint>(Unsafe.As<Vector64<uint>, byte>(ref zero), value);
			return zero;
		}

		// Token: 0x06003481 RID: 13441 RVA: 0x00170D75 File Offset: 0x0016FF75
		[CompilerGenerated]
		internal unsafe static Vector64<ulong> <CreateScalar>g__SoftwareFallback|38_0(ulong value)
		{
			return *Unsafe.As<ulong, Vector64<ulong>>(ref value);
		}
	}
}
