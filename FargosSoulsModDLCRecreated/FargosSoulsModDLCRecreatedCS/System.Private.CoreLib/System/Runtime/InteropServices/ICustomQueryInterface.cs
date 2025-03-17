using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200048A RID: 1162
	public interface ICustomQueryInterface
	{
		// Token: 0x06004490 RID: 17552
		CustomQueryInterfaceResult GetInterface([In] ref Guid iid, out IntPtr ppv);
	}
}
