using System;
using System.Buffers;
using System.Diagnostics.Tracing;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Internal.Win32.SafeHandles;
using Microsoft.Win32.SafeHandles;

// Token: 0x02000008 RID: 8
internal static class Interop
{
	// Token: 0x06000008 RID: 8 RVA: 0x000AAA64 File Offset: 0x000A9C64
	internal unsafe static bool CallStringMethod<TArg1, TArg2, TArg3>(SpanFunc<char, TArg1, TArg2, TArg3, Interop.Globalization.ResultCode> interopCall, TArg1 arg1, TArg2 arg2, TArg3 arg3, out string result)
	{
		Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
		Span<char> span2 = span;
		Interop.Globalization.ResultCode resultCode = interopCall(span2, arg1, arg2, arg3);
		if (resultCode == Interop.Globalization.ResultCode.Success)
		{
			result = span2.Slice(0, span2.IndexOf('\0')).ToString();
			return true;
		}
		if (resultCode == Interop.Globalization.ResultCode.InsufficentBuffer)
		{
			span2 = new char[1280];
			if (interopCall(span2, arg1, arg2, arg3) == Interop.Globalization.ResultCode.Success)
			{
				result = span2.Slice(0, span2.IndexOf('\0')).ToString();
				return true;
			}
		}
		result = null;
		return false;
	}

	// Token: 0x06000009 RID: 9 RVA: 0x000AAB00 File Offset: 0x000A9D00
	internal unsafe static void GetRandomBytes(byte* buffer, int length)
	{
		Interop.BCrypt.NTSTATUS ntstatus = Interop.BCrypt.BCryptGenRandom(IntPtr.Zero, buffer, length, 2);
		if (ntstatus == Interop.BCrypt.NTSTATUS.STATUS_SUCCESS)
		{
			return;
		}
		if (ntstatus == (Interop.BCrypt.NTSTATUS)3221225495U)
		{
			throw new OutOfMemoryException();
		}
		throw new InvalidOperationException();
	}

	// Token: 0x02000009 RID: 9
	internal static class Kernel32
	{
		// Token: 0x0600000A RID: 10
		[SuppressGCTransition]
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetStdHandle(int nStdHandle);

		// Token: 0x0600000B RID: 11
		[DllImport("kernel32.dll")]
		internal static extern IntPtr LocalAlloc(uint uFlags, UIntPtr uBytes);

		// Token: 0x0600000C RID: 12
		[DllImport("kernel32.dll")]
		internal static extern IntPtr LocalReAlloc(IntPtr hMem, IntPtr uBytes, uint uFlags);

		// Token: 0x0600000D RID: 13
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern IntPtr LocalFree(IntPtr hMem);

		// Token: 0x0600000E RID: 14
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern int LCIDToLocaleName(int locale, char* pLocaleName, int cchName, uint dwFlags);

		// Token: 0x0600000F RID: 15
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int LocaleNameToLCID(string lpName, uint dwFlags);

		// Token: 0x06000010 RID: 16
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal unsafe static extern int LCMapStringEx(string lpLocaleName, uint dwMapFlags, char* lpSrcStr, int cchSrc, void* lpDestStr, int cchDest, void* lpVersionInformation, void* lpReserved, IntPtr sortHandle);

		// Token: 0x06000011 RID: 17
		[DllImport("kernel32.dll")]
		internal unsafe static extern int FindNLSStringEx(char* lpLocaleName, uint dwFindNLSStringFlags, char* lpStringSource, int cchSource, char* lpStringValue, int cchValue, int* pcchFound, void* lpVersionInformation, void* lpReserved, IntPtr sortHandle);

		// Token: 0x06000012 RID: 18
		[DllImport("kernel32.dll")]
		internal unsafe static extern int CompareStringEx(char* lpLocaleName, uint dwCmpFlags, char* lpString1, int cchCount1, char* lpString2, int cchCount2, void* lpVersionInformation, void* lpReserved, IntPtr lParam);

		// Token: 0x06000013 RID: 19
		[DllImport("kernel32.dll")]
		internal unsafe static extern int CompareStringOrdinal(char* lpString1, int cchCount1, char* lpString2, int cchCount2, bool bIgnoreCase);

		// Token: 0x06000014 RID: 20
		[DllImport("kernel32.dll")]
		internal unsafe static extern int FindStringOrdinal(uint dwFindStringOrdinalFlags, char* lpStringSource, int cchSource, char* lpStringValue, int cchValue, Interop.BOOL bIgnoreCase);

		// Token: 0x06000015 RID: 21
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern bool IsNLSDefinedString(int Function, uint dwFlags, IntPtr lpVersionInformation, char* lpString, int cchStr);

		// Token: 0x06000016 RID: 22
		[DllImport("kernel32.dll", CharSet = CharSet.Auto)]
		internal unsafe static extern Interop.BOOL GetUserPreferredUILanguages(uint dwFlags, uint* pulNumLanguages, char* pwszLanguagesBuffer, uint* pcchLanguagesBuffer);

		// Token: 0x06000017 RID: 23
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern int GetLocaleInfoEx(string lpLocaleName, uint LCType, void* lpLCData, int cchData);

		// Token: 0x06000018 RID: 24
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern bool EnumSystemLocalesEx(method lpLocaleEnumProcEx, char dwFlags, Interop.BOOL lParam, char* reserved);

		// Token: 0x06000019 RID: 25
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern bool EnumTimeFormatsEx(method lpTimeFmtEnumProcEx, bool lpLocaleName, Interop.BOOL dwFlags, char* lParam);

		// Token: 0x0600001A RID: 26
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetCalendarInfoEx(string lpLocaleName, uint Calendar, IntPtr lpReserved, uint CalType, IntPtr lpCalData, int cchData, out int lpValue);

		// Token: 0x0600001B RID: 27
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal static extern int GetCalendarInfoEx(string lpLocaleName, uint Calendar, IntPtr lpReserved, uint CalType, IntPtr lpCalData, int cchData, IntPtr lpValue);

		// Token: 0x0600001C RID: 28
		[DllImport("kernel32.dll")]
		internal static extern int GetUserGeoID(int geoClass);

		// Token: 0x0600001D RID: 29
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern int GetGeoInfo(int location, int geoType, char* lpGeoData, int cchData, int LangId);

		// Token: 0x0600001E RID: 30
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern bool EnumCalendarInfoExEx(method pCalInfoEnumProcExEx, sbyte lpLocaleName, Interop.BOOL Calendar, char* lpReserved, uint CalType, IntPtr lParam);

		// Token: 0x0600001F RID: 31
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern bool GetNLSVersionEx(int function, string localeName, Interop.Kernel32.NlsVersionInfoEx* lpVersionInformation);

		// Token: 0x06000020 RID: 32
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern int ResolveLocaleName(string lpNameToResolve, char* lpLocaleName, int cchLocaleName);

		// Token: 0x06000021 RID: 33
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern bool CancelIoEx(SafeHandle handle, NativeOverlapped* lpOverlapped);

