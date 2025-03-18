using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000AE RID: 174
	public class MidgardForceHeader : SoulHeader
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x060002EF RID: 751 RVA: 0x00018A0E File Offset: 0x00016C0E
		public override float Priority
		{
			get
			{
				return 9.5f;
			}
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x060002F0 RID: 752 RVA: 0x00018A15 File Offset: 0x00016C15
		public override int Item
		{
			get
			{
				return ModContent.ItemType<MidgardForce>();
			}
		}
	}
}
