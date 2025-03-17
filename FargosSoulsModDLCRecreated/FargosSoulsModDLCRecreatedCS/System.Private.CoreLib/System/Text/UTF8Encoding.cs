using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text.Unicode;

namespace System.Text
{
	// Token: 0x0200038F RID: 911
	public class UTF8Encoding : Encoding
	{
		// Token: 0x17000975 RID: 2421
		// (get) Token: 0x06003008 RID: 12296 RVA: 0x00163DAC File Offset: 0x00162FAC
		internal unsafe static ReadOnlySpan<byte> PreambleSpan
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F1945CD6C19E56B3C1C78943EF5EC18116907A4CA1EFC40A57D48AB1DB7ADFC5), 3);
			}
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x00163DB9 File Offset: 0x00162FB9
		public UTF8Encoding() : this(false)
		{
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x00163DC2 File Offset: 0x00162FC2
		public UTF8Encoding(bool encoderShouldEmitUTF8Identifier) : base(65001)
		{
			this._emitUTF8Identifier = encoderShouldEmitUTF8Identifier;
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x00163DD6 File Offset: 0x00162FD6
		public UTF8Encoding(bool encoderShouldEmitUTF8Identifier, bool throwOnInvalidBytes) : this(encoderShouldEmitUTF8Identifier)
		{
			this._isThrowException = throwOnInvalidBytes;
			if (this._isThrowException)
			{
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x00163DF4 File Offset: 0x00162FF4
		internal sealed override void SetDefaultFallbacks()
		{
			if (this._isThrowException)
			{
				this.encoderFallback = EncoderFallback.ExceptionFallback;
				this.decoderFallback = DecoderFallback.ExceptionFallback;
				return;
			}
			this.encoderFallback = new EncoderReplacementFallback("�");
			this.decoderFallback = new DecoderReplacementFallback("�");
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x00163E40 File Offset: 0x00163040
		[NullableContext(1)]
		public unsafe override int GetByteCount(char[] chars, int index, int count)
		{
			if (chars == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.chars, ExceptionResource.ArgumentNull_Array);
			}
			if ((index | count) < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException((index < 0) ? ExceptionArgument.index : ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (chars.Length - index < count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.chars, ExceptionResource.ArgumentOutOfRange_IndexCountBuffer);
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			return this.GetByteCountCommon(ptr + index, count);
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x00163EA8 File Offset: 0x001630A8
		[NullableContext(1)]
		public unsafe override int GetByteCount(string chars)
		{
			if (chars == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.chars);
			}
			char* ptr;
			if (chars == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = chars.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* pChars = ptr;
			return this.GetByteCountCommon(pChars, chars.Length);
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x00163EDD File Offset: 0x001630DD
		[CLSCompliant(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.chars);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this.GetByteCountCommon(chars, count);
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x00163F00 File Offset: 0x00163100
		public unsafe override int GetByteCount(ReadOnlySpan<char> chars)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* pChars = reference;
				return this.GetByteCountCommon(pChars, chars.Length);
			}
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x0015E1CC File Offset: 0x0015D3CC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe int GetByteCountCommon(char* pChars, int charCount)
		{
			int num2;
			int num = this.GetByteCountFast(pChars, charCount, null, out num2);
			if (num2 != charCount)
			{
				num += base.GetByteCountWithFallback(pChars, charCount, num2);
				if (num < 0)
				{
					Encoding.ThrowConversionOverflow();
				}
			}
			return num;
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x00163F28 File Offset: 0x00163128
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetByteCountFast(char* pChars, int charsLength, EncoderFallback fallback, out int charsConsumed)
		{
			long num;
			int num2;
			char* pointerToFirstInvalidChar = Utf16Utility.GetPointerToFirstInvalidChar(pChars, charsLength, out num, out num2);
			int num3 = (int)((long)(pointerToFirstInvalidChar - pChars));
			charsConsumed = num3;
			long num4 = (long)num3 + num;
			if (num4 > 2147483647L)
			{
				Encoding.ThrowConversionOverflow();
			}
			return (int)num4;
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x00163F64 File Offset: 0x00163164
		[NullableContext(1)]
		public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null || bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException((s == null) ? ExceptionArgument.s : ExceptionArgument.bytes, ExceptionResource.ArgumentNull_Array);
			}
			if ((charIndex | charCount) < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException((charIndex < 0) ? ExceptionArgument.charIndex : ExceptionArgument.charCount, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (s.Length - charIndex < charCount)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.s, ExceptionResource.ArgumentOutOfRange_IndexCount);
			}
			if ((ulong)byteIndex > (ulong)((long)bytes.Length))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.byteIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			char* ptr;
			if (s == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = s.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* ptr2 = ptr;
			byte* ptr3;
			if (bytes == null || bytes.Length == 0)
			{
				ptr3 = null;
			}
			else
			{
				ptr3 = &bytes[0];
			}
			return this.GetBytesCommon(ptr2 + charIndex, charCount, ptr3 + byteIndex, bytes.Length - byteIndex);
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x00164008 File Offset: 0x00163208
		[NullableContext(1)]
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (chars == null || bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException((chars == null) ? ExceptionArgument.chars : ExceptionArgument.bytes, ExceptionResource.ArgumentNull_Array);
			}
			if ((charIndex | charCount) < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException((charIndex < 0) ? ExceptionArgument.charIndex : ExceptionArgument.charCount, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (chars.Length - charIndex < charCount)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.chars, ExceptionResource.ArgumentOutOfRange_IndexCount);
			}
			if ((ulong)byteIndex > (ulong)((long)bytes.Length))
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.byteIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			byte* ptr2;
			if (bytes == null || bytes.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &bytes[0];
			}
			return this.GetBytesCommon(ptr + charIndex, charCount, ptr2 + byteIndex, bytes.Length - byteIndex);
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x001640AE File Offset: 0x001632AE
		[CLSCompliant(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (chars == null || bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException((chars == null) ? ExceptionArgument.chars : ExceptionArgument.bytes, ExceptionResource.ArgumentNull_Array);
			}
			if ((charCount | byteCount) < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException((charCount < 0) ? ExceptionArgument.charCount : ExceptionArgument.byteCount, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this.GetBytesCommon(chars, charCount, bytes, byteCount);
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x001640F0 File Offset: 0x001632F0
		public unsafe override int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* pChars = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* pBytes = reference2;
					return this.GetBytesCommon(pChars, chars.Length, pBytes, bytes.Length);
				}
			}
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x00157CAC File Offset: 0x00156EAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe int GetBytesCommon(char* pChars, int charCount, byte* pBytes, int byteCount)
		{
			int num;
			int bytesFast = this.GetBytesFast(pChars, charCount, pBytes, byteCount, out num);
			if (num == charCount)
			{
				return bytesFast;
			}
			return base.GetBytesWithFallback(pChars, charCount, pBytes, byteCount, num, bytesFast);
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x00164128 File Offset: 0x00163328
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetBytesFast(char* pChars, int charsLength, byte* pBytes, int bytesLength, out int charsConsumed)
		{
			char* ptr;
			byte* ptr2;
			Utf8Utility.TranscodeToUtf8(pChars, charsLength, pBytes, bytesLength, out ptr, out ptr2);
			charsConsumed = (int)((long)(ptr - pChars));
			return (int)((long)(ptr2 - pBytes));
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x00164158 File Offset: 0x00163358
		[NullableContext(1)]
		public unsafe override int GetCharCount(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.bytes, ExceptionResource.ArgumentNull_Array);
			}
			if ((index | count) < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException((index < 0) ? ExceptionArgument.index : ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (bytes.Length - index < count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.bytes, ExceptionResource.ArgumentOutOfRange_IndexCountBuffer);
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return this.GetCharCountCommon(ptr + index, count);
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x001641BA File Offset: 0x001633BA
		[CLSCompliant(false)]
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.bytes, ExceptionResource.ArgumentNull_Array);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this.GetCharCountCommon(bytes, count);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x001641E0 File Offset: 0x001633E0
		public unsafe override int GetCharCount(ReadOnlySpan<byte> bytes)
		{
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* pBytes = reference;
				return this.GetCharCountCommon(pBytes, bytes.Length);
			}
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x00164208 File Offset: 0x00163408
		[NullableContext(1)]
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
		{
			if (bytes == null || chars == null)
			{
				ThrowHelper.ThrowArgumentNullException((bytes == null) ? ExceptionArgument.bytes : ExceptionArgument.chars, ExceptionResource.ArgumentNull_Array);
			}
			if ((byteIndex | byteCount) < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException((byteIndex < 0) ? ExceptionArgument.byteIndex : ExceptionArgument.byteCount, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.bytes, ExceptionResource.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (charIndex > chars.Length)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.charIndex, ExceptionResource.ArgumentOutOfRange_Index);
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			char* ptr2;
			if (chars == null || chars.Length == 0)
			{
				ptr2 = null;
			}
			else
			{
				ptr2 = &chars[0];
			}
			return this.GetCharsCommon(ptr + byteIndex, byteCount, ptr2 + charIndex, chars.Length - charIndex);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x001642AC File Offset: 0x001634AC
		[CLSCompliant(false)]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (bytes == null || chars == null)
			{
				ThrowHelper.ThrowArgumentNullException((bytes == null) ? ExceptionArgument.bytes : ExceptionArgument.chars, ExceptionResource.ArgumentNull_Array);
			}
			if ((byteCount | charCount) < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException((byteCount < 0) ? ExceptionArgument.byteCount : ExceptionArgument.charCount, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this.GetCharsCommon(bytes, byteCount, chars, charCount);
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x001642E8 File Offset: 0x001634E8
		public unsafe override int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars)
		{
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* pBytes = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* pChars = reference2;
					return this.GetCharsCommon(pBytes, bytes.Length, pChars, chars.Length);
				}
			}
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x00158028 File Offset: 0x00157228
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe int GetCharsCommon(byte* pBytes, int byteCount, char* pChars, int charCount)
		{
			int num;
			int charsFast = this.GetCharsFast(pBytes, byteCount, pChars, charCount, out num);
			if (num == byteCount)
			{
				return charsFast;
			}
			return base.GetCharsWithFallback(pBytes, byteCount, pChars, charCount, num, charsFast);
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x00164320 File Offset: 0x00163520
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetCharsFast(byte* pBytes, int bytesLength, char* pChars, int charsLength, out int bytesConsumed)
		{
			byte* ptr;
			char* ptr2;
			Utf8Utility.TranscodeToUtf16(pBytes, bytesLength, pChars, charsLength, out ptr, out ptr2);
			bytesConsumed = (int)((long)(ptr - pBytes));
			return (int)((long)(ptr2 - pChars));
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x00164350 File Offset: 0x00163550
		private protected sealed override int GetCharsWithFallback(ReadOnlySpan<byte> bytes, int originalBytesLength, Span<char> chars, int originalCharsLength, DecoderNLS decoder)
		{
			DecoderReplacementFallback decoderReplacementFallback = ((decoder == null) ? base.DecoderFallback : decoder.Fallback) as DecoderReplacementFallback;
			if (decoderReplacementFallback != null && decoderReplacementFallback.MaxCharCount == 1 && decoderReplacementFallback.DefaultString[0] == '�')
			{
				int start;
				int start2;
				Utf8.ToUtf16(bytes, chars, out start, out start2, true, decoder == null || decoder.MustFlush);
				bytes = bytes.Slice(start);
				chars = chars.Slice(start2);
			}
			if (bytes.IsEmpty)
			{
				return originalCharsLength - chars.Length;
			}
			return base.GetCharsWithFallback(bytes, originalBytesLength, chars, originalCharsLength, decoder);
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x001643E8 File Offset: 0x001635E8
		[NullableContext(1)]
		public unsafe override string GetString(byte[] bytes, int index, int count)
		{
			if (bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.bytes, ExceptionResource.ArgumentNull_Array);
			}
			if ((index | count) < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException((index < 0) ? ExceptionArgument.index : ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (bytes.Length - index < count)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.bytes, ExceptionResource.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (count == 0)
			{
				return string.Empty;
			}
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			return string.CreateStringFromEncoding(ptr + index, count, this);
		}

		// Token: 0x06003023 RID: 12323 RVA: 0x00164454 File Offset: 0x00163654
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe int GetCharCountCommon(byte* pBytes, int byteCount)
		{
			int num2;
			int num = this.GetCharCountFast(pBytes, byteCount, null, out num2);
			if (num2 != byteCount)
			{
				num += base.GetCharCountWithFallback(pBytes, byteCount, num2);
				if (num < 0)
				{
					Encoding.ThrowConversionOverflow();
				}
			}
			return num;
		}

		// Token: 0x06003024 RID: 12324 RVA: 0x00164488 File Offset: 0x00163688
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetCharCountFast(byte* pBytes, int bytesLength, DecoderFallback fallback, out int bytesConsumed)
		{
			int num;
			int num2;
			byte* pointerToFirstInvalidByte = Utf8Utility.GetPointerToFirstInvalidByte(pBytes, bytesLength, out num, out num2);
			int num3 = (int)((long)(pointerToFirstInvalidByte - pBytes));
			bytesConsumed = num3;
			return num3 + num;
		}

		// Token: 0x06003025 RID: 12325 RVA: 0x00158328 File Offset: 0x00157528
		[NullableContext(1)]
		public override Decoder GetDecoder()
		{
			return new DecoderNLS(this);
		}

		// Token: 0x06003026 RID: 12326 RVA: 0x00158330 File Offset: 0x00157530
		[NullableContext(1)]
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}

		// Token: 0x06003027 RID: 12327 RVA: 0x001644B0 File Offset: 0x001636B0
		internal sealed override bool TryGetByteCount(Rune value, out int byteCount)
		{
			byteCount = value.Utf8SequenceLength;
			return true;
		}

		// Token: 0x06003028 RID: 12328 RVA: 0x001644BC File Offset: 0x001636BC
		internal sealed override OperationStatus EncodeRune(Rune value, Span<byte> bytes, out int bytesWritten)
		{
			if (!value.TryEncodeToUtf8(bytes, out bytesWritten))
			{
				return OperationStatus.DestinationTooSmall;
			}
			return OperationStatus.Done;
		}

		// Token: 0x06003029 RID: 12329 RVA: 0x001644CC File Offset: 0x001636CC
		internal sealed override OperationStatus DecodeFirstRune(ReadOnlySpan<byte> bytes, out Rune value, out int bytesConsumed)
		{
			return Rune.DecodeFromUtf8(bytes, out value, out bytesConsumed);
		}

		// Token: 0x0600302A RID: 12330 RVA: 0x001644D8 File Offset: 0x001636D8
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			long num = (long)charCount + 1L;
			if (base.EncoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.EncoderFallback.MaxCharCount;
			}
			num *= 3L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_GetByteCountOverflow);
			}
			return (int)num;
		}

		// Token: 0x0600302B RID: 12331 RVA: 0x0016453C File Offset: 0x0016373C
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			long num = (long)byteCount + 1L;
			if (base.DecoderFallback.MaxCharCount > 1)
			{
				num *= (long)base.DecoderFallback.MaxCharCount;
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("byteCount", SR.ArgumentOutOfRange_GetCharCountOverflow);
			}
			return (int)num;
		}

		// Token: 0x0600302C RID: 12332 RVA: 0x0016459B File Offset: 0x0016379B
		[NullableContext(1)]
		public override byte[] GetPreamble()
		{
			if (this._emitUTF8Identifier)
			{
				return new byte[]
				{
					239,
					187,
					191
				};
			}
			return Array.Empty<byte>();
		}

		// Token: 0x17000976 RID: 2422
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x001645BC File Offset: 0x001637BC
		public override ReadOnlySpan<byte> Preamble
		{
			get
			{
				if (base.GetType() != typeof(UTF8Encoding))
				{
					return new ReadOnlySpan<byte>(this.GetPreamble());
				}
				if (!this._emitUTF8Identifier)
				{
					return default(ReadOnlySpan<byte>);
				}
				return UTF8Encoding.PreambleSpan;
			}
		}

		// Token: 0x0600302E RID: 12334 RVA: 0x00164604 File Offset: 0x00163804
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			UTF8Encoding utf8Encoding = value as UTF8Encoding;
			return utf8Encoding != null && (this._emitUTF8Identifier == utf8Encoding._emitUTF8Identifier && base.EncoderFallback.Equals(utf8Encoding.EncoderFallback)) && base.DecoderFallback.Equals(utf8Encoding.DecoderFallback);
		}

		// Token: 0x0600302F RID: 12335 RVA: 0x00164651 File Offset: 0x00163851
		public override int GetHashCode()
		{
			return base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode() + 65001 + (this._emitUTF8Identifier ? 1 : 0);
		}

		// Token: 0x04000D3E RID: 3390
		internal static readonly UTF8Encoding.UTF8EncodingSealed s_default = new UTF8Encoding.UTF8EncodingSealed(true);

		// Token: 0x04000D3F RID: 3391
		private readonly bool _emitUTF8Identifier;

		// Token: 0x04000D40 RID: 3392
		private readonly bool _isThrowException;

		// Token: 0x02000390 RID: 912
		internal sealed class UTF8EncodingSealed : UTF8Encoding
		{
			// Token: 0x06003031 RID: 12337 RVA: 0x0016468A File Offset: 0x0016388A
			public UTF8EncodingSealed(bool encoderShouldEmitUTF8Identifier) : base(encoderShouldEmitUTF8Identifier)
			{
			}

			// Token: 0x17000977 RID: 2423
			// (get) Token: 0x06003032 RID: 12338 RVA: 0x00164694 File Offset: 0x00163894
			public override ReadOnlySpan<byte> Preamble
			{
				get
				{
					if (!this._emitUTF8Identifier)
					{
						return default(ReadOnlySpan<byte>);
					}
					return UTF8Encoding.PreambleSpan;
				}
			}

			// Token: 0x06003033 RID: 12339 RVA: 0x001646B8 File Offset: 0x001638B8
			public override object Clone()
			{
				return new UTF8Encoding(this._emitUTF8Identifier)
				{
					IsReadOnly = false
				};
			}

			// Token: 0x06003034 RID: 12340 RVA: 0x001646CC File Offset: 0x001638CC
			public override byte[] GetBytes(string s)
			{
				if (s != null && s.Length <= 32)
				{
					return this.GetBytesForSmallInput(s);
				}
				return base.GetBytes(s);
			}

			// Token: 0x06003035 RID: 12341 RVA: 0x001646EC File Offset: 0x001638EC
			private unsafe byte[] GetBytesForSmallInput(string s)
			{
				byte* ptr = stackalloc byte[(UIntPtr)96];
				int length = s.Length;
				char* ptr2;
				if (s == null)
				{
					ptr2 = null;
				}
				else
				{
					fixed (char* ptr3 = s.GetPinnableReference())
					{
						ptr2 = ptr3;
					}
				}
				char* pChars = ptr2;
				int bytesCommon = base.GetBytesCommon(pChars, length, ptr, 96);
				char* ptr3 = null;
				return new Span<byte>(ref *ptr, bytesCommon).ToArray();
			}

			// Token: 0x06003036 RID: 12342 RVA: 0x00164739 File Offset: 0x00163939
			public override string GetString(byte[] bytes)
			{
				if (bytes != null && bytes.Length <= 32)
				{
					return this.GetStringForSmallInput(bytes);
				}
				return base.GetString(bytes);
			}

			// Token: 0x06003037 RID: 12343 RVA: 0x00164754 File Offset: 0x00163954
			private unsafe string GetStringForSmallInput(byte[] bytes)
			{
				char* ptr = stackalloc char[(UIntPtr)64];
				int byteCount = bytes.Length;
				int charsCommon;
				fixed (byte[] array = bytes)
				{
					byte* pBytes;
					if (bytes == null || array.Length == 0)
					{
						pBytes = null;
					}
					else
					{
						pBytes = &array[0];
					}
					charsCommon = base.GetCharsCommon(pBytes, byteCount, ptr, 32);
				}
				return new string(new ReadOnlySpan<char>(ref *ptr, charsCommon));
			}
		}
	}
}
