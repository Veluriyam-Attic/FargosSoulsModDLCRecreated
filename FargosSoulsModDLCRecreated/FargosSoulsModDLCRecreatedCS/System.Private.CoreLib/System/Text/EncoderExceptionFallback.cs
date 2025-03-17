using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000366 RID: 870
	public sealed class EncoderExceptionFallback : EncoderFallback
	{
		// Token: 0x06002DCD RID: 11725 RVA: 0x0015AD04 File Offset: 0x00159F04
		[NullableContext(1)]
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderExceptionFallbackBuffer();
		}

		// Token: 0x17000925 RID: 2341
		// (get) Token: 0x06002DCE RID: 11726 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override int MaxCharCount
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x0015AD0B File Offset: 0x00159F0B
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			return value is EncoderExceptionFallback;
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x0015AD16 File Offset: 0x00159F16
		public override int GetHashCode()
		{
			return 654;
		}

		// Token: 0x04000CA2 RID: 3234
		internal static readonly EncoderExceptionFallback s_default = new EncoderExceptionFallback();
	}
}
