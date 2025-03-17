using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000365 RID: 869
	public abstract class Encoder
	{
		// Token: 0x17000922 RID: 2338
		// (get) Token: 0x06002DBE RID: 11710 RVA: 0x0015A892 File Offset: 0x00159A92
		// (set) Token: 0x06002DBF RID: 11711 RVA: 0x0015A89C File Offset: 0x00159A9C
		[Nullable(2)]
		public EncoderFallback Fallback
		{
			[NullableContext(2)]
			get
			{
				return this._fallback;
			}
			[NullableContext(2)]
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (this._fallbackBuffer != null && this._fallbackBuffer.Remaining > 0)
				{
					throw new ArgumentException(SR.Argument_FallbackBufferNotEmpty, "value");
				}
				this._fallback = value;
				this._fallbackBuffer = null;
			}
		}

		// Token: 0x17000923 RID: 2339
		// (get) Token: 0x06002DC0 RID: 11712 RVA: 0x0015A8EB File Offset: 0x00159AEB
		[Nullable(1)]
		public EncoderFallbackBuffer FallbackBuffer
		{
			[NullableContext(1)]
			get
			{
				if (this._fallbackBuffer == null)
				{
					if (this._fallback != null)
					{
						this._fallbackBuffer = this._fallback.CreateFallbackBuffer();
					}
					else
					{
						this._fallbackBuffer = EncoderFallback.ReplacementFallback.CreateFallbackBuffer();
					}
				}
				return this._fallbackBuffer;
			}
		}

		// Token: 0x17000924 RID: 2340
		// (get) Token: 0x06002DC1 RID: 11713 RVA: 0x0015A926 File Offset: 0x00159B26
		internal bool InternalHasFallbackBuffer
		{
			get
			{
				return this._fallbackBuffer != null;
			}
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x0015A934 File Offset: 0x00159B34
		public virtual void Reset()
		{
			char[] chars = Array.Empty<char>();
			byte[] bytes = new byte[this.GetByteCount(chars, 0, 0, true)];
			this.GetBytes(chars, 0, 0, bytes, 0, true);
			if (this._fallbackBuffer != null)
			{
				this._fallbackBuffer.Reset();
			}
		}

		// Token: 0x06002DC3 RID: 11715
		[NullableContext(1)]
		public abstract int GetByteCount(char[] chars, int index, int count, bool flush);

		// Token: 0x06002DC4 RID: 11716 RVA: 0x0015A978 File Offset: 0x00159B78
		[CLSCompliant(false)]
		public unsafe virtual int GetByteCount(char* chars, int count, bool flush)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", SR.ArgumentNull_Array);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			char[] array = new char[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = chars[i];
			}
			return this.GetByteCount(array, 0, count, flush);
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x0015A9D8 File Offset: 0x00159BD8
		public unsafe virtual int GetByteCount(ReadOnlySpan<char> chars, bool flush)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* chars2 = nonNullPinnableReference;
				return this.GetByteCount(chars2, chars.Length, flush);
			}
		}

		// Token: 0x06002DC6 RID: 11718
		[NullableContext(1)]
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush);

		// Token: 0x06002DC7 RID: 11719 RVA: 0x0015AA00 File Offset: 0x00159C00
		[CLSCompliant(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", SR.ArgumentNull_Array);
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			char[] array = new char[charCount];
			for (int i = 0; i < charCount; i++)
			{
				array[i] = chars[i];
			}
			byte[] array2 = new byte[byteCount];
			int bytes2 = this.GetBytes(array, 0, charCount, array2, 0, flush);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			for (int i = 0; i < byteCount; i++)
			{
				bytes[i] = array2[i];
			}
			return byteCount;
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x0015AAA8 File Offset: 0x00159CA8
		public unsafe virtual int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes, bool flush)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* chars2 = nonNullPinnableReference;
				fixed (byte* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
				{
					byte* bytes2 = nonNullPinnableReference2;
					return this.GetBytes(chars2, chars.Length, bytes2, bytes.Length, flush);
				}
			}
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x0015AAE0 File Offset: 0x00159CE0
		[NullableContext(1)]
		public virtual void Convert(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
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
			for (charsUsed = charCount; charsUsed > 0; charsUsed /= 2)
			{
				if (this.GetByteCount(chars, charIndex, charsUsed, flush) <= byteCount)
				{
					bytesUsed = this.GetBytes(chars, charIndex, charsUsed, bytes, byteIndex, flush);
					completed = (charsUsed == charCount && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0));
					return;
				}
				flush = false;
			}
			throw new ArgumentException(SR.Argument_ConversionOverflow);
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x0015ABF8 File Offset: 0x00159DF8
		[CLSCompliant(false)]
		public unsafe virtual void Convert(char* chars, int charCount, byte* bytes, int byteCount, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", SR.ArgumentNull_Array);
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			for (charsUsed = charCount; charsUsed > 0; charsUsed /= 2)
			{
				if (this.GetByteCount(chars, charsUsed, flush) <= byteCount)
				{
					bytesUsed = this.GetBytes(chars, charsUsed, bytes, byteCount, flush);
					completed = (charsUsed == charCount && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0));
					return;
				}
				flush = false;
			}
			throw new ArgumentException(SR.Argument_ConversionOverflow);
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x0015ACB8 File Offset: 0x00159EB8
		public unsafe virtual void Convert(ReadOnlySpan<char> chars, Span<byte> bytes, bool flush, out int charsUsed, out int bytesUsed, out bool completed)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* chars2 = nonNullPinnableReference;
				fixed (byte* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
				{
					byte* bytes2 = nonNullPinnableReference2;
					this.Convert(chars2, chars.Length, bytes2, bytes.Length, flush, out charsUsed, out bytesUsed, out completed);
				}
			}
		}

		// Token: 0x04000CA0 RID: 3232
		internal EncoderFallback _fallback;

		// Token: 0x04000CA1 RID: 3233
		internal EncoderFallbackBuffer _fallbackBuffer;
	}
}
