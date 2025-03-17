using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200049A RID: 1178
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Class, Inherited = false)]
	public sealed class ProgIdAttribute : Attribute
	{
		// Token: 0x060044B1 RID: 17585 RVA: 0x0017943C File Offset: 0x0017863C
		public ProgIdAttribute(string progId)
		{
			this.Value = progId;
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x060044B2 RID: 17586 RVA: 0x0017944B File Offset: 0x0017864B
		public string Value { get; }
	}
}
