using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000435 RID: 1077
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Bmi2 : X86Base
	{
		// Token: 0x17000A29 RID: 2601
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x00173D49 File Offset: 0x00172F49
		public new static bool IsSupported
		{
			get
			{
				return Bmi2.IsSupported;
			}
		}

		// Token: 0x06004034 RID: 16436 RVA: 0x00173D50 File Offset: 0x00172F50
		public static uint ZeroHighBits(uint value, uint index)
		{
			return Bmi2.ZeroHighBits(value, index);
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x00173D59 File Offset: 0x00172F59
		public static uint MultiplyNoFlags(uint left, uint right)
		{
			return Bmi2.MultiplyNoFlags(left, right);
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x00173D62 File Offset: 0x00172F62
		public unsafe static uint MultiplyNoFlags(uint left, uint right, uint* low)
		{
			return Bmi2.MultiplyNoFlags(left, right, low);
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x00173D6C File Offset: 0x00172F6C
		public static uint ParallelBitDeposit(uint value, uint mask)
		{
			return Bmi2.ParallelBitDeposit(value, mask);
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x00173D75 File Offset: 0x00172F75
		public static uint ParallelBitExtract(uint value, uint mask)
		{
			return Bmi2.ParallelBitExtract(value, mask);
		}

		// Token: 0x02000436 RID: 1078
		[Intrinsic]
		public new abstract class X64 : X86Base.X64
		{
			// Token: 0x17000A2A RID: 2602
			// (get) Token: 0x06004039 RID: 16441 RVA: 0x00173D7E File Offset: 0x00172F7E
			public new static bool IsSupported
			{
				get
				{
					return Bmi2.X64.IsSupported;
				}
			}

			// Token: 0x0600403A RID: 16442 RVA: 0x00173D85 File Offset: 0x00172F85
			public static ulong ZeroHighBits(ulong value, ulong index)
			{
				return Bmi2.X64.ZeroHighBits(value, index);
			}

			// Token: 0x0600403B RID: 16443 RVA: 0x00173D8E File Offset: 0x00172F8E
			public static ulong MultiplyNoFlags(ulong left, ulong right)
			{
				return Bmi2.X64.MultiplyNoFlags(left, right);
			}

			// Token: 0x0600403C RID: 16444 RVA: 0x00173D97 File Offset: 0x00172F97
			public unsafe static ulong MultiplyNoFlags(ulong left, ulong right, ulong* low)
			{
				return Bmi2.X64.MultiplyNoFlags(left, right, low);
			}

			// Token: 0x0600403D RID: 16445 RVA: 0x00173DA1 File Offset: 0x00172FA1
			public static ulong ParallelBitDeposit(ulong value, ulong mask)
			{
				return Bmi2.X64.ParallelBitDeposit(value, mask);
			}

			// Token: 0x0600403E RID: 16446 RVA: 0x00173DAA File Offset: 0x00172FAA
			public static ulong ParallelBitExtract(ulong value, ulong mask)
			{
				return Bmi2.X64.ParallelBitExtract(value, mask);
			}
		}
	}
}
