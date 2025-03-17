using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;

namespace System.IO
{
	// Token: 0x0200069C RID: 1692
	public static class Path
	{
		// Token: 0x060055F2 RID: 22002 RVA: 0x001A5B3C File Offset: 0x001A4D3C
		[NullableContext(2)]
		[return: NotNullIfNotNull("path")]
		public static string ChangeExtension(string path, string extension)
		{
			if (path == null)
			{
				return null;
			}
			int num = path.Length;
			if (num == 0)
			{
				return string.Empty;
			}
			for (int i = path.Length - 1; i >= 0; i--)
			{
				char c = path[i];
				if (c == '.')
				{
					num = i;
					break;
				}
				if (PathInternal.IsDirectorySeparator(c))
				{
					break;
				}
			}
			if (extension == null)
			{
				return path.Substring(0, num);
			}
			ReadOnlySpan<char> str = path.AsSpan(0, num);
			if (!extension.StartsWith('.'))
			{
				return str + "." + extension;
			}
			return str + extension;
		}

		// Token: 0x060055F3 RID: 22003 RVA: 0x001A5BCC File Offset: 0x001A4DCC
		[NullableContext(2)]
		public static string GetDirectoryName(string path)
		{
			if (path == null || PathInternal.IsEffectivelyEmpty(path.AsSpan()))
			{
				return null;
			}
			int directoryNameOffset = Path.GetDirectoryNameOffset(path.AsSpan());
			if (directoryNameOffset < 0)
			{
				return null;
			}
			return PathInternal.NormalizeDirectorySeparators(path.Substring(0, directoryNameOffset));
		}

		// Token: 0x060055F4 RID: 22004 RVA: 0x001A5C0C File Offset: 0x001A4E0C
		public static ReadOnlySpan<char> GetDirectoryName(ReadOnlySpan<char> path)
		{
			if (PathInternal.IsEffectivelyEmpty(path))
			{
				return ReadOnlySpan<char>.Empty;
			}
			int directoryNameOffset = Path.GetDirectoryNameOffset(path);
			if (directoryNameOffset < 0)
			{
				return ReadOnlySpan<char>.Empty;
			}
			return path.Slice(0, directoryNameOffset);
		}

		// Token: 0x060055F5 RID: 22005 RVA: 0x001A5C44 File Offset: 0x001A4E44
		private unsafe static int GetDirectoryNameOffset(ReadOnlySpan<char> path)
		{
			int rootLength = PathInternal.GetRootLength(path);
			int i = path.Length;
			if (i <= rootLength)
			{
				return -1;
			}
			while (i > rootLength)
			{
				if (PathInternal.IsDirectorySeparator((char)(*path[--i])))
				{
					break;
				}
			}
			while (i > rootLength && PathInternal.IsDirectorySeparator((char)(*path[i - 1])))
			{
				i--;
			}
			return i;
		}

		// Token: 0x060055F6 RID: 22006 RVA: 0x001A5C9C File Offset: 0x001A4E9C
		[NullableContext(2)]
		[return: NotNullIfNotNull("path")]
		public static string GetExtension(string path)
		{
			if (path == null)
			{
				return null;
			}
			return Path.GetExtension(path.AsSpan()).ToString();
		}

		// Token: 0x060055F7 RID: 22007 RVA: 0x001A5CC8 File Offset: 0x001A4EC8
		public unsafe static ReadOnlySpan<char> GetExtension(ReadOnlySpan<char> path)
		{
			int length = path.Length;
			int i = length - 1;
			while (i >= 0)
			{
				char c = (char)(*path[i]);
				if (c == '.')
				{
					if (i != length - 1)
					{
						return path.Slice(i, length - i);
					}
					return ReadOnlySpan<char>.Empty;
				}
				else
				{
					if (PathInternal.IsDirectorySeparator(c))
					{
						break;
					}
					i--;
				}
			}
			return ReadOnlySpan<char>.Empty;
		}

		// Token: 0x060055F8 RID: 22008 RVA: 0x001A5D20 File Offset: 0x001A4F20
		[NullableContext(2)]
		[return: NotNullIfNotNull("path")]
		public static string GetFileName(string path)
		{
			if (path == null)
			{
				return null;
			}
			ReadOnlySpan<char> fileName = Path.GetFileName(path.AsSpan());
			if (path.Length == fileName.Length)
			{
				return path;
			}
			return fileName.ToString();
		}

		// Token: 0x060055F9 RID: 22009 RVA: 0x001A5D5C File Offset: 0x001A4F5C
		public unsafe static ReadOnlySpan<char> GetFileName(ReadOnlySpan<char> path)
		{
			int length = Path.GetPathRoot(path).Length;
			int num = path.Length;
			while (--num >= 0)
			{
				if (num < length || PathInternal.IsDirectorySeparator((char)(*path[num])))
				{
					return path.Slice(num + 1, path.Length - num - 1);
				}
			}
			return path;
		}

		// Token: 0x060055FA RID: 22010 RVA: 0x001A5DB8 File Offset: 0x001A4FB8
		[NullableContext(2)]
		[return: NotNullIfNotNull("path")]
		public static string GetFileNameWithoutExtension(string path)
		{
			if (path == null)
			{
				return null;
			}
			ReadOnlySpan<char> fileNameWithoutExtension = Path.GetFileNameWithoutExtension(path.AsSpan());
			if (path.Length == fileNameWithoutExtension.Length)
			{
				return path;
			}
			return fileNameWithoutExtension.ToString();
		}

