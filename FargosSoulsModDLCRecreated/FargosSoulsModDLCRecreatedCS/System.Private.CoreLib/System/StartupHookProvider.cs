using System;
using System.Diagnostics.Tracing;
using System.IO;
using System.Reflection;
using System.Runtime.Loader;

namespace System
{
	// Token: 0x02000095 RID: 149
	internal static class StartupHookProvider
	{
		// Token: 0x06000680 RID: 1664 RVA: 0x000BE4E0 File Offset: 0x000BD6E0
		private unsafe static void ProcessStartupHooks()
		{
			RuntimeEventSource.Initialize();
			string text = AppContext.GetData("STARTUP_HOOKS") as string;
			if (text == null)
			{
				return;
			}
			IntPtr intPtr = stackalloc byte[(UIntPtr)8];
			*intPtr = (short)Path.DirectorySeparatorChar;
			*(intPtr + 2) = (short)Path.AltDirectorySeparatorChar;
			*(intPtr + (IntPtr)2 * 2) = 32;
			*(intPtr + (IntPtr)3 * 2) = 44;
			Span<char> span = new Span<char>(intPtr, 4);
			ReadOnlySpan<char> readOnlySpan = span;
			string[] array = text.Split(Path.PathSeparator, StringSplitOptions.None);
			StartupHookProvider.StartupHookNameOrPath[] array2 = new StartupHookProvider.StartupHookNameOrPath[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				if (!string.IsNullOrEmpty(text2))
				{
					if (Path.IsPathFullyQualified(text2))
					{
						array2[i].Path = text2;
					}
					else
					{
						for (int j = 0; j < readOnlySpan.Length; j++)
						{
							if (text2.Contains((char)(*readOnlySpan[j])))
							{
								throw new ArgumentException(SR.Format(SR.Argument_InvalidStartupHookSimpleAssemblyName, text2));
							}
						}
						if (text2.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
						{
							throw new ArgumentException(SR.Format(SR.Argument_InvalidStartupHookSimpleAssemblyName, text2));
						}
						try
						{
							array2[i].AssemblyName = new AssemblyName(text2);
						}
						catch (Exception innerException)
						{
							throw new ArgumentException(SR.Format(SR.Argument_InvalidStartupHookSimpleAssemblyName, text2), innerException);
						}
					}
				}
			}
			foreach (StartupHookProvider.StartupHookNameOrPath startupHook in array2)
			{
				StartupHookProvider.CallStartupHook(startupHook);
			}
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x000BE65C File Offset: 0x000BD85C
		private static void CallStartupHook(StartupHookProvider.StartupHookNameOrPath startupHook)
		{
			Assembly assembly;
			try
			{
				if (startupHook.Path != null)
				{
					assembly = AssemblyLoadContext.Default.LoadFromAssemblyPath(startupHook.Path);
				}
				else
				{
					if (startupHook.AssemblyName == null)
					{
						return;
					}
					assembly = AssemblyLoadContext.Default.LoadFromAssemblyName(startupHook.AssemblyName);
				}
			}
			catch (Exception innerException)
			{
				throw new ArgumentException(SR.Format(SR.Argument_StartupHookAssemblyLoadFailed, startupHook.Path ?? startupHook.AssemblyName.ToString()), innerException);
			}
			Type type = assembly.GetType("StartupHook", true);
			MethodInfo method = type.GetMethod("Initialize", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic, null, Type.EmptyTypes, null);
			bool flag = false;
			if (method == null)
			{
				try
				{
					method = type.GetMethod("Initialize", BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
				}
				catch (AmbiguousMatchException)
				{
				}
				if (!(method != null))
				{
					throw new MissingMethodException("StartupHook", "Initialize");
				}
				flag = true;
			}
			else if (method.ReturnType != typeof(void))
			{
				flag = true;
			}
			if (flag)
			{
				throw new ArgumentException(SR.Format(SR.Argument_InvalidStartupHookSignature, "StartupHook" + Type.Delimiter.ToString() + "Initialize", startupHook.Path ?? startupHook.AssemblyName.ToString()));
			}
			method.Invoke(null, null);
		}

		// Token: 0x02000096 RID: 150
		private struct StartupHookNameOrPath
		{
			// Token: 0x0400020F RID: 527
			public AssemblyName AssemblyName;

			// Token: 0x04000210 RID: 528
			public string Path;
		}
	}
}
