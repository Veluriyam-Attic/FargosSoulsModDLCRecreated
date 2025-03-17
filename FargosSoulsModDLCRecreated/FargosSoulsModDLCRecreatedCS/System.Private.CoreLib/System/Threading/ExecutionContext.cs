using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Runtime.Serialization;

namespace System.Threading
{
	// Token: 0x0200029B RID: 667
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ExecutionContext : IDisposable, ISerializable
	{
		// Token: 0x06002779 RID: 10105 RVA: 0x00145054 File Offset: 0x00144254
		private ExecutionContext(bool isDefault)
		{
			this.m_isDefault = isDefault;
		}

		// Token: 0x0600277A RID: 10106 RVA: 0x00145063 File Offset: 0x00144263
		private ExecutionContext(IAsyncLocalValueMap localValues, IAsyncLocal[] localChangeNotifications, bool isFlowSuppressed)
		{
			this.m_localValues = localValues;
			this.m_localChangeNotifications = localChangeNotifications;
			this.m_isFlowSuppressed = isFlowSuppressed;
		}

		// Token: 0x0600277B RID: 10107 RVA: 0x000B3617 File Offset: 0x000B2817
		public void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			throw new PlatformNotSupportedException();
		}

		// Token: 0x0600277C RID: 10108 RVA: 0x00145080 File Offset: 0x00144280
		[NullableContext(2)]
		public static ExecutionContext Capture()
		{
			ExecutionContext executionContext = Thread.CurrentThread._executionContext;
			if (executionContext == null)
			{
				executionContext = ExecutionContext.Default;
			}
			else if (executionContext.m_isFlowSuppressed)
			{
				executionContext = null;
			}
			return executionContext;
		}

		// Token: 0x0600277D RID: 10109 RVA: 0x001450AE File Offset: 0x001442AE
		internal static ExecutionContext CaptureForRestore()
		{
			return Thread.CurrentThread._executionContext;
		}

		// Token: 0x0600277E RID: 10110 RVA: 0x001450BA File Offset: 0x001442BA
		private ExecutionContext ShallowClone(bool isFlowSuppressed)
		{
			if (this.m_localValues != null && !AsyncLocalValueMap.IsEmpty(this.m_localValues))
			{
				return new ExecutionContext(this.m_localValues, this.m_localChangeNotifications, isFlowSuppressed);
			}
			if (!isFlowSuppressed)
			{
				return null;
			}
			return ExecutionContext.DefaultFlowSuppressed;
		}

		// Token: 0x0600277F RID: 10111 RVA: 0x001450F0 File Offset: 0x001442F0
		public static AsyncFlowControl SuppressFlow()
		{
			Thread currentThread = Thread.CurrentThread;
			ExecutionContext executionContext = currentThread._executionContext ?? ExecutionContext.Default;
			if (executionContext.m_isFlowSuppressed)
			{
				throw new InvalidOperationException(SR.InvalidOperation_CannotSupressFlowMultipleTimes);
			}
			executionContext = executionContext.ShallowClone(true);
			AsyncFlowControl result = default(AsyncFlowControl);
			currentThread._executionContext = executionContext;
			result.Initialize(currentThread);
			return result;
		}

		// Token: 0x06002780 RID: 10112 RVA: 0x00145148 File Offset: 0x00144348
		public static void RestoreFlow()
		{
			Thread currentThread = Thread.CurrentThread;
			ExecutionContext executionContext = currentThread._executionContext;
			if (executionContext == null || !executionContext.m_isFlowSuppressed)
			{
				throw new InvalidOperationException(SR.InvalidOperation_CannotRestoreUnsupressedFlow);
			}
			currentThread._executionContext = executionContext.ShallowClone(false);
		}

		// Token: 0x06002781 RID: 10113 RVA: 0x00145188 File Offset: 0x00144388
		public static bool IsFlowSuppressed()
		{
			ExecutionContext executionContext = Thread.CurrentThread._executionContext;
			return executionContext != null && executionContext.m_isFlowSuppressed;
		}

