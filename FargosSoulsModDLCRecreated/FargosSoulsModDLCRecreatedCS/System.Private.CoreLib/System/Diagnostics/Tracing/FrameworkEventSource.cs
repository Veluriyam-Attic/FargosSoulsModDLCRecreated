using System;
using Internal.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000744 RID: 1860
	[EventSource(Guid = "8E9F5090-2D75-4d03-8A81-E5AFBF85DAF1", Name = "System.Diagnostics.Eventing.FrameworkEventSource")]
	internal sealed class FrameworkEventSource : EventSource
	{
		// Token: 0x06005B88 RID: 23432 RVA: 0x001BE2FC File Offset: 0x001BD4FC
		private FrameworkEventSource() : base(new Guid(2392805520U, 11637, 19715, 138, 129, 229, 175, 191, 133, 218, 241), "System.Diagnostics.Eventing.FrameworkEventSource")
		{
		}

		// Token: 0x06005B89 RID: 23433 RVA: 0x001BE350 File Offset: 0x001BD550
		[NonEvent]
		private unsafe void WriteEvent(int eventId, long arg1, int arg2, string arg3, bool arg4, int arg5, int arg6)
		{
			if (base.IsEnabled())
			{
				if (arg3 == null)
				{
					arg3 = "";
				}
				char* ptr;
				if (arg3 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg3.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)6) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr3->DataPointer = (IntPtr)((void*)(&arg1));
				ptr3->Size = 8;
				ptr3->Reserved = 0;
				ptr3[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr3[1].Size = 4;
				ptr3[1].Reserved = 0;
				ptr3[2].DataPointer = (IntPtr)((void*)value);
				ptr3[2].Size = (arg3.Length + 1) * 2;
				ptr3[2].Reserved = 0;
				ptr3[3].DataPointer = (IntPtr)((void*)(&arg4));
				ptr3[3].Size = 4;
				ptr3[3].Reserved = 0;
				ptr3[4].DataPointer = (IntPtr)((void*)(&arg5));
				ptr3[4].Size = 4;
				ptr3[4].Reserved = 0;
				ptr3[5].DataPointer = (IntPtr)((void*)(&arg6));
				ptr3[5].Size = 4;
				ptr3[5].Reserved = 0;
				base.WriteEventCore(eventId, 6, ptr3);
				char* ptr2 = null;
			}
		}

		// Token: 0x06005B8A RID: 23434 RVA: 0x001BE4DC File Offset: 0x001BD6DC
		[NonEvent]
		private unsafe void WriteEvent(int eventId, long arg1, int arg2, string arg3)
		{
			if (base.IsEnabled())
			{
				if (arg3 == null)
				{
					arg3 = "";
				}
				char* ptr;
				if (arg3 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg3.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr3->DataPointer = (IntPtr)((void*)(&arg1));
				ptr3->Size = 8;
				ptr3->Reserved = 0;
				ptr3[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr3[1].Size = 4;
				ptr3[1].Reserved = 0;
				ptr3[2].DataPointer = (IntPtr)((void*)value);
				ptr3[2].Size = (arg3.Length + 1) * 2;
				ptr3[2].Reserved = 0;
				base.WriteEventCore(eventId, 3, ptr3);
				char* ptr2 = null;
			}
		}

		// Token: 0x06005B8B RID: 23435 RVA: 0x001BE5B9 File Offset: 0x001BD7B9
		[Event(30, Level = EventLevel.Verbose, Keywords = (EventKeywords)18L)]
		public void ThreadPoolEnqueueWork(long workID)
		{
			base.WriteEvent(30, workID);
		}

		// Token: 0x06005B8C RID: 23436 RVA: 0x001BE5C4 File Offset: 0x001BD7C4
		[NonEvent]
		public unsafe void ThreadPoolEnqueueWorkObject(object workID)
		{
			this.ThreadPoolEnqueueWork((long)((ulong)(*(IntPtr*)Unsafe.AsPointer<object>(ref workID))));
		}

		// Token: 0x06005B8D RID: 23437 RVA: 0x001BE5D5 File Offset: 0x001BD7D5
		[Event(31, Level = EventLevel.Verbose, Keywords = (EventKeywords)18L)]
		public void ThreadPoolDequeueWork(long workID)
		{
			base.WriteEvent(31, workID);
		}

		// Token: 0x06005B8E RID: 23438 RVA: 0x001BE5E0 File Offset: 0x001BD7E0
		[NonEvent]
		public unsafe void ThreadPoolDequeueWorkObject(object workID)
		{
			this.ThreadPoolDequeueWork((long)((ulong)(*(IntPtr*)Unsafe.AsPointer<object>(ref workID))));
		}

		// Token: 0x06005B8F RID: 23439 RVA: 0x001BE5F1 File Offset: 0x001BD7F1
		[Event(150, Level = EventLevel.Informational, Keywords = (EventKeywords)16L, Task = (EventTask)3, Opcode = EventOpcode.Send)]
		public void ThreadTransferSend(long id, int kind, string info, bool multiDequeues, int intInfo1, int intInfo2)
		{
			this.WriteEvent(150, id, kind, info, multiDequeues, intInfo1, intInfo2);
		}

		// Token: 0x06005B90 RID: 23440 RVA: 0x001BE607 File Offset: 0x001BD807
		[NonEvent]
		public unsafe void ThreadTransferSendObj(object id, int kind, string info, bool multiDequeues, int intInfo1, int intInfo2)
		{
			this.ThreadTransferSend((long)((ulong)(*(IntPtr*)Unsafe.AsPointer<object>(ref id))), kind, info, multiDequeues, intInfo1, intInfo2);
		}

		// Token: 0x06005B91 RID: 23441 RVA: 0x001BE620 File Offset: 0x001BD820
		[Event(151, Level = EventLevel.Informational, Keywords = (EventKeywords)16L, Task = (EventTask)3, Opcode = EventOpcode.Receive)]
		public void ThreadTransferReceive(long id, int kind, string info)
		{
			this.WriteEvent(151, id, kind, info);
		}

		// Token: 0x06005B92 RID: 23442 RVA: 0x001BE630 File Offset: 0x001BD830
		[NonEvent]
		public unsafe void ThreadTransferReceiveObj(object id, int kind, string info)
		{
			this.ThreadTransferReceive((long)((ulong)(*(IntPtr*)Unsafe.AsPointer<object>(ref id))), kind, info);
		}

		// Token: 0x04001B23 RID: 6947
		public static readonly FrameworkEventSource Log = new FrameworkEventSource();

		// Token: 0x02000745 RID: 1861
		public static class Keywords
		{
			// Token: 0x04001B24 RID: 6948
			public const EventKeywords ThreadPool = (EventKeywords)2L;

			// Token: 0x04001B25 RID: 6949
			public const EventKeywords ThreadTransfer = (EventKeywords)16L;
		}

		// Token: 0x02000746 RID: 1862
		public static class Tasks
		{
			// Token: 0x04001B26 RID: 6950
			public const EventTask ThreadTransfer = (EventTask)3;
		}
	}
}
