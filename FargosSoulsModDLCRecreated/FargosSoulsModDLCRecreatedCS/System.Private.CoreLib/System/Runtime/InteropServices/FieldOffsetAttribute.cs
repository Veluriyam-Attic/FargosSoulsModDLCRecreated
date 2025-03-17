using System;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000483 RID: 1155
	[AttributeUsage(AttributeTargets.Field, Inherited = false)]
	public sealed class FieldOffsetAttribute : Attribute
	{
		// Token: 0x06004480 RID: 17536 RVA: 0x001792E6 File Offset: 0x001784E6
		public FieldOffsetAttribute(int offset)
		{
			this.Value = offset;
		}

		// Token: 0x17000A72 RID: 2674
		// (get) Token: 0x06004481 RID: 17537 RVA: 0x001792F5 File Offset: 0x001784F5
		public int Value { get; }
	}
}
