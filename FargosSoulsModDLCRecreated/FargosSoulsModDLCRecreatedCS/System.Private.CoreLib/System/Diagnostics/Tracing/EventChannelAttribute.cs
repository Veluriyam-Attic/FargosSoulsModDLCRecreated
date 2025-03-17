using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000738 RID: 1848
	[AttributeUsage(AttributeTargets.Field)]
	internal class EventChannelAttribute : Attribute
	{
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06005B56 RID: 23382 RVA: 0x001BC610 File Offset: 0x001BB810
		// (set) Token: 0x06005B57 RID: 23383 RVA: 0x001BC618 File Offset: 0x001BB818
		public bool Enabled { get; set; }

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06005B58 RID: 23384 RVA: 0x001BC621 File Offset: 0x001BB821
		// (set) Token: 0x06005B59 RID: 23385 RVA: 0x001BC629 File Offset: 0x001BB829
		public EventChannelType EventChannelType { get; set; }
	}
}
