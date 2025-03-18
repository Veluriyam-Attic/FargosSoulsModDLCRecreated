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
	// Token: 0x02000010 RID: 16
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class AlfheimForce : ModItem
	{
		// Token: 0x06000031 RID: 49 RVA: 0x00003224 File Offset: 0x00001424
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003288 File Offset: 0x00001488
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem sacredEnchantment;
			if (mod.TryFind<ModItem>("SacredEnchantment", ref sacredEnchantment))
			{
				sacredEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem warlockEnchantment;
			if (mod.TryFind<ModItem>("WarlockEnchantment", ref warlockEnchantment))
			{
				warlockEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem biotechEnchantment;
			if (mod.TryFind<ModItem>("BiotechEnchantment", ref biotechEnchantment))
			{
				biotechEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem lifeBinderEnchantment;
			if (mod.TryFind<ModItem>("LifeBinderEnchantment", ref lifeBinderEnchantment))
			{
				lifeBinderEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem fallenPaladinEnchantment;
			if (mod.TryFind<ModItem>("FallenPaladinEnchantment", ref fallenPaladinEnchantment))
			{
				fallenPaladinEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00003320 File Offset: 0x00001520
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SacredEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<WarlockEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BiotechEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeBinderEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FallenPaladinEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 5);
			recipe.AddTile(412);
			recipe.Register();
		}
	}
}
