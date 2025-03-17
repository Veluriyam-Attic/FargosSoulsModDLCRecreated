using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.Arm
{
	// Token: 0x02000424 RID: 1060
	[CLSCompliant(false)]
	public abstract class Rdm : AdvSimd
	{
		// Token: 0x17000A19 RID: 2585
		// (get) Token: 0x06003D5F RID: 15711 RVA: 0x000AC09B File Offset: 0x000AB29B
		public new static bool IsSupported
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingAndAddSaturateHigh(Vector64<short> addend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingAndAddSaturateHigh(Vector64<int> addend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingAndAddSaturateHigh(Vector128<short> addend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingAndAddSaturateHigh(Vector128<int> addend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingAndSubtractSaturateHigh(Vector64<short> minuend, Vector64<short> left, Vector64<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingAndSubtractSaturateHigh(Vector64<int> minuend, Vector64<int> left, Vector64<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingAndSubtractSaturateHigh(Vector128<short> minuend, Vector128<short> left, Vector128<short> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingAndSubtractSaturateHigh(Vector128<int> minuend, Vector128<int> left, Vector128<int> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingBySelectedScalarAndAddSaturateHigh(Vector64<short> addend, Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingBySelectedScalarAndAddSaturateHigh(Vector64<short> addend, Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingBySelectedScalarAndAddSaturateHigh(Vector64<int> addend, Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingBySelectedScalarAndAddSaturateHigh(Vector64<int> addend, Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingBySelectedScalarAndAddSaturateHigh(Vector128<short> addend, Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingBySelectedScalarAndAddSaturateHigh(Vector128<short> addend, Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingBySelectedScalarAndAddSaturateHigh(Vector128<int> addend, Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingBySelectedScalarAndAddSaturateHigh(Vector128<int> addend, Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingBySelectedScalarAndSubtractSaturateHigh(Vector64<short> minuend, Vector64<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D71 RID: 15729 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<short> MultiplyRoundedDoublingBySelectedScalarAndSubtractSaturateHigh(Vector64<short> minuend, Vector64<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D72 RID: 15730 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingBySelectedScalarAndSubtractSaturateHigh(Vector64<int> minuend, Vector64<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D73 RID: 15731 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector64<int> MultiplyRoundedDoublingBySelectedScalarAndSubtractSaturateHigh(Vector64<int> minuend, Vector64<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D74 RID: 15732 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingBySelectedScalarAndSubtractSaturateHigh(Vector128<short> minuend, Vector128<short> left, Vector64<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D75 RID: 15733 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<short> MultiplyRoundedDoublingBySelectedScalarAndSubtractSaturateHigh(Vector128<short> minuend, Vector128<short> left, Vector128<short> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D76 RID: 15734 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingBySelectedScalarAndSubtractSaturateHigh(Vector128<int> minuend, Vector128<int> left, Vector64<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D77 RID: 15735 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<int> MultiplyRoundedDoublingBySelectedScalarAndSubtractSaturateHigh(Vector128<int> minuend, Vector128<int> left, Vector128<int> right, byte rightIndex)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x02000425 RID: 1061
		public new abstract class Arm64 : AdvSimd.Arm64
		{
			// Token: 0x17000A1A RID: 2586
			// (get) Token: 0x06003D78 RID: 15736 RVA: 0x000AC09B File Offset: 0x000AB29B
			public new static bool IsSupported
			{
				[Intrinsic]
				get
				{
					return false;
				}
			}

			// Token: 0x06003D79 RID: 15737 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyRoundedDoublingAndAddSaturateHighScalar(Vector64<short> addend, Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D7A RID: 15738 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyRoundedDoublingAndAddSaturateHighScalar(Vector64<int> addend, Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D7B RID: 15739 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyRoundedDoublingAndSubtractSaturateHighScalar(Vector64<short> addend, Vector64<short> left, Vector64<short> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D7C RID: 15740 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyRoundedDoublingAndSubtractSaturateHighScalar(Vector64<int> addend, Vector64<int> left, Vector64<int> right)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D7D RID: 15741 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyRoundedDoublingScalarBySelectedScalarAndAddSaturateHigh(Vector64<short> addend, Vector64<short> left, Vector64<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D7E RID: 15742 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyRoundedDoublingScalarBySelectedScalarAndAddSaturateHigh(Vector64<short> addend, Vector64<short> left, Vector128<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D7F RID: 15743 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyRoundedDoublingScalarBySelectedScalarAndAddSaturateHigh(Vector64<int> addend, Vector64<int> left, Vector64<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D80 RID: 15744 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyRoundedDoublingScalarBySelectedScalarAndAddSaturateHigh(Vector64<int> addend, Vector64<int> left, Vector128<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D81 RID: 15745 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyRoundedDoublingScalarBySelectedScalarAndSubtractSaturateHigh(Vector64<short> minuend, Vector64<short> left, Vector64<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D82 RID: 15746 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<short> MultiplyRoundedDoublingScalarBySelectedScalarAndSubtractSaturateHigh(Vector64<short> minuend, Vector64<short> left, Vector128<short> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D83 RID: 15747 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyRoundedDoublingScalarBySelectedScalarAndSubtractSaturateHigh(Vector64<int> minuend, Vector64<int> left, Vector64<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06003D84 RID: 15748 RVA: 0x000B3617 File Offset: 0x000B2817
			public static Vector64<int> MultiplyRoundedDoublingScalarBySelectedScalarAndSubtractSaturateHigh(Vector64<int> minuend, Vector64<int> left, Vector128<int> right, byte rightIndex)
			{
				throw new PlatformNotSupportedException();
			}
		}
	}
}
