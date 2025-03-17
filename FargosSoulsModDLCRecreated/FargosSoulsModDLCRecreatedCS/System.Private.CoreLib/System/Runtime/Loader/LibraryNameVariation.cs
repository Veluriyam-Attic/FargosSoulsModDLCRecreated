using System;
using System.Collections.Generic;

namespace System.Runtime.Loader
{
	// Token: 0x0200040F RID: 1039
	internal struct LibraryNameVariation
	{
		// Token: 0x06003318 RID: 13080 RVA: 0x0016D08B File Offset: 0x0016C28B
		public LibraryNameVariation(string prefix, string suffix)
		{
			this.Prefix = prefix;
			this.Suffix = suffix;
		}

		// Token: 0x06003319 RID: 13081 RVA: 0x0016D09B File Offset: 0x0016C29B
		internal static IEnumerable<LibraryNameVariation> DetermineLibraryNameVariations(string libName, bool isRelativePath, bool forOSLoader = false)
		{
			yield return new LibraryNameVariation(string.Empty, string.Empty);
			if (isRelativePath && (!forOSLoader || (libName.Contains('.') && !libName.EndsWith('.'))) && !libName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase) && !libName.EndsWith(".exe", StringComparison.OrdinalIgnoreCase))
			{
				yield return new LibraryNameVariation(string.Empty, ".dll");
			}
			yield break;
		}

		// Token: 0x04000E6E RID: 3694
		public string Prefix;

		// Token: 0x04000E6F RID: 3695
		public string Suffix;
	}
}
