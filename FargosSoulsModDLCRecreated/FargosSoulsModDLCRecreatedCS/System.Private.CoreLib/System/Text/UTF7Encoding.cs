using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x0200038A RID: 906
	[Nullable(0)]
	[NullableContext(1)]
	public class UTF7Encoding : Encoding
	{
		// Token: 0x06002FDC RID: 12252 RVA: 0x001636AA File Offset: 0x001628AA
		[Obsolete("The UTF-7 encoding is insecure and should not be used. Consider using UTF-8 instead.", DiagnosticId = "SYSLIB0001", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public UTF7Encoding() : this(false)
		{
		}

		// Token: 0x06002FDD RID: 12253 RVA: 0x001636B3 File Offset: 0x001628B3
		[Obsolete("The UTF-7 encoding is insecure and should not be used. Consider using UTF-8 instead.", DiagnosticId = "SYSLIB0001", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public UTF7Encoding(bool allowOptionals) : base(65000)
		{
			this._allowOptionals = allowOptionals;
			this.MakeTables();
		}

		// Token: 0x06002FDE RID: 12254 RVA: 0x001636D0 File Offset: 0x001628D0
		[MemberNotNull("_base64Values")]
		[MemberNotNull("_base64Bytes")]
		[MemberNotNull("_directEncode")]
		private void MakeTables()
		{
			this._base64Bytes = new byte[64];
			for (int i = 0; i < 64; i++)
			{
				this._base64Bytes[i] = (byte)"ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789+/"[i];
			}
			this._base64Values = new sbyte[128];
			for (int j = 0; j < 128; j++)
			{
				this._base64Values[j] = -1;
			}
			for (int k = 0; k < 64; k++)
			{
				this._base64Values[(int)this._base64Bytes[k]] = (sbyte)k;
			}
			this._directEncode = new bool[128];
			int length = "\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz".Length;
			for (int l = 0; l < length; l++)
			{
				this._directEncode[(int)"\t\n\r '(),-./0123456789:?ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz"[l]] = true;
			}
			if (this._allowOptionals)
			{
				length = "!\"#$%&*;<=>@[]^_`{|}".Length;
				for (int m = 0; m < length; m++)
				{
					this._directEncode[(int)"!\"#$%&*;<=>@[]^_`{|}"[m]] = true;
				}
			}
		}

		// Token: 0x06002FDF RID: 12255 RVA: 0x001637C8 File Offset: 0x001629C8
		internal sealed override void SetDefaultFallbacks()
		{
			this.encoderFallback = new EncoderReplacementFallback(string.Empty);
			this.decoderFallback = new UTF7Encoding.DecoderUTF7Fallback();
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x001637E8 File Offset: 0x001629E8
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			UTF7Encoding utf7Encoding = value as UTF7Encoding;
			return utf7Encoding != null && (this._allowOptionals == utf7Encoding._allowOptionals && base.EncoderFallback.Equals(utf7Encoding.EncoderFallback)) && base.DecoderFallback.Equals(utf7Encoding.DecoderFallback);
		}

		// Token: 0x06002FE1 RID: 12257 RVA: 0x00163835 File Offset: 0x00162A35
		public override int GetHashCode()
		{
			return this.CodePage + base.EncoderFallback.GetHashCode() + base.DecoderFallback.GetHashCode();
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x001611A8 File Offset: 0x001603A8
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

		// Token: 0x06002FE3 RID: 12259 RVA: 0x00161230 File Offset: 0x00160430
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

		// Token: 0x06002FE4 RID: 12260 RVA: 0x0016126A File Offset: 0x0016046A
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

		// Token: 0x06002FE5 RID: 12261 RVA: 0x001612A0 File Offset: 0x001604A0
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

		// Token: 0x06002FE6 RID: 12262 RVA: 0x00161368 File Offset: 0x00160568
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

		// Token: 0x06002FE7 RID: 12263 RVA: 0x00161438 File Offset: 0x00160638
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

		// Token: 0x06002FE8 RID: 12264 RVA: 0x0016149C File Offset: 0x0016069C
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

		// Token: 0x06002FE9 RID: 12265 RVA: 0x0016151F File Offset: 0x0016071F
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

		// Token: 0x06002FEA RID: 12266 RVA: 0x00161554 File Offset: 0x00160754
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

		// Token: 0x06002FEB RID: 12267 RVA: 0x00161624 File Offset: 0x00160824
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

		// Token: 0x06002FEC RID: 12268 RVA: 0x00161688 File Offset: 0x00160888
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

		// Token: 0x06002FED RID: 12269 RVA: 0x00163855 File Offset: 0x00162A55
		internal unsafe sealed override int GetByteCount(char* chars, int count, EncoderNLS baseEncoder)
		{
			return this.GetBytes(chars, count, null, 0, baseEncoder);
		}

		// Token: 0x06002FEE RID: 12270 RVA: 0x00163864 File Offset: 0x00162A64
		internal unsafe sealed override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, EncoderNLS baseEncoder)
		{
			UTF7Encoding.Encoder encoder = (UTF7Encoding.Encoder)baseEncoder;
			int num = 0;
			int i = -1;
			Encoding.EncodingByteBuffer encodingByteBuffer = new Encoding.EncodingByteBuffer(this, encoder, bytes, byteCount, chars, charCount);
			if (encoder != null)
			{
				num = encoder.bits;
				i = encoder.bitCount;
				while (i >= 6)
				{
					i -= 6;
					if (!encodingByteBuffer.AddByte(this._base64Bytes[num >> i & 63]))
					{
						base.ThrowBytesOverflow(encoder, encodingByteBuffer.Count == 0);
					}
				}
			}
			while (encodingByteBuffer.MoreData)
			{
				char nextChar = encodingByteBuffer.GetNextChar();
				if (nextChar < '\u0080' && this._directEncode[(int)nextChar])
				{
					if (i >= 0)
					{
						if (i > 0)
						{
							if (!encodingByteBuffer.AddByte(this._base64Bytes[num << 6 - i & 63]))
							{
								break;
							}
							i = 0;
						}
						if (!encodingByteBuffer.AddByte(45))
						{
							break;
						}
						i = -1;
					}
					if (!encodingByteBuffer.AddByte((byte)nextChar))
					{
						break;
					}
				}
				else if (i < 0 && nextChar == '+')
				{
					if (!encodingByteBuffer.AddByte(43, 45))
					{
						break;
					}
				}
				else
				{
					if (i < 0)
					{
						if (!encodingByteBuffer.AddByte(43))
						{
							break;
						}
						i = 0;
					}
					num = (num << 16 | (int)nextChar);
					i += 16;
					while (i >= 6)
					{
						i -= 6;
						if (!encodingByteBuffer.AddByte(this._base64Bytes[num >> i & 63]))
						{
							i += 6;
							encodingByteBuffer.GetNextChar();
							break;
						}
					}
					if (i >= 6)
					{
						break;
					}
				}
			}
			if (i >= 0 && (encoder == null || encoder.MustFlush))
			{
				if (i > 0 && encodingByteBuffer.AddByte(this._base64Bytes[num << 6 - i & 63]))
				{
					i = 0;
				}
				if (encodingByteBuffer.AddByte(45))
				{
					num = 0;
					i = -1;
				}
				else
				{
					encodingByteBuffer.GetNextChar();
				}
			}
			if (bytes != null && encoder != null)
			{
				encoder.bits = num;
				encoder.bitCount = i;
				encoder._charsUsed = encodingByteBuffer.CharsUsed;
			}
			return encodingByteBuffer.Count;
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x00163A15 File Offset: 0x00162C15
		internal unsafe sealed override int GetCharCount(byte* bytes, int count, DecoderNLS baseDecoder)
		{
			return this.GetChars(bytes, count, null, 0, baseDecoder);
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x00163A24 File Offset: 0x00162C24
		internal unsafe sealed override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, DecoderNLS baseDecoder)
		{
			UTF7Encoding.Decoder decoder = (UTF7Encoding.Decoder)baseDecoder;
			Encoding.EncodingCharBuffer encodingCharBuffer = new Encoding.EncodingCharBuffer(this, decoder, chars, charCount, bytes, byteCount);
			int num = 0;
			int num2 = -1;
			bool flag = false;
			if (decoder != null)
			{
				num = decoder.bits;
				num2 = decoder.bitCount;
				flag = decoder.firstByte;
			}
			if (num2 >= 16)
			{
				if (!encodingCharBuffer.AddChar((char)(num >> num2 - 16 & 65535)))
				{
					base.ThrowCharsOverflow(decoder, true);
				}
				num2 -= 16;
			}
			while (encodingCharBuffer.MoreData)
			{
				byte nextByte = encodingCharBuffer.GetNextByte();
				int num3;
				if (num2 >= 0)
				{
					sbyte b;
					if (nextByte < 128 && (b = this._base64Values[(int)nextByte]) >= 0)
					{
						flag = false;
						num = (num << 6 | (int)((byte)b));
						num2 += 6;
						if (num2 < 16)
						{
							continue;
						}
						num3 = (num >> num2 - 16 & 65535);
						num2 -= 16;
					}
					else
					{
						num2 = -1;
						if (nextByte != 45)
						{
							if (!encodingCharBuffer.Fallback(nextByte))
							{
								break;
							}
							continue;
						}
						else
						{
							if (!flag)
							{
								continue;
							}
							num3 = 43;
						}
					}
				}
				else
				{
					if (nextByte == 43)
					{
						num2 = 0;
						flag = true;
						continue;
					}
					if (nextByte >= 128)
					{
						if (!encodingCharBuffer.Fallback(nextByte))
						{
							break;
						}
						continue;
					}
					else
					{
						num3 = (int)nextByte;
					}
				}
				if (num3 >= 0 && !encodingCharBuffer.AddChar((char)num3))
				{
					if (num2 >= 0)
					{
						encodingCharBuffer.AdjustBytes(1);
						num2 += 16;
						break;
					}
					break;
				}
			}
			if (chars != null && decoder != null)
			{
				if (decoder.MustFlush)
				{
					decoder.bits = 0;
					decoder.bitCount = -1;
					decoder.firstByte = false;
				}
				else
				{
					decoder.bits = num;
					decoder.bitCount = num2;
					decoder.firstByte = flag;
				}
				decoder._bytesUsed = encodingCharBuffer.BytesUsed;
			}
			return encodingCharBuffer.Count;
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x00163BA8 File Offset: 0x00162DA8
		public override System.Text.Decoder GetDecoder()
		{
			return new UTF7Encoding.Decoder(this);
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x00163BB0 File Offset: 0x00162DB0
		public override System.Text.Encoder GetEncoder()
		{
			return new UTF7Encoding.Encoder(this);
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x00163BB8 File Offset: 0x00162DB8
		public override int GetMaxByteCount(int charCount)
		{
			if (charCount < 0)
			{
				throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			long num = (long)charCount * 3L + 2L;
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("charCount", SR.ArgumentOutOfRange_GetByteCountOverflow);
			}
			return (int)num;
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x00163C00 File Offset: 0x00162E00
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			int num = byteCount;
			if (num == 0)
			{
				num = 1;
			}
			return num;
		}

		// Token: 0x04000D31 RID: 3377
		internal static readonly UTF7Encoding s_default = new UTF7Encoding();

		// Token: 0x04000D32 RID: 3378
		private byte[] _base64Bytes;

		// Token: 0x04000D33 RID: 3379
		private sbyte[] _base64Values;

		// Token: 0x04000D34 RID: 3380
		private bool[] _directEncode;

		// Token: 0x04000D35 RID: 3381
		private readonly bool _allowOptionals;

		// Token: 0x0200038B RID: 907
		private sealed class Decoder : DecoderNLS
		{
			// Token: 0x06002FF6 RID: 12278 RVA: 0x00163673 File Offset: 0x00162873
			public Decoder(UTF7Encoding encoding) : base(encoding)
			{
			}

			// Token: 0x06002FF7 RID: 12279 RVA: 0x00163C35 File Offset: 0x00162E35
			public override void Reset()
			{
				this.bits = 0;
				this.bitCount = -1;
				this.firstByte = false;
				if (this._fallbackBuffer != null)
				{
					this._fallbackBuffer.Reset();
				}
			}

			// Token: 0x17000971 RID: 2417
			// (get) Token: 0x06002FF8 RID: 12280 RVA: 0x00163C5F File Offset: 0x00162E5F
			internal override bool HasState
			{
				get
				{
					return this.bitCount != -1;
				}
			}

			// Token: 0x04000D36 RID: 3382
			internal int bits;

			// Token: 0x04000D37 RID: 3383
			internal int bitCount;

			// Token: 0x04000D38 RID: 3384
			internal bool firstByte;
		}

		// Token: 0x0200038C RID: 908
		private sealed class Encoder : EncoderNLS
		{
			// Token: 0x06002FF9 RID: 12281 RVA: 0x00163C6D File Offset: 0x00162E6D
			public Encoder(UTF7Encoding encoding) : base(encoding)
			{
			}

			// Token: 0x06002FFA RID: 12282 RVA: 0x00163C76 File Offset: 0x00162E76
			public override void Reset()
			{
				this.bitCount = -1;
				this.bits = 0;
				if (this._fallbackBuffer != null)
				{
					this._fallbackBuffer.Reset();
				}
			}

			// Token: 0x17000972 RID: 2418
			// (get) Token: 0x06002FFB RID: 12283 RVA: 0x00163C99 File Offset: 0x00162E99
			internal override bool HasState
			{
				get
				{
					return this.bits != 0 || this.bitCount != -1;
				}
			}

			// Token: 0x04000D39 RID: 3385
			internal int bits;

			// Token: 0x04000D3A RID: 3386
			internal int bitCount;
		}

		// Token: 0x0200038D RID: 909
		private sealed class DecoderUTF7Fallback : DecoderFallback
		{
			// Token: 0x06002FFC RID: 12284 RVA: 0x00163CB1 File Offset: 0x00162EB1
			public override DecoderFallbackBuffer CreateFallbackBuffer()
			{
				return new UTF7Encoding.DecoderUTF7FallbackBuffer();
			}

			// Token: 0x17000973 RID: 2419
			// (get) Token: 0x06002FFD RID: 12285 RVA: 0x000AC09E File Offset: 0x000AB29E
			public override int MaxCharCount
			{
				get
				{
					return 1;
				}
			}

			// Token: 0x06002FFE RID: 12286 RVA: 0x00163CB8 File Offset: 0x00162EB8
			public override bool Equals(object value)
			{
				return value is UTF7Encoding.DecoderUTF7Fallback;
			}

			// Token: 0x06002FFF RID: 12287 RVA: 0x00163CC3 File Offset: 0x00162EC3
			public override int GetHashCode()
			{
				return 984;
			}
		}

		// Token: 0x0200038E RID: 910
		private sealed class DecoderUTF7FallbackBuffer : DecoderFallbackBuffer
		{
			// Token: 0x06003001 RID: 12289 RVA: 0x00163CCC File Offset: 0x00162ECC
			public override bool Fallback(byte[] bytesUnknown, int index)
			{
				this.cFallback = (char)bytesUnknown[0];
				if (this.cFallback == '\0')
				{
					return false;
				}
				this.iCount = (this.iSize = 1);
				return true;
			}

			// Token: 0x06003002 RID: 12290 RVA: 0x00163D00 File Offset: 0x00162F00
			public override char GetNextChar()
			{
				int num = this.iCount;
				this.iCount = num - 1;
				if (num > 0)
				{
					return this.cFallback;
				}
				return '\0';
			}

			// Token: 0x06003003 RID: 12291 RVA: 0x00163D29 File Offset: 0x00162F29
			public override bool MovePrevious()
			{
				if (this.iCount >= 0)
				{
					this.iCount++;
				}
				return this.iCount >= 0 && this.iCount <= this.iSize;
			}

			// Token: 0x17000974 RID: 2420
			// (get) Token: 0x06003004 RID: 12292 RVA: 0x00163D5E File Offset: 0x00162F5E
			public override int Remaining
			{
				get
				{
					if (this.iCount <= 0)
					{
						return 0;
					}
					return this.iCount;
				}
			}

			// Token: 0x06003005 RID: 12293 RVA: 0x00163D71 File Offset: 0x00162F71
			public override void Reset()
			{
				this.iCount = -1;
				this.byteStart = null;
			}

			// Token: 0x06003006 RID: 12294 RVA: 0x00163D82 File Offset: 0x00162F82
			internal unsafe override int InternalFallback(byte[] bytes, byte* pBytes)
			{
				if (bytes.Length != 1)
				{
					throw new ArgumentException(SR.Argument_InvalidCharSequenceNoIndex);
				}
				if (bytes[0] != 0)
				{
					return 1;
				}
				return 0;
			}

			// Token: 0x04000D3B RID: 3387
			private char cFallback;

			// Token: 0x04000D3C RID: 3388
			private int iCount = -1;

			// Token: 0x04000D3D RID: 3389
			private int iSize;
		}
	}
}
