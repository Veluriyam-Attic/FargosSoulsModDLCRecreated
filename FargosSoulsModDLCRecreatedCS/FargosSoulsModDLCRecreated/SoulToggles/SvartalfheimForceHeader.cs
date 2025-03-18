using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000B6 RID: 182
	public class SvartalfheimForceHeader : SoulHeader
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000307 RID: 775 RVA: 0x00018A70 File Offset: 0x00016C70
		public override float Priority
		{
			get
			{
				return 9.4f;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x06000308 RID: 776 RVA: 0x00018A77 File Offset: 0x00016C77
		public override int Item
		{
			get
			{
				return ModContent.ItemType<SvartalfheimForce>();
			}
		}
	}
}
