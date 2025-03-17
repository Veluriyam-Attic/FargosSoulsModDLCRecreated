using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006FB RID: 1787
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
	public sealed class MemberNotNullWhenAttribute : Attribute
	{
		// Token: 0x06005952 RID: 22866 RVA: 0x001B1BC0 File Offset: 0x001B0DC0
		public MemberNotNullWhenAttribute(bool returnValue, string member)
		{
			this.ReturnValue = returnValue;
			this.Members = new string[]
			{
				member
			};
		}

		// Token: 0x06005953 RID: 22867 RVA: 0x001B1BDF File Offset: 0x001B0DDF
		public MemberNotNullWhenAttribute(bool returnValue, params string[] members)
		{
			this.ReturnValue = returnValue;
			this.Members = members;
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x06005954 RID: 22868 RVA: 0x001B1BF5 File Offset: 0x001B0DF5
		public bool ReturnValue { get; }

		// Token: 0x17000E98 RID: 3736
		// (get) Token: 0x06005955 RID: 22869 RVA: 0x001B1BFD File Offset: 0x001B0DFD
		public string[] Members { get; }
	}
}
