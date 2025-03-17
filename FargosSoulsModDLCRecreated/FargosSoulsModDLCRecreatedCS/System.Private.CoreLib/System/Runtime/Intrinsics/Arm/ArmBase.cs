using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.Arm
{
	// Token: 0x0200041E RID: 1054
	[CLSCompliant(false)]
	public abstract class ArmBase
	{
		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06003D3B RID: 15675 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool IsSupported
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06003D3C RID: 15676 RVA: 0x000B3617 File Offset: 0x000B2817
		public static int LeadingZeroCount(int value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x000B3617 File Offset: 0x000B2817
		public static int LeadingZeroCount(uint value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x000B3617 File Offset: 0x000B2817
		public static int ReverseElementBits(int value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D3F RID: 15679 RVA: 0x000B3617 File Offset: 0x000B2817
		public static uint ReverseElementBits(uint value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0200041F RID: 1055
		public abstract class Arm64
		{
			// Token: 0x17000A14 RID: 2580
			// (get) Token: 0x06003D40 RID: 15680 RVA: 0x000AC09B File Offset: 0x000AB29B
			public static bool IsSupported
			{
				[Intrinsic]
				get
				{
					return false;
				}
			}

			// Token: 0x06003D41 RID: 15681 RVA: 0x000B3617 File Offset: 0x000B2817
			public static int LeadingSignCount(int value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D42 RID: 15682 RVA: 0x000B3617 File Offset: 0x000B2817
			public static int LeadingSignCount(long value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D43 RID: 15683 RVA: 0x000B3617 File Offset: 0x000B2817
			public static int LeadingZeroCount(long value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D44 RID: 15684 RVA: 0x000B3617 File Offset: 0x000B2817
			public static int LeadingZeroCount(ulong value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D45 RID: 15685 RVA: 0x000B3617 File Offset: 0x000B2817
			public static long ReverseElementBits(long value)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D46 RID: 15686 RVA: 0x000B3617 File Offset: 0x000B2817
			public static ulong ReverseElementBits(ulong value)
			{
				throw new PlatformNotSupportedException();
			}
		}
	}
}
