using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.Arm
{
	// Token: 0x02000426 RID: 1062
	[CLSCompliant(false)]
	public abstract class Sha1 : ArmBase
	{
		// Token: 0x17000A1B RID: 2587
		// (get) Token: 0x06003D85 RID: 15749 RVA: 0x000AC09B File Offset: 0x000AB29B
		public new static bool IsSupported
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> FixedRotate(Vector64<uint> hash_e)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> HashUpdateChoose(Vector128<uint> hash_abcd, Vector64<uint> hash_e, Vector128<uint> wk)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> HashUpdateMajority(Vector128<uint> hash_abcd, Vector64<uint> hash_e, Vector128<uint> wk)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> HashUpdateParity(Vector128<uint> hash_abcd, Vector64<uint> hash_e, Vector128<uint> wk)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ScheduleUpdate0(Vector128<uint> w0_3, Vector128<uint> w4_7, Vector128<uint> w8_11)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D8B RID: 15755 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> ScheduleUpdate1(Vector128<uint> tw0_3, Vector128<uint> w12_15)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x02000427 RID: 1063
		public new abstract class Arm64 : ArmBase.Arm64
		{
			// Token: 0x17000A1C RID: 2588
			// (get) Token: 0x06003D8C RID: 15756 RVA: 0x000AC09B File Offset: 0x000AB29B
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
