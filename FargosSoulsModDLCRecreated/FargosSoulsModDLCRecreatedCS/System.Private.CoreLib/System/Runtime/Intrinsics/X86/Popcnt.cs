using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Intrinsics.X86
{
	// Token: 0x0200043D RID: 1085
	[Intrinsic]
	[CLSCompliant(false)]
	public abstract class Popcnt : Sse42
	{
		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x06004069 RID: 16489 RVA: 0x00173F41 File Offset: 0x00173141
		public new static bool IsSupported
		{
			get
			{
				return Popcnt.IsSupported;
			}
		}

		// Token: 0x0600406A RID: 16490 RVA: 0x00173F48 File Offset: 0x00173148
		public static uint PopCount(uint value)
		{
			return Popcnt.PopCount(value);
		}

		// Token: 0x0200043E RID: 1086
		[Intrinsic]
		public new abstract class X64 : Sse42.X64
		{
			// Token: 0x17000A32 RID: 2610
			// (get) Token: 0x0600406B RID: 16491 RVA: 0x00173F50 File Offset: 0x00173150
			public new static bool IsSupported
			{
				get
				{
					return Popcnt.X64.IsSupported;
				}
			}

			// Token: 0x0600406C RID: 16492 RVA: 0x00173F57 File Offset: 0x00173157
			public static ulong PopCount(ulong value)
			{
				return Popcnt.X64.PopCount(value);
			}
		}
	}
}
