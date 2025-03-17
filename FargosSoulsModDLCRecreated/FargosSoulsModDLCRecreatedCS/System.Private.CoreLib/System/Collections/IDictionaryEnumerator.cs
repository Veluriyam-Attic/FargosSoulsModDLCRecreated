using System;
using System.Runtime.CompilerServices;

namespace System.Collections
{
	// Token: 0x020007B8 RID: 1976
	[NullableContext(1)]
	public interface IDictionaryEnumerator : IEnumerator
	{
		// Token: 0x17000FB3 RID: 4019
		// (get) Token: 0x06005F9E RID: 24478
		object Key { get; }

		// Token: 0x17000FB4 RID: 4020
		// (get) Token: 0x06005F9F RID: 24479
		[Nullable(2)]
		object Value { [NullableContext(2)] get; }

		// Token: 0x17000FB5 RID: 4021
		// (get) Token: 0x06005FA0 RID: 24480
		DictionaryEntry Entry { get; }
	}
}
