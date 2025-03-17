using System;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200056A RID: 1386
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	internal ref struct ObjectHandleOnStack
	{
		// Token: 0x060047B8 RID: 18360 RVA: 0x0017E1E6 File Offset: 0x0017D3E6
		private unsafe ObjectHandleOnStack(void* pObject)
		{
			this._ptr = pObject;
		}

		// Token: 0x060047B9 RID: 18361 RVA: 0x0017E1EF File Offset: 0x0017D3EF
		internal static ObjectHandleOnStack Create<T>(ref T o) where T : class
		{
			return new ObjectHandleOnStack(Unsafe.AsPointer<T>(ref o));
		}

		// Token: 0x04001151 RID: 4433
		private unsafe void* _ptr;
	}
}
