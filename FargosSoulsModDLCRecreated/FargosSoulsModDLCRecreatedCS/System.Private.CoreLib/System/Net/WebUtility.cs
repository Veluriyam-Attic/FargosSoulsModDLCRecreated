using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.Net
{
	// Token: 0x020001D7 RID: 471
	[NullableContext(2)]
	[Nullable(0)]
	public static class WebUtility
	{
		// Token: 0x06001D93 RID: 7571 RVA: 0x0011A304 File Offset: 0x00119504
		[return: NotNullIfNotNull("value")]
		public unsafe static string HtmlEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			ReadOnlySpan<char> input = value.AsSpan();
			int num = WebUtility.IndexOfHtmlEncodingChars(input);
			if (num == -1)
			{
				return value;
			}
			ValueStringBuilder valueStringBuilder;
			if (value.Length < 80)
			{
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
				valueStringBuilder = new ValueStringBuilder(initialBuffer);
			}
			else
			{
				valueStringBuilder = new ValueStringBuilder(value.Length + 200);
			}
			ValueStringBuilder valueStringBuilder2 = valueStringBuilder;
			valueStringBuilder2.Append(input.Slice(0, num));
			WebUtility.HtmlEncode(input.Slice(num), ref valueStringBuilder2);
			return valueStringBuilder2.ToString();
		}

		// Token: 0x06001D94 RID: 7572 RVA: 0x0011A39C File Offset: 0x0011959C
		[NullableContext(1)]
		public unsafe static void HtmlEncode([Nullable(2)] string value, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (string.IsNullOrEmpty(value))
			{
				output.Write(value);
				return;
			}
			ReadOnlySpan<char> input = value.AsSpan();
			int num = WebUtility.IndexOfHtmlEncodingChars(input);
			if (num == -1)
			{
				output.Write(value);
				return;
			}
			ValueStringBuilder valueStringBuilder;
			if (value.Length < 80)
			{
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
				valueStringBuilder = new ValueStringBuilder(initialBuffer);
			}
			else
			{
				valueStringBuilder = new ValueStringBuilder(value.Length + 200);
			}
			ValueStringBuilder valueStringBuilder2 = valueStringBuilder;
			valueStringBuilder2.Append(input.Slice(0, num));
			WebUtility.HtmlEncode(input.Slice(num), ref valueStringBuilder2);
			output.Write(valueStringBuilder2.AsSpan());
			valueStringBuilder2.Dispose();
		}

		// Token: 0x06001D95 RID: 7573 RVA: 0x0011A454 File Offset: 0x00119654
		private unsafe static void HtmlEncode(ReadOnlySpan<char> input, ref ValueStringBuilder output)
		{
			for (int i = 0; i < input.Length; i++)
			{
				char c = (char)(*input[i]);
				if (c <= '>')
				{
					if (c <= '&')
					{
						if (c == '"')
						{
							output.Append("&quot;");
							goto IL_11D;
						}
						if (c == '&')
						{
							output.Append("&amp;");
							goto IL_11D;
						}
					}
					else
					{
						if (c == '\'')
						{
							output.Append("&#39;");
							goto IL_11D;
						}
						if (c == '<')
						{
							output.Append("&lt;");
							goto IL_11D;
						}
						if (c == '>')
						{
							output.Append("&gt;");
							goto IL_11D;
						}
					}
					output.Append(c);
				}
				else
				{
					int num = -1;
					if (c >= '\u00a0' && c < 'Ā')
					{
						num = (int)c;
					}
					else if (char.IsSurrogate(c))
					{
						int nextUnicodeScalarValueFromUtf16Surrogate = WebUtility.GetNextUnicodeScalarValueFromUtf16Surrogate(input, ref i);
						if (nextUnicodeScalarValueFromUtf16Surrogate >= 65536)
						{
							num = nextUnicodeScalarValueFromUtf16Surrogate;
						}
						else
						{
							c = (char)nextUnicodeScalarValueFromUtf16Surrogate;
						}
					}
					if (num >= 0)
					{
						output.Append("&#");
						Span<char> destination = output.AppendSpan(10);
						int num2;
						num.TryFormat(destination, out num2, default(ReadOnlySpan<char>), null);
						output.Length -= 10 - num2;
						output.Append(';');
					}
					else
					{
						output.Append(c);
					}
				}
				IL_11D:;
			}
		}

		// Token: 0x06001D96 RID: 7574 RVA: 0x0011A590 File Offset: 0x00119790
		[return: NotNullIfNotNull("value")]
		public unsafe static string HtmlDecode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			ReadOnlySpan<char> input = value.AsSpan();
			int num = WebUtility.IndexOfHtmlDecodingChars(input);
			if (num == -1)
			{
				return value;
			}
			ValueStringBuilder valueStringBuilder;
			if (value.Length <= 256)
			{
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
				valueStringBuilder = new ValueStringBuilder(initialBuffer);
			}
			else
			{
				valueStringBuilder = new ValueStringBuilder(value.Length);
			}
			ValueStringBuilder valueStringBuilder2 = valueStringBuilder;
			valueStringBuilder2.Append(input.Slice(0, num));
			WebUtility.HtmlDecode(input.Slice(num), ref valueStringBuilder2);
			return valueStringBuilder2.ToString();
		}

		// Token: 0x06001D97 RID: 7575 RVA: 0x0011A624 File Offset: 0x00119824
		[NullableContext(1)]
		public unsafe static void HtmlDecode([Nullable(2)] string value, TextWriter output)
		{
			if (output == null)
			{
				throw new ArgumentNullException("output");
			}
			if (string.IsNullOrEmpty(value))
			{
				output.Write(value);
				return;
			}
			ReadOnlySpan<char> input = value.AsSpan();
			int num = WebUtility.IndexOfHtmlDecodingChars(input);
			if (num == -1)
			{
				output.Write(value);
				return;
			}
			ValueStringBuilder valueStringBuilder;
			if (value.Length <= 256)
			{
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
				valueStringBuilder = new ValueStringBuilder(initialBuffer);
			}
			else
			{
				valueStringBuilder = new ValueStringBuilder(value.Length);
			}
			ValueStringBuilder valueStringBuilder2 = valueStringBuilder;
			valueStringBuilder2.Append(input.Slice(0, num));
			WebUtility.HtmlDecode(input.Slice(num), ref valueStringBuilder2);
			output.Write(valueStringBuilder2.AsSpan());
			valueStringBuilder2.Dispose();
		}

		// Token: 0x06001D98 RID: 7576 RVA: 0x0011A6D8 File Offset: 0x001198D8
		private unsafe static void HtmlDecode(ReadOnlySpan<char> input, ref ValueStringBuilder output)
		{
			int i = 0;
			while (i < input.Length)
			{
				char c = (char)(*input[i]);
				if (c != '&')
				{
					goto IL_152;
				}
				ReadOnlySpan<char> span = input.Slice(i + 1);
				int num = span.IndexOfAny(';', '&');
				if (num < 0 || *span[num] != 59)
				{
					goto IL_152;
				}
				int num2 = i + 1 + num;
				if (num > 1 && *span[0] == 35)
				{
					uint num3;
					bool flag = (*span[1] == 120 || *span[1] == 88) ? uint.TryParse(span.Slice(2, num - 2), NumberStyles.AllowHexSpecifier, CultureInfo.InvariantCulture, out num3) : uint.TryParse(span.Slice(1, num - 1), NumberStyles.Integer, CultureInfo.InvariantCulture, out num3);
					if (flag)
					{
						flag = (num3 < 55296U || (57343U < num3 && num3 <= 1114111U));
					}
					if (!flag)
					{
						goto IL_152;
					}
					if (num3 <= 65535U)
					{
						output.Append((char)num3);
					}
					else
					{
						char c2;
						char c3;
						WebUtility.ConvertSmpToUtf16(num3, out c2, out c3);
						output.Append(c2);
						output.Append(c3);
					}
					i = num2;
				}
				else
				{
					ReadOnlySpan<char> readOnlySpan = span.Slice(0, num);
					i = num2;
					char c4 = WebUtility.HtmlEntities.Lookup(readOnlySpan);
					if (c4 != '\0')
					{
						c = c4;
						goto IL_152;
					}
					output.Append('&');
					output.Append(readOnlySpan);
					output.Append(';');
				}
				IL_159:
				i++;
				continue;
				IL_152:
				output.Append(c);
				goto IL_159;
			}
		}

		// Token: 0x06001D99 RID: 7577 RVA: 0x0011A850 File Offset: 0x00119A50
		private unsafe static int IndexOfHtmlEncodingChars(ReadOnlySpan<char> input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				char c = (char)(*input[i]);
				if (c <= '>')
				{
					if (c <= '&')
					{
						if (c != '"' && c != '&')
						{
							goto IL_51;
						}
					}
					else if (c != '\'' && c != '<' && c != '>')
					{
						goto IL_51;
					}
					return i;
				}
				if (c >= '\u00a0' && c < 'Ā')
				{
					return i;
				}
				if (char.IsSurrogate(c))
				{
					return i;
				}
				IL_51:;
			}
			return -1;
		}

		// Token: 0x06001D9A RID: 7578 RVA: 0x0011A8C0 File Offset: 0x00119AC0
		private static void GetEncodedBytes(byte[] originalBytes, int offset, int count, byte[] expandedBytes)
		{
			int num = 0;
			int num2 = offset + count;
			for (int i = offset; i < num2; i++)
			{
				byte b = originalBytes[i];
				char c = (char)b;
				if (WebUtility.IsUrlSafeChar(c))
				{
					expandedBytes[num++] = b;
				}
				else if (c == ' ')
				{
					expandedBytes[num++] = 43;
				}
				else
				{
					expandedBytes[num++] = 37;
					expandedBytes[num++] = (byte)HexConverter.ToCharUpper(b >> 4);
					expandedBytes[num++] = (byte)HexConverter.ToCharUpper((int)b);
				}
			}
		}

		// Token: 0x06001D9B RID: 7579 RVA: 0x0011A934 File Offset: 0x00119B34
		[return: NotNullIfNotNull("value")]
		public static string UrlEncode(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			int num = 0;
			int num2 = 0;
			foreach (char c in value)
			{
				if (WebUtility.IsUrlSafeChar(c))
				{
					num++;
				}
				else if (c == ' ')
				{
					num2++;
				}
			}
			int num3 = num + num2;
			if (num3 != value.Length)
			{
				int byteCount = Encoding.UTF8.GetByteCount(value);
				int num4 = byteCount - num3;
				int num5 = num4 * 2;
				byte[] array = new byte[byteCount + num5];
				Encoding.UTF8.GetBytes(value, 0, value.Length, array, num5);
				WebUtility.GetEncodedBytes(array, num5, byteCount, array);
				return Encoding.UTF8.GetString(array);
			}
			if (num2 != 0)
			{
				return value.Replace(' ', '+');
			}
			return value;
		}

		// Token: 0x06001D9C RID: 7580 RVA: 0x0011A9F8 File Offset: 0x00119BF8
		[return: NotNullIfNotNull("value")]
		public static byte[] UrlEncodeToBytes(byte[] value, int offset, int count)
		{
			if (!WebUtility.ValidateUrlEncodingParameters(value, offset, count))
			{
				return null;
			}
			bool flag = false;
			int num = 0;
			for (int i = 0; i < count; i++)
			{
				char c = (char)value[offset + i];
				if (c == ' ')
				{
					flag = true;
				}
				else if (!WebUtility.IsUrlSafeChar(c))
				{
					num++;
				}
			}
			if (!flag && num == 0)
			{
				byte[] array = new byte[count];
				Buffer.BlockCopy(value, offset, array, 0, count);
				return array;
			}
			byte[] array2 = new byte[count + num * 2];
			WebUtility.GetEncodedBytes(value, offset, count, array2);
			return array2;
		}

		// Token: 0x06001D9D RID: 7581 RVA: 0x0011AA70 File Offset: 0x00119C70
		[return: NotNullIfNotNull("value")]
		private static string UrlDecodeInternal(string value, Encoding encoding)
		{
			if (string.IsNullOrEmpty(value))
			{
				return value;
			}
			int length = value.Length;
			WebUtility.UrlDecoder urlDecoder = new WebUtility.UrlDecoder(length, encoding);
			bool flag = false;
			bool flag2 = false;
			int i = 0;
			while (i < length)
			{
				char c = value[i];
				if (c == '+')
				{
					flag2 = true;
					c = ' ';
					goto IL_96;
				}
				if (c != '%' || i >= length - 2)
				{
					goto IL_96;
				}
				int num = HexConverter.FromChar((int)value[i + 1]);
				int num2 = HexConverter.FromChar((int)value[i + 2]);
				if ((num | num2) == 255)
				{
					goto IL_96;
				}
				byte b = (byte)(num << 4 | num2);
				i += 2;
				urlDecoder.AddByte(b);
				flag = true;
				IL_B5:
				i++;
				continue;
				IL_96:
				if ((c & 'ﾀ') == '\0')
				{
					urlDecoder.AddByte((byte)c);
					goto IL_B5;
				}
				urlDecoder.AddChar(c);
				goto IL_B5;
			}
			if (flag)
			{
				return urlDecoder.GetString();
			}
			if (flag2)
			{
				return value.Replace('+', ' ');
			}
			return value;
		}

		// Token: 0x06001D9E RID: 7582 RVA: 0x0011AB5C File Offset: 0x00119D5C
		[return: NotNullIfNotNull("bytes")]
		private static byte[] UrlDecodeInternal(byte[] bytes, int offset, int count)
		{
			if (!WebUtility.ValidateUrlEncodingParameters(bytes, offset, count))
			{
				return null;
			}
			int num = 0;
			byte[] array = new byte[count];
			for (int i = 0; i < count; i++)
			{
				int num2 = offset + i;
				byte b = bytes[num2];
				if (b == 43)
				{
					b = 32;
				}
				else if (b == 37 && i < count - 2)
				{
					int num3 = HexConverter.FromChar((int)bytes[num2 + 1]);
					int num4 = HexConverter.FromChar((int)bytes[num2 + 2]);
					if ((num3 | num4) != 255)
					{
						b = (byte)(num3 << 4 | num4);
						i += 2;
					}
				}
				array[num++] = b;
			}
			if (num < array.Length)
			{
				Array.Resize<byte>(ref array, num);
			}
			return array;
		}

		// Token: 0x06001D9F RID: 7583 RVA: 0x0011ABF5 File Offset: 0x00119DF5
		[return: NotNullIfNotNull("encodedValue")]
		public static string UrlDecode(string encodedValue)
		{
			return WebUtility.UrlDecodeInternal(encodedValue, Encoding.UTF8);
		}

		// Token: 0x06001DA0 RID: 7584 RVA: 0x0011AC02 File Offset: 0x00119E02
		[return: NotNullIfNotNull("encodedValue")]
		public static byte[] UrlDecodeToBytes(byte[] encodedValue, int offset, int count)
		{
			return WebUtility.UrlDecodeInternal(encodedValue, offset, count);
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x0011AC0C File Offset: 0x00119E0C
		private static void ConvertSmpToUtf16(uint smpChar, out char leadingSurrogate, out char trailingSurrogate)
		{
			int num = (int)(smpChar - 65536U);
			leadingSurrogate = (char)(num / 1024 + 55296);
			trailingSurrogate = (char)(num % 1024 + 56320);
		}

		// Token: 0x06001DA2 RID: 7586 RVA: 0x0011AC44 File Offset: 0x00119E44
		private unsafe static int GetNextUnicodeScalarValueFromUtf16Surrogate(ReadOnlySpan<char> input, ref int index)
		{
			if (input.Length - index <= 1)
			{
				return 65533;
			}
			char c = (char)(*input[index]);
			char c2 = (char)(*input[index + 1]);
			if (!char.IsSurrogatePair(c, c2))
			{
				return 65533;
			}
			index++;
			return (int)((c - '\ud800') * 'Ѐ' + (c2 - '\udc00')) + 65536;
		}

		// Token: 0x06001DA3 RID: 7587 RVA: 0x0011ACAC File Offset: 0x00119EAC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool IsUrlSafeChar(char ch)
		{
			return ch - 'a' <= '\u0019' || ch - 'A' <= '\u0019' || (ch - ' ' <= '\u0019' && (1 << (int)(ch - ' ') & 67069698) != 0) || ch == '_';
		}

		// Token: 0x06001DA4 RID: 7588 RVA: 0x0011ACEC File Offset: 0x00119EEC
		private static bool ValidateUrlEncodingParameters(byte[] bytes, int offset, int count)
		{
			if (bytes == null && count == 0)
			{
				return false;
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			if (offset < 0 || offset > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("offset");
			}
			if (count < 0 || offset + count > bytes.Length)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			return true;
		}

		// Token: 0x06001DA5 RID: 7589 RVA: 0x0011AD3C File Offset: 0x00119F3C
		private unsafe static int IndexOfHtmlDecodingChars(ReadOnlySpan<char> input)
		{
			for (int i = 0; i < input.Length; i++)
			{
				char c = (char)(*input[i]);
				if (c == '&' || char.IsSurrogate(c))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x020001D8 RID: 472
		private struct UrlDecoder
		{
			// Token: 0x06001DA6 RID: 7590 RVA: 0x0011AD78 File Offset: 0x00119F78
			private void FlushBytes()
			{
				if (this._charBuffer == null)
				{
					this._charBuffer = new char[this._bufferSize];
				}
				this._numChars += this._encoding.GetChars(this._byteBuffer, 0, this._numBytes, this._charBuffer, this._numChars);
				this._numBytes = 0;
			}

			// Token: 0x06001DA7 RID: 7591 RVA: 0x0011ADD6 File Offset: 0x00119FD6
			internal UrlDecoder(int bufferSize, Encoding encoding)
			{
				this._bufferSize = bufferSize;
				this._encoding = encoding;
				this._charBuffer = null;
				this._numChars = 0;
				this._numBytes = 0;
				this._byteBuffer = null;
			}

			// Token: 0x06001DA8 RID: 7592 RVA: 0x0011AE04 File Offset: 0x0011A004
			internal void AddChar(char ch)
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				if (this._charBuffer == null)
				{
					this._charBuffer = new char[this._bufferSize];
				}
				char[] charBuffer = this._charBuffer;
				int numChars = this._numChars;
				this._numChars = numChars + 1;
				charBuffer[numChars] = ch;
			}

			// Token: 0x06001DA9 RID: 7593 RVA: 0x0011AE54 File Offset: 0x0011A054
			internal void AddByte(byte b)
			{
				if (this._byteBuffer == null)
				{
					this._byteBuffer = new byte[this._bufferSize];
				}
				byte[] byteBuffer = this._byteBuffer;
				int numBytes = this._numBytes;
				this._numBytes = numBytes + 1;
				byteBuffer[numBytes] = b;
			}

			// Token: 0x06001DAA RID: 7594 RVA: 0x0011AE93 File Offset: 0x0011A093
			internal string GetString()
			{
				if (this._numBytes > 0)
				{
					this.FlushBytes();
				}
				return new string(this._charBuffer, 0, this._numChars);
			}

			// Token: 0x04000677 RID: 1655
			private readonly int _bufferSize;

			// Token: 0x04000678 RID: 1656
			private int _numChars;

			// Token: 0x04000679 RID: 1657
			private char[] _charBuffer;

			// Token: 0x0400067A RID: 1658
			private int _numBytes;

			// Token: 0x0400067B RID: 1659
			private byte[] _byteBuffer;

			// Token: 0x0400067C RID: 1660
			private readonly Encoding _encoding;
		}

		// Token: 0x020001D9 RID: 473
		private static class HtmlEntities
		{
			// Token: 0x06001DAB RID: 7595 RVA: 0x0011AEB8 File Offset: 0x0011A0B8
			public static char Lookup(ReadOnlySpan<char> entity)
			{
				if (entity.Length <= 8)
				{
					char result;
					WebUtility.HtmlEntities.s_lookupTable.TryGetValue(WebUtility.HtmlEntities.ToUInt64Key(entity), out result);
					return result;
				}
				return '\0';
			}

			// Token: 0x06001DAC RID: 7596 RVA: 0x0011AEE8 File Offset: 0x0011A0E8
			private unsafe static ulong ToUInt64Key(ReadOnlySpan<char> entity)
			{
				ulong num = 0UL;
				for (int i = 0; i < entity.Length; i++)
				{
					if (*entity[i] > 255)
					{
						return 0UL;
					}
					num = (num << 8 | (ulong)(*entity[i]));
				}
				return num;
			}

			// Token: 0x06001DAD RID: 7597 RVA: 0x0011AF30 File Offset: 0x0011A130
			// Note: this type is marked as 'beforefieldinit'.
			static HtmlEntities()
			{
				Dictionary<ulong, char> dictionary = new Dictionary<ulong, char>(253);
				ulong key = WebUtility.HtmlEntities.ToUInt64Key("quot");
				dictionary[key] = '"';
				ulong key2 = WebUtility.HtmlEntities.ToUInt64Key("amp");
				dictionary[key2] = '&';
				ulong key3 = WebUtility.HtmlEntities.ToUInt64Key("apos");
				dictionary[key3] = '\'';
				ulong key4 = WebUtility.HtmlEntities.ToUInt64Key("lt");
				dictionary[key4] = '<';
				ulong key5 = WebUtility.HtmlEntities.ToUInt64Key("gt");
				dictionary[key5] = '>';
				ulong key6 = WebUtility.HtmlEntities.ToUInt64Key("nbsp");
				dictionary[key6] = '\u00a0';
				ulong key7 = WebUtility.HtmlEntities.ToUInt64Key("iexcl");
				dictionary[key7] = '¡';
				ulong key8 = WebUtility.HtmlEntities.ToUInt64Key("cent");
				dictionary[key8] = '¢';
				ulong key9 = WebUtility.HtmlEntities.ToUInt64Key("pound");
				dictionary[key9] = '£';
				ulong key10 = WebUtility.HtmlEntities.ToUInt64Key("curren");
				dictionary[key10] = '¤';
				ulong key11 = WebUtility.HtmlEntities.ToUInt64Key("yen");
				dictionary[key11] = '¥';
				ulong key12 = WebUtility.HtmlEntities.ToUInt64Key("brvbar");
				dictionary[key12] = '¦';
				ulong key13 = WebUtility.HtmlEntities.ToUInt64Key("sect");
				dictionary[key13] = '§';
				ulong key14 = WebUtility.HtmlEntities.ToUInt64Key("uml");
				dictionary[key14] = '¨';
				ulong key15 = WebUtility.HtmlEntities.ToUInt64Key("copy");
				dictionary[key15] = '©';
				ulong key16 = WebUtility.HtmlEntities.ToUInt64Key("ordf");
				dictionary[key16] = 'ª';
				ulong key17 = WebUtility.HtmlEntities.ToUInt64Key("laquo");
				dictionary[key17] = '«';
				ulong key18 = WebUtility.HtmlEntities.ToUInt64Key("not");
				dictionary[key18] = '¬';
				ulong key19 = WebUtility.HtmlEntities.ToUInt64Key("shy");
				dictionary[key19] = '­';
				ulong key20 = WebUtility.HtmlEntities.ToUInt64Key("reg");
				dictionary[key20] = '®';
				ulong key21 = WebUtility.HtmlEntities.ToUInt64Key("macr");
				dictionary[key21] = '¯';
				ulong key22 = WebUtility.HtmlEntities.ToUInt64Key("deg");
				dictionary[key22] = '°';
				ulong key23 = WebUtility.HtmlEntities.ToUInt64Key("plusmn");
				dictionary[key23] = '±';
				ulong key24 = WebUtility.HtmlEntities.ToUInt64Key("sup2");
				dictionary[key24] = '²';
				ulong key25 = WebUtility.HtmlEntities.ToUInt64Key("sup3");
				dictionary[key25] = '³';
				ulong key26 = WebUtility.HtmlEntities.ToUInt64Key("acute");
				dictionary[key26] = '´';
				ulong key27 = WebUtility.HtmlEntities.ToUInt64Key("micro");
				dictionary[key27] = 'µ';
				ulong key28 = WebUtility.HtmlEntities.ToUInt64Key("para");
				dictionary[key28] = '¶';
				ulong key29 = WebUtility.HtmlEntities.ToUInt64Key("middot");
				dictionary[key29] = '·';
				ulong key30 = WebUtility.HtmlEntities.ToUInt64Key("cedil");
				dictionary[key30] = '¸';
				ulong key31 = WebUtility.HtmlEntities.ToUInt64Key("sup1");
				dictionary[key31] = '¹';
				ulong key32 = WebUtility.HtmlEntities.ToUInt64Key("ordm");
				dictionary[key32] = 'º';
				ulong key33 = WebUtility.HtmlEntities.ToUInt64Key("raquo");
				dictionary[key33] = '»';
				ulong key34 = WebUtility.HtmlEntities.ToUInt64Key("frac14");
				dictionary[key34] = '¼';
				ulong key35 = WebUtility.HtmlEntities.ToUInt64Key("frac12");
				dictionary[key35] = '½';
				ulong key36 = WebUtility.HtmlEntities.ToUInt64Key("frac34");
				dictionary[key36] = '¾';
				ulong key37 = WebUtility.HtmlEntities.ToUInt64Key("iquest");
				dictionary[key37] = '¿';
				ulong key38 = WebUtility.HtmlEntities.ToUInt64Key("Agrave");
				dictionary[key38] = 'À';
				ulong key39 = WebUtility.HtmlEntities.ToUInt64Key("Aacute");
				dictionary[key39] = 'Á';
				ulong key40 = WebUtility.HtmlEntities.ToUInt64Key("Acirc");
				dictionary[key40] = 'Â';
				ulong key41 = WebUtility.HtmlEntities.ToUInt64Key("Atilde");
				dictionary[key41] = 'Ã';
				ulong key42 = WebUtility.HtmlEntities.ToUInt64Key("Auml");
				dictionary[key42] = 'Ä';
				ulong key43 = WebUtility.HtmlEntities.ToUInt64Key("Aring");
				dictionary[key43] = 'Å';
				ulong key44 = WebUtility.HtmlEntities.ToUInt64Key("AElig");
				dictionary[key44] = 'Æ';
				ulong key45 = WebUtility.HtmlEntities.ToUInt64Key("Ccedil");
				dictionary[key45] = 'Ç';
				ulong key46 = WebUtility.HtmlEntities.ToUInt64Key("Egrave");
				dictionary[key46] = 'È';
				ulong key47 = WebUtility.HtmlEntities.ToUInt64Key("Eacute");
				dictionary[key47] = 'É';
				ulong key48 = WebUtility.HtmlEntities.ToUInt64Key("Ecirc");
				dictionary[key48] = 'Ê';
				ulong key49 = WebUtility.HtmlEntities.ToUInt64Key("Euml");
				dictionary[key49] = 'Ë';
				ulong key50 = WebUtility.HtmlEntities.ToUInt64Key("Igrave");
				dictionary[key50] = 'Ì';
				ulong key51 = WebUtility.HtmlEntities.ToUInt64Key("Iacute");
				dictionary[key51] = 'Í';
				ulong key52 = WebUtility.HtmlEntities.ToUInt64Key("Icirc");
				dictionary[key52] = 'Î';
				ulong key53 = WebUtility.HtmlEntities.ToUInt64Key("Iuml");
				dictionary[key53] = 'Ï';
				ulong key54 = WebUtility.HtmlEntities.ToUInt64Key("ETH");
				dictionary[key54] = 'Ð';
				ulong key55 = WebUtility.HtmlEntities.ToUInt64Key("Ntilde");
				dictionary[key55] = 'Ñ';
				ulong key56 = WebUtility.HtmlEntities.ToUInt64Key("Ograve");
				dictionary[key56] = 'Ò';
				ulong key57 = WebUtility.HtmlEntities.ToUInt64Key("Oacute");
				dictionary[key57] = 'Ó';
				ulong key58 = WebUtility.HtmlEntities.ToUInt64Key("Ocirc");
				dictionary[key58] = 'Ô';
				ulong key59 = WebUtility.HtmlEntities.ToUInt64Key("Otilde");
				dictionary[key59] = 'Õ';
				ulong key60 = WebUtility.HtmlEntities.ToUInt64Key("Ouml");
				dictionary[key60] = 'Ö';
				ulong key61 = WebUtility.HtmlEntities.ToUInt64Key("times");
				dictionary[key61] = '×';
				ulong key62 = WebUtility.HtmlEntities.ToUInt64Key("Oslash");
				dictionary[key62] = 'Ø';
				ulong key63 = WebUtility.HtmlEntities.ToUInt64Key("Ugrave");
				dictionary[key63] = 'Ù';
				ulong key64 = WebUtility.HtmlEntities.ToUInt64Key("Uacute");
				dictionary[key64] = 'Ú';
				ulong key65 = WebUtility.HtmlEntities.ToUInt64Key("Ucirc");
				dictionary[key65] = 'Û';
				ulong key66 = WebUtility.HtmlEntities.ToUInt64Key("Uuml");
				dictionary[key66] = 'Ü';
				ulong key67 = WebUtility.HtmlEntities.ToUInt64Key("Yacute");
				dictionary[key67] = 'Ý';
				ulong key68 = WebUtility.HtmlEntities.ToUInt64Key("THORN");
				dictionary[key68] = 'Þ';
				ulong key69 = WebUtility.HtmlEntities.ToUInt64Key("szlig");
				dictionary[key69] = 'ß';
				ulong key70 = WebUtility.HtmlEntities.ToUInt64Key("agrave");
				dictionary[key70] = 'à';
				ulong key71 = WebUtility.HtmlEntities.ToUInt64Key("aacute");
				dictionary[key71] = 'á';
				ulong key72 = WebUtility.HtmlEntities.ToUInt64Key("acirc");
				dictionary[key72] = 'â';
				ulong key73 = WebUtility.HtmlEntities.ToUInt64Key("atilde");
				dictionary[key73] = 'ã';
				ulong key74 = WebUtility.HtmlEntities.ToUInt64Key("auml");
				dictionary[key74] = 'ä';
				ulong key75 = WebUtility.HtmlEntities.ToUInt64Key("aring");
				dictionary[key75] = 'å';
				ulong key76 = WebUtility.HtmlEntities.ToUInt64Key("aelig");
				dictionary[key76] = 'æ';
				ulong key77 = WebUtility.HtmlEntities.ToUInt64Key("ccedil");
				dictionary[key77] = 'ç';
				ulong key78 = WebUtility.HtmlEntities.ToUInt64Key("egrave");
				dictionary[key78] = 'è';
				ulong key79 = WebUtility.HtmlEntities.ToUInt64Key("eacute");
				dictionary[key79] = 'é';
				ulong key80 = WebUtility.HtmlEntities.ToUInt64Key("ecirc");
				dictionary[key80] = 'ê';
				ulong key81 = WebUtility.HtmlEntities.ToUInt64Key("euml");
				dictionary[key81] = 'ë';
				ulong key82 = WebUtility.HtmlEntities.ToUInt64Key("igrave");
				dictionary[key82] = 'ì';
				ulong key83 = WebUtility.HtmlEntities.ToUInt64Key("iacute");
				dictionary[key83] = 'í';
				ulong key84 = WebUtility.HtmlEntities.ToUInt64Key("icirc");
				dictionary[key84] = 'î';
				ulong key85 = WebUtility.HtmlEntities.ToUInt64Key("iuml");
				dictionary[key85] = 'ï';
				ulong key86 = WebUtility.HtmlEntities.ToUInt64Key("eth");
				dictionary[key86] = 'ð';
				ulong key87 = WebUtility.HtmlEntities.ToUInt64Key("ntilde");
				dictionary[key87] = 'ñ';
				ulong key88 = WebUtility.HtmlEntities.ToUInt64Key("ograve");
				dictionary[key88] = 'ò';
				ulong key89 = WebUtility.HtmlEntities.ToUInt64Key("oacute");
				dictionary[key89] = 'ó';
				ulong key90 = WebUtility.HtmlEntities.ToUInt64Key("ocirc");
				dictionary[key90] = 'ô';
				ulong key91 = WebUtility.HtmlEntities.ToUInt64Key("otilde");
				dictionary[key91] = 'õ';
				ulong key92 = WebUtility.HtmlEntities.ToUInt64Key("ouml");
				dictionary[key92] = 'ö';
				ulong key93 = WebUtility.HtmlEntities.ToUInt64Key("divide");
				dictionary[key93] = '÷';
				ulong key94 = WebUtility.HtmlEntities.ToUInt64Key("oslash");
				dictionary[key94] = 'ø';
				ulong key95 = WebUtility.HtmlEntities.ToUInt64Key("ugrave");
				dictionary[key95] = 'ù';
				ulong key96 = WebUtility.HtmlEntities.ToUInt64Key("uacute");
				dictionary[key96] = 'ú';
				ulong key97 = WebUtility.HtmlEntities.ToUInt64Key("ucirc");
				dictionary[key97] = 'û';
				ulong key98 = WebUtility.HtmlEntities.ToUInt64Key("uuml");
				dictionary[key98] = 'ü';
				ulong key99 = WebUtility.HtmlEntities.ToUInt64Key("yacute");
				dictionary[key99] = 'ý';
				ulong key100 = WebUtility.HtmlEntities.ToUInt64Key("thorn");
				dictionary[key100] = 'þ';
				ulong key101 = WebUtility.HtmlEntities.ToUInt64Key("yuml");
				dictionary[key101] = 'ÿ';
				ulong key102 = WebUtility.HtmlEntities.ToUInt64Key("OElig");
				dictionary[key102] = 'Œ';
				ulong key103 = WebUtility.HtmlEntities.ToUInt64Key("oelig");
				dictionary[key103] = 'œ';
				ulong key104 = WebUtility.HtmlEntities.ToUInt64Key("Scaron");
				dictionary[key104] = 'Š';
				ulong key105 = WebUtility.HtmlEntities.ToUInt64Key("scaron");
				dictionary[key105] = 'š';
				ulong key106 = WebUtility.HtmlEntities.ToUInt64Key("Yuml");
				dictionary[key106] = 'Ÿ';
				ulong key107 = WebUtility.HtmlEntities.ToUInt64Key("fnof");
				dictionary[key107] = 'ƒ';
				ulong key108 = WebUtility.HtmlEntities.ToUInt64Key("circ");
				dictionary[key108] = 'ˆ';
				ulong key109 = WebUtility.HtmlEntities.ToUInt64Key("tilde");
				dictionary[key109] = '˜';
				ulong key110 = WebUtility.HtmlEntities.ToUInt64Key("Alpha");
				dictionary[key110] = 'Α';
				ulong key111 = WebUtility.HtmlEntities.ToUInt64Key("Beta");
				dictionary[key111] = 'Β';
				ulong key112 = WebUtility.HtmlEntities.ToUInt64Key("Gamma");
				dictionary[key112] = 'Γ';
				ulong key113 = WebUtility.HtmlEntities.ToUInt64Key("Delta");
				dictionary[key113] = 'Δ';
				ulong key114 = WebUtility.HtmlEntities.ToUInt64Key("Epsilon");
				dictionary[key114] = 'Ε';
				ulong key115 = WebUtility.HtmlEntities.ToUInt64Key("Zeta");
				dictionary[key115] = 'Ζ';
				ulong key116 = WebUtility.HtmlEntities.ToUInt64Key("Eta");
				dictionary[key116] = 'Η';
				ulong key117 = WebUtility.HtmlEntities.ToUInt64Key("Theta");
				dictionary[key117] = 'Θ';
				ulong key118 = WebUtility.HtmlEntities.ToUInt64Key("Iota");
				dictionary[key118] = 'Ι';
				ulong key119 = WebUtility.HtmlEntities.ToUInt64Key("Kappa");
				dictionary[key119] = 'Κ';
				ulong key120 = WebUtility.HtmlEntities.ToUInt64Key("Lambda");
				dictionary[key120] = 'Λ';
				ulong key121 = WebUtility.HtmlEntities.ToUInt64Key("Mu");
				dictionary[key121] = 'Μ';
				ulong key122 = WebUtility.HtmlEntities.ToUInt64Key("Nu");
				dictionary[key122] = 'Ν';
				ulong key123 = WebUtility.HtmlEntities.ToUInt64Key("Xi");
				dictionary[key123] = 'Ξ';
				ulong key124 = WebUtility.HtmlEntities.ToUInt64Key("Omicron");
				dictionary[key124] = 'Ο';
				ulong key125 = WebUtility.HtmlEntities.ToUInt64Key("Pi");
				dictionary[key125] = 'Π';
				ulong key126 = WebUtility.HtmlEntities.ToUInt64Key("Rho");
				dictionary[key126] = 'Ρ';
				ulong key127 = WebUtility.HtmlEntities.ToUInt64Key("Sigma");
				dictionary[key127] = 'Σ';
				ulong key128 = WebUtility.HtmlEntities.ToUInt64Key("Tau");
				dictionary[key128] = 'Τ';
				ulong key129 = WebUtility.HtmlEntities.ToUInt64Key("Upsilon");
				dictionary[key129] = 'Υ';
				ulong key130 = WebUtility.HtmlEntities.ToUInt64Key("Phi");
				dictionary[key130] = 'Φ';
				ulong key131 = WebUtility.HtmlEntities.ToUInt64Key("Chi");
				dictionary[key131] = 'Χ';
				ulong key132 = WebUtility.HtmlEntities.ToUInt64Key("Psi");
				dictionary[key132] = 'Ψ';
				ulong key133 = WebUtility.HtmlEntities.ToUInt64Key("Omega");
				dictionary[key133] = 'Ω';
				ulong key134 = WebUtility.HtmlEntities.ToUInt64Key("alpha");
				dictionary[key134] = 'α';
				ulong key135 = WebUtility.HtmlEntities.ToUInt64Key("beta");
				dictionary[key135] = 'β';
				ulong key136 = WebUtility.HtmlEntities.ToUInt64Key("gamma");
				dictionary[key136] = 'γ';
				ulong key137 = WebUtility.HtmlEntities.ToUInt64Key("delta");
				dictionary[key137] = 'δ';
				ulong key138 = WebUtility.HtmlEntities.ToUInt64Key("epsilon");
				dictionary[key138] = 'ε';
				ulong key139 = WebUtility.HtmlEntities.ToUInt64Key("zeta");
				dictionary[key139] = 'ζ';
				ulong key140 = WebUtility.HtmlEntities.ToUInt64Key("eta");
				dictionary[key140] = 'η';
				ulong key141 = WebUtility.HtmlEntities.ToUInt64Key("theta");
				dictionary[key141] = 'θ';
				ulong key142 = WebUtility.HtmlEntities.ToUInt64Key("iota");
				dictionary[key142] = 'ι';
				ulong key143 = WebUtility.HtmlEntities.ToUInt64Key("kappa");
				dictionary[key143] = 'κ';
				ulong key144 = WebUtility.HtmlEntities.ToUInt64Key("lambda");
				dictionary[key144] = 'λ';
				ulong key145 = WebUtility.HtmlEntities.ToUInt64Key("mu");
				dictionary[key145] = 'μ';
				ulong key146 = WebUtility.HtmlEntities.ToUInt64Key("nu");
				dictionary[key146] = 'ν';
				ulong key147 = WebUtility.HtmlEntities.ToUInt64Key("xi");
				dictionary[key147] = 'ξ';
				ulong key148 = WebUtility.HtmlEntities.ToUInt64Key("omicron");
				dictionary[key148] = 'ο';
				ulong key149 = WebUtility.HtmlEntities.ToUInt64Key("pi");
				dictionary[key149] = 'π';
				ulong key150 = WebUtility.HtmlEntities.ToUInt64Key("rho");
				dictionary[key150] = 'ρ';
				ulong key151 = WebUtility.HtmlEntities.ToUInt64Key("sigmaf");
				dictionary[key151] = 'ς';
				ulong key152 = WebUtility.HtmlEntities.ToUInt64Key("sigma");
				dictionary[key152] = 'σ';
				ulong key153 = WebUtility.HtmlEntities.ToUInt64Key("tau");
				dictionary[key153] = 'τ';
				ulong key154 = WebUtility.HtmlEntities.ToUInt64Key("upsilon");
				dictionary[key154] = 'υ';
				ulong key155 = WebUtility.HtmlEntities.ToUInt64Key("phi");
				dictionary[key155] = 'φ';
				ulong key156 = WebUtility.HtmlEntities.ToUInt64Key("chi");
				dictionary[key156] = 'χ';
				ulong key157 = WebUtility.HtmlEntities.ToUInt64Key("psi");
				dictionary[key157] = 'ψ';
				ulong key158 = WebUtility.HtmlEntities.ToUInt64Key("omega");
				dictionary[key158] = 'ω';
				ulong key159 = WebUtility.HtmlEntities.ToUInt64Key("thetasym");
				dictionary[key159] = 'ϑ';
				ulong key160 = WebUtility.HtmlEntities.ToUInt64Key("upsih");
				dictionary[key160] = 'ϒ';
				ulong key161 = WebUtility.HtmlEntities.ToUInt64Key("piv");
				dictionary[key161] = 'ϖ';
				ulong key162 = WebUtility.HtmlEntities.ToUInt64Key("ensp");
				dictionary[key162] = '\u2002';
				ulong key163 = WebUtility.HtmlEntities.ToUInt64Key("emsp");
				dictionary[key163] = '\u2003';
				ulong key164 = WebUtility.HtmlEntities.ToUInt64Key("thinsp");
				dictionary[key164] = '\u2009';
				ulong key165 = WebUtility.HtmlEntities.ToUInt64Key("zwnj");
				dictionary[key165] = '‌';
				ulong key166 = WebUtility.HtmlEntities.ToUInt64Key("zwj");
				dictionary[key166] = '‍';
				ulong key167 = WebUtility.HtmlEntities.ToUInt64Key("lrm");
				dictionary[key167] = '‎';
				ulong key168 = WebUtility.HtmlEntities.ToUInt64Key("rlm");
				dictionary[key168] = '‏';
				ulong key169 = WebUtility.HtmlEntities.ToUInt64Key("ndash");
				dictionary[key169] = '–';
				ulong key170 = WebUtility.HtmlEntities.ToUInt64Key("mdash");
				dictionary[key170] = '—';
				ulong key171 = WebUtility.HtmlEntities.ToUInt64Key("lsquo");
				dictionary[key171] = '‘';
				ulong key172 = WebUtility.HtmlEntities.ToUInt64Key("rsquo");
				dictionary[key172] = '’';
				ulong key173 = WebUtility.HtmlEntities.ToUInt64Key("sbquo");
				dictionary[key173] = '‚';
				ulong key174 = WebUtility.HtmlEntities.ToUInt64Key("ldquo");
				dictionary[key174] = '“';
				ulong key175 = WebUtility.HtmlEntities.ToUInt64Key("rdquo");
				dictionary[key175] = '”';
				ulong key176 = WebUtility.HtmlEntities.ToUInt64Key("bdquo");
				dictionary[key176] = '„';
				ulong key177 = WebUtility.HtmlEntities.ToUInt64Key("dagger");
				dictionary[key177] = '†';
				ulong key178 = WebUtility.HtmlEntities.ToUInt64Key("Dagger");
				dictionary[key178] = '‡';
				ulong key179 = WebUtility.HtmlEntities.ToUInt64Key("bull");
				dictionary[key179] = '•';
				ulong key180 = WebUtility.HtmlEntities.ToUInt64Key("hellip");
				dictionary[key180] = '…';
				ulong key181 = WebUtility.HtmlEntities.ToUInt64Key("permil");
				dictionary[key181] = '‰';
				ulong key182 = WebUtility.HtmlEntities.ToUInt64Key("prime");
				dictionary[key182] = '′';
				ulong key183 = WebUtility.HtmlEntities.ToUInt64Key("Prime");
				dictionary[key183] = '″';
				ulong key184 = WebUtility.HtmlEntities.ToUInt64Key("lsaquo");
				dictionary[key184] = '‹';
				ulong key185 = WebUtility.HtmlEntities.ToUInt64Key("rsaquo");
				dictionary[key185] = '›';
				ulong key186 = WebUtility.HtmlEntities.ToUInt64Key("oline");
				dictionary[key186] = '‾';
				ulong key187 = WebUtility.HtmlEntities.ToUInt64Key("frasl");
				dictionary[key187] = '⁄';
				ulong key188 = WebUtility.HtmlEntities.ToUInt64Key("euro");
				dictionary[key188] = '€';
				ulong key189 = WebUtility.HtmlEntities.ToUInt64Key("image");
				dictionary[key189] = 'ℑ';
				ulong key190 = WebUtility.HtmlEntities.ToUInt64Key("weierp");
				dictionary[key190] = '℘';
				ulong key191 = WebUtility.HtmlEntities.ToUInt64Key("real");
				dictionary[key191] = 'ℜ';
				ulong key192 = WebUtility.HtmlEntities.ToUInt64Key("trade");
				dictionary[key192] = '™';
				ulong key193 = WebUtility.HtmlEntities.ToUInt64Key("alefsym");
				dictionary[key193] = 'ℵ';
				ulong key194 = WebUtility.HtmlEntities.ToUInt64Key("larr");
				dictionary[key194] = '←';
				ulong key195 = WebUtility.HtmlEntities.ToUInt64Key("uarr");
				dictionary[key195] = '↑';
				ulong key196 = WebUtility.HtmlEntities.ToUInt64Key("rarr");
				dictionary[key196] = '→';
				ulong key197 = WebUtility.HtmlEntities.ToUInt64Key("darr");
				dictionary[key197] = '↓';
				ulong key198 = WebUtility.HtmlEntities.ToUInt64Key("harr");
				dictionary[key198] = '↔';
				ulong key199 = WebUtility.HtmlEntities.ToUInt64Key("crarr");
				dictionary[key199] = '↵';
				ulong key200 = WebUtility.HtmlEntities.ToUInt64Key("lArr");
				dictionary[key200] = '⇐';
				ulong key201 = WebUtility.HtmlEntities.ToUInt64Key("uArr");
				dictionary[key201] = '⇑';
				ulong key202 = WebUtility.HtmlEntities.ToUInt64Key("rArr");
				dictionary[key202] = '⇒';
				ulong key203 = WebUtility.HtmlEntities.ToUInt64Key("dArr");
				dictionary[key203] = '⇓';
				ulong key204 = WebUtility.HtmlEntities.ToUInt64Key("hArr");
				dictionary[key204] = '⇔';
				ulong key205 = WebUtility.HtmlEntities.ToUInt64Key("forall");
				dictionary[key205] = '∀';
				ulong key206 = WebUtility.HtmlEntities.ToUInt64Key("part");
				dictionary[key206] = '∂';
				ulong key207 = WebUtility.HtmlEntities.ToUInt64Key("exist");
				dictionary[key207] = '∃';
				ulong key208 = WebUtility.HtmlEntities.ToUInt64Key("empty");
				dictionary[key208] = '∅';
				ulong key209 = WebUtility.HtmlEntities.ToUInt64Key("nabla");
				dictionary[key209] = '∇';
				ulong key210 = WebUtility.HtmlEntities.ToUInt64Key("isin");
				dictionary[key210] = '∈';
				ulong key211 = WebUtility.HtmlEntities.ToUInt64Key("notin");
				dictionary[key211] = '∉';
				ulong key212 = WebUtility.HtmlEntities.ToUInt64Key("ni");
				dictionary[key212] = '∋';
				ulong key213 = WebUtility.HtmlEntities.ToUInt64Key("prod");
				dictionary[key213] = '∏';
				ulong key214 = WebUtility.HtmlEntities.ToUInt64Key("sum");
				dictionary[key214] = '∑';
				ulong key215 = WebUtility.HtmlEntities.ToUInt64Key("minus");
				dictionary[key215] = '−';
				ulong key216 = WebUtility.HtmlEntities.ToUInt64Key("lowast");
				dictionary[key216] = '∗';
				ulong key217 = WebUtility.HtmlEntities.ToUInt64Key("radic");
				dictionary[key217] = '√';
				ulong key218 = WebUtility.HtmlEntities.ToUInt64Key("prop");
				dictionary[key218] = '∝';
				ulong key219 = WebUtility.HtmlEntities.ToUInt64Key("infin");
				dictionary[key219] = '∞';
				ulong key220 = WebUtility.HtmlEntities.ToUInt64Key("ang");
				dictionary[key220] = '∠';
				ulong key221 = WebUtility.HtmlEntities.ToUInt64Key("and");
				dictionary[key221] = '∧';
				ulong key222 = WebUtility.HtmlEntities.ToUInt64Key("or");
				dictionary[key222] = '∨';
				ulong key223 = WebUtility.HtmlEntities.ToUInt64Key("cap");
				dictionary[key223] = '∩';
				ulong key224 = WebUtility.HtmlEntities.ToUInt64Key("cup");
				dictionary[key224] = '∪';
				ulong key225 = WebUtility.HtmlEntities.ToUInt64Key("int");
				dictionary[key225] = '∫';
				ulong key226 = WebUtility.HtmlEntities.ToUInt64Key("there4");
				dictionary[key226] = '∴';
				ulong key227 = WebUtility.HtmlEntities.ToUInt64Key("sim");
				dictionary[key227] = '∼';
				ulong key228 = WebUtility.HtmlEntities.ToUInt64Key("cong");
				dictionary[key228] = '≅';
				ulong key229 = WebUtility.HtmlEntities.ToUInt64Key("asymp");
				dictionary[key229] = '≈';
				ulong key230 = WebUtility.HtmlEntities.ToUInt64Key("ne");
				dictionary[key230] = '≠';
				ulong key231 = WebUtility.HtmlEntities.ToUInt64Key("equiv");
				dictionary[key231] = '≡';
				ulong key232 = WebUtility.HtmlEntities.ToUInt64Key("le");
				dictionary[key232] = '≤';
				ulong key233 = WebUtility.HtmlEntities.ToUInt64Key("ge");
				dictionary[key233] = '≥';
				ulong key234 = WebUtility.HtmlEntities.ToUInt64Key("sub");
				dictionary[key234] = '⊂';
				ulong key235 = WebUtility.HtmlEntities.ToUInt64Key("sup");
				dictionary[key235] = '⊃';
				ulong key236 = WebUtility.HtmlEntities.ToUInt64Key("nsub");
				dictionary[key236] = '⊄';
				ulong key237 = WebUtility.HtmlEntities.ToUInt64Key("sube");
				dictionary[key237] = '⊆';
				ulong key238 = WebUtility.HtmlEntities.ToUInt64Key("supe");
				dictionary[key238] = '⊇';
				ulong key239 = WebUtility.HtmlEntities.ToUInt64Key("oplus");
				dictionary[key239] = '⊕';
				ulong key240 = WebUtility.HtmlEntities.ToUInt64Key("otimes");
				dictionary[key240] = '⊗';
				ulong key241 = WebUtility.HtmlEntities.ToUInt64Key("perp");
				dictionary[key241] = '⊥';
				ulong key242 = WebUtility.HtmlEntities.ToUInt64Key("sdot");
				dictionary[key242] = '⋅';
				ulong key243 = WebUtility.HtmlEntities.ToUInt64Key("lceil");
				dictionary[key243] = '⌈';
				ulong key244 = WebUtility.HtmlEntities.ToUInt64Key("rceil");
				dictionary[key244] = '⌉';
				ulong key245 = WebUtility.HtmlEntities.ToUInt64Key("lfloor");
				dictionary[key245] = '⌊';
				ulong key246 = WebUtility.HtmlEntities.ToUInt64Key("rfloor");
				dictionary[key246] = '⌋';
				ulong key247 = WebUtility.HtmlEntities.ToUInt64Key("lang");
				dictionary[key247] = '〈';
				ulong key248 = WebUtility.HtmlEntities.ToUInt64Key("rang");
				dictionary[key248] = '〉';
				ulong key249 = WebUtility.HtmlEntities.ToUInt64Key("loz");
				dictionary[key249] = '◊';
				ulong key250 = WebUtility.HtmlEntities.ToUInt64Key("spades");
				dictionary[key250] = '♠';
				ulong key251 = WebUtility.HtmlEntities.ToUInt64Key("clubs");
				dictionary[key251] = '♣';
				ulong key252 = WebUtility.HtmlEntities.ToUInt64Key("hearts");
				dictionary[key252] = '♥';
				ulong key253 = WebUtility.HtmlEntities.ToUInt64Key("diams");
				dictionary[key253] = '♦';
				WebUtility.HtmlEntities.s_lookupTable = dictionary;
			}

			// Token: 0x0400067D RID: 1661
			private static readonly Dictionary<ulong, char> s_lookupTable;
		}
	}
}
