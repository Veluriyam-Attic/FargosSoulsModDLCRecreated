using System;
using SpiritMod.Items.Consumable;
using SpiritMod.Items.Consumable.Fish;
using SpiritMod.Items.Placeable;
using SpiritMod.Items.Sets.FloatingItems.Driftwood.DriftwoodArmor;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Enchantments
{
	// Token: 0x02000073 RID: 115
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class DriftwoodEnchant : ModItem
	{
		// Token: 0x060001DF RID: 479 RVA: 0x0000E58F File Offset: 0x0000C78F
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 1;
			base.Item.value = 20000;
			base.Item.accessory = true;
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x0000E5CB File Offset: 0x0000C7CB
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (player.ZoneBeach)
			{
				player.fishingSkill += 5;
				player.cratePotion = true;
				player.statDefense += 3;
			}
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<DriftwoodHelmet>(1);
			recipe.AddIngredient<DriftwoodChestplate>(1);
			recipe.AddIngredient<DriftwoodLeggings>(1);
			recipe.AddIngredient<FishChips>(1);
			recipe.AddIngredient<CrinoidItem>(1);
			recipe.AddIngredient<FishCrate>(2);
			recipe.AddTile(26);
			recipe.Register();
		}
	}
}
