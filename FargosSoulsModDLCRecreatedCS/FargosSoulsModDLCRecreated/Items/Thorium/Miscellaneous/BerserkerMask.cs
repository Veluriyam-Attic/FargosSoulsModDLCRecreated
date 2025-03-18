using System;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000028 RID: 40
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BerserkerMask : ModItem
	{
		// Token: 0x06000087 RID: 135 RVA: 0x00005319 File Offset: 0x00003519
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 4, 16, 0);
			base.Item.rare = 7;
		}

		// Token: 0x06000088 RID: 136 RVA: 0x000058FD File Offset: 0x00003AFD
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BerserkerShard>(), 8);
			recipe.AddTile(16);
			recipe.Register();
		}
	}
}
