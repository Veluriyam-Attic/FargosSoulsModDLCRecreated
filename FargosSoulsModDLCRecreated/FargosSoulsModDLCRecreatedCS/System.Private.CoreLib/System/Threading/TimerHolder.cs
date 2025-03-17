using System;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x020002DC RID: 732
	internal sealed class TimerHolder
	{
		// Token: 0x060028FA RID: 10490 RVA: 0x0014A651 File Offset: 0x00149851
		public TimerHolder(TimerQueueTimer timer)
		{
			this._timer = timer;
		}

		// Token: 0x060028FB RID: 10491 RVA: 0x0014A660 File Offset: 0x00149860
		~TimerHolder()
		{
			this._timer.Close();
		}

		// Token: 0x060028FC RID: 10492 RVA: 0x0014A694 File Offset: 0x00149894
		public void Close()
		{
			this._timer.Close();
			GC.SuppressFinalize(this);
		}

		// Token: 0x060028FD RID: 10493 RVA: 0x0014A6A8 File Offset: 0x001498A8
		public bool Close(WaitHandle notifyObject)
		{
			bool result = this._timer.Close(notifyObject);
			GC.SuppressFinalize(this);
			return result;
		}

		// Token: 0x060028FE RID: 10494 RVA: 0x0014A6CC File Offset: 0x001498CC
		public ValueTask CloseAsync()
		{
			ValueTask result = this._timer.CloseAsync();
			GC.SuppressFinalize(this);
			return result;
		}

		// Token: 0x04000B25 RID: 2853
		internal readonly TimerQueueTimer _timer;
	}
}
