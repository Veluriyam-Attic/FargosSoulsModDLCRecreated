using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Enchantments;
using Fargowiltas.Items.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Slag;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Forces
{
	// Token: 0x0200000F RID: 15
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class VanaheimForce : ModItem
	{
		// Token: 0x0600002D RID: 45 RVA: 0x000030BC File Offset: 0x000012BC
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003120 File Offset: 0x00001320
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem lichEnchantment;
			if (mod.TryFind<ModItem>("LichEnchantment", ref lichEnchantment))
			{
				lichEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem whiteDwarfEnchantment;
			if (mod.TryFind<ModItem>("WhiteDwarfEnchantment", ref whiteDwarfEnchantment))
			{
				whiteDwarfEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem celestialEnchantment;
			if (mod.TryFind<ModItem>("CelestialEnchantment", ref celestialEnchantment))
			{
				celestialEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem shootingStarEnchantment;
			if (mod.TryFind<ModItem>("ShootingStarEnchantment", ref shootingStarEnchantment))
			{
				shootingStarEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600002F RID: 47 RVA: 0x000031A0 File Offset: 0x000013A0
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<LichEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhiteDwarfEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CelestialEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShootingStarEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 5);
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
