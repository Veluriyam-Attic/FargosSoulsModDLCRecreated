using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x020003FB RID: 1019
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class OSPlatformAttribute : Attribute
	{
		// Token: 0x0600328D RID: 12941 RVA: 0x0016B78D File Offset: 0x0016A98D
		private protected OSPlatformAttribute(string platformName)
		{
			this.PlatformName = platformName;
		}

		// Token: 0x170009D0 RID: 2512
		// (get) Token: 0x0600328E RID: 12942 RVA: 0x0016B79C File Offset: 0x0016A99C
		public string PlatformName { get; }
	}
}
