using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000734 RID: 1844
	[NullableContext(2)]
	[Nullable(0)]
	public class EventWrittenEventArgs : EventArgs
	{
		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06005B1D RID: 23325 RVA: 0x001BC1F1 File Offset: 0x001BB3F1
		// (set) Token: 0x06005B1E RID: 23326 RVA: 0x001BC228 File Offset: 0x001BB428
		public string EventName
		{
			get
			{
				if (this.m_eventName != null || this.EventId < 0)
				{
					return this.m_eventName;
				}
				return this.m_eventSource.m_eventData[this.EventId].Name;
			}
			internal set
			{
				this.m_eventName = value;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06005B1F RID: 23327 RVA: 0x001BC231 File Offset: 0x001BB431
		// (set) Token: 0x06005B20 RID: 23328 RVA: 0x001BC239 File Offset: 0x001BB439
		public int EventId { get; internal set; }

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06005B21 RID: 23329 RVA: 0x001BC244 File Offset: 0x001BB444
		// (set) Token: 0x06005B22 RID: 23330 RVA: 0x001BC26C File Offset: 0x001BB46C
		public Guid ActivityId
		{
			get
			{
				Guid guid = this.m_activityId;
				if (guid == Guid.Empty)
				{
					guid = EventSource.CurrentThreadActivityId;
				}
				return guid;
			}
			internal set
			{
				this.m_activityId = value;
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06005B23 RID: 23331 RVA: 0x001BC275 File Offset: 0x001BB475
		// (set) Token: 0x06005B24 RID: 23332 RVA: 0x001BC27D File Offset: 0x001BB47D
		public Guid RelatedActivityId { get; internal set; }

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06005B25 RID: 23333 RVA: 0x001BC286 File Offset: 0x001BB486
		// (set) Token: 0x06005B26 RID: 23334 RVA: 0x001BC28E File Offset: 0x001BB48E
		public ReadOnlyCollection<object> Payload { get; internal set; }

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06005B27 RID: 23335 RVA: 0x001BC298 File Offset: 0x001BB498
		// (set) Token: 0x06005B28 RID: 23336 RVA: 0x001BC30A File Offset: 0x001BB50A
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public ReadOnlyCollection<string> PayloadNames
		{
			[return: Nullable(new byte[]
			{
				2,
				1
			})]
			get
			{
				if (this.EventId >= 0 && this.m_payloadNames == null)
				{
					List<string> list = new List<string>();
					foreach (ParameterInfo parameterInfo in this.m_eventSource.m_eventData[this.EventId].Parameters)
					{
						list.Add(parameterInfo.Name);
					}
					this.m_payloadNames = new ReadOnlyCollection<string>(list);
				}
				return this.m_payloadNames;
			}
			internal set
			{
				this.m_payloadNames = value;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06005B29 RID: 23337 RVA: 0x001BC313 File Offset: 0x001BB513
		[Nullable(1)]
		public EventSource EventSource
		{
			[NullableContext(1)]
			get
			{
				return this.m_eventSource;
			}
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06005B2A RID: 23338 RVA: 0x001BC31B File Offset: 0x001BB51B
		public EventKeywords Keywords
		{
			get
			{
				if (this.EventId <= 0)
				{
					return this.m_keywords;
				}
				return (EventKeywords)this.m_eventSource.m_eventData[this.EventId].Descriptor.Keywords;
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06005B2B RID: 23339 RVA: 0x001BC34F File Offset: 0x001BB54F
		public EventOpcode Opcode
		{
			get
			{
				if (this.EventId <= 0)
				{
					return this.m_opcode;
				}
				return (EventOpcode)this.m_eventSource.m_eventData[this.EventId].Descriptor.Opcode;
			}
		}

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06005B2C RID: 23340 RVA: 0x001BC383 File Offset: 0x001BB583
		public EventTask Task
		{
			get
			{
				if (this.EventId <= 0)
				{
					return EventTask.None;
				}
				return (EventTask)this.m_eventSource.m_eventData[this.EventId].Descriptor.Task;
			}
		}

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06005B2D RID: 23341 RVA: 0x001BC3B2 File Offset: 0x001BB5B2
		public EventTags Tags
		{
			get
			{
				if (this.EventId <= 0)
				{
					return this.m_tags;
				}
				return this.m_eventSource.m_eventData[this.EventId].Tags;
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06005B2E RID: 23342 RVA: 0x001BC3E1 File Offset: 0x001BB5E1
		// (set) Token: 0x06005B2F RID: 23343 RVA: 0x001BC410 File Offset: 0x001BB610
		public string Message
		{
			get
			{
				if (this.EventId <= 0)
				{
					return this.m_message;
				}
				return this.m_eventSource.m_eventData[this.EventId].Message;
			}
			internal set
			{
				this.m_message = value;
			}
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06005B30 RID: 23344 RVA: 0x001BC419 File Offset: 0x001BB619
		public EventChannel Channel
		{
			get
			{
				if (this.EventId <= 0)
				{
					return EventChannel.None;
				}
				return (EventChannel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Channel;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06005B31 RID: 23345 RVA: 0x001BC448 File Offset: 0x001BB648
		public byte Version
		{
			get
			{
				if (this.EventId <= 0)
				{
					return 0;
				}
				return this.m_eventSource.m_eventData[this.EventId].Descriptor.Version;
			}
		}

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x06005B32 RID: 23346 RVA: 0x001BC477 File Offset: 0x001BB677
		public EventLevel Level
		{
			get
			{
				if (this.EventId <= 0)
				{
					return this.m_level;
				}
				return (EventLevel)this.m_eventSource.m_eventData[this.EventId].Descriptor.Level;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x06005B33 RID: 23347 RVA: 0x001BC4AB File Offset: 0x001BB6AB
		// (set) Token: 0x06005B34 RID: 23348 RVA: 0x001BC4D5 File Offset: 0x001BB6D5
		public long OSThreadId
		{
			get
			{
				if (this.m_osThreadId == null)
				{
					this.m_osThreadId = new long?((long)Thread.CurrentOSThreadId);
				}
				return this.m_osThreadId.Value;
			}
			internal set
			{
				this.m_osThreadId = new long?(value);
			}
		}

		// Token: 0x17000EFB RID: 3835
		// (get) Token: 0x06005B35 RID: 23349 RVA: 0x001BC4E3 File Offset: 0x001BB6E3
		// (set) Token: 0x06005B36 RID: 23350 RVA: 0x001BC4EB File Offset: 0x001BB6EB
		public DateTime TimeStamp { get; internal set; }

		// Token: 0x06005B37 RID: 23351 RVA: 0x001BC4F4 File Offset: 0x001BB6F4
		internal EventWrittenEventArgs(EventSource eventSource)
		{
			this.m_eventSource = eventSource;
			this.TimeStamp = DateTime.UtcNow;
		}

		// Token: 0x04001AD5 RID: 6869
		private string m_message;

		// Token: 0x04001AD6 RID: 6870
		private string m_eventName;

		// Token: 0x04001AD7 RID: 6871
		private readonly EventSource m_eventSource;

		// Token: 0x04001AD8 RID: 6872
		private ReadOnlyCollection<string> m_payloadNames;

		// Token: 0x04001AD9 RID: 6873
		private Guid m_activityId;

		// Token: 0x04001ADA RID: 6874
		private long? m_osThreadId;

		// Token: 0x04001ADB RID: 6875
		internal EventTags m_tags;

		// Token: 0x04001ADC RID: 6876
		internal EventOpcode m_opcode;

		// Token: 0x04001ADD RID: 6877
		internal EventLevel m_level;

		// Token: 0x04001ADE RID: 6878
		internal EventKeywords m_keywords;
	}
}
