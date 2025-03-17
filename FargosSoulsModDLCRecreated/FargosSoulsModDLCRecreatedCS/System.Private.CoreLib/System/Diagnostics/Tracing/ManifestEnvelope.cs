using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000741 RID: 1857
	internal struct ManifestEnvelope
	{
		// Token: 0x04001B1B RID: 6939
		public ManifestEnvelope.ManifestFormats Format;

		// Token: 0x04001B1C RID: 6940
		public byte MajorVersion;

		// Token: 0x04001B1D RID: 6941
		public byte MinorVersion;

		// Token: 0x04001B1E RID: 6942
		public byte Magic;

		// Token: 0x04001B1F RID: 6943
		public ushort TotalChunks;

		// Token: 0x04001B20 RID: 6944
		public ushort ChunkNumber;

		// Token: 0x02000742 RID: 1858
		public enum ManifestFormats : byte
		{
			// Token: 0x04001B22 RID: 6946
			SimpleXmlFormat = 1
		}
	}
}
