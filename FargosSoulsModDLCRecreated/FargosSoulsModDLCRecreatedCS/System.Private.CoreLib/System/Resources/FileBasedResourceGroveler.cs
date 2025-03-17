using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Internal.IO;

namespace System.Resources
{
	// Token: 0x02000571 RID: 1393
	internal class FileBasedResourceGroveler : IResourceGroveler
	{
		// Token: 0x060047D9 RID: 18393 RVA: 0x0017ECAA File Offset: 0x0017DEAA
		public FileBasedResourceGroveler(ResourceManager.ResourceManagerMediator mediator)
		{
			this._mediator = mediator;
		}

		// Token: 0x060047DA RID: 18394 RVA: 0x0017ECBC File Offset: 0x0017DEBC
		public ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists)
		{
			ResourceSet result = null;
			string resourceFileName = this._mediator.GetResourceFileName(culture);
			string text = this.FindResourceFile(culture, resourceFileName);
			if (text == null)
			{
				if (tryParents && culture.HasInvariantCultureName)
				{
					throw new MissingManifestResourceException(string.Concat(new string[]
					{
						SR.MissingManifestResource_NoNeutralDisk,
						"\r\nbaseName: ",
						this._mediator.BaseNameField,
						"  locationInfo: ",
						(this._mediator.LocationInfo == null) ? "<null>" : this._mediator.LocationInfo.FullName,
						"  fileName: ",
						this._mediator.GetResourceFileName(culture)
					}));
				}
			}
			else
			{
				result = this.CreateResourceSet(text);
			}
			return result;
		}

		// Token: 0x060047DB RID: 18395 RVA: 0x0017ED7C File Offset: 0x0017DF7C
		private string FindResourceFile(CultureInfo culture, string fileName)
		{
			if (this._mediator.ModuleDir != null)
			{
				string text = Path.Combine(this._mediator.ModuleDir, fileName);
				if (File.Exists(text))
				{
					return text;
				}
			}
			if (File.Exists(fileName))
			{
				return fileName;
			}
			return null;
		}

		// Token: 0x060047DC RID: 18396 RVA: 0x0017EDC0 File Offset: 0x0017DFC0
		private ResourceSet CreateResourceSet(string file)
		{
			if (this._mediator.UserResourceSet == null)
			{
				return new RuntimeResourceSet(file);
			}
			object[] args = new object[]
			{
				file
			};
			ResourceSet result;
			try
			{
				result = (ResourceSet)Activator.CreateInstance(this._mediator.UserResourceSet, args);
			}
			catch (MissingMethodException innerException)
			{
				throw new InvalidOperationException(SR.Format(SR.InvalidOperation_ResMgrBadResSet_Type, this._mediator.UserResourceSet.AssemblyQualifiedName), innerException);
			}
			return result;
		}

		// Token: 0x0400115B RID: 4443
		private readonly ResourceManager.ResourceManagerMediator _mediator;
	}
}
