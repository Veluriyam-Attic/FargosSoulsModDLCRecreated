using System;

namespace System.Threading
{
	// Token: 0x020002CE RID: 718
	internal sealed class QueueUserWorkItemCallback<TState> : QueueUserWorkItemCallbackBase
	{
		// Token: 0x060028CF RID: 10447 RVA: 0x00149F87 File Offset: 0x00149187
		internal QueueUserWorkItemCallback(Action<TState> callback, TState state, ExecutionContext context)
		{
			this._callback = callback;
			this._state = state;
			this._context = context;
		}

		// Token: 0x060028D0 RID: 10448 RVA: 0x00149FA4 File Offset: 0x001491A4
		public override void Execute()
		{
			base.Execute();
			Action<TState> callback = this._callback;
			this._callback = null;
			ExecutionContext.RunForThreadPoolUnsafe<TState>(this._context, callback, this._state);
		}

		// Token: 0x04000AF7 RID: 2807
		private Action<TState> _callback;

		// Token: 0x04000AF8 RID: 2808
		private readonly TState _state;

		// Token: 0x04000AF9 RID: 2809
		private readonly ExecutionContext _context;
	}
}
