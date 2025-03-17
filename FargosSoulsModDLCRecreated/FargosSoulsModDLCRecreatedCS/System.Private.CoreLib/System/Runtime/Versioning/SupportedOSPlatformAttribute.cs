using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x020003FD RID: 1021
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	public sealed class SupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06003290 RID: 12944 RVA: 0x0016B7A4 File Offset: 0x0016A9A4
		[NullableContext(1)]
		public SupportedOSPlatformAttribute(string platformName) : base(platformName)
		{
		}
	}
}
