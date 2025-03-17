using System;
using System.Buffers;
using System.Collections;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using Internal.Win32;

namespace System
{
	// Token: 0x02000067 RID: 103
	[NullableContext(1)]
	[Nullable(0)]
	public static class Environment
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600032C RID: 812 RVA: 0x000B514D File Offset: 0x000B434D
		public static int CurrentManagedThreadId
		{
			get
			{
				return Thread.CurrentThread.ManagedThreadId;
			}
		}

		// Token: 0x0600032D RID: 813
		[DoesNotReturn]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void _Exit(int exitCode);

		// Token: 0x0600032E RID: 814 RVA: 0x000B5159 File Offset: 0x000B4359
		[DoesNotReturn]
		public static void Exit(int exitCode)
		{
			Environment._Exit(exitCode);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600032F RID: 815
		// (set) Token: 0x06000330 RID: 816
		public static extern int ExitCode { [MethodImpl(MethodImplOptions.InternalCall)] get; [MethodImpl(MethodImplOptions.InternalCall)] set; }

		// Token: 0x06000331 RID: 817
		[NullableContext(2)]
		[DoesNotReturn]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FailFast(string message);

		// Token: 0x06000332 RID: 818
		[DoesNotReturn]
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FailFast(string message, Exception exception);

		// Token: 0x06000333 RID: 819
		[NullableContext(2)]
		[DoesNotReturn]
		[MethodImpl(MethodImplOptions.InternalCall)]
		public static extern void FailFast(string message, Exception exception, string errorMessage);

		// Token: 0x06000334 RID: 820
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetCommandLineArgsNative();

		// Token: 0x06000335 RID: 821 RVA: 0x000B5161 File Offset: 0x000B4361
		public static string[] GetCommandLineArgs()
		{
			if (Environment.s_commandLineArgs == null)
			{
				return Environment.GetCommandLineArgsNative();
			}
			return (string[])Environment.s_commandLineArgs.Clone();
		}

		// Token: 0x06000336 RID: 822
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetProcessorCount();

		// Token: 0x06000337 RID: 823 RVA: 0x000B517F File Offset: 0x000B437F
		internal static string GetResourceStringLocal(string key)
		{
			return SR.GetResourceString(key);
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000338 RID: 824 RVA: 0x000B5187 File Offset: 0x000B4387
		public static string StackTrace
		{
			[MethodImpl(MethodImplOptions.NoInlining)]
			get
			{
				return new StackTrace(true).ToString(System.Diagnostics.StackTrace.TraceFormat.Normal);
			}
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000339 RID: 825
		public static extern int TickCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002D RID: 45
		// (get) Token: 0x0600033A RID: 826
		public static extern long TickCount64 { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700002E RID: 46
		// (get) Token: 0x0600033B RID: 827 RVA: 0x000B5195 File Offset: 0x000B4395
		public static int ProcessorCount { get; } = Environment.GetProcessorCount();

		// Token: 0x1700002F RID: 47
		// (get) Token: 0x0600033C RID: 828 RVA: 0x000B519C File Offset: 0x000B439C
		internal static bool IsSingleProcessor
		{
			get
			{
				return Environment.ProcessorCount == 1;
			}
		}

		// Token: 0x17000030 RID: 48
		// (get) Token: 0x0600033D RID: 829 RVA: 0x000AC09B File Offset: 0x000AB29B
		public static bool HasShutdownStarted
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600033E RID: 830 RVA: 0x000B51A6 File Offset: 0x000B43A6
		[return: Nullable(2)]
		public static string GetEnvironmentVariable(string variable)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			return Environment.GetEnvironmentVariableCore(variable);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x000B51BC File Offset: 0x000B43BC
		[return: Nullable(2)]
		public static string GetEnvironmentVariable(string variable, EnvironmentVariableTarget target)
		{
			if (target == EnvironmentVariableTarget.Process)
			{
				return Environment.GetEnvironmentVariable(variable);
			}
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			bool fromMachine = Environment.ValidateAndConvertRegistryTarget(target);
			return Environment.GetEnvironmentVariableFromRegistry(variable, fromMachine);
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000B51F0 File Offset: 0x000B43F0
		public static IDictionary GetEnvironmentVariables(EnvironmentVariableTarget target)
		{
			if (target == EnvironmentVariableTarget.Process)
			{
				return Environment.GetEnvironmentVariables();
			}
			bool fromMachine = Environment.ValidateAndConvertRegistryTarget(target);
			return Environment.GetEnvironmentVariablesFromRegistry(fromMachine);
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000B5213 File Offset: 0x000B4413
		public static void SetEnvironmentVariable(string variable, [Nullable(2)] string value)
		{
			Environment.ValidateVariableAndValue(variable, ref value);
			Environment.SetEnvironmentVariableCore(variable, value);
		}

		// Token: 0x06000342 RID: 834 RVA: 0x000B5224 File Offset: 0x000B4424
		public static void SetEnvironmentVariable(string variable, [Nullable(2)] string value, EnvironmentVariableTarget target)
		{
			if (target == EnvironmentVariableTarget.Process)
			{
				Environment.SetEnvironmentVariable(variable, value);
				return;
			}
			Environment.ValidateVariableAndValue(variable, ref value);
			bool fromMachine = Environment.ValidateAndConvertRegistryTarget(target);
			Environment.SetEnvironmentVariableFromRegistry(variable, value, fromMachine);
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000343 RID: 835 RVA: 0x000B5253 File Offset: 0x000B4453
		public static string CommandLine
		{
			get
			{
				return PasteArguments.Paste(Environment.GetCommandLineArgs(), true);
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x06000344 RID: 836 RVA: 0x000B5260 File Offset: 0x000B4460
		// (set) Token: 0x06000345 RID: 837 RVA: 0x000B5267 File Offset: 0x000B4467
		public static string CurrentDirectory
		{
			get
			{
				return Environment.CurrentDirectoryCore;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.Argument_PathEmpty, "value");
				}
				Environment.CurrentDirectoryCore = value;
			}
		}

		// Token: 0x06000346 RID: 838 RVA: 0x000B5295 File Offset: 0x000B4495
		public static string ExpandEnvironmentVariables(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				return name;
			}
			return Environment.ExpandEnvironmentVariablesCore(name);
		}

		// Token: 0x06000347 RID: 839 RVA: 0x000B52B5 File Offset: 0x000B44B5
		internal static void SetCommandLineArgs(string[] cmdLineArgs)
		{
			Environment.s_commandLineArgs = cmdLineArgs;
		}

		// Token: 0x06000348 RID: 840 RVA: 0x000B52BD File Offset: 0x000B44BD
		public static string GetFolderPath(Environment.SpecialFolder folder)
		{
			return Environment.GetFolderPath(folder, Environment.SpecialFolderOption.None);
		}

		// Token: 0x06000349 RID: 841 RVA: 0x000B52C8 File Offset: 0x000B44C8
		public static string GetFolderPath(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
		{
			if (!Enum.IsDefined(typeof(Environment.SpecialFolder), folder))
			{
				throw new ArgumentOutOfRangeException("folder", folder, SR.Format(SR.Arg_EnumIllegalVal, folder));
			}
			if (option != Environment.SpecialFolderOption.None && !Enum.IsDefined(typeof(Environment.SpecialFolderOption), option))
			{
				throw new ArgumentOutOfRangeException("option", option, SR.Format(SR.Arg_EnumIllegalVal, option));
			}
			return Environment.GetFolderPathCore(folder, option);
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x0600034A RID: 842 RVA: 0x000B534F File Offset: 0x000B454F
		public static int ProcessId
		{
			get
			{
				if (!Environment.s_haveProcessId)
				{
					Environment.s_processId = Environment.GetCurrentProcessId();
					Environment.s_haveProcessId = true;
				}
				return Environment.s_processId;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x0600034B RID: 843 RVA: 0x000B5371 File Offset: 0x000B4571
		public static bool Is64BitProcess
		{
			get
			{
				return IntPtr.Size == 8;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x0600034C RID: 844 RVA: 0x000B537B File Offset: 0x000B457B
		public static bool Is64BitOperatingSystem
		{
			get
			{
				if (!Environment.Is64BitProcess)
				{
				}
				return true;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x0600034D RID: 845 RVA: 0x000B5385 File Offset: 0x000B4585
		public static string NewLine
		{
			get
			{
				return "\r\n";
			}
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600034E RID: 846 RVA: 0x000B538C File Offset: 0x000B458C
		public static OperatingSystem OSVersion
		{
			get
			{
				if (Environment.s_osVersion == null)
				{
					Interlocked.CompareExchange<OperatingSystem>(ref Environment.s_osVersion, Environment.GetOSVersion(), null);
				}
				return Environment.s_osVersion;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600034F RID: 847 RVA: 0x000B53AC File Offset: 0x000B45AC
		public static Version Version
		{
			get
			{
				AssemblyInformationalVersionAttribute customAttribute = typeof(object).Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>();
				string text = (customAttribute != null) ? customAttribute.InformationalVersion : null;
				ReadOnlySpan<char> readOnlySpan = text.AsSpan();
				int num = readOnlySpan.IndexOfAny('-', '+', ' ');
				if (num != -1)
				{
					readOnlySpan = readOnlySpan.Slice(0, num);
				}
				Version result;
				if (!Version.TryParse(readOnlySpan, out result))
				{
					return new Version();
				}
				return result;
			}
		}

		// Token: 0x06000350 RID: 848 RVA: 0x000B540D File Offset: 0x000B460D
		private static bool ValidateAndConvertRegistryTarget(EnvironmentVariableTarget target)
		{
			if (target == EnvironmentVariableTarget.Machine)
			{
				return true;
			}
			if (target == EnvironmentVariableTarget.User)
			{
				return false;
			}
			throw new ArgumentOutOfRangeException("target", target, SR.Format(SR.Arg_EnumIllegalVal, target));
		}

		// Token: 0x06000351 RID: 849 RVA: 0x000B543C File Offset: 0x000B463C
		private static void ValidateVariableAndValue(string variable, ref string value)
		{
			if (variable == null)
			{
				throw new ArgumentNullException("variable");
			}
			if (variable.Length == 0)
			{
				throw new ArgumentException(SR.Argument_StringZeroLength, "variable");
			}
			if (variable[0] == '\0')
			{
				throw new ArgumentException(SR.Argument_StringFirstCharIsZero, "variable");
			}
			if (variable.Contains('='))
			{
				throw new ArgumentException(SR.Argument_IllegalEnvVarName, "variable");
			}
			if (string.IsNullOrEmpty(value) || value[0] == '\0')
			{
				value = null;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000352 RID: 850 RVA: 0x000B54B8 File Offset: 0x000B46B8
		internal static bool IsWindows8OrAbove
		{
			get
			{
				return Environment.WindowsVersion.IsWindows8OrAbove;
			}
		}

		// Token: 0x06000353 RID: 851 RVA: 0x000B54C0 File Offset: 0x000B46C0
		private static string GetEnvironmentVariableFromRegistry(string variable, bool fromMachine)
		{
			string result;
			using (RegistryKey registryKey = Environment.OpenEnvironmentKeyIfExists(fromMachine, false))
			{
				result = (((registryKey != null) ? registryKey.GetValue(variable) : null) as string);
			}
			return result;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x000B5508 File Offset: 0x000B4708
		private unsafe static void SetEnvironmentVariableFromRegistry(string variable, string value, bool fromMachine)
		{
			if (!fromMachine && variable.Length >= 255)
			{
				throw new ArgumentException(SR.Argument_LongEnvVarValue, "variable");
			}
			using (RegistryKey registryKey = Environment.OpenEnvironmentKeyIfExists(fromMachine, true))
			{
				if (registryKey != null)
				{
					if (value == null)
					{
						registryKey.DeleteValue(variable, false);
					}
					else
					{
						registryKey.SetValue(variable, value);
					}
				}
			}
			fixed (char* pinnableReference = "Environment".GetPinnableReference())
			{
				char* value2 = pinnableReference;
				IntPtr intPtr2;
				IntPtr intPtr = Interop.User32.SendMessageTimeout(new IntPtr(65535), 26, IntPtr.Zero, (IntPtr)((void*)value2), 0, 1000, out intPtr2);
			}
		}

		// Token: 0x06000355 RID: 853 RVA: 0x000B55A8 File Offset: 0x000B47A8
		private static IDictionary GetEnvironmentVariablesFromRegistry(bool fromMachine)
		{
			Hashtable hashtable = new Hashtable();
			using (RegistryKey registryKey = Environment.OpenEnvironmentKeyIfExists(fromMachine, false))
			{
				if (registryKey != null)
				{
					foreach (string text in registryKey.GetValueNames())
					{
						string value = registryKey.GetValue(text, "").ToString();
						try
						{
							hashtable.Add(text, value);
						}
						catch (ArgumentException)
						{
						}
					}
				}
			}
			return hashtable;
		}

		// Token: 0x06000356 RID: 854 RVA: 0x000B562C File Offset: 0x000B482C
		private static RegistryKey OpenEnvironmentKeyIfExists(bool fromMachine, bool writable)
		{
			RegistryKey registryKey;
			string name;
			if (fromMachine)
			{
				registryKey = Registry.LocalMachine;
				name = "System\\CurrentControlSet\\Control\\Session Manager\\Environment";
			}
			else
			{
				registryKey = Registry.CurrentUser;
				name = "Environment";
			}
			return registryKey.OpenSubKey(name, writable);
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000357 RID: 855 RVA: 0x000B5660 File Offset: 0x000B4860
		public unsafe static string UserName
		{
			get
			{
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)80], 40);
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
				Environment.GetUserName(ref valueStringBuilder);
				ReadOnlySpan<char> span = valueStringBuilder.AsSpan();
				int num = span.IndexOf('\\');
				if (num != -1)
				{
					span = span.Slice(num + 1);
				}
				string result = span.ToString();
				valueStringBuilder.Dispose();
				return result;
			}
		}

		// Token: 0x06000358 RID: 856 RVA: 0x000B56C4 File Offset: 0x000B48C4
		private static void GetUserName(ref ValueStringBuilder builder)
		{
			uint num = 0U;
			while (Interop.Secur32.GetUserNameExW(2, builder.GetPinnableReference(), ref num) == Interop.BOOLEAN.FALSE)
			{
				if (Marshal.GetLastWin32Error() != 234)
				{
					builder.Length = 0;
					return;
				}
				builder.EnsureCapacity(checked((int)num));
			}
			builder.Length = (int)num;
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000359 RID: 857 RVA: 0x000B570C File Offset: 0x000B490C
		public unsafe static string UserDomainName
		{
			get
			{
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)80], 40);
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
				Environment.GetUserName(ref valueStringBuilder);
				ReadOnlySpan<char> span = valueStringBuilder.AsSpan();
				int num = span.IndexOf('\\');
				if (num != -1)
				{
					valueStringBuilder.Length = num;
					return valueStringBuilder.ToString();
				}
				initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)128], 64);
				ValueStringBuilder valueStringBuilder2 = new ValueStringBuilder(initialBuffer);
				uint capacity = (uint)valueStringBuilder2.Capacity;
				Span<byte> span2 = new Span<byte>(stackalloc byte[(UIntPtr)68], 68);
				Span<byte> span3 = span2;
				uint num2 = 68U;
				uint num3;
				while (!Interop.Advapi32.LookupAccountNameW(null, valueStringBuilder.GetPinnableReference(), MemoryMarshal.GetReference<byte>(span3), ref num2, valueStringBuilder2.GetPinnableReference(), ref capacity, out num3))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					if (lastWin32Error != 122)
					{
						throw new InvalidOperationException(Win32Marshal.GetMessage(lastWin32Error));
					}
					valueStringBuilder2.EnsureCapacity((int)capacity);
				}
				valueStringBuilder.Dispose();
				valueStringBuilder2.Length = (int)capacity;
				return valueStringBuilder2.ToString();
			}
		}

		// Token: 0x0600035A RID: 858 RVA: 0x000B5800 File Offset: 0x000B4A00
		private static string GetFolderPathCore(Environment.SpecialFolder folder, Environment.SpecialFolderOption option)
		{
			string folderGuid;
			switch (folder)
			{
			case Environment.SpecialFolder.Desktop:
				folderGuid = "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}";
				goto IL_2CB;
			case Environment.SpecialFolder.Programs:
				folderGuid = "{A77F5D77-2E2B-44C3-A6A2-ABA601054A51}";
				goto IL_2CB;
			case Environment.SpecialFolder.Personal:
				folderGuid = "{FDD39AD0-238F-46AF-ADB4-6C85480369C7}";
				goto IL_2CB;
			case Environment.SpecialFolder.Favorites:
				folderGuid = "{1777F761-68AD-4D8A-87BD-30B759FA33DD}";
				goto IL_2CB;
			case Environment.SpecialFolder.Startup:
				folderGuid = "{B97D20BB-F46A-4C97-BA10-5E3608430854}";
				goto IL_2CB;
			case Environment.SpecialFolder.Recent:
				folderGuid = "{AE50C081-EBD2-438A-8655-8A092E34987A}";
				goto IL_2CB;
			case Environment.SpecialFolder.SendTo:
				folderGuid = "{8983036C-27C0-404B-8F08-102D10DCFD74}";
				goto IL_2CB;
			case Environment.SpecialFolder.StartMenu:
				folderGuid = "{625B53C3-AB48-4EC1-BA1F-A1EF4146FC19}";
				goto IL_2CB;
			case Environment.SpecialFolder.MyMusic:
				folderGuid = "{4BD8D571-6D19-48D3-BE97-422220080E43}";
				goto IL_2CB;
			case Environment.SpecialFolder.MyVideos:
				folderGuid = "{18989B1D-99B5-455B-841C-AB7C74E4DDFC}";
				goto IL_2CB;
			case Environment.SpecialFolder.DesktopDirectory:
				folderGuid = "{B4BFCC3A-DB2C-424C-B029-7FE99A87C641}";
				goto IL_2CB;
			case Environment.SpecialFolder.MyComputer:
				folderGuid = "{0AC0837C-BBF8-452A-850D-79D08E667CA7}";
				goto IL_2CB;
			case Environment.SpecialFolder.NetworkShortcuts:
				folderGuid = "{C5ABBF53-E17F-4121-8900-86626FC2C973}";
				goto IL_2CB;
			case Environment.SpecialFolder.Fonts:
				folderGuid = "{FD228CB7-AE11-4AE3-864C-16F3910AB8FE}";
				goto IL_2CB;
			case Environment.SpecialFolder.Templates:
				folderGuid = "{A63293E8-664E-48DB-A079-DF759E0509F7}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonStartMenu:
				folderGuid = "{A4115719-D62E-491D-AA7C-E74B8BE3B067}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonPrograms:
				folderGuid = "{0139D44E-6AFE-49F2-8690-3DAFCAE6FFB8}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonStartup:
				folderGuid = "{82A5EA35-D9CD-47C5-9629-E15D2F714E6E}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonDesktopDirectory:
				folderGuid = "{C4AA340D-F20F-4863-AFEF-F87EF2E6BA25}";
				goto IL_2CB;
			case Environment.SpecialFolder.ApplicationData:
				folderGuid = "{3EB685DB-65F9-4CF6-A03A-E3EF65729F3D}";
				goto IL_2CB;
			case Environment.SpecialFolder.PrinterShortcuts:
				folderGuid = "{76FC4E2D-D6AD-4519-A663-37BD56068185}";
				goto IL_2CB;
			case Environment.SpecialFolder.LocalApplicationData:
				folderGuid = "{F1B32785-6FBA-4FCF-9D55-7B8E7F157091}";
				goto IL_2CB;
			case Environment.SpecialFolder.InternetCache:
				folderGuid = "{352481E8-33BE-4251-BA85-6007CAEDCF9D}";
				goto IL_2CB;
			case Environment.SpecialFolder.Cookies:
				folderGuid = "{2B0F765D-C0E9-4171-908E-08A611B84FF6}";
				goto IL_2CB;
			case Environment.SpecialFolder.History:
				folderGuid = "{D9DC8A3B-B784-432E-A781-5A1130A75963}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonApplicationData:
				folderGuid = "{62AB5D82-FDC1-4DC3-A9DD-070D1D495D97}";
				goto IL_2CB;
			case Environment.SpecialFolder.Windows:
				folderGuid = "{F38BF404-1D43-42F2-9305-67DE0B28FC23}";
				goto IL_2CB;
			case Environment.SpecialFolder.System:
				folderGuid = "{1AC14E77-02E7-4E5D-B744-2EB1AE5198B7}";
				goto IL_2CB;
			case Environment.SpecialFolder.ProgramFiles:
				folderGuid = "{905e63b6-c1bf-494e-b29c-65b732d3d21a}";
				goto IL_2CB;
			case Environment.SpecialFolder.MyPictures:
				folderGuid = "{33E28130-4E1E-4676-835A-98395C3BC3BB}";
				goto IL_2CB;
			case Environment.SpecialFolder.UserProfile:
				folderGuid = "{5E6C858F-0E22-4760-9AFE-EA3317B67173}";
				goto IL_2CB;
			case Environment.SpecialFolder.SystemX86:
				folderGuid = "{D65231B0-B2F1-4857-A4CE-A8E7C6EA7D27}";
				goto IL_2CB;
			case Environment.SpecialFolder.ProgramFilesX86:
				folderGuid = "{7C5A40EF-A0FB-4BFC-874A-C0F2E0B9FA8E}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonProgramFiles:
				folderGuid = "{F7F1ED05-9F6D-47A2-AAAE-29D317C6F066}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonProgramFilesX86:
				folderGuid = "{DE974D24-D9C6-4D3E-BF91-F4455120B917}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonTemplates:
				folderGuid = "{B94237E7-57AC-4347-9151-B08C6C32D1F7}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonDocuments:
				folderGuid = "{ED4824AF-DCE4-45A8-81E2-FC7965083634}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonAdminTools:
				folderGuid = "{D0384E7D-BAC3-4797-8F14-CBA229B392B5}";
				goto IL_2CB;
			case Environment.SpecialFolder.AdminTools:
				folderGuid = "{724EF170-A42D-4FEF-9F26-B60E846FBA4F}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonMusic:
				folderGuid = "{3214FAB5-9757-4298-BB61-92A9DEAA44FF}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonPictures:
				folderGuid = "{B6EBFB86-6907-413C-9AF7-4FC2ABF07CC5}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonVideos:
				folderGuid = "{2400183A-6185-49FB-A2D8-4A392A602BA3}";
				goto IL_2CB;
			case Environment.SpecialFolder.Resources:
				folderGuid = "{8AD10C31-2ADB-4296-A8F7-E4701232C972}";
				goto IL_2CB;
			case Environment.SpecialFolder.LocalizedResources:
				folderGuid = "{2A00375E-224C-49DE-B8D1-440DF7EF3DDC}";
				goto IL_2CB;
			case Environment.SpecialFolder.CommonOemLinks:
				folderGuid = "{C1BAE2D0-10DF-4334-BEDD-7AA20B227A9D}";
				goto IL_2CB;
			case Environment.SpecialFolder.CDBurning:
				folderGuid = "{9E52AB10-F80D-49DF-ACB8-4330F5687855}";
				goto IL_2CB;
			}
			return string.Empty;
			IL_2CB:
			return Environment.GetKnownFolderPath(folderGuid, option);
		}

		// Token: 0x0600035B RID: 859 RVA: 0x000B5AE0 File Offset: 0x000B4CE0
		private static string GetKnownFolderPath(string folderGuid, Environment.SpecialFolderOption option)
		{
			Guid rfid = new Guid(folderGuid);
			string result;
			int num = Interop.Shell32.SHGetKnownFolderPath(rfid, (uint)option, IntPtr.Zero, out result);
			if (num != 0)
			{
				return string.Empty;
			}
			return result;
		}

		// Token: 0x1700003C RID: 60
		// (get) Token: 0x0600035C RID: 860 RVA: 0x000B5B10 File Offset: 0x000B4D10
		// (set) Token: 0x0600035D RID: 861 RVA: 0x000B5BA8 File Offset: 0x000B4DA8
		private unsafe static string CurrentDirectoryCore
		{
			get
			{
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)520], 260);
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
				uint currentDirectory;
				while ((ulong)(currentDirectory = Interop.Kernel32.GetCurrentDirectory((uint)valueStringBuilder.Capacity, valueStringBuilder.GetPinnableReference())) > (ulong)((long)valueStringBuilder.Capacity))
				{
					valueStringBuilder.EnsureCapacity((int)currentDirectory);
				}
				if (currentDirectory == 0U)
				{
					throw Win32Marshal.GetExceptionForLastWin32Error("");
				}
				valueStringBuilder.Length = (int)currentDirectory;
				if (valueStringBuilder.AsSpan().Contains('~'))
				{
					string result = PathHelper.TryExpandShortFileName(ref valueStringBuilder, null);
					valueStringBuilder.Dispose();
					return result;
				}
				return valueStringBuilder.ToString();
			}
			set
			{
				if (!Interop.Kernel32.SetCurrentDirectory(value))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw Win32Marshal.GetExceptionForWin32Error((lastWin32Error == 2) ? 3 : lastWin32Error, value);
				}
			}
		}

		// Token: 0x0600035E RID: 862 RVA: 0x000B5BD2 File Offset: 0x000B4DD2
		public static string[] GetLogicalDrives()
		{
			return DriveInfoInternal.GetLogicalDrives();
		}

		// Token: 0x1700003D RID: 61
		// (get) Token: 0x0600035F RID: 863 RVA: 0x000B5BDC File Offset: 0x000B4DDC
		public static int SystemPageSize
		{
			get
			{
				Interop.Kernel32.SYSTEM_INFO system_INFO;
				Interop.Kernel32.GetSystemInfo(out system_INFO);
				return system_INFO.dwPageSize;
			}
		}

		// Token: 0x06000360 RID: 864 RVA: 0x000B5BF8 File Offset: 0x000B4DF8
		private unsafe static string ExpandEnvironmentVariablesCore(string name)
		{
			Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
			ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
			uint num;
			while ((ulong)(num = Interop.Kernel32.ExpandEnvironmentStrings(name, valueStringBuilder.GetPinnableReference(), (uint)valueStringBuilder.Capacity)) > (ulong)((long)valueStringBuilder.Capacity))
			{
				valueStringBuilder.EnsureCapacity((int)num);
			}
			if (num == 0U)
			{
				Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
			}
			valueStringBuilder.Length = (int)(num - 1U);
			return valueStringBuilder.ToString();
		}

		// Token: 0x1700003E RID: 62
		// (get) Token: 0x06000361 RID: 865 RVA: 0x000B5C6F File Offset: 0x000B4E6F
		public static string MachineName
		{
			get
			{
				string computerName = Interop.Kernel32.GetComputerName();
				if (computerName == null)
				{
					throw new InvalidOperationException(SR.InvalidOperation_ComputerName);
				}
				return computerName;
			}
		}

		// Token: 0x06000362 RID: 866 RVA: 0x000B5C85 File Offset: 0x000B4E85
		private static int GetCurrentProcessId()
		{
			return (int)Interop.Kernel32.GetCurrentProcessId();
		}

		// Token: 0x06000363 RID: 867 RVA: 0x000B5C8C File Offset: 0x000B4E8C
		private unsafe static OperatingSystem GetOSVersion()
		{
			Interop.NtDll.RTL_OSVERSIONINFOEX rtl_OSVERSIONINFOEX;
			if (Interop.NtDll.RtlGetVersionEx(out rtl_OSVERSIONINFOEX) != 0)
			{
				throw new InvalidOperationException(SR.InvalidOperation_GetVersion);
			}
			Version version = new Version((int)rtl_OSVERSIONINFOEX.dwMajorVersion, (int)rtl_OSVERSIONINFOEX.dwMinorVersion, (int)rtl_OSVERSIONINFOEX.dwBuildNumber, 0);
			if (rtl_OSVERSIONINFOEX.szCSDVersion.FixedElementField == '\0')
			{
				return new OperatingSystem(PlatformID.Win32NT, version);
			}
			return new OperatingSystem(PlatformID.Win32NT, version, new string(&rtl_OSVERSIONINFOEX.szCSDVersion.FixedElementField));
		}

		// Token: 0x1700003F RID: 63
		// (get) Token: 0x06000364 RID: 868 RVA: 0x000B5CF8 File Offset: 0x000B4EF8
		public unsafe static string SystemDirectory
		{
			get
			{
				Span<char> initialBuffer = new Span<char>(stackalloc byte[(UIntPtr)64], 32);
				ValueStringBuilder valueStringBuilder = new ValueStringBuilder(initialBuffer);
				uint systemDirectoryW;
				while ((ulong)(systemDirectoryW = Interop.Kernel32.GetSystemDirectoryW(valueStringBuilder.GetPinnableReference(), (uint)valueStringBuilder.Capacity)) > (ulong)((long)valueStringBuilder.Capacity))
				{
					valueStringBuilder.EnsureCapacity((int)systemDirectoryW);
				}
				if (systemDirectoryW == 0U)
				{
					throw Win32Marshal.GetExceptionForLastWin32Error("");
				}
				valueStringBuilder.Length = (int)systemDirectoryW;
				return valueStringBuilder.ToString();
			}
		}

		// Token: 0x17000040 RID: 64
		// (get) Token: 0x06000365 RID: 869 RVA: 0x000B5D68 File Offset: 0x000B4F68
		public unsafe static bool UserInteractive
		{
			get
			{
				IntPtr processWindowStation = Interop.User32.GetProcessWindowStation();
				if (processWindowStation != IntPtr.Zero)
				{
					Interop.User32.USEROBJECTFLAGS userobjectflags = default(Interop.User32.USEROBJECTFLAGS);
					uint num = 0U;
					if (Interop.User32.GetUserObjectInformationW(processWindowStation, 1, (void*)(&userobjectflags), (uint)sizeof(Interop.User32.USEROBJECTFLAGS), ref num))
					{
						return (userobjectflags.dwFlags & 1) != 0;
					}
				}
				return true;
			}
		}

		// Token: 0x17000041 RID: 65
		// (get) Token: 0x06000366 RID: 870 RVA: 0x000B5DB4 File Offset: 0x000B4FB4
		public static long WorkingSet
		{
			get
			{
				Interop.Kernel32.PROCESS_MEMORY_COUNTERS process_MEMORY_COUNTERS = default(Interop.Kernel32.PROCESS_MEMORY_COUNTERS);
				process_MEMORY_COUNTERS.cb = (uint)sizeof(Interop.Kernel32.PROCESS_MEMORY_COUNTERS);
				if (!Interop.Kernel32.GetProcessMemoryInfo(Interop.Kernel32.GetCurrentProcess(), ref process_MEMORY_COUNTERS, process_MEMORY_COUNTERS.cb))
				{
					return 0L;
				}
				return (long)((ulong)process_MEMORY_COUNTERS.WorkingSetSize);
			}
		}

		// Token: 0x06000367 RID: 871 RVA: 0x000B5DF8 File Offset: 0x000B4FF8
		private unsafe static string GetEnvironmentVariableCore(string variable)
		{
			Span<char> span = new Span<char>(stackalloc byte[(UIntPtr)256], 128);
			Span<char> buffer = span;
			int environmentVariable = Interop.Kernel32.GetEnvironmentVariable(variable, buffer);
			if (environmentVariable == 0 && Marshal.GetLastWin32Error() == 203)
			{
				return null;
			}
			if (environmentVariable <= buffer.Length)
			{
				return new string(buffer.Slice(0, environmentVariable));
			}
			char[] array = ArrayPool<char>.Shared.Rent(environmentVariable);
			string result;
			try
			{
				buffer = array;
				environmentVariable = Interop.Kernel32.GetEnvironmentVariable(variable, buffer);
				if ((environmentVariable == 0 && Marshal.GetLastWin32Error() == 203) || environmentVariable > buffer.Length)
				{
					result = null;
				}
				else
				{
					result = new string(buffer.Slice(0, environmentVariable));
				}
			}
			finally
			{
				ArrayPool<char>.Shared.Return(array, false);
			}
			return result;
		}

		// Token: 0x06000368 RID: 872 RVA: 0x000B5EC0 File Offset: 0x000B50C0
		private static void SetEnvironmentVariableCore(string variable, string value)
		{
			if (!Interop.Kernel32.SetEnvironmentVariable(variable, value))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error <= 203)
				{
					if (lastWin32Error != 8)
					{
						if (lastWin32Error != 203)
						{
							goto IL_4F;
						}
						return;
					}
				}
				else
				{
					if (lastWin32Error == 206)
					{
						throw new ArgumentException(SR.Argument_LongEnvVarValue);
					}
					if (lastWin32Error != 1450)
					{
						goto IL_4F;
					}
				}
				throw new OutOfMemoryException(Interop.Kernel32.GetMessage(lastWin32Error));
				IL_4F:
				throw new ArgumentException(Interop.Kernel32.GetMessage(lastWin32Error));
			}
		}

		// Token: 0x06000369 RID: 873 RVA: 0x000B5F28 File Offset: 0x000B5128
		public unsafe static IDictionary GetEnvironmentVariables()
		{
			char* environmentStrings = Interop.Kernel32.GetEnvironmentStrings();
			if (environmentStrings == null)
			{
				throw new OutOfMemoryException();
			}
			IDictionary result;
			try
			{
				char* ptr = environmentStrings;
				while (*ptr != '\0' || ptr[1] != '\0')
				{
					ptr++;
				}
				Span<char> span = new Span<char>((void*)environmentStrings, (int)((long)(ptr - environmentStrings) + 1L));
				Hashtable hashtable = new Hashtable();
				for (int i = 0; i < span.Length; i++)
				{
					int num = i;
					while (*span[i] != '=' && *span[i] != '\0')
					{
						i++;
					}
					if (*span[i] != '\0')
					{
						if (i - num == 0)
						{
							while (*span[i] != '\0')
							{
								i++;
							}
						}
						else
						{
							string key = new string(span.Slice(num, i - num));
							i++;
							int num2 = i;
							while (*span[i] != '\0')
							{
								i++;
							}
							string value = new string(span.Slice(num2, i - num2));
							try
							{
								hashtable.Add(key, value);
							}
							catch (ArgumentException)
							{
							}
						}
					}
				}
				result = hashtable;
			}
			finally
			{
				bool flag = Interop.Kernel32.FreeEnvironmentStrings(environmentStrings);
			}
			return result;
		}

		// Token: 0x04000116 RID: 278
		private static string[] s_commandLineArgs;

		// Token: 0x04000117 RID: 279
		private static int s_processId;

		// Token: 0x04000118 RID: 280
		private static volatile bool s_haveProcessId;

		// Token: 0x04000119 RID: 281
		private static OperatingSystem s_osVersion;

		// Token: 0x02000068 RID: 104
		[NullableContext(0)]
		public enum SpecialFolder
		{
			// Token: 0x0400011B RID: 283
			ApplicationData = 26,
			// Token: 0x0400011C RID: 284
			CommonApplicationData = 35,
			// Token: 0x0400011D RID: 285
			LocalApplicationData = 28,
			// Token: 0x0400011E RID: 286
			Cookies = 33,
			// Token: 0x0400011F RID: 287
			Desktop = 0,
			// Token: 0x04000120 RID: 288
			Favorites = 6,
			// Token: 0x04000121 RID: 289
			History = 34,
			// Token: 0x04000122 RID: 290
			InternetCache = 32,
			// Token: 0x04000123 RID: 291
			Programs = 2,
			// Token: 0x04000124 RID: 292
			MyComputer = 17,
			// Token: 0x04000125 RID: 293
			MyMusic = 13,
			// Token: 0x04000126 RID: 294
			MyPictures = 39,
			// Token: 0x04000127 RID: 295
			MyVideos = 14,
			// Token: 0x04000128 RID: 296
			Recent = 8,
			// Token: 0x04000129 RID: 297
			SendTo,
			// Token: 0x0400012A RID: 298
			StartMenu = 11,
			// Token: 0x0400012B RID: 299
			Startup = 7,
			// Token: 0x0400012C RID: 300
			System = 37,
			// Token: 0x0400012D RID: 301
			Templates = 21,
			// Token: 0x0400012E RID: 302
			DesktopDirectory = 16,
			// Token: 0x0400012F RID: 303
			Personal = 5,
			// Token: 0x04000130 RID: 304
			MyDocuments = 5,
			// Token: 0x04000131 RID: 305
			ProgramFiles = 38,
			// Token: 0x04000132 RID: 306
			CommonProgramFiles = 43,
			// Token: 0x04000133 RID: 307
			AdminTools = 48,
			// Token: 0x04000134 RID: 308
			CDBurning = 59,
			// Token: 0x04000135 RID: 309
			CommonAdminTools = 47,
			// Token: 0x04000136 RID: 310
			CommonDocuments = 46,
			// Token: 0x04000137 RID: 311
			CommonMusic = 53,
			// Token: 0x04000138 RID: 312
			CommonOemLinks = 58,
			// Token: 0x04000139 RID: 313
			CommonPictures = 54,
			// Token: 0x0400013A RID: 314
			CommonStartMenu = 22,
			// Token: 0x0400013B RID: 315
			CommonPrograms,
			// Token: 0x0400013C RID: 316
			CommonStartup,
			// Token: 0x0400013D RID: 317
			CommonDesktopDirectory,
			// Token: 0x0400013E RID: 318
			CommonTemplates = 45,
			// Token: 0x0400013F RID: 319
			CommonVideos = 55,
			// Token: 0x04000140 RID: 320
			Fonts = 20,
			// Token: 0x04000141 RID: 321
			NetworkShortcuts = 19,
			// Token: 0x04000142 RID: 322
			PrinterShortcuts = 27,
			// Token: 0x04000143 RID: 323
			UserProfile = 40,
			// Token: 0x04000144 RID: 324
			CommonProgramFilesX86 = 44,
			// Token: 0x04000145 RID: 325
			ProgramFilesX86 = 42,
			// Token: 0x04000146 RID: 326
			Resources = 56,
			// Token: 0x04000147 RID: 327
			LocalizedResources,
			// Token: 0x04000148 RID: 328
			SystemX86 = 41,
			// Token: 0x04000149 RID: 329
			Windows = 36
		}

		// Token: 0x02000069 RID: 105
		[NullableContext(0)]
		public enum SpecialFolderOption
		{
			// Token: 0x0400014B RID: 331
			None,
			// Token: 0x0400014C RID: 332
			Create = 32768,
			// Token: 0x0400014D RID: 333
			DoNotVerify = 16384
		}

		// Token: 0x0200006A RID: 106
		private static class WindowsVersion
		{
			// Token: 0x0600036B RID: 875 RVA: 0x000B6078 File Offset: 0x000B5278
			private static bool GetIsWindows8OrAbove()
			{
				ulong num = Interop.Kernel32.VerSetConditionMask(0UL, 2U, 3);
				num = Interop.Kernel32.VerSetConditionMask(num, 1U, 3);
				num = Interop.Kernel32.VerSetConditionMask(num, 32U, 3);
				num = Interop.Kernel32.VerSetConditionMask(num, 16U, 3);
				Interop.Kernel32.OSVERSIONINFOEX osversioninfoex = default(Interop.Kernel32.OSVERSIONINFOEX);
				osversioninfoex.dwOSVersionInfoSize = sizeof(Interop.Kernel32.OSVERSIONINFOEX);
				osversioninfoex.dwMajorVersion = 6;
				osversioninfoex.dwMinorVersion = 2;
				osversioninfoex.wServicePackMajor = 0;
				osversioninfoex.wServicePackMinor = 0;
				return Interop.Kernel32.VerifyVersionInfoW(ref osversioninfoex, 51U, num);
			}

			// Token: 0x0400014E RID: 334
			internal static readonly bool IsWindows8OrAbove = Environment.WindowsVersion.GetIsWindows8OrAbove();
		}
	}
}
