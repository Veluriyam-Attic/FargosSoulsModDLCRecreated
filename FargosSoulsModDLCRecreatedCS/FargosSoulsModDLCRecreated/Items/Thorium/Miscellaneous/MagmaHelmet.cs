using System;
using Terraria;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000026 RID: 38
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class MagmaHelmet : ModItem
	{
		// Token: 0x06000081 RID: 129 RVA: 0x0000582F File Offset: 0x00003A2F
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 0, 45, 0);
			base.Item.rare = 1;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000586C File Offset: 0x00003A6C
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(175, 10);
			recipe.AddTile(16);
			recipe.Register();
		}
	}
}
