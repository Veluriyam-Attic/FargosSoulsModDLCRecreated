using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;

namespace System.Text
{
	// Token: 0x02000375 RID: 885
	internal static class EncodingTable
	{
		// Token: 0x06002EB4 RID: 11956 RVA: 0x0015D814 File Offset: 0x0015CA14
		internal static int GetCodePageFromName(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			object obj = EncodingTable.s_nameToCodePage[name];
			if (obj != null)
			{
				return (int)obj;
			}
			int num = EncodingTable.InternalGetCodePageFromName(name);
			EncodingTable.s_nameToCodePage[name] = num;
			return num;
		}

		// Token: 0x06002EB5 RID: 11957 RVA: 0x0015D860 File Offset: 0x0015CA60
		private static int InternalGetCodePageFromName(string name)
		{
			int i = 0;
			int num = EncodingTable.s_encodingNameIndices.Length - 2;
			ReadOnlySpan<char> strA = name.ToLowerInvariant().AsSpan();
			while (num - i > 3)
			{
				int num2 = (num - i) / 2 + i;
				int num3 = string.CompareOrdinal(strA, "ansi_x3.4-1968ansi_x3.4-1986asciicp367cp819csasciicsisolatin1csunicode11utf7ibm367ibm819iso-10646-ucs-2iso-8859-1iso-ir-100iso-ir-6iso646-usiso8859-1iso_646.irv:1991iso_8859-1iso_8859-1:1987l1latin1ucs-2unicodeunicode-1-1-utf-7unicode-1-1-utf-8unicode-2-0-utf-7unicode-2-0-utf-8unicodefffeusus-asciiutf-16utf-16beutf-16leutf-32utf-32beutf-32leutf-7utf-8x-unicode-1-1-utf-7x-unicode-1-1-utf-8x-unicode-2-0-utf-7x-unicode-2-0-utf-8".AsSpan(EncodingTable.s_encodingNameIndices[num2], EncodingTable.s_encodingNameIndices[num2 + 1] - EncodingTable.s_encodingNameIndices[num2]));
				if (num3 == 0)
				{
					return (int)EncodingTable.s_codePagesByName[num2];
				}
				if (num3 < 0)
				{
					num = num2;
				}
				else
				{
					i = num2;
				}
			}
			while (i <= num)
			{
				if (string.CompareOrdinal(strA, "ansi_x3.4-1968ansi_x3.4-1986asciicp367cp819csasciicsisolatin1csunicode11utf7ibm367ibm819iso-10646-ucs-2iso-8859-1iso-ir-100iso-ir-6iso646-usiso8859-1iso_646.irv:1991iso_8859-1iso_8859-1:1987l1latin1ucs-2unicodeunicode-1-1-utf-7unicode-1-1-utf-8unicode-2-0-utf-7unicode-2-0-utf-8unicodefffeusus-asciiutf-16utf-16beutf-16leutf-32utf-32beutf-32leutf-7utf-8x-unicode-1-1-utf-7x-unicode-1-1-utf-8x-unicode-2-0-utf-7x-unicode-2-0-utf-8".AsSpan(EncodingTable.s_encodingNameIndices[i], EncodingTable.s_encodingNameIndices[i + 1] - EncodingTable.s_encodingNameIndices[i])) == 0)
				{
					return (int)EncodingTable.s_codePagesByName[i];
				}
				i++;
			}
			throw new ArgumentException(SR.Format(SR.Argument_EncodingNotSupported, name), "name");
		}

		// Token: 0x06002EB6 RID: 11958 RVA: 0x0015D928 File Offset: 0x0015CB28
		internal static EncodingInfo[] GetEncodings()
		{
			ushort[] array = EncodingTable.s_mappedCodePages;
			EncodingInfo[] array2 = new EncodingInfo[LocalAppContextSwitches.EnableUnsafeUTF7Encoding ? array.Length : (array.Length - 1)];
			string text = "utf-16utf-16BEutf-32utf-32BEus-asciiiso-8859-1utf-7utf-8";
			int[] array3 = EncodingTable.s_webNameIndices;
			int num = 0;
			for (int i = 0; i < array.Length; i++)
			{
				int num2 = (int)array[i];
				if (num2 != 65000 || LocalAppContextSwitches.EnableUnsafeUTF7Encoding)
				{
					EncodingInfo[] array4 = array2;
					int num3 = num++;
					int codePage = num2;
					string text2 = text;
					int num4 = array3[i];
					int length = array3[i + 1] - num4;
					array4[num3] = new EncodingInfo(codePage, text2.Substring(num4, length), EncodingTable.GetDisplayName(num2, i));
				}
			}
			return array2;
		}

