using System;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;

namespace System.Text
{
	// Token: 0x0200036A RID: 874
	public abstract class EncoderFallbackBuffer
	{
		// Token: 0x06002DE8 RID: 11752
		public abstract bool Fallback(char charUnknown, int index);

		// Token: 0x06002DE9 RID: 11753
		public abstract bool Fallback(char charUnknownHigh, char charUnknownLow, int index);

		// Token: 0x06002DEA RID: 11754
		public abstract char GetNextChar();

		// Token: 0x06002DEB RID: 11755
		public abstract bool MovePrevious();

		// Token: 0x1700092E RID: 2350
		// (get) Token: 0x06002DEC RID: 11756
		public abstract int Remaining { get; }

		// Token: 0x06002DED RID: 11757 RVA: 0x0015AEC1 File Offset: 0x0015A0C1
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x06002DEE RID: 11758 RVA: 0x0015AECB File Offset: 0x0015A0CB
		internal void InternalReset()
		{
			this.charStart = null;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
			this.Reset();
		}

		// Token: 0x06002DEF RID: 11759 RVA: 0x0015AEE9 File Offset: 0x0015A0E9
		internal unsafe void InternalInitialize(char* charStart, char* charEnd, EncoderNLS encoder, bool setEncoder)
		{
			this.charStart = charStart;
			this.charEnd = charEnd;
			this.encoder = encoder;
			this.setEncoder = setEncoder;
			this.bUsedEncoder = false;
			this.bFallingBack = false;
			this.iRecursionCount = 0;
		}

		// Token: 0x06002DF0 RID: 11760 RVA: 0x0015AF20 File Offset: 0x0015A120
		internal static EncoderFallbackBuffer CreateAndInitialize(Encoding encoding, EncoderNLS encoder, int originalCharCount)
		{
			EncoderFallbackBuffer encoderFallbackBuffer = (encoder == null) ? encoding.EncoderFallback.CreateFallbackBuffer() : encoder.FallbackBuffer;
			encoderFallbackBuffer.encoding = encoding;
			encoderFallbackBuffer.encoder = encoder;
			encoderFallbackBuffer.originalCharCount = originalCharCount;
			return encoderFallbackBuffer;
		}

		// Token: 0x06002DF1 RID: 11761 RVA: 0x0015AF5C File Offset: 0x0015A15C
		internal char InternalGetNextChar()
		{
			char nextChar = this.GetNextChar();
			this.bFallingBack = (nextChar > '\0');
			if (nextChar == '\0')
			{
				this.iRecursionCount = 0;
			}
			return nextChar;
		}

		// Token: 0x06002DF2 RID: 11762 RVA: 0x0015AF88 File Offset: 0x0015A188
		private unsafe bool InternalFallback(ReadOnlySpan<char> chars, out int charsConsumed)
		{
			char c = (char)(*chars[0]);
			char c2 = '\0';
			if (!chars.IsEmpty)
			{
				c = (char)(*chars[0]);
				if (1 < chars.Length)
				{
					c2 = (char)(*chars[1]);
				}
			}
			int index = this.originalCharCount - chars.Length;
			if (!char.IsSurrogatePair(c, c2))
			{
				charsConsumed = 1;
				return this.Fallback(c, index);
			}
			charsConsumed = 2;
			return this.Fallback(c, c2, index);
		}

		// Token: 0x06002DF3 RID: 11763 RVA: 0x0015AFF8 File Offset: 0x0015A1F8
		internal int InternalFallbackGetByteCount(ReadOnlySpan<char> chars, out int charsConsumed)
		{
			int result = 0;
			if (this.InternalFallback(chars, out charsConsumed))
			{
				result = this.DrainRemainingDataForGetByteCount();
			}
			return result;
		}

		// Token: 0x06002DF4 RID: 11764 RVA: 0x0015B019 File Offset: 0x0015A219
		internal bool TryInternalFallbackGetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, out int charsConsumed, out int bytesWritten)
		{
			if (this.InternalFallback(chars, out charsConsumed))
			{
				return this.TryDrainRemainingDataForGetBytes(bytes, out bytesWritten);
			}
			bytesWritten = 0;
			return true;
		}

