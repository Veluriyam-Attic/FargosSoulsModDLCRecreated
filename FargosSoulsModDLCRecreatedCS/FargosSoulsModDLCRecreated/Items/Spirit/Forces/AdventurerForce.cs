using System;
using FargosSoulsModDLCRecreated.Items.Spirit.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Spirit.Forces
{
	// Token: 0x0200006C RID: 108
	[JITWhenModsEnabled(new string[]
	{
		"SpiritMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"SpiritMod"
	})]
	public class AdventurerForce : ModItem
	{
		// Token: 0x060001C2 RID: 450 RVA: 0x0000DB42 File Offset: 0x0000BD42
		public override void SetDefaults()
		{
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 400000;
			base.Item.accessory = true;
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x0000DB80 File Offset: 0x0000BD80
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			player.GetDamage(DamageClass.Generic).Flat += 10f;
			ModContent.Find<ModItem>(base.Mod.Name, "DriftwoodEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "BotanistEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "FloranEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "WayfarersEnchant").UpdateAccessory(player, false);
			ModContent.Find<ModItem>(base.Mod.Name, "SunflowerEnchant").UpdateAccessory(player, false);
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x0000DC34 File Offset: 0x0000BE34
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient<ElderbarkEnchant>(1);
			recipe.AddIngredient<DriftwoodEnchant>(1);
			recipe.AddIngredient<BotanistEnchant>(1);
			recipe.AddIngredient<FloranEnchant>(1);
			recipe.AddIngredient<WayfarersEnchant>(1);
			recipe.AddIngredient<SunflowerEnchant>(1);
			recipe.AddTile(ModContent.Find<ModTile>("Fargowiltas", "CrucibleCosmosSheet"));
			recipe.Register();
		}
	}
}
