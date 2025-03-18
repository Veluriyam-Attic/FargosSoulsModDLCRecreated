using System;
using FargosSoulsModDLCRecreated.Items.Calamity.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000B7 RID: 183
	public class ExaltationForceHeader : SoulHeader
	{
		// Token: 0x1700001D RID: 29
		// (get) Token: 0x0600030A RID: 778 RVA: 0x00018A7E File Offset: 0x00016C7E
		public override float Priority
		{
			get
			{
				return 8f;
			}
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x0600030B RID: 779 RVA: 0x00018A85 File Offset: 0x00016C85
		public override int Item
		{
			get
			{
				return ModContent.ItemType<ExaltationForce>();
			}
		}
	}
}
