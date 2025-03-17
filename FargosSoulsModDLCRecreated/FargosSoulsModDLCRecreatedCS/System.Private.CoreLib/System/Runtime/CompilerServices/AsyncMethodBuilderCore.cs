using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000500 RID: 1280
	internal static class AsyncMethodBuilderCore
	{
		// Token: 0x06004652 RID: 18002 RVA: 0x0017ADE4 File Offset: 0x00179FE4
		[DebuggerStepThrough]
		public static void Start<TStateMachine>(ref TStateMachine stateMachine) where TStateMachine : IAsyncStateMachine
		{
			if (stateMachine == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.stateMachine);
			}
			Thread currentThread = Thread.CurrentThread;
			Thread thread = currentThread;
			ExecutionContext executionContext = currentThread._executionContext;
			ExecutionContext executionContext2 = executionContext;
			SynchronizationContext synchronizationContext = currentThread._synchronizationContext;
			try
			{
				stateMachine.MoveNext();
			}
			finally
			{
				SynchronizationContext synchronizationContext2 = synchronizationContext;
				Thread thread2 = thread;
				if (synchronizationContext2 != thread2._synchronizationContext)
				{
					thread2._synchronizationContext = synchronizationContext2;
				}
				ExecutionContext executionContext3 = executionContext2;
				ExecutionContext executionContext4 = thread2._executionContext;
				if (executionContext3 != executionContext4)
				{
					ExecutionContext.RestoreChangedContextToThread(thread2, executionContext3, executionContext4);
				}
			}
		}

		// Token: 0x06004653 RID: 18003 RVA: 0x0017AE78 File Offset: 0x0017A078
		public static void SetStateMachine(IAsyncStateMachine stateMachine, Task task)
		{
			if (stateMachine == null)
			{
				ThrowHelper.ThrowArgumentNullException(ExceptionArgument.stateMachine);
			}
			if (task != null)
			{
				ThrowHelper.ThrowInvalidOperationException(ExceptionResource.AsyncMethodBuilder_InstanceNotInitialized);
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06004654 RID: 18004 RVA: 0x0017AE8E File Offset: 0x0017A08E
		internal static bool TrackAsyncMethodCompletion
		{
			[MethodImpl(MethodImplOptions.AggressiveInlining)]
			get
			{
				return TplEventSource.Log.IsEnabled(EventLevel.Warning, (EventKeywords)256L);
			}
		}

		// Token: 0x06004655 RID: 18005 RVA: 0x0017AEA4 File Offset: 0x0017A0A4
		[UnconditionalSuppressMessage("ReflectionAnalysis", "IL2075:UnrecognizedReflectionPattern", Justification = "It's okay if unused fields disappear from debug views")]
		internal static string GetAsyncStateMachineDescription(IAsyncStateMachine stateMachine)
		{
			Type type = stateMachine.GetType();
			FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(type.FullName);
			foreach (FieldInfo fieldInfo in fields)
			{
				stringBuilder.Append("    ").Append(fieldInfo.Name).Append(": ").Append(fieldInfo.GetValue(stateMachine)).AppendLine();
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06004656 RID: 18006 RVA: 0x0017AF28 File Offset: 0x0017A128
		internal static Action CreateContinuationWrapper(Action continuation, Action<Action, Task> invokeAction, Task innerTask)
		{
			return new Action(new AsyncMethodBuilderCore.ContinuationWrapper(continuation, invokeAction, innerTask).Invoke);
		}

		// Token: 0x06004657 RID: 18007 RVA: 0x0017AF40 File Offset: 0x0017A140
		internal static Action TryGetStateMachineForDebugger(Action action)
		{
			object target = action.Target;
			IAsyncStateMachineBox asyncStateMachineBox = target as IAsyncStateMachineBox;
			if (asyncStateMachineBox != null)
			{
				return new Action(asyncStateMachineBox.GetStateMachineObject().MoveNext);
			}
			AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = target as AsyncMethodBuilderCore.ContinuationWrapper;
			if (continuationWrapper == null)
			{
				return action;
			}
			return AsyncMethodBuilderCore.TryGetStateMachineForDebugger(continuationWrapper._continuation);
		}

		// Token: 0x06004658 RID: 18008 RVA: 0x0017AF88 File Offset: 0x0017A188
		internal static Task TryGetContinuationTask(Action continuation)
		{
			AsyncMethodBuilderCore.ContinuationWrapper continuationWrapper = continuation.Target as AsyncMethodBuilderCore.ContinuationWrapper;
			if (continuationWrapper == null)
			{
				return continuation.Target as Task;
			}
			return continuationWrapper._innerTask;
		}

		// Token: 0x02000501 RID: 1281
		private sealed class ContinuationWrapper
		{
			// Token: 0x06004659 RID: 18009 RVA: 0x0017AFB6 File Offset: 0x0017A1B6
			internal ContinuationWrapper(Action continuation, Action<Action, Task> invokeAction, Task innerTask)
			{
				this._invokeAction = invokeAction;
				this._continuation = continuation;
				this._innerTask = innerTask;
			}

			// Token: 0x0600465A RID: 18010 RVA: 0x0017AFD3 File Offset: 0x0017A1D3
			internal void Invoke()
			{
				this._invokeAction(this._continuation, this._innerTask);
			}

			// Token: 0x040010D1 RID: 4305
			private readonly Action<Action, Task> _invokeAction;

			// Token: 0x040010D2 RID: 4306
			internal readonly Action _continuation;

			// Token: 0x040010D3 RID: 4307
			internal readonly Task _innerTask;
		}
	}
}