		// Token: 0x1700084D RID: 2125
		// (get) Token: 0x06002782 RID: 10114 RVA: 0x001451AB File Offset: 0x001443AB
		internal bool HasChangeNotifications
		{
			get
			{
				return this.m_localChangeNotifications != null;
			}
		}

		// Token: 0x1700084E RID: 2126
		// (get) Token: 0x06002783 RID: 10115 RVA: 0x001451B6 File Offset: 0x001443B6
		internal bool IsDefault
		{
			get
			{
				return this.m_isDefault;
			}
		}

		// Token: 0x06002784 RID: 10116 RVA: 0x001451BE File Offset: 0x001443BE
		public static void Run(ExecutionContext executionContext, ContextCallback callback, [Nullable(2)] object state)
		{
			if (executionContext == null)
			{
				ExecutionContext.ThrowNullContext();
			}
			ExecutionContext.RunInternal(executionContext, callback, state);
		}

		// Token: 0x06002785 RID: 10117 RVA: 0x001451D0 File Offset: 0x001443D0
		internal static void RunInternal(ExecutionContext executionContext, ContextCallback callback, object state)
		{
			Thread currentThread = Thread.CurrentThread;
			Thread thread = currentThread;
			ExecutionContext executionContext2 = currentThread._executionContext;
			if (executionContext2 != null && executionContext2.m_isDefault)
			{
				executionContext2 = null;
			}
			ExecutionContext executionContext3 = executionContext2;
			SynchronizationContext synchronizationContext = currentThread._synchronizationContext;
			if (executionContext != null && executionContext.m_isDefault)
			{
				executionContext = null;
			}
			if (executionContext2 != executionContext)
			{
				ExecutionContext.RestoreChangedContextToThread(currentThread, executionContext, executionContext2);
			}
			ExceptionDispatchInfo exceptionDispatchInfo = null;
			try
			{
				callback(state);
			}
			catch (Exception source)
			{
				exceptionDispatchInfo = ExceptionDispatchInfo.Capture(source);
			}
			SynchronizationContext synchronizationContext2 = synchronizationContext;
			Thread thread2 = thread;
			if (thread2._synchronizationContext != synchronizationContext2)
			{
				thread2._synchronizationContext = synchronizationContext2;
			}
			ExecutionContext executionContext4 = executionContext3;
			ExecutionContext executionContext5 = thread2._executionContext;
			if (executionContext5 != executionContext4)
			{
				ExecutionContext.RestoreChangedContextToThread(thread2, executionContext4, executionContext5);
			}
			if (exceptionDispatchInfo != null)
			{
				exceptionDispatchInfo.Throw();
			}
		}

		// Token: 0x06002786 RID: 10118 RVA: 0x0014528C File Offset: 0x0014448C
		public static void Restore(ExecutionContext executionContext)
		{
			if (executionContext == null)
			{
				ExecutionContext.ThrowNullContext();
			}
			ExecutionContext.RestoreInternal(executionContext);
		}

		// Token: 0x06002787 RID: 10119 RVA: 0x0014529C File Offset: 0x0014449C
		internal static void RestoreInternal(ExecutionContext executionContext)
		{
			Thread currentThread = Thread.CurrentThread;
			ExecutionContext executionContext2 = currentThread._executionContext;
			if (executionContext2 != null && executionContext2.m_isDefault)
			{
				executionContext2 = null;
			}
			if (executionContext != null && executionContext.m_isDefault)
			{
				executionContext = null;
			}
			if (executionContext2 != executionContext)
			{
				ExecutionContext.RestoreChangedContextToThread(currentThread, executionContext, executionContext2);
			}
		}

