using System;

namespace System.IO
{
	// Token: 0x0200068E RID: 1678
	[Flags]
	public enum FileOptions
	{
		// Token: 0x0400181A RID: 6170
		None = 0,
		// Token: 0x0400181B RID: 6171
		WriteThrough = -2147483648,
		// Token: 0x0400181C RID: 6172
		Asynchronous = 1073741824,
		// Token: 0x0400181D RID: 6173
		RandomAccess = 268435456,
		// Token: 0x0400181E RID: 6174
		DeleteOnClose = 67108864,
		// Token: 0x0400181F RID: 6175
		SequentialScan = 134217728,
		// Token: 0x04001820 RID: 6176
		Encrypted = 16384
	}
}
