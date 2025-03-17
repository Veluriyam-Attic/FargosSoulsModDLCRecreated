using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Collections
{
	// Token: 0x020007AD RID: 1965
	internal static class HashHelpers
	{
		// Token: 0x06005F21 RID: 24353 RVA: 0x001C89DC File Offset: 0x001C7BDC
		public static bool IsPrime(int candidate)
		{
			if ((candidate & 1) != 0)
			{
				int num = (int)Math.Sqrt((double)candidate);
				for (int i = 3; i <= num; i += 2)
				{
					if (candidate % i == 0)
					{
						return false;
					}
				}
				return true;
			}
			return candidate == 2;
		}

		// Token: 0x06005F22 RID: 24354 RVA: 0x001C8A10 File Offset: 0x001C7C10
		public static int GetPrime(int min)
		{
			if (min < 0)
			{
				throw new ArgumentException(SR.Arg_HTCapacityOverflow);
			}
			foreach (int num in HashHelpers.s_primes)
			{
				if (num >= min)
				{
					return num;
				}
			}
			for (int j = min | 1; j < 2147483647; j += 2)
			{
				if (HashHelpers.IsPrime(j) && (j - 1) % 101 != 0)
				{
					return j;
				}
			}
			return min;
		}

		// Token: 0x06005F23 RID: 24355 RVA: 0x001C8A70 File Offset: 0x001C7C70
		public static int ExpandPrime(int oldSize)
		{
			int num = 2 * oldSize;
			if (num > 2146435069 && 2146435069 > oldSize)
			{
				return 2146435069;
			}
			return HashHelpers.GetPrime(num);
		}

		// Token: 0x06005F24 RID: 24356 RVA: 0x001C8A9D File Offset: 0x001C7C9D
		public static ulong GetFastModMultiplier(uint divisor)
		{
			return ulong.MaxValue / (ulong)divisor + 1UL;
		}

		// Token: 0x06005F25 RID: 24357 RVA: 0x001C8AA8 File Offset: 0x001C7CA8
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static uint FastMod(uint value, uint divisor, ulong multiplier)
		{
			return (uint)(((multiplier * (ulong)value >> 32) + 1UL) * (ulong)divisor >> 32);
		}

		// Token: 0x17000F8C RID: 3980
		// (get) Token: 0x06005F26 RID: 24358 RVA: 0x001C8AC8 File Offset: 0x001C7CC8
		public static ConditionalWeakTable<object, SerializationInfo> SerializationInfoTable
		{
			get
			{
				if (HashHelpers.s_serializationInfoTable == null)
				{
					Interlocked.CompareExchange<ConditionalWeakTable<object, SerializationInfo>>(ref HashHelpers.s_serializationInfoTable, new ConditionalWeakTable<object, SerializationInfo>(), null);
				}
				return HashHelpers.s_serializationInfoTable;
			}
		}

		// Token: 0x04001CB9 RID: 7353
		private static readonly int[] s_primes = new int[]
		{
			3,
			7,
			11,
			17,
			23,
			29,
			37,
			47,
			59,
			71,
			89,
			107,
			131,
			163,
			197,
			239,
			293,
			353,
			431,
			521,
			631,
			761,
			919,
			1103,
			1327,
			1597,
			1931,
			2333,
			2801,
			3371,
			4049,
			4861,
			5839,
			7013,
			8419,
			10103,
			12143,
			14591,
			17519,
			21023,
			25229,
			30293,
			36353,
			43627,
			52361,
			62851,
			75431,
			90523,
			108631,
			130363,
			156437,
			187751,
			225307,
			270371,
			324449,
			389357,
			467237,
			560689,
			672827,
			807403,
			968897,
			1162687,
			1395263,
			1674319,
			2009191,
			2411033,
			2893249,
			3471899,
			4166287,
			4999559,
			5999471,
			7199369
		};

		// Token: 0x04001CBA RID: 7354
		private static ConditionalWeakTable<object, SerializationInfo> s_serializationInfoTable;
	}
}
