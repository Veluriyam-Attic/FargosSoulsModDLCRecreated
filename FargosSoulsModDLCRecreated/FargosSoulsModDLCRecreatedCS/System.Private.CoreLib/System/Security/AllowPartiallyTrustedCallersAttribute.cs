using System;

namespace System.Security
{
	// Token: 0x020003B3 RID: 947
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class AllowPartiallyTrustedCallersAttribute : Attribute
	{
		// Token: 0x1700097E RID: 2430
		// (get) Token: 0x0600310A RID: 12554 RVA: 0x00168691 File Offset: 0x00167891
		// (set) Token: 0x0600310B RID: 12555 RVA: 0x00168699 File Offset: 0x00167899
		public PartialTrustVisibilityLevel PartialTrustVisibilityLevel { get; set; }
	}
}
