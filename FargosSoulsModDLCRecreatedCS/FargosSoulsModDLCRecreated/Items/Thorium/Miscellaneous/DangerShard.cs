using System;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000024 RID: 36
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DangerShard : ModItem
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00005760 File Offset: 0x00003960
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.maxStack = 99;
			base.Item.value = Item.sellPrice(0, 0, 1, 0);
			base.Item.rare = 2;
		}
	}
}
