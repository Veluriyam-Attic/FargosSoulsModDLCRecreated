using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.IO;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;
using Internal.IO;

namespace System.Runtime.Loader
{
	// Token: 0x02000406 RID: 1030
	[Nullable(0)]
	[NullableContext(1)]
	public class AssemblyLoadContext
	{
		// Token: 0x060032A1 RID: 12961
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr InitializeAssemblyLoadContext(IntPtr ptrAssemblyLoadContext, bool fRepresentsTPALoadContext, bool isCollectible);

		// Token: 0x060032A2 RID: 12962
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void PrepareForAssemblyLoadContextRelease(IntPtr ptrNativeAssemblyLoadContext, IntPtr ptrAssemblyLoadContextStrong);

		// Token: 0x060032A3 RID: 12963
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr LoadFromStream(IntPtr ptrNativeAssemblyLoadContext, IntPtr ptrAssemblyArray, int iAssemblyArrayLen, IntPtr ptrSymbols, int iSymbolArrayLen, ObjectHandleOnStack retAssembly);

		// Token: 0x060032A4 RID: 12964
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void InternalSetProfileRoot(string directoryPath);

		// Token: 0x060032A5 RID: 12965
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern void InternalStartProfile(string profile, IntPtr ptrNativeAssemblyLoadContext);

		// Token: 0x060032A6 RID: 12966
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void LoadFromPath(IntPtr ptrNativeAssemblyLoadContext, string ilPath, string niPath, ObjectHandleOnStack retAssembly);

		// Token: 0x060032A7 RID: 12967
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern Assembly[] GetLoadedAssemblies();

		// Token: 0x060032A8 RID: 12968
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool IsTracingEnabled();

		// Token: 0x060032A9 RID: 12969
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool TraceResolvingHandlerInvoked(string assemblyName, string handlerName, string alcName, string resultAssemblyName, string resultAssemblyPath);

		// Token: 0x060032AA RID: 12970
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool TraceAssemblyResolveHandlerInvoked(string assemblyName, string handlerName, string resultAssemblyName, string resultAssemblyPath);

		// Token: 0x060032AB RID: 12971
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool TraceAssemblyLoadFromResolveHandlerInvoked(string assemblyName, bool isTrackedAssembly, string requestingAssemblyPath, string requestedAssemblyPath);

		// Token: 0x060032AC RID: 12972
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern bool TraceSatelliteSubdirectoryPathProbed(string filePath, int hResult);

		// Token: 0x060032AD RID: 12973 RVA: 0x0016BAA4 File Offset: 0x0016ACA4
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		private Assembly InternalLoadFromPath(string assemblyPath, string nativeImagePath)
		{
			RuntimeAssembly result = null;
			AssemblyLoadContext.LoadFromPath(this._nativeAssemblyLoadContext, assemblyPath, nativeImagePath, ObjectHandleOnStack.Create<RuntimeAssembly>(ref result));
			return result;
		}

		// Token: 0x060032AE RID: 12974 RVA: 0x0016BAC8 File Offset: 0x0016ACC8
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		internal unsafe Assembly InternalLoad(ReadOnlySpan<byte> arrAssembly, ReadOnlySpan<byte> arrSymbols)
		{
			RuntimeAssembly result = null;
			fixed (byte* ptr = arrAssembly.GetPinnableReference())
			{
				byte* value = ptr;
				fixed (byte* pinnableReference = arrSymbols.GetPinnableReference())
				{
					byte* value2 = pinnableReference;
					AssemblyLoadContext.LoadFromStream(this._nativeAssemblyLoadContext, new IntPtr((void*)value), arrAssembly.Length, new IntPtr((void*)value2), arrSymbols.Length, ObjectHandleOnStack.Create<RuntimeAssembly>(ref result));
					ptr = null;
				}
				return result;
			}
		}

		// Token: 0x060032AF RID: 12975
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr LoadFromInMemoryModuleInternal(IntPtr ptrNativeAssemblyLoadContext, IntPtr hModule, ObjectHandleOnStack retAssembly);

		// Token: 0x060032B0 RID: 12976 RVA: 0x0016BB24 File Offset: 0x0016AD24
		internal Assembly LoadFromInMemoryModule(IntPtr moduleHandle)
		{
			if (moduleHandle == IntPtr.Zero)
			{
				throw new ArgumentNullException("moduleHandle");
			}
			object unloadLock = this._unloadLock;
			Assembly result;
			lock (unloadLock)
			{
				this.VerifyIsAlive();
				RuntimeAssembly runtimeAssembly = null;
				AssemblyLoadContext.LoadFromInMemoryModuleInternal(this._nativeAssemblyLoadContext, moduleHandle, ObjectHandleOnStack.Create<RuntimeAssembly>(ref runtimeAssembly));
				result = runtimeAssembly;
			}
			return result;
		}

