using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200035D RID: 861
	public sealed class DecoderExceptionFallback : DecoderFallback
	{
		// Token: 0x06002D71 RID: 11633 RVA: 0x00159B4C File Offset: 0x00158D4C
		[NullableContext(1)]
		public override DecoderFallbackBuffer CreateFallbackBuffer()
		{
			return new DecoderExceptionFallbackBuffer();
		}

		// Token: 0x17000914 RID: 2324
		// (get) Token: 0x06002D72 RID: 11634 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x00159B53 File Offset: 0x00158D53
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			return value is DecoderExceptionFallback;
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x00159B5E File Offset: 0x00158D5E
		public override int GetHashCode()
		{
			return 879;
		}

		// Token: 0x04000C8D RID: 3213
		internal static readonly DecoderExceptionFallback s_default = new DecoderExceptionFallback();
	}
}
