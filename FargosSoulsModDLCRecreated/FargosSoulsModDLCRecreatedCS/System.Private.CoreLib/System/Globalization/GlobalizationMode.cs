using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.InteropServices;

namespace System.Globalization
{
	// Token: 0x02000202 RID: 514
	internal static class GlobalizationMode
	{
		// Token: 0x060020BB RID: 8379 RVA: 0x0012B69B File Offset: 0x0012A89B
		private static bool GetInvariantSwitchValue()
		{
			return GlobalizationMode.GetSwitchValue("System.Globalization.Invariant", "DOTNET_SYSTEM_GLOBALIZATION_INVARIANT");
		}

		// Token: 0x060020BC RID: 8380 RVA: 0x0012B6AC File Offset: 0x0012A8AC
		private static bool TryGetAppLocalIcuSwitchValue([NotNullWhen(true)] out string value)
		{
			return GlobalizationMode.TryGetStringValue("System.Globalization.AppLocalIcu", "DOTNET_SYSTEM_GLOBALIZATION_APPLOCALICU", out value);
		}

		// Token: 0x060020BD RID: 8381 RVA: 0x0012B6C0 File Offset: 0x0012A8C0
		private static bool GetSwitchValue(string switchName, string envVariable)
		{
			bool result;
			if (!AppContext.TryGetSwitch(switchName, out result))
			{
				string environmentVariable = Environment.GetEnvironmentVariable(envVariable);
				if (environmentVariable != null)
				{
					result = (bool.IsTrueStringIgnoreCase(environmentVariable) || environmentVariable.Equals("1"));
				}
			}
			return result;
		}

		// Token: 0x060020BE RID: 8382 RVA: 0x0012B6FE File Offset: 0x0012A8FE
		private static bool TryGetStringValue(string switchName, string envVariable, [NotNullWhen(true)] out string value)
		{
			value = (AppContext.GetData(switchName) as string);
			if (string.IsNullOrEmpty(value))
			{
				value = Environment.GetEnvironmentVariable(envVariable);
				if (string.IsNullOrEmpty(value))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060020BF RID: 8383 RVA: 0x0012B72C File Offset: 0x0012A92C
		private static void LoadAppLocalIcu(string icuSuffixAndVersion)
		{
			ReadOnlySpan<char> suffix = default(ReadOnlySpan<char>);
			int num = icuSuffixAndVersion.IndexOf(':');
			ReadOnlySpan<char> version;
			if (num >= 0)
			{
				suffix = icuSuffixAndVersion.AsSpan().Slice(0, num);
				version = icuSuffixAndVersion.AsSpan().Slice(suffix.Length + 1);
			}
			else
			{
				version = icuSuffixAndVersion;
			}
			GlobalizationMode.LoadAppLocalIcuCore(version, suffix);
		}

		// Token: 0x060020C0 RID: 8384 RVA: 0x0012B787 File Offset: 0x0012A987
		private static string CreateLibraryName(ReadOnlySpan<char> baseName, ReadOnlySpan<char> suffix, ReadOnlySpan<char> extension, ReadOnlySpan<char> version, bool versionAtEnd = false)
		{
			if (!versionAtEnd)
			{
				return baseName + suffix + version + extension;
			}
			return baseName + suffix + extension + version;
		}

		// Token: 0x060020C1 RID: 8385 RVA: 0x0012B7A0 File Offset: 0x0012A9A0
		private static IntPtr LoadLibrary(string library, bool failOnLoadFailure)
		{
			IntPtr result;
			if (!NativeLibrary.TryLoad(library, typeof(object).Assembly, new DllImportSearchPath?(DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.System32), out result) && failOnLoadFailure)
			{
				Environment.FailFast("Failed to load app-local ICU: " + library);
			}
			return result;
		}

		// Token: 0x17000748 RID: 1864
		// (get) Token: 0x060020C2 RID: 8386 RVA: 0x0012B7E6 File Offset: 0x0012A9E6
		internal static bool Invariant { get; } = GlobalizationMode.GetInvariantSwitchValue();

		// Token: 0x17000749 RID: 1865
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0012B7ED File Offset: 0x0012A9ED
		internal static bool UseNls { get; } = !GlobalizationMode.Invariant && (GlobalizationMode.GetSwitchValue("System.Globalization.UseNls", "DOTNET_SYSTEM_GLOBALIZATION_USENLS") || !GlobalizationMode.LoadIcu());

		// Token: 0x060020C4 RID: 8388 RVA: 0x0012B7F4 File Offset: 0x0012A9F4
		private static bool LoadIcu()
		{
			string icuSuffixAndVersion;
			if (!GlobalizationMode.TryGetAppLocalIcuSwitchValue(out icuSuffixAndVersion))
			{
				return Interop.Globalization.LoadICU() != 0;
			}
			GlobalizationMode.LoadAppLocalIcu(icuSuffixAndVersion);
			return true;
		}

		// Token: 0x060020C5 RID: 8389 RVA: 0x0012B81C File Offset: 0x0012AA1C
		private static void LoadAppLocalIcuCore(ReadOnlySpan<char> version, ReadOnlySpan<char> suffix)
		{
			IntPtr intPtr = IntPtr.Zero;
			IntPtr intPtr2 = IntPtr.Zero;
			int num = version.IndexOf('.');
			if (num > 0)
			{
				ReadOnlySpan<char> version2 = version.Slice(0, num);
				intPtr = GlobalizationMode.LoadLibrary(GlobalizationMode.CreateLibraryName("icuuc", suffix, ".dll", version2, false), false);
				if (intPtr != IntPtr.Zero)
				{
					intPtr2 = GlobalizationMode.LoadLibrary(GlobalizationMode.CreateLibraryName("icuin", suffix, ".dll", version2, false), false);
				}
			}
			if (intPtr == IntPtr.Zero)
			{
				intPtr = GlobalizationMode.LoadLibrary(GlobalizationMode.CreateLibraryName("icuuc", suffix, ".dll", version, false), true);
			}
			if (intPtr2 == IntPtr.Zero)
			{
				intPtr2 = GlobalizationMode.LoadLibrary(GlobalizationMode.CreateLibraryName("icuin", suffix, ".dll", version, false), true);
			}
			Interop.Globalization.InitICUFunctions(intPtr, intPtr2, version, suffix);
		}
	}
}
