using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001B5 RID: 437
	[NullableContext(1)]
	[Nullable(0)]
	public class UnhandledExceptionEventArgs : EventArgs
	{
		// Token: 0x06001AD3 RID: 6867 RVA: 0x000FCF19 File Offset: 0x000FC119
		public UnhandledExceptionEventArgs(object exception, bool isTerminating)
		{
			this._exception = exception;
			this._isTerminating = isTerminating;
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06001AD4 RID: 6868 RVA: 0x000FCF2F File Offset: 0x000FC12F
		public object ExceptionObject
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06001AD5 RID: 6869 RVA: 0x000FCF37 File Offset: 0x000FC137
		public bool IsTerminating
		{
			get
			{
				return this._isTerminating;
			}
		}

		// Token: 0x040005D6 RID: 1494
		private readonly object _exception;

		// Token: 0x040005D7 RID: 1495
		private readonly bool _isTerminating;
	}
}
