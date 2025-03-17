using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200052B RID: 1323
	[AttributeUsage(AttributeTargets.Assembly)]
	public sealed class DefaultDependencyAttribute : Attribute
	{
		// Token: 0x06004722 RID: 18210 RVA: 0x0017D52D File Offset: 0x0017C72D
		public DefaultDependencyAttribute(LoadHint loadHintArgument)
		{
			this.LoadHint = loadHintArgument;
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06004723 RID: 18211 RVA: 0x0017D53C File Offset: 0x0017C73C
		public LoadHint LoadHint { get; }
	}
}
