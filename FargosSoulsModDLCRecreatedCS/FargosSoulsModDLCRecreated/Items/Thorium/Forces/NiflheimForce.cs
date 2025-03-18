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
	// Token: 0x0200000E RID: 14
	[JITWhenModsEnabled(new string[]
	{
		"ThoriumMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"ThoriumMod"
	})]
	public class NiflheimForce : ModItem
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002F30 File Offset: 0x00001130
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x0600002A RID: 42 RVA: 0x00002F94 File Offset: 0x00001194
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("ThoriumMod"))
			{
				return;
			}
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem crierEnchantment;
			if (mod.TryFind<ModItem>("CrierEnchantment", ref crierEnchantment))
			{
				crierEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem nobleEnchantment;
			if (mod.TryFind<ModItem>("NobleEnchantment", ref nobleEnchantment))
			{
				nobleEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem cyberPunkEnchantment;
			if (mod.TryFind<ModItem>("CyberPunkEnchantment", ref cyberPunkEnchantment))
			{
				cyberPunkEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem ornateEnchantment;
			if (mod.TryFind<ModItem>("OrnateEnchantment", ref ornateEnchantment))
			{
				ornateEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem maestroEnchantment;
			if (mod.TryFind<ModItem>("MaestroEnchantment", ref maestroEnchantment))
			{
				maestroEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x0600002B RID: 43 RVA: 0x0000302C File Offset: 0x0000122C
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<CrierEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<NobleEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<CyberPunkEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OrnateEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<MaestroEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<InfernoEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<DeathEssence>(), 5);
			recipe.AddIngredient(ModContent.ItemType<OceanEssence>(), 5);
			recipe.AddTile(412);
			recipe.Register();
		}
	}
}
