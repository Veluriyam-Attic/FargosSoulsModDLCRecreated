using System;
using System.Diagnostics;

namespace System
{
	// Token: 0x0200017F RID: 383
	internal sealed class SpanDebugView<T>
	{
		// Token: 0x0600134D RID: 4941 RVA: 0x000E954D File Offset: 0x000E874D
		public SpanDebugView(Span<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x0600134E RID: 4942 RVA: 0x000E9562 File Offset: 0x000E8762
		public SpanDebugView(ReadOnlySpan<T> span)
		{
			this._array = span.ToArray();
		}

		// Token: 0x170001AF RID: 431
		// (get) Token: 0x0600134F RID: 4943 RVA: 0x000E9577 File Offset: 0x000E8777
		[DebuggerBrowsable(DebuggerBrowsableState.RootHidden)]
		public T[] Items
		{
			get
			{
				return this._array;
			}
		}

		// Token: 0x04000498 RID: 1176
		private readonly T[] _array;
	}
}
