using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x02000289 RID: 649
	[Nullable(0)]
	[NullableContext(1)]
	[DebuggerDisplay("IsCancellationRequested = {IsCancellationRequested}")]
	public readonly struct CancellationToken
	{
		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x0600270A RID: 9994 RVA: 0x00143DE0 File Offset: 0x00142FE0
		public static CancellationToken None
		{
			get
			{
				return default(CancellationToken);
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x0600270B RID: 9995 RVA: 0x00143DF6 File Offset: 0x00142FF6
		public bool IsCancellationRequested
		{
			get
			{
				return this._source != null && this._source.IsCancellationRequested;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x0600270C RID: 9996 RVA: 0x00143E0D File Offset: 0x0014300D
		public bool CanBeCanceled
		{
			get
			{
				return this._source != null;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x0600270D RID: 9997 RVA: 0x00143E18 File Offset: 0x00143018
		public WaitHandle WaitHandle
		{
			get
			{
				return (this._source ?? CancellationTokenSource.s_neverCanceledSource).WaitHandle;
			}
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x00143E2E File Offset: 0x0014302E
		internal CancellationToken(CancellationTokenSource source)
		{
			this._source = source;
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x00143E37 File Offset: 0x00143037
		public CancellationToken(bool canceled)
		{
			this = new CancellationToken(canceled ? CancellationTokenSource.s_canceledSource : null);
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x00143E4A File Offset: 0x0014304A
		public CancellationTokenRegistration Register(Action callback)
		{
			Action<object> callback2 = CancellationToken.s_actionToActionObjShunt;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(callback2, callback, false, true);
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x00143E69 File Offset: 0x00143069
		public CancellationTokenRegistration Register(Action callback, bool useSynchronizationContext)
		{
			Action<object> callback2 = CancellationToken.s_actionToActionObjShunt;
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			return this.Register(callback2, callback, useSynchronizationContext, true);
		}

		// Token: 0x06002712 RID: 10002 RVA: 0x00143E88 File Offset: 0x00143088
		[NullableContext(2)]
		public CancellationTokenRegistration Register([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> callback, object state)
		{
			return this.Register(callback, state, false, true);
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x00143E94 File Offset: 0x00143094
		[NullableContext(2)]
		public CancellationTokenRegistration Register([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> callback, object state, bool useSynchronizationContext)
		{
			return this.Register(callback, state, useSynchronizationContext, true);
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x00143EA0 File Offset: 0x001430A0
		[NullableContext(2)]
		public CancellationTokenRegistration UnsafeRegister([Nullable(new byte[]
		{
			1,
			2
		})] Action<object> callback, object state)
		{
			return this.Register(callback, state, false, false);
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x00143EAC File Offset: 0x001430AC
		private CancellationTokenRegistration Register(Action<object> callback, object state, bool useSynchronizationContext, bool useExecutionContext)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			CancellationTokenSource source = this._source;
			if (source == null)
			{
				return default(CancellationTokenRegistration);
			}
			return source.InternalRegister(callback, state, useSynchronizationContext ? SynchronizationContext.Current : null, useExecutionContext ? ExecutionContext.Capture() : null);
		}

		// Token: 0x06002716 RID: 10006 RVA: 0x00143EFA File Offset: 0x001430FA
		public bool Equals(CancellationToken other)
		{
			return this._source == other._source;
		}

		// Token: 0x06002717 RID: 10007 RVA: 0x00143F0A File Offset: 0x0014310A
		[NullableContext(2)]
		public override bool Equals(object other)
		{
			return other is CancellationToken && this.Equals((CancellationToken)other);
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x00143F22 File Offset: 0x00143122
		public override int GetHashCode()
		{
			return (this._source ?? CancellationTokenSource.s_neverCanceledSource).GetHashCode();
		}

		// Token: 0x06002719 RID: 10009 RVA: 0x00143F38 File Offset: 0x00143138
		public static bool operator ==(CancellationToken left, CancellationToken right)
		{
			return left.Equals(right);
		}

		// Token: 0x0600271A RID: 10010 RVA: 0x00143F42 File Offset: 0x00143142
		public static bool operator !=(CancellationToken left, CancellationToken right)
		{
			return !left.Equals(right);
		}

		// Token: 0x0600271B RID: 10011 RVA: 0x00143F4F File Offset: 0x0014314F
		public void ThrowIfCancellationRequested()
		{
			if (this.IsCancellationRequested)
			{
				this.ThrowOperationCanceledException();
			}
		}

		// Token: 0x0600271C RID: 10012 RVA: 0x00143F5F File Offset: 0x0014315F
		[DoesNotReturn]
		private void ThrowOperationCanceledException()
		{
			throw new OperationCanceledException(SR.OperationCanceled, this);
		}

		// Token: 0x04000A3A RID: 2618
		private readonly CancellationTokenSource _source;

		// Token: 0x04000A3B RID: 2619
		private static readonly Action<object> s_actionToActionObjShunt = delegate(object obj)
		{
			((Action)obj)();
		};
	}
}
