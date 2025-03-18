using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000B0 RID: 176
	public class VanaheimForceHeader : SoulHeader
	{
		// Token: 0x1700000F RID: 15
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x00018A23 File Offset: 0x00016C23
		public override float Priority
		{
			get
			{
				return 9.6f;
			}
		}

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x060002F6 RID: 758 RVA: 0x00018A2A File Offset: 0x00016C2A
		public override int Item
		{
			get
			{
				return ModContent.ItemType<VanaheimForce>();
			}
		}
	}
}
