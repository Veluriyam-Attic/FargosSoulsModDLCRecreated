using System;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200056E RID: 1390
	internal ref struct QCallTypeHandle
	{
		// Token: 0x060047BE RID: 18366 RVA: 0x0017E260 File Offset: 0x0017D460
		internal QCallTypeHandle(ref RuntimeType type)
		{
			this._ptr = Unsafe.AsPointer<RuntimeType>(ref type);
			if (type != null)
			{
				this._handle = type.GetUnderlyingNativeHandle();
				return;
			}
			this._handle = IntPtr.Zero;
		}

		// Token: 0x060047BF RID: 18367 RVA: 0x0017E291 File Offset: 0x0017D491
		internal QCallTypeHandle(ref RuntimeTypeHandle rth)
		{
			this._ptr = Unsafe.AsPointer<RuntimeTypeHandle>(ref rth);
			this._handle = rth.Value;
		}

		// Token: 0x04001157 RID: 4439
		private unsafe void* _ptr;

		// Token: 0x04001158 RID: 4440
		private IntPtr _handle;
	}
}
