using System;
using System.Runtime.CompilerServices;

namespace System.Threading
{
	// Token: 0x0200027F RID: 639
	[NullableContext(2)]
	[Nullable(0)]
	public readonly struct AsyncLocalValueChangedArgs<T>
	{
		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x001434B2 File Offset: 0x001426B2
		public T PreviousValue { get; }

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060026EE RID: 9966 RVA: 0x001434BA File Offset: 0x001426BA
		public T CurrentValue { get; }

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x001434C2 File Offset: 0x001426C2
		public bool ThreadContextChanged { get; }

		// Token: 0x060026F0 RID: 9968 RVA: 0x001434CA File Offset: 0x001426CA
		internal AsyncLocalValueChangedArgs(T previousValue, T currentValue, bool contextChanged)
		{
			this.PreviousValue = previousValue;
			this.CurrentValue = currentValue;
			this.ThreadContextChanged = contextChanged;
		}
	}
}
