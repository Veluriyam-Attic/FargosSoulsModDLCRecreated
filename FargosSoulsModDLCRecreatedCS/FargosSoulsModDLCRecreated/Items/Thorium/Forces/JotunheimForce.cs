using System;
using FargosSoulsModDLCRecreated.Items.Thorium.Enchantments;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using ThoriumMod.Items.BossThePrimordials.Aqua;
using ThoriumMod.Items.BossThePrimordials.Omni;
using ThoriumMod.Items.BossThePrimordials.Slag;

namespace FargosSoulsModDLCRecreated.Items.Thorium.Forces
{
	// Token: 0x02000013 RID: 19
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class JotunheimForce : ModItem
	{
		// Token: 0x0600003D RID: 61 RVA: 0x000036C8 File Offset: 0x000018C8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000372C File Offset: 0x0000192C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem depthDiverEnchantment;
			if (mod.TryFind<ModItem>("DepthDiverEnchantment", ref depthDiverEnchantment))
			{
				depthDiverEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem tideHunterEnchantment;
			if (mod.TryFind<ModItem>("TideHunterEnchantment", ref tideHunterEnchantment))
			{
				tideHunterEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem nagaSkinEnchantment;
			if (mod.TryFind<ModItem>("NagaSkinEnchantment", ref nagaSkinEnchantment))
			{
				nagaSkinEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem cryomancerEnchantment;
			if (mod.TryFind<ModItem>("CryomancerEnchantment", ref cryomancerEnchantment))
			{
				cryomancerEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem whisperingEnchantment;
			if (mod.TryFind<ModItem>("WhisperingEnchantment", ref whisperingEnchantment))
			{
				whisperingEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600003F RID: 63 RVA: 0x000037C4 File Offset: 0x000019C4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<DepthDiverEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<TideHunterEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NagaSkinEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CryomancerEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WhisperingEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 5);
			recipe.AddTile(412);
			recipe.Register();
		}
	}
}
