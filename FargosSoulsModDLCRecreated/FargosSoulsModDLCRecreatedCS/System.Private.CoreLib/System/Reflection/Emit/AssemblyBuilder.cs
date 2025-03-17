using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Emit
{
	// Token: 0x02000613 RID: 1555
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class AssemblyBuilder : Assembly
	{
		// Token: 0x06004E85 RID: 20101
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern RuntimeModule GetInMemoryAssemblyModule(RuntimeAssembly assembly);

		// Token: 0x06004E86 RID: 20102 RVA: 0x0018D4E0 File Offset: 0x0018C6E0
		internal ModuleBuilder GetModuleBuilder(InternalModuleBuilder module)
		{
			object syncRoot = this.SyncRoot;
			ModuleBuilder manifestModuleBuilder;
			lock (syncRoot)
			{
				if (!(this._manifestModuleBuilder.InternalModule == module))
				{
					throw new ArgumentException(null, "module");
				}
				manifestModuleBuilder = this._manifestModuleBuilder;
			}
			return manifestModuleBuilder;
		}

		// Token: 0x17000CCE RID: 3278
		// (get) Token: 0x06004E87 RID: 20103 RVA: 0x0018D544 File Offset: 0x0018C744
		internal object SyncRoot
		{
			get
			{
				return this.InternalAssembly.SyncRoot;
			}
		}

		// Token: 0x17000CCF RID: 3279
		// (get) Token: 0x06004E88 RID: 20104 RVA: 0x0018D551 File Offset: 0x0018C751
		internal InternalAssemblyBuilder InternalAssembly
		{
			get
			{
				return this._internalAssemblyBuilder;
			}
		}

		// Token: 0x06004E89 RID: 20105 RVA: 0x0018D559 File Offset: 0x0018C759
		internal RuntimeAssembly GetNativeHandle()
		{
			return this.InternalAssembly.GetNativeHandle();
		}

		// Token: 0x06004E8A RID: 20106 RVA: 0x0018D568 File Offset: 0x0018C768
		internal AssemblyBuilder(AssemblyName name, AssemblyBuilderAccess access, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> unsafeAssemblyAttributes)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (access != AssemblyBuilderAccess.Run && access != AssemblyBuilderAccess.RunAndCollect)
			{
				throw new ArgumentException(SR.Format(SR.Arg_EnumIllegalVal, (int)access), "access");
			}
			name = (AssemblyName)name.Clone();
			List<CustomAttributeBuilder> list = null;
			if (unsafeAssemblyAttributes != null)
			{
				list = new List<CustomAttributeBuilder>(unsafeAssemblyAttributes);
			}
			Assembly assembly = null;
			AssemblyBuilder.CreateDynamicAssembly(ObjectHandleOnStack.Create<AssemblyName>(ref name), new StackCrawlMarkHandle(ref stackMark), (int)access, ObjectHandleOnStack.Create<Assembly>(ref assembly));
			this._internalAssemblyBuilder = (InternalAssemblyBuilder)assembly;
			this._assemblyData = new AssemblyBuilderData(this._internalAssemblyBuilder, access);
			this.InitManifestModule();
			if (list != null)
			{
				foreach (CustomAttributeBuilder customAttribute in list)
				{
					this.SetCustomAttribute(customAttribute);
				}
			}
		}

		// Token: 0x06004E8B RID: 20107 RVA: 0x0018D64C File Offset: 0x0018C84C
		[MemberNotNull("_manifestModuleBuilder")]
		private void InitManifestModule()
		{
			InternalModuleBuilder internalModuleBuilder = (InternalModuleBuilder)AssemblyBuilder.GetInMemoryAssemblyModule(this.GetNativeHandle());
			this._manifestModuleBuilder = new ModuleBuilder(this, internalModuleBuilder);
			this._manifestModuleBuilder.Init("RefEmit_InMemoryManifestModule");
			this._isManifestModuleUsedAsDefinedModule = false;
		}

		// Token: 0x06004E8C RID: 20108 RVA: 0x0018D690 File Offset: 0x0018C890
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, ref stackCrawlMark, null);
		}

		// Token: 0x06004E8D RID: 20109 RVA: 0x0018D6AC File Offset: 0x0018C8AC
		public static AssemblyBuilder DefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, [Nullable(new byte[]
		{
			2,
			1
		})] IEnumerable<CustomAttributeBuilder> assemblyAttributes)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return AssemblyBuilder.InternalDefineDynamicAssembly(name, access, ref stackCrawlMark, assemblyAttributes);
		}

		// Token: 0x06004E8E RID: 20110
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void CreateDynamicAssembly(ObjectHandleOnStack name, StackCrawlMarkHandle stackMark, int access, ObjectHandleOnStack retAssembly);

		// Token: 0x06004E8F RID: 20111 RVA: 0x0018D6C8 File Offset: 0x0018C8C8
		internal static AssemblyBuilder InternalDefineDynamicAssembly(AssemblyName name, AssemblyBuilderAccess access, ref StackCrawlMark stackMark, IEnumerable<CustomAttributeBuilder> unsafeAssemblyAttributes)
		{
			object obj = AssemblyBuilder.s_assemblyBuilderLock;
			AssemblyBuilder result;
			lock (obj)
			{
				result = new AssemblyBuilder(name, access, ref stackMark, unsafeAssemblyAttributes);
			}
			return result;
		}

		// Token: 0x06004E90 RID: 20112 RVA: 0x0018D70C File Offset: 0x0018C90C
		public ModuleBuilder DefineDynamicModule(string name)
		{
			return this.DefineDynamicModuleInternal(name, false);
		}

		// Token: 0x06004E91 RID: 20113 RVA: 0x0018D716 File Offset: 0x0018C916
		public ModuleBuilder DefineDynamicModule(string name, bool emitSymbolInfo)
		{
			return this.DefineDynamicModuleInternal(name, emitSymbolInfo);
		}

		// Token: 0x06004E92 RID: 20114 RVA: 0x0018D720 File Offset: 0x0018C920
		private ModuleBuilder DefineDynamicModuleInternal(string name, bool emitSymbolInfo)
		{
			object syncRoot = this.SyncRoot;
			ModuleBuilder result;
			lock (syncRoot)
			{
				result = this.DefineDynamicModuleInternalNoLock(name, emitSymbolInfo);
			}
			return result;
		}

		// Token: 0x06004E93 RID: 20115 RVA: 0x0018D764 File Offset: 0x0018C964
		private ModuleBuilder DefineDynamicModuleInternalNoLock(string name, bool emitSymbolInfo)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "name");
			}
			if (name[0] == '\0')
			{
				throw new ArgumentException(SR.Argument_InvalidName, "name");
			}
			if (this._isManifestModuleUsedAsDefinedModule)
			{
				throw new InvalidOperationException(SR.InvalidOperation_NoMultiModuleAssembly);
			}
			ModuleBuilder manifestModuleBuilder = this._manifestModuleBuilder;
			ISymbolWriter symbolWriter = null;
			if (emitSymbolInfo)
			{
				symbolWriter = SymWrapperCore.SymWriter.CreateSymWriter();
				IntPtr ppUnderlyingWriter = ModuleBuilder.nCreateISymWriterForDynamicModule(manifestModuleBuilder.InternalModule, "Unused");
				((SymWrapperCore.SymWriter)symbolWriter).InternalSetUnderlyingWriter(ppUnderlyingWriter);
			}
			manifestModuleBuilder.SetSymWriter(symbolWriter);
			this._assemblyData._moduleBuilderList.Add(manifestModuleBuilder);
			if (manifestModuleBuilder == this._manifestModuleBuilder)
			{
				this._isManifestModuleUsedAsDefinedModule = true;
			}
			return manifestModuleBuilder;
		}

		// Token: 0x06004E94 RID: 20116 RVA: 0x0018D820 File Offset: 0x0018CA20
		internal static void CheckContext(params Type[][] typess)
		{
			if (typess == null)
			{
				return;
			}
			foreach (Type[] array in typess)
			{
				if (array != null)
				{
					AssemblyBuilder.CheckContext(array);
				}
			}
		}

		// Token: 0x06004E95 RID: 20117 RVA: 0x0018D850 File Offset: 0x0018CA50
		internal static void CheckContext(params Type[] types)
		{
			if (types == null)
			{
				return;
			}
			foreach (Type type in types)
			{
				if (!(type == null))
				{
					if (type.Module == null || type.Module.Assembly == null)
					{
						throw new ArgumentException(SR.Argument_TypeNotValid);
					}
					type.Module.Assembly == typeof(object).Module.Assembly;
				}
			}
		}

		// Token: 0x06004E96 RID: 20118 RVA: 0x0018D8CF File Offset: 0x0018CACF
		[NullableContext(2)]
		public override bool Equals(object obj)
		{
			return this.InternalAssembly.Equals(obj);
		}

		// Token: 0x06004E97 RID: 20119 RVA: 0x0018D8DD File Offset: 0x0018CADD
		public override int GetHashCode()
		{
			return this.InternalAssembly.GetHashCode();
		}

		// Token: 0x06004E98 RID: 20120 RVA: 0x0018D8EA File Offset: 0x0018CAEA
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.InternalAssembly.GetCustomAttributes(inherit);
		}

		// Token: 0x06004E99 RID: 20121 RVA: 0x0018D8F8 File Offset: 0x0018CAF8
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.InternalAssembly.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06004E9A RID: 20122 RVA: 0x0018D907 File Offset: 0x0018CB07
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.InternalAssembly.IsDefined(attributeType, inherit);
		}

		// Token: 0x06004E9B RID: 20123 RVA: 0x0018D916 File Offset: 0x0018CB16
		public override IList<CustomAttributeData> GetCustomAttributesData()
		{
			return this.InternalAssembly.GetCustomAttributesData();
		}

		// Token: 0x06004E9C RID: 20124 RVA: 0x0018D923 File Offset: 0x0018CB23
		public override string[] GetManifestResourceNames()
		{
			return this.InternalAssembly.GetManifestResourceNames();
		}

		// Token: 0x06004E9D RID: 20125 RVA: 0x0018D930 File Offset: 0x0018CB30
		public override FileStream GetFile(string name)
		{
			return this.InternalAssembly.GetFile(name);
		}

		// Token: 0x06004E9E RID: 20126 RVA: 0x0018D93E File Offset: 0x0018CB3E
		public override FileStream[] GetFiles(bool getResourceModules)
		{
			return this.InternalAssembly.GetFiles(getResourceModules);
		}

		// Token: 0x06004E9F RID: 20127 RVA: 0x0018D94C File Offset: 0x0018CB4C
		[return: Nullable(2)]
		public override Stream GetManifestResourceStream(Type type, string name)
		{
			return this.InternalAssembly.GetManifestResourceStream(type, name);
		}

		// Token: 0x06004EA0 RID: 20128 RVA: 0x0018D95B File Offset: 0x0018CB5B
		[return: Nullable(2)]
		public override Stream GetManifestResourceStream(string name)
		{
			return this.InternalAssembly.GetManifestResourceStream(name);
		}

		// Token: 0x06004EA1 RID: 20129 RVA: 0x0018D969 File Offset: 0x0018CB69
		[return: Nullable(2)]
		public override ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			return this.InternalAssembly.GetManifestResourceInfo(resourceName);
		}

		// Token: 0x17000CD0 RID: 3280
		// (get) Token: 0x06004EA2 RID: 20130 RVA: 0x0018D977 File Offset: 0x0018CB77
		public override string Location
		{
			get
			{
				return this.InternalAssembly.Location;
			}
		}

		// Token: 0x17000CD1 RID: 3281
		// (get) Token: 0x06004EA3 RID: 20131 RVA: 0x0018D984 File Offset: 0x0018CB84
		public override string ImageRuntimeVersion
		{
			get
			{
				return this.InternalAssembly.ImageRuntimeVersion;
			}
		}

		// Token: 0x17000CD2 RID: 3282
		// (get) Token: 0x06004EA4 RID: 20132 RVA: 0x0018D991 File Offset: 0x0018CB91
		[Nullable(2)]
		public override string CodeBase
		{
			[NullableContext(2)]
			get
			{
				return this.InternalAssembly.CodeBase;
			}
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x06004EA5 RID: 20133 RVA: 0x0018D99E File Offset: 0x0018CB9E
		[Nullable(2)]
		public override MethodInfo EntryPoint
		{
			[NullableContext(2)]
			get
			{
				return this._assemblyData._entryPointMethod;
			}
		}

		// Token: 0x06004EA6 RID: 20134 RVA: 0x0018D9AB File Offset: 0x0018CBAB
		[RequiresUnreferencedCode("Types might be removed")]
		public override Type[] GetExportedTypes()
		{
			return this.InternalAssembly.GetExportedTypes();
		}

		// Token: 0x06004EA7 RID: 20135 RVA: 0x0018D9B8 File Offset: 0x0018CBB8
		public override AssemblyName GetName(bool copiedName)
		{
			return this.InternalAssembly.GetName(copiedName);
		}

		// Token: 0x17000CD4 RID: 3284
		// (get) Token: 0x06004EA8 RID: 20136 RVA: 0x0018D9C6 File Offset: 0x0018CBC6
		[Nullable(2)]
		public override string FullName
		{
			[NullableContext(2)]
			get
			{
				return this.InternalAssembly.FullName;
			}
		}

		// Token: 0x06004EA9 RID: 20137 RVA: 0x0018D9D3 File Offset: 0x0018CBD3
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public override Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			return this.InternalAssembly.GetType(name, throwOnError, ignoreCase);
		}

		// Token: 0x17000CD5 RID: 3285
		// (get) Token: 0x06004EAA RID: 20138 RVA: 0x0018D9E3 File Offset: 0x0018CBE3
		public override Module ManifestModule
		{
			get
			{
				return this._manifestModuleBuilder.InternalModule;
			}
		}

		// Token: 0x17000CD6 RID: 3286
		// (get) Token: 0x06004EAB RID: 20139 RVA: 0x0018D9F0 File Offset: 0x0018CBF0
		public override bool ReflectionOnly
		{
			get
			{
				return this.InternalAssembly.ReflectionOnly;
			}
		}

		// Token: 0x06004EAC RID: 20140 RVA: 0x0018D9FD File Offset: 0x0018CBFD
		[return: Nullable(2)]
		public override Module GetModule(string name)
		{
			return this.InternalAssembly.GetModule(name);
		}

		// Token: 0x06004EAD RID: 20141 RVA: 0x0018DA0B File Offset: 0x0018CC0B
		[RequiresUnreferencedCode("Assembly references might be removed")]
		public override AssemblyName[] GetReferencedAssemblies()
		{
			return this.InternalAssembly.GetReferencedAssemblies();
		}

		// Token: 0x17000CD7 RID: 3287
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x0018DA18 File Offset: 0x0018CC18
		[Obsolete("The Global Assembly Cache is not supported.", DiagnosticId = "SYSLIB0005", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public override bool GlobalAssemblyCache
		{
			get
			{
				return this.InternalAssembly.GlobalAssemblyCache;
			}
		}

		// Token: 0x17000CD8 RID: 3288
		// (get) Token: 0x06004EAF RID: 20143 RVA: 0x0018DA25 File Offset: 0x0018CC25
		public override long HostContext
		{
			get
			{
				return this.InternalAssembly.HostContext;
			}
		}

		// Token: 0x06004EB0 RID: 20144 RVA: 0x0018DA32 File Offset: 0x0018CC32
		public override Module[] GetModules(bool getResourceModules)
		{
			return this.InternalAssembly.GetModules(getResourceModules);
		}

		// Token: 0x06004EB1 RID: 20145 RVA: 0x0018DA40 File Offset: 0x0018CC40
		public override Module[] GetLoadedModules(bool getResourceModules)
		{
			return this.InternalAssembly.GetLoadedModules(getResourceModules);
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x0018DA4E File Offset: 0x0018CC4E
		public override Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			return this.InternalAssembly.GetSatelliteAssembly(culture, null);
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x0018DA5D File Offset: 0x0018CC5D
		public override Assembly GetSatelliteAssembly(CultureInfo culture, [Nullable(2)] Version version)
		{
			return this.InternalAssembly.GetSatelliteAssembly(culture, version);
		}

		// Token: 0x17000CD9 RID: 3289
		// (get) Token: 0x06004EB4 RID: 20148 RVA: 0x000AC09E File Offset: 0x000AB29E
		public override bool IsDynamic
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000CDA RID: 3290
		// (get) Token: 0x06004EB5 RID: 20149 RVA: 0x0018DA6C File Offset: 0x0018CC6C
		public override bool IsCollectible
		{
			get
			{
				return this.InternalAssembly.IsCollectible;
			}
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x0018DA7C File Offset: 0x0018CC7C
		[return: Nullable(2)]
		public ModuleBuilder GetDynamicModule(string name)
		{
			object syncRoot = this.SyncRoot;
			ModuleBuilder dynamicModuleNoLock;
			lock (syncRoot)
			{
				dynamicModuleNoLock = this.GetDynamicModuleNoLock(name);
			}
			return dynamicModuleNoLock;
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x0018DAC0 File Offset: 0x0018CCC0
		private ModuleBuilder GetDynamicModuleNoLock(string name)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (name.Length == 0)
			{
				throw new ArgumentException(SR.Argument_EmptyName, "name");
			}
			for (int i = 0; i < this._assemblyData._moduleBuilderList.Count; i++)
			{
				ModuleBuilder moduleBuilder = this._assemblyData._moduleBuilderList[i];
				if (moduleBuilder._moduleData._moduleName.Equals(name))
				{
					return moduleBuilder;
				}
			}
			return null;
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x0018DB38 File Offset: 0x0018CD38
		public void SetCustomAttribute(ConstructorInfo con, byte[] binaryAttribute)
		{
			if (con == null)
			{
				throw new ArgumentNullException("con");
			}
			if (binaryAttribute == null)
			{
				throw new ArgumentNullException("binaryAttribute");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetCustomAttributeNoLock(con, binaryAttribute);
			}
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x0018DB9C File Offset: 0x0018CD9C
		private void SetCustomAttributeNoLock(ConstructorInfo con, byte[] binaryAttribute)
		{
			TypeBuilder.DefineCustomAttribute(this._manifestModuleBuilder, 536870913, this._manifestModuleBuilder.GetConstructorToken(con).Token, binaryAttribute, false, typeof(DebuggableAttribute) == con.DeclaringType);
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x0018DBE4 File Offset: 0x0018CDE4
		public void SetCustomAttribute(CustomAttributeBuilder customBuilder)
		{
			if (customBuilder == null)
			{
				throw new ArgumentNullException("customBuilder");
			}
			object syncRoot = this.SyncRoot;
			lock (syncRoot)
			{
				this.SetCustomAttributeNoLock(customBuilder);
			}
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0018DC34 File Offset: 0x0018CE34
		private void SetCustomAttributeNoLock(CustomAttributeBuilder customBuilder)
		{
			customBuilder.CreateCustomAttribute(this._manifestModuleBuilder, 536870913);
		}

		// Token: 0x04001411 RID: 5137
		internal AssemblyBuilderData _assemblyData;

		// Token: 0x04001412 RID: 5138
		private readonly InternalAssemblyBuilder _internalAssemblyBuilder;

		// Token: 0x04001413 RID: 5139
		private ModuleBuilder _manifestModuleBuilder;

		// Token: 0x04001414 RID: 5140
		private bool _isManifestModuleUsedAsDefinedModule;

		// Token: 0x04001415 RID: 5141
		private static readonly object s_assemblyBuilderLock = new object();
	}
}
