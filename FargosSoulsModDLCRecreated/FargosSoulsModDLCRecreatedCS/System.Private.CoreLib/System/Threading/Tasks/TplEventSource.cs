using System;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using Internal.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000342 RID: 834
	[EventSource(Name = "System.Threading.Tasks.TplEventSource", Guid = "2e5dba47-a3d2-4d16-8ee0-6671ffdcd7b5", LocalizationResources = "System.Private.CoreLib.Strings")]
	internal sealed class TplEventSource : EventSource
	{
		// Token: 0x06002C2E RID: 11310 RVA: 0x00153C3C File Offset: 0x00152E3C
		protected override void OnEventCommand(EventCommandEventArgs command)
		{
			if (base.IsEnabled(EventLevel.Informational, (EventKeywords)128L))
			{
				ActivityTracker.Instance.Enable();
			}
			else
			{
				this.TasksSetActivityIds = base.IsEnabled(EventLevel.Informational, (EventKeywords)65536L);
			}
			this.Debug = base.IsEnabled(EventLevel.Informational, (EventKeywords)131072L);
			this.DebugActivityId = base.IsEnabled(EventLevel.Informational, (EventKeywords)262144L);
		}

		// Token: 0x06002C2F RID: 11311 RVA: 0x00153CA0 File Offset: 0x00152EA0
		private TplEventSource() : base(new Guid(777894471U, 41938, 19734, 142, 224, 102, 113, byte.MaxValue, 220, 215, 181), "System.Threading.Tasks.TplEventSource")
		{
		}

		// Token: 0x06002C30 RID: 11312 RVA: 0x00153CF0 File Offset: 0x00152EF0
		[Event(7, Task = (EventTask)6, Version = 1, Opcode = EventOpcode.Send, Level = EventLevel.Informational, Keywords = (EventKeywords)3L)]
		public unsafe void TaskScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, int CreatingTaskID, int TaskCreationOptions, int appDomain = 1)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)3L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)6) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr->Reserved = 0;
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[1].Reserved = 0;
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&TaskID));
				ptr[2].Reserved = 0;
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&CreatingTaskID));
				ptr[3].Reserved = 0;
				ptr[4].Size = 4;
				ptr[4].DataPointer = (IntPtr)((void*)(&TaskCreationOptions));
				ptr[4].Reserved = 0;
				ptr[5].Size = 4;
				ptr[5].DataPointer = (IntPtr)((void*)(&appDomain));
				ptr[5].Reserved = 0;
				if (this.TasksSetActivityIds)
				{
					Guid guid = TplEventSource.CreateGuidForTaskID(TaskID);
					base.WriteEventWithRelatedActivityIdCore(7, &guid, 6, ptr);
					return;
				}
				base.WriteEventCore(7, 6, ptr);
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x00153E7C File Offset: 0x0015307C
		[Event(8, Level = EventLevel.Informational, Keywords = (EventKeywords)2L)]
		public void TaskStarted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			if (base.IsEnabled(EventLevel.Informational, (EventKeywords)2L))
			{
				base.WriteEvent(8, OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
			}
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x00153E94 File Offset: 0x00153094
		[Event(9, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)64L)]
		public unsafe void TaskCompleted(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, bool IsExceptional)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)2L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)4) * (UIntPtr)sizeof(EventSource.EventData))];
				int num = IsExceptional ? 1 : 0;
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr->Reserved = 0;
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[1].Reserved = 0;
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&TaskID));
				ptr[2].Reserved = 0;
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&num));
				ptr[3].Reserved = 0;
				base.WriteEventCore(9, 4, ptr);
			}
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x00153F9C File Offset: 0x0015319C
		[Event(10, Version = 3, Task = (EventTask)4, Opcode = EventOpcode.Send, Level = EventLevel.Informational, Keywords = (EventKeywords)3L)]
		public unsafe void TaskWaitBegin(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID, TplEventSource.TaskWaitBehavior Behavior, int ContinueWithTaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)3L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)5) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr->Reserved = 0;
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[1].Reserved = 0;
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&TaskID));
				ptr[2].Reserved = 0;
				ptr[3].Size = 4;
				ptr[3].DataPointer = (IntPtr)((void*)(&Behavior));
				ptr[3].Reserved = 0;
				ptr[4].Size = 4;
				ptr[4].DataPointer = (IntPtr)((void*)(&ContinueWithTaskID));
				ptr[4].Reserved = 0;
				if (this.TasksSetActivityIds)
				{
					Guid guid = TplEventSource.CreateGuidForTaskID(TaskID);
					base.WriteEventWithRelatedActivityIdCore(10, &guid, 5, ptr);
					return;
				}
				base.WriteEventCore(10, 5, ptr);
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x001540F0 File Offset: 0x001532F0
		[Event(11, Level = EventLevel.Verbose, Keywords = (EventKeywords)2L)]
		public void TaskWaitEnd(int OriginatingTaskSchedulerID, int OriginatingTaskID, int TaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)2L))
			{
				base.WriteEvent(11, OriginatingTaskSchedulerID, OriginatingTaskID, TaskID);
			}
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x00154110 File Offset: 0x00153310
		[Event(13, Level = EventLevel.Verbose, Keywords = (EventKeywords)64L)]
		public void TaskWaitContinuationComplete(int TaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)2L))
			{
				base.WriteEvent(13, TaskID);
			}
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x0015412E File Offset: 0x0015332E
		[Event(19, Level = EventLevel.Verbose, Keywords = (EventKeywords)64L)]
		public void TaskWaitContinuationStarted(int TaskID)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Verbose, (EventKeywords)2L))
			{
				base.WriteEvent(19, TaskID);
			}
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x0015414C File Offset: 0x0015334C
		[Event(12, Task = (EventTask)7, Opcode = EventOpcode.Send, Level = EventLevel.Informational, Keywords = (EventKeywords)3L)]
		public unsafe void AwaitTaskContinuationScheduled(int OriginatingTaskSchedulerID, int OriginatingTaskID, int ContinueWithTaskId)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)3L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&OriginatingTaskSchedulerID));
				ptr->Reserved = 0;
				ptr[1].Size = 4;
				ptr[1].DataPointer = (IntPtr)((void*)(&OriginatingTaskID));
				ptr[1].Reserved = 0;
				ptr[2].Size = 4;
				ptr[2].DataPointer = (IntPtr)((void*)(&ContinueWithTaskId));
				ptr[2].Reserved = 0;
				if (this.TasksSetActivityIds)
				{
					Guid guid = TplEventSource.CreateGuidForTaskID(ContinueWithTaskId);
					base.WriteEventWithRelatedActivityIdCore(12, &guid, 3, ptr);
					return;
				}
				base.WriteEventCore(12, 3, ptr);
			}
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x0015422C File Offset: 0x0015342C
		[Event(14, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		public unsafe void TraceOperationBegin(int TaskID, string OperationName, long RelatedContext)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)8L))
			{
				char* ptr;
				if (OperationName == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = OperationName.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr3->Size = 4;
				ptr3->DataPointer = (IntPtr)((void*)(&TaskID));
				ptr3->Reserved = 0;
				ptr3[1].Size = (OperationName.Length + 1) * 2;
				ptr3[1].DataPointer = (IntPtr)((void*)value);
				ptr3[1].Reserved = 0;
				ptr3[2].Size = 8;
				ptr3[2].DataPointer = (IntPtr)((void*)(&RelatedContext));
				ptr3[2].Reserved = 0;
				base.WriteEventCore(14, 3, ptr3);
				char* ptr2 = null;
			}
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x0015430A File Offset: 0x0015350A
		[Event(16, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)16L)]
		public void TraceOperationRelation(int TaskID, CausalityRelation Relation)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)16L))
			{
				base.WriteEvent(16, TaskID, (int)Relation);
			}
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x0015432A File Offset: 0x0015352A
		[Event(15, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)8L)]
		public void TraceOperationEnd(int TaskID, AsyncCausalityStatus Status)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)8L))
			{
				base.WriteEvent(15, TaskID, (int)Status);
			}
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x00154349 File Offset: 0x00153549
		[Event(17, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)32L)]
		public void TraceSynchronousWorkBegin(int TaskID, CausalitySynchronousWork Work)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)32L))
			{
				base.WriteEvent(17, TaskID, (int)Work);
			}
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x0015436C File Offset: 0x0015356C
		[Event(18, Version = 1, Level = EventLevel.Informational, Keywords = (EventKeywords)32L)]
		public unsafe void TraceSynchronousWorkEnd(CausalitySynchronousWork Work)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Informational, (EventKeywords)32L))
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->Size = 4;
				ptr->DataPointer = (IntPtr)((void*)(&Work));
				ptr->Reserved = 0;
				base.WriteEventCore(18, 1, ptr);
			}
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x001543BF File Offset: 0x001535BF
		[NonEvent]
		public unsafe void RunningContinuationList(int TaskID, int Index, object Object)
		{
			this.RunningContinuationList(TaskID, Index, (long)((ulong)(*(IntPtr*)Unsafe.AsPointer<object>(ref Object))));
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x001543D2 File Offset: 0x001535D2
		[Event(21, Keywords = (EventKeywords)131072L)]
		public void RunningContinuationList(int TaskID, int Index, long Object)
		{
			if (this.Debug)
			{
				base.WriteEvent(21, (long)TaskID, (long)Index, Object);
			}
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x001543E9 File Offset: 0x001535E9
		[Event(23, Keywords = (EventKeywords)131072L)]
		public void DebugFacilityMessage(string Facility, string Message)
		{
			base.WriteEvent(23, Facility, Message);
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x001543F5 File Offset: 0x001535F5
		[Event(24, Keywords = (EventKeywords)131072L)]
		public void DebugFacilityMessage1(string Facility, string Message, string Value1)
		{
			base.WriteEvent(24, Facility, Message, Value1);
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x00154402 File Offset: 0x00153602
		[Event(25, Keywords = (EventKeywords)262144L)]
		public void SetActivityId(Guid NewId)
		{
			if (this.DebugActivityId)
			{
				base.WriteEvent(25, new object[]
				{
					NewId
				});
			}
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x00154423 File Offset: 0x00153623
		[Event(26, Keywords = (EventKeywords)131072L)]
		public void NewID(int TaskID)
		{
			if (this.Debug)
			{
				base.WriteEvent(26, TaskID);
			}
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x00154438 File Offset: 0x00153638
		[NonEvent]
		public void IncompleteAsyncMethod(IAsyncStateMachineBox stateMachineBox)
		{
			if (base.IsEnabled() && base.IsEnabled(EventLevel.Warning, (EventKeywords)256L))
			{
				IAsyncStateMachine stateMachineObject = stateMachineBox.GetStateMachineObject();
				if (stateMachineObject != null)
				{
					string asyncStateMachineDescription = AsyncMethodBuilderCore.GetAsyncStateMachineDescription(stateMachineObject);
					this.IncompleteAsyncMethod(asyncStateMachineDescription);
				}
			}
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x00154474 File Offset: 0x00153674
		[Event(27, Level = EventLevel.Warning, Keywords = (EventKeywords)256L)]
		private void IncompleteAsyncMethod(string stateMachineDescription)
		{
			base.WriteEvent(27, stateMachineDescription);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x00154480 File Offset: 0x00153680
		internal static Guid CreateGuidForTaskID(int taskID)
		{
			int processId = Environment.ProcessId;
			return new Guid(taskID, 1, 0, (byte)processId, (byte)(processId >> 8), (byte)(processId >> 16), (byte)(processId >> 24), byte.MaxValue, 220, 215, 181);
		}

		// Token: 0x04000C33 RID: 3123
		internal bool TasksSetActivityIds;

		// Token: 0x04000C34 RID: 3124
		internal bool Debug;

		// Token: 0x04000C35 RID: 3125
		private bool DebugActivityId;

		// Token: 0x04000C36 RID: 3126
		public static readonly TplEventSource Log = new TplEventSource();

		// Token: 0x02000343 RID: 835
		public enum TaskWaitBehavior
		{
			// Token: 0x04000C38 RID: 3128
			Synchronous = 1,
			// Token: 0x04000C39 RID: 3129
			Asynchronous
		}

		// Token: 0x02000344 RID: 836
		public static class Tasks
		{
			// Token: 0x04000C3A RID: 3130
			public const EventTask Loop = (EventTask)1;

			// Token: 0x04000C3B RID: 3131
			public const EventTask Invoke = (EventTask)2;

			// Token: 0x04000C3C RID: 3132
			public const EventTask TaskExecute = (EventTask)3;

			// Token: 0x04000C3D RID: 3133
			public const EventTask TaskWait = (EventTask)4;

			// Token: 0x04000C3E RID: 3134
			public const EventTask ForkJoin = (EventTask)5;

			// Token: 0x04000C3F RID: 3135
			public const EventTask TaskScheduled = (EventTask)6;

			// Token: 0x04000C40 RID: 3136
			public const EventTask AwaitTaskContinuationScheduled = (EventTask)7;

			// Token: 0x04000C41 RID: 3137
			public const EventTask TraceOperation = (EventTask)8;

			// Token: 0x04000C42 RID: 3138
			public const EventTask TraceSynchronousWork = (EventTask)9;
		}

		// Token: 0x02000345 RID: 837
		public static class Keywords
		{
			// Token: 0x04000C43 RID: 3139
			public const EventKeywords TaskTransfer = (EventKeywords)1L;

			// Token: 0x04000C44 RID: 3140
			public const EventKeywords Tasks = (EventKeywords)2L;

			// Token: 0x04000C45 RID: 3141
			public const EventKeywords Parallel = (EventKeywords)4L;

			// Token: 0x04000C46 RID: 3142
			public const EventKeywords AsyncCausalityOperation = (EventKeywords)8L;

			// Token: 0x04000C47 RID: 3143
			public const EventKeywords AsyncCausalityRelation = (EventKeywords)16L;

			// Token: 0x04000C48 RID: 3144
			public const EventKeywords AsyncCausalitySynchronousWork = (EventKeywords)32L;

			// Token: 0x04000C49 RID: 3145
			public const EventKeywords TaskStops = (EventKeywords)64L;

			// Token: 0x04000C4A RID: 3146
			public const EventKeywords TasksFlowActivityIds = (EventKeywords)128L;

			// Token: 0x04000C4B RID: 3147
			public const EventKeywords AsyncMethod = (EventKeywords)256L;

			// Token: 0x04000C4C RID: 3148
			public const EventKeywords TasksSetActivityIds = (EventKeywords)65536L;

			// Token: 0x04000C4D RID: 3149
			public const EventKeywords Debug = (EventKeywords)131072L;

			// Token: 0x04000C4E RID: 3150
			public const EventKeywords DebugActivityId = (EventKeywords)262144L;
		}
	}
}
