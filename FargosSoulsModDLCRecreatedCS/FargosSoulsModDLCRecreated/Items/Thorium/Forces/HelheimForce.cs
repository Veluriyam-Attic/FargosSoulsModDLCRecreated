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
	// Token: 0x0200000C RID: 12
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class HelheimForce : ModItem
	{
		// Token: 0x06000021 RID: 33 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002C34 File Offset: 0x00000E34
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem spiritTrapperEnchantment;
			if (mod.TryFind<ModItem>("SpiritTrapperEnchantment", ref spiritTrapperEnchantment))
			{
				spiritTrapperEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem malignantEnchantment;
			if (mod.TryFind<ModItem>("MalignantEnchantment", ref malignantEnchantment))
			{
				malignantEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem dreadEnchantment;
			if (mod.TryFind<ModItem>("DreadEnchantment", ref dreadEnchantment))
			{
				dreadEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem demonBloodEnchantment;
			if (mod.TryFind<ModItem>("DemonBloodEnchantment", ref demonBloodEnchantment))
			{
				demonBloodEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem berserkerEnchantment;
			if (mod.TryFind<ModItem>("BerserkerEnchantment", ref berserkerEnchantment))
			{
				berserkerEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem harbingerEnchantment;
			if (mod.TryFind<ModItem>("HarbingerEnchantment", ref harbingerEnchantment))
			{
				harbingerEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002CE4 File Offset: 0x00000EE4
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SpiritTrapperEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MalignantEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreadEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DemonBloodEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<BerserkerEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<HarbingerEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 5);
			recipe.AddTile(412);
			recipe.Register();
		}
	}
}
