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
	// Token: 0x02000011 RID: 17
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class AsgardForce : ModItem
	{
		// Token: 0x06000035 RID: 53 RVA: 0x000033B0 File Offset: 0x000015B0
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003414 File Offset: 0x00001614
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem tideTurnerEnchantment;
			if (mod.TryFind<ModItem>("TideTurnerEnchantment", ref tideTurnerEnchantment))
			{
				tideTurnerEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem assassinEnchantment;
			if (mod.TryFind<ModItem>("AssassinEnchantment", ref assassinEnchantment))
			{
				assassinEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem pyromancerEnchantment;
			if (mod.TryFind<ModItem>("PyromancerEnchantment", ref pyromancerEnchantment))
			{
				pyromancerEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem dreamWeaverEnchantment;
			if (mod.TryFind<ModItem>("DreamWeaverEnchantment", ref dreamWeaverEnchantment))
			{
				dreamWeaverEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem rhapsodistEnchantment;
			if (mod.TryFind<ModItem>("RhapsodistEnchantment", ref rhapsodistEnchantment))
			{
				rhapsodistEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000034AC File Offset: 0x000016AC
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<TideTurnerEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AssassinEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<PyromancerEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DreamWeaverEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<RhapsodistEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 5);
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
