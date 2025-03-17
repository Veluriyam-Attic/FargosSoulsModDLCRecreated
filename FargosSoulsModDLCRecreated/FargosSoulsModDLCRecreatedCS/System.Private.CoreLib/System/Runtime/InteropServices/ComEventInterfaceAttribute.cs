using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200046F RID: 1135
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class ComEventInterfaceAttribute : Attribute
	{
		// Token: 0x06004449 RID: 17481 RVA: 0x00178DC3 File Offset: 0x00177FC3
		public ComEventInterfaceAttribute(Type SourceInterface, Type EventProvider)
		{
			this.SourceInterface = SourceInterface;
			this.EventProvider = EventProvider;
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x0600444A RID: 17482 RVA: 0x00178DD9 File Offset: 0x00177FD9
		public Type SourceInterface { get; }

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x0600444B RID: 17483 RVA: 0x00178DE1 File Offset: 0x00177FE1
		public Type EventProvider { get; }
	}
}
