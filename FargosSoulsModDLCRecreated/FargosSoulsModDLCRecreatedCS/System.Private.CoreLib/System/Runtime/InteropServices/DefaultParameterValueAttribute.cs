using System;
using System.Runtime.CompilerServices;

namespace System.Runtime.InteropServices
{
	// Token: 0x0200047C RID: 1148
	[NullableContext(2)]
	[Nullable(0)]
	[AttributeUsage(AttributeTargets.Parameter)]
	public sealed class DefaultParameterValueAttribute : Attribute
	{
		// Token: 0x0600446D RID: 17517 RVA: 0x00179135 File Offset: 0x00178335
		public DefaultParameterValueAttribute(object value)
		{
			this.Value = value;
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x0600446E RID: 17518 RVA: 0x00179144 File Offset: 0x00178344
		public object Value { get; }
	}
}
