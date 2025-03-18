using System;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Tiles;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000022 RID: 34
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class HarbingerChestguard : ModItem
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00005385 File Offset: 0x00003585
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 6, 24, 0);
			base.Item.rare = 7;
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000056C1 File Offset: 0x000038C1
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<HarbingerCatalyst>(), 16);
			recipe.AddTile<SoulForge>();
			recipe.Register();
		}
	}
}
