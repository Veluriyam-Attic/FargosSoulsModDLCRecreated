using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200047B RID: 1147
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Method, AllowMultiple = false)]
	public sealed class DefaultDllImportSearchPathsAttribute : Attribute
	{
		// Token: 0x0600446B RID: 17515 RVA: 0x0017911E File Offset: 0x0017831E
		public DefaultDllImportSearchPathsAttribute(DllImportSearchPath paths)
		{
			this.Paths = paths;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x0600446C RID: 17516 RVA: 0x0017912D File Offset: 0x0017832D
		public DllImportSearchPath Paths { get; }
	}
}
