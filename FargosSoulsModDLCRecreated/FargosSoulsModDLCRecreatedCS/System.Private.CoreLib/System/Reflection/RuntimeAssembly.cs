using System;
using System.Collections.Generic;
using System.Configuration.Assemblies;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Loader;
using System.Runtime.Serialization;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x020005A8 RID: 1448
	internal class RuntimeAssembly : Assembly
	{
		// Token: 0x06004A64 RID: 19044 RVA: 0x001870D5 File Offset: 0x001862D5
		internal RuntimeAssembly()
		{
			throw new NotSupportedException();
		}

		// Token: 0x1400001C RID: 28
		// (add) Token: 0x06004A65 RID: 19045 RVA: 0x001870E4 File Offset: 0x001862E4
		// (remove) Token: 0x06004A66 RID: 19046 RVA: 0x0018711C File Offset: 0x0018631C
		private event ModuleResolveEventHandler _ModuleResolve;

		// Token: 0x06004A67 RID: 19047 RVA: 0x00187151 File Offset: 0x00186351
		internal IntPtr GetUnderlyingNativeHandle()
		{
			return this.m_assembly;
		}

		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06004A68 RID: 19048 RVA: 0x00187159 File Offset: 0x00186359
		internal object SyncRoot
		{
			get
			{
				if (this.m_syncRoot == null)
				{
					Interlocked.CompareExchange<object>(ref this.m_syncRoot, new object(), null);
				}
				return this.m_syncRoot;
			}
		}

		// Token: 0x1400001D RID: 29
		// (add) Token: 0x06004A69 RID: 19049 RVA: 0x0018717B File Offset: 0x0018637B
		// (remove) Token: 0x06004A6A RID: 19050 RVA: 0x00187184 File Offset: 0x00186384
		public override event ModuleResolveEventHandler ModuleResolve
		{
			add
			{
				this._ModuleResolve += value;
			}
			remove
			{
				this._ModuleResolve -= value;
			}
		}

		// Token: 0x06004A6B RID: 19051
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern bool GetCodeBase(QCallAssembly assembly, StringHandleOnStack retString);

		// Token: 0x06004A6C RID: 19052 RVA: 0x00187190 File Offset: 0x00186390
		internal string GetCodeBase()
		{
			string result = null;
			RuntimeAssembly runtimeAssembly = this;
			if (RuntimeAssembly.GetCodeBase(new QCallAssembly(ref runtimeAssembly), new StringHandleOnStack(ref result)))
			{
				return result;
			}
			return null;
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06004A6D RID: 19053 RVA: 0x001871BC File Offset: 0x001863BC
		public override string CodeBase
		{
			get
			{
				string codeBase = this.GetCodeBase();
				if (codeBase == null)
				{
					throw new NotSupportedException(SR.NotSupported_CodeBase);
				}
				return codeBase;
			}
		}

		// Token: 0x06004A6E RID: 19054 RVA: 0x000AC098 File Offset: 0x000AB298
		internal RuntimeAssembly GetNativeHandle()
		{
			return this;
		}

		// Token: 0x06004A6F RID: 19055 RVA: 0x001871E0 File Offset: 0x001863E0
		public override AssemblyName GetName(bool copiedName)
		{
			string codeBase = this.GetCodeBase();
			AssemblyName assemblyName = new AssemblyName(this.GetSimpleName(), this.GetPublicKey(), null, this.GetVersion(), this.GetLocale(), this.GetHashAlgorithm(), AssemblyVersionCompatibility.SameMachine, codeBase, this.GetFlags() | AssemblyNameFlags.PublicKey, null);
			Module manifestModule = this.ManifestModule;
			if (manifestModule.MDStreamVersion > 65536)
			{
				PortableExecutableKinds pek;
				ImageFileMachine ifm;
				manifestModule.GetPEKind(out pek, out ifm);
				assemblyName.SetProcArchIndex(pek, ifm);
			}
			return assemblyName;
		}

		// Token: 0x06004A70 RID: 19056
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetFullName(QCallAssembly assembly, StringHandleOnStack retString);

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06004A71 RID: 19057 RVA: 0x0018724C File Offset: 0x0018644C
		public override string FullName
		{
			get
			{
				if (this.m_fullname == null)
				{
					string value = null;
					RuntimeAssembly runtimeAssembly = this;
					RuntimeAssembly.GetFullName(new QCallAssembly(ref runtimeAssembly), new StringHandleOnStack(ref value));
					Interlocked.CompareExchange<string>(ref this.m_fullname, value, null);
				}
				return this.m_fullname;
			}
		}

		// Token: 0x06004A72 RID: 19058
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetEntryPoint(QCallAssembly assembly, ObjectHandleOnStack retMethod);

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06004A73 RID: 19059 RVA: 0x0018728C File Offset: 0x0018648C
		public override MethodInfo EntryPoint
		{
			get
			{
				IRuntimeMethodInfo runtimeMethodInfo = null;
				RuntimeAssembly runtimeAssembly = this;
				RuntimeAssembly.GetEntryPoint(new QCallAssembly(ref runtimeAssembly), ObjectHandleOnStack.Create<IRuntimeMethodInfo>(ref runtimeMethodInfo));
				if (runtimeMethodInfo == null)
				{
					return null;
				}
				return (MethodInfo)RuntimeType.GetMethodBase(runtimeMethodInfo);
			}
		}

		// Token: 0x06004A74 RID: 19060
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetType(QCallAssembly assembly, string name, bool throwOnError, bool ignoreCase, ObjectHandleOnStack type, ObjectHandleOnStack keepAlive, ObjectHandleOnStack assemblyLoadContext);

		// Token: 0x06004A75 RID: 19061 RVA: 0x001872C0 File Offset: 0x001864C0
		[RequiresUnreferencedCode("Types might be removed")]
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			RuntimeType result = null;
			object obj = null;
			AssemblyLoadContext currentContextualReflectionContext = AssemblyLoadContext.CurrentContextualReflectionContext;
			RuntimeAssembly runtimeAssembly = this;
			RuntimeAssembly.GetType(new QCallAssembly(ref runtimeAssembly), name, throwOnError, ignoreCase, ObjectHandleOnStack.Create<RuntimeType>(ref result), ObjectHandleOnStack.Create<object>(ref obj), ObjectHandleOnStack.Create<AssemblyLoadContext>(ref currentContextualReflectionContext));
			GC.KeepAlive(obj);
			return result;
		}

		// Token: 0x06004A76 RID: 19062
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetExportedTypes(QCallAssembly assembly, ObjectHandleOnStack retTypes);

		// Token: 0x06004A77 RID: 19063 RVA: 0x00187314 File Offset: 0x00186514
		[RequiresUnreferencedCode("Types might be removed")]
		public override Type[] GetExportedTypes()
		{
			Type[] result = null;
			RuntimeAssembly runtimeAssembly = this;
			RuntimeAssembly.GetExportedTypes(new QCallAssembly(ref runtimeAssembly), ObjectHandleOnStack.Create<Type[]>(ref result));
			return result;
		}

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06004A78 RID: 19064 RVA: 0x0018733C File Offset: 0x0018653C
		public override IEnumerable<TypeInfo> DefinedTypes
		{
			[RequiresUnreferencedCode("Types might be removed")]
			get
			{
				RuntimeModule[] modulesInternal = this.GetModulesInternal(true, false);
				if (modulesInternal.Length == 1)
				{
					return modulesInternal[0].GetDefinedTypes();
				}
				List<RuntimeType> list = new List<RuntimeType>();
				for (int i = 0; i < modulesInternal.Length; i++)
				{
					list.AddRange(modulesInternal[i].GetDefinedTypes());
				}
				return list.ToArray();
			}
		}

		// Token: 0x06004A79 RID: 19065
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern Interop.BOOL GetIsCollectible(QCallAssembly assembly);

		// Token: 0x17000B74 RID: 2932
		// (get) Token: 0x06004A7A RID: 19066 RVA: 0x0018738C File Offset: 0x0018658C
		public override bool IsCollectible
		{
			get
			{
				RuntimeAssembly runtimeAssembly = this;
				return RuntimeAssembly.GetIsCollectible(new QCallAssembly(ref runtimeAssembly)) > Interop.BOOL.FALSE;
			}
		}

		// Token: 0x06004A7B RID: 19067
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private unsafe static extern byte* GetResource(QCallAssembly assembly, string resourceName, out uint length);

		// Token: 0x06004A7C RID: 19068 RVA: 0x001873AC File Offset: 0x001865AC
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			if (type == null && name == null)
			{
				throw new ArgumentNullException("type");
			}
			string text = (type != null) ? type.Namespace : null;
			char delimiter = Type.Delimiter;
			string name2 = (text != null && name != null) ? (text + new ReadOnlySpan<char>(ref delimiter, 1) + name) : (text + name);
			return this.GetManifestResourceStream(name2);
		}

		// Token: 0x06004A7D RID: 19069 RVA: 0x00187414 File Offset: 0x00186614
		public unsafe override Stream GetManifestResourceStream(string name)
		{
			RuntimeAssembly runtimeAssembly = this;
			uint num;
			byte* resource = RuntimeAssembly.GetResource(new QCallAssembly(ref runtimeAssembly), name, out num);
			if (resource != null)
			{
				return new RuntimeAssembly.ManifestResourceStream(this, resource, (long)((ulong)num), (long)((ulong)num), FileAccess.Read);
			}
			return null;
		}

		// Token: 0x06004A7E RID: 19070 RVA: 0x000B3617 File Offset: 0x000B2817
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x06004A7F RID: 19071 RVA: 0x00187446 File Offset: 0x00186646
		public override Module ManifestModule
		{
			get
			{
				return RuntimeAssembly.GetManifestModule(this.GetNativeHandle());
			}
		}

		// Token: 0x06004A80 RID: 19072 RVA: 0x00187453 File Offset: 0x00186653
		public override object[] GetCustomAttributes(bool inherit)
		{
			return CustomAttribute.GetCustomAttributes(this, typeof(object) as RuntimeType);
		}

		// Token: 0x06004A81 RID: 19073 RVA: 0x0018746C File Offset: 0x0018666C
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.GetCustomAttributes(this, runtimeType);
		}

		// Token: 0x06004A82 RID: 19074 RVA: 0x001874BC File Offset: 0x001866BC
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			RuntimeType runtimeType = attributeType.UnderlyingSystemType as RuntimeType;
			if (runtimeType == null)
			{
				throw new ArgumentException(SR.Arg_MustBeType, "attributeType");
			}
			return CustomAttribute.IsDefined(this, runtimeType);
		}

		// Token: 0x06004A83 RID: 19075 RVA: 0x00187509 File Offset: 0x00186709
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return CustomAttributeData.GetCustomAttributesInternal(this);
		}

		// Token: 0x06004A84 RID: 19076 RVA: 0x00187511 File Offset: 0x00186711
		internal static RuntimeAssembly InternalLoad(string assemblyName, ref StackCrawlMark stackMark, AssemblyLoadContext assemblyLoadContext = null)
		{
			return RuntimeAssembly.InternalLoad(new AssemblyName(assemblyName), ref stackMark, assemblyLoadContext);
		}

		// Token: 0x06004A85 RID: 19077 RVA: 0x00187520 File Offset: 0x00186720
		internal static RuntimeAssembly InternalLoad(AssemblyName assemblyName, ref StackCrawlMark stackMark, AssemblyLoadContext assemblyLoadContext = null)
		{
			return RuntimeAssembly.InternalLoad(assemblyName, null, ref stackMark, true, assemblyLoadContext);
		}

		// Token: 0x06004A86 RID: 19078 RVA: 0x0018752C File Offset: 0x0018672C
		internal static RuntimeAssembly InternalLoad(AssemblyName assemblyName, RuntimeAssembly requestingAssembly, ref StackCrawlMark stackMark, bool throwOnFileNotFound, AssemblyLoadContext assemblyLoadContext = null)
		{
			RuntimeAssembly result = null;
			RuntimeAssembly.InternalLoad(ObjectHandleOnStack.Create<AssemblyName>(ref assemblyName), ObjectHandleOnStack.Create<RuntimeAssembly>(ref requestingAssembly), new StackCrawlMarkHandle(ref stackMark), throwOnFileNotFound, ObjectHandleOnStack.Create<AssemblyLoadContext>(ref assemblyLoadContext), ObjectHandleOnStack.Create<RuntimeAssembly>(ref result));
			return result;
		}

		// Token: 0x06004A87 RID: 19079
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void InternalLoad(ObjectHandleOnStack assemblyName, ObjectHandleOnStack requestingAssembly, StackCrawlMarkHandle stackMark, bool throwOnFileNotFound, ObjectHandleOnStack assemblyLoadContext, ObjectHandleOnStack retAssembly);

		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x06004A88 RID: 19080 RVA: 0x000AC09B File Offset: 0x000AB29B
		public override bool ReflectionOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06004A89 RID: 19081
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetModule(QCallAssembly assembly, string name, ObjectHandleOnStack retModule);

		// Token: 0x06004A8A RID: 19082 RVA: 0x00187564 File Offset: 0x00186764
		public override Module GetModule(string name)
		{
			Module result = null;
			RuntimeAssembly runtimeAssembly = this;
			RuntimeAssembly.GetModule(new QCallAssembly(ref runtimeAssembly), name, ObjectHandleOnStack.Create<Module>(ref result));
			return result;
		}

		// Token: 0x06004A8B RID: 19083 RVA: 0x0018758C File Offset: 0x0018678C
		public override FileStream GetFile(string name)
		{
			if (this.Location.Length == 0)
			{
				throw new FileNotFoundException(SR.IO_NoFileTableInInMemoryAssemblies);
			}
			RuntimeModule runtimeModule = (RuntimeModule)this.GetModule(name);
			if (runtimeModule == null)
			{
				return null;
			}
			return new FileStream(runtimeModule.GetFullyQualifiedName(), FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
		}

		// Token: 0x06004A8C RID: 19084 RVA: 0x001875E0 File Offset: 0x001867E0
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			if (this.Location.Length == 0)
			{
				throw new FileNotFoundException(SR.IO_NoFileTableInInMemoryAssemblies);
			}
			Module[] modules = this.GetModules(getResourceModules);
			FileStream[] array = new FileStream[modules.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new FileStream(((RuntimeModule)modules[i]).GetFullyQualifiedName(), FileMode.Open, FileAccess.Read, FileShare.Read, 4096, false);
			}
			return array;
		}

		// Token: 0x06004A8D RID: 19085
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern string[] GetManifestResourceNames(RuntimeAssembly assembly);

		// Token: 0x06004A8E RID: 19086 RVA: 0x00187643 File Offset: 0x00186843
		public override string[] GetManifestResourceNames()
		{
			return RuntimeAssembly.GetManifestResourceNames(this.GetNativeHandle());
		}

		// Token: 0x06004A8F RID: 19087
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern AssemblyName[] GetReferencedAssemblies(RuntimeAssembly assembly);

		// Token: 0x06004A90 RID: 19088 RVA: 0x00187650 File Offset: 0x00186850
		[RequiresUnreferencedCode("Assembly references might be removed")]
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return RuntimeAssembly.GetReferencedAssemblies(this.GetNativeHandle());
		}

		// Token: 0x06004A91 RID: 19089
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern int GetManifestResourceInfo(QCallAssembly assembly, string resourceName, ObjectHandleOnStack assemblyRef, StringHandleOnStack retFileName);

		// Token: 0x06004A92 RID: 19090 RVA: 0x00187660 File Offset: 0x00186860
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			RuntimeAssembly containingAssembly = null;
			string containingFileName = null;
			RuntimeAssembly runtimeAssembly = this;
			int manifestResourceInfo = RuntimeAssembly.GetManifestResourceInfo(new QCallAssembly(ref runtimeAssembly), resourceName, ObjectHandleOnStack.Create<RuntimeAssembly>(ref containingAssembly), new StringHandleOnStack(ref containingFileName));
			if (manifestResourceInfo == -1)
			{
				return null;
			}
			return new ManifestResourceInfo(containingAssembly, containingFileName, (ResourceLocation)manifestResourceInfo);
		}

		// Token: 0x06004A93 RID: 19091
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetLocation(QCallAssembly assembly, StringHandleOnStack retString);

		// Token: 0x17000B77 RID: 2935
		// (get) Token: 0x06004A94 RID: 19092 RVA: 0x001876A0 File Offset: 0x001868A0
		public override string Location
		{
			get
			{
				string result = null;
				RuntimeAssembly runtimeAssembly = this;
				RuntimeAssembly.GetLocation(new QCallAssembly(ref runtimeAssembly), new StringHandleOnStack(ref result));
				return result;
			}
		}

		// Token: 0x06004A95 RID: 19093
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetImageRuntimeVersion(QCallAssembly assembly, StringHandleOnStack retString);

		// Token: 0x17000B78 RID: 2936
		// (get) Token: 0x06004A96 RID: 19094 RVA: 0x001876C8 File Offset: 0x001868C8
		public override string ImageRuntimeVersion
		{
			get
			{
				string result = null;
				RuntimeAssembly runtimeAssembly = this;
				RuntimeAssembly.GetImageRuntimeVersion(new QCallAssembly(ref runtimeAssembly), new StringHandleOnStack(ref result));
				return result;
			}
		}

		// Token: 0x17000B79 RID: 2937
		// (get) Token: 0x06004A97 RID: 19095 RVA: 0x000AC09B File Offset: 0x000AB29B
		[Obsolete("The Global Assembly Cache is not supported.", DiagnosticId = "SYSLIB0005", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public override bool GlobalAssemblyCache
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000B7A RID: 2938
		// (get) Token: 0x06004A98 RID: 19096 RVA: 0x001876ED File Offset: 0x001868ED
		public override long HostContext
		{
			get
			{
				return 0L;
			}
		}

		// Token: 0x06004A99 RID: 19097
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetVersion(QCallAssembly assembly, out int majVer, out int minVer, out int buildNum, out int revNum);

		// Token: 0x06004A9A RID: 19098 RVA: 0x001876F4 File Offset: 0x001868F4
		internal Version GetVersion()
		{
			RuntimeAssembly runtimeAssembly = this;
			int major;
			int minor;
			int build;
			int revision;
			RuntimeAssembly.GetVersion(new QCallAssembly(ref runtimeAssembly), out major, out minor, out build, out revision);
			return new Version(major, minor, build, revision);
		}

		// Token: 0x06004A9B RID: 19099
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetLocale(QCallAssembly assembly, StringHandleOnStack retString);

		// Token: 0x06004A9C RID: 19100 RVA: 0x00187724 File Offset: 0x00186924
		internal CultureInfo GetLocale()
		{
			string text = null;
			RuntimeAssembly runtimeAssembly = this;
			RuntimeAssembly.GetLocale(new QCallAssembly(ref runtimeAssembly), new StringHandleOnStack(ref text));
			if (text == null)
			{
				return CultureInfo.InvariantCulture;
			}
			return CultureInfo.GetCultureInfo(text);
		}

		// Token: 0x06004A9D RID: 19101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool FCallIsDynamic(RuntimeAssembly assembly);

		// Token: 0x17000B7B RID: 2939
		// (get) Token: 0x06004A9E RID: 19102 RVA: 0x00187757 File Offset: 0x00186957
		public override bool IsDynamic
		{
			get
			{
				return RuntimeAssembly.FCallIsDynamic(this.GetNativeHandle());
			}
		}

		// Token: 0x06004A9F RID: 19103
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetSimpleName(QCallAssembly assembly, StringHandleOnStack retSimpleName);

		// Token: 0x06004AA0 RID: 19104 RVA: 0x00187764 File Offset: 0x00186964
		internal string GetSimpleName()
		{
			RuntimeAssembly runtimeAssembly = this;
			string result = null;
			RuntimeAssembly.GetSimpleName(new QCallAssembly(ref runtimeAssembly), new StringHandleOnStack(ref result));
			return result;
		}

		// Token: 0x06004AA1 RID: 19105
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern AssemblyHashAlgorithm GetHashAlgorithm(QCallAssembly assembly);

		// Token: 0x06004AA2 RID: 19106 RVA: 0x0018778C File Offset: 0x0018698C
		private AssemblyHashAlgorithm GetHashAlgorithm()
		{
			RuntimeAssembly runtimeAssembly = this;
			return RuntimeAssembly.GetHashAlgorithm(new QCallAssembly(ref runtimeAssembly));
		}

		// Token: 0x06004AA3 RID: 19107
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern AssemblyNameFlags GetFlags(QCallAssembly assembly);

		// Token: 0x06004AA4 RID: 19108 RVA: 0x001877A8 File Offset: 0x001869A8
		private AssemblyNameFlags GetFlags()
		{
			RuntimeAssembly runtimeAssembly = this;
			return RuntimeAssembly.GetFlags(new QCallAssembly(ref runtimeAssembly));
		}

		// Token: 0x06004AA5 RID: 19109
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetPublicKey(QCallAssembly assembly, ObjectHandleOnStack retPublicKey);

		// Token: 0x06004AA6 RID: 19110 RVA: 0x001877C4 File Offset: 0x001869C4
		internal byte[] GetPublicKey()
		{
			byte[] result = null;
			RuntimeAssembly runtimeAssembly = this;
			RuntimeAssembly.GetPublicKey(new QCallAssembly(ref runtimeAssembly), ObjectHandleOnStack.Create<byte[]>(ref result));
			return result;
		}

		// Token: 0x06004AA7 RID: 19111 RVA: 0x001877E9 File Offset: 0x001869E9
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			return this.GetSatelliteAssembly(culture, null);
		}

		// Token: 0x06004AA8 RID: 19112 RVA: 0x001877F3 File Offset: 0x001869F3
		public override Assembly GetSatelliteAssembly(CultureInfo culture, Version version)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			return this.InternalGetSatelliteAssembly(culture, version, true);
		}

		// Token: 0x06004AA9 RID: 19113 RVA: 0x0018780C File Offset: 0x00186A0C
		internal Assembly InternalGetSatelliteAssembly(CultureInfo culture, Version version, bool throwOnFileNotFound)
		{
			AssemblyName assemblyName = new AssemblyName();
			assemblyName.SetPublicKey(this.GetPublicKey());
			assemblyName.Flags = (this.GetFlags() | AssemblyNameFlags.PublicKey);
			assemblyName.Version = (version ?? this.GetVersion());
			assemblyName.CultureInfo = culture;
			assemblyName.Name = this.GetSimpleName() + ".resources";
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMe;
			RuntimeAssembly runtimeAssembly = RuntimeAssembly.InternalLoad(assemblyName, this, ref stackCrawlMark, throwOnFileNotFound, null);
			if (runtimeAssembly == this)
			{
				runtimeAssembly = null;
			}
			if (runtimeAssembly == null && throwOnFileNotFound)
			{
				throw new FileNotFoundException(SR.Format(culture, SR.IO_FileNotFound_FileName, assemblyName.Name));
			}
			return runtimeAssembly;
		}

		// Token: 0x06004AAA RID: 19114
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetModules(QCallAssembly assembly, bool loadIfNotFound, bool getResourceModules, ObjectHandleOnStack retModuleHandles);

		// Token: 0x06004AAB RID: 19115 RVA: 0x001878A4 File Offset: 0x00186AA4
		private RuntimeModule[] GetModulesInternal(bool loadIfNotFound, bool getResourceModules)
		{
			RuntimeModule[] result = null;
			RuntimeAssembly runtimeAssembly = this;
			RuntimeAssembly.GetModules(new QCallAssembly(ref runtimeAssembly), loadIfNotFound, getResourceModules, ObjectHandleOnStack.Create<RuntimeModule[]>(ref result));
			return result;
		}

		// Token: 0x06004AAC RID: 19116 RVA: 0x001878CC File Offset: 0x00186ACC
		public override Module[] GetModules(bool getResourceModules)
		{
			return this.GetModulesInternal(true, getResourceModules);
		}

		// Token: 0x06004AAD RID: 19117 RVA: 0x001878E4 File Offset: 0x00186AE4
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.GetModulesInternal(false, getResourceModules);
		}

		// Token: 0x06004AAE RID: 19118
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern RuntimeModule GetManifestModule(RuntimeAssembly assembly);

		// Token: 0x06004AAF RID: 19119
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern int GetToken(RuntimeAssembly assembly);

		// Token: 0x06004AB0 RID: 19120 RVA: 0x001878FC File Offset: 0x00186AFC
		[RequiresUnreferencedCode("Types might be removed")]
		public sealed override Type[] GetForwardedTypes()
		{
			List<Type> list = new List<Type>();
			List<Exception> list2 = new List<Exception>();
			MetadataEnumResult metadataEnumResult;
			RuntimeAssembly.GetManifestModule(this.GetNativeHandle()).MetadataImport.Enum(MetadataTokenType.ExportedType, 0, out metadataEnumResult);
			RuntimeAssembly runtimeAssembly = this;
			QCallAssembly assembly = new QCallAssembly(ref runtimeAssembly);
			int i = 0;
			while (i < metadataEnumResult.Length)
			{
				MetadataToken mdtExternalType = metadataEnumResult[i];
				Type type = null;
				Exception item = null;
				ObjectHandleOnStack type2 = ObjectHandleOnStack.Create<Type>(ref type);
				try
				{
					RuntimeAssembly.GetForwardedType(assembly, mdtExternalType, type2);
					if (type == null)
					{
						goto IL_A5;
					}
				}
				catch (Exception ex)
				{
					type = null;
					item = ex;
				}
				goto IL_80;
				IL_A5:
				i++;
				continue;
				IL_80:
				if (type != null)
				{
					list.Add(type);
					RuntimeAssembly.AddPublicNestedTypes(type, list, list2);
					goto IL_A5;
				}
				list2.Add(item);
				goto IL_A5;
			}
			if (list2.Count != 0)
			{
				int count = list.Count;
				int count2 = list2.Count;
				list.AddRange(new Type[count2]);
				list2.InsertRange(0, new Exception[count]);
				throw new ReflectionTypeLoadException(list.ToArray(), list2.ToArray());
			}
			return list.ToArray();
		}

		// Token: 0x06004AB1 RID: 19121 RVA: 0x00187A1C File Offset: 0x00186C1C
		private static void AddPublicNestedTypes(Type type, List<Type> types, List<Exception> exceptions)
		{
			Type[] nestedTypes;
			try
			{
				nestedTypes = type.GetNestedTypes(BindingFlags.Public);
			}
			catch (Exception item)
			{
				exceptions.Add(item);
				return;
			}
			foreach (Type type2 in nestedTypes)
			{
				types.Add(type2);
				RuntimeAssembly.AddPublicNestedTypes(type2, types, exceptions);
			}
		}

		// Token: 0x06004AB2 RID: 19122
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetForwardedType(QCallAssembly assembly, MetadataToken mdtExternalType, ObjectHandleOnStack type);

		// Token: 0x04001276 RID: 4726
		private string m_fullname;

		// Token: 0x04001277 RID: 4727
		private object m_syncRoot;

		// Token: 0x04001278 RID: 4728
		private IntPtr m_assembly;

		// Token: 0x020005A9 RID: 1449
		private sealed class ManifestResourceStream : UnmanagedMemoryStream
		{
			// Token: 0x06004AB3 RID: 19123 RVA: 0x00187A74 File Offset: 0x00186C74
			internal unsafe ManifestResourceStream(RuntimeAssembly manifestAssembly, byte* pointer, long length, long capacity, FileAccess access) : base(pointer, length, capacity, access)
			{
				this._manifestAssembly = manifestAssembly;
			}

			// Token: 0x04001279 RID: 4729
			private RuntimeAssembly _manifestAssembly;
		}
	}
}
