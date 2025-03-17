using System;
using System.Text;

namespace System.IO
{
	// Token: 0x02000688 RID: 1672
	internal static class EncodingCache
	{
		// Token: 0x040017FC RID: 6140
		internal static readonly Encoding UTF8NoBOM = new UTF8Encoding(false, true);
	}
}
