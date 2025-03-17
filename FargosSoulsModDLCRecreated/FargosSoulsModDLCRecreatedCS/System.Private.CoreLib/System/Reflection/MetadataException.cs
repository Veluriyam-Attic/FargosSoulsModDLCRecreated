using System;

namespace System.Reflection
{
	// Token: 0x020005A4 RID: 1444
	internal class MetadataException : Exception
	{
		// Token: 0x06004A0E RID: 18958 RVA: 0x0018697A File Offset: 0x00185B7A
		internal MetadataException(int hr)
		{
			this.m_hr = hr;
		}

		// Token: 0x06004A0F RID: 18959 RVA: 0x00186989 File Offset: 0x00185B89
		public override string ToString()
		{
			return string.Format("MetadataException HResult = {0:x}.", this.m_hr);
		}

		// Token: 0x0400126F RID: 4719
		private int m_hr;
	}
}
