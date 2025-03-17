using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.Intrinsics;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Intrinsics.X86;
using System.Runtime.Versioning;

namespace System
{
	// Token: 0x02000074 RID: 116
	public static class Math
	{
		// Token: 0x060003D4 RID: 980
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Abs(double value);

		// Token: 0x060003D5 RID: 981
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern float Abs(float value);

		// Token: 0x060003D6 RID: 982
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Acos(double d);

		// Token: 0x060003D7 RID: 983
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Acosh(double d);

		// Token: 0x060003D8 RID: 984
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Asin(double d);

		// Token: 0x060003D9 RID: 985
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Asinh(double d);

		// Token: 0x060003DA RID: 986
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan(double d);

		// Token: 0x060003DB RID: 987
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atan2(double y, double x);

		// Token: 0x060003DC RID: 988
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Atanh(double d);

		// Token: 0x060003DD RID: 989
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cbrt(double d);

		// Token: 0x060003DE RID: 990
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Ceiling(double a);

		// Token: 0x060003DF RID: 991
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cos(double d);

		// Token: 0x060003E0 RID: 992
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Cosh(double value);

		// Token: 0x060003E1 RID: 993
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Exp(double d);

		// Token: 0x060003E2 RID: 994
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Floor(double d);

		// Token: 0x060003E3 RID: 995
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double FusedMultiplyAdd(double x, double y, double z);

		// Token: 0x060003E4 RID: 996
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern int ILogB(double x);

		// Token: 0x060003E5 RID: 997
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log(double d);

		// Token: 0x060003E6 RID: 998
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log2(double x);

		// Token: 0x060003E7 RID: 999
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Log10(double d);

		// Token: 0x060003E8 RID: 1000
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Pow(double x, double y);

		// Token: 0x060003E9 RID: 1001
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double ScaleB(double x, int n);

		// Token: 0x060003EA RID: 1002
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sin(double a);

		// Token: 0x060003EB RID: 1003
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sinh(double value);

		// Token: 0x060003EC RID: 1004
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Sqrt(double d);

		// Token: 0x060003ED RID: 1005
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tan(double a);

		// Token: 0x060003EE RID: 1006
		[Intrinsic]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern double Tanh(double value);

		// Token: 0x060003EF RID: 1007
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern double ModF(double x, double* intptr);

