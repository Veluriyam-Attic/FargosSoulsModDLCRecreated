using System;

namespace System.Diagnostics.SymbolStore
{
	// Token: 0x02000701 RID: 1793
	internal struct SymbolToken
	{
		// Token: 0x06005977 RID: 22903 RVA: 0x001B1CEA File Offset: 0x001B0EEA
		public SymbolToken(int val)
		{
			this.m_token = val;
		}

		// Token: 0x06005978 RID: 22904 RVA: 0x001B1CF3 File Offset: 0x001B0EF3
		public int GetToken()
		{
			return this.m_token;
		}

		// Token: 0x06005979 RID: 22905 RVA: 0x001B1CF3 File Offset: 0x001B0EF3
		public override int GetHashCode()
		{
			return this.m_token;
		}

		// Token: 0x0600597A RID: 22906 RVA: 0x001B1CFB File Offset: 0x001B0EFB
		public override bool Equals(object obj)
		{
			return obj is SymbolToken && this.Equals((SymbolToken)obj);
		}

		// Token: 0x0600597B RID: 22907 RVA: 0x001B1D13 File Offset: 0x001B0F13
		public bool Equals(SymbolToken obj)
		{
			return obj.m_token == this.m_token;
		}

		// Token: 0x040019DD RID: 6621
		internal int m_token;
	}
}
