using System;

namespace System.IO
{
	// Token: 0x0200068C RID: 1676
	[Flags]
	public enum FileAttributes
	{
		// Token: 0x04001802 RID: 6146
		ReadOnly = 1,
		// Token: 0x04001803 RID: 6147
		Hidden = 2,
		// Token: 0x04001804 RID: 6148
		System = 4,
		// Token: 0x04001805 RID: 6149
		Directory = 16,
		// Token: 0x04001806 RID: 6150
		Archive = 32,
		// Token: 0x04001807 RID: 6151
		Device = 64,
		// Token: 0x04001808 RID: 6152
		Normal = 128,
		// Token: 0x04001809 RID: 6153
		Temporary = 256,
		// Token: 0x0400180A RID: 6154
		SparseFile = 512,
		// Token: 0x0400180B RID: 6155
		ReparsePoint = 1024,
		// Token: 0x0400180C RID: 6156
		Compressed = 2048,
		// Token: 0x0400180D RID: 6157
		Offline = 4096,
		// Token: 0x0400180E RID: 6158
		NotContentIndexed = 8192,
		// Token: 0x0400180F RID: 6159
		Encrypted = 16384,
		// Token: 0x04001810 RID: 6160
		IntegrityStream = 32768,
		// Token: 0x04001811 RID: 6161
		NoScrubData = 131072
	}
}
