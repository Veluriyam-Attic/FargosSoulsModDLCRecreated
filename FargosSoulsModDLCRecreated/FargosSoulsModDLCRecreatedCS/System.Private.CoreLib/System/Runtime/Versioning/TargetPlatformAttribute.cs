using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.Versioning
{
	// Token: 0x020003FC RID: 1020
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false, Inherited = false)]
	public sealed class TargetPlatformAttribute : OSPlatformAttribute
	{
		// Token: 0x0600328F RID: 12943 RVA: 0x0016B7A4 File Offset: 0x0016A9A4
		[NullableContext(1)]
		public TargetPlatformAttribute(string platformName) : base(platformName)
		{
		}
	}
}
