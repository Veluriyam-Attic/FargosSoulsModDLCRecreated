using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006F4 RID: 1780
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class NotNullAttribute : Attribute
	{
	}
}
