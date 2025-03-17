using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005E4 RID: 1508
	[Nullable(0)]
	[NullableContext(2)]
	public class ManifestResourceInfo
	{
		// Token: 0x06004C72 RID: 19570 RVA: 0x0018BC57 File Offset: 0x0018AE57
		public ManifestResourceInfo(Assembly containingAssembly, string containingFileName, ResourceLocation resourceLocation)
		{
			this.ReferencedAssembly = containingAssembly;
			this.FileName = containingFileName;
			this.ResourceLocation = resourceLocation;
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06004C73 RID: 19571 RVA: 0x0018BC74 File Offset: 0x0018AE74
		public virtual Assembly ReferencedAssembly { get; }

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06004C74 RID: 19572 RVA: 0x0018BC7C File Offset: 0x0018AE7C
		public virtual string FileName { get; }

		// Token: 0x17000C15 RID: 3093
		// (get) Token: 0x06004C75 RID: 19573 RVA: 0x0018BC84 File Offset: 0x0018AE84
		public virtual ResourceLocation ResourceLocation { get; }
	}
}
