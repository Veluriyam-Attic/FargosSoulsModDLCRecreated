using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200061D RID: 1565
	internal sealed class GenericMethodInfo
	{
		// Token: 0x06004F3D RID: 20285 RVA: 0x0018FE21 File Offset: 0x0018F021
		internal GenericMethodInfo(RuntimeMethodHandle methodHandle, RuntimeTypeHandle context)
		{
			this.m_methodHandle = methodHandle;
			this.m_context = context;
		}

		// Token: 0x04001437 RID: 5175
		internal RuntimeMethodHandle m_methodHandle;

		// Token: 0x04001438 RID: 5176
		internal RuntimeTypeHandle m_context;
	}
}
