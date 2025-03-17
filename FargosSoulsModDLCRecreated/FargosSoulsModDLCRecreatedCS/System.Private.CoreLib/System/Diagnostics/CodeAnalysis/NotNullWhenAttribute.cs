using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006F6 RID: 1782
	[AttributeUsage(AttributeTargets.Parameter, Inherited = false)]
	public sealed class NotNullWhenAttribute : Attribute
	{
		// Token: 0x06005948 RID: 22856 RVA: 0x001B1B4C File Offset: 0x001B0D4C
		public NotNullWhenAttribute(bool returnValue)
		{
			this.ReturnValue = returnValue;
		}

		// Token: 0x17000E93 RID: 3731
		// (get) Token: 0x06005949 RID: 22857 RVA: 0x001B1B5B File Offset: 0x001B0D5B
		public bool ReturnValue { get; }
	}
}
