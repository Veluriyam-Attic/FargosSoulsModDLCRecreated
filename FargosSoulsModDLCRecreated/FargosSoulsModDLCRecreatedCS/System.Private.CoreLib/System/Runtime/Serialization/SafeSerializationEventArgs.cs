using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Serialization
{
	// Token: 0x020003E8 RID: 1000
	public sealed class SafeSerializationEventArgs : EventArgs
	{
		// Token: 0x0600321A RID: 12826 RVA: 0x000AB30B File Offset: 0x000AA50B
		[NullableContext(1)]
		public void AddSerializedState(ISafeSerializationData serializedState)
		{
		}

		// Token: 0x170009B4 RID: 2484
		// (get) Token: 0x0600321B RID: 12827 RVA: 0x0016A684 File Offset: 0x00169884
		public StreamingContext StreamingContext { get; }
	}
}
