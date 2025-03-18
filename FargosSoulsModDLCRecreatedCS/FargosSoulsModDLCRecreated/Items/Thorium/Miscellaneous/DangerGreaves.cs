using System;
using Terraria;
using Terraria.ModLoader;
using ThoriumMod.Tiles;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Miscellaneous
{
	// Token: 0x02000021 RID: 33
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class DangerGreaves : ModItem
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00005656 File Offset: 0x00003856
		public override void SetDefaults()
		{
			base.Item.width = 18;
			base.Item.height = 18;
			base.Item.value = Item.sellPrice(0, 0, 70, 0);
			base.Item.rare = 2;
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00005693 File Offset: 0x00003893
		public override void AddRecipes()
		{
			ModLoader.GetMod("ThoriumMod");
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DangerShard>(), 8);
			recipe.AddTile<ArcaneArmorFabricator>();
			recipe.Register();
		}
	}
}
