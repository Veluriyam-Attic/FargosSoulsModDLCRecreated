using System;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x0200001B RID: 27
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BerserkerBreastplate : ModItem
	{
		// Token: 0x06000061 RID: 97 RVA: 0x00005385 File Offset: 0x00003585
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 6, 24, 0);
			base.Item.rare = 7;
		}

		// Token: 0x06000062 RID: 98 RVA: 0x000053C2 File Offset: 0x000035C2
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BerserkerShard>(), 14);
			recipe.AddTile(16);
			recipe.Register();
		}
	}
}
