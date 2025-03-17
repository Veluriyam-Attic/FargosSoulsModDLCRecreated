using System;

namespace System
{
	// Token: 0x0200007D RID: 125
	internal class RuntimeMethodInfoStub : IRuntimeMethodInfo
	{
		// Token: 0x06000505 RID: 1285 RVA: 0x000B8EDD File Offset: 0x000B80DD
		public RuntimeMethodInfoStub(RuntimeMethodHandleInternal methodHandleValue, object keepalive)
		{
			this.m_keepalive = keepalive;
			this.m_value = methodHandleValue;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x000B8EF3 File Offset: 0x000B80F3
		public RuntimeMethodInfoStub(IntPtr methodHandleValue, object keepalive)
		{
			this.m_keepalive = keepalive;
			this.m_value = new RuntimeMethodHandleInternal(methodHandleValue);
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000507 RID: 1287 RVA: 0x000B8F0E File Offset: 0x000B810E
		RuntimeMethodHandleInternal IRuntimeMethodInfo.Value
		{
			get
			{
				return this.m_value;
			}
		}

		// Token: 0x04000193 RID: 403
		private readonly object m_keepalive;

		// Token: 0x04000194 RID: 404
		private object m_a;

		// Token: 0x04000195 RID: 405
		private object m_b;

		// Token: 0x04000196 RID: 406
		private object m_c;

		// Token: 0x04000197 RID: 407
		private object m_d;

		// Token: 0x04000198 RID: 408
		private object m_e;

		// Token: 0x04000199 RID: 409
		private object m_f;

		// Token: 0x0400019A RID: 410
		private object m_g;

		// Token: 0x0400019B RID: 411
		public RuntimeMethodHandleInternal m_value;
	}
}
