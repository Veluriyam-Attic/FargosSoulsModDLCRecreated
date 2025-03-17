using System;

namespace System.Runtime.Serialization
{
	// Token: 0x020003DC RID: 988
	public readonly struct DeserializationToken : IDisposable
	{
		// Token: 0x060031F9 RID: 12793 RVA: 0x0016A5C6 File Offset: 0x001697C6
		internal DeserializationToken(DeserializationTracker tracker)
		{
			this._tracker = tracker;
		}

		// Token: 0x060031FA RID: 12794 RVA: 0x0016A5D0 File Offset: 0x001697D0
		public void Dispose()
		{
			if (this._tracker != null && this._tracker.DeserializationInProgress)
			{
				DeserializationTracker tracker = this._tracker;
				lock (tracker)
				{
					if (this._tracker.DeserializationInProgress)
					{
						this._tracker.DeserializationInProgress = false;
						SerializationInfo.AsyncDeserializationInProgress.Value = false;
					}
				}
			}
		}

		// Token: 0x04000DF1 RID: 3569
		private readonly DeserializationTracker _tracker;
	}
}
