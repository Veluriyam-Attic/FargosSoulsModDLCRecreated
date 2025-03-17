using System;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace System.Runtime.ExceptionServices
{
	// Token: 0x020003F0 RID: 1008
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class ExceptionDispatchInfo
	{
		// Token: 0x0600326F RID: 12911 RVA: 0x0016B31F File Offset: 0x0016A51F
		private ExceptionDispatchInfo(Exception exception)
		{
			this._exception = exception;
			this._dispatchState = exception.CaptureDispatchState();
		}

		// Token: 0x06003270 RID: 12912 RVA: 0x0016B33A File Offset: 0x0016A53A
		public static ExceptionDispatchInfo Capture(Exception source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			return new ExceptionDispatchInfo(source);
		}

		// Token: 0x170009C7 RID: 2503
		// (get) Token: 0x06003271 RID: 12913 RVA: 0x0016B350 File Offset: 0x0016A550
		public Exception SourceException
		{
			get
			{
				return this._exception;
			}
		}

		// Token: 0x06003272 RID: 12914 RVA: 0x0016B358 File Offset: 0x0016A558
		[StackTraceHidden]
		[DoesNotReturn]
		public void Throw()
		{
			this._exception.RestoreDispatchState(this._dispatchState);
			throw this._exception;
		}

		// Token: 0x06003273 RID: 12915 RVA: 0x0016B371 File Offset: 0x0016A571
		[StackTraceHidden]
		[DoesNotReturn]
		public static void Throw(Exception source)
		{
			ExceptionDispatchInfo.Capture(source).Throw();
		}

		// Token: 0x06003274 RID: 12916 RVA: 0x0016B37E File Offset: 0x0016A57E
		[StackTraceHidden]
		public static Exception SetCurrentStackTrace(Exception source)
		{
			if (source == null)
			{
				throw new ArgumentNullException("source");
			}
			source.SetCurrentStackTrace();
			return source;
		}

		// Token: 0x04000E17 RID: 3607
		private readonly Exception _exception;

		// Token: 0x04000E18 RID: 3608
		private readonly Exception.DispatchState _dispatchState;
	}
}
