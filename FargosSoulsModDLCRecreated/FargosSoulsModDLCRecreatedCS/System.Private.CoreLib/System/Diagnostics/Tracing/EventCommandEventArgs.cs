using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000732 RID: 1842
	public class EventCommandEventArgs : EventArgs
	{
		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06005B13 RID: 23315 RVA: 0x001BC0EB File Offset: 0x001BB2EB
		// (set) Token: 0x06005B14 RID: 23316 RVA: 0x001BC0F3 File Offset: 0x001BB2F3
		public EventCommand Command { get; internal set; }

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06005B15 RID: 23317 RVA: 0x001BC0FC File Offset: 0x001BB2FC
		// (set) Token: 0x06005B16 RID: 23318 RVA: 0x001BC104 File Offset: 0x001BB304
		[Nullable(new byte[]
		{
			2,
			1,
			2
		})]
		public IDictionary<string, string> Arguments { [return: Nullable(new byte[]
		{
			2,
			1,
			2
		})] get; internal set; }

		// Token: 0x06005B17 RID: 23319 RVA: 0x001BC10D File Offset: 0x001BB30D
		public bool EnableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, this.eventProviderType, eventId, true);
		}

		// Token: 0x06005B18 RID: 23320 RVA: 0x001BC142 File Offset: 0x001BB342
		public bool DisableEvent(int eventId)
		{
			if (this.Command != EventCommand.Enable && this.Command != EventCommand.Disable)
			{
				throw new InvalidOperationException();
			}
			return this.eventSource.EnableEventForDispatcher(this.dispatcher, this.eventProviderType, eventId, false);
		}

		// Token: 0x06005B19 RID: 23321 RVA: 0x001BC178 File Offset: 0x001BB378
		internal EventCommandEventArgs(EventCommand command, IDictionary<string, string> arguments, EventSource eventSource, EventListener listener, EventProviderType eventProviderType, int perEventSourceSessionId, int etwSessionId, bool enable, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.Command = command;
			this.Arguments = arguments;
			this.eventSource = eventSource;
			this.listener = listener;
			this.eventProviderType = eventProviderType;
			this.perEventSourceSessionId = perEventSourceSessionId;
			this.etwSessionId = etwSessionId;
			this.enable = enable;
			this.level = level;
			this.matchAnyKeyword = matchAnyKeyword;
		}

		// Token: 0x04001AC6 RID: 6854
		internal EventSource eventSource;

		// Token: 0x04001AC7 RID: 6855
		internal EventDispatcher dispatcher;

		// Token: 0x04001AC8 RID: 6856
		internal EventProviderType eventProviderType;

		// Token: 0x04001AC9 RID: 6857
		internal EventListener listener;

		// Token: 0x04001ACA RID: 6858
		internal int perEventSourceSessionId;

		// Token: 0x04001ACB RID: 6859
		internal int etwSessionId;

		// Token: 0x04001ACC RID: 6860
		internal bool enable;

		// Token: 0x04001ACD RID: 6861
		internal EventLevel level;

		// Token: 0x04001ACE RID: 6862
		internal EventKeywords matchAnyKeyword;

		// Token: 0x04001ACF RID: 6863
		internal EventCommandEventArgs nextCommand;
	}
}
