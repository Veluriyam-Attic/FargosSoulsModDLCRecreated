using System;
using System.Reflection;

namespace System.Diagnostics.Tracing
{
	// Token: 0x0200073B RID: 1851
	[DefaultMember("Item")]
	internal struct SessionMask
	{
		// Token: 0x06005B5B RID: 23387 RVA: 0x001BC632 File Offset: 0x001BB832
		public SessionMask(uint mask = 0U)
		{
			this.m_mask = (mask & 15U);
		}

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06005B5C RID: 23388 RVA: 0x001BC63E File Offset: 0x001BB83E
		public static SessionMask All
		{
			get
			{
				return new SessionMask(15U);
			}
		}

		// Token: 0x06005B5D RID: 23389 RVA: 0x001BC647 File Offset: 0x001BB847
		public ulong ToEventKeywords()
		{
			return (ulong)this.m_mask << 44;
		}

		// Token: 0x06005B5E RID: 23390 RVA: 0x001BC653 File Offset: 0x001BB853
		public static SessionMask FromEventKeywords(ulong m)
		{
			return new SessionMask((uint)(m >> 44));
		}

		// Token: 0x06005B5F RID: 23391 RVA: 0x001BC65F File Offset: 0x001BB85F
		public static explicit operator uint(SessionMask m)
		{
			return m.m_mask;
		}

		// Token: 0x04001AF9 RID: 6905
		private uint m_mask;
	}
}
