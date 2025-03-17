using System;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;
using System.Threading;

namespace System
{
	// Token: 0x02000167 RID: 359
	[NullableContext(2)]
	[Nullable(0)]
	[TypeForwardedFrom("mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089")]
	[Serializable]
	public class OperationCanceledException : SystemException
	{
		// Token: 0x17000199 RID: 409
		// (get) Token: 0x06001252 RID: 4690 RVA: 0x000E69CF File Offset: 0x000E5BCF
		// (set) Token: 0x06001253 RID: 4691 RVA: 0x000E69D7 File Offset: 0x000E5BD7
		public CancellationToken CancellationToken
		{
			get
			{
				return this._cancellationToken;
			}
			private set
			{
				this._cancellationToken = value;
			}
		}

		// Token: 0x06001254 RID: 4692 RVA: 0x000E69E0 File Offset: 0x000E5BE0
		public OperationCanceledException() : base(SR.OperationCanceled)
		{
			base.HResult = -2146233029;
		}

		// Token: 0x06001255 RID: 4693 RVA: 0x000E69F8 File Offset: 0x000E5BF8
		public OperationCanceledException(string message) : base(message)
		{
			base.HResult = -2146233029;
		}

		// Token: 0x06001256 RID: 4694 RVA: 0x000E6A0C File Offset: 0x000E5C0C
		public OperationCanceledException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146233029;
		}

		// Token: 0x06001257 RID: 4695 RVA: 0x000E6A21 File Offset: 0x000E5C21
		public OperationCanceledException(CancellationToken token) : this()
		{
			this.CancellationToken = token;
		}

		// Token: 0x06001258 RID: 4696 RVA: 0x000E6A30 File Offset: 0x000E5C30
		public OperationCanceledException(string message, CancellationToken token) : this(message)
		{
			this.CancellationToken = token;
		}

		// Token: 0x06001259 RID: 4697 RVA: 0x000E6A40 File Offset: 0x000E5C40
		public OperationCanceledException(string message, Exception innerException, CancellationToken token) : this(message, innerException)
		{
			this.CancellationToken = token;
		}

		// Token: 0x0600125A RID: 4698 RVA: 0x000C7203 File Offset: 0x000C6403
		[NullableContext(1)]
		protected OperationCanceledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000463 RID: 1123
		[NonSerialized]
		private CancellationToken _cancellationToken;
	}
}
