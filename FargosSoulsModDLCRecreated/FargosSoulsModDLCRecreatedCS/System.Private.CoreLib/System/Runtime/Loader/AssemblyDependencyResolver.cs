using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Internal.IO;

namespace System.Runtime.Loader
{
	// Token: 0x0200040B RID: 1035
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AssemblyDependencyResolver
	{
		// Token: 0x0600330E RID: 13070 RVA: 0x0016CD3C File Offset: 0x0016BF3C
		public AssemblyDependencyResolver(string componentAssemblyPath)
		{
			if (componentAssemblyPath == null)
			{
				throw new ArgumentNullException("componentAssemblyPath");
			}
			string assemblyPathsList = null;
			string nativeSearchPathsList = null;
			string resourceSearchPathsList = null;
			int num = 0;
			StringBuilder errorMessage = new StringBuilder();
			try
			{
				Interop.HostPolicy.corehost_error_writer_fn corehost_error_writer_fn = delegate(string message)
				{
					errorMessage.AppendLine(message);
				};
				IntPtr functionPointerForDelegate = Marshal.GetFunctionPointerForDelegate<Interop.HostPolicy.corehost_error_writer_fn>(corehost_error_writer_fn);
				IntPtr errorWriter = Interop.HostPolicy.corehost_set_error_writer(functionPointerForDelegate);
				try
				{
					num = Interop.HostPolicy.corehost_resolve_component_dependencies(componentAssemblyPath, delegate(string assemblyPaths, string nativeSearchPaths, string resourceSearchPaths)
					{
						assemblyPathsList = assemblyPaths;
						nativeSearchPathsList = nativeSearchPaths;
						resourceSearchPathsList = resourceSearchPaths;
					});
				}
				finally
				{
					Interop.HostPolicy.corehost_set_error_writer(errorWriter);
					GC.KeepAlive(corehost_error_writer_fn);
				}
			}
			catch (EntryPointNotFoundException innerException)
			{
				throw new InvalidOperationException(SR.AssemblyDependencyResolver_FailedToLoadHostpolicy, innerException);
			}
			catch (DllNotFoundException innerException2)
			{
				throw new InvalidOperationException(SR.AssemblyDependencyResolver_FailedToLoadHostpolicy, innerException2);
			}
			if (num != 0)
			{
				throw new InvalidOperationException(SR.Format(SR.AssemblyDependencyResolver_FailedToResolveDependencies, componentAssemblyPath, num, errorMessage));
			}
			string[] array = AssemblyDependencyResolver.SplitPathsList(assemblyPathsList);
			this._assemblyPaths = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
			foreach (string text in array)
			{
				this._assemblyPaths.Add(Path.GetFileNameWithoutExtension(text), text);
			}
			this._nativeSearchPaths = AssemblyDependencyResolver.SplitPathsList(nativeSearchPathsList);
			this._resourceSearchPaths = AssemblyDependencyResolver.SplitPathsList(resourceSearchPathsList);
			this._assemblyDirectorySearchPaths = new string[]
			{
				Path.GetDirectoryName(componentAssemblyPath)
			};
		}

		// Token: 0x0600330F RID: 13071 RVA: 0x0016CEB4 File Offset: 0x0016C0B4
		[return: Nullable(2)]
		public string ResolveAssemblyToPath(AssemblyName assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			string text2;
			if (!string.IsNullOrEmpty(assemblyName.CultureName) && !string.Equals(assemblyName.CultureName, "neutral", StringComparison.OrdinalIgnoreCase))
			{
				foreach (string path in this._resourceSearchPaths)
				{
					string text = Path.Combine(path, assemblyName.CultureName, assemblyName.Name + ".dll");
					if (File.Exists(text))
					{
						return text;
					}
				}
			}
			else if (assemblyName.Name != null && this._assemblyPaths.TryGetValue(assemblyName.Name, out text2) && File.Exists(text2))
			{
				return text2;
			}
			return null;
		}

		// Token: 0x06003310 RID: 13072 RVA: 0x0016CF5C File Offset: 0x0016C15C
		[return: Nullable(2)]
		public string ResolveUnmanagedDllToPath(string unmanagedDllName)
		{
			if (unmanagedDllName == null)
			{
				throw new ArgumentNullException("unmanagedDllName");
			}
			string[] array;
			if (unmanagedDllName.Contains(Path.DirectorySeparatorChar))
			{
				array = this._assemblyDirectorySearchPaths;
			}
			else
			{
				array = this._nativeSearchPaths;
			}
			bool isRelativePath = !Path.IsPathFullyQualified(unmanagedDllName);
			foreach (LibraryNameVariation libraryNameVariation in LibraryNameVariation.DetermineLibraryNameVariations(unmanagedDllName, isRelativePath, false))
			{
				string path = libraryNameVariation.Prefix + unmanagedDllName + libraryNameVariation.Suffix;
				foreach (string path2 in array)
				{
					string text = Path.Combine(path2, path);
					if (File.Exists(text))
					{
						return text;
					}
				}
			}
			return null;
		}

		// Token: 0x06003311 RID: 13073 RVA: 0x0016D028 File Offset: 0x0016C228
		private static string[] SplitPathsList(string pathsList)
		{
			if (pathsList == null)
			{
				return Array.Empty<string>();
			}
			return pathsList.Split(Path.PathSeparator, StringSplitOptions.RemoveEmptyEntries);
		}

		// Token: 0x04000E65 RID: 3685
		private readonly Dictionary<string, string> _assemblyPaths;

		// Token: 0x04000E66 RID: 3686
		private readonly string[] _nativeSearchPaths;

		// Token: 0x04000E67 RID: 3687
		private readonly string[] _resourceSearchPaths;

		// Token: 0x04000E68 RID: 3688
		private readonly string[] _assemblyDirectorySearchPaths;
	}
}
