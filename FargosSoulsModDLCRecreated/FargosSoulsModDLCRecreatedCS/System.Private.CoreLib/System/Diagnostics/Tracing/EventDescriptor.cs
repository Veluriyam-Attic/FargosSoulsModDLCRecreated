using System;
using System.Runtime.InteropServices;

namespace System.Diagnostics.Tracing
{
	// Token: 0x02000711 RID: 1809
	[StructLayout(LayoutKind.Explicit, Size = 16)]
	internal struct EventDescriptor
	{
		// Token: 0x06005A09 RID: 23049 RVA: 0x001B39AB File Offset: 0x001B2BAB
		public EventDescriptor(int traceloggingId, byte level, byte opcode, long keywords)
		{
			this.m_id = 0;
			this.m_version = 0;
			this.m_channel = 0;
			this.m_traceloggingId = traceloggingId;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_task = 0;
			this.m_keywords = keywords;
		}

		// Token: 0x06005A0A RID: 23050 RVA: 0x001B39E8 File Offset: 0x001B2BE8
		public EventDescriptor(int id, byte version, byte channel, byte level, byte opcode, int task, long keywords)
		{
			if (id < 0)
			{
				throw new ArgumentOutOfRangeException("id", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (id > 65535)
			{
				throw new ArgumentOutOfRangeException("id", SR.Format(SR.ArgumentOutOfRange_NeedValidId, 1, ushort.MaxValue));
			}
			this.m_traceloggingId = 0;
			this.m_id = (ushort)id;
			this.m_version = version;
			this.m_channel = channel;
			this.m_level = level;
			this.m_opcode = opcode;
			this.m_keywords = keywords;
			if (task < 0)
			{
				throw new ArgumentOutOfRangeException("task", SR.ArgumentOutOfRange_NeedNonNegNum);
			}
			if (task > 65535)
			{
				throw new ArgumentOutOfRangeException("task", SR.Format(SR.ArgumentOutOfRange_NeedValidId, 1, ushort.MaxValue));
			}
			this.m_task = (ushort)task;
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06005A0B RID: 23051 RVA: 0x001B3AB7 File Offset: 0x001B2CB7
		public int EventId
		{
			get
			{
				return (int)this.m_id;
			}
		}

		// Token: 0x17000EC9 RID: 3785
		// (get) Token: 0x06005A0C RID: 23052 RVA: 0x001B3ABF File Offset: 0x001B2CBF
		public byte Version
		{
			get
			{
				return this.m_version;
			}
		}

		// Token: 0x17000ECA RID: 3786
		// (get) Token: 0x06005A0D RID: 23053 RVA: 0x001B3AC7 File Offset: 0x001B2CC7
		public byte Channel
		{
			get
			{
				return this.m_channel;
			}
		}

		// Token: 0x17000ECB RID: 3787
		// (get) Token: 0x06005A0E RID: 23054 RVA: 0x001B3ACF File Offset: 0x001B2CCF
		public byte Level
		{
			get
			{
				return this.m_level;
			}
		}

		// Token: 0x17000ECC RID: 3788
		// (get) Token: 0x06005A0F RID: 23055 RVA: 0x001B3AD7 File Offset: 0x001B2CD7
		public byte Opcode
		{
			get
			{
				return this.m_opcode;
			}
		}

		// Token: 0x17000ECD RID: 3789
		// (get) Token: 0x06005A10 RID: 23056 RVA: 0x001B3ADF File Offset: 0x001B2CDF
		public int Task
		{
			get
			{
				return (int)this.m_task;
			}
		}

		// Token: 0x17000ECE RID: 3790
		// (get) Token: 0x06005A11 RID: 23057 RVA: 0x001B3AE7 File Offset: 0x001B2CE7
		public long Keywords
		{
			get
			{
				return this.m_keywords;
			}
		}

		// Token: 0x06005A12 RID: 23058 RVA: 0x001B3AF0 File Offset: 0x001B2CF0
		public override bool Equals(object obj)
		{
			if (obj is EventDescriptor)
			{
				EventDescriptor other = (EventDescriptor)obj;
				return this.Equals(other);
			}
			return false;
		}

		// Token: 0x06005A13 RID: 23059 RVA: 0x001B3B15 File Offset: 0x001B2D15
		public override int GetHashCode()
		{
			return (int)(this.m_id ^ (ushort)this.m_version ^ (ushort)this.m_channel ^ (ushort)this.m_level ^ (ushort)this.m_opcode ^ this.m_task) ^ (int)this.m_keywords;
		}

		// Token: 0x06005A14 RID: 23060 RVA: 0x001B3B48 File Offset: 0x001B2D48
		public bool Equals(EventDescriptor other)
		{
			return this.m_id == other.m_id && this.m_version == other.m_version && this.m_channel == other.m_channel && this.m_level == other.m_level && this.m_opcode == other.m_opcode && this.m_task == other.m_task && this.m_keywords == other.m_keywords;
		}

		// Token: 0x04001A2B RID: 6699
		[FieldOffset(0)]
		private readonly int m_traceloggingId;

		// Token: 0x04001A2C RID: 6700
		[FieldOffset(0)]
		private readonly ushort m_id;

		// Token: 0x04001A2D RID: 6701
		[FieldOffset(2)]
		private readonly byte m_version;

		// Token: 0x04001A2E RID: 6702
		[FieldOffset(3)]
		private readonly byte m_channel;

		// Token: 0x04001A2F RID: 6703
		[FieldOffset(4)]
		private readonly byte m_level;

		// Token: 0x04001A30 RID: 6704
		[FieldOffset(5)]
		private readonly byte m_opcode;

		// Token: 0x04001A31 RID: 6705
		[FieldOffset(6)]
		private readonly ushort m_task;

		// Token: 0x04001A32 RID: 6706
		[FieldOffset(8)]
		private readonly long m_keywords;
	}
}
