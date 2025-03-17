using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005BB RID: 1467
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, Inherited = false)]
	public sealed class AssemblyCopyrightAttribute : Attribute
	{
		// Token: 0x06004BD0 RID: 19408 RVA: 0x0018B100 File Offset: 0x0018A300
		public AssemblyCopyrightAttribute(string copyright)
		{
			this.Copyright = copyright;
		}

		// Token: 0x17000BE7 RID: 3047
		// (get) Token: 0x06004BD1 RID: 19409 RVA: 0x0018B10F File Offset: 0x0018A30F
		public string Copyright { get; }
	}
}
