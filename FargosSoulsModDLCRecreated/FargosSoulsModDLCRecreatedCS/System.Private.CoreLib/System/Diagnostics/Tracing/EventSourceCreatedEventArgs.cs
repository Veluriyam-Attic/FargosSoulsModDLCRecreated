using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000733 RID: 1843
	[NullableContext(2)]
	[Nullable(0)]
	public class EventSourceCreatedEventArgs : EventArgs
	{
		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06005B1A RID: 23322 RVA: 0x001BC1D8 File Offset: 0x001BB3D8
		// (set) Token: 0x06005B1B RID: 23323 RVA: 0x001BC1E0 File Offset: 0x001BB3E0
		public EventSource EventSource { get; internal set; }
	}
}
