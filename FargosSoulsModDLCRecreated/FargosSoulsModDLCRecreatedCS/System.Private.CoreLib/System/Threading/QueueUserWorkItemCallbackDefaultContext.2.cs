using System;

namespace System.Threading
{
	// Token: 0x020002D0 RID: 720
	internal sealed class QueueUserWorkItemCallbackDefaultContext<TState> : QueueUserWorkItemCallbackBase
	{
		// Token: 0x060028D3 RID: 10451 RVA: 0x0014A01D File Offset: 0x0014921D
		internal QueueUserWorkItemCallbackDefaultContext(Action<TState> callback, TState state)
		{
			this._callback = callback;
			this._state = state;
		}

		// Token: 0x060028D4 RID: 10452 RVA: 0x0014A034 File Offset: 0x00149234
		public override void Execute()
		{
			base.Execute();
			Action<TState> callback = this._callback;
			this._callback = null;
			callback(this._state);
		}

		// Token: 0x04000AFC RID: 2812
		private Action<TState> _callback;

		// Token: 0x04000AFD RID: 2813
		private readonly TState _state;
	}
}
