using System;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x0200001C RID: 28
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class ArcaneDust : ModItem
	{
		// Token: 0x06000064 RID: 100 RVA: 0x000053F4 File Offset: 0x000035F4
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.maxStack = 99;
			base.Item.value = Item.sellPrice(0, 0, 0, 80);
			base.Item.rare = 1;
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00005449 File Offset: 0x00003649
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(5);
			recipe.AddIngredient(75, 1);
			recipe.AddTile(18);
			recipe.Register();
		}
	}
}
