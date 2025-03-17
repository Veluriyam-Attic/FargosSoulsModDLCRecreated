using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200061E RID: 1566
	internal sealed class GenericFieldInfo
	{
		// Token: 0x06004F3E RID: 20286 RVA: 0x0018FE37 File Offset: 0x0018F037
		internal GenericFieldInfo(RuntimeFieldHandle fieldHandle, RuntimeTypeHandle context)
		{
			this.m_fieldHandle = fieldHandle;
			this.m_context = context;
		}

		// Token: 0x04001439 RID: 5177
		internal RuntimeFieldHandle m_fieldHandle;

		// Token: 0x0400143A RID: 5178
		internal RuntimeTypeHandle m_context;
	}
}
