using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000B2 RID: 178
	public class AlfheimForceHeader : SoulHeader
	{
		// Token: 0x17000013 RID: 19
		// (get) Token: 0x060002FB RID: 763 RVA: 0x00018A3F File Offset: 0x00016C3F
		public override float Priority
		{
			get
			{
				return 9.2f;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x060002FC RID: 764 RVA: 0x00018A46 File Offset: 0x00016C46
		public override int Item
		{
			get
			{
				return ModContent.ItemType<AlfheimForce>();
			}
		}
	}
}
