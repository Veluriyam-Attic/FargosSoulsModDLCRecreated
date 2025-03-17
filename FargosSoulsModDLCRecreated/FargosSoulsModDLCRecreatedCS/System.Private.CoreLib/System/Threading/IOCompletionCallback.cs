using System;

namespace System.Threading
{
	// Token: 0x0200029D RID: 669
	// (Invoke) Token: 0x0600279C RID: 10140
	[CLSCompliant(false)]
	public unsafe delegate void IOCompletionCallback(uint errorCode, uint numBytes, NativeOverlapped* pOVERLAP);
}
