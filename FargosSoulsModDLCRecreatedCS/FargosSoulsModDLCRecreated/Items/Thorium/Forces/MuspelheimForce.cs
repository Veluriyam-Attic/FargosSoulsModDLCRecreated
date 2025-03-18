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
	// Token: 0x02000014 RID: 20
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class MuspelheimForce : ModItem
	{
		// Token: 0x06000041 RID: 65 RVA: 0x00003854 File Offset: 0x00001A54
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000038B8 File Offset: 0x00001AB8
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem sandstoneEnchantment;
			if (mod.TryFind<ModItem>("SandstoneEnchantment", ref sandstoneEnchantment))
			{
				sandstoneEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem dangerEnchantment;
			if (mod.TryFind<ModItem>("DangerEnchantment", ref dangerEnchantment))
			{
				dangerEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem flightEnchantment;
			if (mod.TryFind<ModItem>("FlightEnchantment", ref flightEnchantment))
			{
				flightEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem fungusEnchantment;
			if (mod.TryFind<ModItem>("FungusEnchantment", ref fungusEnchantment))
			{
				fungusEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem lifeBloomEnchantment;
			if (mod.TryFind<ModItem>("LifeBloomEnchantment", ref lifeBloomEnchantment))
			{
				lifeBloomEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003950 File Offset: 0x00001B50
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<SandstoneEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DangerEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FlightEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FungusEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<LifeBloomEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 5);
			recipe.AddTile(412);
			recipe.Register();
		}
	}
}
