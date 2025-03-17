using System;
using System.Runtime.CompilerServices;
using System.Threading;

namespace System
{
	// Token: 0x02000170 RID: 368
	[NullableContext(1)]
	[Nullable(0)]
	public class Progress<[Nullable(2)] T> : IProgress<T>
	{
		// Token: 0x0600127D RID: 4733 RVA: 0x000E78C7 File Offset: 0x000E6AC7
		public Progress()
		{
			this._synchronizationContext = (SynchronizationContext.Current ?? ProgressStatics.DefaultContext);
			this._invokeHandlers = new SendOrPostCallback(this.InvokeHandlers);
		}

		// Token: 0x0600127E RID: 4734 RVA: 0x000E78F5 File Offset: 0x000E6AF5
		public Progress(Action<T> handler) : this()
		{
			if (handler == null)
			{
				throw new ArgumentNullException("handler");
			}
			this._handler = handler;
		}

		// Token: 0x1400000E RID: 14
		// (add) Token: 0x0600127F RID: 4735 RVA: 0x000E7914 File Offset: 0x000E6B14
		// (remove) Token: 0x06001280 RID: 4736 RVA: 0x000E794C File Offset: 0x000E6B4C
		[Nullable(new byte[]
		{
			2,
			1
		})]
		public event EventHandler<T> ProgressChanged;

		// Token: 0x06001281 RID: 4737 RVA: 0x000E7984 File Offset: 0x000E6B84
		protected virtual void OnReport(T value)
		{
			Action<T> handler = this._handler;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler != null || progressChanged != null)
			{
				this._synchronizationContext.Post(this._invokeHandlers, value);
			}
		}

		// Token: 0x06001282 RID: 4738 RVA: 0x000E79BC File Offset: 0x000E6BBC
		void IProgress<!0>.Report(T value)
		{
			this.OnReport(value);
		}

		// Token: 0x06001283 RID: 4739 RVA: 0x000E79C8 File Offset: 0x000E6BC8
		private void InvokeHandlers(object state)
		{
			T t = (T)((object)state);
			Action<T> handler = this._handler;
			EventHandler<T> progressChanged = this.ProgressChanged;
			if (handler != null)
			{
				handler(t);
			}
			if (progressChanged != null)
			{
				progressChanged(this, t);
			}
		}

		// Token: 0x04000474 RID: 1140
		private readonly SynchronizationContext _synchronizationContext;

		// Token: 0x04000475 RID: 1141
		private readonly Action<T> _handler;

		// Token: 0x04000476 RID: 1142
		private readonly SendOrPostCallback _invokeHandlers;
	}
}
