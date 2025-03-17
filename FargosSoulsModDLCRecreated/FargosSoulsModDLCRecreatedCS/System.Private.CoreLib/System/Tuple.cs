using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001A2 RID: 418
	[NullableContext(1)]
	[Nullable(0)]
	public static class Tuple
	{
		// Token: 0x0600195B RID: 6491 RVA: 0x000F8534 File Offset: 0x000F7734
		public static Tuple<T1> Create<[Nullable(2)] T1>(T1 item1)
		{
			return new Tuple<T1>(item1);
		}

		// Token: 0x0600195C RID: 6492 RVA: 0x000F853C File Offset: 0x000F773C
		public static Tuple<T1, T2> Create<[Nullable(2)] T1, [Nullable(2)] T2>(T1 item1, T2 item2)
		{
			return new Tuple<T1, T2>(item1, item2);
		}

		// Token: 0x0600195D RID: 6493 RVA: 0x000F8545 File Offset: 0x000F7745
		public static Tuple<T1, T2, T3> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3>(T1 item1, T2 item2, T3 item3)
		{
			return new Tuple<T1, T2, T3>(item1, item2, item3);
		}

		// Token: 0x0600195E RID: 6494 RVA: 0x000F854F File Offset: 0x000F774F
		public static Tuple<T1, T2, T3, T4> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4>(T1 item1, T2 item2, T3 item3, T4 item4)
		{
			return new Tuple<T1, T2, T3, T4>(item1, item2, item3, item4);
		}

		// Token: 0x0600195F RID: 6495 RVA: 0x000F855A File Offset: 0x000F775A
		public static Tuple<T1, T2, T3, T4, T5> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5)
		{
			return new Tuple<T1, T2, T3, T4, T5>(item1, item2, item3, item4, item5);
		}

		// Token: 0x06001960 RID: 6496 RVA: 0x000F8567 File Offset: 0x000F7767
		public static Tuple<T1, T2, T3, T4, T5, T6> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6)
		{
			return new Tuple<T1, T2, T3, T4, T5, T6>(item1, item2, item3, item4, item5, item6);
		}

		// Token: 0x06001961 RID: 6497 RVA: 0x000F8576 File Offset: 0x000F7776
		public static Tuple<T1, T2, T3, T4, T5, T6, T7> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7)
		{
			return new Tuple<T1, T2, T3, T4, T5, T6, T7>(item1, item2, item3, item4, item5, item6, item7);
		}

		// Token: 0x06001962 RID: 6498 RVA: 0x000F8587 File Offset: 0x000F7787
		public static Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>> Create<[Nullable(2)] T1, [Nullable(2)] T2, [Nullable(2)] T3, [Nullable(2)] T4, [Nullable(2)] T5, [Nullable(2)] T6, [Nullable(2)] T7, [Nullable(2)] T8>(T1 item1, T2 item2, T3 item3, T4 item4, T5 item5, T6 item6, T7 item7, T8 item8)
		{
			return new Tuple<T1, T2, T3, T4, T5, T6, T7, Tuple<T8>>(item1, item2, item3, item4, item5, item6, item7, new Tuple<T8>(item8));
		}

		// Token: 0x06001963 RID: 6499 RVA: 0x000F859F File Offset: 0x000F779F
		internal static int CombineHashCodes(int h1, int h2)
		{
			return (h1 << 5) + h1 ^ h2;
		}

		// Token: 0x06001964 RID: 6500 RVA: 0x000F85A8 File Offset: 0x000F77A8
		internal static int CombineHashCodes(int h1, int h2, int h3)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), h3);
		}

		// Token: 0x06001965 RID: 6501 RVA: 0x000F85B7 File Offset: 0x000F77B7
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2), Tuple.CombineHashCodes(h3, h4));
		}

		// Token: 0x06001966 RID: 6502 RVA: 0x000F85CC File Offset: 0x000F77CC
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), h5);
		}

		// Token: 0x06001967 RID: 6503 RVA: 0x000F85DE File Offset: 0x000F77DE
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6));
		}

		// Token: 0x06001968 RID: 6504 RVA: 0x000F85F7 File Offset: 0x000F77F7
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7));
		}

		// Token: 0x06001969 RID: 6505 RVA: 0x000F8612 File Offset: 0x000F7812
		internal static int CombineHashCodes(int h1, int h2, int h3, int h4, int h5, int h6, int h7, int h8)
		{
			return Tuple.CombineHashCodes(Tuple.CombineHashCodes(h1, h2, h3, h4), Tuple.CombineHashCodes(h5, h6, h7, h8));
		}
	}
}
