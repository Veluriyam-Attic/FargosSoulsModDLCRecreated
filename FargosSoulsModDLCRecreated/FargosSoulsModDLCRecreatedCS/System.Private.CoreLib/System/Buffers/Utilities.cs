using System;
using System.Numerics;
using System.Runtime.CompilerServices;

namespace System.Buffers
{
	// Token: 0x02000255 RID: 597
	internal static class Utilities
	{
		// Token: 0x0600246C RID: 9324 RVA: 0x0013A010 File Offset: 0x00139210
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int SelectBucketIndex(int bufferSize)
		{
			return BitOperations.Log2((uint)(bufferSize - 1 | 15)) - 3;
		}

		// Token: 0x0600246D RID: 9325 RVA: 0x0013A020 File Offset: 0x00139220
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static int GetMaxSizeForBucket(int binIndex)
		{
			return 16 << binIndex;
		}
	}
}