		// Token: 0x060055FB RID: 22011 RVA: 0x001A5DF4 File Offset: 0x001A4FF4
		public static ReadOnlySpan<char> GetFileNameWithoutExtension(ReadOnlySpan<char> path)
		{
			ReadOnlySpan<char> fileName = Path.GetFileName(path);
			int num = fileName.LastIndexOf('.');
			if (num != -1)
			{
				return fileName.Slice(0, num);
			}
			return fileName;
		}

		// Token: 0x060055FC RID: 22012 RVA: 0x001A5E20 File Offset: 0x001A5020
		[NullableContext(1)]
		public unsafe static string GetRandomFileName()
		{
			byte* ptr = stackalloc byte[(UIntPtr)8];
			Interop.GetRandomBytes(ptr, 8);
			return string.Create<IntPtr>(12, (IntPtr)((void*)ptr), delegate(Span<char> span, IntPtr key)
			{
				Path.Populate83FileNameFromRandomBytes((byte*)((void*)key), 8, span);
			});
		}

		// Token: 0x060055FD RID: 22013 RVA: 0x001A5E65 File Offset: 0x001A5065
		[NullableContext(1)]
		public static bool IsPathFullyQualified(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			return Path.IsPathFullyQualified(path.AsSpan());
		}

		// Token: 0x060055FE RID: 22014 RVA: 0x001A5E80 File Offset: 0x001A5080
		public static bool IsPathFullyQualified(ReadOnlySpan<char> path)
		{
			return !PathInternal.IsPartiallyQualified(path);
		}

		// Token: 0x060055FF RID: 22015 RVA: 0x001A5E8B File Offset: 0x001A508B
		[NullableContext(2)]
		public static bool HasExtension(string path)
		{
			return path != null && Path.HasExtension(path.AsSpan());
		}

		// Token: 0x06005600 RID: 22016 RVA: 0x001A5EA0 File Offset: 0x001A50A0
		public unsafe static bool HasExtension(ReadOnlySpan<char> path)
		{
			for (int i = path.Length - 1; i >= 0; i--)
			{
				char c = (char)(*path[i]);
				if (c == '.')
				{
					return i != path.Length - 1;
				}
				if (PathInternal.IsDirectorySeparator(c))
				{
					break;
				}
			}
			return false;
		}

		// Token: 0x06005601 RID: 22017 RVA: 0x001A5EE9 File Offset: 0x001A50E9
		[NullableContext(1)]
		public static string Combine(string path1, string path2)
		{
			if (path1 == null || path2 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : "path2");
			}
			return Path.CombineInternal(path1, path2);
		}

