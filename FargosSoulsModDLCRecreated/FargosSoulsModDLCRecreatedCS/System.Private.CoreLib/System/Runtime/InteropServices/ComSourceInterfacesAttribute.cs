using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000474 RID: 1140
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class ComSourceInterfacesAttribute : Attribute
	{
		// Token: 0x06004453 RID: 17491 RVA: 0x00178EFC File Offset: 0x001780FC
		public ComSourceInterfacesAttribute(string sourceInterfaces)
		{
			this.Value = sourceInterfaces;
		}

		// Token: 0x06004454 RID: 17492 RVA: 0x00178F0B File Offset: 0x0017810B
		public ComSourceInterfacesAttribute(Type sourceInterface)
		{
			this.Value = sourceInterface.FullName;
		}

		// Token: 0x06004455 RID: 17493 RVA: 0x00178F1F File Offset: 0x0017811F
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2)
		{
			this.Value = sourceInterface1.FullName + "\0" + sourceInterface2.FullName;
		}

		// Token: 0x06004456 RID: 17494 RVA: 0x00178F44 File Offset: 0x00178144
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3)
		{
			this.Value = string.Concat(new string[]
			{
				sourceInterface1.FullName,
				"\0",
				sourceInterface2.FullName,
				"\0",
				sourceInterface3.FullName
			});
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x00178F94 File Offset: 0x00178194
		public ComSourceInterfacesAttribute(Type sourceInterface1, Type sourceInterface2, Type sourceInterface3, Type sourceInterface4)
		{
			this.Value = string.Concat(new string[]
			{
				sourceInterface1.FullName,
				"\0",
				sourceInterface2.FullName,
				"\0",
				sourceInterface3.FullName,
				"\0",
				sourceInterface4.FullName
			});
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06004458 RID: 17496 RVA: 0x00178FF5 File Offset: 0x001781F5
		public string Value { get; }
	}
}
