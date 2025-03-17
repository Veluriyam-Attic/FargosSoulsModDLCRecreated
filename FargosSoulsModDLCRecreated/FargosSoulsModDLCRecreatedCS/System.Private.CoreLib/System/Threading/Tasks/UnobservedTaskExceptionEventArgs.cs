using System;
using System.Runtime.CompilerServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200033D RID: 829
	[NullableContext(1)]
	[Nullable(0)]
	public class UnobservedTaskExceptionEventArgs : EventArgs
	{
		// Token: 0x06002C11 RID: 11281 RVA: 0x00153997 File Offset: 0x00152B97
		public UnobservedTaskExceptionEventArgs(AggregateException exception)
		{
			this.m_exception = exception;
		}

		// Token: 0x06002C12 RID: 11282 RVA: 0x001539A6 File Offset: 0x00152BA6
		public void SetObserved()
		{
			this.m_observed = true;
		}

		// Token: 0x170008F0 RID: 2288
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x001539AF File Offset: 0x00152BAF
		public bool Observed
		{
			get
			{
				return this.m_observed;
			}
		}

		// Token: 0x170008F1 RID: 2289
		// (get) Token: 0x06002C14 RID: 11284 RVA: 0x001539B7 File Offset: 0x00152BB7
		public AggregateException Exception
		{
			get
			{
				return this.m_exception;
			}
		}

		// Token: 0x04000C29 RID: 3113
		private readonly AggregateException m_exception;

		// Token: 0x04000C2A RID: 3114
		internal bool m_observed;
	}
}
