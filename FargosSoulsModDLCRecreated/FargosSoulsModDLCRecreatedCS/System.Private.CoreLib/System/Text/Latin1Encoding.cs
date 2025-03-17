using System;
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace System.Text
{
	// Token: 0x02000378 RID: 888
	internal class Latin1Encoding : Encoding
	{
		// Token: 0x06002ED2 RID: 11986 RVA: 0x0015E0A3 File Offset: 0x0015D2A3
		public Latin1Encoding() : base(28591)
		{
		}

		// Token: 0x1700095A RID: 2394
		// (get) Token: 0x06002ED3 RID: 11987 RVA: 0x0015E0B0 File Offset: 0x0015D2B0
		public override ReadOnlySpan<byte> Preamble
		{
			get
			{
				return default(ReadOnlySpan<byte>);
			}
		}

		// Token: 0x06002ED4 RID: 11988 RVA: 0x0015E0C6 File Offset: 0x0015D2C6
		internal override void SetDefaultFallbacks()
		{
			this.encoderFallback = EncoderLatin1BestFitFallback.SingletonInstance;
			this.decoderFallback = DecoderFallback.ReplacementFallback;
		}

		// Token: 0x06002ED5 RID: 11989 RVA: 0x0015E0DE File Offset: 0x0015D2DE
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

		// Token: 0x06002ED6 RID: 11990 RVA: 0x0015E104 File Offset: 0x0015D304
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

		// Token: 0x06002ED7 RID: 11991 RVA: 0x0015E16C File Offset: 0x0015D36C
		public unsafe override int GetByteCount(ReadOnlySpan<char> chars)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(chars))
			{
				char* pChars = reference;
				return this.GetByteCountCommon(pChars, chars.Length);
			}
		}

		// Token: 0x06002ED8 RID: 11992 RVA: 0x0015E194 File Offset: 0x0015D394
		public unsafe override int GetByteCount(string s)
		{
			if (s == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.s);
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
			return this.GetByteCountCommon(pChars, s.Length);
		}

		// Token: 0x06002ED9 RID: 11993 RVA: 0x0015E1CC File Offset: 0x0015D3CC
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

		// Token: 0x06002EDA RID: 11994 RVA: 0x0015E200 File Offset: 0x0015D400
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetByteCountFast(char* pChars, int charsLength, EncoderFallback fallback, out int charsConsumed)
		{
			int num = charsLength;
			if (!Latin1Encoding.FallbackSupportsFastGetByteCount(fallback))
			{
				num = (int)Latin1Utility.GetIndexOfFirstNonLatin1Char(pChars, (ulong)charsLength);
			}
			charsConsumed = num;
			return num;
		}

		// Token: 0x06002EDB RID: 11995 RVA: 0x0015826C File Offset: 0x0015746C
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

		// Token: 0x06002EDC RID: 11996 RVA: 0x0015E226 File Offset: 0x0015D426
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

		// Token: 0x06002EDD RID: 11997 RVA: 0x0015E268 File Offset: 0x0015D468
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

		// Token: 0x06002EDE RID: 11998 RVA: 0x0015E310 File Offset: 0x0015D510
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

		// Token: 0x06002EDF RID: 11999 RVA: 0x0015E348 File Offset: 0x0015D548
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

		// Token: 0x06002EE0 RID: 12000 RVA: 0x00157CAC File Offset: 0x00156EAC
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

		// Token: 0x06002EE1 RID: 12001 RVA: 0x0015E3EC File Offset: 0x0015D5EC
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private protected unsafe sealed override int GetBytesFast(char* pChars, int charsLength, byte* pBytes, int bytesLength, out int charsConsumed)
		{
			int num = (int)Latin1Utility.NarrowUtf16ToLatin1(pChars, pBytes, (ulong)Math.Min(charsLength, bytesLength));
			charsConsumed = num;
			return num;
		}

		// Token: 0x06002EE2 RID: 12002 RVA: 0x0015E410 File Offset: 0x0015D610
		public unsafe override int GetCharCount(byte* bytes, int count)
		{
			if (bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.bytes);
			}
			if (count < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.count, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			return count;
		}

		// Token: 0x06002EE3 RID: 12003 RVA: 0x0015E42C File Offset: 0x0015D62C
		public override int GetCharCount(byte[] bytes)
		{
			if (bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.bytes);
			}
			return bytes.Length;
		}

		// Token: 0x06002EE4 RID: 12004 RVA: 0x0015E43B File Offset: 0x0015D63B
		public override int GetCharCount(byte[] bytes, int index, int count)
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
			return count;
		}

		// Token: 0x06002EE5 RID: 12005 RVA: 0x0015E471 File Offset: 0x0015D671
		public override int GetCharCount(ReadOnlySpan<byte> bytes)
		{
			return bytes.Length;
		}

		// Token: 0x06002EE6 RID: 12006 RVA: 0x0015E47A File Offset: 0x0015D67A
		private protected unsafe override int GetCharCountFast(byte* pBytes, int bytesLength, DecoderFallback fallback, out int bytesConsumed)
		{
			bytesConsumed = bytesLength;
			return bytesLength;
		}

		// Token: 0x06002EE7 RID: 12007 RVA: 0x0015E481 File Offset: 0x0015D681
		public override int GetMaxCharCount(int byteCount)
		{
			if (byteCount < 0)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.byteCount, ExceptionResource.ArgumentOutOfRange_NeedNonNegNum);
			}
			return byteCount;
		}

		// Token: 0x06002EE8 RID: 12008 RVA: 0x0015E491 File Offset: 0x0015D691
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

		// Token: 0x06002EE9 RID: 12009 RVA: 0x0015E4CC File Offset: 0x0015D6CC
		public unsafe override char[] GetChars(byte[] bytes)
		{
			if (bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.bytes);
			}
			if (bytes.Length == 0)
			{
				return Array.Empty<char>();
			}
			char[] array = new char[bytes.Length];
			fixed (byte[] array2 = bytes)
			{
				byte* pBytes;
				if (bytes == null || array2.Length == 0)
				{
					pBytes = null;
				}
				else
				{
					pBytes = &array2[0];
				}
				char[] array3;
				char* pChars;
				if ((array3 = array) == null || array3.Length == 0)
				{
					pChars = null;
				}
				else
				{
					pChars = &array3[0];
				}
				this.GetCharsCommon(pBytes, bytes.Length, pChars, array.Length);
				array3 = null;
			}
			return array;
		}

		// Token: 0x06002EEA RID: 12010 RVA: 0x0015E540 File Offset: 0x0015D740
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

		// Token: 0x06002EEB RID: 12011 RVA: 0x0015E5E4 File Offset: 0x0015D7E4
		public unsafe override char[] GetChars(byte[] bytes, int index, int count)
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
			char[] array = new char[count];
			fixed (byte[] array2 = bytes)
			{
				byte* ptr;
				if (bytes == null || array2.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array2[0];
				}
				char[] array3;
				char* pChars;
				if ((array3 = array) == null || array3.Length == 0)
				{
					pChars = null;
				}
				else
				{
					pChars = &array3[0];
				}
				this.GetCharsCommon(ptr + index, count, pChars, array.Length);
				array3 = null;
			}
			return array;
		}

		// Token: 0x06002EEC RID: 12012 RVA: 0x0015E674 File Offset: 0x0015D874
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

		// Token: 0x06002EED RID: 12013 RVA: 0x0015E6AB File Offset: 0x0015D8AB
		public unsafe override string GetString(byte[] bytes)
		{
			if (bytes == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.bytes);
			}
			return string.Create<ValueTuple<Latin1Encoding, byte[]>>(bytes.Length, new ValueTuple<Latin1Encoding, byte[]>(this, bytes), delegate(Span<char> chars, [TupleElementNames(new string[]
			{
				"encoding",
				"bytes"
			})] ValueTuple<Latin1Encoding, byte[]> args)
			{
				byte[] array;
				byte* pBytes;
				if ((array = args.Item2) == null || array.Length == 0)
				{
					pBytes = null;
				}
				else
				{
					pBytes = &array[0];
				}
				fixed (char* pinnableReference = chars.GetPinnableReference())
				{
					char* pChars = pinnableReference;
					args.Item1.GetCharsCommon(pBytes, args.Item2.Length, pChars, chars.Length);
				}
				array = null;
			});
		}

		// Token: 0x06002EEE RID: 12014 RVA: 0x0015E6E8 File Offset: 0x0015D8E8
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
			return string.Create<ValueTuple<Latin1Encoding, byte[], int>>(count, new ValueTuple<Latin1Encoding, byte[], int>(this, bytes, index), delegate(Span<char> chars, [TupleElementNames(new string[]
			{
				"encoding",
				"bytes",
				"index"
			})] ValueTuple<Latin1Encoding, byte[], int> args)
			{
				byte[] array;
				byte* ptr;
				if ((array = args.Item2) == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				fixed (char* pinnableReference = chars.GetPinnableReference())
				{
					char* pChars = pinnableReference;
					args.Item1.GetCharsCommon(ptr + args.Item3, chars.Length, pChars, chars.Length);
				}
				array = null;
			});
		}

		// Token: 0x06002EEF RID: 12015 RVA: 0x0015E755 File Offset: 0x0015D955
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe int GetCharsCommon(byte* pBytes, int byteCount, char* pChars, int charCount)
		{
			if (byteCount > charCount)
			{
				base.ThrowCharsOverflow();
			}
			Latin1Utility.WidenLatin1ToUtf16(pBytes, pChars, (ulong)byteCount);
			return byteCount;
		}

		// Token: 0x06002EF0 RID: 12016 RVA: 0x0015E76C File Offset: 0x0015D96C
		private protected unsafe sealed override int GetCharsFast(byte* pBytes, int bytesLength, char* pChars, int charsLength, out int bytesConsumed)
		{
			int num = Math.Min(bytesLength, charsLength);
			Latin1Utility.WidenLatin1ToUtf16(pBytes, pChars, (ulong)num);
			bytesConsumed = num;
			return num;
		}

		// Token: 0x06002EF1 RID: 12017 RVA: 0x00158328 File Offset: 0x00157528
		public override Decoder GetDecoder()
		{
			return new DecoderNLS(this);
		}

		// Token: 0x06002EF2 RID: 12018 RVA: 0x00158330 File Offset: 0x00157530
		public override Encoder GetEncoder()
		{
			return new EncoderNLS(this);
		}

		// Token: 0x06002EF3 RID: 12019 RVA: 0x0015E790 File Offset: 0x0015D990
		internal sealed override bool TryGetByteCount(Rune value, out int byteCount)
		{
			if (value.Value <= 255)
			{
				byteCount = 1;
				return true;
			}
			byteCount = 0;
			return false;
		}

		// Token: 0x06002EF4 RID: 12020 RVA: 0x0015E7A9 File Offset: 0x0015D9A9
		internal unsafe sealed override OperationStatus EncodeRune(Rune value, Span<byte> bytes, out int bytesWritten)
		{
			if (value.Value > 255)
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

		// Token: 0x06002EF5 RID: 12021 RVA: 0x0015E7E4 File Offset: 0x0015D9E4
		internal unsafe sealed override OperationStatus DecodeFirstRune(ReadOnlySpan<byte> bytes, out Rune value, out int bytesConsumed)
		{
			if (bytes.IsEmpty)
			{
				value = Rune.ReplacementChar;
				bytesConsumed = 0;
				return OperationStatus.NeedMoreData;
			}
			byte b = *bytes[0];
			if (b <= 255)
			{
				value = new Rune((int)b);
				bytesConsumed = 1;
				return OperationStatus.Done;
			}
			value = Rune.ReplacementChar;
			bytesConsumed = 1;
			return OperationStatus.InvalidData;
		}

		// Token: 0x1700095B RID: 2395
		// (get) Token: 0x06002EF6 RID: 12022 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsSingleByte
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002EF7 RID: 12023 RVA: 0x0015E83C File Offset: 0x0015DA3C
		public override bool IsAlwaysNormalized(NormalizationForm form)
		{
			return form == NormalizationForm.FormC;
		}

		// Token: 0x06002EF8 RID: 12024 RVA: 0x0015E844 File Offset: 0x0015DA44
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static bool FallbackSupportsFastGetByteCount(EncoderFallback fallback)
		{
			if (fallback == null)
			{
				return false;
			}
			if (fallback is EncoderLatin1BestFitFallback)
			{
				return true;
			}
			EncoderReplacementFallback encoderReplacementFallback = fallback as EncoderReplacementFallback;
			return encoderReplacementFallback != null && encoderReplacementFallback.MaxCharCount == 1 && encoderReplacementFallback.DefaultString[0] <= 'ÿ';
		}

		// Token: 0x04000CEA RID: 3306
		internal static readonly Latin1Encoding.Latin1EncodingSealed s_default = new Latin1Encoding.Latin1EncodingSealed();

		// Token: 0x02000379 RID: 889
		internal sealed class Latin1EncodingSealed : Latin1Encoding
		{
			// Token: 0x06002EFA RID: 12026 RVA: 0x0015E895 File Offset: 0x0015DA95
			public override object Clone()
			{
				return new Latin1Encoding
				{
					IsReadOnly = false
				};
			}
		}
	}
}
