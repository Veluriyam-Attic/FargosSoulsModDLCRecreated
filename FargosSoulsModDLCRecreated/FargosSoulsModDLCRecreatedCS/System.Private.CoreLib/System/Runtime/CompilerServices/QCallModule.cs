using System;
using System.Reflection;
using System.Reflection.Emit;
using Internal.Runtime.CompilerServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200056C RID: 1388
	internal ref struct QCallModule
	{
		// Token: 0x060047BB RID: 18363 RVA: 0x0017E20A File Offset: 0x0017D40A
		internal QCallModule(ref RuntimeModule module)
		{
			this._ptr = Unsafe.AsPointer<RuntimeModule>(ref module);
			this._module = module.GetUnderlyingNativeHandle();
		}

		// Token: 0x060047BC RID: 18364 RVA: 0x0017E225 File Offset: 0x0017D425
		internal QCallModule(ref ModuleBuilder module)
		{
			this._ptr = Unsafe.AsPointer<ModuleBuilder>(ref module);
			this._module = module.GetNativeHandle().GetUnderlyingNativeHandle();
		}

		// Token: 0x04001153 RID: 4435
		private unsafe void* _ptr;

		// Token: 0x04001154 RID: 4436
		private IntPtr _module;
	}
}
