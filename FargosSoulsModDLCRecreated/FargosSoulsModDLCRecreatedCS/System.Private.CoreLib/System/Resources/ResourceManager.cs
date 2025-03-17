using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Resources
{
	// Token: 0x02000579 RID: 1401
	[Nullable(0)]
	[NullableContext(1)]
	public class ResourceManager
	{
		// Token: 0x060047F7 RID: 18423 RVA: 0x0017F0EC File Offset: 0x0017E2EC
		protected ResourceManager()
		{
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator mediator = new ResourceManager.ResourceManagerMediator(this);
			this._resourceGroveler = new ManifestBasedResourceGroveler(mediator);
			this.BaseNameField = string.Empty;
		}

		// Token: 0x060047F8 RID: 18424 RVA: 0x0017F128 File Offset: 0x0017E328
		private ResourceManager(string baseName, string resourceDir, [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] Type userResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (resourceDir == null)
			{
				throw new ArgumentNullException("resourceDir");
			}
			this.BaseNameField = baseName;
			this._moduleDir = resourceDir;
			this._userResourceSet = userResourceSet;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator mediator = new ResourceManager.ResourceManagerMediator(this);
			this._resourceGroveler = new FileBasedResourceGroveler(mediator);
		}

		// Token: 0x060047F9 RID: 18425 RVA: 0x0017F198 File Offset: 0x0017E398
		public ResourceManager(string baseName, Assembly assembly)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!assembly.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeAssembly);
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			this.CommonAssemblyInit();
		}

		// Token: 0x060047FA RID: 18426 RVA: 0x0017F1F4 File Offset: 0x0017E3F4
		public ResourceManager(string baseName, Assembly assembly, [Nullable(2)] [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] Type usingResourceSet)
		{
			if (baseName == null)
			{
				throw new ArgumentNullException("baseName");
			}
			if (null == assembly)
			{
				throw new ArgumentNullException("assembly");
			}
			if (!assembly.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeAssembly);
			}
			this.MainAssembly = assembly;
			this.BaseNameField = baseName;
			if (usingResourceSet != null && usingResourceSet != ResourceManager.s_minResourceSet && !usingResourceSet.IsSubclassOf(ResourceManager.s_minResourceSet))
			{
				throw new ArgumentException(SR.Arg_ResMgrNotResSet, "usingResourceSet");
			}
			this._userResourceSet = usingResourceSet;
			this.CommonAssemblyInit();
		}

		// Token: 0x060047FB RID: 18427 RVA: 0x0017F28C File Offset: 0x0017E48C
		public ResourceManager(Type resourceSource)
		{
			if (null == resourceSource)
			{
				throw new ArgumentNullException("resourceSource");
			}
			if (!resourceSource.IsRuntimeImplemented())
			{
				throw new ArgumentException(SR.Argument_MustBeRuntimeType);
			}
			this._locationInfo = resourceSource;
			this.MainAssembly = this._locationInfo.Assembly;
			this.BaseNameField = resourceSource.Name;
			this.CommonAssemblyInit();
		}

		// Token: 0x060047FC RID: 18428 RVA: 0x0017F2F0 File Offset: 0x0017E4F0
		[MemberNotNull("_resourceGroveler")]
		private void CommonAssemblyInit()
		{
			this._useManifest = true;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			ResourceManager.ResourceManagerMediator mediator = new ResourceManager.ResourceManagerMediator(this);
			this._resourceGroveler = new ManifestBasedResourceGroveler(mediator);
			this._neutralResourcesCulture = ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(this.MainAssembly, out this._fallbackLoc);
		}

		// Token: 0x17000AD3 RID: 2771
		// (get) Token: 0x060047FD RID: 18429 RVA: 0x0017F344 File Offset: 0x0017E544
		public virtual string BaseName
		{
			get
			{
				return this.BaseNameField;
			}
		}

		// Token: 0x17000AD4 RID: 2772
		// (get) Token: 0x060047FE RID: 18430 RVA: 0x0017F34C File Offset: 0x0017E54C
		// (set) Token: 0x060047FF RID: 18431 RVA: 0x0017F354 File Offset: 0x0017E554
		public virtual bool IgnoreCase
		{
			get
			{
				return this._ignoreCase;
			}
			set
			{
				this._ignoreCase = value;
			}
		}

		// Token: 0x17000AD5 RID: 2773
		// (get) Token: 0x06004800 RID: 18432 RVA: 0x0017F35D File Offset: 0x0017E55D
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		public virtual Type ResourceSetType
		{
			get
			{
				return this._userResourceSet ?? typeof(RuntimeResourceSet);
			}
		}

		// Token: 0x17000AD6 RID: 2774
		// (get) Token: 0x06004801 RID: 18433 RVA: 0x0017F373 File Offset: 0x0017E573
		// (set) Token: 0x06004802 RID: 18434 RVA: 0x0017F37B File Offset: 0x0017E57B
		protected UltimateResourceFallbackLocation FallbackLocation
		{
			get
			{
				return this._fallbackLoc;
			}
			set
			{
				this._fallbackLoc = value;
			}
		}

		// Token: 0x06004803 RID: 18435 RVA: 0x0017F384 File Offset: 0x0017E584
		public virtual void ReleaseAllResources()
		{
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			this._resourceSets = new Dictionary<string, ResourceSet>();
			this._lastUsedResourceCache = new ResourceManager.CultureNameResourceSetPair();
			Dictionary<string, ResourceSet> obj = resourceSets;
			lock (obj)
			{
				foreach (KeyValuePair<string, ResourceSet> keyValuePair in resourceSets)
				{
					string text;
					ResourceSet resourceSet;
					keyValuePair.Deconstruct(out text, out resourceSet);
					ResourceSet resourceSet2 = resourceSet;
					resourceSet2.Close();
				}
			}
		}

		// Token: 0x06004804 RID: 18436 RVA: 0x0017F424 File Offset: 0x0017E624
		public static ResourceManager CreateFileBasedResourceManager(string baseName, string resourceDir, [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)] [Nullable(2)] Type usingResourceSet)
		{
			return new ResourceManager(baseName, resourceDir, usingResourceSet);
		}

		// Token: 0x06004805 RID: 18437 RVA: 0x0017F430 File Offset: 0x0017E630
		protected virtual string GetResourceFileName(CultureInfo culture)
		{
			if (culture.HasInvariantCultureName)
			{
				return this.BaseNameField + ".resources";
			}
			CultureInfo.VerifyCultureName(culture.Name, true);
			return this.BaseNameField + "." + culture.Name + ".resources";
		}

		// Token: 0x06004806 RID: 18438 RVA: 0x0017F480 File Offset: 0x0017E680
		internal ResourceSet GetFirstResourceSet(CultureInfo culture)
		{
			if (this._neutralResourcesCulture != null && culture.Name == this._neutralResourcesCulture.Name)
			{
				culture = CultureInfo.InvariantCulture;
			}
			if (this._lastUsedResourceCache != null)
			{
				ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
				lock (lastUsedResourceCache)
				{
					if (culture.Name == this._lastUsedResourceCache.lastCultureName)
					{
						return this._lastUsedResourceCache.lastResourceSet;
					}
				}
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> obj = resourceSets;
				lock (obj)
				{
					resourceSets.TryGetValue(culture.Name, out resourceSet);
				}
			}
			if (resourceSet != null)
			{
				if (this._lastUsedResourceCache != null)
				{
					ResourceManager.CultureNameResourceSetPair lastUsedResourceCache2 = this._lastUsedResourceCache;
					lock (lastUsedResourceCache2)
					{
						this._lastUsedResourceCache.lastCultureName = culture.Name;
						this._lastUsedResourceCache.lastResourceSet = resourceSet;
					}
				}
				return resourceSet;
			}
			return null;
		}

		// Token: 0x06004807 RID: 18439 RVA: 0x0017F5B4 File Offset: 0x0017E7B4
		[return: Nullable(2)]
		public virtual ResourceSet GetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			if (culture == null)
			{
				throw new ArgumentNullException("culture");
			}
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			if (resourceSets != null)
			{
				Dictionary<string, ResourceSet> obj = resourceSets;
				lock (obj)
				{
					ResourceSet result;
					if (resourceSets.TryGetValue(culture.Name, out result))
					{
						return result;
					}
				}
			}
			if (this._useManifest && culture.HasInvariantCultureName)
			{
				string resourceFileName = this.GetResourceFileName(culture);
				Stream manifestResourceStream = this.MainAssembly.GetManifestResourceStream(this._locationInfo, resourceFileName);
				if (createIfNotExists && manifestResourceStream != null)
				{
					ResourceSet result = ((ManifestBasedResourceGroveler)this._resourceGroveler).CreateResourceSet(manifestResourceStream, this.MainAssembly);
					ResourceManager.AddResourceSet(resourceSets, culture.Name, ref result);
					return result;
				}
			}
			return this.InternalGetResourceSet(culture, createIfNotExists, tryParents);
		}

		// Token: 0x06004808 RID: 18440 RVA: 0x0017F680 File Offset: 0x0017E880
		[return: Nullable(2)]
		protected virtual ResourceSet InternalGetResourceSet(CultureInfo culture, bool createIfNotExists, bool tryParents)
		{
			Dictionary<string, ResourceSet> resourceSets = this._resourceSets;
			ResourceSet resourceSet = null;
			CultureInfo cultureInfo = null;
			Dictionary<string, ResourceSet> obj = resourceSets;
			lock (obj)
			{
				if (resourceSets.TryGetValue(culture.Name, out resourceSet))
				{
					return resourceSet;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(culture, this._neutralResourcesCulture, tryParents);
			foreach (CultureInfo cultureInfo2 in resourceFallbackManager)
			{
				Dictionary<string, ResourceSet> obj2 = resourceSets;
				lock (obj2)
				{
					if (resourceSets.TryGetValue(cultureInfo2.Name, out resourceSet))
					{
						if (culture != cultureInfo2)
						{
							cultureInfo = cultureInfo2;
						}
						break;
					}
				}
				resourceSet = this._resourceGroveler.GrovelForResourceSet(cultureInfo2, resourceSets, tryParents, createIfNotExists);
				if (resourceSet != null)
				{
					cultureInfo = cultureInfo2;
					break;
				}
			}
			if (resourceSet != null && cultureInfo != null)
			{
				foreach (CultureInfo cultureInfo3 in resourceFallbackManager)
				{
					ResourceManager.AddResourceSet(resourceSets, cultureInfo3.Name, ref resourceSet);
					if (cultureInfo3 == cultureInfo)
					{
						break;
					}
				}
			}
			return resourceSet;
		}

		// Token: 0x06004809 RID: 18441 RVA: 0x0017F7D4 File Offset: 0x0017E9D4
		private static void AddResourceSet(Dictionary<string, ResourceSet> localResourceSets, string cultureName, ref ResourceSet rs)
		{
			lock (localResourceSets)
			{
				ResourceSet resourceSet;
				if (localResourceSets.TryGetValue(cultureName, out resourceSet))
				{
					if (resourceSet != rs)
					{
						if (!localResourceSets.ContainsValue(rs))
						{
							rs.Dispose();
						}
						rs = resourceSet;
					}
				}
				else
				{
					localResourceSets.Add(cultureName, rs);
				}
			}
		}

		// Token: 0x0600480A RID: 18442 RVA: 0x0017F838 File Offset: 0x0017EA38
		[return: Nullable(2)]
		protected static Version GetSatelliteContractVersion(Assembly a)
		{
			if (a == null)
			{
				throw new ArgumentNullException("a", SR.ArgumentNull_Assembly);
			}
			SatelliteContractVersionAttribute customAttribute = a.GetCustomAttribute<SatelliteContractVersionAttribute>();
			string text = (customAttribute != null) ? customAttribute.Version : null;
			if (text == null)
			{
				return null;
			}
			Version result;
			if (!Version.TryParse(text, out result))
			{
				throw new ArgumentException(SR.Format(SR.Arg_InvalidSatelliteContract_Asm_Ver, a, text));
			}
			return result;
		}

		// Token: 0x0600480B RID: 18443 RVA: 0x0017F894 File Offset: 0x0017EA94
		protected static CultureInfo GetNeutralResourcesLanguage(Assembly a)
		{
			UltimateResourceFallbackLocation ultimateResourceFallbackLocation;
			return ManifestBasedResourceGroveler.GetNeutralResourcesLanguage(a, out ultimateResourceFallbackLocation);
		}

		// Token: 0x0600480C RID: 18444 RVA: 0x0017F8AC File Offset: 0x0017EAAC
		internal static bool IsDefaultType(string asmTypeName, string typeName)
		{
			int num = asmTypeName.IndexOf(',');
			if (((num == -1) ? asmTypeName.Length : num) != typeName.Length)
			{
				return false;
			}
			if (string.Compare(asmTypeName, 0, typeName, 0, typeName.Length, StringComparison.Ordinal) != 0)
			{
				return false;
			}
			if (num == -1)
			{
				return true;
			}
			while (char.IsWhiteSpace(asmTypeName[++num]))
			{
			}
			AssemblyName assemblyName = new AssemblyName(asmTypeName.Substring(num));
			return string.Equals(assemblyName.Name, "mscorlib", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x0600480D RID: 18445 RVA: 0x0017F923 File Offset: 0x0017EB23
		[return: Nullable(2)]
		public virtual string GetString(string name)
		{
			return this.GetString(name, null);
		}

		// Token: 0x0600480E RID: 18446 RVA: 0x0017F930 File Offset: 0x0017EB30
		[NullableContext(2)]
		public virtual string GetString([Nullable(1)] string name, CultureInfo culture)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentUICulture;
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				string @string = resourceSet.GetString(name, this._ignoreCase);
				if (@string != null)
				{
					return @string;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(culture, this._neutralResourcesCulture, true);
			foreach (CultureInfo cultureInfo in resourceFallbackManager)
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					string string2 = resourceSet2.GetString(name, this._ignoreCase);
					if (string2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						return string2;
					}
					resourceSet = resourceSet2;
				}
			}
			return null;
		}

		// Token: 0x0600480F RID: 18447 RVA: 0x0017FA4C File Offset: 0x0017EC4C
		[return: Nullable(2)]
		public virtual object GetObject(string name)
		{
			return this.GetObject(name, null, true);
		}

		// Token: 0x06004810 RID: 18448 RVA: 0x0017FA57 File Offset: 0x0017EC57
		[NullableContext(2)]
		public virtual object GetObject([Nullable(1)] string name, CultureInfo culture)
		{
			return this.GetObject(name, culture, true);
		}

		// Token: 0x06004811 RID: 18449 RVA: 0x0017FA64 File Offset: 0x0017EC64
		private object GetObject(string name, CultureInfo culture, bool wrapUnmanagedMemStream)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			if (culture == null)
			{
				culture = CultureInfo.CurrentUICulture;
			}
			ResourceSet resourceSet = this.GetFirstResourceSet(culture);
			if (resourceSet != null)
			{
				object @object = resourceSet.GetObject(name, this._ignoreCase);
				if (@object != null)
				{
					UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
					if (unmanagedMemoryStream != null && wrapUnmanagedMemStream)
					{
						return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream);
					}
					return @object;
				}
			}
			ResourceFallbackManager resourceFallbackManager = new ResourceFallbackManager(culture, this._neutralResourcesCulture, true);
			foreach (CultureInfo cultureInfo in resourceFallbackManager)
			{
				ResourceSet resourceSet2 = this.InternalGetResourceSet(cultureInfo, true, true);
				if (resourceSet2 == null)
				{
					break;
				}
				if (resourceSet2 != resourceSet)
				{
					object object2 = resourceSet2.GetObject(name, this._ignoreCase);
					if (object2 != null)
					{
						if (this._lastUsedResourceCache != null)
						{
							ResourceManager.CultureNameResourceSetPair lastUsedResourceCache = this._lastUsedResourceCache;
							lock (lastUsedResourceCache)
							{
								this._lastUsedResourceCache.lastCultureName = cultureInfo.Name;
								this._lastUsedResourceCache.lastResourceSet = resourceSet2;
							}
						}
						UnmanagedMemoryStream unmanagedMemoryStream2 = object2 as UnmanagedMemoryStream;
						if (unmanagedMemoryStream2 != null && wrapUnmanagedMemStream)
						{
							return new UnmanagedMemoryStreamWrapper(unmanagedMemoryStream2);
						}
						return object2;
					}
					else
					{
						resourceSet = resourceSet2;
					}
				}
			}
			return null;
		}

		// Token: 0x06004812 RID: 18450 RVA: 0x0017FBBC File Offset: 0x0017EDBC
		[return: Nullable(2)]
		public UnmanagedMemoryStream GetStream(string name)
		{
			return this.GetStream(name, null);
		}

		// Token: 0x06004813 RID: 18451 RVA: 0x0017FBC8 File Offset: 0x0017EDC8
		[NullableContext(2)]
		public UnmanagedMemoryStream GetStream([Nullable(1)] string name, CultureInfo culture)
		{
			object @object = this.GetObject(name, culture, false);
			UnmanagedMemoryStream unmanagedMemoryStream = @object as UnmanagedMemoryStream;
			if (unmanagedMemoryStream == null && @object != null)
			{
				throw new InvalidOperationException(SR.Format(SR.InvalidOperation_ResourceNotStream_Name, name));
			}
			return unmanagedMemoryStream;
		}

		// Token: 0x04001167 RID: 4455
		protected string BaseNameField;

		// Token: 0x04001168 RID: 4456
		[Nullable(2)]
		protected Assembly MainAssembly;

		// Token: 0x04001169 RID: 4457
		private Dictionary<string, ResourceSet> _resourceSets;

		// Token: 0x0400116A RID: 4458
		private readonly string _moduleDir;

		// Token: 0x0400116B RID: 4459
		private readonly Type _locationInfo;

		// Token: 0x0400116C RID: 4460
		[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
		private readonly Type _userResourceSet;

		// Token: 0x0400116D RID: 4461
		private CultureInfo _neutralResourcesCulture;

		// Token: 0x0400116E RID: 4462
		private ResourceManager.CultureNameResourceSetPair _lastUsedResourceCache;

		// Token: 0x0400116F RID: 4463
		private bool _ignoreCase;

		// Token: 0x04001170 RID: 4464
		private bool _useManifest;

		// Token: 0x04001171 RID: 4465
		private UltimateResourceFallbackLocation _fallbackLoc;

		// Token: 0x04001172 RID: 4466
		private Version _satelliteContractVersion;

		// Token: 0x04001173 RID: 4467
		private bool _lookedForSatelliteContractVersion;

		// Token: 0x04001174 RID: 4468
		private IResourceGroveler _resourceGroveler;

		// Token: 0x04001175 RID: 4469
		public static readonly int MagicNumber = -1091581234;

		// Token: 0x04001176 RID: 4470
		public static readonly int HeaderVersionNumber = 1;

		// Token: 0x04001177 RID: 4471
		private static readonly Type s_minResourceSet = typeof(ResourceSet);

		// Token: 0x0200057A RID: 1402
		internal class CultureNameResourceSetPair
		{
			// Token: 0x04001178 RID: 4472
			public string lastCultureName;

			// Token: 0x04001179 RID: 4473
			public ResourceSet lastResourceSet;
		}

		// Token: 0x0200057B RID: 1403
		internal class ResourceManagerMediator
		{
			// Token: 0x06004816 RID: 18454 RVA: 0x0017FC1F File Offset: 0x0017EE1F
			internal ResourceManagerMediator(ResourceManager rm)
			{
				if (rm == null)
				{
					throw new ArgumentNullException("rm");
				}
				this._rm = rm;
			}

			// Token: 0x17000AD7 RID: 2775
			// (get) Token: 0x06004817 RID: 18455 RVA: 0x0017FC3C File Offset: 0x0017EE3C
			internal string ModuleDir
			{
				get
				{
					return this._rm._moduleDir;
				}
			}

			// Token: 0x17000AD8 RID: 2776
			// (get) Token: 0x06004818 RID: 18456 RVA: 0x0017FC49 File Offset: 0x0017EE49
			internal Type LocationInfo
			{
				get
				{
					return this._rm._locationInfo;
				}
			}

			// Token: 0x17000AD9 RID: 2777
			// (get) Token: 0x06004819 RID: 18457 RVA: 0x0017FC56 File Offset: 0x0017EE56
			[DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)7)]
			internal Type UserResourceSet
			{
				get
				{
					return this._rm._userResourceSet;
				}
			}

			// Token: 0x17000ADA RID: 2778
			// (get) Token: 0x0600481A RID: 18458 RVA: 0x0017FC63 File Offset: 0x0017EE63
			internal string BaseNameField
			{
				get
				{
					return this._rm.BaseNameField;
				}
			}

			// Token: 0x17000ADB RID: 2779
			// (get) Token: 0x0600481B RID: 18459 RVA: 0x0017FC70 File Offset: 0x0017EE70
			internal CultureInfo NeutralResourcesCulture
			{
				get
				{
					return this._rm._neutralResourcesCulture;
				}
			}

			// Token: 0x0600481C RID: 18460 RVA: 0x0017FC7D File Offset: 0x0017EE7D
			internal string GetResourceFileName(CultureInfo culture)
			{
				return this._rm.GetResourceFileName(culture);
			}

			// Token: 0x17000ADC RID: 2780
			// (get) Token: 0x0600481D RID: 18461 RVA: 0x0017FC8B File Offset: 0x0017EE8B
			// (set) Token: 0x0600481E RID: 18462 RVA: 0x0017FC98 File Offset: 0x0017EE98
			internal bool LookedForSatelliteContractVersion
			{
				get
				{
					return this._rm._lookedForSatelliteContractVersion;
				}
				set
				{
					this._rm._lookedForSatelliteContractVersion = value;
				}
			}

			// Token: 0x17000ADD RID: 2781
			// (get) Token: 0x0600481F RID: 18463 RVA: 0x0017FCA6 File Offset: 0x0017EEA6
			// (set) Token: 0x06004820 RID: 18464 RVA: 0x0017FCB3 File Offset: 0x0017EEB3
			internal Version SatelliteContractVersion
			{
				get
				{
					return this._rm._satelliteContractVersion;
				}
				set
				{
					this._rm._satelliteContractVersion = value;
				}
			}

			// Token: 0x06004821 RID: 18465 RVA: 0x0017FCC1 File Offset: 0x0017EEC1
			internal static Version ObtainSatelliteContractVersion(Assembly a)
			{
				return ResourceManager.GetSatelliteContractVersion(a);
			}

			// Token: 0x17000ADE RID: 2782
			// (get) Token: 0x06004822 RID: 18466 RVA: 0x0017FCC9 File Offset: 0x0017EEC9
			internal UltimateResourceFallbackLocation FallbackLoc
			{
				get
				{
					return this._rm.FallbackLocation;
				}
			}

			// Token: 0x17000ADF RID: 2783
			// (get) Token: 0x06004823 RID: 18467 RVA: 0x0017FCD6 File Offset: 0x0017EED6
			internal Assembly MainAssembly
			{
				get
				{
					return this._rm.MainAssembly;
				}
			}

			// Token: 0x17000AE0 RID: 2784
			// (get) Token: 0x06004824 RID: 18468 RVA: 0x0017FCE3 File Offset: 0x0017EEE3
			internal string BaseName
			{
				get
				{
					return this._rm.BaseName;
				}
			}

			// Token: 0x0400117A RID: 4474
			private readonly ResourceManager _rm;
		}
	}
}
