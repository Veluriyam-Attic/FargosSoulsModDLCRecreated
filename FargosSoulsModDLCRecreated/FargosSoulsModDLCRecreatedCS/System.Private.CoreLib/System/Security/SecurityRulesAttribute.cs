using System;

namespace System.Security
{
	// Token: 0x020003C0 RID: 960
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
	public sealed class SecurityRulesAttribute : Attribute
	{
		// Token: 0x06003196 RID: 12694 RVA: 0x00169F1D File Offset: 0x0016911D
		public SecurityRulesAttribute(SecurityRuleSet ruleSet)
		{
			this.RuleSet = ruleSet;
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x06003197 RID: 12695 RVA: 0x00169F2C File Offset: 0x0016912C
		// (set) Token: 0x06003198 RID: 12696 RVA: 0x00169F34 File Offset: 0x00169134
		public bool SkipVerificationInFullTrust { get; set; }

		// Token: 0x17000994 RID: 2452
		// (get) Token: 0x06003199 RID: 12697 RVA: 0x00169F3D File Offset: 0x0016913D
		public SecurityRuleSet RuleSet { get; }
	}
}
