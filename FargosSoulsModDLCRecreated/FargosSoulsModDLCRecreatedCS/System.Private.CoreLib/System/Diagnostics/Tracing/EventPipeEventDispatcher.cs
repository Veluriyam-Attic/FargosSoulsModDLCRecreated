using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000719 RID: 1817
	internal sealed class EventPipeEventDispatcher
	{
		// Token: 0x06005A27 RID: 23079 RVA: 0x001B3D9C File Offset: 0x001B2F9C
		private EventPipeEventDispatcher()
		{
			this.m_RuntimeProviderID = EventPipeInternal.GetProvider("Microsoft-Windows-DotNETRuntime");
			this.m_dispatchTaskWaitHandle.SafeWaitHandle = new SafeWaitHandle(IntPtr.Zero, false);
		}

		// Token: 0x06005A28 RID: 23080 RVA: 0x001B3DF8 File Offset: 0x001B2FF8
		internal void SendCommand(EventListener eventListener, EventCommand command, bool enable, EventLevel level, EventKeywords matchAnyKeywords)
		{
			if (command == EventCommand.Update && enable)
			{
				object dispatchControlLock = this.m_dispatchControlLock;
				lock (dispatchControlLock)
				{
					this.m_subscriptions[eventListener] = new EventPipeEventDispatcher.EventListenerSubscription(matchAnyKeywords, level);
					this.CommitDispatchConfiguration();
					return;
				}
			}
			if (command == EventCommand.Update && !enable)
			{
				this.RemoveEventListener(eventListener);
			}
		}

		// Token: 0x06005A29 RID: 23081 RVA: 0x001B3E64 File Offset: 0x001B3064
		internal void RemoveEventListener(EventListener listener)
		{
			object dispatchControlLock = this.m_dispatchControlLock;
			lock (dispatchControlLock)
			{
				if (this.m_subscriptions.ContainsKey(listener))
				{
					this.m_subscriptions.Remove(listener);
				}
				this.CommitDispatchConfiguration();
			}
		}

		// Token: 0x06005A2A RID: 23082 RVA: 0x001B3EC0 File Offset: 0x001B30C0
		private unsafe void CommitDispatchConfiguration()
		{
			this.StopDispatchTask();
			EventPipeInternal.Disable(this.m_sessionID);
			if (this.m_subscriptions.Count <= 0)
			{
				return;
			}
			EventKeywords eventKeywords = EventKeywords.None;
			EventLevel eventLevel = EventLevel.LogAlways;
			foreach (EventPipeEventDispatcher.EventListenerSubscription eventListenerSubscription in this.m_subscriptions.Values)
			{
				eventKeywords |= eventListenerSubscription.MatchAnyKeywords;
				eventLevel = ((eventListenerSubscription.Level > eventLevel) ? eventListenerSubscription.Level : eventLevel);
			}
			EventPipeProviderConfiguration[] providers = new EventPipeProviderConfiguration[]
			{
				new EventPipeProviderConfiguration("Microsoft-Windows-DotNETRuntime", (ulong)eventKeywords, (uint)eventLevel, null)
			};
			this.m_sessionID = EventPipeInternal.Enable(null, EventPipeSerializationFormat.NetTrace, 10U, providers);
			EventPipeSessionInfo eventPipeSessionInfo;
			EventPipeInternal.GetSessionInfo(this.m_sessionID, &eventPipeSessionInfo);
			this.m_syncTimeUtc = DateTime.FromFileTimeUtc(eventPipeSessionInfo.StartTimeAsUTCFileTime);
			this.m_syncTimeQPC = eventPipeSessionInfo.StartTimeStamp;
			this.m_timeQPCFrequency = eventPipeSessionInfo.TimeStampFrequency;
			this.StartDispatchTask();
		}

		// Token: 0x06005A2B RID: 23083 RVA: 0x001B3FC0 File Offset: 0x001B31C0
		private void StartDispatchTask()
		{
			if (this.m_dispatchTask == null)
			{
				this.m_stopDispatchTask = false;
				this.m_dispatchTaskWaitHandle.SafeWaitHandle = new SafeWaitHandle(EventPipeInternal.GetWaitHandle(this.m_sessionID), false);
				this.m_dispatchTask = Task.Factory.StartNew(new Action(this.DispatchEventsToEventListeners), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
			}
		}

		// Token: 0x06005A2C RID: 23084 RVA: 0x001B401F File Offset: 0x001B321F
		private void StopDispatchTask()
		{
			if (this.m_dispatchTask != null)
			{
				this.m_stopDispatchTask = true;
				EventWaitHandle.Set(this.m_dispatchTaskWaitHandle.SafeWaitHandle);
				this.m_dispatchTask.Wait();
				this.m_dispatchTask = null;
			}
		}

		// Token: 0x06005A2D RID: 23085 RVA: 0x001B4054 File Offset: 0x001B3254
		private unsafe void DispatchEventsToEventListeners()
		{
			while (!this.m_stopDispatchTask)
			{
				bool flag = false;
				EventPipeEventInstanceData eventPipeEventInstanceData;
				while (!this.m_stopDispatchTask && EventPipeInternal.GetNextEvent(this.m_sessionID, &eventPipeEventInstanceData))
				{
					flag = true;
					if (eventPipeEventInstanceData.ProviderID == this.m_RuntimeProviderID)
					{
						ReadOnlySpan<byte> payload = new ReadOnlySpan<byte>((void*)eventPipeEventInstanceData.Payload, (int)eventPipeEventInstanceData.PayloadLength);
						DateTime timeStamp = this.TimeStampToDateTime(eventPipeEventInstanceData.TimeStamp);
						NativeRuntimeEventSource.Log.ProcessEvent(eventPipeEventInstanceData.EventID, eventPipeEventInstanceData.ThreadID, timeStamp, eventPipeEventInstanceData.ActivityId, eventPipeEventInstanceData.ChildActivityId, payload);
					}
				}
				if (!this.m_stopDispatchTask)
				{
					if (!flag)
					{
						this.m_dispatchTaskWaitHandle.WaitOne();
					}
					Thread.Sleep(10);
				}
			}
		}

		// Token: 0x06005A2E RID: 23086 RVA: 0x001B410C File Offset: 0x001B330C
		private DateTime TimeStampToDateTime(long timeStamp)
		{
			if (timeStamp == 9223372036854775807L)
			{
				return DateTime.MaxValue;
			}
			long num = (long)((double)(timeStamp - this.m_syncTimeQPC) * 10000000.0 / (double)this.m_timeQPCFrequency) + this.m_syncTimeUtc.Ticks;
			if (num < 0L || 3155378975999999999L < num)
			{
				num = 3155378975999999999L;
			}
			return new DateTime(num, DateTimeKind.Utc);
		}

		// Token: 0x04001A4B RID: 6731
		internal static readonly EventPipeEventDispatcher Instance = new EventPipeEventDispatcher();

		// Token: 0x04001A4C RID: 6732
		private readonly IntPtr m_RuntimeProviderID;

		// Token: 0x04001A4D RID: 6733
		private ulong m_sessionID;

		// Token: 0x04001A4E RID: 6734
		private DateTime m_syncTimeUtc;

		// Token: 0x04001A4F RID: 6735
		private long m_syncTimeQPC;

		// Token: 0x04001A50 RID: 6736
		private long m_timeQPCFrequency;

		// Token: 0x04001A51 RID: 6737
		private bool m_stopDispatchTask;

		// Token: 0x04001A52 RID: 6738
		private readonly EventPipeWaitHandle m_dispatchTaskWaitHandle = new EventPipeWaitHandle();

		// Token: 0x04001A53 RID: 6739
		private Task m_dispatchTask;

		// Token: 0x04001A54 RID: 6740
		private readonly object m_dispatchControlLock = new object();

		// Token: 0x04001A55 RID: 6741
		private readonly Dictionary<EventListener, EventPipeEventDispatcher.EventListenerSubscription> m_subscriptions = new Dictionary<EventListener, EventPipeEventDispatcher.EventListenerSubscription>();

		// Token: 0x04001A56 RID: 6742
		private const uint DefaultEventListenerCircularMBSize = 10U;

		// Token: 0x0200071A RID: 1818
		internal sealed class EventListenerSubscription
		{
			// Token: 0x17000ED7 RID: 3799
			// (get) Token: 0x06005A30 RID: 23088 RVA: 0x001B4182 File Offset: 0x001B3382
			// (set) Token: 0x06005A31 RID: 23089 RVA: 0x001B418A File Offset: 0x001B338A
			internal EventKeywords MatchAnyKeywords { get; private set; }

			// Token: 0x17000ED8 RID: 3800
			// (get) Token: 0x06005A32 RID: 23090 RVA: 0x001B4193 File Offset: 0x001B3393
			// (set) Token: 0x06005A33 RID: 23091 RVA: 0x001B419B File Offset: 0x001B339B
			internal EventLevel Level { get; private set; }

			// Token: 0x06005A34 RID: 23092 RVA: 0x001B41A4 File Offset: 0x001B33A4
			internal EventListenerSubscription(EventKeywords matchAnyKeywords, EventLevel level)
			{
				this.MatchAnyKeywords = matchAnyKeywords;
				this.Level = level;
			}
		}
	}
}
