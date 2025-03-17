using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using Microsoft.Win32.SafeHandles;

namespace Internal.IO
{
	// Token: 0x02000816 RID: 2070
	internal static class File
	{
		// Token: 0x06006251 RID: 25169 RVA: 0x001D3D44 File Offset: 0x001D2F44
		public static bool Exists(string path)
		{
			try
			{
				if (path == null)
				{
					return false;
				}
				if (path.Length == 0)
				{
					return false;
				}
				path = Path.GetFullPath(path);
				if (path.Length > 0)
				{
					string text = path;
					int index = text.Length - 1;
					if (PathInternal.IsDirectorySeparator(text[index]))
					{
						return false;
					}
				}
				return File.InternalExists(path);
			}
			catch (ArgumentException)
			{
			}
			catch (NotSupportedException)
			{
			}
			catch (SecurityException)
			{
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			return false;
		}

		// Token: 0x06006252 RID: 25170 RVA: 0x001D3DEC File Offset: 0x001D2FEC
		internal static bool InternalExists(string fullPath)
		{
			Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA win32_FILE_ATTRIBUTE_DATA = default(Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA);
			return File.FillAttributeInfo(fullPath, ref win32_FILE_ATTRIBUTE_DATA, true) == 0 && win32_FILE_ATTRIBUTE_DATA.dwFileAttributes != -1 && (win32_FILE_ATTRIBUTE_DATA.dwFileAttributes & 16) == 0;
		}

		// Token: 0x06006253 RID: 25171 RVA: 0x001D3E28 File Offset: 0x001D3028
		internal static int FillAttributeInfo(string path, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA data, bool returnErrorOnNotFound)
		{
			int num = 0;
			using (DisableMediaInsertionPrompt.Create())
			{
				if (!Interop.Kernel32.GetFileAttributesEx(path, Interop.Kernel32.GET_FILEEX_INFO_LEVELS.GetFileExInfoStandard, ref data))
				{
					num = Marshal.GetLastWin32Error();
					if (num == 5)
					{
						Interop.Kernel32.WIN32_FIND_DATA win32_FIND_DATA = default(Interop.Kernel32.WIN32_FIND_DATA);
						using (SafeFindHandle safeFindHandle = Interop.Kernel32.FindFirstFile(path, ref win32_FIND_DATA))
						{
							if (safeFindHandle.IsInvalid)
							{
								num = Marshal.GetLastWin32Error();
							}
							else
							{
								num = 0;
								data.PopulateFrom(ref win32_FIND_DATA);
							}
						}
					}
				}
			}
			if (num != 0 && !returnErrorOnNotFound && (num - 2 <= 1 || num == 21))
			{
				data.dwFileAttributes = -1;
				return 0;
			}
			return num;
		}
	}
}