		// Token: 0x06002EB7 RID: 11959 RVA: 0x0015D9C0 File Offset: 0x0015CBC0
		internal static EncodingInfo[] GetEncodings(Dictionary<int, EncodingInfo> encodingInfoList)
		{
			ushort[] array = EncodingTable.s_mappedCodePages;
			string text = "utf-16utf-16BEutf-32utf-32BEus-asciiiso-8859-1utf-7utf-8";
			int[] array2 = EncodingTable.s_webNameIndices;
			for (int i = 0; i < array.Length; i++)
			{
				int num = (int)array[i];
				if (!encodingInfoList.ContainsKey(num) && (num != 65000 || LocalAppContextSwitches.EnableUnsafeUTF7Encoding))
				{
					int key = num;
					int codePage = num;
					string text2 = text;
					int num2 = array2[i];
					int length = array2[i + 1] - num2;
					encodingInfoList[key] = new EncodingInfo(codePage, text2.Substring(num2, length), EncodingTable.GetDisplayName(num, i));
				}
			}
			if (!LocalAppContextSwitches.EnableUnsafeUTF7Encoding)
			{
				encodingInfoList.Remove(65000);
			}
			EncodingInfo[] array3 = new EncodingInfo[encodingInfoList.Count];
			int num3 = 0;
			foreach (KeyValuePair<int, EncodingInfo> keyValuePair in encodingInfoList)
			{
				array3[num3++] = keyValuePair.Value;
			}
			return array3;
		}

		// Token: 0x06002EB8 RID: 11960 RVA: 0x0015DAB4 File Offset: 0x0015CCB4
		internal static CodePageDataItem GetCodePageDataItem(int codePage)
		{
			if (EncodingTable.s_codePageToCodePageData == null)
			{
				Interlocked.CompareExchange<CodePageDataItem[]>(ref EncodingTable.s_codePageToCodePageData, new CodePageDataItem[EncodingTable.s_mappedCodePages.Length], null);
			}
			int num;
			if (codePage <= 12001)
			{
				if (codePage <= 1201)
				{
					if (codePage == 1200)
					{
						num = 0;
						goto IL_A1;
					}
					if (codePage == 1201)
					{
						num = 1;
						goto IL_A1;
					}
				}
				else
				{
					if (codePage == 12000)
					{
						num = 2;
						goto IL_A1;
					}
					if (codePage == 12001)
					{
						num = 3;
						goto IL_A1;
					}
				}
			}
			else if (codePage <= 28591)
			{
				if (codePage == 20127)
				{
					num = 4;
					goto IL_A1;
				}
				if (codePage == 28591)
				{
					num = 5;
					goto IL_A1;
				}
			}
			else
			{
				if (codePage == 65000)
				{
					num = 6;
					goto IL_A1;
				}
				if (codePage == 65001)
				{
					num = 7;
					goto IL_A1;
				}
			}
			return null;
			IL_A1:
			CodePageDataItem codePageDataItem = EncodingTable.s_codePageToCodePageData[num];
			if (codePageDataItem == null)
			{
				Interlocked.CompareExchange<CodePageDataItem>(ref EncodingTable.s_codePageToCodePageData[num], EncodingTable.InternalGetCodePageDataItem(codePage, num), null);
				codePageDataItem = EncodingTable.s_codePageToCodePageData[num];
			}
			return codePageDataItem;
		}

