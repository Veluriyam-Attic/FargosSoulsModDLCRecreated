using System;

namespace System.Text
{
	// Token: 0x0200035B RID: 859
	internal class CodePageDataItem
	{
		// Token: 0x1700090B RID: 2315
		// (get) Token: 0x06002D59 RID: 11609 RVA: 0x00159669 File Offset: 0x00158869
		public int UIFamilyCodePage { get; }

		// Token: 0x1700090C RID: 2316
		// (get) Token: 0x06002D5A RID: 11610 RVA: 0x00159671 File Offset: 0x00158871
		public string WebName { get; }

		// Token: 0x1700090D RID: 2317
		// (get) Token: 0x06002D5B RID: 11611 RVA: 0x00159679 File Offset: 0x00158879
		public string HeaderName { get; }

		// Token: 0x1700090E RID: 2318
		// (get) Token: 0x06002D5C RID: 11612 RVA: 0x00159681 File Offset: 0x00158881
		public string BodyName { get; }

		// Token: 0x1700090F RID: 2319
		// (get) Token: 0x06002D5D RID: 11613 RVA: 0x00159689 File Offset: 0x00158889
		public string DisplayName { get; }

		// Token: 0x17000910 RID: 2320
		// (get) Token: 0x06002D5E RID: 11614 RVA: 0x00159691 File Offset: 0x00158891
		public uint Flags { get; }

		// Token: 0x06002D5F RID: 11615 RVA: 0x00159699 File Offset: 0x00158899
		internal CodePageDataItem(int uiFamilyCodePage, string webName, string headerName, string bodyName, string displayName, uint flags)
		{
			this.UIFamilyCodePage = uiFamilyCodePage;
			this.WebName = webName;
			this.HeaderName = headerName;
			this.BodyName = bodyName;
			this.DisplayName = displayName;
			this.Flags = flags;
		}
	}
}