		// Token: 0x06002DF5 RID: 11765 RVA: 0x0015B034 File Offset: 0x0015A234
		internal bool TryDrainRemainingDataForGetBytes(Span<byte> bytes, out int bytesWritten)
		{
			int length = bytes.Length;
			Rune value;
			for (;;)
			{
				Rune nextRune;
				value = (nextRune = this.GetNextRune());
				if (nextRune.Value == 0)
				{
					goto Block_3;
				}
				int start;
				switch (this.encoding.EncodeRune(value, bytes, out start))
				{
				case OperationStatus.Done:
					bytes = bytes.Slice(start);
					break;
				case OperationStatus.DestinationTooSmall:
					goto IL_3E;
				case OperationStatus.InvalidData:
					EncoderFallbackBuffer.ThrowLastCharRecursive(value.Value);
					break;
				}
			}
			IL_3E:
			for (int i = 0; i < value.Utf16SequenceLength; i++)
			{
				this.MovePrevious();
			}
			bytesWritten = length - bytes.Length;
			return false;
			Block_3:
			bytesWritten = length - bytes.Length;
			return true;
		}

		// Token: 0x06002DF6 RID: 11766 RVA: 0x0015B0D4 File Offset: 0x0015A2D4
		internal int DrainRemainingDataForGetByteCount()
		{
			int num = 0;
			for (;;)
			{
				Rune nextRune;
				Rune value = nextRune = this.GetNextRune();
				if (nextRune.Value == 0)
				{
					break;
				}
				int num2;
				if (!this.encoding.TryGetByteCount(value, out num2))
				{
					EncoderFallbackBuffer.ThrowLastCharRecursive(value.Value);
				}
				num += num2;
				if (num < 0)
				{
					this.InternalReset();
					Encoding.ThrowConversionOverflow();
				}
			}
			return num;
		}

		// Token: 0x06002DF7 RID: 11767 RVA: 0x0015B128 File Offset: 0x0015A328
		private Rune GetNextRune()
		{
			char nextChar = this.GetNextChar();
			Rune result;
			if (Rune.TryCreate(nextChar, out result) || Rune.TryCreate(nextChar, this.GetNextChar(), out result))
			{
				return result;
			}
			throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex);
		}

		// Token: 0x06002DF8 RID: 11768 RVA: 0x0015B164 File Offset: 0x0015A364
		internal unsafe virtual bool InternalFallback(char ch, ref char* chars)
		{
			int index = (chars - this.charStart) / 2 - 1;
			if (char.IsHighSurrogate(ch))
			{
				if (chars >= this.charEnd)
				{
					if (this.encoder != null && !this.encoder.MustFlush)
					{
						if (this.setEncoder)
						{
							this.bUsedEncoder = true;
							this.encoder._charLeftOver = ch;
						}
						this.bFallingBack = false;
						return false;
					}
				}
				else
				{
					char c = (char)(*chars);
					if (char.IsLowSurrogate(c))
					{
						if (this.bFallingBack)
						{
							int num = this.iRecursionCount;
							this.iRecursionCount = num + 1;
							if (num > 250)
							{
								EncoderFallbackBuffer.ThrowLastCharRecursive(char.ConvertToUtf32(ch, c));
							}
						}
						chars += 2;
						this.bFallingBack = this.Fallback(ch, c, index);
						return this.bFallingBack;
					}
				}
			}
			if (this.bFallingBack)
			{
				int num = this.iRecursionCount;
				this.iRecursionCount = num + 1;
				if (num > 250)
				{
					EncoderFallbackBuffer.ThrowLastCharRecursive((int)ch);
				}
			}
			this.bFallingBack = this.Fallback(ch, index);
			return this.bFallingBack;
		}

		// Token: 0x06002DF9 RID: 11769 RVA: 0x0015B260 File Offset: 0x0015A460
		[DoesNotReturn]
		internal static void ThrowLastCharRecursive(int charRecursive)
		{
			throw new ArgumentException(SR.Format(SR.Argument_RecursiveFallback, charRecursive), "chars");
		}

		// Token: 0x04000CA7 RID: 3239
		internal unsafe char* charStart;

		// Token: 0x04000CA8 RID: 3240
		internal unsafe char* charEnd;

		// Token: 0x04000CA9 RID: 3241
		internal EncoderNLS encoder;

		// Token: 0x04000CAA RID: 3242
		internal bool setEncoder;

		// Token: 0x04000CAB RID: 3243
		internal bool bUsedEncoder;

		// Token: 0x04000CAC RID: 3244
		internal bool bFallingBack;

		// Token: 0x04000CAD RID: 3245
		internal int iRecursionCount;

		// Token: 0x04000CAE RID: 3246
		private Encoding encoding;

		// Token: 0x04000CAF RID: 3247
		private int originalCharCount;
	}
}
