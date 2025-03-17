using System;
using System.Buffers;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using Internal.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000370 RID: 880
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Encoding : ICloneable
	{
		// Token: 0x17000938 RID: 2360
		// (get) Token: 0x06002E26 RID: 11814 RVA: 0x0015BD80 File Offset: 0x0015AF80
		public static Encoding Default
		{
			get
			{
				return Encoding.s_defaultEncoding;
			}
		}

		// Token: 0x06002E27 RID: 11815 RVA: 0x0015BD87 File Offset: 0x0015AF87
		protected Encoding() : this(0)
		{
		}

		// Token: 0x06002E28 RID: 11816 RVA: 0x0015BD90 File Offset: 0x0015AF90
		protected Encoding(int codePage)
		{
			this._isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this._codePage = codePage;
			this.SetDefaultFallbacks();
		}

		// Token: 0x06002E29 RID: 11817 RVA: 0x0015BDBC File Offset: 0x0015AFBC
		[NullableContext(2)]
		protected Encoding(int codePage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			this._isReadOnly = true;
			base..ctor();
			if (codePage < 0)
			{
				throw new ArgumentOutOfRangeException("codePage");
			}
			this._codePage = codePage;
			this.encoderFallback = (encoderFallback ?? EncoderFallback.ReplacementFallback);
			this.decoderFallback = (decoderFallback ?? DecoderFallback.ReplacementFallback);
		}

		// Token: 0x06002E2A RID: 11818 RVA: 0x0015796B File Offset: 0x00156B6B
		[MemberNotNull("encoderFallback")]
		[MemberNotNull("decoderFallback")]
		internal virtual void SetDefaultFallbacks()
		{
			this.encoderFallback = EncoderFallback.ReplacementFallback;
			this.decoderFallback = DecoderFallback.ReplacementFallback;
		}

		// Token: 0x06002E2B RID: 11819 RVA: 0x0015BE0C File Offset: 0x0015B00C
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes");
			}
			return Encoding.Convert(srcEncoding, dstEncoding, bytes, 0, bytes.Length);
		}

		// Token: 0x06002E2C RID: 11820 RVA: 0x0015BE28 File Offset: 0x0015B028
		public static byte[] Convert(Encoding srcEncoding, Encoding dstEncoding, byte[] bytes, int index, int count)
		{
			if (srcEncoding == null || dstEncoding == null)
			{
				throw new ArgumentNullException((srcEncoding == null) ? "srcEncoding" : "dstEncoding", SR.ArgumentNull_Array);
			}
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			return dstEncoding.GetBytes(srcEncoding.GetChars(bytes, index, count));
		}

		// Token: 0x06002E2D RID: 11821 RVA: 0x0015BE78 File Offset: 0x0015B078
		public static void RegisterProvider(EncodingProvider provider)
		{
			EncodingProvider.AddProvider(provider);
		}

		// Token: 0x06002E2E RID: 11822 RVA: 0x0015BE80 File Offset: 0x0015B080
		public static Encoding GetEncoding(int codepage)
		{
			Encoding encoding = Encoding.FilterDisallowedEncodings(EncodingProvider.GetEncodingFromProvider(codepage));
			if (encoding != null)
			{
				return encoding;
			}
			if (codepage <= 1201)
			{
				if (codepage <= 3)
				{
					if (codepage == 0)
					{
						return Encoding.Default;
					}
					if (codepage - 1 > 2)
					{
						goto IL_10D;
					}
				}
				else if (codepage != 42)
				{
					if (codepage == 1200)
					{
						return Encoding.Unicode;
					}
					if (codepage != 1201)
					{
						goto IL_10D;
					}
					return Encoding.BigEndianUnicode;
				}
				throw new ArgumentException(SR.Format(SR.Argument_CodepageNotSupported, codepage), "codepage");
			}
			if (codepage <= 20127)
			{
				if (codepage == 12000)
				{
					return Encoding.UTF32;
				}
				if (codepage == 12001)
				{
					return Encoding.BigEndianUTF32;
				}
				if (codepage == 20127)
				{
					return Encoding.ASCII;
				}
			}
			else
			{
				if (codepage == 28591)
				{
					return Encoding.Latin1;
				}
				if (codepage != 65000)
				{
					if (codepage == 65001)
					{
						return Encoding.UTF8;
					}
				}
				else
				{
					if (LocalAppContextSwitches.EnableUnsafeUTF7Encoding)
					{
						return Encoding.UTF7;
					}
					string p = string.Format(CultureInfo.InvariantCulture, "https://aka.ms/dotnet-warnings/{0}", "SYSLIB0001");
					string message = SR.Format(SR.Encoding_UTF7_Disabled, p);
					throw new NotSupportedException(message);
				}
			}
			IL_10D:
			if (codepage < 0 || codepage > 65535)
			{
				throw new ArgumentOutOfRangeException("codepage", SR.Format(SR.ArgumentOutOfRange_Range, 0, 65535));
			}
			throw new NotSupportedException(SR.Format(SR.NotSupported_NoCodepageData, codepage));
		}

		// Token: 0x06002E2F RID: 11823 RVA: 0x0015BFE0 File Offset: 0x0015B1E0
		public static Encoding GetEncoding(int codepage, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			Encoding encoding = Encoding.FilterDisallowedEncodings(EncodingProvider.GetEncodingFromProvider(codepage, encoderFallback, decoderFallback));
			if (encoding != null)
			{
				return encoding;
			}
			encoding = Encoding.GetEncoding(codepage);
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = encoderFallback;
			encoding2.DecoderFallback = decoderFallback;
			return encoding2;
		}

		// Token: 0x06002E30 RID: 11824 RVA: 0x0015C022 File Offset: 0x0015B222
		public static Encoding GetEncoding(string name)
		{
			return Encoding.FilterDisallowedEncodings(EncodingProvider.GetEncodingFromProvider(name)) ?? Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name));
		}

		// Token: 0x06002E31 RID: 11825 RVA: 0x0015C03E File Offset: 0x0015B23E
		public static Encoding GetEncoding(string name, EncoderFallback encoderFallback, DecoderFallback decoderFallback)
		{
			return Encoding.FilterDisallowedEncodings(EncodingProvider.GetEncodingFromProvider(name, encoderFallback, decoderFallback)) ?? Encoding.GetEncoding(EncodingTable.GetCodePageFromName(name), encoderFallback, decoderFallback);
		}

		// Token: 0x06002E32 RID: 11826 RVA: 0x0015C05E File Offset: 0x0015B25E
		private static Encoding FilterDisallowedEncodings(Encoding encoding)
		{
			if (LocalAppContextSwitches.EnableUnsafeUTF7Encoding)
			{
				return encoding;
			}
			if (encoding == null || encoding.CodePage != 65000)
			{
				return encoding;
			}
			return null;
		}

		// Token: 0x06002E33 RID: 11827 RVA: 0x0015C07C File Offset: 0x0015B27C
		public static EncodingInfo[] GetEncodings()
		{
			Dictionary<int, EncodingInfo> encodingListFromProviders = EncodingProvider.GetEncodingListFromProviders();
			if (encodingListFromProviders != null)
			{
				return EncodingTable.GetEncodings(encodingListFromProviders);
			}
			return EncodingTable.GetEncodings();
		}

		// Token: 0x06002E34 RID: 11828 RVA: 0x0015C09E File Offset: 0x0015B29E
		public virtual byte[] GetPreamble()
		{
			return Array.Empty<byte>();
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002E35 RID: 11829 RVA: 0x0015C0A5 File Offset: 0x0015B2A5
		[Nullable(0)]
		public virtual ReadOnlySpan<byte> Preamble
		{
			[NullableContext(0)]
			get
			{
				return this.GetPreamble();
			}
		}

		// Token: 0x06002E36 RID: 11830 RVA: 0x0015C0B2 File Offset: 0x0015B2B2
		private void GetDataItem()
		{
			if (this._dataItem == null)
			{
				this._dataItem = EncodingTable.GetCodePageDataItem(this._codePage);
				if (this._dataItem == null)
				{
					throw new NotSupportedException(SR.Format(SR.NotSupported_NoCodepageData, this._codePage));
				}
			}
		}

		// Token: 0x1700093A RID: 2362
		// (get) Token: 0x06002E37 RID: 11831 RVA: 0x0015C0F0 File Offset: 0x0015B2F0
		public virtual string BodyName
		{
			get
			{
				if (this._dataItem == null)
				{
					this.GetDataItem();
				}
				return this._dataItem.BodyName;
			}
		}

		// Token: 0x1700093B RID: 2363
		// (get) Token: 0x06002E38 RID: 11832 RVA: 0x0015C10B File Offset: 0x0015B30B
		public virtual string EncodingName
		{
			get
			{
				if (this._dataItem == null)
				{
					this.GetDataItem();
				}
				return this._dataItem.DisplayName;
			}
		}

		// Token: 0x1700093C RID: 2364
		// (get) Token: 0x06002E39 RID: 11833 RVA: 0x0015C126 File Offset: 0x0015B326
		public virtual string HeaderName
		{
			get
			{
				if (this._dataItem == null)
				{
					this.GetDataItem();
				}
				return this._dataItem.HeaderName;
			}
		}

		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x06002E3A RID: 11834 RVA: 0x0015C141 File Offset: 0x0015B341
		public virtual string WebName
		{
			get
			{
				if (this._dataItem == null)
				{
					this.GetDataItem();
				}
				return this._dataItem.WebName;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x0015C15C File Offset: 0x0015B35C
		public virtual int WindowsCodePage
		{
			get
			{
				if (this._dataItem == null)
				{
					this.GetDataItem();
				}
				return this._dataItem.UIFamilyCodePage;
			}
		}

		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002E3C RID: 11836 RVA: 0x0015C177 File Offset: 0x0015B377
		public virtual bool IsBrowserDisplay
		{
			get
			{
				if (this._dataItem == null)
				{
					this.GetDataItem();
				}
				return (this._dataItem.Flags & 2U) > 0U;
			}
		}

		// Token: 0x17000940 RID: 2368
		// (get) Token: 0x06002E3D RID: 11837 RVA: 0x0015C197 File Offset: 0x0015B397
		public virtual bool IsBrowserSave
		{
			get
			{
				if (this._dataItem == null)
				{
					this.GetDataItem();
				}
				return (this._dataItem.Flags & 512U) > 0U;
			}
		}

		// Token: 0x17000941 RID: 2369
		// (get) Token: 0x06002E3E RID: 11838 RVA: 0x0015C1BB File Offset: 0x0015B3BB
		public virtual bool IsMailNewsDisplay
		{
			get
			{
				if (this._dataItem == null)
				{
					this.GetDataItem();
				}
				return (this._dataItem.Flags & 1U) > 0U;
			}
		}

		// Token: 0x17000942 RID: 2370
		// (get) Token: 0x06002E3F RID: 11839 RVA: 0x0015C1DB File Offset: 0x0015B3DB
		public virtual bool IsMailNewsSave
		{
			get
			{
				if (this._dataItem == null)
				{
					this.GetDataItem();
				}
				return (this._dataItem.Flags & 256U) > 0U;
			}
		}

		// Token: 0x17000943 RID: 2371
		// (get) Token: 0x06002E40 RID: 11840 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsSingleByte
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000944 RID: 2372
		// (get) Token: 0x06002E41 RID: 11841 RVA: 0x0015C1FF File Offset: 0x0015B3FF
		// (set) Token: 0x06002E42 RID: 11842 RVA: 0x0015C207 File Offset: 0x0015B407
		public EncoderFallback EncoderFallback
		{
			get
			{
				return this.encoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.encoderFallback = value;
			}
		}

		// Token: 0x17000945 RID: 2373
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x0015C231 File Offset: 0x0015B431
		// (set) Token: 0x06002E44 RID: 11844 RVA: 0x0015C239 File Offset: 0x0015B439
		public DecoderFallback DecoderFallback
		{
			get
			{
				return this.decoderFallback;
			}
			set
			{
				if (this.IsReadOnly)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
				}
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.decoderFallback = value;
			}
		}

		// Token: 0x06002E45 RID: 11845 RVA: 0x0015C264 File Offset: 0x0015B464
		public virtual object Clone()
		{
			Encoding encoding = (Encoding)base.MemberwiseClone();
			encoding._isReadOnly = false;
			return encoding;
		}

		// Token: 0x17000946 RID: 2374
		// (get) Token: 0x06002E46 RID: 11846 RVA: 0x0015C285 File Offset: 0x0015B485
		// (set) Token: 0x06002E47 RID: 11847 RVA: 0x0015C28D File Offset: 0x0015B48D
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
			private protected set
			{
				this._isReadOnly = value;
			}
		}

		// Token: 0x17000947 RID: 2375
		// (get) Token: 0x06002E48 RID: 11848 RVA: 0x0015C296 File Offset: 0x0015B496
		public static Encoding ASCII
		{
			get
			{
				return ASCIIEncoding.s_default;
			}
		}

		// Token: 0x17000948 RID: 2376
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x0015C29D File Offset: 0x0015B49D
		public static Encoding Latin1
		{
			get
			{
				return Latin1Encoding.s_default;
			}
		}

		// Token: 0x06002E4A RID: 11850 RVA: 0x0015C2A4 File Offset: 0x0015B4A4
		public virtual int GetByteCount(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", SR.ArgumentNull_Array);
			}
			return this.GetByteCount(chars, 0, chars.Length);
		}

		// Token: 0x06002E4B RID: 11851 RVA: 0x0015C2C4 File Offset: 0x0015B4C4
		public virtual int GetByteCount(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			char[] array = s.ToCharArray();
			return this.GetByteCount(array, 0, array.Length);
		}

		// Token: 0x06002E4C RID: 11852
		public abstract int GetByteCount(char[] chars, int index, int count);

		// Token: 0x06002E4D RID: 11853 RVA: 0x0015C2F4 File Offset: 0x0015B4F4
		public unsafe int GetByteCount(string s, int index, int count)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", SR.ArgumentNull_String);
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (index > s.Length - count)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_IndexCount);
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
			return this.GetByteCount(ptr2 + index, count);
		}

		// Token: 0x06002E4E RID: 11854 RVA: 0x0015C378 File Offset: 0x0015B578
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe virtual int GetByteCount(char* chars, int count)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", SR.ArgumentNull_Array);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			char[] chars2 = new ReadOnlySpan<char>((void*)chars, count).ToArray();
			return this.GetByteCount(chars2, 0, count);
		}

		// Token: 0x06002E4F RID: 11855 RVA: 0x0015C3C8 File Offset: 0x0015B5C8
		[NullableContext(0)]
		public unsafe virtual int GetByteCount(ReadOnlySpan<char> chars)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* chars2 = nonNullPinnableReference;
				return this.GetByteCount(chars2, chars.Length);
			}
		}

		// Token: 0x06002E50 RID: 11856 RVA: 0x0015C3ED File Offset: 0x0015B5ED
		public virtual byte[] GetBytes(char[] chars)
		{
			if (chars == null)
			{
				throw new ArgumentNullException("chars", SR.ArgumentNull_Array);
			}
			return this.GetBytes(chars, 0, chars.Length);
		}

		// Token: 0x06002E51 RID: 11857 RVA: 0x0015C410 File Offset: 0x0015B610
		public virtual byte[] GetBytes(char[] chars, int index, int count)
		{
			byte[] array = new byte[this.GetByteCount(chars, index, count)];
			this.GetBytes(chars, index, count, array, 0);
			return array;
		}

		// Token: 0x06002E52 RID: 11858
		public abstract int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex);

		// Token: 0x06002E53 RID: 11859 RVA: 0x0015C43C File Offset: 0x0015B63C
		public virtual byte[] GetBytes(string s)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", SR.ArgumentNull_String);
			}
			int byteCount = this.GetByteCount(s);
			byte[] array = new byte[byteCount];
			int bytes = this.GetBytes(s, 0, s.Length, array, 0);
			return array;
		}

		// Token: 0x06002E54 RID: 11860 RVA: 0x0015C480 File Offset: 0x0015B680
		public unsafe byte[] GetBytes(string s, int index, int count)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s", SR.ArgumentNull_String);
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (index > s.Length - count)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_IndexCount);
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
			int byteCount = this.GetByteCount(ptr2 + index, count);
			if (byteCount == 0)
			{
				return Array.Empty<byte>();
			}
			byte[] array = new byte[byteCount];
			fixed (byte* ptr3 = &array[0])
			{
				byte* bytes = ptr3;
				int bytes2 = this.GetBytes(ptr2 + index, count, bytes, byteCount);
			}
			return array;
		}

		// Token: 0x06002E55 RID: 11861 RVA: 0x0015C537 File Offset: 0x0015B737
		public virtual int GetBytes(string s, int charIndex, int charCount, byte[] bytes, int byteIndex)
		{
			if (s == null)
			{
				throw new ArgumentNullException("s");
			}
			return this.GetBytes(s.ToCharArray(), charIndex, charCount, bytes, byteIndex);
		}

		// Token: 0x06002E56 RID: 11862 RVA: 0x0015C55C File Offset: 0x0015B75C
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe virtual int GetBytes(char* chars, int charCount, byte* bytes, int byteCount)
		{
			if (bytes == null || chars == null)
			{
				throw new ArgumentNullException((bytes == null) ? "bytes" : "chars", SR.ArgumentNull_Array);
			}
			if (charCount < 0 || byteCount < 0)
			{
				throw new ArgumentOutOfRangeException((charCount < 0) ? "charCount" : "byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			char[] chars2 = new ReadOnlySpan<char>((void*)chars, charCount).ToArray();
			byte[] array = new byte[byteCount];
			int bytes2 = this.GetBytes(chars2, 0, charCount, array, 0);
			if (bytes2 < byteCount)
			{
				byteCount = bytes2;
			}
			new ReadOnlySpan<byte>(array, 0, byteCount).CopyTo(new Span<byte>((void*)bytes, byteCount));
			return byteCount;
		}

		// Token: 0x06002E57 RID: 11863 RVA: 0x0015C5FC File Offset: 0x0015B7FC
		[NullableContext(0)]
		public unsafe virtual int GetBytes(ReadOnlySpan<char> chars, Span<byte> bytes)
		{
			fixed (char* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
			{
				char* chars2 = nonNullPinnableReference;
				fixed (byte* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
				{
					byte* bytes2 = nonNullPinnableReference2;
					return this.GetBytes(chars2, chars.Length, bytes2, bytes.Length);
				}
			}
		}

		// Token: 0x06002E58 RID: 11864 RVA: 0x0015C633 File Offset: 0x0015B833
		public virtual int GetCharCount(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			return this.GetCharCount(bytes, 0, bytes.Length);
		}

		// Token: 0x06002E59 RID: 11865
		public abstract int GetCharCount(byte[] bytes, int index, int count);

		// Token: 0x06002E5A RID: 11866 RVA: 0x0015C654 File Offset: 0x0015B854
		[CLSCompliant(false)]
		[NullableContext(0)]
		public unsafe virtual int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			byte[] bytes2 = new ReadOnlySpan<byte>((void*)bytes, count).ToArray();
			return this.GetCharCount(bytes2, 0, count);
		}

		// Token: 0x06002E5B RID: 11867 RVA: 0x0015C6A4 File Offset: 0x0015B8A4
		[NullableContext(0)]
		public unsafe virtual int GetCharCount(ReadOnlySpan<byte> bytes)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* bytes2 = nonNullPinnableReference;
				return this.GetCharCount(bytes2, bytes.Length);
			}
		}

		// Token: 0x06002E5C RID: 11868 RVA: 0x0015C6C9 File Offset: 0x0015B8C9
		public virtual char[] GetChars(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			return this.GetChars(bytes, 0, bytes.Length);
		}

		// Token: 0x06002E5D RID: 11869 RVA: 0x0015C6EC File Offset: 0x0015B8EC
		public virtual char[] GetChars(byte[] bytes, int index, int count)
		{
			char[] array = new char[this.GetCharCount(bytes, index, count)];
			this.GetChars(bytes, index, count, array, 0);
			return array;
		}

		// Token: 0x06002E5E RID: 11870
		public abstract int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex);

		// Token: 0x06002E5F RID: 11871 RVA: 0x0015C718 File Offset: 0x0015B918
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe virtual int GetChars(byte* bytes, int byteCount, char* chars, int charCount)
		{
			if (chars == null || bytes == null)
			{
				throw new ArgumentNullException((chars == null) ? "chars" : "bytes", SR.ArgumentNull_Array);
			}
			if (byteCount < 0 || charCount < 0)
			{
				throw new ArgumentOutOfRangeException((byteCount < 0) ? "byteCount" : "charCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			byte[] bytes2 = new ReadOnlySpan<byte>((void*)bytes, byteCount).ToArray();
			char[] array = new char[charCount];
			int chars2 = this.GetChars(bytes2, 0, byteCount, array, 0);
			if (chars2 < charCount)
			{
				charCount = chars2;
			}
			new ReadOnlySpan<char>(array, 0, charCount).CopyTo(new Span<char>((void*)chars, charCount));
			return charCount;
		}

		// Token: 0x06002E60 RID: 11872 RVA: 0x0015C7B8 File Offset: 0x0015B9B8
		[NullableContext(0)]
		public unsafe virtual int GetChars(ReadOnlySpan<byte> bytes, Span<char> chars)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* bytes2 = nonNullPinnableReference;
				fixed (char* nonNullPinnableReference2 = MemoryMarshal.GetNonNullPinnableReference<char>(chars))
				{
					char* chars2 = nonNullPinnableReference2;
					return this.GetChars(bytes2, bytes.Length, chars2, chars.Length);
				}
			}
		}

		// Token: 0x06002E61 RID: 11873 RVA: 0x0015C7EF File Offset: 0x0015B9EF
		[NullableContext(0)]
		[CLSCompliant(false)]
		[return: Nullable(1)]
		public unsafe string GetString(byte* bytes, int byteCount)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			if (byteCount < 0)
			{
				throw new ArgumentOutOfRangeException("byteCount", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			return string.CreateStringFromEncoding(bytes, byteCount, this);
		}

		// Token: 0x06002E62 RID: 11874 RVA: 0x0015C824 File Offset: 0x0015BA24
		[NullableContext(0)]
		[return: Nullable(1)]
		public unsafe string GetString(ReadOnlySpan<byte> bytes)
		{
			fixed (byte* nonNullPinnableReference = MemoryMarshal.GetNonNullPinnableReference<byte>(bytes))
			{
				byte* bytes2 = nonNullPinnableReference;
				return string.CreateStringFromEncoding(bytes2, bytes.Length, this);
			}
		}

		// Token: 0x17000949 RID: 2377
		// (get) Token: 0x06002E63 RID: 11875 RVA: 0x0015C849 File Offset: 0x0015BA49
		public virtual int CodePage
		{
			get
			{
				return this._codePage;
			}
		}

		// Token: 0x06002E64 RID: 11876 RVA: 0x0015C851 File Offset: 0x0015BA51
		public bool IsAlwaysNormalized()
		{
			return this.IsAlwaysNormalized(NormalizationForm.FormC);
		}

		// Token: 0x06002E65 RID: 11877 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsAlwaysNormalized(NormalizationForm form)
		{
			return false;
		}

		// Token: 0x06002E66 RID: 11878 RVA: 0x0015C85A File Offset: 0x0015BA5A
		public virtual Decoder GetDecoder()
		{
			return new Encoding.DefaultDecoder(this);
		}

		// Token: 0x06002E67 RID: 11879 RVA: 0x0015C862 File Offset: 0x0015BA62
		public virtual Encoder GetEncoder()
		{
			return new Encoding.DefaultEncoder(this);
		}

		// Token: 0x06002E68 RID: 11880
		public abstract int GetMaxByteCount(int charCount);

		// Token: 0x06002E69 RID: 11881
		public abstract int GetMaxCharCount(int byteCount);

		// Token: 0x06002E6A RID: 11882 RVA: 0x0015C86A File Offset: 0x0015BA6A
		public virtual string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				throw new ArgumentNullException("bytes", SR.ArgumentNull_Array);
			}
			return this.GetString(bytes, 0, bytes.Length);
		}

		// Token: 0x06002E6B RID: 11883 RVA: 0x0015C88A File Offset: 0x0015BA8A
		public virtual string GetString(byte[] bytes, int index, int count)
		{
			return new string(this.GetChars(bytes, index, count));
		}

		// Token: 0x1700094A RID: 2378
		// (get) Token: 0x06002E6C RID: 11884 RVA: 0x0015C89A File Offset: 0x0015BA9A
		public static Encoding Unicode
		{
			get
			{
				return UnicodeEncoding.s_littleEndianDefault;
			}
		}

		// Token: 0x1700094B RID: 2379
		// (get) Token: 0x06002E6D RID: 11885 RVA: 0x0015C8A1 File Offset: 0x0015BAA1
		public static Encoding BigEndianUnicode
		{
			get
			{
				return UnicodeEncoding.s_bigEndianDefault;
			}
		}

		// Token: 0x1700094C RID: 2380
		// (get) Token: 0x06002E6E RID: 11886 RVA: 0x0015C8A8 File Offset: 0x0015BAA8
		[Obsolete("The UTF-7 encoding is insecure and should not be used. Consider using UTF-8 instead.", DiagnosticId = "SYSLIB0001", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public static Encoding UTF7
		{
			get
			{
				return UTF7Encoding.s_default;
			}
		}

		// Token: 0x1700094D RID: 2381
		// (get) Token: 0x06002E6F RID: 11887 RVA: 0x0015C8AF File Offset: 0x0015BAAF
		public static Encoding UTF8
		{
			get
			{
				return UTF8Encoding.s_default;
			}
		}

		// Token: 0x1700094E RID: 2382
		// (get) Token: 0x06002E70 RID: 11888 RVA: 0x0015C8B6 File Offset: 0x0015BAB6
		public static Encoding UTF32
		{
			get
			{
				return UTF32Encoding.s_default;
			}
		}

		// Token: 0x1700094F RID: 2383
		// (get) Token: 0x06002E71 RID: 11889 RVA: 0x0015C8BD File Offset: 0x0015BABD
		private static Encoding BigEndianUTF32
		{
			get
			{
				return UTF32Encoding.s_bigEndianDefault;
			}
		}

		// Token: 0x06002E72 RID: 11890 RVA: 0x0015C8C4 File Offset: 0x0015BAC4
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			Encoding encoding = value as Encoding;
			return encoding != null && this._codePage == encoding._codePage && this.EncoderFallback.Equals(encoding.EncoderFallback) && this.DecoderFallback.Equals(encoding.DecoderFallback);
		}

		// Token: 0x06002E73 RID: 11891 RVA: 0x0015C90F File Offset: 0x0015BB0F
		public override int GetHashCode()
		{
			return this._codePage + this.EncoderFallback.GetHashCode() + this.DecoderFallback.GetHashCode();
		}

		// Token: 0x06002E74 RID: 11892 RVA: 0x0015C92F File Offset: 0x0015BB2F
		public static Stream CreateTranscodingStream(Stream innerStream, Encoding innerStreamEncoding, Encoding outerStreamEncoding, bool leaveOpen = false)
		{
			if (innerStream == null)
			{
				throw new ArgumentNullException("innerStream");
			}
			if (innerStreamEncoding == null)
			{
				throw new ArgumentNullException("innerStreamEncoding");
			}
			if (outerStreamEncoding == null)
			{
				throw new ArgumentNullException("outerStreamEncoding");
			}
			return new TranscodingStream(innerStream, innerStreamEncoding, outerStreamEncoding, leaveOpen);
		}

		// Token: 0x06002E75 RID: 11893 RVA: 0x0015C964 File Offset: 0x0015BB64
		[DoesNotReturn]
		internal void ThrowBytesOverflow()
		{
			throw new ArgumentException(SR.Format(SR.Argument_EncodingConversionOverflowBytes, this.EncodingName, this.EncoderFallback.GetType()), "bytes");
		}

		// Token: 0x06002E76 RID: 11894 RVA: 0x0015C98B File Offset: 0x0015BB8B
		internal void ThrowBytesOverflow(EncoderNLS encoder, bool nothingEncoded)
		{
			if (encoder == null || encoder._throwOnOverflow || nothingEncoded)
			{
				if (encoder != null && encoder.InternalHasFallbackBuffer)
				{
					encoder.FallbackBuffer.InternalReset();
				}
				this.ThrowBytesOverflow();
			}
			encoder.ClearMustFlush();
		}

		// Token: 0x06002E77 RID: 11895 RVA: 0x0015C9BF File Offset: 0x0015BBBF
		[DoesNotReturn]
		[StackTraceHidden]
		internal static void ThrowConversionOverflow()
		{
			throw new ArgumentException(SR.Argument_ConversionOverflow);
		}

		// Token: 0x06002E78 RID: 11896 RVA: 0x0015C9CB File Offset: 0x0015BBCB
		[DoesNotReturn]
		[StackTraceHidden]
		internal void ThrowCharsOverflow()
		{
			throw new ArgumentException(SR.Format(SR.Argument_EncodingConversionOverflowChars, this.EncodingName, this.DecoderFallback.GetType()), "chars");
		}

		// Token: 0x06002E79 RID: 11897 RVA: 0x0015C9F2 File Offset: 0x0015BBF2
		internal void ThrowCharsOverflow(DecoderNLS decoder, bool nothingDecoded)
		{
			if (decoder == null || decoder._throwOnOverflow || nothingDecoded)
			{
				if (decoder != null && decoder.InternalHasFallbackBuffer)
				{
					decoder.FallbackBuffer.InternalReset();
				}
				this.ThrowCharsOverflow();
			}
			decoder.ClearMustFlush();
		}

		// Token: 0x06002E7A RID: 11898 RVA: 0x000C2700 File Offset: 0x000C1900
		internal virtual OperationStatus DecodeFirstRune(ReadOnlySpan<byte> bytes, out Rune value, out int bytesConsumed)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06002E7B RID: 11899 RVA: 0x000C2700 File Offset: 0x000C1900
		internal virtual OperationStatus EncodeRune(Rune value, Span<byte> bytes, out int bytesWritten)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06002E7C RID: 11900 RVA: 0x000C2700 File Offset: 0x000C1900
		internal virtual bool TryGetByteCount(Rune value, out int byteCount)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06002E7D RID: 11901 RVA: 0x0015CA28 File Offset: 0x0015BC28
		internal unsafe virtual int GetByteCount(char* pChars, int charCount, EncoderNLS encoder)
		{
			int num = 0;
			int num2 = 0;
			if (!encoder.HasLeftoverData)
			{
				num = this.GetByteCountFast(pChars, charCount, encoder.Fallback, out num2);
				if (num2 == charCount)
				{
					return num;
				}
			}
			num += this.GetByteCountWithFallback(pChars, charCount, num2, encoder);
			if (num < 0)
			{
				Encoding.ThrowConversionOverflow();
			}
			return num;
		}

		// Token: 0x06002E7E RID: 11902 RVA: 0x000C2700 File Offset: 0x000C1900
		private protected unsafe virtual int GetByteCountFast(char* pChars, int charsLength, EncoderFallback fallback, out int charsConsumed)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06002E7F RID: 11903 RVA: 0x0015CA70 File Offset: 0x0015BC70
		[MethodImpl(MethodImplOptions.NoInlining)]
		private protected unsafe int GetByteCountWithFallback(char* pCharsOriginal, int originalCharCount, int charsConsumedSoFar)
		{
			return this.GetByteCountWithFallback(new ReadOnlySpan<char>((void*)pCharsOriginal, originalCharCount).Slice(charsConsumedSoFar), originalCharCount, null);
		}

		// Token: 0x06002E80 RID: 11904 RVA: 0x0015CA98 File Offset: 0x0015BC98
		private unsafe int GetByteCountWithFallback(char* pOriginalChars, int originalCharCount, int charsConsumedSoFar, EncoderNLS encoder)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>((void*)pOriginalChars, originalCharCount).Slice(charsConsumedSoFar);
			int start;
			int num = encoder.DrainLeftoverDataForGetByteCount(readOnlySpan, out start);
			readOnlySpan = readOnlySpan.Slice(start);
			num += this.GetByteCountFast((char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(readOnlySpan)), readOnlySpan.Length, encoder.Fallback, out start);
			if (num < 0)
			{
				Encoding.ThrowConversionOverflow();
			}
			readOnlySpan = readOnlySpan.Slice(start);
			if (!readOnlySpan.IsEmpty)
			{
				num += this.GetByteCountWithFallback(readOnlySpan, originalCharCount, encoder);
				if (num < 0)
				{
					Encoding.ThrowConversionOverflow();
				}
			}
			return num;
		}

		// Token: 0x06002E81 RID: 11905 RVA: 0x0015CB20 File Offset: 0x0015BD20
		private protected unsafe virtual int GetByteCountWithFallback(ReadOnlySpan<char> chars, int originalCharsLength, EncoderNLS encoder)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				EncoderFallbackBuffer encoderFallbackBuffer = EncoderFallbackBuffer.CreateAndInitialize(this, encoder, originalCharsLength);
				int num = 0;
				Rune rune;
				int start;
				while (Rune.DecodeFromUtf16(chars, out rune, out start) != OperationStatus.NeedMoreData || encoder == null || encoder.MustFlush)
				{
					int num2 = encoderFallbackBuffer.InternalFallbackGetByteCount(chars, out start);
					num += num2;
					if (num < 0)
					{
						Encoding.ThrowConversionOverflow();
					}
					chars = chars.Slice(start);
					if (!chars.IsEmpty)
					{
						num2 = this.GetByteCountFast((char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(chars)), chars.Length, null, out start);
						num += num2;
						if (num < 0)
						{
							Encoding.ThrowConversionOverflow();
						}
						chars = chars.Slice(start);
					}
					if (chars.IsEmpty)
					{
						break;
					}
				}
				return num;
			}
		}

		// Token: 0x06002E82 RID: 11906 RVA: 0x0015CBCC File Offset: 0x0015BDCC
		internal unsafe virtual int GetBytes(char* pChars, int charCount, byte* pBytes, int byteCount, EncoderNLS encoder)
		{
			int num = 0;
			int num2 = 0;
			if (!encoder.HasLeftoverData)
			{
				num = this.GetBytesFast(pChars, charCount, pBytes, byteCount, out num2);
				if (num2 == charCount)
				{
					encoder._charsUsed = charCount;
					return num;
				}
			}
			return this.GetBytesWithFallback(pChars, charCount, pBytes, byteCount, num2, num, encoder);
		}

		// Token: 0x06002E83 RID: 11907 RVA: 0x000C2700 File Offset: 0x000C1900
		private protected unsafe virtual int GetBytesFast(char* pChars, int charsLength, byte* pBytes, int bytesLength, out int charsConsumed)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06002E84 RID: 11908 RVA: 0x0015CC14 File Offset: 0x0015BE14
		[MethodImpl(MethodImplOptions.NoInlining)]
		private protected unsafe int GetBytesWithFallback(char* pOriginalChars, int originalCharCount, byte* pOriginalBytes, int originalByteCount, int charsConsumedSoFar, int bytesWrittenSoFar)
		{
			return this.GetBytesWithFallback(new ReadOnlySpan<char>((void*)pOriginalChars, originalCharCount).Slice(charsConsumedSoFar), originalCharCount, new Span<byte>((void*)pOriginalBytes, originalByteCount).Slice(bytesWrittenSoFar), originalByteCount, null);
		}

		// Token: 0x06002E85 RID: 11909 RVA: 0x0015CC50 File Offset: 0x0015BE50
		private unsafe int GetBytesWithFallback(char* pOriginalChars, int originalCharCount, byte* pOriginalBytes, int originalByteCount, int charsConsumedSoFar, int bytesWrittenSoFar, EncoderNLS encoder)
		{
			ReadOnlySpan<char> readOnlySpan = new ReadOnlySpan<char>((void*)pOriginalChars, originalCharCount).Slice(charsConsumedSoFar);
			Span<byte> span = new Span<byte>((void*)pOriginalBytes, originalByteCount).Slice(bytesWrittenSoFar);
			int start;
			int bytesFast;
			bool flag = encoder.TryDrainLeftoverDataForGetBytes(readOnlySpan, span, out start, out bytesFast);
			readOnlySpan = readOnlySpan.Slice(start);
			span = span.Slice(bytesFast);
			if (!flag)
			{
				this.ThrowBytesOverflow(encoder, span.Length == originalByteCount);
			}
			else
			{
				bytesFast = this.GetBytesFast((char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(readOnlySpan)), readOnlySpan.Length, (byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(span)), span.Length, out start);
				readOnlySpan = readOnlySpan.Slice(start);
				span = span.Slice(bytesFast);
				if (!readOnlySpan.IsEmpty)
				{
					encoder._charsUsed = originalCharCount;
					return this.GetBytesWithFallback(readOnlySpan, originalCharCount, span, originalByteCount, encoder);
				}
			}
			encoder._charsUsed = originalCharCount - readOnlySpan.Length;
			return originalByteCount - span.Length;
		}

		// Token: 0x06002E86 RID: 11910 RVA: 0x0015CD38 File Offset: 0x0015BF38
		private protected unsafe virtual int GetBytesWithFallback(ReadOnlySpan<char> chars, int originalCharsLength, Span<byte> bytes, int originalBytesLength, EncoderNLS encoder)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* ptr = reference;
				fixed (byte* reference2 = MemoryMarshal.GetReference<byte>(bytes))
				{
					byte* ptr2 = reference2;
					EncoderFallbackBuffer encoderFallbackBuffer = EncoderFallbackBuffer.CreateAndInitialize(this, encoder, originalCharsLength);
					for (;;)
					{
						Rune value;
						int start;
						OperationStatus operationStatus = Rune.DecodeFromUtf16(chars, out value, out start);
						if (operationStatus != OperationStatus.NeedMoreData)
						{
							int num;
							if (operationStatus != OperationStatus.InvalidData && this.EncodeRune(value, bytes, out num) == OperationStatus.DestinationTooSmall)
							{
								goto IL_F3;
							}
						}
						else if (encoder != null && !encoder.MustFlush)
						{
							break;
						}
						int bytesFast;
						bool flag = encoderFallbackBuffer.TryInternalFallbackGetBytes(chars, bytes, out start, out bytesFast);
						chars = chars.Slice(start);
						bytes = bytes.Slice(bytesFast);
						if (!flag)
						{
							goto IL_F3;
						}
						if (!chars.IsEmpty)
						{
							bytesFast = this.GetBytesFast((char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(chars)), chars.Length, (byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(bytes)), bytes.Length, out start);
							chars = chars.Slice(start);
							bytes = bytes.Slice(bytesFast);
						}
						if (chars.IsEmpty)
						{
							goto IL_F3;
						}
					}
					encoder._charLeftOver = (char)(*chars[0]);
					chars = ReadOnlySpan<char>.Empty;
					IL_F3:
					if (!chars.IsEmpty || encoderFallbackBuffer.Remaining > 0)
					{
						this.ThrowBytesOverflow(encoder, bytes.Length == originalBytesLength);
					}
					if (encoder != null)
					{
						encoder._charsUsed = originalCharsLength - chars.Length;
					}
					return originalBytesLength - bytes.Length;
				}
			}
		}

		// Token: 0x06002E87 RID: 11911 RVA: 0x0015CE7C File Offset: 0x0015C07C
		internal unsafe virtual int GetCharCount(byte* pBytes, int byteCount, DecoderNLS decoder)
		{
			int num = 0;
			int num2 = 0;
			if (!decoder.HasLeftoverData)
			{
				num = this.GetCharCountFast(pBytes, byteCount, decoder.Fallback, out num2);
				if (num2 == byteCount)
				{
					return num;
				}
			}
			num += this.GetCharCountWithFallback(pBytes, byteCount, num2, decoder);
			if (num < 0)
			{
				Encoding.ThrowConversionOverflow();
			}
			return num;
		}

		// Token: 0x06002E88 RID: 11912 RVA: 0x000C2700 File Offset: 0x000C1900
		private protected unsafe virtual int GetCharCountFast(byte* pBytes, int bytesLength, DecoderFallback fallback, out int bytesConsumed)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06002E89 RID: 11913 RVA: 0x0015CEC4 File Offset: 0x0015C0C4
		[MethodImpl(MethodImplOptions.NoInlining)]
		private protected unsafe int GetCharCountWithFallback(byte* pBytesOriginal, int originalByteCount, int bytesConsumedSoFar)
		{
			return this.GetCharCountWithFallback(new ReadOnlySpan<byte>((void*)pBytesOriginal, originalByteCount).Slice(bytesConsumedSoFar), originalByteCount, null);
		}

		// Token: 0x06002E8A RID: 11914 RVA: 0x0015CEEC File Offset: 0x0015C0EC
		private unsafe int GetCharCountWithFallback(byte* pOriginalBytes, int originalByteCount, int bytesConsumedSoFar, DecoderNLS decoder)
		{
			ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>((void*)pOriginalBytes, originalByteCount).Slice(bytesConsumedSoFar);
			int num = 0;
			int start;
			if (decoder.HasLeftoverData)
			{
				num = decoder.DrainLeftoverDataForGetCharCount(readOnlySpan, out start);
				readOnlySpan = readOnlySpan.Slice(start);
			}
			num += this.GetCharCountFast((byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(readOnlySpan)), readOnlySpan.Length, decoder.Fallback, out start);
			if (num < 0)
			{
				Encoding.ThrowConversionOverflow();
			}
			readOnlySpan = readOnlySpan.Slice(start);
			if (!readOnlySpan.IsEmpty)
			{
				num += this.GetCharCountWithFallback(readOnlySpan, originalByteCount, decoder);
				if (num < 0)
				{
					Encoding.ThrowConversionOverflow();
				}
			}
			return num;
		}

		// Token: 0x06002E8B RID: 11915 RVA: 0x0015CF80 File Offset: 0x0015C180
		private unsafe int GetCharCountWithFallback(ReadOnlySpan<byte> bytes, int originalBytesLength, DecoderNLS decoder)
		{
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				DecoderFallbackBuffer decoderFallbackBuffer = DecoderFallbackBuffer.CreateAndInitialize(this, decoder, originalBytesLength);
				int num = 0;
				Rune rune;
				int num2;
				while (this.DecodeFirstRune(bytes, out rune, out num2) != OperationStatus.NeedMoreData || decoder == null || decoder.MustFlush)
				{
					int num3 = decoderFallbackBuffer.InternalFallbackGetCharCount(bytes, num2);
					num += num3;
					if (num < 0)
					{
						Encoding.ThrowConversionOverflow();
					}
					bytes = bytes.Slice(num2);
					if (!bytes.IsEmpty)
					{
						num3 = this.GetCharCountFast((byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(bytes)), bytes.Length, null, out num2);
						num += num3;
						if (num < 0)
						{
							Encoding.ThrowConversionOverflow();
						}
						bytes = bytes.Slice(num2);
					}
					if (bytes.IsEmpty)
					{
						break;
					}
				}
				return num;
			}
		}

		// Token: 0x06002E8C RID: 11916 RVA: 0x0015D02C File Offset: 0x0015C22C
		internal unsafe virtual int GetChars(byte* pBytes, int byteCount, char* pChars, int charCount, DecoderNLS decoder)
		{
			int num = 0;
			int num2 = 0;
			if (!decoder.HasLeftoverData)
			{
				num = this.GetCharsFast(pBytes, byteCount, pChars, charCount, out num2);
				if (num2 == byteCount)
				{
					decoder._bytesUsed = byteCount;
					return num;
				}
			}
			return this.GetCharsWithFallback(pBytes, byteCount, pChars, charCount, num2, num, decoder);
		}

		// Token: 0x06002E8D RID: 11917 RVA: 0x000C2700 File Offset: 0x000C1900
		private protected unsafe virtual int GetCharsFast(byte* pBytes, int bytesLength, char* pChars, int charsLength, out int bytesConsumed)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06002E8E RID: 11918 RVA: 0x0015D074 File Offset: 0x0015C274
		[MethodImpl(MethodImplOptions.NoInlining)]
		private protected unsafe int GetCharsWithFallback(byte* pOriginalBytes, int originalByteCount, char* pOriginalChars, int originalCharCount, int bytesConsumedSoFar, int charsWrittenSoFar)
		{
			return this.GetCharsWithFallback(new ReadOnlySpan<byte>((void*)pOriginalBytes, originalByteCount).Slice(bytesConsumedSoFar), originalByteCount, new Span<char>((void*)pOriginalChars, originalCharCount).Slice(charsWrittenSoFar), originalCharCount, null);
		}

		// Token: 0x06002E8F RID: 11919 RVA: 0x0015D0B0 File Offset: 0x0015C2B0
		private protected unsafe int GetCharsWithFallback(byte* pOriginalBytes, int originalByteCount, char* pOriginalChars, int originalCharCount, int bytesConsumedSoFar, int charsWrittenSoFar, DecoderNLS decoder)
		{
			ReadOnlySpan<byte> readOnlySpan = new ReadOnlySpan<byte>((void*)pOriginalBytes, originalByteCount).Slice(bytesConsumedSoFar);
			Span<char> span = new Span<char>((void*)pOriginalChars, originalCharCount).Slice(charsWrittenSoFar);
			int start2;
			int start;
			if (decoder.HasLeftoverData)
			{
				start = decoder.DrainLeftoverDataForGetChars(readOnlySpan, span, out start2);
				readOnlySpan = readOnlySpan.Slice(start2);
				span = span.Slice(start);
			}
			start = this.GetCharsFast((byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(readOnlySpan)), readOnlySpan.Length, (char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(span)), span.Length, out start2);
			readOnlySpan = readOnlySpan.Slice(start2);
			span = span.Slice(start);
			decoder._bytesUsed = originalByteCount;
			if (readOnlySpan.IsEmpty)
			{
				return originalCharCount - span.Length;
			}
			return this.GetCharsWithFallback(readOnlySpan, originalByteCount, span, originalCharCount, decoder);
		}

		// Token: 0x06002E90 RID: 11920 RVA: 0x0015D174 File Offset: 0x0015C374
		private protected unsafe virtual int GetCharsWithFallback(ReadOnlySpan<byte> bytes, int originalBytesLength, Span<char> chars, int originalCharsLength, DecoderNLS decoder)
		{
			fixed (byte* reference = MemoryMarshal.GetReference<byte>(bytes))
			{
				byte* ptr = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(chars))
				{
					char* ptr2 = reference2;
					DecoderFallbackBuffer decoderFallbackBuffer = DecoderFallbackBuffer.CreateAndInitialize(this, decoder, originalBytesLength);
					do
					{
						Rune rune;
						int num;
						OperationStatus operationStatus = this.DecodeFirstRune(bytes, out rune, out num);
						if (operationStatus != OperationStatus.NeedMoreData)
						{
							if (operationStatus != OperationStatus.InvalidData)
							{
								break;
							}
						}
						else if (decoder != null && !decoder.MustFlush)
						{
							goto Block_4;
						}
						int charsFast;
						if (!decoderFallbackBuffer.TryInternalFallbackGetChars(bytes, num, chars, out charsFast))
						{
							break;
						}
						bytes = bytes.Slice(num);
						chars = chars.Slice(charsFast);
						if (!bytes.IsEmpty)
						{
							charsFast = this.GetCharsFast((byte*)Unsafe.AsPointer<byte>(MemoryMarshal.GetReference<byte>(bytes)), bytes.Length, (char*)Unsafe.AsPointer<char>(MemoryMarshal.GetReference<char>(chars)), chars.Length, out num);
							bytes = bytes.Slice(num);
							chars = chars.Slice(charsFast);
						}
					}
					while (!bytes.IsEmpty);
					goto IL_D7;
					Block_4:
					decoder.SetLeftoverData(bytes);
					bytes = ReadOnlySpan<byte>.Empty;
					IL_D7:
					if (!bytes.IsEmpty)
					{
						this.ThrowCharsOverflow(decoder, chars.Length == originalCharsLength);
					}
					if (decoder != null)
					{
						decoder._bytesUsed = originalBytesLength - bytes.Length;
					}
					return originalCharsLength - chars.Length;
				}
			}
		}

		// Token: 0x04000CBF RID: 3263
		private static readonly UTF8Encoding.UTF8EncodingSealed s_defaultEncoding = new UTF8Encoding.UTF8EncodingSealed(false);

		// Token: 0x04000CC0 RID: 3264
		internal int _codePage;

		// Token: 0x04000CC1 RID: 3265
		internal CodePageDataItem _dataItem;

		// Token: 0x04000CC2 RID: 3266
		[OptionalField(VersionAdded = 2)]
		private bool _isReadOnly;

		// Token: 0x04000CC3 RID: 3267
		internal EncoderFallback encoderFallback;

		// Token: 0x04000CC4 RID: 3268
		internal DecoderFallback decoderFallback;

		// Token: 0x02000371 RID: 881
		internal sealed class DefaultEncoder : Encoder, IObjectReference
		{
			// Token: 0x06002E92 RID: 11922 RVA: 0x0015D29F File Offset: 0x0015C49F
			public DefaultEncoder(Encoding encoding)
			{
				this._encoding = encoding;
			}

			// Token: 0x06002E93 RID: 11923 RVA: 0x000B3617 File Offset: 0x000B2817
			public object GetRealObject(StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06002E94 RID: 11924 RVA: 0x0015D2AE File Offset: 0x0015C4AE
			public override int GetByteCount(char[] chars, int index, int count, bool flush)
			{
				return this._encoding.GetByteCount(chars, index, count);
			}

			// Token: 0x06002E95 RID: 11925 RVA: 0x0015D2BE File Offset: 0x0015C4BE
			public unsafe override int GetByteCount(char* chars, int count, bool flush)
			{
				return this._encoding.GetByteCount(chars, count);
			}

			// Token: 0x06002E96 RID: 11926 RVA: 0x0015D2CD File Offset: 0x0015C4CD
			public override int GetBytes(char[] chars, int charIndex, int charCount, byte[] bytes, int byteIndex, bool flush)
			{
				return this._encoding.GetBytes(chars, charIndex, charCount, bytes, byteIndex);
			}

			// Token: 0x06002E97 RID: 11927 RVA: 0x0015D2E1 File Offset: 0x0015C4E1
			public unsafe override int GetBytes(char* chars, int charCount, byte* bytes, int byteCount, bool flush)
			{
				return this._encoding.GetBytes(chars, charCount, bytes, byteCount);
			}

			// Token: 0x04000CC5 RID: 3269
			private readonly Encoding _encoding;
		}

		// Token: 0x02000372 RID: 882
		internal sealed class DefaultDecoder : Decoder, IObjectReference
		{
			// Token: 0x06002E98 RID: 11928 RVA: 0x0015D2F3 File Offset: 0x0015C4F3
			public DefaultDecoder(Encoding encoding)
			{
				this._encoding = encoding;
			}

			// Token: 0x06002E99 RID: 11929 RVA: 0x000B3617 File Offset: 0x000B2817
			public object GetRealObject(StreamingContext context)
			{
				throw new PlatformNotSupportedException();
			}

			// Token: 0x06002E9A RID: 11930 RVA: 0x0015A025 File Offset: 0x00159225
			public override int GetCharCount(byte[] bytes, int index, int count)
			{
				return this.GetCharCount(bytes, index, count, false);
			}

			// Token: 0x06002E9B RID: 11931 RVA: 0x0015D302 File Offset: 0x0015C502
			public override int GetCharCount(byte[] bytes, int index, int count, bool flush)
			{
				return this._encoding.GetCharCount(bytes, index, count);
			}

			// Token: 0x06002E9C RID: 11932 RVA: 0x0015D312 File Offset: 0x0015C512
			public unsafe override int GetCharCount(byte* bytes, int count, bool flush)
			{
				return this._encoding.GetCharCount(bytes, count);
			}

			// Token: 0x06002E9D RID: 11933 RVA: 0x0015A0FE File Offset: 0x001592FE
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex)
			{
				return this.GetChars(bytes, byteIndex, byteCount, chars, charIndex, false);
			}

			// Token: 0x06002E9E RID: 11934 RVA: 0x0015D321 File Offset: 0x0015C521
			public override int GetChars(byte[] bytes, int byteIndex, int byteCount, char[] chars, int charIndex, bool flush)
			{
				return this._encoding.GetChars(bytes, byteIndex, byteCount, chars, charIndex);
			}

			// Token: 0x06002E9F RID: 11935 RVA: 0x0015D335 File Offset: 0x0015C535
			public unsafe override int GetChars(byte* bytes, int byteCount, char* chars, int charCount, bool flush)
			{
				return this._encoding.GetChars(bytes, byteCount, chars, charCount);
			}

			// Token: 0x04000CC6 RID: 3270
			private readonly Encoding _encoding;
		}

		// Token: 0x02000373 RID: 883
		internal class EncodingCharBuffer
		{
			// Token: 0x06002EA0 RID: 11936 RVA: 0x0015D348 File Offset: 0x0015C548
			internal unsafe EncodingCharBuffer(Encoding enc, DecoderNLS decoder, char* charStart, int charCount, byte* byteStart, int byteCount)
			{
				this._enc = enc;
				this._decoder = decoder;
				this._chars = charStart;
				this._charStart = charStart;
				this._charEnd = charStart + charCount;
				this._byteStart = byteStart;
				this._bytes = byteStart;
				this._byteEnd = byteStart + byteCount;
				if (this._decoder == null)
				{
					this._fallbackBuffer = enc.DecoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this._fallbackBuffer = this._decoder.FallbackBuffer;
				}
				this._fallbackBuffer.InternalInitialize(this._bytes, this._charEnd);
			}

			// Token: 0x06002EA1 RID: 11937 RVA: 0x0015D3E4 File Offset: 0x0015C5E4
			internal unsafe bool AddChar(char ch, int numBytes)
			{
				if (this._chars != null)
				{
					if (this._chars >= this._charEnd)
					{
						this._bytes -= numBytes;
						this._enc.ThrowCharsOverflow(this._decoder, this._bytes == this._byteStart);
						return false;
					}
					char* chars = this._chars;
					this._chars = chars + 1;
					*chars = ch;
				}
				this._charCountResult++;
				return true;
			}

			// Token: 0x06002EA2 RID: 11938 RVA: 0x0015D45D File Offset: 0x0015C65D
			internal bool AddChar(char ch)
			{
				return this.AddChar(ch, 1);
			}

			// Token: 0x06002EA3 RID: 11939 RVA: 0x0015D467 File Offset: 0x0015C667
			internal void AdjustBytes(int count)
			{
				this._bytes += count;
			}

			// Token: 0x17000950 RID: 2384
			// (get) Token: 0x06002EA4 RID: 11940 RVA: 0x0015D477 File Offset: 0x0015C677
			internal bool MoreData
			{
				get
				{
					return this._bytes < this._byteEnd;
				}
			}

			// Token: 0x06002EA5 RID: 11941 RVA: 0x0015D488 File Offset: 0x0015C688
			internal unsafe byte GetNextByte()
			{
				if (this._bytes >= this._byteEnd)
				{
					return 0;
				}
				byte* bytes = this._bytes;
				this._bytes = bytes + 1;
				return *bytes;
			}

			// Token: 0x17000951 RID: 2385
			// (get) Token: 0x06002EA6 RID: 11942 RVA: 0x0015D4B7 File Offset: 0x0015C6B7
			internal int BytesUsed
			{
				get
				{
					return (int)((long)(this._bytes - this._byteStart));
				}
			}

			// Token: 0x06002EA7 RID: 11943 RVA: 0x0015D4CC File Offset: 0x0015C6CC
			internal bool Fallback(byte fallbackByte)
			{
				byte[] byteBuffer = new byte[]
				{
					fallbackByte
				};
				return this.Fallback(byteBuffer);
			}

			// Token: 0x06002EA8 RID: 11944 RVA: 0x0015D4EC File Offset: 0x0015C6EC
			internal unsafe bool Fallback(byte[] byteBuffer)
			{
				if (this._chars != null)
				{
					char* chars = this._chars;
					if (!this._fallbackBuffer.InternalFallback(byteBuffer, this._bytes, ref this._chars))
					{
						this._bytes -= byteBuffer.Length;
						this._fallbackBuffer.InternalReset();
						this._enc.ThrowCharsOverflow(this._decoder, this._chars == this._charStart);
						return false;
					}
					this._charCountResult += (int)((long)(this._chars - chars));
				}
				else
				{
					this._charCountResult += this._fallbackBuffer.InternalFallback(byteBuffer, this._bytes);
				}
				return true;
			}

			// Token: 0x17000952 RID: 2386
			// (get) Token: 0x06002EA9 RID: 11945 RVA: 0x0015D59B File Offset: 0x0015C79B
			internal int Count
			{
				get
				{
					return this._charCountResult;
				}
			}

			// Token: 0x04000CC7 RID: 3271
			private unsafe char* _chars;

			// Token: 0x04000CC8 RID: 3272
			private unsafe readonly char* _charStart;

			// Token: 0x04000CC9 RID: 3273
			private unsafe readonly char* _charEnd;

			// Token: 0x04000CCA RID: 3274
			private int _charCountResult;

			// Token: 0x04000CCB RID: 3275
			private readonly Encoding _enc;

			// Token: 0x04000CCC RID: 3276
			private readonly DecoderNLS _decoder;

			// Token: 0x04000CCD RID: 3277
			private unsafe readonly byte* _byteStart;

			// Token: 0x04000CCE RID: 3278
			private unsafe readonly byte* _byteEnd;

			// Token: 0x04000CCF RID: 3279
			private unsafe byte* _bytes;

			// Token: 0x04000CD0 RID: 3280
			private readonly DecoderFallbackBuffer _fallbackBuffer;
		}

		// Token: 0x02000374 RID: 884
		internal class EncodingByteBuffer
		{
			// Token: 0x06002EAA RID: 11946 RVA: 0x0015D5A4 File Offset: 0x0015C7A4
			internal unsafe EncodingByteBuffer(Encoding inEncoding, EncoderNLS inEncoder, byte* inByteStart, int inByteCount, char* inCharStart, int inCharCount)
			{
				this._enc = inEncoding;
				this._encoder = inEncoder;
				this._charStart = inCharStart;
				this._chars = inCharStart;
				this._charEnd = inCharStart + inCharCount;
				this._bytes = inByteStart;
				this._byteStart = inByteStart;
				this._byteEnd = inByteStart + inByteCount;
				if (this._encoder == null)
				{
					this.fallbackBuffer = this._enc.EncoderFallback.CreateFallbackBuffer();
				}
				else
				{
					this.fallbackBuffer = this._encoder.FallbackBuffer;
					if (this._encoder._throwOnOverflow && this._encoder.InternalHasFallbackBuffer && this.fallbackBuffer.Remaining > 0)
					{
						throw new ArgumentException(SR.Format(SR.Argument_EncoderFallbackNotEmpty, this._encoder.Encoding.EncodingName, this._encoder.Fallback.GetType()));
					}
				}
				this.fallbackBuffer.InternalInitialize(this._chars, this._charEnd, this._encoder, this._bytes != null);
			}

			// Token: 0x06002EAB RID: 11947 RVA: 0x0015D6B0 File Offset: 0x0015C8B0
			internal unsafe bool AddByte(byte b, int moreBytesExpected)
			{
				if (this._bytes != null)
				{
					if (this._bytes >= this._byteEnd - moreBytesExpected)
					{
						this.MovePrevious(true);
						return false;
					}
					byte* bytes = this._bytes;
					this._bytes = bytes + 1;
					*bytes = b;
				}
				this._byteCountResult++;
				return true;
			}

			// Token: 0x06002EAC RID: 11948 RVA: 0x0015D702 File Offset: 0x0015C902
			internal bool AddByte(byte b1)
			{
				return this.AddByte(b1, 0);
			}

			// Token: 0x06002EAD RID: 11949 RVA: 0x0015D70C File Offset: 0x0015C90C
			internal bool AddByte(byte b1, byte b2)
			{
				return this.AddByte(b1, b2, 0);
			}

			// Token: 0x06002EAE RID: 11950 RVA: 0x0015D717 File Offset: 0x0015C917
			internal bool AddByte(byte b1, byte b2, int moreBytesExpected)
			{
				return this.AddByte(b1, 1 + moreBytesExpected) && this.AddByte(b2, moreBytesExpected);
			}

			// Token: 0x06002EAF RID: 11951 RVA: 0x0015D730 File Offset: 0x0015C930
			internal void MovePrevious(bool bThrow)
			{
				if (this.fallbackBuffer.bFallingBack)
				{
					this.fallbackBuffer.MovePrevious();
				}
				else if (this._chars != this._charStart)
				{
					this._chars--;
				}
				if (bThrow)
				{
					this._enc.ThrowBytesOverflow(this._encoder, this._bytes == this._byteStart);
				}
			}

			// Token: 0x17000953 RID: 2387
			// (get) Token: 0x06002EB0 RID: 11952 RVA: 0x0015D796 File Offset: 0x0015C996
			internal bool MoreData
			{
				get
				{
					return this.fallbackBuffer.Remaining > 0 || this._chars < this._charEnd;
				}
			}

			// Token: 0x06002EB1 RID: 11953 RVA: 0x0015D7B8 File Offset: 0x0015C9B8
			internal unsafe char GetNextChar()
			{
				char c = this.fallbackBuffer.InternalGetNextChar();
				if (c == '\0' && this._chars < this._charEnd)
				{
					char* chars = this._chars;
					this._chars = chars + 1;
					c = *chars;
				}
				return c;
			}

			// Token: 0x17000954 RID: 2388
			// (get) Token: 0x06002EB2 RID: 11954 RVA: 0x0015D7F6 File Offset: 0x0015C9F6
			internal int CharsUsed
			{
				get
				{
					return (int)((long)(this._chars - this._charStart));
				}
			}

			// Token: 0x17000955 RID: 2389
			// (get) Token: 0x06002EB3 RID: 11955 RVA: 0x0015D809 File Offset: 0x0015CA09
			internal int Count
			{
				get
				{
					return this._byteCountResult;
				}
			}

			// Token: 0x04000CD1 RID: 3281
			private unsafe byte* _bytes;

			// Token: 0x04000CD2 RID: 3282
			private unsafe readonly byte* _byteStart;

			// Token: 0x04000CD3 RID: 3283
			private unsafe readonly byte* _byteEnd;

			// Token: 0x04000CD4 RID: 3284
			private unsafe char* _chars;

			// Token: 0x04000CD5 RID: 3285
			private unsafe readonly char* _charStart;

			// Token: 0x04000CD6 RID: 3286
			private unsafe readonly char* _charEnd;

			// Token: 0x04000CD7 RID: 3287
			private int _byteCountResult;

			// Token: 0x04000CD8 RID: 3288
			private readonly Encoding _enc;

			// Token: 0x04000CD9 RID: 3289
			private readonly EncoderNLS _encoder;

			// Token: 0x04000CDA RID: 3290
			internal EncoderFallbackBuffer fallbackBuffer;
		}
	}
}
