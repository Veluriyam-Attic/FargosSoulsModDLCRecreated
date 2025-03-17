using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000358 RID: 856
	public class ASCIIEncoding : Encoding
	{
		// Token: 0x06002D1C RID: 11548 RVA: 0x0015795E File Offset: 0x00156B5E
		public ASCIIEncoding() : base(20127)
		{
		}

		// Token: 0x06002D1D RID: 11549 RVA: 0x0015796B File Offset: 0x00156B6B
		internal sealed override void SetDefaultFallbacks()
		{
			this.encoderFallback = EncoderFallback.ReplacementFallback;
			this.decoderFallback = DecoderFallback.ReplacementFallback;
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x00157984 File Offset: 0x00156B84
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

		// Token: 0x06002D1F RID: 11551 RVA: 0x001579EC File Offset: 0x00156BEC
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

		// Token: 0x06002D20 RID: 11552 RVA: 0x00157A21 File Offset: 0x00156C21
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

		// Token: 0x06002D21 RID: 11553 RVA: 0x00157A44 File Offset: 0x00156C44
		public unsafe override int GetByteCount(ReadOnlySpan<char> chars)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* pChars = reference;
				return this.GetByteCountCommon(pChars, chars.Length);
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x00157A6C File Offset: 0x00156C6C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe int GetByteCountCommon(char* pChars, int charCount)
		{
			int num2;
			int num = this.GetByteCountFast(pChars, charCount, base.EncoderFallback, out num2);
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

		// Token: 0x06002D23 RID: 11555 RVA: 0x00157AA4 File Offset: 0x00156CA4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetByteCountFast(char* pChars, int charsLength, EncoderFallback fallback, out int charsConsumed)
		{
			int num = charsLength;
			EncoderReplacementFallback encoderReplacementFallback = fallback as EncoderReplacementFallback;
			if (encoderReplacementFallback == null || encoderReplacementFallback.MaxCharCount != 1 || encoderReplacementFallback.DefaultString[0] > '\u007f')
			{
				num = (int)ASCIIUtility.GetIndexOfFirstNonAsciiChar(pChars, (UIntPtr)charsLength);
			}
			charsConsumed = num;
			return num;
		}

		// Token: 0x06002D24 RID: 11556 RVA: 0x00157AE8 File Offset: 0x00156CE8
		[NullableContext(1)]
		public unsafe override int GetBytes(string chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
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

		// Token: 0x06002D25 RID: 11557 RVA: 0x00157B8C File Offset: 0x00156D8C
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

		// Token: 0x06002D26 RID: 11558 RVA: 0x00157C32 File Offset: 0x00156E32
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

		// Token: 0x06002D27 RID: 11559 RVA: 0x00157C74 File Offset: 0x00156E74
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

		// Token: 0x06002D28 RID: 11560 RVA: 0x00157CAC File Offset: 0x00156EAC
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

		// Token: 0x06002D29 RID: 11561 RVA: 0x00157CDC File Offset: 0x00156EDC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetBytesFast(char* pChars, int charsLength, byte* pBytes, int bytesLength, out int charsConsumed)
		{
			int num = (int)ASCIIUtility.NarrowUtf16ToAscii(pChars, pBytes, (UIntPtr)Math.Min(charsLength, bytesLength));
			charsConsumed = num;
			return num;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x00157D00 File Offset: 0x00156F00
		private protected unsafe sealed override int GetBytesWithFallback(ReadOnlySpan<char> chars, int originalCharsLength, Span<byte> bytes, int originalBytesLength, EncoderNLS encoder)
		{
			EncoderReplacementFallback encoderReplacementFallback = ((encoder == null) ? base.EncoderFallback : encoder.Fallback) as EncoderReplacementFallback;
			if (encoderReplacementFallback != null && encoderReplacementFallback.MaxCharCount == 1 && encoderReplacementFallback.DefaultString[0] <= '\u007f')
			{
				byte b = (byte)encoderReplacementFallback.DefaultString[0];
				int num = Math.Min(chars.Length, bytes.Length);
				int i = 0;
				fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr = reference;
					fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
					{
						byte* ptr2 = reference2;
						while (i < num)
						{
							ptr2[i++] = b;
							if (i < num)
							{
								i += (int)ASCIIUtility.NarrowUtf16ToAscii(ptr + i, ptr2 + i, (UIntPtr)(num - i));
							}
						}
					}
				}
				chars = chars.Slice(num);
				bytes = bytes.Slice(num);
			}
			if (chars.IsEmpty)
			{
				return originalBytesLength - bytes.Length;
			}
			return base.GetBytesWithFallback(chars, originalCharsLength, bytes, originalBytesLength, encoder);
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x00157DF4 File Offset: 0x00156FF4
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

		// Token: 0x06002D2C RID: 11564 RVA: 0x00157E56 File Offset: 0x00157056
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

		// Token: 0x06002D2D RID: 11565 RVA: 0x00157E7C File Offset: 0x0015707C
		public unsafe override int GetCharCount(ReadOnlySpan<byte> bytes)
		{
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* pBytes = reference;
				return this.GetCharCountCommon(pBytes, bytes.Length);
			}
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x00157EA4 File Offset: 0x001570A4
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe int GetCharCountCommon(byte* pBytes, int byteCount)
		{
			int num2;
			int num = this.GetCharCountFast(pBytes, byteCount, base.DecoderFallback, out num2);
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

		// Token: 0x06002D2F RID: 11567 RVA: 0x00157EDC File Offset: 0x001570DC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetCharCountFast(byte* pBytes, int bytesLength, DecoderFallback fallback, out int bytesConsumed)
		{
			int num = bytesLength;
			DecoderReplacementFallback decoderReplacementFallback = fallback as DecoderReplacementFallback;
			if (decoderReplacementFallback == null || decoderReplacementFallback.MaxCharCount != 1)
			{
				num = (int)ASCIIUtility.GetIndexOfFirstNonAsciiByte(pBytes, (UIntPtr)bytesLength);
			}
			bytesConsumed = num;
			return num;
		}

		// Token: 0x06002D30 RID: 11568 RVA: 0x00157F10 File Offset: 0x00157110
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

		// Token: 0x06002D31 RID: 11569 RVA: 0x00157FB4 File Offset: 0x001571B4
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

		// Token: 0x06002D32 RID: 11570 RVA: 0x00157FF0 File Offset: 0x001571F0
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

		// Token: 0x06002D33 RID: 11571 RVA: 0x00158028 File Offset: 0x00157228
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

		// Token: 0x06002D34 RID: 11572 RVA: 0x00158058 File Offset: 0x00157258
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetCharsFast(byte* pBytes, int bytesLength, char* pChars, int charsLength, out int bytesConsumed)
		{
			int num = (int)ASCIIUtility.WidenAsciiToUtf16(pBytes, pChars, (UIntPtr)Math.Min(bytesLength, charsLength));
			bytesConsumed = num;
			return num;
		}

		// Token: 0x06002D35 RID: 11573 RVA: 0x0015807C File Offset: 0x0015727C
		private protected unsafe sealed override int GetCharsWithFallback(ReadOnlySpan<byte> bytes, int originalBytesLength, Span<char> chars, int originalCharsLength, DecoderNLS decoder)
		{
			DecoderReplacementFallback decoderReplacementFallback = ((decoder == null) ? base.DecoderFallback : decoder.Fallback) as DecoderReplacementFallback;
			if (decoderReplacementFallback != null && decoderReplacementFallback.MaxCharCount == 1)
			{
				char c = decoderReplacementFallback.DefaultString[0];
				int num = Math.Min(bytes.Length, chars.Length);
				int i = 0;
				fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* ptr = reference;
					fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
					{
						char* ptr2 = reference2;
						while (i < num)
						{
							ptr2[(IntPtr)(i++) * 2] = c;
							if (i < num)
							{
								i += (int)ASCIIUtility.WidenAsciiToUtf16(ptr + i, ptr2 + i, (UIntPtr)(num - i));
							}
						}
					}
				}
				bytes = bytes.Slice(num);
				chars = chars.Slice(num);
			}
			if (bytes.IsEmpty)
			{
				return originalCharsLength - chars.Length;
			}
			return base.GetCharsWithFallback(bytes, originalBytesLength, chars, originalCharsLength, decoder);
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x00158160 File Offset: 0x00157360
		[NullableContext(1)]
		public unsafe override string GetString(byte[] bytes, int byteIndex, int byteCount)
		{
			if (bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.bytes, ExceptionResource.ArgumentNull_Array);
			}
			if ((byteIndex | byteCount) < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException((byteIndex < 0) ? ExceptionArgument.byteIndex : ExceptionArgument.byteCount, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (bytes.Length - byteIndex < byteCount)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.bytes, ExceptionResource.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (byteCount == 0)
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
			return string.CreateStringFromEncoding(ptr + byteIndex, byteCount, this);
		}

		// Token: 0x06002D37 RID: 11575 RVA: 0x001581CB File Offset: 0x001573CB
		internal sealed override bool TryGetByteCount(Rune value, out int byteCount)
		{
			if (value.IsAscii)
			{
				byteCount = 1;
				return true;
			}
			byteCount = 0;
			return false;
		}

		// Token: 0x06002D38 RID: 11576 RVA: 0x001581DF File Offset: 0x001573DF
		internal unsafe sealed override OperationStatus EncodeRune(Rune value, Span<byte> bytes, out int bytesWritten)
		{
			if (!value.IsAscii)
			{
				bytesWritten = 0;
				return OperationStatus.InvalidData;
			}
			if (!bytes.IsEmpty)
			{
				*bytes[0] = (byte)value.Value;
				bytesWritten = 1;
				return OperationStatus.Done;
			}
			bytesWritten = 0;
			return OperationStatus.DestinationTooSmall;
		}

		// Token: 0x06002D39 RID: 11577 RVA: 0x00158214 File Offset: 0x00157414
		internal unsafe sealed override OperationStatus DecodeFirstRune(ReadOnlySpan<byte> bytes, out Rune value, out int bytesConsumed)
		{
			if (bytes.IsEmpty)
			{
				value = Rune.ReplacementChar;
				bytesConsumed = 0;
				return OperationStatus.NeedMoreData;
			}
			byte b = *bytes[0];
			if (b <= 127)
			{
				value = new Rune((int)b);
				bytesConsumed = 1;
				return OperationStatus.Done;
			}
			value = Rune.ReplacementChar;
			bytesConsumed = 1;
			return OperationStatus.InvalidData;
		}

		// Token: 0x06002D3A RID: 11578 RVA: 0x0015826C File Offset: 0x0015746C
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
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_GetByteCountOverflow);
			}
			return (int)num;
		}

		// Token: 0x06002D3B RID: 11579 RVA: 0x001582CC File Offset: 0x001574CC
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			long num = (long)byteCount;
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

		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x06002D3C RID: 11580 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsSingleByte
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002D3D RID: 11581 RVA: 0x00158328 File Offset: 0x00157528
		[NullableContext(1)]
		public override Decoder GetDecoder()
		{
			return new DecoderNLS(this);
		}

		// Token: 0x06002D3E RID: 11582 RVA: 0x00158330 File Offset: 0x00157530
		[NullableContext(1)]
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}

		// Token: 0x04000C84 RID: 3204
		internal static readonly ASCIIEncoding.ASCIIEncodingSealed s_default = new ASCIIEncoding.ASCIIEncodingSealed();

		// Token: 0x02000359 RID: 857
		internal sealed class ASCIIEncodingSealed : ASCIIEncoding
		{
			// Token: 0x06002D40 RID: 11584 RVA: 0x00158344 File Offset: 0x00157544
			public override object Clone()
			{
				return new ASCIIEncoding
				{
					IsReadOnly = false
				};
			}
		}
	}
}
