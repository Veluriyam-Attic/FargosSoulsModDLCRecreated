using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.Arm
{
	// Token: 0x02000422 RID: 1058
	[CLSCompliant(false)]
	public abstract class Dp : AdvSimd
	{
		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06003D51 RID: 15697 RVA: 0x000AC09B File Offset: 0x000AB29B
		public new static bool IsSupported
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> DotProduct(Vector64<int> addend, Vector64<sbyte> left, Vector64<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> DotProduct(Vector64<uint> addend, Vector64<byte> left, Vector64<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> DotProduct(Vector128<int> addend, Vector128<sbyte> left, Vector128<sbyte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> DotProduct(Vector128<uint> addend, Vector128<byte> left, Vector128<byte> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> DotProductBySelectedQuadruplet(Vector64<int> addend, Vector64<sbyte> left, Vector64<sbyte> right, byte rightScaledIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> DotProductBySelectedQuadruplet(Vector64<int> addend, Vector64<sbyte> left, Vector128<sbyte> right, byte rightScaledIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> DotProductBySelectedQuadruplet(Vector64<uint> addend, Vector64<byte> left, Vector64<byte> right, byte rightScaledIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<uint> DotProductBySelectedQuadruplet(Vector64<uint> addend, Vector64<byte> left, Vector128<byte> right, byte rightScaledIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> DotProductBySelectedQuadruplet(Vector128<int> addend, Vector128<sbyte> left, Vector128<sbyte> right, byte rightScaledIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> DotProductBySelectedQuadruplet(Vector128<int> addend, Vector128<sbyte> left, Vector64<sbyte> right, byte rightScaledIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> DotProductBySelectedQuadruplet(Vector128<uint> addend, Vector128<byte> left, Vector128<byte> right, byte rightScaledIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<uint> DotProductBySelectedQuadruplet(Vector128<uint> addend, Vector128<byte> left, Vector64<byte> right, byte rightScaledIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x02000423 RID: 1059
		public new abstract class Arm64 : AdvSimd.Arm64
		{
			// Token: 0x17000A18 RID: 2584
			// (get) Token: 0x06003D5E RID: 15710 RVA: 0x000AC09B File Offset: 0x000AB29B
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
