using System;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x0200001F RID: 31
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BerserkerGreaves : ModItem
	{
		// Token: 0x0600006D RID: 109 RVA: 0x00005525 File Offset: 0x00003725
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 5, 20, 0);
			base.Item.rare = 7;
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00005591 File Offset: 0x00003791
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<BerserkerShard>(), 12);
			recipe.AddTile(16);
			recipe.Register();
		}
	}
}
