using System;

namespace System.Reflection.Emit
{
	// Token: 0x0200061F RID: 1567
	internal sealed class VarArgMethod
	{
		// Token: 0x06004F3F RID: 20287 RVA: 0x0018FE4D File Offset: 0x0018F04D
		internal VarArgMethod(DynamicMethod dm, SignatureHelper signature)
		{
			this.m_dynamicMethod = dm;
			this.m_signature = signature;
		}

		// Token: 0x06004F40 RID: 20288 RVA: 0x0018FE63 File Offset: 0x0018F063
		internal VarArgMethod(RuntimeMethodInfo method, SignatureHelper signature)
		{
			this.m_method = method;
			this.m_signature = signature;
		}

		// Token: 0x0400143B RID: 5179
		internal RuntimeMethodInfo m_method;

		// Token: 0x0400143C RID: 5180
		internal DynamicMethod m_dynamicMethod;

		// Token: 0x0400143D RID: 5181
		internal SignatureHelper m_signature;
	}
}
