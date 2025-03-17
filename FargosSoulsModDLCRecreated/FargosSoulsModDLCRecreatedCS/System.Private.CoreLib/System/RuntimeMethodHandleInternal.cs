using System;

namespace System
{
	// Token: 0x0200007C RID: 124
	internal struct RuntimeMethodHandleInternal
	{
		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x000B8EA4 File Offset: 0x000B80A4
		internal static RuntimeMethodHandleInternal EmptyHandle
		{
			get
			{
				return default(RuntimeMethodHandleInternal);
			}
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x000B8EBA File Offset: 0x000B80BA
		internal bool IsNullHandle()
		{
			return this.m_handle == IntPtr.Zero;
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x000B8ECC File Offset: 0x000B80CC
		internal IntPtr Value
		{
			get
			{
				return this.m_handle;
			}
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000B8ED4 File Offset: 0x000B80D4
		internal RuntimeMethodHandleInternal(IntPtr value)
		{
			this.m_handle = value;
		}

		// Token: 0x04000192 RID: 402
		internal IntPtr m_handle;
	}
}
