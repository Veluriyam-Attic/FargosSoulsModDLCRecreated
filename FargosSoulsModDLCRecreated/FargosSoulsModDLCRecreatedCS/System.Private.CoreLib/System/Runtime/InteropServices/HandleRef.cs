using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000486 RID: 1158
	[NullableContext(2)]
	[Nullable(0)]
	public readonly struct HandleRef
	{
		// Token: 0x06004484 RID: 17540 RVA: 0x00179314 File Offset: 0x00178514
		public HandleRef(object wrapper, IntPtr handle)
		{
			this._wrapper = wrapper;
			this._handle = handle;
		}

		// Token: 0x17000A74 RID: 2676
		// (get) Token: 0x06004485 RID: 17541 RVA: 0x00179324 File Offset: 0x00178524
		public object Wrapper
		{
			get
			{
				return this._wrapper;
			}
		}

		// Token: 0x17000A75 RID: 2677
		// (get) Token: 0x06004486 RID: 17542 RVA: 0x0017932C File Offset: 0x0017852C
		public IntPtr Handle
		{
			get
			{
				return this._handle;
			}
		}

		// Token: 0x06004487 RID: 17543 RVA: 0x0017932C File Offset: 0x0017852C
		public static explicit operator IntPtr(HandleRef value)
		{
			return value._handle;
		}

		// Token: 0x06004488 RID: 17544 RVA: 0x0017932C File Offset: 0x0017852C
		public static IntPtr ToIntPtr(HandleRef value)
		{
			return value._handle;
		}

		// Token: 0x04000F4C RID: 3916
		private readonly object _wrapper;

		// Token: 0x04000F4D RID: 3917
		private readonly IntPtr _handle;
	}
}
