using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.Arm
{
	// Token: 0x02000428 RID: 1064
	[CLSCompliant(false)]
	public abstract class Sha256 : ArmBase
	{
		// Token: 0x17000A1D RID: 2589
		// (get) Token: 0x06003D8D RID: 15757 RVA: 0x000AC09B File Offset: 0x000AB29B
		public new static bool IsSupported
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> HashUpdate1(Vector128<uint> hash_abcd, Vector128<uint> hash_efgh, Vector128<uint> wk)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> HashUpdate2(Vector128<uint> hash_efgh, Vector128<uint> hash_abcd, Vector128<uint> wk)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ScheduleUpdate0(Vector128<uint> w0_3, Vector128<uint> w4_7)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ScheduleUpdate1(Vector128<uint> w0_3, Vector128<uint> w8_11, Vector128<uint> w12_15)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x02000429 RID: 1065
		public new abstract class Arm64 : ArmBase.Arm64
		{
			// Token: 0x17000A1E RID: 2590
			// (get) Token: 0x06003D92 RID: 15762 RVA: 0x000AC09B File Offset: 0x000AB29B
			public new static bool IsSupported
			{
				[Intrinsic]
				get
				{
					return false;
				}
			}
		}
	}
}
