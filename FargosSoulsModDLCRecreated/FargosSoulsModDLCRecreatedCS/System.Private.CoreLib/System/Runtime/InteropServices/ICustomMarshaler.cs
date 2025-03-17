using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000489 RID: 1161
	[NullableContext(1)]
	public interface ICustomMarshaler
	{
		// Token: 0x0600448B RID: 17547
		object MarshalNativeToManaged(IntPtr pNativeData);

		// Token: 0x0600448C RID: 17548
		IntPtr MarshalManagedToNative(object ManagedObj);

		// Token: 0x0600448D RID: 17549
		void CleanUpNativeData(IntPtr pNativeData);

		// Token: 0x0600448E RID: 17550
		void CleanUpManagedData(object ManagedObj);

		// Token: 0x0600448F RID: 17551
		int GetNativeDataSize();
	}
}
