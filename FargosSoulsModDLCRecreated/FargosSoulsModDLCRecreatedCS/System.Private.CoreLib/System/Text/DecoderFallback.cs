using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000360 RID: 864
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class DecoderFallback
	{
		// Token: 0x17000918 RID: 2328
		// (get) Token: 0x06002D84 RID: 11652 RVA: 0x00159C83 File Offset: 0x00158E83
		public static DecoderFallback ReplacementFallback
		{
			get
			{
				return DecoderReplacementFallback.s_default;
			}
		}

		// Token: 0x17000919 RID: 2329
		// (get) Token: 0x06002D85 RID: 11653 RVA: 0x00159C8A File Offset: 0x00158E8A
		public static DecoderFallback ExceptionFallback
		{
			get
			{
				return DecoderExceptionFallback.s_default;
			}
		}

		// Token: 0x06002D86 RID: 11654
		public abstract DecoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x1700091A RID: 2330
		// (get) Token: 0x06002D87 RID: 11655
		public abstract int MaxCharCount { get; }
	}
}
