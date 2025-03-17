using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005C4 RID: 1476
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyKeyNameAttribute : Attribute
	{
		// Token: 0x06004BE5 RID: 19429 RVA: 0x0018B1DE File Offset: 0x0018A3DE
		public AssemblyKeyNameAttribute(string keyName)
		{
			this.KeyName = keyName;
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x06004BE6 RID: 19430 RVA: 0x0018B1ED File Offset: 0x0018A3ED
		public string KeyName { get; }
	}
}
