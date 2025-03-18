using System;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Tiles;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000027 RID: 39
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DangerHelmet : ModItem
	{
		// Token: 0x06000084 RID: 132 RVA: 0x0000589D File Offset: 0x00003A9D
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 0, 60, 0);
			base.Item.rare = 2;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x000058DA File Offset: 0x00003ADA
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DangerShard>(), 6);
			recipe.AddTile<ArcaneArmorFabricator>();
			recipe.Register();
		}
	}
}
