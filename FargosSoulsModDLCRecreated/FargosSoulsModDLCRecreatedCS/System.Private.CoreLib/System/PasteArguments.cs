using System;
using System.Collections.Generic;
using System.Text;

namespace System
{
	// Token: 0x0200016D RID: 365
	internal static class PasteArguments
	{
		// Token: 0x06001276 RID: 4726 RVA: 0x000E7670 File Offset: 0x000E6870
		internal static void AppendArgument(StringBuilder stringBuilder, string argument)
		{
			if (stringBuilder.Length != 0)
			{
				stringBuilder.Append(' ');
			}
			if (argument.Length != 0 && PasteArguments.ContainsNoWhitespaceOrQuotes(argument))
			{
				stringBuilder.Append(argument);
				return;
			}
			stringBuilder.Append('"');
			int i = 0;
			while (i < argument.Length)
			{
				char c = argument[i++];
				if (c == '\\')
				{
					int num = 1;
					while (i < argument.Length && argument[i] == '\\')
					{
						i++;
						num++;
					}
					if (i == argument.Length)
					{
						stringBuilder.Append('\\', num * 2);
					}
					else if (argument[i] == '"')
					{
						stringBuilder.Append('\\', num * 2 + 1);
						stringBuilder.Append('"');
						i++;
					}
					else
					{
						stringBuilder.Append('\\', num);
					}
				}
				else if (c == '"')
				{
					stringBuilder.Append('\\');
					stringBuilder.Append('"');
				}
				else
				{
					stringBuilder.Append(c);
				}
			}
			stringBuilder.Append('"');
		}

		// Token: 0x06001277 RID: 4727 RVA: 0x000E776C File Offset: 0x000E696C
		private static bool ContainsNoWhitespaceOrQuotes(string s)
		{
			foreach (char c in s)
			{
				if (char.IsWhiteSpace(c) || c == '"')
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06001278 RID: 4728 RVA: 0x000E77A4 File Offset: 0x000E69A4
		internal static string Paste(IEnumerable<string> arguments, bool pasteFirstArgumentUsingArgV0Rules)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (string text in arguments)
			{
				if (pasteFirstArgumentUsingArgV0Rules)
				{
					pasteFirstArgumentUsingArgV0Rules = false;
					bool flag = false;
					foreach (char c in text)
					{
						if (c == '"')
						{
							throw new ApplicationException("The argv[0] argument cannot include a double quote.");
						}
						if (char.IsWhiteSpace(c))
						{
							flag = true;
						}
					}
					if (text.Length == 0 || flag)
					{
						stringBuilder.Append('"');
						stringBuilder.Append(text);
						stringBuilder.Append('"');
					}
					else
					{
						stringBuilder.Append(text);
					}
				}
				else
				{
					PasteArguments.AppendArgument(stringBuilder, text);
				}
			}
			return stringBuilder.ToString();
		}
	}
}
