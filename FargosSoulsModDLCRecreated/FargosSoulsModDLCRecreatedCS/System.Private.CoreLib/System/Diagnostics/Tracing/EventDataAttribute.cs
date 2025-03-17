using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200075D RID: 1885
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct, Inherited = false)]
	public class EventDataAttribute : Attribute
	{
		// Token: 0x17000F14 RID: 3860
		// (get) Token: 0x06005C80 RID: 23680 RVA: 0x001C18D4 File Offset: 0x001C0AD4
		// (set) Token: 0x06005C81 RID: 23681 RVA: 0x001C18DC File Offset: 0x001C0ADC
		public string Name { get; set; }

		// Token: 0x17000F15 RID: 3861
		// (get) Token: 0x06005C82 RID: 23682 RVA: 0x001C18E5 File Offset: 0x001C0AE5
		internal EventLevel Level
		{
			get
			{
				return this.level;
			}
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x06005C83 RID: 23683 RVA: 0x001C18ED File Offset: 0x001C0AED
		internal EventOpcode Opcode
		{
			get
			{
				return this.opcode;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x06005C84 RID: 23684 RVA: 0x001C18F5 File Offset: 0x001C0AF5
		internal EventKeywords Keywords { get; }

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x06005C85 RID: 23685 RVA: 0x001C18FD File Offset: 0x001C0AFD
		internal EventTags Tags { get; }

		// Token: 0x04001BBD RID: 7101
		private EventLevel level = (EventLevel)(-1);

		// Token: 0x04001BBE RID: 7102
		private EventOpcode opcode = (EventOpcode)(-1);
	}
}
