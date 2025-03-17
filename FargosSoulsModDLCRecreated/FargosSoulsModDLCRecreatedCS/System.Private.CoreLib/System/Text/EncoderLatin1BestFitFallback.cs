using System;

namespace System.Text
{
	// Token: 0x0200036B RID: 875
	internal sealed class EncoderLatin1BestFitFallback : EncoderFallback
	{
		// Token: 0x06002DFB RID: 11771 RVA: 0x0015ACFC File Offset: 0x00159EFC
		private EncoderLatin1BestFitFallback()
		{
		}

		// Token: 0x06002DFC RID: 11772 RVA: 0x0015B27C File Offset: 0x0015A47C
		public override EncoderFallbackBuffer CreateFallbackBuffer()
		{
			return new EncoderLatin1BestFitFallbackBuffer();
		}

		// Token: 0x1700092F RID: 2351
		// (get) Token: 0x06002DFD RID: 11773 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override int MaxCharCount
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x04000CB0 RID: 3248
		internal static readonly EncoderLatin1BestFitFallback SingletonInstance = new EncoderLatin1BestFitFallback();
	}
}
