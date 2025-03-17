using System;
using System.Diagnostics;

namespace System.Runtime.Versioning
{
	// Token: 0x020003FF RID: 1023
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	[Conditional("RESOURCE_ANNOTATION_WORK")]
	public sealed class ResourceConsumptionAttribute : Attribute
	{
		// Token: 0x170009D1 RID: 2513
		// (get) Token: 0x06003292 RID: 12946 RVA: 0x0016B7AD File Offset: 0x0016A9AD
		public ResourceScope ResourceScope { get; }

		// Token: 0x170009D2 RID: 2514
		// (get) Token: 0x06003293 RID: 12947 RVA: 0x0016B7B5 File Offset: 0x0016A9B5
		public ResourceScope ConsumptionScope { get; }

		// Token: 0x06003294 RID: 12948 RVA: 0x0016B7BD File Offset: 0x0016A9BD
		public ResourceConsumptionAttribute(ResourceScope resourceScope)
		{
			this.ResourceScope = resourceScope;
			this.ConsumptionScope = resourceScope;
		}

		// Token: 0x06003295 RID: 12949 RVA: 0x0016B7D3 File Offset: 0x0016A9D3
		public ResourceConsumptionAttribute(ResourceScope resourceScope, ResourceScope consumptionScope)
		{
			this.ResourceScope = resourceScope;
			this.ConsumptionScope = consumptionScope;
		}
	}
}
