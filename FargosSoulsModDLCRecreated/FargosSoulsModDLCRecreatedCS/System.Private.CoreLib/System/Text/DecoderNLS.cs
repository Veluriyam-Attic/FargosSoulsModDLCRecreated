using System;
using System.Buffers;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000362 RID: 866
	internal class DecoderNLS : Decoder
	{
		// Token: 0x06002D9A RID: 11674 RVA: 0x00159FE7 File Offset: 0x001591E7
		internal DecoderNLS(Encoding encoding)
		{
			this._encoding = encoding;
			this._fallback = this._encoding.DecoderFallback;
			this.Reset();
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x0015A00D File Offset: 0x0015920D
		public override void Reset()
		{
			this.ClearLeftoverData();
			DecoderFallbackBuffer fallbackBuffer = this._fallbackBuffer;
			if (fallbackBuffer == null)
			{
				return;
			}
			fallbackBuffer.Reset();
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x0015A025 File Offset: 0x00159225
		public override int GetCharCount(byte[] bytes, int index, int count)
		{
			return this.GetCharCount(bytes, index, count, false);
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x0015A034 File Offset: 0x00159234
		public unsafe override int GetCharCount(byte[] bytes, int index, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (bytes.Length - index < count)
			{
				throw new ArgumentOutOfRangeException("bytes", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				return this.GetCharCount(ptr + index, count, flush);
			}
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x0015A0AC File Offset: 0x001592AC
		public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			this._mustFlush = flush;
			this._throwOnOverflow = true;
			return this._encoding.GetCharCount(bytes, count, this);
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x0015A0FE File Offset: 0x001592FE
		public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x0015A110 File Offset: 0x00159310
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", SR.ArgumentNull_Array);
			}
			if (byteIndex < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteIndex < 0) ? "byteIndex" : "byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				throw new ArgumentOutOfRangeException("bytes", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (charIndex < 0 || charIndex > chars.Length)
			{
				throw new ArgumentOutOfRangeException("charIndex", SR.ArgumentOutOfRange_Index);
			}
			int charCount = chars.Length - charIndex;
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr2 = reference2;
					return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush);
				}
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x0015A1D4 File Offset: 0x001593D4
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
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
			return this._encoding.GetChars(bytes, byteCount, chars, charCount, this);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x0015A24C File Offset: 0x0015944C
		public unsafe override void Convert(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
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
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr2 = reference2;
					this.Convert(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, flush, out bytesUsed, out charsUsed, out completed);
				}
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x0015A338 File Offset: 0x00159538
		public unsafe override void Convert(byte* bytes, int byteCount, char* chars, int charCount, bool flush, out int bytesUsed, out int charsUsed, out bool completed)
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
			this._throwOnOverflow = false;
			this._bytesUsed = 0;
			charsUsed = this._encoding.GetChars(bytes, byteCount, chars, charCount, this);
			bytesUsed = this._bytesUsed;
			completed = (bytesUsed == byteCount && (!flush || !this.HasState) && (this._fallbackBuffer == null || this._fallbackBuffer.Remaining == 0));
		}

		// Token: 0x1700091C RID: 2332
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x0015A3F3 File Offset: 0x001595F3
		public bool MustFlush
		{
			get
			{
				return this._mustFlush;
			}
		}

		// Token: 0x1700091D RID: 2333
		// (get) Token: 0x06002DA5 RID: 11685 RVA: 0x0015A3FB File Offset: 0x001595FB
		internal virtual bool HasState
		{
			get
			{
				return this._leftoverByteCount != 0;
			}
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x0015A406 File Offset: 0x00159606
		internal void ClearMustFlush()
		{
			this._mustFlush = false;
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x0015A410 File Offset: 0x00159610
		internal ReadOnlySpan<byte> GetLeftoverData()
		{
			return MemoryMarshal.AsBytes<int>(new ReadOnlySpan<int>(ref this._leftoverBytes, 1)).Slice(0, this._leftoverByteCount);
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x0015A43D File Offset: 0x0015963D
		internal void SetLeftoverData(ReadOnlySpan<byte> bytes)
		{
			bytes.CopyTo(MemoryMarshal.AsBytes<int>(new Span<int>(ref this._leftoverBytes, 1)));
			this._leftoverByteCount = bytes.Length;
		}

		// Token: 0x1700091E RID: 2334
		// (get) Token: 0x06002DA9 RID: 11689 RVA: 0x0015A3FB File Offset: 0x001595FB
		internal bool HasLeftoverData
		{
			get
			{
				return this._leftoverByteCount != 0;
			}
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x0015A464 File Offset: 0x00159664
		internal void ClearLeftoverData()
		{
			this._leftoverByteCount = 0;
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x0015A470 File Offset: 0x00159670
		internal unsafe int DrainLeftoverDataForGetCharCount(ReadOnlySpan<byte> bytes, out int bytesConsumed)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)4], 4);
			Span<byte> span2 = span;
			span2 = span2.Slice(0, DecoderNLS.ConcatInto(this.GetLeftoverData(), bytes, span2));
			int result = 0;
			Rune rune;
			int num;
			switch (this._encoding.DecodeFirstRune(span2, out rune, out num))
			{
			case OperationStatus.Done:
				result = rune.Utf16SequenceLength;
				goto IL_9B;
			case OperationStatus.NeedMoreData:
				if (!this.MustFlush)
				{
					goto IL_9B;
				}
				break;
			}
			if (base.FallbackBuffer.Fallback(span2.Slice(0, num).ToArray(), -this._leftoverByteCount))
			{
				result = this._fallbackBuffer.DrainRemainingDataForGetCharCount();
			}
			IL_9B:
			bytesConsumed = num - this._leftoverByteCount;
			return result;
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x0015A524 File Offset: 0x00159724
		internal unsafe int DrainLeftoverDataForGetChars(ReadOnlySpan<byte> bytes, Span<char> chars, out int bytesConsumed)
		{
			Span<byte> span = new Span<byte>(stackalloc byte[(UIntPtr)4], 4);
			Span<byte> span2 = span;
			span2 = span2.Slice(0, DecoderNLS.ConcatInto(this.GetLeftoverData(), bytes, span2));
			int result = 0;
			bool flag = false;
			Rune rune;
			int num;
			switch (this._encoding.DecodeFirstRune(span2, out rune, out num))
			{
			case OperationStatus.Done:
				if (rune.TryEncodeToUtf16(chars, out result))
				{
					goto IL_AA;
				}
				goto IL_CE;
			case OperationStatus.NeedMoreData:
				if (!this.MustFlush)
				{
					flag = true;
					goto IL_AA;
				}
				break;
			}
			if (base.FallbackBuffer.Fallback(span2.Slice(0, num).ToArray(), -this._leftoverByteCount) && !this._fallbackBuffer.TryDrainRemainingDataForGetChars(chars, out result))
			{
				goto IL_CE;
			}
			IL_AA:
			bytesConsumed = num - this._leftoverByteCount;
			if (flag)
			{
				this.SetLeftoverData(span2);
			}
			else
			{
				this.ClearLeftoverData();
			}
			return result;
			IL_CE:
			this._encoding.ThrowCharsOverflow(this, true);
			throw null;
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x0015A610 File Offset: 0x00159810
		private unsafe static int ConcatInto(ReadOnlySpan<byte> srcLeft, ReadOnlySpan<byte> srcRight, Span<byte> dest)
		{
			int num = 0;
			for (int i = 0; i < srcLeft.Length; i++)
			{
				if (num >= dest.Length)
				{
					return num;
				}
				*dest[num++] = *srcLeft[i];
			}
			int num2 = 0;
			while (num2 < srcRight.Length && num < dest.Length)
			{
				*dest[num++] = *srcRight[num2];
				num2++;
			}
			return num;
		}

		// Token: 0x04000C95 RID: 3221
		private readonly Encoding _encoding;

		// Token: 0x04000C96 RID: 3222
		private bool _mustFlush;

		// Token: 0x04000C97 RID: 3223
		internal bool _throwOnOverflow;

		// Token: 0x04000C98 RID: 3224
		internal int _bytesUsed;

		// Token: 0x04000C99 RID: 3225
		private int _leftoverBytes;

		// Token: 0x04000C9A RID: 3226
		private int _leftoverByteCount;
	}
}
