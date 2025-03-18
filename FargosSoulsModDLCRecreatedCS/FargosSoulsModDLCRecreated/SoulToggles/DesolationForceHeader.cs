using System;
using FargosSoulsModDLCRecreated.Items.Calamity.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000B5 RID: 181
	public class DesolationForceHeader : SoulHeader
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x06000304 RID: 772 RVA: 0x00018A62 File Offset: 0x00016C62
		public override float Priority
		{
			get
			{
				return 8.2f;
			}
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000305 RID: 773 RVA: 0x00018A69 File Offset: 0x00016C69
		public override int Item
		{
			get
			{
				return ModContent.ItemType<DesolationForce>();
			}
		}
	}
}
