using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000AF RID: 175
	public class HelheimForceHeader : SoulHeader
	{
		// Token: 0x1700000D RID: 13
		// (get) Token: 0x060002F2 RID: 754 RVA: 0x00018A00 File Offset: 0x00016C00
		public override float Priority
		{
			get
			{
				return 9.8f;
			}
		}

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x060002F3 RID: 755 RVA: 0x00018A1C File Offset: 0x00016C1C
		public override int Item
		{
			get
			{
				return ModContent.ItemType<HelheimForce>();
			}
		}
	}
}
