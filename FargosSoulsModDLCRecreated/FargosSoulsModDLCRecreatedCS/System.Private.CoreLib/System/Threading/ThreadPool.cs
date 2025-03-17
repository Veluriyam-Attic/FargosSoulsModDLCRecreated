using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Runtime.Versioning;
using System.Threading.Tasks;

namespace System.Threading
{
	// Token: 0x02000273 RID: 627
	[NullableContext(1)]
	[Nullable(0)]
	public static class ThreadPool
	{
		// Token: 0x06002657 RID: 9815 RVA: 0x00141F04 File Offset: 0x00141104
		internal static bool KeepDispatching(int startTickCount)
		{
			return Environment.TickCount - startTickCount < 30;
		}

		// Token: 0x06002658 RID: 9816 RVA: 0x00141F11 File Offset: 0x00141111
		public static bool SetMaxThreads(int workerThreads, int completionPortThreads)
		{
			return workerThreads >= 0 && completionPortThreads >= 0 && ThreadPool.SetMaxThreadsNative(workerThreads, completionPortThreads);
		}

		// Token: 0x06002659 RID: 9817 RVA: 0x00141F24 File Offset: 0x00141124
		public static void GetMaxThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMaxThreadsNative(out workerThreads, out completionPortThreads);
		}

		// Token: 0x0600265A RID: 9818 RVA: 0x00141F2D File Offset: 0x0014112D
		public static bool SetMinThreads(int workerThreads, int completionPortThreads)
		{
			return workerThreads >= 0 && completionPortThreads >= 0 && ThreadPool.SetMinThreadsNative(workerThreads, completionPortThreads);
		}

