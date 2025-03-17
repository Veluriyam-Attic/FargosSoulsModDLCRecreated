using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000475 RID: 1141
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	public sealed class ComVisibleAttribute : Attribute
	{
		// Token: 0x06004459 RID: 17497 RVA: 0x00178FFD File Offset: 0x001781FD
		public ComVisibleAttribute(bool visibility)
		{
			this.Value = visibility;
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x0600445A RID: 17498 RVA: 0x0017900C File Offset: 0x0017820C
		public bool Value { get; }
	}
}
