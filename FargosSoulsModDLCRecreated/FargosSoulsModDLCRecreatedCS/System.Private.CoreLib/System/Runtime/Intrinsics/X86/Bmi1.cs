using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000433 RID: 1075
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Bmi1 : X86Base
	{
		// Token: 0x17000A27 RID: 2599
		// (get) Token: 0x06004023 RID: 16419 RVA: 0x00173CBB File Offset: 0x00172EBB
		public new static bool IsSupported
		{
			get
			{
				return Bmi1.IsSupported;
			}
		}

		// Token: 0x06004024 RID: 16420 RVA: 0x00173CC2 File Offset: 0x00172EC2
		public static uint AndNot(uint left, uint right)
		{
			return Bmi1.AndNot(left, right);
		}

		// Token: 0x06004025 RID: 16421 RVA: 0x00173CCB File Offset: 0x00172ECB
		public static uint BitFieldExtract(uint value, byte start, byte length)
		{
			return Bmi1.BitFieldExtract(value, (ushort)((int)start | (int)length << 8));
		}

		// Token: 0x06004026 RID: 16422 RVA: 0x00173CD9 File Offset: 0x00172ED9
		public static uint BitFieldExtract(uint value, ushort control)
		{
			return Bmi1.BitFieldExtract(value, control);
		}

		// Token: 0x06004027 RID: 16423 RVA: 0x00173CE2 File Offset: 0x00172EE2
		public static uint ExtractLowestSetBit(uint value)
		{
			return Bmi1.ExtractLowestSetBit(value);
		}

		// Token: 0x06004028 RID: 16424 RVA: 0x00173CEA File Offset: 0x00172EEA
		public static uint GetMaskUpToLowestSetBit(uint value)
		{
			return Bmi1.GetMaskUpToLowestSetBit(value);
		}

		// Token: 0x06004029 RID: 16425 RVA: 0x00173CF2 File Offset: 0x00172EF2
		public static uint ResetLowestSetBit(uint value)
		{
			return Bmi1.ResetLowestSetBit(value);
		}

		// Token: 0x0600402A RID: 16426 RVA: 0x00173CFA File Offset: 0x00172EFA
		public static uint TrailingZeroCount(uint value)
		{
			return Bmi1.TrailingZeroCount(value);
		}

		// Token: 0x02000434 RID: 1076
		[Intrinsic]
		public new abstract class X64 : X86Base.X64
		{
			// Token: 0x17000A28 RID: 2600
			// (get) Token: 0x0600402B RID: 16427 RVA: 0x00173D02 File Offset: 0x00172F02
			public new static bool IsSupported
			{
				get
				{
					return Bmi1.X64.IsSupported;
				}
			}

			// Token: 0x0600402C RID: 16428 RVA: 0x00173D09 File Offset: 0x00172F09
			public static ulong AndNot(ulong left, ulong right)
			{
				return Bmi1.X64.AndNot(left, right);
			}

			// Token: 0x0600402D RID: 16429 RVA: 0x00173D12 File Offset: 0x00172F12
			public static ulong BitFieldExtract(ulong value, byte start, byte length)
			{
				return Bmi1.X64.BitFieldExtract(value, (ushort)((int)start | (int)length << 8));
			}

			// Token: 0x0600402E RID: 16430 RVA: 0x00173D20 File Offset: 0x00172F20
			public static ulong BitFieldExtract(ulong value, ushort control)
			{
				return Bmi1.X64.BitFieldExtract(value, control);
			}

			// Token: 0x0600402F RID: 16431 RVA: 0x00173D29 File Offset: 0x00172F29
			public static ulong ExtractLowestSetBit(ulong value)
			{
				return Bmi1.X64.ExtractLowestSetBit(value);
			}

			// Token: 0x06004030 RID: 16432 RVA: 0x00173D31 File Offset: 0x00172F31
			public static ulong GetMaskUpToLowestSetBit(ulong value)
			{
				return Bmi1.X64.GetMaskUpToLowestSetBit(value);
			}

			// Token: 0x06004031 RID: 16433 RVA: 0x00173D39 File Offset: 0x00172F39
			public static ulong ResetLowestSetBit(ulong value)
			{
				return Bmi1.X64.ResetLowestSetBit(value);
			}

			// Token: 0x06004032 RID: 16434 RVA: 0x00173D41 File Offset: 0x00172F41
			public static ulong TrailingZeroCount(ulong value)
			{
				return Bmi1.X64.TrailingZeroCount(value);
			}
		}
	}
}
