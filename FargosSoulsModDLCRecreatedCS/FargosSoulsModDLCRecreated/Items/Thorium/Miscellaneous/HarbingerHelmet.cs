using System;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Tiles;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x0200001A RID: 26
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class HarbingerHelmet : ModItem
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00005319 File Offset: 0x00003519
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 4, 16, 0);
			base.Item.rare = 7;
		}

		// Token: 0x0600005F RID: 95 RVA: 0x00005356 File Offset: 0x00003556
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<HarbingerCatalyst>(), 12);
			recipe.AddTile<SoulForge>();
			recipe.Register();
		}
	}
}
