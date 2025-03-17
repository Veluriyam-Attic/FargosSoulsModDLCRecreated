using System;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000569 RID: 1385
	internal ref struct StringHandleOnStack
	{
		// Token: 0x060047B7 RID: 18359 RVA: 0x0017E1D8 File Offset: 0x0017D3D8
		internal StringHandleOnStack(ref string s)
		{
			this._ptr = Unsafe.AsPointer<string>(ref s);
		}

		// Token: 0x04001150 RID: 4432
		private unsafe void* _ptr;
	}
}
