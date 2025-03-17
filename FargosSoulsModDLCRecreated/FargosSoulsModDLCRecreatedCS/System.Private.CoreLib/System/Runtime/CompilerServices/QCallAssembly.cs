using System;
using System.Reflection;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200056D RID: 1389
	[Obsolete("Types with embedded references are not supported in this version of your compiler.", true)]
	internal ref struct QCallAssembly
	{
		// Token: 0x060047BD RID: 18365 RVA: 0x0017E245 File Offset: 0x0017D445
		internal QCallAssembly(ref RuntimeAssembly assembly)
		{
			this._ptr = Unsafe.AsPointer<RuntimeAssembly>(ref assembly);
			this._assembly = assembly.GetUnderlyingNativeHandle();
		}

		// Token: 0x04001155 RID: 4437
		private unsafe void* _ptr;

		// Token: 0x04001156 RID: 4438
		private IntPtr _assembly;
	}
}
