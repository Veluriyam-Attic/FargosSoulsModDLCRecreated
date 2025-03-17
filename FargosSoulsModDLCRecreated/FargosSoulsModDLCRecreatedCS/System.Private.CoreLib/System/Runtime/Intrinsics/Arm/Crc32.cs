using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.Arm
{
	// Token: 0x02000420 RID: 1056
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Crc32 : ArmBase
	{
		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x000AC09B File Offset: 0x000AB29B
		public new static bool IsSupported
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06003D48 RID: 15688 RVA: 0x000B3617 File Offset: 0x000B2817
		public static uint ComputeCrc32(uint crc, byte data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D49 RID: 15689 RVA: 0x000B3617 File Offset: 0x000B2817
		public static uint ComputeCrc32(uint crc, ushort data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D4A RID: 15690 RVA: 0x000B3617 File Offset: 0x000B2817
		public static uint ComputeCrc32(uint crc, uint data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D4B RID: 15691 RVA: 0x000B3617 File Offset: 0x000B2817
		public static uint ComputeCrc32C(uint crc, byte data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D4C RID: 15692 RVA: 0x000B3617 File Offset: 0x000B2817
		public static uint ComputeCrc32C(uint crc, ushort data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D4D RID: 15693 RVA: 0x000B3617 File Offset: 0x000B2817
		public static uint ComputeCrc32C(uint crc, uint data)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x02000421 RID: 1057
		[Intrinsic]
		public new abstract class Arm64 : ArmBase.Arm64
		{
			// Token: 0x17000A16 RID: 2582
			// (get) Token: 0x06003D4E RID: 15694 RVA: 0x000AC09B File Offset: 0x000AB29B
			public new static bool IsSupported
			{
				[Intrinsic]
				get
				{
					return false;
				}
			}

			// Token: 0x06003D4F RID: 15695 RVA: 0x000B3617 File Offset: 0x000B2817
			public static uint ComputeCrc32(uint crc, ulong data)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D50 RID: 15696 RVA: 0x000B3617 File Offset: 0x000B2817
			public static uint ComputeCrc32C(uint crc, ulong data)
			{
				throw new PlatformNotSupportedException();
			}
		}
	}
}
