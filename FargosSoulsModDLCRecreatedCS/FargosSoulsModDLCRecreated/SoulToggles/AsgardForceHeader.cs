using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000AD RID: 173
	public class AsgardForceHeader : SoulHeader
	{
		// Token: 0x17000009 RID: 9
		// (get) Token: 0x060002EC RID: 748 RVA: 0x00018A00 File Offset: 0x00016C00
		public override float Priority
		{
			get
			{
				return 9.8f;
			}
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x060002ED RID: 749 RVA: 0x00018A07 File Offset: 0x00016C07
		public override int Item
		{
			get
			{
				return ModContent.ItemType<AsgardForce>();
			}
		}
	}
}
