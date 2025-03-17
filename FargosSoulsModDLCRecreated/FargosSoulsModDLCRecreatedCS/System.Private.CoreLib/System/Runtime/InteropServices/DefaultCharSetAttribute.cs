using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200047A RID: 1146
	[AttributeUsage(AttributeTargets.Module, Inherited = false)]
	public sealed class DefaultCharSetAttribute : Attribute
	{
		// Token: 0x06004469 RID: 17513 RVA: 0x00179107 File Offset: 0x00178307
		public DefaultCharSetAttribute(CharSet charSet)
		{
			this.CharSet = charSet;
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x0600446A RID: 17514 RVA: 0x00179116 File Offset: 0x00178316
		public CharSet CharSet { get; }
	}
}