		// Token: 0x06005602 RID: 22018 RVA: 0x001A5F0D File Offset: 0x001A510D
		[NullableContext(1)]
		public static string Combine(string path1, string path2, string path3)
		{
			if (path1 == null || path2 == null || path3 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : ((path2 == null) ? "path2" : "path3"));
			}
			return Path.CombineInternal(path1, path2, path3);
		}

		// Token: 0x06005603 RID: 22019 RVA: 0x001A5F3F File Offset: 0x001A513F
		[NullableContext(1)]
		public static string Combine(string path1, string path2, string path3, string path4)
		{
			if (path1 == null || path2 == null || path3 == null || path4 == null)
			{
				throw new ArgumentNullException((path1 == null) ? "path1" : ((path2 == null) ? "path2" : ((path3 == null) ? "path3" : "path4")));
			}
			return Path.CombineInternal(path1, path2, path3, path4);
		}

		// Token: 0x06005604 RID: 22020 RVA: 0x001A5F80 File Offset: 0x001A5180
		[NullableContext(1)]
		public unsafe static string Combine(params string[] paths)
		{
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			int num = 0;
			int num2 = 0;
			for (int i = 0; i < paths.Length; i++)
			{
				if (paths[i] == null)
				{
					throw new ArgumentNullException("paths");
				}
				if (paths[i].Length != 0)
				{
					if (Path.IsPathRooted(paths[i]))
					{
						num2 = i;
						num = paths[i].Length;
					}
					else
					{
						num += paths[i].Length;
					}
					char c = paths[i][paths[i].Length - 1];
					if (!PathInternal.IsDirectorySeparator(c))
					{
						num++;
					}
				}
			}
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			valueStringBuilder.EnsureCapacity(num);
			for (int j = num2; j < paths.Length; j++)
			{
				if (paths[j].Length != 0)
				{
					if (valueStringBuilder.Length == 0)
					{
						valueStringBuilder.Append(paths[j]);
					}
					else
					{
						char c2 = *valueStringBuilder[valueStringBuilder.Length - 1];
						if (!PathInternal.IsDirectorySeparator(c2))
						{
							valueStringBuilder.Append('\\');
						}
						valueStringBuilder.Append(paths[j]);
					}
				}
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x06005605 RID: 22021 RVA: 0x001A609F File Offset: 0x001A529F
		[return: Nullable(1)]
		public static string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2)
		{
			if (path1.Length == 0)
			{
				return path2.ToString();
			}
			if (path2.Length == 0)
			{
				return path1.ToString();
			}
			return Path.JoinInternal(path1, path2);
		}

		// Token: 0x06005606 RID: 22022 RVA: 0x001A60D6 File Offset: 0x001A52D6
		[return: Nullable(1)]
		public static string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3)
		{
			if (path1.Length == 0)
			{
				return Path.Join(path2, path3);
			}
			if (path2.Length == 0)
			{
				return Path.Join(path1, path3);
			}
			if (path3.Length == 0)
			{
				return Path.Join(path1, path2);
			}
			return Path.JoinInternal(path1, path2, path3);
		}

		// Token: 0x06005607 RID: 22023 RVA: 0x001A6114 File Offset: 0x001A5314
		[return: Nullable(1)]
		public static string Join(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, ReadOnlySpan<char> path4)
		{
			if (path1.Length == 0)
			{
				return Path.Join(path2, path3, path4);
			}
			if (path2.Length == 0)
			{
				return Path.Join(path1, path3, path4);
			}
			if (path3.Length == 0)
			{
				return Path.Join(path1, path2, path4);
			}
			if (path4.Length == 0)
			{
				return Path.Join(path1, path2, path3);
			}
			return Path.JoinInternal(path1, path2, path3, path4);
		}

		// Token: 0x06005608 RID: 22024 RVA: 0x001A6172 File Offset: 0x001A5372
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Join(string path1, string path2)
		{
			return Path.Join(path1.AsSpan(), path2.AsSpan());
		}

		// Token: 0x06005609 RID: 22025 RVA: 0x001A6185 File Offset: 0x001A5385
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Join(string path1, string path2, string path3)
		{
			return Path.Join(path1.AsSpan(), path2.AsSpan(), path3.AsSpan());
		}

		// Token: 0x0600560A RID: 22026 RVA: 0x001A619E File Offset: 0x001A539E
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string Join(string path1, string path2, string path3, string path4)
		{
			return Path.Join(path1.AsSpan(), path2.AsSpan(), path3.AsSpan(), path4.AsSpan());
		}

		// Token: 0x0600560B RID: 22027 RVA: 0x001A61C0 File Offset: 0x001A53C0
		[NullableContext(1)]
		public unsafe static string Join([Nullable(new byte[]
		{
			1,
			2
		})] params string[] paths)
		{
			if (paths == null)
			{
				throw new ArgumentNullException("paths");
			}
			if (paths.Length == 0)
			{
				return string.Empty;
			}
			int num = 0;
			foreach (string text in paths)
			{
				num += ((text != null) ? text.Length : 0);
			}
			num += paths.Length - 1;
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			valueStringBuilder.EnsureCapacity(num);
			foreach (string text2 in paths)
			{
				if (!string.IsNullOrEmpty(text2))
				{
					if (valueStringBuilder.Length == 0)
					{
						valueStringBuilder.Append(text2);
					}
					else
					{
						if (!PathInternal.IsDirectorySeparator(*valueStringBuilder[valueStringBuilder.Length - 1]) && !PathInternal.IsDirectorySeparator(text2[0]))
						{
							valueStringBuilder.Append('\\');
						}
						valueStringBuilder.Append(text2);
					}
				}
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x0600560C RID: 22028 RVA: 0x001A62B8 File Offset: 0x001A54B8
		public unsafe static bool TryJoin(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, Span<char> destination, out int charsWritten)
		{
			charsWritten = 0;
			if (path1.Length == 0 && path2.Length == 0)
			{
				return true;
			}
			if (path1.Length == 0 || path2.Length == 0)
			{
				ref ReadOnlySpan<char> ptr = ref (path1.Length == 0) ? ref path2 : ref path1;
				if (destination.Length < ptr.Length)
				{
					return false;
				}
				ptr.CopyTo(destination);
				charsWritten = ptr.Length;
				return true;
			}
			else
			{
				bool flag = !Path.EndsInDirectorySeparator(path1) && !PathInternal.StartsWithDirectorySeparator(path2);
				int num = path1.Length + path2.Length + (flag ? 1 : 0);
				if (destination.Length < num)
				{
					return false;
				}
				path1.CopyTo(destination);
				if (flag)
				{
					*destination[path1.Length] = Path.DirectorySeparatorChar;
				}
				path2.CopyTo(destination.Slice(path1.Length + (flag ? 1 : 0)));
				charsWritten = num;
				return true;
			}
		}

		// Token: 0x0600560D RID: 22029 RVA: 0x001A639C File Offset: 0x001A559C
		public unsafe static bool TryJoin(ReadOnlySpan<char> path1, ReadOnlySpan<char> path2, ReadOnlySpan<char> path3, Span<char> destination, out int charsWritten)
		{
			charsWritten = 0;
			if (path1.Length == 0 && path2.Length == 0 && path3.Length == 0)
			{
				return true;
			}
			if (path1.Length == 0)
			{
				return Path.TryJoin(path2, path3, destination, out charsWritten);
			}
			if (path2.Length == 0)
			{
				return Path.TryJoin(path1, path3, destination, out charsWritten);
			}
			if (path3.Length == 0)
			{
				return Path.TryJoin(path1, path2, destination, out charsWritten);
			}
			int num = (Path.EndsInDirectorySeparator(path1) || PathInternal.StartsWithDirectorySeparator(path2)) ? 0 : 1;
			bool flag = !Path.EndsInDirectorySeparator(path2) && !PathInternal.StartsWithDirectorySeparator(path3);
			if (flag)
			{
				num++;
			}
			int num2 = path1.Length + path2.Length + path3.Length + num;
			if (destination.Length < num2)
			{
				return false;
			}
			bool flag2 = Path.TryJoin(path1, path2, destination, out charsWritten);
			if (flag)
			{
				int num3 = charsWritten;
				charsWritten = num3 + 1;
				*destination[num3] = Path.DirectorySeparatorChar;
			}
			path3.CopyTo(destination.Slice(charsWritten));
			charsWritten += path3.Length;
			return true;
		}

		// Token: 0x0600560E RID: 22030 RVA: 0x001A64A6 File Offset: 0x001A56A6
		private static string CombineInternal(string first, string second)
		{
			if (string.IsNullOrEmpty(first))
			{
				return second;
			}
			if (string.IsNullOrEmpty(second))
			{
				return first;
			}
			if (Path.IsPathRooted(second.AsSpan()))
			{
				return second;
			}
			return Path.JoinInternal(first.AsSpan(), second.AsSpan());
		}

		// Token: 0x0600560F RID: 22031 RVA: 0x001A64DC File Offset: 0x001A56DC
		private static string CombineInternal(string first, string second, string third)
		{
			if (string.IsNullOrEmpty(first))
			{
				return Path.CombineInternal(second, third);
			}
			if (string.IsNullOrEmpty(second))
			{
				return Path.CombineInternal(first, third);
			}
			if (string.IsNullOrEmpty(third))
			{
				return Path.CombineInternal(first, second);
			}
			if (Path.IsPathRooted(third.AsSpan()))
			{
				return third;
			}
			if (Path.IsPathRooted(second.AsSpan()))
			{
				return Path.CombineInternal(second, third);
			}
			return Path.JoinInternal(first.AsSpan(), second.AsSpan(), third.AsSpan());
		}

		// Token: 0x06005610 RID: 22032 RVA: 0x001A6554 File Offset: 0x001A5754
		private static string CombineInternal(string first, string second, string third, string fourth)
		{
			if (string.IsNullOrEmpty(first))
			{
				return Path.CombineInternal(second, third, fourth);
			}
			if (string.IsNullOrEmpty(second))
			{
				return Path.CombineInternal(first, third, fourth);
			}
			if (string.IsNullOrEmpty(third))
			{
				return Path.CombineInternal(first, second, fourth);
			}
			if (string.IsNullOrEmpty(fourth))
			{
				return Path.CombineInternal(first, second, third);
			}
			if (Path.IsPathRooted(fourth.AsSpan()))
			{
				return fourth;
			}
			if (Path.IsPathRooted(third.AsSpan()))
			{
				return Path.CombineInternal(third, fourth);
			}
			if (Path.IsPathRooted(second.AsSpan()))
			{
				return Path.CombineInternal(second, third, fourth);
			}
			return Path.JoinInternal(first.AsSpan(), second.AsSpan(), third.AsSpan(), fourth.AsSpan());
		}

		// Token: 0x06005611 RID: 22033 RVA: 0x001A65FC File Offset: 0x001A57FC
		private unsafe static string JoinInternal(ReadOnlySpan<char> first, ReadOnlySpan<char> second)
		{
			bool flag = PathInternal.IsDirectorySeparator((char)(*first[first.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*second[0]));
			fixed (char* reference = MemoryMarshal.GetReference<char>(first))
			{
				char* value = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(second))
				{
					char* value2 = reference2;
					return string.Create<ValueTuple<IntPtr, int, IntPtr, int, bool>>(first.Length + second.Length + (flag ? 0 : 1), new ValueTuple<IntPtr, int, IntPtr, int, bool>((IntPtr)((void*)value), first.Length, (IntPtr)((void*)value2), second.Length, flag), delegate(Span<char> destination, [TupleElementNames(new string[]
					{
						"First",
						"FirstLength",
						"Second",
						"SecondLength",
						"HasSeparator"
					})] ValueTuple<IntPtr, int, IntPtr, int, bool> state)
					{
						Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
						span.CopyTo(destination);
						if (!state.Item5)
						{
							*destination[state.Item2] = '\\';
						}
						span = new Span<char>((void*)state.Item3, state.Item4);
						span.CopyTo(destination.Slice(state.Item2 + (state.Item5 ? 0 : 1)));
					});
				}
			}
		}

		// Token: 0x06005612 RID: 22034 RVA: 0x001A66A4 File Offset: 0x001A58A4
		private unsafe static string JoinInternal(ReadOnlySpan<char> first, ReadOnlySpan<char> second, ReadOnlySpan<char> third)
		{
			bool flag = PathInternal.IsDirectorySeparator((char)(*first[first.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*second[0]));
			bool flag2 = PathInternal.IsDirectorySeparator((char)(*second[second.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*third[0]));
			fixed (char* reference = MemoryMarshal.GetReference<char>(first))
			{
				char* value = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(second))
				{
					char* value2 = reference2;
					fixed (char* reference3 = MemoryMarshal.GetReference<char>(third))
					{
						char* value3 = reference3;
						return string.Create<ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>>>(first.Length + second.Length + third.Length + (flag ? 0 : 1) + (flag2 ? 0 : 1), new ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>>((IntPtr)((void*)value), first.Length, (IntPtr)((void*)value2), second.Length, (IntPtr)((void*)value3), third.Length, flag, new ValueTuple<bool>(flag2)), delegate(Span<char> destination, [TupleElementNames(new string[]
						{
							"First",
							"FirstLength",
							"Second",
							"SecondLength",
							"Third",
							"ThirdLength",
							"FirstHasSeparator",
							"ThirdHasSeparator",
							null
						})] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, bool, ValueTuple<bool>> state)
						{
							Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
							span.CopyTo(destination);
							if (!state.Item7)
							{
								*destination[state.Item2] = '\\';
							}
							span = new Span<char>((void*)state.Item3, state.Item4);
							span.CopyTo(destination.Slice(state.Item2 + (state.Item7 ? 0 : 1)));
							if (!state.Rest.Item1)
							{
								*destination[destination.Length - state.Item6 - 1] = '\\';
							}
							span = new Span<char>((void*)state.Item5, state.Item6);
							span.CopyTo(destination.Slice(destination.Length - state.Item6));
						});
					}
				}
			}
		}

		// Token: 0x06005613 RID: 22035 RVA: 0x001A67AC File Offset: 0x001A59AC
		private unsafe static string JoinInternal(ReadOnlySpan<char> first, ReadOnlySpan<char> second, ReadOnlySpan<char> third, ReadOnlySpan<char> fourth)
		{
			bool flag = PathInternal.IsDirectorySeparator((char)(*first[first.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*second[0]));
			bool flag2 = PathInternal.IsDirectorySeparator((char)(*second[second.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*third[0]));
			bool flag3 = PathInternal.IsDirectorySeparator((char)(*third[third.Length - 1])) || PathInternal.IsDirectorySeparator((char)(*fourth[0]));
			fixed (char* reference = MemoryMarshal.GetReference<char>(first))
			{
				char* value = reference;
				fixed (char* reference2 = MemoryMarshal.GetReference<char>(second))
				{
					char* value2 = reference2;
					fixed (char* reference3 = MemoryMarshal.GetReference<char>(third))
					{
						char* value3 = reference3;
						fixed (char* reference4 = MemoryMarshal.GetReference<char>(fourth))
						{
							char* value4 = reference4;
							return string.Create<ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>>>(first.Length + second.Length + third.Length + fourth.Length + (flag ? 0 : 1) + (flag2 ? 0 : 1) + (flag3 ? 0 : 1), new ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>>((IntPtr)((void*)value), first.Length, (IntPtr)((void*)value2), second.Length, (IntPtr)((void*)value3), third.Length, (IntPtr)((void*)value4), new ValueTuple<int, bool, bool, bool>(fourth.Length, flag, flag2, flag3)), delegate(Span<char> destination, [TupleElementNames(new string[]
							{
								"First",
								"FirstLength",
								"Second",
								"SecondLength",
								"Third",
								"ThirdLength",
								"Fourth",
								"FourthLength",
								"FirstHasSeparator",
								"ThirdHasSeparator",
								"FourthHasSeparator",
								null,
								null,
								null,
								null
							})] ValueTuple<IntPtr, int, IntPtr, int, IntPtr, int, IntPtr, ValueTuple<int, bool, bool, bool>> state)
							{
								Span<char> span = new Span<char>((void*)state.Item1, state.Item2);
								span.CopyTo(destination);
								if (!state.Rest.Item2)
								{
									*destination[state.Item2] = '\\';
								}
								span = new Span<char>((void*)state.Item3, state.Item4);
								span.CopyTo(destination.Slice(state.Item2 + (state.Rest.Item2 ? 0 : 1)));
								if (!state.Rest.Item3)
								{
									*destination[state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1)] = '\\';
								}
								span = new Span<char>((void*)state.Item5, state.Item6);
								span.CopyTo(destination.Slice(state.Item2 + state.Item4 + (state.Rest.Item2 ? 0 : 1) + (state.Rest.Item3 ? 0 : 1)));
								if (!state.Rest.Item4)
								{
									*destination[destination.Length - state.Rest.Item1 - 1] = '\\';
								}
								span = new Span<char>((void*)state.Item7, state.Rest.Item1);
								span.CopyTo(destination.Slice(destination.Length - state.Rest.Item1));
							});
						}
					}
				}
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06005614 RID: 22036 RVA: 0x001A6909 File Offset: 0x001A5B09
		private unsafe static ReadOnlySpan<byte> Base32Char
		{
			get
			{
				return new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.653BB1245E828FCDA4FA53FCD5A3DEF5BD7654E651F54B4132B73D74E64435C4), 32);
			}
		}

		// Token: 0x06005615 RID: 22037 RVA: 0x001A6918 File Offset: 0x001A5B18
		private unsafe static void Populate83FileNameFromRandomBytes(byte* bytes, int byteCount, Span<char> chars)
		{
			byte b = *bytes;
			byte b2 = bytes[1];
			byte b3 = bytes[2];
			byte b4 = bytes[3];
			byte b5 = bytes[4];
			*chars[11] = (char)(*Path.Base32Char[(int)(bytes[7] & 31)]);
			*chars[0] = (char)(*Path.Base32Char[(int)(b & 31)]);
			*chars[1] = (char)(*Path.Base32Char[(int)(b2 & 31)]);
			*chars[2] = (char)(*Path.Base32Char[(int)(b3 & 31)]);
			*chars[3] = (char)(*Path.Base32Char[(int)(b4 & 31)]);
			*chars[4] = (char)(*Path.Base32Char[(int)(b5 & 31)]);
			*chars[5] = (char)(*Path.Base32Char[(b & 224) >> 5 | (b4 & 96) >> 2]);
			*chars[6] = (char)(*Path.Base32Char[(b2 & 224) >> 5 | (b5 & 96) >> 2]);
			b3 = (byte)(b3 >> 5);
			if ((b4 & 128) != 0)
			{
				b3 |= 8;
			}
			if ((b5 & 128) != 0)
			{
				b3 |= 16;
			}
			*chars[7] = (char)(*Path.Base32Char[(int)b3]);
			*chars[8] = '.';
			*chars[9] = (char)(*Path.Base32Char[(int)(bytes[5] & 31)]);
			*chars[10] = (char)(*Path.Base32Char[(int)(bytes[6] & 31)]);
		}

		// Token: 0x06005616 RID: 22038 RVA: 0x001A6AC2 File Offset: 0x001A5CC2
		[NullableContext(1)]
		public static string GetRelativePath(string relativeTo, string path)
		{
			return Path.GetRelativePath(relativeTo, path, Path.StringComparison);
		}

		// Token: 0x06005617 RID: 22039 RVA: 0x001A6AD0 File Offset: 0x001A5CD0
		private unsafe static string GetRelativePath(string relativeTo, string path, StringComparison comparisonType)
		{
			if (relativeTo == null)
			{
				throw new ArgumentNullException("relativeTo");
			}
			if (PathInternal.IsEffectivelyEmpty(relativeTo.AsSpan()))
			{
				throw new ArgumentException(SR.Arg_PathEmpty, "relativeTo");
			}
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (PathInternal.IsEffectivelyEmpty(path.AsSpan()))
			{
				throw new ArgumentException(SR.Arg_PathEmpty, "path");
			}
			relativeTo = Path.GetFullPath(relativeTo);
			path = Path.GetFullPath(path);
			if (!PathInternal.AreRootsEqual(relativeTo, path, comparisonType))
			{
				return path;
			}
			int num = PathInternal.GetCommonPathLength(relativeTo, path, comparisonType == StringComparison.OrdinalIgnoreCase);
			if (num == 0)
			{
				return path;
			}
			int num2 = relativeTo.Length;
			if (Path.EndsInDirectorySeparator(relativeTo.AsSpan()))
			{
				num2--;
			}
			bool flag = Path.EndsInDirectorySeparator(path.AsSpan());
			int num3 = path.Length;
			if (flag)
			{
				num3--;
			}
			if (num2 == num3 && num >= num2)
			{
				return ".";
			}
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			valueStringBuilder.EnsureCapacity(Math.Max(relativeTo.Length, path.Length));
			if (num < num2)
			{
				valueStringBuilder.Append("..");
				for (int i = num + 1; i < num2; i++)
				{
					if (PathInternal.IsDirectorySeparator(relativeTo[i]))
					{
						valueStringBuilder.Append(Path.DirectorySeparatorChar);
						valueStringBuilder.Append("..");
					}
				}
			}
			else if (PathInternal.IsDirectorySeparator(path[num]))
			{
				num++;
			}
			int num4 = num3 - num;
			if (flag)
			{
				num4++;
			}
			if (num4 > 0)
			{
				if (valueStringBuilder.Length > 0)
				{
					valueStringBuilder.Append(Path.DirectorySeparatorChar);
				}
				valueStringBuilder.Append(path.AsSpan(num, num4));
			}
			return valueStringBuilder.ToString();
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06005618 RID: 22040 RVA: 0x001A6C78 File Offset: 0x001A5E78
		internal static StringComparison StringComparison
		{
			get
			{
				bool isCaseSensitive = Path.IsCaseSensitive;
				return StringComparison.OrdinalIgnoreCase;
			}
		}

		// Token: 0x06005619 RID: 22041 RVA: 0x001A6C81 File Offset: 0x001A5E81
		[NullableContext(1)]
		public static string TrimEndingDirectorySeparator(string path)
		{
			return PathInternal.TrimEndingDirectorySeparator(path);
		}

		// Token: 0x0600561A RID: 22042 RVA: 0x001A6C89 File Offset: 0x001A5E89
		public static ReadOnlySpan<char> TrimEndingDirectorySeparator(ReadOnlySpan<char> path)
		{
			return PathInternal.TrimEndingDirectorySeparator(path);
		}

		// Token: 0x0600561B RID: 22043 RVA: 0x001A6C91 File Offset: 0x001A5E91
		public static bool EndsInDirectorySeparator(ReadOnlySpan<char> path)
		{
			return PathInternal.EndsInDirectorySeparator(path);
		}

		// Token: 0x0600561C RID: 22044 RVA: 0x001A6C99 File Offset: 0x001A5E99
		[NullableContext(1)]
		public static bool EndsInDirectorySeparator(string path)
		{
			return PathInternal.EndsInDirectorySeparator(path);
		}

		// Token: 0x0600561D RID: 22045 RVA: 0x001A6CA1 File Offset: 0x001A5EA1
		[NullableContext(1)]
		public static char[] GetInvalidFileNameChars()
		{
			return new char[]
			{
				'"',
				'<',
				'>',
				'|',
				'\0',
				'\u0001',
				'\u0002',
				'\u0003',
				'\u0004',
				'\u0005',
				'\u0006',
				'\a',
				'\b',
				'\t',
				'\n',
				'\v',
				'\f',
				'\r',
				'\u000e',
				'\u000f',
				'\u0010',
				'\u0011',
				'\u0012',
				'\u0013',
				'\u0014',
				'\u0015',
				'\u0016',
				'\u0017',
				'\u0018',
				'\u0019',
				'\u001a',
				'\u001b',
				'\u001c',
				'\u001d',
				'\u001e',
				'\u001f',
				':',
				'*',
				'?',
				'\\',
				'/'
			};
		}

		// Token: 0x0600561E RID: 22046 RVA: 0x001A6CB5 File Offset: 0x001A5EB5
		[NullableContext(1)]
		public static char[] GetInvalidPathChars()
		{
			return new char[]
			{
				'|',
				'\0',
				'\u0001',
				'\u0002',
				'\u0003',
				'\u0004',
				'\u0005',
				'\u0006',
				'\a',
				'\b',
				'\t',
				'\n',
				'\v',
				'\f',
				'\r',
				'\u000e',
				'\u000f',
				'\u0010',
				'\u0011',
				'\u0012',
				'\u0013',
				'\u0014',
				'\u0015',
				'\u0016',
				'\u0017',
				'\u0018',
				'\u0019',
				'\u001a',
				'\u001b',
				'\u001c',
				'\u001d',
				'\u001e',
				'\u001f'
			};
		}

		// Token: 0x0600561F RID: 22047 RVA: 0x001A6CCC File Offset: 0x001A5ECC
		[NullableContext(1)]
		public static string GetFullPath(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (PathInternal.IsEffectivelyEmpty(path.AsSpan()))
			{
				throw new ArgumentException(SR.Arg_PathEmpty, "path");
			}
			if (path.Contains('\0'))
			{
				throw new ArgumentException(SR.Argument_InvalidPathChars, "path");
			}
			if (PathInternal.IsExtended(path.AsSpan()))
			{
				return path;
			}
			return PathHelper.Normalize(path);
		}

		// Token: 0x06005620 RID: 22048 RVA: 0x001A6D34 File Offset: 0x001A5F34
		[NullableContext(1)]
		public static string GetFullPath(string path, string basePath)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (basePath == null)
			{
				throw new ArgumentNullException("basePath");
			}
			if (!Path.IsPathFullyQualified(basePath))
			{
				throw new ArgumentException(SR.Arg_BasePathNotFullyQualified, "basePath");
			}
			if (basePath.Contains('\0') || path.Contains('\0'))
			{
				throw new ArgumentException(SR.Argument_InvalidPathChars);
			}
			if (Path.IsPathFullyQualified(path))
			{
				return Path.GetFullPath(path);
			}
			if (PathInternal.IsEffectivelyEmpty(path.AsSpan()))
			{
				return basePath;
			}
			int length = path.Length;
			string text;
			if (length >= 1 && PathInternal.IsDirectorySeparator(path[0]))
			{
				text = Path.Join(Path.GetPathRoot(basePath.AsSpan()), path.AsSpan(1));
			}
			else if (length >= 2 && PathInternal.IsValidDriveChar(path[0]) && path[1] == ':')
			{
				if (Path.GetVolumeName(path.AsSpan()).EqualsOrdinal(Path.GetVolumeName(basePath.AsSpan())))
				{
					text = Path.Join(basePath.AsSpan(), path.AsSpan(2));
				}
				else
				{
					text = ((!PathInternal.IsDevice(basePath.AsSpan())) ? path.Insert(2, "\\") : ((length == 2) ? Path.JoinInternal(basePath.AsSpan(0, 4), path.AsSpan(), "\\".AsSpan()) : Path.JoinInternal(basePath.AsSpan(0, 4), path.AsSpan(0, 2), "\\".AsSpan(), path.AsSpan(2))));
				}
			}
			else
			{
				text = Path.JoinInternal(basePath.AsSpan(), path.AsSpan());
			}
			if (!PathInternal.IsDevice(text.AsSpan()))
			{
				return Path.GetFullPath(text);
			}
			return PathInternal.RemoveRelativeSegments(text, PathInternal.GetRootLength(text.AsSpan()));
		}

		// Token: 0x06005621 RID: 22049 RVA: 0x001A6EDC File Offset: 0x001A60DC
		[NullableContext(1)]
		public unsafe static string GetTempPath()
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			Path.GetTempPath(ref valueStringBuilder);
			string result = PathHelper.Normalize(ref valueStringBuilder);
			valueStringBuilder.Dispose();
			return result;
		}

		// Token: 0x06005622 RID: 22050 RVA: 0x001A6F1C File Offset: 0x001A611C
		private static void GetTempPath(ref ValueStringBuilder builder)
		{
			uint tempPathW;
			while ((ulong)(tempPathW = Interop.Kernel32.GetTempPathW(builder.Capacity, builder.GetPinnableReference())) > (ulong)((long)builder.Capacity))
			{
				builder.EnsureCapacity(checked((int)tempPathW));
			}
			if (tempPathW == 0U)
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
			builder.Length = (int)tempPathW;
		}

		// Token: 0x06005623 RID: 22051 RVA: 0x001A6F68 File Offset: 0x001A6168
		[NullableContext(1)]
		public unsafe static string GetTempFileName()
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			Path.GetTempPath(ref valueStringBuilder);
			initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
			ValueStringBuilder valueStringBuilder2 = new ValueStringBuilder(initialBuffer);
			uint tempFileNameW = Interop.Kernel32.GetTempFileNameW(valueStringBuilder.GetPinnableReference(), "tmp", 0U, valueStringBuilder2.GetPinnableReference());
			valueStringBuilder.Dispose();
			if (tempFileNameW == 0U)
			{
				throw Win32Marshal.GetExceptionForLastWin32Error("");
			}
			valueStringBuilder2.Length = valueStringBuilder2.RawChars.IndexOf('\0');
			string result = PathHelper.Normalize(ref valueStringBuilder2);
			valueStringBuilder2.Dispose();
			return result;
		}

		// Token: 0x06005624 RID: 22052 RVA: 0x001A7009 File Offset: 0x001A6209
		[NullableContext(2)]
		public static bool IsPathRooted([NotNullWhen(true)] string path)
		{
			return path != null && Path.IsPathRooted(path.AsSpan());
		}

		// Token: 0x06005625 RID: 22053 RVA: 0x001A701C File Offset: 0x001A621C
		public unsafe static bool IsPathRooted(ReadOnlySpan<char> path)
		{
			int length = path.Length;
			return (length >= 1 && PathInternal.IsDirectorySeparator((char)(*path[0]))) || (length >= 2 && PathInternal.IsValidDriveChar((char)(*path[0])) && *path[1] == 58);
		}

		// Token: 0x06005626 RID: 22054 RVA: 0x001A706C File Offset: 0x001A626C
		[NullableContext(2)]
		public static string GetPathRoot(string path)
		{
			if (PathInternal.IsEffectivelyEmpty(path.AsSpan()))
			{
				return null;
			}
			ReadOnlySpan<char> pathRoot = Path.GetPathRoot(path.AsSpan());
			if (path.Length == pathRoot.Length)
			{
				return PathInternal.NormalizeDirectorySeparators(path);
			}
			return PathInternal.NormalizeDirectorySeparators(pathRoot.ToString());
		}

		// Token: 0x06005627 RID: 22055 RVA: 0x001A70BC File Offset: 0x001A62BC
		public static ReadOnlySpan<char> GetPathRoot(ReadOnlySpan<char> path)
		{
			if (PathInternal.IsEffectivelyEmpty(path))
			{
				return ReadOnlySpan<char>.Empty;
			}
			int rootLength = PathInternal.GetRootLength(path);
			if (rootLength > 0)
			{
				return path.Slice(0, rootLength);
			}
			return ReadOnlySpan<char>.Empty;
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06005628 RID: 22056 RVA: 0x000AC09B File Offset: 0x000AB29B
		internal static bool IsCaseSensitive
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06005629 RID: 22057 RVA: 0x001A70F4 File Offset: 0x001A62F4
		internal static ReadOnlySpan<char> GetVolumeName(ReadOnlySpan<char> path)
		{
			ReadOnlySpan<char> pathRoot = Path.GetPathRoot(path);
			if (pathRoot.Length == 0)
			{
				return pathRoot;
			}
			int num = Path.GetUncRootLength(path);
			if (num == -1)
			{
				if (PathInternal.IsDevice(path))
				{
					num = 4;
				}
				else
				{
					num = 0;
				}
			}
			ReadOnlySpan<char> readOnlySpan = pathRoot.Slice(num);
			if (!Path.EndsInDirectorySeparator(readOnlySpan))
			{
				return readOnlySpan;
			}
			return readOnlySpan.Slice(0, readOnlySpan.Length - 1);
		}

		// Token: 0x0600562A RID: 22058 RVA: 0x001A7150 File Offset: 0x001A6350
		internal static int GetUncRootLength(ReadOnlySpan<char> path)
		{
			bool flag = PathInternal.IsDevice(path);
			if (!flag && path.Slice(0, 2).EqualsOrdinal("\\\\".AsSpan()))
			{
				return 2;
			}
			if (flag && path.Length >= 8 && (path.Slice(0, 8).EqualsOrdinal("\\\\?\\UNC\\".AsSpan()) || path.Slice(5, 4).EqualsOrdinal("UNC\\".AsSpan())))
			{
				return 8;
			}
			return -1;
		}

		// Token: 0x0400186D RID: 6253
		public static readonly char DirectorySeparatorChar = '\\';

		// Token: 0x0400186E RID: 6254
		public static readonly char AltDirectorySeparatorChar = '/';

		// Token: 0x0400186F RID: 6255
		public static readonly char VolumeSeparatorChar = ':';

		// Token: 0x04001870 RID: 6256
		public static readonly char PathSeparator = ';';

		// Token: 0x04001871 RID: 6257
		[Obsolete("Please use GetInvalidPathChars or GetInvalidFileNameChars instead.")]
		[Nullable(1)]
		public static readonly char[] InvalidPathChars = Path.GetInvalidPathChars();
	}
}
