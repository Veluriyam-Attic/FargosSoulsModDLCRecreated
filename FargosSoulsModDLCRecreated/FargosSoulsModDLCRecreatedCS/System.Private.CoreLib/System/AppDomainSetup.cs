using System;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020000C1 RID: 193
	[Nullable(0)]
	[NullableContext(2)]
	public sealed class AppDomainSetup
	{
		// Token: 0x06000A19 RID: 2585 RVA: 0x000ABD27 File Offset: 0x000AAF27
		internal AppDomainSetup()
		{
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000A1A RID: 2586 RVA: 0x000C7DFB File Offset: 0x000C6FFB
		public string ApplicationBase
		{
			get
			{
				return AppContext.BaseDirectory;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000A1B RID: 2587 RVA: 0x000C8459 File Offset: 0x000C7659
		public string TargetFrameworkName
		{
			get
			{
				return AppContext.TargetFrameworkName;
			}
		}
	}
}
