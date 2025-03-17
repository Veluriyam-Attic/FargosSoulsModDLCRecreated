using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x02000400 RID: 1024
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceExposureAttribute : Attribute
	{
		// Token: 0x170009D3 RID: 2515
		// (get) Token: 0x06003296 RID: 12950 RVA: 0x0016B7E9 File Offset: 0x0016A9E9
		public ResourceScope ResourceExposureLevel { get; }

		// Token: 0x06003297 RID: 12951 RVA: 0x0016B7F1 File Offset: 0x0016A9F1
		public ResourceExposureAttribute(ResourceScope exposureLevel)
		{
			this.ResourceExposureLevel = exposureLevel;
		}
	}
}