		// Token: 0x060032B1 RID: 12977 RVA: 0x0016BB98 File Offset: 0x0016AD98
		private static Assembly ResolveSatelliteAssembly(IntPtr gchManagedAssemblyLoadContext, AssemblyName assemblyName)
		{
			AssemblyLoadContext assemblyLoadContext = (AssemblyLoadContext)GCHandle.FromIntPtr(gchManagedAssemblyLoadContext).Target;
			return assemblyLoadContext.ResolveSatelliteAssembly(assemblyName);
		}

		// Token: 0x060032B2 RID: 12978 RVA: 0x0016BBC0 File Offset: 0x0016ADC0
		private static IntPtr ResolveUnmanagedDll(string unmanagedDllName, IntPtr gchManagedAssemblyLoadContext)
		{
			AssemblyLoadContext assemblyLoadContext = (AssemblyLoadContext)GCHandle.FromIntPtr(gchManagedAssemblyLoadContext).Target;
			return assemblyLoadContext.LoadUnmanagedDll(unmanagedDllName);
		}

		// Token: 0x060032B3 RID: 12979 RVA: 0x0016BBE8 File Offset: 0x0016ADE8
		private static IntPtr ResolveUnmanagedDllUsingEvent(string unmanagedDllName, Assembly assembly, IntPtr gchManagedAssemblyLoadContext)
		{
			AssemblyLoadContext assemblyLoadContext = (AssemblyLoadContext)GCHandle.FromIntPtr(gchManagedAssemblyLoadContext).Target;
			return assemblyLoadContext.GetResolvedUnmanagedDll(assembly, unmanagedDllName);
		}

		// Token: 0x060032B4 RID: 12980 RVA: 0x0016BC14 File Offset: 0x0016AE14
		private static Assembly ResolveUsingResolvingEvent(IntPtr gchManagedAssemblyLoadContext, AssemblyName assemblyName)
		{
			AssemblyLoadContext assemblyLoadContext = (AssemblyLoadContext)GCHandle.FromIntPtr(gchManagedAssemblyLoadContext).Target;
			return assemblyLoadContext.ResolveUsingEvent(assemblyName);
		}

		// Token: 0x060032B5 RID: 12981
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern IntPtr GetLoadContextForAssembly(QCallAssembly assembly);

		// Token: 0x060032B6 RID: 12982 RVA: 0x0016BC3C File Offset: 0x0016AE3C
		[return: Nullable(2)]
		public static AssemblyLoadContext GetLoadContext(Assembly assembly)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			AssemblyLoadContext result = null;
			RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
			if (runtimeAssembly != null)
			{
				RuntimeAssembly runtimeAssembly2 = runtimeAssembly;
				IntPtr loadContextForAssembly = AssemblyLoadContext.GetLoadContextForAssembly(new QCallAssembly(ref runtimeAssembly2));
				if (loadContextForAssembly == IntPtr.Zero)
				{
					result = AssemblyLoadContext.Default;
				}
				else
				{
					result = (AssemblyLoadContext)GCHandle.FromIntPtr(loadContextForAssembly).Target;
				}
			}
			return result;
		}

		// Token: 0x060032B7 RID: 12983 RVA: 0x0016BCA9 File Offset: 0x0016AEA9
		public void SetProfileOptimizationRoot(string directoryPath)
		{
			AssemblyLoadContext.InternalSetProfileRoot(directoryPath);
		}

		// Token: 0x060032B8 RID: 12984 RVA: 0x0016BCB1 File Offset: 0x0016AEB1
		[NullableContext(2)]
		public void StartProfileOptimization(string profile)
		{
			AssemblyLoadContext.InternalStartProfile(profile, this._nativeAssemblyLoadContext);
		}

		// Token: 0x060032B9 RID: 12985 RVA: 0x0016BCC0 File Offset: 0x0016AEC0
		private static RuntimeAssembly GetRuntimeAssembly(Assembly asm)
		{
			if (asm == null)
			{
				return null;
			}
			RuntimeAssembly runtimeAssembly = asm as RuntimeAssembly;
			if (runtimeAssembly != null)
			{
				return runtimeAssembly;
			}
			AssemblyBuilder assemblyBuilder = asm as AssemblyBuilder;
			if (assemblyBuilder == null)
			{
				return null;
			}
			return assemblyBuilder.InternalAssembly;
		}

		// Token: 0x060032BA RID: 12986 RVA: 0x0016BCF6 File Offset: 0x0016AEF6
		private static void StartAssemblyLoad(ref Guid activityId, ref Guid relatedActivityId)
		{
			ActivityTracker.Instance.Enable();
			ActivityTracker.Instance.OnStart(NativeRuntimeEventSource.Log.Name, "AssemblyLoad", 0, ref activityId, ref relatedActivityId, EventActivityOptions.Recursive, false);
		}

