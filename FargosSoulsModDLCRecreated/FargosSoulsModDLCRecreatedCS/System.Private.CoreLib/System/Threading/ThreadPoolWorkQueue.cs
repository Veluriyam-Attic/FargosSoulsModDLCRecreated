using System;
using System.Collections.Concurrent;
using System.Diagnostics.Tracing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Internal;
using Internal.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x020002C4 RID: 708
	[StructLayout(LayoutKind.Sequential)]
	internal sealed class ThreadPoolWorkQueue
	{
		// Token: 0x060028A4 RID: 10404 RVA: 0x0014944E File Offset: 0x0014864E
		public ThreadPoolWorkQueue()
		{
			this.loggingEnabled = FrameworkEventSource.Log.IsEnabled(EventLevel.Verbose, (EventKeywords)18L);
		}

		// Token: 0x060028A5 RID: 10405 RVA: 0x00149475 File Offset: 0x00148675
		public ThreadPoolWorkQueueThreadLocals GetOrCreateThreadLocals()
		{
			return ThreadPoolWorkQueueThreadLocals.threadLocals ?? this.CreateThreadLocals();
		}

		// Token: 0x060028A6 RID: 10406 RVA: 0x00149486 File Offset: 0x00148686
		[MethodImpl(MethodImplOptions.NoInlining)]
		private ThreadPoolWorkQueueThreadLocals CreateThreadLocals()
		{
			return ThreadPoolWorkQueueThreadLocals.threadLocals = new ThreadPoolWorkQueueThreadLocals(this);
		}

		// Token: 0x060028A7 RID: 10407 RVA: 0x00149494 File Offset: 0x00148694
		internal void EnsureThreadRequested()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i < Environment.ProcessorCount; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i + 1, i);
				if (num == i)
				{
					ThreadPool.RequestWorkerThread();
					return;
				}
			}
		}

		// Token: 0x060028A8 RID: 10408 RVA: 0x001494D4 File Offset: 0x001486D4
		internal void MarkThreadRequestSatisfied()
		{
			int num;
			for (int i = this.numOutstandingThreadRequests; i > 0; i = num)
			{
				num = Interlocked.CompareExchange(ref this.numOutstandingThreadRequests, i - 1, i);
				if (num == i)
				{
					break;
				}
			}
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x00149508 File Offset: 0x00148708
		public void Enqueue(object callback, bool forceGlobal)
		{
			if (this.loggingEnabled)
			{
				FrameworkEventSource.Log.ThreadPoolEnqueueWorkObject(callback);
			}
			ThreadPoolWorkQueueThreadLocals threadPoolWorkQueueThreadLocals = null;
			if (!forceGlobal)
			{
				threadPoolWorkQueueThreadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			}
			if (threadPoolWorkQueueThreadLocals != null)
			{
				threadPoolWorkQueueThreadLocals.workStealingQueue.LocalPush(callback);
			}
			else
			{
				this.workItems.Enqueue(callback);
			}
			this.EnsureThreadRequested();
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x00149558 File Offset: 0x00148758
		internal bool LocalFindAndPop(object callback)
		{
			ThreadPoolWorkQueueThreadLocals threadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			return threadLocals != null && threadLocals.workStealingQueue.LocalFindAndPop(callback);
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x0014957C File Offset: 0x0014877C
		public object Dequeue(ThreadPoolWorkQueueThreadLocals tl, ref bool missedSteal)
		{
			ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = tl.workStealingQueue;
			object obj;
			if ((obj = workStealingQueue.LocalPop()) == null && !this.workItems.TryDequeue(out obj))
			{
				ThreadPoolWorkQueue.WorkStealingQueue[] queues = ThreadPoolWorkQueue.WorkStealingQueueList.Queues;
				int i = queues.Length;
				int num = i - 1;
				int num2 = tl.random.Next(i);
				while (i > 0)
				{
					num2 = ((num2 < num) ? (num2 + 1) : 0);
					ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue2 = queues[num2];
					if (workStealingQueue2 != workStealingQueue && workStealingQueue2.CanSteal)
					{
						obj = workStealingQueue2.TrySteal(ref missedSteal);
						if (obj != null)
						{
							break;
						}
					}
					i--;
				}
			}
			return obj;
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x060028AC RID: 10412 RVA: 0x00149600 File Offset: 0x00148800
		public long LocalCount
		{
			get
			{
				long num = 0L;
				foreach (ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue in ThreadPoolWorkQueue.WorkStealingQueueList.Queues)
				{
					num += (long)workStealingQueue.Count;
				}
				return num;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x060028AD RID: 10413 RVA: 0x00149633 File Offset: 0x00148833
		public long GlobalCount
		{
			get
			{
				return (long)this.workItems.Count;
			}
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00149644 File Offset: 0x00148844
		internal static bool Dispatch()
		{
			ThreadPoolWorkQueue s_workQueue = ThreadPool.s_workQueue;
			int tickCount = Environment.TickCount;
			s_workQueue.MarkThreadRequestSatisfied();
			s_workQueue.loggingEnabled = FrameworkEventSource.Log.IsEnabled(EventLevel.Verbose, (EventKeywords)18L);
			bool flag = true;
			bool result;
			try
			{
				ThreadPoolWorkQueue threadPoolWorkQueue = s_workQueue;
				ThreadPoolWorkQueueThreadLocals orCreateThreadLocals = threadPoolWorkQueue.GetOrCreateThreadLocals();
				Thread currentThread = orCreateThreadLocals.currentThread;
				currentThread._executionContext = null;
				currentThread._synchronizationContext = null;
				while (ThreadPool.KeepDispatching(tickCount))
				{
					bool flag2 = false;
					object obj = threadPoolWorkQueue.Dequeue(orCreateThreadLocals, ref flag2);
					if (obj == null)
					{
						flag = flag2;
						return true;
					}
					if (threadPoolWorkQueue.loggingEnabled)
					{
						FrameworkEventSource.Log.ThreadPoolDequeueWorkObject(obj);
					}
					threadPoolWorkQueue.EnsureThreadRequested();
					if (ThreadPool.EnableWorkerTracking)
					{
						bool flag3 = false;
						try
						{
							ThreadPool.ReportThreadStatus(true);
							flag3 = true;
							Task task = obj as Task;
							if (task != null)
							{
								task.ExecuteFromThreadPool(currentThread);
								goto IL_F0;
							}
							Unsafe.As<IThreadPoolWorkItem>(obj).Execute();
							goto IL_F0;
						}
						finally
						{
							if (flag3)
							{
								ThreadPool.ReportThreadStatus(false);
							}
						}
						goto IL_CC;
					}
					goto IL_CC;
					IL_F0:
					currentThread.ResetThreadPoolThread();
					ExecutionContext.ResetThreadPoolThread(currentThread);
					if (!ThreadPool.NotifyWorkItemComplete())
					{
						return false;
					}
					continue;
					IL_CC:
					Task task2 = obj as Task;
					if (task2 != null)
					{
						task2.ExecuteFromThreadPool(currentThread);
						goto IL_F0;
					}
					Unsafe.As<IThreadPoolWorkItem>(obj).Execute();
					goto IL_F0;
				}
				result = true;
			}
			finally
			{
				if (flag)
				{
					s_workQueue.EnsureThreadRequested();
				}
			}
			return result;
		}

		// Token: 0x04000ADE RID: 2782
		internal bool loggingEnabled;

		// Token: 0x04000ADF RID: 2783
		internal readonly ConcurrentQueue<object> workItems = new ConcurrentQueue<object>();

		// Token: 0x04000AE0 RID: 2784
		private readonly PaddingFor32 pad1;

		// Token: 0x04000AE1 RID: 2785
		private volatile int numOutstandingThreadRequests;

		// Token: 0x04000AE2 RID: 2786
		private readonly PaddingFor32 pad2;

		// Token: 0x020002C5 RID: 709
		internal static class WorkStealingQueueList
		{
			// Token: 0x1700087A RID: 2170
			// (get) Token: 0x060028AF RID: 10415 RVA: 0x00149798 File Offset: 0x00148998
			public static ThreadPoolWorkQueue.WorkStealingQueue[] Queues
			{
				get
				{
					return ThreadPoolWorkQueue.WorkStealingQueueList._queues;
				}
			}

			// Token: 0x060028B0 RID: 10416 RVA: 0x001497A4 File Offset: 0x001489A4
			public static void Add(ThreadPoolWorkQueue.WorkStealingQueue queue)
			{
				ThreadPoolWorkQueue.WorkStealingQueue[] queues;
				ThreadPoolWorkQueue.WorkStealingQueue[] array;
				do
				{
					queues = ThreadPoolWorkQueue.WorkStealingQueueList._queues;
					array = new ThreadPoolWorkQueue.WorkStealingQueue[queues.Length + 1];
					Array.Copy(queues, array, queues.Length);
					ThreadPoolWorkQueue.WorkStealingQueue[] array2 = array;
					array2[array2.Length - 1] = queue;
				}
				while (Interlocked.CompareExchange<ThreadPoolWorkQueue.WorkStealingQueue[]>(ref ThreadPoolWorkQueue.WorkStealingQueueList._queues, array, queues) != queues);
			}

			// Token: 0x060028B1 RID: 10417 RVA: 0x001497E8 File Offset: 0x001489E8
			public static void Remove(ThreadPoolWorkQueue.WorkStealingQueue queue)
			{
				for (;;)
				{
					ThreadPoolWorkQueue.WorkStealingQueue[] queues = ThreadPoolWorkQueue.WorkStealingQueueList._queues;
					if (queues.Length == 0)
					{
						break;
					}
					int num = Array.IndexOf<ThreadPoolWorkQueue.WorkStealingQueue>(queues, queue);
					if (num == -1)
					{
						return;
					}
					ThreadPoolWorkQueue.WorkStealingQueue[] array = new ThreadPoolWorkQueue.WorkStealingQueue[queues.Length - 1];
					if (num == 0)
					{
						Array.Copy(queues, 1, array, 0, array.Length);
					}
					else if (num == queues.Length - 1)
					{
						Array.Copy(queues, array, array.Length);
					}
					else
					{
						Array.Copy(queues, array, num);
						Array.Copy(queues, num + 1, array, num, array.Length - num);
					}
					if (Interlocked.CompareExchange<ThreadPoolWorkQueue.WorkStealingQueue[]>(ref ThreadPoolWorkQueue.WorkStealingQueueList._queues, array, queues) == queues)
					{
						return;
					}
				}
			}

			// Token: 0x04000AE3 RID: 2787
			private static volatile ThreadPoolWorkQueue.WorkStealingQueue[] _queues = new ThreadPoolWorkQueue.WorkStealingQueue[0];
		}

		// Token: 0x020002C6 RID: 710
		internal sealed class WorkStealingQueue
		{
			// Token: 0x060028B3 RID: 10419 RVA: 0x00149878 File Offset: 0x00148A78
			public void LocalPush(object obj)
			{
				int num = this.m_tailIndex;
				if (num == 2147483647)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.Enter(ref flag);
						if (this.m_tailIndex == 2147483647)
						{
							this.m_headIndex &= this.m_mask;
							num = (this.m_tailIndex &= this.m_mask);
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(true);
						}
					}
				}
				if (num < this.m_headIndex + this.m_mask)
				{
					Volatile.Write<object>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
					return;
				}
				bool flag2 = false;
				try
				{
					this.m_foreignLock.Enter(ref flag2);
					int headIndex = this.m_headIndex;
					int num2 = this.m_tailIndex - this.m_headIndex;
					if (num2 >= this.m_mask)
					{
						object[] array = new object[this.m_array.Length << 1];
						for (int i = 0; i < this.m_array.Length; i++)
						{
							array[i] = this.m_array[i + headIndex & this.m_mask];
						}
						this.m_array = array;
						this.m_headIndex = 0;
						num = (this.m_tailIndex = num2);
						this.m_mask = (this.m_mask << 1 | 1);
					}
					Volatile.Write<object>(ref this.m_array[num & this.m_mask], obj);
					this.m_tailIndex = num + 1;
				}
				finally
				{
					if (flag2)
					{
						this.m_foreignLock.Exit(false);
					}
				}
			}

			// Token: 0x060028B4 RID: 10420 RVA: 0x00149A40 File Offset: 0x00148C40
			public bool LocalFindAndPop(object obj)
			{
				if (this.m_array[this.m_tailIndex - 1 & this.m_mask] == obj)
				{
					object obj2 = this.LocalPop();
					return obj2 != null;
				}
				for (int i = this.m_tailIndex - 2; i >= this.m_headIndex; i--)
				{
					if (this.m_array[i & this.m_mask] == obj)
					{
						bool flag = false;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_array[i & this.m_mask] == null)
							{
								return false;
							}
							Volatile.Write<object>(ref this.m_array[i & this.m_mask], null);
							if (i == this.m_tailIndex)
							{
								this.m_tailIndex--;
							}
							else if (i == this.m_headIndex)
							{
								this.m_headIndex++;
							}
							return true;
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
					}
				}
				return false;
			}

			// Token: 0x060028B5 RID: 10421 RVA: 0x00149B60 File Offset: 0x00148D60
			public object LocalPop()
			{
				if (this.m_headIndex >= this.m_tailIndex)
				{
					return null;
				}
				return this.LocalPopCore();
			}

			// Token: 0x060028B6 RID: 10422 RVA: 0x00149B7C File Offset: 0x00148D7C
			private object LocalPopCore()
			{
				int num3;
				object obj2;
				for (;;)
				{
					int num = this.m_tailIndex;
					if (this.m_headIndex >= num)
					{
						break;
					}
					num--;
					Interlocked.Exchange(ref this.m_tailIndex, num);
					if (this.m_headIndex > num)
					{
						bool flag = false;
						object result;
						try
						{
							this.m_foreignLock.Enter(ref flag);
							if (this.m_headIndex <= num)
							{
								int num2 = num & this.m_mask;
								object obj = Volatile.Read<object>(ref this.m_array[num2]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num2] = null;
								result = obj;
							}
							else
							{
								this.m_tailIndex = num + 1;
								result = null;
							}
						}
						finally
						{
							if (flag)
							{
								this.m_foreignLock.Exit(false);
							}
						}
						return result;
					}
					num3 = (num & this.m_mask);
					obj2 = Volatile.Read<object>(ref this.m_array[num3]);
					if (obj2 != null)
					{
						goto Block_2;
					}
				}
				return null;
				Block_2:
				this.m_array[num3] = null;
				return obj2;
			}

			// Token: 0x1700087B RID: 2171
			// (get) Token: 0x060028B7 RID: 10423 RVA: 0x00149C74 File Offset: 0x00148E74
			public bool CanSteal
			{
				get
				{
					return this.m_headIndex < this.m_tailIndex;
				}
			}

			// Token: 0x060028B8 RID: 10424 RVA: 0x00149C88 File Offset: 0x00148E88
			public object TrySteal(ref bool missedSteal)
			{
				while (this.CanSteal)
				{
					bool flag = false;
					try
					{
						this.m_foreignLock.TryEnter(ref flag);
						if (flag)
						{
							int headIndex = this.m_headIndex;
							Interlocked.Exchange(ref this.m_headIndex, headIndex + 1);
							if (headIndex < this.m_tailIndex)
							{
								int num = headIndex & this.m_mask;
								object obj = Volatile.Read<object>(ref this.m_array[num]);
								if (obj == null)
								{
									continue;
								}
								this.m_array[num] = null;
								return obj;
							}
							else
							{
								this.m_headIndex = headIndex;
							}
						}
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(false);
						}
					}
					missedSteal = true;
					break;
				}
				return null;
			}

			// Token: 0x1700087C RID: 2172
			// (get) Token: 0x060028B9 RID: 10425 RVA: 0x00149D3C File Offset: 0x00148F3C
			public int Count
			{
				get
				{
					bool flag = false;
					int result;
					try
					{
						this.m_foreignLock.Enter(ref flag);
						result = Math.Max(0, this.m_tailIndex - this.m_headIndex);
					}
					finally
					{
						if (flag)
						{
							this.m_foreignLock.Exit(false);
						}
					}
					return result;
				}
			}

			// Token: 0x04000AE4 RID: 2788
			internal volatile object[] m_array = new object[32];

			// Token: 0x04000AE5 RID: 2789
			private volatile int m_mask = 31;

			// Token: 0x04000AE6 RID: 2790
			private volatile int m_headIndex;

			// Token: 0x04000AE7 RID: 2791
			private volatile int m_tailIndex;

			// Token: 0x04000AE8 RID: 2792
			private SpinLock m_foreignLock = new SpinLock(false);
		}
	}
}
