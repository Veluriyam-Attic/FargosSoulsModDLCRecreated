using System;
using System.Collections.Generic;
using System.Text;

namespace System.Globalization
{
	// Token: 0x020001FA RID: 506
	internal class DateTimeFormatInfoScanner
	{
		// Token: 0x06002079 RID: 8313 RVA: 0x0012A354 File Offset: 0x00129554
		internal static int SkipWhiteSpacesAndNonLetter(string pattern, int currentIndex)
		{
			while (currentIndex < pattern.Length)
			{
				char c = pattern[currentIndex];
				if (c == '\\')
				{
					currentIndex++;
					if (currentIndex >= pattern.Length)
					{
						break;
					}
					c = pattern[currentIndex];
					if (c == '\'')
					{
						continue;
					}
				}
				if (char.IsLetter(c) || c == '\'' || c == '.')
				{
					break;
				}
				currentIndex++;
			}
			return currentIndex;
		}

		// Token: 0x0600207A RID: 8314 RVA: 0x0012A3AC File Offset: 0x001295AC
		internal void AddDateWordOrPostfix(string formatPostfix, string str)
		{
			if (str.Length == 0)
			{
				return;
			}
			if (str.Length == 1)
			{
				char c = str[0];
				if (c <= '月')
				{
					if (c <= '年')
					{
						switch (c)
						{
						case '-':
						case '/':
							return;
						case '.':
							this.AddIgnorableSymbols(".");
							return;
						default:
							if (c != '分' && c != '年')
							{
								goto IL_E5;
							}
							break;
						}
					}
					else if (c <= '时')
					{
						if (c != '日' && c != '时')
						{
							goto IL_E5;
						}
					}
					else if (c != '時' && c != '月')
					{
						goto IL_E5;
					}
				}
				else if (c <= '분')
				{
					if (c != '秒' && c != '년' && c != '분')
					{
						goto IL_E5;
					}
				}
				else if (c <= '월')
				{
					if (c != '시' && c != '월')
					{
						goto IL_E5;
					}
				}
				else if (c != '일' && c != '초')
				{
					goto IL_E5;
				}
				return;
			}
			IL_E5:
			if (this.m_dateWords == null)
			{
				this.m_dateWords = new List<string>();
			}
			if (formatPostfix == "MMMM")
			{
				string item = "" + str;
				if (!this.m_dateWords.Contains(item))
				{
					this.m_dateWords.Add(item);
					return;
				}
			}
			else
			{
				if (!this.m_dateWords.Contains(str))
				{
					this.m_dateWords.Add(str);
				}
				int num = str.Length - 1;
				if (str[num] == '.')
				{
					int length = str.Length;
					num = 0;
					int length2 = length - 1 - num;
					string item2 = str.Substring(num, length2);
					if (!this.m_dateWords.Contains(item2))
					{
						this.m_dateWords.Add(item2);
					}
				}
			}
		}

		// Token: 0x0600207B RID: 8315 RVA: 0x0012A548 File Offset: 0x00129748
		internal int AddDateWords(string pattern, int index, string formatPostfix)
		{
			int num = DateTimeFormatInfoScanner.SkipWhiteSpacesAndNonLetter(pattern, index);
			if (num != index && formatPostfix != null)
			{
				formatPostfix = null;
			}
			index = num;
			StringBuilder stringBuilder = new StringBuilder();
			while (index < pattern.Length)
			{
				char c = pattern[index];
				if (c == '\'')
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					index++;
					break;
				}
				if (c == '\\')
				{
					index++;
					if (index < pattern.Length)
					{
						stringBuilder.Append(pattern[index]);
						index++;
					}
				}
				else if (char.IsWhiteSpace(c))
				{
					this.AddDateWordOrPostfix(formatPostfix, stringBuilder.ToString());
					if (formatPostfix != null)
					{
						formatPostfix = null;
					}
					stringBuilder.Length = 0;
					index++;
				}
				else
				{
					stringBuilder.Append(c);
					index++;
				}
			}
			return index;
		}

