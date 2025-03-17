using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000124 RID: 292
	[NullableContext(1)]
	[Nullable(0)]
	public struct HashCode
	{
		// Token: 0x06000F3A RID: 3898 RVA: 0x000D9AA8 File Offset: 0x000D8CA8
		private unsafe static uint GenerateGlobalSeed()
		{
			uint result;
			Interop.GetRandomBytes((byte*)(&result), 4);
			return result;
		}

		// Token: 0x06000F3B RID: 3899 RVA: 0x000D9AC0 File Offset: 0x000D8CC0
		public static int Combine<[Nullable(2)] T1>(T1 value1)
		{
			uint queuedValue = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint num = HashCode.MixEmptyState();
			num += 4U;
			num = HashCode.QueueRound(num, queuedValue);
			return (int)HashCode.MixFinal(num);
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x000D9B00 File Offset: 0x000D8D00
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2>(T1 value1, T2 value2)
		{
			uint queuedValue = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint queuedValue2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint num = HashCode.MixEmptyState();
			num += 8U;
			num = HashCode.QueueRound(num, queuedValue);
			num = HashCode.QueueRound(num, queuedValue2);
			return (int)HashCode.MixFinal(num);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x000D9B64 File Offset: 0x000D8D64
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(T1 value1, T2 value2, T3 value3)
		{
			uint queuedValue = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint queuedValue2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint queuedValue3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint num = HashCode.MixEmptyState();
			num += 12U;
			num = HashCode.QueueRound(num, queuedValue);
			num = HashCode.QueueRound(num, queuedValue2);
			num = HashCode.QueueRound(num, queuedValue3);
			return (int)HashCode.MixFinal(num);
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x000D9BE8 File Offset: 0x000D8DE8
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>(T1 value1, T2 value2, T3 value3, T4 value4)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			uint num5 = HashCode.MixState(num, num2, num3, num4);
			num5 += 16U;
			return (int)HashCode.MixFinal(num5);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x000D9CB0 File Offset: 0x000D8EB0
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint queuedValue = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			uint num5 = HashCode.MixState(num, num2, num3, num4);
			num5 += 20U;
			num5 = HashCode.QueueRound(num5, queuedValue);
			return (int)HashCode.MixFinal(num5);
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x000D9DA0 File Offset: 0x000D8FA0
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint queuedValue = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint queuedValue2 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			uint num5 = HashCode.MixState(num, num2, num3, num4);
			num5 += 24U;
			num5 = HashCode.QueueRound(num5, queuedValue);
			num5 = HashCode.QueueRound(num5, queuedValue2);
			return (int)HashCode.MixFinal(num5);
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x000D9EB4 File Offset: 0x000D90B4
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint queuedValue = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint queuedValue2 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint queuedValue3 = (uint)((value7 != null) ? value7.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			uint num5 = HashCode.MixState(num, num2, num3, num4);
			num5 += 28U;
			num5 = HashCode.QueueRound(num5, queuedValue);
			num5 = HashCode.QueueRound(num5, queuedValue2);
			num5 = HashCode.QueueRound(num5, queuedValue3);
			return (int)HashCode.MixFinal(num5);
		}

		// Token: 0x06000F42 RID: 3906 RVA: 0x000D9FF0 File Offset: 0x000D91F0
		public static int Combine<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>(T1 value1, T2 value2, T3 value3, T4 value4, T5 value5, T6 value6, T7 value7, T8 value8)
		{
			uint input = (uint)((value1 != null) ? value1.GetHashCode() : 0);
			uint input2 = (uint)((value2 != null) ? value2.GetHashCode() : 0);
			uint input3 = (uint)((value3 != null) ? value3.GetHashCode() : 0);
			uint input4 = (uint)((value4 != null) ? value4.GetHashCode() : 0);
			uint input5 = (uint)((value5 != null) ? value5.GetHashCode() : 0);
			uint input6 = (uint)((value6 != null) ? value6.GetHashCode() : 0);
			uint input7 = (uint)((value7 != null) ? value7.GetHashCode() : 0);
			uint input8 = (uint)((value8 != null) ? value8.GetHashCode() : 0);
			uint num;
			uint num2;
			uint num3;
			uint num4;
			HashCode.Initialize(out num, out num2, out num3, out num4);
			num = HashCode.Round(num, input);
			num2 = HashCode.Round(num2, input2);
			num3 = HashCode.Round(num3, input3);
			num4 = HashCode.Round(num4, input4);
			num = HashCode.Round(num, input5);
			num2 = HashCode.Round(num2, input6);
			num3 = HashCode.Round(num3, input7);
			num4 = HashCode.Round(num4, input8);
			uint num5 = HashCode.MixState(num, num2, num3, num4);
			num5 += 32U;
			return (int)HashCode.MixFinal(num5);
		}

		// Token: 0x06000F43 RID: 3907 RVA: 0x000DA14F File Offset: 0x000D934F
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static void Initialize(out uint v1, out uint v2, out uint v3, out uint v4)
		{
			v1 = HashCode.s_seed + 2654435761U + 2246822519U;
			v2 = HashCode.s_seed + 2246822519U;
			v3 = HashCode.s_seed;
			v4 = HashCode.s_seed - 2654435761U;
		}

		// Token: 0x06000F44 RID: 3908 RVA: 0x000DA185 File Offset: 0x000D9385
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint Round(uint hash, uint input)
		{
			return BitOperations.RotateLeft(hash + input * 2246822519U, 13) * 2654435761U;
		}

		// Token: 0x06000F45 RID: 3909 RVA: 0x000DA19D File Offset: 0x000D939D
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint QueueRound(uint hash, uint queuedValue)
		{
			return BitOperations.RotateLeft(hash + queuedValue * 3266489917U, 17) * 668265263U;
		}

		// Token: 0x06000F46 RID: 3910 RVA: 0x000DA1B5 File Offset: 0x000D93B5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixState(uint v1, uint v2, uint v3, uint v4)
		{
			return BitOperations.RotateLeft(v1, 1) + BitOperations.RotateLeft(v2, 7) + BitOperations.RotateLeft(v3, 12) + BitOperations.RotateLeft(v4, 18);
		}

		// Token: 0x06000F47 RID: 3911 RVA: 0x000DA1D8 File Offset: 0x000D93D8
		private static uint MixEmptyState()
		{
			return HashCode.s_seed + 374761393U;
		}

		// Token: 0x06000F48 RID: 3912 RVA: 0x000DA1E5 File Offset: 0x000D93E5
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static uint MixFinal(uint hash)
		{
			hash ^= hash >> 15;
			hash *= 2246822519U;
			hash ^= hash >> 13;
			hash *= 3266489917U;
			hash ^= hash >> 16;
			return hash;
		}

		// Token: 0x06000F49 RID: 3913 RVA: 0x000DA212 File Offset: 0x000D9412
		public void Add<[Nullable(2)] T>(T value)
		{
			this.Add((value != null) ? value.GetHashCode() : 0);
		}

		// Token: 0x06000F4A RID: 3914 RVA: 0x000DA232 File Offset: 0x000D9432
		public void Add<[Nullable(2)] T>(T value, [Nullable(new byte[]
		{
			2,
			1
		})] IEqualityComparer<T> comparer)
		{
			this.Add((value == null) ? 0 : ((comparer != null) ? comparer.GetHashCode(value) : value.GetHashCode()));
		}

		// Token: 0x06000F4B RID: 3915 RVA: 0x000DA260 File Offset: 0x000D9460
		private void Add(int value)
		{
			uint length = this._length;
			this._length = length + 1U;
			uint num = length;
			uint num2 = num % 4U;
			if (num2 == 0U)
			{
				this._queue1 = (uint)value;
				return;
			}
			if (num2 == 1U)
			{
				this._queue2 = (uint)value;
				return;
			}
			if (num2 == 2U)
			{
				this._queue3 = (uint)value;
				return;
			}
			if (num == 3U)
			{
				HashCode.Initialize(out this._v1, out this._v2, out this._v3, out this._v4);
			}
			this._v1 = HashCode.Round(this._v1, this._queue1);
			this._v2 = HashCode.Round(this._v2, this._queue2);
			this._v3 = HashCode.Round(this._v3, this._queue3);
			this._v4 = HashCode.Round(this._v4, (uint)value);
		}

		// Token: 0x06000F4C RID: 3916 RVA: 0x000DA320 File Offset: 0x000D9520
		public int ToHashCode()
		{
			uint length = this._length;
			uint num = length % 4U;
			uint num2 = (length < 4U) ? HashCode.MixEmptyState() : HashCode.MixState(this._v1, this._v2, this._v3, this._v4);
			num2 += length * 4U;
			if (num > 0U)
			{
				num2 = HashCode.QueueRound(num2, this._queue1);
				if (num > 1U)
				{
					num2 = HashCode.QueueRound(num2, this._queue2);
					if (num > 2U)
					{
						num2 = HashCode.QueueRound(num2, this._queue3);
					}
				}
			}
			return (int)HashCode.MixFinal(num2);
		}

		// Token: 0x06000F4D RID: 3917 RVA: 0x000DA3A2 File Offset: 0x000D95A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes. Use ToHashCode to retrieve the computed hash code.", true)]
		public override int GetHashCode()
		{
			throw new NotSupportedException(SR.HashCode_HashCodeNotSupported);
		}

		// Token: 0x06000F4E RID: 3918 RVA: 0x000DA3AE File Offset: 0x000D95AE
		[Obsolete("HashCode is a mutable struct and should not be compared with other HashCodes.", true)]
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			throw new NotSupportedException(SR.HashCode_EqualityNotSupported);
		}

		// Token: 0x040003E0 RID: 992
		private static readonly uint s_seed = HashCode.GenerateGlobalSeed();

		// Token: 0x040003E1 RID: 993
		private uint _v1;

		// Token: 0x040003E2 RID: 994
		private uint _v2;

		// Token: 0x040003E3 RID: 995
		private uint _v3;

		// Token: 0x040003E4 RID: 996
		private uint _v4;

		// Token: 0x040003E5 RID: 997
		private uint _queue1;

		// Token: 0x040003E6 RID: 998
		private uint _queue2;

		// Token: 0x040003E7 RID: 999
		private uint _queue3;

		// Token: 0x040003E8 RID: 1000
		private uint _length;
	}
}
