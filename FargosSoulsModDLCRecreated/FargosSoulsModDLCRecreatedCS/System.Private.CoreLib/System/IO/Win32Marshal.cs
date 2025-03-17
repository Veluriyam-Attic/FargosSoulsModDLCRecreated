using System;
using System.Runtime.InteropServices;

namespace System.IO
{
	// Token: 0x020006C3 RID: 1731
	internal static class Win32Marshal
	{
		// Token: 0x0600583D RID: 22589 RVA: 0x001AFB6B File Offset: 0x001AED6B
		internal static Exception GetExceptionForLastWin32Error(string path = "")
		{
			return Win32Marshal.GetExceptionForWin32Error(Marshal.GetLastWin32Error(), path);
		}

		// Token: 0x0600583E RID: 22590 RVA: 0x001AFB78 File Offset: 0x001AED78
		internal static Exception GetExceptionForWin32Error(int errorCode, string path = "")
		{
			if (errorCode <= 80)
			{
				switch (errorCode)
				{
				case 2:
					return new FileNotFoundException(string.IsNullOrEmpty(path) ? SR.IO_FileNotFound : SR.Format(SR.IO_FileNotFound_FileName, path), path);
				case 3:
					return new DirectoryNotFoundException(string.IsNullOrEmpty(path) ? SR.IO_PathNotFound_NoPathName : SR.Format(SR.IO_PathNotFound_Path, path));
				case 4:
					break;
				case 5:
					return new UnauthorizedAccessException(string.IsNullOrEmpty(path) ? SR.UnauthorizedAccess_IODenied_NoPathName : SR.Format(SR.UnauthorizedAccess_IODenied_Path, path));
				default:
					if (errorCode == 32)
					{
						return new IOException(string.IsNullOrEmpty(path) ? SR.IO_SharingViolation_NoFileName : SR.Format(SR.IO_SharingViolation_File, path), Win32Marshal.MakeHRFromErrorCode(errorCode));
					}
					if (errorCode == 80)
					{
						if (!string.IsNullOrEmpty(path))
						{
							return new IOException(SR.Format(SR.IO_FileExists_Name, path), Win32Marshal.MakeHRFromErrorCode(errorCode));
						}
					}
					break;
				}
			}
			else if (errorCode <= 183)
			{
				if (errorCode != 87)
				{
					if (errorCode == 183)
					{
						if (!string.IsNullOrEmpty(path))
						{
							return new IOException(SR.Format(SR.IO_AlreadyExists_Name, path), Win32Marshal.MakeHRFromErrorCode(errorCode));
						}
					}
				}
			}
			else
			{
				if (errorCode == 206)
				{
					return new PathTooLongException(string.IsNullOrEmpty(path) ? SR.IO_PathTooLong : SR.Format(SR.IO_PathTooLong_Path, path));
				}
				if (errorCode == 995)
				{
					return new OperationCanceledException();
				}
			}
			return new IOException(string.IsNullOrEmpty(path) ? Win32Marshal.GetMessage(errorCode) : (Win32Marshal.GetMessage(errorCode) + " : '" + path + "'"), Win32Marshal.MakeHRFromErrorCode(errorCode));
		}

		// Token: 0x0600583F RID: 22591 RVA: 0x001AFD11 File Offset: 0x001AEF11
		internal static int MakeHRFromErrorCode(int errorCode)
		{
			if (((ulong)-65536 & (ulong)((long)errorCode)) != 0UL)
			{
				return errorCode;
			}
			return -2147024896 | errorCode;
		}

		// Token: 0x06005840 RID: 22592 RVA: 0x001AFD27 File Offset: 0x001AEF27
		internal static string GetMessage(int errorCode)
		{
			return Interop.Kernel32.GetMessage(errorCode);
		}
	}
}
