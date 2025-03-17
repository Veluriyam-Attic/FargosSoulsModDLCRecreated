using System;

namespace System.Text
{
	// Token: 0x02000367 RID: 871
	public sealed class EncoderExceptionFallbackBuffer : EncoderFallbackBuffer
	{
		// Token: 0x06002DD3 RID: 11731 RVA: 0x0015AD31 File Offset: 0x00159F31
		public override bool Fallback(char charUnknown, int index)
		{
			throw new EncoderFallbackException(SR.Format(SR.Argument_InvalidCodePageConversionIndex, (int)charUnknown, index), charUnknown, index);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x0015AD50 File Offset: 0x00159F50
		public override bool Fallback(char charUnknownHigh, char charUnknownLow, int index)
		{
			if (!char.IsHighSurrogate(charUnknownHigh))
			{
				throw new ArgumentOutOfRangeException("charUnknownHigh", SR.Format(SR.ArgumentOutOfRange_Range, 55296, 56319));
			}
			if (!char.IsLowSurrogate(charUnknownLow))
			{
				throw new ArgumentOutOfRangeException("charUnknownLow", SR.Format(SR.ArgumentOutOfRange_Range, 56320, 57343));
			}
			int num = char.ConvertToUtf32(charUnknownHigh, charUnknownLow);
			throw new EncoderFallbackException(SR.Format(SR.Argument_InvalidCodePageConversionIndex, num, index), charUnknownHigh, charUnknownLow, index);
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override char GetNextChar()
		{
			return '\0';
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool MovePrevious()
		{
			return false;
		}

		// Token: 0x17000926 RID: 2342
		// (get) Token: 0x06002DD7 RID: 11735 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}
	}
}
