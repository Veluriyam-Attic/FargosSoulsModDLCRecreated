using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000735 RID: 1845
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class EventSourceAttribute : Attribute
	{
		// Token: 0x17000EFC RID: 3836
		// (get) Token: 0x06005B38 RID: 23352 RVA: 0x001BC50E File Offset: 0x001BB70E
		// (set) Token: 0x06005B39 RID: 23353 RVA: 0x001BC516 File Offset: 0x001BB716
		public string Name { get; set; }

		// Token: 0x17000EFD RID: 3837
		// (get) Token: 0x06005B3A RID: 23354 RVA: 0x001BC51F File Offset: 0x001BB71F
		// (set) Token: 0x06005B3B RID: 23355 RVA: 0x001BC527 File Offset: 0x001BB727
		public string Guid { get; set; }

		// Token: 0x17000EFE RID: 3838
		// (get) Token: 0x06005B3C RID: 23356 RVA: 0x001BC530 File Offset: 0x001BB730
		// (set) Token: 0x06005B3D RID: 23357 RVA: 0x001BC538 File Offset: 0x001BB738
		public string LocalizationResources { get; set; }
	}
}
