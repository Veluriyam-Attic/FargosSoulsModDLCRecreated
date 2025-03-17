using System;

namespace System
{
	// Token: 0x02000080 RID: 128
	internal struct RuntimeFieldHandleInternal
	{
		// Token: 0x17000057 RID: 87
		// (get) Token: 0x06000542 RID: 1346 RVA: 0x000B91F8 File Offset: 0x000B83F8
		internal IntPtr Value
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x06000543 RID: 1347 RVA: 0x000B9200 File Offset: 0x000B8400
		internal RuntimeFieldHandleInternal(IntPtr value)
		{
			this.m_handle = value;
		}

		// Token: 0x0400019D RID: 413
		internal IntPtr m_handle;
	}
}
