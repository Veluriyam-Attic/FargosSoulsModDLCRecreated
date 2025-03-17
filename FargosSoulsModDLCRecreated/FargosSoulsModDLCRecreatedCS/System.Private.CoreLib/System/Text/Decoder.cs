using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x0200035C RID: 860
	public abstract class Decoder
	{
		// Token: 0x17000911 RID: 2321
		// (get) Token: 0x06002D61 RID: 11617 RVA: 0x001596CE File Offset: 0x001588CE
		// (set) Token: 0x06002D62 RID: 11618 RVA: 0x001596D8 File Offset: 0x001588D8
		[Nullable(2)]
		public DecoderFallback Fallback
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

		// Token: 0x17000912 RID: 2322
		// (get) Token: 0x06002D63 RID: 11619 RVA: 0x00159727 File Offset: 0x00158927
		[Nullable(1)]
		public DecoderFallbackBuffer FallbackBuffer
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
						this._fallbackBuffer = DecoderFallback.ReplacementFallback.CreateFallbackBuffer();
					}
				}
				return this._fallbackBuffer;
			}
		}

		// Token: 0x17000913 RID: 2323
		// (get) Token: 0x06002D64 RID: 11620 RVA: 0x00159762 File Offset: 0x00158962
		internal bool InternalHasFallbackBuffer
		{
			get
			{
				return this._fallbackBuffer != null;
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x00159770 File Offset: 0x00158970
		public virtual void Reset()
		{
			byte[] bytes = Array.Empty<byte>();
			char[] chars = new char[this.GetCharCount(bytes, 0, 0, true)];
			this.GetChars(bytes, 0, 0, chars, 0, true);
			DecoderFallbackBuffer fallbackBuffer = this._fallbackBuffer;
			if (fallbackBuffer == null)
			{
				return;
			}
			fallbackBuffer.Reset();
		}

		// Token: 0x06002D66 RID: 11622
		[NullableContext(1)]
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		// Token: 0x06002D67 RID: 11623 RVA: 0x001597B0 File Offset: 0x001589B0
		[NullableContext(1)]
		public virtual int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			return this.GetCharCount(bytes, index, count);
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x001597BC File Offset: 0x001589BC
		[CLSCompliant(false)]
		public unsafe virtual int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				array[i] = bytes[i];
			}
			return this.GetCharCount(array, 0, count);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x00159818 File Offset: 0x00158A18
		public unsafe virtual int GetCharCount(ReadOnlySpan<byte> bytes, bool flush)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* bytes2 = nonNullPinnableReference;
				return this.GetCharCount(bytes2, bytes.Length, flush);
			}
		}

		// Token: 0x06002D6A RID: 11626
		[NullableContext(1)]
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		// Token: 0x06002D6B RID: 11627 RVA: 0x0015983E File Offset: 0x00158A3E
		[NullableContext(1)]
		public virtual int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x00159850 File Offset: 0x00158A50
		[CLSCompliant(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", SR.ArgumentNull_Array);
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			byte[] array = new byte[byteCount];
			for (int i = 0; i < byteCount; i++)
			{
				array[i] = bytes[i];
			}
			char[] array2 = new char[charCount];
			int chars2 = this.GetChars(array, 0, byteCount, array2, 0, flush);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			for (int i = 0; i < charCount; i++)
			{
				chars[i] = array2[i];
			}
			return charCount;
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x001598F8 File Offset: 0x00158AF8
		public unsafe virtual int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars, bool flush)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* bytes2 = nonNullPinnableReference;
				fixed (char* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
				{
					char* chars2 = nonNullPinnableReference2;
					return this.GetChars(bytes2, bytes.Length, chars2, chars.Length, flush);
				}
			}
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x00159930 File Offset: 0x00158B30
		[NullableContext(1)]
		public virtual void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", SR.ArgumentNull_Array);
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (chars.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("chars", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			for (bytesUsed = byteCount; bytesUsed > 0; bytesUsed /= 2)
			{
				if (this.GetCharCount(bytes, byteIndex, bytesUsed, flush) <= charCount)
				{
					charsUsed = this.GetChars(bytes, byteIndex, bytesUsed, chars, charIndex, flush);
					completed = (bytesUsed == byteCount && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0));
					return;
				}
				flush = false;
			}
			throw new ArgumentException(SR.Argument_ConversionOverflow);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x00159A48 File Offset: 0x00158C48
		[CLSCompliant(false)]
		public unsafe virtual void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", SR.ArgumentNull_Array);
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			for (bytesUsed = byteCount; bytesUsed > 0; bytesUsed /= 2)
			{
				if (this.GetCharCount(bytes, bytesUsed, flush) <= charCount)
				{
					charsUsed = this.GetChars(bytes, bytesUsed, chars, charCount, flush);
					completed = (bytesUsed == byteCount && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0));
					return;
				}
				flush = false;
			}
			throw new ArgumentException(SR.Argument_ConversionOverflow);
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x00159B08 File Offset: 0x00158D08
		public unsafe virtual void Convert(ReadOnlySpan<byte> bytes, Span<char> chars, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* bytes2 = nonNullPinnableReference;
				fixed (char* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
				{
					char* chars2 = nonNullPinnableReference2;
					this.Convert(bytes2, bytes.Length, chars2, chars.Length, flush, out bytesUsed, out charsUsed, out completed);
				}
			}
		}

		// Token: 0x04000C8B RID: 3211
		internal DecoderFallback _fallback;

		// Token: 0x04000C8C RID: 3212
		internal DecoderFallbackBuffer _fallbackBuffer;
	}
}
