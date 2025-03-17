using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006ED RID: 1773
	[AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, Inherited = false)]
	[Nullable(0)]
	[NullableContext(1)]
	public sealed class RequiresUnreferencedCodeAttribute : Attribute
	{
		// Token: 0x06005930 RID: 22832 RVA: 0x001B1A48 File Offset: 0x001B0C48
		public RequiresUnreferencedCodeAttribute(string message)
		{
			this.Message = message;
		}

		// Token: 0x17000E89 RID: 3721
		// (get) Token: 0x06005931 RID: 22833 RVA: 0x001B1A57 File Offset: 0x001B0C57
		public string Message { get; }

		// Token: 0x17000E8A RID: 3722
		// (get) Token: 0x06005932 RID: 22834 RVA: 0x001B1A5F File Offset: 0x001B0C5F
		// (set) Token: 0x06005933 RID: 22835 RVA: 0x001B1A67 File Offset: 0x001B0C67
		[Nullable(2)]
		public string Url { [NullableContext(2)] get; [NullableContext(2)] set; }
	}
}
