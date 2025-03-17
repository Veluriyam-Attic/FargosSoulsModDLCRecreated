using System;
using System.Globalization;

namespace System.Threading
{
	// Token: 0x0200026B RID: 619
	internal sealed class ThreadHelper
	{
		// Token: 0x060025B5 RID: 9653 RVA: 0x00141304 File Offset: 0x00140504
		internal ThreadHelper(Delegate start)
		{
			this._start = start;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x00141313 File Offset: 0x00140513
		internal void SetExecutionContextHelper(ExecutionContext ec)
		{
			this._executionContext = ec;
		}

		// Token: 0x060025B7 RID: 9655 RVA: 0x0014131C File Offset: 0x0014051C
		private static void ThreadStart_Context(object state)
		{
			ThreadHelper threadHelper = (ThreadHelper)state;
			threadHelper.InitializeCulture();
			ThreadStart threadStart = threadHelper._start as ThreadStart;
			if (threadStart != null)
			{
				threadStart();
				return;
			}
			((ParameterizedThreadStart)threadHelper._start)(threadHelper._startArg);
		}

		// Token: 0x060025B8 RID: 9656 RVA: 0x00141362 File Offset: 0x00140562
		private void InitializeCulture()
		{
			if (this._startCulture != null)
			{
				CultureInfo.CurrentCulture = this._startCulture;
				this._startCulture = null;
			}
			if (this._startUICulture != null)
			{
				CultureInfo.CurrentUICulture = this._startUICulture;
				this._startUICulture = null;
			}
		}

		// Token: 0x060025B9 RID: 9657 RVA: 0x00141398 File Offset: 0x00140598
		internal void ThreadStart(object obj)
		{
			this._startArg = obj;
			ExecutionContext executionContext = this._executionContext;
			if (executionContext != null)
			{
				ExecutionContext.RunInternal(executionContext, ThreadHelper.s_threadStartContextCallback, this);
				return;
			}
			this.InitializeCulture();
			((ParameterizedThreadStart)this._start)(obj);
		}

		// Token: 0x060025BA RID: 9658 RVA: 0x001413DC File Offset: 0x001405DC
		internal void ThreadStart()
		{
			ExecutionContext executionContext = this._executionContext;
			if (executionContext != null)
			{
				ExecutionContext.RunInternal(executionContext, ThreadHelper.s_threadStartContextCallback, this);
				return;
			}
			this.InitializeCulture();
			((ThreadStart)this._start)();
		}

		// Token: 0x040009DF RID: 2527
		private Delegate _start;

		// Token: 0x040009E0 RID: 2528
		internal CultureInfo _startCulture;

		// Token: 0x040009E1 RID: 2529
		internal CultureInfo _startUICulture;

		// Token: 0x040009E2 RID: 2530
		private object _startArg;

		// Token: 0x040009E3 RID: 2531
		private ExecutionContext _executionContext;

		// Token: 0x040009E4 RID: 2532
		internal static readonly ContextCallback s_threadStartContextCallback = new ContextCallback(ThreadHelper.ThreadStart_Context);
	}
}
