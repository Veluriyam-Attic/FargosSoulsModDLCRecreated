using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.ExceptionServices;

namespace System.Threading.Tasks
{
	// Token: 0x02000333 RID: 819
	internal class TaskExceptionHolder
	{
		// Token: 0x06002B7D RID: 11133 RVA: 0x001523E4 File Offset: 0x001515E4
		internal TaskExceptionHolder(Task task)
		{
			this.m_task = task;
		}

		// Token: 0x06002B7E RID: 11134 RVA: 0x001523F4 File Offset: 0x001515F4
		protected override void Finalize()
		{
			try
			{
				if (this.m_faultExceptions != null && !this.m_isHandled)
				{
					AggregateException exception = new AggregateException(SR.TaskExceptionHolder_UnhandledException, this.m_faultExceptions);
					UnobservedTaskExceptionEventArgs ueea = new UnobservedTaskExceptionEventArgs(exception);
					TaskScheduler.PublishUnobservedTaskException(this.m_task, ueea);
				}
			}
			finally
			{
				base.Finalize();
			}
		}

		// Token: 0x170008DD RID: 2269
		// (get) Token: 0x06002B7F RID: 11135 RVA: 0x00152454 File Offset: 0x00151654
		internal bool ContainsFaultList
		{
			get
			{
				return this.m_faultExceptions != null;
			}
		}

		// Token: 0x06002B80 RID: 11136 RVA: 0x00152461 File Offset: 0x00151661
		internal void Add(object exceptionObject, bool representsCancellation)
		{
			if (representsCancellation)
			{
				this.SetCancellationException(exceptionObject);
				return;
			}
			this.AddFaultException(exceptionObject);
		}

		// Token: 0x06002B81 RID: 11137 RVA: 0x00152478 File Offset: 0x00151678
		private void SetCancellationException(object exceptionObject)
		{
			OperationCanceledException ex = exceptionObject as OperationCanceledException;
			if (ex != null)
			{
				this.m_cancellationException = ExceptionDispatchInfo.Capture(ex);
			}
			else
			{
				ExceptionDispatchInfo cancellationException = exceptionObject as ExceptionDispatchInfo;
				this.m_cancellationException = cancellationException;
			}
			this.MarkAsHandled(false);
		}

		// Token: 0x06002B82 RID: 11138 RVA: 0x001524B4 File Offset: 0x001516B4
		private void AddFaultException(object exceptionObject)
		{
			List<ExceptionDispatchInfo> list = this.m_faultExceptions;
			if (list == null)
			{
				list = (this.m_faultExceptions = new List<ExceptionDispatchInfo>(1));
			}
			Exception ex = exceptionObject as Exception;
			if (ex != null)
			{
				list.Add(ExceptionDispatchInfo.Capture(ex));
			}
			else
			{
				ExceptionDispatchInfo exceptionDispatchInfo = exceptionObject as ExceptionDispatchInfo;
				if (exceptionDispatchInfo != null)
				{
					list.Add(exceptionDispatchInfo);
				}
				else
				{
					IEnumerable<Exception> enumerable = exceptionObject as IEnumerable<Exception>;
					if (enumerable != null)
					{
						using (IEnumerator<Exception> enumerator = enumerable.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								Exception source = enumerator.Current;
								list.Add(ExceptionDispatchInfo.Capture(source));
							}
							goto IL_AE;
						}
					}
					IEnumerable<ExceptionDispatchInfo> enumerable2 = exceptionObject as IEnumerable<ExceptionDispatchInfo>;
					if (enumerable2 == null)
					{
						throw new ArgumentException(SR.TaskExceptionHolder_UnknownExceptionType, "exceptionObject");
					}
					list.AddRange(enumerable2);
				}
			}
			IL_AE:
			if (list.Count > 0)
			{
				this.MarkAsUnhandled();
			}
		}

		// Token: 0x06002B83 RID: 11139 RVA: 0x00152590 File Offset: 0x00151790
		private void MarkAsUnhandled()
		{
			if (this.m_isHandled)
			{
				GC.ReRegisterForFinalize(this);
				this.m_isHandled = false;
			}
		}

		// Token: 0x06002B84 RID: 11140 RVA: 0x001525AB File Offset: 0x001517AB
		internal void MarkAsHandled(bool calledFromFinalizer)
		{
			if (!this.m_isHandled)
			{
				if (!calledFromFinalizer)
				{
					GC.SuppressFinalize(this);
				}
				this.m_isHandled = true;
			}
		}

		// Token: 0x06002B85 RID: 11141 RVA: 0x001525CC File Offset: 0x001517CC
		internal AggregateException CreateExceptionObject(bool calledFromFinalizer, Exception includeThisException)
		{
			List<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(calledFromFinalizer);
			if (includeThisException == null)
			{
				return new AggregateException(faultExceptions);
			}
			Exception[] array = new Exception[faultExceptions.Count + 1];
			for (int i = 0; i < array.Length - 1; i++)
			{
				array[i] = faultExceptions[i].SourceException;
			}
			Exception[] array2 = array;
			array2[array2.Length - 1] = includeThisException;
			return new AggregateException(array);
		}

		// Token: 0x06002B86 RID: 11142 RVA: 0x00152630 File Offset: 0x00151830
		internal ReadOnlyCollection<ExceptionDispatchInfo> GetExceptionDispatchInfos()
		{
			List<ExceptionDispatchInfo> faultExceptions = this.m_faultExceptions;
			this.MarkAsHandled(false);
			return new ReadOnlyCollection<ExceptionDispatchInfo>(faultExceptions);
		}

		// Token: 0x06002B87 RID: 11143 RVA: 0x00152654 File Offset: 0x00151854
		internal ExceptionDispatchInfo GetCancellationExceptionDispatchInfo()
		{
			return this.m_cancellationException;
		}

		// Token: 0x04000C12 RID: 3090
		private readonly Task m_task;

		// Token: 0x04000C13 RID: 3091
		private volatile List<ExceptionDispatchInfo> m_faultExceptions;

		// Token: 0x04000C14 RID: 3092
		private ExceptionDispatchInfo m_cancellationException;

		// Token: 0x04000C15 RID: 3093
		private volatile bool m_isHandled;
	}
}
