using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005CC RID: 1484
	[Nullable(0)]
	[NullableContext(1)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyTrademarkAttribute : Attribute
	{
		// Token: 0x06004BF7 RID: 19447 RVA: 0x0018B5D5 File Offset: 0x0018A7D5
		public AssemblyTrademarkAttribute(string trademark)
		{
			this.Trademark = trademark;
		}

		// Token: 0x17000BF8 RID: 3064
		// (get) Token: 0x06004BF8 RID: 19448 RVA: 0x0018B5E4 File Offset: 0x0018A7E4
		public string Trademark { get; }
	}
}
