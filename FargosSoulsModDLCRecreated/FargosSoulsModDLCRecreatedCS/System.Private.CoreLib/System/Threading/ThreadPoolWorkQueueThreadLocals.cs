using System;

namespace System.Threading
{
	// Token: 0x020002C8 RID: 712
	internal sealed class ThreadPoolWorkQueueThreadLocals
	{
		// Token: 0x060028BD RID: 10429 RVA: 0x00149E54 File Offset: 0x00149054
		public ThreadPoolWorkQueueThreadLocals(ThreadPoolWorkQueue tpq)
		{
			this.workQueue = tpq;
			this.workStealingQueue = new ThreadPoolWorkQueue.WorkStealingQueue();
			ThreadPoolWorkQueue.WorkStealingQueueList.Add(this.workStealingQueue);
			this.currentThread = Thread.CurrentThread;
		}

		// Token: 0x060028BE RID: 10430 RVA: 0x00149EA4 File Offset: 0x001490A4
		protected override void Finalize()
		{
			try
			{
				if (this.workStealingQueue != null)
				{
					if (this.workQueue != null)
					{
						object callback;
						while ((callback = this.workStealingQueue.LocalPop()) != null)
						{
							this.workQueue.Enqueue(callback, true);
						}
					}
					ThreadPoolWorkQueue.WorkStealingQueueList.Remove(this.workStealingQueue);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x04000AED RID: 2797
		[ThreadStatic]
		public static ThreadPoolWorkQueueThreadLocals threadLocals;

		// Token: 0x04000AEE RID: 2798
		public readonly ThreadPoolWorkQueue workQueue;

		// Token: 0x04000AEF RID: 2799
		public readonly ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue;

		// Token: 0x04000AF0 RID: 2800
		public readonly Thread currentThread;

		// Token: 0x04000AF1 RID: 2801
		public FastRandom random = new FastRandom(Thread.CurrentThread.ManagedThreadId);
	}
}
