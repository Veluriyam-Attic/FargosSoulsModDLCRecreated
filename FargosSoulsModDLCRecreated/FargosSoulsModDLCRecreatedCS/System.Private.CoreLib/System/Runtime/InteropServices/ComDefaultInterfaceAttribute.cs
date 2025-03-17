using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200046E RID: 1134
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class ComDefaultInterfaceAttribute : Attribute
	{
		// Token: 0x06004447 RID: 17479 RVA: 0x00178DAC File Offset: 0x00177FAC
		public ComDefaultInterfaceAttribute(Type defaultInterface)
		{
			this.Value = defaultInterface;
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06004448 RID: 17480 RVA: 0x00178DBB File Offset: 0x00177FBB
		public Type Value { get; }
	}
}
