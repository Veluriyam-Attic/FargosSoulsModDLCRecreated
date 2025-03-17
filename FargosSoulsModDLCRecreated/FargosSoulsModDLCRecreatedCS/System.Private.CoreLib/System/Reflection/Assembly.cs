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
using System.Security;
using System.Threading;

namespace System.Reflection
{
	// Token: 0x02000586 RID: 1414
	[Nullable(0)]
	[NullableContext(1)]
	public abstract class Assembly : ICustomAttributeProvider, ISerializable
	{
		// Token: 0x0600487B RID: 18555 RVA: 0x00181F4C File Offset: 0x0018114C
		public static Assembly Load(string assemblyString)
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyString, ref stackCrawlMark, AssemblyLoadContext.CurrentContextualReflectionContext);
		}

		// Token: 0x0600487C RID: 18556 RVA: 0x00181F68 File Offset: 0x00181168
		[Obsolete("This method has been deprecated. Please use Assembly.Load() instead. https://go.microsoft.com/fwlink/?linkid=14202")]
		[return: Nullable(2)]
		public static Assembly LoadWithPartialName(string partialName)
		{
			if (partialName == null)
			{
				throw new ArgumentNullException("partialName");
			}
			if (partialName.Length == 0 || partialName[0] == '\0')
			{
				throw new ArgumentException(SR.Format_StringZeroLength, "partialName");
			}
			Assembly result;
			try
			{
				StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
				result = RuntimeAssembly.InternalLoad(partialName, ref stackCrawlMark, AssemblyLoadContext.CurrentContextualReflectionContext);
			}
			catch (FileNotFoundException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600487D RID: 18557 RVA: 0x00181FD0 File Offset: 0x001811D0
		public static Assembly Load(AssemblyName assemblyRef)
		{
			if (assemblyRef == null)
			{
				throw new ArgumentNullException("assemblyRef");
			}
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return RuntimeAssembly.InternalLoad(assemblyRef, ref stackCrawlMark, AssemblyLoadContext.CurrentContextualReflectionContext);
		}

		// Token: 0x0600487E RID: 18558
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetExecutingAssemblyNative(StackCrawlMarkHandle stackMark, ObjectHandleOnStack retAssembly);

		// Token: 0x0600487F RID: 18559 RVA: 0x00181FFC File Offset: 0x001811FC
		internal static RuntimeAssembly GetExecutingAssembly(ref StackCrawlMark stackMark)
		{
			RuntimeAssembly result = null;
			Assembly.GetExecutingAssemblyNative(new StackCrawlMarkHandle(ref stackMark), ObjectHandleOnStack.Create<RuntimeAssembly>(ref result));
			return result;
		}

		// Token: 0x06004880 RID: 18560 RVA: 0x00182020 File Offset: 0x00181220
		public static Assembly GetExecutingAssembly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCaller;
			return Assembly.GetExecutingAssembly(ref stackCrawlMark);
		}

		// Token: 0x06004881 RID: 18561 RVA: 0x00182038 File Offset: 0x00181238
		public static Assembly GetCallingAssembly()
		{
			StackCrawlMark stackCrawlMark = StackCrawlMark.LookForMyCallersCaller;
			return Assembly.GetExecutingAssembly(ref stackCrawlMark);
		}

		// Token: 0x06004882 RID: 18562
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern void GetEntryAssemblyNative(ObjectHandleOnStack retAssembly);

		// Token: 0x06004883 RID: 18563 RVA: 0x00182050 File Offset: 0x00181250
		private static Assembly GetEntryAssemblyInternal()
		{
			RuntimeAssembly result = null;
			Assembly.GetEntryAssemblyNative(ObjectHandleOnStack.Create<RuntimeAssembly>(ref result));
			return result;
		}

		// Token: 0x06004884 RID: 18564 RVA: 0x0018206C File Offset: 0x0018126C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal bool IsRuntimeImplemented()
		{
			return this is RuntimeAssembly;
		}

		// Token: 0x06004885 RID: 18565
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern uint GetAssemblyCount();

		// Token: 0x17000AE9 RID: 2793
		// (get) Token: 0x06004887 RID: 18567 RVA: 0x00182078 File Offset: 0x00181278
		public virtual IEnumerable<TypeInfo> DefinedTypes
		{
			[RequiresUnreferencedCode("Types might be removed")]
			get
			{
				Type[] types = this.GetTypes();
				TypeInfo[] array = new TypeInfo[types.Length];
				for (int i = 0; i < types.Length; i++)
				{
					TypeInfo typeInfo = types[i].GetTypeInfo();
					if (typeInfo == null)
					{
						throw new NotSupportedException(SR.Format(SR.NotSupported_NoTypeInfo, types[i].FullName));
					}
					array[i] = typeInfo;
				}
				return array;
			}
		}

		// Token: 0x06004888 RID: 18568 RVA: 0x001820D4 File Offset: 0x001812D4
		[RequiresUnreferencedCode("Types might be removed")]
		public virtual Type[] GetTypes()
		{
			Module[] modules = this.GetModules(false);
			if (modules.Length == 1)
			{
				return modules[0].GetTypes();
			}
			int num = 0;
			Type[][] array = new Type[modules.Length][];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = modules[i].GetTypes();
				num += array[i].Length;
			}
			int num2 = 0;
			Type[] array2 = new Type[num];
			for (int j = 0; j < array.Length; j++)
			{
				int num3 = array[j].Length;
				Array.Copy(array[j], 0, array2, num2, num3);
				num2 += num3;
			}
			return array2;
		}

		// Token: 0x17000AEA RID: 2794
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x00182165 File Offset: 0x00181365
		public virtual IEnumerable<Type> ExportedTypes
		{
			[RequiresUnreferencedCode("Types might be removed")]
			get
			{
				return this.GetExportedTypes();
			}
		}

		// Token: 0x0600488A RID: 18570 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Types might be removed")]
		public virtual Type[] GetExportedTypes()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600488B RID: 18571 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Types might be removed")]
		public virtual Type[] GetForwardedTypes()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000AEB RID: 2795
		// (get) Token: 0x0600488C RID: 18572 RVA: 0x000C2700 File Offset: 0x000C1900
		[Nullable(2)]
		public virtual string CodeBase
		{
			[NullableContext(2)]
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000AEC RID: 2796
		// (get) Token: 0x0600488D RID: 18573 RVA: 0x000C2700 File Offset: 0x000C1900
		[Nullable(2)]
		public virtual MethodInfo EntryPoint
		{
			[NullableContext(2)]
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000AED RID: 2797
		// (get) Token: 0x0600488E RID: 18574 RVA: 0x000C2700 File Offset: 0x000C1900
		[Nullable(2)]
		public virtual string FullName
		{
			[NullableContext(2)]
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000AEE RID: 2798
		// (get) Token: 0x0600488F RID: 18575 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual string ImageRuntimeVersion
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000AEF RID: 2799
		// (get) Token: 0x06004890 RID: 18576 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual bool IsDynamic
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06004891 RID: 18577 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual string Location
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool ReflectionOnly
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06004893 RID: 18579 RVA: 0x000AC09E File Offset: 0x000AB29E
		public virtual bool IsCollectible
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004894 RID: 18580 RVA: 0x000C2700 File Offset: 0x000C1900
		[return: Nullable(2)]
		public virtual ManifestResourceInfo GetManifestResourceInfo(string resourceName)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004895 RID: 18581 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual string[] GetManifestResourceNames()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004896 RID: 18582 RVA: 0x000C2700 File Offset: 0x000C1900
		[return: Nullable(2)]
		public virtual Stream GetManifestResourceStream(string name)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x06004897 RID: 18583 RVA: 0x000C2700 File Offset: 0x000C1900
		[return: Nullable(2)]
		public virtual Stream GetManifestResourceStream(Type type, string name)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x000AC09E File Offset: 0x000AB29E
		public bool IsFullyTrusted
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06004899 RID: 18585 RVA: 0x0018216D File Offset: 0x0018136D
		public virtual AssemblyName GetName()
		{
			return this.GetName(false);
		}

		// Token: 0x0600489A RID: 18586 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual AssemblyName GetName(bool copiedName)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x00182176 File Offset: 0x00181376
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public virtual Type GetType(string name)
		{
			return this.GetType(name, false, false);
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x00182181 File Offset: 0x00181381
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public virtual Type GetType(string name, bool throwOnError)
		{
			return this.GetType(name, throwOnError, false);
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Types might be removed")]
		[return: Nullable(2)]
		public virtual Type GetType(string name, bool throwOnError, bool ignoreCase)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x0600489F RID: 18591 RVA: 0x0018218C File Offset: 0x0018138C
		public virtual IEnumerable<CustomAttributeData> CustomAttributes
		{
			get
			{
				return this.GetCustomAttributesData();
			}
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual IList<CustomAttributeData> GetCustomAttributesData()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060048A3 RID: 18595 RVA: 0x00182194 File Offset: 0x00181394
		public virtual string EscapedCodeBase
		{
			get
			{
				return AssemblyName.EscapeCodeBase(this.CodeBase);
			}
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x001821A1 File Offset: 0x001813A1
		[RequiresUnreferencedCode("Assembly.CreateInstance is not supported with trimming. Use Type.GetType instead.")]
		[return: Nullable(2)]
		public object CreateInstance(string typeName)
		{
			return this.CreateInstance(typeName, false, BindingFlags.Instance | BindingFlags.Public, null, null, null, null);
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x001821B1 File Offset: 0x001813B1
		[RequiresUnreferencedCode("Assembly.CreateInstance is not supported with trimming. Use Type.GetType instead.")]
		[return: Nullable(2)]
		public object CreateInstance(string typeName, bool ignoreCase)
		{
			return this.CreateInstance(typeName, ignoreCase, BindingFlags.Instance | BindingFlags.Public, null, null, null, null);
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x001821C4 File Offset: 0x001813C4
		[NullableContext(2)]
		[RequiresUnreferencedCode("Assembly.CreateInstance is not supported with trimming. Use Type.GetType instead.")]
		public virtual object CreateInstance([Nullable(1)] string typeName, bool ignoreCase, BindingFlags bindingAttr, Binder binder, [Nullable(new byte[]
		{
			2,
			1
		})] object[] args, CultureInfo culture, [Nullable(new byte[]
		{
			2,
			1
		})] object[] activationAttributes)
		{
			Type type = this.GetType(typeName, false, ignoreCase);
			if (type == null)
			{
				return null;
			}
			return Activator.CreateInstance(type, bindingAttr, binder, args, culture, activationAttributes);
		}

		// Token: 0x1400001B RID: 27
		// (add) Token: 0x060048A7 RID: 18599 RVA: 0x000C2700 File Offset: 0x000C1900
		// (remove) Token: 0x060048A8 RID: 18600 RVA: 0x000C2700 File Offset: 0x000C1900
		[Nullable(2)]
		public virtual event ModuleResolveEventHandler ModuleResolve
		{
			[NullableContext(2)]
			add
			{
				throw NotImplemented.ByDesign;
			}
			[NullableContext(2)]
			remove
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x060048A9 RID: 18601 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual Module ManifestModule
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x000C2700 File Offset: 0x000C1900
		[return: Nullable(2)]
		public virtual Module GetModule(string name)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x001821F5 File Offset: 0x001813F5
		public Module[] GetModules()
		{
			return this.GetModules(false);
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual Module[] GetModules(bool getResourceModules)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060048AD RID: 18605 RVA: 0x001821FE File Offset: 0x001813FE
		public virtual IEnumerable<Module> Modules
		{
			get
			{
				return this.GetLoadedModules(true);
			}
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x00182207 File Offset: 0x00181407
		public Module[] GetLoadedModules()
		{
			return this.GetLoadedModules(false);
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual Module[] GetLoadedModules(bool getResourceModules)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Assembly references might be removed")]
		public virtual AssemblyName[] GetReferencedAssemblies()
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual Assembly GetSatelliteAssembly(CultureInfo culture, [Nullable(2)] Version version)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x000C2700 File Offset: 0x000C1900
		[return: Nullable(2)]
		public virtual FileStream GetFile(string name)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x00182210 File Offset: 0x00181410
		public virtual FileStream[] GetFiles()
		{
			return this.GetFiles(false);
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual FileStream[] GetFiles(bool getResourceModules)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x00182219 File Offset: 0x00181419
		public override string ToString()
		{
			return this.FullName ?? base.ToString();
		}

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060048B8 RID: 18616 RVA: 0x000C2700 File Offset: 0x000C1900
		[Obsolete("The Global Assembly Cache is not supported.", DiagnosticId = "SYSLIB0005", UrlFormat = "https://aka.ms/dotnet-warnings/{0}")]
		public virtual bool GlobalAssemblyCache
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060048B9 RID: 18617 RVA: 0x000C2700 File Offset: 0x000C1900
		public virtual long HostContext
		{
			get
			{
				throw NotImplemented.ByDesign;
			}
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x001686B3 File Offset: 0x001678B3
		[NullableContext(2)]
		public override bool Equals(object o)
		{
			return base.Equals(o);
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x001686D0 File Offset: 0x001678D0
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x0018222B File Offset: 0x0018142B
		[NullableContext(2)]
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool operator ==(Assembly left, Assembly right)
		{
			if (right == null)
			{
				return left == null;
			}
			return left == right || (left != null && left.Equals(right));
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x00182249 File Offset: 0x00181449
		[NullableContext(2)]
		public static bool operator !=(Assembly left, Assembly right)
		{
			return !(left == right);
		}

		// Token: 0x060048BE RID: 18622 RVA: 0x00182255 File Offset: 0x00181455
		[NullableContext(2)]
		[return: Nullable(1)]
		public static string CreateQualifiedName(string assemblyName, string typeName)
		{
			return typeName + ", " + assemblyName;
		}

		// Token: 0x060048BF RID: 18623 RVA: 0x00182264 File Offset: 0x00181464
		[return: Nullable(2)]
		public static Assembly GetAssembly(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			Module module = type.Module;
			if (module == null)
			{
				return null;
			}
			return module.Assembly;
		}

		// Token: 0x060048C0 RID: 18624 RVA: 0x0018229D File Offset: 0x0018149D
		[NullableContext(2)]
		public static Assembly GetEntryAssembly()
		{
			if (Assembly.s_forceNullEntryPoint)
			{
				return null;
			}
			return Assembly.GetEntryAssemblyInternal();
		}

		// Token: 0x060048C1 RID: 18625 RVA: 0x001822AD File Offset: 0x001814AD
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public static Assembly Load(byte[] rawAssembly)
		{
			return Assembly.Load(rawAssembly, null);
		}

		// Token: 0x060048C2 RID: 18626 RVA: 0x001822B8 File Offset: 0x001814B8
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public static Assembly Load(byte[] rawAssembly, [Nullable(2)] byte[] rawSymbolStore)
		{
			if (rawAssembly == null)
			{
				throw new ArgumentNullException("rawAssembly");
			}
			if (rawAssembly.Length == 0)
			{
				throw new BadImageFormatException(SR.BadImageFormat_BadILFormat);
			}
			SerializationInfo.ThrowIfDeserializationInProgress("AllowAssembliesFromByteArrays", ref Assembly.s_cachedSerializationSwitch);
			AssemblyLoadContext assemblyLoadContext = new IndividualAssemblyLoadContext("Assembly.Load(byte[], ...)");
			return assemblyLoadContext.InternalLoad(rawAssembly, rawSymbolStore);
		}

		// Token: 0x060048C3 RID: 18627 RVA: 0x00182310 File Offset: 0x00181510
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public static Assembly LoadFile(string path)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (PathInternal.IsPartiallyQualified(path))
			{
				throw new ArgumentException(SR.Format(SR.Argument_AbsolutePathRequired, path), "path");
			}
			string fullPath = Path.GetFullPath(path);
			Dictionary<string, Assembly> obj = Assembly.s_loadfile;
			Assembly assembly;
			lock (obj)
			{
				if (Assembly.s_loadfile.TryGetValue(fullPath, out assembly))
				{
					return assembly;
				}
				AssemblyLoadContext assemblyLoadContext = new IndividualAssemblyLoadContext(string.Format("Assembly.LoadFile({0})", fullPath));
				assembly = assemblyLoadContext.LoadFromAssemblyPath(fullPath);
				Assembly.s_loadfile.Add(fullPath, assembly);
			}
			return assembly;
		}

		// Token: 0x060048C4 RID: 18628 RVA: 0x001823C0 File Offset: 0x001815C0
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		private static Assembly LoadFromResolveHandler(object sender, ResolveEventArgs args)
		{
			Assembly requestingAssembly = args.RequestingAssembly;
			if (requestingAssembly == null)
			{
				return null;
			}
			if (AssemblyLoadContext.Default != AssemblyLoadContext.GetLoadContext(requestingAssembly))
			{
				return null;
			}
			string fullPath = Path.GetFullPath(requestingAssembly.Location);
			if (string.IsNullOrEmpty(fullPath))
			{
				return null;
			}
			List<string> obj = Assembly.s_loadFromAssemblyList;
			lock (obj)
			{
				if (!Assembly.s_loadFromAssemblyList.Contains(fullPath))
				{
					if (AssemblyLoadContext.IsTracingEnabled())
					{
						AssemblyLoadContext.TraceAssemblyLoadFromResolveHandlerInvoked(args.Name, false, fullPath, null);
					}
					return null;
				}
			}
			AssemblyName assemblyName = new AssemblyName(args.Name);
			string text = Path.Combine(Path.GetDirectoryName(fullPath), assemblyName.Name + ".dll");
			if (AssemblyLoadContext.IsTracingEnabled())
			{
				AssemblyLoadContext.TraceAssemblyLoadFromResolveHandlerInvoked(args.Name, true, fullPath, text);
			}
			Assembly result;
			try
			{
				result = Assembly.LoadFrom(text);
			}
			catch (FileNotFoundException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060048C5 RID: 18629 RVA: 0x001824BC File Offset: 0x001816BC
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public static Assembly LoadFrom(string assemblyFile)
		{
			if (assemblyFile == null)
			{
				throw new ArgumentNullException("assemblyFile");
			}
			string fullPath = Path.GetFullPath(assemblyFile);
			if (!Assembly.s_loadFromHandlerSet)
			{
				List<string> obj = Assembly.s_loadFromAssemblyList;
				lock (obj)
				{
					if (!Assembly.s_loadFromHandlerSet)
					{
						AssemblyLoadContext.AssemblyResolve += Assembly.LoadFromResolveHandler;
						Assembly.s_loadFromHandlerSet = true;
					}
				}
			}
			List<string> obj2 = Assembly.s_loadFromAssemblyList;
			lock (obj2)
			{
				if (!Assembly.s_loadFromAssemblyList.Contains(fullPath))
				{
					Assembly.s_loadFromAssemblyList.Add(fullPath);
				}
			}
			return AssemblyLoadContext.Default.LoadFromAssemblyPath(fullPath);
		}

		// Token: 0x060048C6 RID: 18630 RVA: 0x0018257C File Offset: 0x0018177C
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public static Assembly LoadFrom(string assemblyFile, [Nullable(2)] byte[] hashValue, AssemblyHashAlgorithm hashAlgorithm)
		{
			throw new NotSupportedException(SR.NotSupported_AssemblyLoadFromHash);
		}

		// Token: 0x060048C7 RID: 18631 RVA: 0x00182588 File Offset: 0x00181788
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public static Assembly UnsafeLoadFrom(string assemblyFile)
		{
			return Assembly.LoadFrom(assemblyFile);
		}

		// Token: 0x060048C8 RID: 18632 RVA: 0x00182590 File Offset: 0x00181790
		[RequiresUnreferencedCode("Types and members the loaded module depends on might be removed")]
		public Module LoadModule(string moduleName, [Nullable(2)] byte[] rawModule)
		{
			return this.LoadModule(moduleName, rawModule, null);
		}

		// Token: 0x060048C9 RID: 18633 RVA: 0x000C2700 File Offset: 0x000C1900
		[RequiresUnreferencedCode("Types and members the loaded module depends on might be removed")]
		public virtual Module LoadModule(string moduleName, [Nullable(2)] byte[] rawModule, [Nullable(2)] byte[] rawSymbolStore)
		{
			throw NotImplemented.ByDesign;
		}

		// Token: 0x060048CA RID: 18634 RVA: 0x000C2F1C File Offset: 0x000C211C
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public static Assembly ReflectionOnlyLoad(byte[] rawAssembly)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ReflectionOnly);
		}

		// Token: 0x060048CB RID: 18635 RVA: 0x000C2F1C File Offset: 0x000C211C
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public static Assembly ReflectionOnlyLoad(string assemblyString)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ReflectionOnly);
		}

		// Token: 0x060048CC RID: 18636 RVA: 0x000C2F1C File Offset: 0x000C211C
		[RequiresUnreferencedCode("Types and members the loaded assembly depends on might be removed")]
		public static Assembly ReflectionOnlyLoadFrom(string assemblyFile)
		{
			throw new PlatformNotSupportedException(SR.PlatformNotSupported_ReflectionOnly);
		}

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060048CD RID: 18637 RVA: 0x000AC09B File Offset: 0x000AB29B
		public virtual SecurityRuleSet SecurityRuleSet
		{
			get
			{
				return SecurityRuleSet.None;
			}
		}

		// Token: 0x040011B7 RID: 4535
		private static readonly Dictionary<string, Assembly> s_loadfile = new Dictionary<string, Assembly>();

		// Token: 0x040011B8 RID: 4536
		private static readonly List<string> s_loadFromAssemblyList = new List<string>();

		// Token: 0x040011B9 RID: 4537
		private static bool s_loadFromHandlerSet;

		// Token: 0x040011BA RID: 4538
		private static int s_cachedSerializationSwitch;

		// Token: 0x040011BB RID: 4539
		private static bool s_forceNullEntryPoint;
	}
}
