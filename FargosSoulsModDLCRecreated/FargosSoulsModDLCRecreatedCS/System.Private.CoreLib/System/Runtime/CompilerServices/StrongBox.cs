using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000551 RID: 1361
	public class StrongBox<[Nullable(2)] T> : IStrongBox
	{
		// Token: 0x0600475F RID: 18271 RVA: 0x000ABD27 File Offset: 0x000AAF27
		public StrongBox()
		{
		}

		// Token: 0x06004760 RID: 18272 RVA: 0x0017D6F9 File Offset: 0x0017C8F9
		[NullableContext(1)]
		public StrongBox(T value)
		{
			this.Value = value;
		}

		// Token: 0x17000AC0 RID: 2752
		// (get) Token: 0x06004761 RID: 18273 RVA: 0x0017D708 File Offset: 0x0017C908
		// (set) Token: 0x06004762 RID: 18274 RVA: 0x0017D715 File Offset: 0x0017C915
		[Nullable(2)]
		object IStrongBox.Value
		{
			get
			{
				return this.Value;
			}
			set
			{
				this.Value = (T)((object)value);
			}
		}

		// Token: 0x04001137 RID: 4407
		[MaybeNull]
		[Nullable(1)]
		public T Value;
	}
}
