using System;

namespace System.Diagnostics.CodeAnalysis
{
	// Token: 0x020006F3 RID: 1779
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, Inherited = false)]
	public sealed class MaybeNullAttribute : Attribute
	{
	}
}
