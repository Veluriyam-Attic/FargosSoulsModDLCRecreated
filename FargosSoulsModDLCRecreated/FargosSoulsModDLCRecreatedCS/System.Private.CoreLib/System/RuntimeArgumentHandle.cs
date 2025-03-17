using System;

namespace System
{
	// Token: 0x02000079 RID: 121
	public ref struct RuntimeArgumentHandle
	{
		// Token: 0x1700004F RID: 79
		// (get) Token: 0x06000497 RID: 1175 RVA: 0x000B8670 File Offset: 0x000B7870
		internal IntPtr Value
		{
			get
			{
				return this.m_ptr;
			}
		}

		// Token: 0x0400018E RID: 398
		private IntPtr m_ptr;
	}
}
