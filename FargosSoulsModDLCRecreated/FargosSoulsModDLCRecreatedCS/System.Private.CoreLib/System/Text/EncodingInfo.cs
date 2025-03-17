using System;
using System.Runtime.CompilerServices;

namespace System.Text
{
	// Token: 0x02000376 RID: 886
	[NullableContext(1)]
	[Nullable(0)]
	public sealed class EncodingInfo
	{
		// Token: 0x06002EBC RID: 11964 RVA: 0x0015DCFF File Offset: 0x0015CEFF
		public EncodingInfo(EncodingProvider provider, int codePage, string name, string displayName) : this(codePage, name, displayName)
		{
			if (name == null || displayName == null || provider == null)
			{
				throw new ArgumentNullException((name == null) ? "name" : ((displayName == null) ? "displayName" : "provider"));
			}
			this.Provider = provider;
		}

		// Token: 0x06002EBD RID: 11965 RVA: 0x0015DD3C File Offset: 0x0015CF3C
		internal EncodingInfo(int codePage, string name, string displayName)
		{
			this.CodePage = codePage;
			this.Name = name;
			this.DisplayName = displayName;
		}

		// Token: 0x17000956 RID: 2390
		// (get) Token: 0x06002EBE RID: 11966 RVA: 0x0015DD59 File Offset: 0x0015CF59
		public int CodePage { get; }

		// Token: 0x17000957 RID: 2391
		// (get) Token: 0x06002EBF RID: 11967 RVA: 0x0015DD61 File Offset: 0x0015CF61
		public string Name { get; }

		// Token: 0x17000958 RID: 2392
		// (get) Token: 0x06002EC0 RID: 11968 RVA: 0x0015DD69 File Offset: 0x0015CF69
		public string DisplayName { get; }

		// Token: 0x06002EC1 RID: 11969 RVA: 0x0015DD71 File Offset: 0x0015CF71
		public Encoding GetEncoding()
		{
			EncodingProvider provider = this.Provider;
			return ((provider != null) ? provider.GetEncoding(this.CodePage) : null) ?? Encoding.GetEncoding(this.CodePage);
		}

		// Token: 0x06002EC2 RID: 11970 RVA: 0x0015DD9C File Offset: 0x0015CF9C
		[NullableContext(2)]
		public override bool Equals(object value)
		{
			EncodingInfo encodingInfo = value as EncodingInfo;
			return encodingInfo != null && this.CodePage == encodingInfo.CodePage;
		}

		// Token: 0x06002EC3 RID: 11971 RVA: 0x0015DDC3 File Offset: 0x0015CFC3
		public override int GetHashCode()
		{
			return this.CodePage;
		}

		// Token: 0x17000959 RID: 2393
		// (get) Token: 0x06002EC4 RID: 11972 RVA: 0x0015DDCB File Offset: 0x0015CFCB
		[Nullable(2)]
		internal EncodingProvider Provider { get; }
	}
}
