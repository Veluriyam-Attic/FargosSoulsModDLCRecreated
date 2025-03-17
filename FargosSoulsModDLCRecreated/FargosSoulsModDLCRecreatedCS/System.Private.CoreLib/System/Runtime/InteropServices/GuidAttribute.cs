using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000485 RID: 1157
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Interface | AttributeTargets.Delegate, Inherited = false)]
	public sealed class GuidAttribute : Attribute
	{
		// Token: 0x06004482 RID: 17538 RVA: 0x001792FD File Offset: 0x001784FD
		public GuidAttribute(string guid)
		{
			this.Value = guid;
		}

		// Token: 0x17000A73 RID: 2675
		// (get) Token: 0x06004483 RID: 17539 RVA: 0x0017930C File Offset: 0x0017850C
		public string Value { get; }
	}
}
