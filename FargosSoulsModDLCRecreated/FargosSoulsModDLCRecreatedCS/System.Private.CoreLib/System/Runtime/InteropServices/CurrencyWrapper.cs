using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000477 RID: 1143
	public sealed class CurrencyWrapper
	{
		// Token: 0x06004466 RID: 17510 RVA: 0x001790C4 File Offset: 0x001782C4
		public CurrencyWrapper(decimal obj)
		{
			this.WrappedObject = obj;
		}

		// Token: 0x06004467 RID: 17511 RVA: 0x001790D3 File Offset: 0x001782D3
		[NullableContext(1)]
		public CurrencyWrapper(object obj)
		{
			if (!(obj is decimal))
			{
				throw new ArgumentException(SR.Arg_MustBeDecimal, "obj");
			}
			this.WrappedObject = (decimal)obj;
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06004468 RID: 17512 RVA: 0x001790FF File Offset: 0x001782FF
		public decimal WrappedObject { get; }
	}
}
