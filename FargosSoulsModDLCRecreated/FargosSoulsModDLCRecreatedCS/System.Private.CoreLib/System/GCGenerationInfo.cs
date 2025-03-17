using System;

namespace System
{
	// Token: 0x0200010B RID: 267
	public readonly struct GCGenerationInfo
	{
		// Token: 0x1700013B RID: 315
		// (get) Token: 0x06000E19 RID: 3609 RVA: 0x000D0145 File Offset: 0x000CF345
		public long SizeBeforeBytes { get; }

		// Token: 0x1700013C RID: 316
		// (get) Token: 0x06000E1A RID: 3610 RVA: 0x000D014D File Offset: 0x000CF34D
		public long FragmentationBeforeBytes { get; }

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000E1B RID: 3611 RVA: 0x000D0155 File Offset: 0x000CF355
		public long SizeAfterBytes { get; }

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000E1C RID: 3612 RVA: 0x000D015D File Offset: 0x000CF35D
		public long FragmentationAfterBytes { get; }
	}
}
