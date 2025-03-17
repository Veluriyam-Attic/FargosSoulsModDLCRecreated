using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Unicode;
using Internal.Runtime.CompilerServices;

namespace System.Globalization
{
	// Token: 0x02000228 RID: 552
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class TextInfo : ICloneable, IDeserializationCallback
	{
		// Token: 0x0600230F RID: 8975 RVA: 0x00134510 File Offset: 0x00133710
		internal TextInfo(CultureData cultureData)
		{
			this._cultureData = cultureData;
			this._cultureName = this._cultureData.CultureName;
			this._textInfoName = this._cultureData.TextInfoName;
			if (GlobalizationMode.UseNls)
			{
				this._sortHandle = CompareInfo.NlsGetSortHandle(this._textInfoName);
			}
		}

		// Token: 0x06002310 RID: 8976 RVA: 0x00134564 File Offset: 0x00133764
		private TextInfo(CultureData cultureData, bool readOnly) : this(cultureData)
		{
			this.SetReadOnlyState(readOnly);
		}

		// Token: 0x06002311 RID: 8977 RVA: 0x000B3617 File Offset: 0x000B2817
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x170007DD RID: 2013
		// (get) Token: 0x06002312 RID: 8978 RVA: 0x00134574 File Offset: 0x00133774
		public int ANSICodePage
		{
			get
			{
				return this._cultureData.ANSICodePage;
			}
		}

		// Token: 0x170007DE RID: 2014
		// (get) Token: 0x06002313 RID: 8979 RVA: 0x00134581 File Offset: 0x00133781
		public int OEMCodePage
		{
			get
			{
				return this._cultureData.OEMCodePage;
			}
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002314 RID: 8980 RVA: 0x0013458E File Offset: 0x0013378E
		public int MacCodePage
		{
			get
			{
				return this._cultureData.MacCodePage;
			}
		}

		// Token: 0x170007E0 RID: 2016
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0013459B File Offset: 0x0013379B
		public int EBCDICCodePage
		{
			get
			{
				return this._cultureData.EBCDICCodePage;
			}
		}

		// Token: 0x170007E1 RID: 2017
		// (get) Token: 0x06002316 RID: 8982 RVA: 0x001345A8 File Offset: 0x001337A8
		public int LCID
		{
			get
			{
				return CultureInfo.GetCultureInfo(this._textInfoName).LCID;
			}
		}

		// Token: 0x170007E2 RID: 2018
		// (get) Token: 0x06002317 RID: 8983 RVA: 0x001345BA File Offset: 0x001337BA
		public string CultureName
		{
			get
			{
				return this._textInfoName;
			}
		}

		// Token: 0x170007E3 RID: 2019
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x001345C2 File Offset: 0x001337C2
		public bool IsReadOnly
		{
			get
			{
				return this._isReadOnly;
			}
		}

		// Token: 0x06002319 RID: 8985 RVA: 0x001345CC File Offset: 0x001337CC
		public object Clone()
		{
			object obj = base.MemberwiseClone();
			((TextInfo)obj).SetReadOnlyState(false);
			return obj;
		}

		// Token: 0x0600231A RID: 8986 RVA: 0x001345F0 File Offset: 0x001337F0
		public static TextInfo ReadOnly(TextInfo textInfo)
		{
			if (textInfo == null)
			{
				throw new ArgumentNullException("textInfo");
			}
			if (textInfo.IsReadOnly)
			{
				return textInfo;
			}
			TextInfo textInfo2 = (TextInfo)textInfo.MemberwiseClone();
			textInfo2.SetReadOnlyState(true);
			return textInfo2;
		}

		// Token: 0x0600231B RID: 8987 RVA: 0x00134629 File Offset: 0x00133829
		private void VerifyWritable()
		{
			if (this._isReadOnly)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
			}
		}

		// Token: 0x0600231C RID: 8988 RVA: 0x0013463E File Offset: 0x0013383E
		internal void SetReadOnlyState(bool readOnly)
		{
			this._isReadOnly = readOnly;
		}

		// Token: 0x170007E4 RID: 2020
		// (get) Token: 0x0600231D RID: 8989 RVA: 0x00134648 File Offset: 0x00133848
		// (set) Token: 0x0600231E RID: 8990 RVA: 0x00134673 File Offset: 0x00133873
		public string ListSeparator
		{
			get
			{
				string result;
				if ((result = this._listSeparator) == null)
				{
					result = (this._listSeparator = this._cultureData.ListSeparator);
				}
				return result;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.VerifyWritable();
				this._listSeparator = value;
			}
		}

		// Token: 0x0600231F RID: 8991 RVA: 0x00134690 File Offset: 0x00133890
		public char ToLower(char c)
		{
			if (GlobalizationMode.Invariant || (UnicodeUtility.IsAsciiCodePoint((uint)c) && this.IsAsciiCasingSameAsInvariant))
			{
				return TextInfo.ToLowerAsciiInvariant(c);
			}
			return this.ChangeCase(c, false);
		}

		// Token: 0x06002320 RID: 8992 RVA: 0x001346B8 File Offset: 0x001338B8
		internal static char ToLowerInvariant(char c)
		{
			if (GlobalizationMode.Invariant || UnicodeUtility.IsAsciiCodePoint((uint)c))
			{
				return TextInfo.ToLowerAsciiInvariant(c);
			}
			return TextInfo.Invariant.ChangeCase(c, false);
		}

		// Token: 0x06002321 RID: 8993 RVA: 0x001346DC File Offset: 0x001338DC
		public string ToLower(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (GlobalizationMode.Invariant)
			{
				return TextInfo.ToLowerAsciiInvariant(str);
			}
			return this.ChangeCaseCommon<TextInfo.ToLowerConversion>(str);
		}

		// Token: 0x06002322 RID: 8994 RVA: 0x00134704 File Offset: 0x00133904
		private unsafe char ChangeCase(char c, bool toUpper)
		{
			char result = '\0';
			this.ChangeCaseCore(&c, 1, &result, 1, toUpper);
			return result;
		}

		// Token: 0x06002323 RID: 8995 RVA: 0x00134723 File Offset: 0x00133923
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ChangeCaseToLower(ReadOnlySpan<char> source, Span<char> destination)
		{
			this.ChangeCaseCommon<TextInfo.ToLowerConversion>(MemoryMarshal.GetReference<char>(source), MemoryMarshal.GetReference<char>(destination), source.Length);
		}

		// Token: 0x06002324 RID: 8996 RVA: 0x0013473E File Offset: 0x0013393E
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal void ChangeCaseToUpper(ReadOnlySpan<char> source, Span<char> destination)
		{
			this.ChangeCaseCommon<TextInfo.ToUpperConversion>(MemoryMarshal.GetReference<char>(source), MemoryMarshal.GetReference<char>(destination), source.Length);
		}

		// Token: 0x06002325 RID: 8997 RVA: 0x00134759 File Offset: 0x00133959
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private void ChangeCaseCommon<TConversion>(ReadOnlySpan<char> source, Span<char> destination) where TConversion : struct
		{
			this.ChangeCaseCommon<TConversion>(MemoryMarshal.GetReference<char>(source), MemoryMarshal.GetReference<char>(destination), source.Length);
		}

		// Token: 0x06002326 RID: 8998 RVA: 0x00134774 File Offset: 0x00133974
		private unsafe void ChangeCaseCommon<TConversion>(ref char source, ref char destination, int charCount) where TConversion : struct
		{
			bool flag = typeof(TConversion) == typeof(TextInfo.ToUpperConversion);
			if (charCount != 0)
			{
				try
				{
					fixed (char* ptr = &source)
					{
						char* ptr2 = ptr;
						try
						{
							fixed (char* ptr3 = &destination)
							{
								char* ptr4 = ptr3;
								UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
								if (this.IsAsciiCasingSameAsInvariant)
								{
									if (charCount >= 4)
									{
										UIntPtr uintPtr2 = (UIntPtr)(charCount - 4);
										for (;;)
										{
											uint value = Unsafe.ReadUnaligned<uint>((void*)(ptr2 + (ulong)uintPtr * 2UL / 2UL));
											if (!Utf16Utility.AllCharsInUInt32AreAscii(value))
											{
												goto IL_171;
											}
											value = (flag ? Utf16Utility.ConvertAllAsciiCharsInUInt32ToUppercase(value) : Utf16Utility.ConvertAllAsciiCharsInUInt32ToLowercase(value));
											Unsafe.WriteUnaligned<uint>((void*)(ptr4 + (ulong)uintPtr * 2UL / 2UL), value);
											value = Unsafe.ReadUnaligned<uint>((void*)(ptr2 + (ulong)uintPtr * 2UL / 2UL + 2));
											if (!Utf16Utility.AllCharsInUInt32AreAscii(value))
											{
												break;
											}
											value = (flag ? Utf16Utility.ConvertAllAsciiCharsInUInt32ToUppercase(value) : Utf16Utility.ConvertAllAsciiCharsInUInt32ToLowercase(value));
											Unsafe.WriteUnaligned<uint>((void*)(ptr4 + (ulong)uintPtr * 2UL / 2UL + 2), value);
											uintPtr += (UIntPtr)((IntPtr)4);
											if (uintPtr > uintPtr2)
											{
												goto IL_E5;
											}
										}
										uintPtr += (UIntPtr)((IntPtr)2);
										goto IL_171;
									}
									IL_E5:
									if ((charCount & 2) != 0)
									{
										uint value2 = Unsafe.ReadUnaligned<uint>((void*)(ptr2 + (ulong)uintPtr * 2UL / 2UL));
										if (!Utf16Utility.AllCharsInUInt32AreAscii(value2))
										{
											goto IL_171;
										}
										value2 = (flag ? Utf16Utility.ConvertAllAsciiCharsInUInt32ToUppercase(value2) : Utf16Utility.ConvertAllAsciiCharsInUInt32ToLowercase(value2));
										Unsafe.WriteUnaligned<uint>((void*)(ptr4 + (ulong)uintPtr * 2UL / 2UL), value2);
										uintPtr += (UIntPtr)((IntPtr)2);
									}
									if ((charCount & 1) != 0)
									{
										uint num = (uint)ptr2[(ulong)uintPtr * 2UL / 2UL];
										if (num > 127U)
										{
											goto IL_171;
										}
										num = (flag ? Utf16Utility.ConvertAllAsciiCharsInUInt32ToUppercase(num) : Utf16Utility.ConvertAllAsciiCharsInUInt32ToLowercase(num));
										ptr4[(ulong)uintPtr * 2UL / 2UL] = (char)num;
									}
									return;
									IL_171:
									charCount -= (int)uintPtr;
								}
								this.ChangeCaseCore(ptr2 + (ulong)uintPtr * 2UL / 2UL, charCount, ptr4 + (ulong)uintPtr * 2UL / 2UL, charCount, flag);
							}
						}
						finally
						{
							char* ptr3 = null;
						}
					}
				}
				finally
				{
					char* ptr = null;
				}
			}
		}

		// Token: 0x06002327 RID: 8999 RVA: 0x00134954 File Offset: 0x00133B54
		private unsafe string ChangeCaseCommon<TConversion>(string source) where TConversion : struct
		{
			bool flag = typeof(TConversion) == typeof(TextInfo.ToUpperConversion);
			if (source.Length == 0)
			{
				return string.Empty;
			}
			char* ptr;
			if (source == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = source.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* ptr2 = ptr;
			UIntPtr uintPtr = (UIntPtr)((IntPtr)0);
			if (this.IsAsciiCasingSameAsInvariant)
			{
				if (source.Length >= 2)
				{
					UIntPtr uintPtr2 = (UIntPtr)(source.Length - 2);
					do
					{
						uint value = Unsafe.ReadUnaligned<uint>((void*)(ptr2 + (ulong)uintPtr * 2UL / 2UL));
						if (!Utf16Utility.AllCharsInUInt32AreAscii(value))
						{
							goto IL_121;
						}
						if (flag ? Utf16Utility.UInt32ContainsAnyLowercaseAsciiChar(value) : Utf16Utility.UInt32ContainsAnyUppercaseAsciiChar(value))
						{
							goto IL_D1;
						}
						uintPtr += (UIntPtr)((IntPtr)2);
					}
					while (uintPtr <= uintPtr2);
				}
				if ((source.Length & 1) != 0)
				{
					uint num = (uint)ptr2[(ulong)uintPtr * 2UL / 2UL];
					if (num > 127U)
					{
						goto IL_121;
					}
					if (flag ? (num - 97U <= 25U) : (num - 65U <= 25U))
					{
						goto IL_D1;
					}
				}
				return source;
				IL_D1:
				string text = string.FastAllocateString(source.Length);
				Span<char> destination = new Span<char>(text.GetRawStringData(), text.Length);
				source.AsSpan(0, (int)uintPtr).CopyTo(destination);
				this.ChangeCaseCommon<TConversion>(source.AsSpan((int)uintPtr), destination.Slice((int)uintPtr));
				return text;
			}
			IL_121:
			string text2 = string.FastAllocateString(source.Length);
			if (uintPtr > (UIntPtr)((IntPtr)0))
			{
				Span<char> destination2 = new Span<char>(text2.GetRawStringData(), text2.Length);
				source.AsSpan(0, (int)uintPtr).CopyTo(destination2);
			}
			char* ptr3;
			if (text2 == null)
			{
				ptr3 = null;
			}
			else
			{
				fixed (char* ptr4 = text2.GetPinnableReference())
				{
					ptr3 = ptr4;
				}
			}
			char* ptr5 = ptr3;
			this.ChangeCaseCore(ptr2 + (ulong)uintPtr * 2UL / 2UL, source.Length - (int)uintPtr, ptr5 + (ulong)uintPtr * 2UL / 2UL, text2.Length - (int)uintPtr, flag);
			char* ptr4 = null;
			return text2;
		}

		// Token: 0x06002328 RID: 9000 RVA: 0x00134B04 File Offset: 0x00133D04
		internal unsafe static string ToLowerAsciiInvariant(string s)
		{
			if (s.Length == 0)
			{
				return string.Empty;
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
			int i = 0;
			while (i < s.Length && ptr2[i] - 'A' > '\u0019')
			{
				i++;
			}
			if (i >= s.Length)
			{
				return s;
			}
			string text = string.FastAllocateString(s.Length);
			char* ptr3;
			if (text == null)
			{
				ptr3 = null;
			}
			else
			{
				fixed (char* ptr4 = text.GetPinnableReference())
				{
					ptr3 = ptr4;
				}
			}
			char* ptr5 = ptr3;
			for (int j = 0; j < i; j++)
			{
				ptr5[j] = ptr2[j];
			}
			ptr5[i] = (ptr2[i] | ' ');
			for (i++; i < s.Length; i++)
			{
				ptr5[i] = TextInfo.ToLowerAsciiInvariant(ptr2[i]);
			}
			char* ptr4 = null;
			return text;
		}

		// Token: 0x06002329 RID: 9001 RVA: 0x00134BDC File Offset: 0x00133DDC
		internal unsafe static void ToLowerAsciiInvariant(ReadOnlySpan<char> source, Span<char> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = TextInfo.ToLowerAsciiInvariant((char)(*source[i]));
			}
		}

		// Token: 0x0600232A RID: 9002 RVA: 0x00134C14 File Offset: 0x00133E14
		private unsafe static string ToUpperAsciiInvariant(string s)
		{
			if (s.Length == 0)
			{
				return string.Empty;
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
			int i = 0;
			while (i < s.Length && ptr2[i] - 'a' > '\u0019')
			{
				i++;
			}
			if (i >= s.Length)
			{
				return s;
			}
			string text = string.FastAllocateString(s.Length);
			char* ptr3;
			if (text == null)
			{
				ptr3 = null;
			}
			else
			{
				fixed (char* ptr4 = text.GetPinnableReference())
				{
					ptr3 = ptr4;
				}
			}
			char* ptr5 = ptr3;
			for (int j = 0; j < i; j++)
			{
				ptr5[j] = ptr2[j];
			}
			ptr5[i] = (char)((int)ptr2[i] & -33);
			for (i++; i < s.Length; i++)
			{
				ptr5[i] = TextInfo.ToUpperAsciiInvariant(ptr2[i]);
			}
			char* ptr4 = null;
			return text;
		}

		// Token: 0x0600232B RID: 9003 RVA: 0x00134CEC File Offset: 0x00133EEC
		internal unsafe static void ToUpperAsciiInvariant(ReadOnlySpan<char> source, Span<char> destination)
		{
			for (int i = 0; i < source.Length; i++)
			{
				*destination[i] = TextInfo.ToUpperAsciiInvariant((char)(*source[i]));
			}
		}

		// Token: 0x0600232C RID: 9004 RVA: 0x00134D22 File Offset: 0x00133F22
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static char ToLowerAsciiInvariant(char c)
		{
			if (UnicodeUtility.IsInRangeInclusive((uint)c, 65U, 90U))
			{
				c = (char)((byte)(c | ' '));
			}
			return c;
		}

		// Token: 0x0600232D RID: 9005 RVA: 0x00134D38 File Offset: 0x00133F38
		public char ToUpper(char c)
		{
			if (GlobalizationMode.Invariant || (UnicodeUtility.IsAsciiCodePoint((uint)c) && this.IsAsciiCasingSameAsInvariant))
			{
				return TextInfo.ToUpperAsciiInvariant(c);
			}
			return this.ChangeCase(c, true);
		}

		// Token: 0x0600232E RID: 9006 RVA: 0x00134D60 File Offset: 0x00133F60
		internal static char ToUpperInvariant(char c)
		{
			if (GlobalizationMode.Invariant || UnicodeUtility.IsAsciiCodePoint((uint)c))
			{
				return TextInfo.ToUpperAsciiInvariant(c);
			}
			return TextInfo.Invariant.ChangeCase(c, true);
		}

		// Token: 0x0600232F RID: 9007 RVA: 0x00134D84 File Offset: 0x00133F84
		public string ToUpper(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (GlobalizationMode.Invariant)
			{
				return TextInfo.ToUpperAsciiInvariant(str);
			}
			return this.ChangeCaseCommon<TextInfo.ToUpperConversion>(str);
		}

		// Token: 0x06002330 RID: 9008 RVA: 0x00134DA9 File Offset: 0x00133FA9
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private static char ToUpperAsciiInvariant(char c)
		{
			if (UnicodeUtility.IsInRangeInclusive((uint)c, 97U, 122U))
			{
				c &= '_';
			}
			return c;
		}

		// Token: 0x170007E5 RID: 2021
		// (get) Token: 0x06002331 RID: 9009 RVA: 0x00134DBF File Offset: 0x00133FBF
		private bool IsAsciiCasingSameAsInvariant
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				if (this._isAsciiCasingSameAsInvariant == TextInfo.Tristate.NotInitialized)
				{
					this.PopulateIsAsciiCasingSameAsInvariant();
				}
				return this._isAsciiCasingSameAsInvariant == TextInfo.Tristate.True;
			}
		}

		// Token: 0x06002332 RID: 9010 RVA: 0x00134DD8 File Offset: 0x00133FD8
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void PopulateIsAsciiCasingSameAsInvariant()
		{
			this._isAsciiCasingSameAsInvariant = ((CultureInfo.GetCultureInfo(this._textInfoName).CompareInfo.Compare("abcdefghijklmnopqrstuvwxyz", "ABCDEFGHIJKLMNOPQRSTUVWXYZ", CompareOptions.IgnoreCase) == 0) ? TextInfo.Tristate.True : TextInfo.Tristate.False);
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x06002333 RID: 9011 RVA: 0x00134E16 File Offset: 0x00134016
		public bool IsRightToLeft
		{
			get
			{
				return this._cultureData.IsRightToLeft;
			}
		}

		// Token: 0x06002334 RID: 9012 RVA: 0x00134E24 File Offset: 0x00134024
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			TextInfo textInfo = obj as TextInfo;
			return textInfo != null && this.CultureName.Equals(textInfo.CultureName);
		}

		// Token: 0x06002335 RID: 9013 RVA: 0x00134E4E File Offset: 0x0013404E
		public override int GetHashCode()
		{
			return this.CultureName.GetHashCode();
		}

		// Token: 0x06002336 RID: 9014 RVA: 0x00134E5B File Offset: 0x0013405B
		public override string ToString()
		{
			return "TextInfo - " + this._cultureData.CultureName;
		}

		// Token: 0x06002337 RID: 9015 RVA: 0x00134E74 File Offset: 0x00134074
		public string ToTitleCase(string str)
		{
			if (str == null)
			{
				throw new ArgumentNullException("str");
			}
			if (str.Length == 0)
			{
				return str;
			}
			StringBuilder stringBuilder = new StringBuilder();
			string text = null;
			bool flag = this.CultureName.StartsWith("nl-", StringComparison.OrdinalIgnoreCase);
			for (int i = 0; i < str.Length; i++)
			{
				int num;
				UnicodeCategory unicodeCategoryInternal = CharUnicodeInfo.GetUnicodeCategoryInternal(str, i, out num);
				if (char.CheckLetter(unicodeCategoryInternal))
				{
					if (flag && i < str.Length - 1 && (str[i] == 'i' || str[i] == 'I') && (str[i + 1] == 'j' || str[i + 1] == 'J'))
					{
						stringBuilder.Append("IJ");
						i += 2;
					}
					else
					{
						i = this.AddTitlecaseLetter(ref stringBuilder, ref str, i, num) + 1;
					}
					int num2 = i;
					bool flag2 = unicodeCategoryInternal == UnicodeCategory.LowercaseLetter;
					while (i < str.Length)
					{
						unicodeCategoryInternal = CharUnicodeInfo.GetUnicodeCategoryInternal(str, i, out num);
						if (TextInfo.IsLetterCategory(unicodeCategoryInternal))
						{
							if (unicodeCategoryInternal == UnicodeCategory.LowercaseLetter)
							{
								flag2 = true;
							}
							i += num;
						}
						else if (str[i] == '\'')
						{
							i++;
							if (flag2)
							{
								if (text == null)
								{
									text = this.ToLower(str);
								}
								stringBuilder.Append(text, num2, i - num2);
							}
							else
							{
								stringBuilder.Append(str, num2, i - num2);
							}
							num2 = i;
							flag2 = true;
						}
						else
						{
							if (TextInfo.IsWordSeparator(unicodeCategoryInternal))
							{
								break;
							}
							i += num;
						}
					}
					int num3 = i - num2;
					if (num3 > 0)
					{
						if (flag2)
						{
							if (text == null)
							{
								text = this.ToLower(str);
							}
							stringBuilder.Append(text, num2, num3);
						}
						else
						{
							stringBuilder.Append(str, num2, num3);
						}
					}
					if (i < str.Length)
					{
						i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
					}
				}
				else
				{
					i = TextInfo.AddNonLetter(ref stringBuilder, ref str, i, num);
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002338 RID: 9016 RVA: 0x0013502A File Offset: 0x0013422A
		private static int AddNonLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				result.Append(input[inputIndex++]);
				result.Append(input[inputIndex]);
			}
			else
			{
				result.Append(input[inputIndex]);
			}
			return inputIndex;
		}

		// Token: 0x06002339 RID: 9017 RVA: 0x00135068 File Offset: 0x00134268
		private unsafe int AddTitlecaseLetter(ref StringBuilder result, ref string input, int inputIndex, int charLen)
		{
			if (charLen == 2)
			{
				ReadOnlySpan<char> readOnlySpan = input.AsSpan(inputIndex, 2);
				if (GlobalizationMode.Invariant)
				{
					result.Append(readOnlySpan);
				}
				else
				{
					Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)4], 2);
					Span<char> span2 = span;
					this.ChangeCaseToUpper(readOnlySpan, span2);
					result.Append(span2);
				}
				inputIndex++;
			}
			else
			{
				char c = input[inputIndex];
				switch (c)
				{
				case 'Ǆ':
				case 'ǅ':
				case 'ǆ':
					result.Append('ǅ');
					break;
				case 'Ǉ':
				case 'ǈ':
				case 'ǉ':
					result.Append('ǈ');
					break;
				case 'Ǌ':
				case 'ǋ':
				case 'ǌ':
					result.Append('ǋ');
					break;
				default:
					switch (c)
					{
					case 'Ǳ':
					case 'ǲ':
					case 'ǳ':
						result.Append('ǲ');
						break;
					default:
						result.Append(this.ToUpper(input[inputIndex]));
						break;
					}
					break;
				}
			}
			return inputIndex;
		}

		// Token: 0x0600233A RID: 9018 RVA: 0x00135169 File Offset: 0x00134369
		private unsafe void ChangeCaseCore(char* src, int srcLen, char* dstBuffer, int dstBufferCapacity, bool bToUpper)
		{
			if (GlobalizationMode.UseNls)
			{
				this.NlsChangeCase(src, srcLen, dstBuffer, dstBufferCapacity, bToUpper);
				return;
			}
			this.IcuChangeCase(src, srcLen, dstBuffer, dstBufferCapacity, bToUpper);
		}

		// Token: 0x0600233B RID: 9019 RVA: 0x0013518D File Offset: 0x0013438D
		private static bool IsWordSeparator(UnicodeCategory category)
		{
			return (536672256 & 1 << (int)category) != 0;
		}

		// Token: 0x0600233C RID: 9020 RVA: 0x0013519E File Offset: 0x0013439E
		private static bool IsLetterCategory(UnicodeCategory uc)
		{
			return uc == UnicodeCategory.UppercaseLetter || uc == UnicodeCategory.LowercaseLetter || uc == UnicodeCategory.TitlecaseLetter || uc == UnicodeCategory.ModifierLetter || uc == UnicodeCategory.OtherLetter;
		}

		// Token: 0x0600233D RID: 9021 RVA: 0x001351B5 File Offset: 0x001343B5
		private static bool NeedsTurkishCasing(string localeName)
		{
			return CultureInfo.GetCultureInfo(localeName).CompareInfo.Compare("ı", "I", CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x170007E7 RID: 2023
		// (get) Token: 0x0600233E RID: 9022 RVA: 0x001351D5 File Offset: 0x001343D5
		private bool IsInvariant
		{
			get
			{
				return this._cultureName.Length == 0;
			}
		}

		// Token: 0x0600233F RID: 9023 RVA: 0x001351E8 File Offset: 0x001343E8
		internal unsafe void IcuChangeCase(char* src, int srcLen, char* dstBuffer, int dstBufferCapacity, bool bToUpper)
		{
			if (this.IsInvariant)
			{
				Interop.Globalization.ChangeCaseInvariant(src, srcLen, dstBuffer, dstBufferCapacity, bToUpper);
				return;
			}
			if (this._needsTurkishCasing == TextInfo.Tristate.NotInitialized)
			{
				this._needsTurkishCasing = (TextInfo.NeedsTurkishCasing(this._textInfoName) ? TextInfo.Tristate.True : TextInfo.Tristate.False);
			}
			if (this._needsTurkishCasing == TextInfo.Tristate.True)
			{
				Interop.Globalization.ChangeCaseTurkish(src, srcLen, dstBuffer, dstBufferCapacity, bToUpper);
				return;
			}
			Interop.Globalization.ChangeCase(src, srcLen, dstBuffer, dstBufferCapacity, bToUpper);
		}

		// Token: 0x06002340 RID: 9024 RVA: 0x0013524C File Offset: 0x0013444C
		private unsafe void NlsChangeCase(char* pSource, int pSourceLen, char* pResult, int pResultLen, bool toUpper)
		{
			uint num = TextInfo.IsInvariantLocale(this._textInfoName) ? 0U : 16777216U;
			if (Interop.Kernel32.LCMapStringEx((this._sortHandle != IntPtr.Zero) ? null : this._textInfoName, num | (toUpper ? 512U : 256U), pSource, pSourceLen, (void*)pResult, pSourceLen, null, null, this._sortHandle) == 0)
			{
				throw new InvalidOperationException(SR.InvalidOperation_ReadOnly);
			}
		}

		// Token: 0x06002341 RID: 9025 RVA: 0x001352BE File Offset: 0x001344BE
		private static bool IsInvariantLocale(string localeName)
		{
			return localeName == "";
		}

		// Token: 0x040008E5 RID: 2277
		private string _listSeparator;

		// Token: 0x040008E6 RID: 2278
		private bool _isReadOnly;

		// Token: 0x040008E7 RID: 2279
		private readonly string _cultureName;

		// Token: 0x040008E8 RID: 2280
		private readonly CultureData _cultureData;

		// Token: 0x040008E9 RID: 2281
		private readonly string _textInfoName;

		// Token: 0x040008EA RID: 2282
		private TextInfo.Tristate _isAsciiCasingSameAsInvariant;

		// Token: 0x040008EB RID: 2283
		internal static readonly TextInfo Invariant = new TextInfo(CultureData.Invariant, true);

		// Token: 0x040008EC RID: 2284
		private TextInfo.Tristate _needsTurkishCasing;

		// Token: 0x040008ED RID: 2285
		private IntPtr _sortHandle;

		// Token: 0x02000229 RID: 553
		private enum Tristate : byte
		{
			// Token: 0x040008EF RID: 2287
			NotInitialized,
			// Token: 0x040008F0 RID: 2288
			False,
			// Token: 0x040008F1 RID: 2289
			True
		}

		// Token: 0x0200022A RID: 554
		private readonly struct ToUpperConversion
		{
		}

		// Token: 0x0200022B RID: 555
		private readonly struct ToLowerConversion
		{
		}
	}
}