		// Token: 0x060003F0 RID: 1008 RVA: 0x000B6E03 File Offset: 0x000B6003
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short Abs(short value)
		{
			if (value < 0)
			{
				value = -value;
				if (value < 0)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000B6E18 File Offset: 0x000B6018
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Abs(int value)
		{
			if (value < 0)
			{
				value = -value;
				if (value < 0)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		// Token: 0x060003F2 RID: 1010 RVA: 0x000B6E2C File Offset: 0x000B602C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long Abs(long value)
		{
			if (value < 0L)
			{
				value = -value;
				if (value < 0L)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		// Token: 0x060003F3 RID: 1011 RVA: 0x000B6E42 File Offset: 0x000B6042
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte Abs(sbyte value)
		{
			if (value < 0)
			{
				value = -value;
				if (value < 0)
				{
					Math.ThrowAbsOverflow();
				}
			}
			return value;
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000B6E57 File Offset: 0x000B6057
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Abs(decimal value)
		{
			return decimal.Abs(value);
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x000B6E60 File Offset: 0x000B6060
		[DoesNotReturn]
		[StackTraceHidden]
		private static void ThrowAbsOverflow()
		{
			throw new OverflowException(SR.Overflow_NegateTwosCompNum);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x000B6E6C File Offset: 0x000B606C
		public static long BigMul(int a, int b)
		{
			return (long)a * (long)b;
		}

		// Token: 0x060003F7 RID: 1015 RVA: 0x000B6E74 File Offset: 0x000B6074
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong BigMul(ulong a, ulong b, out ulong low)
		{
			if (Bmi2.X64.IsSupported)
			{
				ulong num;
				ulong result = Bmi2.X64.MultiplyNoFlags(a, b, &num);
				low = num;
				return result;
			}
			return Math.<BigMul>g__SoftwareFallback|42_0(a, b, out low);
		}

		// Token: 0x060003F8 RID: 1016 RVA: 0x000B6EA0 File Offset: 0x000B60A0
		public static long BigMul(long a, long b, out long low)
		{
			ulong num2;
			ulong num = Math.BigMul((ulong)a, (ulong)b, out num2);
			low = (long)num2;
			return (long)(num - (ulong)(a >> 63 & b) - (ulong)(b >> 63 & a));
		}

		// Token: 0x060003F9 RID: 1017 RVA: 0x000B6ECC File Offset: 0x000B60CC
		public static double BitDecrement(double x)
		{
			long num = BitConverter.DoubleToInt64Bits(x);
			if ((num >> 32 & 2146435072L) >= 2146435072L)
			{
				if (num != 9218868437227405312L)
				{
					return x;
				}
				return double.MaxValue;
			}
			else
			{
				if (num == 0L)
				{
					return -5E-324;
				}
				num += ((num < 0L) ? 1L : -1L);
				return BitConverter.Int64BitsToDouble(num);
			}
		}

		// Token: 0x060003FA RID: 1018 RVA: 0x000B6F2C File Offset: 0x000B612C
		public static double BitIncrement(double x)
		{
			long num = BitConverter.DoubleToInt64Bits(x);
			if ((num >> 32 & 2146435072L) >= 2146435072L)
			{
				if (num != -4503599627370496L)
				{
					return x;
				}
				return double.MinValue;
			}
			else
			{
				if (num == -9223372036854775808L)
				{
					return double.Epsilon;
				}
				num += ((num < 0L) ? -1L : 1L);
				return BitConverter.Int64BitsToDouble(num);
			}
		}

		// Token: 0x060003FB RID: 1019 RVA: 0x000B6F94 File Offset: 0x000B6194
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double CopySign(double x, double y)
		{
			if (Sse2.IsSupported || AdvSimd.IsSupported)
			{
				return VectorMath.ConditionalSelectBitwise(Vector128.CreateScalarUnsafe(--0.0), Vector128.CreateScalarUnsafe(y), Vector128.CreateScalarUnsafe(x)).ToScalar<double>();
			}
			return Math.<CopySign>g__SoftwareFallback|46_0(x, y);
		}

		// Token: 0x060003FC RID: 1020 RVA: 0x000B6FD0 File Offset: 0x000B61D0
		public static int DivRem(int a, int b, out int result)
		{
			int num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x060003FD RID: 1021 RVA: 0x000B6FEC File Offset: 0x000B61EC
		public static long DivRem(long a, long b, out long result)
		{
			long num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x060003FE RID: 1022 RVA: 0x000B7008 File Offset: 0x000B6208
		internal static uint DivRem(uint a, uint b, out uint result)
		{
			uint num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x060003FF RID: 1023 RVA: 0x000B7024 File Offset: 0x000B6224
		internal static ulong DivRem(ulong a, ulong b, out ulong result)
		{
			ulong num = a / b;
			result = a - num * b;
			return num;
		}

		// Token: 0x06000400 RID: 1024 RVA: 0x000B703D File Offset: 0x000B623D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Ceiling(decimal d)
		{
			return decimal.Ceiling(d);
		}

		// Token: 0x06000401 RID: 1025 RVA: 0x000B7045 File Offset: 0x000B6245
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static byte Clamp(byte value, byte min, byte max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<byte>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000402 RID: 1026 RVA: 0x000B705F File Offset: 0x000B625F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Clamp(decimal value, decimal min, decimal max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<decimal>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000403 RID: 1027 RVA: 0x000B7088 File Offset: 0x000B6288
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Clamp(double value, double min, double max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<double>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000404 RID: 1028 RVA: 0x000B70A2 File Offset: 0x000B62A2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static short Clamp(short value, short min, short max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<short>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000405 RID: 1029 RVA: 0x000B70BC File Offset: 0x000B62BC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Clamp(int value, int min, int max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<int>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000406 RID: 1030 RVA: 0x000B70D6 File Offset: 0x000B62D6
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static long Clamp(long value, long min, long max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<long>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000407 RID: 1031 RVA: 0x000B70F0 File Offset: 0x000B62F0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static sbyte Clamp(sbyte value, sbyte min, sbyte max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<sbyte>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000408 RID: 1032 RVA: 0x000B710A File Offset: 0x000B630A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Clamp(float value, float min, float max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<float>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x06000409 RID: 1033 RVA: 0x000B7124 File Offset: 0x000B6324
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ushort Clamp(ushort value, ushort min, ushort max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<ushort>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600040A RID: 1034 RVA: 0x000B713E File Offset: 0x000B633E
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint Clamp(uint value, uint min, uint max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<uint>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600040B RID: 1035 RVA: 0x000B7158 File Offset: 0x000B6358
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ulong Clamp(ulong value, ulong min, ulong max)
		{
			if (min > max)
			{
				Math.ThrowMinMaxException<ulong>(min, max);
			}
			if (value < min)
			{
				return min;
			}
			if (value > max)
			{
				return max;
			}
			return value;
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x000B7172 File Offset: 0x000B6372
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Floor(decimal d)
		{
			return decimal.Floor(d);
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x000B717C File Offset: 0x000B637C
		public static double IEEERemainder(double x, double y)
		{
			if (double.IsNaN(x))
			{
				return x;
			}
			if (double.IsNaN(y))
			{
				return y;
			}
			double num = x % y;
			if (double.IsNaN(num))
			{
				return double.NaN;
			}
			if (num == 0.0 && double.IsNegative(x))
			{
				return --0.0;
			}
			double num2 = num - Math.Abs(y) * (double)Math.Sign(x);
			if (Math.Abs(num2) == Math.Abs(num))
			{
				double num3 = x / y;
				double value = Math.Round(num3);
				if (Math.Abs(value) > Math.Abs(num3))
				{
					return num2;
				}
				return num;
			}
			else
			{
				if (Math.Abs(num2) < Math.Abs(num))
				{
					return num2;
				}
				return num;
			}
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x000B7220 File Offset: 0x000B6420
		public static double Log(double a, double newBase)
		{
			if (double.IsNaN(a))
			{
				return a;
			}
			if (double.IsNaN(newBase))
			{
				return newBase;
			}
			if (newBase == 1.0)
			{
				return double.NaN;
			}
			if (a != 1.0 && (newBase == 0.0 || double.IsPositiveInfinity(newBase)))
			{
				return double.NaN;
			}
			return Math.Log(a) / Math.Log(newBase);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x000B728E File Offset: 0x000B648E
		[NonVersionable]
		public static byte Max(byte val1, byte val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x000B7297 File Offset: 0x000B6497
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static decimal Max(decimal val1, decimal val2)
		{
			return *decimal.Max(val1, val2);
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x000B72A7 File Offset: 0x000B64A7
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Max(double val1, double val2)
		{
			if (val1 != val2)
			{
				if (double.IsNaN(val1))
				{
					return val1;
				}
				if (val2 >= val1)
				{
					return val2;
				}
				return val1;
			}
			else
			{
				if (!double.IsNegative(val2))
				{
					return val2;
				}
				return val1;
			}
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x000B728E File Offset: 0x000B648E
		[NonVersionable]
		public static short Max(short val1, short val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x000B728E File Offset: 0x000B648E
		[NonVersionable]
		public static int Max(int val1, int val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x000B728E File Offset: 0x000B648E
		[NonVersionable]
		public static long Max(long val1, long val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x000B728E File Offset: 0x000B648E
		[CLSCompliant(false)]
		[NonVersionable]
		public static sbyte Max(sbyte val1, sbyte val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x000B72CA File Offset: 0x000B64CA
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Max(float val1, float val2)
		{
			if (val1 != val2)
			{
				if (float.IsNaN(val1))
				{
					return val1;
				}
				if (val2 >= val1)
				{
					return val2;
				}
				return val1;
			}
			else
			{
				if (!float.IsNegative(val2))
				{
					return val2;
				}
				return val1;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x000B728E File Offset: 0x000B648E
		[CLSCompliant(false)]
		[NonVersionable]
		public static ushort Max(ushort val1, ushort val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x000B72ED File Offset: 0x000B64ED
		[CLSCompliant(false)]
		[NonVersionable]
		public static uint Max(uint val1, uint val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x000B72ED File Offset: 0x000B64ED
		[CLSCompliant(false)]
		[NonVersionable]
		public static ulong Max(ulong val1, ulong val2)
		{
			if (val1 < val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x000B72F8 File Offset: 0x000B64F8
		public static double MaxMagnitude(double x, double y)
		{
			double num = Math.Abs(x);
			double num2 = Math.Abs(y);
			if (num > num2 || double.IsNaN(num))
			{
				return x;
			}
			if (num != num2)
			{
				return y;
			}
			if (!double.IsNegative(x))
			{
				return x;
			}
			return y;
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x000B7332 File Offset: 0x000B6532
		[NonVersionable]
		public static byte Min(byte val1, byte val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x000B733B File Offset: 0x000B653B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static decimal Min(decimal val1, decimal val2)
		{
			return *decimal.Min(val1, val2);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x000B734B File Offset: 0x000B654B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Min(double val1, double val2)
		{
			if (val1 != val2 && !double.IsNaN(val1))
			{
				if (val1 >= val2)
				{
					return val2;
				}
				return val1;
			}
			else
			{
				if (!double.IsNegative(val1))
				{
					return val2;
				}
				return val1;
			}
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x000B7332 File Offset: 0x000B6532
		[NonVersionable]
		public static short Min(short val1, short val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x000B7332 File Offset: 0x000B6532
		[NonVersionable]
		public static int Min(int val1, int val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000420 RID: 1056 RVA: 0x000B7332 File Offset: 0x000B6532
		[NonVersionable]
		public static long Min(long val1, long val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x000B7332 File Offset: 0x000B6532
		[NonVersionable]
		[CLSCompliant(false)]
		public static sbyte Min(sbyte val1, sbyte val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x000B736C File Offset: 0x000B656C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static float Min(float val1, float val2)
		{
			if (val1 != val2 && !float.IsNaN(val1))
			{
				if (val1 >= val2)
				{
					return val2;
				}
				return val1;
			}
			else
			{
				if (!float.IsNegative(val1))
				{
					return val2;
				}
				return val1;
			}
		}

		// Token: 0x06000423 RID: 1059 RVA: 0x000B7332 File Offset: 0x000B6532
		[NonVersionable]
		[CLSCompliant(false)]
		public static ushort Min(ushort val1, ushort val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000424 RID: 1060 RVA: 0x000B738D File Offset: 0x000B658D
		[CLSCompliant(false)]
		[NonVersionable]
		public static uint Min(uint val1, uint val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000425 RID: 1061 RVA: 0x000B738D File Offset: 0x000B658D
		[CLSCompliant(false)]
		[NonVersionable]
		public static ulong Min(ulong val1, ulong val2)
		{
			if (val1 > val2)
			{
				return val2;
			}
			return val1;
		}

		// Token: 0x06000426 RID: 1062 RVA: 0x000B7398 File Offset: 0x000B6598
		public static double MinMagnitude(double x, double y)
		{
			double num = Math.Abs(x);
			double num2 = Math.Abs(y);
			if (num < num2 || double.IsNaN(num))
			{
				return x;
			}
			if (num != num2)
			{
				return y;
			}
			if (!double.IsNegative(x))
			{
				return y;
			}
			return x;
		}

		// Token: 0x06000427 RID: 1063 RVA: 0x000B73D2 File Offset: 0x000B65D2
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Round(decimal d)
		{
			return decimal.Round(d, 0);
		}

		// Token: 0x06000428 RID: 1064 RVA: 0x000B73DB File Offset: 0x000B65DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Round(decimal d, int decimals)
		{
			return decimal.Round(d, decimals);
		}

		// Token: 0x06000429 RID: 1065 RVA: 0x000B73E4 File Offset: 0x000B65E4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Round(decimal d, MidpointRounding mode)
		{
			return decimal.Round(d, 0, mode);
		}

		// Token: 0x0600042A RID: 1066 RVA: 0x000B73EE File Offset: 0x000B65EE
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Round(decimal d, int decimals, MidpointRounding mode)
		{
			return decimal.Round(d, decimals, mode);
		}

		// Token: 0x0600042B RID: 1067 RVA: 0x000B73F8 File Offset: 0x000B65F8
		[Intrinsic]
		public static double Round(double a)
		{
			ulong num = (ulong)BitConverter.DoubleToInt64Bits(a);
			int num2 = double.ExtractExponentFromBits(num);
			if (num2 <= 1022)
			{
				if (num << 1 == 0UL)
				{
					return a;
				}
				double x = (num2 == 1022 && double.ExtractSignificandFromBits(num) != 0UL) ? 1.0 : 0.0;
				return Math.CopySign(x, a);
			}
			else
			{
				if (num2 >= 1075)
				{
					return a;
				}
				ulong num3 = 1UL << 1075 - num2;
				ulong num4 = num3 - 1UL;
				num += num3 >> 1;
				if ((num & num4) == 0UL)
				{
					num &= ~num3;
				}
				else
				{
					num &= ~num4;
				}
				return BitConverter.Int64BitsToDouble((long)num);
			}
		}

		// Token: 0x0600042C RID: 1068 RVA: 0x000B748B File Offset: 0x000B668B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Round(double value, int digits)
		{
			return Math.Round(value, digits, MidpointRounding.ToEven);
		}

		// Token: 0x0600042D RID: 1069 RVA: 0x000B7495 File Offset: 0x000B6695
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static double Round(double value, MidpointRounding mode)
		{
			return Math.Round(value, 0, mode);
		}

		// Token: 0x0600042E RID: 1070 RVA: 0x000B74A0 File Offset: 0x000B66A0
		public unsafe static double Round(double value, int digits, MidpointRounding mode)
		{
			if (digits < 0 || digits > 15)
			{
				throw new ArgumentOutOfRangeException("digits", SR.ArgumentOutOfRange_RoundingDigits);
			}
			if (mode < MidpointRounding.ToEven || mode > MidpointRounding.ToPositiveInfinity)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidEnumValue, mode, "MidpointRounding"), "mode");
			}
			if (Math.Abs(value) < 10000000000000000.0)
			{
				double num = Math.roundPower10Double[digits];
				value *= num;
				switch (mode)
				{
				case MidpointRounding.ToEven:
					value = Math.Round(value);
					break;
				case MidpointRounding.AwayFromZero:
				{
					double value2 = Math.ModF(value, &value);
					if (Math.Abs(value2) >= 0.5)
					{
						value += (double)Math.Sign(value2);
					}
					break;
				}
				case MidpointRounding.ToZero:
					value = Math.Truncate(value);
					break;
				case MidpointRounding.ToNegativeInfinity:
					value = Math.Floor(value);
					break;
				case MidpointRounding.ToPositiveInfinity:
					value = Math.Ceiling(value);
					break;
				default:
					throw new ArgumentException(SR.Format(SR.Argument_InvalidEnumValue, mode, "MidpointRounding"), "mode");
				}
				value /= num;
			}
			return value;
		}

		// Token: 0x0600042F RID: 1071 RVA: 0x000B75A1 File Offset: 0x000B67A1
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(decimal value)
		{
			return decimal.Sign(value);
		}

		// Token: 0x06000430 RID: 1072 RVA: 0x000B75AA File Offset: 0x000B67AA
		public static int Sign(double value)
		{
			if (value < 0.0)
			{
				return -1;
			}
			if (value > 0.0)
			{
				return 1;
			}
			if (value == 0.0)
			{
				return 0;
			}
			throw new ArithmeticException(SR.Arithmetic_NaN);
		}

		// Token: 0x06000431 RID: 1073 RVA: 0x000B75E0 File Offset: 0x000B67E0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(short value)
		{
			return Math.Sign((int)value);
		}

		// Token: 0x06000432 RID: 1074 RVA: 0x000B75E8 File Offset: 0x000B67E8
		public static int Sign(int value)
		{
			return value >> 31 | (int)((uint)(-(uint)value) >> 31);
		}

		// Token: 0x06000433 RID: 1075 RVA: 0x000B75F4 File Offset: 0x000B67F4
		public static int Sign(long value)
		{
			return (int)(value >> 63 | (long)((ulong)(-(ulong)value) >> 63));
		}

		// Token: 0x06000434 RID: 1076 RVA: 0x000B75E0 File Offset: 0x000B67E0
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static int Sign(sbyte value)
		{
			return Math.Sign((int)value);
		}

		// Token: 0x06000435 RID: 1077 RVA: 0x000B7601 File Offset: 0x000B6801
		public static int Sign(float value)
		{
			if (value < 0f)
			{
				return -1;
			}
			if (value > 0f)
			{
				return 1;
			}
			if (value == 0f)
			{
				return 0;
			}
			throw new ArithmeticException(SR.Arithmetic_NaN);
		}

		// Token: 0x06000436 RID: 1078 RVA: 0x000B762B File Offset: 0x000B682B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static decimal Truncate(decimal d)
		{
			return decimal.Truncate(d);
		}

		// Token: 0x06000437 RID: 1079 RVA: 0x000B7633 File Offset: 0x000B6833
		public unsafe static double Truncate(double d)
		{
			Math.ModF(d, &d);
			return d;
		}

		// Token: 0x06000438 RID: 1080 RVA: 0x000B7640 File Offset: 0x000B6840
		[DoesNotReturn]
		private static void ThrowMinMaxException<T>(T min, T max)
		{
			throw new ArgumentException(SR.Format(SR.Argument_MinMaxValue, min, max));
		}

		// Token: 0x0600043A RID: 1082 RVA: 0x000B7678 File Offset: 0x000B6878
		[CompilerGenerated]
		internal static ulong <BigMul>g__SoftwareFallback|42_0(ulong a, ulong b, out ulong low)
		{
			uint num = (uint)a;
			uint num2 = (uint)(a >> 32);
			uint num3 = (uint)b;
			uint num4 = (uint)(b >> 32);
			ulong num5 = (ulong)num * (ulong)num3;
			ulong num6 = (ulong)num2 * (ulong)num3 + (num5 >> 32);
			ulong num7 = (ulong)num * (ulong)num4 + (ulong)((uint)num6);
			low = (num7 << 32 | (ulong)((uint)num5));
			return (ulong)num2 * (ulong)num4 + (num6 >> 32) + (num7 >> 32);
		}

		// Token: 0x0600043B RID: 1083 RVA: 0x000B76D4 File Offset: 0x000B68D4
		[CompilerGenerated]
		internal static double <CopySign>g__SoftwareFallback|46_0(double x, double y)
		{
			long num = BitConverter.DoubleToInt64Bits(x);
			long num2 = BitConverter.DoubleToInt64Bits(y);
			num &= long.MaxValue;
			num2 &= long.MinValue;
			return BitConverter.Int64BitsToDouble(num | num2);
		}

		// Token: 0x04000181 RID: 385
		public const double E = 2.718281828459045;

		// Token: 0x04000182 RID: 386
		public const double PI = 3.141592653589793;

		// Token: 0x04000183 RID: 387
		public const double Tau = 6.283185307179586;

		// Token: 0x04000184 RID: 388
		private static readonly double[] roundPower10Double = new double[]
		{
			1.0,
			10.0,
			100.0,
			1000.0,
			10000.0,
			100000.0,
			1000000.0,
			10000000.0,
			100000000.0,
			1000000000.0,
			10000000000.0,
			100000000000.0,
			1000000000000.0,
			10000000000000.0,
			100000000000000.0,
			1000000000000000.0
		};
	}
}
