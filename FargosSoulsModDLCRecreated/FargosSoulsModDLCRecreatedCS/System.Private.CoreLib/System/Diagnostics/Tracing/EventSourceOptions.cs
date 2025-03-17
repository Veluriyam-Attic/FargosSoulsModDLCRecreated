using System;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000764 RID: 1892
	public struct EventSourceOptions
	{
		// Token: 0x17000F23 RID: 3875
		// (get) Token: 0x06005CA6 RID: 23718 RVA: 0x001C1BE6 File Offset: 0x001C0DE6
		// (set) Token: 0x06005CA7 RID: 23719 RVA: 0x001C1BEE File Offset: 0x001C0DEE
		public EventLevel Level
		{
			get
			{
				return (EventLevel)this.level;
			}
			set
			{
				this.level = checked((byte)value);
				this.valuesSet |= 4;
			}
		}

		// Token: 0x17000F24 RID: 3876
		// (get) Token: 0x06005CA8 RID: 23720 RVA: 0x001C1C07 File Offset: 0x001C0E07
		// (set) Token: 0x06005CA9 RID: 23721 RVA: 0x001C1C0F File Offset: 0x001C0E0F
		public EventOpcode Opcode
		{
			get
			{
				return (EventOpcode)this.opcode;
			}
			set
			{
				this.opcode = checked((byte)value);
				this.valuesSet |= 8;
			}
		}

		// Token: 0x17000F25 RID: 3877
		// (get) Token: 0x06005CAA RID: 23722 RVA: 0x001C1C28 File Offset: 0x001C0E28
		internal bool IsOpcodeSet
		{
			get
			{
				return (this.valuesSet & 8) > 0;
			}
		}

		// Token: 0x17000F26 RID: 3878
		// (get) Token: 0x06005CAB RID: 23723 RVA: 0x001C1C35 File Offset: 0x001C0E35
		// (set) Token: 0x06005CAC RID: 23724 RVA: 0x001C1C3D File Offset: 0x001C0E3D
		public EventKeywords Keywords
		{
			get
			{
				return this.keywords;
			}
			set
			{
				this.keywords = value;
				this.valuesSet |= 1;
			}
		}

		// Token: 0x17000F27 RID: 3879
		// (get) Token: 0x06005CAD RID: 23725 RVA: 0x001C1C55 File Offset: 0x001C0E55
		// (set) Token: 0x06005CAE RID: 23726 RVA: 0x001C1C5D File Offset: 0x001C0E5D
		public EventTags Tags
		{
			get
			{
				return this.tags;
			}
			set
			{
				this.tags = value;
				this.valuesSet |= 2;
			}
		}

		// Token: 0x17000F28 RID: 3880
		// (get) Token: 0x06005CAF RID: 23727 RVA: 0x001C1C75 File Offset: 0x001C0E75
		// (set) Token: 0x06005CB0 RID: 23728 RVA: 0x001C1C7D File Offset: 0x001C0E7D
		public EventActivityOptions ActivityOptions
		{
			get
			{
				return this.activityOptions;
			}
			set
			{
				this.activityOptions = value;
				this.valuesSet |= 16;
			}
		}

		// Token: 0x04001BD5 RID: 7125
		internal EventKeywords keywords;

		// Token: 0x04001BD6 RID: 7126
		internal EventTags tags;

		// Token: 0x04001BD7 RID: 7127
		internal EventActivityOptions activityOptions;

		// Token: 0x04001BD8 RID: 7128
		internal byte level;

		// Token: 0x04001BD9 RID: 7129
		internal byte opcode;

		// Token: 0x04001BDA RID: 7130
		internal byte valuesSet;
	}
}
