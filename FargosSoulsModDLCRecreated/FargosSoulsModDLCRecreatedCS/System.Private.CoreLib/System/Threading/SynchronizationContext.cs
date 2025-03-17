using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000269 RID: 617
	[NullableContext(1)]
	[Nullable(0)]
	public class SynchronizationContext
	{
		// Token: 0x060025A5 RID: 9637 RVA: 0x0014124A File Offset: 0x0014044A
		private static int InvokeWaitMethodHelper(SynchronizationContext syncContext, IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			return syncContext.Wait(waitHandles, waitAll, millisecondsTimeout);
		}

		// Token: 0x1700081D RID: 2077
		// (get) Token: 0x060025A7 RID: 9639 RVA: 0x00141255 File Offset: 0x00140455
		[Nullable(2)]
		public static SynchronizationContext Current
		{
			[NullableContext(2)]
			get
			{
				return Thread.CurrentThread._synchronizationContext;
			}
		}

		// Token: 0x060025A8 RID: 9640 RVA: 0x00141261 File Offset: 0x00140461
		protected void SetWaitNotificationRequired()
		{
			this._requireWaitNotification = true;
		}

		// Token: 0x060025A9 RID: 9641 RVA: 0x0014126A File Offset: 0x0014046A
		public bool IsWaitNotificationRequired()
		{
			return this._requireWaitNotification;
		}

		// Token: 0x060025AA RID: 9642 RVA: 0x00141272 File Offset: 0x00140472
		public virtual void Send(SendOrPostCallback d, [Nullable(2)] object state)
		{
			d(state);
		}

		// Token: 0x060025AB RID: 9643 RVA: 0x0014127B File Offset: 0x0014047B
		public virtual void Post(SendOrPostCallback d, [Nullable(2)] object state)
		{
			ThreadPool.QueueUserWorkItem<ValueTuple<SendOrPostCallback, object>>(delegate([TupleElementNames(new string[]
			{
				"d",
				"state"
			})] ValueTuple<SendOrPostCallback, object> s)
			{
				s.Item1(s.Item2);
			}, new ValueTuple<SendOrPostCallback, object>(d, state), false);
		}

		// Token: 0x060025AC RID: 9644 RVA: 0x000AB30B File Offset: 0x000AA50B
		public virtual void OperationStarted()
		{
		}

		// Token: 0x060025AD RID: 9645 RVA: 0x000AB30B File Offset: 0x000AA50B
		public virtual void OperationCompleted()
		{
		}

		// Token: 0x060025AE RID: 9646 RVA: 0x001412AA File Offset: 0x001404AA
		[CLSCompliant(false)]
		public virtual int Wait(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			return SynchronizationContext.WaitHelper(waitHandles, waitAll, millisecondsTimeout);
		}

		// Token: 0x060025AF RID: 9647 RVA: 0x001412B4 File Offset: 0x001404B4
		[CLSCompliant(false)]
		protected static int WaitHelper(IntPtr[] waitHandles, bool waitAll, int millisecondsTimeout)
		{
			if (waitHandles == null)
			{
				throw new ArgumentNullException("waitHandles");
			}
			return WaitHandle.WaitMultipleIgnoringSyncContext(waitHandles, waitAll, millisecondsTimeout);
		}

		// Token: 0x060025B0 RID: 9648 RVA: 0x001412D1 File Offset: 0x001404D1
		[NullableContext(2)]
		public static void SetSynchronizationContext(SynchronizationContext syncContext)
		{
			Thread.CurrentThread._synchronizationContext = syncContext;
		}

		// Token: 0x060025B1 RID: 9649 RVA: 0x001412DE File Offset: 0x001404DE
		public virtual SynchronizationContext CreateCopy()
		{
			return new SynchronizationContext();
		}

		// Token: 0x040009DC RID: 2524
		private bool _requireWaitNotification;
	}
}