		// Token: 0x06000022 RID: 34
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "CreateFileW", ExactSpelling = true, SetLastError = true)]
		private unsafe static extern SafeFileHandle CreateFilePrivate(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, Interop.Kernel32.SECURITY_ATTRIBUTES* lpSecurityAttributes, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

		// Token: 0x06000023 RID: 35 RVA: 0x000AAB32 File Offset: 0x000A9D32
		internal unsafe static SafeFileHandle CreateFile(string lpFileName, int dwDesiredAccess, FileShare dwShareMode, Interop.Kernel32.SECURITY_ATTRIBUTES* lpSecurityAttributes, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile)
		{
			lpFileName = PathInternal.EnsureExtendedPrefixIfNeeded(lpFileName);
			return Interop.Kernel32.CreateFilePrivate(lpFileName, dwDesiredAccess, dwShareMode, lpSecurityAttributes, dwCreationDisposition, dwFlagsAndAttributes, hTemplateFile);
		}

		// Token: 0x06000024 RID: 36
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "ExpandEnvironmentStringsW", ExactSpelling = true, SetLastError = true)]
		internal static extern uint ExpandEnvironmentStrings(string lpSrc, ref char lpDst, uint nSize);

		// Token: 0x06000025 RID: 37
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool FindClose(IntPtr hFindFile);

		// Token: 0x06000026 RID: 38
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "FindFirstFileExW", ExactSpelling = true, SetLastError = true)]
		private static extern SafeFindHandle FindFirstFileExPrivate(string lpFileName, Interop.Kernel32.FINDEX_INFO_LEVELS fInfoLevelId, ref Interop.Kernel32.WIN32_FIND_DATA lpFindFileData, Interop.Kernel32.FINDEX_SEARCH_OPS fSearchOp, IntPtr lpSearchFilter, int dwAdditionalFlags);

		// Token: 0x06000027 RID: 39 RVA: 0x000AAB4B File Offset: 0x000A9D4B
		internal static SafeFindHandle FindFirstFile(string fileName, ref Interop.Kernel32.WIN32_FIND_DATA data)
		{
			fileName = PathInternal.EnsureExtendedPrefixIfNeeded(fileName);
			return Interop.Kernel32.FindFirstFileExPrivate(fileName, Interop.Kernel32.FINDEX_INFO_LEVELS.FindExInfoBasic, ref data, Interop.Kernel32.FINDEX_SEARCH_OPS.FindExSearchNameMatch, IntPtr.Zero, 0);
		}

		// Token: 0x06000028 RID: 40
		[DllImport("kernel32.dll", SetLastError = true)]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool FlushFileBuffers(SafeHandle hHandle);

		// Token: 0x06000029 RID: 41
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool FreeLibrary(IntPtr hModule);

		// Token: 0x0600002A RID: 42
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetComputerNameW", ExactSpelling = true)]
		private static extern int GetComputerName(ref char lpBuffer, ref uint nSize);

		// Token: 0x0600002B RID: 43 RVA: 0x000AAB64 File Offset: 0x000A9D64
		internal unsafe static string GetComputerName()
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)32], 16);
			Span<char> span2 = span;
			uint length = (uint)span2.Length;
			if (Interop.Kernel32.GetComputerName(MemoryMarshal.GetReference<char>(span2), ref length) == 0)
			{
				return null;
			}
			return span2.Slice(0, (int)length).ToString();
		}

		// Token: 0x0600002C RID: 44
		[DllImport("kernel32.dll")]
		internal unsafe static extern Interop.BOOL GetCPInfo(uint codePage, Interop.Kernel32.CPINFO* lpCpInfo);

		// Token: 0x0600002D RID: 45
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetCurrentDirectoryW", ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetCurrentDirectory(uint nBufferLength, ref char lpBuffer);

		// Token: 0x0600002E RID: 46
		[DllImport("kernel32.dll")]
		internal static extern IntPtr GetCurrentProcess();

		// Token: 0x0600002F RID: 47
		[DllImport("kernel32.dll")]
		internal static extern uint GetCurrentProcessId();

		// Token: 0x06000030 RID: 48
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetFileAttributesExW", ExactSpelling = true, SetLastError = true)]
		private static extern bool GetFileAttributesExPrivate(string name, Interop.Kernel32.GET_FILEEX_INFO_LEVELS fileInfoLevel, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA lpFileInformation);

		// Token: 0x06000031 RID: 49 RVA: 0x000AABB0 File Offset: 0x000A9DB0
		internal static bool GetFileAttributesEx(string name, Interop.Kernel32.GET_FILEEX_INFO_LEVELS fileInfoLevel, ref Interop.Kernel32.WIN32_FILE_ATTRIBUTE_DATA lpFileInformation)
		{
			name = PathInternal.EnsureExtendedPrefixIfNeeded(name);
			return Interop.Kernel32.GetFileAttributesExPrivate(name, fileInfoLevel, ref lpFileInformation);
		}

		// Token: 0x06000032 RID: 50
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern bool GetFileInformationByHandleEx(SafeFileHandle hFile, int FileInformationClass, void* lpFileInformation, uint dwBufferSize);

		// Token: 0x06000033 RID: 51
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetFileType(SafeHandle hFile);

		// Token: 0x06000034 RID: 52
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetFullPathNameW(ref char lpFileName, uint nBufferLength, ref char lpBuffer, IntPtr lpFilePart);

		// Token: 0x06000035 RID: 53
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern int GetLogicalDrives();

		// Token: 0x06000036 RID: 54
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetLongPathNameW(ref char lpszShortPath, ref char lpszLongPath, uint cchBuffer);

		// Token: 0x06000037 RID: 55
		[DllImport("kernel32.dll", EntryPoint = "K32GetProcessMemoryInfo")]
		internal static extern bool GetProcessMemoryInfo(IntPtr Process, ref Interop.Kernel32.PROCESS_MEMORY_COUNTERS ppsmemCounters, uint cb);

		// Token: 0x06000038 RID: 56
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetProcessTimes(IntPtr handleProcess, out long creation, out long exit, out long kernel, out long user);

		// Token: 0x06000039 RID: 57
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetSystemDirectoryW(ref char lpBuffer, uint uSize);

		// Token: 0x0600003A RID: 58
		[DllImport("kernel32.dll")]
		internal static extern void GetSystemInfo(out Interop.Kernel32.SYSTEM_INFO lpSystemInfo);

		// Token: 0x0600003B RID: 59
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool GetSystemTimes(out long idle, out long kernel, out long user);

		// Token: 0x0600003C RID: 60
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetTempFileNameW(ref char lpPathName, string lpPrefixString, uint uUnique, ref char lpTempFileName);

		// Token: 0x0600003D RID: 61
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern uint GetTempPathW(int bufferLen, ref char buffer);

		// Token: 0x0600003E RID: 62
		[DllImport("kernel32.dll")]
		internal static extern bool GlobalMemoryStatusEx(ref Interop.Kernel32.MEMORYSTATUSEX lpBuffer);

		// Token: 0x0600003F RID: 63
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadLibraryExW", ExactSpelling = true, SetLastError = true)]
		internal static extern IntPtr LoadLibraryEx(string libFilename, IntPtr reserved, int flags);

		// Token: 0x06000040 RID: 64
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool LockFile(SafeFileHandle handle, int offsetLow, int offsetHigh, int countLow, int countHigh);

		// Token: 0x06000041 RID: 65
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool UnlockFile(SafeFileHandle handle, int offsetLow, int offsetHigh, int countLow, int countHigh);

		// Token: 0x06000042 RID: 66
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern bool GetFileMUIPath(uint dwFlags, string pcwszFilePath, char* pwszLanguage, ref int pcchLanguage, char* pwszFileMUIPath, ref int pcchFileMUIPath, ref long pululEnumerator);

		// Token: 0x06000043 RID: 67
		[DllImport("kernel32.dll")]
		internal unsafe static extern int MultiByteToWideChar(uint CodePage, uint dwFlags, byte* lpMultiByteStr, int cbMultiByte, char* lpWideCharStr, int cchWideChar);

		// Token: 0x06000044 RID: 68
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "OutputDebugStringW", ExactSpelling = true)]
		internal static extern void OutputDebugString(string message);

		// Token: 0x06000045 RID: 69
		[SuppressGCTransition]
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal unsafe static extern Interop.BOOL QueryPerformanceCounter(long* lpPerformanceCount);

		// Token: 0x06000046 RID: 70
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal unsafe static extern Interop.BOOL QueryPerformanceFrequency(long* lpFrequency);

		// Token: 0x06000047 RID: 71
		[DllImport("kernel32.dll")]
		internal static extern bool QueryUnbiasedInterruptTime(out ulong UnbiasedTime);

		// Token: 0x06000048 RID: 72
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafeHandle handle, byte* bytes, int numBytesToRead, out int numBytesRead, IntPtr mustBeZero);

		// Token: 0x06000049 RID: 73
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int ReadFile(SafeHandle handle, byte* bytes, int numBytesToRead, IntPtr numBytesRead_mustBeZero, NativeOverlapped* overlapped);

		// Token: 0x0600004A RID: 74
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "SetCurrentDirectoryW", ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetCurrentDirectory(string path);

		// Token: 0x0600004B RID: 75
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetEndOfFile(SafeFileHandle hFile);

		// Token: 0x0600004C RID: 76
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetFilePointerEx(SafeFileHandle hFile, long liDistanceToMove, out long lpNewFilePointer, uint dwMoveMethod);

		// Token: 0x0600004D RID: 77
		[SuppressGCTransition]
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetThreadErrorMode(uint dwNewMode, out uint lpOldMode);

		// Token: 0x0600004E RID: 78
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetDynamicTimeZoneInformation(out Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION pTimeZoneInformation);

		// Token: 0x0600004F RID: 79
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern uint GetTimeZoneInformation(out Interop.Kernel32.TIME_ZONE_INFORMATION lpTimeZoneInformation);

		// Token: 0x06000050 RID: 80
		[DllImport("kernel32.dll")]
		internal static extern bool VerifyVersionInfoW(ref Interop.Kernel32.OSVERSIONINFOEX lpVersionInfo, uint dwTypeMask, ulong dwlConditionMask);

		// Token: 0x06000051 RID: 81
		[DllImport("kernel32.dll")]
		internal static extern ulong VerSetConditionMask(ulong ConditionMask, uint TypeMask, byte Condition);

		// Token: 0x06000052 RID: 82
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal unsafe static extern void* VirtualAlloc(void* lpAddress, UIntPtr dwSize, int flAllocationType, int flProtect);

		// Token: 0x06000053 RID: 83
		[DllImport("kernel32.dll", ExactSpelling = true)]
		internal unsafe static extern bool VirtualFree(void* lpAddress, UIntPtr dwSize, int dwFreeType);

		// Token: 0x06000054 RID: 84
		[DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern UIntPtr VirtualQuery(void* lpAddress, ref Interop.Kernel32.MEMORY_BASIC_INFORMATION lpBuffer, UIntPtr dwLength);

		// Token: 0x06000055 RID: 85
		[DllImport("kernel32.dll")]
		internal unsafe static extern int WideCharToMultiByte(uint CodePage, uint dwFlags, char* lpWideCharStr, int cchWideChar, byte* lpMultiByteStr, int cbMultiByte, IntPtr lpDefaultChar, IntPtr lpUsedDefaultChar);

		// Token: 0x06000056 RID: 86
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafeHandle handle, byte* bytes, int numBytesToWrite, IntPtr numBytesWritten_mustBeZero, NativeOverlapped* lpOverlapped);

		// Token: 0x06000057 RID: 87
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool CloseHandle(IntPtr handle);

		// Token: 0x06000058 RID: 88
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool SetEvent(SafeWaitHandle handle);

		// Token: 0x06000059 RID: 89
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ResetEvent(SafeWaitHandle handle);

		// Token: 0x0600005A RID: 90
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateEventExW", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeWaitHandle CreateEventEx(IntPtr lpSecurityAttributes, string name, uint flags, uint desiredAccess);

		// Token: 0x0600005B RID: 91
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "OpenEventW", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeWaitHandle OpenEvent(uint desiredAccess, bool inheritHandle, string name);

		// Token: 0x0600005C RID: 92 RVA: 0x000AABC4 File Offset: 0x000A9DC4
		internal unsafe static int GetEnvironmentVariable(string lpName, Span<char> buffer)
		{
			fixed (char* reference = MemoryMarshal.GetReference<char>(buffer))
			{
				char* lpBuffer = reference;
				return Interop.Kernel32.GetEnvironmentVariable(lpName, lpBuffer, buffer.Length);
			}
		}

		// Token: 0x0600005D RID: 93
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "GetEnvironmentVariableW", ExactSpelling = true, SetLastError = true)]
		private unsafe static extern int GetEnvironmentVariable(string lpName, char* lpBuffer, int nSize);

		// Token: 0x0600005E RID: 94
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "GetEnvironmentStringsW", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern char* GetEnvironmentStrings();

		// Token: 0x0600005F RID: 95
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "FreeEnvironmentStringsW", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern bool FreeEnvironmentStrings(char* lpszEnvironmentBlock);

		// Token: 0x06000060 RID: 96
		[DllImport("kernel32.dll", BestFitMapping = true, CharSet = CharSet.Unicode, EntryPoint = "FormatMessageW", ExactSpelling = true, SetLastError = true)]
		private unsafe static extern int FormatMessage(int dwFlags, IntPtr lpSource, uint dwMessageId, int dwLanguageId, void* lpBuffer, int nSize, IntPtr arguments);

		// Token: 0x06000061 RID: 97 RVA: 0x000AABE9 File Offset: 0x000A9DE9
		internal static string GetMessage(int errorCode)
		{
			return Interop.Kernel32.GetMessage(errorCode, IntPtr.Zero);
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000AABF8 File Offset: 0x000A9DF8
		internal unsafe static string GetMessage(int errorCode, IntPtr moduleHandle)
		{
			int num = 12800;
			if (moduleHandle != IntPtr.Zero)
			{
				num |= 2048;
			}
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)512], 256);
			Span<char> span2 = span;
			fixed (char* pinnableReference = span2.GetPinnableReference())
			{
				char* lpBuffer = pinnableReference;
				int num2 = Interop.Kernel32.FormatMessage(num, moduleHandle, (uint)errorCode, 0, (void*)lpBuffer, span2.Length, IntPtr.Zero);
				if (num2 > 0)
				{
					return Interop.Kernel32.GetAndTrimString(span2.Slice(0, num2));
				}
			}
			if (Marshal.GetLastWin32Error() == 122)
			{
				IntPtr intPtr = 0;
				try
				{
					int num3 = Interop.Kernel32.FormatMessage(num | 256, moduleHandle, (uint)errorCode, 0, (void*)(&intPtr), 0, IntPtr.Zero);
					if (num3 > 0)
					{
						return Interop.Kernel32.GetAndTrimString(new Span<char>((void*)intPtr, num3));
					}
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
				}
			}
			return string.Format("Unknown error (0x{0:x})", errorCode);
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000AACE8 File Offset: 0x000A9EE8
		private unsafe static string GetAndTrimString(Span<char> buffer)
		{
			int num = buffer.Length;
			while (num > 0 && *buffer[num - 1] <= ' ')
			{
				num--;
			}
			return buffer.Slice(0, num).ToString();
		}

		// Token: 0x06000064 RID: 100
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "OpenMutexW", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeWaitHandle OpenMutex(uint desiredAccess, bool inheritHandle, string name);

		// Token: 0x06000065 RID: 101
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateMutexExW", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeWaitHandle CreateMutexEx(IntPtr lpMutexAttributes, string name, uint flags, uint desiredAccess);

		// Token: 0x06000066 RID: 102
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ReleaseMutex(SafeWaitHandle handle);

		// Token: 0x06000067 RID: 103
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "OpenSemaphoreW", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeWaitHandle OpenSemaphore(uint desiredAccess, bool inheritHandle, string name);

		// Token: 0x06000068 RID: 104
		[DllImport("kernel32.dll", CharSet = CharSet.Unicode, EntryPoint = "CreateSemaphoreExW", ExactSpelling = true, SetLastError = true)]
		internal static extern SafeWaitHandle CreateSemaphoreEx(IntPtr lpSecurityAttributes, int initialCount, int maximumCount, string name, uint flags, uint desiredAccess);

		// Token: 0x06000069 RID: 105
		[DllImport("kernel32.dll", SetLastError = true)]
		internal static extern bool ReleaseSemaphore(SafeWaitHandle handle, int releaseCount, out int previousCount);

		// Token: 0x0600006A RID: 106
		[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "SetEnvironmentVariableW", ExactSpelling = true, SetLastError = true)]
		internal static extern bool SetEnvironmentVariable(string lpName, string lpValue);

		// Token: 0x0600006B RID: 107
		[DllImport("kernel32.dll", SetLastError = true)]
		internal unsafe static extern int WriteFile(SafeHandle handle, byte* bytes, int numBytesToWrite, out int numBytesWritten, IntPtr mustBeZero);

		// Token: 0x0200000A RID: 10
		internal struct NlsVersionInfoEx
		{
			// Token: 0x04000005 RID: 5
			internal int dwNLSVersionInfoSize;

			// Token: 0x04000006 RID: 6
			internal int dwNLSVersion;

			// Token: 0x04000007 RID: 7
			internal int dwDefinedVersion;

			// Token: 0x04000008 RID: 8
			internal int dwEffectiveId;

			// Token: 0x04000009 RID: 9
			internal Guid guidCustomVersion;
		}

		// Token: 0x0200000B RID: 11
		internal struct FILE_STANDARD_INFO
		{
			// Token: 0x0400000A RID: 10
			internal long AllocationSize;

			// Token: 0x0400000B RID: 11
			internal long EndOfFile;

			// Token: 0x0400000C RID: 12
			internal uint NumberOfLinks;

			// Token: 0x0400000D RID: 13
			internal Interop.BOOL DeletePending;

			// Token: 0x0400000E RID: 14
			internal Interop.BOOL Directory;
		}

		// Token: 0x0200000C RID: 12
		internal struct FILE_TIME
		{
			// Token: 0x0400000F RID: 15
			internal uint dwLowDateTime;

			// Token: 0x04000010 RID: 16
			internal uint dwHighDateTime;
		}

		// Token: 0x0200000D RID: 13
		internal enum FINDEX_INFO_LEVELS : uint
		{
			// Token: 0x04000012 RID: 18
			FindExInfoStandard,
			// Token: 0x04000013 RID: 19
			FindExInfoBasic,
			// Token: 0x04000014 RID: 20
			FindExInfoMaxInfoLevel
		}

		// Token: 0x0200000E RID: 14
		internal enum FINDEX_SEARCH_OPS : uint
		{
			// Token: 0x04000016 RID: 22
			FindExSearchNameMatch,
			// Token: 0x04000017 RID: 23
			FindExSearchLimitToDirectories,
			// Token: 0x04000018 RID: 24
			FindExSearchLimitToDevices,
			// Token: 0x04000019 RID: 25
			FindExSearchMaxSearchOp
		}

		// Token: 0x0200000F RID: 15
		internal enum GET_FILEEX_INFO_LEVELS : uint
		{
			// Token: 0x0400001B RID: 27
			GetFileExInfoStandard,
			// Token: 0x0400001C RID: 28
			GetFileExMaxInfoLevel
		}

		// Token: 0x02000010 RID: 16
		internal struct CPINFO
		{
			// Token: 0x0400001D RID: 29
			internal int MaxCharSize;

			// Token: 0x0400001E RID: 30
			[FixedBuffer(typeof(byte), 2)]
			internal Interop.Kernel32.CPINFO.<DefaultChar>e__FixedBuffer DefaultChar;

			// Token: 0x0400001F RID: 31
			[FixedBuffer(typeof(byte), 12)]
			internal Interop.Kernel32.CPINFO.<LeadByte>e__FixedBuffer LeadByte;

			// Token: 0x02000011 RID: 17
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, Size = 2)]
			public struct <DefaultChar>e__FixedBuffer
			{
				// Token: 0x04000020 RID: 32
				public byte FixedElementField;
			}

			// Token: 0x02000012 RID: 18
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, Size = 12)]
			public struct <LeadByte>e__FixedBuffer
			{
				// Token: 0x04000021 RID: 33
				public byte FixedElementField;
			}
		}

		// Token: 0x02000013 RID: 19
		internal struct PROCESS_MEMORY_COUNTERS
		{
			// Token: 0x04000022 RID: 34
			public uint cb;

			// Token: 0x04000023 RID: 35
			public uint PageFaultCount;

			// Token: 0x04000024 RID: 36
			public UIntPtr PeakWorkingSetSize;

			// Token: 0x04000025 RID: 37
			public UIntPtr WorkingSetSize;

			// Token: 0x04000026 RID: 38
			public UIntPtr QuotaPeakPagedPoolUsage;

			// Token: 0x04000027 RID: 39
			public UIntPtr QuotaPagedPoolUsage;

			// Token: 0x04000028 RID: 40
			public UIntPtr QuotaPeakNonPagedPoolUsage;

			// Token: 0x04000029 RID: 41
			public UIntPtr QuotaNonPagedPoolUsage;

			// Token: 0x0400002A RID: 42
			public UIntPtr PagefileUsage;

			// Token: 0x0400002B RID: 43
			public UIntPtr PeakPagefileUsage;
		}

		// Token: 0x02000014 RID: 20
		internal struct MEMORY_BASIC_INFORMATION
		{
			// Token: 0x0400002C RID: 44
			internal unsafe void* BaseAddress;

			// Token: 0x0400002D RID: 45
			internal unsafe void* AllocationBase;

			// Token: 0x0400002E RID: 46
			internal uint AllocationProtect;

			// Token: 0x0400002F RID: 47
			internal UIntPtr RegionSize;

			// Token: 0x04000030 RID: 48
			internal uint State;

			// Token: 0x04000031 RID: 49
			internal uint Protect;

			// Token: 0x04000032 RID: 50
			internal uint Type;
		}

		// Token: 0x02000015 RID: 21
		internal struct MEMORYSTATUSEX
		{
			// Token: 0x04000033 RID: 51
			internal uint dwLength;

			// Token: 0x04000034 RID: 52
			internal uint dwMemoryLoad;

			// Token: 0x04000035 RID: 53
			internal ulong ullTotalPhys;

			// Token: 0x04000036 RID: 54
			internal ulong ullAvailPhys;

			// Token: 0x04000037 RID: 55
			internal ulong ullTotalPageFile;

			// Token: 0x04000038 RID: 56
			internal ulong ullAvailPageFile;

			// Token: 0x04000039 RID: 57
			internal ulong ullTotalVirtual;

			// Token: 0x0400003A RID: 58
			internal ulong ullAvailVirtual;

			// Token: 0x0400003B RID: 59
			internal ulong ullAvailExtendedVirtual;
		}

		// Token: 0x02000016 RID: 22
		internal struct SECURITY_ATTRIBUTES
		{
			// Token: 0x0400003C RID: 60
			internal uint nLength;

			// Token: 0x0400003D RID: 61
			internal IntPtr lpSecurityDescriptor;

			// Token: 0x0400003E RID: 62
			internal Interop.BOOL bInheritHandle;
		}

		// Token: 0x02000017 RID: 23
		internal struct SYSTEM_INFO
		{
			// Token: 0x0400003F RID: 63
			internal ushort wProcessorArchitecture;

			// Token: 0x04000040 RID: 64
			internal ushort wReserved;

			// Token: 0x04000041 RID: 65
			internal int dwPageSize;

			// Token: 0x04000042 RID: 66
			internal IntPtr lpMinimumApplicationAddress;

			// Token: 0x04000043 RID: 67
			internal IntPtr lpMaximumApplicationAddress;

			// Token: 0x04000044 RID: 68
			internal IntPtr dwActiveProcessorMask;

			// Token: 0x04000045 RID: 69
			internal int dwNumberOfProcessors;

			// Token: 0x04000046 RID: 70
			internal int dwProcessorType;

			// Token: 0x04000047 RID: 71
			internal int dwAllocationGranularity;

			// Token: 0x04000048 RID: 72
			internal short wProcessorLevel;

			// Token: 0x04000049 RID: 73
			internal short wProcessorRevision;
		}

		// Token: 0x02000018 RID: 24
		internal struct SYSTEMTIME
		{
			// Token: 0x0600006C RID: 108 RVA: 0x000AAD30 File Offset: 0x000A9F30
			internal bool Equals(in Interop.Kernel32.SYSTEMTIME other)
			{
				return this.Year == other.Year && this.Month == other.Month && this.DayOfWeek == other.DayOfWeek && this.Day == other.Day && this.Hour == other.Hour && this.Minute == other.Minute && this.Second == other.Second && this.Milliseconds == other.Milliseconds;
			}

			// Token: 0x0400004A RID: 74
			internal ushort Year;

			// Token: 0x0400004B RID: 75
			internal ushort Month;

			// Token: 0x0400004C RID: 76
			internal ushort DayOfWeek;

			// Token: 0x0400004D RID: 77
			internal ushort Day;

			// Token: 0x0400004E RID: 78
			internal ushort Hour;

			// Token: 0x0400004F RID: 79
			internal ushort Minute;

			// Token: 0x04000050 RID: 80
			internal ushort Second;

			// Token: 0x04000051 RID: 81
			internal ushort Milliseconds;
		}

		// Token: 0x02000019 RID: 25
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TIME_DYNAMIC_ZONE_INFORMATION
		{
			// Token: 0x0600006D RID: 109 RVA: 0x000AADB0 File Offset: 0x000A9FB0
			internal unsafe string GetTimeZoneKeyName()
			{
				fixed (char* ptr = &this.TimeZoneKeyName.FixedElementField)
				{
					char* value = ptr;
					return new string(value);
				}
			}

			// Token: 0x04000052 RID: 82
			internal int Bias;

			// Token: 0x04000053 RID: 83
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION.<StandardName>e__FixedBuffer StandardName;

			// Token: 0x04000054 RID: 84
			internal Interop.Kernel32.SYSTEMTIME StandardDate;

			// Token: 0x04000055 RID: 85
			internal int StandardBias;

			// Token: 0x04000056 RID: 86
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION.<DaylightName>e__FixedBuffer DaylightName;

			// Token: 0x04000057 RID: 87
			internal Interop.Kernel32.SYSTEMTIME DaylightDate;

			// Token: 0x04000058 RID: 88
			internal int DaylightBias;

			// Token: 0x04000059 RID: 89
			[FixedBuffer(typeof(char), 128)]
			internal Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION.<TimeZoneKeyName>e__FixedBuffer TimeZoneKeyName;

			// Token: 0x0400005A RID: 90
			internal byte DynamicDaylightTimeDisabled;

			// Token: 0x0200001A RID: 26
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <StandardName>e__FixedBuffer
			{
				// Token: 0x0400005B RID: 91
				public char FixedElementField;
			}

			// Token: 0x0200001B RID: 27
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <DaylightName>e__FixedBuffer
			{
				// Token: 0x0400005C RID: 92
				public char FixedElementField;
			}

			// Token: 0x0200001C RID: 28
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 256)]
			public struct <TimeZoneKeyName>e__FixedBuffer
			{
				// Token: 0x0400005D RID: 93
				public char FixedElementField;
			}
		}

		// Token: 0x0200001D RID: 29
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct TIME_ZONE_INFORMATION
		{
			// Token: 0x0600006E RID: 110 RVA: 0x000AADD4 File Offset: 0x000A9FD4
			internal unsafe TIME_ZONE_INFORMATION(in Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION dtzi)
			{
				fixed (Interop.Kernel32.TIME_ZONE_INFORMATION* ptr = &this)
				{
					Interop.Kernel32.TIME_ZONE_INFORMATION* ptr2 = ptr;
					fixed (Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION* ptr3 = &dtzi)
					{
						Interop.Kernel32.TIME_DYNAMIC_ZONE_INFORMATION* ptr4 = ptr3;
						*ptr2 = *(Interop.Kernel32.TIME_ZONE_INFORMATION*)ptr4;
					}
				}
			}

			// Token: 0x0600006F RID: 111 RVA: 0x000AAE00 File Offset: 0x000AA000
			internal unsafe string GetStandardName()
			{
				fixed (char* ptr = &this.StandardName.FixedElementField)
				{
					char* value = ptr;
					return new string(value);
				}
			}

			// Token: 0x06000070 RID: 112 RVA: 0x000AAE24 File Offset: 0x000AA024
			internal unsafe string GetDaylightName()
			{
				fixed (char* ptr = &this.DaylightName.FixedElementField)
				{
					char* value = ptr;
					return new string(value);
				}
			}

			// Token: 0x0400005E RID: 94
			internal int Bias;

			// Token: 0x0400005F RID: 95
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_ZONE_INFORMATION.<StandardName>e__FixedBuffer StandardName;

			// Token: 0x04000060 RID: 96
			internal Interop.Kernel32.SYSTEMTIME StandardDate;

			// Token: 0x04000061 RID: 97
			internal int StandardBias;

			// Token: 0x04000062 RID: 98
			[FixedBuffer(typeof(char), 32)]
			internal Interop.Kernel32.TIME_ZONE_INFORMATION.<DaylightName>e__FixedBuffer DaylightName;

			// Token: 0x04000063 RID: 99
			internal Interop.Kernel32.SYSTEMTIME DaylightDate;

			// Token: 0x04000064 RID: 100
			internal int DaylightBias;

			// Token: 0x0200001E RID: 30
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <StandardName>e__FixedBuffer
			{
				// Token: 0x04000065 RID: 101
				public char FixedElementField;
			}

			// Token: 0x0200001F RID: 31
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 64)]
			public struct <DaylightName>e__FixedBuffer
			{
				// Token: 0x04000066 RID: 102
				public char FixedElementField;
			}
		}

		// Token: 0x02000020 RID: 32
		internal struct REG_TZI_FORMAT
		{
			// Token: 0x06000071 RID: 113 RVA: 0x000AAE46 File Offset: 0x000AA046
			internal REG_TZI_FORMAT(in Interop.Kernel32.TIME_ZONE_INFORMATION tzi)
			{
				this.Bias = tzi.Bias;
				this.StandardDate = tzi.StandardDate;
				this.StandardBias = tzi.StandardBias;
				this.DaylightDate = tzi.DaylightDate;
				this.DaylightBias = tzi.DaylightBias;
			}

			// Token: 0x04000067 RID: 103
			internal int Bias;

			// Token: 0x04000068 RID: 104
			internal int StandardBias;

			// Token: 0x04000069 RID: 105
			internal int DaylightBias;

			// Token: 0x0400006A RID: 106
			internal Interop.Kernel32.SYSTEMTIME StandardDate;

			// Token: 0x0400006B RID: 107
			internal Interop.Kernel32.SYSTEMTIME DaylightDate;
		}

		// Token: 0x02000021 RID: 33
		internal struct WIN32_FILE_ATTRIBUTE_DATA
		{
			// Token: 0x06000072 RID: 114 RVA: 0x000AAE84 File Offset: 0x000AA084
			internal void PopulateFrom(ref Interop.Kernel32.WIN32_FIND_DATA findData)
			{
				this.dwFileAttributes = (int)findData.dwFileAttributes;
				this.ftCreationTime = findData.ftCreationTime;
				this.ftLastAccessTime = findData.ftLastAccessTime;
				this.ftLastWriteTime = findData.ftLastWriteTime;
				this.nFileSizeHigh = findData.nFileSizeHigh;
				this.nFileSizeLow = findData.nFileSizeLow;
			}

			// Token: 0x0400006C RID: 108
			internal int dwFileAttributes;

			// Token: 0x0400006D RID: 109
			internal Interop.Kernel32.FILE_TIME ftCreationTime;

			// Token: 0x0400006E RID: 110
			internal Interop.Kernel32.FILE_TIME ftLastAccessTime;

			// Token: 0x0400006F RID: 111
			internal Interop.Kernel32.FILE_TIME ftLastWriteTime;

			// Token: 0x04000070 RID: 112
			internal uint nFileSizeHigh;

			// Token: 0x04000071 RID: 113
			internal uint nFileSizeLow;
		}

		// Token: 0x02000022 RID: 34
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct WIN32_FIND_DATA
		{
			// Token: 0x04000072 RID: 114
			internal uint dwFileAttributes;

			// Token: 0x04000073 RID: 115
			internal Interop.Kernel32.FILE_TIME ftCreationTime;

			// Token: 0x04000074 RID: 116
			internal Interop.Kernel32.FILE_TIME ftLastAccessTime;

			// Token: 0x04000075 RID: 117
			internal Interop.Kernel32.FILE_TIME ftLastWriteTime;

			// Token: 0x04000076 RID: 118
			internal uint nFileSizeHigh;

			// Token: 0x04000077 RID: 119
			internal uint nFileSizeLow;

			// Token: 0x04000078 RID: 120
			internal uint dwReserved0;

			// Token: 0x04000079 RID: 121
			internal uint dwReserved1;

			// Token: 0x0400007A RID: 122
			[FixedBuffer(typeof(char), 260)]
			private Interop.Kernel32.WIN32_FIND_DATA.<_cFileName>e__FixedBuffer _cFileName;

			// Token: 0x0400007B RID: 123
			[FixedBuffer(typeof(char), 14)]
			private Interop.Kernel32.WIN32_FIND_DATA.<_cAlternateFileName>e__FixedBuffer _cAlternateFileName;

			// Token: 0x02000023 RID: 35
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 520)]
			public struct <_cFileName>e__FixedBuffer
			{
				// Token: 0x0400007C RID: 124
				public char FixedElementField;
			}

			// Token: 0x02000024 RID: 36
			[UnsafeValueType]
			[CompilerGenerated]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 28)]
			public struct <_cAlternateFileName>e__FixedBuffer
			{
				// Token: 0x0400007D RID: 125
				public char FixedElementField;
			}
		}

		// Token: 0x02000025 RID: 37
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct OSVERSIONINFOEX
		{
			// Token: 0x0400007E RID: 126
			public int dwOSVersionInfoSize;

			// Token: 0x0400007F RID: 127
			public int dwMajorVersion;

			// Token: 0x04000080 RID: 128
			public int dwMinorVersion;

			// Token: 0x04000081 RID: 129
			public int dwBuildNumber;

			// Token: 0x04000082 RID: 130
			public int dwPlatformId;

			// Token: 0x04000083 RID: 131
			[FixedBuffer(typeof(char), 128)]
			public Interop.Kernel32.OSVERSIONINFOEX.<szCSDVersion>e__FixedBuffer szCSDVersion;

			// Token: 0x04000084 RID: 132
			public ushort wServicePackMajor;

			// Token: 0x04000085 RID: 133
			public ushort wServicePackMinor;

			// Token: 0x04000086 RID: 134
			public ushort wSuiteMask;

			// Token: 0x04000087 RID: 135
			public byte wProductType;

			// Token: 0x04000088 RID: 136
			public byte wReserved;

			// Token: 0x02000026 RID: 38
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 256)]
			public struct <szCSDVersion>e__FixedBuffer
			{
				// Token: 0x04000089 RID: 137
				public char FixedElementField;
			}
		}
	}

	// Token: 0x02000027 RID: 39
	internal static class Ole32
	{
		// Token: 0x06000073 RID: 115
		[DllImport("ole32.dll")]
		internal static extern IntPtr CoTaskMemAlloc(UIntPtr cb);

		// Token: 0x06000074 RID: 116
		[DllImport("ole32.dll")]
		internal static extern IntPtr CoTaskMemRealloc(IntPtr pv, UIntPtr cb);

		// Token: 0x06000075 RID: 117
		[DllImport("ole32.dll")]
		internal static extern void CoTaskMemFree(IntPtr ptr);

		// Token: 0x06000076 RID: 118
		[DllImport("ole32.dll")]
		internal static extern int CoCreateGuid(out Guid guid);

		// Token: 0x06000077 RID: 119
		[DllImport("ole32.dll")]
		internal static extern int CoGetStandardMarshal(ref Guid riid, IntPtr pv, int dwDestContext, IntPtr pvDestContext, int mshlflags, out IntPtr ppMarshal);
	}

	// Token: 0x02000028 RID: 40
	internal static class OleAut32
	{
		// Token: 0x06000078 RID: 120
		[DllImport("oleaut32.dll")]
		internal static extern IntPtr SysAllocStringByteLen(byte[] str, uint len);

		// Token: 0x06000079 RID: 121
		[DllImport("oleaut32.dll")]
		internal static extern void VariantClear(IntPtr variant);

		// Token: 0x0600007A RID: 122
		[DllImport("oleaut32.dll", CharSet = CharSet.Unicode)]
		internal static extern IntPtr SysAllocStringLen(string src, int len);

		// Token: 0x0600007B RID: 123
		[DllImport("oleaut32.dll")]
		internal static extern void SysFreeString(IntPtr bstr);
	}

	// Token: 0x02000029 RID: 41
	internal static class Globalization
	{
		// Token: 0x0600007C RID: 124
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_GetCalendars")]
		internal static extern int GetCalendars(string localeName, CalendarId[] calendars, int calendarsCapacity);

		// Token: 0x0600007D RID: 125
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_GetCalendarInfo")]
		internal unsafe static extern Interop.Globalization.ResultCode GetCalendarInfo(string localeName, CalendarId calendarId, CalendarDataType calendarDataType, char* result, int resultCapacity);

		// Token: 0x0600007E RID: 126
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_EnumCalendarInfo")]
		internal unsafe static extern bool EnumCalendarInfo(method callback, bool localeName, void calendarId, char* calendarDataType, IntPtr context);

		// Token: 0x0600007F RID: 127
		[DllImport("QCall", EntryPoint = "GlobalizationNative_GetLatestJapaneseEra")]
		internal static extern int GetLatestJapaneseEra();

		// Token: 0x06000080 RID: 128
		[DllImport("QCall", EntryPoint = "GlobalizationNative_GetJapaneseEraStartDate")]
		internal static extern bool GetJapaneseEraStartDate(int era, out int startYear, out int startMonth, out int startDay);

		// Token: 0x06000081 RID: 129
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_ChangeCase")]
		internal unsafe static extern void ChangeCase(char* src, int srcLen, char* dstBuffer, int dstBufferCapacity, bool bToUpper);

		// Token: 0x06000082 RID: 130
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_ChangeCaseInvariant")]
		internal unsafe static extern void ChangeCaseInvariant(char* src, int srcLen, char* dstBuffer, int dstBufferCapacity, bool bToUpper);

		// Token: 0x06000083 RID: 131
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_ChangeCaseTurkish")]
		internal unsafe static extern void ChangeCaseTurkish(char* src, int srcLen, char* dstBuffer, int dstBufferCapacity, bool bToUpper);

		// Token: 0x06000084 RID: 132
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_InitOrdinalCasingPage")]
		internal unsafe static extern void InitOrdinalCasingPage(int pageNumber, char* pTarget);

		// Token: 0x06000085 RID: 133
		[DllImport("QCall", CharSet = CharSet.Ansi, EntryPoint = "GlobalizationNative_GetSortHandle")]
		internal static extern Interop.Globalization.ResultCode GetSortHandle(string localeName, out IntPtr sortHandle);

		// Token: 0x06000086 RID: 134
		[DllImport("QCall", EntryPoint = "GlobalizationNative_CloseSortHandle")]
		internal static extern void CloseSortHandle(IntPtr handle);

		// Token: 0x06000087 RID: 135
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_CompareString")]
		internal unsafe static extern int CompareString(IntPtr sortHandle, char* lpStr1, int cwStr1Len, char* lpStr2, int cwStr2Len, CompareOptions options);

		// Token: 0x06000088 RID: 136
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_IndexOf")]
		internal unsafe static extern int IndexOf(IntPtr sortHandle, char* target, int cwTargetLength, char* pSource, int cwSourceLength, CompareOptions options, int* matchLengthPtr);

		// Token: 0x06000089 RID: 137
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_LastIndexOf")]
		internal unsafe static extern int LastIndexOf(IntPtr sortHandle, char* target, int cwTargetLength, char* pSource, int cwSourceLength, CompareOptions options, int* matchLengthPtr);

		// Token: 0x0600008A RID: 138
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_StartsWith")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool StartsWith(IntPtr sortHandle, char* target, int cwTargetLength, char* source, int cwSourceLength, CompareOptions options, int* matchedLength);

		// Token: 0x0600008B RID: 139
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_EndsWith")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool EndsWith(IntPtr sortHandle, char* target, int cwTargetLength, char* source, int cwSourceLength, CompareOptions options, int* matchedLength);

		// Token: 0x0600008C RID: 140
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_GetSortKey")]
		internal unsafe static extern int GetSortKey(IntPtr sortHandle, char* str, int strLength, byte* sortKey, int sortKeyLength, CompareOptions options);

		// Token: 0x0600008D RID: 141
		[DllImport("QCall", EntryPoint = "GlobalizationNative_GetSortVersion")]
		internal static extern int GetSortVersion(IntPtr sortHandle);

		// Token: 0x0600008E RID: 142
		[DllImport("QCall", EntryPoint = "GlobalizationNative_LoadICU")]
		internal static extern int LoadICU();

		// Token: 0x0600008F RID: 143 RVA: 0x000AAED9 File Offset: 0x000AA0D9
		internal static void InitICUFunctions(IntPtr icuuc, IntPtr icuin, ReadOnlySpan<char> version, ReadOnlySpan<char> suffix)
		{
			Interop.Globalization.InitICUFunctions(icuuc, icuin, version.ToString(), (suffix.Length > 0) ? suffix.ToString() : null);
		}

		// Token: 0x06000090 RID: 144
		[DllImport("QCall", EntryPoint = "GlobalizationNative_InitICUFunctions")]
		internal static extern void InitICUFunctions(IntPtr icuuc, IntPtr icuin, string version, string suffix);

		// Token: 0x06000091 RID: 145
		[DllImport("QCall", EntryPoint = "GlobalizationNative_GetICUVersion")]
		internal static extern int GetICUVersion();

		// Token: 0x06000092 RID: 146
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_ToAscii")]
		internal unsafe static extern int ToAscii(uint flags, char* src, int srcLen, char* dstBuffer, int dstBufferCapacity);

		// Token: 0x06000093 RID: 147
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_ToUnicode")]
		internal unsafe static extern int ToUnicode(uint flags, char* src, int srcLen, char* dstBuffer, int dstBufferCapacity);

		// Token: 0x06000094 RID: 148
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_GetLocaleInfoString")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool GetLocaleInfoString(string localeName, uint localeStringData, char* value, int valueLength);

		// Token: 0x06000095 RID: 149
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_IsPredefinedLocale")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool IsPredefinedLocale(string localeName);

		// Token: 0x06000096 RID: 150
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_GetLocaleTimeFormat")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal unsafe static extern bool GetLocaleTimeFormat(string localeName, bool shortFormat, char* value, int valueLength);

		// Token: 0x06000097 RID: 151
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_GetLocaleInfoInt")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetLocaleInfoInt(string localeName, uint localeNumberData, ref int value);

		// Token: 0x06000098 RID: 152
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_GetLocaleInfoGroupingSizes")]
		[return: MarshalAs(UnmanagedType.Bool)]
		internal static extern bool GetLocaleInfoGroupingSizes(string localeName, uint localeGroupingData, ref int primaryGroupSize, ref int secondaryGroupSize);

		// Token: 0x06000099 RID: 153
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_GetLocales")]
		internal static extern int GetLocales([Out] char[] value, int valueLength);

		// Token: 0x0600009A RID: 154
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_IsNormalized")]
		internal unsafe static extern int IsNormalized(NormalizationForm normalizationForm, char* src, int srcLen);

		// Token: 0x0600009B RID: 155
		[DllImport("QCall", CharSet = CharSet.Unicode, EntryPoint = "GlobalizationNative_NormalizeString")]
		internal unsafe static extern int NormalizeString(NormalizationForm normalizationForm, char* src, int srcLen, char* dstBuffer, int dstBufferCapacity);

		// Token: 0x0200002A RID: 42
		internal enum ResultCode
		{
			// Token: 0x0400008B RID: 139
			Success,
			// Token: 0x0400008C RID: 140
			UnknownError,
			// Token: 0x0400008D RID: 141
			InsufficentBuffer,
			// Token: 0x0400008E RID: 142
			OutOfMemory
		}
	}

	// Token: 0x0200002B RID: 43
	internal enum BOOL
	{
		// Token: 0x04000090 RID: 144
		FALSE,
		// Token: 0x04000091 RID: 145
		TRUE
	}

	// Token: 0x0200002C RID: 44
	internal static class Normaliz
	{
		// Token: 0x0600009C RID: 156
		[DllImport("Normaliz.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal unsafe static extern int IdnToAscii(uint dwFlags, char* lpUnicodeCharStr, int cchUnicodeChar, char* lpASCIICharStr, int cchASCIIChar);

		// Token: 0x0600009D RID: 157
		[DllImport("Normaliz.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal unsafe static extern int IdnToUnicode(uint dwFlags, char* lpASCIICharStr, int cchASCIIChar, char* lpUnicodeCharStr, int cchUnicodeChar);

		// Token: 0x0600009E RID: 158
		[DllImport("Normaliz.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal unsafe static extern Interop.BOOL IsNormalizedString(NormalizationForm normForm, char* source, int length);

		// Token: 0x0600009F RID: 159
		[DllImport("Normaliz.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal unsafe static extern int NormalizeString(NormalizationForm normForm, char* source, int sourceLength, char* destination, int destinationLength);
	}

	// Token: 0x0200002D RID: 45
	internal static class Advapi32
	{
		// Token: 0x060000A0 RID: 160
		[DllImport("advapi32.dll", ExactSpelling = true)]
		internal static extern int EventActivityIdControl(Interop.Advapi32.ActivityControl ControlCode, ref Guid ActivityId);

		// Token: 0x060000A1 RID: 161
		[DllImport("advapi32.dll", ExactSpelling = true)]
		internal unsafe static extern uint EventRegister(in Guid providerId, Interop.Advapi32.EtwEnableCallback enableCallback, void* callbackContext, ref long registrationHandle);

		// Token: 0x060000A2 RID: 162
		[DllImport("advapi32.dll", ExactSpelling = true)]
		internal unsafe static extern int EventSetInformation(long registrationHandle, Interop.Advapi32.EVENT_INFO_CLASS informationClass, void* eventInformation, int informationLength);

		// Token: 0x060000A3 RID: 163
		[DllImport("advapi32.dll", ExactSpelling = true)]
		internal unsafe static extern int EnumerateTraceGuidsEx(Interop.Advapi32.TRACE_QUERY_INFO_CLASS TraceQueryInfoClass, void* InBuffer, int InBufferSize, void* OutBuffer, int OutBufferSize, out int ReturnLength);

		// Token: 0x060000A4 RID: 164
		[DllImport("advapi32.dll", ExactSpelling = true)]
		internal static extern uint EventUnregister(long registrationHandle);

		// Token: 0x060000A5 RID: 165 RVA: 0x000AAF0C File Offset: 0x000AA10C
		internal unsafe static int EventWriteTransfer(long registrationHandle, in EventDescriptor eventDescriptor, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData)
		{
			int num = Interop.Advapi32.EventWriteTransfer_PInvoke(registrationHandle, eventDescriptor, activityId, relatedActivityId, userDataCount, userData);
			if (num == 87 && relatedActivityId == null)
			{
				Guid empty = Guid.Empty;
				num = Interop.Advapi32.EventWriteTransfer_PInvoke(registrationHandle, eventDescriptor, activityId, &empty, userDataCount, userData);
			}
			return num;
		}

		// Token: 0x060000A6 RID: 166
		[DllImport("advapi32.dll", EntryPoint = "EventWriteTransfer", ExactSpelling = true)]
		private unsafe static extern int EventWriteTransfer_PInvoke(long registrationHandle, in EventDescriptor eventDescriptor, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData);

		// Token: 0x060000A7 RID: 167
		[DllImport("advapi32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern bool LookupAccountNameW(string lpSystemName, ref char lpAccountName, ref byte Sid, ref uint cbSid, ref char ReferencedDomainName, ref uint cchReferencedDomainName, out uint peUse);

		// Token: 0x060000A8 RID: 168
		[DllImport("advapi32.dll")]
		internal static extern int RegCloseKey(IntPtr hKey);

		// Token: 0x060000A9 RID: 169
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegDeleteValueW", ExactSpelling = true)]
		internal static extern int RegDeleteValue(SafeRegistryHandle hKey, string lpValueName);

		// Token: 0x060000AA RID: 170
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegEnumKeyExW", ExactSpelling = true)]
		internal static extern int RegEnumKeyEx(SafeRegistryHandle hKey, int dwIndex, char[] lpName, ref int lpcbName, int[] lpReserved, [Out] char[] lpClass, int[] lpcbClass, long[] lpftLastWriteTime);

		// Token: 0x060000AB RID: 171
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegEnumValueW", ExactSpelling = true)]
		internal static extern int RegEnumValue(SafeRegistryHandle hKey, int dwIndex, char[] lpValueName, ref int lpcbValueName, IntPtr lpReserved_MustBeZero, int[] lpType, byte[] lpData, int[] lpcbData);

		// Token: 0x060000AC RID: 172
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegOpenKeyExW", ExactSpelling = true)]
		internal static extern int RegOpenKeyEx(SafeRegistryHandle hKey, string lpSubKey, int ulOptions, int samDesired, out SafeRegistryHandle hkResult);

		// Token: 0x060000AD RID: 173
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW", ExactSpelling = true)]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] byte[] lpData, ref int lpcbData);

		// Token: 0x060000AE RID: 174
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW", ExactSpelling = true)]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, ref int lpData, ref int lpcbData);

		// Token: 0x060000AF RID: 175
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW", ExactSpelling = true)]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, ref long lpData, ref int lpcbData);

		// Token: 0x060000B0 RID: 176
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegQueryValueExW", ExactSpelling = true)]
		internal static extern int RegQueryValueEx(SafeRegistryHandle hKey, string lpValueName, int[] lpReserved, ref int lpType, [Out] char[] lpData, ref int lpcbData);

		// Token: 0x060000B1 RID: 177
		[DllImport("advapi32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, EntryPoint = "RegSetValueExW", ExactSpelling = true)]
		internal static extern int RegSetValueEx(SafeRegistryHandle hKey, string lpValueName, int Reserved, int dwType, string lpData, int cbData);

		// Token: 0x0200002E RID: 46
		internal enum ActivityControl : uint
		{
			// Token: 0x04000093 RID: 147
			EVENT_ACTIVITY_CTRL_GET_ID = 1U,
			// Token: 0x04000094 RID: 148
			EVENT_ACTIVITY_CTRL_SET_ID,
			// Token: 0x04000095 RID: 149
			EVENT_ACTIVITY_CTRL_CREATE_ID,
			// Token: 0x04000096 RID: 150
			EVENT_ACTIVITY_CTRL_GET_SET_ID,
			// Token: 0x04000097 RID: 151
			EVENT_ACTIVITY_CTRL_CREATE_SET_ID
		}

		// Token: 0x0200002F RID: 47
		internal struct EVENT_FILTER_DESCRIPTOR
		{
			// Token: 0x04000098 RID: 152
			public long Ptr;

			// Token: 0x04000099 RID: 153
			public int Size;

			// Token: 0x0400009A RID: 154
			public int Type;
		}

		// Token: 0x02000030 RID: 48
		// (Invoke) Token: 0x060000B3 RID: 179
		internal unsafe delegate void EtwEnableCallback(in Guid sourceId, int isEnabled, byte level, long matchAnyKeywords, long matchAllKeywords, Interop.Advapi32.EVENT_FILTER_DESCRIPTOR* filterData, void* callbackContext);

		// Token: 0x02000031 RID: 49
		internal enum EVENT_INFO_CLASS
		{
			// Token: 0x0400009C RID: 156
			BinaryTrackInfo,
			// Token: 0x0400009D RID: 157
			SetEnableAllKeywords,
			// Token: 0x0400009E RID: 158
			SetTraits
		}

		// Token: 0x02000032 RID: 50
		internal enum TRACE_QUERY_INFO_CLASS
		{
			// Token: 0x040000A0 RID: 160
			TraceGuidQueryList,
			// Token: 0x040000A1 RID: 161
			TraceGuidQueryInfo,
			// Token: 0x040000A2 RID: 162
			TraceGuidQueryProcess,
			// Token: 0x040000A3 RID: 163
			TraceStackTracingInfo,
			// Token: 0x040000A4 RID: 164
			MaxTraceSetInfoClass
		}

		// Token: 0x02000033 RID: 51
		internal struct TRACE_GUID_INFO
		{
			// Token: 0x040000A5 RID: 165
			public int InstanceCount;

			// Token: 0x040000A6 RID: 166
			public int Reserved;
		}

		// Token: 0x02000034 RID: 52
		internal struct TRACE_PROVIDER_INSTANCE_INFO
		{
			// Token: 0x040000A7 RID: 167
			public int NextOffset;

			// Token: 0x040000A8 RID: 168
			public int EnableCount;

			// Token: 0x040000A9 RID: 169
			public int Pid;

			// Token: 0x040000AA RID: 170
			public int Flags;
		}

		// Token: 0x02000035 RID: 53
		internal struct TRACE_ENABLE_INFO
		{
			// Token: 0x040000AB RID: 171
			public int IsEnabled;

			// Token: 0x040000AC RID: 172
			public byte Level;

			// Token: 0x040000AD RID: 173
			public byte Reserved1;

			// Token: 0x040000AE RID: 174
			public ushort LoggerId;

			// Token: 0x040000AF RID: 175
			public int EnableProperty;

			// Token: 0x040000B0 RID: 176
			public int Reserved2;

			// Token: 0x040000B1 RID: 177
			public long MatchAnyKeyword;

			// Token: 0x040000B2 RID: 178
			public long MatchAllKeyword;
		}
	}

	// Token: 0x02000036 RID: 54
	internal static class HostPolicy
	{
		// Token: 0x060000B4 RID: 180
		[DllImport("hostpolicy.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		internal static extern int corehost_resolve_component_dependencies(string componentMainAssemblyPath, Interop.HostPolicy.corehost_resolve_component_dependencies_result_fn result);

		// Token: 0x060000B5 RID: 181
		[DllImport("hostpolicy.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		internal static extern IntPtr corehost_set_error_writer(IntPtr errorWriter);

		// Token: 0x02000037 RID: 55
		// (Invoke) Token: 0x060000B7 RID: 183
		[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		internal delegate void corehost_resolve_component_dependencies_result_fn(string assemblyPaths, string nativeSearchPaths, string resourceSearchPaths);

		// Token: 0x02000038 RID: 56
		// (Invoke) Token: 0x060000B9 RID: 185
		[UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Auto)]
		internal delegate void corehost_error_writer_fn(string message);
	}

	// Token: 0x02000039 RID: 57
	internal static class BCrypt
	{
		// Token: 0x060000BA RID: 186
		[DllImport("BCrypt.dll", CharSet = CharSet.Unicode)]
		internal unsafe static extern Interop.BCrypt.NTSTATUS BCryptGenRandom(IntPtr hAlgorithm, byte* pbBuffer, int cbBuffer, int dwFlags);

		// Token: 0x0200003A RID: 58
		internal enum NTSTATUS : uint
		{
			// Token: 0x040000B4 RID: 180
			STATUS_SUCCESS,
			// Token: 0x040000B5 RID: 181
			STATUS_NOT_FOUND = 3221226021U,
			// Token: 0x040000B6 RID: 182
			STATUS_INVALID_PARAMETER = 3221225485U,
			// Token: 0x040000B7 RID: 183
			STATUS_NO_MEMORY = 3221225495U,
			// Token: 0x040000B8 RID: 184
			STATUS_AUTH_TAG_MISMATCH = 3221266434U
		}
	}

	// Token: 0x0200003B RID: 59
	internal static class Crypt32
	{
		// Token: 0x060000BB RID: 187
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptProtectMemory(SafeBuffer pData, uint cbData, uint dwFlags);

		// Token: 0x060000BC RID: 188
		[DllImport("crypt32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
		internal static extern bool CryptUnprotectMemory(SafeBuffer pData, uint cbData, uint dwFlags);
	}

	// Token: 0x0200003C RID: 60
	internal enum BOOLEAN : byte
	{
		// Token: 0x040000BA RID: 186
		FALSE,
		// Token: 0x040000BB RID: 187
		TRUE
	}

	// Token: 0x0200003D RID: 61
	internal static class NtDll
	{
		// Token: 0x060000BD RID: 189
		[DllImport("ntdll.dll", ExactSpelling = true)]
		internal unsafe static extern int NtQueryInformationFile(SafeFileHandle FileHandle, out Interop.NtDll.IO_STATUS_BLOCK IoStatusBlock, void* FileInformation, uint Length, uint FileInformationClass);

		// Token: 0x060000BE RID: 190
		[DllImport("ntdll.dll", ExactSpelling = true)]
		internal unsafe static extern uint NtQuerySystemInformation(int SystemInformationClass, void* SystemInformation, uint SystemInformationLength, uint* ReturnLength);

		// Token: 0x060000BF RID: 191
		[DllImport("ntdll.dll", ExactSpelling = true)]
		private static extern int RtlGetVersion(ref Interop.NtDll.RTL_OSVERSIONINFOEX lpVersionInformation);

		// Token: 0x060000C0 RID: 192 RVA: 0x000AAF48 File Offset: 0x000AA148
		internal static int RtlGetVersionEx(out Interop.NtDll.RTL_OSVERSIONINFOEX osvi)
		{
			osvi = default(Interop.NtDll.RTL_OSVERSIONINFOEX);
			osvi.dwOSVersionInfoSize = (uint)sizeof(Interop.NtDll.RTL_OSVERSIONINFOEX);
			return Interop.NtDll.RtlGetVersion(ref osvi);
		}

		// Token: 0x0200003E RID: 62
		internal struct IO_STATUS_BLOCK
		{
			// Token: 0x040000BC RID: 188
			private uint Status;

			// Token: 0x040000BD RID: 189
			private IntPtr Information;
		}

		// Token: 0x0200003F RID: 63
		[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
		internal struct RTL_OSVERSIONINFOEX
		{
			// Token: 0x040000BE RID: 190
			internal uint dwOSVersionInfoSize;

			// Token: 0x040000BF RID: 191
			internal uint dwMajorVersion;

			// Token: 0x040000C0 RID: 192
			internal uint dwMinorVersion;

			// Token: 0x040000C1 RID: 193
			internal uint dwBuildNumber;

			// Token: 0x040000C2 RID: 194
			internal uint dwPlatformId;

			// Token: 0x040000C3 RID: 195
			[FixedBuffer(typeof(char), 128)]
			internal Interop.NtDll.RTL_OSVERSIONINFOEX.<szCSDVersion>e__FixedBuffer szCSDVersion;

			// Token: 0x02000040 RID: 64
			[CompilerGenerated]
			[UnsafeValueType]
			[StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Size = 256)]
			public struct <szCSDVersion>e__FixedBuffer
			{
				// Token: 0x040000C4 RID: 196
				public char FixedElementField;
			}
		}

		// Token: 0x02000041 RID: 65
		internal struct SYSTEM_LEAP_SECOND_INFORMATION
		{
			// Token: 0x040000C5 RID: 197
			public Interop.BOOLEAN Enabled;

			// Token: 0x040000C6 RID: 198
			public uint Flags;
		}
	}

	// Token: 0x02000042 RID: 66
	internal static class Secur32
	{
		// Token: 0x060000C1 RID: 193
		[DllImport("secur32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		internal static extern Interop.BOOLEAN GetUserNameExW(int NameFormat, ref char lpNameBuffer, ref uint lpnSize);
	}

	// Token: 0x02000043 RID: 67
	internal static class Shell32
	{
		// Token: 0x060000C2 RID: 194
		[DllImport("shell32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, ExactSpelling = true)]
		internal static extern int SHGetKnownFolderPath([MarshalAs(UnmanagedType.LPStruct)] Guid rfid, uint dwFlags, IntPtr hToken, out string ppszPath);
	}

	// Token: 0x02000044 RID: 68
	internal static class User32
	{
		// Token: 0x060000C3 RID: 195
		[DllImport("user32.dll", CharSet = CharSet.Unicode, EntryPoint = "LoadStringW", ExactSpelling = true, SetLastError = true)]
		internal unsafe static extern int LoadString(IntPtr hInstance, uint uID, char* lpBuffer, int cchBufferMax);

		// Token: 0x060000C4 RID: 196
		[DllImport("user32.dll", EntryPoint = "SendMessageTimeoutW")]
		public static extern IntPtr SendMessageTimeout(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, int flags, int timeout, out IntPtr pdwResult);

		// Token: 0x060000C5 RID: 197
		[DllImport("user32.dll", ExactSpelling = true)]
		internal static extern IntPtr GetProcessWindowStation();

		// Token: 0x060000C6 RID: 198
		[DllImport("user32.dll", CharSet = CharSet.Unicode, ExactSpelling = true, SetLastError = true)]
		public unsafe static extern bool GetUserObjectInformationW(IntPtr hObj, int nIndex, void* pvBuffer, uint nLength, ref uint lpnLengthNeeded);

		// Token: 0x02000045 RID: 69
		internal struct USEROBJECTFLAGS
		{
			// Token: 0x040000C7 RID: 199
			public int fInherit;

			// Token: 0x040000C8 RID: 200
			public int fReserved;

			// Token: 0x040000C9 RID: 201
			public int dwFlags;
		}
	}
}
