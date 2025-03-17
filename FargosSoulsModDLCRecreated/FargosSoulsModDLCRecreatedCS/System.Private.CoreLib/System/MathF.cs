using System;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;

namespace System
{
	// Token: 0x02000075 RID: 117
	public static class MathF
	{
		// Token: 0x0600043C RID: 1084
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Acos(float x);

		// Token: 0x0600043D RID: 1085
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Acosh(float x);

		// Token: 0x0600043E RID: 1086
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Asin(float x);

		// Token: 0x0600043F RID: 1087
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Asinh(float x);

		// Token: 0x06000440 RID: 1088
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Atan(float x);

		// Token: 0x06000441 RID: 1089
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Atan2(float y, float x);

		// Token: 0x06000442 RID: 1090
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Atanh(float x);

		// Token: 0x06000443 RID: 1091
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Cbrt(float x);

		// Token: 0x06000444 RID: 1092
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Ceiling(float x);

		// Token: 0x06000445 RID: 1093
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Cos(float x);

		// Token: 0x06000446 RID: 1094
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Cosh(float x);

		// Token: 0x06000447 RID: 1095
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Exp(float x);

		// Token: 0x06000448 RID: 1096
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Floor(float x);

		// Token: 0x06000449 RID: 1097
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float FusedMultiplyAdd(float x, float y, float z);

		// Token: 0x0600044A RID: 1098
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ILogB(float x);

		// Token: 0x0600044B RID: 1099
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Log(float x);

		// Token: 0x0600044C RID: 1100
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Log2(float x);

		// Token: 0x0600044D RID: 1101
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Log10(float x);

		// Token: 0x0600044E RID: 1102
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Pow(float x, float y);

		// Token: 0x0600044F RID: 1103
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float ScaleB(float x, int n);

		// Token: 0x06000450 RID: 1104
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Sin(float x);

		// Token: 0x06000451 RID: 1105
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Sinh(float x);

		// Token: 0x06000452 RID: 1106
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Sqrt(float x);

		// Token: 0x06000453 RID: 1107
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Tan(float x);

		// Token: 0x06000454 RID: 1108
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Tanh(float x);

		// Token: 0x06000455 RID: 1109
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern float ModF(float x, float* intptr);

		// Token: 0x06000456 RID: 1110 RVA: 0x000B770F File Offset: 0x000B690F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Abs(float x)
		{
			return Math.Abs(x);
		}

		// Token: 0x06000457 RID: 1111 RVA: 0x000B7718 File Offset: 0x000B6918
		public static float BitDecrement(float x)
		{
			int num = BitConverter.SingleToInt32Bits(x);
			if ((num & 2139095040) >= 2139095040)
			{
				if (num != 2139095040)
				{
					return x;
				}
				return float.MaxValue;
			}
			else
			{
				if (num == 0)
				{
					return -1E-45f;
				}
				num += ((num < 0) ? 1 : -1);
				return BitConverter.Int32BitsToSingle(num);
			}
		}

		// Token: 0x06000458 RID: 1112 RVA: 0x000B7764 File Offset: 0x000B6964
		public static float BitIncrement(float x)
		{
			int num = BitConverter.SingleToInt32Bits(x);
			if ((num & 2139095040) >= 2139095040)
			{
				if (num != -8388608)
				{
					return x;
				}
				return float.MinValue;
			}
			else
			{
				if (num == -2147483648)
				{
					return float.Epsilon;
				}
				num += ((num < 0) ? -1 : 1);
				return BitConverter.Int32BitsToSingle(num);
			}
		}

		// Token: 0x06000459 RID: 1113 RVA: 0x000B77B5 File Offset: 0x000B69B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float CopySign(float x, float y)
		{
			if (Sse.IsSupported || AdvSimd.IsSupported)
			{
				return VectorMath.ConditionalSelectBitwise(Vector128.CreateScalarUnsafe(--0f), Vector128.CreateScalarUnsafe(y), Vector128.CreateScalarUnsafe(x)).ToScalar<float>();
			}
			return MathF.<CopySign>g__SoftwareFallback|36_0(x, y);
		}

		// Token: 0x0600045A RID: 1114 RVA: 0x000B77F0 File Offset: 0x000B69F0
		public static float IEEERemainder(float x, float y)
		{
			if (float.IsNaN(x))
			{
				return x;
			}
			if (float.IsNaN(y))
			{
				return y;
			}
			float num = x % y;
			if (float.IsNaN(num))
			{
				return float.NaN;
			}
			if (num == 0f && float.IsNegative(x))
			{
				return --0f;
			}
			float num2 = num - MathF.Abs(y) * (float)MathF.Sign(x);
			if (MathF.Abs(num2) == MathF.Abs(num))
			{
				float x2 = x / y;
				float x3 = MathF.Round(x2);
				if (MathF.Abs(x3) > MathF.Abs(x2))
				{
					return num2;
				}
				return num;
			}
			else
			{
				if (MathF.Abs(num2) < MathF.Abs(num))
				{
					return num2;
				}
				return num;
			}
		}

		// Token: 0x0600045B RID: 1115 RVA: 0x000B7888 File Offset: 0x000B6A88
		public static float Log(float x, float y)
		{
			if (float.IsNaN(x))
			{
				return x;
			}
			if (float.IsNaN(y))
			{
				return y;
			}
			if (y == 1f)
			{
				return float.NaN;
			}
			if (x != 1f && (y == 0f || float.IsPositiveInfinity(y)))
			{
				return float.NaN;
			}
			return MathF.Log(x) / MathF.Log(y);
		}

