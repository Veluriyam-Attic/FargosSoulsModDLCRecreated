using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000487 RID: 1159
	[NullableContext(1)]
	public interface ICustomAdapter
	{
		// Token: 0x06004489 RID: 17545
		[return: MarshalAs(UnmanagedType.IUnknown)]
		object GetUnderlyingObject();
	}
}
