using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200071B RID: 1819
	internal sealed class EventPipeEventProvider : IEventProvider
	{
		// Token: 0x06005A35 RID: 23093 RVA: 0x001B41BC File Offset: 0x001B33BC
		unsafe uint IEventProvider.EventRegister(EventSource eventSource, Interop.Advapi32.EtwEnableCallback enableCallback, void* callbackContext, ref long registrationHandle)
		{
			uint result = 0U;
			this.m_provHandle = EventPipeInternal.CreateProvider(eventSource.Name, enableCallback);
			if (this.m_provHandle != IntPtr.Zero)
			{
				registrationHandle = 1L;
			}
			else
			{
				result = 1U;
			}
			return result;
		}

		// Token: 0x06005A36 RID: 23094 RVA: 0x001B41F9 File Offset: 0x001B33F9
		uint IEventProvider.EventUnregister(long registrationHandle)
		{
			EventPipeInternal.DeleteProvider(this.m_provHandle);
			return 0U;
		}

		// Token: 0x06005A37 RID: 23095 RVA: 0x001B4208 File Offset: 0x001B3408
		unsafe EventProvider.WriteEventErrorCode IEventProvider.EventWriteTransfer(long registrationHandle, in EventDescriptor eventDescriptor, IntPtr eventHandle, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData)
		{
			if (eventHandle != IntPtr.Zero)
			{
				if (userDataCount == 0)
				{
					EventPipeInternal.WriteEventData(eventHandle, null, 0U, activityId, relatedActivityId);
					return EventProvider.WriteEventErrorCode.NoError;
				}
				EventDescriptor eventDescriptor2 = eventDescriptor;
				if (eventDescriptor2.Channel == 11)
				{
					userData += 3;
					userDataCount -= 3;
				}
				EventPipeInternal.WriteEventData(eventHandle, userData, (uint)userDataCount, activityId, relatedActivityId);
			}
			return EventProvider.WriteEventErrorCode.NoError;
		}

		// Token: 0x06005A38 RID: 23096 RVA: 0x001B426A File Offset: 0x001B346A
		int IEventProvider.EventActivityIdControl(Interop.Advapi32.ActivityControl ControlCode, ref Guid ActivityId)
		{
			return EventPipeInternal.EventActivityIdControl((uint)ControlCode, ref ActivityId);
		}

		// Token: 0x06005A39 RID: 23097 RVA: 0x001B4274 File Offset: 0x001B3474
		unsafe IntPtr IEventProvider.DefineEventHandle(uint eventID, string eventName, long keywords, uint eventVersion, uint level, byte* pMetadata, uint metadataLength)
		{
			return EventPipeInternal.DefineEvent(this.m_provHandle, eventID, keywords, eventVersion, level, (void*)pMetadata, metadataLength);
		}

		// Token: 0x04001A59 RID: 6745
		private IntPtr m_provHandle = IntPtr.Zero;
	}
}
