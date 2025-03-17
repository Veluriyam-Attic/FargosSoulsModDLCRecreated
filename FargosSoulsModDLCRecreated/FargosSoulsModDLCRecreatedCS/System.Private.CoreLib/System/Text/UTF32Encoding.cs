using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000388 RID: 904
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class UTF32Encoding : Encoding
	{
		// Token: 0x06002FBA RID: 12218 RVA: 0x00162B33 File Offset: 0x00161D33
		public UTF32Encoding() : this(false, true)
		{
		}

		// Token: 0x06002FBB RID: 12219 RVA: 0x00162B3D File Offset: 0x00161D3D
		public UTF32Encoding(bool bigEndian, bool byteOrderMark) : base(bigEndian ? 12001 : 12000)
		{
			this._bigEndian = bigEndian;
			this._emitUTF32ByteOrderMark = byteOrderMark;
		}

		// Token: 0x06002FBC RID: 12220 RVA: 0x00162B62 File Offset: 0x00161D62
		public UTF32Encoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidCharacters) : this(bigEndian, byteOrderMark)
		{
			this._isThrowException = throwOnInvalidCharacters;
			if (this._isThrowException)
			{
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x06002FBD RID: 12221 RVA: 0x00162B84 File Offset: 0x00161D84
		internal override void SetDefaultFallbacks()
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

		// Token: 0x06002FBE RID: 12222 RVA: 0x001611A8 File Offset: 0x001603A8
		public unsafe override int GetByteCount(char[] chars, int index, int count)
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
			if (count == 0)
			{
				return 0;
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
			return this.GetByteCount(ptr + index, count, null);
		}

		// Token: 0x06002FBF RID: 12223 RVA: 0x00161230 File Offset: 0x00160430
		public unsafe override int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
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
			char* pChars = ptr;
			return this.GetByteCount(pChars, s.Length, null);
		}

		// Token: 0x06002FC0 RID: 12224 RVA: 0x0016126A File Offset: 0x0016046A
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe override int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", SR.ArgumentNull_Array);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this.GetByteCount(chars, count, null);
		}

		// Token: 0x06002FC1 RID: 12225 RVA: 0x001612A0 File Offset: 0x001604A0
		public unsafe override int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null || bytes == null)
			{
				throw new ArgumentNullException((s == null) ? "s" : "bytes", SR.ArgumentNull_Array);
			}
			if (charIndex < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((charIndex < 0) ? "charIndex" : "charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (s.Length - charIndex < charCount)
			{
				throw new ArgumentOutOfRangeException("s", SR.ArgumentOutOfRange_IndexCount);
			}
			if (byteIndex < 0 || byteIndex > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("byteIndex", SR.ArgumentOutOfRange_Index);
			}
			int byteCount = bytes.Length - byteIndex;
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
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr3 = reference;
				return this.GetBytes(ptr2 + charIndex, charCount, ptr3 + byteIndex, byteCount, null);
			}
		}

		// Token: 0x06002FC2 RID: 12226 RVA: 0x00161368 File Offset: 0x00160568
		public unsafe override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex)
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
			if (charCount == 0)
			{
				return 0;
			}
			int byteCount = bytes.Length - byteIndex;
			char* ptr;
			if (chars == null || chars.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &chars[0];
			}
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr2 = reference;
				return this.GetBytes(ptr + charIndex, charCount, ptr2 + byteIndex, byteCount, null);
			}
		}

		// Token: 0x06002FC3 RID: 12227 RVA: 0x00161438 File Offset: 0x00160638
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", SR.ArgumentNull_Array);
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this.GetBytes(chars, charCount, bytes, byteCount, null);
		}

		// Token: 0x06002FC4 RID: 12228 RVA: 0x0016149C File Offset: 0x0016069C
		public unsafe override int GetCharCount(byte[] bytes, int index, int count)
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
			if (count == 0)
			{
				return 0;
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
			return this.GetCharCount(ptr + index, count, null);
		}

		// Token: 0x06002FC5 RID: 12229 RVA: 0x0016151F File Offset: 0x0016071F
		[CLSCompliant(false)]
		[NullableContext(0)]
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this.GetCharCount(bytes, count, null);
		}

		// Token: 0x06002FC6 RID: 12230 RVA: 0x00161554 File Offset: 0x00160754
		public unsafe override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
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
			if (byteCount == 0)
			{
				return 0;
			}
			int charCount = chars.Length - charIndex;
			byte* ptr;
			if (bytes == null || bytes.Length == 0)
			{
				ptr = null;
			}
			else
			{
				ptr = &bytes[0];
			}
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr2 = reference;
				return this.GetChars(ptr + byteIndex, byteCount, ptr2 + charIndex, charCount, null);
			}
		}

		// Token: 0x06002FC7 RID: 12231 RVA: 0x00161624 File Offset: 0x00160824
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", SR.ArgumentNull_Array);
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return this.GetChars(bytes, byteCount, chars, charCount, null);
		}

		// Token: 0x06002FC8 RID: 12232 RVA: 0x00161688 File Offset: 0x00160888
		public unsafe override string GetString(byte[] bytes, int index, int count)
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

		// Token: 0x06002FC9 RID: 12233 RVA: 0x00162BD0 File Offset: 0x00161DD0
		internal unsafe override int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			char* ptr = chars + count;
			char* charStart = chars;
			int num = 0;
			char c = '\0';
			EncoderFallbackBuffer encoderFallbackBuffer;
			if (encoder != null)
			{
				c = encoder._charLeftOver;
				encoderFallbackBuffer = encoder.FallbackBuffer;
				if (encoderFallbackBuffer.Remaining > 0)
				{
					string argument_EncoderFallbackNotEmpty = SR.Argument_EncoderFallbackNotEmpty;
					object encodingName = this.EncodingName;
					EncoderFallback fallback = encoder.Fallback;
					throw new ArgumentException(SR.Format(argument_EncoderFallbackNotEmpty, encodingName, ((fallback != null) ? fallback.GetType().ToString() : null) ?? string.Empty));
				}
			}
			else
			{
				encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
			}
			encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
			for (;;)
			{
				char c2;
				if ((c2 = encoderFallbackBuffer.InternalGetNextChar()) == '\0' && chars >= ptr)
				{
					if ((encoder != null && !encoder.MustFlush) || c <= '\0')
					{
						break;
					}
					char* ptr2 = chars;
					encoderFallbackBuffer.InternalFallback(c, ref ptr2);
					chars = ptr2;
					c = '\0';
				}
				else
				{
					if (c2 == '\0')
					{
						c2 = *chars;
						chars++;
					}
					if (c != '\0')
					{
						if (char.IsLowSurrogate(c2))
						{
							c = '\0';
							num += 4;
						}
						else
						{
							chars--;
							char* ptr2 = chars;
							encoderFallbackBuffer.InternalFallback(c, ref ptr2);
							chars = ptr2;
							c = '\0';
						}
					}
					else if (char.IsHighSurrogate(c2))
					{
						c = c2;
					}
					else if (char.IsLowSurrogate(c2))
					{
						char* ptr2 = chars;
						encoderFallbackBuffer.InternalFallback(c2, ref ptr2);
						chars = ptr2;
					}
					else
					{
						num += 4;
					}
				}
			}
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_GetByteCountOverflow);
			}
			return num;
		}

		// Token: 0x06002FCA RID: 12234 RVA: 0x00162D10 File Offset: 0x00161F10
		internal unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			char* ptr = chars;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			byte* ptr4 = bytes + byteCount;
			char c = '\0';
			EncoderFallbackBuffer encoderFallbackBuffer;
			if (encoder != null)
			{
				c = encoder._charLeftOver;
				encoderFallbackBuffer = encoder.FallbackBuffer;
				if (encoder._throwOnOverflow && encoderFallbackBuffer.Remaining > 0)
				{
					string argument_EncoderFallbackNotEmpty = SR.Argument_EncoderFallbackNotEmpty;
					object encodingName = this.EncodingName;
					EncoderFallback fallback = encoder.Fallback;
					throw new ArgumentException(SR.Format(argument_EncoderFallbackNotEmpty, encodingName, (fallback != null) ? fallback.GetType() : null));
				}
			}
			else
			{
				encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
			}
			encoderFallbackBuffer.InternalInitialize(ptr, ptr2, encoder, true);
			for (;;)
			{
				char c2;
				char* ptr5;
				if ((c2 = encoderFallbackBuffer.InternalGetNextChar()) != '\0' || chars < ptr2)
				{
					if (c2 == '\0')
					{
						c2 = *chars;
						chars++;
					}
					if (c != '\0')
					{
						if (!char.IsLowSurrogate(c2))
						{
							chars--;
							ptr5 = chars;
							encoderFallbackBuffer.InternalFallback(c, ref ptr5);
							chars = ptr5;
							c = '\0';
							continue;
						}
						uint surrogate = this.GetSurrogate(c, c2);
						c = '\0';
						if (bytes + 3 >= ptr4)
						{
							if (encoderFallbackBuffer.bFallingBack)
							{
								encoderFallbackBuffer.MovePrevious();
								encoderFallbackBuffer.MovePrevious();
							}
							else
							{
								chars -= 2;
							}
							base.ThrowBytesOverflow(encoder, bytes == ptr3);
							c = '\0';
						}
						else
						{
							if (this._bigEndian)
							{
								*(bytes++) = 0;
								*(bytes++) = (byte)(surrogate >> 16);
								*(bytes++) = (byte)(surrogate >> 8);
								*(bytes++) = (byte)surrogate;
								continue;
							}
							*(bytes++) = (byte)surrogate;
							*(bytes++) = (byte)(surrogate >> 8);
							*(bytes++) = (byte)(surrogate >> 16);
							*(bytes++) = 0;
							continue;
						}
					}
					else
					{
						if (char.IsHighSurrogate(c2))
						{
							c = c2;
							continue;
						}
						if (char.IsLowSurrogate(c2))
						{
							ptr5 = chars;
							encoderFallbackBuffer.InternalFallback(c2, ref ptr5);
							chars = ptr5;
							continue;
						}
						if (bytes + 3 >= ptr4)
						{
							if (encoderFallbackBuffer.bFallingBack)
							{
								encoderFallbackBuffer.MovePrevious();
							}
							else
							{
								chars--;
							}
							base.ThrowBytesOverflow(encoder, bytes == ptr3);
						}
						else
						{
							if (this._bigEndian)
							{
								*(bytes++) = 0;
								*(bytes++) = 0;
								*(bytes++) = (byte)(c2 >> 8);
								*(bytes++) = (byte)c2;
								continue;
							}
							*(bytes++) = (byte)c2;
							*(bytes++) = (byte)(c2 >> 8);
							*(bytes++) = 0;
							*(bytes++) = 0;
							continue;
						}
					}
				}
				if ((encoder != null && !encoder.MustFlush) || c <= '\0')
				{
					break;
				}
				ptr5 = chars;
				encoderFallbackBuffer.InternalFallback(c, ref ptr5);
				chars = ptr5;
				c = '\0';
			}
			if (encoder != null)
			{
				encoder._charLeftOver = c;
				encoder._charsUsed = (int)((long)(chars - ptr));
			}
			return (int)((long)(bytes - ptr3));
		}

		// Token: 0x06002FCB RID: 12235 RVA: 0x00162FB0 File Offset: 0x001621B0
		internal unsafe override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			UTF32Encoding.UTF32Decoder utf32Decoder = (UTF32Encoding.UTF32Decoder)baseDecoder;
			int num = 0;
			byte* ptr = bytes + count;
			byte* byteStart = bytes;
			int i = 0;
			uint num2 = 0U;
			DecoderFallbackBuffer decoderFallbackBuffer;
			if (utf32Decoder != null)
			{
				i = utf32Decoder.readByteCount;
				num2 = (uint)utf32Decoder.iChar;
				decoderFallbackBuffer = utf32Decoder.FallbackBuffer;
			}
			else
			{
				decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
			}
			decoderFallbackBuffer.InternalInitialize(byteStart, null);
			while (bytes < ptr && num >= 0)
			{
				if (this._bigEndian)
				{
					num2 <<= 8;
					num2 += (uint)(*(bytes++));
				}
				else
				{
					num2 >>= 8;
					num2 += (uint)((uint)(*(bytes++)) << 24);
				}
				i++;
				if (i >= 4)
				{
					i = 0;
					if (num2 > 1114111U || (num2 >= 55296U && num2 <= 57343U))
					{
						byte[] bytes2;
						if (this._bigEndian)
						{
							bytes2 = new byte[]
							{
								(byte)(num2 >> 24),
								(byte)(num2 >> 16),
								(byte)(num2 >> 8),
								(byte)num2
							};
						}
						else
						{
							bytes2 = new byte[]
							{
								(byte)num2,
								(byte)(num2 >> 8),
								(byte)(num2 >> 16),
								(byte)(num2 >> 24)
							};
						}
						num += decoderFallbackBuffer.InternalFallback(bytes2, bytes);
						num2 = 0U;
					}
					else
					{
						if (num2 >= 65536U)
						{
							num++;
						}
						num++;
						num2 = 0U;
					}
				}
			}
			if (i > 0 && (utf32Decoder == null || utf32Decoder.MustFlush))
			{
				byte[] array = new byte[i];
				if (this._bigEndian)
				{
					while (i > 0)
					{
						array[--i] = (byte)num2;
						num2 >>= 8;
					}
				}
				else
				{
					while (i > 0)
					{
						array[--i] = (byte)(num2 >> 24);
						num2 <<= 8;
					}
				}
				num += decoderFallbackBuffer.InternalFallback(array, bytes);
			}
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_GetByteCountOverflow);
			}
			return num;
		}

		// Token: 0x06002FCC RID: 12236 RVA: 0x00163170 File Offset: 0x00162370
		internal unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			UTF32Encoding.UTF32Decoder utf32Decoder = (UTF32Encoding.UTF32Decoder)baseDecoder;
			char* ptr = chars;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			byte* ptr4 = bytes + byteCount;
			int num = 0;
			uint num2 = 0U;
			DecoderFallbackBuffer decoderFallbackBuffer;
			if (utf32Decoder != null)
			{
				num = utf32Decoder.readByteCount;
				num2 = (uint)utf32Decoder.iChar;
				decoderFallbackBuffer = baseDecoder.FallbackBuffer;
			}
			else
			{
				decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
			}
			decoderFallbackBuffer.InternalInitialize(bytes, chars + charCount);
			while (bytes < ptr4)
			{
				if (this._bigEndian)
				{
					num2 <<= 8;
					num2 += (uint)(*(bytes++));
				}
				else
				{
					num2 >>= 8;
					num2 += (uint)((uint)(*(bytes++)) << 24);
				}
				num++;
				if (num >= 4)
				{
					num = 0;
					if (num2 > 1114111U || (num2 >= 55296U && num2 <= 57343U))
					{
						byte[] bytes2;
						if (this._bigEndian)
						{
							bytes2 = new byte[]
							{
								(byte)(num2 >> 24),
								(byte)(num2 >> 16),
								(byte)(num2 >> 8),
								(byte)num2
							};
						}
						else
						{
							bytes2 = new byte[]
							{
								(byte)num2,
								(byte)(num2 >> 8),
								(byte)(num2 >> 16),
								(byte)(num2 >> 24)
							};
						}
						char* ptr5 = chars;
						bool flag = decoderFallbackBuffer.InternalFallback(bytes2, bytes, ref ptr5);
						chars = ptr5;
						if (!flag)
						{
							bytes -= 4;
							num2 = 0U;
							decoderFallbackBuffer.InternalReset();
							base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
							break;
						}
						num2 = 0U;
					}
					else
					{
						if (num2 >= 65536U)
						{
							if (chars >= ptr2 - 1)
							{
								bytes -= 4;
								num2 = 0U;
								base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
								break;
							}
							*(chars++) = this.GetHighSurrogate(num2);
							num2 = (uint)this.GetLowSurrogate(num2);
						}
						else if (chars >= ptr2)
						{
							bytes -= 4;
							num2 = 0U;
							base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
							break;
						}
						*(chars++) = (char)num2;
						num2 = 0U;
					}
				}
			}
			if (num > 0 && (utf32Decoder == null || utf32Decoder.MustFlush))
			{
				byte[] array = new byte[num];
				int i = num;
				if (this._bigEndian)
				{
					while (i > 0)
					{
						array[--i] = (byte)num2;
						num2 >>= 8;
					}
				}
				else
				{
					while (i > 0)
					{
						array[--i] = (byte)(num2 >> 24);
						num2 <<= 8;
					}
				}
				char* ptr5 = chars;
				bool flag2 = decoderFallbackBuffer.InternalFallback(array, bytes, ref ptr5);
				chars = ptr5;
				if (!flag2)
				{
					decoderFallbackBuffer.InternalReset();
					base.ThrowCharsOverflow(utf32Decoder, chars == ptr);
				}
				else
				{
					num = 0;
					num2 = 0U;
				}
			}
			if (utf32Decoder != null)
			{
				utf32Decoder.iChar = (int)num2;
				utf32Decoder.readByteCount = num;
				utf32Decoder._bytesUsed = (int)((long)(bytes - ptr3));
			}
			return (int)((long)(chars - ptr));
		}

		// Token: 0x06002FCD RID: 12237 RVA: 0x001633FB File Offset: 0x001625FB
		private uint GetSurrogate(char cHigh, char cLow)
		{
			return (uint)((cHigh - '\ud800') * 'Ѐ' + (cLow - '\udc00')) + 65536U;
		}

		// Token: 0x06002FCE RID: 12238 RVA: 0x00163418 File Offset: 0x00162618
		private char GetHighSurrogate(uint iChar)
		{
			return (char)((iChar - 65536U) / 1024U + 55296U);
		}

		// Token: 0x06002FCF RID: 12239 RVA: 0x0016342E File Offset: 0x0016262E
		private char GetLowSurrogate(uint iChar)
		{
			return (char)((iChar - 65536U) % 1024U + 56320U);
		}

		// Token: 0x06002FD0 RID: 12240 RVA: 0x00163444 File Offset: 0x00162644
		public override Decoder GetDecoder()
		{
			return new UTF32Encoding.UTF32Decoder(this);
		}

		// Token: 0x06002FD1 RID: 12241 RVA: 0x00158330 File Offset: 0x00157530
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}

		// Token: 0x06002FD2 RID: 12242 RVA: 0x0016344C File Offset: 0x0016264C
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
			num *= 4L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_GetByteCountOverflow);
			}
			return (int)num;
		}

		// Token: 0x06002FD3 RID: 12243 RVA: 0x001634B0 File Offset: 0x001626B0
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			int num = byteCount / 2 + 2;
			if (base.DecoderFallback.MaxCharCount > 2)
			{
				num *= base.DecoderFallback.MaxCharCount;
				num /= 2;
			}
			if (num > 2147483647)
			{
				throw new ArgumentOutOfRangeException("byteCount", SR.ArgumentOutOfRange_GetCharCountOverflow);
			}
			return num;
		}

		// Token: 0x06002FD4 RID: 12244 RVA: 0x00163510 File Offset: 0x00162710
		public override byte[] GetPreamble()
		{
			if (!this._emitUTF32ByteOrderMark)
			{
				return Array.Empty<byte>();
			}
			if (this._bigEndian)
			{
				return new byte[]
				{
					0,
					0,
					254,
					byte.MaxValue
				};
			}
			byte[] array = new byte[4];
			array[0] = byte.MaxValue;
			array[1] = 254;
			return array;
		}

		// Token: 0x1700096F RID: 2415
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x00163560 File Offset: 0x00162760
		[Nullable(0)]
		public unsafe override ReadOnlySpan<byte> Preamble
		{
			[NullableContext(0)]
			get
			{
				if (base.GetType() != typeof(UTF32Encoding))
				{
					return new ReadOnlySpan<byte>(this.GetPreamble());
				}
				if (!this._emitUTF32ByteOrderMark)
				{
					return default(ReadOnlySpan<byte>);
				}
				if (!this._bigEndian)
				{
					return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.0B856F2A1EA6FE59346BEC325DFE906BFA23BABE05EB10AC9FE7F5B46196AE71), 4);
				}
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F083DD8458E610A6B9650AC3F9C98822968EEDE9219C3424BE5FC457A290C50C), 4);
			}
		}

		// Token: 0x06002FD6 RID: 12246 RVA: 0x001635C4 File Offset: 0x001627C4
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			UTF32Encoding utf32Encoding = value as UTF32Encoding;
			return utf32Encoding != null && (this._emitUTF32ByteOrderMark == utf32Encoding._emitUTF32ByteOrderMark && this._bigEndian == utf32Encoding._bigEndian && base.EncoderFallback.Equals(utf32Encoding.EncoderFallback)) && base.DecoderFallback.Equals(utf32Encoding.DecoderFallback);
		}

		// Token: 0x06002FD7 RID: 12247 RVA: 0x0016361F File Offset: 0x0016281F
		public override int GetHashCode()
		{
			return base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode() + this.CodePage + (this._emitUTF32ByteOrderMark ? 4 : 0) + (this._bigEndian ? 8 : 0);
		}

		// Token: 0x04000D2A RID: 3370
		internal static readonly UTF32Encoding s_default = new UTF32Encoding(false, true);

		// Token: 0x04000D2B RID: 3371
		internal static readonly UTF32Encoding s_bigEndianDefault = new UTF32Encoding(true, true);

		// Token: 0x04000D2C RID: 3372
		private readonly bool _emitUTF32ByteOrderMark;

		// Token: 0x04000D2D RID: 3373
		private readonly bool _isThrowException;

		// Token: 0x04000D2E RID: 3374
		private readonly bool _bigEndian;

		// Token: 0x02000389 RID: 905
		private sealed class UTF32Decoder : DecoderNLS
		{
			// Token: 0x06002FD9 RID: 12249 RVA: 0x00163673 File Offset: 0x00162873
			public UTF32Decoder(UTF32Encoding encoding) : base(encoding)
			{
			}

			// Token: 0x06002FDA RID: 12250 RVA: 0x0016367C File Offset: 0x0016287C
			public override void Reset()
			{
				this.iChar = 0;
				this.readByteCount = 0;
				if (this._fallbackBuffer != null)
				{
					this._fallbackBuffer.Reset();
				}
			}

			// Token: 0x17000970 RID: 2416
			// (get) Token: 0x06002FDB RID: 12251 RVA: 0x0016369F File Offset: 0x0016289F
			internal override bool HasState
			{
				get
				{
					return this.readByteCount != 0;
				}
			}

			// Token: 0x04000D2F RID: 3375
			internal int iChar;

			// Token: 0x04000D30 RID: 3376
			internal int readByteCount;
		}
	}
}
