using System;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Tiles;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000029 RID: 41
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DangerMail : ModItem
	{
		// Token: 0x0600008A RID: 138 RVA: 0x0000592D File Offset: 0x00003B2D
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 0, 80, 0);
			base.Item.rare = 2;
		}

		// Token: 0x0600008B RID: 139 RVA: 0x0000596A File Offset: 0x00003B6A
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DangerShard>(), 10);
			recipe.AddTile<ArcaneArmorFabricator>();
			recipe.Register();
		}
	}
}