		// Token: 0x06002EB9 RID: 11961 RVA: 0x0015DB90 File Offset: 0x0015CD90
		private static CodePageDataItem InternalGetCodePageDataItem(int codePage, int index)
		{
			int uiFamilyCodePage = EncodingTable.s_uiFamilyCodePages[index];
			string text = "utf-16utf-16BEutf-32utf-32BEus-asciiiso-8859-1utf-7utf-8";
			int num = EncodingTable.s_webNameIndices[index];
			int length = EncodingTable.s_webNameIndices[index + 1] - num;
			string text2 = text.Substring(num, length);
			string headerName = text2;
			string bodyName = text2;
			string displayName = EncodingTable.GetDisplayName(codePage, index);
			uint flags = EncodingTable.s_flags[index];
			return new CodePageDataItem(uiFamilyCodePage, text2, headerName, bodyName, displayName, flags);
		}

		// Token: 0x06002EBA RID: 11962 RVA: 0x0015DBF0 File Offset: 0x0015CDF0
		private static string GetDisplayName(int codePage, int englishNameIndex)
		{
			string text = SR.GetResourceString("Globalization_cp_" + codePage.ToString());
			if (string.IsNullOrEmpty(text))
			{
				string text2 = "UnicodeUnicode (Big-Endian)Unicode (UTF-32)Unicode (UTF-32 Big-Endian)US-ASCIIWestern European (ISO)Unicode (UTF-7)Unicode (UTF-8)";
				int num = EncodingTable.s_englishNameIndices[englishNameIndex];
				int length = EncodingTable.s_englishNameIndices[englishNameIndex + 1] - num;
				text = text2.Substring(num, length);
			}
			return text;
		}

		// Token: 0x04000CDB RID: 3291
		private static readonly int[] s_encodingNameIndices = new int[]
		{
			0,
			14,
			28,
			33,
			38,
			43,
			50,
			61,
			76,
			82,
			88,
			103,
			113,
			123,
			131,
			140,
			149,
			165,
			175,
			190,
			192,
			198,
			203,
			210,
			227,
			244,
			261,
			278,
			289,
			291,
			299,
			305,
			313,
			321,
			327,
			335,
			343,
			348,
			353,
			372,
			391,
			410,
			429
		};

		// Token: 0x04000CDC RID: 3292
		private static readonly ushort[] s_codePagesByName = new ushort[]
		{
			20127,
			20127,
			20127,
			20127,
			28591,
			20127,
			28591,
			65000,
			20127,
			28591,
			1200,
			28591,
			28591,
			20127,
			20127,
			28591,
			20127,
			28591,
			28591,
			28591,
			28591,
			1200,
			1200,
			65000,
			65001,
			65000,
			65001,
			1201,
			20127,
			20127,
			1200,
			1201,
			1200,
			12000,
			12001,
			12000,
			65000,
			65001,
			65000,
			65001,
			65000,
			65001
		};

		// Token: 0x04000CDD RID: 3293
		private static readonly ushort[] s_mappedCodePages = new ushort[]
		{
			1200,
			1201,
			12000,
			12001,
			20127,
			28591,
			65000,
			65001
		};

		// Token: 0x04000CDE RID: 3294
		private static readonly int[] s_uiFamilyCodePages = new int[]
		{
			1200,
			1200,
			1200,
			1200,
			1252,
			1252,
			1200,
			1200
		};

		// Token: 0x04000CDF RID: 3295
		private static readonly int[] s_webNameIndices = new int[]
		{
			0,
			6,
			14,
			20,
			28,
			36,
			46,
			51,
			56
		};

		// Token: 0x04000CE0 RID: 3296
		private static readonly int[] s_englishNameIndices = new int[]
		{
			0,
			7,
			27,
			43,
			70,
			78,
			100,
			115,
			130
		};

		// Token: 0x04000CE1 RID: 3297
		private static readonly uint[] s_flags = new uint[]
		{
			512U,
			0U,
			0U,
			0U,
			257U,
			771U,
			257U,
			771U
		};

		// Token: 0x04000CE2 RID: 3298
		private static readonly Hashtable s_nameToCodePage = Hashtable.Synchronized(new Hashtable(StringComparer.OrdinalIgnoreCase));

		// Token: 0x04000CE3 RID: 3299
		private static CodePageDataItem[] s_codePageToCodePageData;
	}
}