		// Token: 0x06002788 RID: 10120 RVA: 0x001452E0 File Offset: 0x001444E0
		internal static void RunFromThreadPoolDispatchLoop(Thread threadPoolThread, ExecutionContext executionContext, ContextCallback callback, object state)
		{
			if (executionContext != null && !executionContext.m_isDefault)
			{
				ExecutionContext.RestoreChangedContextToThread(threadPoolThread, executionContext, null);
			}
			ExceptionDispatchInfo exceptionDispatchInfo = null;
			try
			{
				callback(state);
			}
			catch (Exception source)
			{
				exceptionDispatchInfo = ExceptionDispatchInfo.Capture(source);
			}
			ExecutionContext executionContext2 = threadPoolThread._executionContext;
			threadPoolThread._synchronizationContext = null;
			if (executionContext2 != null)
			{
				ExecutionContext.RestoreChangedContextToThread(threadPoolThread, null, executionContext2);
			}
			if (exceptionDispatchInfo != null)
			{
				exceptionDispatchInfo.Throw();
			}
		}

		// Token: 0x06002789 RID: 10121 RVA: 0x0014534C File Offset: 0x0014454C
		internal static void RunForThreadPoolUnsafe<TState>(ExecutionContext executionContext, Action<TState> callback, in TState state)
		{
			Thread.CurrentThread._executionContext = executionContext;
			if (executionContext.HasChangeNotifications)
			{
				ExecutionContext.OnValuesChanged(null, executionContext);
			}
			callback(state);
		}

		// Token: 0x0600278A RID: 10122 RVA: 0x00145374 File Offset: 0x00144574
		internal static void RestoreChangedContextToThread(Thread currentThread, ExecutionContext contextToRestore, ExecutionContext currentContext)
		{
			currentThread._executionContext = contextToRestore;
			if ((currentContext != null && currentContext.HasChangeNotifications) || (contextToRestore != null && contextToRestore.HasChangeNotifications))
			{
				ExecutionContext.OnValuesChanged(currentContext, contextToRestore);
			}
		}

		// Token: 0x0600278B RID: 10123 RVA: 0x0014539C File Offset: 0x0014459C
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal static void ResetThreadPoolThread(Thread currentThread)
		{
			ExecutionContext executionContext = currentThread._executionContext;
			currentThread._synchronizationContext = null;
			currentThread._executionContext = null;
			if (executionContext != null && executionContext.HasChangeNotifications)
			{
				ExecutionContext.OnValuesChanged(executionContext, null);
				currentThread._synchronizationContext = null;
				currentThread._executionContext = null;
			}
		}

		// Token: 0x0600278C RID: 10124 RVA: 0x001453E0 File Offset: 0x001445E0
		internal static void OnValuesChanged(ExecutionContext previousExecutionCtx, ExecutionContext nextExecutionCtx)
		{
			IAsyncLocal[] array = (previousExecutionCtx != null) ? previousExecutionCtx.m_localChangeNotifications : null;
			IAsyncLocal[] array2 = (nextExecutionCtx != null) ? nextExecutionCtx.m_localChangeNotifications : null;
			try
			{
				if (array != null && array2 != null)
				{
					foreach (IAsyncLocal asyncLocal in array)
					{
						object obj;
						previousExecutionCtx.m_localValues.TryGetValue(asyncLocal, out obj);
						object obj2;
						nextExecutionCtx.m_localValues.TryGetValue(asyncLocal, out obj2);
						if (obj != obj2)
						{
							asyncLocal.OnValueChanged(obj, obj2, true);
						}
					}
					if (array2 != array)
					{
						foreach (IAsyncLocal asyncLocal2 in array2)
						{
							object obj3;
							if (!previousExecutionCtx.m_localValues.TryGetValue(asyncLocal2, out obj3))
							{
								object obj4;
								nextExecutionCtx.m_localValues.TryGetValue(asyncLocal2, out obj4);
								if (obj3 != obj4)
								{
									asyncLocal2.OnValueChanged(obj3, obj4, true);
								}
							}
						}
					}
				}
				else if (array != null)
				{
					foreach (IAsyncLocal asyncLocal3 in array)
					{
						object obj5;
						previousExecutionCtx.m_localValues.TryGetValue(asyncLocal3, out obj5);
						if (obj5 != null)
						{
							asyncLocal3.OnValueChanged(obj5, null, true);
						}
					}
				}
				else
				{
					foreach (IAsyncLocal asyncLocal4 in array2)
					{
						object obj6;
						nextExecutionCtx.m_localValues.TryGetValue(asyncLocal4, out obj6);
						if (obj6 != null)
						{
							asyncLocal4.OnValueChanged(null, obj6, true);
						}
					}
				}
			}
			catch (Exception exception)
			{
				Environment.FailFast(SR.ExecutionContext_ExceptionInAsyncLocalNotification, exception);
			}
		}

