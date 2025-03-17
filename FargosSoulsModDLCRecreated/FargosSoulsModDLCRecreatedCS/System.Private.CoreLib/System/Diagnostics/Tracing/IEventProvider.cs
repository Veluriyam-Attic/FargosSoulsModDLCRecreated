using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000747 RID: 1863
	internal interface IEventProvider
	{
		// Token: 0x06005B94 RID: 23444
		unsafe uint EventRegister(EventSource eventSource, Interop.Advapi32.EtwEnableCallback enableCallback, void* callbackContext, ref long registrationHandle);

		// Token: 0x06005B95 RID: 23445
		uint EventUnregister(long registrationHandle);

		// Token: 0x06005B96 RID: 23446
		unsafe EventProvider.WriteEventErrorCode EventWriteTransfer(long registrationHandle, in EventDescriptor eventDescriptor, IntPtr eventHandle, Guid* activityId, Guid* relatedActivityId, int userDataCount, EventProvider.EventData* userData);

		// Token: 0x06005B97 RID: 23447
		int EventActivityIdControl(Interop.Advapi32.ActivityControl ControlCode, ref Guid ActivityId);

		// Token: 0x06005B98 RID: 23448
		unsafe IntPtr DefineEventHandle(uint eventID, string eventName, long keywords, uint eventVersion, uint level, byte* pMetadata, uint metadataLength);
	}
}
