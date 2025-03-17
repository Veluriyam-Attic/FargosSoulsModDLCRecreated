using System;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x02000116 RID: 278
	internal ref struct __DTString
	{
		// Token: 0x17000151 RID: 337
		// (get) Token: 0x06000EA9 RID: 3753 RVA: 0x000D6C5D File Offset: 0x000D5E5D
		internal int Length
		{
			get
			{
				return this.Value.Length;
			}
		}

		// Token: 0x06000EAA RID: 3754 RVA: 0x000D6C6A File Offset: 0x000D5E6A
		internal __DTString(ReadOnlySpan<char> str, DateTimeFormatInfo dtfi, bool checkDigitToken)
		{
			this = new __DTString(str, dtfi);
			this.m_checkDigitToken = checkDigitToken;
		}

		// Token: 0x06000EAB RID: 3755 RVA: 0x000D6C7B File Offset: 0x000D5E7B
		internal __DTString(ReadOnlySpan<char> str, DateTimeFormatInfo dtfi)
		{
			this.Index = -1;
			this.Value = str;
			this.m_current = '\0';
			this.m_info = dtfi.CompareInfo;
			this.m_checkDigitToken = ((dtfi.FormatFlags & DateTimeFormatFlags.UseDigitPrefixInTokens) > DateTimeFormatFlags.None);
		}

		// Token: 0x17000152 RID: 338
		// (get) Token: 0x06000EAC RID: 3756 RVA: 0x000D6CB0 File Offset: 0x000D5EB0
		internal CompareInfo CompareInfo
		{
			get
			{
				return this.m_info;
			}
		}

		// Token: 0x06000EAD RID: 3757 RVA: 0x000D6CB8 File Offset: 0x000D5EB8
		internal unsafe bool GetNext()
		{
			this.Index++;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
				return true;
			}
			return false;
		}

		// Token: 0x06000EAE RID: 3758 RVA: 0x000D6CF1 File Offset: 0x000D5EF1
		internal bool AtEnd()
		{
			return this.Index >= this.Length;
		}

		// Token: 0x06000EAF RID: 3759 RVA: 0x000D6D04 File Offset: 0x000D5F04
		internal unsafe bool Advance(int count)
		{
			this.Index += count;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
				return true;
			}
			return false;
		}

		// Token: 0x06000EB0 RID: 3760 RVA: 0x000D6D40 File Offset: 0x000D5F40
		internal unsafe void GetRegularToken(out TokenType tokenType, out int tokenValue, DateTimeFormatInfo dtfi)
		{
			tokenValue = 0;
			if (this.Index >= this.Length)
			{
				tokenType = TokenType.EndOfString;
				return;
			}
			IL_15:
			while (!DateTimeParse.IsDigit(this.m_current))
			{
				if (char.IsWhiteSpace(this.m_current))
				{
					for (;;)
					{
						int num = this.Index + 1;
						this.Index = num;
						if (num >= this.Length)
						{
							break;
						}
						this.m_current = (char)(*this.Value[this.Index]);
						if (!char.IsWhiteSpace(this.m_current))
						{
							goto IL_15;
						}
					}
					tokenType = TokenType.EndOfString;
					return;
				}
				dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType, out tokenValue, ref this);
				return;
			}
			tokenValue = (int)(this.m_current - '0');
			int index = this.Index;
			for (;;)
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.Length)
				{
					break;
				}
				this.m_current = (char)(*this.Value[this.Index]);
				int num2 = (int)(this.m_current - '0');
				if (num2 < 0 || num2 > 9)
				{
					break;
				}
				tokenValue = tokenValue * 10 + num2;
			}
			if (this.Index - index > 8)
			{
				tokenType = TokenType.NumberToken;
				tokenValue = -1;
			}
			else if (this.Index - index < 3)
			{
				tokenType = TokenType.NumberToken;
			}
			else
			{
				tokenType = TokenType.YearNumberToken;
			}
			if (!this.m_checkDigitToken)
			{
				return;
			}
			int index2 = this.Index;
			char current = this.m_current;
			this.Index = index;
			this.m_current = (char)(*this.Value[this.Index]);
			TokenType tokenType2;
			int num3;
			if (dtfi.Tokenize(TokenType.RegularTokenMask, out tokenType2, out num3, ref this))
			{
				tokenType = tokenType2;
				tokenValue = num3;
				return;
			}
			this.Index = index2;
			this.m_current = current;
		}

		// Token: 0x06000EB1 RID: 3761 RVA: 0x000D6EC0 File Offset: 0x000D60C0
		internal TokenType GetSeparatorToken(DateTimeFormatInfo dtfi, out int indexBeforeSeparator, out char charBeforeSeparator)
		{
			indexBeforeSeparator = this.Index;
			charBeforeSeparator = this.m_current;
			if (!this.SkipWhiteSpaceCurrent())
			{
				return TokenType.SEP_End;
			}
			TokenType result;
			if (!DateTimeParse.IsDigit(this.m_current))
			{
				int num;
				if (!dtfi.Tokenize(TokenType.SeparatorTokenMask, out result, out num, ref this))
				{
					result = TokenType.SEP_Space;
				}
			}
			else
			{
				result = TokenType.SEP_Space;
			}
			return result;
		}

		// Token: 0x06000EB2 RID: 3762 RVA: 0x000D6F1C File Offset: 0x000D611C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool MatchSpecifiedWord(string target)
		{
			return this.Index + target.Length <= this.Length && this.m_info.Compare(this.Value.Slice(this.Index, target.Length), target, CompareOptions.IgnoreCase) == 0;
		}

		// Token: 0x06000EB3 RID: 3763 RVA: 0x000D6F6C File Offset: 0x000D616C
		internal unsafe bool MatchSpecifiedWords(string target, bool checkWordBoundary, ref int matchLength)
		{
			int num = this.Value.Length - this.Index;
			matchLength = target.Length;
			if (matchLength > num || this.m_info.Compare(this.Value.Slice(this.Index, matchLength), target, CompareOptions.IgnoreCase) != 0)
			{
				int num2 = 0;
				int num3 = this.Index;
				int num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2);
				if (num4 == -1)
				{
					return false;
				}
				for (;;)
				{
					int num5 = num4 - num2;
					if (num3 >= this.Value.Length - num5)
					{
						break;
					}
					if (num5 == 0)
					{
						matchLength--;
					}
					else
					{
						if (!char.IsWhiteSpace((char)(*this.Value[num3 + num5])))
						{
							return false;
						}
						if (this.m_info.CompareOptionIgnoreCase(this.Value.Slice(num3, num5), target.AsSpan(num2, num5)) != 0)
						{
							return false;
						}
						num3 = num3 + num5 + 1;
					}
					num2 = num4 + 1;
					while (num3 < this.Value.Length && char.IsWhiteSpace((char)(*this.Value[num3])))
					{
						num3++;
						matchLength++;
					}
					if ((num4 = target.IndexOfAny(__DTString.WhiteSpaceChecks, num2)) < 0)
					{
						goto Block_8;
					}
				}
				return false;
				Block_8:
				if (num2 < target.Length)
				{
					int num6 = target.Length - num2;
					if (num3 > this.Value.Length - num6)
					{
						return false;
					}
					if (this.m_info.CompareOptionIgnoreCase(this.Value.Slice(num3, num6), target.AsSpan(num2, num6)) != 0)
					{
						return false;
					}
				}
			}
			if (checkWordBoundary)
			{
				int num7 = this.Index + matchLength;
				if (num7 < this.Value.Length && char.IsLetter((char)(*this.Value[num7])))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000EB4 RID: 3764 RVA: 0x000D7110 File Offset: 0x000D6310
		internal bool Match(string str)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.Length)
			{
				return false;
			}
			if (str.Length > this.Value.Length - this.Index)
			{
				return false;
			}
			if (this.m_info.Compare(this.Value.Slice(this.Index, str.Length), str, CompareOptions.Ordinal) == 0)
			{
				this.Index += str.Length - 1;
				return true;
			}
			return false;
		}

		// Token: 0x06000EB5 RID: 3765 RVA: 0x000D719C File Offset: 0x000D639C
		internal unsafe bool Match(char ch)
		{
			int num = this.Index + 1;
			this.Index = num;
			if (num >= this.Length)
			{
				return false;
			}
			if (*this.Value[this.Index] == (ushort)ch)
			{
				this.m_current = ch;
				return true;
			}
			this.Index--;
			return false;
		}

		// Token: 0x06000EB6 RID: 3766 RVA: 0x000D71F4 File Offset: 0x000D63F4
		internal int MatchLongestWords(string[] words, ref int maxMatchStrLen)
		{
			int result = -1;
			for (int i = 0; i < words.Length; i++)
			{
				string text = words[i];
				int length = text.Length;
				if (this.MatchSpecifiedWords(text, false, ref length) && length > maxMatchStrLen)
				{
					maxMatchStrLen = length;
					result = i;
				}
			}
			return result;
		}

		// Token: 0x06000EB7 RID: 3767 RVA: 0x000D7234 File Offset: 0x000D6434
		internal unsafe int GetRepeatCount()
		{
			char c = (char)(*this.Value[this.Index]);
			int num = this.Index + 1;
			while (num < this.Length && *this.Value[num] == (ushort)c)
			{
				num++;
			}
			int result = num - this.Index;
			this.Index = num - 1;
			return result;
		}

		// Token: 0x06000EB8 RID: 3768 RVA: 0x000D7290 File Offset: 0x000D6490
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal unsafe bool GetNextDigit()
		{
			int num = this.Index + 1;
			this.Index = num;
			return num < this.Length && DateTimeParse.IsDigit((char)(*this.Value[this.Index]));
		}

		// Token: 0x06000EB9 RID: 3769 RVA: 0x000D72CF File Offset: 0x000D64CF
		internal unsafe char GetChar()
		{
			return (char)(*this.Value[this.Index]);
		}

		// Token: 0x06000EBA RID: 3770 RVA: 0x000D72E3 File Offset: 0x000D64E3
		internal unsafe int GetDigit()
		{
			return (int)(*this.Value[this.Index] - 48);
		}

		// Token: 0x06000EBB RID: 3771 RVA: 0x000D72FC File Offset: 0x000D64FC
		internal unsafe void SkipWhiteSpaces()
		{
			while (this.Index + 1 < this.Length)
			{
				char c = (char)(*this.Value[this.Index + 1]);
				if (!char.IsWhiteSpace(c))
				{
					return;
				}
				this.Index++;
			}
		}

		// Token: 0x06000EBC RID: 3772 RVA: 0x000D7348 File Offset: 0x000D6548
		internal unsafe bool SkipWhiteSpaceCurrent()
		{
			if (this.Index >= this.Length)
			{
				return false;
			}
			if (!char.IsWhiteSpace(this.m_current))
			{
				return true;
			}
			do
			{
				int num = this.Index + 1;
				this.Index = num;
				if (num >= this.Length)
				{
					return false;
				}
				this.m_current = (char)(*this.Value[this.Index]);
			}
			while (char.IsWhiteSpace(this.m_current));
			return true;
		}

		// Token: 0x06000EBD RID: 3773 RVA: 0x000D73B8 File Offset: 0x000D65B8
		internal unsafe void TrimTail()
		{
			int num = this.Length - 1;
			while (num >= 0 && char.IsWhiteSpace((char)(*this.Value[num])))
			{
				num--;
			}
			this.Value = this.Value.Slice(0, num + 1);
		}

		// Token: 0x06000EBE RID: 3774 RVA: 0x000D7404 File Offset: 0x000D6604
		internal unsafe void RemoveTrailingInQuoteSpaces()
		{
			int num = this.Length - 1;
			if (num <= 1)
			{
				return;
			}
			char c = (char)(*this.Value[num]);
			if ((c == '\'' || c == '"') && char.IsWhiteSpace((char)(*this.Value[num - 1])))
			{
				num--;
				while (num >= 1 && char.IsWhiteSpace((char)(*this.Value[num - 1])))
				{
					num--;
				}
				Span<char> span = new char[num + 1];
				*span[num] = c;
				this.Value.Slice(0, num).CopyTo(span);
				this.Value = span;
			}
		}

		// Token: 0x06000EBF RID: 3775 RVA: 0x000D74AC File Offset: 0x000D66AC
		internal unsafe void RemoveLeadingInQuoteSpaces()
		{
			if (this.Length <= 2)
			{
				return;
			}
			int num = 0;
			char c = (char)(*this.Value[num]);
			if (c != '\'')
			{
				if (c != '"')
				{
					return;
				}
			}
			while (num + 1 < this.Length && char.IsWhiteSpace((char)(*this.Value[num + 1])))
			{
				num++;
			}
			if (num != 0)
			{
				Span<char> span = new char[this.Value.Length - num];
				*span[0] = c;
				this.Value.Slice(num + 1).CopyTo(span.Slice(1));
				this.Value = span;
			}
		}

		// Token: 0x06000EC0 RID: 3776 RVA: 0x000D7554 File Offset: 0x000D6754
		internal unsafe DTSubString GetSubString()
		{
			DTSubString dtsubString = default(DTSubString);
			dtsubString.index = this.Index;
			dtsubString.s = this.Value;
			while (this.Index + dtsubString.length < this.Length)
			{
				char c = (char)(*this.Value[this.Index + dtsubString.length]);
				DTSubStringType dtsubStringType;
				if (c >= '0' && c <= '9')
				{
					dtsubStringType = DTSubStringType.Number;
				}
				else
				{
					dtsubStringType = DTSubStringType.Other;
				}
				if (dtsubString.length == 0)
				{
					dtsubString.type = dtsubStringType;
				}
				else if (dtsubString.type != dtsubStringType)
				{
					break;
				}
				dtsubString.length++;
				if (dtsubStringType != DTSubStringType.Number)
				{
					break;
				}
				if (dtsubString.length > 8)
				{
					dtsubString.type = DTSubStringType.Invalid;
					return dtsubString;
				}
				int num = (int)(c - '0');
				dtsubString.value = dtsubString.value * 10 + num;
			}
			if (dtsubString.length == 0)
			{
				dtsubString.type = DTSubStringType.End;
				return dtsubString;
			}
			return dtsubString;
		}

		// Token: 0x06000EC1 RID: 3777 RVA: 0x000D762F File Offset: 0x000D682F
		internal unsafe void ConsumeSubString(DTSubString sub)
		{
			this.Index = sub.index + sub.length;
			if (this.Index < this.Length)
			{
				this.m_current = (char)(*this.Value[this.Index]);
			}
		}

		// Token: 0x04000359 RID: 857
		internal ReadOnlySpan<char> Value;

		// Token: 0x0400035A RID: 858
		internal int Index;

		// Token: 0x0400035B RID: 859
		internal char m_current;

		// Token: 0x0400035C RID: 860
		private readonly CompareInfo m_info;

		// Token: 0x0400035D RID: 861
		private readonly bool m_checkDigitToken;

		// Token: 0x0400035E RID: 862
		private static readonly char[] WhiteSpaceChecks = new char[]
		{
			' ',
			'\u00a0'
		};
	}
}
