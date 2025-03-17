using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System.Threading
{
	// Token: 0x02000272 RID: 626
	[UnsupportedOSPlatform("browser")]
	public sealed class RegisteredWaitHandle : MarshalByRefObject
	{
		// Token: 0x06002653 RID: 9811 RVA: 0x00141EC7 File Offset: 0x001410C7
		internal RegisteredWaitHandle()
		{
			this.internalRegisteredWait = new RegisteredWaitHandleSafe();
		}

		// Token: 0x06002654 RID: 9812 RVA: 0x00141EDA File Offset: 0x001410DA
		internal void SetHandle(IntPtr handle)
		{
			this.internalRegisteredWait.SetHandle(handle);
		}

		// Token: 0x06002655 RID: 9813 RVA: 0x00141EE8 File Offset: 0x001410E8
		internal void SetWaitObject(WaitHandle waitObject)
		{
			this.internalRegisteredWait.SetWaitObject(waitObject);
		}

		// Token: 0x06002656 RID: 9814 RVA: 0x00141EF6 File Offset: 0x001410F6
		[NullableContext(2)]
		public bool Unregister(WaitHandle waitObject)
		{
			return this.internalRegisteredWait.Unregister(waitObject);
		}

		// Token: 0x040009FA RID: 2554
		private readonly RegisteredWaitHandleSafe internalRegisteredWait;
	}
}
