using System;

namespace System
{
	// Token: 0x020000D3 RID: 211
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class AttributeUsageAttribute : Attribute
	{
		// Token: 0x06000A95 RID: 2709 RVA: 0x000C9267 File Offset: 0x000C8467
		public AttributeUsageAttribute(AttributeTargets validOn)
		{
			this._attributeTarget = validOn;
			this._inherited = true;
		}

		// Token: 0x06000A96 RID: 2710 RVA: 0x000C927D File Offset: 0x000C847D
		internal AttributeUsageAttribute(AttributeTargets validOn, bool allowMultiple, bool inherited)
		{
			this._attributeTarget = validOn;
			this._allowMultiple = allowMultiple;
			this._inherited = inherited;
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x06000A97 RID: 2711 RVA: 0x000C929A File Offset: 0x000C849A
		public AttributeTargets ValidOn
		{
			get
			{
				return this._attributeTarget;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x06000A98 RID: 2712 RVA: 0x000C92A2 File Offset: 0x000C84A2
		// (set) Token: 0x06000A99 RID: 2713 RVA: 0x000C92AA File Offset: 0x000C84AA
		public bool AllowMultiple
		{
			get
			{
				return this._allowMultiple;
			}
			set
			{
				this._allowMultiple = value;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x06000A9A RID: 2714 RVA: 0x000C92B3 File Offset: 0x000C84B3
		// (set) Token: 0x06000A9B RID: 2715 RVA: 0x000C92BB File Offset: 0x000C84BB
		public bool Inherited
		{
			get
			{
				return this._inherited;
			}
			set
			{
				this._inherited = value;
			}
		}

		// Token: 0x0400029B RID: 667
		private readonly AttributeTargets _attributeTarget;

		// Token: 0x0400029C RID: 668
		private bool _allowMultiple;

		// Token: 0x0400029D RID: 669
		private bool _inherited;

		// Token: 0x0400029E RID: 670
		internal static readonly AttributeUsageAttribute Default = new AttributeUsageAttribute(AttributeTargets.All);
	}
}