		// Token: 0x0600278D RID: 10125 RVA: 0x00145560 File Offset: 0x00144760
		[DoesNotReturn]
		[StackTraceHidden]
		private static void ThrowNullContext()
		{
			throw new InvalidOperationException(SR.InvalidOperation_NullContext);
		}

		// Token: 0x0600278E RID: 10126 RVA: 0x0014556C File Offset: 0x0014476C
		internal static object GetLocalValue(IAsyncLocal local)
		{
			ExecutionContext executionContext = Thread.CurrentThread._executionContext;
			if (executionContext == null)
			{
				return null;
			}
			object result;
			executionContext.m_localValues.TryGetValue(local, out result);
			return result;
		}

		// Token: 0x0600278F RID: 10127 RVA: 0x0014559C File Offset: 0x0014479C
		internal static void SetLocalValue(IAsyncLocal local, object newValue, bool needChangeNotifications)
		{
			ExecutionContext executionContext = Thread.CurrentThread._executionContext;
			object obj = null;
			bool flag = false;
			if (executionContext != null)
			{
				flag = executionContext.m_localValues.TryGetValue(local, out obj);
			}
			if (obj == newValue)
			{
				return;
			}
			IAsyncLocal[] array = null;
			bool flag2 = false;
			IAsyncLocalValueMap asyncLocalValueMap;
			if (executionContext != null)
			{
				flag2 = executionContext.m_isFlowSuppressed;
				asyncLocalValueMap = executionContext.m_localValues.Set(local, newValue, !needChangeNotifications);
				array = executionContext.m_localChangeNotifications;
			}
			else
			{
				asyncLocalValueMap = AsyncLocalValueMap.Create(local, newValue, !needChangeNotifications);
			}
			if (needChangeNotifications && !flag)
			{
				if (array == null)
				{
					array = new IAsyncLocal[]
					{
						local
					};
				}
				else
				{
					int num = array.Length;
					Array.Resize<IAsyncLocal>(ref array, num + 1);
					array[num] = local;
				}
			}
			Thread.CurrentThread._executionContext = ((!flag2 && AsyncLocalValueMap.IsEmpty(asyncLocalValueMap)) ? null : new ExecutionContext(asyncLocalValueMap, array, flag2));
			if (needChangeNotifications)
			{
				local.OnValueChanged(obj, newValue, false);
			}
		}

		// Token: 0x06002790 RID: 10128 RVA: 0x000AC098 File Offset: 0x000AB298
		public ExecutionContext CreateCopy()
		{
			return this;
		}

		// Token: 0x06002791 RID: 10129 RVA: 0x000AB30B File Offset: 0x000AA50B
		public void Dispose()
		{
		}

		// Token: 0x04000A67 RID: 2663
		internal static readonly ExecutionContext Default = new ExecutionContext(true);

		// Token: 0x04000A68 RID: 2664
		internal static readonly ExecutionContext DefaultFlowSuppressed = new ExecutionContext(AsyncLocalValueMap.Empty, Array.Empty<IAsyncLocal>(), true);

		// Token: 0x04000A69 RID: 2665
		private readonly IAsyncLocalValueMap m_localValues;

		// Token: 0x04000A6A RID: 2666
		private readonly IAsyncLocal[] m_localChangeNotifications;

		// Token: 0x04000A6B RID: 2667
		private readonly bool m_isFlowSuppressed;

		// Token: 0x04000A6C RID: 2668
		private readonly bool m_isDefault;
	}
}
