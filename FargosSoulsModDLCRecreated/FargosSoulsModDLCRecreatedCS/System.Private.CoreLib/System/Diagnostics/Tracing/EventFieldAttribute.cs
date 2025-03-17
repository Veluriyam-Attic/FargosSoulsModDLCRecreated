using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200075F RID: 1887
	[AttributeUsage(AttributeTargets.Property)]
	public class EventFieldAttribute : Attribute
	{
		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x06005C87 RID: 23687 RVA: 0x001C191B File Offset: 0x001C0B1B
		// (set) Token: 0x06005C88 RID: 23688 RVA: 0x001C1923 File Offset: 0x001C0B23
		public EventFieldTags Tags { get; set; }

		// Token: 0x17000F1A RID: 3866
		// (get) Token: 0x06005C89 RID: 23689 RVA: 0x001C192C File Offset: 0x001C0B2C
		[Nullable(2)]
		internal string Name { get; }

		// Token: 0x17000F1B RID: 3867
		// (get) Token: 0x06005C8A RID: 23690 RVA: 0x001C1934 File Offset: 0x001C0B34
		// (set) Token: 0x06005C8B RID: 23691 RVA: 0x001C193C File Offset: 0x001C0B3C
		public EventFieldFormat Format { get; set; }
	}
}
