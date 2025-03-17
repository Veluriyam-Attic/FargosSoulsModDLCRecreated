using System;
using System.Runtime.CompilerServices;

namespace System.Numerics
{
	// Token: 0x020001D3 RID: 467
	internal static class ConstantHelper
	{
		// Token: 0x06001CE6 RID: 7398 RVA: 0x0010AC5C File Offset: 0x00109E5C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static byte GetByteWithAllBitsSet()
		{
			byte result = 0;
			*(&result) = byte.MaxValue;
			return result;
		}

		// Token: 0x06001CE7 RID: 7399 RVA: 0x0010AC78 File Offset: 0x00109E78
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static sbyte GetSByteWithAllBitsSet()
		{
			sbyte result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x06001CE8 RID: 7400 RVA: 0x0010AC90 File Offset: 0x00109E90
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ushort GetUInt16WithAllBitsSet()
		{
			ushort result = 0;
			*(&result) = ushort.MaxValue;
			return result;
		}

		// Token: 0x06001CE9 RID: 7401 RVA: 0x0010ACAC File Offset: 0x00109EAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static short GetInt16WithAllBitsSet()
		{
			short result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x06001CEA RID: 7402 RVA: 0x0010ACC4 File Offset: 0x00109EC4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static uint GetUInt32WithAllBitsSet()
		{
			uint result = 0U;
			*(&result) = uint.MaxValue;
			return result;
		}

		// Token: 0x06001CEB RID: 7403 RVA: 0x0010ACDC File Offset: 0x00109EDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static int GetInt32WithAllBitsSet()
		{
			int result = 0;
			*(&result) = -1;
			return result;
		}

		// Token: 0x06001CEC RID: 7404 RVA: 0x0010ACF4 File Offset: 0x00109EF4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static ulong GetUInt64WithAllBitsSet()
		{
			ulong result = 0UL;
			*(&result) = ulong.MaxValue;
			return result;
		}

		// Token: 0x06001CED RID: 7405 RVA: 0x0010AD0C File Offset: 0x00109F0C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static long GetInt64WithAllBitsSet()
		{
			long result = 0L;
			*(&result) = -1L;
			return result;
		}

		// Token: 0x06001CEE RID: 7406 RVA: 0x0010AD24 File Offset: 0x00109F24
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static float GetSingleWithAllBitsSet()
		{
			float result = 0f;
			*(int*)(&result) = -1;
			return result;
		}

		// Token: 0x06001CEF RID: 7407 RVA: 0x0010AD40 File Offset: 0x00109F40
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public unsafe static double GetDoubleWithAllBitsSet()
		{
			double result = 0.0;
			*(long*)(&result) = -1L;
			return result;
		}
	}
}
