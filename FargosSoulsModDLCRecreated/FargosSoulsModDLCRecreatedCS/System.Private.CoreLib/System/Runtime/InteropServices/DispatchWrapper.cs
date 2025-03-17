using System;
using System.Runtime.CompilerServices;
using System.Runtime.Versioning;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200047D RID: 1149
	[Nullable(0)]
	[NullableContext(2)]
	[SupportedOSPlatform("windows")]
	public sealed class DispatchWrapper
	{
		// Token: 0x0600446F RID: 17519 RVA: 0x0017914C File Offset: 0x0017834C
		public DispatchWrapper(object obj)
		{
			if (obj != null)
			{
				IntPtr idispatchForObject = Marshal.GetIDispatchForObject(obj);
				Marshal.Release(idispatchForObject);
				this.WrappedObject = obj;
			}
		}

		// Token: 0x17000A6D RID: 2669
		// (get) Token: 0x06004470 RID: 17520 RVA: 0x00179177 File Offset: 0x00178377
		public object WrappedObject { get; }
	}
}
