using System;
using System.Configuration.Assemblies;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Loader;
using System.Runtime.Remoting;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.Threading;

namespace System
{
	// Token: 0x020000C0 RID: 192
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AppDomain : MarshalByRefObject
	{
		// Token: 0x060009C7 RID: 2503 RVA: 0x000C7DDA File Offset: 0x000C6FDA
		private AppDomain()
		{
		}

		// Token: 0x170000F0 RID: 240
		// (get) Token: 0x060009C8 RID: 2504 RVA: 0x000C7DF4 File Offset: 0x000C6FF4
		public static AppDomain CurrentDomain
		{
			get
			{
				return AppDomain.s_domain;
			}
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x060009C9 RID: 2505 RVA: 0x000C7DFB File Offset: 0x000C6FFB
		public string BaseDirectory
		{
			get
			{
				return AppContext.BaseDirectory;
			}
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x060009CA RID: 2506 RVA: 0x000C26FD File Offset: 0x000C18FD
		[Nullable(2)]
		public string RelativeSearchPath
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x060009CB RID: 2507 RVA: 0x000C7E02 File Offset: 0x000C7002
		public AppDomainSetup SetupInformation
		{
			get
			{
				return new AppDomainSetup();
			}
		}

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x060009CC RID: 2508 RVA: 0x000C7E09 File Offset: 0x000C7009
		[Obsolete("Code Access Security is not supported or honored by the runtime.", DiagnosticId = "SYSLIB0003", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public PermissionSet PermissionSet
		{
			get
			{
				return new PermissionSet(PermissionState.Unrestricted);
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060009CD RID: 2509 RVA: 0x000C7E11 File Offset: 0x000C7011
		// (remove) Token: 0x060009CE RID: 2510 RVA: 0x000C7E19 File Offset: 0x000C7019
		[Nullable(2)]
		public event UnhandledExceptionEventHandler UnhandledException
		{
			[NullableContext(2)]
			add
			{
				AppContext.UnhandledException += value;
			}
			[NullableContext(2)]
			remove
			{
				AppContext.UnhandledException -= value;
			}
		}

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x060009CF RID: 2511 RVA: 0x000C26FD File Offset: 0x000C18FD
		[Nullable(2)]
		public string DynamicDirectory
		{
			[NullableContext(2)]
			get
			{
				return null;
			}
		}

		// Token: 0x060009D0 RID: 2512 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NullableContext(2)]
		[Obsolete("AppDomain.SetDynamicBase has been deprecated. Please investigate the use of AppDomainSetup.DynamicBase instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetDynamicBase(string path)
		{
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x060009D1 RID: 2513 RVA: 0x000C7E24 File Offset: 0x000C7024
		public string FriendlyName
		{
			get
			{
				Assembly entryAssembly = Assembly.GetEntryAssembly();
				if (!(entryAssembly != null))
				{
					return "DefaultDomain";
				}
				return entryAssembly.GetName().Name;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x060009D2 RID: 2514 RVA: 0x000AC09E File Offset: 0x000AB29E
		public int Id
		{
			get
			{
				return 1;
			}
		}

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x060009D3 RID: 2515 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool IsFullyTrusted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x060009D4 RID: 2516 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool IsHomogenous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x060009D5 RID: 2517 RVA: 0x000C7E54 File Offset: 0x000C7054
		// (remove) Token: 0x060009D6 RID: 2518 RVA: 0x000C7E8C File Offset: 0x000C708C
		[Nullable(2)]
		[method: NullableContext(2)]
		public event EventHandler DomainUnload;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x060009D7 RID: 2519 RVA: 0x000C7EC1 File Offset: 0x000C70C1
		// (remove) Token: 0x060009D8 RID: 2520 RVA: 0x000C7EC9 File Offset: 0x000C70C9
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public event EventHandler<FirstChanceExceptionEventArgs> FirstChanceException
		{
			add
			{
				AppContext.FirstChanceException += value;
			}
			remove
			{
				AppContext.FirstChanceException -= value;
			}
		}

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x060009D9 RID: 2521 RVA: 0x000C7ED1 File Offset: 0x000C70D1
		// (remove) Token: 0x060009DA RID: 2522 RVA: 0x000C7ED9 File Offset: 0x000C70D9
		[Nullable(2)]
		public event EventHandler ProcessExit
		{
			[NullableContext(2)]
			add
			{
				AppContext.ProcessExit += value;
			}
			[NullableContext(2)]
			remove
			{
				AppContext.ProcessExit -= value;
			}
		}

		// Token: 0x060009DB RID: 2523 RVA: 0x000C7EE1 File Offset: 0x000C70E1
		public string ApplyPolicy(string assemblyName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			if (assemblyName.Length == 0 || assemblyName[0] == '\0')
			{
				throw new ArgumentException(SR.Argument_StringZeroLength, "assemblyName");
			}
			return assemblyName;
		}

		// Token: 0x060009DC RID: 2524 RVA: 0x000C7F13 File Offset: 0x000C7113
		public static AppDomain CreateDomain(string friendlyName)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_AppDomains);
		}

		// Token: 0x060009DD RID: 2525 RVA: 0x000C7F2D File Offset: 0x000C712D
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public int ExecuteAssembly(string assemblyFile)
		{
			return this.ExecuteAssembly(assemblyFile, null);
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000C7F38 File Offset: 0x000C7138
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public int ExecuteAssembly(string assemblyFile, [Nullable(2)] string[] args)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			string fullPath = Path.GetFullPath(assemblyFile);
			Assembly assembly = Assembly.LoadFile(fullPath);
			return AppDomain.ExecuteAssembly(assembly, args);
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x000C7F68 File Offset: 0x000C7168
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		[NullableContext(2)]
		public int ExecuteAssembly([Nullable(1)] string assemblyFile, string[] args, byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_CAS);
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x000C7F74 File Offset: 0x000C7174
		private static int ExecuteAssembly(Assembly assembly, string[] args)
		{
			MethodInfo entryPoint = assembly.EntryPoint;
			if (entryPoint == null)
			{
				throw new MissingMethodException(SR.Arg_EntryPointNotFoundException);
			}
			MethodBase methodBase = entryPoint;
			object obj = null;
			BindingFlags invokeAttr = BindingFlags.DoNotWrapExceptions;
			Binder binder = null;
			object parameters;
			if (entryPoint.GetParameters().Length == 0)
			{
				parameters = null;
			}
			else
			{
				(parameters = new object[1])[0] = args;
			}
			object obj2 = methodBase.Invoke(obj, invokeAttr, binder, parameters, null);
			if (obj2 == null)
			{
				return 0;
			}
			return (int)obj2;
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x000C7FCC File Offset: 0x000C71CC
		public int ExecuteAssemblyByName(AssemblyName assemblyName, [Nullable(2)] params string[] args)
		{
			return AppDomain.ExecuteAssembly(Assembly.Load(assemblyName), args);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x000C7FDA File Offset: 0x000C71DA
		public int ExecuteAssemblyByName(string assemblyName)
		{
			return this.ExecuteAssemblyByName(assemblyName, null);
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000C7FE4 File Offset: 0x000C71E4
		public int ExecuteAssemblyByName(string assemblyName, [Nullable(2)] params string[] args)
		{
			return AppDomain.ExecuteAssembly(Assembly.Load(assemblyName), args);
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x000C7FF2 File Offset: 0x000C71F2
		[return: Nullable(2)]
		public object GetData(string name)
		{
			return AppContext.GetData(name);
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x000C7FFA File Offset: 0x000C71FA
		public void SetData(string name, [Nullable(2)] object data)
		{
			AppContext.SetData(name, data);
		}

		// Token: 0x060009E6 RID: 2534 RVA: 0x000C8004 File Offset: 0x000C7204
		public bool? IsCompatibilitySwitchSet(string value)
		{
			bool value2;
			if (!AppContext.TryGetSwitch(value, out value2))
			{
				return null;
			}
			return new bool?(value2);
		}

		// Token: 0x060009E7 RID: 2535 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool IsDefaultAppDomain()
		{
			return true;
		}

		// Token: 0x060009E8 RID: 2536 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool IsFinalizingForUnload()
		{
			return false;
		}

		// Token: 0x060009E9 RID: 2537 RVA: 0x000C802B File Offset: 0x000C722B
		public override string ToString()
		{
			return SR.AppDomain_Name + this.FriendlyName + "\r\n" + SR.AppDomain_NoContextPolicies;
		}

		// Token: 0x060009EA RID: 2538 RVA: 0x000C8047 File Offset: 0x000C7247
		public static void Unload(AppDomain domain)
		{
			if (domain == null)
			{
				throw new ArgumentNullException("domain");
			}
			throw new CannotUnloadAppDomainException(SR.Arg_PlatformNotSupported);
		}

		// Token: 0x060009EB RID: 2539 RVA: 0x000C8061 File Offset: 0x000C7261
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public Assembly Load(byte[] rawAssembly)
		{
			return Assembly.Load(rawAssembly);
		}

		// Token: 0x060009EC RID: 2540 RVA: 0x000C8069 File Offset: 0x000C7269
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public Assembly Load(byte[] rawAssembly, [Nullable(2)] byte[] rawSymbolStore)
		{
			return Assembly.Load(rawAssembly, rawSymbolStore);
		}

		// Token: 0x060009ED RID: 2541 RVA: 0x000C8072 File Offset: 0x000C7272
		public Assembly Load(AssemblyName assemblyRef)
		{
			return Assembly.Load(assemblyRef);
		}

		// Token: 0x060009EE RID: 2542 RVA: 0x000C807A File Offset: 0x000C727A
		public Assembly Load(string assemblyString)
		{
			return Assembly.Load(assemblyString);
		}

		// Token: 0x060009EF RID: 2543 RVA: 0x000C8082 File Offset: 0x000C7282
		public Assembly[] ReflectionOnlyGetAssemblies()
		{
			return Array.Empty<Assembly>();
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x000AC09E File Offset: 0x000AB29E
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x000C8089 File Offset: 0x000C7289
		public static bool MonitoringIsEnabled
		{
			get
			{
				return true;
			}
			set
			{
				if (!value)
				{
					throw new ArgumentException(SR.Arg_MustBeTrue);
				}
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x000C8099 File Offset: 0x000C7299
		public long MonitoringSurvivedMemorySize
		{
			get
			{
				return AppDomain.MonitoringSurvivedProcessMemorySize;
			}
		}

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x060009F3 RID: 2547 RVA: 0x000C80A0 File Offset: 0x000C72A0
		public static long MonitoringSurvivedProcessMemorySize
		{
			get
			{
				GCMemoryInfo gcmemoryInfo = GC.GetGCMemoryInfo();
				return gcmemoryInfo.HeapSizeBytes - gcmemoryInfo.FragmentedBytes;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x000C80C2 File Offset: 0x000C72C2
		public long MonitoringTotalAllocatedMemorySize
		{
			get
			{
				return GC.GetTotalAllocatedBytes(false);
			}
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x000C80CA File Offset: 0x000C72CA
		[Obsolete("AppDomain.GetCurrentThreadId has been deprecated because it does not provide a stable Id when managed threads are running on fibers (aka lightweight threads). To get a stable identifier for a managed thread, use the ManagedThreadId property on Thread.  https://go.microsoft.com/fwlink/?linkid=14202", false)]
		public static int GetCurrentThreadId()
		{
			return Environment.CurrentManagedThreadId;
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x000AC09B File Offset: 0x000AB29B
		public bool ShadowCopyFiles
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Obsolete("AppDomain.AppendPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		[NullableContext(2)]
		public void AppendPrivatePath(string path)
		{
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Obsolete("AppDomain.ClearPrivatePath has been deprecated. Please investigate the use of AppDomainSetup.PrivateBinPath instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		public void ClearPrivatePath()
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Obsolete("AppDomain.ClearShadowCopyPath has been deprecated. Please investigate the use of AppDomainSetup.ShadowCopyDirectories instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		public void ClearShadowCopyPath()
		{
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NullableContext(2)]
		[Obsolete("AppDomain.SetCachePath has been deprecated. Please investigate the use of AppDomainSetup.CachePath instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetCachePath(string path)
		{
		}

		// Token: 0x060009FB RID: 2555 RVA: 0x000AB30B File Offset: 0x000AA50B
		[Obsolete("AppDomain.SetShadowCopyFiles has been deprecated. Please investigate the use of AppDomainSetup.ShadowCopyFiles instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetShadowCopyFiles()
		{
		}

		// Token: 0x060009FC RID: 2556 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NullableContext(2)]
		[Obsolete("AppDomain.SetShadowCopyPath has been deprecated. Please investigate the use of AppDomainSetup.ShadowCopyDirectories instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		public void SetShadowCopyPath(string path)
		{
		}

		// Token: 0x060009FD RID: 2557 RVA: 0x000C80D1 File Offset: 0x000C72D1
		public Assembly[] GetAssemblies()
		{
			return AssemblyLoadContext.GetLoadedAssemblies();
		}

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x060009FE RID: 2558 RVA: 0x000C80D8 File Offset: 0x000C72D8
		// (remove) Token: 0x060009FF RID: 2559 RVA: 0x000C80E0 File Offset: 0x000C72E0
		[Nullable(2)]
		public event AssemblyLoadEventHandler AssemblyLoad
		{
			[NullableContext(2)]
			add
			{
				AssemblyLoadContext.AssemblyLoad += value;
			}
			[NullableContext(2)]
			remove
			{
				AssemblyLoadContext.AssemblyLoad -= value;
			}
		}

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000A00 RID: 2560 RVA: 0x000C80E8 File Offset: 0x000C72E8
		// (remove) Token: 0x06000A01 RID: 2561 RVA: 0x000C80F0 File Offset: 0x000C72F0
		[Nullable(2)]
		public event ResolveEventHandler AssemblyResolve
		{
			[NullableContext(2)]
			add
			{
				AssemblyLoadContext.AssemblyResolve += value;
			}
			[NullableContext(2)]
			remove
			{
				AssemblyLoadContext.AssemblyResolve -= value;
			}
		}

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x06000A02 RID: 2562 RVA: 0x000C80F8 File Offset: 0x000C72F8
		// (remove) Token: 0x06000A03 RID: 2563 RVA: 0x000C8130 File Offset: 0x000C7330
		[Nullable(2)]
		[method: NullableContext(2)]
		public event ResolveEventHandler ReflectionOnlyAssemblyResolve;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x06000A04 RID: 2564 RVA: 0x000C8165 File Offset: 0x000C7365
		// (remove) Token: 0x06000A05 RID: 2565 RVA: 0x000C816D File Offset: 0x000C736D
		[Nullable(2)]
		public event ResolveEventHandler TypeResolve
		{
			[NullableContext(2)]
			add
			{
				AssemblyLoadContext.TypeResolve += value;
			}
			[NullableContext(2)]
			remove
			{
				AssemblyLoadContext.TypeResolve -= value;
			}
		}

		// Token: 0x1400000D RID: 13
		// (add) Token: 0x06000A06 RID: 2566 RVA: 0x000C8175 File Offset: 0x000C7375
		// (remove) Token: 0x06000A07 RID: 2567 RVA: 0x000C817D File Offset: 0x000C737D
		[Nullable(2)]
		public event ResolveEventHandler ResourceResolve
		{
			[NullableContext(2)]
			add
			{
				AssemblyLoadContext.ResourceResolve += value;
			}
			[NullableContext(2)]
			remove
			{
				AssemblyLoadContext.ResourceResolve -= value;
			}
		}

		// Token: 0x06000A08 RID: 2568 RVA: 0x000C8185 File Offset: 0x000C7385
		public void SetPrincipalPolicy(PrincipalPolicy policy)
		{
			this._principalPolicy = policy;
		}

		// Token: 0x06000A09 RID: 2569 RVA: 0x000C8190 File Offset: 0x000C7390
		public void SetThreadPrincipal(IPrincipal principal)
		{
			if (principal == null)
			{
				throw new ArgumentNullException("principal");
			}
			object forLock = this._forLock;
			lock (forLock)
			{
				if (this._defaultPrincipal != null)
				{
					throw new SystemException(SR.AppDomain_Policy_PrincipalTwice);
				}
				this._defaultPrincipal = principal;
			}
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x000C81F4 File Offset: 0x000C73F4
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public ObjectHandle CreateInstance(string assemblyName, string typeName)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName);
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x000C820B File Offset: 0x000C740B
		[NullableContext(2)]
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		public ObjectHandle CreateInstance([Nullable(1)] string assemblyName, [Nullable(1)] string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000C822D File Offset: 0x000C742D
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public ObjectHandle CreateInstance(string assemblyName, string typeName, [Nullable(2)] object[] activationAttributes)
		{
			if (assemblyName == null)
			{
				throw new ArgumentNullException("assemblyName");
			}
			return Activator.CreateInstance(assemblyName, typeName, activationAttributes);
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000C8248 File Offset: 0x000C7448
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x000C826C File Offset: 0x000C746C
		[NullableContext(2)]
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		public object CreateInstanceAndUnwrap([Nullable(1)] string assemblyName, [Nullable(1)] string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x000C8298 File Offset: 0x000C7498
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public object CreateInstanceAndUnwrap(string assemblyName, string typeName, [Nullable(2)] object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstance(assemblyName, typeName, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x000C82BA File Offset: 0x000C74BA
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName);
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x000C82C3 File Offset: 0x000C74C3
		[NullableContext(2)]
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		public ObjectHandle CreateInstanceFrom([Nullable(1)] string assemblyFile, [Nullable(1)] string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
		}

		// Token: 0x06000A12 RID: 2578 RVA: 0x000C82D7 File Offset: 0x000C74D7
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public ObjectHandle CreateInstanceFrom(string assemblyFile, string typeName, [Nullable(2)] object[] activationAttributes)
		{
			return Activator.CreateInstanceFrom(assemblyFile, typeName, activationAttributes);
		}

		// Token: 0x06000A13 RID: 2579 RVA: 0x000C82E4 File Offset: 0x000C74E4
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public object CreateInstanceFromAndUnwrap(string assemblyFile, string typeName)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyFile, typeName);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06000A14 RID: 2580 RVA: 0x000C8308 File Offset: 0x000C7508
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[NullableContext(2)]
		public object CreateInstanceFromAndUnwrap([Nullable(1)] string assemblyFile, [Nullable(1)] string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, object[] args, CultureInfo culture, object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyFile, typeName, ignoreCase, bindingAttr, binder, args, culture, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06000A15 RID: 2581 RVA: 0x000C8334 File Offset: 0x000C7534
		[RequiresUnreferencedCode("Type and its constructor could be removed")]
		[return: Nullable(2)]
		public object CreateInstanceFromAndUnwrap(string assemblyFile, string typeName, [Nullable(2)] object[] activationAttributes)
		{
			ObjectHandle objectHandle = this.CreateInstanceFrom(assemblyFile, typeName, activationAttributes);
			if (objectHandle == null)
			{
				return null;
			}
			return objectHandle.Unwrap();
		}

		// Token: 0x06000A16 RID: 2582 RVA: 0x000C8358 File Offset: 0x000C7558
		[DynamicDependency("GetDefaultInstance", "System.Security.Principal.GenericPrincipal", "System.Security.Claims")]
		[DynamicDependency("GetDefaultInstance", "System.Security.Principal.WindowsPrincipal", "System.Security.Principal.Windows")]
		internal IPrincipal GetThreadPrincipal()
		{
			IPrincipal principal = this._defaultPrincipal;
			if (principal == null)
			{
				PrincipalPolicy principalPolicy = this._principalPolicy;
				if (principalPolicy != PrincipalPolicy.UnauthenticatedPrincipal)
				{
					if (principalPolicy == PrincipalPolicy.WindowsPrincipal)
					{
						if (this.s_getWindowsPrincipal == null)
						{
							Type type = Type.GetType("System.Security.Principal.WindowsPrincipal, System.Security.Principal.Windows", true);
							MethodInfo method = type.GetMethod("GetDefaultInstance", BindingFlags.Static | BindingFlags.NonPublic);
							if (method == null)
							{
								throw new PlatformNotSupportedException(SR.PlatformNotSupported_Principal);
							}
							Volatile.Write<Func<IPrincipal>>(ref this.s_getWindowsPrincipal, method.CreateDelegate<Func<IPrincipal>>());
						}
						principal = this.s_getWindowsPrincipal();
					}
				}
				else
				{
					if (this.s_getUnauthenticatedPrincipal == null)
					{
						Type type2 = Type.GetType("System.Security.Principal.GenericPrincipal, System.Security.Claims", true);
						MethodInfo method2 = type2.GetMethod("GetDefaultInstance", BindingFlags.Static | BindingFlags.NonPublic);
						Volatile.Write<Func<IPrincipal>>(ref this.s_getUnauthenticatedPrincipal, method2.CreateDelegate<Func<IPrincipal>>());
					}
					principal = this.s_getUnauthenticatedPrincipal();
				}
			}
			return principal;
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000A17 RID: 2583 RVA: 0x000C8420 File Offset: 0x000C7620
		public TimeSpan MonitoringTotalProcessorTime
		{
			get
			{
				long num;
				long num2;
				long num3;
				long ticks;
				if (!Interop.Kernel32.GetProcessTimes(Interop.Kernel32.GetCurrentProcess(), out num, out num2, out num3, out ticks))
				{
					return TimeSpan.Zero;
				}
				return new TimeSpan(ticks);
			}
		}

		// Token: 0x04000266 RID: 614
		private static readonly AppDomain s_domain = new AppDomain();

		// Token: 0x04000267 RID: 615
		private readonly object _forLock = new object();

		// Token: 0x04000268 RID: 616
		private IPrincipal _defaultPrincipal;

		// Token: 0x04000269 RID: 617
		private PrincipalPolicy _principalPolicy = PrincipalPolicy.NoPrincipal;

		// Token: 0x0400026A RID: 618
		private Func<IPrincipal> s_getWindowsPrincipal;

		// Token: 0x0400026B RID: 619
		private Func<IPrincipal> s_getUnauthenticatedPrincipal;
	}
}
