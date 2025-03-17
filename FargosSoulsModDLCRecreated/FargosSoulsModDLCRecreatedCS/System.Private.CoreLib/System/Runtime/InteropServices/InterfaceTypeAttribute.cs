using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200048E RID: 1166
	[AttributeUsage(AttributeTargets.Interface, Inherited = false)]
	public sealed class InterfaceTypeAttribute : Attribute
	{
		// Token: 0x06004495 RID: 17557 RVA: 0x00179334 File Offset: 0x00178534
		public InterfaceTypeAttribute(ComInterfaceType interfaceType)
		{
			this.Value = interfaceType;
		}

		// Token: 0x06004496 RID: 17558 RVA: 0x00179334 File Offset: 0x00178534
		public InterfaceTypeAttribute(short interfaceType)
		{
			this.Value = interfaceType;
		}

		// Token: 0x17000A76 RID: 2678
		// (get) Token: 0x06004497 RID: 17559 RVA: 0x00179343 File Offset: 0x00178543
		public ComInterfaceType Value { get; }
	}
}
