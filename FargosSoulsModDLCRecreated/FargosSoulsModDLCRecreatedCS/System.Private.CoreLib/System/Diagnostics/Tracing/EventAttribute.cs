using System;
using System.Runtime.CompilerServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000736 RID: 1846
	[Nullable(0)]
	[NullableContext(2)]
	[AttributeUsage(AttributeTargets.Method)]
	public sealed class EventAttribute : Attribute
	{
		// Token: 0x06005B3F RID: 23359 RVA: 0x001BC541 File Offset: 0x001BB741
		public EventAttribute(int eventId)
		{
			this.EventId = eventId;
			this.Level = EventLevel.Informational;
		}

		// Token: 0x17000EFF RID: 3839
		// (get) Token: 0x06005B40 RID: 23360 RVA: 0x001BC557 File Offset: 0x001BB757
		// (set) Token: 0x06005B41 RID: 23361 RVA: 0x001BC55F File Offset: 0x001BB75F
		public int EventId { get; private set; }

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06005B42 RID: 23362 RVA: 0x001BC568 File Offset: 0x001BB768
		// (set) Token: 0x06005B43 RID: 23363 RVA: 0x001BC570 File Offset: 0x001BB770
		public EventLevel Level { get; set; }

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06005B44 RID: 23364 RVA: 0x001BC579 File Offset: 0x001BB779
		// (set) Token: 0x06005B45 RID: 23365 RVA: 0x001BC581 File Offset: 0x001BB781
		public EventKeywords Keywords { get; set; }

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06005B46 RID: 23366 RVA: 0x001BC58A File Offset: 0x001BB78A
		// (set) Token: 0x06005B47 RID: 23367 RVA: 0x001BC592 File Offset: 0x001BB792
		public EventOpcode Opcode
		{
			get
			{
				return this.m_opcode;
			}
			set
			{
				this.m_opcode = value;
				this.m_opcodeSet = true;
			}
		}

		// Token: 0x17000F03 RID: 3843
		// (get) Token: 0x06005B48 RID: 23368 RVA: 0x001BC5A2 File Offset: 0x001BB7A2
		internal bool IsOpcodeSet
		{
			get
			{
				return this.m_opcodeSet;
			}
		}

		// Token: 0x17000F04 RID: 3844
		// (get) Token: 0x06005B49 RID: 23369 RVA: 0x001BC5AA File Offset: 0x001BB7AA
		// (set) Token: 0x06005B4A RID: 23370 RVA: 0x001BC5B2 File Offset: 0x001BB7B2
		public EventTask Task { get; set; }

		// Token: 0x17000F05 RID: 3845
		// (get) Token: 0x06005B4B RID: 23371 RVA: 0x001BC5BB File Offset: 0x001BB7BB
		// (set) Token: 0x06005B4C RID: 23372 RVA: 0x001BC5C3 File Offset: 0x001BB7C3
		public EventChannel Channel { get; set; }

		// Token: 0x17000F06 RID: 3846
		// (get) Token: 0x06005B4D RID: 23373 RVA: 0x001BC5CC File Offset: 0x001BB7CC
		// (set) Token: 0x06005B4E RID: 23374 RVA: 0x001BC5D4 File Offset: 0x001BB7D4
		public byte Version { get; set; }

		// Token: 0x17000F07 RID: 3847
		// (get) Token: 0x06005B4F RID: 23375 RVA: 0x001BC5DD File Offset: 0x001BB7DD
		// (set) Token: 0x06005B50 RID: 23376 RVA: 0x001BC5E5 File Offset: 0x001BB7E5
		public string Message { get; set; }

		// Token: 0x17000F08 RID: 3848
		// (get) Token: 0x06005B51 RID: 23377 RVA: 0x001BC5EE File Offset: 0x001BB7EE
		// (set) Token: 0x06005B52 RID: 23378 RVA: 0x001BC5F6 File Offset: 0x001BB7F6
		public EventTags Tags { get; set; }

		// Token: 0x17000F09 RID: 3849
		// (get) Token: 0x06005B53 RID: 23379 RVA: 0x001BC5FF File Offset: 0x001BB7FF
		// (set) Token: 0x06005B54 RID: 23380 RVA: 0x001BC607 File Offset: 0x001BB807
		public EventActivityOptions ActivityOptions { get; set; }

		// Token: 0x04001AEB RID: 6891
		private EventOpcode m_opcode;

		// Token: 0x04001AEC RID: 6892
		private bool m_opcodeSet;
	}
}
