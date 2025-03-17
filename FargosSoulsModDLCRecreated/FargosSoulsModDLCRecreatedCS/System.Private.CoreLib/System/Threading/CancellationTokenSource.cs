using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x0200028C RID: 652
	[NullableContext(1)]
	[Nullable(0)]
	public class CancellationTokenSource : IDisposable
	{
		// Token: 0x17000847 RID: 2119
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x00144199 File Offset: 0x00143399
		public bool IsCancellationRequested
		{
			get
			{
				return this._state >= 2;
			}
		}

		// Token: 0x17000848 RID: 2120
		// (get) Token: 0x0600272E RID: 10030 RVA: 0x001441A9 File Offset: 0x001433A9
		internal bool IsCancellationCompleted
		{
			get
			{
				return this._state == 3;
			}
		}

		// Token: 0x17000849 RID: 2121
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x001441B6 File Offset: 0x001433B6
		// (set) Token: 0x06002730 RID: 10032 RVA: 0x001441C0 File Offset: 0x001433C0
		internal int ThreadIDExecutingCallbacks
		{
			get
			{
				return this._threadIDExecutingCallbacks;
			}
			set
			{
				this._threadIDExecutingCallbacks = value;
			}
		}

		// Token: 0x1700084A RID: 2122
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x001441CB File Offset: 0x001433CB
		public CancellationToken Token
		{
			get
			{
				this.ThrowIfDisposed();
				return new CancellationToken(this);
			}
		}

		// Token: 0x1700084B RID: 2123
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x001441DC File Offset: 0x001433DC
		internal WaitHandle WaitHandle
		{
			get
			{
				this.ThrowIfDisposed();
				if (this._kernelEvent != null)
				{
					return this._kernelEvent;
				}
				ManualResetEvent manualResetEvent = new ManualResetEvent(false);
				if (Interlocked.CompareExchange<ManualResetEvent>(ref this._kernelEvent, manualResetEvent, null) != null)
				{
					manualResetEvent.Dispose();
				}
				if (this.IsCancellationRequested)
				{
					this._kernelEvent.Set();
				}
				return this._kernelEvent;
			}
		}

		// Token: 0x1700084C RID: 2124
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x0014423C File Offset: 0x0014343C
		internal long ExecutingCallback
		{
			get
			{
				return Volatile.Read(ref this._executingCallbackId);
			}
		}

		// Token: 0x06002734 RID: 10036 RVA: 0x00144249 File Offset: 0x00143449
		public CancellationTokenSource()
		{
			this._state = 1;
		}

		// Token: 0x06002735 RID: 10037 RVA: 0x00144264 File Offset: 0x00143464
		public CancellationTokenSource(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.InitializeWithTimer((int)num);
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x001442AA File Offset: 0x001434AA
		public CancellationTokenSource(int millisecondsDelay)
		{
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			this.InitializeWithTimer(millisecondsDelay);
		}

		// Token: 0x06002737 RID: 10039 RVA: 0x001442D1 File Offset: 0x001434D1
		private void InitializeWithTimer(int millisecondsDelay)
		{
			if (millisecondsDelay == 0)
			{
				this._state = 3;
				return;
			}
			this._state = 1;
			this._timer = new TimerQueueTimer(CancellationTokenSource.s_timerCallback, this, (uint)millisecondsDelay, uint.MaxValue, false);
		}

		// Token: 0x06002738 RID: 10040 RVA: 0x001442FF File Offset: 0x001434FF
		public void Cancel()
		{
			this.Cancel(false);
		}

		// Token: 0x06002739 RID: 10041 RVA: 0x00144308 File Offset: 0x00143508
		public void Cancel(bool throwOnFirstException)
		{
			this.ThrowIfDisposed();
			this.NotifyCancellation(throwOnFirstException);
		}

		// Token: 0x0600273A RID: 10042 RVA: 0x00144318 File Offset: 0x00143518
		public void CancelAfter(TimeSpan delay)
		{
			long num = (long)delay.TotalMilliseconds;
			if (num < -1L || num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("delay");
			}
			this.CancelAfter((int)num);
		}

		// Token: 0x0600273B RID: 10043 RVA: 0x00144350 File Offset: 0x00143550
		public void CancelAfter(int millisecondsDelay)
		{
			this.ThrowIfDisposed();
			if (millisecondsDelay < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsDelay");
			}
			if (this.IsCancellationRequested)
			{
				return;
			}
			TimerQueueTimer timerQueueTimer = this._timer;
			if (timerQueueTimer == null)
			{
				timerQueueTimer = new TimerQueueTimer(CancellationTokenSource.s_timerCallback, this, uint.MaxValue, uint.MaxValue, false);
				TimerQueueTimer timerQueueTimer2 = Interlocked.CompareExchange<TimerQueueTimer>(ref this._timer, timerQueueTimer, null);
				if (timerQueueTimer2 != null)
				{
					timerQueueTimer.Close();
					timerQueueTimer = timerQueueTimer2;
				}
			}
			try
			{
				timerQueueTimer.Change((uint)millisecondsDelay, uint.MaxValue);
			}
			catch (ObjectDisposedException)
			{
			}
		}

		// Token: 0x0600273C RID: 10044 RVA: 0x001443D0 File Offset: 0x001435D0
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600273D RID: 10045 RVA: 0x001443E0 File Offset: 0x001435E0
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && !this._disposed)
			{
				TimerQueueTimer timer = this._timer;
				if (timer != null)
				{
					this._timer = null;
					timer.Close();
				}
				this._callbackPartitions = null;
				if (this._kernelEvent != null)
				{
					ManualResetEvent manualResetEvent = Interlocked.Exchange<ManualResetEvent>(ref this._kernelEvent, null);
					if (manualResetEvent != null && this._state != 2)
					{
						manualResetEvent.Dispose();
					}
				}
				this._disposed = true;
			}
		}

		// Token: 0x0600273E RID: 10046 RVA: 0x0014444E File Offset: 0x0014364E
		private void ThrowIfDisposed()
		{
			if (this._disposed)
			{
				CancellationTokenSource.ThrowObjectDisposedException();
			}
		}

		// Token: 0x0600273F RID: 10047 RVA: 0x0014445D File Offset: 0x0014365D
		[DoesNotReturn]
		private static void ThrowObjectDisposedException()
		{
			throw new ObjectDisposedException(null, SR.CancellationTokenSource_Disposed);
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x0014446C File Offset: 0x0014366C
		internal CancellationTokenRegistration InternalRegister(Action<object> callback, object stateForCallback, SynchronizationContext syncContext, ExecutionContext executionContext)
		{
			if (!this.IsCancellationRequested)
			{
				if (this._disposed)
				{
					return default(CancellationTokenRegistration);
				}
				CancellationTokenSource.CallbackPartition[] array = this._callbackPartitions;
				if (array == null)
				{
					array = new CancellationTokenSource.CallbackPartition[CancellationTokenSource.s_numPartitions];
					array = (Interlocked.CompareExchange<CancellationTokenSource.CallbackPartition[]>(ref this._callbackPartitions, array, null) ?? array);
				}
				int num = Environment.CurrentManagedThreadId & CancellationTokenSource.s_numPartitionsMask;
				CancellationTokenSource.CallbackPartition callbackPartition = array[num];
				if (callbackPartition == null)
				{
					callbackPartition = new CancellationTokenSource.CallbackPartition(this);
					callbackPartition = (Interlocked.CompareExchange<CancellationTokenSource.CallbackPartition>(ref array[num], callbackPartition, null) ?? callbackPartition);
				}
				bool flag = false;
				callbackPartition.Lock.Enter(ref flag);
				long id;
				CancellationTokenSource.CallbackNode callbackNode;
				try
				{
					CancellationTokenSource.CallbackPartition callbackPartition2 = callbackPartition;
					long nextAvailableId = callbackPartition2.NextAvailableId;
					callbackPartition2.NextAvailableId = nextAvailableId + 1L;
					id = nextAvailableId;
					callbackNode = callbackPartition.FreeNodeList;
					if (callbackNode != null)
					{
						callbackPartition.FreeNodeList = callbackNode.Next;
					}
					else
					{
						callbackNode = new CancellationTokenSource.CallbackNode(callbackPartition);
					}
					callbackNode.Id = id;
					callbackNode.Callback = callback;
					callbackNode.CallbackState = stateForCallback;
					callbackNode.ExecutionContext = executionContext;
					callbackNode.SynchronizationContext = syncContext;
					callbackNode.Next = callbackPartition.Callbacks;
					if (callbackNode.Next != null)
					{
						callbackNode.Next.Prev = callbackNode;
					}
					callbackPartition.Callbacks = callbackNode;
				}
				finally
				{
					callbackPartition.Lock.Exit(false);
				}
				CancellationTokenRegistration result = new CancellationTokenRegistration(id, callbackNode);
				if (!this.IsCancellationRequested || !callbackPartition.Unregister(id, callbackNode))
				{
					return result;
				}
			}
			callback(stateForCallback);
			return default(CancellationTokenRegistration);
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x001445E0 File Offset: 0x001437E0
		private void NotifyCancellation(bool throwOnFirstException)
		{
			if (!this.IsCancellationRequested && Interlocked.CompareExchange(ref this._state, 2, 1) == 1)
			{
				TimerQueueTimer timer = this._timer;
				if (timer != null)
				{
					this._timer = null;
					timer.Close();
				}
				ManualResetEvent kernelEvent = this._kernelEvent;
				if (kernelEvent != null)
				{
					kernelEvent.Set();
				}
				this.ExecuteCallbackHandlers(throwOnFirstException);
			}
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x0014463C File Offset: 0x0014383C
		private void ExecuteCallbackHandlers(bool throwOnFirstException)
		{
			this.ThreadIDExecutingCallbacks = Environment.CurrentManagedThreadId;
			CancellationTokenSource.CallbackPartition[] array = Interlocked.Exchange<CancellationTokenSource.CallbackPartition[]>(ref this._callbackPartitions, null);
			if (array == null)
			{
				Interlocked.Exchange(ref this._state, 3);
				return;
			}
			List<Exception> list = null;
			try
			{
				foreach (CancellationTokenSource.CallbackPartition callbackPartition in array)
				{
					if (callbackPartition != null)
					{
						for (;;)
						{
							bool flag = false;
							callbackPartition.Lock.Enter(ref flag);
							CancellationTokenSource.CallbackNode callbacks;
							try
							{
								callbacks = callbackPartition.Callbacks;
								if (callbacks == null)
								{
									break;
								}
								if (callbacks.Next != null)
								{
									callbacks.Next.Prev = null;
								}
								callbackPartition.Callbacks = callbacks.Next;
								this._executingCallbackId = callbacks.Id;
								callbacks.Id = 0L;
							}
							finally
							{
								callbackPartition.Lock.Exit(false);
							}
							try
							{
								if (callbacks.SynchronizationContext != null)
								{
									callbacks.SynchronizationContext.Send(delegate(object s)
									{
										CancellationTokenSource.CallbackNode callbackNode = (CancellationTokenSource.CallbackNode)s;
										callbackNode.Partition.Source.ThreadIDExecutingCallbacks = Environment.CurrentManagedThreadId;
										callbackNode.ExecuteCallback();
									}, callbacks);
									this.ThreadIDExecutingCallbacks = Environment.CurrentManagedThreadId;
								}
								else
								{
									callbacks.ExecuteCallback();
								}
								continue;
							}
							catch (Exception item) when (!throwOnFirstException)
							{
								List<Exception> list2;
								if ((list2 = list) == null)
								{
									list2 = (list = new List<Exception>());
								}
								list2.Add(item);
								continue;
							}
							break;
						}
					}
				}
			}
			finally
			{
				this._state = 3;
				Volatile.Write(ref this._executingCallbackId, 0L);
				Interlocked.MemoryBarrier();
			}
			if (list != null)
			{
				throw new AggregateException(list);
			}
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x001447F8 File Offset: 0x001439F8
		private static int GetPartitionCount()
		{
			int processorCount = Environment.ProcessorCount;
			return (processorCount > 8) ? 16 : ((processorCount > 4) ? 8 : ((processorCount > 2) ? 4 : ((processorCount > 1) ? 2 : 1)));
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x0014482B File Offset: 0x00143A2B
		public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token1, CancellationToken token2)
		{
			if (!token1.CanBeCanceled)
			{
				return CancellationTokenSource.CreateLinkedTokenSource(token2);
			}
			if (!token2.CanBeCanceled)
			{
				return new CancellationTokenSource.Linked1CancellationTokenSource(token1);
			}
			return new CancellationTokenSource.Linked2CancellationTokenSource(token1, token2);
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x00144854 File Offset: 0x00143A54
		public static CancellationTokenSource CreateLinkedTokenSource(CancellationToken token)
		{
			if (!token.CanBeCanceled)
			{
				return new CancellationTokenSource();
			}
			return new CancellationTokenSource.Linked1CancellationTokenSource(token);
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x0014486C File Offset: 0x00143A6C
		public static CancellationTokenSource CreateLinkedTokenSource(params CancellationToken[] tokens)
		{
			if (tokens == null)
			{
				throw new ArgumentNullException("tokens");
			}
			CancellationTokenSource result;
			switch (tokens.Length)
			{
			case 0:
				throw new ArgumentException(SR.CancellationToken_CreateLinkedToken_TokensIsEmpty);
			case 1:
				result = CancellationTokenSource.CreateLinkedTokenSource(tokens[0]);
				break;
			case 2:
				result = CancellationTokenSource.CreateLinkedTokenSource(tokens[0], tokens[1]);
				break;
			default:
				result = new CancellationTokenSource.LinkedNCancellationTokenSource(tokens);
				break;
			}
			return result;
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x001448D8 File Offset: 0x00143AD8
		internal void WaitForCallbackToComplete(long id)
		{
			SpinWait spinWait = default(SpinWait);
			while (this.ExecutingCallback == id)
			{
				spinWait.SpinOnce();
			}
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x00144900 File Offset: 0x00143B00
		internal ValueTask WaitForCallbackToCompleteAsync(long id)
		{
			if (this.ExecutingCallback != id)
			{
				return default(ValueTask);
			}
			return new ValueTask(Task.Factory.StartNew(delegate(object s)
			{
				Tuple<CancellationTokenSource, long> tuple = (Tuple<CancellationTokenSource, long>)s;
				tuple.Item1.WaitForCallbackToComplete(tuple.Item2);
			}, Tuple.Create<CancellationTokenSource, long>(this, id), CancellationToken.None, TaskCreationOptions.None, TaskScheduler.Default));
		}

		// Token: 0x04000A3F RID: 2623
		internal static readonly CancellationTokenSource s_canceledSource = new CancellationTokenSource
		{
			_state = 3
		};

		// Token: 0x04000A40 RID: 2624
		internal static readonly CancellationTokenSource s_neverCanceledSource = new CancellationTokenSource();

		// Token: 0x04000A41 RID: 2625
		private static readonly TimerCallback s_timerCallback = delegate(object obj)
		{
			((CancellationTokenSource)obj).NotifyCancellation(false);
		};

		// Token: 0x04000A42 RID: 2626
		private static readonly int s_numPartitions = CancellationTokenSource.GetPartitionCount();

		// Token: 0x04000A43 RID: 2627
		private static readonly int s_numPartitionsMask = CancellationTokenSource.s_numPartitions - 1;

		// Token: 0x04000A44 RID: 2628
		private volatile int _state;

		// Token: 0x04000A45 RID: 2629
		private volatile int _threadIDExecutingCallbacks = -1;

		// Token: 0x04000A46 RID: 2630
		private long _executingCallbackId;

		// Token: 0x04000A47 RID: 2631
		private volatile CancellationTokenSource.CallbackPartition[] _callbackPartitions;

		// Token: 0x04000A48 RID: 2632
		private volatile TimerQueueTimer _timer;

		// Token: 0x04000A49 RID: 2633
		private volatile ManualResetEvent _kernelEvent;

		// Token: 0x04000A4A RID: 2634
		private bool _disposed;

		// Token: 0x0200028D RID: 653
		private sealed class Linked1CancellationTokenSource : CancellationTokenSource
		{
			// Token: 0x0600274A RID: 10058 RVA: 0x001449B5 File Offset: 0x00143BB5
			internal Linked1CancellationTokenSource(CancellationToken token1)
			{
				this._reg1 = token1.UnsafeRegister(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
			}

			// Token: 0x0600274B RID: 10059 RVA: 0x001449D0 File Offset: 0x00143BD0
			protected override void Dispose(bool disposing)
			{
				if (!disposing || this._disposed)
				{
					return;
				}
				this._reg1.Dispose();
				base.Dispose(disposing);
			}

			// Token: 0x04000A4B RID: 2635
			private readonly CancellationTokenRegistration _reg1;
		}

		// Token: 0x0200028E RID: 654
		private sealed class Linked2CancellationTokenSource : CancellationTokenSource
		{
			// Token: 0x0600274C RID: 10060 RVA: 0x001449F0 File Offset: 0x00143BF0
			internal Linked2CancellationTokenSource(CancellationToken token1, CancellationToken token2)
			{
				this._reg1 = token1.UnsafeRegister(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
				this._reg2 = token2.UnsafeRegister(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
			}

			// Token: 0x0600274D RID: 10061 RVA: 0x00144A1E File Offset: 0x00143C1E
			protected override void Dispose(bool disposing)
			{
				if (!disposing || this._disposed)
				{
					return;
				}
				this._reg1.Dispose();
				this._reg2.Dispose();
				base.Dispose(disposing);
			}

			// Token: 0x04000A4C RID: 2636
			private readonly CancellationTokenRegistration _reg1;

			// Token: 0x04000A4D RID: 2637
			private readonly CancellationTokenRegistration _reg2;
		}

		// Token: 0x0200028F RID: 655
		private sealed class LinkedNCancellationTokenSource : CancellationTokenSource
		{
			// Token: 0x0600274E RID: 10062 RVA: 0x00144A4C File Offset: 0x00143C4C
			internal LinkedNCancellationTokenSource(params CancellationToken[] tokens)
			{
				this._linkingRegistrations = new CancellationTokenRegistration[tokens.Length];
				for (int i = 0; i < tokens.Length; i++)
				{
					if (tokens[i].CanBeCanceled)
					{
						this._linkingRegistrations[i] = tokens[i].UnsafeRegister(CancellationTokenSource.LinkedNCancellationTokenSource.s_linkedTokenCancelDelegate, this);
					}
				}
			}

			// Token: 0x0600274F RID: 10063 RVA: 0x00144AA8 File Offset: 0x00143CA8
			protected override void Dispose(bool disposing)
			{
				if (!disposing || this._disposed)
				{
					return;
				}
				CancellationTokenRegistration[] linkingRegistrations = this._linkingRegistrations;
				if (linkingRegistrations != null)
				{
					this._linkingRegistrations = null;
					for (int i = 0; i < linkingRegistrations.Length; i++)
					{
						linkingRegistrations[i].Dispose();
					}
				}
				base.Dispose(disposing);
			}

			// Token: 0x04000A4E RID: 2638
			internal static readonly Action<object> s_linkedTokenCancelDelegate = delegate(object s)
			{
				((CancellationTokenSource)s).NotifyCancellation(false);
			};

			// Token: 0x04000A4F RID: 2639
			private CancellationTokenRegistration[] _linkingRegistrations;
		}

		// Token: 0x02000291 RID: 657
		internal sealed class CallbackPartition
		{
			// Token: 0x06002754 RID: 10068 RVA: 0x00144B24 File Offset: 0x00143D24
			public CallbackPartition(CancellationTokenSource source)
			{
				this.Source = source;
			}

			// Token: 0x06002755 RID: 10069 RVA: 0x00144B48 File Offset: 0x00143D48
			internal bool Unregister(long id, CancellationTokenSource.CallbackNode node)
			{
				if (id == 0L)
				{
					return false;
				}
				bool flag = false;
				this.Lock.Enter(ref flag);
				bool result;
				try
				{
					if (node.Id != id)
					{
						result = false;
					}
					else
					{
						if (this.Callbacks == node)
						{
							this.Callbacks = node.Next;
						}
						else
						{
							node.Prev.Next = node.Next;
						}
						if (node.Next != null)
						{
							node.Next.Prev = node.Prev;
						}
						node.Id = 0L;
						node.Callback = null;
						node.CallbackState = null;
						node.ExecutionContext = null;
						node.SynchronizationContext = null;
						node.Prev = null;
						node.Next = this.FreeNodeList;
						this.FreeNodeList = node;
						result = true;
					}
				}
				finally
				{
					this.Lock.Exit(false);
				}
				return result;
			}

			// Token: 0x04000A51 RID: 2641
			public readonly CancellationTokenSource Source;

			// Token: 0x04000A52 RID: 2642
			public SpinLock Lock = new SpinLock(false);

			// Token: 0x04000A53 RID: 2643
			public CancellationTokenSource.CallbackNode Callbacks;

			// Token: 0x04000A54 RID: 2644
			public CancellationTokenSource.CallbackNode FreeNodeList;

			// Token: 0x04000A55 RID: 2645
			public long NextAvailableId = 1L;
		}

		// Token: 0x02000292 RID: 658
		internal sealed class CallbackNode
		{
			// Token: 0x06002756 RID: 10070 RVA: 0x00144C1C File Offset: 0x00143E1C
			public CallbackNode(CancellationTokenSource.CallbackPartition partition)
			{
				this.Partition = partition;
			}

			// Token: 0x06002757 RID: 10071 RVA: 0x00144C2C File Offset: 0x00143E2C
			public void ExecuteCallback()
			{
				ExecutionContext executionContext = this.ExecutionContext;
				if (executionContext != null)
				{
					ExecutionContext.RunInternal(executionContext, delegate(object s)
					{
						CancellationTokenSource.CallbackNode callbackNode = (CancellationTokenSource.CallbackNode)s;
						callbackNode.Callback(callbackNode.CallbackState);
					}, this);
					return;
				}
				this.Callback(this.CallbackState);
			}

			// Token: 0x04000A56 RID: 2646
			public readonly CancellationTokenSource.CallbackPartition Partition;

			// Token: 0x04000A57 RID: 2647
			public CancellationTokenSource.CallbackNode Prev;

			// Token: 0x04000A58 RID: 2648
			public CancellationTokenSource.CallbackNode Next;

			// Token: 0x04000A59 RID: 2649
			public long Id;

			// Token: 0x04000A5A RID: 2650
			public Action<object> Callback;

			// Token: 0x04000A5B RID: 2651
			public object CallbackState;

			// Token: 0x04000A5C RID: 2652
			public ExecutionContext ExecutionContext;

			// Token: 0x04000A5D RID: 2653
			public SynchronizationContext SynchronizationContext;
		}
	}
}
