using System;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000025 RID: 37
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class MagmaChestguard : ModItem
	{
		// Token: 0x0600007E RID: 126 RVA: 0x000057B4 File Offset: 0x000039B4
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 0, 75, 0);
			base.Item.rare = 1;
		}

		// Token: 0x0600007F RID: 127 RVA: 0x000057F1 File Offset: 0x000039F1
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(175, 18);
			recipe.AddIngredient(1322, 1);
			recipe.AddTile(16);
			recipe.Register();
		}
	}
}
