using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace System.Reflection
{
	// Token: 0x020005C7 RID: 1479
	internal static class AssemblyNameFormatter
	{
		// Token: 0x06004BEA RID: 19434 RVA: 0x0018B21C File Offset: 0x0018A41C
		public static string ComputeDisplayName(string name, Version version, string cultureName, byte[] pkt, AssemblyNameFlags flags, AssemblyContentType contentType)
		{
			if (name == string.Empty)
			{
				throw new FileLoadException();
			}
			StringBuilder stringBuilder = new StringBuilder();
			if (name != null)
			{
				stringBuilder.AppendQuoted(name);
			}
			if (version != null)
			{
				Version version2 = version.CanonicalizeVersion();
				if (version2.Major != 65535)
				{
					stringBuilder.Append(", Version=");
					stringBuilder.Append(version2.Major);
					if (version2.Minor != 65535)
					{
						stringBuilder.Append('.');
						stringBuilder.Append(version2.Minor);
						if (version2.Build != 65535)
						{
							stringBuilder.Append('.');
							stringBuilder.Append(version2.Build);
							if (version2.Revision != 65535)
							{
								stringBuilder.Append('.');
								stringBuilder.Append(version2.Revision);
							}
						}
					}
				}
			}
			if (cultureName != null)
			{
				if (cultureName.Length == 0)
				{
					cultureName = "neutral";
				}
				stringBuilder.Append(", Culture=");
				stringBuilder.AppendQuoted(cultureName);
			}
			if (pkt != null)
			{
				if (pkt.Length > 8)
				{
					throw new ArgumentException();
				}
				stringBuilder.Append(", PublicKeyToken=");
				if (pkt.Length == 0)
				{
					stringBuilder.Append("null");
				}
				else
				{
					stringBuilder.Append(HexConverter.ToString(pkt, HexConverter.Casing.Lower));
				}
			}
			if ((flags & AssemblyNameFlags.Retargetable) != AssemblyNameFlags.None)
			{
				stringBuilder.Append(", Retargetable=Yes");
			}
			if (contentType == AssemblyContentType.WindowsRuntime)
			{
				stringBuilder.Append(", ContentType=WindowsRuntime");
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004BEB RID: 19435 RVA: 0x0018B388 File Offset: 0x0018A588
		private static void AppendQuoted(this StringBuilder sb, string s)
		{
			bool flag = false;
			if (s != s.Trim() || s.Contains('"') || s.Contains('\''))
			{
				flag = true;
			}
			if (flag)
			{
				sb.Append('"');
			}
			for (int i = 0; i < s.Length; i++)
			{
				bool flag2 = false;
				foreach (KeyValuePair<char, string> keyValuePair in AssemblyNameFormatter.EscapeSequences)
				{
					string value = keyValuePair.Value;
					if (s[i] == value[0] && s.Length - i >= value.Length && s.AsSpan(i, value.Length).SequenceEqual(value))
					{
						sb.Append('\\');
						sb.Append(keyValuePair.Key);
						flag2 = true;
					}
				}
				if (!flag2)
				{
					sb.Append(s[i]);
				}
			}
			if (flag)
			{
				sb.Append('"');
			}
		}

		// Token: 0x06004BEC RID: 19436 RVA: 0x0018B480 File Offset: 0x0018A680
		private static Version CanonicalizeVersion(this Version version)
		{
			ushort num = (ushort)version.Major;
			ushort num2 = (ushort)version.Minor;
			ushort num3 = (ushort)version.Build;
			ushort num4 = (ushort)version.Revision;
			if ((int)num == version.Major && (int)num2 == version.Minor && (int)num3 == version.Build && (int)num4 == version.Revision)
			{
				return version;
			}
			return new Version((int)num, (int)num2, (int)num3, (int)num4);
		}

		// Token: 0x040012E9 RID: 4841
		public static KeyValuePair<char, string>[] EscapeSequences = new KeyValuePair<char, string>[]
		{
			new KeyValuePair<char, string>('\\', "\\"),
			new KeyValuePair<char, string>(',', ","),
			new KeyValuePair<char, string>('=', "="),
			new KeyValuePair<char, string>('\'', "'"),
			new KeyValuePair<char, string>('"', "\""),
			new KeyValuePair<char, string>('n', "\r\n"),
			new KeyValuePair<char, string>('t', "\t")
		};
	}
}
