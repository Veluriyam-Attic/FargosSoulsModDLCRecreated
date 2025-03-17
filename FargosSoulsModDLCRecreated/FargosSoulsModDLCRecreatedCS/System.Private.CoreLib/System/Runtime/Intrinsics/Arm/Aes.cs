using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.Arm
{
	// Token: 0x0200041C RID: 1052
	[CLSCompliant(false)]
	public abstract class Aes : ArmBase
	{
		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06003D31 RID: 15665 RVA: 0x000AC09B File Offset: 0x000AB29B
		public new static bool IsSupported
		{
			[Intrinsic]
			get
			{
				return false;
			}
		}

		// Token: 0x06003D32 RID: 15666 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Decrypt(Vector128<byte> value, Vector128<byte> roundKey)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D33 RID: 15667 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> Encrypt(Vector128<byte> value, Vector128<byte> roundKey)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D34 RID: 15668 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> InverseMixColumns(Vector128<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D35 RID: 15669 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<byte> MixColumns(Vector128<byte> value)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D36 RID: 15670 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> PolynomialMultiplyWideningLower(Vector64<long> left, Vector64<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D37 RID: 15671 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> PolynomialMultiplyWideningLower(Vector64<ulong> left, Vector64<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D38 RID: 15672 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<long> PolynomialMultiplyWideningUpper(Vector128<long> left, Vector128<long> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x06003D39 RID: 15673 RVA: 0x000B3617 File Offset: 0x000B2817
		public static Vector128<ulong> PolynomialMultiplyWideningUpper(Vector128<ulong> left, Vector128<ulong> right)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0200041D RID: 1053
		public new abstract class Arm64 : ArmBase.Arm64
		{
			// Token: 0x17000A12 RID: 2578
			// (get) Token: 0x06003D3A RID: 15674 RVA: 0x000AC09B File Offset: 0x000AB29B
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
