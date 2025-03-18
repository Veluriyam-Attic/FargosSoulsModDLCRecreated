using System;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Tiles;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x0200001E RID: 30
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class HarbingerGreaves : ModItem
	{
		// Token: 0x0600006A RID: 106 RVA: 0x00005525 File Offset: 0x00003725
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 5, 20, 0);
			base.Item.rare = 7;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00005562 File Offset: 0x00003762
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<HarbingerCatalyst>(), 14);
			recipe.AddTile<SoulForge>();
			recipe.Register();
		}
	}
}
