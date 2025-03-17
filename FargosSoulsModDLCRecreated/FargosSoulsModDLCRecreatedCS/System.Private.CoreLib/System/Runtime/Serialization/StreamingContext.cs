using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003ED RID: 1005
	[Nullable(0)]
	[NullableContext(2)]
	public readonly struct StreamingContext
	{
		// Token: 0x06003267 RID: 12903 RVA: 0x0016B2A2 File Offset: 0x0016A4A2
		public StreamingContext(StreamingContextStates state)
		{
			this = new StreamingContext(state, null);
		}

		// Token: 0x06003268 RID: 12904 RVA: 0x0016B2AC File Offset: 0x0016A4AC
		public StreamingContext(StreamingContextStates state, object additional)
		{
			this._state = state;
			this._additionalContext = additional;
		}

		// Token: 0x06003269 RID: 12905 RVA: 0x0016B2BC File Offset: 0x0016A4BC
		public override bool Equals(object obj)
		{
			if (!(obj is StreamingContext))
			{
				return false;
			}
			StreamingContext streamingContext = (StreamingContext)obj;
			return streamingContext._additionalContext == this._additionalContext && streamingContext._state == this._state;
		}

		// Token: 0x0600326A RID: 12906 RVA: 0x0016B2F8 File Offset: 0x0016A4F8
		public override int GetHashCode()
		{
			return (int)this._state;
		}

		// Token: 0x170009C5 RID: 2501
		// (get) Token: 0x0600326B RID: 12907 RVA: 0x0016B2F8 File Offset: 0x0016A4F8
		public StreamingContextStates State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170009C6 RID: 2502
		// (get) Token: 0x0600326C RID: 12908 RVA: 0x0016B300 File Offset: 0x0016A500
		public object Context
		{
			get
			{
				return this._additionalContext;
			}
		}

		// Token: 0x04000E0A RID: 3594
		private readonly object _additionalContext;

		// Token: 0x04000E0B RID: 3595
		private readonly StreamingContextStates _state;
	}
}
