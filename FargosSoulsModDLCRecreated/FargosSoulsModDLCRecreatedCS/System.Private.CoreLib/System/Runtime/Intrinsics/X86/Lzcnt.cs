using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x02000439 RID: 1081
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Lzcnt : X86Base
	{
		// Token: 0x17000A2D RID: 2605
		// (get) Token: 0x06004061 RID: 16481 RVA: 0x00173F01 File Offset: 0x00173101
		public new static bool IsSupported
		{
			get
			{
				return Lzcnt.IsSupported;
			}
		}

		// Token: 0x06004062 RID: 16482 RVA: 0x00173F08 File Offset: 0x00173108
		public static uint LeadingZeroCount(uint value)
		{
			return Lzcnt.LeadingZeroCount(value);
		}

		// Token: 0x0200043A RID: 1082
		[Intrinsic]
		public new abstract class X64 : X86Base.X64
		{
			// Token: 0x17000A2E RID: 2606
			// (get) Token: 0x06004063 RID: 16483 RVA: 0x00173F10 File Offset: 0x00173110
			public new static bool IsSupported
			{
				get
				{
					return Lzcnt.X64.IsSupported;
				}
			}

			// Token: 0x06004064 RID: 16484 RVA: 0x00173F17 File Offset: 0x00173117
			public static ulong LeadingZeroCount(ulong value)
			{
				return Lzcnt.X64.LeadingZeroCount(value);
			}
		}
	}
}
