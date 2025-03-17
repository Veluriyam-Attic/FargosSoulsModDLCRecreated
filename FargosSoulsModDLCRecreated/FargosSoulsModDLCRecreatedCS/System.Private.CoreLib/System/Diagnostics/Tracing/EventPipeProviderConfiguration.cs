using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000714 RID: 1812
	internal struct EventPipeProviderConfiguration
	{
		// Token: 0x06005A15 RID: 23061 RVA: 0x001B3BBC File Offset: 0x001B2DBC
		internal EventPipeProviderConfiguration(string providerName, ulong keywords, uint loggingLevel, string filterData)
		{
			if (string.IsNullOrEmpty(providerName))
			{
				throw new ArgumentNullException("providerName");
			}
			if (loggingLevel > 5U)
			{
				throw new ArgumentOutOfRangeException("loggingLevel");
			}
			this.m_providerName = providerName;
			this.m_keywords = keywords;
			this.m_loggingLevel = loggingLevel;
			this.m_filterData = filterData;
		}

		// Token: 0x17000ECF RID: 3791
		// (get) Token: 0x06005A16 RID: 23062 RVA: 0x001B3C08 File Offset: 0x001B2E08
		internal string ProviderName
		{
			get
			{
				return this.m_providerName;
			}
		}

		// Token: 0x17000ED0 RID: 3792
		// (get) Token: 0x06005A17 RID: 23063 RVA: 0x001B3C10 File Offset: 0x001B2E10
		internal ulong Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x17000ED1 RID: 3793
		// (get) Token: 0x06005A18 RID: 23064 RVA: 0x001B3C18 File Offset: 0x001B2E18
		internal uint LoggingLevel
		{
			get
			{
				return this.m_loggingLevel;
			}
		}

		// Token: 0x17000ED2 RID: 3794
		// (get) Token: 0x06005A19 RID: 23065 RVA: 0x001B3C20 File Offset: 0x001B2E20
		internal string FilterData
		{
			get
			{
				return this.m_filterData;
			}
		}

		// Token: 0x04001A3E RID: 6718
		[MarshalAs(UnmanagedType.LPWStr)]
		private readonly string m_providerName;

		// Token: 0x04001A3F RID: 6719
		private readonly ulong m_keywords;

		// Token: 0x04001A40 RID: 6720
		private readonly uint m_loggingLevel;

		// Token: 0x04001A41 RID: 6721
		[MarshalAs(UnmanagedType.LPWStr)]
		private readonly string m_filterData;
	}
}
