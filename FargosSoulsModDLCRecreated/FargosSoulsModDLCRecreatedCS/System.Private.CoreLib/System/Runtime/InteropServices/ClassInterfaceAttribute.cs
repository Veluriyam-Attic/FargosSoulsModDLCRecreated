using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200046A RID: 1130
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class, Inherited = false)]
	public sealed class ClassInterfaceAttribute : Attribute
	{
		// Token: 0x06004441 RID: 17473 RVA: 0x00178D52 File Offset: 0x00177F52
		public ClassInterfaceAttribute(ClassInterfaceType classInterfaceType)
		{
			this.Value = classInterfaceType;
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x00178D52 File Offset: 0x00177F52
		public ClassInterfaceAttribute(short classInterfaceType)
		{
			this.Value = classInterfaceType;
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x00178D61 File Offset: 0x00177F61
		public ClassInterfaceType Value { get; }
	}
}
