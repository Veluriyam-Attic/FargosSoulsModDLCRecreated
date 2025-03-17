using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000729 RID: 1833
	internal sealed class NoOpEventProvider : IEventProvider
	{
		// Token: 0x06005A76 RID: 23158 RVA: 0x000AC09B File Offset: 0x000AB29B
		unsafe uint IEventProvider.EventRegister(EventSource eventSource, Interop.Advapi32.EtwEnableCallback enableCallback, void* callbackContext, ref long registrationHandle)
		{
			return 0U;
		}

		// Token: 0x06005A77 RID: 23159 RVA: 0x000AC09B File Offset: 0x000AB29B
		uint IEventProvider.EventUnregister(long registrationHandle)
		{
			return 0U;
		}

		// Token: 0x06005A78 RID: 23160 RVA: 0x000AC09B File Offset: 0x000AB29B
		unsafe EventProvider.WriteEventErrorCode IEventProvider.EventWriteTransfer(long registrationHandle, in EventDescriptor eventDescriptor, IntPtr eventHandle, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData)
		{
			return EventProvider.WriteEventErrorCode.NoError;
		}

		// Token: 0x06005A79 RID: 23161 RVA: 0x000AC09B File Offset: 0x000AB29B
		int IEventProvider.EventActivityIdControl(Interop.Advapi32.ActivityControl ControlCode, ref Guid ActivityId)
		{
			return 0;
		}

		// Token: 0x06005A7A RID: 23162 RVA: 0x0016C54C File Offset: 0x0016B74C
		unsafe IntPtr IEventProvider.DefineEventHandle(uint eventID, string eventName, long keywords, uint eventVersion, uint level, byte* pMetadata, uint metadataLength)
		{
			return IntPtr.Zero;
		}
	}
}
