using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x0200035E RID: 862
	public sealed class DecoderExceptionFallbackBuffer : DecoderFallbackBuffer
	{
		// Token: 0x06002D77 RID: 11639 RVA: 0x00159B79 File Offset: 0x00158D79
		[NullableContext(1)]
		public override bool Fallback(byte[] bytesUnknown, int index)
		{
			this.Throw(bytesUnknown, index);
			return true;
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override char GetNextChar()
		{
			return '\0';
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool MovePrevious()
		{
			return false;
		}

		// Token: 0x17000915 RID: 2325
		// (get) Token: 0x06002D7A RID: 11642 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override int Remaining
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x00159B84 File Offset: 0x00158D84
		[DoesNotReturn]
		private void Throw(byte[] bytesUnknown, int index)
		{
			if (bytesUnknown == null)
			{
				bytesUnknown = Array.Empty<byte>();
			}
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 4);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				stringBuilder.Append('[');
				stringBuilder.Append(bytesUnknown[num].ToString("X2", CultureInfo.InvariantCulture));
				stringBuilder.Append(']');
				num++;
			}
			if (bytesUnknown.Length > 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new DecoderFallbackException(SR.Format(SR.Argument_InvalidCodePageBytesIndex, stringBuilder, index), bytesUnknown, index);
		}
	}
}
