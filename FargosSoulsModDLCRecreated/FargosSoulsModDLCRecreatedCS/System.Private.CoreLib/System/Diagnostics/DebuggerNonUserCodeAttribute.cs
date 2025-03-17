using System;

namespace System.Diagnostics
{
	// Token: 0x020006D5 RID: 1749
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property, Inherited = false)]
	public sealed class DebuggerNonUserCodeAttribute : Attribute
	{
	}
}
