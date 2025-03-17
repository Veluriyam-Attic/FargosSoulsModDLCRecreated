using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000728 RID: 1832
	internal sealed class EtwEventProvider : IEventProvider
	{
		// Token: 0x06005A70 RID: 23152 RVA: 0x001B64B8 File Offset: 0x001B56B8
		unsafe uint IEventProvider.EventRegister(EventSource eventSource, Interop.Advapi32.EtwEnableCallback enableCallback, void* callbackContext, ref long registrationHandle)
		{
			Guid guid = eventSource.Guid;
			return Interop.Advapi32.EventRegister(guid, enableCallback, callbackContext, ref registrationHandle);
		}

		// Token: 0x06005A71 RID: 23153 RVA: 0x001B64D7 File Offset: 0x001B56D7
		uint IEventProvider.EventUnregister(long registrationHandle)
		{
			return Interop.Advapi32.EventUnregister(registrationHandle);
		}

		// Token: 0x06005A72 RID: 23154 RVA: 0x001B64E0 File Offset: 0x001B56E0
		unsafe EventProvider.WriteEventErrorCode IEventProvider.EventWriteTransfer(long registrationHandle, in EventDescriptor eventDescriptor, IntPtr eventHandle, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData)
		{
			int num = Interop.Advapi32.EventWriteTransfer(registrationHandle, eventDescriptor, activityId, relatedActivityId, userDataCount, userData);
			if (num == 8)
			{
				return EventProvider.WriteEventErrorCode.NoFreeBuffers;
			}
			if (num == 234 || num == 534)
			{
				return EventProvider.WriteEventErrorCode.EventTooBig;
			}
			return EventProvider.WriteEventErrorCode.NoError;
		}

		// Token: 0x06005A73 RID: 23155 RVA: 0x001B6516 File Offset: 0x001B5716
		int IEventProvider.EventActivityIdControl(Interop.Advapi32.ActivityControl ControlCode, ref Guid ActivityId)
		{
			return Interop.Advapi32.EventActivityIdControl(ControlCode, ref ActivityId);
		}

		// Token: 0x06005A74 RID: 23156 RVA: 0x000C279F File Offset: 0x000C199F
		unsafe IntPtr IEventProvider.DefineEventHandle(uint eventID, string eventName, long keywords, uint eventVersion, uint level, byte* pMetadata, uint metadataLength)
		{
			throw new NotSupportedException();
		}
	}
}
