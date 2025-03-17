using System;
using Microsoft.CodeAnalysis;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000007 RID: 7
	[CompilerGenerated]
	[Embedded]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event | AttributeTargets.Parameter | AttributeTargets.ReturnValue | AttributeTargets.GenericParameter, AllowMultiple = false, Inherited = false)]
	internal sealed class NativeIntegerAttribute : Attribute
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000AAA49 File Offset: 0x000A9C49
		public NativeIntegerAttribute()
		{
			this.TransformFlags = new bool[]
			{
				true
			};
		}

		// Token: 0x04000004 RID: 4
		public readonly bool[] TransformFlags;
	}
}
