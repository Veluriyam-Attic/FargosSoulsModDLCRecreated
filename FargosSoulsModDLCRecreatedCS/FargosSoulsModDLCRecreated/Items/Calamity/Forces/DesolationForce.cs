using System;
using System.Collections.Generic;
using CalamityMod.Items.Materials;
using FargosSoulsModDLCRecreated.Items.Calamity.Enchantments;
using Fargowiltas.Items.Tiles;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace FargosSoulsModDLCRecreated.Items.Calamity.Forces
{
	// Token: 0x0200008F RID: 143
	[JITWhenModsEnabled(new string[]
	{
		"CalamityMod"
	})]
	[ExtendsFromMod(new string[]
	{
		"CalamityMod"
	})]
	public class DesolationForce : ModItem
	{
		// Token: 0x06000254 RID: 596 RVA: 0x000105EC File Offset: 0x0000E7EC
		public override void ModifyTooltips(List<TooltipLine> list)
		{
			list.Add(new TooltipLine(base.Mod, "Tooltip1", "When the world is barren and cold, you will be all that remains"));
			list.Add(new TooltipLine(base.Mod, "Tooltip2", "All armor bonuses from Daedalus, Snow Ruffian, Umbraphile, and Astral"));
			list.Add(new TooltipLine(base.Mod, "Tooltip3", "All armor bonuses from Omega Blue, Mollusk, Victide, Fathom Swarmer, and Sulphurous"));
			list.Add(new TooltipLine(base.Mod, "Tooltip4", "Effects of Scuttler's Jewel and Permafrost Concoction"));
			list.Add(new TooltipLine(base.Mod, "Tooltip5", "Effects of Cryo Stone, Howls Heart, Frost Barrier, and Frost Flare"));
			list.Add(new TooltipLine(base.Mod, "Tooltip6", "Effects of Thief's Dime, Vampiric Talisman, and Evasion Scarf"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Radiance, Gravistar Sabaton and Ursa Sergeant"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip7", "Effects of Hide of Asturm Deus, Gravistar Sabaton and Ursa Sergeant"));
			}
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of the Abyssal Diving Suit and Reaper Tooth Necklace"));
			}
			else
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip8", "Effects of Old Duke Scales and Reaper Tooth Necklace"));
			}
			list.Add(new TooltipLine(base.Mod, "Tooltip9", "Effects of Aquatic Emblem, Shield of the Ocean, and Giant Pearl"));
			list.Add(new TooltipLine(base.Mod, "Tooltip10", "Effects of Ocean's Crest, Deep Diver and Luxor's Gift"));
			list.Add(new TooltipLine(base.Mod, "Tooltip11", "Effects of Corrosive Spine, Lumenous Amulet, Amadias Pendant, and Leviathan Ambergris"));
			if (!ModLoader.HasMod("FargowiltasCrossmod"))
			{
				list.Add(new TooltipLine(base.Mod, "Tooltip12", "Effects of Sand Cloak, Old Die, Rusty Medallion, Aquatic Heart and Alluring Bait"));
				return;
			}
			list.Add(new TooltipLine(base.Mod, "Tooltip12", "Effects of Sand Cloak, Old Die, Rusty Medallion, and Aquatic Heart"));
		}

		// Token: 0x06000255 RID: 597 RVA: 0x000107B8 File Offset: 0x0000E9B8
		public override void SetDefaults()
		{
			base.Item.width = 20;
			base.Item.height = 20;
			base.Item.accessory = true;
			ItemID.Sets.ItemNoGravity[base.Item.type] = true;
			base.Item.rare = 11;
			base.Item.value = 600000;
		}

		// Token: 0x06000256 RID: 598 RVA: 0x0001081C File Offset: 0x0000EA1C
		public override void UpdateAccessory(Player player, bool hideVisual)
		{
			if (!ModLoader.HasMod("CalamityMod"))
			{
				return;
			}
			ModLoader.GetMod("CalamityMod");
			Mod mod = ModLoader.GetMod("FargosSoulsModDLCRecreated");
			ModItem molluskEnchantment;
			if (mod.TryFind<ModItem>("MolluskEnchantment", ref molluskEnchantment))
			{
				molluskEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem fathomSwarmerEnchantment;
			if (mod.TryFind<ModItem>("FathomSwarmerEnchantment", ref fathomSwarmerEnchantment))
			{
				fathomSwarmerEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem daedalusEnchantment;
			if (mod.TryFind<ModItem>("DaedalusEnchantment", ref daedalusEnchantment))
			{
				daedalusEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem umbraphileEnchantment;
			if (mod.TryFind<ModItem>("UmbraphileEnchantment", ref umbraphileEnchantment))
			{
				umbraphileEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem astralEnchantment;
			if (mod.TryFind<ModItem>("AstralEnchantment", ref astralEnchantment))
			{
				astralEnchantment.UpdateAccessory(player, hideVisual);
			}
			ModItem omegaBlueEnchantment;
			if (mod.TryFind<ModItem>("OmegaBlueEnchantment", ref omegaBlueEnchantment))
			{
				omegaBlueEnchantment.UpdateAccessory(player, hideVisual);
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x000108D8 File Offset: 0x0000EAD8
		public override void AddRecipes()
		{
			Recipe recipe = base.CreateRecipe(1);
			recipe.AddIngredient(ModContent.ItemType<MolluskEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<FathomSwarmerEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DaedalusEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<AstralEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<OmegaBlueEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<UmbraphileEnchantment>(), 1);
			recipe.AddIngredient(ModContent.ItemType<DivineGeode>(), 4);
			recipe.AddTile(ModContent.TileType<CrucibleCosmosSheet>());
			recipe.Register();
		}
	}
}
