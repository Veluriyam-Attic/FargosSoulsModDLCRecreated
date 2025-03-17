using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000730 RID: 1840
	[NullableContext(1)]
	[Nullable(0)]
	public class EventListener : IDisposable
	{
		// Token: 0x14000020 RID: 32
		// (add) Token: 0x06005AFD RID: 23293 RVA: 0x001BBB24 File Offset: 0x001BAD24
		// (remove) Token: 0x06005AFE RID: 23294 RVA: 0x001BBB45 File Offset: 0x001BAD45
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public event EventHandler<EventSourceCreatedEventArgs> EventSourceCreated
		{
			add
			{
				this.CallBackForExistingEventSources(false, value);
				this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Combine(this._EventSourceCreated, value);
			}
			remove
			{
				this._EventSourceCreated = (EventHandler<EventSourceCreatedEventArgs>)Delegate.Remove(this._EventSourceCreated, value);
			}
		}

		// Token: 0x14000021 RID: 33
		// (add) Token: 0x06005AFF RID: 23295 RVA: 0x001BBB60 File Offset: 0x001BAD60
		// (remove) Token: 0x06005B00 RID: 23296 RVA: 0x001BBB98 File Offset: 0x001BAD98
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public event EventHandler<EventWrittenEventArgs> EventWritten;

		// Token: 0x06005B01 RID: 23297 RVA: 0x001BBBCD File Offset: 0x001BADCD
		static EventListener()
		{
			GC.KeepAlive(NativeRuntimeEventSource.Log);
		}

		// Token: 0x06005B02 RID: 23298 RVA: 0x001BBBD9 File Offset: 0x001BADD9
		public EventListener()
		{
			this.CallBackForExistingEventSources(true, delegate(object obj, EventSourceCreatedEventArgs args)
			{
				args.EventSource.AddListener((EventListener)obj);
			});
		}

		// Token: 0x06005B03 RID: 23299 RVA: 0x001BBC08 File Offset: 0x001BAE08
		public virtual void Dispose()
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_Listeners != null)
				{
					if (this == EventListener.s_Listeners)
					{
						EventListener listenerToRemove = EventListener.s_Listeners;
						EventListener.s_Listeners = this.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(listenerToRemove);
					}
					else
					{
						EventListener eventListener = EventListener.s_Listeners;
						EventListener next;
						for (;;)
						{
							next = eventListener.m_Next;
							if (next == null)
							{
								break;
							}
							if (next == this)
							{
								goto Block_6;
							}
							eventListener = next;
						}
						return;
						Block_6:
						eventListener.m_Next = next.m_Next;
						EventListener.RemoveReferencesToListenerInEventSources(next);
					}
				}
			}
		}

		// Token: 0x06005B04 RID: 23300 RVA: 0x001BBCA8 File Offset: 0x001BAEA8
		public void EnableEvents(EventSource eventSource, EventLevel level)
		{
			this.EnableEvents(eventSource, level, EventKeywords.None);
		}

		// Token: 0x06005B05 RID: 23301 RVA: 0x001BBCB4 File Offset: 0x001BAEB4
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword)
		{
			this.EnableEvents(eventSource, level, matchAnyKeyword, null);
		}

		// Token: 0x06005B06 RID: 23302 RVA: 0x001BBCC0 File Offset: 0x001BAEC0
		public void EnableEvents(EventSource eventSource, EventLevel level, EventKeywords matchAnyKeyword, [Nullable(new byte[]
		{
			2,
			1,
			2
		})] IDictionary<string, string> arguments)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, EventProviderType.None, 0, 0, EventCommand.Update, true, level, matchAnyKeyword, arguments);
			if (eventSource.GetType() == typeof(NativeRuntimeEventSource))
			{
				EventPipeEventDispatcher.Instance.SendCommand(this, EventCommand.Update, true, level, matchAnyKeyword);
			}
		}

		// Token: 0x06005B07 RID: 23303 RVA: 0x001BBD14 File Offset: 0x001BAF14
		public void DisableEvents(EventSource eventSource)
		{
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			eventSource.SendCommand(this, EventProviderType.None, 0, 0, EventCommand.Update, false, EventLevel.LogAlways, EventKeywords.None, null);
			if (eventSource.GetType() == typeof(NativeRuntimeEventSource))
			{
				EventPipeEventDispatcher.Instance.SendCommand(this, EventCommand.Update, false, EventLevel.LogAlways, EventKeywords.None);
			}
		}

		// Token: 0x06005B08 RID: 23304 RVA: 0x001BBD66 File Offset: 0x001BAF66
		public static int EventSourceIndex(EventSource eventSource)
		{
			return eventSource.m_id;
		}

		// Token: 0x06005B09 RID: 23305 RVA: 0x001BBD70 File Offset: 0x001BAF70
		protected internal virtual void OnEventSourceCreated(EventSource eventSource)
		{
			EventHandler<EventSourceCreatedEventArgs> eventSourceCreated = this._EventSourceCreated;
			if (eventSourceCreated != null)
			{
				eventSourceCreated(this, new EventSourceCreatedEventArgs
				{
					EventSource = eventSource
				});
			}
		}

		// Token: 0x06005B0A RID: 23306 RVA: 0x001BBD9C File Offset: 0x001BAF9C
		protected internal virtual void OnEventWritten(EventWrittenEventArgs eventData)
		{
			EventHandler<EventWrittenEventArgs> eventWritten = this.EventWritten;
			if (eventWritten == null)
			{
				return;
			}
			eventWritten(this, eventData);
		}

		// Token: 0x06005B0B RID: 23307 RVA: 0x001BBDB0 File Offset: 0x001BAFB0
		internal static void AddEventSource(EventSource newEventSource)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_EventSources == null)
				{
					EventListener.s_EventSources = new List<WeakReference<EventSource>>(2);
				}
				if (!EventListener.s_EventSourceShutdownRegistered)
				{
					EventListener.s_EventSourceShutdownRegistered = true;
					AppContext.ProcessExit += EventListener.DisposeOnShutdown;
				}
				int num = -1;
				if (EventListener.s_EventSources.Count % 64 == 63)
				{
					int num2 = EventListener.s_EventSources.Count;
					while (0 < num2)
					{
						num2--;
						WeakReference<EventSource> weakReference = EventListener.s_EventSources[num2];
						EventSource eventSource;
						if (!weakReference.TryGetTarget(out eventSource))
						{
							num = num2;
							weakReference.SetTarget(newEventSource);
							break;
						}
					}
				}
				if (num < 0)
				{
					num = EventListener.s_EventSources.Count;
					EventListener.s_EventSources.Add(new WeakReference<EventSource>(newEventSource));
				}
				newEventSource.m_id = num;
				for (EventListener next = EventListener.s_Listeners; next != null; next = next.m_Next)
				{
					newEventSource.AddListener(next);
				}
			}
		}

		// Token: 0x06005B0C RID: 23308 RVA: 0x001BBEAC File Offset: 0x001BB0AC
		private static void DisposeOnShutdown(object sender, EventArgs e)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				foreach (WeakReference<EventSource> weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource;
					if (weakReference.TryGetTarget(out eventSource))
					{
						eventSource.Dispose();
					}
				}
			}
		}

		// Token: 0x06005B0D RID: 23309 RVA: 0x001BBF30 File Offset: 0x001BB130
		private static void RemoveReferencesToListenerInEventSources(EventListener listenerToRemove)
		{
			using (List<WeakReference<EventSource>>.Enumerator enumerator = EventListener.s_EventSources.GetEnumerator())
			{
				IL_79:
				while (enumerator.MoveNext())
				{
					WeakReference<EventSource> weakReference = enumerator.Current;
					EventSource eventSource;
					if (weakReference.TryGetTarget(out eventSource))
					{
						if (eventSource.m_Dispatchers.m_Listener == listenerToRemove)
						{
							eventSource.m_Dispatchers = eventSource.m_Dispatchers.m_Next;
						}
						else
						{
							EventDispatcher eventDispatcher = eventSource.m_Dispatchers;
							EventDispatcher next;
							for (;;)
							{
								next = eventDispatcher.m_Next;
								if (next == null)
								{
									goto IL_79;
								}
								if (next.m_Listener == listenerToRemove)
								{
									break;
								}
								eventDispatcher = next;
							}
							eventDispatcher.m_Next = next.m_Next;
						}
					}
				}
			}
			EventPipeEventDispatcher.Instance.RemoveEventListener(listenerToRemove);
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06005B0E RID: 23310 RVA: 0x001BBFEC File Offset: 0x001BB1EC
		internal static object EventListenersLock
		{
			get
			{
				if (EventListener.s_EventSources == null)
				{
					Interlocked.CompareExchange<List<WeakReference<EventSource>>>(ref EventListener.s_EventSources, new List<WeakReference<EventSource>>(2), null);
				}
				return EventListener.s_EventSources;
			}
		}

		// Token: 0x06005B0F RID: 23311 RVA: 0x001BC00C File Offset: 0x001BB20C
		private void CallBackForExistingEventSources(bool addToListenersList, EventHandler<EventSourceCreatedEventArgs> callback)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (EventListener.s_CreatingListener)
				{
					throw new InvalidOperationException(SR.EventSource_ListenerCreatedInsideCallback);
				}
				try
				{
					EventListener.s_CreatingListener = true;
					if (addToListenersList)
					{
						this.m_Next = EventListener.s_Listeners;
						EventListener.s_Listeners = this;
					}
					if (callback != null)
					{
						foreach (WeakReference<EventSource> weakReference in EventListener.s_EventSources.ToArray())
						{
							EventSource eventSource;
							if (weakReference.TryGetTarget(out eventSource))
							{
								callback(this, new EventSourceCreatedEventArgs
								{
									EventSource = eventSource
								});
							}
						}
					}
				}
				finally
				{
					EventListener.s_CreatingListener = false;
				}
			}
		}

		// Token: 0x04001ABB RID: 6843
		[CompilerGenerated]
		private EventHandler<EventSourceCreatedEventArgs> _EventSourceCreated;

		// Token: 0x04001ABD RID: 6845
		internal volatile EventListener m_Next;

		// Token: 0x04001ABE RID: 6846
		internal static EventListener s_Listeners;

		// Token: 0x04001ABF RID: 6847
		internal static List<WeakReference<EventSource>> s_EventSources;

		// Token: 0x04001AC0 RID: 6848
		private static bool s_CreatingListener;

		// Token: 0x04001AC1 RID: 6849
		private static bool s_EventSourceShutdownRegistered;
	}
}