		// Token: 0x0600207C RID: 8316 RVA: 0x0012A5FE File Offset: 0x001297FE
		internal static int ScanRepeatChar(string pattern, char ch, int index, out int count)
		{
			count = 1;
			while (++index < pattern.Length && pattern[index] == ch)
			{
				count++;
			}
			return index;
		}

		// Token: 0x0600207D RID: 8317 RVA: 0x0012A624 File Offset: 0x00129824
		internal void AddIgnorableSymbols(string text)
		{
			if (this.m_dateWords == null)
			{
				this.m_dateWords = new List<string>();
			}
			string item = "" + text;
			if (!this.m_dateWords.Contains(item))
			{
				this.m_dateWords.Add(item);
			}
		}

		// Token: 0x0600207E RID: 8318 RVA: 0x0012A66C File Offset: 0x0012986C
		internal void ScanDateWord(string pattern)
		{
			this._ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
			for (int i = 0; i < pattern.Length; i++)
			{
				char c = pattern[i];
				if (c <= 'M')
				{
					if (c == '\'')
					{
						i = this.AddDateWords(pattern, i + 1, null);
						continue;
					}
					if (c == '.')
					{
						if (this._ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag)
						{
							this.AddIgnorableSymbols(".");
							this._ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
						}
						i++;
						continue;
					}
					if (c == 'M')
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'M', i, out num);
						if (num >= 4 && i < pattern.Length && pattern[i] == '\'')
						{
							i = this.AddDateWords(pattern, i + 1, "MMMM");
						}
						this._ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundMonthPatternFlag;
						continue;
					}
				}
				else
				{
					if (c == '\\')
					{
						i += 2;
						continue;
					}
					if (c != 'd')
					{
						if (c == 'y')
						{
							int num2;
							i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'y', i, out num2);
							this._ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundYearPatternFlag;
							continue;
						}
					}
					else
					{
						int num;
						i = DateTimeFormatInfoScanner.ScanRepeatChar(pattern, 'd', i, out num);
						if (num <= 2)
						{
							this._ymdFlags |= DateTimeFormatInfoScanner.FoundDatePattern.FoundDayPatternFlag;
							continue;
						}
						continue;
					}
				}
				if (this._ymdFlags == DateTimeFormatInfoScanner.FoundDatePattern.FoundYMDPatternFlag && !char.IsWhiteSpace(c))
				{
					this._ymdFlags = DateTimeFormatInfoScanner.FoundDatePattern.None;
				}
			}
		}

		// Token: 0x0600207F RID: 8319 RVA: 0x0012A7A4 File Offset: 0x001299A4
		internal string[] GetDateWordsOfDTFI(DateTimeFormatInfo dtfi)
		{
			string[] allDateTimePatterns = dtfi.GetAllDateTimePatterns('D');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			allDateTimePatterns = dtfi.GetAllDateTimePatterns('d');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			allDateTimePatterns = dtfi.GetAllDateTimePatterns('y');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			this.ScanDateWord(dtfi.MonthDayPattern);
			allDateTimePatterns = dtfi.GetAllDateTimePatterns('T');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			allDateTimePatterns = dtfi.GetAllDateTimePatterns('t');
			for (int i = 0; i < allDateTimePatterns.Length; i++)
			{
				this.ScanDateWord(allDateTimePatterns[i]);
			}
			string[] array = null;
			if (this.m_dateWords != null && this.m_dateWords.Count > 0)
			{
				array = new string[this.m_dateWords.Count];
				for (int i = 0; i < this.m_dateWords.Count; i++)
				{
					array[i] = this.m_dateWords[i];
				}
			}
			return array;
		}

		// Token: 0x06002080 RID: 8320 RVA: 0x0012A8AC File Offset: 0x00129AAC
		internal static FORMATFLAGS GetFormatFlagGenitiveMonth(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			if (DateTimeFormatInfoScanner.EqualStringArrays(monthNames, genitveMonthNames) && DateTimeFormatInfoScanner.EqualStringArrays(abbrevMonthNames, genetiveAbbrevMonthNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseGenitiveMonth;
		}

		// Token: 0x06002081 RID: 8321 RVA: 0x0012A8C4 File Offset: 0x00129AC4
		internal static FORMATFLAGS GetFormatFlagUseSpaceInMonthNames(string[] monthNames, string[] genitveMonthNames, string[] abbrevMonthNames, string[] genetiveAbbrevMonthNames)
		{
			FORMATFLAGS formatflags = FORMATFLAGS.None;
			formatflags |= ((DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(monthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsBeginWithDigit(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseDigitPrefixInTokens : FORMATFLAGS.None);
			return formatflags | ((DateTimeFormatInfoScanner.ArrayElementsHaveSpace(monthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genitveMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevMonthNames) || DateTimeFormatInfoScanner.ArrayElementsHaveSpace(genetiveAbbrevMonthNames)) ? FORMATFLAGS.UseSpacesInMonthNames : FORMATFLAGS.None);
		}

		// Token: 0x06002082 RID: 8322 RVA: 0x0012A923 File Offset: 0x00129B23
		internal static FORMATFLAGS GetFormatFlagUseSpaceInDayNames(string[] dayNames, string[] abbrevDayNames)
		{
			if (!DateTimeFormatInfoScanner.ArrayElementsHaveSpace(dayNames) && !DateTimeFormatInfoScanner.ArrayElementsHaveSpace(abbrevDayNames))
			{
				return FORMATFLAGS.None;
			}
			return FORMATFLAGS.UseSpacesInDayNames;
		}

		// Token: 0x06002083 RID: 8323 RVA: 0x0012A939 File Offset: 0x00129B39
		internal static FORMATFLAGS GetFormatFlagUseHebrewCalendar(int calID)
		{
			if (calID != 8)
			{
				return FORMATFLAGS.None;
			}
			return (FORMATFLAGS)10;
		}

		// Token: 0x06002084 RID: 8324 RVA: 0x0012A944 File Offset: 0x00129B44
		private static bool EqualStringArrays(string[] array1, string[] array2)
		{
			if (array1 == array2)
			{
				return true;
			}
			if (array1.Length != array2.Length)
			{
				return false;
			}
			for (int i = 0; i < array1.Length; i++)
			{
				if (array1[i] != array2[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06002085 RID: 8325 RVA: 0x0012A980 File Offset: 0x00129B80
		private static bool ArrayElementsHaveSpace(string[] array)
		{
			for (int i = 0; i < array.Length; i++)
			{
				for (int j = 0; j < array[i].Length; j++)
				{
					if (char.IsWhiteSpace(array[i][j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x06002086 RID: 8326 RVA: 0x0012A9C4 File Offset: 0x00129BC4
		private static bool ArrayElementsBeginWithDigit(string[] array)
		{
			int i = 0;
			while (i < array.Length)
			{
				if (array[i].Length > 0 && array[i][0] >= '0' && array[i][0] <= '9')
				{
					int num = 1;
					while (num < array[i].Length && array[i][num] >= '0' && array[i][num] <= '9')
					{
						num++;
					}
					if (num == array[i].Length)
					{
						return false;
					}
					if (num == array[i].Length - 1)
					{
						char c = array[i][num];
						if (c == '月' || c == '월')
						{
							return false;
						}
					}
					return num != array[i].Length - 4 || array[i][num] != '\'' || array[i][num + 1] != ' ' || array[i][num + 2] != '月' || array[i][num + 3] != '\'';
				}
				else
				{
					i++;
				}
			}
			return false;
		}

		// Token: 0x040007F7 RID: 2039
		internal List<string> m_dateWords = new List<string>();

		// Token: 0x040007F8 RID: 2040
		private DateTimeFormatInfoScanner.FoundDatePattern _ymdFlags;

		// Token: 0x020001FB RID: 507
		private enum FoundDatePattern
		{
			// Token: 0x040007FA RID: 2042
			None,
			// Token: 0x040007FB RID: 2043
			FoundYearPatternFlag,
			// Token: 0x040007FC RID: 2044
			FoundMonthPatternFlag,
			// Token: 0x040007FD RID: 2045
			FoundDayPatternFlag = 4,
			// Token: 0x040007FE RID: 2046
			FoundYMDPatternFlag = 7
		}
	}
}
