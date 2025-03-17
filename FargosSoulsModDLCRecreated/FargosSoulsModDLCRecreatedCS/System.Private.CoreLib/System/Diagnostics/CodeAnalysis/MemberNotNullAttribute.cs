using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006FA RID: 1786
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	public sealed class MemberNotNullAttribute : Attribute
	{
		// Token: 0x0600594F RID: 22863 RVA: 0x001B1B91 File Offset: 0x001B0D91
		public MemberNotNullAttribute(string member)
		{
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x06005950 RID: 22864 RVA: 0x001B1BA9 File Offset: 0x001B0DA9
		public MemberNotNullAttribute(params string[] members)
		{
			this.Members = members;
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x06005951 RID: 22865 RVA: 0x001B1BB8 File Offset: 0x001B0DB8
		public string[] Members { get; }
	}
}
