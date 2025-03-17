using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005C9 RID: 1481
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyProductAttribute : Attribute
	{
		// Token: 0x06004BF0 RID: 19440 RVA: 0x0018B581 File Offset: 0x0018A781
		public AssemblyProductAttribute(string product)
		{
			this.Product = product;
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x06004BF1 RID: 19441 RVA: 0x0018B590 File Offset: 0x0018A790
		public string Product { get; }
	}
}