		// Token: 0x0600045C RID: 1116 RVA: 0x000B78E2 File Offset: 0x000B6AE2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Max(float x, float y)
		{
			return Math.Max(x, y);
		}

		// Token: 0x0600045D RID: 1117 RVA: 0x000B78EC File Offset: 0x000B6AEC
		public static float MaxMagnitude(float x, float y)
		{
			float num = MathF.Abs(x);
			float num2 = MathF.Abs(y);
			if (num > num2 || float.IsNaN(num))
			{
				return x;
			}
			if (num != num2)
			{
				return y;
			}
			if (!float.IsNegative(x))
			{
				return x;
			}
			return y;
		}

		// Token: 0x0600045E RID: 1118 RVA: 0x000B7926 File Offset: 0x000B6B26
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Min(float x, float y)
		{
			return Math.Min(x, y);
		}

		// Token: 0x0600045F RID: 1119 RVA: 0x000B7930 File Offset: 0x000B6B30
		public static float MinMagnitude(float x, float y)
		{
			float num = MathF.Abs(x);
			float num2 = MathF.Abs(y);
			if (num < num2 || float.IsNaN(num))
			{
				return x;
			}
			if (num != num2)
			{
				return y;
			}
			if (!float.IsNegative(x))
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000460 RID: 1120 RVA: 0x000B796C File Offset: 0x000B6B6C
		[Intrinsic]
		public static float Round(float x)
		{
			uint num = (uint)BitConverter.SingleToInt32Bits(x);
			int num2 = float.ExtractExponentFromBits(num);
			if (num2 <= 126)
			{
				if (num << 1 == 0U)
				{
					return x;
				}
				float x2 = (num2 == 126 && float.ExtractSignificandFromBits(num) != 0U) ? 1f : 0f;
				return MathF.CopySign(x2, x);
			}
			else
			{
				if (num2 >= 150)
				{
					return x;
				}
				uint num3 = 1U << 150 - num2;
				uint num4 = num3 - 1U;
				num += num3 >> 1;
				if ((num & num4) == 0U)
				{
					num &= ~num3;
				}
				else
				{
					num &= ~num4;
				}
				return BitConverter.Int32BitsToSingle((int)num);
			}
		}

		// Token: 0x06000461 RID: 1121 RVA: 0x000B79EF File Offset: 0x000B6BEF
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Round(float x, int digits)
		{
			return MathF.Round(x, digits, MidpointRounding.ToEven);
		}

		// Token: 0x06000462 RID: 1122 RVA: 0x000B79F9 File Offset: 0x000B6BF9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Round(float x, MidpointRounding mode)
		{
			return MathF.Round(x, 0, mode);
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x000B7A04 File Offset: 0x000B6C04
		public unsafe static float Round(float x, int digits, MidpointRounding mode)
		{
			if (digits < 0 || digits > 6)
			{
				throw new ArgumentOutOfRangeException("digits", SR.ArgumentOutOfRange_RoundingDigits);
			}
			if (mode < MidpointRounding.ToEven || mode > MidpointRounding.ToPositiveInfinity)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidEnumValue, mode, "MidpointRounding"), "mode");
			}
			if (MathF.Abs(x) < 100000000f)
			{
				float num = MathF.roundPower10Single[digits];
				x *= num;
				switch (mode)
				{
				case MidpointRounding.ToEven:
					x = MathF.Round(x);
					break;
				case MidpointRounding.AwayFromZero:
				{
					float x2 = MathF.ModF(x, &x);
					if ((double)MathF.Abs(x2) >= 0.5)
					{
						x += (float)MathF.Sign(x2);
					}
					break;
				}
				case MidpointRounding.ToZero:
					x = MathF.Truncate(x);
					break;
				case MidpointRounding.ToNegativeInfinity:
					x = MathF.Floor(x);
					break;
				case MidpointRounding.ToPositiveInfinity:
					x = MathF.Ceiling(x);
					break;
				default:
					throw new ArgumentException(SR.Format(SR.Argument_InvalidEnumValue, mode, "MidpointRounding"), "mode");
				}
				x /= num;
			}
			return x;
		}

		// Token: 0x06000464 RID: 1124 RVA: 0x000B7B01 File Offset: 0x000B6D01
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(float x)
		{
			return Math.Sign(x);
		}

		// Token: 0x06000465 RID: 1125 RVA: 0x000B7B09 File Offset: 0x000B6D09
		public unsafe static float Truncate(float x)
		{
			MathF.ModF(x, &x);
			return x;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000B7B30 File Offset: 0x000B6D30
		[CompilerGenerated]
		internal static float <CopySign>g__SoftwareFallback|36_0(float x, float y)
		{
			int num = BitConverter.SingleToInt32Bits(x);
			int num2 = BitConverter.SingleToInt32Bits(y);
			num &= int.MaxValue;
			num2 &= int.MinValue;
			return BitConverter.Int32BitsToSingle(num | num2);
		}

		// Token: 0x04000185 RID: 389
		public const float E = 2.7182817f;

		// Token: 0x04000186 RID: 390
		public const float PI = 3.1415927f;

		// Token: 0x04000187 RID: 391
		public const float Tau = 6.2831855f;

		// Token: 0x04000188 RID: 392
		private static readonly float[] roundPower10Single = new float[]
		{
			1f,
			10f,
			100f,
			1000f,
			10000f,
			100000f,
			1000000f
		};
	}
}
