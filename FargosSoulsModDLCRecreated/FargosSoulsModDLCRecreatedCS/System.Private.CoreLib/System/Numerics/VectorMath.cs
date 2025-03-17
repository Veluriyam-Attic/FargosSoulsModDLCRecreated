using System;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace System.Numerics
{
	// Token: 0x020001D2 RID: 466
	internal static class VectorMath
	{
		// Token: 0x06001CE1 RID: 7393 RVA: 0x0010AA73 File Offset: 0x00109C73
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<float> ConditionalSelectBitwise(Vector128<float> selector, Vector128<float> ifTrue, Vector128<float> ifFalse)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.BitwiseSelect(selector, ifTrue, ifFalse);
			}
			if (Sse.IsSupported)
			{
				return Sse.Or(Sse.And(ifTrue, selector), Sse.AndNot(selector, ifFalse));
			}
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001CE2 RID: 7394 RVA: 0x0010AAA5 File Offset: 0x00109CA5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<double> ConditionalSelectBitwise(Vector128<double> selector, Vector128<double> ifTrue, Vector128<double> ifFalse)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.BitwiseSelect(selector, ifTrue, ifFalse);
			}
			if (Sse2.IsSupported)
			{
				return Sse2.Or(Sse2.And(ifTrue, selector), Sse2.AndNot(selector, ifFalse));
			}
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001CE3 RID: 7395 RVA: 0x0010AAD8 File Offset: 0x00109CD8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool Equal(Vector128<float> vector1, Vector128<float> vector2)
		{
			if (AdvSimd.Arm64.IsSupported)
			{
				Vector128<uint> vector3 = AdvSimd.CompareEqual(vector1, vector2).AsUInt32<float>();
				Vector64<byte> left = vector3.GetLower<uint>().AsByte<uint>();
				Vector64<byte> right = vector3.GetUpper<uint>().AsByte<uint>();
				Vector64<byte> vector4 = AdvSimd.Arm64.ZipLow(left, right);
				Vector64<byte> vector5 = AdvSimd.Arm64.ZipHigh(left, right);
				Vector64<ushort> vector6 = AdvSimd.Arm64.ZipHigh(vector4.AsUInt16<byte>(), vector5.AsUInt16<byte>());
				return vector6.AsUInt32<ushort>().GetElement(1) == uint.MaxValue;
			}
			if (Sse.IsSupported)
			{
				return Sse.MoveMask(Sse.CompareNotEqual(vector1, vector2)) == 0;
			}
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001CE4 RID: 7396 RVA: 0x0010AB64 File Offset: 0x00109D64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector128<float> Lerp(Vector128<float> a, Vector128<float> b, Vector128<float> t)
		{
			if (AdvSimd.IsSupported)
			{
				return AdvSimd.FusedMultiplyAdd(a, AdvSimd.Subtract(b, a), t);
			}
			if (Fma.IsSupported)
			{
				return Fma.MultiplyAdd(Sse.Subtract(b, a), t, a);
			}
			if (Sse.IsSupported)
			{
				return Sse.Add(Sse.Multiply(a, Sse.Subtract(Vector128.Create(1f), t)), Sse.Multiply(b, t));
			}
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06001CE5 RID: 7397 RVA: 0x0010ABCC File Offset: 0x00109DCC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool NotEqual(Vector128<float> vector1, Vector128<float> vector2)
		{
			if (AdvSimd.IsSupported)
			{
				Vector128<uint> vector3 = AdvSimd.CompareEqual(vector1, vector2).AsUInt32<float>();
				Vector64<byte> left = vector3.GetLower<uint>().AsByte<uint>();
				Vector64<byte> right = vector3.GetUpper<uint>().AsByte<uint>();
				Vector64<byte> vector4 = AdvSimd.Arm64.ZipLow(left, right);
				Vector64<byte> vector5 = AdvSimd.Arm64.ZipHigh(left, right);
				Vector64<ushort> vector6 = AdvSimd.Arm64.ZipHigh(vector4.AsUInt16<byte>(), vector5.AsUInt16<byte>());
				return vector6.AsUInt32<ushort>().GetElement(1) != uint.MaxValue;
			}
			if (Sse.IsSupported)
			{
				return Sse.MoveMask(Sse.CompareNotEqual(vector1, vector2)) != 0;
			}
			throw new PlatformNotSupportedException();
		}
	}
}
