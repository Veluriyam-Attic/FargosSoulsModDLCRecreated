using System;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Items.Misc;
using ThoriumMod.Items.Thorium;
using ThoriumMod.Tiles;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000020 RID: 32
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class BerserkerShard : ModItem
	{
		// Token: 0x06000070 RID: 112 RVA: 0x000055C4 File Offset: 0x000037C4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.maxStack = 99;
			base.Item.value = Item.sellPrice(0, 0, 48, 0);
			base.Item.rare = 7;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00005619 File Offset: 0x00003819
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(2);
			recipe.AddIngredient(ModContent.ItemType<ConcentratedThorium>(), 1);
			recipe.AddIngredient(175, 2);
			recipe.AddIngredient(ModContent.ItemType<SoulofPlight>(), 1);
			recipe.AddTile<SoulForge>();
			recipe.Register();
		}
	}
}
