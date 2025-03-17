using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x020004A7 RID: 1191
	[Nullable(0)]
	[NullableContext(2)]
	public sealed class VariantWrapper
	{
		// Token: 0x060044EC RID: 17644 RVA: 0x00179BB0 File Offset: 0x00178DB0
		public VariantWrapper(object obj)
		{
			this.WrappedObject = obj;
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x060044ED RID: 17645 RVA: 0x00179BBF File Offset: 0x00178DBF
		public object WrappedObject { get; }
	}
}
