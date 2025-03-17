using System;
using System.Runtime.InteropServices;

namespace System
{
	// Token: 0x02000082 RID: 130
	[StructLayout(LayoutKind.Sequential)]
	internal class RuntimeFieldInfoStub : IRuntimeFieldInfo
	{
		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x000B9209 File Offset: 0x000B8409
		RuntimeFieldHandleInternal IRuntimeFieldInfo.Value
		{
			get
			{
				return this.m_fieldHandle;
			}
		}

		// Token: 0x0400019E RID: 414
		private object m_keepalive;

		// Token: 0x0400019F RID: 415
		private object m_c;

		// Token: 0x040001A0 RID: 416
		private object m_d;

		// Token: 0x040001A1 RID: 417
		private int m_b;

		// Token: 0x040001A2 RID: 418
		private object m_e;

		// Token: 0x040001A3 RID: 419
		private RuntimeFieldHandleInternal m_fieldHandle;
	}
}
