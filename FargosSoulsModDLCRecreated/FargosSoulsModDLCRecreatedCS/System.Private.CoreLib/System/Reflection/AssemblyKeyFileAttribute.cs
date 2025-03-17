using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005C3 RID: 1475
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class AssemblyKeyFileAttribute : Attribute
	{
		// Token: 0x06004BE3 RID: 19427 RVA: 0x0018B1C7 File Offset: 0x0018A3C7
		public AssemblyKeyFileAttribute(string keyFile)
		{
			this.KeyFile = keyFile;
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x06004BE4 RID: 19428 RVA: 0x0018B1D6 File Offset: 0x0018A3D6
		public string KeyFile { get; }
	}
}
