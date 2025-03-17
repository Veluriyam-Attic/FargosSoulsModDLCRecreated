using System;

namespace System.Threading
{
	// Token: 0x02000280 RID: 640
	internal interface IAsyncLocalValueMap
	{
		// Token: 0x060026F1 RID: 9969
		bool TryGetValue(IAsyncLocal key, out object value);

		// Token: 0x060026F2 RID: 9970
		IAsyncLocalValueMap Set(IAsyncLocal key, object value, bool treatNullValueAsNonexistent);
	}
}
