using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004A3 RID: 1187
	[NullableContext(2)]
	[Nullable(0)]
	public sealed class UnknownWrapper
	{
		// Token: 0x060044E7 RID: 17639 RVA: 0x00179B73 File Offset: 0x00178D73
		public UnknownWrapper(object obj)
		{
			this.WrappedObject = obj;
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x060044E8 RID: 17640 RVA: 0x00179B82 File Offset: 0x00178D82
		public object WrappedObject { get; }
	}
}
