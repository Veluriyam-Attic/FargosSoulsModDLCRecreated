using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020002BB RID: 699
	[NullableContext(1)]
	[Nullable(0)]
	public class ThreadExceptionEventArgs : EventArgs
	{
		// Token: 0x06002879 RID: 10361 RVA: 0x00148AF6 File Offset: 0x00147CF6
		public ThreadExceptionEventArgs(Exception t)
		{
			this.m_exception = t;
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x0600287A RID: 10362 RVA: 0x00148B05 File Offset: 0x00147D05
		public Exception Exception
		{
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x04000ACB RID: 2763
		private readonly Exception m_exception;
	}
}
