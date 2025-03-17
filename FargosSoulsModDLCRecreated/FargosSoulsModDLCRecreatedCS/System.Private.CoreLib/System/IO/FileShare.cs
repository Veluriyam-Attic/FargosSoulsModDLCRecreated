using System;

namespace System.IO
{
	// Token: 0x0200068F RID: 1679
	[Flags]
	public enum FileShare
	{
		// Token: 0x04001822 RID: 6178
		None = 0,
		// Token: 0x04001823 RID: 6179
		Read = 1,
		// Token: 0x04001824 RID: 6180
		Write = 2,
		// Token: 0x04001825 RID: 6181
		ReadWrite = 3,
		// Token: 0x04001826 RID: 6182
		Delete = 4,
		// Token: 0x04001827 RID: 6183
		Inheritable = 16
	}
}
