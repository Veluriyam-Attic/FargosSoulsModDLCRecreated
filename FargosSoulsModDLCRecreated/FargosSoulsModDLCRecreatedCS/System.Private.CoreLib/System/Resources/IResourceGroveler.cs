using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.Resources
{
	// Token: 0x02000572 RID: 1394
	internal interface IResourceGroveler
	{
		// Token: 0x060047DD RID: 18397
		ResourceSet GrovelForResourceSet(CultureInfo culture, Dictionary<string, ResourceSet> localResourceSets, bool tryParents, bool createIfNotExists);
	}
}
