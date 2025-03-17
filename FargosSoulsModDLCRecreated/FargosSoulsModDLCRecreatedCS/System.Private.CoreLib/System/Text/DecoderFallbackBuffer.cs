using System;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000361 RID: 865
	public abstract class DecoderFallbackBuffer
	{
		// Token: 0x06002D89 RID: 11657
		[NullableContext(1)]
		public abstract bool Fallback(byte[] bytesUnknown, int index);

		// Token: 0x06002D8A RID: 11658
		public abstract char GetNextChar();

		// Token: 0x06002D8B RID: 11659
		public abstract bool MovePrevious();

		// Token: 0x1700091B RID: 2331
		// (get) Token: 0x06002D8C RID: 11660
		public abstract int Remaining { get; }

		// Token: 0x06002D8D RID: 11661 RVA: 0x00159C91 File Offset: 0x00158E91
		public virtual void Reset()
		{
			while (this.GetNextChar() != '\0')
			{
			}
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x00159C9B File Offset: 0x00158E9B
		internal void InternalReset()
		{
			this.byteStart = null;
			this.Reset();
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x00159CAB File Offset: 0x00158EAB
		internal unsafe void InternalInitialize(byte* byteStart, char* charEnd)
		{
			this.byteStart = byteStart;
			this.charEnd = charEnd;
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x00159CBC File Offset: 0x00158EBC
		internal static DecoderFallbackBuffer CreateAndInitialize(Encoding encoding, DecoderNLS decoder, int originalByteCount)
		{
			DecoderFallbackBuffer decoderFallbackBuffer = (decoder == null) ? encoding.DecoderFallback.CreateFallbackBuffer() : decoder.FallbackBuffer;
			decoderFallbackBuffer._encoding = encoding;
			decoderFallbackBuffer._decoder = decoder;
			decoderFallbackBuffer._originalByteCount = originalByteCount;
			return decoderFallbackBuffer;
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x00159CF8 File Offset: 0x00158EF8
		internal unsafe virtual bool InternalFallback(byte[] bytes, byte* pBytes, ref char* chars)
		{
			if (this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				char* ptr = chars;
				bool flag = false;
				char nextChar;
				while ((nextChar = this.GetNextChar()) != '\0')
				{
					if (char.IsSurrogate(nextChar))
					{
						if (char.IsHighSurrogate(nextChar))
						{
							if (flag)
							{
								throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex);
							}
							flag = true;
						}
						else
						{
							if (!flag)
							{
								throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex);
							}
							flag = false;
						}
					}
					if (ptr >= this.charEnd)
					{
						return false;
					}
					*(ptr++) = nextChar;
				}
				if (flag)
				{
					throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex);
				}
				chars = ptr;
			}
			return true;
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x00159D88 File Offset: 0x00158F88
		internal unsafe virtual int InternalFallback(byte[] bytes, byte* pBytes)
		{
			if (!this.Fallback(bytes, (int)((long)(pBytes - this.byteStart) - (long)bytes.Length)))
			{
				return 0;
			}
			int num = 0;
			bool flag = false;
			char nextChar;
			while ((nextChar = this.GetNextChar()) != '\0')
			{
				if (char.IsSurrogate(nextChar))
				{
					if (char.IsHighSurrogate(nextChar))
					{
						if (flag)
						{
							throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex);
						}
						flag = true;
					}
					else
					{
						if (!flag)
						{
							throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex);
						}
						flag = false;
					}
				}
				num++;
			}
			if (flag)
			{
				throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex);
			}
			return num;
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x00159E08 File Offset: 0x00159008
		internal int InternalFallbackGetCharCount(ReadOnlySpan<byte> remainingBytes, int fallbackLength)
		{
			if (!this.Fallback(remainingBytes.Slice(0, fallbackLength).ToArray(), this._originalByteCount - remainingBytes.Length))
			{
				return 0;
			}
			return this.DrainRemainingDataForGetCharCount();
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x00159E44 File Offset: 0x00159044
		internal bool TryInternalFallbackGetChars(ReadOnlySpan<byte> remainingBytes, int fallbackLength, Span<char> chars, out int charsWritten)
		{
			if (this.Fallback(remainingBytes.Slice(0, fallbackLength).ToArray(), this._originalByteCount - remainingBytes.Length))
			{
				return this.TryDrainRemainingDataForGetChars(chars, out charsWritten);
			}
			charsWritten = 0;
			return true;
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x00159E88 File Offset: 0x00159088
		private Rune GetNextRune()
		{
			char nextChar = this.GetNextChar();
			Rune result;
			if (!Rune.TryCreate(nextChar, out result) && !Rune.TryCreate(nextChar, this.GetNextChar(), out result))
			{
				throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex);
			}
			return result;
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x00159EC4 File Offset: 0x001590C4
		internal int DrainRemainingDataForGetCharCount()
		{
			int num = 0;
			for (;;)
			{
				Rune nextRune;
				Rune rune = nextRune = this.GetNextRune();
				if (nextRune.Value == 0)
				{
					break;
				}
				num += rune.Utf16SequenceLength;
				if (num < 0)
				{
					this.InternalReset();
					Encoding.ThrowConversionOverflow();
				}
			}
			return num;
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x00159F04 File Offset: 0x00159104
		internal bool TryDrainRemainingDataForGetChars(Span<char> chars, out int charsWritten)
		{
			int length = chars.Length;
			for (;;)
			{
				Rune nextRune;
				Rune rune = nextRune = this.GetNextRune();
				if (nextRune.Value == 0)
				{
					goto Block_2;
				}
				int start;
				if (!rune.TryEncodeToUtf16(chars, out start))
				{
					break;
				}
				chars = chars.Slice(start);
			}
			this.InternalReset();
			charsWritten = 0;
			return false;
			Block_2:
			charsWritten = length - chars.Length;
			return true;
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x00159F5C File Offset: 0x0015915C
		[DoesNotReturn]
		internal void ThrowLastBytesRecursive(byte[] bytesUnknown)
		{
			if (bytesUnknown == null)
			{
				bytesUnknown = Array.Empty<byte>();
			}
			StringBuilder stringBuilder = new StringBuilder(bytesUnknown.Length * 3);
			int num = 0;
			while (num < bytesUnknown.Length && num < 20)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(' ');
				}
				stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "\\x{0:X2}", bytesUnknown[num]);
				num++;
			}
			if (num == 20)
			{
				stringBuilder.Append(" ...");
			}
			throw new ArgumentException(SR.Format(SR.Argument_RecursiveFallbackBytes, stringBuilder.ToString()), "bytesUnknown");
		}

		// Token: 0x04000C90 RID: 3216
		internal unsafe byte* byteStart;

		// Token: 0x04000C91 RID: 3217
		internal unsafe char* charEnd;

		// Token: 0x04000C92 RID: 3218
		internal Encoding _encoding;

		// Token: 0x04000C93 RID: 3219
		internal DecoderNLS _decoder;

		// Token: 0x04000C94 RID: 3220
		private int _originalByteCount;
	}
}
