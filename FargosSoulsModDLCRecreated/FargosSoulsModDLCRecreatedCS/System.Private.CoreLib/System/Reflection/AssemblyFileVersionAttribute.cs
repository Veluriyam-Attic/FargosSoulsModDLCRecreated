using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005C0 RID: 1472
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyFileVersionAttribute : Attribute
	{
		// Token: 0x06004BDA RID: 19418 RVA: 0x0018B173 File Offset: 0x0018A373
		public AssemblyFileVersionAttribute(string version)
		{
			if (version == null)
			{
				throw new ArgumentNullException("version");
			}
			this.Version = version;
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06004BDB RID: 19419 RVA: 0x0018B191 File Offset: 0x0018A391
		public string Version { get; }
	}
}
