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
	// Token: 0x0200000D RID: 13
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class MidgardForce : ModItem
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002D80 File Offset: 0x00000F80
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002DE4 File Offset: 0x00000FE4
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem terrariumEnchantment;
			if (mod.TryFind<ModItem>("TerrariumEnchantment", ref terrariumEnchantment))
			{
				terrariumEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem lodestoneEnchantment;
			if (mod.TryFind<ModItem>("LodestoneEnchantment", ref lodestoneEnchantment))
			{
				lodestoneEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem valadiumEnchantment;
			if (mod.TryFind<ModItem>("ValadiumEnchantment", ref valadiumEnchantment))
			{
				valadiumEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem illumiteEnchantment;
			if (mod.TryFind<ModItem>("IllumiteEnchantment", ref illumiteEnchantment))
			{
				illumiteEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem shadeMasterEnchantment;
			if (mod.TryFind<ModItem>("ShadeMasterEnchantment", ref shadeMasterEnchantment))
			{
				shadeMasterEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem geodeEnchantment;
			if (mod.TryFind<ModItem>("GeodeEnchantment", ref geodeEnchantment))
			{
				geodeEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002E94 File Offset: 0x00001094
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<TerrariumEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LodestoneEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ValadiumEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<IllumiteEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<ShadeMasterEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<GeodeEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 5);
			recipe.AddTile(412);
			recipe.Register();
		}
	}
}
