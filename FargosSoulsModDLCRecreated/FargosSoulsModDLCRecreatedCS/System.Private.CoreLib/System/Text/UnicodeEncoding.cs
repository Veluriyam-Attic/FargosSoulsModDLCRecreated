using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Internal.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000385 RID: 901
	[Nullable(0)]
	[NullableContext(1)]
	public class UnicodeEncoding : Encoding
	{
		// Token: 0x06002F8E RID: 12174 RVA: 0x0016110E File Offset: 0x0016030E
		public UnicodeEncoding() : this(false, true)
		{
		}

		// Token: 0x06002F8F RID: 12175 RVA: 0x00161118 File Offset: 0x00160318
		public UnicodeEncoding(bool bigEndian, bool byteOrderMark) : base(bigEndian ? 1201 : 1200)
		{
			this.bigEndian = bigEndian;
			this.byteOrderMark = byteOrderMark;
		}

		// Token: 0x06002F90 RID: 12176 RVA: 0x0016113D File Offset: 0x0016033D
		public UnicodeEncoding(bool bigEndian, bool byteOrderMark, bool throwOnInvalidBytes) : this(bigEndian, byteOrderMark)
		{
			this.isThrowException = throwOnInvalidBytes;
			if (this.isThrowException)
			{
				this.SetDefaultFallbacks();
			}
		}

		// Token: 0x06002F91 RID: 12177 RVA: 0x0016115C File Offset: 0x0016035C
		internal sealed override void SetDefaultFallbacks()
		{
			if (this.isThrowException)
			{
				this.encoderFallback = EncoderFallback.ExceptionFallback;
				this.decoderFallback = DecoderFallback.ExceptionFallback;
				return;
			}
			this.encoderFallback = new EncoderReplacementFallback("�");
			this.decoderFallback = new DecoderReplacementFallback("�");
		}

		// Token: 0x06002F92 RID: 12178 RVA: 0x001611A8 File Offset: 0x001603A8
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

		// Token: 0x06002F93 RID: 12179 RVA: 0x00161230 File Offset: 0x00160430
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

		// Token: 0x06002F94 RID: 12180 RVA: 0x0016126A File Offset: 0x0016046A
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

		// Token: 0x06002F95 RID: 12181 RVA: 0x001612A0 File Offset: 0x001604A0
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

		// Token: 0x06002F96 RID: 12182 RVA: 0x00161368 File Offset: 0x00160568
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

		// Token: 0x06002F97 RID: 12183 RVA: 0x00161438 File Offset: 0x00160638
		[CLSCompliant(false)]
		[NullableContext(0)]
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

		// Token: 0x06002F98 RID: 12184 RVA: 0x0016149C File Offset: 0x0016069C
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

		// Token: 0x06002F99 RID: 12185 RVA: 0x0016151F File Offset: 0x0016071F
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

		// Token: 0x06002F9A RID: 12186 RVA: 0x00161554 File Offset: 0x00160754
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

		// Token: 0x06002F9B RID: 12187 RVA: 0x00161624 File Offset: 0x00160824
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

		// Token: 0x06002F9C RID: 12188 RVA: 0x00161688 File Offset: 0x00160888
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

		// Token: 0x06002F9D RID: 12189 RVA: 0x00161710 File Offset: 0x00160910
		internal unsafe sealed override int GetByteCount(char* chars, int count, EncoderNLS encoder)
		{
			int num = count << 1;
			if (num < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_GetByteCountOverflow);
			}
			char* charStart = chars;
			char* ptr = chars + count;
			char c = '\0';
			bool flag = false;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			if (encoder != null)
			{
				c = encoder._charLeftOver;
				if (c > '\0')
				{
					num += 2;
				}
				if (encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0)
					{
						string argument_EncoderFallbackNotEmpty = SR.Argument_EncoderFallbackNotEmpty;
						object encodingName = this.EncodingName;
						EncoderFallback fallback = encoder.Fallback;
						throw new ArgumentException(SR.Format(argument_EncoderFallbackNotEmpty, encodingName, (fallback != null) ? fallback.GetType() : null));
					}
					encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
				}
			}
			for (;;)
			{
				char c2;
				char* ptr4;
				if ((c2 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr)
				{
					if (c2 == '\0')
					{
						if ((this.bigEndian ^ BitConverter.IsLittleEndian) && (chars & 7L) == null && c == '\0')
						{
							ulong* ptr2 = (ulong*)(ptr - 3);
							ulong* ptr3;
							for (ptr3 = (ulong*)chars; ptr3 < ptr2; ptr3++)
							{
								if ((9223512776490647552UL & *ptr3) != 0UL)
								{
									ulong num2 = (17870556004450629632UL & *ptr3) ^ 15564677810327967744UL;
									if (((num2 & 18446462598732840960UL) == 0UL || (num2 & 281470681743360UL) == 0UL || (num2 & (ulong)-65536) == 0UL || (num2 & 65535UL) == 0UL) && (18158790778715962368UL & *ptr3) != (BitConverter.IsLittleEndian ? 15852908186546788352UL : 15564682208374479872UL))
									{
										break;
									}
								}
							}
							chars = (char*)ptr3;
							if (chars >= ptr)
							{
								goto IL_2A0;
							}
						}
						c2 = *chars;
						chars++;
					}
					else
					{
						num += 2;
					}
					if (c2 >= '\ud800' && c2 <= '\udfff')
					{
						if (c2 <= '\udbff')
						{
							if (c > '\0')
							{
								chars--;
								num -= 2;
								if (encoderFallbackBuffer == null)
								{
									if (encoder == null)
									{
										encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
									}
									else
									{
										encoderFallbackBuffer = encoder.FallbackBuffer;
									}
									encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
								}
								ptr4 = chars;
								encoderFallbackBuffer.InternalFallback(c, ref ptr4);
								chars = ptr4;
								c = '\0';
								continue;
							}
							c = c2;
							continue;
						}
						else
						{
							if (c == '\0')
							{
								num -= 2;
								if (encoderFallbackBuffer == null)
								{
									if (encoder == null)
									{
										encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
									}
									else
									{
										encoderFallbackBuffer = encoder.FallbackBuffer;
									}
									encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
								}
								ptr4 = chars;
								encoderFallbackBuffer.InternalFallback(c2, ref ptr4);
								chars = ptr4;
								continue;
							}
							c = '\0';
							continue;
						}
					}
					else
					{
						if (c > '\0')
						{
							chars--;
							if (encoderFallbackBuffer == null)
							{
								if (encoder == null)
								{
									encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
								}
								else
								{
									encoderFallbackBuffer = encoder.FallbackBuffer;
								}
								encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
							}
							ptr4 = chars;
							encoderFallbackBuffer.InternalFallback(c, ref ptr4);
							chars = ptr4;
							num -= 2;
							c = '\0';
							continue;
						}
						continue;
					}
				}
				IL_2A0:
				if (c <= '\0')
				{
					return num;
				}
				num -= 2;
				if (encoder != null && !encoder.MustFlush)
				{
					return num;
				}
				if (flag)
				{
					break;
				}
				if (encoderFallbackBuffer == null)
				{
					if (encoder == null)
					{
						encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
					}
					else
					{
						encoderFallbackBuffer = encoder.FallbackBuffer;
					}
					encoderFallbackBuffer.InternalInitialize(charStart, ptr, encoder, false);
				}
				ptr4 = chars;
				encoderFallbackBuffer.InternalFallback(c, ref ptr4);
				chars = ptr4;
				c = '\0';
				flag = true;
			}
			throw new ArgumentException(SR.Format(SR.Argument_RecursiveFallback, c), "chars");
		}

		// Token: 0x06002F9E RID: 12190 RVA: 0x00161A38 File Offset: 0x00160C38
		internal unsafe sealed override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS encoder)
		{
			char c = '\0';
			bool flag = false;
			byte* ptr = bytes + byteCount;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			char* ptr4 = chars;
			EncoderFallbackBuffer encoderFallbackBuffer = null;
			if (encoder != null)
			{
				c = encoder._charLeftOver;
				if (encoder.InternalHasFallbackBuffer)
				{
					encoderFallbackBuffer = encoder.FallbackBuffer;
					if (encoderFallbackBuffer.Remaining > 0 && encoder._throwOnOverflow)
					{
						string argument_EncoderFallbackNotEmpty = SR.Argument_EncoderFallbackNotEmpty;
						object encodingName = this.EncodingName;
						EncoderFallback fallback = encoder.Fallback;
						throw new ArgumentException(SR.Format(argument_EncoderFallbackNotEmpty, encodingName, (fallback != null) ? fallback.GetType() : null));
					}
					encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, false);
				}
			}
			for (;;)
			{
				char c2;
				char* ptr8;
				if ((c2 = ((encoderFallbackBuffer == null) ? '\0' : encoderFallbackBuffer.InternalGetNextChar())) != '\0' || chars < ptr2)
				{
					if (c2 == '\0')
					{
						if ((this.bigEndian ^ BitConverter.IsLittleEndian) && (chars & 7L) == null && c == '\0')
						{
							ulong* ptr5 = (ulong*)(chars - 3 + (((long)(ptr - bytes) >> 1 < (long)(ptr2 - chars)) ? ((long)(ptr - bytes) >> 1) : ((long)(ptr2 - chars))) * 2L / 8L);
							ulong* ptr6 = (ulong*)chars;
							ulong* ptr7 = (ulong*)bytes;
							while (ptr6 < ptr5)
							{
								if ((9223512776490647552UL & *ptr6) != 0UL)
								{
									ulong num = (17870556004450629632UL & *ptr6) ^ 15564677810327967744UL;
									if (((num & 18446462598732840960UL) == 0UL || (num & 281470681743360UL) == 0UL || (num & (ulong)-65536) == 0UL || (num & 65535UL) == 0UL) && (18158790778715962368UL & *ptr6) != (BitConverter.IsLittleEndian ? 15852908186546788352UL : 15564682208374479872UL))
									{
										break;
									}
								}
								Unsafe.WriteUnaligned<ulong>((void*)ptr7, *ptr6);
								ptr6++;
								ptr7++;
							}
							chars = (char*)ptr6;
							bytes = (byte*)ptr7;
							if (chars >= ptr2)
							{
								goto IL_3AA;
							}
						}
						c2 = *chars;
						chars++;
					}
					if (c2 >= '\ud800' && c2 <= '\udfff')
					{
						if (c2 <= '\udbff')
						{
							if (c > '\0')
							{
								chars--;
								if (encoderFallbackBuffer == null)
								{
									if (encoder == null)
									{
										encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
									}
									else
									{
										encoderFallbackBuffer = encoder.FallbackBuffer;
									}
									encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
								}
								ptr8 = chars;
								encoderFallbackBuffer.InternalFallback(c, ref ptr8);
								chars = ptr8;
								c = '\0';
								continue;
							}
							c = c2;
							continue;
						}
						else
						{
							if (c == '\0')
							{
								if (encoderFallbackBuffer == null)
								{
									if (encoder == null)
									{
										encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
									}
									else
									{
										encoderFallbackBuffer = encoder.FallbackBuffer;
									}
									encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
								}
								ptr8 = chars;
								encoderFallbackBuffer.InternalFallback(c2, ref ptr8);
								chars = ptr8;
								continue;
							}
							if (bytes + 3 >= ptr)
							{
								if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
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
								goto IL_3AA;
							}
							if (this.bigEndian)
							{
								*(bytes++) = (byte)(c >> 8);
								*(bytes++) = (byte)c;
							}
							else
							{
								*(bytes++) = (byte)c;
								*(bytes++) = (byte)(c >> 8);
							}
							c = '\0';
						}
					}
					else if (c > '\0')
					{
						chars--;
						if (encoderFallbackBuffer == null)
						{
							if (encoder == null)
							{
								encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
							}
							else
							{
								encoderFallbackBuffer = encoder.FallbackBuffer;
							}
							encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
						}
						ptr8 = chars;
						encoderFallbackBuffer.InternalFallback(c, ref ptr8);
						chars = ptr8;
						c = '\0';
						continue;
					}
					if (bytes + 1 >= ptr)
					{
						if (encoderFallbackBuffer != null && encoderFallbackBuffer.bFallingBack)
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
						if (this.bigEndian)
						{
							*(bytes++) = (byte)(c2 >> 8);
							*(bytes++) = (byte)c2;
							continue;
						}
						*(bytes++) = (byte)c2;
						*(bytes++) = (byte)(c2 >> 8);
						continue;
					}
				}
				IL_3AA:
				if (c <= '\0' || (encoder != null && !encoder.MustFlush))
				{
					goto IL_422;
				}
				if (flag)
				{
					break;
				}
				if (encoderFallbackBuffer == null)
				{
					if (encoder == null)
					{
						encoderFallbackBuffer = this.encoderFallback.CreateFallbackBuffer();
					}
					else
					{
						encoderFallbackBuffer = encoder.FallbackBuffer;
					}
					encoderFallbackBuffer.InternalInitialize(ptr4, ptr2, encoder, true);
				}
				ptr8 = chars;
				encoderFallbackBuffer.InternalFallback(c, ref ptr8);
				chars = ptr8;
				c = '\0';
				flag = true;
			}
			throw new ArgumentException(SR.Format(SR.Argument_RecursiveFallback, c), "chars");
			IL_422:
			if (encoder != null)
			{
				encoder._charLeftOver = c;
				encoder._charsUsed = (int)((long)(chars - ptr4));
			}
			return (int)((long)(bytes - ptr3));
		}

		// Token: 0x06002F9F RID: 12191 RVA: 0x00161E8C File Offset: 0x0016108C
		internal unsafe sealed override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder)baseDecoder;
			byte* ptr = bytes + count;
			byte* byteStart = bytes;
			int num = -1;
			char c = '\0';
			int num2 = count >> 1;
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			if (decoder != null)
			{
				num = decoder.lastByte;
				c = decoder.lastChar;
				if (c > '\0')
				{
					num2++;
				}
				if (num >= 0 && (count & 1) == 1)
				{
					num2++;
				}
			}
			while (bytes < ptr)
			{
				if ((this.bigEndian ^ BitConverter.IsLittleEndian) && (bytes & 7L) == null && num == -1 && c == '\0')
				{
					ulong* ptr2 = (ulong*)(ptr - 7);
					ulong* ptr3;
					for (ptr3 = (ulong*)bytes; ptr3 < ptr2; ptr3++)
					{
						if ((9223512776490647552UL & *ptr3) != 0UL)
						{
							ulong num3 = (17870556004450629632UL & *ptr3) ^ 15564677810327967744UL;
							if (((num3 & 18446462598732840960UL) == 0UL || (num3 & 281470681743360UL) == 0UL || (num3 & (ulong)-65536) == 0UL || (num3 & 65535UL) == 0UL) && (18158790778715962368UL & *ptr3) != (BitConverter.IsLittleEndian ? 15852908186546788352UL : 15564682208374479872UL))
							{
								break;
							}
						}
					}
					bytes = (byte*)ptr3;
					if (bytes >= ptr)
					{
						break;
					}
				}
				if (num < 0)
				{
					num = (int)(*(bytes++));
					if (bytes >= ptr)
					{
						break;
					}
				}
				char c2;
				if (this.bigEndian)
				{
					c2 = (char)(num << 8 | (int)(*(bytes++)));
				}
				else
				{
					c2 = (char)((int)(*(bytes++)) << 8 | num);
				}
				num = -1;
				if (c2 >= '\ud800' && c2 <= '\udfff')
				{
					if (c2 <= '\udbff')
					{
						if (c > '\0')
						{
							num2--;
							byte[] bytes2;
							if (this.bigEndian)
							{
								bytes2 = new byte[]
								{
									(byte)(c >> 8),
									(byte)c
								};
							}
							else
							{
								bytes2 = new byte[]
								{
									(byte)c,
									(byte)(c >> 8)
								};
							}
							if (decoderFallbackBuffer == null)
							{
								if (decoder == null)
								{
									decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
								}
								else
								{
									decoderFallbackBuffer = decoder.FallbackBuffer;
								}
								decoderFallbackBuffer.InternalInitialize(byteStart, null);
							}
							num2 += decoderFallbackBuffer.InternalFallback(bytes2, bytes);
						}
						c = c2;
					}
					else if (c == '\0')
					{
						num2--;
						byte[] bytes3;
						if (this.bigEndian)
						{
							bytes3 = new byte[]
							{
								(byte)(c2 >> 8),
								(byte)c2
							};
						}
						else
						{
							bytes3 = new byte[]
							{
								(byte)c2,
								(byte)(c2 >> 8)
							};
						}
						if (decoderFallbackBuffer == null)
						{
							if (decoder == null)
							{
								decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
							}
							else
							{
								decoderFallbackBuffer = decoder.FallbackBuffer;
							}
							decoderFallbackBuffer.InternalInitialize(byteStart, null);
						}
						num2 += decoderFallbackBuffer.InternalFallback(bytes3, bytes);
					}
					else
					{
						c = '\0';
					}
				}
				else if (c > '\0')
				{
					num2--;
					byte[] bytes4;
					if (this.bigEndian)
					{
						bytes4 = new byte[]
						{
							(byte)(c >> 8),
							(byte)c
						};
					}
					else
					{
						bytes4 = new byte[]
						{
							(byte)c,
							(byte)(c >> 8)
						};
					}
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(byteStart, null);
					}
					num2 += decoderFallbackBuffer.InternalFallback(bytes4, bytes);
					c = '\0';
				}
			}
			if (decoder == null || decoder.MustFlush)
			{
				if (c > '\0')
				{
					num2--;
					byte[] bytes5;
					if (this.bigEndian)
					{
						bytes5 = new byte[]
						{
							(byte)(c >> 8),
							(byte)c
						};
					}
					else
					{
						bytes5 = new byte[]
						{
							(byte)c,
							(byte)(c >> 8)
						};
					}
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(byteStart, null);
					}
					num2 += decoderFallbackBuffer.InternalFallback(bytes5, bytes);
					c = '\0';
				}
				if (num >= 0)
				{
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(byteStart, null);
					}
					num2 += decoderFallbackBuffer.InternalFallback(new byte[]
					{
						(byte)num
					}, bytes);
				}
			}
			if (c > '\0')
			{
				num2--;
			}
			return num2;
		}

		// Token: 0x06002FA0 RID: 12192 RVA: 0x00162290 File Offset: 0x00161490
		internal unsafe sealed override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			UnicodeEncoding.Decoder decoder = (UnicodeEncoding.Decoder)baseDecoder;
			int num = -1;
			char c = '\0';
			if (decoder != null)
			{
				num = decoder.lastByte;
				c = decoder.lastChar;
			}
			DecoderFallbackBuffer decoderFallbackBuffer = null;
			byte* ptr = bytes + byteCount;
			char* ptr2 = chars + charCount;
			byte* ptr3 = bytes;
			char* ptr4 = chars;
			while (bytes < ptr)
			{
				if ((this.bigEndian ^ BitConverter.IsLittleEndian) && (chars & 7L) == null && num == -1 && c == '\0')
				{
					ulong* ptr5 = (ulong*)(bytes - 7 + (IntPtr)(((long)(ptr - bytes) >> 1 < (long)(ptr2 - chars)) ? ((long)(ptr - bytes)) : ((long)(ptr2 - chars) << 1)) / 8);
					ulong* ptr6 = (ulong*)bytes;
					ulong* ptr7 = (ulong*)chars;
					while (ptr6 < ptr5)
					{
						if ((9223512776490647552UL & *ptr6) != 0UL)
						{
							ulong num2 = (17870556004450629632UL & *ptr6) ^ 15564677810327967744UL;
							if (((num2 & 18446462598732840960UL) == 0UL || (num2 & 281470681743360UL) == 0UL || (num2 & (ulong)-65536) == 0UL || (num2 & 65535UL) == 0UL) && (18158790778715962368UL & *ptr6) != (BitConverter.IsLittleEndian ? 15852908186546788352UL : 15564682208374479872UL))
							{
								break;
							}
						}
						Unsafe.WriteUnaligned<ulong>((void*)ptr7, *ptr6);
						ptr6++;
						ptr7++;
					}
					chars = (char*)ptr7;
					bytes = (byte*)ptr6;
					if (bytes >= ptr)
					{
						break;
					}
				}
				if (num < 0)
				{
					num = (int)(*(bytes++));
				}
				else
				{
					char c2;
					if (this.bigEndian)
					{
						c2 = (char)(num << 8 | (int)(*(bytes++)));
					}
					else
					{
						c2 = (char)((int)(*(bytes++)) << 8 | num);
					}
					num = -1;
					if (c2 >= '\ud800' && c2 <= '\udfff')
					{
						if (c2 <= '\udbff')
						{
							if (c > '\0')
							{
								byte[] bytes2;
								if (this.bigEndian)
								{
									bytes2 = new byte[]
									{
										(byte)(c >> 8),
										(byte)c
									};
								}
								else
								{
									bytes2 = new byte[]
									{
										(byte)c,
										(byte)(c >> 8)
									};
								}
								if (decoderFallbackBuffer == null)
								{
									if (decoder == null)
									{
										decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
									}
									else
									{
										decoderFallbackBuffer = decoder.FallbackBuffer;
									}
									decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
								}
								char* ptr8 = chars;
								bool flag = decoderFallbackBuffer.InternalFallback(bytes2, bytes, ref ptr8);
								chars = ptr8;
								if (!flag)
								{
									bytes -= 2;
									decoderFallbackBuffer.InternalReset();
									base.ThrowCharsOverflow(decoder, chars == ptr4);
									break;
								}
							}
							c = c2;
							continue;
						}
						if (c == '\0')
						{
							byte[] bytes3;
							if (this.bigEndian)
							{
								bytes3 = new byte[]
								{
									(byte)(c2 >> 8),
									(byte)c2
								};
							}
							else
							{
								bytes3 = new byte[]
								{
									(byte)c2,
									(byte)(c2 >> 8)
								};
							}
							if (decoderFallbackBuffer == null)
							{
								if (decoder == null)
								{
									decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
								}
								else
								{
									decoderFallbackBuffer = decoder.FallbackBuffer;
								}
								decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
							}
							char* ptr8 = chars;
							bool flag2 = decoderFallbackBuffer.InternalFallback(bytes3, bytes, ref ptr8);
							chars = ptr8;
							if (!flag2)
							{
								bytes -= 2;
								decoderFallbackBuffer.InternalReset();
								base.ThrowCharsOverflow(decoder, chars == ptr4);
								break;
							}
							continue;
						}
						else
						{
							if (chars >= ptr2 - 1)
							{
								bytes -= 2;
								base.ThrowCharsOverflow(decoder, chars == ptr4);
								break;
							}
							*(chars++) = c;
							c = '\0';
						}
					}
					else if (c > '\0')
					{
						byte[] bytes4;
						if (this.bigEndian)
						{
							bytes4 = new byte[]
							{
								(byte)(c >> 8),
								(byte)c
							};
						}
						else
						{
							bytes4 = new byte[]
							{
								(byte)c,
								(byte)(c >> 8)
							};
						}
						if (decoderFallbackBuffer == null)
						{
							if (decoder == null)
							{
								decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
							}
							else
							{
								decoderFallbackBuffer = decoder.FallbackBuffer;
							}
							decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
						}
						char* ptr8 = chars;
						bool flag3 = decoderFallbackBuffer.InternalFallback(bytes4, bytes, ref ptr8);
						chars = ptr8;
						if (!flag3)
						{
							bytes -= 2;
							decoderFallbackBuffer.InternalReset();
							base.ThrowCharsOverflow(decoder, chars == ptr4);
							break;
						}
						c = '\0';
					}
					if (chars >= ptr2)
					{
						bytes -= 2;
						base.ThrowCharsOverflow(decoder, chars == ptr4);
						break;
					}
					*(chars++) = c2;
				}
			}
			if (decoder == null || decoder.MustFlush)
			{
				if (c > '\0')
				{
					byte[] bytes5;
					if (this.bigEndian)
					{
						bytes5 = new byte[]
						{
							(byte)(c >> 8),
							(byte)c
						};
					}
					else
					{
						bytes5 = new byte[]
						{
							(byte)c,
							(byte)(c >> 8)
						};
					}
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
					}
					char* ptr8 = chars;
					bool flag4 = decoderFallbackBuffer.InternalFallback(bytes5, bytes, ref ptr8);
					chars = ptr8;
					if (!flag4)
					{
						bytes -= 2;
						if (num >= 0)
						{
							bytes--;
						}
						decoderFallbackBuffer.InternalReset();
						base.ThrowCharsOverflow(decoder, chars == ptr4);
						bytes += 2;
						if (num >= 0)
						{
							bytes++;
							goto IL_4EA;
						}
						goto IL_4EA;
					}
					else
					{
						c = '\0';
					}
				}
				if (num >= 0)
				{
					if (decoderFallbackBuffer == null)
					{
						if (decoder == null)
						{
							decoderFallbackBuffer = this.decoderFallback.CreateFallbackBuffer();
						}
						else
						{
							decoderFallbackBuffer = decoder.FallbackBuffer;
						}
						decoderFallbackBuffer.InternalInitialize(ptr3, ptr2);
					}
					char* ptr8 = chars;
					bool flag5 = decoderFallbackBuffer.InternalFallback(new byte[]
					{
						(byte)num
					}, bytes, ref ptr8);
					chars = ptr8;
					if (!flag5)
					{
						bytes--;
						decoderFallbackBuffer.InternalReset();
						base.ThrowCharsOverflow(decoder, chars == ptr4);
						bytes++;
					}
					else
					{
						num = -1;
					}
				}
			}
			IL_4EA:
			if (decoder != null)
			{
				decoder._bytesUsed = (int)((long)(bytes - ptr3));
				decoder.lastChar = c;
				decoder.lastByte = num;
			}
			return (int)((long)(chars - ptr4));
		}

		// Token: 0x06002FA1 RID: 12193 RVA: 0x00158330 File Offset: 0x00157530
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}

		// Token: 0x06002FA2 RID: 12194 RVA: 0x001627AE File Offset: 0x001619AE
		public override System.Text.Decoder GetDecoder()
		{
			return new UnicodeEncoding.Decoder(this);
		}

		// Token: 0x06002FA3 RID: 12195 RVA: 0x001627B8 File Offset: 0x001619B8
		public override byte[] GetPreamble()
		{
			if (!this.byteOrderMark)
			{
				return Array.Empty<byte>();
			}
			if (this.bigEndian)
			{
				return new byte[]
				{
					254,
					byte.MaxValue
				};
			}
			return new byte[]
			{
				byte.MaxValue,
				254
			};
		}

		// Token: 0x1700096D RID: 2413
		// (get) Token: 0x06002FA4 RID: 12196 RVA: 0x00162808 File Offset: 0x00161A08
		[Nullable(0)]
		public unsafe override ReadOnlySpan<byte> Preamble
		{
			[NullableContext(0)]
			get
			{
				if (base.GetType() != typeof(UnicodeEncoding))
				{
					return new ReadOnlySpan<byte>(this.GetPreamble());
				}
				if (!this.byteOrderMark)
				{
					return default(ReadOnlySpan<byte>);
				}
				if (!this.bigEndian)
				{
					return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.B3D510EF04275CA8E698E5B3CBB0ECE3949EF9252F0CDC839E9EE347409A2209), 2);
				}
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.F197692810D457E297FCE9C5653B02581FF99A50852370F29D7E5FE47D9D37E6), 2);
			}
		}

		// Token: 0x06002FA5 RID: 12197 RVA: 0x0016286C File Offset: 0x00161A6C
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
			num <<= 1;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_GetByteCountOverflow);
			}
			return (int)num;
		}

		// Token: 0x06002FA6 RID: 12198 RVA: 0x001628D0 File Offset: 0x00161AD0
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			long num = (long)(byteCount >> 1) + (long)(byteCount & 1) + 1L;
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

		// Token: 0x06002FA7 RID: 12199 RVA: 0x00162938 File Offset: 0x00161B38
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			UnicodeEncoding unicodeEncoding = value as UnicodeEncoding;
			return unicodeEncoding != null && (this.CodePage == unicodeEncoding.CodePage && this.byteOrderMark == unicodeEncoding.byteOrderMark && this.bigEndian == unicodeEncoding.bigEndian && base.EncoderFallback.Equals(unicodeEncoding.EncoderFallback)) && base.DecoderFallback.Equals(unicodeEncoding.DecoderFallback);
		}

		// Token: 0x06002FA8 RID: 12200 RVA: 0x001629A1 File Offset: 0x00161BA1
		public override int GetHashCode()
		{
			return this.CodePage + base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode() + (this.byteOrderMark ? 4 : 0) + (this.bigEndian ? 8 : 0);
		}

		// Token: 0x04000D22 RID: 3362
		internal static readonly UnicodeEncoding s_bigEndianDefault = new UnicodeEncoding(true, true);

		// Token: 0x04000D23 RID: 3363
		internal static readonly UnicodeEncoding s_littleEndianDefault = new UnicodeEncoding(false, true);

		// Token: 0x04000D24 RID: 3364
		private readonly bool isThrowException;

		// Token: 0x04000D25 RID: 3365
		private readonly bool bigEndian;

		// Token: 0x04000D26 RID: 3366
		private readonly bool byteOrderMark;

		// Token: 0x04000D27 RID: 3367
		public const int CharSize = 2;

		// Token: 0x02000386 RID: 902
		private sealed class Decoder : DecoderNLS
		{
			// Token: 0x06002FAA RID: 12202 RVA: 0x001629F5 File Offset: 0x00161BF5
			public Decoder(UnicodeEncoding encoding) : base(encoding)
			{
			}

			// Token: 0x06002FAB RID: 12203 RVA: 0x00162A05 File Offset: 0x00161C05
			public override void Reset()
			{
				this.lastByte = -1;
				this.lastChar = '\0';
				if (this._fallbackBuffer != null)
				{
					this._fallbackBuffer.Reset();
				}
			}

			// Token: 0x1700096E RID: 2414
			// (get) Token: 0x06002FAC RID: 12204 RVA: 0x00162A28 File Offset: 0x00161C28
			internal override bool HasState
			{
				get
				{
					return this.lastByte != -1 || this.lastChar > '\0';
				}
			}

			// Token: 0x04000D28 RID: 3368
			internal int lastByte = -1;

			// Token: 0x04000D29 RID: 3369
			internal char lastChar;
		}
	}
}
