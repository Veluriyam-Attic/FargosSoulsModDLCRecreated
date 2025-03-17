using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000369 RID: 873
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class EncoderFallback
	{
		// Token: 0x1700092B RID: 2347
		// (get) Token: 0x06002DE3 RID: 11747 RVA: 0x0015AEB3 File Offset: 0x0015A0B3
		public static EncoderFallback ReplacementFallback
		{
			get
			{
				return EncoderReplacementFallback.s_default;
			}
		}

		// Token: 0x1700092C RID: 2348
		// (get) Token: 0x06002DE4 RID: 11748 RVA: 0x0015AEBA File Offset: 0x0015A0BA
		public static EncoderFallback ExceptionFallback
		{
			get
			{
				return EncoderExceptionFallback.s_default;
			}
		}

		// Token: 0x06002DE5 RID: 11749
		public abstract EncoderFallbackBuffer CreateFallbackBuffer();

		// Token: 0x1700092D RID: 2349
		// (get) Token: 0x06002DE6 RID: 11750
		public abstract int MaxCharCount { get; }
	}
}
