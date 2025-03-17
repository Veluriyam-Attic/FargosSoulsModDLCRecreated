using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.ExceptionServices
{
	// Token: 0x020003F1 RID: 1009
	[NullableContext(1)]
	[Nullable(0)]
	public class FirstChanceExceptionEventArgs : EventArgs
	{
		// Token: 0x06003275 RID: 12917 RVA: 0x0016B395 File Offset: 0x0016A595
		public FirstChanceExceptionEventArgs(Exception exception)
		{
			this.Exception = exception;
		}

		// Token: 0x170009C8 RID: 2504
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x0016B3A4 File Offset: 0x0016A5A4
		public Exception Exception { get; }
	}
}
