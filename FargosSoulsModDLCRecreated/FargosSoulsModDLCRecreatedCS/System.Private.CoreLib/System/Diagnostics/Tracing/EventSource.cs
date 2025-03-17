using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Numerics;
using System.Reflection;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200072A RID: 1834
	[NullableContext(2)]
	[Nullable(0)]
	public class EventSource : IDisposable
	{
		// Token: 0x17000EDB RID: 3803
		// (get) Token: 0x06005A7C RID: 23164 RVA: 0x001B651F File Offset: 0x001B571F
		private static bool IsSupported { get; } = EventSource.InitializeIsSupported();

		// Token: 0x06005A7D RID: 23165 RVA: 0x001B6528 File Offset: 0x001B5728
		private static bool InitializeIsSupported()
		{
			bool flag;
			return !AppContext.TryGetSwitch("System.Diagnostics.Tracing.EventSource.IsSupported", out flag) || flag;
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06005A7E RID: 23166 RVA: 0x001B6546 File Offset: 0x001B5746
		[Nullable(1)]
		public string Name
		{
			[NullableContext(1)]
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06005A7F RID: 23167 RVA: 0x001B654E File Offset: 0x001B574E
		public Guid Guid
		{
			get
			{
				return this.m_guid;
			}
		}

		// Token: 0x06005A80 RID: 23168 RVA: 0x001B6556 File Offset: 0x001B5756
		public bool IsEnabled()
		{
			return this.m_eventSourceEnabled;
		}

		// Token: 0x06005A81 RID: 23169 RVA: 0x001B655E File Offset: 0x001B575E
		public bool IsEnabled(EventLevel level, EventKeywords keywords)
		{
			return this.IsEnabled(level, keywords, EventChannel.None);
		}

		// Token: 0x06005A82 RID: 23170 RVA: 0x001B6569 File Offset: 0x001B5769
		public bool IsEnabled(EventLevel level, EventKeywords keywords, EventChannel channel)
		{
			return this.IsEnabled() && this.IsEnabledCommon(this.m_eventSourceEnabled, this.m_level, this.m_matchAnyKeyword, level, keywords, channel);
		}

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06005A83 RID: 23171 RVA: 0x001B6595 File Offset: 0x001B5795
		public EventSourceSettings Settings
		{
			get
			{
				return this.m_config;
			}
		}

		// Token: 0x06005A84 RID: 23172 RVA: 0x001B65A0 File Offset: 0x001B57A0
		[NullableContext(1)]
		public static Guid GetGuid(Type eventSourceType)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), EventManifestOptions.None);
			string name = eventSourceType.Name;
			if (eventSourceAttribute != null)
			{
				Guid result;
				if (eventSourceAttribute.Guid != null && Guid.TryParse(eventSourceAttribute.Guid, out result))
				{
					return result;
				}
				if (eventSourceAttribute.Name != null)
				{
					name = eventSourceAttribute.Name;
				}
			}
			if (name == null)
			{
				throw new ArgumentException(SR.Argument_InvalidTypeName, "eventSourceType");
			}
			return EventSource.GenerateGuidFromName(name.ToUpperInvariant());
		}

		// Token: 0x06005A85 RID: 23173 RVA: 0x001B6628 File Offset: 0x001B5828
		[NullableContext(1)]
		public static string GetName(Type eventSourceType)
		{
			return EventSource.GetName(eventSourceType, EventManifestOptions.None);
		}

		// Token: 0x06005A86 RID: 23174 RVA: 0x001B6631 File Offset: 0x001B5831
		public static string GenerateManifest([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods | DynamicallyAccessedMemberTypes.PublicNestedTypes)] [Nullable(1)] Type eventSourceType, string assemblyPathToIncludeInManifest)
		{
			return EventSource.GenerateManifest(eventSourceType, assemblyPathToIncludeInManifest, EventManifestOptions.None);
		}

		// Token: 0x06005A87 RID: 23175 RVA: 0x001B663C File Offset: 0x001B583C
		public static string GenerateManifest([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods | DynamicallyAccessedMemberTypes.PublicNestedTypes)] [Nullable(1)] Type eventSourceType, string assemblyPathToIncludeInManifest, EventManifestOptions flags)
		{
			if (!EventSource.IsSupported)
			{
				return null;
			}
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			byte[] array = EventSource.CreateManifestAndDescriptors(eventSourceType, assemblyPathToIncludeInManifest, null, flags);
			if (array != null)
			{
				return Encoding.UTF8.GetString(array, 0, array.Length);
			}
			return null;
		}

		// Token: 0x06005A88 RID: 23176 RVA: 0x001B6684 File Offset: 0x001B5884
		[NullableContext(1)]
		public static IEnumerable<EventSource> GetSources()
		{
			if (!EventSource.IsSupported)
			{
				return Array.Empty<EventSource>();
			}
			List<EventSource> list = new List<EventSource>();
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				foreach (WeakReference<EventSource> weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource;
					if (weakReference.TryGetTarget(out eventSource) && !eventSource.IsDisposed)
					{
						list.Add(eventSource);
					}
				}
			}
			return list;
		}

		// Token: 0x06005A89 RID: 23177 RVA: 0x001B6728 File Offset: 0x001B5928
		[NullableContext(1)]
		public static void SendCommand(EventSource eventSource, EventCommand command, [Nullable(new byte[]
		{
			2,
			1,
			2
		})] IDictionary<string, string> commandArguments)
		{
			if (!EventSource.IsSupported)
			{
				return;
			}
			if (eventSource == null)
			{
				throw new ArgumentNullException("eventSource");
			}
			if (command <= EventCommand.Update && command != EventCommand.SendManifest)
			{
				throw new ArgumentException(SR.EventSource_InvalidCommand, "command");
			}
			eventSource.SendCommand(null, EventProviderType.ETW, 0, 0, command, true, EventLevel.LogAlways, EventKeywords.None, commandArguments);
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06005A8A RID: 23178 RVA: 0x001B6773 File Offset: 0x001B5973
		public Exception ConstructionException
		{
			get
			{
				return this.m_constructionException;
			}
		}

		// Token: 0x06005A8B RID: 23179 RVA: 0x001B677C File Offset: 0x001B597C
		[NullableContext(1)]
		[return: Nullable(2)]
		public string GetTrait(string key)
		{
			if (this.m_traits != null)
			{
				for (int i = 0; i < this.m_traits.Length - 1; i += 2)
				{
					if (this.m_traits[i] == key)
					{
						return this.m_traits[i + 1];
					}
				}
			}
			return null;
		}

		// Token: 0x06005A8C RID: 23180 RVA: 0x001B67C2 File Offset: 0x001B59C2
		[NullableContext(1)]
		public override string ToString()
		{
			return SR.Format(SR.EventSource_ToString, this.Name, this.Guid);
		}

		// Token: 0x1400001F RID: 31
		// (add) Token: 0x06005A8D RID: 23181 RVA: 0x001B67E0 File Offset: 0x001B59E0
		// (remove) Token: 0x06005A8E RID: 23182 RVA: 0x001B6823 File Offset: 0x001B5A23
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public event EventHandler<EventCommandEventArgs> EventCommandExecuted
		{
			add
			{
				if (value == null)
				{
					return;
				}
				this.m_eventCommandExecuted = (EventHandler<EventCommandEventArgs>)Delegate.Combine(this.m_eventCommandExecuted, value);
				for (EventCommandEventArgs eventCommandEventArgs = this.m_deferredCommands; eventCommandEventArgs != null; eventCommandEventArgs = eventCommandEventArgs.nextCommand)
				{
					value(this, eventCommandEventArgs);
				}
			}
			remove
			{
				this.m_eventCommandExecuted = (EventHandler<EventCommandEventArgs>)Delegate.Remove(this.m_eventCommandExecuted, value);
			}
		}

		// Token: 0x06005A8F RID: 23183 RVA: 0x001B683C File Offset: 0x001B5A3C
		public static void SetCurrentThreadActivityId(Guid activityId)
		{
			if (!EventSource.IsSupported)
			{
				return;
			}
			if (TplEventSource.Log != null)
			{
				TplEventSource.Log.SetActivityId(activityId);
			}
			EventPipeInternal.EventActivityIdControl(2U, ref activityId);
			Interop.Advapi32.EventActivityIdControl(Interop.Advapi32.ActivityControl.EVENT_ACTIVITY_CTRL_SET_ID, ref activityId);
		}

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06005A90 RID: 23184 RVA: 0x001B686C File Offset: 0x001B5A6C
		public static Guid CurrentThreadActivityId
		{
			get
			{
				if (!EventSource.IsSupported)
				{
					return default(Guid);
				}
				Guid result = default(Guid);
				Interop.Advapi32.EventActivityIdControl(Interop.Advapi32.ActivityControl.EVENT_ACTIVITY_CTRL_GET_ID, ref result);
				return result;
			}
		}

		// Token: 0x06005A91 RID: 23185 RVA: 0x001B689C File Offset: 0x001B5A9C
		public static void SetCurrentThreadActivityId(Guid activityId, out Guid oldActivityThatWillContinue)
		{
			if (!EventSource.IsSupported)
			{
				oldActivityThatWillContinue = default(Guid);
				return;
			}
			oldActivityThatWillContinue = activityId;
			EventPipeInternal.EventActivityIdControl(2U, ref oldActivityThatWillContinue);
			Interop.Advapi32.EventActivityIdControl(Interop.Advapi32.ActivityControl.EVENT_ACTIVITY_CTRL_GET_SET_ID, ref oldActivityThatWillContinue);
			if (TplEventSource.Log != null)
			{
				TplEventSource.Log.SetActivityId(activityId);
			}
		}

		// Token: 0x06005A92 RID: 23186 RVA: 0x001B68D6 File Offset: 0x001B5AD6
		protected EventSource() : this(EventSourceSettings.EtwManifestEventFormat)
		{
		}

		// Token: 0x06005A93 RID: 23187 RVA: 0x001B68DF File Offset: 0x001B5ADF
		protected EventSource(bool throwOnEventWriteErrors) : this(EventSourceSettings.EtwManifestEventFormat | (throwOnEventWriteErrors ? EventSourceSettings.ThrowOnEventWriteErrors : EventSourceSettings.Default))
		{
		}

		// Token: 0x06005A94 RID: 23188 RVA: 0x001B68F0 File Offset: 0x001B5AF0
		protected EventSource(EventSourceSettings settings) : this(settings, null)
		{
		}

		// Token: 0x06005A95 RID: 23189 RVA: 0x001B68FC File Offset: 0x001B5AFC
		protected EventSource(EventSourceSettings settings, [Nullable(new byte[]
		{
			2,
			1
		})] params string[] traits)
		{
			this.m_createEventLock = new object();
			this.m_writeEventStringEventHandle = IntPtr.Zero;
			base..ctor();
			if (EventSource.IsSupported)
			{
				this.m_eventHandleTable = new TraceLoggingEventHandleTable();
				this.m_config = this.ValidateSettings(settings);
				Type type = base.GetType();
				Guid guid = EventSource.GetGuid(type);
				string name = EventSource.GetName(type);
				this.Initialize(guid, name, traits);
			}
		}

		// Token: 0x06005A96 RID: 23190 RVA: 0x001B6964 File Offset: 0x001B5B64
		private unsafe void DefineEventPipeEvents()
		{
			if (this.SelfDescribingEvents)
			{
				return;
			}
			int num = this.m_eventData.Length;
			for (int i = 0; i < num; i++)
			{
				uint eventId = (uint)this.m_eventData[i].Descriptor.EventId;
				if (eventId != 0U)
				{
					byte[] array = EventPipeMetadataGenerator.Instance.GenerateEventMetadata(this.m_eventData[i]);
					uint metadataLength = (uint)((array != null) ? array.Length : 0);
					string name = this.m_eventData[i].Name;
					long keywords = this.m_eventData[i].Descriptor.Keywords;
					uint version = (uint)this.m_eventData[i].Descriptor.Version;
					uint level = (uint)this.m_eventData[i].Descriptor.Level;
					byte[] array2;
					byte* pMetadata;
					if ((array2 = array) == null || array2.Length == 0)
					{
						pMetadata = null;
					}
					else
					{
						pMetadata = &array2[0];
					}
					IntPtr eventHandle = this.m_eventPipeProvider.m_eventProvider.DefineEventHandle(eventId, name, keywords, version, level, pMetadata, metadataLength);
					this.m_eventData[i].EventHandle = eventHandle;
					array2 = null;
				}
			}
		}

		// Token: 0x06005A97 RID: 23191 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NullableContext(1)]
		protected virtual void OnEventCommand(EventCommandEventArgs command)
		{
		}

		// Token: 0x06005A98 RID: 23192 RVA: 0x001B6A93 File Offset: 0x001B5C93
		protected void WriteEvent(int eventId)
		{
			this.WriteEventCore(eventId, 0, null);
		}

		// Token: 0x06005A99 RID: 23193 RVA: 0x001B6AA0 File Offset: 0x001B5CA0
		protected unsafe void WriteEvent(int eventId, int arg1)
		{
			if (this.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				ptr->Reserved = 0;
				this.WriteEventCore(eventId, 1, ptr);
			}
		}

		// Token: 0x06005A9A RID: 23194 RVA: 0x001B6AE8 File Offset: 0x001B5CE8
		protected unsafe void WriteEvent(int eventId, int arg1, int arg2)
		{
			if (this.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				ptr->Reserved = 0;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				ptr[1].Reserved = 0;
				this.WriteEventCore(eventId, 2, ptr);
			}
		}

		// Token: 0x06005A9B RID: 23195 RVA: 0x001B6B60 File Offset: 0x001B5D60
		protected unsafe void WriteEvent(int eventId, int arg1, int arg2, int arg3)
		{
			if (this.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 4;
				ptr->Reserved = 0;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 4;
				ptr[1].Reserved = 0;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 4;
				ptr[2].Reserved = 0;
				this.WriteEventCore(eventId, 3, ptr);
			}
		}

		// Token: 0x06005A9C RID: 23196 RVA: 0x001B6C14 File Offset: 0x001B5E14
		protected unsafe void WriteEvent(int eventId, long arg1)
		{
			if (this.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr->Reserved = 0;
				this.WriteEventCore(eventId, 1, ptr);
			}
		}

		// Token: 0x06005A9D RID: 23197 RVA: 0x001B6C5C File Offset: 0x001B5E5C
		protected unsafe void WriteEvent(int eventId, long arg1, long arg2)
		{
			if (this.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr->Reserved = 0;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 8;
				ptr[1].Reserved = 0;
				this.WriteEventCore(eventId, 2, ptr);
			}
		}

		// Token: 0x06005A9E RID: 23198 RVA: 0x001B6CD4 File Offset: 0x001B5ED4
		protected unsafe void WriteEvent(int eventId, long arg1, long arg2, long arg3)
		{
			if (this.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr->Reserved = 0;
				ptr[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr[1].Size = 8;
				ptr[1].Reserved = 0;
				ptr[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr[2].Size = 8;
				ptr[2].Reserved = 0;
				this.WriteEventCore(eventId, 3, ptr);
			}
		}

		// Token: 0x06005A9F RID: 23199 RVA: 0x001B6D88 File Offset: 0x001B5F88
		protected unsafe void WriteEvent(int eventId, string arg1)
		{
			if (this.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				char* ptr;
				if (arg1 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg1.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)1) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr3->DataPointer = (IntPtr)((void*)value);
				ptr3->Size = (arg1.Length + 1) * 2;
				ptr3->Reserved = 0;
				this.WriteEventCore(eventId, 1, ptr3);
				char* ptr2 = null;
			}
		}

		// Token: 0x06005AA0 RID: 23200 RVA: 0x001B6DF4 File Offset: 0x001B5FF4
		protected unsafe void WriteEvent(int eventId, string arg1, string arg2)
		{
			if (this.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				char* ptr;
				if (arg1 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg1.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				char* ptr3;
				if (arg2 == null)
				{
					ptr3 = null;
				}
				else
				{
					fixed (char* ptr4 = arg2.GetPinnableReference())
					{
						ptr3 = ptr4;
					}
				}
				char* value2 = ptr3;
				EventSource.EventData* ptr5 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr5->DataPointer = (IntPtr)((void*)value);
				ptr5->Size = (arg1.Length + 1) * 2;
				ptr5->Reserved = 0;
				ptr5[1].DataPointer = (IntPtr)((void*)value2);
				ptr5[1].Size = (arg2.Length + 1) * 2;
				ptr5[1].Reserved = 0;
				this.WriteEventCore(eventId, 2, ptr5);
				char* ptr4 = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06005AA1 RID: 23201 RVA: 0x001B6EC0 File Offset: 0x001B60C0
		protected unsafe void WriteEvent(int eventId, string arg1, string arg2, string arg3)
		{
			if (this.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				if (arg2 == null)
				{
					arg2 = "";
				}
				if (arg3 == null)
				{
					arg3 = "";
				}
				char* ptr;
				if (arg1 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg1.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				char* ptr3;
				if (arg2 == null)
				{
					ptr3 = null;
				}
				else
				{
					fixed (char* ptr4 = arg2.GetPinnableReference())
					{
						ptr3 = ptr4;
					}
				}
				char* value2 = ptr3;
				char* ptr5;
				if (arg3 == null)
				{
					ptr5 = null;
				}
				else
				{
					fixed (char* ptr6 = arg3.GetPinnableReference())
					{
						ptr5 = ptr6;
					}
				}
				char* value3 = ptr5;
				EventSource.EventData* ptr7 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr7->DataPointer = (IntPtr)((void*)value);
				ptr7->Size = (arg1.Length + 1) * 2;
				ptr7->Reserved = 0;
				ptr7[1].DataPointer = (IntPtr)((void*)value2);
				ptr7[1].Size = (arg2.Length + 1) * 2;
				ptr7[1].Reserved = 0;
				ptr7[2].DataPointer = (IntPtr)((void*)value3);
				ptr7[2].Size = (arg3.Length + 1) * 2;
				ptr7[2].Reserved = 0;
				this.WriteEventCore(eventId, 3, ptr7);
				char* ptr6 = null;
				char* ptr4 = null;
				char* ptr2 = null;
			}
		}

		// Token: 0x06005AA2 RID: 23202 RVA: 0x001B6FF8 File Offset: 0x001B61F8
		protected unsafe void WriteEvent(int eventId, string arg1, int arg2)
		{
			if (this.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				char* ptr;
				if (arg1 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg1.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr3->DataPointer = (IntPtr)((void*)value);
				ptr3->Size = (arg1.Length + 1) * 2;
				ptr3->Reserved = 0;
				ptr3[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr3[1].Size = 4;
				ptr3[1].Reserved = 0;
				this.WriteEventCore(eventId, 2, ptr3);
				char* ptr2 = null;
			}
		}

		// Token: 0x06005AA3 RID: 23203 RVA: 0x001B7098 File Offset: 0x001B6298
		protected unsafe void WriteEvent(int eventId, string arg1, int arg2, int arg3)
		{
			if (this.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				char* ptr;
				if (arg1 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg1.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr3->DataPointer = (IntPtr)((void*)value);
				ptr3->Size = (arg1.Length + 1) * 2;
				ptr3->Reserved = 0;
				ptr3[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr3[1].Size = 4;
				ptr3[1].Reserved = 0;
				ptr3[2].DataPointer = (IntPtr)((void*)(&arg3));
				ptr3[2].Size = 4;
				ptr3[2].Reserved = 0;
				this.WriteEventCore(eventId, 3, ptr3);
				char* ptr2 = null;
			}
		}

		// Token: 0x06005AA4 RID: 23204 RVA: 0x001B7174 File Offset: 0x001B6374
		protected unsafe void WriteEvent(int eventId, string arg1, long arg2)
		{
			if (this.IsEnabled())
			{
				if (arg1 == null)
				{
					arg1 = "";
				}
				char* ptr;
				if (arg1 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg1.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr3->DataPointer = (IntPtr)((void*)value);
				ptr3->Size = (arg1.Length + 1) * 2;
				ptr3->Reserved = 0;
				ptr3[1].DataPointer = (IntPtr)((void*)(&arg2));
				ptr3[1].Size = 8;
				ptr3[1].Reserved = 0;
				this.WriteEventCore(eventId, 2, ptr3);
				char* ptr2 = null;
			}
		}

		// Token: 0x06005AA5 RID: 23205 RVA: 0x001B7214 File Offset: 0x001B6414
		protected unsafe void WriteEvent(int eventId, long arg1, string arg2)
		{
			if (this.IsEnabled())
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				char* ptr;
				if (arg2 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg2.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr3->DataPointer = (IntPtr)((void*)(&arg1));
				ptr3->Size = 8;
				ptr3->Reserved = 0;
				ptr3[1].DataPointer = (IntPtr)((void*)value);
				ptr3[1].Size = (arg2.Length + 1) * 2;
				ptr3[1].Reserved = 0;
				this.WriteEventCore(eventId, 2, ptr3);
				char* ptr2 = null;
			}
		}

		// Token: 0x06005AA6 RID: 23206 RVA: 0x001B72B4 File Offset: 0x001B64B4
		protected unsafe void WriteEvent(int eventId, int arg1, string arg2)
		{
			if (this.IsEnabled())
			{
				if (arg2 == null)
				{
					arg2 = "";
				}
				char* ptr;
				if (arg2 == null)
				{
					ptr = null;
				}
				else
				{
					fixed (char* ptr2 = arg2.GetPinnableReference())
					{
						ptr = ptr2;
					}
				}
				char* value = ptr;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr3->DataPointer = (IntPtr)((void*)(&arg1));
				ptr3->Size = 4;
				ptr3->Reserved = 0;
				ptr3[1].DataPointer = (IntPtr)((void*)value);
				ptr3[1].Size = (arg2.Length + 1) * 2;
				ptr3[1].Reserved = 0;
				this.WriteEventCore(eventId, 2, ptr3);
				char* ptr2 = null;
			}
		}

		// Token: 0x06005AA7 RID: 23207 RVA: 0x001B7354 File Offset: 0x001B6554
		protected unsafe void WriteEvent(int eventId, byte[] arg1)
		{
			if (this.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventSource.EventData))];
				if (arg1 == null || arg1.Length == 0)
				{
					int num = 0;
					ptr->DataPointer = (IntPtr)((void*)(&num));
					ptr->Size = 4;
					ptr->Reserved = 0;
					ptr[1].DataPointer = (IntPtr)((void*)(&num));
					ptr[1].Size = 0;
					ptr[1].Reserved = 0;
					this.WriteEventCore(eventId, 2, ptr);
					return;
				}
				int size = arg1.Length;
				fixed (byte* ptr2 = &arg1[0])
				{
					byte* value = ptr2;
					ptr->DataPointer = (IntPtr)((void*)(&size));
					ptr->Size = 4;
					ptr->Reserved = 0;
					ptr[1].DataPointer = (IntPtr)((void*)value);
					ptr[1].Size = size;
					ptr[1].Reserved = 0;
					this.WriteEventCore(eventId, 2, ptr);
				}
			}
		}

		// Token: 0x06005AA8 RID: 23208 RVA: 0x001B7444 File Offset: 0x001B6644
		protected unsafe void WriteEvent(int eventId, long arg1, byte[] arg2)
		{
			if (this.IsEnabled())
			{
				EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)3) * (UIntPtr)sizeof(EventSource.EventData))];
				ptr->DataPointer = (IntPtr)((void*)(&arg1));
				ptr->Size = 8;
				ptr->Reserved = 0;
				if (arg2 == null || arg2.Length == 0)
				{
					int num = 0;
					ptr[1].DataPointer = (IntPtr)((void*)(&num));
					ptr[1].Size = 4;
					ptr[1].Reserved = 0;
					ptr[2].DataPointer = (IntPtr)((void*)(&num));
					ptr[2].Size = 0;
					ptr[2].Reserved = 0;
					this.WriteEventCore(eventId, 3, ptr);
					return;
				}
				int size = arg2.Length;
				fixed (byte* ptr2 = &arg2[0])
				{
					byte* value = ptr2;
					ptr[1].DataPointer = (IntPtr)((void*)(&size));
					ptr[1].Size = 4;
					ptr[1].Reserved = 0;
					ptr[2].DataPointer = (IntPtr)((void*)value);
					ptr[2].Size = size;
					ptr[2].Reserved = 0;
					this.WriteEventCore(eventId, 3, ptr);
				}
			}
		}

		// Token: 0x06005AA9 RID: 23209 RVA: 0x001B7589 File Offset: 0x001B6789
		[NullableContext(0)]
		[CLSCompliant(false)]
		protected unsafe void WriteEventCore(int eventId, int eventDataCount, EventSource.EventData* data)
		{
			this.WriteEventWithRelatedActivityIdCore(eventId, null, eventDataCount, data);
		}

		// Token: 0x06005AAA RID: 23210 RVA: 0x001B7598 File Offset: 0x001B6798
		[NullableContext(0)]
		[CLSCompliant(false)]
		protected unsafe void WriteEventWithRelatedActivityIdCore(int eventId, Guid* relatedActivityId, int eventDataCount, EventSource.EventData* data)
		{
			if (this.IsEnabled())
			{
				try
				{
					EventOpcode opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode;
					EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
					Guid* activityID = null;
					Guid empty = Guid.Empty;
					Guid empty2 = Guid.Empty;
					if (opcode != EventOpcode.Info && relatedActivityId == null && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
					{
						if (opcode == EventOpcode.Start)
						{
							this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, ref empty2, this.m_eventData[eventId].ActivityOptions, true);
						}
						else if (opcode == EventOpcode.Stop)
						{
							this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, true);
						}
						if (empty != Guid.Empty)
						{
							activityID = &empty;
						}
						if (empty2 != Guid.Empty)
						{
							relatedActivityId = &empty2;
						}
					}
					if (this.m_eventData[eventId].EnabledForETW || this.m_eventData[eventId].EnabledForEventPipe)
					{
						if (!this.SelfDescribingEvents)
						{
							if (!this.m_etwProvider.WriteEvent(ref this.m_eventData[eventId].Descriptor, this.m_eventData[eventId].EventHandle, activityID, relatedActivityId, eventDataCount, (IntPtr)((void*)data)))
							{
								this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
							}
							if (!this.m_eventPipeProvider.WriteEvent(ref this.m_eventData[eventId].Descriptor, this.m_eventData[eventId].EventHandle, activityID, relatedActivityId, eventDataCount, (IntPtr)((void*)data)))
							{
								this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
							}
						}
						else
						{
							TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
							if (traceLoggingEventTypes == null)
							{
								traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, this.m_eventData[eventId].Tags, this.m_eventData[eventId].Parameters);
								Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
							}
							EventSourceOptions eventSourceOptions = new EventSourceOptions
							{
								Keywords = (EventKeywords)this.m_eventData[eventId].Descriptor.Keywords,
								Level = (EventLevel)this.m_eventData[eventId].Descriptor.Level,
								Opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode
							};
							this.WriteMultiMerge(this.m_eventData[eventId].Name, ref eventSourceOptions, traceLoggingEventTypes, activityID, relatedActivityId, data);
						}
					}
					if (this.m_Dispatchers != null && this.m_eventData[eventId].EnabledForAnyListener)
					{
						this.WriteToAllListeners(eventId, activityID, relatedActivityId, eventDataCount, data);
					}
				}
				catch (Exception ex)
				{
					if (ex is EventSourceException)
					{
						throw;
					}
					this.ThrowEventSourceException(this.m_eventData[eventId].Name, ex);
				}
			}
		}

		// Token: 0x06005AAB RID: 23211 RVA: 0x001B7928 File Offset: 0x001B6B28
		protected void WriteEvent(int eventId, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			this.WriteEventVarargs(eventId, null, args);
		}

		// Token: 0x06005AAC RID: 23212 RVA: 0x001B7934 File Offset: 0x001B6B34
		protected unsafe void WriteEventWithRelatedActivityId(int eventId, Guid relatedActivityId, [Nullable(new byte[]
		{
			1,
			2
		})] params object[] args)
		{
			this.WriteEventVarargs(eventId, &relatedActivityId, args);
		}

		// Token: 0x06005AAD RID: 23213 RVA: 0x001B7941 File Offset: 0x001B6B41
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06005AAE RID: 23214 RVA: 0x001B7950 File Offset: 0x001B6B50
		protected virtual void Dispose(bool disposing)
		{
			if (!EventSource.IsSupported)
			{
				return;
			}
			if (disposing)
			{
				if (this.m_eventSourceEnabled)
				{
					try
					{
						this.SendManifest(this.m_rawManifest);
					}
					catch
					{
					}
					this.m_eventSourceEnabled = false;
				}
				if (this.m_etwProvider != null)
				{
					this.m_etwProvider.Dispose();
					this.m_etwProvider = null;
				}
				if (this.m_eventPipeProvider != null)
				{
					this.m_eventPipeProvider.Dispose();
					this.m_eventPipeProvider = null;
				}
			}
			this.m_eventSourceEnabled = false;
			this.m_eventSourceDisposed = true;
		}

		// Token: 0x06005AAF RID: 23215 RVA: 0x001B79E8 File Offset: 0x001B6BE8
		~EventSource()
		{
			this.Dispose(false);
		}

		// Token: 0x06005AB0 RID: 23216 RVA: 0x001B7A18 File Offset: 0x001B6C18
		private unsafe void WriteEventRaw(string eventName, ref EventDescriptor eventDescriptor, IntPtr eventHandle, Guid* activityID, Guid* relatedActivityID, int dataCount, IntPtr data)
		{
			bool flag = true;
			flag &= (this.m_etwProvider == null);
			if (this.m_etwProvider != null && !this.m_etwProvider.WriteEventRaw(ref eventDescriptor, eventHandle, activityID, relatedActivityID, dataCount, data))
			{
				this.ThrowEventSourceException(eventName, null);
			}
			flag &= (this.m_eventPipeProvider == null);
			if (this.m_eventPipeProvider != null && !this.m_eventPipeProvider.WriteEventRaw(ref eventDescriptor, eventHandle, activityID, relatedActivityID, dataCount, data))
			{
				this.ThrowEventSourceException(eventName, null);
			}
			if (flag)
			{
				this.ThrowEventSourceException(eventName, null);
			}
		}

		// Token: 0x06005AB1 RID: 23217 RVA: 0x001B7AA4 File Offset: 0x001B6CA4
		internal EventSource(Guid eventSourceGuid, string eventSourceName) : this(eventSourceGuid, eventSourceName, EventSourceSettings.EtwManifestEventFormat, null)
		{
		}

		// Token: 0x06005AB2 RID: 23218 RVA: 0x001B7AB0 File Offset: 0x001B6CB0
		internal EventSource(Guid eventSourceGuid, string eventSourceName, EventSourceSettings settings, string[] traits = null)
		{
			this.m_createEventLock = new object();
			this.m_writeEventStringEventHandle = IntPtr.Zero;
			base..ctor();
			if (EventSource.IsSupported)
			{
				this.m_eventHandleTable = new TraceLoggingEventHandleTable();
				this.m_config = this.ValidateSettings(settings);
				this.Initialize(eventSourceGuid, eventSourceName, traits);
			}
		}

		// Token: 0x06005AB3 RID: 23219 RVA: 0x001B7B04 File Offset: 0x001B6D04
		private void Initialize(Guid eventSourceGuid, string eventSourceName, string[] traits)
		{
			try
			{
				this.m_traits = traits;
				if (this.m_traits != null && this.m_traits.Length % 2 != 0)
				{
					throw new ArgumentException(SR.EventSource_TraitEven, "traits");
				}
				if (eventSourceGuid == Guid.Empty)
				{
					throw new ArgumentException(SR.EventSource_NeedGuid);
				}
				if (eventSourceName == null)
				{
					throw new ArgumentException(SR.EventSource_NeedName);
				}
				this.m_name = eventSourceName;
				this.m_guid = eventSourceGuid;
				this.m_activityTracker = ActivityTracker.Instance;
				this.InitializeProviderMetadata();
				EventSource.OverideEventProvider overideEventProvider = new EventSource.OverideEventProvider(this, EventProviderType.ETW);
				overideEventProvider.Register(this);
				EventSource.OverideEventProvider overideEventProvider2 = new EventSource.OverideEventProvider(this, EventProviderType.EventPipe);
				object eventListenersLock = EventListener.EventListenersLock;
				lock (eventListenersLock)
				{
					overideEventProvider2.Register(this);
				}
				EventListener.AddEventSource(this);
				this.m_etwProvider = overideEventProvider;
				if (this.Name != "System.Diagnostics.Eventing.FrameworkEventSource" || Environment.IsWindows8OrAbove)
				{
					GCHandle gchandle = GCHandle.Alloc(this.providerMetadata, GCHandleType.Pinned);
					IntPtr data = gchandle.AddrOfPinnedObject();
					int num = this.m_etwProvider.SetInformation(Interop.Advapi32.EVENT_INFO_CLASS.SetTraits, data, (uint)this.providerMetadata.Length);
					gchandle.Free();
				}
				this.m_eventPipeProvider = overideEventProvider2;
				this.m_completelyInited = true;
			}
			catch (Exception ex)
			{
				if (this.m_constructionException == null)
				{
					this.m_constructionException = ex;
				}
				this.ReportOutOfBandMessage("ERROR: Exception during construction of EventSource " + this.Name + ": " + ex.Message);
			}
			object eventListenersLock2 = EventListener.EventListenersLock;
			lock (eventListenersLock2)
			{
				for (EventCommandEventArgs eventCommandEventArgs = this.m_deferredCommands; eventCommandEventArgs != null; eventCommandEventArgs = eventCommandEventArgs.nextCommand)
				{
					this.DoCommand(eventCommandEventArgs);
				}
			}
		}

		// Token: 0x06005AB4 RID: 23220 RVA: 0x001B7CF0 File Offset: 0x001B6EF0
		private static string GetName(Type eventSourceType, EventManifestOptions flags)
		{
			if (eventSourceType == null)
			{
				throw new ArgumentNullException("eventSourceType");
			}
			EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), flags);
			if (eventSourceAttribute != null && eventSourceAttribute.Name != null)
			{
				return eventSourceAttribute.Name;
			}
			return eventSourceType.Name;
		}

		// Token: 0x06005AB5 RID: 23221 RVA: 0x001B7D40 File Offset: 0x001B6F40
		private unsafe static Guid GenerateGuidFromName(string name)
		{
			ReadOnlySpan<byte> input = new ReadOnlySpan<byte>((void*)(&<PrivateImplementationDetails>.BABB0FF3A59F6738CDF14170DA56230D6528DC9D619C5860288B86A4E3B29D63), 16);
			byte[] bytes = Encoding.BigEndianUnicode.GetBytes(name);
			EventSource.Sha1ForNonSecretPurposes sha1ForNonSecretPurposes = default(EventSource.Sha1ForNonSecretPurposes);
			sha1ForNonSecretPurposes.Start();
			sha1ForNonSecretPurposes.Append(input);
			sha1ForNonSecretPurposes.Append(bytes);
			Array.Resize<byte>(ref bytes, 16);
			sha1ForNonSecretPurposes.Finish(bytes);
			bytes[7] = ((bytes[7] & 15) | 80);
			return new Guid(bytes);
		}

		// Token: 0x06005AB6 RID: 23222 RVA: 0x001B7DB0 File Offset: 0x001B6FB0
		private unsafe object DecodeObject(int eventId, int parameterId, ref EventSource.EventData* data)
		{
			IntPtr dataPointer = data.DataPointer;
			data += (IntPtr)sizeof(EventSource.EventData);
			Type type = EventSource.GetDataType(this.m_eventData[eventId], parameterId);
			while (!(type == typeof(IntPtr)))
			{
				if (type == typeof(int))
				{
					return *(int*)((void*)dataPointer);
				}
				if (type == typeof(uint))
				{
					return *(uint*)((void*)dataPointer);
				}
				if (type == typeof(long))
				{
					return *(long*)((void*)dataPointer);
				}
				if (type == typeof(ulong))
				{
					return (ulong)(*(long*)((void*)dataPointer));
				}
				if (type == typeof(byte))
				{
					return *(byte*)((void*)dataPointer);
				}
				if (type == typeof(sbyte))
				{
					return *(sbyte*)((void*)dataPointer);
				}
				if (type == typeof(short))
				{
					return *(short*)((void*)dataPointer);
				}
				if (type == typeof(ushort))
				{
					return *(ushort*)((void*)dataPointer);
				}
				if (type == typeof(float))
				{
					return *(float*)((void*)dataPointer);
				}
				if (type == typeof(double))
				{
					return *(double*)((void*)dataPointer);
				}
				if (type == typeof(decimal))
				{
					return *(decimal*)((void*)dataPointer);
				}
				if (type == typeof(bool))
				{
					return *(int*)((void*)dataPointer) == 1;
				}
				if (type == typeof(Guid))
				{
					return *(Guid*)((void*)dataPointer);
				}
				if (type == typeof(char))
				{
					return (char)(*(ushort*)((void*)dataPointer));
				}
				if (type == typeof(DateTime))
				{
					long fileTime = *(long*)((void*)dataPointer);
					return DateTime.FromFileTimeUtc(fileTime);
				}
				if (type == typeof(byte[]))
				{
					int num = *(int*)((void*)dataPointer);
					byte[] array = new byte[num];
					dataPointer = data.DataPointer;
					data += (IntPtr)sizeof(EventSource.EventData);
					for (int i = 0; i < num; i++)
					{
						array[i] = *(byte*)((void*)(dataPointer + i));
					}
					return array;
				}
				if (type == typeof(byte*))
				{
					return null;
				}
				object result;
				try
				{
					EventSource.m_EventSourceInDecodeObject = true;
					if (type.IsEnum)
					{
						type = Enum.GetUnderlyingType(type);
						int num2 = Marshal.SizeOf(type);
						if (num2 < 4)
						{
							type = typeof(int);
						}
						continue;
					}
					if (dataPointer == IntPtr.Zero)
					{
						result = null;
					}
					else
					{
						result = new string((char*)((void*)dataPointer));
					}
				}
				finally
				{
					EventSource.m_EventSourceInDecodeObject = false;
				}
				return result;
			}
			return *(IntPtr*)((void*)dataPointer);
		}

		// Token: 0x06005AB7 RID: 23223 RVA: 0x001B80C4 File Offset: 0x001B72C4
		private EventDispatcher GetDispatcher(EventListener listener)
		{
			EventDispatcher eventDispatcher;
			for (eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				if (eventDispatcher.m_Listener == listener)
				{
					return eventDispatcher;
				}
			}
			return eventDispatcher;
		}

		// Token: 0x06005AB8 RID: 23224 RVA: 0x001B80F4 File Offset: 0x001B72F4
		private unsafe void WriteEventVarargs(int eventId, Guid* childActivityID, object[] args)
		{
			if (this.IsEnabled())
			{
				try
				{
					if (childActivityID != null && !this.m_eventData[eventId].HasRelatedActivityID)
					{
						throw new ArgumentException(SR.EventSource_NoRelatedActivityId);
					}
					this.LogEventArgsMismatches(eventId, args);
					Guid* activityID = null;
					Guid empty = Guid.Empty;
					Guid empty2 = Guid.Empty;
					EventOpcode opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode;
					EventActivityOptions activityOptions = this.m_eventData[eventId].ActivityOptions;
					if (childActivityID == null && (activityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
					{
						if (opcode == EventOpcode.Start)
						{
							this.m_activityTracker.OnStart(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, ref empty2, this.m_eventData[eventId].ActivityOptions, true);
						}
						else if (opcode == EventOpcode.Stop)
						{
							this.m_activityTracker.OnStop(this.m_name, this.m_eventData[eventId].Name, this.m_eventData[eventId].Descriptor.Task, ref empty, true);
						}
						if (empty != Guid.Empty)
						{
							activityID = &empty;
						}
						if (empty2 != Guid.Empty)
						{
							childActivityID = &empty2;
						}
					}
					if (this.m_eventData[eventId].EnabledForETW || this.m_eventData[eventId].EnabledForEventPipe)
					{
						if (!this.SelfDescribingEvents)
						{
							if (!this.m_etwProvider.WriteEvent(ref this.m_eventData[eventId].Descriptor, this.m_eventData[eventId].EventHandle, activityID, childActivityID, args))
							{
								this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
							}
							if (!this.m_eventPipeProvider.WriteEvent(ref this.m_eventData[eventId].Descriptor, this.m_eventData[eventId].EventHandle, activityID, childActivityID, args))
							{
								this.ThrowEventSourceException(this.m_eventData[eventId].Name, null);
							}
						}
						else
						{
							TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
							if (traceLoggingEventTypes == null)
							{
								traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
								Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
							}
							EventSourceOptions eventSourceOptions = new EventSourceOptions
							{
								Keywords = (EventKeywords)this.m_eventData[eventId].Descriptor.Keywords,
								Level = (EventLevel)this.m_eventData[eventId].Descriptor.Level,
								Opcode = (EventOpcode)this.m_eventData[eventId].Descriptor.Opcode
							};
							this.WriteMultiMerge(this.m_eventData[eventId].Name, ref eventSourceOptions, traceLoggingEventTypes, activityID, childActivityID, args);
						}
					}
					if (this.m_Dispatchers != null && this.m_eventData[eventId].EnabledForAnyListener)
					{
						if (LocalAppContextSwitches.PreserveEventListnerObjectIdentity)
						{
							this.WriteToAllListeners(eventId, null, null, activityID, childActivityID, args);
						}
						else
						{
							object[] args2 = this.SerializeEventArgs(eventId, args);
							this.WriteToAllListeners(eventId, null, null, activityID, childActivityID, args2);
						}
					}
				}
				catch (Exception ex)
				{
					if (ex is EventSourceException)
					{
						throw;
					}
					this.ThrowEventSourceException(this.m_eventData[eventId].Name, ex);
				}
			}
		}

		// Token: 0x06005AB9 RID: 23225 RVA: 0x001B84B0 File Offset: 0x001B76B0
		private object[] SerializeEventArgs(int eventId, object[] args)
		{
			TraceLoggingEventTypes traceLoggingEventTypes = this.m_eventData[eventId].TraceLoggingEventTypes;
			if (traceLoggingEventTypes == null)
			{
				traceLoggingEventTypes = new TraceLoggingEventTypes(this.m_eventData[eventId].Name, EventTags.None, this.m_eventData[eventId].Parameters);
				Interlocked.CompareExchange<TraceLoggingEventTypes>(ref this.m_eventData[eventId].TraceLoggingEventTypes, traceLoggingEventTypes, null);
			}
			int num = Math.Min(traceLoggingEventTypes.typeInfos.Length, args.Length);
			object[] array = new object[traceLoggingEventTypes.typeInfos.Length];
			for (int i = 0; i < num; i++)
			{
				array[i] = traceLoggingEventTypes.typeInfos[i].GetData(args[i]);
			}
			return array;
		}

		// Token: 0x06005ABA RID: 23226 RVA: 0x001B855C File Offset: 0x001B775C
		private void LogEventArgsMismatches(int eventId, object[] args)
		{
			ParameterInfo[] parameters = this.m_eventData[eventId].Parameters;
			if (args.Length != parameters.Length)
			{
				this.ReportOutOfBandMessage(SR.Format(SR.EventSource_EventParametersMismatch, eventId, args.Length, parameters.Length));
				return;
			}
			for (int i = 0; i < args.Length; i++)
			{
				Type parameterType = parameters[i].ParameterType;
				object obj = args[i];
				if ((obj != null && !parameterType.IsAssignableFrom(obj.GetType())) || (obj == null && parameterType.IsValueType && (!parameterType.IsGenericType || !(parameterType.GetGenericTypeDefinition() == typeof(Nullable<>)))))
				{
					this.ReportOutOfBandMessage(SR.Format(SR.EventSource_VarArgsParameterMismatch, eventId, parameters[i].Name));
					return;
				}
			}
		}

		// Token: 0x06005ABB RID: 23227 RVA: 0x001B8624 File Offset: 0x001B7824
		private unsafe void WriteToAllListeners(int eventId, Guid* activityID, Guid* childActivityID, int eventDataCount, EventSource.EventData* data)
		{
			int num = EventSource.GetParameterCount(this.m_eventData[eventId]);
			int num2 = 0;
			for (int i = 0; i < num; i++)
			{
				Type dataType = EventSource.GetDataType(this.m_eventData[eventId], i);
				if (dataType == typeof(byte[]))
				{
					num2 += 2;
				}
				else
				{
					num2++;
				}
			}
			if (eventDataCount != num2)
			{
				this.ReportOutOfBandMessage(SR.Format(SR.EventSource_EventParametersMismatch, eventId, eventDataCount, num));
				num = Math.Min(num, eventDataCount);
			}
			object[] array = new object[num];
			EventSource.EventData* ptr = data;
			for (int j = 0; j < num; j++)
			{
				array[j] = this.DecodeObject(eventId, j, ref ptr);
			}
			this.WriteToAllListeners(eventId, null, null, activityID, childActivityID, array);
		}

		// Token: 0x06005ABC RID: 23228 RVA: 0x001B86F4 File Offset: 0x001B78F4
		internal unsafe void WriteToAllListeners(int eventId, uint* osThreadId, DateTime* timeStamp, Guid* activityID, Guid* childActivityID, params object[] args)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventId = eventId;
			if (osThreadId != null)
			{
				eventWrittenEventArgs.OSThreadId = (long)(*osThreadId);
			}
			if (timeStamp != null)
			{
				eventWrittenEventArgs.TimeStamp = *timeStamp;
			}
			if (activityID != null)
			{
				eventWrittenEventArgs.ActivityId = *activityID;
			}
			if (childActivityID != null)
			{
				eventWrittenEventArgs.RelatedActivityId = *childActivityID;
			}
			eventWrittenEventArgs.EventName = this.m_eventData[eventId].Name;
			eventWrittenEventArgs.Message = this.m_eventData[eventId].Message;
			eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>(args);
			this.DispatchToAllListeners(eventId, eventWrittenEventArgs);
		}

		// Token: 0x06005ABD RID: 23229 RVA: 0x001B879C File Offset: 0x001B799C
		private void DispatchToAllListeners(int eventId, EventWrittenEventArgs eventCallbackArgs)
		{
			Exception ex = null;
			for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				if (eventId == -1 || eventDispatcher.m_EventEnabled[eventId])
				{
					try
					{
						eventDispatcher.m_Listener.OnEventWritten(eventCallbackArgs);
					}
					catch (Exception ex2)
					{
						this.ReportOutOfBandMessage("ERROR: Exception during EventSource.OnEventWritten: " + ex2.Message);
						ex = ex2;
					}
				}
			}
			if (ex != null)
			{
				throw new EventSourceException(ex);
			}
		}

		// Token: 0x06005ABE RID: 23230 RVA: 0x001B8814 File Offset: 0x001B7A14
		private unsafe void WriteEventString(string msgString)
		{
			bool flag = true;
			flag &= (this.m_etwProvider == null);
			flag &= (this.m_eventPipeProvider == null);
			if (flag)
			{
				return;
			}
			EventLevel eventLevel = EventLevel.LogAlways;
			long keywords = -1L;
			if (this.SelfDescribingEvents)
			{
				EventSourceOptions eventSourceOptions = new EventSourceOptions
				{
					Keywords = (EventKeywords)keywords,
					Level = eventLevel
				};
				TraceLoggingEventTypes eventTypes = new TraceLoggingEventTypes("EventSourceMessage", EventTags.None, new Type[]
				{
					typeof(string)
				});
				this.WriteMultiMergeInner("EventSourceMessage", ref eventSourceOptions, eventTypes, null, null, new object[]
				{
					msgString
				});
				return;
			}
			if (this.m_rawManifest == null && this.m_outOfBandMessageCount == 1)
			{
				ManifestBuilder manifestBuilder = new ManifestBuilder(this.Name, this.Guid, this.Name, null, EventManifestOptions.None);
				manifestBuilder.StartEvent("EventSourceMessage", new EventAttribute(0)
				{
					Level = eventLevel,
					Task = (EventTask)65534
				});
				manifestBuilder.AddEventParameter(typeof(string), "message");
				manifestBuilder.EndEvent();
				this.SendManifest(manifestBuilder.CreateManifest());
			}
			char* ptr;
			if (msgString == null)
			{
				ptr = null;
			}
			else
			{
				fixed (char* ptr2 = msgString.GetPinnableReference())
				{
					ptr = ptr2;
				}
			}
			char* ptr3 = ptr;
			EventDescriptor eventDescriptor = new EventDescriptor(0, 0, 0, (byte)eventLevel, 0, 0, keywords);
			EventProvider.EventData eventData = default(EventProvider.EventData);
			eventData.Ptr = ptr3;
			eventData.Size = (uint)(2 * (msgString.Length + 1));
			eventData.Reserved = 0U;
			if (this.m_etwProvider != null)
			{
				this.m_etwProvider.WriteEvent(ref eventDescriptor, IntPtr.Zero, null, null, 1, (IntPtr)((void*)(&eventData)));
			}
			if (this.m_eventPipeProvider != null)
			{
				if (this.m_writeEventStringEventHandle == IntPtr.Zero)
				{
					object createEventLock = this.m_createEventLock;
					lock (createEventLock)
					{
						if (this.m_writeEventStringEventHandle == IntPtr.Zero)
						{
							string eventName = "EventSourceMessage";
							EventParameterInfo eventParameterInfo = default(EventParameterInfo);
							eventParameterInfo.SetInfo("message", typeof(string), null);
							byte[] array = EventPipeMetadataGenerator.Instance.GenerateMetadata(0, eventName, keywords, (uint)eventLevel, 0U, EventOpcode.Info, new EventParameterInfo[]
							{
								eventParameterInfo
							});
							uint metadataLength = (uint)((array != null) ? array.Length : 0);
							byte[] array2;
							byte* pMetadata;
							if ((array2 = array) == null || array2.Length == 0)
							{
								pMetadata = null;
							}
							else
							{
								pMetadata = &array2[0];
							}
							this.m_writeEventStringEventHandle = this.m_eventPipeProvider.m_eventProvider.DefineEventHandle(0U, eventName, keywords, 0U, (uint)eventLevel, pMetadata, metadataLength);
							array2 = null;
						}
					}
				}
				this.m_eventPipeProvider.WriteEvent(ref eventDescriptor, this.m_writeEventStringEventHandle, null, null, 1, (IntPtr)((void*)(&eventData)));
			}
			char* ptr2 = null;
		}

		// Token: 0x06005ABF RID: 23231 RVA: 0x001B8AC8 File Offset: 0x001B7CC8
		private void WriteStringToAllListeners(string eventName, string msg)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventId = 0;
			eventWrittenEventArgs.Message = msg;
			eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>(new List<object>
			{
				msg
			});
			eventWrittenEventArgs.PayloadNames = new ReadOnlyCollection<string>(new List<string>
			{
				"message"
			});
			eventWrittenEventArgs.EventName = eventName;
			for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
			{
				bool flag = false;
				if (eventDispatcher.m_EventEnabled == null)
				{
					flag = true;
				}
				else
				{
					for (int i = 0; i < eventDispatcher.m_EventEnabled.Length; i++)
					{
						if (eventDispatcher.m_EventEnabled[i])
						{
							flag = true;
							break;
						}
					}
				}
				try
				{
					if (flag)
					{
						eventDispatcher.m_Listener.OnEventWritten(eventWrittenEventArgs);
					}
				}
				catch
				{
				}
			}
		}

		// Token: 0x06005AC0 RID: 23232 RVA: 0x001B8B8C File Offset: 0x001B7D8C
		private bool IsEnabledByDefault(int eventNum, bool enable, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword)
		{
			if (!enable)
			{
				return false;
			}
			EventLevel level = (EventLevel)this.m_eventData[eventNum].Descriptor.Level;
			EventKeywords eventKeywords = (EventKeywords)(this.m_eventData[eventNum].Descriptor.Keywords & (long)(~(long)SessionMask.All.ToEventKeywords()));
			EventChannel channel = (EventChannel)this.m_eventData[eventNum].Descriptor.Channel;
			return this.IsEnabledCommon(enable, currentLevel, currentMatchAnyKeyword, level, eventKeywords, channel);
		}

		// Token: 0x06005AC1 RID: 23233 RVA: 0x001B8C08 File Offset: 0x001B7E08
		private bool IsEnabledCommon(bool enabled, EventLevel currentLevel, EventKeywords currentMatchAnyKeyword, EventLevel eventLevel, EventKeywords eventKeywords, EventChannel eventChannel)
		{
			if (!enabled)
			{
				return false;
			}
			if (currentLevel != EventLevel.LogAlways && currentLevel < eventLevel)
			{
				return false;
			}
			if (currentMatchAnyKeyword != EventKeywords.None && eventKeywords != EventKeywords.None)
			{
				if (eventChannel != EventChannel.None && this.m_channelData != null && this.m_channelData.Length > (int)eventChannel)
				{
					EventKeywords eventKeywords2 = (EventKeywords)(this.m_channelData[(int)eventChannel] | (ulong)eventKeywords);
					if (eventKeywords2 != EventKeywords.None && (eventKeywords2 & currentMatchAnyKeyword) == EventKeywords.None)
					{
						return false;
					}
				}
				else if ((eventKeywords & currentMatchAnyKeyword) == EventKeywords.None)
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06005AC2 RID: 23234 RVA: 0x001B8C6C File Offset: 0x001B7E6C
		[MethodImpl(MethodImplOptions.NoInlining)]
		private void ThrowEventSourceException(string eventName, Exception innerEx = null)
		{
			if (EventSource.m_EventSourceExceptionRecurenceCount > 0)
			{
				return;
			}
			try
			{
				EventSource.m_EventSourceExceptionRecurenceCount += 1;
				string text = "EventSourceException";
				if (eventName != null)
				{
					text = text + " while processing event \"" + eventName + "\"";
				}
				switch (EventProvider.GetLastWriteEventError())
				{
				case EventProvider.WriteEventErrorCode.NoFreeBuffers:
					this.ReportOutOfBandMessage(text + ": " + SR.EventSource_NoFreeBuffers);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(SR.EventSource_NoFreeBuffers, innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.EventTooBig:
					this.ReportOutOfBandMessage(text + ": " + SR.EventSource_EventTooBig);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(SR.EventSource_EventTooBig, innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.NullInput:
					this.ReportOutOfBandMessage(text + ": " + SR.EventSource_NullInput);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(SR.EventSource_NullInput, innerEx);
					}
					break;
				case EventProvider.WriteEventErrorCode.TooManyArgs:
					this.ReportOutOfBandMessage(text + ": " + SR.EventSource_TooManyArgs);
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(SR.EventSource_TooManyArgs, innerEx);
					}
					break;
				default:
					if (innerEx != null)
					{
						innerEx = innerEx.GetBaseException();
						string[] array = new string[5];
						array[0] = text;
						array[1] = ": ";
						int num = 2;
						Type type = innerEx.GetType();
						array[num] = ((type != null) ? type.ToString() : null);
						array[3] = ":";
						array[4] = innerEx.Message;
						this.ReportOutOfBandMessage(string.Concat(array));
					}
					else
					{
						this.ReportOutOfBandMessage(text);
					}
					if (this.ThrowOnEventWriteErrors)
					{
						throw new EventSourceException(innerEx);
					}
					break;
				}
			}
			finally
			{
				EventSource.m_EventSourceExceptionRecurenceCount -= 1;
			}
		}

		// Token: 0x06005AC3 RID: 23235 RVA: 0x001B8E14 File Offset: 0x001B8014
		internal static EventOpcode GetOpcodeWithDefault(EventOpcode opcode, string eventName)
		{
			if (opcode == EventOpcode.Info && eventName != null)
			{
				if (eventName.EndsWith("Start", StringComparison.Ordinal))
				{
					return EventOpcode.Start;
				}
				if (eventName.EndsWith("Stop", StringComparison.Ordinal))
				{
					return EventOpcode.Stop;
				}
			}
			return opcode;
		}

		// Token: 0x06005AC4 RID: 23236 RVA: 0x001B8E3D File Offset: 0x001B803D
		private static int GetParameterCount(EventSource.EventMetadata eventData)
		{
			return eventData.Parameters.Length;
		}

		// Token: 0x06005AC5 RID: 23237 RVA: 0x001B8E47 File Offset: 0x001B8047
		private static Type GetDataType(EventSource.EventMetadata eventData, int parameterId)
		{
			return eventData.Parameters[parameterId].ParameterType;
		}

		// Token: 0x06005AC6 RID: 23238 RVA: 0x001B8E58 File Offset: 0x001B8058
		internal void SendCommand(EventListener listener, EventProviderType eventProviderType, int perEventSourceSessionId, int etwSessionId, EventCommand command, bool enable, EventLevel level, EventKeywords matchAnyKeyword, IDictionary<string, string> commandArguments)
		{
			if (!EventSource.IsSupported)
			{
				return;
			}
			EventCommandEventArgs eventCommandEventArgs = new EventCommandEventArgs(command, commandArguments, this, listener, eventProviderType, perEventSourceSessionId, etwSessionId, enable, level, matchAnyKeyword);
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				if (this.m_completelyInited)
				{
					this.m_deferredCommands = null;
					this.DoCommand(eventCommandEventArgs);
				}
				else if (this.m_deferredCommands == null)
				{
					this.m_deferredCommands = eventCommandEventArgs;
				}
				else
				{
					EventCommandEventArgs eventCommandEventArgs2 = this.m_deferredCommands;
					while (eventCommandEventArgs2.nextCommand != null)
					{
						eventCommandEventArgs2 = eventCommandEventArgs2.nextCommand;
					}
					eventCommandEventArgs2.nextCommand = eventCommandEventArgs;
				}
			}
		}

		// Token: 0x06005AC7 RID: 23239 RVA: 0x001B8EF8 File Offset: 0x001B80F8
		internal void DoCommand(EventCommandEventArgs commandArgs)
		{
			if (!EventSource.IsSupported)
			{
				return;
			}
			if (this.m_etwProvider == null)
			{
				return;
			}
			if (this.m_eventPipeProvider == null)
			{
				return;
			}
			this.m_outOfBandMessageCount = 0;
			try
			{
				this.EnsureDescriptorsInitialized();
				commandArgs.dispatcher = this.GetDispatcher(commandArgs.listener);
				if (commandArgs.dispatcher == null && commandArgs.listener != null)
				{
					throw new ArgumentException(SR.EventSource_ListenerNotFound);
				}
				if (commandArgs.Arguments == null)
				{
					commandArgs.Arguments = new Dictionary<string, string>();
				}
				if (commandArgs.Command == EventCommand.Update)
				{
					for (int i = 0; i < this.m_eventData.Length; i++)
					{
						this.EnableEventForDispatcher(commandArgs.dispatcher, commandArgs.eventProviderType, i, this.IsEnabledByDefault(i, commandArgs.enable, commandArgs.level, commandArgs.matchAnyKeyword));
					}
					if (commandArgs.enable)
					{
						if (!this.m_eventSourceEnabled)
						{
							this.m_level = commandArgs.level;
							this.m_matchAnyKeyword = commandArgs.matchAnyKeyword;
						}
						else
						{
							if (commandArgs.level > this.m_level)
							{
								this.m_level = commandArgs.level;
							}
							if (commandArgs.matchAnyKeyword == EventKeywords.None)
							{
								this.m_matchAnyKeyword = EventKeywords.None;
							}
							else if (this.m_matchAnyKeyword != EventKeywords.None)
							{
								this.m_matchAnyKeyword |= commandArgs.matchAnyKeyword;
							}
						}
					}
					bool flag = commandArgs.perEventSourceSessionId >= 0;
					if (commandArgs.perEventSourceSessionId == 0 && !commandArgs.enable)
					{
						flag = false;
					}
					if (commandArgs.listener == null)
					{
						if (!flag)
						{
							commandArgs.perEventSourceSessionId = -commandArgs.perEventSourceSessionId;
						}
						commandArgs.perEventSourceSessionId--;
					}
					commandArgs.Command = (flag ? EventCommand.Enable : EventCommand.Disable);
					if (flag && commandArgs.dispatcher == null && !this.SelfDescribingEvents)
					{
						this.SendManifest(this.m_rawManifest);
					}
					if (commandArgs.enable)
					{
						this.m_eventSourceEnabled = true;
					}
					this.OnEventCommand(commandArgs);
					EventHandler<EventCommandEventArgs> eventCommandExecuted = this.m_eventCommandExecuted;
					if (eventCommandExecuted != null)
					{
						eventCommandExecuted(this, commandArgs);
					}
					if (!commandArgs.enable)
					{
						for (int j = 0; j < this.m_eventData.Length; j++)
						{
							bool enabledForAnyListener = false;
							for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
							{
								if (eventDispatcher.m_EventEnabled[j])
								{
									enabledForAnyListener = true;
									break;
								}
							}
							this.m_eventData[j].EnabledForAnyListener = enabledForAnyListener;
						}
						if (!this.AnyEventEnabled())
						{
							this.m_level = EventLevel.LogAlways;
							this.m_matchAnyKeyword = EventKeywords.None;
							this.m_eventSourceEnabled = false;
						}
					}
				}
				else
				{
					if (commandArgs.Command == EventCommand.SendManifest && this.m_rawManifest != null)
					{
						this.SendManifest(this.m_rawManifest);
					}
					this.OnEventCommand(commandArgs);
					EventHandler<EventCommandEventArgs> eventCommandExecuted2 = this.m_eventCommandExecuted;
					if (eventCommandExecuted2 != null)
					{
						eventCommandExecuted2(this, commandArgs);
					}
				}
			}
			catch (Exception ex)
			{
				this.ReportOutOfBandMessage("ERROR: Exception in Command Processing for EventSource " + this.Name + ": " + ex.Message);
			}
		}

		// Token: 0x06005AC8 RID: 23240 RVA: 0x001B91D8 File Offset: 0x001B83D8
		internal bool EnableEventForDispatcher(EventDispatcher dispatcher, EventProviderType eventProviderType, int eventId, bool value)
		{
			if (!EventSource.IsSupported)
			{
				return false;
			}
			if (dispatcher == null)
			{
				if (eventId >= this.m_eventData.Length)
				{
					return false;
				}
				if (this.m_etwProvider != null && eventProviderType == EventProviderType.ETW)
				{
					this.m_eventData[eventId].EnabledForETW = value;
				}
				if (this.m_eventPipeProvider != null && eventProviderType == EventProviderType.EventPipe)
				{
					this.m_eventData[eventId].EnabledForEventPipe = value;
				}
			}
			else
			{
				if (eventId >= dispatcher.m_EventEnabled.Length)
				{
					return false;
				}
				dispatcher.m_EventEnabled[eventId] = value;
				if (value)
				{
					this.m_eventData[eventId].EnabledForAnyListener = true;
				}
			}
			return true;
		}

		// Token: 0x06005AC9 RID: 23241 RVA: 0x001B9278 File Offset: 0x001B8478
		private bool AnyEventEnabled()
		{
			for (int i = 0; i < this.m_eventData.Length; i++)
			{
				if (this.m_eventData[i].EnabledForETW || this.m_eventData[i].EnabledForAnyListener || this.m_eventData[i].EnabledForEventPipe)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06005ACA RID: 23242 RVA: 0x001B92DC File Offset: 0x001B84DC
		private bool IsDisposed
		{
			get
			{
				return this.m_eventSourceDisposed;
			}
		}

		// Token: 0x06005ACB RID: 23243 RVA: 0x001B92E4 File Offset: 0x001B84E4
		private void EnsureDescriptorsInitialized()
		{
			if (this.m_eventData == null)
			{
				this.m_rawManifest = EventSource.CreateManifestAndDescriptors(base.GetType(), this.Name, this, EventManifestOptions.None);
				foreach (WeakReference<EventSource> weakReference in EventListener.s_EventSources)
				{
					EventSource eventSource;
					if (weakReference.TryGetTarget(out eventSource) && eventSource.Guid == this.m_guid && !eventSource.IsDisposed && eventSource != this)
					{
						throw new ArgumentException(SR.Format(SR.EventSource_EventSourceGuidInUse, this.m_guid));
					}
				}
				for (EventDispatcher eventDispatcher = this.m_Dispatchers; eventDispatcher != null; eventDispatcher = eventDispatcher.m_Next)
				{
					EventDispatcher eventDispatcher2 = eventDispatcher;
					if (eventDispatcher2.m_EventEnabled == null)
					{
						eventDispatcher2.m_EventEnabled = new bool[this.m_eventData.Length];
					}
				}
				this.DefineEventPipeEvents();
			}
		}

		// Token: 0x06005ACC RID: 23244 RVA: 0x001B93D8 File Offset: 0x001B85D8
		private unsafe void SendManifest(byte[] rawManifest)
		{
			if (rawManifest == null)
			{
				return;
			}
			fixed (byte[] array = rawManifest)
			{
				byte* ptr;
				if (rawManifest == null || array.Length == 0)
				{
					ptr = null;
				}
				else
				{
					ptr = &array[0];
				}
				EventDescriptor eventDescriptor = new EventDescriptor(65534, 1, 0, 0, 254, 65534, 72057594037927935L);
				ManifestEnvelope manifestEnvelope = default(ManifestEnvelope);
				manifestEnvelope.Format = ManifestEnvelope.ManifestFormats.SimpleXmlFormat;
				manifestEnvelope.MajorVersion = 1;
				manifestEnvelope.MinorVersion = 0;
				manifestEnvelope.Magic = 91;
				int i = rawManifest.Length;
				manifestEnvelope.ChunkNumber = 0;
				EventProvider.EventData* ptr2 = stackalloc EventProvider.EventData[checked(unchecked((UIntPtr)2) * (UIntPtr)sizeof(EventProvider.EventData))];
				ptr2->Ptr = &manifestEnvelope;
				ptr2->Size = (uint)sizeof(ManifestEnvelope);
				ptr2->Reserved = 0U;
				ptr2[1].Ptr = ptr;
				ptr2[1].Reserved = 0U;
				int num = 65280;
				for (;;)
				{
					IL_C7:
					manifestEnvelope.TotalChunks = (ushort)((i + (num - 1)) / num);
					while (i > 0)
					{
						ptr2[1].Size = (uint)Math.Min(i, num);
						if (this.m_etwProvider != null && !this.m_etwProvider.WriteEvent(ref eventDescriptor, IntPtr.Zero, null, null, 2, (IntPtr)((void*)ptr2)))
						{
							if (EventProvider.GetLastWriteEventError() == EventProvider.WriteEventErrorCode.EventTooBig && manifestEnvelope.ChunkNumber == 0 && num > 256)
							{
								num /= 2;
								goto IL_C7;
							}
							goto IL_142;
						}
						else
						{
							i -= num;
							ptr2[1].Ptr += (ulong)num;
							manifestEnvelope.ChunkNumber += 1;
							if (manifestEnvelope.ChunkNumber % 5 == 0)
							{
								Thread.Sleep(15);
							}
						}
					}
					goto IL_19A;
				}
				IL_142:
				if (this.ThrowOnEventWriteErrors)
				{
					this.ThrowEventSourceException("SendManifest", null);
				}
				IL_19A:;
			}
		}

		// Token: 0x06005ACD RID: 23245 RVA: 0x001B9584 File Offset: 0x001B8784
		internal static Attribute GetCustomAttributeHelper(MemberInfo member, [DynamicallyAccessedMembers((DynamicallyAccessedMemberTypes)515)] Type attributeType, EventManifestOptions flags = EventManifestOptions.None)
		{
			if (!member.Module.Assembly.ReflectionOnly && (flags & EventManifestOptions.AllowEventSourceOverride) == EventManifestOptions.None)
			{
				Attribute result = null;
				object[] customAttributes = member.GetCustomAttributes(attributeType, false);
				int num = 0;
				if (num < customAttributes.Length)
				{
					object obj = customAttributes[num];
					result = (Attribute)obj;
				}
				return result;
			}
			foreach (CustomAttributeData customAttributeData in CustomAttributeData.GetCustomAttributes(member))
			{
				if (EventSource.AttributeTypeNamesMatch(attributeType, customAttributeData.Constructor.ReflectedType))
				{
					Attribute attribute = null;
					if (customAttributeData.ConstructorArguments.Count == 1)
					{
						attribute = (Attribute)Activator.CreateInstance(attributeType, new object[]
						{
							customAttributeData.ConstructorArguments[0].Value
						});
					}
					else if (customAttributeData.ConstructorArguments.Count == 0)
					{
						attribute = (Attribute)Activator.CreateInstance(attributeType);
					}
					if (attribute != null)
					{
						foreach (CustomAttributeNamedArgument customAttributeNamedArgument in customAttributeData.NamedArguments)
						{
							PropertyInfo property = attributeType.GetProperty(customAttributeNamedArgument.MemberInfo.Name, BindingFlags.Instance | BindingFlags.Public);
							object obj2 = customAttributeNamedArgument.TypedValue.Value;
							if (property.PropertyType.IsEnum)
							{
								string value = obj2.ToString();
								obj2 = Enum.Parse(property.PropertyType, value);
							}
							property.SetValue(attribute, obj2, null);
						}
						return attribute;
					}
				}
			}
			return null;
		}

		// Token: 0x06005ACE RID: 23246 RVA: 0x001B9744 File Offset: 0x001B8944
		private static bool AttributeTypeNamesMatch(Type attributeType, Type reflectedAttributeType)
		{
			return attributeType == reflectedAttributeType || string.Equals(attributeType.FullName, reflectedAttributeType.FullName, StringComparison.Ordinal) || (string.Equals(attributeType.Name, reflectedAttributeType.Name, StringComparison.Ordinal) && attributeType.Namespace.EndsWith("Diagnostics.Tracing", StringComparison.Ordinal) && reflectedAttributeType.Namespace.EndsWith("Diagnostics.Tracing", StringComparison.Ordinal));
		}

		// Token: 0x06005ACF RID: 23247 RVA: 0x001B97AC File Offset: 0x001B89AC
		private static Type GetEventSourceBaseType(Type eventSourceType, bool allowEventSourceOverride, bool reflectionOnly)
		{
			Type type = eventSourceType;
			if (type.BaseType == null)
			{
				return null;
			}
			do
			{
				type = type.BaseType;
			}
			while (type != null && type.IsAbstract);
			if (type != null)
			{
				if (!allowEventSourceOverride)
				{
					if ((reflectionOnly && type.FullName != typeof(EventSource).FullName) || (!reflectionOnly && type != typeof(EventSource)))
					{
						return null;
					}
				}
				else if (type.Name != "EventSource")
				{
					return null;
				}
			}
			return type;
		}

		// Token: 0x06005AD0 RID: 23248 RVA: 0x001B983C File Offset: 0x001B8A3C
		private static byte[] CreateManifestAndDescriptors([DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicMethods | DynamicallyAccessedMemberTypes.NonPublicMethods | DynamicallyAccessedMemberTypes.PublicNestedTypes)] Type eventSourceType, string eventSourceDllName, EventSource source, EventManifestOptions flags = EventManifestOptions.None)
		{
			ManifestBuilder manifestBuilder = null;
			bool flag = source == null || !source.SelfDescribingEvents;
			Exception ex = null;
			byte[] result = null;
			if (eventSourceType.IsAbstract && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
			{
				return null;
			}
			try
			{
				MethodInfo[] methods = eventSourceType.GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
				int num = 1;
				EventSource.EventMetadata[] array = null;
				Dictionary<string, string> dictionary = null;
				if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
				{
					array = new EventSource.EventMetadata[methods.Length + 1];
					array[0].Name = "";
				}
				ResourceManager resources = null;
				EventSourceAttribute eventSourceAttribute = (EventSourceAttribute)EventSource.GetCustomAttributeHelper(eventSourceType, typeof(EventSourceAttribute), flags);
				if (eventSourceAttribute != null && eventSourceAttribute.LocalizationResources != null)
				{
					resources = new ResourceManager(eventSourceAttribute.LocalizationResources, eventSourceType.Assembly);
				}
				manifestBuilder = new ManifestBuilder(EventSource.GetName(eventSourceType, flags), EventSource.GetGuid(eventSourceType), eventSourceDllName, resources, flags);
				manifestBuilder.StartEvent("EventSourceMessage", new EventAttribute(0)
				{
					Level = EventLevel.LogAlways,
					Task = (EventTask)65534
				});
				manifestBuilder.AddEventParameter(typeof(string), "message");
				manifestBuilder.EndEvent();
				if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None)
				{
					if (!(EventSource.GetEventSourceBaseType(eventSourceType, (flags & EventManifestOptions.AllowEventSourceOverride) > EventManifestOptions.None, eventSourceType.Assembly.ReflectionOnly) != null))
					{
						manifestBuilder.ManifestError(SR.EventSource_TypeMustDeriveFromEventSource, false);
					}
					if (!eventSourceType.IsAbstract && !eventSourceType.IsSealed)
					{
						manifestBuilder.ManifestError(SR.EventSource_TypeMustBeSealedOrAbstract, false);
					}
				}
				foreach (string text in new string[]
				{
					"Keywords",
					"Tasks",
					"Opcodes"
				})
				{
					Type nestedType = eventSourceType.GetNestedType(text);
					if (nestedType != null)
					{
						if (eventSourceType.IsAbstract)
						{
							manifestBuilder.ManifestError(SR.Format(SR.EventSource_AbstractMustNotDeclareKTOC, nestedType.Name), false);
						}
						else
						{
							foreach (FieldInfo staticField in nestedType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic))
							{
								EventSource.AddProviderEnumKind(manifestBuilder, staticField, text);
							}
						}
					}
				}
				manifestBuilder.AddKeyword("Session3", 17592186044416UL);
				manifestBuilder.AddKeyword("Session2", 35184372088832UL);
				manifestBuilder.AddKeyword("Session1", 70368744177664UL);
				manifestBuilder.AddKeyword("Session0", 140737488355328UL);
				if (eventSourceType != typeof(EventSource))
				{
					foreach (MethodInfo methodInfo in methods)
					{
						ParameterInfo[] parameters = methodInfo.GetParameters();
						EventAttribute eventAttribute = (EventAttribute)EventSource.GetCustomAttributeHelper(methodInfo, typeof(EventAttribute), flags);
						if (!methodInfo.IsStatic)
						{
							if (eventSourceType.IsAbstract)
							{
								if (eventAttribute != null)
								{
									manifestBuilder.ManifestError(SR.Format(SR.EventSource_AbstractMustNotDeclareEventMethods, methodInfo.Name, eventAttribute.EventId), false);
								}
							}
							else
							{
								if (eventAttribute == null)
								{
									if (methodInfo.ReturnType != typeof(void) || methodInfo.IsVirtual || EventSource.GetCustomAttributeHelper(methodInfo, typeof(NonEventAttribute), flags) != null)
									{
										goto IL_5FB;
									}
									EventAttribute eventAttribute2 = new EventAttribute(num);
									eventAttribute = eventAttribute2;
								}
								else if (eventAttribute.EventId <= 0)
								{
									manifestBuilder.ManifestError(SR.Format(SR.EventSource_NeedPositiveId, methodInfo.Name), true);
									goto IL_5FB;
								}
								if (methodInfo.Name.LastIndexOf('.') >= 0)
								{
									manifestBuilder.ManifestError(SR.Format(SR.EventSource_EventMustNotBeExplicitImplementation, methodInfo.Name, eventAttribute.EventId), false);
								}
								num++;
								string name = methodInfo.Name;
								if (eventAttribute.Opcode == EventOpcode.Info)
								{
									bool flag2 = eventAttribute.Task == EventTask.None;
									if (flag2)
									{
										eventAttribute.Task = (EventTask)65534 - eventAttribute.EventId;
									}
									if (!eventAttribute.IsOpcodeSet)
									{
										eventAttribute.Opcode = EventSource.GetOpcodeWithDefault(EventOpcode.Info, name);
									}
									if (flag2)
									{
										if (eventAttribute.Opcode == EventOpcode.Start)
										{
											string text2 = name.Substring(0, name.Length - "Start".Length);
											if (string.Compare(name, 0, text2, 0, text2.Length) == 0 && string.Compare(name, text2.Length, "Start", 0, Math.Max(name.Length - text2.Length, "Start".Length)) == 0)
											{
												manifestBuilder.AddTask(text2, (int)eventAttribute.Task);
											}
										}
										else if (eventAttribute.Opcode == EventOpcode.Stop)
										{
											int num2 = eventAttribute.EventId - 1;
											if (array != null && num2 < array.Length)
											{
												EventSource.EventMetadata eventMetadata = array[num2];
												string text3 = name.Substring(0, name.Length - "Stop".Length);
												if (eventMetadata.Descriptor.Opcode == 1 && string.Compare(eventMetadata.Name, 0, text3, 0, text3.Length) == 0 && string.Compare(eventMetadata.Name, text3.Length, "Start", 0, Math.Max(eventMetadata.Name.Length - text3.Length, "Start".Length)) == 0)
												{
													eventAttribute.Task = (EventTask)eventMetadata.Descriptor.Task;
													flag2 = false;
												}
											}
											if (flag2 && (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
											{
												throw new ArgumentException(SR.EventSource_StopsFollowStarts);
											}
										}
									}
								}
								bool hasRelatedActivityID = EventSource.RemoveFirstArgIfRelatedActivityId(ref parameters);
								if (source == null || !source.SelfDescribingEvents)
								{
									manifestBuilder.StartEvent(name, eventAttribute);
									for (int l = 0; l < parameters.Length; l++)
									{
										manifestBuilder.AddEventParameter(parameters[l].ParameterType, parameters[l].Name);
									}
									manifestBuilder.EndEvent();
								}
								if (source != null || (flags & EventManifestOptions.Strict) != EventManifestOptions.None)
								{
									EventSource.DebugCheckEvent(ref dictionary, array, methodInfo, eventAttribute, manifestBuilder, flags);
									if (eventAttribute.Channel != EventChannel.None)
									{
										eventAttribute.Keywords |= (EventKeywords)manifestBuilder.GetChannelKeyword(eventAttribute.Channel, (ulong)eventAttribute.Keywords);
									}
									string key = "event_" + name;
									string localizedMessage = manifestBuilder.GetLocalizedMessage(key, CultureInfo.CurrentUICulture, false);
									if (localizedMessage != null)
									{
										eventAttribute.Message = localizedMessage;
									}
									EventSource.AddEventDescriptor(ref array, name, eventAttribute, parameters, hasRelatedActivityID);
								}
							}
						}
						IL_5FB:;
					}
				}
				NameInfo.ReserveEventIDsBelow(num);
				if (source != null)
				{
					EventSource.TrimEventDescriptors(ref array);
					source.m_eventData = array;
					source.m_channelData = manifestBuilder.GetChannelData();
				}
				if (!eventSourceType.IsAbstract && (source == null || !source.SelfDescribingEvents))
				{
					flag = ((flags & EventManifestOptions.OnlyIfNeededForRegistration) == EventManifestOptions.None || manifestBuilder.GetChannelData().Length != 0);
					if (!flag && (flags & EventManifestOptions.Strict) == EventManifestOptions.None)
					{
						return null;
					}
					result = manifestBuilder.CreateManifest();
				}
			}
			catch (Exception ex2)
			{
				if ((flags & EventManifestOptions.Strict) == EventManifestOptions.None)
				{
					throw;
				}
				ex = ex2;
			}
			if ((flags & EventManifestOptions.Strict) != EventManifestOptions.None && ((manifestBuilder != null && manifestBuilder.Errors.Count > 0) || ex != null))
			{
				string text4 = string.Empty;
				if (manifestBuilder != null && manifestBuilder.Errors.Count > 0)
				{
					bool flag3 = true;
					using (IEnumerator<string> enumerator = manifestBuilder.Errors.GetEnumerator())
					{
						while (enumerator.MoveNext())
						{
							string str = enumerator.Current;
							if (!flag3)
							{
								text4 += Environment.NewLine;
							}
							flag3 = false;
							text4 += str;
						}
						goto IL_71D;
					}
				}
				text4 = "Unexpected error: " + ex.Message;
				IL_71D:
				throw new ArgumentException(text4, ex);
			}
			if (!flag)
			{
				return null;
			}
			return result;
		}

		// Token: 0x06005AD1 RID: 23249 RVA: 0x001B9FAC File Offset: 0x001B91AC
		private static bool RemoveFirstArgIfRelatedActivityId(ref ParameterInfo[] args)
		{
			if (args.Length != 0 && args[0].ParameterType == typeof(Guid) && string.Equals(args[0].Name, "relatedActivityId", StringComparison.OrdinalIgnoreCase))
			{
				ParameterInfo[] array = new ParameterInfo[args.Length - 1];
				Array.Copy(args, 1, array, 0, args.Length - 1);
				args = array;
				return true;
			}
			return false;
		}

		// Token: 0x06005AD2 RID: 23250 RVA: 0x001BA010 File Offset: 0x001B9210
		private static void AddProviderEnumKind(ManifestBuilder manifest, FieldInfo staticField, string providerEnumKind)
		{
			bool reflectionOnly = staticField.Module.Assembly.ReflectionOnly;
			Type fieldType = staticField.FieldType;
			if ((!reflectionOnly && fieldType == typeof(EventOpcode)) || EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventOpcode)))
			{
				if (!(providerEnumKind != "Opcodes"))
				{
					int value = (int)staticField.GetRawConstantValue();
					manifest.AddOpcode(staticField.Name, value);
					return;
				}
			}
			else
			{
				if ((reflectionOnly || !(fieldType == typeof(EventTask))) && !EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventTask)))
				{
					if ((!reflectionOnly && fieldType == typeof(EventKeywords)) || EventSource.AttributeTypeNamesMatch(fieldType, typeof(EventKeywords)))
					{
						if (providerEnumKind != "Keywords")
						{
							goto IL_107;
						}
						ulong value2 = (ulong)((long)staticField.GetRawConstantValue());
						manifest.AddKeyword(staticField.Name, value2);
					}
					return;
				}
				if (!(providerEnumKind != "Tasks"))
				{
					int value3 = (int)staticField.GetRawConstantValue();
					manifest.AddTask(staticField.Name, value3);
					return;
				}
			}
			IL_107:
			manifest.ManifestError(SR.Format(SR.EventSource_EnumKindMismatch, staticField.Name, staticField.FieldType.Name, providerEnumKind), false);
		}

		// Token: 0x06005AD3 RID: 23251 RVA: 0x001BA148 File Offset: 0x001B9348
		private static void AddEventDescriptor([NotNull] ref EventSource.EventMetadata[] eventData, string eventName, EventAttribute eventAttribute, ParameterInfo[] eventParameters, bool hasRelatedActivityID)
		{
			if (eventData.Length <= eventAttribute.EventId)
			{
				EventSource.EventMetadata[] array = new EventSource.EventMetadata[Math.Max(eventData.Length + 16, eventAttribute.EventId + 1)];
				Array.Copy(eventData, array, eventData.Length);
				eventData = array;
			}
			eventData[eventAttribute.EventId].Descriptor = new EventDescriptor(eventAttribute.EventId, eventAttribute.Version, (byte)eventAttribute.Channel, (byte)eventAttribute.Level, (byte)eventAttribute.Opcode, (int)eventAttribute.Task, (long)(eventAttribute.Keywords | (EventKeywords)SessionMask.All.ToEventKeywords()));
			eventData[eventAttribute.EventId].Tags = eventAttribute.Tags;
			eventData[eventAttribute.EventId].Name = eventName;
			eventData[eventAttribute.EventId].Parameters = eventParameters;
			eventData[eventAttribute.EventId].Message = eventAttribute.Message;
			eventData[eventAttribute.EventId].ActivityOptions = eventAttribute.ActivityOptions;
			eventData[eventAttribute.EventId].HasRelatedActivityID = hasRelatedActivityID;
			eventData[eventAttribute.EventId].EventHandle = IntPtr.Zero;
		}

		// Token: 0x06005AD4 RID: 23252 RVA: 0x001BA274 File Offset: 0x001B9474
		private static void TrimEventDescriptors(ref EventSource.EventMetadata[] eventData)
		{
			int num = eventData.Length;
			while (0 < num)
			{
				num--;
				if (eventData[num].Descriptor.EventId != 0)
				{
					break;
				}
			}
			if (eventData.Length - num > 2)
			{
				EventSource.EventMetadata[] array = new EventSource.EventMetadata[num + 1];
				Array.Copy(eventData, array, array.Length);
				eventData = array;
			}
		}

		// Token: 0x06005AD5 RID: 23253 RVA: 0x001BA2C4 File Offset: 0x001B94C4
		internal void AddListener(EventListener listener)
		{
			object eventListenersLock = EventListener.EventListenersLock;
			lock (eventListenersLock)
			{
				bool[] eventEnabled = null;
				if (this.m_eventData != null)
				{
					eventEnabled = new bool[this.m_eventData.Length];
				}
				this.m_Dispatchers = new EventDispatcher(this.m_Dispatchers, eventEnabled, listener);
				listener.OnEventSourceCreated(this);
			}
		}

		// Token: 0x06005AD6 RID: 23254 RVA: 0x001BA338 File Offset: 0x001B9538
		private static void DebugCheckEvent(ref Dictionary<string, string> eventsByName, EventSource.EventMetadata[] eventData, MethodInfo method, EventAttribute eventAttribute, ManifestBuilder manifest, EventManifestOptions options)
		{
			int eventId = eventAttribute.EventId;
			string name = method.Name;
			int helperCallFirstArg = EventSource.GetHelperCallFirstArg(method);
			if (helperCallFirstArg >= 0 && eventId != helperCallFirstArg)
			{
				manifest.ManifestError(SR.Format(SR.EventSource_MismatchIdToWriteEvent, name, eventId, helperCallFirstArg), true);
			}
			if (eventId < eventData.Length && eventData[eventId].Descriptor.EventId != 0)
			{
				manifest.ManifestError(SR.Format(SR.EventSource_EventIdReused, name, eventId, eventData[eventId].Name), true);
			}
			for (int i = 0; i < eventData.Length; i++)
			{
				if (eventData[i].Name != null && eventData[i].Descriptor.Task == (int)eventAttribute.Task && (EventOpcode)eventData[i].Descriptor.Opcode == eventAttribute.Opcode)
				{
					manifest.ManifestError(SR.Format(SR.EventSource_TaskOpcodePairReused, new object[]
					{
						name,
						eventId,
						eventData[i].Name,
						i
					}), false);
					if ((options & EventManifestOptions.Strict) == EventManifestOptions.None)
					{
						break;
					}
				}
			}
			if (eventAttribute.Opcode != EventOpcode.Info)
			{
				bool flag = false;
				if (eventAttribute.Task == EventTask.None)
				{
					flag = true;
				}
				else
				{
					EventTask eventTask = (EventTask)65534 - eventId;
					if (eventAttribute.Opcode != EventOpcode.Start && eventAttribute.Opcode != EventOpcode.Stop && eventAttribute.Task == eventTask)
					{
						flag = true;
					}
				}
				if (flag)
				{
					manifest.ManifestError(SR.Format(SR.EventSource_EventMustHaveTaskIfNonDefaultOpcode, name, eventId), false);
				}
			}
			if (eventsByName == null)
			{
				eventsByName = new Dictionary<string, string>();
			}
			if (eventsByName.ContainsKey(name))
			{
				manifest.ManifestError(SR.Format(SR.EventSource_EventNameReused, name), true);
			}
			eventsByName[name] = name;
		}

		// Token: 0x06005AD7 RID: 23255 RVA: 0x001BA4E4 File Offset: 0x001B96E4
		private static int GetHelperCallFirstArg(MethodInfo method)
		{
			byte[] ilasByteArray = method.GetMethodBody().GetILAsByteArray();
			int num = -1;
			for (int i = 0; i < ilasByteArray.Length; i++)
			{
				byte b = ilasByteArray[i];
				if (b <= 110)
				{
					switch (b)
					{
					case 0:
					case 1:
					case 2:
					case 3:
					case 4:
					case 5:
					case 6:
					case 7:
					case 8:
					case 9:
					case 10:
					case 11:
					case 12:
					case 13:
					case 20:
					case 37:
						break;
					case 14:
					case 16:
						i++;
						break;
					case 15:
					case 17:
					case 18:
					case 19:
					case 33:
					case 34:
					case 35:
					case 36:
					case 38:
					case 39:
					case 41:
					case 42:
					case 43:
					case 46:
					case 47:
					case 48:
					case 49:
					case 50:
					case 51:
					case 52:
					case 53:
					case 54:
					case 55:
					case 56:
						return -1;
					case 21:
					case 22:
					case 23:
					case 24:
					case 25:
					case 26:
					case 27:
					case 28:
					case 29:
					case 30:
						if (i > 0 && ilasByteArray[i - 1] == 2)
						{
							num = (int)(ilasByteArray[i] - 22);
						}
						break;
					case 31:
						if (i > 0 && ilasByteArray[i - 1] == 2)
						{
							num = (int)ilasByteArray[i + 1];
						}
						i++;
						break;
					case 32:
						i += 4;
						break;
					case 40:
						i += 4;
						if (num >= 0)
						{
							for (int j = i + 1; j < ilasByteArray.Length; j++)
							{
								if (ilasByteArray[j] == 42)
								{
									return num;
								}
								if (ilasByteArray[j] != 0)
								{
									break;
								}
							}
						}
						num = -1;
						break;
					case 44:
					case 45:
						num = -1;
						i++;
						break;
					case 57:
					case 58:
						num = -1;
						i += 4;
						break;
					default:
						if (b - 103 > 3 && b - 109 > 1)
						{
							return -1;
						}
						break;
					}
				}
				else if (b - 140 > 1)
				{
					if (b != 162)
					{
						if (b != 254)
						{
							return -1;
						}
						i++;
						if (i >= ilasByteArray.Length || ilasByteArray[i] >= 6)
						{
							return -1;
						}
					}
				}
				else
				{
					i += 4;
				}
			}
			return -1;
		}

		// Token: 0x06005AD8 RID: 23256 RVA: 0x001BA6EC File Offset: 0x001B98EC
		internal void ReportOutOfBandMessage(string msg)
		{
			try
			{
				if (this.m_outOfBandMessageCount < 15)
				{
					this.m_outOfBandMessageCount += 1;
				}
				else
				{
					if (this.m_outOfBandMessageCount == 16)
					{
						return;
					}
					this.m_outOfBandMessageCount = 16;
					msg = "Reached message limit.   End of EventSource error messages.";
				}
				Debugger.Log(0, null, string.Format("EventSource Error: {0}{1}", msg, Environment.NewLine));
				this.WriteEventString(msg);
				this.WriteStringToAllListeners("EventSourceMessage", msg);
			}
			catch
			{
			}
		}

		// Token: 0x06005AD9 RID: 23257 RVA: 0x001BA770 File Offset: 0x001B9970
		private EventSourceSettings ValidateSettings(EventSourceSettings settings)
		{
			if ((settings & (EventSourceSettings.EtwManifestEventFormat | EventSourceSettings.EtwSelfDescribingEventFormat)) == (EventSourceSettings.EtwManifestEventFormat | EventSourceSettings.EtwSelfDescribingEventFormat))
			{
				throw new ArgumentException(SR.EventSource_InvalidEventFormat, "settings");
			}
			if ((settings & (EventSourceSettings.EtwManifestEventFormat | EventSourceSettings.EtwSelfDescribingEventFormat)) == EventSourceSettings.Default)
			{
				settings |= EventSourceSettings.EtwSelfDescribingEventFormat;
			}
			return settings;
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06005ADA RID: 23258 RVA: 0x001BA796 File Offset: 0x001B9996
		private bool ThrowOnEventWriteErrors
		{
			get
			{
				return (this.m_config & EventSourceSettings.ThrowOnEventWriteErrors) > EventSourceSettings.Default;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06005ADB RID: 23259 RVA: 0x001BA7A3 File Offset: 0x001B99A3
		private bool SelfDescribingEvents
		{
			get
			{
				return (this.m_config & EventSourceSettings.EtwSelfDescribingEventFormat) > EventSourceSettings.Default;
			}
		}

		// Token: 0x06005ADC RID: 23260 RVA: 0x001BA7B0 File Offset: 0x001B99B0
		[NullableContext(1)]
		public EventSource(string eventSourceName) : this(eventSourceName, EventSourceSettings.EtwSelfDescribingEventFormat)
		{
		}

		// Token: 0x06005ADD RID: 23261 RVA: 0x001BA7BA File Offset: 0x001B99BA
		[NullableContext(1)]
		public EventSource(string eventSourceName, EventSourceSettings config) : this(eventSourceName, config, null)
		{
		}

		// Token: 0x06005ADE RID: 23262 RVA: 0x001BA7C8 File Offset: 0x001B99C8
		[NullableContext(1)]
		public EventSource(string eventSourceName, EventSourceSettings config, [Nullable(new byte[]
		{
			2,
			1
		})] params string[] traits) : this((eventSourceName == null) ? default(Guid) : EventSource.GenerateGuidFromName(eventSourceName.ToUpperInvariant()), eventSourceName, config, traits)
		{
			if (eventSourceName == null)
			{
				throw new ArgumentNullException("eventSourceName");
			}
		}

		// Token: 0x06005ADF RID: 23263 RVA: 0x001BA808 File Offset: 0x001B9A08
		public void Write(string eventName)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.WriteImpl(eventName, ref eventSourceOptions, null, null, null, SimpleEventTypes<EmptyStruct>.Instance);
		}

		// Token: 0x06005AE0 RID: 23264 RVA: 0x001BA839 File Offset: 0x001B9A39
		public void Write(string eventName, EventSourceOptions options)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			this.WriteImpl(eventName, ref options, null, null, null, SimpleEventTypes<EmptyStruct>.Instance);
		}

		// Token: 0x06005AE1 RID: 23265 RVA: 0x001BA858 File Offset: 0x001B9A58
		public void Write<T>(string eventName, [Nullable(1)] T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			EventSourceOptions eventSourceOptions = default(EventSourceOptions);
			this.WriteImpl(eventName, ref eventSourceOptions, data, null, null, SimpleEventTypes<T>.Instance);
		}

		// Token: 0x06005AE2 RID: 23266 RVA: 0x001BA88E File Offset: 0x001B9A8E
		public void Write<T>(string eventName, EventSourceOptions options, [Nullable(1)] T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			this.WriteImpl(eventName, ref options, data, null, null, SimpleEventTypes<T>.Instance);
		}

		// Token: 0x06005AE3 RID: 23267 RVA: 0x001BA8B1 File Offset: 0x001B9AB1
		public void Write<T>(string eventName, ref EventSourceOptions options, [Nullable(1)] ref T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			this.WriteImpl(eventName, ref options, data, null, null, SimpleEventTypes<T>.Instance);
		}

		// Token: 0x06005AE4 RID: 23268 RVA: 0x001BA8D8 File Offset: 0x001B9AD8
		public unsafe void Write<T>(string eventName, ref EventSourceOptions options, ref Guid activityId, ref Guid relatedActivityId, [Nullable(1)] ref T data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			fixed (Guid* ptr = &activityId)
			{
				Guid* pActivityId = ptr;
				fixed (Guid* ptr2 = &relatedActivityId)
				{
					Guid* ptr3 = ptr2;
					this.WriteImpl(eventName, ref options, data, pActivityId, (relatedActivityId == Guid.Empty) ? null : ptr3, SimpleEventTypes<T>.Instance);
					ptr = null;
				}
				return;
			}
		}

		// Token: 0x06005AE5 RID: 23269 RVA: 0x001BA934 File Offset: 0x001B9B34
		private unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			byte level = ((options.valuesSet & 4) != 0) ? options.level : eventTypes.level;
			EventKeywords keywords = ((options.valuesSet & 1) != 0) ? options.keywords : eventTypes.keywords;
			if (this.IsEnabled((EventLevel)level, keywords))
			{
				this.WriteMultiMergeInner(eventName, ref options, eventTypes, activityID, childActivityID, values);
			}
		}

		// Token: 0x06005AE6 RID: 23270 RVA: 0x001BA998 File Offset: 0x001B9B98
		private unsafe void WriteMultiMergeInner(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, params object[] values)
		{
			byte level = ((options.valuesSet & 4) != 0) ? options.level : eventTypes.level;
			byte opcode = ((options.valuesSet & 8) != 0) ? options.opcode : eventTypes.opcode;
			EventTags tags = ((options.valuesSet & 2) != 0) ? options.tags : eventTypes.Tags;
			EventKeywords keywords = ((options.valuesSet & 1) != 0) ? options.keywords : eventTypes.keywords;
			NameInfo nameInfo = eventTypes.GetNameInfo(eventName ?? eventTypes.Name, tags);
			if (nameInfo == null)
			{
				return;
			}
			int identity = nameInfo.identity;
			EventDescriptor descriptor = new EventDescriptor(identity, level, opcode, (long)keywords);
			IntPtr orCreateEventHandle = nameInfo.GetOrCreateEventHandle(this.m_eventPipeProvider, this.m_eventHandleTable, descriptor, eventTypes);
			int pinCount = eventTypes.pinCount;
			byte* scratch = stackalloc byte[(UIntPtr)eventTypes.scratchSize];
			EventSource.EventData* ptr = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)(eventTypes.dataCount + 3)) * (UIntPtr)sizeof(EventSource.EventData))];
			for (int i = 0; i < eventTypes.dataCount + 3; i++)
			{
				ptr[i] = default(EventSource.EventData);
			}
			GCHandle* ptr2 = stackalloc GCHandle[checked(unchecked((UIntPtr)pinCount) * (UIntPtr)sizeof(GCHandle))];
			for (int j = 0; j < pinCount; j++)
			{
				ptr2[j] = default(GCHandle);
			}
			byte[] array;
			byte* pointer;
			if ((array = this.providerMetadata) == null || array.Length == 0)
			{
				pointer = null;
			}
			else
			{
				pointer = &array[0];
			}
			byte[] array2;
			byte* pointer2;
			if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
			{
				pointer2 = null;
			}
			else
			{
				pointer2 = &array2[0];
			}
			byte[] array3;
			byte* pointer3;
			if ((array3 = eventTypes.typeMetadata) == null || array3.Length == 0)
			{
				pointer3 = null;
			}
			else
			{
				pointer3 = &array3[0];
			}
			ptr->SetMetadata(pointer, this.providerMetadata.Length, 2);
			ptr[1].SetMetadata(pointer2, nameInfo.nameMetadata.Length, 1);
			ptr[2].SetMetadata(pointer3, eventTypes.typeMetadata.Length, 1);
			try
			{
				DataCollector.ThreadInstance.Enable(scratch, eventTypes.scratchSize, ptr + 3, eventTypes.dataCount, ptr2, pinCount);
				for (int k = 0; k < eventTypes.typeInfos.Length; k++)
				{
					TraceLoggingTypeInfo traceLoggingTypeInfo = eventTypes.typeInfos[k];
					traceLoggingTypeInfo.WriteData(TraceLoggingDataCollector.Instance, traceLoggingTypeInfo.PropertyValueFactory(values[k]));
				}
				this.WriteEventRaw(eventName, ref descriptor, orCreateEventHandle, activityID, childActivityID, (int)((long)(DataCollector.ThreadInstance.Finish() - ptr)), (IntPtr)((void*)ptr));
			}
			finally
			{
				EventSource.WriteCleanup(ptr2, pinCount);
			}
			array = null;
			array2 = null;
			array3 = null;
		}

		// Token: 0x06005AE7 RID: 23271 RVA: 0x001BAC40 File Offset: 0x001B9E40
		internal unsafe void WriteMultiMerge(string eventName, ref EventSourceOptions options, TraceLoggingEventTypes eventTypes, Guid* activityID, Guid* childActivityID, EventSource.EventData* data)
		{
			if (!this.IsEnabled())
			{
				return;
			}
			fixed (EventSourceOptions* ptr = &options)
			{
				EventSourceOptions* ptr2 = ptr;
				EventDescriptor descriptor;
				NameInfo nameInfo = this.UpdateDescriptor(eventName, eventTypes, ref options, out descriptor);
				if (nameInfo == null)
				{
					return;
				}
				IntPtr orCreateEventHandle = nameInfo.GetOrCreateEventHandle(this.m_eventPipeProvider, this.m_eventHandleTable, descriptor, eventTypes);
				int num = eventTypes.dataCount + eventTypes.typeInfos.Length * 2 + 3;
				EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)num) * (UIntPtr)sizeof(EventSource.EventData))];
				for (int i = 0; i < num; i++)
				{
					ptr3[i] = default(EventSource.EventData);
				}
				byte[] array;
				byte* pointer;
				if ((array = this.providerMetadata) == null || array.Length == 0)
				{
					pointer = null;
				}
				else
				{
					pointer = &array[0];
				}
				byte[] array2;
				byte* pointer2;
				if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
				{
					pointer2 = null;
				}
				else
				{
					pointer2 = &array2[0];
				}
				byte[] array3;
				byte* pointer3;
				if ((array3 = eventTypes.typeMetadata) == null || array3.Length == 0)
				{
					pointer3 = null;
				}
				else
				{
					pointer3 = &array3[0];
				}
				ptr3->SetMetadata(pointer, this.providerMetadata.Length, 2);
				ptr3[1].SetMetadata(pointer2, nameInfo.nameMetadata.Length, 1);
				ptr3[2].SetMetadata(pointer3, eventTypes.typeMetadata.Length, 1);
				int num2 = 3;
				for (int j = 0; j < eventTypes.typeInfos.Length; j++)
				{
					ptr3[num2].m_Ptr = data[j].m_Ptr;
					ptr3[num2].m_Size = data[j].m_Size;
					if (data[j].m_Size == 4 && eventTypes.typeInfos[j].DataType == typeof(bool))
					{
						ptr3[num2].m_Size = 1;
					}
					num2++;
				}
				this.WriteEventRaw(eventName, ref descriptor, orCreateEventHandle, activityID, childActivityID, num2, (IntPtr)((void*)ptr3));
				array = null;
				array2 = null;
				array3 = null;
			}
		}

		// Token: 0x06005AE8 RID: 23272 RVA: 0x001BAE50 File Offset: 0x001BA050
		private unsafe void WriteImpl(string eventName, ref EventSourceOptions options, object data, Guid* pActivityId, Guid* pRelatedActivityId, TraceLoggingEventTypes eventTypes)
		{
			try
			{
				try
				{
					fixed (EventSourceOptions* ptr = &options)
					{
						EventSourceOptions* ptr2 = ptr;
						options.Opcode = (options.IsOpcodeSet ? options.Opcode : EventSource.GetOpcodeWithDefault(options.Opcode, eventName));
						EventDescriptor descriptor;
						NameInfo nameInfo = this.UpdateDescriptor(eventName, eventTypes, ref options, out descriptor);
						if (nameInfo != null)
						{
							IntPtr orCreateEventHandle = nameInfo.GetOrCreateEventHandle(this.m_eventPipeProvider, this.m_eventHandleTable, descriptor, eventTypes);
							int pinCount = eventTypes.pinCount;
							byte* scratch = stackalloc byte[(UIntPtr)eventTypes.scratchSize];
							EventSource.EventData* ptr3 = stackalloc EventSource.EventData[checked(unchecked((UIntPtr)(eventTypes.dataCount + 3)) * (UIntPtr)sizeof(EventSource.EventData))];
							for (int i = 0; i < eventTypes.dataCount + 3; i++)
							{
								ptr3[i] = default(EventSource.EventData);
							}
							GCHandle* ptr4 = stackalloc GCHandle[checked(unchecked((UIntPtr)pinCount) * (UIntPtr)sizeof(GCHandle))];
							for (int j = 0; j < pinCount; j++)
							{
								ptr4[j] = default(GCHandle);
							}
							try
							{
								byte[] array;
								byte* pointer;
								if ((array = this.providerMetadata) == null || array.Length == 0)
								{
									pointer = null;
								}
								else
								{
									pointer = &array[0];
								}
								byte[] array2;
								byte* pointer2;
								if ((array2 = nameInfo.nameMetadata) == null || array2.Length == 0)
								{
									pointer2 = null;
								}
								else
								{
									pointer2 = &array2[0];
								}
								byte[] array3;
								byte* pointer3;
								if ((array3 = eventTypes.typeMetadata) == null || array3.Length == 0)
								{
									pointer3 = null;
								}
								else
								{
									pointer3 = &array3[0];
								}
								ptr3->SetMetadata(pointer, this.providerMetadata.Length, 2);
								ptr3[1].SetMetadata(pointer2, nameInfo.nameMetadata.Length, 1);
								ptr3[2].SetMetadata(pointer3, eventTypes.typeMetadata.Length, 1);
								EventOpcode opcode = (EventOpcode)descriptor.Opcode;
								Guid empty = Guid.Empty;
								Guid empty2 = Guid.Empty;
								if (pActivityId == null && pRelatedActivityId == null && (options.ActivityOptions & EventActivityOptions.Disable) == EventActivityOptions.None)
								{
									if (opcode == EventOpcode.Start)
									{
										this.m_activityTracker.OnStart(this.m_name, eventName, 0, ref empty, ref empty2, options.ActivityOptions, true);
									}
									else if (opcode == EventOpcode.Stop)
									{
										this.m_activityTracker.OnStop(this.m_name, eventName, 0, ref empty, true);
									}
									if (empty != Guid.Empty)
									{
										pActivityId = &empty;
									}
									if (empty2 != Guid.Empty)
									{
										pRelatedActivityId = &empty2;
									}
								}
								try
								{
									DataCollector.ThreadInstance.Enable(scratch, eventTypes.scratchSize, ptr3 + 3, eventTypes.dataCount, ptr4, pinCount);
									TraceLoggingTypeInfo traceLoggingTypeInfo = eventTypes.typeInfos[0];
									traceLoggingTypeInfo.WriteData(TraceLoggingDataCollector.Instance, traceLoggingTypeInfo.PropertyValueFactory(data));
									this.WriteEventRaw(eventName, ref descriptor, orCreateEventHandle, pActivityId, pRelatedActivityId, (int)((long)(DataCollector.ThreadInstance.Finish() - ptr3)), (IntPtr)((void*)ptr3));
									if (this.m_Dispatchers != null)
									{
										EventPayload payload = (EventPayload)eventTypes.typeInfos[0].GetData(data);
										this.WriteToAllListeners(eventName, ref descriptor, nameInfo.tags, pActivityId, pRelatedActivityId, payload);
									}
								}
								catch (Exception ex)
								{
									if (ex is EventSourceException)
									{
										throw;
									}
									this.ThrowEventSourceException(eventName, ex);
								}
								finally
								{
									EventSource.WriteCleanup(ptr4, pinCount);
								}
							}
							finally
							{
								byte[] array = null;
								byte[] array2 = null;
								byte[] array3 = null;
							}
						}
					}
				}
				finally
				{
					EventSourceOptions* ptr = null;
				}
			}
			catch (Exception ex2)
			{
				if (ex2 is EventSourceException)
				{
					throw;
				}
				this.ThrowEventSourceException(eventName, ex2);
			}
		}

		// Token: 0x06005AE9 RID: 23273 RVA: 0x001BB1FC File Offset: 0x001BA3FC
		private unsafe void WriteToAllListeners(string eventName, ref EventDescriptor eventDescriptor, EventTags tags, Guid* pActivityId, Guid* pChildActivityId, EventPayload payload)
		{
			EventWrittenEventArgs eventWrittenEventArgs = new EventWrittenEventArgs(this);
			eventWrittenEventArgs.EventName = eventName;
			eventWrittenEventArgs.m_level = (EventLevel)eventDescriptor.Level;
			eventWrittenEventArgs.m_keywords = (EventKeywords)eventDescriptor.Keywords;
			eventWrittenEventArgs.m_opcode = (EventOpcode)eventDescriptor.Opcode;
			eventWrittenEventArgs.m_tags = tags;
			eventWrittenEventArgs.EventId = -1;
			if (pActivityId != null)
			{
				eventWrittenEventArgs.ActivityId = *pActivityId;
			}
			if (pChildActivityId != null)
			{
				eventWrittenEventArgs.RelatedActivityId = *pChildActivityId;
			}
			if (payload != null)
			{
				eventWrittenEventArgs.Payload = new ReadOnlyCollection<object>((IList<object>)payload.Values);
				eventWrittenEventArgs.PayloadNames = new ReadOnlyCollection<string>((IList<string>)payload.Keys);
			}
			this.DispatchToAllListeners(-1, eventWrittenEventArgs);
		}

		// Token: 0x06005AEA RID: 23274 RVA: 0x001BB2AC File Offset: 0x001BA4AC
		[NonEvent]
		private unsafe static void WriteCleanup(GCHandle* pPins, int cPins)
		{
			DataCollector.ThreadInstance.Disable();
			for (int i = 0; i < cPins; i++)
			{
				if (pPins[i].IsAllocated)
				{
					pPins[i].Free();
				}
			}
		}

		// Token: 0x06005AEB RID: 23275 RVA: 0x001BB2F4 File Offset: 0x001BA4F4
		private void InitializeProviderMetadata()
		{
			if (this.m_traits != null)
			{
				List<byte> list = new List<byte>(100);
				for (int i = 0; i < this.m_traits.Length - 1; i += 2)
				{
					if (this.m_traits[i].StartsWith("ETW_", StringComparison.Ordinal))
					{
						string text = this.m_traits[i].Substring(4);
						byte item;
						if (!byte.TryParse(text, out item))
						{
							if (!(text == "GROUP"))
							{
								throw new ArgumentException(SR.Format(SR.EventSource_UnknownEtwTrait, text), "traits");
							}
							item = 1;
						}
						string value = this.m_traits[i + 1];
						int count = list.Count;
						list.Add(0);
						list.Add(0);
						list.Add(item);
						int num = EventSource.AddValueToMetaData(list, value) + 3;
						list[count] = (byte)num;
						list[count + 1] = (byte)(num >> 8);
					}
				}
				this.providerMetadata = Statics.MetadataForString(this.Name, 0, list.Count, 0);
				int num2 = this.providerMetadata.Length - list.Count;
				using (List<byte>.Enumerator enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						byte b = enumerator.Current;
						this.providerMetadata[num2++] = b;
					}
					return;
				}
			}
			this.providerMetadata = Statics.MetadataForString(this.Name, 0, 0, 0);
		}

		// Token: 0x06005AEC RID: 23276 RVA: 0x001BB460 File Offset: 0x001BA660
		private static int AddValueToMetaData(List<byte> metaData, string value)
		{
			if (value.Length == 0)
			{
				return 0;
			}
			int count = metaData.Count;
			char c = value[0];
			if (c == '@')
			{
				metaData.AddRange(Encoding.UTF8.GetBytes(value.Substring(1)));
			}
			else if (c == '{')
			{
				metaData.AddRange(new Guid(value).ToByteArray());
			}
			else if (c == '#')
			{
				for (int i = 1; i < value.Length; i++)
				{
					if (value[i] != ' ')
					{
						if (i + 1 >= value.Length)
						{
							throw new ArgumentException(SR.EventSource_EvenHexDigits, "traits");
						}
						metaData.Add((byte)(EventSource.HexDigit(value[i]) * 16 + EventSource.HexDigit(value[i + 1])));
						i++;
					}
				}
			}
			else
			{
				if ('A' > c && ' ' != c)
				{
					throw new ArgumentException(SR.Format(SR.EventSource_IllegalValue, value), "traits");
				}
				metaData.AddRange(Encoding.UTF8.GetBytes(value));
			}
			return metaData.Count - count;
		}

		// Token: 0x06005AED RID: 23277 RVA: 0x001BB568 File Offset: 0x001BA768
		private static int HexDigit(char c)
		{
			if ('0' <= c && c <= '9')
			{
				return (int)(c - '0');
			}
			if ('a' <= c)
			{
				c -= ' ';
			}
			if ('A' <= c && c <= 'F')
			{
				return (int)(c - 'A' + '\n');
			}
			throw new ArgumentException(SR.Format(SR.EventSource_BadHexDigit, c), "traits");
		}

		// Token: 0x06005AEE RID: 23278 RVA: 0x001BB5BC File Offset: 0x001BA7BC
		private NameInfo UpdateDescriptor(string name, TraceLoggingEventTypes eventInfo, ref EventSourceOptions options, out EventDescriptor descriptor)
		{
			NameInfo nameInfo = null;
			int traceloggingId = 0;
			byte level = ((options.valuesSet & 4) != 0) ? options.level : eventInfo.level;
			byte opcode = ((options.valuesSet & 8) != 0) ? options.opcode : eventInfo.opcode;
			EventTags tags = ((options.valuesSet & 2) != 0) ? options.tags : eventInfo.Tags;
			EventKeywords keywords = ((options.valuesSet & 1) != 0) ? options.keywords : eventInfo.keywords;
			if (this.IsEnabled((EventLevel)level, keywords))
			{
				nameInfo = eventInfo.GetNameInfo(name ?? eventInfo.Name, tags);
				traceloggingId = nameInfo.identity;
			}
			descriptor = new EventDescriptor(traceloggingId, level, opcode, (long)keywords);
			return nameInfo;
		}

		// Token: 0x04001A86 RID: 6790
		private string m_name;

		// Token: 0x04001A87 RID: 6791
		internal int m_id;

		// Token: 0x04001A88 RID: 6792
		private Guid m_guid;

		// Token: 0x04001A89 RID: 6793
		internal volatile EventSource.EventMetadata[] m_eventData;

		// Token: 0x04001A8A RID: 6794
		private volatile byte[] m_rawManifest;

		// Token: 0x04001A8B RID: 6795
		private EventHandler<EventCommandEventArgs> m_eventCommandExecuted;

		// Token: 0x04001A8C RID: 6796
		private readonly EventSourceSettings m_config;

		// Token: 0x04001A8D RID: 6797
		private bool m_eventSourceDisposed;

		// Token: 0x04001A8E RID: 6798
		private bool m_eventSourceEnabled;

		// Token: 0x04001A8F RID: 6799
		internal EventLevel m_level;

		// Token: 0x04001A90 RID: 6800
		internal EventKeywords m_matchAnyKeyword;

		// Token: 0x04001A91 RID: 6801
		internal volatile EventDispatcher m_Dispatchers;

		// Token: 0x04001A92 RID: 6802
		private volatile EventSource.OverideEventProvider m_etwProvider;

		// Token: 0x04001A93 RID: 6803
		private object m_createEventLock;

		// Token: 0x04001A94 RID: 6804
		private IntPtr m_writeEventStringEventHandle;

		// Token: 0x04001A95 RID: 6805
		private volatile EventSource.OverideEventProvider m_eventPipeProvider;

		// Token: 0x04001A96 RID: 6806
		private bool m_completelyInited;

		// Token: 0x04001A97 RID: 6807
		private Exception m_constructionException;

		// Token: 0x04001A98 RID: 6808
		private byte m_outOfBandMessageCount;

		// Token: 0x04001A99 RID: 6809
		private EventCommandEventArgs m_deferredCommands;

		// Token: 0x04001A9A RID: 6810
		private string[] m_traits;

		// Token: 0x04001A9B RID: 6811
		[ThreadStatic]
		private static byte m_EventSourceExceptionRecurenceCount;

		// Token: 0x04001A9C RID: 6812
		[ThreadStatic]
		private static bool m_EventSourceInDecodeObject;

		// Token: 0x04001A9D RID: 6813
		internal volatile ulong[] m_channelData;

		// Token: 0x04001A9E RID: 6814
		private ActivityTracker m_activityTracker;

		// Token: 0x04001A9F RID: 6815
		private byte[] providerMetadata;

		// Token: 0x04001AA0 RID: 6816
		private readonly TraceLoggingEventHandleTable m_eventHandleTable;

		// Token: 0x0200072B RID: 1835
		[NullableContext(0)]
		protected internal struct EventData
		{
			// Token: 0x17000EE4 RID: 3812
			// (get) Token: 0x06005AF0 RID: 23280 RVA: 0x001BB677 File Offset: 0x001BA877
			// (set) Token: 0x06005AF1 RID: 23281 RVA: 0x001BB685 File Offset: 0x001BA885
			public unsafe IntPtr DataPointer
			{
				get
				{
					return (IntPtr)this.m_Ptr;
				}
				set
				{
					this.m_Ptr = (void*)value;
				}
			}

			// Token: 0x17000EE5 RID: 3813
			// (get) Token: 0x06005AF2 RID: 23282 RVA: 0x001BB694 File Offset: 0x001BA894
			// (set) Token: 0x06005AF3 RID: 23283 RVA: 0x001BB69C File Offset: 0x001BA89C
			public int Size
			{
				get
				{
					return this.m_Size;
				}
				set
				{
					this.m_Size = value;
				}
			}

			// Token: 0x17000EE6 RID: 3814
			// (set) Token: 0x06005AF4 RID: 23284 RVA: 0x001BB6A5 File Offset: 0x001BA8A5
			internal int Reserved
			{
				set
				{
					this.m_Reserved = value;
				}
			}

			// Token: 0x06005AF5 RID: 23285 RVA: 0x001BB6AE File Offset: 0x001BA8AE
			internal unsafe void SetMetadata(byte* pointer, int size, int reserved)
			{
				this.m_Ptr = pointer;
				this.m_Size = size;
				this.m_Reserved = reserved;
			}

			// Token: 0x04001AA1 RID: 6817
			internal ulong m_Ptr;

			// Token: 0x04001AA2 RID: 6818
			internal int m_Size;

			// Token: 0x04001AA3 RID: 6819
			internal int m_Reserved;
		}

		// Token: 0x0200072C RID: 1836
		private struct Sha1ForNonSecretPurposes
		{
			// Token: 0x06005AF6 RID: 23286 RVA: 0x001BB6C8 File Offset: 0x001BA8C8
			public void Start()
			{
				if (this.w == null)
				{
					this.w = new uint[85];
				}
				this.length = 0L;
				this.pos = 0;
				this.w[80] = 1732584193U;
				this.w[81] = 4023233417U;
				this.w[82] = 2562383102U;
				this.w[83] = 271733878U;
				this.w[84] = 3285377520U;
			}

			// Token: 0x06005AF7 RID: 23287 RVA: 0x001BB740 File Offset: 0x001BA940
			public void Append(byte input)
			{
				this.w[this.pos / 4] = (this.w[this.pos / 4] << 8 | (uint)input);
				int num = 64;
				int num2 = this.pos + 1;
				this.pos = num2;
				if (num == num2)
				{
					this.Drain();
				}
			}

			// Token: 0x06005AF8 RID: 23288 RVA: 0x001BB78C File Offset: 0x001BA98C
			public unsafe void Append(ReadOnlySpan<byte> input)
			{
				ReadOnlySpan<byte> readOnlySpan = input;
				for (int i = 0; i < readOnlySpan.Length; i++)
				{
					byte input2 = *readOnlySpan[i];
					this.Append(input2);
				}
			}

			// Token: 0x06005AF9 RID: 23289 RVA: 0x001BB7C0 File Offset: 0x001BA9C0
			public void Finish(byte[] output)
			{
				long num = this.length + (long)(8 * this.pos);
				this.Append(128);
				while (this.pos != 56)
				{
					this.Append(0);
				}
				this.Append((byte)(num >> 56));
				this.Append((byte)(num >> 48));
				this.Append((byte)(num >> 40));
				this.Append((byte)(num >> 32));
				this.Append((byte)(num >> 24));
				this.Append((byte)(num >> 16));
				this.Append((byte)(num >> 8));
				this.Append((byte)num);
				int num2 = (output.Length < 20) ? output.Length : 20;
				for (int num3 = 0; num3 != num2; num3++)
				{
					uint num4 = this.w[80 + num3 / 4];
					output[num3] = (byte)(num4 >> 24);
					this.w[80 + num3 / 4] = num4 << 8;
				}
			}

			// Token: 0x06005AFA RID: 23290 RVA: 0x001BB894 File Offset: 0x001BAA94
			private void Drain()
			{
				for (int num = 16; num != 80; num++)
				{
					this.w[num] = BitOperations.RotateLeft(this.w[num - 3] ^ this.w[num - 8] ^ this.w[num - 14] ^ this.w[num - 16], 1);
				}
				uint num2 = this.w[80];
				uint num3 = this.w[81];
				uint num4 = this.w[82];
				uint num5 = this.w[83];
				uint num6 = this.w[84];
				for (int num7 = 0; num7 != 20; num7++)
				{
					uint num8 = (num3 & num4) | (~num3 & num5);
					uint num9 = BitOperations.RotateLeft(num2, 5) + num8 + num6 + 1518500249U + this.w[num7];
					num6 = num5;
					num5 = num4;
					num4 = BitOperations.RotateLeft(num3, 30);
					num3 = num2;
					num2 = num9;
				}
				for (int num10 = 20; num10 != 40; num10++)
				{
					uint num11 = num3 ^ num4 ^ num5;
					uint num12 = BitOperations.RotateLeft(num2, 5) + num11 + num6 + 1859775393U + this.w[num10];
					num6 = num5;
					num5 = num4;
					num4 = BitOperations.RotateLeft(num3, 30);
					num3 = num2;
					num2 = num12;
				}
				for (int num13 = 40; num13 != 60; num13++)
				{
					uint num14 = (num3 & num4) | (num3 & num5) | (num4 & num5);
					uint num15 = BitOperations.RotateLeft(num2, 5) + num14 + num6 + 2400959708U + this.w[num13];
					num6 = num5;
					num5 = num4;
					num4 = BitOperations.RotateLeft(num3, 30);
					num3 = num2;
					num2 = num15;
				}
				for (int num16 = 60; num16 != 80; num16++)
				{
					uint num17 = num3 ^ num4 ^ num5;
					uint num18 = BitOperations.RotateLeft(num2, 5) + num17 + num6 + 3395469782U + this.w[num16];
					num6 = num5;
					num5 = num4;
					num4 = BitOperations.RotateLeft(num3, 30);
					num3 = num2;
					num2 = num18;
				}
				this.w[80] += num2;
				this.w[81] += num3;
				this.w[82] += num4;
				this.w[83] += num5;
				this.w[84] += num6;
				this.length += 512L;
				this.pos = 0;
			}

			// Token: 0x04001AA4 RID: 6820
			private long length;

			// Token: 0x04001AA5 RID: 6821
			private uint[] w;

			// Token: 0x04001AA6 RID: 6822
			private int pos;
		}

		// Token: 0x0200072D RID: 1837
		private class OverideEventProvider : EventProvider
		{
			// Token: 0x06005AFB RID: 23291 RVA: 0x001BBAD5 File Offset: 0x001BACD5
			public OverideEventProvider(EventSource eventSource, EventProviderType providerType) : base(providerType)
			{
				this.m_eventSource = eventSource;
				this.m_eventProviderType = providerType;
			}

			// Token: 0x06005AFC RID: 23292 RVA: 0x001BBAEC File Offset: 0x001BACEC
			protected override void OnControllerCommand(ControllerCommand command, IDictionary<string, string> arguments, int perEventSourceSessionId, int etwSessionId)
			{
				EventListener listener = null;
				this.m_eventSource.SendCommand(listener, this.m_eventProviderType, perEventSourceSessionId, etwSessionId, (EventCommand)command, base.IsEnabled(), base.Level, base.MatchAnyKeyword, arguments);
			}

			// Token: 0x04001AA7 RID: 6823
			private readonly EventSource m_eventSource;

			// Token: 0x04001AA8 RID: 6824
			private readonly EventProviderType m_eventProviderType;
		}

		// Token: 0x0200072E RID: 1838
		internal struct EventMetadata
		{
			// Token: 0x04001AA9 RID: 6825
			public EventDescriptor Descriptor;

			// Token: 0x04001AAA RID: 6826
			public IntPtr EventHandle;

			// Token: 0x04001AAB RID: 6827
			public EventTags Tags;

			// Token: 0x04001AAC RID: 6828
			public bool EnabledForAnyListener;

			// Token: 0x04001AAD RID: 6829
			public bool EnabledForETW;

			// Token: 0x04001AAE RID: 6830
			public bool EnabledForEventPipe;

			// Token: 0x04001AAF RID: 6831
			public bool HasRelatedActivityID;

			// Token: 0x04001AB0 RID: 6832
			public byte TriggersActivityTracking;

			// Token: 0x04001AB1 RID: 6833
			public string Name;

			// Token: 0x04001AB2 RID: 6834
			public string Message;

			// Token: 0x04001AB3 RID: 6835
			public ParameterInfo[] Parameters;

			// Token: 0x04001AB4 RID: 6836
			public TraceLoggingEventTypes TraceLoggingEventTypes;

			// Token: 0x04001AB5 RID: 6837
			public EventActivityOptions ActivityOptions;
		}
	}
}
