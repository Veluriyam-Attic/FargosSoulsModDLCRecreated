using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;

namespace System.Resources
{
	// Token: 0x0200056F RID: 1391
	internal class ManifestBasedResourceGroveler : IResourceGroveler
	{
		// Token: 0x060047C0 RID: 18368 RVA: 0x0017E2AB File Offset: 0x0017D4AB
		private static Assembly InternalGetSatelliteAssembly(Assembly mainAssembly, CultureInfo culture, Version version)
		{
			return ((RuntimeAssembly)mainAssembly).InternalGetSatelliteAssembly(culture, version, false);
		}

		// Token: 0x060047C1 RID: 18369 RVA: 0x0017E2BB File Offset: 0x0017D4BB
		public ManifestBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
		{
			this._mediator = mediator;
		}

		// Token: 0x060047C2 RID: 18370 RVA: 0x0017E2CC File Offset: 0x0017D4CC
		public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists)
		{
			ResourceSet resourceSet = null;
			Stream stream = null;
			CultureInfo cultureInfo = this.UltimateFallbackFixup(culture);
			Assembly assembly;
			if (cultureInfo.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
			{
				assembly = this._mediator.MainAssembly;
			}
			else
			{
				assembly = this.GetSatelliteAssembly(cultureInfo);
				if (assembly == null)
				{
					bool flag = culture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite;
					if (flag)
					{
						this.HandleSatelliteMissing();
					}
				}
			}
			string resourceFileName = this._mediator.GetResourceFileName(cultureInfo);
			if (assembly != null)
			{
				lock (localResourceSets)
				{
					localResourceSets.TryGetValue(culture.Name, out resourceSet);
				}
				stream = this.GetManifestResourceStream(assembly, resourceFileName);
			}
			if (createIfNotExists && stream != null && resourceSet == null)
			{
				resourceSet = this.CreateResourceSet(stream, assembly);
			}
			else if (stream == null && tryParents)
			{
				bool hasInvariantCultureName = culture.HasInvariantCultureName;
				if (hasInvariantCultureName)
				{
					this.HandleResourceStreamMissing(resourceFileName);
				}
			}
			return resourceSet;
		}

		// Token: 0x060047C3 RID: 18371 RVA: 0x0017E3D0 File Offset: 0x0017D5D0
		private CultureInfo UltimateFallbackFixup(CultureInfo lookForCulture)
		{
			CultureInfo result = lookForCulture;
			if (lookForCulture.Name == this._mediator.NeutralResourcesCulture.Name && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.MainAssembly)
			{
				result = CultureInfo.InvariantCulture;
			}
			else if (lookForCulture.HasInvariantCultureName && this._mediator.FallbackLoc == UltimateResourceFallbackLocation.Satellite)
			{
				result = this._mediator.NeutralResourcesCulture;
			}
			return result;
		}

		// Token: 0x060047C4 RID: 18372 RVA: 0x0017E434 File Offset: 0x0017D634
		internal static CultureInfo GetNeutralResourcesLanguage(Assembly a, out UltimateResourceFallbackLocation fallbackLocation)
		{
			NeutralResourcesLanguageAttribute customAttribute = a.GetCustomAttribute<NeutralResourcesLanguageAttribute>();
			if (customAttribute == null)
			{
				fallbackLocation = UltimateResourceFallbackLocation.MainAssembly;
				return CultureInfo.InvariantCulture;
			}
			fallbackLocation = customAttribute.Location;
			if (fallbackLocation < UltimateResourceFallbackLocation.MainAssembly || fallbackLocation > UltimateResourceFallbackLocation.Satellite)
			{
				throw new ArgumentException(SR.Format(SR.Arg_InvalidNeutralResourcesLanguage_FallbackLoc, fallbackLocation));
			}
			CultureInfo result;
			try
			{
				result = CultureInfo.GetCultureInfo(customAttribute.CultureName);
			}
			catch (ArgumentException innerException)
			{
				if (!(a == typeof(object).Assembly))
				{
					throw new ArgumentException(SR.Format(SR.Arg_InvalidNeutralResourcesLanguage_Asm_Culture, a, customAttribute.CultureName), innerException);
				}
				result = CultureInfo.InvariantCulture;
			}
			return result;
		}

		// Token: 0x060047C5 RID: 18373 RVA: 0x0017E4D8 File Offset: 0x0017D6D8
		internal ResourceSet CreateResourceSet(Stream store, Assembly assembly)
		{
			if (store.CanSeek && store.Length > 4L)
			{
				long position = store.Position;
				BinaryReader binaryReader = new BinaryReader(store);
				int num = binaryReader.ReadInt32();
				if (num == ResourceManager.MagicNumber)
				{
					int num2 = binaryReader.ReadInt32();
					string text;
					string text2;
					if (num2 == ResourceManager.HeaderVersionNumber)
					{
						binaryReader.ReadInt32();
						text = binaryReader.ReadString();
						text2 = binaryReader.ReadString();
					}
					else
					{
						if (num2 <= ResourceManager.HeaderVersionNumber)
						{
							throw new NotSupportedException(SR.Format(SR.NotSupported_ObsoleteResourcesFile, this._mediator.MainAssembly.GetName().Name));
						}
						int num3 = binaryReader.ReadInt32();
						long offset = binaryReader.BaseStream.Position + (long)num3;
						text = binaryReader.ReadString();
						text2 = binaryReader.ReadString();
						binaryReader.BaseStream.Seek(offset, SeekOrigin.Begin);
					}
					store.Position = position;
					if (this.CanUseDefaultResourceClasses(text, text2))
					{
						return new RuntimeResourceSet(store, true);
					}
					IResourceReader resourceReader;
					if (ResourceManager.IsDefaultType(text, "System.Resources.ResourceReader"))
					{
						resourceReader = new ResourceReader(store, new Dictionary<string, ResourceLocator>(FastResourceComparer.Default), true);
					}
					else
					{
						Type type = Type.GetType(text, true);
						resourceReader = (IResourceReader)Activator.CreateInstance(type, new object[]
						{
							store
						});
					}
					object[] args = new object[]
					{
						resourceReader
					};
					Type type2;
					if (this._mediator.UserResourceSet == null)
					{
						type2 = Type.GetType(text2, true, false);
					}
					else
					{
						type2 = this._mediator.UserResourceSet;
					}
					return (ResourceSet)Activator.CreateInstance(type2, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.CreateInstance, null, args, null, null);
				}
				else
				{
					store.Position = position;
				}
			}
			if (this._mediator.UserResourceSet == null)
			{
				return new RuntimeResourceSet(store, true);
			}
			object[] args2 = new object[]
			{
				store,
				assembly
			};
			ResourceSet result;
			try
			{
				try
				{
					return (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, args2);
				}
				catch (MissingMethodException)
				{
				}
				args2 = new object[]
				{
					store
				};
				result = (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, args2);
			}
			catch (MissingMethodException innerException)
			{
				throw new InvalidOperationException(SR.Format(SR.InvalidOperation_ResMgrBadResSet_Type, this._mediator.UserResourceSet.AssemblyQualifiedName), innerException);
			}
			return result;
		}

		// Token: 0x060047C6 RID: 18374 RVA: 0x0017E72C File Offset: 0x0017D92C
		private Stream GetManifestResourceStream(Assembly satellite, string fileName)
		{
			return satellite.GetManifestResourceStream(this._mediator.LocationInfo, fileName) ?? this.CaseInsensitiveManifestResourceStreamLookup(satellite, fileName);
		}

		// Token: 0x060047C7 RID: 18375 RVA: 0x0017E74C File Offset: 0x0017D94C
		private Stream CaseInsensitiveManifestResourceStreamLookup(Assembly satellite, string name)
		{
			Type locationInfo = this._mediator.LocationInfo;
			string text = (locationInfo != null) ? locationInfo.Namespace : null;
			char delimiter = Type.Delimiter;
			string text2 = (text != null && name != null) ? (text + new ReadOnlySpan<char>(ref delimiter, 1) + name) : (text + name);
			string text3 = null;
			foreach (string text4 in satellite.GetManifestResourceNames())
			{
				if (string.Equals(text4, text2, StringComparison.InvariantCultureIgnoreCase))
				{
					if (text3 != null)
					{
						throw new MissingManifestResourceException(SR.Format(SR.MissingManifestResource_MultipleBlobs, text2, satellite.ToString()));
					}
					text3 = text4;
				}
			}
			if (text3 == null)
			{
				return null;
			}
			return satellite.GetManifestResourceStream(text3);
		}

		// Token: 0x060047C8 RID: 18376 RVA: 0x0017E7FC File Offset: 0x0017D9FC
		private Assembly GetSatelliteAssembly(CultureInfo lookForCulture)
		{
			if (!this._mediator.LookedForSatelliteContractVersion)
			{
				this._mediator.SatelliteContractVersion = ResourceManager.ResourceManagerMediator.ObtainSatelliteContractVersion(this._mediator.MainAssembly);
				this._mediator.LookedForSatelliteContractVersion = true;
			}
			Assembly result = null;
			try
			{
				result = ManifestBasedResourceGroveler.InternalGetSatelliteAssembly(this._mediator.MainAssembly, lookForCulture, this._mediator.SatelliteContractVersion);
			}
			catch (FileLoadException)
			{
			}
			catch (BadImageFormatException)
			{
			}
			return result;
		}

		// Token: 0x060047C9 RID: 18377 RVA: 0x0017E884 File Offset: 0x0017DA84
		private bool CanUseDefaultResourceClasses(string readerTypeName, string resSetTypeName)
		{
			return !(this._mediator.UserResourceSet != null) && (readerTypeName == null || ResourceManager.IsDefaultType(readerTypeName, "System.Resources.ResourceReader")) && (resSetTypeName == null || ResourceManager.IsDefaultType(resSetTypeName, "System.Resources.RuntimeResourceSet"));
		}

		// Token: 0x060047CA RID: 18378 RVA: 0x0017E8C0 File Offset: 0x0017DAC0
		private void HandleSatelliteMissing()
		{
			string text = this._mediator.MainAssembly.GetName().Name + ".resources.dll";
			if (this._mediator.SatelliteContractVersion != null)
			{
				text = text + ", Version=" + this._mediator.SatelliteContractVersion.ToString();
			}
			byte[] publicKeyToken = this._mediator.MainAssembly.GetName().GetPublicKeyToken();
			if (publicKeyToken != null)
			{
				int num = publicKeyToken.Length;
				StringBuilder stringBuilder = new StringBuilder(num * 2);
				for (int i = 0; i < num; i++)
				{
					stringBuilder.Append(publicKeyToken[i].ToString("x", CultureInfo.InvariantCulture));
				}
				string str = text;
				string str2 = ", PublicKeyToken=";
				StringBuilder stringBuilder2 = stringBuilder;
				text = str + str2 + ((stringBuilder2 != null) ? stringBuilder2.ToString() : null);
			}
			string text2 = this._mediator.NeutralResourcesCulture.Name;
			if (text2.Length == 0)
			{
				text2 = "<invariant>";
			}
			throw new MissingSatelliteAssemblyException(SR.Format(SR.MissingSatelliteAssembly_Culture_Name, this._mediator.NeutralResourcesCulture, text), text2);
		}

		// Token: 0x060047CB RID: 18379 RVA: 0x0017E9C8 File Offset: 0x0017DBC8
		private static string GetManifestResourceNamesList(Assembly assembly)
		{
			string result;
			try
			{
				string[] manifestResourceNames = assembly.GetManifestResourceNames();
				int num = manifestResourceNames.Length;
				string str = "\"";
				if (num > 10)
				{
					num = 10;
					str = "\", ...";
				}
				result = "\"" + string.Join("\", \"", manifestResourceNames, 0, num) + str;
			}
			catch
			{
				result = "\"\"";
			}
			return result;
		}

		// Token: 0x060047CC RID: 18380 RVA: 0x0017EA2C File Offset: 0x0017DC2C
		private void HandleResourceStreamMissing(string fileName)
		{
			if (this._mediator.MainAssembly == typeof(object).Assembly && this._mediator.BaseName.Equals("System.Private.CoreLib"))
			{
				Environment.FailFast("System.Private.CoreLib.resources couldn't be found!  Large parts of the BCL won't work!");
			}
			string text = string.Empty;
			if (this._mediator.LocationInfo != null && this._mediator.LocationInfo.Namespace != null)
			{
				text = this._mediator.LocationInfo.Namespace + Type.Delimiter.ToString();
			}
			text += fileName;
			throw new MissingManifestResourceException(SR.Format(SR.MissingManifestResource_NoNeutralAsm, text, this._mediator.MainAssembly.GetName().Name, ManifestBasedResourceGroveler.GetManifestResourceNamesList(this._mediator.MainAssembly)));
		}

		// Token: 0x04001159 RID: 4441
		private readonly ResourceManager.ResourceManagerMediator _mediator;
	}
}
