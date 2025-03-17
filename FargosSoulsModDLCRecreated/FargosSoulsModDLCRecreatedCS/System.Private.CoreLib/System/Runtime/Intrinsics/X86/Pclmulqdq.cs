using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x0200043B RID: 1083
	[CLSCompliant(false)]
	[Intrinsic]
	public abstract class Pclmulqdq : Sse2
	{
		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x06004065 RID: 16485 RVA: 0x00173F1F File Offset: 0x0017311F
		public new static bool IsSupported
		{
			get
			{
				return Pclmulqdq.IsSupported;
			}
		}

		// Token: 0x06004066 RID: 16486 RVA: 0x00173F26 File Offset: 0x00173126
		public static Vector128<long> CarrylessMultiply(Vector128<long> left, Vector128<long> right, byte control)
		{
			return Pclmulqdq.CarrylessMultiply(left, right, control);
		}

		// Token: 0x06004067 RID: 16487 RVA: 0x00173F30 File Offset: 0x00173130
		public static Vector128<ulong> CarrylessMultiply(Vector128<ulong> left, Vector128<ulong> right, byte control)
		{
			return Pclmulqdq.CarrylessMultiply(left, right, control);
		}

		// Token: 0x0200043C RID: 1084
		[Intrinsic]
		public new abstract class X64 : Sse2.X64
		{
			// Token: 0x17000A30 RID: 2608
			// (get) Token: 0x06004068 RID: 16488 RVA: 0x00173F3A File Offset: 0x0017313A
			public new static bool IsSupported
			{
				get
				{
					return Pclmulqdq.X64.IsSupported;
				}
			}
		}
	}
}
