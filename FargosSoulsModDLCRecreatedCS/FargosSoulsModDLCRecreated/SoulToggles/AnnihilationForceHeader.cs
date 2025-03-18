using System;
using FargosSoulsModDLCRecreated.Items.Calamity.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000B3 RID: 179
	public class AnnihilationForceHeader : SoulHeader
	{
		// Token: 0x17000015 RID: 21
		// (get) Token: 0x060002FE RID: 766 RVA: 0x00018A4D File Offset: 0x00016C4D
		public override float Priority
		{
			get
			{
				return 8.3f;
			}
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x060002FF RID: 767 RVA: 0x00018A54 File Offset: 0x00016C54
		public override int Item
		{
			get
			{
				return ModContent.ItemType<AnnihilationForce>();
			}
		}
	}
}
