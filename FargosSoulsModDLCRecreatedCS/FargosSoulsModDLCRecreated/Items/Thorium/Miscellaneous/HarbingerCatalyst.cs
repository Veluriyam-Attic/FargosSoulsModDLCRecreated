using System;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Tiles;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x0200001D RID: 29
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class HarbingerCatalyst : ModItem
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00005478 File Offset: 0x00003678
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.maxStack = 99;
			base.Item.value = Item.sellPrice(0, 0, 48, 0);
			base.Item.rare = 7;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x000054D0 File Offset: 0x000036D0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(2);
			recipe.AddIngredient(ModContent.ItemType<ConcentratedThorium>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ArcaneDust>(), 1);
			recipe.AddIngredient(520, 1);
			recipe.AddIngredient(501, 3);
			recipe.AddTile<SoulForge>();
			recipe.Register();
		}
	}
}