		// Token: 0x060032BB RID: 12987 RVA: 0x0016BD20 File Offset: 0x0016AF20
		private static void StopAssemblyLoad(ref Guid activityId)
		{
			ActivityTracker.Instance.OnStop(NativeRuntimeEventSource.Log.Name, "AssemblyLoad", 0, ref activityId, false);
		}

		// Token: 0x060032BC RID: 12988 RVA: 0x0016BD3E File Offset: 0x0016AF3E
		private static void InitializeDefaultContext()
		{
			AssemblyLoadContext @default = AssemblyLoadContext.Default;
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060032BD RID: 12989 RVA: 0x0016BD48 File Offset: 0x0016AF48
		// (remove) Token: 0x060032BE RID: 12990 RVA: 0x0016BD80 File Offset: 0x0016AF80
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		private event Func<Assembly, string, IntPtr> _resolvingUnmanagedDll;

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060032BF RID: 12991 RVA: 0x0016BDB8 File Offset: 0x0016AFB8
		// (remove) Token: 0x060032C0 RID: 12992 RVA: 0x0016BDF0 File Offset: 0x0016AFF0
		[Nullable(new byte[]
		{
			2,
			1,
			1,
			1
		})]
		private event Func<AssemblyLoadContext, AssemblyName, Assembly> _resolving;

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060032C1 RID: 12993 RVA: 0x0016BE28 File Offset: 0x0016B028
		// (remove) Token: 0x060032C2 RID: 12994 RVA: 0x0016BE60 File Offset: 0x0016B060
		[Nullable(new byte[]
		{
			2,
			1
		})]
		private event Action<AssemblyLoadContext> _unloading;

		// Token: 0x060032C3 RID: 12995 RVA: 0x0016BE95 File Offset: 0x0016B095
		protected AssemblyLoadContext() : this(false, false, null)
		{
		}

		// Token: 0x060032C4 RID: 12996 RVA: 0x0016BEA0 File Offset: 0x0016B0A0
		protected AssemblyLoadContext(bool isCollectible) : this(false, isCollectible, null)
		{
		}

		// Token: 0x060032C5 RID: 12997 RVA: 0x0016BEAB File Offset: 0x0016B0AB
		[NullableContext(2)]
		public AssemblyLoadContext(string name, bool isCollectible = false) : this(false, isCollectible, name)
		{
		}

		// Token: 0x060032C6 RID: 12998 RVA: 0x0016BEB8 File Offset: 0x0016B0B8
		private protected AssemblyLoadContext(bool representsTPALoadContext, bool isCollectible, string name)
		{
			this._isCollectible = isCollectible;
			this._name = name;
			this._unloadLock = new object();
			if (!isCollectible)
			{
				GC.SuppressFinalize(this);
			}
			GCHandle value = GCHandle.Alloc(this, this.IsCollectible ? GCHandleType.WeakTrackResurrection : GCHandleType.Normal);
			IntPtr ptrAssemblyLoadContext = GCHandle.ToIntPtr(value);
			this._nativeAssemblyLoadContext = AssemblyLoadContext.InitializeAssemblyLoadContext(ptrAssemblyLoadContext, representsTPALoadContext, isCollectible);
			Dictionary<long, WeakReference<AssemblyLoadContext>> obj = AssemblyLoadContext.s_allContexts;
			lock (obj)
			{
				long num = AssemblyLoadContext.s_nextId;
				AssemblyLoadContext.s_nextId = num + 1L;
				this._id = num;
				AssemblyLoadContext.s_allContexts.Add(this._id, new WeakReference<AssemblyLoadContext>(this, true));
			}
		}

		// Token: 0x060032C7 RID: 12999 RVA: 0x0016BF6C File Offset: 0x0016B16C
		~AssemblyLoadContext()
		{
			if (this._unloadLock != null)
			{
				this.InitiateUnload();
			}
		}

		// Token: 0x060032C8 RID: 13000 RVA: 0x0016BFA0 File Offset: 0x0016B1A0
		private void RaiseUnloadEvent()
		{
			Action<AssemblyLoadContext> action = Interlocked.Exchange<Action<AssemblyLoadContext>>(ref this._unloading, null);
			if (action == null)
			{
				return;
			}
			action(this);
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x0016BFBC File Offset: 0x0016B1BC
		private void InitiateUnload()
		{
			this.RaiseUnloadEvent();
			object unloadLock = this._unloadLock;
			lock (unloadLock)
			{
				GCHandle value = GCHandle.Alloc(this, GCHandleType.Normal);
				IntPtr ptrAssemblyLoadContextStrong = GCHandle.ToIntPtr(value);
				AssemblyLoadContext.PrepareForAssemblyLoadContextRelease(this._nativeAssemblyLoadContext, ptrAssemblyLoadContextStrong);
				this._state = AssemblyLoadContext.InternalState.Unloading;
			}
			Dictionary<long, WeakReference<AssemblyLoadContext>> obj = AssemblyLoadContext.s_allContexts;
			lock (obj)
			{
				AssemblyLoadContext.s_allContexts.Remove(this._id);
			}
		}

		// Token: 0x170009D6 RID: 2518
		// (get) Token: 0x060032CA RID: 13002 RVA: 0x0016C05C File Offset: 0x0016B25C
		public IEnumerable<Assembly> Assemblies
		{
			get
			{
				foreach (Assembly assembly in AssemblyLoadContext.GetLoadedAssemblies())
				{
					AssemblyLoadContext loadContext = AssemblyLoadContext.GetLoadContext(assembly);
					if (loadContext == this)
					{
						yield return assembly;
					}
				}
				Assembly[] array = null;
				yield break;
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060032CB RID: 13003 RVA: 0x0016C079 File Offset: 0x0016B279
		// (remove) Token: 0x060032CC RID: 13004 RVA: 0x0016C082 File Offset: 0x0016B282
		[Nullable(new byte[]
		{
			2,
			1,
			1
		})]
		public event Func<Assembly, string, IntPtr> ResolvingUnmanagedDll
		{
			add
			{
				this._resolvingUnmanagedDll += value;
			}
			remove
			{
				this._resolvingUnmanagedDll -= value;
			}
		}

		// Token: 0x14000014 RID: 20
		// (add) Token: 0x060032CD RID: 13005 RVA: 0x0016C08B File Offset: 0x0016B28B
		// (remove) Token: 0x060032CE RID: 13006 RVA: 0x0016C094 File Offset: 0x0016B294
		[Nullable(new byte[]
		{
			2,
			1,
			1,
			2
		})]
		public event Func<AssemblyLoadContext, AssemblyName, Assembly> Resolving
		{
			add
			{
				this._resolving += value;
			}
			remove
			{
				this._resolving -= value;
			}
		}

		// Token: 0x14000015 RID: 21
		// (add) Token: 0x060032CF RID: 13007 RVA: 0x0016C09D File Offset: 0x0016B29D
		// (remove) Token: 0x060032D0 RID: 13008 RVA: 0x0016C0A6 File Offset: 0x0016B2A6
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public event Action<AssemblyLoadContext> Unloading
		{
			add
			{
				this._unloading += value;
			}
			remove
			{
				this._unloading -= value;
			}
		}

		// Token: 0x14000016 RID: 22
		// (add) Token: 0x060032D1 RID: 13009 RVA: 0x0016C0B0 File Offset: 0x0016B2B0
		// (remove) Token: 0x060032D2 RID: 13010 RVA: 0x0016C0E4 File Offset: 0x0016B2E4
		[Nullable(2)]
		internal static event AssemblyLoadEventHandler AssemblyLoad;

		// Token: 0x14000017 RID: 23
		// (add) Token: 0x060032D3 RID: 13011 RVA: 0x0016C118 File Offset: 0x0016B318
		// (remove) Token: 0x060032D4 RID: 13012 RVA: 0x0016C14C File Offset: 0x0016B34C
		[Nullable(2)]
		internal static event ResolveEventHandler TypeResolve;

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x060032D5 RID: 13013 RVA: 0x0016C180 File Offset: 0x0016B380
		// (remove) Token: 0x060032D6 RID: 13014 RVA: 0x0016C1B4 File Offset: 0x0016B3B4
		[Nullable(2)]
		internal static event ResolveEventHandler ResourceResolve;

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x060032D7 RID: 13015 RVA: 0x0016C1E8 File Offset: 0x0016B3E8
		// (remove) Token: 0x060032D8 RID: 13016 RVA: 0x0016C21C File Offset: 0x0016B41C
		[Nullable(2)]
		internal static event ResolveEventHandler AssemblyResolve;

		// Token: 0x170009D7 RID: 2519
		// (get) Token: 0x060032D9 RID: 13017 RVA: 0x0016C24F File Offset: 0x0016B44F
		public static AssemblyLoadContext Default
		{
			get
			{
				return DefaultAssemblyLoadContext.s_loadContext;
			}
		}

		// Token: 0x170009D8 RID: 2520
		// (get) Token: 0x060032DA RID: 13018 RVA: 0x0016C256 File Offset: 0x0016B456
		public bool IsCollectible
		{
			get
			{
				return this._isCollectible;
			}
		}

		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x060032DB RID: 13019 RVA: 0x0016C25E File Offset: 0x0016B45E
		[Nullable(2)]
		public string Name
		{
			[NullableContext(2)]
			get
			{
				return this._name;
			}
		}

		// Token: 0x060032DC RID: 13020 RVA: 0x0016C268 File Offset: 0x0016B468
		public override string ToString()
		{
			return string.Concat(new string[]
			{
				"\"",
				this.Name,
				"\" ",
				base.GetType().ToString(),
				" #",
				this._id.ToString()
			});
		}

		// Token: 0x170009DA RID: 2522
		// (get) Token: 0x060032DD RID: 13021 RVA: 0x0016C2C0 File Offset: 0x0016B4C0
		public static IEnumerable<AssemblyLoadContext> All
		{
			get
			{
				AssemblyLoadContext @default = AssemblyLoadContext.Default;
				List<WeakReference<AssemblyLoadContext>> list = null;
				Dictionary<long, WeakReference<AssemblyLoadContext>> obj = AssemblyLoadContext.s_allContexts;
				lock (obj)
				{
					list = new List<WeakReference<AssemblyLoadContext>>(AssemblyLoadContext.s_allContexts.Values);
				}
				foreach (WeakReference<AssemblyLoadContext> weakReference in list)
				{
					AssemblyLoadContext assemblyLoadContext;
					if (weakReference.TryGetTarget(out assemblyLoadContext))
					{
						yield return assemblyLoadContext;
					}
				}
				List<WeakReference<AssemblyLoadContext>>.Enumerator enumerator = default(List<WeakReference<AssemblyLoadContext>>.Enumerator);
				yield break;
				yield break;
			}
		}

		// Token: 0x060032DE RID: 13022 RVA: 0x0016C2D6 File Offset: 0x0016B4D6
		public static AssemblyName GetAssemblyName(string assemblyPath)
		{
			if (assemblyPath == null)
			{
				throw new ArgumentNullException("assemblyPath");
			}
			return AssemblyName.GetAssemblyName(assemblyPath);
		}

		// Token: 0x060032DF RID: 13023 RVA: 0x000C26FD File Offset: 0x000C18FD
		[return: Nullable(2)]
		protected virtual Assembly Load(AssemblyName assemblyName)
		{
			return null;
		}

		// Token: 0x060032E0 RID: 13024 RVA: 0x0016C2EC File Offset: 0x0016B4EC
		public Assembly LoadFromAssemblyName(AssemblyName assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyName, ref stackCrawlMark, this);
		}

		// Token: 0x060032E1 RID: 13025 RVA: 0x0016C314 File Offset: 0x0016B514
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public Assembly LoadFromAssemblyPath(string assemblyPath)
		{
			if (assemblyPath == null)
			{
				throw new ArgumentNullException("assemblyPath");
			}
			if (PathInternal.IsPartiallyQualified(assemblyPath))
			{
				throw new ArgumentException(SR.Format(SR.Argument_AbsolutePathRequired, assemblyPath), "assemblyPath");
			}
			object unloadLock = this._unloadLock;
			Assembly result;
			lock (unloadLock)
			{
				this.VerifyIsAlive();
				result = this.InternalLoadFromPath(assemblyPath, null);
			}
			return result;
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x0016C390 File Offset: 0x0016B590
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public Assembly LoadFromNativeImagePath(string nativeImagePath, [Nullable(2)] string assemblyPath)
		{
			if (nativeImagePath == null)
			{
				throw new ArgumentNullException("nativeImagePath");
			}
			if (PathInternal.IsPartiallyQualified(nativeImagePath))
			{
				throw new ArgumentException(SR.Format(SR.Argument_AbsolutePathRequired, nativeImagePath), "nativeImagePath");
			}
			if (assemblyPath != null && PathInternal.IsPartiallyQualified(assemblyPath))
			{
				throw new ArgumentException(SR.Format(SR.Argument_AbsolutePathRequired, assemblyPath), "assemblyPath");
			}
			object unloadLock = this._unloadLock;
			Assembly result;
			lock (unloadLock)
			{
				this.VerifyIsAlive();
				result = this.InternalLoadFromPath(assemblyPath, nativeImagePath);
			}
			return result;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x0016C434 File Offset: 0x0016B634
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public Assembly LoadFromStream(Stream assembly)
		{
			return this.LoadFromStream(assembly, null);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x0016C440 File Offset: 0x0016B640
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public Assembly LoadFromStream(Stream assembly, [Nullable(2)] Stream assemblySymbols)
		{
			if (assembly == null)
			{
				throw new ArgumentNullException("assembly");
			}
			int num = (int)assembly.Length;
			if (num <= 0)
			{
				throw new BadImageFormatException(SR.BadImageFormat_BadILFormat);
			}
			byte[] array = new byte[num];
			assembly.Read(array, 0, num);
			byte[] array2 = null;
			if (assemblySymbols != null)
			{
				int num2 = (int)assemblySymbols.Length;
				array2 = new byte[num2];
				assemblySymbols.Read(array2, 0, num2);
			}
			object unloadLock = this._unloadLock;
			Assembly result;
			lock (unloadLock)
			{
				this.VerifyIsAlive();
				result = this.InternalLoad(array, array2);
			}
			return result;
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x0016C4F0 File Offset: 0x0016B6F0
		protected IntPtr LoadUnmanagedDllFromPath(string unmanagedDllPath)
		{
			if (unmanagedDllPath == null)
			{
				throw new ArgumentNullException("unmanagedDllPath");
			}
			if (unmanagedDllPath.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyPath, "unmanagedDllPath");
			}
			if (PathInternal.IsPartiallyQualified(unmanagedDllPath))
			{
				throw new ArgumentException(SR.Format(SR.Argument_AbsolutePathRequired, unmanagedDllPath), "unmanagedDllPath");
			}
			return NativeLibrary.Load(unmanagedDllPath);
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x0016C54C File Offset: 0x0016B74C
		protected virtual IntPtr LoadUnmanagedDll(string unmanagedDllName)
		{
			return IntPtr.Zero;
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x0016C553 File Offset: 0x0016B753
		public void Unload()
		{
			if (!this.IsCollectible)
			{
				throw new InvalidOperationException(SR.AssemblyLoadContext_Unload_CannotUnloadIfNotCollectible);
			}
			GC.SuppressFinalize(this);
			this.InitiateUnload();
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x0016C574 File Offset: 0x0016B774
		internal static void OnProcessExit()
		{
			Dictionary<long, WeakReference<AssemblyLoadContext>> obj = AssemblyLoadContext.s_allContexts;
			lock (obj)
			{
				foreach (KeyValuePair<long, WeakReference<AssemblyLoadContext>> keyValuePair in AssemblyLoadContext.s_allContexts)
				{
					AssemblyLoadContext assemblyLoadContext;
					if (keyValuePair.Value.TryGetTarget(out assemblyLoadContext))
					{
						assemblyLoadContext.RaiseUnloadEvent();
					}
				}
			}
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x0016C5FC File Offset: 0x0016B7FC
		private void VerifyIsAlive()
		{
			if (this._state != AssemblyLoadContext.InternalState.Alive)
			{
				throw new InvalidOperationException(SR.AssemblyLoadContext_Verify_NotUnloading);
			}
		}

		// Token: 0x170009DB RID: 2523
		// (get) Token: 0x060032EA RID: 13034 RVA: 0x0016C611 File Offset: 0x0016B811
		[Nullable(2)]
		public static AssemblyLoadContext CurrentContextualReflectionContext
		{
			[NullableContext(2)]
			get
			{
				AsyncLocal<AssemblyLoadContext> asyncLocal = AssemblyLoadContext.s_asyncLocalCurrent;
				if (asyncLocal == null)
				{
					return null;
				}
				return asyncLocal.Value;
			}
		}

		// Token: 0x060032EB RID: 13035 RVA: 0x0016C623 File Offset: 0x0016B823
		private static void SetCurrentContextualReflectionContext(AssemblyLoadContext value)
		{
			if (AssemblyLoadContext.s_asyncLocalCurrent == null)
			{
				Interlocked.CompareExchange<AsyncLocal<AssemblyLoadContext>>(ref AssemblyLoadContext.s_asyncLocalCurrent, new AsyncLocal<AssemblyLoadContext>(), null);
			}
			AssemblyLoadContext.s_asyncLocalCurrent.Value = value;
		}

		// Token: 0x060032EC RID: 13036 RVA: 0x0016C648 File Offset: 0x0016B848
		public AssemblyLoadContext.ContextualReflectionScope EnterContextualReflection()
		{
			return new AssemblyLoadContext.ContextualReflectionScope(this);
		}

		// Token: 0x060032ED RID: 13037 RVA: 0x0016C650 File Offset: 0x0016B850
		[NullableContext(2)]
		public static AssemblyLoadContext.ContextualReflectionScope EnterContextualReflection(Assembly activating)
		{
			if (activating == null)
			{
				return new AssemblyLoadContext.ContextualReflectionScope(null);
			}
			AssemblyLoadContext loadContext = AssemblyLoadContext.GetLoadContext(activating);
			if (loadContext == null)
			{
				throw new ArgumentException(SR.Arg_MustBeRuntimeAssembly, "activating");
			}
			return loadContext.EnterContextualReflection();
		}

		// Token: 0x060032EE RID: 13038 RVA: 0x0016C690 File Offset: 0x0016B890
		private static Assembly Resolve(IntPtr gchManagedAssemblyLoadContext, AssemblyName assemblyName)
		{
			AssemblyLoadContext assemblyLoadContext = (AssemblyLoadContext)GCHandle.FromIntPtr(gchManagedAssemblyLoadContext).Target;
			return assemblyLoadContext.ResolveUsingLoad(assemblyName);
		}

		// Token: 0x060032EF RID: 13039 RVA: 0x0016C6B8 File Offset: 0x0016B8B8
		private Assembly GetFirstResolvedAssemblyFromResolvingEvent(AssemblyName assemblyName)
		{
			Func<AssemblyLoadContext, AssemblyName, Assembly> resolving = this._resolving;
			if (resolving != null)
			{
				foreach (Func<AssemblyLoadContext, AssemblyName, Assembly> func in resolving.GetInvocationList())
				{
					Assembly assembly = func(this, assemblyName);
					if (AssemblyLoadContext.IsTracingEnabled())
					{
						AssemblyLoadContext.TraceResolvingHandlerInvoked(assemblyName.FullName, func.Method.Name, (this != AssemblyLoadContext.Default) ? this.ToString() : this.Name, (assembly != null) ? assembly.FullName : null, (assembly != null && !assembly.IsDynamic) ? assembly.Location : null);
					}
					if (assembly != null)
					{
						return assembly;
					}
				}
			}
			return null;
		}

		// Token: 0x060032F0 RID: 13040 RVA: 0x0016C768 File Offset: 0x0016B968
		private static Assembly ValidateAssemblyNameWithSimpleName(Assembly assembly, string requestedSimpleName)
		{
			if (string.IsNullOrEmpty(requestedSimpleName))
			{
				throw new ArgumentException(SR.ArgumentNull_AssemblyNameName);
			}
			string value = null;
			RuntimeAssembly runtimeAssembly = assembly as RuntimeAssembly;
			if (runtimeAssembly != null)
			{
				value = runtimeAssembly.GetSimpleName();
			}
			if (string.IsNullOrEmpty(value) || !requestedSimpleName.Equals(value, StringComparison.InvariantCultureIgnoreCase))
			{
				throw new InvalidOperationException(SR.Argument_CustomAssemblyLoadContextRequestedNameMismatch);
			}
			return assembly;
		}

		// Token: 0x060032F1 RID: 13041 RVA: 0x0016C7C0 File Offset: 0x0016B9C0
		private Assembly ResolveUsingLoad(AssemblyName assemblyName)
		{
			string name = assemblyName.Name;
			Assembly assembly = this.Load(assemblyName);
			if (assembly != null)
			{
				assembly = AssemblyLoadContext.ValidateAssemblyNameWithSimpleName(assembly, name);
			}
			return assembly;
		}

		// Token: 0x060032F2 RID: 13042 RVA: 0x0016C7F0 File Offset: 0x0016B9F0
		private Assembly ResolveUsingEvent(AssemblyName assemblyName)
		{
			string name = assemblyName.Name;
			Assembly assembly = this.GetFirstResolvedAssemblyFromResolvingEvent(assemblyName);
			if (assembly != null)
			{
				assembly = AssemblyLoadContext.ValidateAssemblyNameWithSimpleName(assembly, name);
			}
			return assembly;
		}

		// Token: 0x060032F3 RID: 13043 RVA: 0x0016C81E File Offset: 0x0016BA1E
		private static void OnAssemblyLoad(RuntimeAssembly assembly)
		{
			AssemblyLoadEventHandler assemblyLoad = AssemblyLoadContext.AssemblyLoad;
			if (assemblyLoad == null)
			{
				return;
			}
			assemblyLoad(AppDomain.CurrentDomain, new AssemblyLoadEventArgs(assembly));
		}

		// Token: 0x060032F4 RID: 13044 RVA: 0x0016C83A File Offset: 0x0016BA3A
		private static RuntimeAssembly OnResourceResolve(RuntimeAssembly assembly, string resourceName)
		{
			return AssemblyLoadContext.InvokeResolveEvent(AssemblyLoadContext.ResourceResolve, assembly, resourceName);
		}

		// Token: 0x060032F5 RID: 13045 RVA: 0x0016C848 File Offset: 0x0016BA48
		private static RuntimeAssembly OnTypeResolve(RuntimeAssembly assembly, string typeName)
		{
			return AssemblyLoadContext.InvokeResolveEvent(AssemblyLoadContext.TypeResolve, assembly, typeName);
		}

		// Token: 0x060032F6 RID: 13046 RVA: 0x0016C856 File Offset: 0x0016BA56
		private static RuntimeAssembly OnAssemblyResolve(RuntimeAssembly assembly, string assemblyFullName)
		{
			return AssemblyLoadContext.InvokeResolveEvent(AssemblyLoadContext.AssemblyResolve, assembly, assemblyFullName);
		}

		// Token: 0x060032F7 RID: 13047 RVA: 0x0016C864 File Offset: 0x0016BA64
		private static RuntimeAssembly InvokeResolveEvent(ResolveEventHandler eventHandler, RuntimeAssembly assembly, string name)
		{
			if (eventHandler == null)
			{
				return null;
			}
			ResolveEventArgs args = new ResolveEventArgs(name, assembly);
			foreach (ResolveEventHandler resolveEventHandler in eventHandler.GetInvocationList())
			{
				Assembly assembly2 = resolveEventHandler(AppDomain.CurrentDomain, args);
				if (eventHandler == AssemblyLoadContext.AssemblyResolve && AssemblyLoadContext.IsTracingEnabled())
				{
					AssemblyLoadContext.TraceAssemblyResolveHandlerInvoked(name, resolveEventHandler.Method.Name, (assembly2 != null) ? assembly2.FullName : null, (assembly2 != null && !assembly2.IsDynamic) ? assembly2.Location : null);
				}
				RuntimeAssembly runtimeAssembly = AssemblyLoadContext.GetRuntimeAssembly(assembly2);
				if (runtimeAssembly != null)
				{
					return runtimeAssembly;
				}
			}
			return null;
		}

		// Token: 0x060032F8 RID: 13048 RVA: 0x0016C918 File Offset: 0x0016BB18
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2026:RequiresUnreferencedCode", Justification = "Satellite assemblies have no code in them and loading is not a problem")]
		private Assembly ResolveSatelliteAssembly(AssemblyName assemblyName)
		{
			if (assemblyName.Name == null || !assemblyName.Name.EndsWith(".resources", StringComparison.Ordinal))
			{
				return null;
			}
			string assemblyName2 = assemblyName.Name.Substring(0, assemblyName.Name.Length - ".resources".Length);
			Assembly assembly = this.LoadFromAssemblyName(new AssemblyName(assemblyName2));
			AssemblyLoadContext loadContext = AssemblyLoadContext.GetLoadContext(assembly);
			string directoryName = Path.GetDirectoryName(assembly.Location);
			if (directoryName == null)
			{
				return null;
			}
			string text = Path.Combine(directoryName, assemblyName.CultureName, assemblyName.Name + ".dll");
			bool flag = File.InternalExists(text);
			if (flag || Path.IsCaseSensitive)
			{
			}
			Assembly result = flag ? loadContext.LoadFromAssemblyPath(text) : null;
			if (AssemblyLoadContext.IsTracingEnabled())
			{
				AssemblyLoadContext.TraceSatelliteSubdirectoryPathProbed(text, flag ? 0 : -2147024894);
			}
			return result;
		}

		// Token: 0x060032F9 RID: 13049 RVA: 0x0016C9EC File Offset: 0x0016BBEC
		internal IntPtr GetResolvedUnmanagedDll(Assembly assembly, string unmanagedDllName)
		{
			IntPtr intPtr = IntPtr.Zero;
			Func<Assembly, string, IntPtr> resolvingUnmanagedDll = this._resolvingUnmanagedDll;
			if (resolvingUnmanagedDll != null)
			{
				foreach (Func<Assembly, string, IntPtr> func in resolvingUnmanagedDll.GetInvocationList())
				{
					intPtr = func(assembly, unmanagedDllName);
					if (intPtr != IntPtr.Zero)
					{
						return intPtr;
					}
				}
			}
			return IntPtr.Zero;
		}

		// Token: 0x04000E44 RID: 3652
		private const string AssemblyLoadName = "AssemblyLoad";

		// Token: 0x04000E45 RID: 3653
		private static readonly Dictionary<long, WeakReference<AssemblyLoadContext>> s_allContexts = new Dictionary<long, WeakReference<AssemblyLoadContext>>();

		// Token: 0x04000E46 RID: 3654
		private static long s_nextId;

		// Token: 0x04000E47 RID: 3655
		private readonly object _unloadLock;

		// Token: 0x04000E4B RID: 3659
		private readonly string _name;

		// Token: 0x04000E4C RID: 3660
		private readonly IntPtr _nativeAssemblyLoadContext;

		// Token: 0x04000E4D RID: 3661
		private readonly long _id;

		// Token: 0x04000E4E RID: 3662
		private AssemblyLoadContext.InternalState _state;

		// Token: 0x04000E4F RID: 3663
		private readonly bool _isCollectible;

		// Token: 0x04000E54 RID: 3668
		private static AsyncLocal<AssemblyLoadContext> s_asyncLocalCurrent;

		// Token: 0x02000407 RID: 1031
		private enum InternalState
		{
			// Token: 0x04000E56 RID: 3670
			Alive,
			// Token: 0x04000E57 RID: 3671
			Unloading
		}

		// Token: 0x02000408 RID: 1032
		[EditorBrowsable(EditorBrowsableState.Never)]
		[NullableContext(0)]
		public struct ContextualReflectionScope : IDisposable
		{
			// Token: 0x060032FB RID: 13051 RVA: 0x0016CA52 File Offset: 0x0016BC52
			internal ContextualReflectionScope(AssemblyLoadContext activating)
			{
				this._predecessor = AssemblyLoadContext.CurrentContextualReflectionContext;
				AssemblyLoadContext.SetCurrentContextualReflectionContext(activating);
				this._activated = activating;
				this._initialized = true;
			}

			// Token: 0x060032FC RID: 13052 RVA: 0x0016CA73 File Offset: 0x0016BC73
			public void Dispose()
			{
				if (this._initialized)
				{
					AssemblyLoadContext.SetCurrentContextualReflectionContext(this._predecessor);
				}
			}

			// Token: 0x04000E58 RID: 3672
			private readonly AssemblyLoadContext _activated;

			// Token: 0x04000E59 RID: 3673
			private readonly AssemblyLoadContext _predecessor;

			// Token: 0x04000E5A RID: 3674
			private readonly bool _initialized;
		}
	}
}
