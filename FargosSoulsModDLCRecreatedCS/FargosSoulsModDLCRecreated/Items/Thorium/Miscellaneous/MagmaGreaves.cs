using System;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000023 RID: 35
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class MagmaGreaves : ModItem
	{
		// Token: 0x06000079 RID: 121 RVA: 0x000056F0 File Offset: 0x000038F0
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 0, 60, 0);
			base.Item.rare = 1;
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000572D File Offset: 0x0000392D
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(175, 14);
			recipe.AddTile(16);
			recipe.Register();
		}
	}
}
