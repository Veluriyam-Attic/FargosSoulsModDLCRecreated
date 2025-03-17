using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x020003FE RID: 1022
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Module | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event, AllowMultiple = true, Inherited = false)]
	public sealed class UnsupportedOSPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x06003291 RID: 12945 RVA: 0x0016B7A4 File Offset: 0x0016A9A4
		[NullableContext(1)]
		public UnsupportedOSPlatformAttribute(string platformName) : base(platformName)
		{
		}
	}
}
