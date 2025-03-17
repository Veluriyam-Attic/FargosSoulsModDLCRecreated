using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200069E RID: 1694
	internal static class PathInternal
	{
		// Token: 0x06005632 RID: 22066 RVA: 0x001A74DB File Offset: 0x001A66DB
		internal unsafe static bool StartsWithDirectorySeparator(ReadOnlySpan<char> path)
		{
			return path.Length > 0 && PathInternal.IsDirectorySeparator((char)(*path[0]));
		}

		// Token: 0x06005633 RID: 22067 RVA: 0x001A74F7 File Offset: 0x001A66F7
		internal static bool IsRoot(ReadOnlySpan<char> path)
		{
			return path.Length == PathInternal.GetRootLength(path);
		}

		// Token: 0x06005634 RID: 22068 RVA: 0x001A7508 File Offset: 0x001A6708
		internal static int GetCommonPathLength(string first, string second, bool ignoreCase)
		{
			int num = PathInternal.EqualStartingCharacterCount(first, second, ignoreCase);
			if (num == 0)
			{
				return num;
			}
			if (num == first.Length && (num == second.Length || PathInternal.IsDirectorySeparator(second[num])))
			{
				return num;
			}
			if (num == second.Length && PathInternal.IsDirectorySeparator(first[num]))
			{
				return num;
			}
			while (num > 0 && !PathInternal.IsDirectorySeparator(first[num - 1]))
			{
				num--;
			}
			return num;
		}

		// Token: 0x06005635 RID: 22069 RVA: 0x001A7578 File Offset: 0x001A6778
		internal unsafe static int EqualStartingCharacterCount(string first, string second, bool ignoreCase)
		{
			if (string.IsNullOrEmpty(first) || string.IsNullOrEmpty(second))
			{
				return 0;
			}
			int num = 0;
			char* ptr;
			if (first == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = first.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			char* ptr4;
			if (second == null)
			{
				ptr4 = null;
			}
			else
			{
				fixed (char* ptr5 = second.GetPinnableReference())
				{
					ptr4 = ptr5;
				}
			}
			char* ptr6 = ptr4;
			char* ptr7 = ptr3;
			char* ptr8 = ptr6;
			char* ptr9 = ptr7 + first.Length;
			char* ptr10 = ptr8 + second.Length;
			while (ptr7 != ptr9 && ptr8 != ptr10 && (*ptr7 == *ptr8 || (ignoreCase && char.ToUpperInvariant(*ptr7) == char.ToUpperInvariant(*ptr8))))
			{
				num++;
				ptr7++;
				ptr8++;
			}
			char* ptr5 = null;
			char* ptr2 = null;
			return num;
		}

		// Token: 0x06005636 RID: 22070 RVA: 0x001A7624 File Offset: 0x001A6824
		internal static bool AreRootsEqual(string first, string second, StringComparison comparisonType)
		{
			int rootLength = PathInternal.GetRootLength(first.AsSpan());
			int rootLength2 = PathInternal.GetRootLength(second.AsSpan());
			return rootLength == rootLength2 && string.Compare(first, 0, second, 0, rootLength, comparisonType) == 0;
		}

		// Token: 0x06005637 RID: 22071 RVA: 0x001A7660 File Offset: 0x001A6860
		internal unsafe static string RemoveRelativeSegments(string path, int rootLength)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			if (PathInternal.RemoveRelativeSegments(path.AsSpan(), rootLength, ref valueStringBuilder))
			{
				path = valueStringBuilder.ToString();
			}
			valueStringBuilder.Dispose();
			return path;
		}

		// Token: 0x06005638 RID: 22072 RVA: 0x001A76B0 File Offset: 0x001A68B0
		internal unsafe static bool RemoveRelativeSegments(ReadOnlySpan<char> path, int rootLength, ref ValueStringBuilder sb)
		{
			bool flag = false;
			int num = rootLength;
			if (PathInternal.IsDirectorySeparator((char)(*path[num - 1])))
			{
				num--;
			}
			if (num > 0)
			{
				sb.Append(path.Slice(0, num));
			}
			int i = num;
			while (i < path.Length)
			{
				char c = (char)(*path[i]);
				if (!PathInternal.IsDirectorySeparator(c) || i + 1 >= path.Length)
				{
					goto IL_148;
				}
				if (!PathInternal.IsDirectorySeparator((char)(*path[i + 1])))
				{
					if ((i + 2 == path.Length || PathInternal.IsDirectorySeparator((char)(*path[i + 2]))) && *path[i + 1] == 46)
					{
						i++;
					}
					else
					{
						if (i + 2 >= path.Length || (i + 3 != path.Length && !PathInternal.IsDirectorySeparator((char)(*path[i + 3]))) || *path[i + 1] != 46 || *path[i + 2] != 46)
						{
							goto IL_148;
						}
						int j;
						for (j = sb.Length - 1; j >= num; j--)
						{
							if (PathInternal.IsDirectorySeparator(*sb[j]))
							{
								sb.Length = ((i + 3 >= path.Length && j == num) ? (j + 1) : j);
								break;
							}
						}
						if (j < num)
						{
							sb.Length = num;
						}
						i += 2;
					}
				}
				IL_15E:
				i++;
				continue;
				IL_148:
				if (c != '\\' && c == '/')
				{
					c = '\\';
					flag = true;
				}
				sb.Append(c);
				goto IL_15E;
			}
			if (!flag && sb.Length == path.Length)
			{
				return false;
			}
			if (num != rootLength && sb.Length < rootLength)
			{
				sb.Append((char)(*path[rootLength - 1]));
			}
			return true;
		}

		// Token: 0x06005639 RID: 22073 RVA: 0x001A785F File Offset: 0x001A6A5F
		[return: NotNullIfNotNull("path")]
		internal static string TrimEndingDirectorySeparator(string path)
		{
			if (!PathInternal.EndsInDirectorySeparator(path) || PathInternal.IsRoot(path.AsSpan()))
			{
				return path;
			}
			return path.Substring(0, path.Length - 1);
		}

		// Token: 0x0600563A RID: 22074 RVA: 0x001A7887 File Offset: 0x001A6A87
		internal static bool EndsInDirectorySeparator(string path)
		{
			return !string.IsNullOrEmpty(path) && PathInternal.IsDirectorySeparator(path[path.Length - 1]);
		}

		// Token: 0x0600563B RID: 22075 RVA: 0x001A78A6 File Offset: 0x001A6AA6
		internal static ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path)
		{
			if (!PathInternal.EndsInDirectorySeparator(path) || PathInternal.IsRoot(path))
			{
				return path;
			}
			return path.Slice(0, path.Length - 1);
		}

		// Token: 0x0600563C RID: 22076 RVA: 0x001A78CB File Offset: 0x001A6ACB
		internal unsafe static bool EndsInDirectorySeparator(ReadOnlySpan<char> path)
		{
			return path.Length > 0 && PathInternal.IsDirectorySeparator((char)(*path[path.Length - 1]));
		}

		// Token: 0x0600563D RID: 22077 RVA: 0x001A78EF File Offset: 0x001A6AEF
		internal static bool IsValidDriveChar(char value)
		{
			return (value >= 'A' && value <= 'Z') || (value >= 'a' && value <= 'z');
		}

		// Token: 0x0600563E RID: 22078 RVA: 0x001A790C File Offset: 0x001A6B0C
		internal static bool EndsWithPeriodOrSpace(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return false;
			}
			char c = path[path.Length - 1];
			return c == ' ' || c == '.';
		}

		// Token: 0x0600563F RID: 22079 RVA: 0x001A793E File Offset: 0x001A6B3E
		[return: NotNullIfNotNull("path")]
		internal static string EnsureExtendedPrefixIfNeeded(string path)
		{
			if (path != null && (path.Length >= 260 || PathInternal.EndsWithPeriodOrSpace(path)))
			{
				return PathInternal.EnsureExtendedPrefix(path);
			}
			return path;
		}

		// Token: 0x06005640 RID: 22080 RVA: 0x001A7960 File Offset: 0x001A6B60
		internal static string EnsureExtendedPrefix(string path)
		{
			if (PathInternal.IsPartiallyQualified(path.AsSpan()) || PathInternal.IsDevice(path.AsSpan()))
			{
				return path;
			}
			if (path.StartsWith("\\\\", StringComparison.OrdinalIgnoreCase))
			{
				return path.Insert(2, "?\\UNC\\");
			}
			return "\\\\?\\" + path;
		}

		// Token: 0x06005641 RID: 22081 RVA: 0x001A79B0 File Offset: 0x001A6BB0
		internal unsafe static bool IsDevice(ReadOnlySpan<char> path)
		{
			return PathInternal.IsExtended(path) || (path.Length >= 4 && PathInternal.IsDirectorySeparator((char)(*path[0])) && PathInternal.IsDirectorySeparator((char)(*path[1])) && (*path[2] == 46 || *path[2] == 63) && PathInternal.IsDirectorySeparator((char)(*path[3])));
		}

		// Token: 0x06005642 RID: 22082 RVA: 0x001A7A1C File Offset: 0x001A6C1C
		internal unsafe static bool IsDeviceUNC(ReadOnlySpan<char> path)
		{
			return path.Length >= 8 && PathInternal.IsDevice(path) && PathInternal.IsDirectorySeparator((char)(*path[7])) && *path[4] == 85 && *path[5] == 78 && *path[6] == 67;
		}

		// Token: 0x06005643 RID: 22083 RVA: 0x001A7A74 File Offset: 0x001A6C74
		internal unsafe static bool IsExtended(ReadOnlySpan<char> path)
		{
			return path.Length >= 4 && *path[0] == 92 && (*path[1] == 92 || *path[1] == 63) && *path[2] == 63 && *path[3] == 92;
		}

		// Token: 0x06005644 RID: 22084 RVA: 0x001A7AD0 File Offset: 0x001A6CD0
		internal unsafe static int GetRootLength(ReadOnlySpan<char> path)
		{
			int length = path.Length;
			int i = 0;
			bool flag = PathInternal.IsDevice(path);
			bool flag2 = flag && PathInternal.IsDeviceUNC(path);
			if ((!flag || flag2) && length > 0 && PathInternal.IsDirectorySeparator((char)(*path[0])))
			{
				if (flag2 || (length > 1 && PathInternal.IsDirectorySeparator((char)(*path[1]))))
				{
					i = (flag2 ? 8 : 2);
					int num = 2;
					while (i < length)
					{
						if (PathInternal.IsDirectorySeparator((char)(*path[i])) && --num <= 0)
						{
							break;
						}
						i++;
					}
				}
				else
				{
					i = 1;
				}
			}
			else if (flag)
			{
				i = 4;
				while (i < length && !PathInternal.IsDirectorySeparator((char)(*path[i])))
				{
					i++;
				}
				if (i < length && i > 4 && PathInternal.IsDirectorySeparator((char)(*path[i])))
				{
					i++;
				}
			}
			else if (length >= 2 && *path[1] == 58 && PathInternal.IsValidDriveChar((char)(*path[0])))
			{
				i = 2;
				if (length > 2 && PathInternal.IsDirectorySeparator((char)(*path[2])))
				{
					i++;
				}
			}
			return i;
		}

		// Token: 0x06005645 RID: 22085 RVA: 0x001A7BE0 File Offset: 0x001A6DE0
		internal unsafe static bool IsPartiallyQualified(ReadOnlySpan<char> path)
		{
			if (path.Length < 2)
			{
				return true;
			}
			if (PathInternal.IsDirectorySeparator((char)(*path[0])))
			{
				return *path[1] != 63 && !PathInternal.IsDirectorySeparator((char)(*path[1]));
			}
			return path.Length < 3 || *path[1] != 58 || !PathInternal.IsDirectorySeparator((char)(*path[2])) || !PathInternal.IsValidDriveChar((char)(*path[0]));
		}

		// Token: 0x06005646 RID: 22086 RVA: 0x001A7C64 File Offset: 0x001A6E64
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static bool IsDirectorySeparator(char c)
		{
			return c == '\\' || c == '/';
		}

		// Token: 0x06005647 RID: 22087 RVA: 0x001A7C74 File Offset: 0x001A6E74
		[return: NotNullIfNotNull("path")]
		internal unsafe static string NormalizeDirectorySeparators(string path)
		{
			if (string.IsNullOrEmpty(path))
			{
				return path;
			}
			bool flag = true;
			for (int i = 0; i < path.Length; i++)
			{
				char c = path[i];
				if (PathInternal.IsDirectorySeparator(c) && (c != '\\' || (i > 0 && i + 1 < path.Length && PathInternal.IsDirectorySeparator(path[i + 1]))))
				{
					flag = false;
					break;
				}
			}
			if (flag)
			{
				return path;
			}
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			int num = 0;
			if (PathInternal.IsDirectorySeparator(path[num]))
			{
				num++;
				valueStringBuilder.Append('\\');
			}
			int j = num;
			while (j < path.Length)
			{
				char c = path[j];
				if (!PathInternal.IsDirectorySeparator(c))
				{
					goto IL_D2;
				}
				if (j + 1 >= path.Length || !PathInternal.IsDirectorySeparator(path[j + 1]))
				{
					c = '\\';
					goto IL_D2;
				}
				IL_DA:
				j++;
				continue;
				IL_D2:
				valueStringBuilder.Append(c);
				goto IL_DA;
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x06005648 RID: 22088 RVA: 0x001A7D78 File Offset: 0x001A6F78
		internal unsafe static bool IsEffectivelyEmpty(ReadOnlySpan<char> path)
		{
			if (path.IsEmpty)
			{
				return true;
			}
			ReadOnlySpan<char> readOnlySpan = path;
			for (int i = 0; i < readOnlySpan.Length; i++)
			{
				char c = (char)(*readOnlySpan[i]);
				if (c != ' ')
				{
					return false;
				}
			}
			return true;
		}
	}
}
