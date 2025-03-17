using System;
using System.Runtime.CompilerServices;

namespace System.Reflection
{
	// Token: 0x020005C5 RID: 1477
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class AssemblyMetadataAttribute : Attribute
	{
		// Token: 0x06004BE7 RID: 19431 RVA: 0x0018B1F5 File Offset: 0x0018A3F5
		public AssemblyMetadataAttribute(string key, [Nullable(2)] string value)
		{
			this.Key = key;
			this.Value = value;
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x06004BE8 RID: 19432 RVA: 0x0018B20B File Offset: 0x0018A40B
		public string Key { get; }

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x06004BE9 RID: 19433 RVA: 0x0018B213 File Offset: 0x0018A413
		[Nullable(2)]
		public string Value { [NullableContext(2)] get; }
	}
}
