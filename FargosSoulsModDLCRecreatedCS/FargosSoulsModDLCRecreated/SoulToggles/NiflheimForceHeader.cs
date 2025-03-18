using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Forces;
using FargowiltasSouls.Core.Toggler.Content;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.SoulToggles
{
	// Token: 0x020000B1 RID: 177
	public class NiflheimForceHeader : SoulHeader
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x060002F8 RID: 760 RVA: 0x00018A31 File Offset: 0x00016C31
		public override float Priority
		{
			get
			{
				return 9.3f;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x00018A38 File Offset: 0x00016C38
		public override int Item
		{
			get
			{
				return ModContent.ItemType<NiflheimForce>();
			}
		}
	}
}
