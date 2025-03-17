using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Loader;
using System.Runtime.Versioning;
using System.Threading;

namespace System
{
	// Token: 0x020000BF RID: 191
	[NullableContext(2)]
	[Nullable(0)]
	public static class AppContext
	{
		// Token: 0x170000EE RID: 238
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x000C79BF File Offset: 0x000C6BBF
		[Nullable(1)]
		public static string BaseDirectory
		{
			[NullableContext(1)]
			get
			{
				string result;
				if ((result = (AppContext.GetData("APP_CONTEXT_BASE_DIRECTORY") as string)) == null && (result = AppContext.s_defaultBaseDirectory) == null)
				{
					result = (AppContext.s_defaultBaseDirectory = AppContext.GetBaseDirectoryCore());
				}
				return result;
			}
		}

		// Token: 0x170000EF RID: 239
		// (get) Token: 0x060009B9 RID: 2489 RVA: 0x000C79E8 File Offset: 0x000C6BE8
		public static string TargetFrameworkName
		{
			get
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				if (entryAssembly == null)
				{
					return null;
				}
				TargetFrameworkAttribute customAttribute = entryAssembly.GetCustomAttribute<TargetFrameworkAttribute>();
				if (customAttribute == null)
				{
					return null;
				}
				return customAttribute.FrameworkName;
			}
		}

		// Token: 0x060009BA RID: 2490 RVA: 0x000C7A08 File Offset: 0x000C6C08
		[NullableContext(1)]
		[return: Nullable(2)]
		public static object GetData(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (AppContext.s_dataStore == null)
			{
				return null;
			}
			Dictionary<string, object> obj = AppContext.s_dataStore;
			object result;
			lock (obj)
			{
				AppContext.s_dataStore.TryGetValue(name, out result);
			}
			return result;
		}

		// Token: 0x060009BB RID: 2491 RVA: 0x000C7A68 File Offset: 0x000C6C68
		[NullableContext(1)]
		public static void SetData(string name, [Nullable(2)] object data)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (AppContext.s_dataStore == null)
			{
				Interlocked.CompareExchange<Dictionary<string, object>>(ref AppContext.s_dataStore, new Dictionary<string, object>(), null);
			}
			Dictionary<string, object> obj = AppContext.s_dataStore;
			lock (obj)
			{
				AppContext.s_dataStore[name] = data;
			}
		}

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x060009BC RID: 2492 RVA: 0x000C7AD4 File Offset: 0x000C6CD4
		// (remove) Token: 0x060009BD RID: 2493 RVA: 0x000C7B08 File Offset: 0x000C6D08
		public static event UnhandledExceptionEventHandler UnhandledException;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x060009BE RID: 2494 RVA: 0x000C7B3C File Offset: 0x000C6D3C
		// (remove) Token: 0x060009BF RID: 2495 RVA: 0x000C7B70 File Offset: 0x000C6D70
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public static event EventHandler<FirstChanceExceptionEventArgs> FirstChanceException;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x060009C0 RID: 2496 RVA: 0x000C7BA4 File Offset: 0x000C6DA4
		// (remove) Token: 0x060009C1 RID: 2497 RVA: 0x000C7BD8 File Offset: 0x000C6DD8
		public static event EventHandler ProcessExit;

		// Token: 0x060009C2 RID: 2498 RVA: 0x000C7C0B File Offset: 0x000C6E0B
		internal static void OnProcessExit()
		{
			AssemblyLoadContext.OnProcessExit();
			EventHandler processExit = AppContext.ProcessExit;
			if (processExit == null)
			{
				return;
			}
			processExit(AppDomain.CurrentDomain, EventArgs.Empty);
		}

		// Token: 0x060009C3 RID: 2499 RVA: 0x000C7C2C File Offset: 0x000C6E2C
		[NullableContext(1)]
		public static bool TryGetSwitch(string switchName, out bool isEnabled)
		{
			if (switchName == null)
			{
				throw new ArgumentNullException("switchName");
			}
			if (switchName.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "switchName");
			}
			if (AppContext.s_switches != null)
			{
				Dictionary<string, bool> obj = AppContext.s_switches;
				lock (obj)
				{
					if (AppContext.s_switches.TryGetValue(switchName, out isEnabled))
					{
						return true;
					}
				}
			}
			string text = AppContext.GetData(switchName) as string;
			if (text != null && bool.TryParse(text, out isEnabled))
			{
				return true;
			}
			isEnabled = false;
			return false;
		}

		// Token: 0x060009C4 RID: 2500 RVA: 0x000C7CC4 File Offset: 0x000C6EC4
		[NullableContext(1)]
		public static void SetSwitch(string switchName, bool isEnabled)
		{
			if (switchName == null)
			{
				throw new ArgumentNullException("switchName");
			}
			if (switchName.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "switchName");
			}
			if (AppContext.s_switches == null)
			{
				Interlocked.CompareExchange<Dictionary<string, bool>>(ref AppContext.s_switches, new Dictionary<string, bool>(), null);
			}
			Dictionary<string, bool> obj = AppContext.s_switches;
			lock (obj)
			{
				AppContext.s_switches[switchName] = isEnabled;
			}
		}

		// Token: 0x060009C5 RID: 2501 RVA: 0x000C7D48 File Offset: 0x000C6F48
		internal unsafe static void Setup(char** pNames, char** pValues, int count)
		{
			AppContext.s_dataStore = new Dictionary<string, object>(count);
			for (int i = 0; i < count; i++)
			{
				AppContext.s_dataStore.Add(new string(*(IntPtr*)(pNames + (IntPtr)i * (IntPtr)sizeof(char*) / (IntPtr)sizeof(char*))), new string(*(IntPtr*)(pValues + (IntPtr)i * (IntPtr)sizeof(char*) / (IntPtr)sizeof(char*))));
			}
		}

		// Token: 0x060009C6 RID: 2502 RVA: 0x000C7D98 File Offset: 0x000C6F98
		private static string GetBaseDirectoryCore()
		{
			Assembly entryAssembly = Assembly.GetEntryAssembly();
			string text = Path.GetDirectoryName((entryAssembly != null) ? entryAssembly.Location : null);
			if (text == null)
			{
				return string.Empty;
			}
			if (!Path.EndsInDirectorySeparator(text))
			{
				text += "\\";
			}
			return text;
		}

		// Token: 0x04000260 RID: 608
		private static Dictionary<string, object> s_dataStore;

		// Token: 0x04000261 RID: 609
		private static Dictionary<string, bool> s_switches;

		// Token: 0x04000262 RID: 610
		private static string s_defaultBaseDirectory;
	}
}
