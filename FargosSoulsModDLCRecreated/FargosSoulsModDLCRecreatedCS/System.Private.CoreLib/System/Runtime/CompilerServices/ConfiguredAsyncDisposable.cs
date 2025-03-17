using System;
using System.Runtime.InteropServices;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000520 RID: 1312
	[StructLayout(LayoutKind.Auto)]
	public readonly struct ConfiguredAsyncDisposable
	{
		// Token: 0x060046FB RID: 18171 RVA: 0x0017CD63 File Offset: 0x0017BF63
		internal ConfiguredAsyncDisposable(IAsyncDisposable source, bool continueOnCapturedContext)
		{
			this._source = source;
			this._continueOnCapturedContext = continueOnCapturedContext;
		}

		// Token: 0x060046FC RID: 18172 RVA: 0x0017CD74 File Offset: 0x0017BF74
		public ConfiguredValueTaskAwaitable DisposeAsync()
		{
			return this._source.DisposeAsync().ConfigureAwait(this._continueOnCapturedContext);
		}

		// Token: 0x04001107 RID: 4359
		private readonly IAsyncDisposable _source;

		// Token: 0x04001108 RID: 4360
		private readonly bool _continueOnCapturedContext;
	}
}
