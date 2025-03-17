using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000521 RID: 1313
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredCancelableAsyncEnumerable<[Nullable(2)] T>
	{
		// Token: 0x060046FD RID: 18173 RVA: 0x0017CD9A File Offset: 0x0017BF9A
		internal ConfiguredCancelableAsyncEnumerable(IAsyncEnumerable<T> enumerable, bool continueOnCapturedContext, CancellationToken cancellationToken)
		{
			this._enumerable = enumerable;
			this._continueOnCapturedContext = continueOnCapturedContext;
			this._cancellationToken = cancellationToken;
		}

		// Token: 0x060046FE RID: 18174 RVA: 0x0017CDB1 File Offset: 0x0017BFB1
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ConfiguredCancelableAsyncEnumerable<T> ConfigureAwait(bool continueOnCapturedContext)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(this._enumerable, continueOnCapturedContext, this._cancellationToken);
		}

		// Token: 0x060046FF RID: 18175 RVA: 0x0017CDC5 File Offset: 0x0017BFC5
		[return: Nullable(new byte[]
		{
			0,
			1
		})]
		public ConfiguredCancelableAsyncEnumerable<T> WithCancellation(CancellationToken cancellationToken)
		{
			return new ConfiguredCancelableAsyncEnumerable<T>(this._enumerable, this._continueOnCapturedContext, cancellationToken);
		}

		// Token: 0x06004700 RID: 18176 RVA: 0x0017CDD9 File Offset: 0x0017BFD9
		public ConfiguredCancelableAsyncEnumerable<T>.Enumerator GetAsyncEnumerator()
		{
			return new ConfiguredCancelableAsyncEnumerable<T>.Enumerator(this._enumerable.GetAsyncEnumerator(this._cancellationToken), this._continueOnCapturedContext);
		}

		// Token: 0x04001109 RID: 4361
		private readonly IAsyncEnumerable<T> _enumerable;

		// Token: 0x0400110A RID: 4362
		private readonly CancellationToken _cancellationToken;

		// Token: 0x0400110B RID: 4363
		private readonly bool _continueOnCapturedContext;

		// Token: 0x02000522 RID: 1314
		[StructLayout(LayoutKind.Auto)]
		public readonly struct Enumerator
		{
			// Token: 0x06004701 RID: 18177 RVA: 0x0017CDF7 File Offset: 0x0017BFF7
			internal Enumerator(IAsyncEnumerator<T> enumerator, bool continueOnCapturedContext)
			{
				this._enumerator = enumerator;
				this._continueOnCapturedContext = continueOnCapturedContext;
			}

			// Token: 0x06004702 RID: 18178 RVA: 0x0017CE08 File Offset: 0x0017C008
			public ConfiguredValueTaskAwaitable<bool> MoveNextAsync()
			{
				return this._enumerator.MoveNextAsync().ConfigureAwait(this._continueOnCapturedContext);
			}

			// Token: 0x17000AA9 RID: 2729
			// (get) Token: 0x06004703 RID: 18179 RVA: 0x0017CE2E File Offset: 0x0017C02E
			[Nullable(1)]
			public T Current
			{
				[NullableContext(1)]
				get
				{
					return this._enumerator.Current;
				}
			}

			// Token: 0x06004704 RID: 18180 RVA: 0x0017CE3C File Offset: 0x0017C03C
			public ConfiguredValueTaskAwaitable DisposeAsync()
			{
				return this._enumerator.DisposeAsync().ConfigureAwait(this._continueOnCapturedContext);
			}

			// Token: 0x0400110C RID: 4364
			private readonly IAsyncEnumerator<T> _enumerator;

			// Token: 0x0400110D RID: 4365
			private readonly bool _continueOnCapturedContext;
		}
	}
}
