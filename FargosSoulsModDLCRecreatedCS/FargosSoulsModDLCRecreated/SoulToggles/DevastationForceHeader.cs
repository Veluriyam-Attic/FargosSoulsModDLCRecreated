using System;
using FargosSoulsModDLCRecreated.Items.Calamity.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000AB RID: 171
	public class DevastationForceHeader : SoulHeader
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x060002E6 RID: 742 RVA: 0x000189DC File Offset: 0x00016BDC
		public override float Priority
		{
			get
			{
				return 8.1f;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x060002E7 RID: 743 RVA: 0x000189E3 File Offset: 0x00016BE3
		public override int Item
		{
			get
			{
				return ModContent.ItemType<DevastationForce>();
			}
		}
	}
}
