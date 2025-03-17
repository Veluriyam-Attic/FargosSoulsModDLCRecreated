using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x02000532 RID: 1330
	[NullableContext(1)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FixedBufferAttribute : Attribute
	{
		// Token: 0x0600472C RID: 18220 RVA: 0x0017D56A File Offset: 0x0017C76A
		public FixedBufferAttribute(Type elementType, int length)
		{
			this.ElementType = elementType;
			this.Length = length;
		}

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x0600472D RID: 18221 RVA: 0x0017D580 File Offset: 0x0017C780
		public Type ElementType { get; }

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x0600472E RID: 18222 RVA: 0x0017D588 File Offset: 0x0017C788
		public int Length { get; }
	}
}
