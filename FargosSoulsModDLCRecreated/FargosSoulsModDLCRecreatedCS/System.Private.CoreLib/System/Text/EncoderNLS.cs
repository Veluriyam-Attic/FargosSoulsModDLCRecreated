using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x0200036D RID: 877
	internal class EncoderNLS : Encoder
	{
		// Token: 0x06002E08 RID: 11784 RVA: 0x0015B488 File Offset: 0x0015A688
		internal EncoderNLS(Encoding encoding)
		{
			this._encoding = encoding;
			this._fallback = this._encoding.EncoderFallback;
			this.Reset();
		}

		// Token: 0x06002E09 RID: 11785 RVA: 0x0015B4AE File Offset: 0x0015A6AE
		public override void Reset()
		{
			this._charLeftOver = '\0';
			if (this._fallbackBuffer != null)
			{
				this._fallbackBuffer.Reset();
			}
		}

		// Token: 0x06002E0A RID: 11786 RVA: 0x0015B4CC File Offset: 0x0015A6CC
		public unsafe override int GetByteCount(char[] chars, int index, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", SR.ArgumentNull_Array);
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (chars.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("chars", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			int byteCount;
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				byteCount = this.GetByteCount(ptr + index, count, flush);
			}
			return byteCount;
		}

		// Token: 0x06002E0B RID: 11787 RVA: 0x0015B54C File Offset: 0x0015A74C
		public unsafe override int GetByteCount(char* chars, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", SR.ArgumentNull_Array);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetByteCount(chars, count, this);
		}

		// Token: 0x06002E0C RID: 11788 RVA: 0x0015B5A0 File Offset: 0x0015A7A0
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", SR.ArgumentNull_Array);
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", SR.ArgumentOutOfRange_Index);
			}
			int byteCount = bytes.Length - byteIndex;
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* ptr2 = reference2;
					return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, flush);
				}
			}
		}

		// Token: 0x06002E0D RID: 11789 RVA: 0x0015B664 File Offset: 0x0015A864
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", SR.ArgumentNull_Array);
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetBytes(chars, charCount, bytes, byteCount, this);
		}

		// Token: 0x06002E0E RID: 11790 RVA: 0x0015B6DC File Offset: 0x0015A8DC
		public unsafe override void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", SR.ArgumentNull_Array);
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* ptr2 = reference2;
					this.Convert(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, flush, out charsUsed, out bytesUsed, out completed);
				}
			}
		}

		// Token: 0x06002E0F RID: 11791 RVA: 0x0015B7C8 File Offset: 0x0015A9C8
		public unsafe override void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", SR.ArgumentNull_Array);
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this._mustFlush = flush;
			this._throwOnOverflow = false;
			this._charsUsed = 0;
			bytesUsed = this._encoding.GetBytes(chars, charCount, bytes, byteCount, this);
			charsUsed = this._charsUsed;
			completed = (charsUsed == charCount && (!flush || !this.HasState) && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0));
		}

		// Token: 0x17000931 RID: 2353
		// (get) Token: 0x06002E10 RID: 11792 RVA: 0x0015B883 File Offset: 0x0015AA83
		public Encoding Encoding
		{
			get
			{
				return this._encoding;
			}
		}

		// Token: 0x17000932 RID: 2354
		// (get) Token: 0x06002E11 RID: 11793 RVA: 0x0015B88B File Offset: 0x0015AA8B
		public bool MustFlush
		{
			get
			{
				return this._mustFlush;
			}
		}

		// Token: 0x17000933 RID: 2355
		// (get) Token: 0x06002E12 RID: 11794 RVA: 0x0015B893 File Offset: 0x0015AA93
		internal bool HasLeftoverData
		{
			get
			{
				return this._charLeftOver != '\0' || (this._fallbackBuffer != null && this._fallbackBuffer.Remaining > 0);
			}
		}

		// Token: 0x17000934 RID: 2356
		// (get) Token: 0x06002E13 RID: 11795 RVA: 0x0015B8B7 File Offset: 0x0015AAB7
		internal virtual bool HasState
		{
			get
			{
				return this._charLeftOver > '\0';
			}
		}

		// Token: 0x06002E14 RID: 11796 RVA: 0x0015B8C2 File Offset: 0x0015AAC2
		internal void ClearMustFlush()
		{
			this._mustFlush = false;
		}

		// Token: 0x06002E15 RID: 11797 RVA: 0x0015B8CC File Offset: 0x0015AACC
		internal unsafe int DrainLeftoverDataForGetByteCount(ReadOnlySpan<char> chars, out int charsConsumed)
		{
			if (this._fallbackBuffer != null && this._fallbackBuffer.Remaining > 0)
			{
				throw new ArgumentException(SR.Format(SR.Argument_EncoderFallbackNotEmpty, this.Encoding.EncodingName, this._fallbackBuffer.GetType()));
			}
			charsConsumed = 0;
			if (this._charLeftOver == '\0')
			{
				return 0;
			}
			char c = '\0';
			if (chars.IsEmpty)
			{
				if (!this.MustFlush)
				{
					return 0;
				}
			}
			else
			{
				c = (char)(*chars[0]);
			}
			Rune value;
			if (Rune.TryCreate(this._charLeftOver, c, out value))
			{
				charsConsumed = 1;
				int result;
				if (this._encoding.TryGetByteCount(value, out result))
				{
					return result;
				}
				bool flag = base.FallbackBuffer.Fallback(this._charLeftOver, c, -1);
			}
			else
			{
				bool flag = base.FallbackBuffer.Fallback(this._charLeftOver, -1);
			}
			return this._fallbackBuffer.DrainRemainingDataForGetByteCount();
		}

		// Token: 0x06002E16 RID: 11798 RVA: 0x0015B99C File Offset: 0x0015AB9C
		internal unsafe bool TryDrainLeftoverDataForGetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, out int charsConsumed, out int bytesWritten)
		{
			charsConsumed = 0;
			bytesWritten = 0;
			if (this._charLeftOver != '\0')
			{
				char c = '\0';
				if (chars.IsEmpty)
				{
					if (!this.MustFlush)
					{
						charsConsumed = 0;
						bytesWritten = 0;
						return true;
					}
				}
				else
				{
					c = (char)(*chars[0]);
				}
				char charLeftOver = this._charLeftOver;
				this._charLeftOver = '\0';
				Rune value;
				if (Rune.TryCreate(charLeftOver, c, out value))
				{
					charsConsumed = 1;
					switch (this._encoding.EncodeRune(value, bytes, out bytesWritten))
					{
					case OperationStatus.Done:
						return true;
					case OperationStatus.DestinationTooSmall:
						this._encoding.ThrowBytesOverflow(this, true);
						break;
					case OperationStatus.InvalidData:
						base.FallbackBuffer.Fallback(charLeftOver, c, -1);
						break;
					}
				}
				else
				{
					base.FallbackBuffer.Fallback(charLeftOver, -1);
				}
			}
			return this._fallbackBuffer == null || this._fallbackBuffer.Remaining <= 0 || this._fallbackBuffer.TryDrainRemainingDataForGetBytes(bytes, out bytesWritten);
		}

		// Token: 0x04000CB5 RID: 3253
		internal char _charLeftOver;

		// Token: 0x04000CB6 RID: 3254
		private readonly Encoding _encoding;

		// Token: 0x04000CB7 RID: 3255
		private bool _mustFlush;

		// Token: 0x04000CB8 RID: 3256
		internal bool _throwOnOverflow;

		// Token: 0x04000CB9 RID: 3257
		internal int _charsUsed;
	}
}
