using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics
{
	// Token: 0x020006CD RID: 1741
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class ConditionalAttribute : Attribute
	{
		// Token: 0x06005880 RID: 22656 RVA: 0x001B0B34 File Offset: 0x001AFD34
		public ConditionalAttribute(string conditionString)
		{
			this.ConditionString = conditionString;
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x06005881 RID: 22657 RVA: 0x001B0B43 File Offset: 0x001AFD43
		public string ConditionString { get; }
	}
}
