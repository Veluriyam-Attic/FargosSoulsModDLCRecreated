using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006EF RID: 1775
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, Inherited = false)]
	public sealed class DynamicallyAccessedMembersAttribute : Attribute
	{
		// Token: 0x06005934 RID: 22836 RVA: 0x001B1A70 File Offset: 0x001B0C70
		public DynamicallyAccessedMembersAttribute(DynamicallyAccessedMemberTypes memberTypes)
		{
			this.MemberTypes = memberTypes;
		}

		// Token: 0x17000E8B RID: 3723
		// (get) Token: 0x06005935 RID: 22837 RVA: 0x001B1A7F File Offset: 0x001B0C7F
		public DynamicallyAccessedMemberTypes MemberTypes { get; }
	}
}
