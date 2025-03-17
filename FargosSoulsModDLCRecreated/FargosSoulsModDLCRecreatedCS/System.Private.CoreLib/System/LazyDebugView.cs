using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;

namespace System
{
	// Token: 0x02000143 RID: 323
	internal sealed class LazyDebugView<[DynamicallyAccessedMembers(DynamicallyAccessedMemberTypes.PublicParameterlessConstructor)] T>
	{
		// Token: 0x06001053 RID: 4179 RVA: 0x000DB625 File Offset: 0x000DA825
		public LazyDebugView(Lazy<T> lazy)
		{
			this._lazy = lazy;
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x06001054 RID: 4180 RVA: 0x000DB634 File Offset: 0x000DA834
		public bool IsValueCreated
		{
			get
			{
				return this._lazy.IsValueCreated;
			}
		}

		// Token: 0x1700016E RID: 366
		// (get) Token: 0x06001055 RID: 4181 RVA: 0x000DB641 File Offset: 0x000DA841
		public T Value
		{
			get
			{
				return this._lazy.ValueForDebugDisplay;
			}
		}

		// Token: 0x1700016F RID: 367
		// (get) Token: 0x06001056 RID: 4182 RVA: 0x000DB64E File Offset: 0x000DA84E
		public LazyThreadSafetyMode? Mode
		{
			get
			{
				return this._lazy.Mode;
			}
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x06001057 RID: 4183 RVA: 0x000DB65B File Offset: 0x000DA85B
		public bool IsValueFaulted
		{
			get
			{
				return this._lazy.IsValueFaulted;
			}
		}

		// Token: 0x0400040A RID: 1034
		private readonly Lazy<T> _lazy;
	}
}