		// Token: 0x0600265B RID: 9819 RVA: 0x00141F40 File Offset: 0x00141140
		public static void GetMinThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetMinThreadsNative(out workerThreads, out completionPortThreads);
		}

		// Token: 0x0600265C RID: 9820 RVA: 0x00141F49 File Offset: 0x00141149
		public static void GetAvailableThreads(out int workerThreads, out int completionPortThreads)
		{
			ThreadPool.GetAvailableThreadsNative(out workerThreads, out completionPortThreads);
		}

		// Token: 0x1700082E RID: 2094
		// (get) Token: 0x0600265D RID: 9821
		public static extern int ThreadCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x1700082F RID: 2095
		// (get) Token: 0x0600265E RID: 9822 RVA: 0x00141F52 File Offset: 0x00141152
		public static long CompletedWorkItemCount
		{
			get
			{
				return ThreadPool.GetCompletedWorkItemCount();
			}
		}

		// Token: 0x0600265F RID: 9823
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		private static extern long GetCompletedWorkItemCount();

		// Token: 0x17000830 RID: 2096
		// (get) Token: 0x06002660 RID: 9824
		private static extern long PendingUnmanagedWorkItemCount { [MethodImpl(MethodImplOptions.InternalCall)] get; }

		// Token: 0x06002661 RID: 9825 RVA: 0x00141F5C File Offset: 0x0014115C
		private static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce, bool compressStack)
		{
			RegisteredWaitHandle registeredWaitHandle = new RegisteredWaitHandle();
			if (callBack != null)
			{
				_ThreadPoolWaitOrTimerCallback threadPoolWaitOrTimerCallback = new _ThreadPoolWaitOrTimerCallback(callBack, state, compressStack);
				state = threadPoolWaitOrTimerCallback;
				registeredWaitHandle.SetWaitObject(waitObject);
				IntPtr handle = ThreadPool.RegisterWaitForSingleObjectNative(waitObject, state, millisecondsTimeOutInterval, executeOnlyOnce, registeredWaitHandle);
				registeredWaitHandle.SetHandle(handle);
				return registeredWaitHandle;
			}
			throw new ArgumentNullException("WaitOrTimerCallback");
		}

		// Token: 0x06002662 RID: 9826
		[DllImport("QCall", CharSet = CharSet.Unicode)]
		internal static extern Interop.BOOL RequestWorkerThread();

		// Token: 0x06002663 RID: 9827
		[MethodImpl(MethodImplOptions.InternalCall)]
		private unsafe static extern bool PostQueuedCompletionStatus(NativeOverlapped* overlapped);

		// Token: 0x06002664 RID: 9828 RVA: 0x00141FA7 File Offset: 0x001411A7
		[NullableContext(0)]
		[CLSCompliant(false)]
		public unsafe static bool UnsafeQueueNativeOverlapped(NativeOverlapped* overlapped)
		{
			return ThreadPool.PostQueuedCompletionStatus(overlapped);
		}

		// Token: 0x06002665 RID: 9829
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMinThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x06002666 RID: 9830
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool SetMaxThreadsNative(int workerThreads, int completionPortThreads);

		// Token: 0x06002667 RID: 9831
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMinThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x06002668 RID: 9832
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetMaxThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x06002669 RID: 9833
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern void GetAvailableThreadsNative(out int workerThreads, out int completionPortThreads);

		// Token: 0x0600266A RID: 9834
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern bool NotifyWorkItemComplete();

		// Token: 0x0600266B RID: 9835
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void ReportThreadStatus(bool isWorking);

		// Token: 0x0600266C RID: 9836 RVA: 0x00141FAF File Offset: 0x001411AF
		internal static void NotifyWorkItemProgress()
		{
			ThreadPool.NotifyWorkItemProgressNative();
		}

		// Token: 0x0600266D RID: 9837
		[MethodImpl(MethodImplOptions.InternalCall)]
		internal static extern void NotifyWorkItemProgressNative();

		// Token: 0x0600266E RID: 9838
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool GetEnableWorkerTracking();

		// Token: 0x0600266F RID: 9839
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern IntPtr RegisterWaitForSingleObjectNative(WaitHandle waitHandle, object state, uint timeOutInterval, bool executeOnlyOnce, RegisteredWaitHandle registeredWaitHandle);

		// Token: 0x06002670 RID: 9840 RVA: 0x00141FB6 File Offset: 0x001411B6
		[Obsolete("ThreadPool.BindHandle(IntPtr) has been deprecated.  Please use ThreadPool.BindHandle(SafeHandle) instead.", false)]
		public static bool BindHandle(IntPtr osHandle)
		{
			return ThreadPool.BindIOCompletionCallbackNative(osHandle);
		}

		// Token: 0x06002671 RID: 9841 RVA: 0x00141FC0 File Offset: 0x001411C0
		[SupportedOSPlatform("windows")]
		public static bool BindHandle(SafeHandle osHandle)
		{
			if (osHandle == null)
			{
				throw new ArgumentNullException("osHandle");
			}
			bool result = false;
			bool flag = false;
			try
			{
				osHandle.DangerousAddRef(ref flag);
				result = ThreadPool.BindIOCompletionCallbackNative(osHandle.DangerousGetHandle());
			}
			finally
			{
				if (flag)
				{
					osHandle.DangerousRelease();
				}
			}
			return result;
		}

		// Token: 0x06002672 RID: 9842
		[MethodImpl(MethodImplOptions.InternalCall)]
		private static extern bool BindIOCompletionCallbackNative(IntPtr fileHandle);

		// Token: 0x06002673 RID: 9843 RVA: 0x00142010 File Offset: 0x00141210
		[UnsupportedOSPlatform("browser")]
		[CLSCompliant(false)]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, [Nullable(2)] object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval > 2147483647U && millisecondsTimeOutInterval != 4294967295U)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", SR.ArgumentOutOfRange_LessEqualToIntegerMaxVal);
			}
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, true);
		}

		// Token: 0x06002674 RID: 9844 RVA: 0x0014203A File Offset: 0x0014123A
		[CLSCompliant(false)]
		[UnsupportedOSPlatform("browser")]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, [Nullable(2)] object state, uint millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval > 2147483647U && millisecondsTimeOutInterval != 4294967295U)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, millisecondsTimeOutInterval, executeOnlyOnce, false);
		}

		// Token: 0x06002675 RID: 9845 RVA: 0x00142064 File Offset: 0x00141264
		[UnsupportedOSPlatform("browser")]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, [Nullable(2)] object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)millisecondsTimeOutInterval, executeOnlyOnce, true);
		}

		// Token: 0x06002676 RID: 9846 RVA: 0x00142086 File Offset: 0x00141286
		[UnsupportedOSPlatform("browser")]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, [Nullable(2)] object state, int millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)millisecondsTimeOutInterval, executeOnlyOnce, false);
		}

		// Token: 0x06002677 RID: 9847 RVA: 0x001420A8 File Offset: 0x001412A8
		[UnsupportedOSPlatform("browser")]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, [Nullable(2)] object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (millisecondsTimeOutInterval > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", SR.ArgumentOutOfRange_LessEqualToIntegerMaxVal);
			}
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)millisecondsTimeOutInterval, executeOnlyOnce, true);
		}

		// Token: 0x06002678 RID: 9848 RVA: 0x001420E5 File Offset: 0x001412E5
		[UnsupportedOSPlatform("browser")]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, [Nullable(2)] object state, long millisecondsTimeOutInterval, bool executeOnlyOnce)
		{
			if (millisecondsTimeOutInterval < -1L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (millisecondsTimeOutInterval > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("millisecondsTimeOutInterval", SR.ArgumentOutOfRange_LessEqualToIntegerMaxVal);
			}
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)millisecondsTimeOutInterval, executeOnlyOnce, false);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x00142124 File Offset: 0x00141324
		[UnsupportedOSPlatform("browser")]
		public static RegisteredWaitHandle RegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, [Nullable(2)] object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.ArgumentOutOfRange_LessEqualToIntegerMaxVal);
			}
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, true);
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x00142178 File Offset: 0x00141378
		[UnsupportedOSPlatform("browser")]
		public static RegisteredWaitHandle UnsafeRegisterWaitForSingleObject(WaitHandle waitObject, WaitOrTimerCallback callBack, [Nullable(2)] object state, TimeSpan timeout, bool executeOnlyOnce)
		{
			long num = (long)timeout.TotalMilliseconds;
			if (num < -1L)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.ArgumentOutOfRange_NeedNonNegOrNegative1);
			}
			if (num > 2147483647L)
			{
				throw new ArgumentOutOfRangeException("timeout", SR.ArgumentOutOfRange_LessEqualToIntegerMaxVal);
			}
			return ThreadPool.RegisterWaitForSingleObject(waitObject, callBack, state, (uint)num, executeOnlyOnce, false);
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x001421C9 File Offset: 0x001413C9
		public static bool QueueUserWorkItem(WaitCallback callBack)
		{
			return ThreadPool.QueueUserWorkItem(callBack, null);
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x001421D4 File Offset: 0x001413D4
		public static bool QueueUserWorkItem(WaitCallback callBack, [Nullable(2)] object state)
		{
			if (callBack == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.callBack);
			}
			ExecutionContext executionContext = ExecutionContext.Capture();
			object callback = (executionContext == null || executionContext.IsDefault) ? new QueueUserWorkItemCallbackDefaultContext(callBack, state) : new QueueUserWorkItemCallback(callBack, state, executionContext);
			ThreadPool.s_workQueue.Enqueue(callback, true);
			return true;
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0014221C File Offset: 0x0014141C
		public static bool QueueUserWorkItem<[Nullable(2)] TState>(Action<TState> callBack, TState state, bool preferLocal)
		{
			if (callBack == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.callBack);
			}
			ExecutionContext executionContext = ExecutionContext.Capture();
			object callback = (executionContext == null || executionContext.IsDefault) ? new QueueUserWorkItemCallbackDefaultContext<TState>(callBack, state) : new QueueUserWorkItemCallback<TState>(callBack, state, executionContext);
			ThreadPool.s_workQueue.Enqueue(callback, !preferLocal);
			return true;
		}

		// Token: 0x0600267E RID: 9854 RVA: 0x00142268 File Offset: 0x00141468
		public static bool UnsafeQueueUserWorkItem<[Nullable(2)] TState>(Action<TState> callBack, TState state, bool preferLocal)
		{
			if (callBack == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.callBack);
			}
			if (callBack == ThreadPool.s_invokeAsyncStateMachineBox)
			{
				if (!(state is IAsyncStateMachineBox))
				{
					ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
				}
				ThreadPool.UnsafeQueueUserWorkItemInternal(state, preferLocal);
				return true;
			}
			ThreadPool.s_workQueue.Enqueue(new QueueUserWorkItemCallbackDefaultContext<TState>(callBack, state), !preferLocal);
			return true;
		}

		// Token: 0x0600267F RID: 9855 RVA: 0x001422C0 File Offset: 0x001414C0
		public static bool UnsafeQueueUserWorkItem(WaitCallback callBack, [Nullable(2)] object state)
		{
			if (callBack == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.callBack);
			}
			object callback = new QueueUserWorkItemCallbackDefaultContext(callBack, state);
			ThreadPool.s_workQueue.Enqueue(callback, true);
			return true;
		}

		// Token: 0x06002680 RID: 9856 RVA: 0x001422EC File Offset: 0x001414EC
		public static bool UnsafeQueueUserWorkItem(IThreadPoolWorkItem callBack, bool preferLocal)
		{
			if (callBack == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.callBack);
			}
			if (callBack is Task)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.callBack);
			}
			ThreadPool.UnsafeQueueUserWorkItemInternal(callBack, preferLocal);
			return true;
		}

		// Token: 0x06002681 RID: 9857 RVA: 0x0014230F File Offset: 0x0014150F
		internal static void UnsafeQueueUserWorkItemInternal(object callBack, bool preferLocal)
		{
			ThreadPool.s_workQueue.Enqueue(callBack, !preferLocal);
		}

		// Token: 0x06002682 RID: 9858 RVA: 0x00142320 File Offset: 0x00141520
		internal static bool TryPopCustomWorkItem(object workItem)
		{
			return ThreadPool.s_workQueue.LocalFindAndPop(workItem);
		}

		// Token: 0x06002683 RID: 9859 RVA: 0x0014232D File Offset: 0x0014152D
		internal static IEnumerable<object> GetQueuedWorkItems()
		{
			foreach (object obj in ThreadPool.s_workQueue.workItems)
			{
				yield return obj;
			}
			IEnumerator<object> enumerator = null;
			foreach (ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue in ThreadPoolWorkQueue.WorkStealingQueueList.Queues)
			{
				if (workStealingQueue != null && workStealingQueue.m_array != null)
				{
					object[] items = workStealingQueue.m_array;
					int num;
					for (int i = 0; i < items.Length; i = num + 1)
					{
						object obj2 = items[i];
						if (obj2 != null)
						{
							yield return obj2;
						}
						num = i;
					}
					items = null;
				}
			}
			ThreadPoolWorkQueue.WorkStealingQueue[] array = null;
			yield break;
			yield break;
		}

		// Token: 0x06002684 RID: 9860 RVA: 0x00142336 File Offset: 0x00141536
		internal static IEnumerable<object> GetLocallyQueuedWorkItems()
		{
			ThreadPoolWorkQueueThreadLocals threadLocals = ThreadPoolWorkQueueThreadLocals.threadLocals;
			ThreadPoolWorkQueue.WorkStealingQueue workStealingQueue = (threadLocals != null) ? threadLocals.workStealingQueue : null;
			if (workStealingQueue != null && workStealingQueue.m_array != null)
			{
				object[] items = workStealingQueue.m_array;
				int num;
				for (int i = 0; i < items.Length; i = num + 1)
				{
					object obj = items[i];
					if (obj != null)
					{
						yield return obj;
					}
					num = i;
				}
				items = null;
			}
			yield break;
		}

		// Token: 0x06002685 RID: 9861 RVA: 0x0014233F File Offset: 0x0014153F
		internal static IEnumerable<object> GetGloballyQueuedWorkItems()
		{
			return ThreadPool.s_workQueue.workItems;
		}

		// Token: 0x06002686 RID: 9862 RVA: 0x0014234C File Offset: 0x0014154C
		private static object[] ToObjectArray(IEnumerable<object> workitems)
		{
			int num = 0;
			foreach (object obj in workitems)
			{
				num++;
			}
			object[] array = new object[num];
			num = 0;
			foreach (object obj2 in workitems)
			{
				if (num < array.Length)
				{
					array[num] = obj2;
				}
				num++;
			}
			return array;
		}

		// Token: 0x06002687 RID: 9863 RVA: 0x001423E4 File Offset: 0x001415E4
		internal static object[] GetQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetQueuedWorkItems());
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x001423F0 File Offset: 0x001415F0
		internal static object[] GetGloballyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetGloballyQueuedWorkItems());
		}

		// Token: 0x06002689 RID: 9865 RVA: 0x001423FC File Offset: 0x001415FC
		internal static object[] GetLocallyQueuedWorkItemsForDebugger()
		{
			return ThreadPool.ToObjectArray(ThreadPool.GetLocallyQueuedWorkItems());
		}

		// Token: 0x17000831 RID: 2097
		// (get) Token: 0x0600268A RID: 9866 RVA: 0x00142408 File Offset: 0x00141608
		public static long PendingWorkItemCount
		{
			get
			{
				ThreadPoolWorkQueue threadPoolWorkQueue = ThreadPool.s_workQueue;
				return threadPoolWorkQueue.LocalCount + threadPoolWorkQueue.GlobalCount + ThreadPool.PendingUnmanagedWorkItemCount;
			}
		}

		// Token: 0x040009FB RID: 2555
		internal static readonly bool EnableWorkerTracking = ThreadPool.GetEnableWorkerTracking();

		// Token: 0x040009FC RID: 2556
		internal static readonly ThreadPoolWorkQueue s_workQueue = new ThreadPoolWorkQueue();

		// Token: 0x040009FD RID: 2557
		internal static readonly Action<object> s_invokeAsyncStateMachineBox = delegate(object state)
		{
			IAsyncStateMachineBox asyncStateMachineBox = state as IAsyncStateMachineBox;
			if (asyncStateMachineBox == null)
			{
				ThrowHelper.ThrowArgumentOutOfRangeException(ExceptionArgument.state);
				return;
			}
			asyncStateMachineBox.MoveNext();
		};
	}
}
