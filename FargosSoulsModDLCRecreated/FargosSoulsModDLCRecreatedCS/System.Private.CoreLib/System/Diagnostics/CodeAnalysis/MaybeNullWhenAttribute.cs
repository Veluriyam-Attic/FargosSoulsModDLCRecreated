using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006F5 RID: 1781
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class MaybeNullWhenAttribute : Attribute
	{
		// Token: 0x06005946 RID: 22854 RVA: 0x001B1B35 File Offset: 0x001B0D35
		public MaybeNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06005947 RID: 22855 RVA: 0x001B1B44 File Offset: 0x001B0D44
		public bool ReturnValue { get; }
	}
}
