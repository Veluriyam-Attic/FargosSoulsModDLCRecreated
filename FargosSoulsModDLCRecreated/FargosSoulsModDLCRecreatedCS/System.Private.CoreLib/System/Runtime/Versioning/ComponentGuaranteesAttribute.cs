using System;

namespace System.Runtime.Versioning
{
	// Token: 0x020003F8 RID: 1016
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Event | AttributeTargets.Interface | AttributeTargets.Delegate, AllowMultiple = false, Inherited = false)]
	public sealed class ComponentGuaranteesAttribute : Attribute
	{
		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x0600327E RID: 12926 RVA: 0x0016B3D2 File Offset: 0x0016A5D2
		public ComponentGuaranteesOptions Guarantees { get; }

		// Token: 0x0600327F RID: 12927 RVA: 0x0016B3DA File Offset: 0x0016A5DA
		public ComponentGuaranteesAttribute(ComponentGuaranteesOptions guarantees)
		{
			this.Guarantees = guarantees;
		}
	}
}
