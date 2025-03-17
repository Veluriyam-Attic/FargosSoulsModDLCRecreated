using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020004FC RID: 1276
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Field)]
	public sealed class AccessedThroughPropertyAttribute : Attribute
	{
		// Token: 0x06004647 RID: 17991 RVA: 0x0017AD53 File Offset: 0x00179F53
		public AccessedThroughPropertyAttribute(string propertyName)
		{
			this.PropertyName = propertyName;
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06004648 RID: 17992 RVA: 0x0017AD62 File Offset: 0x00179F62
		public string PropertyName { get; }
	}
}
