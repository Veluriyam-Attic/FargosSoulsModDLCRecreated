using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.Globalization
{
	// Token: 0x02000212 RID: 530
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class IdnMapping
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x0012E501 File Offset: 0x0012D701
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x0012E509 File Offset: 0x0012D709
		public bool AllowUnassigned
		{
			get
			{
				return this._allowUnassigned;
			}
			set
			{
				this._allowUnassigned = value;
			}
		}

		// Token: 0x17000764 RID: 1892
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x0012E512 File Offset: 0x0012D712
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x0012E51A File Offset: 0x0012D71A
		public bool UseStd3AsciiRules
		{
			get
			{
				return this._useStd3AsciiRules;
			}
			set
			{
				this._useStd3AsciiRules = value;
			}
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x0012E523 File Offset: 0x0012D723
		public string GetAscii(string unicode)
		{
			return this.GetAscii(unicode, 0);
		}

		// Token: 0x0600216D RID: 8557 RVA: 0x0012E52D File Offset: 0x0012D72D
		public string GetAscii(string unicode, int index)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			return this.GetAscii(unicode, index, unicode.Length - index);
		}

		// Token: 0x0600216E RID: 8558 RVA: 0x0012E550 File Offset: 0x0012D750
		public unsafe string GetAscii(string unicode, int index, int count)
		{
			if (unicode == null)
			{
				throw new ArgumentNullException("unicode");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (index > unicode.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (index > unicode.Length - count)
			{
				throw new ArgumentOutOfRangeException("unicode", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (count == 0)
			{
				throw new ArgumentException(SR.Argument_IdnBadLabelSize, "unicode");
			}
			if (unicode[index + count - 1] == '\0')
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidCharSequence, index + count - 1), "unicode");
			}
			if (GlobalizationMode.Invariant)
			{
				return this.GetAsciiInvariant(unicode, index, count);
			}
			char* ptr;
			if (unicode == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = unicode.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* ptr2 = ptr;
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuGetAsciiCore(unicode, ptr2 + index, count);
			}
			return this.NlsGetAsciiCore(unicode, ptr2 + index, count);
		}

		// Token: 0x0600216F RID: 8559 RVA: 0x0012E647 File Offset: 0x0012D847
		public string GetUnicode(string ascii)
		{
			return this.GetUnicode(ascii, 0);
		}

		// Token: 0x06002170 RID: 8560 RVA: 0x0012E651 File Offset: 0x0012D851
		public string GetUnicode(string ascii, int index)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			return this.GetUnicode(ascii, index, ascii.Length - index);
		}

		// Token: 0x06002171 RID: 8561 RVA: 0x0012E674 File Offset: 0x0012D874
		public unsafe string GetUnicode(string ascii, int index, int count)
		{
			if (ascii == null)
			{
				throw new ArgumentNullException("ascii");
			}
			if (index < 0 || count < 0)
			{
				throw new ArgumentOutOfRangeException((index < 0) ? "index" : "count", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (index > ascii.Length)
			{
				throw new ArgumentOutOfRangeException("index", SR.ArgumentOutOfRange_Index);
			}
			if (index > ascii.Length - count)
			{
				throw new ArgumentOutOfRangeException("ascii", SR.ArgumentOutOfRange_IndexCountBuffer);
			}
			if (count > 0 && ascii[index + count - 1] == '\0')
			{
				throw new ArgumentException(SR.Argument_IdnBadPunycode, "ascii");
			}
			if (GlobalizationMode.Invariant)
			{
				return this.GetUnicodeInvariant(ascii, index, count);
			}
			char* ptr;
			if (ascii == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* pinnableReference = ascii.GetPinnableReference())
				{
					ptr = pinnableReference;
				}
			}
			char* ptr2 = ptr;
			if (!GlobalizationMode.UseNls)
			{
				return this.IcuGetUnicodeCore(ascii, ptr2 + index, count);
			}
			return this.NlsGetUnicodeCore(ascii, ptr2 + index, count);
		}

		// Token: 0x06002172 RID: 8562 RVA: 0x0012E750 File Offset: 0x0012D950
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			IdnMapping idnMapping = obj as IdnMapping;
			return idnMapping != null && this._allowUnassigned == idnMapping._allowUnassigned && this._useStd3AsciiRules == idnMapping._useStd3AsciiRules;
		}

		// Token: 0x06002173 RID: 8563 RVA: 0x0012E785 File Offset: 0x0012D985
		public override int GetHashCode()
		{
			return (this._allowUnassigned ? 100 : 200) + (this._useStd3AsciiRules ? 1000 : 2000);
		}

		// Token: 0x06002174 RID: 8564 RVA: 0x0012E7AD File Offset: 0x0012D9AD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		private unsafe static string GetStringForOutput(string originalString, char* input, int inputLength, char* output, int outputLength)
		{
			if (originalString.Length == inputLength && inputLength == outputLength && Ordinal.EqualsIgnoreCase(ref *input, ref *output, inputLength))
			{
				return originalString;
			}
			return new string(output, 0, outputLength);
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0012E7D4 File Offset: 0x0012D9D4
		private string GetAsciiInvariant(string unicode, int index, int count)
		{
			if (index > 0 || count < unicode.Length)
			{
				unicode = unicode.Substring(index, count);
			}
			if (IdnMapping.ValidateStd3AndAscii(unicode, this.UseStd3AsciiRules, true))
			{
				return unicode;
			}
			string text = unicode;
			int index2 = text.Length - 1;
			if (text[index2] <= '\u001f')
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidCharSequence, unicode.Length - 1), "unicode");
			}
			if (this.UseStd3AsciiRules)
			{
				IdnMapping.ValidateStd3AndAscii(unicode, true, false);
			}
			return IdnMapping.PunycodeEncode(unicode);
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0012E858 File Offset: 0x0012DA58
		private static bool ValidateStd3AndAscii(string unicode, bool bUseStd3, bool bCheckAscii)
		{
			if (unicode.Length == 0)
			{
				throw new ArgumentException(SR.Argument_IdnBadLabelSize, "unicode");
			}
			int num = -1;
			for (int i = 0; i < unicode.Length; i++)
			{
				if (unicode[i] <= '\u001f')
				{
					throw new ArgumentException(SR.Format(SR.Argument_InvalidCharSequence, i), "unicode");
				}
				if (bCheckAscii && unicode[i] >= '\u007f')
				{
					return false;
				}
				if (IdnMapping.IsDot(unicode[i]))
				{
					if (i == num + 1)
					{
						throw new ArgumentException(SR.Argument_IdnBadLabelSize, "unicode");
					}
					if (i - num > 64)
					{
						throw new ArgumentException(SR.Argument_IdnBadLabelSize, "unicode");
					}
					if (bUseStd3 && i > 0)
					{
						IdnMapping.ValidateStd3(unicode[i - 1], true);
					}
					num = i;
				}
				else if (bUseStd3)
				{
					IdnMapping.ValidateStd3(unicode[i], i == num + 1);
				}
			}
			if (num == -1 && unicode.Length > 63)
			{
				throw new ArgumentException(SR.Argument_IdnBadLabelSize, "unicode");
			}
			int length = unicode.Length;
			int num2 = 255;
			int index = unicode.Length - 1;
			if (length > num2 - (IdnMapping.IsDot(unicode[index]) ? 0 : 1))
			{
				string argument_IdnBadNameSize = SR.Argument_IdnBadNameSize;
				int num3 = 255;
				index = unicode.Length - 1;
				throw new ArgumentException(SR.Format(argument_IdnBadNameSize, num3 - (IdnMapping.IsDot(unicode[index]) ? 0 : 1)), "unicode");
			}
			if (bUseStd3)
			{
				index = unicode.Length - 1;
				if (!IdnMapping.IsDot(unicode[index]))
				{
					index = unicode.Length - 1;
					IdnMapping.ValidateStd3(unicode[index], true);
				}
			}
			return true;
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0012E9EC File Offset: 0x0012DBEC
		private static string PunycodeEncode(string unicode)
		{
			if (unicode.Length == 0)
			{
				throw new ArgumentException(SR.Argument_IdnBadLabelSize, "unicode");
			}
			StringBuilder stringBuilder = new StringBuilder(unicode.Length);
			int i = 0;
			int num = 0;
			int num2 = 0;
			while (i < unicode.Length)
			{
				i = unicode.IndexOfAny(IdnMapping.s_dotSeparators, num);
				if (i < 0)
				{
					i = unicode.Length;
				}
				if (i == num)
				{
					if (i != unicode.Length)
					{
						throw new ArgumentException(SR.Argument_IdnBadLabelSize, "unicode");
					}
					break;
				}
				else
				{
					stringBuilder.Append("xn--");
					bool flag = false;
					StrongBidiCategory bidiCategory = CharUnicodeInfo.GetBidiCategory(unicode, num);
					if (bidiCategory == StrongBidiCategory.StrongRightToLeft)
					{
						flag = true;
						int num3 = i - 1;
						if (char.IsLowSurrogate(unicode, num3))
						{
							num3--;
						}
						bidiCategory = CharUnicodeInfo.GetBidiCategory(unicode, num3);
						if (bidiCategory != StrongBidiCategory.StrongRightToLeft)
						{
							throw new ArgumentException(SR.Argument_IdnBadBidi, "unicode");
						}
					}
					int j = 0;
					for (int k = num; k < i; k++)
					{
						StrongBidiCategory bidiCategory2 = CharUnicodeInfo.GetBidiCategory(unicode, k);
						if (flag && bidiCategory2 == StrongBidiCategory.StrongLeftToRight)
						{
							throw new ArgumentException(SR.Argument_IdnBadBidi, "unicode");
						}
						if (!flag && bidiCategory2 == StrongBidiCategory.StrongRightToLeft)
						{
							throw new ArgumentException(SR.Argument_IdnBadBidi, "unicode");
						}
						if (IdnMapping.Basic((uint)unicode[k]))
						{
							stringBuilder.Append(IdnMapping.EncodeBasic(unicode[k]));
							j++;
						}
						else if (char.IsSurrogatePair(unicode, k))
						{
							k++;
						}
					}
					int num4 = j;
					if (num4 == i - num)
					{
						stringBuilder.Remove(num2, "xn--".Length);
					}
					else
					{
						if (unicode.Length - num >= "xn--".Length && unicode.Substring(num, "xn--".Length).Equals("xn--", StringComparison.OrdinalIgnoreCase))
						{
							throw new ArgumentException(SR.Argument_IdnBadPunycode, "unicode");
						}
						int num5 = 0;
						if (num4 > 0)
						{
							stringBuilder.Append('-');
						}
						int num6 = 128;
						int num7 = 0;
						int num8 = 72;
						while (j < i - num)
						{
							int num9 = 134217727;
							int num10;
							for (int l = num; l < i; l += (IdnMapping.IsSupplementary(num10) ? 2 : 1))
							{
								num10 = char.ConvertToUtf32(unicode, l);
								if (num10 >= num6 && num10 < num9)
								{
									num9 = num10;
								}
							}
							num7 += (num9 - num6) * (j - num5 + 1);
							num6 = num9;
							for (int l = num; l < i; l += (IdnMapping.IsSupplementary(num10) ? 2 : 1))
							{
								num10 = char.ConvertToUtf32(unicode, l);
								if (num10 < num6)
								{
									num7++;
								}
								if (num10 == num6)
								{
									int num11 = num7;
									int num12 = 36;
									for (;;)
									{
										int num13 = (num12 <= num8) ? 1 : ((num12 >= num8 + 26) ? 26 : (num12 - num8));
										if (num11 < num13)
										{
											break;
										}
										stringBuilder.Append(IdnMapping.EncodeDigit(num13 + (num11 - num13) % (36 - num13)));
										num11 = (num11 - num13) / (36 - num13);
										num12 += 36;
									}
									stringBuilder.Append(IdnMapping.EncodeDigit(num11));
									num8 = IdnMapping.Adapt(num7, j - num5 + 1, j == num4);
									num7 = 0;
									j++;
									if (IdnMapping.IsSupplementary(num9))
									{
										j++;
										num5++;
									}
								}
							}
							num7++;
							num6++;
						}
					}
					if (stringBuilder.Length - num2 > 63)
					{
						throw new ArgumentException(SR.Argument_IdnBadLabelSize, "unicode");
					}
					if (i != unicode.Length)
					{
						stringBuilder.Append('.');
					}
					num = i + 1;
					num2 = stringBuilder.Length;
				}
			}
			int length = stringBuilder.Length;
			int num14 = 255;
			int index = unicode.Length - 1;
			if (length > num14 - (IdnMapping.IsDot(unicode[index]) ? 0 : 1))
			{
				string argument_IdnBadNameSize = SR.Argument_IdnBadNameSize;
				int num15 = 255;
				index = unicode.Length - 1;
				throw new ArgumentException(SR.Format(argument_IdnBadNameSize, num15 - (IdnMapping.IsDot(unicode[index]) ? 0 : 1)), "unicode");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06002178 RID: 8568 RVA: 0x0012EDCA File Offset: 0x0012DFCA
		private static bool IsDot(char c)
		{
			return c == '.' || c == '。' || c == '．' || c == '｡';
		}

		// Token: 0x06002179 RID: 8569 RVA: 0x0012EDEB File Offset: 0x0012DFEB
		private static bool IsSupplementary(int cTest)
		{
			return cTest >= 65536;
		}

		// Token: 0x0600217A RID: 8570 RVA: 0x0012EDF8 File Offset: 0x0012DFF8
		private static bool Basic(uint cp)
		{
			return cp < 128U;
		}

		// Token: 0x0600217B RID: 8571 RVA: 0x0012EE04 File Offset: 0x0012E004
		private static void ValidateStd3(char c, bool bNextToDot)
		{
			if (c <= ',' || c == '/' || (c >= ':' && c <= '@') || (c >= '[' && c <= '`') || (c >= '{' && c <= '\u007f') || (c == '-' && bNextToDot))
			{
				throw new ArgumentException(SR.Format(SR.Argument_IdnBadStd3, c), "c");
			}
		}

		// Token: 0x0600217C RID: 8572 RVA: 0x0012EE60 File Offset: 0x0012E060
		private string GetUnicodeInvariant(string ascii, int index, int count)
		{
			if (index > 0 || count < ascii.Length)
			{
				ascii = ascii.Substring(index, count);
			}
			string text = IdnMapping.PunycodeDecode(ascii);
			if (!ascii.Equals(this.GetAscii(text), StringComparison.OrdinalIgnoreCase))
			{
				throw new ArgumentException(SR.Argument_IdnIllegalName, "ascii");
			}
			return text;
		}

		// Token: 0x0600217D RID: 8573 RVA: 0x0012EEAC File Offset: 0x0012E0AC
		private static string PunycodeDecode(string ascii)
		{
			if (ascii.Length == 0)
			{
				throw new ArgumentException(SR.Argument_IdnBadLabelSize, "ascii");
			}
			int length = ascii.Length;
			int num = 255;
			int index = ascii.Length - 1;
			if (length > num - (IdnMapping.IsDot(ascii[index]) ? 0 : 1))
			{
				string argument_IdnBadNameSize = SR.Argument_IdnBadNameSize;
				int num2 = 255;
				index = ascii.Length - 1;
				throw new ArgumentException(SR.Format(argument_IdnBadNameSize, num2 - (IdnMapping.IsDot(ascii[index]) ? 0 : 1)), "ascii");
			}
			StringBuilder stringBuilder = new StringBuilder(ascii.Length);
			int i = 0;
			int num3 = 0;
			int num4 = 0;
			while (i < ascii.Length)
			{
				i = ascii.IndexOf('.', num3);
				if (i < 0 || i > ascii.Length)
				{
					i = ascii.Length;
				}
				if (i == num3)
				{
					if (i != ascii.Length)
					{
						throw new ArgumentException(SR.Argument_IdnBadLabelSize, "ascii");
					}
					break;
				}
				else
				{
					if (i - num3 > 63)
					{
						throw new ArgumentException(SR.Argument_IdnBadLabelSize, "ascii");
					}
					if (ascii.Length < "xn--".Length + num3 || string.Compare(ascii, num3, "xn--", 0, "xn--".Length, StringComparison.OrdinalIgnoreCase) != 0)
					{
						stringBuilder.Append(ascii, num3, i - num3);
					}
					else
					{
						num3 += "xn--".Length;
						int num5 = ascii.LastIndexOf('-', i - 1);
						if (num5 == i - 1)
						{
							throw new ArgumentException(SR.Argument_IdnBadPunycode, "ascii");
						}
						int num6;
						if (num5 <= num3)
						{
							num6 = 0;
						}
						else
						{
							num6 = num5 - num3;
							for (int j = num3; j < num3 + num6; j++)
							{
								if (ascii[j] > '\u007f')
								{
									throw new ArgumentException(SR.Argument_IdnBadPunycode, "ascii");
								}
								stringBuilder.Append((ascii[j] >= 'A' && ascii[j] <= 'Z') ? (ascii[j] - 'A' + 'a') : ascii[j]);
							}
						}
						int k = num3 + ((num6 > 0) ? (num6 + 1) : 0);
						int num7 = 128;
						int num8 = 72;
						int num9 = 0;
						int num10 = 0;
						IL_3CC:
						while (k < i)
						{
							int num11 = num9;
							int num12 = 1;
							int num13 = 36;
							while (k < i)
							{
								int num14 = IdnMapping.DecodeDigit(ascii[k++]);
								if (num14 > (134217727 - num9) / num12)
								{
									throw new ArgumentException(SR.Argument_IdnBadPunycode, "ascii");
								}
								num9 += num14 * num12;
								int num15 = (num13 <= num8) ? 1 : ((num13 >= num8 + 26) ? 26 : (num13 - num8));
								if (num14 >= num15)
								{
									if (num12 > 134217727 / (36 - num15))
									{
										throw new ArgumentException(SR.Argument_IdnBadPunycode, "ascii");
									}
									num12 *= 36 - num15;
									num13 += 36;
								}
								else
								{
									num8 = IdnMapping.Adapt(num9 - num11, stringBuilder.Length - num4 - num10 + 1, num11 == 0);
									if (num9 / (stringBuilder.Length - num4 - num10 + 1) > 134217727 - num7)
									{
										throw new ArgumentException(SR.Argument_IdnBadPunycode, "ascii");
									}
									num7 += num9 / (stringBuilder.Length - num4 - num10 + 1);
									num9 %= stringBuilder.Length - num4 - num10 + 1;
									if (num7 < 0 || num7 > 1114111 || (num7 >= 55296 && num7 <= 57343))
									{
										throw new ArgumentException(SR.Argument_IdnBadPunycode, "ascii");
									}
									string value = char.ConvertFromUtf32(num7);
									int num16;
									if (num10 > 0)
									{
										int l = num9;
										num16 = num4;
										while (l > 0)
										{
											if (num16 >= stringBuilder.Length)
											{
												throw new ArgumentException(SR.Argument_IdnBadPunycode, "ascii");
											}
											if (char.IsSurrogate(stringBuilder[num16]))
											{
												num16++;
											}
											l--;
											num16++;
										}
									}
									else
									{
										num16 = num4 + num9;
									}
									stringBuilder.Insert(num16, value);
									if (IdnMapping.IsSupplementary(num7))
									{
										num10++;
									}
									num9++;
									goto IL_3CC;
								}
							}
							throw new ArgumentException(SR.Argument_IdnBadPunycode, "ascii");
						}
						bool flag = false;
						StrongBidiCategory bidiCategory = CharUnicodeInfo.GetBidiCategory(stringBuilder, num4);
						if (bidiCategory == StrongBidiCategory.StrongRightToLeft)
						{
							flag = true;
						}
						for (int m = num4; m < stringBuilder.Length; m++)
						{
							if (!char.IsLowSurrogate(stringBuilder[m]))
							{
								bidiCategory = CharUnicodeInfo.GetBidiCategory(stringBuilder, m);
								if ((flag && bidiCategory == StrongBidiCategory.StrongLeftToRight) || (!flag && bidiCategory == StrongBidiCategory.StrongRightToLeft))
								{
									throw new ArgumentException(SR.Argument_IdnBadBidi, "ascii");
								}
							}
						}
						if (flag && bidiCategory != StrongBidiCategory.StrongRightToLeft)
						{
							throw new ArgumentException(SR.Argument_IdnBadBidi, "ascii");
						}
					}
					if (i - num3 > 63)
					{
						throw new ArgumentException(SR.Argument_IdnBadLabelSize, "ascii");
					}
					if (i != ascii.Length)
					{
						stringBuilder.Append('.');
					}
					num3 = i + 1;
					num4 = stringBuilder.Length;
				}
			}
			if (stringBuilder.Length > 255 - (IdnMapping.IsDot(stringBuilder[stringBuilder.Length - 1]) ? 0 : 1))
			{
				throw new ArgumentException(SR.Format(SR.Argument_IdnBadNameSize, 255 - (IdnMapping.IsDot(stringBuilder[stringBuilder.Length - 1]) ? 0 : 1)), "ascii");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x0600217E RID: 8574 RVA: 0x0012F3B4 File Offset: 0x0012E5B4
		private static int DecodeDigit(char cp)
		{
			if (cp >= '0' && cp <= '9')
			{
				return (int)(cp - '0' + '\u001a');
			}
			if (cp >= 'a' && cp <= 'z')
			{
				return (int)(cp - 'a');
			}
			if (cp >= 'A' && cp <= 'Z')
			{
				return (int)(cp - 'A');
			}
			throw new ArgumentException(SR.Argument_IdnBadPunycode, "cp");
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0012F400 File Offset: 0x0012E600
		private static int Adapt(int delta, int numpoints, bool firsttime)
		{
			delta = (firsttime ? (delta / 700) : (delta / 2));
			delta += delta / numpoints;
			uint num = 0U;
			while (delta > 455)
			{
				delta /= 35;
				num += 36U;
			}
			return (int)((ulong)num + (ulong)((long)(36 * delta / (delta + 38))));
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0012F44A File Offset: 0x0012E64A
		private static char EncodeBasic(char bcp)
		{
			if (IdnMapping.HasUpperCaseFlag(bcp))
			{
				bcp += ' ';
			}
			return bcp;
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0012F45C File Offset: 0x0012E65C
		private static bool HasUpperCaseFlag(char punychar)
		{
			return punychar >= 'A' && punychar <= 'Z';
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0012F46D File Offset: 0x0012E66D
		private static char EncodeDigit(int d)
		{
			if (d > 25)
			{
				return (char)(d - 26 + 48);
			}
			return (char)(d + 97);
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0012F484 File Offset: 0x0012E684
		private unsafe string IcuGetAsciiCore(string unicodeString, char* unicode, int count)
		{
			uint icuFlags = this.IcuFlags;
			IdnMapping.CheckInvalidIdnCharacters(unicode, count, icuFlags, "unicode");
			int num = (int)Math.Min((long)count * 3L + 4L, 512L);
			int num2;
			if (num < 512)
			{
				char* ptr = stackalloc char[checked(unchecked((UIntPtr)num) * 2)];
				num2 = Interop.Globalization.ToAscii(icuFlags, unicode, count, ptr, num);
				if (num2 > 0 && num2 <= num)
				{
					return IdnMapping.GetStringForOutput(unicodeString, unicode, count, ptr, num2);
				}
			}
			else
			{
				num2 = Interop.Globalization.ToAscii(icuFlags, unicode, count, null, 0);
			}
			if (num2 == 0)
			{
				throw new ArgumentException(SR.Argument_IdnIllegalName, "unicode");
			}
			char[] array = new char[num2];
			fixed (char* ptr2 = &array[0])
			{
				char* ptr3 = ptr2;
				num2 = Interop.Globalization.ToAscii(icuFlags, unicode, count, ptr3, num2);
				if (num2 == 0 || num2 > array.Length)
				{
					throw new ArgumentException(SR.Argument_IdnIllegalName, "unicode");
				}
				return IdnMapping.GetStringForOutput(unicodeString, unicode, count, ptr3, num2);
			}
		}

		// Token: 0x06002184 RID: 8580 RVA: 0x0012F550 File Offset: 0x0012E750
		private unsafe string IcuGetUnicodeCore(string asciiString, char* ascii, int count)
		{
			uint icuFlags = this.IcuFlags;
			IdnMapping.CheckInvalidIdnCharacters(ascii, count, icuFlags, "ascii");
			if (count < 512)
			{
				char* output = stackalloc char[checked(unchecked((UIntPtr)count) * 2)];
				return this.IcuGetUnicodeCore(asciiString, ascii, count, icuFlags, output, count, true);
			}
			char[] array = new char[count];
			fixed (char* ptr = &array[0])
			{
				char* output2 = ptr;
				return this.IcuGetUnicodeCore(asciiString, ascii, count, icuFlags, output2, count, true);
			}
		}

		// Token: 0x06002185 RID: 8581 RVA: 0x0012F5B0 File Offset: 0x0012E7B0
		private unsafe string IcuGetUnicodeCore(string asciiString, char* ascii, int count, uint flags, char* output, int outputLength, bool reattempt)
		{
			int num = Interop.Globalization.ToUnicode(flags, ascii, count, output, outputLength);
			if (num == 0)
			{
				throw new ArgumentException(SR.Argument_IdnIllegalName, "ascii");
			}
			if (num <= outputLength)
			{
				return IdnMapping.GetStringForOutput(asciiString, ascii, count, output, num);
			}
			if (reattempt)
			{
				char[] array = new char[num];
				char[] array2;
				char* output2;
				if ((array2 = array) == null || array2.Length == 0)
				{
					output2 = null;
				}
				else
				{
					output2 = &array2[0];
				}
				return this.IcuGetUnicodeCore(asciiString, ascii, count, flags, output2, num, false);
			}
			throw new ArgumentException(SR.Argument_IdnIllegalName, "ascii");
		}

		// Token: 0x17000765 RID: 1893
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0012F630 File Offset: 0x0012E830
		private uint IcuFlags
		{
			get
			{
				return (this.AllowUnassigned ? 1U : 0U) | (this.UseStd3AsciiRules ? 2U : 0U);
			}
		}

		// Token: 0x06002187 RID: 8583 RVA: 0x0012F658 File Offset: 0x0012E858
		private unsafe static void CheckInvalidIdnCharacters(char* s, int count, uint flags, string paramName)
		{
			if ((flags & 2U) == 0U)
			{
				for (int i = 0; i < count; i++)
				{
					char c = s[i];
					if (c <= '\u001f' || c == '\u007f')
					{
						throw new ArgumentException(SR.Argument_IdnIllegalName, paramName);
					}
				}
			}
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0012F694 File Offset: 0x0012E894
		private unsafe string NlsGetAsciiCore(string unicodeString, char* unicode, int count)
		{
			uint nlsFlags = this.NlsFlags;
			int num = Interop.Normaliz.IdnToAscii(nlsFlags, unicode, count, null, 0);
			if (num == 0)
			{
				IdnMapping.ThrowForZeroLength(true);
			}
			if (num < 512)
			{
				char* output = stackalloc char[checked(unchecked((UIntPtr)num) * 2)];
				return this.NlsGetAsciiCore(unicodeString, unicode, count, nlsFlags, output, num);
			}
			char[] array = new char[num];
			fixed (char* ptr = &array[0])
			{
				char* output2 = ptr;
				return this.NlsGetAsciiCore(unicodeString, unicode, count, nlsFlags, output2, num);
			}
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0012F6FC File Offset: 0x0012E8FC
		private unsafe string NlsGetAsciiCore(string unicodeString, char* unicode, int count, uint flags, char* output, int outputLength)
		{
			int num = Interop.Normaliz.IdnToAscii(flags, unicode, count, output, outputLength);
			if (num == 0)
			{
				IdnMapping.ThrowForZeroLength(true);
			}
			return IdnMapping.GetStringForOutput(unicodeString, unicode, count, output, num);
		}

		// Token: 0x0600218A RID: 8586 RVA: 0x0012F72C File Offset: 0x0012E92C
		private unsafe string NlsGetUnicodeCore(string asciiString, char* ascii, int count)
		{
			uint nlsFlags = this.NlsFlags;
			int num = Interop.Normaliz.IdnToUnicode(nlsFlags, ascii, count, null, 0);
			if (num == 0)
			{
				IdnMapping.ThrowForZeroLength(false);
			}
			if (num < 512)
			{
				char* output = stackalloc char[checked(unchecked((UIntPtr)num) * 2)];
				return this.NlsGetUnicodeCore(asciiString, ascii, count, nlsFlags, output, num);
			}
			char[] array = new char[num];
			fixed (char* ptr = &array[0])
			{
				char* output2 = ptr;
				return this.NlsGetUnicodeCore(asciiString, ascii, count, nlsFlags, output2, num);
			}
		}

		// Token: 0x0600218B RID: 8587 RVA: 0x0012F794 File Offset: 0x0012E994
		private unsafe string NlsGetUnicodeCore(string asciiString, char* ascii, int count, uint flags, char* output, int outputLength)
		{
			int num = Interop.Normaliz.IdnToUnicode(flags, ascii, count, output, outputLength);
			if (num == 0)
			{
				IdnMapping.ThrowForZeroLength(false);
			}
			return IdnMapping.GetStringForOutput(asciiString, ascii, count, output, num);
		}

		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600218C RID: 8588 RVA: 0x0012F630 File Offset: 0x0012E830
		private uint NlsFlags
		{
			get
			{
				return (this.AllowUnassigned ? 1U : 0U) | (this.UseStd3AsciiRules ? 2U : 0U);
			}
		}

		// Token: 0x0600218D RID: 8589 RVA: 0x0012F7C4 File Offset: 0x0012E9C4
		[DoesNotReturn]
		private static void ThrowForZeroLength(bool unicode)
		{
			int lastWin32Error = Marshal.GetLastWin32Error();
			throw new ArgumentException((lastWin32Error == 123) ? SR.Argument_IdnIllegalName : (unicode ? SR.Argument_InvalidCharSequenceNoIndex : SR.Argument_IdnBadPunycode), unicode ? "unicode" : "ascii");
		}

		// Token: 0x04000878 RID: 2168
		private bool _allowUnassigned;

		// Token: 0x04000879 RID: 2169
		private bool _useStd3AsciiRules;

		// Token: 0x0400087A RID: 2170
		private static readonly char[] s_dotSeparators = new char[]
		{
			'.',
			'。',
			'．',
			'｡'
		};
	}
}
