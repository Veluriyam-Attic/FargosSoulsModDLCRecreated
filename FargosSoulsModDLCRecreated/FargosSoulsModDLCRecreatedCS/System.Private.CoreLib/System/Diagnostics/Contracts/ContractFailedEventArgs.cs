using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Contracts
{
	// Token: 0x020006DF RID: 1759
	[Nullable(0)]
	[NullableContext(2)]
	public sealed class ContractFailedEventArgs : EventArgs
	{
		// Token: 0x060058F9 RID: 22777 RVA: 0x001B1641 File Offset: 0x001B0841
		public ContractFailedEventArgs(ContractFailureKind failureKind, string message, string condition, Exception originalException)
		{
			this._failureKind = failureKind;
			this._message = message;
			this._condition = condition;
			this._originalException = originalException;
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x060058FA RID: 22778 RVA: 0x001B1666 File Offset: 0x001B0866
		public string Message
		{
			get
			{
				return this._message;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x060058FB RID: 22779 RVA: 0x001B166E File Offset: 0x001B086E
		public string Condition
		{
			get
			{
				return this._condition;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x060058FC RID: 22780 RVA: 0x001B1676 File Offset: 0x001B0876
		public ContractFailureKind FailureKind
		{
			get
			{
				return this._failureKind;
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x060058FD RID: 22781 RVA: 0x001B167E File Offset: 0x001B087E
		public Exception OriginalException
		{
			get
			{
				return this._originalException;
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x060058FE RID: 22782 RVA: 0x001B1686 File Offset: 0x001B0886
		public bool Handled
		{
			get
			{
				return this._handled;
			}
		}

		// Token: 0x060058FF RID: 22783 RVA: 0x001B168E File Offset: 0x001B088E
		public void SetHandled()
		{
			this._handled = true;
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06005900 RID: 22784 RVA: 0x001B1697 File Offset: 0x001B0897
		public bool Unwind
		{
			get
			{
				return this._unwind;
			}
		}

		// Token: 0x06005901 RID: 22785 RVA: 0x001B169F File Offset: 0x001B089F
		public void SetUnwind()
		{
			this._unwind = true;
		}

		// Token: 0x0400198F RID: 6543
		private readonly ContractFailureKind _failureKind;

		// Token: 0x04001990 RID: 6544
		private readonly string _message;

		// Token: 0x04001991 RID: 6545
		private readonly string _condition;

		// Token: 0x04001992 RID: 6546
		private readonly Exception _originalException;

		// Token: 0x04001993 RID: 6547
		private bool _handled;

		// Token: 0x04001994 RID: 6548
		private bool _unwind;

		// Token: 0x04001995 RID: 6549
		internal Exception thrownDuringHandler;
	}
}
