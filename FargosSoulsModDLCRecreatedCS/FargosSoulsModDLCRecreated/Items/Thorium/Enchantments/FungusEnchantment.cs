using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.Consumable;
using ThoriumMod.Items.MagicItems;
using ThoriumMod.Items.ThrownItems;
using ThoriumMod.Utilities;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Enchantments
{
	// Token: 0x02000057 RID: 87
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class FungusEnchantment : ModItem
	{
		// Token: 0x06000163 RID: 355 RVA: 0x0000B580 File Offset: 0x00009780
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 4;
			base.Item.value = 120000;
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000B5E1 File Offset: 0x000097E1
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			ModLoader.GetMod("ThoriumMod");
			if (ModLoader.HasMod("ThoriumMod"))
			{
				PlayerHelper.GetThoriumPlayer(player).setFungus = true;
			}
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000B614 File Offset: 0x00009814
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<FungusHat>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FungusGuard>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FungusLeggings>(), 1);
			recipe.AddIngredient(ModContent.ItemType<Spores>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SwampSpike>(), 1);
			recipe.AddIngredient(ModContent.ItemType<SporeCoatingItem>(), 10);
			recipe.AddTile(125);
			recipe.Register();
		}
	}
}
