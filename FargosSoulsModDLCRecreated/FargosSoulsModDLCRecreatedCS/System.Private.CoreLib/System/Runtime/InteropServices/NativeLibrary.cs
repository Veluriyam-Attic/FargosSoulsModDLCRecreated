using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000451 RID: 1105
	[Nullable(0)]
	[NullableContext(1)]
	public static class NativeLibrary
	{
		// Token: 0x060043C7 RID: 17351 RVA: 0x001775C8 File Offset: 0x001767C8
		internal static IntPtr LoadLibraryByName(string libraryName, Assembly assembly, DllImportSearchPath? searchPath, bool throwOnError)
		{
			RuntimeAssembly runtimeAssembly = (RuntimeAssembly)assembly;
			return NativeLibrary.LoadByName(libraryName, new QCallAssembly(ref runtimeAssembly), searchPath != null, (uint)searchPath.GetValueOrDefault(), throwOnError);
		}

		// Token: 0x060043C8 RID: 17352
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr LoadFromPath(string libraryName, bool throwOnError);

		// Token: 0x060043C9 RID: 17353
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr LoadByName(string libraryName, QCallAssembly callingAssembly, bool hasDllImportSearchPathFlag, uint dllImportSearchPathFlag, bool throwOnError);

		// Token: 0x060043CA RID: 17354
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void FreeLib(IntPtr handle);

		// Token: 0x060043CB RID: 17355
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern IntPtr GetSymbol(IntPtr handle, string symbolName, bool throwOnError);

		// Token: 0x060043CC RID: 17356 RVA: 0x001775F8 File Offset: 0x001767F8
		public static IntPtr Load(string libraryPath)
		{
			if (libraryPath == null)
			{
				throw new ArgumentNullException("libraryPath");
			}
			return NativeLibrary.LoadFromPath(libraryPath, true);
		}

		// Token: 0x060043CD RID: 17357 RVA: 0x0017760F File Offset: 0x0017680F
		public static bool TryLoad(string libraryPath, out IntPtr handle)
		{
			if (libraryPath == null)
			{
				throw new ArgumentNullException("libraryPath");
			}
			handle = NativeLibrary.LoadFromPath(libraryPath, false);
			return handle != IntPtr.Zero;
		}

		// Token: 0x060043CE RID: 17358 RVA: 0x00177634 File Offset: 0x00176834
		public static IntPtr Load(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
		{
			if (libraryName == null)
			{
				throw new ArgumentNullException("libraryName");
			}
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!assembly.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeAssembly);
			}
			return NativeLibrary.LoadLibraryByName(libraryName, assembly, searchPath, true);
		}

		// Token: 0x060043CF RID: 17359 RVA: 0x00177674 File Offset: 0x00176874
		public static bool TryLoad(string libraryName, Assembly assembly, DllImportSearchPath? searchPath, out IntPtr handle)
		{
			if (libraryName == null)
			{
				throw new ArgumentNullException("libraryName");
			}
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!assembly.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeAssembly);
			}
			handle = NativeLibrary.LoadLibraryByName(libraryName, assembly, searchPath, false);
			return handle != IntPtr.Zero;
		}

		// Token: 0x060043D0 RID: 17360 RVA: 0x001776CD File Offset: 0x001768CD
		public static void Free(IntPtr handle)
		{
			if (handle == IntPtr.Zero)
			{
				return;
			}
			NativeLibrary.FreeLib(handle);
		}

		// Token: 0x060043D1 RID: 17361 RVA: 0x001776E3 File Offset: 0x001768E3
		public static IntPtr GetExport(IntPtr handle, string name)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			return NativeLibrary.GetSymbol(handle, name, true);
		}

		// Token: 0x060043D2 RID: 17362 RVA: 0x00177713 File Offset: 0x00176913
		public static bool TryGetExport(IntPtr handle, string name, out IntPtr address)
		{
			if (handle == IntPtr.Zero)
			{
				throw new ArgumentNullException("handle");
			}
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			address = NativeLibrary.GetSymbol(handle, name, false);
			return address != IntPtr.Zero;
		}

		// Token: 0x060043D3 RID: 17363 RVA: 0x00177754 File Offset: 0x00176954
		public static void SetDllImportResolver(Assembly assembly, DllImportResolver resolver)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			if (resolver == null)
			{
				throw new ArgumentNullException("resolver");
			}
			if (!assembly.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeAssembly);
			}
			if (NativeLibrary.s_nativeDllResolveMap == null)
			{
				Interlocked.CompareExchange<ConditionalWeakTable<Assembly, DllImportResolver>>(ref NativeLibrary.s_nativeDllResolveMap, new ConditionalWeakTable<Assembly, DllImportResolver>(), null);
			}
			try
			{
				NativeLibrary.s_nativeDllResolveMap.Add(assembly, resolver);
			}
			catch (ArgumentException)
			{
				throw new InvalidOperationException(SR.InvalidOperation_CannotRegisterSecondResolver);
			}
		}

		// Token: 0x060043D4 RID: 17364 RVA: 0x001777DC File Offset: 0x001769DC
		internal static IntPtr LoadLibraryCallbackStub(string libraryName, Assembly assembly, bool hasDllImportSearchPathFlags, uint dllImportSearchPathFlags)
		{
			if (NativeLibrary.s_nativeDllResolveMap == null)
			{
				return IntPtr.Zero;
			}
			DllImportResolver dllImportResolver;
			if (!NativeLibrary.s_nativeDllResolveMap.TryGetValue(assembly, out dllImportResolver))
			{
				return IntPtr.Zero;
			}
			return dllImportResolver(libraryName, assembly, hasDllImportSearchPathFlags ? new DllImportSearchPath?((DllImportSearchPath)dllImportSearchPathFlags) : null);
		}

		// Token: 0x04000EB7 RID: 3767
		private static ConditionalWeakTable<Assembly, DllImportResolver> s_nativeDllResolveMap;
	}
}
