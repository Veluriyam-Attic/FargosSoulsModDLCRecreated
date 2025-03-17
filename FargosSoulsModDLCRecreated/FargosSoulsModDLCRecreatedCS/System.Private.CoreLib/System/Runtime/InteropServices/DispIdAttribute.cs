using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200047E RID: 1150
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, Inherited = false)]
	public sealed class DispIdAttribute : Attribute
	{
		// Token: 0x06004471 RID: 17521 RVA: 0x0017917F File Offset: 0x0017837F
		public DispIdAttribute(int dispId)
		{
			this.Value = dispId;
		}

		// Token: 0x17000A6E RID: 2670
		// (get) Token: 0x06004472 RID: 17522 RVA: 0x0017918E File Offset: 0x0017838E
		public int Value { get; }
	}
}
