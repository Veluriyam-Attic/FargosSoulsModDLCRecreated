using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000AC RID: 172
	public class MuspelheimForceHeader : SoulHeader
	{
		// Token: 0x17000007 RID: 7
		// (get) Token: 0x060002E9 RID: 745 RVA: 0x000189F2 File Offset: 0x00016BF2
		public override float Priority
		{
			get
			{
				return 9f;
			}
		}

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x060002EA RID: 746 RVA: 0x000189F9 File Offset: 0x00016BF9
		public override int Item
		{
			get
			{
				return ModContent.ItemType<MuspelheimForce>();
			}
		}
	}
}
