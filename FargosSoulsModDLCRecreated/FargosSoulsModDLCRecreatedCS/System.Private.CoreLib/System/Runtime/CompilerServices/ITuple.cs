using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000543 RID: 1347
	[NullableContext(2)]
	public interface ITuple
	{
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06004748 RID: 18248
		int Length { get; }

		// Token: 0x17000ABA RID: 2746
		object this[int index]
		{
			get;
		}
	}
}
