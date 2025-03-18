using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000B4 RID: 180
	public class JotunheimForceHeader : SoulHeader
	{
		// Token: 0x17000017 RID: 23
		// (get) Token: 0x06000301 RID: 769 RVA: 0x00018A5B File Offset: 0x00016C5B
		public override float Priority
		{
			get
			{
				return 9.1f;
			}
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x06000302 RID: 770 RVA: 0x000189F9 File Offset: 0x00016BF9
		public override int Item
		{
			get
			{
				return ModContent.ItemType<MuspelheimForce>();
			}
		}
	}
}
