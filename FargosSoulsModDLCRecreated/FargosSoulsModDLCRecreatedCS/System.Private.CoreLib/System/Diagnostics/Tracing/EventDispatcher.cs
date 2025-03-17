using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200073C RID: 1852
	internal class EventDispatcher
	{
		// Token: 0x06005B60 RID: 23392 RVA: 0x001BC667 File Offset: 0x001BB867
		internal EventDispatcher(EventDispatcher next, bool[] eventEnabled, EventListener listener)
		{
			this.m_Next = next;
			this.m_EventEnabled = eventEnabled;
			this.m_Listener = listener;
		}

		// Token: 0x04001AFA RID: 6906
		internal readonly EventListener m_Listener;

		// Token: 0x04001AFB RID: 6907
		internal bool[] m_EventEnabled;

		// Token: 0x04001AFC RID: 6908
		internal EventDispatcher m_Next;
	}
}
