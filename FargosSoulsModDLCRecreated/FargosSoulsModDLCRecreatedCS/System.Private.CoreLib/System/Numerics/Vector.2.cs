using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x020001D6 RID: 470
	[Intrinsic]
	public static class Vector
	{
		// Token: 0x06001D36 RID: 7478 RVA: 0x0011971C File Offset: 0x0011891C
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static void Widen(Vector<byte> source, out Vector<ushort> low, out Vector<ushort> high)
		{
			int count = Vector<byte>.Count;
			ushort* ptr = stackalloc ushort[checked(unchecked((UIntPtr)(count / 2)) * 2)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (ushort)source[i];
			}
			ushort* ptr2 = stackalloc ushort[checked(unchecked((UIntPtr)(count / 2)) * 2)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (ushort)source[j + count / 2];
			}
			low = new Vector<ushort>((void*)ptr);
			high = new Vector<ushort>((void*)ptr2);
		}

		// Token: 0x06001D37 RID: 7479 RVA: 0x001197A0 File Offset: 0x001189A0
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static void Widen(Vector<ushort> source, out Vector<uint> low, out Vector<uint> high)
		{
			int count = Vector<ushort>.Count;
			uint* ptr = stackalloc uint[checked(unchecked((UIntPtr)(count / 2)) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (uint)source[i];
			}
			uint* ptr2 = stackalloc uint[checked(unchecked((UIntPtr)(count / 2)) * 4)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (uint)source[j + count / 2];
			}
			low = new Vector<uint>((void*)ptr);
			high = new Vector<uint>((void*)ptr2);
		}

		// Token: 0x06001D38 RID: 7480 RVA: 0x00119824 File Offset: 0x00118A24
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static void Widen(Vector<uint> source, out Vector<ulong> low, out Vector<ulong> high)
		{
			int count = Vector<uint>.Count;
			ulong* ptr = stackalloc ulong[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (ulong)source[i];
			}
			ulong* ptr2 = stackalloc ulong[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (ulong)source[j + count / 2];
			}
			low = new Vector<ulong>((void*)ptr);
			high = new Vector<ulong>((void*)ptr2);
		}

		// Token: 0x06001D39 RID: 7481 RVA: 0x001198A8 File Offset: 0x00118AA8
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static void Widen(Vector<sbyte> source, out Vector<short> low, out Vector<short> high)
		{
			int count = Vector<sbyte>.Count;
			short* ptr = stackalloc short[checked(unchecked((UIntPtr)(count / 2)) * 2)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (short)source[i];
			}
			short* ptr2 = stackalloc short[checked(unchecked((UIntPtr)(count / 2)) * 2)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (short)source[j + count / 2];
			}
			low = new Vector<short>((void*)ptr);
			high = new Vector<short>((void*)ptr2);
		}

		// Token: 0x06001D3A RID: 7482 RVA: 0x0011992C File Offset: 0x00118B2C
		[Intrinsic]
		public unsafe static void Widen(Vector<short> source, out Vector<int> low, out Vector<int> high)
		{
			int count = Vector<short>.Count;
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)(count / 2)) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (int)source[i];
			}
			int* ptr2 = stackalloc int[checked(unchecked((UIntPtr)(count / 2)) * 4)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (int)source[j + count / 2];
			}
			low = new Vector<int>((void*)ptr);
			high = new Vector<int>((void*)ptr2);
		}

		// Token: 0x06001D3B RID: 7483 RVA: 0x001199B0 File Offset: 0x00118BB0
		[Intrinsic]
		public unsafe static void Widen(Vector<int> source, out Vector<long> low, out Vector<long> high)
		{
			int count = Vector<int>.Count;
			long* ptr = stackalloc long[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (long)source[i];
			}
			long* ptr2 = stackalloc long[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (long)source[j + count / 2];
			}
			low = new Vector<long>((void*)ptr);
			high = new Vector<long>((void*)ptr2);
		}

		// Token: 0x06001D3C RID: 7484 RVA: 0x00119A34 File Offset: 0x00118C34
		[Intrinsic]
		public unsafe static void Widen(Vector<float> source, out Vector<double> low, out Vector<double> high)
		{
			int count = Vector<float>.Count;
			double* ptr = stackalloc double[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (double)source[i];
			}
			double* ptr2 = stackalloc double[checked(unchecked((UIntPtr)(count / 2)) * 8)];
			for (int j = 0; j < count / 2; j++)
			{
				ptr2[j] = (double)source[j + count / 2];
			}
			low = new Vector<double>((void*)ptr);
			high = new Vector<double>((void*)ptr2);
		}

		// Token: 0x06001D3D RID: 7485 RVA: 0x00119AB8 File Offset: 0x00118CB8
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<byte> Narrow(Vector<ushort> low, Vector<ushort> high)
		{
			int count = Vector<byte>.Count;
			byte* ptr = stackalloc byte[(UIntPtr)count];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (byte)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (byte)high[j];
			}
			return new Vector<byte>((void*)ptr);
		}

		// Token: 0x06001D3E RID: 7486 RVA: 0x00119B10 File Offset: 0x00118D10
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<ushort> Narrow(Vector<uint> low, Vector<uint> high)
		{
			int count = Vector<ushort>.Count;
			ushort* ptr = stackalloc ushort[checked(unchecked((UIntPtr)count) * 2)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (ushort)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (ushort)high[j];
			}
			return new Vector<ushort>((void*)ptr);
		}

		// Token: 0x06001D3F RID: 7487 RVA: 0x00119B70 File Offset: 0x00118D70
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<uint> Narrow(Vector<ulong> low, Vector<ulong> high)
		{
			int count = Vector<uint>.Count;
			uint* ptr = stackalloc uint[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (uint)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (uint)high[j];
			}
			return new Vector<uint>((void*)ptr);
		}

		// Token: 0x06001D40 RID: 7488 RVA: 0x00119BD0 File Offset: 0x00118DD0
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<sbyte> Narrow(Vector<short> low, Vector<short> high)
		{
			int count = Vector<sbyte>.Count;
			sbyte* ptr = stackalloc sbyte[(UIntPtr)count];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (sbyte)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (sbyte)high[j];
			}
			return new Vector<sbyte>((void*)ptr);
		}

		// Token: 0x06001D41 RID: 7489 RVA: 0x00119C28 File Offset: 0x00118E28
		[Intrinsic]
		public unsafe static Vector<short> Narrow(Vector<int> low, Vector<int> high)
		{
			int count = Vector<short>.Count;
			short* ptr = stackalloc short[checked(unchecked((UIntPtr)count) * 2)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (short)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (short)high[j];
			}
			return new Vector<short>((void*)ptr);
		}

		// Token: 0x06001D42 RID: 7490 RVA: 0x00119C88 File Offset: 0x00118E88
		[Intrinsic]
		public unsafe static Vector<int> Narrow(Vector<long> low, Vector<long> high)
		{
			int count = Vector<int>.Count;
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (int)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (int)high[j];
			}
			return new Vector<int>((void*)ptr);
		}

		// Token: 0x06001D43 RID: 7491 RVA: 0x00119CE8 File Offset: 0x00118EE8
		[Intrinsic]
		public unsafe static Vector<float> Narrow(Vector<double> low, Vector<double> high)
		{
			int count = Vector<float>.Count;
			float* ptr = stackalloc float[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count / 2; i++)
			{
				ptr[i] = (float)low[i];
			}
			for (int j = 0; j < count / 2; j++)
			{
				ptr[j + count / 2] = (float)high[j];
			}
			return new Vector<float>((void*)ptr);
		}

		// Token: 0x06001D44 RID: 7492 RVA: 0x00119D48 File Offset: 0x00118F48
		[Intrinsic]
		public unsafe static Vector<float> ConvertToSingle(Vector<int> value)
		{
			int count = Vector<float>.Count;
			float* ptr = stackalloc float[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (float)value[i];
			}
			return new Vector<float>((void*)ptr);
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x00119D84 File Offset: 0x00118F84
		[Intrinsic]
		[CLSCompliant(false)]
		public unsafe static Vector<float> ConvertToSingle(Vector<uint> value)
		{
			int count = Vector<float>.Count;
			float* ptr = stackalloc float[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = value[i];
			}
			return new Vector<float>((void*)ptr);
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00119DC4 File Offset: 0x00118FC4
		[Intrinsic]
		public unsafe static Vector<double> ConvertToDouble(Vector<long> value)
		{
			int count = Vector<double>.Count;
			double* ptr = stackalloc double[checked(unchecked((UIntPtr)count) * 8)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (double)value[i];
			}
			return new Vector<double>((void*)ptr);
		}

		// Token: 0x06001D47 RID: 7495 RVA: 0x00119E00 File Offset: 0x00119000
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<double> ConvertToDouble(Vector<ulong> value)
		{
			int count = Vector<double>.Count;
			double* ptr = stackalloc double[checked(unchecked((UIntPtr)count) * 8)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = value[i];
			}
			return new Vector<double>((void*)ptr);
		}

		// Token: 0x06001D48 RID: 7496 RVA: 0x00119E40 File Offset: 0x00119040
		[Intrinsic]
		public unsafe static Vector<int> ConvertToInt32(Vector<float> value)
		{
			int count = Vector<int>.Count;
			int* ptr = stackalloc int[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (int)value[i];
			}
			return new Vector<int>((void*)ptr);
		}

		// Token: 0x06001D49 RID: 7497 RVA: 0x00119E7C File Offset: 0x0011907C
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<uint> ConvertToUInt32(Vector<float> value)
		{
			int count = Vector<uint>.Count;
			uint* ptr = stackalloc uint[checked(unchecked((UIntPtr)count) * 4)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (uint)value[i];
			}
			return new Vector<uint>((void*)ptr);
		}

		// Token: 0x06001D4A RID: 7498 RVA: 0x00119EB8 File Offset: 0x001190B8
		[Intrinsic]
		public unsafe static Vector<long> ConvertToInt64(Vector<double> value)
		{
			int count = Vector<long>.Count;
			long* ptr = stackalloc long[checked(unchecked((UIntPtr)count) * 8)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (long)value[i];
			}
			return new Vector<long>((void*)ptr);
		}

		// Token: 0x06001D4B RID: 7499 RVA: 0x00119EF4 File Offset: 0x001190F4
		[CLSCompliant(false)]
		[Intrinsic]
		public unsafe static Vector<ulong> ConvertToUInt64(Vector<double> value)
		{
			int count = Vector<ulong>.Count;
			ulong* ptr = stackalloc ulong[checked(unchecked((UIntPtr)count) * 8)];
			for (int i = 0; i < count; i++)
			{
				ptr[i] = (ulong)value[i];
			}
			return new Vector<ulong>((void*)ptr);
		}

		// Token: 0x06001D4C RID: 7500 RVA: 0x00119F30 File Offset: 0x00119130
		[DoesNotReturn]
		internal static void ThrowInsufficientNumberOfElementsException(int requiredElementCount)
		{
			throw new IndexOutOfRangeException(SR.Format(SR.Arg_InsufficientNumberOfElements, requiredElementCount, "values"));
		}

		// Token: 0x06001D4D RID: 7501 RVA: 0x00119F4C File Offset: 0x0011914C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<float> ConditionalSelect(Vector<int> condition, Vector<float> left, Vector<float> right)
		{
			return Vector<float>.ConditionalSelect((Vector<float>)condition, left, right);
		}

		// Token: 0x06001D4E RID: 7502 RVA: 0x00119F5B File Offset: 0x0011915B
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<double> ConditionalSelect(Vector<long> condition, Vector<double> left, Vector<double> right)
		{
			return Vector<double>.ConditionalSelect((Vector<double>)condition, left, right);
		}

		// Token: 0x06001D4F RID: 7503 RVA: 0x00119F6A File Offset: 0x0011916A
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> ConditionalSelect<T>(Vector<T> condition, Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.ConditionalSelect(condition, left, right);
		}

		// Token: 0x06001D50 RID: 7504 RVA: 0x00119F74 File Offset: 0x00119174
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Equals<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.Equals(left, right);
		}

		// Token: 0x06001D51 RID: 7505 RVA: 0x00119F7D File Offset: 0x0011917D
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> Equals(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.Equals(left, right);
		}

		// Token: 0x06001D52 RID: 7506 RVA: 0x00119F8B File Offset: 0x0011918B
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> Equals(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.Equals(left, right);
		}

		// Token: 0x06001D53 RID: 7507 RVA: 0x00119F94 File Offset: 0x00119194
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> Equals(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.Equals(left, right);
		}

		// Token: 0x06001D54 RID: 7508 RVA: 0x00119FA2 File Offset: 0x001191A2
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> Equals(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.Equals(left, right);
		}

		// Token: 0x06001D55 RID: 7509 RVA: 0x00119FAB File Offset: 0x001191AB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EqualsAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left == right;
		}

		// Token: 0x06001D56 RID: 7510 RVA: 0x00119FB4 File Offset: 0x001191B4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool EqualsAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !Vector<T>.Equals(left, right).Equals(Vector<T>.Zero);
		}

		// Token: 0x06001D57 RID: 7511 RVA: 0x00119FD8 File Offset: 0x001191D8
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> LessThan<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.LessThan(left, right);
		}

		// Token: 0x06001D58 RID: 7512 RVA: 0x00119FE1 File Offset: 0x001191E1
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> LessThan(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.LessThan(left, right);
		}

		// Token: 0x06001D59 RID: 7513 RVA: 0x00119FEF File Offset: 0x001191EF
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> LessThan(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.LessThan(left, right);
		}

		// Token: 0x06001D5A RID: 7514 RVA: 0x00119FF8 File Offset: 0x001191F8
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> LessThan(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.LessThan(left, right);
		}

		// Token: 0x06001D5B RID: 7515 RVA: 0x0011A006 File Offset: 0x00119206
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> LessThan(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.LessThan(left, right);
		}

		// Token: 0x06001D5C RID: 7516 RVA: 0x0011A010 File Offset: 0x00119210
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool LessThanAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return ((Vector<int>)Vector<T>.LessThan(left, right)).Equals(Vector<int>.AllBitsSet);
		}

		// Token: 0x06001D5D RID: 7517 RVA: 0x0011A038 File Offset: 0x00119238
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool LessThanAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !((Vector<int>)Vector<T>.LessThan(left, right)).Equals(Vector<int>.Zero);
		}

		// Token: 0x06001D5E RID: 7518 RVA: 0x0011A061 File Offset: 0x00119261
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> LessThanOrEqual<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.LessThanOrEqual(left, right);
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x0011A06A File Offset: 0x0011926A
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> LessThanOrEqual(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.LessThanOrEqual(left, right);
		}

		// Token: 0x06001D60 RID: 7520 RVA: 0x0011A078 File Offset: 0x00119278
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> LessThanOrEqual(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.LessThanOrEqual(left, right);
		}

		// Token: 0x06001D61 RID: 7521 RVA: 0x0011A081 File Offset: 0x00119281
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> LessThanOrEqual(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.LessThanOrEqual(left, right);
		}

		// Token: 0x06001D62 RID: 7522 RVA: 0x0011A08A File Offset: 0x0011928A
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> LessThanOrEqual(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.LessThanOrEqual(left, right);
		}

		// Token: 0x06001D63 RID: 7523 RVA: 0x0011A098 File Offset: 0x00119298
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool LessThanOrEqualAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return ((Vector<int>)Vector<T>.LessThanOrEqual(left, right)).Equals(Vector<int>.AllBitsSet);
		}

		// Token: 0x06001D64 RID: 7524 RVA: 0x0011A0C0 File Offset: 0x001192C0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool LessThanOrEqualAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !((Vector<int>)Vector<T>.LessThanOrEqual(left, right)).Equals(Vector<int>.Zero);
		}

		// Token: 0x06001D65 RID: 7525 RVA: 0x0011A0E9 File Offset: 0x001192E9
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> GreaterThan<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.GreaterThan(left, right);
		}

		// Token: 0x06001D66 RID: 7526 RVA: 0x0011A0F2 File Offset: 0x001192F2
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> GreaterThan(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.GreaterThan(left, right);
		}

		// Token: 0x06001D67 RID: 7527 RVA: 0x0011A100 File Offset: 0x00119300
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> GreaterThan(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.GreaterThan(left, right);
		}

		// Token: 0x06001D68 RID: 7528 RVA: 0x0011A109 File Offset: 0x00119309
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> GreaterThan(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.GreaterThan(left, right);
		}

		// Token: 0x06001D69 RID: 7529 RVA: 0x0011A117 File Offset: 0x00119317
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> GreaterThan(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.GreaterThan(left, right);
		}

		// Token: 0x06001D6A RID: 7530 RVA: 0x0011A120 File Offset: 0x00119320
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GreaterThanAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return ((Vector<int>)Vector<T>.GreaterThan(left, right)).Equals(Vector<int>.AllBitsSet);
		}

		// Token: 0x06001D6B RID: 7531 RVA: 0x0011A148 File Offset: 0x00119348
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GreaterThanAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !((Vector<int>)Vector<T>.GreaterThan(left, right)).Equals(Vector<int>.Zero);
		}

		// Token: 0x06001D6C RID: 7532 RVA: 0x0011A171 File Offset: 0x00119371
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> GreaterThanOrEqual<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06001D6D RID: 7533 RVA: 0x0011A17A File Offset: 0x0011937A
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> GreaterThanOrEqual(Vector<float> left, Vector<float> right)
		{
			return (Vector<int>)Vector<float>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06001D6E RID: 7534 RVA: 0x0011A188 File Offset: 0x00119388
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> GreaterThanOrEqual(Vector<int> left, Vector<int> right)
		{
			return Vector<int>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06001D6F RID: 7535 RVA: 0x0011A191 File Offset: 0x00119391
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> GreaterThanOrEqual(Vector<long> left, Vector<long> right)
		{
			return Vector<long>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x0011A19A File Offset: 0x0011939A
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> GreaterThanOrEqual(Vector<double> left, Vector<double> right)
		{
			return (Vector<long>)Vector<double>.GreaterThanOrEqual(left, right);
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x0011A1A8 File Offset: 0x001193A8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GreaterThanOrEqualAll<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return ((Vector<int>)Vector<T>.GreaterThanOrEqual(left, right)).Equals(Vector<int>.AllBitsSet);
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x0011A1D0 File Offset: 0x001193D0
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool GreaterThanOrEqualAny<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return !((Vector<int>)Vector<T>.GreaterThanOrEqual(left, right)).Equals(Vector<int>.Zero);
		}

		// Token: 0x1700067A RID: 1658
		// (get) Token: 0x06001D73 RID: 7539 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsHardwareAccelerated
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06001D74 RID: 7540 RVA: 0x0011A1F9 File Offset: 0x001193F9
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Abs<T>(Vector<T> value) where T : struct
		{
			return Vector<T>.Abs(value);
		}

		// Token: 0x06001D75 RID: 7541 RVA: 0x0011A201 File Offset: 0x00119401
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Min<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.Min(left, right);
		}

		// Token: 0x06001D76 RID: 7542 RVA: 0x0011A20A File Offset: 0x0011940A
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Max<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.Max(left, right);
		}

		// Token: 0x06001D77 RID: 7543 RVA: 0x0011A213 File Offset: 0x00119413
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static T Dot<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return Vector<T>.Dot(left, right);
		}

		// Token: 0x06001D78 RID: 7544 RVA: 0x0011A21C File Offset: 0x0011941C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> SquareRoot<T>(Vector<T> value) where T : struct
		{
			return Vector<T>.SquareRoot(value);
		}

		// Token: 0x06001D79 RID: 7545 RVA: 0x0011A224 File Offset: 0x00119424
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<float> Ceiling(Vector<float> value)
		{
			return Vector<float>.Ceiling(value);
		}

		// Token: 0x06001D7A RID: 7546 RVA: 0x0011A22C File Offset: 0x0011942C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<double> Ceiling(Vector<double> value)
		{
			return Vector<double>.Ceiling(value);
		}

		// Token: 0x06001D7B RID: 7547 RVA: 0x0011A234 File Offset: 0x00119434
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<float> Floor(Vector<float> value)
		{
			return Vector<float>.Floor(value);
		}

		// Token: 0x06001D7C RID: 7548 RVA: 0x0011A23C File Offset: 0x0011943C
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<double> Floor(Vector<double> value)
		{
			return Vector<double>.Floor(value);
		}

		// Token: 0x06001D7D RID: 7549 RVA: 0x0011A244 File Offset: 0x00119444
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Add<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left + right;
		}

		// Token: 0x06001D7E RID: 7550 RVA: 0x0011A24D File Offset: 0x0011944D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Subtract<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left - right;
		}

		// Token: 0x06001D7F RID: 7551 RVA: 0x0011A256 File Offset: 0x00119456
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Multiply<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left * right;
		}

		// Token: 0x06001D80 RID: 7552 RVA: 0x0011A25F File Offset: 0x0011945F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Multiply<T>(Vector<T> left, T right) where T : struct
		{
			return left * right;
		}

		// Token: 0x06001D81 RID: 7553 RVA: 0x0011A268 File Offset: 0x00119468
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Multiply<T>(T left, Vector<T> right) where T : struct
		{
			return left * right;
		}

		// Token: 0x06001D82 RID: 7554 RVA: 0x0011A271 File Offset: 0x00119471
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Divide<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left / right;
		}

		// Token: 0x06001D83 RID: 7555 RVA: 0x0011A27A File Offset: 0x0011947A
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Negate<T>(Vector<T> value) where T : struct
		{
			return -value;
		}

		// Token: 0x06001D84 RID: 7556 RVA: 0x0011A282 File Offset: 0x00119482
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> BitwiseAnd<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left & right;
		}

		// Token: 0x06001D85 RID: 7557 RVA: 0x0011A28B File Offset: 0x0011948B
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> BitwiseOr<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left | right;
		}

		// Token: 0x06001D86 RID: 7558 RVA: 0x0011A294 File Offset: 0x00119494
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> OnesComplement<T>(Vector<T> value) where T : struct
		{
			return ~value;
		}

		// Token: 0x06001D87 RID: 7559 RVA: 0x0011A29C File Offset: 0x0011949C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> Xor<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left ^ right;
		}

		// Token: 0x06001D88 RID: 7560 RVA: 0x0011A2A5 File Offset: 0x001194A5
		[Intrinsic]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<T> AndNot<T>(Vector<T> left, Vector<T> right) where T : struct
		{
			return left & ~right;
		}

		// Token: 0x06001D89 RID: 7561 RVA: 0x0011A2B3 File Offset: 0x001194B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<byte> AsVectorByte<T>(Vector<T> value) where T : struct
		{
			return (Vector<byte>)value;
		}

		// Token: 0x06001D8A RID: 7562 RVA: 0x0011A2BB File Offset: 0x001194BB
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<sbyte> AsVectorSByte<T>(Vector<T> value) where T : struct
		{
			return (Vector<sbyte>)value;
		}

		// Token: 0x06001D8B RID: 7563 RVA: 0x0011A2C3 File Offset: 0x001194C3
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<ushort> AsVectorUInt16<T>(Vector<T> value) where T : struct
		{
			return (Vector<ushort>)value;
		}

		// Token: 0x06001D8C RID: 7564 RVA: 0x0011A2CB File Offset: 0x001194CB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<short> AsVectorInt16<T>(Vector<T> value) where T : struct
		{
			return (Vector<short>)value;
		}

		// Token: 0x06001D8D RID: 7565 RVA: 0x0011A2D3 File Offset: 0x001194D3
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<uint> AsVectorUInt32<T>(Vector<T> value) where T : struct
		{
			return (Vector<uint>)value;
		}

		// Token: 0x06001D8E RID: 7566 RVA: 0x0011A2DB File Offset: 0x001194DB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<int> AsVectorInt32<T>(Vector<T> value) where T : struct
		{
			return (Vector<int>)value;
		}

		// Token: 0x06001D8F RID: 7567 RVA: 0x0011A2E3 File Offset: 0x001194E3
		[CLSCompliant(false)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<ulong> AsVectorUInt64<T>(Vector<T> value) where T : struct
		{
			return (Vector<ulong>)value;
		}

		// Token: 0x06001D90 RID: 7568 RVA: 0x0011A2EB File Offset: 0x001194EB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<long> AsVectorInt64<T>(Vector<T> value) where T : struct
		{
			return (Vector<long>)value;
		}

		// Token: 0x06001D91 RID: 7569 RVA: 0x0011A2F3 File Offset: 0x001194F3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<float> AsVectorSingle<T>(Vector<T> value) where T : struct
		{
			return (Vector<float>)value;
		}

		// Token: 0x06001D92 RID: 7570 RVA: 0x0011A2FB File Offset: 0x001194FB
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Vector<double> AsVectorDouble<T>(Vector<T> value) where T : struct
		{
			return (Vector<double>)value;
		}
	}
}
